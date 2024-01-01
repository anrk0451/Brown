using System;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using System.Text;
using Brown.Misc;
using System.Configuration;
using Brown.Action;

namespace Brown
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
                bool bCreate;
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, "SINGILE_INSTANCE_MUTEX", out bCreate);

                if (!bCreate)
                {
                    MessageBox.Show("程序已经启动!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    return;
                }

				if (args.Length > 0)
					MessageBox.Show(args[0].Substring(8));

				//设置应用程序处理异常方式：ThreadException处理
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				//处理UI线程异常
				Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
				//处理非UI线程异常
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

				#region 应用程序的主入口点
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				BonusSkins.Register();
				SkinManager.EnableFormSkins();
				//	UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");


				///// 连接数据库 //////
				SqlAssist.ConnectDb();

				///// 检查版本  ///////
				string curNewestVersion = Tools.getNewVersion();
				if (string.Compare(curNewestVersion, AppInfo.AppVersion) > 0)
				{
					MessageBox.Show("服务器发现更新的版本!系统需要升级", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Tools.DownloadNew(curNewestVersion);
					SqlAssist.DisConnect();

					//启动升级拷贝 //////

					try
					{
						Process.Start("Upgrade.exe", curNewestVersion);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}

					Application.Exit();
					return;
				}
 
				/// 检查 工作站是否进行登记
				Envior.WORKSTATIONID = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["workstationID"].Value.ToString();
 
				string hostname = string.Empty;
				string ipaddress = string.Empty;

				Tools.GetIpAddress(out hostname, out ipaddress);
				if(!string.IsNullOrEmpty(Envior.WORKSTATIONID))
				{
					switch (AppAction.WorkStationIsRegistered(Envior.WORKSTATIONID, hostname, ipaddress))
					{
						case 0:  //未登记
							MessageBox.Show("此工作站尚未登记！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
							Application.Exit();
							return;
						case 2:  //主机名不符
							MessageBox.Show("此工作站【计算机名称】与登记不符!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
							Application.Exit();
							return;
						case 3:  //ip地址不符
							MessageBox.Show("此工作站【IP地址】与登记不符!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
							Application.Exit();
							return;
						default:						
							break;
					}
				}
				else
				{
					MessageBox.Show("未设置工作站ID!","错误",MessageBoxButtons.OK,MessageBoxIcon.Stop);
					Application.Exit();
					return;
				}

				

				Application.Run(new MainForm());
				#endregion
			}
			catch (Exception ex)
			{
				string str = GetExceptionMsg(ex, string.Empty);
				LogUtils.Error(str);
				MessageBox.Show(ex.ToString(), "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			string str = GetExceptionMsg(e.Exception, e.ToString());
			MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			LogUtils.Error(str);
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
			MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			LogUtils.Error(str);
		}



		/// <summary>
		/// 生成自定义异常消息
		/// </summary>
		/// <param name="ex">异常对象</param>
		/// <param name="backStr">备用异常消息：当ex为null时有效</param>
		/// <returns>异常字符串文本</returns>
		static string GetExceptionMsg(Exception ex, string backStr)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("****************************异常文本****************************");
			sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
			if (ex != null)
			{
				sb.AppendLine("【异常类型】：" + ex.GetType().Name);
				sb.AppendLine("【异常信息】：" + ex.Message);
				sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
			}
			else
			{
				sb.AppendLine("【未处理异常】：" + backStr);
			}
			sb.AppendLine("***************************************************************");
			return sb.ToString();
		}
	}
}
