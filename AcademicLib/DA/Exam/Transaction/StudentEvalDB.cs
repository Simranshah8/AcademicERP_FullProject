using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class StudentEvalDB
    {
        DataAccessLayer1 dal = null;
        public StudentEvalDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
       
        public AcademicLib.BE.Exam.Transaction.StudentEvalCollections getAllStudentEval(int UserId, int EntityId, int? AcademicYearId, int ClassId,int SectionId, int ExamTypeId, string ExamTypeIdColl=null, string StudentIdColl = null, int? BatchId=null,int? SemesterId=null,int? ClassYearId=null)
        {
            AcademicLib.BE.Exam.Transaction.StudentEvalCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentEvalCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamTypeIdColl", ExamTypeIdColl);
            cmd.Parameters.AddWithValue("@StudentIdColl", StudentIdColl);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            //cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.CommandText = "usp_GetExamEvaluation";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                
                    AcademicLib.BE.Exam.Transaction.StudentEval beData = new AcademicLib.BE.Exam.Transaction.StudentEval();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.DOB_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.F_ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.M_Contact = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Height = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Weigth = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Photopath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.CompLogoPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ExamName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.RollNo = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.RegNo = reader.GetString(16);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                dataColl.AchievementColl = new AcademicLib.BE.Exam.Transaction.AchievementDetailCollections();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.AchievementDetail det = new AcademicLib.BE.Exam.Transaction.AchievementDetail();
                    if (!(reader[0] is DBNull)) det.Remarks = reader.GetString(0);
                    if (!(reader[1] is DBNull)) det.Point = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) det.StudentId = reader.GetInt32(2);

                    dataColl.Find(p1 => p1.StudentId==det.StudentId).AchievementColl.Add(det);
                }
                reader.NextResult();
                dataColl.EvaluationBarColl = new AcademicLib.BE.Exam.Transaction.EvaluationBarCollections();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.EvaluationBar det1 = new AcademicLib.BE.Exam.Transaction.EvaluationBar();
                    if (!(reader[0] is DBNull)) det1.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.ExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.SubjectId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.SubjectName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det1.Obtain = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det1.ClassId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) det1.ExamTypeName = reader.GetString(6);
                    dataColl.Find(p1 => p1.StudentId == det1.StudentId).EvaluationBarColl.Add(det1);
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