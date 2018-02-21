﻿using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPC.BDCData
{
	public class DTO_ExcelWorksheet
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ExcelWorksheet()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	WBID					{ get;	set;	}
				public	string	WSID					{ get;	set;	}
				public	string	WSNo					{ get;	set;	}
				public	string	UsedAddress		{ get;	set;	}
				//.................................................
				public	string[,]	WSData			{ get; set;	}
				public	object[,]	WSCells			{ get; set;	}

			#endregion

		}
}
