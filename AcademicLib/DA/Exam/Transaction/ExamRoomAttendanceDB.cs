using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamRoomAttendanceDB
    {
        DataAccessLayer1 dal = null;
        public ExamRoomAttendanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
  
        public AcademicLib.BE.Exam.Transaction.ExamRoomAttendanceCollections getExamWiseAttendance(int UserId, int AcademicYearId, int EntityId, int ExamTypeId, int ExamShiftId, int RoomId, DateTime ExamDate)
        {
            AcademicLib.BE.Exam.Transaction.ExamRoomAttendanceCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamRoomAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
            cmd.CommandText = "usp_GetExamWiseAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamRoomAttendance beData = new AcademicLib.BE.Exam.Transaction.ExamRoomAttendance();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.StudentName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.SymbolNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubjectName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Attendance = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Remarks = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.PaperType = reader.GetString(10);
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

        public ResponeValues SaveUpdateExamAttendance(int UserId, List<BE.Exam.Transaction.ExamRoomAttendance> DataColl)
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
                tableAllocation.Columns.Add("ExamDate", typeof(DateTime));
                tableAllocation.Columns.Add("ExamTypeId", typeof(int));
                tableAllocation.Columns.Add("ExamShiftId", typeof(int));
                tableAllocation.Columns.Add("RoomId", typeof(int));
                tableAllocation.Columns.Add("StudentId", typeof(int));      
                tableAllocation.Columns.Add("Attendance", typeof(int));
                tableAllocation.Columns.Add("Remarks", typeof(string));
                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["ExamDate"] = v.ExamDate;
                    row["ExamTypeId"] = v.ExamTypeId;
                    row["ExamShiftId"] = v.ExamShiftId;
                    row["RoomId"] = v.RoomId;
                    row["StudentId"] = v.StudentId;
                    row["Attendance"] = v.Attendance;
                    row["Remarks"] = v.Remarks;

                    tableAllocation.Rows.Add(row);
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@ExamRoomAttendanceColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddExamRoomAttendance";
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

        public ResponeValues DeleteExamRoomWiseAttendance(int UserId, DateTime ExamDate, int ExamTypeId, int ExamShiftId, int RoomId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.CommandText = "usp_DeleteExamRoomAttendance";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

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
