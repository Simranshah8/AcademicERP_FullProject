using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ReExamWiseSymbolNoDB
    {
        DataAccessLayer1 dal = null;
        public ReExamWiseSymbolNoDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@StartNumber", beData.StartNumber);
                    cmd.Parameters.AddWithValue("@PadWith", beData.PadWith);
                    cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
                    cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
                    cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandText = "usp_AddReExamWiseSymbolNo";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@StartAlpha", beData.StartAlpha);
                    cmd.Parameters.AddWithValue("@ReExamTypeId", beData.ReExamTypeId);
                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                    if (!(cmd.Parameters[10].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                    if (!(cmd.Parameters[11].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                    if (!(cmd.Parameters[12].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                    if(resVal.IsSuccess==true && resVal.RId > 0)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        foreach (var v in beData.ReExamSubjectList)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TranId", resVal.RId);
                            cmd.Parameters.AddWithValue("@SubjectId", v);
                            cmd.CommandText = "insert into tbl_ReExamSubjectList(TranId,SubjectId) values(@TranId,@SubjectId)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                }

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Exam Wise Symbol No. was updated";
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

        public AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNoCollections getStudentList(int UserId, int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId,bool ForClassWise,int? AcademicYearId)
        {
            AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNoCollections dataColl = new AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ReExamTypeId", ReExamTypeId);
            cmd.Parameters.AddWithValue("@ForClassWise", ForClassWise);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentForReExamWiseSymbolNo";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNo beData = new AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNo();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.BoardRegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName= reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);                    
                    if (!(reader[6] is DBNull)) beData.SymbolNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartNumber = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.PadWith = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Prefix = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Suffix = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.StartAlpha = reader.GetString(11);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while(reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ReExamSubject beData = new BE.Exam.Transaction.ReExamSubject();
                    int studentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ObtainMark = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.IsFail = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.ConductReExam = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.IsFailTH = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsFailPR = reader.GetBoolean(6);
                    dataColl.Find(p1 => p1.StudentId == studentId).ExamSubjectList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Creation.Subject beData = new BE.Academic.Creation.Subject();
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    dataColl.SubjectList.Add(beData);
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

        public AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections getExamWiseSymbolNoClassWise(int UserId, int ClassId, int? SectionId, int ExamTypeId)
        {
            AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_GetExamWiseSymbolNoOfClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo beData = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SymbolNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.StartNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.PadWith = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Prefix = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Suffix = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StartAlpha = reader.GetString(6);
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

        public AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections getSymbolNoForMarkImport(int UserId)
        {
            AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetSymbolNoForMarkImport";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo beData = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo();
                    beData.ExamTypeId = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SymbolNo = reader.GetString(2);
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
        public AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo getExamWiseSymbolNoById(int UserId, int EntityId, int ExamWiseSymbolNoId)
        {
            AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo beData = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamWiseSymbolNoId", ExamWiseSymbolNoId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamWiseSymbolNoById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNo();
                    //beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SymbolNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.PadWith = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Prefix = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Suffix = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StudentId = reader.GetInt32(7);


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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelExamWiseSymbolNoById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
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

        public ResponeValues Transfor(int UserId, int FromExamTypeId, int ToExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromExamTypeId", FromExamTypeId);
            cmd.Parameters.AddWithValue("@ToExamTypeId", ToExamTypeId);
            cmd.CommandText = "usp_TransforSymbolNo";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
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
