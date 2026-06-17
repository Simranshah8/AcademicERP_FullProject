using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Academic
{
    public class StudentSummary : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentSummary(List<AcademicLib.RE.Academic.StudentSummary> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Academic.StudentSummary> dataColl = null;
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

            // StudentId, UserId, AutoNumber, RegNo, Name, ClassName, SectionName, RollNo, Gender, FatherName, F_ContactNo, MotherName, M_ContactNo, ContactNo, Address, PhotoPath, BloodGroup,
            // DOB_AD, DOB_BS, HouseName, Medium, BoardName, BoardRegNo, EnrollNo, CardNo, BusStop, BusPoint, GuardianName, G_Relation, G_Address, G_ContacNo, UserName, ClassSection, SMSText,
            // AdmitDate_AD, AdmitDate_BS, LeftDate_AD, LeftDate_BS, LeftRemarks, CurrentAddress, StudentType, Caste, AgeRange, LedgerPanaNo, EMSId, IsNew, FatherOccupation, GuardianOccupation,
            // MotherOccupation, Level, Faculty, Semester, ClassYear, Batch  


           //StudentId, UserId, AutoNumber, RegNo, Name, ClassName, SectionName, RollNo, Gender, FatherName, F_ContactNo, MotherName, M_ContactNo, ContactNo, Address, PhotoPath,
           //BloodGroup, DOB_AD, DOB_BS, HouseName, Medium, BoardName, BoardRegNo, EnrollNo, CardNo, BusStop, BusPoint, GuardianName, G_Relation, G_Address, G_ContacNo, UserName,
           //ClassSection, SMSText, AdmitDate_AD, AdmitDate_BS, LeftDate_AD, LeftDate_BS, LeftRemarks, CurrentAddress, StudentType, Caste, AgeRange, LedgerPanaNo, EMSId, IsNew,
           //FatherOccupation, GuardianOccupation, MotherOccupation, Level, Faculty, Semester, ClassYear, Batch, Email 

               AcademicLib.RE.Academic.StudentSummary csl = (AcademicLib.RE.Academic.StudentSummary)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.UserId;
            row.Data[2] = csl.AutoNumber;
            row.Data[3] = csl.RegNo;
            row.Data[4] = csl.Name;
            row.Data[5] = csl.ClassName;
            row.Data[6] = csl.SectionName;
            row.Data[7] = csl.RollNo;
            row.Data[8] = csl.Gender;
            row.Data[9] = csl.FatherName;
            row.Data[10] = csl.F_ContactNo;
            row.Data[11] = csl.MotherName;
            row.Data[12] = csl.M_ContactNo;
            row.Data[13] = csl.ContactNo;                      
            row.Data[14] = csl.Address;
            row.Data[15] = csl.PhotoPath;
            row.Data[16] = csl.BloodGroup;
            row.Data[17] = csl.DOB_AD;
            row.Data[18] = csl.DOB_BS;                                    
            row.Data[19] = csl.HouseName;
            row.Data[20] = csl.Medium;                        
            row.Data[21] = csl.BoardName;
            row.Data[22] = csl.BoardRegNo;
            row.Data[23] = csl.EnrollNo;
            row.Data[24] = csl.CardNo;
            row.Data[25] = csl.BusStop;
            row.Data[26] = csl.BusPoint;
            row.Data[27] = csl.GuardianName;
            row.Data[28] = csl.G_Relation;
            row.Data[29] = csl.G_Address;                       
            
            row.Data[30] = csl.G_ContacNo;
            row.Data[31] = csl.UserName;
            row.Data[32] = csl.ClassSection;
            row.Data[33] = csl.SMSText;
            row.Data[34] = csl.AdmitDate_AD;
            row.Data[35] = csl.AdmitDate_BS;
            row.Data[36] = csl.LeftDate_AD;
            row.Data[37] = csl.LeftDate_BS;
            row.Data[38] = csl.LeftRemarks; 
            try
            {
                row.Data[39] = csl.CurrentAddress;
                row.Data[40] = csl.StudentType;
                row.Data[41] = csl.Caste;
                row.Data[42] = csl.AgeRange;
                row.Data[43] = csl.LedgerPanaNo;
                row.Data[44] = csl.EMSId;
                row.Data[45] = csl.IsNew;
                row.Data[46] = csl.FatherOccupation;
                row.Data[47] = csl.GuardianOccupation;                
                row.Data[48] = csl.MotherOccupation;
                row.Data[49] = csl.Level;
                row.Data[50] = csl.Faculty;
                row.Data[51] = csl.Semester;
                row.Data[52] = csl.ClassYear;
                row.Data[53] = csl.Batch;
                row.Data[54] = csl.Email;

                row.Data[55] = csl.F_Email;
                row.Data[56] = csl.M_Email;
                row.Data[57] = csl.G_Email;
                row.Data[58] = csl.CitizenshipNo;
                row.Data[59] = csl.MotherTongue;
                row.Data[60] = csl.Height;
                row.Data[61] = csl.Weight;
                row.Data[62] = csl.PhysicalDisability;
                row.Data[63] = csl.Aim;
                row.Data[64] = csl.BirthCertificateNo;
                row.Data[65] = csl.Remarks;
                row.Data[66] = csl.PA_Province;
                row.Data[67] = csl.PA_District;
                row.Data[68] = csl.PA_LocalLevel;
                row.Data[69] = csl.PA_Village;
                row.Data[70] = csl.Pwd;
                row.Data[71] = csl.IsUserActive;
                row.Data[72] = csl.AgeDet;
                row.Data[73] = csl.Age;

                 
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
