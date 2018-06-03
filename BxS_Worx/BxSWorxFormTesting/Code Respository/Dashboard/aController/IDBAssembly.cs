﻿using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IDBAssembly
		{
			#region "Properties"

				IDBViewConfig	FormConfig { get; }
				//...
				IList<IToolBarConfig>	ToolBarList	{ get; }
				IList<IButtonProfile>	ButtonList	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

				void Load();

			#endregion

		}
}
