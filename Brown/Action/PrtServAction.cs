using Brown.Misc;
using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.Action
{
    class PrtServAction
    {
		
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);


		public static int Connect(int connId, int prtservHandle, int clientHandle)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_ConnectPrtServ", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//连接Id
			OracleParameter op_connId = new OracleParameter("in_connectId", OracleDbType.Int32, 10);
			op_connId.Direction = ParameterDirection.Input;
			op_connId.Value = connId;
			//打印服务窗口Handle
			OracleParameter op_servHandle = new OracleParameter("in_servHandle", OracleDbType.Int32);
			op_servHandle.Direction = ParameterDirection.Input;
			op_servHandle.Value = prtservHandle;
			//打印客户端窗口
			OracleParameter op_clientHandle = new OracleParameter("in_clientHandle", OracleDbType.Int32);
			op_clientHandle.Direction = ParameterDirection.Input;
			op_clientHandle.Value = clientHandle;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_connId, op_servHandle, op_clientHandle, appcode, apperror });
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
		/// 生成新的命令编号
		/// </summary>
		/// <returns></returns>
		public static int GenNewCommandNum()
		{
			int result = int.Parse(SqlAssist.ExecuteScalar("select seq_prtserv.nextVal from dual", null).ToString());
			return result;
		}

		/// <summary>
		/// 返回打印服务响应
		/// </summary>
		/// <param name="commandNum"></param>
		/// <returns></returns>
		public static string GetResponseText(int commandNum)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.fun_getResponseText ", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 100);
			returnValue.Direction = ParameterDirection.ReturnValue;

			OracleParameter op_commandNum = new OracleParameter("in_commandNum", OracleDbType.Int32);
			op_commandNum.Direction = ParameterDirection.Input;
			op_commandNum.Value = commandNum;

			try
			{
				cmd.Parameters.Add(returnValue);
				cmd.Parameters.Add(op_commandNum);
				cmd.ExecuteNonQuery();
			}
			finally
			{
				cmd.Dispose();
			}

			return returnValue.Value.ToString();
		}


		/// <summary>
		/// 发送打印命令
		/// </summary>
		/// <param name="connId"></param>
		/// <param name="clientHandle"></param>
		/// <param name="commandNum"></param>
		/// <param name="commandString"></param>
		/// <param name="para_arry"></param>  不定参数 但实际运行只有2或8个参数（对应Oracle 过程重载）
		/// <returns></returns>
		public static int SendPrtCommand(int connId, int clientHandle, int commandNum, string commandString, params string[] para_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_SendPrtCommand", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//连接Id
			OracleParameter op_connId = new OracleParameter("in_connectId", OracleDbType.Int32, 10);
			op_connId.Direction = ParameterDirection.Input;
			op_connId.Value = connId;
			//打印客户端窗口
			OracleParameter op_clientHandle = new OracleParameter("in_clientHandle", OracleDbType.Int32);
			op_clientHandle.Direction = ParameterDirection.Input;
			op_clientHandle.Value = clientHandle;
			//打印命令编号
			OracleParameter op_commandNum = new OracleParameter("in_commandNum", OracleDbType.Int32);
			op_commandNum.Direction = ParameterDirection.Input;
			op_commandNum.Value = commandNum;
			//打印命令字符串
			OracleParameter op_commandString = new OracleParameter("ic_commandString", OracleDbType.Varchar2, 50);
			op_commandString.Direction = ParameterDirection.Input;
			op_commandString.Value = commandString;

			List<OracleParameter> para_list = new List<OracleParameter>();
			OracleParameter op_1 = null;

			for (int i = 0; i < para_arry.Length; i++)
			{
				op_1 = new OracleParameter("ic_para" + (i + 1).ToString(), OracleDbType.Varchar2, 200);
				op_1.Direction = ParameterDirection.Input;
				op_1.Value = para_arry[i];
				para_list.Add(op_1);
			}
 

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();

				para_list.InsertRange(0, new OracleParameter[] { op_connId, op_clientHandle, op_commandNum, op_commandString });
				para_list.AddRange(new OracleParameter[] { appcode, apperror });


				cmd.Parameters.AddRange(para_list.ToArray());
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					XtraMessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
				trans.Commit();

				SendMessage(Envior.prtservHandle, 0x2710, commandNum, 0);

				return 1;
			}
			catch (InvalidOperationException e)
			{
				trans.Rollback();
				MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			finally
			{
				cmd.Dispose();
			}
		}


		/// <summary>
		/// 打印火化证明
		/// </summary>
		/// <param name="ac001"></param>
		//public static void Print_HHZM(string ac001, int whandle)
		//{
		//	int commandNum = GenNewCommandNum();
		//	SendPrtCommand(Envior.prtConnId,
		//								whandle,
		//								commandNum,
		//								"Fire_HHZM",
		//								ac001,
		//								null
		//	);
		//}




		//反击从现在开始............................................
		/// <summary>
		/// 打印火化证明
		/// </summary>
		/// <param name="ac001"></param>
		public static void Print_HHZM(string ac001,int whandle)
		{
			OracleCommand oc_hhzm = new OracleCommand("select * from v_print_hhzm where ac001= :ac001", SqlAssist.conn);
			OracleParameter op_ac001 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			oc_hhzm.Parameters.Add(op_ac001);

			OracleDataReader reader = null;
			try
			{
				reader = oc_hhzm.ExecuteReader();
				if (reader.HasRows && reader.Read())
				{
					StringBuilder sb_1 = new StringBuilder(100);
					sb_1.Append(Convert.ToInt64(reader["AC001"].ToString()).ToString() + "\t");  // id 编号
					sb_1.Append(reader["AC003"].ToString() + "\t");                              //逝者姓名
					sb_1.Append(reader["AC002"].ToString() + "\t");                              //性别
					sb_1.Append(reader["AC004"].ToString() + "\t");                              //年龄
					sb_1.Append(reader["AC008"].ToString() + "\t");                              //详细住址
					sb_1.Append(reader["AC005"].ToString() + "\t");                              //死亡原因
					sb_1.Append(reader["FIRETIME"].ToString() + "\t");                           //火化时间
					sb_1.Append(Envior.cur_userName + "\t");                                     //经办人
					sb_1.Append(reader["FIRETIME"].ToString() + "\t");                           //经办时间(为火化日期)
					sb_1.Append(reader["UNITNAME"].ToString() + "\t");                           //单位名称
					sb_1.Append(reader["AC014"].ToString());									 //身份证号
					int commandNum = GenNewCommandNum();
					SendPrtCommand(Envior.prtConnId,
										whandle,
										commandNum,
										"Fire_HHZM",
										sb_1.ToString(),
										" "
					);


					//if (Envior.prtserv.of_print_hhzm(sb_1.ToString()) > 0)
					//{
					//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					//}
				}
				else
				{
					XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show(ee.ToString(), "错误");
			}
			finally
			{
				reader.Dispose();
				oc_hhzm.Dispose();
			}
		}



		/// <summary>
		/// 打印骨灰寄存证 (初次) 包括原始登记
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="settleId"></param>
		public static void PrtRegisterCert(string rc001, string settleId,int whandle)
		{

			OracleCommand oc_base = new OracleCommand("select * from v_print_regcert where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_base.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_base.ExecuteReader();

			OracleCommand oc_fin = new OracleCommand("select * from rc04 where rc010 = :rc010", SqlAssist.conn);
			OracleParameter op_rc010 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_rc010.Direction = ParameterDirection.Input;
			op_rc010.Value = settleId;
			oc_fin.Parameters.Add(op_rc010);

			OracleDataReader reader2 = oc_fin.ExecuteReader();

			try
			{
				if (reader.HasRows && reader.Read())
				{
					StringBuilder sb_1 = new StringBuilder(100);
					sb_1.Append(reader["RC003"].ToString() + "\t");                   // 逝者姓名
					sb_1.Append(reader["RC109"].ToString() + "\t");                   // 寄存证号
					sb_1.Append(reader["POSITION"].ToString() + "\t");               // 寄存位置
					sb_1.Append(reader["RC050"].ToString() + "\t");                  // 联系人
					sb_1.Append(reader["RC052"].ToString() + "\t");                  // 与逝者关系
					sb_1.Append(reader["LXFS"].ToString() + "\t");                    // 电话、地址
					sb_1.Append(reader["RC200"].ToString() + "\t");                   // 经办日期
					sb_1.Append(reader["UNITNAME"].ToString() + "\t");               // 单位名称

					reader2.Read();
					if (reader2.HasRows)
					{
						sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader2["RC020"]) + "\t");     // 开始日期
						sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader2["RC022"]) + "\t");     // 终止日期
						sb_1.Append(reader2["NUMS"].ToString() + "\t");                            // 年限
						sb_1.Append(reader2["RC030"].ToString() + "\t");                           // 缴费金额
					}
					else
					{
						sb_1.Append("" + "\t");                                                    // 开始日期
						sb_1.Append("" + "\t");                                                    // 终止日期
						sb_1.Append("" + "\t");                                                    // 年限
						sb_1.Append("" + "\t");                                                    // 缴费金额
					}
					sb_1.Append(reader["RC100"].ToString() + "\t");                                // 经办人
					sb_1.Append(reader["UNITTELE"].ToString() + "\t");                             // 业务电话


					//if (Envior.prtserv.of_prtregistercert(sb_1.ToString()) > 0)
					//{
					//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					//}

					int commandNum = GenNewCommandNum();
					SendPrtCommand(Envior.prtConnId,
										whandle,
										commandNum,
										"Register_Cert_First",
										sb_1.ToString(),
										" "
					);



				}
				else
				{
					XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ee)
			{
				MessageBox.Show("打印错误!\r\n" + ee.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				reader.Dispose();
				reader2.Dispose();
				oc_base.Dispose();
				oc_fin.Dispose();
			}
		}


		/// <summary>
		/// 打印寄存标签
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterLabel(string rc001,int whandle)
		{
			OracleCommand oc_command = new OracleCommand("select * from v_print_reglabel where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_command.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				string s_szxm = string.Empty;
				StringBuilder sb_1 = new StringBuilder(100);
				if (reader["RC303"] == null || reader["RC303"] is System.DBNull)
				{
					s_szxm = reader["RC003"].ToString();
				}
				else
				{
					s_szxm = reader["RC003"].ToString() + "/" + reader["RC303"];
				}

				sb_1.Append(s_szxm + "\t");                          // 逝者姓名
				sb_1.Append(reader["POSITION"].ToString() + "\t");   // 寄存位置
				sb_1.Append(reader["RC050"].ToString() + "\t");      // 联系人
				sb_1.Append(reader["RC051"].ToString() + "\t");      // 联系电话

				//if (Envior.prtserv.of_print_reglabel(sb_1.ToString()) > 0)
				//{
				//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//}
				int commandNum = GenNewCommandNum();
				SendPrtCommand(Envior.prtConnId,
									whandle,
									commandNum,
									"Register_Label",
									sb_1.ToString(),
									" "
				);



			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}

		/// <summary>
		/// 打印寄存 续存记录
		/// </summary>
		/// <param name="settleId"></param>
		public static void PrtRegisterPayRecord(string settleId,int whandle)
		{
			OracleCommand oc_command = new OracleCommand("select * from rc04 where rc010 = :rc010", SqlAssist.conn);
			OracleParameter op_rc010 = new OracleParameter("rc010", OracleDbType.Varchar2, 10);
			op_rc010.Direction = ParameterDirection.Input;
			op_rc010.Value = settleId;
			oc_command.Parameters.Add(op_rc010);

			OracleDataReader reader = oc_command.ExecuteReader();
			StringBuilder sb_1 = new StringBuilder(100);

			if (reader.HasRows && reader.Read())
			{
				string s_jbrq = string.Format("{0:yyyy-MM-dd}", reader["RC200"]);
				string s_begin = string.Format("{0:yyyy-MM-dd}", reader["RC020"]);
				string s_end = string.Format("{0:yyyy-MM-dd}", reader["RC022"]);
				string s_rc001 = reader["RC001"].ToString();

				sb_1.Append(s_jbrq + "\t");                          // 经办日期
				sb_1.Append(s_begin + "\t");                         // 寄存开始日期
				sb_1.Append(s_end + "\t");                           // 寄存终止日期
				sb_1.Append(reader["NUMS"].ToString() + "\t");       // 缴费年限
				sb_1.Append(reader["RC030"].ToString() + "\t");      // 缴费金额
				sb_1.Append(MiscAction.Mapper_operator(reader["RC100"].ToString()) + "\t");   //经办人


				short i_order = Convert.ToSByte(SqlAssist.ExecuteScalar("select count(*) from v_rc04 where rc001 ='" + s_rc001 + "' and rc010 <= '" + settleId + "'"));

				try
				{
					//if (Envior.prtserv.of_print_payrecord(sb_1.ToString(), i_order) > 0)
					//{
					//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					//}
					int commandNum = GenNewCommandNum();
					SendPrtCommand(Envior.prtConnId,
										whandle,
										commandNum,
										"Register_Payrecord",
										sb_1.ToString(),
										i_order.ToString()
					);



				}
				catch (Exception ee)
				{
					XtraMessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();

		}



		/// <summary>
		/// 补打寄存证
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterCertBD(string rc001,int whandle)
		{
			OracleCommand oc_base = new OracleCommand("select * from v_print_regcert where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_base.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_base.ExecuteReader();
			try
			{
				if (reader.HasRows && reader.Read())
				{
					StringBuilder sb_1 = new StringBuilder(100);
					sb_1.Append(reader["RC003"].ToString() + "\t");                   // 逝者姓名
					sb_1.Append(reader["RC109"].ToString() + "\t");                   // 寄存证号
					sb_1.Append(reader["POSITION"].ToString() + "\t");                // 寄存位置
					sb_1.Append(reader["RC050"].ToString() + "\t");                   // 联系人
					sb_1.Append(reader["RC052"].ToString() + "\t");                   // 与逝者关系
					sb_1.Append(reader["LXFS"].ToString() + "\t");                    // 电话、地址
					sb_1.Append(reader["RC200"].ToString() + "\t");                   // 经办日期
					sb_1.Append(reader["UNITNAME"].ToString() + "\t");                // 单位名称
					sb_1.Append(reader["RC100"].ToString() + "\t");                    // 经办人
					sb_1.Append(reader["UNITTELE"].ToString() + "\t");                 // 业务电话

					//if (Envior.prtserv.of_prtregistercertbd(sb_1.ToString()) > 0)
					//{
					//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					//}

					int commandNum = GenNewCommandNum();
					SendPrtCommand(Envior.prtConnId,
										whandle,
										commandNum,
										"Register_Cert_BD",
										sb_1.ToString(),
										" "
					);


				}
				else
				{
					XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show("打印错误!\r\n" + ee.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				reader.Dispose();
				oc_base.Dispose();
			}
		}

		/// <summary>
		/// 打印 迁出通知单
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterOutNotice(string rc001,int whandle)
		{
			OracleCommand oc_command = new OracleCommand("select * from v_print_outcard where rc001= :rc001", SqlAssist.conn);
			OracleParameter op_rc001 = new OracleParameter("rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			oc_command.Parameters.Add(op_rc001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				StringBuilder sb_1 = new StringBuilder(100);

				sb_1.Append(reader["RC003"].ToString() + "\t");       // 逝者姓名
				sb_1.Append(reader["POSITION"].ToString() + "\t");    // 寄存位置
				sb_1.Append(reader["RC050"].ToString() + "\t");       // 联系人
				sb_1.Append(reader["RC051"].ToString() + "\t");       // 联系电话
				sb_1.Append(reader["OC005"].ToString() + "\t");       // 迁出原因
				sb_1.Append(reader["OC003"].ToString() + "\t");       // 迁出人
				sb_1.Append(reader["OC002"].ToString() + "\t");       // 迁出日期

				//if (Envior.prtserv.of_print_outcard(sb_1.ToString()) > 0)
				//{
				//	XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//}

				int commandNum = GenNewCommandNum();
				SendPrtCommand(Envior.prtConnId,
									whandle,
									commandNum,
									"Register_Out_Notice",
									sb_1.ToString(),
									" "
				);

			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}


		/// <summary>
		/// 打印财政电子票
		/// </summary>
		/// <param name="ac001"></param>
		public static void Print_EInvoice(int whandle)
		{ 
			try
			{				  
				int commandNum = GenNewCommandNum();
				SendPrtCommand(Envior.prtConnId,
									whandle,
									commandNum,
									"Invoice_Elec",
									" ",
									" "
				);				 
			}
			catch (Exception ee)
			{
				XtraMessageBox.Show(ee.ToString(), "错误");
			}			 
		}

	}
}
