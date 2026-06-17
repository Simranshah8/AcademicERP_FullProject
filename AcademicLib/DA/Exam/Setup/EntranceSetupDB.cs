using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Exam.Setup
{
    internal class EntranceSetupDB
    {
        DataAccessLayer1 dal = null;
        public EntranceSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Exam.Setup.EntranceSetup beData, int BranchId, int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ExamName", beData.ExamName);
            cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
            cmd.Parameters.AddWithValue("@ForClassWise", beData.ForClassWise);
            cmd.Parameters.AddWithValue("@Venue", beData.Venue);
            cmd.Parameters.AddWithValue("@ExamRules", beData.ExamRules);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.CommandText = "usp_AddEntranceSetup";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            //Add by Prashant Chaitra 26
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@Subject", beData.Subject);
            cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
            cmd.Parameters.AddWithValue("@PassMarks", beData.PassMarks);
            cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
           
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && beData.ForClassWise)
                {
                    SaveClassWiseEntranceSetup(beData.CUserId,BranchId, beData.ClassWiseEntranceSetupList,AcademicYearId);
                  
                }
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
        public void SaveClassWiseEntranceSetup(int UserId,int BranchId, List<BE.Exam.Setup.ClassWiseEntranceSetup> dataColl,int AcademicYearId)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var beData in dataColl)
            {
                if (string.IsNullOrWhiteSpace(beData.ExamName))
                    continue;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@ExamName", beData.ExamName);
                cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
                cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
                cmd.Parameters.AddWithValue("@Venue", beData.Venue);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                //Add by Prashant Chaitra 26
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@Subject", beData.Subject);
                cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
                cmd.Parameters.AddWithValue("@PassMarks", beData.PassMarks);
                cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
                cmd.CommandText = "usp_AddClasswiseEntranceSetup";
                cmd.ExecuteNonQuery();
            }
        }
 
        public BE.Exam.Setup.EntranceSetup getEntranceSetup(int UserId, int EntityId)
        {
            BE.Exam.Setup.EntranceSetup beData = new BE.Exam.Setup.EntranceSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEntranceSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Setup.EntranceSetup();
                    if (!(reader[0] is DBNull)) beData.ExamName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ExamDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.StartTime = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.EndTime = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.ForClassWise = Convert.ToBoolean(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Venue = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ExamRules = reader.GetString(6);
                    //Add by Prashant Chaitra 26
                    if (!(reader[7] is DBNull)) beData.Subject = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FullMarks = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.PassMarks = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.ResultDate = Convert.ToDateTime(reader[10]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseEntranceSetup det = new BE.Exam.Setup.ClassWiseEntranceSetup();
                    if (!(reader[0] is DBNull)) det.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.ExamName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.ExamDate = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) det.StartTime = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) det.EndTime = Convert.ToDateTime(reader[4]);
                    if (!(reader[5] is DBNull)) det.Venue = reader.GetString(5);
                    //Add by Prashant Chaitra 26
                    if (!(reader[6] is DBNull)) det.Subject = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det.FullMarks = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) det.PassMarks = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) det.ResultDate = Convert.ToDateTime(reader[9]);
                    beData.ClassWiseEntranceSetupList.Add(det);
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

        public BE.Exam.Setup.EntranceCardDataCollections getDataForEntranceCard(int UserId, DateTime? DateFrom, DateTime? DateTo)
        {
            BE.Exam.Setup.EntranceCardDataCollections dataColl = new BE.Exam.Setup.EntranceCardDataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.CommandText = "usp_GetDataForEntranceCard";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Setup.EntranceCardData beData = new BE.Exam.Setup.EntranceCardData();
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
                    if (!(reader[20] is DBNull)) beData.SymbolNo = reader.GetString(20);
                    //DONE: added Field
                    if (!(reader[21] is DBNull)) beData.ExamDateMiti = reader.GetString(21);
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
