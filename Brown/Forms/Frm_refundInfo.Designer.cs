namespace Brown.Forms
{
    partial class Frm_refundInfo
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.te_pjlx = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.te_pjh = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.te_zch = new DevExpress.XtraEditors.TextEdit();
			this.sb_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.te_pjlx.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_pjh.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_zch.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.Location = new System.Drawing.Point(21, 21);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(54, 12);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "票据类型:";
			// 
			// te_pjlx
			// 
			this.te_pjlx.Enabled = false;
			this.te_pjlx.Location = new System.Drawing.Point(94, 19);
			this.te_pjlx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.te_pjlx.Name = "te_pjlx";
			this.te_pjlx.Size = new System.Drawing.Size(114, 18);
			this.te_pjlx.TabIndex = 1;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Image = null;
			this.labelControl2.AppearanceDisabled.Image = null;
			this.labelControl2.AppearanceHovered.Image = null;
			this.labelControl2.AppearancePressed.Image = null;
			this.labelControl2.Location = new System.Drawing.Point(21, 52);
			this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(42, 12);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "票据号:";
			// 
			// te_pjh
			// 
			this.te_pjh.Enabled = false;
			this.te_pjh.Location = new System.Drawing.Point(94, 50);
			this.te_pjh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.te_pjh.Name = "te_pjh";
			this.te_pjh.Size = new System.Drawing.Size(114, 18);
			this.te_pjh.TabIndex = 3;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Image = null;
			this.labelControl3.AppearanceDisabled.Image = null;
			this.labelControl3.AppearanceHovered.Image = null;
			this.labelControl3.AppearancePressed.Image = null;
			this.labelControl3.Location = new System.Drawing.Point(21, 83);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(42, 12);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "注册号:";
			// 
			// te_zch
			// 
			this.te_zch.Location = new System.Drawing.Point(94, 81);
			this.te_zch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.te_zch.Name = "te_zch";
			this.te_zch.Size = new System.Drawing.Size(114, 18);
			this.te_zch.TabIndex = 5;
			// 
			// sb_exit
			// 
			this.sb_exit.Appearance.BackColor = System.Drawing.Color.LimeGreen;
			this.sb_exit.Appearance.ForeColor = System.Drawing.Color.Snow;
			this.sb_exit.Appearance.Options.UseBackColor = true;
			this.sb_exit.Appearance.Options.UseForeColor = true;
			this.sb_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.sb_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_exit.Location = new System.Drawing.Point(227, 49);
			this.sb_exit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.sb_exit.Name = "sb_exit";
			this.sb_exit.Size = new System.Drawing.Size(94, 21);
			this.sb_exit.TabIndex = 181;
			this.sb_exit.Text = "退出";
			this.sb_exit.Click += new System.EventHandler(this.sb_exit_Click);
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(227, 21);
			this.b_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(94, 21);
			this.b_ok.TabIndex = 180;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// Frm_refundInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 113);
			this.Controls.Add(this.sb_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.te_zch);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.te_pjh);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.te_pjlx);
			this.Controls.Add(this.labelControl1);
			this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.Name = "Frm_refundInfo";
			this.Text = "财政发票退费-原发票号";
			((System.ComponentModel.ISupportInitialize)(this.te_pjlx.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_pjh.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_zch.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_pjlx;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_pjh;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_zch;
        private DevExpress.XtraEditors.SimpleButton sb_exit;
        private DevExpress.XtraEditors.SimpleButton b_ok;
    }
}