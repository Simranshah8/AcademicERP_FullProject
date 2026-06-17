using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class HODDB
    {
        DataAccessLayer1 dal = null;
        public HODDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int? AcademicYearId, BE.Academic.Transaction.ClassHODCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                //TODO: change in delete
                foreach (var fst in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@DepartmentId", fst.DepartmentId);
                    cmd.Parameters.AddWithValue("@EmployeeId", fst.EmployeeId);
                    cmd.Parameters.AddWithValue("@ShiftId", fst.ShiftId);
                    cmd.Parameters.AddWithValue("@SubjectId", fst.SubjectId);
                    cmd.Parameters.AddWithValue("@BatchId", fst.BatchId);
                    cmd.Parameters.AddWithValue("@ClassYearId", fst.ClassYearId);
                    cmd.Parameters.AddWithValue("@SemesterId", fst.SemesterId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.CommandText = "usp_DelClassHODById";
                    cmd.ExecuteNonQuery();
                }

                foreach (var beData in dataColl)
                {
                    if (beData.IsHod)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
                        cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                        cmd.Parameters.AddWithValue("@ShiftId", beData.ShiftId);
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                        cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                        //TODO: Added Field
                        cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                        cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.CommandText = "usp_AddClassHOD";
                        cmd.ExecuteNonQuery();

                    }

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "HOD Updated";

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
        public AcademicLib.BE.Academic.Transaction.ClassHODCollections getAllHOD(int UserId)
        {
            AcademicLib.BE.Academic.Transaction.ClassHODCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassHODCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAllClassHodList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassHOD beData = new BE.Academic.Transaction.ClassHOD();
                    if (!(reader[0] is DBNull)) beData.HODId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.DepartmentName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TeacherName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ShiftName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.DepartmentId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EmployeeId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ClassId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.SubjectName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassShiftId = reader.GetInt32(10);
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
        public AcademicLib.BE.Academic.Transaction.ClassHODCollections getAllHOD(int UserId, int DepartmentId, int EmployeeId, int ShiftId, int? SubjectId,int? AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.ClassHODCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassHODCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@ShiftId", ShiftId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetClassListForHOD";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassHOD beData = new BE.Academic.Transaction.ClassHOD();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.IsHod = reader.GetBoolean(4);
                    //TODO: Added Field
                    if (!(reader[5] is DBNull)) beData.BatchId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Batch = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ClassYear = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Semester = reader.GetString(10);
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
        public ResponeValues DeleteById(int UserId, int DepartmentId,int EmployeeId, int ShiftId,int? AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@ShiftId", ShiftId);
            cmd.CommandText = "usp_DelClassHOD";            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

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
