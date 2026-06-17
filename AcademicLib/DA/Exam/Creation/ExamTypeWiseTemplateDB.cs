using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Creation
{
    internal class ExamTypeWiseTemplateDB
    {
        DataAccessLayer1 dal = null;
        public ExamTypeWiseTemplateDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate( int UserId, BE.Exam.Creation.ExamTypeWiseTemplateCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var fst = dataColl.First();

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamTypeId", fst.ExamTypeId);
                cmd.Parameters.AddWithValue("@ExamTypeGroupId", fst.ExamTypeGroupId);
                cmd.CommandText = "usp_DelExamTypeWiseTemplate";
                cmd.ExecuteNonQuery();

                foreach (var ss in dataColl)
                {
                    cmd.Parameters.Clear(); 
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ExamTypeId", ss.ExamTypeId);
                    cmd.Parameters.AddWithValue("@ExamTypeGroupId", ss.ExamTypeGroupId);
                    cmd.Parameters.AddWithValue("@ClassId", ss.ClassId);
                    cmd.Parameters.AddWithValue("@ReportTemplateId", fst.ReportTemplateId);
                    cmd.Parameters.AddWithValue("@AdmitCardTemplateId", fst.AdmitCardTemplateId);
                    cmd.CommandText = "usp_AddExamTypeWiseTemplate";                     
                    cmd.ExecuteNonQuery();
                }
 
                dal.CommitTransaction();
                 
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Exam Wise Template Done";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public BE.Exam.Creation.ExamTypeWiseTemplateCollections getExamTypeWiseTemplate(int UserId, int? ExamTypeId,int? ExamTypeGroupId)
        {
            BE.Exam.Creation.ExamTypeWiseTemplateCollections dataColl = new BE.Exam.Creation.ExamTypeWiseTemplateCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);             
            cmd.CommandText = "usp_GetExamTypeWiseTemplate";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Creation.ExamTypeWiseTemplate beData = new BE.Exam.Creation.ExamTypeWiseTemplate();                
                    if (!(reader[0] is DBNull)) beData.ExamTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamTypeGroupId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ReportTemplateId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ReportName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ReportTemplateId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ReportName = reader.GetString(7);
                    if (!(reader[6] is DBNull)) beData.AdmitCardTemplateId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AdmitCardReportName = reader.GetString(7);
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
        public ResponeValues DelExamTypeWiseTemplate(int UserId, int? ExamTypeId, int? ExamTypeGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
                cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
                cmd.CommandText = "usp_DelExamTypeWiseTemplate";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Delete Exam Wise Template";

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
