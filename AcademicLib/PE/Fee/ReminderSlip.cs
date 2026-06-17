using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class ReminderSlip : Dynamic.Accounting.IReportLoadObjectData
    {

        public ReminderSlip(List<AcademicLib.RE.Fee.ReminderSlip> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.ReminderSlip> dataColl = null;
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
            //StudentId, RegNo, ClassName, SectionName, RollNo, Name, FatherName, F_ContactNo, MotherName, M_ContactNo, Address, IsLeft,
            //IsFixedStudent, IsHostel, IsNewStudent, IsTransport, Debit, Credit, Balance, UserId, TransportRoute, TransportPoint, RoomName,
            //ReminderNotes, CompName, CompAddress, CompPhoneNo, CompFaxNo, CompEmailId, CompWebSite, CompLogoPath, CompImgPath, CompBannerPath,
            //CompanyRegdNo, CompanyPanVat, ClassSec, UptoMonth, ClassOrderNo, Level, Faculty, Semester, ClassYear, Batch, Email 

           
            AcademicLib.RE.Fee.ReminderSlip csl = (AcademicLib.RE.Fee.ReminderSlip)obj;
            try
            {
                row.Data[0] = csl.StudentId;
                row.Data[1] = csl.RegNo;
                row.Data[2] = csl.ClassName;
                row.Data[3] = csl.SectionName;
                row.Data[4] = csl.RollNo;
                row.Data[5] = csl.Name;
                row.Data[6] = csl.FatherName;
                row.Data[7] = csl.F_ContactNo;
                row.Data[8] = csl.MotherName;
                row.Data[9] = csl.M_ContactNo;
                row.Data[10] = csl.Address;
                row.Data[11] = csl.IsLeft;
                row.Data[12] = csl.IsFixedStudent;
                row.Data[13] = csl.IsHostel;
                row.Data[14] = csl.IsNewStudent;
                row.Data[15] = csl.IsTransport;
                row.Data[16] = csl.Debit;
                row.Data[17] = csl.Credit;
                row.Data[18] = csl.Balance;
                row.Data[19] = csl.UserId;
                row.Data[20] = csl.TransportRoute;
                row.Data[21] = csl.TransportPoint;
                row.Data[22] = csl.RoomName;
                row.Data[23] = csl.ReminderNotes;
                row.Data[24] = csl.CompName;
                row.Data[25] = csl.CompAddress;
                row.Data[26] = csl.CompPhoneNo;
                row.Data[27] = csl.CompFaxNo;
                row.Data[28] = csl.CompEmailId;
                row.Data[29] = csl.CompWebSite;
                row.Data[30] = csl.CompLogoPath;
                row.Data[31] = csl.CompImgPath;
                row.Data[32] = csl.CompBannerPath;
                row.Data[33] = csl.CompanyRegdNo;
                row.Data[34] = csl.CompanyPanVat;
                row.Data[35] = csl.ClassSec;
                row.Data[36] = csl.UptoMonth;
                row.Data[37] = csl.ClassOrderNo;

                row.Data[38] = csl.Level;
                row.Data[39] = csl.Faculty;
                row.Data[40] = csl.Semester;
                row.Data[41] = csl.ClassYear;
                row.Data[42] = csl.Batch;
                row.Data[43] = csl.Email;
                 

            }
            catch { }


        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
