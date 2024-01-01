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
using Brown.Misc;
using Brown.Domain;
using Oracle.ManagedDataAccess.Client;
using Brown.Action;

namespace Brown.Forms
{
    public partial class Frm_TaxClientInfo : BaseDialog
    {
        private string fa001 = string.Empty;
        TaxClientInfo taxClientInfo = new TaxClientInfo();
        
        public Frm_TaxClientInfo()
        {
            InitializeComponent();
        }

        public Frm_TaxClientInfo(string cuname)
        {
            InitializeComponent();
            txtedit_clientName.Text = cuname;  
        }

        public Frm_TaxClientInfo(string cuname,string fa001)
        {
            InitializeComponent();
            txtedit_clientName.Text = cuname;
            this.fa001 = fa001;
        }


        public Frm_TaxClientInfo(Tu01 tu01,string fa001)
        {
            InitializeComponent();
            txtedit_clientName.Text = tu01.tu003;
            txtedit_InfoClientTaxCode.Text = tu01.tu005;
            txtedit_infoclientaddressphone.Text = tu01.tu006;
            txtedit_infoclientbankaccount.Text = tu01.tu007;
            this.fa001 = fa001;
        }

        private void Frm_TaxClientInfo_Load(object sender, EventArgs e)
        {
            txtedit_infocashier.Text = Envior.TAX_CASHIER;  //收款人
            txtedit_infochecker.Text = Envior.TAX_CHECKER;  //复核人
            txtedit_clientName.Focus();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            if (txtedit_clientName.EditValue == null)
            {
                txtedit_clientName.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_clientName.ErrorText = "【购方名称】必须输入!";
                return;
            }
            taxClientInfo.InfoClientName = txtedit_clientName.Text;                    //客户名称
            taxClientInfo.InfoClientTaxCode = txtedit_InfoClientTaxCode.Text;          //税号
            taxClientInfo.infoclientbankaccount = txtedit_infoclientbankaccount.Text;  //客户银行账户
            taxClientInfo.infoclientaddressphone = txtedit_infoclientaddressphone.Text;//客户地址及电话
            taxClientInfo.infocashier = txtedit_infocashier.Text;                      //收款人
            taxClientInfo.infochecker = txtedit_infochecker.Text;                      //复核人

            this.swapdata["taxclientinfo"] = taxClientInfo;

            if(TaxInvoice.SaveTaxBill(fa001, taxClientInfo.InfoClientName, taxClientInfo.InfoClientTaxCode, taxClientInfo.infoclientbankaccount, taxClientInfo.infoclientaddressphone,txt_phone.Text, taxClientInfo.infocashier, taxClientInfo.infochecker,Envior.TAX_EXTENSION, Envior.TAX_MACHINECODE, Envior.cur_userId,Envior.WORKSTATIONID) > 0)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }             
        }
    }
}