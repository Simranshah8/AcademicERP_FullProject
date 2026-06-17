using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class PostalDispatchDB
    {
        DataAccessLayer1 dal = null;
        public PostalDispatchDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.FrontDesk.Transaction.PostalDispatch beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FormTitle", beData.FromTitle);
            cmd.Parameters.AddWithValue("@Totitle", beData.Totitle);
            cmd.Parameters.AddWithValue("@ReferenceNumber", beData.ReferenceNumber);

            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@Date", beData.Date);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PostalDispatchId", beData.PostalDispatchId);


            if (isModify)
            {
                cmd.CommandText = "usp_UpdatePostalDispatch";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddPostalDispatch";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
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


        private void SaveDocument(int UserId, int PostalDispatchId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || PostalDispatchId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@PostalDispatchId", PostalDispatchId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddPostalDispatchAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public AcademicLib.BE.FrontDesk.Transaction.PostalDispatchCollections GetAllDispatchList(int UserId)
        {
            AcademicLib.BE.FrontDesk.Transaction.PostalDispatchCollections dataColl = new AcademicLib.BE.FrontDesk.Transaction.PostalDispatchCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);


            cmd.CommandText = "usp_GetAllPostalDispatch";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.FrontDesk.Transaction.PostalDispatch beData = new AcademicLib.BE.FrontDesk.Transaction.PostalDispatch();
                    beData.PostalDispatchId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromTitle = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Totitle = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ReferenceNumber = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);

                    if (!(reader[6] is DBNull)) beData.Date = reader.GetDateTime(6);

                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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


        public AcademicLib.BE.FrontDesk.Transaction.PostalDispatch getPostalDispatchById(int UserId, int PostalDispatchId)
        {
            AcademicLib.BE.FrontDesk.Transaction.PostalDispatch beData = new AcademicLib.BE.FrontDesk.Transaction.PostalDispatch();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PostalDispatchId", PostalDispatchId);
            cmd.CommandText = "usp_GetPostalDispatchById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.FrontDesk.Transaction.PostalDispatch();
                    beData.PostalDispatchId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromTitle = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Totitle = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ReferenceNumber = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);

                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Date = reader.GetDateTime(6);
                }
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Description = reader.GetString(4);

                    beData.AttachmentColl.Add(doc);
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
        public ResponeValues DeleteDispatchById(int UserId, int PostalDispatchId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.Parameters.AddWithValue("@PostalDispatchId", PostalDispatchId);
            cmd.CommandText = "usp_DelDispatchById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);


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
