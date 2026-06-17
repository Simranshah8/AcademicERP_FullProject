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
    
    public class AcademicController : APIBaseController
    {
        #region "Caste List"

        // POST GetCasteList
        /// <summary>
        ///  Get CasteList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.CasteCollections))]
        public IHttpActionResult GetCasteList()
        {
            AcademicLib.BE.Academic.Creation.CasteCollections classSection = new AcademicLib.BE.Academic.Creation.CasteCollections();
            try
            {
                int uid = 1;

                if (User.Identity.IsAuthenticated)
                    uid = UserId;

                classSection = new AcademicLib.BL.Academic.Creation.Caste(uid, hostName, dbName).GetAllCaste(0);

                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "CasteId","Name","ResponseMSG","IsSuccess","id","text"
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

        // POST StudentAnalysis
        /// <summary>
        ///  Get Student Analysis                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Academic.AnalysisCollections))]
        public IHttpActionResult GetStudentAnalysis()
        {
            AcademicLib.RE.Academic.AnalysisCollections classSection = new AcademicLib.RE.Academic.AnalysisCollections();
            try
            {
                classSection = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getAnalysis(this.AcademicYearId, null);
                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //                {
                        //                    "CasteId","Name","ResponseMSG","IsSuccess","id","text"
                        //                }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        #region "Class List"

        // POST ClassList
        /// <summary>
        ///  Get All Class List                 
        /// </summary>
        /// <returns></returns>
        
        [HttpPost]             
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.CasteCollections))]        
        public IHttpActionResult ClassList([FromBody]JObject para)
        {            
            try
            { 
                int? academicYearId = null;
                if (para != null)
                {
                    if (para.ContainsKey("academicYearId"))
                        academicYearId = Convert.ToInt32(para["academicYearId"]);
                    else if (para.ContainsKey("ayId"))
                        academicYearId = Convert.ToInt32(para["ayId"]);
                }
                else
                    academicYearId = this.AcademicYearId;

                var dataColl = new AcademicLib.BL.Academic.Creation.Class(UserId, hostName, dbName).GetAllClass(0);
                var retunVal = new
                {
                    ResMSG= dataColl.ResponseMSG,
                    ResStatus= dataColl.IsSuccess,
                    TotalRows = dataColl.Count,
                    DataColl = dataColl,                     
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "OrderNo","Name","ResMSG","ResStatus","DataColl","TotalRows","ClassId"
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
        [AllowAnonymous]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.CasteCollections))]
        public IHttpActionResult ClassListForReg([FromBody] JObject para)
        {
            try
            {               
                var dataColl = new AcademicLib.BL.Academic.Creation.Class(1, hostName, dbName).GetAllClass(0,true);
                var retunVal = new
                {
                    ResMSG = dataColl.ResponseMSG,
                    ResStatus = dataColl.IsSuccess,
                    TotalRows = dataColl.Count,
                    DataColl = dataColl,
                };
                return Json(retunVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "OrderNo","Name","ResMSG","ResStatus","DataColl","TotalRows","ClassId"
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

        #endregion

    }
}