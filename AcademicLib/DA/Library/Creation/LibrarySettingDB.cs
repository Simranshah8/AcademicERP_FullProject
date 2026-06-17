using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Library.Creation
{
    internal class LibrarySettingDB
    {
        DataAccessLayer1 dal = null;
        public LibrarySettingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveLibrarySettingStudent(BE.Library.Creation.LibrarySetting beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForStudentTeacher", 1);
            cmd.Parameters.AddWithValue("@BookLimit", beData.BookLimit);
            cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
            cmd.Parameters.AddWithValue("@FixFineAmount", beData.FixFineAmount);
            cmd.Parameters.AddWithValue("@LateFineAmountPerDay", beData.LateFineAmountPerDay);
            cmd.Parameters.AddWithValue("@SlabWiseFine", beData.SlabWiseFine);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);                        
            cmd.CommandText = "usp_AddLibrarySetting";
            cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;            
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0 && beData.FineRuleColl!=null)
                    SaveFineRulesStudent(beData.CUserId, resVal.RId, beData.FineRuleColl);
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
        private void SaveFineRulesStudent(int UserId, int TranId, List<BE.Library.Creation.LibraryFineRule> fineColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in fineColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@FromDays", beData.FromDays);
                cmd.Parameters.AddWithValue("@ToDays", beData.ToDays);
                cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddLibraryFineRule";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }
        public ResponeValues SaveLibrarySettingTeacher(BE.Library.Creation.LibrarySetting beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForStudentTeacher", 2);
            cmd.Parameters.AddWithValue("@BookLimit", beData.BookLimit);
            cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
            cmd.Parameters.AddWithValue("@FixFineAmount", beData.FixFineAmount);
            cmd.Parameters.AddWithValue("@LateFineAmountPerDay", beData.LateFineAmountPerDay);
            cmd.Parameters.AddWithValue("@SlabWiseFine", beData.SlabWiseFine);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.CommandText = "usp_AddLibrarySetting";
            cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0 && beData.FineRuleColl != null)
                    SaveFineRulesTeacher(beData.CUserId, resVal.RId, beData.FineRuleColl);
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
        private void SaveFineRulesTeacher(int UserId, int TranId, List<BE.Library.Creation.LibraryFineRule> fineColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in fineColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@FromDays", beData.FromDays);
                cmd.Parameters.AddWithValue("@ToDays", beData.ToDays);
                cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddLibraryFineRule";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }
        public ResponeValues SaveLibrarySettingClassWise(int UserId, List<BE.Library.Creation.LibraryClassWiseSetting> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                //TODO: Delete value if exist before add 
                foreach (var fData in dataColl)
                {
                    cmd.CommandText = "usp_DelLibraryClassWiseSetting";
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", fData.TranId);
                    cmd.Parameters.AddWithValue("@ClassId", fData.ClassId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                foreach (var beData in dataColl)
                {
                    if (!beData.ClassId.HasValue)
                        continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@BookLimit", beData.BookLimit);
                    cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
                    cmd.Parameters.AddWithValue("@FixFineAmount", beData.FixFineAmount);
                    cmd.Parameters.AddWithValue("@LateFineAmountPerDay", beData.LateFineAmountPerDay);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_AddLibraryClassWiseSetting";
                    cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;

                    //Added By Suresh
                    cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[6].Value is DBNull))
                        resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                    if (!(cmd.Parameters[7].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                    if (!(cmd.Parameters[8].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                    if (resVal.IsSuccess && resVal.RId > 0 && beData.FineRuleColl != null)
                        SaveFineRulesClassWise(beData.CUserId, resVal.RId, beData.FineRuleColl);
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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
        private void SaveFineRulesClassWise(int UserId, int TranId, List<BE.Library.Creation.LibraryFineRule> fineColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in fineColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@FromDays", beData.FromDays);
                cmd.Parameters.AddWithValue("@ToDays", beData.ToDays);
                cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddLibraryClassWiseFineRule";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }

        public AcademicLib.BE.Library.Creation.Setup getSetting(int UserId)
        {
            AcademicLib.BE.Library.Creation.Setup setup = new BE.Library.Creation.Setup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetLibrarySetting";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibrarySetting beData = new BE.Library.Creation.LibrarySetting();                    
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ForStudentTeacher = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BookLimit = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.CreditDays = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FixFineAmount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LateFineAmountPerDay = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.SlabWiseFine = reader.GetBoolean(6);

                    if (beData.ForStudentTeacher == 1)
                        setup.Student = beData;
                    else
                        setup.Teacher = beData;
                }
                reader.NextResult();
                while(reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibraryFineRule beData = new BE.Library.Creation.LibraryFineRule();
                    int tranid = 0;
                    if (!(reader[0] is DBNull)) tranid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ToDays = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FineAmount = Convert.ToDouble(reader[3]);
                    if (setup.Student.TranId == tranid)
                        setup.Student.FineRuleColl.Add(beData);
                    else if (setup.Teacher.TranId == tranid)
                        setup.Teacher.FineRuleColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibraryClassWiseSetting beData = new BE.Library.Creation.LibraryClassWiseSetting();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BookLimit = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.CreditDays = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FixFineAmount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LateFineAmountPerDay = Convert.ToDouble(reader[5]);

                    //Added By Suresh on 21 Magh starts
                    if (!(reader[6] is DBNull)) beData.BatchId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.SemesterId = reader.GetInt32(8);
                    setup.ClassWise.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibraryFineRule beData = new BE.Library.Creation.LibraryFineRule();
                    int tranid = 0;
                    if (!(reader[0] is DBNull)) tranid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ToDays = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FineAmount = Convert.ToDouble(reader[3]);
                    setup.ClassWise.Find(p1 => p1.TranId == tranid).FineRuleColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibraryCategoryWiseSetting beData = new BE.Library.Creation.LibraryCategoryWiseSetting();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.CategoryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BookLimit = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.CreditDays = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FixFineAmount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LateFineAmountPerDay = Convert.ToDouble(reader[5]);
                    setup.CategoryWise.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Library.Creation.LibraryFineRule beData = new BE.Library.Creation.LibraryFineRule();
                    int tranid = 0;
                    if (!(reader[0] is DBNull)) tranid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ToDays = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FineAmount = Convert.ToDouble(reader[3]);
                    setup.CategoryWise.Find(p1 => p1.TranId == tranid).FineRuleColl.Add(beData);
                }
                reader.Close();
                setup.IsSuccess = true;
                setup.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                setup.IsSuccess = false;
                setup.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return setup;
        }

        public ResponeValues SaveLibrarySettingCategoryWise(int UserId, List<BE.Library.Creation.LibraryCategoryWiseSetting> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                foreach (var beData in dataColl)
                {
                    if (beData.CategoryId > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
                        cmd.Parameters.AddWithValue("@BookLimit", beData.BookLimit);
                        cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
                        cmd.Parameters.AddWithValue("@FixFineAmount", beData.FixFineAmount);
                        cmd.Parameters.AddWithValue("@LateFineAmountPerDay", beData.LateFineAmountPerDay);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.CommandText = "usp_AddLibraryCategoryWiseSetting";
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                        cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;


                        cmd.ExecuteNonQuery();

                        if (!(cmd.Parameters[6].Value is DBNull))
                            resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                        if (!(cmd.Parameters[7].Value is DBNull))
                            resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                        if (!(cmd.Parameters[8].Value is DBNull))
                            resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                        if (!(cmd.Parameters[9].Value is DBNull))
                            resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

                        if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                            resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                        if (resVal.IsSuccess && resVal.RId > 0 && beData.FineRuleColl != null)
                            SaveFineRulesCategoryWise(beData.CUserId, resVal.RId, beData.FineRuleColl);
                    }
                    else
                    {
                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                    }
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
        private void SaveFineRulesCategoryWise(int UserId, int TranId, List<BE.Library.Creation.LibraryFineRule> fineColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int sno = 1;
            foreach (var beData in fineColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@FromDays", beData.FromDays);
                cmd.Parameters.AddWithValue("@ToDays", beData.ToDays);
                cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddLibraryCategoryWiseFineRule";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }

        public ResponeValues DeleteBookIssueClassWiseById(int UserId, int TranId, int ClassId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.CommandText = "usp_DelLibraryClassWiseSetting";
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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

