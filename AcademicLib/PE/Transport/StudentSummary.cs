using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Transport
{
    public class StudentSummary : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentSummary(List<AcademicLib.RE.Transport.StudentSummary> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Transport.StudentSummary> dataColl = null;
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
            // RouteName, PointName, TravelType, Rate, PickupTime, DropTime, ArrivalTime, DepartureTime, DuesAmt, DebitAmt, CreditAmt, StudentId,
            // UserId, AutoNumber, RegNo, Name, ClassName, SectionName, RollNo, Gender, FatherName, F_ContactNo, MotherName, M_ContactNo, ContactNo,
            // Address, PhotoPath, BloodGroup, DOB_AD, DOB_BS, HouseName, Medium, BoardName, BoardRegNo, EnrollNo, CardNo, BusStop, BusPoint,
            // GuardianName, G_Relation, G_Address, G_ContacNo, UserName, ClassSection, SMSText, AdmitDate_AD, AdmitDate_BS, LeftDate_AD,
            // LeftDate_BS, LeftRemarks, CurrentAddress, StudentType, Caste, AgeRange, LedgerPanaNo, EMSId, IsNew, FatherOccupation,
            // GuardianOccupation, MotherOccupation, Level, Faculty, Semester, ClassYear, Batch, Email 

            try
            {
                AcademicLib.RE.Transport.StudentSummary csl = (AcademicLib.RE.Transport.StudentSummary)obj;
                row.Data[0] = csl.RouteName;
                row.Data[1] = csl.PointName;
                row.Data[2] = csl.TravelType; 
                row.Data[3] = csl.Rate;
                row.Data[4] = csl.PickupTime;
                row.Data[5] = csl.DropTime;
                row.Data[6] = csl.ArrivalTime;
                row.Data[7] = csl.DepartureTime;
                row.Data[8] = csl.DuesAmt;
                row.Data[9] = csl.DebitAmt;
                row.Data[10] = csl.CreditAmt;
                row.Data[11] = csl.StudentId;
                row.Data[12] = csl.UserId;
                row.Data[13] = csl.AutoNumber;
                row.Data[14] = csl.RegNo;
                row.Data[15] = csl.Name;
                row.Data[16] = csl.ClassName;
                row.Data[17] = csl.SectionName;
                row.Data[18] = csl.RollNo;
                row.Data[19] = csl.Gender;
                row.Data[20] = csl.FatherName;
                row.Data[21] = csl.F_ContactNo;
                row.Data[22] = csl.MotherName;
                row.Data[23] = csl.M_ContactNo;
                row.Data[24] = csl.ContactNo;
                row.Data[25] = csl.Address;
                row.Data[26] = csl.PhotoPath;
                row.Data[27] = csl.BloodGroup;
                row.Data[28] = csl.DOB_AD;
                row.Data[29] = csl.DOB_BS;
                row.Data[30] = csl.HouseName;
                row.Data[31] = csl.Medium;
                row.Data[32] = csl.BoardName;
                row.Data[33] = csl.BoardRegNo;
                row.Data[34] = csl.EnrollNo;
                row.Data[35] = csl.CardNo;
                row.Data[36] = csl.BusStop;
                row.Data[37] = csl.BusPoint;
                row.Data[38] = csl.GuardianName;
                row.Data[39] = csl.G_Relation;
                row.Data[40] = csl.G_Address;
                row.Data[41] = csl.G_ContacNo;
                row.Data[42] = csl.UserName;
                row.Data[43] = csl.ClassSection;
                row.Data[44] = csl.SMSText;
                row.Data[45] = csl.AdmitDate_AD;
                row.Data[46] = csl.AdmitDate_BS;
                row.Data[47] = csl.LeftDate_AD;
                row.Data[48] = csl.LeftDate_BS;
                row.Data[49] = csl.LeftRemarks;
                row.Data[50] = csl.CurrentAddress;
                row.Data[51] = csl.StudentType;
                row.Data[52] = csl.Caste;
                row.Data[53] = csl.AgeRange;
                row.Data[54] = csl.LedgerPanaNo;
                row.Data[55] = csl.EMSId;
                row.Data[56] = csl.IsNew;
                row.Data[57] = csl.FatherOccupation;
                row.Data[58] = csl.GuardianOccupation;
                row.Data[59] = csl.MotherOccupation;
                row.Data[60] = csl.Level;
                row.Data[61] = csl.Faculty;
                row.Data[62] = csl.Semester;
                row.Data[63] = csl.ClassYear;
                row.Data[64] = csl.Batch;
                row.Data[65] = csl.Email;
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
