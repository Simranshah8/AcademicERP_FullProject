using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Elearning.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Elearning/Creation
        public ActionResult AddLesson()
        {
            return View();
        }
        public ActionResult AddlessonPlan()
        {
            return View();
        }
        public ActionResult UpdateLessonPlan()
        {
            return View();
        }
        public ActionResult TodaysClass()
        {
            return View();
        }
        public ActionResult SyllabusStatus()
        {
            return View();
        }
        public ActionResult EContent()
        {
            return View();
        }

        #region "Question Category"

        public ActionResult QuestionCategory()
        {
            return View();
        }
       
        [HttpPost]
        public JsonNetResult AddExamModal()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.OnlineExam.ExamModal>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    resVal = new AcademicLib.BL.OnlineExam.ExamModal(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };

        }

        //GetAllExamModalList


        [HttpPost]
        public JsonNetResult GetAllExamModalList()
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamModal(User.UserId, User.HostName, User.DBName).GetAllExamModal(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        //EditQuestionCategory

        [HttpPost]
        public JsonNetResult GetQuestionCategoryById(int CategoryId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamModal(User.UserId, User.HostName, User.DBName).GetExamModalById(0, CategoryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }


        //deleteQuestionCategory

        [HttpPost]
        public JsonNetResult DelQuestionCategory(int CategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.OnlineExam.ExamModal(User.UserId, User.HostName, User.DBName).DeleteById(0, CategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };

        }

        #endregion
        public ActionResult ExamSetup()
        {
            return View();
        }

        #region"ExamSetup Crude"
        [HttpPost, ValidateInput(false)]
        public JsonNetResult AddExamSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.OnlineExam.ExamSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamSetupId.HasValue)
                        beData.ExamSetupId = 0;

                    resVal = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };


        }

        [HttpPost]
        public JsonNetResult GetExamSetupDetails(int examTypeId,int classId,string sectionIdColl,int subjectId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getExamSetupDetails(examTypeId, classId, sectionIdColl, subjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess =true, ResponseMSG = GLOBALMSG.SUCCESS };

        }

        [HttpPost]
        public JsonNetResult GetQuestionByExamSetupId(int examSetupId, int categoryId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).getQuestionSetupByExamSetupId(examSetupId, categoryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };

        }

        [HttpPost]
        public JsonNetResult GetAllExamSetup()
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).GetAllExamSetup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult GetExamSetupById(int ExamSetupId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).GetExamSetupById(0, ExamSetupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }


        //deleteQuestionCategory
        [HttpPost]
        public JsonNetResult DelExamSetupById(int ExamSetupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).DeleteById(0, ExamSetupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };

        }

        #endregion

        public ActionResult QuestionSetup()
        {
            return View();
        }

        #region "Question setup"

        [HttpPost]
        public JsonNetResult AddQuestionSetup()
        {
            string photoLocation = "/Attachments/onlineexam";
            AcademicLib.BE.OnlineExam.QuestionSetup questionVal = new AcademicLib.BE.OnlineExam.QuestionSetup();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.OnlineExam.QuestionSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var question = filesColl["question"]; 

                        if (question != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, question, true);
                            beData.QuestionPath = photoDoc.DocPath;                            
                        }
                         
                        int fInd = 1;
                        foreach (var v in beData.DetailsColl)
                        {
                            HttpPostedFileBase file = filesColl["answer" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                v.FilePath = att.DocPath;
                            }
                            fInd++;
                        }
                    }

                    beData.CUserId = User.UserId;
                   var resVal = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        questionVal = beData;
                        questionVal.IsSuccess = resVal.IsSuccess;
                        questionVal.ResponseMSG = resVal.ResponseMSG;
                    }                    
                }
                else
                {
                    questionVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                questionVal.IsSuccess = false;
                questionVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = questionVal, TotalCount = 0, IsSuccess = questionVal.IsSuccess, ResponseMSG = questionVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelQuestionList(int tranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).DeleteById(0, tranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult GetQuestionList(int examTypeId,int classId,string sectionIdColl,int subjectId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).getQuestionSetupList(examTypeId, classId, sectionIdColl, subjectId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion
        public ActionResult Questionlist()
        {
            return View();
        }
        public ActionResult OnlineClass()
        {
            return View();
        }
        public ActionResult StudentAttendance()
        {
            return View();
        }

        public ActionResult EmployeeAttendance()
        {
            return View();
        }

        public ActionResult ExamStatus()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetOnlineExamList(int? forType)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getExamList(forType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetOnlineExamDetById(int examSetupId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getExamSetupByIdForAPI(examSetupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess =true, ResponseMSG = GLOBALMSG.SUCCESS };

        }

        [HttpPost]
        public JsonNetResult GetQuestionListById(int examSetupId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).getQuestionSetupList(0, 0, "", 0, examSetupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult Evaluate()
        {
            return View();
        }
        
        [HttpPost]
        public JsonNetResult GetOnlineExamListForEvaluate(DateTime dateFrom, DateTime dateTo, int examTypeId, int classId, int? sectionId, int subjectId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getExamListForEvaluate(dateFrom, dateTo, examTypeId, classId, sectionId, subjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentForEvaluate(int examSetupId,int classId,int? sectionId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getStudentForEvaluate(examSetupId, classId, sectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetOnlineExamAnswer(int examSetupId, int studentId)
        {
            var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName, User.DBName).getQuestionListForAPI(examSetupId, studentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ExamCopyCheck()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/onlineexam");

            string photoLocation = "/Attachments/onlineexam";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.API.Teacher.ExamCopyCheck>(Request["jsonData"]);
                if (beData == null)
                {
                    resVal.ResponseMSG="No form data found";
                }
                else if (!beData.OETranId.HasValue)
                {
                    resVal.ResponseMSG = "Invalid Exam Copy";
                }
                else if (beData.OETranId.Value == 0)
                {
                    resVal.ResponseMSG = "Invalid Exam Copy";
                }
                if (beData != null)
                {
                    bool validFile = true;
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var DocumentColl = new List<Dynamic.BusinessEntity.GeneralDocument>();
                        foreach(System.Web.HttpPostedFileBase fl in filesColl)
                        {
                            var newDoc = GetAttachmentDocuments(photoLocation, fl);
                            if (newDoc != null)
                                DocumentColl.Add(newDoc);
                        }

                        foreach (var dc in DocumentColl)
                        {
                            string fullPath = path + "//" + dc.Name;
                            if (System.IO.File.Exists(fullPath))
                            {
                                string nDoc = basePath + dc.DocPath;
                                try
                                {
                                    System.IO.File.Copy(nDoc, fullPath, true);
                                }
                                catch (Exception fr)
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = "Can't Update Exam File " + fr.Message;

                                    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                                }

                            }
                            else
                            {
                                validFile = false;
                                resVal.ResponseMSG = "Invalid File Name";
                            }
                        }
                    }

                    if (validFile)
                        resVal = new AcademicLib.BL.OnlineExam.QuestionSetup(User.UserId, User.HostName,User.DBName).ExamCopyCheck(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetOnlineExamListForPS()
        {
            var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(User.UserId, User.HostName, User.DBName).getExamListForPreStudent();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonNetResult SaveLessonPlanningColl()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlanningCollections>(Request["jsonData"]);
                if (beData != null && beData.Count > 0)
                {


                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlanning(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLPClassSubjectSecWise(int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(User.UserId, User.HostName, User.DBName).GetLPClassSubjectSecWise(ClassId, SubjectId, SectionIdColl, BatchId, ClassYearId, SemesterId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult SaveLessonPlanningContent()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>>(Request["jsonData"]);
                if (dataColl != null)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlanning(User.UserId, User.HostName, User.DBName).SaveLessonPlannigContent(dataColl);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [HttpPost]
        public JsonNetResult GetLessonPlanningTopicContent(int TranId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(User.UserId, User.HostName, User.DBName).getLessonPlanningTopicContent(TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonPlanForUpdate(int ClassId, int SubjectId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(User.UserId, User.HostName, User.DBName).getLessonPlanForUpdate(ClassId, SubjectId, SectionId, BatchId, ClassYearId, SemesterId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}