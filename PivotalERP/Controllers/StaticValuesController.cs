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

namespace AcademicERP.Controllers
{
    public class StaticValuesController : APIBaseController
    {
        // GET api/GetLedgerSearchOption
        /// <summary>
        ///  Get Ledger Search Options 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLedgerSearchOptions()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.APIEnitity.Account.LEDGER_SEARCHOPTIONS)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetLedgerTypes
        /// <summary>
        ///  Get Ledger Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetLedgerTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.APIEnitity.Account.LEDGERTYPES)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetCostCenterSearchOption
        /// <summary>
        ///  Get CostCenter(SubLedger) Search Options 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetCostCenterSearchOptions()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.APIEnitity.Account.COSTCENTER_SEARCHOPTIONS)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {                   
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetProductSearchOption
        /// <summary>
        ///  Get Product Search Options 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetProductSearchOptions()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.APIEnitity.Inventory.PRODUCT_SEARCHOPTIONS)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetAreaTypes
        /// <summary>
        ///  Get Area Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetAreaTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.AreaTypes)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetVoucherType
        /// <summary>
        ///  Get Voucher Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetVoucherTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.VoucherTypes)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {                 
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetDrCr
        /// <summary>
        ///  Get DR CR
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetDrCr()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.DRCR)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetDashboardTypes
        /// <summary>
        ///  Get Dashboard Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetDashboardTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.Dashboard.BE.ReportTypes)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetNotificationTypes
        /// <summary>
        ///  Get Notification Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetNotificationTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Global.NOTIFICATION_ENTITY)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude=true,
                       IncludeProperties = new List<string>
                                        {
                                            "id","text"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetAttendanceType
        /// <summary>
        ///  Get AttendanceType 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetAttendanceType()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Attendance.ATTENDANCES)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "id","text"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetInOutModes
        /// <summary>
        ///  Get AttendanceType 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetInOutModes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Attendance.INOUTMODES)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "id","text"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetOnlinePlatform
        /// <summary>
        ///  Get OnlinePlatform 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetOnlinePlatform()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Global.ONLINE_PLATFORMS)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "id","text"
                                        }
                    }
                });

            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetRemarksFor
        /// <summary>
        ///  Get Student Remarks For
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetRemarksFor()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Academic.Transaction.REMARKSFOR)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/GetNatureOfLG
        /// <summary>
        ///  Get Nature of Ledger Group
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetNatureOfLG()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.NatureOfGroups)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET api/GetBOMProductType
        /// <summary>
        ///  Get B.O.M. Product Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Dynamic.APIEnitity.Common>))]
        public IHttpActionResult GetBOMProductType()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Inventory.BOMProductTypes)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

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
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }
    }
}
