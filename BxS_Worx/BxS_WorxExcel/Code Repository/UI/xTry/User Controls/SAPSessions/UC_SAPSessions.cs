﻿using System;
using System.Windows.Forms;
using System.Drawing;
//.........................................................
using BxS_WorxExcel.UI.UC;
using BxS_WorxExcel.UI;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.SAPSessions
{
	internal partial class UC_SAPSessions : UserControl , IViewT<SAPSessionsVM>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_SAPSessions()
					{
						InitializeComponent();
						//.............................................
						this.ViewModel	= new	SAPSessionsVM();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_SAPSessions( SAPSessionsVM	vm )
					{
						InitializeComponent();
						//.............................................
						this.ViewModel	= vm;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const DataSourceUpdateMode DSMODE	= DataSourceUpdateMode.OnPropertyChanged;
				//.................................................
				private	const	string	PNME_VAL		= "Value"	;
				private	const	string	PNME_TEXT		= "Text"	;
				//.................................................
				private	DataGridView	_DGV;
				private BindingSource	_BS;

			#endregion

			//===========================================================================================
			#region "Properties"

				public SAPSessionsVM	ViewModel { get; set; }

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnLoad( EventArgs e )
					{
						this.ViewModel.SuspendLayout	+=	this.OnSuspendLayout	;
						this.ViewModel.ResumeLayout		+=	this.OnResumeLayout		;
						//.............................................
						this.xtbn_Load.Click	+=	this.ViewModel.RequestSAPSessionListEventHandler	;
						this.xtbn_Reset.Click	+=	this.ViewModel.ResetSAPSessionListEventHandler				;
						//.............................................
						var x				= new DataGridViewCellStyle { BackColor	= Color.WhiteSmoke };
						this._DGV   = new DataGridView	{
																								Dock                            = DockStyle.Fill
																							,	Name                            = "BxSDGV"
																							, AllowUserToAddRows	            = false
																							,	AllowUserToDeleteRows	          = false
																							, AllowUserToResizeRows						= false
																							,	AlternatingRowsDefaultCellStyle	= x
																							, AutoGenerateColumns							= false
																							, MultiSelect											= true
																						};
						//...
						this.splitContainer1.Panel2.Controls.Add( this._DGV );

						this.ConfigureBindings()	;
						this.ConfigureSetup()			;
						this.ConfigureColumns()		;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void OnSuspendLayout()	=>	this._BS.SuspendBinding()	;	//	this._DGV.SuspendLayout()	;
				protected void OnResumeLayout()		=>	this._BS.ResumeBinding()	;	//  this._DGV.ResumeLayout()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindSelection()
					{
						this.BindControl( this.xtbx_User	, PNME_TEXT	, nameof( this.ViewModel.UserID				) );
						this.BindControl( this.xtbx_SsnID	, PNME_TEXT	, nameof( this.ViewModel.SessionName	) );
						this.BindControl( this.xdtp_Start	, PNME_VAL	, nameof( this.ViewModel.StartDate		) );
						this.BindControl( this.xdtp_End		, PNME_VAL	, nameof( this.ViewModel.EndDate			) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindControl( Control control , string cntrlPropName , string vmPropName )
					{
						control.DataBindings.Add( new	Binding( cntrlPropName , this.ViewModel	, vmPropName , true , DSMODE ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureBindings()
					{
						this._BS							= new BindingSource	{	DataSource = this.ViewModel.List };
						this._DGV.DataSource	=	this._BS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureSetup()
					{
						this._DGV.AllowUserToAddRows	= false;
						this._DGV.MultiSelect         = false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureColumns()
					{
						const	string	SAPID		= "SAPID";

						var lo_C1 = new DataGridViewTextBoxColumn	{
																													Name							= SAPID
																												,	HeaderText				= "SAP System"
																												,	DataPropertyName	= this.ViewModel.PName_User
																											};
						//...
						const	string	NAME	= "Name";

						var lo_C2 = new DataGridViewTextBoxColumn	{
																													Name							= NAME
																												,	HeaderText				= NAME
																												,	DataPropertyName	= this.ViewModel.PName_Session
																											};
						//...
						const	string	CLIENT	= "Client";

						var lo_C3 = new DataGridViewTextBoxColumn	{
																													Name							= CLIENT
																												,	HeaderText				= CLIENT
																												,	DataPropertyName	= this.ViewModel.PName_SAPTCde
																											};
						//...
						const	string	DATE	= "Date";

						var lo_C4 = new DataGridViewTextBoxColumn	{
																													Name							= DATE
																												,	HeaderText				= DATE
																												,	DataPropertyName	= this.ViewModel.PName_Date
																											};
						//...
						this._DGV.Columns.Add(lo_C1);
						this._DGV.Columns.Add(lo_C2);
						this._DGV.Columns.Add(lo_C3);
						this._DGV.Columns.Add(lo_C4);

						this._DGV.Columns["Date"].DefaultCellStyle.Format = " yyyy/MM/dd";
					}

		#endregion

		}
}
