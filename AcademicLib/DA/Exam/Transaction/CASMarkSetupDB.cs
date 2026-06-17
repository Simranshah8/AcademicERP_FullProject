using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class CASMarkSetupDB
    {
        DataAccessLayer1 dal = null;
        public CASMarkSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.CASMarksSetup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();                       
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);                
                cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                cmd.CommandText = "[usp_AddCASMarkSetup]";
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;                
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveMarkSetupDetails(beData.CUserId, resVal.RId, beData.MarksSetupDetailsColl);
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
        private void SaveMarkSetupDetails(int UserId, int TranId, List<BE.Exam.Transaction.CASMarksSetupDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Exam.Transaction.CASMarksSetupDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@CASTypeId", beData.CASTypeId);
                cmd.Parameters.AddWithValue("@Mark", beData.Mark);
                cmd.Parameters.AddWithValue("@Under", beData.Under);
                cmd.Parameters.AddWithValue("@Scheme", beData.Scheme);
                cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
                cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);                
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.Parameters.AddWithValue("@AttendanceFrom", beData.AttendanceFrom);
                cmd.Parameters.AddWithValue("@Formula", beData.Formula);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddCASMarkSetupDetails";
                cmd.ExecuteNonQuery();
                sno++;
            }

        }

        public AcademicLib.BE.Exam.Transaction.ExamClassSubjectCollections getExamClassSubjectList(int UserId,int? ClassId,int? AcademicYearId)
        {
            AcademicLib.BE.Exam.Transaction.ExamClassSubjectCollections dataColl = new BE.Exam.Transaction.ExamClassSubjectCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetClassSubjectListForCASMarkSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();                
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamClassSubject det = new BE.Exam.Transaction.ExamClassSubject();
                    if (!(reader[0] is DBNull)) det.ExamTypeId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) det.ClassId = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) det.SubjectId = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) det.ExamType = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) det.ClassName = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) det.SubjectName = Convert.ToString(reader[5]);                    
                    dataColl.Add(det);
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

        public AcademicLib.BE.Exam.Transaction.CASMarksSetup getMarksSetupByClassId(int UserId, int ClassId, int? SectionId,int SubjectId, int ExamTypeId)
        {
            AcademicLib.BE.Exam.Transaction.CASMarksSetup beData = new BE.Exam.Transaction.CASMarksSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetCASMarkSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                beData.MarksSetupDetailsColl = new List<BE.Exam.Transaction.CASMarksSetupDetails>();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.CASMarksSetupDetails det = new BE.Exam.Transaction.CASMarksSetupDetails();                    
                    if (!(reader[0] is DBNull)) det.FullMark = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) det.CASTypeId = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) det.Mark = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) det.Under = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) det.Scheme = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) det.DateFrom = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) det.DateTo = Convert.ToDateTime(reader[6]);
                    if (!(reader[7] is DBNull)) det.ExamTypeId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) det.AttendanceFrom = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) det.Formula = Convert.ToString(reader[9]);
                    beData.MarksSetupDetailsColl.Add(det);
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
   
        public AcademicLib.RE.Exam.CASMarkSetupStatusCollections GetMarkSetupStatus(int UserId, int ExamTypeId,int ClassId)
        {
            AcademicLib.RE.Exam.CASMarkSetupStatusCollections dataColl = new RE.Exam.CASMarkSetupStatusCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetCASMarkSetupStatusList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                int sno = 1;
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.CASMarkSetupStatus beData = new RE.Exam.CASMarkSetupStatus();
                    beData.SNo = sno;
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SubjectCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.FullMark = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.SubmitDateTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.SubmitMiti = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.UserName = Convert.ToString(reader[6]);
                     
                    dataColl.Add(beData);
                    sno++;
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
