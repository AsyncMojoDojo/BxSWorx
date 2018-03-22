﻿using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Main.API
{
	public interface IController
		{
			#region "Properties"

				int LoadedSystemCount	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				//.................................................
				IRfcDestination	GetDestination( Guid ID		);
				IRfcDestination	GetDestination( string ID );
				//.................................................
				void LoadGlobalConfig( IConfigSetupGlobal config );
				//.................................................
				IConfigSetupDestination		CreateDestinationConfig()	;
				IConfigSetupGlobal				CreateGlobalConfig()			;
				//.................................................
				void Reset();

			#endregion

		}
}