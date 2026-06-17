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
using System.Web;

namespace AcademicERP.Controllers
{
    public class TeacherController : APIBaseController
    {
        #region "Profile"

        // POST GetEmployeeProfile
        /// <summary>
        ///  Get GetEmployeeProfile                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Teacher.EmployeeProfile))]
        public IHttpActionResult GetEmployeeProfile()
        {
            AcademicLib.API.Teacher.EmployeeProfile profile = new AcademicLib.API.Teacher.EmployeeProfile();
            try
            {
                var empBL = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName);
                profile = empBL.getEmployeeForApp(null);
                var emp = empBL.getEmployeeById(0, profile.EmployeeId);

                profile.BankAccountNo = emp.BA_AccountNo;
                profile.PasswordNo = emp.PasswordNo;
                profile.PasswordExpiryDate = emp.PasswordExpiryDate;
                profile.PasswordIssueDate = emp.PasswordIssueDate;
                profile.PasswordIssuePlace = emp.PasswordIssuePlace;
                
                profile.LI_InsuranceType = emp.LI_InsuranceType;
                profile.LI_InsuranceCompany = emp.LI_InsuranceCompany;
                profile.LI_PolicyName = emp.LI_PolicyName;
                profile.LI_PolicyNo = emp.LI_PolicyNo;
                profile.LI_PolicyAmount = emp.LI_PolicyAmount;
                profile.LI_PolicyStartDate = emp.LI_PolicyStartDate;
                profile.LI_PolicyLastDate = emp.LI_PolicyLastDate;
                profile.LI_PremiunAmount = emp.LI_PremiunAmount;
                profile.LI_PaymentType = emp.LI_PaymentType;
                profile.LI_StartMonth = emp.LI_StartMonth;
                profile.LI_IsDeductFromSalary = emp.LI_IsDeductFromSalary;
                profile.LI_Remarks = emp.LI_Remarks;
                profile.HI_InsurenceType = emp.HI_InsurenceType;
                profile.HI_InsuranceCompany = emp.HI_InsuranceCompany;
                profile.HI_PolicyName = emp.HI_PolicyName;
                profile.HI_PolicyNo = emp.HI_PolicyNo;
                profile.HI_PolicyAmount = emp.HI_PolicyAmount;
                profile.HI_PolicyStartDate = emp.HI_PolicyStartDate;
                profile.HI_PolicyLastDate = emp.HI_PolicyLastDate;
                profile.HI_PremiumAmount = emp.HI_PremiumAmount;
                profile.HI_PaymentType = emp.HI_PaymentType;
                profile.HI_StartMonth = emp.HI_StartMonth;
                profile.HI_IsDeductFromSalary = emp.HI_IsDeductFromSalary;
                profile.HI_Remarks = emp.HI_Remarks;
                profile.MotherTonque = emp.MotherTonque;
                profile.Rank = emp.Rank;
                profile.Position = emp.Position;
                profile.TeacherType = emp.TeacherType;
                profile.TeachingLanguage = emp.TeachingLanguage;
                profile.LicenseNo = emp.LicenseNo;
                profile.TrkNo = emp.TrkNo;
                profile.PFAccountNo = emp.PFAccountNo;

                profile.BankName = emp.BankName;
                profile.BA_AccountName = emp.BA_AccountName;
                profile.BA_AccountNo = emp.BA_AccountNo;
                profile.BA_Branch = emp.BA_Branch;
                profile.BA_IsForPayroll = emp.BA_IsForPayroll;

                profile.DrivingLicenceNo = emp.DrivindLicenceNo;
                profile.LicenceIssueDate = emp.LicenceIssueDate;
                profile.LicenceExpiryDate = emp.LicenceExpiryDate;
                profile.IsTeaching = emp.IsTeaching;
                profile.DocumentColl = emp.AttachmentColl;
                profile.EC_PersonalName = emp.EC_PersonalName;
                profile.EC_Relationship = emp.EC_Relationship;
                profile.EC_Address = emp.EC_Address;
                profile.EC_Phone = emp.EC_Phone;
                profile.EC_Mobile = emp.EC_Mobile;
                

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

        // POST GetEmployeeDetails
        /// <summary>
        ///  Get EmployeeDetails                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.Employee))]
        public IHttpActionResult GetEmployeeDetails()
        {
            AcademicLib.BE.Academic.Transaction.Employee profile = new AcademicLib.BE.Academic.Transaction.Employee();
            try
            {
                profile = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getEmployeeById(0, 0);
                if (profile.EmployeeId > 0)
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
                        ResponseMSG = "Invalid Employee"
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

        #endregion

        #region "ClassSectionList"

        // POST GetClassSectionList
        /// <summary>
        ///  Get GetClassSectionList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.ClassSectionList))]
        public IHttpActionResult GetClassSectionList([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Creation.ClassSectionList classSection = new AcademicLib.BE.Academic.Creation.ClassSectionList();
            try
            {
                int AcademicYearId;
                string Role = "";
                if (para != null)
                {
                    if (para.ContainsKey("Role"))
                        Role = para["Role"].ToString();

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

                classSection = new AcademicLib.BL.Academic.Creation.Class(UserId, hostName, dbName).getClassSectionList(AcademicYearId,true,Role);
                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude=true,
                        IncludeProperties = new List<string>
                                        {
                                           "SubjectTeacher","ClassTeacher","CoOrdinator","HOD","Role", "BatchId","Batch","SemesterId","Semester","ClassYearId","ClassYear","ClassList","SectionList","ClassId","SectionId","ClassName","SectionName","ResponseMSG","IsSuccess","Name","SectionListOnly","SectionListWithClass","FilterSection"
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

        #region "ClassForClassTeacher"

        // POST GetClassForClassTeacher
        /// <summary>
        ///  Get GetClassForClassTeacher                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Teacher.ClassTeacherCollections))]
        public IHttpActionResult GetClassForClassTeacher([FromBody] JObject para)
        {
            AcademicLib.API.Teacher.ClassTeacherCollections classSection = new AcademicLib.API.Teacher.ClassTeacherCollections();
            try
            {
                int? classId = null, sectionId = null, classYearId = null, batchId = null, semesterId = null; ;
                int? academicYearId = null;

                if (para != null)
                {
                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = ToIntNull(para["classId"]);
                    else if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        classId = ToIntNull(para["ClassId"]);

                    if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                        sectionId = ToIntNull(para["sectionId"]);
                    else if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                        sectionId = ToIntNull(para["SectionId"]);

                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = ToIntNull(para["academicYearId"]);
                    else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                        academicYearId = ToIntNull(para["AcademicYearId"]);


                    if (para.ContainsKey("semesterId"))
                    {                 
                         semesterId = ToIntNull(para["semesterId"]);
                    }

                    if (para.ContainsKey("classYearId"))
                    {                        
                            classYearId = ToIntNull(para["classYearId"]);
                    }

                    if (para.ContainsKey("batchId"))
                    {                        
                            batchId = ToIntNull(para["batchId"]);
                    }
                }

                if (classId == 0)
                    classId = null;

                if (sectionId == 0)
                    sectionId = null;

                if (!academicYearId.HasValue || academicYearId == 0)
                    academicYearId = this.AcademicYearId;

                classSection = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getClassListForClassTeacher(academicYearId,batchId,semesterId,classYearId);
                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ClassList","SectionList","ClassId","SectionId","ClassName","SectionName","ResponseMSG","IsSuccess","Name","SectionListOnly"
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

        #region "ClassGroupList"

        // POST GetClassGroupList
        /// <summary>
        ///  Get GetClassGroupList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.ClassGroupCollections))]
        public IHttpActionResult GetClassGroupList()
        {
            AcademicLib.BE.Academic.Transaction.ClassGroupCollections classSection = new AcademicLib.BE.Academic.Transaction.ClassGroupCollections();
            try
            {
                classSection = new AcademicLib.BL.Academic.Transaction.ClassGroup(UserId, hostName, dbName).GetAllClassGroup(0);
                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ClassList","SectionList","ClassId","SectionId","ClassName","SectionName","ResponseMSG","IsSuccess","Name","SectionListOnly"
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

        #region "HomeWorkTypeList"

        // POST GetHomeWorkTypeList
        /// <summary>
        ///  Get HomeWorkTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.HomeWork.HomeworkTypeCollections))]
        public IHttpActionResult GetHomeWorkTypeList()
        {
            AcademicLib.BE.HomeWork.HomeworkTypeCollections homeWorkTypeColl = new AcademicLib.BE.HomeWork.HomeworkTypeCollections();
            try
            {
                homeWorkTypeColl = new AcademicLib.BL.HomeWork.HomeworkType(UserId, hostName, dbName).GetAllHomeworkType(0);
                return Json(homeWorkTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","HomeworkTypeId","Name","Description"
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

        #region "AssignmentTypeList"

        // POST GetAssignmentTypeList
        /// <summary>
        ///  Get AssignmentTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.HomeWork.AssignmentTypeCollections))]
        public IHttpActionResult GetAssignmentTypeList()
        {
            AcademicLib.BE.HomeWork.AssignmentTypeCollections homeWorkTypeColl = new AcademicLib.BE.HomeWork.AssignmentTypeCollections();
            try
            {
                homeWorkTypeColl = new AcademicLib.BL.HomeWork.AssignmentType(UserId, hostName, dbName).GetAllAssignmentType(0);
                return Json(homeWorkTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","AssignmentTypeId","Name","Description"
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

        #region "AddHomeWork"


        // Post api/AddHomeWork
        /// <summary>
        ///  Add Homework To Student         
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddHomeWork()
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

                    AcademicLib.BE.HomeWork.HomeWork para = DeserializeObject<AcademicLib.BE.HomeWork.HomeWork>(jsonData);
                    para.HomeWorkId = 0;
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
                        var retVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).SaveFormData(para);
                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.ResponseId))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            notification.Content = retVal.ResponseMSG;
                            notification.ContentPath = resVal.RId.ToString();
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK.ToString();
                            notification.Heading = "Homework";
                            notification.Subject = para.Topic;
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.ResponseId.Trim();
                           
                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification,false);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if(retVal.RId>0)
                                resVal.ResponseMSG = "No Student Found On this Class "+(retVal.IsSuccess ? "" : retVal.ResponseMSG );
                            else
                                resVal.ResponseMSG =retVal.ResponseMSG;

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

        // Post api/UpdateHomeWorkDeadline
        /// <summary>
        ///  Update Homework Deadline     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateHomeWorkDeadline()
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

                    AcademicLib.BE.HomeWork.HomeWork para = DeserializeObject<AcademicLib.BE.HomeWork.HomeWork>(jsonData);                    
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;
                        
                        resVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).UpdateDeadline(para);                        
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

        #region "AddAssignment"


        // Post api/AddAssignment
        /// <summary>
        ///  Add Assignment To Student         
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddAssignment()
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

                    AcademicLib.BE.HomeWork.Assignment para = DeserializeObject<AcademicLib.BE.HomeWork.Assignment>(jsonData);
                    para.AssignmentId = 0;
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
                        var retVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).SaveFormData(para);
                        if (retVal.IsSuccess)
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = "New Assignment";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ASSIGNMENT);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ASSIGNMENT.ToString();
                            notification.Heading = "Assignment";
                            notification.Subject = "Assignment";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;

                            if(!string.IsNullOrEmpty(retVal.ResponseId))
                                notification.UserIdColl = retVal.ResponseId.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification, true);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if (retVal.RId > 0)
                                resVal.ResponseMSG = "No Student Found On this Class " + (retVal.IsSuccess ? "" : retVal.ResponseMSG);
                            else
                                resVal.ResponseMSG = retVal.ResponseMSG;

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

        // Post api/UpdateAssignmentDeadline
        /// <summary>
        ///  Update Assignment Deadline     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateAssignmentDeadline()
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

                    AcademicLib.BE.HomeWork.Assignment para = DeserializeObject<AcademicLib.BE.HomeWork.Assignment>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;

                        resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).UpdateDeadline(para);
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
                    if (para.ContainsKey("dateFrom") && para["dateFrom"]!=null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo") && para["dateTo"]!=null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                }
                homeWorkColl = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).GetAllHomeWork(0, dateFrom, dateTo);

                if(homeWorkColl!=null && homeWorkColl.Count > 0)
                {
                    var query = from hw in homeWorkColl
                                group hw by new { hw.ClassName, hw.SectionName } into g
                                orderby  g.Key.ClassName,g.Key.SectionName
                                select new
                                {
                                    ClassId = g.First().ClassId,
                                    SectionId = g.First().SectionId,
                                    ClassName = g.First().ClassName,
                                    SectionName = g.First().SectionName,
                                    Total=g.Count(),
                                    DataColl = g
                                };

                    return Json(query, new JsonSerializerSettings
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

        #region "HomeWork Checked"

        // Post api/CheckHomeWork
        /// <summary>
        ///  Checked Homework
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> CheckHomeWork()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/homework");
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
                    var provider = new FormDataStreamProvider(path);
                    var task=await Request.Content.ReadAsMultipartAsync(provider);
                    
                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.HomeWorkChecked para = DeserializeObject<AcademicLib.API.Teacher.HomeWorkChecked>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (para.HomeWorkId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Homework";
                    }
                    else if (para.StudentId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Student";
                    }
                    else
                    {
                        para.UserId = UserId;
                        bool validFile = true;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);


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
                                    catch(Exception fr)
                                    {
                                        resVal.IsSuccess = false;
                                        resVal.ResponseMSG ="Can't Update Homework File "+ fr.Message;

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
                                    
                                }
                                else
                                {
                                    validFile = false;
                                    resVal.ResponseMSG = "Invalid File Name";
                                }
                            }

                        }
                        else
                        {
                            validFile = false;
                            resVal.ResponseMSG = "No Attachment found";
                        }

                        if (validFile)
                            resVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).CheckHomeWork(para);

                        if (resVal.IsSuccess)
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = "Homework Check";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK_CHECK);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK_CHECK.ToString();
                            notification.Heading = "Homework Check";
                            notification.Subject = "Homework Check";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = resVal.ResponseId;

                            var notRes = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

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

        // Post api/CheckClassWiseHomeWork
        /// <summary>
        ///  Checked ClassWise Homework
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> CheckClassWiseHomeWork()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/homework");
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
                    var provider = new FormDataStreamProvider(path);
                    var task = await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.HomeWorkCheckedCollections para = DeserializeObject<AcademicLib.API.Teacher.HomeWorkCheckedCollections>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }                   
                    else
                    {
                        resVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).CheckClassWiseHomeWork(para);

                        if (resVal.IsSuccess)
                        {
                            List<Dynamic.BusinessEntity.Global.NotificationLog> notificationColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();

                            foreach(var be in para)
                            {
                                if (be.SUserId > 0)
                                {
                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                    notification.Content = "Homework "+(be.Status==1 ? "Done" : " Not Done");
                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK_CHECK);
                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK_CHECK.ToString();
                                    notification.Heading = "Homework Check";
                                    notification.Subject = "Homework Check";
                                    notification.UserId = UserId;
                                    notification.UserName = User.Identity.Name;
                                    notification.UserIdColl = be.SUserId.ToString();
                                    notificationColl.Add(notification);
                                }
                                                         
                            }

                            if (notificationColl.Count > 0)
                            {
                                var notRes = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notificationColl);
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

        #region "HomeWork Details"

        // POST GetHomeWorkById
        /// <summary>
        ///  Get HomeWorkById     
        ///  homeWorkId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.HomeWorkDetailsCollections))]
        public IHttpActionResult GetHomeWorkById([FromBody] JObject para)
        {
            AcademicLib.RE.HomeWork.HomeWorkDetailsCollections homeWorkColl = new AcademicLib.RE.HomeWork.HomeWorkDetailsCollections();
            try
            {
                int homeWorkId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("homeWorkId") && para["homeWorkId"] != null)
                        homeWorkId = ToInt(para["homeWorkId"]);
                    else if (para.ContainsKey("HomeWorkId") && para["HomeWorkId"] != null)
                        homeWorkId = ToInt(para["HomeWorkId"]);
                    else if (para.ContainsKey("homeWorkid") && para["homeWorkid"] != null)
                        homeWorkId = ToInt(para["homeWorkid"]);
                }
                homeWorkColl = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).getHomeWorkDetailsById(homeWorkId);

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

        #region "Delete HomeWork"

        // POST DelHomeWorkById
        /// <summary>
        ///  Get DelHomeWorkById     
        ///  homeWorkId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.HomeWorkDetailsCollections))]
        public IHttpActionResult DelHomeWorkById([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int homeWorkId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("homeWorkId") && para["homeWorkId"] != null)
                        homeWorkId = Convert.ToInt32(para["homeWorkId"]);
                    else if (para.ContainsKey("HomeWorkId") && para["HomeWorkId"] != null)
                        homeWorkId = Convert.ToInt32(para["HomeWorkId"]);
                    else if (para.ContainsKey("homeWorkid") && para["homeWorkid"] != null)
                        homeWorkId = Convert.ToInt32(para["homeWorkid"]);
                }

                if(homeWorkId==0)
                {
                    resVal.ResponseMSG = "Invalid Homework pls ! select valid homework for delete";
                }else
                    resVal = new AcademicLib.BL.HomeWork.HomeWork(UserId, hostName, dbName).DeleteById(0, homeWorkId);

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

                if (AssignmentColl != null && AssignmentColl.Count > 0)
                {
                    var query = from hw in AssignmentColl
                                group hw by new { hw.ClassId, hw.SectionId } into g
                                select new
                                {
                                    ClassId = g.Key.ClassId,
                                    SectionId = g.Key.SectionId,
                                    ClassName = g.First().ClassName,
                                    SectionName = g.First().SectionName,
                                    Total = g.Count(),
                                    DataColl = g
                                };

                    return Json(query, new JsonSerializerSettings
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

        #region "Assignment Checked"

        // Post api/CheckAssignment
        /// <summary>
        ///  Checked Assignment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> CheckAssignment()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/homework");
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
                    var provider = new FormDataStreamProvider(path);
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.AssignmentChecked para = DeserializeObject<AcademicLib.API.Teacher.AssignmentChecked>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (para.AssignmentId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Assignment";
                    }
                    else if (para.StudentId == 0)
                    {
                        resVal.ResponseMSG = "Invalid Student";
                    }
                    else
                    {
                        para.UserId = UserId;
                        bool validFile = true;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);


                            foreach (var dc in DocumentColl)
                            {
                                string fullPath = path + "//" + dc.Name;
                                if (System.IO.File.Exists(fullPath))
                                {
                                    string nDoc = basePath + dc.DocPath;
                                    System.IO.File.Copy(nDoc, fullPath, true);
                                }
                                else
                                {
                                    validFile = false;
                                    resVal.ResponseMSG = "Invalid File Name";
                                }
                            }


                        }

                        if (validFile)
                            resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).CheckAssignment(para);

                        if (resVal.IsSuccess)
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = "Assignment Check";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment_CHECK);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment_CHECK.ToString();
                            notification.Heading = "Assignment Check";
                            notification.Subject = "Assignment Check";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = resVal.ResponseId;

                            var notRes = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

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





        // Post api/CheckClassWiseAssignment
        /// <summary>
        ///  Checked ClassWise Assignment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> CheckClassWiseAssignment()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/homework");
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
                    var provider = new FormDataStreamProvider(path);
                    var task = await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.AssignmentCheckedCollections para = DeserializeObject<AcademicLib.API.Teacher.AssignmentCheckedCollections>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        var uid = UserId;
                        foreach(var v in para)
                        {
                            v.UserId = uid;
                        }
                        resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).CheckClassWiseAssignment(para);

                        if (resVal.IsSuccess)
                        {
                            List<Dynamic.BusinessEntity.Global.NotificationLog> notificationColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();

                            foreach (var be in para)
                            {
                                if (be.SUserId > 0)
                                {
                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                    notification.Content = "Assignment Check";
                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment_CHECK);
                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.Assignment_CHECK.ToString();
                                    notification.Heading = "Assignment Check";
                                    notification.Subject = "Assignment Check";
                                    notification.UserId = UserId;
                                    notification.UserName = User.Identity.Name;
                                    notification.UserIdColl = be.SUserId.ToString();
                                    notificationColl.Add(notification);
                                }

                            }

                            if (notificationColl.Count > 0)
                            {
                                var notRes = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notificationColl);
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

        #region "Assignment Details"

        // POST GetAssignmentById
        /// <summary>
        ///  Get AssignmentById     
        ///  AssignmentId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.AssignmentDetailsCollections))]
        public IHttpActionResult GetAssignmentById([FromBody] JObject para)
        {
            AcademicLib.RE.HomeWork.AssignmentDetailsCollections AssignmentColl = new AcademicLib.RE.HomeWork.AssignmentDetailsCollections();
            try
            {
                int AssignmentId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("AssignmentId") && para["AssignmentId"] != null)
                        AssignmentId = Convert.ToInt32(para["AssignmentId"]);
                    else if (para.ContainsKey("assignmentId") && para["assignmentId"] != null)
                        AssignmentId = Convert.ToInt32(para["assignmentId"]);
                    else if (para.ContainsKey("assignmentid") && para["assignmentid"] != null)
                        AssignmentId = Convert.ToInt32(para["assignmentid"]);
                }
                AssignmentColl = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).getAssignmentDetailsById(AssignmentId);

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

        #region "Delete Assignment Details"

        // POST DelAssignmentById
        /// <summary>
        ///  Delete AssignmentById     
        ///  AssignmentId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult DelAssignmentById([FromBody] JObject para)
        {
            ResponeValues resVal=new ResponeValues();
            try
            {
                int AssignmentId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("AssignmentId") && para["AssignmentId"] != null)
                        AssignmentId = Convert.ToInt32(para["AssignmentId"]);
                    else if (para.ContainsKey("assignmentId") && para["assignmentId"] != null)
                        AssignmentId = Convert.ToInt32(para["assignmentId"]);
                    else if (para.ContainsKey("assignmentid") && para["assignmentid"] != null)
                        AssignmentId = Convert.ToInt32(para["assignmentid"]);
                }
                resVal = new AcademicLib.BL.HomeWork.Assignment(UserId, hostName, dbName).DeleteById(0,AssignmentId);

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

        #region "RemarksTypeList"

        // POST GetRemarksTypeList
        /// <summary>
        ///  Get RemarksTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.RemarksTypeCollections))]
        public IHttpActionResult GetRemarksTypeList()
        {
            AcademicLib.BE.Academic.Creation.RemarksTypeCollections remarksTypeColl = new AcademicLib.BE.Academic.Creation.RemarksTypeCollections();
            try
            {
                remarksTypeColl = new AcademicLib.BL.Academic.Creation.RemarksType(UserId, hostName, dbName).GetAllRemarksType(0);
                return Json(remarksTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","RemarksTypeId","Name","Description"
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

        #region "GetClassWiseStudentList"

        // POST GetClassWiseStudentList
        /// <summary>
        ///  Get ClassWiseStudentList 
        ///  classId as Int   classId:41,
        ///  sectionIdColl as String  sectionIdColl:2,4 ( this is optional )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.ClassSectionList))]
        public IHttpActionResult GetClassWiseStudentList([FromBody] JObject para)
        {
            if (para == null)
            {
                return BadRequest("No form data found");
            }
            else
            {
                AcademicLib.BE.Academic.Transaction.StudentListCollections studentList = new AcademicLib.BE.Academic.Transaction.StudentListCollections();

                int? subjectId = null,classYearId=null,batchId=null,semesterId=null;

                int classId = 0;
                string sectionIdColl = "";
                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                int AcademicYearId ;
                if (para.ContainsKey("AcademicYearId"))
                {
                    try
                    {
                        AcademicYearId = ToInt(para["AcademicYearId"]);
                        if ( AcademicYearId == 0)
                            AcademicYearId = this.AcademicYearId;
                    }
                    catch(Exception e)
                    {
                        AcademicYearId = this.AcademicYearId;
                    }
                    
                }else
                {
                    AcademicYearId = this.AcademicYearId;
                }


                if (para.ContainsKey("sectionIdColl"))
                {
                    sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                    if (sectionIdColl == "0")
                        sectionIdColl = "";
                }

                if (para.ContainsKey("subjectId"))
                {
                    if(para["subjectId"].ToString().ToLower()!="null")
                        subjectId = ToIntNull(para["subjectId"]);                     
                }


                if (para.ContainsKey("semesterId"))
                {
                    if (para["semesterId"].ToString().ToLower() != "null")
                        semesterId = Convert.ToInt32(para["semesterId"]);
                }

                if (para.ContainsKey("classYearId"))
                {
                    if (para["classYearId"].ToString().ToLower() != "null")
                        classYearId = Convert.ToInt32(para["classYearId"]);
                }

                if (para.ContainsKey("batchId"))
                {
                    if (para["batchId"].ToString().ToLower() != "null")
                        batchId = Convert.ToInt32(para["batchId"]);
                }

                try
                {
                    studentList = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getClassWiseStudentList(AcademicYearId, classId, sectionIdColl,true,semesterId,classYearId,batchId,subjectId);


                    return Json(studentList.OrderBy(p1 => p1.SectionName).ThenBy(p1 => p1.RollNo), new JsonSerializerSettings
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

            


        }

        #endregion

        #region "SendNoticeToStudent"


        // Post api/SendNoticeToStudent
        /// <summary>
        ///  Send Notice To Student 
        ///  studentIdColl as string  = studentIdColl=1,2,4,5
        ///  title as string 
        ///  description as string
        ///  attachFiles as files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendNoticeToStudent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.StudentNotice para = DeserializeObject<AcademicLib.API.Teacher.StudentNotice>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (string.IsNullOrEmpty(para.studentIdColl))
                    {
                        resVal.ResponseMSG = "student does not found";
                    }
                    else if (string.IsNullOrEmpty(para.title))
                    {
                        resVal.ResponseMSG = "please ! enter notice title";
                    }else if (string.IsNullOrEmpty(para.description))
                    {
                        resVal.ResponseMSG = "please ! enter notice details";
                    }
                    else
                    {
                        var retVal = new AcademicLib.BL.Global(UserId, hostName, dbName).GetUserIdColl(para.studentIdColl, 1);
                        if (retVal.IsSuccess)
                        {
                            string userIdCollStr = "";
                            Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                            foreach (var uc in retVal.ResponseId.Split('#'))
                            {
                                string[] ucSep = uc.Split(',');
                                if (ucSep.Length == 2)
                                {
                                    if (!string.IsNullOrEmpty(userIdCollStr))
                                        userIdCollStr = userIdCollStr + ",";

                                    userIdCollStr = userIdCollStr + ucSep[1];

                                    userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                }
                            }

                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = para.description;
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE.ToString();
                            notification.Heading = para.title;
                            notification.Subject = para.title;
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = userIdCollStr.Trim();
                            if (provider.FileData.Count > 0)
                            {
                                var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                if(DocumentColl!=null && DocumentColl.Count > 0)
                                {
                                    notification.ContentPath = DocumentColl[0].DocPath;
                                }
                            }

                            if (ActiveBranch)
                                notification.BranchCode = this.BranchCode;

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

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

        #endregion

        #region "AddRemarsToStudent"


        // Post api/AddRemarksToStudent
        /// <summary>
        ///  Add Remarks To Student 
        ///  studentIdColl as string  = studentIdColl=1,2,4,5
        ///  forDate as Date
        ///  description as string
        ///  attachFiles as files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddRemarksToStudent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.StudentRemarks para = DeserializeObject<AcademicLib.API.Teacher.StudentRemarks>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (string.IsNullOrEmpty(para.studentIdColl))
                    {
                        resVal.ResponseMSG = "student does not found";
                    }
                    else if (string.IsNullOrEmpty(para.description))
                    {
                        resVal.ResponseMSG = "please ! enter notice details";
                    }else if (para.remarksTypeId == 0)
                    {
                        resVal.ResponseMSG = "Please ! Select Valid Remarks Type Name";
                    }
                    else
                    {
                        if (!para.forDate.HasValue)
                            para.forDate = DateTime.Now;

                        var uid = UserId;
                        var uName = User.Identity.Name;
                        var retVal = new AcademicLib.BL.Global(UserId, hostName, dbName).GetUserIdColl(para.studentIdColl, 1);
                        if (retVal.IsSuccess)
                        {
                            var attachFile = "";
                            if (provider.FileData.Count > 0)
                            {
                                var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                if (DocumentColl != null && DocumentColl.Count > 0)
                                {
                                    attachFile = DocumentColl[0].DocPath;
                                }
                            }

                            List<AcademicLib.BE.Academic.Transaction.StudentRemarks> studentRemarksColl = new List<AcademicLib.BE.Academic.Transaction.StudentRemarks>();
                            foreach(string idStr in para.studentIdColl.Split(','))
                            {
                                int id = 0;
                                int.TryParse(idStr, out id);
                                if (id > 0)
                                {
                                    AcademicLib.BE.Academic.Transaction.StudentRemarks studentRemarks = new AcademicLib.BE.Academic.Transaction.StudentRemarks();
                                    studentRemarks.StudentId = id;
                                    studentRemarks.Remarks = para.description;
                                    studentRemarks.RemarksTypeId = para.remarksTypeId;
                                    studentRemarks.RemarksFor = AcademicLib.BE.Academic.Transaction.REMARKSFOR.OTHERS;
                                    studentRemarks.ForDate = para.forDate.Value;
                                    studentRemarks.FilePath = attachFile;
                                    studentRemarks.Point = para.point;
                                    studentRemarks.RemarksFor = para.RemarksFor;
                                    studentRemarksColl.Add(studentRemarks);
                                }
                            }
                            var saveRes= new AcademicLib.BL.Academic.Transaction.StudentRemarks(UserId, hostName, dbName).SaveUpdate(studentRemarksColl);
                            if (!saveRes.IsSuccess)
                            {
                                retVal = saveRes;
                            }
                            else
                            {
                                string userIdCollStr = "";
                                Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                                foreach (var uc in retVal.ResponseId.Split('#'))
                                {
                                    string[] ucSep = uc.Split(',');
                                    if (ucSep.Length == 2)
                                    {
                                        if (!string.IsNullOrEmpty(userIdCollStr))
                                            userIdCollStr = userIdCollStr + ",";

                                        userIdCollStr = userIdCollStr + ucSep[1];

                                        userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                    }
                                }

                                Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                notification.Content = para.description;
                                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS);
                                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS.ToString();
                                notification.Heading = "Teacher Remarks";
                                notification.Subject = "Teacher Remarks";
                                notification.UserId = uid;
                                notification.UserName = uName;
                                notification.UserIdColl = userIdCollStr;
                                if (!string.IsNullOrEmpty(attachFile))
                                {
                                    notification.ContentPath = attachFile;
                                }
                                
                                if (ActiveBranch)
                                    notification.BranchCode = this.BranchCode;

                                resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                            }
                            
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


        // Post api/AddAchievmentToStudent
        /// <summary>
        ///  Add Remarks To Student 
        ///  studentIdColl as string  = studentIdColl=1,2,4,5
        ///  forDate as Date
        ///  description as string
        ///  attachFiles as files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddAchievmentToStudent([FromBody] AcademicLib.BE.Exam.Transaction.StudentAchievementCollections para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
           
                if (para == null || para.Count == 0)
                {
                    return BadRequest("No form data found");
                }

                else
                {
                    var uid = UserId;
                    var uName = User.Identity.Name;

                    resVal = new AcademicLib.BL.Exam.Transaction.StudentAchievement(uid, hostName, dbName).SaveFormData(para);

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


        // POST GetStudentAchievment
        /// <summary>
        ///  Get Student Achievment
        ///  studentId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.StudentRemarks))]
        public IHttpActionResult GetStudentAchievment([FromBody] JObject para)
        {
            List<AcademicLib.BE.Exam.Transaction.PrevAchievement> dataColl = new List<AcademicLib.BE.Exam.Transaction.PrevAchievement>();
            try
            {
                int studentId = 0, ExamTypeId=0,RemarksTypeId=0;
                if (para != null)
                {
                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);

                    if (para.ContainsKey("examTypeId") && para["examTypeId"] != null)
                        ExamTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("remarksTypeId") && para["remarksTypeId"] != null)
                        RemarksTypeId = Convert.ToInt32(para["remarksTypeId"]);

                }
                if (studentId == 0)
                    return BadRequest("Please ! Send StudentId in Request Para");

                dataColl = new AcademicLib.BL.Exam.Transaction.StudentAchievement(UserId, hostName, dbName).GetPreviousAchievement(0, studentId, ExamTypeId);                
                return Json(dataColl, new JsonSerializerSettings
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


        #region "AddRemarsToEmployee"


        // Post api/AddRemarsToEmployee
        /// <summary>
        ///  Add Remarks To Employee 
        ///  employeeIdColl as string  = employeeIdColl=1,2,4,5
        ///  forDate as Date
        ///  description as string
        ///  attachFiles as files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddRemarsToEmployee()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.EmployeeRemarks para = DeserializeObject<AcademicLib.API.Teacher.EmployeeRemarks>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (string.IsNullOrEmpty(para.employeeIdColl))
                    {
                        resVal.ResponseMSG = "employee does not found";
                    }
                    else if (string.IsNullOrEmpty(para.description))
                    {
                        resVal.ResponseMSG = "please ! enter remarks details";
                    }
                    else if (para.remarksTypeId == 0)
                    {
                        resVal.ResponseMSG = "Please ! Select Valid Remarks Type Name";
                    }
                    else
                    {
                        if (!para.forDate.HasValue)
                            para.forDate = DateTime.Now;

                        var uid = UserId;
                        var uName = User.Identity.Name;
                        var retVal = new AcademicLib.BL.Global(UserId, hostName, dbName).GetUserIdColl(para.employeeIdColl, 2);
                        if (retVal.IsSuccess)
                        {
                            var attachFile = "";
                            if (provider.FileData.Count > 0)
                            {
                                var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                if (DocumentColl != null && DocumentColl.Count > 0)
                                {
                                    attachFile = DocumentColl[0].DocPath;
                                }
                            }

                            List<AcademicLib.BE.Academic.Transaction.EmployeeRemarks> empRemarksColl = new List<AcademicLib.BE.Academic.Transaction.EmployeeRemarks>();
                            foreach (string idStr in para.employeeIdColl.Split(','))
                            {
                                int id = 0;
                                int.TryParse(idStr, out id);
                                if (id > 0)
                                {
                                    AcademicLib.BE.Academic.Transaction.EmployeeRemarks empRemarks = new AcademicLib.BE.Academic.Transaction.EmployeeRemarks();
                                    empRemarks.CUserId = uid;
                                    empRemarks.EmployeeId = id;
                                    empRemarks.Remarks = para.description;
                                    empRemarks.RemarksTypeId = para.remarksTypeId;
                                    empRemarks.RemarksFor = AcademicLib.BE.Academic.Transaction.REMARKSFOR.OTHERS;
                                    empRemarks.ForDate = para.forDate.Value;
                                    empRemarks.FilePath = attachFile;
                                    empRemarks.Point = para.point;
                                    empRemarksColl.Add(empRemarks);
                                }
                            }
                            var saveRes = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(UserId, hostName, dbName).SaveUpdate(empRemarksColl);
                            if (!saveRes.IsSuccess)
                            {
                                resVal = saveRes;
                            }
                            else
                            {
                                string userIdCollStr = "";
                                Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                                foreach (var uc in retVal.ResponseId.Split('#'))
                                {
                                    string[] ucSep = uc.Split(',');
                                    if (ucSep.Length == 2)
                                    {
                                        if (!string.IsNullOrEmpty(userIdCollStr))
                                            userIdCollStr = userIdCollStr + ",";

                                        userIdCollStr = userIdCollStr + ucSep[1];

                                        userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                    }
                                }

                                Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                notification.Content = para.description;
                                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS);
                                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS.ToString();
                                notification.Heading = "Employee Remarks";
                                notification.Subject = "Employee Remarks";
                                notification.UserId = uid;
                                notification.UserName = uName;
                                notification.UserIdColl = userIdCollStr;
                                if (!string.IsNullOrEmpty(attachFile))
                                {
                                    notification.ContentPath = attachFile;
                                }

                                if (ActiveBranch)
                                    notification.BranchCode = this.BranchCode;

                                resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                            }

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

        #endregion

        #region "GetSubjectMapping"

        // POST GetSubjectMapping
        /// <summary>
        ///  Get GetSubjectMapping                 
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetSubjectMapping([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.SubjectMappingClassWiseCollections subjectColl = new AcademicLib.BE.Academic.Transaction.SubjectMappingClassWiseCollections();
            try
            {
                int classId = 0;
                int? sectionId = null; 

                if (para != null)
                {
                    int AcademicYearId;

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
                        classId = ToInt(para["classId"]);

                    try
                    {
                        if (para.ContainsKey("sectionId"))
                        {
                            var sid = Convert.ToString(para["sectionId"]);

                            if (sid != null && sid.Contains(','))
                            {
                                string[] splitId = sid.Split(',');
                                if (splitId.Length > 0)
                                    sectionId = Convert.ToInt32(splitId[0]);
                            }
                            else
                                sectionId = ToIntNull(para["sectionId"]);
                        }

                        if (para.ContainsKey("sectionIdColl"))
                        {
                            string sidCOll = Convert.ToString(para["sectionIdColl"]);
                            if (sidCOll == "0")
                                sidCOll = "";

                            if (!sectionId.HasValue)
                            {
                                int id = 0;
                                int.TryParse(sidCOll, out id);
                                if (id > 0)
                                {
                                    sectionId = id;
                                }
                            }
                        }
                    }
                    catch { }


                }

                string sssId = sectionId.HasValue && sectionId.Value > 0 ? sectionId.Value.ToString() : "";

                subjectColl = new AcademicLib.BL.Academic.Transaction.SubjectMappingClassWise(UserId, hostName, dbName).getClassWiseSubjectMapping(AcademicYearId,  classId, sssId,null,null,null);
                return Json(subjectColl, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = true,
                    //    IncludeProperties = new List<string>
                    //                    {
                    //                        "ResponseMSG","IsSuccess","SubjectId","Name","Code","CodeTH","CodePR","ClassId","SectionId"
                    //                    }
                    //}
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "GetEmpListForClassTeacher"

        // POST GetEmpListForClassTeacher
        /// <summary>
        ///  Get GetEmpListForClassTeacher                 
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetEmpListForClassTeacher([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeUserCollections subjectColl = new AcademicLib.BE.Academic.Transaction.EmployeeUserCollections();
            try
            {
                int classId = 0;
                int? sectionId = null;
                int? subjectId = null;

                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    try
                    {
                        if (para.ContainsKey("subjectId"))
                        {
                            if (para["subjectId"].ToString() != "null")
                                subjectId = ToIntNull(para["subjectId"]);
                        }

                        if (para.ContainsKey("sectionId"))
                        {
                            var sid = Convert.ToString(para["sectionId"]);

                            if (sid != null && sid.Contains(','))
                            {
                                string[] splitId = sid.Split(',');
                                if (splitId.Length > 0)
                                    sectionId = Convert.ToInt32(splitId[0]);
                            }
                            else
                                sectionId = ToIntNull(para["sectionId"]);
                        }

                        if (para.ContainsKey("sectionIdColl"))
                        {
                            string sidCOll = Convert.ToString(para["sectionIdColl"]);
                            if (sidCOll == "0")
                                sidCOll = "";

                            if (!sectionId.HasValue)
                            {
                                int id = 0;
                                int.TryParse(sidCOll, out id);
                                if (id > 0)
                                {
                                    sectionId = id;
                                }
                            }
                        }
                    }
                    catch { }


                }

                string sssId = sectionId.HasValue && sectionId.Value > 0 ? sectionId.Value.ToString() : "";
                subjectColl = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getEmpListForClassTeacher(classId, sectionId, null, null, null,subjectId);                
                return Json(subjectColl, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = true,
                    //    IncludeProperties = new List<string>
                    //                    {
                    //                        "ResponseMSG","IsSuccess","SubjectId","Name","Code","CodeTH","CodePR","ClassId","SectionId"
                    //                    }
                    //}
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
                int? classId = null,sectionId=null,batchId=null,classYearId=null,semesterId=null;
                bool forAllSubject = false;
                int AcademicYearId;
                string Role = "";
                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("Role"))
                        Role = para["Role"].ToString();

                    if (para.ContainsKey("batchId"))
                        batchId = Convert.ToInt32(para["batchId"]);
                    
                    if (para.ContainsKey("classYearId"))
                        classYearId = Convert.ToInt32(para["classYearId"]);

                    if (para.ContainsKey("semesterId"))
                        semesterId = Convert.ToInt32(para["semesterId"]);

                    if (para.ContainsKey("forAllSubject"))
                        forAllSubject = Convert.ToBoolean(para["forAllSubject"]);

                  
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

                    try
                    {
                        if (para.ContainsKey("sectionId"))
                        {
                            var sid = Convert.ToString(para["sectionId"]);

                            if (sid != null && sid.Contains(','))
                            {
                                string[] splitId = sid.Split(',');
                                if (splitId.Length > 0)
                                    sectionId = Convert.ToInt32(splitId[0]);
                            }
                            else
                                sectionId = ToIntNull(para["sectionId"]);
                        }

                        if (para.ContainsKey("sectionIdColl"))
                        {
                            string sidCOll = Convert.ToString(para["sectionIdColl"]);
                            if (sidCOll == "0")
                                sidCOll = "";

                            if (!sectionId.HasValue)
                            {
                                int id = 0;
                                int.TryParse(sidCOll, out id);
                                if (id > 0)
                                {
                                    sectionId = id;
                                }
                            }
                        }
                    }
                    catch { }


                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }

                subjectColl = new AcademicLib.BL.Academic.Creation.Subject(UserId, hostName, dbName).GetAllSubject(0,AcademicYearId,null,classId,sectionId, forAllSubject,batchId,classYearId,semesterId,Role);
                return Json(subjectColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                             "SubjectTeacher","ClassTeacher","CoOrdinator","HOD","Role","ResponseMSG","IsSuccess","SubjectId","Name","Code","CodeTH","CodePR","ClassId","SectionId","BatchId","SemesterId","ClassYearId","Batch","Semester","ClassYear","IsECA","IsMath"
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

        #region "ClassWiseAttendance"


        // Post api/SaveClassWiseAttendance
        /// <summary>
        ///  Save Student Class Wise Daily Attendance
        ///  "ClassId":1,
        ///  "SectionId":null, 
        ///  "ForDate":"2021-05-01",
        ///  "InOutMode":3,
        ///  "StudentId":1,
        ///  "Attendance":1,
        ///  "LateMin":0,
        ///  "Remarks":"Test",
        ///  "Notify":false
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveClassWiseAttendance([FromBody] AcademicLib.BE.Attendance.AttendanceStudentWiseCollections para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var uid = UserId;
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    var retVal = new AcademicLib.BL.Attendance.AttendanceStudentWise(uid, hostName, dbName).SaveFormData(this.AcademicYearId, para);
                    if (retVal.IsSuccess)
                    {
                        var forDate = para[0].ForDate;
                        resVal.IsSuccess = retVal.IsSuccess;
                        resVal.ResponseMSG = retVal.ResponseMSG;

                        bool notify = para.First().Notify;

                        if (notify && forDate.ToString("yyyy-MM-dd")==DateTime.Today.ToString("yyyy-MM-dd"))
                        {
                            string idColl = "";                            
                            foreach(var v in para)
                            {
                                if (!string.IsNullOrEmpty(idColl))
                                    idColl = idColl + ",";

                                idColl = idColl + v.StudentId.ToString();
                            }
                            string[] idArray = idColl.Split(',');

                            //var idRes = new AcademicLib.BL.Global(uid, hostName, dbName).GetUserIdColl(idColl, 1);

                            var idRes = new AcademicLib.BL.Global(uid, hostName, dbName).GetStudentIdColl(idColl, 1);
                            if (idRes.IsSuccess)
                            {
                                await Task.Run(() =>
                                {
                                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(uid, hostName, dbName,GetBaseUrl);
                                    var templatesPresentColl = new AcademicLib.BL.Setup.SENT(uid, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.PresentStudent, 3, 3);
                                    var templatesAbsentColl = new AcademicLib.BL.Setup.SENT(uid, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.AbsentStudent, 3, 3);

                                    List<Dynamic.BusinessEntity.Global.NotificationLog> notificationColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();


                                    var query = from si in idRes
                                                join p in para on si.Id equals p.StudentId
                                                select new AcademicLib.SEN.StudentAttendance
                                                {
                                                    ClassName = si.ClassName,
                                                    Id = si.Id,
                                                    Name = si.Name,
                                                    RegNo = si.RegNo,
                                                    RollNo = si.RollNo,
                                                    SectionName = si.SectionName,
                                                    TodayAD = si.TodayAD,
                                                    TodayBS = si.TodayBS,
                                                    UserId = si.UserId,
                                                    Attendance = p.Attendance.ToString(),
                                                    ForDate = p.ForDate,
                                                    InOutMode = p.InOutMode.ToString(),
                                                    LateMin = p.LateMin,
                                                    Remarks = p.Remarks
                                                };



                                    foreach (var st in query)
                                    {

                                        if (st.Attendance == "PRESENT" || st.Attendance == "LATE")
                                        {
                                            if (templatesPresentColl != null && templatesPresentColl.Count>0)
                                            {
                                                var templateNotifiation = templatesPresentColl[0];

                                                #region "Send Notification"

                                                if (templateNotifiation != null)
                                                {
                                                    string tempMSG = templateNotifiation.Description;
                                                    System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.SEN.StudentAttendance), templateNotifiation.Description);
                                                    foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                                    {
                                                        try
                                                        {
                                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(st, field.Name).ToString());
                                                        }
                                                        catch { }
                                                    }

                                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                                    notification.Content = tempMSG;
                                                    notification.ContentPath = "";
                                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE);
                                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE.ToString();
                                                    notification.Heading = templateNotifiation.Title;
                                                    notification.Subject = templateNotifiation.Title;
                                                    notification.UserId = uid;
                                                    notification.UserName = User.Identity.Name;
                                                    notification.UserIdColl = st.UserId.ToString();
                                                    notificationColl.Add(notification);
                                                }


                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            if (templatesAbsentColl != null && templatesAbsentColl.Count>0)
                                            {
                                                var templateNotifiation = templatesAbsentColl[0];

                                                #region "Send Notification"

                                                if (templateNotifiation != null)
                                                {
                                                    string tempMSG = templateNotifiation.Description;
                                                    System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.SEN.StudentAttendance), templateNotifiation.Description);
                                                    foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                                    {
                                                        try
                                                        {
                                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(st, field.Name).ToString());
                                                        }
                                                        catch { }
                                                    }

                                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                                    notification.Content = tempMSG;
                                                    notification.ContentPath = "";
                                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE);
                                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE.ToString();
                                                    notification.Heading = templateNotifiation.Title;
                                                    notification.Subject = templateNotifiation.Title;
                                                    notification.UserId = uid;
                                                    notification.UserName = User.Identity.Name;
                                                    notification.UserIdColl = st.UserId.ToString();
                                                    notificationColl.Add(notification);
                                                }


                                                #endregion
                                            }
                                        }

                                    }

                                    if (notificationColl != null && notificationColl.Count > 0)
                                        globlFun.SendNotification(UserId, notificationColl, true);
                                });

                                                                      
                            }

                        }
                        
                       
                    }
                    else
                        resVal = retVal;

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

        // POST GetClassWiseAttendance
        /// <summary>
        ///  Get ClassWiseAttendance                 
        ///  classId as Int   
        ///  sectionId as Int   (Optional)
        ///  forDate as Date
        ///  inOutMode as int ( 1=In,2=Out,3=Both)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetClassWiseAttendance([FromBody] JObject para)
        {
            AcademicLib.BE.Attendance.AttendanceStudentWiseCollections subjectColl = new AcademicLib.BE.Attendance.AttendanceStudentWiseCollections();
            try
            {
                int classId=0 ;
                int? sectionId = null, classYearId = null, batchId = null, semesterId = null;
                DateTime forDate = DateTime.Today;
                int inOutMode = 3;


                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId =ToInt(para["classId"]);
                    
                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("forDate"))
                        forDate = Convert.ToDateTime(para["forDate"]);

                    if (para.ContainsKey("inOutMode"))
                        inOutMode = Convert.ToInt32(para["inOutMode"]);

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


                    int AcademicYearId;
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

                    subjectColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(UserId, hostName, dbName).getClassWiseAttendance(AcademicYearId, classId, sectionId, forDate, inOutMode,batchId,semesterId,classYearId);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = true,
                            IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks","RegdNo","RollNo","Name","PhotoPath","ClassName","SectionName","BatchId","SemesterId","ClassYearId","Batch","Semester","ClassYear"
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

        // POST GetDateWiseAttendanceSummary
        /// <summary>
        ///  Get DateWiseAttendanceSummary                 
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        ///  fromDate as Date  (Optional)
        ///  toDate as Date  (Optional) 
        ///  dateFrom as Date  (Optional)
        ///  dateTo as Date  (Optional)      
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetDateWiseAttendanceSummary([FromBody] JObject para)
        {
            AcademicLib.API.Teacher.AttendanceSummaryCollections subjectColl = new AcademicLib.API.Teacher.AttendanceSummaryCollections();
            try
            {
                int classId = 0;
                int? sectionId = null;
                DateTime fromDate = DateTime.Today;
                DateTime toDate = DateTime.Today;
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
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("dateFrom"))
                        fromDate = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("dateTo"))
                        toDate = Convert.ToDateTime(para["dateTo"]);

                    subjectColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(UserId, hostName, dbName).getClassWiseSummary(AcademicYearId, classId, sectionId, fromDate, toDate);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //            {
                            //                "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                            //            }
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


        // POST GetClassWiseAttendanceMonthly
        /// <summary>
        ///  Get ClassWiseAttendanceMonthly                 
        ///  classId as Int   
        ///  sectionId as Int   (Optional)
        ///  yearId as Int
        ///  monthId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetClassWiseAttendanceMonthly([FromBody] JObject para)
        {
            
            try
            {
                int classId = 0;
                int? sectionId = null;
                int yearId = 0, monthId = 0;


                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("yearId"))
                        yearId = Convert.ToInt32(para["yearId"]);

                    if (para.ContainsKey("monthId"))
                        monthId = Convert.ToInt32(para["monthId"]);

                    var returnData = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getStudentMonthlyAttendance(this.AcademicYearId,yearId,monthId, classId, sectionId);
                    return Json(returnData, new JsonSerializerSettings
                    {
                        //ContractResolver = new JsonContractResolver()
                        //{
                        //    IsInclude = true,
                        //    IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                        //                }
                        //}
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

        // POST GetStudentAttendanceMonthly
        /// <summary>
        ///  Get StudentAttendanceMonthly                 
        ///  studentId as Int           
        ///  yearId as Int optional
        ///  monthId as Int optional        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetStudentAttendanceMonthly([FromBody] JObject para)
        {

            try
            {
                int studentId = 0;                
                int yearId = 0, monthId = 0;
                int? subjectId = null;

                if (para != null)
                {
                    if (para.ContainsKey("studentId"))
                        studentId = Convert.ToInt32(para["studentId"]);

                    
                    if (para.ContainsKey("yearId"))
                        yearId = Convert.ToInt32(para["yearId"]);

                    if (para.ContainsKey("monthId"))
                        monthId = Convert.ToInt32(para["monthId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    var returnData = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getStudentBIOAttendance(this.AcademicYearId,studentId,null,null, yearId, monthId, subjectId);
                    return Json(returnData, new JsonSerializerSettings
                    {
                        //ContractResolver = new JsonContractResolver()
                        //{
                        //    IsInclude = true,
                        //    IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                        //                }
                        //}
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


        // POST GetClassWiseAttendance
        /// <summary>
        ///  Get ClassWiseAttendance                 
        ///  classId as Int   
        ///  sectionId as Int   (Optional)
        ///  dateFrom as Date
        ///  inOutMode as int ( 1=In,2=Out,3=Both)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Attendance.AttendanceSummary))]
        public IHttpActionResult GetClassWiseAttendanceSummary([FromBody] JObject para)
        {
            AcademicLib.API.Attendance.AttendanceSummaryCollections subjectColl = new AcademicLib.API.Attendance.AttendanceSummaryCollections();
            try
            {
                int classId = 0;
                int? sectionId = null,yearId=null,monthId=null,subjectId=null, classYearId = null, batchId = null, semesterId = null;
                DateTime? fromDate = null;
                DateTime? toDate = null;
                int? studentId = null;


                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("dateFrom"))
                        fromDate = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("dateTo"))
                        toDate = Convert.ToDateTime(para["dateTo"]);

                    if (para.ContainsKey("yearId"))
                        yearId = Convert.ToInt32(para["yearId"]);

                    if (para.ContainsKey("monthId"))
                        monthId = Convert.ToInt32(para["monthId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    if (para.ContainsKey("studentId"))
                        studentId = Convert.ToInt32(para["studentId"]);

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


                    subjectColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getClassWiseAttendanceSummary(UserId, classId, sectionId, fromDate, toDate, this.AcademicYearId, yearId, monthId,subjectId,studentId, batchId, semesterId, classYearId);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        //ContractResolver = new JsonContractResolver()
                        //{
                        //    IsInclude = true,
                        //    IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                        //                }
                        //}
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


        // POST GetAttendanceLog
        /// <summary>       
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Attendance.AttendanceSummary))]
        public IHttpActionResult GetAttendanceLog([FromBody] JObject para)
        {
            try
            {
                
                int?   yearId = null, monthId = null, employeeId = null;
                DateTime? fromDate = null;
                DateTime? toDate = null; 
                if (para != null)
                { 
                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("dateFrom"))
                        fromDate = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("dateTo"))
                        toDate = Convert.ToDateTime(para["dateTo"]);

                    if (para.ContainsKey("yearId"))
                        yearId = Convert.ToInt32(para["yearId"]);

                    if (para.ContainsKey("monthId"))
                        monthId = Convert.ToInt32(para["monthId"]);

                    if (para.ContainsKey("employeeId"))
                        employeeId = Convert.ToInt32(para["employeeId"]);
                      
                }

                int totalAbsent = 0;
                double totalWorkingHour = 0;
                var subjectColl = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).getEmployeeWiseAttendance(employeeId, fromDate, toDate, yearId, monthId, ref totalAbsent, ref totalWorkingHour);
                var newRet = new
                {
                    TotalAbsent=totalAbsent,
                    TotalWorkingHour=totalWorkingHour,
                    DataColl=subjectColl,
                    IsSuccess=subjectColl.IsSuccess,
                    ResponseMSG=subjectColl.ResponseMSG
                };
                return Json(newRet, new JsonSerializerSettings
                {
                    
                });



            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        #endregion

        #region "SubjectWiseAttendance"


        // Post api/SaveSubjectWiseAttendance
        /// <summary>
        ///  Save Student Subject Wise Daily Attendance     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SaveSubjectWiseAttendance([FromBody] AcademicLib.BE.Attendance.AttendanceSubjectWiseCollections para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null || para.Count==0) 
                {
                    return BadRequest("No form data found");
                }
                else if(para.Count>0)
                {
                    var retVal = new AcademicLib.BL.Attendance.AttendanceSubjectWise(UserId, hostName, dbName).SaveFormData(this.AcademicYearId, para);
                    if (retVal.IsSuccess)
                    {
                        var forDate = para[0].ForDate;
                        resVal.IsSuccess = retVal.IsSuccess;
                        resVal.ResponseMSG = retVal.ResponseMSG;

                        bool notify = para.First().Notify;

                        if (notify && forDate.ToString("yyyy-MM-dd") == DateTime.Today.ToString("yyyy-MM-dd"))
                        {
                            string idColl = "";
                            foreach (var v in para)
                            {
                                if (!string.IsNullOrEmpty(idColl))
                                    idColl = idColl + ",";

                                idColl = idColl + v.StudentId.ToString();
                            }
                            string[] idArray = idColl.Split(',');

                            var idRes = new AcademicLib.BL.Global(UserId, hostName, dbName).GetUserIdColl(idColl, 1);

                            if (idRes.IsSuccess)
                            {

                                Task.Run(() =>
                                {
                                    if (!string.IsNullOrEmpty(idRes.ResponseId))
                                    {
                                        Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                                        foreach (var uc in idRes.ResponseId.Split('#'))
                                        {
                                            string[] ucSep = uc.Split(',');
                                            if (ucSep.Length == 2)
                                            {
                                                userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                            }
                                        }

                                        List<Dynamic.BusinessEntity.Global.NotificationLog> notificationLogsColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();
                                        foreach (var st in para)
                                        {
                                            if (userIdColl.ContainsKey(st.StudentId))
                                            {
                                                Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                                notification.Content = "Attendance : " + st.Attendance.ToString();
                                                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.SUBJECTWISE_ATTENDANCE);
                                                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.SUBJECTWISE_ATTENDANCE.ToString();
                                                notification.Heading = "Student Subject Attendance";
                                                notification.Subject = "Student Subject Attendance";
                                                notification.UserId = UserId;
                                                notification.UserName = User.Identity.Name;
                                                notification.UserIdColl = userIdColl[st.StudentId].ToString();
                                                notificationLogsColl.Add(notification);
                                                //   new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName).SendNotification(UserId, notification);
                                            }
                                        }

                                        if (notificationLogsColl.Count > 0)
                                            new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notificationLogsColl);
                                    }
                                });                                 
                            }

                        }


                    }
                    else
                        resVal = retVal;

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

        // POST GetSubjectWiseAttendance
        /// <summary>
        ///  Get SubjectWiseAttendance                 
        ///  classId as Int   
        ///  sectionId as Int   (Optional)
        ///  subjectId as Int
        ///  forDate as Date
        ///  inOutMode as int ( 1=In,2=Out,3=Both)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetSubjectWiseAttendance([FromBody] JObject para)
        {
            
            try
            {
                int classId = 0;
                int subjectId = 0;
                int? sectionId = null;
                DateTime? forDate = null;
                int inOutMode = 3;
                DateTime? fromDate = null, toDate = null;
                int? batchId = null;
                int? semesterId = null;
                int? classYearId = null;

                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("batchId"))
                        batchId = Convert.ToInt32(para["batchId"]);

                    if (para.ContainsKey("semesterId"))
                        semesterId = Convert.ToInt32(para["semesterId"]);


                    if (para.ContainsKey("classYearId"))
                        classYearId = Convert.ToInt32(para["classYearId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("forDate"))
                        forDate = Convert.ToDateTime(para["forDate"]);

                    if (para.ContainsKey("inOutMode"))
                        inOutMode = Convert.ToInt32(para["inOutMode"]);

                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("dateFrom"))
                        fromDate = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("dateTo"))
                        toDate = Convert.ToDateTime(para["dateTo"]);


                    if (forDate.HasValue)
                    {
                        var subjectColl = new AcademicLib.BL.Attendance.AttendanceSubjectWise(UserId, hostName, dbName).getClassWiseAttendance(this.AcademicYearId, classId, sectionId, subjectId, forDate.Value, inOutMode,batchId,semesterId,classYearId);

                        return Json(subjectColl, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                                IsInclude = true,
                                IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks","RegdNo","RollNo","Name","PhotoPath","ClassName","SectionName","Batch","Faculty","Semester","ClassYear","Level"
                                        }
                            }
                        });
                    }else
                    {
                        var subjectColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(UserId, hostName, dbName).getSubjectWiseAttendance(this.AcademicYearId, fromDate.Value, toDate.Value, classId, sectionId, subjectId,batchId,semesterId,classYearId);

                        return Json(subjectColl, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                                IsInclude = true,
                                IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks","Batch","Faculty","Semester","ClassYear","Level"
                                        }
                            }
                        });

                    }
                        

                }
                else
                    return BadRequest("No parameters found");





            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetDateWiseSubAttendanceSummary
        /// <summary>
        ///  Get DateWiseSubAttendanceSummary                 
        ///  classId as Int    (Optional)
        ///  sectionId as Int   (Optional)
        ///  subjectId as Int   (Optional)
        ///  fromDate as Date  (Optional)
        ///  toDate as Date  (Optional)        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.SubjectCollections))]
        public IHttpActionResult GetDateWiseSubAttendanceSummary([FromBody] JObject para)
        {
            AcademicLib.API.Teacher.AttendanceSummaryCollections subjectColl = new AcademicLib.API.Teacher.AttendanceSummaryCollections();
            try
            {
                int classId = 0;
                int? sectionId = null;
                int subjectId = 0;
                DateTime fromDate = DateTime.Today;
                DateTime toDate = DateTime.Today;

                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("dateFrom"))
                        fromDate = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("dateTo"))
                        toDate = Convert.ToDateTime(para["dateTo"]);

                    subjectColl = new AcademicLib.BL.Attendance.AttendanceSubjectWise(UserId, hostName, dbName).getClassWiseSummary(this.AcademicYearId, classId, sectionId,subjectId, fromDate, toDate);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //            {
                            //                "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                            //            }
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

        #region "Online Platform"
 
        // POST AddOnlinePlatform
        /// <summary>
        ///  Get AddOnlinePlatform                 
        ///  UserId as Int
        ///  PlatformType as Int
        ///  UserName as String
        ///  Pwd as String
        ///  Link  as String
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddOnlinePlatform([FromBody]AcademicLib.BE.OnlineClass.OnlinePlatform para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {                
                if (para != null)
                {
                    para.UserId = UserId;
                    para.CUserId = UserId;
                    para.EntityId = 0;
                    resVal = new AcademicLib.BL.OnlineClass.OnlinePlatform(UserId, hostName, dbName).SaveFormData(para);
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


        // POST GetOnlinePlatform
        /// <summary>
        ///  Get OnlinePlatform                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineClass.OnlinePlatformCollections))]
        public IHttpActionResult GetOnlinePlatform()
        {
            AcademicLib.BE.OnlineClass.OnlinePlatformCollections onlinePlatformColl = new AcademicLib.BE.OnlineClass.OnlinePlatformCollections();
            try
            {
                onlinePlatformColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(UserId, hostName, dbName).GetAllOnlinePlatform(0);
                return Json(onlinePlatformColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","PlatformType","UserName","Pwd","Link"
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

        #region "ClassShiftList"

        // POST GetClassShiftLit
        /// <summary>
        ///  Get ClassShiftLit                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.ClassShiftCollections))]
        public IHttpActionResult GetClassShiftLit([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.ClassShiftCollections shiftColl = new AcademicLib.BE.Academic.Transaction.ClassShiftCollections();
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

                shiftColl = new AcademicLib.BL.Academic.Transaction.ClassShift(UserId, hostName, dbName).GetAllClassShift(0,AcademicYearId,true);
                return Json(shiftColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","ClassShiftId","Name","WeeklyDayOff","StartTime","","EndTime","NoofBreak","Duration"
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
                int? classId = null, sectionId = null,batchId=null,classYearId=null,semesterId=null;
                int AcademicYearId;

                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("batchId"))
                        batchId = Convert.ToInt32(para["batchId"]);

                    if (para.ContainsKey("classYearId"))
                        classYearId = Convert.ToInt32(para["classYearId"]);
                    
                    if (para.ContainsKey("semesterId"))
                        semesterId = Convert.ToInt32(para["semesterId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

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

                scheduleColl = new AcademicLib.BL.Academic.Transaction.ClassSchedule(UserId, hostName, dbName).getClassSchedule(AcademicYearId, classId, sectionId,semesterId,classYearId,batchId);

                var query = from sc in scheduleColl
                            group sc by new { sc.DayId, sc.ClassId, sc.StartTime,sc.Batch,sc.ClassYear,sc.Semester,sc.Faculty,sc.Level } into g
                            select new
                            {
                                DayId=g.Key.DayId,
                                ClassId = g.Key.ClassId,
                                StartTime = g.Key.StartTime,
                                Batch=g.Key.Batch,
                                ClassYear=g.Key.ClassYear,
                                Semester=g.Key.Semester,
                                Faculty=g.Key.Faculty,
                                Level=g.Key.Level,
                                BatchId = g.First().BatchId,
                                ClassYearId = g.First().ClassYearId,
                                SemesterId = g.First().SemesterId,
                                DataColl = g
                            };

                AcademicLib.RE.Academic.ClassScheduleCollections tmpColl = new AcademicLib.RE.Academic.ClassScheduleCollections();
                tmpColl.IsSuccess = scheduleColl.IsSuccess;
                tmpColl.ResponseMSG = scheduleColl.ResponseMSG;
                foreach (var q in query)
                {
                    var f = q.DataColl.First();
                    AcademicLib.RE.Academic.ClassSchedule cs = new AcademicLib.RE.Academic.ClassSchedule();
                    cs.ClassId = q.ClassId;
                    cs.StartTime = q.StartTime;
                    cs.ShiftId = f.ShiftId;
                    cs.ShiftName = f.ShiftName;
                    cs.ShiftStartTime = f.ShiftStartTime;
                    cs.ShiftEndTime = f.ShiftEndTime;
                    cs.NoOfBreak = f.NoOfBreak;                    
                    cs.SectionId = f.SectionId;
                    cs.ClassName = f.ClassName;
                    cs.SectionName = f.SectionName;
                    cs.Batch = f.Batch;
                    cs.ClassYear = f.ClassYear;
                    cs.Semester = f.Semester;
                    cs.Faculty = f.Faculty;
                    cs.Level = f.Level;
                    cs.BatchId = f.BatchId;
                    cs.SemesterId = f.SemesterId;
                    cs.ClassYearId = f.ClassYearId;

                    cs.DayId = f.DayId;
                    cs.Period = f.Period;
                    cs.StartTime = f.StartTime;
                    cs.EndTime = f.EndTime;
                    cs.SubjectName = f.SubjectName;
                    cs.TeacherName = f.TeacherName;
                    cs.TeacherAddress = f.TeacherAddress;
                    cs.TeacherContactNo = f.TeacherContactNo;
                    cs.SubjectId = f.SubjectId;
                    cs.Duration = f.Duration;
                    cs.ForType = f.ForType;
                    cs.TeacherPhotoPath = f.TeacherPhotoPath;

                    cs.SectionName = "";
                    cs.SectionIdColl = "";

                    List<int> tmpSIDColl = new List<int>();
                    foreach(var dd in q.DataColl)
                    {
                        if (!tmpSIDColl.Contains(dd.SectionId))
                        {
                            if (!string.IsNullOrEmpty(cs.SectionName))
                            {
                                cs.SectionName = cs.SectionName + ",";
                                cs.SectionIdColl = cs.SectionIdColl + ",";
                            }


                            cs.SectionName = cs.SectionName + dd.SectionName;
                            cs.SectionIdColl = cs.SectionIdColl + dd.SectionId.ToString();
                            tmpSIDColl.Add(dd.SectionId);
                        }
                        
                    }

                    tmpColl.Add(cs);
                }

                return Json(tmpColl, new JsonSerializerSettings
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

        #region "ExamTypeList"

        // POST GetExamTypeList
        /// <summary>
        ///  Get ExamTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.ExamTypeCollections))]
        public IHttpActionResult GetExamTypeList([FromBody]JObject para)
        {
            AcademicLib.BE.Exam.Creation.ExamTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.ExamTypeCollections();
            try
            {
                int? forEntity = null;
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

                    if (para.ContainsKey("forEntity"))
                        forEntity = Convert.ToInt32(para["forEntity"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }
                examTypeColl = new AcademicLib.BL.Exam.Creation.ExamType(UserId, hostName, dbName).GetAllExamType(AcademicYearId, 0,forEntity);
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

        #region "ReExamTypeList"

        // POST GetReExamTypeList
        /// <summary>
        ///  Get ReExamTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.ExamTypeCollections))]
        public IHttpActionResult GetReExamTypeList([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.ReExamTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.ReExamTypeCollections();
            try
            {
                int AcademicYearId;
                int? forEntity = null;
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


                    if (para.ContainsKey("forEntity"))
                        forEntity = Convert.ToInt32(para["forEntity"]);
                }
                else
                {
                    AcademicYearId = this.AcademicYearId;
                }
                examTypeColl = new AcademicLib.BL.Exam.Creation.ReExamType(UserId, hostName, dbName).GetAllReExamType(AcademicYearId, 0, forEntity);
                return Json(examTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","ExamTypeId","ReExamTypeId","Name","DisplayName","ResultDate","ResultTime","ExamDate","StartTime","Duration"
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
                                            "ResponseMSG","IsSuccess","ExamTypeGroupId","ExamTypeId","Name","DisplayName","ResultDate","ResultTime"
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
                int? classId = null, examTypeId = null;
                string sectionIdColl = "";
                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                    {
                        classId = ToIntNull(para["classId"].ToString());
                    }
                        

                    if (para.ContainsKey("sectionIdColl"))
                    {
                        sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                        if (sectionIdColl == "0")
                            sectionIdColl = "";
                    }
                        

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = ToIntNull(para["examTypeId"].ToString());
                }
                scheduleColl = new AcademicLib.BL.Exam.Transaction.ExamSchedule(UserId, hostName, dbName).GetExamSchedule(this.AcademicYearId, classId, sectionIdColl, examTypeId);

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

        #region "Online Class"


        // POST StartOnlineClass
        /// <summary>
        ///  Get AddOnlinePlatform                 
        ///  UserId as Int
        ///  PlatformType as Int
        ///  UserName as String
        ///  Pwd as String
        ///  Link  as String
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult StartOnlineClass([FromBody] AcademicLib.API.Teacher.OnlineClass para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para != null)
                {
                    para.UserId = UserId;
                    para.StartDateTime = DateTime.Now;
                    if(!string.IsNullOrEmpty(para.SectionIdColl))
                    {
                        if (para.SectionIdColl.ToString() == "0")
                            para.SectionIdColl = "";
                    }
                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).StartOnlineClass(para);

                    if (string.IsNullOrEmpty(resVal.ResponseId) && resVal.IsSuccess)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "No Student Found.";
                    }
                    else if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                        notification.Content = resVal.ResponseMSG;
                        notification.ContentPath = resVal.RId.ToString();
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ONLINE_CLASS_START);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ONLINE_CLASS_START.ToString();
                        notification.Heading = "Online Class";
                        notification.Subject = "Online Class";
                        notification.UserId = UserId;
                        notification.UserName = User.Identity.Name;                        
                        notification.UserIdColl = resVal.ResponseId.Trim();

                        if (ActiveBranch)
                            notification.BranchCode = this.BranchCode;

                        var res1= new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

                        if (!res1.IsSuccess)
                            resVal.ResponseMSG = res1.ResponseMSG;
                    }

                    return Json(resVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = true,
                            IncludeProperties = new List<string>
                                        {
                                "ResponseMSG","IsSuccess","RId"
                                //"ResponseMSG","IsSuccess","RId","ResponseId"
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

        // POST EndOnlineClass
        /// <summary>
        ///  Get EndOnlineClass                 
        ///  tranId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EndOnlineClass([FromBody]ResponeValues req)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int tranId = 0;

                if (req != null)
                {
                    if (req.RId != 0)
                        tranId = req.RId;
                }

                if (tranId != 0)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).EndOnlineClass(tranId);
                    if(resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                        notification.Content = resVal.ResponseMSG;
                        notification.ContentPath = tranId.ToString();
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ONLINE_CLASS_END);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ONLINE_CLASS_END.ToString();
                        notification.Heading = "Online Class End";
                        notification.Subject = "Subject Name";
                        notification.UserId = UserId;
                        notification.UserName = User.Identity.Name;
                        notification.UserIdColl = resVal.ResponseId;

                        if (ActiveBranch)
                            notification.BranchCode = this.BranchCode;

                        new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);
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
                else
                    return BadRequest("No parameters found");

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }



        // POST GetDateWiseOnlineAttendance
        /// <summary>
        ///  Get GetDateWiseOnlineAttendance                 
        ///  classId as Int   
        ///  sectionId as Int    
        ///  forDate as Date        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections))]
        public IHttpActionResult GetDateWiseOnlineAttendance([FromBody] JObject para)
        {
            AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections subjectColl = new AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections();
            try
            {
                int? classId = null;
                int? sectionId = null;
                DateTime forDate = DateTime.Today;
                
                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("forDate"))
                        forDate = Convert.ToDateTime(para["forDate"]);

                    subjectColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(UserId, hostName, dbName).getDateWiseAttendance(forDate, classId, sectionId);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //            {
                            //                "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                            //            }
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

        // POST GetSubjectWiseOnlineAttendance
        /// <summary>
        ///  Get GetSubjectWiseOnlineAttendance                 
        ///  classId as Int   
        ///  sectionId as Int    
        ///  forDate as Date        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections))]
        public IHttpActionResult GetSubjectWiseOnlineAttendance([FromBody] JObject para)
        {
            AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections subjectColl = new AcademicLib.RE.OnlineClass.DateWiseAttendanceCollections();
            try
            {
                int? classId = null;
                int? sectionId = null;
                int subjectId = 0;
                DateTime fromDate = DateTime.Today;
                DateTime toDate = DateTime.Today;

                if (para != null)
                {
                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("fromDate"))
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("toDate"))
                        toDate = Convert.ToDateTime(para["toDate"]);

                    subjectColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(UserId, hostName, dbName).getSubjectWiseAttendance(fromDate, toDate, classId, sectionId, subjectId);
                    return Json(subjectColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            //IsInclude = true,
                            //IncludeProperties = new List<string>
                            //            {
                            //                "ResponseMSG","IsSuccess","StudentId","Attendance","LateMin","Remarks"
                            //            }
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

        #region "Running ClassList"

        // POST GetRunningClasses
        /// <summary>
        ///  Get Running Classes List   
        ///  tranId as Int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.RunningClassCollections))]
        public IHttpActionResult GetRunningClasses([FromBody]ResponeValues req)
        {
            AcademicLib.RE.Academic.RunningClassCollections classColl = new AcademicLib.RE.Academic.RunningClassCollections();
            try
            {
                int? tranId = null;

                if (req != null)
                {
                    if(req.RId!=0)
                        tranId = req.RId;
                }

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

        #region "Colleagues Running ClassList"

        // POST GetColleaguesRunningClasses
        /// <summary>
        ///  Get  GetColleaguesRunningClasses      
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.RunningClassCollections))]
        public IHttpActionResult GetColleaguesRunningClasses([FromBody] ResponeValues req)
        {
            AcademicLib.RE.Academic.RunningClassCollections classColl = new AcademicLib.RE.Academic.RunningClassCollections();
            try
            {
               
                classColl = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getColleaguesRunningClassList();
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

        // POST GetMissedClassList
        /// <summary>
        ///  Get Missed Class List
        ///  forDate as Date        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.PassedOnlineClassCollections))]
        public IHttpActionResult GetMissedClassList([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.OnlineClassAdmin classColl = new AcademicLib.RE.Academic.OnlineClassAdmin();
            try
            {
                DateTime? forDate = DateTime.Today;
                

                if (para == null)
                {
                    return BadRequest("No form data found");
                }

                if (para.ContainsKey("forDate"))
                    forDate = Convert.ToDateTime(para["dateFrom"]);


                classColl = new AcademicLib.BL.Academic.Reporting.OnlineClass(UserId, hostName, dbName).getMissedClassList(forDate);

                if (classColl != null && classColl.MissedColl.Count > 0)
                {
                    var query = new
                    {
                        ForDate = forDate,
                        DataColl = classColl.MissedColl,
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

                if(classColl!=null && classColl.Count > 0)
                {
                    var query = from cc in classColl
                                group cc by cc.ForDate into g
                                select new
                                {
                                    ForDate=g.Key,
                                    Date_AD=g.First().StartDateTime_AD,
                                    Date_BS=g.First().StartDate_BS,
                                    DataColl=g,
                                    IsSuccess= classColl.IsSuccess,
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

        #region "Rpt OnlineClasses"

        // POST RptOnlineClasses
        /// <summary>
        ///  Get Live Running Class List
        ///  forDate as Date        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.PassedOnlineClassCollections))]
        public IHttpActionResult RptOnlineClasses([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.OnlineClassAdmin classColl = new AcademicLib.RE.Academic.OnlineClassAdmin();
            try
            {
                DateTime? forDate = DateTime.Today;

                if (para != null)
                {
                    if (para.ContainsKey("forDate"))
                        forDate = Convert.ToDateTime(para["forDate"]);
                   
                }

                classColl = new AcademicLib.BL.Academic.Reporting.OnlineClass(UserId,hostName, dbName).getClassList(forDate);
                
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

        #region "Get Online Class Attendance By Id"

        // POST GetOnlineClassAttById
        /// <summary>
        ///  Get Online Class Attendance By TranId of OnlineClass
        ///  tranId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.OnlineClasssAttendanceCollections))]
        public IHttpActionResult GetOnlineClassAttById([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            AcademicLib.RE.Academic.OnlineClasssAttendanceCollections classColl = new AcademicLib.RE.Academic.OnlineClasssAttendanceCollections();
            try
            {
                int tranId = 0;

                if (para == null)
                {
                    return BadRequest("No form data found");
                }

                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);


                classColl = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getOnlineClassAttendanceById(tranId);
                resVal.IsSuccess = classColl.IsSuccess;
                resVal.ResponseMSG = classColl.ResponseMSG;
                if (classColl != null && classColl.Count > 0)
                {
                    var query = new
                    {
                        PresentColl=classColl.Where(p1=>p1.AttendanceType==1 || p1.AttendanceType==3),
                        AbsentColl=classColl.Where(p1=>p1.AttendanceType==2),
                        LateColl=classColl.Where(p1=>p1.AttendanceType==3),
                        LeaveColl=classColl.Where(p1=>p1.AttendanceType==4),
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

                }else
                {
                    List<string> tmpColl = new List<string>();
                    var query = new
                    {
                        PresentColl =tmpColl,
                        AbsentColl = tmpColl,
                        LateColl = tmpColl,
                        LeaveColl = tmpColl,
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
                return Json(resVal, new JsonSerializerSettings
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

        #region "Online Exam ( Exam Modal )"

        // POST AddExamModal
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamModal([FromBody] AcademicLib.BE.OnlineExam.ExamModal para)
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
                    resVal = new AcademicLib.BL.OnlineExam.ExamModal(UserId, hostName, dbName).SaveFormData(para);
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

        // POST GetAllExamModal
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.ExamModalCollections))]
        public IHttpActionResult GetAllExamModal()
        {           
            try
            {

                var dataColl = new AcademicLib.BL.OnlineExam.ExamModal(UserId, hostName, dbName).GetAllExamModal(0);
               
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","CategoryId","ExamModalType","OrderNo","CategoryName","Description","NumberingMethod"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST GetExamModalById
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.ExamModal))]
        public IHttpActionResult GetExamModalById([FromBody]JObject para)
        {
            try
            {
                int categoryId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("categoryId"))
                        categoryId = Convert.ToInt32(para["categoryId"]);
                }
                var dataColl = new AcademicLib.BL.OnlineExam.ExamModal(UserId, hostName, dbName).GetExamModalById(0,categoryId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","CategoryId","ExamModalType","OrderNo","CategoryName","Description","NumberingMethod"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST DelExamModalById
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult DelExamModalById([FromBody] JObject para)
        {
            try
            {
                int categoryId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("categoryId"))
                        categoryId = Convert.ToInt32(para["categoryId"]);
                }
                var dataColl = new AcademicLib.BL.OnlineExam.ExamModal(UserId, hostName, dbName).DeleteById(0, categoryId);

                return Json(dataColl, new JsonSerializerSettings
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

        #region "Online Exam ( Exam Setup )"

        // POST AddExamSetup
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamSetup([FromBody] AcademicLib.BE.OnlineExam.ExamSetup para)
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
                    resVal = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).SaveFormData(para);
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


        // POST GetAllExamSetup
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineExam.ExamSetupCollections))]
        public IHttpActionResult GetAllExamSetup()
        {
            try
            {

                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).GetAllExamSetup(0);

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

        // POST GetOnlineExamList
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineExam.ExamListCollections))]
        public IHttpActionResult GetOnlineExamList([FromBody]JObject para)
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

        // POST GetOnlineExamListForEvaluate
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.OnlineExam.ExamListCollections))]
        public IHttpActionResult GetOnlineExamListForEvaluate([FromBody] JObject para)
        {
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int examTypeId = 0, classId = 0, subjectId = 0;
                int? sectionId = null;

                if (para != null)
                {
                    if (para.ContainsKey("dateFrom"))
                    {
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);
                    }
                    if (para.ContainsKey("dateTo"))
                    {
                        dateTo = Convert.ToDateTime(para["dateTo"]);
                    }
                    if (para.ContainsKey("examTypeId"))
                    {
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                    }
                    if (para.ContainsKey("classId"))
                    {
                        classId = ToInt(para["classId"]);
                    }
                    //if (para.ContainsKey("sectionId"))
                    //{
                    //    sectionId = ToIntNull(para["sectionId"]);
                    //}
                    if (para.ContainsKey("subjectId"))
                    {
                        subjectId = ToInt(para["subjectId"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).getExamListForEvaluate(dateFrom, dateTo, examTypeId, classId, sectionId, subjectId);

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

        // POST GetOnlineExamList
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.OnlineExam.StudentForEvaluateCollections))]
        public IHttpActionResult GetStudentForEvaluatet([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0;
                int classId = 0;
                int? sectionId = null;

                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                    {
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                    }
                    if (para.ContainsKey("classId"))
                    {
                        classId = ToInt(para["classId"]);
                    }
                    if (para.ContainsKey("sectionId"))
                    {
                        if(para["sectionId"]!=null)
                            sectionId = ToIntNull(para["sectionId"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).getStudentForEvaluate(examSetupId,classId,sectionId);

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

        // POST GetOnlineExamQuestion
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.QuestionSetupCollections))]
        public IHttpActionResult GetOnlineExamAnswer([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0,studentId=0;
                bool returnTypeAsList = true;
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                    {
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                    }
                    if (para.ContainsKey("studentId"))
                    {
                        studentId = Convert.ToInt32(para["studentId"]);
                    }
                    if (para.ContainsKey("returnTypeAsList"))
                    {
                        returnTypeAsList = Convert.ToBoolean(para["returnTypeAsList"]);
                    }
                }

                var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).getQuestionListForAPI(examSetupId,studentId);

                if (returnTypeAsList)
                {
                    if (studentId > 0)
                    {
                        return Json(dataColl, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                                IsInclude = true,
                                IncludeProperties = new List<string>
                                        {
                                            "TranId","QNo","Marks","QuestionTitle","Question","QuestionPath","DetailsColl","SNo","Answer","FilePath","CategoryName","ExamModal","ResponseMSG","IsSuccess","SubmitType","QuestionRemarks","StudentAnswerNo","AnswerText","SNo_Str","FileType","FileCount","StudentDocsPath","StudentDocColl","IsCorrect","AnswerSNo","IsRightAnswer","OETranId","ObtainMark","Remarks","IsChecked"
                                        }
                            }
                        });
                    }
                    else
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

                }
                else
                {
                    var returnVal = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };


                    if (studentId > 0)
                    {
                        return Json(returnVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                                IsInclude = true,
                                IncludeProperties = new List<string>
                                        {
                                            "DataColl","IsSuccess","ResponseMSG","TranId","QNo","Marks","QuestionTitle","Question","QuestionPath","DetailsColl","SNo","Answer","FilePath","CategoryName","ExamModal","ResponseMSG","IsSuccess","SubmitType","QuestionRemarks","StudentAnswerNo","AnswerText","SNo_Str","FileType","FileCount","StudentDocsPath","StudentDocColl","IsCorrect","AnswerSNo","IsRightAnswer","OETranId","ObtainMark","Remarks","IsChecked"
                                        }
                            }
                        });
                    }
                    else
                    {
                        return Json(returnVal, new JsonSerializerSettings
                        {
                            ContractResolver = new JsonContractResolver()
                            {
                                IsInclude = true,
                                IncludeProperties = new List<string>
                                        {
                                            "DataColl","IsSuccess","ResponseMSG","TranId","QNo","Marks","QuestionTitle","Question","QuestionPath","DetailsColl","SNo","Answer","FilePath","CategoryName","ExamModal","ResponseMSG","IsSuccess","SubmitType","QuestionRemarks","StudentAnswerNo","AnswerText","SNo_Str","FileType","FileCount","StudentDocsPath","StudentDocColl"
                                        }
                            }
                        });

                    }
                }
              
               

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

        // POST GetExamSetupById
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.ExamModal))]
        public IHttpActionResult GetExamSetupById([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                }
                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).GetExamSetupById(0, examSetupId);

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

        // POST DelExamSetupById
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult DelExamSetupById([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                        examSetupId = Convert.ToInt32(para["examSetupId"]);
                }
                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).DeleteById(0, examSetupId);

                return Json(dataColl, new JsonSerializerSettings
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

        // POST GetExamSetupDetails
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.ExamModal))]
        public IHttpActionResult GetExamSetupDetails([FromBody] JObject para)
        {
            try
            {
                int examTypeId = 0,classId=0,subjectId=0;
                string sectionIdColl = "";
                if (para != null)
                {
                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("classId"))
                        classId =ToInt(para["classId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionIdColl"))
                    {
                        sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                        if (sectionIdColl == "0")
                            sectionIdColl = "";
                    }
                        
                }
                var dataColl = new AcademicLib.BL.OnlineExam.ExamSetup(UserId, hostName, dbName).getExamSetupDetails(examTypeId, classId, sectionIdColl, subjectId);

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

        #endregion

        #region "Online Exam ( Question Setup )"

        // POST AddQuestionSetup
        /// <summary>                          
        /// </summary>
        /// <returns></returns>       
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddQuestionSetup()
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

                    AcademicLib.BE.OnlineExam.QuestionSetup para = DeserializeObject<AcademicLib.BE.OnlineExam.QuestionSetup>(jsonData);
                    para.TranId = 0;
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

                            foreach(var dc in DocumentColl)
                            {
                                if (dc.ParaName.Contains("question"))
                                {
                                    para.QuestionPath = dc.DocPath;
                                }                                    
                            }

                            foreach(var v in para.DetailsColl)
                            {
                                var doc = DocumentColl.Find(p1 => p1.ParaName.ToLower() == "answer" + v.SNo.ToString());
                                if (doc != null)
                                {
                                    v.FilePath = doc.DocPath;
                                }
                            }                            
                        }
                        resVal = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).SaveFormData(para);
                      
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

        // POST GetQuestionByExamSetupId
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.QuestionSetupCollections))]
        public IHttpActionResult GetQuestionByExamSetupId([FromBody] JObject para)
        {
            try
            {
                int examSetupId = 0,categoryId=0;
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                        examSetupId = Convert.ToInt32(para["examSetupId"]);

                    if (para.ContainsKey("categoryId"))
                        categoryId = Convert.ToInt32(para["categoryId"]);

                }
                var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).getQuestionSetupByExamSetupId(examSetupId, categoryId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                        {
                                            "RId","CUserId","ResponseId","EntityId","ErrorNumber","TranId"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST GetQuestionList
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.OnlineExam.QuestionSetupCollections))]
        public IHttpActionResult GetQuestionList([FromBody] JObject para)
        {
            try
            {
                int examTypeId = 0, classId = 0, subjectId = 0,examSetupId=0;
                string sectionIdColl = "";
                if (para != null)
                {
                    if (para.ContainsKey("examSetupId"))
                        examSetupId = Convert.ToInt32(para["examSetupId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionIdColl"))
                    {
                        sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                        if (sectionIdColl == "0")
                            sectionIdColl = "";
                    }
                        
                }
                var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).getQuestionSetupList(examTypeId, classId, sectionIdColl, subjectId,examSetupId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                        {
                                            "RId","CUserId","ResponseId","EntityId","ErrorNumber"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST DelQuestionSetup
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult DelQuestionSetup([FromBody] JObject para)
        {
            try
            {
                int tranId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("tranId"))
                        tranId = Convert.ToInt32(para["tranId"]);
                }
                var dataColl = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).DeleteById(0, tranId);

                return Json(dataColl, new JsonSerializerSettings
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


        // Post api/CheckExamCopy
        /// <summary>
        ///  Checked Exam Copy
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> CheckExamCopy()
        {
            string basePath = GetPath("~");
            string path = GetPath("~/Attachments/onlineexam");
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
                    var provider = new FormDataStreamProvider(path);
                    var task = await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.ExamCopyCheck para = DeserializeObject<AcademicLib.API.Teacher.ExamCopyCheck>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (!para.OETranId.HasValue)
                    {
                        resVal.ResponseMSG = "Invalid Exam Copy";
                    }
                    else if (para.OETranId.Value == 0)
                    {
                        resVal.ResponseMSG = "Invalid Exam Copy";
                    }
                    else
                    {
                        para.UserId = UserId;
                        bool validFile = true;
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);


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

                                }
                                else
                                {
                                    validFile = false;
                                    resVal.ResponseMSG = "Invalid File Name";
                                }
                            }

                        }
                        //else
                        //{
                        //    validFile = false;
                        //    resVal.ResponseMSG = "No Attachment found";
                        //}

                        if (validFile)
                            resVal = new AcademicLib.BL.OnlineExam.QuestionSetup(UserId, hostName, dbName).ExamCopyCheck(para);
                       
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

        #region "Mark Entry"


        // POST GetStudentForExamComment
        /// <summary>     
        /// GetStudentForExamComment Get Student List For Student Wise Comment as per result
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetStudentForExamComment([FromBody] JObject para)
        {
            AcademicLib.API.Teacher.ExamWiseCommentCollections dataColl = new AcademicLib.API.Teacher.ExamWiseCommentCollections();
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    int classId = 0, examTypeId = 0;
                    int? sectionId = null;
                    int AcademicYearId;

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
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
                   
                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);
                     
                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getStudentForTeacherComment(UserId,classId,sectionId, AcademicYearId, examTypeId);
                    resVal.IsSuccess = dataColl.IsSuccess;
                    resVal.ResponseMSG = dataColl.ResponseMSG;


                }

                var retVal = new
                {
                    IsSucccess=resVal.IsSuccess,
                    ResponseMSG=resVal.ResponseMSG,
                    DataColl=dataColl
                };
                return Json(retVal, new JsonSerializerSettings
                {                    
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }



        // POST AddMarkEntry
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateExamComment([FromBody] AcademicLib.API.Teacher.ExamWiseCommentCollections para)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                if (para == null || para.Count==0)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).UpdateExamComment(para);
                }
                return Json(resVal, new JsonSerializerSettings
                {                   
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // POST GetStudentForSubjectME
        /// <summary>     
        /// GetStudentListForMarkEntrySubjectWise
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetStudentForSubjectME([FromBody] JObject para)
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
                    int classId = 0,examTypeId=0,subjectId=0;
                    int? sectionId = null;
                    bool filterSection = true;
                    int AcademicYearId;

                    int? batchId = null;
                    int? semesterId = null;
                    int? classYearId = null;


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

                    if (para.ContainsKey("batchId"))
                        batchId = Convert.ToInt32(para["batchId"]);

                    if (para.ContainsKey("semesterId"))
                        semesterId = Convert.ToInt32(para["semesterId"]);


                    if (para.ContainsKey("classYearId"))
                        classYearId = Convert.ToInt32(para["classYearId"]);


                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("filterSection"))
                        filterSection = Convert.ToBoolean(para["filterSection"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getStudentForMarkEntrySubWise(AcademicYearId, classId, sectionId, examTypeId, subjectId, filterSection,semesterId,classYearId,batchId);

                    if (dataColl.IsSuccess)
                    {
                        return Json(dataColl, new JsonSerializerSettings
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


        // POST IsValidForMarkEntry
        /// <summary>     
        /// IsValidForMarkEntry
        /// examTypeId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult IsValidForMarkEntry([FromBody] JObject para)
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
                    int   examTypeId = 0 ;
                     
                     
                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);
 
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).IsValidForMarkEntry(examTypeId);
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




        // POST AddMarkEntry
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddMarkEntry([FromBody] AcademicLib.API.Teacher.MarkEntryCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).SaveMarkEntry(para);
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


        // POST AddExamComment
        /// <summary> 
        ///  Update Student Wise Comment For Particular Exam
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamComment([FromBody] AcademicLib.API.Teacher.StudentWiseCommentCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).SaveStudentWiseComment(para);
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

        #endregion

        #region "Re-Exam Mark Entry"

        // POST GetStudentForSubjectReME
        /// <summary>     
        /// Get StudentForSubjectReME
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetStudentForSubjectReME([FromBody] JObject para)
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
                    int classId = 0, examTypeId = 0, subjectId = 0,reExamTypeId=0;
                    int? sectionId = null;

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("reExamTypeId"))
                        reExamTypeId = Convert.ToInt32(para["reExamTypeId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getStudentForReMarkEntry(classId, sectionId, examTypeId,reExamTypeId, subjectId);

                    if (dataColl.IsSuccess)
                    {
                        return Json(dataColl, new JsonSerializerSettings
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


        // POST IsValidForReMarkEntry
        /// <summary>     
        /// IsValidForReMarkEntry
        /// examTypeId as Int
        /// reExamTypeId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult IsValidForReMarkEntry([FromBody] JObject para)
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
                    int examTypeId = 0,reExamTypeId=0;


                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("reExamTypeId"))
                        reExamTypeId = Convert.ToInt32(para["reExamTypeId"]);

                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).IsValidForReMarkEntry(examTypeId,reExamTypeId);
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




        // POST AddReMarkEntry
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddReMarkEntry([FromBody] AcademicLib.API.Teacher.MarkEntryCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).SaveReMarkEntry(para);
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

        #endregion

        #region "Exam Wise Attendance"
        // POST GetStudentForExamAttendance
        /// <summary>     
        /// Get Student For Exam Wise Attendance
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetStudentForExamAttendance([FromBody] JObject para)
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
                    int classId = 0, examTypeId = 0, subjectId = 0;
                    int? sectionId = null;

                    if (para.ContainsKey("classId"))
                        classId =ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.ExamAttendance(UserId, hostName, dbName).getStudentForExamAttendance(this.AcademicYearId, classId, sectionId, examTypeId);

                    if (dataColl.IsSuccess)
                    {
                        return Json(dataColl, new JsonSerializerSettings
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

        // POST AddExamWiseAttendance
        /// <summary>    
        /// Save / Update Exam Wise Attendance
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamWiseAttendance([FromBody] AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamAttendance(UserId, hostName, dbName).SaveFormData(para);
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
                    int? classId = null;
                    int? sectionId = null;

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);

                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                        sectionId = ToIntNull(para["sectionId"]);

                    int academicYearId = 0;
                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);
                    else if (para.ContainsKey("AcademicYearId") && para["AcademicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);

                    if (academicYearId == 0)
                        academicYearId = this.AcademicYearId;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getMarkSheetClassWise(academicYearId, studentId, classId,sectionId, examTypeId,true,"");

                    if (dataColl.IsSuccess)
                    {
                        var retVal = new
                        {
                            ResponseMSG = dataColl.ResponseMSG,
                            IsSuccess = dataColl.IsSuccess,
                            DataColl =  dataColl
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

        // POST v1/PrintMarkSheet
        /// <summary>        
        /// examTypeId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintMarkSheet([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int examTypeId = 0;
                if (para.ContainsKey("examTypeId"))
                    examTypeId = Convert.ToInt32(para["examTypeId"]);

                int studentId = 0;
                int classId = 0;
                int sectionId = 0;

                if (para.ContainsKey("studentId") && para["studentId"] != null)
                    studentId = Convert.ToInt32(para["studentId"]);

                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = ToInt(para["sectionId"]);

                string format = "pdf";
                if (para.ContainsKey("format"))
                    format = Convert.ToString(para["format"]);

                if (examTypeId == 0)
                {
                    return BadRequest("Please ! Select Valid ExamType Name");
                }

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.Marksheet;
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
                    paraColl.Add("StudentId",studentId.ToString());
                    paraColl.Add("ClassId", classId.ToString());
                    paraColl.Add("SectionId", sectionId.ToString());
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("ExamTypeId", examTypeId.ToString());
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
                            string basePath = "print-tran-log//" + Guid.NewGuid().ToString().Replace("-","") + ".pdf";
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
                            printLog.LogText = "Print Normal Marksheet of Teacher id=" + UserId.ToString();
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
                    int? classId = null;
                    int? sectionId = null;

                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);

                    if (para.ContainsKey("classId") && para["classId"] != null)
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                        sectionId = ToIntNull(para["sectionId"]);

                    int academicYearId = 0;
                    if (para.ContainsKey("academicYearId") && para["academicYearId"] != null)
                        academicYearId = ToInt(para["AcademicYearId"]);

                    if (academicYearId == 0)
                        academicYearId = this.AcademicYearId;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getGroupMarkSheetClassWise(academicYearId, studentId, classId, sectionId, examTypeGroupId,true,null);

                    if (dataColl.IsSuccess)
                    {
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

        // POST v1/PrintMarkSheet
        /// <summary>        
        /// examTypeId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintGroupMarkSheet([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int examTypeGroupId = 0;
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

                int studentId = 0;
                int classId = 0;
                int sectionId = 0;

                if (para.ContainsKey("studentId") && para["studentId"] != null)
                    studentId = Convert.ToInt32(para["studentId"]);

                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("sectionId") && para["sectionId"] != null)
                    sectionId = ToInt(para["sectionId"]);

                if (examTypeGroupId == 0)
                {
                    return BadRequest("Please ! Select Valid ExamType Name");
                }

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.GroupMarksheet;
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
                    paraColl.Add("StudentId", studentId.ToString());
                    paraColl.Add("ClassId", classId.ToString());
                    paraColl.Add("SectionId", sectionId.ToString());
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("ExamTypeGroupId", examTypeGroupId.ToString());
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
                            string basePath = "print-tran-log//" + Guid.NewGuid().ToString().Replace("-", "") + ".pdf";
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
                            printLog.LogText = "Print Normal Marksheet of Teacher id=" + UserId.ToString();
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

        #region "Employee Profile"

        // POST UpdatePersonalInfo
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdatePersonalInfo([FromBody] AcademicLib.API.Teacher.PersonalInformation para)
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
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.Employee_Information);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdatePersonalInfo(para);
                    }
                    //resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdatePersonalInfo(para);
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
        public IHttpActionResult UpdatePermanentAddress([FromBody] AcademicLib.API.Teacher.Emp_PermananetAddress para)
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
                    //resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdatePermanentAddress(para);
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.Permanent_Address);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdatePermanentAddress(para);
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
        public IHttpActionResult UpdateTemporaryAddress([FromBody] AcademicLib.API.Teacher.Emp_TemporaryAddress para)
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
                    //resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateTemporaryAddress(para);
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.Temporary_Address);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateTemporaryAddress(para);
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
        public IHttpActionResult UpdateCitizenship([FromBody] AcademicLib.API.Teacher.CitizenshipDetails para)
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
                    //resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateCitizenship(para);
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.Identification_Number);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateCitizenship(para);
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
        public IHttpActionResult UpdateCITDetails([FromBody] AcademicLib.API.Teacher.CITDetails para)
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
                    //resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateCIT(para);
                    resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.CIT);
                    if (resVal.IsSuccess)
                    {
                        resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdateCIT(para);
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
                if (!Request.Content.IsMimeMultipartContent())
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = HttpStatusCode.UnsupportedMediaType.ToString();
                }
                else
                {
                    int? EmployeeId = null;
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/Academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    try
                    {
                        string jsonData = provider.FormData["paraDataColl"];
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            var beData = DeserializeObject<JObject>(jsonData);
                            EmployeeId = ToIntNull(beData["EmployeeId"]);
                        }
                    }
                    catch { }

                    if (!EmployeeId.HasValue)
                    {
                        resVal = new AcademicLib.BL.FormEntity.EntityFieldsAllow(UserId, hostName, dbName).CheckAllowFields(0, (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeProfile, (int)AcademicLib.BE.FormEntity.EmployeeProfile.Employee_Photo);                       
                    }
                    else
                    {
                        resVal.IsSuccess = true;
                    }

                    if (resVal.IsSuccess)
                    {
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                var photo = DocumentColl[0];
                                resVal = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).UpdatePhoto(photo.DocPath,EmployeeId);
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
        #endregion

        #region "Transport"
        // POST GetRouteList
        /// <summary>
        ///  Get RouteList 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Teacher.TransportRoute))]
        public IHttpActionResult GetRouteList()
        {
            try
            {
                var routeList = new AcademicLib.BL.Transport.Creation.TransportPoint(UserId, hostName, dbName).getAllPickupPointsForMap();

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



        // POST SendNotificationToPP
        /// <summary>
        ///  Send notification to Nearest location of transport point                  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendNotificationToPP([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var glb = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                string pointIdColl = "";
                if (para.ContainsKey("pointIdColl") && para["pointIdColl"] != null)
                    pointIdColl = Convert.ToString(para["pointIdColl"]);

                string nextPointIdColl = "";
                if (para.ContainsKey("nextPointIdColl") && para["nextPointIdColl"] != null)
                    nextPointIdColl = Convert.ToString(para["nextPointIdColl"]);

                string pastPointIdColl = "";
                if (para.ContainsKey("pastPointIdColl") && para["pastPointIdColl"] != null)
                    pastPointIdColl = Convert.ToString(para["pastPointIdColl"]);

                string msg = "";
                if (para.ContainsKey("msg") && para["msg"] != null)
                    msg = Convert.ToString(para["msg"]);

                int msgFor = 1;
                if (para.ContainsKey("msgFor") && para["msgFor"] != null)
                    msgFor = Convert.ToInt32(para["msgFor"]);

                

                int eId = 0;
                string eName = "";
                switch (msgFor)
                {
                    case 1:
                        eId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.TRANSPORT_START_FOR_PICKUP);
                        eName = "TRANSPORT_START_FOR_PICKUP";
                        break;
                    case 2:
                        eId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.TRANSPORT_PICKUP);
                        eName = "TRANSPORT_PICKUP";
                        break;
                    case 3:
                        eId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.TRANSPORT_DROP);
                        eName = "TRANSPORT_DROP";
                        break;
                    case 4:
                        eId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.TRANSPORT_START_FOR_DROP);
                        eName = "TRANSPORT_START_FOR_DROP";
                        break;
                }

                Dynamic.BusinessEntity.Global.AuditLog auditLog = new Dynamic.BusinessEntity.Global.AuditLog();
                auditLog.TranId = 0;
                auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.TransportPickupDrop;
                auditLog.Action = Dynamic.BusinessEntity.Global.Actions.Save;
                auditLog.LogText = eName+"pointId:"+ pointIdColl+" nextPointId:"+nextPointIdColl+ " pastPointIdColl:"+pastPointIdColl+ " msg:"+msg;
                auditLog.AutoManualNo = eId.ToString();
                SaveAuditLog(auditLog);

                var userColl = new AcademicLib.BL.Transport.Creation.TransportPoint(UserId, hostName, dbName).getPickUpMSG(pointIdColl,msgFor, nextPointIdColl,  pastPointIdColl);
                
                List<Dynamic.BusinessEntity.Global.NotificationLog> notificationColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();
                Dynamic.BusinessEntity.Global.NotificationLog notificationAdmin = new Dynamic.BusinessEntity.Global.NotificationLog();
                notificationAdmin.Content = eName;
                notificationAdmin.ContentPath = resVal.RId.ToString();
                notificationAdmin.EntityId = eId;
                notificationAdmin.EntityName = eName;
                notificationAdmin.Heading = "Transport";
                notificationAdmin.Subject = "Transport";
                notificationAdmin.UserId = UserId;
                notificationAdmin.UserName = User.Identity.Name;
                notificationAdmin.UserIdColl = "1";
                if (ActiveBranch)
                    notificationAdmin.BranchCode = this.BranchCode;

                notificationColl.Add(notificationAdmin);


                string userIdColl = "";
                foreach (var v in userColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                    notification.Content =(string.IsNullOrEmpty(msg) ? v.Message : msg);
                    notification.ContentPath = resVal.RId.ToString();
                    notification.EntityId = eId;
                    notification.EntityName =eName;
                    notification.Heading = "Transport";
                    notification.Subject = "Transport";
                    notification.UserId = UserId;
                    notification.UserName = User.Identity.Name;
                    notification.UserIdColl = v.UserId.ToString();

                    if (ActiveBranch)
                        notification.BranchCode = this.BranchCode;

                    notificationColl.Add(notification);
                    userIdColl = userIdColl + "," + v.UserId.ToString();
                }

                auditLog = new Dynamic.BusinessEntity.Global.AuditLog();
                auditLog.TranId = 0;
                auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.TransportPickupDrop;
                auditLog.Action = Dynamic.BusinessEntity.Global.Actions.Save;
                auditLog.LogText = eName +"count:"+userColl.Count.ToString()+" ResMSG:"+userColl.ResponseMSG.ToString()+" userIdColl:" + userIdColl;
                auditLog.AutoManualNo = eId.ToString();
                SaveAuditLog(auditLog);

                resVal = glb.SendNotification(UserId, notificationColl, true);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG ="Er:"+ ee.Message;
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

        #endregion
         
        #region "Add Student Remarks"


        // Post api/AddStudentRemarks
        /// <summary>
        ///  Add Student Remarks    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddStudentRemarks()
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

                    AcademicLib.BE.Academic.Transaction.StudentRemarks para = DeserializeObject<AcademicLib.BE.Academic.Transaction.StudentRemarks>(jsonData);
                    if(!para.TranId.HasValue)
                        para.TranId = 0;

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
                            if (DocumentColl.Count > 0)
                                para.FilePath = DocumentColl[0].DocPath;
                        }
                        var retVal = new AcademicLib.BL.Academic.Transaction.StudentRemarks(UserId, hostName, dbName).SaveFormData(para);
                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.ResponseId))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            notification.Content = para.Remarks;
                            notification.ContentPath = "";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.STUDENT_REMARKS);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.STUDENT_REMARKS.ToString();
                            notification.Heading = "Remarks";
                            notification.Subject = para.RemarksFor.ToString();
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.ResponseId.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification, false);


                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if (retVal.RId > 0)
                                resVal.ResponseMSG = "No Student Found On this Class " + (retVal.IsSuccess ? "" : retVal.ResponseMSG);
                            else
                                resVal.ResponseMSG = retVal.ResponseMSG;

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
                int studentId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("studentId") && para["studentId"] != null)
                        studentId = Convert.ToInt32(para["studentId"]);
                }
                if (studentId == 0)
                    return BadRequest("Please ! Send StudentId in Request Para");

                classColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(UserId, hostName, dbName).getRemarksList(this.AcademicYearId, DateTime.Today, DateTime.Today, null, studentId);
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

        #region "Get Student By QrCode"

        // POST GetStudentByQrCode
        /// <summary>
        ///  Get Student By QrCode                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.StudentAutoComplete))]
        public IHttpActionResult GetStudentByQrCode([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.StudentAutoComplete student = new AcademicLib.BE.Academic.Transaction.StudentAutoComplete();
            try
            {
                string qrCode = "";
                if (para != null)
                {
                    if (para.ContainsKey("qrCode"))
                        qrCode = Convert.ToString(para["qrCode"]);
                }
                student = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getStudentByQrCode(qrCode);
                return Json(student, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime","ExamDate","StartTime","Duration"
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

        #region "Get Employee By QrCode"

        // POST GetEmployeeByQrCode
        /// <summary>
        ///  Get Employee By QrCode                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.EmployeeAutoComplete))]
        public IHttpActionResult GetEmployeeByQrCode([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeAutoComplete student = new AcademicLib.BE.Academic.Transaction.EmployeeAutoComplete();
            try
            {
                string qrCode = "";
                if (para != null)
                {
                    if (para.ContainsKey("qrCode"))
                        qrCode = Convert.ToString(para["qrCode"]);
                }
                student = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName).getEmployeeByQrCode(qrCode);
                return Json(student, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","ExamTypeId","Name","DisplayName","ResultDate","ResultTime","ExamDate","StartTime","Duration"
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

        #region "Add Employee Remarks"


        // Post api/AddEmployeeRemarks
        /// <summary>
        ///  Add Employee Remarks    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddEmployeeRemarks()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.BE.Academic.Transaction.EmployeeRemarks para = DeserializeObject<AcademicLib.BE.Academic.Transaction.EmployeeRemarks>(jsonData);
                    if (!para.TranId.HasValue)
                        para.TranId = 0;

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
                            if (DocumentColl.Count > 0)
                                para.FilePath = DocumentColl[0].DocPath;
                        }
                        var retVal = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(UserId, hostName, dbName).SaveFormData(para);
                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.ResponseId))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            notification.Content = para.Remarks;
                            notification.ContentPath = "";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.REMARKS.ToString();
                            notification.Heading = "Remarks";
                            notification.Subject = para.RemarksFor.ToString();
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.ResponseId.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification, false);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if (retVal.RId > 0)
                                resVal.ResponseMSG = "No Employee Found On this  " + (retVal.IsSuccess ? "" : retVal.ResponseMSG);
                            else
                                resVal.ResponseMSG = retVal.ResponseMSG;

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

   
        // POST Get Employee Remarks List
        /// <summary>
        ///  Get Employee Remarks List
        ///  employeeId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.StudentRemarks))]
        public IHttpActionResult GetEmployeeRemarks([FromBody] JObject para)
        {
            AcademicLib.RE.Academic.EmployeeRemarksCollections classColl = new AcademicLib.RE.Academic.EmployeeRemarksCollections();
            try
            {
                int employeeId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("employeeId") && para["employeeId"] != null)
                        employeeId = Convert.ToInt32(para["employeeId"]);
                }
                if (employeeId == 0)
                    return BadRequest("Please ! Send EmployeeId in Request Para");

                classColl = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(UserId, hostName, dbName).getRemarksList(DateTime.Today, DateTime.Today, null, employeeId);
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
        public IHttpActionResult GetMyRemarks()
        {
            AcademicLib.RE.Academic.EmployeeRemarksCollections classColl = new AcademicLib.RE.Academic.EmployeeRemarksCollections();
            try
            {

                classColl = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(UserId, hostName, dbName).getRemarksList(DateTime.Today, DateTime.Today, null, null, true);
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

        #region "Add Leave Request"


        // POST GetLeaveTypes
        /// <summary>
        ///  Get LeaveTypes                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Attendance.LeaveType))]
        public IHttpActionResult GetLeaveTypes()
        {
            AcademicLib.BE.Attendance.LeaveTypeCollections leaveTypeColl = new AcademicLib.BE.Attendance.LeaveTypeCollections();
            try
            {
                leaveTypeColl = new AcademicLib.BL.Attendance.LeaveType(UserId, hostName, dbName).GetAllLeaveType(0);
                foreach(var v in leaveTypeColl)
                {
                    v.IsSuccess = leaveTypeColl.IsSuccess;
                    v.ResponseMSG = leaveTypeColl.ResponseMSG;
                }    
                return Json(leaveTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","LeaveTypeId","Name","Code"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // Post api/AddLeaveRequest
        /// <summary>
        ///  Add Leave Request 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddLeaveRequest()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Attendance.LeaveRequest para = DeserializeObject<AcademicLib.API.Attendance.LeaveRequest>(jsonData);
           

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;
                        if (provider.FileData.Count > 0)
                        {
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);                            
                        }
                        var retVal = new AcademicLib.BL.Attendance.LeaveRequest(UserId, hostName, dbName).SaveFromApp(para);
                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.CUserName))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            notification.Content = retVal.JsonStr;
                            notification.ContentPath = "";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_REQUEST);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_REQUEST.ToString();
                            notification.Heading = "Leave Request";
                            notification.Subject = "Leave Request";
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.CUserName.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification, true);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if (retVal.RId > 0)
                                resVal.ResponseMSG = "No Employee Found On this  " + (retVal.IsSuccess ? "" : retVal.ResponseMSG);
                            else
                                resVal.ResponseMSG = retVal.ResponseMSG;

                        }
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

        #endregion

        #region "CAS MarkEntry"

        // POST GetExamTypeList
        /// <summary>
        ///  Get ExamTypeList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetCASTypeList([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.CASTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.CASTypeCollections();
            try
            {

                int classId = 0, subjectId = 0;
                int? sectionId = null;
                bool filterSection = true;
                int examTypeId = 0;
 
                if (para.ContainsKey("examTypeId"))
                    examTypeId = Convert.ToInt32(para["examTypeId"]);

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId =ToInt (para["subjectId"]);

                if (para.ContainsKey("sectionId"))
                    sectionId = ToIntNull(para["sectionId"]);

                if (para.ContainsKey("filterSection"))
                    filterSection = Convert.ToBoolean(para["filterSection"]);

                examTypeColl = new AcademicLib.BL.Exam.Transaction.CASType(UserId, hostName, dbName).GetAllCASType(0,classId,sectionId,subjectId,examTypeId);
                return Json(examTypeColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme" 
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetStudentForCASME
        /// <summary>     
        /// Get StudentForCASME
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetStudentForCASME([FromBody] JObject para)
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
                    DateTime ExamDate = DateTime.Today;

                    int classId = 0,   subjectId = 0,casTypeId=0;
                    int? sectionId = null;
                    bool filterSection = true;
                    int examTypeId = 0;

                    if (para.ContainsKey("casTypeId"))
                        casTypeId = Convert.ToInt32(para["casTypeId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);                  

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("filterSection"))
                        filterSection = Convert.ToBoolean(para["filterSection"]);

                    if (para.ContainsKey("examDate"))
                        ExamDate = Convert.ToDateTime(para["examDate"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;
                    var meDA = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(UserId, hostName, dbName);

                    if (examTypeId > 0)
                    {
                        var dataColl= meDA.getStudentForExamTypeMarkEntry(classId, sectionId, filterSection, subjectId, examTypeId,casTypeId,this.AcademicYearId);
                        if (dataColl.IsSuccess)
                        {
                            return Json(dataColl, new JsonSerializerSettings
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
                    else
                    {
                        var dataColl = meDA.getStudentForMarkEntry(classId, sectionId, filterSection, subjectId, casTypeId, ExamDate,this.AcademicYearId);
                        if (dataColl.IsSuccess)
                        {
                            return Json(dataColl, new JsonSerializerSettings
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

                }
                var dtCOll = new AcademicLib.RE.Exam.StudentForCASMarkEntryCollections();
                dtCOll.IsSuccess = resVal.IsSuccess;
                dtCOll.ResponseMSG = resVal.ResponseMSG;

                return Json(dtCOll, new JsonSerializerSettings
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

        // POST AddCASMarkEntry
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddCASMarkEntry([FromBody] AcademicLib.RE.Exam.StudentForCASMarkEntryCollections para)
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
                    foreach(var v in para)
                    {
                        v.EmployeeId = null;
                    }

                    resVal = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(UserId, hostName, dbName).SaveFormData(para);
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


        // POST GetCASMarkEntrySubjectSummary
        /// <summary>     
        /// Get CASMarkEntrySubjectSummary
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetCASMarkEntrySubjectSummary([FromBody] JObject para)
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
                    DateTime dateFrom = DateTime.Today;
                    DateTime dateTo = DateTime.Today;

                    int classId = 0;
                    int? sectionId = null;
                    bool filterSection = true;
                    int? casTypeId = null;
                    int? subjectId = null;

                    if (para.ContainsKey("casTypeId"))
                        casTypeId = Convert.ToInt32(para["casTypeId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]); 

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("filterSection"))
                        filterSection = Convert.ToBoolean(para["filterSection"]);

                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;
                    
                    var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(UserId, hostName, dbName).getMarkEntrySubjectSummary(classId, sectionId, filterSection, dateFrom, dateTo, this.AcademicYearId,casTypeId,subjectId);

                    return Json(dataColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                        }
                    });
                }
                var dtCOll = new AcademicLib.RE.Exam.CASMarkEntrySubjectCollections();
                dtCOll.IsSuccess = resVal.IsSuccess;
                dtCOll.ResponseMSG = resVal.ResponseMSG;

                return Json(dtCOll, new JsonSerializerSettings
                {
                     
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // POST GetCASMarkEntrySummary
        /// <summary>     
        /// Get CASMarkEntrySummary
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetCASMarkEntrySummary([FromBody] JObject para)
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
                    DateTime dateFrom = DateTime.Today;
                    DateTime dateTo = DateTime.Today;

                    int classId = 0,subjectId=0;
                    int? sectionId = null;
                    bool filterSection = true;

                    if (para.ContainsKey("classId"))
                        classId =ToInt(para["classId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToInt(para["subjectId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (para.ContainsKey("filterSection"))
                        filterSection = Convert.ToBoolean(para["filterSection"]);

                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(UserId, hostName, dbName).getMarkEntrySummary(classId, sectionId, filterSection, subjectId, dateFrom, dateTo, this.AcademicYearId);

                    return Json(dataColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                        }
                    });
                }
                var dtCOll = new AcademicLib.RE.Exam.CASMarkEntrySubjectCollections();
                dtCOll.IsSuccess = resVal.IsSuccess;
                dtCOll.ResponseMSG = resVal.ResponseMSG;

                return Json(dtCOll, new JsonSerializerSettings
                {

                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        #endregion

        #region "Lession Plan"

        // POST GetLessonPlanByClassSubject
        /// <summary>
        ///  Get LessonPlanByClassSubject                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonPlanByClassSubject([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            try
            {
                //added batchid,semesterid, classyearid
                int classId = 0, subjectId = 0; int batchId = 0; int semesterId = 0;int classyearId = 0, AcademicYearId=this.AcademicYearId;
                //int sectionId = 0;
                string sectionIdColl = "";

                if (para.ContainsKey("classId"))
                    classId =ToInt(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);
                //Added by Suresh
                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                //if (para.ContainsKey("sectionId"))
                //    sectionId = ToInt(para["sectionId"]);

                //if (para.ContainsKey("sectionId"))
                //    sectionId = ToInt(para["sectionId"]);

                if (para.ContainsKey("sectionIdColl"))

                    sectionIdColl = Convert.ToString(para["sectionIdColl"]);

                if (para.ContainsKey("AcademicYearId"))

                    AcademicYearId = ToInt(para["AcademicYearId"]);



                //dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).getLessonPlanForUpdate(classId, subjectId, sectionId, batchId, classyearId, semesterId,AcademicYearId);
                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonPlanByClassSubjectWise(classId, subjectId, sectionIdColl,batchId,classyearId, semesterId,this.AcademicYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }



        // Post api/AddLessionPlan
        /// <summary>
        ///  Add LessionPlan Of Class and Subject         
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddLessionPlan()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/lms"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.BE.Academic.Transaction.LessonPlan para = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlan>(jsonData);
                    
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {

                       
                        if (provider.FileData.Count > 0)
                        {
                            Dynamic.BusinessEntity.GeneralDocumentCollections data = GetAttachmentDocuments(provider.FileData);
                            foreach (var docV in data)
                            {
                                if (docV.ParaName == "coverFile")
                                    para.CoverFilePath = docV.DocPath;
                               
                                else
                                    para.CoverFilePath = null;
                            }
                        }

                        para.CUserId = UserId;

                        if (!para.TranId.HasValue)
                            para.TranId = 0;

                        para.BatchId = (para.BatchId == 0) ? null : para.BatchId;
                        para.SemesterId = (para.SemesterId == 0) ? null : para.SemesterId;
                        para.ClassYearId = (para.ClassYearId == 0) ? null : para.ClassYearId;

                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                para.CoverFilePath = DocumentColl[0].DocPath;
                            }
                        }
                        resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).SaveFormData(para);
                          
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

        // POST UpdateLessionPlanDate
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateLessionPlanDate([FromBody] AcademicLib.BE.Academic.Transaction.LessonPlanningCollections para)
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
                    //foreach (var v in para)
                    //{
                    //    v.TranId = null;
                    //}
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).SaveUpdate(para);
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


        // POST GetLessonTopicTeacherContent
        /// <summary>
        ///  Get LessonTopicTeacherContent                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonTopicTeacherContent([FromBody] JObject para)
        {
            
            try
            {
                int lessonId = 0, lessonSNo = 0, topicSNo = 0;

                if (para.ContainsKey("lessonId"))
                    lessonId = Convert.ToInt32(para["lessonId"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = Convert.ToInt32(para["topicSNo"]);

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTopicTeacherContent(lessonId, lessonSNo, topicSNo);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }



        // Post api/AddLessonTopicContent
        /// <summary>
        ///  Add LessonTopicContent Of Class and Subject         
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddLessonTopicContent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/lms"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    List<AcademicLib.BE.Academic.Transaction.LessonTopicContent> para = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicContent>>(jsonData);

                    if (para == null)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Blank Data Can't be Accept";
                        return Ok(resVal);
                    }
                    if (provider.FileData != null && provider.FileData.Count > 0)
                    {
                        for (int fInd = 0; fInd < para.Count; fInd++)
                        {
                            var fileData = provider.FileData.FirstOrDefault(f =>
                            {
                                var key = f.Headers.ContentDisposition.Name;
                                if (!string.IsNullOrEmpty(key))
                                    key = key.Trim('"');
                                return key == "file" + fInd;
                            });

                            if (fileData != null)
                            {
                                var singleFileCollection = new System.Collections.ObjectModel.Collection<System.Net.Http.MultipartFileData>();
                                singleFileCollection.Add(fileData);
                                var DocumentColl = GetAttachmentDocuments(singleFileCollection);
                                if (DocumentColl != null && DocumentColl.Count > 0)
                                {
                                    var att = DocumentColl[0];
                                    para[fInd].FilePath = att.DocPath;
                                    para[fInd].FileName = att.Name;
                                }
                            }
                        }
                    }
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).SaveLessonTopicContent(para);
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
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetTodayLessonPlan([FromBody] JObject para)
        {

            try
            {
                DateTime? forDate = DateTime.Today;
                int? classId = null, sectionId = null, subjectId = null, employeeId = null, BatchId = null, SemesterId = null, ClassYearId = null;
                int reportType = 1;

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

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getTodayLessonPlan(this.AcademicYearId, forDate, classId, sectionId, subjectId, employeeId, reportType,BatchId, SemesterId, ClassYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetLessonTopicContent
        /// <summary>
        ///  Get LessonTopicContent                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonTopicContent([FromBody] JObject para)
        {

            try
            {
                int /*lessonId = 0,*/ lessonSNo = 0, topicSNo = 0, classId=0,subjectId=0,batchId=0,semesterId=0,classYearId=0;

                //if (para.ContainsKey("lessonId"))
                //    lessonId = Convert.ToInt32(para["lessonId"]);

                if (para.ContainsKey("classId"))
                    classId = Convert.ToInt32(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = Convert.ToInt32(para["subjectId"]);

                if (para.ContainsKey("batchId"))
                    batchId = Convert.ToInt32(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = Convert.ToInt32(para["semesterId"]);

                if (para.ContainsKey("classYearId"))
                    classYearId = Convert.ToInt32(para["classYearId"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = Convert.ToInt32(para["topicSNo"]);

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTopicContent(/*lessonId,*/ lessonSNo, topicSNo,classId,subjectId,batchId,semesterId,classYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST StartLesson
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult StartLesson([FromBody] AcademicLib.BE.Academic.Transaction.LessonPlanDetails para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).StartLesson(para);
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


        // POST EndLesson
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EndLesson([FromBody] AcademicLib.BE.Academic.Transaction.LessonPlanDetails para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).EndLesson(para);
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


        // POST StartTopic
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult StartTopic([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopic para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).StartTopic(para);
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


        // POST EndTopic
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EndTopic([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopic para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).EndTopic(para);
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


        // POST StartTopicContent
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult StartTopicContent([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).StartTopicContent(para);
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


        // POST EndTopicContent
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EndTopicContent([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).EndTopicContent(para);
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

        #endregion

        #region "LMS"

        // POST GetLessonTopicVideo
        /// <summary>
        ///  Get Lesson Topi cVideo                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonTopicVideo([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.CASTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.CASTypeCollections();
            try
            {

                int lessonSNo = 0, topicSNo = 0, classId = 0, subjectId = 0; int? batchId = 0, semesterId = 0, classYearId = 0;

                //if (para.ContainsKey("lessonId"))
                //    lessonId = Convert.ToInt32(para["lessonId"]);
                if (para.ContainsKey("classId"))
                    classId = Convert.ToInt32(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = Convert.ToInt32(para["subjectId"]);

                if (para.ContainsKey("batchId"))
                    batchId = Convert.ToInt32(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = Convert.ToInt32(para["semesterId"]);

                if (para.ContainsKey("classYearId"))
                    classYearId = Convert.ToInt32(para["classYearId"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = Convert.ToInt32(para["topicSNo"]); 

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId,hostName,dbName).getLessonTopicVideo(lessonSNo, topicSNo, classId, subjectId, batchId, semesterId, classYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST GetLessonTopicQuiz
        /// <summary>
        ///  Get Lesson Topic Quiz          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonTopicQuiz([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.CASTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.CASTypeCollections();
            try
            {

                int quizId = 0;

                if (para.ContainsKey("quizId"))
                    quizId = Convert.ToInt32(para["quizId"]);

                //if (para.ContainsKey("lessonSNo"))
                //    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                //if (para.ContainsKey("topicSNo"))
                //    topicSNo = Convert.ToInt32(para["topicSNo"]);

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTopicQuiz(quizId);                
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST UpdateLessionPlanDate
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddLessonTopicVideo([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections para)
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
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).SaveLessonTopicVideo(para);                     
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

        // Post api/AddLessonTeacherContent
        /// <summary>
        ///  Add Lesson Teacher Content
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddLessonTeacherContent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/lms"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    var para = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            { 
                                int fInd = 0;
                                foreach (var beData in para)
                                {
                                    var file = DocumentColl.Find(p1 => p1.Name.Contains("file" + fInd ));
                                    if (file != null)
                                    {                                    
                                        beData.FilePath = file.DocPath;
                                        beData.FileName = file.Name;
                                    }
                                    fInd++;
                                }

                            }
                        }

                       

                        var retVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).SaveLessonTeacherContent(para);
                        
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

        // POST GetLessonTeacherContent
        /// <summary>
        ///  Get LessonTeacherContent                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonTeacherContent([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Creation.CASTypeCollections examTypeColl = new AcademicLib.BE.Exam.Creation.CASTypeCollections();
            try
            {

                int lessonId = 0, lessonSNo = 0;

                if (para.ContainsKey("lessonId"))
                    lessonId = Convert.ToInt32(para["lessonId"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonTeacherContent(lessonId, lessonSNo);                
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // Post api/AddLessonTopicTeacherContent
        /// <summary>
        ///  Add LessonTopicTeacherContent
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> AddLessonTopicTeacherContent()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/lms"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    var para = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (provider.FileData.Count > 0)
                        {
                            var DocumentColl = GetAttachmentDocuments(provider.FileData);
                            if (DocumentColl != null && DocumentColl.Count > 0)
                            {
                                int fInd = 0;
                                foreach (var beData in para)
                                {
                                    var file = DocumentColl.Find(p1 => p1.Name.Contains("file" + fInd));
                                    if (file != null)
                                    {
                                        beData.FilePath = file.DocPath;
                                        beData.FileName = file.Name;
                                    }
                                    fInd++;
                                }

                            }
                        }



                        resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).SaveLessonTopicTeacherContent(para);

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


        #region "Exam Wise HeightWeight"

        // POST GetExamWiseHeightWeight
        /// <summary>     
        /// Get Student For Exam Wise Height Weight
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetExamWiseHeightWeight([FromBody] JObject para)
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
                    int classId = 0, examTypeId = 0;
                    int? sectionId = null;

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.HeightAndWeight(UserId, hostName, dbName).getHeightWeightClassWise(this.AcademicYearId, classId, sectionId, examTypeId);

                    if (dataColl.IsSuccess)
                    {
                        return Json(dataColl, new JsonSerializerSettings
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

        // POST AddExamWiseHeightWeight
        /// <summary>    
        /// Save / Update Exam Wise Height And Weight
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamWiseHeightWeight([FromBody] AcademicLib.BE.Exam.Transaction.HeightAndWeightCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.HeightAndWeight(UserId, hostName, dbName).SaveFormData(para);
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

        #endregion

        #region "Exam Wise ResultDispatch"

        // POST GetExamResultDispatch
        /// <summary>     
        /// Get Student For Exam Result Dispatch
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetExamResultDispatch([FromBody] JObject para)
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
                    int classId = 0, examTypeId = 0;
                    int? sectionId = null;

                    if (para.ContainsKey("classId"))
                        classId = ToInt(para["classId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = Convert.ToInt32(para["examTypeId"]);

                    if (para.ContainsKey("sectionId"))
                        sectionId = ToIntNull(para["sectionId"]);

                    if (sectionId.HasValue && sectionId.Value == 0)
                        sectionId = null;

                    var dataColl = new AcademicLib.BL.Exam.Transaction.ResultDispatch(UserId, hostName, dbName).getResultDispatch(this.AcademicYearId, classId, sectionId, examTypeId);

                    if (dataColl.IsSuccess)
                    {
                        return Json(dataColl, new JsonSerializerSettings
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

        // POST AddExamResultDispatch
        /// <summary>    
        /// Save / Update Exam Wise Result Dispatch
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddExamResultDispatch([FromBody] AcademicLib.BE.Exam.Transaction.ResultDispatchCollections para)
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
                    resVal = new AcademicLib.BL.Exam.Transaction.ResultDispatch(UserId, hostName, dbName).SaveFormData(para);
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

        #endregion


        #region "Leave"

        // POST GetEmpLeaveReq
        /// <summary>     
        /// Get Employee Leave Request List
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetEmpLeaveReq([FromBody] JObject para)
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
                    int? employeeId = null;
                    DateTime? dateFrom = null, dateTo = null;
                    int leaveStatus = 0;

                    if (para.ContainsKey("leaveStatus"))
                        leaveStatus = Convert.ToInt32(para["leaveStatus"]);

                    if (para.ContainsKey("employeeId"))
                        employeeId = Convert.ToInt32(para["employeeId"]);

                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    var dataColl = new AcademicLib.BL.Attendance.LeaveRequest(UserId, hostName, dbName).getEmpLeaveRequestLst(dateFrom, dateTo, leaveStatus, employeeId);

                    var retData = new
                    {
                        IsSuccess=dataColl.IsSuccess,
                        ResponseMSG=dataColl.ResponseMSG,
                        LeaveColl = dataColl,
                        BalanceColl = dataColl.LeaveBalanceColl
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

          
        // POST AddMarkEntry
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult LeaveApprove([FromBody] AcademicLib.API.Attendance.LeaveApprove para)
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
                    if (string.IsNullOrEmpty(para.ApprovedRemarks))
                        resVal.ResponseMSG = "Please ! Enter Remarks";
                    else if (para.ApprovedType <= 1)
                        resVal.ResponseMSG = "Please ! Select Approved Status";
                    else
                    {
                        para.ApprovedBy = UserId;
                        para.ApprovedByUser = User.Identity.Name;
                        var retVal = new AcademicLib.BL.Attendance.LeaveRequest(UserId, hostName,dbName).LeaveApproved(para);

                        if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.CUserName))
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                            string approvedTypes = ((AcademicLib.BE.Attendance.APPROVEDTYPES)para.ApprovedType).ToString();
                            notification.Content = retVal.JsonStr;
                            notification.ContentPath = "";
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_APPROVED);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_APPROVED.ToString();
                            notification.Heading = "Leave "+approvedTypes;
                            notification.Subject = "Leave "+approvedTypes;
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = retVal.CUserName.Trim();

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification, true);

                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                        }
                        else
                        {
                            resVal = retVal;

                            if (retVal.RId > 0)
                                resVal.ResponseMSG = "No Employee Found On this  " + (retVal.IsSuccess ? "" : retVal.ResponseMSG);
                            else
                                resVal.ResponseMSG = retVal.ResponseMSG;

                        }
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

        #endregion


        #region "Health Report"

        // POST GetHealthReport
        /// <summary>     
        /// Get Student/Employee Health Report
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetHealthRpt([FromBody] JObject para)
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
                   int? studentId=null,employeeId=null;

                    if (para.ContainsKey("studentId"))
                        studentId = Convert.ToInt32(para["studentId"]);

                    if (para.ContainsKey("employeeId"))
                        employeeId = Convert.ToInt32(para["employeeId"]);


                    var dataColl = new AcademicERP.BL.Health.Transaction.HealthCampaign(UserId, hostName, dbName).getHealthReport(studentId, employeeId);

                    return Json(dataColl, new JsonSerializerSettings
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


        #endregion


        #region "Get Student Type Attendance By QrCode"

        // POST AddStudentTypeAttendanceByQrCode
        /// <summary>
        ///  Get Student By QrCode                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddStudentTypeAttendanceByQrCode([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string qrCode = "";
                int studentTypeId = 0;
                int attendanceType = 1;
                double? lat = null, lng = null;
                string liveLocation = "";

                if (para != null)
                {
                    if (para.ContainsKey("qrCode"))
                        qrCode = Convert.ToString(para["qrCode"]);

                    if (para.ContainsKey("studentTypeId"))
                        studentTypeId = Convert.ToInt32(para["studentTypeId"]);

                    if (para.ContainsKey("attendanceType"))
                        attendanceType = Convert.ToInt32(para["attendanceType"]);

                    try
                    {
                        if (para.ContainsKey("lat"))
                            lat = Convert.ToDouble(para["lat"]);

                        if (para.ContainsKey("lng"))
                            lng = Convert.ToDouble(para["lng"]);

                        if (para.ContainsKey("liveLocation"))
                            liveLocation = Convert.ToString(para["liveLocation"]);
                    }
                    catch { }

                }

                if(string.IsNullOrEmpty(qrCode))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid QR";
                }
                else if (studentTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Student Type";
                    resVal.IsSuccess = false;                    
                }
                else if (attendanceType == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Attendance Type";
                    resVal.IsSuccess = false;
                }
                else
                {
                    var findStudent = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getStudentByQrCode(qrCode);
                    if (findStudent != null || findStudent.StudentId==0)
                    {
                        AcademicLib.BE.Attendance.StudentTypeDailyAttendance attendance = new AcademicLib.BE.Attendance.StudentTypeDailyAttendance();
                        attendance.StudentId = findStudent.StudentId;
                        attendance.StudentTypeId = studentTypeId;
                        attendance.Attendance = attendanceType;
                        attendance.ForDate = DateTime.Now;
                        attendance.Lat = lat;
                        attendance.Lng = lng;
                        attendance.LiveLocation = liveLocation;
                        attendance.TranId = 0;
                        attendance.CUserId = UserId;

                        resVal = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(UserId, hostName, dbName).SaveFormData(attendance);
                    }else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Invalid Student QR";
                    }

                }
                 
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                      
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "StudentType List"

        // POST GetStudentTypeList
        /// <summary>
        ///  Get StudentType List                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.StudentType))]
        public IHttpActionResult GetStudentTypeList()
        {
            AcademicLib.BE.Academic.Creation.StudentTypeCollections classSection = new AcademicLib.BE.Academic.Creation.StudentTypeCollections();
            try
            {
                classSection = new AcademicLib.BL.Academic.Creation.StudentType(UserId, hostName, dbName).GetAllStudentType(0);
                return Json(classSection, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = true,
                    //    IncludeProperties = new List<string>
                    //                    {
                    //                        "CasteId","Name","ResponseMSG","IsSuccess","id","text"
                    //                    }
                    //}
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion


        // POST v1/PaySlip
        /// <summary>        
        /// get PaySlip
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PaySlip([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                string format = "PDF";
                int yearId = 0; 
                int monthId = 0;
                if (para.ContainsKey("yearId"))
                    yearId = Convert.ToInt32(para["yearId"]);

                if (para.ContainsKey("monthId"))
                    monthId = Convert.ToInt32(para["monthId"]);
                 
                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.PaySlip;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, 0);

                if (comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
                {
                    var empBL = new AcademicLib.BL.Academic.Transaction.Employee(UserId, hostName, dbName);
                    var profile = empBL.getEmployeeForApp(null);

                    AcademicLib.BE.PayrollConfig payRollConfig = new AcademicLib.BL.PayrollConfig(UserId, hostName, dbName).GetPayrollConfig(0);

                    if (!payRollConfig.PaySlipTemplateId.HasValue || payRollConfig.PaySlipTemplateId == 0)
                        return BadRequest("No Report Templates Found");

                    PivotalERP.Global.ReportTemplate reportTemplate = null;
                    //if (payRollConfig == null || !payRollConfig.PaySlipTemplateId.HasValue || payRollConfig.PaySlipTemplateId == 0)
                      //  reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                 //   else
                        reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false, payRollConfig.PaySlipTemplateId.Value);

                    if (reportTemplate==null || reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");
                    }

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                    paraColl.Add("UserId", UserId.ToString());                    
                    paraColl.Add("UserName", User.Identity.Name);
                    paraColl.Add("YearId", yearId.ToString());
                    paraColl.Add("MonthId", monthId.ToString());
                    paraColl.Add("EmployeeId", profile.EmployeeId.ToString());
                    paraColl.Add("EmployeeIdColl", profile.EmployeeId.ToString());
                    paraColl.Add("Name", profile.Name);
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
                            printLog.AutoManualNo = profile.EmployeeId.ToString(); ;
                            printLog.SystemUser = "API";
                            printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                            printLog.EntityId = entityId;
                            printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                            printLog.LogDate = DateTime.Now;
                            printLog.LogText = "Print Payslip of EmployeeId id=" + profile.EmployeeId.ToString();
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



        #region "HomeWorkList"

        // POST GetStudentEvaluation
        /// <summary>
        ///  Get StudentEvaluation     
        ///  int ClassId, int SectionId, int ExamTypeId, string ExamTypeIdColl = null, string StudentIdColl = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.HomeWork.HomeWorkCollections))]
        public IHttpActionResult GetStudentEvaluation([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.StudentEvalCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentEvalCollections();
            try
            {
                int ClassId=0;
                int SectionId=0;
                int ExamTypeId=0;
                string ExamTypeIdColl = null;
                string StudentIdColl = null;
                int? BatchId = null;
                int? SemesterId = null;
                int? ClassYearId = null;

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


                if (para != null)
                {
                   
                    if (para.ContainsKey("ClassId") && para["ClassId"] != null)
                        ClassId = ToInt(para["ClassId"]);

                    if (para.ContainsKey("SectionId") && para["SectionId"] != null)
                        SectionId = ToInt(para["sectionId"]);

                    if (para.ContainsKey("ExamTypeId") && para["ExamTypeId"] != null)
                        ExamTypeId = Convert.ToInt32(para["ExamTypeId"]);

                    if (para.ContainsKey("ExamTypeIdColl") && para["ExamTypeIdColl"] != null)
                        ExamTypeIdColl = Convert.ToString(para["ExamTypeIdColl"]);

                    if (para.ContainsKey("StudentIdColl") && para["StudentIdColl"] != null)
                        StudentIdColl = Convert.ToString(para["StudentIdColl"]);
                    
                    if (para.ContainsKey("BatchId") && para["BatchId"] != null)
                        BatchId = Convert.ToInt32(para["BatchId"]);

                    if (para.ContainsKey("SemesterId") && para["SemesterId"] != null)
                        SemesterId = Convert.ToInt32(para["SemesterId"]);


                    if (para.ContainsKey("ClassYearId") && para["ClassYearId"] != null)
                        ClassYearId = Convert.ToInt32(para["ClassYearId"]); 
                }
                dataColl = new AcademicLib.BL.Exam.Transaction.StudentEval(UserId, hostName, dbName).GetStudentEvaluation(0, AcademicYearId, ClassId, SectionId, ExamTypeId, ExamTypeIdColl, StudentIdColl, BatchId, SemesterId, ClassYearId);

               
                return Json(dataColl, new JsonSerializerSettings
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


        #region "Student QuickAccess"

        // POST GetStudentQuickAccess
        /// <summary>
        ///  Get Student Quick Access by StudentId
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.StudentType))]
        public IHttpActionResult GetStudentQuickAccess([FromBody]JObject para)
        {
            if (para == null || para.ContainsKey("StudentId")==false)
            {
                return BadRequest("Invalid Parameters");
            }
            int StudentId = Convert.ToInt32(para["StudentId"]);

            var uid = UserId;
            //var profile = new AcademicLib.BL.Academic.Transaction.Student(uid,hostName,dbName).GetStudentById(0, StudentId);
            var profile = new AcademicLib.BL.Academic.Transaction.Student(uid, hostName, dbName).getStudentForApp(this.AcademicYearId, StudentId, null,null,null);
            var studentLedger = new AcademicLib.BL.Fee.Transaction.FeeReceipt(uid, hostName, dbName).getStudentLedger(this.AcademicYearId, StudentId, null, null);
            var studentVoucher = new AcademicLib.BL.Fee.Transaction.FeeReceipt(uid, hostName, dbName).getStudentVoucher(this.AcademicYearId, StudentId, null, null);
            var studentResults = new AcademicLib.BL.Exam.Transaction.MarksEntry(uid, hostName, dbName).getStudentResult(StudentId, this.AcademicYearId);
            var groupResults = new AcademicLib.BL.Exam.Transaction.MarksEntry(uid, hostName, dbName).getStudentGroupResult(StudentId);
            var notificationLog = new AcademicLib.BL.Global(uid, hostName, dbName).GetNotificationLogForQuickAccess(StudentId);
            var remarksColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(uid, hostName, dbName).getRemarksList(this.AcademicYearId, DateTime.Today, DateTime.Today, null, StudentId);
            var homeWorkColl = new AcademicLib.BL.HomeWork.HomeWork(uid, hostName, dbName).GetAllHomeWork(0, null, null, true, StudentId);
            var assignmentColl = new AcademicLib.BL.HomeWork.Assignment(uid, hostName, dbName).GetAllAssignment(0, null, null, true, StudentId);
            var bookLedger = new AcademicLib.BL.Library.Transaction.BookEntry(uid, hostName, dbName).getStudentLedger(this.AcademicYearId, StudentId);
            var attendanceColl = new AcademicLib.BL.Attendance.Device(uid, hostName, dbName).getStudentAttendance(StudentId, this.AcademicYearId);
            try
            {
                var attSumm = new
                {
                    TotalDays = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1=>ToInt(p1.TotalDays)) : 0,
                    TotalWeekEnd = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalWeekEnd)) : 0,
                    TotalHoliday = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalHoliday)) : 0,
                    TotalPresent = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalPresent)) : 0,
                    TotalLeave = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalLeave)) : 0,
                    TotalAbsent = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalAbsent)) : 0,
                    TotalSchoolDays= attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalDays)) - attendanceColl.Sum(p1 => ToInt(p1.TotalWeekEnd)) - attendanceColl.Sum(p1 => ToInt(p1.TotalHoliday)) : 0,
                };                

                var returnData = new
                {
                    Profile = profile,
                    StudentLedger = studentLedger,
                    StudentVoucher = studentVoucher,
                    ExamResults = studentResults,
                    ExamGroupResults = groupResults,
                    NotificationLog = notificationLog,
                    Remarks = remarksColl,
                    Homework = homeWorkColl,
                    Assignment = assignmentColl,
                    BookLedger = bookLedger,
                    Attendance = attendanceColl,
                    AttendanceSummary= attSumm,
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS
                };
                return Json(returnData, new JsonSerializerSettings
                {
                  
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion


        #region "Teacher QuickAccess"

        // POST GetTeacherQuickAccess
        /// <summary>
        ///  Get Teacher Quick Access by StudentId
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.StudentType))]
        public IHttpActionResult GetEmployeeQuickAccess([FromBody] JObject para)
        {
            if (para == null || para.ContainsKey("EmployeeId") == false)
            {
                return BadRequest("Invalid Parameters");
            }
            int EmployeeId = Convert.ToInt32(para["EmployeeId"]);

            int? yearId = null;
            if (para.ContainsKey("YearId"))
                yearId = ToIntNull(para["YearId"]);

            int leaveStatus = 1;
            if (para.ContainsKey("LeaveStatus"))
                leaveStatus = ToInt(para["LeaveStatus"]);

            var uid = UserId;
            var profile = new AcademicLib.BL.Academic.Transaction.Employee(uid, hostName, dbName).getEmployeeForApp(EmployeeId);
            var attendanceColl = new AcademicLib.BL.Attendance.Device(uid, hostName, dbName).getEmpYearAttendanceLog(EmployeeId, yearId, null);
            var documentColl = new AcademicLib.BL.Academic.Transaction.EQuickAccess(uid, hostName, dbName).GetEmpAttForQuickAccess(0, EmployeeId);
            var remarksColl = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(uid, hostName, dbName).getRemarksList(DateTime.Today, DateTime.Today, null, EmployeeId);
            var leaveColl = new AcademicLib.BL.Academic.Transaction.EQuickAccess(uid, hostName, dbName).GetEmpLeaveTakenForQuickAccess(0, EmployeeId);
            ///var leaveStatusColl = new AcademicLib.BL.Attendance.LeaveRequest(uid, hostName, dbName).getEmpLeaveRequestLst(null,null, leaveStatus, EmployeeId);
            ///
            var attSumm = new
            {
                TotalDays = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalDays)) : 0,
                TotalWeekEnd = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalWeekend)) : 0,
                TotalHoliday = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalHoliday)) : 0,
                TotalPresent = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalPresent)) : 0,
                TotalLeave = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalLeave)) : 0,
                TotalAbsent = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalAbsent)) : 0,
                TotalSchoolDays = attendanceColl != null && attendanceColl.Count > 0 ? attendanceColl.Sum(p1 => ToInt(p1.TotalDays)) - attendanceColl.Sum(p1 => ToInt(p1.TotalWeekend)) - attendanceColl.Sum(p1 => ToInt(p1.TotalHoliday)) : 0,
            };

            try
            {
                var returnData = new
                {
                    Profile = profile,
                    Remarks = remarksColl,
                    Documents = documentColl,
                    Attendance = attendanceColl,
                    AttendanceSummary = attSumm,
                    Leave = leaveColl,
               //     LeaveStatusColl=leaveStatusColl,
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS
                };
                return Json(returnData, new JsonSerializerSettings
                {

                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "SendNoticeToEmployee"


        // Post api/SendNoticeToEmployee
        /// <summary>
        ///  Send Notice To Student 
        ///  employeeIdColl as string  = employeeIdColl=1,2,4,5
        ///  title as string 
        ///  description as string
        ///  attachFiles as files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendNoticeToEmployee()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/academic/employee"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    AcademicLib.API.Teacher.EmployeeNotice para = DeserializeObject<AcademicLib.API.Teacher.EmployeeNotice>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else if (string.IsNullOrEmpty(para.employeeIdColl))
                    {
                        resVal.ResponseMSG = "employee does not found";
                    }
                    else if (string.IsNullOrEmpty(para.title))
                    {
                        resVal.ResponseMSG = "please ! enter notice title";
                    }
                    else if (string.IsNullOrEmpty(para.description))
                    {
                        resVal.ResponseMSG = "please ! enter notice details";
                    }
                    else
                    {
                        var retVal = new AcademicLib.BL.Global(UserId, hostName, dbName).GetUserIdColl(para.employeeIdColl, 2);
                        if (retVal.IsSuccess)
                        {
                            string userIdCollStr = "";
                            Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                            foreach (var uc in retVal.ResponseId.Split('#'))
                            {
                                string[] ucSep = uc.Split(',');
                                if (ucSep.Length == 2)
                                {
                                    if (!string.IsNullOrEmpty(userIdCollStr))
                                        userIdCollStr = userIdCollStr + ",";

                                    userIdCollStr = userIdCollStr + ucSep[1];

                                    userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                }
                            }

                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                            notification.Content = para.description;
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE.ToString();
                            notification.Heading = para.title;
                            notification.Subject = para.title;
                            notification.UserId = UserId;
                            notification.UserName = User.Identity.Name;
                            notification.UserIdColl = userIdCollStr.Trim();
                            if (provider.FileData.Count > 0)
                            {
                                var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                if (DocumentColl != null && DocumentColl.Count > 0)
                                {
                                    notification.ContentPath = DocumentColl[0].DocPath;
                                }
                            }

                            if (ActiveBranch)
                                notification.BranchCode = this.BranchCode;

                            resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);

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

        #endregion


        // POST v1/GetMarkSubmit
        /// <summary>
        /// Get   MarkSubmit
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetMarkSubmit([FromBody] JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            { 
                int AcademicYearId=this.AcademicYearId, ExamTypeId=0, BranchId=1;

                if (para.ContainsKey("AcademicYearId"))
                    AcademicYearId = Convert.ToInt32(para["AcademicYearId"]);


                if (para.ContainsKey("ExamTypeId"))
                    ExamTypeId = Convert.ToInt32(para["ExamTypeId"]);


                if (para.ContainsKey("BranchId"))
                    BranchId = Convert.ToInt32(para["BranchId"]);

                var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName,dbName).getMarkSubmit(this.AcademicYearId, ExamTypeId, BranchId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                    }
                });
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


        // POST v1/GetTabulation
        /// <summary>
        /// Get  Tabulation
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetTabulation([FromBody] JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                bool FromPublished = true;
                int AcademicYearId = this.AcademicYearId;

                if (para.ContainsKey("AcademicYearId"))
                    AcademicYearId = Convert.ToInt32(para["AcademicYearId"]);

                int? branchId = ToIntNull(para["BranchId"]);
                int? classId = ToIntNull(para["ClassId"]);
                int? sectionId = ToIntNull(para["SectionId"]);
                int examTypeId = ToInt(para["ExamTypeId"]);
                int? studentId = null;
                bool FilterSection = Convert.ToBoolean(para["FilterSection"]);

                FromPublished = Convert.ToBoolean(para["FromPublished"]);

                int? SemesterId = null, ClassYearId = null, BatchId = null;
                
                var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName,dbName).getMarkSheetClassWise(AcademicYearId, studentId, classId, sectionId, examTypeId, FilterSection, "", BatchId, SemesterId, ClassYearId, FromPublished, branchId);
                 
                List<AcademicLib.RE.Exam.Tabulation> dataSource = new List<AcademicLib.RE.Exam.Tabulation>();
                int rowSNO = 0;
                foreach (var dc in dataColl)
                {
                    rowSNO++;
                    foreach (var sd in dc.DetailsColl)
                    {
                        AcademicLib.RE.Exam.Tabulation tbl = new AcademicLib.RE.Exam.Tabulation();
                        tbl.RowSNo = rowSNO;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.WorkingDays = dc.WorkingDays;
                        tbl.PresentDays = dc.PresentDays;
                        tbl.AbsentDays = dc.AbsentDays;
                        tbl.SymbolNo = dc.SymbolNo;

                        tbl.Caste = dc.Caste;
                        tbl.StudentType = dc.StudentType;
                        tbl.Address = dc.Address;
                        tbl.TeacherComment = dc.Comment;

                        tbl.RollNo = dc.RollNo;
                        tbl.Division = dc.Division;
                        tbl.StudentId = dc.StudentId;
                        tbl.ClassName = dc.ClassName;
                        tbl.SectionName = dc.SectionName;
                        tbl.RegdNo = dc.RegdNo;
                        tbl.BoardName = dc.BoardName;
                        tbl.BoardRegdNo = dc.BoardRegdNo;
                        tbl.Name = dc.Name;
                        tbl.FatherName = dc.FatherName;
                        tbl.MotherName = dc.MotherName;
                        tbl.DOB_AD = dc.DOB_AD;
                        tbl.DOB_BS = dc.DOB_BS;
                        tbl.F_ContactNo = dc.F_ContactNo;
                        tbl.M_ContactNo = dc.M_ContactNo;
                        tbl.Gender = dc.Gender;
                        tbl.HouseName = dc.HouseName;
                        tbl.ObtainMark = dc.ObtainMark;
                        tbl.Per = dc.Per;
                        tbl.RankInClass = dc.RankInClass;
                        tbl.RankInSection = dc.RankInSection;
                        tbl.Result = dc.Result;
                        tbl.GP = dc.GP;
                        tbl.Grade = dc.Grade;
                        tbl.GradeTH = dc.GradeTH;
                        tbl.GradePR = dc.GradePR;
                        tbl.GPA = dc.GPA;
                        tbl.GP_Grade = dc.GP_Grade;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.FM = dc.FM;
                        tbl.FMTH = dc.FMTH;
                        tbl.FMPR = dc.FMPR;
                        tbl.PM = dc.PM;
                        tbl.PMTH = dc.PMTH;
                        tbl.PMPR = dc.PMPR;

                        tbl.IsOptional = sd.IsOptional;
                        tbl.SubjectId = sd.SubjectId;
                        tbl.SubjectName = sd.SubjectName;
                        tbl.CodeTH = sd.CodeTH;
                        tbl.CodePR = sd.CodePR;
                        tbl.Sub_OM = sd.ObtainMark;
                        tbl.Sub_OMPR = sd.OPR;
                        tbl.Sub_OMTH = sd.OTH;
                        tbl.Sub_OM_Str = sd.ObtainMark_Str;
                        tbl.Sub_OMTH_Str = sd.ObtainMarkTH_Str;
                        tbl.Sub_OMPR_Str = sd.ObtainMarkPR_Str;
                        tbl.IsFailTH = sd.IsFailTH;
                        tbl.IsFailPR = sd.IsFailPR;
                        tbl.PaperType = sd.PaperType;

                        tbl.Sub_FM = sd.FM;
                        tbl.Sub_FMTH = sd.FMTH;
                        tbl.Sub_FMPR = sd.FMPR;
                        tbl.Sub_PM = sd.PM;
                        tbl.Sub_PMTH = sd.PMTH;
                        tbl.Sub_PMPR = sd.PMPR;

                        tbl.Sub_GP = sd.GP;
                        tbl.Sub_GP_TH = sd.GP_TH;
                        tbl.Sub_GP_PR = sd.GP_PR;
                        tbl.Sub_Grade = sd.Grade;
                        tbl.Sub_Grade_TH = sd.GradeTH;
                        tbl.Sub_Grade_PR = sd.GradePR;
                        tbl.Sub_GP_Grade = sd.GP_Grade;
                        tbl.Sub_GP_Grade_TH = sd.GP_GradeTH;
                        tbl.Sub_GP_Grade_PR = sd.GP_GradePR;

                        tbl.CAS1 = sd.CAS1;
                        tbl.CAS2 = sd.CAS2;
                        tbl.CAS3 = sd.CAS3;
                        tbl.CAS4 = sd.CAS4;
                        tbl.CAS5 = sd.CAS5;
                        tbl.CAS6 = sd.CAS6;
                        tbl.CAS7 = sd.CAS7;
                        tbl.CAS8 = sd.CAS8;
                        tbl.CAS9 = sd.CAS9;
                        tbl.CAS10 = sd.CAS10;
                        tbl.CAS11 = sd.CAS11;
                        tbl.CAS12 = sd.CAS12;

                        if (tbl.PaperType == 1)
                            tbl.PaperTypeName = "TH";
                        else if (tbl.PaperType == 2)
                            tbl.PaperTypeName = "PR";

                        tbl.SNo = sd.SNo;
                        dataSource.Add(tbl);
                    }


                }
                 
                return Json(dataSource, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                    }
                });
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
        // POST v1/GetGrpTabulation
        /// <summary>
        /// Get  Group Tabulation
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetGrpTabulation([FromBody] JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                bool FromPublished = true;
                int AcademicYearId = this.AcademicYearId;
                
                if (para.ContainsKey("AcademicYearId"))
                    AcademicYearId = Convert.ToInt32(para["AcademicYearId"]);

                int? branchId = ToIntNull(para["BranchId"]);
                int? classId = ToIntNull(para["ClassId"]);
                int? sectionId = ToIntNull(para["SectionId"]);
                int examTypeId = ToInt(para["ExamTypeId"]);
                int? curExamTypeId = ToInt(para["curExamTypeId"]);
                int? studentId = null;
                bool FilterSection = Convert.ToBoolean(para["FilterSection"]);

                FromPublished = Convert.ToBoolean(para["FromPublished"]);

                int? SemesterId = null, ClassYearId = null, BatchId = null;

                var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(UserId, hostName, dbName).getGroupMarkSheetClassWise(AcademicYearId, studentId, classId, sectionId, examTypeId, FilterSection, curExamTypeId, BatchId, SemesterId, ClassYearId, FromPublished, branchId);

                List<AcademicLib.RE.Exam.GroupTabulation> dataSource = new List<AcademicLib.RE.Exam.GroupTabulation>();
                int rowSNO = 0;
                foreach (var dc in dataColl)
                {
                    rowSNO++;
                    foreach (var sd in dc.DetailsColl)
                    {
                        AcademicLib.RE.Exam.GroupTabulation tbl = new AcademicLib.RE.Exam.GroupTabulation();
                        tbl.RowSNo = rowSNO;
                        tbl.WorkingDays = dc.WorkingDays;
                        tbl.PresentDays = dc.PresentDays;
                        tbl.AbsentDays = dc.AbsentDays;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.SymbolNo = dc.SymbolNo;

                        tbl.Caste = dc.Caste;
                        tbl.StudentType = dc.StudentType;
                        tbl.Address = dc.Address;
                        tbl.TeacherComment = dc.TeacherComment;
                        tbl.RollNo = dc.RollNo;
                        tbl.Division = dc.Division;
                        tbl.StudentId = dc.StudentId;
                        tbl.ClassName = dc.ClassName;
                        tbl.SectionName = dc.SectionName;
                        tbl.RegdNo = dc.RegdNo;
                        tbl.BoardName = dc.BoardName;
                        tbl.BoardRegdNo = dc.BoardRegdNo;
                        tbl.Name = dc.Name;
                        tbl.FatherName = dc.FatherName;
                        tbl.MotherName = dc.MotherName;
                        tbl.DOB_AD = dc.DOB_AD;
                        tbl.DOB_BS = dc.DOB_BS;
                        tbl.F_ContactNo = dc.F_ContactNo;
                        tbl.M_ContactNo = dc.M_ContactNo;
                        tbl.Gender = dc.Gender;
                        tbl.HouseName = dc.HouseName;
                        tbl.ObtainMark = dc.ObtainMark;
                        tbl.Per = dc.Per;
                        tbl.RankInClass = dc.RankInClass;
                        tbl.RankInSection = dc.RankInSection;
                        tbl.Result = dc.Result;
                        tbl.GP = dc.GP;
                        tbl.Grade = dc.Grade;
                        tbl.GradeTH = dc.GradeTH;
                        tbl.GradePR = dc.GradePR;
                        tbl.GPA = dc.GPA;
                        tbl.GP_Grade = dc.GP_Grade;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.FM = dc.FM;
                        tbl.FMTH = dc.FMTH;
                        tbl.FMPR = dc.FMPR;
                        tbl.PM = dc.PM;
                        tbl.PMTH = dc.PMTH;
                        tbl.PMPR = dc.PMPR;

                        tbl.TeacherComment = dc.TeacherComment;
                        tbl.CompanyName = dc.CompanyName;
                        tbl.CompanyAddress = dc.CompanyAddress;
                        tbl.CompPhoneNo = dc.CompPhoneNo;
                        tbl.CompFaxNo = dc.CompFaxNo;
                        tbl.CompEmailId = dc.CompEmailId;
                        tbl.CompWebSite = dc.CompWebSite;
                        tbl.CompLogoPath = dc.CompLogoPath;
                        tbl.CompImgPath = dc.CompImgPath;
                        tbl.CompBannerPath = dc.CompBannerPath;
                        tbl.ExamName = dc.ExamName;
                        tbl.IssueDateAD = dc.IssueDateAD;
                        tbl.IssueDateBS = dc.IssueDateBS;
                        tbl.CompRegdNo = dc.CompRegdNo;
                        tbl.CompPanVat = dc.CompPanVat;
                        tbl.TotalStudentInClass = dc.TotalStudentInClass;
                        tbl.TotalStudentInSection = dc.TotalStudentInSection;
                        tbl.ResultDateAD = dc.ResultDateAD;
                        tbl.ResultDateBS = dc.ResultDateBS;
                        tbl.Exam1 = dc.Exam1;
                        tbl.Exam2 = dc.Exam2;
                        tbl.Exam3 = dc.Exam3;
                        tbl.Exam4 = dc.Exam4;
                        tbl.Exam5 = dc.Exam5;
                        tbl.Exam6 = dc.Exam6;
                        tbl.Exam7 = dc.Exam7;
                        tbl.Exam8 = dc.Exam8;
                        tbl.Exam9 = dc.Exam9;
                        tbl.Exam10 = dc.Exam10;
                        tbl.Exam11 = dc.Exam11;
                        tbl.Exam12 = dc.Exam12;
                        tbl.E_Grade_1 = dc.E_Grade_1;
                        tbl.E_Grade_2 = dc.E_Grade_2;
                        tbl.E_Grade_3 = dc.E_Grade_3;
                        tbl.E_Grade_4 = dc.E_Grade_4;
                        tbl.E_Grade_5 = dc.E_Grade_5;
                        tbl.E_Grade_6 = dc.E_Grade_6;
                        tbl.E_Grade_7 = dc.E_Grade_7;
                        tbl.E_Grade_8 = dc.E_Grade_8;
                        tbl.E_Grade_9 = dc.E_Grade_9;
                        tbl.E_Grade_10 = dc.E_Grade_10;
                        tbl.E_Grade_11 = dc.E_Grade_11;
                        tbl.E_Grade_12 = dc.E_Grade_12;
                        tbl.E_AVGGP_1 = dc.E_AVGGP_1;
                        tbl.E_AVGGP_2 = dc.E_AVGGP_2;
                        tbl.E_AVGGP_3 = dc.E_AVGGP_3;
                        tbl.E_AVGGP_4 = dc.E_AVGGP_4;
                        tbl.E_AVGGP_5 = dc.E_AVGGP_5;
                        tbl.E_AVGGP_6 = dc.E_AVGGP_6;
                        tbl.E_AVGGP_7 = dc.E_AVGGP_7;
                        tbl.E_AVGGP_8 = dc.E_AVGGP_8;
                        tbl.E_AVGGP_9 = dc.E_AVGGP_9;
                        tbl.E_AVGGP_10 = dc.E_AVGGP_10;
                        tbl.E_AVGGP_11 = dc.E_AVGGP_11;
                        tbl.E_AVGGP_12 = dc.E_AVGGP_12;

                        tbl.IsOptional = sd.IsOptional;
                        tbl.SubjectId = sd.SubjectId;
                        tbl.SubjectName = sd.SubjectName;
                        tbl.CodeTH = sd.CodeTH;
                        tbl.CodePR = sd.CodePR;
                        tbl.Sub_OM = sd.ObtainMark;
                        tbl.Sub_OMPR = sd.OPR;
                        tbl.Sub_OMTH = sd.OTH;
                        tbl.Sub_OM_Str = sd.ObtainMark_Str;
                        tbl.Sub_OMTH_Str = sd.ObtainMarkTH_Str;
                        tbl.Sub_OMPR_Str = sd.ObtainMarkPR_Str;
                        tbl.IsFailTH = sd.IsFailTH;
                        tbl.IsFailPR = sd.IsFailPR;
                        tbl.PaperType = sd.PaperType;
                        tbl.Sub_FM = sd.FM;
                        tbl.Sub_FMTH = sd.FMTH;
                        tbl.Sub_FMPR = sd.FMPR;
                        tbl.Sub_PM = sd.PM;
                        tbl.Sub_PMTH = sd.PMTH;
                        tbl.Sub_PMPR = sd.PMPR;

                        tbl.Sub_GP = sd.GP;
                        tbl.Sub_GP_TH = sd.GP_TH;
                        tbl.Sub_GP_PR = sd.GP_PR;
                        tbl.Sub_Grade = sd.Grade;
                        tbl.Sub_Grade_TH = sd.GradeTH;
                        tbl.Sub_Grade_PR = sd.GradePR;
                        tbl.Sub_GP_Grade = sd.GP_Grade;
                        tbl.Sub_GP_Grade_TH = sd.GP_GradeTH;
                        tbl.Sub_GP_Grade_PR = sd.GP_GradePR;

                        tbl.IsECA = sd.IsECA;
                        tbl.Sub_Exam1 = sd.Sub_Exam1;
                        tbl.Sub_Exam2 = sd.Sub_Exam2;
                        tbl.Sub_Exam3 = sd.Sub_Exam3;
                        tbl.Sub_Exam4 = sd.Sub_Exam4;
                        tbl.Sub_Exam5 = sd.Sub_Exam5;
                        tbl.Sub_Exam6 = sd.Sub_Exam6;
                        tbl.Sub_Exam7 = sd.Sub_Exam7;
                        tbl.Sub_Exam8 = sd.Sub_Exam8;
                        tbl.Sub_Exam9 = sd.Sub_Exam9;
                        tbl.Sub_Exam10 = sd.Sub_Exam10;
                        tbl.Sub_Exam11 = sd.Sub_Exam11;
                        tbl.Sub_Exam12 = sd.Sub_Exam12;

                        tbl.Sub_Exam1_Str = sd.Sub_Exam1_Str;
                        tbl.Sub_Exam2_Str = sd.Sub_Exam2_Str;
                        tbl.Sub_Exam3_Str = sd.Sub_Exam3_Str;
                        tbl.Sub_Exam4_Str = sd.Sub_Exam4_Str;
                        tbl.Sub_Exam5_Str = sd.Sub_Exam5_Str;
                        tbl.Sub_Exam6_Str = sd.Sub_Exam6_Str;
                        tbl.Sub_Exam7_Str = sd.Sub_Exam7_Str;
                        tbl.Sub_Exam8_Str = sd.Sub_Exam8_Str;
                        tbl.Sub_Exam9_Str = sd.Sub_Exam9_Str;
                        tbl.Sub_Exam10_Str = sd.Sub_Exam10_Str;
                        tbl.Sub_Exam11_Str = sd.Sub_Exam11_Str;
                        tbl.Sub_Exam12_Str = sd.Sub_Exam12_Str;

                        tbl.Sub_E_TH_1 = sd.Sub_E_TH_1;
                        tbl.Sub_E_TH_2 = sd.Sub_E_TH_2;
                        tbl.Sub_E_TH_3 = sd.Sub_E_TH_3;
                        tbl.Sub_E_TH_4 = sd.Sub_E_TH_4;
                        tbl.Sub_E_TH_5 = sd.Sub_E_TH_5;
                        tbl.Sub_E_TH_6 = sd.Sub_E_TH_6;
                        tbl.Sub_E_TH_7 = sd.Sub_E_TH_7;
                        tbl.Sub_E_TH_8 = sd.Sub_E_TH_8;
                        tbl.Sub_E_TH_9 = sd.Sub_E_TH_9;
                        tbl.Sub_E_TH_10 = sd.Sub_E_TH_10;
                        tbl.Sub_E_TH_11 = sd.Sub_E_TH_11;
                        tbl.Sub_E_TH_12 = sd.Sub_E_TH_12;
                        tbl.Sub_E_PR_1 = sd.Sub_E_PR_1;
                        tbl.Sub_E_PR_2 = sd.Sub_E_PR_2;
                        tbl.Sub_E_PR_3 = sd.Sub_E_PR_3;
                        tbl.Sub_E_PR_4 = sd.Sub_E_PR_4;
                        tbl.Sub_E_PR_5 = sd.Sub_E_PR_5;
                        tbl.Sub_E_PR_6 = sd.Sub_E_PR_6;
                        tbl.Sub_E_PR_7 = sd.Sub_E_PR_7;
                        tbl.Sub_E_PR_8 = sd.Sub_E_PR_8;
                        tbl.Sub_E_PR_9 = sd.Sub_E_PR_9;
                        tbl.Sub_E_PR_10 = sd.Sub_E_PR_10;
                        tbl.Sub_E_PR_11 = sd.Sub_E_PR_11;
                        tbl.Sub_E_PR_12 = sd.Sub_E_PR_12;
                        tbl.Sub_E_GP_1 = sd.Sub_E_GP_1;
                        tbl.Sub_E_GP_2 = sd.Sub_E_GP_2;
                        tbl.Sub_E_GP_3 = sd.Sub_E_GP_3;
                        tbl.Sub_E_GP_4 = sd.Sub_E_GP_4;
                        tbl.Sub_E_GP_5 = sd.Sub_E_GP_5;
                        tbl.Sub_E_GP_6 = sd.Sub_E_GP_6;
                        tbl.Sub_E_GP_7 = sd.Sub_E_GP_7;
                        tbl.Sub_E_GP_8 = sd.Sub_E_GP_8;
                        tbl.Sub_E_GP_9 = sd.Sub_E_GP_9;
                        tbl.Sub_E_GP_10 = sd.Sub_E_GP_10;
                        tbl.Sub_E_GP_11 = sd.Sub_E_GP_11;
                        tbl.Sub_E_GP_12 = sd.Sub_E_GP_12;
                        tbl.Sub_E_Grade_1 = sd.Sub_E_Grade_1;
                        tbl.Sub_E_Grade_2 = sd.Sub_E_Grade_2;
                        tbl.Sub_E_Grade_3 = sd.Sub_E_Grade_3;
                        tbl.Sub_E_Grade_4 = sd.Sub_E_Grade_4;
                        tbl.Sub_E_Grade_5 = sd.Sub_E_Grade_5;
                        tbl.Sub_E_Grade_6 = sd.Sub_E_Grade_6;
                        tbl.Sub_E_Grade_7 = sd.Sub_E_Grade_7;
                        tbl.Sub_E_Grade_8 = sd.Sub_E_Grade_8;
                        tbl.Sub_E_Grade_9 = sd.Sub_E_Grade_9;
                        tbl.Sub_E_Grade_10 = sd.Sub_E_Grade_10;
                        tbl.Sub_E_Grade_11 = sd.Sub_E_Grade_11;
                        tbl.Sub_E_Grade_12 = sd.Sub_E_Grade_12;
                        tbl.Sub_Cur_FTH = sd.Sub_Cur_FTH;
                        tbl.Sub_Cur_FPR = sd.Sub_Cur_FPR;
                        tbl.Sub_Cur_PTH = sd.Sub_Cur_PTH;
                        tbl.Sub_Cur_PPR = sd.Sub_Cur_PPR;
                        tbl.Sub_Cur_OTH = sd.Sub_Cur_OTH;
                        tbl.Sub_Cur_OPR = sd.Sub_Cur_OPR;
                        tbl.Sub_Cur_OM = sd.Sub_Cur_OM;
                        tbl.Sub_Cur_OTH_Str = sd.Sub_Cur_OTH_Str;
                        tbl.Sub_Cur_OPR_Str = sd.Sub_Cur_OPR_Str;

                        if (tbl.PaperType == 1)
                            tbl.PaperTypeName = "TH";
                        else if (tbl.PaperType == 2)
                            tbl.PaperTypeName = "PR";

                        tbl.SNo = sd.SNo;
                        dataSource.Add(tbl);
                    }
                }

                return Json(dataSource, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                    }
                });
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

        // POST GetClassSubjectwiseTermSyllabus
        /// <summary>
        ///  Get GetClassSubjectwiseTermSyllabus                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.TermSyllabusColl))]
        public IHttpActionResult GetClassSubjectwiseTermSyllabus([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Creation.TermSyllabusColl dataColl = new AcademicLib.BE.Academic.Creation.TermSyllabusColl();

            try
            {
                int? classId = null, subjectId = null, batchId = null, semesterId = null, classyearId = null, examTypeId = null;

                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    if (para.ContainsKey("classId"))
                        classId = ToIntNull(para["classId"]);

                    if (para.ContainsKey("subjectId"))
                        subjectId = ToIntNull(para["subjectId"]);

                    if (para.ContainsKey("batchId"))
                        batchId = ToIntNull(para["batchId"]);

                    if (para.ContainsKey("semesterId"))
                        semesterId = ToIntNull(para["semesterId"]);

                    if (para.ContainsKey("classyearId"))
                        classyearId = ToIntNull(para["classyearId"]);

                    if (para.ContainsKey("examTypeId"))
                        examTypeId = ToIntNull(para["examTypeId"]);

                    dataColl = new AcademicLib.BL.Academic.Creation.TermSyllabus(UserId, hostName, dbName)
                                .GetClassSubjectwiseTermSyllabus(
                                    0,
                                    batchId,
                                    classId,
                                    semesterId,
                                    classyearId,
                                    examTypeId,
                                    subjectId
                                );
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
                "DataColl", 
                // DB Fields
                "TranId","LessonId","TopicId","Content","TeachingPeriod","TeachingMethods","TeachingMaterials","Evaluation", "Remarks","LessonName",
                "TopicNames","ClassId","SubjectId","ExamTypeId","BatchId","SemesterId","ClassYearId"
            }
                }
            });
        }


        // POST GetAllSyllabus
        /// <summary>
        ///  Get AllSyllabus List                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.TermSyllabus))]
        public IHttpActionResult GetAllSyllabus()
        {
            AcademicLib.BE.Academic.Creation.TermSyllabusColl syllabus = new AcademicLib.BE.Academic.Creation.TermSyllabusColl();
            try
            {
                syllabus = new AcademicLib.BL.Academic.Creation.TermSyllabus(UserId, hostName, dbName).GetAllSyllabus(0);
                return Json(syllabus, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = true,
                    //    IncludeProperties = new List<string>
                    //                    {
                    //                        "CasteId","Name","ResponseMSG","IsSuccess","id","text"
                    //                    }
                    //}
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST AddTermSyllabus
        /// <summary>                          
        /// </summary>
        /// <returns></returns>
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AddTermSyllabus([FromBody] AcademicLib.BE.Academic.Creation.TermSyllabusColl para)
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
                    foreach (var v in para)
                    {
                        v.TranId = null;
                    }

                    resVal = new AcademicLib.BL.Academic.Creation.TermSyllabus(UserId, hostName, dbName).SaveFormData(para);
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

        #region "GetClassSubjectWiseLesson"

        // POST GetClassSubjectWiseLesson
        /// <summary>
        ///  Get GetClassSubjectWiseLesson      
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections))]
        public IHttpActionResult GetClassSubjectWiseLesson([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections subjectlessoncoll = new AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections();
            try
            {
                int classId = 0, subjectId = 0;
                if (para.ContainsKey("classId"))
                    classId = Convert.ToInt32(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = Convert.ToInt32(para["subjectId"]);               

                var dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(UserId, hostName, dbName).GetSubjectLessonWise(0, classId, subjectId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
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


        #region "GetLessonTopicDetailsWise"

        // POST GetLessonTopicDetailsWise
        /// <summary>
        ///  Get GetLessonTopicDetailsWise      
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections))]
        public IHttpActionResult GetLessonTopicDetailsWise([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections lessontopiccoll = new AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections();
            try
            {
                int lessonId = 0;
                if (para.ContainsKey("lessonId"))
                    lessonId = Convert.ToInt32(para["lessonId"]);              

                var dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(UserId, hostName, dbName).GetLessonTopicDetailsWise(0, lessonId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
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



        #region "LessonPlanByClass


        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.LessonPlanCollections))]
        public IHttpActionResult getLessonPlanByClass([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlanCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlanCollections();
            try
            {
                int classId = 0, sectionId = 0, subjectId = 0, batchId = 0, semesterId = 0, classyearId = 0, employeeId = 0;
                int reportType = 0;

                if (para.ContainsKey("classId"))
                    classId = Convert.ToInt32(para["classId"]);


                if (para.ContainsKey("sectionId"))
                    sectionId = Convert.ToInt32(para["sectionId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = Convert.ToInt32(para["subjectId"]);

                if (para.ContainsKey("batchId"))
                    batchId = Convert.ToInt32(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = Convert.ToInt32(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = Convert.ToInt32(para["classyearId"]);

                if (para.ContainsKey("reportType"))
                    reportType = Convert.ToInt32(para["reportType"]);

                if (para.ContainsKey("employeeId"))
                    employeeId = Convert.ToInt32(para["employeeId"]);

                 dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonPlanByClass(classId, sectionId, employeeId, subjectId, reportType, batchId, semesterId, classyearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
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


        #region "ReadingMaterial

        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.ReadingMaterials.ReadingMaterials))]
        public IHttpActionResult GetClassSubWiseData([FromBody] JObject para)
        {
            AcademicLib.BE.ReadingMaterials.ReadingMaterials dataColl = new AcademicLib.BE.ReadingMaterials.ReadingMaterials();

            try
            {
                int classId = 0, subjectId = 0,
                semesterId = 0, classyearId = 0,batchId=0;

                if (para.ContainsKey("classId") && para["classId"] != null)
                    classId = Convert.ToInt32(para["classId"]);

                if (para.ContainsKey("subjectId") && para["subjectId"] != null)
                    subjectId = Convert.ToInt32(para["subjectId"]);

                if (para.ContainsKey("semesterId") && para["semesterId"] != null)
                    semesterId = Convert.ToInt32(para["semesterId"]);

                if (para.ContainsKey("classyearId") && para["classyearId"] != null)
                    classyearId = Convert.ToInt32(para["classyearId"]);

                if (para.ContainsKey("batchId") && para["batchId"] != null)
                    classyearId = Convert.ToInt32(para["batchId"]);

                dataColl = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).GetClassSubWiseData(0, classId, classyearId, semesterId, subjectId, batchId);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent))]
        public IHttpActionResult GetReadMatContById([FromBody] JObject para)
        {
            AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent data = new AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent();
            try
            {
                int readMatContId = 0;
                if (para != null && para.ContainsKey("readMatContId"))
                    readMatContId = ToInt(para["readMatContId"]);

                data = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).GetReadMatContById(0, readMatContId);

                return Json(data, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideo))]
        public IHttpActionResult GetReadMatVideoById([FromBody] JObject para)
        {
            AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideo data = new AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideo();
            try
            {
                int readMatVideoId = 0;
                if (para != null && para.ContainsKey("readMatVideoId"))
                    readMatVideoId = ToInt(para["readMatVideoId"]);
                data = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).GetReadMatVideoById(0, readMatVideoId);
                return Json(data, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveReadMatCont()
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return BadRequest("Unsupported media type");

                var provider = new FormDataStreamProvider(GetPath("~/Attachments/ReadingMaterials"));
                await Request.Content.ReadAsMultipartAsync(provider);

                string jsonData = provider.FormData["jsonData"];

                if (string.IsNullOrEmpty(jsonData))
                    return BadRequest("No data found");

                var beData =
                    DeserializeObject<AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent>(jsonData);

                if (beData == null)
                    return BadRequest("No form data found");

                beData.CUserId = UserId;

                if (provider.FileData != null && provider.FileData.Count > 0)
                {
                    var uploadedDocs = GetAttachmentDocuments(provider.FileData);
                    var existingDocs = beData.ContentAttach;

                    beData.ContentAttach = new Dynamic.BusinessEntity.GeneralDocumentCollections();

                    int i = 0;

                    foreach (var fileDoc in uploadedDocs)
                    {
                        if (fileDoc == null)
                            continue;

                        var attach = new Dynamic.BusinessEntity.GeneralDocument
                        {
                            Data = fileDoc.Data,
                            DocPath = fileDoc.DocPath,
                            Extension = fileDoc.Extension,
                            Name = fileDoc.Name
                        };

                        // safe mapping from existing metadata
                        if (existingDocs != null && existingDocs.Count > i)
                        {
                            attach.DocumentTypeId = existingDocs[i].DocumentTypeId;
                            attach.Description = existingDocs[i].Description;
                        }

                        beData.ContentAttach.Add(attach);
                        i++;
                    }
                }

                if (!beData.ReadMatContId.HasValue)
                    beData.ReadMatContId = 0;

                var retVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(
                    UserId,
                    hostName,
                    dbName
                ).SaveContent(beData);

                return Ok(retVal);
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SaveReadMatVideo([FromBody] AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideoCollection para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null || para.Count == 0)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                    return Ok(resVal);
                }

                foreach (var v in para)
                {
                    v.ReadMatVideoId = null;
                }

                resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).SaveVideo(para);
                return Ok(resVal);
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideoCollection))]
        public IHttpActionResult DelReadMatVideo([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int readMatVideoId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("readMatVideoId") && para["readMatVideoId"] != null)
                        readMatVideoId = Convert.ToInt32(para["readMatVideoId"]);
                    else if (para.ContainsKey("ReadMatVideoId") && para["ReadMatVideoId"] != null)
                        readMatVideoId = Convert.ToInt32(para["ReadMatVideoId"]);
                    else if (para.ContainsKey("readMatVideoId") && para["readMatVideoId"] != null)
                        readMatVideoId = Convert.ToInt32(para["readMatVideoId"]);
                }

                if (readMatVideoId == 0)
                {
                    resVal.ResponseMSG = "Invalid Video Content pls ! select valid Video Content for delete";
                }
                else
                    resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).DeleteVideoById(0, readMatVideoId);

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



        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContentCollection))]
        public IHttpActionResult DelReadMatCont([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int readMatContId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("readMatContId") && para["readMatContId"] != null)
                        readMatContId = Convert.ToInt32(para["readMatContId"]);
                    else if (para.ContainsKey("ReadMatContId") && para["ReadMatContId"] != null)
                        readMatContId = Convert.ToInt32(para["ReadMatContId"]);
                    else if (para.ContainsKey("readMatContId") && para["readMatContId"] != null)
                        readMatContId = Convert.ToInt32(para["readMatContId"]);
                }
                if (readMatContId == 0)
                {
                    resVal.ResponseMSG = "Invalid Content pls ! select valid  Content for delete";
                }
                else
                    resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(UserId, hostName, dbName).DeleteContentById(0, readMatContId);

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


        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLPClassSubjectSecWise([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            try
            {
                //added batchid,semesterid, classyearid
                int classId = 0, subjectId = 0; int batchId = 0; int semesterId = 0; int classyearId = 0, academicYearId=0;
                //int sectionId = 0;
                string sectionIdColl = "";

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);
                //Added by Suresh
                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                if (para.ContainsKey("academicYearId"))
                    academicYearId = ToInt(para["academicYearId"]);

                //if (para.ContainsKey("sectionId"))
                //    sectionId = ToInt(para["sectionId"]);
                //Ends

                if (para.ContainsKey("sectionIdColl"))
                    sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                else if (para.ContainsKey("sectionIdColl"))
                    sectionIdColl = Convert.ToString(para["sectionIdColl"]);
                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).GetLPClassSubjectSecWise(classId, subjectId, sectionIdColl, batchId, classyearId, semesterId, academicYearId);
                //var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).getLessonPlanByClassSubjectWise(classId, subjectId, SectionIdColl,batchId,semesterId,classyearId,this.AcademicYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }


        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.LessonPlanningCollections))]
        public IHttpActionResult GetLessonPlanningTopicContent([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan lessonplancoll = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            try
            {
                int tranId = 0;
                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);

              

                var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).getLessonPlanningTopicContent(tranId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
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
        public IHttpActionResult SaveLessonPlanningContent([FromBody] AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections para)
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
                    //foreach (var v in para)
                    //{
                    //    v.TranId = null;
                    //}
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).SaveLessonPlannigContent(para);
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
        #region "Class Test"

        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetStudentsDetailsClassWise([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections();
            try
            {
                //added batchid,semesterid, classyearid
                int academicYearId = this.AcademicYearId;
                int classId = 0, subjectId = 0, employeeId = 0;
                int? sectionId= null, lessonSNo = null, batchId = null,semesterId = null,classyearId = null;
                DateTime testDate = DateTime.Now;
                bool filterSection = true;

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("sectionId"))
                    sectionId = Convert.ToInt32(para["sectionId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                if (para.ContainsKey("filterSection"))
                    filterSection = Convert.ToBoolean(para["filterSection"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("testDate"))
                    testDate = Convert.ToDateTime(para["testDate"]);

                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                dataColl = new AcademicLib.BL.Exam.Transaction.ClassTestMarkEntry(UserId, hostName, dbName).GetStudentsDetailsClassWise(0,academicYearId, classId, sectionId, subjectId, filterSection, lessonSNo, testDate, employeeId, batchId, semesterId, classyearId);
                 
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
                        "DataColl","ResponseMSG","IsSuccess","StudentId","SectionId","SectionName","RegdNo","RollNumber","StudentName","BoardRegdNumber",
                        "FullMarks","PassMarks","ObtainMarks","RemarksType","IsAbsent","ClassId","BatchId","SemesterId","ClassYearId"
                     }
                }
            });
        }

        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SaveUpdateClassTestMarkEntry([FromBody] AcademicLib.BE.Exam.Transaction.ClassTestMarkEntryCollections para)
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
                    foreach (var v in para)
                    {
                        v.AcademicYearId = this.AcademicYearId;
                    }

                    resVal = new AcademicLib.BL.Exam.Transaction.ClassTestMarkEntry(UserId, hostName, dbName).SaveUpdate(para);
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


        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetViewClassSummaryDetails([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.ClassSummaryCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryCollections();
            try
            {
                int academicYearId = this.AcademicYearId;
                int classId = 0, subjectId =0;
                int? sectionId = null,  batchId = null, semesterId = null, classyearId = null;
                DateTime dateFrom = DateTime.Now;
                DateTime dateTo = DateTime.Now;

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("sectionId"))
                    sectionId = Convert.ToInt32(para["sectionId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                if (para.ContainsKey("dateFrom"))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo"))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                dataColl = new AcademicLib.BL.Exam.Transaction.ClassSummary(UserId, hostName, dbName).GetViewClassSummaryDetails(0, classId, sectionId, subjectId, dateFrom, dateTo, batchId, semesterId, classyearId);

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
                        "DataColl","ResponseMSG","IsSuccess","TestDate","ClassId","SubjectId","LessonSno","LessonName","EmployeeId","EmployeeName",
                        "FullMarks","PassMarks","PresentStudent","TotalStudent","TestDateBS","TotalPassStudent"
                     }
                }
            });
        }


        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult getClassSummaryDetailsById([FromBody] JObject para)
        {
            AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections();
            try
            {
                int academicYearId = this.AcademicYearId;
                int classId = 0, subjectId = 0, employeeId = 0;
                int? batchId = null, semesterId = null, classyearId = null,lessonSNo = null;
                DateTime testDate = DateTime.Now;

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("testDate"))
                    testDate = Convert.ToDateTime(para["testDate"]);

                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                dataColl = new AcademicLib.BL.Exam.Transaction.ClassSummary(UserId, hostName, dbName).GetClassSummaryDetailsById(0, academicYearId, classId, employeeId, subjectId, lessonSNo, testDate, batchId, semesterId, classyearId);

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
                        "DataColl","ResponseMSG","IsSuccess","TranId","StudentId","RegdNo","RollNo","BoardRegdNumber","StudentName","Remarks",
                        "ObtMarks","SubjectId","ClassId","SubjectName","ClassName","SectionName","IsAbsent"
                     }
                }
            });
        }

        #endregion



        // POST GetLessonTopicQuizList
        /// <summary>
        ///  Get LessonTopicQuizList                 
        ///  lessonId as Int   
        ///  lessonSNo as Int 
        ///  topicSNo as Int 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLessonTopicQuizList([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicQuizCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicQuizCollections();
            try
            {

                int lessonSNo = 0, topicSNo = 0, classId = 0, subjectId = 0; int? batchId = 0, semesterId = 0, classYearId = 0;               

                if (para.ContainsKey("lessonSNo"))
                    lessonSNo = Convert.ToInt32(para["lessonSNo"]);

                if (para.ContainsKey("topicSNo"))
                    topicSNo = Convert.ToInt32(para["topicSNo"]);

                if (para.ContainsKey("classId"))
                    classId = Convert.ToInt32(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = Convert.ToInt32(para["subjectId"]);

                if (para.ContainsKey("batchId"))
                    batchId = Convert.ToInt32(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = Convert.ToInt32(para["semesterId"]);

                if (para.ContainsKey("classYearId"))
                    classYearId = Convert.ToInt32(para["classYearId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).GetLessonTopicQuizList(lessonSNo,topicSNo, classId, subjectId, batchId, semesterId, classYearId);
                

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
                                           "DataColl", "QuizId","Topic","Description","NoOfQuestion","FullMark","PassMark","FullMark","Duration"
                                        }
                }
            });
        }
        #region "Delete TopicQuiz"

        // POST DelLessonTopicQuizById
        /// <summary>
        ///  Get DelLessonTopicQuizById     
        ///  quizId as Int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Transaction.LessonPlanCollections))]
        public IHttpActionResult DelLessonTopicQuizById([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                int quizId = 0;
                if (para != null)
                {
                    if (para.ContainsKey("quizId") && para["quizId"] != null)
                        quizId = Convert.ToInt32(para["quizId"]);
                    else if (para.ContainsKey("QuizId") && para["QuizId"] != null)
                        quizId = Convert.ToInt32(para["QuizId"]);
                    else if (para.ContainsKey("quizId") && para["quizId"] != null)
                        quizId = Convert.ToInt32(para["quizId"]);
                }

                if (quizId == 0)
                {
                    resVal.ResponseMSG = "Invalid Quiz pls ! select valid quiz for delete";
                }
                else
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName).DelLessonTopicQuizById(0, quizId);

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

        //POST SaveLessonTopicQuiz
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public async Task<IHttpActionResult> SaveLessonTopicQuiz()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/lms"));
                    await Request.Content.ReadAsMultipartAsync(provider);
                    string jsonData = provider.FormData["paraData"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");
                    AcademicLib.BE.Academic.Transaction.LessonTopicQuiz para = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopicQuiz>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (provider.FileData.Count > 0)
                        {
                            Dynamic.BusinessEntity.GeneralDocumentCollections docs = GetAttachmentDocuments(provider.FileData);
                            foreach (var doc in docs)
                            {
                                string key = doc.ParaName;
                                foreach (var que in para.QuestionColl)
                                {
                                    if (key == "file-q" + que.SNo)
                                    {
                                        que.ContentPath = doc.DocPath;
                                    }
                                    if (que.AnswerColl != null && que.AnswerColl.Count > 0)
                                    {
                                        foreach (var ans in que.AnswerColl)
                                        {
                                            if (key == "file-q" + que.SNo + "-a" + ans.SNo)
                                            {
                                                ans.ContentPath = doc.DocPath;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        AcademicLib.BL.Academic.Transaction.LessonPlan jrn = new AcademicLib.BL.Academic.Transaction.LessonPlan(UserId, hostName, dbName);
                        para.CUserId = UserId;
                        resVal = jrn.SaveLessonTopicQuiz(para);
                    }
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
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



        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Exam.Creation.CASTypeCollections))]
        public IHttpActionResult GetLessonPlanForUpdate([FromBody] JObject para)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            try
            {
                //added batchid,semesterid, classyearid
                int classId = 0, subjectId = 0; int batchId = 0; int semesterId = 0; int classyearId = 0, AcademicYearId = this.AcademicYearId;
                int sectionId = 0;
                

                if (para.ContainsKey("classId"))
                    classId = ToInt(para["classId"]);

                if (para.ContainsKey("subjectId"))
                    subjectId = ToInt(para["subjectId"]);
                //Added by Suresh
                if (para.ContainsKey("batchId"))
                    batchId = ToInt(para["batchId"]);

                if (para.ContainsKey("semesterId"))
                    semesterId = ToInt(para["semesterId"]);

                if (para.ContainsKey("classyearId"))
                    classyearId = ToInt(para["classyearId"]);

                if (para.ContainsKey("sectionId"))
                    sectionId = ToInt(para["sectionId"]);

                if (para.ContainsKey("AcademicYearId"))
                    AcademicYearId = ToInt(para["AcademicYearId"]);

                dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlanning(UserId, hostName, dbName).getLessonPlanForUpdate(classId, subjectId, sectionId, batchId, classyearId, semesterId,AcademicYearId);
               
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "ResponseMSG","IsSuccess","CASTypeId","Name","Description","OrderNo","IsActive","FullMark","Under","Scheme"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }
    }
}
