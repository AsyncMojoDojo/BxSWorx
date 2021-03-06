﻿using System;
using System.Collections.Generic;
using BxS_WorxIPX.Main;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal class Model_FormX
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Model_FormX()
					{
						this.Request	= this._NCOxCntlr.NewSAPSessionRequest();
						this.List			= new	List<IDTO_Session>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	INCOx_Controller	_NCOxCntlr = IPX_Controller.Instance.NCOx_Controller	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IDTO_SessionRequest		Request	{ get;	private	set; }
				internal	List<IDTO_Session>		List		{ get; }
				//...
				private		string	MySettings	{	get	=>	Properties.Settings.Default.XML_SAPSession					;
																				set	=>	Properties.Settings.Default.XML_SAPSession	= value	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ClearList()
					{
						this.List.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void UpdateSAPSessionList()
					{
						this.List.Clear();
						this.List.AddRange( this._NCOxCntlr.RequestSAPSessionList( this.Request ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void GetSettings()
					{
						if ( this.MySettings.Length.Equals(0) )
							{
								this.FactorySettings();
								this.SaveSettings();
							}
						else
							{
								this.Request				= this._NCOxCntlr.DeserializeSAPSessionRequest( this.MySettings );
								//...
								this.Request.FromX	= false;
								this.Request.ToX		= false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void SaveSettings()
					{
						this.MySettings		= this._NCOxCntlr.SerializeSAPSessionRequest( this.Request );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void FactorySettings()
					{
						this.Request.User		= "*"															;
						this.Request.Name		= "*"															;
						this.Request.From		= new DateTime( 2000 , 01 , 01 )	;
						this.Request.To			= new DateTime( 2999 , 12 , 31 )	;
						this.Request.FromX	= false														;
						this.Request.ToX		= false														;
					}

			#endregion

		}
}
