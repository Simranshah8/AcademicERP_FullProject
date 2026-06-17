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
    public class AccountController : APIBaseController
    {

        #region "Ledger"

        // POST api/AutoCompleteLedgerList
        /// <summary>
        ///  Get AutoCompleteLedgerList 
        ///  searchBy as Int
        ///  searchValue as String
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Account.Ledger>))]
        public IHttpActionResult AutoCompleteLedgerList([FromBody] JObject para)
        {
            Dynamic.APIEnitity.Account.LedgerCollections dataColl = new Dynamic.APIEnitity.Account.LedgerCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    string searchBy = ((Dynamic.APIEnitity.Account.LEDGER_SEARCHOPTIONS)Convert.ToInt32(para["searchBy"])).GetStringValue();
                    string searchValue = Convert.ToString(para["searchValue"]);

                    Dynamic.APIEnitity.Account.LEDGERTYPES ledgerType = Dynamic.APIEnitity.Account.LEDGERTYPES.ALL;

                    if (para["ledgerType"] != null)
                    {
                        ledgerType = (Dynamic.APIEnitity.Account.LEDGERTYPES)Convert.ToInt32(para["ledgerType"]);
                    }

                    dataColl = new Dynamic.DataAccess.Account.LedgerDB(hostName, dbName).getAutoCompleteLedgerList(UserId, searchBy, searchValue, ledgerType);
                    return Json(dataColl, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/GetLedgerDetail
        /// <summary>
        ///  Get GetLedgerDetail 
        ///  ledgerId as Int
        ///  voucherType as Int (Optional)
        ///  dateFrom as DateTime (Optional)
        ///  dateTo as DateTime (Optional)
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Common.LedgerDetails))]
        public IHttpActionResult GetLedgerDetail([FromBody] JObject para)
        {
            Dynamic.BusinessEntity.Common.LedgerDetails det = new Dynamic.BusinessEntity.Common.LedgerDetails();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    int LedgerId = 0;
                    if (para["ledgerId"] != null)
                        LedgerId = Convert.ToInt32(para["ledgerId"]);
                    else
                        return BadRequest("Invalid Selected Ledger");

                    int voucherType = 0;

                    if (para["voucherType"] != null)
                        voucherType = Convert.ToInt32(para["voucherType"]);

                    DateTime? dateFrom = null;

                    if (para["dateFrom"] != null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime? dateTo = null;
                    if (para["dateTo"] != null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    det = new Dynamic.DataAccess.Common.LedgerDB(hostName, dbName).getLedgerDetails(UserId, LedgerId, dateFrom, dateTo, voucherType);

                    if (det.IsSuccess)
                    {
                        return Json(det, new JsonSerializerSettings
                        {
                        });
                    }
                    else
                        return BadRequest(det.ResponseMSG);
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // POST api/GetLedgerShortDetails
        /// <summary>
        ///  Get LedgerShortDetails 
        ///  ledgerId as Int
        ///  ledgerCode as varchar (Optional)
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Common.LedgerDetails))]
        public IHttpActionResult GetLedgerShortDetails([FromBody] JObject para)
        {
            Dynamic.BusinessEntity.Account.Ledger det = new Dynamic.BusinessEntity.Account.Ledger();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    int? LedgerId = null;
                    if (para["ledgerId"] != null)
                        LedgerId = Convert.ToInt32(para["ledgerId"]);

                    string ledgerCode = "";

                    if (para["ledgerCode"] != null)
                        ledgerCode = Convert.ToString(para["ledgerCode"]);

                    det = new Dynamic.DataAccess.Account.LedgerDB(hostName, dbName).getLedgerShortDetailsByIdCode(UserId, LedgerId, ledgerCode);

                    return Json(det, new JsonSerializerSettings
                    {
                    });
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // POST api/SaveLedger
        /// <summary>
        ///  Save New Ledger
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveLedger()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/ledger"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.Ledger para = DeserializeObject<Dynamic.BusinessEntity.Account.Ledger>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {

                        if (provider.FileData.Count > 0)
                        {
                            Dynamic.BusinessEntity.GeneralDocumentCollections docColl = GetAttachmentDocuments(provider.FileData);
                            para.DocumentColl = new Dynamic.BusinessEntity.Account.LedgerDocumentCollections();
                            foreach (var docV in docColl)
                            {
                                para.DocumentColl.Add(new Dynamic.BusinessEntity.Account.LedgerDocument()
                                {
                                    Data = docV.Data,
                                    DocPath = docV.DocPath,
                                    Extension = docV.Extension,
                                    Name = docV.Name
                                });
                            }
                        }

                        if ((para.StatutoryDetail == null || string.IsNullOrEmpty(para.StatutoryDetail.PanVatNo)) && !string.IsNullOrEmpty(para.PanVatNo))
                        {
                            para.StatutoryDetail = new Dynamic.BusinessEntity.Account.LedgerStatutoryDetail();
                            para.StatutoryDetail.PanVatNo = para.PanVatNo;
                        }

                        if ((para.ContactPersons == null || para.ContactPersons.Count == 0) && !string.IsNullOrEmpty(para.MobileNo))
                        {
                            para.ContactPersons = new Dynamic.BusinessEntity.Account.LedgerContactPersonCollections();
                            para.ContactPersons.Add(new Dynamic.BusinessEntity.Account.LedgerContactPerson()
                            {
                                Name = para.Name,
                                MobileNo1 = para.MobileNo
                            });
                        }
                        Dynamic.BusinessLogic.Account.Ledger ledger = new Dynamic.BusinessLogic.Account.Ledger(hostName, dbName);

                        resVal = ledger.SaveFormData(para);
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

        #region "LedgerGroupList"

        // POST api/LedgerGroupList
        /// <summary>
        ///  Get Ledger Group List  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.LedgerGroup>))]
        public IHttpActionResult GetLedgerGroupList([FromBody] JObject para)
        {
            Dynamic.BusinessEntity.Account.LedgerGroupCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerGroupCollections();
            try
            {
                int types = 1;

                if (para != null)
                {
                    if (para.ContainsKey("types"))
                        types = Convert.ToInt32(para["types"]);
                }


                if (types == 0)
                    types = 1;

                dataColl = new Dynamic.DataAccess.Account.LedgerGroupDB(hostName, dbName).getAllLedgerGroup(UserId, (Dynamic.APIEnitity.Account.LEDGERGROUPTYPES)types);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "LedgerGroupId","Name","Code","NatureOfGroup","IsSuccess","ResponseMSG"
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

        #region "AreaList"

        // POST api/GetAreaList
        /// <summary>
        ///  Get Area Master List  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.AreaMasterCollections>))]
        public IHttpActionResult GetAreaList()
        {
            Dynamic.BusinessEntity.Account.AreaMasterCollections dataColl = new Dynamic.BusinessEntity.Account.AreaMasterCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.AreaMasterDB(hostName, dbName).getAllAreaMaster(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "AreaId","Name","Alias","Code","District","AreaType","State","City","IsSuccess","ResponseMSG"
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

        #region "Agent"

        // POST api/GetAgentList
        /// <summary>
        ///  Get Agent/Salesman List  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.Agent>))]
        public IHttpActionResult GetAgentList()
        {
            Dynamic.BusinessEntity.Account.AgentCollections dataColl = new Dynamic.BusinessEntity.Account.AgentCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.AgentDB(hostName, dbName).getAllAgent(UserId, false);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "AgentId","Name","Alias","Code","Address","City","MobileNo","Email","BranchName","NameNP","CitizenshipNo","ParentAgent","AreaName","IsSuccess","ResponseMSG"
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

        #region "CostClass"

        // POST api/GetCostClassList
        /// <summary>
        ///  Get All CostClass List  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.CostClass>))]
        public IHttpActionResult GetCostClassList()
        {
            Dynamic.BusinessEntity.Account.CostClassCollections dataColl = new Dynamic.BusinessEntity.Account.CostClassCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.CostClassDB(hostName, dbName).getAllCostClass(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "CostClassId","Name","Alias","Code","IsSuccess","ResponseMSG"
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

        #region "CostCenter"

        // POST api/AutoCompleteCostCenterList
        /// <summary>
        ///  Get AutoCompleteCostCenterList 
        ///  searchBy as Int
        ///  searchValue as String
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Account.CostCenter>))]
        public IHttpActionResult AutoCompleteCostCenterList([FromBody] JObject para)
        {

            Dynamic.APIEnitity.Account.CostCenterCollections dataColl = new Dynamic.APIEnitity.Account.CostCenterCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    string searchBy = ((Dynamic.APIEnitity.Account.COSTCENTER_SEARCHOPTIONS)Convert.ToInt32(para["searchBy"])).GetStringValue();
                    string searchValue = Convert.ToString(para["searchValue"]);
                    dataColl = new Dynamic.DataAccess.Account.CostCenterDB(hostName, dbName).getAutoCompleteCostCenterList(UserId, searchBy, searchValue);
                    return Json(dataColl, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        #endregion

        #region "Voucher Modes"

        // POST api/GetAllVoucherModes
        /// <summary>
        ///  Get All VoucherModes 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.VoucherMode>))]
        public IHttpActionResult GetAllVoucherModes()
        {
            Dynamic.BusinessEntity.Account.VoucherModeCollections dataColl = new Dynamic.BusinessEntity.Account.VoucherModeCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(hostName, dbName).getAllVoucherShortDetails(UserId, null, null);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "VoucherId","VoucherName","VoucherType","Abbreviation","IsSuccess","ResponseMSG"
                                 }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetVoucherModes
        /// <summary>
        ///  Get  GetVoucherModes By Voucher Types
        /// </summary>
        /// <param name="VoucherType"> VoucherType as Int from Static Values</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Account.VoucherMode>))]
        public IHttpActionResult GetVoucherModes(int? VoucherType)
        {
            Dynamic.BusinessEntity.Account.VoucherModeCollections dataColl = new Dynamic.BusinessEntity.Account.VoucherModeCollections();
            try
            {
                if (VoucherType == 0)
                    VoucherType = null;

                dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(hostName, dbName).getAllVoucherShortDetails(UserId, VoucherType, null);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "VoucherId","VoucherName","VoucherType","Abbreviation","IsSuccess","ResponseMSG"
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

        #region "Save Journal"

        // POST api/SaveJournal
        /// <summary>
        ///  Save Accounting Transaction Journal 
        ///  {
        ///  VoucherDate:'2020-10-23',
        ///  ManualVoucherNO:'',
        ///  VoucherId:1,
        ///  RefNo:'test ref 1212',
        ///  Narration:'Test Narration',
        ///  LedgerAllocationColl:
        ///  [
        ///  {        
        ///     DRCR:1,
        ///     LedgerId:1,
        ///     DrAmount:5,
        ///     CrAmount:0
        ///   },
        ///  {
        ///     DRCR:2,
        ///     LedgerId:2,
        ///     DrAmount:0,
        ///     CrAmount:5
        ///  }
        /// ]
        /// }
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveJournal()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/journal"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.Transaction.Journal para = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("BranchId"))
                            para.BranchId = 0;

                        if (!jsonData.Contains("CostClassId"))
                            para.CostClassId = 0;

                        if (!jsonData.Contains("CurrencyId"))
                        {
                            para.CurrencyId = 0;
                            para.CurRate = 0;
                        }

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.Transaction.Journal jrn = new Dynamic.BusinessLogic.Account.Transaction.Journal(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.Journal;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = jrn.SaveFormData(para);
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

        #region "Save Contra"

        // POST api/SaveContra
        /// <summary>
        ///  Save Accounting Transaction Contra 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveContra()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/contra"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.Transaction.Journal para = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("BranchId"))
                            para.BranchId = 0;

                        if (!jsonData.Contains("CostClassId"))
                            para.CostClassId = 0;

                        if (!jsonData.Contains("CurrencyId"))
                        {
                            para.CurrencyId = 0;
                            para.CurRate = 0;
                        }

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.Transaction.Journal jrn = new Dynamic.BusinessLogic.Account.Transaction.Journal(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Contra);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.Contra;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = jrn.SaveFormData(para);
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

        #region "Save Receipt"

        // POST api/SaveReceipt
        /// <summary>
        ///  Save Accounting Transaction Receipt 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveReceipt()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/receipt"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.Transaction.Journal para = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("BranchId"))
                            para.BranchId = 0;

                        if (!jsonData.Contains("CostClassId"))
                            para.CostClassId = 0;

                        if (!jsonData.Contains("CurrencyId"))
                        {
                            para.CurrencyId = 0;
                            para.CurRate = 0;
                        }

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.Transaction.Journal jrn = new Dynamic.BusinessLogic.Account.Transaction.Journal(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Receipt);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.Receipt;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = jrn.SaveFormData(para);
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

        #region "Save Payment"

        // POST api/SavePayment
        /// <summary>
        ///  Save Accounting Transaction Payment 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SavePayment()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/payment"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.Transaction.Journal para = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("BranchId"))
                            para.BranchId = 0;

                        if (!jsonData.Contains("CostClassId"))
                            para.CostClassId = 0;

                        if (!jsonData.Contains("CurrencyId"))
                        {
                            para.CurrencyId = 0;
                            para.CurRate = 0;
                        }

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.Transaction.Journal jrn = new Dynamic.BusinessLogic.Account.Transaction.Journal(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Payment);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.Payment;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = jrn.SaveFormData(para);
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

        #region "Save PDC Post Dated Chequed"

        // POST api/SavePDC
        /// <summary>
        ///  Save Accounting Creation PDC ( Post Dated Chequed )
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SavePDC()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/pdc"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.PDC para = DeserializeObject<Dynamic.BusinessEntity.Account.PDC>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("VoucherDate"))
                            para.VoucherDate = DateTime.Today;

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.PDCDetails pdcBL = new Dynamic.BusinessLogic.Account.PDCDetails(hostName, dbName);
                        para.CreateBy = UserId;
                        para.ModifyBy = UserId;

                        resVal = pdcBL.SaveFormData(para);
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

        #region "Save BG DETAILS"

        // POST api/SaveBG
        /// <summary>
        ///  Save Accounting Creation BG
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveBG()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/account/bg"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Account.BGDetails para = DeserializeObject<Dynamic.BusinessEntity.Account.BGDetails>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        if (!jsonData.Contains("ExpiredDate"))
                            para.ExpiredDate = DateTime.Today;

                        if (provider.FileData.Count > 0)
                            para.DocumentColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Account.BGDetails pdcBL = new Dynamic.BusinessLogic.Account.BGDetails(hostName, dbName);
                        para.UserId = UserId;

                        resVal = pdcBL.SaveFormData(para);
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

        #region "Reporting"

        // POST api/GetDayBook
        /// <summary>
        ///  Get DayBook Summary 
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  VoucherType as Int ( Optional)
        ///  IsPost as true/false (Optional) default true
        ///  BranchId as int(optional) default all
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.DayBookCollections>))]
        public IHttpActionResult GetDayBook([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.DayBookCollections dataColl = new Dynamic.ReportEntity.Account.DayBookCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int VoucherType = 0;
                    if (para.ContainsKey("voucherType"))
                        VoucherType = Convert.ToInt32(para["voucherType"]);

                    bool IsPost = true;
                    if (para.ContainsKey("isPost"))
                        IsPost = Convert.ToBoolean(para["isPost"]);

                    int BranchId = 0;
                    if (para.ContainsKey("branchId"))
                        BranchId = Convert.ToInt32(para["branchId"]);

                    dataColl = new Dynamic.Reporting.Account.DayBook(hostName, dbName).getDayBook(dateFrom, dateTo, VoucherType, IsPost, BranchId, UserId);
                    foreach (var dc in dataColl)
                    {
                        //dc.VoucherName=dc.VoucherName.Replace(dc.VoucherType.ToString(), "");
                        //if (string.IsNullOrEmpty(dc.VoucherName))
                        //    dc.VoucherName = dc.VoucherType.ToString();


                        if (dc.IsInventory)
                        {
                            if (dc.ItemAllocationColl != null)
                            {
                                if (dc.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.Consumption || dc.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor)
                                    dc.DrAmount = dc.ItemAllocationColl.Sum(p1 => p1.Amount);
                            }
                        }

                        if (dc.VoucherType != Dynamic.BusinessEntity.Account.VoucherTypes.Receipt)
                        {
                            if (dc.LedgerAllocationColl != null && dc.LedgerAllocationColl.Count > 0)
                            {
                                dc.DrAmount = dc.LedgerAllocationColl[0].DrAmount;
                            }
                        }

                        if (dc.VoucherType != Dynamic.BusinessEntity.Account.VoucherTypes.Payment && dc.VoucherType != Dynamic.BusinessEntity.Account.VoucherTypes.Journal)
                        {
                            if (dc.LedgerAllocationColl != null)
                                dc.CrAmount = dc.LedgerAllocationColl.Sum(p1 => p1.CrAmount);
                        }



                    }
                    return Json(dataColl, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }



        // POST api/GetLedgerVoucher
        /// <summary>
        ///  Get Ledger Voucher
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  LedgerId as Int        
        ///  BranchId as string(optional) default all
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetLedgerVoucher([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections dataColl = new Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int LedgerId = 0;
                    if (para.ContainsKey("ledgerId"))
                        LedgerId = Convert.ToInt32(para["ledgerId"]);

                    string BranchId = "";
                    if (para.ContainsKey("branchId"))
                        BranchId = Convert.ToString(para["branchId"]);

                    double openingAmt = 0;
                    double closingAmt = 0, drAmt = 0, crAmt = 0;

                    dataColl = new Dynamic.Reporting.Account.LedgerSummary(hostName, dbName).getLedgerVoucherDetails(UserId, LedgerId, dateFrom, dateTo, ref openingAmt, false, false, BranchId);

                    if (dataColl != null && dataColl.IsSuccess)
                    {
                        drAmt = dataColl.Sum(p1 => p1.TranDrAmount);
                        crAmt = dataColl.Sum(p1 => p1.TranCrAmount);
                        closingAmt = openingAmt + drAmt - crAmt;
                    }

                    Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections tmpDataColl = new Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections();
                    double currentClosing = openingAmt;
                    DateTime currentDateTime = DateTime.Now;

                    var query = from dc in dataColl
                                group dc by new { dc.TranId, dc.VoucherId } into g
                                select new Dynamic.ReportEntity.Account.LedgerVoucherDetails
                                {
                                    Agent = g.First().Agent,
                                    AgentCode = g.First().AgentCode,
                                    AgentId = g.First().AgentId,
                                    AutoVoucherNo = g.First().AutoVoucherNo,
                                    Branch = g.First().Branch,
                                    CancelRemarks = g.First().CancelRemarks,
                                    CostCenterCrAmount = g.Sum(p1 => p1.CostCenterCrAmount),
                                    CostCenterDrAmount = g.Sum(p1 => p1.CostCenterDrAmount),
                                    CostClass = g.First().CostClass,
                                    CostClassId = g.First().CostClassId,
                                    CrAmount = g.Sum(p1 => p1.CrAmount),
                                    CurRate = g.First().CurRate,
                                    CurrencyName = g.First().CurrencyName,
                                    DrAmount = g.Sum(p1 => p1.DrAmount),
                                    EDND = g.First().EDND,
                                    EDNM = g.First().EDNM,
                                    EDNY = g.First().EDNY,
                                    EntryDate = g.First().EntryDate,
                                    IsLocked = g.First().IsLocked,
                                    IsParent = true,
                                    RefNo = g.First().RefNo,
                                    IsReject = g.First().IsReject,
                                    IsVerify = g.First().IsVerify,
                                    LogDateTime = g.First().LogDateTime,
                                    Narration = g.First().Narration,
                                    ND = g.First().ND,
                                    NM = g.First().NM,
                                    NY = g.First().NY,
                                    RejectRemarks = g.First().RejectRemarks,
                                    TranCrAmount = g.Sum(p1 => p1.TranCrAmount),
                                    TranDrAmount = g.Sum(p1 => p1.TranDrAmount),
                                    TranId = g.First().TranId,
                                    UserName = g.First().UserName,
                                    VerifyRemarks = g.First().VerifyRemarks,
                                    VoucherDate = g.First().VoucherDate,
                                    VoucherDateBS = g.First().VoucherDateBS,
                                    VoucherId = g.First().VoucherId,
                                    VoucherName = g.First().VoucherName,
                                    VoucherNo = g.First().VoucherNo,
                                    VoucherType = g.First().VoucherType,
                                    VoucherAge = (int)(currentDateTime - g.First().VoucherDate).TotalDays,
                                    CostCenter = g.First().CostCenter,
                                    CostCenterCode = g.First().CostCenterCode,
                                    AllChieldColl = g.ToList()

                                };
                    foreach (var v in query)
                    {
                        currentClosing += v.TranDrAmount - v.TranCrAmount;
                        v.CurrentClosing = currentClosing;

                        int ledgerAllocationId = 0;
                        Dynamic.ReportEntity.Account.LedgerVoucherDetails tmpLedDet = v.AllChieldColl.Find(p1 => p1.LedgerId == LedgerId);
                        if (tmpLedDet != null)
                        {

                            v.ChequeNo = IsNullOrEmptyStr(tmpLedDet.ChequeNo);
                            v.ChequeRemarks = IsNullOrEmptyStr(tmpLedDet.ChequeRemarks);
                            v.LedgerNarration = IsNullOrEmptyStr(tmpLedDet.LedgerNarration);

                            if (tmpLedDet.DrCr == Dynamic.BusinessEntity.Account.DRCR.DR)
                            {
                                tmpLedDet = v.AllChieldColl.Find(p1 => p1.DrCr == Dynamic.BusinessEntity.Account.DRCR.CR);
                            }
                            else
                                tmpLedDet = v.AllChieldColl.Find(p1 => p1.DrCr == Dynamic.BusinessEntity.Account.DRCR.DR);

                            if (tmpLedDet != null)
                            {
                                ledgerAllocationId = tmpLedDet.LedgerAllocationId;
                                v.LedgerName = tmpLedDet.LedgerName;
                                v.LedgerNarration = v.LedgerNarration + "  " + IsNullOrEmptyStr(tmpLedDet.LedgerNarration);
                                v.ChequeNo = v.ChequeNo + "  " + tmpLedDet.ChequeNo;
                                v.ChequeRemarks = v.ChequeRemarks + " " + tmpLedDet.ChequeRemarks;
                                v.DrAmount = tmpLedDet.DrAmount;
                                v.CrAmount = tmpLedDet.CrAmount;
                                v.CostCenter = tmpLedDet.CostCenter;
                                v.CostCenterCode = tmpLedDet.CostCenterCode;
                                v.CostCenterDrAmount = tmpLedDet.CostCenterDrAmount;
                                v.CostCenterCrAmount = tmpLedDet.CostCenterCrAmount;
                                //v.DrCr = tmpLedDet.DrCr;

                            }
                        }

                        v.ChieldColl = new List<Dynamic.ReportEntity.Account.LedgerVoucherDetails>();

                        foreach (var vv in v.AllChieldColl.Where(p1 => p1.LedgerId == LedgerId))
                        {
                            if (!string.IsNullOrEmpty(vv.CostCenter))
                                v.ChieldColl.Add(vv);
                        }

                        foreach (var vv in v.AllChieldColl.Where(p1 => p1.LedgerId != LedgerId && p1.LedgerAllocationId != ledgerAllocationId))
                        {
                            v.ChieldColl.Add(vv);
                        }



                        tmpDataColl.Add(v);
                    }

                    Dynamic.BusinessEntity.Common.LedgerDetails ledDetails = new Dynamic.DataAccess.Common.LedgerDB(hostName, dbName).getLedgerDetails(UserId, LedgerId, dateFrom, dateTo, null);

                    var returnVal = new
                    {
                        OpeningAmount = Math.Abs(openingAmt),
                        DrCr = openingAmt > 0 ? "DR" : "CR",
                        DrAmount = drAmt,
                        CrAmount = crAmt,
                        Closing = Math.Abs(closingAmt),
                        ClosingDrCr = closingAmt > 0 ? "DR" : "CR",
                        Details = ledDetails,
                        DataColl = tmpDataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetPDCList
        /// <summary>
        ///  Get PDC List
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  reportType as Int (1= Cleared Only,2=Expired Only,3=Cancel Only,4=Pending Onlye,5=All               
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetPDCList([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.PDCCollections dataColl = new Dynamic.ReportEntity.Account.PDCCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int reportType = 4;
                    if (para.ContainsKey("reportType"))
                        reportType = Convert.ToInt32(para["reportType"]);

                    dataColl = new Dynamic.Reporting.Account.PDC(hostName, dbName).getPDCDetails(UserId, dateFrom, dateTo, reportType,1);

                    var returnVal = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetCashBankBook
        /// <summary>
        ///  Get Cash and Bank BOok
        ///  DateFrom as Date
        ///  DateTo as Date 
        ///  branchId   as int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetCashBankBook([FromBody] JObject para)
        {
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int ledgerGroupId = 0;
                    if (para.ContainsKey("ledgerGroupId"))
                        ledgerGroupId = Convert.ToInt32(para["ledgerGroupId"]);

                    string branchId = "";
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToString(para["branchId"]);

                    bool forList = false;
                    if (para.ContainsKey("forList"))
                        forList = Convert.ToBoolean(para["forList"]);

                    if (ledgerGroupId == 0)
                        ledgerGroupId = 1;

                    CurrentDataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(hostName, dbName).getCashAndBankBookLedgerWise(UserId, dateFrom, dateTo);
                    var returnVal = new
                    {
                        DataColl = CurrentDataColl,
                        IsSuccess = CurrentDataColl.IsSuccess,
                        ResponseMSG = CurrentDataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                    });

                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/GetLedgerGroupSummary
        /// <summary>
        ///  Get LedgerGroup Summary
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  ledgerGroupId as Int
        ///  branchId   as int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetLedgerGroupSummary([FromBody] JObject para)
        {
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int ledgerGroupId = 0;
                    if (para.ContainsKey("ledgerGroupId"))
                        ledgerGroupId = Convert.ToInt32(para["ledgerGroupId"]);

                    string branchId = "";
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToString(para["branchId"]);

                    bool forList = false;
                    if (para.ContainsKey("forList"))
                        forList = Convert.ToBoolean(para["forList"]);

                    if (ledgerGroupId == 0)
                        ledgerGroupId = 1;

                    CurrentDataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(hostName, dbName).getTrailBalance(dateFrom, dateTo, ledgerGroupId, branchId.ToString(), UserId, "");

                    if (!forList)
                    {
                        LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(hostName, dbName).getAllLedgerGroup(UserId, Dynamic.APIEnitity.Account.LEDGERGROUPTYPES.ALL);
                        var tmpLG = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == ledgerGroupId);
                        if (tmpLG != null)
                        {
                            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
                            rootNote.IsLedgerGroup = true;
                            rootNote.LedgerGroupId = ledgerGroupId;
                            rootNote.LedgerGroupName = tmpLG.Name;
                            rootNote.Particulars = tmpLG.Name;
                            AddChieldNodeForTrailBalance(rootNote);

                            var returnVal = new
                            {
                                DataColl = rootNote,
                                IsSuccess = CurrentDataColl.IsSuccess,
                                ResponseMSG = CurrentDataColl.ResponseMSG
                            };
                            return Json(returnVal, new JsonSerializerSettings
                            {
                            });
                        }
                        else
                        {
                            var returnVal = new
                            {
                                DataColl = "",
                                IsSuccess = CurrentDataColl.IsSuccess,
                                ResponseMSG = CurrentDataColl.ResponseMSG
                            };
                            return Json(returnVal, new JsonSerializerSettings
                            {
                            });
                        }
                    }
                    else
                    {
                        var returnVal = new
                        {
                            DataColl = CurrentDataColl,
                            IsSuccess = CurrentDataColl.IsSuccess,
                            ResponseMSG = CurrentDataColl.ResponseMSG
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }


                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        private Dynamic.BusinessEntity.Account.LedgerGroupCollections LedgerGroupColl;
        private Dynamic.ReportEntity.Account.TrailBalanceCollections CurrentDataColl;
        private void AddChieldNodeForTrailBalance(Dynamic.ReportEntity.Account.TrailBalance beData)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.TrailBalance node = new Dynamic.ReportEntity.Account.TrailBalance();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 1;
                node.IsLedgerGroup = true;
                node.LedgerGroupName = v.Name;
                node.Particulars = v.Name;
                beData.ChieldColl.Add(node);
                AddChieldNodeForTrailBalance(node);
            }

            beData.ChieldColl.ForEach(delegate (Dynamic.ReportEntity.Account.TrailBalance v)
            {
                beData.Closing += v.Closing;
                beData.ClosingCr += v.ClosingCr;
                beData.ClosingDr += v.ClosingDr;
                beData.ClosingOpeningCr += v.ClosingOpeningCr;
                beData.ClosingOpeningDr += v.ClosingOpeningDr;
                beData.Opening += v.Opening;
                beData.OpeningCr += v.OpeningCr;
                beData.OpeningDr += v.OpeningDr;
                beData.TotalOpeningCr += v.TotalOpeningCr;
                beData.TotalOpeningDr += v.TotalOpeningDr;
                beData.Transaction += v.Transaction;
                beData.TransactionCr += v.TransactionCr;
                beData.TransactionDr += v.TransactionDr;
                beData.TransactionOpeningCr += v.TransactionOpeningCr;
                beData.TransactionOpeningDr += v.TransactionOpeningDr;
            });

            var led = from cd in CurrentDataColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                beData.Particulars = v.LedgerName;
                beData.Closing += v.Closing;
                beData.ClosingCr += v.ClosingCr;
                beData.ClosingDr += v.ClosingDr;
                beData.ClosingOpeningCr += v.ClosingOpeningCr;
                beData.ClosingOpeningDr += v.ClosingOpeningDr;
                beData.Opening += v.Opening;
                beData.OpeningCr += v.OpeningCr;
                beData.OpeningDr += v.OpeningDr;
                beData.TotalOpeningCr += v.TotalOpeningCr;
                beData.TotalOpeningDr += v.TotalOpeningDr;
                beData.Transaction += v.Transaction;
                beData.TransactionCr += v.TransactionCr;
                beData.TransactionDr += v.TransactionDr;
                beData.TransactionOpeningCr += v.TransactionOpeningCr;
                beData.TransactionOpeningDr += v.TransactionOpeningDr;

                if (v.Opening != 0 || v.Transaction != 0)
                    beData.ChieldColl.Add(v);
            }

        }


        // POST api/GetPDCList
        /// <summary>
        ///  Get BG List
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  flag as Int (1= All,2=ExpiredOnly,3=ActiveOnly )     
        ///  expiredAfterDays as Int
        ///  forLedgerId as Int
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.BGDetails>))]
        public IHttpActionResult GetBGList([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.BGDetailsCollections dataColl = new Dynamic.ReportEntity.Account.BGDetailsCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);


                    int flag = 1;
                    if (para.ContainsKey("flag"))
                        flag = Convert.ToInt32(para["flag"]);

                    int expiredAfterDays = 1;
                    if (para.ContainsKey("expiredAfterDays"))
                        expiredAfterDays = Convert.ToInt32(para["expiredAfterDays"]);

                    int ForLedgerId = 0;
                    if (para.ContainsKey("forLedgerId"))
                        ForLedgerId = Convert.ToInt32(para["forLedgerId"]);

                    dataColl = new Dynamic.Reporting.Account.BGDetails(hostName, dbName).getBGDetails(UserId, dateFrom, dateTo, expiredAfterDays, flag, ForLedgerId);

                    var returnVal = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }



        // POST api/GetStatutoryReport
        /// <summary>
        ///  Get Statutory Reports for Audit
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  reportType as Int (1= Debtor,2=Creditor )   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetStatutoryReport([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.LedgerTypeWiseSummaryCollections dataColl = new Dynamic.ReportEntity.Account.LedgerTypeWiseSummaryCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int reportType = 1;
                    if (para.ContainsKey("reportType"))
                        reportType = Convert.ToInt32(para["reportType"]);

                    dataColl = new Dynamic.Reporting.Account.LedgerTypeWiseSummary(hostName, dbName).getLedgerTypeWiseSummary(UserId, dateFrom, dateTo, (reportType == 1 ? true : false));

                    var returnVal = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetSalesVatRegister
        /// <summary>
        ///  Get Sales Vat Register Reports for Audit
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  reportType as Int (1= Debtor,2=Creditor )   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections))]
        public IHttpActionResult GetSalesVatRegister([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections CurrentDataColl = new Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int voucherId = 0;
                    if (para.ContainsKey("voucherId"))
                        voucherId = Convert.ToInt32(para["voucherId"]);

                    int branchId = 0;
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToInt32(para["branchId"]);

                    var vatRegister = new Dynamic.Reporting.Inventory.VatRegister(hostName, dbName);

                    Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections salesColl = null, salesReturnColl = null, purchaseReturnColl = null;
                    vatRegister.getSalesVatRegister(UserId, dateFrom, dateTo, ref salesColl, ref salesReturnColl, ref purchaseReturnColl, voucherId, branchId);

                    CurrentDataColl = new Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections();
                    Dynamic.ReportEntity.Inventory.VatRegisterSummary blanck = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();

                    // Sales Vat Register

                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat6 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat6.CustomerName = "SALES VAT REGISTER";
                    vat6.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    CurrentDataColl.Add(vat6);
                    CurrentDataColl.AddRange(salesColl);
                    CurrentDataColl.Add(blanck);

                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat7 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat7.CustomerName = "SALES VAT REGISTER TOTALS";
                    vat7.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    vat7.TotalSales = salesColl.Sum(P1 => P1.TotalSales);
                    vat7.NonTaxableSales = salesColl.Sum(P1 => P1.NonTaxableSales);
                    vat7.ExportSales = salesColl.Sum(P1 => P1.ExportSales);
                    vat7.TaxableSales = salesColl.Sum(P1 => P1.TaxableSales);
                    vat7.Vat = salesColl.Sum(P1 => P1.Vat);
                    vat7.ExDuty = salesColl.Sum(P1 => P1.ExDuty);
                    vat7.ExDutyAbleValue = salesColl.Sum(P1 => P1.ExDutyAbleValue);
                    vat7.Schame = salesColl.Sum(P1 => P1.Schame);
                    vat7.Freight = salesColl.Sum(P1 => P1.Freight);
                    vat7.Discount = salesColl.Sum(P1 => P1.Discount);
                    vat7.VatDiff = salesColl.Sum(P1 => P1.VatDiff);
                    vat7.Insurance = salesColl.Sum(P1 => P1.Insurance);
                    vat7.ItemAmount = salesColl.Sum(P1 => P1.ItemAmount);
                    CurrentDataColl.Add(vat7);
                    CurrentDataColl.Add(blanck);


                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat11 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat11.CustomerName = "DEBIT NOTE";
                    vat11.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    CurrentDataColl.Add(vat11);
                    CurrentDataColl.AddRange(purchaseReturnColl);
                    CurrentDataColl.Add(blanck);

                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat12 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat12.CustomerName = "DEBIT NOTE TOTALS";
                    vat12.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    vat12.TotalSales = purchaseReturnColl.Sum(P1 => P1.TotalSales);
                    vat12.NonTaxableSales = purchaseReturnColl.Sum(P1 => P1.NonTaxableSales);
                    vat12.ExportSales = purchaseReturnColl.Sum(P1 => P1.ExportSales);
                    vat12.TaxableSales = purchaseReturnColl.Sum(P1 => P1.TaxableSales);
                    vat12.Vat = purchaseReturnColl.Sum(P1 => P1.Vat);
                    vat12.ExDuty = purchaseReturnColl.Sum(P1 => P1.ExDuty);
                    vat12.ExDutyAbleValue = purchaseReturnColl.Sum(P1 => P1.ExDutyAbleValue);
                    vat12.Schame = purchaseReturnColl.Sum(P1 => P1.Schame);
                    vat12.Freight = purchaseReturnColl.Sum(P1 => P1.Freight);
                    vat12.Discount = purchaseReturnColl.Sum(P1 => P1.Discount);
                    vat12.VatDiff = purchaseReturnColl.Sum(P1 => P1.VatDiff);
                    vat12.Insurance = purchaseReturnColl.Sum(P1 => P1.Insurance);
                    vat12.ItemAmount = purchaseReturnColl.Sum(P1 => P1.ItemAmount);
                    CurrentDataColl.Add(vat12);
                    CurrentDataColl.Add(blanck);



                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat8 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat8.CustomerName = "CREDIT NOTE ";
                    vat8.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    CurrentDataColl.Add(vat8);
                    CurrentDataColl.AddRange(salesReturnColl);
                    CurrentDataColl.Add(blanck);

                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat9 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat9.CustomerName = "CREDIT NOTE TOTALS";
                    vat9.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    vat9.TotalSales = salesReturnColl.Sum(P1 => P1.TotalSales);
                    vat9.NonTaxableSales = salesReturnColl.Sum(P1 => P1.NonTaxableSales);
                    vat9.ExportSales = salesReturnColl.Sum(P1 => P1.ExportSales);
                    vat9.TaxableSales = salesReturnColl.Sum(P1 => P1.TaxableSales);
                    vat9.Vat = salesReturnColl.Sum(P1 => P1.Vat);
                    vat9.ExDuty = salesReturnColl.Sum(P1 => P1.ExDuty);
                    vat9.ExDutyAbleValue = salesReturnColl.Sum(P1 => P1.ExDutyAbleValue);
                    vat9.Schame = salesReturnColl.Sum(P1 => P1.Schame);
                    vat9.Freight = salesReturnColl.Sum(P1 => P1.Freight);
                    vat9.Discount = salesReturnColl.Sum(P1 => P1.Discount);
                    vat9.VatDiff = salesReturnColl.Sum(P1 => P1.VatDiff);
                    vat9.Insurance = salesReturnColl.Sum(P1 => P1.Insurance);
                    vat9.ItemAmount = salesReturnColl.Sum(P1 => P1.ItemAmount);

                    CurrentDataColl.Add(vat9);

                    CurrentDataColl.Add(blanck);

                    Dynamic.ReportEntity.Inventory.VatRegisterSummary vat10 = new Dynamic.ReportEntity.Inventory.VatRegisterSummary();
                    vat10.CustomerName = "NET SALES TOTAL";
                    vat10.RowType = Dynamic.ReportEntity.Inventory.RowTypes.Heading;
                    vat10.TotalSales = vat7.TotalSales - vat9.TotalSales + vat12.TotalSales;
                    vat10.NonTaxableSales = vat7.NonTaxableSales - vat9.NonTaxableSales + vat12.NonTaxableSales;
                    vat10.ExportSales = vat7.ExportSales - vat9.ExportSales + vat12.ExportSales;
                    vat10.TaxableSales = vat7.TaxableSales - vat9.TaxableSales + vat12.TaxableSales;
                    vat10.Vat = vat7.Vat - vat9.Vat + vat12.Vat;

                    vat10.ExDuty = vat7.ExDuty - vat9.ExDuty + vat12.ExDuty;
                    vat10.ExDutyAbleValue = vat7.ExDutyAbleValue - vat9.ExDutyAbleValue + vat12.ExDutyAbleValue;
                    vat10.Schame = vat7.Schame - vat9.Schame + vat12.Schame;
                    vat10.Freight = vat7.Freight - vat9.Freight + vat12.Freight;
                    vat10.Discount = vat7.Discount - vat9.Discount + vat12.Discount;
                    vat10.VatDiff = vat7.VatDiff - vat9.VatDiff + vat12.VatDiff;
                    vat10.Insurance = vat7.Insurance - vat9.Insurance + vat12.Insurance;
                    vat10.ItemAmount = vat7.ItemAmount - vat9.ItemAmount + vat12.ItemAmount;

                    CurrentDataColl.Add(vat10);
                    CurrentDataColl.Add(blanck);
                    CurrentDataColl.Add(blanck);

                    CurrentDataColl.IsSuccess = true;
                    CurrentDataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                    var returnVal = new
                    {
                        DataColl = CurrentDataColl,
                        IsSuccess = CurrentDataColl.IsSuccess,
                        ResponseMSG = CurrentDataColl.ResponseMSG
                    };
                    return Json(returnVal, new JsonSerializerSettings
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetNewVatRegister
        /// <summary>
        ///  Get Vat Register Reports for Audit
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  reportType as Int (1=Purchase,2=PurchaseReturn,3=Sales,4=SalesReturn)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.ReportEntity.Inventory.VatRegisterSummaryCollections))]
        public IHttpActionResult GetNewVatRegister([FromBody] JObject para)
        {
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int voucherId = 0;
                    if (para.ContainsKey("voucherId"))
                        voucherId = Convert.ToInt32(para["voucherId"]);

                    int branchId = 0;
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToInt32(para["branchId"]);

                    int reportType = 1;
                    if (para.ContainsKey("reportType"))
                        reportType = Convert.ToInt32(para["reportType"]);

                    if (reportType == 1)
                    {
                        var purchaseVat = new Dynamic.Reporting.Inventory.VatRegister(hostName, dbName).getPurchaseVatRegister(UserId, dateFrom, dateTo, voucherId, branchId);
                        var returnVal = new
                        {
                            DataColl = purchaseVat,
                            IsSuccess = purchaseVat.IsSuccess,
                            ResponseMSG = purchaseVat.ResponseMSG
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }
                    else if (reportType == 2)
                    {
                        var purchaseVat = new Dynamic.Reporting.Inventory.VatRegister(hostName, dbName).getPurchaseReturnVatRegister(UserId, dateFrom, dateTo, voucherId, branchId);
                        var returnVal = new
                        {
                            DataColl = purchaseVat,
                            IsSuccess = purchaseVat.IsSuccess,
                            ResponseMSG = purchaseVat.ResponseMSG
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }
                    else if (reportType == 3)
                    {
                        var purchaseVat = new Dynamic.Reporting.Inventory.VatRegister(hostName, dbName).getSalesVatRegister(UserId, dateFrom, dateTo, voucherId, branchId);
                        var returnVal = new
                        {
                            DataColl = purchaseVat,
                            IsSuccess = purchaseVat.IsSuccess,
                            ResponseMSG = purchaseVat.ResponseMSG
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }
                    else if (reportType == 4)
                    {
                        var purchaseVat = new Dynamic.Reporting.Inventory.VatRegister(hostName, dbName).getSalesReturnVatRegister(UserId, dateFrom, dateTo, voucherId, branchId);
                        var returnVal = new
                        {
                            DataColl = purchaseVat,
                            IsSuccess = purchaseVat.IsSuccess,
                            ResponseMSG = purchaseVat.ResponseMSG
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }
                    else
                    {
                        var returnVal = new
                        {
                            IsSuccess = false,
                            ResponseMSG = "Invalid Report Type"
                        };
                        return Json(returnVal, new JsonSerializerSettings
                        {
                        });
                    }
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        // POST api/GetIncomeExpenditure
        /// <summary>
        ///  Get Income Expenditure
        ///  DateFrom as Date
        ///  DateTo as Date      
        ///  BranchId as int(optional) default all
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.IncomeExpenditure>))]
        public IHttpActionResult GetIncomeExpenditure([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Account.IncomeExpenditureCollections dataColl = new Dynamic.ReportEntity.Account.IncomeExpenditureCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);
                     
                    int BranchId = 0;
                    if (para.ContainsKey("branchId"))
                        BranchId = Convert.ToInt32(para["branchId"]);

                    dataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(hostName, dbName).getIncomeExpenditure(dateFrom, dateTo, UserId,"");
        
                    return Json(dataColl, new JsonSerializerSettings
                    {                       
                    });
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetPLAsT
        /// <summary>
        ///  Get Profit and loss As T Format
        ///  DateFrom as Date
        ///  DateTo as Date 
        ///  branchId   as int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetPLAsT([FromBody] JObject para)
        {
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    string branchId = "";
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToString(para["branchId"]);



                    double openingStock = 0, closingStock = 0, closingStockOpening = 0;
                    LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(hostName, dbName).getAllLedgerGroup(UserId);
                    System.Collections.ArrayList data = new Dynamic.Reporting.Account.ProfitAndLoss(hostName, dbName).getProfitAndLoss(UserId,null, "", dateFrom, dateTo, "",true,false, ref openingStock, ref closingStock, ref closingStockOpening);
                    IncomeColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[0];
                    ExpensesColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[1];

                    System.Collections.ArrayList SalesTreeListView1 = new System.Collections.ArrayList();
                    System.Collections.ArrayList purchaseTreeListView1 = new System.Collections.ArrayList();
                    Dynamic.ReportEntity.Account.ProfitAndLosss blankData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    blankData.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.Others;


                    Dynamic.ReportEntity.Account.ProfitAndLosss closingStockNode = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    closingStockNode.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.ClosingStock;
                    closingStockNode.Particulars = " Closing Stock";
                    closingStockNode.IsLedgerGroup = true;
                    closingStockNode.OpeningAmt = closingStockOpening;
                    closingStockNode.TransactionAmt = closingStock;
                    closingStockNode.ClosingAmt = closingStock + closingStockOpening;
                    SalesTreeListView1.Add(closingStockNode);


                    Dynamic.BusinessEntity.Account.LedgerGroup salesLedgerGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 39);
                    Dynamic.ReportEntity.Account.ProfitAndLosss salesAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    salesAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAccount;
                    salesAc.Particulars = salesLedgerGroup.Name;
                    salesAc.IsLedgerGroup = true;
                    salesAc.LedgerGroupId = salesLedgerGroup.LedgerGroupId;

                    AddChieldNodeOfIncome(salesAc);
                    SalesTreeListView1.Add(salesAc);

                    Dynamic.BusinessEntity.Account.LedgerGroup directIncomeGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 40);
                    Dynamic.ReportEntity.Account.ProfitAndLosss directIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    directIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectIncome;
                    directIncomeAc.Particulars = directIncomeGroup.Name;
                    directIncomeAc.IsLedgerGroup = true;
                    directIncomeAc.LedgerGroupId = directIncomeGroup.LedgerGroupId;

                    AddChieldNodeOfIncome(directIncomeAc);
                    SalesTreeListView1.Add(directIncomeAc);

                    double otherIncomeOpening = 0, otherIncomeTran = 0, otherIncomeCloseing = 0;
                    foreach (var v in LedgerGroupColl.Where(p1 => p1.ParentGroupId == 1 && p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Income))
                    {
                        if (v.LedgerGroupId != 39 && v.LedgerGroupId != 40 && v.LedgerGroupId != 41)
                        {
                            Dynamic.ReportEntity.Account.ProfitAndLosss otherIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                            otherIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectIncome;
                            otherIncomeAc.Particulars = v.Name;
                            otherIncomeAc.IsLedgerGroup = true;
                            otherIncomeAc.LedgerGroupId = v.LedgerGroupId;

                            AddChieldNodeOfIncome(otherIncomeAc);
                            SalesTreeListView1.Add(otherIncomeAc);

                            otherIncomeOpening += otherIncomeAc.OpeningAmt;
                            otherIncomeTran += otherIncomeAc.TransactionAmt;
                            otherIncomeCloseing += otherIncomeAc.ClosingAmt;
                        }
                    }



                    Dynamic.ReportEntity.Account.ProfitAndLosss openingStockNode = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    openingStockNode.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.OpeningStock;
                    openingStockNode.Particulars = " Opening Stock";
                    openingStockNode.IsLedgerGroup = true;
                    openingStockNode.OpeningAmt = openingStock;
                    openingStockNode.ClosingAmt = openingStock;
                    purchaseTreeListView1.Add(openingStockNode);


                    Dynamic.BusinessEntity.Account.LedgerGroup purchaseLedgerGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 42);
                    Dynamic.ReportEntity.Account.ProfitAndLosss purchaseAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    purchaseAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAccount;
                    purchaseAc.Particulars = purchaseLedgerGroup.Name;
                    purchaseAc.IsLedgerGroup = true;
                    purchaseAc.LedgerGroupId = purchaseLedgerGroup.LedgerGroupId;

                    AddChieldNodeOfExpenses(purchaseAc);
                    purchaseTreeListView1.Add(purchaseAc);


                    Dynamic.BusinessEntity.Account.LedgerGroup directExpGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 43);
                    Dynamic.ReportEntity.Account.ProfitAndLosss directExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    directExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectExpenses;
                    directExpAc.Particulars = directExpGroup.Name;
                    directExpAc.IsLedgerGroup = true;
                    directExpAc.LedgerGroupId = directExpGroup.LedgerGroupId;

                    AddChieldNodeOfExpenses(directExpAc);
                    purchaseTreeListView1.Add(directExpAc);

                    double otherExpOpening = 0, otherExpTran = 0, otherExpCloseing = 0;
                    foreach (var v in LedgerGroupColl.Where(p1 => p1.ParentGroupId == 1 && p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Expenses))
                    {
                        if (v.LedgerGroupId != 42 && v.LedgerGroupId != 43 && v.LedgerGroupId != 44)
                        {
                            Dynamic.ReportEntity.Account.ProfitAndLosss otherIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                            otherIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectExpenses;
                            otherIncomeAc.Particulars = v.Name;
                            otherIncomeAc.IsLedgerGroup = true;
                            otherIncomeAc.LedgerGroupId = v.LedgerGroupId;

                            AddChieldNodeOfExpenses(otherIncomeAc);
                            purchaseTreeListView1.Add(otherIncomeAc);
                            otherExpOpening += otherIncomeAc.OpeningAmt;
                            otherExpTran += otherIncomeAc.TransactionAmt;
                            otherExpCloseing += otherIncomeAc.ClosingAmt;
                        }
                    }




                    Dynamic.ReportEntity.Account.ProfitAndLosss grossLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grossLostAndProfit.OpeningAmt = (closingStockNode.OpeningAmt + salesAc.OpeningAmt + directIncomeAc.OpeningAmt + otherIncomeOpening) - (openingStockNode.OpeningAmt + purchaseAc.OpeningAmt + directExpAc.OpeningAmt + otherExpOpening);
                    grossLostAndProfit.TransactionAmt = (closingStockNode.TransactionAmt + salesAc.TransactionAmt + directIncomeAc.ClosingAmt + otherIncomeTran) - (openingStockNode.TransactionAmt + purchaseAc.TransactionAmt + directExpAc.TransactionAmt + otherExpTran);
                    grossLostAndProfit.ClosingAmt = (closingStockNode.ClosingAmt + salesAc.ClosingAmt + directIncomeAc.ClosingAmt + otherIncomeCloseing) - (openingStockNode.ClosingAmt + purchaseAc.ClosingAmt + directExpAc.ClosingAmt + otherExpCloseing);
                    grossLostAndProfit.Particulars = (grossLostAndProfit.ClosingAmt > 0 ? " Gross Profit C/O " : " Gross Loss C/O");
                    grossLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.GrossProfitAndLoss;
                    if (grossLostAndProfit.ClosingAmt != 0)
                    {
                        if (grossLostAndProfit.ClosingAmt > 0)
                        {
                            purchaseTreeListView1.Add(grossLostAndProfit);
                            SalesTreeListView1.Add(blankData);
                        }
                        else
                        {
                            SalesTreeListView1.Add(grossLostAndProfit);
                            purchaseTreeListView1.Add(blankData);
                        }
                    }

                    purchaseTreeListView1.Add(blankData);
                    SalesTreeListView1.Add(blankData);


                    Dynamic.ReportEntity.Account.ProfitAndLosss totalIncome = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    totalIncome.Particulars = " TOTAL ";
                    totalIncome.OpeningAmt = otherIncomeOpening + salesAc.OpeningAmt + directIncomeAc.OpeningAmt + closingStockNode.OpeningAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.OpeningAmt) : 0);
                    totalIncome.TransactionAmt = otherIncomeTran + salesAc.TransactionAmt + directIncomeAc.TransactionAmt + closingStockNode.TransactionAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.TransactionAmt) : 0);
                    totalIncome.ClosingAmt = otherIncomeCloseing + salesAc.ClosingAmt + directIncomeAc.ClosingAmt + closingStockNode.ClosingAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.TransactionAmt) : 0);
                    totalIncome.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAndDirectIncomeTotal;


                    SalesTreeListView1.Add(blankData);
                    SalesTreeListView1.Add(totalIncome);
                    SalesTreeListView1.Add(blankData);


                    Dynamic.ReportEntity.Account.ProfitAndLosss totalExp = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    totalExp.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAndDirectExpensesTotal;
                    totalExp.Particulars = " TOTAL ";
                    totalExp.OpeningAmt = otherExpOpening + purchaseAc.OpeningAmt + directExpAc.OpeningAmt + openingStockNode.OpeningAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.OpeningAmt : 0);
                    totalExp.TransactionAmt = otherExpTran + purchaseAc.TransactionAmt + directExpAc.TransactionAmt + openingStockNode.TransactionAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.TransactionAmt : 0);
                    totalExp.ClosingAmt = otherExpCloseing + purchaseAc.ClosingAmt + directExpAc.ClosingAmt + openingStockNode.ClosingAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.ClosingAmt : 0);
                    purchaseTreeListView1.Add(blankData);
                    purchaseTreeListView1.Add(totalExp);
                    purchaseTreeListView1.Add(blankData);

                    if (grossLostAndProfit.ClosingAmt > 0)
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss grossProfitAndLossCD = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        grossProfitAndLossCD.Particulars = " Gross Profit And Loss B/F ";
                        grossProfitAndLossCD.OpeningAmt = grossLostAndProfit.OpeningAmt;
                        grossProfitAndLossCD.TransactionAmt = grossLostAndProfit.TransactionAmt;
                        grossProfitAndLossCD.ClosingAmt = grossLostAndProfit.ClosingAmt;

                        SalesTreeListView1.Add(grossProfitAndLossCD);
                        purchaseTreeListView1.Add(blankData);
                    }
                    else
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss grossProfitAndLossCD = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        grossProfitAndLossCD.Particulars = " Gross Profit And Loss B/F ";
                        grossProfitAndLossCD.OpeningAmt = grossLostAndProfit.OpeningAmt;
                        grossProfitAndLossCD.TransactionAmt = grossLostAndProfit.TransactionAmt;
                        grossProfitAndLossCD.ClosingAmt = Math.Abs(grossLostAndProfit.ClosingAmt);

                        purchaseTreeListView1.Add(grossProfitAndLossCD);
                        SalesTreeListView1.Add(blankData);
                    }

                    Dynamic.BusinessEntity.Account.LedgerGroup indirectIncGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 41);
                    Dynamic.ReportEntity.Account.ProfitAndLosss indirectIncAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    indirectIncAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectIncome;
                    indirectIncAc.Particulars = indirectIncGroup.Name;
                    indirectIncAc.IsLedgerGroup = true;
                    indirectIncAc.LedgerGroupId = indirectIncGroup.LedgerGroupId;

                    AddChieldNodeOfIncome(indirectIncAc);
                    SalesTreeListView1.Add(indirectIncAc);

                    Dynamic.BusinessEntity.Account.LedgerGroup indirectExpGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 44);
                    Dynamic.ReportEntity.Account.ProfitAndLosss indirectExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    indirectExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectExpenses;
                    indirectExpAc.Particulars = indirectExpGroup.Name;
                    indirectExpAc.IsLedgerGroup = true;
                    indirectExpAc.LedgerGroupId = indirectExpGroup.LedgerGroupId;

                    AddChieldNodeOfExpenses(indirectExpAc);
                    purchaseTreeListView1.Add(indirectExpAc);



                    Dynamic.ReportEntity.Account.ProfitAndLosss netLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    netLostAndProfit.OpeningAmt = grossLostAndProfit.OpeningAmt + indirectIncAc.OpeningAmt - indirectExpAc.OpeningAmt;
                    netLostAndProfit.TransactionAmt = grossLostAndProfit.TransactionAmt + indirectIncAc.TransactionAmt - indirectExpAc.TransactionAmt;
                    netLostAndProfit.ClosingAmt = grossLostAndProfit.ClosingAmt + indirectIncAc.ClosingAmt - indirectExpAc.ClosingAmt;
                    netLostAndProfit.Particulars = (netLostAndProfit.ClosingAmt > 0 ? " Net Profit C/O " : " Net Loss C/O");
                    netLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.NetProfitAndLoss;
                    if (netLostAndProfit.ClosingAmt != 0)
                    {
                        if (netLostAndProfit.ClosingAmt > 0)
                        {
                            purchaseTreeListView1.Add(netLostAndProfit);
                            SalesTreeListView1.Add(blankData);
                        }
                        else
                        {
                            SalesTreeListView1.Add(netLostAndProfit);
                            purchaseTreeListView1.Add(blankData);
                        }
                    }

                    purchaseTreeListView1.Add(blankData);
                    SalesTreeListView1.Add(blankData);

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalPurchase = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalPurchase.Particulars = " TOTAL ";
                    grandTotalPurchase.OpeningAmt = (netLostAndProfit.ClosingAmt > 0 ? netLostAndProfit.OpeningAmt : 0) + indirectExpAc.OpeningAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.OpeningAmt) : 0);
                    grandTotalPurchase.TransactionAmt = (netLostAndProfit.ClosingAmt > 0 ? netLostAndProfit.TransactionAmt : 0) + indirectExpAc.TransactionAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.TransactionAmt) : 0);
                    grandTotalPurchase.ClosingAmt = (netLostAndProfit.ClosingAmt > 0 ? netLostAndProfit.ClosingAmt : 0) + indirectExpAc.ClosingAmt + (grossLostAndProfit.ClosingAmt < 0 ? Math.Abs(grossLostAndProfit.ClosingAmt) : 0);


                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalSales = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalSales.Particulars = " TOTAL ";
                    grandTotalSales.OpeningAmt = (netLostAndProfit.ClosingAmt < 0 ? Math.Abs(netLostAndProfit.OpeningAmt) : 0) + indirectIncAc.OpeningAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.OpeningAmt : 0);
                    grandTotalSales.TransactionAmt = (netLostAndProfit.ClosingAmt < 0 ? Math.Abs(netLostAndProfit.TransactionAmt) : 0) + indirectIncAc.TransactionAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.TransactionAmt : 0);
                    grandTotalSales.ClosingAmt = (netLostAndProfit.ClosingAmt < 0 ? Math.Abs(netLostAndProfit.ClosingAmt) : 0) + indirectIncAc.ClosingAmt + (grossLostAndProfit.ClosingAmt > 0 ? grossLostAndProfit.ClosingAmt : 0);

                    purchaseTreeListView1.Add(grandTotalPurchase);
                    SalesTreeListView1.Add(grandTotalSales);

                    System.Collections.ArrayList finalDataColl = new System.Collections.ArrayList();
                    finalDataColl.Add(SalesTreeListView1);
                    finalDataColl.Add(purchaseTreeListView1);

                    var returnVal = new
                    {
                        DataColl = finalDataColl,
                        IsSuccess = true,
                        ResponseMSG = GLOBALMSG.SUCCESS
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                    });

                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections AssetsColl;
        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections LiabilitiesColl;
        // POST api/GetBSAsT
        /// <summary>
        ///  Get Balance Sheet As T Format
        ///  DateFrom as Date
        ///  DateTo as Date 
        ///  branchId   as int (optional)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Account.LedgerVoucherDetailsCollections>))]
        public IHttpActionResult GetBSAsT([FromBody] JObject para)
        {
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    string branchId = "";
                    if (para.ContainsKey("branchId"))
                        branchId = Convert.ToString(para["branchId"]);



                    LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(hostName, dbName).getAllLedgerGroup(UserId);
                    double netProfitAndLossOpening = 0, netProfitAndLossTransaction = 0;
                    Dynamic.Reporting.Account.ProfitAndLoss trail = new Dynamic.Reporting.Account.ProfitAndLoss(hostName, dbName);
                    System.Collections.ArrayList data = trail.getBalanceSheet(UserId, null, "", dateFrom, dateTo, "",true,false, ref netProfitAndLossOpening, ref netProfitAndLossTransaction);
                    AssetsColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[0];
                    LiabilitiesColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[1];

                    Dynamic.ReportEntity.Account.ProfitAndLosss blankData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    blankData.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.Others;

                    double totalAssetOpening = 0, totalAssetTransaction = 0, totalAssetClosing = 0;
                    double totalLiabilitiesOpening = 0, totalLiabilitiesTransaction = 0, totalLiabilitiesClosing = 0;

                    Dynamic.ReportEntity.Account.ProfitAndLosss capitalAndLiabilities = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    capitalAndLiabilities.Particulars = " Capital And Liabilities ";
                    foreach (var v in LedgerGroupColl.Where(p1 => p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Liability && p1.ParentGroupId == 1))
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss beData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        beData.Particulars = v.Name;
                        beData.IsLedgerGroup = true;
                        beData.LedgerGroupId = v.LedgerGroupId;
                        AddChieldNodeOfLiabilities(beData);
                        capitalAndLiabilities.ChieldsCOll.Add(beData);

                        totalLiabilitiesOpening += beData.OpeningAmt;
                        totalLiabilitiesTransaction += beData.TransactionAmt;
                        totalLiabilitiesClosing += beData.ClosingAmt;
                    }




                    Dynamic.ReportEntity.Account.ProfitAndLosss assets = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    assets.Particulars = " Assets ";
                    foreach (var v in LedgerGroupColl.Where(p1 => p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Assets && p1.ParentGroupId == 1))
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss beData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        beData.Particulars = v.Name;
                        beData.IsLedgerGroup = true;
                        beData.LedgerGroupId = v.LedgerGroupId;
                        AddChieldNodeOfAssets(beData);
                        assets.ChieldsCOll.Add(beData);

                        totalAssetOpening += beData.OpeningAmt;
                        totalAssetTransaction += beData.TransactionAmt;
                        totalAssetClosing += beData.ClosingAmt;

                    }


                    Dynamic.ReportEntity.Account.ProfitAndLosss netProfit = GetProfitAndLossAmount(dateFrom, dateTo);

                    if (netProfit != null)
                    {
                        if (netProfit.ClosingAmt > 0)
                        {
                            capitalAndLiabilities.ChieldsCOll.Add(netProfit);

                            totalLiabilitiesOpening += Math.Abs(netProfit.OpeningAmt);
                            totalLiabilitiesTransaction += Math.Abs(netProfit.TransactionAmt);
                            totalLiabilitiesClosing += Math.Abs(netProfit.ClosingAmt);

                        }
                        else
                        {
                            assets.ChieldsCOll.Add(netProfit);

                            totalAssetOpening += Math.Abs(netProfit.OpeningAmt);
                            totalAssetTransaction += Math.Abs(netProfit.TransactionAmt);
                            totalAssetClosing += Math.Abs(netProfit.ClosingAmt);
                        }
                    }


                    if (totalAssetClosing > totalLiabilitiesClosing)
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss diffAmt = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        diffAmt.Particulars = " Diff. In Opening Balance ";
                        diffAmt.OpeningAmt = totalAssetOpening - totalLiabilitiesOpening;
                        diffAmt.TransactionAmt = totalAssetTransaction - totalLiabilitiesTransaction;
                        diffAmt.ClosingAmt = totalAssetClosing - totalLiabilitiesClosing;

                        if (Math.Round(diffAmt.ClosingAmt, 3) != 0)
                        {
                            capitalAndLiabilities.ChieldsCOll.Add(diffAmt);
                            totalLiabilitiesClosing += diffAmt.ClosingAmt;
                            totalLiabilitiesOpening += diffAmt.OpeningAmt;
                            totalLiabilitiesTransaction += diffAmt.TransactionAmt;
                        }


                    }
                    else if (totalLiabilitiesClosing > totalAssetClosing)
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss diffAmt = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        diffAmt.Particulars = " Diff. In Opening Balance ";
                        diffAmt.OpeningAmt = totalLiabilitiesOpening - totalAssetOpening;
                        diffAmt.TransactionAmt = totalLiabilitiesTransaction - totalAssetTransaction;
                        diffAmt.ClosingAmt = totalLiabilitiesClosing - totalAssetClosing;

                        if (Math.Round(diffAmt.ClosingAmt, 3) != 0)
                        {
                            assets.ChieldsCOll.Add(diffAmt);
                            totalAssetTransaction += diffAmt.TransactionAmt;
                            totalAssetClosing += diffAmt.ClosingAmt;
                            totalAssetOpening += diffAmt.OpeningAmt;
                        }
                    }
                    System.Collections.ArrayList finalDataColl = new System.Collections.ArrayList();
                    capitalAndLiabilities.OpeningAmt = totalLiabilitiesOpening;
                    capitalAndLiabilities.TransactionAmt = totalLiabilitiesTransaction;
                    capitalAndLiabilities.ClosingAmt = totalLiabilitiesClosing;
                    finalDataColl.Add(capitalAndLiabilities);

                    assets.OpeningAmt = totalAssetOpening;
                    assets.TransactionAmt = totalAssetTransaction;
                    assets.ClosingAmt = totalAssetClosing;
                    finalDataColl.Add(assets);
                    var returnVal = new
                    {
                        DataColl = finalDataColl,
                        IsSuccess = true,
                        ResponseMSG = GLOBALMSG.SUCCESS
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                    });

                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        private void AddChieldNodeOfAssets(Dynamic.ReportEntity.Account.ProfitAndLosss beData)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId && lg.ParentGroupId != 1
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.ProfitAndLosss node = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 3;
                node.IsLedgerGroup = true;
                node.Particulars = v.Name;
                beData.ChieldsCOll.Add(node);
                AddChieldNodeOfAssets(node);
            }

            beData.ChieldsCOll.ForEach(delegate (Dynamic.ReportEntity.Account.ProfitAndLosss v)
            {
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;
            });

            var led = from cd in AssetsColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                v.TotalSpace = beData.TotalSpace + 2;
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;

                beData.ChieldsCOll.Add(v);
            }

        }
        private void AddChieldNodeOfLiabilities(Dynamic.ReportEntity.Account.ProfitAndLosss beData)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId && lg.ParentGroupId != 1
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.ProfitAndLosss node = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 3;
                node.IsLedgerGroup = true;
                node.Particulars = v.Name;
                beData.ChieldsCOll.Add(node);
                AddChieldNodeOfLiabilities(node);
            }

            beData.ChieldsCOll.ForEach(delegate (Dynamic.ReportEntity.Account.ProfitAndLosss v)
            {
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;
            });

            var led = from cd in LiabilitiesColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                v.TotalSpace = beData.TotalSpace + 2;
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;

                beData.ChieldsCOll.Add(v);
            }

        }
        private Dynamic.BusinessEntity.Account.LedgerGroupCollections LedgerGroupColl1;
        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections IncomeColl;
        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections ExpensesColl;
        private Dynamic.ReportEntity.Account.ProfitAndLosss GetProfitAndLossAmount(DateTime fromDate, DateTime toDate)
        {
            try
            {

                LedgerGroupColl1 = new Dynamic.DataAccess.Account.LedgerGroupDB(hostName, dbName).getAllLedgerGroupOfProfitAndLoss(UserId);

                double openingStock = 0, closingStock = 0, closingStockOpening = 0;
                Dynamic.Reporting.Account.ProfitAndLoss trail = new Dynamic.Reporting.Account.ProfitAndLoss(hostName, dbName);
                System.Collections.ArrayList data = trail.getProfitAndLoss(UserId, null, "", fromDate, toDate, "",true,false, ref openingStock, ref closingStock, ref closingStockOpening);
                IncomeColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[0];
                ExpensesColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[1];

                Dynamic.BusinessEntity.Account.LedgerGroup salesLedgerGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 39);
                Dynamic.ReportEntity.Account.ProfitAndLosss salesAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                salesAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAccount;
                salesAc.Particulars = salesLedgerGroup.Name;
                salesAc.IsLedgerGroup = true;
                salesAc.LedgerGroupId = salesLedgerGroup.LedgerGroupId;

                AddChieldNodeOfIncome(salesAc);

                Dynamic.BusinessEntity.Account.LedgerGroup directIncomeGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 40);
                Dynamic.ReportEntity.Account.ProfitAndLosss directIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                directIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectIncome;
                directIncomeAc.Particulars = directIncomeGroup.Name;
                directIncomeAc.IsLedgerGroup = true;
                directIncomeAc.LedgerGroupId = directIncomeGroup.LedgerGroupId;

                AddChieldNodeOfIncome(directIncomeAc);

                Dynamic.ReportEntity.Account.ProfitAndLosss totalIncome = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                totalIncome.Particulars = " TOTAL ";

                foreach (var v in LedgerGroupColl1.Where(p1 => p1.ParentGroupId == 1 && p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Income))
                {
                    if (v.LedgerGroupId != 39 && v.LedgerGroupId != 40 && v.LedgerGroupId != 41)
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss otherIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        otherIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectIncome;
                        otherIncomeAc.Particulars = v.Name;
                        otherIncomeAc.IsLedgerGroup = true;
                        otherIncomeAc.LedgerGroupId = v.LedgerGroupId;

                        AddChieldNodeOfIncome(otherIncomeAc);

                        totalIncome.OpeningAmt += otherIncomeAc.OpeningAmt;
                        totalIncome.TransactionAmt += otherIncomeAc.TransactionAmt;
                        totalIncome.ClosingAmt += otherIncomeAc.ClosingAmt;
                    }
                }


                totalIncome.OpeningAmt += salesAc.OpeningAmt + directIncomeAc.OpeningAmt;
                totalIncome.TransactionAmt += salesAc.TransactionAmt + directIncomeAc.TransactionAmt;
                totalIncome.ClosingAmt += salesAc.ClosingAmt + directIncomeAc.ClosingAmt + closingStock;
                totalIncome.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAndDirectIncomeTotal;

                Dynamic.ReportEntity.Account.ProfitAndLosss blankData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                blankData.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.Others;


                Dynamic.BusinessEntity.Account.LedgerGroup purchaseLedgerGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 42);
                Dynamic.ReportEntity.Account.ProfitAndLosss purchaseAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                purchaseAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAccount;
                purchaseAc.Particulars = purchaseLedgerGroup.Name;
                purchaseAc.IsLedgerGroup = true;
                purchaseAc.LedgerGroupId = purchaseLedgerGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(purchaseAc);

                Dynamic.BusinessEntity.Account.LedgerGroup directExpGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 43);
                Dynamic.ReportEntity.Account.ProfitAndLosss directExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                directExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectExpenses;
                directExpAc.Particulars = directExpGroup.Name;
                directExpAc.IsLedgerGroup = true;
                directExpAc.LedgerGroupId = directExpGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(directExpAc);


                Dynamic.ReportEntity.Account.ProfitAndLosss totalExp = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                totalExp.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAndDirectExpensesTotal;
                totalExp.Particulars = " TOTAL ";
                foreach (var v in LedgerGroupColl1.Where(p1 => p1.ParentGroupId == 1 && p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Expenses))
                {
                    if (v.LedgerGroupId != 42 && v.LedgerGroupId != 43 && v.LedgerGroupId != 44)
                    {
                        Dynamic.ReportEntity.Account.ProfitAndLosss otherIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                        otherIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectExpenses;
                        otherIncomeAc.Particulars = v.Name;
                        otherIncomeAc.IsLedgerGroup = true;
                        otherIncomeAc.LedgerGroupId = v.LedgerGroupId;

                        AddChieldNodeOfExpenses(otherIncomeAc);


                        totalExp.OpeningAmt += otherIncomeAc.OpeningAmt;
                        totalExp.TransactionAmt += otherIncomeAc.TransactionAmt;
                        totalExp.ClosingAmt += otherIncomeAc.ClosingAmt;
                    }
                }



                totalExp.OpeningAmt += purchaseAc.OpeningAmt + directExpAc.OpeningAmt;
                totalExp.TransactionAmt += purchaseAc.TransactionAmt + directExpAc.TransactionAmt;
                totalExp.ClosingAmt += purchaseAc.ClosingAmt + directExpAc.ClosingAmt + openingStock;

                Dynamic.ReportEntity.Account.ProfitAndLosss grossLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                grossLostAndProfit.OpeningAmt = totalIncome.OpeningAmt - totalExp.OpeningAmt;
                grossLostAndProfit.TransactionAmt = totalIncome.TransactionAmt - totalExp.TransactionAmt;
                grossLostAndProfit.ClosingAmt = totalIncome.ClosingAmt - totalExp.ClosingAmt;
                grossLostAndProfit.Particulars = (grossLostAndProfit.ClosingAmt > 0 ? " Gross Profit " : " Gross Loss ");
                grossLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.GrossProfitAndLoss;


                Dynamic.BusinessEntity.Account.LedgerGroup indirectIncGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 41);
                Dynamic.ReportEntity.Account.ProfitAndLosss indirectIncAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                indirectIncAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectIncome;
                indirectIncAc.Particulars = indirectIncGroup.Name;
                indirectIncAc.IsLedgerGroup = true;
                indirectIncAc.LedgerGroupId = indirectIncGroup.LedgerGroupId;

                AddChieldNodeOfIncome(indirectIncAc);


                Dynamic.BusinessEntity.Account.LedgerGroup indirectExpGroup = LedgerGroupColl1.Find(p1 => p1.LedgerGroupId == 44);
                Dynamic.ReportEntity.Account.ProfitAndLosss indirectExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                indirectExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectExpenses;
                indirectExpAc.Particulars = indirectExpGroup.Name;
                indirectExpAc.IsLedgerGroup = true;
                indirectExpAc.LedgerGroupId = indirectExpGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(indirectExpAc);

                Dynamic.ReportEntity.Account.ProfitAndLosss netLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                netLostAndProfit.OpeningAmt = grossLostAndProfit.OpeningAmt + indirectIncAc.OpeningAmt - indirectExpAc.OpeningAmt;
                netLostAndProfit.TransactionAmt = grossLostAndProfit.TransactionAmt + indirectIncAc.TransactionAmt - indirectExpAc.TransactionAmt;
                netLostAndProfit.ClosingAmt = grossLostAndProfit.ClosingAmt + indirectIncAc.ClosingAmt - indirectExpAc.ClosingAmt;
                netLostAndProfit.Particulars = (netLostAndProfit.ClosingAmt > 0 ? " Net Profit " : " Net Loss ");
                netLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.NetProfitAndLoss;

                if (netLostAndProfit.ClosingAmt != 0)
                    return netLostAndProfit;

                return null;
            }
            catch (Exception ee)
            {
                return null;
            }
        }
        private void AddChieldNodeOfIncome(Dynamic.ReportEntity.Account.ProfitAndLosss beData)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.ProfitAndLosss node = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 3;
                node.IsLedgerGroup = true;
                node.Particulars = v.Name;
                beData.ChieldsCOll.Add(node);
                AddChieldNodeOfIncome(node);
            }

            beData.ChieldsCOll.ForEach(delegate (Dynamic.ReportEntity.Account.ProfitAndLosss v)
            {
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;
            });

            var led = from cd in IncomeColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                v.TotalSpace = beData.TotalSpace + 2;
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;

                beData.ChieldsCOll.Add(v);
            }

        }
        private void AddChieldNodeOfExpenses(Dynamic.ReportEntity.Account.ProfitAndLosss beData)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.ProfitAndLosss node = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 3;
                node.IsLedgerGroup = true;
                node.Particulars = v.Name;
                beData.ChieldsCOll.Add(node);
                AddChieldNodeOfExpenses(node);
            }

            beData.ChieldsCOll.ForEach(delegate (Dynamic.ReportEntity.Account.ProfitAndLosss v)
            {
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;
            });

            var led = from cd in ExpensesColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                v.TotalSpace = beData.TotalSpace + 2;
                beData.OpeningAmt += v.OpeningAmt;
                beData.TransactionAmt += v.TransactionAmt;
                beData.ClosingAmt += v.ClosingAmt;

                beData.ChieldsCOll.Add(v);
            }

        }
        #endregion

        #region "Save Employee Salary"

        // POST api/SaveEmpSalaryJournal
        /// <summary>
        ///  Save Accounting Transaction Employee Salary Details from HR         
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveEmpSalaryJournal([FromBody] Dynamic.BusinessEntity.Account.Transaction.EmployeeSalaryCollections para)
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
                    var empDB = new Dynamic.DataAccess.Account.Transaction.EmployeeSalaryDB(hostName, dbName);
                    resVal = empDB.SaveUpdate(UserId, para,0);
                    if (resVal.IsSuccess && para.Count > 0)
                    {
                        var paraFirst = para.First();
                        Dynamic.BusinessEntity.Account.Transaction.EmployeeSalaryCollections tmpDataColl = empDB.getDataForJournal(UserId, paraFirst.YearId, paraFirst.MonthId, paraFirst.BranchCode);
                        if (tmpDataColl.IsSuccess)
                        {
                            string ledgerMissing = "", costCenterMissing = "", missingBranch = "", missingVoucher = "", missingCostClass = "";

                            bool missing = false;

                            var tmpFirst = tmpDataColl.First();

                            if (!tmpFirst.BranchId.HasValue || tmpFirst.BranchId.Value == 0)
                            {
                                missingBranch = "Branch : " + tmpFirst.BranchCode;
                                missing = true;
                            }

                            if (!tmpFirst.VoucherId.HasValue || tmpFirst.VoucherId.Value == 0)
                            {
                                missingVoucher = "Voucher : " + tmpFirst.VoucherName;
                                missing = true;
                            }

                            if (!tmpFirst.CostClassId.HasValue || tmpFirst.CostClassId.Value == 0)
                            {
                                missingCostClass = "CostClass : " + tmpFirst.CostClassName;
                                missing = true;
                            }

                            foreach (var v in tmpDataColl)
                            {
                                if (!v.LedgerId.HasValue || v.LedgerId.Value == 0)
                                {
                                    ledgerMissing = ledgerMissing + " , " + v.LedgerCode;
                                    missing = true;
                                }
                                if (!v.CostCenterId.HasValue || v.CostCenterId.Value == 0)
                                {
                                    costCenterMissing = costCenterMissing + " , " + v.EmployeeCode;
                                    missing = true;
                                }
                            }

                            if (!missing)
                            {
                                var query = from dc in tmpDataColl
                                            group dc by dc.LedgerId.Value into g
                                            select new
                                            {
                                                LedgerId = g.Key,
                                                DrAmount = g.Sum(p1 => p1.DrAmount),
                                                CrAmount = g.Sum(p1 => p1.CrAmount),
                                                ChieldColl = g
                                            };



                                Dynamic.BusinessEntity.Account.Transaction.Journal jrn = new Dynamic.BusinessEntity.Account.Transaction.Journal();
                                jrn.VoucherDate = tmpFirst.VoucherDate;
                                jrn.NY = tmpFirst.NY;
                                jrn.NM = tmpFirst.NM;
                                jrn.ND = tmpFirst.ND;
                                jrn.EntryDate = DateTime.Now;
                                jrn.CreatedBy = UserId;
                                jrn.ModifyBy = UserId;
                                jrn.RefNo = "HR JV";
                                jrn.Narration = "BEING SALARY AMOUNT CREDITED FOR THE MONTH  OF " + tmpFirst.NM.ToString() + " Year " + tmpFirst.NY.ToString() + " AS PER ATTENDANCE";
                                jrn.TranType = Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal;
                                jrn.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.Journal;
                                jrn.Amount = tmpDataColl.Sum(p1 => p1.DrAmount);
                                jrn.VoucherId = tmpFirst.VoucherId.Value;
                                jrn.CostClassId = tmpFirst.CostClassId.Value;
                                jrn.BranchId = tmpFirst.BranchId.Value;
                                jrn.CurrencyId = 0;
                                jrn.CurRate = 0;
                                jrn.LedgerAllocationColl = new Dynamic.BusinessEntity.Account.Transaction.LedgerAllocationCollections();
                                foreach (var q in query)
                                {
                                    Dynamic.BusinessEntity.Account.Transaction.LedgerAllocation ledAllocation = new Dynamic.BusinessEntity.Account.Transaction.LedgerAllocation();
                                    ledAllocation.LedgerId = q.LedgerId;
                                    ledAllocation.DrAmount = q.DrAmount;
                                    ledAllocation.CrAmount = q.CrAmount;
                                    ledAllocation.Narration = "Auto";
                                    ledAllocation.CostCenterColl = new Dynamic.BusinessEntity.Account.Transaction.CostCenterDetailsCollections();
                                    if (q.DrAmount != 0)
                                        ledAllocation.DrCr = Dynamic.BusinessEntity.Account.DRCR.DR;
                                    else
                                        ledAllocation.DrCr = Dynamic.BusinessEntity.Account.DRCR.CR;

                                    foreach (var v in q.ChieldColl)
                                    {
                                        Dynamic.BusinessEntity.Account.Transaction.CostCenterDetails ccD = new Dynamic.BusinessEntity.Account.Transaction.CostCenterDetails();
                                        ccD.CostCategoriesId = v.CostCategoryId.Value;
                                        ccD.CostCenterId = v.CostCenterId.Value;
                                        ccD.DrAmount = v.DrAmount;
                                        ccD.CrAmount = v.CrAmount;
                                        ccD.DrCr = ledAllocation.DrCr;
                                        ccD.Narration = "Auto";
                                        ledAllocation.CostCenterColl.Add(ccD);
                                    }

                                    jrn.LedgerAllocationColl.Add(ledAllocation);
                                }

                                Dynamic.BusinessLogic.Account.Transaction.Journal jrnBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal);
                                resVal = jrnBL.SaveFormData(jrn);

                                if (!resVal.IsSuccess)
                                {
                                    var delRes = empDB.DelDataForJournal(UserId, para.First().YearId, para.First().MonthId, para.First().BranchCode);
                                    if (!delRes.IsSuccess)
                                        resVal.ResponseMSG = resVal.ResponseMSG + delRes.ResponseMSG;
                                }
                                else
                                {
                                    var upRes = empDB.UpdateJrnTranDataForJournal(UserId, para.First().YearId, para.First().MonthId, para.First().BranchCode, resVal.RId);
                                    if (upRes.IsSuccess)
                                    {
                                        var resJrn = jrnBL.getJournalByTranId(resVal.RId,UserId);
                                        if (resJrn != null)
                                        {
                                            resVal.ResponseMSG = " Voucher No:- " + resJrn.AutoManualNo;
                                        }
                                    }
                                    else
                                    {
                                        resVal.ResponseMSG = upRes.ResponseMSG;
                                    }
                                }
                            }
                            else
                            {
                                var delRes = empDB.DelDataForJournal(UserId, para.First().YearId, para.First().MonthId, para.First().BranchCode);
                                if (!delRes.IsSuccess)
                                    resVal.ResponseMSG = resVal.ResponseMSG + delRes.ResponseMSG;

                                resVal.ResponseMSG = "Some Data Not found :" + missingBranch + missingVoucher + missingCostClass + costCenterMissing + ledgerMissing;
                            }
                        }
                        else
                            resVal.ResponseMSG = tmpDataColl.ResponseMSG;
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
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        #endregion
    }
}
