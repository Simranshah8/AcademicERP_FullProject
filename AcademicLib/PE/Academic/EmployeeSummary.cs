using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Academic
{
    public class EmployeeSummary : Dynamic.Accounting.IReportLoadObjectData
    {

        public EmployeeSummary(List<AcademicLib.RE.Academic.EmployeeSummary> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Academic.EmployeeSummary> dataColl = null;
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
       
            // EmployeeId, AutoNumber, EmployeeCode, EnrollNumber, Name, Address, ContactNo, Department, Designation, Category, Gender, DOB_AD, DOB_BS, PhotoPath, TA_FullAddress, PA_FullAddress,
            // TA_District, PA_District, FatherName, MotherName, SpouseName, CardNo, UserId, UserName, SMSText, Caste, LeftDate, LeftMiti, LeftRemarks, Nationality, OfficeContactNo, PersonalContactNo,
            // CitizenShipno, Email, BloodGroup, Level  

            try
            {
                AcademicLib.RE.Academic.EmployeeSummary csl = (AcademicLib.RE.Academic.EmployeeSummary)obj;
                row.Data[0] = csl.EmployeeId;
                row.Data[1] = csl.AutoNumber;
                row.Data[2] = csl.EmployeeCode;
                row.Data[3] = csl.EnrollNumber;
                row.Data[4] = csl.Name;
                row.Data[5] = csl.Address;
                row.Data[6] = csl.ContactNo;
                row.Data[7] = csl.Department;
                row.Data[8] = csl.Designation;
                row.Data[9] = csl.Category;
                row.Data[10] = csl.Gender;
                row.Data[11] = csl.DOB_AD;
                row.Data[12] = csl.DOB_BS;
                row.Data[13] = csl.PhotoPath;
                row.Data[14] = csl.TA_FullAddress;
                row.Data[15] = csl.PA_FullAddress;
                row.Data[16] = csl.TA_District;
                row.Data[17] = csl.PA_District;
                row.Data[18] = csl.FatherName;
                row.Data[19] = csl.MotherName;
                row.Data[20] = csl.SpouseName;
                row.Data[21] = csl.CardNo;
                row.Data[22] = csl.UserId;
                row.Data[23] = csl.UserName;
                row.Data[24] = csl.SMSText;
                row.Data[25] = csl.Caste;
                row.Data[26] = csl.LeftDate;
                row.Data[27] = csl.LeftMiti;
                row.Data[28] = csl.LeftRemarks;
                row.Data[29] = csl.Nationality;
                row.Data[30] = csl.OfficeContactNo;
                row.Data[31] = csl.PersonalContactNo;
                row.Data[32] = csl.CitizenShipno;
                row.Data[33] = csl.Email;
                row.Data[34] = csl.BloodGroup;
                row.Data[35] = csl.Level;

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
