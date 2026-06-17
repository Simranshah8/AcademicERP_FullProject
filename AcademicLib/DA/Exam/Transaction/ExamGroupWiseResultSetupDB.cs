using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamGroupWiseResultSetupDB
    {
        DataAccessLayer1 dal = null;
        public ExamGroupWiseResultSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            List<int> tmpSIColl = new List<int>();
            if (!string.IsNullOrEmpty(beData.SectionIdColl))
            {
                string[] sectionIdColl = beData.SectionIdColl.Split(',');
                foreach (string sStr in sectionIdColl)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);
                    cmd.Parameters.AddWithValue("@SectionId", sStr.Trim());
                    cmd.CommandText = "delete from tbl_ExamGroupWiseResultSetup where ClassId=@ClassId and SectionId=@SectionId and ExamTypeGroupId=@ExamTypeGroupId";
                    cmd.ExecuteNonQuery();
                    tmpSIColl.Add(int.Parse(sStr));
                }
            }
            else
            {
                tmpSIColl.Add(0);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);
                cmd.CommandText = "delete from tbl_ExamGroupWiseResultSetup where ClassId=@ClassId and ExamTypeGroupId=@ExamTypeGroupId";
                cmd.ExecuteNonQuery();
            }
            cmd.Parameters.Clear();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                foreach (var s in tmpSIColl)
                {
                    int? sid = null;
                    if (s > 0)
                        sid = s;
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@SectionId", sid);
                    cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);
                    cmd.Parameters.AddWithValue("@TotalFailSubject", beData.TotalFailSubject);
                    cmd.Parameters.AddWithValue("@IsSubjectWise", beData.IsSubjectWise);

                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    if (isModify)
                    {
                        cmd.CommandText = "usp_UpdateExamGroupWiseResultSetup";
                    }
                    else
                    {
                        cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_AddExamGroupWiseResultSetup";
                    }
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
                        SaveExamGroupWiseResultSetupDetails(beData.CUserId, resVal.RId, beData.ExamGroupWiseResultSetupDetailsColl);
                    }
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
        private void SaveExamGroupWiseResultSetupDetails(int UserId, int TranId, List<BE.Exam.Transaction.ExamGroupWiseResultSetupDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Exam.Transaction.ExamGroupWiseResultSetupDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@TH", beData.TH);
                cmd.Parameters.AddWithValue("@PR", beData.PR);
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamGroupWiseResultSetupDetails";
                cmd.ExecuteNonQuery();
                sno++;
            }

        }

        public AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup getExamGroupWiseResultSetupByClassId(int UserId, int ClassId, string SectionIdColl, int ExamTypeGroupId)
        {
            AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup beData = new BE.Exam.Transaction.ExamGroupWiseResultSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
            cmd.CommandText = "usp_GetExamGroupWiseResultSetupClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TotalFailSubject = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(2);
                }
                reader.NextResult();

                if (beData.TranId > 0)
                {
                    beData.ExamGroupWiseResultSetupDetailsColl = new List<BE.Exam.Transaction.ExamGroupWiseResultSetupDetails>();
                    while (reader.Read())
                    {
                        AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetupDetails det = new BE.Exam.Transaction.ExamGroupWiseResultSetupDetails();
                        det.SubjectId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) det.TH = Convert.ToBoolean(reader[1]);
                        if (!(reader[2] is DBNull)) det.PR = Convert.ToBoolean(reader[2]);
                        beData.ExamGroupWiseResultSetupDetailsColl.Add(det);
                    }
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
        public AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup getExamGroupWiseResultSetupById(int UserId, int EntityId, int ExamGroupWiseResultSetupId)
        {
            AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup beData = new AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamGroupWiseResultSetupId", ExamGroupWiseResultSetupId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamGroupWiseResultSetupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup();
                    //beData.ExamGroupWiseResultSetupId = reader.GetInt32(0);
                    //if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    //if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    //if (!(reader[3] is DBNull)) beData.ExamTypeGroupId = reader.GetInt32(3);
                    //if (!(reader[4] is DBNull)) beData.FullMarks = reader.GetInt32(4);
                    //if (!(reader[5] is DBNull)) beData.PassMarks = reader.GetInt32(5);
                    //if (!(reader[6] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(6);


                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.Exam.Transaction.ExamGroupWiseResultSetupDetails BookEntryDetails = new BE.Exam.Transaction.ExamGroupWiseResultSetupDetails();

                    //if (!(reader[0] is System.DBNull)) BookEntryDetails.ExamGroupWiseResultSetupId = reader.GetInt32(0);
                    //if (!(reader[1] is System.DBNull)) BookEntryDetails.SubjectId = reader.GetInt32(1);
                    //if (!(reader[2] is System.DBNull)) BookEntryDetails.CreditHourTH = reader.GetFloat(2);
                    //if (!(reader[3] is System.DBNull)) BookEntryDetails.CreditHourPR = reader.GetFloat(3);
                    //if (!(reader[4] is System.DBNull)) BookEntryDetails.FullMarksTH = reader.GetFloat(4);
                    //if (!(reader[5] is System.DBNull)) BookEntryDetails.FullMarksPR = reader.GetFloat(5);
                    //if (!(reader[6] is System.DBNull)) BookEntryDetails.PassMarksTH = reader.GetFloat(6);
                    //if (!(reader[7] is System.DBNull)) BookEntryDetails.PassMarksPR = reader.GetFloat(7);
                    //if (!(reader[8] is System.DBNull)) BookEntryDetails.IsInclude = reader.GetBoolean(8);

                    beData.ExamGroupWiseResultSetupDetailsColl.Add(BookEntryDetails);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelExamGroupWiseResultSetupById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

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


        public ResponeValues DeleteExamTypeGroupById(int UserId, int EntityId, int ClassId, int ExamTypeId, string SectionIdColl="")
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);      
            cmd.CommandText = "usp_DelExamGroupWiseResultSetupById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

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
