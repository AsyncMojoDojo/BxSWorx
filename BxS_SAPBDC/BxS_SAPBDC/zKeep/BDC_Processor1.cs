﻿using System;
using System.Collections.Generic;
//.........................................................
using BxS_SAPIPC.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor1
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor1(DTO_BDCHeaderRowRef		bdcHeaderRowRef	)
					{
						this.BDCHeaderRowRef	= bdcHeaderRowRef		?? throw new Exception();
						//.............................................
						this.Tokens		= new Dictionary< string	, DTO_TokenReference >	();
						this.Columns	= new Dictionary< int			, DTO_BDCColumn >				();
						//.............................................
						this.SetupProcessBoundaries();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string[,]			Data		{ get; }
				internal	BDC_Processor		Session	{ get; }
				//.................................................
				internal	Dictionary<	string	, DTO_TokenReference >	Tokens						{ get; }
				internal	Dictionary<	int			, DTO_BDCColumn >				Columns						{ get; }

				internal	DTO_BDCHeaderRowRef		BDCHeaderRowRef		{ get; }
				internal	DTO_BDCXMLConfig			XMLConfig					{ get; set; }
				//.................................................
				internal	int		RowLB		{ get; private set; }
				internal	int		RowUB		{ get; private set; }
				internal	int		ColLB		{ get; private set; }
				internal	int		ColUB		{ get; private set; }

				internal	int		ColDataStart	{ get; set; }
				internal	int		ColDataEnd		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IPC_BDCSession Process( string[,] data )
					{
						IPC_BDCSession	lo_IPCSession	= IPC_Controller.CreateSession();
						var							lo_BDCSession	= new DTO_BDCSession();
						//.............................................



						//.............................................
						return	lo_IPCSession;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTranData CreateBDCData()
					{
						return	new DTO_SessionTranData();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Transaction CreateTransaction( Guid ID = default(Guid) )
					{
						return	new BDC_Transaction( ID );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupProcessBoundaries()
					{
						this.RowLB		= this.Data.GetLowerBound(0);
						this.RowUB		= this.Data.GetUpperBound(0)	+ 1;
						//.............................................
						this.ColLB		= this.Data.GetLowerBound(1);
						this.ColUB		= this.Data.GetUpperBound(1)	+ 1;
					}

			#endregion

		}
}
