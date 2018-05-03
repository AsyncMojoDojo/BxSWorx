﻿namespace BxS_WorxExcel
	{
	partial class BxS_WorxMain : Microsoft.Office.Tools.Ribbon.RibbonBase
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public BxS_WorxMain()
				: base(Globals.Factory.GetRibbonFactory())
			{
			InitializeComponent();
			}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.rtb_BxS = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.btn_SaveAct = this.Factory.CreateRibbonButton();
			this.btn_SaveAll = this.Factory.CreateRibbonButton();
			this.btn_XmlCfg = this.Factory.CreateRibbonButton();
			this.xbg_SAPLogon = this.Factory.CreateRibbonGroup();
			this.button4 = this.Factory.CreateRibbonButton();
			this.dropDown1 = this.Factory.CreateRibbonDropDown();
			this.btn_Submit = this.Factory.CreateRibbonButton();
			this.rtb_BxS.SuspendLayout();
			this.group1.SuspendLayout();
			this.xbg_SAPLogon.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtb_BxS
			// 
			this.rtb_BxS.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.rtb_BxS.Groups.Add(this.group1);
			this.rtb_BxS.Groups.Add(this.xbg_SAPLogon);
			this.rtb_BxS.Label = "BxSAP";
			this.rtb_BxS.Name = "rtb_BxS";
			// 
			// group1
			// 
			this.group1.Items.Add(this.btn_SaveAct);
			this.group1.Items.Add(this.btn_SaveAll);
			this.group1.Items.Add(this.btn_XmlCfg);
			this.group1.Label = "group1";
			this.group1.Name = "group1";
			// 
			// btn_SaveAct
			// 
			this.btn_SaveAct.Label = "Write Active";
			this.btn_SaveAct.Name = "btn_SaveAct";
			this.btn_SaveAct.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.WriteActive_Click);
			// 
			// btn_SaveAll
			// 
			this.btn_SaveAll.Label = "Write ALL";
			this.btn_SaveAll.Name = "btn_SaveAll";
			this.btn_SaveAll.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.WriteAll_Click);
			// 
			// btn_XmlCfg
			// 
			this.btn_XmlCfg.Label = "Add Config";
			this.btn_XmlCfg.Name = "btn_XmlCfg";
			this.btn_XmlCfg.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SaveXMLCfg_Click);
			// 
			// xbg_SAPLogon
			// 
			this.xbg_SAPLogon.Items.Add(this.button4);
			this.xbg_SAPLogon.Items.Add(this.dropDown1);
			this.xbg_SAPLogon.Items.Add(this.btn_Submit);
			this.xbg_SAPLogon.Label = "SAP logon";
			this.xbg_SAPLogon.Name = "xbg_SAPLogon";
			// 
			// button4
			// 
			this.button4.Label = "Request";
			this.button4.Name = "button4";
			// 
			// dropDown1
			// 
			this.dropDown1.Label = "dropDown1";
			this.dropDown1.Name = "dropDown1";
			this.dropDown1.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDown1_Load);
			// 
			// btn_Submit
			// 
			this.btn_Submit.Label = "Submit";
			this.btn_Submit.Name = "btn_Submit";
			// 
			// BxS_WorxMain
			// 
			this.Name = "BxS_WorxMain";
			this.RibbonType = "Microsoft.Excel.Workbook";
			this.Tabs.Add(this.rtb_BxS);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BxS_WorxMain_Load);
			this.rtb_BxS.ResumeLayout(false);
			this.rtb_BxS.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.xbg_SAPLogon.ResumeLayout(false);
			this.xbg_SAPLogon.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab rtb_BxS;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_SaveAct;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_SaveAll;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_XmlCfg;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup xbg_SAPLogon;
		internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Submit;
		}

	partial class ThisRibbonCollection
		{
		internal BxS_WorxMain BxS_WorxMain
			{
			get { return this.GetRibbon<BxS_WorxMain>(); }
			}
		}
	}
