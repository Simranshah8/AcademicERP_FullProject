using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Attendance.Reporting
{
    internal class StudentAttendanceSumaryDB
    {
        DataAccessLayer1 dal = null;
        public StudentAttendanceSumaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public RE.Attendance.StudentAttendanceSumaryCollections GetStudentAttendanceSumary(int UserId, DateTime? DateFrom, DateTime? DateTo,int? AcademicYearId,int? ClassId,int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId, int? BranchId)
        {
            RE.Attendance.StudentAttendanceSumaryCollections dataColl = new RE.Attendance.StudentAttendanceSumaryCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentAttendanceSumm";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Attendance.StudentAttendanceSumary beData = new RE.Attendance.StudentAttendanceSumary();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.EMSId = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LeftStatus = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Batch = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Semester = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassYear = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TotalDays = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.TotalWeekEnd = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.TotalHoliday = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.TotalPresent = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.TotalLeave = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.TotalAbsent = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.SchoolDays = reader.GetInt32(17);
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