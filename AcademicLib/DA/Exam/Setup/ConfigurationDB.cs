using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Exam.Setup
{
    internal class ConfigurationDB
    {
        DataAccessLayer1 dal = null;
        public ConfigurationDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Exam.Setup.Configuration beData,int BranchId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsShowRankForFailStudent", beData.IsShowRankForFailStudent);
            cmd.Parameters.AddWithValue("@IsShowDivisionForFailStudent", beData.IsShowDivisionForFailStudent);
            cmd.Parameters.AddWithValue("@IsShowGradeForFailStudent", beData.IsShowGradeForFailStudent);
            cmd.Parameters.AddWithValue("@IsAllowGraceMarkStudentwise", beData.IsAllowGraceMarkStudentwise);
            cmd.Parameters.AddWithValue("@IsAllowGraceMarkSubjectwise", beData.IsAllowGraceMarkSubjectwise);
            cmd.Parameters.AddWithValue("@IsAllowMarkEntry", beData.IsAllowMarkEntry);
            cmd.Parameters.AddWithValue("@IsShowForFailStudent", beData.IsShowForFailStudent);
            cmd.Parameters.AddWithValue("@IsMatchSubjectStudentWise", beData.IsMatchSubjectStudentWise);
            cmd.Parameters.AddWithValue("@ResultForPassStudent", beData.ResultForPassStudent);
            cmd.Parameters.AddWithValue("@ResultForFailStudent", beData.ResultForFailStudent);
            cmd.Parameters.AddWithValue("@AbsentSymbol", beData.AbsentSymbol);
            cmd.Parameters.AddWithValue("@ResultForPassWithGraceMark", beData.ResultForPassWithGraceMark);
            cmd.Parameters.AddWithValue("@NoOfDecimalPlace", beData.NoOfDecimalPlace);
            cmd.Parameters.AddWithValue("@StudentRankAs", beData.StudentRankAs);
            cmd.Parameters.AddWithValue("@StudentCommentAs", beData.StudentCommentAs);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.CommandText = "usp_AddExamConfiguration";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@WithHeldSymbol", beData.WithHeldSymbol);
            cmd.Parameters.AddWithValue("@IsAllowGraceMarkClassWise", beData.IsAllowGraceMarkClassWise);
            cmd.Parameters.AddWithValue("@PassFailCondition1", beData.PassFailCondition1);
            cmd.Parameters.AddWithValue("@PassFailResult1", beData.PassFailResult1);
            cmd.Parameters.AddWithValue("@GPAAs", beData.GPAAs);
            cmd.Parameters.AddWithValue("@ShowStartForFailTH", beData.ShowStartForFailTH);
            cmd.Parameters.AddWithValue("@ShowStartForFailPR", beData.ShowStartForFailPR);
            cmd.Parameters.AddWithValue("@ShowStartForFail", beData.ShowStartForFail);
            cmd.Parameters.AddWithValue("@AllowExtraSubjectInSubjectMapping", beData.AllowExtraSubjectInSubjectMapping);
            cmd.Parameters.AddWithValue("@ForClassWiseComment", beData.ForClassWiseComment);
            cmd.Parameters.AddWithValue("@AllowSubjectWiseComment", beData.AllowSubjectWiseComment);
            cmd.Parameters.AddWithValue("@FailDivision", beData.FailDivision);
            cmd.Parameters.AddWithValue("@FailDivisionAs", beData.FailDivisionAs);
            cmd.Parameters.AddWithValue("@ForClassWiseRank", beData.ForClassWiseRank);
            cmd.Parameters.AddWithValue("@ShowStartForClassWise", beData.ShowStartForClassWise);
            cmd.Parameters.AddWithValue("@PassFailConditionClassWise", beData.PassFailConditionClassWise);
            cmd.Parameters.AddWithValue("@ShowResultSummaryForStudent", beData.ShowResultSummaryForStudent);
            cmd.Parameters.AddWithValue("@ShowResultSummaryForClassTeacher", beData.ShowResultSummaryForClassTeacher);
            cmd.Parameters.AddWithValue("@ShowResultSummaryForAdmin", beData.ShowResultSummaryForAdmin);
            cmd.Parameters.AddWithValue("@ClassWiseGPA", beData.ClassWiseGPA);
            cmd.Parameters.AddWithValue("@PassFailCondition2", beData.PassFailCondition2);
            cmd.Parameters.AddWithValue("@PassFailResult2", beData.PassFailResult2);
            cmd.Parameters.AddWithValue("@ActiveMajorMinor", beData.ActiveMajorMinor);
            cmd.Parameters.AddWithValue("@GPAs", beData.GPAs);
            cmd.Parameters.AddWithValue("@ClassWiseGP", beData.ClassWiseGP);

            cmd.Parameters.AddWithValue("@SymbolForReExam", beData.SymbolForReExam);
            cmd.Parameters.AddWithValue("@ResultForPassedReExam", beData.ResultForPassedReExam);
            cmd.Parameters.AddWithValue("@ResultForFailedReExam", beData.ResultForFailedReExam);

            cmd.Parameters.AddWithValue("@GradeId", beData.GradeId);
            cmd.Parameters.AddWithValue("@NoOfGrade", beData.NoOfGrade);
            cmd.Parameters.AddWithValue("@CalculateECASubject", beData.CalculateECASubject);
            cmd.Parameters.AddWithValue("@SubjectGradeId", beData.SubjectGradeId);
            cmd.Parameters.AddWithValue("@AllowAllSubjectCT", beData.AllowAllSubjectCT);
            //Added on 19 Magh 2082
            cmd.Parameters.AddWithValue("@MarksheetTeacherBeforeResult", beData.MarksheetTeacherBeforeResult);
            cmd.Parameters.AddWithValue("@MarksheetClassTeacherBeforeResult", beData.MarksheetClassTeacherBeforeResult);
            cmd.Parameters.AddWithValue("@MarksheetAdminBeforeResult", beData.MarksheetAdminBeforeResult);
            cmd.Parameters.AddWithValue("@TabulationTeacherBeforeResult", beData.TabulationTeacherBeforeResult);
            cmd.Parameters.AddWithValue("@TabulationClassTeacherBeforeResult", beData.TabulationClassTeacherBeforeResult);
            cmd.Parameters.AddWithValue("@TabulationAdminBeforeResult", beData.TabulationAdminBeforeResult);
            cmd.Parameters.AddWithValue("@IncludeReExamMarks", beData.IncludeReExamMarks);
            //Ends
            //Added on 11 falgun
            cmd.Parameters.AddWithValue("@ClassWiseDivGrade", beData.ClassWiseDivGrade);
            cmd.Parameters.AddWithValue("@ExamTypeWiseGrade", beData.ExamTypeWiseGrade);
            //Ends
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[18].Value);

                if (!(cmd.Parameters[19].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[19].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess)
                {
                    SaveClassWiseRank(BranchId,beData.ClassWiseRankList);
                    SaveClassWiseComment(BranchId, beData.ClassWiseCommentList);
                    SaveClassWiseDues(BranchId, beData.ClassWiseDuesList);
                    SaveClassStarSymbol(BranchId, beData.ClassWiseStarSymbolList);
                    SaveClassGPA(BranchId, beData.ClassWiseGPAList);
                    SaveClassGP(BranchId, beData.ClassWiseGPList);
                    SaveExamConfig(BranchId, beData.ExamConfigForAppList);
                    SaveConditionForFailPass(BranchId, beData.ConditionForFailPassList); 
                }
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
        public void SaveClassWiseRank(int BranchId, List<BE.Exam.Setup.ClassWiseRankAs> dataColl)
        {            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach(var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@RankAs", beData.RankAs);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_ClassWiseRankAs(BranchId,ClassId,RankAs) values(@BranchId,@ClassId,@RankAs)";
                cmd.ExecuteNonQuery();
            }            
        }
        public void SaveClassWiseComment(int BranchId, List<BE.Exam.Setup.ClassWiseCommentAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@CommentAs", beData.CommentAs);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_ClassWiseCommentAs(BranchId,ClassId,CommentAs) values(@BranchId,@ClassId,@CommentAs)";
                cmd.ExecuteNonQuery();
            }
        }
        public void SaveClassWiseDues(int BranchId, List<BE.Exam.Setup.ClassWiseDuesAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@UptoMonthId", beData.UptoMonthId);
                cmd.Parameters.AddWithValue("@DuesAmt", beData.DuesAmt);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_ClassWiseDuesAs(BranchId,ClassId,UptoMonthId,DuesAmt) values(@BranchId,@ClassId,@UptoMonthId,@DuesAmt)";
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveConditionForFailPass(int BranchId, List<BE.Exam.Setup.ConditionForFailPassAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                if(beData.Condition1>0 && beData.Condition2>0 && beData.Result1>0 && beData.Result2 > 0)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@Condition1", beData.Condition1);
                    cmd.Parameters.AddWithValue("@Result1", beData.Result1);
                    cmd.Parameters.AddWithValue("@Condition2", beData.Condition2);
                    cmd.Parameters.AddWithValue("@Result2", beData.Result2);
                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                    cmd.CommandText = "insert into tbl_ConditionForFailPass(BranchId,ClassId,Condition1,Result1,Condition2,Result2) values(@BranchId,@ClassId,@Condition1,@Result1,@Condition2,@Result2)";
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
        public void SaveClassStarSymbol(int BranchId, List<BE.Exam.Setup.ClassWiseStarSymbolAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@ShowStartForFail", beData.ShowStartForFail);
                cmd.Parameters.AddWithValue("@ShowStartForFailTH", beData.ShowStartForFailTH);
                cmd.Parameters.AddWithValue("@ShowStartForFailPR", beData.ShowStartForFailPR);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_ClassWiseStarSymbol(BranchId,ClassId,ShowStartForFail,ShowStartForFailTH,ShowStartForFailPR) values(@BranchId,@ClassId,@ShowStartForFail,@ShowStartForFailTH,@ShowStartForFailPR)";
                cmd.ExecuteNonQuery();
            }
        }
        public void SaveClassGPA(int BranchId, List<BE.Exam.Setup.ClassWiseGPAAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@GPAAs", beData.GPAAs);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_GPAFormula(BranchId,ClassId,GPAAs) values(@BranchId,@ClassId,@GPAAs)";
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveClassGP(int BranchId, List<BE.Exam.Setup.ClassWiseGPAs> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@GPAs", beData.GPAs);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.CommandText = "insert into tbl_GPFormula(BranchId,ClassId,GPAs) values(@BranchId,@ClassId,@GPAs)";
                cmd.ExecuteNonQuery();
            }
        }
        public void SaveExamConfig(int BranchId, List<BE.Exam.Setup.ExamConfigForApp> dataColl)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@MinDues", beData.MinDues);
                cmd.Parameters.AddWithValue("@MarkType", beData.MarkType);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                if (beData.GeneralMarkSheet_RptTranId.HasValue)
                    cmd.Parameters.AddWithValue("@GeneralMarkSheet_RptTranId", beData.GeneralMarkSheet_RptTranId);
                else
                    cmd.Parameters.AddWithValue("@GeneralMarkSheet_RptTranId", DBNull.Value);

                if (beData.UptoMonthId.HasValue)
                    cmd.Parameters.AddWithValue("@UptoMonthId", beData.UptoMonthId.Value);
                else
                    cmd.Parameters.AddWithValue("@UptoMonthId", DBNull.Value);


                if (beData.GroupMarkSheet_RptTranId.HasValue)
                    cmd.Parameters.AddWithValue("@GroupMarkSheet_RptTranId", beData.GroupMarkSheet_RptTranId);
                else
                    cmd.Parameters.AddWithValue("@GroupMarkSheet_RptTranId", DBNull.Value);

                if (beData.AdmitCard_RptTranId.HasValue)
                    cmd.Parameters.AddWithValue("@AdmitCard_RptTranId", beData.AdmitCard_RptTranId);
                else
                    cmd.Parameters.AddWithValue("@AdmitCard_RptTranId", DBNull.Value);

                cmd.CommandText = "insert into tbl_ExamConfigForApp(AdmitCard_RptTranId,UptoMonthId,BranchId,ClassId,MinDues,MarkType,GeneralMarkSheet_RptTranId,GroupMarkSheet_RptTranId) values(@AdmitCard_RptTranId,@UptoMonthId,@BranchId,@ClassId,@MinDues,@MarkType,@GeneralMarkSheet_RptTranId,@GroupMarkSheet_RptTranId)";
                cmd.ExecuteNonQuery();
            }
        }
        public BE.Exam.Setup.Configuration getConfiguration(int UserId, int EntityId)
        {
            BE.Exam.Setup.Configuration beData = new BE.Exam.Setup.Configuration();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamConfiguration";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Setup.Configuration();
                    if (!(reader[0] is DBNull)) beData.IsShowRankForFailStudent = reader.GetBoolean(0);
                    if (!(reader[1] is DBNull)) beData.IsShowDivisionForFailStudent = reader.GetBoolean(1);
                    if (!(reader[2] is DBNull)) beData.IsShowGradeForFailStudent = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.IsAllowGraceMarkStudentwise = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.IsAllowGraceMarkSubjectwise = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.IsAllowMarkEntry = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsShowForFailStudent = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsMatchSubjectStudentWise = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.ResultForPassStudent = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ResultForFailStudent = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AbsentSymbol = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ResultForPassWithGraceMark = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.NoOfDecimalPlace = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.StudentRankAs = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.StudentCommentAs = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.WithHeldSymbol = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.IsAllowGraceMarkClassWise = reader.GetBoolean(16);
                    if (!(reader[17] is DBNull)) beData.GPAAs = reader.GetInt32(17);                    
                    if (!(reader[18] is DBNull)) beData.PassFailCondition1 = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.PassFailResult1 = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.ShowStartForFailTH = reader.GetBoolean(20);
                    if (!(reader[21] is DBNull)) beData.ShowStartForFailPR = reader.GetBoolean(21);
                    if (!(reader[22] is DBNull)) beData.ShowStartForFail = reader.GetBoolean(22);
                    if (!(reader[23] is DBNull)) beData.AllowExtraSubjectInSubjectMapping = reader.GetBoolean(23);
                    if (!(reader[24] is DBNull)) beData.ForClassWiseComment = reader.GetBoolean(24);
                    if (!(reader[25] is DBNull)) beData.AllowSubjectWiseComment = reader.GetBoolean(25);
                    if (!(reader[26] is DBNull)) beData.FailDivision = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.FailDivisionAs = reader.GetInt32(27);
                    if (!(reader[28] is DBNull)) beData.ForClassWiseRank = reader.GetBoolean(28);
                    if (!(reader[29] is DBNull)) beData.PassFailConditionClassWise = reader.GetBoolean(29);                   
                    if (!(reader[30] is DBNull)) beData.ShowResultSummaryForStudent = reader.GetBoolean(30);
                    if (!(reader[31] is DBNull)) beData.ShowResultSummaryForClassTeacher = reader.GetBoolean(31);
                    if (!(reader[32] is DBNull)) beData.ShowResultSummaryForAdmin = reader.GetBoolean(32);
                    if (!(reader[33] is DBNull)) beData.ClassWiseGPA = reader.GetBoolean(33);
                    if (!(reader[34] is DBNull)) beData.ShowStartForClassWise = reader.GetBoolean(34);
                    if (!(reader[35] is DBNull)) beData.PassFailCondition2 = reader.GetInt32(35);
                    if (!(reader[36] is DBNull)) beData.PassFailResult2 = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.ActiveMajorMinor = reader.GetBoolean(37);
                    if (!(reader[38] is DBNull)) beData.ClassWiseGP = reader.GetBoolean(38);
                    if (!(reader[39] is DBNull)) beData.GPAs = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.SymbolForReExam = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.ResultForPassedReExam = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.ResultForFailedReExam = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.GradeId = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.NoOfGrade = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.CalculateECASubject = reader.GetBoolean(45);
                    if (!(reader[46] is DBNull)) beData.SubjectGradeId = reader.GetInt32(46);
                    if (!(reader[47] is DBNull)) beData.AllowAllSubjectCT = Convert.ToBoolean(reader[47]);
                    //Added on Magh 19
                    if (!(reader[48] is DBNull)) beData.MarksheetTeacherBeforeResult = Convert.ToBoolean(reader[48]);
                    if (!(reader[49] is DBNull)) beData.MarksheetClassTeacherBeforeResult = Convert.ToBoolean(reader[49]);
                    if (!(reader[50] is DBNull)) beData.MarksheetAdminBeforeResult = Convert.ToBoolean(reader[50]);
                    if (!(reader[51] is DBNull)) beData.TabulationTeacherBeforeResult = Convert.ToBoolean(reader[51]);
                    if (!(reader[52] is DBNull)) beData.TabulationClassTeacherBeforeResult = Convert.ToBoolean(reader[52]);
                    if (!(reader[53] is DBNull)) beData.TabulationAdminBeforeResult = Convert.ToBoolean(reader[53]);
                    if (!(reader[54] is DBNull)) beData.IncludeReExamMarks = Convert.ToBoolean(reader[54]);
                    //Ends
                    //Added on 11 falgun
                    if (!(reader[55] is DBNull)) beData.ClassWiseDivGrade = Convert.ToBoolean(reader[55]);
                    if (!(reader[56] is DBNull)) beData.ExamTypeWiseGrade = Convert.ToBoolean(reader[56]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseRankAs det = new BE.Exam.Setup.ClassWiseRankAs();
                    det.ClassId = reader.GetInt32(0);
                    det.RankAs = reader.GetInt32(1);
                    beData.ClassWiseRankList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseCommentAs det = new BE.Exam.Setup.ClassWiseCommentAs();
                    det.ClassId = reader.GetInt32(0);
                    det.CommentAs = reader.GetInt32(1);
                    beData.ClassWiseCommentList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseDuesAs det = new BE.Exam.Setup.ClassWiseDuesAs();
                    det.ClassId = reader.GetInt32(0);
                    det.UptoMonthId = reader.GetInt32(1);
                    det.DuesAmt = Convert.ToDouble(reader[2]);
                    beData.ClassWiseDuesList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ConditionForFailPassAs det = new BE.Exam.Setup.ConditionForFailPassAs();
                    det.ClassId = reader.GetInt32(0);
                    det.Condition1 = reader.GetInt32(1);
                    det.Result1 = Convert.ToInt32(reader[2]);
                    det.Condition2 = reader.GetInt32(3);
                    det.Result2 = Convert.ToInt32(reader[4]);
                    beData.ConditionForFailPassList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseStarSymbolAs det = new BE.Exam.Setup.ClassWiseStarSymbolAs();
                    det.ClassId = reader.GetInt32(0);
                    det.ShowStartForFailTH = reader.GetBoolean(1);
                    det.ShowStartForFailPR = Convert.ToBoolean(reader[2]);
                    det.ShowStartForFail = Convert.ToBoolean(reader[3]);
                    beData.ClassWiseStarSymbolList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ExamConfigForApp det = new BE.Exam.Setup.ExamConfigForApp();
                    det.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.MinDues = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) det.MarkType = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.GeneralMarkSheet_RptTranId = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) det.GroupMarkSheet_RptTranId = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) det.UptoMonthId = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) det.AdmitCard_RptTranId = Convert.ToInt32(reader[6]);
                    beData.ExamConfigForAppList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.Global.ReportTempletes det = new Dynamic.BusinessEntity.Global.ReportTempletes();
                    det.RptTranId = reader.GetInt32(0);
                    det.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ReportName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Path = reader.GetString(4);
                    beData.ReportTemplateList.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseGPAAs det = new BE.Exam.Setup.ClassWiseGPAAs();
                    det.ClassId = reader.GetInt32(0);
                    det.GPAAs = reader.GetInt32(1);                
                    beData.ClassWiseGPAList.Add(det);
                }
                reader.NextResult();
                beData.ClassWiseGPList = new List<BE.Exam.Setup.ClassWiseGPAs>();
                while (reader.Read())
                {
                    BE.Exam.Setup.ClassWiseGPAs det = new BE.Exam.Setup.ClassWiseGPAs();
                    det.ClassId = reader.GetInt32(0);
                    det.GPAs = reader.GetInt32(1);
                    beData.ClassWiseGPList.Add(det);
                }
                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }

        public ResponeValues SaveSeatPlanConfiguration(int UserId,List<BE.Exam.Setup.SeatPlanConfiguraion> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
             
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelSeatPlanConfiguration";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if(beData.ClassId>0 && beData.ExamTypeId > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                        cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                        cmd.CommandText = "usp_AddSeatPlanConfiguration";
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Seatplan Configuration Updated";

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
        public List<BE.Exam.Setup.SeatPlanConfiguraion> getSeatPlanConfiguration(int UserId)
        {
            List<BE.Exam.Setup.SeatPlanConfiguraion> dataColl = new List<BE.Exam.Setup.SeatPlanConfiguraion>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.CommandText = "usp_GetSeatPlanConfiguration";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Setup.SeatPlanConfiguraion beData = new BE.Exam.Setup.SeatPlanConfiguraion();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    dataColl.Add(beData);
                }
               
                reader.Close();
                return dataColl;

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            } 
        }
        public BE.Exam.Setup.ClassWiseMinDues getClasswiseMinDues(int UserId, int ClassId)
        {
            BE.Exam.Setup.ClassWiseMinDues beData = new BE.Exam.Setup.ClassWiseMinDues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetClasswiseMinDues";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Setup.ClassWiseMinDues();
                    if (!(reader[0] is DBNull)) beData.ClassId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.MinDues = Convert.ToDouble(reader[1]);

                }
                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }

    }
}
