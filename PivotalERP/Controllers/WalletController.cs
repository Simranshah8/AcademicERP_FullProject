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
    public class WalletController : APIBaseController
    {

        #region "GetPaymentGateWays"

        // POST GetPaymentGateWays
        /// <summary>
        ///  Get PaymentGateWays                 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AcademicLib.BE.Wallet.PaymentGateWayCollections))]
        public IHttpActionResult GetPaymentGateWays()
        {
            AcademicLib.BE.Wallet.PaymentGateWayCollections dataColl = new AcademicLib.BE.Wallet.PaymentGateWayCollections();
            try
            {
                dataColl = new AcademicLib.BL.Wallet.OnlinePayment(UserId, hostName, dbName).GetPaymentGateWays();
                return Json(dataColl, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                        {
                                            "GateWay","PrivateKey","PublicKey","SchoolId","UserName","Pwd","Name","IconPath","MerchantId","MerchantName"
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


        // Put api/SaveOnlinePayment
        /// <summary>
        ///  = Payment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult OnlinePayment([FromBody]AcademicLib.BE.Wallet.OnlinePayment data)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (data == null)
                {
                    resVal.ResponseMSG = "No form data found";
                }              
                else
                {
                    data.CUserId = UserId;

                    AcademicLib.BE.Wallet.PAYMENTGATEWAYS gateWay = AcademicLib.BE.Wallet.PAYMENTGATEWAYS.E_SEWA;

                    if (data.PaymentGateWayId != 0)
                        gateWay = (AcademicLib.BE.Wallet.PAYMENTGATEWAYS)data.PaymentGateWayId;

                    if(!string.IsNullOrEmpty(data.PrivateKey))
                    {
                        gateWay = AcademicLib.BE.Wallet.PAYMENTGATEWAYS.KHALTI;
                        string errorMSG = "";
                        ResponeValues isVerify=VerifyPaymentOnly(data.PrivateKey, data.ReferenceId, data.Amount, ref errorMSG);
                        if (!isVerify.IsSuccess)
                        {
                            return Json(isVerify, new JsonSerializerSettings
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

                    var refUptoMonthId = data.UptoMonthId;
                    var forMonthId = data.UptoMonthId;

                    var recBL = new AcademicLib.BL.Fee.Transaction.FeeReceipt(data.CUserId, hostName, dbName);

                    if (!data.UptoMonthId.HasValue || data.UptoMonthId==0)
                    {
                        data.UptoMonthId = 12;
                        refUptoMonthId = 12;
                    }

                    var academicYearId = this.AcademicYearId;
                    var feeDues = recBL.getDuesDetailsForWallet(academicYearId,data.UptoMonthId);

                    int count =(feeDues.EndMonthId.HasValue ? feeDues.EndMonthId.Value :  12 );
                    while (count>0)
                    {
                        if (feeDues.TotalDues < data.Amount)
                        {
                            data.UptoMonthId = data.UptoMonthId + 1;
                            feeDues = recBL.getDuesDetailsForWallet(academicYearId, data.UptoMonthId);
                        }
                        else
                            break;
                        count--;
                    }

                    if (feeDues.IsSuccess)
                    {
                        double totalAmt = data.Amount;

                        var costClassColl = new Dynamic.DataAccess.Account.CostClassDB(hostName, dbName).getAllCostClass(UserId);
                        var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(data.CUserId, hostName, dbName).GetFeeConfigurationById(0, AcademicYearId);
                        int? walletLedgerId = null;

                        try
                        {
                            var paymentGateWaysColl = new AcademicLib.BL.Wallet.PaymentGateway(data.CUserId, hostName, dbName).GetAllPaymentGateway(0);

                            if (paymentGateWaysColl != null)
                            {
                                var selectedGateWay = paymentGateWaysColl.FirstOrDefault(p1 => p1.ForGateWay == gateWay);
                                if (selectedGateWay != null)
                                {
                                    walletLedgerId = selectedGateWay.LedgerId;
                                    data.PaymentGateWayId = selectedGateWay.TranId.Value;
                                }                                    
                            }
                        }
                        catch { }

                        var findAY = new AcademicLib.BL.Academic.Creation.AcademicYear(UserId, hostName, dbName).GetAcademicYearById(0, academicYearId);

                        AcademicLib.BE.Fee.Transaction.FeeReceipt feeReceipt = new AcademicLib.BE.Fee.Transaction.FeeReceipt();
                        feeReceipt.AcademicYearId = academicYearId;
                        feeReceipt.VoucherDate = DateTime.Today;
                        feeReceipt.CUserId = data.CUserId;
                        feeReceipt.CostClassId =(findAY!=null && findAY.CostClassId.HasValue && findAY.CostClassId>0 ? findAY.CostClassId.Value : costClassColl[0].CostClassId);
                        feeReceipt.DetailsColl = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetailsCollections();
                        feeReceipt.Narration = data.Notes;
                        //feeReceipt.PaidUpToMonth =(!refUptoMonthId.HasValue || refUptoMonthId==0 ? 1 : refUptoMonthId.Value);
                        feeReceipt.PaidUpToMonth = (!forMonthId.HasValue || forMonthId == 0 ?  (feeDues.StartMonthId.HasValue ? feeDues.StartMonthId.Value : 1)  : forMonthId.Value);
                        feeReceipt.ReceiptAsLedgerId = walletLedgerId.HasValue ? walletLedgerId.Value : (feeConfig.FeeReceiptLedgerId.HasValue ? feeConfig.FeeReceiptLedgerId.Value : 1);
                        feeReceipt.ReceivableAmt = feeDues.FeeItemWiseDuesColl.Sum(p1 => p1.TotalDues);
                        feeReceipt.ReceivedAmt = totalAmt;
                        feeReceipt.RefNo = data.ReferenceId;
                        feeReceipt.StudentId = feeDues.StudentId;
                        feeReceipt.TotalDues = feeReceipt.ReceivableAmt;
                        feeReceipt.TranId = 0;
                        feeReceipt.AfterReceivedDues = feeReceipt.ReceivableAmt-feeReceipt.ReceivedAmt;

                        feeReceipt.ClassId =data.ClassId.HasValue && data.ClassId>0 ? data.ClassId.Value : feeDues.ClassId;
                        feeReceipt.ClassName = feeDues.ClassName;
                        feeReceipt.SemesterId = data.SemesterId.HasValue && data.SemesterId>0 ? data.SemesterId.Value : feeDues.SemesterId;
                        feeReceipt.ClassYearId = data.ClassYearId.HasValue && data.ClassYearId>0 ? data.ClassYearId.Value : feeDues.ClassYearId;
                        feeReceipt.StudentName = feeDues.Name;

                        if (feeReceipt.ClassId == 0)
                            feeReceipt.ClassId = null;

                        if (feeReceipt.SemesterId == 0)
                            feeReceipt.SemesterId = null;

                        if (feeReceipt.ClassYearId == 0)
                            feeReceipt.ClassYearId = null;
                         

                        foreach (var v in feeDues.FeeItemWiseDuesColl)
                        {
                            AcademicLib.BE.Fee.Transaction.FeeReceiptDetails receiptDetails = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();
                            receiptDetails.FeeItemId = v.FeeItemId;
                            receiptDetails.FeeItemName = v.FeeItemName;
                            receiptDetails.ReceivableAmt = v.TotalDues;
                            receiptDetails.PreviousDues = v.PreviousDues;
                            receiptDetails.CurrentDues = v.CurrentDues;
                            
                            if (v.TotalDues >= totalAmt)
                            {
                                receiptDetails.ReceivedAmt = totalAmt;
                                receiptDetails.AfterReceivedDues = receiptDetails.PreviousDues + receiptDetails.CurrentDues - receiptDetails.ReceivedAmt;
                                feeReceipt.DetailsColl.Add(receiptDetails);
                                totalAmt = 0;
                                break;
                            }else if (v.TotalDues < totalAmt)
                            {
                                receiptDetails.ReceivedAmt = v.TotalDues;
                                receiptDetails.AfterReceivedDues = receiptDetails.PreviousDues + receiptDetails.CurrentDues - receiptDetails.ReceivedAmt;
                                feeReceipt.DetailsColl.Add(receiptDetails);
                                totalAmt = totalAmt - v.TotalDues;
                            }


                            if (totalAmt <= 0)
                                break;

                        }

                        if (totalAmt > 0)
                        {
                            var feeItemColl = new AcademicLib.BL.Fee.Creation.FeeItem(UserId, hostName, dbName).GetAllFeeItem(0);
                            if (feeConfig.AdvanceFeeItemId.HasValue && feeConfig.AdvanceFeeItemId.Value > 0)
                            {
                                var findFeeItem = feeItemColl.Find(p1 => p1.FeeItemId == feeConfig.AdvanceFeeItemId.Value);
                                if (findFeeItem != null)
                                {
                                    AcademicLib.BE.Fee.Transaction.FeeReceiptDetails receiptDetails = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();
                                    receiptDetails.FeeItemId = findFeeItem.FeeItemId;
                                    receiptDetails.FeeItemName = findFeeItem.Name;
                                    receiptDetails.ReceivableAmt = 0;
                                    receiptDetails.PreviousDues = 0;
                                    receiptDetails.CurrentDues = 0;
                                    receiptDetails.ReceivedAmt = totalAmt;
                                    receiptDetails.AfterReceivedDues = -totalAmt;
                                    feeReceipt.DetailsColl.Add(receiptDetails);
                                }

                            }
                            else
                            {
                                if (feeItemColl != null && feeItemColl.Count > 0)
                                {
                                    AcademicLib.BE.Fee.Creation.FeeItem findFeeItem = null;

                                    if (findFeeItem == null)
                                        findFeeItem = feeItemColl.Find(p1 => p1.OrderNo == 1);

                                    if (feeItemColl == null)
                                        findFeeItem = feeItemColl[0];

                                    if (findFeeItem != null)
                                    {
                                        AcademicLib.BE.Fee.Transaction.FeeReceiptDetails receiptDetails = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();
                                        receiptDetails.FeeItemId = findFeeItem.FeeItemId;
                                        receiptDetails.FeeItemName = findFeeItem.Name;
                                        receiptDetails.ReceivableAmt = 0;
                                        receiptDetails.PreviousDues = 0;
                                        receiptDetails.CurrentDues = 0;
                                        receiptDetails.ReceivedAmt = totalAmt;
                                        receiptDetails.AfterReceivedDues = -totalAmt;
                                        feeReceipt.DetailsColl.Add(receiptDetails);
                                    }
                                }

                            }
                            
                          
                          
                        }

                        resVal=recBL.SaveFormData(AcademicYearId, feeReceipt,false);
                        Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;
                        if (resVal.IsSuccess && resVal.RId > 0)
                        {
                            data.ReceiptTranId = resVal.RId;
                            int entityId = (int)Dynamic.BusinessEntity.Global.FormsEntity.FeeReceipt;
                            comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, entityId, 0, data.ReceiptTranId.Value);

                            if (comDet!=null && !string.IsNullOrEmpty(comDet.CompanyName))
                            {
                                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, true);
                                if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                                {
                                    return BadRequest("No Report Templates Found");
                                }

                                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                                System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                                paraColl.Add("UserId", data.CUserId.ToString());
                                paraColl.Add("TranId", data.ReceiptTranId.Value.ToString());
                                paraColl.Add("UserName", User.Identity.Name);
                                Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                                _rdlReport.ComDet = comDet;
                                _rdlReport.ConnectionString = ConnectionString;
                                _rdlReport.RenderType = "pdf";
                                _rdlReport.NoShow = false;

                                if (!string.IsNullOrEmpty(template.Path))
                                {
                                    _rdlReport.ReportFile = reportTemplate.GetPath(template);
                                    string basePath = "print-tran-log//" + Guid.NewGuid().ToString() + ".pdf";
                                    if (_rdlReport.Object != null)
                                    {                                        
                                        string sFile = GetPath("~//" + basePath);
                                        reportTemplate.SavePDF(_rdlReport.Object, sFile);
                                        resVal.IsSuccess = true;
                                        resVal.ResponseMSG = basePath;

                                        Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                                        printLog.UserId = data.CUserId;
                                        printLog.UserName = User.Identity.Name;
                                        printLog.TranId = data.ReceiptTranId.Value;
                                        printLog.AutoManualNo = data.ReceiptTranId.Value.ToString();
                                        printLog.SystemUser = "StudentAPI";
                                        printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                                        printLog.EntityId = entityId;
                                        printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                                        printLog.LogDate = DateTime.Now;
                                        printLog.LogText = "Print Voucher of tranid=" + data.ReceiptTranId.Value.ToString();
                                        printLog.MacAddress = "";
                                        printLog.PCName = "";
                                        var printRes = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).SaveTransactionPrintAuditLog(printLog);                                         
                                    }

                                    data.ReceiptPath = basePath;
                                    resVal = new AcademicLib.BL.Wallet.OnlinePayment(data.CUserId, hostName, dbName).SavePaymentLog(data);
                                    if (resVal.IsSuccess)
                                    {
                                        resVal.ResponseMSG = basePath;
                                    }

                                }
                            }

                          
                        } 
                    }
                    else
                    {
                        resVal = feeDues;
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



        // Put api/KhaltiPayment
        /// <summary>
        ///  = Payment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult KhaltiPayment([FromBody] JObject data)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (data == null)
                {
                    resVal.ResponseMSG = "No form data found";                    
                }else if (!data.ContainsKey("MobileNo"))
                {
                    resVal.ResponseMSG = "Pls Enter Mobile No.";
                }
                else if (!data.ContainsKey("PinNo"))
                {
                    resVal.ResponseMSG = "Pls Enter PinNo No.";
                }
                else if (!data.ContainsKey("Amount"))
                {
                    resVal.ResponseMSG = "Pls Enter Amount";
                }
                else
                {
                    var beData = new
                    {
                        UserId = UserId,
                        MobileNo = Convert.ToString(data["MobileNo"]),
                        PinNo = Convert.ToString(data["PinNo"]),
                        Amount = Convert.ToDouble(data["Amount"]),
                        IpAddress = GetClientIp()
                    };

                    if (string.IsNullOrEmpty(beData.MobileNo) || beData.MobileNo.Length != 10)
                    {
                        resVal.ResponseMSG = "Invalid Mobile No";
                    }
                    else if (beData.Amount <= 0)
                    {
                        resVal.ResponseMSG = "Invalid Amount. Please ! Enter Greater then zero and round amount only";
                    }else if(string.IsNullOrEmpty(khalti_public_key) || string.IsNullOrEmpty(khalti_private_key))
                    {
                        resVal.ResponseMSG = "Khalti PublicKey And PrivateKey Was Not Setup";
                    }
                    else
                    {

                        try
                        {
                           

                            var para = new
                            {
                                public_key = khalti_public_key,
                                mobile = beData.MobileNo,
                                transaction_pin = beData.PinNo,
                                amount = Convert.ToInt32(beData.Amount) * 100,
                                product_identity = "AcademicERP/id-2",
                                product_name = "Dynamic AcademicERP 2.0",
                                product_url = "https://mydynamicerp.com"
                            };

                            string url = "https://khalti.com/api/v2/payment/initiate/";
                            Models.APIRequest request = new Models.APIRequest(url, "POST", "application/json");

                            DateTime requestDateTime = DateTime.Now;
                            var responseData = request.Execute<AcademicLib.BE.Wallet.WalletResponse>(para);
                            DateTime responseDateTime = DateTime.Now;

                            if (responseData != null)
                            {
                                if (responseData is AcademicLib.BE.Wallet.WalletResponse)
                                {
                                    AcademicLib.BE.Wallet.WalletResponse resBeData = (AcademicLib.BE.Wallet.WalletResponse)responseData;
                                    if (!string.IsNullOrEmpty(resBeData.token))
                                    {
                                        var newBeData = new AcademicLib.BE.Wallet.WalletRequest
                                        {
                                            Amount = para.amount,
                                            IpAddress = beData.IpAddress,
                                            MobileNo = beData.MobileNo,
                                            PrivateKey = khalti_private_key,
                                            PublicKey = khalti_public_key,
                                            ProductId = para.product_identity,
                                            ProductName = para.product_name,
                                            ProductURL = para.product_url,
                                            RequestTime = requestDateTime,
                                            ResponseTime = responseDateTime,
                                            Token = resBeData.token,
                                            Url = url,
                                            UserId = UserId
                                        };

                                        resVal = new AcademicLib.BL.Wallet.Khalti(UserId,hostName,dbName).SaveWalletLog(newBeData);

                                    }
                                    else
                                    {
                                        resVal.IsSuccess = false;
                                        resVal.ResponseMSG = "Unable To do process";
                                    }

                                }
                                else
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = responseData.ToString();
                                }


                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Unable To do process";
                            }

                        }
                        catch (Exception eee)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = eee.Message;
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


        // Put api/KhaltiPaymentConfirm
        /// <summary>
        ///  Check Payment Confirmation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult KhaltiPaymentConfirm([FromBody] JObject data)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                if (data == null)
                {
                    resVal.ResponseMSG = "No form data found";
                }
                else if (!data.ContainsKey("PublicKey"))
                {
                    resVal.ResponseMSG = "Pls Enter PublicKey.";
                }
                else if (!data.ContainsKey("OTP"))
                {
                    resVal.ResponseMSG = "Pls Enter OTP.";
                }
                else if (!data.ContainsKey("PinNo"))
                {
                    resVal.ResponseMSG = "Pls Enter PinNo.";
                }
                else
                {
                    try
                    {

                        var beData = new
                        {
                            NewGuiId = Convert.ToString(data["PublicKey"]),
                            OTP = Convert.ToString(data["OTP"]),
                            PinNo = Convert.ToString(data["PinNo"])
                        };

                        AcademicLib.BE.Wallet.WalletRequest wallet = new AcademicLib.BL.Wallet.Khalti(UserId,hostName,dbName).getWalletToken(null,null,beData.NewGuiId);
                        if (wallet == null || string.IsNullOrEmpty(wallet.Token))
                        {
                            resVal.ResponseMSG = "Invalid Public Key ( Token )";
                        }
                        else
                        {
                            var para = new
                            {
                                public_key = khalti_public_key,
                                token = wallet.Token,
                                confirmation_code = beData.OTP,
                                transaction_pin = beData.PinNo
                            };

                            string url = "https://khalti.com/api/v2/payment/confirm/";
                            Models.APIRequest request = new Models.APIRequest(url, "POST", "application/json");

                            DateTime requestDateTime = DateTime.Now;
                            var responseData = request.Execute<AcademicLib.BE.Wallet.WalletResponse>(para);
                            DateTime responseDateTime = DateTime.Now;

                            if (responseData != null)
                            {
                                if (responseData is AcademicLib.BE.Wallet.WalletResponse)
                                {
                                    AcademicLib.BE.Wallet.WalletResponse resBeData = (AcademicLib.BE.Wallet.WalletResponse)responseData;
                                    if (!string.IsNullOrEmpty(resBeData.token))
                                    {
                                        var newBeData = new AcademicLib.BE.Wallet.WalletRequest
                                        {
                                            Amount = resBeData.amount,
                                            TranId = wallet.TranId,
                                            PrivateKey = khalti_private_key,
                                            PublicKey = khalti_public_key,
                                            ProductId = resBeData.product_identity,
                                            ProductName = resBeData.product_name,
                                            RequestTime = requestDateTime,
                                            ResponseTime = responseDateTime,
                                            Token = resBeData.token,
                                            Url = url,
                                            UserId = UserId
                                        };

                                        resVal = new AcademicLib.BL.Wallet.Khalti(UserId, hostName, dbName).SaveWalletConfirmationLog(newBeData);

                                        if (resVal.IsSuccess)
                                        {
                                            string errorMSG = "";
                                            bool verify = VerifyPayment(wallet.TranId, khalti_private_key, wallet.Token, wallet.Amount, ref errorMSG);
                                            if (!verify)
                                            {
                                                resVal.IsSuccess = false;
                                                resVal.ResponseMSG = errorMSG;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        resVal.IsSuccess = false;
                                        resVal.ResponseMSG = "Unable To do process";
                                    }

                                }
                                else
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = responseData.ToString();
                                }


                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Unable To do process";
                            }

                        }

                        //resVal = new HRM.DataAccess.Employee.EmployeeExpensesClaimDB().SaveUpdate(data, false);
                    }
                    catch (Exception eee)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = eee.Message;
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

        private bool VerifyPayment(int TranId, string privateKey, string token, double amount, ref string errorMSG)
        {
            try
            {
                errorMSG = "";
                string url = "https://khalti.com/api/v2/payment/verify/";

                Models.APIRequest request = new Models.APIRequest(url, "POST", "application/json");
                var para = new
                {
                    token = token,
                    amount = amount
                };
                DateTime requestDateTime = DateTime.Now;
                Dictionary<string, string> headersColl = new Dictionary<string, string>();
                headersColl.Add("Key", privateKey);
                var responseData = request.Execute<AcademicLib.BE.Wallet.WalletVerify>(para, headersColl);
                DateTime responseDateTime = DateTime.Now;
                if (responseData == null)
                {
                    errorMSG = "unable to verify transaction.";
                    return false;
                }
                else
                {
                    if (responseData is AcademicLib.BE.Wallet.WalletVerify)
                    {
                        AcademicLib.BE.Wallet.WalletVerify verifyData = (AcademicLib.BE.Wallet.WalletVerify)responseData;
                        var beData = new AcademicLib.BE.Wallet.WalletVerificationLog
                        {
                            TranId = TranId,
                            Amount = verifyData.Amount,
                            Created_On = verifyData.created_on,
                            Ebanker = verifyData.ebanker,
                            FeeAmount = verifyData.fee_amount,
                            Idx = verifyData.Idx,
                            MerchantIdx = verifyData.merchant.idx,
                            MerchantMobile = verifyData.merchant.mobile,
                            MerchantName = verifyData.merchant.name,
                            Refunded = verifyData.refunded,
                            RequestTime = requestDateTime,
                            ResponseTime = responseDateTime,
                            StateIdx = verifyData.state.idx,
                            StateName = verifyData.state.name,
                            StateTemplate = verifyData.state.template,
                            TypeIdx = verifyData.type.idx,
                            TypeName = verifyData.type.name,
                            UserIdx = verifyData.user.idx,
                            UserMobile = verifyData.user.mobile,
                            UserName = verifyData.user.name

                        };

                        var resVal = new AcademicLib.BL.Wallet.Khalti(UserId,hostName,dbName).SaveWalletVerificationLog(beData);
                        if (resVal.IsSuccess)
                        {
                            return true;
                        }
                        else
                        {
                            errorMSG = resVal.ResponseMSG;
                            return false;
                        }
                    }

                }

                return true;
            }
            catch (Exception ee)
            {
                errorMSG = ee.Message;
                return false;
            }
        }

        private ResponeValues VerifyPaymentOnly(string privateKey, string token, double amount, ref string errorMSG)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                errorMSG = "";
                string url = "https://khalti.com/api/v2/payment/verify/";

                Models.APIRequest request = new Models.APIRequest(url, "POST", "application/json");
                var para = new
                {
                    token = token,
                    amount = amount*100
                };
                DateTime requestDateTime = DateTime.Now;
                Dictionary<string, string> headersColl = new Dictionary<string, string>();
                headersColl.Add("Key", privateKey);
                
                var responseData = request.Execute<AcademicLib.BE.Wallet.WalletVerify>(para, headersColl);
                DateTime responseDateTime = DateTime.Now;
                if (responseData == null)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "unable to verify transaction. responseData is null";
                }
                else
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = ((AcademicLib.BE.Wallet.WalletVerify)responseData).Idx;
                }
                
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }



        // Get api/fonepayURL
        /// <summary>
        /// fonepay URL
        /// </summary>
        /// <returns></returns>     
        [AllowAnonymous]
        [HttpGet]
        public ResponeValues fonepayURL()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string query = Request.RequestUri.Query;
                resVal = new AcademicLib.BL.Wallet.PaymentGateway(1, hostName, dbName).SavePaymentGatewayReturnURL(3, query);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;

        }

        // Get api/esewaURL
        /// <summary>
        /// esewa URL
        /// </summary>
        /// <returns></returns>     
        [AllowAnonymous]
        [HttpGet]
        public ResponeValues esewaURL()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string query = Request.RequestUri.Query;
                resVal = new AcademicLib.BL.Wallet.PaymentGateway(1, hostName, dbName).SavePaymentGatewayReturnURL(2, query);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;

        }

        // Get api/khaltiURL
        /// <summary>
        /// khalti URL
        /// </summary>
        /// <returns></returns>     
        [AllowAnonymous]
        [HttpGet]
        public ResponeValues khaltiURL()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string query = Request.RequestUri.Query;
                resVal = new AcademicLib.BL.Wallet.PaymentGateway(1, hostName, dbName).SavePaymentGatewayReturnURL(1, query);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;

        }
        // Get api/payuURL
        /// <summary>
        /// payu URL 
        /// </summary>
        /// <returns></returns>     
        [AllowAnonymous]
        [HttpGet]
        public ResponeValues payuURL()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string query = Request.RequestUri.Query;
                resVal=new AcademicLib.BL.Wallet.PaymentGateway(4, hostName, dbName).SavePaymentGatewayReturnURL(1, query);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;

        }

        // Get api/payuURL
        /// <summary>
        /// payu URL 
        /// </summary>
        /// <returns></returns>     
        [AllowAnonymous]
        [HttpPost]
        public ResponeValues HIKAttendance([FromBody]JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string query = "";
                if (para != null)
                {
                    query = para.ToString();
                }                
                resVal = new AcademicLib.BL.Wallet.PaymentGateway(4, hostName, dbName).SavePaymentGatewayReturnURL(1, query);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;

        }
    }
}