using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using AcademicLib.BE.Global;
using Newtonsoft.Json;
namespace PivotalERP.Areas.Account.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {

        public ActionResult Bank()
        {
            return View();
        }


        #region "Bank"

        [HttpPost]
        public JsonNetResult SaveBank()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Bank.Bank>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.BankId.HasValue)
                        beData.BankId = 0;
                    resVal = new Dynamic.BusinessLogic.Bank.Bank(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getBankById(int BankId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Bank.Bank(User.UserId, User.HostName, User.DBName).GetBankById(0, BankId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteBank(int BankId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BankId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Company name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Bank.Bank(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BankId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBank()
        {
            Dynamic.BusinessEntity.Bank.BankCollections dataColl = new Dynamic.BusinessEntity.Bank.BankCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.Bank(User.UserId, User.HostName, User.DBName).GetAllBank(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "BankGroup"
        [HttpPost]
        public JsonNetResult SaveBankGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Bank.BankGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.BankGroupId.HasValue)
                        beData.BankGroupId = 0;
                    resVal = new Dynamic.BusinessLogic.Bank.BankGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getBankGroupById(int BankGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Bank.BankGroup(User.UserId, User.HostName, User.DBName).GetBankGroupById(0, BankGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]

        public JsonNetResult DeleteBankGroup(int BankGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BankGroupId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Company name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Bank.BankGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BankGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBankGroup()
        {
            Dynamic.BusinessEntity.Bank.BankGroupCollections dataColl = new Dynamic.BusinessEntity.Bank.BankGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.BankGroup(User.UserId, User.HostName, User.DBName).GetAllBankGroup(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "BankAccount"

        [HttpPost]
        public JsonNetResult SaveBankAccount()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Bank.BankAccount>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.BankAccountId.HasValue)
                        beData.BankAccountId = 0;
                    resVal = new Dynamic.BusinessLogic.Bank.BankAccount(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getBankAccountById(int BankAccountId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Bank.BankAccount(User.UserId, User.HostName, User.DBName).GetBankAccountById(0, BankAccountId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteBankAccount(int BankAccountId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BankAccountId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Company name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Bank.BankAccount(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BankAccountId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBankAccount()
        {
            Dynamic.BusinessEntity.Bank.BankAccountCollections dataColl = new Dynamic.BusinessEntity.Bank.BankAccountCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.BankAccount(User.UserId, User.HostName, User.DBName).GetAllBankAccount(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "ChequeEntry"

        [HttpPost]
        public JsonNetResult SaveChequeEntry()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Bank.ChequeEntry>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    resVal = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getChequeEntryById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).GetChequeEntryById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteChequeEntry(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Company name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllChequeEntry()
        {
            Dynamic.BusinessEntity.Bank.ChequeEntryCollections dataColl = new Dynamic.BusinessEntity.Bank.ChequeEntryCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).GetAllChequeEntry(0);
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
        public JsonNetResult GetPendingCheque(int LedgerId)
        {
            Dynamic.ReportEntity.Bank.PendingChequeCollections dataColl = new Dynamic.ReportEntity.Bank.PendingChequeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).getPendingChequeList(LedgerId);
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
        public JsonNetResult GetChequeRegister(int ReportType)
        {
            Dynamic.ReportEntity.Bank.PendingChequeCollections dataColl = new Dynamic.ReportEntity.Bank.PendingChequeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).getChequeRegister(ReportType);
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
        public JsonNetResult CancelCheque(int ChequeId, string CancelRemarks)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Bank.ChequeEntry(User.UserId, User.HostName, User.DBName).CancelCheque(ChequeId, CancelRemarks);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region Denomination


        
        public ActionResult Denomination()
        {
            return View();
        }

        [HttpPost]        
        public JsonNetResult SaveUpdateDenomination()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Denomination>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.DenominationId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.Denomination(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.Denomination(User.HostName, User.DBName).SaveFormData(beData);
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
        }


        [HttpPost]        
        public JsonNetResult getDenominationById(int DenominationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.Denomination(User.HostName, User.DBName).getDenominationById(User.UserId, DenominationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult DeleteDenomination(int DenominationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (DenominationId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.Denomination(User.HostName, User.DBName).DeleteById(DenominationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllDenomination()
        {
            Dynamic.BusinessEntity.Account.DenominationCollections dataColl = new Dynamic.BusinessEntity.Account.DenominationCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Denomination(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region DebtorCreditorsType
        
        public ActionResult DebtorsCreditorsType()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateDebtorsCreditorsType()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.DebtorType>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.DebtorTypeId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).SaveFormData(beData);
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
        }


        [HttpPost]        
        public JsonNetResult getDebtorsCreditorsTypeById(int DebtorTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).getDebtorTypeById(User.UserId, DebtorTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult DeleteDebtorsCreditorsType(int DebtorTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (DebtorTypeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).DeleteById(DebtorTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllDebtorsCreditorsType()
        {
            Dynamic.BusinessEntity.Account.DebtorTypeCollections dataColl = new Dynamic.BusinessEntity.Account.DebtorTypeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region PaymentTerms

        
        public ActionResult PaymentTerms()
        {
            return View();
        }
        [HttpPost]        
        public JsonNetResult SaveUpdatePaymentTerms()
        {
            string photoLocation = "/Attachments/account/ledger";

            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.PaymentTerms>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;
                        //beData.ImagePath = "";
                        var filesColl = Request.Files;
                        if (filesColl.Count > 0)
                        {
                            HttpPostedFileBase file = filesColl[0];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.ImagePath = att.DocPath;
                            }
                        }
                        if (beData.PaymentTermsId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.PaymentTerms(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.PaymentTerms(User.HostName, User.DBName).SaveFormData(beData);
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
        }


        [HttpPost]        
        public JsonNetResult getPaymentTermsById(int PaymentTermsId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.PaymentTerms(User.HostName, User.DBName).getPaymentTermsById(User.UserId, PaymentTermsId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult DeletePaymentTerms(int PaymentTermsId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (PaymentTermsId < 0)
                {
                    resVal.ResponseMSG = "can't delete default PaymentTerms name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.PaymentTerms(User.HostName, User.DBName).DeleteById(PaymentTermsId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllPaymentTerms()
        {
            Dynamic.BusinessEntity.Account.PaymentTermsCollections dataColl = new Dynamic.BusinessEntity.Account.PaymentTermsCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.PaymentTerms(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region FreightType

        
        public ActionResult FreightType()
        {
            return View();
        }
        [HttpPost]        
        public JsonNetResult SaveUpdateFreightType()
        {

            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.FreightType>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.FreightTypeId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.FreightType(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.FreightType(User.HostName, User.DBName).SaveFormData(beData);
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
        }


        [HttpPost]        
        public JsonNetResult getFreightTypeById(int FreightTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.FreightType(User.HostName, User.DBName).getFreightTypeById(User.UserId, FreightTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult DeleteFreightType(int FreightTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (FreightTypeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default FreightType name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.FreightType(User.HostName, User.DBName).DeleteById(FreightTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllFreightType()
        {
            Dynamic.BusinessEntity.Account.FreightTypeCollections dataColl = new Dynamic.BusinessEntity.Account.FreightTypeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.FreightType(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "Ledger"

        
        public ActionResult LedgerCredit()
        {
            return View();
        }

        [HttpPost]        
        public JsonNetResult SaveUpdateLedgerCredit()
        {
            string str = Request["jsonData"];
            var dataColl = DeserializeObject<Dynamic.BusinessEntity.Account.LedgerCreditCollections>(str);

            var resVal = new Dynamic.DataAccess.Account.LedgerDB(User.HostName, User.DBName).UpdateLedgerCredit(User.UserId, dataColl);

            return new JsonNetResult() { Data = resVal, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult GetLedgerCredit(int LedgerId)
        {
            var resVal = new Dynamic.DataAccess.Account.LedgerDB(User.HostName, User.DBName).getLedgerCreditLog(User.UserId, LedgerId);

            return new JsonNetResult() { Data = resVal, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [PermissionsAttribute(Actions.View, (int)ENTITIES.Ledger, false)]

        public ActionResult Ledger(int tranId = 0)
        {
            ViewBag.TranId = tranId;
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Ledger, false)]

        public ActionResult Customer(int tranId = 0)
        {
            ViewBag.TranId = tranId;
            return View();
        }


        [HttpPost]
        public JsonNetResult GetLedgerLst(TableFilter filter)
        {
            Dynamic.BusinessEntity.Common.LedgerCollections dataColl = new Dynamic.BusinessEntity.Common.LedgerCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Common.LedgerDB(User.HostName, User.DBName).getLedgerSearchList(filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [HttpGet]
        public JsonNetResult GetSalesLedger(int? voucherId)
        {
            var dataColl = new Dynamic.DataAccess.Common.LedgerDB(User.HostName, User.DBName).getSalesLedger(User.UserId, voucherId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetDebtorCreditGroup()
        {
            var dataColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getDebtorAndCreditor(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetPurchaseLedger(int? voucherId)
        {
            var dataColl = new Dynamic.DataAccess.Common.LedgerDB(User.HostName, User.DBName).getPurchaseLedger(User.UserId, voucherId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetLedgerById(int ledgerId)
        {
            var ledgerBL = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName);
            Dynamic.BusinessEntity.Account.Ledger dataColl = ledgerBL.getLedgerById(User.UserId, ledgerId);
            dataColl.CUserId = User.UserId;
            ledgerBL.getLedgerDetailsByLedgerId(ref dataColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetBillWiseDues(int BranchId, int LedgerId, int CostClassId)
        {
            double openingAmt = 0;
            Dynamic.BusinessEntity.Account.BillWiseDuesCollections dataColl = new Dynamic.DataAccess.Account.BillWiseDuesDB(User.HostName, User.DBName).getBillWiseDues(LedgerId, BranchId, User.UserId, ref openingAmt);
            var retData = new
            {
                OpeningAmt = openingAmt,
                BillColl = dataColl
            };
            return new JsonNetResult() { Data = retData, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveBillWiseOpening()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string str = Request["jsonData"];
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.BillWiseDuesCollections>(str);
                var openData = DeserializeObject<Dynamic.BusinessEntity.Account.LedgerOpeningDetails>(Request["openingData"]);
                if (beData != null)
                {
                    var first = beData.First();
                    resVal = new Dynamic.DataAccess.Account.BillWiseDuesDB(User.HostName, User.DBName).SaveUpdate(first.LedgerId, first.BranchId, User.UserId, beData, openData);

                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = first.LedgerId;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BillWiseLedgerOpening;
                    auditLog.Action = Actions.Save;
                    auditLog.LogText = auditLog.EntityId.ToString() + first.BranchId.ToString();
                    auditLog.AutoManualNo = IsNullStr(first.LedgerId.ToString());
                    SaveAuditLog(auditLog);
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
        [PermissionsAttribute(Actions.View, (int)ENTITIES.BranchWiseLedgerOpening, false)]

        public JsonNetResult DelBillWise(int tranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.BillWiseDues(User.UserId, User.HostName, User.DBName).Delete(User.UserId, tranId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = tranId;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BillWiseLedgerOpening;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = auditLog.EntityId.ToString() + tranId.ToString();
                    auditLog.AutoManualNo = IsNullStr(tranId.ToString());
                    SaveAuditLog(auditLog);
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
        public JsonNetResult GetLedgerOpeningforBranch(int BranchId, int LedgerId, DateTime voucherDate)
        {
            Dynamic.BusinessEntity.Account.LedgerOpeningDetails dataColl = new Dynamic.DataAccess.Account.LedgerDB(User.HostName, User.DBName).getLedgerOpeningforBranch(BranchId, LedgerId, voucherDate, User.UserId,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCostCenterLedgerOpening(int BranchId, int LedgerId, int CostCenterId, DateTime voucherDate)
        {
            Dynamic.BusinessEntity.Account.CostCenterOpening beData = new Dynamic.BusinessEntity.Account.CostCenterOpening();
            beData.BranchId = BranchId;
            beData.LedgerId = LedgerId;
            beData.CostCenterId = CostCenterId;
            beData.VoucherDate = voucherDate;
            new Dynamic.DataAccess.Account.CostCenterDB(User.HostName, User.DBName).getCostCenterOpening(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        [HttpPost]
        public JsonNetResult SaveUpdateLedgerOpeningforBranch(Dynamic.BusinessEntity.Account.LedgerOpeningDetailsCollections dataColl)
        {
            var resVal = new Dynamic.DataAccess.Account.LedgerDB(User.HostName, User.DBName).updateLedgerOpeningforBranch(User.UserId, dataColl);

            return new JsonNetResult() { Data = resVal, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLedgerCode(string name, int? ledgerGroupId)
        {
            var dataColl = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).getLedgerCode(User.UserId, name, ledgerGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Ledger)]
        public JsonNetResult SaveLedger()
        {
            string photoLocation = "/Attachments/account/ledger";
            ResponeValues resVal = new ResponeValues();
            try
            {
                string str = Request["jsonData"];
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Ledger>(str);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    var tmpAttachmentColl = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.Account.LedgerDocumentCollections();
                    var filesColl = Request.Files;
                    int fInd = 0;
                    foreach (var v in tmpAttachmentColl)
                    {
                        HttpPostedFileBase file = filesColl["file" + fInd];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.DocumentColl.Add
                                (
                                 new Dynamic.BusinessEntity.Account.LedgerDocument()
                                 {
                                     Data = att.Data,
                                     DocPath = att.DocPath,
                                     DocumentTypeId = v.DocumentTypeId,
                                     Extension = att.Extension,
                                     Name = v.Name,
                                     Description = v.Description,
                                     LogDateTime = DateTime.Now
                                 }
                                );
                        }
                        else
                        {
                            beData.DocumentColl.Add(v);
                        }
                        fInd++;
                    }


                    if (BranchWiseMaster)
                    {
                        if (beData.BDId == 0)
                            beData.BDId = User.BranchId;
                    }

                    bool isModify = false;
                    if (beData.LedgerId > 0)
                    {
                        isModify = true;
                        var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId,User.HostName, User.DBName).GetFeeConfigurationById(0,this.AcademicYearId);
                        if (feeConfig != null)
                        {
                            if(beData.LedgerId==feeConfig.FixedStudentLedgerId ||
                                beData.LedgerId == feeConfig.FineLedgerId ||
                                beData.LedgerId == feeConfig.DiscountLedgerId ||
                                beData.LedgerId == feeConfig.FeeReceiptLedgerId ||
                                beData.LedgerId == feeConfig.SalesPartyLedgerId ||
                                beData.LedgerId == feeConfig.TaxLedgerId
                                )
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Ledger already used in fee configuration";                                
                            }
                            else
                            {
                                resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).ModifyFormData(beData);
                            }
                        }
                        else
                        {
                            resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).ModifyFormData(beData);
                        }
                        
                        

                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    //if (beData.LedgerId > 0)
                    //{
                    //    isModify = true;
                    //    var allowAction = checkSecurityEntity(Actions.Modify, (int)FormsEntity.Ledger, false, 0);
                    //    if (allowAction)
                    //    {
                    //        resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).ModifyFormData(beData);
                    //    }
                    //    else
                    //    {
                    //        resVal.IsSuccess = false;
                    //        resVal.ResponseMSG = "Access denied";
                    //        return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }

                    //}
                    //else
                    //{
                    //    var allowAction = checkSecurityEntity(Actions.Save, (int)FormsEntity.Ledger, false, 0);
                    //    if (allowAction)
                    //    {
                    //        resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).SaveFormData(beData);
                    //    }
                    //    else
                    //    {
                    //        resVal.IsSuccess = false;
                    //        resVal.ResponseMSG = "Access denied";
                    //        return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }

                    //}

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.LedgerId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Ledger;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.Name : "New " + auditLog.EntityId.ToString()) + beData.Name;
                        auditLog.AutoManualNo = IsNullStr(beData.Code);
                        SaveAuditLog(auditLog);
                    }
 

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
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Ledger, false)]

        public JsonNetResult DelLedger(int LedgerId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).DeleteById(User.UserId, LedgerId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAditionalCostTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            int id = 0;
            foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Setup.AditionalCostOnTheBasisOf)))
            {
                Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                beData.Id = id;
                beData.Text = str;
                dataColl.Add(beData);
                id++;
            }
            dataColl.IsSuccess = true;
            dataColl.ResponseMSG = GLOBALMSG.SUCCESS;


            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [HttpPost]
        public JsonNetResult GetTypeOfDutyTaxs()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            int id = 0;
            foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.TypeOfDutyTaxs)))
            {
                Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                beData.Id = id;
                beData.Text = str;
                dataColl.Add(beData);
                id++;
            }
            dataColl.IsSuccess = true;
            dataColl.ResponseMSG = GLOBALMSG.SUCCESS;


            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTypeOfIncomeExpenses()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            int id = 1;
            foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.TypeOfIncomeExpenses)))
            {
                Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                beData.Id = id;
                beData.Text = str;
                dataColl.Add(beData);
                id++;
            }
            dataColl.IsSuccess = true;
            dataColl.ResponseMSG = GLOBALMSG.SUCCESS;


            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        //[HttpGet]
        //public JsonNetResult GetAllSalesMan()
        //{
        //    var dataColl = new Dynamic.DataAccess.Account.AgentDB(User.HostName, User.DBName).getAllAgent(User.UserId, false);

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        //}

        #region "SalesMan / Agent"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Salesman, false)]

        public ActionResult Salesman()
        {
            ViewBag.Title = "Salesman";
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.ListOfSalesMan);
            return View();
        }
        [HttpGet]
        public JsonNetResult GetAllSalesMan()
        {
            Dynamic.BusinessEntity.Account.AgentCollections dataColl = new Dynamic.BusinessEntity.Account.AgentCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAreaMaster()
        {
            Dynamic.BusinessEntity.Account.AreaMasterCollections dataColl = new Dynamic.BusinessEntity.Account.AreaMasterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetCostCenter()
        {
            Dynamic.BusinessEntity.Account.CostCenterCollections dataColl = new Dynamic.BusinessEntity.Account.CostCenterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        public JsonNetResult GetCostCenterLst(TableFilter filter)
        {
            Dynamic.BusinessEntity.Common.CostCenterCollections dataColl = new Dynamic.BusinessEntity.Common.CostCenterCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Common.CostCenterDB(User.HostName, User.DBName).getCostCenterSearchList(filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetCostCenterCode(string name, int? CategoryId)
        {
            var dataColl = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getCode(User.UserId, name, CategoryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetCostCategories()
        {
            Dynamic.BusinessEntity.Account.CostCategoryCollections dataColl = new Dynamic.BusinessEntity.Account.CostCategoryCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Salesman, false)]

        public JsonNetResult SaveSalesMan()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Agent>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (BranchWiseMaster)
                        {
                            if (beData.BDId == 0)
                                beData.BDId = User.BranchId;
                        }

                        if (beData.AgentId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).SaveFormData(beData);
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
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Salesman, false)]

        public JsonNetResult getSalesmanById(int AgentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).getAgentById(User.UserId, AgentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Salesman, false)]

        public JsonNetResult deleteSalesmanById(int AgentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.Agent(User.HostName, User.DBName).DeleteById(AgentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "CostClass"

        [HttpPost]
        public JsonNetResult GetAllCostClassList()
        {
            var dataColl = new Dynamic.BusinessLogic.Account.CostClass(User.HostName, User.DBName).getAll(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count(), IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        [PermissionsAttribute(Actions.View, (int)ENTITIES.CostClass, false)]

        public ActionResult CostClass()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.CostClass, false)]

        public JsonNetResult SaveCostClass()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.CostClass>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.CostClassId == 0)
                            resVal = new Dynamic.BusinessLogic.Account.CostClass(User.HostName, User.DBName).SaveFormData(beData);
                        else if (beData.CostClassId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.CostClass(User.HostName, User.DBName).ModifyFormData(beData);
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
        }

        [HttpGet]
        public JsonNetResult GetAllCostClasss()
        {
            Dynamic.BusinessEntity.Account.CostClassCollections dataColl = new Dynamic.BusinessEntity.Account.CostClassCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.CostClassDB(User.HostName, User.DBName).getAllCostClassForSecurity(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetCostClasssRpt()
        {
            Dynamic.BusinessEntity.Account.CostClassCollections dataColl = new Dynamic.BusinessEntity.Account.CostClassCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.CostClassDB(User.HostName, User.DBName).getCostClassRpt(User.UserId);
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
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.CostClass, false)]
        public JsonNetResult DeleteCostClass(int CostClassId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.CostClass(User.HostName, User.DBName).DeleteById(CostClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        [HttpGet]
        public JsonNetResult GetAllDebtorTypeList()
        {
            Dynamic.BusinessEntity.Account.DebtorTypeCollections dataColl = new Dynamic.BusinessEntity.Account.DebtorTypeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.DebtorType(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllDebtorRouteList()
        {
            Dynamic.BusinessEntity.Account.DebtorRouteCollections dataColl = new Dynamic.BusinessEntity.Account.DebtorRouteCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).getAllAsList(User.UserId,null);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllLedgerGroupList()
        {
            Dynamic.BusinessEntity.Account.LedgerGroupCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllCurrencyList()
        {
            Dynamic.BusinessEntity.Account.CurrencyCollections dataColl = new Dynamic.BusinessEntity.Account.CurrencyCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Currency(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Currency"

        public ActionResult Currency()
        {
            return View();
        }
        [HttpGet]
        public JsonNetResult GetAllCurrency()
        {
            var dataColl = new Dynamic.DataAccess.Account.CurrencyDB(User.HostName, User.DBName).getAllCurrency(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetCurrencyRate(int CurrencyId)
        {
            try
            {
                var dataColl = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName).getCurrencyRate(CurrencyId, DateTime.Today);
                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }

        [HttpPost]     

        public JsonNetResult SaveUpdateCurrency()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Currency>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;
                        bool isModify = beData.CurrencyId > 0;

                        if (beData.CurrencyId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.Currency(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.Currency(User.HostName, User.DBName).SaveFormData(beData);


                        if (resVal.IsSuccess)
                        {
                            Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                            auditLog.TranId = (isModify ? beData.CurrencyId : resVal.RId);
                            auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.CurrencyMaster;
                            auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                            auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.Name : "New " + auditLog.EntityId.ToString()) + beData.Name;
                            auditLog.AutoManualNo = IsNullStr(beData.Code);
                            SaveAuditLog(auditLog);
                        }
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
        }


        [HttpPost]        
        public JsonNetResult getCurrencyById(int CurrencyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.Currency(User.HostName, User.DBName).getCurrencyById(User.UserId, CurrencyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult DeleteCurrency(int CurrencyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (CurrencyId < 2)
                {
                    resVal.ResponseMSG = "can't delete default Currency name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.Currency(User.HostName, User.DBName).DeleteById(CurrencyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        [HttpGet]
        public JsonNetResult GetAllAreaMasterList()
        {
            Dynamic.BusinessEntity.Account.AreaMasterCollections dataColl = new Dynamic.BusinessEntity.Account.AreaMasterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        #region "Ledger Group"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.LedgerGroup, false)]

        public ActionResult LedgerGroup()
        {
            return View();
        }


        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.LedgerGroup, false)]

        public JsonNetResult SaveUpdateLedgerGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.LedgerGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (BranchWiseMaster)
                    {
                        if (beData.BDId == 0)
                            beData.BDId = User.BranchId;
                    }

                    if (beData.LedgerGroupId > 1)
                        resVal = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).SaveFormData(beData);
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
        [HttpGet]
        public JsonNetResult GetBaseGroup()
        {
            Dynamic.BusinessEntity.Account.LedgerGroupCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllLedgerGroup()
        {
            Dynamic.BusinessEntity.Account.LedgerGroupCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.LedgerGroup, false)]

        public JsonNetResult GetLedgerGroupById(int LedgerGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).getLedgerGroupById(User.UserId, LedgerGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.LedgerGroup, false)]

        public JsonNetResult DeleteLedgerGroup(int LedgerGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (LedgerGroupId < 0)
                {
                    resVal.ResponseMSG = "can't delete ledger group ";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.LedgerGroup(User.HostName, User.DBName).DeleteById(User.UserId, LedgerGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        //[HttpGet]
        //public JsonNetResult GetAllLedgerGroup()
        //{
        //    var dataColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getAllLedgerGroupOfBalanceSheet(User.UserId);

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        //}

        #endregion

        #region "PDC Cheque"



        [HttpGet]
        public JsonNetResult GetPartyList()
        {
            Dynamic.BusinessEntity.Account.LedgerCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        public JsonNetResult PDCChequeBounce(Dynamic.BusinessEntity.Account.PDC beData)
        {

            beData.CUserId = User.UserId;
            beData.CreateBy = beData.CUserId;
            beData.UserName = User.UserName;

            var dataColl = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).ChequeBounce(beData);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PDCChequeCancel(Dynamic.BusinessEntity.Account.PDC beData)
        {

            beData.CUserId = User.UserId;
            beData.CreateBy = beData.CUserId;
            beData.UserName = User.UserName;
            var dataColl = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).ChequeCancel(beData);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult PDCCleared(Dynamic.BusinessEntity.Account.PDC beData)
        {

            beData.CUserId = User.UserId;
            beData.CreateBy = beData.CUserId;
            beData.UserName = User.UserName;

            var resVal = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).ChequeClear(beData);

           

            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "VoucherMode"
       

        public ActionResult VoucherMode(int tranId = 0)
        {
            ViewBag.TranId = tranId;
            return View();
        }
        [HttpPost]
        public JsonNetResult GetVoucherLst(TableFilter filter)
        {
            Dynamic.BusinessEntity.Common.VoucherCollections dataColl = new Dynamic.BusinessEntity.Common.VoucherCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Common.VoucherDB(User.HostName, User.DBName).getVoucherSearchList(filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpGet]
        public JsonNetResult GetVoucherList(int? voucherType)
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getAllVoucherShortDetails(User.UserId, voucherType,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResultWithEnum GetAllVoucherWithEnum()
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getAllVoucherShortDetails(User.UserId, null,null);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllVoucherList()
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getAllVoucherShortDetails(User.UserId, null, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetVoucherModeById(int voucherId)
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getVoucherModeByVoucherId(User.UserId, voucherId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetVMForDayBook(int voucherId)
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getVoucherForDayBook(User.UserId, voucherId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpGet]
        public JsonNetResult GetAllVoucherData()
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

        [HttpPost]
        public JsonNetResult GetVoucherNo(int voucherId, int costClassId, DateTime? voucherDate)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.BankQuotation(User.UserId, User.HostName, User.DBName).getAutoNumber(voucherId, costClassId, voucherDate);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetVoucherMandetoryFields(int voucherType)
        {
            var dataColl = new Dynamic.DataAccess.Setup.AditionalInvoiceDetailsMandetoryDB(User.HostName, User.DBName).getMandetoryFields(voucherType);
            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        [HttpPost]        
        public JsonNetResult SaveVoucherMode()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.VoucherMode>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    bool isModify = beData.VoucherId > 0;
                    if (beData.VoucherId > 0)
                        resVal = new Dynamic.BusinessLogic.Account.VoucherMode(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Account.VoucherMode(User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.VoucherId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Product;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.VoucherName : "New " + auditLog.EntityId.ToString()) + beData.VoucherName;
                        auditLog.AutoManualNo = IsNullStr(beData.VoucherName);
                        SaveAuditLog(auditLog);
                    }
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
        [HttpGet]
        public JsonNetResult GetVoucherTypes()
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

                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };

            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }

        #endregion

        #region"Bank Gaurantee"

        
        public ActionResult BankGaurantee()
        {
            return View();
        }
        [HttpPost]        
        public JsonNetResult SaveBankGuarantee()
        {
            string photoLocation = "/Attachments/account/bg";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.BGDetails>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.UserId = User.UserId;

                    var tmpAttachmentColl = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    var filesColl = Request.Files;
                    int fInd = 0;
                    foreach (var v in tmpAttachmentColl)
                    {
                        HttpPostedFileBase file = filesColl["file" + fInd];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.DocumentColl.Add
                                (
                                 new Dynamic.BusinessEntity.Account.LedgerDocument()
                                 {
                                     Data = att.Data,
                                     DocPath = att.DocPath,
                                     DocumentTypeId = v.DocumentTypeId,
                                     Extension = att.Extension,
                                     Name = v.Name,
                                     Description = v.Description,
                                     LogDateTime = DateTime.Now
                                 }
                                );
                        }
                        else
                        {
                            beData.DocumentColl.Add(v);
                        }
                        fInd++;
                    }


                    if (beData.TranId > 0)
                        resVal = new Dynamic.BusinessLogic.Account.BGDetails(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Account.BGDetails(User.HostName, User.DBName).SaveFormData(beData);
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
        [HttpGet]
        public JsonNetResult GetBankGuarantee()
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
        [HttpPost]        
        public JsonNetResult GetBankGuaranteeById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.BGDetails(User.HostName, User.DBName).getBGDetailsById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]        
        public JsonNetResult DeleteBankGuarantee(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.BGDetails(User.HostName, User.DBName).DeleteById(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        #region "PDC Details"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PDCCheque, false)]

        public ActionResult PDCDetails()
        {
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.PDCCheque, false)]

        public JsonNetResult GetPDCById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).getPDCById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetPDC()
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
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PDCCheque, false)]

        public JsonNetResult SaveUpdatePDC()
        {
            string photoLocation = "/Attachments/account/pdc";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.PDC>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreateBy = beData.CUserId;
                    beData.ModifyBy = beData.CUserId;
                    beData.BranchId = User.BranchId;
                    var tmpAttachmentColl = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    var filesColl = Request.Files;
                    int fInd = 0;
                    foreach (var v in tmpAttachmentColl)
                    {
                        HttpPostedFileBase file = filesColl["file" + fInd];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.DocumentColl.Add
                                (
                                 new Dynamic.BusinessEntity.Account.LedgerDocument()
                                 {
                                     Data = att.Data,
                                     DocPath = att.DocPath,
                                     DocumentTypeId = v.DocumentTypeId,
                                     Extension = att.Extension,
                                     Name = v.Name,
                                     Description = v.Description,
                                     LogDateTime = DateTime.Now
                                 }
                                );
                        }
                        else
                        {
                            beData.DocumentColl.Add(v);
                        }
                        fInd++;
                    }

                    if (beData.TranId > 0)
                        resVal = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).SaveFormData(beData);
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
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.PDCCheque, false)]

        public JsonNetResult deletePDC(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.PDCDetails(User.HostName, User.DBName).DeleteById(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "ODC Details"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ODCCheque, false)]

        public ActionResult ODCDetails()
        {
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.ODCCheque, false)]

        public JsonNetResult GetODCById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.ODCDetails(User.HostName, User.DBName).getPDCById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ODCCheque, false)]

        public JsonNetResult deleteODC(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.ODCDetails(User.HostName, User.DBName).DeleteById(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ODCCheque, false)]
        public JsonNetResult SaveUpdateODC()
        {
            string photoLocation = "/Attachments/account/bg";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.PDC>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.CreateBy = beData.CUserId;
                    beData.ModifyBy = beData.CUserId;
                    var tmpAttachmentColl = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    var filesColl = Request.Files;
                    int fInd = 0;
                    foreach (var v in tmpAttachmentColl)
                    {
                        HttpPostedFileBase file = filesColl["file" + fInd];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.DocumentColl.Add
                                (
                                 new Dynamic.BusinessEntity.Account.LedgerDocument()
                                 {
                                     Data = att.Data,
                                     DocPath = att.DocPath,
                                     DocumentTypeId = v.DocumentTypeId,
                                     Extension = att.Extension,
                                     Name = v.Name,
                                     Description = v.Description,
                                     LogDateTime = DateTime.Now
                                 }
                                );
                        }
                        else
                        {
                            beData.DocumentColl.Add(v);
                        }
                        fInd++;
                    }

                    if (beData.TranId > 0)
                        resVal = new Dynamic.BusinessLogic.Account.ODCDetails(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Account.ODCDetails(User.HostName, User.DBName).SaveFormData(beData);
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

        [HttpGet]
        public JsonNetResult GetODC()
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

        #endregion

        #region"Cost Center"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.CostCenter, false)]

        public ActionResult CostCenter()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.CostCenter, false)]

        public JsonNetResult SaveUpdateCostCenter()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.CostCenter>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (BranchWiseMaster)
                        {
                            if (beData.BDId == 0)
                                beData.BDId = User.BranchId;
                        }

                        if (beData.CostCenterId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).SaveFormData(beData);
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
        }

        [HttpGet]
        public JsonNetResult GetAllCostCenter()
        {
            Dynamic.BusinessEntity.Account.CostCenterCollections dataColl = new Dynamic.BusinessEntity.Account.CostCenterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.CostCenter, false)]

        public JsonNetResult getCostCenterById(int CostCenterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getCostCenterById(User.UserId, CostCenterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.CostCenter, false)]

        public JsonNetResult deleteCostCenter(int CostCenterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (CostCenterId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).DeleteById(CostCenterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]

        public JsonNetResult GetSalesCostCenter()
        {
            var dataColl = new Dynamic.DataAccess.Common.CostCenterDB(User.HostName, User.DBName).getSalesCostCenter(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetPurchaseCostCenter()
        {
            var dataColl = new Dynamic.DataAccess.Common.CostCenterDB(User.HostName, User.DBName).getPurchaseCostCenter(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpGet]
        public JsonNetResult GetAllCostCategoryLisst()
        {
            Dynamic.BusinessEntity.Account.CostCategoryCollections dataColl = new Dynamic.BusinessEntity.Account.CostCategoryCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetLedgerList()
        {
            Dynamic.BusinessEntity.Account.LedgerCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Ledger(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpGet]
        public JsonNetResult GetAllDimension()
        {
            Dynamic.BusinessEntity.Account.DimensionCollections dataColl = new Dynamic.BusinessEntity.Account.DimensionCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.Dimension(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllCostCenterDepartment()
        {
            Dynamic.BusinessEntity.Account.CostCenterDepartmentCollections dataColl = new Dynamic.BusinessEntity.Account.CostCenterDepartmentCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCenterDepartment(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "CostClass"

        [HttpGet]
        public JsonNetResult GetCostClassForEntry()
        {
            var dataColl = new Dynamic.DataAccess.Account.CostClassDB(User.HostName, User.DBName).getAllCostClass(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region"DebtorCreditorsRoute"

        
        public ActionResult DebtorsCreditorsRoute()
        {

            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveUpdateDebtorsCreditorsRoute()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.DebtorRoute>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.DebtorRouteId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).SaveFormData(beData);
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
        }
        [HttpGet]
        public JsonNetResult GetAllDebtorsCreditorsRoute()
        {
            Dynamic.BusinessEntity.Account.DebtorRouteCollections dataColl = new Dynamic.BusinessEntity.Account.DebtorRouteCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).getAllAsList(User.UserId,null);
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
        
        public JsonNetResult getDebtorsCreditorsRouteById(int DebtorRouteId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).getDebtorRouteById(User.UserId, DebtorRouteId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult DeleteDebtorsRouteType(int DebtorRouteId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (DebtorRouteId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.DebtorRoute(User.HostName, User.DBName).DeleteFormData(DebtorRouteId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Area Master"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AreaMaster, false)]

        public ActionResult AreaMaster()
        {

            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AreaMaster, false)]

        public JsonNetResult SaveAreaMaster()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.AreaMaster>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.AreaId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).SaveFormData(beData);
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
        }

        [HttpGet]
        public JsonNetResult GetAllAreaType()
        {
            Dynamic.BusinessEntity.Account.AreaMasterCollections dataColl = new Dynamic.BusinessEntity.Account.AreaMasterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAreaTypes()
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
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetSalesmanLevel()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(Dynamic.BusinessEntity.Account.AGENTLEVELS)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
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
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.AreaMaster, false)]
        public JsonNetResult getAreaMasterByIdd(int AreaId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).getAreaMasterById(AreaId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.AreaMaster, false)]
        public JsonNetResult deleteAreaMasterById(int AreaId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (AreaId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.AreaMaster(User.HostName, User.DBName).DeleteById(AreaId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "CostCategory"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.CostCategory, false)]
        public ActionResult CostCategory()
        {

            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.CostCategory, false)]
        public JsonNetResult SaveCostCategory()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.CostCategory>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (BranchWiseMaster)
                        {
                            if (beData.BDId == 0)
                                beData.BDId = User.BranchId;
                        }

                        if (beData.CostCategoryId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).SaveFormData(beData);
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
        }
        [HttpGet]
        public JsonNetResult GetAllParentCategoryList()
        {
            Dynamic.BusinessEntity.Account.CostCategoryCollections dataColl = new Dynamic.BusinessEntity.Account.CostCategoryCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllCostCategoryList()
        {
            Dynamic.BusinessEntity.Account.CostCategoryCollections dataColl = new Dynamic.BusinessEntity.Account.CostCategoryCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).getAllAsList(User.UserId);
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
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.CostCategory, false)]
        public JsonNetResult getCostCategoryById(int CostCategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).getCostCategoryById(User.UserId, CostCategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.CostCategory, false)]
        public JsonNetResult deleteCostCategory(int CostCategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (CostCategoryId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.CostCategory(User.HostName, User.DBName).DeleteById(CostCategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Narration"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.NarrationMaster, false)]
        public ActionResult NarrationMaster()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpGet]
        public JsonNetResult GetVoucherWiseNarration(int voucherType)
        {
            List<string> dataColl = new Dynamic.DataAccess.Account.NarrationMasterDB(User.HostName, User.DBName).getNarrationMasterAsList(User.UserId, (Dynamic.BusinessEntity.Account.VoucherTypes)voucherType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.NarrationMaster, false)]
        public JsonNetResult SaveNarrationMaster()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.NarrationMaster>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.NarrationMasterId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.NarrationMaster(User.HostName, User.DBName).ModifyFormData(beData);
                        else
                            resVal = new Dynamic.BusinessLogic.Account.NarrationMaster(User.HostName, User.DBName).SaveFormData(beData);
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
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.NarrationMaster, false)]
        public JsonNetResult GetNarrationMasterById(int NarrationMasterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.NarrationMaster(User.HostName, User.DBName).getNarrationMasterById(User.UserId, NarrationMasterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.NarrationMaster, false)]
        public JsonNetResult DeleteNarrationMaster(int NarrationMasterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (NarrationMasterId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Narration Master name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Account.NarrationMaster(User.HostName, User.DBName).DeleteById(NarrationMasterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllNarrationMaster()
        {
            Dynamic.BusinessEntity.Account.NarrationMasterCollections dataColl = new Dynamic.BusinessEntity.Account.NarrationMasterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.NarrationMaster(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion



        #region "Cheque Log Details"

        public ActionResult ChequeDetail()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PDCCheque, false)]
        public JsonNetResult SaveChequeDetail()
        {
            {
                ResponeValues resVal = new ResponeValues();
                try
                {
                    var beData = DeserializeObject<Dynamic.BusinessEntity.Account.ChequeLogDetail>(Request["jsonData"]);
                    if (beData != null)
                    {
                        beData.CUserId = User.UserId;

                        if (beData.TranId == 0)
                            resVal = new Dynamic.BusinessLogic.Account.ChequeLogDetail(User.HostName, User.DBName).SaveFormData(beData);
                        else if (beData.TranId > 0)
                            resVal = new Dynamic.BusinessLogic.Account.ChequeLogDetail(User.HostName, User.DBName).ModifyFormData(beData);
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
        }

        [HttpGet]
        public JsonNetResult GetAllChequeDetail()
        {
            Dynamic.BusinessEntity.Account.CostClassCollections dataColl = new Dynamic.BusinessEntity.Account.CostClassCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.CostClassDB(User.HostName, User.DBName).getAllCostClassForSecurity(User.UserId);
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
        public JsonNetResult DeleteChequeDetail(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Account.ChequeLogDetail(User.HostName, User.DBName).DeleteById(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.BranchWiseLedgerOpening, false)]
        public ActionResult BranchWiseLedgerOpening()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.CostCenter, false)]
        public ActionResult CostCenterLedgerOpening()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateCostCenterLedgerOpening(Dynamic.BusinessEntity.Account.CostCenterOpeningCollections dataColl)
        {
            var usr = User;
            foreach (var d in dataColl)
            {
                d.UserId = usr.UserId;
            }
            var resVal = new Dynamic.DataAccess.Account.CostCenterDB(User.HostName, User.DBName).UpdateAllCostCenterOpening(dataColl);

            return new JsonNetResult() { Data = resVal, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }        
        public ActionResult BillWiseLedgerOpening()
        {
            return View();
        }

        [HttpGet]
        public JsonNetResult GetVoucherListByType(string voucherTypeColl)
        {
            var dataColl = new Dynamic.DataAccess.Account.VoucherModeDB(User.HostName, User.DBName).getAllVoucherShortDetails(User.UserId, null, voucherTypeColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [PermissionsAttribute(Actions.View, (int)ENTITIES.BudgetSetup, false)]
        public ActionResult Budget()
        {
            ViewBag.Title = "Budget";
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.LedgerWiseBudget);
            return View();
        }
        [HttpPost]
        public JsonNetResult GetBudget(int DateType, int CostClassId, int BranchId, int MonthId, int BudgetType, int? LedgerGroupId, int? LedgerId)
        {
            Dynamic.BusinessEntity.Account.BudgetCollections dataColl = new Dynamic.BusinessEntity.Account.BudgetCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.BudgetDB(User.HostName, User.DBName).getDataForBudget(User.UserId, CostClassId, BranchId, DateType, MonthId, BudgetType, LedgerGroupId, LedgerId);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveBudget()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.BudgetCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new Dynamic.DataAccess.Account.BudgetDB(User.HostName, User.DBName).SaveBudget(User.UserId, beData);
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

        [HttpGet]
        public JsonNetResult GetExpensesLedger()
        {
            Dynamic.BusinessEntity.Account.LedgerCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Account.LedgerDB(User.HostName, User.DBName).getExpensesLedger(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllFundTransferType()
        {
            Dynamic.BusinessEntity.Account.FundTransferTypeCollections dataColl = new Dynamic.BusinessEntity.Account.FundTransferTypeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.FundTransferType(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetCreditorGroup()
        {
            var dataColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getCreditorGroup(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpGet]
        public JsonNetResult GetDebtorGroup()
        {
            var dataColl = new Dynamic.DataAccess.Account.LedgerGroupDB(User.HostName, User.DBName).getDebtorGroup(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

    }
}