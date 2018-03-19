﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncManager : IRfcFncManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncManager( IRfcDestination rfcDestination )
					{
						this._RfcDestination	= rfcDestination	?? throw new ArgumentException("IRfcDestination is null");
						//.............................................
						this.UseRoundtrip			= true;
						this._RfcFncProfiles	= new ConcurrentDictionary< string , IRfcFncProfile >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IRfcDestination																		_RfcDestination;
				private readonly	ConcurrentDictionary< string , IRfcFncProfile >		_RfcFncProfiles;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	UseRoundtrip	{	get;	set; }
				//.................................................
				public	SMC.RfcRepository	NCORepository	{ get { return this._RfcDestination.NCORepository; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterProfile( IRfcFncProfile rfcFncProfile , bool loadMetadata = false )
					{
						if ( this._RfcFncProfiles.TryAdd( rfcFncProfile.FunctionName , rfcFncProfile ) )
							{
								this._RfcDestination.RegisterRfcFunctionForMetadata( rfcFncProfile.FunctionName , loadMetadata );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ProfileExists( string rfcFncName )
					{
						return	this._RfcFncProfiles.ContainsKey( rfcFncName );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ProfileType GetProfile<ProfileType>( string rfcFncName )
					{
						this._RfcFncProfiles.TryGetValue(	rfcFncName	, out IRfcFncProfile lo_Prof );
						return	(ProfileType) lo_Prof;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool UpdateProfiles()
					{
						bool lb_Ret	= true;
						//...............................................
						foreach ( KeyValuePair< string , IRfcFncProfile > lo_Prof in this._RfcFncProfiles )
							{
								if ( ! this._RfcDestination.LoadRfcFunctionProfileMetadata( lo_Prof.Value ) )
									{
										lb_Ret	= false;
									}
							}
						//...............................................
						return	lb_Ret;
					}

			#endregion

		}
}
