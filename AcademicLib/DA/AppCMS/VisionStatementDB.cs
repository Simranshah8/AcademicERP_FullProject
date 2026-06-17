using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.AppCMS.Creation
{
    internal class VisionStatementDB
    {
        DataAccessLayer1 dal = null;
        public VisionStatementDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.AppCMS.Creation.VisionStatement beData, bool isModify)
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
            cmd.Parameters.AddWithValue("@VisionStatementId", beData.VisionStatementId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateVisionStatement";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddVisionStatement";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@SubmenuTitle", beData.SubmenuTitle);

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


        public AcademicLib.BE.AppCMS.Creation.VisionStatementCollections getAllVisionStatement(int UserId, int EntityId, string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.VisionStatementCollections dataColl = new AcademicLib.BE.AppCMS.Creation.VisionStatementCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllVisionStatement";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.VisionStatement beData = new AcademicLib.BE.AppCMS.Creation.VisionStatement();
                    beData.VisionStatementId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OrderNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubmenuTitle = reader.GetString(5);

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
        public AcademicLib.BE.AppCMS.Creation.VisionStatement getVisionStatementById(int UserId, int EntityId, int VisionStatementId)
        {
            AcademicLib.BE.AppCMS.Creation.VisionStatement beData = new AcademicLib.BE.AppCMS.Creation.VisionStatement();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VisionStatementId", VisionStatementId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetVisionStatementById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.AppCMS.Creation.VisionStatement();
                    beData.VisionStatementId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OrderNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubmenuTitle = reader.GetString(5);

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
        public ResponeValues DeleteById(int UserId, int EntityId, int VisionStatementId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@VisionStatementId", VisionStatementId);
            cmd.CommandText = "usp_DelVisionStatementById";
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

        public AcademicLib.API.AppCMS.Introduction getIntroduction(int UserId,string BranchCode)
        {
            AcademicLib.API.AppCMS.Introduction introduction = new API.AppCMS.Introduction();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetIntroduction";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.VisionStatement beData = new BE.AppCMS.Creation.VisionStatement();                    
                    if (!(reader[0] is DBNull)) beData.Title = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Description = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ImagePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    beData.GuId = Guid.NewGuid().ToString();
                    introduction.VisionList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.FounderMessage beData = new BE.AppCMS.Creation.FounderMessage();
                    if (!(reader[0] is DBNull)) beData.Title = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Description = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ImagePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Qualification = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubmenuTitle = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FounderMessageId = reader.GetInt32(8);
                    beData.GuId = Guid.NewGuid().ToString();
                    introduction.FounderMSGList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.StaffHierarchy beData = new BE.AppCMS.Creation.StaffHierarchy();
                    if (!(reader[0] is DBNull)) beData.FullName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Designation = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ContactNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Message = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ImagePath = reader.GetString(5);
                    if (!(reader[7] is DBNull)) beData.Department = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Qualification = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.StaffHierarchyId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.OrderNo = reader.GetInt32(10);
                    beData.GuId = Guid.NewGuid().ToString();
                    introduction.StaffList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int fid = 0;
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia detF = new BE.AppCMS.Creation.FounderSocialMedia();
                    if (!(reader[0] is DBNull)) fid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) detF.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) detF.IconPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) detF.ThumbnailPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) detF.UrlPath = reader.GetString(4);
                    try
                    {
                        introduction.FounderMSGList.Find(p1 => p1.FounderMessageId == fid).FounderSocialMediaColl.Add(detF);
                    }
                    catch { }
                    detF.GuId = Guid.NewGuid().ToString();
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int fid = 0;
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia detF = new BE.AppCMS.Creation.FounderSocialMedia();
                    if (!(reader[0] is DBNull)) fid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) detF.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) detF.IconPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) detF.ThumbnailPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) detF.UrlPath = reader.GetString(4);
                    try
                    {
                        introduction.StaffList.Find(p1 => p1.StaffHierarchyId == fid).SocialMediaColl.Add(detF);
                    }
                    catch { }
                    detF.GuId = Guid.NewGuid().ToString();
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.StaffHierarchy beData = new BE.AppCMS.Creation.StaffHierarchy();
                    if (!(reader[0] is DBNull)) beData.FullName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Designation = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ContactNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Message = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ImagePath = reader.GetString(5);
                    if (!(reader[7] is DBNull)) beData.Department = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Qualification = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.StaffHierarchyId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.OrderNo = reader.GetInt32(10);
                    beData.GuId = Guid.NewGuid().ToString();
                    introduction.CommitteList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int fid = 0;
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia detF = new BE.AppCMS.Creation.FounderSocialMedia();
                    if (!(reader[0] is DBNull)) fid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) detF.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) detF.IconPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) detF.ThumbnailPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) detF.UrlPath = reader.GetString(4);
                    try
                    {
                        introduction.CommitteList.Find(p1 => p1.StaffHierarchyId == fid).SocialMediaColl.Add(detF);
                    }
                    catch { }
                    detF.GuId = Guid.NewGuid().ToString();
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.FounderMessage beData = new BE.AppCMS.Creation.FounderMessage();
                    if (!(reader[0] is DBNull)) beData.Title = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Description = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ImagePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Qualification = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubmenuTitle = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FounderMessageId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.OrderNo = reader.GetInt32(9);
                    beData.GuId = Guid.NewGuid().ToString();
                    introduction.TestimonialList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int fid = 0;
                    AcademicLib.BE.AppCMS.Creation.FounderSocialMedia detF = new BE.AppCMS.Creation.FounderSocialMedia();
                    if (!(reader[0] is DBNull)) fid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) detF.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) detF.IconPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) detF.ThumbnailPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) detF.UrlPath = reader.GetString(4);
                    try
                    {
                        introduction.TestimonialList.Find(p1 => p1.FounderMessageId == fid).FounderSocialMediaColl.Add(detF);
                    }
                    catch { }
                    detF.GuId = Guid.NewGuid().ToString();
                }
                reader.Close();
                introduction.IsSuccess = true;
                introduction.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                introduction.IsSuccess = false;
                introduction.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return introduction;
        }

    }
}
