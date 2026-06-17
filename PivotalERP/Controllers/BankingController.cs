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
    public class BankingController : APIBaseController
    {

        // POST api/SaveProfile
        /// <summary>
        ///  Save Student And Employee Profile For Bank Account
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveProfile()
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

                    string jsonData = provider.FormData["paraData"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                   var para = DeserializeObject<AcademicLib.BE.Academic.Transaction.NewBankAccount>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.BankId = 0;
                        var isValid = true;
                        if (provider.FileData.Count > 0)
                        {
                            var existsDocColl = para.AttachmentColl;

                            Dynamic.BusinessEntity.GeneralDocumentCollections docColl = GetAttachmentDocuments(provider.FileData);
                            para.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

                            int ind1 = 0;
                            foreach (var docV in docColl)
                            {
                                var findInd = -1;
                                string fName = docV.ParaName.Replace("file", "");
                                int.TryParse(fName, out findInd);

                                if (docV.ParaName == "SPhotoPath")
                                {
                                    para.SPhotoPath = docV.DocPath;

                                }
                                else if (docV.ParaName == "PhotoPath")
                                {
                                    para.PhotoPath = docV.DocPath;
                                }
                                else
                                {
                                    if (findInd >= 0)
                                    {
                                        ind1 = findInd;
                                        para.AttachmentColl.Add(new Dynamic.BusinessEntity.GeneralDocument()
                                        {
                                            DocumentTypeId = existsDocColl[ind1].DocumentTypeId,
                                            Data = docV.Data,
                                            DocPath = docV.DocPath,
                                            Extension = docV.Extension,
                                            Name = docV.Name,
                                            Description = existsDocColl[ind1].Description
                                        });
                                    }
                                }
                            }

                            //if (existsDocColl != null && provider.FileData.Count==existsDocColl.Count)
                            //{

                            //}
                            //else if (existsDocColl != null && provider.FileData.Count != existsDocColl.Count)
                            //{
                            //    isValid = false;
                            //    resVal.IsSuccess = false;
                            //    resVal.ResponseMSG = "Document Attachment missing";
                            //}

                            int ind = 0;
                            foreach(var ed in existsDocColl)
                            {
                                if (!string.IsNullOrEmpty(ed.DocPath))
                                {
                                    para.AttachmentColl.Add(new Dynamic.BusinessEntity.GeneralDocument()
                                    {
                                        DocumentTypeId = existsDocColl[ind].DocumentTypeId,
                                        //Data = docV.Data,
                                        DocPath = ed.DocPath,
                                        Extension = ed.Extension,
                                        Name = ed.Name,
                                        Description = existsDocColl[ind].Description
                                    });
                                }
                                ind++;
                            }

                        }
 

                        if (isValid)
                        {                            

                            para.CUserId = UserId;
                            para.ForUserId = UserId;
                            AcademicLib.BL.Academic.Transaction.NewBankAccount ledger = new AcademicLib.BL.Academic.Transaction.NewBankAccount(UserId, hostName, dbName);

                            resVal = ledger.SaveFormData(para);
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


        // POST GetAllowFieldsDB
        /// <summary>
        ///  Get AllowFields For Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.Admin.Student))]
        public IHttpActionResult NewAccount_NIC([FromBody] NICBank.BE.NewAccount para)
        {
            ResponeValues retVal = new ResponeValues();
            if (para == null)
            {
                retVal.IsSuccess = false;
                retVal.ResponseMSG = "No Data Found";
            }else
            {
                string sessionId = "", processId = "";
                retVal = new NICBank.BL.NewAccount(1,"","").OpenNewAccount("11",para,ref sessionId,ref processId);
            }
             
            return Json(retVal, new JsonSerializerSettings
            {
            });
        }

        // POST OpenNewAccount
        /// <summary>
        ///  Get Opening New Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult OpenNewAccount()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new AcademicLib.BL.Academic.Transaction.NewBankAccount(uid, hostName, dbName).GetNewBankAccountById(0, 0, uid);

            if (data.IsSuccess)
            {
                List<NICBank.BE.IdForNewAccount> idColl = new List<NICBank.BE.IdForNewAccount>();
                if (data.StudentId.HasValue)
                {
                    idColl.Add(new NICBank.BE.IdForNewAccount()
                    {
                          BankId=data.BankId.Value,
                           Id=data.StudentId.Value
                    });
                    retVal = new NICBank.BL.NewAccount(uid, hostName, dbName).OpenStudentAccount(idColl);
                }else if (data.EmployeeId.HasValue)
                {
                    idColl.Add(new NICBank.BE.IdForNewAccount()
                    {
                        BankId = data.BankId.Value,
                        Id = data.EmployeeId.Value
                    });
                    retVal = new NICBank.BL.NewAccount(uid, hostName, dbName).OpenEmpAccount(idColl);
                }
            }
            

            return Json(retVal, new JsonSerializerSettings
            {
            });
        }


        // POST GetProfile
        /// <summary>
        ///  Get Profile Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetProfile()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new AcademicLib.BL.Academic.Transaction.NewBankAccount(uid, hostName, dbName).GetNewBankAccountById(0, 0, uid);

            if (data.IsSuccess)
            {
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data=data
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,                    
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }


            return Json(retVal, new JsonSerializerSettings
            {
            });
        }


        // POST CheckAccountStatus
        /// <summary>
        ///  Get Check Account Status 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult CheckBAStatus_S()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new NICBank.BL.NewAccount(uid, hostName, dbName).getDataForAccountOpenS();

            if (data.IsSuccess)
            {
                var status = new NICBank.BL.NewAccount(uid, hostName, dbName).AccountStatus(data.BankId, data.ProcessInstanceId);
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data = status
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
             
        }


        // POST AMobileBanking_S
        /// <summary>
        ///  Active Mobile Banking
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AMobileBanking_S()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new NICBank.BL.NewAccount(uid, hostName, dbName).getDataForAccountOpenS();

            if (data.IsSuccess)
            {
                var status = new NICBank.BL.NewAccount(uid, hostName, dbName).activeMobileBank(data);
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data = status
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }

        }

        // POST CheckAccountStatus
        /// <summary>
        ///  Get Check Account Status 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult CheckBAStatus_E()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new NICBank.BL.NewAccount(uid, hostName, dbName).getDataForAccountOpenE();

            if (data.IsSuccess)
            {
                var status = new NICBank.BL.NewAccount(uid, hostName, dbName).AccountStatus(data.BankId, data.ProcessInstanceId);
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data = status
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }

        }



        // POST AMobileBanking_S
        /// <summary>
        ///  Active Mobile Banking
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult AMobileBanking_E()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new NICBank.BL.NewAccount(uid, hostName, dbName).getDataForAccountOpenE();

            if (data.IsSuccess)
            {
                var status = new NICBank.BL.NewAccount(uid, hostName, dbName).activeMobileBank(data);
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data = status
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }

        }

        // Post api/ImportNICBankBranch
        /// <summary>
        ///  Import NIC Bank Branch List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> ImportNICBankBranch()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new NICBank.BL.NewAccount(UserId, hostName, dbName).ImportBankBranchList();
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


        // POST GetNICBranch
        /// <summary>
        ///  Get NIC Bank Branch List                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(NICBank.BE.BankBranch))]
        public IHttpActionResult GetNICBranch([FromBody] JObject para)
        {
            try
            {
                var dataColl = new NICBank.BL.NewAccount(UserId, hostName, dbName).getBranchList();

                var returnVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS
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


        // POST OnlineKYC
        /// <summary>
        ///  Make Schedule for Online KYC
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult OnlineKYC()
        {
            ResponeValues retVal = new ResponeValues();
            var uid = UserId;

            var data = new NICBank.BL.NewAccount(uid, hostName, dbName).getDataForAccountOpenS();

            if (data.IsSuccess)
            {
                var status = new NICBank.BL.NewAccount(uid, hostName, dbName).OnlineKYC(data);
                var retData = new
                {
                    ResponseMSG = "Success",
                    IsSuccess = true,
                    Data = status
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }
            else
            {
                var retData = new
                {
                    ResponseMSG = "No Data Found",
                    IsSuccess = false,
                };
                return Json(retData, new JsonSerializerSettings
                {
                });
            }

        }
    }
}