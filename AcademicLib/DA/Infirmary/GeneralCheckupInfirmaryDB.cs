using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{
    internal class GeneralCheckupInfirmaryDB
    {
        DataAccessLayer1 dal = null;
        public GeneralCheckupInfirmaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public AcademicERP.BE.StudentForGCInfirmaryCollections getStudentForGCInfirmary(int UserId, int EntityId, int? ClassId)
        {
            AcademicERP.BE.StudentForGCInfirmaryCollections dataColl = new AcademicERP.BE.StudentForGCInfirmaryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetStudentForGeneralCheckup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.StudentForGCInfirmary beData = new AcademicERP.BE.StudentForGCInfirmary();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestNameId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.StudentName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.OrderNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.TestName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassId = reader.GetInt32(7);
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

        public ResponeValues SaveUpdate(BE.GeneralCheckupInfirmary beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HealthCampaignId", beData.HealthCampaignId);
            cmd.Parameters.AddWithValue("@CheckupDate", beData.CheckupDate);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
            cmd.Parameters.AddWithValue("@ExaminerId", beData.ExaminerId);
            cmd.Parameters.AddWithValue("@IsVaccination", beData.IsVaccination);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGeneralCheckupInfirmary";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddGeneralCheckupInfirmary";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

            //Add the table allocation here
            System.Data.DataTable tableAllocation = new System.Data.DataTable();
            tableAllocation.Columns.Add("StudentId", typeof(int));
            tableAllocation.Columns.Add("TestNameId", typeof(int));
            tableAllocation.Columns.Add("Value", typeof(string));
            foreach (var v in beData.GeneralCheckupTestValueColl)
            {
                var row = tableAllocation.NewRow();
                row["StudentId"] = v.StudentId;
                row["TestNameId"] = v.TestNameId;
                row["Value"] = v.Value;
                tableAllocation.Rows.Add(row);
            }
            //For Vaccination
            System.Data.DataTable tableVaccination = new System.Data.DataTable();
            tableVaccination.Columns.Add("StudentId", typeof(int));
            tableVaccination.Columns.Add("TestNameId", typeof(int));
            tableVaccination.Columns.Add("Remarks", typeof(string));
            tableVaccination.Columns.Add("IsAllow", typeof(bool));
            foreach (var av in beData.GeneralCheckupVaccinationColl)
            {
                var row = tableVaccination.NewRow();
                row["StudentId"] = av.StudentId;
                row["TestNameId"] = av.TestNameId;
                row["Remarks"] = av.Remarks;
                row["IsAllow"] = av.IsAllow;
                tableVaccination.Rows.Add(row);
            }

            System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@StudentTestValueColl", tableAllocation);
            sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

            System.Data.SqlClient.SqlParameter sqlParam2 = cmd.Parameters.AddWithValue("@StudentForVaccinationColl", tableVaccination);
            sqlParam2.SqlDbType = System.Data.SqlDbType.Structured;

            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

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

        public BE.GeneralCheckupInfirmaryCollections getAllGeneralCheckupInfirmary(int UserId, int EntityId)
        {
            BE.GeneralCheckupInfirmaryCollections dataColl = new BE.GeneralCheckupInfirmaryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllGeneralCheckupInfirmary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.GeneralCheckupInfirmary beData = new BE.GeneralCheckupInfirmary();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HealthCampaignId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.CheckupDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ExaminerId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.IsVaccination = Convert.ToBoolean(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ExaminerName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.HealthCampaignName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ClassName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Month = reader.GetString(10);
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


        public BE.GeneralCheckupInfirmary getGeneralCheckupInfirmaryById(int UserId, int EntityId, int TranId)
        {
            BE.GeneralCheckupInfirmary beData = new BE.GeneralCheckupInfirmary();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGeneralCheckupInfirmaryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.GeneralCheckupInfirmary();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HealthCampaignId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.CheckupDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ExaminerId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.IsVaccination = Convert.ToBoolean(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.MonthId = reader.GetInt32(7);
                }
                reader.NextResult();
                beData.GeneralCheckupTestValueColl = new BE.GeneralCheckupTestValueCollections();
                while (reader.Read())
                {
                    BE.GeneralCheckupTestValue det1 = new BE.GeneralCheckupTestValue();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.TestNameId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Value = reader.GetString(3);
                    beData.GeneralCheckupTestValueColl.Add(det1);
                }
                reader.NextResult();
                beData.GeneralCheckupVaccinationColl = new BE.GeneralCheckupVaccinationCollections();
                while (reader.Read())
                {
                    BE.GeneralCheckupVaccination det1 = new BE.GeneralCheckupVaccination();
                    if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.TestNameId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.Remarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det1.IsAllow = Convert.ToBoolean(reader[4]);
                    beData.GeneralCheckupVaccinationColl.Add(det1);
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
            cmd.CommandText = "usp_DelGeneralCheckupInfirmaryById";
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

        public BE.GeneralCheckUpData getDataForGeneralCheckupInfirmary(int UserId, int EntityId, int GeneralCheckUpId)
        {
            BE.GeneralCheckUpData beData = new BE.GeneralCheckUpData();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGeneralHealthCampaignDatabyId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.GeneralCheckUpData();
                    if (!(reader[0] is DBNull)) beData.GeneralCheckUpId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Vaccination = Convert.ToBoolean(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Month = reader.GetInt32(2);
                }
                reader.NextResult();
                beData.GeneralCheckupClassColl = new BE.GeneralCheckUpClassCollection();
                while (reader.Read())
                {
                    BE.GeneralCheckUpClass det1 = new BE.GeneralCheckUpClass();
                    if (!(reader[0] is DBNull)) det1.GeneralCheckUpId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
                    beData.GeneralCheckupClassColl.Add(det1);
                }
                reader.NextResult();
                beData.GeneralHealthExaminerColl = new BE.GeneralExaminerCollection();
                while (reader.Read())
                {
                    BE.GeneralCheckUpExaminer det1 = new BE.GeneralCheckUpExaminer();
                    if (!(reader[0] is DBNull)) det1.GeneralCheckUpId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.ExaminerId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);                   
                    beData.GeneralHealthExaminerColl.Add(det1);
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
        public BE.GeneralCheckupInfirmary getGeneralCheckupForDetailById(int UserId, int EntityId, int TranId)
        {
            BE.GeneralCheckupInfirmary beData = new BE.GeneralCheckupInfirmary();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGeneralCheckupDetailById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.GeneralCheckupInfirmary();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HealthCampaignName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.CheckupDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.CheckupMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ExaminerName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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
    }
}

