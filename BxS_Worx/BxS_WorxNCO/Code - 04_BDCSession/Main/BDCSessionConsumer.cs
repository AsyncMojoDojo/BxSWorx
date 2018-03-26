﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSessionConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal BDCSessionConsumer(	BDCCall_Profile		profile
				internal BDCSessionConsumer(	BDCCall_Function	function
																		,	BDCCall_Lines			bdcData		)
					{
						//this._Profile		= profile		;
						this._Func			= function	;
						this._BDCData		= bdcData		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private	readonly	BDCCall_Profile		_Profile;
				private readonly	BDCCall_Function	_Func;
				private readonly	BDCCall_Lines			_BDCData;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TransactionsProcessed		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue )
					{
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								this._BDCData.Reset();
								//.........................................
								this._BDCData.LoadSPA( lo_Tran.SPAData );
								this._BDCData.LoadBDC( lo_Tran.BDCData );
								//.........................................
								try
									{
										this._Func.Process( this._BDCData );
										this._BDCData.PostProcess();
									}
								catch (System.Exception)
									{
									throw;
									}
								finally
									{
										this.TransactionsProcessed	++;
									}
							}
					}

			#endregion

			////===========================================================================================
			//#region "Methods: Private"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private void LoadSPA(	SMC.IRfcTable SPATable , IList< DTO_BDC_SPA > SPASrce )
			//		{
			//			SPATable.Append( SPASrce.Count );

			//			for ( int i = 0; i < SPASrce.Count; i++ )
			//				{
			//					SPATable.CurrentIndex	= i;
			//					//.........................................
			//					SPATable.SetValue( this._Profile.SPADat_MID , SPASrce[i].MemoryID		);
			//					SPATable.SetValue( this._Profile.SPADat_Val , SPASrce[i].MemoryValue	);
			//				}
			//		}

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private void LoadBDC(	SMC.IRfcTable BDCTable , IList< DTO_BDC_Data > BDCSrce )
			//		{
			//			BDCTable.Append( BDCSrce.Count );

			//			for ( int i = 0; i < BDCSrce.Count; i++ )
			//				{
			//					BDCTable.CurrentIndex	= i;
			//					//.........................................
			//					BDCTable.SetValue( this._Profile.BDCDat_Prg , BDCSrce[i].ProgramName	);
			//					BDCTable.SetValue( this._Profile.BDCDat_Dyn , BDCSrce[i].Dynpro				);
			//					BDCTable.SetValue( this._Profile.BDCDat_Bgn , BDCSrce[i].Begin				);
			//					BDCTable.SetValue( this._Profile.BDCDat_Fld , BDCSrce[i].FieldName		);
			//					BDCTable.SetValue( this._Profile.BDCDat_Val , BDCSrce[i].FieldValue		);
			//				}
			//		}

			//#endregion

		}
}
