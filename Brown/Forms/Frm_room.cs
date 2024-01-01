using Brown.BaseObject;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brown.Forms
{
	public partial class Frm_room : BaseDialog
	{
		string s_rg003 = string.Empty;
		string s_rg099 = string.Empty;
		public Frm_room()
		{
			InitializeComponent();
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Frm_room_Load(object sender, EventArgs e)
		{
			te_rg003.Text = this.swapdata["rg003"].ToString();
			chk_rg099.Checked = this.swapdata["rg099"].ToString().Equals("1");
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			string s_rg003 = te_rg003.Text;
			if (String.IsNullOrEmpty(s_rg003))
			{
				te_rg003.Focus();
				te_rg003.ErrorText = "请输入寄存室名字!";
				return;
			}
			string s_rg099 = chk_rg099.Checked ? "1" : "0";


		}
	}
}