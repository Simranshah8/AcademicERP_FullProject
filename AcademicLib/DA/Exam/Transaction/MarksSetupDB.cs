using Dynamic.BusinessEntity.SMS;
using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class MarksSetupDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public MarksSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.MarksSetup beData, bool isModify)
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
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@SectionId", sStr.Trim());

                    if(beData.SemesterId.HasValue)
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    else
                        cmd.Parameters.AddWithValue("@SemesterId", DBNull.Value);

                    if (beData.ClassYearId.HasValue)
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    else
                        cmd.Parameters.AddWithValue("@ClassYearId", DBNull.Value);

                    if (beData.BatchId.HasValue)
                        cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                    else
                        cmd.Parameters.AddWithValue("@BatchId", DBNull.Value);

                     
                    cmd.CommandText = "delete MS from tbl_MarkSetup MS where ClassId=@ClassId and SectionId=@SectionId and ExamTypeId=@ExamTypeId and  isnull(MS.SemesterId,0)=isnull(@SemesterId,0) and isnull(MS.ClassYearId,0)=isnull(@ClassYearId,0) and isnull(MS.BatchId,0)=isnull(@BatchId,0) ";
                    cmd.ExecuteNonQuery();
                    tmpSIColl.Add(int.Parse(sStr));
                }
            }
            else
            {
                tmpSIColl.Add(0);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);

                if (beData.SemesterId.HasValue)
                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                else
                    cmd.Parameters.AddWithValue("@SemesterId", DBNull.Value);

                if (beData.ClassYearId.HasValue)
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                else
                    cmd.Parameters.AddWithValue("@ClassYearId", DBNull.Value);

                if (beData.BatchId.HasValue)
                    cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                else
                    cmd.Parameters.AddWithValue("@BatchId", DBNull.Value);

                cmd.CommandText = "delete MS from tbl_MarkSetup MS where ClassId=@ClassId and ExamTypeId=@ExamTypeId  and  isnull(MS.SemesterId,0)=isnull(@SemesterId,0) and isnull(MS.ClassYearId,0)=isnull(@ClassYearId,0) and isnull(MS.BatchId,0)=isnull(@BatchId,0) ";
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
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);
                    cmd.Parameters.AddWithValue("@PassMark", beData.PassMark);
                    cmd.Parameters.AddWithValue("@IsAutoSum", beData.IsAutoSum);

                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    if (isModify)
                    {
                        cmd.CommandText = "usp_UpdateMarksSetup";
                    }
                    else
                    {
                        cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_AddMarkSetup";
                    }
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);

                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[8].Value is DBNull))
                        resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                    if (!(cmd.Parameters[10].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                    if (!(cmd.Parameters[11].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                    if (resVal.RId > 0 && resVal.IsSuccess)
                    {
                        SaveMarkSetupDetails(beData.CUserId, resVal.RId, beData.MarksSetupDetailsColl);
                    }
                }

                if (!string.IsNullOrEmpty(beData.ToClassIdColl))
                {
                    if (string.IsNullOrEmpty(beData.ToSectionIdColl))
                        beData.ToSectionIdColl = "";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.Parameters.AddWithValue("@FromClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@ToClassIdColl", beData.ToClassIdColl);
                    cmd.Parameters.AddWithValue("@FromSectionIdColl", beData.FromSectionIdColl);
                    cmd.Parameters.AddWithValue("@ToSectionIdColl", beData.ToSectionIdColl);                    
                    cmd.CommandText = "usp_CopyMarkSetupClassWise";
                    cmd.ExecuteNonQuery();
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
        private void SaveMarkSetupDetails(int UserId, int TranId, List<BE.Exam.Transaction.MarksSetupDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Exam.Transaction.MarksSetupDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@CRTH", beData.CRTH);
                cmd.Parameters.AddWithValue("@CRPR", beData.CRPR);
                cmd.Parameters.AddWithValue("@FMTH", beData.FMTH);
                cmd.Parameters.AddWithValue("@FMPR", beData.FMPR);
                cmd.Parameters.AddWithValue("@PMTH", beData.PMTH);
                cmd.Parameters.AddWithValue("@PMPR", beData.PMPR);
                cmd.Parameters.AddWithValue("@IsInclude", beData.IsInclude);
                cmd.Parameters.AddWithValue("@OTH", beData.OTH);
                cmd.Parameters.AddWithValue("@OPR", beData.OPR);
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.Parameters.AddWithValue("@SubjectType", (beData.SubjectType == 0 ? 1 : beData.SubjectType));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddMarksSetupDetails";
                cmd.ExecuteNonQuery();
                sno++;
            }

        }

        public AcademicLib.BE.Exam.Transaction.MarksSetup getMarksSetupByClassId(int UserId,int ClassId,string SectionIdColl,int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            AcademicLib.BE.Exam.Transaction.MarksSetup beData = new BE.Exam.Transaction.MarksSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetMarkSetupClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.MarksSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FullMark = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.PassMark = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.IsAutoSum = reader.GetBoolean(3);                    
                }
                reader.NextResult();

                if (beData.TranId > 0)
                {
                    beData.MarksSetupDetailsColl = new List<BE.Exam.Transaction.MarksSetupDetails>();
                    while (reader.Read())
                    {
                        AcademicLib.BE.Exam.Transaction.MarksSetupDetails det = new BE.Exam.Transaction.MarksSetupDetails();
                        det.SubjectId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) det.CRTH = Convert.ToDouble(reader[1]);
                        if (!(reader[2] is DBNull)) det.CRPR = Convert.ToDouble(reader[2]);
                        if (!(reader[3] is DBNull)) det.FMTH = Convert.ToDouble(reader[3]);
                        if (!(reader[4] is DBNull)) det.FMPR = Convert.ToDouble(reader[4]);
                        if (!(reader[5] is DBNull)) det.PMTH = Convert.ToDouble(reader[5]);
                        if (!(reader[6] is DBNull)) det.PMPR = Convert.ToDouble(reader[6]);
                        if (!(reader[7] is DBNull)) det.IsInclude = Convert.ToBoolean(reader[7]);
                        if (!(reader[8] is DBNull)) det.SubjectType = Convert.ToInt32(reader[8]);
                        if (!(reader[9] is DBNull)) det.OTH = Convert.ToInt32(reader[9]);
                        if (!(reader[10] is DBNull)) det.OPR = Convert.ToInt32(reader[10]);
                        beData.MarksSetupDetailsColl.Add(det);
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
        public AcademicLib.BE.Exam.Transaction.MarksSetup getMarksSetupById(int UserId, int EntityId, int MarksSetupId)
        {
            AcademicLib.BE.Exam.Transaction.MarksSetup beData = new AcademicLib.BE.Exam.Transaction.MarksSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MarksSetupId", MarksSetupId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetMarksSetupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.MarksSetup();
                    //beData.MarksSetupId = reader.GetInt32(0);
                    //if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    //if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    //if (!(reader[3] is DBNull)) beData.ExamTypeId = reader.GetInt32(3);
                    //if (!(reader[4] is DBNull)) beData.FullMarks = reader.GetInt32(4);
                    //if (!(reader[5] is DBNull)) beData.PassMarks = reader.GetInt32(5);
                    //if (!(reader[6] is DBNull)) beData.IsAutoSum = reader.GetBoolean(6);


                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.Exam.Transaction.MarksSetupDetails BookEntryDetails = new BE.Exam.Transaction.MarksSetupDetails();

                    //if (!(reader[0] is System.DBNull)) BookEntryDetails.MarksSetupId = reader.GetInt32(0);
                    //if (!(reader[1] is System.DBNull)) BookEntryDetails.SubjectId = reader.GetInt32(1);
                    //if (!(reader[2] is System.DBNull)) BookEntryDetails.CreditHourTH = reader.GetFloat(2);
                    //if (!(reader[3] is System.DBNull)) BookEntryDetails.CreditHourPR = reader.GetFloat(3);
                    //if (!(reader[4] is System.DBNull)) BookEntryDetails.FullMarksTH = reader.GetFloat(4);
                    //if (!(reader[5] is System.DBNull)) BookEntryDetails.FullMarksPR = reader.GetFloat(5);
                    //if (!(reader[6] is System.DBNull)) BookEntryDetails.PassMarksTH = reader.GetFloat(6);
                    //if (!(reader[7] is System.DBNull)) BookEntryDetails.PassMarksPR = reader.GetFloat(7);
                    //if (!(reader[8] is System.DBNull)) BookEntryDetails.IsInclude = reader.GetBoolean(8);

                    beData.MarksSetupDetailsColl.Add(BookEntryDetails);
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
            cmd.CommandText = "usp_DelMarksSetupById";
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

        public AcademicLib.RE.Exam.MarkSetupStatusCollections GetMarkSetupStatus(int UserId, int ExamTypeId,int? BranchId=null)
        {
            AcademicLib.RE.Exam.MarkSetupStatusCollections dataColl = new RE.Exam.MarkSetupStatusCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetMarkSetupStatusList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.MarkSetupStatus beData = new RE.Exam.MarkSetupStatus();
                    if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.IsPending = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.CreateDateTime_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.CreateDateTime_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.UserName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FullMark = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.PassMark = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.TotalSubject = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Batch = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Semester = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ClassYear = reader.GetString(11);
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

        public ResponeValues Transfor(int UserId, int FromExamTypeId, int ToExamTypeId,int? BranchId=null)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromExamTypeId", FromExamTypeId);
            cmd.Parameters.AddWithValue("@ToExamTypeId", ToExamTypeId);
            cmd.CommandText = "usp_TransforMarkSetup";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
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

        public ResponeValues ImportMarkSetup(int UserId, List<BE.Exam.Transaction.ImportMarkSetup> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

            if (dataColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("AcademicYear", typeof(string));
                table.Columns.Add("ExamName", typeof(string));
                table.Columns.Add("ClassName", typeof(string));
                table.Columns.Add("SectionName", typeof(string));
                table.Columns.Add("Batch", typeof(string));
                table.Columns.Add("Semester", typeof(string));
                table.Columns.Add("ClassYear", typeof(string));
                table.Columns.Add("FullMark", typeof(double));
                table.Columns.Add("PassMark", typeof(double));
                table.Columns.Add("Subject", typeof(string));
                table.Columns.Add("CRTH", typeof(double));
                table.Columns.Add("CRPR", typeof(double));
                table.Columns.Add("CR", typeof(double));
                table.Columns.Add("FMTH", typeof(double));
                table.Columns.Add("FMPR", typeof(double));
                table.Columns.Add("FM", typeof(double));
                table.Columns.Add("PMTH", typeof(double));
                table.Columns.Add("PMPR", typeof(double));
                table.Columns.Add("PM", typeof(double));
                table.Columns.Add("OTH", typeof(double));
                table.Columns.Add("OPR", typeof(double));
                table.Columns.Add("IsInclude", typeof(bool));
                table.Columns.Add("SubjectType", typeof(string));
                foreach (var v in dataColl)
                {
                    var row = table.NewRow();
                    row["AcademicYear"] = IsDBNull(v.AcademicYear);
                    row["ExamName"] = IsDBNull(v.ExamName);
                    row["ClassName"] = IsDBNull(v.ClassName);   
                    row["SectionName"] = IsDBNull(v.SectionName);   
                    row["Batch"] = IsDBNull(v.Batch);   
                    row["Semester"] = IsDBNull(v.Semester); 
                    row["ClassYear"] = IsDBNull(v.ClassYear);   
                    row["FullMark"] = ZeroTo(v.FullMark); 
                    row["PassMark"] = ZeroTo(v.PassMark);   
                    row["Subject"] = IsDBNull(v.Subject);
                    row["CRTH"] = ZeroTo(v.CRTH);
                    row["CRPR"] = ZeroTo(v.CRPR);
                    row["CR"] = ZeroTo(v.CR);   
                    row["FMTH"] = ZeroTo(v.FMTH);
                    row["FMPR"] = ZeroTo(v.FMPR);
                    row["FM"] = ZeroTo(v.FM);
                    row["PMTH"] = ZeroTo(v.PMTH);
                    row["PMPR"] = ZeroTo(v.PMPR);
                    row["PM"] = ZeroTo(v.PM);
                    row["OTH"] = ZeroTo(v.OTH);
                    row["OPR"] = ZeroTo(v.OPR);
                    row["IsInclude"] = v.IsInclude;
                    row["SubjectType"] = IsDBNull(v.SubjectType);
                    table.Rows.Add(row);
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@DataColl", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }

            cmd.CommandText = "usp_ImportMarkSetup";
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (resVal.IsSuccess)
                    dal.CommitTransaction();
                else
                    dal.RollbackTransaction();
                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Bill Generate Success";
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
    }
}
