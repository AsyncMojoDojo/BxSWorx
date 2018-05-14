﻿using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	public interface IMvC
		{
			#region "Properties"

				string	ID	{ get; }

			#endregion

				event	Action Closing;

			//===========================================================================================
			#region "Methods: Exposed"

				void	Shutdown()		;
				void	ToggleView()	;

			#endregion

		}
}
