using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PivotalERP.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
namespace AcademicERP.Controllers
{
    public class AdminController : APIBaseController
    {
        // POST GetAllowFieldsDB
        /// <summary>
        ///  Get AllowFields For Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.Student))]
        public IHttpActionResult GetAllowFieldsForEntity([FromBody] JObject para)
        {

            int entityId = 0;
            if (para != null)
            {
                if (para.ContainsKey("entityid") && para["entityid"] != null)
                    entityId = Convert.ToInt32(para["entityid"]);

                if (para.ContainsKey("EntityId") && para["EntityId"] != null)
                    entityId = Convert.ToInt32(para["EntityId"]);


                if (para.ContainsKey("entityId") && para["entityId"] != null)
                    entityId = Convert.ToInt32(para["entityId"]);
            }

            
            var dataColl = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName,dbName).getAllowFields(UserId, entityId);

            var returnVal = new
            {
                IsSuccess=true,
                ResponseMessage=GLOBALMSG.SUCCESS,
                DataColl=dataColl
            };

            return Json(returnVal, new JsonSerializerSettings
            {
            });
        }


        #region "Get Student Analysis"

        // POST GetStudentAnalysis
        /// <summary>
        ///  Get StudentAnalysis                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.Analysis))]
        public IHttpActionResult GetStudentAnalysis()
        {
            AcademicLib.RE.Academic.AnalysisCollections resultColl = new AcademicLib.RE.Academic.AnalysisCollections() ;
            try
            {
                
                resultColl = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getAnalysis(this.AcademicYearId, null);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {                   
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        // POST GetStudentList
        /// <summary>
        ///  Get StudentList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.Student))]
        public IHttpActionResult GetStudentList([FromBody] JObject para)
        {
            AcademicLib.API.Admin.Student returnVal = new AcademicLib.API.Admin.Student();
            int? classId = null, sectionId = null, classYearId = null, batchId = null, semesterId = null ;
            int? academicYearId = null;
            bool groupByClass = false;
            if (para != null)
            {
                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = Convert.ToInt32(para["classId"]);
                else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                    classId = Convert.ToInt32(para["ClassId"]);

                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = Convert.ToInt32(para["sectionId"]);
                else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                    sectionId = Convert.ToInt32(para["SectionId"]);

                if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["academicYearId"]);
                else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["AcademicYearId"]);


                if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    semesterId = Convert.ToInt32(para["semesterId"]);
                else if (para.ContainsKey("SemesterId") && para["SemesterId"] != null)
                    semesterId = Convert.ToInt32(para["SemesterId"]);

                if (para.ContainsKey("classYearId") && para["classYearId"] != null)
                    classYearId = Convert.ToInt32(para["classYearId"]);
                else if (para.ContainsKey("ClassYearId") && para["ClassYearId"] != null)
                    classYearId = Convert.ToInt32(para["ClassYearId"]);

                if (para.ContainsKey("batchId") && para["batchId"] != null)
                    batchId = Convert.ToInt32(para["batchId"]);
                else if (para.ContainsKey("BatchId") && para["BatchId"] != null)
                    batchId = Convert.ToInt32(para["BatchId"]);

                if (para.ContainsKey("groupByClass") && para["groupByClass"] != null)
                    groupByClass = Convert.ToBoolean(para["groupByClass"]);                

            }

            if (classId == 0)
                classId = null;

            if (sectionId == 0)
                sectionId = null;

            if (!academicYearId.HasValue || academicYearId == 0)
                academicYearId = this.AcademicYearId;
            
            try
            {
                returnVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).admin_StudentList(academicYearId.Value, classId, sectionId,batchId,semesterId,classYearId);
                if (groupByClass)
                {
                    returnVal.ClassWiseStudentColl = from st in returnVal.StudentColl
                                group st by new { st.ClassName, st.SectionName, st.Batch, st.Semester, st.ClassYear } into g
                                select new
                                {
                                    ClassName=g.Key.ClassName,
                                    SectionName=g.Key.SectionName,
                                    Batch=g.Key.Batch,
                                    Semester=g.Key.Semester,
                                    ClassYear=g.Key.ClassYear,
                                    TotalStudent = g.Count(),
                                    TotalNew= g.Count(p1 => p1.IsNew == true),
                                    TotalOld = g.Count(p1 => p1.IsNew == false),
                                    StudentColl =g,
                                };

                    returnVal.StudentColl = new List<AcademicLib.API.Admin.StudentDetail>();
                }
            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;
            }

            return Json(returnVal, new JsonSerializerSettings
            {
            });
        }

        // POST EmployeeList
        /// <summary>
        ///  Get EmployeeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.Employee))]
        public IHttpActionResult GetEmployeeList([FromBody]JObject para)
        {
            AcademicLib.API.Admin.Employee returnVal = new AcademicLib.API.Admin.Employee();
          
            try
            {                
                int? departmentId = null;
                if (para != null)
                {                   
                    if (para.ContainsKey("DepartmentId"))
                        departmentId = ToIntNull(para["DepartmentId"]);
                }
                
                returnVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).admin_EmployeeList(departmentId);
            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;
            }

            return Json(returnVal, new JsonSerializerSettings
            {
            });
        }


        // POST GetTransportHostelAnalysis
        /// <summary>
        ///  Get TransportHostelAnalysis                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.TransportHostelAnalysis))]
        public IHttpActionResult GetTransportHostelAnalysis()
        {
            AcademicLib.API.Admin.TransportHostelAnalysis returnVal = new AcademicLib.API.Admin.TransportHostelAnalysis();

            try
            {
                returnVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).admin_TransportHostelAnalysis(this.AcademicYearId);
            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;
            }

            return Json(returnVal, new JsonSerializerSettings
            {
            });
        }

        // POST GetDuesList
        /// <summary>
        ///  Get DuesList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.FeeDues))]
        public IHttpActionResult GetDuesList([FromBody] JObject para)
        {
            AcademicLib.API.Admin.FeeDuesCollections resultColl = new AcademicLib.API.Admin.FeeDuesCollections();
            int? classId = null, sectionId = null;
            int UpToMonthId = 0;

            int? batchId = null, semesterId = null, classYearId = null;

            int forStudent = 0;
            int academicYearId = 0;
            if (para != null)
            {
                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = Convert.ToInt32(para["classId"]);
                else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                    classId = Convert.ToInt32(para["ClassId"]);

                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = Convert.ToInt32(para["sectionId"]);
                else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                    sectionId = Convert.ToInt32(para["SectionId"]);


                if (para.ContainsKey("uptoMonthId") && para["uptoMonthId"] != null)
                    UpToMonthId = Convert.ToInt32(para["uptoMonthId"]);
                else if (para.ContainsKey("UptoMonthId") && para["UptoMonthId"] != null)
                    UpToMonthId = Convert.ToInt32(para["UptoMonthId"]);

                if (para.ContainsKey("forStudent") && para["forStudent"] != null)
                    forStudent = Convert.ToInt32(para["forStudent"]);
                else if (para.ContainsKey("ForStudent") && para["ForStudent"] != null)
                    forStudent = Convert.ToInt32(para["ForStudent"]);

                if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["academicYearId"]);
                else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["AcademicYearId"]);


                if (para.ContainsKey("batchId") && para["batchId"] != null)
                    batchId = Convert.ToInt32(para["batchId"]);
                else if (para.ContainsKey("BatchId") && para["BatchId"] != null)
                    batchId = Convert.ToInt32(para["BatchId"]);

                if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    semesterId = Convert.ToInt32(para["semesterId"]);
                else if (para.ContainsKey("SemesterId") && para["SemesterId"] != null)
                    semesterId = Convert.ToInt32(para["SemesterId"]);

                if (para.ContainsKey("classYearId") && para["classYearId"] != null)
                    classYearId = Convert.ToInt32(para["classYearId"]);
                else if (para.ContainsKey("ClassYearId") && para["ClassYearId"] != null)
                    classYearId = Convert.ToInt32(para["ClassYearId"]);


            }

            if (academicYearId == 0)
                academicYearId = this.AcademicYearId;

            if (classId == 0)
                classId = null;

            if (sectionId == 0)
                sectionId = null;

            try
            {
                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).admin_DuesList(academicYearId, classId, sectionId,UpToMonthId,forStudent,batchId,semesterId,classYearId);
                var returnVal = new
                {
                    Debit=resultColl.Sum(p1=>p1.Debit),
                    Credit=resultColl.Sum(p1=>p1.Credit),
                    Dues=resultColl.Sum(p1=>p1.Dues),
                    DrDiscountAmt=resultColl.Sum(p1=>p1.DrDiscountAmt),
                    CrDiscountAmt=resultColl.Sum(p1=>p1.CrDiscountAmt),
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetFeeHeadingWiseDuesList
        /// <summary>
        ///  Get FeeHeadingWiseDuesList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.FeeDues))]
        public IHttpActionResult GetFeeHeadingWiseDuesList([FromBody] JObject para)
        {
            AcademicLib.API.Admin.FeeHeadingWiseFeeSummaryCollections resultColl = new AcademicLib.API.Admin.FeeHeadingWiseFeeSummaryCollections();
            int? classId = null, sectionId = null;
            int UpToMonthId = 0;
            int academicYearId = 0;
            if (para != null)
            {
                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = Convert.ToInt32(para["classId"]);
                else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                    classId = Convert.ToInt32(para["ClassId"]);


                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = Convert.ToInt32(para["sectionId"]);
                else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                    sectionId = Convert.ToInt32(para["SectionId"]);

                if (para.ContainsKey("uptoMonthId") && para["uptoMonthId"] != null)
                    UpToMonthId = Convert.ToInt32(para["uptoMonthId"]);
                else if (para.ContainsKey("UptoMonthId") && para["UptoMonthId"] != null)
                    UpToMonthId = Convert.ToInt32(para["UptoMonthId"]);

                if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["academicYearId"]);
                else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["AcademicYearId"]);
            }

            if (classId == 0)
                classId = null;

            if (sectionId == 0)
                sectionId = null;

            if (academicYearId == 0)
                academicYearId = this.AcademicYearId;

            try
            {
                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).admin_FeeHeadingWiseFeeSummary(classId, sectionId, UpToMonthId,academicYearId);
                var returnVal = new
                {
                    Debit = resultColl.Sum(p1 => p1.Debit),
                    Credit = resultColl.Sum(p1 => p1.Credit),
                    Dues = resultColl.Sum(p1 => p1.Dues),
                    DrDiscountAmt = resultColl.Sum(p1 => p1.DrDiscountAmt),
                    CrDiscountAmt = resultColl.Sum(p1 => p1.CrDiscountAmt),
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetHostelStudentList
        /// <summary>
        ///  Get HostelStudentList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.HostelStudentCollections))]
        public IHttpActionResult GetHostelStudentList([FromBody] JObject para)
        {
            AcademicLib.API.Admin.HostelStudentCollections resultColl = new AcademicLib.API.Admin.HostelStudentCollections();
            int? classId = null, sectionId = null, academicYearId=null;            
            if (para != null)
            {
                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = Convert.ToInt32(para["classId"]);
                else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                    classId = Convert.ToInt32(para["ClassId"]);

                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = Convert.ToInt32(para["sectionId"]);
                else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                    sectionId = Convert.ToInt32(para["SectionId"]);

                if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["academicYearId"]);
                else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    academicYearId = Convert.ToInt32(para["AcademicYearId"]);
            }

            if (classId == 0)
                classId = null;

            if (sectionId == 0)
                sectionId = null;

            try
            {
                resultColl = new AcademicLib.BL.Hostel.BedMapping(UserId, hostName, dbName).admin_StudentList(classId, sectionId,academicYearId);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetVehicleList
        /// <summary>
        ///  Get VehicleList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.VehicleDetailCollections))]
        public IHttpActionResult GetVehicleList()
        {
            AcademicLib.API.Admin.VehicleDetailCollections resultColl = new AcademicLib.API.Admin.VehicleDetailCollections();
            
            try
            {
                resultColl = new AcademicLib.BL.Transport.Creation.Vehicle(UserId, hostName, dbName).admin_VehicleList();
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetDashboard
        /// <summary>
        ///  Get Dashboard                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Global.AdminDashboard))]
        public IHttpActionResult GetDashboard([FromBody] JObject para)
        {
            try
            {
                int? branchId = null;
                int academicYearId=0;
                if (para != null)
                {
                    if (para.ContainsKey("branchId") && para["branchId"] != null)
                        branchId = Convert.ToInt32(para["branchId"]);
                    else if (para.ContainsKey("BranchId") && para["BranchId"] != null)
                        branchId = Convert.ToInt32(para["BranchId"]);


                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = Convert.ToInt32(para["academicYearId"]);
                    else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                        academicYearId = Convert.ToInt32(para["AcademicYearId"]);
                }

                if (academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                var dataColl = new AcademicLib.BL.Global(UserId, hostName, dbName).GetAdminDashboard(academicYearId, branchId);
              
                return Json(dataColl, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        #region "Get Exam Analysis"

        // POST GetExamResultSummary
        /// <summary>
        ///  Get ExamResultSummary  For Analysis Report               
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Exam.MarkSheet))]
        public IHttpActionResult GetExamResultSummary([FromBody] JObject para)
        {

            AcademicLib.RE.Exam.MarkSheetCollections resultColl = new AcademicLib.RE.Exam.MarkSheetCollections();
            int examTypeId = 0;
            if (para != null)
            {
                if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["examTypeId"]);
                else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["ExamTypeId"]);

            }

            try
            {
                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getExamResultSummary(this.AcademicYearId, examTypeId);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetExamGroupResultSummary
        /// <summary>
        ///  Get ExamGroupResultSummary    For Analysis Report                    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Exam.MarkSheet))]
        public IHttpActionResult GetExamGroupResultSummary([FromBody] JObject para)
        {

            AcademicLib.RE.Exam.MarkSheetCollections resultColl = new AcademicLib.RE.Exam.MarkSheetCollections();
            int examTypeId = 0;
            if (para != null)
            {
                if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["examTypeId"]);
                else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["ExamTypeId"]);

            }

            try
            {
                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getExamGroupResultSummary(this.AcademicYearId, examTypeId);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetTeacherSubjectAnalysis
        /// <summary>
        ///  Get TeacherSubjectAnalysis    For Analysis Report                    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Exam.TeacherWiseSubjectAnalysis))]
        public IHttpActionResult GetTeacherSubjectAnalysis([FromBody] JObject para)
        {

            AcademicLib.RE.Exam.TeacherWiseSubjectAnalysisCollections resultColl = new AcademicLib.RE.Exam.TeacherWiseSubjectAnalysisCollections();
            int examTypeId = 0;
            int examTypeGroupId = 0;
            if (para != null)
            {
                if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["examTypeId"]);
                else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                if (para.ContainsKey("examTypeGroupId") && para["examTypeGroupId"] != null)
                    examTypeId = Convert.ToInt32(para["examTypeGroupId"]);
                else if (para.ContainsKey("ExamTypeGroupId") && para["ExamTypeGroupId"] != null)
                    examTypeId = Convert.ToInt32(para["ExamTypeGroupId"]);
            }

            try
            {
                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getTeacherWiseSubjectAnalysis(this.AcademicYearId, examTypeId,examTypeGroupId);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion


        // POST GetClassWiseTop
        /// <summary>
        ///  Get ClassWiseTop                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ClassWiseTop))]
        public IHttpActionResult GetClassWiseTop([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ClassWiseTopCollections resultColl = new AcademicLib.API.Admin.ClassWiseTopCollections();

            try
            {
                int examTypeId = 0,classId=0,top=10;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = Convert.ToInt32(para["classId"]);
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        classId = Convert.ToInt32(para["ClassId"]);

                    if (para.ContainsKey("top") && para["top"] != null)
                        top = Convert.ToInt32(para["top"]);
                    else if (para.ContainsKey("Top") && para["Top"] != null)
                        top = Convert.ToInt32(para["Top"]);
                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_ClassWiseTop(classId, examTypeId, top);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetSubjectWiseTop
        /// <summary>
        ///  Get SubjectWiseTop                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ClassWiseTop))]
        public IHttpActionResult GetSubjectWiseTop([FromBody] JObject para)
        {
            AcademicLib.API.Admin.SubjectWiseTopCollections resultColl = new AcademicLib.API.Admin.SubjectWiseTopCollections();

            try
            {
                int examTypeId = 0, classId = 0, top = 10;
                int? sectionId = null;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);


                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = Convert.ToInt32(para["classId"]);
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        classId = Convert.ToInt32(para["ClassId"]);

                    if (para.ContainsKey("top") && para["top"] != null)
                        top = Convert.ToInt32(para["top"]);
                    else if (para.ContainsKey("Top") && para["Top"] != null)
                        top = Convert.ToInt32(para["Top"]);

                    if (para.ContainsKey("sectionId") && para["sectionId"] != null && para["sectionId"].ToString()!="null")
                        sectionId = Convert.ToInt32(para["sectionId"]);
                    else if (para.ContainsKey("SectionId") && para["SectionId"] != null && para["SectionId"].ToString() != "null")
                        sectionId = Convert.ToInt32(para["SectionId"]);
                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_SubjectWiseTop(classId,sectionId, examTypeId, top);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetExamWiseTop
        /// <summary>
        ///  Get ExamWiseTop                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ClassWiseTop))]
        public IHttpActionResult GetExamWiseTop([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ClassWiseTopCollections resultColl = new AcademicLib.API.Admin.ClassWiseTopCollections();

            try
            {
                int   top = 10;
                int AcademicYearId;
                if (para != null)
                { 
                    if (para.ContainsKey("top") && para["top"] != null)
                        top = Convert.ToInt32(para["top"]); 
                    else if (para.ContainsKey("Top") && para["Top"] != null)
                        top = Convert.ToInt32(para["Top"]);

                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = Convert.ToInt32(para["AcademicYearId"]);
                            if (AcademicYearId == 0)
                                AcademicYearId = this.AcademicYearId;
                        }
                        catch (Exception e)
                        {
                            AcademicYearId = this.AcademicYearId;
                        }

                    }
                    else
                    {
                        AcademicYearId = this.AcademicYearId;
                    }
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_ExamWiseTop(top,AcademicYearId);
                var returnVal = new
                {
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetExamWiseEvaluation
        /// <summary>
        ///  Get ExamWiseEvaluation                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ExamWiseEvaluation))]
        public IHttpActionResult GetExamWiseEvaluation([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ExamWiseEvaluationCollections resultColl = new AcademicLib.API.Admin.ExamWiseEvaluationCollections();

            try
            {
                int examTypeId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_ExamWiseEvaluation(examTypeId);
                double noOfStudent = resultColl.Sum(p1 => p1.NoOfStudent);
                double noOfFail = resultColl.Sum(p1 => p1.NoOfFail);
                double noOfPass = resultColl.Sum(p1 => p1.NoOfPass);
                double passPer = Math.Round((noOfPass / noOfStudent) * 100,2);
                var returnVal = new
                {
                    NoOfStudent= noOfStudent,
                    NoOfFail=noOfFail,
                    NoOfPass=noOfPass,
                    PassPer=Double.IsNaN(passPer) ? 0 : passPer,
                    FailPer=Double.IsNaN(passPer) ? 0 : Math.Round((100 - passPer), 2),
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetExamGradeWiseEvaluation
        /// <summary>
        ///  Get ExamGradeWiseEvaluation                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ExamWiseEvaluation))]
        public IHttpActionResult GetExamGradeWiseEvaluation([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ExamGradeWiseEvaluationCollections resultColl = new AcademicLib.API.Admin.ExamGradeWiseEvaluationCollections();

            try
            {
                int examTypeId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_ExamGradeWiseEvaluation(examTypeId);
                var query = (from rc in resultColl
                            group rc by rc.Grade into g
                            select new
                            {
                                Grade = g.Key,
                                NoOfStudent = g.Count()
                            }).ToList().OrderBy(p1=>p1.Grade);

                var returnVal = new
                {
                    GradeColl=query,
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetClassWiseEvaluation
        /// <summary>
        ///  Get ClassWiseEvaluation                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.ClassWiseEvaluation))]
        public IHttpActionResult GetClassWiseEvaluation([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ClassWiseEvaluationCollections resultColl = new AcademicLib.API.Admin.ClassWiseEvaluationCollections();

            try
            {
                int examTypeId = 0, classId = 0;
                int? sectionId = null;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = Convert.ToInt32(para["classId"]);
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        classId = Convert.ToInt32(para["ClassId"]);


                    if (para.ContainsKey("sectionId") && para["sectionId"] != null && para["sectionId"].ToString() != "null")
                        sectionId = Convert.ToInt32(para["sectionId"]);
                    else if (para.ContainsKey("SectionId") && para["SectionId"] != null && para["SectionId"].ToString() != "null")
                        sectionId = Convert.ToInt32(para["SectionId"]);
                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_ClassWiseEvaluation(classId, sectionId, examTypeId);
                double noOfStudent = resultColl.Count();
                double totalPass = resultColl.Where(p1 => p1.IsFail == false).Count();
                double totalFail = noOfStudent - totalPass;
                double passPer = Math.Round((totalPass / noOfStudent) * 100, 2);
                double failPer =Math.Round((100 - passPer),2);

                var qry = resultColl.Where(p1 => p1.RankInClass > 0).OrderByDescending(p1 => p1.Per).ToArray();
                var classQry = resultColl.OrderBy(p1 => p1.ObtainMark).ToArray();

                var returnVal = new
                {
                    NoOfStudent=noOfStudent,
                    TotalPass=totalPass,
                    TotalFail=totalFail,
                    PassPer=passPer,
                    FailPer=failPer,
                    ClassToper=(noOfStudent > 0) ? qry[0]  : null,
                    ClassLast= (noOfStudent > 0) ? classQry[0] : null,
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetSubjectWiseEvaluation
        /// <summary>
        ///  Get SubjectWiseEvaluation                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.SubjectWiseEvaluation))]
        public IHttpActionResult GetSubjectWiseEvaluation([FromBody] JObject para)
        {
            AcademicLib.API.Admin.SubjectWiseEvaluationCollections resultColl = new AcademicLib.API.Admin.SubjectWiseEvaluationCollections();

            try
            {
                int examTypeId = 0, classId = 0;
                int? sectionId = null;
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    else if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["ExamTypeId"]);

                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = Convert.ToInt32(para["classId"]);
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        classId = Convert.ToInt32(para["ClassId"]);


                    if (para.ContainsKey("sectionId") && para["sectionId"] != null && para["sectionId"].ToString() != "null")
                        sectionId = Convert.ToInt32(para["sectionId"]);
                    else if (para.ContainsKey("SectionId") && para["SectionId"] != null && para["SectionId"].ToString() != "null")
                        sectionId = Convert.ToInt32(para["SectionId"]);
                }

                resultColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).admin_SubjectWiseEvaluation(classId, sectionId, examTypeId);
                double noOfStudent = resultColl.Sum(p1=>p1.NoOfStudent);
                double totalPass = resultColl.Sum(p1 => p1.NoOfPass);
                double totalFail = noOfStudent - totalPass;
                double passPer = Math.Round((totalPass / noOfStudent) * 100, 2);
                double failPer = Math.Round((100 - passPer), 2);

                var returnVal = new
                {
                    NoOfStudent = noOfStudent,
                    TotalPass = totalPass,
                    TotalFail = totalFail,
                    PassPer = passPer,
                    FailPer = failPer,
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetLastLoginLog
        /// <summary>
        ///  Get LastLoginLog                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetLastLoginLog([FromBody] JObject para)
        {
            AcademicLib.API.Admin.LastLoginLogCollections resultColl = new AcademicLib.API.Admin.LastLoginLogCollections();

            try
            {
                int? forUserId = null;
                string forUser = null;
                DateTime? dateFrom = null, dateTo = null;

                if (para != null)
                {
                    if (para.ContainsKey("forUserId") && para["forUserId"] != null) 
                    { 
                        if(para["forUserId"].ToString() != "null" && !string.IsNullOrEmpty(para["forUserId"].ToString()))
                            forUserId = Convert.ToInt32(para["forUserId"]); 
                    }
                    if (para.ContainsKey("forUser") && para["forUser"] != null)
                    {
                        if (para["forUser"].ToString() != "null" && !string.IsNullOrEmpty(para["forUser"].ToString()))
                            forUser = Convert.ToString(para["forUser"]);
                    }
                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                    {
                        if (para["dateFrom"].ToString() != "null" && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                            dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }
                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                    {
                        if (para["dateTo"].ToString() != "null" && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                            dateTo = Convert.ToDateTime(para["dateTo"]);
                    }
                }

                resultColl = new AcademicLib.BL.Setup.EntityAccess(UserId, hostName, dbName).getLoginLog(forUserId, forUser, dateFrom, dateTo);
           
                var returnVal = new
                {                   
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetDailyCollection
        /// <summary>
        ///  Get DailyCollection                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetDailyCollection([FromBody] JObject para)
        {
            AcademicLib.API.Admin.DailyFeeReceipt resultColl = new AcademicLib.API.Admin.DailyFeeReceipt();

            try
            { 
                DateTime? dateFrom = null, dateTo = null;
                int academicYearId = 0;
                if (para != null)
                {
                    
                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                    {
                        if (para["dateFrom"].ToString() != "null" && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                            dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }
                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                    {
                        if (para["dateTo"].ToString() != "null" && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                            dateTo = Convert.ToDateTime(para["dateTo"]);
                    }
                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    {
                        if (para["academicYearId"].ToString() != "null" && !string.IsNullOrEmpty(para["academicYearId"].ToString()))
                            academicYearId = Convert.ToInt32(para["academicYearId"]);

                    }
                }

                if (academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).admin_DailyCollection(academicYearId, dateFrom, dateTo);

                var returnVal = new
                {
                    TotalReceivedAmt=resultColl.FeeHeadingWiseColl.Sum(p1=>p1.ReceivedAmt),
                    TotalDiscountAmt=resultColl.FeeHeadingWiseColl.Sum(p1=>p1.DiscountAmt),
                    FeeHeadingWiseColl=resultColl.FeeHeadingWiseColl,
                    ReceiptColl=resultColl.ReceiptColl,                    
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetStudentVoucher
        /// <summary>
        ///  Get StudentVoucher                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetStudentVoucher([FromBody] JObject para)
        {
            AcademicLib.RE.Fee.StudentVoucher resultColl = new AcademicLib.RE.Fee.StudentVoucher();

            try
            {
                int StudentId = 0;
                int? SemesterId = null;
                int? ClassYearId = null;
                if (para != null)
                {

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                    {
                        if (para["studentId"].ToString() != "null" && !string.IsNullOrEmpty(para["studentId"].ToString()))
                            StudentId = Convert.ToInt32(para["studentId"]);
                    }

                    if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    {
                        if (para["semesterId"].ToString() != "null" && !string.IsNullOrEmpty(para["semesterId"].ToString()))
                            SemesterId = Convert.ToInt32(para["semesterId"]);
                    }

                    if (para.ContainsKey("classYearId") && para["classYearId"] != null)
                    {
                        if (para["classYearId"].ToString() != "null" && !string.IsNullOrEmpty(para["classYearId"].ToString()))
                            ClassYearId = Convert.ToInt32(para["classYearId"]);
                    }

                }

                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).getStudentVoucher(this.AcademicYearId, StudentId,SemesterId,ClassYearId);

                var returnVal = new
                {
                    Data=resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetClassWiseFeeSummary
        /// <summary>
        ///  Get ClassWiseFeeSummary                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetClassWiseFeeSummary([FromBody] JObject para)
        {
            AcademicLib.API.Admin.ClassWiseFeeSummaryCollections resultColl = new AcademicLib.API.Admin.ClassWiseFeeSummaryCollections();

            try
            {
                int fromMonthId=0, toMonthId=0;
                int forStudent = 0;
                int? studentId = null;
                int? classId = null, sectionId = null, batchId = null, semesterId = null, classYearId = null;
                bool groupWise = false;
                if (para != null)
                {

                    if (para.ContainsKey("groupWise") && para["groupWise"] != null)
                    {
                        if (para["groupWise"].ToString() != "null" && !string.IsNullOrEmpty(para["groupWise"].ToString()))
                            groupWise = Convert.ToBoolean(para["groupWise"]);
                    }

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                    {
                        if (para["studentId"].ToString() != "null" && !string.IsNullOrEmpty(para["studentId"].ToString()))
                            studentId = Convert.ToInt32(para["studentId"]);
                    }

                    if (para.ContainsKey("fromMonthId") && para["fromMonthId"] != null)
                    {
                        if (para["fromMonthId"].ToString() != "null" && !string.IsNullOrEmpty(para["fromMonthId"].ToString()))
                            fromMonthId = Convert.ToInt32(para["fromMonthId"]);
                    }
                    if (para.ContainsKey("toMonthId") && para["toMonthId"] != null)
                    {
                        if (para["toMonthId"].ToString() != "null" && !string.IsNullOrEmpty(para["toMonthId"].ToString()))
                            toMonthId = Convert.ToInt32(para["toMonthId"]);
                    }

                    if (para.ContainsKey("forStudent") && para["forStudent"] != null)
                    {
                        if (para["forStudent"].ToString() != "null" && !string.IsNullOrEmpty(para["forStudent"].ToString()))
                            forStudent = Convert.ToInt32(para["forStudent"]);
                    }

                    if (para.ContainsKey("classId") && para["classId"] != null)
                    {
                        if (para["classId"].ToString() != "null" && !string.IsNullOrEmpty(para["classId"].ToString()))
                            classId = Convert.ToInt32(para["classId"]);
                    }
                    if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    {
                        if (para["sectionId"].ToString() != "null" && !string.IsNullOrEmpty(para["sectionId"].ToString()))
                            sectionId = Convert.ToInt32(para["sectionId"]);
                    }
                    if (para.ContainsKey("batchId") && para["batchId"] != null)
                    {
                        if (para["batchId"].ToString() != "null" && !string.IsNullOrEmpty(para["batchId"].ToString()))
                            batchId = Convert.ToInt32(para["batchId"]);
                    }
                    if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    {
                        if (para["semesterId"].ToString() != "null" && !string.IsNullOrEmpty(para["semesterId"].ToString()))
                            forStudent = Convert.ToInt32(para["semesterId"]);
                    }
                    if (para.ContainsKey("classYearId") && para["classYearId"] != null)
                    {
                        if (para["classYearId"].ToString() != "null" && !string.IsNullOrEmpty(para["classYearId"].ToString()))
                            forStudent = Convert.ToInt32(para["classYearId"]);
                    }
                }

                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).admin_ClassWiseFeeSummary(studentId, fromMonthId, toMonthId, forStudent, classId, sectionId, batchId, semesterId, classYearId);
                if (groupWise)
                {

                    var groupQry = from rc in resultColl
                                   group rc by rc.ClassId into g
                                   select new AcademicLib.API.Admin.ClassWiseFeeSummary
                                   {
                                        BalanceAmt=g.Sum(p1=>p1.BalanceAmt),
                                         ClassId=g.First().ClassId,
                                          ClassName=g.First().ClassName,
                                           C_SNo=g.First().C_SNo,
                                            CurrentDues=g.Sum(p1=>p1.CurrentDues),
                                             DiscountAmt=g.Sum(p1=>p1.DiscountAmt),
                                              NoOfStudent=g.Sum(p1=>p1.NoOfStudent),
                                               PaidAmount=g.Sum(p1=>p1.PaidAmount),
                                                PreviousDues=g.Sum(p1=>p1.PreviousDues),
                                                 ChieldColl=(from ch in g 
                                                             select new AcademicLib.API.Admin.ClassWiseFeeSummary
                                                             {
                                                                  BalanceAmt=ch.BalanceAmt,
                                                                    ClassId=ch.ClassId,
                                                                     ClassName=ch.ClassName,
                                                                      ClassYear=ch.ClassYear,
                                                                       ClassYearId=ch.ClassYearId,
                                                                        CurrentDues=ch.CurrentDues,
                                                                         C_SNo=ch.C_SNo,
                                                                          DiscountAmt=ch.DiscountAmt,
                                                                           NoOfStudent=ch.NoOfStudent,
                                                                            PaidAmount=ch.PaidAmount,
                                                                             PreviousDues=ch.PreviousDues,
                                                                              R_SNo=ch.R_SNo,
                                                                               Section=ch.Section,
                                                                                SectionId=ch.SectionId,
                                                                                 Semester=ch.Semester,                                                                                 
                                                             })
                                   };

                    var returnVal = new
                    {
                        NoOfStudent = resultColl.Sum(p1 => p1.NoOfStudent),
                        PreviousDues = resultColl.Sum(p1 => p1.PreviousDues),
                        CurrentDues = resultColl.Sum(p1 => p1.CurrentDues),
                        PaidAmount = resultColl.Sum(p1 => p1.PaidAmount),
                        DiscountAmt = resultColl.Sum(p1 => p1.DiscountAmt),
                        BalanceAmt = resultColl.Sum(p1 => p1.BalanceAmt),
                        DataColl = groupQry,
                        IsSuccess = resultColl.IsSuccess,
                        ResponseMSG = resultColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                    });



                }
                else
                {
                    var returnVal = new
                    {
                        NoOfStudent = resultColl.Sum(p1 => p1.NoOfStudent),
                        PreviousDues = resultColl.Sum(p1 => p1.PreviousDues),
                        CurrentDues = resultColl.Sum(p1 => p1.CurrentDues),
                        PaidAmount = resultColl.Sum(p1 => p1.PaidAmount),
                        DiscountAmt = resultColl.Sum(p1 => p1.DiscountAmt),
                        BalanceAmt = resultColl.Sum(p1 => p1.BalanceAmt),
                        DataColl = resultColl,
                        IsSuccess = resultColl.IsSuccess,
                        ResponseMSG = resultColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                    });

                }
                

                
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetDiscountStudentList
        /// <summary>
        ///  Get DiscountStudentList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Fee.DiscountStudent))]
        public IHttpActionResult GetDiscountStudentList()
        {
            AcademicLib.RE.Fee.DiscountStudentCollections resultColl = new AcademicLib.RE.Fee.DiscountStudentCollections();

            try
            {
                 

                resultColl = new AcademicLib.BL.Fee.Creation.DiscountType(UserId, hostName, dbName).getDiscountStudentList(this.AcademicYearId,"","");

                var returnVal = new
                {
                    NoOfStudent = resultColl.Count,                   
                    DataColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetCancelFeeReceipt
        /// <summary>
        ///  Get CancelFeeReceipt                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetCancelFeeReceipt([FromBody] JObject para)
        {
            AcademicLib.RE.Fee.FeeReceiptCollections resultColl = new AcademicLib.RE.Fee.FeeReceiptCollections();

            try
            {
                DateTime? dateFrom = null, dateTo = null;
                int? fromReceiptNo = null, toReceiptNo = null;
                if (para != null)
                {

                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                    {
                        if (para["dateFrom"].ToString() != "null" && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                            dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }
                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                    {
                        if (para["dateTo"].ToString() != "null" && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                            dateTo = Convert.ToDateTime(para["dateTo"]);
                    }

                    if (para.ContainsKey("fromReceiptNo") && para["fromReceiptNo"] != null)
                    {
                        if (para["fromReceiptNo"].ToString() != "null" && !string.IsNullOrEmpty(para["fromReceiptNo"].ToString()))
                            fromReceiptNo = Convert.ToInt32(para["fromReceiptNo"]);
                    }

                    if (para.ContainsKey("toReceiptNo") && para["toReceiptNo"] != null)
                    {
                        if (para["toReceiptNo"].ToString() != "null" && !string.IsNullOrEmpty(para["toReceiptNo"].ToString()))
                            toReceiptNo = Convert.ToInt32(para["toReceiptNo"]);
                    }

                }

                double openingAmt = 0;
                double openingDisAmt = 0;
                resultColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).getFeeReceiptCollection(this.AcademicYearId, dateFrom, dateTo, true,fromReceiptNo,toReceiptNo, ref openingAmt,ref openingDisAmt);

                var returnVal = new
                {
                    NoOfReceipt = resultColl.Count,                 
                    ReceiptColl = resultColl,
                    IsSuccess = resultColl.IsSuccess,
                    ResponseMSG = resultColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetBirthDayList
        /// <summary>
        ///  Get Student and Employee BirthDay List
        ///  dateFrom as date is optional
        ///  dateTo as date is optional
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(AcademicLib.RE.Academic.StudentBirthDay))]
        public IHttpActionResult GetBirthDayList([FromBody] JObject para)
        {
            
            try
            {
                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                DateTime? dateFrom = null, dateTo = null;

                if (para != null)
                {

                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                    {
                        if (para["dateFrom"].ToString() != "null" && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                            dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }
                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                    {
                        if (para["dateTo"].ToString() != "null" && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                            dateTo = Convert.ToDateTime(para["dateTo"]);
                    }
                }
                 
                var studentColl = new AcademicLib.BL.Academic.Transaction.Student(_userId, hostName, dbName).getStudentBirthDayList(this.AcademicYearId, dateFrom, dateTo);
                var empColl = new AcademicLib.BL.Academic.Transaction.Employee(_userId, hostName, dbName).getEmpBirthDayList(dateFrom, dateTo);

                var returnVal = new
                {
                    EmployeeColl=empColl,
                    StudentColl = studentColl,
                    IsSuccess = studentColl.IsSuccess,
                    ResponseMSG = studentColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }


        // POST GetStudentDailyBIOAttendance
        /// <summary>
        ///  Get StudentDailyBIOAttendance                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetStudentDailyBIOAttendance([FromBody] JObject para)
        { 
            try
            {
                DateTime forDate = DateTime.Today;
                string classIdColl = "";
                string sectionIdColl = "";
                string batchIdColl = "";
                string semesterIdColl = "";
                string classYearIdColl = "";

                int? AcademicYearId = null;
                if (para != null)
                {

                    if (para.ContainsKey("forDate") && para["forDate"] != null)
                    {
                        if (para["forDate"].ToString() != "null" && !string.IsNullOrEmpty(para["forDate"].ToString()))
                            forDate = Convert.ToDateTime(para["forDate"]);
                    }
                    else if (para.ContainsKey("ForDate") && para["ForDate"] != null)
                    {
                        if (para["ForDate"].ToString() != "null" && !string.IsNullOrEmpty(para["ForDate"].ToString()))
                            forDate = Convert.ToDateTime(para["ForDate"]);
                    }

                    if (para.ContainsKey("classIdColl") && para["classIdColl"] != null)
                    {
                        if (para["classIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["classIdColl"].ToString()))
                            classIdColl = Convert.ToString(para["classIdColl"]);
                    }
                    else if (para.ContainsKey("ClassIdColl") && para["ClassIdColl"] != null)
                    {
                        if (para["ClassIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["ClassIdColl"].ToString()))
                            classIdColl = Convert.ToString(para["ClassIdColl"]);
                    }
                    else if (para.ContainsKey("classId") && para["classId"] != null)
                    {
                        if (para["classId"].ToString() != "null" && !string.IsNullOrEmpty(para["classId"].ToString()))
                            classIdColl = Convert.ToString(para["classId"]);

                        if (classIdColl == "0")
                            classIdColl = "";
                    }

                    if (para.ContainsKey("sectionIdColl") && para["sectionIdColl"] != null)
                    {
                        if (para["sectionIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["sectionIdColl"].ToString()))
                            sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                    }
                    else if (para.ContainsKey("SectionIdColl") && para["SectionIdColl"] != null)
                    {
                        if (para["SectionIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["SectionIdColl"].ToString()))
                            sectionIdColl = Convert.ToString(para["SectionIdColl"]);
                    }
                    else if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    {
                        if (para["sectionId"].ToString() != "null" && !string.IsNullOrEmpty(para["sectionId"].ToString()))
                            sectionIdColl = Convert.ToString(para["sectionId"]);

                        if (sectionIdColl == "0")
                            sectionIdColl = "";
                    }

                    if (para.ContainsKey("batchIdColl") && para["batchIdColl"] != null)
                    {
                        if (para["batchIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["batchIdColl"].ToString()))
                            batchIdColl = Convert.ToString(para["batchIdColl"]);
                    }
                    else if (para.ContainsKey("BatchIdColl") && para["BatchIdColl"] != null)
                    {
                        if (para["BatchIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["BatchIdColl"].ToString()))
                            batchIdColl = Convert.ToString(para["BatchIdColl"]);
                    }
                    else if (para.ContainsKey("batchId") && para["batchId"] != null)
                    {
                        if (para["batchId"].ToString() != "null" && !string.IsNullOrEmpty(para["batchId"].ToString()))
                            batchIdColl = Convert.ToString(para["batchId"]);


                        if (batchIdColl == "0")
                            batchIdColl = "";
                    }


                    if (para.ContainsKey("semesterIdColl") && para["semesterIdColl"] != null)
                    {
                        if (para["semesterIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["semesterIdColl"].ToString()))
                            semesterIdColl = Convert.ToString(para["semesterIdColl"]);
                    }
                    else if (para.ContainsKey("SemesterIdColl") && para["SemesterIdColl"] != null)
                    {
                        if (para["SemesterIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["SemesterIdColl"].ToString()))
                            semesterIdColl = Convert.ToString(para["SemesterIdColl"]);
                    }
                    else if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    {
                        if (para["semesterId"].ToString() != "null" && !string.IsNullOrEmpty(para["semesterId"].ToString()))
                            semesterIdColl = Convert.ToString(para["semesterId"]);



                        if (semesterIdColl == "0")
                            semesterIdColl = "";
                    }


                    if (para.ContainsKey("classYearIdColl") && para["classYearIdColl"] != null)
                    {
                        if (para["classYearIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["classYearIdColl"].ToString()))
                            classYearIdColl = Convert.ToString(para["classYearIdColl"]);
                    }
                    else if (para.ContainsKey("ClassYearIdColl") && para["ClassYearIdColl"] != null)
                    {
                        if (para["ClassYearIdColl"].ToString() != "null" && !string.IsNullOrEmpty(para["ClassYearIdColl"].ToString()))
                            classYearIdColl = Convert.ToString(para["ClassYearIdColl"]);
                    }
                    else if (para.ContainsKey("classYearId") && para["classYearId"] != null)
                    {
                        if (para["classYearId"].ToString() != "null" && !string.IsNullOrEmpty(para["classYearId"].ToString()))
                            classYearIdColl = Convert.ToString(para["classYearId"]);
                         

                        if (classYearIdColl == "0")
                            classYearIdColl = "";
                    }


                    if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    {
                        if (para["AcademicYearId"].ToString() != "null" && !string.IsNullOrEmpty(para["AcademicYearId"].ToString()))
                            AcademicYearId = Convert.ToInt32(para["AcademicYearId"]);
                    }

                }

                if (!AcademicYearId.HasValue)
                    AcademicYearId = this.AcademicYearId;

                var dataColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getStudentDailyAttendance(AcademicYearId.Value, forDate, classIdColl,sectionIdColl,batchIdColl,semesterIdColl,classYearIdColl);
                 
                var returnVal = new
                {
                    DataColl= dataColl,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetStudentMonthlyBIOAttendance
        /// <summary>
        ///  Get StudentMonthlyBIOAttendance                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetStudentMonthlyBIOAttendance([FromBody] JObject para)
        {
            try
            {
                int yearId = 0, monthId = 0;
                int classId = 0;
                int? sectionId = null;
                if (para != null)
                {

                    if (para.ContainsKey("yearId") && para["yearId"] != null)
                    {
                        if (para["yearId"].ToString() != "null" && !string.IsNullOrEmpty(para["yearId"].ToString()))
                            yearId = Convert.ToInt32(para["yearId"]);
                    }
                    else if (para.ContainsKey("YearId") && para["YearId"] != null)
                    {
                        if (para["YearId"].ToString() != "null" && !string.IsNullOrEmpty(para["YearId"].ToString()))
                            yearId = Convert.ToInt32(para["YearId"]);
                    }

                    if (para.ContainsKey("monthId") && para["monthId"] != null)
                    {
                        if (para["monthId"].ToString() != "null" && !string.IsNullOrEmpty(para["monthId"].ToString()))
                            monthId = Convert.ToInt32(para["monthId"]);
                    }else if (para.ContainsKey("MonthId") && para["MonthId"] != null)
                    {
                        if (para["MonthId"].ToString() != "null" && !string.IsNullOrEmpty(para["MonthId"].ToString()))
                            monthId = Convert.ToInt32(para["MonthId"]);
                    }

                    if (para.ContainsKey("classId") && para["classId"] != null)
                    {
                        if (para["classId"].ToString() != "null" && !string.IsNullOrEmpty(para["classId"].ToString()))
                            classId = Convert.ToInt32(para["classId"]);
                    }
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                    {
                        if (para["ClassId"].ToString() != "null" && !string.IsNullOrEmpty(para["ClassId"].ToString()))
                            classId = Convert.ToInt32(para["ClassId"]);
                    }

                    if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    {
                        if (para["sectionId"].ToString() != "null" && !string.IsNullOrEmpty(para["sectionId"].ToString()))
                            sectionId = Convert.ToInt32(para["sectionId"]);

                    }else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                    {
                        if (para["SectionId"].ToString() != "null" && !string.IsNullOrEmpty(para["SectionId"].ToString()))
                            sectionId = Convert.ToInt32(para["SectionId"]);
                    }

                }

                var dataColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getStudentMonthlyAttendance(this.AcademicYearId, yearId, monthId, classId, sectionId);

                var returnVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

   

        // POST GetEmpDailyBIOAttendance
        /// <summary>
        ///  Get EmpDailyBIOAttendance                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetEmpDailyBIOAttendance([FromBody] JObject para)
        {
            try
            {
                DateTime forDate = DateTime.Today;
                int empType = 1;
                if (para != null)
                {

                    if (para.ContainsKey("forDate") && para["forDate"] != null)
                    {
                        if (para["forDate"].ToString() != "null" && !string.IsNullOrEmpty(para["forDate"].ToString()))
                            forDate = Convert.ToDateTime(para["forDate"]);
                    }
                    else if (para.ContainsKey("ForDate") && para["ForDate"] != null)
                    {
                        if (para["ForDate"].ToString() != "null" && !string.IsNullOrEmpty(para["ForDate"].ToString()))
                            forDate = Convert.ToDateTime(para["ForDate"]);
                    }

                    if (para.ContainsKey("empType") && para["empType"] != null)
                    {
                        if (para["empType"].ToString() != "null" && !string.IsNullOrEmpty(para["empType"].ToString()))
                            empType = Convert.ToInt32(para["empType"]);
                    }
                    else if (para.ContainsKey("EmpType") && para["EmpType"] != null)
                    {
                        if (para["EmpType"].ToString() != "null" && !string.IsNullOrEmpty(para["EmpType"].ToString()))
                            empType = Convert.ToInt32(para["EmpType"]);
                    }
                }

                var dataColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getEmpDailyAttendance(forDate,"",false,empType);

                var returnVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        // POST GetStudentMonthlyBIOAttendance
        /// <summary>
        ///  Get StudentMonthlyBIOAttendance                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.LastLoginLog))]
        public IHttpActionResult GetEmpMonthlyBIOAttendance([FromBody] JObject para)
        {
            try
            {
                int yearId = 0, monthId = 0;
                int empType = 0;
                int ForEmp = 0;
                //1=All,2=Teaching,3=Non Teaching
                if (para != null)
                {

                    if (para.ContainsKey("yearId") && para["yearId"] != null)
                    {
                        if (para["yearId"].ToString() != "null" && !string.IsNullOrEmpty(para["yearId"].ToString()))
                            yearId = Convert.ToInt32(para["yearId"]);
                    }
                    else if (para.ContainsKey("YearId") && para["YearId"] != null)
                    {
                        if (para["YearId"].ToString() != "null" && !string.IsNullOrEmpty(para["YearId"].ToString()))
                            yearId = Convert.ToInt32(para["YearId"]);
                    }

                    if (para.ContainsKey("monthId") && para["monthId"] != null)
                    {
                        if (para["monthId"].ToString() != "null" && !string.IsNullOrEmpty(para["monthId"].ToString()))
                            monthId = Convert.ToInt32(para["monthId"]);
                    }
                    else if (para.ContainsKey("MonthId") && para["MonthId"] != null)
                    {
                        if (para["MonthId"].ToString() != "null" && !string.IsNullOrEmpty(para["MonthId"].ToString()))
                            monthId = Convert.ToInt32(para["MonthId"]);
                    }

                    if (para.ContainsKey("empType") && para["empType"] != null)
                    {
                        if (para["empType"].ToString() != "null" && !string.IsNullOrEmpty(para["empType"].ToString()))
                            empType = Convert.ToInt32(para["empType"]);
                    }
                    else if (para.ContainsKey("EmpType") && para["EmpType"] != null)
                    {
                        if (para["EmpType"].ToString() != "null" && !string.IsNullOrEmpty(para["EmpType"].ToString()))
                            empType = Convert.ToInt32(para["EmpType"]);
                    }
                    

                    if (para.ContainsKey("ForEmp") && para["ForEmp"] != null)
                    {
                        if (para["ForEmp"].ToString() != "null" && !string.IsNullOrEmpty(para["ForEmp"].ToString()))
                            ForEmp = Convert.ToInt32(para["ForEmp"]);
                    }
                    else if (para.ContainsKey("ForEmp") && para["ForEmp"] != null)
                    {
                        if (para["ForEmp"].ToString() != "null" && !string.IsNullOrEmpty(para["ForEmp"].ToString()))
                            ForEmp = Convert.ToInt32(para["ForEmp"]);
                    }

                }

                var dataColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getEmpMonthlyAttendance( yearId, monthId,"", empType, ForEmp);

                var returnVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        #region "Add Visitor Log"


        // Post api/AddVisitor
        /// <summary>
        ///  Add Visitor Log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddVisitor()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = HttpStatusCode.UnsupportedMediaType.ToString();
                }
                else
                {
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/fronddesk"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.BE.FrontDesk.Transaction.Visitor para = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.Visitor>(jsonData);
                    para.VisitorId = 0;
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.AttachmentColl = DocumentColl;
                            }
                        }
                        var retVal = new AcademicLib.BL.FrontDesk.Transaction.Visitor(UserId, hostName, dbName).SaveFormData(para);
                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.ResponseId))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            notification.Content = retVal.ResponseMSG;
                            notification.ContentPath = resVal.RId.ToString();
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.VISITOR);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.VISITOR.ToString();
                            notification.Heading = "Visitor";
                            notification.Subject = para.Purpose;
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.ResponseId.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName,GetBaseUrl).SendNotification(UserId, notification, true);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                            resVal = retVal;
                          
                    }
                }

                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                   "IsSuccess","ResponseMSG"
                                 }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetVisitorLog
        /// <summary>
        ///  Get VisitorLog                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.FrontDesk.Transaction.Visitor))]
        public IHttpActionResult GetVisitorLog([FromBody] JObject para)
        {
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                 
                if (para != null)
                {

                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                    {
                        if (para["dateFrom"].ToString() != "null" && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                            dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }

                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                    {
                        if (para["dateTo"].ToString() != "null" && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                            dateTo = Convert.ToDateTime(para["dateTo"]);
                    }
                }

                var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Visitor(UserId, hostName, dbName).GetAllVisitor(0, dateFrom, dateTo);

                var returnVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }
            catch (Exception ee)
            {
                return BadRequest("Invalid Data " + ee.Message);
            }

        }

        #endregion

        #region "HomeWork"



        #endregion


        // POST Get Web Users List
        /// <summary>
        ///  Get All Web User List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Security.User))]
        public IHttpActionResult GetWebUsers()
        {

            //var dataColl = new Dynamic.DataAccess.Security.UserDB(hostName, dbName).getAllUserShortDetailForWeb(UserId, true);
            var dataColl = new AcademicLib.BL.Global(UserId, hostName, dbName).GetWebUser();
            var returnVal = new
            {
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG,
                DataColl = dataColl
            };

            return Json(returnVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                                 {
                                   "IsSuccess","ResponseMSG","UserId","UserName","DataColl"
                                 }
                }
            });
        }


    }
}