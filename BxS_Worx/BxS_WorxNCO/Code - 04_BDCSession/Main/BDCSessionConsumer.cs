﻿using System;
using System.Threading;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSessionConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionConsumer( BDCCall_Function	function )
					{
						this._Func		= function	;
						//.............................................
						this._BDCData	= new	Lazy<BDCCall_Lines>	(	()=>	this._Func.MyProfile.Value.CreateBDCCallLines()	, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDCCall_Function			_Func;
				private	readonly	Lazy< BDCCall_Lines >	_BDCData;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TransactionsProcessed		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue
															,	SMC.RfcCustomDestination									rfcDestination	)
					{
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								this._BDCData.Value.Reset();
								//.........................................
								this._BDCData.Value.LoadSPA( lo_Tran.SPAData );
								this._BDCData.Value.LoadBDC( lo_Tran.BDCData );
								//.........................................
								try
									{
										this._Func.Process(		this._BDCData.Value
																				, rfcDestination			);

										this._BDCData.Value.PostProcess();
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

		}
}
