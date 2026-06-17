using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    public class EntranceExamResultDB
    {
        DataAccessLayer1 dal = null;
        public EntranceExamResultDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public BE.Exam.Transaction.MarkEntryCollections GetEntranceResult(int UserId, int? EntityId, int? ClassId)
        {
            BE.Exam.Transaction.MarkEntryCollections dataColl = new BE.Exam.Transaction.MarkEntryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetEntranceResult";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.MarkEntry beData = new BE.Exam.Transaction.MarkEntry();
                    if (!(reader[0] is DBNull)) beData.EnquiryId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Status = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Sourse = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.EntryDate = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.DOB_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.DOB_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Email = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PaymentStatus = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ExamName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ExamDate = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.ExamTime = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Venue = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.PhotoPath = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ExamRules = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ResultDate = Convert.ToDateTime(reader[20]);
                    if (!(reader[21] is DBNull)) beData.FullMarks = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.PassMarks = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.ResultDateMiti = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.ObtMarks = Convert.ToDouble(reader[24]);
                    if (!(reader[25] is DBNull)) beData.Result = reader.GetInt32(25);
                    if (!(reader[26] is DBNull)) beData.Remarks = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.Subject = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ExamDateMiti = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.TranId = reader.GetInt32(29);
                    if (!(reader[30] is DBNull)) beData.SymbolNo = reader.GetString(30);
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
       
        public ResponeValues SaveUpdate(int UserId, List<BE.Exam.Transaction.EntranceMarkEntry> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var uniqueEnquiryNos = dataColl.Select(x => x.EnquiryNo).Distinct();
                foreach (var enquiryNo in uniqueEnquiryNos)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EnquiryId", enquiryNo);
                    cmd.CommandText = "usp_DelEntranceMarkEntry";
                    cmd.ExecuteNonQuery();
                }
                foreach (var beData in dataColl)
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EnquiryNo", beData.EnquiryNo);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@AppliedClass", beData.AppliedClass);
                    cmd.Parameters.AddWithValue("@ExamName", beData.ExamName);
                    cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
                    cmd.Parameters.AddWithValue("@PassMarks", beData.PassMarks);
                    cmd.Parameters.AddWithValue("@ObtMarks", beData.ObtMarks);
                    cmd.Parameters.AddWithValue("@Result", beData.Result);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@ExamDatetime", beData.ExamDatetime);
                    cmd.Parameters.AddWithValue("@SubjectIncluded", beData.SubjectIncluded);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    cmd.CommandText = "usp_AddEntranceMarkEntry";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Entrance Mark Entry Saved";
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

        //Add by pRashant Baishak02
        public BE.Exam.Transaction.MarkSetupCollections GetMarkSetup(int UserId, int? EntityId, int? ClassId)
        {
            BE.Exam.Transaction.MarkSetupCollections dataColl = new BE.Exam.Transaction.MarkSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetMarkSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.MarkSetup beData = new BE.Exam.Transaction.MarkSetup();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SubjectName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FullMark = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PassMark = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TranId = reader.GetInt32(6);
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

        public ResponeValues SaveMarkSetup(int UserId, List<BE.Exam.Transaction.EntranceMarkSetup> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var uniqueSubjectIds = dataColl.Select(x => x.SubjectId).Distinct();
                foreach (var SubjectId in uniqueSubjectIds)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                    cmd.CommandText = "usp_DelEntranceMarkSetup";
                    cmd.ExecuteNonQuery();
                }
                foreach (var beData in dataColl)
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                    cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);
                    cmd.Parameters.AddWithValue("@PassMark", beData.PassMark);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    cmd.CommandText = "usp_AddEntranceMarkSetup";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Entrance Mark Setup Saved Successfully";
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