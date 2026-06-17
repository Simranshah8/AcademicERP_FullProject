using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Transaction
{
    internal  class BookIssueDB
    {
        DataAccessLayer1 dal = null;
        public BookIssueDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int AcademicYearId, BE.Library.Transaction.BookIssue beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@IssueDate", beData.IssueDate);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateBookIssue";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddBookIssue";
            }
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

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";


                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    var rVal= SaveBookIssueDetails(beData.CUserId, resVal.RId, beData.DetailsColl);                    
                    if(!rVal.IsSuccess)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = rVal.ResponseMSG;
                        dal.RollbackTransaction();
                    }
                    else
                    {
                        dal.CommitTransaction();
                    }
                }

                
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
        public ResponeValues getBookIssueNo(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetBookIssueNo";

            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

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
      
        private ResponeValues SaveBookIssueDetails(int UserId, int TranId, List<BE.Library.Transaction.BookIssueDetails> beDataColl)
        {
            ResponeValues resVal = new ResponeValues();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in beDataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.Parameters.AddWithValue("@BookEntryId", beData.BookEntryId);
                cmd.Parameters.AddWithValue("@IssueDate", beData.IssueDate);
                cmd.Parameters.AddWithValue("@IssueRemarks", beData.IssueRemarks);
                cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);                                
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddBookIssueDetails";
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!resVal.IsSuccess)
                    return resVal;
                sno++;
            }

            return resVal;
        }

        public BE.Library.Transaction.PreviousBookCollections getPreviousBookDueList(int UserId,int? StudentId,int? EmployeeId)
        {
            BE.Library.Transaction.PreviousBookCollections dataColl = new BE.Library.Transaction.PreviousBookCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetPreviousBook";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Transaction.PreviousBook beData = new BE.Library.Transaction.PreviousBook();                    
                    beData.TranId = reader.GetInt32(0);
                    beData.AccessionNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BookTitle = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Edition = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Publication = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Department = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.IssueDate_AD = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.IssueDate_BS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.CreditDays = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.IssueRemarks = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.OutStandingDays = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.IssueId = Convert.ToInt32(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Barcode = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.FronCoverPath = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.BackCoverPath = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Authors = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.ClassName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Medium = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.BookEntryId = Convert.ToInt32(reader[19]);

                    if (!(reader[20] is DBNull)) beData.Faculty = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Level = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Semester = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.ClassYear = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.BookCategory = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Subject = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[26]);
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

        public BE.Library.Transaction.CreditRules getCreditRules(int UserId,int? StudentId,int? EmployeeId)
        {
            BE.Library.Transaction.CreditRules beData = new BE.Library.Transaction.CreditRules();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetLibraryRules";
            try
            {                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beData = new BE.Library.Transaction.CreditRules();             
                    if (!(reader[0] is DBNull)) beData.BookLimit = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.CreditDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TotalIssueBook = reader.GetInt32(2);
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
        public RE.Library.BookDetailsCollections getBookDetailsList(int UserId)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetBookList";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
                    beData.SNo = sno;
                    beData.TranId = reader.GetInt32(0);
                    beData.TranId = reader.GetInt32(1);
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
                    if (!(reader[23] is DBNull)) beData.Faculty = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Level = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Semester = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.ClassYear = reader.GetString(26);

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

        public BE.Library.Transaction.BookEntryCollections getAllBookEntry(int UserId, int EntityId)
        {
            BE.Library.Transaction.BookEntryCollections dataColl = new BE.Library.Transaction.BookEntryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllBookEntry";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Transaction.BookEntry beData = new BE.Library.Transaction.BookEntry();
                    //beData.TranId = reader.GetInt32(0);
                    // if (!(reader[1] is DBNull)) beData.AccessionNo = reader.GetString(1);
                    //  if (!(reader[2] is DBNull)) beData.BarCode = reader.GetString(2);
                    //if (!(reader[3] is DBNull)) beData.BookTitle = reader.GetString(3);
                    //if (!(reader[4] is DBNull)) beData.Author = reader.GetString(4);
                    //if (!(reader[5] is DBNull)) beData.Publication = reader.GetString(5);
                    //if (!(reader[6] is DBNull)) beData.Edition = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Volume = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ISBNNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Language = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.DonarId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.ClassId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.MediumId = reader.GetInt32(12);
                    // if (!(reader[13] is DBNull)) beData.RackId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Location = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PurchaseDate = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.Vendor = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.BookPrice = reader.GetInt32(17);
                    //if (!(reader[18] is DBNull)) beData.CreditDays = reader.GetString(18);
                    //if (!(reader[19] is DBNull)) beData.NoOfBooks = reader.GetInt32(19);
                    //if (!(reader[20] is DBNull)) beData.StartedBookCode = reader.GetString(20);
                    //if (!(reader[21] is DBNull)) beData.Image = reader.GetString(21);
                    //if (!(reader[22] is DBNull)) beData.ImagePath = reader.GetString(22);
                    //if (!(reader[23] is DBNull)) beData.SubjectId = reader.GetString(23);
                    //if (!(reader[24] is DBNull)) beData.Year = reader.GetString(24);
                    //if (!(reader[25] is DBNull)) beData.Pages = reader.GetInt32(25);
                    //if (!(reader[26] is DBNull)) beData.DepartmentId = reader.GetInt32(26);
                    //if (!(reader[27] is DBNull)) beData.AcademicYearId = reader.GetInt32(27);
                    //if (!(reader[28] is DBNull)) beData.Status = reader.GetInt32(28);
                    //if (!(reader[29] is DBNull)) beData.BillNo = reader.GetString(29);
                    //if (!(reader[30] is DBNull)) beData.Description = reader.GetString(30);
                    //if (!(reader[31] is DBNull)) beData.EndedBookCode = reader.GetString(31);
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
        public BE.Library.Transaction.BookEntry getBookEntryById(int UserId, int EntityId, int TranId)
        {
            BE.Library.Transaction.BookEntry beData = new BE.Library.Transaction.BookEntry();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetBookEntryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Library.Transaction.BookEntry();
                    //beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PublicationId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EditionId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Year = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MaterialTypeId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ISBNNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Volume = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Pages = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Language = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.DonarId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.DepartmentId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.ClassId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.MediumId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.AcademicYearId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.Location = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Status = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.PurchaseDate = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.Vendor = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.BillNo = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[20]);
                    if (!(reader[21] is DBNull)) beData.CreditDays = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.Description = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.NoOfBooks = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.StartedAccessionNo = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.EndedAccessionNo = reader.GetInt32(25);
                    if (!(reader[26] is DBNull)) beData.FrontCoverPath = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.BackCoverPath = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.BookTitleId = reader.GetInt32(28);

                }
                reader.NextResult();
                beData.AuthorIdColl = new List<int>();
                beData.DetailsList = new List<BE.Library.Transaction.BookDetails>();
                while (reader.Read())
                {
                    beData.AuthorIdColl.Add(reader.GetInt32(0));
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Library.Transaction.BookDetails det = new BE.Library.Transaction.BookDetails();
                    if (!(reader[0] is DBNull)) det.AccessionNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.BarCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.RackId = reader.GetInt32(2);
                    beData.DetailsList.Add(det);
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
            cmd.CommandText = "usp_DelBookEntryById";
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
