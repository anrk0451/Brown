namespace Brown.Forms
{
	partial class Frm_layerPrice
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
			this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// simpleButton3
			// 
			this.simpleButton3.Appearance.BackColor = System.Drawing.Color.SteelBlue;
			this.simpleButton3.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton3.Appearance.Options.UseBackColor = true;
			this.simpleButton3.Appearance.Options.UseForeColor = true;
			this.simpleButton3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton3.Location = new System.Drawing.Point(3, 243);
			this.simpleButton3.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.simpleButton3.Name = "simpleButton3";
			this.simpleButton3.Size = new System.Drawing.Size(88, 22);
			this.simpleButton3.TabIndex = 11;
			this.simpleButton3.Text = "复制当前价格";
			this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
			// 
			// simpleButton2
			// 
			this.simpleButton2.Appearance.BackColor = System.Drawing.Color.LimeGreen;
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton2.Appearance.Options.UseBackColor = true;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point(286, 243);
			this.simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(60, 22);
			this.simpleButton2.TabIndex = 10;
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
			this.simpleButton1.Location = new System.Drawing.Point(193, 243);
			this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(89, 22);
			this.simpleButton1.TabIndex = 9;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			this.gridControl1.Size = new System.Drawing.Size(364, 229);
			this.gridControl1.TabIndex = 8;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 233;
			this.gridView1.FixedLineWidth = 1;
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsCustomization.AllowFilter = false;
			this.gridView1.OptionsCustomization.AllowSort = false;
			this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridView1.OptionsFilter.AllowFilterEditor = false;
			this.gridView1.OptionsFilter.AllowFilterIncrementalSearch = false;
			this.gridView1.OptionsFilter.AllowMRUFilterList = false;
			this.gridView1.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
			this.gridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
			this.gridView1.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
			this.gridView1.OptionsMenu.EnableColumnMenu = false;
			this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridView1_ValidatingEditor);
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Mask.EditMask = "N2";
			this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// Frm_layerPrice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 273);
			this.Controls.Add(this.simpleButton3);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.gridControl1);
			this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.Name = "Frm_layerPrice";
			this.Text = "层定价";
			this.Load += new System.EventHandler(this.Frm_layerPrice_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton3;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
	}
}