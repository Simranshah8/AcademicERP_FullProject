using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class Student : AcademicLib.BL.CommonBL
    {
        DA.Academic.Transaction.StudentDB db = null;
        int _UserId = 0;
        public Student(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.StudentDB(hostName, dbName);
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
        //public ResponeValues UpdateStudent_QuickAccess(AcademicLib.BE.Academic.Transaction.Student beData)
        //{
        //    return db.UpdateStudent_QuickAccess(beData);
        //}

        //Added
        public ResponeValues UpdateStudent_QuickAccess(AcademicLib.BE.Academic.Transaction.Student beData)
        {

            ResponeValues isValid = IsValidDataQA(ref beData);
            if (isValid.IsSuccess)
                return db.UpdateStudent_QuickAccess(beData);
            else
                return isValid;

        }
        public ResponeValues IsValidDataQA(ref BE.Academic.Transaction.Student beData)
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

                    if (string.IsNullOrEmpty(beData.FatherName))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Father's Name";
                        return resVal;
                    }
                     
                    if (beData.IsImport == false)
                    {
                        if (!string.IsNullOrWhiteSpace(beData.Email))
                        {
                            var vEmail = base.IsValidEmail(beData.Email);
                            if (!vEmail.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Email is invalid. Please enter valid Email.";
                                return resVal;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(beData.F_Email))
                        {
                            var fEmail = base.IsValidEmail(beData.F_Email);
                            if (!fEmail.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Father's Email is invalid. Please enter valid Father's Email.";
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

                        var vMobile = base.IsValidContactNo(beData.ContactNo);
                        if (vMobile.IsSuccess == false)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Phone No";
                            return resVal;
                        }

                        var fmobile = base.IsValidContactNo(beData.F_ContactNo);
                        if (fmobile.IsSuccess == false)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter valid 10 digit Father's Phone No";
                            return resVal;
                        }

                        if (!string.IsNullOrWhiteSpace(beData.M_Contact))
                        {
                            var mmobile = base.IsValidContactNo(beData.M_Contact);
                            if (!mmobile.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter valid 10 digit Mother's Phone No";
                                return resVal;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(beData.G_ContactNo))
                        {
                            var gmobile = base.IsValidContactNo(beData.G_ContactNo);
                            if (!gmobile.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter valid 10 digit Guardian's Contact";
                                return resVal;
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
        //Ends

        //public ResponeValues UpdateStudent_QuickAccess(AcademicLib.BE.Academic.Transaction.Student beData)
        //{
        //    return db.UpdateStudent_QuickAccess(beData);
        //}
        public ResponeValues UpdateClassWiseStudent(List<BE.Academic.Transaction.Student> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            int row = 1;
            foreach (var dc in dataColl)
            {
                string name = string.IsNullOrWhiteSpace(dc.FirstName)
                    ? "N/A"
                    : (dc.FirstName + " " + dc.MiddleName + " " + dc.LastName).Trim();

                string admNo = string.IsNullOrWhiteSpace(dc.RegNo) ? "N/A" : dc.RegNo;

                string info = $"Row {row} | Name: {name} | Adm.No: {admNo}";

                if (string.IsNullOrEmpty(dc.RegNo))
                {
                    resVal.ResponseMSG = $"Please enter Reg. No. ({info})";
                    return resVal;
                }

                if (string.IsNullOrEmpty(dc.FirstName))
                {
                    resVal.ResponseMSG = $"Please enter First Name ({info})";
                    return resVal;
                }

                if (string.IsNullOrEmpty(dc.FatherName))
                {
                    resVal.ResponseMSG = $"Please enter Father Name ({info})";
                    return resVal;
                }

                var vMobile = base.IsValidContactNo(dc.F_ContactNo);
                if (!vMobile.IsSuccess)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = $"Invalid Father Contact No ({info})";
                    return resVal;
                }

                if (!string.IsNullOrWhiteSpace(dc.M_Contact))
                {
                    var mMobile = base.IsValidContactNo(dc.M_Contact);
                    if (!mMobile.IsSuccess)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = $"Invalid Mother Contact No ({info})";
                        return resVal;
                    }
                }

                row++;
            }
            //foreach(var dc in dataColl)
            //{
            //    if (string.IsNullOrEmpty(dc.RegNo))
            //    {
            //        resVal.ResponseMSG = "Please ! Enter Reg. No. at row " + row.ToString();
            //        return resVal;
            //    }

            //    if (string.IsNullOrEmpty(dc.FirstName))
            //    {
            //        resVal.ResponseMSG = "Please ! Enter First Name at row "+row.ToString();
            //        return resVal;
            //    }
            //    //Validation on Magh 5
            //    if (!string.IsNullOrWhiteSpace(dc.F_ContactNo))
            //    {
            //        var vMobile = base.IsValidContactNo(dc.F_ContactNo);
            //        if (!vMobile.IsSuccess)
            //        {
            //            resVal.IsSuccess = false;
            //            resVal.ResponseMSG = "Please enter valid Father Contact No at row " + row;
            //            return resVal;
            //        }
            //    }

            //    if (!string.IsNullOrWhiteSpace(dc.M_Contact))
            //    {
            //        var mMobile = base.IsValidContactNo(dc.M_Contact);
            //        if (!mMobile.IsSuccess)
            //        {
            //            resVal.IsSuccess = false;
            //            resVal.ResponseMSG = "Please enter valid Mother Contact No at row " + row;
            //            return resVal;
            //        }
            //    }
            //    //Ends
            //    row++;
            //}
            return db.UpdateClassWiseStudent(_UserId, dataColl);
        }
        public ResponeValues UpdateStudent_Query(List<AcademicLib.BE.Academic.Transaction.UpdateStudentQuery> dataColl, string query)
        {
            return db.UpdateStudent_Query(_UserId, dataColl, query);
        }
            public BE.Academic.Transaction.Student GetStudentById(int EntityId, int StudentId)
        {
            return db.getStudentById(_UserId, EntityId, StudentId);
        }

        public ResponeValues AddUpdateStudentsFromApi(List<Public_API.Student> dataColl)
        {
            return db.AddUpdateStudentsFromApi(_UserId, dataColl);
        }
        public ResponeValues AddUpdateStudentsClosingFromApi( List<Public_API.StudentClosingBal> dataColl)
        {
            return db.AddUpdateStudentsClosingFromApi(_UserId, dataColl);
        }
            public ResponeValues DeleteById(int EntityId, int AcademicYearId)
        {
            return db.DeleteById(_UserId, EntityId, AcademicYearId);
        }

        public BE.Academic.Transaction.StudentAutoComplete getStudentByQrCode(string qrCode, bool getUserPwd = false)
        {
            return db.getStudentByQrCode(_UserId, qrCode,getUserPwd);
        }
            public BE.Academic.Transaction.StudentAutoCompleteCollections getAllStudentAutoComplete(int AcademicYearId, string searchBy, string Operator, string searchValue,bool showLeft, int? classId = null, int? sectionId = null)
        {
            return db.getAllStudentAutoComplete(_UserId,AcademicYearId, searchBy, Operator, searchValue,showLeft,classId,sectionId);
        }
        public BE.Academic.Transaction.StudentUserCollections getStudentUserList(int AcademicYearId,int ClassId,string SectionIdColl, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getStudentUserList(_UserId,AcademicYearId, ClassId,SectionIdColl,BatchId,SemesterId,ClassYearId);
        }
        public AcademicLib.BE.Academic.Transaction.StudentListCollections getStudentListForMarkImport(int AcademicYearId)
        {
            return db.getStudentListForMarkImport(_UserId,AcademicYearId);
        }

        public AcademicLib.API.Student.SiblingDetailCollections getStudentSiblingDetails( int StudentId,int AcademicYearId)
        {
            return db.getStudentSiblingDetails(_UserId, StudentId,AcademicYearId);
        }
            public List<AcademicLib.BE.Academic.Transaction.Student> getClassWiseStudentForUpdate(int AcademicYearId, int ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId,int? BranchId=null)
        {
            if (SectionId.HasValue && SectionId.Value == 0)
                SectionId = null;

            return db.getClassWiseStudentForUpdate(_UserId,AcademicYearId, ClassId, SectionId,BatchId,ClassYearId,SemesterId,BranchId);
        }

        public AcademicLib.RE.Academic.SiblingStudentCollections getSiblintStudentList( int AcademicYearId)
        {
            return db.getSiblintStudentList(_UserId, AcademicYearId);
        }
            public ResponeValues UpdatePersonalInfo(AcademicLib.API.Student.PersonalInfo beData)
        {
            return db.UpdatePersonalInfo(beData);
        }
        public ResponeValues UpdateParentDetails(AcademicLib.API.Student.ParentDetails beData)
        {
            return db.UpdateParentDetails(beData);
        }
        public ResponeValues UpdateContactInfo(AcademicLib.API.Student.ContactInfo beData)
        {
            return db.UpdateContactInfo(beData);
        }
        public ResponeValues UpdateGuardianDetails(AcademicLib.API.Student.GuardianDetails beData)
        {
            return db.UpdateGuardianDetails(beData);
        }

        public ResponeValues UpdatePermanentAddress(AcademicLib.API.Student.PermanentAddress beData)
        {
            return db.UpdatePermanentAddress(beData);
        }
        public ResponeValues UpdateTemporaryAddress(AcademicLib.API.Student.TemporaryAddress beData)
        {
            return db.UpdateTemporaryAddress(beData);
        }
        public ResponeValues UpdateSignature( string SignaturePath)
        {
            return db.UpdateSignature(_UserId, SignaturePath);
        }
            public ResponeValues UpdatePhoto( string PhotoPath,int? StudentId)
        {
            return db.UpdatePhoto(_UserId, PhotoPath,StudentId);
        }
        public ResponeValues UpdateStudentPhoto( int StudentId, string photoPath)
        {
            return db.UpdateStudentPhoto(_UserId, StudentId, photoPath);
        }
            public ResponeValues UpdateStudentPhoto_Query(List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto> dataColl, string query)
        {
            return db.UpdateStudentPhoto_Query(_UserId, dataColl, query);
        }
            public ResponeValues UpdateDOB(List<BE.Academic.Transaction.ImportStudentDOB> dataColl)
        {
            return db.UpdateDOB(_UserId, dataColl);
        }
        public ResponeValues UpdatePhotoUrl(List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto> dataColl)
        {
            return db.UpdatePhoto(_UserId, dataColl);
        }
            public ResponeValues UpdateAdmitDate(List<BE.Academic.Transaction.ImportStudentAdmitDate> dataColl)
        {
            return db.UpdateAdmitDate(_UserId, dataColl);
        }
        public ResponeValues UpdatePreviousSchool(List<AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails> beDataColl)
        {
            return db.UpdatePreviousSchool(_UserId, beDataColl);
        }

        public AcademicLib.RE.Academic.StudentBirthDayCollections getStudentBirthDayList(int? AcademicYearId, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getStudentBirthDayList(_UserId,AcademicYearId, dateFrom, dateTo);
        }
            public ResponeValues ReArrangeRollNoCS(int ReArrangeBy,int? AcademicYearId)
        {
            return db.ReArrangeRollNoCS(_UserId, ReArrangeBy, AcademicYearId);
        }

        public AcademicLib.RE.Academic.StudentPhotoCollections getStudentPhoto(int ClassId, int? SectionId)
        {
            return db.getStudentPhoto(_UserId, ClassId, SectionId);
        }
        public BE.Academic.Transaction.StudentComplainCollections GetStudentComplainForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentComplainForQuickAccessById(_UserId, EntityId, StudentId);
        }
        public BE.Academic.Transaction.StudentLeaveTakenCollections GetStudentLeaveTakenForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentLeaveTakenForQuickAccessById(_UserId, EntityId, StudentId);
        }

        public BE.Academic.Transaction.StudentAttachmentForQACollections GetStudentAttForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentAttForQuickAccessById(_UserId, EntityId, StudentId);
        }
        public BE.Academic.Transaction.StudentAttendanceCollections GetStudentAttendance(int StudentId, int AcademicYearId)
        {
            return db.getStudentAttendance(_UserId, StudentId, AcademicYearId);
        }
        public AcademicLib.RE.Academic.StudentSummaryList getStudentSummaryList(int AcademicYearId,string ClassIdColl, string SectionIdColl, string StudentTypeIdColl, string HouseNameIdColl, string CasteIdColl, int flag=1, List<int> AgeRange = null,string MediumIdColl="", int? BatchId = null, int? SemesterId = null, int? ClassYearId = null,int? BranchId=null)
        {
            if (!string.IsNullOrEmpty(ClassIdColl) && ClassIdColl.Trim() == "0")
                ClassIdColl = "";

            if (!string.IsNullOrEmpty(SectionIdColl) && SectionIdColl.Trim() == "0")
                SectionIdColl = "";

            if (!string.IsNullOrEmpty(HouseNameIdColl) && HouseNameIdColl.Trim() == "0")
                HouseNameIdColl = "";

            if (!string.IsNullOrEmpty(StudentTypeIdColl) && StudentTypeIdColl.Trim() == "0")
                StudentTypeIdColl = "";

            if (!string.IsNullOrEmpty(CasteIdColl) && CasteIdColl.Trim() == "0")
                CasteIdColl = "";

            if (!string.IsNullOrEmpty(MediumIdColl) && MediumIdColl.Trim() == "0")
                MediumIdColl = "";

            

            return db.getStudentSummaryList(_UserId, AcademicYearId, ClassIdColl, SectionIdColl, StudentTypeIdColl, HouseNameIdColl, CasteIdColl, flag,AgeRange,MediumIdColl, BatchId, SemesterId, ClassYearId,BranchId);
        }
        public AcademicLib.RE.Academic.StudentDetailsForLeftCollections getStudentListForBoardRegdNo(int AcademicYearId,int ClassId, int? SectionId,bool all,int? SemesterId=null,int? ClassYearId=null, int? typeId=null, int? BatchId=null,int? BranchId=null)
        {
            return db.getStudentListForBoardRegdNo(_UserId,AcademicYearId, ClassId, SectionId,all,SemesterId,ClassYearId,typeId, BatchId,BranchId);
        }
        public AcademicLib.RE.Academic.StudentDetailsForLeftCollections getStudentListForOpening(int AcademicYearId, int ClassId, int? SectionId, bool all, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null, int? BranchId = null)
        {
            return db.getStudentListForOpening(_UserId, AcademicYearId, ClassId, SectionId, all, SemesterId, ClassYearId, typeId, BatchId, BranchId);
        }
        public ResponeValues UpdateBoardRegdNo(RE.Academic.StudentDetailsForLeftCollections dataColl)
        {
            foreach (var v in dataColl)
                v.UserId = _UserId;

            return db.UpdateBoardRegdNo(dataColl);
        }
        public ResponeValues UpdateStudentLeft(int AcademicYearId, RE.Academic.StudentDetailsForLeftCollections dataColl)
        {
            return db.UpdateStudentLeft(_UserId,AcademicYearId, dataColl);
        }
            public ResponeValues getAutoRegdNo()
        {
            return db.getAutoRegdNo(_UserId);
        }
        public List<AcademicLib.BE.Academic.Transaction.ImportStudent> getStudentListForEMIS(int AcademicYearId)
        {
            return db.getStudentListForEMIS(_UserId,AcademicYearId);
        }
        public AcademicLib.BE.Academic.Transaction.StudentForTranCollections getAllStudentForTran(int AcademicYearId, int? ClassId=null,int? SectionId=null)
        {
            return db.getAllStudentForTran(_UserId,AcademicYearId, ClassId,SectionId);
        }
            public AcademicLib.RE.Academic.StudentIdCardCollections getStudentListForIdCard(int AcademicYearId, int ClassId, int? SectionId, string StudentIdColl, DateTime? validFrom, DateTime? validTo, double duesAmt,int For,int RollNoFrom,int RollNoTo,int? ForMonth, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, bool OnlyPhotoStudent = false,string ClassIdColl="")
        {
            return db.getStudentListForIdCard(_UserId,AcademicYearId, ClassId, SectionId, StudentIdColl, validFrom, validTo, duesAmt,For,RollNoFrom,RollNoTo, ForMonth, SemesterId, ClassYearId, BatchId,OnlyPhotoStudent,ClassIdColl);
        }
            public ResponeValues getAutoRollNo( int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getAutoRollNo(_UserId, ClassId, SectionId, BatchId, SemesterId, ClassYearId);
        }
        public AcademicLib.RE.Academic.AnalysisCollections getAnalysis(int AcademicYearId,List<int> rangeColl)
        {
            return db.getAnalysis(_UserId,AcademicYearId, rangeColl);
        }

        public AcademicLib.API.Admin.Student admin_StudentList(int AcademicYearId, int? ClassId = null, int? SectionId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.admin_StudentList(_UserId,AcademicYearId, ClassId, SectionId,BatchId,SemesterId,ClassYearId);
        }
        public AcademicLib.API.Admin.TransportHostelAnalysis admin_TransportHostelAnalysis(int AcademicYearId)
        {
            return db.admin_TransportHostelAnalysis(_UserId,AcademicYearId);
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
                else if (string.IsNullOrEmpty(beData.RegNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Regd.No.";
                }
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
                else if (string.IsNullOrEmpty(beData.F_ContactNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Father Contact No.";
                }
                else if (string.IsNullOrEmpty(beData.PA_FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Permanent Address";
                }
                else
                {

                    if (string.IsNullOrEmpty(beData.LastName))
                        beData.LastName = "";

                    if (beData.FamilyType == 0)
                        beData.FamilyType = 1;

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

                    if (beData.DOB_AD.HasValue)
                    {
                        var validDOB = DateTime.Today.AddYears(-1);
                        if (beData.DOB_AD.Value > validDOB)
                        {
                            resVal.ResponseMSG = "Please ! Enter Valid DOB";
                            return resVal;
                        }
                    }

                    if (beData.IsImport == false)
                    {//Added validation for father contact No
                        var fmobile = base.IsValidContactNo(beData.F_ContactNo);
                        if (fmobile.IsSuccess == false)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please Enter Valid 10 digit Father Contact No";
                            return resVal;
                        }

                        //Ends
                        //Starts
                        if (!string.IsNullOrWhiteSpace(beData.ContactNo))
                        {
                            var psmobile = base.IsValidContactNo(beData.ContactNo);
                            if (!psmobile.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter valid 10 digit Contact No";
                                return resVal;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(beData.Email))
                        {
                            var psEmail = base.IsValidEmail(beData.Email);
                            if (!psEmail.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Email is invalid. Please enter valid  Email.";
                                return resVal;
                            }
                        }


                        if (!string.IsNullOrWhiteSpace(beData.F_Email))
                        {
                            var fEmail = base.IsValidEmail(beData.F_Email);
                            if (!fEmail.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Father's Email is invalid. Please enter valid Father's Email.";
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

                        if (!string.IsNullOrWhiteSpace(beData.M_Contact))
                        {
                            var mmobile = base.IsValidContactNo(beData.M_Contact);
                            if (!mmobile.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter valid 10 digit Mother's Contact No";
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


                        if (!string.IsNullOrWhiteSpace(beData.G_ContactNo))
                        {
                            var gmobile = base.IsValidContactNo(beData.G_ContactNo);
                            if (!gmobile.IsSuccess)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter valid 10 digit Guardian's Contact No";
                                return resVal;
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
        public AcademicLib.API.Student.StudentProfile getStudentForApp(int AcademicYearId,int? StudentId, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null,string regNo = null)
        {
            return db.getStudentForApp(_UserId,AcademicYearId, StudentId,BatchId,ClassYearId,SemesterId, regNo);
        }
        public ResponeValues GenerateUser(int AcademicYearId, int AsPer, bool CanUpdateUserName, string Prefix, string Suffix)
        {
            return db.GenerateUser(_UserId,AcademicYearId, AsPer, CanUpdateUserName,Prefix,Suffix);
        }
        public ResponeValues ResetClassWisePwd(int ClassId)
        {
            return db.ResetClassWisePwd(_UserId, ClassId);
        }
        public ResponeValues ResetPwdEmployeee()
        {
            return db.ResetPwdEmployeee(_UserId);
        }
        public AcademicLib.BE.Academic.Transaction.StudentListCollections getClassWiseStudentList(int AcademicYearId, int ClassId, string SectionIdColl, bool FilterSection = true, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, int? SubjectId = null, int? BranchId = null, int? FacultyId = null)
        {
            return db.getClassWiseStudentList(_UserId, AcademicYearId, ClassId, SectionIdColl, FilterSection, SemesterId, ClassYearId, BatchId, SubjectId, BranchId, FacultyId);
        }
        public ResponeValues UpdateEnrollCardNo(BE.Academic.Transaction.StudentListCollections dataColl)
        {
            return db.UpdateEnrollCardNo(_UserId,dataColl);
        }
        public AcademicLib.BE.Academic.Transaction.StudentForTranCollections getStudentForTran(int AcademicYearId, int ClassId, int? SectionId, int? ExamTypeId, int? BranchId = null, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            return db.getStudentForTran(_UserId, AcademicYearId, ClassId, SectionId, ExamTypeId, BranchId, BatchId, ClassYearId, SemesterId);
        }

        public ResponeValues UpgradeClassSection(BE.Academic.Transaction.Student beData)
        {
            bool isModify = beData.StudentId > 0;
            ResponeValues isValid = IsValidDataForClass(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.UpgradeClassSection(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues IsValidDataForClass(ref BE.Academic.Transaction.Student beData, bool IsModify)
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
                else if (!beData.ClassId.HasValue || beData.ClassId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }
                else
                {
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

        public ResponeValues UpdateStudentDocument(BE.Academic.Transaction.Student beData)
        {
            bool isModify = beData.StudentId > 0;
            return db.UpdateStudentDocument(beData, isModify);
        }
        public AcademicLib.RE.Academic.StudentPhotoCollections GetAllStudentPhotoDownloadLog(int EntityId)
        {
            return db.getAllStudentPhotoDownloadLog(_UserId, EntityId);
        }
    }
}
