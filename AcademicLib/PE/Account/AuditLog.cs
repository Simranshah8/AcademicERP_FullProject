using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class AuditLog : Dynamic.Accounting.IReportLoadObjectData
    {

        public AuditLog(List<Dynamic.BusinessEntity.Global.AuditLog> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.BusinessEntity.Global.AuditLog> dataColl = null;
        public System.Collections.IEnumerable DataColl
        {
            get
            {
                return dataColl;
            }
        }

        public string ReportPath
        {
            get
            {
                return "";
            }
        }
        public void GetDataOfCurrentRow(object obj, Dynamic.ReportEngine.RDL.Row row)
        {

            Dynamic.BusinessEntity.Global.AuditLog ag = (Dynamic.BusinessEntity.Global.AuditLog)obj;
           //EntityId, Action, TranId, UserId, UserName, SystemUser, MacAddress, PCName, LogDate, Screen,
           //AutoManualNo, LogText, SNo  

            row.Data[0] = ag.EntityId;
            row.Data[1] = ag.Action;
            row.Data[2] = ag.TranId;
            row.Data[3] = ag.UserId;
            row.Data[4] = ag.UserName;
            row.Data[5] = ag.SystemUser;
            row.Data[6] = ag.MacAddress;
            row.Data[7] = ag.PCName;
            row.Data[8] = ag.LogDate;
            row.Data[9] = ag.Screen;
            row.Data[10] = ag.AutoManualNo;
            row.Data[11] = ag.LogText;
            row.Data[12] = ag.SNo;
            

        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
