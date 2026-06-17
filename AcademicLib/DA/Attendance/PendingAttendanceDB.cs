using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Attendance
{
    public class PendingAttendanceDB
    {

        DataAccessLayer1 dal = null;
        public PendingAttendanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public RE.Attendance.PendingAttendanceCollections GetPendingAttendance(int UserId, int? AcademicYearId, int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId, DateTime? DateFrom, DateTime? DateTo,int For=1)
        {
            RE.Attendance.PendingAttendanceCollections dataColl = new RE.Attendance.PendingAttendanceCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@For", For);
            cmd.CommandText = "usp_GetPendingAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Attendance.PendingAttendance beData = new RE.Attendance.PendingAttendance();
                    if (!(reader[0] is DBNull)) beData.ForDate = Convert.ToDateTime(reader[0]);
                    if (!(reader[1] is DBNull)) beData.ForMiti = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Section = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Batch = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Semester = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassYear = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassTeacher = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Coordinator = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.HOD = reader.GetString(9);

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