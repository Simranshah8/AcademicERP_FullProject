using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class Employee : AcademicLib.BL.CommonBL
    {
        DA.Academic.Transaction.EmployeeDB db = null;
        int _UserId = 0;
        public Employee(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.EmployeeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.Employee beData)
        {
            bool isModify = beData.EmployeeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        //public ResponeValues UpdateEmployee_QuickAccess(AcademicLib.BE.Academic.Transaction.Employee beData)
        //{
        //    return db.UpdateEmployee_QuickAccess(beData);
        //}
        public ResponeValues UpdateEmployee_QuickAccess(AcademicLib.BE.Academic.Transaction.Employee beData)
        {

            ResponeValues isValid = IsValidDataQA(ref beData);
            if (isValid.IsSuccess)
                return db.UpdateEmployee_QuickAccess(beData);
            else
                return isValid;

        }
        public ResponeValues IsValidDataQA(ref BE.Academic.Transaction.Employee beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else
                {
                    if (string.IsNullOrEmpty(beData.FirstName))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter First Name";
                        return resVal;
                    }

                    if (string.IsNullOrEmpty(beData.LastName))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Last Name";
                        return resVal;
                    }

                    if (beData.Gender == 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Select Gender";
                        return resVal;
                    }

                    if (string.IsNullOrEmpty(beData.PA_FullAddress))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Permanent Address";
                        return resVal;
                    }

                    if (string.IsNullOrEmpty(beData.TA_FullAddress))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Temporary Address";
                        return resVal;
                    }

                    if (!string.IsNullOrWhiteSpace(beData.EmailId))
                    {
                        var vEmail = base.IsValidEmail(beData.EmailId);
                        if (!vEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Personal Email is invalid. Please enter valid Personal Email.";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.OfficeEmailId))
                    {
                        var fEmail = base.IsValidEmail(beData.OfficeEmailId);
                        if (!fEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Office Email is invalid. Please enter valid Office Email.";
                            return resVal;
                        }
                    }


                    var vMobile = base.IsValidContactNo(beData.PersnalContactNo);
                    if (vMobile.IsSuccess == false)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please enter valid 10 digit Persnal Contact No";
                        return resVal;
                    }

                    if (!string.IsNullOrWhiteSpace(beData.OfficeContactNo))
                    {
                        var fmobile = base.IsValidContactNo(beData.OfficeContactNo);
                        if (!fmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Office Contact No";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.MotherContactNo))
                    {
                        var motmobile = base.IsValidContactNo(beData.MotherContactNo);
                        if (!motmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Mother Contact No";
                            return resVal;
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(beData.SpouseContactNo))
                    {
                        var mmobile = base.IsValidContactNo(beData.SpouseContactNo);
                        if (!mmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Spouse Contact No";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.FatherContactNo))
                    {
                        var fcmobile = base.IsValidContactNo(beData.FatherContactNo);
                        if (!fcmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Father Contact No";
                            return resVal;
                        }
                    }

                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;
        }
        public ResponeValues UpdatePhoto( string PhotoPath,int? EmployeeId)
        {
            return db.UpdatePhoto(_UserId, PhotoPath,EmployeeId);
        }
        public AcademicLib.BE.Academic.Transaction.EmployeeBankAccountCollections getAllEmpBankDetail(int EntityId)
        {
            return db.getAllEmpBankDetail(_UserId, EntityId);
        }
            public ResponeValues UpdateEmployee_Query(List<AcademicLib.BE.Academic.Transaction.UpdateEmployeeQuery> dataColl, string query)
        {
            return db.UpdateEmployee_Query(_UserId, dataColl, query);
        }
            public ResponeValues UpdateEmployeePhoto( int EmployeeId, string photoPath)
        {
            return db.UpdateEmployeePhoto(_UserId, EmployeeId, photoPath);
        }
            public ResponeValues SaveLeftEmployee(BE.Academic.Transaction.EmployeeLeft beData)
        {
            return db.SaveEmployeeLeft(beData);
        }
        public ResponeValues DeleteLeftEmp( int TranId, int EmployeeId)
        {
            return db.DeleteLeftEmp(_UserId, TranId, EmployeeId);
        }
            public ResponeValues DeleteById(int EntityId, int EmployeeId)
        {
            return db.DeleteById(_UserId, EntityId, EmployeeId);
        }
        public AcademicLib.BE.Academic.Transaction.Employee getEmployeeById(int EntityId, int EmployeeId)
        {
            return db.getEmployeeById(_UserId, EntityId, EmployeeId);
        }
        public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getAllEmpShortList()
        {
            return db.getAllEmpShortList(_UserId);
        }
        public BE.Academic.Transaction.EmployeeAutoCompleteCollections getAllEmployeeForSelection(int For)
        {
            return db.getAllEmployeeForSelection(_UserId, For);
        }
            public BE.Academic.Transaction.EmployeeAutoComplete getEmployeeByQrCode(string qrCode, bool getUserPwd = false)
        {
            return db.getEmployeeByQrCode(_UserId, qrCode,getUserPwd);
        }
            public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getEmpListForClassTeacher( int ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, int? SubjectId = null)
        {
            return db.getEmpListForClassTeacher(_UserId, ClassId, SectionId, SemesterId, ClassYearId, BatchId,SubjectId);
        }
        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeSummaryList(string DepartmentIdColl="")
        {
            return db.getEmployeeSummaryList(_UserId,DepartmentIdColl);
        }
        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeLeftSummaryList(string DepartmentIdColl = "")
        {
            return db.getEmployeeLeftSummaryList(_UserId, DepartmentIdColl);
        }
        public ResponeValues UpdateEnrollCardNo(RE.Academic.EmployeeSummaryCollections dataColl)
        {
            return db.UpdateEnrollCardNo(_UserId,dataColl);
        }
        public ResponeValues getAutoRegdNo()
        {
            return db.getAutoRegdNo(_UserId);
        }
        public AcademicLib.RE.Academic.LeftEmployeeCollections getLeftEmployeeList( )
        {
            return db.getLeftEmployeeList(_UserId);
        }
        public AcademicLib.BE.Academic.Transaction.EmployeeLeft getLeftEmployeeById(  int EmployeeId)
        {
            return db.getLeftEmployeeById(_UserId, EmployeeId);
        }
        public List<AcademicLib.BE.Academic.Transaction.ImportEmployee> getEmployeeListForEMIS()
        {
            return db.getEmployeeListForEMIS(_UserId);
        }
        public AcademicLib.RE.Academic.EmployeeIdCardCollections getEmpListForIdCard( string EmpIdColl, DateTime? validFrom, DateTime? validTo,int? DepartmentId, int? DesignationId)
        {
            return db.getEmpListForIdCard(_UserId, EmpIdColl, validFrom, validTo,DepartmentId,DesignationId);
        }
        public AcademicLib.API.Teacher.ClassTeacherCollections getClassListForClassTeacher(int? AcademicYearId,int? BatchId=null,int? SemesterId=null,int? ClassYearId=null)
        {
            return db.getClassListForClassTeacher(_UserId,AcademicYearId,BatchId,SemesterId,ClassYearId);
        }
        public ResponeValues UpdatePersonalInfo(AcademicLib.API.Teacher.PersonalInformation beData)
        {
            return db.UpdatePersonalInfo(beData);
        }
        public ResponeValues UpdateCIT(AcademicLib.API.Teacher.CITDetails beData)
        {
            return db.UpdateCIT(beData);
        }
            public ResponeValues UpdatePermanentAddress(AcademicLib.API.Teacher.Emp_PermananetAddress beData)
        {
            return db.UpdatePermanentAddress(beData);
        }
        public ResponeValues UpdateTemporaryAddress(AcademicLib.API.Teacher.Emp_TemporaryAddress beData)
        {
            return db.UpdateTemporaryAddress(beData);
        }
        public ResponeValues UpdateCitizenship(AcademicLib.API.Teacher.CitizenshipDetails beData)
        {
            return db.UpdateCitizenship(beData);
        }
        public ResponeValues UpdateDOB(List<BE.Academic.Transaction.ImportEmployeeDOB> dataColl)
        {
            return db.UpdateDOB(_UserId, dataColl);
        }
        public AcademicLib.API.Admin.Employee admin_EmployeeList(int? DepartmentId)
        {
            return db.admin_EmployeeList(_UserId,DepartmentId);
        }
        public AcademicLib.RE.Academic.EmployeeBirthDayCollections getEmpBirthDayList(DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getEmpBirthDayList(_UserId, dateFrom, dateTo);
        }
        public ResponeValues UpdateEmployeePhoto_Query( List<AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto> dataColl, string query)
        {
            return db.UpdateEmployeePhoto_Query(_UserId, dataColl, query);
        }


        public BE.Academic.Transaction.EmpAttachmentCollections GetEmpAttForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEAttForQuickAccessByyId(_UserId, EntityId, EmployeeId);
        }
        public BE.Academic.Transaction.EmpComplainCollections GetEmpComplainForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEmpComplainForQuickAccessByyId(_UserId, EntityId, EmployeeId);
        }

        public BE.Academic.Transaction.EmpLeaveTakenCollections GetEmpLeaveTakenForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEmpLeaveTakenForQuickAccessByyId(_UserId, EntityId, EmployeeId);

        }

        //Added By Suresh on 16 poush fo eEmployee Update Starts
        public List<AcademicLib.BE.Academic.Transaction.Employee> getEmployeeListForupdate(int? DepartmentId, int? DesignationId, int? CategoryId)
        {          
            return db.getEmployeeForUpdate(_UserId, DepartmentId, DesignationId, CategoryId);
        }

        public ResponeValues UpdateEmployee(List<BE.Academic.Transaction.Employee> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            int row = 1;
            foreach (var dc in dataColl)
            {
                if (string.IsNullOrEmpty(dc.EmployeeCode))
                {
                    resVal.ResponseMSG = "Please ! Enter Employee Code. at row " + row.ToString();
                    return resVal;
                }

                if (string.IsNullOrEmpty(dc.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter First Name at row " + row.ToString();
                    return resVal;
                }

                row++;
            }

            return db.UpdateEmployee(_UserId, dataColl);
        }
        //Ends
        public ResponeValues IsValidData(ref BE.Academic.Transaction.Employee beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.EmployeeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.EmployeeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.EmployeeCode))
                {
                    resVal.ResponseMSG = "Please ! Enter Employee Code";
                }
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter  First Name";
                }
                //else if (string.IsNullOrEmpty(beData.LastName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter  Last Name";
                //}
                else if (beData.Gender == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Gender";
                }
                else if (!beData.DepartmentId.HasValue || beData.DepartmentId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Employee Department";
                }
                else if (!beData.DesignationId.HasValue || beData.DesignationId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Employee Designation";
                }
                else if (string.IsNullOrEmpty(beData.PersnalContactNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Personal Contact No.";
                }
                //else if (string.IsNullOrEmpty(beData.FatherName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Father Name";
                //}
                else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Permanent Address";
                }
                else if (beData.IsPhysicalDisability && string.IsNullOrEmpty(beData.PhysicalDisability))
                {
                    resVal.ResponseMSG = "Please ! Enter PhysicalDisability Details";
                }
                else
                {

                    if (beData.TaxRuleAs == 0)
                        beData.TaxRuleAs = 1;

                    if (beData.DepartmentId.HasValue && beData.DepartmentId.Value == 0)
                        beData.DepartmentId = null;

                    if (beData.DesignationId.HasValue && beData.DesignationId.Value == 0)
                        beData.DesignationId = null;

                    if (beData.LevelId.HasValue && beData.LevelId.Value == 0)
                        beData.LevelId = null;

                    if (beData.ServiceTypeId.HasValue && beData.ServiceTypeId.Value == 0)
                        beData.ServiceTypeId = null;

                    if (beData.CategoryId.HasValue && beData.CategoryId.Value == 0)
                        beData.CategoryId = null;

                    if (beData.SubjectTeacherId.HasValue && beData.SubjectTeacherId == 0)
                        beData.SubjectTeacherId = null;

                    if (beData.BankList != null && beData.BankList.Count > 0)
                    {
                        var tmpBankList = beData.BankList;
                        beData.BankList = new List<BE.Academic.Transaction.EmployeeBankAccount>();
                        foreach (var ba in tmpBankList)
                        {
                            if (!string.IsNullOrEmpty(ba.BankName) && !string.IsNullOrEmpty(ba.AccountName) && !string.IsNullOrEmpty(ba.AccountNo) && !string.IsNullOrEmpty(ba.Branch))
                            {
                                beData.BankList.Add(ba);
                            }
                        }
                    }

                    //Added phone No validation
                    var permobile = base.IsValidContactNo(beData.PersnalContactNo);
                    if (permobile.IsSuccess == false)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please Enter Valid Personal Contact No";
                        return resVal;
                    }
                    //Ends
                    //Starts

                    var vMobile = base.IsValidContactNo(beData.PersnalContactNo);
                    if (vMobile.IsSuccess == false)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please Enter valid 10 digit Personal Contact No.";
                        return resVal;
                    }
                    if (!string.IsNullOrWhiteSpace(beData.OfficeContactNo))
                    {
                        var mmobile = base.IsValidContactNo(beData.OfficeContactNo);
                        if (!mmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid 10 digit Office Contact No";
                            return resVal;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(beData.FatherContactNo))
                    {
                        var fmobile = base.IsValidContactNo(beData.FatherContactNo);
                        if (!fmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid 10 digit Father Contact No";
                            return resVal;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(beData.MotherContactNo))
                    {
                        var fmmobile = base.IsValidContactNo(beData.MotherContactNo);
                        if (!fmmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid 10 digit Mother Contact No";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.EC_Phone))
                    {
                        var emobile = base.IsValidContactNo(beData.EC_Phone);
                        if (!emobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid Emergency Phone";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.EC_Mobile))
                    {
                        var emmobile = base.IsValidContactNo(beData.EC_Mobile);
                        if (!emmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid Emergency Mobile";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.SpouseContactNo))
                    {
                        var spmobile = base.IsValidContactNo(beData.SpouseContactNo);
                        if (!spmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter valid 10 digit Spouse Contact No";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.EmailId))
                    {
                        var pfEmail = base.IsValidEmail(beData.EmailId);
                        if (!pfEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Email is Invalid. Please enter valid Personal Email.";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.OfficeEmailId))
                    {
                        var pofEmail = base.IsValidEmail(beData.OfficeEmailId);
                        if (!pofEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Email is Invalid. Please enter valid Office Email.";
                            return resVal;
                        }
                    }


                    //Ends


                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }
        public BE.Academic.Transaction.EmployeeAutoCompleteCollections getAllEmployeeAutoComplete(string searchBy, string Operator, string searchValue, bool? showLeft = false)
        {
            return db.getAllEmployeeAutoComplete(_UserId, searchBy, Operator, searchValue,showLeft);
        }
        public AcademicLib.API.Teacher.EmployeeProfile getEmployeeForApp(int? EmployeeId)
        {
            return db.getEmployeeForApp(_UserId, EmployeeId);
        }
        public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getEmployeeUserList()
        {
            return db.getEmployeeUserList(_UserId);
        }
        public ResponeValues GenerateUser(int AsPer,bool CanUpdateUserName, string Prefix, string Suffix)
        {
            return db.GenerateUser(_UserId, AsPer,CanUpdateUserName,Prefix,Suffix);
        }
        public ResponeValues StartOnlineClass(API.Teacher.OnlineClass beData)
        {
            return db.StartOnlineClass(beData);
        }
        public AcademicLib.RE.Academic.RunningClassCollections getRunningClassList(int? tranId)
        {
            return db.getRunningClassList(_UserId,tranId);
        }
        public AcademicLib.RE.Academic.RunningClassCollections getColleaguesRunningClassList()
        {
            return db.getColleaguesRunningClassList(_UserId);
        }
            public AcademicLib.RE.Academic.PassedOnlineClassCollections getPassedClassList( DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getPassedClassList(_UserId, dateFrom, dateTo);
        }
        public AcademicLib.RE.Academic.OnlineClasssAttendanceCollections getOnlineClassAttendanceById(int TranId)
        {
            return db.getOnlineClassAttendanceById(_UserId, TranId);
        }
        public ResponeValues EndOnlineClass(int TranId)
        {
            return db.EndOnlineClass(_UserId, TranId);
        }
        public ResponeValues JoinOnlineClass(int TranId)
        {
            return db.JoinOnlineClass(_UserId, TranId);
        }
        public ResponeValues ImportBankAccount( List<BE.Academic.Transaction.EmployeeBankAccount> dataColl)
        {
            return db.ImportBankAccount(_UserId, dataColl);
        }
        public AcademicLib.RE.Academic.EmployeePhotoCollections getEmployeePhoto(int? DepartmentId)
        {
            return db.getEmployeePhoto(_UserId, DepartmentId);
        }

        public AcademicLib.RE.Academic.EmployeePhotoCollections GetAllEmployeePhotoDownloadLog(int EntityId)
        {
            return db.getAllEmployeePhotoDownloadLog(_UserId, EntityId);
        }
    }
}
