using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class EmployeeCandidateDB
    {
        DataAccessLayer1 dal = null;
        public EmployeeCandidateDB(string hostName, string dbName)
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
            cmd.Parameters.AddWithValue("@EmpCandidateCode", beData.EmployeeCode);
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
            cmd.Parameters.AddWithValue("@EmpCandidateId", beData.EmployeeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateEmpCandidate";
            }
            else
            {
                cmd.Parameters[117].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmpCandidate";
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

            cmd.Parameters.AddWithValue("@EntryDate", beData.EntryDate);
            cmd.Parameters.AddWithValue("@SourceId", beData.SourceId);
            cmd.Parameters.AddWithValue("@Level", beData.Level);
            cmd.Parameters.AddWithValue("@SalaryExpectation", beData.SalaryExpectation);

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
                    //Added By Suresh on 10 Chaitra
                    SaveEmployeeReference(beData.CUserId, resVal.RId, beData.ReferenceColl);
                    //Ends
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


        private void SaveEmployeeAcademicQualification(int UserId, int EmployeeId, List<BE.Academic.Transaction.EmployeeAcademicQualification> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeAcademicQualification beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.DegreeName) && !string.IsNullOrEmpty(beData.BoardUniversity))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
                    cmd.Parameters.AddWithValue("@DegreeName", beData.DegreeName);
                    cmd.Parameters.AddWithValue("@BoardUniversity", beData.BoardUniversity);
                    cmd.Parameters.AddWithValue("@PassedYear", beData.PassedYear);
                    cmd.Parameters.AddWithValue("@GradePercentage", beData.GradePercentage);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmpCandidateAcademicQualification";
                    cmd.ExecuteNonQuery();
                }

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
                    cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
                    cmd.Parameters.AddWithValue("@Organization", beData.Organization);
                    cmd.Parameters.AddWithValue("@Department", beData.Department);
                    cmd.Parameters.AddWithValue("@JobTitle", beData.JobTitle);
                    cmd.Parameters.AddWithValue("@StartDate", beData.StartDate);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

                    //Added By Suresh on 10 Chaitra
                    cmd.Parameters.AddWithValue("@EndDate", beData.EndDate);
                    //Ends
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmpCandidateWorkExperience";
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
                    cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
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
                    cmd.CommandText = "usp_AddEmpCandidateAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        //Added by Suresh on 10 Chaitra 2081
        private void SaveEmployeeReference(int UserId, int EmployeeId, List<BE.Academic.Transaction.EmployeeReference> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || EmployeeId == 0)
                return;

            foreach (BE.Academic.Transaction.EmployeeReference beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.ReferencePerson))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
                    cmd.Parameters.AddWithValue("@ReferencePerson", beData.ReferencePerson);
                    cmd.Parameters.AddWithValue("@Designation", beData.Designation);
                    cmd.Parameters.AddWithValue("@Contact", beData.Contact);
                    cmd.Parameters.AddWithValue("@Email", beData.Email);
                    cmd.Parameters.AddWithValue("@Organisation", beData.Organisation);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddEmpReference";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        //Ends
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
            cmd.CommandText = "usp_GetEmpCandidateAutoRegdNo";
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

        public AcademicLib.BE.Academic.Transaction.EmployeeCollections getAllEmployee(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmployeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllEmpCandidate";
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
            cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmpCandidateById";
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

                    if (!(reader[135] is DBNull)) beData.EntryDate = reader.GetDateTime(135);
                    if (!(reader[136] is DBNull)) beData.SourceId = reader.GetInt32(136);
                    if (!(reader[137] is DBNull)) beData.SalaryExpectation = Convert.ToDouble(reader[137]);
                    if (!(reader[138] is DBNull)) beData.Level = reader.GetString(138);

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
                    //Added By Suresh on 10 Chaitra
                    if (!(reader[5] is System.DBNull)) Experience.EndDate = reader.GetDateTime(5);
                    beData.WorkExperienceColl.Add(Experience);
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
                //Added By Suresh on Chaitra 10 starts
                reader.NextResult();
                beData.ReferenceColl = new List<BE.Academic.Transaction.EmployeeReference>();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmployeeReference Ref = new AcademicLib.BE.Academic.Transaction.EmployeeReference();
                    if (!(reader[0] is System.DBNull)) Ref.ReferencePerson = reader.GetString(0);
                    if (!(reader[1] is System.DBNull)) Ref.Designation = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Ref.Contact = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Ref.Email = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Ref.Organisation = reader.GetString(4);
                    beData.ReferenceColl.Add(Ref);
                }
                //Ends
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
            cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
            cmd.CommandText = "usp_DelEmpCandidateLeft";
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
            cmd.Parameters.AddWithValue("@EmpCandidateId", EmployeeId);
            cmd.CommandText = "usp_DelEmpCandidateById";
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

        public AcademicLib.RE.Academic.EmployeeSummaryCollections getEmployeeSummaryList(int UserId, string DepartmentIdColl = "")
        {
            AcademicLib.RE.Academic.EmployeeSummaryCollections dataColl = new RE.Academic.EmployeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            if (!string.IsNullOrEmpty(DepartmentIdColl) && DepartmentIdColl.Trim() != "0")
                cmd.Parameters.AddWithValue("@DepartmentIdColl", DepartmentIdColl);

            cmd.CommandText = "usp_GetEmpCandidateSummary";
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
