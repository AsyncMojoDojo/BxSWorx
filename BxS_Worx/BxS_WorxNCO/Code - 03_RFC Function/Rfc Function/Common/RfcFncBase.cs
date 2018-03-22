﻿using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncBase : IRfcFncBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncBase( IRfcFncProfile profile	)
					{
						this.Profile	= profile;
						//.............................................
						this.MyID	= Guid.NewGuid();

						this._NCORfcFunction	= new Lazy< SMC.IRfcFunction >
																					(		()=>	this.Profile.CreateRfcFunction()
																						, LazyThreadSafetyMode.ExecutionAndPublication );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< SMC.IRfcFunction > _NCORfcFunction;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	MyID	{	get; }
				//.................................................
				public	IRfcFncProfile		Profile					{ get; }
				public	SMC.IRfcFunction	NCORfcFunction	{ get { return	this._NCORfcFunction.Value	; } }
				//.................................................
				private	SMC.RfcCustomDestination	NCODestination	{ get { return	this.Profile.NCODestination	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke()
					{
						bool	lb_Ret	= false;
						//.............................................
						try
							{
								this.Profile.ReadyProfile();
								this._NCORfcFunction.Value.Invoke( this.NCODestination );
								lb_Ret	= true;
							}
						catch ( Exception ex )
							{
								throw new System.Exception( "NCO invoke error" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}