using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class PostalReceiveDB
    {
        DataAccessLayer1 dal = null;
        public PostalReceiveDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.PostalReceive beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FormTitle", beData.FormTitle);
            cmd.Parameters.AddWithValue("@ReferenceNumber", beData.ReferenceNumber);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
           // cmd.Parameters.AddWithValue("@ToTitle", beData.ToTitle);
            cmd.Parameters.AddWithValue("@Date", beData.Date);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@PostalReceiveId", beData.PostalReceiveId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdatePostalReceive";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddPostalReceive";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                if (resVal.RId > 0 && resVal.IsSuccess)
                {

                    //SavePostalReceiveDocumentAttach(beData.CUserId, resVal.RId, beData.PostalReceiveDocumentAttachColl);

                }
                //if (beData.PostalReceiveDocumentAttachColl != null && beData.PostalReceiveDocumentAttachColl.Count > 0)
                //    SavePostalReceiveDocumentAttach(beData.PostalReceiveId, beData.PostalReceiveDocumentAttachColl);


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
        private void SavePostalReceiveDocumentAttach(int UserId, int PostalReceiveId, List<Dynamic.BusinessEntity.GeneralDocument> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || PostalReceiveId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@PostalReceiveId", PostalReceiveId);
                //cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                //cmd.Parameters.AddWithValue("@AttachDocument", beData.AttachDocument);
                //cmd.Parameters.AddWithValue("@AttachDocumentPath", beData.AttachDocumentPath);
                //cmd.Parameters.AddWithValue("@Description", beData.Description);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "usp_AddPostalReceiveDocumentAttach";
                //cmd.ExecuteNonQuery();
            }

        }

       
        public BE.FrontDesk.Transaction.PostalReceiveCollections getAllPostalReceive(int UserId, int EntityId)
        {
            BE.FrontDesk.Transaction.PostalReceiveCollections dataColl = new BE.FrontDesk.Transaction.PostalReceiveCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllPostalReceive";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.PostalReceive beData = new BE.FrontDesk.Transaction.PostalReceive();
                    beData.PostalReceiveId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FormTitle = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ReferenceNumber = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Address = reader.GetString(3);
                    //if (!(reader[4] is DBNull)) beData.ToTitle = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Date = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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
        public BE.FrontDesk.Transaction.PostalReceive getPostalReceiveById(int UserId, int EntityId, int PostalReceiveId)
        {
            BE.FrontDesk.Transaction.PostalReceive beData = new BE.FrontDesk.Transaction.PostalReceive();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostalReceiveId", PostalReceiveId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetPostalReceiveById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.PostalReceive();
                    beData.PostalReceiveId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FormTitle = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ReferenceNumber = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Address = reader.GetString(3);
                   // if (!(reader[4] is DBNull)) beData.ToTitle = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Date = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    //BE.FrontDesk.Transaction.PostalReceiveDocumentAttach Academic = new BE.FrontDesk.Transaction.PostalReceiveDocumentAttach();

                    //if (!(reader[0] is System.DBNull)) Academic.PostalReceiveId = reader.GetInt32(0);
                    //if (!(reader[1] is System.DBNull)) Academic.DocumentTypeId = reader.GetInt32(1);
                    //if (!(reader[2] is System.DBNull)) Academic.AttachDocument = reader.GetString(2);
                    //if (!(reader[3] is System.DBNull)) Academic.AttachDocumentPath = reader.GetString(3);
                    //if (!(reader[4] is System.DBNull)) Academic.Description = reader.GetString(4);
                    //beData.PostalReceiveDocumentAttachColl.Add(Academic);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int PostalReceiveId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@PostalReceiveId", PostalReceiveId);
            cmd.CommandText = "usp_DelPostalReceiveById";
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
