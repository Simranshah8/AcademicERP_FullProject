using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class EmployeeCandidate
    {
        DA.FrontDesk.Transaction.EmployeeCandidateDB db = null;
        int _UserId = 0;
        public EmployeeCandidate(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.FrontDesk.Transaction.EmployeeCandidateDB(hostName, dbName);
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
      
        public ResponeValues DeleteLeftEmp(int TranId, int EmployeeId)
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
      
        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeSummaryList(string DepartmentIdColl = "")
        {
            return db.getEmployeeSummaryList(_UserId, DepartmentIdColl);
        }
       
        public ResponeValues getAutoRegdNo()
        {
            return db.getAutoRegdNo(_UserId);
        }
        
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
                else if (string.IsNullOrEmpty(beData.FatherName))
                {
                    resVal.ResponseMSG = "Please ! Enter Father Name";
                }
                else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Permanent Address";
                }
                else if (beData.IsPhysicalDisability && string.IsNullOrEmpty(beData.PhysicalDisability))
                {
                    resVal.ResponseMSG = "Please ! Enter PhysicalDisability Details";
                }
                else if(!beData.SourceId.HasValue || beData.SourceId == 0)
                {
                    resVal.ResponseMSG = "Please ! Choose Source";
                }

                else
                {

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
       
    }
}
