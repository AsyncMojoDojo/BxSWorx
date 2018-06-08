﻿using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal interface IUC_TBarModel
		{
				IUC_TBarSetup	Setup	{ get;	set; }

			#region "Methods: Exposed"

				IList<IUC_Button>	ScenarioButtons( string scenarioID )	;
				//...
				void				LoadButton( string scenarioID , IUC_Button	button	 ) ;
				IUC_Button	GetButton	( string scenarioID , string			buttonID ) ;

			#endregion

		}
}
