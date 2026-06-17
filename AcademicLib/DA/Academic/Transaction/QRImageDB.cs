using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
using System.Data.SqlClient;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class QRImageDB
    {
        DataAccessLayer1 dal = null;
       
        public QRImageDB (string dbName, string hostName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }


        public ResponeValues SaveUpdate(BE.Academic.Transaction.QRImage beData,bool isModify,int EntityId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OnlineQrPath", beData.OnlineQrPath);
            cmd.Parameters.AddWithValue("@PlaystoreQrPath", beData.PlaystoreQrPath);
            cmd.Parameters.AddWithValue("@AppStoreQrPath", beData.AppStoreQrPath);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@QRImageId", beData.QrImageId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateQRImage"; 
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_SaveQRImage";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("jsidfjskdfjkdsfklsdjf");
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

            }
            catch (SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch(Exception e)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = e.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues DeleteById(int UserId, int EntityId, int QrImageId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@QrImageId", QrImageId);

            cmd.CommandText = "usp_DelQrImageById";

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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (SqlException ee)
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

        public BE.Academic.Transaction.QRCollections GetAllQrImage(int UserId, int EntityId)
        {
            BE.Academic.Transaction.QRCollections dataColl = new BE.Academic.Transaction.QRCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);

            cmd.CommandText = "usp_GetAllQrImages";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.QRImage beData = new BE.Academic.Transaction.QRImage();
                    if (!(reader[0] is DBNull)) beData.QrImageId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OnlineQrPath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PlaystoreQrPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AppStoreQrPath = reader.GetString(3);
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
        public BE.Academic.Transaction.QRImage GetQrImageById(int UserId, int EntityId, int QrImageId)
        {
            BE.Academic.Transaction.QRImage beData = new BE.Academic.Transaction.QRImage();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QrImageId", QrImageId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);

            cmd.CommandText = "usp_GetQrImageById";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.QRImage();
                    if (!(reader[0] is DBNull)) beData.QrImageId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OnlineQrPath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PlaystoreQrPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AppStoreQrPath = reader.GetString(3);
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
    }
}
