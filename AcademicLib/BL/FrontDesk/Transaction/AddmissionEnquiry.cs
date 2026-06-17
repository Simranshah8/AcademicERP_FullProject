using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class AddmissionEnquiry : CommonBL
    {
        DA.FrontDesk.Transaction.AddmissionEnquiryDB db = null;
        int _UserId = 0;

        public AddmissionEnquiry(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.AddmissionEnquiryDB(hostName, dbName);
        }

        public ResponeValues getAutoNo()
        {
            return db.getAutoNo(_UserId);
        }
            public ResponeValues SaveFormData(int AcademicYearId, BE.FrontDesk.Transaction.AddmissionEnquiry beData)
        {
            beData.Photo = null;
            bool isModify = false;
            if (beData.EnquiryId.HasValue && beData.EnquiryId > 0)
                isModify = true;

            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public RE.FrontDesk.EnqSummaryCollections getEnqSummary( DateTime? dateFrom, DateTime? dateTo,int? EnquiryId=null)
        {
            return db.getEnqSummary(_UserId, dateFrom, dateTo,EnquiryId);
        }
            public BE.FrontDesk.Transaction.AddmissionEnquiryCollections GetAllAddmissionEnquiry(int EntityId)
        {
            return db.getAllAddmissionEnquiry(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.AddmissionEnquiry GetAddmissionEnquiryById(int EntityId, int TranId)
        {
            return db.getAddmissionEnquiryById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public RE.FrontDesk.EnqFollowupCollections getEnqForFollowup( int? AcademicYearId, int FollowupType)
        {
            return db.getEnqForFollowup(_UserId, AcademicYearId, FollowupType);
        }
        public RE.FrontDesk.StudentPaymentFollowupCollections getFollowupList( int TranId, int? AcademicYearId)
        {
            return db.getFollowupList(_UserId, TranId, AcademicYearId);
        }

        public RE.FrontDesk.EnqFollowupCollections getEnqForCounCelling(int? AcademicYearId)
        {
            return db.getEnqForCounCelling(_UserId, AcademicYearId);
        }

        public AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections GetEmpCouncellingStatuses(  int AcademicYearId, int? EmpId)
        {
            return db.GetEmpCouncellingStatuses(_UserId, AcademicYearId, EmpId);
        }
       public ResponeValues SaveFollowup(BE.FrontDesk.Transaction.StudentPaymentFollowup beData)
        {
            ResponeValues resVal = new ResponeValues();

            if (string.IsNullOrEmpty(beData.Remarks))
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Please !  Enter Followup Remarks";
                return resVal;
            }

            if (beData.NextFollowupDate.HasValue)
            {
                DateTime nextDate = beData.NextFollowupDate.Value;

                if (nextDate < DateTime.Today)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Next Followup Date";
                    return resVal;                        
                }

                if (beData.NextFollowupTime.HasValue)
                {
                    var tm = beData.NextFollowupTime.Value;
                    beData.NextFollowupDateTime = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, tm.Hour, tm.Minute, tm.Second);
                }
                else
                    beData.NextFollowupDateTime = nextDate;

            }

            return db.SaveFollowup(beData);
        }
        public ResponeValues SaveClosed(int RefTranId, string Remarks)
        {
            return db.SaveClosed(_UserId, RefTranId, Remarks);
        }
        public ResponeValues SaveAssignCounselor( int TranId, DateTime? AssignDate, List<int> EmployeeIdColl)
        {
            return db.SaveAssignCounselor(_UserId, TranId, AssignDate, EmployeeIdColl);
        }
        public ResponeValues SaveEnqStatus(int TranId, int Status, string Remarks)
        {
            return db.SaveEnqStatus(_UserId, TranId, Status, Remarks);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.AddmissionEnquiry beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.EnquiryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.EnquiryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter First Name";
                }
                //else if (string.IsNullOrEmpty(beData.LastName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Last Name";
                //}
                //else if ((!beData.ClassId.HasValue || beData.ClassId.Value == 0) && string.IsNullOrEmpty(beData.Department))
                else if ((!beData.ClassId.HasValue || beData.ClassId.Value == 0))
                {
                    resVal.ResponseMSG = "Please ! Select Class Name";
                }
                else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Permanent Full Address";
                }
                //else if (string.IsNullOrEmpty(beData.FatherName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Father Name";
                //}
                //else if (string.IsNullOrEmpty(beData.F_ContactNo))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Father Contact No.";
                //}
                else if (string.IsNullOrEmpty(beData.ContactNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Contact No.";
                }
                else
                {
                    if (string.IsNullOrEmpty(beData.LastName))
                        beData.LastName = "";

                    var validName = IsValidName(beData.FirstName);
                    if (!validName.IsSuccess)
                        return validName;

                    if (!string.IsNullOrEmpty(beData.ContactNo))
                    {
                        var validContactNo = IsValidContactNo(beData.ContactNo);
                        if (!validContactNo.IsSuccess)
                            return validContactNo;
                    }

                    if (!string.IsNullOrEmpty(beData.Email))
                    {
                        var validEMail = IsValidEmail(beData.Email);
                        if (!validEMail.IsSuccess)
                            return validEMail;
                    }

                    if (!string.IsNullOrEmpty(beData.F_Email))
                    {
                        var validEMail = IsValidEmail(beData.F_Email);
                        if (!validEMail.IsSuccess)
                            return validEMail;
                    }

                    if (beData.DOB.HasValue)
                    {
                        var validDOB = DateTime.Today.AddYears(-2);
                        if (beData.DOB.Value > validDOB)
                        {
                            resVal.ResponseMSG = "Please ! Enter Valid DOB";
                            return resVal;
                        }
                    }

                    if (!beData.AnyDisease)
                    {
                        beData.Problem = "";
                        beData.PresentCondition = "";
                    }
                    else if (beData.AnyDisease)
                    {
                        if (string.IsNullOrEmpty(beData.Problem))
                        {
                            resVal.ResponseMSG = "Enter Disease Problem";
                            return resVal;
                        }
                        else if (string.IsNullOrEmpty(beData.PresentCondition))
                        {
                            resVal.ResponseMSG = "Please Select Present Status of Disease Problem";
                            return resVal;
                        }
                    }

                    if (string.IsNullOrEmpty(beData.MiddleName))
                        beData.MiddleName = "";

                    if (string.IsNullOrEmpty(beData.LastName))
                        beData.LastName = "";

                    if (string.IsNullOrEmpty(beData.ClassName))
                        beData.ClassName = "";

                    if (beData.FollowupDate.HasValue && beData.FollowUpTime.HasValue)
                    {
                        var nd = beData.FollowupDate.Value;
                        beData.FollowUpTime = new DateTime(nd.Year, nd.Month, nd.Day, beData.FollowUpTime.Value.Hour, beData.FollowUpTime.Value.Minute, 0);

                    }

                    if (beData.FormSale && (beData.FeeItemColl == null || beData.FeeItemColl.Count == 0))
                    {
                        beData.IsSuccess = false;
                        beData.ResponseMSG = "Please ! 1st setup fee structure.";
                        return beData;
                    }

                    if (beData.IsFollowupRequired)
                    {
                        if (!beData.FollowupDate.HasValue)
                        {
                            beData.ResponseMSG = "Please ! Enter Followup Date";
                            beData.IsSuccess = false;
                            return beData;
                        }

                        if (!beData.CommunicationTypeId.HasValue || beData.CommunicationTypeId == 0)
                        {

                            beData.ResponseMSG = "Please ! Select Communication Type";
                            beData.IsSuccess = false;
                            return beData;
                        }
                    }
                    else
                    {
                        beData.FollowupDate = null;
                        beData.CommunicationTypeId = null;
                    }
                    //Added new Validation

                    if (!string.IsNullOrWhiteSpace(beData.M_ContactNo))
                    {
                        var mmobile = base.IsValidContactNo(beData.M_ContactNo);
                        if (!mmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Mother's Contact No";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.G_Contact))
                    {
                        var gmobile = base.IsValidContactNo(beData.G_Contact);
                        if (!gmobile.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Guardian's Contact No";
                            return resVal;
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(beData.M_Email))
                    {
                        var mEmail = base.IsValidEmail(beData.M_Email);
                        if (!mEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Mother's Email is invalid. Please enter valid Mother's Email.";
                            return resVal;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(beData.G_Email))
                    {
                        var grEmail = base.IsValidEmail(beData.G_Email);
                        if (!grEmail.IsSuccess)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Guardian's Email is invalid. Please enter valid Guardian's Email.";
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
    }
}
