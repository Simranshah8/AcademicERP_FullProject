using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.AppCMS.Creation
{
    internal class BannerDB
    {
        DataAccessLayer1 dal = null;
        public BannerDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.AppCMS.Creation.Banner beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", beData.Title);
            cmd.Parameters.AddWithValue("@PublishOn", beData.PublishOn);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            cmd.Parameters.AddWithValue("@ValidUpTo", beData.ValidUpTo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@BannerId", beData.BannerId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateBanner";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddBanner";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@DisplayEachTime", beData.DisplayEachTime);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            //Add code by prashant
            cmd.Parameters.AddWithValue("@Weblink", beData.Weblink);
            cmd.Parameters.AddWithValue("@ForOnlineRegistration", beData.ForOnlineRegistration);
            cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
            try
            {
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


        public AcademicLib.BE.AppCMS.Creation.BannerCollections getAllBanner(int UserId, int EntityId, string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.BannerCollections dataColl = new AcademicLib.BE.AppCMS.Creation.BannerCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllBanner";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.Banner beData = new AcademicLib.BE.AppCMS.Creation.Banner();
                    beData.BannerId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PublishOn = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ValidUpTo = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.PublishOn_BS = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ValidUpTo_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.DisplayEachTime = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.OrderNo = reader.GetInt32(9);                    
                    if (!(reader[10] is DBNull)) beData.Weblink = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ForOnlineRegistration = Convert.ToBoolean(reader[11]);
                    if (!(reader[12] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[12]);
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

        public AcademicLib.BE.AppCMS.Creation.BannerCollections getAllBannerForApp(int UserId,string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.BannerCollections dataColl = new AcademicLib.BE.AppCMS.Creation.BannerCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllBannerFroApp";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.Banner beData = new AcademicLib.BE.AppCMS.Creation.Banner();
                    if (!(reader[0] is DBNull)) beData.Title = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ImagePath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayEachTime = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BannerId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.OrderNo = Convert.ToInt32(reader[5]);                    
                    if (!(reader[6] is DBNull)) beData.Weblink = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ForOnlineRegistration = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[8]);
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
        public AcademicLib.BE.AppCMS.Creation.Banner getBannerById(int UserId, int EntityId, int BannerId)
        {
            AcademicLib.BE.AppCMS.Creation.Banner beData = new AcademicLib.BE.AppCMS.Creation.Banner();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BannerId", BannerId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetBannerById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.AppCMS.Creation.Banner();
                    beData.BannerId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PublishOn = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ValidUpTo = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.DisplayEachTime = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.OrderNo = reader.GetInt32(7);
   
                    if (!(reader[8] is DBNull)) beData.Weblink = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ForOnlineRegistration = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[10]);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int BannerId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BannerId", BannerId);
            cmd.CommandText = "usp_DelBannerById";
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
