using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Academic.Transaction
{
    internal class EmployeeDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public EmployeeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.Employee beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Photo", beData.Photo);
            cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
            cmd.Parameters.AddWithValue("@EmployeeCode", beData.EmployeeCode);
            cmd.Parameters.AddWithValue("@EnrollNumber", beData.EnrollNumber);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);            
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@PersnalContactNo", beData.PersnalContactNo);
            cmd.Parameters.AddWithValue("@OfficeContactNo", beData.OfficeContactNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@MaritalStatus", beData.MaritalStatus);
            cmd.Parameters.AddWithValue("@SpouseName", beData.SpouseName);
            cmd.Parameters.AddWithValue("@AnniversaryDate", beData.AnniversaryDate);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@GrandFather", beData.GrandFather);
            cmd.Parameters.AddWithValue("@PanId", beData.PanId);
            cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
            cmd.Parameters.AddWithValue("@CitizenIssueDate", beData.CitizenIssueDate);
            cmd.Parameters.AddWithValue("@CitizenShipIssuePlace", beData.CitizenShipIssuePlace);
            cmd.Parameters.AddWithValue("@DrivindLicenceNo", beData.DrivindLicenceNo);                        
            cmd.Parameters.AddWithValue("@LicenceIssueDate", beData.LicenceIssueDate);
            cmd.Parameters.AddWithValue("@LicenceExpiryDate", beData.LicenceExpiryDate);
            cmd.Parameters.AddWithValue("@PasswordNo", beData.PasswordNo);
            cmd.Parameters.AddWithValue("@PasswordIssueDate", beData.PasswordIssueDate);
            cmd.Parameters.AddWithValue("@PasswordExpiryDate", beData.PasswordExpiryDate);
            cmd.Parameters.AddWithValue("@PasswordIssuePlace", beData.PasswordIssuePlace);
            cmd.Parameters.AddWithValue("@PA_Country", beData.PA_Country);
            cmd.Parameters.AddWithValue("@PA_State", beData.PA_State);
            cmd.Parameters.AddWithValue("@PA_Zone", beData.PA_Zone);
            cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
            cmd.Parameters.AddWithValue("@PA_City", beData.PA_City);
            cmd.Parameters.AddWithValue("@PA_Municipality", beData.PA_Municipality);
            cmd.Parameters.AddWithValue("@PA_Ward", beData.PA_Ward);
            cmd.Parameters.AddWithValue("@PA_Street", beData.PA_Street);
            cmd.Parameters.AddWithValue("@PA_HouseNo", beData.PA_HouseNo);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@TA_Counrty", beData.TA_Counrty);
            cmd.Parameters.AddWithValue("@TA_State", beData.TA_State);
            cmd.Parameters.AddWithValue("@TA_Zone", beData.TA_Zone);
            cmd.Parameters.AddWithValue("@TA_District", beData.TA_District);
            cmd.Parameters.AddWithValue("@TA_City", beData.TA_City);
            cmd.Parameters.AddWithValue("@TA_Municipality", beData.TA_Municipality);
            cmd.Parameters.AddWithValue("@TA_Ward", beData.TA_Ward);
            cmd.Parameters.AddWithValue("@TA_Street", beData.TA_Street);
            cmd.Parameters.AddWithValue("@TA_HouseNo", beData.TA_HouseNo);
            cmd.Parameters.AddWithValue("@TA_FullAddress", beData.TA_FullAddress);
            cmd.Parameters.AddWithValue("@EC_PersonalName", beData.EC_PersonalName);
            cmd.Parameters.AddWithValue("@EC_Relationship", beData.EC_Relationship);
            cmd.Parameters.AddWithValue("@EC_Address", beData.EC_Address);
            cmd.Parameters.AddWithValue("@EC_Phone", beData.EC_Phone);
            cmd.Parameters.AddWithValue("@EC_Mobile", beData.EC_Mobile);
            cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
            cmd.Parameters.AddWithValue("@DesignationId", beData.DesignationId);
            cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
            cmd.Parameters.AddWithValue("@LevelId", beData.LevelId);
            cmd.Parameters.AddWithValue("@JobTitle", beData.JobTitle);
            cmd.Parameters.AddWithValue("@ServiceTypeId", beData.ServiceTypeId);
            cmd.Parameters.AddWithValue("@DateofJoining", beData.DateOfJoining);
            cmd.Parameters.AddWithValue("@DateOfConfirmation", beData.DateOfConfirmation);
            cmd.Parameters.AddWithValue("@DateOfRetirement", beData.DateOfRetirement);
            cmd.Parameters.AddWithValue("@RemoteArea", beData.RemoteArea);
            cmd.Parameters.AddWithValue("@Disability", beData.Disability);
            cmd.Parameters.AddWithValue("@AccessionNo", beData.AccessionNo);
            cmd.Parameters.AddWithValue("@SSFNo", beData.SSFNo);
            cmd.Parameters.AddWithValue("@CITCode", beData.CITCode);
            cmd.Parameters.AddWithValue("@CITAcNo", beData.CITAcNo);
            cmd.Parameters.AddWithValue("@CIT_Amount", beData.CIT_Amount);
            cmd.Parameters.AddWithValue("@CIT_Nominee", beData.CIT_Nominee);
            cmd.Parameters.AddWithValue("@CIT_RelationShip", beData.CIT_RelationShip);
            cmd.Parameters.AddWithValue("@CIT_IDType", beData.CIT_IDType);
            cmd.Parameters.AddWithValue("@CIT_IDNo", beData.CIT_IDNo);
            cmd.Parameters.AddWithValue("@CIT_EntryDate", beData.CIT_EntryDate);
            cmd.Parameters.AddWithValue("@BankName", beData.BankName);
            cmd.Parameters.AddWithValue("@BA_AccountName", beData.BA_AccountName);
            cmd.Parameters.AddWithValue("@BA_AccountNo", beData.BA_AccountNo);
            cmd.Parameters.AddWithValue("@BA_Branch", beData.BA_Branch);
            cmd.Parameters.AddWithValue("@BA_IsForPayroll", beData.BA_IsForPayroll);
            cmd.Parameters.AddWithValue("@LI_InsuranceType", beData.LI_InsuranceType);
            cmd.Parameters.AddWithValue("@LI_InsuranceCompany", beData.LI_InsuranceCompany);
            cmd.Parameters.AddWithValue("@LI_PolicyName", beData.LI_PolicyName);
            cmd.Parameters.AddWithValue("@LI_PolicyNo", beData.LI_PolicyNo);
            cmd.Parameters.AddWithValue("@LI_PolicyAmount", beData.LI_PolicyAmount);
            cmd.Parameters.AddWithValue("@LI_PolicyStartDate", beData.LI_PolicyStartDate);
            cmd.Parameters.AddWithValue("@LI_PolicyLastDate", beData.LI_PolicyLastDate);
            cmd.Parameters.AddWithValue("@LI_PremiunAmount", beData.LI_PremiunAmount);
            cmd.Parameters.AddWithValue("@LI_PaymentType", beData.LI_PaymentType);
            cmd.Parameters.AddWithValue("@LI_StartMonth", beData.LI_StartMonth);
            cmd.Parameters.AddWithValue("@LI_IsDeductFromSalary", beData.LI_IsDeductFromSalary);
            cmd.Parameters.AddWithValue("@LI_Remarks", beData.LI_Remarks);
            cmd.Parameters.AddWithValue("@HI_InsurenceType", beData.HI_InsurenceType);
            cmd.Parameters.AddWithValue("@HI_InsuranceCompany", beData.HI_InsuranceCompany);
            cmd.Parameters.AddWithValue("@HI_PolicyName", beData.HI_PolicyName);
            cmd.Parameters.AddWithValue("@HI_PolicyNo", beData.HI_PolicyNo);
            cmd.Parameters.AddWithValue("@HI_PolicyAmount", beData.HI_PolicyAmount);
            cmd.Parameters.AddWithValue("@HI_PolicyStartDate", beData.HI_PolicyStartDate);
            cmd.Parameters.AddWithValue("@HI_PolicyLastDate", beData.HI_PolicyLastDate);
            cmd.Parameters.AddWithValue("@HI_PremiumAmount", beData.HI_PremiumAmount);
            cmd.Parameters.AddWithValue("@HI_PaymentType", beData.HI_PaymentType);
            cmd.Parameters.AddWithValue("@HI_StartMonth", beData.HI_StartMonth);
            cmd.Parameters.AddWithValue("@HI_IsDeductFromSalary", beData.HI_IsDeductFromSalary);
            cmd.Parameters.AddWithValue("@HI_Remarks", beData.HI_Remarks);
            cmd.Parameters.AddWithValue("@AD_Ledger", beData.AD_Ledger);
            cmd.Parameters.AddWithValue("@AD_CostCenter", beData.AD_CostCenter);
            cmd.Parameters.AddWithValue("@AD_OTLedger", beData.AD_OTLedger);
            cmd.Parameters.AddWithValue("@S_FirstLevelId", beData.S_FirstLevelId);
            cmd.Parameters.AddWithValue("@S_SecondLevelId", beData.S_SecondLevelId);
            cmd.Parameters.AddWithValue("@S_ThirdLevel", beData.S_ThirdLevel);
            cmd.Parameters.AddWithValue("@Signature", beData.Signature);
            cmd.Parameters.AddWithValue("@SignaturePath", beData.SignaturePath);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateEmployee";
            }
            else
            {
                cmd.Parameters[117].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployee";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[118].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[119].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[120].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);

            cmd.Parameters.AddWithValue("@MotherTonque", beData.MotherTonque);
            cmd.Parameters.AddWithValue("@Rank", beData.Rank);
            cmd.Parameters.AddWithValue("@Position", beData.Position);
            cmd.Parameters.AddWithValue("@TeacherType", beData.TeacherType);
            cmd.Parameters.AddWithValue("@TeachingLanguage", beData.TeachingLanguage);
            cmd.Parameters.AddWithValue("@LicenseNo", beData.LicenseNo);
            cmd.Parameters.AddWithValue("@TrkNo", beData.TrkNo);
            cmd.Parameters.AddWithValue("@PFAccountNo", beData.PFAccountNo);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@DOBBS_Str", beData.DOBBS_Str);
            cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);
            cmd.Parameters.AddWithValue("@IsTeaching", beData.IsTeaching);

            cmd.Parameters.AddWithValue("@SubjectTeacherId", beData.SubjectTeacherId);
            cmd.Parameters.AddWithValue("@IsPhysicalDisability", beData.IsPhysicalDisability);
            cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);

            //FatherContactNo,MotherContactNo,SpouseContactNo,OfficeEmailId
            cmd.Parameters.AddWithValue("@FatherContactNo", beData.FatherContactNo);
            cmd.Parameters.AddWithValue("@MotherContactNo", beData.MotherContactNo);
            cmd.Parameters.AddWithValue("@SpouseContactNo", beData.SpouseContactNo);
            cmd.Parameters.AddWithValue("@OfficeEmailId", beData.OfficeEmailId);
            cmd.Parameters.AddWithValue("@SystemUserId", beData.SystemUserId);
            cmd.Parameters.AddWithValue("@TaxRuleAs", beData.TaxRuleAs);

            //Added By Suresh on 14 Poush 2081
            cmd.Parameters.AddWithValue("@LicenceIssuePlace", beData.LicenceIssuePlace);
            cmd.Parameters.AddWithValue("@SalaryApplicableYearId", beData.SalaryApplicableYearId);
            cmd.Parameters.AddWithValue("@SalaryApplicableMonthId", beData.SalaryApplicableMonthId);
            cmd.Parameters.AddWithValue("@FirstNameNP", beData.FirstNameNP);
            cmd.Parameters.AddWithValue("@MiddleNameNP", beData.MiddleNameNP);
            cmd.Parameters.AddWithValue("@LastNameNP", beData.LastNameNP);
            cmd.Parameters.AddWithValue("@CitizenFrontPhoto", beData.CitizenFrontPhoto);
            cmd.Parameters.AddWithValue("@CitizenBackPhoto", beData.CitizenBackPhoto);
            cmd.Parameters.AddWithValue("@NIDNo", beData.NIDNo);
            cmd.Parameters.AddWithValue("@NIDPhoto", beData.NIDPhoto);
            cmd.Parameters.AddWithValue("@isEDJ", beData.isEDJ);
            cmd.Parameters.AddWithValue("@EDJ", beData.EDJ);
            cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[117].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[117].Value);

                if (!(cmd.Parameters[118].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[118].Value);

                if (!(cmd.Parameters[119].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[119].Value);

                if (!(cmd.Parameters[120].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[120].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveEmployeeAcademicQualification(beData.CUserId, resVal.RId, beData.AcademicQualificationColl);
                    SaveEmployeeWorkExperience(beData.CUserId, resVal.RId, beData.WorkExperienceColl);
                    SaveEmployeeDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
                    SaveEmployeeBank(beData.CUserId, resVal.RId, beData.BankList);
                    SaveClassShift(beData.CUserId, resVal.RId, beData.ClassShiftIdColl);
                    SaveEmployeeResearchPublication(beData.CUserId, resVal.RId, beData.EmployeeResearchPublicationColl);
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
        public ResponeValues SaveEmployeeLeft(BE.Academic.Transaction.EmployeeLeft beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@LeftDate", beData.LeftDate_AD);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.CommandText = "usp_AddLeftEmployee";
            cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@LeftTypeId", beData.LeftTypeId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveLeftEmployeeDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
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
        private void SaveLeftEmployeeDocument(int UserId, int TranId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
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
                    cmd.CommandText = "usp_AddEmployeeLeftAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        private void SaveClassShift(int UserId, int EmployeeId, List<int> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (int beData in beDataColl)
            {
                if (beData>0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@ClassShiftId", UserId);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);                    
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tbl_EmployeeClassShift(ClassShiftId,EmployeeId) values(@ClassShiftId,@EmployeeId)";
                    cmd.ExecuteNonQuery();
                }

            }

        }
        private void SaveEmployeeBank(int UserId, int EmployeeId, List<BE.Academic.Transaction.EmployeeBankAccount> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeBankAccount beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.BankName) && !string.IsNullOrEmpty(beData.AccountName))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    cmd.Parameters.AddWithValue("@BankName", beData.BankName);
                    cmd.Parameters.AddWithValue("@AccountName", beData.AccountName);                    
                    cmd.Parameters.AddWithValue("@AccountNo", beData.AccountNo);
                    cmd.Parameters.AddWithValue("@Branch", beData.Branch);
                    cmd.Parameters.AddWithValue("@ForPayRoll", beData.ForPayRoll);

                    //Added By Suresh on poush 15 2081
                    cmd.Parameters.AddWithValue("@BankId", beData.BankId);
                    //Ends
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmployeeBankAccount";
                    cmd.ExecuteNonQuery();
                }

            }

        }
        private void SaveEmployeeAcademicQualification(int UserId, int EmployeeId, List<BE.Academic.Transaction.EmployeeAcademicQualification> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeAcademicQualification beData in beDataColl)
            {
                if(!string.IsNullOrEmpty(beData.DegreeName) && !string.IsNullOrEmpty(beData.BoardUniversity))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    cmd.Parameters.AddWithValue("@DegreeName", beData.DegreeName);
                    cmd.Parameters.AddWithValue("@BoardUniversity", beData.BoardUniversity);
                    cmd.Parameters.AddWithValue("@PassedYear", beData.PassedYear);
                    cmd.Parameters.AddWithValue("@GradePercentage", beData.GradePercentage);
                    cmd.Parameters.AddWithValue("@RegistrationNo", beData.RegistrationNo);
                    cmd.Parameters.AddWithValue("@NameOfInstitution", beData.NameOfInstitution);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmployeeAcademicQualification";
                    cmd.ExecuteNonQuery();
                }
                
            }

        }
        private void SaveEmployeeResearchPublication(int UserId, int EmployeeId, BE.Academic.Transaction.EmployeeResearchPublicationCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeResearchPublication beData in beDataColl)
            {
                // FIX: Process only if ResearchTitle is NOT empty
                if (string.IsNullOrWhiteSpace(beData.ResearchTitle))
                    continue;

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@ResearchTitle", beData.ResearchTitle);
                cmd.Parameters.AddWithValue("@PublicationDate", beData.PublicationDate);
                cmd.Parameters.AddWithValue("@JournalName", beData.JournalName);
                cmd.Parameters.AddWithValue("@Coauthors", beData.Coauthors);
                cmd.Parameters.AddWithValue("@Abstract_Link", beData.Abstract_Link);
                cmd.Parameters.AddWithValue("@PublicationType", beData.PublicationType);
                cmd.Parameters.AddWithValue("@DOI_ISSNNo", beData.DOI_ISSNNo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddEmployeeResearchPublication";
                cmd.ExecuteNonQuery();
            }
        }
        private void SaveEmployeeWorkExperience(int UserId, int EmployeeId, List<BE.Academic.Transaction.EmployeeWorkExperience> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeWorkExperience beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Organization) && !string.IsNullOrEmpty(beData.JobTitle))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    cmd.Parameters.AddWithValue("@Organization", beData.Organization);
                    cmd.Parameters.AddWithValue("@Department", beData.Department);
                    cmd.Parameters.AddWithValue("@JobTitle", beData.JobTitle);
                    cmd.Parameters.AddWithValue("@StartDate", beData.StartDate);                   
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    //Added By Suresh on poush 14
                    cmd.Parameters.AddWithValue("@EndDate", beData.EndDate);
                    //Ends
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmployeeWorkExperience";
                    cmd.ExecuteNonQuery();
                }
                
            }

        }
        private void SaveEmployeeDocument(int UserId, int EmployeeId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
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
                    cmd.CommandText = "usp_AddEmployeeAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

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
            cmd.CommandText = "usp_GetEmployeeAutoRegdNo";
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
        public ResponeValues UpdateEnrollCardNo(int UserId,RE.Academic.EmployeeSummaryCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                foreach (var beData in dataColl)
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                    cmd.Parameters.AddWithValue("@EnrollNumber", beData.EnrollNumber);
                    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + " update tbl_Employee set EnrollNumber=@EnrollNumber,CardNo=@CardNo where EmployeeId=@EmployeeId";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Enroll & Card No. Updated";
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

        public ResponeValues UpdateDOB(int UserId,List<BE.Academic.Transaction.ImportEmployeeDOB> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete from tmpEmploeeDOB";
            cmd.ExecuteNonQuery();
            try
            {
                foreach (var beData in dataColl)
                {
                    if(beData.EmployeeId.HasValue && beData.EmployeeId.Value > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);

                        if(beData.DOB_AD.HasValue)
                            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
                        else
                            cmd.Parameters.AddWithValue("@DOB_AD", DBNull.Value);

                        cmd.Parameters.AddWithValue("@DOB_BS", beData.DOB_BS);

                        cmd.Parameters.AddWithValue("@NY", beData.NY);
                        cmd.Parameters.AddWithValue("@NM", beData.NM);
                        cmd.Parameters.AddWithValue("@ND", beData.ND);
                        cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tmpEmploeeDOB(EmployeeId,DOB_AD,DOB_BS,NY,NM,ND) values(@EmployeeId,@DOB_AD,@DOB_BS,@NY,@NM,@ND)";
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateEmpDOB";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Employee DOB Updated";
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
        public AcademicLib.API.Teacher.EmployeeProfile getEmployeeForApp(int UserId, int? EmployeeId)
        {
            AcademicLib.API.Teacher.EmployeeProfile beData = new API.Teacher.EmployeeProfile();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetEmployeeProfileForApp";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new API.Teacher.EmployeeProfile();
                    beData.EmployeeId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.Code = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EnrollNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Department = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Category = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FirstName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MiddleName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.LastName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Gender = reader.GetString(10);                     
                    if (!(reader[11] is DBNull)) beData.DOB_AD = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.DOB_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Qualification = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.WorkExperience = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.TotalDocument = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SSFNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.CITCode = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.CITAccountNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.BankName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BankAccountNo = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.LifeInsuranceCompany = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.LifeInsurancePolicyNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.LifeInsurancePAmount = Convert.ToDouble(reader[23]);
                    if (!(reader[24] is DBNull)) beData.LifeInsurancePaymentType = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.PhotoPath = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.CurrentAddress = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.PermanentAddress = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.CurrentDistrict = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.PermanentDistrict = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.FatherName = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.MotherName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.SpouseName = reader.GetString(32);

                    if (!(reader[33] is DBNull)) beData.About = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.ContactNo = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BloodGroup = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.Nationality = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.Religion = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.DateOfJoining_AD = reader.GetDateTime(38);
                    if (!(reader[39] is DBNull)) beData.DateOfJoining_BS = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.UserName = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.SignaturePath = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.EMSId = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.EmailId = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.AnniversaryDateAD = reader.GetDateTime(44);
                    if (!(reader[45] is DBNull)) beData.AnniversaryDateBS = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.CasteId = reader.GetInt32(46);
                    if (!(reader[47] is DBNull)) beData.CasteName = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.GrandFather = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.PA_Country = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.PA_State = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.PA_Zone = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.PA_District = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.PA_City = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.PA_Municipality = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.PA_Ward = reader.GetInt32(55);
                    if (!(reader[56] is DBNull)) beData.PA_Street = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.PA_HouseNo = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.PA_FullAddress = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.TA_Country = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.TA_State = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.TA_Zone = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.TA_District = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.TA_City = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.TA_Municipality = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.TA_Ward = reader.GetInt32(65);
                    if (!(reader[66] is DBNull)) beData.TA_Street = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.TA_HouseNo = reader.GetString(67);
                    if (!(reader[68] is DBNull)) beData.TA_FullAddress = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.MaritalStatus = reader.GetString(69);
                    if (!(reader[70] is DBNull)) beData.PanId = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.CitizenshipNo = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.CitizenIssueDate = reader.GetDateTime(72);
                    if (!(reader[73] is DBNull)) beData.CitizenShipIssuePlace = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.CITAcNo = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.CIT_Amount =Convert.ToDouble(reader[75]);
                    if (!(reader[76] is DBNull)) beData.CIT_Nominee = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.CIT_RelationShip = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.CIT_IDType = reader.GetString(78);
                    if (!(reader[79] is DBNull)) beData.CIT_IDNo = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.CIT_EntryDate = reader.GetDateTime(80);
                    if (!(reader[81] is DBNull)) beData.OfficeContactNo = reader.GetString(81);
                    if (!(reader[82] is DBNull)) beData.PersonalContactNo = reader.GetString(82);
                    if (!(reader[83] is DBNull)) beData.ServiceType = reader.GetString(83);
                    if (!(reader[84] is DBNull)) beData.RemoteArea = Convert.ToString(reader[84]);
                    if (!(reader[85] is DBNull)) beData.DateOfConfirmation = reader.GetDateTime(85);
                    if (!(reader[86] is DBNull)) beData.DateOfRetirement = reader.GetDateTime(86);
                    if (!(reader[87] is DBNull)) beData.MitiOfConfirmation = reader.GetString(87);
                    if (!(reader[88] is DBNull)) beData.MitiOfRetirement = reader.GetString(88);
                    if (!(reader[89] is DBNull)) beData.Supervisor1 = reader.GetString(89);
                    if (!(reader[90] is DBNull)) beData.Supervisor2 = reader.GetString(90);
                    if (!(reader[91] is DBNull)) beData.Supervisor3 = reader.GetString(91);
                    if (!(reader[92] is DBNull)) beData.Pwd = reader.GetString(92);
                    if (!(reader[93] is DBNull)) beData.QRCode = reader.GetString(93);
                    if (!(reader[94] is DBNull)) beData.IsLeft = Convert.ToBoolean(reader[94]);
                    if (!(reader[95] is DBNull)) beData.LeftDate = Convert.ToDateTime(reader[95]);
                    if (!(reader[96] is DBNull)) beData.LeftDate_BS = reader.GetString(96);
                    if (!(reader[97] is DBNull)) beData.LeftRemarks = reader.GetString(97);
                    if (!(reader[98] is DBNull)) beData.OfficeEmailId = reader.GetString(98);
                    if (!(reader[99] is DBNull)) beData.SpouseContactNo = reader.GetString(99);
                    if (!(reader[100] is DBNull)) beData.FatherContactNo = reader.GetString(100);
                    if (!(reader[101] is DBNull)) beData.MotherContactNo = reader.GetString(101);
                    if (!(reader[102] is DBNull)) beData.AgeDet = reader.GetString(102);
                    if (!(reader[103] is DBNull)) beData.ServicePeriod = reader.GetString(103);
                    if (!(reader[104] is DBNull)) beData.Level = reader.GetString(104);
                    if (!(reader[105] is DBNull)) beData.AccessionNo = reader.GetString(105);
                    if (!(reader[106] is DBNull)) beData.EmployeeStatus = reader.GetString(106);
                    if (!(reader[107] is DBNull)) beData.UserId = reader.GetInt32(107);
                }
                reader.NextResult();
                beData.QualificationColl = new List<API.Teacher.AcademicQualifications>();
                beData.WorkExperienceColl = new List<API.Teacher.WorkExperience>();

                while (reader.Read())
                {
                    API.Teacher.AcademicQualifications q = new API.Teacher.AcademicQualifications();
                    if (!(reader[0] is DBNull)) q.DepartmentName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) q.University = reader.GetString(1);
                    if (!(reader[2] is DBNull)) q.PassedYear = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) q.GraduatePercentage = Convert.ToDouble(reader[3]);
                    beData.QualificationColl.Add(q);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    API.Teacher.WorkExperience q = new API.Teacher.WorkExperience();
                    if (!(reader[0] is DBNull)) q.Organization = reader.GetString(0);
                    if (!(reader[1] is DBNull)) q.Department = reader.GetString(1);
                    if (!(reader[2] is DBNull)) q.JobTitle = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) q.StartDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) q.EndDate = Convert.ToDateTime(reader[4]);
                    if (!(reader[5] is DBNull)) q.Remarks = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) q.StartMiti = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) q.EndMiti = Convert.ToString(reader[7]);
                    beData.WorkExperienceColl.Add(q);
                }
                reader.NextResult();
                beData.BankDetailsColl = new List<API.Teacher.BankDetails>();
                while (reader.Read())
                {
                    API.Teacher.BankDetails q = new API.Teacher.BankDetails();
                    if (!(reader[0] is DBNull)) q.BankName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) q.AccountName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) q.AccountNo = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) q.Branch = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) q.ForPayRoll = Convert.ToBoolean(reader[4]);
                    beData.BankDetailsColl.Add(q);
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
        public AcademicLib.BE.Academic.Transaction.EmployeeCollections getAllEmployee(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmployeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllEmployee";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.Employee beData = new AcademicLib.BE.Academic.Transaction.Employee();
                    beData.EmployeeId = reader.GetInt32(0);
                    //if (!(reader[1] is DBNull)) beData.Photo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PhotoPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EmployeeCode = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.EnrollNumber = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FirstName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MiddleName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LastName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Gender = reader.GetInt32(8);
                   // if (!(reader[9] is DBNull)) beData.DOB_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.DOB_AD = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.BloodGroup = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Religion = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Nationality = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.PersnalContactNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.OfficeContactNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.EmailId = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.MaritalStatus = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.SpouseName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.AnniversaryDate = reader.GetDateTime(19);
                    if (!(reader[20] is DBNull)) beData.FatherName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.MotherName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.GrandFather = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.PanId = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CitizenshipNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.CitizenIssueDate = reader.GetDateTime(25);
                    if (!(reader[26] is DBNull)) beData.CitizenShipIssuePlace = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.DrivindLicenceNo = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.LicenceIssueDate = reader.GetDateTime(28);
                    if (!(reader[29] is DBNull)) beData.LicenceExpiryDate = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.PasswordNo = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.PasswordIssueDate = reader.GetDateTime(31);
                    if (!(reader[32] is DBNull)) beData.PasswordExpiryDate = reader.GetDateTime(32);
                    if (!(reader[33] is DBNull)) beData.PasswordIssuePlace = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_Country = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.PA_State = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.PA_Zone = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.PA_District = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.PA_City = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.PA_Municipality = reader.GetString(39);
                    //if (!(reader[40] is DBNull)) beData.PA_Ward = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.PA_Street = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.PA_HouseNo = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.PA_FullAddress = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.TA_Counrty = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.TA_State = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.TA_Zone = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.TA_District = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.TA_City = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.TA_Municipality = reader.GetString(49);
                   //if (!(reader[50] is DBNull)) beData.TA_Ward = reader.GetString(50);
                    //if (!(reader[51] is DBNull)) beData.TA_Street = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.TA_HouseNo = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.TA_FullAddress = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EC_PersonalName = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.EC_Relationship = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.EC_Address = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.EC_Phone = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.EC_Mobile = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.DepartmentId = reader.GetInt32(59);
                    if (!(reader[60] is DBNull)) beData.DesignationId = reader.GetInt32(60);
                    if (!(reader[61] is DBNull)) beData.CategoryId = reader.GetInt32(61);
                    if (!(reader[62] is DBNull)) beData.LevelId = reader.GetInt32(62);
                    if (!(reader[63] is DBNull)) beData.JobTitle = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.ServiceTypeId = reader.GetInt32(64);
                    if (!(reader[65] is DBNull)) beData.DateOfJoining = reader.GetDateTime(65);
                    if (!(reader[66] is DBNull)) beData.DateOfConfirmation = reader.GetDateTime(66);
                    if (!(reader[67] is DBNull)) beData.DateOfRetirement = reader.GetDateTime(67);
                    //if (!(reader[68] is DBNull)) beData.RemoteArea = reader.GetInt32(68);
                    //if (!(reader[69] is DBNull)) beData.Disability = reader.GetInt32(69);
                    if (!(reader[70] is DBNull)) beData.AccessionNo = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.SSFNo = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.CITCode = reader.GetString(72);
                    if (!(reader[73] is DBNull)) beData.CITAcNo = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.CIT_Amount = reader.GetFloat(74);
                    if (!(reader[75] is DBNull)) beData.CIT_Nominee = reader.GetString(75);
                    if (!(reader[76] is DBNull)) beData.CIT_RelationShip = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.CIT_IDType = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.CIT_IDNo = reader.GetString(78);
                    if (!(reader[79] is DBNull)) beData.CIT_EntryDate = reader.GetDateTime(79);
                    if (!(reader[80] is DBNull)) beData.BankName = reader.GetString(80);
                    if (!(reader[81] is DBNull)) beData.BA_AccountName = reader.GetString(81);
                    if (!(reader[82] is DBNull)) beData.BA_AccountNo = reader.GetString(82);
                    if (!(reader[83] is DBNull)) beData.BA_Branch = reader.GetString(83);
                    if (!(reader[84] is DBNull)) beData.BA_IsForPayroll = reader.GetBoolean(84);
                    if (!(reader[85] is DBNull)) beData.LI_InsuranceType = reader.GetInt32(85);
                    if (!(reader[86] is DBNull)) beData.LI_InsuranceCompany = reader.GetString(86);
                    if (!(reader[87] is DBNull)) beData.LI_PolicyName = reader.GetString(87);
                    if (!(reader[88] is DBNull)) beData.LI_PolicyNo = reader.GetString(88);
                    if (!(reader[89] is DBNull)) beData.LI_PolicyAmount = reader.GetFloat(89);
                    if (!(reader[90] is DBNull)) beData.LI_PolicyStartDate = reader.GetDateTime(90);
                    if (!(reader[91] is DBNull)) beData.LI_PolicyLastDate = reader.GetDateTime(91);
                    if (!(reader[92] is DBNull)) beData.LI_PremiunAmount = reader.GetFloat(92);
                    if (!(reader[93] is DBNull)) beData.LI_PaymentType = reader.GetString(93);
                    if (!(reader[94] is DBNull)) beData.LI_StartMonth = reader.GetInt32(94);
                    if (!(reader[95] is DBNull)) beData.LI_IsDeductFromSalary = reader.GetBoolean(95);
                    if (!(reader[96] is DBNull)) beData.LI_Remarks = reader.GetString(96);
                    if (!(reader[97] is DBNull)) beData.HI_InsurenceType = reader.GetInt32(97);
                    if (!(reader[98] is DBNull)) beData.HI_InsuranceCompany = reader.GetString(98);
                    if (!(reader[99] is DBNull)) beData.HI_PolicyName = reader.GetString(99);
                    if (!(reader[100] is DBNull)) beData.HI_PolicyNo = reader.GetString(100);
                    if (!(reader[101] is DBNull)) beData.HI_PolicyAmount = reader.GetFloat(101);
                    if (!(reader[102] is DBNull)) beData.HI_PolicyStartDate = reader.GetDateTime(102);
                    if (!(reader[103] is DBNull)) beData.HI_PolicyLastDate = reader.GetDateTime(103);
                    if (!(reader[104] is DBNull)) beData.HI_PremiumAmount = reader.GetFloat(104);
                    if (!(reader[105] is DBNull)) beData.HI_PaymentType = reader.GetInt32(105);
                    if (!(reader[106] is DBNull)) beData.HI_StartMonth = reader.GetInt32(106);
                    if (!(reader[107] is DBNull)) beData.HI_IsDeductFromSalary = reader.GetBoolean(107);
                    if (!(reader[108] is DBNull)) beData.HI_Remarks = reader.GetString(108);
                    if (!(reader[109] is DBNull)) beData.AD_Ledger = reader.GetString(109);
                    if (!(reader[110] is DBNull)) beData.AD_CostCenter = reader.GetString(110);
                    if (!(reader[111] is DBNull)) beData.AD_OTLedger = reader.GetString(111);
                    if (!(reader[112] is DBNull)) beData.S_FirstLevelId = reader.GetInt32(112);
                    if (!(reader[113] is DBNull)) beData.S_SecondLevelId = reader.GetInt32(113);
                    if (!(reader[114] is DBNull)) beData.S_ThirdLevel = reader.GetInt32(114);
                    //if (!(reader[115] is DBNull)) beData.Signature = reader.GetString(115);
                    //if (!(reader[116] is DBNull)) beData.SignaturePath = reader.GetString(116);

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
        public AcademicLib.BE.Academic.Transaction.Employee getEmployeeById(int UserId, int EntityId, int EmployeeId)
        {
            AcademicLib.BE.Academic.Transaction.Employee beData = new AcademicLib.BE.Academic.Transaction.Employee();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.Employee();
                    beData.EmployeeId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.PhotoPath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EnrollNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.CardNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FirstName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MiddleName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LastName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Gender = reader.GetInt32(8);                    
                    if (!(reader[9] is DBNull)) beData.DOB_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.BloodGroup = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Religion = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Nationality = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PersnalContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.OfficeContactNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.EmailId = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.MaritalStatus = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.SpouseName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.AnniversaryDate = reader.GetDateTime(18);
                    if (!(reader[19] is DBNull)) beData.FatherName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.MotherName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.GrandFather = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.PanId = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.CitizenshipNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CitizenIssueDate = reader.GetDateTime(24);
                    if (!(reader[25] is DBNull)) beData.CitizenShipIssuePlace = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.DrivindLicenceNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.LicenceIssueDate = reader.GetDateTime(27);
                    if (!(reader[28] is DBNull)) beData.LicenceExpiryDate = reader.GetDateTime(28);
                    if (!(reader[29] is DBNull)) beData.PasswordNo = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.PasswordIssueDate = reader.GetDateTime(30);
                    if (!(reader[31] is DBNull)) beData.PasswordExpiryDate = reader.GetDateTime(31);
                    if (!(reader[32] is DBNull)) beData.PasswordIssuePlace = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PA_Country = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PA_State = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.PA_Zone = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.PA_District = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.PA_City = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.PA_Municipality = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.PA_Ward = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.PA_Street = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.PA_HouseNo = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.PA_FullAddress = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.TA_Counrty = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.TA_State = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.TA_Zone = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.TA_District = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.TA_City = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.TA_Municipality = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.TA_Ward = reader.GetInt32(49);
                    if (!(reader[50] is DBNull)) beData.TA_Street = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.TA_HouseNo = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.TA_FullAddress = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.EC_PersonalName = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.EC_Relationship = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.EC_Address = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.EC_Phone = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.EC_Mobile = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.DepartmentId = reader.GetInt32(58);
                    if (!(reader[59] is DBNull)) beData.DesignationId = reader.GetInt32(59);
                    if (!(reader[60] is DBNull)) beData.CategoryId = reader.GetInt32(60);
                    if (!(reader[61] is DBNull)) beData.LevelId = reader.GetInt32(61);
                    if (!(reader[62] is DBNull)) beData.JobTitle = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.ServiceTypeId = reader.GetInt32(63);
                    if (!(reader[64] is DBNull)) beData.DateOfJoining = reader.GetDateTime(64);
                    if (!(reader[65] is DBNull)) beData.DateOfConfirmation = reader.GetDateTime(65);
                    if (!(reader[66] is DBNull)) beData.DateOfRetirement = reader.GetDateTime(66);
                    if (!(reader[67] is DBNull)) beData.RemoteArea = reader.GetString(67);
                    if (!(reader[68] is DBNull)) beData.Disability = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.AccessionNo = reader.GetString(69);
                    if (!(reader[70] is DBNull)) beData.SSFNo = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.CITCode = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.CITAcNo = reader.GetString(72);
                    if (!(reader[73] is DBNull)) beData.CIT_Amount = Convert.ToDouble(reader[73]);
                    if (!(reader[74] is DBNull)) beData.CIT_Nominee = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.CIT_RelationShip = reader.GetString(75);
                    if (!(reader[76] is DBNull)) beData.CIT_IDType = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.CIT_IDNo = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.CIT_EntryDate = reader.GetDateTime(78);
                    if (!(reader[79] is DBNull)) beData.BankName = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.BA_AccountName = reader.GetString(80);
                    if (!(reader[81] is DBNull)) beData.BA_AccountNo = reader.GetString(81);
                    if (!(reader[82] is DBNull)) beData.BA_Branch = reader.GetString(82);
                    if (!(reader[83] is DBNull)) beData.BA_IsForPayroll = reader.GetBoolean(83);
                    if (!(reader[84] is DBNull)) beData.LI_InsuranceType = reader.GetInt32(84);
                    if (!(reader[85] is DBNull)) beData.LI_InsuranceCompany = reader.GetString(85);
                    if (!(reader[86] is DBNull)) beData.LI_PolicyName = reader.GetString(86);
                    if (!(reader[87] is DBNull)) beData.LI_PolicyNo = reader.GetString(87);
                    if (!(reader[88] is DBNull)) beData.LI_PolicyAmount = Convert.ToDouble(reader[88]);
                    if (!(reader[89] is DBNull)) beData.LI_PolicyStartDate = reader.GetDateTime(89);
                    if (!(reader[90] is DBNull)) beData.LI_PolicyLastDate = reader.GetDateTime(90);
                    if (!(reader[91] is DBNull)) beData.LI_PremiunAmount = Convert.ToDouble(reader[91]);
                    if (!(reader[92] is DBNull)) beData.LI_PaymentType = reader.GetString(92);
                    if (!(reader[93] is DBNull)) beData.LI_StartMonth = reader.GetInt32(93);
                    if (!(reader[94] is DBNull)) beData.LI_IsDeductFromSalary = reader.GetBoolean(94);
                    if (!(reader[95] is DBNull)) beData.LI_Remarks = reader.GetString(95);
                    if (!(reader[96] is DBNull)) beData.HI_InsurenceType = reader.GetInt32(96);
                    if (!(reader[97] is DBNull)) beData.HI_InsuranceCompany = reader.GetString(97);
                    if (!(reader[98] is DBNull)) beData.HI_PolicyName = reader.GetString(98);
                    if (!(reader[99] is DBNull)) beData.HI_PolicyNo = reader.GetString(99);
                    if (!(reader[100] is DBNull)) beData.HI_PolicyAmount = Convert.ToDouble(reader[100]);
                    if (!(reader[101] is DBNull)) beData.HI_PolicyStartDate = reader.GetDateTime(101);
                    if (!(reader[102] is DBNull)) beData.HI_PolicyLastDate = reader.GetDateTime(102);
                    if (!(reader[103] is DBNull)) beData.HI_PremiumAmount = Convert.ToDouble(reader[103]);
                    if (!(reader[104] is DBNull)) beData.HI_PaymentType = reader.GetInt32(104);
                    if (!(reader[105] is DBNull)) beData.HI_StartMonth = reader.GetInt32(105);
                    if (!(reader[106] is DBNull)) beData.HI_IsDeductFromSalary = reader.GetBoolean(106);
                    if (!(reader[107] is DBNull)) beData.HI_Remarks = reader.GetString(107);
                    if (!(reader[108] is DBNull)) beData.AD_Ledger = reader.GetString(108);
                    if (!(reader[109] is DBNull)) beData.AD_CostCenter = reader.GetString(109);
                    if (!(reader[110] is DBNull)) beData.AD_OTLedger = reader.GetString(110);
                    if (!(reader[111] is DBNull)) beData.S_FirstLevelId = reader.GetInt32(111);
                    if (!(reader[112] is DBNull)) beData.S_SecondLevelId = reader.GetInt32(112);
                    if (!(reader[113] is DBNull)) beData.S_ThirdLevel = reader.GetInt32(113);                   
                    if (!(reader[114] is DBNull)) beData.SignaturePath = reader.GetString(114);
                    if (!(reader[115] is DBNull)) beData.AutoNumber = reader.GetInt32(115);

                    if (!(reader[116] is DBNull)) beData.MotherTonque = reader.GetString(116);
                    if (!(reader[117] is DBNull)) beData.Rank = reader.GetString(117);
                    if (!(reader[118] is DBNull)) beData.Position = reader.GetString(118);
                    if (!(reader[119] is DBNull)) beData.TeacherType = reader.GetString(119);
                    if (!(reader[120] is DBNull)) beData.TeachingLanguage = reader.GetString(120);
                    if (!(reader[121] is DBNull)) beData.LicenseNo = reader.GetString(121);
                    if (!(reader[122] is DBNull)) beData.TrkNo = reader.GetString(122);
                    if (!(reader[123] is DBNull)) beData.PFAccountNo = reader.GetString(123);
                    if (!(reader[124] is DBNull)) beData.CasteId = reader.GetInt32(124);
                    if (!(reader[125] is DBNull)) beData.DOBBS_Str = reader.GetString(125);
                    if (!(reader[126] is DBNull)) beData.EMSId = reader.GetString(126);
                    if (!(reader[127] is DBNull)) beData.IsTeaching = reader.GetBoolean(127);

                    if (!(reader[128] is DBNull)) beData.SubjectTeacherId = reader.GetInt32(128);
                    if (!(reader[129] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(129);
                    if (!(reader[130] is DBNull)) beData.PhysicalDisability = reader.GetString(130);
                    if (!(reader[131] is DBNull)) beData.FatherContactNo = reader.GetString(131);
                    if (!(reader[132] is DBNull)) beData.MotherContactNo = reader.GetString(132);
                    if (!(reader[133] is DBNull)) beData.SpouseContactNo = reader.GetString(133);
                    if (!(reader[134] is DBNull)) beData.OfficeEmailId = reader.GetString(134);
                    try
                    {
                        if (!(reader[135] is DBNull)) beData.SystemUserId = reader.GetInt32(135);
                        if (!(reader[136] is DBNull)) beData.TaxRuleAs = reader.GetInt32(136);

                        //Added by Suresh on Poush 14 2081
                        if (!(reader[137] is DBNull)) beData.LicenceIssuePlace = reader.GetString(137);
                        if (!(reader[138] is DBNull)) beData.SalaryApplicableYearId = reader.GetInt32(138);
                        if (!(reader[139] is DBNull)) beData.SalaryApplicableMonthId = reader.GetInt32(139);
                        if (!(reader[140] is DBNull)) beData.FirstNameNP = reader.GetString(140);
                        if (!(reader[141] is DBNull)) beData.MiddleNameNP = reader.GetString(141);
                        if (!(reader[142] is DBNull)) beData.LastNameNP = reader.GetString(142);
                        if (!(reader[143] is DBNull)) beData.CitizenFrontPhoto = reader.GetString(143);
                        if (!(reader[144] is DBNull)) beData.CitizenBackPhoto = reader.GetString(144);
                        if (!(reader[145] is DBNull)) beData.NIDNo = reader.GetString(145);
                        if (!(reader[146] is DBNull)) beData.NIDPhoto = reader.GetString(146);
                        if (!(reader[147] is DBNull)) beData.isEDJ = Convert.ToBoolean(reader[147]);
                        if (!(reader[148] is DBNull)) beData.EDJ = reader.GetString(148);
                        if (!(reader[149] is DBNull)) beData.Qualification = reader.GetString(149);
                    }
                    catch { }
                    
                }
                reader.NextResult();
                beData.AcademicQualificationColl = new List<BE.Academic.Transaction.EmployeeAcademicQualification>();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeAcademicQualification Qualification = new AcademicLib.BE.Academic.Transaction.EmployeeAcademicQualification();
                    if (!(reader[0] is System.DBNull)) Qualification.DegreeName = reader.GetString(0);
                    if (!(reader[1] is System.DBNull)) Qualification.BoardUniversity = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Qualification.PassedYear = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Qualification.GradePercentage = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Qualification.RegistrationNo = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) Qualification.NameOfInstitution = reader.GetString(5);

                    beData.AcademicQualificationColl.Add(Qualification);
                }
                reader.NextResult();

                beData.WorkExperienceColl = new List<BE.Academic.Transaction.EmployeeWorkExperience>();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeWorkExperience Experience = new AcademicLib.BE.Academic.Transaction.EmployeeWorkExperience();
                      if (!(reader[0] is System.DBNull)) Experience.Organization = reader.GetString(0);
                    if (!(reader[1] is System.DBNull)) Experience.Department = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Experience.JobTitle = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Experience.StartDate = reader.GetDateTime(3);
                    if (!(reader[4] is System.DBNull)) Experience.Remarks = reader.GetString(4);

                    //Added By Suresh on 14 Poush
                    if (!(reader[5] is System.DBNull)) Experience.EndDate = reader.GetDateTime(5);
                    //Ends

                    beData.WorkExperienceColl.Add(Experience);
                }
                reader.NextResult();

                beData.BankList = new List<BE.Academic.Transaction.EmployeeBankAccount>();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeBankAccount Experience = new AcademicLib.BE.Academic.Transaction.EmployeeBankAccount();
                    if (!(reader[0] is System.DBNull)) Experience.BankName = reader.GetString(0);
                    if (!(reader[1] is System.DBNull)) Experience.AccountName = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Experience.AccountNo = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Experience.Branch = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Experience.ForPayRoll = reader.GetBoolean(4);

                    //Added By Suresh on Poush 15 2081
                    if (!(reader[5] is System.DBNull)) Experience.BankId = reader.GetInt32(5);

                    beData.BankList.Add(Experience);
                }
                reader.NextResult();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is System.DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) doc.Extension = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) doc.DocPath = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) doc.Description = reader.GetString(4);
                    beData.AttachmentColl.Add(doc);
                }
                reader.NextResult();
                beData.EmployeeResearchPublicationColl = new BE.Academic.Transaction.EmployeeResearchPublicationCollections();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmployeeResearchPublication Research = new BE.Academic.Transaction.EmployeeResearchPublication();
                    if (!(reader[0] is System.DBNull)) Research.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Research.ResearchTitle = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Research.PublicationDate = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Research.Coauthors = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Research.Abstract_Link = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) Research.PublicationType = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) Research.DOI_ISSNNo = reader.GetString(6);
                    beData.EmployeeResearchPublicationColl.Add(Research);
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
        public ResponeValues DeleteLeftEmp(int UserId, int TranId, int EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_DelEmployeeLeft";
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
        public ResponeValues DeleteById(int UserId, int EntityId, int EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_DelEmployeeById";
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

        public BE.Academic.Transaction.EmployeeAutoComplete getEmployeeByQrCode(int UserId,string qrCode,bool getUserPwd=false)
        {
            BE.Academic.Transaction.EmployeeAutoComplete beData = new BE.Academic.Transaction.EmployeeAutoComplete();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@QrCode", qrCode);
            cmd.CommandText = "usp_GetEmployeeByQrCode";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                     beData = new BE.Academic.Transaction.EmployeeAutoComplete();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Code = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EnrollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MobileNo = reader.GetString(5);

                    if (getUserPwd)
                    {
                        if (!(reader[6] is DBNull)) beData.UserId = reader.GetInt32(6);
                        if (!(reader[7] is DBNull)) beData.UserName = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.Pwd = reader.GetString(8);
                    }
                    
                }
                reader.Close();
                 
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }

        public BE.Academic.Transaction.EmployeeAutoCompleteCollections getAllEmployeeForSelection(int UserId,int For)
        {
            BE.Academic.Transaction.EmployeeAutoCompleteCollections dataColl = new BE.Academic.Transaction.EmployeeAutoCompleteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@For", For);            
            cmd.CommandText = "usp_GetAllEmployeeForSelection";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmployeeAutoComplete beData = new BE.Academic.Transaction.EmployeeAutoComplete();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Code = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EnrollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MobileNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.UserId = reader.GetInt32(6);
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

        public BE.Academic.Transaction.EmployeeAutoCompleteCollections getAllEmployeeAutoComplete(int UserId, string searchBy, string Operator, string searchValue, bool? showLeft = false)
        {
            BE.Academic.Transaction.EmployeeAutoCompleteCollections dataColl = new BE.Academic.Transaction.EmployeeAutoCompleteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ColName", searchBy);
            cmd.Parameters.AddWithValue("@ColValue", searchValue);
            cmd.Parameters.AddWithValue("@Operator", Operator);
            cmd.Parameters.AddWithValue("@showLeft", showLeft);
            cmd.CommandText = "usp_GetAllEmployeeAutoComplete";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmployeeAutoComplete beData = new BE.Academic.Transaction.EmployeeAutoComplete();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Code = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EnrollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MobileNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.UserId = reader.GetInt32(6);
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
        public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getEmployeeUserList(int UserId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl = new BE.Academic.Transaction.EmployeeUserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetEmployeeUserList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeUser beData = new BE.Academic.Transaction.EmployeeUser();
                    beData.SNo = reader.GetInt32(0);
                    beData.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Department = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.UserId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.UserName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Pwd = reader.GetString(10);
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

        public ResponeValues GenerateUser(int UserId, int AsPer,bool CanUpdateUserName, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AsPer", AsPer);            
            cmd.CommandText = "sp_GetAutoCreateEmployeeUser";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CanUpdateUserName", CanUpdateUserName);
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            cmd.Parameters.AddWithValue("@Suffix", Suffix);
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

        public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getAllEmpShortList(int UserId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl = new BE.Academic.Transaction.EmployeeUserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAllEmpShortList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeUser beData = new BE.Academic.Transaction.EmployeeUser();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DepartmentId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.EmailId = reader.GetString(8);
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

        public AcademicLib.BE.Academic.Transaction.EmployeeUserCollections getEmpListForClassTeacher(int UserId,int ClassId,int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, int? SubjectId = null)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl = new BE.Academic.Transaction.EmployeeUserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetEmpListForClassTeacher";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeUser beData = new BE.Academic.Transaction.EmployeeUser();                    
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);                   
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

        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeSummaryList(int UserId,string DepartmentIdColl="")
        {
            AcademicLib.RE.Academic.EmployeeSummaryCollections dataColl = new RE.Academic.EmployeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            if(!string.IsNullOrEmpty(DepartmentIdColl) && DepartmentIdColl.Trim()!="0")
                cmd.Parameters.AddWithValue("@DepartmentIdColl", DepartmentIdColl);

            cmd.CommandText = "usp_GetEmployeeSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeeSummary beData = new RE.Academic.EmployeeSummary();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EnrollNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Address = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Department = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Designation = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Category = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Gender = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.DOB_AD = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.DOB_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TA_FullAddress = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PA_FullAddress = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TA_District = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PA_District = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.FatherName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.MotherName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.SpouseName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.CardNo = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.UserId = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.UserName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Caste = reader.GetString(24);

                    if (!(reader[25] is DBNull)) beData.Nationality = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.OfficeContactNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.PersonalContactNo = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.CitizenShipno = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.Email = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.BloodGroup = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Level = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.MaritalStatus = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PanId = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.Qualification = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.DateofJoining = reader.GetDateTime(35);
                    if (!(reader[36] is DBNull)) beData.MitiOfJoining = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.DateOfConfirmation = reader.GetDateTime(37);
                    if (!(reader[38] is DBNull)) beData.MitiOfConfirmation = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.Age = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.PA_State = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.PA_Municipality = reader.GetString(41);

                    if (!(reader[42] is DBNull)) beData.FatherContactNo = reader.GetString(42);                    
                    if (!(reader[43] is DBNull)) beData.MotherContactNo = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.BankName = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.BankBranch = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.BankAccountName = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.BankAccountNo = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.NameNP = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.IsTeaching = Convert.ToBoolean(reader[49]);
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

        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeLeftSummaryList(int UserId, string DepartmentIdColl = "")
        {
            AcademicLib.RE.Academic.EmployeeSummaryCollections dataColl = new RE.Academic.EmployeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            if (!string.IsNullOrEmpty(DepartmentIdColl) && DepartmentIdColl.Trim() != "0")
                cmd.Parameters.AddWithValue("@DepartmentIdColl", DepartmentIdColl);

            cmd.CommandText = "usp_GetEmployeeLeftSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeeSummary beData = new RE.Academic.EmployeeSummary();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EnrollNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Address = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Department = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Designation = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Category = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Gender = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.DOB_AD = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.DOB_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TA_FullAddress = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PA_FullAddress = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TA_District = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PA_District = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.FatherName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.MotherName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.SpouseName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.CardNo = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.UserId = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.UserName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Caste = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.LeftDate = reader.GetDateTime(25);
                    if (!(reader[26] is DBNull)) beData.LeftMiti = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.LeftRemarks = reader.GetString(27);
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

        public ResponeValues StartOnlineClass(API.Teacher.OnlineClass beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            //TranId,UserId,ClassShiftId,ClassId,SubjectId,PlatformType,UserName,Pwd,Link,Notes,StartDateTime,Duration,IsRunning,EndDateTime
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@PlatformType", beData.PlatformType);
            cmd.Parameters.AddWithValue("@UserName", beData.UserName);
            cmd.Parameters.AddWithValue("@Pwd", beData.Pwd);
            cmd.Parameters.AddWithValue("@Link", beData.Link);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@StartDateTime", beData.StartDateTime);
            cmd.Parameters.AddWithValue("@Duration", beData.Duration);            
            cmd.CommandText = "usp_StartOnlineClass";
            cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 4000);
            cmd.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar,800);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@SectionIdColl", beData.SectionIdColl);
            cmd.Parameters.AddWithValue("@ClassGroupId", beData.ClassGroupId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull) && resVal.IsSuccess)
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[16].Value);

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
        public ResponeValues EndOnlineClass(int UserId,int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 4000);
            cmd.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_EndRunningClass";
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull) )
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

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

        public ResponeValues JoinOnlineClass(int UserId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JoinUserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_OnlineClassAttempt";
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);
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
        public AcademicLib.RE.Academic.RunningClassCollections getRunningClassList(int UserId, int? tranId)
        {
            AcademicLib.RE.Academic.RunningClassCollections dataColl = new RE.Academic.RunningClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@tranId", tranId);
            cmd.CommandText = "usp_GetRunningClassList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.RunningClass beData = new RE.Academic.RunningClass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PlatformType = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ShiftName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SubjectName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StartDateTime_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.StartDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.EndDateTime_AD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.EndDate_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsRunning = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Notes = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.UserName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Pwd = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Link = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TeacherName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Duration = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SectionName = reader.GetString(16);
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

        public AcademicLib.RE.Academic.RunningClassCollections getColleaguesRunningClassList(int UserId)
        {
            AcademicLib.RE.Academic.RunningClassCollections dataColl = new RE.Academic.RunningClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetColleaguesRunningClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.RunningClass beData = new RE.Academic.RunningClass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PlatformType = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ShiftName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SubjectName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StartDateTime_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.StartDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.EndDateTime_AD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.EndDate_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsRunning = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Notes = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.UserName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Pwd = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Link = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TeacherName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Duration = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SectionName = reader.GetString(16);
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

        public AcademicLib.RE.Academic.PassedOnlineClassCollections getPassedClassList(int UserId, DateTime? dateFrom,DateTime? dateTo)
        {
            AcademicLib.RE.Academic.PassedOnlineClassCollections dataColl = new RE.Academic.PassedOnlineClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetPassOnlineClassList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.PassedOnlineClass beData = new RE.Academic.PassedOnlineClass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PlatformType = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.ShiftName = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ClassName = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.SubjectName = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.StartDateTime_AD = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) beData.StartDate_BS = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) beData.EndDateTime_AD = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EndDate_BS = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.IsRunning = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Notes = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.UserName = Convert.ToString(reader[11]);
                    if (!(reader[12] is DBNull)) beData.Pwd = Convert.ToString(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Link = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.TeacherName = Convert.ToString(reader[14]);
                    if (!(reader[15] is DBNull)) beData.ContactNo = Convert.ToString(reader[15]);
                    if (!(reader[16] is DBNull)) beData.Duration = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.NoOfPresent = Convert.ToInt32(reader[18]);
                    if (!(reader[19] is DBNull)) beData.FirstJoinAt = Convert.ToString(reader[19]);
                    if (!(reader[20] is DBNull)) beData.LastJoinAt = Convert.ToString(reader[20]);
                    if (!(reader[21] is DBNull)) beData.PresentMinute = Convert.ToInt32(reader[21]);
                    if (!(reader[22] is DBNull)) beData.TeacherPhotoPath = Convert.ToString(reader[22]);
                    if (!(reader[23] is DBNull)) beData.SectionName = Convert.ToString(reader[23]);
                    beData.ForDate = new DateTime(beData.StartDateTime_AD.Year, beData.StartDateTime_AD.Month, beData.StartDateTime_AD.Day);
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

        public AcademicLib.RE.Academic.OnlineClasssAttendanceCollections getOnlineClassAttendanceById(int UserId, int TranId)
        {
            AcademicLib.RE.Academic.OnlineClasssAttendanceCollections dataColl = new RE.Academic.OnlineClasssAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);            
            cmd.CommandText = "usp_GetPassOnlineClassAttendanceById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.OnlineClasssAttendance beData = new RE.Academic.OnlineClasssAttendance();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.PhotoPath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) beData.StartTime = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.EndTime = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.FirstJoinAt = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.LastJoinAt = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LateMinute = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.AttendanceType = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.Duration = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.UserName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.UserId = Convert.ToInt32(reader[18]);
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
        public AcademicLib.BE.Academic.Transaction.EmployeeLeft getLeftEmployeeById(int UserId,int EmployeeId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeLeft beData = new BE.Academic.Transaction.EmployeeLeft();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetEmployeeLeftById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.EmployeeLeft();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);                    
                }
                reader.NextResult();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Extension = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Data = (byte[])reader[4];
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    beData.AttachmentColl.Add(doc);
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
        public AcademicLib.RE.Academic.LeftEmployeeCollections getLeftEmployeeList(int UserId)
        {
            AcademicLib.RE.Academic.LeftEmployeeCollections dataColl = new RE.Academic.LeftEmployeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetLeftEmployeeList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.LeftEmployee beData = new RE.Academic.LeftEmployee();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.DateOfJoining_AD = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.LeftRemarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DateOfJoining_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.LeftDate_BS = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Code = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.UserId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.ContactNo = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.LeftType = reader.GetString(11);

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int empId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocPath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    dataColl.Find(p1 => p1.EmployeeId == empId).DocumentsColl.Add(doc);
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


        public List<AcademicLib.BE.Academic.Transaction.ImportEmployee> getEmployeeListForEMIS(int UserId)
        {
            List<AcademicLib.BE.Academic.Transaction.ImportEmployee> dataColl = new List<BE.Academic.Transaction.ImportEmployee>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetEmployeeListForEMIS";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ImportEmployee beData = new BE.Academic.Transaction.ImportEmployee();
                    if (!(reader[0] is DBNull)) beData.EmployeeCode = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Gender = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Caste = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Nationality = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DOB_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.CitizenshipNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.CitizenShipIssuePlace = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.MotherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.SpouseName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.CIT_Nominee = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.MotherTonque = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Disability = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.EmailId = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PersonContactNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Level = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Rank = Convert.ToString(reader[17]);
                    if (!(reader[18] is DBNull)) beData.Position = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.TeacherType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.TeachingLanguage = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.LicenseNo = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.LI_PolicyNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.PFAccountNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.TrkNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BA_AccountName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.BA_AccountNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.EMSId = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.EmployeeId = reader.GetInt32(28);
                    dataColl.Add(beData);
                }
                reader.Close();
                return dataColl;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        public AcademicLib.RE.Academic.EmployeeIdCardCollections getEmpListForIdCard(int UserId, string EmpIdColl, DateTime? validFrom, DateTime? validTo,int? DepartmentId,int? DesignationId)
        {
            AcademicLib.RE.Academic.EmployeeIdCardCollections dataColl = new RE.Academic.EmployeeIdCardCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmpIdColl", EmpIdColl);
            cmd.Parameters.AddWithValue("@ValidFrom", validFrom);
            cmd.Parameters.AddWithValue("@ValidTo", validTo);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@DesignationId", DesignationId);
            cmd.CommandText = "usp_GetEmployeeListForIdCard";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeeIdCard beData = new RE.Academic.EmployeeIdCard();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Gender = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Category = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Department = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Designation = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.PA_FullAddress = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TA_FullAddress = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PhotoPath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.BloodGroup = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DOB_AD = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.DOB_BS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.EnrollNo = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.CardNo = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.UserName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ValidFromBS = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ValidToBS = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.ValidFromAD = reader.GetDateTime(21);
                    if (!(reader[22] is DBNull)) beData.ValidToAD = reader.GetDateTime(22);                    
                    if (!(reader[23] is DBNull)) beData.CompName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CompAddress = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.CompPhoneNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.CompFaxNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.CompEmailId = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.CompWebSite = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.CompLogoPath = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.CompImgPath = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.CompBannerPath = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.CompRegdNo = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.CompPanVat = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.MembershipNo = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.LibValidFromBS = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.LibValidToBS = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.LibValidFromAD = reader.GetDateTime(37);
                    if (!(reader[38] is DBNull)) beData.LibValidToAD = reader.GetDateTime(38);                    
                    if (!(reader[39] is DBNull)) beData.BranchName = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.BranchAddress = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.QrCode = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.JoiningDate = reader.GetDateTime(42);
                    if (!(reader[43] is DBNull)) beData.JoiningMiti = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.CitizenshipNo = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.TRKNo = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.Rank = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.PAN = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.ServiceType = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.PersonalContact = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.Level = reader.GetString(50);

                    try
                    {
                        if (!(reader[51] is DBNull)) beData.EmployeeEmailId = reader.GetString(51);
                        if (!(reader[52] is DBNull)) beData.SpouseName = reader.GetString(52);
                        if (!(reader[53] is DBNull)) beData.Pwd = reader.GetString(53);
                        if (!(reader[54] is DBNull)) beData.FirstName = reader.GetString(54);
                        if (!(reader[55] is DBNull)) beData.MiddleName = reader.GetString(55);
                        if (!(reader[56] is DBNull)) beData.LastName = reader.GetString(56);
                        if (!(reader[57] is DBNull)) beData.IsTeaching = Convert.ToBoolean(reader[57]);
                        if (!(reader[58] is DBNull)) beData.Religion = reader.GetString(58);
                        if (!(reader[59] is DBNull)) beData.Nationality = reader.GetString(59);
                        if (!(reader[60] is DBNull)) beData.OfficialContactNo = reader.GetString(60);
                        if (!(reader[61] is DBNull)) beData.MaritalStatus = reader.GetString(61);
                        if (!(reader[62] is DBNull)) beData.GrandFatherName = reader.GetString(62);
                        if (!(reader[63] is DBNull)) beData.DrivingLicenceNo = reader.GetString(63);
                        if (!(reader[64] is DBNull)) beData.PassPortNo = reader.GetString(64);
                        if (!(reader[65] is DBNull)) beData.SSFNo = reader.GetString(65);
                        if (!(reader[66] is DBNull)) beData.CITCode = reader.GetString(66);
                        if (!(reader[67] is DBNull)) beData.EMSID = reader.GetString(67);
                        if (!(reader[68] is DBNull)) beData.FatherContactNo = reader.GetString(68);
                        if (!(reader[69] is DBNull)) beData.MotherContactNo = reader.GetString(69);
                        if (!(reader[70] is DBNull)) beData.SpouseContactNo = reader.GetString(70);
                        if (!(reader[71] is DBNull)) beData.PhysicalDisability = reader.GetString(71);
                        if (!(reader[72] is DBNull)) beData.SubjectTeacher = reader.GetString(72);
                        if (!(reader[73] is DBNull)) beData.ClassTeacherOf = reader.GetString(77);
                        beData.PersonalContactNo = beData.PersonalContact;
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

        public AcademicLib.API.Teacher.ClassTeacherCollections getClassListForClassTeacher(int UserId,int? AcademicYearId,int? BatchId=null,int? SemesterId=null,int? ClassYearId=null)
        {
            AcademicLib.API.Teacher.ClassTeacherCollections dataColl = new API.Teacher.ClassTeacherCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetClassTeacherList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Teacher.ClassTeacher beData = new API.Teacher.ClassTeacher();
                    beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BatchId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SemesterId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ClassYearId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.Batch = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Semester = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ClassYear = reader.GetString(9);

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

        #region "Emp Profile"
        public ResponeValues UpdatePhoto(int UserId, string PhotoPath, int? EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PhotoPath", PhotoPath);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmployeePhoto";
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

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

        public ResponeValues UpdateEmployeePhoto(int UserId, int EmployeeId, string photoPath)
        {

            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@PhotoPath", photoPath);

                cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ;  update tbl_Employee set PhotoPath=@PhotoPath where EmployeeId=@EmployeeId ; ";
                cmd.ExecuteNonQuery();
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Employee Photo Update Success";
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
        public ResponeValues UpdatePersonalInfo(AcademicLib.API.Teacher.PersonalInformation beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();            
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@MaritalStatus", beData.MaritalStatus);
            cmd.Parameters.AddWithValue("@SpouseName", beData.SpouseName);
            cmd.Parameters.AddWithValue("@AnniversaryDate", beData.AnniversaryDate);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@GrandFather", beData.GrandFather);            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmpPersonalInfo";
            cmd.Parameters.AddWithValue("@PersnalContactNo", beData.PersnalContactNo);
            cmd.Parameters.AddWithValue("@OfficeContactNo", beData.OfficeContactNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);

            try
            {
                cmd.ExecuteNonQuery();
                
                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[14].Value);

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

        public ResponeValues UpdatePermanentAddress(AcademicLib.API.Teacher.Emp_PermananetAddress beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PA_Country", beData.PA_Country);
            cmd.Parameters.AddWithValue("@PA_State", beData.PA_State);
            cmd.Parameters.AddWithValue("@PA_Zone", beData.PA_Zone);
            cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
            cmd.Parameters.AddWithValue("@PA_City", beData.PA_City);
            cmd.Parameters.AddWithValue("@PA_Municipality", beData.PA_Municipality);
            cmd.Parameters.AddWithValue("@PA_Ward", beData.PA_Ward);
            cmd.Parameters.AddWithValue("@PA_Street", beData.PA_Street);
            cmd.Parameters.AddWithValue("@PA_HouseNo", beData.PA_HouseNo);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmpPermanentAddress";


            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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

        public ResponeValues UpdateTemporaryAddress(AcademicLib.API.Teacher.Emp_TemporaryAddress beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@TA_Country", beData.TA_Country);
            cmd.Parameters.AddWithValue("@TA_State", beData.TA_State);
            cmd.Parameters.AddWithValue("@TA_Zone", beData.TA_Zone);
            cmd.Parameters.AddWithValue("@TA_District", beData.TA_District);
            cmd.Parameters.AddWithValue("@TA_City", beData.TA_City);
            cmd.Parameters.AddWithValue("@TA_Municipality", beData.TA_Municipality);
            cmd.Parameters.AddWithValue("@TA_Ward", beData.TA_Ward);
            cmd.Parameters.AddWithValue("@TA_Street", beData.TA_Street);
            cmd.Parameters.AddWithValue("@TA_HouseNo", beData.TA_HouseNo);
            cmd.Parameters.AddWithValue("@TA_FullAddress", beData.TA_FullAddress);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmpTemporaryAddress";


            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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

        public ResponeValues UpdateCitizenship(AcademicLib.API.Teacher.CitizenshipDetails beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PanId", beData.PanId);
            cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
            cmd.Parameters.AddWithValue("@CitizenIssueDate", beData.CitizenIssueDate);
            cmd.Parameters.AddWithValue("@CitizenShipIssuePlace", beData.CitizenShipIssuePlace);            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmpCitizenship";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

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

        public ResponeValues UpdateCIT(AcademicLib.API.Teacher.CITDetails beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@SSFNo", beData.SSFNo);
            cmd.Parameters.AddWithValue("@CITCode", beData.CITCode);
            cmd.Parameters.AddWithValue("@CITAcNo", beData.CITAcNo);
            cmd.Parameters.AddWithValue("@CIT_Amount", beData.CIT_Amount);
            cmd.Parameters.AddWithValue("@CIT_Nominee", beData.CIT_Nominee);
            cmd.Parameters.AddWithValue("@CIT_RelationShip", beData.CIT_RelationShip);
            cmd.Parameters.AddWithValue("@CIT_IDType", beData.CIT_IDType);
            cmd.Parameters.AddWithValue("@CIT_IDNo", beData.CIT_IDNo);
            cmd.Parameters.AddWithValue("@CIT_EntryDate", beData.CIT_EntryDate);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateEmpCIT";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

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

        #endregion

        public AcademicLib.API.Admin.Employee admin_EmployeeList(int UserId,int? DepartmentId)
        {
            AcademicLib.API.Admin.Employee dataColl = new API.Admin.Employee();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.CommandText = "usp_admin_EmpList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.EmployeeDetail beData = new API.Admin.EmployeeDetail();
                    beData.SNo = reader.GetInt32(0);
                    beData.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EmpCode = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsTeaching = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.PhotoPath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.DepartmentSNo = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.DesignationSNo = reader.GetInt32(12);
                    dataColl.EmployeeColl.Add(beData);
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
        public AcademicLib.RE.Academic.EmployeeBirthDayCollections getEmpBirthDayList(int UserId, DateTime? dateFrom, DateTime? dateTo)
        {
            AcademicLib.RE.Academic.EmployeeBirthDayCollections dataColl = new RE.Academic.EmployeeBirthDayCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromDate", dateFrom);
            cmd.Parameters.AddWithValue("@ToDate", dateTo);
            cmd.CommandText = "usp_GetEmployeeBirthDay";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeeBirthDay beData = new RE.Academic.EmployeeBirthDay();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Department = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EnrollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AgeYear = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.AgeMonth = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.AgeDay = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DOB_AD = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.DOB_BS = reader.GetString(15);

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
        public ResponeValues UpdateEmployee_Query(int UserId, List<AcademicLib.BE.Academic.Transaction.UpdateEmployeeQuery> dataColl, string query)
        {

            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@EmployeeCode", beData.EmployeeCode);
                    cmd.Parameters.AddWithValue("@EnrollNumber", beData.EnrollNo);
                    cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", beData.LastName); 

                    if(!string.IsNullOrEmpty(beData.Gender))
                        cmd.Parameters.AddWithValue("@Gender", beData.Gender);
                    else
                        cmd.Parameters.AddWithValue("@Gender", DBNull.Value);

                    cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
                    cmd.Parameters.AddWithValue("@PersnalContactNo", beData.PersonContactNo); 
                    cmd.Parameters.AddWithValue("@OfficeContactNo", beData.OfficeContactNo); 
                    cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
                    cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
                    cmd.Parameters.AddWithValue("@PanId", beData.PanId);
                    cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
                    cmd.Parameters.AddWithValue("@PA_FullAddress", beData.Address);
                    cmd.Parameters.AddWithValue("@TA_FullAddress", beData.Temp_Address);
                    cmd.Parameters.AddWithValue("@PA_State", beData.State);
                    cmd.Parameters.AddWithValue("@District", beData.District);
                    cmd.Parameters.AddWithValue("@Municipality", beData.Municipality);
                    cmd.Parameters.AddWithValue("@WardNo", beData.WardNo);
                    cmd.Parameters.AddWithValue("@Religion", beData.Religion);
                    cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
                    cmd.Parameters.AddWithValue("@LicenseNo", beData.LicenseNo);
                    cmd.Parameters.AddWithValue("@TrkNo", beData.TrkNo);
                    cmd.Parameters.AddWithValue("@PFAccountNo", beData.PFAccountNo);
                    cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);

                    if(beData.DepartmentId.HasValue)
                        cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
                    else
                        cmd.Parameters.AddWithValue("@DepartmentId", DBNull.Value);

                    if (beData.DesignationId.HasValue)
                        cmd.Parameters.AddWithValue("@DesignationId", beData.DesignationId);
                    else
                        cmd.Parameters.AddWithValue("@DesignationId", DBNull.Value);

                    if (beData.CategoryId.HasValue)
                        cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
                    else
                        cmd.Parameters.AddWithValue("@CategoryId", DBNull.Value);

                    if (beData.LevelId.HasValue)
                        cmd.Parameters.AddWithValue("@LevelId", beData.LevelId);
                    else
                        cmd.Parameters.AddWithValue("@LevelId", DBNull.Value);

                    if (beData.ServiceTypeId.HasValue)
                        cmd.Parameters.AddWithValue("@ServiceTypeId", beData.ServiceTypeId);
                    else
                        cmd.Parameters.AddWithValue("@ServiceTypeId", DBNull.Value);


                    if (beData.DateOfJoinAD.HasValue)
                        cmd.Parameters.AddWithValue("@DateofJoining", beData.DateOfJoinAD);
                    else
                        cmd.Parameters.AddWithValue("@DateofJoining", DBNull.Value);

                    if (beData.AnniversaryDateAD.HasValue)
                        cmd.Parameters.AddWithValue("@AnniversaryDate", beData.AnniversaryDateAD);
                    else
                        cmd.Parameters.AddWithValue("@AnniversaryDate", DBNull.Value);

                    if (beData.DOB_AD.HasValue)
                        cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
                    else
                        cmd.Parameters.AddWithValue("@DOB_AD", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FatherContactNo", beData.FatherContactNo);
                    cmd.Parameters.AddWithValue("@MotherContactNo", beData.MotherContactNo);
                    cmd.Parameters.AddWithValue("@SpouseContactNo", beData.SpouseContactNo);
                    cmd.Parameters.AddWithValue("@OfficeEmailId", beData.OfficeEmailId);

                    cmd.Parameters.AddWithValue("@BankName", beData.BankName);
                    cmd.Parameters.AddWithValue("@BA_Branch", beData.BA_Branch);

                    cmd.Parameters.AddWithValue("@IsTeaching", beData.IsTeaching);

                    cmd.Parameters.AddWithValue("@MaritalStatus",IsNull(beData.MaritalStatus));

                    cmd.Parameters.AddWithValue("@Qualification", IsDBNull(beData.Qualification));
                    cmd.Parameters.AddWithValue("@NIDNo", IsDBNull(beData.NIDNo));
                    cmd.Parameters.AddWithValue("@FirstNameNP", IsDBNull(beData.FirstNameNP));
                    cmd.Parameters.AddWithValue("@MiddleNameNP", IsDBNull(beData.MiddleNameNP));
                    cmd.Parameters.AddWithValue("@LastNameNP", IsDBNull(beData.LastNameNP));


                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + query;
                    cmd.ExecuteNonQuery();
                     
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Employee Details Update Success";
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
        public ResponeValues UpdateEmployeePhoto_Query(int UserId, List<AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto> dataColl, string query)
        {

            ResponeValues resVal = new ResponeValues();
            List<string> missingEmployees = new List<string>();
            int successCount = 0;
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@AutoNumber", beData.AutoNumber);
                    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                    cmd.Parameters.AddWithValue("@EmployeeCode", beData.EmployeeCode);
                    cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                    cmd.Parameters.AddWithValue("@EnrollNumber", beData.EnrollNo);
                    cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + query;
                    //cmd.ExecuteNonQuery();

                    //Added on 5 Magh
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // detect which identifier was used
                        string idType = "";
                        string idValue = "";

                        if (!string.IsNullOrWhiteSpace(beData.EmployeeCode))
                        {
                            idType = "EmployeeCode";
                            idValue = beData.EmployeeCode;
                        }
                        else if (beData.EmployeeId > 0)
                        {
                            idType = "EmployeeId";
                            idValue = beData.EmployeeId.ToString();
                        }
                        else if (beData.AutoNumber > 0)
                        {
                            idType = "AutoNumber";
                            idValue = beData.AutoNumber.ToString();
                        }
                        else if (beData.CardNo > 0)
                        {
                            idType = "CardNo";
                            idValue = beData.CardNo.ToString();
                        }
                        else if (beData.EnrollNo > 0)
                        {
                            idType = "EnrollNumber";
                            idValue = beData.EnrollNo.ToString();
                        }

                        if (!string.IsNullOrEmpty(idValue))
                            missingEmployees.Add($"{idType}: {idValue}");
                    }
                    else
                    {
                        successCount++;
                    }
                    //Ends
                }
                dal.CommitTransaction();
                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Employee Photo Update Success";
                //Commented above two lines and added following
                if (missingEmployees.Any())
                {
                    string firstItem = missingEmployees[0];
                    int colonIndex = firstItem.IndexOf(':');
                    string idType = firstItem.Substring(0, colonIndex);
                    List<string> idValues = new List<string>();

                    foreach (string item in missingEmployees)
                    {
                        int idx = item.IndexOf(':');
                        if (idx > -1 && idx + 1 < item.Length)
                        {
                            idValues.Add(item.Substring(idx + 1).Trim());
                        }
                    }

                    resVal.IsSuccess = false;
                    resVal.ResponseMSG =
                        "Employee(s) with [" + idType + ": " +
                        string.Join(", ", idValues.Distinct()) +
                        "] do not exist. Please check the file names and try again.";
                }
                else
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = $"Successfully uploaded {successCount} employee photo(s).";
                }
                //Ends here
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


        public ResponeValues ImportBankAccount(int UserId, List<BE.Academic.Transaction.EmployeeBankAccount> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add(new System.Data.DataColumn("EmpCode", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BankName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("AccountName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("AccountNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Branch", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("ForPayRoll", typeof(bool)));
                 
                foreach (var beData in dataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();                     
                    dr["EmpCode"] =IsNull(beData.EmpCode);
                    dr["BankName"] = IsNull(beData.BankName);
                    dr["AccountName"] = IsNull(beData.AccountName);
                    dr["AccountNo"] = IsNull(beData.AccountNo);
                    dr["Branch"] = IsNull(beData.Branch);
                    dr["ForPayRoll"] = beData.ForPayRoll;
                     

                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@BankColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.Parameters.AddWithValue("@UserId", UserId);                 
                cmd.CommandText = "usp_ImportEmpBankAccount";
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (!resVal.IsSuccess)
                    return resVal;

              
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

        public AcademicLib.BE.Academic.Transaction.EmployeeBankAccountCollections getAllEmpBankDetail(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeBankAccountCollections dataColl = new BE.Academic.Transaction.EmployeeBankAccountCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeBankDetail";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeBankAccount beData = new BE.Academic.Transaction.EmployeeBankAccount();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BankName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.AccountNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ForPayRoll = Convert.ToBoolean(reader[3]);
                    if (!(reader[4] is DBNull)) beData.EmpCode = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AccountName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Branch = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Name = reader.GetString(8);
                    dataColl.Add(beData);
                };
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

        public ResponeValues UpdateEmployee_QuickAccess(AcademicLib.BE.Academic.Transaction.Employee beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeCode", beData.EmployeeCode);
            cmd.Parameters.AddWithValue("@EnrollNumber", beData.EnrollNumber);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@TA_FullAddress", beData.TA_FullAddress);
            cmd.Parameters.AddWithValue("@PersnalContactNo", beData.PersnalContactNo);
            cmd.Parameters.AddWithValue("@OfficeContactNo", beData.OfficeContactNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@MaritalStatus", beData.MaritalStatus);
            cmd.Parameters.AddWithValue("@SpouseName", beData.SpouseName);
            cmd.Parameters.AddWithValue("@SpouseContactNo", beData.SpouseContactNo);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@FatherContactNo", beData.FatherContactNo);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@MotherContactNo", beData.MotherContactNo);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.CommandText = "usp_QuickEmployeeUpdate";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@OfficeEmailId",IsNull(beData.OfficeEmailId));
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[24].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[24].Value);

                if (!(cmd.Parameters[25].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[25].Value);

                if (!(cmd.Parameters[26].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[26].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";
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


        public BE.Academic.Transaction.EmpAttachmentCollections getEAttForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            BE.Academic.Transaction.EmpAttachmentCollections dataColl = new BE.Academic.Transaction.EmpAttachmentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeAttachmentForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmpAttachment beData = new BE.Academic.Transaction.EmpAttachment();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DocumentType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Extension = reader.GetString(5);
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

        public BE.Academic.Transaction.EmpComplainCollections getEmpComplainForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            BE.Academic.Transaction.EmpComplainCollections dataColl = new BE.Academic.Transaction.EmpComplainCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeComplainForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmpComplain beData = new BE.Academic.Transaction.EmpComplain();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ComplainDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ActionRemarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ComplainType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ActionTakenBy = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ActionTakenByName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ActionDate = reader.GetDateTime(7);
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


        public BE.Academic.Transaction.EmpLeaveTakenCollections getEmpLeaveTakenForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            BE.Academic.Transaction.EmpLeaveTakenCollections dataColl = new BE.Academic.Transaction.EmpLeaveTakenCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmpLeaveTakenForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.EmpLeaveTaken beData = new BE.Academic.Transaction.EmpLeaveTaken();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RequestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.LeaveType = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DateFrom = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DateTo = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.TotalDays = reader.GetDouble(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ApprovedByUser = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.RequestMiti = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FromMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ToMiti = reader.GetString(10);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {

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



        //Added By Suresh on 16 Poush for Employee Update
        public List<AcademicLib.BE.Academic.Transaction.Employee> getEmployeeForUpdate(int UserId, int? DepartmentId, int? DesignationId, int? CategoryId)
        {
            List<AcademicLib.BE.Academic.Transaction.Employee> dataColl = new List<BE.Academic.Transaction.Employee>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@DesignationId", DesignationId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);           
            cmd.CommandText = "usp_GetEmployeeListForUpdate";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.Employee beData = new BE.Academic.Transaction.Employee();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.MiddleName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.LastName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.EnrollNumber = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.CardNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.EMSId = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.OfficeContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.OfficeEmailId = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.TA_FullAddress = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Nationality = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PanId = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.MaritalStatus = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DepartmentId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.DesignationId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.CategoryId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.LevelId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.DateOfJoining = reader.GetDateTime(18);
                    if (!(reader[19] is DBNull)) beData.TaxRuleAs = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.SalaryApplicableYearId = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.SalaryApplicableMonthId = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.EmployeeCode = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Qualification = reader.GetString(23);
                    dataColl.Add(beData);
                }
                reader.Close();
                return dataColl;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
        }


        public ResponeValues UpdateEmployee(int UserId, List<BE.Academic.Transaction.Employee> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(new System.Data.DataColumn("EmployeeId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("FirstName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MiddleName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("LastName", typeof(string)));               
                dt.Columns.Add(new System.Data.DataColumn("EnrollNumber", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CardNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("EMSId", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Gender", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("OfficeContactNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("OfficeEmailId", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("TA_FullAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Nationality", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PanId", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MaritalStatus", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("DepartmentId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("DesignationId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CategoryId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("LevelId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("DateOfJoining", typeof(DateTime)));
                dt.Columns.Add(new System.Data.DataColumn("TaxRuleAs", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SalaryApplicableYearId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SalaryApplicableMonthId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("EmployeeCode", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Qualification", typeof(string)));
                foreach (var beData in dataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    dr["EmployeeId"] = beData.EmployeeId;
                    dr["FirstName"] = beData.FirstName;
                    dr["MiddleName"] = beData.MiddleName;
                    dr["LastName"] = beData.LastName;
                    dr["EnrollNumber"] = beData.EnrollNumber;
                    dr["CardNo"] = beData.CardNo;                  

                    dr["EMSId"] = beData.EMSId;
                    dr["Gender"] = beData.Gender;
                    dr["OfficeContactNo"] = beData.OfficeContactNo;
                    dr["OfficeEmailId"] = beData.OfficeEmailId;
                    dr["TA_FullAddress"] = beData.TA_FullAddress;
                    dr["Nationality"] = beData.Nationality;
                    dr["PanId"] = beData.PanId;
                    dr["MaritalStatus"] = beData.MaritalStatus;

                    if (beData.DepartmentId.HasValue && beData.DepartmentId.Value > 0)
                        dr["DepartmentId"] = beData.DepartmentId;
                    else
                        dr["DepartmentId"] = DBNull.Value;

                    if (beData.DesignationId.HasValue && beData.DesignationId.Value > 0)
                        dr["DesignationId"] = beData.DesignationId;
                    else
                        dr["DesignationId"] = DBNull.Value;

                    if (beData.CategoryId.HasValue && beData.CategoryId.Value > 0)
                        dr["CategoryId"] = beData.CategoryId;
                    else
                        dr["CategoryId"] = DBNull.Value;


                    if (beData.LevelId.HasValue && beData.LevelId.Value > 0)
                        dr["LevelId"] = beData.LevelId;
                    else
                        dr["LevelId"] = DBNull.Value;

                    if (beData.DateOfJoining.HasValue)
                        dr["DateOfJoining"] = beData.DateOfJoining.Value;
                    else
                        dr["DateOfJoining"] = DBNull.Value;

                    dr["TaxRuleAs"] = beData.TaxRuleAs;

                    if (beData.SalaryApplicableYearId.HasValue && beData.SalaryApplicableYearId.Value > 0)
                        dr["SalaryApplicableYearId"] = beData.SalaryApplicableYearId;
                    else
                        dr["SalaryApplicableYearId"] = DBNull.Value;


                    if (beData.SalaryApplicableMonthId.HasValue && beData.SalaryApplicableMonthId.Value > 0)
                        dr["SalaryApplicableMonthId"] = beData.SalaryApplicableMonthId;
                    else
                        dr["SalaryApplicableMonthId"] = DBNull.Value;

                    dr["EmployeeCode"] = beData.EmployeeCode;

                    dr["Qualification"] =IsDBNull(beData.Qualification);
                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@tmpEmployeeColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@EntityId", 0);
                cmd.CommandText = "usp_UpdateEmployeeFromUE";
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (!resVal.IsSuccess)
                    return resVal;

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

        //Ends

        public AcademicLib.RE.Academic.EmployeePhotoCollections getEmployeePhoto(int UserId, int? DepartmentId)
        {
            AcademicLib.RE.Academic.EmployeePhotoCollections dataColl = new RE.Academic.EmployeePhotoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.CommandText = "usp_GetEmployeePhotoPath";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeePhoto beData = new RE.Academic.EmployeePhoto();
                    beData.EmployeeId = reader.GetInt32(0);
                    beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.DepartmentId = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.PhotoPath = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.EmployeeCode = Convert.ToString(reader[4]);
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

        public AcademicLib.RE.Academic.EmployeePhotoCollections getAllEmployeePhotoDownloadLog(int UserId, int EntityId)
        {
            AcademicLib.RE.Academic.EmployeePhotoCollections dataColl = new AcademicLib.RE.Academic.EmployeePhotoCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllEmployeePhotoDownloadLog";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.EmployeePhoto beData = new AcademicLib.RE.Academic.EmployeePhoto();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.DownloadDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.DepartmentId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.DownloadById = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.DownloadMiti = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DepartmentName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.DownloaderName = reader.GetString(6);
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
