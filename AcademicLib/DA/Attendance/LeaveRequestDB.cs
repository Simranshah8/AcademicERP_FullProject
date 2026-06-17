using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
    internal class LeaveRequestDB
    {
        DataAccessLayer1 dal = null;
        public LeaveRequestDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Attendance.LeaveRequest beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", beData.LeaveTypeId);
            cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
            cmd.Parameters.AddWithValue("@TotalDays", beData.TotalDays);
            cmd.Parameters.AddWithValue("@AlternativeEmployeeId", beData.AlternativeEmployeeId);
            cmd.Parameters.AddWithValue("@MessagetoAllEmployee", beData.MessagetoAllEmployee);
            cmd.Parameters.AddWithValue("@ApprovedBy", beData.ApprovedBy);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@LeaveRequestId", beData.LeaveRequestId);

            if (isModify)
            {
                cmd.CommandText = "sp_UpdateLeaveRequest";
            }
            else
            {
                cmd.CommandText = "sp_AddLeaveRequest";
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            }

            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@LeaveDuration", beData.LeaveDuration);
            cmd.Parameters.AddWithValue("@LeavePeriod", beData.LeavePeriod);
            cmd.Parameters.AddWithValue("@LeaveHours", beData.LeaveHours);

            try
            {
                cmd.ExecuteNonQuery();
               

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (beData.LeaveRequestDocumentColl != null && beData.LeaveRequestDocumentColl.Count > 0)
                    SaveLeaveRequestDocument(resVal.RId, beData.LeaveRequestDocumentColl);


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;
               
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;
                
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public void UpdateLeaveRequest(AcademicLib.BE.Attendance.LeaveRequest beData)
        {
            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveRequestId", beData.LeaveRequestId);
            cmd.Parameters.AddWithValue("@ApprovedBy", beData.ApprovedBy);
            cmd.Parameters.AddWithValue("@ApprovedByUser", beData.ApprovedByUser);
            cmd.Parameters.AddWithValue("@ApprovedRemarks", beData.ApprovedRemarks);
            cmd.Parameters.AddWithValue("@ApprovedType", beData.ApprovedType);
            cmd.CommandText = "sp_UpdateLeaveRequestApproved";                        
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@EmployeeId", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;


            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    beData.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    beData.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    beData.EmployeeId = Convert.ToInt32(cmd.Parameters[7].Value);

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                beData.ResponseMSG = ee.Message;
                beData.IsSuccess = false;                
                //throw ee;
            }
            catch (Exception ee)
            {
                beData.ResponseMSG = ee.Message;
                beData.IsSuccess = false;
            }
            finally
            {
                dal.CloseConnection();
            }
        }
        private void SaveLeaveRequestDocument(int LeaveRequestId,Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {


                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var beData in beDataColl)
                {
                    if (LeaveRequestId == 0)
                        continue;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LeaveRequestId", LeaveRequestId);

                    if(beData.DocumentTypeId.HasValue && beData.DocumentTypeId.Value>0)
                        cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                    else
                        cmd.Parameters.AddWithValue("@DocumentTypeId",DBNull.Value);

                    cmd.Parameters.AddWithValue("@Path", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);

                    cmd.CommandText = "usp_AddLeaveRequestDocument";

                  
                    cmd.ExecuteNonQuery();

            }

        }
        public ResponeValues DeleteById(int UserId, int EntityId, int LeaveRequestId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@LeaveRequestId", LeaveRequestId);
            cmd.CommandText = "usp_DelLeaveRequestById";
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
  
        public AcademicLib.BE.Attendance.LeaveRequestCollections GetAllLeaveRequest(int UserId)
        {
            AcademicLib.BE.Attendance.LeaveRequestCollections dataColl = new AcademicLib.BE.Attendance.LeaveRequestCollections();

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "sp_GetAllLeaveRequest";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Attendance.LeaveRequest beData = new AcademicLib.BE.Attendance.LeaveRequest();
                    beData.LeaveRequestId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.BranchName = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.DepartmentName = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) beData.EmployeeName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) beData.EmployeeCode = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) beData.LeaveTypeName = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) beData.DateFrom = reader.GetDateTime(6);
                    if (!(reader[7] is System.DBNull)) beData.DateTo = reader.GetDateTime(7);
                    if (!(reader[8] is System.DBNull)) beData.TotalDays = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is System.DBNull)) beData.AlternativeEmployeeId = reader.GetInt32(9);
                    if (!(reader[10] is System.DBNull)) beData.MessagetoAllEmployee = reader.GetString(10);
                    if (!(reader[11] is System.DBNull)) beData.ApprovedBy = reader.GetInt32(11);
                    if (!(reader[12] is System.DBNull)) beData.Remarks = reader.GetString(12);
                    if (!(reader[13] is System.DBNull)) beData.UserName = reader.GetString(13);
                    if (!(reader[14] is System.DBNull)) beData.DateFromBS = reader.GetString(14);
                    if (!(reader[15] is System.DBNull)) beData.DateToBS = reader.GetString(15);
                    if (!(reader[16] is System.DBNull)) beData.RequestFrom = reader.GetString(16);

                    if (!(reader[17] is System.DBNull)) beData.LeaveDuration = (AcademicLib.BE.Attendance.LEAVEDURATION)reader.GetInt32(17);
                    if (!(reader[18] is System.DBNull)) beData.LeavePeriod = (AcademicLib.BE.Attendance.LEAVEPERIOD)reader.GetInt32(18);
                    if (!(reader[19] is System.DBNull)) beData.LeaveHours = Convert.ToDouble(reader[19]);

                    if (beData.TotalDays == 0)
                        beData.TotalDays = Math.Abs((beData.DateFrom - beData.DateTo).TotalDays) + 1;

                    dataColl.Add(beData);
                }
                reader.Close();
                return dataColl;
            }
            catch (Exception ee)
            {                
                return dataColl;
            }
            finally
            {
                dal.CloseConnection();
            }

        }
        public AcademicLib.BE.Attendance.LeaveRequest getLeaveRequestById(int LeaveRequestId, int UserId)
        {
            AcademicLib.BE.Attendance.LeaveRequest beData = null;

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeaveRequestId", LeaveRequestId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "sp_GetLeaveRequestById";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Attendance.LeaveRequest();
                    beData.LeaveRequestId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) beData.DepartmentId = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) beData.DesignationId = reader.GetInt32(3);
                    if (!(reader[4] is System.DBNull)) beData.ServiceTypeId = reader.GetInt32(4);
                    if (!(reader[5] is System.DBNull)) beData.EmployeeId = reader.GetInt32(5);
                    //if (!(reader[6] is System.DBNull)) beData.Gender = reader.GetInt32(6);
                    if (!(reader[7] is System.DBNull)) beData.LeaveTypeId = reader.GetInt32(7);
                    if (!(reader[8] is System.DBNull)) beData.DateFrom = reader.GetDateTime(8);
                    if (!(reader[9] is System.DBNull)) beData.DateTo = reader.GetDateTime(9);
                    if (!(reader[10] is System.DBNull)) beData.TotalDays = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is System.DBNull)) beData.AlternativeEmployeeId = reader.GetInt32(11);
                    if (!(reader[12] is System.DBNull)) beData.MessagetoAllEmployee = reader.GetString(12);
                    if (!(reader[13] is System.DBNull)) beData.ApprovedBy = reader.GetInt32(13);
                    if (!(reader[14] is System.DBNull)) beData.Remarks = reader.GetString(14);
                    if (!(reader[15] is System.DBNull)) beData.CUserId = reader.GetInt32(15);

                    if (!(reader[16] is System.DBNull)) beData.LeaveDuration =(AcademicLib.BE.Attendance.LEAVEDURATION)reader.GetInt32(16);
                    if (!(reader[17] is System.DBNull)) beData.LeavePeriod =(AcademicLib.BE.Attendance.LEAVEPERIOD)reader.GetInt32(17);
                    if (!(reader[18] is System.DBNull)) beData.LeaveHours = Convert.ToDouble(reader[18]);

                }
                reader.Close();
                return beData;
            }
            catch (Exception ee)
            {
                return null;
            }
            finally
            {
                dal.CloseConnection();
            }

        }

        public ResponeValues SaveFromApp(AcademicLib.API.Attendance.LeaveRequest beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", beData.LeaveTypeId);
            cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
            cmd.Parameters.AddWithValue("@TotalDays", beData.TotalDays);
            //cmd.Parameters.AddWithValue("@TotalDays",Math.Abs((beData.DateFrom-beData.DateTo).TotalDays+1 ));
            cmd.Parameters.AddWithValue("@LeaveDuration", beData.LeaveDuration);
            cmd.Parameters.AddWithValue("@LeavePeriod", beData.LeavePeriod);
            cmd.Parameters.AddWithValue("@LeaveHours", beData.LeaveHours);
            cmd.Parameters.AddWithValue("@MessagetoAllEmployee", beData.MessageToEmployee);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@Lat", beData.Lat);
            cmd.Parameters.AddWithValue("@Lan", beData.Lan);
            cmd.Parameters.AddWithValue("@Location", beData.Location);
            cmd.Parameters.AddWithValue("@AlternativeEmployeeId", beData.AlternativeEmployeeId);
            cmd.CommandText = "usp_AddLeaveRequestFromApp";
            cmd.Parameters.Add("@LeaveRequestId", System.Data.SqlDbType.Int);                        
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;            
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@NotificationMSG", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[20].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@SubstitueEmpId", beData.SubstitueEmpId);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.RId= Convert.ToInt32(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[18].Value);

                if (resVal.RId > 0)
                {

                    if (!(cmd.Parameters[19].Value is DBNull))
                        resVal.CUserName = Convert.ToString(cmd.Parameters[19].Value);

                    if (!(cmd.Parameters[20].Value is DBNull))
                        resVal.JsonStr = Convert.ToString(cmd.Parameters[20].Value);

                    if (beData.DocumentColl != null && beData.DocumentColl.Count() > 0)
                        SaveLeaveRequestDocument(resVal.RId, beData.DocumentColl);
                }
                
                dal.CommitTransaction();

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;                                
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;
            }
            finally
            {
                dal.CloseConnection();
            }

            return resVal;
        }

        public ResponeValues LeaveApproved(AcademicLib.API.Attendance.LeaveApprove beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveRequestId", beData.LeaveRequestId);
            cmd.Parameters.AddWithValue("@ApprovedBy", beData.ApprovedBy);
            cmd.Parameters.AddWithValue("@ApprovedByUser", beData.ApprovedByUser);
            cmd.Parameters.AddWithValue("@ApprovedRemarks", beData.ApprovedRemarks);
            cmd.Parameters.AddWithValue("@ApprovedType", beData.ApprovedType);           
            cmd.CommandText = "sp_UpdateLeaveRequestApproved";
            cmd.Parameters.Add("@UserId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@NotificationMSG", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.CUserName = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.JsonStr = Convert.ToString(cmd.Parameters[9].Value);


                dal.CommitTransaction();

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
                resVal.IsSuccess = false;
            }
            finally
            {
                dal.CloseConnection();
            }

            return resVal;
        }

        public AcademicLib.RE.Attendance.EmpLeaveRequestCollections getEmpLeaveRequestLst(int UserId,DateTime? dateFrom,DateTime? dateTo,int LeaveStatus,int? EmployeeId)
        {
            AcademicLib.RE.Attendance.EmpLeaveRequestCollections dataColl = new RE.Attendance.EmpLeaveRequestCollections();

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                cmd.Parameters.AddWithValue("@DateTo", dateTo);
                cmd.Parameters.AddWithValue("@LeaveStatus", LeaveStatus);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.CommandText = "usp_GetEmpLeaveRequest";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.EmpLeaveRequest beData = new RE.Attendance.EmpLeaveRequest();
                    if (!(reader[0] is System.DBNull)) beData.LeaveRequestId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) beData.LeaveType = reader.GetString(6);
                    if (!(reader[7] is System.DBNull)) beData.DateFrom = reader.GetDateTime(7);
                    if (!(reader[8] is System.DBNull)) beData.DateTo = reader.GetDateTime(8);
                    if (!(reader[9] is System.DBNull)) beData.MitiFrom = reader.GetString(9);
                    if (!(reader[10] is System.DBNull)) beData.MitiTo = reader.GetString(10);
                    if (!(reader[11] is System.DBNull)) beData.TotalDays = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is System.DBNull)) beData.LeaveDuration = ((AcademicLib.BE.Attendance.LEAVEDURATION)reader.GetInt32(12)).ToString();
                    if (!(reader[13] is System.DBNull)) beData.LeavePeriod = ((AcademicLib.BE.Attendance.LEAVEPERIOD)reader.GetInt32(13)).ToString();
                    if (!(reader[14] is System.DBNull)) beData.LeaveHours = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is System.DBNull)) beData.Al_EmployeeCode = reader.GetString(15);
                    if (!(reader[16] is System.DBNull)) beData.AL_Name = reader.GetString(16);
                    if (!(reader[17] is System.DBNull)) beData.MessageToAllEmployee = reader.GetString(17);
                    if (!(reader[18] is System.DBNull)) beData.ApprovedBy = reader.GetString(18);
                    if (!(reader[19] is System.DBNull)) beData.ApprovedType = ((AcademicLib.BE.Attendance.APPROVEDTYPES)reader.GetInt32(19)).ToString();
                    if (!(reader[20] is System.DBNull)) beData.ApprovedRemarks = reader.GetString(20);
                    if (!(reader[21] is System.DBNull)) beData.AprovedLogDate = reader.GetDateTime(21);
                    if (!(reader[22] is System.DBNull)) beData.ApprovedLogMiti = reader.GetString(22);
                    if (!(reader[23] is System.DBNull)) beData.Remarks = reader.GetString(23);
                    if (!(reader[24] is System.DBNull)) beData.Lan = Convert.ToDouble(reader[24]);
                    if (!(reader[25] is System.DBNull)) beData.Lat = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is System.DBNull)) beData.Location = reader.GetString(26);
                    if (!(reader[27] is System.DBNull)) beData.LogDateTime = reader.GetDateTime(27);
                    if (!(reader[28] is System.DBNull)) beData.LogMiti = reader.GetString(28);
                    if (!(reader[29] is System.DBNull)) beData.EmployeeId = reader.GetInt32(29);
                    if (!(reader[30] is System.DBNull)) beData.BranchName = reader.GetString(30);
                    if (!(reader[31] is System.DBNull)) beData.BranchAddress = reader.GetString(31);

                    if (!(reader[19] is System.DBNull)) beData.ApprovedTypeId =reader.GetInt32(19);

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) doc.DocumentTypeId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) doc.DocPath = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) doc.Description = reader.GetString(3);
                    dataColl.Find(p1 => p1.LeaveRequestId == tranId).DocumentColl.Add(doc);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.LeaveBalance beData = new RE.Attendance.LeaveBalance();
                    if (!(reader[0] is System.DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.LeaveType = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.OpeningQty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is System.DBNull)) beData.QuotaQty = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is System.DBNull)) beData.LeaveQty = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is System.DBNull)) beData.BalanceQty = Convert.ToDouble(reader[5]);
                    dataColl.LeaveBalanceColl.Add(beData);
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

        //In below method Added batch, classyear, semester by suresh on 18 Magh starts
        public AcademicLib.RE.Attendance.StudentLeaveRequestCollections getStudentLeaveRequestLst(int UserId, DateTime? dateFrom, DateTime? dateTo, int LeaveStatus, int? StudentId,int? ClassId,int? SectionId,int? AcademicYearId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.RE.Attendance.StudentLeaveRequestCollections dataColl = new RE.Attendance.StudentLeaveRequestCollections();

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                cmd.Parameters.AddWithValue("@DateTo", dateTo);
                cmd.Parameters.AddWithValue("@LeaveStatus", LeaveStatus);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@BatchId", BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId",ClassYearId);
                cmd.CommandText = "usp_GetStudentLeaveRequest";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.StudentLeaveRequest beData = new RE.Attendance.StudentLeaveRequest();
                    if (!(reader[0] is System.DBNull)) beData.LeaveRequestId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) beData.Gender = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is System.DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is System.DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is System.DBNull)) beData.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is System.DBNull)) beData.Level = reader.GetString(10);
                    if (!(reader[11] is System.DBNull)) beData.Faculty = reader.GetString(11);
                    if (!(reader[12] is System.DBNull)) beData.Semester = reader.GetString(12);
                    if (!(reader[13] is System.DBNull)) beData.ClassYear = reader.GetString(13);
                    if (!(reader[14] is System.DBNull)) beData.Batch = reader.GetString(14);                      
                    if (!(reader[15] is System.DBNull)) beData.LeaveType = reader.GetString(15);
                    if (!(reader[16] is System.DBNull)) beData.DateFrom = reader.GetDateTime(16);
                    if (!(reader[17] is System.DBNull)) beData.DateTo = reader.GetDateTime(17);
                    if (!(reader[18] is System.DBNull)) beData.MitiFrom = reader.GetString(18);
                    if (!(reader[19] is System.DBNull)) beData.MitiTo = reader.GetString(19);
                    if (!(reader[20] is System.DBNull)) beData.TotalDays = Convert.ToDouble(reader[20]);
                    if (!(reader[21] is System.DBNull)) beData.LeaveDuration = ((AcademicLib.BE.Attendance.LEAVEDURATION)reader.GetInt32(21)).ToString();
                    if (!(reader[22] is System.DBNull)) beData.LeavePeriod = ((AcademicLib.BE.Attendance.LEAVEPERIOD)reader.GetInt32(22)).ToString();
                    if (!(reader[23] is System.DBNull)) beData.LeaveHours = Convert.ToDouble(reader[23]);                     
                    if (!(reader[24] is System.DBNull)) beData.ApprovedBy = reader.GetString(24);
                    if (!(reader[25] is System.DBNull)) beData.ApprovedType = ((AcademicLib.BE.Attendance.APPROVEDTYPES)reader.GetInt32(25)).ToString();
                    if (!(reader[26] is System.DBNull)) beData.ApprovedRemarks = reader.GetString(26);
                    if (!(reader[27] is System.DBNull)) beData.AprovedLogDate = reader.GetDateTime(27);
                    if (!(reader[28] is System.DBNull)) beData.ApprovedLogMiti = reader.GetString(28);
                    if (!(reader[29] is System.DBNull)) beData.Remarks = reader.GetString(29);
                    if (!(reader[30] is System.DBNull)) beData.Lan = Convert.ToDouble(reader[30]);
                    if (!(reader[31] is System.DBNull)) beData.Lat = Convert.ToDouble(reader[31]);
                    if (!(reader[32] is System.DBNull)) beData.Location = reader.GetString(32);
                    if (!(reader[33] is System.DBNull)) beData.LogDateTime = reader.GetDateTime(33);
                    if (!(reader[34] is System.DBNull)) beData.LogMiti = reader.GetString(34);
                    if (!(reader[35] is System.DBNull)) beData.StudentId = reader.GetInt32(35);

                    if (!(reader[36] is System.DBNull)) beData.ClassId = reader.GetInt32(36);
                    if (!(reader[37] is System.DBNull)) beData.SectionId = reader.GetInt32(37);
                    if (!(reader[38] is System.DBNull)) beData.BatchId = reader.GetInt32(38);
                    if (!(reader[39] is System.DBNull)) beData.SemesterId = reader.GetInt32(39);
                    if (!(reader[40] is System.DBNull)) beData.ClassYearId = reader.GetInt32(40);

                    if (!(reader[25] is System.DBNull)) beData.ApprovedTypeId = reader.GetInt32(25);

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) doc.DocumentTypeId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) doc.DocPath = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) doc.Description = reader.GetString(3);
                    dataColl.Find(p1 => p1.LeaveRequestId == tranId).DocumentColl.Add(doc);
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