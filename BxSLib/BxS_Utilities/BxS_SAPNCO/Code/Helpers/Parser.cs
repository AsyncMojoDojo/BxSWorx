﻿using System.Collections.Generic;
//.........................................................
using BxS_SAPConn.API;
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helper
{
	internal class Parser
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcConfigParameters Parse(IDTOConnParameters DTO)
					{
						var lo_RfcCnfParms	= new SMC.RfcConfigParameters();
						//.............................................
						foreach (KeyValuePair<string, string> ls_kvp in DTO.Parameters)
							{
								lo_RfcCnfParms[ls_kvp.Key] = ls_kvp.Value;
							}
						//.............................................
						return	lo_RfcCnfParms;
					}

			#endregion

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal void	LoadParameters(string ID, IDTOConnParameters DTO)
				//	{
				//		foreach (KeyValuePair<string, string> ls_kvp in this.GetConfig(ID))
				//			{
				//				DTO.Parameters.Add(ls_kvp.Key, ls_kvp.Value);
				//			}
				//	}

		}
}
