﻿using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API.Destination
{
	public interface ISAPSystemReference
		{
			#region "Properties"

				Guid		ID			{ get; set; }
				string	SAPName	{ get; set; }

			#endregion

		}
}
