﻿using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	internal class BDCData
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTransaction(	IRFCFunction	RfcFunction)
					{
						this._RFCFunction	= RfcFunction;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly IRFCFunction	_RFCFunction;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string							Name						{ get;			}
				public	SMC.IRfcFunction		RfcFunction			{ get; set; }
				public	SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke()
					{
						bool	lb_Ret	= true;
						//.............................................
						try
							{
								this.RfcFunction.Invoke(this.RfcDestination);
							}
						catch (System.Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}


//BDCDATA
//Batch input: New table field structure
//PROGRAM	1 Types	BDC_PROG	CHAR	40	0	BDC module pool
//DYNPRO	1 Types	BDC_DYNR	NUMC	4	0	BDC Dynpro Number
//DYNBEGIN	1 Types	BDC_START	CHAR	1	0	BDC Dynpro Start
//FNAM	1 Types	FNAM_____4	CHAR	132	0	Field name
//FVAL	1 Types	BDC_FVAL	CHAR	132	0	BDC field value


//CTU_PARAMS
//	Parameter string for runtime of CALL TRANSACTION USING...
//DISMODE	1 Types	CTU_MODE	CHAR	1	0	Processing mode for CALL TRANSACTION USING...
//UPDMODE	1 Types	CTU_UPDATE	CHAR	1	0	Update mode for CALL TRANSACTION USING...
//CATTMODE	1 Types	CTU_CATT	CHAR	1	0	CATT mode for CALL TRANSACTION USING...
//DEFSIZE	1 Types	CTU_DEFSZE	CHAR	1	0	Default screen size for CALL TRANSACTION USING...
//RACOMMIT	1 Types	CTU_RAFC	CHAR	1	0	CALL TRANSACTION USING... is not completed by COMMIT
//NOBINPT	1 Types	CTU_NOBIM	CHAR	1	0	SY-BINPT=SPACE for CALL TRANSACTION USING...
//NOBIEND	1 Types	CTU_NOBEN	CHAR	1	0	SY-BINPT=SPACE after data end for CALL TRANSACTION USING...

//					 CTU_MODE              A	Display all screens
//																E	Display Errors
//																N	Background processing
//																P	Background processing; debugging possible
//	CTU_UPDATE	L	Local
//							S	Synchronous
//							A	Asynchronous

//	CTU_CATT		No CATT
//							N	CATT without individual screen control
//							A	CATT with individual screen control

//	CTU_DEFSZE		No
//							X	Yes

//	CTU_RAFC

//	CTU_NOBIM

//	CTU_NOBEN



//BDCMSGCOLL
//Collecting messages in the SAP System

//	TCODE	1 Types	BDC_TCODE	CHAR	20	0	BDC Transaction code
//DYNAME	1 Types	BDC_MODULE	CHAR	40	0	Batch input module name
//DYNUMB	1 Types	BDC_DYNNR	CHAR	4	0	Batch input screen number
//MSGTYP	1 Types	BDC_MART	CHAR	1	0	Batch input message type
//MSGSPRA	1 Types	BDC_SPRAS	LANG	1	0	Language ID of a message
//MSGID	1 Types	BDC_MID	CHAR	20	0	Batch input message ID
//MSGNR	1 Types	BDC_MNR	CHAR	3	0	Batch input message number
//MSGV1	1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
//MSGV2	1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
//MSGV3	1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
//MSGV4	1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
//ENV	1 Types	BDC_AKT	CHAR	4	0	Batch input monitoring activity
//FLDNAME	1 Types	FNAM_____4	CHAR	132	0	Field name



//RFC_SPAGPA
//SPA/GPA structure for RFC
//	PARID	1 Types	MEMORYID	CHAR	20	0	Set/Get parameter ID
//PARVAL	1 Types		CHAR	255	0	Set/Get Parameter Value (Char 255)



//	FUNCTION /isdfps/call_transaction.
//*"----------------------------------------------------------------------
//*"*"Lokale Schnittstelle:
//*"  IMPORTING
//*"     VALUE(IF_TCODE) TYPE  TCODE
//*"     VALUE(IF_SKIP_FIRST_SCREEN) TYPE  FLAG DEFAULT SPACE
//*"     VALUE(IT_BDCDATA) TYPE  BDCDATA_TAB OPTIONAL
//*"     VALUE(IS_OPTIONS) TYPE  CTU_PARAMS OPTIONAL
//*"  EXPORTING
//*"     VALUE(ET_MSG) TYPE  ETTCD_MSG_TABTYPE
//*"  TABLES
//*"      CT_SETGET_PARAMETER STRUCTURE  RFC_SPAGPA OPTIONAL
//*"  EXCEPTIONS
//*"      IMPORT_PARA_ERROR
//*"      TCODE_ERROR
//*"      AUTH_ERROR
//*"      TRANS_ERROR

