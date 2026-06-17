using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class StudentDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public StudentDB(string hostName, string dbName)
        {
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
                cmd.CommandText = "usp_UpdateStudent";
            }
            else
            {
                cmd.Parameters[71].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudent";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[72].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[73].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[74].Direction = System.Data.ParameterDirection.Output;
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
            cmd.Parameters.AddWithValue("@RegistrationId", beData.RegistrationId);
            cmd.Parameters.AddWithValue("@ReferralCode", beData.ReferralCode);
            cmd.Parameters.AddWithValue("@IsImport", beData.IsImport);
            cmd.Parameters.AddWithValue("@DonorLedgerId", beData.DonorLedgerId);

            cmd.Parameters.AddWithValue("@PassOutClassId", beData.PassOutClassId);
            cmd.Parameters.AddWithValue("@PassOutGPA", beData.PassOutGPA);
            cmd.Parameters.AddWithValue("@PassOutGrade", beData.PassOutGrade);
            cmd.Parameters.AddWithValue("@PassOutRemarks", beData.PassOutRemarks);
            cmd.Parameters.AddWithValue("@PassOutSymbolNo", beData.PassOutSymbolNo);
            cmd.Parameters.AddWithValue("@FirstNameNP", beData.FirstNameNP);
            cmd.Parameters.AddWithValue("@MiddleNameNP", beData.MiddleNameNP);
            cmd.Parameters.AddWithValue("@LastNameNP", beData.LastNameNP);
            cmd.Parameters.AddWithValue("@CitizenFrontPhoto", beData.CitizenFrontPhoto);
            cmd.Parameters.AddWithValue("@CitizenBackPhoto", beData.CitizenBackPhoto);
            cmd.Parameters.AddWithValue("@NIDNo", beData.NIDNo);
            cmd.Parameters.AddWithValue("@NIDPhoto", beData.NIDPhoto);
            cmd.Parameters.AddWithValue("@isEDJ", beData.isEDJ);
            cmd.Parameters.AddWithValue("@EDJ", beData.EDJ);
            cmd.Parameters.AddWithValue("@PA_HouseNo", beData.PA_HouseNo);
            cmd.Parameters.AddWithValue("@CA_HouseNo", beData.CA_HouseNo);
            cmd.Parameters.AddWithValue("@CA_Country", beData.CA_Country);
            if (!isModify)
            {
                cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
                cmd.Parameters.AddWithValue("@PaidUptoMonth", beData.PaidUptoMonth);
            }

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

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if(resVal.RId>0 && resVal.IsSuccess)
                {
                    SaveAcademicDetails(beData.CUserId, resVal.RId, beData.AcademicDetailsColl);
                    SaveSiblingDetails(beData.CUserId, resVal.RId, beData.SiblingDetailColl);
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);

                    cmd.Parameters.Clear();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYear);

                    cmd.CommandText = "usp_ReInsertSubjectMappingStudentWise";
                    cmd.ExecuteNonQuery();
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
                    cmd.CommandText = "usp_AddStudentAttachDocument";
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
                cmd.Parameters.AddWithValue("@Level", beData.Level);
                cmd.Parameters.AddWithValue("@University", beData.University);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddStudentAcademicDetails";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveSiblingDetails(int UserId, int StudentId, List<AcademicLib.BE.Academic.Transaction.SiblingDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || StudentId == 0)
                return;

            foreach (AcademicLib.BE.Academic.Transaction.SiblingDetails beData in beDataColl)
            {
                if (beData.SectionId.HasValue && beData.SectionId.Value == 0)
                    beData.SectionId = null;

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@ForStudentId", beData.ForStudentId);
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@Relation", beData.Relation);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);             
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddStudentSiblingDetails";
                cmd.ExecuteNonQuery();
            }

        }

        public ResponeValues UpdateClassWiseStudent(int UserId,List<BE.Academic.Transaction.Student> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add(new System.Data.DataColumn("StudentId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("RegNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BoardRegNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("FirstName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MiddleName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("LastName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Gender", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("RollNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("DOB_AD", typeof(DateTime))); 
                dt.Columns.Add(new System.Data.DataColumn("FatherName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("F_ContactNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MotherName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("M_Contact", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_FullAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MediumId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("HouseNameId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("Weigth", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Height", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Aim", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PhysicalDisability", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BloodGroup", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("IsNewStudent", typeof(bool)));
                dt.Columns.Add(new System.Data.DataColumn("SectionId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CasteId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CardNo", typeof(long)));
                dt.Columns.Add(new System.Data.DataColumn("EMSId", typeof(string)));

                dt.Columns.Add(new System.Data.DataColumn("StudentTypeId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("LedgerPanaNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CitizenshipNo", typeof(string)));

                foreach (var beData in dataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();
                     
                    dr["StudentId"] = beData.StudentId;
                    dr["RegNo"] = beData.RegNo.Trim();
                    dr["FirstName"] = IsNull(beData.FirstName).Trim();
                    dr["MiddleName"] = IsNull(beData.MiddleName).Trim();
                    dr["LastName"] = IsNull(beData.LastName).Trim();
                    dr["Gender"] = beData.Gender;

                    if (beData.DOB_AD.HasValue)
                        dr["DOB_AD"] = beData.DOB_AD.Value;
                    else
                        dr["DOB_AD"] = DBNull.Value;

                    dr["BloodGroup"] =IsNull(beData.BloodGroup).Trim();
                    dr["Height"] = IsNull(beData.Height).Trim();
                    dr["Weigth"] = IsNull(beData.Weigth).Trim();
                    dr["PhysicalDisability"] = IsNull(beData.PhysicalDisability).Trim();
                    dr["Aim"] = IsNull(beData.Aim).Trim();

                    if(beData.SectionId.HasValue && beData.SectionId.Value>0)
                        dr["SectionId"] = beData.SectionId;
                    else
                        dr["SectionId"] = DBNull.Value;

                    dr["RollNo"] = beData.RollNo;
                    dr["IsNewStudent"] = beData.IsNewStudent;

                    if (beData.MediumId.HasValue && beData.MediumId.Value>0)
                        dr["MediumId"] = beData.MediumId;
                    else
                        dr["MediumId"] = DBNull.Value;

                    if (beData.HouseNameId.HasValue && beData.HouseNameId.Value>0)
                        dr["HouseNameId"] = beData.HouseNameId;
                    else
                        dr["HouseNameId"] = DBNull.Value;

                    dr["BoardRegNo"] = IsNull(beData.BoardRegNo).Trim();
                    dr["FatherName"] = IsNull(beData.FatherName).Trim();
                    dr["F_ContactNo"] = IsNull(beData.F_ContactNo).Trim();
                    dr["MotherName"] = IsNull(beData.MotherName).Trim();
                    dr["M_Contact"] = IsNull(beData.M_Contact).Trim();
                    dr["PA_FullAddress"] = IsNull(beData.PA_FullAddress).Trim();

                    if(beData.CasteId.HasValue && beData.CasteId.Value>0)
                        dr["CasteId"] = beData.CasteId;
                    else
                        dr["CasteId"] = DBNull.Value;

                    dr["CardNo"] = beData.CardNo;
                    dr["EMSId"] = IsNull(beData.EMSId).Trim();

                    if (beData.StudentTypeId.HasValue && beData.StudentTypeId.Value > 0)
                        dr["StudentTypeId"] = beData.StudentTypeId;
                    else
                        dr["StudentTypeId"] = DBNull.Value;

                    dr["LedgerPanaNo"] = IsNull(beData.LedgerPanaNo).Trim();

                    if(!string.IsNullOrEmpty(beData.CitizenshipNo))
                        dr["CitizenshipNo"] = IsNull(beData.CitizenshipNo).Trim();
                    else
                        dr["CitizenshipNo"] = "";

                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@tmpStudentColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@EntityId", 0);
                cmd.CommandText = "usp_UpdateStudetFromUS";
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

                //foreach (var beData in dataColl)
                //{
                //    cmd.Parameters.Clear();
                //    cmd.Parameters.AddWithValue("@RegNo", beData.RegNo);
                //    cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
                //    cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
                //    cmd.Parameters.AddWithValue("@LastName", beData.LastName);
                //    cmd.Parameters.AddWithValue("@Gender", beData.Gender);
                //    cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
                //    cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
                //    cmd.Parameters.AddWithValue("@Height", beData.Height);
                //    cmd.Parameters.AddWithValue("@Weigth", beData.Weigth);
                //    cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);
                //    cmd.Parameters.AddWithValue("@Aim", beData.Aim);
                //    cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                //    cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);
                //    cmd.Parameters.AddWithValue("@IsNewStudent", beData.IsNewStudent);
                //    cmd.Parameters.AddWithValue("@HouseNameId", beData.HouseNameId);
                //    cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
                //    cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
                //    cmd.Parameters.AddWithValue("@F_ContactNo", beData.F_ContactNo);
                //    cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
                //    cmd.Parameters.AddWithValue("@M_Contact", beData.M_Contact);
                //    cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
                //    cmd.Parameters.AddWithValue("@UserId", UserId);
                //    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                //    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                //    cmd.CommandText = "usp_UpdateStudetFromUS";
                //    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                //    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                //    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                //    cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
                //    cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
                //    cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;
                //    cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
                //    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                //    cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);
                //    cmd.ExecuteNonQuery();

                //    if (!(cmd.Parameters[24].Value is DBNull))
                //        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[24].Value);

                //    if (!(cmd.Parameters[25].Value is DBNull))
                //        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[25].Value);

                //    if (!(cmd.Parameters[26].Value is DBNull))
                //        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[26].Value);

                //    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                //        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                //    if (!resVal.IsSuccess)
                //        return resVal;
                //}

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

        public ResponeValues UpdateBoardRegdNo(RE.Academic.StudentDetailsForLeftCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                foreach (var beData in dataColl)
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", beData.UserId);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegdNo);
                    cmd.CommandText = "usp_UpdateBoardRegdNo";
                    //cmd.CommandText = "update tbl_Student set BoardRegNo=@BoardRegNo  where StudentId=@StudentId";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Board Reg.No Updated";
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

        public ResponeValues UpdateStudentLeft(int UserId,int AcademicYearId,RE.Academic.StudentDetailsForLeftCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var first = dataColl.First();

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", first.ClassId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                if (first.SectionId.HasValue && first.SectionId>0)
                    cmd.Parameters.AddWithValue("@SectionId", first.SectionId);
                else
                    cmd.Parameters.AddWithValue("@SectionId", DBNull.Value);

                if (first.SemesterId.HasValue && first.SemesterId>0)
                    cmd.Parameters.AddWithValue("@SemesterId", first.SemesterId);
                else
                    cmd.Parameters.AddWithValue("@SemesterId", DBNull.Value);
                
                if (first.ClassYearId.HasValue && first.ClassYearId>0)
                    cmd.Parameters.AddWithValue("@ClassYearId", first.ClassYearId);
                else
                    cmd.Parameters.AddWithValue("@ClassYearId", DBNull.Value);
                 
                cmd.CommandText = "usp_DelStudentLeft";                
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add(new System.Data.DataColumn("StudentId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("LeftDate", typeof(DateTime)));
                dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("StatusId", typeof(int)));

                dt.Columns.Add(new System.Data.DataColumn("ClassId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SectionId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("BatchId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SemesterId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("ClassYearId", typeof(int)));

                foreach (var beData in dataColl)
                {
                    if (beData.StudentId > 0)
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        dr["StudentId"] = beData.StudentId;

                        if (beData.LeftDate_AD.HasValue)
                            dr["LeftDate"] = beData.LeftDate_AD;
                        else
                            dr["LeftDate"] = DBNull.Value;

                        dr["Remarks"] = beData.LeftRemarks;

                        dr["StatusId"] =IsDBNull(beData.StatusId);

                        dr["ClassId"] = IsDBNull(beData.ClassId);
                        dr["SectionId"] = IsDBNull(beData.SectionId);
                        dr["BatchId"] = IsDBNull(beData.BatchId);
                        dr["SemesterId"] = IsDBNull(beData.SemesterId);
                        dr["ClassYearId"] = IsDBNull(beData.ClassYearId) ;

                        dt.Rows.Add(dr);
                    }
                   
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@tmpStudentColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.CommandText = "usp_AddStudentLeft";                
                cmd.ExecuteNonQuery();

                //foreach (var beData in dataColl)
                //{
                //    if (beData.StudentId > 0)
                //    {
                //        cmd.Parameters.Clear();
                //        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                //        cmd.Parameters.AddWithValue("@LeftDate", beData.LeftDate_AD);
                //        cmd.Parameters.AddWithValue("@Remarks", beData.LeftRemarks);
                //        cmd.Parameters.AddWithValue("@UserId", UserId);
                //        //cmd.CommandText = "insert into tbl_StudentLeft(StudentId,LeftDate,Remarks,CreateBy) values(@StudentId,@LeftDate,@Remarks,@CreateBy)";
                //        cmd.CommandText = "usp_AddStudentLeft";
                //        cmd.ExecuteNonQuery();
                //    }                    
                //}
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Left Data Updated";
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

        public ResponeValues UpdateEnrollCardNo(int UserId,BE.Academic.Transaction.StudentListCollections dataColl)
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
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@EnrollNo", beData.EnrollNo);
                    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "update tbl_Student set EnrollNo=@EnrollNo,CardNo=@CardNo where StudentId=@StudentId";
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
    
        public BE.Academic.Transaction.Student getStudentById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Transaction.Student beData = new BE.Academic.Transaction.Student();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentById";
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
                    if (!(reader[69] is DBNull)) beData.CardNo = Convert.ToInt64(reader[69]);
                    if (!(reader[70] is DBNull)) beData.EMSId = " "+reader.GetString(70);
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

                    try
                    {
                        if (!(reader[84] is DBNull)) beData.ReferralCode = reader.GetString(84);
                        if (!(reader[85] is DBNull)) beData.DonorLedgerId = reader.GetInt32(85);
                        if (!(reader[86] is DBNull)) beData.PassOutClassId = reader.GetInt32(86);
                        if (!(reader[87] is DBNull)) beData.PassOutSymbolNo = reader.GetString(87);
                        if (!(reader[88] is DBNull)) beData.PassOutGPA = Convert.ToDouble(reader[88]);
                        if (!(reader[89] is DBNull)) beData.PassOutGrade = reader.GetString(89);
                        if (!(reader[90] is DBNull)) beData.PassOutRemarks = reader.GetString(90);
                        if (!(reader[91] is DBNull)) beData.BranchId = reader.GetInt32(91);
                        if (!(reader[92] is DBNull)) beData.FirstNameNP = reader.GetString(92);
                        if (!(reader[93] is DBNull)) beData.MiddleNameNP = reader.GetString(93);
                        if (!(reader[94] is DBNull)) beData.LastNameNP = reader.GetString(94);
                        if (!(reader[95] is DBNull)) beData.CitizenFrontPhoto = reader.GetString(95);
                        if (!(reader[96] is DBNull)) beData.CitizenBackPhoto = reader.GetString(96);
                        if (!(reader[97] is DBNull)) beData.NIDNo = reader.GetString(97);
                        if (!(reader[98] is DBNull)) beData.NIDPhoto = reader.GetString(98);
                        if (!(reader[99] is DBNull)) beData.isEDJ = Convert.ToBoolean(reader[99]);
                        if (!(reader[100] is DBNull)) beData.EDJ = reader.GetString(100);
                        if (!(reader[101] is DBNull)) beData.PA_HouseNo = reader.GetString(101);
                        if (!(reader[102] is DBNull)) beData.CA_HouseNo = reader.GetString(102);
                        if (!(reader[103] is DBNull)) beData.CA_Country = reader.GetString(103);
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
                    if (!(reader[10] is DBNull)) det.Level = reader.GetString(10);
                    if (!(reader[11] is DBNull)) det.University = reader.GetString(11);
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
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.SiblingDetails sbDet = new BE.Academic.Transaction.SiblingDetails();
                    if (!(reader[0] is DBNull)) sbDet.ForStudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) sbDet.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) sbDet.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) sbDet.Relation = reader.GetString(3);
                    if (!(reader[4] is DBNull)) sbDet.Remarks = reader.GetString(4);

                    beData.SiblingDetailColl.Add(sbDet);
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
            cmd.CommandText = "usp_DelStudentById";
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

        public ResponeValues UpdateStudent_Query(int UserId,List<AcademicLib.BE.Academic.Transaction.UpdateStudentQuery> dataColl, string query)
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
                    
                    cmd.Parameters.AddWithValue("@RegNo", beData.RegNo);
                    cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", beData.LastName);
                    cmd.Parameters.AddWithValue("@Gender", beData.GenderId);
                    if (beData.DOBAD.HasValue)
                        cmd.Parameters.AddWithValue("@DOB_AD", beData.DOBAD);
                    else
                        cmd.Parameters.AddWithValue("@DOB_AD", DBNull.Value);

                    if (beData.CasteId.HasValue)
                        cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
                    else
                        cmd.Parameters.AddWithValue("@CasteId", DBNull.Value);


                    cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
                    cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
                    cmd.Parameters.AddWithValue("@Email", beData.EmailId);

                    if (beData.ClassId.HasValue)
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    else
                        cmd.Parameters.AddWithValue("@ClassId", DBNull.Value);


                    if (beData.SectionId.HasValue)
                        cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                    else
                        cmd.Parameters.AddWithValue("@SectionId", DBNull.Value);


                    cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);

                    if (beData.StudentTypeId.HasValue)
                        cmd.Parameters.AddWithValue("@StudentTypeId", beData.StudentTypeId);
                    else
                        cmd.Parameters.AddWithValue("@StudentTypeId", DBNull.Value);


                    if (beData.MediumId.HasValue)
                        cmd.Parameters.AddWithValue("@MediumId", beData.MediumId);
                    else
                        cmd.Parameters.AddWithValue("@MediumId", DBNull.Value);

                    cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegdNo);
                    cmd.Parameters.AddWithValue("@EnrollNo", beData.EnrollNo);
                    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                    cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
                    cmd.Parameters.AddWithValue("@F_ContactNo", beData.FatherContactNo);
                    cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
                    cmd.Parameters.AddWithValue("@M_Contact", beData.MotherContactNo);
                    cmd.Parameters.AddWithValue("@GuardianName", beData.GuardianName);
                    cmd.Parameters.AddWithValue("@G_Address", beData.GuardianAddress);
                    cmd.Parameters.AddWithValue("@G_ContactNo", beData.GuardianContactNo);
                    cmd.Parameters.AddWithValue("@G_Relation", beData.GuardianRelation);
                    cmd.Parameters.AddWithValue("@PA_FullAddress", beData.P_Address);
                    cmd.Parameters.AddWithValue("@CA_FullAddress", beData.T_Address);
                    cmd.Parameters.AddWithValue("@IsNewStudent", beData.IsNewStudent);
                    cmd.Parameters.AddWithValue("@LedgerPanaNo", beData.LedgerPanaNo);

                    cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);

                    if (beData.AcademicYearId.HasValue)
                        cmd.Parameters.AddWithValue("@AcademicYear", beData.AcademicYearId);
                    else
                        cmd.Parameters.AddWithValue("@AcademicYear", DBNull.Value);


                    if (beData.HouseNameId.HasValue)
                        cmd.Parameters.AddWithValue("@HouseNameId", beData.HouseNameId);
                    else
                        cmd.Parameters.AddWithValue("@HouseNameId", DBNull.Value);

                    if (beData.HouseDressId.HasValue)
                        cmd.Parameters.AddWithValue("@HouseDressId", beData.HouseDressId);
                    else
                        cmd.Parameters.AddWithValue("@HouseDressId", DBNull.Value);


                    if (beData.SemesterId.HasValue)
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    else
                        cmd.Parameters.AddWithValue("@SemesterId", DBNull.Value);

                    if (beData.ClassYearId.HasValue)
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    else
                        cmd.Parameters.AddWithValue("@ClassYearId", DBNull.Value);

                    if (beData.BatchId.HasValue)
                        cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                    else
                        cmd.Parameters.AddWithValue("@BatchId", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Religion", beData.Religion);
                    cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);

                    cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
                    cmd.Parameters.AddWithValue("@CA_Province", beData.CA_Province);
                    cmd.Parameters.AddWithValue("@PA_Province", beData.PA_Province);
                    cmd.Parameters.AddWithValue("@CA_District", beData.CA_District);
                    cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
                    cmd.Parameters.AddWithValue("@PA_LocalLevel", beData.PA_LocalLevel);
                    cmd.Parameters.AddWithValue("@CA_LocalLevel", beData.CA_LocalLevel);

                    cmd.Parameters.AddWithValue("@BirthCertificateNo", beData.BirthCertificateNo);
                    cmd.Parameters.AddWithValue("@M_Email", beData.M_Email);
                    cmd.Parameters.AddWithValue("@F_Email", beData.F_Email);
                    cmd.Parameters.AddWithValue("@F_Profession", beData.F_Profession);
                    cmd.Parameters.AddWithValue("@M_Profession", beData.M_Profession);

                    if (beData.AdmitDate.HasValue)
                        cmd.Parameters.AddWithValue("@AdmitDate", beData.AdmitDate);
                    else
                        cmd.Parameters.AddWithValue("@AdmitDate", DBNull.Value);


                    cmd.Parameters.AddWithValue("@MotherTongue", beData.MotherTongue);
                    cmd.Parameters.AddWithValue("@Height", beData.Height);    
                    cmd.Parameters.AddWithValue("@Weigth", beData.Weigth);    
                    cmd.Parameters.AddWithValue("@IsPhysicalDisability", beData.IsPhysicalDisability); 
                    cmd.Parameters.AddWithValue("@Aim", beData.Aim); 
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks); 
                    cmd.Parameters.AddWithValue("@EnquiryNo", beData.EnquiryNo);

                    if (beData.TransportPointId.HasValue)
                        cmd.Parameters.AddWithValue("@TransportPointId", beData.TransportPointId);
                    else
                        cmd.Parameters.AddWithValue("@TransportPointId", DBNull.Value);

                    if (beData.BoardersTypeId.HasValue)
                        cmd.Parameters.AddWithValue("@BoardersTypeId", beData.BoardersTypeId);
                    else
                        cmd.Parameters.AddWithValue("@BoardersTypeId", DBNull.Value);

                    if (beData.BoardId.HasValue)
                        cmd.Parameters.AddWithValue("@BoardId", beData.BoardId);
                    else
                        cmd.Parameters.AddWithValue("@BoardId", DBNull.Value);

                    if (beData.ClassId_FirstId.HasValue)
                        cmd.Parameters.AddWithValue("@ClassId_First", beData.ClassId_FirstId);              
                    else
                        cmd.Parameters.AddWithValue("@ClassId_First", DBNull.Value);

                    cmd.Parameters.AddWithValue("@F_AnnualIncome", beData.F_AnnualIncome);             
                    cmd.Parameters.AddWithValue("@M_AnnualIncome", beData.M_AnnualIncome); 
                    cmd.Parameters.AddWithValue("@G_Profesion", beData.G_Profesion); 
                    cmd.Parameters.AddWithValue("@G_Email", beData.G_Email);


                    cmd.Parameters.AddWithValue("@PassOutClassId",IsDBNull(beData.PassOutClass));
                    cmd.Parameters.AddWithValue("@PassOutSymbolNo", IsDBNull(beData.PassOutSymbolNo));
                    cmd.Parameters.AddWithValue("@PassOutGPA", IsDBNull(beData.PassOutGPA));
                    cmd.Parameters.AddWithValue("@PassOutGrade", IsDBNull(beData.PassOutGrade));
                    cmd.Parameters.AddWithValue("@PassOutRemarks", IsDBNull(beData.PassOutRemarks));
                    cmd.Parameters.AddWithValue("@NIDNo", IsDBNull(beData.NIDNo));
                    cmd.Parameters.AddWithValue("@FirstNameNP", IsDBNull(beData.FirstNameNP));
                    cmd.Parameters.AddWithValue("@MiddleNameNP", IsDBNull(beData.MiddleNameNP));
                    cmd.Parameters.AddWithValue("@LastNameNP", IsDBNull(beData.LastNameNP));
                    

                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value="+UserId.ToString()+" ; "+ query;
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Details Update Success";
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

        public ResponeValues UpdateStudentPhoto(int UserId, int StudentId,string photoPath)
        {

            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@UserId", UserId);                
                cmd.Parameters.AddWithValue("@PhotoPath", photoPath);

                cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ;  update tbl_Student set PhotoPath=@PhotoPath where StudentId=@StudentId ; " ;
                cmd.ExecuteNonQuery();
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Photo Update Success";
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

        public ResponeValues UpdateStudentPhoto_Query(int UserId, List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto> dataColl, string query)
        {

            ResponeValues resVal = new ResponeValues();
            //Added on Falgun 5
            List<string> missingStudents = new List<string>();
            int successCount = 0;
            //Ends
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@RegNo", beData.RegNo);
                    cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegdNo);
                    cmd.Parameters.AddWithValue("@CardNo", beData.CardNo);
                    cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);
                    cmd.Parameters.AddWithValue("@EnrollNo", beData.EnrollNo);
                    cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
                    cmd.Parameters.AddWithValue("@AutoNumber", beData.AutoNumber);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);

                    cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);

                    if (beData.ClassId.HasValue)
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    else
                        cmd.Parameters.AddWithValue("@ClassId", DBNull.Value);

                    if (beData.SectionId.HasValue)
                        cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                    else
                        cmd.Parameters.AddWithValue("@SectionId", DBNull.Value);

                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + query;
                    //cmd.ExecuteNonQuery();
                    //Commented above line and added below on 5 Falgun
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // detect identifier used
                        string idType = "";
                        string idValue = "";

                        if (!string.IsNullOrWhiteSpace(beData.RegNo))
                        {
                            idType = "RegNo";
                            idValue = beData.RegNo;
                        }
                        else if (!string.IsNullOrWhiteSpace(beData.BoardRegdNo))
                        {
                            idType = "BoardRegdNo";
                            idValue = beData.BoardRegdNo;
                        }
                        else if (beData.EnrollNo > 0)
                        {
                            idType = "EnrollNo";
                            idValue = beData.EnrollNo.ToString();
                        }
                        else if (beData.CardNo > 0)
                        {
                            idType = "CardNo";
                            idValue = beData.CardNo.ToString();
                        }
                        else if (!string.IsNullOrWhiteSpace(beData.EMSId))
                        {
                            idType = "EMSId";
                            idValue = beData.EMSId;
                        }
                        else if (beData.AutoNumber > 0)
                        {
                            idType = "AutoNumber";
                            idValue = beData.AutoNumber.ToString();
                        }
                        else if (beData.StudentId > 0)
                        {
                            idType = "StudentId";
                            idValue = beData.StudentId.ToString();
                        }
                        else if (beData.RollNo > 0 && beData.ClassId.HasValue && beData.SectionId.HasValue)
                        {
                            idType = "RollNo";
                            idValue = $"{beData.RollNo}";
                        }

                        if (!string.IsNullOrEmpty(idValue))
                            missingStudents.Add($"{idType}: {idValue}");
                    }
                    else
                    {
                        successCount++;
                    }
                    //Ends
                }
                dal.CommitTransaction();
                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Student Photo Update Success";
                //Commented above 2 line and added following codes
                if (missingStudents.Any())
                {
                    string firstItem = missingStudents[0];
                    int colonIndex = firstItem.IndexOf(':');
                    string idType = firstItem.Substring(0, colonIndex);
                    List<string> idValues = new List<string>();

                    foreach (string item in missingStudents)
                    {
                        int idx = item.IndexOf(':');
                        if (idx > -1 && idx + 1 < item.Length)
                        {
                            idValues.Add(item.Substring(idx + 1).Trim());
                        }
                    }

                    resVal.IsSuccess = false;
                    resVal.ResponseMSG =
                        "Student(s) with [" + idType + ": " +
                        string.Join(", ", idValues.Distinct()) +
                        "] do not exist. Please check the file names and try again.";
                }
                else
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = $"Successfully uploaded {successCount} student photo(s).";
                }
                //Ends
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


        public BE.Academic.Transaction.StudentAutoComplete getStudentByQrCode(int UserId, string qrCode, bool getUserPwd = false)
        {
            BE.Academic.Transaction.StudentAutoComplete beData = new BE.Academic.Transaction.StudentAutoComplete();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@QrCode", qrCode);
            cmd.CommandText = "usp_GetStudentByQrCode";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.StudentAutoComplete();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.UserId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.UserName = reader.GetString(10);       
                    
                    if(getUserPwd)
                        if (!(reader[11] is DBNull)) beData.Pwd = reader.GetString(11);

                    try
                    {
                        if (!(reader[12] is DBNull)) beData.Batch = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.Faculty = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);                        
                    }
                    catch { }
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

        public BE.Academic.Transaction.StudentAutoCompleteCollections getAllStudentAutoComplete(int UserId,int AcademicYearId,  string searchBy,string Operator,string searchValue,bool showLeft,int? classId=null,int? sectionId=null)
        {
            BE.Academic.Transaction.StudentAutoCompleteCollections dataColl = new BE.Academic.Transaction.StudentAutoCompleteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ColName", searchBy);            
            cmd.Parameters.AddWithValue("@ColValue", searchValue);
            cmd.Parameters.AddWithValue("@Operator", Operator);
            cmd.Parameters.AddWithValue("@ShowLeft", showLeft);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllStudentAutoComplete";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentAutoComplete beData = new BE.Academic.Transaction.StudentAutoComplete();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.UserId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.UserName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.LedgerPanaNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Batch = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Faculty = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.SemesterId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.ClassYearId = reader.GetInt32(17);
                    try
                    {
                        if (!(reader[18] is DBNull)) beData.BatchId = reader.GetInt32(18);
                        if (!(reader[19] is DBNull)) beData.ClassId = reader.GetInt32(19);
                        if (!(reader[20] is DBNull)) beData.SectionId = reader.GetInt32(20);
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

        public AcademicLib.API.Student.SiblingDetailCollections getStudentSiblingDetails(int UserId, int StudentId, int AcademicYearId)
        {
            AcademicLib.API.Student.SiblingDetailCollections dataColl = new API.Student.SiblingDetailCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetSiblingDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    API.Student.SiblingDetail det = new API.Student.SiblingDetail();
                    if (!(reader[0] is DBNull)) det.RegdNo = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.RollNo = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) det.Name = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) det.Relation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.Remarks = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) det.StudentId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) det.UserName = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) det.Pwd = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) det.PhotoPath = Convert.ToString(reader[10]);
                    dataColl.Add(det);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        API.Student.StudentAcademicDetail acad = new API.Student.StudentAcademicDetail();

                        if (!(reader[0] is DBNull)) acad.RegNo = reader.GetString(0);
                        if (!(reader[1] is DBNull)) acad.Name = reader.GetString(1);
                        if (!(reader[2] is DBNull)) acad.AcademicYear = reader.GetString(2);
                        if (!(reader[3] is DBNull)) acad.ClassName = reader.GetString(3);
                        if (!(reader[4] is DBNull)) acad.SectionName = reader.GetString(4);
                        if (!(reader[5] is DBNull)) acad.Batch = reader.GetString(5);
                        if (!(reader[6] is DBNull)) acad.Semester = reader.GetString(6);
                        if (!(reader[7] is DBNull)) acad.ClassYear = reader.GetString(7);
                        if (!(reader[8] is DBNull)) acad.RollNo = Convert.ToInt32(reader[8]);
                        if (!(reader[9] is DBNull)) acad.BoardRegNo = Convert.ToString(reader[9]);
                        if (!(reader[10] is DBNull)) acad.IsRunning = Convert.ToBoolean(reader[10]);
                        if (!(reader[11] is DBNull)) acad.LeftStatus = reader.GetString(11);
                        if (!(reader[12] is DBNull)) acad.LeftDate_BS = Convert.ToString(reader[12]);
                        if (!(reader[13] is DBNull)) acad.LeftDate_AD = Convert.ToDateTime(reader[13]);

                        dataColl.AcademicDetails.Add(acad);
                    }
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

        public AcademicLib.API.Student.StudentProfile getStudentForApp(int UserId,int AcademicYearId, int? StudentId, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null, string regNo = null)
        {
            AcademicLib.API.Student.StudentProfile beData = new API.Student.StudentProfile();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@RegNo", regNo);
            cmd.CommandText = "usp_GetStudentProfileForApp";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new API.Student.StudentProfile();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EnrollNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Gender = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.HouseName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.BloodGroup = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.DOB_AD = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.DOB_BS = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.BusRoot = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BusStop = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.F_ContactNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.MotherName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.M_ContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.GuardianName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Relation = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.G_Address = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.G_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.PhotoPath = reader.GetString(23);

                    if (!(reader[24] is DBNull)) beData.BoardName = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BoardRegNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.PhysicalDisability = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.IsPhysicalDisability = reader.GetBoolean(27);
                    if (!(reader[28] is DBNull)) beData.Age = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.AdmitDate_AD = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.AdmitDate_BS = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Height = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.Weigth = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.ContactNo = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.Email = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.F_Profession = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.M_Profession = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.F_Email = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.M_Email = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.PA_FullAddress = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.CA_FullAddress = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.FeeAmount = Convert.ToDouble(reader[41]);
                    if (!(reader[42] is DBNull)) beData.PayAmount = Convert.ToDouble(reader[42]);
                    if (!(reader[43] is DBNull)) beData.Discount = Convert.ToDouble(reader[43]);
                    if (!(reader[44] is DBNull)) beData.DuesAmount = Convert.ToDouble(reader[44]);
                    if (!(reader[45] is DBNull)) beData.LastPaymentDate_AD = reader.GetDateTime(45);
                    if (!(reader[46] is DBNull)) beData.LastPaymentDate_BS = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.LastPaymentAmt = Convert.ToDouble(reader[47]);
                    if (!(reader[48] is DBNull)) beData.UserName = Convert.ToString(reader[48]);
                    if (!(reader[49] is DBNull)) beData.CardNo = Convert.ToInt64(reader[49]);
                    if (!(reader[50] is DBNull)) beData.Religion = Convert.ToString(reader[50]);
                    if (!(reader[51] is DBNull)) beData.CasteId = Convert.ToInt32(reader[51]);
                    if (!(reader[52] is DBNull)) beData.Nationality = Convert.ToString(reader[52]);
                    if (!(reader[53] is DBNull)) beData.MotherTongue = Convert.ToString(reader[53]);
                    if (!(reader[54] is DBNull)) beData.Aim = Convert.ToString(reader[54]);
                    if (!(reader[55] is DBNull)) beData.BirthCertificateNo = Convert.ToString(reader[55]);
                    if (!(reader[56] is DBNull)) beData.CitizenshipNo = Convert.ToString(reader[56]);
                    if (!(reader[57] is DBNull)) beData.Remarks = Convert.ToString(reader[57]);
                    if (!(reader[58] is DBNull)) beData.G_Profesion = Convert.ToString(reader[58]);
                    if (!(reader[59] is DBNull)) beData.G_Email = Convert.ToString(reader[59]);

                    if (!(reader[60] is DBNull)) beData.PA_Province = Convert.ToString(reader[60]);
                    if (!(reader[61] is DBNull)) beData.PA_District = Convert.ToString(reader[61]);
                    if (!(reader[62] is DBNull)) beData.PA_LocalLevel = Convert.ToString(reader[62]);
                    if (!(reader[63] is DBNull)) beData.PA_WardNo = Convert.ToInt32(reader[63]);
                    if (!(reader[64] is DBNull)) beData.PA_Village = Convert.ToString(reader[64]);
                    if (!(reader[65] is DBNull)) beData.CA_Province = Convert.ToString(reader[65]);
                    if (!(reader[66] is DBNull)) beData.CA_District = Convert.ToString(reader[66]);
                    if (!(reader[67] is DBNull)) beData.CA_LocalLevel = Convert.ToString(reader[67]);
                    if (!(reader[68] is DBNull)) beData.CA_WardNo = Convert.ToInt32(reader[68]);
                    if (!(reader[69] is DBNull)) beData.AcademicYear = Convert.ToString(reader[69]);
                    if (!(reader[70] is DBNull)) beData.IsNewStudent = Convert.ToBoolean(reader[70]);
                    if (!(reader[71] is DBNull)) beData.StudentType = Convert.ToString(reader[71]);
                    if (!(reader[72] is DBNull)) beData.Medium = Convert.ToString(reader[72]);
                    if (!(reader[73] is DBNull)) beData.TransportPoint = Convert.ToString(reader[73]);
                    if (!(reader[74] is DBNull)) beData.BoarderName = Convert.ToString(reader[74]);
                    if (!(reader[75] is DBNull)) beData.SignaturePath = Convert.ToString(reader[75]);

                    if (!(reader[76] is DBNull)) beData.PA_LAN = Convert.ToDouble(reader[76]);
                    if (!(reader[77] is DBNull)) beData.PA_LAT = Convert.ToDouble(reader[77]);
                    if (!(reader[78] is DBNull)) beData.CA_LAN = Convert.ToDouble(reader[78]);
                    if (!(reader[79] is DBNull)) beData.CA_LAT = Convert.ToDouble(reader[79]);
                    if (!(reader[80] is DBNull)) beData.CA_Village = Convert.ToString(reader[80]);

                    try
                    {
                        if (!(reader[81] is DBNull)) beData.Batch = Convert.ToString(reader[81]);
                        if (!(reader[82] is DBNull)) beData.Faculty = Convert.ToString(reader[82]);
                        if (!(reader[83] is DBNull)) beData.Semester = Convert.ToString(reader[83]);
                        if (!(reader[84] is DBNull)) beData.ClassYear = Convert.ToString(reader[84]);
                        if (!(reader[85] is DBNull)) beData.LedgerPanaNo = Convert.ToString(reader[85]);
                        if (!(reader[86] is DBNull)) beData.VehicleNo = Convert.ToString(reader[86]);
                        if (!(reader[87] is DBNull)) beData.TransportFare = Convert.ToDouble(reader[87]);

                        if (!(reader[88] is DBNull)) beData.UserId = Convert.ToInt32(reader[88]);
                        if (!(reader[89] is DBNull)) beData.Pwd = Convert.ToString(reader[89]);
                        if (!(reader[90] is DBNull)) beData.QrCode = Convert.ToString(reader[90]);
                        if (!(reader[91] is DBNull)) beData.Branch = Convert.ToString(reader[91]);
                        if (!(reader[92] is DBNull)) beData.IsLeft = Convert.ToBoolean(reader[92]);
                        if (!(reader[93] is DBNull)) beData.LeftDate_AD = Convert.ToDateTime(reader[93]);
                        if (!(reader[94] is DBNull)) beData.LeftDate_BS = Convert.ToString(reader[94]);
                        if (!(reader[95] is DBNull)) beData.LeftRemarks = Convert.ToString(reader[95]);
                        if (!(reader[96] is DBNull)) beData.Caste = Convert.ToString(reader[96]);
                        if (!(reader[97] is DBNull)) beData.AgeDet = Convert.ToString(reader[97]);
                        if (!(reader[98] is DBNull)) beData.EMSId = Convert.ToString(reader[98]);

                        if (!(reader[99] is DBNull)) beData.ClassId = reader.GetInt32(99);
                        if (!(reader[100] is DBNull)) beData.SectionId = reader.GetInt32(100);
                        if (!(reader[101] is DBNull)) beData.BatchId = reader.GetInt32(101);
                        if (!(reader[102] is DBNull)) beData.SemesterId = reader.GetInt32(102);
                        if (!(reader[103] is DBNull)) beData.ClassYearId = reader.GetInt32(103);
                        if (!(reader[104] is DBNull)) beData.BranchId = reader.GetInt32(104);
                        if (!(reader[105] is DBNull)) beData.StudentStatus = reader.GetString(105);
                    }
                    catch { }

                }
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
                 
                    beData.AcademicDetailsColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    API.Student.SiblingDetail det = new API.Student.SiblingDetail();
                    if (!(reader[0] is DBNull)) det.RegdNo = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.RollNo = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) det.Name = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) det.Relation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.Remarks = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) det.IsLeft = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) det.LeftDate_AD = Convert.ToDateTime(reader[8]);
                    if (!(reader[9] is DBNull)) det.LeftDate_BS = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) det.LeftRemarks = Convert.ToString(reader[10]);
                    beData.SiblingDetailColl.Add(det);
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

        public AcademicLib.BE.Academic.Transaction.StudentUserCollections getStudentUserList(int UserId,int AcademicYearId, int ClassId,string SectionIdColl,int? BatchId,int? SemesterId,int? ClassYearId)
        {
            AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl = new BE.Academic.Transaction.StudentUserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);

            if (!string.IsNullOrEmpty(SectionIdColl))
                cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);

            cmd.CommandText = "usp_GetStudentUserList";
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
                    if (!(reader[6] is DBNull)) beData.FatherName= reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.UserId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.UserName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Pwd = reader.GetString(10);

                    try
                    {
                        if (!(reader[11] is DBNull)) beData.Address = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.PhotoPath = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.Branch = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.RegdNo = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.Gender = reader.GetString(15);
                        if (!(reader[16] is DBNull)) beData.Faculty = reader.GetString(16);
                        if (!(reader[17] is DBNull)) beData.Batch = reader.GetString(17);
                        if (!(reader[18] is DBNull)) beData.Semester = reader.GetString(18);
                        if (!(reader[19] is DBNull)) beData.ClassYear = reader.GetString(19);

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

        public ResponeValues ResetClassWisePwd(int UserId,int ClassId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "sp_ResetClassWiseStudentPwd";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;           
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

        public ResponeValues ResetPwdEmployeee(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_ResetAllEmployeePwd";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);

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
        public ResponeValues GenerateUser(int UserId,int AcademicYearId, int AsPer, bool CanUpdateUserName, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AsPer", AsPer);
            cmd.CommandText = "sp_GetAutoCreateStudentUser";
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

        public AcademicLib.BE.Academic.Transaction.StudentListCollections getClassWiseStudentList(int UserId, int AcademicYearId, int ClassId, string SectionIdColl, bool FilterSection = true, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, int? SubjectId = null, int? BranchId = null, int? FacultyId = null)
        {
            AcademicLib.BE.Academic.Transaction.StudentListCollections dataColl = new BE.Academic.Transaction.StudentListCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);

            if (!string.IsNullOrEmpty(SectionIdColl) && SectionIdColl.Trim() != "0")
                cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);

            cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            //DONR Filter
            cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
            cmd.CommandText = "usp_GetClassWiseStudentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentList beData = new BE.Academic.Transaction.StudentList();
                    beData.StudentId = reader.GetInt32(0);
                    beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.PhotoPath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.EnrollNo = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.CardNo = reader.GetInt64(12);
                    if (!(reader[13] is DBNull)) beData.UserName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Pwd = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ContactNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.DOB_AD = reader.GetDateTime(16);
                    if (!(reader[17] is DBNull)) beData.DOB_BS = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.BoardRegdNo = reader.GetString(18);

                    try
                    {
                        if (!(reader[19] is DBNull)) beData.ClassId = reader.GetInt32(19);
                        if (!(reader[20] is DBNull)) beData.SectionId = reader.GetInt32(20);

                        if (!(reader[21] is DBNull)) beData.SemesterId = reader.GetInt32(21);
                        if (!(reader[22] is DBNull)) beData.ClassYearId = reader.GetInt32(22);
                        if (!(reader[23] is DBNull)) beData.BatchId = reader.GetInt32(23);
                        if (!(reader[24] is DBNull)) beData.Semester = reader.GetString(24);
                        if (!(reader[25] is DBNull)) beData.ClassYear = reader.GetString(25);
                        if (!(reader[26] is DBNull)) beData.Batch = reader.GetString(26);
                        if (!(reader[27] is DBNull)) beData.MotherName = reader.GetString(27);
                        if (!(reader[28] is DBNull)) beData.MotherContactNo = reader.GetString(28);
                        if (!(reader[29] is DBNull)) beData.LedgerPanaNo = reader.GetString(29);
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

        public ResponeValues getAutoRegdNo(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@RegdNo", System.Data.SqlDbType.NVarChar,100);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetStudentAutoRegdNo";
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
        public ResponeValues getAutoRollNo(int UserId,int? ClassId,int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId)
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
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetStudentAutoRollNo";
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

        public AcademicLib.BE.Academic.Transaction.StudentListCollections getStudentListForMarkImport(int UserId,int AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.StudentListCollections dataColl = new BE.Academic.Transaction.StudentListCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentListForMarkImport";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentList beData = new BE.Academic.Transaction.StudentList();
                    beData.StudentId = reader.GetInt32(0);
                    beData.RollNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BoardRegdNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);                    
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

        public List<AcademicLib.BE.Academic.Transaction.ImportStudent> getStudentListForEMIS(int UserId,int AcademicYearId)
        {
            List<AcademicLib.BE.Academic.Transaction.ImportStudent> dataColl = new List<BE.Academic.Transaction.ImportStudent>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentListForEMIS";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ImportStudent beData = new BE.Academic.Transaction.ImportStudent();
                    if (!(reader[0] is DBNull)) beData.RegNo = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.LastName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Gender = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FatherName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MotherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.DOB_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.DOBAD_Str = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Caste = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PhysicalDisability = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsECD = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.ClassName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.EMSId =reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.StudentId = reader.GetInt32(13);
                    dataColl.Add(beData);
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
            return dataColl;
        }

        public List<AcademicLib.BE.Academic.Transaction.Student> getClassWiseStudentForUpdate(int UserId,int AcademicYearId, int ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId,int? BranchId=null)
        {
            List<AcademicLib.BE.Academic.Transaction.Student> dataColl = new List<BE.Academic.Transaction.Student>();

            dal.OpenConnection();            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentListForUpdate";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.Student beData = new BE.Academic.Transaction.Student();
                    beData.StudentId = reader.GetInt32(0);
                    beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BoardRegNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FirstName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MiddleName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.LastName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.RollNo = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.DOB_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.FatherName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.F_ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.MotherName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.M_Contact = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.PA_FullAddress = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.HouseNameId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.Weigth = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Height = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Aim = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.PhysicalDisability = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BloodGroup = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.IsNewStudent = reader.GetBoolean(21);
                    if (!(reader[22] is DBNull)) beData.SectionId = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.CasteId = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.CardNo = reader.GetInt64(24);
                    if (!(reader[25] is DBNull)) beData.EMSId =" "+reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.StudentTypeId = reader.GetInt32(26);
                    if (!(reader[27] is DBNull)) beData.LedgerPanaNo = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.CitizenshipNo = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.MediumId = reader.GetInt32(29);
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

        public AcademicLib.RE.Academic.StudentSummaryList getStudentSummaryList(int UserId,int AcademicYearId, string ClassIdColl,string SectionIdColl,string StudentTypeIdColl,string HouseNameIdColl,string CasteIdColl, int flag, List<int> AgeRange,string MediumIdColl, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null,int? BranchId=null)
        {
            AcademicLib.RE.Academic.StudentSummaryList dataColl = new RE.Academic.StudentSummaryList();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@StudentTypeIdColl", StudentTypeIdColl);
            cmd.Parameters.AddWithValue("@HouseNameIdColl", HouseNameIdColl);
            cmd.Parameters.AddWithValue("@CasteIdColl", CasteIdColl);
            cmd.Parameters.AddWithValue("@MediumIdColl", MediumIdColl);

            int r = 1;
            if(AgeRange!=null && AgeRange.Count > 0)
            {
                foreach(var ar in AgeRange)
                {
                    cmd.Parameters.AddWithValue("@R" + r.ToString(), ar);
                    r++;
                }
            }

            cmd.Parameters.AddWithValue("@flag", flag);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentSummary beData = new RE.Academic.StudentSummary();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RegNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Gender = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.MotherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.M_ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Address = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PhotoPath = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BloodGroup = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.DOB_AD = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.DOB_BS = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.HouseName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Medium = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BoardName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.BoardRegNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.EnrollNo = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.CardNo = reader.GetInt64(24);
                    if (!(reader[25] is DBNull)) beData.BusStop = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.BusPoint = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.GuardianName = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Relation = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Address = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_ContacNo = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.UserName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.AdmitDate_AD = reader.GetDateTime(32);
                    if (!(reader[33] is DBNull)) beData.AdmitDate_BS = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.CurrentAddress = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.Caste = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.StudentType = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.AgeRange = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.LedgerPanaNo = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.EMSId =" "+reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.IsNew = reader.GetBoolean(40);
                    if (!(reader[41] is DBNull)) beData.FatherOccupation = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.GuardianOccupation = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.Level = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.Faculty = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.Semester = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.ClassYear = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.Batch = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.MotherOccupation = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.Email = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.F_Email = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.M_Email = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.G_Email = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.CitizenshipNo = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.MotherTongue = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.Height = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.Weight = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.PhysicalDisability = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.Aim = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.BirthCertificateNo = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.Remarks = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.PA_Province = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.PA_District = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.PA_LocalLevel = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.PA_Village = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(65);
                    if (!(reader[66] is DBNull)) beData.LeftDate_BS = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.LeftRemarks = reader.GetString(67);
                    if (!(reader[68] is DBNull)) beData.Pwd = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.IsUserActive = reader.GetBoolean(69);
                    if (!(reader[70] is DBNull)) beData.Branch = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.NameNP = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.AgeStr = reader.GetString(72);
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

        public AcademicLib.RE.Academic.StudentDetailsForLeftCollections getStudentListForBoardRegdNo(int UserId,int AcademicYearId, int ClassId,int? SectionId,bool all, int? SemesterId = null, int? ClassYearId = null, int? typeId=null,int? BatchId=null,int? BranchId=null)
        {
            AcademicLib.RE.Academic.StudentDetailsForLeftCollections dataColl = new RE.Academic.StudentDetailsForLeftCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@All", all);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@TypeId", typeId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentListForBoardRegdNo";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentDetailsForLeft beData = new RE.Academic.StudentDetailsForLeft();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);                    
                    if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);                    
                    if (!(reader[8] is DBNull)) beData.IsLeft = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.BoardRegdNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BoardTypeId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.BoardName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.LeftDate_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LeftRemarks = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.FatherContact = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Address = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Gender = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Semester = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ClassYear = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.SemesterId = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.ClassYearId = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.Batch = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.BatchId = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.StatusId = reader.GetInt32(25);
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

        public AcademicLib.RE.Academic.StudentDetailsForLeftCollections getStudentListForOpening(int UserId, int AcademicYearId, int ClassId, int? SectionId, bool all, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null, int? BranchId = null)
        {
            AcademicLib.RE.Academic.StudentDetailsForLeftCollections dataColl = new RE.Academic.StudentDetailsForLeftCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@All", all);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@TypeId", typeId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentListForOpening";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentDetailsForLeft beData = new RE.Academic.StudentDetailsForLeft();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.IsLeft = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.BoardRegdNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BoardTypeId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.BoardName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.LeftDate_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LeftRemarks = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.FatherContact = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Address = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Gender = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Semester = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ClassYear = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.SemesterId = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.ClassYearId = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.Batch = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.BatchId = reader.GetInt32(24);

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

        public AcademicLib.RE.Academic.StudentIdCardCollections getStudentListForIdCard(int UserId,int AcademicYearId, int ClassId, int? SectionId,string StudentIdColl,DateTime? validFrom,DateTime? validTo,double duesAmt,int For,int rollNoFrom,int rollNoTo,int? ForMonth, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,bool OnlyPhotoStudent=false,string ClassIdColl="")
        {
            AcademicLib.RE.Academic.StudentIdCardCollections dataColl = new RE.Academic.StudentIdCardCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@StudentIdColl", StudentIdColl);
            cmd.Parameters.AddWithValue("@ValidFrom", validFrom);
            cmd.Parameters.AddWithValue("@ValidTo", validTo);
            cmd.Parameters.AddWithValue("@DuesAmt", duesAmt);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@For", For);
            cmd.Parameters.AddWithValue("@RollNoFrom", rollNoFrom);
            cmd.Parameters.AddWithValue("@RollNoTo", rollNoTo);
            cmd.Parameters.AddWithValue("@ForMonth", ForMonth);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@OnlyPhotoStudent", OnlyPhotoStudent);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.CommandText = "usp_GetStudentListForIdCard";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentIdCard beData = new RE.Academic.StudentIdCard();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.M_ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BlooedGroup = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.DOB_AD = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.DOB_BS = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.HouseName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Medium = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.EnrollNo = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.CardNo = Convert.ToString(reader[20]);
                    if (!(reader[21] is DBNull)) beData.BusPoint = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.BusStop = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.GuardianName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.G_Relation = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.G_ContactNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.UserName = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.ValidFromBS = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ValidToBS = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.ValidFromAD = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.ValidToAD = reader.GetDateTime(30);
                    if (!(reader[31] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[31]);
                    if (!(reader[32] is DBNull)) beData.CompName = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.CompAddress = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.CompPhoneNo = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.CompFaxNo = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.CompEmailId = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CompWebSite = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.CompLogoPath = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CompImgPath = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.CompBannerPath = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.CompRegdNo = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.CompPanVat = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.MembershipNo = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.LibValidFromBS = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.LibValidToBS = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.LibValidFromAD = reader.GetDateTime(46);
                    if (!(reader[47] is DBNull)) beData.LibValidToAD = reader.GetDateTime(47);
                    if (!(reader[48] is DBNull)) beData.CurrentAddress = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.BranchName = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.BranchAddress = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.QrCodeStr = reader.GetString(51);

                    if (!(reader[52] is DBNull)) beData.RouteName = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.PointName = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.RoomName = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.BedName = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.BedNo = reader.GetInt32(56);

                    if (!(reader[57] is DBNull)) beData.FatherPhotoPath = reader.GetString(57);
                    if (!(reader[58] is DBNull)) beData.MotherPhotoPath = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.GuardianPhotoPath = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.TravelType = reader.GetString(60);

                    if (!(reader[61] is DBNull)) beData.Level = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.Faculty = reader.GetString(62);
                    if (!(reader[63] is DBNull)) beData.Semester = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.ClassYear = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.Batch = reader.GetString(65);

                    try
                    {
                        if (!(reader[66] is DBNull)) beData.EMSId = " "+reader.GetString(66);
                        if (!(reader[67] is DBNull)) beData.NextClass = reader.GetString(67);

                        if (!(reader[68] is DBNull)) beData.SNo = reader.GetInt32(68);
                        if (!(reader[69] is DBNull)) beData.PageSNo = reader.GetInt32(69);
                        if (!(reader[70] is DBNull)) beData.RSNo = reader.GetInt32(70);
                        if (!(reader[71] is DBNull)) beData.Pwd = reader.GetString(71);
                        if (!(reader[72] is DBNull)) beData.ClassTeacher = reader.GetString(72);
                        if (!(reader[73] is DBNull)) beData.ClassTecherContactNo = reader.GetString(73);
                        if (!(reader[74] is DBNull)) beData.Gender = reader.GetString(74);
                        if (!(reader[75] is DBNull)) beData.Caste = reader.GetString(75);
                        if (!(reader[76] is DBNull)) beData.Religion = reader.GetString(76);
                        if (!(reader[77] is DBNull)) beData.PhysicalDisability = reader.GetString(77);
                        if (!(reader[78] is DBNull)) beData.RowSNo = reader.GetInt32(78);
                        if (!(reader[79] is DBNull)) beData.Email = reader.GetString(79);
                        if (!(reader[80] is DBNull)) beData.F_Email = reader.GetString(80);
                        if (!(reader[81] is DBNull)) beData.M_Email = reader.GetString(81);
                        if (!(reader[82] is DBNull)) beData.NameNP = reader.GetString(82);

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

        public AcademicLib.RE.Academic.AnalysisCollections getAnalysis(int UserId,int AcademicYearId,List<int> rangeColl)
        {
            AcademicLib.RE.Academic.AnalysisCollections dataColl = new RE.Academic.AnalysisCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            int r = 1;
            if (rangeColl != null)
            {
                foreach (var v in rangeColl)
                {
                    cmd.Parameters.AddWithValue("@R" + r.ToString(), v);
                    r++;
                }
            }
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAcademicAnalysis";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.Analysis beData = new RE.Academic.Analysis();
                    if (!(reader[0] is DBNull)) beData.NoOfStudent = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Class = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Section = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Gender = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.House = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Medium = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Board = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Route = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Point = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TravelType = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Room = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.IsNew = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.Disability = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.StudentType = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Age = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Batch = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Faculty = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Level = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Semester = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ClassYear = reader.GetString(20);
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

        public AcademicLib.BE.Academic.Transaction.StudentForTranCollections getStudentForTran(int UserId,int AcademicYearId, int ClassId,int? SectionId,int? ExamTypeId,int? BranchId=null, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            AcademicLib.BE.Academic.Transaction.StudentForTranCollections dataColl = new BE.Academic.Transaction.StudentForTranCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.CommandText = "usp_GetClassStudentForTran";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentForTran beData = new BE.Academic.Transaction.StudentForTran();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SymbolNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.BoardRegNo = reader.GetString(6);
                    
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

        public AcademicLib.BE.Academic.Transaction.StudentForTranCollections getAllStudentForTran(int UserId,int AcademicYearId, int? ClassId=null,int? SectionId=null)
        {
            AcademicLib.BE.Academic.Transaction.StudentForTranCollections dataColl = new BE.Academic.Transaction.StudentForTranCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllStudentForTran";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentForTran beData = new BE.Academic.Transaction.StudentForTran();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.SectionId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Gender = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsLeft = reader.GetBoolean(10);
                    try
                    {
                        if (!(reader[11] is DBNull)) beData.FatherName = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);

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

        public AcademicLib.RE.Academic.StudentBirthDayCollections getStudentBirthDayList(int UserId,int? AcademicYearId, DateTime? dateFrom,DateTime? dateTo)
        {
            AcademicLib.RE.Academic.StudentBirthDayCollections dataColl = new RE.Academic.StudentBirthDayCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromDate", dateFrom);
            cmd.Parameters.AddWithValue("@ToDate", dateTo);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentBirthDay";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentBirthDay beData = new RE.Academic.StudentBirthDay();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AgeYear = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.AgeMonth = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.AgeDay = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DOB_AD = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.DOB_BS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Email = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.CompName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.CompAddress = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.CompPhoneNo = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.CompFaxNo = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.CompEmail = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.CompWebSite = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.CompLogoPath = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CompImgPath = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.CompBannerPath = reader.GetString(25);

                    //Change the indexing no Poush 23 by Prashant
                    if (!(reader[26] is DBNull)) beData.F_Email = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.M_Email = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Email = reader.GetString(28);

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

        public ResponeValues UpdateDOB(int UserId, List<BE.Academic.Transaction.ImportStudentDOB> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete from tmpStudentDOB";
            cmd.ExecuteNonQuery();
            try
            {
                foreach (var beData in dataColl)
                {
                    if (beData.StudentId.HasValue && beData.StudentId.Value > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        if (beData.DOB_AD.HasValue)
                            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
                        else
                            cmd.Parameters.AddWithValue("@DOB_AD", DBNull.Value);

                        cmd.Parameters.AddWithValue("@DOB_BS", beData.DOB_BS);

                        cmd.Parameters.AddWithValue("@NY", beData.NY);
                        cmd.Parameters.AddWithValue("@NM", beData.NM);
                        cmd.Parameters.AddWithValue("@ND", beData.ND);
                        cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tmpStudentDOB(StudentId,DOB_AD,DOB_BS,NY,NM,ND) values(@StudentId,@DOB_AD,@DOB_BS,@NY,@NM,@ND)";
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateStudentDOB";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student DOB Updated";
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

        public ResponeValues UpdateAdmitDate(int UserId, List<BE.Academic.Transaction.ImportStudentAdmitDate> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete from tmpStudentAdmitDate";
            cmd.ExecuteNonQuery();
            try
            {
                foreach (var beData in dataColl)
                {
                    if (beData.StudentId.HasValue && beData.StudentId.Value > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        if (beData.AdmitDate_AD.HasValue)
                            cmd.Parameters.AddWithValue("@AdmitDate_AD", beData.AdmitDate_AD);
                        else
                            cmd.Parameters.AddWithValue("@AdmitDate_AD", DBNull.Value);

                        cmd.Parameters.AddWithValue("@AdmitDate_BS", beData.AdmitDate_BS);

                        cmd.Parameters.AddWithValue("@NY", beData.NY);
                        cmd.Parameters.AddWithValue("@NM", beData.NM);
                        cmd.Parameters.AddWithValue("@ND", beData.ND);
                        cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tmpStudentAdmitDate(StudentId,AdmitDate_AD,AdmitDate_BS,NY,NM,ND) values(@StudentId,@AdmitDate_AD,@AdmitDate_BS,@NY,@NM,@ND)";
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateStudentAdmitDate";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Admit Date Updated";
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

        #region "Update Profile"
        public ResponeValues UpdatePersonalInfo(AcademicLib.API.Student.PersonalInfo beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);            
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@MotherTongue", beData.MotherTongue);
            cmd.Parameters.AddWithValue("@Height", beData.Height);
            cmd.Parameters.AddWithValue("@Weigth", beData.Weigth);
            cmd.Parameters.AddWithValue("@Aim", beData.Aim);
            cmd.Parameters.AddWithValue("@BirthCertificateNo", beData.BirthCertificateNo);
            cmd.Parameters.AddWithValue("@CitizenshipNo", beData.CitizenshipNo);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentPerfornalInfo";
            cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
            cmd.Parameters.AddWithValue("@IsPhysicalDisability", beData.IsPhysicalDisability);
            cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[16].Value);

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

        public ResponeValues UpdateParentDetails(AcademicLib.API.Student.ParentDetails beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@F_Profession", beData.F_Profession);
            cmd.Parameters.AddWithValue("@F_ContactNo", beData.F_ContactNo);
            cmd.Parameters.AddWithValue("@F_Email", beData.F_Email);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@M_Profession", beData.M_Profession);
            cmd.Parameters.AddWithValue("@M_ContactNo", beData.M_ContactNo);
            cmd.Parameters.AddWithValue("@M_Email", beData.M_Email);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentParentDetails";


            try
            {
                cmd.ExecuteNonQuery();

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

        public ResponeValues UpdateContactInfo(AcademicLib.API.Student.ContactInfo beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Email", beData.Email);            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentContactInfo";

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

        public ResponeValues UpdateGuardianDetails(AcademicLib.API.Student.GuardianDetails beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@GuardianName", beData.GuardianName);
            cmd.Parameters.AddWithValue("@Relation", beData.Relation);
            cmd.Parameters.AddWithValue("@G_ContactNo", beData.G_ContactNo);
            cmd.Parameters.AddWithValue("@G_Address", beData.G_Address);
            cmd.Parameters.AddWithValue("@G_Profesion", beData.G_Profesion);
            cmd.Parameters.AddWithValue("@G_Email", beData.G_Email);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentGuardianDetails";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

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

        public ResponeValues UpdatePermanentAddress(AcademicLib.API.Student.PermanentAddress beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@PA_Province", beData.PA_Province);
            cmd.Parameters.AddWithValue("@PA_District", beData.PA_District);
            cmd.Parameters.AddWithValue("@PA_LocalLevel", beData.PA_LocalLevel);
            cmd.Parameters.AddWithValue("@PA_WardNo", beData.PA_WardNo);
            cmd.Parameters.AddWithValue("@PA_Village", beData.PA_Village);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentPermanentAddress";
            cmd.Parameters.AddWithValue("@PA_LAT", beData.PA_LAT);
            cmd.Parameters.AddWithValue("@PA_LAN", beData.PA_LAN);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

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

        public ResponeValues UpdateTemporaryAddress(AcademicLib.API.Student.TemporaryAddress beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@CA_FullAddress", beData.CA_FullAddress);
            cmd.Parameters.AddWithValue("@CA_Province", beData.CA_Province);
            cmd.Parameters.AddWithValue("@CA_District", beData.CA_District);
            cmd.Parameters.AddWithValue("@CA_LocalLevel", beData.CA_LocalLevel);
            cmd.Parameters.AddWithValue("@CA_WardNo", beData.CA_WardNo);
            cmd.Parameters.AddWithValue("@CA_Village", beData.CA_Village);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentTemporaryAddress";
            cmd.Parameters.AddWithValue("@CA_LAT", beData.CA_LAT);
            cmd.Parameters.AddWithValue("@CA_LAN", beData.CA_LAN);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

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

        public ResponeValues UpdatePhoto(int UserId,string PhotoPath,int? StudentId)
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
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_UpdateStudentPhoto";

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

        public ResponeValues UpdatePhoto(int UserId, List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 

            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath); 
                    cmd.CommandText = "usp_UpdateStudentPhotoFromURL";
                    cmd.ExecuteNonQuery();
                }
                resVal.IsSuccess = true;
                resVal.ResponseMSG ="Update Student Photo "+ GLOBALMSG.SUCCESS;
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

        public ResponeValues UpdateSignature(int UserId, string SignaturePath)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SignaturePath", SignaturePath);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateStudentSignature";

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

        public ResponeValues UpdatePreviousSchool(int UserId, List<AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails> beDataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            

            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelStudentAcademicDetails";
                cmd.ExecuteNonQuery();
                foreach (AcademicLib.BE.Academic.Transaction.StudentPreviousAcademicDetails beData in beDataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);                    
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
                    cmd.CommandText = "usp_UpdateStudentAcademicDetails";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Previous Academic Details Updated";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {                
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }


        #endregion


        public AcademicLib.API.Admin.Student admin_StudentList(int UserId,int AcademicYearId, int? ClassId = null, int? SectionId = null,int? BatchId=null,int? SemesterId=null,int? ClassYearId=null)
        {
            AcademicLib.API.Admin.Student dataColl = new API.Admin.Student();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);

            cmd.CommandText = "usp_Admin_StudentList";
            try
            {
                dataColl.StudentColl = new List<API.Admin.StudentDetail>();
                dataColl.ClassColl = new List<API.Admin.ClassDetail>();

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.StudentDetail beData = new API.Admin.StudentDetail();
                    if (!(reader[0] is DBNull)) beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);                   
                    if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.RegNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsNew = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.PhotoPath = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.CurrentAddress = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Level = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Faculty = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Semester = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.ClassYear = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Batch = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.BatchId = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.SemesterId = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.ClassYearId = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.MotherName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.MotherContactNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.LedgerPanaNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.DOB_AD = reader.GetDateTime(25);
                    if (!(reader[26] is DBNull)) beData.DOB_BS = reader.GetString(26);

                    dataColl.StudentColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.ClassDetail beData = new API.Admin.ClassDetail();
                    if (!(reader[0] is DBNull)) beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Batch = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Semester = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassYear = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.BatchId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.ClassYearId = reader.GetInt32(10);

                    if (!(reader[11] is DBNull)) beData.ClassOrderNo = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.SectionOrderNo = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.BatchOrderNo = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.SemesterOrderNo = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.ClassYearOrderNo = reader.GetInt32(15);

                    dataColl.ClassColl.Add(beData);
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

        public AcademicLib.API.Admin.TransportHostelAnalysis admin_TransportHostelAnalysis(int UserId,int AcademicYearId)
        {
            AcademicLib.API.Admin.TransportHostelAnalysis dataColl = new API.Admin.TransportHostelAnalysis();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "ups_admin_TransportHostelAnalysis";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.TransportHostelDetails beData = new API.Admin.TransportHostelDetails();
                    beData.SNo = reader.GetInt32(0);
                    beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Male = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Female = Convert.ToInt32(reader[3]);
                    dataColl.TransportColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.TransportHostelDetails beData = new API.Admin.TransportHostelDetails();
                    beData.SNo = reader.GetInt32(0);
                    beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Male = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Female = Convert.ToInt32(reader[3]);
                    dataColl.HostelColl.Add(beData);
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

        public ResponeValues ReArrangeRollNoCS(int UserId,int ReArrangeBy, int? AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_ReArrangeClassSectionWiseRollNo";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ReArrangeBy", ReArrangeBy);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            try
            {
                cmd.ExecuteNonQuery();
                 
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);

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

        public AcademicLib.RE.Academic.StudentPhotoCollections getStudentPhoto(int UserId,int ClassId,int? SectionId)
        {
            AcademicLib.RE.Academic.StudentPhotoCollections dataColl = new RE.Academic.StudentPhotoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.CommandText = "usp_GetStudentPhotoPath";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentPhoto beData = new RE.Academic.StudentPhoto();
                    beData.StudentId = reader.GetInt32(0);
                    beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) beData.PhotoPath = Convert.ToString(reader[3]);
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

        public AcademicLib.RE.Academic.SiblingStudentCollections getSiblintStudentList(int UserId, int AcademicYearId)
        {
            AcademicLib.RE.Academic.SiblingStudentCollections dataColl = new RE.Academic.SiblingStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.CommandText = "usp_GetSiblingStudentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.SiblingStudent beData = new RE.Academic.SiblingStudent();
                    if (!(reader[0] is DBNull)) beData.P_ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.P_SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.P_RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.P_RegNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.P_Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ClassName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.SectionName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.RollNo = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.RegNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Name = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Relation = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Remarks = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.F_Email = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.M_Email = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.G_Email = reader.GetString(17);

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

        public ResponeValues AddUpdateStudentsFromApi(int UserId, List<Public_API.Student> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add(new System.Data.DataColumn("StudentId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("AutoNumber", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("RegNo", typeof(string)));

                dt.Columns.Add(new System.Data.DataColumn("AdmitDate", typeof(DateTime)));
                dt.Columns.Add(new System.Data.DataColumn("FirstName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MiddleName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("LastName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Gender", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("DOB_AD", typeof(DateTime)));
                dt.Columns.Add(new System.Data.DataColumn("Religion", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Caste", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Nationality", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BloodGroup", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("ContactNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Email", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MotherTongue", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Height", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Weigth", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("IsPhysicalDisability", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PhysicalDisability", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Aim", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BirthCertificateNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CitizenshipNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PhotoPath", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("SignaturePath", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("ClassName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Section", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("RollNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("AcademicYear", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("IsNewStudent", typeof(bool)));
                dt.Columns.Add(new System.Data.DataColumn("StudentType", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Medium", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("HouseName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("HouseDress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("TransportPoint", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BoardersType", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Board", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("BoardRegNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("EnrollNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CardNo", typeof(long)));
                dt.Columns.Add(new System.Data.DataColumn("FatherName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("F_Profession", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("F_ContactNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("F_Email", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MotherName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("M_Profession", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("M_Contact", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("M_Email", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("GuardianName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("G_Relation", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("G_Profesion", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("G_ContactNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("G_Email", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("G_Address", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PermanentAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_FullAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_Province", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_District", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_LocalLevel", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_Village", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_WardNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_LAN", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("PA_LAT", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CurrentAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_FullAddress", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_Province", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_District", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_LocalLevel", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_WardNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("CA_LAN", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CA_LAT", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("StreetName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("EMSId", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("LedgerPanaNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Branch", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("FatherPhotoPath", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MotherPhotoPath", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("GuardianPhotoPath", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Level", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Faculty", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Semester", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("ClassYear", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Batch", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("UDF", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("F_AnnualIncome", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("M_AnnualIncome", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("ReferralCode", typeof(string)));

                int r = 1;
                foreach (var beData in dataColl)
                {
                    if (string.IsNullOrEmpty(beData.RegNo))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Reg. No. At Row "+r.ToString();
                        return resVal;
                    }

                    System.Data.DataRow dr = dt.NewRow();

                    dr["StudentId"] = 0;
                    dr["AutoNumber"] = 0;
                    dr["RegNo"] = beData.RegNo;

                    dr["AdmitDate"] = IsDBNull(beData.AdmitDate);
                    dr["FirstName"] = IsDBNull(beData.FirstName);
                    dr["MiddleName"] = IsDBNull(beData.MiddleName);
                    dr["LastName"] = IsDBNull(beData.LastName);
                    dr["Gender"] = IsDBNull(beData.Gender);
                    dr["DOB_AD"] = IsDBNull(beData.DOB_AD);
                    dr["Religion"] = IsDBNull(beData.Religion);
                    dr["Caste"] = IsDBNull(beData.Caste);
                    dr["Nationality"] = IsDBNull(beData.Nationality);
                    dr["BloodGroup"] = IsDBNull(beData.BloodGroup);
                    dr["ContactNo"] = IsDBNull(beData.ContactNo);
                    dr["Email"] = IsDBNull(beData.Email);
                    dr["MotherTongue"] = IsDBNull(beData.MotherTongue);
                    dr["Height"] = IsDBNull(beData.Height);
                    dr["Weigth"] = IsDBNull(beData.Weigth);
                    dr["IsPhysicalDisability"] = IsDBNull(beData.IsPhysicalDisability);
                    dr["PhysicalDisability"] = IsDBNull(beData.PhysicalDisability);
                    dr["Aim"] = IsDBNull(beData.Aim);
                    dr["BirthCertificateNo"] = IsDBNull(beData.BirthCertificateNo);
                    dr["CitizenshipNo"] = IsDBNull(beData.CitizenshipNo);
                    dr["Remarks"] = IsDBNull(beData.Remarks);
                    dr["PhotoPath"] = IsDBNull(beData.PhotoPath);
                    dr["SignaturePath"] = IsDBNull(beData.SignaturePath);
                    dr["ClassName"] = IsDBNull(beData.ClassName);
                    dr["Section"] = IsDBNull(beData.Section);
                    dr["RollNo"] = IsDBNull(beData.RollNo);
                    dr["AcademicYear"] = IsDBNull(beData.AcademicYear);
                    dr["IsNewStudent"] = IsDBNull(beData.IsNewStudent);
                    dr["StudentType"] = IsDBNull(beData.StudentType);
                    dr["Medium"] = IsDBNull(beData.Medium);
                    dr["HouseName"] = IsDBNull(beData.HouseName);
                    dr["HouseDress"] = IsDBNull(beData.HouseDress);
                    dr["TransportPoint"] = IsDBNull(beData.TransportPoint);
                    dr["BoardersType"] = IsDBNull(beData.BoardersType);
                    dr["Board"] = IsDBNull(beData.Board);
                    dr["BoardRegNo"] = IsDBNull(beData.BoardRegNo);
                    dr["EnrollNo"] = IsDBNull(beData.EnrollNo);
                    dr["CardNo"] = IsDBNull(beData.CardNo);
                    dr["FatherName"] = IsDBNull(beData.FatherName);
                    dr["F_Profession"] = IsDBNull(beData.F_Profession);
                    dr["F_ContactNo"] = IsDBNull(beData.F_ContactNo);
                    dr["F_Email"] = IsDBNull(beData.F_Email);
                    dr["MotherName"] = IsDBNull(beData.MotherName);
                    dr["M_Profession"] = IsDBNull(beData.M_Profession);
                    dr["M_Contact"] = IsDBNull(beData.M_Contact);
                    dr["M_Email"] = IsDBNull(beData.M_Email);
                    dr["GuardianName"] = IsDBNull(beData.GuardianName);
                    dr["G_Relation"] = IsDBNull(beData.G_Relation);
                    dr["G_Profesion"] = IsDBNull(beData.G_Profesion);
                    dr["G_ContactNo"] = IsDBNull(beData.G_ContactNo);
                    dr["G_Email"] = IsDBNull(beData.G_Email);
                    dr["G_Address"] = IsDBNull(beData.G_Address);
                    dr["PermanentAddress"] = IsDBNull(beData.PermanentAddress);
                    dr["PA_FullAddress"] = IsDBNull(beData.PA_FullAddress);
                    dr["PA_Province"] = IsDBNull(beData.PA_Province);
                    dr["PA_District"] = IsDBNull(beData.PA_District);
                    dr["PA_LocalLevel"] = IsDBNull(beData.PA_LocalLevel);
                    dr["PA_Village"] = IsDBNull(beData.PA_Village);
                    dr["PA_WardNo"] = IsDBNull(beData.PA_WardNo);
                    dr["PA_LAN"] = IsDBNull(beData.PA_LAN);
                    dr["PA_LAT"] = IsDBNull(beData.PA_LAT);
                    dr["CurrentAddress"] = IsDBNull(beData.CurrentAddress);
                    dr["CA_FullAddress"] = IsDBNull(beData.CA_FullAddress);
                    dr["CA_Province"] = IsDBNull(beData.CA_Province);
                    dr["CA_District"] = IsDBNull(beData.CA_District);
                    dr["CA_LocalLevel"] = IsDBNull(beData.CA_LocalLevel);
                    dr["CA_WardNo"] = IsDBNull(beData.CA_WardNo);
                    dr["CA_LAN"] = IsDBNull(beData.CA_LAN);
                    dr["CA_LAT"] = IsDBNull(beData.CA_LAT);
                    dr["StreetName"] = IsDBNull(beData.StreetName);
                    dr["EMSId"] = IsDBNull(beData.EMSId);
                    dr["LedgerPanaNo"] = IsDBNull(beData.LedgerPanaNo);
                    dr["Branch"] = IsDBNull(beData.Branch);
                    dr["FatherPhotoPath"] = IsDBNull(beData.FatherPhotoPath);
                    dr["MotherPhotoPath"] = IsDBNull(beData.MotherPhotoPath);
                    dr["GuardianPhotoPath"] = IsDBNull(beData.GuardianPhotoPath);
                    dr["Level"] = IsDBNull(beData.Level);
                    dr["Faculty"] = IsDBNull(beData.Faculty);
                    dr["Semester"] = IsDBNull(beData.Semester);
                    dr["ClassYear"] = IsDBNull(beData.ClassYear);
                    dr["Batch"] = IsDBNull(beData.Batch);
                    dr["UDF"] = IsDBNull(beData.UDF);
                    dr["F_AnnualIncome"] = IsDBNull(beData.F_AnnualIncome);
                    dr["M_AnnualIncome"] = IsDBNull(beData.M_AnnualIncome);
                    dr["ReferralCode"] = IsDBNull(beData.ReferralCode);

                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@StudentColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                cmd.Parameters.AddWithValue("@UserId", UserId);                
                cmd.CommandText = "usp_Pub_UpdateStudent";
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);                
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;                
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);
                 
                if (!resVal.IsSuccess)
                    resVal.ResponseMSG = resVal.ResponseMSG ;

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

        public ResponeValues AddUpdateStudentsClosingFromApi(int UserId, List<Public_API.StudentClosingBal> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(new System.Data.DataColumn("RegNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("FeeItemName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("FeeItemCode", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("DuesAmt", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("UptoMonthName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("UpToMonthId", typeof(int)));
                 
                int r = 1;
                foreach (var beData in dataColl)
                {
                    if (string.IsNullOrEmpty(beData.RegNo))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Reg. No. At Row " + r.ToString();
                        return resVal;
                    }

                    System.Data.DataRow dr = dt.NewRow();
                    dr["RegNo"] = beData.RegNo;
                    dr["FeeItemName"] = beData.FeeItemName;
                    dr["FeeItemCode"] = beData.FeeItemCode;

                    dr["DuesAmt"] = IsDBNull(beData.DuesAmt);
                    dr["UptoMonthName"] = IsDBNull(beData.UptoMonthName);
                    dr["UpToMonthId"] = IsDBNull(beData.UpToMonthId);
                     
                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@StudentColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_Pub_UpdateStudentBalance";
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!resVal.IsSuccess)
                    resVal.ResponseMSG = resVal.ResponseMSG;

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
        public ResponeValues UpdateStudent_QuickAccess(AcademicLib.BE.Academic.Transaction.Student beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegNo", beData.RegNo);
            cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@Height", beData.Height);
            cmd.Parameters.AddWithValue("@Weigth", beData.Weigth);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@PhysicalDisability", beData.PhysicalDisability);
            cmd.Parameters.AddWithValue("@CasteId", beData.CasteId);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@PA_FullAddress", beData.PA_FullAddress);
            cmd.Parameters.AddWithValue("@TransportPointId", beData.TransportPointId);
            cmd.Parameters.AddWithValue("@BoardersTypeId", beData.BoardersTypeId);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@F_ContactNo", beData.F_ContactNo);
            cmd.Parameters.AddWithValue("@F_Email", beData.F_Email);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@M_Contact", beData.M_Contact);
            cmd.Parameters.AddWithValue("@M_Email", beData.M_Email);
            cmd.Parameters.AddWithValue("@GuardianName", beData.GuardianName);
            cmd.Parameters.AddWithValue("@G_Relation", beData.G_Relation);
            cmd.Parameters.AddWithValue("@G_ContactNo", beData.G_ContactNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);

            cmd.CommandText = "usp_QuickStudentUpdate";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[29].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[30].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[31].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[29].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[29].Value);

                if (!(cmd.Parameters[30].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[30].Value);

                if (!(cmd.Parameters[31].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[31].Value);

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

        public BE.Academic.Transaction.StudentComplainCollections getStudentComplainForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Transaction.StudentComplainCollections dataColl = new BE.Academic.Transaction.StudentComplainCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentComplainForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentComplain beData = new BE.Academic.Transaction.StudentComplain();
                    beData.StudentId = reader.GetInt32(0);
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

        public BE.Academic.Transaction.StudentLeaveTakenCollections getStudentLeaveTakenForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Transaction.StudentLeaveTakenCollections dataColl = new BE.Academic.Transaction.StudentLeaveTakenCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentLeaveTakenForQA";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentLeaveTaken beData = new BE.Academic.Transaction.StudentLeaveTaken();
                    beData.StudentId = reader.GetInt32(0);
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

        public BE.Academic.Transaction.StudentAttachmentForQACollections getStudentAttForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Transaction.StudentAttachmentForQACollections dataColl = new BE.Academic.Transaction.StudentAttachmentForQACollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentAttachmentForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentAttachmentForQA beData = new BE.Academic.Transaction.StudentAttachmentForQA();
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
        public BE.Academic.Transaction.StudentAttendanceCollections getStudentAttendance(int UserId, int StudentId, int AcademicYearId)
        {
            BE.Academic.Transaction.StudentAttendanceCollections dataColl = new BE.Academic.Transaction.StudentAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.StudentAttendance beData = new BE.Academic.Transaction.StudentAttendance();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.YearId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.MonthId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.MonthName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Day1 = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Day2 = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Day3 = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Day4 = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Day5 = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Day6 = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Day7 = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Day8 = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Day9 = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Day10 = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Day11 = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Day12 = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Day13 = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Day14 = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Day15 = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Day16 = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Day17 = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Day18 = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Day19 = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Day20 = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Day21 = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Day22 = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.Day23 = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.Day24 = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.Day25 = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.Day26 = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.Day27 = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Day28 = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.Day29 = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.Day30 = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.Day31 = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.Day32 = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.TotalDays = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.TotalWeekEnd = reader.GetInt32(37);
                    if (!(reader[38] is DBNull)) beData.TotalHoliday = reader.GetInt32(38);
                    if (!(reader[39] is DBNull)) beData.TotalPresent = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.TotalLeave = reader.GetInt32(40);
                    if (!(reader[41] is DBNull)) beData.TotalAbsent = reader.GetInt32(41);
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
        public ResponeValues UpgradeClassSection(BE.Academic.Transaction.Student beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpgradeStudentClassSection";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;

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

        public ResponeValues UpdateStudentDocument(BE.Academic.Transaction.Student beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentDocument";
            }
            else
            {
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
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

        public AcademicLib.RE.Academic.StudentPhotoCollections getAllStudentPhotoDownloadLog(int UserId, int EntityId)
        {
            AcademicLib.RE.Academic.StudentPhotoCollections dataColl = new RE.Academic.StudentPhotoCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentPhotoDownloadLog";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentPhoto beData = new AcademicLib.RE.Academic.StudentPhoto();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.DownloadDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SectionId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.DownloadById = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.DownloadMiti = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.DownloaderName = reader.GetString(8);
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
