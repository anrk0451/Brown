namespace Brown.Forms
{
    partial class Frm_Operator
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
			this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
			this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
			this.clbx_roles = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.txtedit_pwd2 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_pwd = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc002 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.te_uc007 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.te_uc008 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_uc007.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_uc008.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// sb_cancel
			// 
			this.sb_cancel.Appearance.BackColor = System.Drawing.Color.LimeGreen;
			this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.White;
			this.sb_cancel.Appearance.Options.UseBackColor = true;
			this.sb_cancel.Appearance.Options.UseForeColor = true;
			this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_cancel.Location = new System.Drawing.Point(267, 313);
			this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.sb_cancel.Name = "sb_cancel";
			this.sb_cancel.Size = new System.Drawing.Size(68, 23);
			this.sb_cancel.TabIndex = 32;
			this.sb_cancel.Text = "关闭";
			this.sb_cancel.Click += new System.EventHandler(this.sb_cancel_Click);
			// 
			// sb_ok
			// 
			this.sb_ok.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.sb_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.sb_ok.Appearance.Options.UseBackColor = true;
			this.sb_ok.Appearance.Options.UseForeColor = true;
			this.sb_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_ok.Location = new System.Drawing.Point(160, 313);
			this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.sb_ok.Name = "sb_ok";
			this.sb_ok.Size = new System.Drawing.Size(103, 23);
			this.sb_ok.TabIndex = 31;
			this.sb_ok.Text = "确定";
			this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
			// 
			// clbx_roles
			// 
			this.clbx_roles.CheckOnClick = true;
			this.clbx_roles.Cursor = System.Windows.Forms.Cursors.Default;
			this.clbx_roles.DisplayMember = "RO003";
			this.clbx_roles.Location = new System.Drawing.Point(68, 239);
			this.clbx_roles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.clbx_roles.Name = "clbx_roles";
			this.clbx_roles.Size = new System.Drawing.Size(267, 63);
			this.clbx_roles.TabIndex = 30;
			this.clbx_roles.ValueMember = "RO001";
			// 
			// txtedit_pwd2
			// 
			this.txtedit_pwd2.EditValue = "";
			this.txtedit_pwd2.Location = new System.Drawing.Point(146, 111);
			this.txtedit_pwd2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtedit_pwd2.Name = "txtedit_pwd2";
			this.txtedit_pwd2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_pwd2.Properties.NullText = "请再次输入密码";
			this.txtedit_pwd2.Properties.NullValuePrompt = "请再次输入密码";
			this.txtedit_pwd2.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_pwd2.Properties.PasswordChar = '*';
			this.txtedit_pwd2.Size = new System.Drawing.Size(190, 18);
			this.txtedit_pwd2.TabIndex = 29;
			// 
			// labelControl4
			// 
			this.labelControl4.Appearance.Image = null;
			this.labelControl4.AppearanceDisabled.Image = null;
			this.labelControl4.AppearanceHovered.Image = null;
			this.labelControl4.AppearancePressed.Image = null;
			this.labelControl4.Location = new System.Drawing.Point(68, 113);
			this.labelControl4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(54, 12);
			this.labelControl4.TabIndex = 28;
			this.labelControl4.Text = "确认密码*";
			// 
			// txtedit_pwd
			// 
			this.txtedit_pwd.EditValue = "";
			this.txtedit_pwd.Location = new System.Drawing.Point(146, 79);
			this.txtedit_pwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtedit_pwd.Name = "txtedit_pwd";
			this.txtedit_pwd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_pwd.Properties.NullText = "请输入密码";
			this.txtedit_pwd.Properties.NullValuePrompt = "请输入密码";
			this.txtedit_pwd.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_pwd.Properties.PasswordChar = '*';
			this.txtedit_pwd.Size = new System.Drawing.Size(190, 18);
			this.txtedit_pwd.TabIndex = 27;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Image = null;
			this.labelControl3.AppearanceDisabled.Image = null;
			this.labelControl3.AppearanceHovered.Image = null;
			this.labelControl3.AppearancePressed.Image = null;
			this.labelControl3.Location = new System.Drawing.Point(68, 81);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(30, 12);
			this.labelControl3.TabIndex = 26;
			this.labelControl3.Text = "密码*";
			// 
			// txtedit_uc003
			// 
			this.txtedit_uc003.EditValue = "";
			this.txtedit_uc003.Location = new System.Drawing.Point(146, 45);
			this.txtedit_uc003.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtedit_uc003.Name = "txtedit_uc003";
			this.txtedit_uc003.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_uc003.Properties.NullText = "请输入用户姓名";
			this.txtedit_uc003.Properties.NullValuePrompt = "请输入用户姓名";
			this.txtedit_uc003.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc003.Size = new System.Drawing.Size(190, 18);
			this.txtedit_uc003.TabIndex = 25;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Image = null;
			this.labelControl2.AppearanceDisabled.Image = null;
			this.labelControl2.AppearanceHovered.Image = null;
			this.labelControl2.AppearancePressed.Image = null;
			this.labelControl2.Location = new System.Drawing.Point(68, 47);
			this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(54, 12);
			this.labelControl2.TabIndex = 24;
			this.labelControl2.Text = "用户姓名*";
			// 
			// txtedit_uc002
			// 
			this.txtedit_uc002.EditValue = "";
			this.txtedit_uc002.Location = new System.Drawing.Point(146, 14);
			this.txtedit_uc002.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtedit_uc002.Name = "txtedit_uc002";
			this.txtedit_uc002.Properties.NullText = "请输入用户登录代码";
			this.txtedit_uc002.Properties.NullValuePrompt = "请输入用户登录代码";
			this.txtedit_uc002.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc002.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtedit_uc002.Properties.ValidateOnEnterKey = true;
			this.txtedit_uc002.Size = new System.Drawing.Size(190, 18);
			this.txtedit_uc002.TabIndex = 23;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.Location = new System.Drawing.Point(68, 16);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(54, 12);
			this.labelControl1.TabIndex = 22;
			this.labelControl1.Text = "用户代码*";
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.Image = null;
			this.labelControl5.AppearanceDisabled.Image = null;
			this.labelControl5.AppearanceHovered.Image = null;
			this.labelControl5.AppearancePressed.Image = null;
			this.labelControl5.Location = new System.Drawing.Point(68, 147);
			this.labelControl5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(108, 12);
			this.labelControl5.TabIndex = 33;
			this.labelControl5.Text = "财政发票服务器账号";
			// 
			// te_uc007
			// 
			this.te_uc007.EditValue = "";
			this.te_uc007.Location = new System.Drawing.Point(145, 163);
			this.te_uc007.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.te_uc007.Name = "te_uc007";
			this.te_uc007.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.te_uc007.Properties.NullValuePromptShowForEmptyValue = true;
			this.te_uc007.Size = new System.Drawing.Size(190, 18);
			this.te_uc007.TabIndex = 34;
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.Image = null;
			this.labelControl6.AppearanceDisabled.Image = null;
			this.labelControl6.AppearanceHovered.Image = null;
			this.labelControl6.AppearancePressed.Image = null;
			this.labelControl6.Location = new System.Drawing.Point(68, 193);
			this.labelControl6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(108, 12);
			this.labelControl6.TabIndex = 35;
			this.labelControl6.Text = "财政发票服务器密码";
			// 
			// te_uc008
			// 
			this.te_uc008.EditValue = "";
			this.te_uc008.Location = new System.Drawing.Point(145, 209);
			this.te_uc008.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.te_uc008.Name = "te_uc008";
			this.te_uc008.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.te_uc008.Properties.NullText = "请输入密码";
			this.te_uc008.Properties.NullValuePrompt = "请输入密码";
			this.te_uc008.Properties.NullValuePromptShowForEmptyValue = true;
			this.te_uc008.Properties.PasswordChar = '*';
			this.te_uc008.Size = new System.Drawing.Size(190, 18);
			this.te_uc008.TabIndex = 36;
			// 
			// Frm_Operator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 345);
			this.Controls.Add(this.te_uc008);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.te_uc007);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.sb_cancel);
			this.Controls.Add(this.sb_ok);
			this.Controls.Add(this.clbx_roles);
			this.Controls.Add(this.txtedit_pwd2);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.txtedit_pwd);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txtedit_uc003);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.txtedit_uc002);
			this.Controls.Add(this.labelControl1);
			this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.Name = "Frm_Operator";
			this.Text = "操作员";
			this.Load += new System.EventHandler(this.Frm_Operator_Load);
			((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_uc007.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_uc008.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.CheckedListBoxControl clbx_roles;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtedit_uc003;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtedit_uc002;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit te_uc007;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit te_uc008;
    }
}