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
using System.Configuration;
using System.Net.Http.Headers;
using System.IO;
using PivotalERP.Global;
using System.Threading.Tasks;

namespace AcademicERP.Controllers
{
   
    public class GeneralController : APIBaseController
    {


        [HttpGet]
        [ResponseType(typeof(ResponeValues))]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAppVer()
        {
            try
            {
                int uid = 1;
                if (User.Identity.IsAuthenticated)
                    uid = UserId;

                var appVer = new Dynamic.DataAccess.Setup.GeneralConfigurationDB(hostName, dbName).getGeneralConfiguration(uid);

                var retVal = new
                {
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS,
                    AppVer = appVer.AppVersion,
                    IOSVer = appVer.IOSVersion
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

        // POST v1/UpdateAppVer
        /// <summary>
        /// Update App Version
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateAppVer([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                string appVer = "";
                if (para.ContainsKey("ver"))
                    appVer = Convert.ToString(para["ver"]);

                resVal = new Dynamic.DataAccess.Setup.GeneralConfigurationDB(hostName, dbName).UpdateAppVer(UserId, appVer);

                var retVal = new
                {
                    IsSuccess = resVal.IsSuccess,
                    ResponseMSG = resVal.ResponseMSG
                };

                return Json(retVal, new JsonSerializerSettings
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

        // POST v1/UpdateAppVer
        /// <summary>
        /// Update App Version
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdateIOSVer([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                string appVer = "";
                if (para.ContainsKey("ver"))
                    appVer = Convert.ToString(para["ver"]);

                resVal = new Dynamic.DataAccess.Setup.GeneralConfigurationDB(hostName, dbName).UpdateIOSAppVer(UserId, appVer);

                var retVal = new
                {
                    IsSuccess = resVal.IsSuccess,
                    ResponseMSG = resVal.ResponseMSG
                };

                return Json(retVal, new JsonSerializerSettings
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


        [HttpGet]
        [ResponseType(typeof(ResponeValues))]        
        public async Task<IHttpActionResult> GetFeeConfig()
        {
            var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(UserId, hostName, dbName).GetFeeConfigurationById(0,this.AcademicYearId);

            var retVal = new
            {
                IsSuccess = feeConfig.IsSuccess,
                ResponseMSG = feeConfig.ResponseMSG,
                Data = feeConfig
            };
            return Json(retVal, new JsonSerializerSettings
            {
            });

        }

        // GET v1/GetUserDetail
        /// <summary>
        /// General User Details        
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Dynamic.APIEnitity.Security.User))]
        public IHttpActionResult GetUserDetail()
        {
            
            ResponeValues resVal = new ResponeValues();
            
            try
            {                
                Dynamic.APIEnitity.Security.User retVal = new Dynamic.DataAccess.Security.UserDB(hostName, dbName).getUserByIdForAPI(UserId);
                
                return Json(retVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch(Exception ee)
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

        // POST v1/UpdatePwd
        /// <summary>
        /// Update Login User Pwd
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult UpdatePwd([FromBody] JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                string oldPwd = "", newPwd = "";
                if (para.ContainsKey("oldPwd"))
                    oldPwd = Convert.ToString(para["oldPwd"]);

                if (para.ContainsKey("newPwd"))
                    newPwd = Convert.ToString(para["newPwd"]);

                if (string.IsNullOrEmpty(oldPwd))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please ! Enter Old Pwd";
                }
                else if (string.IsNullOrEmpty(newPwd))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please ! Enter New Pwd";
                }
                else
                {
                    resVal = new AcademicLib.BL.Global(UserId,hostName, dbName).UpdatePwd( oldPwd, newPwd,"");
                }

                var retVal = new
                {
                    IsSuccess = resVal.IsSuccess,
                    ResponseMSG = resVal.ResponseMSG
                };

                return Json(retVal, new JsonSerializerSettings
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
        #region "Dashboard"

        // POST api/GetDashboard
        /// <summary>
        ///  Get GetDashboard         
        /// </summary>
        /// <param name="para">Json(Object)
        ///  reportTypes="1,2,3"
        ///  DateFrom="2020-01-01" Optional
        ///  DateTo="2020-12-01" Optional
        ///  CashLedgerId=null Optional Int DataType
        ///  BankLedgerId=null Optional Int DataType
        ///  CashFlowLedgerId=null Optional Int DataType
        ///  BankFlowLedgerId=null Optional Int DataType
        ///  PurchaseLedgerId=null Optional Int DataType
        ///  SalesLedgerId=null Optional Int DataType
        ///  UnitId=null Optional Int DataType
        ///  AgentId=null Optional Int DataType
        ///  ProductTypeId=null Optional Int DataType
        ///  ProductBrandId=null Optional Int DataType
        ///  OrderBy=null Optional Int DataType
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.Dashboard.BE.Common))]
        public IHttpActionResult GetDashboard([FromBody]Dynamic.Dashboard.BE.CommonPara para)
        {
            Dynamic.Dashboard.BE.Common dataColl = new Dynamic.Dashboard.BE.Common();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {                    
                    para.UserId = UserId;
                    dataColl = new Dynamic.Dashboard.DA.CommonDB(hostName, dbName).getCommon(para);

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

        // GET v1/GetBranchList
        /// <summary>
        /// Get All Branch List
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Security.Branch))]
        public IHttpActionResult GetBranchList()
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                Dynamic.BusinessEntity.Security.BranchCollections retVal = new Dynamic.DataAccess.Security.BranchDB(hostName, dbName).getAllBranch(UserId);

                return Json(retVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "BranchId","Name","Address","Code","ContactPerson","ContactNo","EmailId","IsSuccess","ResponseMSG"
                                 }
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


        // GET v1/GetCommonNarration
        /// <summary>
        /// Get Common Narration For Transaction Entry
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetCommonNarration([FromBody]JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                int voucherType = 0;
                if (para.ContainsKey("voucherType"))
                    voucherType = Convert.ToInt32(para["voucherType"]);

                List<string> dataColl = new Dynamic.DataAccess.Account.NarrationMasterDB(hostName, dbName).getNarrationMasterAsList(UserId,(Dynamic.BusinessEntity.Account.VoucherTypes)voucherType);

                var retVal = new
                {
                    DataColl = dataColl,
                    IsSuccess = true,
                    ResponseMSG = "Success"
                };

                return Json(retVal, new JsonSerializerSettings
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

        // GET v1/GetOneSignalSetup
        /// <summary>
        /// Get OneSignal App Id and API Key
        /// </summary>        
        /// <returns></returns>
        [HttpPost]        
        public IHttpActionResult GetOneSignalSetup()
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                string apiId = "", apiKey = "";
                System.Configuration.Configuration configFile = null;

                System.Configuration.ExeConfigurationFileMap map = new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = System.AppDomain.CurrentDomain.BaseDirectory + "/Web.Config" };
                configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                var settings = configFile.AppSettings.Settings;

                if (settings.AllKeys.Contains("oneSignal_App_Id"))
                    apiId = settings["oneSignal_App_Id"].Value;

                if (settings.AllKeys.Contains("oneSignal_Api_Key"))
                    apiKey = settings["oneSignal_Api_Key"].Value;


                var retVal = new
                {
                    ApiId = apiId,
                    ApiKey = apiKey,
                    IsSuccess=true,
                    ResponseMSG="Success"
                };
                return Json(retVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        //IsInclude = true,
                        //IncludeProperties = new List<string>
                        //         {
                        //            "ApiId","ApiKey","IsSuccess","ResponseMSG"
                        //         }
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

        // GET v1/DelTransaction
        /// <summary>
        /// Delete Account and Inventory Transaction
        /// voucherType/voucherId/tranId  as Int
        /// </summary>        
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DelTransaction([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                Dynamic.BusinessEntity.Account.VoucherTypes voucherType=Dynamic.BusinessEntity.Account.VoucherTypes.IG;
                if (para.ContainsKey("voucherType"))
                    voucherType =(Dynamic.BusinessEntity.Account.VoucherTypes)Convert.ToInt32(para["voucherType"]);
                else
                {
                    return BadRequest("No voucher type found");
                }

                int voucherId = 0;
                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);
                else
                {
                    return BadRequest("No voucher id found");
                }

                int tranId = 0;
                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);
                else
                {
                    return BadRequest("No transaction found");
                }

                Dynamic.BusinessEntity.Global.FormsEntity entity = GetEntityFromVoucherType(voucherType);
                Dynamic.BusinessEntity.Global.VoucherDefaultValues voucherDefault= new Dynamic.DataAccess.Global.GlobalDB(hostName,dbName).IsValidVoucher(UserId, Convert.ToInt32(entity),voucherId,null, DateTime.Today, 3, false);
                if(voucherDefault.IsSuccess)
                {
                    switch (voucherType)
                    {
                        case Dynamic.BusinessEntity.Account.VoucherTypes.Contra:
                            resVal = new Dynamic.DataAccess.Account.Transaction.JournalDB(hostName,dbName,Dynamic.BusinessEntity.Account.Transaction.TranTypes.Contra).Delete(tranId); break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.DeliveryNote:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.DeliveryNoteDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.Journal:
                            resVal = new Dynamic.DataAccess.Account.Transaction.JournalDB(hostName, dbName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PartsDemand:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PartsDemandDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.Payment:
                            resVal = new Dynamic.DataAccess.Account.Transaction.JournalDB(hostName, dbName,Dynamic.BusinessEntity.Account.Transaction.TranTypes.Payment).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PhysicalStock:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PhysicalStockDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseAdditionalInvoice:
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PurchaseInvoiceDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseOrder:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PurchaseOrderDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseQuotation:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PurchaseQuotationDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.PurchaseReturnDB(hostName, dbName).Delete(tranId);
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.Receipt:                            
                            resVal = new Dynamic.DataAccess.Account.Transaction.JournalDB(hostName, dbName,Dynamic.BusinessEntity.Account.Transaction.TranTypes.Receipt).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNote:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.ReceiptNoteDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.SalesAdditionalInvoice:                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice:
                             resVal = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.SalesOrder:
                            resVal = new Dynamic.DataAccess.Inventory.Transaction.SalesOrderDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.SalesQuotation:                            
                                resVal = new Dynamic.DataAccess.Inventory.Transaction.SalesQuotationDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.SalesReturn:                            
                                resVal = new Dynamic.DataAccess.Inventory.Transaction.SalesReturnDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.StockJournal:                            
                                resVal = new Dynamic.DataAccess.Inventory.Transaction.StockJournalDB(hostName, dbName).Delete(tranId);                            
                            break;
                        case Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor:
                             resVal = new Dynamic.DataAccess.Inventory.Transaction.StockTransforDB(hostName, dbName).Delete(tranId);                           
                            break;
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
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = voucherDefault.ResponseMSG;
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

        // GET v1/PostTransaction
        /// <summary>
        /// Post Account and Inventory Transaction
        /// voucherType/voucherId/tranId  as Int
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostTransaction([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                Dynamic.BusinessEntity.Account.VoucherTypes voucherType = Dynamic.BusinessEntity.Account.VoucherTypes.IG;
                if (para.ContainsKey("voucherType"))
                    voucherType = (Dynamic.BusinessEntity.Account.VoucherTypes)Convert.ToInt32(para["voucherType"]);
                else
                {
                    return BadRequest("No voucher type found");
                }

                int voucherId = 0;
                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);
                else
                {
                    return BadRequest("No voucher id found");
                }

                int tranId = 0;
                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);
                else
                {
                    return BadRequest("No transaction found");
                }

                Dynamic.BusinessEntity.Global.FormsEntity entity = GetEntityFromVoucherType(voucherType);
                Dynamic.BusinessEntity.Global.VoucherDefaultValues voucherDefault = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).IsValidVoucher(UserId, Convert.ToInt32(entity), voucherId,null, DateTime.Today,5, false,tranId);
                if (voucherDefault.IsSuccess)
                {
                    Dynamic.ReportEntity.Account.DayBookCollections dayBooksColl = new Dynamic.ReportEntity.Account.DayBookCollections();
                    dayBooksColl.Add(new Dynamic.ReportEntity.Account.DayBook()
                    {
                        VoucherId=voucherId,
                        VoucherType=voucherType,
                        TranId=tranId,
                        NY=voucherDefault.V_NY,
                        NM=voucherDefault.V_NM,
                        ND=voucherDefault.V_ND,
                        CostClassId=voucherDefault.V_CostClassId
                    });
                    resVal= new Dynamic.Reporting.Account.DayBook(hostName, dbName).PostPendingVoucher(dayBooksColl, UserId);
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
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = voucherDefault.ResponseMSG;
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

        // GET v1/CancelTransaction
        /// <summary>
        /// Cancel Account and Inventory Transaction
        /// voucherType/voucherId/tranId  as Int
        /// reason as String
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CancelTransaction([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                Dynamic.BusinessEntity.Account.VoucherTypes voucherType = Dynamic.BusinessEntity.Account.VoucherTypes.IG;
                if (para.ContainsKey("voucherType"))
                    voucherType = (Dynamic.BusinessEntity.Account.VoucherTypes)Convert.ToInt32(para["voucherType"]);
                else
                {
                    return BadRequest("No voucher type found");
                }

                int voucherId = 0;
                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);
                else
                {
                    return BadRequest("No voucher id found");
                }

                int tranId = 0;
                if (para.ContainsKey("tranId"))
                    tranId = Convert.ToInt32(para["tranId"]);
                else
                {
                    return BadRequest("No transaction found");
                }

                string reason = "";
                if (para.ContainsKey("reason"))
                    reason = Convert.ToString(para["reason"]);
                else
                {
                    return BadRequest("No Reason found for cancel.");
                }

                Dynamic.BusinessEntity.Global.FormsEntity entity = GetEntityFromVoucherType(voucherType);
                Dynamic.BusinessEntity.Global.VoucherDefaultValues voucherDefault = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).IsValidVoucher(UserId, Convert.ToInt32(entity), voucherId,null, DateTime.Today, 5, false, tranId);
                if (voucherDefault.IsSuccess)
                {
                    int daysDiff =Math.Abs((voucherDefault.V_VoucherDate - voucherDefault.CurrentDateTime).Days);
                    if (daysDiff <= voucherDefault.CancelVoucherWithIn)
                    {
                        Dynamic.ReportEntity.Account.DayBookCollections dayBooksColl = new Dynamic.ReportEntity.Account.DayBookCollections();
                        dayBooksColl.Add(new Dynamic.ReportEntity.Account.DayBook()
                        {
                            VoucherId = voucherId,
                            VoucherType = voucherType,
                            TranId = tranId,
                            NY = voucherDefault.V_NY,
                            NM = voucherDefault.V_NM,
                            ND = voucherDefault.V_ND,
                            CostClassId = voucherDefault.V_CostClassId
                        });
                        resVal = new Dynamic.Reporting.Account.DayBook(hostName, dbName).CancelVoucher(UserId,dayBooksColl, reason);
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Can't Cancel Selected Voucher ("+voucherDefault.V_VoucherNo+"). days exced.";
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
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = voucherDefault.ResponseMSG;
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

        // GET v1/GetVoucherNo
        /// <summary>
        /// AutoVoucherNo Account and Inventory Transaction
        /// voucherId/costClassId  as Int
        /// voucherDate as Date
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Account.Transaction.VoucherNo))]
        public IHttpActionResult GetVoucherNo([FromBody] JObject para)
        {
            Dynamic.BusinessEntity.Account.Transaction.VoucherNo resVal = new Dynamic.BusinessEntity.Account.Transaction.VoucherNo();

            try
            {              
                int voucherId = 0;
                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);
                else
                {
                    return BadRequest("No voucher id found");
                }

                int costClassId = 0;
                if (para.ContainsKey("costClassId"))
                    costClassId = Convert.ToInt32(para["costClassId"]);


                DateTime voucherDate = DateTime.Today;
                if (para.ContainsKey("voucherDate"))
                    voucherDate = Convert.ToDateTime(para["voucherDate"]);

                resVal = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getVoucherNo(UserId, voucherId, costClassId, voucherDate);
                
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
                        "AutoVoucherNo","AutoManualNo","IsSuccess","ResponseMSG"
                    }
                }
            });
        }


        // POST v1/PrintVoucher
        /// <summary>
        /// AutoVoucherNo Account and Inventory Transaction
        /// voucherId,voucherType  as Int
        /// voucherDate as Date
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PrintVoucher([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                int voucherType = 0;
                if (para.ContainsKey("voucherType"))
                    voucherType = Convert.ToInt32(para["voucherType"]);
                
                if(voucherType==0)
                {
                   return BadRequest("No voucher type found");                   
                }

                int voucherId = 0;
                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);

                if (voucherId == 0)
                {
                    return BadRequest("No voucher found");                    
                }

                int tranid = 0;
                if (para.ContainsKey("tranid"))
                    tranid = Convert.ToInt32(para["tranid"]);

                if (tranid == 0)
                {
                    return BadRequest("pls select valid transaction for print");                    
                }

                int entityId=(int)GetEntityFromVoucherType((Dynamic.BusinessEntity.Account.VoucherTypes)voucherType);
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, voucherType, tranid);
                
                if (comDet.IsSuccess)
                {
                    PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, voucherId, true);                    
                    if(reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        return BadRequest("No Report Templates Found");                        
                    }

                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;                    
                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                    paraColl.Add("UserId", UserId.ToString());
                    paraColl.Add("TranId", tranid.ToString());
                    paraColl.Add("UserName", User.Identity.Name);
                    Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                    _rdlReport.ComDet = comDet;
                    _rdlReport.ConnectionString = ConnectionString;
                    _rdlReport.RenderType = "pdf";
                    _rdlReport.NoShow = false;

                    if (!string.IsNullOrEmpty(template.Path))
                    {
                                              
                        _rdlReport.ReportFile =reportTemplate.GetPath(template);

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
                            printLog.TranId = tranid;
                            printLog.AutoManualNo = tranid.ToString();
                            printLog.SystemUser = "API";
                            printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                            printLog.EntityId = entityId;
                            printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                            printLog.LogDate = DateTime.Now;
                            printLog.LogText = "Print Voucher of tranid="+tranid.ToString();
                            printLog.MacAddress = "";
                            printLog.PCName = "";
                           var printRes= new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);
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

                      

                        //result = Request.CreateResponse(HttpStatusCode.OK);
                        //result.Content = new ByteArrayContent(_rdlReport.Object);
                        //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                        //result.Content.Headers.ContentDisposition.FileName = tranid.ToString()+".pdf";
                        
                        
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

        private Dynamic.BusinessEntity.Global.FormsEntity GetEntityFromVoucherType(Dynamic.BusinessEntity.Account.VoucherTypes voucher)
        {
            switch (voucher)
            {
                case Dynamic.BusinessEntity.Account.VoucherTypes.Receipt:
                    return Dynamic.BusinessEntity.Global.FormsEntity.Receipt;
                case Dynamic.BusinessEntity.Account.VoucherTypes.Payment:
                    return Dynamic.BusinessEntity.Global.FormsEntity.Payment;
                case Dynamic.BusinessEntity.Account.VoucherTypes.Journal:
                    return Dynamic.BusinessEntity.Global.FormsEntity.Journal;
                case Dynamic.BusinessEntity.Account.VoucherTypes.Contra:
                    return Dynamic.BusinessEntity.Global.FormsEntity.Contra;
                case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseQuotation:
                    return Dynamic.BusinessEntity.Global.FormsEntity.PurchaseQuotation;
                case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseOrder:                
                    return Dynamic.BusinessEntity.Global.FormsEntity.PurchaseOrder;
                case Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNote:
                    return Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNote;
                case Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNoteReturn:
                    return Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNoteReturn;
                case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice:
                    return Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice;
                case Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn:
                    return Dynamic.BusinessEntity.Global.FormsEntity.PurchaseReturn;
                case Dynamic.BusinessEntity.Account.VoucherTypes.SalesQuotation:
                    return Dynamic.BusinessEntity.Global.FormsEntity.SalesQuotation;
                case Dynamic.BusinessEntity.Account.VoucherTypes.SalesOrder:
                    return Dynamic.BusinessEntity.Global.FormsEntity.SalesOrder;
                case Dynamic.BusinessEntity.Account.VoucherTypes.DeliveryNote:
                    return Dynamic.BusinessEntity.Global.FormsEntity.DeliveryNote;
                case Dynamic.BusinessEntity.Account.VoucherTypes.DispatchOrder:
                    return Dynamic.BusinessEntity.Global.FormsEntity.DispatchOrder;
                case Dynamic.BusinessEntity.Account.VoucherTypes.DispatchSection:
                    return Dynamic.BusinessEntity.Global.FormsEntity.DispatchSection;
                case Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice:
                    return Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                case Dynamic.BusinessEntity.Account.VoucherTypes.SalesReturn:
                    return Dynamic.BusinessEntity.Global.FormsEntity.SalesReturn;
                case Dynamic.BusinessEntity.Account.VoucherTypes.PartsDemand:
                    return Dynamic.BusinessEntity.Global.FormsEntity.PartsDemand;
                case Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor:
                    return Dynamic.BusinessEntity.Global.FormsEntity.StockTransfor;
                case Dynamic.BusinessEntity.Account.VoucherTypes.StockJournal:
                    return Dynamic.BusinessEntity.Global.FormsEntity.StockJournal;
                case Dynamic.BusinessEntity.Account.VoucherTypes.SalesAllotment:
                    return Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotment;

                
            }
            return Dynamic.BusinessEntity.Global.FormsEntity.CompanyAlternation;
        }



        // GET v1/SendSMS
        /// <summary>
        /// Send SMS        
        /// </summary>        
        /// <returns></returns>
        //For HH [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendSMS([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para.ContainsKey("message"))
                    message = Convert.ToString(para["message"]);
                else
                {
                    return BadRequest("No message for SMS");
                }

                string number = string.Empty;
                if (para.ContainsKey("number"))
                    number = Convert.ToString(para["number"]);
                else
                {
                    return BadRequest("No mobile Number for SMS");
                }
                  
                bool storeLog = true;                
                if (para.ContainsKey("storelog"))
                    storeLog = Convert.ToBoolean(para["storelog"]);            

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                var global = new PivotalERP.Global.GlobalFunction(_userId, hostName, dbName, GetBaseUrl);
                //foreach(var num in number.Split(',')) 
                //{
                //    if(!string.IsNullOrEmpty(num))
                //        resVal = global.SendSMS(num, message,storeLog);
                //}                    
                char[] splitChar = { ',' };
                foreach (var num in number.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(num))
                        resVal = global.SendSMS(num, message, storeLog);
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


        // GET v1/SendSMS
        /// <summary>
        /// Send SMS        
        /// </summary>        
        /// <returns></returns>
        //For HH[AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendSMSWithToken([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para.ContainsKey("message"))
                    message = Convert.ToString(para["message"]);
                else
                {
                    return BadRequest("No message for SMS");
                }

                string number = string.Empty;
                if (para.ContainsKey("number"))
                    number = Convert.ToString(para["number"]);
                else
                {
                    return BadRequest("No mobile Number for SMS");
                }

                bool storeLog = true;
                if (para.ContainsKey("storelog"))
                    storeLog = Convert.ToBoolean(para["storelog"]);

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                var global = new PivotalERP.Global.GlobalFunction(_userId, hostName, dbName, GetBaseUrl);
                //foreach(var num in number.Split(',')) 
                //{
                //    if(!string.IsNullOrEmpty(num))
                //        resVal = global.SendSMS(num, message,storeLog);
                //}                    
                char[] splitChar = { ',' };
                foreach (var num in number.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(num))
                        resVal =await global.SendSMSWithToken(num, message, storeLog);
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

        // GET v1/SendSMSFN
        /// <summary>
        /// Send SMS Throw Notification        
        /// </summary>        
        /// <returns></returns>        
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendSMSFN([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para.ContainsKey("message"))
                    message = Convert.ToString(para["message"]);
                else
                {
                    return BadRequest("No message for SMS");
                }

                int toUserId = 1;
                if (para.ContainsKey("toUserId"))
                    toUserId = Convert.ToInt32(para["toUserId"]);
                else
                {
                    return BadRequest("No mobile Number for SMS");
                }

                Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                notification.Content = message;
                notification.ContentPath = message;
                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.SEND_SMS);
                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.SEND_SMS.ToString();
                notification.Heading = "Send SMS";
                notification.Subject = "Send SMS";
                notification.UserId = UserId;
                notification.UserName = User.Identity.Name;
                notification.UserIdColl = toUserId.ToString();
                resVal = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).SendNotification(UserId, notification);
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

        // GET v1/SendBIONotification
        /// <summary>
        /// Send BIO Attendance Notification        
        /// </summary>        
        /// <returns></returns>
       //For HH [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendBIONotification(JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Notification");
                }
                string forUserId = "";
                string msg = "";
                string passKey = "";
                if (para.ContainsKey("forUserId"))
                    forUserId = Convert.ToString(para["forUserId"]);

                if (para.ContainsKey("msg"))
                    msg = Convert.ToString(para["msg"]);

                if (para.ContainsKey("passKey"))
                    passKey = Convert.ToString(para["passKey"]);

                if(string.IsNullOrEmpty(passKey) || string.IsNullOrEmpty(forUserId) || string.IsNullOrEmpty(msg))
                    return BadRequest("No Data Found For Notification");

                if(passKey=="20211031@Nepal#321@BIO")
                {
                   

                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                    notification.Content = msg;
                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIO_DAILY_ATTENDANCE);
                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIO_DAILY_ATTENDANCE.ToString();
                    notification.Heading = "Attendance – Present";
                    notification.Subject = "Attendance – Present";
                    notification.UserId = 1;
                    notification.UserName = "Admin";
                    notification.UserIdColl = forUserId;

                    int _userId = 1;
                    if (User.Identity.IsAuthenticated)
                        _userId = UserId;

                    resVal = new PivotalERP.Global.GlobalFunction(_userId,hostName, dbName, GetBaseUrl).SendNotification(_userId, notification,true);
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Pass Key";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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

        // GET v1/SendBIOEmail
        /// <summary>
        /// Send BIA Attendance Email        
        /// </summary>        
        /// <returns></returns>
      //For HH  [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendBIOEmail(JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Notification");
                }
                string mailTo = "";
                string msg = "";
                string passKey = "";
                if (para.ContainsKey("mailTo"))
                    mailTo = Convert.ToString(para["mailTo"]);

                if (para.ContainsKey("msg"))
                    msg = Convert.ToString(para["msg"]);

                if (para.ContainsKey("passKey"))
                    passKey = Convert.ToString(para["passKey"]);

                if (string.IsNullOrEmpty(passKey) || string.IsNullOrEmpty(mailTo) || string.IsNullOrEmpty(msg))
                    return BadRequest("No Data Found For Notification");

                if (passKey == "20211031@Nepal#321@BIO")
                {
                    Dynamic.BusinessEntity.Global.MailDetails mailDet = new Dynamic.BusinessEntity.Global.MailDetails();
                    mailDet.To = mailTo;
                    mailDet.Subject = "Attendance-Present";
                    mailDet.CUserId = 1;
                    mailDet.Message = msg;
                    mailDet.EntityId= Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIO_DAILY_ATTENDANCE);

                    int _userId = 1;
                    if (User.Identity.IsAuthenticated)
                        _userId = UserId;

                    var global = new PivotalERP.Global.GlobalFunction(_userId, hostName, dbName, GetBaseUrl);
                    resVal = global.SendEMail(mailDet);
                    
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Pass Key";
                }

                
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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

        // GET v1/SendBIOSMS
        /// <summary>
        /// Send BIA Attendance SMS        
        /// </summary>        
        /// <returns></returns>
        //For HH[AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendBIOSMS(JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Notification");
                }
                string smsTo = "";
                string msg = "";
                string passKey = "";
                int studentId = 0;

                if (para.ContainsKey("studentId"))
                    studentId = Convert.ToInt32(para["studentId"]);

                if (para.ContainsKey("smsTo"))
                    smsTo = Convert.ToString(para["smsTo"]);

                if (para.ContainsKey("msg"))
                    msg = Convert.ToString(para["msg"]);

                if (para.ContainsKey("passKey"))
                    passKey = Convert.ToString(para["passKey"]);

                if (string.IsNullOrEmpty(passKey) || string.IsNullOrEmpty(smsTo) || string.IsNullOrEmpty(msg))
                    return BadRequest("No Data Found For SMS");

                if (passKey == "20211031@Nepal#321@BIO")
                {
                    AcademicERP.Global.SMSFunction smsFN = new AcademicERP.Global.SMSFunction();
                    SMSServer smsUser = smsFN.GetSMSUser();
                    resVal = smsFN.IsValidSMSUser(ref smsUser);
                    if (resVal.IsSuccess)
                    {
                        SMSLog sms = new SMSLog();
                        sms.Message = msg;
                        sms.StudentId = studentId.ToString();
                        sms.PhoneNo = smsTo;
                        if (sms.PhoneNo.Length >= 10)
                        {
                            var sRes = smsFN.SendSMS(sms, smsUser);
                            if (!sRes.IsSuccess)
                            {
                                if (sRes.ResponseMSG.Contains("Credit Balance"))
                                {
                                    resVal.ResponseMSG = sRes.ResponseMSG;
                                    resVal.IsSuccess = false;
                                }
                                else
                                {
                                    resVal.IsSuccess = true;
                                    resVal.ResponseMSG = "SMS Send Success";
                                }
                            }
                        }
                        smsFN.closeConnection();                        
                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Pass Key";
                }


            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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



        // GET v1/SendNotification
        /// <summary>
        /// Send Notification        
        /// </summary>        
        /// <returns></returns>
        //For HH [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SendNotification([FromBody] Dynamic.BusinessEntity.Global.NotificationLog para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Notification");
                }
                await Task.Run(() =>
                {
                    int userId = 1;
                    var global = new PivotalERP.Global.GlobalFunction(userId, hostName, dbName, GetBaseUrl);
                    resVal = new PivotalERP.Global.GlobalFunction(userId, hostName, dbName, GetBaseUrl).SendNotification(userId,para);
                });
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Notification On Que";

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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
        //public IHttpActionResult SendNotification([FromBody]Dynamic.BusinessEntity.Global.NotificationLog para)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {                
        //        string message = string.Empty;
        //        if (para==null)                    
        //        {
        //            return BadRequest("No Data Found For Notification");
        //        }
        //        int userId = 1;// (User != null && User.Identity!=null ? UserId : 1);
        //        var global = new PivotalERP.Global.GlobalFunction(userId, hostName, dbName, GetBaseUrl);
        //        resVal = new PivotalERP.Global.GlobalFunction(userId, hostName, dbName, GetBaseUrl).SendNotification(userId,para);
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message+ee.StackTrace;
        //    }
        //    return Json(resVal, new JsonSerializerSettings
        //    {
        //        ContractResolver = new JsonContractResolver()
        //        {
        //            IsInclude = true,
        //            IncludeProperties = new List<string>
        //            {
        //                "IsSuccess","ResponseMSG"
        //            }
        //        }
        //    });
        //}

        // GET v1/SendEmail
        /// <summary>
        /// Send Email        
        /// </summary>        
        /// <returns></returns>
       //For HH [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendEmail([FromBody] Dynamic.BusinessEntity.Global.MailDetails para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Email");
                }
                int userId = 1;// (User != null && User.Identity!=null ? UserId : 1);
                var global = new PivotalERP.Global.GlobalFunction(userId, hostName, dbName, GetBaseUrl);
                resVal = global.SendEMail(para);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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


        // GET v1/SendEmail
        /// <summary>
        /// Send Email        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SendEmailToStudent([FromBody] Dynamic.BusinessEntity.Global.MailDetails para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var uid = UserId;
                var srvPath = System.Web.HttpContext.Current.Server.MapPath("~");
                PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(uid, hostName, dbName, GetBaseUrl);
                string tempMSG = para.Message;
                string subject = para.Subject;
                Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                {
                    To = para.To,
                    Cc = IsNullOrEmptyStr(para.Cc),
                    BCC = "",
                    Subject = subject,
                    Message = tempMSG,
                    CUserId = uid
                };


                List<string> attachmentFileColl = new List<string>();

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(uid, 0, 0, 0);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, uid, para.EntityId, 0, true);

                if (reportTemplate.TemplateAttachments != null && reportTemplate.TemplateAttachments.Count > 0)
                {
                    Dynamic.BusinessEntity.Global.ReportTempletes template = null;
                    foreach (var rT in reportTemplate.TemplateAttachments)
                    {
                        if (rT.ForEmail == true)
                        {
                            template = reportTemplate.GetTemplate(rT);
                            break;
                        }
                    }

                    if (template == null)
                        template = reportTemplate.DefaultTemplate;

                    if (template != null && !string.IsNullOrEmpty(template.Path))
                    {
                        System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                        paraColl.Add("UserId", uid.ToString());
                        paraColl.Add("UserName", User.Identity.Name);

                        if (para.ParaColl != null)
                        {
                            foreach (var par in para.ParaColl)
                            {
                                if (paraColl.AllKeys.Contains(par.Key))
                                    paraColl.Remove(par.Key);

                                paraColl.Add(par.Key, par.Value);
                            }
                        }

                        Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                        _rdlReport.ComDet = comDet;
                        _rdlReport.ConnectionString = ConnectionString;
                        _rdlReport.RenderType = "pdf";
                        _rdlReport.NoShow = false;
                        _rdlReport.ReportFile = reportTemplate.GetPath(template, srvPath);
                        if (_rdlReport.Object != null)
                        {
                            string basePath = @"print-tran-log\" + para.FileName + "-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".pdf";
                            string sFile = srvPath + basePath;
                            reportTemplate.SavePDF(_rdlReport.Object, sFile);

                            attachmentFileColl.Add(sFile);
                        }
                    }

                }
                  
                globlFun.SendEMailWithAttachment(mail, null, attachmentFileColl);

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "EMail Send";
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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

        // POST api/GetNotificationLog
        /// <summary>
        ///  Get Notification Log
        ///  DateFrom as Date
        ///  DateTo as Date        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Global.NotificationLogCollections))]
        public IHttpActionResult GetNotificationLog([FromBody] JObject para)
        {
            AcademicLib.RE.Global.NotificationLogCollections dataColl = new AcademicLib.RE.Global.NotificationLogCollections();
            try
            {
                DateTime? dateFrom = null;
                DateTime? dateTo = null;
                bool isGeneral = false;
                int PageNumber = 1;
                int RowsOfPage = 100;
                int TotalRows = 0;
                bool paging = false;
                string For = "";
                if (para != null)
                {
                    if (para.ContainsKey("dateFrom") && para["dateFrom"] != null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    if (para.ContainsKey("dateTo") && para["dateTo"] != null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    if (para.ContainsKey("isGeneral"))
                        isGeneral = Convert.ToBoolean(para["isGeneral"]);

                    if (para.ContainsKey("pageNumber"))
                        PageNumber = Convert.ToInt32(para["pageNumber"]);

                    if (para.ContainsKey("rowsOfPage"))
                        RowsOfPage = Convert.ToInt32(para["rowsOfPage"]);

                    if (para.ContainsKey("paging"))
                        paging = Convert.ToBoolean(para["paging"]);

                    if (para.ContainsKey("for"))
                        For = Convert.ToString(para["for"]);

                }

                if(paging==false)
                {
                    RowsOfPage = 1000;
                }
                dataColl = new AcademicLib.BL.Global(UserId, hostName, dbName).GetNotificationLog(isGeneral, dateFrom, dateTo, ref TotalRows, PageNumber,RowsOfPage,For);

                if (paging == false)
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
                    var returnVal = new
                    {
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG,
                        TotalRows = TotalRows,
                        DataColl = dataColl,
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {

                        }
                    });
                }
              
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        [HttpPost]
        [ResponseType(typeof(AcademicLib.RE.Global.NotificationLogCollections))]
        public IHttpActionResult GetTopNotificationLog()
        {
            AcademicLib.RE.Global.NotificationLogCollections dataColl = new AcademicLib.RE.Global.NotificationLogCollections();
            try
            {
                dataColl = new AcademicLib.BL.Global(UserId, hostName, dbName).GetTopNotificationLog();

                var returnData = new
                {
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG,
                    TotalCount=dataColl.Count,
                    DataColl=dataColl,
                };

                return Json(returnData, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {

                    }
                }); ;

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }
        // POST api/ReadNotificationLog
        /// <summary>
        ///  Read Notification Log
        ///  tranId as int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult ReadNotificationLog([FromBody] JObject para)
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
                    int tranId = 0;
                    if (para.ContainsKey("tranId"))
                        tranId = Convert.ToInt32(para["tranId"]);
                    else if (para.ContainsKey("TranId"))
                        tranId = Convert.ToInt32(para["TranId"]);

                    resVal = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).ReadNotificationLog(UserId, tranId);
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
                    }); ;
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/ReadAllNotificationLog
        /// <summary>
        ///  Read All Notification Log       
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult ReadAllNotificationLog()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).ReadAllNotificationLog(UserId);
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
        // POST api/NotificationCount
        /// <summary>
        ///  Notification Count        
        /// </summary>
        /// <returns></returns>
        [HttpPost]        
        public IHttpActionResult NotificationCount([FromBody] JObject optPara)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string For = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("for"))
                        For = optPara["for"].ToString().Trim();                    
                }

                resVal = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).CountNotificationLog(UserId,For);

                if(resVal.IsSuccess)
                {
                    string[] values = resVal.ResponseId.Split(',');
                    var retVal = new
                    {
                        Total = Convert.ToInt32(values[0]) + Convert.ToInt32(values[1]),
                        Read = Convert.ToInt32(values[0]),
                        UnRead = Convert.ToInt32(values[1]),
                        IsSuccess = resVal.IsSuccess,
                        ResponseMSG = resVal.ResponseMSG

                    };
                    return Json(retVal, new JsonSerializerSettings
                    {
                        //    ContractResolver = new JsonContractResolver()
                        //    {
                        //        IsInclude = true,
                        //        IncludeProperties = new List<string>
                        //                            {
                        //                                "IsSuccess","ResponseMSG"
                        //                            }
                        //    }
                    }); ;

                }else
                {
                    return BadRequest(resVal.ResponseMSG);
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // Post api/SaveReg
        /// <summary>
        ///  Submit Online Registration
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //For HH[AllowAnonymous]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveReg()
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

                    AcademicLib.BE.FrontDesk.Transaction.AddmissionEnquiry para = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.AddmissionEnquiry>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }                   
                    else
                    {
                        para.Sourse = "Online";
                        para.EnquiryId = 0;

                        para.CUserId = 1;
                        para.EnquiryDate = DateTime.Now;
                        para.Address = para.PA_FullAddress;
                        para.IsAnonymous = true;

                        
                        if (provider.FileData.Count > 0)
                        {
                            try
                            {
                                var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                foreach (var file in DocumentColl)
                                {
                                    if (file.ParaName == "photo")
                                    {
                                        para.PhotoPath = file.DocPath;
                                    }
                                }

                                int fInd = 0;
                                foreach (var att in para.AttachmentColl)
                                {
                                    var fileName = "file" + fInd.ToString();
                                    var findDoc = DocumentColl.Find(p1 => p1.ParaName == fileName);
                                    if (findDoc!=null)
                                    {
                                        att.DocPath = findDoc.DocPath;
                                    }
                                    fInd++;

                                }
                            }
                            catch { }
                             
                        }
                        string msg = "";
                        resVal = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(1, hostName, dbName).SaveFormData(this.AcademicYearId, para);
                         
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

        // GET v1/GetAboutCompany
        /// <summary>
        /// Get About Company 
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.AppCMS.About))]
        public IHttpActionResult GetAboutCompany([FromBody]JObject optPara)
        {
            AcademicLib.API.AppCMS.About resVal = new AcademicLib.API.AppCMS.About();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                if (branchCode == "0")
                    branchCode = string.Empty;

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                resVal = new AcademicLib.BL.AppCMS.Creation.AboutUs(_userId,hostName, dbName).getAbout(branchCode,true);

                var hd = new AcademicLib.BL.AppCMS.Creation.HeaderDetails(_userId,hostName,dbName).GetHeaderDetailsById(0);
                resVal.HD_CompanyName = hd.CompanyName;
                resVal.HD_HeaderIsActive = hd.HeaderIsActive;
                resVal.HD_LogoPhoto = hd.LogoPhoto;
                resVal.HD_NameIsActive = hd.NameIsActive;
                resVal.HD_Slogan = hd.Slogan;
                resVal.HD_SloganIsActive = hd.SloganIsActive;

                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
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



        // GET v1/GetAboutCompany
        /// <summary>
        /// Get About Company 
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Contact))]
        public IHttpActionResult GetContactUs([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.Contact resVal = new AcademicLib.BE.AppCMS.Creation.Contact();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                resVal = new AcademicLib.BL.AppCMS.Creation.Contact(_userId, hostName, dbName).GetContactById(0, 0);

                return Json(resVal, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = false,
                    //    ExcludeProperties = new List<string>
                    //             {
                    //                "RId","CUserId","ResponseId"
                    //             }
                    //}
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

        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(List<AcademicLib.BE.AppCMS.Creation.ThemeConfig>))]
        public IHttpActionResult GetAllThemeConfig([FromBody] JObject para)
        {

            AcademicLib.BE.AppCMS.Creation.ThemeConfig beData = new AcademicLib.BE.AppCMS.Creation.ThemeConfig();
            beData = new AcademicLib.BL.AppCMS.Creation.ThemeConfig(1, hostName, dbName).GetAllThemeConfig(0);

            var retVal = new
            {
                ResponseMSG = beData.ResponseMSG,
                IsSuccess = beData.IsSuccess,
                DataColl = beData,
            };

            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {
                    IsInclude = true,
                    IncludeProperties = new List<string>
                                 {
                                    "PrimaryColor","SecondaryColor","ThirdColor","FourthColor","FifthColor","ResponseMSG","IsSuccess","DataColl","ThemeConfig"
                                 }
                }
            });
        }

 
        // GET v1/GetAboutCompany
        /// <summary>
        /// Get About Company 
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.SocialMedia))]
        public IHttpActionResult GetSocialMedia([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.SocialMediaCollections dataColl = new AcademicLib.BE.AppCMS.Creation.SocialMediaCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.SocialMedia(_userId, hostName, dbName).GetAllSocialMedia(0);

                var retVal = new
                {
                    IsSuccess= dataColl.IsSuccess,
                    ResponseMSG= dataColl.ResponseMSG,
                    DataColl=dataColl
                };
                return Json(retVal, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonContractResolver()
                    //{
                    //    IsInclude = false,
                    //    ExcludeProperties = new List<string>
                    //             {
                    //                "RId","CUserId","ResponseId"
                    //             }
                    //}
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        #region "App CMS"

        // GET v1/GetAppCMSFeatures
        /// <summary>
        /// Get Web CMS Entity Active And DeActive List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.AppCMS.About))]
        public IHttpActionResult GetAppCMSFeatures([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections dataCOll = new AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataCOll = new AcademicLib.BL.AppCMS.Creation.AppCMSEntity(_userId, hostName, dbName).GetEntity(branchCode);

            }
            catch (Exception ee)
            {
                dataCOll.IsSuccess = false;
                dataCOll.ResponseMSG = ee.Message;
            }


            var retVal = new
            {
                IsSuccess = dataCOll.IsSuccess,
                ResponseMSG = dataCOll.ResponseMSG,
                DataColl = dataCOll,
            };
            return Json(retVal, new JsonSerializerSettings
            {
            });
        }

        // POST v1/FeedbackSuggestion
        /// <summary>
        /// POST FeedbackSuggestion
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult FeedbackSuggestion([FromBody]AcademicLib.API.AppCMS.FeedbackSuggestion feedback)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (feedback == null)
                {
                    resVal.ResponseMSG = "Invalid Data";
                }
                else if (string.IsNullOrEmpty(feedback.PassKey))
                {
                    resVal.ResponseMSG = "Invalid PassKey";
                }
                else if(feedback.PassKey!= "new@2021#FeeDBacK@2021")
                {
                    resVal.ResponseMSG = "Invalid PassKey";
                }
                else
                {
                    feedback.IPAddress = GetClientIp();
                                           
                    int _userId = 1;
                    if (User.Identity.IsAuthenticated)
                        _userId = UserId;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AboutUs(_userId, hostName, dbName).SaveFeedback(feedback);
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

         
        // POST v1/SaveAttendanceLog
        /// <summary>
        /// Save Attendance Log
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveAttLog([FromBody] AcademicLib.BE.Attendance.DeviceLog log)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (log == null)
                {
                    return BadRequest("No Data Found For Attendance Log");
                }
                try
                {
                    if (log == null)
                    {
                        resVal.ResponseMSG = "Invalid Data";
                    }
                    else if (string.IsNullOrEmpty(log.PassKey))
                    {
                        resVal.ResponseMSG = "Invalid PassKey";
                    }
                    else if (log.PassKey != "new@2021#FeeDBacK@2021")
                    {
                        resVal.ResponseMSG = "Invalid PassKey";
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            int _userId = 1;
                            if (User.Identity.IsAuthenticated)
                                _userId = UserId;

                            resVal = new AcademicLib.BL.Attendance.Device(_userId, hostName, dbName).SaveLog(log);
                        });
                    }

                }
                catch (Exception ee)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = ee.Message;
                }

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Attendance Log On Que";

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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
        // GET v1/GetBannerList
        /// <summary>
        /// Get GetBannerList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Banner))]
        public IHttpActionResult GetBannerList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.BannerCollections dataColl = new AcademicLib.BE.AppCMS.Creation.BannerCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Banner(_userId, hostName, dbName).getAllBannerForApp(branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "Title","ImagePath","DisplayEachTime", "IsSuccess","ResponseMSG","Description","OrderNo","BannerId","Weblink"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetMeritAchievers
        /// <summary>
        /// Get MeritAchievers
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Banner))]
        public IHttpActionResult GetMeritAchievers([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.MeritAchieversCollections dataColl = new AcademicLib.BE.AppCMS.Creation.MeritAchieversCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.MeritAchievers(_userId, hostName, dbName).GetAllMeritAchievers(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "TranId","Name","DegreeDetails","OrderNo","Description","ImagePath", "IsSuccess","ResponseMSG","MeritAchievementsColl","Achievement",
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetProudAlumni
        /// <summary>
        /// Get ProudAlumni
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Banner))]
        public IHttpActionResult GetProudAlumni([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.ProudAlumniCollections dataColl = new AcademicLib.BE.AppCMS.ProudAlumniCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.ProudAlumni(_userId, hostName, dbName).GetAllProudAlumni(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "TranId","BranchId","Name","DegreeDetails","CurrentCompany","Position","OrderNo","Description","ImagePath", "IsSuccess","ResponseMSG",
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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



        // GET v1/GetNoticeList
        /// <summary>
        /// Get NoticeList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Notice))]
        public IHttpActionResult GetNoticeList([FromBody] JObject para)
        {
            AcademicLib.BE.AppCMS.Creation.NoticeCollections dataColl = new AcademicLib.BE.AppCMS.Creation.NoticeCollections();
            try
            {
                int PageNumber = 1;
                int RowsOfPage = 100;
                int TotalRows = 0;
                bool paging = false;

                string noticeFor = "";

                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (para != null)
                {
                    if (para.ContainsKey("branchCode"))
                        branchCode = para["branchCode"].ToString().Trim();
                    else if (para.ContainsKey("BranchCode"))
                        branchCode = para["BranchCode"].ToString().Trim();
                    else if (para.ContainsKey("branchcode"))
                        branchCode = para["branchcode"].ToString().Trim();

                    if (para.ContainsKey("pageNumber"))
                        PageNumber = Convert.ToInt32(para["pageNumber"]);

                    if (para.ContainsKey("rowsOfPage"))
                        RowsOfPage = Convert.ToInt32(para["rowsOfPage"]);

                    if (para.ContainsKey("paging"))
                        paging = Convert.ToBoolean(para["paging"]);

                    if (para.ContainsKey("noticeFor"))
                        noticeFor = Convert.ToString(para["noticeFor"]);

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Notice(_userId, hostName, dbName).GetAllNotice(0,branchCode,ref TotalRows,PageNumber,RowsOfPage,noticeFor);

                if (paging == false)
                {
                    return Json(dataColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = false,
                            ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId","SelectOptions","JsonStr","ColWidth","Source","Formula","FieldAfter","DropDownList","VId","ErrorNumber","VBId"
                            }                            
                        }
                    });
                }
                else
                {
                    var returnVal = new
                    {
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG,
                        TotalRows = TotalRows,
                        DataColl = dataColl,
                    };
                    return Json(returnVal, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                            IsInclude = false,
                            ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId","SelectOptions","JsonStr","ColWidth","Source","Formula","FieldAfter","DropDownList","VId","ErrorNumber","VBId"
                                 }
                        }
                    });
                }

                 
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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



        // GET v1/GetHeaderDetails
        /// <summary>
        /// Get HeaderDetails
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Notice))]
        public IHttpActionResult GetHeaderDetails([FromBody] JObject para)
        {
            AcademicLib.BE.AppCMS.Creation.HeaderDetails beData = new AcademicLib.BE.AppCMS.Creation.HeaderDetails();
            try
            {
                
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (para != null)
                {
                    if (para.ContainsKey("branchCode"))
                        branchCode = para["branchCode"].ToString().Trim();
                    else if (para.ContainsKey("BranchCode"))
                        branchCode = para["BranchCode"].ToString().Trim();
                    else if (para.ContainsKey("branchcode"))
                        branchCode = para["branchcode"].ToString().Trim();
                     

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                beData = new AcademicLib.BL.AppCMS.Creation.HeaderDetails(_userId,hostName,dbName).GetHeaderDetailsById(0);
                var returnVal = new
                {
                    IsSuccess = beData.IsSuccess,
                    ResponseMSG = beData.ResponseMSG,
                    TotalRows = 1,
                    DataColl = beData,
                };
                return Json(returnVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });


            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return Json(beData, new JsonSerializerSettings
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

        // POST api/ReadNotice
        /// <summary>
        ///  Read Notice
        ///  tranId as int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult ReadNotice([FromBody] JObject para)
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
                    int _userId = UserId;

                    int tranId = 0;
                    if (para.ContainsKey("tranId"))
                        tranId = Convert.ToInt32(para["tranId"]);
                    else if (para.ContainsKey("TranId"))
                        tranId = Convert.ToInt32(para["TranId"]);
                    if (para.ContainsKey("noticeId"))
                        tranId = Convert.ToInt32(para["noticeId"]);
                    else if (para.ContainsKey("NoticeId"))
                        tranId = Convert.ToInt32(para["NoticeId"]);

                    resVal = new AcademicLib.BL.AppCMS.Creation.Notice(_userId, hostName, dbName).ReadNotice( tranId);
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
                    }); ;
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/ReadAllNotice
        /// <summary>
        ///  Read All Notice
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult ReadAllNotice()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                int _userId = UserId;
                resVal = new AcademicLib.BL.AppCMS.Creation.Notice(_userId, hostName, dbName).ReadAllNotice();
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
        // POST api/NoticeCount
        /// <summary>
        ///  Notice Count        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult NoticeCount([FromBody] JObject optPara)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string noticeFor = "";
                if (optPara != null)
                {
                    if (optPara.ContainsKey("noticeFor"))
                        noticeFor = optPara["noticeFor"].ToString().Trim();                    
                }

                int _userId = UserId;
                resVal = new AcademicLib.BL.AppCMS.Creation.Notice(_userId, hostName, dbName).CountNotice(noticeFor);

                if (resVal.IsSuccess)
                {
                    string[] values = resVal.ResponseId.Split(',');
                    var retVal = new
                    {
                        Total = Convert.ToInt32(values[0]) + Convert.ToInt32(values[1]),
                        Read = Convert.ToInt32(values[0]),
                        UnRead = Convert.ToInt32(values[1]),
                        IsSuccess = resVal.IsSuccess,
                        ResponseMSG = resVal.ResponseMSG

                    };
                    return Json(retVal, new JsonSerializerSettings
                    {
                        //    ContractResolver = new JsonContractResolver()
                        //    {
                        //        IsInclude = true,
                        //        IncludeProperties = new List<string>
                        //                            {
                        //                                "IsSuccess","ResponseMSG"
                        //                            }
                        //    }
                    }); ;

                }
                else
                {
                    return BadRequest(resVal.ResponseMSG);
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }



        // GET v1/GetGalleryList
        /// <summary>
        /// Get GalleryList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Gallery))]
        public IHttpActionResult GetGalleryList([FromBody]JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.GalleryCollections dataColl = new AcademicLib.BE.AppCMS.Creation.GalleryCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Gallery(_userId, hostName, dbName).GetAllGallery(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetVideosList
        /// <summary>
        /// Get VideosList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Videos))]
        public IHttpActionResult GetVideosList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.VideosCollections dataColl = new AcademicLib.BE.AppCMS.Creation.VideosCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Videos(_userId, hostName, dbName).GetAllVideos(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetSliderList
        /// <summary>
        /// Get SliderList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Slider))]
        public IHttpActionResult GetSliderList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.SliderCollections dataColl = new AcademicLib.BE.AppCMS.Creation.SliderCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Slider(_userId, hostName, dbName).GetAllSlider(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetServicesAndFacilitiesList
        /// <summary>
        /// Get ServicesAndFacilitiesList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.ServicesAndFacilities))]
        public IHttpActionResult GetServicesAndFacilitiesList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.ServicesAndFacilitiesCollections dataColl = new AcademicLib.BE.AppCMS.Creation.ServicesAndFacilitiesCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.ServicesAndFacilities(_userId, hostName, dbName).GetAllServicesAndFacilities(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetAcademicProgramList
        /// <summary>
        /// Get AcademicProgramList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.AcademicProgram))]
        public IHttpActionResult GetAcademicProgramList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.AcademicProgramCollections dataColl = new AcademicLib.BE.AppCMS.Creation.AcademicProgramCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicProgram(_userId, hostName, dbName).GetAllAcademicProgram(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetExecutiveMemberList
        /// <summary>
        /// Get ExecutiveMemberList
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.ExecutiveMembersCollections))]
        public IHttpActionResult GetExecutiveMemberList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.ExecutiveMembersCollections dataColl = new AcademicLib.BE.AppCMS.Creation.ExecutiveMembersCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.ExecutiveMembers(_userId, hostName, dbName).GetAllExecutiveMembers(0,branchCode);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetIntroduction
        /// <summary>
        /// Get Introduction
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.AppCMS.Introduction))]
        public IHttpActionResult GetIntroduction([FromBody] JObject optPara)
        {
            AcademicLib.API.AppCMS.Introduction dataColl = new AcademicLib.API.AppCMS.Introduction();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.VisionStatement(_userId, hostName, dbName).getIntroduction(branchCode);

                if(dataColl!=null)
                {                    
                    foreach (var v in dataColl.StaffList)
                    {
                        v.ResponseId = "0";
                    }
                    foreach (var v in dataColl.VisionList)
                    {
                        v.ResponseId = "0";
                    }
                    foreach (var v in dataColl.FounderMSGList)
                    {
                        v.ResponseId = "0";
                    }

                    foreach (var v in dataColl.CommitteList)
                    {
                        v.ResponseId = "0";
                    }
                    var staffColl = from dc in dataColl.StaffList
                                    group dc by dc.Department into g
                                    select new
                                    {
                                        Department=g.Key,
                                        EmpColl=g
                                    };

                    
                    var query = new
                    {
                        ResponseMSG=GLOBALMSG.SUCCESS,
                        IsSuccess=true,
                        VisionList=dataColl.VisionList,
                        FounderMSGList=dataColl.FounderMSGList,
                        StaffList= staffColl,
                        CommitteList = dataColl.CommitteList,
                        TestimonialList = dataColl.TestimonialList
                    };

                    return Json(query, new JsonSerializerSettings
                    {
                        //ContractResolver = new JsonContractResolver()
                        //{
                        //    //IsInclude = true,
                        //    //IncludeProperties = new List<string>
                        //    //     {
                        //    //        "IsSuccess","ResponseMSG","VisionList","FounderMSGList","StaffList","Title","Description","ImagePath","OrderNo","FullName","Designation","ContactNo","Email","Message","Department"
                        //    //     }
                        //}
                    });

                }

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                   "FounderSocialMediaColl","TestimonialList","SocialMediaColl","UrlPath","Name","IconPath","ThumbnailPath", "IsSuccess","ResponseMSG","VisionList","FounderMSGList","StaffList","CommitteList","Title","Description","ImagePath","OrderNo","FullName","Designation","ContactNo","Email","Message","Department"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetTestimonial
        /// <summary>
        /// Get Testimonial
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.AppCMS.Introduction))]
        public IHttpActionResult GetTestimonial([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.TestimonialCollections dataColl = new AcademicLib.BE.AppCMS.Creation.TestimonialCollections();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Testimonial(_userId, hostName, dbName).GetAllTestimonial(0,branchCode);

                var query = new
                {
                    ResponseMSG = GLOBALMSG.SUCCESS,
                    IsSuccess = true,
                    DataColl = dataColl
                };
                return Json(query, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                  
                    }
                });              
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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

        // GET v1/GetWhoWeAre
        /// <summary>
        /// Get WhoWeAre
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.API.AppCMS.WhoWeAre))]
        public IHttpActionResult GetWhoWeAre([FromBody] JObject optPara)
        {
            AcademicLib.API.AppCMS.WhoWeAre dataColl = new AcademicLib.API.AppCMS.WhoWeAre();
            try
            {

                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.AboutUs(_userId, hostName, dbName).getWhoWeAre(branchCode);
                
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "SchoolPhoto","AffiliatedLogo", "GuId","IsSuccess","ResponseMSG","AboutDet","AdmissionProcedureList","RulesRegulationList","ContactDet","LogoPath","ImagePath","BannerPath","Content","Title","Description","ImagePath","OrderNo","Address","MapUrl","ContactNo","EmailId","OpeningHours"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // POST v1/GetAcademicCalendar
        /// <summary>
        /// POST GetAcademicCalendar
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetAcademicCalendar([FromBody] JObject optPara)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                var dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicCalendar(_userId, hostName, dbName).getNepaliCalendar(null,branchCode);
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

        // POST v1/GetEventHoliday
        /// <summary>
        /// POST GetEventHoliday
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetEventHoliday([FromBody]JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                DateTime? fromDate = null;
                DateTime? toDate = null;
                int etype = 0;
                if (para != null)
                {
                    if (para.ContainsKey("fromDate") && para["fromDate"]!=null && para["fromDate"].ToString().ToLower() != "null")
                        fromDate = Convert.ToDateTime(para["fromDate"]);

                    if (para.ContainsKey("toDate") && para["toDate"]!=null && para["toDate"].ToString().ToLower()!="null")
                        toDate = Convert.ToDateTime(para["toDate"]);

                    if (para.ContainsKey("eType") && para["eType"]!=null && para["eType"].ToString().ToLower() != "null")
                        etype = Convert.ToInt32(para["eType"]);
                }

                //[FromBody] JObject optPara
                string branchCode = string.Empty;
                if (para != null)
                {
                    if (para.ContainsKey("branchCode"))
                        branchCode = para["branchCode"].ToString().Trim();
                    else if (para.ContainsKey("BranchCode"))
                        branchCode = para["BranchCode"].ToString().Trim();
                    else if (para.ContainsKey("branchcode"))
                        branchCode = para["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }


                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                var dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicCalendar(_userId, hostName, dbName).getUpcomingEventHoliday(fromDate,toDate,etype,branchCode);
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



        // GET v1/GetNewsCategoryList
        /// <summary>
        /// Get NewsCategory List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.NewsCategory))]
        public IHttpActionResult GetNewsCategoryList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.NewsCategoryCollections dataColl = new AcademicLib.BE.AppCMS.Creation.NewsCategoryCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.NewsCategory(_userId, hostName, dbName).GetAllNewsCategory(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetNewsCategoryList
        /// <summary>
        /// Get NewsSection List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.NewsSection))]
        public IHttpActionResult GetNewsList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.NewsSectionCollections dataColl = new AcademicLib.BE.AppCMS.Creation.NewsSectionCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.NewsSection(_userId, hostName, dbName).GetAllNewsSection(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "PublishedDate","NewsSectionId","Headline","PublishedDateBS","Tags","Category","Description","ResponseMSG","IsSuccess","Photo"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetAchSectionList
        /// <summary>
        /// Get AchievementSection List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.AchievementSection))]
        public IHttpActionResult GetAchievmentList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.AchievementSectionCollections dataColl = new AcademicLib.BE.AppCMS.Creation.AchievementSectionCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.AchievementSection(_userId, hostName, dbName).GetAllAchievementSection(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "IsSuccess","ResponseMSG","AchievementSectionId","Headline","AchievementDateBS","Tags","Category","Photo","Description","AchievementDate"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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




        // GET v1/GetDisclosureList
        /// <summary>
        /// Get tDisclosure List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.MandatoryDisclosure))]
        public IHttpActionResult GetDisclosureList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.MandatoryDisclosureCollections dataColl = new AcademicLib.BE.AppCMS.Creation.MandatoryDisclosureCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.MandatoryDisclosure(_userId, hostName, dbName).GetMandatoryDisclosures(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "IsSuccess","ResponseMSG","TranId","Title","Description"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetDownloadList
        /// <summary>
        /// Get Download List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Download))]
        public IHttpActionResult GetDownloadList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.DownloadCollections dataColl = new AcademicLib.BE.AppCMS.Creation.DownloadCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Download(_userId, hostName, dbName).GetDownloads(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "IsSuccess","ResponseMSG","TranId","Title","Description","AttachmentPath"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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




        // GET v1/GetProgramList
        /// <summary>
        /// Get Program List
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.Program))]
        public IHttpActionResult GetProgramList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.ProgramCollections dataColl = new AcademicLib.BE.AppCMS.Creation.ProgramCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Program(_userId, hostName, dbName).GetAllProgram(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "IsSuccess","ResponseMSG","ProgramId","Name","Description","OrderNo"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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



        // GET v1/GetSyllabusPlanList
        /// <summary>
        /// Get SyllabusPlan List  
        /// </summary>        
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.AppCMS.Creation.SyllabusPlan))]
        public IHttpActionResult GetSyllabusPlanList([FromBody] JObject optPara)
        {
            AcademicLib.BE.AppCMS.Creation.SyllabusPlanCollections dataColl = new AcademicLib.BE.AppCMS.Creation.SyllabusPlanCollections();
            try
            {
                string branchCode = string.Empty;
                if (optPara != null)
                {
                    if (optPara.ContainsKey("branchCode"))
                        branchCode = optPara["branchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("BranchCode"))
                        branchCode = optPara["BranchCode"].ToString().Trim();
                    else if (optPara.ContainsKey("branchcode"))
                        branchCode = optPara["branchcode"].ToString().Trim();

                    if (branchCode.Trim().ToLower() == "null")
                        branchCode = string.Empty;
                }

                int _userId = 1;
                if (User.Identity.IsAuthenticated)
                    _userId = UserId;

                dataColl = new AcademicLib.BL.AppCMS.Creation.Syllabus(_userId, hostName, dbName).getAllSyllabus(0);

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        
                         IsInclude = false,
                        ExcludeProperties = new List<string>
                                 {
                                    "RId","CUserId","ResponseId","EntityId","ErrorNumber","CUserName","ExpireDateTime","DropDownList","FieldAfter","Formula","Source","ColWidth","JsonStr", "SelectOptions"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
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


        // GET v1/GetAppFeatures
        /// <summary>
        /// Get Active App Features
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Setup.AppFeatures))]
        public IHttpActionResult GetAppFeatures()
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                AcademicLib.BE.Setup.AppFeaturesCollections retVal = new AcademicLib.BL.Setup.AppFeatures(UserId, hostName, dbName).getAppFeatures();

                return Json(retVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "SubjectTeacher","ClassTeacher","CoOrdinator","HOD","Role","ForUser","ModuleName","EntityId","Name","IsActive","IsSuccess","ResponseMSG","MOrderNo","EOrderNo"
                                 }
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

        

        // GET v1/QRAttendance
        /// <summary>
        /// Do Attendance throw QR Code      
        /// </summary>        
        /// <returns></returns>        
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult QRAttendance(JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string message = string.Empty;
                if (para == null)
                {
                    return BadRequest("No Data Found For Notification");
                }
                
                string qrCode = "";
                if (para.ContainsKey("qrCode"))
                    qrCode = Convert.ToString(para["qrCode"]);

                if (!string.IsNullOrEmpty(qrCode))
                {
                    var retVal = new AcademicLib.BL.Attendance.Device(UserId, hostName, dbName).QrAttendance(qrCode);
                    resVal = retVal;
                    if (retVal.IsSuccess)
                    {
                        int _userId = UserId;
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                        notification.Content = retVal.ResponseMSG;
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIO_DAILY_ATTENDANCE);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIO_DAILY_ATTENDANCE.ToString();
                        notification.Heading = "QR-Attendance";
                        notification.Subject = "QR-Attendance";
                        notification.UserId = _userId;
                        notification.UserName = "";
                        notification.UserIdColl = retVal.ResponseId;

                        new PivotalERP.Global.GlobalFunction(_userId, hostName, dbName,GetBaseUrl).SendNotification(_userId, notification, true);

                        
                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please Send QrCode";
                }
                

              
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message + ee.StackTrace;
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

        // POST GetAcademicYearList
        /// <summary>
        ///  Get AcademicYearList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.AcademicYearCollections))]
        public IHttpActionResult GetAcademicYearList()
        {
            AcademicLib.BE.Academic.Creation.AcademicYearCollections classSection = new AcademicLib.BE.Academic.Creation.AcademicYearCollections();
            try
            {
                classSection = new AcademicLib.BL.Academic.Creation.AcademicYear(UserId, hostName, dbName).GetAllAcademicYear(0);
                return Json(classSection, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            //"AcademicYearId","Name","IsRunning"
                                            "AcademicYearId","Name","IsRunning","ResponseMSG","IsSuccess" 
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // Put General/PutAttendanceLogOffline
        /// <summary>
        ///  Upload ZK attendance device log to server  
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        [AllowAnonymous]
        public IHttpActionResult PutAttendanceLogOffline([FromBody]AcademicLib.API.Attendance.AttendanceLogCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                if (dataColl == null || dataColl.Count == 0)
                {
                    return BadRequest("No form data found");
                }
                string PassKey = dataColl.First().PassKey;

                if (string.IsNullOrEmpty(PassKey))
                {
                    return BadRequest("No form data found");
                }else if(PassKey!= "aero-12m31-ksd-12167-5632zx")
                {
                    return BadRequest("Invalid Passkey");
                }
                else
                {
                    try
                    {
                        resVal = new AcademicLib.BL.Attendance.Device(1,hostName,dbName).SaveUpdateOfflineAttendanceLog( dataColl);
                    }
                    catch (Exception eee)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = eee.Message;
                    }
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return Json(resVal, new JsonSerializerSettings
            {
            });
        }


        // POST v1/EmailVoucher
        /// <summary>        
        /// voucherId,voucherType,tranId  as Int        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EmailFeeReceipt([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {    
                int tranid = 0;
                if (para.ContainsKey("tranid"))
                    tranid = Convert.ToInt32(para["tranid"]);

                if (tranid == 0)
                {
                    return BadRequest("pls select valid transaction for print");
                }

                int rpttranid = 0;
                if (para.ContainsKey("rpttranid"))
                    rpttranid = Convert.ToInt32(para["rpttranid"]);

                string toemail = "";
                if (para.ContainsKey("toemail"))
                    toemail = Convert.ToString(para["toemail"]);

                string ccemail = "";
                if (para.ContainsKey("ccemail"))
                    ccemail = Convert.ToString(para["ccemail"]);

                string bccemail = "";
                if (para.ContainsKey("bccemail"))
                    bccemail = Convert.ToString(para["bccemail"]);


                string msg = "";
                if (para.ContainsKey("msg"))
                    msg = Convert.ToString(para["msg"]);

                string sub = "";
                if (para.ContainsKey("sub"))
                    sub = Convert.ToString(para["sub"]);

                int entityId = (int)Dynamic.BusinessEntity.Global.FormsEntity.FeeReceipt;
                comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, tranid);

                PivotalERP.Global.ReportTemplate reportTemplate = null;

                if (rpttranid == 0)
                    reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, true);
                else
                    reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, true, rpttranid);

                if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                {
                    return BadRequest("No Report Templates Found");
                }


                Dynamic.BusinessEntity.Global.ReportTempletes template = null;

                foreach(var rT in reportTemplate.TemplateAttachments)
                {
                    if (rT.ForEmail == true)
                    {
                        template = reportTemplate.GetTemplate(rT);
                        break;
                    }   
                }

                if(template==null)
                    template = reportTemplate.DefaultTemplate;

                System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                paraColl.Add("UserId", UserId.ToString());
                paraColl.Add("TranId", tranid.ToString());
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

                        string basePath = "print-tran-log//feereceipt-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".pdf";
                        string sFile = GetPath("~//" + basePath);
                        reportTemplate.SavePDF(_rdlReport.Object, sFile);
                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = basePath;

                        PivotalERP.Global.GlobalFunction glbFN = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                        Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                        {
                            To = toemail,
                            Cc = ccemail,
                            BCC = bccemail,
                            Subject = sub,
                            Message = msg,
                            CUserId = UserId
                        };

                        System.Threading.Thread thread = new System.Threading.Thread(() =>
                        {
                            glbFN.SendEMail(mail, null, sFile);
                        });
                        thread.IsBackground = true;
                        thread.Start();

                        
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


        // POST GetMonthNameList
        /// <summary>
        ///  Get MonthNameList                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Global.CompanyPeriodMonth))]
        public IHttpActionResult GetMonthNameList()
        { 
            try
            { 
                var dataColl = new AcademicLib.BL.Global(UserId, hostName, dbName).usp_GetMonthNameList(this.AcademicYearId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST v1/GetCompanyPeriodMonth
        /// <summary>
        /// Get CompanyPeriodMonth  
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetCompanyPeriodMonth()
        {
            Dynamic.BusinessEntity.Global.CompanyPeriodMonthCollections dataColl = new Dynamic.BusinessEntity.Global.CompanyPeriodMonthCollections();

            try
            {
                dataColl = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyPeriodMonth(UserId,null);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return Json(dataColl, new JsonSerializerSettings
            {
            });
        }

        // POST v1/GetMidasURL
        /// <summary>
        /// Get MidasURL  
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetMidasURL()
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                int userId = UserId;
                var user = new Dynamic.DataAccess.Security.UserDB(hostName, dbName).getUserById(1, userId);
                if (user != null)
                {
                    resVal.ResponseMSG = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl).GetMidasLMS(user.BranchId);
                    resVal.IsSuccess = true;
                }
                 
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return Json(resVal, new JsonSerializerSettings
            {
            });
        }


        // GET v1/GetAllDocumetType
        /// <summary>
        /// Get All Document Type
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(Dynamic.BusinessEntity.GeneralDocument))]
        public IHttpActionResult GetAllDocumentType()
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.DocumentType(1, hostName, dbName).GetAllDocumentType(0);
                var retVal = new
                {
                    IsSuccess=dataColl.IsSuccess,
                    ResponseMSG=dataColl.ResponseMSG,
                    DataColl=dataColl
                };
                return Json(retVal, new JsonSerializerSettings
                {                    
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




        // POST v1/GetCustomData
        /// <summary>
        /// Get Dynamic Data User Define Procedure        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetCustomData([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string procName = "";
                string qry = "";
                bool asParentChild = false;
                string tblNames = "";
                List<string> tblNameColl = new List<string>();

                string colRelations = "";
                List<string> tblColRelationColl = new List<string>();
                System.Collections.Generic.Dictionary<string, string> paraColl = new Dictionary<string, string>();
                if (para != null)
                {
                    if (para.ContainsKey("procName"))
                        procName = Convert.ToString(para["procName"]);
                    else if (para.ContainsKey("qry"))
                        qry = Convert.ToString(para["qry"]);

                    if (para.ContainsKey("asParentChild"))
                        asParentChild = Convert.ToBoolean(para["asParentChild"]);

                    if (para.ContainsKey("tblNames"))
                    {
                        tblNames = Convert.ToString(para["tblNames"]);

                        if (!string.IsNullOrEmpty(tblNames))
                        {
                            foreach (var tN in tblNames.Split(','))
                            {
                                tblNameColl.Add(tN);
                            }
                        }
                    }

                    if (para.ContainsKey("colRelation"))
                    {
                        colRelations = Convert.ToString(para["colRelation"]);

                        if (!string.IsNullOrEmpty(colRelations))
                        {
                            foreach (var tN in colRelations.Split(','))
                            {
                                tblColRelationColl.Add(tN);
                            }
                        }
                    }

                    foreach (var v in para.Properties())
                    {
                        if (v.Name != "procName" && v.Name != "qry" && v.Name != "asParentChild" && v.Name != "tblNames" && v.Name != "colRelation")
                        {
                            paraColl.Add(v.Name, v.Value.ToString());
                        }
                    }
                }

                if (!string.IsNullOrEmpty(procName))
                {

                    if (asParentChild)
                    {
                        var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCustomDataPC(UserId, procName, paraColl, tblNameColl, tblColRelationColl);
                        var retVal = new
                        {
                            IsSuccess = printData.IsSuccess,
                            ResponseMSG = printData.ResponseMSG,
                            DataColl = string.IsNullOrEmpty(printData.JsonStr) ? new JObject() : DeserializeObject<JObject>(printData.JsonStr),
                            //DataColl = DeserializeObject<JObject>(printData.JsonStr),
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                        });
                    }
                    else
                    {
                        var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCustomData(UserId, procName, paraColl);
                        var retVal = new
                        {
                            IsSuccess = printData.IsSuccess,
                            ResponseMSG = printData.ResponseMSG,
                            DataColl = printData.DataColl,
                        };
                        return Json(retVal, new JsonSerializerSettings
                        {
                        });
                    }


                }
                else
                {
                    var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCustomDataFromQry(UserId, qry, paraColl);

                    var retVal = new
                    {
                        IsSuccess = printData.IsSuccess,
                        ResponseMSG = printData.ResponseMSG,
                        DataColl = printData.DataColl
                    };
                    return Json(retVal, new JsonSerializerSettings
                    {
                    });
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

        // POST v1/GetMRSCustomData
        /// <summary>
        /// Get Dynamic Multiple Result Set Data User Define Procedure        
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetMRSCustomData([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string procName = "";
                string qry = "";
                bool asParentChild = false;
                string tblNames = "";
                List<string> tblNameColl = new List<string>();

                System.Collections.Generic.Dictionary<string, string> paraColl = new Dictionary<string, string>();
                if (para != null)
                {
                    if (para.ContainsKey("procName"))
                        procName = Convert.ToString(para["procName"]);
                    else if (para.ContainsKey("qry"))
                        qry = Convert.ToString(para["qry"]);

                    if (para.ContainsKey("asParentChild"))
                        asParentChild = Convert.ToBoolean(para["asParentChild"]);

                    if (para.ContainsKey("tblNames"))
                    {
                        tblNames = Convert.ToString(para["tblNames"]);

                        if (!string.IsNullOrEmpty(tblNames))
                        {
                            foreach (var tN in tblNames.Split(','))
                            {
                                tblNameColl.Add(tN);
                            }
                        }
                    }

                    foreach (var v in para.Properties())
                    {
                        if (v.Name != "procName" && v.Name != "qry" && v.Name != "asParentChild" && v.Name != "tblNames" && v.Name != "colRelation")
                        {
                            paraColl.Add(v.Name, v.Value.ToString());
                        }
                    }
                }

                var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCustomDataMRS(UserId, procName, paraColl, tblNameColl);
                var retVal = new
                {
                    IsSuccess = printData.IsSuccess,
                    ResponseMSG = printData.ResponseMSG,
                    DataColl = string.IsNullOrEmpty(printData.JsonStr) ? new JObject() : DeserializeObject<JObject>(printData.JsonStr),
                };
                return Json(retVal, new JsonSerializerSettings
                {
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


        // POST v1/RunCustomProc
        /// <summary>
        /// Run Custom Procedure with para
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult RunCustomProc([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string procName = "";
                string sqlPara = "";
                List<string> tblNameColl = new List<string>();

                List<string> tblColRelationColl = new List<string>();
                System.Collections.Generic.Dictionary<string, string> paraColl = new Dictionary<string, string>();
                if (para != null)
                {
                    if (para.ContainsKey("procName"))
                        procName = Convert.ToString(para["procName"]);

                    foreach (var v in para.Properties())
                    {
                        if (v.Name != "procName")
                        {
                            sqlPara = sqlPara + " , " + v.Name + ":" + v.Value.ToString();

                            if (v.Name.Trim().ToLower() == "userid")
                            {
                                paraColl.Add("UserId", UserId.ToString());
                            }
                            else
                                paraColl.Add(v.Name, v.Value.ToString());
                        }
                    }
                }

                Dynamic.BusinessEntity.Global.AuditLog auditLog = new Dynamic.BusinessEntity.Global.AuditLog();
                auditLog.TranId = 0;
                auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                auditLog.Action = Dynamic.BusinessEntity.Global.Actions.Modify;
                auditLog.LogText = "Run Customer Proc : " + procName + " with para :" + sqlPara;
                auditLog.AutoManualNo = procName;
                SaveAuditLog(auditLog);

                var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).RunCustomProc(UserId, procName, paraColl);
                var retVal = new
                {
                    IsSuccess = printData.IsSuccess,
                    ResponseMSG = printData.ResponseMSG,
                };
                return Json(retVal, new JsonSerializerSettings
                {
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


        // POST v1/EmailCustomData
        /// <summary>
        /// Send Email  Custom Data
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult EmailCustomData([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                List<string> ignoreKey = new List<string>();
                ignoreKey.Add("UId");
                ignoreKey.Add("passKey");
                ignoreKey.Add("RptPath");
                ignoreKey.Add("procName");

                ignoreKey.Add("ToEmail");
                ignoreKey.Add("CcEmail");
                ignoreKey.Add("Subject");
                ignoreKey.Add("Message");
                ignoreKey.Add("AutoNumber");
                ignoreKey.Add("FileName");
                ignoreKey.Add("Variables");
                ignoreKey.Add("CurrentDateTime");

                if (para == null)
                {
                    return BadRequest();
                }
                else if (!para.ContainsKey("passKey"))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Pass Key";
                }
                else if (para["passKey"].ToString() != "pass@Email")
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Pass Key";
                }
                else if (!para.ContainsKey("UId"))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid User Id";
                }
                else if (!para.ContainsKey("RptPath"))
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Report Path";
                }
                else
                {
                    string procName = "";

                    int AutoNumber = 0;
                    if (para.ContainsKey("AutoNumber"))
                        AutoNumber = Convert.ToInt32(para["AutoNumber"]);

                    string FileName = "";
                    if (para.ContainsKey("FileName"))
                        FileName = Convert.ToString(para["FileName"]);


                    Dynamic.BusinessEntity.Global.SMSEmailNotification emailTemplate = new Dynamic.DataAccess.Global.CustomSMSEmailNotificationDB(hostName, dbName).getSMSEmailNotificationById(1, AutoNumber, Dynamic.BusinessEntity.Global.FORTEMPLATES.EMAIL);

                    if (emailTemplate == null)
                        emailTemplate = new Dynamic.BusinessEntity.Global.SMSEmailNotification();

                    string ToEmail = Convert.ToString(para["ToEmail"]);
                    string CcEmail = emailTemplate.Email_CC;
                    string Subject = emailTemplate.Title;
                    string Message = emailTemplate.Message;

                    if (string.IsNullOrEmpty(Subject))
                    {
                        if (para.ContainsKey("Subject"))
                        {
                            Subject = para["Subject"].ToString();
                        }
                    }


                    if (string.IsNullOrEmpty(Message))
                    {
                        if (para.ContainsKey("Message"))
                        {
                            Message = para["Message"].ToString();
                        }
                    }

                    if (para.ContainsKey("CurrentDateTime"))
                    {
                        var obj = DeserializeObject<JObject>(para["CurrentDateTime"].ToString());
                        foreach (var v in obj.Properties())
                        {
                            string variable = "##" + v.Name + "##";
                            Subject = Subject.Replace(variable, v.Value.ToString());
                            Message = Message.Replace(variable, v.Value.ToString());
                        }
                    }
                    if (para.ContainsKey("Variables"))
                    {
                        var obj = DeserializeObject<JObject>(para["Variables"].ToString());
                        foreach (var v in obj.Properties())
                        {
                            string variable = "##" + v.Name + "##";
                            Subject = Subject.Replace(variable, v.Value.ToString());
                            Message = Message.Replace(variable, v.Value.ToString());
                        }
                    }

                    int UId = Convert.ToInt32(para["UId"]);
                    string fname = Convert.ToString(para["RptPath"]);
                    string RptPath = GetPath("~") + fname;
                    System.Collections.Generic.Dictionary<string, string> paraColl = new Dictionary<string, string>();
                    if (para != null)
                    {
                        if (para.ContainsKey("procName"))
                            procName = Convert.ToString(para["procName"]);

                        foreach (var v in para.Properties())
                        {
                            if (ignoreKey.Contains(v.Name))
                            {

                            }
                            else
                            {
                                paraColl.Add(v.Name, v.Value.ToString());
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(procName))
                    {
                        var printData = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCustomData(UId, procName, paraColl);

                        var template = new ClosedXML.Report.XLTemplate(RptPath);
                        var key = GuiId.Replace("-", "");
                        var urlPath = "print-tran-log\\" + key + ".xlsx";
                        string outputFile = GetPath("~") + urlPath;

                        var comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UId, 0, 0, 0);
                        if (comDet != null || !string.IsNullOrEmpty(comDet.CompanyName))
                        {
                            System.Collections.Specialized.NameValueCollection rptParaColl = GetObjectAsKeyVal(comDet);
                            rptParaColl.Add("UserId", UId.ToString());
                            rptParaColl.Add("UserName", "AutoEmail");
                            rptParaColl.Add("UserFullName", "AutoEmail");
                            rptParaColl.Add("UserDesignation", "AutoEmail");
                            for (int i = 0; i < rptParaColl.Count; i++)
                            {
                                template.AddVariable(rptParaColl.GetKey(i), rptParaColl[i]);
                            }

                            template.AddVariable("DataSource", printData.DataColl);

                            template.Generate();
                            template.SaveAs(outputFile);

                            Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                            {
                                To = ToEmail,
                                Cc = CcEmail,
                                BCC = emailTemplate.Email_BCC,
                                Subject = Subject,
                                Message = Message,
                                CUserId = UId
                            }; 
                            PivotalERP.Global.GlobalFunction glbFN = new PivotalERP.Global.GlobalFunction(UId, hostName, dbName, GetBaseUrl);
                            glbFN.SendEMail(mail, null, outputFile);

                            resVal.ResponseId = outputFile;
                            resVal.IsSuccess = true;

                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Unable To Get Company Details";
                        }

                    }
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


        // GET v1/GetHomeworkConfig
        /// <summary>
        /// Get Homework Configuration
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetHomeworkConfig([FromBody]JObject para)
        {

            int? branchId = null;

            if (para != null)
            {
                if (para.ContainsKey("branchId"))
                    branchId = Convert.ToInt32(para[branchId]);
            }

            var beData = new AcademicLib.BL.HomeWork.Configuration(UserId, hostName, dbName).GetHAConfiguration(0, branchId);

            var retVal = new
            {
                Data = beData,
                IsSuccess = beData.IsSuccess,
                ResponseMSG = beData.ResponseMSG
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {

                }
            });
        }


        // Post api/SaveReg
        /// <summary>
        ///  Submit Online Registration
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveAlumni()
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

                    AcademicLib.BE.AppCMS.Creation.AlumniReg para = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AlumniReg>(jsonData);
                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        var dataValidation = GenerateHash(HASH_KEY, $"{para.FullName},{para.Contact},{para.Email}");
                        if (para.HashData == dataValidation)
                        {
                            if (provider.FileData.Count > 0)
                            {
                                try
                                {
                                    var DocumentColl = GetAttachmentDocuments(provider.FileData);
                                    foreach (var file in DocumentColl)
                                    {
                                        if (file.ParaName == "MarksheetPhoto")
                                        {
                                            para.MarksheetPath = file.DocPath;
                                        }
                                        else if (file.ParaName == "ProfilePhoto")
                                        {
                                            para.ProfilePhoto = file.DocPath;
                                        }
                                        else if (file.ParaName == "MemoryPhoto1")
                                        {
                                            para.MemoryPhoto1 = file.DocPath;
                                        }
                                        else if (file.ParaName == "MemoryPhoto2")
                                        {
                                            para.MemoryPhoto2 = file.DocPath;
                                        }
                                        else
                                        {
                                            para.Achievement_Doc = file.DocPath;
                                        }
                                    }
                                }
                                catch { }
                            }
                            para.AlumniRegId = 0;
                            para.CUserId = 1;
                            resVal = new AcademicLib.BL.AppCMS.Creation.AlumniReg(1, hostName, dbName).SaveFormData(para);
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Invalid Hashdata";
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

        // POST v1/GetRptTemplates
        /// <summary>
        /// Get Report Templates
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult GetRptTemplates([FromBody] JObject para)
        {

            ResponeValues resVal = new ResponeValues();

            try
            {
                int entityId=0;
                int voucherId=0;
                bool isTran=false;
                int? rptTranId = null;

                if (para.ContainsKey("entityId"))
                    entityId = Convert.ToInt32(para["entityId"]);


                if (para.ContainsKey("voucherId"))
                    voucherId = Convert.ToInt32(para["voucherId"]);


                if (para.ContainsKey("isTran"))
                    isTran = Convert.ToBoolean(para["isTran"]);

                if (para.ContainsKey("rptTranId"))
                    rptTranId = Convert.ToInt32(para["rptTranId"]);

                if (rptTranId.HasValue && rptTranId>0)
                {
                    PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false, rptTranId.Value);                    
                    return Json(reportTemplate.DefaultTemplate, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                        }
                    });
                }
                else
                {
                    Dynamic.BusinessEntity.Global.ReportTempletesAttachmentsCollections dataColl = new Dynamic.DataAccess.Global.ReportTempletesDB(hostName, dbName, isTran).getReportTempletesForSelection(UserId, entityId, voucherId, isTran);

                    return Json(dataColl, new JsonSerializerSettings
                    {
                        ContractResolver = new JsonContractResolver()
                        {
                        }
                    });
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

        
        /// <summary>
        /// Get AcademicConfig
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAcademicConfig([FromBody] JObject para)
        {
            var beData = new AcademicLib.BL.Academic.Setup.AcademicConfiguration(UserId, hostName, dbName).GetConfiguration(0);

            var retVal = new
            {
                Data = beData,
                IsSuccess = beData.IsSuccess,
                ResponseMSG = beData.ResponseMSG
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {

                }
            });
        }



        /// <summary>
        /// Get Default AcademicYearId
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDefaultAcademicYearId([FromBody] JObject para)
        {
            var beData = new AcademicLib.BL.Academic.Creation.AcademicYear(UserId, hostName, dbName).getDefaultAcademicYearId();

            var retVal = new
            {
                RId = beData.RId,
                IsSuccess = beData.IsSuccess,
                ResponseMSG = beData.ResponseMSG
            };
            return Json(retVal, new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver()
                {

                }
            });
        }


        /// <summary>
        /// Get Enquiry Form Config
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(AcademicLib.BE.Academic.Creation.EnquiryFormConfig))]
        public IHttpActionResult GetEnquiryFormConfig()
        {
            AcademicLib.BE.Academic.Creation.EnquiryFormConfig returnVal = new AcademicLib.BE.Academic.Creation.EnquiryFormConfig();

            try
            {
                returnVal = new AcademicLib.BL.Academic.Creation.EnquiryFormConfig(UserId, hostName, dbName).GetAllEnquiryFormConfig(0);
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


        //[HttpPost]
        //[AllowAnonymous]
        //[ResponseType(typeof(ResponeValues))]
        //public IHttpActionResult CheckUserAuthCode(string Username)
        //{
        //    ResponeValue resVal = new ResponeValue();

        //    try
        //    {
        //        resVal = new AcademicLib.BL.Setup.CheckOTP(UserId, hostName, dbName).GetcheckOTPById(Username);
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return Json(resVal, new JsonSerializerSettings
        //    {
        //    });
        //}


        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult CheckUserAuthCode([FromBody] JObject para)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                string Username = "";
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {

                    if (para.ContainsKey("Username"))
                        Username = Convert.ToString(para["Username"]);
                    resVal = new AcademicLib.BL.Setup.CheckOTP(UserId, hostName, dbName).GetcheckOTPById(Username);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            var retVal = new
            {
                ResponseMSG = resVal.ResponseMSG,
                IsSuccess = resVal.IsSuccess
            };
            return Json(retVal, new JsonSerializerSettings
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
}
