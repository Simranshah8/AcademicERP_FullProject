using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AcademicERP.BE;
using AcademicLib.BE;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA
{
    internal class StudentHealthReportDB
    {
        DataAccessLayer1 dal = null;

        public StudentHealthReportDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public RE.StudentHealthReportCollections GetAllStudentHealthReport(int UserId,DateTime? DateFrom,DateTime? DateTo)
        {
            RE.StudentHealthReportCollections dataColl = new RE.StudentHealthReportCollections();
            try
            {
                dal.OpenConnection();

                SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetStudentHealthReport";

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);

                SqlDataReader reader = cmd.ExecuteReader();
                int SNo = 0;
                while (reader.Read())
                {
                    RE.StudentHealthReport beData = new RE.StudentHealthReport();

                    SNo++;  
                    beData.SNo = SNo;
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ObservedOn = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AdmissionNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Class = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Section = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Batch = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Semester = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassYear = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ObservedAt = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.HealthIssue = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.IsAdmitted = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.AdmittedAt = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.AdmittedDate = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.MedicineGiven = reader.GetBoolean(16);
                    if (!(reader[17] is DBNull)) beData.Age = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.PrescribedBy = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ObservedTime = Convert.ToDateTime(reader[19]);

                    dataColl.Add(beData);
                }

                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ex)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ex.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return dataColl;
        }
        public RE.StudentHealthPastHistoryCollections GetAllStudentHealthPastHistory(int UserId, DateTime? DateFrom, DateTime? DateTo)
        {
             RE.StudentHealthPastHistoryCollections dataColl = new RE.StudentHealthPastHistoryCollections();
            try
            {
                dal.OpenConnection();

                SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetStudentPastMedicalHistory";

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);

                SqlDataReader reader = cmd.ExecuteReader();
                int SNo = 0;
                while (reader.Read())
                {
                    RE.StudentHealthPastHistory beData = new RE.StudentHealthPastHistory();
                    SNo++;
                    beData.SNo = SNo;
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ObservedOn = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AdmissionNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Class = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Section = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Batch = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Semester = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassYear = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.HealthIssue = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Details = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.MedicineGiven = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.Age = reader.GetString(14);


                    dataColl.Add(beData);
                }

                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ex)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ex.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return dataColl;
        }



    }
}
