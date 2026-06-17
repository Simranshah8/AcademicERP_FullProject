using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class LessonPlan
    {
        DA.Academic.Transaction.LessonPlanDB db = null;
        int _UserId = 0;
        public LessonPlan(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.LessonPlanDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.LessonPlan beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues UpdatePlanDate(BE.Academic.Transaction.LessonPlan beData)
        {
            //ResponeValues resVal = new ResponeValues();
            //foreach(var ld in beData.DetailsColl)
            //{
            //    if(ld.PlanStartDate_AD.HasValue && ld.PlanEndDate_AD.HasValue)
            //    {

            //    }
            //}
            //resVal=db.UpdatePlanDate(beData);

            //return resVal;

            return db.UpdatePlanDate(beData); 
        }
        public AcademicLib.RE.Academic.TodayLessonPlanCollections getTodayLessonPlan( int AcademicYearId, DateTime? ForDate, int? ClassId, int? SectionId, int? SubjectId, int? EmployeeId,int ReportType, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getTodayLessonPlan(_UserId, AcademicYearId, ForDate, ClassId, SectionId, SubjectId, EmployeeId,ReportType, BatchId,SemesterId,ClassYearId);
        }
            public ResponeValues StartLesson(BE.Academic.Transaction.LessonPlanDetails beData)
        {
            return db.StartLesson(_UserId,beData);
        }

        public ResponeValues EndLesson(BE.Academic.Transaction.LessonPlanDetails beData)
        {
            return db.EndLesson(_UserId,beData);
        }

        public ResponeValues StartTopic(BE.Academic.Transaction.LessonTopic beData)
        {
            return db.StartTopic(_UserId, beData);
        }

        public ResponeValues EndTopic(BE.Academic.Transaction.LessonTopic beData)
        {
            return db.EndTopic(_UserId, beData);
        }

        public ResponeValues StartTopicContent(BE.Academic.Transaction.LessonTopicTeacherContent beData)
        {
            return db.StartTopicContent(_UserId, beData);
        }

        public ResponeValues EndTopicContent(BE.Academic.Transaction.LessonTopicTeacherContent beData)
        {
            return db.EndTopicContent(_UserId, beData);
        }
        public AcademicLib.BE.Academic.Transaction.LessonPlanCollections getLessonPlanByClass( int? ClassId,int? SectionId, int? EmployeeId, int? SubjectId, int ReportType, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            var lessonPlanColl = db.getLessonPlanByClass(_UserId, ClassId,SectionId,EmployeeId,SubjectId,ReportType, BatchId, ClassYearId, SemesterId);

            foreach(var lessonPlan in lessonPlanColl)
            {
                double totalTopicContent1 = 0;
                double totalInProgress1 = 0, totalCompleted1 = 0, totalPending1 = 0;

                foreach (var lt in lessonPlan.DetailsColl)
                {
                    double totalTopicContent = 0;
                    double totalInProgress = 0, totalCompleted = 0, totalPending = 0;
                    if (lt.TopicColl != null)
                    {
                        lt.TotalDays = lt.TopicColl.Sum(p1 => p1.TotalDays);
                        foreach (var tc in lt.TopicColl)
                        {
                            totalTopicContent += tc.ContentsColl.Count;
                            totalCompleted += tc.ContentsColl.Where(p1 => p1.StatusValue == 3).Count();
                            totalInProgress += tc.ContentsColl.Where(p1 => p1.StatusValue == 2).Count();
                            totalPending += tc.ContentsColl.Where(p1 => p1.StatusValue == 1).Count();
                        }
                    }

                    totalTopicContent1 += totalTopicContent;
                    totalCompleted1 += totalCompleted;
                    totalInProgress1 += totalInProgress;
                    totalPending1 += totalPending;

                    if (totalTopicContent > 0)
                    {
                        if (totalPending > 0)
                        {
                            lt.PendingPer = Math.Round(totalPending / totalTopicContent * 100, 2);
                        }
                        if (totalCompleted > 0)
                        {
                            lt.CompletedPer = Math.Round(totalCompleted / totalTopicContent * 100, 2);
                        }
                        if (totalInProgress > 0)
                        {
                            lt.InProgressPer = Math.Round(totalInProgress / totalTopicContent * 100, 2);
                        }
                    }
                }

                if (totalTopicContent1 > 0)
                {
                    if (totalPending1 > 0)
                    {
                        lessonPlan.PendingPer = Math.Round(totalPending1 / totalTopicContent1 * 100, 2);
                    }
                    if (totalCompleted1 > 0)
                    {
                        lessonPlan.CompletedPer = Math.Round(totalCompleted1 / totalTopicContent1 * 100, 2);
                    }
                    if (totalInProgress1 > 0)
                    {
                        lessonPlan.InProgressPer = Math.Round(totalInProgress1 / totalTopicContent1 * 100, 2);
                    }
                }
            }
            

            return lessonPlanColl;
        }
        public AcademicLib.BE.Academic.Transaction.LessonPlan getLessonPlanByClassSubjectWise(int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId,int? AcademicYearId)
        {
            var lessonPlan = db.getLessonPlanByClassSubjectWise(_UserId, ClassId, SubjectId, SectionIdColl, BatchId, ClassYearId, SemesterId,AcademicYearId);

            foreach (var lt in lessonPlan.DetailsColl)
            {
                double totalTopicContent = 0;
                double totalInProgress = 0, totalCompleted = 0, totalPending = 0;
                if (lt.TopicColl != null)
                {
                    lt.TotalDays = lt.TopicColl.Sum(p1 => p1.TotalDays);
                    foreach (var tc in lt.TopicColl)
                    {
                        totalTopicContent += tc.ContentsColl.Count;
                        totalCompleted += tc.ContentsColl.Where(p1 => p1.StatusValue == 3).Count();
                        totalInProgress += tc.ContentsColl.Where(p1 => p1.StatusValue == 2).Count();
                        totalPending += tc.ContentsColl.Where(p1 => p1.StatusValue == 1).Count();
                    }
                }

                if (totalTopicContent > 0)
                {
                    if (totalPending > 0)
                    {
                        lt.PendingPer = Math.Round(totalPending / totalTopicContent * 100, 2);
                    }
                    if (totalCompleted > 0)
                    {
                        lt.CompletedPer = Math.Round(totalCompleted / totalTopicContent * 100, 2);
                    }
                    if (totalInProgress > 0)
                    {
                        lt.InProgressPer = Math.Round(totalInProgress / totalTopicContent * 100, 2);
                    }
                }
            }

            return lessonPlan;
        }

        public ResponeValues SaveLessonTeacherContent(List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
        {
            return db.SaveLessonTeacherContent(_UserId, dataColl);
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLessonTeacherContent(int LessonId, int LessonSNo)
        {
            return db.getLessonTeacherContent(_UserId, LessonId, LessonSNo);
        }



        public ResponeValues SaveLessonTopicTeacherContent(List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
        {
            return db.SaveLessonTopicTeacherContent(_UserId, dataColl);
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLessonTopicTeacherContent(int LessonId, int LessonSNo, int TopicSNo)
        {
            return db.getLessonTopicTeacherContent(_UserId, LessonId, LessonSNo, TopicSNo);
        }

        public ResponeValues SaveLessonTopicContent(List<BE.Academic.Transaction.LessonTopicContent> dataColl)
        {
            return db.SaveLessonTopicContent(_UserId, dataColl);
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections getLessonTopicContent( /*int LessonId,*/ int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getLessonTopicContent(_UserId, /*LessonId,*/ LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);
        }
        public ResponeValues SaveLessonTopicVideo(List<BE.Academic.Transaction.LessonTopicVideo> dataColl)
        {
            return db.SaveLessonTopicVideo(_UserId, dataColl);
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections getLessonTopicVideo( /*int LessonId,*/ int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getLessonTopicVideo(_UserId, /*LessonId,*/ LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);
        }
        //Eha dekhi tala ko end comment lekheko (line no 238) samma ko code lai replace gardenu. Esma  DelLessonTopicQuizById and GetLessonTopicQuizList new ho
        public ResponeValues SaveLessonTopicQuiz(BE.Academic.Transaction.LessonTopicQuiz beData)
        {
            bool isModify = beData.QuizId > 0;
            return db.SaveLessonTopicQuiz(_UserId,beData, isModify);           
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicQuiz getLessonTopicQuiz(int QuizId)
        {
            return db.getLessonTopicQuiz(_UserId, QuizId);
        }

        public BE.Academic.Transaction.LessonTopicQuizCollections GetLessonTopicQuizList( /*int LessonId,*/ int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getLessonTopicQuizList(_UserId, /*LessonId,*/ LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);
        }
        public ResponeValues DelLessonTopicQuizById(int EntityId, int QuizId)
        {
            return db.DelLessonTopicQuizById(_UserId, EntityId, QuizId);
        }
        //Ends
        public ResponeValues IsValidData(ref BE.Academic.Transaction.LessonPlan beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }
                else if (beData.SubjectId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Subject Name";
                }
                else
                {
                    if (beData.NoOfLesson != beData.DetailsColl.Count)
                    {
                        resVal.ResponseMSG = "No of lesson and its details does not matched";
                        return resVal;
                    }

                    foreach(var ld in beData.DetailsColl)
                    {
                        if (ld.SNo == 0)
                        {
                            resVal.ResponseMSG = "Please ! Enter S.No. of Topic";
                            return resVal;
                        }

                        if (ld.TopicColl != null)
                        {
                            if (!ld.PlanStartDate_AD.HasValue)
                            {
                                ld.PlanStartDate_AD = ld.TopicColl.Min(p1 => p1.PlanStartDate_AD);
                            }

                            if (!ld.PlanEndDate_AD.HasValue)
                            {
                                ld.PlanEndDate_AD = ld.TopicColl.Max(p1 => p1.PlanEndDate_AD);
                            }
                        }
                        

                        if (string.IsNullOrEmpty(ld.LessonName))
                        {
                            resVal.IsSuccess = false;   
                            resVal.ResponseMSG = "Please ! Enter Lesson Name";
                            return resVal;
                        }

                        foreach(var td in ld.TopicColl)
                        {
                            if (string.IsNullOrEmpty(td.TopicName))
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please ! Enter Topic Name Of Lesson("+ld.SNo.ToString()+")";
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
    }
}
