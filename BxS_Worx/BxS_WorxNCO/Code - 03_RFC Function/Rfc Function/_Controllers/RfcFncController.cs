﻿using System;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.SAPMsg;
using BxS_WorxNCO.RfcFunction.TableReader;

using static	BxS_WorxNCO.Main							.NCO_Constants;
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncController : IRfcFncController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncController( IRfcDestination rfcDestination )
					{
						this.RfcDestination		= rfcDestination	??	throw		new ArgumentException( $"{typeof(BDC_Data).Namespace}:- BDCData null" );
						//.............................................
						this._RfcFncMngr			=	new	Lazy<IRfcFncManager>	(	()=>	new	RfcFncManager( this.RfcDestination )	, cz_LM );
						//.............................................
						this._Lock	= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<IRfcFncManager>	_RfcFncMngr	;
				//.................................................
				private readonly	object	_Lock	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IRfcDestination		RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task ActivateProfilesAsync()
					{
						await	this._RfcFncMngr.Value.UpdateProfilesAsync().ConfigureAwait(false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterBDCProfile( bool TranVersion = false )
					{
						string	lc_Name		= TranVersion ? cz_BDCTran : cz_BDCCall;
						//.............................................
						if ( ! this._RfcFncMngr.Value.ProfileExists( lc_Name ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( lc_Name ) )
											{
												this._RfcFncMngr.Value.RegisterProfile( new BDC_Profile( lc_Name , BDC_Factory.Instance , TranVersion ) );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Profile GetAddBDCProfile( bool TranVersion = false )
					{
						this.RegisterBDCProfile( TranVersion );
						//.............................................
						return	this._RfcFncMngr.Value.GetProfile< BDC_Profile >( TranVersion ? cz_BDCTran : cz_BDCCall );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Function CreateBDCFunction	( bool TranVersion = false )=>	new	BDC_Function(	this.GetAddBDCProfile( TranVersion ) );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Message Compiler"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterSAPMsgProfile()
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( cz_SAPMsgCompiler ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( cz_SAPMsgCompiler ) )
											{
												var	lo_Prof	= new SAPMsg_Profile(		cz_SAPMsgCompiler
																													, SAPMsg_Factory.Instance	);

												this._RfcFncMngr.Value.RegisterProfile( lo_Prof );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPMsg_Profile GetAddSAPMsgProfile()
					{
						this.RegisterSAPMsgProfile();
						//.............................................
						return	this._RfcFncMngr.Value.GetProfile< SAPMsg_Profile >( cz_SAPMsgCompiler );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPMsg_Function CreateSAPMsgFunction()	=>	new SAPMsg_Function( this.GetAddSAPMsgProfile() );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Table Reader"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterTableReaderProfile()
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( cz_TableReader ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( cz_TableReader ) )
											{
												var	lo_Prof	= new TblRdr_Profile(		cz_TableReader
																													, TblRdr_Factory.Instance	);

												this._RfcFncMngr.Value.RegisterProfile( lo_Prof );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TblRdr_Profile GetAddTblRdrProfile()
					{
						this.RegisterTableReaderProfile();
						//.............................................
						return	this._RfcFncMngr.Value.GetProfile< TblRdr_Profile >( cz_TableReader );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TblRdr_Function CreateTblRdrFunction()	=>	new TblRdr_Function( this.GetAddTblRdrProfile() );

			#endregion

		}
}
