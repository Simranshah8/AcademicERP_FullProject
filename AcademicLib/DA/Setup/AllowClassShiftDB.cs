using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal class AllowClassShiftDB
    {
        DataAccessLayer1 dal = null;
        public AllowClassShiftDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,AcademicLib.BE.Setup.AllowEntityAccessCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
            try
            { 
                var uid = dataColl.First();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ForUserId", uid.ForUserId);
                cmd.Parameters.AddWithValue("@ForGroupId", uid.ForGroupId);
                cmd.CommandText = "ups_DelAllowClassShift";
                cmd.ExecuteNonQuery();
                foreach (var beData in dataColl)
                {
                    if (beData.IsAllow)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@ClassShiftId", beData.id);
                        cmd.Parameters.AddWithValue("@ForUserId", beData.ForUserId);
                        cmd.Parameters.AddWithValue("@ForGroupId", beData.ForGroupId);
                        cmd.CommandText = "ups_AddAllowClassShift";
                        cmd.ExecuteNonQuery();
                    }                    
                }
                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Allow User Wise ClassShift Update Success";

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
        public BE.Setup.AllowEntityAccessCollections getAllowClassShiftList(int UserId, int? ForUserId, int? ForGroupId)
        {
            BE.Setup.AllowEntityAccessCollections dataColl = new BE.Setup.AllowEntityAccessCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
            cmd.Parameters.AddWithValue("@ForGroupId", ForGroupId);
            cmd.CommandText = "usp_GetAllowClassShiftList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.AllowEntityAccess beData = new BE.Setup.AllowEntityAccess();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
                    beData.IsAllow = reader.GetBoolean(2);
                    beData.ForUserId = ForUserId;
                    beData.ForGroupId = ForGroupId;
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
