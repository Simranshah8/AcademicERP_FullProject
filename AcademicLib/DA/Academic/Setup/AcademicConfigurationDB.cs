using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Setup
{
    internal class AcademicConfigurationDB
    {
        DataAccessLayer1 dal = null;
        public AcademicConfigurationDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Academic.Setup.AcademicConfiguration beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActiveLevel", beData.ActiveLevel);
            cmd.Parameters.AddWithValue("@ActiveFaculty", beData.ActiveFaculty);
            cmd.Parameters.AddWithValue("@ActiveBatch", beData.ActiveBatch);
            cmd.Parameters.AddWithValue("@ActiveSemester", beData.ActiveSemester);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.CommandText = "usp_AddAcademicConfiguration";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ActiveClassYear", beData.ActiveClassYear); 
            cmd.Parameters.AddWithValue("@ActiveClassWiseMonth", beData.ActiveClassWiseMonth);
            cmd.Parameters.AddWithValue("@SectionWiseSetup", beData.SectionWiseSetup);
            cmd.Parameters.AddWithValue("@SectionWiseSubjectMapping", beData.SectionWiseSubjectMapping);
            cmd.Parameters.AddWithValue("@SectionWiseExamSchedule", beData.SectionWiseExamSchedule);
            cmd.Parameters.AddWithValue("@SectionWiseMarkSetup", beData.SectionWiseMarkSetup);
            cmd.Parameters.AddWithValue("@SectionWiseLessonPlan", beData.SectionWiseLessonPlan);
        
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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
        public BE.Academic.Setup.AcademicConfiguration getConfiguration(int UserId, int EntityId)
        {
            BE.Academic.Setup.AcademicConfiguration beData = new BE.Academic.Setup.AcademicConfiguration();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAcademicConfiguration";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Setup.AcademicConfiguration();
                    if (!(reader[0] is DBNull)) beData.ActiveLevel = reader.GetBoolean(0);
                    if (!(reader[1] is DBNull)) beData.ActiveFaculty = reader.GetBoolean(1);
                    if (!(reader[2] is DBNull)) beData.ActiveSemester = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.ActiveBatch = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.ActiveClassYear = reader.GetBoolean(4);                     
                    if (!(reader[5] is DBNull)) beData.ActiveClassWiseMonth = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.SectionWiseSetup = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.SectionWiseSubjectMapping = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.SectionWiseExamSchedule = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.SectionWiseMarkSetup = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.SectionWiseLessonPlan = reader.GetBoolean(10);
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
