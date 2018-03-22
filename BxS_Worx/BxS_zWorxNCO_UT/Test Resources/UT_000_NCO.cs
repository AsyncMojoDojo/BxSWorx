﻿using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	public class UT_000_NCO
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_000_NCO()
				{
					this.DestController	= new DestinationController();
				}

			internal IDestinationController DestController { get; }

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal string GetSAPID()
				{
					IList< string > lt_Ini	=	this.DestController.GetSAPINIList();
					string					lc_ID		= lt_Ini.FirstOrDefault( s => s.Contains("PWD)") );
					Assert.IsNotNull	( lc_ID	, "" );
					return	lc_ID;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal IRfcDestination GetSAPDest()
				{
					IRfcDestination lo_Dest = this.DestController.GetDestination( this.GetSAPID() );
					//...............................................
					Assert.IsNotNull	( lo_Dest	, "" );
					//...............................................
					return	lo_Dest;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal IRfcDestination GetSAPDestLoggedOn( bool DoLogonCheck = false )
				{
					IRfcDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					lo_Dest.Client			= "700"						;
					lo_Dest.User				= "DERRICKBINGH"	;
					lo_Dest.Password		= "M@@n4321"			;
					lo_Dest.LogonCheck	= DoLogonCheck		;
					lo_Dest.UseSAPGui		= "2";
					//...............................................
					Assert.IsTrue	(	lo_Dest.IsConnected	, "" )	;
					//...............................................
					return	lo_Dest	;
				}
		}
}