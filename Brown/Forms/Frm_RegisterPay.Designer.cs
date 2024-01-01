namespace Brown.Forms
{
    partial class Frm_RegisterPay
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
            this.b_exit = new DevExpress.XtraEditors.SimpleButton();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtEdit_rc109 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc001 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_regfee = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_price = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.be_position = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc404 = new DevExpress.XtraEditors.TextEdit();
            this.rg_rc202 = new DevExpress.XtraEditors.RadioGroup();
            this.txtEdit_rc303 = new DevExpress.XtraEditors.TextEdit();
            this.txtEdit_rc004 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.rg_rc002 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_regfee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc404.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc202.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc303.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc004.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc002.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // b_exit
            // 
            this.b_exit.Appearance.BackColor = System.Drawing.Color.LimeGreen;
            this.b_exit.Appearance.ForeColor = System.Drawing.Color.Snow;
            this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_exit.Appearance.Options.UseBackColor = true;
            this.b_exit.Appearance.Options.UseForeColor = true;
            this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_exit.Location = new System.Drawing.Point(434, 317);
            this.b_exit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(58, 23);
            this.b_exit.TabIndex = 162;
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
            this.b_ok.Location = new System.Drawing.Point(329, 317);
            this.b_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(100, 23);
            this.b_ok.TabIndex = 161;
            this.b_ok.Text = "确定";
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(10, 142);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(480, 164);
            this.groupControl1.TabIndex = 160;
            this.groupControl1.Text = "缴费记录";
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Location = new System.Drawing.Point(4, 20);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(472, 109);
            this.gridControl1.TabIndex = 0;
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
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.DetailHeight = 233;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "开始日期";
            this.gridColumn1.DisplayFormat.FormatString = "d";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "RC020";
            this.gridColumn1.MinWidth = 15;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 56;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "截止日期";
            this.gridColumn2.DisplayFormat.FormatString = "d";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "RC022";
            this.gridColumn2.MinWidth = 15;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 56;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "类型";
            this.gridColumn3.FieldName = "RC031";
            this.gridColumn3.MinWidth = 15;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 56;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "单价";
            this.gridColumn4.DisplayFormat.FormatString = "N2";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "PRICE";
            this.gridColumn4.MinWidth = 15;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 56;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "缴费年限";
            this.gridColumn5.DisplayFormat.FormatString = "N1";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "NUMS";
            this.gridColumn5.MinWidth = 15;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 56;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "金额";
            this.gridColumn6.DisplayFormat.FormatString = "N2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "RC030";
            this.gridColumn6.MinWidth = 15;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 56;
            // 
            // txtEdit_rc109
            // 
            this.txtEdit_rc109.Enabled = false;
            this.txtEdit_rc109.Location = new System.Drawing.Point(386, 10);
            this.txtEdit_rc109.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc109.Name = "txtEdit_rc109";
            this.txtEdit_rc109.Size = new System.Drawing.Size(92, 18);
            this.txtEdit_rc109.TabIndex = 159;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(334, 12);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(48, 12);
            this.labelControl13.TabIndex = 158;
            this.labelControl13.Text = "寄存证号";
            // 
            // txtEdit_rc001
            // 
            this.txtEdit_rc001.Enabled = false;
            this.txtEdit_rc001.Location = new System.Drawing.Point(92, 10);
            this.txtEdit_rc001.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc001.Name = "txtEdit_rc001";
            this.txtEdit_rc001.Size = new System.Drawing.Size(92, 18);
            this.txtEdit_rc001.TabIndex = 157;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(21, 12);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 12);
            this.labelControl8.TabIndex = 156;
            this.labelControl8.Text = "逝者编号";
            // 
            // txtedit_regfee
            // 
            this.txtedit_regfee.Enabled = false;
            this.txtedit_regfee.Location = new System.Drawing.Point(388, 63);
            this.txtedit_regfee.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtedit_regfee.Name = "txtedit_regfee";
            this.txtedit_regfee.Properties.Appearance.Options.UseTextOptions = true;
            this.txtedit_regfee.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtedit_regfee.Properties.Mask.EditMask = "N2";
            this.txtedit_regfee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtedit_regfee.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtedit_regfee.Size = new System.Drawing.Size(90, 18);
            this.txtedit_regfee.TabIndex = 155;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(334, 65);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 12);
            this.labelControl7.TabIndex = 154;
            this.labelControl7.Text = "寄存费用";
            // 
            // txtedit_price
            // 
            this.txtedit_price.Enabled = false;
            this.txtedit_price.Location = new System.Drawing.Point(243, 63);
            this.txtedit_price.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtedit_price.Name = "txtedit_price";
            this.txtedit_price.Properties.Appearance.Options.UseTextOptions = true;
            this.txtedit_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtedit_price.Properties.Mask.EditMask = "N2";
            this.txtedit_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtedit_price.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtedit_price.Size = new System.Drawing.Size(61, 18);
            this.txtedit_price.TabIndex = 153;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(203, 65);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 12);
            this.labelControl6.TabIndex = 152;
            this.labelControl6.Text = "单价";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "10"});
            this.comboBox1.Location = new System.Drawing.Point(92, 63);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(68, 20);
            this.comboBox1.TabIndex = 151;
            this.comboBox1.Text = "1";
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            this.comboBox1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox1_Validating);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 65);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 12);
            this.labelControl5.TabIndex = 150;
            this.labelControl5.Text = "缴费年限*";
            // 
            // be_position
            // 
            this.be_position.Location = new System.Drawing.Point(92, 38);
            this.be_position.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.be_position.Name = "be_position";
            this.be_position.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.be_position.Properties.ReadOnly = true;
            this.be_position.Size = new System.Drawing.Size(386, 19);
            this.be_position.TabIndex = 149;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(21, 39);
            this.labelControl16.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(54, 12);
            this.labelControl16.TabIndex = 148;
            this.labelControl16.Text = "寄存位置*";
            // 
            // txtEdit_rc404
            // 
            this.txtEdit_rc404.Enabled = false;
            this.txtEdit_rc404.Location = new System.Drawing.Point(388, 119);
            this.txtEdit_rc404.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc404.Name = "txtEdit_rc404";
            this.txtEdit_rc404.Properties.Mask.BeepOnError = true;
            this.txtEdit_rc404.Properties.Mask.EditMask = "n0";
            this.txtEdit_rc404.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEdit_rc404.Size = new System.Drawing.Size(89, 18);
            this.txtEdit_rc404.TabIndex = 147;
            // 
            // rg_rc202
            // 
            this.rg_rc202.EditValue = "1";
            this.rg_rc202.Enabled = false;
            this.rg_rc202.Location = new System.Drawing.Point(218, 116);
            this.rg_rc202.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rg_rc202.Name = "rg_rc202";
            this.rg_rc202.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rg_rc202.Properties.Appearance.Options.UseBackColor = true;
            this.rg_rc202.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rg_rc202.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "男"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "女")});
            this.rg_rc202.Size = new System.Drawing.Size(124, 21);
            this.rg_rc202.TabIndex = 146;
            // 
            // txtEdit_rc303
            // 
            this.txtEdit_rc303.Enabled = false;
            this.txtEdit_rc303.Location = new System.Drawing.Point(92, 119);
            this.txtEdit_rc303.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc303.Name = "txtEdit_rc303";
            this.txtEdit_rc303.Size = new System.Drawing.Size(67, 18);
            this.txtEdit_rc303.TabIndex = 145;
            // 
            // txtEdit_rc004
            // 
            this.txtEdit_rc004.Enabled = false;
            this.txtEdit_rc004.Location = new System.Drawing.Point(388, 93);
            this.txtEdit_rc004.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc004.Name = "txtEdit_rc004";
            this.txtEdit_rc004.Properties.Mask.BeepOnError = true;
            this.txtEdit_rc004.Properties.Mask.EditMask = "n0";
            this.txtEdit_rc004.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEdit_rc004.Size = new System.Drawing.Size(89, 18);
            this.txtEdit_rc004.TabIndex = 144;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(351, 96);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 12);
            this.labelControl3.TabIndex = 143;
            this.labelControl3.Text = "年龄*";
            // 
            // rg_rc002
            // 
            this.rg_rc002.EditValue = "0";
            this.rg_rc002.Enabled = false;
            this.rg_rc002.Location = new System.Drawing.Point(218, 91);
            this.rg_rc002.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rg_rc002.Name = "rg_rc002";
            this.rg_rc002.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rg_rc002.Properties.Appearance.Options.UseBackColor = true;
            this.rg_rc002.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rg_rc002.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "男"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "女")});
            this.rg_rc002.Size = new System.Drawing.Size(128, 20);
            this.rg_rc002.TabIndex = 142;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(184, 96);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 12);
            this.labelControl2.TabIndex = 141;
            this.labelControl2.Text = "性别*";
            // 
            // txtEdit_rc003
            // 
            this.txtEdit_rc003.Enabled = false;
            this.txtEdit_rc003.Location = new System.Drawing.Point(92, 93);
            this.txtEdit_rc003.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEdit_rc003.Name = "txtEdit_rc003";
            this.txtEdit_rc003.Size = new System.Drawing.Size(67, 18);
            this.txtEdit_rc003.TabIndex = 140;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 96);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 12);
            this.labelControl1.TabIndex = 139;
            this.labelControl1.Text = "逝者姓名*";
            // 
            // Frm_RegisterPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 350);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtEdit_rc109);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.txtEdit_rc001);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtedit_regfee);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtedit_price);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.be_position);
            this.Controls.Add(this.labelControl16);
            this.Controls.Add(this.txtEdit_rc404);
            this.Controls.Add(this.rg_rc202);
            this.Controls.Add(this.txtEdit_rc303);
            this.Controls.Add(this.txtEdit_rc004);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.rg_rc002);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtEdit_rc003);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Frm_RegisterPay";
            this.Text = "寄存缴费";
            this.Load += new System.EventHandler(this.Frm_RegisterPay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_regfee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc404.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc202.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc303.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc004.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc002.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton b_exit;
        private DevExpress.XtraEditors.SimpleButton b_ok;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc109;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc001;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtedit_regfee;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtedit_price;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ButtonEdit be_position;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc404;
        private DevExpress.XtraEditors.RadioGroup rg_rc202;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc303;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc004;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup rg_rc002;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEdit_rc003;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}