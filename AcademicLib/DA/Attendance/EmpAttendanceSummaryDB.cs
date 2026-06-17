using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
    internal class EmpAttendanceSummaryDB
    {
        DataAccessLayer1 dal = null;
        public EmpAttendanceSummaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicLib.RE.Attendance.EmpAttendanceSummaryCollections getAllEmpAttendanceSummary(int UserId, string BranchIdColl, string DepartmentIdColl, string GroupIdColl, DateTime? DateFrom, DateTime? DateTo, int? EmpType,int ForEmp)
        {
            AcademicLib.RE.Attendance.EmpAttendanceSummaryCollections dataColl = new AcademicLib.RE.Attendance.EmpAttendanceSummaryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchIdColl", BranchIdColl);
            cmd.Parameters.AddWithValue("@DepartmentIdColl", DepartmentIdColl);
            cmd.Parameters.AddWithValue("@GroupIdColl", GroupIdColl);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@EmpType", EmpType);           
            cmd.Parameters.AddWithValue("@ForEmp", ForEmp);           
            //cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            //cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            //cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.CommandText = "sp_GetEmpAttendanceSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    AcademicLib.RE.Attendance.EmpAttendanceSummary beData = new AcademicLib.RE.Attendance.EmpAttendanceSummary();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EnrollNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Department = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.LevelName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ServiceType = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.GroupName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TotalDays = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.TotalWeekEnd = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.TotalPresent = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.TotalLeave = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.TotalHoliday = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.EmployeeCode = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.WeekendPresent = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.HolidayPresent = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.LeavePresent = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.Category = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ServiceType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.CompanyName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.WorkingDuration = Convert.ToDouble(reader[21]);
                    if (!(reader[22] is DBNull)) beData.OTDuration = Convert.ToDouble(reader[22]); ;
                    if (!(reader[23] is DBNull)) beData.SinglePunchDeduction = Convert.ToDouble(reader[23]); ;
                    if (!(reader[24] is DBNull)) beData.EarlyInMinutes = Convert.ToDouble(reader[24]); ;
                    if (!(reader[25] is DBNull)) beData.LateInMinutes = Convert.ToDouble(reader[25]); ;
                    if (!(reader[26] is DBNull)) beData.EarlyOutMinutes = Convert.ToDouble(reader[26]); ;
                    if (!(reader[27] is DBNull)) beData.DelayOutMinutes = Convert.ToDouble(reader[27]); ;
                    if (!(reader[28] is DBNull)) beData.SinglePunchCount = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.EarlyInMinutesCount = reader.GetInt32(29);
                    if (!(reader[30] is DBNull)) beData.LateInMinutesCount = reader.GetInt32(30);
                    if (!(reader[31] is DBNull)) beData.EarlyOutMinutesCount = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.DelayOutMinutesCount = reader.GetInt32(32);
                    if (!(reader[33] is DBNull)) beData.BranchName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.BranchAddress = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.TotalAbsent = reader.GetInt32(35);
                    if (!(reader[36] is DBNull)) beData.WorkingShift = reader.GetString(36);
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

    }
}