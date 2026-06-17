using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Payroll
{
    internal class PeriodSalarySheetDB
    {
        DataAccessLayer1 dal = null;
        public PeriodSalarySheetDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicLib.BE.Payroll.PeriodSalarySheetCollections getAllPeriodSalarySheet(int UserId, int FromYearId, int FromMonthId, int ToYearId, int ToMonthId, int? BranchId=null,int? DepartmentId=null, int? CategoryId=null)
        {
            AcademicLib.BE.Payroll.PeriodSalarySheetCollections dataColl = new AcademicLib.BE.Payroll.PeriodSalarySheetCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);           
            cmd.Parameters.AddWithValue("@FromYearId", FromYearId);
            cmd.Parameters.AddWithValue("@FromMonthId", FromMonthId);
            cmd.Parameters.AddWithValue("@ToYearId", ToYearId);
            cmd.Parameters.AddWithValue("@ToMonthId", ToMonthId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            //cmd.Parameters.AddWithValue("@ForEmployee", ForEmployee);
            cmd.CommandText = "usp_GetPeriodSalarySheet";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Payroll.PeriodSalarySheet beData = new AcademicLib.BE.Payroll.PeriodSalarySheet();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BranchName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Designation = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Category = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.PayHeadingId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.PayHeading = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PayHeadingSNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Amount = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Rate = Convert.ToDouble(reader[11]);

                    //New Field Added by Suresh on 7Jan 2025
                    if (!(reader[12] is DBNull)) beData.Earning = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Deducation = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.Tax = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.Netpayable = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.PayHeadType = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PanNo = reader.GetString(17);
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