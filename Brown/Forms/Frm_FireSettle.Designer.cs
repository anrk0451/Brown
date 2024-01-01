namespace Brown.Forms
{
    partial class Frm_FireSettle
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(508, 385);
			this.gridControl1.TabIndex = 61;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
			this.gridView1.DetailHeight = 233;
			this.gridView1.FixedLineWidth = 1;
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 34;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsCustomization.AllowGroup = false;
			this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowFooter = true;
			this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "销售编号";
			this.gridColumn1.FieldName = "SA001";
			this.gridColumn1.MinWidth = 15;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowShowHide = false;
			this.gridColumn1.Width = 56;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "商品或服务名";
			this.gridColumn2.FieldName = "SA003";
			this.gridColumn2.MinWidth = 15;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			this.gridColumn2.Width = 139;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "单价";
			this.gridColumn3.DisplayFormat.FormatString = "N2";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn3.FieldName = "PRICE";
			this.gridColumn3.MinWidth = 15;
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			this.gridColumn3.Width = 67;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "数量";
			this.gridColumn4.DisplayFormat.FormatString = "N1";
			this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn4.FieldName = "NUMS";
			this.gridColumn4.MinWidth = 15;
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 2;
			this.gridColumn4.Width = 64;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "销售金额";
			this.gridColumn5.DisplayFormat.FormatString = "N2";
			this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn5.FieldName = "SA007";
			this.gridColumn5.MinWidth = 15;
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SA007", "合计={0:N2}")});
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 3;
			this.gridColumn5.Width = 94;
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
			this.b_exit.Location = new System.Drawing.Point(522, 34);
			this.b_exit.Margin = new System.Windows.Forms.Padding(2);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(113, 21);
			this.b_exit.TabIndex = 62;
			this.b_exit.Text = "退出";
			this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(522, 8);
			this.b_ok.Margin = new System.Windows.Forms.Padding(2);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(113, 21);
			this.b_ok.TabIndex = 63;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// Frm_FireSettle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 385);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.Name = "Frm_FireSettle";
			this.Text = "业务结算收费";
			this.Load += new System.EventHandler(this.Frm_FireSettle_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton b_exit;
        private DevExpress.XtraEditors.SimpleButton b_ok;
    }
}