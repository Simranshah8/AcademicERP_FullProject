using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    public class EntranceSymbolNoDB
    {
        DataAccessLayer1 dal = null;
        public EntranceSymbolNoDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public BE.Exam.Transaction.SymboolNoCollections GetEntranceSymbolNo(int UserId, int? ClassId)
        {
            BE.Exam.Transaction.SymboolNoCollections dataColl = new BE.Exam.Transaction.SymboolNoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetForSymbol";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.SymboolNo beData = new BE.Exam.Transaction.SymboolNo();
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
                    if (!(reader[14] is DBNull)) beData.PhotoPath = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.SymbolNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TranId = reader.GetInt32(16);
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

        public ResponeValues SaveUpdate(int UserId, List<BE.Exam.Transaction.EntranceSymbolNo> dataColl)
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
                    cmd.CommandText = "usp_DelEntranceSymbolNo";
                    cmd.ExecuteNonQuery();
                }
                foreach (var beData in dataColl)
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EnquiryNo", beData.EnquiryNo);
                    cmd.Parameters.AddWithValue("@StartNumber", beData.StartNumber);
                    cmd.Parameters.AddWithValue("@StartAlpha", beData.StartAlpha);
                    cmd.Parameters.AddWithValue("@PadWith", beData.PadWith);
                    cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
                    cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
                    cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    cmd.CommandText = "usp_AddEntranceSymbolNo";
                    cmd.ExecuteNonQuery();

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Symbol No Saved";
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