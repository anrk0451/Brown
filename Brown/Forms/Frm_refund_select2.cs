using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Brown.BaseObject;
using Oracle.ManagedDataAccess.Client;
using Brown.Action;
using Brown.Domain;
using Brown.Misc;
using System.Threading;

namespace Brown.Forms
{
    public partial class Frm_refund_select2 : BaseDialog
    {
        DataTable dt_sales = new DataTable();
        OracleParameter op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
        OracleParameter op_sa020 = new OracleParameter("sa020", OracleDbType.Varchar2, 3);
        string s_sa010 = string.Empty;
        string s_sa020 = string.Empty;
        OracleDataAdapter salesAdapter =
            new OracleDataAdapter("select * from v_refund_select where sa010 = :fa001 and sa020 like :sa020", SqlAssist.conn);


        public Frm_refund_select2()
        {
            InitializeComponent();
        }

        private void Frm_refund_select2_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_sales;
            s_sa010 = this.swapdata["SA010"].ToString();
            s_sa020 = this.swapdata["SA020"].ToString();
            op_sa010.Direction = ParameterDirection.Input;
            op_sa020.Direction = ParameterDirection.Input;

            op_sa010.Value = s_sa010;
            op_sa020.Value = s_sa020;

            salesAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010, op_sa020 });
            salesAdapter.Fill(dt_sales);

            gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn4.SummaryItem.DisplayFormat = "合计 = {0:N2}";
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            decimal dec_num = decimal.Zero;
            int rowHandle = gridView1.FocusedRowHandle;
            if (decimal.TryParse(e.Value.ToString(), out dec_num))
            {
                if (dec_num > Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NUMS")))
                {
                    e.Valid = false;
                    e.ErrorText = "退费金额不能大于原收费金额!";
                    return;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!gridView1.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow()) return;

            List<string> itemIdList = new List<string>();
            List<decimal> numsList = new List<decimal>();
            List<decimal> priceList = new List<decimal>();
            List<string> itemTypeList = new List<string>();

            decimal dec_refund_fee = decimal.Zero;
            decimal dec_tax_sum = decimal.Zero;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (decimal.TryParse(gridView1.GetRowCellValue(i, "RFEE").ToString(), out dec_refund_fee))
                {
                    if (dec_refund_fee <= 0) continue;
                    itemIdList.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
                    priceList.Add(Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")));
                    numsList.Add(0 - Convert.ToDecimal(gridView1.GetRowCellValue(i,"REFUNDNUM")));
                    itemTypeList.Add(gridView1.GetRowCellValue(i, "SA002").ToString());
                    dec_tax_sum += Convert.ToDecimal(gridView1.GetRowCellValue(i, "RFEE"));
                }
            }
            if (numsList.Count <= 0)
            {
                XtraMessageBox.Show("还未选择退费的项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string s_fa001 = Tools.GetEntityPK("FA01");
            string s_memo = te_memo.Text;
 
            try
            {
                int re = MiscAction.TaxRefundSettle(s_fa001, itemIdList.ToArray(), itemTypeList.ToArray(), priceList.ToArray(), numsList.ToArray(), Envior.cur_userId, s_memo, s_sa010);

                if (re > 0)
                {
                    XtraMessageBox.Show("退费结算完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string s_cuname = SqlAssist.ExecuteScalar("select fa003 from fa01 where fa001='" + s_sa010 + "'").ToString();
                    //获取税务客户信息
                    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_cuname,s_fa001);
                    if (frm_taxClient.ShowDialog() == DialogResult.OK)
                    {
                        TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;
                        
                        if (TaxInvoice2023.GetNextInvoiceNo() > 0)
                        {
                            if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (TaxInvoice2023.Refund(s_fa001) > 0)
                                {
                                    XtraMessageBox.Show("税务发票请求提交成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bool b_exit = false;
                                    int i_count = 0;
                                    this.Cursor = Cursors.WaitCursor;
                                    while (!b_exit && i_count < 8)
                                    {
                                        i_count++;
                                        if (TaxInvoice2023.QueryInvoice(s_fa001) > 0 && TaxInvoice2023.getInvoiceStatus(s_fa001) == "2")
                                        {
                                            b_exit = true;
                                        }
                                        Thread.Sleep(600 * (i_count - 1));
                                    }
                                    this.Cursor = Cursors.Arrow;


                                    if (TaxInvoice2023.getInvoiceStatus(s_fa001) == "2")
                                    {
                                        string s_data = TaxInvoice2023.getInvoiceNo(s_fa001);
                                        string s_code = s_data.Substring(0, s_data.Length - 8);
                                        string s_no = s_data.Substring(s_data.Length - 8, 8);
                                        XtraMessageBox.Show("发票开具成功!\r\n" + "税票代码:" + s_code + "\r\n" + "票号:" + s_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        if (XtraMessageBox.Show("现在打印税务发票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            string printId = TaxInvoice2023.getInvoicePrintId(s_fa001);
                                            if (!string.IsNullOrEmpty(printId))
                                            {
                                                //打印发票
                                                TaxInvoice2023.print_Invoice(printId, "2");
                                                //打印清单//////
                                                if (TaxInvoice2023.getTaxItems(s_fa001) > AppInfo.TAXITEMCOUNT && XtraMessageBox.Show("现在打印【税务发票清单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    TaxInvoice2023.print_Invoice(printId, "3");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("未查询到发票状态,请联系管理员或稍候再次查询!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("请联系管理员或稍后进行补开发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 全部退费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "REFUNDNUM", gridView1.GetRowCellValue(i, "NUMS"));
            }
        }
    }
}