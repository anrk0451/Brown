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
using Brown.DataSet;
using Oracle.ManagedDataAccess.Client;
using Brown.Action;
using Brown.Misc;
using Brown.Domain;
using Brown.Dao;
using System.IO;
using System.Threading;

namespace Brown.Forms
{
    public partial class Frm_Register : BaseDialog
    {
        Register_ds register_ds = null;
        private string source = string.Empty;
        private string ac001 = string.Empty;

        private string regionId = string.Empty;
        private string bitDesc = string.Empty;
        private string bitId = string.Empty;

        private decimal bitPrice = decimal.Zero;   //号位定价
        private decimal fpfee = decimal.Zero;      //附品费用 	
        private decimal regfee = decimal.Zero;     //寄存费用

		private Ic01 ic01 = null;
		private Ic01_dao ic01_dao = new Ic01_dao();
		private bool IDC_FLAG = false;

		public Frm_Register()
        {
            InitializeComponent();
        }

        private void Frm_Register_Load(object sender, EventArgs e)
        {
			register_ds = this.swapdata["dataset"] as Register_ds;
			source = this.swapdata["source"].ToString();

			if (source == "0")  //本馆火化寄存
			{
				ac001 = this.swapdata["AC001"].ToString();
				OracleDataReader reader = SqlAssist.ExecuteReader("select * from ac01 where ac001='" + ac001 + "'");
				while (reader.Read())
				{
					txtEdit_rc001.EditValue = reader["AC001"];
					txtEdit_rc003.EditValue = reader["AC003"];
					rg_rc002.EditValue = reader["AC002"];
					txtEdit_rc004.EditValue = reader["AC004"];
					txtedit_rc014.EditValue = reader["AC014"];
					txtEdit_rc050.EditValue = reader["AC050"];
					txtEdit_rc051.EditValue = reader["AC051"];
					lookUp_rc052.EditValue = reader["AC052"];
					txtEdit_ac055.EditValue = reader["AC055"];

					//如果从火化转来并且有照片
					if (MiscAction.HasIDC(ac001))
					{
						OracleDataReader photo_reader = SqlAssist.ExecuteReader("select ic020 from ic01 where ic000 = '0' and ac001 ='" + ac001 + "'");
						if (photo_reader.HasRows && photo_reader.Read())
						{
							MemoryStream ms = new MemoryStream((byte[])photo_reader["IC020"]);//把照片读到MemoryStream里  
							Image imageBlob = Image.FromStream(ms, true);//用流创建Image  
							pictureEdit1.Image = imageBlob;//输出图片   
						}
						photo_reader.Dispose();
						txtedit_rc014.Properties.Buttons[0].Enabled = false;
					}

				}
			}

			lookup_sa004.DataSource = register_ds.Jcfp;
			lookup_sa004.DisplayMember = "ITEM_TEXT";
			lookup_sa004.ValueMember = "ITEM_ID";

			lookUp_rc052.Properties.DataSource = register_ds.Relation;
			lookUp_rc052.Properties.ValueMember = "ST003";
			lookUp_rc052.Properties.DisplayMember = "ST003";

			register_ds.Sa01.Rows.Clear();
			gridControl1.DataSource = register_ds.Sa01;

			//寄存所属套餐
			DataRow dr_new = null;
			foreach (DataRow r in register_ds.RegCombo.Rows)
			{
				dr_new = register_ds.Sa01.NewRow();
				dr_new["SA004"] = r["CB021"];   //商品或服务编号
				dr_new["PRICE"] = MiscAction.GetItemFixPrice(r["CB021"].ToString());
				dr_new["NUMS"] = r["CB030"];
				dr_new["SA007"] = Convert.ToDecimal(dr_new["PRICE"]) * Convert.ToInt32(dr_new["NUMS"]);

				fpfee += Convert.ToDecimal(dr_new["SA007"]);
				register_ds.Sa01.Rows.Add(dr_new);
			}
			this.CalcHJ();
		}

		/// <summary>
		/// 计算合计金额
		/// </summary>
		private void CalcHJ()
		{
			decimal nums = decimal.Zero;

			if (decimal.TryParse(comboBox1.Text, out nums))
			{
				if (bitPrice > 0)
					regfee = nums * bitPrice;
				else
					regfee = 0;
			}
			else
			{
				regfee = 0;
			}

			txtedit_regfee.EditValue = regfee;


			lc_hj.Text = string.Format("{0:C2}", regfee + fpfee);
		}

		/// <summary>
		/// 选择寄存位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void be_position_Click(object sender, EventArgs e)
		{
			Frm_freeBit frm_free = new Frm_freeBit();
			frm_free.swapdata["parent"] = this;

			if (frm_free.ShowDialog() == DialogResult.OK)
			{
				regionId = this.swapdata["regionId"].ToString();
				bitDesc = this.swapdata["bitDesc"].ToString();
				bitId = RegisterAction.GetBitId(regionId, bitDesc);

				be_position.Text = RegisterAction.GetBitFullName(regionId, bitDesc);
				bitPrice = RegisterAction.GetBitPrice(regionId, bitDesc);
				txtedit_price.EditValue = bitPrice;

				this.CalcHJ();
			}
		}

		private void comboBox1_TextUpdate(object sender, EventArgs e)
		{
			this.CalcHJ();
		}

		/// <summary>
		/// 寄存年限校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_Validating(object sender, CancelEventArgs e)
		{
			decimal nums;
			if (!decimal.TryParse(comboBox1.Text, out nums))
			{
				e.Cancel = true;
				MessageBox.Show("请输入正确的缴费年限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (nums - Math.Truncate(nums) > 0 && nums - Math.Truncate(nums) != new decimal(0.5))
			{
				e.Cancel = true;
				MessageBox.Show("缴费年限只能是整年或半年!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CalcHJ();
		}

		/// <summary>
		/// 性别变更设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rg_rc002_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rg_rc002.EditValue.ToString() == "0")
			{
				rg_rc202.EditValue = "1";
			}
			else if (rg_rc002.EditValue.ToString() == "1")
			{
				rg_rc202.EditValue = "0";
			}
		}

		/// <summary>
		/// 身份证号校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtedit_rc014_Validating(object sender, CancelEventArgs e)
		{
			string s_idcard = txtedit_rc014.Text.Trim();
			if (string.IsNullOrWhiteSpace(s_idcard)) return;

			if (s_idcard.Length != 15 && s_idcard.Length != 18)
			{
				txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_rc014.ErrorText = "身份证号位数错误!";
				e.Cancel = true;
			}
			else if (s_idcard.Length == 15)
			{
				if (!Tools.CheckIDCard15(s_idcard))
				{
					txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_rc014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
			else if (s_idcard.Length == 18)
			{
				if (!Tools.CheckIDCard18(s_idcard))
				{
					txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_rc014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 附品删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
		{
			fpfee = 0;
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if (gridView1.GetRowCellValue(i, "SA007") != null && gridView1.GetRowCellValue(i, "SA007") != System.DBNull.Value)
				{
					fpfee += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
				}
			}
			this.CalcHJ();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			txtedit_price.Text = "";
			txtEdit_rc001.Text = "";
			txtEdit_rc109.Text = "";
			be_position.Text = "";
			comboBox1.Text = "1";
			txtedit_regfee.Text = "";
			txtEdit_rc003.Text = "";
			txtEdit_rc303.Text = "";
			txtEdit_rc004.Text = "";
			txtEdit_rc404.Text = "";
			rg_rc002.EditValue = "0";
			rg_rc202.EditValue = "1";
			txtedit_rc014.Text = "";
			txtEdit_rc050.Text = "";
			lookUp_rc052.EditValue = "";
			txtEdit_rc051.Text = "";
			txtEdit_ac055.Text = "";
			mem_rc099.Text = "";
			lc_hj.Text = "";
			register_ds.Sa01.Rows.Clear();
		}


		/// <summary>
		/// 附品单价数量变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (e.Column.FieldName == "SA004" && e.Value != null && e.Value != System.DBNull.Value)
			{
				gridView1.SetRowCellValue(rowHandle, "PRICE", MiscAction.GetItemFixPrice(gridView1.GetRowCellValue(rowHandle, "SA004").ToString()));
				gridView1.SetRowCellValue(rowHandle, "NUMS", 1);
				calcFee(rowHandle);
			}
			else if (e.Column.FieldName == "PRICE" || e.Column.FieldName == "NUMS")
			{
				calcFee(rowHandle);
			}
			else if (e.Column.FieldName == "SA007")
			{
				fpfee = 0;
				for (int i = 0; i < gridView1.RowCount; i++)
				{
					if (i == rowHandle)
					{
						fpfee += Convert.ToDecimal(e.Value);
					}
					else
					{
						if (gridView1.GetRowCellValue(i, "SA007") != null && gridView1.GetRowCellValue(i, "SA007") != System.DBNull.Value)
							fpfee += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
					}

				}
				///// 如果是新行
				if (rowHandle < 0)
				{
					fpfee += Convert.ToDecimal(e.Value);
				}

				this.CalcHJ();
			}
		}


		/// <summary>
		/// 计算附品金额
		/// </summary>
		/// <param name="rowHandle"></param>
		private void calcFee(int rowHandle)
		{
			decimal price;
			if (!(gridView1.GetRowCellValue(rowHandle, "PRICE") is System.DBNull))
				price = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "PRICE"));
			else
				price = 0;

			int nums;
			if (!(gridView1.GetRowCellValue(rowHandle, "NUMS") is System.DBNull))
				nums = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "NUMS"));
			else
				nums = 0;

			gridView1.SetRowCellValue(rowHandle, "SA007", price * nums);
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 保存前检查
		/// </summary>
		/// <returns></returns>
		private bool SaveCheck()
		{
			//逝者姓名
			if (string.IsNullOrWhiteSpace(txtEdit_rc003.Text.Trim()))
			{
				txtEdit_rc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc003.ErrorText = "逝者姓名必须输入!";
				txtEdit_rc003.Focus();
				return false;
			}
			//年龄
			if (string.IsNullOrWhiteSpace(txtEdit_rc004.Text.Trim()))
			{
				txtEdit_rc004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc004.ErrorText = "年龄必须输入!";
				txtEdit_rc004.Focus();
				return false;
			}

			if (!string.IsNullOrWhiteSpace(txtEdit_rc303.Text.Trim()) && string.IsNullOrWhiteSpace(txtEdit_rc404.Text.Trim()))
			{
				txtEdit_rc404.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc404.ErrorText = "年龄必须输入!";
				txtEdit_rc404.Focus();
				return false;
			}


			//联系人
			if (string.IsNullOrWhiteSpace(txtEdit_rc050.Text))
			{
				txtEdit_rc050.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc050.ErrorText = "联系人必须输入!";
				txtEdit_rc050.Focus();
				return false;
			}
			//与逝者关系
			if (lookUp_rc052.EditValue == null)
			{
				lookUp_rc052.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookUp_rc052.ErrorText = "与逝者关系必须输入!";
				lookUp_rc052.Focus();
				return false;
			}
			//联系电话
			if (string.IsNullOrWhiteSpace(txtEdit_rc051.Text))
			{
				txtEdit_rc051.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc051.ErrorText = "联系人必须输入!";
				txtEdit_rc051.Focus();
				return false;
			}

			//寄存位置
			if (string.IsNullOrEmpty(be_position.Text) || string.IsNullOrEmpty(bitId))
			{
				be_position.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				be_position.ErrorText = "请选择寄存位置!";
				return false;
			}
			//if (bitPrice <= 0)
			//{
			//	txtedit_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
			//	txtedit_price.ErrorText = "此号位未定价!";
			//	return false;
			//}
			if (string.IsNullOrEmpty(comboBox1.Text))
			{
				XtraMessageBox.Show("请输入缴费年限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				comboBox1.Focus();
				return false;
			}

			return true;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			if (!gridView1.PostEditor()) return;
			if (!gridView1.UpdateCurrentRow()) return;
			if (!SaveCheck()) return;  //数据合法性校验!!!

			if (bitPrice <= 0)
			{
				if(XtraMessageBox.Show("此号位未定价或价格为0，是否继续?","提示",MessageBoxButtons.YesNo) == DialogResult.No)
					return;
			}

			if (source == "1")
				ac001 = Tools.GetEntityPK("RC01");         //逝者编号

			string s_fa001 = Tools.GetEntityPK("FA01");         //结算流水号
			string s_rc109 = RegisterAction.GenRegisterNo("0"); //正常登记寄存证号
			string s_rc002 = rg_rc002.EditValue.ToString();     //性别
			string s_rc202 = rg_rc202.EditValue.ToString();     //性别2
			string s_rc003 = txtEdit_rc003.Text;                //逝者姓名
			string s_rc303 = txtEdit_rc303.Text;                //逝者姓名2
			int rc004 = int.Parse(txtEdit_rc004.Text);          //年龄
			int rc404;
			if (!string.IsNullOrEmpty(txtEdit_rc404.Text))
				rc404 = int.Parse(txtEdit_rc404.Text);
			else
				rc404 = 0;

			string s_rc014 = txtedit_rc014.Text;                  //身份证号
			string s_rc050 = txtEdit_rc050.Text;                  //联系人
			string s_rc051 = txtEdit_rc051.Text;                  //联系电话
			string s_rc052 = lookUp_rc052.EditValue.ToString();   //与逝者关系
			string s_rc055 = txtEdit_ac055.Text;                  //联系地址
			string s_rc099 = mem_rc099.Text;                      //备注
			DateTime d_rc140 = DateTime.Now;                      //寄存日期
			decimal nums = decimal.Parse(comboBox1.Text);         //缴费年限
			decimal dec_tax_sum = new decimal(0);

			//输入交款人信息
			string s_cuname = s_rc003;

			List<string> itemId_List = new List<string>();
			List<decimal> itemPrice_List = new List<decimal>();
			List<int> itemNums_List = new List<int>();

			if (fpfee > 0)
			{
				foreach (DataRow r in register_ds.Sa01.Rows)
				{
					itemId_List.Add(r["SA004"].ToString());
					itemPrice_List.Add(Convert.ToDecimal(r["PRICE"]));
					itemNums_List.Add(Convert.ToInt32(r["NUMS"]));
					//计算税票项目金额
					if (MiscAction.GetItemInvoiceType(r["SA004"].ToString()) == "T")
					{
						dec_tax_sum += Convert.ToDecimal(r["SA007"]);
					}
				}
			}
			string s_tip = string.Empty;
			if (dec_tax_sum > 0)
				s_tip = "本次结算共需要一张财政发票和一张税务发票,是否继续?";
			else
				s_tip = "本次结算共需要一张财政发票,是否继续?";

			if(XtraMessageBox.Show(s_tip, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

			int re = 0;
			if (fpfee > 0)
			{				 
				re = RegisterAction.RegisterEnroll(ac001,
												   s_rc109,
												   s_fa001,
												   s_rc002,
												   s_rc202,
												   s_rc003,
												   s_rc303,
												   rc004,
												   rc404,
												   s_rc014,
												   s_rc050,
												   s_rc051,
												   s_rc052,
												   s_rc055,
												   s_rc099,
												   bitId,
												   bitPrice,
												   d_rc140,
												   d_rc140,
												   nums,
												   source,
												   itemId_List.ToArray(),
												   itemPrice_List.ToArray(),
												   itemNums_List.ToArray(),
												   Envior.cur_userId
				);
			}
			else
			{
				re = RegisterAction.RegisterEnroll(ac001,
												   s_rc109,
												   s_fa001,
												   s_rc002,
												   s_rc202,
												   s_rc003,
												   s_rc303,
												   rc004,
												   rc404,
												   s_rc014,
												   s_rc050,
												   s_rc051,
												   s_rc052,
												   s_rc055,
												   s_rc099,
												   bitId,
												   bitPrice,
												   d_rc140,
												   d_rc140,
												   nums,
												   source,
												   Envior.cur_userId
				);
			}

			if (re > 0)
			{
				if (IDC_FLAG)
				{
					ic01.ac001 = ac001;
					ic01_dao.Insert(ic01);

					///更新身份证照片					 
					FileStream file = new FileStream("zp.bmp", FileMode.Open, FileAccess.Read);
					Byte[] imgByte = new Byte[file.Length];//把图片转成 Byte型 二进制流
					file.Read(imgByte, 0, imgByte.Length);//把二进制流读入缓冲区
					file.Close();
					MiscAction.Update_IDC_Photo(ic01.ic001, imgByte);
				}

				if (XtraMessageBox.Show("现在打印【骨灰寄存证】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					PrtServAction.PrtRegisterCert(ac001, s_fa001,Envior.mform.Handle.ToInt32());
				}
				if (XtraMessageBox.Show("现在打印【寄存标签】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					PrtServAction.PrtRegisterLabel(ac001,this.Handle.ToInt32());
				}

				txtEdit_rc001.EditValue = ac001;
				txtEdit_rc109.EditValue = s_rc109;
				XtraMessageBox.Show("寄存登记成功!现在开始开具发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
				if (FinInvoice.InvoiceElec(s_fa001) > 0)
				{
					if (XtraMessageBox.Show("现在打印财政电子票吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Frm_FinInvoice frm_1 = new Frm_FinInvoice();
						frm_1.swapdata["FA001"] = s_fa001;
						frm_1.ShowDialog();
						frm_1.Dispose();
					}
				}



				//// 开税票
				if (dec_tax_sum > 0)
				{
					//获取税务客户信息
					Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_rc003,s_fa001);
					if (frm_taxClient.ShowDialog() == DialogResult.OK)
					{
						TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;
						//if (TaxInvoice.GetNextInvoiceNo() > 0)
						//{
						//	if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						//	{
						//		TaxInvoice.Invoice(s_fa001, clientInfo);
						//	}
						//}
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

				
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}
		/// <summary>
		/// 读取身份证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtedit_rc014_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
						txtEdit_rc050.Focus();
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

				ic01.ic000 = "0";  //0-逝者 1-家属
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

				txtEdit_rc003.EditValue = ic01.ic003;
				rg_rc002.EditValue = ic01.ic002;
 
				txtEdit_rc004.EditValue = MiscAction.Calc_Age_Via_Birth(ic01.ic004.ToString("yyyy-MM-dd"));
				txtedit_rc014.EditValue = ic01.ic014;

			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.ToString(), "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}