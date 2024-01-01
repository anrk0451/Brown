
namespace Brown.Forms
{
	partial class Frm_FinInvoice
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
			this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
			this.simpleButton29 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// radioGroup1
			// 
			this.radioGroup1.EditValue = "1202";
			this.radioGroup1.Location = new System.Drawing.Point(68, 347);
			this.radioGroup1.Margin = new System.Windows.Forms.Padding(2);
			this.radioGroup1.Name = "radioGroup1";
			this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
			this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.radioGroup1.Properties.Columns = 2;
			this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1202", "电子邮件"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1201", "移动电话")});
			this.radioGroup1.Size = new System.Drawing.Size(155, 23);
			this.radioGroup1.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = global::Brown.Properties.Resources.nodata;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(727, 343);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(9, 353);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(48, 12);
			this.labelControl1.TabIndex = 2;
			this.labelControl1.Text = "通知类型";
			// 
			// textEdit1
			// 
			this.textEdit1.Location = new System.Drawing.Point(225, 351);
			this.textEdit1.Margin = new System.Windows.Forms.Padding(2);
			this.textEdit1.Name = "textEdit1";
			this.textEdit1.Size = new System.Drawing.Size(250, 18);
			this.textEdit1.TabIndex = 3;
			// 
			// simpleButton29
			// 
			this.simpleButton29.Location = new System.Drawing.Point(9, 379);
			this.simpleButton29.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton29.Name = "simpleButton29";
			this.simpleButton29.Size = new System.Drawing.Size(126, 19);
			this.simpleButton29.TabIndex = 43;
			this.simpleButton29.Text = "发送电子票通知";
			this.simpleButton29.Click += new System.EventHandler(this.simpleButton29_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(139, 379);
			this.simpleButton1.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(126, 19);
			this.simpleButton1.TabIndex = 44;
			this.simpleButton1.Text = "打印电子票";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// simpleButton2
			// 
			this.simpleButton2.Location = new System.Drawing.Point(401, 379);
			this.simpleButton2.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(126, 19);
			this.simpleButton2.TabIndex = 45;
			this.simpleButton2.Text = "关闭";
			this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
			// 
			// simpleButton3
			// 
			this.simpleButton3.Location = new System.Drawing.Point(269, 379);
			this.simpleButton3.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton3.Name = "simpleButton3";
			this.simpleButton3.Size = new System.Drawing.Size(126, 19);
			this.simpleButton3.TabIndex = 46;
			this.simpleButton3.Text = "另存为......";
			this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "照片文件(*.jpg)|*.jpg|所有文件(*.*)|*.*";
			// 
			// Frm_FinInvoice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(727, 411);
			this.Controls.Add(this.simpleButton3);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.simpleButton29);
			this.Controls.Add(this.textEdit1);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.radioGroup1);
			this.Controls.Add(this.pictureBox1);
			this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.Name = "Frm_FinInvoice";
			this.Text = "打印财政电子发票";
			this.Load += new System.EventHandler(this.Frm_FinInvoice_Load);
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private DevExpress.XtraEditors.RadioGroup radioGroup1;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit textEdit1;
		private DevExpress.XtraEditors.SimpleButton simpleButton29;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private DevExpress.XtraEditors.SimpleButton simpleButton3;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}