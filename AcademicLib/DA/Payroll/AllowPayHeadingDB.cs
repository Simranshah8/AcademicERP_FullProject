using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Payroll
{
    internal class AllowPayHeadingDB
    {
        DataAccessLayer1 dal = null;
        public AllowPayHeadingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
               
        public ResponeValues UpdateAllowPayHeading(int UserId, List<AcademicLib.BE.Payroll.AllowPayHeading> DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsValid", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("EmployeeId", typeof(int));
                tableAllocation.Columns.Add("PayHeadingId", typeof(int));
                

                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["EmployeeId"] = v.EmployeeId;
                    row["PayHeadingId"] = v.PayHeadingId;                    
                    tableAllocation.Rows.Add(row);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@AllowPayHeadColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddAllowPayHeading";
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);

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


        public AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections getAllAllowPayHeading(int UserId, int EntityId, int? BranchId, int? DepartmentId, int? CategoryId)
        {
            AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.CommandText = "usp_GetEmployeForAllowPayHeading";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Payroll.EmployeeForAllowPayHeading beData = new AcademicLib.BE.Payroll.EmployeeForAllowPayHeading();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PayHeadingId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.IsAllow = Convert.ToBoolean(reader[2]);
                    if (!(reader[3] is DBNull)) beData.EmployeeCode = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.EnrollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.EmployeeName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Branch = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Department = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Designation = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.SNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.PayHeading = reader.GetString(10);
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