﻿using BxS_Worx.Dashboard.UI.Button;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public sealed class DBAssemblyExcel	: DBAssemblyBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBAssemblyExcel()	: base()
					{	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDBAssembly	Create()	=>	new	DBAssemblyExcel();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void	LoadFromSource()
					{
						base.LoadFromSource();
						//...
						this.LoadToolbarsFromSource()	;
						this.LoadButtonsFromSource()	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadButtonsFromSource()
					{
						IButtonTag lo_Tag1	=	DB_Factory.CreateButtonTag( "TB2"	, "SC2"	);

						IButtonProfile	BP1	= DB_Factory.CreateButtonProfile(		"BP1"
																																	,	ButtonTypes.TypeStd
																																	, ImageNames.Settings
																																	, "Settings"
																																	,	lo_Tag1							)	;

						BP1.SeqNo						= 01		;
						BP1.ToolbarID				=	"TB1"	;
						BP1.ScenarioID			= "SC1"	;
						//BP1.OnClickHandler	= this.OnButtonClick_RouteScenario	;

						this.LoadButton( BP1 ) ;
						//...
						IButtonTag lo_Tag2	=	DB_Factory.CreateButtonTag( "TB2"	, "SC2"	);

						IButtonProfile	BP2	=	DB_Factory.CreateButtonProfile(		"BP2"
																																	,	ButtonTypes.TypeFlp
																																	, ImageNames.Logo
																																	, "Settings"
																																	,	lo_Tag2							)	;

						BP2.SeqNo						= 03		;
						BP2.ToolbarID				=	"TB1"	;
						BP2.ScenarioID			= "SC1"	;
						//BP2.OnClickHandler	= this.OnButtonClick_RouteScenario	;

						this.LoadButton( BP2 ) ;
						//...
						IButtonTag lo_Tag3	=	DB_Factory.CreateButtonTag( "TB2"	, "SC2"	);

						IButtonProfile	BP3	=	DB_Factory.CreateButtonProfile(		"BP3"
																																	,	ButtonTypes.TypeFlp
																																	, ImageNames.Excel
																																	, "Settings"
																																	,	lo_Tag3							)	;

						BP3.SeqNo						= 02		;
						BP3.ToolbarID				=	"TB1"	;
						BP3.ScenarioID			= "SC1"	;
						//BP3.OnClickHandler	= this.OnButtonClick_RouteScenario	;

						this.LoadButton( BP3 ) ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolbarsFromSource()
					{
						IUC_TBarSetup	TB1		=	DB_Factory.CreateTBSetup();		TB1.ID	= "TB1";	TB1.SeqNo	= 1	;	TB1.IsHorizontal	= true	;

						TB1.IsStartupToolBar	= true	;
						TB1.StartupScenario		=	"SC1"	;
						TB1.ButtonType				= ButtonTypes.TypeStd	;

						TB1.ViewConfig.ColourBack				= System.Drawing.Color.Plum	;
						TB1.ViewConfig.TransitionSpeed	=	0	;

						this.LoadToolbar( TB1 )	;
						//...
						IUC_TBarSetup	TB2		= DB_Factory.CreateTBSetup();		TB2.ID	= "TB2";	TB2.SeqNo	= 2	;	TB2.IsHorizontal	= false	;

						TB2.ViewConfig.ColourBack					= System.Drawing.Color.Aquamarine	;
						TB2.ViewConfig.TransitionSpeed		=	10	;

						this.LoadToolbar( TB2 )	;
					}

			#endregion

		}
}
