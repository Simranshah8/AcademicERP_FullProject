using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class RegistrationDB
    {
        DataAccessLayer1 dal = null;
        string hostName, dbName;
        public RegistrationDB(string hostName, string dbName)
        {
            this.hostName = hostName;
            this.dbName = dbName;
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.Student beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegNo", beData.RegNo);
            cmd.Parameters.AddWithValue("@AdmitDate", beData.AdmitDate);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@MotherTongue", beData.MotherTongue);
            cmd.Parameters.AddWithValue("@Height", beData.Height);
            cmd.Parameters.AddWithValue("@Weigth", beData.Weigth);
            cmd.Parameters.AddWithValue("@IsPhysicalDisability", beData.IsPhysicalDisability);
            cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);
            cmd.Parameters.AddWithValue("@Aim", beData.Aim);
            cmd.Parameters.AddWithValue("@BirthCertificateNo", beData.BirthCertificateNo);
            cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@Photo", beData.Photo);
            cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
            cmd.Parameters.AddWithValue("@Signature", beData.Signature);
            cmd.Parameters.AddWithValue("@SignaturePath", beData.SignaturePath);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);
            cmd.Parameters.AddWithValue("@AcademicYear", beData.AcademicYear);
            cmd.Parameters.AddWithValue("@IsNewStudent", beData.IsNewStudent);
            cmd.Parameters.AddWithValue("@StudentTypeId", beData.StudentTypeId);
            cmd.Parameters.AddWithValue("@MediumId", beData.MediumId);
            cmd.Parameters.AddWithValue("@HouseNameId", beData.HouseNameId);
            cmd.Parameters.AddWithValue("@TransportPointId", beData.TransportPointId);
            cmd.Parameters.AddWithValue("@BoardersTypeId", beData.BoardersTypeId);
            cmd.Parameters.AddWithValue("@BoardId", beData.BoardId);
            cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
            cmd.Parameters.AddWithValue("@EnrollNo", beData.EnrollNo);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@F_Profession", beData.F_Profession);
            cmd.Parameters.AddWithValue("@F_ContactNo", beData.F_ContactNo);
            cmd.Parameters.AddWithValue("@F_Email", beData.F_Email);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@M_Profession", beData.M_Profession);
            cmd.Parameters.AddWithValue("@M_Contact", beData.M_Contact);
            cmd.Parameters.AddWithValue("@M_Email", beData.M_Email);
            cmd.Parameters.AddWithValue("@IfGurandianIs", beData.IfGurandianIs);
            cmd.Parameters.AddWithValue("@GuardianName", beData.GuardianName);
            cmd.Parameters.AddWithValue("@G_Relation", beData.G_Relation);
            cmd.Parameters.AddWithValue("@G_Profesion", beData.G_Profesion);
            cmd.Parameters.AddWithValue("@G_ContactNo", beData.G_ContactNo);
            cmd.Parameters.AddWithValue("@G_Email", beData.G_Email);
            cmd.Parameters.AddWithValue("@G_Address", beData.G_Address);
            cmd.Parameters.AddWithValue("@PermanentAddress", beData.PermanentAddress);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@PA_Province", beData.PA_Province);
            cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
            cmd.Parameters.AddWithValue("@PA_LocalLevel", beData.PA_LocalLevel);
            cmd.Parameters.AddWithValue("@PA_Village", beData.PA_Village);
            cmd.Parameters.AddWithValue("@PA_WardNo", beData.PA_WardNo);
            cmd.Parameters.AddWithValue("@CurrentAddress", beData.CurrentAddress);
            cmd.Parameters.AddWithValue("@IsSameAsPermanentAddress", beData.IsSameAsPermanentAddress);
            cmd.Parameters.AddWithValue("@CA_FullAddress", beData.CA_FullAddress);
            cmd.Parameters.AddWithValue("@CA_Province", beData.CA_Province);
            cmd.Parameters.AddWithValue("@CA_District", beData.CA_District);
            cmd.Parameters.AddWithValue("@CA_LocalLevel", beData.CA_LocalLevel);
            cmd.Parameters.AddWithValue("@CA_WardNo", beData.CA_WardNo);
            cmd.Parameters.AddWithValue("@StreetName", beData.StreetName);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateRegistration";
            }
            else
            {
                cmd.Parameters[71].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddRegistration";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[72].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[73].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[74].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("@Ret_RegNo", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters[75].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
            cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);
            cmd.Parameters.AddWithValue("@LedgerPanaNo", beData.LedgerPanaNo);
            cmd.Parameters.AddWithValue("@HouseDressId", beData.HouseDressId);

            cmd.Parameters.AddWithValue("@FatherPhotoPath", beData.FatherPhotoPath);
            cmd.Parameters.AddWithValue("@MotherPhotoPath", beData.MotherPhotoPath);
            cmd.Parameters.AddWithValue("@GuardianPhotoPath", beData.GuardianPhotoPath);

            //SemesterId,ClassYearId,BatchId
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.AdmissionEnquiryId);
            cmd.Parameters.AddWithValue("@EnquiryNo", beData.EnquiryNo);
            cmd.Parameters.AddWithValue("@F_AnnualIncome", beData.F_AnnualIncome);
            cmd.Parameters.AddWithValue("@M_AnnualIncome", beData.M_AnnualIncome);
            cmd.Parameters.AddWithValue("@ClassId_First", beData.ClassId_First);

            cmd.Parameters.AddWithValue("@IsFollowupRequired", beData.IsFollowupRequired);
            cmd.Parameters.AddWithValue("@FollowupDate", beData.FollowupDate);
            cmd.Parameters.AddWithValue("@FollowupDateTime", beData.FollowupDateTime);
            cmd.Parameters.AddWithValue("@SourceId", beData.SourceId);
            cmd.Parameters.AddWithValue("@CommunicationTypeId", beData.CommunicationTypeId);
            cmd.Parameters.AddWithValue("@FormSale", beData.FormSale);
            cmd.Parameters.AddWithValue("@ReceiptAsLedgerId", beData.ReceiptAsLedgerId);
            cmd.Parameters.AddWithValue("@ReceiptNarration", beData.ReceiptNarration);
            cmd.Parameters.AddWithValue("@FollowupRemarks", beData.FollowupRemarks);
            cmd.Parameters.AddWithValue("@FamilyType", beData.FamilyType);
            cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);
            cmd.Parameters.AddWithValue("@ReferralCode", beData.ReferralCode);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[71].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[71].Value);

                if (!(cmd.Parameters[72].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[72].Value);

                if (!(cmd.Parameters[73].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[73].Value);

                if (!(cmd.Parameters[74].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[74].Value);

                if (!(cmd.Parameters[75].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[75].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveAcademicDetails(beData.CUserId, resVal.RId, beData.AcademicDetailsColl); 
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
                    SaveFeeDetailss(beData.CUserId, resVal.RId, beData.FeeItemColl);

                    try
                    {
                        if (!isModify)
                        {
                            //if (beData.FeeItemColl != null && beData.FeeItemColl.Count > 0 && beData.FormSale)
                            if (beData.FeeItemColl != null && beData.FeeItemColl.Count > 0)
                            {
                                var newBill = new AcademicLib.BE.Fee.Creation.ManualBilling();
                                newBill.RefNo = resVal.ResponseId;
                                newBill.RegistrationId = resVal.RId;
                                newBill.TranId = 0;
                                newBill.BillingDate = DateTime.Today;
                                newBill.Address = beData.PA_FullAddress;
                                newBill.ClassName = beData.ClassName;
                                newBill.CUserId = beData.CUserId;
                                newBill.IsCash = true;
                                newBill.BillingType = BE.Fee.Creation.BILLINGTYPES.SALESINVOICE;
                                newBill.BillingTypeName = "SalesInvoice";
                                newBill.ClassId = beData.ClassId;
                                newBill.ManualBillingDetailsColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                                newBill.LedgerId = beData.ReceiptAsLedgerId;
                                foreach (var det in beData.FeeItemColl)
                                {
                                    newBill.ManualBillingDetailsColl.Add(det);
                                }
                                newBill.RefBillNo = resVal.ResponseId;
                                newBill.RegdNo = resVal.RId.ToString();
                                newBill.StudentName = ((beData.FirstName + " " + beData.MiddleName).Trim() + " " + beData.LastName).Trim();
                                newBill.TotalAmount = newBill.ManualBillingDetailsColl.Sum(p1 => p1.PayableAmt);
                                newBill.Remarks = beData.ReceiptNarration;
                                var billRet = new AcademicLib.BL.Fee.Creation.ManualBilling(beData.CUserId, hostName, dbName).SaveFormData(beData.AcademicYear.Value, newBill);
                                if (billRet.IsSuccess)
                                    resVal.ResponseId = billRet.RId.ToString();

                            }
                        }

                    }
                    catch { }
                }
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues SaveUpdateEligible(BE.Academic.Transaction.RegistrationEligibility beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@ExaminarName", beData.ExaminarName);
            cmd.Parameters.AddWithValue("@AppliedClassId", beData.AppliedClassId);
            cmd.Parameters.AddWithValue("@ClassPreferredForId", beData.ClassPreferredForId);
            cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);
            cmd.Parameters.AddWithValue("@PassMark", beData.PassMark);
            cmd.Parameters.AddWithValue("@ObtainMark", beData.ObtainMark);
            cmd.Parameters.AddWithValue("@Percentage", beData.Percentage);
            cmd.Parameters.AddWithValue("@Result", beData.Result);
            cmd.Parameters.AddWithValue("@Status", beData.Status);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId); 
            cmd.CommandText = "usp_AddRegEligibility";
            cmd.Parameters.Add("@TranId",  System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ApprovalBy", beData.ApprovalBy);
            cmd.Parameters.AddWithValue("@ExaminationMode", beData.ExaminationMode);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[18].Value);

                if (!(cmd.Parameters[19].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[19].Value);
                 
                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    if(beData.AttachmentColl!=null && beData.AttachmentColl.Count>0)
                        SaveEligibleDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);                      
                }

                dal.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues SaveEligibleFeeReceipt(int AcademicYearId, BE.Academic.Transaction.RegistrationEligibility beData)
        {
            ResponeValues resVal = new ResponeValues();
            
            try
            {
                if (beData.FeeItemColl != null && beData.FeeItemColl.Count > 0)
                {
                    var newBill = new AcademicLib.BE.Fee.Creation.ManualBilling();
                    newBill.RegistrationId = beData.StudentId;
                    newBill.TranId = 0;
                    newBill.BillingDate = DateTime.Today;
                    newBill.Address = beData.Address;
                    newBill.ClassName = beData.ClassName;
                    newBill.CUserId = beData.CUserId;
                    newBill.IsCash = true;
                    newBill.BillingType = BE.Fee.Creation.BILLINGTYPES.SALESINVOICE;
                    newBill.BillingTypeName = "SalesInvoice";
                    newBill.ClassId = beData.ClassPreferredForId;
                    newBill.ManualBillingDetailsColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                    newBill.LedgerId = beData.ReceiptAsLedgerId;
                    newBill.PaymentModeColl = beData.PaymentModeColl;
                    newBill.AdvanceFeeItemColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                    newBill.ForMonthId = (beData.PaidUpToMonth.HasValue ? beData.PaidUpToMonth.Value : 0);
                    foreach (var det in beData.FeeItemColl)
                    {
                        if (det.IsAdvance == false)
                            newBill.ManualBillingDetailsColl.Add(det);
                        else
                            newBill.AdvanceFeeItemColl.Add(det);
                    }
                    newBill.RefBillNo = resVal.ResponseId;
                    newBill.RegdNo = resVal.RId.ToString();
                    newBill.StudentName = beData.Name;
                    newBill.TotalAmount = newBill.ManualBillingDetailsColl.Sum(p1 => p1.PayableAmt);
                    newBill.PaidAmt = newBill.ManualBillingDetailsColl.Sum(p1 => p1.PaidAmt);
                    newBill.Waiver = newBill.ManualBillingDetailsColl.Sum(p1 => p1.Waiver);
                    newBill.Remarks = beData.ReceiptNarration;
                    var billRet = new AcademicLib.BL.Fee.Creation.ManualBilling(beData.CUserId, hostName, dbName).SaveFormData(AcademicYearId, newBill);
                    if (billRet.IsSuccess)
                    {
                        var recTranId = billRet.RId.ToString();
                        dal.OpenConnection();
                        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                        cmd.Parameters.AddWithValue("@USerId", beData.CUserId);
                        cmd.Parameters.AddWithValue("@ReceiptAsLedgerId", beData.ReceiptAsLedgerId);
                        cmd.Parameters.AddWithValue("@ReceiptNarration", beData.ReceiptNarration);
                        cmd.Parameters.AddWithValue("@ReceiptTranId", recTranId);
                        cmd.Parameters.AddWithValue("@DiscountTypeId", beData.DiscountTypeId);
                        cmd.Parameters.AddWithValue("@RouteId", beData.RouteId);
                        cmd.Parameters.AddWithValue("@PointId", beData.PointId);
                        cmd.Parameters.AddWithValue("@BedId", beData.BedId);
                        cmd.Parameters.AddWithValue("@PaidUpToMonth", beData.PaidUpToMonth);
                        cmd.CommandText = "usp_AddRegEligibilityFeeReceipt";
                        cmd.ExecuteNonQuery();

                        int sno = 1;
                        foreach (BE.Fee.Creation.ManualBillingDetails fee in beData.FeeItemColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                            cmd.Parameters.AddWithValue("@SNo", sno);
                            cmd.Parameters.AddWithValue("@FeeItemId", fee.FeeItemId);
                            cmd.Parameters.AddWithValue("@Qty", fee.Qty);
                            cmd.Parameters.AddWithValue("@Rate", fee.Rate);
                            cmd.Parameters.AddWithValue("@DiscountPer", fee.DiscountPer);
                            cmd.Parameters.AddWithValue("@DiscountAmt", fee.DiscountAmt);
                            cmd.Parameters.AddWithValue("@PayableAmt", fee.PayableAmt);
                            cmd.Parameters.AddWithValue("@Remarks", fee.Remarks);
                            cmd.Parameters.AddWithValue("@Waiver", fee.Waiver);
                            cmd.Parameters.AddWithValue("@PaidAmt", fee.PaidAmt);
                            cmd.Parameters.AddWithValue("@DuesAmt", fee.DuesAmt);
                            cmd.Parameters.AddWithValue("@IsAdvance", fee.IsAdvance);
                            cmd.Parameters.AddWithValue("@MonthId", fee.MonthId);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "sp_AddRegEligibilityFeeDetails";
                            cmd.ExecuteNonQuery();
                            sno++;
                        }
                    }

                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                }else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No Data Found For Receipt";
                }
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
               
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            { 
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        private void SaveFeeDetailss(int UserId, int TranId, List<BE.Fee.Creation.ManualBillingDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.Fee.Creation.ManualBillingDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                cmd.Parameters.AddWithValue("@Qty", beData.Qty);
                cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                cmd.Parameters.AddWithValue("@PayableAmt", beData.PayableAmt);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddRegistrationFeeDetails";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveEligibleDocument(int UserId, int StudentId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || StudentId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", StudentId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddRegEligibleAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        private void SaveDocument(int UserId, int StudentId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || StudentId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddRegistrationAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        private void SaveAcademicDetails(int UserId, int StudentId, List<AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || StudentId == 0)
                return;

            foreach (AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
                cmd.Parameters.AddWithValue("@Exam", beData.Exam);
                cmd.Parameters.AddWithValue("@PassoutYear", beData.PassoutYear);
                cmd.Parameters.AddWithValue("@ObtainMarks", beData.ObtainMarks);
                cmd.Parameters.AddWithValue("@ObtainPer", beData.ObtainPer);
                cmd.Parameters.AddWithValue("@Division", beData.Division);
                cmd.Parameters.AddWithValue("@GPA", beData.GPA);
                cmd.Parameters.AddWithValue("@SchoolColledge", beData.SchoolColledge);
                cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
                cmd.Parameters.AddWithValue("@BoardName", beData.BoardName);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddRegistrationAcademicDetails";
                cmd.ExecuteNonQuery();
            }

        }
  
        public BE.Academic.Transaction.Student getStudentById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Transaction.Student beData = new BE.Academic.Transaction.Student();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetRegistrationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.Student();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AdmitDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.FirstName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MiddleName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.LastName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.DOB_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.Religion = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.CasteId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.Nationality = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.BloodGroup = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Email = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.MotherTongue = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Height = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Weigth = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(18);
                    if (!(reader[19] is DBNull)) beData.PhysicalDisability = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Aim = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BirthCertificateNo = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.CitizenshipNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Remarks = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.PhotoPath = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.SignaturePath = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.ClassId = reader.GetInt32(26);
                    if (!(reader[27] is DBNull)) beData.SectionId = reader.GetInt32(27);
                    if (!(reader[28] is DBNull)) beData.RollNo = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.AcademicYear = reader.GetInt32(29);
                    if (!(reader[30] is DBNull)) beData.IsNewStudent = reader.GetBoolean(30);
                    if (!(reader[31] is DBNull)) beData.StudentTypeId = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.MediumId = reader.GetInt32(32);
                    if (!(reader[33] is DBNull)) beData.HouseNameId = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.TransportPointId = reader.GetInt32(34);
                    if (!(reader[35] is DBNull)) beData.BoardersTypeId = reader.GetInt32(35);
                    if (!(reader[36] is DBNull)) beData.BoardId = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.BoardRegNo = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.EnrollNo = reader.GetInt32(38);
                    if (!(reader[39] is DBNull)) beData.FatherName = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.F_Profession = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.F_ContactNo = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.F_Email = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.MotherName = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.M_Profession = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.M_Contact = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.M_Email = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.IfGurandianIs = reader.GetInt32(47);
                    if (!(reader[48] is DBNull)) beData.GuardianName = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.G_Relation = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.G_Profesion = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.G_ContactNo = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.G_Email = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.G_Address = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.PermanentAddress = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.PA_FullAddress = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.PA_Province = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.PA_District = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.PA_LocalLevel = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.PA_Village = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.PA_WardNo = reader.GetInt32(60);
                    if (!(reader[61] is DBNull)) beData.CurrentAddress = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.IsSameAsPermanentAddress = reader.GetBoolean(62);
                    if (!(reader[63] is DBNull)) beData.CA_FullAddress = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.CA_Province = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.CA_District = reader.GetString(65);
                    if (!(reader[66] is DBNull)) beData.CA_LocalLevel = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.CA_WardNo = reader.GetInt32(67);
                    if (!(reader[68] is DBNull)) beData.StreetName = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.CardNo = reader.GetInt32(69);
                    if (!(reader[70] is DBNull)) beData.EMSId = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.LedgerPanaNo = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.HouseDressId = reader.GetInt32(72);
                    if (!(reader[73] is DBNull)) beData.FatherPhotoPath = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.MotherPhotoPath = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.GuardianPhotoPath = reader.GetString(75);

                    //SemesterId,ClassYearId,BatchId
                    if (!(reader[76] is DBNull)) beData.SemesterId = reader.GetInt32(76);
                    if (!(reader[77] is DBNull)) beData.ClassYearId = reader.GetInt32(77);
                    if (!(reader[78] is DBNull)) beData.BatchId = reader.GetInt32(78);

                    if (!(reader[79] is DBNull)) beData.AdmissionEnquiryId = reader.GetInt32(79);
                    if (!(reader[80] is DBNull)) beData.EnquiryNo = reader.GetString(80);
                    if (!(reader[81] is DBNull)) beData.F_AnnualIncome = Convert.ToDouble(reader[81]);
                    if (!(reader[82] is DBNull)) beData.M_AnnualIncome = Convert.ToDouble(reader[82]);
                    if (!(reader[83] is DBNull)) beData.ClassId_First = reader.GetInt32(83);
                    if (!(reader[84] is DBNull)) beData.ClassShiftId = reader.GetInt32(84);

                    if (!(reader[85] is DBNull)) beData.IsFollowupRequired = reader.GetBoolean(85);
                    if (!(reader[86] is DBNull)) beData.FollowupDate = reader.GetDateTime(86);
                    if (!(reader[87] is DBNull)) beData.FollowUpTime = reader.GetDateTime(87);
                    if (!(reader[88] is DBNull)) beData.FollowupRemarks = reader.GetString(88);
                    if (!(reader[89] is DBNull)) beData.SourceId = reader.GetInt32(89);
                    if (!(reader[90] is DBNull)) beData.CommunicationTypeId = reader.GetInt32(90);
                    if (!(reader[91] is DBNull)) beData.ReceiptAsLedgerId = reader.GetInt32(91);
                    if (!(reader[92] is DBNull)) beData.ReceiptNarration = reader.GetString(92);
                    try
                    {
                        if (!(reader[93] is DBNull)) beData.ReferralCode = reader.GetString(93);
                    }
                    catch { }
                }
                reader.NextResult();
                beData.AcademicDetailsColl = new List<BE.Academic.Transaction.StudentPreviousAcademicDetails>();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                beData.SiblingDetailColl = new List<BE.Academic.Transaction.SiblingDetails>();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentPreviousAcademicDetails det = new BE.Academic.Transaction.StudentPreviousAcademicDetails();
                    if (!(reader[0] is DBNull)) det.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.Exam = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.PassoutYear = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.ObtainMarks = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.ObtainPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det.Division = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.GPA = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det.SchoolColledge = reader.GetString(7);
                    if (!(reader[8] is DBNull)) det.SymbolNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) det.BoardName = reader.GetString(9);
                    beData.AcademicDetailsColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Description = reader.GetString(4);

                    beData.AttachmentColl.Add(doc);
                }
                reader.NextResult();
                //while (reader.Read())
                //{
                //    AcademicLib.BE.Academic.Transaction.SiblingDetails sbDet = new BE.Academic.Transaction.SiblingDetails();
                //    if (!(reader[0] is DBNull)) sbDet.ForStudentId = reader.GetInt32(0);
                //    if (!(reader[1] is DBNull)) sbDet.ClassId = reader.GetInt32(1);
                //    if (!(reader[2] is DBNull)) sbDet.SectionId = reader.GetInt32(2);
                //    if (!(reader[3] is DBNull)) sbDet.Relation = reader.GetString(3);
                //    if (!(reader[4] is DBNull)) sbDet.Remarks = reader.GetString(4);

                //    beData.SiblingDetailColl.Add(sbDet);
                //}
                beData.Eligibility = new BE.Academic.Transaction.RegistrationEligibility();
                beData.EligibilityAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                beData.Eligibility.StudentId = beData.StudentId.Value;
                beData.Eligibility.ClassPreferredForId = beData.ClassId;
                beData.Eligibility.AppliedClassId = beData.ClassId;

                if (reader.Read())
                {
                    BE.Academic.Transaction.RegistrationEligibility det = new BE.Academic.Transaction.RegistrationEligibility();
                    det.StudentId = beData.StudentId.Value;
                    if (!(reader[0] is DBNull)) det.ExamDate = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) det.ExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.SubjectId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.ExaminarName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.AppliedClassId= reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det.ClassPreferredForId= reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) det.FullMark = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det.PassMark = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) det.ObtainMark = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) det.Percentage = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) det.Result = reader.GetString(10);
                    if (!(reader[11] is DBNull)) det.Status = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) det.Remarks = reader.GetString(12);
                    beData.Eligibility = det;
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Description = reader.GetString(4);
                    beData.EligibilityAttachmentColl.Add(doc);
                }

                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }
        public ResponeValues DeleteById(int UserId, int EntityId, int StudentId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_DelRegistrationById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }               
        public ResponeValues getAutoRegdNo(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@RegdNo", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetRegistrationAutoRegdNo";
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public RE.FrontDesk.EnqSummaryCollections getRegSummary(int UserId, DateTime dateFrom, DateTime dateTo)
        {
            RE.FrontDesk.EnqSummaryCollections dataColl = new RE.FrontDesk.EnqSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetRegSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqSummary beData = new RE.FrontDesk.EnqSummary();
                    beData.SNo = reader.GetInt32(0);
                    beData.EnquiryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EnqDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.EnqDate_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DOB_AD = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.DOB_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.BirthCertificateNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Nationality = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Religion = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Email = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.F_Email = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.F_Profession = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.M_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.M_Email = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.M_Profession = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.GuardianName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.G_Address = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.G_Contact = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Email = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Professsion = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_Relation = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PA_Province = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.PA_District = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_LocalLevel = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_WardNo = reader.GetInt32(34);
                    if (!(reader[35] is DBNull)) beData.PA_StreetName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CA_Province = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CA_District = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CA_LocalLevel = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CA_WardNo = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.CA_StreetName = reader.GetString(40);                   
                    if (!(reader[41] is DBNull)) beData.MotherName = reader.GetString(41); 
                    if (!(reader[42] is DBNull)) beData.Source = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.EnquiryNo = reader.GetInt32(43); 
                    if (!(reader[44] is DBNull)) beData.ReceiptNo = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[45]);
                    if (!(reader[46] is DBNull)) beData.ReceiptTranId = reader.GetInt32(46); 
                    if (!(reader[47] is DBNull)) beData.CommunicationType = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.AutoManualNo = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.FormSale = reader.GetBoolean(49);
                    if (!(reader[50] is DBNull)) beData.Status = reader.GetInt32(50);
                    if (!(reader[51] is DBNull)) beData.StatusRemarks = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(52);
                    if (!(reader[53] is DBNull)) beData.Counselor = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.ClassShiftName = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.Medium = reader.GetString(55);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.EnqSummaryCollections getRegSummaryForEligible(int UserId, DateTime dateFrom, DateTime dateTo)
        {
            RE.FrontDesk.EnqSummaryCollections dataColl = new RE.FrontDesk.EnqSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetRegSummaryForEligibility";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqSummary beData = new RE.FrontDesk.EnqSummary();
                    beData.SNo = reader.GetInt32(0);
                    beData.EnquiryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EnqDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.EnqDate_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DOB_AD = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.DOB_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.BirthCertificateNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Nationality = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Religion = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Email = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.F_Email = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.F_Profession = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.M_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.M_Email = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.M_Profession = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.GuardianName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.G_Address = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.G_Contact = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Email = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Professsion = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_Relation = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PA_Province = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.PA_District = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_LocalLevel = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_WardNo = reader.GetInt32(34);
                    if (!(reader[35] is DBNull)) beData.PA_StreetName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CA_Province = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CA_District = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CA_LocalLevel = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CA_WardNo = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.CA_StreetName = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.MotherName = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Source = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.EnquiryNo = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.ReceiptNo = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[45]);
                    if (!(reader[46] is DBNull)) beData.ReceiptTranId = reader.GetInt32(46);
                    if (!(reader[47] is DBNull)) beData.CommunicationType = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.AutoManualNo = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.FormSale = reader.GetBoolean(49);
                    if (!(reader[50] is DBNull)) beData.Status = reader.GetInt32(50);
                    if (!(reader[51] is DBNull)) beData.StatusRemarks = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(52);
                    if (!(reader[53] is DBNull)) beData.Counselor = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EligibleClassPreferredFor = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.EligibleFullMark = Convert.ToDouble(reader[55]);
                    if (!(reader[56] is DBNull)) beData.EligiblePassMark = Convert.ToDouble(reader[56]);
                    if (!(reader[57] is DBNull)) beData.EligiblePercentage = Convert.ToDouble(reader[57]);
                    if (!(reader[58] is DBNull)) beData.EligibleResult = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.EligibleStatus = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.EligibleRemarks = reader.GetString(60);

                    if (!(reader[61] is DBNull)) beData.ExamDate = reader.GetDateTime(61);
                    if (!(reader[62] is DBNull)) beData.ExamMiti = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.ExaminarName = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.ExamType = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.StudentType = reader.GetString(65);
                    if (!(reader[66] is DBNull)) beData.EligibleObtainMark = Convert.ToDouble(reader[66]);
                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.EnqSummaryCollections getRegSummaryForAdmitConfirm(int UserId)
        {
            RE.FrontDesk.EnqSummaryCollections dataColl = new RE.FrontDesk.EnqSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetRegSummaryForAdmissionConfirm";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqSummary beData = new RE.FrontDesk.EnqSummary();
                    beData.SNo = reader.GetInt32(0);
                    beData.EnquiryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EnqDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.EnqDate_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DOB_AD = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.DOB_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.BirthCertificateNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Nationality = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Religion = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Email = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.F_Email = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.F_Profession = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.M_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.M_Email = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.M_Profession = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.GuardianName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.G_Address = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.G_Contact = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Email = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Professsion = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_Relation = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PA_Province = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.PA_District = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_LocalLevel = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_WardNo = reader.GetInt32(34);
                    if (!(reader[35] is DBNull)) beData.PA_StreetName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CA_Province = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CA_District = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CA_LocalLevel = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CA_WardNo = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.CA_StreetName = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.MotherName = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Source = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.EnquiryNo = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.ReceiptNo = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[45]);
                    if (!(reader[46] is DBNull)) beData.ReceiptTranId = reader.GetInt32(46);
                    if (!(reader[47] is DBNull)) beData.CommunicationType = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.AutoManualNo = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.FormSale = reader.GetBoolean(49);
                    if (!(reader[50] is DBNull)) beData.Status = reader.GetInt32(50);
                    if (!(reader[51] is DBNull)) beData.StatusRemarks = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(52);
                    if (!(reader[53] is DBNull)) beData.Counselor = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EligibleClassPreferredFor = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.EligibleFullMark = Convert.ToDouble(reader[55]);
                    if (!(reader[56] is DBNull)) beData.EligiblePassMark = Convert.ToDouble(reader[56]);
                    if (!(reader[57] is DBNull)) beData.EligiblePercentage = Convert.ToDouble(reader[57]);
                    if (!(reader[58] is DBNull)) beData.EligibleResult = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.EligibleStatus = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.EligibleRemarks = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.ExamDate = reader.GetDateTime(61);
                    if (!(reader[62] is DBNull)) beData.ExamMiti = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.ExaminarName = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.ExamType = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.StudentType = reader.GetString(65);
                    if (!(reader[66] is DBNull)) beData.EligibleObtainMark = Convert.ToDouble(reader[66]);
                    if (!(reader[67] is DBNull)) beData.EligibleTranId = Convert.ToInt32(reader[67]);
                    if (!(reader[68] is DBNull)) beData.AdmissionStatus = Convert.ToInt32(reader[68]);
                    if (!(reader[69] is DBNull)) beData.AdmissionStatusRemarks = Convert.ToString(reader[69]);
                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.EnqSummaryCollections getRegAdmitStudent(int UserId,int? AcademicYearId)
        {
            RE.FrontDesk.EnqSummaryCollections dataColl = new RE.FrontDesk.EnqSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetRegAdmissionConfirm";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqSummary beData = new RE.FrontDesk.EnqSummary();
                    beData.SNo = reader.GetInt32(0);
                    beData.EnquiryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EnqDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.EnqDate_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DOB_AD = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.DOB_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.BirthCertificateNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Nationality = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Religion = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Email = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.F_Email = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.F_Profession = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.M_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.M_Email = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.M_Profession = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.GuardianName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.G_Address = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.G_Contact = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Email = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Professsion = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_Relation = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PA_Province = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.PA_District = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_LocalLevel = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_WardNo = reader.GetInt32(34);
                    if (!(reader[35] is DBNull)) beData.PA_StreetName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CA_Province = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CA_District = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CA_LocalLevel = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CA_WardNo = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.CA_StreetName = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.MotherName = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Source = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.EnquiryNo = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.ReceiptNo = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[45]);
                    if (!(reader[46] is DBNull)) beData.ReceiptTranId = reader.GetInt32(46);
                    if (!(reader[47] is DBNull)) beData.CommunicationType = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.AutoManualNo = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.FormSale = reader.GetBoolean(49);
                    if (!(reader[50] is DBNull)) beData.Status = reader.GetInt32(50);
                    if (!(reader[51] is DBNull)) beData.StatusRemarks = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(52);
                    if (!(reader[53] is DBNull)) beData.Counselor = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EligibleClassPreferredFor = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.EligibleFullMark = Convert.ToDouble(reader[55]);
                    if (!(reader[56] is DBNull)) beData.EligiblePassMark = Convert.ToDouble(reader[56]);
                    if (!(reader[57] is DBNull)) beData.EligiblePercentage = Convert.ToDouble(reader[57]);
                    if (!(reader[58] is DBNull)) beData.EligibleResult = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.EligibleStatus = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.EligibleRemarks = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.ExamDate = reader.GetDateTime(61);
                    if (!(reader[62] is DBNull)) beData.ExamMiti = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.ExaminarName = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.ExamType = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.StudentType = reader.GetString(65);
                    if (!(reader[66] is DBNull)) beData.EligibleObtainMark = Convert.ToDouble(reader[66]);
                    if (!(reader[67] is DBNull)) beData.EligibleTranId = Convert.ToInt32(reader[67]);

                    if (!(reader[68] is DBNull)) beData.RegNo = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.EnquiryDate = reader.GetDateTime(69);
                    if (!(reader[70] is DBNull)) beData.EnqRemarks = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.NextFollowupDate = reader.GetDateTime(71);
                    if (!(reader[72] is DBNull)) beData.IsClosed = reader.GetBoolean(72);
                    if (!(reader[73] is DBNull)) beData.ClosedRemarks = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(74);
                    if (!(reader[75] is DBNull)) beData.ClosedMiti = reader.GetString(75);
                    if (!(reader[76] is DBNull)) beData.EnquiryMiti = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.NextFollowupMiti = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.CreateBy = reader.GetString(78);
                    if (!(reader[79] is DBNull)) beData.ModifyBy = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.ClosedBy = reader.GetString(80);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.EnqFollowupCollections getRegForFollowup(int UserId, int? AcademicYearId, int FollowupType)
        {
            RE.FrontDesk.EnqFollowupCollections dataColl = new RE.FrontDesk.EnqFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FollowupType", FollowupType);
            cmd.CommandText = "usp_GetRegistrationEnqFollowup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqFollowup beData = new RE.FrontDesk.EnqFollowup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FirstName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MiddleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.LastName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Email = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Medium = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Faculty = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClassShift = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IsTransport = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsHostel = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.FatherName = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.EnquiryDate = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.EnqRemarks = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.NextFollowupDate = reader.GetDateTime(19);
                    if (!(reader[20] is DBNull)) beData.IsClosed = reader.GetBoolean(20);
                    if (!(reader[21] is DBNull)) beData.ClosedRemarks = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(22);
                    if (!(reader[23] is DBNull)) beData.ClosedMiti = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.EnquiryMiti = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.NextFollowupMiti = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.CreateBy = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.ModifyBy = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ClosedBy = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.F_ContactNo = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.EntryDate = reader.GetDateTime(30);
                    if (!(reader[31] is DBNull)) beData.EntryMiti = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.RefTranId = reader.GetInt32(32);
                    if (!(reader[33] is DBNull)) beData.FollowupType = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(34);
                    if (!(reader[35] is DBNull)) beData.Counselor = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.FormSale = reader.GetBoolean(36);
                    if (!(reader[37] is DBNull)) beData.Status = reader.GetInt32(37);
                    if (!(reader[38] is DBNull)) beData.StatusRemarks = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.F_Email = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.M_ContactNo = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.M_Email = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.G_Contact = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.G_Email = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.AutoManualNo = reader.GetString(44);
                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.EnqFollowupCollections getAdmissionForFollowup(int UserId, int? AcademicYearId, int FollowupType)
        {
            RE.FrontDesk.EnqFollowupCollections dataColl = new RE.FrontDesk.EnqFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FollowupType", FollowupType);
            cmd.CommandText = "usp_GetAdmissionRegEnqFollowup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqFollowup beData = new RE.FrontDesk.EnqFollowup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FirstName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MiddleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.LastName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Email = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Medium = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Faculty = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClassShift = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IsTransport = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsHostel = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.FatherName = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.EnquiryDate = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.EnqRemarks = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.NextFollowupDate = reader.GetDateTime(19);
                    if (!(reader[20] is DBNull)) beData.IsClosed = reader.GetBoolean(20);
                    if (!(reader[21] is DBNull)) beData.ClosedRemarks = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(22);
                    if (!(reader[23] is DBNull)) beData.ClosedMiti = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.EnquiryMiti = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.NextFollowupMiti = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.CreateBy = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.ModifyBy = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ClosedBy = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.F_ContactNo = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.EntryDate = reader.GetDateTime(30);
                    if (!(reader[31] is DBNull)) beData.EntryMiti = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.RefTranId = reader.GetInt32(32);
                    if (!(reader[33] is DBNull)) beData.FollowupType = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(34);
                    if (!(reader[35] is DBNull)) beData.Counselor = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.FormSale = reader.GetBoolean(36);
                    if (!(reader[37] is DBNull)) beData.Status = reader.GetInt32(37);
                    if (!(reader[38] is DBNull)) beData.StatusRemarks = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.F_Email = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.M_ContactNo = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.M_Email = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.G_Contact = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.G_Email = reader.GetString(43);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public RE.FrontDesk.StudentPaymentFollowupCollections getFollowupList(int UserId, int TranId, int? AcademicYearId)
        {
            RE.FrontDesk.StudentPaymentFollowupCollections dataColl = new RE.FrontDesk.StudentPaymentFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetRegistrationFollowupList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.StudentPaymentFollowup beData = new RE.FrontDesk.StudentPaymentFollowup();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PaymentDueDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.PaymentDueMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Remarks = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FollowupDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.FollowupMiti = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FollowupBy = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.NextFollowupDateTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.NextFollowupMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsClosed = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.ClosedRemarks = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.ClosedMiti = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ClosedBy = reader.GetString(14);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public ResponeValues SaveFollowup(BE.FrontDesk.Transaction.StudentPaymentFollowup beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EnquiryDate", beData.PaymentDueDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@NextFollowupRequired", beData.NextFollowupRequired);
            cmd.Parameters.AddWithValue("@NextFollowupDateTime", beData.NextFollowupDateTime);
            cmd.Parameters.AddWithValue("@RefTranId", beData.RefTranId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_AddRegistrationFollowup";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CommunicationTypeId", beData.CommunicationTypeId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }





        public RE.FrontDesk.StudentPaymentFollowupCollections getAdmitFollowupList(int UserId, int TranId, int? AcademicYearId)
        {
            RE.FrontDesk.StudentPaymentFollowupCollections dataColl = new RE.FrontDesk.StudentPaymentFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAdmitFollowupList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.StudentPaymentFollowup beData = new RE.FrontDesk.StudentPaymentFollowup();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PaymentDueDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.PaymentDueMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Remarks = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FollowupDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.FollowupMiti = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FollowupBy = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.NextFollowupDateTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.NextFollowupMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsClosed = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.ClosedRemarks = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.ClosedMiti = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ClosedBy = reader.GetString(14);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public ResponeValues SaveAdmitFollowup(BE.FrontDesk.Transaction.StudentPaymentFollowup beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EnquiryDate", beData.PaymentDueDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@NextFollowupRequired", beData.NextFollowupRequired);
            cmd.Parameters.AddWithValue("@NextFollowupDateTime", beData.NextFollowupDateTime);
            cmd.Parameters.AddWithValue("@RefTranId", beData.RefTranId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_AddAdmitFollowup";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CommunicationTypeId", beData.CommunicationTypeId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues SaveAssignCounselor(int UserId, int TranId,DateTime? AssignDate, List<int> EmployeeIdColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                foreach (var empId in EmployeeIdColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@EmployeeId", empId);
                    cmd.Parameters.AddWithValue("@AssignDate", AssignDate);
                    cmd.CommandText = "usp_AddRegEnquiryCounselor";
                    cmd.ExecuteNonQuery();
                }
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues SaveEnqStatus(int UserId, int TranId, int Status, string Remarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.CommandText = "usp_AddRegistrationEnqStatus";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;


            try
            {
                cmd.ExecuteNonQuery();


                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues SaveAdmitStatus(int UserId, int TranId, int Status, string Remarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.CommandText = "usp_AddRegistrationAdmitStatus";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;


            try
            {
                cmd.ExecuteNonQuery();


                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues GenerateUser(int UserId, int AcademicYearId, int AsPer, bool CanUpdateUserName, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AsPer", AsPer);
            cmd.CommandText = "sp_GetAutoCreateRegisterStudentUser";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CanUpdateUserName", CanUpdateUserName);
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            cmd.Parameters.AddWithValue("@Suffix", Suffix);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public AcademicLib.BE.Academic.Transaction.StudentUserCollections getStudentUserList(int UserId, int AcademicYearId, int? ClassId)
        {
            AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl = new BE.Academic.Transaction.StudentUserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
             

            cmd.CommandText = "usp_GetRegisterStudentUserList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentUser beData = new BE.Academic.Transaction.StudentUser();
                    beData.SNo = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.UserId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.UserName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Pwd = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.RegdNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.F_ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.M_ContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Email= reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.F_Email = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.M_Email = reader.GetString(16);

                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections GetEmpCouncellingStatuses(int UserId, int AcademicYearId, int? EmpId)
        {
            AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections dataColl = new BE.FrontDesk.Transaction.EmpCouncellingStatusCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.CommandText = "usp_GetRegistrationCounselorStatus";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatus beData = new BE.FrontDesk.Transaction.EmpCouncellingStatus();
                    beData.Status =((AcademicLib.RE.FrontDesk.ENQUIRYSTATUS) reader.GetInt32(0)).ToString();
                    beData.NoOfCouncelling = Convert.ToInt32(reader[1]);  
                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

    }
}
