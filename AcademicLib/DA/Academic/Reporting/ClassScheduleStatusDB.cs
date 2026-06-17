using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Academic.Reporting
{
    internal class ClassScheduleStatusDB
    {
        DataAccessLayer1 dal = null;
        public ClassScheduleStatusDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public RE.Academic.ClassScheduleStatus GetClassScheduleStatus(int UserId, int AcademicyearId)
        {
            RE.Academic.ClassScheduleStatus beData = new RE.Academic.ClassScheduleStatus();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicyearId", AcademicyearId);
            cmd.CommandText = "usp_GetClassScheduleStatus";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                beData.completedColl = new RE.Academic.CompletedColl();
                while (reader.Read())
                {
                    RE.Academic.Completed dataColl = new RE.Academic.Completed();
                    if (!(reader[0] is DBNull)) dataColl.ClassSection = reader.GetString(0);
                    if (!(reader[1] is DBNull)) dataColl.Shift = reader.GetString(1);
                    if (!(reader[2] is DBNull)) dataColl.TotalPeriod = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) dataColl.PeriodAssigned = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) dataColl.TeacherName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) dataColl.CreatedAt = reader.GetString(5);
                    //DONE: Change and Add field
                    if (!(reader[6] is DBNull)) dataColl.UserId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) dataColl.LogDateTime = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) dataColl.User = reader.GetString(8);
                    beData.completedColl.Add(dataColl);
                }
                if (reader.NextResult())
                {
                    beData.pendingColl = new RE.Academic.PendingColl();
                    while (reader.Read())
                    {
                        RE.Academic.Pending dataColl = new RE.Academic.Pending();
                        if (!(reader[0] is DBNull)) dataColl.ClassSection = reader.GetString(0);
                        beData.pendingColl.Add(dataColl);
                    }
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