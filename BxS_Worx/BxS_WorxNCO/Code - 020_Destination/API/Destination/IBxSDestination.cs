﻿using System;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IBxSDestination
		{
			#region "Properties"

				Guid	MyID			{ get; }
				Guid	SAPGUIID	{ get; }
				//.................................................
				SMC.RfcDestination	SMCDestination	{ get; }
				SMC.RfcRepository		SMCRepository		{ get; }
				//.................................................
				bool	IsConnected		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				SMC.RfcConfigParameters		CreateNCOConfig();
				//...
				IConfigLogon				CreateLogonConfig( bool ForRepository = false );
				//...
				IConfigRepository		CreateRepositoryConfig()	;
				IConfigDestination	CreateDestinationConfig()	;
				//.................................................
				void	LoadConfig	( SMC.RfcConfigParameters	config );
				void	LoadConfig	( IConfigLogon	config );
				void	LoadConfig	( IConfigBase		config );
				//.................................................
				void	RegisterRfcFunctionForMetadata( string fncName );
				//.................................................
				SMC.RfcFunctionMetadata	FetchFunctionMetadata	( string	fncName );
				Task										FetchMetadataAsync		( bool		optimiseMetadataFetch = true );

			#endregion

		}
}