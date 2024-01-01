using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brown.DataSet
{
	class FireBusiness_ds : System.Data.DataSet
	{
		public DataTable Sa01 { get; }						//销售表
		public OracleDataAdapter sa01Adapter { get; }       //销售表-适配器

		public DataTable St01 { get; }                      //系统数据字典表
		public OracleDataAdapter st01Adapter { get; }       //系统数据字典适配器

		public DataView St01_relation { get; }              //与逝者关系 视图

		public DataTable Uc01 { get; }						//操作员表
		public OracleDataAdapter uc01Adapter { get; }		//操作员适配器

		public DataTable AllItem { get; }				    //商品服务表
		public OracleDataAdapter allItemAdapter { get; }    //商品服务适配器

		public FireBusiness_ds()
		{
			//1.
			DataColumn col_sa001 = new DataColumn("SA001", typeof(string));    // 销售流水号
			DataColumn col_ac001 = new DataColumn("AC001", typeof(string));   // 逝者编号
			DataColumn col_sa002 = new DataColumn("SA002", typeof(string));   // 服务或商品类别
			DataColumn col_sa003 = new DataColumn("SA003", typeof(string));   // 服务或商品名称
			DataColumn col_sa004 = new DataColumn("SA004", typeof(string));   // 服务或商品编号
			DataColumn col_sa005 = new DataColumn("SA005", typeof(string));   // 销售类别 0-火化业务 1-临时性销售 2骨灰寄存
			DataColumn col_price = new DataColumn("PRICE", typeof(decimal));  // 单价
			DataColumn col_nums = new DataColumn("NUMS", typeof(decimal));    // 数量
			DataColumn col_sa007 = new DataColumn("SA007", typeof(decimal));  // 销售金额
			DataColumn col_sa006 = new DataColumn("SA006", typeof(decimal));  // 原始单价
			DataColumn col_sa008 = new DataColumn("SA008", typeof(string));   // 结算状态 0-未结算 1-已结算 2-退费
			DataColumn col_sa010 = new DataColumn("SA010", typeof(string));   // 结算流水号
			DataColumn col_sa020 = new DataColumn("SA020", typeof(string));   // 发票类型 F-财政发票 T-税票
			DataColumn col_sa025 = new DataColumn("SA025", typeof(decimal));   // 税率
			DataColumn col_sa100 = new DataColumn("SA100", typeof(string));   // 经办人
			DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 0-删除 1-正常

			Sa01 = new DataTable("Sa01");
			this.Tables.Add(Sa01);
			Sa01.Columns.AddRange(new DataColumn[] { col_sa001,col_ac001,col_sa002,col_sa003,col_sa004,col_sa005,col_price,col_nums,col_sa007,col_sa006,
				col_sa008,col_sa010,col_sa100,col_status,col_sa020,col_sa025
			});
 
			sa01Adapter = new OracleDataAdapter("select * from v_sa01 where sa005 = '0' and ac001 = :ac001 order by sa002", SqlAssist.conn);
			sa01Adapter.Requery = true;
			//2.
			St01 = new DataTable("St01");
			this.Tables.Add(St01);
			st01Adapter = new OracleDataAdapter("select * from st01 where st002 = 'RELATION' order by sortId", SqlAssist.conn);
			st01Adapter.Fill(St01);
			//3.
			St01_relation = new DataView(St01);

			//4.
			Uc01 = new DataTable("Uc01");
			this.Tables.Add(Uc01);
			uc01Adapter = new OracleDataAdapter("select * from uc01", SqlAssist.conn);
			uc01Adapter.Fill(Uc01);

			//5.系统商品服务项目
			AllItem = new DataTable("AllItem");
			this.Tables.Add(AllItem);
			allItemAdapter = new OracleDataAdapter("select item_id,item_type,item_text,price,sortId,zjf,invcode,invtype,1 nums,status from v_allitem order by sortId", SqlAssist.conn);
			allItemAdapter.Fill(AllItem);
		}
	}
}
