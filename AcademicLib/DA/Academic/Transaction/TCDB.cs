using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class TCDB
    {
        DataAccessLayer1 dal = null;
        public TCDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.TC beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@RegdNo", beData.RegdNo);
            cmd.Parameters.AddWithValue("@AdmitDate", beData.AdmitDate);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@Caste", beData.Caste);
            cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
            cmd.Parameters.AddWithValue("@BloodGroup", beData.BloodGroup);
            cmd.Parameters.AddWithValue("@Moral", beData.Moral);
            cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
            cmd.Parameters.AddWithValue("@SectionName", beData.SectionName);
            cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);
            cmd.Parameters.AddWithValue("@AcademicYear", beData.AcademicYear);
            cmd.Parameters.AddWithValue("@Medium", beData.Medium);
            cmd.Parameters.AddWithValue("@StudentType", beData.StudentType);
            cmd.Parameters.AddWithValue("@TransportPoint", beData.TransportPoint);
            cmd.Parameters.AddWithValue("@BoarderType", beData.BoarderType);
            cmd.Parameters.AddWithValue("@HouseName", beData.HouseName);
            cmd.Parameters.AddWithValue("@HouseDress", beData.HouseDress);
            cmd.Parameters.AddWithValue("@Board", beData.Board);
            cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
            cmd.Parameters.AddWithValue("@FullAddress", beData.FullAddress);
            cmd.Parameters.AddWithValue("@Province", beData.Province);
            cmd.Parameters.AddWithValue("@District", beData.District);
            cmd.Parameters.AddWithValue("@LocalLevel", beData.LocalLevel);
            cmd.Parameters.AddWithValue("@WardNo", beData.WardNo);
            cmd.Parameters.AddWithValue("@StreetName", beData.StreetName);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@FatherContactNo", beData.FatherContactNo);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@PreviousClass", beData.PreviousClass);
            cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
            cmd.Parameters.AddWithValue("@Percentage", beData.Percentage);
            cmd.Parameters.AddWithValue("@Division", beData.Division);
            cmd.Parameters.AddWithValue("@Grade", beData.Grade);
            cmd.Parameters.AddWithValue("@GPA", beData.GPA);
            cmd.Parameters.AddWithValue("@IsLeft", beData.IsLeft);
            cmd.Parameters.AddWithValue("@LeftDate", beData.LeftDate);
            cmd.Parameters.AddWithValue("@IssueDate", beData.IssueDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateTC";
            }
            else
            {
                cmd.Parameters[46].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddTC";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[47].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[48].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[49].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@UDF", beData.UDF);
            cmd.Parameters.AddWithValue("@EMSId", beData.EMSId);
            cmd.Parameters.AddWithValue("@PassOutClassId", beData.PassOutClassId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[46].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[46].Value);

                if (!(cmd.Parameters[47].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[47].Value);

                if (!(cmd.Parameters[48].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[48].Value);

                if (!(cmd.Parameters[49].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[49].Value);

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

        public ResponeValues Request(BE.Academic.Transaction.TCCCRequest beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@DOB", beData.DOB);
            cmd.Parameters.AddWithValue("@RegdNo", beData.RegdNo);
            cmd.Parameters.AddWithValue("@IPAddress", beData.IPAddress);
            cmd.Parameters.AddWithValue("@Browser", beData.Browser);
            cmd.Parameters.AddWithValue("@UserAgent", beData.UserAgent);
            cmd.CommandText = "usp_AddTCCRequest";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);            
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@BoardRegNo", beData.BoardRegNo);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

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
        public BE.Academic.Transaction.TC getStudentDetailsForTCCC(int UserId, int StudentId,int AcademicYearId)
        {
            BE.Academic.Transaction.TC beData = new BE.Academic.Transaction.TC();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentDetailsForTCCC";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beData = new BE.Academic.Transaction.TC();
                    beData.StudentId = reader.GetInt32(0);
                    beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AdmitDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.FirstName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MiddleName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.LastName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Caste = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DOB_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.BloodGroup = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ClassId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.SectionId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.ClassName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.SectionName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.RollNo = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.AcademicYear = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Medium = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.StudentType = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.TransportPoint = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BoarderType = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.HouseName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.HouseDress = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.Board = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.BoardRegNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.FullAddress = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.Province = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.District = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.LocalLevel = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.WardNo = reader.GetInt32(29);
                    if (!(reader[30] is DBNull)) beData.StreetName = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.FatherName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.FatherContactNo = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.MotherName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.PreviousClass = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.SymbolNo = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.Percentage = Convert.ToDouble(reader[36]);
                    if (!(reader[37] is DBNull)) beData.Division = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.Grade = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.GPA = Convert.ToDouble(reader[39]);
                    if (!(reader[40] is DBNull)) beData.IsLeft = reader.GetBoolean(40);
                    if (!(reader[41] is DBNull)) beData.LeftDate = reader.GetDateTime(41);
                    if (!(reader[42] is DBNull)) beData.LeftRemarks = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.IssueDate = reader.GetDateTime(43);
                    if (!(reader[44] is DBNull)) beData.Remarks = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.Moral = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.LeftDate_BS = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.DuesAmt= Convert.ToDouble(reader[47]);


                    try
                    {

                        if (!(reader[48] is DBNull)) beData.PassOutClassId = Convert.ToInt32(reader[48]);

                        if (string.IsNullOrEmpty(beData.SymbolNo))
                        {
                            if (!(reader[49] is DBNull)) beData.SymbolNo = Convert.ToString(reader[49]);
                        }

                        if (beData.GPA == 0)
                        {
                            if (!(reader[50] is DBNull)) beData.GPA = Convert.ToDouble(reader[50]);
                        }

                        if (string.IsNullOrEmpty(beData.Grade))
                        {
                            if (!(reader[51] is DBNull)) beData.Grade = Convert.ToString(reader[51]);
                        }

                        if (string.IsNullOrEmpty(beData.Remarks))
                        {
                            if (!(reader[52] is DBNull)) beData.Remarks = Convert.ToString(reader[52]);
                        }

                        if (!(reader[53] is DBNull)) beData.EMSId = Convert.ToString(reader[53]);
                        
                        if (!(reader[54] is DBNull)) beData.Nationality = Convert.ToString(reader[54]);

                    }
                    catch { }


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

        public BE.Academic.Transaction.TCCollections getAllTC(int UserId)
        {
            BE.Academic.Transaction.TCCollections dataColl = new BE.Academic.Transaction.TCCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetAllTC";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.TC beData = new BE.Academic.Transaction.TC();
                    beData.TranId = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.AdmitDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.AdmitDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FirstName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MiddleName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.LastName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Gender = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DOB_AD = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.DOB_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BloodGroup = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Moral = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.RollNo = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.AcademicYear = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Medium = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.StudentType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.TransportPoint = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BoarderType = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.HouseName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.HouseDress = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Board = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BoardRegNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.FullAddress = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.Province = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.District = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.LocalLevel = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.WardNo = reader.GetInt32(30);
                    if (!(reader[31] is DBNull)) beData.StreetName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.FatherName = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.MotherName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.FatherContactNo = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.PreviousClass = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.SymbolNo = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.Percentage = Convert.ToDouble(reader[37]);
                    if (!(reader[38] is DBNull)) beData.Division = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.Grade = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.GPA = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.IssueDate = reader.GetDateTime(41);
                    if (!(reader[42] is DBNull)) beData.IssueDate_BS = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.Remarks = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.UserName = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.LogDateTime_AD = reader.GetDateTime(45);
                    if (!(reader[46] is DBNull)) beData.LogDate_BS = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.AutoNumber = reader.GetInt32(47);
                    if (!(reader[48] is DBNull)) beData.UDF = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.EMSId = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.Nationality = reader.GetString(50);
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

        public BE.Academic.Transaction.TC getTCByStudentId(int UserId,int StudentId,int? AcademicYearId)
        {
            BE.Academic.Transaction.TC beData = new BE.Academic.Transaction.TC();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetTCForEdit";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.TC();
                    beData.TranId = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.AdmitDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.AdmitDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FirstName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MiddleName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.LastName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Gender = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Caste = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DOB_AD = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.DOB_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BloodGroup = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Moral = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.RollNo = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.AcademicYear = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Medium = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.StudentType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.TransportPoint = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BoarderType = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.HouseName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.HouseDress = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.Board = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.BoardRegNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.FullAddress = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.Province = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.District = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.LocalLevel = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.WardNo = reader.GetInt32(30);
                    if (!(reader[31] is DBNull)) beData.StreetName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.FatherName = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.MotherName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.FatherContactNo = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.PreviousClass = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.SymbolNo = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.Percentage = Convert.ToDouble(reader[37]);
                    if (!(reader[38] is DBNull)) beData.Division = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.Grade = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.GPA = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.IssueDate = reader.GetDateTime(41);
                    if (!(reader[42] is DBNull)) beData.IssueDate_BS = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.Remarks = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.UserName = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.LogDateTime_AD = reader.GetDateTime(45);
                    if (!(reader[46] is DBNull)) beData.LogDate_BS = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.AutoNumber = reader.GetInt32(47);
                    if (!(reader[48] is DBNull)) beData.UDF = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.EMSId = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.Nationality = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.IsLeft = Convert.ToBoolean(reader[51]);
                    if (!(reader[52] is DBNull)) beData.LeftDate = reader.GetDateTime(52);
                    if (!(reader[53] is DBNull)) beData.LeftDate_BS = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[54]);
                    if (!(reader[55] is DBNull)) beData.PassOutClassId = reader.GetInt32(55);
                    beData.IsSuccess = true;
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                }else
                {
                    beData.IsSuccess = false;
                    beData.ResponseMSG = "Not Issue";
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelTCById";
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

        public RE.Academic.TCCCRequestCollections getAllRequest(int UserId,int? AcademicYearId, int? ReportType)
        {
            RE.Academic.TCCCRequestCollections dataColl = new RE.Academic.TCCCRequestCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ReportType", ReportType);
            cmd.CommandText = "usp_GetTCCCRequestList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                int sno = 1;
                while (reader.Read())
                {
                    RE.Academic.TCCCRequest beData = new RE.Academic.TCCCRequest();
                    beData.SNo = sno;
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Class = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Section = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Batch = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Faculty = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Semester = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ClassYear = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RequestDateTime = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.RequestMiti = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IPAddress = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Browser = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.UserAgent = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IsIssueTC = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsIssueCC = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.BoardRegNo = reader.GetString(16);
                    beData.RequestMiti = beData.RequestMiti + " " + beData.RequestDateTime.ToShortTimeString();

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
    }
}
