namespace Brown.Forms
{
	partial class Frm_login
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_login));
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			this.textEdit_pwd = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.textEdit_user = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit_pwd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit_user.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.Color.LimeGreen;
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(235, 125);
			this.b_exit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(47, 21);
			this.b_exit.TabIndex = 12;
			this.b_exit.Text = "退出";
			this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
			// 
			// checkEdit1
			// 
			this.checkEdit1.Location = new System.Drawing.Point(75, 100);
			this.checkEdit1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.checkEdit1.Name = "checkEdit1";
			this.checkEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.checkEdit1.Properties.Appearance.Options.UseForeColor = true;
			this.checkEdit1.Properties.Caption = "记住密码";
			this.checkEdit1.Size = new System.Drawing.Size(68, 20);
			this.checkEdit1.TabIndex = 9;
			this.checkEdit1.Visible = false;
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(123, 125);
			this.b_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(107, 21);
			this.b_ok.TabIndex = 11;
			this.b_ok.Text = "登陆";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// textEdit_pwd
			// 
			this.textEdit_pwd.EditValue = "";
			this.textEdit_pwd.Location = new System.Drawing.Point(75, 72);
			this.textEdit_pwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textEdit_pwd.Name = "textEdit_pwd";
			this.textEdit_pwd.Properties.Appearance.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textEdit_pwd.Properties.Appearance.Options.UseFont = true;
			this.textEdit_pwd.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textEdit_pwd.Properties.ContextImageOptions.Image")));
			this.textEdit_pwd.Properties.PasswordChar = '*';
			this.textEdit_pwd.Size = new System.Drawing.Size(250, 20);
			this.textEdit_pwd.TabIndex = 8;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("新宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.LineVisible = true;
			this.labelControl1.Location = new System.Drawing.Point(168, 5);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(60, 30);
			this.labelControl1.TabIndex = 10;
			this.labelControl1.Text = "欢迎";
			// 
			// dxErrorProvider1
			// 
			this.dxErrorProvider1.ContainerControl = this;
			// 
			// textEdit_user
			// 
			this.textEdit_user.Location = new System.Drawing.Point(75, 45);
			this.textEdit_user.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.textEdit_user.Name = "textEdit_user";
			this.textEdit_user.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textEdit_user.Properties.ContextImageOptions.Image")));
			this.textEdit_user.Size = new System.Drawing.Size(250, 20);
			this.textEdit_user.TabIndex = 7;
			// 
			// Frm_login
			// 
			this.AcceptButton = this.b_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Center;
			this.BackgroundImageStore = global::Brown.Properties.Resources._1;
			this.CancelButton = this.b_exit;
			this.ClientSize = new System.Drawing.Size(401, 151);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.checkEdit1);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.textEdit_pwd);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.textEdit_user);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Frm_login";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Frm_login";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Frm_login_Load);
			this.Shown += new System.EventHandler(this.Frm_login_Shown);
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit_pwd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit_user.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.CheckEdit checkEdit1;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.TextEdit textEdit_pwd;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
		private DevExpress.XtraEditors.TextEdit textEdit_user;
	}
}