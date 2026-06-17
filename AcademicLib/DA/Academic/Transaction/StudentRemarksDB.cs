using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class StudentRemarksDB
    {
        DataAccessLayer1 dal = null;
        public StudentRemarksDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.StudentRemarks beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
            cmd.Parameters.AddWithValue("@RemarksTypeId", beData.RemarksTypeId);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@FilePath", beData.FilePath);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStudentRemarks";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStudentRemarks";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;            
            cmd.Parameters.AddWithValue("@RemarksFor", beData.RemarksFor);

            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@MSG", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Point", beData.Point);

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

                if (!(cmd.Parameters[13].Value is DBNull) && resVal.IsSuccess)
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull) && resVal.IsSuccess)
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[14].Value);

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

        public ResponeValues SaveUpdate(int UserId,List<BE.Academic.Transaction.StudentRemarks> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 
            try
            {

                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                    cmd.Parameters.AddWithValue("@RemarksTypeId", beData.RemarksTypeId);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@FilePath", beData.FilePath);
                    cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@RemarksFor", beData.RemarksFor);
                    cmd.Parameters.AddWithValue("@Point", beData.Point);
                    cmd.CommandText = "usp_AddStudentRemarks";          
              
                    cmd.ExecuteNonQuery();
  
                }
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Remarks Updated";

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

        public AcademicLib.BE.Academic.Transaction.StudentRemarksCollections getRemarks(int UserId,int StudentId)
        {
            AcademicLib.BE.Academic.Transaction.StudentRemarksCollections dataColl = new AcademicLib.BE.Academic.Transaction.StudentRemarksCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_GetAllStudentRemarks";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentRemarks beData = new BE.Academic.Transaction.StudentRemarks();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ForDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.RemarksTypeId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Remarks = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FilePath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EmployeeId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.RemarksFor =(BE.Academic.Transaction.REMARKSFOR) reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Point = Convert.ToDouble(reader[8]);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStudentRemarksById";
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

        public AcademicLib.RE.Academic.StudentRemarksCollections getRemarksList(int UserId,int AcademicYearId, DateTime dateFrom,DateTime dateTo,int? remarksTypeId,int? studentId, bool IsStudentUser = false, int? remarksFor=null)
        {
            AcademicLib.RE.Academic.StudentRemarksCollections dataColl = new RE.Academic.StudentRemarksCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@RemarksTypeId", remarksTypeId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@IsStudentUser", IsStudentUser);
            cmd.Parameters.AddWithValue("@RemarksFor", remarksFor);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetRemarksList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.StudentRemarks beData = new RE.Academic.StudentRemarks();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ForDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.ForMiti = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.RemarsType = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Remarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.FilePath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.UserName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.UserPhotoPath = reader.GetString(15);
                    if (!(reader[16] is DBNull))  beData.RemarksFor = ((BE.Academic.Transaction.REMARKSFOR)reader.GetInt32(16)).ToString();
                    if (!(reader[17] is DBNull)) beData.Point = Convert.ToDouble(reader[17]);

                    if (!(reader[18] is DBNull)) beData.NY = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.NM = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ND = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.MonthName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.LogDate = reader.GetDateTime(22);
                    if (!(reader[23] is DBNull)) beData.LogMiti = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.RemarksBy = reader.GetString(24);

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
    }
}
