using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Brown.BaseObject;
using Oracle.ManagedDataAccess.Client;
using Brown.Action;

namespace Brown.BusinessObject
{
    public partial class RightGrant : BaseBusiness
    {
        private DataTable dt_ro01 = new DataTable("RO01");
        private OracleDataAdapter ro01Adapter = new OracleDataAdapter("select ro001,ro003 from ro01 where status = '1'", SqlAssist.conn);

        private DataTable dt_grants = new DataTable("GRANTS");

        private static string sql = @"select a.ri001 ri001,mo003,ri003,ri005,gr009 from v_grants a,
                                    (select ri001,ro001,gr009 from gr01 where ro001 = :ro001) b where a.ri001 = b.ri001(+) order by ri001 ";
        private OracleDataAdapter grantsAdapter =
            new OracleDataAdapter(sql, SqlAssist.conn);

        private DataTable dt_levelSource = new DataTable();
        private OracleDataAdapter levelAdapter =
            new OracleDataAdapter("select to_number(st001) st001,st003 from st01 where st002 = 'RIGHTLEVEL'", SqlAssist.conn);
 
        private OracleParameter op_ro001 = new OracleParameter("ro001", OracleDbType.Varchar2, 10);

        private bool isUpdate = false;
        public RightGrant()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }

        private void RightGrant_Load(object sender, EventArgs e)
        {
            grantsAdapter.SelectCommand.Parameters.Add(op_ro001);
            op_ro001.Direction = ParameterDirection.Input;

            gridControl1.DataSource = dt_grants;
            lookup_roles.Properties.DataSource = dt_ro01;
            lookup_roles.Properties.DisplayMember = "RO003";
            lookup_roles.Properties.ValueMember = "RO001";

            op_ro001.Value = "";
            grantsAdapter.Fill(dt_grants);
            ro01Adapter.Fill(dt_ro01);

            levelAdapter.Fill(dt_levelSource);
            repositoryItemLookUpEdit2.DataSource = dt_levelSource;
        }


        /// <summary>
        /// 角色改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookup_roles_EditValueChanged(object sender, EventArgs e)
        {
            string s_ro001 = lookup_roles.EditValue.ToString();
            if (string.IsNullOrEmpty(s_ro001)) return;

            op_ro001.Value = s_ro001;
            dt_grants.Rows.Clear();
            grantsAdapter.Fill(dt_grants);
        }
        /// <summary>
        /// 设置允许
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("请选择需要授权的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (int i in gridView1.GetSelectedRows())
            {
                gridView1.SetRowCellValue(i, "GR009", "1");
            }

            gridView1.ClearSelection();

            isUpdate = true;
        }
        /// <summary>
        /// 设置禁止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("请选择需要授权的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (int i in gridView1.GetSelectedRows())
            {
                gridView1.SetRowCellValue(i, "GR009", "0");
            }
            gridView1.ClearSelection();

            isUpdate = true;
        }

        /// <summary>
        /// 允许操控他人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请选择需要授权的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (int i in gridView1.GetSelectedRows())
            {
                if (gridView1.GetRowCellValue(i, "RI005").ToString() == "1")
                {
                    gridView1.SetRowCellValue(i, "GR009", "2");
                }
            }
            gridView1.ClearSelection();
            isUpdate = true;
        }

        /// <summary>
        /// 保存过程
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            string s_ro001 = lookup_roles.EditValue.ToString();
            if (string.IsNullOrEmpty(s_ro001)) return 0;

            List<string> ri001_list = new List<string>();
            List<string> right_list = new List<string>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                ri001_list.Add(gridView1.GetRowCellValue(i, "RI001").ToString());
                if (gridView1.GetRowCellValue(i, "GR009") == null || gridView1.GetRowCellValue(i, "GR009") is System.DBNull)
                {
                    right_list.Add("0");
                }
                else
                {
                    right_list.Add(gridView1.GetRowCellValue(i, "GR009").ToString());
                }
            }

            if (MiscAction.GrantRights(s_ro001, ri001_list.ToArray(), right_list.ToArray()) > 0)
            {
                XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isUpdate = false;
                return 1;
            }
            else
                return -1;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }
    }
}
