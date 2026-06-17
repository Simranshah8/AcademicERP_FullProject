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
using PivotalERP;

namespace AcademicERP.Controllers
{
    public class InventoryController : APIBaseController
    {


        #region "Unit"

        // POST api/GetUnitList
        /// <summary>
        ///  Get UnitList 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Inventory.Unit>))]
        public IHttpActionResult GetUnitList()
        {
            Dynamic.BusinessEntity.Inventory.UnitCollections dataColl = new Dynamic.BusinessEntity.Inventory.UnitCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.UnitDB(hostName, dbName).getAllUnit(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "UnitId","Name","Alias","NoOfDecimalPlaces","IsSuccess","ResponseMSG"
                                 }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/SaveUnit
        /// <summary>
        ///  Save Unit 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult SaveUnit([FromBody] Dynamic.BusinessEntity.Inventory.Unit para)
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
                    Dynamic.BusinessLogic.Inventory.Unit unit = new Dynamic.BusinessLogic.Inventory.Unit(hostName, dbName);
                    resVal = unit.SaveFormData(para);
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

        #region "Godown"

        // POST api/GetGodownList
        /// <summary>
        ///  Get GodownList 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Inventory.Godown>))]
        public IHttpActionResult GetGodownList()
        {
            Dynamic.BusinessEntity.Inventory.GodownCollections dataColl = new Dynamic.BusinessEntity.Inventory.GodownCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.GodownDB(hostName, dbName).getAllGodown(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "GodownId","Name","Alias","Code","Address","PhoneNo","ContactPerson","IsSuccess","ResponseMSG"
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

        #region "ProductGroup"

        // POST api/GetProductGroupList
        /// <summary>
        ///  Get ProductGroup List 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Inventory.ProductGroup>))]
        public IHttpActionResult GetProductGroupList()
        {
            Dynamic.BusinessEntity.Inventory.ProductGroupCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductGroupCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductGroupDB(hostName, dbName).getAllProductGroup(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "ProductGroupId","Name","Alias","Code","ParentGroupName","IsSuccess","ResponseMSG"
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

        #region "ProductBrand"

        // POST api/GetProductBrandList
        /// <summary>
        ///  Get ProductBrand List 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.BusinessEntity.Inventory.ProductGroup>))]
        public IHttpActionResult GetProductBrandList()
        {
            Dynamic.BusinessEntity.Inventory.ProductBrandCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductBrandCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductBrandDB(hostName, dbName).getAllProductBrand(UserId);
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                    "ProductBrandId","Name","Alias","IsSuccess","ResponseMSG"
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

        #region "Product"

        // POST api/AutoCompleteProductList
        /// <summary>
        ///  Get AutoCompleteProductList 
        ///  searchBy as Int
        ///  searchValue as String
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Inventory.Product>))]
        public IHttpActionResult AutoCompleteProductList([FromBody] JObject para)
        {
            Dynamic.APIEnitity.Inventory.ProductCollections dataColl = new Dynamic.APIEnitity.Inventory.ProductCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    string searchBy = ((Dynamic.APIEnitity.Inventory.PRODUCT_SEARCHOPTIONS)Convert.ToInt32(para["searchBy"])).GetStringValue();
                    string searchValue = Convert.ToString(para["searchValue"]);
                    dataColl = new Dynamic.DataAccess.Inventory.ProductDB(hostName, dbName).getAutoCompleteProductList(UserId, searchBy, searchValue);
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

        // POST api/GetProductDetail
        /// <summary>
        ///  Get GetProductDetail  \n
        ///  productId as Int \n
        ///  ledgerId(partyledgerId) as Int (Optional) \n
        ///  voucherType as Int (Optional) \n
        ///  voucherDate(EntryDate) as DateTime (Optional) \n 
        ///  dateFrom as DateTime (Optional) \n
        ///  dateTo as DateTime (Optional)
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        [ResponseType(typeof(Dynamic.BusinessEntity.Common.ProductDetails))]
        public IHttpActionResult GetProductDetail([FromBody] JObject para)
        {
            Dynamic.BusinessEntity.Common.ProductDetails det = new Dynamic.BusinessEntity.Common.ProductDetails();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {

                    int ProductId = 0;
                    if (para["productId"] != null)
                        ProductId = Convert.ToInt32(para["productId"]);
                    else
                        return BadRequest("Invalid Selected Product(StockItem)");

                    int? LedgerId = null;
                    if (para["ledgerId"] != null)
                        LedgerId = Convert.ToInt32(para["ledgerId"]);

                    int? voucherType = null;
                    if (para["voucherType"] != null)
                        voucherType = Convert.ToInt32(para["voucherType"]);

                    DateTime? voucherDate = null;
                    if (para["voucherDate"] != null)
                        voucherDate = Convert.ToDateTime(para["voucherDate"]);

                    DateTime? dateFrom = null;
                    if (para["dateFrom"] != null)
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime? dateTo = null;
                    if (para["dateTo"] != null)
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    det = new Dynamic.DataAccess.Common.ProductDB(hostName, dbName).getProductDetails(UserId, ProductId, LedgerId, voucherDate, dateFrom, dateTo, voucherType, null,null,null,null);

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


        // POST api/SaveProduct
        /// <summary>
        ///  Save Inventory New Product
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveProduct()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/inventory/product"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Inventory.Product para = DeserializeObject<Dynamic.BusinessEntity.Inventory.Product>(jsonData);

                    if (para == null)
                    {
                        return BadRequest("No form data found");
                    }
                    else
                    {
                        para.CUserId = UserId;

                        if (!jsonData.Contains("ProductGroupId"))
                            para.ProductGroupId = 1;

                        if (!jsonData.Contains("ProductCompanyId"))
                            para.ProductCompanyId = 1;

                        if (provider.FileData.Count > 0)
                            para.ImageColl = GetAttachmentDocuments(provider.FileData);

                        Dynamic.BusinessLogic.Inventory.Product product = new Dynamic.BusinessLogic.Inventory.Product(hostName, dbName);

                        resVal = product.SaveFormData(para);
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

        #region "Purchase Invoice "

        // POST api/SavePurchaseInvoice
        /// <summary>
        ///  Save Inventory Transaction Purchase Invoice 
        ///  { 
        ///  VoucherDate:'2020-10-23',
        ///  ManualVoucherNO:'',
        ///  VoucherId:1,
        ///  RefNo:'test ref 1212',
        ///  Narration:'Test Narration',
        ///  PartyLedgerId:1,
        ///  TotalAmount:152.55,
        ///  ItemAllocationColl:
        ///  [
        ///  {
        ///  
        ///  ProductId:1,
        ///  LedgerId:4,
        ///  UnitId:1,
        ///  ActualQty:6,
        ///  BilledQty:5,
        ///  FreeQty:1,
        ///  Rate:20,
        ///  DiscountPer:10,
        ///  DiscountAmt:10,
        ///  Amount:90
        ///  },
        ///  {
        ///  ProductId:2,
        ///  LedgerId:4,
        ///  UnitId:1,
        ///  ActualQty:12,
        ///  BilledQty:10,
        ///  FreeQty:2,
        ///  Rate:5,
        ///  DiscountPer:10,
        ///  DiscountAmt:5,
        ///  Amount:45
        ///  }
        ///  ],
        ///  AditionalCostColl:[
        ///  {
        ///  
        ///  LedgerId:5,
        ///  Rate:13,
        ///  Amount:17.55,
        ///  Narration:''
        ///  }
        ///  ]
        ///  }
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SavePurchaseInvoice()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/inventory/purchase"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Inventory.Transaction.PurchaseInvoice para = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseInvoice>(jsonData);

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

                        Dynamic.BusinessLogic.Inventory.Transaction.PurchaseInvoice purInv = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseInvoice(hostName, dbName);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = purInv.SaveFormData(para);
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

        #region "Purchase Return"

        // POST api/SavePurchaseReturn
        /// <summary>
        ///  Save Inventory Transaction Purchase Return 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SavePurchaseReturn()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/inventory/purchasereturn"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn para = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn>(jsonData);

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

                        Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn purRet = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn(hostName, dbName);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = purRet.SaveFormData(para);
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

        #region "Sales Invoice "

        // POST api/SaveSalesInvoice
        /// <summary>
        ///  Save Inventory Transaction Sales Invoice 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveSalesInvoice()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/inventory/sales"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice para = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(jsonData);

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

                        Dynamic.BusinessLogic.Inventory.Transaction.SalesInvoice salesInv = new Dynamic.BusinessLogic.Inventory.Transaction.SalesInvoice(hostName, dbName);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = salesInv.SaveFormData(para);
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

        #region "Sales Return "

        // POST api/SaveSalesReturn
        /// <summary>
        ///  Save Inventory Transaction Sales Return 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> SaveSalesReturn()
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
                    var provider = new FormDataStreamProvider(GetPath("~/Attachments/inventory/salesreturn"));
                    await Request.Content.ReadAsMultipartAsync(provider);

                    string jsonData = provider.FormData["paraDataColl"];
                    if (string.IsNullOrEmpty(jsonData))
                        return BadRequest("No data found");

                    Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn para = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn>(jsonData);

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

                        Dynamic.BusinessLogic.Inventory.Transaction.SalesReturn salesRet = new Dynamic.BusinessLogic.Inventory.Transaction.SalesReturn(hostName, dbName);
                        para.VoucherType = Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn;
                        para.CreatedBy = UserId;
                        para.ModifyBy = UserId;


                        resVal = salesRet.SaveFormData(para);
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

        // POST api/GetProductGroupSummaryAsList
        /// <summary>
        ///  Get Product Group Summary as List
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  ProductGroupId as Int   
        ///  showAlternetUnit as boolean
        ///  GodownIdColl as string(optional)  like 1,2,3
        ///  voucherIdColl as string(optional)  like 1,2,3
        ///  IncludeAditionalCost as Boolean
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.ProductGroupSummary>))]
        public IHttpActionResult GetProductGroupSummaryAsList([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.ProductGroupSummaryCollections dataColl = new Dynamic.ReportEntity.Inventory.ProductGroupSummaryCollections();
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

                    int ProductGroupId = 0;
                    if (para.ContainsKey("productGroupId"))
                        ProductGroupId = Convert.ToInt32(para["productGroupId"]);

                    bool showalternetUnit = false;
                    if (para.ContainsKey("showAlternetUnit"))
                        showalternetUnit = Convert.ToBoolean(para["showAlternetUnit"]);

                    string voucherIdColl = "";
                    if (para.ContainsKey("voucherIdColl"))
                        voucherIdColl = Convert.ToString(para["voucherIdColl"]);

                    string godownIdColl = "";
                    if (para.ContainsKey("godownIdColl"))
                        godownIdColl = Convert.ToString(para["godownIdColl"]);

                    bool includeAditionalCost = false;
                    if (para.ContainsKey("includeAditionalCost"))
                        includeAditionalCost = Convert.ToBoolean(para["includeAditionalCost"]);

                    dataColl = new Dynamic.Reporting.Inventory.ProductGroupSummary(hostName, dbName).getProductGroupSummary(UserId, ProductGroupId, dateFrom, dateTo, showalternetUnit, includeAditionalCost, voucherIdColl, godownIdColl);

                    var retData = new
                    {
                        DataColl = dataColl,
                        IsSuccess = dataColl.IsSuccess,
                        ResponseMSG = dataColl.ResponseMSG
                    };

                    return Json(retData, new JsonSerializerSettings
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

        // POST api/GetCurrentStatus
        /// <summary>
        ///  Get Currect Status
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.ProductCurrentStatusCollections>))]
        public IHttpActionResult GetProductCurrentStatus([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.ProductCurrentStatusCollections dataColl = new Dynamic.ReportEntity.Inventory.ProductCurrentStatusCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    int productId = 0;
                    if (para.ContainsKey("productId"))
                        productId = Convert.ToInt32(para["productId"]);

                    Dynamic.BusinessEntity.Inventory.Product prod = new Dynamic.BusinessEntity.Inventory.Product();
                    prod.ProductId = productId;

                    dataColl = new Dynamic.Reporting.Inventory.ProductCurrentStatus(hostName, dbName).getProductCurrentStatus(UserId,prod.ProductId,null, ref prod);
                    var retData = new
                    {
                        Name = prod.Name,
                        Code = prod.Code,
                        Alias = prod.Alias,
                        PartNo = prod.PartNo,
                        Remarks = prod.Remarks,
                        BaseUnit = prod.BaseUnitName,
                        GodownWiseDataColl = dataColl,
                        IsSuccess = true,
                        ResponseMSG = "Success"
                    };

                    return Json(retData, new JsonSerializerSettings
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

        // POST api/GetProductVoucher
        /// <summary>
        ///  Get Product Voucher
        ///  DateFrom as Date
        ///  DateTo as Date
        ///  ProductId as Int        
        ///  GodownIdColl as string(optional) default blank for all (1,2,3)
        ///  IncludeAditionalCost as Boolean
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.ProductVoucher>))]
        public IHttpActionResult GetProductVoucher([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.ProductVoucherCollections dataColl = new Dynamic.ReportEntity.Inventory.ProductVoucherCollections();
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

                    int ProductId = 0;
                    if (para.ContainsKey("productId"))
                        ProductId = Convert.ToInt32(para["productId"]);

                    string GodownIdColl = "";
                    if (para.ContainsKey("godownIdColl"))
                        GodownIdColl = Convert.ToString(para["godownIdColl"]);

                    bool IncludeAditionalCost = false;
                    if (para.ContainsKey("includeAditionalCost"))
                        IncludeAditionalCost = Convert.ToBoolean(para["includeAditionalCost"]);

                    double openingAmt = 0, openingQty = 0;
                    double closingRate = 0;
                    DateTime openingDateAD=DateTime.Today;
                    string openingDateBS = "" ;
                    Dynamic.ReportEntity.Account.StockReport stkRpt = new Dynamic.ReportEntity.Account.StockReport();
                    Dynamic.ReportEntity.Inventory.ProductVoucherCollections openingColl = new Dynamic.ReportEntity.Inventory.ProductVoucherCollections();
                    var tmpDataColl = new Dynamic.Reporting.Inventory.ProductMonthlySummary(hostName, dbName).getProductVoucher(ref openingColl, ref stkRpt, UserId, ProductId, dateFrom, dateTo, ref openingQty, ref openingAmt, ref closingRate, ref openingDateAD,ref openingDateBS, IncludeAditionalCost, GodownIdColl);
                    if (tmpDataColl != null && tmpDataColl.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Common.ProductDetails productDetails = new Dynamic.DataAccess.Common.ProductDB(hostName, dbName).getProductDetails(UserId, ProductId, null, DateTime.Today, dateFrom, dateTo, null, null,null,null,null);

                        if (productDetails != null)
                        {
                            Dynamic.BusinessEntity.Inventory.StockItemAlternetUnitCollections alternetUnitColl = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getProductUnitDetails(BranchId, ProductId);

                            if (alternetUnitColl == null)
                                alternetUnitColl = new Dynamic.BusinessEntity.Inventory.StockItemAlternetUnitCollections();

                            double unitBaseValue1 = (alternetUnitColl.Count > 0 ? alternetUnitColl[0].BaseUnitValue : Convert.ToDouble(0));
                            double unitBaseValue2 = (alternetUnitColl.Count > 1 ? alternetUnitColl[1].BaseUnitValue : Convert.ToDouble(0));
                            double unitBaseValue3 = (alternetUnitColl.Count > 2 ? alternetUnitColl[2].BaseUnitValue : Convert.ToDouble(0));

                            double unitAlternetValue1 = (alternetUnitColl.Count > 0 ? alternetUnitColl[0].AlterNetUnitValue : Convert.ToDouble(0));
                            double unitAlternetValue2 = (alternetUnitColl.Count > 1 ? alternetUnitColl[1].AlterNetUnitValue : Convert.ToDouble(0));
                            double unitAlternetValue3 = (alternetUnitColl.Count > 2 ? alternetUnitColl[2].AlterNetUnitValue : Convert.ToDouble(0));

                            if (openingAmt != 0 || openingQty != 0)
                            {
                                Dynamic.ReportEntity.Inventory.ProductVoucher voucher = new Dynamic.ReportEntity.Inventory.ProductVoucher();
                                voucher.Particulars = "Opening Balance";
                                voucher.PartyLedger = "Opening Balance";
                                voucher.VoucherDateStr = "";
                                voucher.VoucherDateNP = "";
                                //voucher.VoucherDate = Dynamic.BusinessEntity.Global.GlobalObject.CurrentCompany.StartDate;
                                voucher.InQty = openingQty;
                                voucher.InAmt = openingAmt;
                                voucher.IsOpening = true;
                                if (openingAmt != 0 && openingQty != 0)
                                    voucher.InRate = openingAmt / openingQty;

                                voucher.BalanceQty = openingQty;
                                voucher.BalanceAmt = openingAmt;
                                voucher.BalanceRate = voucher.InRate;
                                voucher.VoucherSNo = 0;

                                if (tmpDataColl.Count > 0)
                                    tmpDataColl.Insert(0, voucher);
                                else
                                    tmpDataColl.Add(voucher);
                            }


                            double curQty = 0, curRate = 0;
                            double curInQty = 0, curInAmt = 0;

                            var query = from q in tmpDataColl
                                        orderby q.VoucherDate, q.VoucherSNo
                                        select q;

                            Dynamic.ReportEntity.Inventory.ProductVoucherCollections inDataOnlyColl = new Dynamic.ReportEntity.Inventory.ProductVoucherCollections();

                            double negativeBal = 0;

                            double curAmt = 0;

                            foreach (var v in query)
                            {
                                if (v.VoucherDate.Year > 2000)
                                {
                                    v.VoucherDateStr = v.VoucherDate.ToString("yyyy-MM-dd");
                                    v.VoucherDateNP = v.NY.ToString() + "-" + v.NM.ToString().PadLeft(2, '0') + "-" + v.ND.ToString().PadLeft(2, '0');
                                }
                                if (v.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor)
                                    v.Particulars = v.VoucherLedger;
                                else if (v.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.StockJournal || v.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.ManufacturingStockJournal)
                                    v.Particulars = v.GodownName;
                                else if (v.VoucherType == Dynamic.BusinessEntity.Account.VoucherTypes.Consumption)
                                {
                                    if (string.IsNullOrEmpty(v.PartyLedger))
                                        v.Particulars = v.GodownName;
                                    else
                                        v.Particulars = v.PartyLedger;

                                }
                                else
                                    v.Particulars = v.PartyLedger;


                                if (v.InAmt != 0 && v.InQty != 0)
                                    v.InRate = (v.InAmt / v.InQty);

                                if (v.OutAmt != 0 && v.OutQty != 0)
                                    v.OutRate = (v.OutAmt / v.OutQty);

                                if (v.OutQty == 0)
                                {
                                    v.AfterIssueQty = v.InQty;
                                    v.IssueAmt = v.InAmt;
                                    inDataOnlyColl.Add(v);
                                }

                                curInQty += v.InQty;
                                curInAmt += v.InAmt;
                                curQty += (v.InQty - v.OutQty);
                                curAmt += (v.InAmt - v.OutAmt);

                                if (curInQty != 0 && curInAmt != 0)
                                    curRate = (curInAmt / curInQty);

                                v.BalanceQty = curQty;

                                if (v.BalanceQty < 0)
                                    negativeBal = Math.Abs(v.BalanceQty);
                                else
                                    negativeBal = 0;

                                Dynamic.BusinessEntity.Inventory.Product selectedProduct = new Dynamic.BusinessEntity.Inventory.Product();
                                switch (selectedProduct.CostingMethod)
                                {
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.At_Zero_Cost:
                                        curRate = 0;
                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.Monthly_Avg_Cost:
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.Avg_Cost:
                                        {
                                            double totalIn = inDataOnlyColl.Sum(p1 => p1.InQty);
                                            double totalInAmt = inDataOnlyColl.Sum(p1 => p1.InAmt);

                                            if (totalIn != 0 && totalInAmt != 0)
                                                curRate = (totalInAmt / totalIn);
                                        }
                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.FIFO:
                                        {
                                            var queryIn = inDataOnlyColl.Where(p1 => p1.AfterIssueQty > 0);
                                            double hOut = v.OutQty + negativeBal;
                                            System.Collections.Generic.List<Dynamic.ReportEntity.Inventory.ProductVoucher> tmpC = new List<Dynamic.ReportEntity.Inventory.ProductVoucher>();
                                            foreach (var iV in queryIn)
                                            {
                                                if (iV.AfterIssueQty > hOut)
                                                {
                                                    iV.IssueQty = iV.IssueQty + hOut;
                                                    iV.AfterIssueQty = iV.InQty - iV.IssueQty;
                                                    iV.IssueAmt = iV.AfterIssueQty * iV.InRate;
                                                    break;
                                                }
                                                else
                                                {
                                                    hOut -= iV.AfterIssueQty;
                                                    tmpC.Add(iV);
                                                }
                                            }

                                            if (hOut > 0)
                                                negativeBal = hOut;

                                            foreach (var vvv in tmpC)
                                                inDataOnlyColl.Remove(vvv);

                                            double totalIn = inDataOnlyColl.Sum(p1 => p1.AfterIssueQty);
                                            double totalInAmt = inDataOnlyColl.Sum(p1 => p1.IssueAmt);

                                            if (totalIn != 0 && totalInAmt != 0)
                                                curRate = (totalInAmt / totalIn);

                                        }

                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.Last_Purchase_Cost:
                                        if (inDataOnlyColl.Count > 0)
                                            curRate = inDataOnlyColl.Last().InRate;
                                        else
                                            curRate = 0;
                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.LIFO:
                                        {
                                            var queryIn = inDataOnlyColl.Where(p1 => p1.AfterIssueQty > 0).OrderByDescending(p1 => p1.VoucherDate).OrderByDescending(p1 => p1.TranId);
                                            double hOut = v.OutQty + negativeBal;
                                            System.Collections.Generic.List<Dynamic.ReportEntity.Inventory.ProductVoucher> tmpC = new List<Dynamic.ReportEntity.Inventory.ProductVoucher>();
                                            foreach (var iV in queryIn)
                                            {
                                                if (iV.AfterIssueQty > hOut)
                                                {
                                                    iV.IssueQty = iV.IssueQty + hOut;
                                                    iV.AfterIssueQty = iV.InQty - iV.IssueQty;
                                                    iV.IssueAmt = iV.AfterIssueQty * iV.InRate;
                                                    break;
                                                }
                                                else
                                                {
                                                    hOut -= iV.AfterIssueQty;
                                                    tmpC.Add(iV);
                                                }
                                            }

                                            if (hOut > 0)
                                                negativeBal = hOut;

                                            foreach (var vvv in tmpC)
                                                inDataOnlyColl.Remove(vvv);

                                            double totalIn = inDataOnlyColl.Sum(p1 => p1.AfterIssueQty);
                                            double totalInAmt = inDataOnlyColl.Sum(p1 => p1.IssueAmt);

                                            if (totalIn != 0 && totalInAmt != 0)
                                                curRate = (totalInAmt / totalIn);

                                        }

                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.Std_Cost:
                                        curRate = closingRate;
                                        break;
                                    case Dynamic.BusinessEntity.Inventory.CostingMethods.InOut_Amount:

                                        if (curQty != 0 && curAmt != 0)
                                            curRate = curAmt / curQty;

                                        break;
                                }

                                if (v.BalanceQty == 0)
                                    curRate = 0;

                                v.BalanceRate = curRate;
                                v.BalanceAmt = v.BalanceQty * v.BalanceRate;


                                if (unitBaseValue1 != 0 && unitAlternetValue1 != 0)
                                {
                                    v.InQty1 = (unitAlternetValue1 * v.InQty) / unitBaseValue1;
                                    v.OutQty1 = (unitAlternetValue1 * v.OutQty) / unitBaseValue1;
                                    v.BalanceQty1 = (unitAlternetValue1 * v.BalanceQty) / unitBaseValue1;
                                }

                                if (unitBaseValue2 != 0 && unitAlternetValue2 != 0)
                                {
                                    v.InQty2 = (unitAlternetValue2 * v.InQty) / unitBaseValue2;
                                    v.OutQty2 = (unitAlternetValue2 * v.OutQty) / unitBaseValue2;
                                    v.BalanceQty2 = (unitAlternetValue2 * v.BalanceQty) / unitBaseValue2;
                                }

                                if (unitBaseValue3 != 0 && unitAlternetValue3 != 0)
                                {
                                    v.InQty3 = (unitAlternetValue3 * v.InQty) / unitBaseValue3;
                                    v.OutQty3 = (unitAlternetValue3 * v.OutQty) / unitBaseValue3;
                                    v.BalanceQty3 = (unitAlternetValue3 * v.BalanceQty) / unitBaseValue3;
                                }

                                dataColl.Add(v);
                                // tmpDataColl.Add(v);
                            }

                            //dataColl = tmpDataColl;

                            double inQty = 0, inAmt = 0, inRate = 0;
                            double outQty = 0, outRate = 0, outAmt = 0;
                            double clQty = 0, clAmt = 0, clRate = 0;
                            inQty = dataColl.Sum(p1 => p1.InQty);
                            inAmt = dataColl.Sum(p1 => p1.InAmt);
                            outQty = dataColl.Sum(p1 => p1.OutQty);
                            outAmt = dataColl.Sum(p1 => p1.OutAmt);

                            if (inAmt != 0 && inQty != 0)
                                inRate = inAmt / inQty;

                            if (outAmt != 0 && outQty != 0)
                                outRate = outAmt / outQty;

                            clQty = inQty - outQty;
                            clAmt = inAmt - outAmt;
                            clRate = curRate;
                            clAmt = clQty * clRate;

                            dataColl.IsSuccess = tmpDataColl.IsSuccess;
                            dataColl.ResponseMSG = tmpDataColl.ResponseMSG;

                            var returnVal = new
                            {
                                OpeningAmount = openingAmt,
                                OpeningQty = openingQty,
                                InQty = inQty,
                                OutQty = outQty,
                                InAmt = inAmt,
                                OutAmt = outAmt,
                                BalanceQty = clQty,
                                BalanceAmt = clAmt,
                                BalanceRate = clRate,
                                Details = productDetails,
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

                    return BadRequest("No Data Found");
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }



        // POST api/PAEmailToAgent
        /// <summary>
        ///  Send Email Party Ageing Report To Agent (ASM)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult PAEmailToAgent([FromBody] JObject para)
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
                    int entityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.PartyWiseAgeingReport);
                    DateTime dateFrom = DateTime.Today;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime dateTo = DateTime.Today;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    int r1 = 0;
                    if (para.ContainsKey("r1"))
                        r1 = Convert.ToInt32(para["r1"]);

                    int r2 = 0;
                    if (para.ContainsKey("r2"))
                        r2 = Convert.ToInt32(para["r2"]);

                    int r3 = 0;
                    if (para.ContainsKey("r3"))
                        r3 = Convert.ToInt32(para["r3"]);

                    int r4 = 0;
                    if (para.ContainsKey("r4"))
                        r4 = Convert.ToInt32(para["r4"]);

                    int r5 = 0;
                    if (para.ContainsKey("r5"))
                        r5 = Convert.ToInt32(para["r5"]);

                    int r6 = 0;
                    if (para.ContainsKey("r6"))
                        r6 = Convert.ToInt32(para["r6"]);

                    int r7 = 0;
                    if (para.ContainsKey("r7"))
                        r7 = Convert.ToInt32(para["r7"]);

                    int r8 = 0;
                    if (para.ContainsKey("r8"))
                        r8 = Convert.ToInt32(para["r8"]);

                    int r9 = 0;
                    if (para.ContainsKey("r9"))
                        r9 = Convert.ToInt32(para["r9"]);

                    int r10 = 0;
                    if (para.ContainsKey("r10"))
                        r10 = Convert.ToInt32(para["r10"]);

                    string subject = "Party OutStanding";
                    if (para.ContainsKey("subject"))
                        subject = Convert.ToString(para["subject"]);

                    string message = "Dear sir/madam, \n pls find the attach pdf file.";

                    if (para.ContainsKey("message"))
                        message = Convert.ToString(para["message"]);

                    var tmpDataColl = new Dynamic.Reporting.Inventory.PartyAgeingReport(hostName, dbName).getPartyAgeingReport(1, 12, dateFrom, dateTo, r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, false, false, false);
                    if (tmpDataColl != null && tmpDataColl.IsSuccess)
                    {
                        PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                        if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                        {
                            return BadRequest("No Report Templates Found");
                        }
                        Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                        Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, 0, 0, 0);

                        System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);    
                        paraColl.Add("UserId", UserId.ToString());
                        paraColl.Add("UserName", User.Identity.Name);
                        paraColl.Add("Period", dateFrom.ToString("yyyy-MM-dd") + " To " + dateTo.ToString("yyyy-MM-dd"));
                        paraColl.Add("R1", "<= " + r1.ToString() + " Days");
                        paraColl.Add("R2", (r2 != 0 ? ">" + r1.ToString() + " & <= " + r2.ToString() + " Days" : (r1 == 0 ? "" : " > " + r1.ToString())));
                        paraColl.Add("R3", (r3 != 0 ? ">" + r2.ToString() + " & <= " + r3.ToString() + " Days" : (r2 == 0 ? "" : " > " + r2.ToString())));
                        paraColl.Add("R4", (r4 != 0 ? ">" + r3.ToString() + " & <= " + r3.ToString() + " Days" : (r3 == 0 ? "" : " > " + r3.ToString())));
                        paraColl.Add("R5", (r5 != 0 ? ">" + r4.ToString() + " & <= " + r3.ToString() + " Days" : (r4 == 0 ? "" : " > " + r4.ToString())));
                        paraColl.Add("R6", (r6 != 0 ? ">" + r5.ToString() + " & <= " + r3.ToString() + " Days" : (r5 == 0 ? "" : " > " + r5.ToString())));
                        paraColl.Add("R7", (r7 != 0 ? ">" + r6.ToString() + " & <= " + r3.ToString() + " Days" : (r6 == 0 ? "" : " > " + r6.ToString())));
                        paraColl.Add("R8", (r8 != 0 ? ">" + r7.ToString() + " & <= " + r3.ToString() + " Days" : (r7 == 0 ? "" : " > " + r7.ToString())));
                        paraColl.Add("R9", (r9 != 0 ? ">" + r8.ToString() + " & <= " + r3.ToString() + " Days" : (r8 == 0 ? "" : " > " + r8.ToString())));
                        paraColl.Add("R10", (r10 != 0 ? ">" + r9.ToString() + " & <= " + r3.ToString() + " Days" : (r9 == 0 ? "" : " > " + r9.ToString())));
                        paraColl.Add("RV1", r1.ToString());
                        paraColl.Add("RV2", r2.ToString());
                        paraColl.Add("RV3", r3.ToString());
                        paraColl.Add("RV4", r4.ToString());
                        paraColl.Add("RV5", r5.ToString());
                        paraColl.Add("RV6", r6.ToString());
                        paraColl.Add("RV7", r7.ToString());
                        paraColl.Add("RV8", r8.ToString());
                        paraColl.Add("RV9", r9.ToString());
                        paraColl.Add("RV10", r10.ToString());


                       
                        return Json(resVal, new JsonSerializerSettings
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

                    return BadRequest("No Data Found");
                }

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }

        }


        // POST api/GetPartyAgeing
        /// <summary>
        ///  Get Party Outstanding Ageing Reports
        ///  dateFrom as Date
        ///  dateTo as Date
        ///  ledgerGroupId as Int        
        ///  dayRange as string like 15,30,45,60        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.PartyAgeingReport>))]
        public IHttpActionResult GetPartyAgeing([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.PartyAgeingReportCollections dataColl = new Dynamic.ReportEntity.Inventory.PartyAgeingReportCollections();
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

                    string dayRange = "";
                    if (para.ContainsKey("dayRange"))
                        dayRange = Convert.ToString(para["dayRange"]);

                    int r1 = 0, r2 = 0, r3 = 0, r4 = 0, r5 = 0, r6 = 0, r7 = 0, r8 = 0, r9 = 0, r10 = 0;

                    string[] dayColl = dayRange.Split(',');
                    int ind = 0;
                    foreach (var v in dayColl)
                    {
                        switch (ind)
                        {
                            case 0:
                                int.TryParse(v, out r1);
                                break;
                            case 1:
                                int.TryParse(v, out r2);
                                break;
                            case 2:
                                int.TryParse(v, out r3);
                                break;
                            case 3:
                                int.TryParse(v, out r4);
                                break;
                            case 4:
                                int.TryParse(v, out r5);
                                break;
                            case 5:
                                int.TryParse(v, out r6);
                                break;
                            case 6:
                                int.TryParse(v, out r7);
                                break;
                            case 7:
                                int.TryParse(v, out r8);
                                break;
                            case 8:
                                int.TryParse(v, out r9);
                                break;
                            case 9:
                                int.TryParse(v, out r10);
                                break;
                        }
                    }

                    dataColl = new Dynamic.Reporting.Inventory.PartyAgeingReport(hostName, dbName).getPartyAgeingReport(UserId, ledgerGroupId, dateFrom, dateTo, r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, false, false, false);
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        // POST api/GetCreditLimitExpiredParty
        /// <summary>
        ///  Get Credit Limit Amount Exceed Party List
        ///  dateFrom as Date
        ///  dateTo as Date          
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.PartyAgeingReport>))]
        public IHttpActionResult GetCreditLimitExpiredParty([FromBody] JObject para)
        {
            Dynamic.ReportEntity.Inventory.CreditLimitExpiredCollections dataColl = new Dynamic.ReportEntity.Inventory.CreditLimitExpiredCollections();
            try
            {
                if (para == null)
                {
                    return BadRequest("No form data found");
                }
                else
                {
                    DateTime? dateFrom = null;
                    if (para.ContainsKey("dateFrom"))
                        dateFrom = Convert.ToDateTime(para["dateFrom"]);

                    DateTime? dateTo = null;
                    if (para.ContainsKey("dateTo"))
                        dateTo = Convert.ToDateTime(para["dateTo"]);

                    dataColl = new Dynamic.Reporting.Inventory.PartyAgeingReport(hostName, dbName).getCreditLimitExpiredPartyList(UserId, dateFrom, dateTo);
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

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }

        // POST api/IRDSalesInvoice
        /// <summary>
        ///  Push Sales Invoice TO IRD
        ///  tranId as int        
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Dynamic.ReportEntity.Inventory.PartyAgeingReport>))]
        public IHttpActionResult IRDSalesInvoice([FromBody] JObject para)
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

                    bool isRealTime = false;
                    if (para.ContainsKey("isRealTime"))
                        isRealTime = Convert.ToBoolean(para["isRealTime"]);

                    var salesInvoice = new Dynamic.BusinessLogic.Inventory.Transaction.SalesInvoice(hostName, dbName).getInvoiceDetailsForIRD(UserId, tranId);
                    if (salesInvoice.IsSuccess)
                    {
                        salesInvoice.isrealtime = isRealTime;
                    }

                    resVal=new DynamicAccounting.Global.IRDAPI(hostName, dbName, isRealTime).callSalesAPI(salesInvoice, tranId);
                    
                    return Json(resVal, new JsonSerializerSettings
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

    }
}
