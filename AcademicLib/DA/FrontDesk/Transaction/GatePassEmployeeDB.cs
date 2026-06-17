using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class GatePassEmployeeDB
    {
        DataAccessLayer1 dal = null;
        public GatePassEmployeeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.GatePassEmployee beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            //cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@DesignationId", beData.DesignationId);
            cmd.Parameters.AddWithValue("@PurposeId", beData.PurposeId);
            cmd.Parameters.AddWithValue("@OutTime", beData.OutTime);
            cmd.Parameters.AddWithValue("@ValidationTime", beData.ValidationTime);
            cmd.Parameters.AddWithValue("@InTime", beData.InTime);
            cmd.Parameters.AddWithValue("@InTime", beData.Remarks);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@GatePassEmployeeId", beData.GatePassEmployeeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGatePassEmployee";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddGatePassEmployee";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    
                    //SaveGatePassEmployeeDocumentAttach(beData.CUserId, resVal.RId, beData.GatePassEmployeeDocumentAttachColl);
                }
                //if (beData.GatePassEmployeeDocumentAttachColl != null && beData.GatePassEmployeeDocumentAttachColl.Count > 0)
                //    SaveGatePassEmployeeDocumentAttach(beData.GatePassEmployeeId, beData.GatePassEmployeeDocumentAttachColl);

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
        private void SaveGatePassEmployeeDocumentAttach(int UserId, int GatePassEmployeeId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || GatePassEmployeeId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {

                //System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@GatePassEmployeeId", GatePassEmployeeId);
                //cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                //cmd.Parameters.AddWithValue("@AttachDocument", beData.AttachDocument);
                //cmd.Parameters.AddWithValue("@AttachDocumentPath", beData.AttachDocumentPath);
                //cmd.Parameters.AddWithValue("@Description", beData.Description);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "usp_AddGatePassEmployeeDocumentAttach";
                //cmd.ExecuteNonQuery();
            }

        }
        //private void SaveAdmissionEnquiryAcademicDetails(int TranId, BE.FrontDesk.Transaction.AddmissionE beDataColl)
        //{
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    foreach (var beData in beDataColl)
        //    {
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.AdmissionEnquiryId);
        //        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
        //        cmd.Parameters.AddWithValue("@Exam", beData.Exam);
        //        cmd.Parameters.AddWithValue("@PassedYear", beData.PassedYear);
        //        cmd.Parameters.AddWithValue("@SymbolNo", beData.SymbolNo);
        //        cmd.Parameters.AddWithValue("@ObtainedMarks", beData.ObtainedMarks);
        //        cmd.Parameters.AddWithValue("@ObtainPercent", beData.ObtainPercent);
        //        cmd.Parameters.AddWithValue("@Division", beData.Division);
        //        cmd.Parameters.AddWithValue("@GPA", beData.GPA);
        //        cmd.Parameters.AddWithValue("@SchoolColledge", beData.SchoolColledge);
        //        cmd.CommandText = "usp_AddAdmissionEnquiryAcademicDetails";
        //        cmd.ExecuteNonQuery();

        //    }
        //}


        public BE.FrontDesk.Transaction.GatePassEmployeeCollections getAllGatePassEmployee(int UserId, int EntityId)
        {
            BE.FrontDesk.Transaction.GatePassEmployeeCollections dataColl = new BE.FrontDesk.Transaction.GatePassEmployeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllGatePassEmployee";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.GatePassEmployee beData = new BE.FrontDesk.Transaction.GatePassEmployee();
                    beData.GatePassEmployeeId = reader.GetInt32(0);
                   // if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                   // if (!(reader[2] is DBNull)) beData.EmployeeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.DesignationId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.PurposeId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.OutTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ValidationTime = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.InTime = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
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
        public BE.FrontDesk.Transaction.GatePassEmployee getGatePassEmployeeById(int UserId, int EntityId, int GatePassEmployeeId)
        {
            BE.FrontDesk.Transaction.GatePassEmployee beData = new BE.FrontDesk.Transaction.GatePassEmployee();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GatePassEmployeeId", GatePassEmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetGatePassEmployeeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.GatePassEmployee();
                    beData.GatePassEmployeeId = reader.GetInt32(0);
                    //if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    //if (!(reader[2] is DBNull)) beData.EmployeeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.DesignationId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.PurposeId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.OutTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ValidationTime = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.InTime = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    //BE.FrontDesk.Transaction.GatePassEmployeeDocumentAttach Attach = new BE.FrontDesk.Transaction.GatePassEmployeeDocumentAttach();

                    //if (!(reader[0] is System.DBNull)) Attach.TranId = reader.GetInt32(0);
                    //if (!(reader[1] is System.DBNull)) Attach.GatePassEmployeeId = reader.GetInt32(1);
                    //if (!(reader[2] is System.DBNull)) Attach.DocumentTypeId = reader.GetInt32(2);
                    //if (!(reader[3] is System.DBNull)) Attach.AttachDocument = reader.GetString(3);
                    //if (!(reader[4] is System.DBNull)) Attach.AttachDocumentPath = reader.GetString(4);
                    //if (!(reader[5] is System.DBNull)) Attach.Description = reader.GetString(5);
                    //beData.GatePassEmployeeDocumentAttachColl.Add(Attach);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int GatePassEmployeeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@GatePassEmployeeId", GatePassEmployeeId);
            cmd.CommandText = "usp_DelGatePassEmployeeById";
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
