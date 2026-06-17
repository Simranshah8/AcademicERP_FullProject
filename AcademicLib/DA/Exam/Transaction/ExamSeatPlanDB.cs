using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamSeatPlanDB
    {
        DataAccessLayer1 dal = null;
        public ExamSeatPlanDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveSeatDetails(int UserId, BE.Exam.Transaction.SeatDetailsCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ExamShiftId",beData.ExamShiftId);
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@RoomId", beData.RoomId);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@Seat_Col", beData.Seat_Col);
                    cmd.Parameters.AddWithValue("@Seat_SNo", beData.Seat_SNo);
                    cmd.Parameters.AddWithValue("@BanchNo", beData.BanchNo);
                    cmd.Parameters.AddWithValue("@Banch_Row_SNo", beData.Banch_Row_SNo);
                    cmd.CommandText = "usp_SaveExamWiseSeatPlan";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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

        public RE.Exam.SeatPlanCollections GetAllExamSeatPlan(int UserId,int AcademicYearId)
        {
            RE.Exam.SeatPlanCollections dataColl = new RE.Exam.SeatPlanCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllExamSeatPlanList";           
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.SeatPlan beData = new RE.Exam.SeatPlan();
                    if (!(reader[0] is DBNull)) beData.ExamShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RoomId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ExamShiftName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ExamTypeName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RoomName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.TotalSeat = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.NoOfSeatAlloted = Convert.ToInt32(reader[9]);

                    if (!(reader[10] is DBNull)) beData.UserName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.LogDateTime_AD = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.LogDateTime_BS = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.TotalBanch = Convert.ToInt32(reader[13]);

                    beData.ClassSectionName = (beData.ClassName + " " + beData.SectionName).Trim();

                    dataColl.Add(beData);
                }
                reader.Close();

                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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

        public BE.Exam.Transaction.ExamRoomCollections GetRoomList(int UserId,int AcademicYearId, int ExamShiftId,int ExamTypeId)
        {
            BE.Exam.Transaction.ExamRoomCollections dataColl = new BE.Exam.Transaction.ExamRoomCollections();
           
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_GetVacanRoomForExamSheetPlan";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamRoom beData = new BE.Exam.Transaction.ExamRoom();
                    if (!(reader[0] is DBNull)) beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.AvailableSeats = Convert.ToInt32(reader[2]);
                    dataColl.Add(beData);
                }
                reader.Close();

                if (!(cmd.Parameters[3].Value is DBNull))
                    dataColl.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    dataColl.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);
                 
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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

        public BE.Exam.Transaction.RoomSeatDetailsCollections GetRoomSeatDetails(int UserId,int AcademicYearId, int ExamShiftId, int ExamTypeId,int RoomId, int SeatPlanAs)
        {
            BE.Exam.Transaction.RoomSeatDetailsCollections dataColl = new BE.Exam.Transaction.RoomSeatDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);            
            cmd.CommandText = "usp_GetRommWiseVacanSheet";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.Parameters.AddWithValue("@SeatPlanAs", SeatPlanAs);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.RoomSeatDetails beData = new BE.Exam.Transaction.RoomSeatDetails();
                    if (!(reader[0] is DBNull)) beData.RId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RoomId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Banch_Row_SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Seat_Col = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.BanchNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.StudentDet = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TotalBanch = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.NoOfBanchRow = Convert.ToInt32(reader[8]);
                    dataColl.Add(beData);
                }
                reader.Close();

                if (!(cmd.Parameters[3].Value is DBNull))
                    dataColl.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    dataColl.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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

        public BE.Academic.Creation.ClassSectionCollections GetClassSectionList(int UserId, int AcademicYearId, int ExamShiftId, int ExamTypeId, int RoomId, int SeatPlanAs, ref BE.Exam.Transaction.StudentForSeatPlanCollections studentColl)
        {
            studentColl = new BE.Exam.Transaction.StudentForSeatPlanCollections();
            BE.Academic.Creation.ClassSectionCollections dataColl = new BE.Academic.Creation.ClassSectionCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_GetClassSectionForExamSeatPlan";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.Parameters.AddWithValue("@SeatPlanAs", SeatPlanAs);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Creation.ClassSection beData = new BE.Academic.Creation.ClassSection();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[4]);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.StudentForSeatPlan beData = new BE.Exam.Transaction.StudentForSeatPlan();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SymbolNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RankInClass = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.RankInSection = reader.GetInt32(10);
                    studentColl.Add(beData);
                }
                reader.Close();

                if (!(cmd.Parameters[3].Value is DBNull))
                    dataColl.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    dataColl.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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
        public ResponeValues DeleteSeatPlan(int UserId, int ExamShiftId, int ExamTypeId, int RoomId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);            
            cmd.Parameters.AddWithValue("@RoomId", RoomId);            
            cmd.CommandText = "usp_DelExamSheetPlan";
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
        public ResponeValues GenerateSeatPlan(int AcademicYearId, AcademicLib.BE.Exam.Transaction.GenerateSeatPlan beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@ExamShiftId", beData.ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@ClassIdColl", beData.ClassIdColl);
            cmd.Parameters.AddWithValue("@RoomIdColl", beData.RoomIdColl);
            cmd.Parameters.AddWithValue("@SeatPlanAs", beData.SeatPlanAs);
            cmd.CommandText = "usp_CreateExamSheetPlan";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();
             
                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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
        public AcademicLib.RE.Exam.ExamSeatPlanDetails GetExamSeatPlan(int UserId,int AcademicYearId, int ExamTypeId, int ExamShiftId,int FieldValueAs,string RoomIdColl)
        {
            AcademicLib.RE.Exam.ExamSeatPlanDetails seatPlan = new RE.Exam.ExamSeatPlanDetails();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@FieldValueAs", FieldValueAs);
            cmd.Parameters.AddWithValue("@RoomIdColl", RoomIdColl);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_PrintExamSeatPlan";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                seatPlan.ExamSeatPlanColl = new List<RE.Exam.ExamSeatPlan>();
                seatPlan.RoomSummaryColl = new List<RE.Exam.RoomSummary>();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.ExamSeatPlan beData = new RE.Exam.ExamSeatPlan();                    
                    if (!(reader[0] is DBNull)) beData.RoomName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.RowName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.BanchNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Col1 = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Col2 = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Col3 = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Col4 = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Col5 = reader.GetString(7);

                    if (!(reader[8] is DBNull)) beData.Name1 = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Name2 = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Name3 = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Name4 = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Name5 = reader.GetString(12);

                    if (!(reader[13] is DBNull)) beData.Class1 = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Class2 = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Class3 = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Class4 = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Class5 = reader.GetString(17);

                    if (!(reader[18] is DBNull)) beData.RollNo1 = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.RollNo2 = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.RollNo3 = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.RollNo4 = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.RollNo5 = reader.GetString(22);

                    if (!(reader[23] is DBNull)) beData.RegNo1 = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.RegNo2 = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.RegNo3 = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.RegNo4 = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.RegNo5 = reader.GetString(27);

                    if (!(reader[28] is DBNull)) beData.SymbolNo1= reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.SymbolNo2 = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.SymbolNo3 = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.SymbolNo4 = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.SymbolNo5 = reader.GetString(32);

                    if (!(reader[33] is DBNull)) beData.BenchOrdinalNo = reader.GetString(33);
                     

                    seatPlan.ExamSeatPlanColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.RoomSummary beData = new RE.Exam.RoomSummary();
                    if (!(reader[0] is DBNull)) beData.RoomName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SymbolNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.NoOfStudent = reader.GetInt32(7);
                    beData.ClassSectionName = (beData.ClassName + " " + beData.SectionName).Trim();
                    seatPlan.RoomSummaryColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.RoomSummary beData = new RE.Exam.RoomSummary();
                    if (!(reader[0] is DBNull)) beData.RoomName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SymbolNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.NoOfStudent = reader.GetInt32(7);
                    beData.ClassSectionName = (beData.ClassName + " " + beData.SectionName).Trim();
                    seatPlan.ClassSummaryColl.Add(beData);
                }
                reader.Close();
                seatPlan.IsSuccess = true;
                seatPlan.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                seatPlan.IsSuccess = false;
                seatPlan.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return seatPlan;
        }

        public AcademicLib.RE.Exam.StudentExamSeatPlanCollections GetStudentListForSMS(int UserId, int AcademicYearId, int ExamTypeId, int ExamShiftId)
        {
            AcademicLib.RE.Exam.StudentExamSeatPlanCollections seatPlan = new RE.Exam.StudentExamSeatPlanCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetSeatPlanForSMS";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.StudentExamSeatPlan beData = new RE.Exam.StudentExamSeatPlan();
                    if (!(reader[0] is DBNull)) beData.ExamType = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ExamShift = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Room = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BenchNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SeatNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.RegdNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.SymbolNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Name = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.FatherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.UserId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.StudentId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.ColumnName = reader.GetString(15);
                    seatPlan.Add(beData);
                }                
                reader.Close();
                seatPlan.IsSuccess = true;
                seatPlan.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                seatPlan.IsSuccess = false;
                seatPlan.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return seatPlan;
        }
    }
}
