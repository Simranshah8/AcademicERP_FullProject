using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicLib.BE.Infirmary;
using Dynamic.DataAccess.Global;
using Dynamic.BusinessEntity.Account;
using System.Web;
namespace AcademicLib.DA.Infirmary
{
    public class HealthCheckupGroupDB
    {
        DataAccessLayer1 dal = null;
        public HealthCheckupGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues AddModifyHealthCheckupGroup(HealthCheckupGroup healthCheckupGroup, int UserId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", healthCheckupGroup.GroupId);
            cmd.Parameters.AddWithValue("@GroupName", healthCheckupGroup.GroupName);
            cmd.Parameters.AddWithValue("@Description", healthCheckupGroup.Description);
            cmd.Parameters.AddWithValue("@StudentInfirmaryOrderNo", healthCheckupGroup.StudentInfirmaryOrderNo);
            cmd.Parameters.AddWithValue("@EmployeeInfirmaryOrderNo", healthCheckupGroup.EmployeeInfirmaryOrderNo);
            cmd.Parameters.AddWithValue("@ModifyBy", UserId);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);
            cmd.CommandText = "usp_InfirmaryAddModifyHealthCheckupGroup";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues DeleteHealthCheckupGroup(int healthExaminerID)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", healthExaminerID);
            cmd.CommandText = "usp_InfirmaryDeleteHealthCheckupGroup";
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthCheckupGroupCollections GetAllHealthCheckupGroup(int PerPage, int PageNo, string SearchText)
        {
            HealthCheckupGroupCollections resVal = new HealthCheckupGroupCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PerPage", PerPage);
            cmd.Parameters.AddWithValue("@PageNo", PageNo);
            cmd.Parameters.AddWithValue("@SearchText", SearchText);
            cmd.CommandText = "usp_InfirmaryGetAllHealtCheckupGroup";
            cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HealthCheckupGroup beData = new HealthCheckupGroup();
                    if (!(reader[1] is DBNull)) beData.GroupId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.GroupName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StudentInfirmaryOrderNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.EmployeeInfirmaryOrderNo = reader.GetInt32(5);
                    resVal.Add(beData);
                }
                reader.Close();
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.TotalRows = Convert.ToInt32(cmd.Parameters[3].Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthCheckupGroup GetHealthCheckupGroup(int GroupID)
        {
            HealthCheckupGroup resVal = new HealthCheckupGroup();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", GroupID);
            cmd.CommandText = "usp_InfirmaryGetHealthCheckupGroupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader[0] is DBNull)) resVal.GroupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) resVal.GroupName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) resVal.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) resVal.StudentInfirmaryOrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) resVal.EmployeeInfirmaryOrderNo = reader.GetInt32(4);
                }
                reader.Close();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public Select2DataCollections GetCheckupGroup(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetCheckupGroup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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
        public ResponeValues AddModifyHealthTestName(HealthCheckupTest healthCheckupTest, int UserId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TestNameId", healthCheckupTest.TestNameId);
            cmd.Parameters.AddWithValue("@TestName", healthCheckupTest.TestName);
            cmd.Parameters.AddWithValue("@CheckupGroupId", healthCheckupTest.CheckupGroupId);
            cmd.Parameters.AddWithValue("@OrderNo", healthCheckupTest.OrderNo);
            cmd.Parameters.AddWithValue("@InputTextType", healthCheckupTest.InputTextType);
            cmd.Parameters.AddWithValue("@SampleCollection", healthCheckupTest.SampleCollection);
            cmd.Parameters.AddWithValue("@SampleUnitOrVolume", healthCheckupTest.SampleUnitOrVolume);
            cmd.Parameters.AddWithValue("@Description", healthCheckupTest.Description);
            cmd.Parameters.AddWithValue("@ModifyBy", UserId);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);
            cmd.CommandText = "usp_InfirmaryAddModifyTest";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues AddModifyLabParameters(HealthCheckupLabParameters healthCheckupLabParameters, int TestId, int UserId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ParameterId", healthCheckupLabParameters.ParameterId);
            cmd.Parameters.AddWithValue("@ParameterName", healthCheckupLabParameters.ParameterName);
            cmd.Parameters.AddWithValue("@DefaultValue", healthCheckupLabParameters.DefaultValue);
            cmd.Parameters.AddWithValue("@NormalRange", healthCheckupLabParameters.NormalRange);
            cmd.Parameters.AddWithValue("@LowerRange", healthCheckupLabParameters.LowerRange);
            cmd.Parameters.AddWithValue("@UpperRange", healthCheckupLabParameters.UpperRange);
            cmd.Parameters.AddWithValue("@GroupName", healthCheckupLabParameters.GroupName);
            cmd.Parameters.AddWithValue("@Remarks", healthCheckupLabParameters.Remarks);
            cmd.Parameters.AddWithValue("@TestId", TestId);
            cmd.Parameters.AddWithValue("@ModifyBy", UserId);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);
            cmd.CommandText = "usp_InfirmaryAddModifyLabParameters";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues DeleteLabParameters(int ParameterId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ParameterId", ParameterId);
            cmd.CommandText = "usp_InfirmaryDeleteLabParameters";
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues DeleteTest(int TestNameId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TestNameId", TestNameId);
            cmd.CommandText = "usp_InfirmaryDeleteTest";
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
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthCheckupTestCollections GetAllHealthTest(int PerPage, int PageNo, string SearchText)
        {
            HealthCheckupTestCollections resVal = new HealthCheckupTestCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PerPage", PerPage);
            cmd.Parameters.AddWithValue("@PageNo", PageNo);
            cmd.Parameters.AddWithValue("@SearchText", SearchText);
            cmd.CommandText = "usp_InfirmaryGetAllTest";
            cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HealthCheckupTest beData = new HealthCheckupTest();
                    if (!(reader[1] is DBNull)) beData.TestNameId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TestName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CheckupGroupId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.CheckupGroupText = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.OrderNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.InputTextType = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SampleCollection = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SampleUnitOrVolume = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Description = reader.GetString(9);
                    resVal.Add(beData);
                }
                reader.Close();
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.TotalRows = Convert.ToInt32(cmd.Parameters[3].Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthCheckupTest GetHealthTest(int TestNameId)
        {
            HealthCheckupTest resVal = new HealthCheckupTest();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd1 = dal.GetCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@TestNameId", TestNameId);
            cmd1.CommandText = "usp_InfirmaryGetTestById";
            System.Data.SqlClient.SqlCommand cmd2 = dal.GetCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@TestNameId", TestNameId);
            cmd2.CommandText = "usp_InfirmaryGetLabParametersByTestNameId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    if (!(reader1[0] is DBNull)) resVal.TestNameId = reader1.GetInt32(0);
                    if (!(reader1[1] is DBNull)) resVal.TestName = reader1.GetString(1);
                    if (!(reader1[2] is DBNull)) resVal.CheckupGroupId = reader1.GetInt32(2);
                    if (!(reader1[3] is DBNull)) resVal.CheckupGroupText = reader1.GetString(3);
                    if (!(reader1[4] is DBNull)) resVal.OrderNo = reader1.GetInt32(4);
                    if (!(reader1[5] is DBNull)) resVal.InputTextType = reader1.GetString(5);
                    if (!(reader1[6] is DBNull)) resVal.SampleCollection = reader1.GetString(6);
                    if (!(reader1[7] is DBNull)) resVal.SampleUnitOrVolume = reader1.GetString(7);
                    if (!(reader1[8] is DBNull)) resVal.Description = reader1.GetString(8);
                }
                resVal.LabParameters = new HealthCheckupLabParametersCollections();
                reader1.Close();
                System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    HealthCheckupLabParameters hed = new HealthCheckupLabParameters();
                    if (!(reader2[0] is DBNull)) hed.ParameterId = reader2.GetInt32(0);
                    if (!(reader2[1] is DBNull)) hed.ParameterName = reader2.GetString(1);
                    if (!(reader2[2] is DBNull)) hed.DefaultValue = reader2.GetString(2);
                    if (!(reader2[3] is DBNull)) hed.NormalRange = reader2.GetString(3);
                    if (!(reader2[4] is DBNull)) hed.LowerRange = reader2.GetString(4);
                    if (!(reader2[5] is DBNull)) hed.UpperRange = reader2.GetString(5);
                    if (!(reader2[6] is DBNull)) hed.GroupName = reader2.GetString(6);
                    if (!(reader2[7] is DBNull)) hed.Remarks = reader2.GetString(7);
                    if (!(reader2[8] is DBNull)) hed.TestId = reader2.GetInt32(8);
                    hed.Mode = "old";
                    resVal.LabParameters.Add(hed);
                }
                reader2.Close();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public int GetOrderNo(int Type)
        {
            int response = -1;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.CommandText = "usp_InfirmaryGetCheckupGroupOrderNo";
            cmd.Parameters.Add("@OrderNo", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    response = Convert.ToInt32(cmd.Parameters[1].Value);
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return response;
        }
    }
}