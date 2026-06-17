
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class GatePassStudentDB
    {
        DataAccessLayer1 dal = null;
        public GatePassStudentDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.GatePassStudent beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            //cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@Purpose", beData.Purpose);
            cmd.Parameters.AddWithValue("@OutTime", beData.OutTime);
            cmd.Parameters.AddWithValue("@ValidityTime", beData.ValidityTime);
            cmd.Parameters.AddWithValue("@InTime", beData.InTime);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@GatePassStudentId", beData.GatePassStudentId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGatePassStudent";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddGatePassStudent";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                if (resVal.RId > 0 && resVal.IsSuccess)
                {

                    //SaveGatePassStudentDocumentAttach(beData.CUserId, resVal.RId, beData.GatePassStudentDocumentAttachColl);
                }
                //if (beData.GatePassStudentDocumentAttachColl != null && beData.GatePassStudentDocumentAttachColl.Count > 0)
                //    SaveGatePassStudentDocumentAttach(beData.GatePassStudentId, beData.GatePassStudentDocumentAttachColl);

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
        private void SaveGatePassStudentDocumentAttach(int UserId, int GatePassStudentId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || GatePassStudentId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {

                //System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@GatePassStudentId", GatePassStudentId);
                //cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                //cmd.Parameters.AddWithValue("@AttachDocument", beData.AttachDocument);
                //cmd.Parameters.AddWithValue("@AttachDocumentPath", beData.AttachDocumentPath);
                //cmd.Parameters.AddWithValue("@Description", beData.Description);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "usp_AddGatePassStudentDocumentAttach";
                //cmd.ExecuteNonQuery();
            }

        }

      
        public BE.FrontDesk.Transaction.GatePassStudentCollections getAllGatePassStudent(int UserId, int EntityId)
        {
            BE.FrontDesk.Transaction.GatePassStudentCollections dataColl = new BE.FrontDesk.Transaction.GatePassStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllGatePassStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.GatePassStudent beData = new BE.FrontDesk.Transaction.GatePassStudent();
                    beData.GatePassStudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    //if (!(reader[2] is DBNull)) beData.StudentId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Purpose = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OutTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ValidityTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.InTime = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.Remarks = reader.GetString(7);
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
        public BE.FrontDesk.Transaction.GatePassStudent getGatePassStudentById(int UserId, int EntityId, int GatePassStudentId)
        {
            BE.FrontDesk.Transaction.GatePassStudent beData = new BE.FrontDesk.Transaction.GatePassStudent();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GatePassStudentId", GatePassStudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGatePassStudentById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.GatePassStudent();
                    beData.GatePassStudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                   // if (!(reader[2] is DBNull)) beData.StudentId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Purpose = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.OutTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ValidityTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.InTime = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.Remarks = reader.GetString(7);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    //BE.FrontDesk.Transaction.GatePassStudentDocumentAttach Attach = new BE.FrontDesk.Transaction.GatePassStudentDocumentAttach();

                    //if (!(reader[0] is System.DBNull)) Attach.TranId = reader.GetInt32(0);
                    //if (!(reader[1] is System.DBNull)) Attach.GatePassStudentId = reader.GetInt32(1);
                    //if (!(reader[2] is System.DBNull)) Attach.DocumentTypeId = reader.GetInt32(2);
                    //if (!(reader[3] is System.DBNull)) Attach.AttachDocument = reader.GetString(3);
                    //if (!(reader[4] is System.DBNull)) Attach.AttachDocumentPath = reader.GetString(4);
                    //if (!(reader[5] is System.DBNull)) Attach.Description = reader.GetString(5);
                    // beData.GatePassStudentDocumentAttachColl.Add(Attach);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int GatePassStudentId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@GatePassStudentId", GatePassStudentId);
            cmd.CommandText = "usp_DelGatePassStudentById";
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
