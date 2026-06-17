using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{
    internal class AcademicCalendarDB
    {
        DataAccessLayer1 dal = null;
        public AcademicCalendarDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public AcademicLib.BE.AppCMS.Creation.AcademicCalendarCollections getNepaliCalendar(int UserId, int? YearId, string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.AcademicCalendarCollections dataColl = new BE.AppCMS.Creation.AcademicCalendarCollections();

            dal.OpenConnection();
            //dal.OpenConnection(true);
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@YearId", YearId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAcademicCalendar";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.AcademicCalendar beData = new BE.AppCMS.Creation.AcademicCalendar();
                    beData.AD_Date = reader.GetDateTime(0);
                    beData.BS_Date = reader.GetString(1);
                    beData.NY = reader.GetInt32(2);
                    beData.NM = reader.GetInt32(3);
                    beData.ND = reader.GetInt32(4);
                    beData.DayId = reader.GetInt32(5);
                    beData.StartDayId = reader.GetInt32(6);
                    beData.DaysInMonth = reader.GetInt32(7);
                    beData.MonthName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsWeekend = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.WeekendColorCode = reader.GetString(10);

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.EventSummary summary = new BE.AppCMS.Creation.EventSummary();
                    summary.EventHolidayId = reader.GetInt32(0);
                    summary.ForDate = reader.GetDateTime(1);
                    summary.NY = reader.GetInt32(2);
                    summary.NM = reader.GetInt32(3);
                    summary.ND = reader.GetInt32(4);
                    summary.EventType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) summary.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) summary.Description = reader.GetString(7);
                    if (!(reader[8] is DBNull)) summary.ColorCode = reader.GetString(8);
                    if (!(reader[9] is DBNull)) summary.FromDate_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) summary.ToDate_AD = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) summary.FromDate_BS = reader.GetString(11);
                    if (!(reader[12] is DBNull)) summary.ToDate_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) summary.ImagePath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) summary.ForClass = reader.GetString(14);
                    if (!(reader[15] is DBNull)) summary.AtTime = reader.GetString(15);
                    var data = dataColl.Find(p1 => p1.NM == summary.NM && p1.ND == summary.ND);
                    if (data != null)
                        data.EventColl.Add(summary);
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

        public AcademicLib.API.AppCMS.EventHolidayCollections  getUpcomingEventHoliday(int UserId,DateTime? fromDate,DateTime? toDate,int eType, string BranchCode)
        {
            AcademicLib.API.AppCMS.EventHolidayCollections dataColl = new API.AppCMS.EventHolidayCollections();

            dal.OpenConnection(true);
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", fromDate);
            cmd.Parameters.AddWithValue("@DateTo", toDate);
            cmd.Parameters.AddWithValue("@EType", eType);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetUpComingEventHoliday";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.AppCMS.EventHoliday beData = new API.AppCMS.EventHoliday();
                    if (!(reader[0] is DBNull)) beData.HolidayEvent = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.EventType = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FromDate_AD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ToDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.FromDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ToDate_BS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ForClass = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ColorCode = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ImagePath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.AtTime = reader.GetString(11);

                    int daysDiff = (beData.FromDate_AD - DateTime.Today).Days;

                    if (daysDiff > 0)
                        beData.Remaining = daysDiff.ToString() + " Days Remaining";
                    else
                        beData.Remaining = "Passed";


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
