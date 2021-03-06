﻿using BxS_SAPIPX.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.BDC
{
	public class DTO_CTUParms
		{
			#region "Documentation"

				//	CTU_PARAMS:	Parameter string for runtime of CALL TRANSACTION USING...
				//
				//	DISMODE		1 Types	CTU_MODE		CHAR	1	0	Processing mode for CALL TRANSACTION USING...
				//	UPDMODE		1 Types	CTU_UPDATE	CHAR	1	0	Update mode for CALL TRANSACTION USING...
				//	CATTMODE	1 Types	CTU_CATT		CHAR	1	0	CATT mode for CALL TRANSACTION USING...
				//	DEFSIZE		1 Types	CTU_DEFSZE	CHAR	1	0	Default screen size for CALL TRANSACTION USING...
				//	RACOMMIT	1 Types	CTU_RAFC		CHAR	1	0	CALL TRANSACTION USING... is not completed by COMMIT
				//	NOBINPT		1 Types	CTU_NOBIM		CHAR	1	0	SY-BINPT=SPACE for CALL TRANSACTION USING...
				//	NOBIEND		1 Types	CTU_NOBEN		CHAR	1	0	SY-BINPT=SPACE after data end for CALL TRANSACTION USING...

				//	CTU_MODE:	A	Display all screens
				//						E	Display Errors
				//						N	Background processing
				//						P	Background processing; debugging possible

				//	CTU_UPDATE:	L	Local
				//							S	Synchronous
				//							A	Asynchronous

				//	CTU_CATT:	b	No CATT
				//						N	CATT without	individual screen control
				//						A	CATT with			individual screen control

				//	CTU_DEFSZE/CTU_RAFC/CTU_NOBIM/CTU_NOBEN:	b	No
				//																						X	Yes

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_CTUParms()
					{
						this.SetToDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	char	DisplayMode			{	get; set; }
				public	char	UpdateMode			{	get; set; }
				public	char	CATTMode				{	get; set; }
				public	char	DefaultSize			{	get; set; }
				public	char	NoCommit				{	get; set; }
				public	char	NoBatchInpFor		{	get; set; }
				public	char	NoBatchInpAft		{	get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SetToDefaults()
					{
						this.DisplayMode		=	BDC_Constants.lz_CTU_N	;
						this.UpdateMode			=	BDC_Constants.lz_CTU_A	;
						this.CATTMode				=	BDC_Constants.lz_CTU_F	;
						this.DefaultSize		=	BDC_Constants.lz_CTU_F	;
						this.NoCommit				=	BDC_Constants.lz_CTU_F	;
						this.NoBatchInpFor	=	BDC_Constants.lz_CTU_F	;
						this.NoBatchInpAft	=	BDC_Constants.lz_CTU_F	;
					}

			#endregion

		}
}
