using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.HomeWork
{
    internal class AssignmentDB
    {
        DataAccessLayer1 dal = null;
        public AssignmentDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.HomeWork.Assignment beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", beData.TeacherId);
            cmd.Parameters.AddWithValue("@AssignmentTypeId", beData.AssignmentTypeId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@Title", beData.Title);            
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@DeadlineDate", beData.DeadlineDate);
            cmd.Parameters.AddWithValue("@DeadlineTime", beData.DeadlineTime);
            cmd.Parameters.AddWithValue("@DeadlineforRedo", beData.DeadlineforRedo);
            cmd.Parameters.AddWithValue("@DeadlineforRedoTime", beData.DeadlineforRedoTime);
            cmd.Parameters.AddWithValue("@IsAllowLateSibmission", beData.IsAllowLateSibmission);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@AssignmentId", beData.AssignmentId);            
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateAssignment";
            }
            else
            {
                cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddAssignment";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@Weblink", beData.Weblink);
            cmd.Parameters.AddWithValue("@Marks", beData.Marks);
            cmd.Parameters.AddWithValue("@MarkScheme", beData.MarkScheme);
            cmd.Parameters.AddWithValue("@SectionIdColl", beData.SectionIdColl);

            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.NVarChar, 4000);
            cmd.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@Lesson", beData.Lesson);
            cmd.Parameters.AddWithValue("@Topic", beData.Topic);
            cmd.Parameters.AddWithValue("@SubmissionsRequired", beData.SubmissionsRequired);
            cmd.Parameters.AddWithValue("@Grade", beData.Grade);

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

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (!(cmd.Parameters[22].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[22].Value);

                if (!(cmd.Parameters[23].Value is DBNull) && resVal.IsSuccess)
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[23].Value);


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
        private void SaveSection(int UserId, int AssignmentId, int[] beDataColl)
        {
            if (beDataColl == null || beDataColl.Length == 0 || AssignmentId == 0)
                return;

            foreach (int sectionId in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddAssignmentSection";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveDocument(int UserId, int AssignmentId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || AssignmentId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
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
                    cmd.CommandText = "usp_AddAssignmentAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public ResponeValues UpdateDeadline(AcademicLib.BE.HomeWork.Assignment beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeadlineDate", beData.DeadlineDate);
            cmd.Parameters.AddWithValue("@DeadlineTime", beData.DeadlineTime);
            cmd.Parameters.AddWithValue("@DeadlineforRedo", beData.DeadlineforRedo);
            cmd.Parameters.AddWithValue("@DeadlineforRedoTime", beData.DeadlineforRedoTime);
            cmd.Parameters.AddWithValue("@AssignmentId", beData.AssignmentId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.CommandText = "usp_UpdateAssignmentDeadline";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@MarkScheme", beData.MarkScheme);
            cmd.Parameters.AddWithValue("@Marks", beData.Marks);
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
        public ResponeValues SubmitAssignment(AcademicLib.API.Student.AssignmentSubmit beData, ref string msg)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AssignmentId", beData.AssignmentId);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.CommandText = "usp_AssignmentSubmit";
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
                   // SaveStudentDocument(beData.UserId, resVal.RId, beData.AttachmentColl);
                    SaveStudentDocument(beData.UserId, resVal.RId, isRe, beData.AttachmentColl);
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
        private void SaveStudentDocument(int UserId, int TranId, bool isRe, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
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
                    cmd.Parameters.AddWithValue("@isRe", isRe);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddAssignmentSubmitAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public ResponeValues CheckAssignment(AcademicLib.API.Teacher.AssignmentChecked beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@AssignmentId", beData.AssignmentId);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@Status", beData.Status);
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.CommandText = "usp_AssignmentCheck";
            cmd.Parameters.Add("@SUserId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ObtainGrade", beData.ObtainGrade);
            cmd.Parameters.AddWithValue("@ObtainMark", beData.ObtainMark);
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

        public ResponeValues CheckClassWiseAssignment(int UserId, AcademicLib.API.Teacher.AssignmentCheckedCollections dataColl)
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
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@AssignmentId", beData.AssignmentId);
                    cmd.Parameters.AddWithValue("@Notes", beData.Notes);
                    cmd.Parameters.AddWithValue("@Status", beData.Status);
                    cmd.Parameters.AddWithValue("@UserId", beData.UserId);
                    cmd.CommandText = "usp_AssignmentCheck";
                    cmd.Parameters.Add("@SUserId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ObtainGrade", beData.ObtainGrade);
                    cmd.Parameters.AddWithValue("@ObtainMark", beData.ObtainMark);
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
        public AcademicLib.RE.HomeWork.AssignmentCollections getAllAssignment(int UserId, int EntityId, DateTime? dateFrom, DateTime? dateTo, bool isStudent = false,int? studentId=null, int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? EmployeeId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.RE.HomeWork.AssignmentCollections dataColl = new RE.HomeWork.AssignmentCollections();

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
            cmd.CommandText = "usp_GetAllAssignment";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.HomeWork.Assignment beData = new RE.HomeWork.Assignment();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AssignmentId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AssignmentType = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Title = reader.GetString(7);                    
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TeacherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.TeacherAddress = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TeacherContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.NoOfAttachment = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.Attachments = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.AsignDateTime_AD = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.AsignDateTime_BS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.DeadlineDate_AD = reader.GetDateTime(16);
                    if (!(reader[17] is DBNull)) beData.DeadlineDate_BS = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.DeadlineTime = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.TotalStudent = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.NoOfSubmit = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.TotalChecked = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.AssignmentStatus = reader.GetString(22);
                    try
                    {
                        if (!(reader[23] is DBNull)) beData.SubmitDateTime_AD = reader.GetDateTime(23);
                        if (!(reader[24] is DBNull)) beData.SubmitDate_BS = reader.GetString(24);                        
                        if (!(reader[25] is DBNull)) beData.TotalDone = reader.GetInt32(25);
                        if (!(reader[26] is DBNull)) beData.TotalNotDone = reader.GetInt32(26);
                        if (!(reader[27] is DBNull)) beData.IsAllowLateSibmission = reader.GetBoolean(27);
                        if (!(reader[28] is DBNull)) beData.DeallineForRedo_AD = reader.GetDateTime(28);
                        if (!(reader[29] is DBNull)) beData.DeadlineForRedo_BS = reader.GetString(29);
                        if (!(reader[30] is DBNull)) beData.DeadlineforRedoTime = reader.GetDateTime(30);
                        if (!(reader[31] is DBNull)) beData.MarkScheme = reader.GetInt32(31);
                        if (!(reader[32] is DBNull)) beData.Marks = Convert.ToDouble(reader[32]);
                        if (!(reader[33] is DBNull)) beData.Weblink = Convert.ToString(reader[33]);

                        try
                        {
                            if (!(reader["Lesson"] is DBNull)) beData.Lesson = Convert.ToString(reader["Lesson"]);
                            if (!(reader["Topic"] is DBNull)) beData.Topic = Convert.ToString(reader["Topic"]);
                            if (!(reader["SubmissionsRequired"] is DBNull)) beData.SubmissionsRequired = Convert.ToBoolean(reader["SubmissionsRequired"]);
                            if (!(reader["Grade"] is DBNull)) beData.Topic = Convert.ToString(reader["Grade"]);
                        }
                        catch { }

                        if (reader.FieldCount > 30)
                        {
                            try
                            { 

                                if (!(reader[34] is DBNull)) beData.StudentAttachments = reader.GetString(34);
                                if (!(reader[35] is DBNull)) beData.StudentNotes = reader.GetString(35);
                                if (!(reader[36] is DBNull)) beData.ReStudentNotes = reader.GetString(36);
                                if (!(reader[37] is DBNull)) beData.CheckedRemarks = reader.GetString(37);
                                if (!(reader[38] is DBNull)) beData.ReCheckedRemarks = reader.GetString(38);
                                if (!(reader[39] is DBNull)) beData.reSubmitStudentAttachments = reader.GetString(39);
                                if (!(reader[40] is DBNull)) beData.ReSubmitDateTime_AD = reader.GetDateTime(40);
                                if (!(reader[41] is DBNull)) beData.ReSubmitDateTime_BS = reader.GetString(41);
                                if (!(reader[42] is DBNull)) beData.ObtainGrade = reader.GetString(42);
                                if (!(reader[43] is DBNull)) beData.ObtainMark = Convert.ToDouble(reader[43]);

                            
                            }
                            catch { }
                        }


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

        public AcademicLib.RE.HomeWork.AssignmentDetailsCollections getAssignmentDetailsById(int UserId, int AssignmentId)
        {
            AcademicLib.RE.HomeWork.AssignmentDetailsCollections dataColl = new RE.HomeWork.AssignmentDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
            cmd.CommandText = "usp_GetAssignmentById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.HomeWork.AssignmentDetails beData = new RE.HomeWork.AssignmentDetails();
                    if (!(reader[0] is DBNull)) beData.AssignmentType = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.AssignDateTime_AD = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.AssignDate_BS = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DeadlineDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DeadlineDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DeadlineTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Topic = reader.GetString(6);
                    beData.Title = beData.Topic;
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

                        if (!(reader[46] is DBNull)) beData.MarkScheme = reader.GetInt32(46);
                        if (!(reader[47] is DBNull)) beData.Marks = Convert.ToDouble(reader[47]);
                        if (!(reader[48] is DBNull)) beData.ObtainGrade = reader.GetString(48);
                        if (!(reader[49] is DBNull)) beData.ObtainMark = Convert.ToDouble(reader[49]);
                        if (!(reader[50] is DBNull)) beData.Weblink = reader.GetString(50);

                        if (!(reader[51] is DBNull)) beData.Lesson = reader.GetString(51);
                        if (!(reader[52] is DBNull)) beData.Topic = reader.GetString(52);
                        if (!(reader[53] is DBNull)) beData.SubmissionsRequired = reader.GetBoolean(53);
                        if (!(reader[54] is DBNull)) beData.Grade = reader.GetString(54);

                        //beData.Lesson = beData.Weblink;

                        if (beData.MarkScheme == 1)
                            beData.OM = beData.ObtainMark.ToString();
                        else
                            beData.OM = beData.ObtainGrade;
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

        public AcademicLib.BE.HomeWork.Assignment getAssignmentById(int UserId, int EntityId, int AssignmentId)
        {
            AcademicLib.BE.HomeWork.Assignment beData = new AcademicLib.BE.HomeWork.Assignment();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAddAssignmentById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.HomeWork.Assignment();
                    beData.AssignmentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TeacherId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AssignmentTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Title = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Weblink = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DeadlineDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.DeadlineTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.DeadlineforRedo = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.DeadlineforRedoTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.MarkScheme = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Marks = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.IsAllowLateSibmission = reader.GetBoolean(15);   
                    if (!(reader[16] is DBNull)) beData.Lesson = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Topic = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.SubmissionsRequired = reader.GetBoolean(18);
                    if (!(reader[19] is DBNull)) beData.Grade = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BatchId = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.SemesterId = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.ClassYearId = reader.GetInt32(22);
                    //Added
                    if (!(reader[23] is DBNull)) beData.ClassName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.SectionName = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.AssignmentTypeName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.SubjectName = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.BatchName = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ClassYearName = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.SemesterName = reader.GetString(29);

                    if (!(reader[30] is DBNull)) beData.SectionIdColl = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.SectionListName = reader.GetString(31);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int AssignmentId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
            cmd.CommandText = "usp_DelAssignmentById";
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
