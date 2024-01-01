namespace Brown.Forms
{
	partial class Frm_business07
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
            this.b_exit = new DevExpress.XtraEditors.SimpleButton();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            this.glookup_lc = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.glookup_lc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // b_exit
            // 
            this.b_exit.Appearance.BackColor = System.Drawing.Color.Gray;
            this.b_exit.Appearance.ForeColor = System.Drawing.Color.White;
            this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_exit.Appearance.Options.UseBackColor = true;
            this.b_exit.Appearance.Options.UseForeColor = true;
            this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_exit.Location = new System.Drawing.Point(244, 49);
            this.b_exit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(80, 20);
            this.b_exit.TabIndex = 62;
            this.b_exit.Text = "退出";
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // b_ok
            // 
            this.b_ok.Appearance.BackColor = System.Drawing.Color.Blue;
            this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
            this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_ok.Appearance.Options.UseBackColor = true;
            this.b_ok.Appearance.Options.UseForeColor = true;
            this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_ok.Location = new System.Drawing.Point(244, 23);
            this.b_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(80, 20);
            this.b_ok.TabIndex = 63;
            this.b_ok.Text = "确定";
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // glookup_lc
            // 
            this.glookup_lc.EditValue = "";
            this.glookup_lc.Location = new System.Drawing.Point(86, 23);
            this.glookup_lc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.glookup_lc.Name = "glookup_lc";
            this.glookup_lc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glookup_lc.Properties.NullText = "";
            this.glookup_lc.Properties.PopupView = this.gridLookUpEdit1View;
            this.glookup_lc.Size = new System.Drawing.Size(128, 19);
            this.glookup_lc.TabIndex = 61;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridLookUpEdit1View.DetailHeight = 233;
            this.gridLookUpEdit1View.FixedLineWidth = 1;
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "编号";
            this.gridColumn1.FieldName = "ITEM_ID";
            this.gridColumn1.MinWidth = 15;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 56;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "ITEM_TEXT";
            this.gridColumn2.MinWidth = 15;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 56;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "单价";
            this.gridColumn3.DisplayFormat.FormatString = "n2";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "PRICE";
            this.gridColumn3.MinWidth = 15;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 56;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 25);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "灵车";
            // 
            // Frm_business07
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 89);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.glookup_lc);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Frm_business07";
            this.Text = "灵车办理";
            this.Load += new System.EventHandler(this.Frm_business07_Load);
            ((System.ComponentModel.ISupportInitialize)(this.glookup_lc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.GridLookUpEdit glookup_lc;
		private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
	}
}