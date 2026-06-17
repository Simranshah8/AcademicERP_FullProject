using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Attendance
{

    internal class StudentTypeDailyAttendanceDB
    {

        DataAccessLayer1 dal = null;
        public StudentTypeDailyAttendanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Attendance.StudentTypeDailyAttendance beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
            cmd.Parameters.AddWithValue("@StudentTypeId", beData.StudentTypeId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@Attendance", beData.Attendance);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@Lat", beData.Lat);
            cmd.Parameters.AddWithValue("@Lng", beData.Lng);
            cmd.Parameters.AddWithValue("@LiveLocation", beData.LiveLocation);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentTypeDailyAttendance";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentTypeDailyAttendance";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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

        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentTypeDailyAttendanceById";
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
        public BE.Attendance.StudentTypeDailyAttendanceCollections getAllStudentTypeDailyAttendance(int UserId, int EntityId)
        {
            BE.Attendance.StudentTypeDailyAttendanceCollections dataColl = new BE.Attendance.StudentTypeDailyAttendanceCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllStudentTypeDailyAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Attendance.StudentTypeDailyAttendance beData = new BE.Attendance.StudentTypeDailyAttendance();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ForDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.StudentTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Attendance = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Lng = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.LiveLocation = reader.GetString(8);
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
        public BE.Attendance.StudentTypeDailyAttendance getStudentTypeDailyAttendanceById(int UserId, int EntityId, int TranId)
        {
            BE.Attendance.StudentTypeDailyAttendance beData = new BE.Attendance.StudentTypeDailyAttendance();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentTypeDailyAttendanceById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Attendance.StudentTypeDailyAttendance();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ForDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.StudentTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Attendance = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Lng = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.LiveLocation = reader.GetString(8);
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
        public BE.Attendance.StudentTypeDailyAttendanceCollections getTypeWiseAttendance(int UserId, int AcademicYearId, int StudentTypeId, int ClassId, int? SectionId, DateTime ForDate, int InOutMode = 3, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            BE.Attendance.StudentTypeDailyAttendanceCollections dataColl = new BE.Attendance.StudentTypeDailyAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentTypeId", StudentTypeId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);

            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@InOutMode", InOutMode);


            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);

            cmd.CommandText = "usp_GetStudentTypeWiseAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Attendance.StudentTypeDailyAttendance beData = new BE.Attendance.StudentTypeDailyAttendance();
                    if (!(reader[0] is DBNull)) beData.Attendance = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LateMin = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);

                    try
                    {

                        if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                        if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                        if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.PhotoPath = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.ClassName = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.SectionName = reader.GetString(9);

                        if (!(reader[10] is DBNull)) beData.BatchId = reader.GetInt32(10);
                        if (!(reader[11] is DBNull)) beData.SemesterId = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) beData.ClassYearId = reader.GetInt32(12);
                        if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);
                        if (!(reader[16] is DBNull)) beData.StudentType = reader.GetString(16);

                    }
                    catch { }

                    beData.IsSuccess = true;
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
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

        public ResponeValues SaveUpdateStudentTypeWise(int UserId, List<BE.Attendance.StudentTypeDailyAttendance> DataColl)
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
                tableAllocation.Columns.Add("ForDate", typeof(DateTime));
                tableAllocation.Columns.Add("StudentTypeId", typeof(int));
                tableAllocation.Columns.Add("StudentId", typeof(int));
                tableAllocation.Columns.Add("InOutMode", typeof(int));
                tableAllocation.Columns.Add("Attendance", typeof(int));
                tableAllocation.Columns.Add("LateMin", typeof(int));
                tableAllocation.Columns.Add("Remarks", typeof(string));
                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["ForDate"] = v.ForDate;
                    row["StudentTypeId"] = v.StudentTypeId;
                    row["StudentId"] = v.StudentId;
                    row["InOutMode"] = v.InOutMode;
                    row["Attendance"] = v.Attendance;
                    row["LateMin"] = v.LateMin;
                    row["Remarks"] = v.Remarks;

                    tableAllocation.Rows.Add(row);
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@StudentTypeWiseColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddStudentTypeWiseAttendance";
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

    }

}

