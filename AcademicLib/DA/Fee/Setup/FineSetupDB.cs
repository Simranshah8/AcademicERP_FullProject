using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Setup
{
    internal class FineSetupDB
    {
        DataAccessLayer1 dal = null;
        public FineSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Fee.Setup.Fine fine,int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", fine.CUserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelFineSetup";
            cmd.ExecuteNonQuery();
              
           
            try
            {
                foreach (var beData in fine.FineSetupDetColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@SelectConditionAsPer", beData.SelectConditionAsPer);
                    cmd.Parameters.AddWithValue("@ConditionAmount", beData.ConditionAmount);
                    cmd.Parameters.AddWithValue("@FineOnBasisOfAmnt", beData.FineOnBasisOfAmnt);
                    cmd.Parameters.AddWithValue("@FineOnBasisOfMonth", beData.FineOnBasisOfMonth);
                    cmd.Parameters.AddWithValue("@FineAmount", beData.FineAmount);
                    cmd.Parameters.AddWithValue("@DebateOnBasisOfAmnt", beData.DebateOnBasisOfAmnt);
                    cmd.Parameters.AddWithValue("@DebateOnBasisOfMonth", beData.DebateOnBasisOfMonth);
                    cmd.Parameters.AddWithValue("@ReBateAmount", beData.ReBateAmount);
                    cmd.Parameters.AddWithValue("@UserId", fine.CUserId);
                    cmd.Parameters.AddWithValue("@EntityId", fine.EntityId);                     
                    cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
                    
                    cmd.CommandText = "usp_AddFineSetup";
                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[12].Value is DBNull))
                        resVal.RId = Convert.ToInt32(cmd.Parameters[12].Value);

                    if (!(cmd.Parameters[13].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[13].Value);

                    if (!(cmd.Parameters[14].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[14].Value);

                    if (!(cmd.Parameters[15].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[15].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                    if (resVal.IsSuccess && resVal.RId > 0)
                    {
                        SaveFineSetupDet(beData.FineSetupColl, resVal.RId, fine.CUserId);
                    }
                 }

                if (resVal.IsSuccess && resVal.RId > 0)
                { 

                    foreach(var newDet in fine.DueDataDetColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ClassId", newDet.ClassId);
                        cmd.Parameters.AddWithValue("@FineOnBasisOfAmount", newDet.FineOnBasisOfAmount);
                        cmd.Parameters.AddWithValue("@FineAmount", newDet.FineAmount);
                        cmd.Parameters.AddWithValue("@DebateOnBasisOfAmount", newDet.DebateOnBasisOfAmount);
                        cmd.Parameters.AddWithValue("@ReBateAmount", newDet.ReBateAmount);
                        cmd.Parameters.AddWithValue("@UserId", fine.CUserId);
                        cmd.Parameters.AddWithValue("@EntityId", fine.EntityId);                   
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                        cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                        cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_AddFollowUpDue";
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
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


                        SaveFineFollowUpDueDetails(newDet.CurFineSetupColl, resVal.RId, fine.CUserId);

                    }
                    
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
        private void SaveFineSetupDet(List<AcademicLib.BE.Fee.Setup.FineSetupDetails> dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DaysFrom", beData.DaysFrom);
                cmd.Parameters.AddWithValue("@DaysTo", beData.DaysTo);
                cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.CommandText = "usp_AddFineSetupDetails";

                cmd.ExecuteNonQuery();
            }
        }
        private void SaveFineFollowUpDueDetails(List<AcademicLib.BE.Fee.Setup.FollowUpDueDetails> dataColl, int TranId, int UserId)
        {
            foreach (var newDet in dataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DaysFrom", newDet.DaysFrom);
                cmd.Parameters.AddWithValue("@DaysTo", newDet.DaysTo);
                cmd.Parameters.AddWithValue("@Amount", newDet.Amount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.CommandText = "usp_AddFollowUpDueDetails";

                cmd.ExecuteNonQuery();
            }
        }
        public AcademicLib.BE.Fee.Setup.FineSetupCollections getAllFineSetup(int UserId, int EntityId)
        {
            AcademicLib.BE.Fee.Setup.FineSetupCollections dataColl = new AcademicLib.BE.Fee.Setup.FineSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllFineSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Setup.FineSetup beData = new AcademicLib.BE.Fee.Setup.FineSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SelectConditionAsPer = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ConditionAmount = reader.GetDouble(3);
                    if (!(reader[4] is DBNull)) beData.FineOnBasisOfAmnt = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FineOnBasisOfMonth = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.FineAmount = reader.GetDouble(6);
                    if (!(reader[7] is DBNull)) beData.DebateOnBasisOfAmnt = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.DebateOnBasisOfMonth = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.ReBateAmount = reader.GetDouble(9);
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
        public AcademicLib.BE.Fee.Setup.Fine getFineSetup(int UserId,int AcademicYearId)
        {
            AcademicLib.BE.Fee.Setup.Fine beData = new AcademicLib.BE.Fee.Setup.Fine();
            beData.DueDataDetColl = new List<BE.Fee.Setup.FollowUpDue>();
            beData.FineSetupDetColl = new List<BE.Fee.Setup.FineSetup>();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.CommandText = "usp_GetFineSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var det = new AcademicLib.BE.Fee.Setup.FineSetup();                    
                    if (!(reader[0] is DBNull)) det.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.SelectConditionAsPer = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.ConditionAmount = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.FineOnBasisOfAmnt = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det.FineOnBasisOfMonth = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) det.FineAmount = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det.DebateOnBasisOfAmnt = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) det.DebateOnBasisOfMonth = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) det.ReBateAmount = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) det.TranId = reader.GetInt32(10);
                    beData.FineSetupDetColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var det = new AcademicLib.BE.Fee.Setup.FineSetupDetails();
                    if (!(reader[0] is DBNull)) det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.DaysFrom = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.DaysTo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.Amount = Convert.ToDouble(reader[3]);
                    beData.FineSetupDetColl.Find(p1 => p1.TranId == det.TranId).FineSetupColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var det = new AcademicLib.BE.Fee.Setup.FollowUpDue();
                    if (!(reader[0] is DBNull)) det.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.FineOnBasisOfAmount = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.FineAmount = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.DebateOnBasisOfAmount = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det.ReBateAmount = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det.TranId = reader.GetInt32(6);
                    beData.DueDataDetColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var det = new AcademicLib.BE.Fee.Setup.FollowUpDueDetails();
                    if (!(reader[0] is DBNull)) det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.DaysFrom = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.DaysTo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.Amount = Convert.ToDouble(reader[3]);
                    try
                    {
                        beData.DueDataDetColl.Find(p1 => p1.TranId == det.TranId).CurFineSetupColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.CommandText = "usp_DelFineSetup";
           
            try
            {
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.DELETE_SUCCESS;

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