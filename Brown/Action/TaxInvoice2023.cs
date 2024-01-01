using Brown.Domain;
using Brown.Misc;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace Brown.Action
{
    //税务2023新版发票系统
    class TaxInvoice2023
    {
		[DllImport("kernel32.dll")]
		public static extern int WinExec(string programPath, int operType);


		private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}



		/// <summary>
		/// 获取下一张可用的税务发票号码
		/// </summary>
		/// <returns></returns>
		public static int GetNextInvoiceNo()
        {
			if(string.IsNullOrEmpty(Envior.TAX_MACHINECODE) && string.IsNullOrEmpty(Envior.TAX_EXTENSION))
            {
				XtraMessageBox.Show("此工作站未配置税控盘编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return -1;
			}

			string url = Envior.TAX_SERVER_URL + "getNextBillNo/" + Envior.TAX_EXTENSION + "/" + Envior.TAX_MACHINECODE;

			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version10;
			 
			// 设置请求方式
			request.Method = "GET";
            // 网络请求打开
            try
            {
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射				 
					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
                    if (taxResult.code.Equals("E0000"))
                    {
						Envior.NEXT_BILL_CODE = taxResult.data.Substring(0, taxResult.data.Length - 8);  //发票代码
						Envior.NEXT_BILL_NUM = taxResult.data.Substring(taxResult.data.Length - 8, 8);	 //发票号码
						return 1;
                    }
                    else
                    {
						XtraMessageBox.Show(taxResult.desc,"提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						return -1;
                    }
				}
			}
			catch(Exception e)
            {
				XtraMessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
            }			 
        }


		/// <summary>
		/// 发票开具
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int Invoice(string fa001)
        {
			if (string.IsNullOrEmpty(Envior.TAX_MACHINECODE) && string.IsNullOrEmpty(Envior.TAX_EXTENSION))
			{
				XtraMessageBox.Show("此工作站未配置税控盘编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return -1;
			}

			string url = Envior.TAX_SERVER_URL + "kp/" + fa001;
			
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version10;

			// 设置请求方式
			request.Method = "GET";
			// 网络请求打开
			try
			{
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射				 
					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
					if (taxResult.code.Equals("E0000"))
					{						
						return 1;
					}
					else
					{
						XtraMessageBox.Show(taxResult.desc, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
				}
			}
			catch (Exception e)
			{
				XtraMessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}

		public static int Refund(string fa001)
        {
			if (string.IsNullOrEmpty(Envior.TAX_MACHINECODE) && string.IsNullOrEmpty(Envior.TAX_EXTENSION))
			{
				XtraMessageBox.Show("此工作站未配置税控盘编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return -1;
			}

			string url = Envior.TAX_SERVER_URL + "refund/" + fa001;
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version10;

			// 设置请求方式
			request.Method = "GET";
			// 网络请求打开
			try
			{
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射				 
					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
					if (taxResult.code.Equals("E0000"))
					{
						return 1;
					}
					else
					{
						XtraMessageBox.Show(taxResult.desc, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
				}
			}
			catch (Exception e)
			{
				XtraMessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}


		public static int QueryInvoice(string fa001)
        {
			string lsh = getInvoiceSequence(fa001);
            if (string.IsNullOrEmpty(lsh))
            {
				XtraMessageBox.Show("当前结算记录未找到对应的开票序列号!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return -1;
            }

			string url = Envior.TAX_SERVER_URL + "query/" + lsh;
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version10;


			// 设置请求方式
			request.Method = "GET";
			// 网络请求打开
			try
			{
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射				 
					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
					if (taxResult.code.Equals("E0000"))
					{
						return 1;
					}
					else
					{
						XtraMessageBox.Show(taxResult.desc, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return -1;
					}
				}
			}
			catch (Exception e)
			{
				XtraMessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
		}


		/// <summary>
		/// 根据结算流水号返回开票流水号
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string getInvoiceSequence(string fa001)
        {
			string lsh = SqlAssist.ExecuteScalar("select invoiceId from tax_log2 where fa001='" + fa001 + "'").ToString();
			if (lsh == null || string.IsNullOrEmpty(lsh))
				return "";
			else
				return lsh;
        }


		/// <summary>
		/// 返回税务发票开票状态
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string getInvoiceStatus(string fa001)
        {
            OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_GetTaxInvoiceStatus(:ic_fa001) from dual", new OracleParameter[] { op_fa001 });
			return re.ToString();			 
        }

		public static int getTaxItems(string fa001)
        {
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_GetTaxItems(:ic_fa001) from dual", new OracleParameter[] { op_fa001 });
			return Convert.ToInt32(re);
		}


		/// <summary>
		/// 返回已开具发票代码和号码
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string getInvoiceNo(string fa001)
        {
			string result = string.Empty;
			using(OracleDataReader reader = SqlAssist.ExecuteReader("select invoicecode,invoiceno from tax_log2 where fa001='" + fa001 + "'"))
            {
				if(reader.HasRows && reader.Read())
                {
					result = reader["INVOICECODE"].ToString() + reader["INVOICENO"].ToString();
                }
            }
			return result;
        }

		/// <summary>
		/// 返回发票打印流水号
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string getInvoicePrintId(string fa001)
        {
			string lsh = getInvoiceSequence(fa001);
			string url = Envior.TAX_SERVER_URL + "getInvoicePrintNo/" + lsh;
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version10;


			// 设置请求方式
			request.Method = "GET";
			// 网络请求打开
			try
			{
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射				 
					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
					if (taxResult.code.Equals("E0000"))
					{
						return taxResult.data;
					}
					else
					{
						XtraMessageBox.Show(taxResult.desc, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return "";
					}
				}
			}
			catch (Exception e)
			{
				XtraMessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}
		}

		/// <summary>
		/// 打印税务发票|清单
		/// </summary>
		/// <param name="printId"></param>
		public static void print_Invoice(string printId,string flag)
        {
            if (string.IsNullOrEmpty(Envior.TAX_PRINTASSIST_LOCATION))
            {
				XtraMessageBox.Show("没有正确配置诺诺打印助手!", "提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
            }
		 
			string pathStr = string.Empty;
			if (flag == "2")
				//Process.Start(Envior.TAX_PRINTASSIST_LOCATION + " webprint:2," + printId);
				pathStr = Envior.TAX_PRINTASSIST_LOCATION + " webprint:2," + printId;
			else
				pathStr = Envior.TAX_PRINTASSIST_LOCATION + " webprint:3," + printId;


			var result = WinExec(pathStr, (int)ShowWindowCommands.Show);

		}

    }
}
