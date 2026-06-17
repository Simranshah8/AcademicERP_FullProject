using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.AppCMS.Creation
{
    internal class FounderMessageDB
    {
        DataAccessLayer1 dal = null;
        public FounderMessageDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.AppCMS.Creation.FounderMessage beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@Title", beData.Title);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@FounderMessageId", beData.FounderMessageId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateFounderMessage";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddFounderMessage";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Designation", beData.Designation);
            cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
            cmd.Parameters.AddWithValue("@SubmenuTitle", beData.SubmenuTitle);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
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

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveSocialMediaDetails(beData.CUserId, resVal.RId, beData.FounderSocialMediaColl);
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
        private void SaveSocialMediaDetails(int UserId, int FounderMessageId, AcademicLib.BE.AppCMS.Creation.FounderSocialMediaCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || FounderMessageId == 0)
                return;

            foreach (AcademicLib.BE.AppCMS.Creation.FounderSocialMedia beData in beDataColl)
            {
                if (beData.SocialMediaId > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@FounderMessageId", FounderMessageId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@SocialMediaId", beData.SocialMediaId);
                    cmd.Parameters.AddWithValue("@UrlPath", beData.UrlPath);
                    cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddFounderSocialMedia";
                    cmd.ExecuteNonQuery();
                }
              
            }

        }

        public AcademicLib.BE.AppCMS.Creation.FounderMessageCollections getAllFounderMessage(int UserId, int EntityId, string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.FounderMessageCollections dataColl = new AcademicLib.BE.AppCMS.Creation.FounderMessageCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllFounderMessage";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.FounderMessage beData = new AcademicLib.BE.AppCMS.Creation.FounderMessage();
                    beData.FounderMessageId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OrderNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Qualification = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubmenuTitle = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Name = reader.GetString(8);
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
        public AcademicLib.BE.AppCMS.Creation.FounderMessage getFounderMessageById(int UserId, int EntityId, int FounderMessageId)
        {
            AcademicLib.BE.AppCMS.Creation.FounderMessage beData = new AcademicLib.BE.AppCMS.Creation.FounderMessage();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FounderMessageId", FounderMessageId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetFounderMessageById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.AppCMS.Creation.FounderMessage();
                    beData.FounderMessageId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OrderNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Qualification = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubmenuTitle = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Name = reader.GetString(8);
                }
                reader.NextResult();
                beData.FounderSocialMediaColl = new BE.AppCMS.Creation.FounderSocialMediaCollections();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia det = new BE.AppCMS.Creation.FounderSocialMedia();
                    //if (!(reader[0] is DBNull)) det.FounderMessageId = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.SocialMediaId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.UrlPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.IsActive = reader.GetBoolean(3);
                    beData.FounderSocialMediaColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int FounderMessageId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@FounderMessageId", FounderMessageId);
            cmd.CommandText = "usp_DelFounderMessageById";
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
