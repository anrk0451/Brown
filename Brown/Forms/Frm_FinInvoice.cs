using Brown.Action;
using Brown.BaseObject;
using Brown.Misc;
using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.Forms
{
	public partial class Frm_FinInvoice : BaseDialog
	{
		private string s_fa001 = string.Empty;
		private string s_inv_batch_code = string.Empty;
		private string s_inv_no = string.Empty;
		public Frm_FinInvoice()
		{
			InitializeComponent();
		}

		private void Frm_FinInvoice_Load(object sender, EventArgs e)
		{
			try
			{
				s_fa001 = this.swapdata["FA001"].ToString();
				OracleDataReader reader = SqlAssist.ExecuteReader("select INVOICEZCH,INVOICENO from fin_log where settleId ='" + s_fa001 + "'");
				if (reader.Read() && reader.HasRows)
				{
					s_inv_batch_code = reader["INVOICEZCH"].ToString();
					s_inv_no = reader["INVOICENO"].ToString();
				}
				else
				{
					XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				reader.Dispose();

				//XtraMessageBox.Show(s_inv_batch_code);
				//XtraMessageBox.Show(s_inv_no);

				string imgTxt = FinInvoice.GetInvoiceImageBase64(s_inv_batch_code, s_inv_no);
				//string imgTxt = "iVBORw0KGgoAAAANSUhEUgAAAK8AAACvCAIAAAAE8BkiAAAFyUlEQVR42u3d227bQAxF0fz/T7tPBdoitTg8m5QQbz3mYkszyxjekHy9vLx+X18ugde/Gr6y65vXPflW7wUrv3V0Y+HPUGsYLnjy7mpQgxrUUNFwdsxkq1b5rXBBQ3CVG8Ofq7fgyA6qQQ1qUMORhnDRwxP3aBvCRacikqPbwMUgO6gGNahBDZsaqDOYStKog5ZKNXuBiBrUoAY1/EgNeBnuxlJdO8QJwwU1qEENanimhvDIPHoA/Hxd6FqF0Q+1ho+uTKtBDWr4oRrm2kV+Zecr5HyDGtSgBjX8oYG6qKwPH4/AZzKoOIYqgDIvqwY1qEEN/9cQnkNUkhbKC71u1j2p36L2Sw1qUIMaLjXg3ZTeI4XhAnWs9jZvqJN0Ws9NMlU1qEENagi7Vr1m/1zfiBo+wJNPqmOHj0dcLoIa1KAGNVT6FJsjC1QxsbdqtwQQYYCF";


				byte[] bytes = Convert.FromBase64String(imgTxt);
				MemoryStream ms = new MemoryStream(bytes, true);
				ms.Write(bytes, 0, bytes.Length);
				pictureBox1.Image = new Bitmap(ms);
			}
			catch (Exception ee)
			{
				LogUtils.Error(ee.ToString());
				pictureBox1.Image = Brown.Properties.Resources.nodata;
			}

		}
		/// <summary>
		/// 打印电子票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton1_Click(object sender, EventArgs e)
		{
			//PrintDocument pd = new PrintDocument();
			////设置边距
			//Margins margin = new Margins(10, 10, 20, 20);
			//pd.DefaultPageSettings.Margins = margin;
			//pd.DefaultPageSettings.Landscape = false;

			//////纸张设置默认
			////foreach (PaperSize ps in pd.PrinterSettings.PaperSizes)
			////{
			////	if (ps.PaperName.Equals("A4"))
			////		pd.DefaultPageSettings.PaperSize = ps;
			////}
			//PaperSize pageSize = new PaperSize("First custom size", 260, 160);
			//pd.DefaultPageSettings.PaperSize = pageSize;

			////打印事件设置
			//pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			////ppd.Document = pd;
			////ppd.ShowDialog();
			//try
			//{
			//	pd.Print();
			//	XtraMessageBox.Show("电子票打印完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
			//}

			string s_pictureName = "einvoice.png";
			if (pictureBox1.Image != null && pictureBox1.Image != Brown.Properties.Resources.nodata)
			{
				////********************照片另存*********************************
				using (MemoryStream mem = new MemoryStream())
				{
					//这句很重要，不然不能正确保存图片或出错（关键就这一句）
					Bitmap bmp = new Bitmap(pictureBox1.Image);

					//保存到内存
					//bmp.Save(mem, pictureBox1.Image.RawFormat );
					//保存到磁盘文件
					bmp.Save(s_pictureName, pictureBox1.Image.RawFormat);
					bmp.Dispose();

					PrtServAction.Print_EInvoice(Envior.mform.Handle.ToInt32());
				}
				////********************照片另存*********************************
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
			Rectangle destRect = new Rectangle(x, y, width, height - 80);
			e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void simpleButton3_Click(object sender, EventArgs e)
		{
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string s_pictureName = saveFileDialog1.FileName; 
				if (pictureBox1.Image != null && pictureBox1.Image != Brown.Properties.Resources.nodata)
				{
					////********************照片另存*********************************
					using (MemoryStream mem = new MemoryStream())
					{
						//这句很重要，不然不能正确保存图片或出错（关键就这一句）
						Bitmap bmp = new Bitmap(pictureBox1.Image);

						//保存到内存
						//bmp.Save(mem, pictureBox1.Image.RawFormat );
						//保存到磁盘文件
						bmp.Save(s_pictureName, pictureBox1.Image.RawFormat);
						bmp.Dispose();						 
						XtraMessageBox.Show("图片另存成功！", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					}
					////********************照片另存*********************************
				}
			}
		}
		/// <summary>
		/// 发送电子票通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleButton29_Click(object sender, EventArgs e)
		{
			string s_type = radioGroup1.EditValue.ToString();
			string s_data = textEdit1.Text;
			if (string.IsNullOrEmpty(s_data))
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "请输入电子票接收目标地址!";
				return;
			}
			if (FinInvoice.SendElecInvoiceNotice(s_inv_batch_code, s_inv_no, s_type, s_data) > 0)
				XtraMessageBox.Show("发送电子票通知成功!请按预留方式进行查收!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

		}
	}
}
