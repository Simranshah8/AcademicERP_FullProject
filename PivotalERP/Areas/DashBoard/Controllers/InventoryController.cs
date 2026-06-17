using Dynamic.BusinessEntity.Account;
using Dynamic.BusinessEntity.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
namespace PivotalERP.Areas.DashBoard.Controllers
{
    public class InventoryController : PivotalERP.Controllers.BaseController
    {
        #region "Sales Dashboard"
        public ActionResult Sales()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetSalesData()
        {
            Dynamic.Dashboard.BE.Common common = new Dynamic.Dashboard.BE.Common();
            try
            {
                Dynamic.Dashboard.BE.CommonPara para = new Dynamic.Dashboard.BE.CommonPara();
                para.UserId = User.UserId;
                para.DateFrom = null;
                para.DateTo = null;
                para.ReportTypes = "19,20,26";
                common = new Dynamic.Dashboard.DA.CommonDB(User.HostName, User.DBName).getCommon(para);

                return new JsonNetResult() { Data = common, TotalCount = 1, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };
            }
            catch (Exception ee)
            {
                common.IsSuccess = false;
                common.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetSalesDashBoardDetails19(int OrderBy)
        {
            Dynamic.Dashboard.BE.Common common = new Dynamic.Dashboard.BE.Common();
            try
            {
                Dynamic.Dashboard.BE.CommonPara para = new Dynamic.Dashboard.BE.CommonPara();
                para.UserId = User.UserId;
                para.DateFrom = null;
                para.DateTo = null;
                para.ReportTypes = "19";
                para.OrderBy = OrderBy;
                common = new Dynamic.Dashboard.DA.CommonDB(User.HostName, User.DBName).getCommon(para);

                return new JsonNetResult() { Data = common, TotalCount = 1, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };
            }
            catch (Exception ee)
            {
                common.IsSuccess = false;
                common.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetSalesDashBoardDetails20(int OrderBy)
        {
            Dynamic.Dashboard.BE.Common common = new Dynamic.Dashboard.BE.Common();
            try
            {
                Dynamic.Dashboard.BE.CommonPara para = new Dynamic.Dashboard.BE.CommonPara();
                para.UserId = User.UserId;
                para.DateFrom = null;
                para.DateTo = null;
                para.ReportTypes = "20";
                para.OrderBy = OrderBy;
                common = new Dynamic.Dashboard.DA.CommonDB(User.HostName, User.DBName).getCommon(para);

                return new JsonNetResult() { Data = common, TotalCount = 1, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };
            }
            catch (Exception ee)
            {
                common.IsSuccess = false;
                common.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };

        }

        public ActionResult ProductBrandSummary(int ProductBrandId, int UnitId)
        {
            ViewBag.ProductBrandId = ProductBrandId;
            ViewBag.UnitId = UnitId;
            return View();
        }

        [HttpPost]
        public JsonNetResult GetProductBrandWise(int ProductBrandId, int Top, int? UnitId)
        {
            Dynamic.Dashboard.BE.ProductBrandWiseCollections dataColl = new Dynamic.Dashboard.BE.ProductBrandWiseCollections();
            try
            {
                dataColl = new Dynamic.Dashboard.DA.SalesDB(User.HostName, User.DBName).getProductBrandWise(User.UserId, ProductBrandId, null, null, 0, 0, 0, Top, UnitId);

                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult LastMonthSalesAnalysis()
        {

            return View();
        }

        [HttpPost]
        public JsonNetResult GetSalesDashBoardDetails1()
        {
            Dynamic.Dashboard.BE.Sales sales = new Dynamic.Dashboard.BE.Sales();
            try
            {
                sales = new Dynamic.Dashboard.DA.SalesDB(User.HostName, User.DBName).getSalesDashBoardDetails1(User.UserId, null, null, 0, 0, 0);

                return new JsonNetResult() { Data = sales, TotalCount = 1, IsSuccess = sales.IsSuccess, ResponseMSG = sales.ResponseMSG };
            }
            catch (Exception ee)
            {
                sales.IsSuccess = false;
                sales.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = sales.IsSuccess, ResponseMSG = sales.ResponseMSG };
        }

        public ActionResult LastMonthSalesAnalysisQty()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetSalesDashBoardDetails2()
        {
            Dynamic.Dashboard.BE.Sales sales = new Dynamic.Dashboard.BE.Sales();
            try
            {
                sales = new Dynamic.Dashboard.DA.SalesDB(User.HostName, User.DBName).getSalesDashBoardDetails2(User.UserId, null, null, 0, 0, 0);

                return new JsonNetResult() { Data = sales, TotalCount = 1, IsSuccess = sales.IsSuccess, ResponseMSG = sales.ResponseMSG };
            }
            catch (Exception ee)
            {
                sales.IsSuccess = false;
                sales.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = sales.IsSuccess, ResponseMSG = sales.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSalesProjectVSSales(int? UnitId,int? AgentId, DateTime? DateFrom,DateTime? DateTo)
        {
            Dynamic.Dashboard.BE.SalesProjectionVSSalesCollections dataColl = new Dynamic.Dashboard.BE.SalesProjectionVSSalesCollections();
            try
            {
                Dynamic.Dashboard.BE.CommonPara para = new Dynamic.Dashboard.BE.CommonPara();
                para.UserId = User.UserId;
                para.DateFrom = DateFrom;
                para.DateTo = DateTo;
                para.ReportTypes = "26";
                para.UnitId = UnitId;
                para.AgentId = AgentId;
                var data = new Dynamic.Dashboard.DA.CommonDB(User.HostName, User.DBName).getCommon(para);
                dataColl = data.ProjectionVSSalesColl;

                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = data.IsSuccess, ResponseMSG = data.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult SalesProjection(int? unitId,int? agentId,DateTime? dateFrom,DateTime? dateTo)
        {            
            ViewBag.UnitId = unitId;
            ViewBag.AgentId = agentId;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            return View();
        }

        [HttpPost]
        public JsonNetResult GetSalesProjection(int? unitId, int? agentId, DateTime? dateFrom, DateTime? dateTo)
        {
            Dynamic.Dashboard.BE.SalesProjectionVSSalesCollections dataCol = new Dynamic.Dashboard.BE.SalesProjectionVSSalesCollections();
            try
            {
                dataCol = new Dynamic.Dashboard.DA.SalesDB(User.HostName, User.DBName).getSalesDashBoardDetails26(User.UserId,dateFrom,dateTo,0,0,0,agentId,unitId);

                return new JsonNetResult() { Data = dataCol, TotalCount = dataCol.Count, IsSuccess = dataCol.IsSuccess, ResponseMSG = dataCol.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataCol.IsSuccess = false;
                dataCol.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataCol.IsSuccess, ResponseMSG = dataCol.ResponseMSG };
        }
        #endregion

        #region "Inventory Summary"

        public ActionResult InventorySummary()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetInventorySummary()
        {
            Dynamic.Dashboard.BE.Common common = new Dynamic.Dashboard.BE.Common();
            try
            {
                Dynamic.Dashboard.BE.CommonPara para = new Dynamic.Dashboard.BE.CommonPara();
                para.UserId = User.UserId;
                para.DateFrom = null;
                para.DateTo = null;
                para.ReportTypes = "21,22,23,24,25,27";
                common = new Dynamic.Dashboard.DA.CommonDB(User.HostName, User.DBName).getCommon(para);

                return new JsonNetResult() { Data = common, TotalCount = 1, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };
            }
            catch (Exception ee)
            {
                common.IsSuccess = false;
                common.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = common.IsSuccess, ResponseMSG = common.ResponseMSG };

        }

        #endregion

    }
}