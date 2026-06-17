using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.HomeWork
{
    internal class HomeWorkDB
    {
        DataAccessLayer1 dal = null;
        public HomeWorkDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.HomeWork.HomeWork beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", beData.TeacherId);
            cmd.Parameters.AddWithValue("@HomeworkTypeId", beData.HomeworkTypeId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);            
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@Lesson", beData.Lesson);
            cmd.Parameters.AddWithValue("@Topic", beData.Topic);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@DeadlineDate", beData.DeadlineDate);
            cmd.Parameters.AddWithValue("@DeadlineTime", beData.DeadlineTime);
            cmd.Parameters.AddWithValue("@DeadlineforRedo", beData.DeadlineforRedo);
            cmd.Parameters.AddWithValue("@DeadlineforRedoTime", beData.DeadlineforRedoTime);            
            cmd.Parameters.AddWithValue("@IsAllowLateSibmission", beData.IsAllowLateSibmission);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@HomeWorkId", beData.HomeWorkId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHomeWork";
            }
            else
            {
                cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddHomeWork";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@SubmissionsRequired", beData.SubmissionsRequired);
            cmd.Parameters.AddWithValue("@SectionIdColl", beData.SectionIdColl);
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 4000);
            cmd.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;
            //Added By Suresh on 29 Magh
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[21].Value);

                if (!(cmd.Parameters[22].Value is DBNull) && resVal.IsSuccess)
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[22].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";


                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
                    //SaveSection(beData.CUserId, resVal.RId, beData.SectionIdColl);
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
        public ResponeValues UpdateDeadline(AcademicLib.BE.HomeWork.HomeWork beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeadlineDate", beData.DeadlineDate);
            cmd.Parameters.AddWithValue("@DeadlineTime", beData.DeadlineTime);
            cmd.Parameters.AddWithValue("@DeadlineforRedo", beData.DeadlineforRedo);
            cmd.Parameters.AddWithValue("@DeadlineforRedoTime", beData.DeadlineforRedoTime);
            cmd.Parameters.AddWithValue("@HomeWorkId", beData.HomeWorkId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);                        
            cmd.CommandText = "usp_UpdateHomeWorkDeadline";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

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
        private void SaveSection(int UserId, int HomeWorkId, int[] beDataColl)
        {
            if (beDataColl == null || beDataColl.Length == 0 || HomeWorkId == 0)
                return;

            foreach (int sectionId in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddHomeWorkSection";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveDocument(int UserId, int HomeWorkId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || HomeWorkId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddHomeWorkAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public ResponeValues SubmitHomeWork(AcademicLib.API.Student.HomeWorkSubmit beData,ref string msg)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HomeWorkId", beData.HomeWorkId);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.CommandText = "usp_HomeWorkSubmit";
            cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 2000);
            cmd.Parameters.Add("@IsRe", System.Data.SqlDbType.Bit);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull) && resVal.IsSuccess)
                    msg = Convert.ToString(cmd.Parameters[8].Value);

                bool isRe = false;
                if (!(cmd.Parameters[9].Value is DBNull))
                    isRe = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";


                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveStudentDocument(beData.UserId, resVal.RId,isRe, beData.AttachmentColl);                    
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
        private void SaveStudentDocument(int UserId, int TranId,bool isRe, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.Parameters.AddWithValue("@IsRe", isRe);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddHomeWorkSubmitAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public ResponeValues CheckClassWiseHomeWork(int UserId,AcademicLib.API.Teacher.HomeWorkCheckedCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@HomeWorkId", beData.HomeWorkId);
                    cmd.Parameters.AddWithValue("@Notes", beData.Notes);
                    cmd.Parameters.AddWithValue("@Status", beData.Status);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.CommandText = "usp_HomeWorkCheck";
                    cmd.Parameters.Add("@SUserId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    if (!(cmd.Parameters[6].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                    if (!(cmd.Parameters[7].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                    if (resVal.IsSuccess)
                    {
                        if (!(cmd.Parameters[5].Value is DBNull))
                            beData.SUserId = Convert.ToInt32(cmd.Parameters[5].Value);
                    }

                    if (!resVal.IsSuccess)
                        return resVal;
                }

                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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

        public ResponeValues CheckHomeWork(AcademicLib.API.Teacher.HomeWorkChecked beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@HomeWorkId", beData.HomeWorkId);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@Status", beData.Status);
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.CommandText = "usp_HomeWorkCheck";
            cmd.Parameters.Add("@SUserId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                    resVal.ResponseId = resVal.RId.ToString();
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
        public AcademicLib.RE.HomeWork.HomeWorkCollections getAllHomeWork(int UserId, int EntityId,DateTime? dateFrom,DateTime? dateTo,bool isStudent=false,int? studentId=null,int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? EmployeeId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.RE.HomeWork.HomeWorkCollections dataColl = new RE.HomeWork.HomeWorkCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            //cmd.Parameters.AddWithValue("@IsStudent", isStudent);
            cmd.CommandText = "usp_GetAllHomeWork";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.HomeWork.HomeWork beData = new RE.HomeWork.HomeWork();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HomeWorkId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.HomeWorkType = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Topic = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Lession = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Description = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.TeacherName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TeacherAddress = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.TeacherContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.NoOfAttachment = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Attachments = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.AsignDateTime_AD = reader.GetDateTime(15);                    
                    if (!(reader[16] is DBNull)) beData.AsignDateTime_BS = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.DeadlineDate_AD = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.DeadlineDate_BS = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.DeadlineTime  = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.TotalStudent = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.NoOfSubmit = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.TotalChecked = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.HomeWorkStatus = reader.GetString(23);
                    try
                    {
                        if (!(reader[24] is DBNull)) beData.SubmitDateTime_AD = reader.GetDateTime(24);
                        if (!(reader[25] is DBNull)) beData.SubmitDate_BS = reader.GetString(25);
                        if (!(reader[26] is DBNull)) beData.SubmissionsRequired = reader.GetBoolean(26);

                        if (!(reader[27] is DBNull)) beData.TotalDone = reader.GetInt32(27);
                        if (!(reader[28] is DBNull)) beData.TotalNotDone = reader.GetInt32(28);
                        if (!(reader[29] is DBNull)) beData.IsAllowLateSibmission = reader.GetBoolean(29);
                        if (!(reader[30] is DBNull)) beData.DeallineForRedo_AD = reader.GetDateTime(30);
                        if (!(reader[31] is DBNull)) beData.DeadlineForRedo_BS = reader.GetString(31);
                        if (!(reader[32] is DBNull)) beData.DeadlineforRedoTime = reader.GetDateTime(32);
                        if (reader.FieldCount > 32)
                        {
                            try
                            {
                                
                                
                                if (!(reader[33] is DBNull)) beData.StudentAttachments = reader.GetString(33);
                                if (!(reader[34] is DBNull)) beData.StudentNotes = reader.GetString(34);
                                if (!(reader[35] is DBNull)) beData.ReStudentNotes = reader.GetString(35);
                                if (!(reader[36] is DBNull)) beData.CheckedRemarks = reader.GetString(36);
                                if (!(reader[37] is DBNull)) beData.ReCheckedRemarks = reader.GetString(37);
                                if (!(reader[38] is DBNull)) beData.reSubmitStudentAttachments = reader.GetString(38);

                                if (!(reader[39] is DBNull)) beData.ReSubmitDateTime_AD = reader.GetDateTime(39);
                                if (!(reader[40] is DBNull)) beData.ReSubmitDateTime_BS = reader.GetString(40);

                               
                            }
                            catch { }
                        }
                        //Added By Suresh on 30 Magh for batch , Year, Semester
                        if (!(reader[41] is DBNull)) beData.Batch = reader.GetString(41);
                        if (!(reader[42] is DBNull)) beData.ClassYear = reader.GetString(42);
                        if (!(reader[43] is DBNull)) beData.Semester = reader.GetString(43);

                    }
                    catch { }

                   

                    string[] splits = new string[] { "##" };
                    if (!string.IsNullOrEmpty(beData.Attachments))
                    {
                        beData.AttachmentColl = new List<string>();
                        string[] attColl = beData.Attachments.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var v in attColl)
                            beData.AttachmentColl.Add(v);
                    }
                    if (!string.IsNullOrEmpty(beData.StudentAttachments))
                    {
                        beData.StudentAttachmentColl = new List<string>();
                        string[] attColl = beData.StudentAttachments.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var v in attColl)
                            beData.StudentAttachmentColl.Add(v);
                    }
                    if (!string.IsNullOrEmpty(beData.reSubmitStudentAttachments))
                    {
                        beData.reStudentAttachmentColl = new List<string>();
                        string[] attColl = beData.reSubmitStudentAttachments.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var v in attColl)
                            beData.reStudentAttachmentColl.Add(v);
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

        public AcademicLib.RE.HomeWork.HomeWorkDetailsCollections getHomeWorkDetailsById(int UserId, int HomeWorkId)
        {
            AcademicLib.RE.HomeWork.HomeWorkDetailsCollections dataColl = new RE.HomeWork.HomeWorkDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);            
            cmd.CommandText = "usp_GetHomeWorkById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.HomeWork.HomeWorkDetails beData = new RE.HomeWork.HomeWorkDetails();
                    if (!(reader[0] is DBNull)) beData.HomeWorkType = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.AssignDateTime_AD = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.AssignDate_BS = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DeadlineDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DeadlineDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DeadlineTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Topic = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Description = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.StudentId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.AutoNumber = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Name = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ClassName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Sectionname = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.RollNo = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Address = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.F_ContactNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PhotoPath = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.NoOfStudent = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.SubmitStatus = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.CheckStatus = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.NoOfSubmit = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.NoOfDone = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.NoOfReDo = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.TotalChecked = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.NoOfAttachment = reader.GetInt32(25);
                    if (!(reader[26] is DBNull)) beData.Attachments = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.StudentAttachments = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.StudentNotes = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.SubmitDateTime_AD = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.SubmitDate_BS = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.ReSubmitDateTime_AD = reader.GetDateTime(31);
                    if (!(reader[32] is DBNull)) beData.ReSubmitDate_BS = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.CheckedDateTime_AD = reader.GetDateTime(33);
                    if (!(reader[34] is DBNull)) beData.CheckeDate_BS = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.ReCheckedDateTime_AD = reader.GetDateTime(35);
                    if (!(reader[36] is DBNull)) beData.ReCheckeDate_BS = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.CheckedBy = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.Status = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.CheckedRemarks = reader.GetString(39);

                    try
                    {
                        if (!(reader[40] is DBNull)) beData.UserName = reader.GetString(40);
                        if (!(reader[41] is DBNull)) beData.SubjectName = reader.GetString(41);
                        if (!(reader[42] is DBNull)) beData.RegNo = reader.GetString(42);
                        if (!(reader[43] is DBNull)) beData.ReCheckedRemarks = reader.GetString(43);
                        if (!(reader[44] is DBNull)) beData.ReNotes = reader.GetString(44);
                        if (!(reader[45] is DBNull)) beData.reSubmitStudentAttachments = reader.GetString(45);
                        if (!(reader[46] is DBNull)) beData.Lesson = reader.GetString(46);

                        //Added by Suresh on 30 Magh
                        if (!(reader[47] is DBNull)) beData.Batch = reader.GetString(47);
                        if (!(reader[48] is DBNull)) beData.ClassYear = reader.GetString(48);
                        if (!(reader[49] is DBNull)) beData.Semester = reader.GetString(49);
                    }
                    catch { }
                    

                    //if(!string.IsNullOrEmpty(beData.StudentAttachments))
                    //{
                    //    beData.StudentAttachments = beData.StudentAttachments.Replace("\\", "/");
                    //}
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

        public AcademicLib.BE.HomeWork.HomeWork getHomeWorkById(int UserId, int EntityId, int HomeWorkId)
        {
            AcademicLib.BE.HomeWork.HomeWork beData = new AcademicLib.BE.HomeWork.HomeWork();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAddHomeWorkById";
            //cmd.CommandText = "usp_GetAdmissionEnquiryById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.HomeWork.HomeWork();
                    beData.HomeWorkId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TeacherId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.HomeworkTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
                   
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Lesson = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Topic = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DeadlineDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.DeadlineTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.DeadlineforRedo = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.DeadlineforRedoTime = reader.GetDateTime(12);                  
                    if (!(reader[13] is DBNull)) beData.IsAllowLateSibmission = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.SubmissionsRequired = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.BatchId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SemesterId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.ClassYearId = reader.GetInt32(17);
                    //Added
                    if (!(reader[18] is DBNull)) beData.ClassName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.SectionName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.HomeworkTypeName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.SubjectName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.BatchName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.ClassYearName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.SemesterName = reader.GetString(24);

                    if (!(reader[25] is DBNull)) beData.SectionIdColl = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.SectionListName = reader.GetString(26);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    beData.AttachmentColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int HomeWorkId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
            cmd.CommandText = "usp_DelHomeWorkById";
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
    }
}
