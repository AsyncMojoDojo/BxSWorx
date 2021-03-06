﻿using System.ComponentModel;
using System.Drawing				;
using System.Windows.Forms	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IUC_TBarSetup : INotifyPropertyChanged
		{
			#region "Properties"

				string		ID										{ get;  set; }
				int				SeqNo									{ get;  set; }
				//...
				bool			IsHorizontal					{ get;  set; }
				bool			ShowOnstartup					{ get;  set; }
				//...
				string		ButtonType						{ get;  set; }
				//...
				bool			IsStartupToolBar			{ get;  set; }
				bool			IsStartupSpanMax			{ get;  set; }
				string		StartupScenario				{ get;  set; }
				//...
				Color			ColourBack						{ get;	set; }
				Color			ColourFocus						{ get;	set; }
				//...
				int				TransitionSpanMin			{ get;  set; }
				int				TransitionSpanMax			{ get;  set; }
				int				TransitionSpeed				{ get;  set; }
				//...
				DockStyle	FocusDocking					{ get;  set; }

			#endregion

		}
}
