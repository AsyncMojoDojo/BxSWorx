using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.CTU;
//�������������������������������������������������������������������������������������������������
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_900_BDC_Helpers
		{
			#region "Declarations"

				private NCOController_BDC	lo_Cntlr;

			#endregion

			//...................................................
			public UT_900_BDC_Helpers()
				{
					this.lo_Cntlr		= new NCOController_BDC();
				}

			//...................................................
			[TestMethod]
			public void UT_900_10_BDCOpFnc()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					BDCOpFnc x = this.lo_Cntlr.OpFnc;
					BDCOpFnc y = this.lo_Cntlr.OpFnc;

					Assert.IsNotNull(	x			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsNotNull(	y			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.AreSame	(	y	,	x	, $"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					DTO_SessionHeader f	= this.lo_Cntlr.OpFnc.SessionHeader();
					Assert.IsNotNull(	f			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_900_20_BDCOpEnv()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
				}
		}
}
