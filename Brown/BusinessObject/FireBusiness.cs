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
using Brown.Action;
using Brown.Forms;
using Brown.Misc;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using System.IO;

namespace Brown.BusinessObject
{
	public partial class FireBusiness : BaseBusiness
	{
		FireBusiness_ds business_ds = new FireBusiness_ds();    //销售数据集
		string AC001 = string.Empty;                            //逝者编号

		OracleParameter parm1 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
		OracleParameter parm2 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);

		OracleDataReader reader = null;

		public FireBusiness()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 对象装入事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FireBusiness_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = business_ds.Sa01;

			parm1.Direction = ParameterDirection.Input;
			parm2.Direction = ParameterDirection.Input;

			business_ds.sa01Adapter.SelectCommand.Parameters.Add(parm1);
 
			//this.Business_Init();

			//与逝者关系
			//lookup_ac052.Properties.DataSource = business_ds.St01_relation;
			//lookup_ac052.Properties.DisplayMember = "ST003";
			//lookup_ac052.Properties.ValueMember = "ST001";

			//经办人
			lookup_sa100.DataSource = business_ds.Uc01;


			//守灵厅
			lookup_store.Properties.DataSource = business_ds.AllItem;
			//告别厅
			lookUp_gbt.Properties.DataSource = business_ds.AllItem;
		}

		/// <summary>
		/// 业务初始化
		/// </summary>
		public override void Business_Init()
		{
			//获取逝者编号
			AC001 = this.swapdata["parm"].ToString();

			//填充逝者个人信息
			parm1.Value = AC001;
			parm2.Value = AC001;

			reader = SqlAssist.ExecuteReader("select * from v_ac01 where ac001 = :ac001", new OracleParameter[] { parm2 });
			

			if (!reader.HasRows)
			{
			   	XtraMessageBox.Show("参数传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			reader.Read();

			txtedit_ac001.EditValue = AC001;
			txtedit_ac003.EditValue = reader["AC003"];  //逝者姓名
			txtedit_ac004.EditValue = reader["AC004"];	//年龄
			rg_ac002.EditValue = reader["AC002"];		//性别
			txtedit_ac020.EditValue = reader["AC020"];  //到达中心时间
			txtedit_ac050.EditValue = reader["AC050"];  //联系人
			txtedit_ac051.EditValue = reader["AC051"];  //电话
			txtedit_ac052.Text = reader["AC052"].ToString();	//与逝者关系

			this.Parent.Text = "火化业务办理" + "【" + reader["AC003"] + "】";

			reader.Dispose();

			//读入照片		
			if (MiscAction.HasIDC(AC001))
			{
				OracleDataReader photo_reader = SqlAssist.ExecuteReader("select ic020 from ic01 where ic000 = '0' and ac001 ='" + AC001 + "'");
				if (photo_reader.HasRows && photo_reader.Read())
				{
					MemoryStream ms = new MemoryStream((byte[])photo_reader["IC020"]);//把照片读到MemoryStream里  
					Image imageBlob = Image.FromStream(ms, true);//用流创建Image  
					pictureEdit1.Image = imageBlob;//输出图片   
				}
				photo_reader.Dispose();
			}
			else
			{
				pictureEdit1.Image = null;
			}
			 
			///刷新销售数据
			this.RefreshSalesData();
		}

		/// <summary>
		/// 刷新销售数据
		/// </summary>
		public void RefreshSalesData()
		{
			gridView1.BeginUpdate();
			business_ds.Sa01.Rows.Clear();
			business_ds.sa01Adapter.Fill(business_ds.Sa01);
			gridView1.EndUpdate();

			this.RefreshPanel();
		}

		/// <summary>
		/// 刷新业务显示面板
		/// </summary>
		private void RefreshPanel()
		{
			int rowHandle = int.MinValue;
			 
			//存放位置
			lookup_store.EditValue = FireAction.GetFireStoreId(AC001);

			//休息室
			txtedit_xxs.EditValue = FireAction.GetRestRoomList(AC001);

			//告别厅
			rowHandle = gridView1.LocateByValue("SA002", "04");
			if (rowHandle >= 0)
			{
				lookUp_gbt.EditValue = gridView1.GetRowCellValue(rowHandle, "SA004");
			}

			//告别时间
			txtedit_ac018.EditValue = FireAction.GetGBTime(AC001);
			//火化时间
			txtedit_ac015.EditValue = FireAction.GetHHTime(AC001);


			///计算项目金额汇总
			decimal dec_tax = new decimal(0);
			decimal dec_fin = new decimal(0);
			foreach(DataRow dr in business_ds.Sa01.Rows)
			{
				if (dr["SA020"].ToString() == "T")
				{
					if(!(dr["SA007"] is DBNull))
						dec_tax += Convert.ToDecimal(dr["SA007"]);
				}
				else if (dr["SA020"].ToString() == "F")
				{
					if (!(dr["SA007"] is DBNull))
						dec_fin += Convert.ToDecimal(dr["SA007"]);
				}
					
			}

			te_fin.Text = dec_fin.ToString("##,##0.00");
			te_tax.Text = dec_tax.ToString("##,##0.00");
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
		/// 守灵厅办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			if (gridView1.LocateByValue("SA002", "01") >= 0 || gridView1.LocateByValue("SA002", "02") >= 0)
			{
				if(XtraMessageBox.Show("已经办理守灵或冷藏业务!确认要继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}

			Frm_business01 frm_slt = new Frm_business01();
			frm_slt.swapdata["dataset"] = this.business_ds;
			frm_slt.swapdata["AC001"] = AC001;
			frm_slt.swapdata["SALESTYPE"] = "0";    //火化业务

			if (frm_slt.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
			frm_slt.Dispose();
		}

		/// <summary>
		/// 冷藏办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			if (gridView1.LocateByValue("SA002", "01") >= 0 || gridView1.LocateByValue("SA002", "02") >= 0)
			{
				if (XtraMessageBox.Show("已经办理守灵或冷藏业务!确认要继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}

			Frm_business02 frm_lcg = new Frm_business02();
			frm_lcg.swapdata["dataset"] = business_ds;
			frm_lcg.swapdata["AC001"] = AC001;
			frm_lcg.swapdata["SALESTYPE"] = "0";

			if (frm_lcg.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
			frm_lcg.Dispose();
		}

		/// <summary>
		/// 休息室
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_business03 frm_xxs = new Frm_business03();
			frm_xxs.swapdata["dataset"] = business_ds;
			frm_xxs.swapdata["AC001"] = AC001;
			frm_xxs.swapdata["SALESTYPE"] = "0";


			if (frm_xxs.ShowDialog() == DialogResult.OK)
			{
				List<string> itemIdList = frm_xxs.swapdata["xxs"] as List<string>;
				int result = 0;
				foreach (string s in itemIdList)
				{
					result = FireAction.FireSales_03(AC001,
													 s,
													 Envior.cur_userId
					);
				}
				RefreshSalesData();
			}
			frm_xxs.Dispose();
		}

		/// <summary>
		/// 告别办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "04");
			if (row >= 0)
			{
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
				{
					XtraMessageBox.Show("告别已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (XtraMessageBox.Show("已经办理告别业务,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}

			Frm_business04 frm_gbt = new Frm_business04();
			frm_gbt.swapdata["dataset"] = business_ds;
			frm_gbt.swapdata["AC001"] = AC001;
			frm_gbt.swapdata["SALESTYPE"] = "0";


			if (frm_gbt.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
			frm_gbt.Dispose();
		}

		/// <summary>
		/// 灵车办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "07");
			if (row >= 0)
			{
				//if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
				//{
				//	XtraMessageBox.Show("灵车已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//	return;
				//}
				if (XtraMessageBox.Show("已经办理灵车业务,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}
			Frm_business07 frm_lc = new Frm_business07();
			frm_lc.swapdata["dataset"] = business_ds;
			frm_lc.swapdata["AC001"] = AC001;
			frm_lc.swapdata["SALESTYPE"] = "0";

			if (frm_lc.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
			frm_lc.Dispose();
		}

		/// <summary>
		/// 火化办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "06");
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//if (row >= 0)
			//{
			//	if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
			//	{
			//		XtraMessageBox.Show("火化已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//		return;
			//	}
			//	if (XtraMessageBox.Show("已经办理火化业务,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			//		return;
			//}
			Frm_business06 frm_hh = new Frm_business06();
			frm_hh.swapdata["dataset"] = business_ds;
			frm_hh.swapdata["AC001"] = AC001;
 

			if (frm_hh.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
			frm_hh.Dispose();
		}

		/// <summary>
		/// 服务商品
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_businessMisc frm_misc = new Frm_businessMisc();
			frm_misc.swapdata["dataset"] = business_ds;
			frm_misc.swapdata["SALESTYPE"] = "0";

			if (frm_misc.ShowDialog() == DialogResult.OK)
			{
				List<string> itemId_list = frm_misc.swapdata["itemIdList"] as List<string>;
				List<string> itemType_list = frm_misc.swapdata["itemTypeList"] as List<string>;
				List<decimal> price_list = frm_misc.swapdata["priceList"] as List<decimal>;
				List<decimal> nums_list = frm_misc.swapdata["numsList"] as List<decimal>;
				int re = 0;

				for (int i = 0; i < itemId_list.Count; i++)
				{
					if (itemType_list[i] == "10" || itemType_list[i] == "11")
					{
						re = gridView1.LocateByValue("SA002", itemType_list[i]);
						if (re > 0)
						{
							//如果已经办理 谷类或纸类并且已经结算,则跳过
							if (gridView1.GetRowCellValue(re, "SA008").ToString() == "1")
								continue;
							else
							{
								if (itemType_list[i] == "10")
								{
									if (XtraMessageBox.Show("已经选择【骨灰盒】,是否要继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
								}
								else if (itemId_list[i] == "11")
								{
									if (XtraMessageBox.Show("已经选择【纸棺】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
								}
							}
						}
					}

					re = gridView1.LocateByValue("SA004", itemId_list[i]);
					if (re >= 0)
					{
						if (XtraMessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要继续选择吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
					}
					re = FireAction.FireSales_Misc(AC001,
												   itemId_list[i],
												   nums_list[i],
												   Envior.cur_userId
					);
					if (re < 0) return;
				}
				RefreshSalesData();
			}
			frm_misc.Dispose();
		}

		/// <summary>
		/// 转换 票别 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName.ToUpper() == "SA020")  //票别
			{
				if (e.Value.ToString() == "T")
					e.DisplayText = "税票";
				else if (e.Value.ToString() == "F")
					e.DisplayText = "财政票";
			}else if(e.Column.FieldName.ToUpper() == "SA008")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "未结算";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "已结算";
			}
		}

		/// <summary>
		/// 删除项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.SelectedRowsCount == 0)
			{
				XtraMessageBox.Show("请先选择要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string sa001;
			int re;

			if (XtraMessageBox.Show("确认要删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

			foreach (int i in gridView1.GetSelectedRows())
			{
				//权限检查
				if (!AppAction.CheckRight("业务项目删除", gridView1.GetRowCellValue(i, "SA100").ToString())) continue;

				sa001 = gridView1.GetRowCellValue(i, "SA001").ToString();
				re = FireAction.FireBusinessRemove(sa001);
				if (re < 0) return;
			}

			this.RefreshSalesData();
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
		/// 双击修改
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			int row = -1;
			if ((row = (sender as ColumnView).FocusedRowHandle) >= 0)
			{
				this.SalesEdit(row);
			}
		}

		/// <summary>
		/// 业务项目编辑
		/// </summary>
		/// <param name="rowIndex"></param>
		private void SalesEdit(int rowIndex)
		{
			if (gridView1.GetRowCellValue(rowIndex, "SA008").ToString() == "1")
			{
				XtraMessageBox.Show("结算完成的记录不能修改!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//权限检查
			if (!AppAction.CheckRight("业务项目修改", gridView1.GetRowCellValue(rowIndex, "SA100").ToString())) return;
 
			string sa001 = gridView1.GetRowCellValue(rowIndex, "SA001").ToString();
			int index = gridView1.GetDataSourceRowIndex(rowIndex);
			Frm_salesEdit frm_edit = new Frm_salesEdit();

			frm_edit.swapdata["DATAROW"] = business_ds.Sa01.Rows[index];

			if (frm_edit.ShowDialog() == DialogResult.OK)
			{
				this.RefreshSalesData();
			}
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			RefreshSalesData();
		}

		/// <summary>
		/// 应用套餐
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				XtraMessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_ComboSelect frm_combo = new Frm_ComboSelect();
			frm_combo.swapdata["AC001"] = AC001;

			if (frm_combo.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}

			frm_combo.Dispose();
		}

		/// <summary>
		/// 结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("火化业务结算")) return;
			this.SettleHandle();
		}


		/// <summary>
		/// 结算办理
		/// </summary>
		private void SettleHandle()
		{
			//权限检查
			if (!AppAction.CheckRight("火化业务结算")) return;

			if (gridView1.GetSelectedRows().Length <= 0)
			{
				XtraMessageBox.Show("请选择要结算的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			List<int> rowList = new List<int>();
			//检查是否有未输入单价项目
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if (!gridView1.IsRowSelected(i) || gridView1.GetRowCellValue(i, "SA008").ToString() == "1") continue;
				if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")) == 0)
				{
					if(gridView1.GetRowCellValue(i,"SA002").ToString() == "06")  //如果是火化 价格位0 是低保
					{
						if (XtraMessageBox.Show("本次结算含有0元火化费,是否为低保火化?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No) return;
					}
					else
					{
						XtraMessageBox.Show("第" + (i + 1).ToString() + "行项目未输入价格!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}					
				}
				if (gridView1.GetRowCellValue(i, "INVOICECODE") is DBNull)
				{
					XtraMessageBox.Show("第" + (i + 1).ToString() + "行项目未设置发票编码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				rowList.Add(gridView1.GetDataSourceRowIndex(i));
			}

			int i_find = gridView1.LocateByValue("SA002", "06");
			if(i_find >=0 && gridView1.IsRowSelected(i_find))
			{
				if (SqlAssist.ExecuteScalar("select ac015 from ac01 where ac001= '" + AC001 + "'") is DBNull)
				{
					Frm_FireTime frm_1 = new Frm_FireTime();
					if (frm_1.ShowDialog() != DialogResult.OK)
					{
						frm_1.Dispose();
						return;
					}
					DateTime dt_fire = Convert.ToDateTime(frm_1.swapdata["AC015"]);
					if (FireAction.SetFireTime(AC001, dt_fire.ToString("yyyy-MM-dd HH:mm")) < 0) return;
					txtedit_ac015.EditValue = dt_fire;
					frm_1.Dispose();
				}
			}
			 
			Frm_FireSettle frm_settle = new Frm_FireSettle();
			frm_settle.swapdata["dataset"] = business_ds;
			frm_settle.swapdata["AC001"] = AC001;
			frm_settle.swapdata["rowList"] = rowList;


			if (frm_settle.ShowDialog() == DialogResult.OK)
			{
				this.RefreshSalesData();
			}
			frm_settle.Dispose();

			CancelSelect();
		}
		 

		/// <summary>
		/// 取消所有选择的行
		/// </summary>
		private void CancelSelect()
		{
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				gridView1.UnselectRow(i);
			}
		}

		/// <summary>
		/// 全部结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if(gridView1.GetRowCellValue(i,"SA008").ToString() == "0")
				{
					gridView1.SelectRow(i);
				}
			}
			this.SettleHandle();
		}

		private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			if (!AppAction.CheckRight("设置火化时间")) return;

			////// 检查是否 火化结算完成  //////
			if (FireAction.FireIsSettled(AC001) == "1" && Envior.cur_userId != AppInfo.ROOTID)
			{
				XtraMessageBox.Show("火化业务已经办理并且结算,不能修改!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}

			Frm_FireTime frm_1 = new Frm_FireTime();
			if (frm_1.ShowDialog() != DialogResult.OK)
			{
				frm_1.Dispose();
				return;
			}
			DateTime dt_fire = Convert.ToDateTime(frm_1.swapdata["AC015"]);
			//XtraMessageBox.Show(dt_fire.ToString("yyyy-MM-dd HH:mm"));
			if (FireAction.SetFireTime(AC001, dt_fire.ToString("yyyy-MM-dd HH:mm")) < 0) return;
			txtedit_ac015.EditValue = dt_fire;
			frm_1.Dispose(); 
		}

		/// <summary>
		/// 编辑项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int row = -1;
			if ((row = gridView1.FocusedRowHandle) >= 0)
			{
				this.SalesEdit(row);
			}
		}

		private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				//int row = gridView1.FocusedRowHandle;
				int row = e.ControllerRow;
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")
				{
					MessageBox.Show("已结算数据不能修改!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					gridView1.UnselectRow(row);
				}
			}
			else if (e.Action == CollectionChangeAction.Refresh && gridView1.SelectedRowsCount > 0)
			{
				gridView1.BeginUpdate();
				for (int i = 0; i < gridView1.RowCount; i++)
				{
					if (gridView1.GetRowCellValue(i, "SA008").ToString() == "1")
					{
						gridView1.UnselectRow(i);
					}
				}
				gridView1.EndUpdate();
			}
		}

		
	}
}
