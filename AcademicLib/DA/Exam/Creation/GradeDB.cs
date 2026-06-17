using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Creation
{
    internal class GradeDB
    {
        DataAccessLayer1 dal = null;
        public GradeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Exam.Creation.Grade beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@MinPer", beData.MinPer);
            cmd.Parameters.AddWithValue("@MaxPer", beData.MaxPer);
            cmd.Parameters.AddWithValue("@GradePoint", beData.GradePoint);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@GradeId", beData.GradeId);
      


            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGrade";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddGrade";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@SubjectType", (beData.SubjectType==0 ? 1 : beData.SubjectType) );
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

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

        public BE.Exam.Creation.GradeCollections getAllGrade(int UserId, int EntityId)
        {
            BE.Exam.Creation.GradeCollections dataColl = new BE.Exam.Creation.GradeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllGrade";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Creation.Grade beData = new BE.Exam.Creation.Grade();
                    beData.GradeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.MinPer = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.MaxPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.GradePoint = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.SubjectType = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) beData.ClassId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.ClassName = Convert.ToString(reader[8]);
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
        public BE.Exam.Creation.Grade getGradeById(int UserId, int EntityId, int GradeId)
        {
            BE.Exam.Creation.Grade beData = new BE.Exam.Creation.Grade();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GradeId", GradeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGradeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Creation.Grade();
                    beData.GradeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.MinPer = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.MaxPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.GradePoint = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.SubjectType = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) beData.ClassId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.ClassName = Convert.ToString(reader[8]);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int GradeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@GradeId", GradeId);
            cmd.CommandText = "usp_DelGradeById";
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

    }
}
