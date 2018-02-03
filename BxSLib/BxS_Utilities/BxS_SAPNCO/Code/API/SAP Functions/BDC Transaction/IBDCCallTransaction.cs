﻿//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public interface IBDCCallTransaction
		{
			#region "Properties"

				int								Count					{	get; }
				string						Name					{ get; }

				DTO_CTUParams		DTO_CTUParms	{ get; }


				BDCData						BDCData				{ get; }

				//SMC.IRfcFunction		RfcFunction			{ get; set; }
				//SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				BDCEntry	CreateBDCEntry	(	string	programName	= BDCConstants.lz_E	,
																		int			dynpro			= 0									,
																		bool		begin				= false							,
																		string	field				= BDCConstants.lz_E	,
																		string	value				= BDCConstants.lz_E	,
																		bool		autoAdd			= true								);

				//.................................................
				bool	AddBDCEntry( BDCEntry	entry );
				//.................................................
				void	Reset();

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
