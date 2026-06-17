using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Hostel
{
    public class StudentSummary : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentSummary(List<AcademicLib.RE.Hostel.StudentSummary> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Hostel.StudentSummary> dataColl = null;
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
            //RoomName, BedName, BedNo, Rate, AllotDate, AllotMiti, DuesAmt, DebitAmt, CreditAmt, StudentId, UserId, AutoNumber, RegNo, Name, ClassName, SectionName, RollNo, Gender,
            //FatherName, F_ContactNo, MotherName, M_ContactNo, ContactNo, Address, PhotoPath, BloodGroup, DOB_AD, DOB_BS, HouseName, Medium, BoardName, BoardRegNo, EnrollNo, CardNo,
            //BusStop, BusPoint, GuardianName, G_Relation, G_Address, G_ContacNo, UserName, ClassSection, SMSText, AdmitDate_AD, AdmitDate_BS, LeftDate_AD, LeftDate_BS, LeftRemarks,
            //CurrentAddress, StudentType, Caste, AgeRange, LedgerPanaNo, EMSId  

            AcademicLib.RE.Hostel.StudentSummary csl = (AcademicLib.RE.Hostel.StudentSummary)obj;
            row.Data[0] = csl.RoomName;
            row.Data[1] = csl.BedName;
            row.Data[2] = csl.BedNo;
            row.Data[3] = csl.Rate;
            row.Data[4] = csl.AllotDate;
            row.Data[5] = csl.AllotMiti;
            row.Data[6] = csl.DuesAmt;
            row.Data[7] = csl.DebitAmt;
            row.Data[8] = csl.CreditAmt;            
            row.Data[9] = csl.StudentId;
            row.Data[10] = csl.UserId;
            row.Data[11] = csl.AutoNumber;
            row.Data[12] = csl.RegNo;
            row.Data[13] = csl.Name;
            row.Data[14] = csl.ClassName;
            row.Data[15] = csl.SectionName;
            row.Data[16] = csl.RollNo;
            row.Data[17] = csl.Gender;
            row.Data[18] = csl.FatherName;
            row.Data[19] = csl.F_ContactNo;
            row.Data[20] = csl.MotherName;
            row.Data[21] = csl.M_ContactNo;
            row.Data[22] = csl.ContactNo;
            row.Data[23] = csl.Address;
            row.Data[24] = csl.PhotoPath;
            row.Data[25] = csl.BloodGroup;
            row.Data[26] = csl.DOB_AD;
            row.Data[27] = csl.DOB_BS;
            row.Data[28] = csl.HouseName;
            row.Data[29] = csl.Medium;
            row.Data[30] = csl.BoardName;
            row.Data[31] = csl.BoardRegNo;
            row.Data[32] = csl.EnrollNo;
            row.Data[33] = csl.CardNo; 
            row.Data[34] = csl.BusStop;
            row.Data[35] = csl.BusPoint;
            row.Data[36] = csl.GuardianName;
            row.Data[37] = csl.G_Relation;
            row.Data[38] = csl.G_Address;
            row.Data[39] = csl.G_ContacNo;
            row.Data[40] = csl.UserName;
            row.Data[41] = csl.ClassSection;
            row.Data[42] = csl.SMSText;
            row.Data[43] = csl.AdmitDate_AD;
            row.Data[44] = csl.AdmitDate_BS;
            row.Data[45] = csl.LeftDate_AD;
            row.Data[46] = csl.LeftDate_BS;
            row.Data[47] = csl.LeftRemarks;
            row.Data[48] = csl.CurrentAddress;
            row.Data[49] = csl.StudentType;
            row.Data[50] = csl.Caste;
            row.Data[51] = csl.AgeRange;
            row.Data[52] = csl.LedgerPanaNo;
            row.Data[53] = csl.EMSId;
        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
