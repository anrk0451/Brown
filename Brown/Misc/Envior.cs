using Brown;
using DevExpress.XtraBars.Ribbon;
//using PrtServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brown.Misc
{
	class Envior
	{
		public static MainForm mform { get; set; }          //当前主窗口
        public static string cur_user { get; set; }         //当前登录用户
        public static string cur_userId { get; set; }       //当前登录用户Id
        public static string cur_userName { get; set; }     //当前登录用户名

        
        //public static string cur_userBosi  { get; set; }    //博思服务账号
        //public static string cur_pwdBosi { get; set; }      //博思服务密码
 
        ////关于财政发票
        //public static bool FINANCE_INVOICE_HANDLE { get; set; }   //是否允许开具财政发票操作
        
        //public static string FIN_INVOICE_TITLE { get; set; }      //财政发票交款人标题
        //public static string FIN_INVOICE_TYPE { get; set; }       //财政发票票据类型

        public static string FIN_REGION_CODE { get; set; }        //财政发票-区划
        public static string FIN_VERSION { get; set; }            //版本
        public static string FIN_AGENCY_CODE { get; set; }        //单位编码
        public static string FIN_AGENCY_NAME { get; set; }        //单位名称
        public static string FIN_URL { get; set; }                 //财政发票服务URL
        public static string FIN_APPID { get; set; }               //appid
        public static string FIN_APPKEY { get; set; }              //app key
        public static string FIN_BATCH_CODE { get; set; }          //票据代码(以前的注册号)
        public static string FIN_CODE { get; set; }                //票据种类编码(以前的票据类型)
        public static string FIN_BILLNAME { get; set; }            //票据名称
        public static string FIN_BILL_SITE { get; set; }           //开票点编码
        
        public static string FIN_NEXT_BATCH_CODE { get; set; }     //财政票（下一张注册号）
        public static string FIN_NEXT_BILL_NO { get; set; }        //财政票(下一张票号)
 

        public static string NEXT_BILL_CODE { get; set; }     //下张发票代码
        public static string NEXT_BILL_NUM { get; set; }      //下张发票票号
        public static string TAX_ID { get; set; }             //纳税识别号
        public static string TAX_ADDR_TELE { get; set; }      //税务-销方地址电话
        public static string TAX_BANK_ACCOUNT { get; set; }   //税务-销方银行&账号
        public static string TAX_APPID { get; set; }          //税务 appId
        public static string TAX_INVOICE_TYPE { get; set; }   //发票类型
        public static string TAX_PUBLIC_KEY { get; set; }     //公钥
        public static string TAX_PRIVATE_KEY { get; set; }    //私钥
        public static string TAX_SERVER_URL { get; set; }     //税务发票服务URL  
        public static string TAX_CASHIER { get; set; }        //税务发票-收款人
        public static string TAX_CHECKER { get; set; }        //税务发票-复核人
        public static string TAX_EXTENSION { get; set; }      //税务发票-分机号  
        public static string TAX_MACHINECODE { get; set; }    //税务发票-税控盘号
        public static string TAX_PRINTASSIST_LOCATION { get; set; }   //税务发票-打印助手安装位置


        
        public static string WORKSTATIONID { get; set; }      //工作站ID


        public static string[] rolearry { get; set; }      //所属角色组
        public static char loginMode { get; set; }         //登陆模式

        //public static bool printable { get; set; }       //打印进程是否启动
		public static bool canInvoice { get; set; }		   //当前的用户允许开发票
        public static IntPtr prtservHandle { get; set; }   //打印服务窗口Handle
        public static int prtConnId { get; set; }          //打印会话连接Id 

		//public static bool FIN_READY { get; set; }		   // 博思开票状态

		//public static n_prtserv prtserv { get; set; }      //打印服务对象
         
        public static bool IDC_Reader_State { get; set; }    //身份证读卡器状态
        public static int IDC_Reader_Rate { get; set; }      //身份证读卡器速率
        public static int IDC_Reader_Port { get; set; }      //身份证读卡器端口
	}
}
