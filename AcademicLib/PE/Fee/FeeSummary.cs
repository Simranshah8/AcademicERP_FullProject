using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class FeeSummary : Dynamic.Accounting.IReportLoadObjectData
    {

        public FeeSummary(List<AcademicLib.RE.Fee.FeeSummary> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.FeeSummary> dataColl = null;
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
            //StudentId, RegdNo, ClassName, SectionName, RollNo, Name, FatherName, F_ContactNo, MotherName, M_ContactNo, Address, IsLeft, IsFixedStudent, IsHostel,
            //IsNewStudent, IsTransport, Opening, DrAmt, DrDiscountAmt, DrFineAmt, DrTax, DrTotal, CrAmt, CrDiscountAmt, CrFineAmt, TotalDebit, TotalCredit, TotalDues,
            //UserId, MonthName, CardNo, EnrollNo, LedgerPanaNo, ClassOrderNo, FeeItemName, RouteName, PointName, BoardersName, AutoNumber, LastReceiptDate, LastReceiptMiti,
            //LastReceiptNo, LastReceiptAmt, FutureDR, FutureCR, FutureDues, Level, Faculty, Semester, ClassYear, Batch, Email 

               AcademicLib.RE.Fee.FeeSummary csl = (AcademicLib.RE.Fee.FeeSummary)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.RegdNo;
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
            row.Data[16] = csl.Opening;
            row.Data[17] = csl.DrAmt;
            row.Data[18] = csl.DrDiscountAmt;
            row.Data[19] = csl.DrFineAmt;
            row.Data[20] = csl.DrTax;
            row.Data[21] = csl.DrTotal;
            row.Data[22] = csl.CrAmt;
            row.Data[23] = csl.CrDiscountAmt;
            row.Data[24] = csl.CrFineAmt;
            row.Data[25] = csl.TotalDebit;
            row.Data[26] = csl.TotalCredit;
            row.Data[27] = csl.TotalDues;
            row.Data[28] = csl.UserId;

            try
            {
                row.Data[29] = csl.MonthName;
                row.Data[30] = csl.CardNo;
                row.Data[31] = csl.EnrollNo;
                row.Data[32] = csl.LedgerPanaNo;
                row.Data[33] = csl.ClassOrderNo;
                row.Data[34] = csl.FeeItemName;

                row.Data[35] = csl.RouteName;
                row.Data[36] = csl.PointName;
                row.Data[37] = csl.BoardersName;
                row.Data[38] = csl.AutoNumber;
                row.Data[39] = csl.LastReceiptDate;
                row.Data[40] = csl.LastReceiptMiti;
                row.Data[41] = csl.LastReceiptNo;
                row.Data[42] = csl.LastReceiptAmt;

                row.Data[43] = csl.FutureDR;
                row.Data[44] = csl.FutureCR;
                row.Data[45] = csl.FutureDues;
                 
                row.Data[46] = csl.Level;
                row.Data[47] = csl.Faculty;
                row.Data[48] = csl.Semester;
                row.Data[49] = csl.ClassYear;
                row.Data[50] = csl.Batch;
                row.Data[51] = csl.Email;

                row.Data[52] = csl.IsDefaulter;
                row.Data[53] = csl.RefTranId;
                row.Data[54] = csl.FollowupRemarks;
                row.Data[55] = csl.NextFollowupDateTime;
                row.Data[56] = csl.NextFollowupMiti;
                row.Data[57] = csl.NextFollowupBy;
                row.Data[58] = csl.ClosedRemarks;
                row.Data[59] = csl.ClosedBy;
                row.Data[60] = csl.ClosedDate;
                row.Data[61] = csl.ClosedMiti;
                row.Data[62] = csl.LeftDate;
                row.Data[63] = csl.LeftMiti;
                row.Data[64] = csl.LeftReason;
                row.Data[65] = csl.HouseName;
                row.Data[66] = csl.HouseDress;
                row.Data[67] = csl.VehicleName;
                row.Data[68] = csl.VehicleNumber;
               
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
