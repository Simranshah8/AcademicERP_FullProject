using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA
{
    internal class EmployeeInfirmaryDB
    {
        DataAccessLayer1 dal = null;
        public EmployeeInfirmaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public BE.EmployeeDetForInfirmary getEmployeeForInfirmaryById(int UserId, int? EntityId, int EmployeeId)
        {
            BE.EmployeeDetForInfirmary beData = new BE.EmployeeDetForInfirmary();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeDetailsForInfirmary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.EmployeeDetForInfirmary();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BranchName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Address = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.EmployeeCode = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Category = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.MotherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DOB_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DobAD = Convert.ToDateTime(reader[13]);
                    if (!(reader[14] is DBNull)) beData.SpouseName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.EmailId = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.OfficeEmailId = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PersnalContactNo = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.FatherContactNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.MotherContactNo = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.SpouseContactNo = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Age = reader.GetString(21);
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


        public ResponeValues SaveUpdatePMHistory(BE.EmployeePastMedicalHistory beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
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
                cmd.CommandText = "usp_UpdateEmployeePastMedicalHistory";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployeePastMedicalHistory";
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
                    SaveEmployeePastMedicineDetDetails(beData.CUserId, resVal.RId, beData.EmployeePastMedicineDetColl);
                    SaveEmployeePastMedicalDocumentsDetails(beData.EmployeePastMedicalDocumentsColl, resVal.RId, beData.CUserId);
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

        private void SaveEmployeePastMedicineDetDetails(int UserId, int TranId, BE.EmployeePastMedicineDetCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;
            foreach (BE.EmployeePastMedicineDet beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicineId", beData.MedicineId);
                cmd.Parameters.AddWithValue("@NoOfDose", beData.NoOfDose);
                cmd.Parameters.AddWithValue("@Duration", beData.Duration);
                cmd.Parameters.AddWithValue("@NoOfDays", beData.NoOfDays);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddEmployeePastMedicineDetDetails";
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveEmployeePastMedicalDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
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
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.CommandText = "usp_AddEmployeePastMedicalDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }
        }


        public BE.EmployeePastMedicalHistoryCollections getEmployeeMedicalHistoryById(int UserId, int? EntityId, int EmployeeId)
        {
            BE.EmployeePastMedicalHistoryCollections dataColl = new BE.EmployeePastMedicalHistoryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeMedicalHistoryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.EmployeePastMedicalHistory beData1 = new BE.EmployeePastMedicalHistory();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData1.HealthIssueId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData1.ObservedDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData1.Details = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData1.Prescription = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData1.MedicineTaken = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData1.EmployeeName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData1.HealthIssueName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData1.ObservedDateBS = reader.GetString(9);
                    dataColl.Add(beData1);
                }
                reader.NextResult();
                dataColl.EmployeePastMedicineDetColl = new BE.EmployeePastMedicineDetCollections();
                while (reader.Read())
                {
                    BE.EmployeePastMedicineDet det1 = new BE.EmployeePastMedicineDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.ProductName = reader.GetString(5);
                    dataColl.Find(p1 => p1.TranId == det1.TranId).EmployeePastMedicineDetColl.Add(det1);
                    //dataColl.EmployeePastMedicineDetColl.Add(det1);
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
                    dataColl.Find(p1 => p1.TranId == det.Id).EmployeePastMedicalDocumentsColl.Add(det);
                    //dataColl.EmployeePastMedicalDocumentsColl.Add(det);

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

        public BE.EmployeePastMedicalHistory getEmployeePastMedicalHistoryById(int UserId, int? EntityId, int TranId)
        {
            BE.EmployeePastMedicalHistory beData = new BE.EmployeePastMedicalHistory();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeePastMedicalHistoryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.EmployeePastMedicalHistory();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HealthIssueId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ObservedDate = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Details = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Prescription = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MedicineTaken = Convert.ToBoolean(reader[6]);
                }
                reader.NextResult();
                beData.EmployeePastMedicineDetColl = new BE.EmployeePastMedicineDetCollections();
                while (reader.Read())
                {
                    BE.EmployeePastMedicineDet det1 = new BE.EmployeePastMedicineDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    beData.EmployeePastMedicineDetColl.Add(det1);
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
                    beData.EmployeePastMedicalDocumentsColl.Add(det);

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

        public ResponeValues DeleteEmployeePastMedicalHistoryById(int UserId, int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelEmployeePastMedicalHistoryById";
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

        public BE.EmployeeHealthIssueCollections getEmployeeMedicalIssuesById(int UserId, int? EntityId, int EmployeeId)
        {
            BE.EmployeeHealthIssueCollections dataColl = new BE.EmployeeHealthIssueCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeHealthById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.EmployeeHealthIssue beData1 = new BE.EmployeeHealthIssue();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.EmployeeId = reader.GetInt32(1);
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
                dataColl.EmployeeMedicineGivenDetColl = new BE.EmployeeMedicineGivenDetCollections();
                while (reader.Read())
                {
                    BE.EmployeeMedicineGivenDet det1 = new BE.EmployeeMedicineGivenDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.Qty = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det1.Price = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det1.Amount = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) det1.ProductName = reader.GetString(8);
                    dataColl.Find(p1 => p1.TranId == det1.TranId).EmployeeMedicineGivenDetColl.Add(det1);
                    //dataColl.EmployeePastMedicineDetColl.Add(det1);
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
                    //dataColl.EmployeePastMedicalDocumentsColl.Add(det);

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

        public ResponeValues SaveUpdateHealthIssue(BE.EmployeeHealthIssue beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
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
                cmd.CommandText = "usp_UpdateEmployeeHealthIssue";
            }
            else
            {
                cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployeeHealthIssue";
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
                    SaveEmployeeMedicineGivenDetDetails(beData.CUserId, resVal.RId, beData.EmployeeMedicineGivenDetColl);
                    SaveEmployeeMedicalDocumentsDetails(beData.HealthIssueAttachmentColl, resVal.RId, beData.CUserId);
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

        private void SaveEmployeeMedicineGivenDetDetails(int UserId, int TranId, BE.EmployeeMedicineGivenDetCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.EmployeeMedicineGivenDet beData in beDataColl)
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
                cmd.CommandText = "usp_AddEmployeeMedicineGivenDetDetails";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveEmployeeMedicalDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
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
                    cmd.CommandText = "usp_AddEmployeeMedicalDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }

        }
        public BE.EmployeeHealthIssue getEmployeeHealthIssueById(int UserId, int? EntityId, int TranId)
        {
            BE.EmployeeHealthIssue beData = new BE.EmployeeHealthIssue();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeHealthIssueById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.EmployeeHealthIssue();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
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
                beData.EmployeeMedicineGivenDetColl = new BE.EmployeeMedicineGivenDetCollections();
                while (reader.Read())
                {
                    BE.EmployeeMedicineGivenDet det1 = new BE.EmployeeMedicineGivenDet();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.MedicineId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.NoOfDose = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Duration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.NoOfDays = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.Qty = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det1.Price = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det1.Amount = Convert.ToDouble(reader[7]);
                    beData.EmployeeMedicineGivenDetColl.Add(det1);
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

        public ResponeValues DeleteEmployeeHealthIssue(int UserId, int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelEmployeeHealthIssueById";
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

        //Employee General Checkup STarts
        public ResponeValues UpdateEmployeeGeneralCheckup(int UserId, List<BE.EmployeeGCheckup> DataColl)
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
                tableAllocation.Columns.Add("EmployeeId", typeof(int));
                tableAllocation.Columns.Add("TestNameId", typeof(int));
                tableAllocation.Columns.Add("Value", typeof(float));
                tableAllocation.Columns.Add("Remarks", typeof(string));
                tableAllocation.Columns.Add("CheckupDate", typeof(DateTime));
                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["EmployeeId"] = v.EmployeeId;
                    row["TestNameId"] = v.TestNameId;
                    row["Value"] = v.Value;
                    row["Remarks"] = v.Remarks;
                    row["CheckupDate"] = v.CheckupDate;
                    tableAllocation.Rows.Add(row);
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@EmployeeGCheckupColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddEmployeeGeneralCheckup";
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

        public BE.EmployeeGCheckupCollections GetEmployeeGeneralCheckup(int UserId, int EntityId, int EmployeeId)
        {
            BE.EmployeeGCheckupCollections dataColl = new BE.EmployeeGCheckupCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeGeneralCheckup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.EmployeeGCheckup beData1 = new BE.EmployeeGCheckup();
                    if (!(reader[0] is DBNull)) beData1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData1.EmployeeId = reader.GetInt32(1);
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

        //Employee Health Immunization Tab Code Starts
        public ResponeValues SaveUpdateEmployeeImmunization(BE.EmployeeHealthImmunization beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
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
                cmd.CommandText = "usp_UpdateEmployeeHealthImmunization";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployeeHealthImmunization";
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
                    SaveEmployeeImmunizationDocumentsDetails(beData.EmployeeImmunizationAttachmentColl, resVal.RId, beData.CUserId);
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

        private void SaveEmployeeImmunizationDocumentsDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
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
                    cmd.CommandText = "usp_AddEmployeeImmunizationDocumentsDetails";
                    cmd.ExecuteNonQuery();
                }
        }

        public BE.EmployeeHealthImmunizationCollections GetEmployeeHealthImmunization(int UserId, int EntityId, int EmployeeId)
        {
            BE.EmployeeHealthImmunizationCollections dataColl = new BE.EmployeeHealthImmunizationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeHealthImmunization";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.EmployeeHealthImmunization beData = new BE.EmployeeHealthImmunization();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
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
                    dataColl.Find(p1 => p1.TranId == det.Id).EmployeeImmunizationAttachmentColl.Add(det);
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


        public BE.EmployeeHealthImmunization getEmployeeHealthImmunizationById(int UserId, int EntityId, int TranId)
        {
            BE.EmployeeHealthImmunization beData = new BE.EmployeeHealthImmunization();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeHealthImmunizationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.EmployeeHealthImmunization();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
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
                    beData.EmployeeImmunizationAttachmentColl.Add(det);
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

        public Dynamic.BusinessEntity.GeneralDocumentCollections GetEmployeeHealthImmunizationDoc(int UserId, int EntityId, int TranId)
        {
            Dynamic.BusinessEntity.GeneralDocumentCollections dataColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeHealthImmunizationDoc";
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

        public ResponeValues DeleteEmployeeImmunizationById(int UserId, int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelEmployeeHealthImmunizationById";
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
        //Clinical Lab Evaluation 
        public ResponeValues SaveUpdateEmployeeCLEvaluation(BE.EmployeeClinicalLabEvaluation beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
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
                cmd.CommandText = "usp_UpdateEmployeeClinicalLabEvaluation";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployeeClinicalLabEvaluation";
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
                    SaveEmployeeCLELabValueDetails(beData.CUserId, resVal.RId, beData.LabValueList);
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

        private void SaveEmployeeCLELabValueDetails(int UserId, int TranId, BE.EmployeeCLELabValueCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.EmployeeCLELabValue beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@Result", beData.Result);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.CommandText = "usp_AddEmployeeCLELabValueDetails";
                cmd.ExecuteNonQuery();
            }
        }

        public BE.EmployeeClinicalLabEvaluationCollections getEmployeeClinicalLabEvaluation(int UserId, int? EntityId, int EmployeeId)
        {
            BE.EmployeeClinicalLabEvaluationCollections dataColl = new BE.EmployeeClinicalLabEvaluationCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetEmployeeClinicalLabEvaluation";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.EmployeeClinicalLabEvaluation beData = new BE.EmployeeClinicalLabEvaluation();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
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


        public BE.EmployeeClinicalLabEvaluation getEmployeeClinicalLabEvaluationById(int UserId, int? EntityId, int TranId)
        {
            BE.EmployeeClinicalLabEvaluation beData = new BE.EmployeeClinicalLabEvaluation();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeClinicalLabEvaluationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.EmployeeClinicalLabEvaluation();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EvaluateDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.TestNameId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TestMethod = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.HealthIssueId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                }
                reader.NextResult();
                beData.LabValueList = new BE.EmployeeCLELabValueCollections();
                while (reader.Read())
                {
                    BE.EmployeeCLELabValue det1 = new BE.EmployeeCLELabValue();
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

        public ResponeValues DeleteSLEvaluationById(int UserId, int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelEmployeeClinicalLabEvaluationById";
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

    }
}