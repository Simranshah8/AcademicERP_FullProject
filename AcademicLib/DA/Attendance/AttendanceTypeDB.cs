using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
    internal class AttendanceTypeDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceTypeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.AttendanceType beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            //dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Code", beData.Code);
            cmd.Parameters.AddWithValue("@AttendanceTypes", beData.AttendanceTypes);
            cmd.Parameters.AddWithValue("@PeriodType", beData.PeriodType);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@CalculationValue", beData.CalculationValue);
            cmd.Parameters.AddWithValue("@PayHeadingId", beData.PayHeadingId);
            cmd.Parameters.AddWithValue("@SNo", beData.SNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@AttendanceTypeId", beData.AttendanceTypeId);
            if (isModify)
            {
                cmd.CommandText = "sp_UpdateAttendanceType";
            }
            else
            { 
                cmd.CommandText = "sp_AddAttendanceType";
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            }

            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;


            try
            {
                cmd.ExecuteNonQuery();
                

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (beData.AttendanceTypeDetailsColl != null && beData.AttendanceTypeDetailsColl.Count > 0)
                    SaveAttendanceTypeDetails(resVal.RId, beData.AttendanceTypeDetailsColl);

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

        private void SaveAttendanceTypeDetails(int AttendanceTypeId,AcademicLib.BE.Payroll.AttendanceTypeDetailsCollections beDataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var beData in beDataColl)
            {
                if (beData.CalculationValue > 0)
                {
                    cmd.Parameters.Clear();

                    if (beData.BranchId == 0)
                        beData.BranchId = null;

                    if (beData.DepartmentId == 0)
                        beData.DepartmentId = null;

                    if (beData.CategoryId == 0)
                        beData.CategoryId = null;

                    cmd.Parameters.AddWithValue("@AttendanceTypeId", AttendanceTypeId);                    
                    cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
                    cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
                    cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
                    cmd.Parameters.AddWithValue("@CalculationValue", beData.CalculationValue);

                    cmd.CommandText = "sp_AddAttendanceTypeDetails";
                    cmd.ExecuteNonQuery();
                }            
            }

        }
        public void Delete(AcademicLib.BE.Payroll.AttendanceType beData)
        {
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AttendanceTypeId", beData.AttendanceTypeId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "sp_DeleteAttendanceType";
            try
            {
                cmd.ExecuteNonQuery();
                dal.CommitTransaction();

                if (!(cmd.Parameters[1].Value is DBNull))
                    beData.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    beData.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
            }
            finally
            {
                dal.CloseConnection();
            }

        }
        public AcademicLib.BE.Payroll.AttendanceTypeCollections getAllAttendanceType(int UserId)
        {
            AcademicLib.BE.Payroll.AttendanceTypeCollections dataColl = new AcademicLib.BE.Payroll.AttendanceTypeCollections();

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CreateBy", UserId);
                cmd.CommandText = "sp_GetAllAttendanceType";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Payroll.AttendanceType beData = new AcademicLib.BE.Payroll.AttendanceType();
                    beData.AttendanceTypeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.AttendanceTypes = (AcademicLib.BE.Payroll.AttendancesTypes)reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) beData.PeriodType = (AcademicLib.BE.Payroll.PeriodsTypes)reader.GetInt32(3);
                    if (!(reader[4] is System.DBNull)) beData.Description = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) beData.CalculationValue = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is System.DBNull)) beData.PayHeadingName = reader.GetString(6);
                    if (!(reader[7] is System.DBNull)) beData.SNo = reader.GetInt32(7);
                    if (!(reader[8] is System.DBNull)) beData.Code = reader.GetString(8);
                    if (!(reader[9] is System.DBNull)) beData.PayHeadingId = reader.GetInt32(9);
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
        public AcademicLib.BE.Payroll.AttendanceType getAttendanceTypeById(int AttendanceTypeId, int UserId)
        {
            AcademicLib.BE.Payroll.AttendanceType beData = null;

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttendanceTypeId", AttendanceTypeId);
                cmd.Parameters.AddWithValue("@CreateBy", UserId);
                cmd.CommandText = "sp_GetAttendanceTypeById";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Payroll.AttendanceType();
                    beData.AttendanceTypeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) beData.AttendanceTypes = (AcademicLib.BE.Payroll.AttendancesTypes)reader.GetInt32(3);
                    if (!(reader[4] is System.DBNull)) beData.PeriodType = (AcademicLib.BE.Payroll.PeriodsTypes)reader.GetInt32(4);
                    if (!(reader[5] is System.DBNull)) beData.Description = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) beData.CalculationValue = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is System.DBNull)) beData.PayHeadingId = reader.GetInt32(7);
                    if (!(reader[8] is System.DBNull)) beData.SNo = reader.GetInt32(8);                    
                }
                reader.NextResult();
                beData.AttendanceTypeDetailsColl = new BE.Payroll.AttendanceTypeDetailsCollections();
                while (reader.Read())
                {
                    AcademicLib.BE.Payroll.AttendanceTypeDetails det = new BE.Payroll.AttendanceTypeDetails();
                    if (!(reader[0] is System.DBNull)) det.BranchId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) det.DepartmentId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) det.CategoryId = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) det.CalculationValue = Convert.ToDouble(reader[3]);
                    beData.AttendanceTypeDetailsColl.Add(det);
                }

                if (beData.AttendanceTypeDetailsColl.Count == 0)
                    beData.AttendanceTypeDetailsColl.Add(new BE.Payroll.AttendanceTypeDetails());

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
    }
}