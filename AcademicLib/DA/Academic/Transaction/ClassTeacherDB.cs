using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class ClassTeacherDB
    {
        DataAccessLayer1 dal = null;
        public ClassTeacherDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int? AcademicYearId, BE.Academic.Transaction.ClassTeacher beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@TeacherId", beData.TeacherId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ClassTeacherId", beData.ClassTeacherId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateClassTeacher";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddClassTeacher";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

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
        public AcademicLib.BE.Academic.Transaction.ClassTeacherCollections getAllClassTeacher(int UserId, int EntityId,int? AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.ClassTeacherCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassTeacherCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllClassTeacher";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassTeacher beData = new AcademicLib.BE.Academic.Transaction.ClassTeacher();
                    beData.ClassTeacherId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EmployeeName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.EmployeeCode = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Level = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Faculty = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Semester = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ClassYear = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Batch = reader.GetString(9);
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
        public AcademicLib.BE.Academic.Transaction.ClassTeacher getClassTeacherById(int UserId, int EntityId, int ClassTeacherId)
        {
            AcademicLib.BE.Academic.Transaction.ClassTeacher beData = new AcademicLib.BE.Academic.Transaction.ClassTeacher();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassTeacherId", ClassTeacherId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetClassTeacherById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.ClassTeacher();
                    beData.ClassTeacherId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.TeacherId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SemesterId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassYearId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.BatchId = reader.GetInt32(6);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ClassTeacherId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassTeacherId", ClassTeacherId);
            cmd.CommandText = "usp_DelClassTeacherById";
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
