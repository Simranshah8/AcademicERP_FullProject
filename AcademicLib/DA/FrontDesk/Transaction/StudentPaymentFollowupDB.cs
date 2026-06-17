using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class StudentPaymentFollowupDB
    {
        DataAccessLayer1 dal = null;
        public StudentPaymentFollowupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.StudentPaymentFollowup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@UptoMonthId", beData.UptoMonthId);
            cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
            cmd.Parameters.AddWithValue("@PaymentDueDate", beData.PaymentDueDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@NextFollowupRequired", beData.NextFollowupRequired);
            cmd.Parameters.AddWithValue("@NextFollowupDateTime", beData.NextFollowupDateTime);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGatePass";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentPaymentFollowup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@RefTranId", beData.RefTranId);
            cmd.Parameters.AddWithValue("@DuesAmt", beData.DuesAmt);
            cmd.Parameters.AddWithValue("@StatusId", beData.StatusId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

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

        public ResponeValues SaveClosed(int UserId,int RefTranId,string Remarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@RefTranId", RefTranId);
            cmd.Parameters.AddWithValue("@Remarks", Remarks); 
            cmd.CommandText = "usp_AddStudentPaymentClosed";
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
        public RE.FrontDesk.StudentPaymentFollowupCollections getStudentWiseList(int UserId, int? StudentId,int? AcademicYearId)
        {
            RE.FrontDesk.StudentPaymentFollowupCollections dataColl = new RE.FrontDesk.StudentPaymentFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentFollowupList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.StudentPaymentFollowup beData = new RE.FrontDesk.StudentPaymentFollowup();                                       
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ForMonth = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.PaymentDueDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.PaymentDueMiti = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FollowupDate = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.FollowupMiti = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FollowupBy = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.NextFollowupDateTime = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.NextFollowupMiti = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IsClosed = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.ClosedRemarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.ClosedMiti = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClosedBy = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.StatusId = reader.GetInt32(17);
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

        public RE.FrontDesk.StudentPaymentFollowupCollections getFollowupList(int UserId, DateTime? dateFrom,DateTime? dateTo)
        {
            RE.FrontDesk.StudentPaymentFollowupCollections dataColl = new RE.FrontDesk.StudentPaymentFollowupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetStudentPaymentFollowupList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.FrontDesk.StudentPaymentFollowup beData = new RE.FrontDesk.StudentPaymentFollowup();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ForMonth = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.PaymentDueDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.PaymentDueMiti = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FollowupDate = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.FollowupMiti = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FollowupBy = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.NextFollowupDateTime = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.NextFollowupMiti = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IsClosed = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.ClosedRemarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClosedDateTime = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.ClosedMiti = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClosedBy = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[16]);

                    if (!(reader[17] is DBNull)) beData.StudentId = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.Name = Convert.ToString(reader[18]);
                    if (!(reader[19] is DBNull)) beData.ClassName = Convert.ToString(reader[19]);
                    if (!(reader[20] is DBNull)) beData.SectionName = Convert.ToString(reader[20]);
                    if (!(reader[21] is DBNull)) beData.RollNo = Convert.ToInt32(reader[21]);
                    if (!(reader[22] is DBNull)) beData.RegNo = Convert.ToString(reader[22]);
                    if (!(reader[23] is DBNull)) beData.FatherName = Convert.ToString(reader[23]);
                    if (!(reader[24] is DBNull)) beData.F_ContactNo = Convert.ToString(reader[24]);
                    if (!(reader[25] is DBNull)) beData.Email = Convert.ToString(reader[25]);

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
