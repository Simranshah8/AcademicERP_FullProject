using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class EQuickAccessDB
    {
        DataAccessLayer1 dal = null;
        public EQuickAccessDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
                
        public AcademicLib.BE.Academic.Transaction.EmpAttachmentCollections getEAttForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            AcademicLib.BE.Academic.Transaction.EmpAttachmentCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmpAttachmentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeAttachmentForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmpAttachment beData = new AcademicLib.BE.Academic.Transaction.EmpAttachment();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DocumentType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Extension = reader.GetString(5);
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

        public AcademicLib.BE.Academic.Transaction.EmpComplainCollections getEmpComplainForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            AcademicLib.BE.Academic.Transaction.EmpComplainCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmpComplainCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeComplainForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmpComplain beData = new AcademicLib.BE.Academic.Transaction.EmpComplain();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ComplainDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ActionRemarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ComplainType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ActionTakenBy = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ActionTakenByName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ActionDate = reader.GetDateTime(7);
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
              

        public AcademicLib.BE.Academic.Transaction.EmpLeaveTakenCollections getEmpLeaveTakenForQuickAccessByyId(int UserId, int EntityId, int EmployeeId)
        {
            AcademicLib.BE.Academic.Transaction.EmpLeaveTakenCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmpLeaveTakenCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmpLeaveTakenForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.EmpLeaveTaken beData = new AcademicLib.BE.Academic.Transaction.EmpLeaveTaken();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RequestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.LeaveType = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DateFrom = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DateTo = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.TotalDays = reader.GetDouble(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ApprovedByUser = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.RequestMiti = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FromMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ToMiti = reader.GetString(10);
                    try
                    {
                        if (!(reader[11] is DBNull)) beData.ApprovedTypeId = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) beData.ApprovedRemarks = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.ApprovedLogDateTime = reader.GetDateTime(13);
                        if (!(reader[14] is DBNull)) beData.ApprovedMiti = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.LeaveRequestId = reader.GetInt32(15);

                        if (beData.ApprovedLogDateTime.HasValue)
                            beData.ApprovedMiti = beData.ApprovedMiti + " " + beData.ApprovedLogDateTime.Value.ToString("HH:mm:ss");
                    }
                    catch { }
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int lid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.DocPath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    try
                    {
                        dataColl.Find(p1 => p1.LeaveRequestId == lid).DocumentColl.Add(doc);
                    }
                    catch { }                    
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

        //Student Quick access Starts
        public AcademicLib.BE.Academic.Transaction.StudentComplainCollections getStudentComplainForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            AcademicLib.BE.Academic.Transaction.StudentComplainCollections dataColl = new AcademicLib.BE.Academic.Transaction.StudentComplainCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentComplainForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentComplain beData = new AcademicLib.BE.Academic.Transaction.StudentComplain();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ComplainDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ActionRemarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ComplainType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ActionTakenBy = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ActionTakenByName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ActionDate = reader.GetDateTime(7);
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

        public AcademicLib.BE.Academic.Transaction.StudentLeaveTakenCollections getStudentLeaveTakenForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            AcademicLib.BE.Academic.Transaction.StudentLeaveTakenCollections dataColl = new AcademicLib.BE.Academic.Transaction.StudentLeaveTakenCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentLeaveTakenForQA";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentLeaveTaken beData = new AcademicLib.BE.Academic.Transaction.StudentLeaveTaken();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RequestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.LeaveType = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DateFrom = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DateTo = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.TotalDays = reader.GetDouble(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ApprovedByUser = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.RequestMiti = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FromMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ToMiti = reader.GetString(10);
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

        public AcademicLib.BE.Academic.Transaction.StudentAttachmentForQACollections getStudentAttForQuickAccessById(int UserId, int EntityId, int StudentId)
        {
            AcademicLib.BE.Academic.Transaction.StudentAttachmentForQACollections dataColl = new AcademicLib.BE.Academic.Transaction.StudentAttachmentForQACollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentAttachmentForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.StudentAttachmentForQA beData = new AcademicLib.BE.Academic.Transaction.StudentAttachmentForQA();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DocumentType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Extension = reader.GetString(5);
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