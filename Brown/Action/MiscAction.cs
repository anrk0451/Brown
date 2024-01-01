using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.Action
{
    class MiscAction
    {
		/// <summary>
		/// 返回服务或商品名
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemFullName(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemFullName(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}

		public static void DrawGridLineNo(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
		/// 返回服务或商品单价
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static decimal GetItemFixPrice(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getFixPrice(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 返回销售项目规格型号
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemGGXH(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemGGXH(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString() ;
		}

		/// <summary>
		/// 返回销售项目计量单位
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemDW(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemDW(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}


		/// <summary>
		/// 获取项目票别
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemInvoiceType(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemInvoiceType(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}

		/// <summary>
		/// 返回商品税率
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static decimal GetItemTaxRate(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getItemTaxRate(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 获取项目的 发票编码 (含财政、税务)
		/// </summary>
		/// <param name="serviceSalesType"></param>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemInvoiceCode(string serviceSalesType,string itemId)
		{
			//项目类别
			OracleParameter op_type = new OracleParameter("ic_serviceSalesType", OracleDbType.Varchar2, 3);
			op_type.Direction = ParameterDirection.Input;
			op_type.Value = serviceSalesType;

			//项目ID
			OracleParameter op_itemId = new OracleParameter("ic_salesItemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_GetInvoiceCode(:type,:itemId) from dual", new OracleParameter[] { op_type,op_itemId }).ToString();
		}

		/// <summary>
		/// 操作员姓名映射
		/// </summary>
		/// <param name="uc001"></param>
		/// <returns></returns>
		public static string Mapper_operator(string uc001)
		{
			OracleParameter op_uc001 = new OracleParameter("uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;
			Object re = SqlAssist.ExecuteScalar("select uc003 from uc01 where uc001 = :uc001", new OracleParameter[] { op_uc001 });
			return re.ToString();
		}

		/// <summary>
		/// 财务收款作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FinanceRemove(string fa001, string reason, string handler)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废原因
			OracleParameter op_fa003 = new OracleParameter("ic_fa003", OracleDbType.Varchar2, 50);
			op_fa003.Direction = ParameterDirection.Input;
			op_fa003.Value = reason;

			//作废人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinanceRemove",
				new OracleParameter[] { op_fa001, op_fa003, op_handler });
		}

		/// <summary>
		/// 财政发票作废日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="zfr"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public static int FinRemove_log(string fa001,string zfr,string reason)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废人
			OracleParameter op_zfr = new OracleParameter("ic_zfr", OracleDbType.Varchar2, 50);
			op_zfr.Direction = ParameterDirection.Input;
			op_zfr.Value = zfr;

			//作废原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinRemove_log",
				new OracleParameter[] { op_fa001, op_zfr,op_reason});
		}

		/// <summary>
		/// 税务发票作废日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="zfr"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		public static int TaxRemove_log(string fa001, string zfr, string reason)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//作废人
			OracleParameter op_zfr = new OracleParameter("ic_zfr", OracleDbType.Varchar2, 50);
			op_zfr.Direction = ParameterDirection.Input;
			op_zfr.Value = zfr;

			//作废原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_TaxRemove_log",
				new OracleParameter[] { op_fa001, op_zfr, op_reason });
		}
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="uc001"></param>
		/// <param name="newpwd"></param>
		/// <returns></returns>
		public static int Modify_Pwd(string uc001, string newpwd)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;

			//新密码
			OracleParameter op_newpwd = new OracleParameter("ic_newPwd", OracleDbType.Varchar2, 50);
			op_newpwd.Direction = ParameterDirection.Input;
			op_newpwd.Value = newpwd;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_Modify_Pwd",
				new OracleParameter[] { op_uc001, op_newpwd });
		}


		/// <summary>
		/// 判断指定收费记录 是否有退费记录
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static bool HaveRefund(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			int result = int.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_HaveRefund(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }).ToString());
			return result >0 ? true:false;
		}


		/// <summary>
		/// 退费结算
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="itemId_arry"></param>
		/// <param name="itemType_arry"></param>
		/// <param name="price_arry"></param>
		/// <param name="nums_arry"></param>
		/// <param name="handler"></param>
		/// <param name="memo"></param>
		/// <param name="ofa001"></param>
		/// <returns></returns>
		public static int FinRefundSettle(string fa001,  string[] itemId_arry, string[] itemType_arry, decimal[] price_arry, decimal[] nums_arry,
			string handler, string memo,string ofa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
 
			//销售项目编号数组
			OracleParameter op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
			op_itemId_arry.Direction = ParameterDirection.Input;
			op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemId_arry.Value = itemId_arry;

			//销售项目类型数组
			OracleParameter op_itemType_arry = new OracleParameter("ic_itemType_arry", OracleDbType.Varchar2);
			op_itemType_arry.Direction = ParameterDirection.Input;
			op_itemType_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemType_arry.Value = itemType_arry;

			//销售项目单价数组
			OracleParameter op_price_arry = new OracleParameter("in_price_arry", OracleDbType.Decimal);
			op_price_arry.Direction = ParameterDirection.Input;
			op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_price_arry.Value = price_arry;

			//销售项目数量数组
			OracleParameter op_nums_arry = new OracleParameter("in_nums_arry", OracleDbType.Decimal);
			op_nums_arry.Direction = ParameterDirection.Input;
			op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_nums_arry.Value = nums_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			//备注
			OracleParameter op_memo = new OracleParameter("ic_memo", OracleDbType.Varchar2, 80);
			op_memo.Direction = ParameterDirection.Input;
			op_memo.Value = memo;

			//原结算流水号
			OracleParameter op_ofa001= new OracleParameter("ic_ofa001", OracleDbType.Varchar2, 10);
			op_ofa001.Direction = ParameterDirection.Input;
			op_ofa001.Value = ofa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RefundSettle_Fin",
				new OracleParameter[] { op_fa001, op_itemId_arry, op_itemType_arry, op_price_arry, op_nums_arry, op_handler, op_memo,op_ofa001 });
		}

		/// <summary>
		/// 税务项目退费结算
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="itemId_arry"></param>
		/// <param name="itemType_arry"></param>
		/// <param name="price_arry"></param>
		/// <param name="nums_arry"></param>
		/// <param name="handler"></param>
		/// <param name="memo"></param>
		/// <param name="ofa001"></param>
		/// <returns></returns>
		public static int TaxRefundSettle(string fa001, string[] itemId_arry, string[] itemType_arry, decimal[] price_arry, decimal[] nums_arry,
			string handler, string memo, string ofa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//销售项目编号数组
			OracleParameter op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
			op_itemId_arry.Direction = ParameterDirection.Input;
			op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemId_arry.Value = itemId_arry;

			//销售项目类型数组
			OracleParameter op_itemType_arry = new OracleParameter("ic_itemType_arry", OracleDbType.Varchar2);
			op_itemType_arry.Direction = ParameterDirection.Input;
			op_itemType_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemType_arry.Value = itemType_arry;

			//销售项目单价数组
			OracleParameter op_price_arry = new OracleParameter("in_price_arry", OracleDbType.Decimal);
			op_price_arry.Direction = ParameterDirection.Input;
			op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_price_arry.Value = price_arry;

			//销售项目数量数组
			OracleParameter op_nums_arry = new OracleParameter("in_nums_arry", OracleDbType.Decimal);
			op_nums_arry.Direction = ParameterDirection.Input;
			op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_nums_arry.Value = nums_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			//备注
			OracleParameter op_memo = new OracleParameter("ic_memo", OracleDbType.Varchar2, 80);
			op_memo.Direction = ParameterDirection.Input;
			op_memo.Value = memo;

			//原结算流水号
			OracleParameter op_ofa001 = new OracleParameter("ic_ofa001", OracleDbType.Varchar2, 10);
			op_ofa001.Direction = ParameterDirection.Input;
			op_ofa001.Value = ofa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RefundSettle_Tax",
				new OracleParameter[] { op_fa001, op_itemId_arry, op_itemType_arry, op_price_arry, op_nums_arry, op_handler, op_memo, op_ofa001 });
		}

		/// <summary>
		/// 财务类别统计
		/// </summary>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <param name="class_arry"></param>
		/// <returns></returns>
		public static int ClassStat(string dbegin, string dend, string[] class_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_report.prc_ClassStat", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//统计日期1
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 16);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = dbegin;
			//统计日期2
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 16);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = dend;

			//销售记录编号数组
			OracleParameter op_class_arry = new OracleParameter("ic_class", OracleDbType.Varchar2);
			op_class_arry.Direction = ParameterDirection.Input;
			op_class_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_class_arry.Value = class_arry;
 
			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_class_arry, appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					XtraMessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				XtraMessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}

		/// <summary>
		/// 返回 分类统计笔数
		/// </summary>
		/// <returns></returns>
		public static int GetClassStat_BS()
		{
			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_GetClassStat_BS from dual"));
		}


		/// <summary>
		/// 收款员收款统计
		/// </summary>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int CasherStat(string dbegin, string dend, string handler)
		{
			//开始日期
			OracleParameter op_dbegin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 20);
			op_dbegin.Direction = ParameterDirection.Input;
			op_dbegin.Value = dbegin;

			//结束日期
			OracleParameter op_dend = new OracleParameter("ic_end", OracleDbType.Varchar2, 20);
			op_dend.Direction = ParameterDirection.Input;
			op_dend.Value = dend;

			//收款人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_report.prc_CasherStat",
				new OracleParameter[] { op_dbegin, op_dend, op_handler });
		}


		/// <summary>
		/// 工作站统计
		/// </summary>
		/// <param name="ws001"></param>
		/// <param name="dbegin"></param>
		/// <param name="dend"></param>
		/// <returns></returns>
		public static int WorkStationStat(string ws001,string dbegin,string dend)
		{
			//工作站编号
			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;

			//开始日期
			OracleParameter op_dbegin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 20);
			op_dbegin.Direction = ParameterDirection.Input;
			op_dbegin.Value = dbegin;

			//结束日期
			OracleParameter op_dend = new OracleParameter("ic_end", OracleDbType.Varchar2, 20);
			op_dend.Direction = ParameterDirection.Input;
			op_dend.Value = dend;
			 
			return SqlAssist.ExecuteProcedure("pkg_report.prc_workStationStat",
				new OracleParameter[] {op_ws001, op_dbegin, op_dend });
		}

		/// <summary>
		/// 授权过程
		/// </summary>
		/// <param name="ro001"></param>
		/// <param name="ri001_arry"></param>
		/// <param name="right_arry"></param>
		/// <returns></returns>
		public static int GrantRights(string ro001, string[] ri001_arry, string[] right_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_GrantRights", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//角色编号
			OracleParameter op_ro001 = new OracleParameter("ic_ro001", OracleDbType.Varchar2, 10);
			op_ro001.Direction = ParameterDirection.Input;
			op_ro001.Value = ro001;
			//功能编号数组
			OracleParameter op_ri001_arry = new OracleParameter("ri001_arry", OracleDbType.Varchar2);
			op_ri001_arry.Direction = ParameterDirection.Input;
			op_ri001_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_ri001_arry.Value = ri001_arry;

			//授权数组
			OracleParameter op_right_arry = new OracleParameter("right_arry", OracleDbType.Varchar2);
			op_right_arry.Direction = ParameterDirection.Input;
			op_right_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_right_arry.Value = right_arry;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_ro001, op_ri001_arry, op_right_arry, appcode, apperror });
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					XtraMessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}

				trans.Commit();
				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				XtraMessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}

		/// <summary>
		/// 获取操作员权限
		/// </summary>
		/// <returns></returns>
		public static string GetRight(string uc001, string ri001)
		{
			if (uc001 == AppInfo.ROOTID) return "2";
			OracleCommand cmd = new OracleCommand("pkg_business.fun_GetRight", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 3);
			returnValue.Direction = ParameterDirection.ReturnValue;

			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Size = 10;
			op_uc001.Value = uc001;

			OracleParameter op_ri001 = new OracleParameter("ic_ri001", OracleDbType.Varchar2, 10);
			op_ri001.Direction = ParameterDirection.Input;
			op_ri001.Size = 10;
			op_ri001.Value = ri001;

			try
			{
				cmd.Parameters.Add(returnValue);
				cmd.Parameters.Add(op_uc001);
				cmd.Parameters.Add(op_ri001);

				cmd.ExecuteNonQuery();
			}
			catch (InvalidOperationException e)
			{
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				cmd.Dispose();
			}

			return returnValue.Value.ToString();
		}


		/// <summary>
		/// 判断 当前工作站是否允许 操作结算记录 1-允许操作 0-不允许
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="ws001"></param>
		/// <returns></returns>
		public static string CheckWorkStationCompare(string fa001,string ws001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_CheckWorkStationCompare(:ic_fa001,:ic_ws001) from dual", new OracleParameter[] { op_fa001,op_ws001 }).ToString();
 
		}
		/// <summary>
		/// 判断 当前工作站是否允许 操作结算记录 1-允许操作 0-不允许
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="ws001"></param>
		/// <returns></returns>
		public static string CheckWorkStationCompare(string fa001, string ws001,string itype)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;

			OracleParameter op_itype = new OracleParameter("ic_itype", OracleDbType.Varchar2, 3);
			op_itype.Direction = ParameterDirection.Input;
			op_itype.Value = itype;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_CheckWorkStationCompare(:ic_fa001,:ic_ws001,:ic_itype) from dual", new OracleParameter[] { op_fa001, op_ws001,op_itype}).ToString();

		}

		/// <summary>
		/// 补填财务发票日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="pjlx"></param>
		/// <param name="zch"></param>
		/// <param name="pjh"></param>
		/// <returns></returns>
		public static int FinReLog(string fa001,string pjlx,string zch,string pjh)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//票据类型
			OracleParameter op_pjlx = new OracleParameter("ic_pjlx", OracleDbType.Varchar2, 20);
			op_pjlx.Direction = ParameterDirection.Input;
			op_pjlx.Value = pjlx;

			//注册号
			OracleParameter op_zch = new OracleParameter("ic_zch", OracleDbType.Varchar2, 20);
			op_zch.Direction = ParameterDirection.Input;
			op_zch.Value = zch;

			//票据号
			OracleParameter op_pjh = new OracleParameter("ic_pjh", OracleDbType.Varchar2, 20);
			op_pjh.Direction = ParameterDirection.Input;
			op_pjh.Value = pjh;


			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinReLog",
				new OracleParameter[] { op_fa001,op_pjlx,op_zch,op_pjh});
		}


		public static int TaxReLog(string fa001,string fpdm,string fph)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//发票代码
			OracleParameter op_fpdm = new OracleParameter("ic_fpdm", OracleDbType.Varchar2, 20);
			op_fpdm.Direction = ParameterDirection.Input;
			op_fpdm.Value = fpdm;

			//发票号
			OracleParameter op_fph = new OracleParameter("ic_fph", OracleDbType.Varchar2, 20);
			op_fph.Direction = ParameterDirection.Input;
			op_fph.Value = fph;

			 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_TaxReLog",
				new OracleParameter[] { op_fa001, op_fpdm,op_fph });
		}

		/// <summary>
		/// 返回服务器时间
		/// </summary>
		/// <returns></returns>
		public static DateTime GetServerTime()
		{
			return Convert.ToDateTime(SqlAssist.ExecuteScalar("select sysdate from dual"));
		}
		/// <summary>
		/// 财政退费操作中 原发票是否在新接口上线前
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static bool FinRefundBeforeOnline(string fa001)
        {
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			 
			int i_result = Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_business.fun_FinRefundBeforeOnline(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }));
			if (i_result == 1)
				return true;
			else
				return false;

		}
		/// <summary>
		/// 指示逝者是否具有身份证信息
		/// </summary>
		/// <param name="ac001"></param>
		/// <returns></returns>
		public static bool HasIDC(string ac001)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			object result = SqlAssist.ExecuteFunction("pkg_report.fun_HasIDC", new OracleParameter[] {op_ac001});
			if (Convert.ToInt32(result.ToString()) >= 1)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 根据出生日期计算精确年龄
		/// </summary>
		/// <param name="dt_birth"></param>
		/// <returns></returns>
		public static int Calc_Age_Via_Birth(string dt_birth)
		{
			//出生日期
			OracleParameter op_birth = new OracleParameter("ic_birth", OracleDbType.Varchar2,10);
			op_birth.Direction = ParameterDirection.Input;
			op_birth.Value = dt_birth;

			return Convert.ToInt32(SqlAssist.ExecuteFunction("pkg_report.fun_Calc_Age", new OracleParameter[] { op_birth }).ToString());
		}
		/// <summary>
		/// 判断税务发票是否可以作废 1-yes 0-no
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int TaxInvoiceCanCanceled(string fa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			return Convert.ToInt32(SqlAssist.ExecuteFunction("pkg_business.fun_TaxInvoiceCanCanceled", new OracleParameter[] { op_fa001 }).ToString());
		}


		/// <summary>
		/// 更新身份证照片
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="imgByte"></param>
		/// <returns></returns>
		public static int Update_IDC_Photo(string ic001, Byte[] imgByte)
		{
			string s_sql = "update ic01 set ic020 = :ic020 where ic001 = :ic001";

			OracleParameter op_ic001 = new OracleParameter("ic001", OracleDbType.Varchar2, 10);
			op_ic001.Direction = ParameterDirection.Input;
			op_ic001.Value = ic001;

			OracleParameter op_ic020 = new OracleParameter("ic020", OracleDbType.Blob);
			op_ic020.Direction = ParameterDirection.Input;
			op_ic020.Value = imgByte;

			return SqlAssist.ExecuteNonQuery(s_sql, new OracleParameter[] { op_ic020, op_ic001 });
		}

		/// <summary>
		/// 退费结算
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="handler"></param>
		/// <param name="memo"></param>
		/// <param name="ofa001"></param>
		/// <returns></returns>
		public static int FinRefundSettle(string fa001,string handler, string memo, string ofa001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			 
			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			//备注
			OracleParameter op_memo = new OracleParameter("ic_memo", OracleDbType.Varchar2, 80);
			op_memo.Direction = ParameterDirection.Input;
			op_memo.Value = memo;

			//原结算流水号
			OracleParameter op_ofa001 = new OracleParameter("ic_ofa001", OracleDbType.Varchar2, 10);
			op_ofa001.Direction = ParameterDirection.Input;
			op_ofa001.Value = ofa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RefundSettle_Fin",
				new OracleParameter[] { op_fa001,op_handler, op_memo, op_ofa001 });
		}


		/// <summary>
		/// 生成微信对账临时数据
		/// </summary>
		/// <returns></returns>
		public static int genWechatCheckData(string[] wxpayId_arry, string[] orderId_arry, DateTime[] orderDate, string[] scene_arry, string[] state_arry, decimal[] payfee_arry)
		{
			//微信支付Id 数组
			OracleParameter op_wxpayId_arry = new OracleParameter("ic_wxpay_id_arry", OracleDbType.Varchar2);
			op_wxpayId_arry.Direction = ParameterDirection.Input;
			op_wxpayId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_wxpayId_arry.Value = wxpayId_arry;
			//订单Id 数组
			OracleParameter op_orderId_arry = new OracleParameter("ic_order_id_arry", OracleDbType.Varchar2);
			op_orderId_arry.Direction = ParameterDirection.Input;
			op_orderId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_orderId_arry.Value = orderId_arry;
			//支付日期 数组
			OracleParameter op_payDate_arry = new OracleParameter("id_paydate_arry", OracleDbType.Date);
			op_payDate_arry.Direction = ParameterDirection.Input;
			op_payDate_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_payDate_arry.Value = orderDate;
			//支付场景 数组
			OracleParameter op_scene_arry = new OracleParameter("ic_scene_arry", OracleDbType.Varchar2);
			op_scene_arry.Direction = ParameterDirection.Input;
			op_scene_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_scene_arry.Value = scene_arry;
			//支付状态数组
			OracleParameter op_state_arry = new OracleParameter("ic_state_arry", OracleDbType.Varchar2);
			op_state_arry.Direction = ParameterDirection.Input;
			op_state_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_state_arry.Value = state_arry;
			//支付金额数组
			OracleParameter op_payfee_arry = new OracleParameter("in_payfee_arry", OracleDbType.Decimal);
			op_payfee_arry.Direction = ParameterDirection.Input;
			op_payfee_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_payfee_arry.Value = payfee_arry;

			return SqlAssist.ExecuteProcedure("pkg_wechat.prc_genCheckData",
				new OracleParameter[] { op_wxpayId_arry, op_orderId_arry, op_payDate_arry, op_scene_arry, op_state_arry, op_payfee_arry });			 
		}





	}
}
