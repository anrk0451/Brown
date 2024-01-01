namespace Brown.BusinessObject
{
    partial class Report_Item_Stat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report_Item_Stat));
			this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			this.bs_bs = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.reportGenerator1 = new DevExpress.XtraReports.ReportGeneration.ReportGenerator(this.components);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// barHeaderItem1
			// 
			this.barHeaderItem1.Id = 4;
			this.barHeaderItem1.Name = "barHeaderItem1";
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 603);
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barHeaderItem1,
            this.bs_bs,
            this.barButtonItem5,
            this.barButtonItem6});
			this.barManager1.MaxItemId = 8;
			// 
			// bar1
			// 
			this.bar1.BarName = "工具";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem5, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem4, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bs_bs)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawDragBorder = false;
			this.bar1.Text = "工具";
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "查询条件";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "刷新";
			this.barButtonItem2.Id = 1;
			this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
			this.barButtonItem2.Name = "barButtonItem2";
			this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
			// 
			// barButtonItem5
			// 
			this.barButtonItem5.Caption = "打印";
			this.barButtonItem5.Id = 6;
			this.barButtonItem5.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem5.ImageOptions.SvgImage")));
			this.barButtonItem5.Name = "barButtonItem5";
			this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
			// 
			// barButtonItem4
			// 
			this.barButtonItem4.Caption = "导出";
			this.barButtonItem4.Id = 3;
			this.barButtonItem4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem4.ImageOptions.SvgImage")));
			this.barButtonItem4.Name = "barButtonItem4";
			this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
			// 
			// barButtonItem6
			// 
			this.barButtonItem6.Caption = "barButtonItem6";
			this.barButtonItem6.Id = 7;
			this.barButtonItem6.Name = "barButtonItem6";
			// 
			// bs_bs
			// 
			this.bs_bs.Caption = "                             共有收费笔数:";
			this.bs_bs.Id = 5;
			this.bs_bs.Name = "bs_bs";
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.barDockControlTop.Size = new System.Drawing.Size(1180, 32);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 635);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.barDockControlBottom.Size = new System.Drawing.Size(1180, 0);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(1180, 32);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 603);
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "导出";
			this.barButtonItem3.Id = 2;
			this.barButtonItem3.Name = "barButtonItem3";
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "金额";
			this.gridColumn4.DisplayFormat.FormatString = "N2";
			this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn4.FieldName = "JE";
			this.gridColumn4.MinWidth = 25;
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 165;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "数量";
			this.gridColumn3.DisplayFormat.FormatString = "N1";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn3.FieldName = "NUMS";
			this.gridColumn3.MinWidth = 25;
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 105;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "单价";
			this.gridColumn2.DisplayFormat.FormatString = "N2";
			this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn2.FieldName = "PRICE";
			this.gridColumn2.MinWidth = 25;
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 108;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "服务商品名";
			this.gridColumn1.FieldName = "CS05";
			this.gridColumn1.MinWidth = 25;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 185;
			// 
			// gridView1
			// 
			this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
			this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridView1.Appearance.Row.Options.UseFont = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
			this.gridView1.DetailHeight = 292;
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 45;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsBehavior.ReadOnly = true;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowFooter = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gridControl1.Location = new System.Drawing.Point(0, 32);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gridControl1.MenuManager = this.barManager1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(1180, 603);
			this.gridControl1.TabIndex = 27;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// Report_Item_Stat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "Report_Item_Stat";
			this.Size = new System.Drawing.Size(1180, 635);
			this.Load += new System.EventHandler(this.Report_Item_Stat_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarStaticItem bs_bs;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraReports.ReportGeneration.ReportGenerator reportGenerator1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem6;
	}
}
