using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicLib.RE;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA
{
    internal class PrintLogsDB
    {
        DataAccessLayer1 dal = null;
        public PrintLogsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public RE.ExamTypeCollection  GetExamTypeData(int userId, int? ExamTypeId, int? ExamTypeGroupId, int? AcademicYearId)
        {
            RE.ExamTypeCollection dataColl = new ExamTypeCollection();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.CommandText = "usp_GetMarksheetPrintLog";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        ExamTypeDataList beData = new ExamTypeDataList();
                        if (!(reader[0] is DBNull)) beData.UserId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) beData.UserName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                        if (!(reader[3] is DBNull)) beData.RegNo = reader.GetString(3);
                        if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                        if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                        if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.Batch = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.Semester = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.ClassYear = reader.GetString(9);
                        if (!(reader[10] is DBNull)) beData.Faculty = reader.GetString(10);
                        if (!(reader[11] is DBNull)) beData.Level = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.PublicIP = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.LogDate = reader.GetDateTime(13);
                        if (!(reader[14] is DBNull)) beData.LogMitiTme = reader.GetString(14);
                        dataColl.Add(beData);
                    }
                    reader.Close();
                    dataColl.IsSuccess = true;
                    dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
                }  
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
