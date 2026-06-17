using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class PhysicalClassScheduleDB
    {
        DataAccessLayer1 dal = null;
        public PhysicalClassScheduleDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, BE.Academic.Transaction.ClassScheduleCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
       

            try
            {
                int? secId = dataColl.First().SectionId;
                cmd.Parameters.AddWithValue("@ClassId", dataColl.First().ClassId);                
                cmd.Parameters.AddWithValue("@ClassShiftId", dataColl.First().ClassShiftId);
                if (secId.HasValue && secId.Value > 0)
                {
                    cmd.Parameters.AddWithValue("@SectionId", secId.Value);
                    cmd.CommandText = "delete from tbl_PhysicalClassSchedule where ClassId=@ClassId and ClassShiftId=@ClassShiftId and SectionId=@SectionId";                    
                }else
                {                    
                    cmd.CommandText = "delete from tbl_PhysicalClassSchedule where ClassId=@ClassId and ClassShiftId=@ClassShiftId";                    
                }
                cmd.ExecuteNonQuery();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                    cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);
                    cmd.Parameters.AddWithValue("@DayId", beData.DayId);
                    cmd.Parameters.AddWithValue("@Period", beData.Period);
                    cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                    cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_SavePhysicalClassSchedule";
                    cmd.ExecuteNonQuery();
                }

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Physical Class Schedule Saved Success";

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
        public AcademicLib.BE.Academic.Transaction.ClassScheduleCollections getClassScheduleByClassId(int UserId, int ClassId,int? SectionId,int ClassShiftId)
        {
            AcademicLib.BE.Academic.Transaction.ClassScheduleCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassScheduleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.CommandText = "usp_GetPhysicalClassScheduleByClassId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassSchedule beData = new BE.Academic.Transaction.ClassSchedule();
                    beData.DayId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Period = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
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

        public AcademicLib.RE.Academic.ClassScheduleCollections getClassSchedule(int UserId, int? ClassId, int? SectionId)
        {
            AcademicLib.RE.Academic.ClassScheduleCollections dataColl = new RE.Academic.ClassScheduleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            if (ClassId.HasValue && ClassId.Value == 0)
                ClassId = null;

            if (SectionId.HasValue && SectionId.Value == 0)
                SectionId = null;

            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);            
            cmd.CommandText = "usp_GetPhysicalClassSchedule";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.ClassSchedule beData = new RE.Academic.ClassSchedule();
                    beData.ShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ShiftName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ShiftStartTime = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ShiftEndTime = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.NoOfBreak = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.SectionId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ClassName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SectionName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DayId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Period = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.StartTime = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.EndTime = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.SubjectName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TeacherName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.TeacherAddress = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TeacherContactNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.SubjectId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.Duration = reader.GetInt32(18);

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
