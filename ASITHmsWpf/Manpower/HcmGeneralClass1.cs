using ASITHmsEntity;
using ASITFunLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ASITHmsViewMan.Manpower;

namespace ASITHmsWpf.Manpower
{


    public static class HcmGeneralClass1
    {
        private class attnEval
        {
            public DateTime schtime { get; set; }
            public DateTime attntime { get; set; }
            public double diffminute { get; set; }
            public double direction { get; set; }
            public bool isvalid { get; set; }
        }
        public static List<HmsEntityManpower.RptAttnSchInfo> GetIndRosterAttendance(string monthid1, string hccode1a, string RptType = "Attendance")
        {
            vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();
            var pap1r = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1a, "PRINT");
            DataSet ds1r = WpfProcessAccess.GetHmsDataSet(pap1r);
            if (ds1r == null)
                return null;

            if (ds1r.Tables[0].Rows.Count == 0)
                return null;

            var ListSchAttn1r = ds1r.Tables[0].DataTableToList<HmsEntityManpower.RptAttnSchInfo>();

            List<HmsEntityManpower.HcmDayWiseAttanReport> ListDayWiseAttnRptP = null;
            if (RptType == "Attendance")
            {
                string AttnDate1 = "%";

                string hcDept1 = "";
                var pap1 = vm2.SetParamShowActualAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, AttnDate1, AttnDate1, hccode1a, hcDept1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return null;

                ListDayWiseAttnRptP = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmDayWiseAttanReport>();
            }

            List<HmsEntityManpower.RptAttnSchInfo> Rptlst = new List<HmsEntityManpower.RptAttnSchInfo>();
            //foreach (var item in this.ListSchAttn1)
            foreach (var item in ListSchAttn1r)
            {
                HmsEntityManpower.RptAttnSchInfo a = new HmsEntityManpower.RptAttnSchInfo();
                a.attndate = item.attndate;
                a.attnhour = item.attnhour;
                a.attnrmrk = item.attnrmrk;
                a.attnstat = item.attnstat;
                a.attnstatid = item.attnstatid;
                a.hccode = item.hccode;
                a.intime1 = item.intime1;
                a.intime2 = item.intime2;
                a.monthid = item.monthid;
                a.newedit = item.newedit;
                a.outtime1 = item.outtime1;
                a.outtime2 = item.outtime2;
                a.schworkhr = item.schworkhr;
                a.breakhr = item.breakhr;
                a.visibletime = item.visibletime;
                a.wrkshift = item.wrkshift;
                a.actworkhr = 0.00m;
                a.actoffhr = 0.00m;
                a.lesworkhr = 0.00m;
                a.otworkhr = 0.00m;
                a.latein = 0.00m;
                a.earlyout = 0.00m;
                if (ListDayWiseAttnRptP != null)
                {
                    var attnRecord1 = ListDayWiseAttnRptP.Find(x => x.attndate.Day == item.attndate.Day);//.atndtl.Trim();
                    if (attnRecord1 != null)
                    {
                        a.attnrmrk = (item.attnrmrk + " " + attnRecord1.atndtl.Trim()).Trim();
                        var actInf = HcmGeneralClass1.CalcActWorkHourAdv(attnRecord1);// vmr1.CalcActWorkHourAdv(attnRecord1);
                        a.schworkhr = actInf.schworkhr;
                        a.breakhr = actInf.breakhr;
                        a.actworkhr = actInf.actworkhr;
                        a.actoffhr = actInf.actoffhr;
                        a.lesworkhr = actInf.lesworkhr;
                        a.otworkhr = actInf.otworkhr;
                        a.latein = actInf.latein;
                        a.latein1 = actInf.latein1;
                        a.latein2 = actInf.latein2;
                        a.confirmerr = actInf.confirmerr;
                        a.confirmlate = actInf.confirmlate;
                        a.confirmearly = actInf.confirmearly;
                        a.earlyout = actInf.earlyout;
                        a.earlyout1 = actInf.earlyout1;
                        a.earlyout2 = actInf.earlyout2;
                        a.attnrmrk = a.attnrmrk.Trim() + (a.attnrmrk.Trim().Length > 0 && actInf.attnrmrk.Trim().Length > 0 ? "\n" : "") + actInf.attnrmrk.Trim();
                    }
                }

                Rptlst.Add(a);
            }
            return Rptlst;
        }


        public static HmsEntityManpower.RptAttnSchInfo CalcActWorkHourAdv(HmsEntityManpower.HcmDayWiseAttanReport attn1)
        {
            var m_actinf = new HmsEntityManpower.RptAttnSchInfo();
            string m_str_attndate = attn1.attndate.ToString("dd-MMM-yyyy");
            DateTime m_intime1 = DateTime.Parse(m_str_attndate + " " + attn1.InTime1);
            DateTime m_intime2 = DateTime.Parse(m_str_attndate + " " + attn1.InTime2);
            DateTime m_outtime1 = DateTime.Parse(m_str_attndate + " " + attn1.OutTime1);
            DateTime m_outtime2 = DateTime.Parse(m_str_attndate + " " + attn1.OutTime2);
            m_outtime2 = (m_outtime2 < m_intime2 ? m_outtime2.AddDays(1) : m_outtime2);

            m_actinf.schworkhr = Convert.ToDecimal((m_outtime2.Subtract(m_intime1).TotalMinutes - m_intime2.Subtract(m_outtime1).TotalMinutes) / 60.00);    //.attn1.schworkhr;
            m_actinf.breakhr = Convert.ToDecimal(m_intime2.Subtract(m_outtime1).TotalMinutes / 60.00);

            if (attn1.wrkshift == 0 || attn1.attndate > DateTime.Today)
            {
                // Return Values After Calculation
                m_actinf.actworkhr = 0.00m;
                m_actinf.actoffhr = 0.00m;
                m_actinf.lesworkhr = 0.00m;
                m_actinf.otworkhr = 0.00m;
                m_actinf.attnrmrk = "";
                m_actinf.latein = 0.00m;
                m_actinf.latein1 = 0.00m;
                m_actinf.latein2 = 0.00m;
                m_actinf.earlyout = 0.00m;
                m_actinf.earlyout1 = 0.00m;
                m_actinf.earlyout2 = 0.00m;
                m_actinf.confirmlate = 0.00m;
                m_actinf.confirmearly = 0.00m;
                m_actinf.confirmerr = 0.00m;
                return m_actinf;
            }

            string[] m_punch = attn1.atndtl.Trim().Split('/');
            if (m_punch[0].Trim().Length == 0)
            {
                // Return Values After Calculation
                m_actinf.actworkhr = -0.0001m;
                m_actinf.actoffhr = -0.0001m;
                m_actinf.lesworkhr = -0.0001m;
                m_actinf.otworkhr = -0.0001m;
                m_actinf.latein = -0.0001m;
                m_actinf.latein1 = -0.0001m;
                m_actinf.latein2 = -0.0001m;
                m_actinf.earlyout = -0.0001m;
                m_actinf.earlyout1 = -0.0001m;
                m_actinf.earlyout2 = -0.0001m;
                m_actinf.confirmlate = 0.0000m;
                m_actinf.confirmearly = 0.0000m;
                m_actinf.confirmerr = 0.0000m;
                m_actinf.attnrmrk = "(Absent)";
                return m_actinf;
            }

            List<DateTime> m_punchs1 = new List<DateTime>();
            if (m_punch.Length > 0)
                m_punchs1.Add(DateTime.Parse(m_str_attndate + " " + m_punch[0].Trim()));

            if (m_punch.Length > 1)
            {
                //m_punchs1.Add(DateTime.Parse(m_str_attndate + " " + m_punch[0].Trim()));
                if (DateTime.Parse(m_str_attndate + " " + m_punch[1].Trim()).Subtract(DateTime.Parse(m_str_attndate + " " + m_punch[0].Trim())).TotalMinutes > 2)
                    m_punchs1.Add(DateTime.Parse(m_str_attndate + " " + m_punch[1].Trim()));
            }

            for (int j = 2; j < 9; j++)
            {
                if (m_punch.Length > j)
                {
                    if (DateTime.Parse(m_str_attndate + " " + m_punch[j].Trim()).Subtract(DateTime.Parse(m_str_attndate + " " + m_punch[j - 1].Trim())).TotalMinutes > 2)
                        m_punchs1.Add(DateTime.Parse(m_str_attndate + " " + m_punch[j].Trim()));
                    else
                        m_punchs1[m_punchs1.Count - 1] = DateTime.Parse(m_str_attndate + " " + m_punch[j].Trim());
                }
            }

            for (int k = 1; k < m_punchs1.Count; k++)
            {
                if (m_punchs1[k] < m_punchs1[k - 1])
                    m_punchs1[k] = m_punchs1[k].AddDays(1);
            }

            if (attn1.wrkshift == 1 && m_punchs1.Count > 1 && m_punch.Length > 1)
            {
                if (DateTime.Parse(m_str_attndate + " " + m_punch[m_punch.Length - 1].Trim()).Subtract(DateTime.Parse(m_str_attndate + " " + m_punch[m_punch.Length - 2].Trim())).TotalMinutes > 2)
                    m_punchs1[1] = (DateTime.Parse(m_str_attndate + " " + m_punch[m_punch.Length - 1].Trim()));
            }
            //-------------------------------------------------
            int MissResp1 = 0;
            int AprvErr1 = 0;
            if (attn1.approvals.Trim().Length > 5)
            {
                string[] Err1 = attn1.approvals.Split('|');
                for (int i = 0; i < Err1.Length; i++)
                {
                    if (Err1[i].Trim().Length > 1)
                    {
                        switch (Err1[i].Trim().Substring(0, 2))
                        {
                            case "1E":
                                m_punchs1.Add(m_intime1);
                                MissResp1++;
                                AprvErr1++;
                                break;
                            case "2E":
                                if (attn1.wrkshift == 2)
                                {
                                    m_punchs1.Add(m_outtime1);
                                    MissResp1++;
                                    AprvErr1++;
                                }
                                break;
                            case "3E":
                                if (attn1.wrkshift == 2)
                                {
                                    m_punchs1.Add(m_intime2);
                                    MissResp1++;
                                    AprvErr1++;
                                }
                                break;
                            case "4E":
                                m_punchs1.Add(m_outtime2);
                                MissResp1++;
                                AprvErr1++;
                                break;
                        }
                    }
                }
            }
            m_punchs1.Sort(delegate(DateTime x, DateTime y)
            {
                return (x).CompareTo(y);
            });
            //-------------------------------------------------
            List<attnEval> m_schs1 = new List<attnEval>();
            List<attnEval> m_schs2 = new List<attnEval>();
            if (attn1.wrkshift > 0)
            {
                m_schs1.Add(new attnEval() { schtime = m_intime1, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                m_schs2.Add(new attnEval() { schtime = m_intime1, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                if (m_outtime1 != m_intime2 && attn1.wrkshift == 2)
                {
                    m_schs1.Add(new attnEval() { schtime = m_outtime1, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                    m_schs1.Add(new attnEval() { schtime = m_intime2, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });

                    m_schs2.Add(new attnEval() { schtime = m_outtime1, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                    m_schs2.Add(new attnEval() { schtime = m_intime2, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                }
                m_schs1.Add(new attnEval() { schtime = m_outtime2, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
                m_schs2.Add(new attnEval() { schtime = m_outtime2, attntime = DateTime.Parse("01-Jan-1900"), diffminute = 0, direction = 0, isvalid = false });
            }

            if (m_punchs1.Count > 0)
            {
                var p1 = m_punchs1[0];
                m_schs1[0].attntime = p1;
                m_schs1[0].diffminute = (p1 > m_schs1[0].schtime ? p1.Subtract(m_schs1[0].schtime).TotalMinutes : m_schs1[0].schtime.Subtract(p1).TotalMinutes);
                m_schs1[0].direction = (p1 > m_schs1[0].schtime ? 1 : -1);
            }

            if (m_punchs1.Count > 1 && attn1.wrkshift == 1)
            {
                var p2 = m_punchs1[m_punchs1.Count - 1];
                m_schs1[1].attntime = p2;
                m_schs1[1].diffminute = (p2 > m_schs1[1].schtime ? p2.Subtract(m_schs1[1].schtime).TotalMinutes : m_schs1[1].schtime.Subtract(p2).TotalMinutes);
                m_schs1[1].direction = (p2 > m_schs1[1].schtime ? 1 : -1);
            }
            else if (attn1.wrkshift == 2)
            {
                List<DateTime> m_punchs2 = new List<DateTime>();

                double m_break1 = m_schs1[2].schtime.Subtract(m_schs1[1].schtime).TotalMinutes; // Break Hour
                var m_schs1a = m_schs1[1].schtime.AddMinutes(m_break1 / 2.0);

                for (int i = 1; i < m_punchs1.Count; i++)
                {
                    if (m_punchs1[i] <= m_schs1a) // if (m_punchs1[i] < m_schs1[2].schtime) /////////////////
                        m_punchs2.Add(m_punchs1[i]);
                }
                if (m_punchs2.Count > 0)
                {
                    var p2a = m_punchs2[m_punchs2.Count - 1]; // Consider Maximum No
                    double diff2 = (p2a > m_schs1[1].schtime ? p2a.Subtract(m_schs1[1].schtime).TotalMinutes : m_schs1[1].schtime.Subtract(p2a).TotalMinutes);
                    m_schs1[1].attntime = p2a;
                    m_schs1[1].diffminute = diff2;
                    m_schs1[1].direction = (p2a > m_schs1[1].schtime ? 1 : -1);
                }

                List<DateTime> m_punchs3 = new List<DateTime>();
                for (int j = 1; j < m_punchs1.Count; j++)
                {
                    if (m_punchs1[j] > m_schs1[1].schtime && m_punchs1[j] > m_schs1[1].attntime)
                        m_punchs3.Add(m_punchs1[j]);
                }

                if (m_punchs3.Count > 0)
                {
                    var p3 = m_punchs3[0];
                    m_schs1[2].attntime = p3;
                    m_schs1[2].diffminute = (p3 > m_schs1[2].schtime ? p3.Subtract(m_schs1[2].schtime).TotalMinutes : m_schs1[2].diffminute = m_schs1[2].schtime.Subtract(p3).TotalMinutes);
                    m_schs1[2].direction = (p3 > m_schs1[2].schtime ? 1 : -1);
                }
                if (m_punchs3.Count > 1)
                {
                    var p4 = m_punchs3[m_punchs3.Count - 1];
                    m_schs1[3].attntime = p4;
                    m_schs1[3].diffminute = (p4 > m_schs1[3].schtime ? p4.Subtract(m_schs1[2].schtime).TotalMinutes : m_schs1[3].diffminute = m_schs1[3].schtime.Subtract(p4).TotalMinutes);
                    m_schs1[3].direction = (p4 > m_schs1[3].schtime ? 1 : -1);
                }
            }

            var FindInvalid = m_schs1.FindAll(x => x.attntime == DateTime.Parse("01-Jan-1900"));
            MissResp1 = MissResp1 + FindInvalid.Count;
            if (MissResp1 > 0)
                m_actinf.attnrmrk = "(Missing Resp." + MissResp1.ToString("-#") + ") " + (AprvErr1 > 0 ? "[ Aprv." + AprvErr1.ToString("-#") + " ]" : "");

            if (FindInvalid.Count > 0)
            {
                // Return Values After Calculation
                m_actinf.actworkhr = -0.0001m;
                m_actinf.actoffhr = -0.0001m;
                m_actinf.lesworkhr = -0.0001m;
                m_actinf.otworkhr = -0.0001m;
                m_actinf.latein = -0.0001m;
                m_actinf.latein1 = -0.0001m;
                m_actinf.latein2 = -0.0001m;
                m_actinf.earlyout = -0.0001m;
                m_actinf.earlyout1 = -0.0001m;
                m_actinf.earlyout2 = -0.0001m;
                m_actinf.confirmlate = 0.0000m;
                m_actinf.confirmearly = 0.0000m;
                m_actinf.confirmerr = 0.0000m;
                m_actinf.confirmerr = 1.00m;
                return m_actinf;
            }

            m_actinf.actworkhr = Convert.ToDecimal(m_schs1[1].attntime.Subtract(m_schs1[0].attntime).TotalMinutes);
            m_schs1[0].attntime = (m_schs1[0].direction == -1 ? m_schs1[0].schtime : m_schs1[0].attntime);
            m_actinf.latein1 = (m_schs1[0].attntime > m_schs1[0].schtime ? Convert.ToDecimal(m_schs1[0].attntime.Subtract(m_schs1[0].schtime).TotalMinutes) : 0.00m);
            m_actinf.latein2 = 0.000m;

            m_schs1[1].attntime = (m_schs1[1].direction == 1 ? m_schs1[1].schtime : m_schs1[1].attntime);
            m_actinf.earlyout1 = (m_schs1[1].attntime < m_schs1[1].schtime ? Convert.ToDecimal(m_schs1[1].schtime.Subtract(m_schs1[1].attntime).TotalMinutes) : 0.00m);
            m_actinf.earlyout2 = 0.000m;
            m_actinf.actoffhr = 0.00m;

            if (attn1.wrkshift == 2)
            {
                m_actinf.actworkhr = m_actinf.actworkhr + Convert.ToDecimal(m_schs1[3].attntime.Subtract(m_schs1[2].attntime).TotalMinutes);
                m_actinf.actoffhr = m_actinf.actoffhr + Convert.ToDecimal(m_schs1[2].attntime.Subtract(m_schs1[1].attntime).TotalMinutes);
                m_schs1[2].attntime = (m_schs1[2].direction == -1 ? m_schs1[2].schtime : m_schs1[2].attntime);
                m_actinf.latein2 = (m_schs1[2].attntime > m_schs1[2].schtime ? Convert.ToDecimal(m_schs1[2].attntime.Subtract(m_schs1[2].schtime).TotalMinutes) : 0.00m);

                m_schs1[3].attntime = (m_schs1[3].direction == 1 ? m_schs1[3].schtime : m_schs1[3].attntime);
                m_actinf.earlyout2 = (m_schs1[3].attntime < m_schs1[3].schtime ? Convert.ToDecimal(m_schs1[3].schtime.Subtract(m_schs1[3].attntime).TotalMinutes) : 0.00m);
            }

            m_actinf.latein = m_actinf.latein1 + m_actinf.latein2;
            m_actinf.confirmlate = (m_actinf.latein1 > 5.00m ? 1.00m : 0.00m) + (m_actinf.latein2 > 5.00m ? 1.00m : 0.00m);
            m_actinf.earlyout = m_actinf.earlyout1 + m_actinf.earlyout2;
            m_actinf.confirmearly = (m_actinf.earlyout1 > 5.00m ? 1.00m : 0.00m) + (m_actinf.earlyout2 > 5.00m ? 1.00m : 0.00m);

            m_actinf.actworkhr = m_actinf.actworkhr / 60.00m;
            m_actinf.actoffhr = m_actinf.actoffhr / 60.00m;

            // Return Values After Calculation
            m_actinf.lesworkhr = (m_actinf.schworkhr > m_actinf.actworkhr ? (m_actinf.schworkhr - m_actinf.actworkhr) : 0.00m);
            m_actinf.otworkhr = (m_actinf.actworkhr > m_actinf.schworkhr ? (m_actinf.actworkhr - m_actinf.schworkhr) : 0.00m);
            m_actinf.attnrmrk = m_actinf.attnrmrk + (m_actinf.confirmlate == 0.00m ? "" : "(Late-" + (m_actinf.latein1 > 5.00m && m_actinf.latein2 > 5.00m ? "2" : "1") + ")");
            if (attn1.approvals.Trim().Length > 5)
            {
                int countr1a = 0;
                string[] Err1a = attn1.approvals.Split('|');
                for (int i = 0; i < Err1a.Length; i++)
                {
                    if (Err1a[i].Trim().Length > 1)
                        if (Err1a[i].Trim().Substring(1, 1) == "L")
                            countr1a++;
                }
                m_actinf.attnrmrk = m_actinf.attnrmrk + (countr1a > 0 ? " [ Aprv." + countr1a.ToString("-#") + " ]" : "");
                if (countr1a > 0)
                {
                    //m_actinf.latein = 0.00m;
                    m_actinf.confirmlate = 0.00m;
                }
            }

            m_actinf.attnrmrk = m_actinf.attnrmrk + (m_actinf.confirmearly == 0.00m ? "" : "(Early-" + (m_actinf.earlyout1 > 5.00m && m_actinf.earlyout2 > 5.00m ? "2" : "1") + ")");
            if (attn1.approvals.Trim().Length > 5)
            {
                int countr1o = 0;
                string[] Err1o = attn1.approvals.Split('|');
                for (int i = 0; i < Err1o.Length; i++)
                {
                    if (Err1o[i].Trim().Length > 1)
                        if (Err1o[i].Trim().Substring(1, 1) == "O")
                            countr1o++;
                }
                m_actinf.attnrmrk = m_actinf.attnrmrk + (countr1o > 0 ? " [ Aprv." + countr1o.ToString("-#") + " ]" : "");
                if (countr1o > 0)
                {
                    //m_actinf.earlyout = 0.00m;
                    m_actinf.confirmearly = 0.00m;
                }
            }
            //m_actinf.attnrmrk = "";
            return m_actinf;
        }
    }
}
