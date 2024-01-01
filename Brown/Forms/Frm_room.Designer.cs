
namespace Brown.Forms
{
	partial class Frm_room
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.te_rg003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
			this.chk_rg099 = new DevExpress.XtraEditors.CheckEdit();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.te_rg003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chk_rg099.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// te_rg003
			// 
			this.te_rg003.Enabled = false;
			this.te_rg003.Location = new System.Drawing.Point(101, 28);
			this.te_rg003.Margin = new System.Windows.Forms.Padding(2);
			this.te_rg003.Name = "te_rg003";
			this.te_rg003.Properties.Mask.EditMask = "d";
			this.te_rg003.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.te_rg003.Size = new System.Drawing.Size(134, 18);
			this.te_rg003.TabIndex = 53;
			// 
			// labelControl9
			// 
			this.labelControl9.Appearance.Image = null;
			this.labelControl9.AppearanceDisabled.Image = null;
			this.labelControl9.AppearanceHovered.Image = null;
			this.labelControl9.AppearancePressed.Image = null;
			this.labelControl9.Location = new System.Drawing.Point(27, 30);
			this.labelControl9.Margin = new System.Windows.Forms.Padding(2);
			this.labelControl9.Name = "labelControl9";
			this.labelControl9.Size = new System.Drawing.Size(54, 12);
			this.labelControl9.TabIndex = 52;
			this.labelControl9.Text = "寄存室名:";
			// 
			// chk_rg099
			// 
			this.chk_rg099.Location = new System.Drawing.Point(25, 72);
			this.chk_rg099.Name = "chk_rg099";
			this.chk_rg099.Properties.Caption = "自助选号";
			this.chk_rg099.Size = new System.Drawing.Size(75, 20);
			this.chk_rg099.TabIndex = 54;
			// 
			// simpleButton2
			// 
			this.simpleButton2.Appearance.BackColor = System.Drawing.Color.LimeGreen;
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton2.Appearance.Options.UseBackColor = true;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point(271, 52);
			this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton2.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(82, 21);
			this.simpleButton2.TabIndex = 56;
			this.simpleButton2.Text = "取消";
			this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton1.Location = new System.Drawing.Point(271, 27);
			this.simpleButton1.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(82, 21);
			this.simpleButton1.TabIndex = 55;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// Frm_room
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 116);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.chk_rg099);
			this.Controls.Add(this.te_rg003);
			this.Controls.Add(this.labelControl9);
			this.Name = "Frm_room";
			this.Text = "寄存室";
			this.Load += new System.EventHandler(this.Frm_room_Load);
			((System.ComponentModel.ISupportInitialize)(this.te_rg003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chk_rg099.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit te_rg003;
		private DevExpress.XtraEditors.LabelControl labelControl9;
		private DevExpress.XtraEditors.CheckEdit chk_rg099;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
	}
}