﻿using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main
{
	internal sealed class SAPDM
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Singleton
				//
				private	static readonly	Lazy<SAPDM>	_Instance		= new Lazy< SAPDM >( ()=>	new SAPDM() , cz_LM );

				internal static SAPDM Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private SAPDM()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				public	static	string	UseSAPGUINotUse		{ get { return	SMC.RfcConfigParameters.RfcUseSAPGui.NotUse	; } }
				public	static	string	UseSAPGUIUse			{ get { return	SMC.RfcConfigParameters.RfcUseSAPGui.Use		; } }
				public	static	string	UseSAPGUIHidden		{ get { return	SMC.RfcConfigParameters.RfcUseSAPGui.Hidden	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters		CreateNCOConfig()	=>	new	SMC.RfcConfigParameters	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcDestination					GetDestination				( string ID )	=>	SDM.GetDestination( ID );
				public SMC.RfcCustomDestination		GetCustomDestination	( string ID )	=>	SDM.GetDestination( ID ).CreateCustomDestination();
				//...
				public SMC.RfcDestination					GetDestination				( SMC.RfcConfigParameters rfcConfig )	=>	SDM.GetDestination( rfcConfig );
				public SMC.RfcCustomDestination		GetCustomDestination	( SMC.RfcConfigParameters rfcConfig )	=>	SDM.GetDestination( rfcConfig ).CreateCustomDestination();

			#endregion

		}
}