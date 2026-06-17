using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class VisitorDB
    {
        DataAccessLayer1 dal = null;
        public VisitorDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.Visitor beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@Contact", beData.Contact);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@MeeTo", beData.MeeTo);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@OthersName", beData.OthersName);
            cmd.Parameters.AddWithValue("@Purpose", beData.Purpose);
            cmd.Parameters.AddWithValue("@InTime", beData.InTime);
            cmd.Parameters.AddWithValue("@ValidityTime", beData.ValidityTime);
            cmd.Parameters.AddWithValue("@OutTime", beData.OutTime);
            cmd.Parameters.AddWithValue("@Photo", beData.Photo);
            cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@VisitorId", beData.VisitorId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateVisitor";
            }
            else
            {
                cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddVisitor";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[20].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;


            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[18].Value);

                if (!(cmd.Parameters[19].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[19].Value);

                if (!(cmd.Parameters[20].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[20].Value);

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[21].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";


                if (resVal.RId > 0 && resVal.IsSuccess)
                {

                    //SaveVisitorAttachDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
                }
                //if (beData.VisitorDocumentAttachColl != null && beData.VisitorDocumentAttachColl.Count > 0)
                //    SaveVisitorAttachDocument(beData.VisitorId, beData.VisitorDocumentAttachColl);

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
        private void SaveDocument(int UserId, int VisitorId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || VisitorId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
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
                    cmd.CommandText = "[usp_AddVisitorDocumentAttach]";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        //private void SaveVisitorAttachDocument(int UserId, int VisitorId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        //{
        //    if (beDataColl == null || beDataColl.Count == 0 || VisitorId == 0)
        //        return;

        //    foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
        //    {

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
        //        cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
        //        cmd.Parameters.AddWithValue("@AttachDoc", beData.Data);
        //        cmd.Parameters.AddWithValue("@AttachDocPath", beData.DocPath);
        //        cmd.Parameters.AddWithValue("@Description", beData.Description);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_UpdateVisitorDocumentAttach";
        //        cmd.ExecuteNonQuery();
        //    }

        //}


        public BE.FrontDesk.Transaction.VisitorCollections getAllVisitor(int UserId, int EntityId,DateTime? dateFrom,DateTime? dateTo)
        {
            BE.FrontDesk.Transaction.VisitorCollections dataColl = new BE.FrontDesk.Transaction.VisitorCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetAllVisitor";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.Visitor beData = new BE.FrontDesk.Transaction.Visitor();
                    beData.VisitorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Contact = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Email = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MeeTo =(AcademicLib.BE.FrontDesk.Transaction.MEETTOS) reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.StudentId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EmployeeId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.OthersName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Purpose = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.InTime = Convert.ToDateTime(reader[10]);
                    if (!(reader[11] is DBNull)) beData.ValidityTime = Convert.ToDateTime(reader[11]);
                    if (!(reader[12] is DBNull)) beData.OutTime = Convert.ToDateTime(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.UserName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.LogDateTime = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.LogMiti = reader.GetString(16);
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
        public BE.FrontDesk.Transaction.Visitor getVisitorById(int UserId, int EntityId, int VisitorId)
        {
            BE.FrontDesk.Transaction.Visitor beData = new BE.FrontDesk.Transaction.Visitor();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "[usp_GetVisitorById]";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.Visitor();
                    beData.VisitorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Contact = reader.GetString(3);
                    if (!(reader[5] is DBNull)) beData.MeeTo = (AcademicLib.BE.FrontDesk.Transaction.MEETTOS)reader.GetInt32(5);
                    if (!(reader[5] is DBNull)) beData.StudentId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.EmployeeId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.OthersName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Email = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Purpose = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.InTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.ValidityTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.OutTime = reader.GetDateTime(12);
                    //if (!(reader[13] is DBNull)) beData.Photo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.PhotoPath = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Remarks = reader.GetString(15);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument Document = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is System.DBNull)) Document.Id = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Document.DocumentTypeId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Document.Name = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Document.Description = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Document.Extension = reader.GetString(4);
                    if (!(reader[6] is System.DBNull)) Document.DocPath = reader.GetString(6);
                    beData.AttachmentColl.Add(Document); //beData.Add(Document);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int VisitorId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.CommandText = "usp_DelVisitorById";
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
        public ResponeValues UpdateInTime(int UserId, int VisitorId, DateTime InTime, DateTime OutTime)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.Parameters.AddWithValue("@OutTime ",OutTime);
            cmd.CommandText = "usp_GetUpdateVisitorInTime";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.UPDATE_SUCCESS;
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
