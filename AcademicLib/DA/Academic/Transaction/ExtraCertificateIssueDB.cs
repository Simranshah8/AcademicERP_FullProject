using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class ExtraCertificateIssueDB
    {
        DataAccessLayer1 dal = null;
        public ExtraCertificateIssueDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExtraEntityId", beData.ExtraEntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);            
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@DOB", beData.DOB);
            cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
            cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@Department", beData.Department);
            cmd.Parameters.AddWithValue("@Designation", beData.Designation);
            cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
            cmd.Parameters.AddWithValue("@SectionName", beData.SectionName);
            cmd.Parameters.AddWithValue("@RollNo", beData.RollNo);
            cmd.Parameters.AddWithValue("@RegdNo", beData.RegdNo);
            cmd.Parameters.AddWithValue("@Attributes", beData.Attributes);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExtraCertificateIssue";
            }
            else
            {
                cmd.Parameters[20].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExtraCertificateIssue";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IssueDate", beData.IssueDate);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[20].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[20].Value);

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[21].Value);

                if (!(cmd.Parameters[22].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[22].Value);

                if (!(cmd.Parameters[23].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[23].Value);

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

        public ResponeValues getAutoNo(int UserId,int ExtraEntityId, int? AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int); 
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetExtraCertificateNo";
            cmd.Parameters.AddWithValue("@ExtraEntityId", ExtraEntityId);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[2].Value);
                  
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
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
        public AcademicLib.BE.Academic.Transaction.ExtraCertificateIssueCollections getAllExtraCertificateIssue(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.ExtraCertificateIssueCollections dataColl = new AcademicLib.BE.Academic.Transaction.ExtraCertificateIssueCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExtraCertificateIssue";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                int sno = 1;
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue beData = new AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue();
                    beData.SNo = sno;
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DOB = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EmailId = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ForName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RegdNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ClassName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IssueMiti = reader.GetString(9);
                    dataColl.Add(beData);
                    sno++;
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
        public AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue getExtraCertificateIssueById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue beData = new AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExtraCertificateIssueById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExtraEntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AcademicYearId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.AutoNumber = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.StudentId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.EmployeeId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Address = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.DOB = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.MotherName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.EmailId = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Department = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Designation = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClassName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.SectionName = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.RollNo = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.RegdNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Attributes = reader.GetString(19);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelExtraCertificateIssueById";
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