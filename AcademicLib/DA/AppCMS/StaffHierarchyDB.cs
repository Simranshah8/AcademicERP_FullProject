using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.AppCMS.Creation
{
    internal class StaffHierarchyDB
    {
        DataAccessLayer1 dal = null;
        public StaffHierarchyDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.AppCMS.Creation.StaffHierarchy beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FullName", beData.FullName);
            cmd.Parameters.AddWithValue("@Designation", beData.Designation);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Email", beData.Email);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@StaffHierarchyId", beData.StaffHierarchyId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStaffHierarchy";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStaffHierarchy";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Message", beData.Message);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            cmd.Parameters.AddWithValue("@Department", beData.Department);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
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
                    SaveSocialMediaDetails(beData.CUserId, resVal.RId, beData.SocialMediaColl);
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

        private void SaveSocialMediaDetails(int UserId, int StaffHierarchyId, AcademicLib.BE.AppCMS.Creation.FounderSocialMediaCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || StaffHierarchyId == 0)
                return;

            foreach (AcademicLib.BE.AppCMS.Creation.FounderSocialMedia beData in beDataColl)
            {
                if (beData.SocialMediaId > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@StaffHierarchyId", StaffHierarchyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@SocialMediaId", beData.SocialMediaId);
                    cmd.Parameters.AddWithValue("@UrlPath", beData.UrlPath);
                    cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddStaffHierarchySocialMedia";
                    cmd.ExecuteNonQuery();
                }

            }

        }
        public AcademicLib.BE.AppCMS.Creation.StaffHierarchyCollections getAllStaffHierarchy(int UserId, int EntityId, string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.StaffHierarchyCollections dataColl = new AcademicLib.BE.AppCMS.Creation.StaffHierarchyCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllStaffHierarchy";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.StaffHierarchy beData = new AcademicLib.BE.AppCMS.Creation.StaffHierarchy();
                    beData.StaffHierarchyId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FullName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ContactNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Email = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Message = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ImagePath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.OrderNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Department = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Qualification = reader.GetString(9);

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
        public AcademicLib.BE.AppCMS.Creation.StaffHierarchy getStaffHierarchyById(int UserId, int EntityId, int StaffHierarchyId)
        {
            AcademicLib.BE.AppCMS.Creation.StaffHierarchy beData = new AcademicLib.BE.AppCMS.Creation.StaffHierarchy();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StaffHierarchyId", StaffHierarchyId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStaffHierarchyById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.AppCMS.Creation.StaffHierarchy();
                    beData.StaffHierarchyId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FullName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ContactNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Email = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Message = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ImagePath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.OrderNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Department = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Qualification = reader.GetString(9);
                }
                reader.NextResult();
                beData.SocialMediaColl = new BE.AppCMS.Creation.FounderSocialMediaCollections();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia det = new BE.AppCMS.Creation.FounderSocialMedia();
                    //if (!(reader[0] is DBNull)) det.FounderMessageId = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.SocialMediaId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.UrlPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.IsActive = reader.GetBoolean(3);
                    beData.SocialMediaColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int StaffHierarchyId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@StaffHierarchyId", StaffHierarchyId);
            cmd.CommandText = "usp_DelStaffHierarchyById";
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
