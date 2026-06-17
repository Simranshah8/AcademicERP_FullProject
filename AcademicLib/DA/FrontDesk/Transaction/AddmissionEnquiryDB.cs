using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
   internal class AddmissionEnquiryDB
    {
        DataAccessLayer1 dal = null;
        string hostName, dbName;
        public AddmissionEnquiryDB(string hostName, string dbName)
        {
            this.hostName = hostName;
            this.dbName = dbName;
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.FrontDesk.Transaction.AddmissionEnquiry beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName) ;
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@DOB", beData.DOB);
            cmd.Parameters.AddWithValue("@BirthCertificateNo", beData.BirthCertificateNo);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@IsPhysicalDisability", beData.IsPhysicalDisability);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@MediumId", beData.MediumId);
            cmd.Parameters.AddWithValue("@FacultyId", beData.FacultyId);
            cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);
            cmd.Parameters.AddWithValue("@IsTransport", beData.IsTransport);
            cmd.Parameters.AddWithValue("@IsHostel", beData.IsHostel);
            cmd.Parameters.AddWithValue("@IsTiffin", beData.IsTiffin);
            cmd.Parameters.AddWithValue("@Tiffin", beData.Tiffin);
            cmd.Parameters.AddWithValue("@IsTution", beData.IsTution);
            cmd.Parameters.AddWithValue("@IsOtherfaciltity", beData.IsOtherfaciltity);
            cmd.Parameters.AddWithValue("@Photo", beData.Photo);
            cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@F_Profession", beData.F_Profession);
            cmd.Parameters.AddWithValue("@F_ContactNo", beData.F_ContactNo);
            cmd.Parameters.AddWithValue("@F_Email", beData.F_Email);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@M_Profession", beData.M_Profession);
            cmd.Parameters.AddWithValue("@M_ContactNo", beData.M_ContactNo);
            cmd.Parameters.AddWithValue("@M_Email", beData.M_Email);
            cmd.Parameters.AddWithValue("@Ifguradianis", beData.IfGuradianIs);
            cmd.Parameters.AddWithValue("@GuardianName", beData.GuardianName);
            cmd.Parameters.AddWithValue("@G_Relation", beData.G_Relation);
            cmd.Parameters.AddWithValue("@G_Professsion", beData.G_Professsion);
            cmd.Parameters.AddWithValue("@G_Contact", beData.G_Contact);
            cmd.Parameters.AddWithValue("@G_Email", beData.G_Email);
            cmd.Parameters.AddWithValue("@G_Address", beData.G_Address);
            cmd.Parameters.AddWithValue("@EnquiryDate", beData.EnquiryDate);
            cmd.Parameters.AddWithValue("@Sourse", beData.Sourse);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@PA_Province", beData.PA_Province);
            cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
            cmd.Parameters.AddWithValue("@PA_LocalLevel", beData.PA_LocalLevel);
            cmd.Parameters.AddWithValue("@PA_WardNo", beData.PA_WardNo);
            cmd.Parameters.AddWithValue("@PA_StreetName", beData.PA_StreetName);
            cmd.Parameters.AddWithValue("@IsSameAsPermanentAddress", beData.IsSameAsPermanentAddress);
            cmd.Parameters.AddWithValue("@CA_FullAddress", beData.CA_FullAddress);
            cmd.Parameters.AddWithValue("@CA_Province", beData.CA_Province);
            cmd.Parameters.AddWithValue("@CA_District", beData.CA_District);
            cmd.Parameters.AddWithValue("@CA_LocalLevel", beData.CA_LocalLevel);
            cmd.Parameters.AddWithValue("@CA_WardNo", beData.CA_WardNo);
            cmd.Parameters.AddWithValue("@CA_StreetName", beData.CA_StreetName);
            cmd.Parameters.AddWithValue("@PreviousSchool", beData.PreviousSchool);
            cmd.Parameters.AddWithValue("@PreviousSchoolAddress", beData.PreviousSchoolAddress);
            cmd.Parameters.AddWithValue("@PreviousClassGpa", beData.PreviousClassGpa);
            cmd.Parameters.AddWithValue("@OptionalFirst", beData.OptionalFirst);
            cmd.Parameters.AddWithValue("@OptionalSecond", beData.OptionalSecond);
            cmd.Parameters.AddWithValue("@Talent", beData.Talent);
            cmd.Parameters.AddWithValue("@AnyDisease", beData.AnyDisease);
            cmd.Parameters.AddWithValue("@Problem", beData.Problem);
            cmd.Parameters.AddWithValue("@PresentCondition", beData.PresentCondition);
            cmd.Parameters.AddWithValue("@IsFollowupRequired", beData.IsFollowupRequired);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.EnquiryId);
            
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateAdmissionEnquiry";
            }
            else
            {
                cmd.Parameters[67].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddAdmissionEnquiry";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[68].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[69].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[70].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsAnonymous", beData.IsAnonymous);

            cmd.Parameters.AddWithValue("@TransportFacility", beData.TransportFacility);
            cmd.Parameters.AddWithValue("@HostelRequired", beData.HostelRequired);
            cmd.Parameters.AddWithValue("@Otherfaciltity", beData.Otherfaciltity);
            cmd.Parameters.AddWithValue("@FollowupDate", beData.FollowupDate);
            cmd.Parameters.AddWithValue("@FollowupDateTime", beData.FollowUpTime);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

            cmd.Parameters.AddWithValue("@Department", beData.Department);
            cmd.Parameters.AddWithValue("@Shift", beData.Shift);

            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[80].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);
            cmd.Parameters.AddWithValue("@SourceId", beData.SourceId);
            cmd.Parameters.AddWithValue("@CommunicationTypeId", beData.CommunicationTypeId);
            cmd.Parameters.AddWithValue("@FormSale", beData.FormSale);
            cmd.Parameters.AddWithValue("@ReceiptAsLedgerId", beData.ReceiptAsLedgerId);
            cmd.Parameters.AddWithValue("@ReceiptNarration", beData.ReceiptNarration);
            cmd.Parameters.AddWithValue("@FollowupRemarks", beData.FollowupRemarks);

            cmd.Parameters.AddWithValue("@StudentTypeId", beData.StudentTypeId);
            cmd.Parameters.AddWithValue("@F_AnnualIncome", beData.F_AnnualIncome);
            cmd.Parameters.AddWithValue("@M_AnnualIncome", beData.M_AnnualIncome);

            cmd.Parameters.Add("@RegOut", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters[91].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@ReferralCode", beData.ReferralCode);

            cmd.Parameters.AddWithValue("@SEETypeId", beData.SEETypeId);
            cmd.Parameters.AddWithValue("@PlusTwoId", beData.PlusTwoId);

            cmd.Parameters.AddWithValue("@IPAddress", beData.IPAddress);
            cmd.Parameters.AddWithValue("@Agent", beData.Agent);
            cmd.Parameters.AddWithValue("@Browser", beData.Browser);
            cmd.Parameters.AddWithValue("@IeltsToeflScore", beData.IeltsToeflScore);
            cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
            //SEETypeId,PlusTwoId
            //Otherfaciltity

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[67].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[67].Value);

                if (!(cmd.Parameters[68].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[68].Value);

                if (!(cmd.Parameters[69].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[69].Value);

                if (!(cmd.Parameters[70].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[70].Value);

                string regOut = "";
                if (!(cmd.Parameters[91].Value is DBNull))
                    regOut = Convert.ToString(cmd.Parameters[91].Value);

                int autoNumber = 0;
                if (!(cmd.Parameters[80].Value is DBNull))
                    autoNumber = Convert.ToInt32(cmd.Parameters[80].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveAcademicDetails(beData.CUserId, resVal.RId, beData.AcademicDetailsColl);
                    SaveFeeDetailss(beData.CUserId, resVal.RId, beData.FeeItemColl);

                    try
                    {
                        //if (!isModify)
                        if (!beData.AlreadyFormSale)
                        {
                            if (beData.FeeItemColl != null && beData.FeeItemColl.Count > 0 && beData.FormSale)
                            {
                                var newBill = new AcademicLib.BE.Fee.Creation.ManualBilling();
                                newBill.RefNo = regOut;
                                newBill.AdmissionEnquiryId = resVal.RId;
                                newBill.TranId = 0;
                                newBill.BillingDate = DateTime.Today;
                                newBill.Address = beData.Address;
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
                                newBill.RefBillNo = autoNumber.ToString();
                                newBill.RegdNo = resVal.RId.ToString();
                                newBill.StudentName = ((beData.FirstName + " " + beData.MiddleName).Trim() + " " + beData.LastName).Trim();
                                newBill.TotalAmount = newBill.ManualBillingDetailsColl.Sum(p1 => p1.PayableAmt);
                                newBill.Remarks = beData.ReceiptNarration;
                                var billRet = new AcademicLib.BL.Fee.Creation.ManualBilling(beData.CUserId, hostName, dbName).SaveFormData(AcademicYearId, newBill);
                                if (billRet.IsSuccess)
                                    resVal.ResponseId = billRet.RId.ToString();

                            }
                        }
                  
                    }
                    catch { }

                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);                  
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
                cmd.CommandText = "sp_AddAdmissionEnquiryFeeDetails";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveAcademicDetails(int UserId, int TranId, List<AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails beData in beDataColl)
            {
                if(!string.IsNullOrEmpty(beData.ClassName) && !string.IsNullOrEmpty(beData.SchoolColledge))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
                    cmd.Parameters.AddWithValue("@Exam", beData.Exam);
                    cmd.Parameters.AddWithValue("@PassedYear", beData.PassoutYear);
                    cmd.Parameters.AddWithValue("@ObtainedMarks", beData.ObtainMarks);
                    cmd.Parameters.AddWithValue("@ObtainPercent", beData.ObtainPer);
                    cmd.Parameters.AddWithValue("@Division", beData.Division);
                    cmd.Parameters.AddWithValue("@GPA", beData.GPA);
                    cmd.Parameters.AddWithValue("@SchoolColledge", beData.SchoolColledge);
                    cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddAdmissionEngAcademicDetails";
                    cmd.ExecuteNonQuery();
                }
              
            }

        }

        public ResponeValues getAutoNo(int UserId)
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
            cmd.CommandText = "usp_GetEnquiryAutoRegdNo";
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
    
        private void SaveDocument(int UserId, int AdmissionEnquiryId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || AdmissionEnquiryId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AdmissionEnquiryId", AdmissionEnquiryId);
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
                    cmd.CommandText = "usp_AddAdmissionEnquiryAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        //private void SaveAdmissionEnquiryAcademicDetails(int UserId, int AdmissionEnquiryId, List<BE.FrontDesk.Transaction.AdmissionEnquiryAcademicDetails> beDataColl)
        //{
        //    if (beDataColl == null || beDataColl.Count == 0 || AdmissionEnquiryId == 0)
        //        return;

        //    foreach (BE.FrontDesk.Transaction.AdmissionEnquiryAcademicDetails beData in beDataColl)
        //    {

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@AdmissionEnquiryId", AdmissionEnquiryId);
        //        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
        //        cmd.Parameters.AddWithValue("@Exam", beData.Exam);
        //        cmd.Parameters.AddWithValue("@PassedYear", beData.PassedYear);
        //        cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
        //        cmd.Parameters.AddWithValue("@ObtainedMarks", beData.ObtainedMarks);
        //        cmd.Parameters.AddWithValue("@ObtainPercent", beData.ObtainPercent);
        //        cmd.Parameters.AddWithValue("@Division", beData.Division);
        //        cmd.Parameters.AddWithValue("@GPA", beData.GPA);
        //        cmd.Parameters.AddWithValue("@SchoolColledge", beData.SchoolColledge);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_AddAdmissionEnquiryAcademicDetails";
        //        cmd.ExecuteNonQuery();
        //    }

        //}

        public RE.FrontDesk.EnqSummaryCollections getEnqSummary(int UserId, DateTime? dateFrom,DateTime? dateTo, int? EnquiryId = null)
        {
            RE.FrontDesk.EnqSummaryCollections dataColl = new RE.FrontDesk.EnqSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@EnquiryId", EnquiryId);
            cmd.CommandText = "usp_GetEnqSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.EnqSummary beData = new RE.FrontDesk.EnqSummary();
                    beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EnqDate_AD = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.EnqDate_BS = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Gender = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Email = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.F_ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Caste = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ClassName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DOB_AD = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.DOB_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BirthCertificateNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Nationality = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Religion = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.ContactNo = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Email = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.F_Email = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.F_Profession = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.M_ContactNo = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.M_Email = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.M_Profession = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.GuardianName = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.G_Address = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.G_Contact = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.G_Email = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Professsion = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Relation = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.PA_Province = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PA_District = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.PA_LocalLevel = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_WardNo = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.PA_StreetName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.CA_Province = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CA_District = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CA_LocalLevel = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CA_WardNo = reader.GetInt32(38);
                    if (!(reader[39] is DBNull)) beData.CA_StreetName = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.PreviousSchool = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.PreviousSchoolAddress = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.PreviousClassGpa = Convert.ToDouble(reader[42]);
                    if (!(reader[43] is DBNull)) beData.OptionalFirst = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.OptionalSecond = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.Talent = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.IsTransport = reader.GetBoolean(46);
                    if (!(reader[47] is DBNull)) beData.IsHostel = reader.GetBoolean(47);
                    if (!(reader[48] is DBNull)) beData.IsTiffin = reader.GetBoolean(48);
                    if (!(reader[49] is DBNull)) beData.MotherName = reader.GetString(49);

                    if (!(reader[50] is DBNull)) beData.Department = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.Medium = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.Shift = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.Source = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EnquiryId = reader.GetInt32(54);
                    if (!(reader[55] is DBNull)) beData.EnquiryNo = reader.GetInt32(55);

                    if (!(reader[56] is DBNull)) beData.ReceiptNo = reader.GetInt32(56);
                    if (!(reader[57] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[57]);
                    if (!(reader[58] is DBNull)) beData.ReceiptTranId = reader.GetInt32(58);

                    if (!(reader[59] is DBNull)) beData.CommunicationType = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.AutoManualNo = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.FormSale = reader.GetBoolean(61);
                    if (!(reader[62] is DBNull)) beData.Status = reader.GetInt32(62);
                    if (!(reader[63] is DBNull)) beData.StatusRemarks = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.IsAssignCounselor = reader.GetBoolean(64);
                    if (!(reader[65] is DBNull)) beData.Counselor = reader.GetString(65);
                     
                    if (!(reader[66] is DBNull)) beData.EnqRemarks = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.NextFollowupDate = reader.GetDateTime(67);
                    if (!(reader[68] is DBNull)) beData.IsClosed = reader.GetBoolean(68);
                    if (!(reader[69] is DBNull)) beData.ClosedRemarks = reader.GetString(69);
                    if (!(reader[70] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(70);
                    if (!(reader[71] is DBNull)) beData.ClosedMiti = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.EnquiryMiti = reader.GetString(72);
                    if (!(reader[73] is DBNull)) beData.NextFollowupMiti = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.CreateBy = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.ModifyBy = reader.GetString(75);
                    if (!(reader[76] is DBNull)) beData.ClosedBy = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.EnqCommunicationType = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.Age = reader.GetString(78);

                    if (!(reader[79] is DBNull)) beData.PhysicalDisability = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(80);
                    if (!(reader[81] is DBNull)) beData.IsOtherfaciltity = reader.GetBoolean(81);
                    if (!(reader[82] is DBNull)) beData.StudentType= reader.GetString(82);
                    if (!(reader[83] is DBNull)) beData.F_AnnualIncome = Convert.ToDouble(reader[83]);
                    if (!(reader[84] is DBNull)) beData.M_AnnualIncome = Convert.ToDouble(reader[84]);
                    if (!(reader[85] is DBNull)) beData.PreClassName = reader.GetString(85);
                    if (!(reader[86] is DBNull)) beData.Exam = reader.GetString(86);
                    if (!(reader[87] is DBNull)) beData.PassedYear = reader.GetString(87);
                    if (!(reader[88] is DBNull)) beData.SymbolNo = reader.GetString(88);
                    if (!(reader[89] is DBNull)) beData.ObtainedMarks = Convert.ToDouble(reader[89]);
                    if (!(reader[90] is DBNull)) beData.Division = reader.GetString(90);

                    if (!(reader[91] is DBNull)) beData.TransportFacility = reader.GetString(91);
                    if (!(reader[92] is DBNull)) beData.HostelRequired = reader.GetString(92);
                    if (!(reader[93] is DBNull)) beData.Otherfaciltity = reader.GetString(93);
                    if (!(reader[94] is DBNull)) beData.ReferralCode = reader.GetString(94);

                    try
                    {
                        if (!(reader[95] is DBNull)) beData.SLCSEEype = reader.GetString(95);
                        if (!(reader[96] is DBNull)) beData.PlusTwoType = reader.GetString(96);
                        if (!(reader[97] is DBNull)) beData.IeltsToeflScore = Convert.ToDouble(reader[97]);
                        if (!(reader[98] is DBNull)) beData.Qualification = Convert.ToString(reader[98]);
                    }
                    catch { }

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
        public BE.FrontDesk.Transaction.AddmissionEnquiryCollections getAllAddmissionEnquiry(int UserId, int EntityId)
        {
            BE.FrontDesk.Transaction.AddmissionEnquiryCollections dataColl = new BE.FrontDesk.Transaction.AddmissionEnquiryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllAdmissionEnquiry";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.AddmissionEnquiry beData = new BE.FrontDesk.Transaction.AddmissionEnquiry();
                    //beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.MiddleName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.LastName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Gender = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.CasteId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.DOB = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.Nationality = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Religion = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Email = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.ClassId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.MediumId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.FacultyId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.ClassShiftId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.IsTransport = reader.GetBoolean(17);
                    if (!(reader[18] is DBNull)) beData.IsHostel = reader.GetBoolean(18);
                    //if (!(reader[19] is DBNull)) beData.IsOtherfaciltityRequired = reader.GetBoolean(19);
                    //if (!(reader[20] is DBNull)) beData.Photo = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.PhotoPath = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.FatherName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.F_Profession = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.F_ContactNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.F_Email = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.MotherName = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.M_Profession = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.M_ContactNo = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.M_Email = reader.GetString(29);
                   // if (!(reader[30] is DBNull)) beData.Ifguradianis = reader.GetInt32(30);
                    if (!(reader[31] is DBNull)) beData.GuardianName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.G_Relation = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.G_Professsion = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.G_Contact = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.G_Email = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.G_Address = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.EnquiryDate = reader.GetDateTime(37);
                   // if (!(reader[38] is DBNull)) beData.Sourse = reader.GetInt32(38);
                   // if (!(reader[39] is DBNull)) beData.IsFollowupRequired = reader.GetBoolean(39);
                    //if (!(reader[40] is DBNull)) beData.Remarks = reader.GetString(40);
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
        public BE.FrontDesk.Transaction.AddmissionEnquiry getAddmissionEnquiryById(int UserId, int EntityId, int TranId)
        {
            BE.FrontDesk.Transaction.AddmissionEnquiry beData = new BE.FrontDesk.Transaction.AddmissionEnquiry();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAdmissionEnquiryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.AddmissionEnquiry();
                    beData.EnquiryId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Department = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Shift = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Remarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FollowUpTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.FollowupDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.AutoNumber = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.Otherfaciltity = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.HostelRequired = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TransportFacility = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FirstName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.MiddleName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.LastName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Gender = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.CasteId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.DOB = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.BirthCertificateNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Nationality = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Religion = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Address = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ContactNo = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Email = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(22);
                    if (!(reader[23] is DBNull)) beData.ClassId = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.MediumId = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.FacultyId = reader.GetInt32(25);
                    if (!(reader[26] is DBNull)) beData.ClassShiftId = reader.GetInt32(26);
                    if (!(reader[27] is DBNull)) beData.IsTransport = reader.GetBoolean(27);
                    if (!(reader[28] is DBNull)) beData.IsHostel = reader.GetBoolean(28);
                    if (!(reader[29] is DBNull)) beData.IsTiffin = reader.GetBoolean(29);
                    if (!(reader[30] is DBNull)) beData.Tiffin = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.IsOtherfaciltity = reader.GetBoolean(31);
                    if (!(reader[32] is DBNull)) beData.PhotoPath = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.FatherName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.F_Profession = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.F_ContactNo = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.F_Email = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.M_Profession = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.M_ContactNo = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.M_Email = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.IfGuradianIs = reader.GetInt32(40);
                    if (!(reader[41] is DBNull)) beData.GuardianName = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.G_Relation = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.G_Professsion = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.G_Contact = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.G_Email = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.G_Address = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.EnquiryDate = reader.GetDateTime(47);
                    if (!(reader[48] is DBNull)) beData.Sourse = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.PA_FullAddress = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.PA_Province = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.PA_District = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.PA_LocalLevel = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.PA_WardNo = reader.GetInt32(53);
                    if (!(reader[54] is DBNull)) beData.PA_StreetName = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.IsSameAsPermanentAddress = reader.GetBoolean(55);
                    if (!(reader[56] is DBNull)) beData.CA_FullAddress = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.CA_Province = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.CA_District = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.CA_LocalLevel = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.CA_WardNo = reader.GetInt32(60);
                    if (!(reader[61] is DBNull)) beData.CA_StreetName = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.PreviousSchool = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.PreviousSchoolAddress = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.PreviousClassGpa = Convert.ToDouble(reader[64]);
                    if (!(reader[65] is DBNull)) beData.OptionalFirst = reader.GetString(65);
                    if (!(reader[66] is DBNull)) beData.OptionalSecond = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.Talent = reader.GetString(67);
                    if (!(reader[68] is DBNull)) beData.AnyDisease = reader.GetBoolean(68);
                    if (!(reader[69] is DBNull)) beData.Problem = reader.GetString(69);
                    if (!(reader[70] is DBNull)) beData.PresentCondition = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.IsFollowupRequired = reader.GetBoolean(71);
                    if (!(reader[72] is DBNull)) beData.MotherName = reader.GetString(72);
                    if (!(reader[73] is DBNull)) beData.IsTution = reader.GetBoolean(73);
                    if (!(reader[74] is DBNull)) beData.PhysicalDisability = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.SourceId = reader.GetInt32(75);
                    if (!(reader[76] is DBNull)) beData.CommunicationTypeId = reader.GetInt32(76);
                    if (!(reader[77] is DBNull)) beData.AutoManualNo = reader.GetString(77);

                    if (!(reader[78] is DBNull)) beData.ReceiptAsLedgerId = reader.GetInt32(78);
                    if (!(reader[79] is DBNull)) beData.FollowupRemarks = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.FormSale = reader.GetBoolean(80);

                    if (!(reader[81] is DBNull)) beData.StudentTypeId = reader.GetInt32(81);
                    if (!(reader[82] is DBNull)) beData.F_AnnualIncome = Convert.ToDouble(reader[82]);
                    if (!(reader[83] is DBNull)) beData.M_AnnualIncome = Convert.ToDouble(reader[83]);
                    if (!(reader[84] is DBNull)) beData.AcademicYearId = reader.GetInt32(84);
                    if (!(reader[85] is DBNull)) beData.ReceiptNarration = reader.GetString(85);
                    try
                    {
                        if (!(reader[86] is DBNull)) beData.ReferralCode = reader.GetString(86);
                        if (!(reader[87] is DBNull)) beData.SLCSEEype = reader.GetString(87);
                        if (!(reader[88] is DBNull)) beData.PlusTwoType = reader.GetString(88);
                        if (!(reader[89] is DBNull)) beData.IeltsToeflScore = Convert.ToDouble(reader[89]);
                        if (!(reader[90] is DBNull)) beData.Qualification = Convert.ToString(reader[90]);
                    }
                    catch { }

                }

                beData.AcademicDetailsColl = new List<BE.Academic.Transaction.StudentPreviousAcademicDetails>();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                reader.NextResult();
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
                beData.FeeItemColl = new List<BE.Fee.Creation.ManualBillingDetails>();
                while (reader.Read())
                {
                    BE.Fee.Creation.ManualBillingDetails det = new BE.Fee.Creation.ManualBillingDetails();
                    if (!(reader[0] is DBNull)) det.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.FeeItemName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.Qty = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.Rate = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det.DiscountPer = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det.DiscountAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det.PayableAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) det.Remarks = reader.GetString(8);
                    beData.FeeItemColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelAdmissionEnquiryById";
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


        public RE.FrontDesk.EnqFollowupCollections getEnqForFollowup(int UserId,int? AcademicYearId,int FollowupType)
        {
            RE.FrontDesk.EnqFollowupCollections dataColl = new RE.FrontDesk.EnqFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FollowupType", FollowupType);
            cmd.CommandText = "usp_GetAdmissionEnqFollowup";
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
                    //Added on 27 Poush
                    if (!(reader[45] is DBNull)) beData.FollowupDate = reader.GetDateTime(45);
                    if (!(reader[46] is DBNull)) beData.FollowupMiti = reader.GetString(46);

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

        public RE.FrontDesk.EnqFollowupCollections getEnqForCounCelling(int UserId, int? AcademicYearId)
        {
            RE.FrontDesk.EnqFollowupCollections dataColl = new RE.FrontDesk.EnqFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.CommandText = "usp_GetAdmissionEnqCouncelling";
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
                    if (!(reader[34] is DBNull)) beData.FormSale = reader.GetBoolean(34);
                    if (!(reader[35] is DBNull)) beData.Status = reader.GetInt32(35);
                    if (!(reader[36] is DBNull)) beData.StatusRemarks = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.F_Email = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.M_ContactNo = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.M_Email = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.G_Contact = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.G_Email = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.AutoManualNo = reader.GetString(42);

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
            cmd.CommandText = "usp_GetEnquiryFollowupList";
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
            cmd.CommandText = "usp_AddEnquiryFollowup";
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

        public ResponeValues SaveClosed(int UserId, int RefTranId, string Remarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@RefTranId", RefTranId);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.CommandText = "usp_AddEnquiryClosed";
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

        public ResponeValues SaveAssignCounselor(int UserId,int TranId, DateTime? AssignDate, List<int> EmployeeIdColl)
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
                    cmd.CommandText = "usp_AddEnquiryCounselor";
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

        public ResponeValues SaveEnqStatus(int UserId, int TranId,int Status, string Remarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.CommandText = "usp_AddAdmissionEnqStatus";
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
        public AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections GetEmpCouncellingStatuses(int UserId, int AcademicYearId, int? EmpId)
        {
            AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatusCollections dataColl = new BE.FrontDesk.Transaction.EmpCouncellingStatusCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetEnquiryCounselorStatus";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.FrontDesk.Transaction.EmpCouncellingStatus beData = new BE.FrontDesk.Transaction.EmpCouncellingStatus();
                    beData.Status = ((AcademicLib.RE.FrontDesk.ENQUIRYSTATUS)reader.GetInt32(0)).ToString().Replace("COUNCELLING_","");
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
