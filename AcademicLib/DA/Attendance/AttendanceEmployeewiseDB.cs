using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Attendance
{
    internal class AttendanceEmployeewiseDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceEmployeewiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Attendance.AttendanceEmployeewiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var fData = dataColl.First();
                cmd.Parameters.AddWithValue("@UserId", fData.CUserId);
                cmd.Parameters.AddWithValue("@DepartmentId", fData.DepartmentId);
                cmd.Parameters.AddWithValue("@ForDate", fData.ForDate);
                cmd.Parameters.AddWithValue("@InOutMode", fData.InOutMode);                
                cmd.CommandText = "usp_DelEmployeeDailyAttendance";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (beData.Attendance.HasValue)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                        cmd.Parameters.AddWithValue("@InOutMode", beData.InOutMode);
                        cmd.Parameters.AddWithValue("@Attendance", beData.Attendance);
                        cmd.Parameters.AddWithValue("@LateMin", beData.LateMin);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        cmd.CommandText = "usp_AddEmployeeDailyAttendance";
                        cmd.ExecuteNonQuery();

                    }
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Employee Daily Attendance Done";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public BE.Attendance.AttendanceEmployeewiseCollections getDepartmentWiseAttendance(int UserId, int DepartmentId,  DateTime forDate, int InOutMode = 2)
        {
            BE.Attendance.AttendanceEmployeewiseCollections dataColl = new BE.Attendance.AttendanceEmployeewiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);            
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@InOutMode", InOutMode);
            cmd.CommandText = "usp_GetEmployeeDailyAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Attendance.AttendanceEmployeewise beData = new BE.Attendance.AttendanceEmployeewise();
                    if (!(reader[0] is DBNull)) beData.Attendance = (BE.Attendance.ATTENDANCES)reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LateMin = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EmployeeId = reader.GetInt32(3);
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
        public ResponeValues DeleteEmployeeWiseAttendance(int UserId, DateTime? ForDate, int? DepartmentId, int? AttendaneTypeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@AttendaneTypeId", AttendaneTypeId);
            cmd.CommandText = "usp_DeleteEmployeeWiseAttendance";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
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
