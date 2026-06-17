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
    public class StudentController : APIBaseController
    {
        #region "Profile"

        // POST GetStudentProfile
        /// <summary>
        ///  Get GetStudentProfile                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Student.StudentProfile))]
        public IHttpActionResult GetStudentProfile([FromBody] JObject para)
        {
            AcademicLib.API.Student.StudentProfile profile = new AcademicLib.API.Student.StudentProfile();
            try
            {
                int AcademicYearId;
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                profile = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getStudentForApp(AcademicYearId, null);
                if (profile.StudentId > 0)
                {
                    profile.IsSuccess = true;
                    return Json(profile, new JsonSerializerSettings
                    {
                    });

                }
                else
                {
                    var retVal = new
                    {
                        IsSuccess = false,
                        ResponseMSG = "Invalid Student"
                    };

                    return Json(profile, new JsonSerializerSettings
                    {

                    });
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetStudentDetails
        /// <summary>
        ///  Get GetStudentDetails                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.Student))]
        public IHttpActionResult GetStudentDetails()
        {
            AcademicLib.BE.Academic.Transaction.Student profile = new AcademicLib.BE.Academic.Transaction.Student();
            try
            {
                profile = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).GetStudentById(0, 0);
                return Json(profile, new JsonSerializerSettings
                {
                    //ContractResolver = new MyJsonContractResolver()
                    //{
                    //    ExcludeProperties = new List<string>
                    //                    {
                    //                        "UserId"                                     
                    //                    }
                    //}
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST UpdatePersonalInfo
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdatePersonalInfo([FromBody] AcademicLib.API.Student.PersonalInfo para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Student_Information);
                    if(resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdatePersonalInfo(para);
                    }                   
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateParentDetails([FromBody] AcademicLib.API.Student.ParentDetails para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Parent_Detail);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdateParentDetails(para);
                    }
                   
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateContactInfo([FromBody] AcademicLib.API.Student.ContactInfo para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Contact_Info);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdateContactInfo(para);
                    }
                    
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateGuardianDetails([FromBody] AcademicLib.API.Student.GuardianDetails para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Guardian_Detail);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdateGuardianDetails(para);
                    }
                    // 
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdatePermanentAddress([FromBody] AcademicLib.API.Student.PermanentAddress para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Permanent_Address);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdatePermanentAddress(para);
                    }

                    //
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateTemporaryAddress([FromBody] AcademicLib.API.Student.TemporaryAddress para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    para.CUserId = UserId;
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Current_Addres);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdateTemporaryAddress(para);
                    }
                    
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // Post api/UpdatePhoto
        /// <summary>
        ///  Submit Photo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdatePhoto()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int? StudentId = null;

                var provider = new FormDataStreamProvider(GetPath("~/Attachments/Academic/Student"));
                await Request.Content.ReadAsMultipartAsync(provider);
                try
                {
                    string jsonData = provider.FormData["paraDataColl"];
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        var beData= DeserializeObject<JObject>(jsonData);
                        StudentId = ToIntNull(beData["StudentId"]);
                    }
                }
                catch { }

                if (!StudentId.HasValue)
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Student_Photo);
                else
                    resVal.IsSuccess = true;

                if (resVal.IsSuccess)
                {
                    if (!Request.Content.IsMimeMultipartContent())
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = HttpStatusCode.UnsupportedMediaType.ToString();
                    }
                    else
                    {
                      

                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                               
                                var photo = DocumentColl[0];
                                resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdatePhoto(photo.DocPath,StudentId);
                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "No Photo";
                            }
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "No Photo";
                        }
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

        // Post api/UpdateSignature
        /// <summary>
        ///  Submit Photo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateSignature()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
              
                resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Student_Signature);
                if (resVal.IsSuccess)
                {
                    if (!Request.Content.IsMimeMultipartContent())
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = HttpStatusCode.UnsupportedMediaType.ToString();
                    }
                    else
                    {
                        var provider = new FormDataStreamProvider(GetPath("~/Attachments/Academic/Student"));
                        await Request.Content.ReadAsMultipartAsync(provider);

                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                var photo = DocumentColl[0];
                                resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdateSignature(photo.DocPath);
                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "No Photo";
                            }
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "No Photo";
                        }
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

        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdatePreviousSchool([FromBody] List<AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails> para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentProfile, (int)AcademicLib.BE.FormEntity.StudentProfile.Previous_Academic_Detail);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).UpdatePreviousSchool(para);
                    }
                   
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        #endregion

        #region "ClassSchedule"

        // POST GetClassSchedule
        /// <summary>
        ///  Get ClassSchedule     
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.ClassScheduleCollections))]
        public IHttpActionResult GetClassSchedule([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.ClassScheduleCollections scheduleColl = new AcademicLib.RE.Academic.ClassScheduleCollections();
            try
            {
                int? classId = null, sectionId = null;
                int AcademicYearId ;
                if (para != null)
                {
                   
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }

                scheduleColl = new AcademicLib.BL.Academic.Transaction.ClassSchedule(UserId, hostName, dbName).getClassSchedule(AcademicYearId, classId, sectionId);

                return Json(scheduleColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "GetSubjectList"

        // POST GetSubjectList
        /// <summary>
        ///  Get SubjectList                 
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetSubjectList([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Creation.SubjectCollections subjectColl = new AcademicLib.BE.Academic.Creation.SubjectCollections();
            try
            {
                int? classId = null, sectionId = null;
                int AcademicYearId;
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }


                subjectColl = new AcademicLib.BL.Academic.Creation.Subject(UserId, hostName, dbName).GetAllSubject(0,AcademicYearId, classId, sectionId);
                return Json(subjectColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","SubjectId","Name","Code","CodeTH","CodePR"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "ExamTypeList"

        // POST GetExamTypeList
        /// <summary>
        ///  Get ExamTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.ExamTypeCollections))]
        public IHttpActionResult GetExamTypeList([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.ExamTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.ExamTypeCollections();
            try
            {
                int AcademicYearId;
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                examTypeColl = new AcademicLib.BL.Exam.Creation.ExamType(UserId, hostName, dbName).GetAllExamType(AcademicYearId, 0);
                return Json(examTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime","ExamDate","StartTime","Duration"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "ExamTypeGroupList"

        // POST GetExamTypeGroupList
        /// <summary>
        ///  Get GetExamTypeGroupList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.ExamTypeGroupCollections))]
        public IHttpActionResult GetExamTypeGroupList([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.ExamTypeGroupCollections examTypeColl = new AcademicLib.BE.Exam.Creation.ExamTypeGroupCollections();
            try
            {
                int AcademicYearId;
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                examTypeColl = new AcademicLib.BL.Exam.Creation.ExamTypeGroup(UserId, hostName, dbName).GetAllExamTypeGroup(AcademicYearId, 0);
                return Json(examTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","ExamTypeGroupId","Name","DisplayName","ResultDate","ResultTime"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "ExamSchedule"

        // POST GetExamSchedule
        /// <summary>
        ///  Get ClassSchedule     
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Exam.ExamScheduleCollections))]
        public IHttpActionResult GetExamSchedule([FromBody] JObject para)
        {
            AcademicLib.RE.Exam.ExamScheduleCollections scheduleColl = new AcademicLib.RE.Exam.ExamScheduleCollections();
            try
            {
                int AcademicYearId;
                int? classId = null, examTypeId = null;
                string sectionIdColl = "";
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionIdColl"))
                        sectionIdColl = Convert.ToString(para["sectionIdColl"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }

                scheduleColl = new AcademicLib.BL.Exam.Transaction.ExamSchedule(UserId, hostName, dbName).GetExamSchedule(AcademicYearId, classId, sectionIdColl, examTypeId);

                return Json(scheduleColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Running ClassList"

        // POST GetRunningClasses
        /// <summary>
        ///  Get Running Classes List        
        ///  tranId as Int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.RunningClassCollections))]
        public IHttpActionResult GetRunningClasses([FromBody]int? tranId)
        {
            AcademicLib.RE.Academic.RunningClassCollections classColl = new AcademicLib.RE.Academic.RunningClassCollections();
            try
            {
                classColl = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getRunningClassList(tranId);
                return Json(classColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Pass Online ClassList"

        // POST GetPassOnlineClasses
        /// <summary>
        ///  Get PassOnline Classes
        ///  dateFrom as Date
        ///  dateTo as Date
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.PassedOnlineClassCollections))]
        public IHttpActionResult GetPassOnlineClasses([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.PassedOnlineClassCollections classColl = new AcademicLib.RE.Academic.PassedOnlineClassCollections();
            try
            {
                DateTime? dateFrom = DateTime.Today;
                DateTime? dateTo = DateTime.Today;

                if (para == null)
                {
                    return BadRequest("No form data found");
                }

                if (para.ContainsKey("dateFrom"))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo"))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                classColl = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getPassedClassList(dateFrom, dateTo);
                if (classColl != null && classColl.Count > 0)
                {
                    var query = from cc in classColl
                                group cc by cc.StartDateTime_AD into g
                                select new
                                {
                                    Date_AD = g.First().StartDateTime_AD,
                                    Date_BS = g.First().StartDate_BS,
                                    DataColl = g,
                                    IsSuccess = classColl.IsSuccess,
                                    ResponseMSG = classColl.ResponseMSG
                                };

                    return Json(query, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //                {
                            //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
                            //                }
                        }
                    });

                }
                return Json(classColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Join Online Class"
        // POST JoinOnlineClass
        /// <summary>
        ///  Get JoinOnlineClass                 
        ///  tranId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult JoinOnlineClass([FromBody] ResponeValues req)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int tranId = req.RId;
                if (tranId != 0)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).JoinOnlineClass(tranId);

                    return Json(resVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = true,
                            IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                        }
                    });

                }
                else
                    return BadRequest("No parameters found");

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        #endregion

        #region "HomeWorkList"

        // POST GetHomeWorkList
        /// <summary>
        ///  Get HomeWorkList     
        ///  dateFrom as Date    (Optional)
        ///  dateTo as Date   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.HomeWorkCollections))]
        public IHttpActionResult GetHomeWorkList([FromBody] JObject para)
        {
            AcademicLib.RE.HomeWork.HomeWorkCollections homeWorkColl = new AcademicLib.RE.HomeWork.HomeWorkCollections();
            try
            {
                DateTime? dateFrom = null, dateTo = null;
                if (para != null)
                {
                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                }
                homeWorkColl = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).GetAllHomeWork(0, dateFrom, dateTo,true);

                return Json(homeWorkColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "SubmitHomeWork"

        // Post api/SubmitHomeWork
        /// <summary>
        ///  Submit Homework
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SubmitHomeWork()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/homework"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Student.HomeWorkSubmit para = DeserializeObject<AcademicLib.API.Student.HomeWorkSubmit>(jsonData);                 
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }else if (para.HomeWorkId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Homework";
                    }
                    else
                    {
                        para.UserId = UserId;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.AttachmentColl = DocumentColl;
                            }
                        }
                        string msg = "";
                        resVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).SubmitHomeWork(para,ref msg);
                        if (resVal.IsSuccess && !string.IsNullOrEmpty(resVal.ResponseId) && !string.IsNullOrEmpty(msg))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = msg;
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK.ToString();
                            notification.Heading = "Homework Submit";
                            notification.Subject = "Homework Submit";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = resVal.ResponseId;
                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

                            if (!resVal.IsSuccess)
                            {
                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = resVal.ResponseMSG + " Error On Send Notification.";
                            }
                        }
                        

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

        #endregion

        #region "AssignmentList"

        // POST GetAssignmentList
        /// <summary>
        ///  Get AssignmentList     
        ///  dateFrom as Date    (Optional)
        ///  dateTo as Date   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.AssignmentCollections))]
        public IHttpActionResult GetAssignmentList([FromBody] JObject para)
        {
            AcademicLib.RE.HomeWork.AssignmentCollections AssignmentColl = new AcademicLib.RE.HomeWork.AssignmentCollections();
            try
            {
                DateTime? dateFrom = null, dateTo = null;
                if (para != null)
                {
                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                }
                AssignmentColl = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).GetAllAssignment(0, dateFrom, dateTo);

                return Json(AssignmentColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "SubmitAssignment"

        //// Post api/SubmitAssignment
        ///// <summary>
        /////  Submit Assignment
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[ResponseType(typeof(ResponeValues))]
        //public async Task<IHttpActionResult> SubmitAssignment()
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            resVal.IsSuccess = false;
        //            resVal.ResponseMSG = HttpStatusCode.UnsupportedMediaType.ToString();
        //        }
        //        else
        //        {
        //            var provider = new FormDataStreamProvider(GetPath("~/Attachments/homework"));
        //            await Request.Content.ReadAsMultipartAsync(provider);

        //            string jsonData = provider.FormData["paraDataColl"];
        //            if (string.IsNullOrEmpty(jsonData))
        //                return BadRequest("No data found");

        //            AcademicLib.API.Student.AssignmentSubmit para = DeserializeObject<AcademicLib.API.Student.AssignmentSubmit>(jsonData);
        //            if (para == null)
        //            {
        //                return BadRequest("No form data found");
        //            }
        //            else if (para.AssignmentId == 0)
        //            {
        //                resVal.ResponseMSG = "Invalid Assignment";
        //            }
        //            else
        //            {
        //                para.UserId = UserId;
        //                if (provider.FileData.Count > 0)
        //                {
        //                    var DocumentColl = GetAttachmentDocuments(provider.FileData);
        //                    if (DocumentColl != null && DocumentColl.Count > 0)
        //                    {
        //                        para.AttachmentColl = DocumentColl;
        //                    }
        //                }
        //                resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).SubmitAssignment(para);
        //                //if (retVal.IsSuccess)
        //                //{
        //                //    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
        //                //    notification.Content = "New Assignment";
        //                //    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment);
        //                //    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment.ToString();
        //                //    notification.Heading = "Assignment";
        //                //    notification.Subject = "Assignment";
        //                //    notification.UserId = UserId;
        //                //    notification.UserName = User.Identity.Name;
        //                //    notification.UserIdColl = "";// retVal.ResponseId.Trim();

        //                //    resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName).SendNotification(UserId, notification);

        //                //    resVal.IsSuccess = true;
        //                //    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
        //                //}
        //                //else
        //                //   resVal = retVal;

        //            }
        //        }

        //        return Json(resVal, new JsonSerializerSettings
        //        {
        //            ContractResolver = new JsonContractResolver()
        //            {
        //                IsInclude = true,
        //                IncludeProperties = new List<string>
        //                         {
        //                           "IsSuccess","ResponseMSG"
        //                         }
        //            }
        //        });

        //    }
        //    catch (Exception ee)
        //    {
        //        return BadRequest(ee.Message);
        //    }


        //}



        // Post api/SubmitAssignment
        /// <summary>
        ///  Submit Assignment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SubmitAssignment()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/homework"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Student.AssignmentSubmit para = DeserializeObject<AcademicLib.API.Student.AssignmentSubmit>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (para.AssignmentId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Assignment";
                    }
                    else
                    {
                        para.UserId = UserId;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.AttachmentColl = DocumentColl;
                            }
                        }
                        string msg = "";
                        resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).SubmitAssignment(para, ref msg);
                        if (resVal.IsSuccess && !string.IsNullOrEmpty(resVal.ResponseId) && !string.IsNullOrEmpty(msg))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = msg;
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ASSIGNMENT);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ASSIGNMENT.ToString();
                            notification.Heading = "Assignment Submit";
                            notification.Subject = "Assignment Submit";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = resVal.ResponseId;
                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

                            if (!resVal.IsSuccess)
                            {
                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = resVal.ResponseMSG + " Error On Send Notification.";
                            }
                        }


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

        #endregion

        #region "Online Exam"
        // POST GetOnlineExamList
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineExam.ExamListCollections))]
        public IHttpActionResult GetOnlineExamList([FromBody] JObject para)
        {
            try
            {
                int? forType = null;

                if (para != null)
                {
                    if (para.ContainsKey("forType"))
                    {
                        forType = Convert.ToInt32(para["forType"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).getExamList(forType);

                if (!dataColl.IsSuccess)
                {
                    var resVal = new ResponeValues();
                    resVal.IsSuccess = dataColl.IsSuccess;
                    resVal.ResponseMSG = dataColl.ResponseMSG;
                    return Json(resVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //                {
                            //                    "ResponseMSG","IsSuccess","CategoryId","ExamModalType","OrderNo","CategoryName","Description"
                            //                }
                        }
                    });
                }
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CategoryId","ExamModalType","OrderNo","CategoryName","Description"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }

        // POST GetOnlineExamDetById
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineExam.ExamListCollections))]
        public IHttpActionResult GetOnlineExamDetById([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0;

                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                    {
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).getExamSetupByIdForAPI(examSetupId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CategoryId","ExamModalType","OrderNo","CategoryName","Description"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }


        // Post api/StartOnlineExam
        /// <summary>
        ///  Start Online Exam
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> StartOnlineExam()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/onlineexam"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.OnlineExam.StartOnlineExam para = DeserializeObject<AcademicLib.API.OnlineExam.StartOnlineExam>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }                
                    else
                    {
                        para.CUserId = UserId;
                        para.IPAddress = GetClientIp();
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.ImagePath = DocumentColl[0].DocPath;
                            }
                        }
                        resVal = new AcademicLib.BL.OnlineExam.OnlineExam(UserId, hostName, dbName).SaveFormData(para);

                        var retVal =new
                        {
                            IsSuccess=resVal.IsSuccess,
                            ResponseMSG=resVal.ResponseMSG,
                            StartDateTime=para.StartDateTime,
                            EndDateTime=para.EndDateTime
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                               
                            }
                        });
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

        // Post api/EndOnlineExam
        /// <summary>
        ///  End Online Exam
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> EndOnlineExam()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/onlineexam"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.OnlineExam.StartOnlineExam para = DeserializeObject<AcademicLib.API.OnlineExam.StartOnlineExam>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;
                        para.IPAddress = GetClientIp();
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.ImagePath = DocumentColl[0].DocPath;
                            }
                        }
                        resVal = new AcademicLib.BL.OnlineExam.OnlineExam(UserId, hostName, dbName).EndExam(para);
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

        // POST GetOnlineExamQuestion
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.QuestionSetupCollections))]
        public IHttpActionResult GetOnlineExamQuestion([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0;
                bool returnTypeAsList = true;
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                    {
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                    }
                    if (para.ContainsKey("returnTypeAsList"))
                    {
                        returnTypeAsList = Convert.ToBoolean(para["returnTypeAsList"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).getQuestionListForAPI(examSetupId,null);

             
                if (returnTypeAsList)
                {
                    return Json(dataColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = true,
                            IncludeProperties = new List<string>
                                        {
                                            "TranId","QNo","Marks","QuestionTitle","Question","QuestionPath","DetailsColl","SNo","Answer","FilePath","CategoryName","ExamModal","ResponseMSG","IsSuccess","SubmitType","QuestionRemarks","StudentAnswerNo","AnswerText","SNo_Str","FileType","FileCount","StudentDocsPath","StudentDocColl"
                                        }
                        }
                    });

                }
                else
                {
                    var returnVal = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };


                    return Json(returnVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = true,
                            IncludeProperties = new List<string>
                                        {
                                           "DataColl","IsSuccess","ResponseMSG", "TranId","QNo","Marks","QuestionTitle","Question","QuestionPath","DetailsColl","SNo","Answer","FilePath","CategoryName","ExamModal","ResponseMSG","IsSuccess","SubmitType","QuestionRemarks","StudentAnswerNo","AnswerText","SNo_Str","FileType","FileCount","StudentDocsPath","StudentDocColl"
                                        }
                        }
                    });

                }
              

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }

        // Post api/SubmitOEAnswer
        /// <summary>
        ///  Submit Online Exam Answer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SubmitOEAnswer()
        {
            string bFilePath = GetPath("~/Attachments/onlineexam");
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
                    var provider = new FormDataStreamProvider(bFilePath);
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.OnlineExam.StudentAnswer para = DeserializeObject<AcademicLib.API.OnlineExam.StudentAnswer>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;
                        para.IPAddress = GetClientIp();
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.AttachmentColl = DocumentColl;
                            }
                        }

                        if(para.StudentDocColl!=null && para.StudentDocColl.Count() > 0)
                        {
                            if (para.AttachmentColl == null)
                                para.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

                            string basePath = GetPath("~");
                            foreach(var v in para.StudentDocColl)
                            {
                                string file = basePath + v;
                                if (System.IO.File.Exists(file))
                                {
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                                    para.AttachmentColl.Add(new Dynamic.BusinessEntity.GeneralDocument()
                                    {
                                         Description="",
                                          DocPath=v,
                                           Name=fileInfo.Name,
                                            Extension=fileInfo.Extension                                             
                                    });
                                }
                            }
                        }

                        resVal = new AcademicLib.BL.OnlineExam.OnlineExam(UserId, hostName, dbName).SubmitAnswer(para);
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
        #endregion

        #region "MarkSheet"
        // POST GetObtainMark
        /// <summary>
        ///  Get GetObtainMark     
        ///  examTypeId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.AssignmentCollections))]
        public IHttpActionResult GetObtainMark([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para != null)
                {
                    int examTypeId = 0;
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    int? studentId = null;

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);

                    int academicYearId = 0;
                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);
                    else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);

                    if (academicYearId == 0)
                        academicYearId = this.AcademicYearId;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetClassWise(academicYearId, studentId, null, null, examTypeId,true,"");

                    if (dataColl.IsSuccess)
                    {
   
                        var retVal = new
                        {
                            ResponseMSG = dataColl.ResponseMSG,
                            IsSuccess = dataColl.IsSuccess,
                            DataColl = (dataColl.Count > 0 ? dataColl[0] : null)
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                            }
                        });
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = dataColl.ResponseMSG;
                    }


                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No Data Found";
                }


                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST v1/PrintMarkSheet
        /// <summary>        
        /// examTypeId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintMarkSheet([FromBody] JObject para)
        {
            int uid = UserId;
            string _host = hostName;
            string _db = dbName;
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int academicYearId = 0;

                
                int examTypeId = 0;
                if (para.ContainsKey("examTypeId"))
                    examTypeId = Convert.ToInt32(para["examTypeId"]);

                string format = "pdf";
                if (para.ContainsKey("format"))
                    format = Convert.ToString(para["format"]);

                if (para.ContainsKey("academicYearId"))
                    academicYearId = ToInt(para["AcademicYearId"]);
                else if (para.ContainsKey("AcademicYearId"))
                    academicYearId = ToInt(para["AcademicYearId"]);

                if (academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                if (examTypeId == 0)
                {
                    return BadRequest("Please ! Select Valid ExamType Name");
                }

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.Marksheet;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, 0);

                if (comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
                {
                    AcademicLib.API.Student.MarkSheetTemplate templateTran = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetTemplateTranId(academicYearId);
                    PivotalERP.Global.ReportTemplate reportTemplate = null;
                    if (templateTran == null || templateTran.MarkSheetId == 0)
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                    else
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false, templateTran.MarkSheetId);

                    if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");
                    }

                    Dynamic.BusinessEntity.Setup.CompanyDetail comDet1 = new Dynamic.BusinessEntity.Setup.CompanyDetail();
                    var abt = new AcademicLib.BL.AppCMS.Creation.AboutUs(uid, _host, dbName).getAbout(null);
                    var com = new Dynamic.DataAccess.Setup.CompanyDetailDB(_host, _db).getCompanyDetailsWithOutLogo(uid,comDet.BranchId);

                    comDet1.Name = comDet.CompanyName;
                    comDet1.Address = comDet.CompanyAddress;
                    comDet1.PanVatNo = comDet.PanVat;
                    comDet1.PhoneNo = comDet.PhoneNo;
                    comDet1.EmailId = comDet.EmailId;
                    comDet1.WebSite = comDet.WebSite;
                    comDet1.MailingName = com.MailingName;
                    comDet1.RegdNo = com.RegdNo;
                    comDet1.FaxNo = com.FaxNo;
                    comDet1.ZipCode = com.ZipCode;
                    comDet1.City = com.City;
                    comDet1.Zone = com.Zone;
                    comDet1.Country = com.Country;
                    comDet1.WebUrl = System.Web.HttpContext.Current.Server.MapPath("~");
                    comDet1.Affiliated = com.Affiliated;
                    comDet1.Slogan = com.Slogan;
                    comDet1.Established = com.Established;
                    comDet1.AffiliatedLogo = abt.AffiliatedLogo;

                    try
                    {
                        string logoPath = System.Web.HttpContext.Current.Server.MapPath(abt.LogoPath);
                        if (System.IO.File.Exists(logoPath))
                        {
                            comDet1.CompanyLogoPath = logoPath;
                            System.Drawing.Image img = System.Drawing.Image.FromFile(logoPath);
                            byte[] arr;
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                arr = ms.ToArray();
                            }
                            comDet1.Logo = arr;
                        }
                    }
                    catch { }
                    try
                    {
                        string affiliatedLogoPath = System.Web.HttpContext.Current.Server.MapPath(abt.AffiliatedLogo);
                        if (System.IO.File.Exists(affiliatedLogoPath))
                        {
                            comDet1.AffiliatedLogo = affiliatedLogoPath;
                        }
                    }
                    catch { }
                    Dynamic.BusinessEntity.Global.GlobalObject.CurrentCompany = comDet1;

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                     
                    paraColl.Add("UserId", UserId.ToString());
                    paraColl.Add("StudentId", "0");
                    paraColl.Add("ClassId", "0");
                    paraColl.Add("SectionId", "0");
                    paraColl.Add("ClassIdColl", "");
                    paraColl.Add("StudentIdColl", "");
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("AcademicYearId", academicYearId.ToString());
                    paraColl.Add("ExamTypeId", examTypeId.ToString());

                    var paraColl1 = GetObjectAsKeyVal(comDet1,paraColl.AllKeys);
                    paraColl.Add(paraColl1);

                    try
                    {
                        if (academicYearId != 0)
                        {
                            var academicYearBL = new AcademicLib.BL.Academic.Creation.AcademicYear(UserId, hostName, dbName);
                            var academicYear = academicYearBL.GetAcademicYearById(0, academicYearId);
                            if (academicYear != null)
                            {
                                paraColl.Add("AcademicYearName", academicYear.Name);
                                paraColl.Add("AcademicYearDisplayName", academicYear.Description);
                            }
                        }
                    }
                    catch { }

                    if (!string.IsNullOrEmpty(template.Path))
                    {
                        string reportFilePath = template.Path.Trim().ToLower();
                        //string reportFilePath = @"D:\OneDrive\live project\Academic 2.0\ReportTemplateSource\RptMarkSheet.rdlc";
                        //string reportFilePath = @"D:\OneDrive\live project\Academic 2.0\ReportTemplateSource\MainMarksheet.rdlc";
                        if (reportFilePath.Contains(".rdlc") || reportFilePath.Contains(".RDLC"))
                        {

                            //AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
                            //{
                            //    if (args.Exception is System.Runtime.InteropServices.SEHException)
                            //    {
                            //        // Suppress PInvokeStackImbalance for testing
                            //    }
                            //};

                            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetClassWise(academicYearId, null, null, null, examTypeId, true, "");                             
                            if (dataColl.IsSuccess)
                            {
                                Microsoft.Reporting.WebForms.Warning[] warnings;
                                string[] streamIds;
                                string contentType;
                                string encoding;
                                string extension;

                                Microsoft.Reporting.WebForms.ReportViewer reportViewer = new Microsoft.Reporting.WebForms.ReportViewer();
                                Microsoft.Reporting.WebForms.LocalReport report = new Microsoft.Reporting.WebForms.LocalReport();
                                reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                                reportViewer.LocalReport.ReportPath = reportFilePath;

                                Dynamic.BusinessEntity.Setup.ReportWriterParaCollections rptParaColl = new Dynamic.BusinessEntity.Setup.ReportWriterParaCollections();
                                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ExamTypeId", DefaultValue = examTypeId.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });

                                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamTypeId", examTypeId.ToString()));

                                var globlDB = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName);
                                var tmpDataColl = globlDB.getDataTable(UserId, "exec usp_PrintMarkSheet_Only @UserId=@UserId,@ExamTypeId=@ExamTypeId", rptParaColl);
                                Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", tmpDataColl);
                                reportViewer.LocalReport.DataSources.Clear();
                                reportViewer.LocalReport.DataSources.Add(datasource);
                                reportViewer.LocalReport.SetParameters(parameterColl);
                                reportViewer.LocalReport.SubreportProcessing += (sender, e) =>
                                {
                                    int sid = Convert.ToInt32(e.Parameters["StudentId"].Values[0]);
                                    int eid = Convert.ToInt32(e.Parameters["ExamTypeId"].Values[0]);
                                    rptParaColl = new Dynamic.BusinessEntity.Setup.ReportWriterParaCollections();
                                    rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "StudentId", DefaultValue = sid.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                                    rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ExamTypeId", DefaultValue = eid.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                                    var subTables = globlDB.getDataTable(UserId, "exec usp_SubMarkSheet @UserId=@UserId,@StudentId=@StudentId,@ExamTypeId=@ExamTypeId", rptParaColl);
                                    Microsoft.Reporting.WebForms.ReportDataSource subDS = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", subTables);
                                    e.DataSources.Add(subDS);
                                };
                                //Export the RDLC Report to Byte Array.

                                //  string renderFormat = (filenameToSave.EndsWith(".xlsx") ? "EXCELOPENXML" : "Excel");
                                byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                                string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                                string sFile = GetPath("~//" + basePath);
                                reportTemplate.SavePDF(bytes, sFile);
                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = basePath;
                                resVal.ResponseId = format;
                                Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                                printLog.UserId = UserId;
                                printLog.UserName = User.Identity.Name;
                                printLog.TranId = UserId;
                                printLog.AutoManualNo = examTypeId.ToString();
                                printLog.SystemUser = "API";
                                printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                                printLog.EntityId = entityId;
                                printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                                printLog.LogDate = DateTime.Now;
                                printLog.LogText = "Print Normal Marksheet of Student id=" + UserId.ToString();
                                printLog.MacAddress = "";
                                printLog.PCName = GetClientIp();
                                var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                            }
                            else
                            {                                
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = dataColl.ResponseMSG;
                            }

                        
                        }
                        else
                        {
                            Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                            _rdlReport.ComDet = comDet;
                            _rdlReport.ConnectionString = ConnectionString;
                            _rdlReport.RenderType = format;
                            _rdlReport.NoShow = false;

                            _rdlReport.ReportFile = reportTemplate.GetPath(template);

                            if (format == "html" || format == "htm")
                            {

                                string css = "";
                                if (_rdlReport.CSS != null)
                                    css = "<style type=text/css>" + _rdlReport.CSS + "</style>";

                                string js = "";
                                if (_rdlReport.JavaScript != null)
                                    js = "<script>" + _rdlReport.JavaScript + "</script>";

                                string htmlContent = js + "\n" + css + "\n" + _rdlReport.Html;

                                // Initialize NReco PdfGenerator
                                var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

                                // Configure PDF options
                                htmlToPdf.PageWidth = 210; // A4 width in mm
                                htmlToPdf.PageHeight = 297; // A4 height in mm
                                                            //  htmlToPdf.Margins = new PageMargins { Top = 10, Bottom = 10, Left = 10, Right = 10 };
                                                            //htmlToPdf.ExecutionTimeout = true; // Enable external resources (e.g., CSS, images)

                                // Generate PDF from HTML
                                byte[] pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

                                if (pdfBytes != null)
                                {
                                    string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                                    string sFile = GetPath("~//" + basePath);
                                    reportTemplate.SavePDF(pdfBytes, sFile);
                                    resVal.IsSuccess = true;
                                    resVal.ResponseMSG = basePath;
                                    resVal.ResponseId = format;
                                    Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                                    printLog.UserId = UserId;
                                    printLog.UserName = User.Identity.Name;
                                    printLog.TranId = UserId;
                                    printLog.AutoManualNo = examTypeId.ToString();
                                    printLog.SystemUser = "API";
                                    printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                                    printLog.EntityId = entityId;
                                    printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                                    printLog.LogDate = DateTime.Now;
                                    printLog.LogText = "Print Normal Marksheet of Student id=" + UserId.ToString();
                                    printLog.MacAddress = "";
                                    printLog.PCName = GetClientIp();
                                    var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                                }
                            }
                            else
                            {
                                if (_rdlReport.Object != null)
                                {
                                    string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                                    string sFile = GetPath("~//" + basePath);
                                    reportTemplate.SavePDF(_rdlReport.Object, sFile);
                                    resVal.IsSuccess = true;
                                    resVal.ResponseMSG = basePath;
                                    resVal.ResponseId = format;
                                    Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                                    printLog.UserId = UserId;
                                    printLog.UserName = User.Identity.Name;
                                    printLog.TranId = UserId;
                                    printLog.AutoManualNo = examTypeId.ToString();
                                    printLog.SystemUser = "API";
                                    printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                                    printLog.EntityId = entityId;
                                    printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                                    printLog.LogDate = DateTime.Now;
                                    printLog.LogText = "Print Normal Marksheet of Student id=" + UserId.ToString();
                                    printLog.MacAddress = "";
                                    printLog.PCName = GetClientIp();
                                    var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                                }
                                else
                                {
                                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetClassWise(academicYearId, null, null, null, examTypeId, true, "");

                                    string str = "";
                                    if (dataColl.IsSuccess)
                                    {
                                        foreach (var err in _rdlReport.Errors)
                                        {
                                            str = str + err.ToString() + "\n";
                                        }
                                    }
                                    else
                                    {
                                        str = dataColl.ResponseMSG;
                                    }
                                    str = str.Replace("'", "");
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = str;
                                }
                            }

                        }
                    }
               

                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = comDet.ResponseMSG;
                }
                 
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "IsSuccess","ResponseMSG","ResponseId"
                    }
                }
            });
        }
        #endregion

        #region "Group MarkSheet"
        // POST GetObtainMarkGroup
        /// <summary>
        ///  Get ObtainMarkGroup     
        ///  examTypeGroupId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.AssignmentCollections))]
        public IHttpActionResult GetObtainMarkGroup([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para != null)
                {
                    int examTypeGroupId = 0;
                    if (para.ContainsKey("examTypeGroupId") && para["examTypeGroupId"] != null)
                        examTypeGroupId = Convert.ToInt32(para["examTypeGroupId"]);

                    if(examTypeGroupId==0)
                    {
                        if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                            examTypeGroupId = Convert.ToInt32(para["examTypeId"]);
                    }
                    int? studentId = null;

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);

                    int academicYearId = 0;
                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);
                    else  if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);

                    if (academicYearId == 0)
                        academicYearId = this.AcademicYearId;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getGroupMarkSheetClassWise(academicYearId, studentId, null, null, examTypeGroupId,true,null);

                    if (dataColl.IsSuccess)
                    {
                        var retVal = new
                        {
                            ResponseMSG = dataColl.ResponseMSG,
                            IsSuccess = dataColl.IsSuccess,
                            DataColl = (dataColl.Count > 0 ? dataColl[0] : null)
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                            }
                        });
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = dataColl.ResponseMSG;
                    }
                     

                }


                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST v1/PrintGroupMarkSheet
        /// <summary>        
        /// examTypeId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintGroupMarkSheet([FromBody] JObject para)
        {
            int uid = UserId;
            string _host = hostName;
            string _db = dbName;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int academicYearId = 0;
                if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                    academicYearId = ToInt(para["AcademicYearId"]);
                else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                    academicYearId = ToInt(para["AcademicYearId"]);

                if (academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                int examTypeGroupId = 0;
                int curExamTypeId = 0;
                if (para.ContainsKey("curExamTypeId") && para["curExamTypeId"] != null)
                    curExamTypeId = Convert.ToInt32(para["curExamTypeId"]);

                if (para.ContainsKey("examTypeGroupId"))
                    examTypeGroupId = Convert.ToInt32(para["examTypeGroupId"]);

                if (examTypeGroupId == 0)
                {
                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        examTypeGroupId = Convert.ToInt32(para["examTypeId"]);
                }

                string format = "pdf";
                if (para.ContainsKey("format"))
                    format = Convert.ToString(para["format"]);

                if (examTypeGroupId == 0)
                {
                    return BadRequest("Please ! Select Valid ExamType Name");
                }

                if (para.ContainsKey("academicYearId"))
                    academicYearId = ToInt(para["AcademicYearId"]);
                else if (para.ContainsKey("AcademicYearId"))
                    academicYearId = ToInt(para["AcademicYearId"]);

                if (academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.GroupMarksheet;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, 0);

                if (comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
                {
                    AcademicLib.API.Student.MarkSheetTemplate templateTran = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetTemplateTranId(this.AcademicYearId);
                    PivotalERP.Global.ReportTemplate reportTemplate = null;
                    if (templateTran == null || templateTran.GroupMarkSheetId == 0)
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                    else
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false, templateTran.GroupMarkSheetId);

                    if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");
                    }

                    Dynamic.BusinessEntity.Setup.CompanyDetail comDet1 = new Dynamic.BusinessEntity.Setup.CompanyDetail();
                    var abt = new AcademicLib.BL.AppCMS.Creation.AboutUs(uid, _host, dbName).getAbout(null);
                    var com = new Dynamic.DataAccess.Setup.CompanyDetailDB(_host,_db).getCompanyDetailsWithOutLogo(uid,comDet.BranchId);

                    comDet1.Name = comDet.CompanyName;
                    comDet1.Address = comDet.CompanyAddress;
                    comDet1.PanVatNo = comDet.PanVat;
                    comDet1.PhoneNo = comDet.PhoneNo;
                    comDet1.EmailId = comDet.EmailId;
                    comDet1.WebSite = comDet.WebSite;
                    comDet1.MailingName = com.MailingName;
                    comDet1.RegdNo = com.RegdNo;
                    comDet1.FaxNo = com.FaxNo;
                    comDet1.ZipCode = com.ZipCode;
                    comDet1.City = com.City;
                    comDet1.Zone = com.Zone;
                    comDet1.Country = com.Country;
                    comDet1.WebUrl = System.Web.HttpContext.Current.Server.MapPath("~");
                    comDet1.Affiliated = com.Affiliated;
                    comDet1.Slogan = com.Slogan;
                    comDet1.Established = com.Established;
                    comDet1.AffiliatedLogo = abt.AffiliatedLogo;

                    try
                    {
                        string logoPath = System.Web.HttpContext.Current.Server.MapPath(abt.LogoPath);
                        if (System.IO.File.Exists(logoPath))
                        {
                            comDet1.CompanyLogoPath = logoPath;
                            System.Drawing.Image img = System.Drawing.Image.FromFile(logoPath);
                            byte[] arr;
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                arr = ms.ToArray();
                            }
                            comDet1.Logo = arr;
                        }
                    }
                    catch { }

                    try
                    {
                        string affiliatedLogoPath = System.Web.HttpContext.Current.Server.MapPath(abt.AffiliatedLogo);
                        if (System.IO.File.Exists(affiliatedLogoPath))
                        {
                            comDet1.AffiliatedLogo = affiliatedLogoPath;
                        }
                    }
                    catch { }

                    Dynamic.BusinessEntity.Global.GlobalObject.CurrentCompany = comDet1;

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                    paraColl.Add("UserId", UserId.ToString());
                    paraColl.Add("StudentId", "0");
                    paraColl.Add("ClassId", "0");
                    paraColl.Add("SectionId", "0");
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("ExamTypeGroupId", examTypeGroupId.ToString());
                    paraColl.Add("CurExamTypeId", curExamTypeId.ToString());
                    paraColl.Add("AcademicYearId", academicYearId.ToString());

                    var paraColl1 = GetObjectAsKeyVal(comDet1, paraColl.AllKeys);
                    paraColl.Add(paraColl1);
                    try
                    {
                        if (academicYearId != 0)
                        {
                            var academicYearBL = new AcademicLib.BL.Academic.Creation.AcademicYear(UserId, hostName, dbName);
                            var academicYear = academicYearBL.GetAcademicYearById(0, academicYearId);
                            if (academicYear != null)
                            {
                                paraColl.Add("AcademicYearName", academicYear.Name);
                                paraColl.Add("AcademicYearDisplayName", academicYear.Description);
                            }
                        }
                    }
                    catch { }
                    Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                    _rdlReport.ComDet = comDet;
                    _rdlReport.ConnectionString = ConnectionString;
                    _rdlReport.RenderType = format;
                    _rdlReport.NoShow = false;

                    if (!string.IsNullOrEmpty(template.Path))
                    {

                        _rdlReport.ReportFile = reportTemplate.GetPath(template);

                        if (_rdlReport.Object != null)
                        {
                            string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                            string sFile = GetPath("~//" + basePath);
                            reportTemplate.SavePDF(_rdlReport.Object, sFile);
                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = basePath;
                            resVal.ResponseId = format;
                            Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                            printLog.UserId = UserId;
                            printLog.UserName = User.Identity.Name;
                            printLog.TranId = UserId;
                            printLog.AutoManualNo = examTypeGroupId.ToString();
                            printLog.SystemUser = "API";
                            printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                            printLog.EntityId = entityId;
                            printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                            printLog.LogDate = DateTime.Now;
                            printLog.LogText = "Print Group Marksheet of Student id=" + UserId.ToString();
                            printLog.MacAddress = "";
                            printLog.PCName = GetClientIp();
                            var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                        }
                        else
                        {
                            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getGroupMarkSheetClassWise(academicYearId, null, null, null, examTypeGroupId, true, null);

                            string str = "";
                            if (dataColl.IsSuccess)
                            {
                                foreach (var err in _rdlReport.Errors)
                                {
                                    str = str + err.ToString() + "\n";
                                }
                            }
                            else
                            {
                                str = dataColl.ResponseMSG;
                            }

                            str = str.Replace("'", "");
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = str;
                        }

                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = comDet.ResponseMSG;
                }

                 
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "IsSuccess","ResponseMSG","ResponseId"
                    }
                }
            });
        }
        #endregion

        #region "GetFeeDetails"

        // POST GetFeeDetails
        /// <summary>
        ///  Get FeeDetails             
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Student.Fee))]
        public IHttpActionResult GetFeeDetails([FromBody] JObject para)
        {
            AcademicLib.API.Student.Fee fee = new AcademicLib.API.Student.Fee();
            try
            {
                int AcademicYearId;
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                fee = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).getStudentFee(AcademicYearId);

                fee.ResponseId = "0";

                return Json(fee, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST GetFeeSummary
        /// <summary>
        ///  Get FeeSummary             
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(AcademicLib.API.Student.Fee))]
        public IHttpActionResult GetFeeSummary(int? AcademicYearId=null)
        {
            AcademicLib.API.Student.Fee fee = new AcademicLib.API.Student.Fee();
            try
            {

                if (!AcademicYearId.HasValue || AcademicYearId == 0)
                    AcademicYearId = this.AcademicYearId;

                fee = new AcademicLib.BL.Fee.Transaction.FeeReceipt(UserId, hostName, dbName).getStudentFeeForWallet(AcademicYearId.Value);

                return Json(fee, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","Opening","FeeAmt","DiscountAmt","PaidAmt","DuesAmt","MonthId","MonthName"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST v1/PrintFeeReceipt
        /// <summary>        
        /// tranId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintFeeReceipt([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int tranId = 0;
                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);

                if (tranId == 0)
                {
                    return BadRequest("No Receipt found");
                }

                int entityId = (int)Dynamic.BusinessEntity.Global.FormsEntity.FeeReceipt;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, tranId);

                if (comDet!=null && !string.IsNullOrEmpty(comDet.CompanyName))
                {
                    var classWiseTemplate = new AcademicLib.BL.Fee.Setup.FeeConfiguration(UserId, hostName, dbName).getRecTemplateTranId(entityId, this.AcademicYearId);

                    int temlateTranId = ((classWiseTemplate != null && classWiseTemplate.ReceiptTemplateId.HasValue) ? classWiseTemplate.ReceiptTemplateId.Value : 0);

                    PivotalERP.Global.ReportTemplate reportTemplate = null;
                    if(temlateTranId>0)
                        reportTemplate= new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, true,temlateTranId);
                    else
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, true);

                    if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");
                    }

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                    paraColl.Add("UserId", UserId.ToString());
                    paraColl.Add("TranId", tranId.ToString());
                    paraColl.Add("UserName", User.Identity.Name);
                    Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                    _rdlReport.ComDet = comDet;
                    _rdlReport.ConnectionString = ConnectionString;
                    _rdlReport.RenderType = "pdf";
                    _rdlReport.NoShow = false;

                    if (!string.IsNullOrEmpty(template.Path))
                    {

                        _rdlReport.ReportFile = reportTemplate.GetPath(template);

                        if (_rdlReport.Object != null)
                        {
                            string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                            string sFile = GetPath("~//" + basePath);
                            reportTemplate.SavePDF(_rdlReport.Object, sFile);
                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = basePath;

                            Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                            printLog.UserId = UserId;
                            printLog.UserName = User.Identity.Name;
                            printLog.TranId = tranId;
                            printLog.AutoManualNo = tranId.ToString();
                            printLog.SystemUser = "StudentAPI";
                            printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                            printLog.EntityId = entityId;
                            printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                            printLog.LogDate = DateTime.Now;
                            printLog.LogText = "Print Voucher of tranid=" + tranId.ToString();
                            printLog.MacAddress = "";
                            printLog.PCName = "";
                            var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                        }
                        else
                        {
                            string str = "";
                            foreach (var err in _rdlReport.Errors)
                            {
                                str = str + err.ToString() + "\n";
                            }
                            str = str.Replace("'", "");
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = str;
                        }
                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = comDet.ResponseMSG;
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
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
        #endregion

        #region "Print AdmitCard"

        // POST v1/PrintAdmitCardSheet
        /// <summary>        
        /// examTypeId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintAdmitCardSheet([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int examTypeId = 0;
                if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                    examTypeId = Convert.ToInt32(para["examTypeId"]);
            

                string format = "pdf";
                if (para.ContainsKey("format"))
                    format = Convert.ToString(para["format"]);

                if (examTypeId == 0)
                {
                    return BadRequest("Please ! Select Valid ExamType Name");
                }

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.AdmitCard;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, 0);

                if (comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
                {
                    PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                    if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");
                    }

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                    paraColl.Add("UserId", UserId.ToString());
                    paraColl.Add("StudentId", "0");
                    paraColl.Add("ClassId", "0");
                    paraColl.Add("SectionId", "0");
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("ExamTypeId", examTypeId.ToString());
                    Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                    _rdlReport.ComDet = comDet;
                    _rdlReport.ConnectionString = ConnectionString;
                    _rdlReport.RenderType = format;
                    _rdlReport.NoShow = false;

                    if (!string.IsNullOrEmpty(template.Path))
                    {
                        string fpath = reportTemplate.GetPath(template);
                        if (!System.IO.File.Exists(fpath))
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "AdmitCard Template Not Found.";                            
                        }
                        else
                        {
                            _rdlReport.ReportFile = fpath;

                            if (_rdlReport.Object != null)
                            {
                                string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                                string sFile = GetPath("~//" + basePath);
                                reportTemplate.SavePDF(_rdlReport.Object, sFile);
                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = basePath;
                                resVal.ResponseId = format;
                                Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                                printLog.UserId = UserId;
                                printLog.UserName = User.Identity.Name;
                                printLog.TranId = UserId;
                                printLog.AutoManualNo = examTypeId.ToString();
                                printLog.SystemUser = "API";
                                printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                                printLog.EntityId = entityId;
                                printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                                printLog.LogDate = DateTime.Now;
                                printLog.LogText = "Print Normal AdmitCard of Student id=" + UserId.ToString();
                                printLog.MacAddress = "";
                                printLog.PCName = GetClientIp();
                                var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
                            }
                            else
                            {
                                string str = "";
                                foreach (var err in _rdlReport.Errors)
                                {
                                    str = str + err.ToString() + "\n";
                                }
                                str = str.Replace("'", "");
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = str;
                            }

                        }

                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = comDet.ResponseMSG;
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "IsSuccess","ResponseMSG","ResponseId"
                    }
                }
            });
        }

        #endregion

        #region "Transport"
        // POST PickupPoint
        /// <summary>
        ///  Get PickupPoint 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Student.TransportPoint))]
        public IHttpActionResult GetPickupPoint()
        {
            try
            {
                var routeList = new AcademicLib.BL.Transport.Creation.TransportPoint(UserId, hostName, dbName).getPickupPoint();

                return Json(routeList, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ClassList","SectionList","ClassId","SectionId","ClassName","SectionName","ResponseMSG","IsSuccess","Name"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }

        #endregion

        #region "Student Remarks"


       
        // POST Get Student Remarks List
        /// <summary>
        ///  Get Student Remarks List
        ///  studentId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.StudentRemarks))]
        public IHttpActionResult GetStudentRemarks([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.StudentRemarksCollections classColl = new AcademicLib.RE.Academic.StudentRemarksCollections();
            try
            {

                int AcademicYearId;
                if (para != null)
                {

                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                classColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(UserId, hostName, dbName).getRemarksList(AcademicYearId, DateTime.Today, DateTime.Today, null, null,true);
                return Json(classColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST Get My Remarks List
        /// <summary>
        ///  Get My Remarks List        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.StudentRemarks))]
        public IHttpActionResult GetMyRemarks([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.StudentRemarksCollections classColl = new AcademicLib.RE.Academic.StudentRemarksCollections();
            try
            {
                int AcademicYearId;
                if (para != null)
                {

                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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
                classColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(UserId, hostName, dbName).getRemarksList(AcademicYearId, DateTime.Today, DateTime.Today, null, null, true);
                return Json(classColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        // POST GetExamConfiguration
        /// <summary>
        ///  Get ExamConfiguration                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Setup.ExamConfigForApp))]
        public IHttpActionResult GetExamConfiguration()
        {
            AcademicLib.BE.Exam.Setup.Configuration returnVal = new AcademicLib.BE.Exam.Setup.Configuration();

            try
            {
                returnVal = new AcademicLib.BL.Exam.Setup.Configuration(UserId, hostName, dbName).GetConfiguration(0);

                var config = new {
                    IsSuccess = returnVal.IsSuccess,
                    ResponseMSG=returnVal.ResponseMSG,
                    MarkType =  returnVal.ExamConfigForAppList[0].MarkType,
                    MinDues= returnVal.ExamConfigForAppList[0].MinDues,
                    StudentRankAs=returnVal.StudentRankAs
                };

                return Json(config, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;

                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }

           
        }


        // POST TodayQuotes
        /// <summary>
        ///  Get GetTodayQuotes                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Quotes))]
        public IHttpActionResult GetTodayQuotes()
        {
            AcademicLib.BE.AppCMS.Creation.Quotes returnVal = new AcademicLib.BE.AppCMS.Creation.Quotes();

            try
            {
                returnVal = new AcademicLib.BL.AppCMS.Creation.Quotes(UserId, hostName, dbName).getTodayQuotes();

                var config = new
                {
                    IsSuccess = returnVal.IsSuccess,
                    ResponseMSG = returnVal.ResponseMSG,
                    Message = returnVal.Title,
                    ImagePath = returnVal.ImagePath                    
                };

                return Json(config, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;

                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }


        }


        // POST TodayQuotes
        /// <summary>
        ///  Get GetTodayQuotes                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Quotes))]
        public IHttpActionResult GetSiblingDetails([FromBody] JObject para)
        {
            AcademicLib.API.Student.SiblingDetailCollections returnVal = new AcademicLib.API.Student.SiblingDetailCollections();

            try
            {
                int AcademicYearId;
                if (para != null)
                {

                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                returnVal = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getStudentSiblingDetails(0, AcademicYearId);

                var config = new
                {
                    IsSuccess = returnVal.IsSuccess,
                    ResponseMSG = returnVal.ResponseMSG,
                    DataColl = returnVal
                };

                return Json(config, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                returnVal.IsSuccess = false;
                returnVal.ResponseMSG = ee.Message;

                return Json(returnVal, new JsonSerializerSettings
                {
                });
            }


        }



        // POST GetClassSchedule
        /// <summary>
        ///  Get ClassSchedule     
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.ClassScheduleCollections))]
        public IHttpActionResult GetAttendance([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.ClassScheduleCollections scheduleColl = new AcademicLib.RE.Academic.ClassScheduleCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int? yearId=null, monthId = null, subjectId = null,studentId=null;
                int AcademicYearId;
                int baseDate = (int)BaseDate.NepaliDate;
                //baseDate= 0-AD,1=BS
                if (para != null)
                {
                    if (para.ContainsKey("AcademicYearId"))
                    {
                        try
                        {
                            AcademicYearId = ToInt(para["AcademicYearId"]);
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

                    if (para.ContainsKey("baseDate"))
                        baseDate = Convert.ToInt32(para["baseDate"]);

                    if (para.ContainsKey("yearId"))
                        yearId = Convert.ToInt32(para["yearId"]);

                    if (para.ContainsKey("monthId"))
                        monthId = Convert.ToInt32(para["monthId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    if (para.ContainsKey("studentId"))
                        studentId = Convert.ToInt32(para["studentId"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }

                if (!studentId.HasValue)
                    studentId = 0;

                var dataColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getStudentBIOAttendance(AcademicYearId, studentId.Value, dateFrom, dateTo,yearId,monthId,subjectId);

                int TotalPresent = dataColl.Where(p1 => p1.IsPresent == true).Count();
                int TotalHoliday = dataColl.Where(p1 => p1.IsHoliday == true).Count();
                int TotalWeekEnd = dataColl.Where(p1 => p1.IsWeekEnd == true).Count();                
                int TotalLeave= dataColl.Where(p1 => p1.OnLeave == true).Count();
                int TotalEvent = dataColl.Where(p1 => p1.IsEvent == true).Count();
                int TotalDays = dataColl.Count > 0 ? dataColl.First().TotalDays : 0;
                int TotalAbsent = TotalDays - TotalPresent - TotalWeekEnd - TotalHoliday;                
                var config = new
                {
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG,
                    DataColl = dataColl,
                    TotalPresent=TotalPresent,
                    TotalHoliday=TotalHoliday,
                    TotalWeekEnd=TotalWeekEnd,
                    TotalDays=TotalDays,
                    TotalAbsent=TotalAbsent,
                    TotalLeave= TotalLeave,
                    TotalEvent=TotalEvent,
                    PhotoPath=dataColl.Count>0 ? dataColl[0].PhotoPath : ""
                };

                return Json(config, new JsonSerializerSettings
                {
                });
                 
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST GetLMS
        /// <summary>
        ///  Get LMS                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.LessonPlan))]
        public IHttpActionResult GetLMS()
        {
            AcademicLib.BE.Academic.Transaction.LessonPlanCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlanCollections();

            try
            {
                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonPlanByClass(null, null, null, null, 4,null,null,null);

                var config = new
                {
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG,
                    DataColl= dataColl
                };

                return Json(config, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

                return Json(dataColl, new JsonSerializerSettings
                {
                });
            }


        }

        // POST GetLeaveReq
        /// <summary>     
        /// Get Student Leave Request List
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetLeaveReq([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    int? studentId = null;
                    DateTime? dateFrom = null, dateTo = null;
                    int leaveStatus = 0;

                    int? classId = null, sectionId = null;

                    if (para.ContainsKey("leaveStatus"))
                        leaveStatus = Convert.ToInt32(para["leaveStatus"]);

                    if (para.ContainsKey("studentId"))
                        studentId = Convert.ToInt32(para["studentId"]);

                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    var dataColl = new AcademicLib.BL.Attendance.LeaveRequest(UserId, hostName, dbName).getStudentLeaveRequestLst(dateFrom, dateTo, leaveStatus, studentId,classId,sectionId,this.AcademicYearId);

                    var retData = new
                    {
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG,
                        LeaveColl = dataColl
                    };
                    return Json(retData, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //            {
                            //                "ResponseMSG","IsSuccess","RId"
                            //            }
                        }
                    });


                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","RId"
                                        }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }



        // POST TCCC_Request
        /// <summary>
        /// Request For TC,CC
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult TCCC_Request([FromBody] AcademicLib.BE.Academic.Transaction.TCCCRequest para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {              
                if (para != null)
                {
                    int uid = 0;
                    if (User.Identity.IsAuthenticated)
                        uid = UserId;
                    else
                    {
                        if(para.PassKey==base.PassKey)
                            uid = 2;
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Invalid PassKey";
                        }
                    }

                    if (uid > 0)
                    {
                        para.IPAddress = GetClientIp();
                        para.UserAgent = Request.Headers.UserAgent.ToString();
                        para.Browser = Request.Headers.Host;
                        resVal = new AcademicLib.BL.Academic.Transaction.TC(uid, hostName, dbName).Request(para);
                    }
                        
                    
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Data";
                }

                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess" 
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // Post api/ContinuousConfirmation
        /// <summary>
        ///  Save ContinuousConfirmation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> ContinuousConfirmation()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/onlineexam"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.BE.Academic.Transaction.ContinuousConfirmation para = DeserializeObject<AcademicLib.BE.Academic.Transaction.ContinuousConfirmation>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;

                        if (!para.TranId.HasValue)
                            para.TranId = 0;

                        //if (provider.FileData.Count > 0)
                        //{
                        //    var DocumentColl = GetAttachmentDocuments(provider.FileData);
                        //    if (DocumentColl != null && DocumentColl.Count > 0)
                        //    {
                        //        para.ImagePath = DocumentColl[0].DocPath;
                        //    }
                        //}
                        resVal = new AcademicLib.BL.Academic.Transaction.ContinuousConfirmation(UserId, hostName, dbName).SaveFormData(para);

                        var retVal = new
                        {
                            IsSuccess = resVal.IsSuccess,
                            ResponseMSG = resVal.ResponseMSG,                          
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {

                            }
                        });
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


        // POST GetTodayLessonPlan
        /// <summary>
        ///  Get TodayLessonPlan                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetTodayLessonPlan([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.TodayLessonPlanCollections dataColl = new AcademicLib.RE.Academic.TodayLessonPlanCollections();
            try
            {
                DateTime? forDate = DateTime.Today;
                int? classId = null, sectionId = null, subjectId = null, employeeId = null, BatchId = null, SemesterId = null, ClassYearId = null;
                int reportType = 1;

                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    if (para.ContainsKey("forDate"))
                        forDate = Convert.ToDateTime(para["forDate"]);

                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    if (para.ContainsKey("employeeId"))
                        employeeId = Convert.ToInt32(para["employeeId"]);

                    if (para.ContainsKey("reportType"))
                        reportType = Convert.ToInt32(para["reportType"]);

                    if (para.ContainsKey("batchId"))
                        BatchId = Convert.ToInt32(para["batchId"]);

                    if (para.ContainsKey("semesterId"))
                        SemesterId = Convert.ToInt32(para["semesterId"]);

                    if (para.ContainsKey("classYearId"))
                        ClassYearId = Convert.ToInt32(para["classYearId"]);

                    dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getTodayLessonPlan(this.AcademicYearId, forDate, classId, sectionId, subjectId, employeeId, reportType, BatchId, SemesterId, ClassYearId);

                }

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","IsSuccess","ResponseMSG","LessonId","LessonSNo","TopicSNo","ContentSNo","SubjectName","CoverFilePath","LessonName","TopicName","ForDate","ForMiti","FileName","FilePath","Contents","StartDate",
                        "EndDate","StartRemarks","EndRemarks","StartBy","StartMiti","EndMiti","EmpName","EmpCode","Status","StatusValue","TotalDays","ClassName","Batch","Semester","ClassYear"
                    }
                }
            });
        }

        #region "Reading Materials"

        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetClassSubWiseData([FromBody] JObject para)
        {
            AcademicLib.BE.ReadingMaterials.ReadingMaterials dataColl = new AcademicLib.BE.ReadingMaterials.ReadingMaterials();
            try
            {
                int subjectId = 0;

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                dataColl = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).GetClassSubWiseData(0, 0, 0, 0, subjectId, 0);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","ReadMatContentColl","ReadMatContId","BatchId","ClassId","SubjectId","Title",
                        "ReadMadVideoColl","ReadMatVideoId","BatchId","ClassId","SubjectId","VideoTitle","VideoLink","Remarks"
                     }
                }
            });
        }

        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetReadMatContById([FromBody] JObject para)
        {
            AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent data = new AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent();
            try
            {
                int readMatContId = 0;

                if (para.ContainsKey("readMatContId"))
                    readMatContId = ToInt(para["readMatContId"]);

                data = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).GetReadMatContById(0, readMatContId);
            }
            catch (Exception ee)
            {
                data.IsSuccess = false;
                data.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = data.ResponseMSG,
                IsSuccess = data.IsSuccess,
                DataColl = data
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","ReadMatContId","BatchId","ClassId","SubjectId","Title","SemesterId","ClassYearId","SemesterName","ClassYearName","BatchName","ClassName","SubjectName",
                        "ContentAttach","Id","Name","DocPath","Extension"

                     }
                }
            });
        }
        #endregion

        #region "E-Contain"

        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult getLessonPlanForUpdate([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            try
            {
                int subjectId = 0, AcademicYearId= this.AcademicYearId;

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                if (para.ContainsKey("AcademicYearId"))
                    AcademicYearId = ToInt(para["AcademicYearId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).getLessonPlanForUpdate(0, subjectId, 0, 0, 0, 0,AcademicYearId);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","TranId","ClassId","SubjectId","NoOfLesson","CoverFilePath",
                        "DetailsColl","tranId","LessonId","SNo","LessonName","PlanStartDate_AD","PlanEndDate_AD","PlanStartDate_BS","PlanEndDate_BS","Status","StatusValue","TotalDays",
                        "TopicColl","LessonId","SNo","TopicName","StartDate_AD","EndDate_AD","StartDate_BS","EndDate_BS","StartRemarks","EndRemarks","StartBy","EmpName","EmpCode","Status","StatusValue","TotalDays","StatusDays",
                        "ContentsColl","LessonId","LessonSNo","TopicSNo","SNo","ForDate","Contents","ForDateBS","StartDate_AD","EndDate_AD","StartRemarks","EndRemarks","StartBy","StartDate_BS","EndDate_BS","EmpName","EmpCode","Status","StatusValue","TotalDays"

                     }
                }
            });
        }



        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLessonTopicContents([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections();
            try
            {
                int lessonSNo = 0, topicSNo = 0, subjectId = 0;

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = ToInt(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = ToInt(para["topicSNo"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTopicContent(lessonSNo, topicSNo, 0, subjectId, 0, 0, 0);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","LessonSNo","FileName","FilePath","FileType"

                     }
                }
            });
        }


        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLessonTopicVideo([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections();
            try
            {
                int lessonSNo = 0, topicSNo = 0, subjectId = 0;

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = ToInt(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = ToInt(para["topicSNo"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTopicVideo(lessonSNo, topicSNo, 0, subjectId, 0, 0, 0);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","LessonSNo","Title","Link","Remarks"

                     }
                }
            });
        }
        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLessonTopicQuizList([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicQuizCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicQuizCollections();
            try
            {
                int lessonSNo = 0, topicSNo = 0, subjectId = 0;

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = ToInt(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = ToInt(para["topicSNo"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).GetLessonTopicQuizList(lessonSNo, topicSNo, 0, subjectId, 0, 0, 0);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = dataColl.ResponseMSG,
                IsSuccess = dataColl.IsSuccess,
                DataColl = dataColl
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                    {
                        "DataColl","ResponseMSG","IsSuccess","QuizId","Topic","Description","NoOfQuestion","FullMark","PassMark","Duration"

                     }
                }
            });
        }
        #endregion
    }
}
