using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class PostalServiceDB
    {
        DataAccessLayer1 dal = null;
        public PostalServiceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.FrontDesk.Transaction.PostalService beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FormTitle", beData.FromTitle);
            cmd.Parameters.AddWithValue("@ReferenceNumber", beData.ReferenceNumber);
            cmd.Parameters.AddWithValue("@Totitle", beData.Totitle);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@Date", beData.Date);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PostalServicesId", beData.PostalServicesId);


            if (isModify)
            {
                cmd.CommandText = "usp_UpdatePostalService";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddPostalServices";
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
        private void SaveDocument(int UserId, int PostalServicesId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || PostalServicesId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@PostalServicesId", PostalServicesId);
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
                    cmd.CommandText = "usp_AddPostalServicesAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public AcademicLib.BE.FrontDesk.Transaction.PostalServiceCollections GetAllReceivedList(int UserId,DateTime Fromdate, DateTime Todate)
        {
            AcademicLib.BE.FrontDesk.Transaction.PostalServiceCollections dataColl = new AcademicLib.BE.FrontDesk.Transaction.PostalServiceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", Fromdate);
            cmd.Parameters.AddWithValue("@DateTo", Todate);
            cmd.CommandText = "usp_GetAllPostalServices";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.FrontDesk.Transaction.PostalService beData = new AcademicLib.BE.FrontDesk.Transaction.PostalService();
                    beData.PostalServicesId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromTitle = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Totitle = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ReferenceNumber = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Date = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.Miti = reader.GetString(7);
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
        public AcademicLib.BE.FrontDesk.Transaction.PostalService getPostalServiceById(int UserId, int PostalServicesId)
        {
            AcademicLib.BE.FrontDesk.Transaction.PostalService beData = new AcademicLib.BE.FrontDesk.Transaction.PostalService();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PostalServicesId", PostalServicesId);
            cmd.CommandText = "usp_GetPostalServicesById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.FrontDesk.Transaction.PostalService();
                    beData.PostalServicesId = reader.GetInt32(0);
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
        public ResponeValues DeleteReceivedById(int UserId, int PostalServicesId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.Parameters.AddWithValue("@PostalServicesId", PostalServicesId);
            cmd.CommandText = "usp_DelPostalServiceById";
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

                /*  if (!(cmd.Parameters[4].Value is DBNull))
                      resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                  if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                      resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";*/

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