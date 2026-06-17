using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class SubjectMappingStudentWiseDB
    {
        DataAccessLayer1 dal = null;
        public SubjectMappingStudentWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId, List<BE.Academic.Transaction.OptionalSubjectStudentWise> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@ClassId", dataColl.First().ClassId);
                cmd.Parameters.AddWithValue("@SectionId", dataColl.First().SectionId);
                cmd.Parameters.AddWithValue("@SemesterId", dataColl.First().SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", dataColl.First().ClassYearId);
                cmd.Parameters.AddWithValue("@BatchId", dataColl.First().BatchId);
                cmd.CommandText = "usp_DelSubjectmappingStudentWise";
                cmd.ExecuteNonQuery();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (var beData in dataColl)
                {
                    if (beData.TranIdColl != null)
                    {
                        foreach (var tId in beData.TranIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TranId", tId);
                            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                            cmd.Parameters.AddWithValue("@CreateBy", UserId);
                            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tbl_SubjectMappingStudentWise(TranId,StudentId,CreateBy) values(@TranId,@StudentId,@CreateBy)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                dal.CommitTransaction();

                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@ClassId", dataColl.First().ClassId);
                cmd.CommandText = "usp_ReInsertSubjectMappingStudentWise";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Subject Mapping StudentWise Done";
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
        public BE.Academic.Transaction.SubjectMappingStudentWise getStudentWiseSubjectMapping(int UserId,int AcademicYearId, int ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,int? BranchId=null)
        {
            BE.Academic.Transaction.SubjectMappingStudentWise dataColl = new BE.Academic.Transaction.SubjectMappingStudentWise();
            dataColl.StudentList = new List<BE.Academic.Transaction.OptionalSubjectStudentWise>();
            dataColl.OptionalSubjectList = new List<BE.Academic.Transaction.OptionalSubject>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetSubjectMappingStudentWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.OptionalSubject beData = new BE.Academic.Transaction.OptionalSubject();
                    beData.TranId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Code = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CodeTH = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.CodePR = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.NoOfOptionalSubject = reader.GetInt32(6);
                    dataColl.OptionalSubjectList.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.OptionalSubjectStudentWise student = new BE.Academic.Transaction.OptionalSubjectStudentWise();
                    student.ClassId = ClassId;
                    student.SectionId = SectionId;
                    student.SemesterId = SemesterId;
                    student.ClassYearId = ClassYearId;
                    student.BatchId = BatchId;
                    student.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) student.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) student.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) student.RegdNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) student.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) student.BoardRegNo = reader.GetString(5);
                    dataColl.StudentList.Add(student);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int sid = reader.GetInt32(0);
                    int tranId = reader.GetInt32(1);

                    var student = dataColl.StudentList.Find(p1 => p1.StudentId == sid);
                    if (student != null)
                    {
                        if (student.TranIdColl == null || student.TranIdColl.Count == 0)
                            student.TranIdColl = new List<int>();

                        student.TranIdColl.Add(tranId);
                    }
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
