using AcademicLib.RE.Academic;
using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Academic.Reporting
{ 
    internal class StudentSummaryModelDB
    {
        DataAccessLayer1 dal = null;
        public StudentSummaryModelDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public StudentSummaryModelCollections GetStudentDynamicSummary(int UserId,int? AcademicYearId)
        {
            StudentSummaryModelCollections dataColl = new StudentSummaryModelCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentDynamicSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                int fieldCount = reader.FieldCount;
                List<string> dynamicColumns = new List<string>();
                for (int i = 10; i < fieldCount; i++)
                {
                    dynamicColumns.Add(reader.GetName(i));
                }
                while (reader.Read())
                {
                    StudentSummaryModel beData = new StudentSummaryModel();

                    if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassTeacher = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CTContactNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Batch = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Faculty = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Level = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Semester = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ClassYear = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TotalStudent = reader.GetInt32(9);
                    beData.DynamicCounts = new Dictionary<string, int>();
                    int ind = 0;
                    for (int i = 0; i < dynamicColumns.Count; i++)
                    {
                        var colName = dynamicColumns[i];
                        int val = 0;
                        if (!reader.IsDBNull(i + 10) && int.TryParse(reader[i + 10].ToString(), out val))
                        {
                            beData.DynamicCounts[colName] = val;
                        }
                    }
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