using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class Registration
    {
        DA.FrontDesk.Transaction.RegistrationDB db = null;
        int _UserId = 0;
        public Registration(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.RegistrationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.Student beData)
        {
            bool isModify = beData.StudentId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SaveUpdateEligible(BE.Academic.Transaction.RegistrationEligibility beData)
        {
            ResponeValues resVal = new ResponeValues();
            if(beData.StudentId==0)
            {
                resVal.ResponseMSG = "Please ! Select Valid Student";
            }
            else if (!beData.ExamDate.HasValue)
            {
                resVal.ResponseMSG = "Enter Exam Date";
            }
            else if (!beData.ExamTypeId.HasValue || beData.ExamTypeId.Value==0)
            {
                resVal.ResponseMSG = "Please ! Select Exam Type Name";
            }
            else if (string.IsNullOrEmpty(beData.ExaminarName))
            {
                resVal.ResponseMSG = "Enter Examinar Name";
            }
            else if (string.IsNullOrEmpty(beData.Result))
            {
                resVal.ResponseMSG = "Enter Result";
            }
            else if (string.IsNullOrEmpty(beData.ApprovalBy))
            {
                resVal.ResponseMSG = "Enter Approved By";
            }
            else if (!beData.AppliedClassId.HasValue || beData.AppliedClassId.Value == 0)
            {
                resVal.ResponseMSG = "Select Applied Class Name";
            }
            else if (!beData.ClassPreferredForId.HasValue || beData.ClassPreferredForId.Value == 0)
            {
                resVal.ResponseMSG = "select Preferred For Class";
            }
            else if (!beData.SubjectId.HasValue || beData.SubjectId.Value == 0)
            {
                resVal.ResponseMSG = "Please ! Select Subject Name";
            }
            else if (beData.FullMark==0)
            {
                resVal.ResponseMSG = "Enter Full Mark";
            }
            else if (beData.PassMark == 0)
            {
                resVal.ResponseMSG = "Enter Pass Mark";
            }
            else if ( (beData.ExaminationMode==2) && (beData.AttachmentColl == null || beData.AttachmentColl.Count == 0))
            {
                resVal.ResponseMSG = "Please ! Attach Required Documents";
            }
            else if (beData.ExaminationMode == 0)
            {
                resVal.ResponseMSG = "Please ! Select Mode Of Exam";
            }
            else
                resVal= db.SaveUpdateEligible(beData);

            return resVal;
        }
            public BE.Academic.Transaction.Student GetStudentById(int EntityId, int StudentId)
        {
            return db.getStudentById(_UserId, EntityId, StudentId);
        }
        public ResponeValues DeleteById(int EntityId, int AcademicYearId)
        {
            return db.DeleteById(_UserId, EntityId, AcademicYearId);
        }
                
        public ResponeValues getAutoRegdNo()
        {
            return db.getAutoRegdNo(_UserId);
        }
        public RE.FrontDesk.EnqSummaryCollections getRegSummary( DateTime dateFrom, DateTime dateTo)
        {
            return db.getRegSummary(_UserId, dateFrom, dateTo);
        }
        public RE.FrontDesk.EnqSummaryCollections getRegSummaryForEligible( DateTime dateFrom, DateTime dateTo)
        {
            return db.getRegSummaryForEligible(_UserId, dateFrom, dateTo);
        }
        public RE.FrontDesk.EnqSummaryCollections getRegSummaryForAdmitConfirm()
        {
            return db.getRegSummaryForAdmitConfirm(_UserId);
        }
        public RE.FrontDesk.EnqFollowupCollections getRegForFollowup(int? AcademicYearId, int FollowupType)
        {
            return db.getRegForFollowup(_UserId, AcademicYearId, FollowupType);
        }
        public RE.FrontDesk.EnqFollowupCollections getAdmissionForFollowup(int? AcademicYearId, int FollowupType)
        {
            return db.getAdmissionForFollowup(_UserId, AcademicYearId, FollowupType);
        }
        public RE.FrontDesk.EnqSummaryCollections getRegAdmitStudent(int? AcademicYearId)
        {
            return db.getRegAdmitStudent(_UserId,AcademicYearId);
        }
            public ResponeValues SaveAssignCounselor(int TranId, DateTime? AssignDate, List<int> EmployeeIdColl)
        {
            return db.SaveAssignCounselor(_UserId, TranId,AssignDate, EmployeeIdColl);
        }

        public AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections GetEmpCouncellingStatuses( int AcademicYearId, int? EmpId)
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
        public ResponeValues SaveAdmitFollowup(BE.FrontDesk.Transaction.StudentPaymentFollowup beData)
        {
            if (beData.NextFollowupDate.HasValue)
            {
                DateTime nextDate = beData.NextFollowupDate.Value;
                if (beData.NextFollowupTime.HasValue)
                {
                    var tm = beData.NextFollowupTime.Value;
                    beData.NextFollowupDateTime = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, tm.Hour, tm.Minute, tm.Second);
                }
                else
                    beData.NextFollowupDateTime = nextDate;

            }

            return db.SaveAdmitFollowup(beData);
        }
        public ResponeValues SaveEnqStatus( int TranId, int Status, string Remarks)
        {
            return db.SaveEnqStatus(_UserId, TranId, Status, Remarks);
        }
        public ResponeValues SaveAdmitStatus(int TranId, int Status, string Remarks)
        {
            return db.SaveAdmitStatus(_UserId, TranId, Status, Remarks);
        }
        public RE.FrontDesk.StudentPaymentFollowupCollections getFollowupList( int TranId, int? AcademicYearId)
        {
            return db.getFollowupList(_UserId, TranId, AcademicYearId);
        }
        public RE.FrontDesk.StudentPaymentFollowupCollections getAdmitFollowupList(int TranId, int? AcademicYearId)
        {
            return db.getAdmitFollowupList(_UserId, TranId, AcademicYearId);
        }
        public ResponeValues GenerateUser(int AcademicYearId, int AsPer, bool CanUpdateUserName, string Prefix, string Suffix)
        {
            return db.GenerateUser(_UserId, AcademicYearId, AsPer, CanUpdateUserName, Prefix, Suffix);
        }
        public AcademicLib.BE.Academic.Transaction.StudentUserCollections getStudentUserList(int AcademicYearId, int? ClassId)
        {
            return db.getStudentUserList(_UserId, AcademicYearId, ClassId);
        }

        public ResponeValues SaveEligibleFeeReceipt(int AcademicYearId, BE.Academic.Transaction.RegistrationEligibility beData)
        {
            ResponeValues resVal = new ResponeValues();

            if(beData.StudentId==0)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Please ! Select Valid Student";
            }
            else if (beData.TranId == 0)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "1st Need To Approved";
            }else if(beData.FeeItemColl==null || beData.FeeItemColl.Count==0)
            {
                resVal.ResponseMSG = "No Data Found For Receipt";
            }
            else if (beData.PaymentModeColl == null || beData.PaymentModeColl.Count==0)
            {
                resVal.ResponseMSG = "Please ! Select Payment Mode";
            }
            else if(Math.Round(beData.PaymentModeColl.Sum(p1=>p1.Amount),2)!=Math.Round(beData.FeeItemColl.Sum(p1=>p1.PaidAmt),2))
            {
                resVal.ResponseMSG = "Please ! Select Payment Mode Amount Does Not Match With Paid Amount";
            }
            else
                resVal= db.SaveEligibleFeeReceipt(AcademicYearId, beData);

            return resVal;
        }
            public ResponeValues IsValidData(ref BE.Academic.Transaction.Student beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.StudentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.StudentId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.RegNo))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Regd.No.";
                //}
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter Student First Name";
                }
                //else if (string.IsNullOrEmpty(beData.LastName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Student Last Name";
                //}
                else if (!beData.ClassId.HasValue || beData.ClassId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }
                else if (!beData.AcademicYear.HasValue || beData.AcademicYear.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Academic Year";
                }
                else if (string.IsNullOrEmpty(beData.FatherName))
                {
                    resVal.ResponseMSG = "Please ! Enter Father Name";
                }
                else if (string.IsNullOrEmpty(beData.ContactNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Contact No.";
                }
                else if (string.IsNullOrEmpty(beData.RegNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Reg. No";
                }

                //else if (!beData.CasteId.HasValue || beData.CasteId.Value == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Valid Category Name";
                //}
                //else if (!beData.StudentTypeId.HasValue || beData.StudentTypeId.Value == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Valid Student Type Name";
                //}
                //else if (!beData.SectionId.HasValue || beData.SectionId.Value == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Valid Section Name";
                //}
                //else if (string.IsNullOrEmpty(beData.M_Contact))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Mother Contact No.";
                //}
                //else if (string.IsNullOrEmpty(beData.M_Contact))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Mother Contact No.";
                //}
                else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Permanent Full Address";
                }
                //else if (string.IsNullOrEmpty(beData.PA_District))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Permanent District";
                //}
                //else if (string.IsNullOrEmpty(beData.PA_LocalLevel))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Permanent City/Town";
                //}
                //else if (beData.PA_WardNo==0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Permanent Pin Code";
                //}

                //else if (string.IsNullOrEmpty(beData.CA_FullAddress))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Temporary Full Address";
                //}
                //else if (string.IsNullOrEmpty(beData.CA_District))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Temporary District";
                //}
                //else if (string.IsNullOrEmpty(beData.CA_LocalLevel))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Temporary City/Town";
                //}
                //else if (beData.CA_WardNo == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Temporary Pin Code";
                //}
 
                //else if (string.IsNullOrEmpty(beData.F_ContactNo))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Father Contact No.";
                //}
                //else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Permanent Address";
                //}
                else
                {
                    if (string.IsNullOrEmpty(beData.LastName))
                        beData.LastName = "";

                    if (beData.SemesterId.HasValue && beData.SemesterId.Value == 0)
                        beData.SemesterId = null;

                    if (beData.ClassYearId.HasValue && beData.ClassYearId.Value == 0)
                        beData.ClassYearId = null;

                    if (beData.BatchId.HasValue && beData.BatchId.Value == 0)
                        beData.BatchId = null;


                    if (beData.CasteId.HasValue && beData.CasteId.Value == 0)
                        beData.CasteId = null;

                    if (beData.AcademicYear.HasValue && beData.AcademicYear.Value == 0)
                        beData.AcademicYear = null;

                    if (beData.ClassId.HasValue && beData.ClassId.Value == 0)
                        beData.ClassId = null;

                    if (beData.SectionId.HasValue && beData.SectionId.Value == 0)
                        beData.SectionId = null;

                    if (beData.HouseNameId.HasValue && beData.HouseNameId.Value == 0)
                        beData.HouseNameId = null;

                    if (beData.StudentTypeId.HasValue && beData.StudentTypeId.Value == 0)
                        beData.StudentTypeId = null;

                    if (beData.BoardersTypeId.HasValue && beData.BoardersTypeId.Value == 0)
                        beData.BoardersTypeId = null;

                    if (beData.TransportPointId.HasValue && beData.TransportPointId.Value == 0)
                        beData.TransportPointId = null;

                    if (beData.MediumId.HasValue && beData.MediumId.Value == 0)
                        beData.MediumId = null;

                    if (beData.BoardersTypeId.HasValue && beData.BoardersTypeId.Value == 0)
                        beData.BoardersTypeId = null;

                    if (beData.BoardId.HasValue && beData.BoardId.Value == 0)
                        beData.BoardId = null;

                    if (beData.ClassId_First.HasValue && beData.ClassId_First.Value == 0)
                        beData.ClassId_First = null;

                    if (beData.FamilyType == 0)
                        beData.FamilyType = 1;

                    if (beData.AcademicDetailsColl != null && beData.AcademicDetailsColl.Count > 0)
                    {
                        var tmpAcademicColl = beData.AcademicDetailsColl;
                        beData.AcademicDetailsColl = new List<BE.Academic.Transaction.StudentPreviousAcademicDetails>();
                        foreach (var aDet in tmpAcademicColl)
                        {
                            if (!string.IsNullOrEmpty(aDet.ClassName) && !string.IsNullOrEmpty(aDet.SchoolColledge))
                            {
                                beData.AcademicDetailsColl.Add(aDet);
                            }
                        }
                    }
                     
                    if (beData.FollowupDate.HasValue && beData.FollowUpTime.HasValue)
                    {
                        var nd = beData.FollowupDate.Value;
                        beData.FollowUpTime = new DateTime(nd.Year, nd.Month, nd.Day, beData.FollowUpTime.Value.Hour, beData.FollowUpTime.Value.Minute, 0);
                        beData.FollowupDateTime = beData.FollowUpTime;
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

                    if (beData.DOB_AD.HasValue)
                    {
                        var validDOB = DateTime.Today.AddYears(-2);
                        if (beData.DOB_AD.Value > validDOB)
                        {
                            resVal.ResponseMSG = "Please ! Enter Valid DOB";
                            return resVal;
                        }
                    }

                    if (!beData.AdmitDate.HasValue)
                        beData.AdmitDate = DateTime.Today;

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
