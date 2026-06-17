using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Hostel
{
    internal class HostelDB
    {
        DataAccessLayer1 dal = null;
        public HostelDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Hostel.Hostel beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Code", beData.Code);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@WardenId", beData.WardenId);
            cmd.Parameters.AddWithValue("@IsBoy", beData.IsBoy);
            cmd.Parameters.AddWithValue("@IsGirls", beData.IsGirls);
            cmd.Parameters.AddWithValue("@IsGuest", beData.IsGuest);
            cmd.Parameters.AddWithValue("@IsStaff", beData.IsStaff);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@HostelId", beData.HostelId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHostel";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddHostel";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if(resVal.RId>0 && resVal.IsSuccess)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    foreach (var m in beData.FacilitiesColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@HostelId", resVal.RId);
                        cmd.Parameters.AddWithValue("@Name", m);                        
                        cmd.CommandText = "insert into tbl_HostelFacilities(HostelId,Name) values(@HostelId,@Name)";
                        cmd.ExecuteNonQuery();
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
        public AcademicLib.BE.Hostel.HostelCollections getAllHostel(int UserId, int EntityId)
        {
            AcademicLib.BE.Hostel.HostelCollections dataColl = new AcademicLib.BE.Hostel.HostelCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllHostel";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.Hostel beData = new AcademicLib.BE.Hostel.Hostel();
                    beData.HostelId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Address = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.WardenId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.IsBoy = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsGirls = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsGuest = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.IsStaff = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.WardenName = reader.GetString(9);
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
        public AcademicLib.BE.Hostel.Hostel getHostelById(int UserId, int EntityId, int HostelId)
        {
            AcademicLib.BE.Hostel.Hostel beData = new AcademicLib.BE.Hostel.Hostel();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HostelId", HostelId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetHostelById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Hostel.Hostel();
                    beData.HostelId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Address = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.WardenId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.IsBoy = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsGirls = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsGuest = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.IsStaff = reader.GetBoolean(8);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    beData.FacilitiesColl.Add(reader.GetString(0));
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
        public ResponeValues DeleteById(int UserId, int EntityId, int HostelId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@HostelId", HostelId);
            cmd.CommandText = "usp_DelHostelById";
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
