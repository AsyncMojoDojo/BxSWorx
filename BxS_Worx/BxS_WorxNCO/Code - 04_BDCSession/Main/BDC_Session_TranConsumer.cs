﻿using System;
using System.Threading;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;

using BxS_WorxUtil.ObjectPool;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session_TranConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_TranConsumer( BDC_Function	function )
					{
						this._Func	= function	;
						//.............................................
						this._BDCHead	= new	Lazy< BDC_Header >	(	()=>	this._Func.MyProfile.Value.CreateBDCHeader() , cz_LM );
						this._BDCData	= new	Lazy< BDC_Data >		(	()=>	this._Func.MyProfile.Value.CreateBDCData	() , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDC_Function	_Func;
						//.............................................
				private	readonly	Lazy< BDC_Header >	_BDCHead;
				private	readonly	Lazy< BDC_Data >		_BDCData;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TransactionsProcessed		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	DTO_BDC_Header														header
															,	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue
															,	SMC.RfcDestination												rfcDestination	)
					{
						this._BDCHead.Value.Load( header );
						//.............................................
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								try
									{
										this._BDCData.Value.Reset();
										//.........................................
										this._BDCData.Value.LoadSPA( lo_Tran.SPAData );
										this._BDCData.Value.LoadBDC( lo_Tran.BDCData );
										//.........................................
										this._Func.Config( this._BDCHead.Value );
										this._Func.Process( this._BDCData.Value , rfcDestination );
										//.........................................
										this._BDCData.Value.LoadMsg( lo_Tran.MSGData );
										this._BDCData.Value.PostProcess();
										//.........................................
										lo_Tran.Successful	= true;
									}
								catch
									{
										lo_Tran.Successful	= false;
									}
								finally
									{
										lo_Tran.Processed	= true;
										this.TransactionsProcessed	++;
									}
							}
					}

			#endregion

		}
}
