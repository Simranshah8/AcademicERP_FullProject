using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Transaction
{
    internal class BookLostDB
    {
        DataAccessLayer1 dal = null;

        public BookLostDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveBookLost(List<BE.Library.Transaction.BookLostDamage> dataColl)
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
                    cmd.Parameters.AddWithValue("@LostDate", beData.LostDate);
                    cmd.Parameters.AddWithValue("@LostTypeId", beData.LostTypeId);
                    cmd.Parameters.AddWithValue("@ActionTypeId", beData.ActionTypeId);
                    cmd.Parameters.AddWithValue("@LostRemarks", beData.LostRemarks);
                    cmd.Parameters.AddWithValue("@RecoveryAmount", beData.RecoveryAmount);
                    cmd.Parameters.AddWithValue("@IsBookSubmitted", beData.IsBookSubmitted);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.CommandText = "usp_AddBookLostDamage";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[8].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                    if (!(cmd.Parameters[10].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

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

        public BE.Library.Transaction.BookLostCollections GetBookLostDamages(int UserId, DateTime? dateFrom, DateTime? dateTo)
        {
            BE.Library.Transaction.BookLostCollections dataColl = new BE.Library.Transaction.BookLostCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetBookLost";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Transaction.BookLostDamage beData = new BE.Library.Transaction.BookLostDamage();
                    beData.SNo = sno;
                    beData.TranId = reader.GetInt32(0);
                    beData.BookEntryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AccessionNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.BarCode = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BookTitle = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Subject = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Publication = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Edition = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MaterialType = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Department = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Medium = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Authors = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Year = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ISBNNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Volume = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Rack = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Location = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Language = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Status = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.CreditDays = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.StartedAccessionNo = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.EndedAccessionNo = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.IssueTo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Name = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.RegdNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.IssueDate_AD = reader.GetDateTime(26);
                    if (!(reader[27] is DBNull)) beData.IssueDate_BS = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.TotalDays = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.ReturnDate_AD = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.ReturnDate_BS = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.CreditDays = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.IssueRemarks = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.ReturnRemarks = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.IssueBy = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.ReceiedBy = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.FineAmount = Convert.ToDouble(reader[36]);
                    if (!(reader[37] is DBNull)) beData.OutStandingDays = Convert.ToInt32(reader[37]);
                    if (!(reader[38] is DBNull)) beData.BookNo = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CallNo = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.SectionName = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.Batch = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Faculty = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.Level = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.Semester = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.ClassYear = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[46]);
                    if (!(reader[47] is DBNull)) beData.B_Faculty = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.B_Level = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.B_Semester = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.B_ClassYear = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.PurchaseDate_BS = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.Vendor = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.BillNo = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.BookCategory = reader.GetString(54);

                    if (!(reader[55] is DBNull)) beData.LostDamage = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.LostBy = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.LostDate = Convert.ToDateTime(reader[57]);
                    if (!(reader[58] is DBNull)) beData.LostDateBS = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.ActionTypeId = reader.GetInt32(59);
                    if (!(reader[60] is DBNull)) beData.ActionType = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.LostRemarks = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.RecoveryAmount = Convert.ToDouble(reader[62]);
                    if (!(reader[63] is DBNull)) beData.IsBookSubmitted = Convert.ToBoolean(reader[63]);
                    if (!(reader[64] is DBNull)) beData.IssueId = reader.GetInt32(64);
                    dataColl.Add(beData);
                    sno++;
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

        public ResponeValues UpdateBookLostAction(int UserId, int IssueId, double? RecoveryAmount, bool? IsBookSubmitted)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IssueId", IssueId);
            cmd.Parameters.AddWithValue("@RecoveryAmount", RecoveryAmount);
            cmd.CommandText = "usp_UpdateBookLostAction";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsBookSubmitted", IsBookSubmitted);
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
