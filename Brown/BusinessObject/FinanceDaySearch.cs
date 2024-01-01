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
using Brown.Misc;
using Brown.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Brown.Action;
using Brown.Domain;
using System.Web.UI.WebControls;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using System.Threading;

namespace Brown.BusinessObject
{
    public partial class FinanceDaySearch : BaseBusiness
    {
		private DataTable dt_finance = new DataTable("FINANCE");
		private DataTable dt_invoice = new DataTable();

		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeDay where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa003 like :fa003 and fa100 like :fa100 ", SqlAssist.conn);

		private OracleDataAdapter invAdapter =
			new OracleDataAdapter("select * from v_financeDay_invoices where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa003 like :fa003 and fa100 like :fa100 order by fa001 desc ", SqlAssist.conn);


		private DataTable dt_detail = new DataTable("DETAIL");
		private OracleDataAdapter deAdapter =
			new OracleDataAdapter("select * from v_findetail where sa010 = :sa010", SqlAssist.conn);

		private DataTable dt_detail2 = new DataTable("DETAIL2");
		private OracleDataAdapter deAdapter2 =
			new OracleDataAdapter("select * from v_findetail where sa010 = :sa010 and sa020 = :sa020", SqlAssist.conn);


		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_fa003 = null;
		OracleParameter op_fa100 = null;

		OracleParameter op_begin_2 = null;
		OracleParameter op_end_2 = null;
		OracleParameter op_fa003_2 = null;
		OracleParameter op_fa100_2 = null;

		OracleParameter op_sa010 = null;
		OracleParameter op_sa010_2 = null;
		OracleParameter op_sa020 = null;
		 
		public FinanceDaySearch()
        {
            InitializeComponent();
        }

		private void FinanceDaySearch_Load(object sender, EventArgs e)
		{
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_fa003 = new OracleParameter("fa003", OracleDbType.Varchar2, 80);
			op_fa003.Direction = ParameterDirection.Input;

 
			op_fa100 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);
			op_fa100.Direction = ParameterDirection.Input;

			op_begin_2 = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin_2.Direction = ParameterDirection.Input;

			op_end_2 = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end_2.Direction = ParameterDirection.Input;

			op_fa003_2 = new OracleParameter("fa003", OracleDbType.Varchar2, 80);
			op_fa003_2.Direction = ParameterDirection.Input;


			op_fa100_2 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);
			op_fa100_2.Direction = ParameterDirection.Input;

			 
			op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;

			op_sa010_2 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010_2.Direction = ParameterDirection.Input;

			op_sa020 = new OracleParameter("sa020", OracleDbType.Varchar2, 3);
			op_sa020.Direction = ParameterDirection.Input;

			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_fa003,op_fa100  });
			invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin_2, op_end_2, op_fa003_2, op_fa100_2 });
			deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });
			deAdapter2.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010_2,op_sa020 });

			gridControl1.DataSource = dt_finance;
			gridControl2.DataSource = dt_detail;
			gridControl3.DataSource = dt_invoice;
			gridControl4.DataSource = dt_detail2;

			gridControl1.Visible = true;
		}


		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;

			gridView1.BeginUpdate();
			dt_finance.Rows.Clear();
			finAdapter.Fill(dt_finance);
			gridView1.EndUpdate();

			gridView3.BeginUpdate();
			dt_invoice.Rows.Clear();
			invAdapter.Fill(dt_invoice);
			gridView3.EndUpdate();


			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 显示查询条件
		/// </summary>
		private void Show_Condition()
		{
			Frm_financeDaySearch frm_1 = new Frm_financeDaySearch();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_fa003 = string.Empty;
				string s_fa100 = string.Empty; 

				if (frm_1.swapdata["dbegin"] == null)
				{
					s_begin = "1900/01/01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_1.swapdata["dbegin"]).ToString("yyyy/MM/dd");
				}

				if (frm_1.swapdata["dend"] == null)
				{
					s_end = "9999/12/31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_1.swapdata["dend"]).ToString("yyyy/MM/dd");
				}

				if (frm_1.swapdata["FA003"] == null || string.IsNullOrEmpty(frm_1.swapdata["FA003"].ToString()))
				{
					s_fa003 = "%";
				}
				else
				{
					s_fa003 = frm_1.swapdata["FA003"].ToString() + "%";
				}

				if(frm_1.swapdata["FA100"] == null)
				{
					s_fa100 = "%";
				}
				else
				{
					s_fa100 = frm_1.swapdata["FA100"].ToString();
				}


				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_fa003.Value = s_fa003;
				op_fa100.Value = s_fa100;

				op_begin_2.Value = s_begin;
				op_end_2.Value = s_end;
				op_fa003_2.Value = s_fa003;
				op_fa100_2.Value = s_fa100;

				this.Cursor = Cursors.WaitCursor;
				 

				//////1.按收费笔数检索
				gridView1.BeginUpdate();
				dt_finance.Rows.Clear();

				finAdapter.Fill(dt_finance);

				gridCol_Fa004.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridCol_Fa004.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn5.SummaryItem.DisplayFormat = "共计 = {0:N0}笔";

				gridView1.EndUpdate();


				//////2. 按发票检索
				gridView3.BeginUpdate();
				dt_invoice.Rows.Clear();

				invAdapter.Fill(dt_invoice);

				gridColumn_fee.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn_fee.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn_ph.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn_ph.SummaryItem.DisplayFormat = "共计 = {0:N0}张发票";

				gridView3.EndUpdate();
 
				this.Cursor = Cursors.Arrow;
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 输入查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Show_Condition();
			OnlyMe_Filter();
		}

		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if(tabPane1.SelectedPageIndex == 0)
			{
				if (!gridView1.IsFindPanelVisible)
					gridView1.ShowFindPanel();
				else
					gridView1.HideFindPanel();
			}
			else if (tabPane1.SelectedPageIndex == 1)
			{
				if (!gridView3.IsFindPanelVisible)
					gridView3.ShowFindPanel();
				else
					gridView3.HideFindPanel();
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

		/// <summary>
		/// 行焦点改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0 )
			{
				this.RetrieveDetail(e.FocusedRowHandle);
			}
		}

		/// <summary>
		/// 检索明细
		/// </summary>
		/// <param name="rowHandle"></param>
		private void RetrieveDetail(int rowHandle)
		{
			if (rowHandle >= 0)
			{
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				op_sa010.Value = s_fa001;
				gridView2.BeginUpdate();
				dt_detail.Rows.Clear();
				deAdapter.Fill(dt_detail);
				gridView2.EndUpdate();
			}
		}

		 
		/// <summary>
		/// 标签页改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
		{
			if (tabPane1.SelectedPageIndex == 0)
			{
				bBi_remove.Enabled = true;
				bBi_refund.Enabled = false;
				bBi_bk.Enabled = true;
				bBi_log.Enabled = true;
				bsm_invlog.Enabled = true;
			}
			else
			{
				bBi_remove.Enabled = false;
				bBi_refund.Enabled = true;
				bBi_bk.Enabled = false;
				bBi_log.Enabled = false;
				bsm_invlog.Enabled = false;
			}
				
		}

		/// <summary>
		/// 文本转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if(e.Column.FieldName.ToUpper() == "FA190")
			{
				if (e.Value.ToString() == "00")
					e.DisplayText = "未开票";
				else if (e.Value.ToString() == "01")
					e.DisplayText = "税票";
				else if (e.Value.ToString() == "10")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "11")
					e.DisplayText = "财政票+税票";
				else if (e.Value.ToString() == "1w")
					e.DisplayText = "财政票+税票开票中";
				else if (e.Value.ToString() == "0w")
					e.DisplayText = "税票开票中";
			}
			else if(e.Column.FieldName.ToUpper() == "FA195")
			{
				if (e.Value.ToString() == "00")
					e.DisplayText = "";
				else if (e.Value.ToString() == "01")
					e.DisplayText = "税票";
				else if (e.Value.ToString() == "10")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "11")
					e.DisplayText = "财政票+税票";
			}
		}

		 

		/// <summary>
		/// 显示发票信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			try
			{
				if (e.Button == MouseButtons.Left && e.Clicks == 1)
				{
					//判断光标是否在行范围内  
					if (hInfo.InRow && hInfo.Column != null && hInfo.Column.FieldName.ToUpper() == "FA190" && gridView1.GetRowCellValue(hInfo.RowHandle, "FA190").ToString() != "00")
					{						 
						string s_fa001 = gridView1.GetRowCellValue(hInfo.RowHandle, "FA001").ToString();
						Frm_InvoiceInfo frm_1 = new Frm_InvoiceInfo();
						frm_1.swapdata["FA001"] = s_fa001;
						frm_1.ShowDialog();
						frm_1.Dispose();						
					}
				}
			}catch(Exception ee)
			{
				XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
				LogUtils.Error(ee.ToString());
			}
			
		}

		/// <summary>
		/// 显示文本转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if(e.Column.FieldName.ToUpper() == "BILLTYPE")
			{
				if (e.Value.ToString() == "F")
					e.DisplayText = "财政票";
				else if (e.Value.ToString() == "T")
					e.DisplayText = "税票";
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
		/// 发票 行焦点改变事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0 )
			{
				string s_fa001 = gridView3.GetRowCellValue(e.FocusedRowHandle, "FA001").ToString();
				string s_sa020 = gridView3.GetRowCellValue(e.FocusedRowHandle, "BILLTYPE").ToString();

				op_sa010_2.Value = s_fa001;
				op_sa020.Value = s_sa020;

				gridView3.BeginUpdate();
				dt_detail2.Rows.Clear();
				deAdapter2.Fill(dt_detail2);
				gridView3.EndUpdate();
			}
		}

		/// <summary>
		/// 收款作废
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bBi_remove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				if (tabPane1.SelectedPageIndex == 0)  //按笔数显示
				{
					int rowHandle = gridView1.FocusedRowHandle;
					if (rowHandle >= 0)
					{						 
						string s_handler = gridView1.GetRowCellValue(rowHandle, "FA100").ToString();
						string s_tip = string.Empty;

						if (!AppAction.CheckRight("收费作废", s_handler)) return;

						string s_reason = string.Empty;
						string s_rc001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
						string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
						string s_fa190 = gridView1.GetRowCellValue(rowHandle, "FA190").ToString();
						 
						if (XtraMessageBox.Show("确认要作废吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

						//检查与开票所在工作站是否一致!!!
						//if (MiscAction.CheckWorkStationCompare(s_fa001,Envior.WORKSTATIONID) == "0")
						//{
						//	XtraMessageBox.Show("此笔收费发票不是在当前工作站开具,不能继续!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						//	return;
						//}

						////如果开具财政发票,且在新接口上线前
						//if (s_fa190.Substring(0, 1) == "1" && MiscAction.FinRefundBeforeOnline(s_fa001))
						//{
						//	XtraMessageBox.Show("此笔收费在财政电子发票上线前开具,不能在此作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						//	return;
						//}
						//if(s_fa190.Substring(1,1) == "1" && MiscAction.TaxInvoiceCanCanceled(s_fa001) == 0)
						//{
						//	XtraMessageBox.Show("此笔收费包换税务发票,税务发票只能作废当月!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						//	return;
						//}

						if(s_fa190 != "00")
                        {
							XtraMessageBox.Show("该笔收费已经开票,不能作废!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
							return;
                        }

						if (gridView1.GetRowCellValue(rowHandle, "FA002").ToString() == "2")  //寄存业务
						{
							decimal count = (decimal)SqlAssist.ExecuteScalar("select count(*) from v_rc04 where rc001='" + s_rc001 + "'", null);
							if (count <= 1)
							{
								if (XtraMessageBox.Show("此记录是唯一一次交费记录,作废此记录将删除寄存登记信息,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
							}
						}
						 
						//if (s_fa190 == "10")
						//	s_tip = "此笔收费包含一张财政发票,作废该笔业务将会开具一张红字发票!";
						//else if (s_fa190 == "11")
						//	s_tip = "此笔收费包含一张税务发票和财政发票,作废该笔业务将作废税务发票并再开具一张财政红字电子票!";

						//if (!string.IsNullOrEmpty(s_tip)) XtraMessageBox.Show(s_tip ,"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
						 
						Frm_RemoveFinReason frm_reason = new Frm_RemoveFinReason();
						if (frm_reason.ShowDialog() == DialogResult.OK)
						{
							s_reason = frm_reason.swapdata["reason"].ToString();
						}
						frm_reason.Dispose();
						 
						int re = MiscAction.FinanceRemove(s_fa001, s_reason, Envior.cur_userId);

						///作废成功,开始作废发票
						if (re > 0)
						{
							this.RefreshData();
							XtraMessageBox.Show("操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
							//if (s_fa190.Substring(0, 1) == "1")
       //                     {
							//	if(XtraMessageBox.Show("现在开具财政发票!","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
       //                         {
							//		///检索退费 结算流水号
							//		string s_refund_fa001 = SqlAssist.ExecuteScalar("select rf001 from refund where rf300 ='" + s_fa001 + "'").ToString();
							//		if (!string.IsNullOrEmpty(s_refund_fa001))
							//		{
							//			if (FinInvoice.InvoiceElec_Refund(s_refund_fa001) > 0)
       //                                 {
							//				if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							//				{
							//					Frm_FinInvoice frm_1 = new Frm_FinInvoice();
							//					frm_1.swapdata["FA001"] = s_refund_fa001;
							//					frm_1.ShowDialog();
							//					frm_1.Dispose();
							//				}
							//			}
							//		}
       //                             else
       //                             {
							//			XtraMessageBox.Show("未找到退费结算记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //                             }									
       //                         }
       //                     }

							/////作废财政发票
							//if (s_fa190.Substring(0, 1) == "1")
							//{
							//	//如果不在新接口上线前,则可以作废发票
       //                         if (!MiscAction.FinRefundBeforeOnline(s_fa001))
       //                         {
							//		if (FinInvoice.InvoiceRemoved(s_fa001) > 0)
							//		{
							//			MiscAction.FinRemove_log(s_fa001, Envior.cur_userName, s_reason);
							//		}
							//	}
							//	else
							//	{
							//		XtraMessageBox.Show("此笔收费为新财政发票接口上线前开具,发票需要在系统外进行作废!详情请与管理员联系!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
							//	}								 
       //                     }///作废税务发票
							//if (s_fa190.Substring(1, 1) == "1"  )
							//{
							//	string s_tax_fa001 = string.Empty;
							//	if (s_fa190.Substring(0, 1) == "1")
							//		s_tax_fa001 = SqlAssist.ExecuteScalar("select fa999 from fa01_splog where fa001 ='" + s_fa001 + "'").ToString();
							//	else
							//		s_tax_fa001 = s_fa001;
								
       //                         if (!string.IsNullOrEmpty(s_tax_fa001))
       //                         {
							//		if (TaxInvoice.Remove(s_tax_fa001, Envior.cur_userName) > 0) //发票作废成功
							//		{
							//			//修改发票作废日志
							//			MiscAction.TaxRemove_log(s_tax_fa001, Envior.cur_userName, s_reason);
							//		}
							//		else
							//		{
							//			XtraMessageBox.Show("未能作废税务发票,请在【税神通】中作废指定票据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
							//		}
							//	}
       //                         else
       //                         {
							//		XtraMessageBox.Show("未找到税务收费记录!","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
       //                         }
							//}
						}

					}
				}
				else   //发票作废 !!!!
				{
					//int rowHandle = gridView1.FocusedRowHandle;
					//string s_fa001 = gridView3.GetRowCellValue(rowHandle, "FA001").ToString();
					//string s_billType = gridView3.GetRowCellValue(rowHandle, "BILLTYPE").ToString();
					//if(s_billType == "F")        //财政发票作废
					//{
						
					//}
					//else if(s_billType == "T")
					//{

					//}
				}				
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.RefreshData();
			this.OnlyMe_Filter();
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 补开发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("补开发票")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString() == gridView1.GetRowCellValue(rowHandle, "FA195").ToString())
			{
				XtraMessageBox.Show("此笔收费记录已经开具发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}

			string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
			string s_cuname = gridView1.GetRowCellValue(rowHandle, "FA003").ToString();

			//如果办理过退费,不能再开具发票了 
			if (MiscAction.HaveRefund(s_fa001))
			{
				XtraMessageBox.Show("此笔收费记录办理过退费,不能再开具发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//如果收费不是本工作站,提示
			if (!string.IsNullOrEmpty(gridView1.GetRowCellValue(rowHandle, "WS001").ToString()))
			{
				if(!Envior.WORKSTATIONID.Equals(gridView1.GetRowCellValue(rowHandle, "WS001").ToString()))
				{
					if (XtraMessageBox.Show("此收费记录结算并非当前工作站,是否继续?", "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No) return;
				}
			}
			 
			//TODO 负数发票补开
			if (Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "FA004")) < 0 )
			{
				//需要补开财政发票
				if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString().Substring(0,1) == "0" && gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(0, 1) == "1")
				{
					//如果是新版接口上线前开具的原发票
					if (MiscAction.FinRefundBeforeOnline(s_fa001))
					{
						XtraMessageBox.Show("原发票在财政新接口上线前开具,不能开具对应退费发票,请在财政发票系统内完成发票开具.\r\n 开具成功后请更新发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					FinInvoice.InvoiceElec_Refund(s_fa001);				 
				}
				//需要补开税务发票
				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Substring(1, 1) == "w" && gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(1, 1) == "1")
                {
					XtraMessageBox.Show("该笔收费税票已经提交请求,不能补开!请查新查询开票状态!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Substring(1, 1) == "0" && gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(1, 1) == "1")
				{
					XtraMessageBox.Show("现在开始补开【税务发票】!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					ReInvoiceTaxRefund(s_fa001);
				}
				return;
			}
			 


			//需要开具财政发票
			if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString().Substring(0,1) == "0"  &&
				gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(0, 1) == "1")
			{
				XtraMessageBox.Show("现在准备开具财政发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);				 
				FinInvoice.InvoiceElec(s_fa001);
			}

			///开税票
			if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Substring(1, 1) == "w" &&
				gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(1, 1) == "1")
            {
				XtraMessageBox.Show("该笔收费税票已经提交请求,不能补开!请查新查询开票状态!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			else if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Substring(1, 1) == "0" &&
				gridView1.GetRowCellValue(rowHandle, "FA195").ToString().Substring(1, 1) == "1")
			{
				XtraMessageBox.Show("现在准备开具税务发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//获取税务客户信息
				Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_cuname,s_fa001);
				if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
				TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;
 
				if (TaxInvoice2023.GetNextInvoiceNo() > 0)
				{
					if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (TaxInvoice2023.Invoice(s_fa001) > 0)
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

		/// <summary>
		/// 补开财政退费发票
		/// </summary>
		/// <param name="fa001"></param>
		private void ReInvoiceFinRefund(string fa001)
		{
			//如果是新版接口上线前开具的原发票
			if (MiscAction.FinRefundBeforeOnline(fa001))
			{
				XtraMessageBox.Show("原发票在财政新接口上线前开具,不能开具对应退费发票,请在财政发票系统内完成发票开具.\r\n 开具成功后请更新发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FinInvoice.InvoiceElec_Refund(fa001) > 0)
			{
				
			}			 	 			 			 
		}

		/// <summary>
		/// 补开税务退费发票
		/// </summary>
		/// <param name="fa001"></param>
		private void ReInvoiceTaxRefund(string fa001)
		{
			string s_cuname = SqlAssist.ExecuteScalar("select fa003 from fa01 where fa001='" + fa001 + "'").ToString();
			//获取税务客户信息
			Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_cuname,fa001);
			if (frm_taxClient.ShowDialog() == DialogResult.OK)
			{
				TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;				 
				if (TaxInvoice2023.GetNextInvoiceNo() > 0)
				{
					if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (TaxInvoice2023.Refund(fa001) > 0)
						{
							XtraMessageBox.Show("税务发票请求提交成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
							bool b_exit = false;
							int i_count = 0;
							this.Cursor = Cursors.WaitCursor;
							while (!b_exit && i_count < 8)
							{
								i_count++;
								if (TaxInvoice2023.QueryInvoice(fa001) > 0 && TaxInvoice2023.getInvoiceStatus(fa001) == "2")
								{
									b_exit = true;
								}
								Thread.Sleep(600 * (i_count - 1));
							}
							this.Cursor = Cursors.Arrow;


							if (TaxInvoice2023.getInvoiceStatus(fa001) == "2")
							{
								string s_data = TaxInvoice2023.getInvoiceNo(fa001);
								string s_code = s_data.Substring(0, s_data.Length - 8);
								string s_no = s_data.Substring(s_data.Length - 8, 8);
								XtraMessageBox.Show("发票开具成功!\r\n" + "税票代码:" + s_code + "\r\n" + "票号:" + s_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
								if (XtraMessageBox.Show("现在打印税务发票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									string printId = TaxInvoice2023.getInvoicePrintId(fa001);
									if (!string.IsNullOrEmpty(printId))
									{
										//打印发票
										TaxInvoice2023.print_Invoice(printId, "2");
										//打印清单//////
										if (TaxInvoice2023.getTaxItems(fa001) > AppInfo.TAXITEMCOUNT && XtraMessageBox.Show("现在打印【税务发票清单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

		/// <summary>
		/// 只看自己
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toggle_onlyme_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.OnlyMe_Filter();
		}


		private void OnlyMe_Filter()
		{
			if (toggle_onlyme.Checked && toggle_wechat.Checked)			//只显示自己+显示微信
			{
				gridView1.ActiveFilter.Clear();
				gridView3.ActiveFilter.Clear();
				gridView1.ActiveFilterString = "FA001 like 'wx%'";
				gridView3.ActiveFilterString = "FA001 like 'wx%'";
			}
			else if (toggle_onlyme.Checked && !toggle_wechat.Checked)	//只显示自己 + 不显示微信
			{
				gridView1.ActiveFilter.Clear();
				gridView3.ActiveFilter.Clear();
				gridView1.ActiveFilterString = "FA100 = '" + Envior.cur_userId + "' and FA001 not like 'wx%'";
				gridView3.ActiveFilterString = "FA100 = '" + Envior.cur_userId + "' and FA001 not like 'wx%'";
			}
			else if (!toggle_onlyme.Checked && toggle_wechat.Checked)   //显示所有 + 显示微信
			{
				gridView1.ActiveFilter.Clear();
				gridView3.ActiveFilter.Clear();
				gridView1.ActiveFilterString = "FA001 like 'wx%'";
				gridView3.ActiveFilterString = "FA001 like 'wx%'";
			}
			else                                                        //显示所有 + 不显示微信
			{
				gridView1.ActiveFilter.Clear();
				gridView3.ActiveFilter.Clear();
				gridView1.ActiveFilterString = "FA001 not like 'wx%'";
				gridView3.ActiveFilterString = "FA001 not like 'wx%'";
			}
		}

		 
		/// <summary>
		/// 打印税务发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("补打发票")) return;

			string s_fa001 = string.Empty;
			int rowHandle;
			if(tabPane1.SelectedPageIndex == 0)
			{
				rowHandle = gridView1.FocusedRowHandle;
				if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString().Substring(1,1) == "0")
				{
					XtraMessageBox.Show("当前收费记录未开具税务发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
			}
			else if (tabPane1.SelectedPageIndex == 1)
			{
				rowHandle = gridView3.FocusedRowHandle;
				if(gridView3.GetRowCellValue(rowHandle,"BILLTYPE").ToString() == "F")
				{
					XtraMessageBox.Show("当前发票为财政发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				s_fa001 = gridView3.GetRowCellValue(rowHandle, "FA001").ToString();
			}

			using (OracleDataReader reader = SqlAssist.ExecuteReader("select * from tax_log2 where fa001='" + s_fa001 + "' and kpstatus = '2' "))
			{
				if(reader.HasRows && reader.Read())
                {
					string s_fpdm = reader["INVOICECODE"].ToString();
					string s_fphm = reader["INVOICENO"].ToString();
					if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						//TaxInvoice.PrintInvoice(s_fa001, "0");
						TaxInvoice2023.print_Invoice(TaxInvoice2023.getInvoicePrintId(s_fa001), "2");
					}
                }
                else
                {
					XtraMessageBox.Show("未找到税务发票记录!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
			}
		}

		/// <summary>
		/// 打印税票清单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("补打发票")) return;

			string s_fa001 = string.Empty;
			int rowHandle;
			if (tabPane1.SelectedPageIndex == 0)
			{
				rowHandle = gridView1.FocusedRowHandle;
				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Substring(1, 1) == "0")
				{
					XtraMessageBox.Show("当前收费记录未开具税务发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();

			}
			else if (tabPane1.SelectedPageIndex == 1)
			{
				rowHandle = gridView3.FocusedRowHandle;
				if (gridView3.GetRowCellValue(rowHandle, "BILLTYPE").ToString() == "F")
				{
					XtraMessageBox.Show("当前发票为财政发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				s_fa001 = gridView3.GetRowCellValue(rowHandle, "FA001").ToString();
			}
			OracleDataReader reader = SqlAssist.ExecuteReader("select * from tax_log2 where fa001='" + s_fa001 + "'");
			{
				if(reader.HasRows && reader.Read())
                {
					string s_fpdm = reader["INVOICECODE"].ToString();
					string s_fphm = reader["INVOICENO"].ToString();
					if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						//TaxInvoice.PrintInvoice(s_fa001, "1");
						TaxInvoice2023.print_Invoice(TaxInvoice2023.getInvoicePrintId(s_fa001), "3");
					}
                }
                else
                {
					XtraMessageBox.Show("未找到税务发票记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			reader.Dispose();
		}

		/// <summary>
		/// 导出excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			GridControl grid = tabPane1.SelectedPageIndex == 0 ? gridControl1 : gridControl3;
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				grid.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 退费操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bBi_refund_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle;

			string s_billType = string.Empty;
			string s_fa001 = string.Empty;
			if (tabPane1.SelectedPageIndex == 0)
			{				
				return;
			}
			else if(tabPane1.SelectedPageIndex == 1)
			{
				rowHandle = gridView3.FocusedRowHandle;
				if (!AppAction.CheckRight("收费退费", gridView3.GetRowCellValue(rowHandle, "FA100").ToString())) return;

				s_billType = gridView3.GetRowCellValue(rowHandle, "BILLTYPE").ToString();
				s_fa001 = gridView3.GetRowCellValue(rowHandle, "FA001").ToString();
				string s_fa002 = SqlAssist.ExecuteScalar("select fa002 from fa01 where fa001='" + s_fa001 + "'").ToString();

				//检查与开票所在工作站是否一致!!!
				if (MiscAction.CheckWorkStationCompare(s_fa001, Envior.WORKSTATIONID, s_billType) == "0")
				{
					XtraMessageBox.Show("此笔收费发票不是在当前工作站开具,不能继续!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}


				//if(s_fa002 == "2")
				//{
				//	XtraMessageBox.Show("此收费记录不能退费!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//	return;
				//}
				if (MiscAction.HaveRefund(s_fa001))
				{
					if (XtraMessageBox.Show("此收费记录已经有退费记录,是否再次退费?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No || Envior.cur_userId != AppInfo.ROOTID) return;
					//if (s_billType == "T")
					//{
					//	if (XtraMessageBox.Show("此收费记录已经有退费记录,是否再次退费?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
					//}
					//else
					//{
					//	XtraMessageBox.Show("此收费记录已经有退费记录!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					//	return;
					//}
				}
			}

			BaseDialog frm_1 = null;

			if (s_billType == "F") 
			{
				//frm_1 = new Frm_refund_select();
				if (XtraMessageBox.Show("确认要对当前发票冲红?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				string s_refund_fa001 = Tools.GetEntityPK("FA01");
				int re = MiscAction.FinRefundSettle(s_refund_fa001, Envior.cur_userId, " ", s_fa001);
                if (re > 0)
                {
					XtraMessageBox.Show("退费结算完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					string s_fa190 = SqlAssist.ExecuteScalar("select fa190 from fa01 where fa001='" + s_fa001 + "'").ToString();
					if (s_fa190.Substring(0, 1) == "1")  //原收费记录  财政发票已开
					{
						//如果是新版接口上线前开具的原发票
						if (MiscAction.FinRefundBeforeOnline(s_fa001))
						{
							XtraMessageBox.Show("原发票在财政电子发票上线前开具,不能开具对应退费发票,请在财政发票系统内完成发票开具.\r\n 开具成功后请更新发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
                        else
                        {
							if (FinInvoice.InvoiceElec_Refund(s_refund_fa001) > 0)
							{
								if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									Frm_FinInvoice frm_2 = new Frm_FinInvoice();
									frm_2.swapdata["FA001"] = s_refund_fa001;
									frm_2.ShowDialog();
									frm_2.Dispose();
								}
							}
						}
					}					 
				}

				return;
			}
			else
				frm_1 = new Frm_refund_select2();

			frm_1.swapdata["SA010"] = s_fa001;      //结算流水号
			frm_1.swapdata["SA020"] = s_billType;   //票别
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
			frm_1.Dispose();


		}
		/// <summary>
		/// 补打火化证明
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;
			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			if (gridView1.GetRowCellValue(rowHandle, "FA002").ToString() == "0")
				//PrtServAction.Print_HHZM(s_ac001);
				PrtServAction.Print_HHZM(s_ac001, Envior.mform.Handle.ToInt32());
		}

		///补充开票日志 
		private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString() != gridView1.GetRowCellValue(rowHandle, "FA195").ToString())
				{
					Frm_invoiceLog frm_1 = new Frm_invoiceLog();
					frm_1.swapdata["fa001"] = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
					if (frm_1.ShowDialog() == DialogResult.OK)
					{
						this.RefreshData();
					}
					frm_1.Dispose();
				}
				else
					XtraMessageBox.Show("该笔收费发票日志已记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		//查找下一个需要维护的收费记录
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			for(int i=(gridView1.FocusedRowHandle>0? gridView1.FocusedRowHandle:0);i<gridView1.RowCount - 1; i++)
			{
				if(gridView1.GetRowCellValue(i,"FA190").ToString() != gridView1.GetRowCellValue(i, "FA195").ToString())
				{
					gridView1.FocusedRowHandle = i;
					return;
				}
			}
		}
		/// <summary>
		/// 打印财政发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			int rowHandle = gridView1.FocusedRowHandle;
			if(rowHandle >= 0)
            {
				if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString().Substring(0,1) == "1")
                {
					string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                    //如果是新接口上线前开具的发票,不能打印
					if (MiscAction.FinRefundBeforeOnline(s_fa001))
                    {
						XtraMessageBox.Show("此笔收费财政发票为新接口上线前开具,不能通过新接口打印!","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
						return;
                    }
					Frm_FinInvoice frm_1 = new Frm_FinInvoice();
					frm_1.swapdata["FA001"] = s_fa001;
					frm_1.ShowDialog();
					frm_1.Dispose();

					//OracleDataReader reader = SqlAssist.ExecuteReader("select INVOICENO,INVOICEZCH from fin_log where settleId ='" + s_fa001 + "'");
					//if(reader.HasRows && reader.Read())
     //               {
					//	string s_batch_code = reader["INVOICEZCH"].ToString();  
					//	string s_billno = reader["INVOICENO"].ToString();
					//	reader.Dispose();
					//	if(XtraMessageBox.Show("打印财政发票?\r\n" + "注册号:" + s_batch_code + "\r\n" + "发票号:" + s_billno,"确认",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
     //                   {
					//		FinInvoice.PrintInvoice(s_batch_code, s_billno);
					//	}						
     //               }
                }
                else
                {
					XtraMessageBox.Show("当前收费记录未开财政发票!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

		/// <summary>
		/// 作废单边发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_SingleSideRemoved frm_1 = new Frm_SingleSideRemoved();
			frm_1.ShowDialog();
			frm_1.Dispose();
		}

		/// <summary>
		/// 重新查询税务发票状态
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			string fa001 = string.Empty;
			int rowHandle = gridView1.FocusedRowHandle;
			if(tabPane1.SelectedPageIndex == 0 && rowHandle >=0)
            {
				fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();

            }
		}


		/// <summary>
		/// 查询税票状态
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			string fa001 = string.Empty;
			int rowHandle = gridView1.FocusedRowHandle;
			if (tabPane1.SelectedPageIndex == 0 && rowHandle >= 0)
			{
				if (gridView1.GetRowCellValue(rowHandle, "FA190").ToString().Equals(gridView1.GetRowCellValue(rowHandle, "FA195").ToString()))
					return;

				fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				if (TaxInvoice2023.QueryInvoice(fa001) > 0 && TaxInvoice2023.getInvoiceStatus(fa001) == "2")
                {
					this.RefreshData();
                }

			}
		}
		/// <summary>
		/// 显示微信缴费变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void toggle_wechat_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			this.OnlyMe_Filter();
		}
    }
}
