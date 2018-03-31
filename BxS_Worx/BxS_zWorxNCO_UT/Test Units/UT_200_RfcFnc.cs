﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_200_RfcFnc
		{
			private	const string cz_FNme	= "/ISDFPS/CALL_TRANSACTION";
			private readonly	UT_000_NCO	co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_200_RfcFnc()
				{
					this.co_NCO			= new	UT_000_NCO();
					//...............................................
					Assert.IsNotNull	( this.co_NCO									, "" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_200_RfcFnc_10_Instantiate()
				{
					IRfcDestination	lo_D	= this.co_NCO.GetSAPDest();
					IRfcFncManager	lo_M	= new RfcFncManager( lo_D );

					Assert.IsNotNull	( lo_D , "" );
					Assert.IsNotNull	( lo_M , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_200_RfcFnc_20_MetaData()
				{
					IRfcDestination	lo_DS	= this.co_NCO.GetSAPDestLoggedOn();
					IRfcFncManager	lo_FM = new RfcFncManager( lo_DS );
					//...............................................
					BDCCall_Profile lo_PR0	= this.CreateBDCCallProfile();
					lo_FM.RegisterProfile	( lo_PR0 );
					//...............................................
					BDCCall_Profile lo_PR1	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );
					BDCCall_Profile lo_PR2	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );

					Assert.AreEqual	( lo_PR1 , lo_PR2 ,	"" );
					//...............................................
					Assert.AreEqual	(	0	, lo_PR1.FNCIndex.TabSPA	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.CTUIndex.NoBtcE	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.SPAIndex.Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.BDCIndex.Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.MSGIndex.Fldnm		,	"" );

					lo_FM.UpdateProfiles();

					Assert.AreNotEqual	(	0	, lo_PR1.FNCIndex.TabSPA	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.CTUIndex.NoBtcE	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.SPAIndex.Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.BDCIndex.Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.MSGIndex.Fldnm		,	"" );
					//...............................................
					var lo_FN1	= new MyRfcFnc( lo_PR1 );
					var lo_FN2	= new MyRfcFnc( lo_PR2 );

					//lo_DS.LoadRfcFunction( lo_FN1 );
					//lo_DS.LoadRfcFunction( lo_FN2 );

					Assert.IsNotNull	( lo_FN1.NCORfcFunction , "" );
					Assert.IsNotNull	( lo_FN2.NCORfcFunction , "" );

					Assert.AreNotEqual	( lo_FN1.NCORfcFunction	, lo_FN2.NCORfcFunction	,	"" );

					SMC.IRfcStructure x1 = lo_FN1.NCORfcFunction.GetStructure("IS_OPTIONS");
					SMC.IRfcStructure x2 = lo_FN1.NCORfcFunction.GetStructure(lo_PR2.FNCIndex.CTUOpt);

					Assert.AreNotEqual	(	0	, x1.Count	,	"" );
					Assert.AreNotEqual	(	0	, x2.Count	,	"" );
			}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCCall_Profile CreateBDCCallProfile()
					{
						IRfcDestination	lo_DS	= this.co_NCO.GetSAPDestLoggedOn();
						BDCCall_Factory lo_FC	= BDCCall_Factory.Instance;

						return	new BDCCall_Profile(	cz_FNme
																				, lo_FC	);
					}

		//

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		private class MyRfcFnc : RfcFncBase
			{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MyRfcFnc( BDCCall_Profile profile ) : base( profile )
					{
					}
			}

		//

		}
}
