using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{
    internal class StudentInfirmaryDB
    {
        DataAccessLayer1 dal = null;
        public StudentInfirmaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicERP.BE.StudentDetForInfirmary getStudentForInfirmaryById(int UserId, int EntityId, int StudentId)
        {
            AcademicERP.BE.StudentDetForInfirmary beData = new AcademicERP.BE.StudentDetForInfirmary();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentDetailsForInfirmary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicERP.BE.StudentDetForInfirmary();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StudentTypeId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.F_ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.GuardianName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.G_ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PhotoPath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.RegNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.G_Email = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.F_Email = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.MotherName = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.M_Contact = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.M_Email = reader.GetString(18);
		            //Prashant AddCode 14 mangsir
                    if (!(reader[19] is DBNull)) beData.DOB_AD = reader.GetDateTime(19);
                    if (!(reader[20] is DBNull)) beData.DOB_BS = reader.GetString(20);

                    //Added by Suresh for colege
                    if (!(reader[21] is DBNull)) beData.Batch = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ClassYear = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Semester = reader.GetString(23);

                    if (!(reader[24] is DBNull)) beData.BloodGroup = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Height = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.Weigth = reader.GetString(26);
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


        public BE.MedicalProductsCollections getAllMedicalProduct(int UserId, int EntityId)
        {
            BE.MedicalProductsCollections dataColl = new BE.MedicalProductsCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllMedicalProduct";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.MedicalProducts beData = new BE.MedicalProducts();
                    if (!(reader[0] is DBNull)) beData.ProductId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
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



        public ResponeValues SaveUpdatePMHistory(BE.StudentPastMedicalHistory beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@HealthIssueId", beData.HealthIssueId);
            cmd.Parameters.AddWithValue("@ObservedDate", beData.ObservedDate);
            cmd.Parameters.AddWithValue("@Details", beData.Details);
            cmd.Parameters.AddWithValue("@Prescription", beData.Prescription);
            cmd.Parameters.AddWithValue("@MedicineTaken", beData.MedicineTaken);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentPastMedicalHistory";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentPastMedicalHistory";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveStudentPastMedicineDetDetails(beData.CUserId, resVal.RId, beData.StudentPastMedicineDetColl);
                    SaveStudentPastMedicalDocumentsDetails(beData.StudentPastMedicalDocumentsColl, resVal.RId, beData.CUserId);
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


        private void SaveStudentPastMedicineDetDetails(int UserId, int TranId, BE.StudentPastMedicineDetCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.StudentPastMedicineDet beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicineId", beData.MedicineId);
                cmd.Parameters.AddWithValue("@NoOfDose", beData.NoOfDose);
                cmd.Parameters.AddWithValue("@Duration", beData.Duration);
                cmd.Parameters.AddWithValue("@NoOfDays", beData.NoOfDays);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddStudentPastMedicineDetDetails";
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveStudentPastMedicalDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
                if (!string.IsNullOrEmpty(beData.DocPath))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@docDescription", beData.Description);
                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Document", beData.Data);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_AddStudentPastMedicalDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }

        }


        public BE.StudentPastMedicalHistoryCollections getAllStudentPastMedicalHistory(int UserId, int EntityId)
        {
            BE.StudentPastMedicalHistoryCollections dataColl = new BE.StudentPastMedicalHistoryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentPastMedicalHistory";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentPastMedicalHistory beData = new BE.StudentPastMedicalHistory();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssue = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ObservedDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Details = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Prescription = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MedicineTaken = Convert.ToBoolean(reader[6]);
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



        public BE.StudentPastMedicalHistory getStudentPastMedicalHistoryById(int UserId, int EntityId, int TranId)
        {
            BE.StudentPastMedicalHistory beData = new BE.StudentPastMedicalHistory();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentPastMedicalHistoryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.StudentPastMedicalHistory();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssueId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ObservedDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Details = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Prescription = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MedicineTaken = Convert.ToBoolean(reader[6]);
                }
                reader.NextResult();
                beData.StudentPastMedicineDetColl = new BE.StudentPastMedicineDetCollections();
                while (reader.Read())
                {
                    BE.StudentPastMedicineDet det1 = new BE.StudentPastMedicineDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    beData.StudentPastMedicineDetColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    beData.StudentPastMedicalDocumentsColl.Add(det);

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


        public ResponeValues DeleteStudentPastMedicalHistoryById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentPastMedicalHistoryById";
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


        //For Health Issue Tab code starts
        public ResponeValues SaveUpdateHealthIssue(BE.StudentHealthIssue beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@ObservedDate", beData.ObservedDate);
            cmd.Parameters.AddWithValue("@ObservedTime", beData.ObservedTime);
            cmd.Parameters.AddWithValue("@ObservedAt", beData.ObservedAt);
            cmd.Parameters.AddWithValue("@HealthIssueId", beData.HealthIssueId);
            cmd.Parameters.AddWithValue("@IsAdmitted", beData.IsAdmitted);
            cmd.Parameters.AddWithValue("@AdmittedDate", beData.AdmittedDate);
            cmd.Parameters.AddWithValue("@AdmittedAt", beData.AdmittedAt);
            cmd.Parameters.AddWithValue("@DischargedDate", beData.DischargedDate);
            cmd.Parameters.AddWithValue("@MedicineGiven", beData.MedicineGiven);
            cmd.Parameters.AddWithValue("@Prescription", beData.Prescription);
            cmd.Parameters.AddWithValue("@PrescribedBy", beData.PrescribedBy);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentHealthIssue";
            }
            else
            {
                cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentHealthIssue";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[17].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveStudentMedicineGivenDetDetails(beData.CUserId, resVal.RId, beData.StudentMedicineGivenDetColl);
                    SaveStudentMedicalDocumentsDetails(beData.HealthIssueAttachmentColl, resVal.RId, beData.CUserId);
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

        private void SaveStudentMedicineGivenDetDetails(int UserId, int TranId, BE.StudentMedicineGivenDetCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.StudentMedicineGivenDet beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;               
                cmd.Parameters.AddWithValue("@MedicineId", beData.MedicineId);
                cmd.Parameters.AddWithValue("@NoOfDose", beData.NoOfDose);
                cmd.Parameters.AddWithValue("@Duration", beData.Duration);
                cmd.Parameters.AddWithValue("@NoOfDays", beData.NoOfDays);
                cmd.Parameters.AddWithValue("@Qty", beData.Qty);
                cmd.Parameters.AddWithValue("@Price", beData.Price);
                cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddStudentMedicineGivenDetDetails";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveStudentMedicalDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
                if (!string.IsNullOrEmpty(beData.DocPath))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@docDescription", beData.Description);
                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Document", beData.Data);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_AddStudentMedicalDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }

        }


        public BE.StudentHealthIssueCollections getAllStudentHealthIssue(int UserId, int EntityId)
        {
            BE.StudentHealthIssueCollections dataColl = new BE.StudentHealthIssueCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentHealthIssue";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentHealthIssue beData = new BE.StudentHealthIssue();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ObservedDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ObservedTime = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.ObservedAt = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.HealthIssueId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsAdmitted = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData.AdmittedDate = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData.AdmittedAt = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DischargedDate = Convert.ToDateTime(reader[9]);
                    if (!(reader[10] is DBNull)) beData.MedicineGiven = Convert.ToBoolean(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Prescription = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PrescribedBy = reader.GetInt32(12);

                    if (!(reader[13] is DBNull)) beData.ObservedMiti = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.AdmittedMiti = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.DischargedMiti = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.HealthIssueName = reader.GetString(16);
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

        public BE.StudentHealthIssue getStudentHealthIssueById(int UserId, int EntityId, int TranId)
        {
            BE.StudentHealthIssue beData = new BE.StudentHealthIssue();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentHealthIssueById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.StudentHealthIssue();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ObservedDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ObservedTime = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.ObservedAt = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.HealthIssueId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsAdmitted = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData.AdmittedDate = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData.AdmittedAt = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DischargedDate = Convert.ToDateTime(reader[9]);
                    if (!(reader[10] is DBNull)) beData.MedicineGiven = Convert.ToBoolean(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Prescription = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PrescribedBy = reader.GetInt32(12);
                }
                reader.NextResult();
                beData.StudentMedicineGivenDetColl = new BE.StudentMedicineGivenDetCollections();
                while (reader.Read())
                {
                    BE.StudentMedicineGivenDet det1 = new BE.StudentMedicineGivenDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.Qty = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det1.Price = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det1.Amount = Convert.ToDouble(reader[7]);
                    beData.StudentMedicineGivenDetColl.Add(det1);
        }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Id = reader.GetInt32(5);
                    beData.HealthIssueAttachmentColl.Add(det);
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


        public ResponeValues DeleteStudentHealthIssueById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentHealthIssueById";
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

        //Student Health Immunization Tab Code Starts
        public ResponeValues SaveUpdateStudentImmunization(BE.StudentHealthImmunization beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@HealthIssueId", beData.HealthIssueId);
            cmd.Parameters.AddWithValue("@VaccineId", beData.VaccineId);
            cmd.Parameters.AddWithValue("@VaccinatorId", beData.VaccinatorId);
            cmd.Parameters.AddWithValue("@VaccinationDate", beData.VaccinationDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentHealthImmunization";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentHealthImmunization";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveStudentImmunizationDocumentsDetails(beData.StudentImmunizationAttachmentColl, resVal.RId, beData.CUserId);
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

        private void SaveStudentImmunizationDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
                if (!string.IsNullOrEmpty(beData.DocPath))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@docDescription", beData.Description);
                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Document", beData.Data);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_AddStudentImmunizationDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }
        }

        public BE.StudentHealthImmunizationCollections getAllStudentHealthImmunization(int UserId, int EntityId)
        {
            BE.StudentHealthImmunizationCollections dataColl = new BE.StudentHealthImmunizationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentHealthImmunization";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentHealthImmunization beData = new BE.StudentHealthImmunization();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssueName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VaccineName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.VaccinatorId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.VaccinationDate = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.VaccineMiti = reader.GetString(7);
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

        public BE.StudentHealthImmunization getStudentHealthImmunizationById(int UserId, int EntityId, int TranId)
        {
            BE.StudentHealthImmunization beData = new BE.StudentHealthImmunization();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentHealthImmunizationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.StudentHealthImmunization();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssueId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.VaccineId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.VaccinatorId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.VaccinationDate = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    beData.StudentImmunizationAttachmentColl.Add(det);
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

        public ResponeValues DeleteStudentImmunizationById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentHealthImmunizationById";
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

       
        // Student Clinical Lab Evaluation Tab Starts
        public AcademicERP.BE.LabValueCollections getLabValueById(int UserId, int EntityId, int TestNameId)
        {
            AcademicERP.BE.LabValueCollections dataColl = new AcademicERP.BE.LabValueCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TestNameId", TestNameId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetLabValueById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.LabValue beData = new AcademicERP.BE.LabValue();
                    beData.TestNameId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DefaultValue = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NormalRange = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.LowerRange = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.UpperRange = reader.GetString(5);
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


        public ResponeValues SaveUpdateStudentCLEvaluation(BE.StudentClinicalLabEvaluation beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EvaluateDate", beData.EvaluateDate);
            cmd.Parameters.AddWithValue("@TestNameId", beData.TestNameId);
            cmd.Parameters.AddWithValue("@TestMethod", beData.TestMethod);
            cmd.Parameters.AddWithValue("@HealthIssueId", beData.HealthIssueId);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentClinicalLabEvaluation";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentClinicalLabEvaluation";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveStudentCLELabValueDetails(beData.CUserId, resVal.RId, beData.LabValueList);
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


        private void SaveStudentCLELabValueDetails(int UserId, int TranId, BE.StudentCLELabValueCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.StudentCLELabValue beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@Result", beData.Result);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.CommandText = "usp_AddStudentCLELabValueDetails";
                cmd.ExecuteNonQuery();
            }

        }


        public BE.StudentClinicalLabEvaluationCollections getAllStudentClinicalLabEvaluation(int UserId, int EntityId)
        {
            BE.StudentClinicalLabEvaluationCollections dataColl = new BE.StudentClinicalLabEvaluationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentClinicalLabEvaluation";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentClinicalLabEvaluation beData = new BE.StudentClinicalLabEvaluation();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EvaluateDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.EvaluateMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.TestName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.TestMethod = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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

        public BE.StudentClinicalLabEvaluation getStudentClinicalLabEvaluationById(int UserId, int EntityId, int TranId)
        {
            BE.StudentClinicalLabEvaluation beData = new BE.StudentClinicalLabEvaluation();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentClinicalLabEvaluationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.StudentClinicalLabEvaluation();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EvaluateDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.TestNameId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TestMethod = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.HealthIssueId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                }
                reader.NextResult();
                beData.LabValueList = new BE.StudentCLELabValueCollections();
                while (reader.Read())
                {
                    BE.StudentCLELabValue det1 = new BE.StudentCLELabValue();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det1.Result = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det1.Remarks = reader.GetString(3);
                    beData.LabValueList.Add(det1);
        
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

        public ResponeValues DeleteSLEvaluationById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentClinicalLabEvaluationById";
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

        //Student General Checkup STarts
        public ResponeValues UpdateStudentGeneralCheckup(int UserId, List<AcademicERP.BE.StudentGCheckup> DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsValid", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("StudentId", typeof(int));
                tableAllocation.Columns.Add("TestNameId", typeof(int));
                tableAllocation.Columns.Add("Value", typeof(float));
                tableAllocation.Columns.Add("Remarks", typeof(string));
                tableAllocation.Columns.Add("CheckupDate", typeof(DateTime));
                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["StudentId"] = v.StudentId;
                    row["TestNameId"] = v.TestNameId;
                    row["Value"] = v.Value;
                    row["Remarks"] = v.Remarks;
                    row["CheckupDate"] = v.CheckupDate;
                    tableAllocation.Rows.Add(row);
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@StudentGCheckupColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddStudentGeneralCheckup";
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);
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


        //Prashant Code
        
        public BE.StudentPastMedicalHistoryCollections getStudentMedicalHistoryById(int UserId, int EntityId, int StudentId)
        {
            BE.StudentPastMedicalHistoryCollections dataColl = new BE.StudentPastMedicalHistoryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentMedicalHistoryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentPastMedicalHistory beData1 = new BE.StudentPastMedicalHistory();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData1.HealthIssueId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData1.ObservedDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData1.Details = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData1.Prescription = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData1.MedicineTaken = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData1.StudentName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData1.HealthIssueName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData1.ObservedDateBS = reader.GetString(9);
                    dataColl.Add(beData1);
                }
                reader.NextResult();
                dataColl.StudentPastMedicineDetColl = new BE.StudentPastMedicineDetCollections();
                while (reader.Read())
                {
                    BE.StudentPastMedicineDet det1 = new BE.StudentPastMedicineDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.ProductName = reader.GetString(5);
                    dataColl.Find(p1 => p1.TranId == det1.TranId).StudentPastMedicineDetColl.Add(det1);
                    //dataColl.StudentPastMedicineDetColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Id = reader.GetInt32(5);
                    dataColl.Find(p1 => p1.TranId == det.Id).StudentPastMedicalDocumentsColl.Add(det);
                    //dataColl.StudentPastMedicalDocumentsColl.Add(det);

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
        
        public BE.StudentHealthIssueCollections getStudentMedicalIssuesById(int UserId, int EntityId, int StudentId)
        {
            BE.StudentHealthIssueCollections dataColl = new BE.StudentHealthIssueCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentHealthById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentHealthIssue beData1 = new BE.StudentHealthIssue();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData1.ObservedDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData1.ObservedTime = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData1.ObservedAt = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData1.HealthIssueId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData1.IsAdmitted = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData1.AdmittedDate = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData1.AdmittedAt = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData1.DischargedDate = Convert.ToDateTime(reader[9]);
                    if (!(reader[10] is DBNull)) beData1.MedicineGiven = Convert.ToBoolean(reader[10]);
                    if (!(reader[11] is DBNull)) beData1.Prescription = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData1.PrescribedBy = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData1.HealthIssueName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData1.PrescribedByName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData1.ObservedDateBS = reader.GetString(15);
                    dataColl.Add(beData1);
                }
                reader.NextResult();
                dataColl.StudentMedicineGivenDetColl = new BE.StudentMedicineGivenDetCollections();
                while (reader.Read())
                {
                    BE.StudentMedicineGivenDet det1 = new BE.StudentMedicineGivenDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.Qty = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det1.Price = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det1.Amount = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) det1.ProductName = reader.GetString(8);
                    dataColl.Find(p1 => p1.TranId == det1.TranId).StudentMedicineGivenDetColl.Add(det1);
                    //dataColl.StudentMedicineGivenDetColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Id = reader.GetInt32(5);
                    dataColl.Find(p1 => p1.TranId == det.Id).HealthIssueAttachmentColl.Add(det);
                    //dataColl.HealthIssueAttachmentColl.Add(det);

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

        public AcademicERP.BE.StudentGCheckupCollections GetStudentGeneralCheckup(int UserId, int EntityId, int StudentId)
        {
            AcademicERP.BE.StudentGCheckupCollections dataColl = new AcademicERP.BE.StudentGCheckupCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentGeneralCheckup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.StudentGCheckup beData1 = new AcademicERP.BE.StudentGCheckup();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData1.TestNameId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData1.TestName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData1.Value = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData1.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData1.CheckupDate = Convert.ToDateTime(reader[6]);
                    if (!(reader[7] is DBNull)) beData1.CheckupDateBS = reader.GetString(7);
                    dataColl.Add(beData1);
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

        public AcademicERP.BE.StudentHealthImmunizationCollections GetStudentHealthImmunization(int UserId, int EntityId, int StudentId)
        {
            AcademicERP.BE.StudentHealthImmunizationCollections dataColl = new AcademicERP.BE.StudentHealthImmunizationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentHealthImmunization";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.StudentHealthImmunization beData = new AcademicERP.BE.StudentHealthImmunization();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssueName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VaccineName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.VaccinatorId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.VaccinationDate = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.VaccineMiti = reader.GetString(7);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Id = reader.GetInt32(5);
                    dataColl.Find(p1 => p1.TranId == det.Id).StudentImmunizationAttachmentColl.Add(det);
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

        public Dynamic.BusinessEntity.GeneralDocumentCollections GetStudentHealthImmunizationDoc(int UserId, int EntityId, int TranId)
        {
            Dynamic.BusinessEntity.GeneralDocumentCollections dataColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentHealthImmunizationDoc";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Id = reader.GetInt32(5);
                    dataColl.Add(det);
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

        public BE.StudentClinicalLabEvaluationCollections getStudentClinicalLabEvaluation(int UserId, int EntityId, int StudentId)
        {
            BE.StudentClinicalLabEvaluationCollections dataColl = new BE.StudentClinicalLabEvaluationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_GetStudentClinicalLabEvaluation";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.StudentClinicalLabEvaluation beData = new BE.StudentClinicalLabEvaluation();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EvaluateDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.EvaluateMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.TestName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.TestMethod = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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