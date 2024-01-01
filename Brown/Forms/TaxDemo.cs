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
using Brown.Action;
using System.Web;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Brown.Misc;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Printing;
using Brown.Domain;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Brown.Forms
{
	
	public partial class TaxDemo : BaseDialog
	{
		/// <summary>
		/// 税务发票明细项
		/// </summary>
		class C_params
		{
			public string region_code { get; set; }
			public string agency_code { get; set; }
			public string format { get; set; }
			public string datetime { get; set; }
			public string encryption { get; set; }
			public string message_id { get; set; }
			public string message { get; set; }
			public string app_id { get; set; }
			public string version { get; set; }
			public string method { get; set; }
			public string sign { get; set; }
		}

		public class C_detail
		{
			public string xh;       //序号
			public string fphxz;    //发票行性质 0 正常行1 折扣行2 被折扣行
			public string spmc;     //商品名称
			public string ggxh;     //规格型号(可空)
			public string dw;       //单位(可空)
			public string spsl;     //商品数量(可空)
			public string dj;       //单价(可空)
			public string je;       //金额
			public string sl;       //税率
			public string se;       //税额
			public string hsbz;     //含税标志 0-不含税 1-含税
			public string spbm;     //商品编码 税收分类编码
			public string zxbm;     //自行编码(可空)
			public string yhzcbs;   //优惠政策标识 0是不使用，1是使用
			public string slbs;     //税率标识(可空)  空，是正常税率 1-免税 2-是不征税 3-普通零税率
			public string zzstsgl;  //增值税特殊管理(可空)

		}



		public TaxDemo()
		{
			InitializeComponent();
		}


		//获取当前未开具发票号码
		private void simpleButton1_Click(object sender, EventArgs e)
		{
			//业务数据
			Dictionary<string, Object> bdata = new Dictionary<string, object>();
		 
			bdata.Add("fplxdm", "007");   //发票类型代码
			string s_json = Tools.ConvertObjectToJson(bdata);
			memoEdit1.EditValue = s_json;

			string s_inputmi = Tools.AesEncrypt(s_json, Envior.TAX_PRIVATE_KEY);
			memoEdit2.EditValue = s_inputmi;

			//完整请求数据
			Dictionary<string, object> fulldata = new Dictionary<string, object>();
			fulldata.Add("async", "true");
			fulldata.Add("input", s_inputmi);
			fulldata.Add("nsrsbh", "12231000414356488Q");
			fulldata.Add("appid", "207547b9-e5c8-414a-af47-df726867cbaa");
			fulldata.Add("serviceid", "HQDQFPDMHM");
			fulldata.Add("sid", "00000000049");
			

			string s_json2 = Tools.ConvertObjectToJson(fulldata);
			memoEdit3.EditValue = s_json2;
			string s_fullmi = Tools.AesEncrypt(s_json2, Envior.TAX_PUBLIC_KEY);
			memoEdit1.EditValue = s_fullmi;

			string s_urlencode = HttpUtility.UrlEncode(s_fullmi);
			memoEdit5.EditValue = s_urlencode;


			///准备发送报文
			Dictionary<string, string> body = new Dictionary<string, string>();
			body.Add("json",s_urlencode);

			string s_url = "https://taxsapi.holytax.com/v1/api/s";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s_url);

			request.Timeout = 60000;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.KeepAlive = true;



			memoEdit6.EditValue =  s_urlencode  ;

			string s_send =  @"json=F1hq3p639eB9WOxKFQ9Bvk9NXIov%2B9Ri1jdLgv29OPtY4nd6mDwpg661iB%2F2WFaSEkZKW8jvg84oIQwhQZGeEz4V5muaxp2Fs%2FEpv7sVhttuJ21KaD6QiP2YyXPaCKB4gKBT2eWrcb%2BJkIEvvvJCqJUqvvUkvgh%2F1nwzXYIAbw%2FJZ7ezDx5bL%2B4pihsCs8Eldb6Ui8nMfOu76ElFlOH3f1LTWdjoOyfRnffo%2BbVsaRCZ%2F385RqXV89PvHz74E%2FKRFoZeOnJ%2FfOs1YDJz%2BJdfSw%3D%3D";

			byte[] buf = System.Text.Encoding.UTF8.GetBytes(s_send);

	 
  
			Stream myRequestStream = request.GetRequestStream();
 
			myRequestStream.Write(buf, 0, buf.Length);
			//myRequestStream.Flush();

			myRequestStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			string retString = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();

			memoEdit7.EditValue = retString;


		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			string s_source = memoEdit1.Text;
			memoEdit2.EditValue = HttpUtility.UrlEncode(s_source);
		}

		private void simpleButton3_Click(object sender, EventArgs e)
		{
			Dictionary<string, string> body = new Dictionary<string, string>();
			body.Add("json", memoEdit1.Text);
			string postData =   HttpUtility.UrlEncode(Tools.AesEncrypt(memoEdit3.Text,Envior.TAX_PUBLIC_KEY));

			Stream outstream = null;
			Stream instream = null;
			StreamReader sr = null;
			HttpWebResponse response = null;
			HttpWebRequest request = null;
			Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");

			byte[] data = encoding.GetBytes(postData);
			//var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			try
			{
				// 设置参数
				request = WebRequest.Create("https://taxsapi.holytax.com/v1/api/s") as HttpWebRequest;
				CookieContainer cookieContainer = new CookieContainer();
				request.CookieContainer = cookieContainer;
				request.AllowAutoRedirect = true;
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = data.Length;
				outstream = request.GetRequestStream();
				outstream.Write(data, 0, data.Length);
				outstream.Close();

				//发送请求并获取相应回应数据
				response = request.GetResponse() as HttpWebResponse;
				//直到request.GetResponse()程序才开始向目标网页发送Post请求
				instream = response.GetResponseStream();
				sr = new StreamReader(instream, encoding);
				//返回结果网页（html）代码
				string content = sr.ReadToEnd();
				string err = string.Empty;
				memoEdit4.Text = content;
			}
			catch (Exception ex)
			{
				string err = ex.Message;
			}
		}

		private void simpleButton4_Click(object sender, EventArgs e)
		{
			memoEdit6.Text = Tools.AesDecrypt("B6TEQq8VhZW7ihNPEF1gwX9A1ZX7q8vb80KV3kICKgg=", Envior.TAX_PRIVATE_KEY);
			memoEdit7.Text = Tools.AesDecrypt("XNcG0MOW5YNy5c7iNVK4xolPghB+5yZsUTBdk4JakMBiGJz59fJ523C+Qgqj+vugpYeelgr6NaOa6siGVGrUVw==",Envior.TAX_PRIVATE_KEY);
		}

		private void simpleButton5_Click(object sender, EventArgs e)
		{
			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", HttpUtility.UrlEncode(Tools.AesEncrypt(memoEdit3.Text, Envior.TAX_PUBLIC_KEY)));
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			XtraMessageBox.Show(retstr);
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject; 
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);

			XtraMessageBox.Show(result);

			Object obj2 = JsonConvert.DeserializeObject(result);
			Newtonsoft.Json.Linq.JObject js2 = obj2 as Newtonsoft.Json.Linq.JObject;
			XtraMessageBox.Show(js2["fpdm"].ToString());
			//Console.WriteLine(response.Content);
		}


		/// <summary>
		/// 发票开具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton6_Click(object sender, EventArgs e)
		{
			Dictionary<string, Object> bdata = new Dictionary<string, object>();
			bdata.Add("fplxdm", "007");				//发票类型代码
			bdata.Add("kplx","0");					//开票类型 0-正数发票 1-负数
			bdata.Add("tspz", "00");				//特殊票种 00-不是 01-农产品销售 02-农产品收购
			bdata.Add("xhdwdzdh", "省农机公司");    //销货单位地址电话
			bdata.Add("xhdwyhzh", "农业银行");      //销货单位银行账号
			bdata.Add("ghdwsbh", "");               //购货单位纳税识别号
			bdata.Add("ghdwmc", "黑龙江电视台");	//购货单位名称
			bdata.Add("ghdwdzdh","汉水路");			//购货单位地址电话
			bdata.Add("ghdwyhzh", "");			    //购货单位银行账号

			List<C_detail> detaildata = new List<C_detail>();
			C_detail c_detail = new C_detail();
			c_detail.xh = "1";
			c_detail.fphxz = "0";                     //发票行性质	
			c_detail.ggxh = "";                       //规格型号
			c_detail.dw = "";						  //单位
			c_detail.spmc = "卫生棺A";				  //商品名称
			c_detail.spsl = "1";
			c_detail.dj = "12.18";
			c_detail.je = "12.18";
			c_detail.sl = "0.03";
			c_detail.se = "0.37";					   //税额						
			c_detail.hsbz = "0";
			//c_detail.spbm = "3070401000000000000";    //商品编码
			c_detail.spbm = "1050104050000000000";
			c_detail.zxbm = "";
			c_detail.yhzcbs = "0";
			c_detail.slbs = "";
			c_detail.zzstsgl = "";
			detaildata.Add(c_detail);
			bdata.Add("mx",detaildata);
			bdata.Add("hjje","12.18");             //合计金额
			bdata.Add("hjse","0.37");              //合计税额
			bdata.Add("jshj","12.55");             //价税合计
			bdata.Add("bz", "");                   //备注
			bdata.Add("skr","张三");               //收款人
			bdata.Add("fhr", "复核人");            //复核人
			bdata.Add("kpr", "开票人");            //开票人
			bdata.Add("tzdbh","");                 //通知单编号 专票红字必填
			bdata.Add("yfphm", "");                //原发票号码 负数发票必填
			bdata.Add("yfpdm", "");                //原发票代码 负数发票必填
			bdata.Add("gmf_dzyx", "");             //购买方电子邮箱 推送使用，电子发票，购买方电子邮箱和手机号码微信id三个必填一
			bdata.Add("gmf_sjhm", "13333335678");  //购买方手机号码
			bdata.Add("gmf_openid", "");           //购买方微信id
 
			string s_business_json = Tools.ConvertObjectToJson(bdata);
			memoEdit1.EditValue = s_business_json;

			string s_inputmi = Tools.AesEncrypt(s_business_json, Envior.TAX_PRIVATE_KEY);
			memoEdit2.EditValue = s_inputmi;

			//完整请求数据
			Dictionary<string, object> fulldata = new Dictionary<string, object>();
			fulldata.Add("async", "true");
			fulldata.Add("input", s_inputmi);
			fulldata.Add("nsrsbh", "12231000414356488Q");
			fulldata.Add("appid", "207547b9-e5c8-414a-af47-df726867cbaa");
			fulldata.Add("serviceid", "FPKJ");
			fulldata.Add("sid","00000000042");
		

			string s_full_json = Tools.ConvertObjectToJson(fulldata);
			string s_fullmi = Tools.AesEncrypt(s_full_json, Envior.TAX_PUBLIC_KEY);
			memoEdit3.Text = s_full_json;
			memoEdit4.Text = s_fullmi;

			/////准备要发送的报文 urlencode编码
			string s_urlencode = HttpUtility.UrlEncode(s_fullmi);
			memoEdit5.Text = s_urlencode;

			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_urlencode);
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			memoEdit6.Text = retstr;

			////处理返回信息
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);
			memoEdit7.Text = result;
			//////// 成功返回 
			/// {"kpjh":"","fpdm":"150003521030","fplxdm":"026","hjse":"0.37","kprq":"20191223132103","jshj":"12.55","mw":"0311425<<32*-<<6<>265<0883<5+615*62938-2116<-15>*62/260*<-11425<<32*-<<6<>69<984080/<->9*85>3-01894319533556+-74","hjje":"12.18","jym":"11350443984358496456","fphm":"16798030"}

		}

		private void TaxDemo_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 连接开票服务器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton7_Click(object sender, EventArgs e)
		{
			 
		}

		private void simpleButton8_Click(object sender, EventArgs e)
		{
			 
		}

		private void simpleButton9_Click(object sender, EventArgs e)
		{
			//string pjh = FinInvoice.GetCurrentPh("04007002");
			//if (!string.IsNullOrEmpty(pjh))
			//{
			//	MessageBox.Show("新的票据号" + pjh);
			//	memoEdit1.Text = pjh;
			//}
			//else
			//{
			//	MessageBox.Show("获取票据号失败!");
			//}
		}

		/// <summary>
		/// 开票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton10_Click(object sender, EventArgs e)
		{
			//string s_content = "<&票据><&票据头>姓名=发票测试		</&票据头><&收费项目>收费项目=10000	计费数量=1	收费标准=100.00	金额=100	收费项目=20000	计费数量=1	收费标准=18.00	金额=18	</&收费项目></&票据>";
			//string retstr = FinInvoice.Invoice(s_content, 1, "04007002", "");
			//memoEdit2.Text = retstr;
			/////成功返回
			/////成功:04007002,56140002,118,财乙（2010) 
		}

		/// <summary>
		/// 财政发票作废
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton11_Click(object sender, EventArgs e)
		{
			//string retstr = FinInvoice.Remove("票据类型=14004003|票据号=00040005|注册号=1114512");
			//memoEdit3.Text = retstr;
		}

		private void simpleButton12_Click(object sender, EventArgs e)
		{
			//string retstr = FinInvoice.Refund("04007002", "56140002", "财乙（2010)", "04007002", "10000	65", "F_Qt1=xxx|F_Qt2=xxx|F_Qt3=xxx");
			//memoEdit3.Text = retstr;
		}

		private void simpleButton13_Click(object sender, EventArgs e)
		{
			//int result = FinInvoice.AdvConnect("001", "", "");

			//if (result > 0)
			//	XtraMessageBox.Show("连接开票服务器成功!");
			//else
			//	XtraMessageBox.Show("连接开票服务器失败!");
		}

		private void simpleButton14_Click(object sender, EventArgs e)
		{
			//if (FinInvoice.IsConnect())
			//	XtraMessageBox.Show("已经连接!");
			//else
			//	XtraMessageBox.Show("尚未连接!");
		}


		//税务发票作废
		private void simpleButton15_Click(object sender, EventArgs e)
		{
			Dictionary<string, Object> bdata = new Dictionary<string, object>();
			bdata.Add("fplxdm", "026");             //发票类型代码
			bdata.Add("fpdm", "150003521030");      //发票代码
			bdata.Add("fphm", "16798030");          //发票号码
			bdata.Add("hjje", "12.18");             //合计金额
			bdata.Add("zfr", "root");               //合计金额

			string s_business_json = Tools.ConvertObjectToJson(bdata);
			memoEdit1.EditValue = s_business_json;

			string s_inputmi = Tools.AesEncrypt(s_business_json, Envior.TAX_PRIVATE_KEY);
			memoEdit2.EditValue = s_inputmi;

			//完整请求数据
			Dictionary<string, object> fulldata = new Dictionary<string, object>();
			fulldata.Add("async", "true");
			fulldata.Add("input", s_inputmi);
			fulldata.Add("nsrsbh", "110101201707010054");
			fulldata.Add("appid",Envior.TAX_APPID);
			fulldata.Add("serviceid", "FPZF");
			fulldata.Add("sid", "00000000025");


			string s_full_json = Tools.ConvertObjectToJson(fulldata);
			string s_fullmi = Tools.AesEncrypt(s_full_json, Envior.TAX_PUBLIC_KEY);
			memoEdit3.Text = s_full_json;
			memoEdit4.Text = s_fullmi;

			/////准备要发送的报文 urlencode编码
			string s_urlencode = HttpUtility.UrlEncode(s_fullmi);
			memoEdit5.Text = s_urlencode;

			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_urlencode);
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			memoEdit6.Text = retstr;

			////处理返回信息
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);
			memoEdit7.Text = result;


		}

		private void simpleButton16_Click(object sender, EventArgs e)
		{
			//1.加密业务数据
			//string s_inputmi = "";

			//2.打包发送请求数据
			Dictionary<string, object> sendmsg = new Dictionary<string, object>();

			sendmsg.Add("serviceid", "DJAPP");
			//sendmsg.Add("sid", "0000000060");                  //请求流水号	
			sendmsg.Add("dev_key", "9011e4c9-4070-4a0e-a571-baadb7588723");
			 
			sendmsg.Add("app_name", "牡丹江市第二殡仪馆");
			//sendmsg.Add("app_name", "鸡西市殡葬事务服务中心");
			//sendmsg.Add("app_name", "牡丹江市龙凤公墓服务站");
			//3.将请求报文转换为Json 字符串 并整体用公钥 加密
			string s_msg_json = Tools.ConvertObjectToJson(sendmsg);
			string s_fullmi = Tools.AesEncrypt(s_msg_json, Envior.TAX_PUBLIC_KEY);

			//4.加密后的字符串用 urlencode 编码,生产最终发送数据!!!
			string s_post = HttpUtility.UrlEncode(s_fullmi);

			//5.发送数据
			var client = new RestClient("https://taxsapi.holytax.com/v1/api/s");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_post);
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			
			memoEdit1.Text = retstr;
			//XtraMessageBox.Show(retstr);
			Object obj = JsonConvert.DeserializeObject(retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			string data = js["data"].ToString();
			string result = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);
			memoEdit2.Text = result;


		}

		private void simpleButton17_Click(object sender, EventArgs e)
		{
			string s_appkey = "0d3ae7af6c20c68a89390a6ec445";
			Dictionary<string, string> bdata = new Dictionary<string, string>();
			Dictionary<string, Dictionary<string, string>> msg = new Dictionary<string, Dictionary<string, string>>();

		 	bdata.Add("place_code", "001");
			bdata.Add("bill_batch_code", "3910");

			msg.Add("message", bdata);
			memoEdit1.Text = Tools.ConvertObjectToJson(msg);


			string s_bdata_base64 = Tools.EncodeBase64("utf-8", Tools.ConvertObjectToJson(msg));
			memoEdit2.Text = s_bdata_base64;


			C_params c_params = new C_params();
			c_params.agency_code = "021099001";
			c_params.app_id = "MDJSDYBYG7838387";
			c_params.format = "json";
			c_params.datetime = "20171206185418124";
			c_params.encryption = "0";
			c_params.message_id = "0.7576015286829508";
			c_params.message = s_bdata_base64;
			c_params.method = "stock.billno.get";
			c_params.region_code = "231001";
			c_params.version = "1.0.1";



			StringBuilder s_big = new StringBuilder(100);
			s_big.Append(c_params.agency_code);
			s_big.Append(c_params.app_id);
			s_big.Append(c_params.datetime);
			s_big.Append(c_params.encryption);
			s_big.Append(c_params.format);
			s_big.Append(c_params.message);
			s_big.Append(c_params.message_id);
			s_big.Append(c_params.method);
			s_big.Append(c_params.region_code);
			s_big.Append(c_params.version);

			string s_1 = s_appkey + s_big.ToString() + s_appkey;
			string s_md5 = Tools.EncryptWithMD5(s_1).ToUpper();

			StringBuilder sb_url = new StringBuilder(100);
			//sb_url.Append("http://111.41.51.157:7001/mergestandard/agency3/rest.do");
			sb_url.Append("agency_code=" + c_params.agency_code + "&");
			sb_url.Append("app_id=" + c_params.app_id + "&");
			sb_url.Append("datetime=" + c_params.datetime + "&");
			sb_url.Append("encryption=" + c_params.encryption + "&");
			sb_url.Append("format=" + c_params.format + "&");
			sb_url.Append("message=" + s_bdata_base64 + "&");
			sb_url.Append("message_id=" + c_params.message_id + "&");
			sb_url.Append("method=" + c_params.method + "&");
			sb_url.Append("region_code=" + c_params.region_code + "&");
			sb_url.Append("security=" + s_md5 + "&");
			sb_url.Append("version=" + c_params.version);


			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://111.41.51.157:7001/mergestandard/agency3/rest.do");
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			memoEdit3.Text = sb_url.ToString();
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

			memoEdit4.Text = retString;

			memoEdit5.Text = Tools.DecodeBase64("utf-8",retString );
		}
		/// <summary>
		/// 发票开具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton18_Click(object sender, EventArgs e)
		{
			string s_appkey = "0d3ae7af6c20c68a89390a6ec445";
			Dictionary<string, object> bdata = new Dictionary<string, object>();
			Dictionary<string, Dictionary<string, object>> msg = new Dictionary<string, Dictionary<string, object>>();

			bdata.Add("serial_number", "0000001005");          //业务流水号 
			bdata.Add("place_code", "001");                    //开票点
			bdata.Add("payer", "张三");                        //缴款人单位
			bdata.Add("date", "2020-10-31");                   //开票日期
			bdata.Add("author", "根用户");                     //开票人
			bdata.Add("payer_type", "1");                      //缴款人类型：1 个人 2 单位
			bdata.Add("credit_code", "");                      //组织机构代码
			bdata.Add("bill_code", "0202");                    //票据种类编码
			bdata.Add("total_amt", "700");                     //合计金额
			bdata.Add("rec_mode", "1");                        //收款方式:1现金,2转账,3其它

			
			List<Dictionary<string, object>> detail_list = new List<Dictionary<string, object>>();
			Dictionary<string, object> detail_data = new Dictionary<string, object>();
			detail_data.Add("item_code", "10304490801");   //项目名-接运
			detail_data.Add("std",200);                   //单价
			detail_data.Add("number", 2);                 //数量
			detail_data.Add("amt", 400);                  //金额
			detail_list.Add(detail_data);

			detail_data = new Dictionary<string, object>();
			detail_data.Add("item_code", "10304490802");   //项目名-火化
			detail_data.Add("std", 300);                  //单价
			detail_data.Add("number", 1);                 //数量
			detail_data.Add("amt", 300);                  //金额
			detail_list.Add(detail_data);

			bdata.Add("item_details", detail_list);
			 
			msg.Add("message", bdata);
			memoEdit1.Text = Tools.ConvertObjectToJson(msg);


			string s_bdata_base64 = Tools.EncodeBase64("utf-8", Tools.ConvertObjectToJson(msg));
			memoEdit2.Text = s_bdata_base64;


			C_params c_params = new C_params();
			c_params.agency_code = "021099001";
			c_params.app_id = "MDJSDYBYG7838387";
			c_params.format = "json";
			c_params.datetime = "20201031185418128";
			c_params.encryption = "0";
			c_params.message_id = "0.7576015286829639";
			c_params.message = s_bdata_base64;
			c_params.method = "invoice.issue.do";
			c_params.region_code = "231001";
			c_params.version = "1.0.1";
			 
			StringBuilder s_big = new StringBuilder(100);
			s_big.Append(c_params.agency_code);
			s_big.Append(c_params.app_id);
			s_big.Append(c_params.datetime);
			s_big.Append(c_params.encryption);
			s_big.Append(c_params.format);
			s_big.Append(c_params.message);
			s_big.Append(c_params.message_id);
			s_big.Append(c_params.method);
			s_big.Append(c_params.region_code);
			s_big.Append(c_params.version);

			string s_1 = s_appkey + s_big.ToString() + s_appkey;
			string s_md5 = Tools.EncryptWithMD5(s_1).ToUpper();

			StringBuilder sb_url = new StringBuilder(100);
			//sb_url.Append("http://111.41.51.157:7001/mergestandard/agency3/rest.do");
			sb_url.Append("agency_code=" + c_params.agency_code + "&");
			sb_url.Append("app_id=" + c_params.app_id + "&");
			sb_url.Append("datetime=" + c_params.datetime + "&");
			sb_url.Append("encryption=" + c_params.encryption + "&");
			sb_url.Append("format=" + c_params.format + "&");
			sb_url.Append("message=" + s_bdata_base64 + "&");
			sb_url.Append("message_id=" + c_params.message_id + "&");
			sb_url.Append("method=" + c_params.method + "&");
			sb_url.Append("region_code=" + c_params.region_code + "&");
			sb_url.Append("security=" + s_md5 + "&");
			sb_url.Append("version=" + c_params.version);


			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://111.41.51.157:7001/mergestandard/agency3/rest.do");
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			memoEdit3.Text = sb_url.ToString();
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

			memoEdit4.Text = retString;

			memoEdit5.Text = Tools.DecodeBase64("utf-8", retString);
			 
		}

		private void simpleButton19_Click(object sender, EventArgs e)
		{			 
			if(FinInvoice.GetCurrentPh() > 0)
			{
				XtraMessageBox.Show(Envior.FIN_NEXT_BATCH_CODE, "注册号");
				XtraMessageBox.Show(Envior.FIN_NEXT_BILL_NO, "票号");
			}
		}

		private void simpleButton20_Click(object sender, EventArgs e)
		{
			FinInvoice.PrintInvoice("3910", "00356022");
		}
		/// <summary>
		/// 参数初始化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton21_Click(object sender, EventArgs e)
		{
			//int result = FinInvoice.initDll(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY);
			//XtraMessageBox.Show(result.ToString(),"结果");
			FinInvoice.PrintInvoice("1", "2");
		}

        private void simpleButton22_Click(object sender, EventArgs e)
        {
			int result = FinInvoice.InvoiceRemoved("3910", "00356003");
			if(result > 0)
            {
				XtraMessageBox.Show("作废发票成功!");
            } 
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
			string s_state = FinInvoice.GetInvoiceState("3910", "00356025");
			if (!string.IsNullOrEmpty(s_state))
				XtraMessageBox.Show(s_state, "状态");
			else
				XtraMessageBox.Show("未获取到状态");
        }
		/// <summary>
		/// 退费
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void simpleButton24_Click(object sender, EventArgs e)
        {
			///FinInvoice.InvoiceElec_Refund("");
			//FinInvoice.InvoiceElec_Refund();
		}
		/// <summary>
		/// 获取电子票据号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton25_Click(object sender, EventArgs e)
		{
			if (FinInvoice.GetCurrentElecPh() > 0)
			{
				XtraMessageBox.Show(Envior.FIN_NEXT_BATCH_CODE);
				XtraMessageBox.Show(Envior.FIN_NEXT_BILL_NO);
			}
		}

		private void simpleButton26_Click(object sender, EventArgs e)
		{
			//FinInvoice.InvoiceElec();

			string s_url = "{\"ParamsStr\":\"" + "agency_code=021099001&app_id=MDJSDYBYG5537156&datetime=20210717093928000&encryption=0&format=json&message=eyJtZXNzYWdlIjp7InNlcmlhbF9udW1iZXIiOiIwMDAyNzgwNTcxIiwicGxhY2VfY29kZSI6IjAwMiIsInBheWVyIjoi5rWL6K+VZCIsImRhdGUiOiIyMDIxLTA3LTE3IiwiYXV0aG9yIjoi5qC555So5oi3IiwicGF5ZXJfdHlwZSI6IjEiLCJjcmVkaXRfY29kZSI6IiAiLCJiaWxsX2NvZGUiOiIwMTMzIiwicmVjX21vZGUiOiIxIiwibWVtbyI6Ilx0ICIsIml0ZW1fZGV0YWlscyI6W3siaXRlbV9jb2RlIjoiMTAzMDQ0OTA4MDQiLCJzdGQiOjEuMCwibnVtYmVyIjoxLjAsImFtdCI6MS4wfV0sInRvdGFsX2FtdCI6MS4wfX0=&message_id=0000000286&method=invoice.e.issue.do&region_code=231001&security=7490CE8A0DA82F13C7D1A332E3E48D00&version=1.0.1\"}";
			
			StringBuilder sb_res = new StringBuilder(500);
			//Dictionary<string, object> bdata = new Dictionary<string, object>();
			//bdata.Add("agency_code", "021099001");                   //单位编号
			//bdata.Add("app_id", "MDJSDYBYG5537156");                 //单位编号	
			//bdata.Add("datetime", "20210717093928000");
			//bdata.Add("encryption", "0");
			//bdata.Add("format", "json");
			//bdata.Add("message", "eyJtZXNzYWdlIjp7InNlcmlhbF9udW1iZXIiOiIwMDAyNzgwNTcxIiwicGxhY2VfY29kZSI6IjAwMiIsInBheWVyIjoi5rWL6K+VZCIsImRhdGUiOiIyMDIxLTA3LTE3IiwiYXV0aG9yIjoi5qC555So5oi3IiwicGF5ZXJfdHlwZSI6IjEiLCJjcmVkaXRfY29kZSI6IiAiLCJiaWxsX2NvZGUiOiIwMTMzIiwicmVjX21vZGUiOiIxIiwibWVtbyI6Ilx0ICIsIml0ZW1fZGV0YWlscyI6W3siaXRlbV9jb2RlIjoiMTAzMDQ0OTA4MDQiLCJzdGQiOjEuMCwibnVtYmVyIjoxLjAsImFtdCI6MS4wfV0sInRvdGFsX2FtdCI6MS4wfX0=");
			//bdata.Add("message_id", "9000000286");
			//bdata.Add("method", "invoice.e.issue.do");
			//bdata.Add("region_code", "231001");
			//bdata.Add("security", "7490CE8A0DA82F13C7D1A332E3E48D00");
			//bdata.Add("version", "1.0.1");

			//string s_json = Tools.ConvertObjectToJson(bdata);

			if (FinInvoice.CallinitParams(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY) > 0)
			{
				memoEdit1.Text = s_url;
				FinInvoice.CallService(s_url, sb_res);
				memoEdit2.Text = sb_res.ToString();
			}
			else
			{
				XtraMessageBox.Show("初始化失败");
			}	
		}

		private void simpleButton27_Click(object sender, EventArgs e)
		{
			memoEdit6.Text = FinInvoice.GetInvoiceImageBase64("23013321", "0000338491");

			string base64 = memoEdit6.Text;

			byte[] bytes = Convert.FromBase64String(base64);

			//System.IO.File.WriteAllBytes(@"c:\test.jpg", bytes);

			MemoryStream ms = new MemoryStream(bytes,true);

			//ms.Write(bytes, 0, bytes.Length);
			ms.Write(bytes, 0, bytes.Length);
			pictureBox1.Image = new Bitmap(ms);

			////byte[] bytes = Convert.FromBase64String(base64);
			////MemoryStream memStream = new MemoryStream(bytes);
			//BinaryFormatter binFormatter = new BinaryFormatter();
			//Image img = (Image)binFormatter.Deserialize(ms);
			//pictureEdit1.Image = img;
		}
		/// <summary>
		/// 打印电子票据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton28_Click(object sender, EventArgs e)
		{
			//打印预览
			//PrintPreviewDialog ppd = new PrintPreviewDialog();
			PrintDocument pd = new PrintDocument();
			//设置边距
			Margins margin = new Margins(10, 10, 10, 10);
			pd.DefaultPageSettings.Margins = margin;
			pd.DefaultPageSettings.Landscape = false;
			////纸张设置默认
			PaperSize pageSize = new PaperSize("First custom size", 240, 140);
			pd.DefaultPageSettings.PaperSize = pageSize;
			//打印事件设置
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			//ppd.Document = pd;
			//ppd.ShowDialog();
			try
			{
				pd.Print();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
				pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
			}
		}
		 
		//打印事件处理
		private void pd_PrintPage(object sender, PrintPageEventArgs e)
		{

			//读取图片模板
			Image temp = pictureBox1.Image; // Image.FromFile(@"Receipts.jpg");
		 
			int x = e.MarginBounds.X;
			int y = e.MarginBounds.Y;
			int width = temp.Width;
			int height = temp.Height;
			Rectangle destRect = new Rectangle(x, y, width, height-80);
			e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
		}

		private void simpleButton29_Click(object sender, EventArgs e)
		{
			FinInvoice.SendElecInvoiceNotice("23013321", "0000255805", "", "");
		}

		private void simpleButton30_Click(object sender, EventArgs e)
		{
			if (FinInvoice.CallinitParams(Envior.FIN_URL, Envior.FIN_APPID, Envior.FIN_APPKEY) > 0)
				XtraMessageBox.Show("初始化成功!");
			else
				XtraMessageBox.Show("初始化失败!");
		}

		private void simpleButton31_Click(object sender, EventArgs e)
		{
			if (FinInvoice.FinInvoiceLog("0000000000", "0133", "0000255805", "23013321", 0, "0000000000") > 0)
				XtraMessageBox.Show("日志成功");
			else
				XtraMessageBox.Show("日志失败!");

		}

		private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}


		private void simpleButton32_Click(object sender, EventArgs e)
        {
			// 连接地址
			string strUrl = "https://192.168.2.10/m2023/api/tax/getNextBillNo/0/889914681884/";
			HttpWebRequest request = null;

			try
            {
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
				request = WebRequest.Create(strUrl) as HttpWebRequest;
				request.ProtocolVersion = HttpVersion.Version10;

				// 设置请求方式
				request.Method = "GET";
				// 设置请求头
				//request.Headers.Add("key", "value");
				// 网络请求打开
				using (WebResponse wr = request.GetResponse())
				{
					// 网络返回
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					// 设置返回编码格式与读取流
					StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
					// 读取返回的信息流
					string content = reader.ReadToEnd();
					// 进行json的实体映射
					memoEdit1.Text = content;

					TaxResult taxResult = JsonConvert.DeserializeObject<TaxResult>(content);
					memoEdit2.Text = taxResult.data;
				}

			}
            catch (Exception ex)
            {
				XtraMessageBox.Show(ex.Message);
            }			 
		}






        private void simpleButton33_Click(object sender, EventArgs e)
        {
			Process.Start("C:\\Program Files (x86)\\诺诺\\诺诺打印助手升级版\\nprint.exe","webprint:2," + "A6C129FB271C277D581B842D86831880B65A3A1C7B9A755508F1F3C72A0CE72A3E28D06E1C31BF4A");
		}
    }
}