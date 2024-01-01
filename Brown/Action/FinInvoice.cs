using Brown.Misc;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Drawing;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.Action
{
    /// <summary>
    /// 财政发票类
    /// </summary>
    class FinInvoice
    {

		//[DllImport("nkpjk.dll", EntryPoint = "PLoginSuccess")]
		//private extern static int PLoginSuccess();


		[DllImport("agency3intfc.dll", EntryPoint = "initParams", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private extern static int initParams(string url, string appId,string appKey);


		[DllImport("agency3intfc.dll", EntryPoint = "callagency3intfc", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private extern static int callagency3intfc(string sendata, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);

		//[DllImport("nkpjk.dll", EntryPoint = "PZrPj", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		//private extern static int PZrPj(string invdata,int ifprt,string pjlx,string bz, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);

		//[DllImport("nkpjk.dll", EntryPoint = "PDelPj", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		//private extern static int PDelPj(string pjh, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);


		//[DllImport("nkpjk.dll", EntryPoint = "PZrTkkp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		//private extern static int PZrTkkp(string oldph,string oldpjlx,string oldzch,string newpjlx,string tkitem,string aqt, [MarshalAs(UnmanagedType.LPStr)]StringBuilder res);


		///// <summary>
		///// 连接开票服务器
		///// </summary>
		///// <returns></returns>
		//public static int Connect()
		//{
		//    int result = ConnectKp();
		//    return result;
		//}

		///// <summary>
		///// 连接开票服务器
		///// </summary>
		///// <param name="user"></param>
		///// <param name="pwd"></param>
		///// <param name="zt"></param>
		///// <returns></returns>
		//public static int AdvConnect(string user,string pwd,string zt)
		//{
		//    return AdvConnectKp(user, pwd, zt);
		//}


		///// <summary>
		///// 断开开票服务器
		///// </summary>
		///// <returns></returns>
		//public static int DisConnect()
		//{
		//    return DisconnectKp();
		//}

		///// <summary>
		///// 判断是否连接博思开票服务器
		///// </summary>
		///// <returns></returns>
		//public static bool IsConnect()
		//{
		//    if (PLoginSuccess() == 1)
		//        return true;
		//    else
		//        return false;
		//}

		/// <summary>
		/// 获取当前下张票号
		/// </summary>
		/// <param name="batch_code"></param>
		/// <param name="bill_no"></param>
		/// <returns></returns>
		public static int GetCurrentPh()
		{ 
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("place_code",Envior.FIN_BILL_SITE);				//开票点编码
			bdata.Add("bill_batch_code", Envior.FIN_BATCH_CODE );       //票据代码(注册号)

			msg.Add("message", bdata);
			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("stock.billno.get", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if(retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					Envior.FIN_NEXT_BATCH_CODE = success_msg["bill_batch_code"];
					Envior.FIN_NEXT_BILL_NO = success_msg["bill_no"];
					return 1;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("获取票据号错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return -1;
			}
				 
		}

		/// <summary>
		/// 发送业务请求
		/// </summary>
		/// <param name="b_json"></param>
		/// <returns></returns>
		public static string SendRequest(string method,string b_json)
		{
			StringBuilder s_big = new StringBuilder(200);
			StringBuilder sb_url = new StringBuilder(100);


			string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());			 
			string s_msg_64 = Tools.EncodeBase64("utf-8", b_json);
			string s_msg_id = "0000000000202";   // Tools.GetEntityPK("FINREQ");

			s_big.Append(Envior.FIN_AGENCY_CODE);
			s_big.Append(Envior.FIN_APPID);
			s_big.Append(s_datetime);
			s_big.Append("0");                                         //是否加密
			s_big.Append("JSON");
			s_big.Append(s_msg_64);									   //message
			s_big.Append(s_msg_id);									   //message_id	
			s_big.Append(method);										   //method	
			s_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			s_big.Append(Envior.FIN_VERSION);                           //version
			 
			string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + s_big.ToString() + Envior.FIN_APPKEY).ToUpper();
			sb_url.Append("agency_code=" + Envior.FIN_AGENCY_CODE + "&");
			sb_url.Append("app_id=" + Envior.FIN_APPID + "&");
			sb_url.Append("datetime=" + s_datetime + "&");
			sb_url.Append("encryption=" + "0" + "&");
			sb_url.Append("format=" + "JSON" + "&");
			sb_url.Append("message=" + s_msg_64 + "&");
			sb_url.Append("message_id=" + s_msg_id + "&");
			sb_url.Append("method=" + method + "&");
			sb_url.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
			sb_url.Append("security=" + s_md5 + "&");
			sb_url.Append("version=" + Envior.FIN_VERSION );

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Envior.FIN_URL);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded"; 

			byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sb_url.ToString());
			request.ContentLength = buf.Length;
			Stream myRequestStream = request.GetRequestStream();
			myRequestStream.Write(buf, 0, buf.Length);
			myRequestStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			string retString = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();
			
			return Tools.DecodeBase64("utf-8", retString); 
		}
		
		/// <summary>
		/// 计算Md5 security
		/// </summary>
		/// <param name="s_bjson"></param>
		/// <returns></returns>
		private static string Calc_Security(string s_bjson)
        {
			StringBuilder s_big = new StringBuilder(200); 
			string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
			string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
			string s_msg_id = Tools.GetEntityPK("FINREQ");

			s_big.Append(Envior.FIN_AGENCY_CODE);
			s_big.Append(Envior.FIN_APPID);
			s_big.Append(s_datetime);
			s_big.Append("0");                                            //是否加密
			s_big.Append("json");
			s_big.Append(s_msg_64);                                    //message
			s_big.Append(s_msg_id);                                    //message_id	
			s_big.Append("");                                          //method	
			s_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			s_big.Append(Envior.FIN_VERSION);                           //version

			string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + s_big.ToString() + Envior.FIN_APPKEY).ToUpper();
			return s_md5;
		}

		///// <summary>
		///// 根据结算流水号开具发票
		///// </summary>
		///// <param name="fa001"></param>
		///// <returns></returns>
		public static int Invoice(string fa001)
		{
			//做开具财政发票前的预处理!!!
			if (FinInvoicePrePare(fa001) < 0) return -1;

			OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			OracleDataReader reader_fa01 = SqlAssist.ExecuteReader("select fa003,fa180 from fa01 where fa001 = :fa001", new OracleParameter[] { op_fa001 });

			string s_head = string.Empty;
			string s_memo = string.Empty;  //备注
			decimal dec_hjje = decimal.Zero;
			while (reader_fa01.Read())
			{
				//读取交款人
				s_head =  reader_fa01["FA003"].ToString();
				s_memo = reader_fa01["FA180"].ToString() + "	" + GetMemoAtMixedMode(fa001);
			}
			reader_fa01.Dispose();

			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("serial_number", fa001);				    //业务流水号 
			bdata.Add("place_code", Envior.FIN_BILL_SITE);      //开票点
			bdata.Add("payer", s_head);                         //缴款人单位
			bdata.Add("date", string.Format("{0:yyyy-MM-dd}", MiscAction.GetServerTime())); //开票日期
			bdata.Add("author",Envior.cur_userName);            //开票人
			bdata.Add("payer_type", "1");                       //缴款人类型：1 个人 2 单位
			bdata.Add("credit_code", "");                       //组织机构代码
			bdata.Add("bill_code", Envior.FIN_CODE);            //票据种类编码			
			bdata.Add("rec_mode", "1");                         //收款方式:1现金,2转账,3其它
			bdata.Add("memo", s_memo);                          //备注
 

			string s_sql = @"select * from fa05 where fa001 = :fa001" ;			 
			OracleDataReader reader_fa05 = SqlAssist.ExecuteReader(s_sql, new OracleParameter[] { op_fa001 });

			List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
			Dictionary<string, object> detail_data = null;
			while (reader_fa05.Read())
			{
				//if (reader_sa01["SA020"].ToString() != "F") continue;  //如果不是财政发票,忽略				
				detail_data = new Dictionary<string, object>();
				detail_data.Add("item_code", reader_fa05["INVOICECODE"].ToString());   //项目名 
				detail_data.Add("std", reader_fa05["PRICE"]);					   //单价
				detail_data.Add("number",reader_fa05["NUMS"]);					   //数量
				detail_data.Add("amt", reader_fa05["FEE"]);					       //金额
				detail_list.Add(detail_data);
				dec_hjje += Convert.ToDecimal(reader_fa05["FEE"]);				 
			}
			reader_fa05.Dispose();
			 
			bdata.Add("item_details", detail_list);
			bdata.Add("total_amt", dec_hjje);                                        //合计金额
			msg.Add("message", bdata);

			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.issue.do", s_json);

			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
					string s_batch_code = string.Empty;
					string s_bill_no = string.Empty;
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					s_batch_code = success_msg["bill_batch_code"];
					s_bill_no = success_msg["bill_no"];

					if (FinInvoiceLog(fa001,Envior.FIN_CODE, s_bill_no, s_batch_code, dec_hjje, Envior.cur_userId) > 0)
					{
						XtraMessageBox.Show("发票开具成功!\r\n" + "注册号:" + s_batch_code + "\r\n" + "票据号:" + s_bill_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						XtraMessageBox.Show("发票开具成功!!!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						XtraMessageBox.Show( "票据号:" + s_bill_no + "\r\n注册号:" + s_batch_code + "\r\n");
					} 
					if(XtraMessageBox.Show("现在打印【财政发票】吗?" + "\r\n" + "发票号:" + s_bill_no,"提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
						PrintInvoice(s_batch_code, s_bill_no);
                    }
					return 1;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("开财政发票失败!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}
		 		
        /// <summary>
		/// 打印财政发票
		/// </summary>
		/// <param name="batch_code"></param>
		/// <param name="bill_no"></param>
		public static void PrintInvoice(string batch_code,string bill_no)
		{			 
			//组装业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("bill_batch_code", batch_code);                  //发票注册号
			bdata.Add("bill_no", bill_no);							   //发票号
			msg.Add("message", bdata);

			StringBuilder sb_paras = new StringBuilder(100);
			StringBuilder sb_big = new StringBuilder(100);
			string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
			string s_bjson = Tools.ConvertObjectToJson(msg);
			string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
			string s_msg_id = Tools.GetEntityPK("FINREQ");

			////// 计算 MD5 
			sb_big.Append(Envior.FIN_AGENCY_CODE);
			sb_big.Append(Envior.FIN_APPID);
			sb_big.Append(s_datetime);
			sb_big.Append("0");                                          //是否加密
			sb_big.Append("json");
			sb_big.Append(s_msg_64);                                     //message
			sb_big.Append(s_msg_id);                                     //message_id	
			sb_big.Append("invoice.print.call");                         //method	
			sb_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			sb_big.Append(Envior.FIN_VERSION);                           //version
			string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + sb_big.ToString() + Envior.FIN_APPKEY).ToUpper();

			StringBuilder sb_pdata = new StringBuilder(100);
			sb_pdata.Append("{\"ParamsStr\":\"" + "app_id=" + Envior.FIN_APPID + "&");
			sb_pdata.Append("security=" + s_md5 + "&");
			sb_pdata.Append("agency_code=" + Envior.FIN_AGENCY_CODE + "&");
			sb_pdata.Append("datetime=" + s_datetime + "&");
			sb_pdata.Append("encryption=0&");
			sb_pdata.Append("format=json&");
			sb_pdata.Append("method=invoice.print.call&");
			sb_pdata.Append("message=" + s_msg_64 + "&");
			sb_pdata.Append("message_id=" + s_msg_id + "&");
			sb_pdata.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
			sb_pdata.Append("version=" + Envior.FIN_VERSION + "\"}");

			string s_url = @"http://127.0.0.1:13526/NontaxAgencyActuator?dllName=NontaxIndustry&Method=CallRemote&Params=" +
						   Tools.EncodeBase64("utf-8", sb_pdata.ToString()) + "&" +
						   "Random=" + new Random().Next().ToString() + "&" +
						   "ServiceUnit=ServiceUnit";


			//string s_url2 = @"http://127.0.0.1:13526/NontaxAgencyActuator?dllName=NontaxIndustry&Method=CallRemote&Params=eyJQYXJhbXNTdHIiOiJhcHBfaWQ9TURKU0RZQllHNzgzODM4NyZzZWN1cml0eT01QzZBREE3NjEzRkNGQUE3ODhEMkVGN0QxNUY4RTVDRiZhZ2VuY3lfY29kZT0wMjEwOTkwMDEmZGF0ZXRpbWU9MjAyMDExMDgyMDEwMTUxMjMmZW5jcnlwdGlvbj0wJmZvcm1hdD1qc29uJm1ldGhvZD1pbnZvaWNlLnByaW50LmNhbGwmbWVzc2FnZT1ld29nSUNKdFpYTnpZV2RsSWlBNklIc0tJQ0FnSUNKaWFXeHNYMkpoZEdOb1gyTnZaR1VpSURvZ0lqTTVNVEFpTEFvZ0lDQWdJbUpwYkd4ZmJtOGlJRG9nSWpBd016VTJNREEwSWdvZ0lIMEtmUT09Jm1lc3NhZ2VfaWQ9MC42MDA4NjExNTk2ODI3MzY1JnJlZ2lvbl9jb2RlPTIzMTAwMSZ2ZXJzaW9uPTEuMC4xIn0=&Random=0.659813784025407&ServiceUnit=ServiceUnit";
			//LogUtils.Info(s_url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url);
            request.Method = "GET";
			//request.ContentType = "application/json";
			request.ContentType = "text/html";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            int i_pos1 = retString.IndexOf("\"ret\":\"");
            int i_pos2 = 0;
            if (i_pos1 >= 0)
            {
                i_pos2 = retString.IndexOf("\"", i_pos1 + 7);
                if (i_pos2 > i_pos1)
                {
                    string s_base64 = retString.Substring(i_pos1 + 7, i_pos2 - (i_pos1 + 7));
                    string s_1 = Tools.DecodeBase64("utf-8", s_base64);
                    if(s_1.IndexOf("0000") >= 0)
                    {
						XtraMessageBox.Show("打印成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
            }			 
        }
				
		 
		/// <summary>
		/// 发票作废
		/// </summary>
		/// <returns></returns>
		public static int InvoiceRemoved(string batch_code,string bill_no)
        {
			 
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("bill_batch_code", batch_code);        //发票注册号
			bdata.Add("bill_no", bill_no);                   //发票号
			msg.Add("message", bdata);

			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.p.invalidate.do", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
                {
					XtraMessageBox.Show("财政发票作废成功! \r\n" + "注册号:" + batch_code + "\r\n" + "发票号:" + bill_no ,"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return 1;
                }					 
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("作废发票错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}
		
		
		/// <summary>
		/// 开退费发票
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int Refund(string fa001)
        {
			string s_ori_batch = string.Empty;
			string s_ori_billno = string.Empty;
			string s_refund_reason = string.Empty;
			decimal dec_hjje = decimal.Zero;

			//检索原发票注册号和票号
			OracleDataReader inv_reader = SqlAssist.ExecuteReader("select * from fin_log where flag = '1' and settleId = (select rf300 from refund where rf001 = '" + fa001 + "')");
			if(inv_reader.HasRows && inv_reader.Read())
            {
				s_ori_batch = inv_reader["INVOICEZCH"].ToString();
				s_ori_billno = inv_reader["INVOICENO"].ToString();
				s_refund_reason = SqlAssist.ExecuteScalar("select rf003 from refund where rf001='" + fa001 + "'").ToString();					 
            }
            else
            {
				inv_reader.Dispose();
				XtraMessageBox.Show("读取财政发票日志错误,未找到数据!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return -1;
            }

			inv_reader.Dispose();

			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();
			List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
			Dictionary<string, object> detail_data = new Dictionary<string, object>();

			bdata.Add("bill_batch_code", s_ori_batch);        //原发票注册号
			bdata.Add("bill_no", s_ori_billno);               //发票号
			bdata.Add("scarlet_bill_code", "");               //红票票据种类编码
			bdata.Add("scarlet_bill_batch_code",Envior.FIN_NEXT_BATCH_CODE);    //红票票据注册号
			bdata.Add("scarlet_bill_no", Envior.FIN_NEXT_BILL_NO);              //红票票据号
			bdata.Add("writeoff_reason", s_refund_reason);                      //冲红原因

			OracleDataReader sa01_reader = SqlAssist.ExecuteReader("select * from v_sa01 where sa010 = '" + fa001 + "'");
            if (sa01_reader.HasRows)
            {
                while (sa01_reader.Read())
                {
					if (sa01_reader["SA020"].ToString() == "F")
                    {
						detail_data = new Dictionary<string, object>();
						detail_data.Add("item_code", sa01_reader["INVOICECODE"]);								 //项目发票代码
						detail_data.Add("refund_amt",Math.Abs(Convert.ToDecimal(sa01_reader["SA007"])));         //退费金额
						detail_list.Add(detail_data);
						dec_hjje += Convert.ToDecimal(sa01_reader["SA007"]);
					}					
				}				 
			}
			sa01_reader.Dispose();
			bdata.Add("item_details", detail_list);
			msg.Add("message", bdata);

			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.p.writeOff.do", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
					string s_batch_code = string.Empty;
					string s_bill_no = string.Empty;
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					s_batch_code = success_msg["scarlet_bill_batch_code"];
					s_bill_no = success_msg["scarlet_bill_no"];

					if (FinInvoiceLog(fa001, Envior.FIN_CODE, s_bill_no, s_batch_code, dec_hjje, Envior.cur_userId) > 0)
					{
						XtraMessageBox.Show("退费发票开具成功!\r\n" + "注册号:" + s_batch_code + "\r\n" + "票据号:" + s_bill_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);						 
					}
					else
					{
						XtraMessageBox.Show("退费发票开具成功!!!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						XtraMessageBox.Show("票据号:" + s_bill_no + "\r\n注册号:" + s_batch_code + "\r\n");					 
					}
					if (XtraMessageBox.Show("现在打印【财政发票】吗?" + "\r\n" + "发票号:" + s_bill_no, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						PrintInvoice(s_batch_code, s_bill_no);
					}
					return 1;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("退费发票开具失败!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}			 
		}

		/// <summary>
		/// 发票状态查询
		/// </summary>
		/// <returns></returns>
		public static string GetInvoiceState(string batch_code,string bill_no)
        {
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("bill_batch_code", batch_code);        //发票注册号
			bdata.Add("bill_no", bill_no);                   //发票号
			msg.Add("message", bdata);


			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.state.get", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
                    Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
                    string s_state = success_msg["state"];
					return s_state;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("获取状态错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return "";
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}
		}



		/// <summary>
		/// 发票作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int InvoiceRemoved(string fa001) 
		{
			OracleDataReader reader = SqlAssist.ExecuteReader("select INVOICENO,INVOICEZCH from fin_log where settleId ='" + fa001 + "'");
			if(reader.HasRows && reader.Read())
            {
				string s_batch_code = reader["INVOICEZCH"].ToString();
				string s_billno = reader["INVOICENO"].ToString();
				reader.Dispose();
				return InvoiceRemoved(s_batch_code, s_billno);
            }
            else
            {
				XtraMessageBox.Show("未找到财政发票日志!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return 0;
            }			
		}


		 

		///// <summary>
		///// 发票开具
		///// </summary>
		///// <param name="invdata"></param>
		///// <param name="ifprt"></param>
		///// <param name="pjlx"></param>
		///// <param name="bz"></param>
		///// <returns></returns>
		//public static string Invoice(string invdata,int ifprt,string pjlx,string bz)
		//{
		//    StringBuilder sb_res = new StringBuilder();
		//    try
		//    {
		//        PZrPj(invdata, ifprt, pjlx, bz, sb_res);
		//    }
		//    catch (Exception ee)
		//    {
		//        XtraMessageBox.Show(ee.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
		//    }

		//    return sb_res.ToString();
		//}

		///// <summary>
		///// 作废财政发票
		///// </summary>
		///// <param name="pjh"></param>
		///// <returns></returns>
		//public static string Remove(string pjh)
		//{
		//    StringBuilder sb_res = new StringBuilder();
		//    PDelPj(pjh, sb_res);
		//    return sb_res.ToString();
		//} 

		///// <summary>
		///// 作废财政发票
		///// </summary>
		///// <param name="zch"></param>
		///// <param name="pjlx"></param>
		///// <param name="pjh"></param>
		///// <returns></returns>
		//public static int Remove(string zch,string pjlx,string pjh)
		//{
		//    string s_content = "票据类型=" + pjlx + "|票据号=" + pjh + "|注册号=" + zch;
		//    //MessageBox.Show(s_content);
		//    string retstr = Remove(s_content);
		//    if (retstr.IndexOf("成功") >= 0)
		//    {
		//        XtraMessageBox.Show("作废财政发票成功!\r\n" + "票据类型:" + pjlx + ",票据号:" + pjh + ",注册号:" + zch, "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
		//        return 1;
		//    }
		//    else
		//    {
		//        LogUtils.Error("作废财政发票失败:\r\n" + "票据号:" + pjh + "注册号:" + zch + "\r\n" + "返回字符串:" + retstr);
		//        XtraMessageBox.Show("作废财政发票失败!请与管理员联系!\r\n" + retstr,"提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
		//        return -1;
		//    }
		//}

		///// <summary>
		///// 退费
		///// </summary>
		///// <param name="OldPjlx"></param>
		///// <param name="OldPjh"></param>
		///// <param name="OldZch"></param>
		///// <param name="NewPjlx"></param>
		///// <param name="tkitem"></param>
		///// <param name="aQt"></param>
		///// <param name="res"></param>
		///// <returns></returns>
		//public static int Refund(string OldPjlx,string OldPjh,string OldZch,string tkitem,string aQt,string fa001,string NewPjh,decimal hjje)
		//{
		//    StringBuilder sb_res = new StringBuilder();
		//    PZrTkkp(OldPjh,OldPjlx, OldZch, Envior.FIN_INVOICE_TYPE, tkitem, aQt, sb_res);
		//    if (sb_res.ToString().IndexOf("成功") >= 0)  //退费发票开具成功
		//    {
		//        if (FinInvoiceLog(fa001, Envior.FIN_INVOICE_TYPE, NewPjh, "", 0 - Math.Abs(hjje),Envior.cur_userId) > 0)
		//        {
		//            XtraMessageBox.Show("发票开具成功!\r\n" + "发票类型:" + Envior.FIN_INVOICE_TYPE + "\r\n" + "发票号:" + NewPjh, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//            return 1;
		//        }
		//        else
		//        {
		//            XtraMessageBox.Show("发票开具成功!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//            return 1;
		//        }
		//    }
		//    else
		//    {
		//        XtraMessageBox.Show("发票开具失败!\r\n" + sb_res.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
		//        return -1;
		//    }
		//}


		///// <summary>
		///// 根据结算流水号开具发票
		///// </summary>
		///// <param name="fa001"></param>
		///// <returns></returns>
		//public static int Invoice(string fa001)
		//{
		//    OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
		//    op_fa001.Direction = ParameterDirection.Input;
		//    op_fa001.Value = fa001;
		//    OracleDataReader reader_fa01 = SqlAssist.ExecuteReader("select fa003,fa180 from fa01 where fa001 = :fa001",new OracleParameter[] { op_fa001});

		//    string s_head = string.Empty;
		//    string s_memo = string.Empty;  //备注
		//    decimal dec_hjje = decimal.Zero;
		//    while (reader_fa01.Read())
		//    {
		//        //读取交款人
		//        s_head = Envior.FIN_INVOICE_TITLE + "=" + reader_fa01["FA003"].ToString() + "	";
		//        s_memo = reader_fa01["FA180"].ToString();
		//    }
		//    reader_fa01.Dispose();

		//    string s_sql = @"select sa002,
		//                            sa004,
		//                            sa003,
		//                            price,
		//                            nums,
		//                            sa020,
		//                            sa007,
		//                            pkg_business.fun_GetInvoiceCode(sa002,sa004) invcode
		//                     from v_sa01
		//                    where sa010 = :fa001 order by sa001";
		//    OracleDataReader reader_sa01 = SqlAssist.ExecuteReader(s_sql,new OracleParameter[] { op_fa001});

		//    StringBuilder sb_detail = new StringBuilder(100);
		//    while (reader_sa01.Read())
		//    {
		//        if (reader_sa01["SA020"].ToString() != "F") continue;  //如果不是财政发票,忽略
		//        sb_detail.Append("收费项目=" + reader_sa01["INVCODE"].ToString() + "	" + "计费数量=" + reader_sa01["NUMS"].ToString() + "	" + "收费标准=" + reader_sa01["PRICE"].ToString() + "	" + "金额=" + reader_sa01["SA007"].ToString() + "	");
		//        dec_hjje += Convert.ToDecimal(reader_sa01["SA007"]);
		//    }
		//    reader_sa01.Dispose();

		//    string vContent = "<&票据><&票据头>" + s_head + "	" + "</&票据头>" + "<&收费项目>" + sb_detail.ToString() + "</&收费项目></&票据>";
		//    string retstr = Invoice(vContent, 1,Envior.FIN_INVOICE_TYPE, s_memo);
		//    if(retstr.IndexOf("成功") >= 0)
		//    {
		//        //TODO 4. 记录财政发票日志
		//        string s_info = retstr.Substring(3);
		//        string[] s_arry = s_info.Split(',');
		//        if(s_arry.Length >= 4)
		//        {
		//            string s_pjlx = s_arry[0];    //票据类型
		//            string s_pjh = s_arry[1];     //发票号
		//            string s_zch = s_arry[3];     //注册号
		//            if(FinInvoiceLog(fa001,s_pjlx,s_pjh,s_zch,dec_hjje,Envior.cur_userId) > 0)
		//            {
		//                XtraMessageBox.Show("发票开具成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//                return 1;
		//            }
		//            else
		//            {
		//                XtraMessageBox.Show("发票开具成功!!!但记录日志失败，请与管理员联系！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		//                XtraMessageBox.Show("票据类型:" + s_pjlx + "\r\n" + "票据号:" + s_pjh + "\r\n注册号:" + s_zch + "\r\n");
		//                return 1;
		//            }
		//        }
		//        else
		//        {
		//            XtraMessageBox.Show("发票开具成功!但记录日志出现错误，请与管理员联系!！\r\n" + retstr + "数组大小:" + s_arry.Length.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//            return 1;
		//        }
		//    }
		//    else
		//    {
		//        XtraMessageBox.Show("发票开具失败!\r\n" + retstr,"提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		//        return -1;
		//    }

		//}

		/// <summary>
		/// 财政发票开票日志
		/// </summary>
		/// <param name="fa001">结算流水号</param>
		/// <param name="fplx">发票类型</param>
		/// <param name="fph">发票号</param>
		/// <param name="zch">注册号</param>
		/// <returns></returns>
		public static int FinInvoiceLog(string fa001, string fplx, string fph, string zch, decimal hjje, string kpr)
		{
			//逝者编号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//票据类型
			OracleParameter op_pjlx = new OracleParameter("ic_billCode", OracleDbType.Varchar2, 10);
			op_pjlx.Direction = ParameterDirection.Input;
			op_pjlx.Value = fplx;

			//票号
			OracleParameter op_fph = new OracleParameter("ic_billNo", OracleDbType.Varchar2, 10);
			op_fph.Direction = ParameterDirection.Input;
			op_fph.Value = fph;

			//注册号
			OracleParameter op_zch = new OracleParameter("ic_zch", OracleDbType.Varchar2, 20);
			op_zch.Direction = ParameterDirection.Input;
			op_zch.Value = zch;

			//合计金额
			OracleParameter op_hjje = new OracleParameter("in_hjje", OracleDbType.Decimal);
			op_hjje.Direction = ParameterDirection.Input;
			op_hjje.Value = hjje;
			//开票人
			OracleParameter op_kpr = new OracleParameter("ic_kpr", OracleDbType.Varchar2, 10);
			op_kpr.Direction = ParameterDirection.Input;

			if (fa001.StartsWith("wx"))
				op_kpr.Value = "8888888888";  //微信开票
			else
				op_kpr.Value = kpr;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinInvoiceLog",
				new OracleParameter[] { op_fa001, op_pjlx, op_fph, op_zch, op_hjje, op_kpr });
		}

		///// <summary>
		///// 自动连接到博思服务器
		///// </summary>
		///// <returns></returns>
		//public static void AutoConnectBosi()
		//{
		//    int result = 0;
		//    if (String.IsNullOrEmpty(Envior.cur_userBosi))
		//    {
		//        result = Connect();
		//    }
		//    else
		//    {
		//        result = AdvConnect(Envior.cur_userBosi, Envior.cur_pwdBosi, "");
		//    }
		//    if (result == 1)
		//        Envior.FIN_READY = true;
		//    else
		//    {
		//        XtraMessageBox.Show("连接财政开票服务器失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        Envior.FIN_READY = false;
		//    }
		// PrtServAction.ConnectBosi(Envior.cur_userBosi, Envior.cur_pwdBosi, Envior.mform.Handle.ToInt32());

		/// <summary>
		/// 财政发票开具预处理
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int FinInvoicePrePare(string fa001)
		{
			//服务请求url
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinInvoicePrePare", new OracleParameter[] { op_fa001 });
		}
		/// <summary>
		/// 财政发票混合模式下获取备注
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string GetMemoAtMixedMode(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			return SqlAssist.ExecuteFunction("pkg_business.fun_GetMemoAtMixedMode", new OracleParameter[] { op_fa001 }).ToString();
		}

		/// <summary>
		/// 获取电子票据下张票号
		/// </summary>
		/// <returns></returns>
		public static int GetCurrentElecPh()
		{
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("place_code", "001");                                             //开票点编码
			//bdata.Add("bill_batch_code", Envior.GetEnvior().FIN_BATCH_CODE);          //票据代码(注册号)
			bdata.Add("bill_batch_code", "23013321");          //电子票据代码(注册号)

			msg.Add("message", bdata);
			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("stock.billno.get", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					Envior.FIN_NEXT_BATCH_CODE = success_msg["bill_batch_code"];
					Envior.FIN_NEXT_BILL_NO = success_msg["bill_no"];
					return 1;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("获取票据号错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}
		/// <summary>
		/// 开具电子票据
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		/// 


		[System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
		[System.Security.SecurityCritical]
		public static int InvoiceElec(string fa001)
		{
			//做开具财政发票前的预处理!!!
			if (FinInvoicePrePare(fa001) < 0) return -1;

			OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			OracleDataReader reader_fa01 = SqlAssist.ExecuteReader("select fa003,fa180 from fa01 where fa001 = :fa001", new OracleParameter[] { op_fa001 });

			string s_head = string.Empty;
			string s_memo = string.Empty;  //备注
			decimal dec_hjje = decimal.Zero;
			while (reader_fa01.Read())
			{
				//读取交款人
				s_head = reader_fa01["FA003"].ToString();
				s_memo = reader_fa01["FA180"].ToString() + "	" + GetMemoAtMixedMode(fa001);
			}
			reader_fa01.Dispose();

			///生成发送的数据

			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
			Dictionary<string, object> detail_data = null;

			bdata.Add("serial_number", "MDJBYG" + fa001);                   //业务流水号 
			bdata.Add("place_code", Envior.FIN_BILL_SITE);                  //开票点
			bdata.Add("payer", s_head);                                     //缴款人单位
			bdata.Add("date", string.Format("{0:yyyy-MM-dd}", MiscAction.GetServerTime())); //开票日期
			bdata.Add("author", Envior.cur_userName);                       //开票人
			bdata.Add("payer_type", "1");                                   //缴款人类型：1 个人 2 单位
			bdata.Add("credit_code", "");                                   //组织机构代码
			bdata.Add("bill_code", Envior.FIN_CODE);                        //票据种类编码	
			bdata.Add("bill_no", "");
			bdata.Add("rec_mode", "1");                                     //收款方式:1现金,2转账,3其它 
			bdata.Add("memo", s_memo);                                      //备注
			 
			/////读取销售明细
			string s_sql = @"select * from fa05 where fa001 = :fa001";
			OracleDataReader reader_fa05 = SqlAssist.ExecuteReader(s_sql, new OracleParameter[] { op_fa001 }); 
			while (reader_fa05.Read())
			{
				detail_data = new Dictionary<string, object>();
				detail_data.Add("item_code", reader_fa05["INVOICECODE"].ToString());   //项目名 
			    detail_data.Add("std", reader_fa05["PRICE"]);						   //单价
				detail_data.Add("number", reader_fa05["NUMS"]);						   //数量
				detail_data.Add("unit", "");									       //单位
				detail_data.Add("amt", reader_fa05["FEE"]);							   //金额 
				detail_list.Add(detail_data);
				dec_hjje += Convert.ToDecimal(reader_fa05["FEE"]);				 
			}
			reader_fa05.Dispose();

			bdata.Add("item_details", detail_list);
			bdata.Add("total_amt", dec_hjje);                                          //合计金额
			msg.Add("message", bdata);

			string s_json = Tools.ConvertObjectToJson(msg);			
			LogUtils.Info(s_json);
			 

			StringBuilder sb_paras = new StringBuilder(500);
			StringBuilder sb_big = new StringBuilder(500);
			string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
			string s_bjson = Tools.ConvertObjectToJson(msg);
			string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
			string s_msg_id = Tools.GetEntityPK("FINREQ");

			////// 计算 MD5 
			sb_big.Append(Envior.FIN_AGENCY_CODE);
			sb_big.Append(Envior.FIN_APPID);
			sb_big.Append(s_datetime);
			sb_big.Append("0");                                          //是否加密
			sb_big.Append("JSON");
			sb_big.Append(s_msg_64);                                     //message
			sb_big.Append(s_msg_id);                                     //message_id	
			sb_big.Append("invoice.e.issue.do");                         //method	
			sb_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			sb_big.Append(Envior.FIN_VERSION);                           //version
			string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + sb_big.ToString() + Envior.FIN_APPKEY).ToUpper();

			StringBuilder sb_pdata = new StringBuilder(500);
			sb_pdata.Append("{\"ParamsStr\":\"" + "agency_code=" + Envior.FIN_AGENCY_CODE + "&" + "app_id=" + Envior.FIN_APPID + "&");
			sb_pdata.Append("datetime=" + s_datetime + "&");
			sb_pdata.Append("encryption=0&");
			sb_pdata.Append("format=JSON&");
			sb_pdata.Append("message=" + s_msg_64 + "&");
			sb_pdata.Append("message_id=" + s_msg_id + "&");
			sb_pdata.Append("method=invoice.e.issue.do&");
			sb_pdata.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
			sb_pdata.Append("security=" + s_md5 + "&");
			sb_pdata.Append("version=" + Envior.FIN_VERSION + "\"}");

			//XtraMessageBox.Show("debug point 1");
			try
			{
				if (CallinitParams(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY) > 0)
				{
					StringBuilder sb_res = new StringBuilder(600);
					LogUtils.Debug(sb_pdata.ToString());

					FinInvoice.CallService(sb_pdata.ToString(), sb_res);
					LogUtils.Debug(sb_res.ToString());

					//XtraMessageBox.Show("debug point 2");

					Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(sb_res.ToString());
					if (retdata != null)
					{
						if (retdata.ContainsKey("message"))
						{
							string s_batch_code = string.Empty;
							string s_bill_no = string.Empty;
							Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
							s_batch_code = success_msg["bill_batch_code"];
							s_bill_no = success_msg["bill_no"];

							if (FinInvoiceLog(fa001, Envior.FIN_CODE, s_bill_no, s_batch_code, dec_hjje, Envior.cur_userId) > 0)
							{
								XtraMessageBox.Show("财政电子票开具成功!\r\n" + "注册号:" + s_batch_code + "\r\n" + "票据号:" + s_bill_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								XtraMessageBox.Show("电子票开具成功!!!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								XtraMessageBox.Show("票据号:" + s_bill_no + "\r\n注册号:" + s_batch_code + "\r\n");
							}
							//if (XtraMessageBox.Show("现在打印【财政发票】吗?" + "\r\n" + "发票号:" + s_bill_no, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							//{
							//	PrintInvoice(s_batch_code, s_bill_no);
							//}
							return 1;
						}
						else
						{
							Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
							LogUtils.Fatal(sb_res.ToString());
							XtraMessageBox.Show("开财政发票失败!!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return -1;
						}
					}
					else
					{
						XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return -1;
					}
				}
				else
				{
					XtraMessageBox.Show("初始化失败");
					return -1;
				}
			}
			catch (Exception ee) 
			{
				LogUtils.Error(ee.ToString());
				XtraMessageBox.Show("执行失败!" + ee.ToString());
				return -1;
			}
			 
		}

		/// <summary>
		/// 开具电子票据
		/// </summary>
		/// <returns></returns>
		//public static int InvoiceElec()
		//{
		//	Dictionary<string, object> bdata = new Dictionary<string, object>();
		//	Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

		//	bdata.Add("serial_number", "1000009996");                     //业务流水号 
		//	bdata.Add("place_code", "001");                               //开票点
		//	bdata.Add("payer", "测试用户");                               //缴款人单位
		//	bdata.Add("date", string.Format("{0:yyyy-MM-dd}", MiscAction.GetServerTime())); //开票日期
		//	bdata.Add("author", "测试人员");                              //开票人
		//	bdata.Add("payer_type", "1");                       //缴款人类型：1 个人 2 单位
		//	bdata.Add("credit_code", " ");                       //组织机构代码
		//	bdata.Add("bill_code", "0133");                     //票据种类编码			
		//	bdata.Add("rec_mode", "1");                         //收款方式:1现金,2转账,3其它
		//	bdata.Add("memo", "备注");                          //备注

		//	List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
		//	Dictionary<string, object> detail_data = null;

		//	detail_data = new Dictionary<string, object>();
		//	detail_data.Add("item_code", "10304490802");   //项目名 
		//	detail_data.Add("std", 2);                     //单价
		//	detail_data.Add("number", 1);                    //数量
		//	detail_data.Add("amt", 2);                     //金额
		//	detail_list.Add(detail_data);

		//	bdata.Add("item_details", detail_list);
		//	bdata.Add("total_amt", 2);                    //合计金额

		//	msg.Add("message", bdata);

		//	string s_json = Tools.ConvertObjectToJson(msg);
		//	string s_ret = SendRequest("invoice.e.issue.do", s_json);

		//	Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
		//	if (retdata != null)
		//	{
		//		if (retdata.ContainsKey("message"))
		//		{
		//			string s_batch_code = string.Empty;
		//			string s_bill_no = string.Empty;
		//			Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
		//			s_batch_code = success_msg["bill_batch_code"];
		//			s_bill_no = success_msg["bill_no"];

		//			XtraMessageBox.Show("开具发票成功!");
		//			XtraMessageBox.Show(s_batch_code, "注册号");
		//			XtraMessageBox.Show(s_bill_no, "票号");

		//			return 1;
		//		}
		//		else
		//		{
		//			Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
		//			XtraMessageBox.Show("开财政发票失败!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//			return -1;
		//		}
		//	}
		//	else
		//	{
		//		XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		return -1;
		//	}			 
		//}


		/// <summary>
		/// 获取电子票据图像 Base64编码
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string GetInvoiceImageBase64(string fa001)
		{
			string s_bill_code = string.Empty;
			string s_bill_no = string.Empty;
			//检索原发票注册号和票号
			OracleDataReader inv_reader = SqlAssist.ExecuteReader("select * from fin_log where flag = '1' and settleId = '" + fa001 + "'" );
			if (inv_reader.HasRows && inv_reader.Read())
			{
				s_bill_code = inv_reader["INVOICEZCH"].ToString();
				s_bill_no = inv_reader["INVOICENO"].ToString();

				inv_reader.Dispose();

				//业务数据
				Dictionary<string, object> bdata = new Dictionary<string, object>();
				Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

				bdata.Add("bill_code", Envior.FIN_CODE);           //票据种类编码
				bdata.Add("bill_batch_code", s_bill_code);         //注册号
				bdata.Add("bill_no", s_bill_no);                   //票据号
				bdata.Add("source_type","1");                      // 1取云平台生成的图片（在线模式）2取本地生成的图片

				msg.Add("message", bdata);
				string s_json = Tools.ConvertObjectToJson(msg);
				string s_ret = SendRequest("invoice.image.get", s_json);
				Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
				if (retdata != null)
				{
					if (retdata.ContainsKey("message"))
					{
						Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());						 
						return success_msg["image"]; 
					}
					else
					{
						Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
						XtraMessageBox.Show("获取票据图片错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return "";
					}
				}
				else
				{
					XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return "";
				}
			}
			else
			{
				inv_reader.Dispose();
				XtraMessageBox.Show("读取财政发票日志错误,未找到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}
			 
		}


		/// <summary>
		/// 获取电子票据图像 Base64编码
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string GetInvoiceImageBase64(string bill_code,string bill_no)
		{
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("bill_code", "0133");                  //票据种类编码(电子票)
			bdata.Add("bill_batch_code", bill_code);         //注册号
			bdata.Add("bill_no", bill_no);                   //票据号
			bdata.Add("source_type", "1");                   // 1取云平台生成的图片（在线模式）2取本地生成的图片

			msg.Add("message", bdata);
			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.image.get", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				//XtraMessageBox.Show(retdata.ToString());
				if (retdata.ContainsKey("message"))
				{
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					return success_msg["image"];
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("获取票据图片错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return "";
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}


			////组装业务数据
			//Dictionary<string, object> bdata = new Dictionary<string, object>();
			//Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();


			//bdata.Add("bill_code", "0133");                  //票据种类编码(电子票)
			//bdata.Add("bill_batch_code", bill_code);         //注册号
			//bdata.Add("bill_no", bill_no);                   //票据号
			//bdata.Add("source_type", "1");                   // 1取云平台生成的图片（在线模式）2取本地生成的图片
			//msg.Add("message", bdata);

			//StringBuilder sb_paras = new StringBuilder(100);
			//StringBuilder sb_big = new StringBuilder(100);
			//string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
			//string s_bjson = Tools.ConvertObjectToJson(msg);
			//string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
			//string s_msg_id = Tools.GetEntityPK("FINREQ");

			//////// 计算 MD5 
			//sb_big.Append(Envior.FIN_AGENCY_CODE);
			//sb_big.Append(Envior.FIN_APPID);
			//sb_big.Append(s_datetime);
			//sb_big.Append("0");                                          //是否加密
			//sb_big.Append("json");
			//sb_big.Append(s_msg_64);                                     //message
			//sb_big.Append(s_msg_id);                                     //message_id	
			//sb_big.Append("invoice.image.get");                          //method	
			//sb_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			//sb_big.Append(Envior.FIN_VERSION);                           //version
			//string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + sb_big.ToString() + Envior.FIN_APPKEY).ToUpper();

			//StringBuilder sb_pdata = new StringBuilder(100);
			//sb_pdata.Append("{\"ParamsStr\":\"" + "app_id=" + Envior.FIN_APPID + "&");
			//sb_pdata.Append("security=" + s_md5 + "&");
			//sb_pdata.Append("agency_code=" + Envior.FIN_AGENCY_CODE + "&");
			//sb_pdata.Append("datetime=" + s_datetime + "&");
			//sb_pdata.Append("encryption=0&");
			//sb_pdata.Append("format=json&");
			//sb_pdata.Append("method=invoice.print.call&");
			//sb_pdata.Append("message=" + s_msg_64 + "&");
			//sb_pdata.Append("message_id=" + s_msg_id + "&");
			//sb_pdata.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
			//sb_pdata.Append("version=" + Envior.FIN_VERSION + "\"}");

			//string s_url = @"http://127.0.0.1:13526/NontaxAgencyActuator?dllName=NontaxIndustry&Method=CallRemote&Params=" +
			//			   Tools.EncodeBase64("utf-8", sb_pdata.ToString()) + "&" +
			//			   "Random=" + new Random().Next().ToString() + "&" +
			//			   "ServiceUnit=ServiceUnit";


			////string s_url2 = @"http://127.0.0.1:13526/NontaxAgencyActuator?dllName=NontaxIndustry&Method=CallRemote&Params=eyJQYXJhbXNTdHIiOiJhcHBfaWQ9TURKU0RZQllHNzgzODM4NyZzZWN1cml0eT01QzZBREE3NjEzRkNGQUE3ODhEMkVGN0QxNUY4RTVDRiZhZ2VuY3lfY29kZT0wMjEwOTkwMDEmZGF0ZXRpbWU9MjAyMDExMDgyMDEwMTUxMjMmZW5jcnlwdGlvbj0wJmZvcm1hdD1qc29uJm1ldGhvZD1pbnZvaWNlLnByaW50LmNhbGwmbWVzc2FnZT1ld29nSUNKdFpYTnpZV2RsSWlBNklIc0tJQ0FnSUNKaWFXeHNYMkpoZEdOb1gyTnZaR1VpSURvZ0lqTTVNVEFpTEFvZ0lDQWdJbUpwYkd4ZmJtOGlJRG9nSWpBd016VTJNREEwSWdvZ0lIMEtmUT09Jm1lc3NhZ2VfaWQ9MC42MDA4NjExNTk2ODI3MzY1JnJlZ2lvbl9jb2RlPTIzMTAwMSZ2ZXJzaW9uPTEuMC4xIn0=&Random=0.659813784025407&ServiceUnit=ServiceUnit";
			//LogUtils.Info(s_url);
			//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url);
			//request.Method = "GET";
			////request.ContentType = "application/json";
			//request.ContentType = "text/html";


			//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//Stream myResponseStream = response.GetResponseStream();
			//StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			//string retString = myStreamReader.ReadToEnd();

			//myStreamReader.Close();
			//myResponseStream.Close();

			//int i_pos1 = retString.IndexOf("\"ret\":\"");
			//int i_pos2 = 0;
			//if (i_pos1 >= 0)
			//{
			//	i_pos2 = retString.IndexOf("\"", i_pos1 + 7);
			//	if (i_pos2 > i_pos1)
			//	{
			//		string s_base64 = retString.Substring(i_pos1 + 7, i_pos2 - (i_pos1 + 7));
			//		string s_1 = Tools.DecodeBase64("utf-8", s_base64);
			//		if (s_1.IndexOf("0000") >= 0)
			//		{
			//			XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//		}
			//	}
			//}
		}
		/// <summary>
		/// 该接口提供获根据票据代码和票号来发送信息
		/// </summary>
		/// <param name="inv_code"></param>
		/// <param name="inv_no"></param>
		/// <param name="ntype"></param>
		/// <param name="nvalue"></param>
		public static int SendElecInvoiceNotice(string inv_code,string inv_no,string ntype,string nvalue)
		{
			//业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();
			 
			bdata.Add("bill_batch_code", inv_code);         //注册号
			bdata.Add("bill_no", inv_no);                   //票据号
			bdata.Add("type", ntype);                      // 通知类型 1202-电子邮件
			bdata.Add("type_value", nvalue); // 通知类型 1202-电子邮件

			msg.Add("message", bdata);
			string s_json = Tools.ConvertObjectToJson(msg);
			string s_ret = SendRequest("invoice.message.send", s_json);
			Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(s_ret);
			if (retdata != null)
			{
				if (retdata.ContainsKey("message"))
				{
					Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
					return 1 ;
				}
				else
				{
					Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
					XtraMessageBox.Show("发送通知错误!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return  -1;
			}
		}

		public static int CallinitParams(string url, string appId, string appKey)
		{
			return initParams(url, appId, appKey);
		}

		public static int CallService(string url,StringBuilder res)
		{
			return callagency3intfc(url, res); 
		}


		/// <summary>
		/// 电子票据冲红
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		//public static int InvoiceElec_Refund(string fa001)
		//{
		//	//string s_ori_serial = string.Empty;
		//	//string s_reason = string.Empty;

		//	////检索原发票注册号和票号
		//	//OracleDataReader inv_reader = SqlAssist.ExecuteReader("select * from refund where rf001 = '" + fa001 + "'");
		//	//if (inv_reader.HasRows && inv_reader.Read())
		//	//{
		//	//	s_ori_serial = "MDJBYG" + inv_reader["RF300"].ToString();
		//	//	s_reason = inv_reader["RF003"].ToString();
		//	//}
		//	//else
		//	//{
		//	//	inv_reader.Dispose();
		//	//	XtraMessageBox.Show("读取退费日志错误,未找到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	//	return -1;
		//	//}
		//	//inv_reader.Dispose();

		//	Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();
		//	Dictionary<string, object> bdata = new Dictionary<string, object>();

		//	bdata.Add("bill_batch_code","23013321");                   //原发票批次号
		//	bdata.Add("bill_no", "0000257727");                          //原发票号
		//	bdata.Add("scarlet_bill_code", Envior.FIN_CODE);                          //
		//	bdata.Add("scarlet_bill_batch_code", "23013321");
		//	bdata.Add("scarlet_bill_no", "0000257729");


		//	bdata.Add("writeoff_reason", "测试部分冲红");

		//	List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
		//	Dictionary<string, object> detail_data = null;

		//	detail_data = new Dictionary<string, object>();
		//	detail_data.Add("item_code", "10304490804");   //项目名 
		//	detail_data.Add("refund_amt", 1.3); //退费金额
		//	detail_list.Add(detail_data);

		//	bdata.Add("item_details", detail_list);
		//	msg.Add("message", bdata);

		//	string s_json = Tools.ConvertObjectToJson(msg);
		//	LogUtils.Error(s_json);

		//	StringBuilder sb_paras = new StringBuilder(500);
		//	StringBuilder sb_big = new StringBuilder(500);
		//	string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
		//	string s_bjson = Tools.ConvertObjectToJson(msg);
		//	string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
		//	string s_msg_id = Tools.GetEntityPK("FINREQ");

		//	////// 计算 MD5 
		//	sb_big.Append(Envior.FIN_AGENCY_CODE);
		//	sb_big.Append(Envior.FIN_APPID);
		//	sb_big.Append(s_datetime);
		//	sb_big.Append("0");                                          //是否加密
		//	sb_big.Append("JSON");
		//	sb_big.Append(s_msg_64);                                     //message
		//	sb_big.Append(s_msg_id);                                     //message_id	
		//	sb_big.Append("invoice.p.writeOff.do");                      //method	
		//	sb_big.Append(Envior.FIN_REGION_CODE);                       //region_code
		//	sb_big.Append(Envior.FIN_VERSION);                           //version
		//	string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + sb_big.ToString() + Envior.FIN_APPKEY).ToUpper();

		//	StringBuilder sb_pdata = new StringBuilder(500);
		//	sb_pdata.Append("{\"ParamsStr\":\"" + "agency_code=" + Envior.FIN_AGENCY_CODE + "&" + "app_id=" + Envior.FIN_APPID + "&");
		//	sb_pdata.Append("datetime=" + s_datetime + "&");
		//	sb_pdata.Append("encryption=0&");
		//	sb_pdata.Append("format=JSON&");
		//	sb_pdata.Append("message=" + s_msg_64 + "&");
		//	sb_pdata.Append("message_id=" + s_msg_id + "&");
		//	sb_pdata.Append("method=invoice.p.writeOff.do&");
		//	sb_pdata.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
		//	sb_pdata.Append("security=" + s_md5 + "&");
		//	sb_pdata.Append("version=" + Envior.FIN_VERSION + "\"}");

		//	if (CallinitParams(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY) > 0)
		//	{
		//		StringBuilder sb_res = new StringBuilder(600);
		//		LogUtils.Debug(sb_pdata.ToString());
				
		//		FinInvoice.CallService(sb_pdata.ToString(), sb_res);
		//		LogUtils.Debug(sb_res.ToString());

		//		Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(sb_res.ToString());
		//		if (retdata != null)
		//		{
		//			if (retdata.ContainsKey("message"))
		//			{
		//				string s_batch_code = string.Empty;
		//				string s_bill_no = string.Empty;
		//				Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
		//				s_batch_code = success_msg["scarlet_bill_batch_code"];
		//				s_bill_no = success_msg["scarlet_bill_no"];

		//				///if (FinInvoiceLog(fa001, Envior.FIN_CODE, s_bill_no, s_batch_code, dec_hjje, Envior.cur_userId) > 0)
		//				///{
		//					XtraMessageBox.Show("退费发票开具成功!\r\n" + "注册号:" + s_batch_code + "\r\n" + "票据号:" + s_bill_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//				///}
		//				//else
		//				//{
		//				//	XtraMessageBox.Show("退费发票开具成功!!!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//				//	XtraMessageBox.Show("票据号:" + s_bill_no + "\r\n注册号:" + s_batch_code + "\r\n");
		//				//}
		//				//if (XtraMessageBox.Show("现在打印【财政发票】吗?" + "\r\n" + "发票号:" + s_bill_no, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		//				//{
		//				//	PrintInvoice(s_batch_code, s_bill_no);
		//				//}
		//				return 1;
		//			}
		//			else
		//			{
		//				Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
		//				XtraMessageBox.Show("退费发票开具失败!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//				return -1;
		//			}
		//		}
		//		else
		//		{
		//			XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//			return -1;
		//		}
		//	}
		//	else
		//	{
		//		XtraMessageBox.Show("初始化失败");
		//		return -1;
		//	}			 
		//}

		public static int InvoiceElec_Refund(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			OracleDataReader reader_fa01 = SqlAssist.ExecuteReader("select * from refund where rf001 = :fa001", new OracleParameter[] { op_fa001 });
			string s_ori_fa001 = string.Empty;
			string s_reason = string.Empty;
			decimal dec_rf004 = decimal.Zero;

			///检索原收费流水号
			while (reader_fa01.Read())
			{
				s_ori_fa001 = reader_fa01["RF300"].ToString();
				s_reason = reader_fa01["RF003"].ToString();
				dec_rf004 = Convert.ToDecimal(reader_fa01["RF004"]);
			}
			reader_fa01.Dispose();


			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();
			Dictionary<string, object> bdata = new Dictionary<string, object>();

			bdata.Add("serial_number", "MDJBYG" + s_ori_fa001);       //原电子票据业务流水号
			bdata.Add("scarlet_serial_number", "MDJBYG" + fa001);     //红字电子票据业务流水号
			bdata.Add("scarlet_bill_code", Envior.FIN_CODE);          //红字电子票据种类编码
			bdata.Add("place_code", Envior.FIN_BILL_SITE);            //开票点编码
			bdata.Add("date", string.Format("{0:yyyy-MM-dd}", MiscAction.GetServerTime()));  //开票日期		
			bdata.Add("author", Envior.cur_userName);                 //开票人
			bdata.Add("writeoff_reason", s_reason);                   //红字原因

			msg.Add("message", bdata);

			string s_json = Tools.ConvertObjectToJson(msg);
			//LogUtils.Error(s_json);

			StringBuilder sb_paras = new StringBuilder(500);
			StringBuilder sb_big = new StringBuilder(500);
			string s_datetime = string.Format("{0:yyyyMMddHHmmssfff}", MiscAction.GetServerTime());
			string s_bjson = Tools.ConvertObjectToJson(msg);
			string s_msg_64 = Tools.EncodeBase64("utf-8", s_bjson);
			string s_msg_id = Tools.GetEntityPK("FINREQ");

			////// 计算 MD5 
			sb_big.Append(Envior.FIN_AGENCY_CODE);
			sb_big.Append(Envior.FIN_APPID);
			sb_big.Append(s_datetime);
			sb_big.Append("0");                                          //是否加密
			sb_big.Append("JSON");
			sb_big.Append(s_msg_64);                                     //message
			sb_big.Append(s_msg_id);                                     //message_id	
			sb_big.Append("invoice.e.writeOff.bySN.do");                 //method	
			sb_big.Append(Envior.FIN_REGION_CODE);                       //region_code
			sb_big.Append(Envior.FIN_VERSION);                           //version
			string s_md5 = Tools.EncryptWithMD5(Envior.FIN_APPKEY + sb_big.ToString() + Envior.FIN_APPKEY).ToUpper();

			StringBuilder sb_pdata = new StringBuilder(500);
			sb_pdata.Append("{\"ParamsStr\":\"" + "agency_code=" + Envior.FIN_AGENCY_CODE + "&" + "app_id=" + Envior.FIN_APPID + "&");
			sb_pdata.Append("datetime=" + s_datetime + "&");
			sb_pdata.Append("encryption=0&");
			sb_pdata.Append("format=JSON&");
			sb_pdata.Append("message=" + s_msg_64 + "&");
			sb_pdata.Append("message_id=" + s_msg_id + "&");
			sb_pdata.Append("method=invoice.e.writeOff.bySN.do&");
			sb_pdata.Append("region_code=" + Envior.FIN_REGION_CODE + "&");
			sb_pdata.Append("security=" + s_md5 + "&");
			sb_pdata.Append("version=" + Envior.FIN_VERSION + "\"}");

			if (CallinitParams(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY) > 0)
			{
				StringBuilder sb_res = new StringBuilder(600);
				//LogUtils.Debug(sb_pdata.ToString());
				FinInvoice.CallService(sb_pdata.ToString(), sb_res);
				//LogUtils.Debug(sb_res.ToString());

				Dictionary<string, object> retdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(sb_res.ToString());
				if (retdata != null)
				{
					if (retdata.ContainsKey("message"))
					{
						string s_batch_code = string.Empty;
						string s_bill_no = string.Empty;
						Dictionary<string, string> success_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["message"].ToString());
						s_batch_code = success_msg["scarlet_bill_batch_code"];
						s_bill_no = success_msg["scarlet_bill_no"];

                        if (FinInvoiceLog(fa001, Envior.FIN_CODE, s_bill_no, s_batch_code, dec_rf004, Envior.cur_userId) > 0)
                        {
                            XtraMessageBox.Show("红字发票开具成功!\r\n" + "注册号:" + s_batch_code + "\r\n" + "票据号:" + s_bill_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show("发票开具成功!!!但记录日志失败，请与管理员联系！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            XtraMessageBox.Show("票据号:" + s_bill_no + "\r\n注册号:" + s_batch_code + "\r\n");
                        }                        
                        return 1;
					}
					else
					{
						Dictionary<string, string> error_msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(retdata["error_message"].ToString());
						LogUtils.Fatal(sb_res.ToString());
						XtraMessageBox.Show("开财政发票失败!!" + error_msg["error_msg"] + "\r\n" + "错误代码:" + error_msg["error_code"], "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return -1;
					}
				}
				else
				{
					XtraMessageBox.Show("未获取到数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
			}
			else
			{
				XtraMessageBox.Show("初始化失败");
				return -1;
			} 
		}

	}
}
