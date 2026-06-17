using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class ClassScheduleDB
    {
        DataAccessLayer1 dal = null;
        public ClassScheduleDB(string hostName, string dbName)
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
                var fst = dataColl.First();
                int? secId = dataColl.First().SectionId;
                cmd.Parameters.AddWithValue("@ClassId", fst.ClassId);                
                cmd.Parameters.AddWithValue("@ClassShiftId", fst.ClassShiftId);

                if(fst.SemesterId.HasValue)
                    cmd.Parameters.AddWithValue("@SemesterId", fst.SemesterId);
                else
                    cmd.Parameters.AddWithValue("@SemesterId", DBNull.Value);

                if (fst.ClassYearId.HasValue)
                    cmd.Parameters.AddWithValue("@ClassYearId", fst.ClassYearId);
                else
                    cmd.Parameters.AddWithValue("@ClassYearId", DBNull.Value);

                if (fst.BatchId.HasValue)
                    cmd.Parameters.AddWithValue("@BatchId", fst.BatchId);
                else
                    cmd.Parameters.AddWithValue("@BatchId", DBNull.Value);
                 
                if (secId.HasValue && secId.Value > 0)
                {
                    cmd.Parameters.AddWithValue("@SectionId", secId.Value);
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete CS from tbl_ClassSchedule CS where ClassId=@ClassId and ClassShiftId=@ClassShiftId and SectionId=@SectionId and isnull(CS.SemesterId,0)=isnull(@SemesterId,0) and isnull(CS.ClassYearId,0)=isnull(@ClassYearId,0) and isnull(CS.BatchId,0)=isnull(@BatchId,0)";                    
                }else
                {                    
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete CS from tbl_ClassSchedule CS where ClassId=@ClassId and ClassShiftId=@ClassShiftId and isnull(CS.SemesterId,0)=isnull(@SemesterId,0) and isnull(CS.ClassYearId,0)=isnull(@ClassYearId,0) and isnull(CS.BatchId,0)=isnull(@BatchId,0)";                    
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
                    cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandText = "usp_SaveClassSchedule";

                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                    cmd.ExecuteNonQuery();

                    if(!(cmd.Parameters[8].Value is DBNull))
                    {
                        beData.TranId = Convert.ToInt32(cmd.Parameters[8].Value);
                        if(beData.AlternetColl!=null)
                            SaveAlternet(UserId, beData.TranId.Value, beData.AlternetColl);
                    }
                }

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Class Schedule Saved Success";

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
        private void SaveAlternet(int UserId, int TranId, List<AcademicLib.BE.Academic.Transaction.ClassScheduleAlternet> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (AcademicLib.BE.Academic.Transaction.ClassScheduleAlternet beData in beDataColl)
            {
                if (beData.SubjectId.HasValue && beData.SubjectId.Value>0)
                {

                    if (beData.EmployeeId.HasValue && beData.EmployeeId.Value == 0)
                        beData.EmployeeId = null;

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                    cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddClassScheduleAlternet";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public AcademicLib.BE.Academic.Transaction.ClassScheduleCollections getClassScheduleByClassId(int UserId, int ClassId,int? SectionId,int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            AcademicLib.BE.Academic.Transaction.ClassScheduleCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassScheduleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);

            cmd.CommandText = "usp_GetClassScheduleByClassId";
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
                    if (!(reader[4] is DBNull)) beData.TranId = reader.GetInt32(4);

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassScheduleAlternet beData = new BE.Academic.Transaction.ClassScheduleAlternet();
                    beData.TranId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeId = reader.GetInt32(2);
                    dataColl.Find(p1 => p1.TranId == beData.TranId).AlternetColl.Add(beData);
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

        public AcademicLib.RE.Academic.ClassScheduleCollections getClassSchedule(int UserId,int AcademicYearId, int? ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
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
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetClassSchedule";
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
                    if (!(reader[19] is DBNull)) beData.ForType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.TeacherPhotoPath = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Level = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Faculty = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Semester = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.ClassYear = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Batch = reader.GetString(25);

                    try
                    {
                        if (!(reader[26] is DBNull)) beData.SemesterId = reader.GetInt32(26);
                        if (!(reader[27] is DBNull)) beData.ClassYearId = reader.GetInt32(27);
                        if (!(reader[28] is DBNull)) beData.BatchId = reader.GetInt32(28);
                    }
                    catch { }

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

        public ResponeValues DeleteByShiftId(int UserId, int EntityId,int ClassId, int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.CommandText = "usp_DelClassScheduleByShiftId";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
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
