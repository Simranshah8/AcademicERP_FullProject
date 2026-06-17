using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.HomeWork
{

	internal class ConfigurationDB
	{

		DataAccessLayer1 dal = null;
		public ConfigurationDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
        public ResponeValues SaveUpdate(int UserId, BE.HomeWork.Configuration beData, int? AcademicYearId, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HomeworkLesson", beData.HomeworkLesson);
            cmd.Parameters.AddWithValue("@HomeworkTopic", beData.HomeworkTopic);
            cmd.Parameters.AddWithValue("@AssignmentLesson", beData.AssignmentLesson);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHAConfiguration";
            }else
            {
                cmd.CommandText = "usp_AddHAConfiguration";
            }

            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

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
        public BE.HomeWork.Configuration getHAConfiguration(int UserId, int EntityId, int? BranchId)
        {
            BE.HomeWork.Configuration configuration = new BE.HomeWork.Configuration();
            this.dal.OpenConnection();
            System.Data.SqlClient.SqlCommand command = this.dal.GetCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@EntityId", EntityId);
            command.Parameters.AddWithValue("@BranchId", BranchId);
            command.CommandText = "usp_GetHAConfiguration";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    configuration = new BE.HomeWork.Configuration();
                    if (!(reader[0] is DBNull)) configuration.BranchId = new int?(reader.GetInt32(0));
                    if (!(reader[1] is DBNull)) configuration.AcademicYearId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) configuration.HomeworkLesson = Convert.ToBoolean(reader[2]);
                    if (!(reader[3] is DBNull)) configuration.HomeworkTopic = Convert.ToBoolean(reader[3]);
                    if (!(reader[2] is DBNull)) configuration.AssignmentLesson = Convert.ToBoolean(reader[4]);
                }
                reader.Close();
                configuration.IsSuccess = true;
                configuration.ResponseMSG = "Success";
            }
            catch (Exception ex)
            {
                configuration.IsSuccess = false;
                configuration.ResponseMSG = ex.Message;
            }
            finally
            {
                this.dal.CloseConnection();
            }
            return configuration;
        }


    }

}

