using Brown.Domain;
using Brown.Misc;
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
	/// <summary>
	/// 系统Action
	/// </summary>
	class AppAction
	{
		/// <summary>
		/// 权限检查
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public static bool CheckRight(string command)
		{
			if (Envior.cur_userId == AppInfo.ROOTID) return true;
			string sql = string.Format("select ri001 from ri01 where ri003 ='{0}' ", command);
			string commandId = string.Empty;
			object o = SqlAssist.ExecuteScalar(sql);
			if (o != null)
			{
				commandId = o.ToString();
				if (!(MiscAction.GetRight(Envior.cur_userId, commandId) == "1"))
				{
					XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
					return true;
			}
			else
				return false;
		}

		public static bool CheckRight(string command,string handler)
		{
			if (Envior.cur_userId == AppInfo.ROOTID) return true;
			string sql = string.Format("select ri001 from ri01 where ri003 ='{0}' ", command);
			string commandId = string.Empty;
			object o = SqlAssist.ExecuteScalar(sql);
			if (o != null)
			{
				commandId = o.ToString();
				switch(MiscAction.GetRight(Envior.cur_userId, commandId))
				{
					case "0":
						XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return false;
					case "1":
						if (handler != Envior.cur_userId)
						{
							XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return false;
						}
						else
							return true;
					case "2":
						return true;
					default:
						XtraMessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return false;
				}				 
			}
			else
				return false;
		}

		public static int SaveTaxInfo(string url,string id,string appId,string addr,string bank,string fplx,string publicKey,string privateKey,string cashier,string checker )
		{
			//服务请求url
			OracleParameter op_url = new OracleParameter("ic_url", OracleDbType.Varchar2, 50);
			op_url.Direction = ParameterDirection.Input;
			op_url.Value = url;

			//纳税识别号
			OracleParameter op_id = new OracleParameter("ic_id", OracleDbType.Varchar2, 20);
			op_id.Direction = ParameterDirection.Input;
			op_id.Value = id;

			//AppId
			OracleParameter op_appId = new OracleParameter("ic_appId", OracleDbType.Varchar2, 50);
			op_appId.Direction = ParameterDirection.Input;
			op_appId.Value = appId;

			//地址、电话
			OracleParameter op_addr = new OracleParameter("ic_addr", OracleDbType.Varchar2, 100);
			op_addr.Direction = ParameterDirection.Input;
			op_addr.Value = addr;

			//银行、账号
			OracleParameter op_bank = new OracleParameter("ic_bank", OracleDbType.Varchar2, 100);
			op_bank.Direction = ParameterDirection.Input;
			op_bank.Value = bank;

			//发票类型
			OracleParameter op_fplx = new OracleParameter("ic_fplx", OracleDbType.Varchar2, 10);
			op_fplx.Direction = ParameterDirection.Input;
			op_fplx.Value = fplx;

			//公钥
			OracleParameter op_public = new OracleParameter("ic_public", OracleDbType.Varchar2, 20);
			op_public.Direction = ParameterDirection.Input;
			op_public.Value = publicKey;
			//私钥
			OracleParameter op_private = new OracleParameter("ic_private", OracleDbType.Varchar2, 20);
			op_private.Direction = ParameterDirection.Input;
			op_private.Value = privateKey;

			//收款人
			OracleParameter op_cashier = new OracleParameter("ic_cashier", OracleDbType.Varchar2, 20);
			op_cashier.Direction = ParameterDirection.Input;
			op_cashier.Value = cashier;

			//审核人
			OracleParameter op_checker = new OracleParameter("ic_checker", OracleDbType.Varchar2, 20);
			op_checker.Direction = ParameterDirection.Input;
			op_checker.Value = checker;
			 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_SaveTaxInfo", new OracleParameter[] {op_url, op_id,op_appId,op_addr,op_bank,op_fplx,op_public,op_private,op_cashier,op_checker});
		}

		/// <summary>
		/// 创建新的操作员
		/// </summary>
		/// <param name="uc01"></param>
		/// <param name="rolesarry"></param>
		/// <returns></returns>
		public  static int CreateOperator(Uc01 uc01, string[] rolesarry)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc01.uc001;

			//用户代码
			OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
			op_uc002.Direction = ParameterDirection.Input;
			op_uc002.Value = uc01.uc002;

			//用户姓名
			OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
			op_uc003.Direction = ParameterDirection.Input;
			op_uc003.Value = uc01.uc003;

			//用户密码
			OracleParameter op_uc004 = new OracleParameter("ic_uc004", OracleDbType.Varchar2, 50);
			op_uc004.Direction = ParameterDirection.Input;
			op_uc004.Value = uc01.uc004;

			//登录博思账号
			OracleParameter op_uc007 = new OracleParameter("ic_uc007", OracleDbType.Varchar2, 20);
			op_uc007.Direction = ParameterDirection.Input;
			op_uc007.Value = uc01.uc007;

			//登录博思密码
			OracleParameter op_uc008 = new OracleParameter("ic_uc008", OracleDbType.Varchar2, 20);
			op_uc008.Direction = ParameterDirection.Input;
			op_uc008.Value = uc01.uc008;


			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
			op_roles_arry.Direction = ParameterDirection.Input;
			op_roles_arry.Value = string.Join("|", rolesarry);


			return SqlAssist.ExecuteProcedure("pkg_business.prc_CreateOperator", new OracleParameter[] { op_uc001, op_uc002, op_uc003, op_uc004, op_roles_arry });
		}

		/// <summary>
		/// 修改用户
		/// </summary>
		/// <returns></returns>
		public static int UpdateOperator(Uc01 uc01, string[] rolesarry)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc01.uc001;

			//用户代码
			OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
			op_uc002.Direction = ParameterDirection.Input;
			op_uc002.Value = uc01.uc002;

			//用户姓名
			OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
			op_uc003.Direction = ParameterDirection.Input;
			op_uc003.Value = uc01.uc003;

			//登录博思账号
			OracleParameter op_uc007 = new OracleParameter("ic_uc007", OracleDbType.Varchar2, 20);
			op_uc007.Direction = ParameterDirection.Input;
			op_uc007.Value = uc01.uc007;

			//登录博思密码
			OracleParameter op_uc008 = new OracleParameter("ic_uc008", OracleDbType.Varchar2, 20);
			op_uc008.Direction = ParameterDirection.Input;
			op_uc008.Value = uc01.uc008;

			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
			op_roles_arry.Direction = ParameterDirection.Input;
			op_roles_arry.Value = string.Join("|", rolesarry);


			return SqlAssist.ExecuteProcedure("pkg_business.prc_UpdateOperator",
				new OracleParameter[] { op_uc001, op_uc002, op_uc003,op_uc007,op_uc008, op_roles_arry });
		}

		/// <summary>
		/// 保存财政发票基础信息
		/// </summary>
		/// <param name="pjlx"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		public static int SaveFinanceInfo(string pjlx,string title)
		{
			//票据类型
			OracleParameter op_pjlx = new OracleParameter("ic_pjlx", OracleDbType.Varchar2, 10);
			op_pjlx.Direction = ParameterDirection.Input;
			op_pjlx.Value = pjlx;
			//题头
			OracleParameter op_title = new OracleParameter("ic_title", OracleDbType.Varchar2, 50);
			op_title.Direction = ParameterDirection.Input;
			op_title.Value = title;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_SaveFinanceInfo", new OracleParameter[] { op_pjlx,op_title});
		} 

		 
		public static int WorkStationIsRegistered(string ws001,string ws005,string ws007)
		{
			//工作站编号
			OracleParameter op_ws001 = new OracleParameter("ic_workStationId", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;
			//工作站计算机名称
			OracleParameter op_ws005 = new OracleParameter("ic_hostname", OracleDbType.Varchar2, 50);
			op_ws005.Direction = ParameterDirection.Input;
			op_ws005.Value = ws005;
			//工作站ip地址
			OracleParameter op_ws007 = new OracleParameter("ic_ipaddress", OracleDbType.Varchar2, 50);
			op_ws007.Direction = ParameterDirection.Input;
			op_ws007.Value = ws007;
			 
			return Convert.ToInt32(SqlAssist.ExecuteFunction("pkg_business.fun_workStationIsRegistered",new OracleParameter[] { op_ws001,op_ws005,op_ws007}).ToString());
		}



	}
}
