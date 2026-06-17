using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
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
    public class ThirdPartyController : ApiKeyValBaseController
    {
        #region "Add and Update Students"

        // Post api/UpdateStudent
        /// <summary>
        ///  Add New Students and Update Exists Students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateStudent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/student"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    List<AcademicLib.Public_API.Student> para = DeserializeObject<List<AcademicLib.Public_API.Student>>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {

                        foreach(AcademicLib.Public_API.Student beData in para)
                        {
                            if (!string.IsNullOrEmpty(beData.Gender))
                            {
                                var g = beData.Gender.Trim().ToLower();
                                if(g=="m" || g=="male" || g=="1" || g == "boy")
                                {
                                    beData.Gender = "M";
                                }
                                else if (g == "f" || g == "female" || g == "2" || g == "girl")
                                {
                                    beData.Gender = "F";
                                }
                            }
                            if (!string.IsNullOrEmpty(beData.FullName))
                            {
                                beData.FullName = beData.FullName.Trim();
                                char[] split = { ' ' };
                                string[] str = beData.FullName.Trim().Split(split, StringSplitOptions.RemoveEmptyEntries);
                                try
                                {
                                    beData.FirstName = str[0];

                                    if (str.Length > 2)
                                    {
                                        beData.MiddleName = str[1];
                                        beData.LastName = str[2];

                                        if (str.Length > 3)
                                        {
                                            beData.LastName = beData.LastName + " " + str[3];
                                        }
                                        if (str.Length > 4)
                                        {
                                            beData.LastName = beData.LastName + " " + str[4];
                                        }
                                    }
                                    else if (str.Length > 1)
                                    {
                                        beData.LastName = str[1];
                                    } 
                                }
                                catch (Exception ee) { }
                            }
                            
                        }
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                //para.AttachmentColl = DocumentColl;
                            }
                        }

                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).AddUpdateStudentsFromApi(para);

                    }
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // Post api/UpdateStudent
        /// <summary>
        ///  Add New Students and Update Exists Students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateStudentB([FromBody]List<AcademicLib.Public_API.Student> para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    foreach (AcademicLib.Public_API.Student beData in para)
                    {
                        if (!string.IsNullOrEmpty(beData.Gender))
                        {
                            var g = beData.Gender.Trim().ToLower();
                            if (g == "m" || g == "male" || g == "1" || g == "boy")
                            {
                                beData.Gender = "M";
                            }
                            else if (g == "f" || g == "female" || g == "2" || g == "girl")
                            {
                                beData.Gender = "F";
                            }
                        }

                        if (!string.IsNullOrEmpty(beData.FullName))
                        {
                            beData.FullName = beData.FullName.Trim();
                            char[] split = { ' ' };
                            string[] str = beData.FullName.Trim().Split(split, StringSplitOptions.RemoveEmptyEntries);
                            try
                            {
                                beData.FirstName = str[0];

                                if (str.Length > 2)
                                {
                                    beData.MiddleName = str[1];
                                    beData.LastName = str[2];

                                    if (str.Length > 3)
                                    {
                                        beData.LastName = beData.LastName + " " + str[3];
                                    }
                                    if (str.Length > 4)
                                    {
                                        beData.LastName = beData.LastName + " " + str[4];
                                    }
                                }
                                else if (str.Length > 1)
                                {
                                    beData.LastName = str[1];
                                }
                            }
                            catch (Exception ee) { }
                        }

                    }

                    resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).AddUpdateStudentsFromApi(para);
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // Post api/UpdateStudent
        /// <summary>
        ///  Add New Students and Update Exists Students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))] 
        public async Task<IHttpActionResult> UpdateStudentS([FromBody]AcademicLib.Public_API.Student para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    List<AcademicLib.Public_API.Student> dataColl = new List<AcademicLib.Public_API.Student>();
                    dataColl.Add(para);

                    foreach (AcademicLib.Public_API.Student beData in dataColl)
                    {
                        if (!string.IsNullOrEmpty(beData.Gender))
                        {
                            var g = beData.Gender.Trim().ToLower();
                            if (g == "m" || g == "male" || g == "1" || g == "boy")
                            {
                                beData.Gender = "M";
                            }
                            else if (g == "f" || g == "female" || g == "2" || g == "girl")
                            {
                                beData.Gender = "F";
                            }
                        }

                        if (!string.IsNullOrEmpty(beData.FullName))
                        {
                            beData.FullName = beData.FullName.Trim();
                            char[] split = { ' ' };
                            string[] str = beData.FullName.Trim().Split(split, StringSplitOptions.RemoveEmptyEntries);
                            try
                            {
                                beData.FirstName = str[0];

                                if (str.Length > 2)
                                {
                                    beData.MiddleName = str[1];
                                    beData.LastName = str[2];

                                    if (str.Length > 3)
                                    {
                                        beData.LastName = beData.LastName + " " + str[3];
                                    }
                                    if (str.Length > 4)
                                    {
                                        beData.LastName = beData.LastName + " " + str[4];
                                    }
                                }
                                else if (str.Length > 1)
                                {
                                    beData.LastName = str[1];
                                }
                            }
                            catch (Exception ee) { }
                        }

                    }

                    resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).AddUpdateStudentsFromApi(dataColl);
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        #endregion

        #region "Student Closing Bal"

        // Post api/UpdateStudentClosing
        /// <summary>
        ///  Update Student Closing balance
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateStudentClosing([FromBody] List<AcademicLib.Public_API.StudentClosingBal> para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {                     
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).AddUpdateStudentsClosingFromApi(para);
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // Post api/UpdateStudentClosingS
        /// <summary>
        ///  Update Student Closing balance
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateStudentClosingS([FromBody] AcademicLib.Public_API.StudentClosingBal para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    List<AcademicLib.Public_API.StudentClosingBal> dataColl = new List<AcademicLib.Public_API.StudentClosingBal>();
                    dataColl.Add(para);
                     

                    resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).AddUpdateStudentsClosingFromApi(dataColl);
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Send Notification"

        // Post api/Send Notification
        /// <summary>
        ///  Update Student Closing balance
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendNotification([FromBody] AcademicLib.Public_API.Notification para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }                
                else
                {
                    if(string.IsNullOrEmpty(para.RegNo))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Reg.No. is missing";

                    }
                    else
                    {

                        var studentIdColl = new AcademicLib.BL.Global(1, hostName, dbName).GetStudentIdColl(para.RegNo);

                        if(studentIdColl!=null && studentIdColl.Count > 0)
                        {
                            List<Dynamic.BusinessEntity.Global.NotificationLog> logColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();

                            var glbFN = new PivotalERP.Global.GlobalFunction(1, hostName, dbName);

                            string uid = "";
                            foreach(var st in studentIdColl)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                    uid = uid + ",";

                                uid = uid + st.UserId.ToString();
                            }

                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = para.Message;
                            notification.ContentPath = "";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE.ToString();
                            notification.Heading = para.Heading;
                            notification.Subject = para.Heading;
                            notification.UserId = 1;
                            notification.UserName = "Admin";
                            notification.UserIdColl = uid;
                            logColl.Add(notification);
                            resVal = glbFN.SendNotification(1, logColl);
                        }
                        else
                        {
                            resVal.ResponseMSG = studentIdColl.ResponseMSG;
                            resVal.IsSuccess = studentIdColl.IsSuccess;
                        }
                        
                    }
                  
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendNotifications([FromBody] List<AcademicLib.Public_API.Notification> paraColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (paraColl == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    string regdColl = "";
                    foreach(var para in paraColl)
                    {
                        if(!string.IsNullOrEmpty(para.RegNo))
                        {
                            if (!string.IsNullOrEmpty(regdColl))
                                regdColl = regdColl + ",";

                            regdColl = regdColl+para.RegNo;
                        }
                    }

                    if (string.IsNullOrEmpty(regdColl))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Reg.No. is missing";

                    }
                    else
                    {

                        var studentIdColl = new AcademicLib.BL.Global(1, hostName, dbName).GetStudentIdColl(regdColl);

                        if (studentIdColl != null && studentIdColl.Count > 0)
                        {
                            List<Dynamic.BusinessEntity.Global.NotificationLog> logColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();

                            var glbFN = new PivotalERP.Global.GlobalFunction(1, hostName, dbName);                             
                            foreach (var st in studentIdColl)
                            {
                                var findStudent = paraColl.Find(p1 => p1.RegNo == st.RegNo);

                                if (findStudent != null)
                                {
                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                    notification.Content = findStudent.Message;
                                    notification.ContentPath = "";
                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE);
                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE.ToString();
                                    notification.Heading = findStudent.Heading;
                                    notification.Subject = findStudent.Heading;
                                    notification.UserId = 1;
                                    notification.UserName = "Admin";
                                    notification.UserIdColl = st.UserId.ToString();
                                    logColl.Add(notification);
                                }                                
                            }
                            resVal = glbFN.SendNotification(1, logColl);
                        }
                        else
                        {
                            resVal.ResponseMSG = studentIdColl.ResponseMSG;
                            resVal.IsSuccess = studentIdColl.IsSuccess;
                        }
                    }
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Upload Student Statement"

        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UploadStudentStatement([FromBody] List<AcademicLib.Public_API.StudentStatement> paraColl)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                if (paraColl == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    List<AcademicLib.Public_API.StudentStatement> dataColl = new List<AcademicLib.Public_API.StudentStatement>();
                      foreach(var v in paraColl)
                        {
                            if (!string.IsNullOrEmpty(v.RegNo))
                            {
                                if (string.IsNullOrEmpty(v.Remarks))
                                    v.Remarks = "";

                            if (string.IsNullOrEmpty(v.Particular))
                                v.Particular = "";

                            if (v.VoucherDate.Year < 1900)
                                v.VoucherDate = DateTime.Today;


                            dataColl.Add(v);
                            }
                        }

                    resVal = new AcademicLib.BL.Fee.Creation.BillGenerate(1, hostName, dbName).Upload_TP_StudentStatement(dataColl);
                  
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Get Online Payment List"

        [HttpGet]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> GetOnlinePaymentList(DateTime? fromDate=null,DateTime? toDate=null)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                if (!fromDate.HasValue)
                    fromDate = DateTime.Today;

                if (!toDate.HasValue)
                    toDate = DateTime.Today;

                var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(1, hostName, dbName).getOnlinePaymentList(this.AcademicYearId, fromDate.Value, toDate.Value);
                var retunVal = new
                {
                    DataColl=dataColl,
                    ResMSG = dataColl.ResponseMSG,
                    ResStatus = dataColl.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Add Exam Mark Entrys"
 
        // Post api/ExamMarks
        /// <summary>
        ///  Upload Exam Mark Entry
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddExamMarks([FromBody] List<AcademicLib.BE.Exam.Transaction.ImportMarkEntry> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {

                    var uid =UserId ;
                    
                    #region "Mark Entry"

                    AcademicLib.BE.Academic.Creation.SubjectCollections _tmpSubjectColl = new AcademicLib.BL.Academic.Creation.Subject(uid, hostName,dbName).GetAllSubject(0, this.AcademicYearId);
                    AcademicLib.BE.Exam.Creation.ExamTypeCollections _tmpExamTypeColl = new AcademicLib.BL.Exam.Creation.ExamType(uid, hostName, dbName).GetAllExamType(this.AcademicYearId, 0);
                    AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections _tmpSymbolNoColl = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(uid, hostName, dbName).getSymbolNoForMarkImport();
                    AcademicLib.BE.Academic.Transaction.StudentListCollections _tmpStudentColl = new AcademicLib.BL.Academic.Transaction.Student(uid, hostName, dbName).getStudentListForMarkImport(this.AcademicYearId);
                    AcademicLib.BE.Academic.Creation.ClassCollections _tmpClassColl = new AcademicLib.BL.Academic.Creation.Class(uid, hostName, dbName).GetAllClass(0);
                    AcademicLib.BE.Academic.Creation.SectionCollections _tmpSectionColl = new AcademicLib.BL.Academic.Creation.Section(uid, hostName, dbName).GetAllSection(0);
                    try
                    {
                        List<AcademicLib.BE.Exam.Transaction.ImportMarkEntry> tmpMarksColl = new List<AcademicLib.BE.Exam.Transaction.ImportMarkEntry>();
                        foreach (var v in dataColl)
                        {
                            AcademicLib.BE.Exam.Transaction.ImportMarkEntry beData = (AcademicLib.BE.Exam.Transaction.ImportMarkEntry)v;
                            if (beData != null)
                            {
                                if (!string.IsNullOrEmpty(beData.ExamType))
                                {
                                    if (!string.IsNullOrEmpty(beData.SubjectName))
                                    {
                                        if (!string.IsNullOrEmpty(beData.PaperType))
                                        {
                                            beData.ObtainMarkTH = IsNullStr(beData.ObtainMarkTH);
                                            beData.ObtainMarkPR = IsNullStr(beData.ObtainMarkPR);
                                            beData.GraceMarkTH = IsNullStr(beData.GraceMarkTH);
                                            beData.GraceMarkPR = IsNullStr(beData.GraceMarkPR);
                                            beData.ExamType = beData.ExamType.Trim().ToLower();
                                            beData.SubjectName = beData.SubjectName.Trim().ToLower();
                                            beData.RegdNo = IsNullStr(beData.RegdNo).Trim().ToLower();
                                            beData.BoardRegdNo = IsNullStr(beData.BoardRegdNo).Trim().ToLower();
                                            beData.SymbolNo = IsNullStr(beData.SymbolNo).Trim().ToLower();
                                            beData.ClassName = IsNullStr(beData.ClassName).Trim().ToLower();
                                            beData.SectionName = IsNullStr(beData.SectionName).Trim().ToLower();
                                            beData.PaperType = IsNullStr(beData.PaperType).Trim().ToLower();

                                            tmpMarksColl.Add(beData);

                                        }

                                    }
                                }
                            }
                        }

                        AcademicLib.API.Teacher.MarkEntryCollections markEntriesColl = new AcademicLib.API.Teacher.MarkEntryCollections();
                        foreach (var mc in tmpMarksColl)
                        {
                            var examType = _tmpExamTypeColl.Find(p1 => p1.Name.Trim().ToLower() == mc.ExamType);
                            if (examType != null)
                            {
                                var sub = _tmpSubjectColl.Find(p1 => p1.Name.Trim().ToLower() == mc.SubjectName);
                                if (sub != null)
                                {
                                    AcademicLib.BE.Academic.Transaction.StudentList st = null;
                                    if (!string.IsNullOrEmpty(mc.RegdNo) && st is null)
                                    {
                                        st = _tmpStudentColl.Find(p1 => p1.RegdNo.Trim().ToLower() == mc.RegdNo);
                                    }

                                    if (!string.IsNullOrEmpty(mc.BoardRegdNo) && st is null)
                                    {
                                        st = _tmpStudentColl.Find(p1 => p1.BoardRegdNo.Trim().ToLower() == mc.BoardRegdNo);
                                    }

                                    if (!string.IsNullOrEmpty(mc.SymbolNo) && st is null)
                                    {
                                        var symb = _tmpSymbolNoColl.Find(p1 => p1.SymbolNo.Trim().ToLower() == mc.SymbolNo && p1.ExamTypeId == examType.ExamTypeId);
                                        if (symb != null)
                                        {
                                            st = _tmpStudentColl.Find(p1 => p1.StudentId == symb.StudentId);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(mc.ClassName) && !string.IsNullOrEmpty(mc.SectionName) && st is null && mc.RollNo > 0)
                                    {
                                        st = _tmpStudentColl.Find(p1 => p1.RollNo == mc.RollNo && p1.ClassName.Trim().ToLower() == mc.ClassName && p1.SectionName.Trim().ToLower() == mc.SectionName);
                                    }

                                    if (!string.IsNullOrEmpty(mc.ClassName) && string.IsNullOrEmpty(mc.SectionName) && st is null && mc.RollNo > 0)
                                    {
                                        st = _tmpStudentColl.Find(p1 => p1.RollNo == mc.RollNo && p1.ClassName.Trim().ToLower() == mc.ClassName);
                                    }

                                    if (st != null)
                                    {
                                        AcademicLib.API.Teacher.MarkEntry me = new AcademicLib.API.Teacher.MarkEntry();
                                        me.ExamTypeId = examType.ExamTypeId.Value;
                                        me.StudentId = st.StudentId;
                                        me.SubjectId = sub.SubjectId.Value;
                                        me.ObtainMarkTH = mc.ObtainMarkTH;
                                        me.ObtainMarkPR = mc.ObtainMarkPR;
                                        me.Remarks = mc.Remarks;

                                        if (mc.PaperType == "th" || mc.PaperType == "t")
                                            me.PaperType = 1;
                                        else if (mc.PaperType == "pr" || mc.PaperType == "p")
                                            me.PaperType = 2;
                                        else if (mc.PaperType == "both" || mc.PaperType == "tp" || mc.PaperType == "b")
                                            me.PaperType = 3;
                                        else if (mc.PaperType == "grading" || mc.PaperType == "g" || mc.PaperType == "grade")
                                            me.PaperType = 4;
                                        else
                                            me.PaperType = 3;

                                        markEntriesColl.Add(me);
                                    }

                                }
                            }
                        }

                        resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(uid, hostName, dbName).SaveMarkEntry(markEntriesColl); 
                    }
                    catch { }

                    #endregion
                     
                     
                }

                var retunVal = new
                {
                    ResMSG = resVal.ResponseMSG,
                    ResStatus = resVal.IsSuccess,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        #endregion
    }
}
