using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{
    internal class HouseNameDB
    {
        DataAccessLayer1 dal = null;
        public HouseNameDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Creation.HouseName beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@HouseNameId", beData.HouseNameId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHouseName";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddHouseName";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CoOrdinatorId", beData.CoOrdinatorId);

            //CaptainId_Boy,ViceCaptainId_Boy,CaptainId_Girl,ViceCaptainId_Girl
            cmd.Parameters.AddWithValue("@CaptainId_Boy", beData.CaptainId_Boy);
            cmd.Parameters.AddWithValue("@ViceCaptainId_Boy", beData.ViceCaptainId_Boy);
            cmd.Parameters.AddWithValue("@CaptainId_Girl", beData.CaptainId_Girl);
            cmd.Parameters.AddWithValue("@ViceCaptainId_Girl", beData.ViceCaptainId_Girl);

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

                 if(resVal.IsSuccess && resVal.RId > 0)
                {
                    if(beData.HouseInchargeIdColl!=null && beData.HouseInchargeIdColl.Count > 0)
                    {
                        foreach (var det in beData.HouseInchargeIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                            cmd.Parameters.AddWithValue("@EmployeeId", det);
                            cmd.Parameters.AddWithValue("@HouseNameId", resVal.RId);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "usp_AddHouseIncharge";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (beData.HouseMemberIdColl != null && beData.HouseMemberIdColl.Count > 0)
                    {
                        foreach (var det in beData.HouseMemberIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                            cmd.Parameters.AddWithValue("@EmployeeId", det);
                            cmd.Parameters.AddWithValue("@HouseNameId", resVal.RId);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "usp_AddHouseMember";
                            cmd.ExecuteNonQuery();

                        }
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
        public BE.Academic.Creation.HouseNameCollections getAllHouseName(int UserId, int EntityId)
        {
            BE.Academic.Creation.HouseNameCollections dataColl = new BE.Academic.Creation.HouseNameCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllHouseName";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.HouseName beData = new BE.Academic.Creation.HouseName();
                    beData.HouseNameId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CoOrdinatorId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.CoOrdinatorName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.CoOrdinatorCode = reader.GetString(6);
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
        public BE.Academic.Creation.HouseName getHouseNameById(int UserId, int EntityId, int HouseNameId)
        {
            BE.Academic.Creation.HouseName beData = new BE.Academic.Creation.HouseName();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HouseNameId", HouseNameId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetHouseNameById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.HouseName();
                    beData.HouseNameId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CoOrdinatorId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.CaptainId_Boy = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ViceCaptainId_Boy = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.CaptainId_Girl = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ViceCaptainId_Girl = reader.GetInt32(8);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    beData.HouseInchargeIdColl.Add(reader.GetInt32(0));
                }
                reader.NextResult();
                while (reader.Read())
                {
                    beData.HouseMemberIdColl.Add(reader.GetInt32(0));
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
        public ResponeValues DeleteById(int UserId, int EntityId, int HouseNameId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@HouseNameId", HouseNameId);
            cmd.CommandText = "usp_DelHouseNameById";
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
