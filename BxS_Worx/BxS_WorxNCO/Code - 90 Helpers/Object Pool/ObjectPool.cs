﻿using System;
using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.ObjectPool
{
	public class ObjectPool<T> where T : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPool(	int			minimumPoolSize			= DefaultMinimumSize
													, int			maximumPoolSize			= DefaultMaximumSize
													,	bool		activateDiagnostics	= false
													, bool		populateAtStartup		= false
													, Func<T>	factory							= null								)
					{
						this._MinPoolSize				= minimumPoolSize	;
						this._MaxPoolSize				= maximumPoolSize	;
						this.DiagnosticsActive	= activateDiagnostics	;
						this.Factory						= factory							;
						//.............................................
						this._Pool					= new	ConcurrentBag<T>();
						this._Diag					= new	Lazy< ObjectPoolDiagnostics >(	()=>	new	ObjectPoolDiagnostics() );
						this._ReturnAction	=	this.ReturnObject;
						this._Ready					= true;
						//.............................................
						this.SetLimits();
						//.............................................
						if ( populateAtStartup )
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	int	DefaultMinimumSize	=	01;
				private const int DefaultMaximumSize	=	05;

				private int		_MinPoolSize;
				private int		_MaxPoolSize;
				private	bool	_Ready			;

				private readonly Lazy< ObjectPoolDiagnostics >	_Diag;
				private readonly ConcurrentBag<T>								_Pool;
				private readonly Action< PooledObject , bool >	_ReturnAction;

			#endregion

			//===========================================================================================
			#region "Properties"

				public Func<T>								Factory						{	get; }
				public bool										DiagnosticsActive	{ get; set; }
				public int										Count							{	get { return	this._Pool.Count; }	}
				public ObjectPoolDiagnostics	Diagnostics				{ get { return	this._Diag.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Acquire()
					{
						if ( this._Pool.TryTake( out T lo_Object ) )
							{
								if ( this.DiagnosticsActive )	this._Diag.Value.UpHitCount();
								return lo_Object;
							}
						else
							{
								if ( this.DiagnosticsActive )	this._Diag.Value.UpMissCount();
								return CreateObject();
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReturnObject(		PooledObject	objectToReturn
																		, bool					reRegisterForFinalization = false	)
					{
						if ( this.DiagnosticsActive && reRegisterForFinalization ) this._Diag.Value.UpRessurectionCount();
						//.............................................
						// Checking that the pool is not full
						//
						if ( this._Pool.Count < this._MaxPoolSize )
							{
								// Reset object state (if implemented) before returning to the pool.
								// If resetting the object failed, destroy.
								//
								if ( ! objectToReturn.ResetState() )
									{
										if ( this.DiagnosticsActive )	this._Diag.Value.UpResetFailedCount();

										DestroyObject( objectToReturn );
										return;
									}
								//.........................................
								// re-registering for finalization - in case of resurrection (called from Finalize method)
								//
								if ( reRegisterForFinalization )
									{
										GC.ReRegisterForFinalize( objectToReturn );
									}
								//.........................................
								if ( this.DiagnosticsActive )	this._Diag.Value.UpReturnedCount();
								//.........................................
								this._Pool.Add( (T) objectToReturn );
							}
						else
							{
								//The Pool's upper limit has exceeded.
								// No need to add this object back into the pool, destroy it.
								//
								if ( this.DiagnosticsActive )	this._Diag.Value.UpOverflowCount();
								this.DestroyObject( objectToReturn );
							}
					}

			#endregion

			//===========================================================================================
			#region "De-Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// The pool is going down, release resources for all objects
				//
				~ObjectPool()
					{
						foreach ( T lo_Obj in this._Pool )
							{
								this.DestroyObject( lo_Obj );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private T CreateObject()
					{
						T newObject;

						if ( this.Factory != null )
							{
								newObject = this.Factory();
							}
						else
							{
								// Throws an exception if the type doesn't have default ctor - on purpose!
								// Could've added generic constraint with new ()
								// Didn't want to limit the user and force a parameterless c'tor
								//
								newObject = (T)	Activator.CreateInstance(	typeof(T) );
							}

						// Setting the 'return' action in the newly created pooled object
						//
						newObject.ReturnToPool = ( Action< PooledObject , bool > )	this._ReturnAction;
						//...............................................
						if ( this.DiagnosticsActive )	this._Diag.Value.UpCreatedCount();
						//...............................................

						this._Ready	= true;			// TEMP MOVE

						return newObject;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Make sure that the object is only disposed once
				// In case of application shutting down, we don't control the order of the finalization
				//
				private void DestroyObject(	PooledObject objectToDestroy )
				{
					if ( ! objectToDestroy.Disposed )
						{
							// Deterministically release object resources, nevermind the result, we are destroying the object
							//
							objectToDestroy.ReleaseResources();
							objectToDestroy.Disposed = true;

							if ( this.DiagnosticsActive )	this._Diag.Value.UpDestroyedCount();
						}

						// The object is being destroyed, resources have been already released deterministically
						// No need to fire finalizer
						//
						GC.SuppressFinalize( objectToDestroy );
				}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetLimits()
					{
						this._MinPoolSize	= this._MinPoolSize < 0									? 0 : this._MinPoolSize	;
						this._MaxPoolSize = this._MaxPoolSize < 1									? 1 : this._MaxPoolSize	;
						this._MinPoolSize	= this._MinPoolSize > this._MaxPoolSize	? 1 : this._MinPoolSize ;
					}

			#endregion

		}
}


