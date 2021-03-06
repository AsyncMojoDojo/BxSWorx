﻿using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main
{
	internal class Repository
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository( Func< Guid , IBxSDestination >	createDestination  )
					{
						this._CreateDestFunc	= createDestination;
						//.............................................
						this.SAPSystems	= new Dictionary< Guid		, string >									()	;
						this._Map				= new Dictionary< string	, Guid >										()	;
						this._Des				= new Dictionary< Guid		, SMC.RfcConfigParameters >	()	;
						//.............................................
						this._SAPINI		= new Lazy< SAPINI >( ()=> SAPINI.Instance 	, cz_LM	);
						//.............................................
						this._Lock	= new	object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< SAPINI >	_SAPINI;
				//.................................................
				private readonly	Dictionary< string	,	Guid >										_Map;
				private readonly	Dictionary< Guid		, SMC.RfcConfigParameters >	_Des;
				//.................................................
				private	readonly	Func< Guid , IBxSDestination >									_CreateDestFunc;
				//.................................................
				private readonly	object	_Lock;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Dictionary< Guid	,	string > SAPSystems { get; }

				internal int	Count	{ get	{ return	this.SAPSystems.Count; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.SAPSystems.Clear();
						this._Map.Clear();
						this._Map.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List as per SAP Logon GUI setup
				//
				public IList<string> GetSAPINIList()
					{
						return this._SAPINI.Value.GetSAPINIList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBxSDestination GetDestination( string ID , bool useSAPINI = true )
					{
						Guid lg = this.GetAddIDFor( ID , true );
						//.............................................
						return	this.GetDestination( lg , useSAPINI );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBxSDestination GetDestination( Guid ID , bool useSAPINI = true )
					{
						if ( !this._Des.TryGetValue( ID , out SMC.RfcConfigParameters lo_Cnf ) )
							{
								lock ( this._Lock )
									{
										if ( !this._Des.TryGetValue(ID, out lo_Cnf)	)
											{
												if ( this.SAPSystems.TryGetValue( ID , out string lc_SAPID ) )
													{
														if ( useSAPINI )
															{
																lo_Cnf	= this._SAPINI.Value.GetIniParameters( lc_SAPID )	?? new SMC.RfcConfigParameters();
															}
														else
															{
															}
														//...
														this._Des.Add( ID , lo_Cnf );
													}
												else
													{
														lo_Cnf	= null;
													}
											}
									}
								}
						//.............................................
						IBxSDestination	lo_Des = this._CreateDestFunc( ID );
						lo_Des.LoadConfig( lo_Cnf );
						//.............................................
						return lo_Des;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string GetNameFor( Guid ID )
					{
						return	this.SAPSystems[ID] ?? string.Empty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Guid GetAddIDFor(		string	ID
																	, bool		Add = false)
					{
						if (ID == null)	return	Guid.Empty;
						//.............................................
						if (!this._Map.TryGetValue(ID, out Guid lg_Guid))
							{
								if (Add)
									{
										lock( this._Lock )
											{
												if (!this._Map.TryGetValue(ID, out lg_Guid))
													{
														lg_Guid	= Guid.NewGuid();
														this._Map[ID]							= lg_Guid;
														this.SAPSystems[lg_Guid]	= ID;
													}
											}
									}
								else
									{
										lg_Guid	= Guid.NewGuid();
									}
							}
						//.............................................
						return	lg_Guid;
					}

			#endregion

		}
}
