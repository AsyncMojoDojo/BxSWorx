﻿using System;
using System.Collections.Generic;
//.........................................................
using					BxS_SAPBDC.BDC;
using					BxS_SAPIPX.Excel;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Parser_Transaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Transaction(	Lazy< BDC_Parser_Factory > factory )
					{
						this._Factory	= factory;
						//.............................................
						this._FldIndex	= -1;
						this._CsrIndex	= -1;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Parser_Factory > 	_Factory;
				//.................................................
				private	int		_FldIndex	;
				private	int		_CsrIndex	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_BDCSessionRequest dtoRequest
															,	DTO_ParserProfile				dtoProfile
															, BDC_Session						Session			)
					{
						if (dtoRequest.WSData == null)	return;
						//.............................................
						foreach ( KeyValuePair< int , List< int > > ls_KvpRow in dtoProfile.TranRows )
							{
								BDC_SessionTransaction lo_BDCTran	= this._Factory.Value.CreateBDCSessionTransaction();
								//.........................................
								for ( int r = 0; r < ls_KvpRow.Value.Count; r++ )
									{
										foreach ( KeyValuePair< int , DTO_ParserColumn > ls_KvpCol in dtoProfile.Columns )
											{
												this.CompileBDCEntries(		lo_BDCTran
																								,	ls_KvpCol.Value
																								,	dtoRequest.WSData[ ls_KvpRow.Value[r] , ls_KvpCol.Key ] );
											}
									}
								//.........................................
								Session.AddTransaction( lo_BDCTran );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CompileBDCEntries(		BDC_SessionTransaction bdcTran
																				, DTO_ParserColumn		column
																				, string					value		)
					{
						if ( column.DoOnlyIfValue && value.Equals(string.Empty) )
								{
									return;
								}
						//.............................................
						if (		column.IsFieldIndexColumn
								||	column.IsCursorIndexColumn )
							{
								if ( int.TryParse( value, out int ln_Idx ) )
									{
										if ( column.IsCursorIndexColumn )
											{
												this._CsrIndex	= ln_Idx;
											}

										if ( column.IsFieldIndexColumn )
											{
												this._FldIndex	= ln_Idx;
											}
									}
								//.........................................
								return;
							}

						//.............................................
						// Process after all criteria met
						//.............................................
						if (column.DynBegin)
							{
								bdcTran.AddBDCData( this.CompileBDCScreen( column.Program	, column.ScreenNo ) );
							}
						//.............................................
						if ( !column.OKCode.Equals( string.Empty ) )
							{
								bdcTran.AddBDCData(	this.CompileBDCField(	cz_SAP_OKCode	, column.OKCode	) );
							}
						//.............................................
						if ( !column.Cursor.Equals( string.Empty ) )
							{
								if ( column.DoCursorIndex && this._CsrIndex.Equals(-1) )
									{ }
								else
									{
										string lc_CsrFld;

										if ( column.DoCursorIndex )
											{
												lc_CsrFld	=	column.Cursor.Replace( cz_Sub_Token	,	this._FldIndex.ToString()	);
											}
										else
											{
												lc_CsrFld	= column.Cursor;
											}

										bdcTran.AddBDCData( this.CompileBDCField(	cz_SAP_Cursor	, lc_CsrFld	) );
									}
							}
						//.............................................
						if ( !column.Subscreen.Equals(string.Empty) )
							{
								bdcTran.AddBDCData( this.CompileBDCField(	cz_SAP_SubScr	, column.Subscreen ) );
							}
						//.............................................
						if ( !value.Equals( cz_Sym_ActionCol ) )
							{
								if ( column.DoFieldIndex && this._FldIndex.Equals(-1) )
									{ }
								else
									{
										string lc_Fld;

										if ( column.DoFieldIndex )
											{
												lc_Fld	=	column.Field.Replace(	cz_Sub_Token , this._FldIndex.ToString() );
											}
										else
											{
												lc_Fld	= column.Field;
											}

										if ( value.Equals( cz_Sym_ClearFld ) )
											{
												bdcTran.AddBDCData( this.CompileBDCField(	lc_Fld , string.Empty	) );
											}
										else
											{
												bdcTran.AddBDCData( this.CompileBDCField( lc_Fld , value ) );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCScreen( string program , int screenNo )
					{
						DTO_SessionTranData lo_BDCData	= this._Factory.Value.CreateDTOSessionTranData();
						//.............................................
						lo_BDCData.ProgramName	= program;
						lo_BDCData.Dynpro				= screenNo.ToString("0000");
						lo_BDCData.Begin				= "X";
						//.............................................
						return	lo_BDCData;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCField( string field , string value )
					{
						DTO_SessionTranData lo_BDCData	= this._Factory.Value.CreateDTOSessionTranData();
						//.............................................
						lo_BDCData.FieldName	= field;
						lo_BDCData.FieldValue	=	value;
						//.............................................
						return	lo_BDCData;
					}

			#endregion

		}
}
