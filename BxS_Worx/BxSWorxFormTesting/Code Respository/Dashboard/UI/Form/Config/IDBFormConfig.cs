﻿using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IDBFormConfig
		{
			#region "Properties"

				Color	ColourBack	{ get;  set; }
				Color	ColourMove	{ get;  set; }
				Color	ColourHead	{ get;  set; }

			#endregion

		}
}