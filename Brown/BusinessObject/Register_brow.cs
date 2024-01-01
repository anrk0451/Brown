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
using Brown.Forms;
using Brown.DataSet;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Brown.Action;
using DevExpress.XtraPrinting;
using Brown.Misc;

namespace Brown.BusinessObject
{
    public partial class Register_brow : BaseBusiness
    {
		Register_ds register_ds = new Register_ds();

		public Register_brow()
        {
            InitializeComponent();
        }


        private void Register_brow_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = register_ds.Rc01;
            ///装入数据
            Load_Data(combo_days.EditValue.ToString());
        }

		/// <summary>
		/// 装入数据
		/// </summary>
		/// <param name="filter"></param>
		private void Load_Data(string filter)
		{
			switch (filter)
			{
				case "今日登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where trunc(rc200) = trunc(sysdate)  ";
					break;
				case "近三日登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where (trunc(sysdate) - trunc(rc200)) <=2  ";
					break;
				case "一个月内登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where (trunc(sysdate) - trunc(rc200)) <=30  ";
					break;
				case "条件查询":

					Frm_registerSearch frm_search = new Frm_registerSearch();
					frm_search.swapdata["parent"] = this;

					if (frm_search.ShowDialog() == DialogResult.OK)
					{
						register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where 1=1 " + this.swapdata["sql"].ToString();
					}
					break;
			}
			///this.RefreshData();

			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			register_ds.Rc01.Rows.Clear();
			register_ds.rc01Adapter.Fill(register_ds.Rc01);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void RefreshData()
		{
			Load_Data(combo_days.EditValue.ToString());
		}

		/// <summary>
		/// 外来寄存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存登记")) return;

			Frm_Register frm_reg = new Frm_Register();
			frm_reg.swapdata["dataset"] = register_ds;
			frm_reg.swapdata["source"] = "1";
			if (frm_reg.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
			frm_reg.Dispose();
		}

		/// <summary>
		/// 本馆火化寄存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("寄存登记")) return;

			Frm_fromFire frm_fromFire = new Frm_fromFire();
			//frm_fromFire.swapdata["BusinessObject"] = this;
			if (frm_fromFire.ShowDialog() == DialogResult.OK)
			{
				string s_ac001 = frm_fromFire.swapdata["AC001"].ToString();
				frm_fromFire.Dispose();

				Frm_Register regform = new Frm_Register();
				regform.swapdata["dataset"] = register_ds;
				regform.swapdata["source"] = "0";
				regform.swapdata["AC001"] = s_ac001;
				if (regform.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				regform.Dispose();
			}
			else
				frm_fromFire.Dispose();
		}

		/// <summary>
		/// 原始寄存登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("原始寄存登记")) return;

			Frm_RegOrig frm_ori = new Frm_RegOrig();
			frm_ori.swapdata["dataset"] = register_ds;
			if (frm_ori.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
			frm_ori.Dispose();
		}


		private void combo_days_EditValueChanged(object sender, EventArgs e)
		{
			if (combo_days.EditValue != null && !string.IsNullOrWhiteSpace(combo_days.EditValue.ToString()))
			{
				Load_Data(combo_days.EditValue.ToString());
			}
		}

		private void gridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			if (e.Button == MouseButtons.Left && e.Clicks == 2)
			{
				//判断光标是否在行范围内  
				if (hInfo.InRow)
				{
					EditData(gridView1.FocusedRowHandle);
				}
			}
		}

		/// <summary>
		/// 修改数据
		/// </summary>
		/// <param name="rowHandle"></param>
		private void EditData(int rowHandle)
		{
			//权限检查
			if (!AppAction.CheckRight("寄存信息修改")) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();

			Frm_RegEdit frm_edit = new Frm_RegEdit();
			frm_edit.swapdata["dataset"] = register_ds;
			frm_edit.swapdata["AC001"] = s_ac001;
			if (frm_edit.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
		}

		/// <summary>
		/// 编辑寄存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				this.EditData(rowHandle);
			}
		}

		/// <summary>
		/// 位置变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("寄存位置变更")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				Frm_RegisterMove frm_move = new Frm_RegisterMove();
				frm_move.swapdata["RC001"] = gridView1.GetRowCellValue(rowHandle, "RC001");
				if (frm_move.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				frm_move.Dispose();
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 缴费
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("寄存缴费办理")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				Frm_RegisterPay frm_pay = new Frm_RegisterPay();
				frm_pay.swapdata["RC001"] = gridView1.GetRowCellValue(rowHandle, "RC001");
				if (frm_pay.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				frm_pay.Dispose();
			}
		}

		/// <summary>
		/// 补打寄存证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("补打寄存证")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				XtraMessageBox.Show("现在打印【寄存证】!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				PrtServAction.PrtRegisterCertBD(s_rc001,Envior.mform.Handle.ToInt32());
			}
		}

		/// <summary>
		/// 补打寄存标签
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				XtraMessageBox.Show("现在打印【寄存标签】!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				PrtServAction.PrtRegisterLabel(s_rc001,Envior.mform.Handle.ToInt32());
			}
		}

		/// <summary>
		/// 补打缴费记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("补打寄存证")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				Frm_prtPayRecord frm_payrec = new Frm_prtPayRecord();
				frm_payrec.swapdata["RC001"] = s_rc001;

				frm_payrec.ShowDialog();
				frm_payrec.Dispose();
			}
		}

		/// <summary>
		/// 条件查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Load_Data("条件查询");
		}

		/// <summary>
		/// 迁出办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("寄存迁出")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				Frm_registerOut frm_out = new Frm_registerOut();
				frm_out.swapdata["RC001"] = s_rc001;

				if (frm_out.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				frm_out.Dispose();
			}
		}

		/// <summary>
		/// 寄存时间修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("寄存时间修正")) return;

			int rowHandle = gridView1.FocusedRowHandle;
			string s_rc001 = string.Empty;

			if (rowHandle >= 0)
			{
				if (register_ds.Rc01.Rows[gridView1.GetDataSourceRowIndex(rowHandle)]["SOURCE"].ToString() != "2")
				{
					MessageBox.Show("只有原始寄存可以调整寄存日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				if (RegisterAction.GetRegPayRecordNum(s_rc001) > 1)
				{
					MessageBox.Show("原始登记已经缴费,不能继续!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Frm_AdjustRegisterDate frm_1 = new Frm_AdjustRegisterDate();
				frm_1.swapdata["AC001"] = s_rc001;
				frm_1.ShowDialog();
				frm_1.Dispose();
			}
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void te_quicksearch_EditValueChanged(object sender, EventArgs e)
		{
			string s_sql = string.Empty;
			string s_text = te_quicksearch.EditValue.ToString();

			if(s_text.Length == 4 || (s_text.Length == 5 && s_text.Contains("-") ) )   //号位查询
			{
				s_sql = "select * from v_register r where exists(select 1 from bi01 b where r.rc130 = b.bi001 and bi003 ='" + s_text + "')";
			}else if (Tools.IsHZ(s_text))
			{
				s_sql = "select * from v_register r where rc003 like '" + s_text + "%'";
			}else if(s_text.Length > 4)
			{
				s_sql = "select * from v_register r where rc109 like '" + s_text + "%'";
			}

			if (!string.IsNullOrEmpty(s_sql))
			{
				register_ds.rc01Adapter.SelectCommand.CommandText = s_sql;
				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				register_ds.Rc01.Rows.Clear();
				register_ds.rc01Adapter.Fill(register_ds.Rc01);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
		}
		/// <summary>
		/// 选号登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}
	}
}
