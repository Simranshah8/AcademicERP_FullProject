using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeMappingForWiseDB
    {
        DataAccessLayer1 dal = null;
        public FeeMappingForWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId, BE.Fee.Creation.FeeMappingMediumWiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            int mediumId = dataColl.First().MediumId;
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MediumId", mediumId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelFeeMappingForWise";
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.ExecuteNonQuery();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var beData in dataColl)
                {
                    if (beData.Rate != 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MediumId", beData.MediumId);
                        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                        cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.CommandText = "usp_SaveForWiseFeeMapping";
                        cmd.ExecuteNonQuery();
                    }
                 
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Feemapping ForWise Done.";
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

        public BE.Fee.Creation.FeeMappingMediumWiseCollections getAllFeeMapping(int UserId, int AcademicYearId, int EntityId, int MediumId)
        {
            BE.Fee.Creation.FeeMappingMediumWiseCollections dataColl = new BE.Fee.Creation.FeeMappingMediumWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MediumId", MediumId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeemappingForWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeMappingMediumWise beData = new BE.Fee.Creation.FeeMappingMediumWise();
                    beData.ClassId = reader.GetInt32(0);
                    beData.ClassName = reader.GetString(1);
                    beData.FeeItemId = reader.GetInt32(2);
                    beData.FeeItemName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Rate = Convert.ToDouble(reader[4]);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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

        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId, int MediumId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@MediumId", MediumId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelFeeMappingForWise";
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


        public BE.Fee.Creation.ManualBillingDetailsCollections getFeeItemFor(int UserId, int? AcademicYearId,int? ClassId, int ForId)
        {
            BE.Fee.Creation.ManualBillingDetailsCollections dataColl = new BE.Fee.Creation.ManualBillingDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);            
            cmd.Parameters.AddWithValue("@ForId", ForId);
            cmd.CommandText = "usp_GetFeeItemFor";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.ManualBillingDetails beData = new BE.Fee.Creation.ManualBillingDetails();
                    beData.FeeItemId = reader.GetInt32(0);
                    beData.FeeItemName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Rate = Convert.ToDouble(reader[2]);
                    beData.Qty = 1;
                    beData.PayableAmt = beData.Rate;
                    beData.PaidAmt = beData.Rate;
                    
                    dataColl.Add(beData);
                }
                reader.Close(); 

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }
    }
}
