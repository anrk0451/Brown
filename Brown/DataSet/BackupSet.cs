using DevExpress.XtraEditors;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Brown.DataSet
{
    [Serializable]
    class BackupSet : System.Data.DataSet
    {
        private DataTable dt_rc01 = new DataTable("RC01");
        private OracleDataAdapter rc01Adapter = new OracleDataAdapter("select * from rc01", SqlAssist.conn);

        private DataTable dt_ac01 = new DataTable("AC01");
        private OracleDataAdapter ac01Adapter = new OracleDataAdapter("select * from ac01", SqlAssist.conn);

        private DataTable dt_bi01 = new DataTable("BI01");
        private OracleDataAdapter bi01Adapter = new OracleDataAdapter("select * from bi01", SqlAssist.conn);

        private DataTable dt_fa01 = new DataTable("FA01");
        private OracleDataAdapter fa01Adapter = new OracleDataAdapter("select * from fa01", SqlAssist.conn);

        private DataTable dt_fa02 = new DataTable("FA02");
        private OracleDataAdapter fa02Adapter = new OracleDataAdapter("select * from fa02", SqlAssist.conn);

        private DataTable dt_cb01 = new DataTable("CB01");
        private OracleDataAdapter cb01Adapter = new OracleDataAdapter("select * from cb01", SqlAssist.conn);

        private DataTable dt_cb02 = new DataTable("CB02");
        private OracleDataAdapter cb02Adapter = new OracleDataAdapter("select * from cb02", SqlAssist.conn);

        private DataTable dt_fa01_splog = new DataTable("FA01_SPLOG");
        private OracleDataAdapter fa01logAdapter = new OracleDataAdapter("select * from fa01_splog", SqlAssist.conn);

        private DataTable dt_fa05 = new DataTable("FA05");
        private OracleDataAdapter fa05Adapter = new OracleDataAdapter("select * from fa05", SqlAssist.conn);

        private DataTable dt_fc01 = new DataTable("FC01");
        private OracleDataAdapter fc01dapter = new OracleDataAdapter("select * from fc01", SqlAssist.conn);

        private DataTable dt_fin_log = new DataTable("FIN_LOG");
        private OracleDataAdapter finlogdapter = new OracleDataAdapter("select * from fin_log", SqlAssist.conn);

        private DataTable dt_fv01 = new DataTable("FV01");
        private OracleDataAdapter fv01dapter = new OracleDataAdapter("select * from fv01", SqlAssist.conn);

        private DataTable dt_gi01 = new DataTable("GI01");
        private OracleDataAdapter gi01dapter = new OracleDataAdapter("select * from gi01", SqlAssist.conn);

        private DataTable dt_gr01 = new DataTable("GR01");
        private OracleDataAdapter gr01dapter = new OracleDataAdapter("select * from gr01", SqlAssist.conn);

        private DataTable dt_ic01 = new DataTable("IC01");
        private OracleDataAdapter ic01dapter = new OracleDataAdapter("select * from ic01", SqlAssist.conn);

        private DataTable dt_in01 = new DataTable("IN01");
        private OracleDataAdapter in01dapter = new OracleDataAdapter("select * from in01", SqlAssist.conn);

        private DataTable dt_ly01 = new DataTable("LY01");
        private OracleDataAdapter ly01dapter = new OracleDataAdapter("select * from ly01", SqlAssist.conn);

        private DataTable dt_oc01 = new DataTable("OC01");
        private OracleDataAdapter oc01dapter = new OracleDataAdapter("select * from oc01", SqlAssist.conn);

        private DataTable dt_rc04 = new DataTable("RC04");
        private OracleDataAdapter rc04dapter = new OracleDataAdapter("select * from rc04", SqlAssist.conn);

        private DataTable dt_refund = new DataTable("REFUND");
        private OracleDataAdapter refundAdapter = new OracleDataAdapter("select * from refund", SqlAssist.conn);

        private DataTable dt_rg01 = new DataTable("RG01");
        private OracleDataAdapter rg01dapter = new OracleDataAdapter("select * from rg01", SqlAssist.conn);

        private DataTable dt_ri01 = new DataTable("RI01");
        private OracleDataAdapter ri01dapter = new OracleDataAdapter("select * from ri01", SqlAssist.conn);

        private DataTable dt_ro01 = new DataTable("RO01");
        private OracleDataAdapter ro01dapter = new OracleDataAdapter("select * from ro01", SqlAssist.conn);

        private DataTable dt_rt01 = new DataTable("RT01");
        private OracleDataAdapter rt01dapter = new OracleDataAdapter("select * from rt01", SqlAssist.conn);

        private DataTable dt_sa01 = new DataTable("SA01");
        private OracleDataAdapter sa01dapter = new OracleDataAdapter("select * from sa01", SqlAssist.conn);

        private DataTable dt_sa01log = new DataTable("SA01_LOG");
        private OracleDataAdapter sa01logdapter = new OracleDataAdapter("select * from sa01_log", SqlAssist.conn);

        private DataTable dt_sa10 = new DataTable("SA10");
        private OracleDataAdapter sa10dapter = new OracleDataAdapter("select * from sa10", SqlAssist.conn);

        private DataTable dt_si01 = new DataTable("SI01");
        private OracleDataAdapter si01dapter = new OracleDataAdapter("select * from si01", SqlAssist.conn);

        private DataTable dt_st01 = new DataTable("ST01");
        private OracleDataAdapter st01dapter = new OracleDataAdapter("select * from st01", SqlAssist.conn);

        private DataTable dt_taxlog = new DataTable("TAX_LOG");
        private OracleDataAdapter taxlogAdapter = new OracleDataAdapter("select * from tax_log", SqlAssist.conn);

        private DataTable dt_tu01 = new DataTable("TU01");
        private OracleDataAdapter tu01dapter = new OracleDataAdapter("select * from tu01", SqlAssist.conn);

        private DataTable dt_uc01 = new DataTable("UC01");
        private OracleDataAdapter uc01dapter = new OracleDataAdapter("select * from uc01", SqlAssist.conn);

        private DataTable dt_ws01 = new DataTable("WS01");
        private OracleDataAdapter ws01Adapter = new OracleDataAdapter("select * from ws01", SqlAssist.conn);

        private DataTable dt_urMapper = new DataTable("UR_MAPPER");
        private OracleDataAdapter urMapperdapter = new OracleDataAdapter("select * from ur_mapper", SqlAssist.conn);

        public BackupSet()
        {
            this.Tables.Add(dt_rc01);
            this.Tables.Add(dt_ac01);
            this.Tables.Add(dt_bi01);
            this.Tables.Add(dt_fa01);
            this.Tables.Add(dt_fa02);
            this.Tables.Add(dt_cb01);
            this.Tables.Add(dt_cb02);
            this.Tables.Add(dt_fin_log);
            this.Tables.Add(dt_fv01);
            this.Tables.Add(dt_ic01);
            this.Tables.Add(dt_in01);
            this.Tables.Add(dt_refund);

            this.Tables.Add(dt_fc01);
            this.Tables.Add(dt_gi01);
            this.Tables.Add(dt_gr01);
            this.Tables.Add(dt_ly01);
            this.Tables.Add(dt_oc01);
            this.Tables.Add(dt_rc04);
            this.Tables.Add(dt_rg01);
            this.Tables.Add(dt_ri01);
            this.Tables.Add(dt_ro01);
            this.Tables.Add(dt_rt01);
            this.Tables.Add(dt_sa01);
            this.Tables.Add(dt_sa01log);
            this.Tables.Add(dt_sa10);
            this.Tables.Add(dt_si01);
            this.Tables.Add(dt_st01);
            this.Tables.Add(dt_uc01);
            this.Tables.Add(dt_urMapper);
            this.Tables.Add(dt_tu01);
            this.Tables.Add(dt_taxlog);


            rc01Adapter.Fill(dt_rc01);
            rc01Adapter.Fill(dt_ac01);
            bi01Adapter.Fill(dt_bi01);
            fa01Adapter.Fill(dt_fa01);
            fa02Adapter.Fill(dt_fa02);
            fa01Adapter.Fill(dt_fa01_splog);
            fa05Adapter.Fill(dt_fa05);
            finlogdapter.Fill(dt_fin_log);
            fv01dapter.Fill(dt_fv01);
            ic01dapter.Fill(dt_ic01);
            in01dapter.Fill(dt_in01);
            refundAdapter.Fill(dt_refund);
            taxlogAdapter.Fill(dt_taxlog);
            tu01dapter.Fill(dt_tu01);
            ws01Adapter.Fill(dt_ws01);

            cb01Adapter.Fill(dt_cb01);
            cb01Adapter.Fill(dt_cb02);
            fc01dapter.Fill(dt_fc01);
            gi01dapter.Fill(dt_gi01);
            gr01dapter.Fill(dt_gr01);
            ly01dapter.Fill(dt_ly01);
            oc01dapter.Fill(dt_oc01);
            rc04dapter.Fill(dt_rc04);
            rg01dapter.Fill(dt_rg01);
            ri01dapter.Fill(dt_ri01);
            ro01dapter.Fill(dt_ro01);
            rt01dapter.Fill(dt_rt01);
            sa01dapter.Fill(dt_sa01);
            sa01logdapter.Fill(dt_sa01log);
            sa10dapter.Fill(dt_sa10);
            si01dapter.Fill(dt_si01);
            st01dapter.Fill(dt_st01);
            uc01dapter.Fill(dt_uc01);
            urMapperdapter.Fill(dt_urMapper);

        }

        public void Backup(string fname)
        {
			try
			{
                this.RemotingFormat = SerializationFormat.Binary;
                FileStream fs = new FileStream(fname, FileMode.Create);
                BinaryFormatter bFormat = new BinaryFormatter();
                bFormat.Serialize(fs, this);
                fs.Close();
                this.Clear();

                this.Dispose();
            }
			catch (Exception ee)
			{
                XtraMessageBox.Show(ee.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }
    }
}
