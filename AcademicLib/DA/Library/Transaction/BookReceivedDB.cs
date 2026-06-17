using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Transaction
{
    internal class BookReceivedDB
    {
        DataAccessLayer1 dal = null;
        public BookReceivedDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(List<BE.Library.Transaction.BookReceived> dataColl)
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
                    cmd.Parameters.AddWithValue("@IssueId", beData.IssueId);
                    cmd.Parameters.AddWithValue("@ReceivedType", beData.ReceivedType);
                    cmd.Parameters.AddWithValue("@ReturnDate", beData.ReturnDate);
                    cmd.Parameters.AddWithValue("@ReturnRemarks", beData.ReturnRemarks);
                    cmd.Parameters.AddWithValue("@RenewalDate", beData.RenewalDate);
                    cmd.Parameters.AddWithValue("@RenewalRemarks", beData.RenewalRemarks);
                    cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
                    cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.CommandText = "usp_AddBookReceived";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();


                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                    if (!(cmd.Parameters[10].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                    if (!(cmd.Parameters[11].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                    if (!resVal.IsSuccess)
                        break;
                }

                if (!resVal.IsSuccess)
                    dal.RollbackTransaction();
                else
                    dal.CommitTransaction();


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
      
    }
}
