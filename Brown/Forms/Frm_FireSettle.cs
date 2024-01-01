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
using Brown.BusinessObject;
using Brown.DataSet;
using Brown.Action;
using Brown.Misc;
using Brown.Domain;
using System.Threading;

namespace Brown.Forms
{
    public partial class Frm_FireSettle : BaseDialog
    {
        FireBusiness_ds business_ds = null;
        string AC001 = string.Empty;
        List<int> rowList;
        DataTable dt_source;


        public Frm_FireSettle()
        {
            InitializeComponent();
        }

        private void Frm_FireSettle_Load(object sender, EventArgs e)
        {
            AC001 = this.swapdata["AC001"].ToString();
            business_ds = this.swapdata["dataset"] as FireBusiness_ds;
            rowList = this.swapdata["rowList"] as List<int>;


            ///拷贝要结算的记录!!!
            dt_source = business_ds.Sa01.Clone();
            foreach (int i in rowList)
            {
                dt_source.Rows.Add(business_ds.Sa01.Rows[i].ItemArray);
            }

            gridControl1.DataSource = dt_source;
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

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            decimal dec_fin = new decimal(0);
            decimal dec_tax = new decimal(0);
            string s_tip = string.Empty;

            foreach(DataRow dr in dt_source.Rows)
            {
                if (dr["SA020"].ToString() == "F")
                    dec_fin += Convert.ToDecimal(dr["SA007"]);
                else if (dr["SA020"].ToString() == "T")
                    dec_tax += Convert.ToDecimal(dr["SA007"]);
            }

            if (dec_fin > 0 && dec_tax > 0)
                s_tip = "本次结算共需要一张财政发票和一张税务发票,是否继续?";
            else if (dec_fin > 0)
                s_tip = "本次结算共需要一张财政发票,是否继续?";
            else if (dec_tax > 0)
                s_tip = "本次结算共需要一张税务发票,是否继续?";
            else
                return;

            if (XtraMessageBox.Show(s_tip, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string s_fa001 = Tools.GetEntityPK("FA01");
            string s_cuname = FireAction.GetGuyNameById(AC001);
            List<string> sa001_list = new List<string>();
            foreach (DataRow r in dt_source.Rows)
            {
                sa001_list.Add(r["SA001"].ToString());
            }

            int result = FireAction.FireBusinessSettle(s_fa001,
                                                       AC001,
                                                       s_cuname,
                                                       sa001_list.ToArray(),
                                                       Envior.cur_userId 
            );

            if (result > 0)
            {
                b_ok.Enabled = false;

                int fire_row = gridView1.LocateByValue("SA002", "06");
                //如果有火化,打印火化证明
                if (fire_row >= 0)
                {   //打印火化证明
                    if (XtraMessageBox.Show("现在打印火化证明?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        PrtServAction.Print_HHZM(AC001,0);
                }

                XtraMessageBox.Show("结算成功!现在开始开具发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////开财政票!
                if (dec_fin > 0)
                {
                    //string s_pjh = string.Empty;
                    //string s_zch = string.Empty;
                    //if (FinInvoice.GetCurrentPh() > 0)
                    //{
                    //    if (XtraMessageBox.Show("下一张财政发票号码:" + Envior.FIN_NEXT_BILL_NO + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {
                    //        FinInvoice.Invoice(s_fa001);
                    //    }
                    //}
                    if(FinInvoice.InvoiceElec(s_fa001) > 0)
					{
                        if(XtraMessageBox.Show("现在打印财政电子票吗?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
						{
                            Frm_FinInvoice frm_1 = new Frm_FinInvoice();
                            frm_1.swapdata["FA001"] = s_fa001;
                            frm_1.ShowDialog();
                            frm_1.Dispose();
                        }
					}
                }

                //// 开税票
                if (dec_tax > 0)
                {
                    //获取税务客户信息
                    string s_ac003 = SqlAssist.ExecuteScalar("select ac003 from ac01 where ac001='" + AC001 + "'").ToString();
                    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_ac003,s_fa001);
                    if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                    TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                    if (TaxInvoice2023.GetNextInvoiceNo() > 0) 
                    {
                        if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if(TaxInvoice2023.Invoice(s_fa001) > 0)
                            {
                                XtraMessageBox.Show("税务发票请求提交成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                ////查询发票开具状态//////
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
                                    XtraMessageBox.Show("发票开具成功!\r\n" + "税票代码:" + s_code + "\r\n" + "票号:" + s_no, "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                    if(XtraMessageBox.Show("现在打印税务发票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        string printId = TaxInvoice2023.getInvoicePrintId(s_fa001);
                                        if (!string.IsNullOrEmpty(printId))
                                        {
                                            //打印发票
                                            TaxInvoice2023.print_Invoice(printId, "2");
                                            //打印清单//////
                                            if(TaxInvoice2023.getTaxItems(s_fa001) > AppInfo.TAXITEMCOUNT && XtraMessageBox.Show("现在打印【税务发票清单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                TaxInvoice2023.print_Invoice(printId, "3");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("未查询到发票状态或发票未正确开具,请联系管理员或稍候再次查询!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("请联系管理员或稍后进行补开发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
        }
    }
}