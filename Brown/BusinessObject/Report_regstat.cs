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
using DevExpress.XtraPrinting;

namespace Brown.BusinessObject
{
    public partial class Report_regstat : BaseBusiness
    {
        DataTable dt_regstat = new DataTable();
        OracleDataAdapter regAdapter = new OracleDataAdapter("select * from v_register_stat where to_char(rc140,'yyyy-mm-dd') between :begin and :end", SqlAssist.conn);

        OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
        OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);

        public Report_regstat()
        {
            InitializeComponent();
			op_end.Direction = ParameterDirection.Input;
			op_begin.Direction = ParameterDirection.Input;
			regAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			gridControl1.DataSource = dt_regstat;
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        private void DoSearch()
        {
            Frm_duration frm_1 = new Frm_duration();
			frm_1.swapdata["MODE"] = "0";
			string s_begin = string.Empty;
			string s_end = string.Empty;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				 
				if (frm_1.swapdata["begin"] == null || frm_1.swapdata["begin"] is System.DBNull)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_1.swapdata["begin"]).ToString("yyyy-MM-dd");
				}

				if (frm_1.swapdata["end"] == null || frm_1.swapdata["end"] is System.DBNull)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_1.swapdata["end"]).ToString("yyyy-MM-dd");
				}

				op_begin.Value = s_begin;
				op_end.Value = s_end;

				this.RefreshData();
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_regstat.Rows.Clear();
			regAdapter.Fill(dt_regstat);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.DoSearch();
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
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
	}
}
