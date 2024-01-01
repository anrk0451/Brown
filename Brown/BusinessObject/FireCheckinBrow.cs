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
using Brown.DataSet;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Brown.Forms;
using Brown.Misc;
using Brown.Action;
using DevExpress.XtraPrinting;

namespace Brown.BusinessObject
{
	public partial class FireCheckinBrow : BaseBusiness
	{
		Checkin_ds checkin_ds = new Checkin_ds();
		private DataTable shadow_dt = new DataTable();
		private OracleDataAdapter adapter = new OracleDataAdapter("", SqlAssist.conn);      //查询结构数据适配器


		/// <summary>
		/// 构造函数
		/// </summary>
		public FireCheckinBrow()
		{
			InitializeComponent();

			///////// 装入下拉结果集 ///////////
			//1.经办人
			checkin_ds.uc01Adapter.Fill(checkin_ds.Uc01);
			lookup_ac100.DataSource = checkin_ds.Uc01;
			lookup_ac100.DisplayMember = "UC003";
			lookup_ac100.ValueMember = "UC001";
		}

		/// <summary>
		/// 对象装入事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FireCheckinBrow_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = checkin_ds.Ac01;

			//装入数据
			DataFilter(rangeList.EditValue.ToString());
		}

		/// <summary>
		/// 装入登记数据
		/// </summary>
		/// <param name="action"></param>
		private void DataFilter(string action)
		{
			switch (action)
			{
				case "今日登记":
					checkin_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where trunc(ac200) = trunc(sysdate) and status <> '0' ";
					break;
				case "近三日登记":
					checkin_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where (trunc(sysdate) - trunc(ac200)) <=2 and status <> '0' ";
					break;
				case "一个月内登记":
					checkin_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where (trunc(sysdate) - trunc(ac200)) <=30 and status <> '0' ";
					break;
			}

			gridView1.BeginUpdate();
			checkin_ds.Fill_ac01();
			gridView1.EndUpdate();
		}

		/// <summary>
		/// 数据表格 代码文字转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "AC002")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "男";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "女";
				else
					e.DisplayText = "未知";
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
		/// 双击编辑记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			if (e.Button == MouseButtons.Left && e.Clicks == 2)
			{
				//判断光标是否在行范围内  
				if (hInfo.InRow)
				{
					Modify(gridView1.FocusedRowHandle);
				}
			}
		}

		/// <summary>
		/// 编辑记录
		/// </summary>
		/// <param name="rowHandle"></param>
		private void Modify(int rowHandle)
		{
			if (rowHandle < 0) return;

			string handler = gridView1.GetRowCellValue(rowHandle, "AC100").ToString();
			if (!AppAction.CheckRight("登记信息修改", handler)) return;

			Frm_fireCheckin frm_ac01 = new Frm_fireCheckin();
			frm_ac01.swapdata["action"] = "edit";
			frm_ac01.swapdata["dataset"] = this.checkin_ds;
			frm_ac01.swapdata["businessObject"] = this;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			frm_ac01.swapdata["AC001"] = s_ac001;

			if (frm_ac01.ShowDialog() == DialogResult.OK)
			{
				
				adapter.SelectCommand.CommandText = "select * from ac01 where ac001='" + s_ac001 + "'";
				adapter.Fill(shadow_dt);
				checkin_ds.Ac01.Merge(this.shadow_dt);
			}
			frm_ac01.Dispose();
			//TODO 3.同步ac01与 rc01
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("进灵登记")) return;
			Frm_fireCheckin frm_ac01 = new Frm_fireCheckin();
			frm_ac01.swapdata["action"] = "add";
			frm_ac01.swapdata["dataset"] = this.checkin_ds;
			frm_ac01.swapdata["businessObject"] = this;

			if (frm_ac01.ShowDialog() == DialogResult.OK)
			{
				string s_ac001 = this.swapdata["AC001"].ToString();

				adapter.SelectCommand.CommandText = "select * from ac01 where ac001='" + s_ac001 + "'";
				adapter.Fill(this.shadow_dt);
				checkin_ds.Ac01.Merge(this.shadow_dt);
			}
		}

		/// <summary>
		/// 修改记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{			 
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				Modify(rowHandle);
			}
		}

		/// <summary>
		/// 删除登记记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle; ;
			if (rowHandle < 0) return;

			if (!AppAction.CheckRight("登记信息删除", gridView1.GetRowCellValue(rowHandle,"AC100").ToString())) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			if (MessageBox.Show("确认要删除登记信息吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

			if (FireAction.RemoveFireCheckin(s_ac001, Envior.cur_userId) > 0)
			{
				XtraMessageBox.Show("删除成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.RefreshData();
			}
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			DataFilter(rangeList.EditValue.ToString());
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		/// <summary>
		/// 业务办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!AppAction.CheckRight("业务办理")) return;
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			(Envior.mform as MainForm).openBusinessObject("FireBusiness", s_ac001);
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		/// <summary>
		/// 选择条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rangeList_EditValueChanged(object sender, EventArgs e)
		{
			if (rangeList.EditValue != null && !string.IsNullOrWhiteSpace(rangeList.EditValue.ToString()))
			{
				DataFilter(rangeList.EditValue.ToString());
			}
		}
	}
}
