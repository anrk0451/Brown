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
using Oracle.ManagedDataAccess.Client;
using Brown.Action;
using Brown.Misc;
using Brown.Domain;
using Brown.Dao;
using System.IO;

namespace Brown.Forms
{
    public partial class Frm_registerOut : BaseDialog
    {
        private string rc001 = string.Empty;
        private decimal price = decimal.Zero;  //寄存单价
        private bool isrefund = false;         //是否退费

		private Ic01 ic01 = null;
		private Ic01_dao ic01_dao = new Ic01_dao();

		private bool IDC_FLAG = false;

		public Frm_registerOut()
        {
            InitializeComponent();
        }

		private void Frm_registerOut_Load(object sender, EventArgs e)
		{
			rc001 = this.swapdata["RC001"].ToString();

			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + rc001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.Text = rc001;
				txtEdit_rc109.EditValue = reader["RC109"];
				txtEdit_rc003.EditValue = reader["RC003"];
				txtEdit_rc303.EditValue = reader["RC303"];
				rg_rc002.EditValue = reader["RC002"];
				rg_rc202.EditValue = reader["RC202"];
				txtEdit_rc004.EditValue = reader["RC004"];
				txtEdit_rc404.EditValue = reader["RC404"];
				txtEdit_rc150.EditValue = reader["RC150"];   //寄存到期日期
				be_position.EditValue = RegisterAction.GetRegPathName(rc001);

				price = Math.Round(  RegisterAction.GetBitPrice(reader["RC130"].ToString()) / 12, 0);
				txtEdit_price.EditValue = price;

				int diff = RegisterAction.CalcOutDiffDays(rc001);

				int compare = string.Compare(Convert.ToDateTime(reader["RC150"]).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
				if (compare == 0)
				{
					checkEdit1.Enabled = false;
					txtEdit_nums.Enabled = false;
				}
				else if (compare > 0)  //退费
				{
					lc_1.Text = "剩余天数";
					lc_2.Text = "应退费月数";
					lc_3.Text = "退费金额";
					isrefund = true;

					txtEdit_nums.EditValue = Math.Round((diff * 1.0f) / 30, 0);
					txtEdit_fee.EditValue = Convert.ToDecimal(Math.Round((diff * 1.0f) / 30, 0)) * price;
				}
				else
				{
					lc_1.Text = "过期天数";
					lc_2.Text = "应补费月数";
					lc_3.Text = "补费金额";

					txtEdit_nums.EditValue = Math.Round((diff * 1.0f) / 30, 0);
					txtEdit_fee.EditValue = Convert.ToDecimal(Math.Round((diff * 1.0f) / 30, 0)) * price;
				}


				txtEdit_diff.EditValue = diff;
			}

			//TODO 5. 根据权限设置 是否允许补退费
			//权限检查
			if (!AppAction.CheckRight("迁出时允许选择补退")) 
				checkEdit1.Enabled = false;

		}


		/// <summary>
		/// 是否补退费 开关
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkEdit1_CheckedChanged(object sender, EventArgs e)
		{
			txtEdit_nums.Enabled = checkEdit1.Checked;
			if (!checkEdit1.Checked)
			{
				txtEdit_nums.Text = "0.00";
			}
		}

		/// <summary>
		/// 补退费年限校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEdit_nums_Validating(object sender, CancelEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEdit_nums.Text))
			{
				if (Convert.ToDecimal(txtEdit_nums.Text) < 0)
				{
					txtEdit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtEdit_nums.ErrorText = "应为正值!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 补退费年限变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEdit_nums_EditValueChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEdit_nums.Text))
			{
				decimal nums = Convert.ToDecimal(txtEdit_nums.Text);
				txtEdit_fee.EditValue = Math.Round(price * nums);
			}
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (rc001 == null)
			{
				XtraMessageBox.Show("数据传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (txtEdit_oc003.EditValue == null || txtEdit_oc003.EditValue is System.DBNull)
			{
				txtEdit_oc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_oc003.ErrorText = "请输入迁出办理人!";
				return;
			}
			if (te_oc005.EditValue == null)
			{
				te_oc005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				te_oc005.ErrorText = "请输入迁出原因!";
				return;
			}
			string s_oc003 = txtEdit_oc003.Text;   //迁出人
			string s_oc005 = te_oc005.Text;       //迁出原因
			string s_oc004 = txtEdit_oc004.Text;   //迁出人身份证号
			string s_fa001_2 = string.Empty;       //如果是退费,则要有一笔正数的收费发票
			bool b_elecinv_refund = false;         //是否是电子票退款



			int diff = int.Parse(txtEdit_diff.EditValue.ToString());
			decimal nums = decimal.Zero;
			string fa001 = Tools.GetEntityPK("FA01");
			string last_fa001 = RegisterAction.GetREGLastSettleId(rc001);     //获取最后一次缴费 结算流水号

			//补退情况
			if (checkEdit1.Checked && (!string.IsNullOrEmpty(txtEdit_nums.Text)))
			{
				nums = decimal.Parse(txtEdit_nums.Text);
				if (isrefund && nums > 0 && (!MiscAction.FinRefundBeforeOnline(fa001)))
				{
					b_elecinv_refund = true;
					s_fa001_2 = Tools.GetEntityPK("FA01");
				}					
			}
			else
			{
				nums = 0;
			}

			if (XtraMessageBox.Show("确认要继续办理迁出吗？本业务将不能回退!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
			if(isrefund && nums > 0 && b_elecinv_refund)			 
				XtraMessageBox.Show("本次迁出需要退费操作!系统会生成两笔收款记录,两笔操作差额即是退费金额!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);		 
			else if (isrefund && nums > 0 && !b_elecinv_refund)			 
				XtraMessageBox.Show("原发票在财政新接口上线前开具,不能开具对应退费发票,请在财政发票系统内完成发票开具.\r\n 开具成功后请更新发票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		 
			//if ((!string.IsNullOrEmpty(txtEdit_fee.Text)) && Convert.ToDecimal(txtEdit_fee.Text) > 0 && Envior.cur_userId != AppInfo.ROOTID && !isrefund )
			//{
			//	XtraMessageBox.Show("当前记录已经欠费,不能迁出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//}
			 
			int re = RegisterAction.RegisterOut(rc001,
												 s_oc003,
												 s_oc004,
												 s_oc005,
												 diff,
												 fa001,
												 price,
												 isrefund ? 0 - nums : nums,
												 Envior.cur_userId,
												 s_fa001_2
				);
			if (re > 0)
			{
				//保存迁出人信息 
				if (IDC_FLAG) 
				{
					ic01_dao.Insert(ic01);
					///更新身份证照片
					if (ic01 != null)
					{
						FileStream file = new FileStream("zp.bmp", FileMode.Open, FileAccess.Read);
						Byte[] imgByte = new Byte[file.Length];//把图片转成 Byte型 二进制流
						file.Read(imgByte, 0, imgByte.Length);//把二进制流读入缓冲区
						file.Close();
						MiscAction.Update_IDC_Photo(ic01.ic001, imgByte);
					}
				}
				
				XtraMessageBox.Show("迁出办理成功!现在打印【迁出通知单】", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				PrtServAction.PrtRegisterOutNotice(rc001,this.Handle.ToInt32());

				if (!isrefund && nums  > 0)
				{
					if (XtraMessageBox.Show("现在开具【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{						
						if (FinInvoice.InvoiceElec(fa001) > 0)
						{
							if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								Frm_FinInvoice frm_1 = new Frm_FinInvoice();
								frm_1.swapdata["FA001"] = fa001;
								frm_1.ShowDialog();
								frm_1.Dispose();
							}
						}
					}
				}
				else if(isrefund && Math.Abs(nums) > 0 && b_elecinv_refund)    //退费发票(电子票)
				{
					if (XtraMessageBox.Show("现在开具【发票】吗?共需要两张,一张负数红冲发票,一张正数发票!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						if (FinInvoice.InvoiceElec_Refund(fa001) > 0)
						{
							if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								Frm_FinInvoice frm_1 = new Frm_FinInvoice();
								frm_1.swapdata["FA001"] = fa001;
								frm_1.ShowDialog();
								frm_1.Dispose();
							}
						}
						if (FinInvoice.InvoiceElec(s_fa001_2) > 0)
						{
							if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								Frm_FinInvoice frm_1 = new Frm_FinInvoice();
								frm_1.swapdata["FA001"] = s_fa001_2;
								frm_1.ShowDialog();
								frm_1.Dispose();
							}
						}
					}					 
				}
			}
			DialogResult = DialogResult.OK;
			this.Close();
 
		}

		private void sb_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// 读取身份证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
		{

		}
		/// <summary>
		/// 填充数据
		/// </summary>
		private void FillData()
		{
			try
			{
				int length;

				IDC_FLAG = true;

				// 照片保存在当前目录
				String szXPPath = "zp.bmp";
				System.Drawing.Image img = System.Drawing.Image.FromFile(szXPPath);
				System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
				img.Dispose();
				pictureEdit1.Image = bmp;


				byte[] name = new byte[128];
				length = 128;
				CVRSDK.GetPeopleName(ref name[0], ref length);

				byte[] cnName = new byte[128];
				length = 128;
				CVRSDK.GetPeopleChineseName(ref cnName[0], ref length);

				byte[] number = new byte[128];
				length = 128;
				CVRSDK.GetPeopleIDCode(ref number[0], ref length);

				byte[] peopleNation = new byte[128];
				length = 128;
				CVRSDK.GetPeopleNation(ref peopleNation[0], ref length);

				byte[] peopleNationCode = new byte[128];
				length = 128;
				CVRSDK.GetNationCode(ref peopleNationCode[0], ref length);

				byte[] validtermOfStart = new byte[128];
				length = 128;
				CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);

				byte[] birthday = new byte[128];
				length = 128;
				CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);

				byte[] address = new byte[128];
				length = 128;
				CVRSDK.GetPeopleAddress(ref address[0], ref length);

				byte[] validtermOfEnd = new byte[128];
				length = 128;
				CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);

				byte[] signdate = new byte[128];
				length = 128;
				CVRSDK.GetDepartment(ref signdate[0], ref length);

				byte[] sex = new byte[128];
				length = 128;
				CVRSDK.GetPeopleSex(ref sex[0], ref length);

				byte[] Uid = new byte[128];
				length = 128;

				//CVRSDK.GetIDCardUID(ref Uid[0], 128);
				 
				byte[] certType = new byte[32];
				length = 32;
				CVRSDK.GetCertType(ref certType[0], ref length);

				string strType = System.Text.Encoding.ASCII.GetString(certType);
				int nStart = strType.IndexOf("I");
				 
				if (ic01 == null)
				{
					ic01 = new Ic01();
					ic01.ic001 = Tools.GetEntityPK("IC01");
				}

				ic01.ic000 = "1";  //0-逝者 1-家属
				ic01.ic003 = System.Text.Encoding.GetEncoding("GB2312").GetString(name);    //姓名
				ic01.ic002 = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim() == "男" ? "0" : "1";

				//出生日期
				string s_birth = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
				ic01.ic004 = Convert.ToDateTime(s_birth.Substring(0, 4) + "-" + s_birth.Substring(4, 2) + "-" + s_birth.Substring(6));

				//身份证号
				ic01.ic014 = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();

				//地址
				ic01.ic016 = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();

				//签发机关
				ic01.ic017 = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();

				//有效期限
				ic01.ic018 = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
				 
				txtEdit_oc003.EditValue = ic01.ic003;
				txtEdit_oc004.EditValue = ic01.ic014;

			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.ToString(), "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtEdit_oc004_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				int authenticate = CVRSDK.CVR_Authenticate();
				if (authenticate == 1)
				{
					int readContent = CVRSDK.CVR_Read_Content(4);
					if (readContent == 1)
					{
						FillData();
					}
					else
					{
						XtraMessageBox.Show("读卡失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					XtraMessageBox.Show("未放卡或卡片放置不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}