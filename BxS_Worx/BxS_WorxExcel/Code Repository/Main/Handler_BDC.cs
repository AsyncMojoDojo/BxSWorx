﻿using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.Main;
using BxS_WorxUTL.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class Handler_BDC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_BDC()
					{
						this._IPXBDCParser		=	new	Lazy< IExcelBDCSession_Parser >		(	()=>	Globals.ThisAddIn._IPXCntlr.Value.CreateBDCSessionParser	()	, cz_LM );
						this._UTLSerialiser		=	new	Lazy< Serializer >								(	()=>	this._UTLCntlr.CreateSerializer()	, cz_LM );
						this._UTLIO						=	new	Lazy< IO >												(	()=>	Globals.ThisAddIn._UTLCntlr.Value.CreateIO								()	, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IIPXController	_IPXCntlr		{ get	{	return	Globals.ThisAddIn._IPXCntlr.Value	; } }
				private	IUtl_Controller	_UTLCntlr		{ get	{	return	Globals.ThisAddIn._UTLCntlr.Value	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	const LazyThreadSafetyMode	cz_LM		= LazyThreadSafetyMode.ExecutionAndPublication;

				internal readonly Lazy< IExcelBDCSession_Parser >		_IPXBDCParser		;
				internal readonly Lazy< Serializer >								_UTLSerialiser	;
				internal readonly Lazy< IO >												_UTLIO					;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteDataXML( IExcelBDCSessionWS DTO )
					{
						IExcelBDCSessionRequest lo_Req	= Globals.ThisAddIn._IPXCntlr.Value.CreateBDCSessionRequest();
						this._IPXBDCParser.Value.ParseWStoRequest(DTO,lo_Req);
						var lt = new List<Type>	{	typeof(ExcelBDCSessionRequest) };
						string lc_XML		=	this._UTLSerialiser.Value.Serialize( lo_Req , lt );
						this._UTLIO.Value.WriteFile( $@"C:\ProgramData\BxS_Worx\{DTO.WSID}.xml" , lc_XML );
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal DTO_BDCSessionRequest CreateBDCSessionRequest()
				//	{
				//		return	this._IPXCntlr.Value.CreateBDCSessionRequest();
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal DTO_BDCSessionResult CreateBDCSessionResult()
				//	{
				//		return	this._IPXCntlr.Value.CreateBDCSessionResult();
				//	}

			#endregion

			////===========================================================================================
			//#region "Methods: Private"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private void Parse2Dto1D( DTO_BDCSessionRequest DTO, bool resetSource = true )
			//		{
			//			DTO.RowLB	= DTO.WSCells.GetLowerBound(0);
			//			DTO.RowUB	= DTO.WSCells.GetUpperBound(0);
			//			DTO.ColLB	= DTO.WSCells.GetLowerBound(1);
			//			DTO.ColUB	= DTO.WSCells.GetUpperBound(1);

			//			DTO.WSData1D.Clear();
			//			//.............................................
			//			for (int r = DTO.RowLB; r <= DTO.RowUB; r++)
			//				{
			//					for (int c = DTO.ColLB; c <= DTO.ColUB; c++)
			//						{
			//							if ( DTO.WSCells[r,c] != null )
			//								{
			//									string lc_Key	= $"{r.ToString()},{c.ToString()}";

			//									DTO.WSData1D.Add( lc_Key , DTO.WSCells[r,c].ToString() );
			//								}
			//						}
			//				}
			//			//.............................................
			//			if ( resetSource )	DTO.WSCells = null;
			//		}

			//#endregion

		}
}
