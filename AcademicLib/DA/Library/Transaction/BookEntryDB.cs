
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Transaction
{
    internal class BookEntryDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public BookEntryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Library.Transaction.BookEntry beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //BookTitleId,SubjectId,PublicationId,EditionId,[Year],MaterialTypeId,ISBNNo,Volume,Pages,Language,
            //DonarId,DepartmentId,ClassId,MediumId,AcademicYearId,Location,[Status],PurchaseDate,Vendor,BillNo,BookPrice,
            //CreditDays,[Description],NoOfBooks,StartedAccessionNo,EndedAccessionNo,FrontCoverPath,BackCoverPath
            cmd.Parameters.AddWithValue("@BookTitleId", beData.BookTitleId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@PublicationId", beData.PublicationId);
            cmd.Parameters.AddWithValue("@EditionId", beData.EditionId);
            cmd.Parameters.AddWithValue("@Year", beData.Year);
            cmd.Parameters.AddWithValue("@MaterialTypeId", beData.MaterialTypeId);
            cmd.Parameters.AddWithValue("@ISBNNo", beData.ISBNNo);
            cmd.Parameters.AddWithValue("@Volume", beData.Volume);
            cmd.Parameters.AddWithValue("@Pages", beData.Pages);
            cmd.Parameters.AddWithValue("@Language", beData.Language);
            cmd.Parameters.AddWithValue("@DonarId", beData.DonarId);
            cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@MediumId", beData.MediumId);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            cmd.Parameters.AddWithValue("@Location", beData.Location);
            cmd.Parameters.AddWithValue("@Status", beData.Status);
            cmd.Parameters.AddWithValue("@PurchaseDate", beData.PurchaseDate);
            cmd.Parameters.AddWithValue("@Vendor", beData.Vendor);
            cmd.Parameters.AddWithValue("@BillNo", beData.BillNo);            
            cmd.Parameters.AddWithValue("@BookPrice", beData.BookPrice);
            cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@NoOfBooks", beData.NoOfBooks);
            cmd.Parameters.AddWithValue("@StartedAccessionNo", beData.StartedAccessionNo);
            cmd.Parameters.AddWithValue("@EndedAccessionNo", beData.EndedAccessionNo);
            cmd.Parameters.AddWithValue("@FrontCoverPath", beData.FrontCoverPath);
            cmd.Parameters.AddWithValue("@BackCoverPath", beData.BackCoverPath);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@BookEntryId", beData.BookEntryId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateBookEntry";
            }
            else
            {
                cmd.Parameters[30].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddBookEntry";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[31].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[32].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[33].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@FixFineAmount", beData.FixFineAmount);
            cmd.Parameters.AddWithValue("@LateFineAmountPerDay", beData.LateFineAmountPerDay);
            cmd.Parameters.AddWithValue("@BookNo", beData.BookNo);
            cmd.Parameters.AddWithValue("@CallNo", beData.CallNo);

            cmd.Parameters.AddWithValue("@ClassLevelId", beData.ClassLevelId);
            cmd.Parameters.AddWithValue("@FacultyId", beData.FacultyId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
            //ClassLevelId,FacultyId,SemesterId,ClassYearId
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[30].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[30].Value);

                if (!(cmd.Parameters[31].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[31].Value);

                if (!(cmd.Parameters[32].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[32].Value);

                if (!(cmd.Parameters[33].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[33].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                

                if(resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveBookEntryDetails(beData.CUserId, resVal.RId, beData.DetailsList);
                    SaveAuthors(beData.CUserId, resVal.RId, beData.AuthorIdColl);
                }

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
        public ResponeValues getAccessionNo(int UserId)
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
            cmd.CommandText = "usp_GetAccessionNo";
            
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
        private void SaveAuthors(int UserId, int BookEntryId,List<int> authorsColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in authorsColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);
                cmd.Parameters.AddWithValue("@AuthorId", beData);                
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddBookEntryAuthor";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }
        private void SaveBookEntryDetails(int UserId, int BookEntryId,List<BE.Library.Transaction.BookDetails> beDataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in beDataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);
                cmd.Parameters.AddWithValue("@AccessionNo", beData.AccessionNo);
                cmd.Parameters.AddWithValue("@RackId", beData.RackId);
                cmd.Parameters.AddWithValue("@Barcode", beData.BarCode);
                cmd.Parameters.AddWithValue("@SNo", sno);                
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddBookList";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }

        public RE.Library.BookDetailsCollections getBookDetailsList(int UserId,int MaterialTypeId,int ForType)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@MaterialTypeId", MaterialTypeId);
            cmd.Parameters.AddWithValue("@ForType", ForType);
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
                    if (!(reader[23] is DBNull)) beData.BookNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CallNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is DBNull)) beData.Vendor = Convert.ToString(reader[26]);
                    if (!(reader[27] is DBNull)) beData.BillNo = Convert.ToString(reader[27]);
                    if (!(reader[28] is DBNull)) beData.PurchaseDate = Convert.ToString(reader[28]);
                     
                    if (!(reader[29] is DBNull)) beData.Faculty = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.Level = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Semester = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.ClassYear = reader.GetString(32);
                     
                    if (!(reader[33] is DBNull)) beData.Pages = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.DonorName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BookCategory = reader.GetString(35);
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

        public RE.Library.BookDetailsCollections getBookIssueRegister(int UserId, DateTime dateFrom,DateTime dateTo,bool PendingForReceived)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@PendingForReceived", PendingForReceived);
            cmd.CommandText = "usp_GetBookIssueRegister";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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

                    //Added By Suresh on Falgun 25
                    if (!(reader[51] is DBNull)) beData.PurchaseDate_BS = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.Vendor = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.BillNo = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.BookCategory = reader.GetString(54);
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

        public RE.Library.BookDetailsCollections getBookReceivedRegister(int UserId, DateTime dateFrom, DateTime dateTo)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetBookReceivedRegister";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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

                    //Added By Suresh on Falgun 25
                    if (!(reader[51] is DBNull)) beData.PurchaseDate_BS = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.Vendor = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.BillNo = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.BookCategory = reader.GetString(54);
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

        public RE.Library.BookDetailsCollections getBookRegister(int UserId,int BookEntryId)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);            
            cmd.CommandText = "usp_GetBookRegister";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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
                    if (!(reader[51] is DBNull)) beData.BookCategory = reader.GetString(51);
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

        public RE.Library.BookDetailsCollections getStudentEmpBookRegister(int UserId, int? StudentId,int? EmployeeId)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetStudentEmpBookRegister";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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
                    if (!(reader[40] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.B_Faculty = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.B_Level = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.B_Semester = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.B_ClassYear = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.BookCategory = reader.GetString(45);
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
                    beData.BookEntryId = reader.GetInt32(0);
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
        public BE.Library.Transaction.BookEntry getBookEntryById(int UserId, int EntityId, int BookEntryId)
        {
            BE.Library.Transaction.BookEntry beData = new BE.Library.Transaction.BookEntry();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetBookEntryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Library.Transaction.BookEntry();
                    beData.BookEntryId = reader.GetInt32(0);
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
                    if (!(reader[29] is DBNull)) beData.BookNo = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.CallNo = reader.GetString(30);                    
                    if (!(reader[31] is DBNull)) beData.ClassLevelId = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.FacultyId = reader.GetInt32(32);
                    if (!(reader[33] is DBNull)) beData.SemesterId = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.ClassYearId = reader.GetInt32(34);

                    //New Field Added By Suresh on 15 Magh
                    if (!(reader[35] is DBNull)) beData.FixFineAmount = Convert.ToDouble(reader[35]);
                    if (!(reader[36] is DBNull)) beData.LateFineAmountPerDay = Convert.ToDouble(reader[36]);
                    if (!(reader[37] is DBNull)) beData.CategoryId = reader.GetInt32(37);

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
        public ResponeValues DeleteById(int UserId, int EntityId, int BookEntryId,int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);
            cmd.CommandText = "usp_DelBookEntryById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@TranId", TranId);
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

        public BE.Library.Transaction.BookAutoCompleteCollections getAllBookAutoComplete(int UserId, string searchBy, string Operator, string searchValue,bool forReport)
        {
            BE.Library.Transaction.BookAutoCompleteCollections dataColl = new BE.Library.Transaction.BookAutoCompleteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ColName", searchBy);
            cmd.Parameters.AddWithValue("@ColValue", searchValue);
            cmd.Parameters.AddWithValue("@Operator", Operator);
            cmd.Parameters.AddWithValue("@forReport", forReport);
            cmd.CommandText = "usp_GetAllBookDetailsAutoComplete";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Transaction.BookAutoComplete beData = new BE.Library.Transaction.BookAutoComplete();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AccessionNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Barcode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BookTitle = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Subject = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Publication = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Authors = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.MaterialType = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Department = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FrontCoverPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BackCoverPath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Location = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.MediumName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BookNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.CallNo = reader.GetString(15);
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

        public BE.Library.Transaction.BookAutoComplete getBookDetailsByBarcode(int UserId, string Barcode)
        {
            BE.Library.Transaction.BookAutoComplete beData  = new BE.Library.Transaction.BookAutoComplete();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Barcode",Barcode);
            cmd.CommandText = "usp_GetBookDetailsByBarCode";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                     beData = new BE.Library.Transaction.BookAutoComplete();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AccessionNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Barcode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BookTitle = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Subject = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Publication = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Authors = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.MaterialType = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Department = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FrontCoverPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BackCoverPath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Location = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.MediumName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.CreditDays = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.BookCategory = reader.GetString(15);
                    beData.IsSuccess = true;
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                }else
                {
                    beData.IsSuccess = false;
                    beData.ResponseMSG = "No Book Data Found";
                }
                reader.Close();
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

        public BE.Library.Transaction.BookDetailsForBarCodeCollections getBookForPrintBarcode(int UserId, int fromAccessionNo,int toAccessionNo)
        {
            BE.Library.Transaction.BookDetailsForBarCodeCollections dataColl = new BE.Library.Transaction.BookDetailsForBarCodeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromAccessionNo", fromAccessionNo);
            cmd.Parameters.AddWithValue("@ToAccessionNo", toAccessionNo);            
            cmd.CommandText = "usp_GetBookListForBarcode";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Transaction.BookDetailsForBarCode beData = new BE.Library.Transaction.BookDetailsForBarCode();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BookEntryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.AccessionNo = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.BarCode = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RackName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.BookTitle = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Publication = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ClassName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ISBSNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Medium = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Department = reader.GetString(12);
                    
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
        public RE.Library.StudentLedgerCollections getStudentLedger(int UserId,int AcademicYearId, int StudentId)
        {
            RE.Library.StudentLedgerCollections dataColl = new RE.Library.StudentLedgerCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentLibBookLedger";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.StudentLedger beData = new RE.Library.StudentLedger();
                    beData.SNo = sno;                    
                    if (!(reader[0] is DBNull)) beData.IssueNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Subject = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.IssueDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.IssueMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CreditDays = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) beData.IssueRemarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ReceivedType = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FineAmount = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.ReturnDate = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.ReturnMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AccessionNo = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.BookTitle = reader.GetString(11);

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

        public RE.Library.BookDetailsCollections getAllBookList(int UserId, string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, int ForType)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SubjectIdColl", SubjectIdColl);
            cmd.Parameters.AddWithValue("@AuthorIdColl", AuthorIdColl);
            cmd.Parameters.AddWithValue("@PublicationIdColl", PublicationIdColl);
            cmd.Parameters.AddWithValue("@EditionIdColl", EditionIdColl);
            cmd.Parameters.AddWithValue("@CategoryIdColl", CategoryIdColl);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@FacultyIdColl", FacultyIdColl);
            cmd.Parameters.AddWithValue("@LevelIdColl", LevelIdColl);
            cmd.Parameters.AddWithValue("@SemesterIdColl", SemesterIdColl);
            cmd.Parameters.AddWithValue("@ClassYearIdColl", ClassYearIdColl);
            cmd.Parameters.AddWithValue("@ForType", ForType);
            cmd.CommandText = "usp_GetAllLibraryBook";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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
                    if (!(reader[23] is DBNull)) beData.BookNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CallNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BookPrice = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is DBNull)) beData.Vendor = Convert.ToString(reader[26]);
                    if (!(reader[27] is DBNull)) beData.BillNo = Convert.ToString(reader[27]);
                    if (!(reader[28] is DBNull)) beData.PurchaseDate = Convert.ToString(reader[28]);
                    if (!(reader[29] is DBNull)) beData.Faculty = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.Level = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Semester = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.ClassYear = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.Pages = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.DonorName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BookCategory = reader.GetString(35);
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


        public RE.Library.BookDetailsCollections getAllBooksTaken(int UserId, string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, bool PendingForReceived)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SubjectIdColl", SubjectIdColl);
            cmd.Parameters.AddWithValue("@AuthorIdColl", AuthorIdColl);
            cmd.Parameters.AddWithValue("@PublicationIdColl", PublicationIdColl);
            cmd.Parameters.AddWithValue("@EditionIdColl", EditionIdColl);
            cmd.Parameters.AddWithValue("@CategoryIdColl", CategoryIdColl);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@FacultyIdColl", FacultyIdColl);
            cmd.Parameters.AddWithValue("@LevelIdColl", LevelIdColl);
            cmd.Parameters.AddWithValue("@SemesterIdColl", SemesterIdColl);
            cmd.Parameters.AddWithValue("@ClassYearIdColl", ClassYearIdColl);
            cmd.Parameters.AddWithValue("@PendingForReceived", PendingForReceived);
            cmd.CommandText = "usp_GetAllBookTakenList";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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




        public RE.Library.BookDetailsCollections getAllBooksReturned(int UserId, string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            RE.Library.BookDetailsCollections dataColl = new RE.Library.BookDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SubjectIdColl", SubjectIdColl);
            cmd.Parameters.AddWithValue("@AuthorIdColl", AuthorIdColl);
            cmd.Parameters.AddWithValue("@PublicationIdColl", PublicationIdColl);
            cmd.Parameters.AddWithValue("@EditionIdColl", EditionIdColl);
            cmd.Parameters.AddWithValue("@CategoryIdColl", CategoryIdColl);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@FacultyIdColl", FacultyIdColl);
            cmd.Parameters.AddWithValue("@LevelIdColl", LevelIdColl);
            cmd.Parameters.AddWithValue("@SemesterIdColl", SemesterIdColl);
            cmd.Parameters.AddWithValue("@ClassYearIdColl", ClassYearIdColl);
            cmd.CommandText = "usp_GetBooksReturned";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Library.BookDetails beData = new RE.Library.BookDetails();
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

        public ResponeValues CheckBookInUse(int UserId, int BookEntryId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BookEntryId", BookEntryId);
            cmd.CommandText = "usp_CheckBookInUse";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

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
