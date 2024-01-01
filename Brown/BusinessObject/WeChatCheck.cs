using Brown.Action;
using Brown.BaseObject;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.BusinessObject
{
    public partial class WeChatCheck : BaseBusiness
    {
        private DataTable dt_check = new DataTable();
        private OracleDataAdapter checkAdapter = new OracleDataAdapter("select * from v_wechat_check order by orderdate desc ", SqlAssist.conn);

        public WeChatCheck()
        {
            InitializeComponent();
            gridView1.CustomDrawRowIndicator += MiscAction.DrawGridLineNo;
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fname = string.Empty;
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = xtraOpenFileDialog1.FileName;
                this.readWeChatOrder(fname);
            }
        }


        /// <summary>
        /// 读入对账文件
        /// </summary>
        /// <param name="fname"></param>
        private void readWeChatOrder(string fname)
        {
            int i_skip = 1;
            string[] sdata = new string[6];
            StreamReader reader = new StreamReader(fname, Encoding.Default);
            string line = reader.ReadLine();
            List<string> wxpayId = new List<string>();          //微信支付id
            List<string> orderId = new List<string>();          //商户ID
            List<DateTime> orderDate = new List<DateTime>();  //交易日期
            List<string> scene = new List<string>();            //交易场景
            List<string> state = new List<string>();            //交易状态
            List<decimal> fee = new List<decimal>();            //交易金额 

            while (line != null)
            {
                try
                {
                    if (i_skip >= 6)
                    {
                        sdata = line.Split(',');
                        wxpayId.Add(sdata[1].Substring(1));
                        orderId.Add(sdata[2].Substring(1));
                        orderDate.Add(DateTime.Parse(sdata[0], CultureInfo.InvariantCulture));
                        scene.Add(sdata[3]);
                        state.Add(sdata[4]);
                        fee.Add(decimal.Parse(sdata[5]));
                    }
                }
                catch (Exception ee)
                {
                    XtraMessageBox.Show("读取数据错误!" + ee.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                line = reader.ReadLine();
                i_skip++;
            }

            if (MiscAction.genWechatCheckData(wxpayId.ToArray(), orderId.ToArray(), orderDate.ToArray(), scene.ToArray(), state.ToArray(), fee.ToArray()) > 0)
            {
                dt_check.Rows.Clear();
                checkAdapter.Fill(dt_check);

                gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn6.SummaryItem.DisplayFormat = "{0:N2}";
            }
        }
        /// <summary>
        /// 到账确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string orderId = string.Empty;
            string transactionId = string.Empty;
            DateTime dt_paydate;
            decimal fee = decimal.Zero;
            if (rowHandle >= 0 && XtraMessageBox.Show("要对当前记录做到账确认?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (gridView1.GetRowCellValue(rowHandle, "STATUS").ToString() == "2")
                {
                    XtraMessageBox.Show("该支付记录已经到账确认!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                orderId = gridView1.GetRowCellValue(rowHandle, "ORDERID").ToString();
                fee = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "FEE"));
                transactionId = gridView1.GetRowCellValue(rowHandle, "WXPAYID").ToString();
                dt_paydate = Convert.ToDateTime(gridView1.GetRowCellValue(rowHandle, "PAYDATE").ToString());

                if (dt_paydate.ToString("yyyy-MM-dd").CompareTo("2023-02-19") < 0)
                {
                    XtraMessageBox.Show("系统割接前数据,不能确认!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (RegisterAction.wechatConfirm(orderId, fee, transactionId, dt_paydate) > 0)
                {
                    XtraMessageBox.Show("操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Refresh_Data();
                    return;
                }
            }
        }

        /// <summary>
        /// 代码转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            if (e.Column.FieldName == "STATUS")
            {
                if (e.Value.ToString() == "1")
                    e.DisplayText = "未确认";
                else if (e.Value.ToString() == "2")
                    e.DisplayText = "已确认";
            }
        }

        private void WeChatCheck_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_check;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.IsFindPanelVisible)
                gridView1.HideFindPanel();
            else
                gridView1.ShowFindPanel();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = TextExportMode.Value;
                gridControl1.ExportToXlsx(fileDialog.FileName, options);
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Refresh_Data();
        }

        private void Refresh_Data()
        {
            gridView1.BeginUpdate();
            dt_check.Rows.Clear();
            checkAdapter.Fill(dt_check);
            gridView1.EndUpdate();
        }

        /// <summary>
        /// 全部确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gridView1.LocateByValue("STATUS", "1");
            if (row < 0)
            {
                XtraMessageBox.Show("所有支付记录都已经到账确认!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (XtraMessageBox.Show("要对所有未确认的支付记录做到账确认?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string orderId = string.Empty;
            string transactionId = string.Empty;
            DateTime dt_paydate;
            decimal fee = decimal.Zero;

            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "STATUS").ToString() == "2")  //忽略已经确认的记录
                    continue;

                orderId = gridView1.GetRowCellValue(i, "ORDERID").ToString();
                fee = Convert.ToDecimal(gridView1.GetRowCellValue(i, "FEE"));
                transactionId = gridView1.GetRowCellValue(i, "WXPAYID").ToString();
                dt_paydate = Convert.ToDateTime(gridView1.GetRowCellValue(i, "PAYDATE").ToString());

                //割接前数据
                if (dt_paydate.ToString("yyyy-MM-dd").CompareTo("2023-02-19") < 0)
                    continue;

                if (RegisterAction.wechatConfirm(orderId, fee, transactionId, dt_paydate) < 0)
                {
                    if (XtraMessageBox.Show("是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Refresh_Data();
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            this.Cursor = Cursors.Arrow;
            XtraMessageBox.Show("操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Refresh_Data();
        }
    }
}
