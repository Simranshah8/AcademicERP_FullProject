using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class TeacherWiseQuotaDB
    {
        DataAccessLayer1 dal = null;
        public TeacherWiseQuotaDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId,int BranchId, BE.Academic.Transaction.TeacherWiseQuotaCollections DataColl)
        {
            ResponeValues responeValues = new ResponeValues();
            dal.OpenConnection();
            SqlCommand command = dal.GetCommand();
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                command.Parameters.Add("@ResponseMSG", SqlDbType.NVarChar, 254);
                command.Parameters.Add("@IsValid", SqlDbType.Bit);
                command.Parameters[0].Direction = ParameterDirection.Output;
                command.Parameters[1].Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@UserId", UserId);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("EmployeeId", typeof(int));
                dataTable.Columns.Add("BranchId", typeof(int));
                dataTable.Columns.Add("AcademicYearId", typeof(int));
                dataTable.Columns.Add("WeekDay", typeof(int));
                dataTable.Columns.Add("NoofPeriod", typeof(int));
                dataTable.Columns.Add("TotalPeriod", typeof(int));
                dataTable.Columns.Add("CanBlock", typeof(bool));

                foreach (BE.Academic.Transaction.TeacherWiseQuota dbData in (List<BE.Academic.Transaction.TeacherWiseQuota>)DataColl)
                {
                    if (dbData.NoofPeriod > 0 && dbData.EmployeeId.HasValue)
                    {
                        int totalPeriod = 0; 
                        foreach (BE.Academic.Transaction.TeacherWiseQuota innerData in (List<BE.Academic.Transaction.TeacherWiseQuota>)DataColl)
                        {
                            if (innerData.EmployeeId == dbData.EmployeeId)
                            {
                                totalPeriod += innerData.NoofPeriod ?? 0;
                            }
                        }
                        DataRow row = dataTable.NewRow();
                        row["EmployeeId"] = dbData.EmployeeId;
                        row["BranchId"] = BranchId;
                        row["AcademicYearId"] = AcademicYearId;
                        row["WeekDay"] = dbData.WeekDay;
                        row["NoofPeriod"] = dbData.NoofPeriod ?? 0;
                        row["TotalPeriod"] = totalPeriod;
                        row["CanBlock"] = dbData.CanBlock;
                        dataTable.Rows.Add(row);
                    }
                }
                command.Parameters.AddWithValue("@TeacherWiseQuotaColl", dataTable).SqlDbType = SqlDbType.Structured;
                command.CommandText = "usp_AddTeacherWiseQuota";
                command.ExecuteNonQuery();
                if (!(command.Parameters[0].Value is DBNull))
                    responeValues.ResponseMSG = Convert.ToString(command.Parameters[0].Value);
                if (!(command.Parameters[1].Value is DBNull))
                    responeValues.IsSuccess = Convert.ToBoolean(command.Parameters[1].Value);
            }
            catch (Exception ex)
            {
                responeValues.IsSuccess = false;
                responeValues.ResponseMSG = ex.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return responeValues;
        }

        public BE.Academic.Transaction.TeacherWiseQuotaCollections GetTeacherWiseQuota(int UserId, int? DepartmentId, int? EntityId)
        {
            BE.Academic.Transaction.TeacherWiseQuotaCollections dataColl = new BE.Academic.Transaction.TeacherWiseQuotaCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetTeacherWiseQuota";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.TeacherWiseQuota beData = new BE.Academic.Transaction.TeacherWiseQuota();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.WeekDay = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.NoofPeriod = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.TotalPeriod = reader.GetInt32(7);
                    //Added fiels By Prashant
                    if (!(reader[8] is DBNull)) beData.TranId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.CanBlock = Convert.ToBoolean(reader[9]);
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
        public BE.Academic.Transaction.TeacherWiseQuotaCollections GetAllTeacherWiseQuota(int UserId, int? DepartmentId,int? DesignationId, int? EntityId)
        {
            BE.Academic.Transaction.TeacherWiseQuotaCollections dataColl = new BE.Academic.Transaction.TeacherWiseQuotaCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@DesignationId", DesignationId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllTeacherWiseQuota";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.TeacherWiseQuota beData = new BE.Academic.Transaction.TeacherWiseQuota();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.TotalPeriod = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.AssignedPeriod = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.CanBlock = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EmpStatus = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RemainingQuota = reader.GetInt32(9);
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

        public RE.Academic.TeacherQuotaCollections GetAllTeacherQuota(int UserId)
        {
            RE.Academic.TeacherQuotaCollections dataColl = new RE.Academic.TeacherQuotaCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAllTeacherQuota";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Academic.TeacherQuota beData = new RE.Academic.TeacherQuota();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RemainingQuota = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.CanBlock = Convert.ToBoolean(reader[2]);
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