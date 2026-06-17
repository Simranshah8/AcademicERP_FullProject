using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicLib.BE.Global;
using Dynamic.BusinessEntity.Global;
namespace PivotalERP.Areas.Account.Controllers
{
    public class ReportingController : PivotalERP.Controllers.BaseController
    {
        // GET: Account/Reporting

        public ActionResult DailyVoucher()
        {
            return View();
        }

        #region "DayBook"


        public ActionResult DayBook()
        {
            return View();
        }


        public JsonNetResult GetDayBook(DateTime dateFrom, DateTime dateTo, int VoucherType, bool isPost, int branchId, int For)
        {
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;

            Dynamic.ReportEntity.Account.DayBookCollections dataColl = new Dynamic.ReportEntity.Account.DayBookCollections();

            if (For==2)
                dataColl=new Dynamic.Reporting.Account.DayBook(User.HostName, User.DBName).getDayBook(dateFrom, dateTo, VoucherType, false, branchId, User.UserId);
            else if(For==3)
                dataColl = new Dynamic.Reporting.Account.DayBook(User.HostName, User.DBName).getCancelDayBook(User.UserId, dateFrom, dateTo, VoucherType);
            else
                dataColl = new Dynamic.Reporting.Account.DayBook(User.HostName, User.DBName).getDayBook(dateFrom, dateTo, VoucherType, true, branchId, User.UserId);

            return new JsonNetResult() { Data = dataColl };
        }

        [HttpPost]
        public JsonNetResult PrintDayBook()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.DayBook> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.DayBook>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonResult PostPendingVoucher(Dynamic.ReportEntity.Account.DayBookCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;

            try
            {
                Dynamic.Reporting.Account.DayBook dayBook = new Dynamic.Reporting.Account.DayBook(User.HostName, User.DBName);
             
                Dynamic.ReportEntity.Account.DayBookCollections tmpDataColl = new Dynamic.ReportEntity.Account.DayBookCollections();
                foreach (Dynamic.ReportEntity.Account.DayBook beData in dataColl)
                {
                    //if (beData.VoucherDate.Year > 1900)
                    //{
                    //    dt.GetNepaliDate(beData.VoucherDate);
                    //    beData.NY = dt.NY;
                    //    beData.NM = dt.NM;
                    //    beData.ND = dt.ND;
                    //    tmpDataColl.Add(beData);
                    //}

                }

                if (tmpDataColl.Count > 0)
                {
                    resVal=dayBook.PostPendingVoucher(tmpDataColl, User.UserId);
                    //resVal.IsSuccess = true;
                    //resVal.ResponseMSG = "Selected Voucher Post Success";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResultWithEnum() { Data = resVal };
        }

        #endregion


        [HttpGet]
        public JsonNetResult GetAllVoucherList()
        {
            Dynamic.BusinessEntity.Account.VoucherModeCollections dataColl = new Dynamic.BusinessEntity.Account.VoucherModeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.VoucherMode(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult Ledger()
        {
            ViewBag.Title = "Ledger List";
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.ListOfLedger);
            return View();
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllLedger()
        {
            Dynamic.ReportEntity.Account.LedgerCollections dataColl = new Dynamic.ReportEntity.Account.LedgerCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.Ledger(User.HostName, User.DBName).getAllLedger(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult LedgerContactList()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResultWithEnum GetAllLedgerContactList()
        {
            Dynamic.ReportEntity.Account.LedgerContactListCollections dataColl = new Dynamic.ReportEntity.Account.LedgerContactListCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.LedgerContactList(User.HostName, User.DBName).getAllLedgerContactList(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        public ActionResult LedgerOpening()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResultWithEnum GetAllLedgerOpening()
        {
            Dynamic.ReportEntity.Account.LedgerOpeningDetailsCollections dataColl = new Dynamic.ReportEntity.Account.LedgerOpeningDetailsCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.LedgerOpeningDetails(User.HostName, User.DBName).getLedgerOpeningDetails(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult SubLedger()
        {

            return View();
        }

        [HttpGet]
        public JsonNetResultWithEnum GetAllSubLedger()
        {
            Dynamic.BusinessEntity.Account.CostCenterCollections dataColl = new Dynamic.BusinessEntity.Account.CostCenterCollections();
            try
            {

                dataColl = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult Salesman()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResultWithEnum GetAllSalesman()
        {
            Dynamic.BusinessEntity.Account.AgentCollections dataColl = new Dynamic.BusinessEntity.Account.AgentCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult Area()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResultWithEnum GetAllArea()
        {
            Dynamic.BusinessEntity.Account.AreaMasterCollections dataColl = new Dynamic.BusinessEntity.Account.AreaMasterCollections();
            try
            {

                dataColl = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult Voucher()
        {
            return View();
        }

        [HttpGet]
        public JsonNetResultWithEnum GetAllVoucher()
        {

            Dynamic.BusinessEntity.Account.VoucherModeCollections dataColl = new Dynamic.BusinessEntity.Account.VoucherModeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.VoucherMode(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult BankGuarantee()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResult GetAllBankGuarantee()
        {
            Dynamic.BusinessEntity.Account.BGDetailsCollections dataColl = new Dynamic.BusinessEntity.Account.BGDetailsCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.BGDetails(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult PostDatedCheque()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResult GetAllPostDatedCheque()
        {
            Dynamic.BusinessEntity.Account.PDCCollections dataColl = new Dynamic.BusinessEntity.Account.PDCCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult OpenDatedCheque()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResult GetAllOpenDatedCheque()
        {
            Dynamic.BusinessEntity.Account.PDCCollections dataColl = new Dynamic.BusinessEntity.Account.PDCCollections();
            try
            {

                dataColl = new Dynamic.BusinessLogic.Account.ODCDetails(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        
        [PermissionsAttribute(Actions.View, (int)ENTITIES.TrailBalance, false)]
        public ActionResult TrailBalanceLedgerwise()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult PrintTrailBalance()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.TrailBalance> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.TrailBalance>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTBLedgerWise()
        {
            Newtonsoft.Json.Linq.JObject para=DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalanceCollections dataColl = new Dynamic.ReportEntity.Account.TrailBalanceCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";

                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                List<int> ignoreList = new List<int>();
                ignoreList.Add(1);
                dataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(User.HostName, User.DBName).getTrailBalanceLedgerWise(dateFrom, dateTo, branchId, User.UserId, ignoreList, branchIdColl);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        
        [PermissionsAttribute(Actions.View, (int)ENTITIES.TrailBalance, false)]
        public ActionResult TrailBalanceGroupwise()
        {
            return View();
        }

        private Dynamic.BusinessEntity.Account.LedgerGroupCollections LedgerGroupColl = null;
        private Dynamic.ReportEntity.Account.TrailBalanceCollections TrailBalanceGroupColl = null;
        [HttpPost]
        public JsonNetResult GetTBGroupWise()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                int groupId = 1;
                string branchIdColl = "";
                bool showZeroBalance = true;
                bool showCostCenter = false;
                bool ShowAsList = false;
                int? brandId = null;

                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("groupId") && para["groupId"] != null && !string.IsNullOrEmpty(para["groupId"].ToString()))
                    groupId = Convert.ToInt32(para["groupId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                if (para.ContainsKey("showZeroBalance") && para["showZeroBalance"] != null && !string.IsNullOrEmpty(para["showZeroBalance"].ToString()))
                    showZeroBalance = Convert.ToBoolean(para["showZeroBalance"]);

                if (para.ContainsKey("showCostCenter") && para["showCostCenter"] != null && !string.IsNullOrEmpty(para["showCostCenter"].ToString()))
                    showCostCenter = Convert.ToBoolean(para["showCostCenter"]);

                if (para.ContainsKey("showAsList") && para["showAsList"] != null && !string.IsNullOrEmpty(para["showAsList"].ToString()))
                    ShowAsList = Convert.ToBoolean(para["showAsList"]);

                if (para.ContainsKey("brandId") && para["brandId"] != null && !string.IsNullOrEmpty(para["brandId"].ToString()))
                    brandId = Convert.ToInt32(para["brandId"]);

                if (ShowAsList)
                    showCostCenter = false;

                List<int> ignoreList = new List<int>();
                ignoreList.Add(1);

                bool showOpeningClosingStock = new Dynamic.DataAccess.Setup.AccountConfigurationDB(User.HostName, User.DBName).getAccountConfiguration(User.UserId).ShowAutoClosingStock;

                if (groupId > 1)
                    showOpeningClosingStock = false;

                TrailBalanceGroupColl = new Dynamic.Reporting.Account.TrailBalanceSheet(User.HostName, User.DBName).getTrailBalance(dateFrom, dateTo, groupId, branchIdColl, User.UserId, "", showCostCenter, brandId, showOpeningClosingStock);

                if (ShowAsList)
                {
                    return new JsonNetResult() { Data = TrailBalanceGroupColl.OrderBy(p1=>p1.GroupingName).ThenBy(p2=>p2.Particulars), TotalCount = TrailBalanceGroupColl.Count, IsSuccess = TrailBalanceGroupColl.IsSuccess, ResponseMSG = TrailBalanceGroupColl.ResponseMSG };
                }

                LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroup(User.UserId);
                var rootGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == groupId);
                rootNote.RowType = Dynamic.ReportEntity.Account.TB_ROWTYPES.LEDGERGROUP;
                rootNote.IsLedgerGroup = true;
                rootNote.LedgerGroupId = groupId;
                rootNote.LedgerGroupName = rootGroup.Name;

                AddChieldNode(rootNote, showZeroBalance);

                System.Collections.ArrayList root = new System.Collections.ArrayList();
                root.Add(rootNote);

                if (groupId == 1)
                {
                    double diffAmt = TrailBalanceGroupColl.Sum(p1 => p1.Opening);
                    if (diffAmt < 0)
                    {
                        double amt = Math.Abs(diffAmt);
                        Dynamic.ReportEntity.Account.TrailBalance diffNote = new Dynamic.ReportEntity.Account.TrailBalance();
                        diffNote.LedgerGroupId = -1;
                        diffNote.Closing = diffAmt;
                        diffNote.ClosingDr = amt;
                        diffNote.Opening = amt;
                        diffNote.OpeningDr = amt;
                        diffNote.TotalOpeningDr = amt;
                        diffNote.LedgerName = "Diff. In Opening Balance";
                        root.Add(diffNote);
                    }
                    else if (diffAmt > 0)
                    {
                        double amt = Math.Abs(diffAmt);
                        Dynamic.ReportEntity.Account.TrailBalance diffNote = new Dynamic.ReportEntity.Account.TrailBalance();
                        diffNote.LedgerGroupId = -1;
                        diffNote.Closing = diffAmt;
                        diffNote.ClosingCr = amt;
                        diffNote.Opening = -amt;
                        diffNote.OpeningCr = amt;
                        diffNote.TotalOpeningCr = amt;
                        diffNote.LedgerName = "Diff. In Opening Balance";
                        root.Add(diffNote);
                    }
                }

                return new JsonNetResult() { Data = root, TotalCount = 1, IsSuccess = true, ResponseMSG = TrailBalanceGroupColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }
        private void AddChieldNode(Dynamic.ReportEntity.Account.TrailBalance beData, bool showZeroBalance)
        {
            var query = from lg in LedgerGroupColl
                        where lg.ParentGroupId == beData.LedgerGroupId
                        select lg;

            foreach (var v in query)
            {
                Dynamic.ReportEntity.Account.TrailBalance node = new Dynamic.ReportEntity.Account.TrailBalance();
                node.LedgerGroupId = v.LedgerGroupId;
                node.TotalSpace += beData.TotalSpace + 3;
                node.IsLedgerGroup = true;
                node.LedgerGroupName = v.Name;
                beData.ChieldColl.Add(node);
                AddChieldNode(node, showZeroBalance);
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

            var led = from cd in TrailBalanceGroupColl
                      where cd.LedgerGroupId == beData.LedgerGroupId
                      select cd;

            foreach (var v in led)
            {
                bool added = false;
                if (showZeroBalance)
                    added = true;
                else
                {
                    if (v.Opening != 0 || v.TransactionDr != 0 || v.TransactionCr != 0)
                        added = true;
                }


                if (added)
                {
                    beData.BranchWiseColl = v.BranchWiseColl;
                    beData.NatureOfGroup = v.NatureOfGroup;
                    beData.RowType = v.RowType;
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
                }

                if (v.Opening != 0 || v.TransactionDr != 0 || v.TransactionCr != 0)
                    beData.ChieldColl.Add(v);
            }

        }

        public ActionResult TrailBalanceBranchwise()
        {
            return View();
        }
        public ActionResult TrailBalanceCostCenter()
        {
            return View();
        }
        public ActionResult ProfitAndLoss()
        {
            return View();
        }

        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections IncomeColl;
        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections ExpensesColl;
        public JsonNetResult GetPL()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";
                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                double openingStock = 0, closingStock = 0, closingStockOpening = 0;
                LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroup(User.UserId);
                System.Collections.ArrayList data = new Dynamic.Reporting.Account.ProfitAndLoss(User.HostName, User.DBName).getProfitAndLoss(User.UserId,null,"", dateFrom, dateTo, "",true,false, ref openingStock, ref closingStock, ref closingStockOpening);
                IncomeColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[0];
                ExpensesColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[1];

                System.Collections.ArrayList finalDataColl = new System.Collections.ArrayList();
                if (closingStock != 0 || openingStock != 0)
                {
                    Dynamic.ReportEntity.Account.ProfitAndLosss closingStockNode = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    closingStockNode.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.ClosingStock;
                    closingStockNode.Particulars = " Closing Stock";
                    closingStockNode.IsLedgerGroup = true;
                    //closingStockNode.OpeningAmt = openingStock;
                    //closingStockNode.TransactionAmt = closingStock;
                    //closingStockNode.ClosingAmt = closingStock+openingStock ;
                    closingStockNode.ClosingAmt = closingStock;
                    finalDataColl.Add(closingStockNode);
                }

                Dynamic.BusinessEntity.Account.LedgerGroup salesLedgerGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 39);
                Dynamic.ReportEntity.Account.ProfitAndLosss salesAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                salesAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAccount;
                salesAc.Particulars = salesLedgerGroup.Name;
                salesAc.IsLedgerGroup = true;
                salesAc.LedgerGroupId = salesLedgerGroup.LedgerGroupId;

                AddChieldNodeOfIncome(salesAc);
                finalDataColl.Add(salesAc);

                Dynamic.BusinessEntity.Account.LedgerGroup directIncomeGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 40);
                Dynamic.ReportEntity.Account.ProfitAndLosss directIncomeAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                directIncomeAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectIncome;
                directIncomeAc.Particulars = directIncomeGroup.Name;
                directIncomeAc.IsLedgerGroup = true;
                directIncomeAc.LedgerGroupId = directIncomeGroup.LedgerGroupId;

                AddChieldNodeOfIncome(directIncomeAc);
                finalDataColl.Add(directIncomeAc);

                Dynamic.ReportEntity.Account.ProfitAndLosss totalIncome = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                totalIncome.Particulars = " TOTAL ";

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
                        finalDataColl.Add(otherIncomeAc);

                        totalIncome.OpeningAmt += otherIncomeAc.OpeningAmt;
                        totalIncome.TransactionAmt += otherIncomeAc.TransactionAmt;
                        totalIncome.ClosingAmt += otherIncomeAc.ClosingAmt;
                    }
                }


                //totalIncome.OpeningAmt += salesAc.OpeningAmt + directIncomeAc.OpeningAmt+openingStock;
                //totalIncome.TransactionAmt += salesAc.TransactionAmt + directIncomeAc.TransactionAmt + closingStock;
                //totalIncome.ClosingAmt += salesAc.ClosingAmt + directIncomeAc.ClosingAmt + closingStock+openingStock;
                totalIncome.OpeningAmt += salesAc.OpeningAmt + directIncomeAc.OpeningAmt;
                totalIncome.TransactionAmt += salesAc.TransactionAmt + directIncomeAc.TransactionAmt;
                totalIncome.ClosingAmt += salesAc.ClosingAmt + directIncomeAc.ClosingAmt + closingStock;
                totalIncome.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.SalesAndDirectIncomeTotal;

                Dynamic.ReportEntity.Account.ProfitAndLosss blankData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                blankData.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.Others;
                finalDataColl.Add(blankData);
                finalDataColl.Add(totalIncome);
                finalDataColl.Add(blankData);

                if (openingStock != 0)
                {
                    Dynamic.ReportEntity.Account.ProfitAndLosss openingStockNode = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    openingStockNode.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.OpeningStock;
                    openingStockNode.Particulars = " Opening Stock";
                    openingStockNode.IsLedgerGroup = true;
                    //openingStockNode.OpeningAmt = openingStock;
                    openingStockNode.ClosingAmt = openingStock;
                    finalDataColl.Add(openingStockNode);
                }

                Dynamic.BusinessEntity.Account.LedgerGroup purchaseLedgerGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 42);
                Dynamic.ReportEntity.Account.ProfitAndLosss purchaseAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                purchaseAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAccount;
                purchaseAc.Particulars = purchaseLedgerGroup.Name;
                purchaseAc.IsLedgerGroup = true;
                purchaseAc.LedgerGroupId = purchaseLedgerGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(purchaseAc);
                finalDataColl.Add(purchaseAc);


                Dynamic.BusinessEntity.Account.LedgerGroup directExpGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 43);
                Dynamic.ReportEntity.Account.ProfitAndLosss directExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                directExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.DirectExpenses;
                directExpAc.Particulars = directExpGroup.Name;
                directExpAc.IsLedgerGroup = true;
                directExpAc.LedgerGroupId = directExpGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(directExpAc);
                finalDataColl.Add(directExpAc);


                Dynamic.ReportEntity.Account.ProfitAndLosss totalExp = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                totalExp.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.PurchaseAndDirectExpensesTotal;
                totalExp.Particulars = " TOTAL ";
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
                        finalDataColl.Add(otherIncomeAc);

                        totalExp.OpeningAmt += otherIncomeAc.OpeningAmt;
                        totalExp.TransactionAmt += otherIncomeAc.TransactionAmt;
                        totalExp.ClosingAmt += otherIncomeAc.ClosingAmt;
                    }
                }



                //totalExp.OpeningAmt += purchaseAc.OpeningAmt + directExpAc.OpeningAmt+openingStock;
                //totalExp.TransactionAmt += purchaseAc.TransactionAmt + directExpAc.TransactionAmt ;
                //totalExp.ClosingAmt += purchaseAc.ClosingAmt + directExpAc.ClosingAmt +openingStock;                
                totalExp.OpeningAmt += purchaseAc.OpeningAmt + directExpAc.OpeningAmt;
                totalExp.TransactionAmt += purchaseAc.TransactionAmt + directExpAc.TransactionAmt;
                totalExp.ClosingAmt += purchaseAc.ClosingAmt + directExpAc.ClosingAmt + openingStock;
                finalDataColl.Add(blankData);
                finalDataColl.Add(totalExp);
                finalDataColl.Add(blankData);

                Dynamic.ReportEntity.Account.ProfitAndLosss grossLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                grossLostAndProfit.OpeningAmt = totalIncome.OpeningAmt - totalExp.OpeningAmt;
                grossLostAndProfit.TransactionAmt = totalIncome.TransactionAmt - totalExp.TransactionAmt;
                grossLostAndProfit.ClosingAmt = totalIncome.ClosingAmt - totalExp.ClosingAmt;
                grossLostAndProfit.Particulars = (grossLostAndProfit.ClosingAmt > 0 ? " Gross Profit " : " Gross Loss ");
                grossLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.GrossProfitAndLoss;
                if (grossLostAndProfit.ClosingAmt != 0)
                {
                    finalDataColl.Add(grossLostAndProfit);
                    finalDataColl.Add(blankData);
                }

                Dynamic.BusinessEntity.Account.LedgerGroup indirectIncGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 41);
                Dynamic.ReportEntity.Account.ProfitAndLosss indirectIncAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                indirectIncAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectIncome;
                indirectIncAc.Particulars = indirectIncGroup.Name;
                indirectIncAc.IsLedgerGroup = true;
                indirectIncAc.LedgerGroupId = indirectIncGroup.LedgerGroupId;

                AddChieldNodeOfIncome(indirectIncAc);
                finalDataColl.Add(indirectIncAc);

                Dynamic.BusinessEntity.Account.LedgerGroup indirectExpGroup = LedgerGroupColl.Find(p1 => p1.LedgerGroupId == 44);
                Dynamic.ReportEntity.Account.ProfitAndLosss indirectExpAc = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                indirectExpAc.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.IndirectExpenses;
                indirectExpAc.Particulars = indirectExpGroup.Name;
                indirectExpAc.IsLedgerGroup = true;
                indirectExpAc.LedgerGroupId = indirectExpGroup.LedgerGroupId;

                AddChieldNodeOfExpenses(indirectExpAc);
                finalDataColl.Add(indirectExpAc);
                finalDataColl.Add(blankData);

                Dynamic.ReportEntity.Account.ProfitAndLosss netLostAndProfit = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                netLostAndProfit.OpeningAmt = grossLostAndProfit.OpeningAmt + indirectIncAc.OpeningAmt - indirectExpAc.OpeningAmt;
                netLostAndProfit.TransactionAmt = grossLostAndProfit.TransactionAmt + indirectIncAc.TransactionAmt - indirectExpAc.TransactionAmt;
                netLostAndProfit.ClosingAmt = grossLostAndProfit.ClosingAmt + indirectIncAc.ClosingAmt - indirectExpAc.ClosingAmt;
                netLostAndProfit.Particulars = (netLostAndProfit.ClosingAmt > 0 ? " Net Profit " : " Net Loss ");
                netLostAndProfit.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.NetProfitAndLoss;
                if (netLostAndProfit.ClosingAmt != 0)
                {
                    finalDataColl.Add(netLostAndProfit);
                }

                return new JsonNetResult() { Data = finalDataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
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

        public ActionResult ProfitAndLossAsT()
        {
            return View();
        }
        public JsonNetResult GetPLAsT()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";
                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                double openingStock = 0, closingStock = 0, closingStockOpening = 0;
                LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroup(User.UserId);
                System.Collections.ArrayList data = new Dynamic.Reporting.Account.ProfitAndLoss(User.HostName, User.DBName).getProfitAndLoss(User.UserId, null, "", dateFrom, dateTo, "",true,false, ref openingStock, ref closingStock, ref closingStockOpening);
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

                return new JsonNetResult() { Data = finalDataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }
        public ActionResult BalanceSheet()
        {
            return View();
        }

        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections AssetsColl;
        private Dynamic.ReportEntity.Account.ProfitAndLosssCollections LiabilitiesColl;
        public JsonNetResult GetBS()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";
                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);
                
                LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroup(User.UserId);
                double netProfitAndLossOpening = 0, netProfitAndLossTransaction = 0;
                Dynamic.Reporting.Account.ProfitAndLoss trail = new Dynamic.Reporting.Account.ProfitAndLoss(User.HostName, User.DBName);
                System.Collections.ArrayList data = trail.getBalanceSheet(User.UserId,null, "", dateFrom,dateTo,"",true,false,ref netProfitAndLossOpening, ref netProfitAndLossTransaction);
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


                Dynamic.ReportEntity.Account.ProfitAndLosss netProfit = GetProfitAndLossAmount(dateFrom,dateTo);

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

                return new JsonNetResult() { Data = finalDataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
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
        private Dynamic.ReportEntity.Account.ProfitAndLosss GetProfitAndLossAmount(DateTime fromDate,DateTime toDate)
        {
            try
            {

                LedgerGroupColl1 = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName,User.DBName).getAllLedgerGroupOfProfitAndLoss(User.UserId);

                double openingStock = 0, closingStock = 0, closingStockOpening = 0;
                Dynamic.Reporting.Account.ProfitAndLoss trail = new Dynamic.Reporting.Account.ProfitAndLoss(User.HostName, User.DBName);
                System.Collections.ArrayList data = trail.getProfitAndLoss(User.UserId, null, "", fromDate, toDate, "",true,false, ref openingStock, ref closingStock, ref closingStockOpening);
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
        public ActionResult BalanceSheetAsT()
        {
            return View();
        }
        public JsonNetResult GetBSAsT()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalance rootNote = new Dynamic.ReportEntity.Account.TrailBalance();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";
                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                LedgerGroupColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroup(User.UserId);
                double netProfitAndLossOpening = 0, netProfitAndLossTransaction = 0;
                Dynamic.Reporting.Account.ProfitAndLoss trail = new Dynamic.Reporting.Account.ProfitAndLoss(User.HostName, User.DBName);
                System.Collections.ArrayList data = trail.getBalanceSheet(User.UserId, null, "", dateFrom, dateTo, "",true,false, ref netProfitAndLossOpening, ref netProfitAndLossTransaction);
                AssetsColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[0];
                LiabilitiesColl = (Dynamic.ReportEntity.Account.ProfitAndLosssCollections)data[1];

                Dynamic.ReportEntity.Account.ProfitAndLosss blankData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                blankData.TotalType = Dynamic.ReportEntity.Account.ProfitAndLossTotalTypes.Others;

                double totalAssetOpening = 0, totalAssetTransaction = 0, totalAssetClosing = 0;
                double totalLiabilitiesOpening = 0, totalLiabilitiesTransaction = 0, totalLiabilitiesClosing = 0;
                
                System.Collections.ArrayList AssetsTreeListView1 = new System.Collections.ArrayList();
                System.Collections.ArrayList LiabilitiesTreeListView1 = new System.Collections.ArrayList();

                foreach (var v in LedgerGroupColl.Where(p1 => p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Assets && p1.ParentGroupId == 1))
                {
                    Dynamic.ReportEntity.Account.ProfitAndLosss beData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    beData.Particulars = v.Name;
                    beData.IsLedgerGroup = true;
                    beData.LedgerGroupId = v.LedgerGroupId;
                    AddChieldNodeOfAssets(beData);
                    AssetsTreeListView1.Add(beData);

                    totalAssetOpening += beData.OpeningAmt;
                    totalAssetTransaction += beData.TransactionAmt;
                    totalAssetClosing += beData.ClosingAmt;

                }

                foreach (var v in LedgerGroupColl.Where(p1 => p1.NatureOfGroup == Dynamic.BusinessEntity.Account.NatureOfGroups.Liability && p1.ParentGroupId == 1))
                {
                    Dynamic.ReportEntity.Account.ProfitAndLosss beData = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    beData.Particulars = v.Name;
                    beData.IsLedgerGroup = true;
                    beData.LedgerGroupId = v.LedgerGroupId;
                    AddChieldNodeOfLiabilities(beData);
                    LiabilitiesTreeListView1.Add(beData);

                    totalLiabilitiesOpening += beData.OpeningAmt;
                    totalLiabilitiesTransaction += beData.TransactionAmt;
                    totalLiabilitiesClosing += beData.ClosingAmt;
                }

                //double netProfitAndLoss = netProfitAndLossOpening + netProfitAndLossTransaction;

                AssetsTreeListView1.Add(blankData);
                LiabilitiesTreeListView1.Add(blankData);


                Dynamic.ReportEntity.Account.ProfitAndLosss netProfit = GetProfitAndLossAmount(dateFrom,dateTo);

                if (netProfit != null)
                {
                    if (netProfit.ClosingAmt > 0)
                    {
                        LiabilitiesTreeListView1.Add(netProfit);
                        totalLiabilitiesOpening += Math.Abs(netProfit.OpeningAmt);
                        totalLiabilitiesTransaction += Math.Abs(netProfit.TransactionAmt);
                        totalLiabilitiesClosing += Math.Abs(netProfit.ClosingAmt);

                    }
                    else
                    {
                        AssetsTreeListView1.Add(netProfit);

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
                        LiabilitiesTreeListView1.Add(diffAmt);
                        AssetsTreeListView1.Add(blankData);
                    }

                    AssetsTreeListView1.Add(blankData);
                    LiabilitiesTreeListView1.Add(blankData);

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalAsset = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalAsset.Particulars = " TOTAL ";
                    grandTotalAsset.OpeningAmt = totalAssetOpening;
                    grandTotalAsset.TransactionAmt = totalAssetTransaction;
                    grandTotalAsset.ClosingAmt = totalAssetClosing;

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalLiab = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalLiab.Particulars = " TOTAL ";
                    grandTotalLiab.OpeningAmt = totalLiabilitiesOpening + diffAmt.OpeningAmt;
                    grandTotalLiab.TransactionAmt = totalLiabilitiesTransaction + diffAmt.TransactionAmt;
                    grandTotalLiab.ClosingAmt = totalLiabilitiesClosing + diffAmt.ClosingAmt;

                    AssetsTreeListView1.Add(grandTotalAsset);
                    LiabilitiesTreeListView1.Add(grandTotalLiab);

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
                        AssetsTreeListView1.Add(diffAmt);
                        LiabilitiesTreeListView1.Add(blankData);
                    }

                    AssetsTreeListView1.Add(blankData);
                    LiabilitiesTreeListView1.Add(blankData);

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalAsset = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalAsset.Particulars = " TOTAL ";
                    grandTotalAsset.OpeningAmt = totalAssetOpening + diffAmt.OpeningAmt;
                    grandTotalAsset.TransactionAmt = totalAssetTransaction + diffAmt.TransactionAmt;
                    grandTotalAsset.ClosingAmt = totalAssetClosing + diffAmt.ClosingAmt;

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalLiab = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalLiab.Particulars = " TOTAL ";
                    grandTotalLiab.OpeningAmt = totalLiabilitiesOpening;
                    grandTotalLiab.TransactionAmt = totalLiabilitiesTransaction;
                    grandTotalLiab.ClosingAmt = totalLiabilitiesClosing;

                    AssetsTreeListView1.Add(grandTotalAsset);
                    LiabilitiesTreeListView1.Add(grandTotalLiab);
                }
                else
                {
                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalAsset = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalAsset.Particulars = " TOTAL ";
                    grandTotalAsset.OpeningAmt = totalAssetOpening;
                    grandTotalAsset.TransactionAmt = totalAssetTransaction;
                    grandTotalAsset.ClosingAmt = totalAssetClosing;

                    Dynamic.ReportEntity.Account.ProfitAndLosss grandTotalLiab = new Dynamic.ReportEntity.Account.ProfitAndLosss();
                    grandTotalLiab.Particulars = " TOTAL ";
                    grandTotalLiab.OpeningAmt = totalLiabilitiesOpening;
                    grandTotalLiab.TransactionAmt = totalLiabilitiesTransaction;
                    grandTotalLiab.ClosingAmt = totalLiabilitiesClosing;

                    AssetsTreeListView1.Add(grandTotalAsset);
                    LiabilitiesTreeListView1.Add(grandTotalLiab);
                }

                System.Collections.ArrayList finalDataColl = new System.Collections.ArrayList();
                finalDataColl.Add(AssetsTreeListView1);
                finalDataColl.Add(LiabilitiesTreeListView1);

                return new JsonNetResult() { Data = finalDataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }
        public ActionResult CashAndBankBook()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetCBLedgerWise()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalanceCollections dataColl = new Dynamic.ReportEntity.Account.TrailBalanceCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";

                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);
 
                dataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(User.HostName, User.DBName).getCashAndBankBookLedgerWise(User.UserId, dateFrom, dateTo);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult CashBankVoucher()
        {
            return View();
        }

        public ActionResult BankReconciliation()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResultWithEnum GetBankReconciliation(int LedgerId, DateTime? dateFrom, DateTime? dateTo, string BranchIdColl, int ClearanceDateAs, int TranType)
        {
            double openingAmt = 0,bankOpeningAmt=0;

            if (!dateFrom.HasValue)
                dateFrom = DateTime.Today;

            if (!dateTo.HasValue)
                dateTo = DateTime.Today;

            Dynamic.ReportEntity.Account.BankReconciliationCollections dataColl = new Dynamic.Reporting.Account.BankReconciliation(User.HostName, User.DBName).getBankReconciliation(User.UserId, LedgerId, BranchIdColl, dateFrom.Value, dateTo.Value, ClearanceDateAs, TranType, ref openingAmt,ref bankOpeningAmt);
            Dynamic.ReportEntity.Account.BankReconciliationCollections tmpDataColl = new Dynamic.ReportEntity.Account.BankReconciliationCollections();
            double currentClosing = openingAmt;
            DateTime currentDateTime = DateTime.Today;
            double drAmt = 0, crAmt = 0;
            double drAmtP = 0, crAmtP = 0;
            double drAmtC = 0, crAmtC = 0;
            foreach (var v in dataColl)
            {
                currentClosing += v.DrAmt - v.CrAmt;
                v.ClosingAmt = currentClosing;

                if (v.VoucherDate.Year > 1900)
                    v.VoucherAge = (int)(currentDateTime - v.VoucherDate).TotalDays;

                drAmt += v.DrAmt;
                crAmt += v.CrAmt;

                if (v.IsCleared)
                {
                    drAmtC += v.DrAmt;
                    crAmtC += v.CrAmt;
                }
                else
                {
                    drAmtP += v.DrAmt;
                    crAmtP += v.CrAmt;
                }

                tmpDataColl.Add(v);
            }

            double cl = openingAmt + drAmt - crAmt;

            double totalCRAmt = (openingAmt < 0 ? Math.Abs(openingAmt) : -openingAmt) + crAmt;

            var returnVal = new
            {
                OpeningAmt = openingAmt,
                DrAmt = drAmt,
                CrAmt = crAmt,
                ClosingAmt = cl,
                DrAmtP = drAmtP,
                CrAmtP = crAmtP,
                DrAmtC = drAmtC,
                CrAmtC = crAmtC,
                DataColl = tmpDataColl
            };
            return new JsonNetResultWithEnum() { Data = returnVal };
        }

        [HttpPost]
        public JsonNetResult SaveBRS()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.ReportEntity.Account.BankReconciliation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.ReconcileBY = User.UserName;
                    Dynamic.ReportEntity.Account.BankReconciliationCollections tmpDataColl = new Dynamic.ReportEntity.Account.BankReconciliationCollections();
                    tmpDataColl.Add(beData);
                    resVal = new Dynamic.Reporting.Account.BankReconciliation(User.HostName, User.DBName).SaveUpdateBankReconciliation(User.UserId, tmpDataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveBRSColl()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.ReportEntity.Account.BankReconciliationCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    foreach (var v in beData)
                    {
                        v.ReconcileBY = User.UserName;

                    }
                    resVal = new Dynamic.Reporting.Account.BankReconciliation(User.HostName, User.DBName).SaveUpdateBankReconciliation(User.UserId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult ChequeBookSummary()
        {
            return View();
        }
        public ActionResult CashFlow()
        {
            return View();
        }


        public ActionResult LedgerVoucher(int? ledgerId = null)
        {
            if (ledgerId.HasValue)
                ViewBag.SelectedLedgerId = ledgerId;
            else
                ViewBag.SelectedLedgerId = 0;

            ViewBag.Title = "LedgerVoucher";
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.LedgerVoucher);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetLedgerVoucher(DateTime dateFrom, DateTime dateTo, int ledgerId)
        {
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;
            double OpeningPoint = 0;
            double openingAmt = 0;
            Dynamic.ReportEntity.Account.LedgerVoucherCollections dataColl = new Dynamic.Reporting.Account.LedgerSummary(User.HostName, User.DBName).getLedgerVoucher(User.UserId,BaseDate.NepaliDate, ledgerId, dateFrom, dateTo, ref openingAmt,ref OpeningPoint, true, false);

            Dynamic.ReportEntity.Account.LedgerVoucherCollections tmpDataColl = new Dynamic.ReportEntity.Account.LedgerVoucherCollections();
            double currentClosing = openingAmt;
            foreach (var v in dataColl)
            {
                currentClosing += v.DebitAmt - v.CreditAmt;
                v.CurrentClosing = currentClosing;
                tmpDataColl.Add(v);
            }

            double drAmt = 0, crAmt = 0;
            drAmt = tmpDataColl.Sum(p1 => p1.DebitAmt);
            crAmt = tmpDataColl.Sum(p1 => p1.CreditAmt);

            double cl = openingAmt + drAmt - crAmt;

            var returnVal = new
            {
                OpeningAmt = openingAmt,
                DrAmt = drAmt,
                CrAmt = crAmt,
                ClosingAmt = cl,
                DataColl = tmpDataColl
            };
            return new JsonNetResult() { Data = returnVal };
        }

        [HttpPost]
        public JsonNetResult PrintLedgerVoucher()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.LedgerVoucher> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.LedgerVoucher>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult CostCenterVoucher()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetCostCenterVoucher(DateTime dateFrom, DateTime dateTo, int costCenterId,int? ledgerId)
        {
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;

            double openingAmt = 0;
            int LedgerId = 0;
            if (ledgerId.HasValue)
                LedgerId = ledgerId.Value;

            Dynamic.ReportEntity.Account.LedgerVoucherCollections dataColl = new Dynamic.Reporting.Account.CostCenterVoucher(User.HostName, User.DBName).getCostCenterVoucher(User.UserId, costCenterId, dateFrom, dateTo, ref openingAmt, true,LedgerId);

            Dynamic.ReportEntity.Account.LedgerVoucherCollections tmpDataColl = new Dynamic.ReportEntity.Account.LedgerVoucherCollections();
            double currentClosing = openingAmt;
            foreach (var v in dataColl)
            {
                v.NVoucherDate = v.NY.ToString() + "-" + v.NM.ToString().PadLeft(2,'0') + "-" + v.ND.ToString().PadLeft(2, '0');
                currentClosing += v.DebitAmt - v.CreditAmt;
                v.CurrentClosing = currentClosing;
                tmpDataColl.Add(v);
            }

            double drAmt = 0, crAmt = 0;
            drAmt = tmpDataColl.Sum(p1 => p1.DebitAmt);
            crAmt = tmpDataColl.Sum(p1 => p1.CreditAmt);

            double cl = openingAmt + drAmt - crAmt;

            var returnVal = new
            {
                OpeningAmt = openingAmt,
                DrAmt = drAmt,
                CrAmt = crAmt,
                ClosingAmt = cl,
                DataColl = tmpDataColl
            };
            return new JsonNetResult() { Data = returnVal };
        }

        public ActionResult LedgerMonthly()
        {
            return View();
        }
        public ActionResult LedgerDaily()
        {
            return View();
        }
        public ActionResult MultipleLedger()
        {
            return View();
        }
        public ActionResult LedgerFlow()
        {
            return View();
        }

        public ActionResult LedgerGroupSummary()
        {
            return View();
        }
        public ActionResult LedgerGroupVoucher()
        {
            return View();
        }
        public ActionResult LedgerGroupMonthly()
        {
            return View();
        }
        public ActionResult LedgerGroupDaily()
        {
            return View();
        }
        public ActionResult CostCenterSummary()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResultWithEnum GetAllCostCenterClosing(DateTime? fromDate, DateTime? toDate, int? LedgerId, string LedgerCode = "", string CostCategoryIdColl = "")
        {
            if (!LedgerId.HasValue)
                LedgerId = 0;

            Dynamic.ReportEntity.Account.TrailBalanceCollections dataColl = new Dynamic.ReportEntity.Account.TrailBalanceCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Account.CostCenter(User.HostName, User.DBName).getCostCenterClosingBalance(User.UserId, fromDate.Value, toDate.Value, LedgerId.Value, LedgerCode, CostCategoryIdColl);
                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetCostCenterSummary()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.TrailBalanceCollections dataColl = new Dynamic.ReportEntity.Account.TrailBalanceCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int ledgerId = 0;
                string CostCategoryIdColl = "";

                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("ledgerId") && para["ledgerId"] != null && !string.IsNullOrEmpty(para["ledgerId"].ToString()))
                    ledgerId = Convert.ToInt32(para["ledgerId"]);


                if (para.ContainsKey("costCategoryIdColl") && para["costCategoryIdColl"] != null && !string.IsNullOrEmpty(para["costCategoryIdColl"].ToString()))
                    CostCategoryIdColl = Convert.ToString(para["costCategoryIdColl"]);

                dataColl = new Dynamic.Reporting.Account.CostCenter(User.HostName, User.DBName).getCostCenterClosingBalance(User.UserId, dateFrom, dateTo, ledgerId,"",CostCategoryIdColl);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult CostCategorySummary()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetCostCategorySummary()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.CostCategoriesSummaryCollections dataColl = new Dynamic.ReportEntity.Account.CostCategoriesSummaryCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                int branchId = 0;
                string branchIdColl = "";

                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                if (para.ContainsKey("branchId") && para["branchId"] != null && !string.IsNullOrEmpty(para["branchId"].ToString()))
                    branchId = Convert.ToInt32(para["branchId"]);

                if (para.ContainsKey("branchIdColl") && para["branchIdColl"] != null && !string.IsNullOrEmpty(para["branchIdColl"].ToString()))
                    branchIdColl = Convert.ToString(para["branchIdColl"]);

                List<int> ignoreList = new List<int>();
                ignoreList.Add(1);
                dataColl = new Dynamic.Reporting.Account.CostCenter(User.HostName, User.DBName).getAllCostCategoriesSummary(User.UserId, dateFrom, dateTo);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult CostCenterMonthly()
        {
            return View();
        }
        public ActionResult CostCenterDaily()
        {
            return View();
        }

        #region "Income Expenditure"
        public ActionResult IncomeExpenditure()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult PrintIncomeExpenditure()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.IncomeExpenditure> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.IncomeExpenditure>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetIncomeExpenditure()
        {
            Newtonsoft.Json.Linq.JObject para = DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["jsonData"]);
            Dynamic.ReportEntity.Account.IncomeExpenditureCollections dataColl = new Dynamic.ReportEntity.Account.IncomeExpenditureCollections();
            try
            {
                DateTime dateFrom = DateTime.Today;
                DateTime dateTo = DateTime.Today;
                
                if (para.ContainsKey("dateFrom") && para["dateFrom"] != null && !string.IsNullOrEmpty(para["dateFrom"].ToString()))
                    dateFrom = Convert.ToDateTime(para["dateFrom"]);

                if (para.ContainsKey("dateTo") && para["dateTo"] != null && !string.IsNullOrEmpty(para["dateTo"].ToString()))
                    dateTo = Convert.ToDateTime(para["dateTo"]);

                dataColl = new Dynamic.Reporting.Account.TrailBalanceSheet(User.HostName, User.DBName).getIncomeExpenditure(dateFrom, dateTo,  User.UserId,"");

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "Audit Report"

        public ActionResult UserLog()
        {
            return View();
        }
        public JsonNetResult GetUserLog(DateTime dateFrom, DateTime dateTo, int userId,int entityId,int action)
        {
            AuditLogCollections dataColl = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName).getAuditLogDateWise(dateFrom, dateTo, userId, entityId, action);

            return new JsonNetResult() { Data = dataColl, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG, TotalCount = dataColl.Count };
        }

        //public ActionResult SalesMaterializedView()
        //{
        //    return View();
        //}
        //public JsonNetResult GetSalesMaterializedView(DateTime dateFrom, DateTime dateTo, bool isReturn)
        //{
        //    Dynamic.ReportEntity.Inventory.SalesMaterializedViewCollections dataColl = new Dynamic.Reporting.Inventory.SalesMaterilizedView(User.HostName, User.DBName).getSalesMaterializedView(User.UserId,dateFrom, dateTo,isReturn);

        //    return new JsonNetResult() { Data = dataColl,IsSuccess=dataColl.IsSuccess,ResponseMSG=dataColl.ResponseMSG,TotalCount=dataColl.Count };
        //}
        [HttpPost]
        public JsonNetResult PrintSalesMaterializedView()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Inventory.SalesMaterializedView> paraData = DeserializeObject<List<Dynamic.ReportEntity.Inventory.SalesMaterializedView>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        
        [HttpPost]
        public JsonNetResult PrintSalesInvoiceDetail()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Inventory.SalesInvoiceDetails> paraData = DeserializeObject<List<Dynamic.ReportEntity.Inventory.SalesInvoiceDetails>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult SalesVatRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResultWithEnum GetSalesVatRegister(DateTime? dateFrom, DateTime? dateTo, int VoucherId, int BranchId, int? PartyLedgerId, int ReportType = 1, bool ShowReturn = false, bool ShowDebitNote = false, bool ShowCreditNote = false, bool ShowPIDebitNote = false, bool ShowPICreditNote = false, bool ShowPurchaseInvoice = false, bool ShowPurchaseReturn = false, bool ShowSalesInvoice = true, bool IsExciseReg = false)
        {
            Dynamic.ReportEntity.Account.NewSalesVatRegisterCollections dataColl = new Dynamic.ReportEntity.Account.NewSalesVatRegisterCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.VatRegister(User.HostName, User.DBName).getSalesVatRegister(User.UserId, dateFrom.Value, dateTo.Value, VoucherId, BranchId, PartyLedgerId, ReportType, ShowReturn, ShowDebitNote, ShowCreditNote, ShowPIDebitNote, ShowPICreditNote, ShowPurchaseInvoice, ShowPurchaseReturn, ShowSalesInvoice, IsExciseReg);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult SalesReturnVatRegister()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResultWithEnum GetSalesReturnVatRegister(DateTime? dateFrom, DateTime? dateTo, int VoucherId, int BranchId, int ReportType = 1, bool ShowDebitNote = false, bool ShowCreditNote = false)
        {
            Dynamic.ReportEntity.Account.NewSalesVatRegisterCollections dataColl = new Dynamic.ReportEntity.Account.NewSalesVatRegisterCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.VatRegister(User.HostName, User.DBName).getSalesReturnVatRegister(User.UserId, dateFrom.Value, dateTo.Value, VoucherId, BranchId, ReportType, null, ShowDebitNote, ShowCreditNote);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintSalesVatRegister()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.NewSalesVatRegister> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.NewSalesVatRegister>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult PurchaseVatRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResultWithEnum GetPurchaseVatRegister(DateTime dateFrom, DateTime dateTo, int VoucherId = 0, int BranchId = 0, int ReportType = 1, bool ShowReturn = false, bool ShowDebitNote = false, bool ShowCreditNote = false)
        {
            Dynamic.ReportEntity.Account.NewPurchaseVatRegisterCollections dataColl = new Dynamic.ReportEntity.Account.NewPurchaseVatRegisterCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.VatRegister(User.HostName, User.DBName).getPurchaseVatRegister(User.UserId, dateFrom, dateTo, VoucherId, BranchId, ReportType, null, ShowReturn, ShowDebitNote, ShowCreditNote);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult PurchaseReturnVatRegister()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResultWithEnum GetPurchaseReturnVatRegister(DateTime dateFrom, DateTime dateTo, int VoucherId = 0, int BranchId = 0, int ReportType = 1, bool ShowDebitNote = false, bool ShowCreditNote = false)
        {
            Dynamic.ReportEntity.Account.NewPurchaseVatRegisterCollections dataColl = new Dynamic.ReportEntity.Account.NewPurchaseVatRegisterCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.VatRegister(User.HostName, User.DBName).getPurchaseReturnVatRegister(User.UserId, dateFrom, dateTo, VoucherId, BranchId, ReportType, null, ShowDebitNote, ShowCreditNote);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintPurchaseVatRegister()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.NewPurchaseVatRegister> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.NewPurchaseVatRegister>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult AccountConfirmation()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetAccountConfirmationLetter(DateTime? dateFrom, DateTime? dateTo, int LedgerGroupId, int ReportType)
        {
            Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections dataColl = new Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.AccountConfirmationLetter(User.HostName, User.DBName).getAccountConfirmation(User.UserId, dateFrom.Value, dateTo.Value, ReportType, LedgerGroupId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintAccountConfirmation()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.AccountConfirmationLetter> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.AccountConfirmationLetter>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult CancelVoucherList()
        {
            return View();
        }
        public JsonNetResult GetCancelVoucherList(DateTime dateFrom, DateTime dateTo, int VoucherType, bool isPost, int branchId)
        {
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;
            Dynamic.ReportEntity.Account.DayBookCollections dataColl = new Dynamic.Reporting.Account.DayBook(User.HostName, User.DBName).getCancelDayBook(User.UserId, dateFrom, dateTo, VoucherType);

            return new JsonNetResult() { Data = dataColl };
        }
        [HttpPost]
        public JsonNetResult PrintCancelVoucherList()
        {
            var jsonData = Request["jsonData"];
            List<Dynamic.ReportEntity.Account.DayBook> paraData = DeserializeObject<List<Dynamic.ReportEntity.Account.DayBook>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion
         
        public ActionResult TDSVat()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetAllTDSVat(DateTime? dateFrom, DateTime? dateTo)
        {
            Dynamic.ReportEntity.Account.TDSVatCollections dataColl = new Dynamic.ReportEntity.Account.TDSVatCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.TDSVat(User.HostName, User.DBName).getTDSVat(User.UserId, dateFrom.Value, dateTo.Value);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult BudgetReporting()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetBudgetSummary(string BranchIdColl, DateTime? DateFrom, DateTime? DateTo, int? CostClassId, int? DateType)
        {
            Dynamic.ReportEntity.Account.BudgetCollections dataColl = new Dynamic.ReportEntity.Account.BudgetCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.BudgetDB(User.HostName, User.DBName).GetBudgetSummary(User.UserId, BranchIdColl, DateFrom, DateTo, 1, DateType);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.ExciseRegister, true)]
        public ActionResult ExciseRegister()
        {
            return View();
        }


        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.VatRegister, true)]
        public ActionResult VatSummary()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResultWithEnum GetVatSummary(int ReportFormat, string BranchIdColl, DateTime DateFrom, DateTime DateTo)
        {

            Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections dataColl = new Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.AccountConfirmationLetter(User.HostName, User.DBName).getVatSummaryMonthly(User.UserId, ReportFormat, DateFrom, DateTo, BranchIdColl);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.OneLakhAboveSales, true)]
        public ActionResult OneLakhAboveSales()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetOneLakhAboveSales(DateTime? dateFrom, DateTime? dateTo, int LedgerGroupId, int ReportType, int? BranchId = null)
        {
            Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections dataColl = new Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections();
            try
            {
                string branchIdColl = "";
                if (BranchId.HasValue && BranchId.Value > 0)
                    branchIdColl = BranchId.Value.ToString();

                dataColl = new Dynamic.Reporting.Account.AccountConfirmationLetter(User.HostName, User.DBName).getOneLakhAboveSales(User.UserId, dateFrom.Value, dateTo.Value, ReportType, LedgerGroupId, branchIdColl);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.OneLakhAbovePurchase, true)]
        public ActionResult OneLakhAbovePurchase()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetOneLakhAbovePurchase(DateTime? dateFrom, DateTime? dateTo, int LedgerGroupId, int ReportType, int? BranchId = null)
        {
            Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections dataColl = new Dynamic.ReportEntity.Account.AccountConfirmationLetterCollections();
            try
            {
                string branchIdColl = "";
                if (BranchId.HasValue && BranchId.Value > 0)
                    branchIdColl = BranchId.Value.ToString();

                dataColl = new Dynamic.Reporting.Account.AccountConfirmationLetter(User.HostName, User.DBName).getOneLakhAbovePurchase(User.UserId, dateFrom.Value, dateTo.Value, ReportType, LedgerGroupId, branchIdColl);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.CancelVouchersList, true)]
        public ActionResult CancelDayBook()
        {
            ViewBag.Title = "CancelDayBook";
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.CancelVouchersList);
            return View();
        }


        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)RptFormsEntity.TDSSummary, true)]
        public ActionResult TDSSummary()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResultWithEnum GetTDSSummary(DateTime? dateFrom, DateTime? dateTo, int? BranchId)
        {
            Dynamic.ReportEntity.Inventory.PurchaseTDSCollections dataColl = new Dynamic.ReportEntity.Inventory.PurchaseTDSCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.PurchaseTDS(User.HostName, User.DBName).getTDSSummary(User.UserId, dateFrom.Value, dateTo.Value, BranchId);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResultWithEnum() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult CostCenterOpeningDetails()
        {
            return View();
        }


        [HttpGet]
        public JsonNetResult GetAllCostCenterOpeningDetails()
        {
            Dynamic.ReportEntity.Account.CostCenterOpeningDetailsCollections dataColl = new Dynamic.ReportEntity.Account.CostCenterOpeningDetailsCollections();
            try
            {

                dataColl = new Dynamic.Reporting.Account.CostCenter(User.HostName, User.DBName).getCostCenterOpening(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


    }
}