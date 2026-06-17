using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Library.Creation
{
    internal class AuthorDB
    {
        DataAccessLayer1 dal = null;
        public AuthorDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
       
        public ResponeValues SaveUpdate(BE.Library.Creation.Author beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Image", beData.Image);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@PhoneNo", beData.PhoneNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@AuthorId", beData.AuthorId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateAuthor";
            }
            else
            {
                cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddAuthor";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@SpinerAuthorMark", beData.SpinerAuthorMark);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[14].Value);

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
        public BE.Library.Creation.AuthorCollections getAllAuthor(int UserId, int EntityId)
        {
            BE.Library.Creation.AuthorCollections dataColl = new BE.Library.Creation.AuthorCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllAuthor";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Creation.Author beData = new BE.Library.Creation.Author();
                    beData.AuthorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1); 
                    if (!(reader[2] is DBNull)) beData.ImagePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Nationality = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MobileNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EmailId = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PhoneNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.SpinerAuthorMark = reader.GetString(10);
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
        public BE.Library.Creation.Author getAuthorById(int UserId, int EntityId, int AuthorId)
        {
            BE.Library.Creation.Author beData = new BE.Library.Creation.Author();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", AuthorId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAuthorById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Library.Creation.Author();
                    beData.AuthorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ImagePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Nationality = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MobileNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EmailId = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Gender = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PhoneNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.SpinerAuthorMark = reader.GetString(10);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int AuthorId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AuthorId", AuthorId);
            cmd.CommandText = "usp_DelAuthorById";
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
