﻿namespace BxS_WorxExcel.UI.UC
	{
	partial class UC_FlipFlop
		{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if ( disposing && ( components != null ) )
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
			this.xobj_Button = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// xlbl_Button
			// 
			this.xobj_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xobj_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xobj_Button.Location = new System.Drawing.Point(1 , 1);
			this.xobj_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xobj_Button.Name = "xlbl_Button";
			this.xobj_Button.Size = new System.Drawing.Size(46 , 43);
			this.xobj_Button.TabIndex = 1;
			this.xobj_Button.Text = "label1";
			this.xobj_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UC_FlipFlop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F , 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.xobj_Button);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_FlipFlop";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(48 , 45);
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

			}

		#endregion
		private System.Windows.Forms.Label xobj_Button;
		}
	}