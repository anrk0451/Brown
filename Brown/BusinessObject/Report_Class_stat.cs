using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Brown.BaseObject;
using Oracle.ManagedDataAccess.Client;
using Brown.Forms;
using Brown.Action;
using DevExpress.XtraPrinting;
using Brown.Report;
using DevExpress.XtraReports.UI;

namespace Brown.BusinessObject
{
    public partial class Report_Class_stat : BaseBusiness
    {
        private DataTable dt_cs = new DataTable();
        private OracleDataAdapter csAdapter =
            new OracleDataAdapter("select * from v_classStat order by cs01", SqlAssist.conn);

        private string s_begin = string.Empty;
        private string s_end = string.Empty;
        private string[] classArry;


        public Report_Class_stat()
        {
            InitializeComponent();
            gridControl1.DataSource = dt_cs;
        }
		
		/// <summary>
		/// 统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			Frm_Report_ClassStat frm_stat = new Frm_Report_ClassStat();
 

			if (frm_stat.ShowDialog() == DialogResult.OK)
			{
				classArry = frm_stat.swapdata["class"] as string[];

				if (frm_stat.swapdata["dbegin"] == null || frm_stat.swapdata["dbegin"] is System.DBNull)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_stat.swapdata["dbegin"]).ToString("yyyy-MM-dd");
				}

				if (frm_stat.swapdata["dend"] == null || frm_stat.swapdata["dend"] is System.DBNull)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_stat.swapdata["dend"]).ToString("yyyy-MM-dd");
				}

				this.RefreshData();
			}
			frm_stat.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			int re = MiscAction.ClassStat(s_begin, s_end, classArry);
			if (re > 0)
			{
				gridView1.BeginUpdate();
				dt_cs.Rows.Clear();
				csAdapter.Fill(dt_cs);

				gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn3.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				bs_bs.Caption = "           共有收费笔数:" + MiscAction.GetClassStat_BS().ToString() + "笔";

				gridView1.EndUpdate();

			}
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 导出excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Value;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		 
		/// <summary>
		/// 打印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Class_stat_Report report = new Class_stat_Report();
			report.DataSource = dt_cs;

			report.Parameters[0].Value = s_begin;
			report.Parameters[1].Value = s_end;
			report.RequestParameters = false;    //禁止显示参数确认窗口

			using (ReportPrintTool printTool = new ReportPrintTool(report))
			{
				printTool.PrintDialog();
			}
		}
	}
}
