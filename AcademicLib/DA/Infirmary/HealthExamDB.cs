using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicLib.BE.Infirmary;
using Dynamic.DataAccess.Global;
using Dynamic.BusinessEntity.Account;
using System.Web;


namespace AcademicLib.DA.Infirmary
{
    public class HealthExamDB
    {
        DataAccessLayer1 dal = null;
        public HealthExamDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public Select2DataCollections GetUsers(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetUser";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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
        public Select2DataCollections GetDocumentTypes(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetDocumentTypes";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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
        public ResponeValues AddModifyHealthExaminer(HealthExaminer healthExaminer, int UserId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerId", healthExaminer.ExaminerId);
            cmd.Parameters.AddWithValue("@Name", healthExaminer.Name);
            cmd.Parameters.AddWithValue("@Designation", healthExaminer.Designation);
            cmd.Parameters.AddWithValue("@RegNo", healthExaminer.RegNo);
            cmd.Parameters.AddWithValue("@ContactNo", healthExaminer.ContactNo);
            cmd.Parameters.AddWithValue("@Email", healthExaminer.Email);
            cmd.Parameters.AddWithValue("@Qualification", healthExaminer.Qualification);
            cmd.Parameters.AddWithValue("@Address", healthExaminer.Address);
            cmd.Parameters.AddWithValue("@Specialization", healthExaminer.Specialization);
            cmd.Parameters.AddWithValue("@Username", healthExaminer.Username);
            cmd.Parameters.AddWithValue("@Remarks", healthExaminer.Remarks);
            cmd.Parameters.AddWithValue("@PhotoPath", healthExaminer.PhotoPath);
            cmd.Parameters.AddWithValue("@ModifyBy", UserId);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);
            cmd.CommandText = "usp_InfirmaryAddModifyHealthExaminer";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[16].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues AddModifyHealthExaminerDocument(HealthExaminerDocument healthExaminerDocument, int ExaminerId, int UserId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocumentId", healthExaminerDocument.DocumentID);
            cmd.Parameters.AddWithValue("@ExaminerId", ExaminerId);
            cmd.Parameters.AddWithValue("@DocumentTypeID", healthExaminerDocument.DocumentTypeID);
            cmd.Parameters.AddWithValue("@DocumentPath", healthExaminerDocument.DocumentPath);
            cmd.Parameters.AddWithValue("@DocumentSize", healthExaminerDocument.DocumentSize);
            cmd.Parameters.AddWithValue("@Description", healthExaminerDocument.Description);
            cmd.Parameters.AddWithValue("@ModifyBy", UserId);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);
            cmd.CommandText = "usp_InfirmaryAddModifyHealthExaminerDocment";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues DeleteHealthExaminer(int healthExaminerID)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerId", healthExaminerID);
            cmd.CommandText = "usp_InfirmaryDeleteHealthExaminer";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues DeleteHealthExaminerDocument(int healthExaminerDocumentID)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocumentId", healthExaminerDocumentID);
            cmd.CommandText = "usp_InfirmaryDeleteHealthExaminerDocment";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthExaminerCollections GetAllHealthExaminer(int PerPage, int PageNo, string SearchText)
        {
            HealthExaminerCollections resVal = new HealthExaminerCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PerPage", PerPage);
            cmd.Parameters.AddWithValue("@PageNo", PageNo);
            cmd.Parameters.AddWithValue("@SearchText", SearchText);
            cmd.CommandText = "usp_InfirmaryGetAllHealthExaminer";
            cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HealthExaminer beData = new HealthExaminer();
                    if (!(reader[1] is DBNull)) beData.ExaminerId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Designation = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Email = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Qualification = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Specialization = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Username = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.UsernameText = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Remarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.PhotoPath = reader.GetString(13);
                    resVal.Add(beData);
                }
                reader.Close();
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.TotalRows = Convert.ToInt32(cmd.Parameters[3].Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public HealthExaminer GetHealthExaminer(int ExaminerID)
        {
            HealthExaminer resVal = new HealthExaminer();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd1 = dal.GetCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ExaminerId", ExaminerID);
            cmd1.CommandText = "usp_InfirmaryGetHealthExaminerById";
            System.Data.SqlClient.SqlCommand cmd2 = dal.GetCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@ExaminerId", ExaminerID);
            cmd2.CommandText = "usp_InfirmaryGetHealthExaminerDocumentByExaminerId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    if (!(reader1[0] is DBNull)) resVal.ExaminerId = reader1.GetInt32(0);
                    if (!(reader1[1] is DBNull)) resVal.Name = reader1.GetString(1);
                    if (!(reader1[2] is DBNull)) resVal.Designation = reader1.GetString(2);
                    if (!(reader1[3] is DBNull)) resVal.RegNo = reader1.GetString(3);
                    if (!(reader1[4] is DBNull)) resVal.ContactNo = reader1.GetString(4);
                    if (!(reader1[5] is DBNull)) resVal.Email = reader1.GetString(5);
                    if (!(reader1[6] is DBNull)) resVal.Qualification = reader1.GetString(6);
                    if (!(reader1[7] is DBNull)) resVal.Address = reader1.GetString(7);
                    if (!(reader1[8] is DBNull)) resVal.Specialization = reader1.GetString(8);
                    if (!(reader1[9] is DBNull)) resVal.Username = reader1.GetInt32(9);
                    if (!(reader1[10] is DBNull)) resVal.UsernameText = reader1.GetString(10);
                    if (!(reader1[11] is DBNull)) resVal.Remarks = reader1.GetString(11);
                    if (!(reader1[12] is DBNull)) resVal.PhotoPath = reader1.GetString(12);
                }
                resVal.HealthExaminerDocuments = new HealthExaminerDocumentCollections();
                reader1.Close();
                System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    HealthExaminerDocument hed = new HealthExaminerDocument();
                    if (!(reader2[0] is DBNull)) hed.DocumentID = reader2.GetInt32(0);
                    if (!(reader2[2] is DBNull)) hed.DocumentTypeID = reader2.GetInt32(2);
                    if (!(reader2[3] is DBNull)) hed.DocumentTypeText = reader2.GetString(3);
                    if (!(reader2[4] is DBNull)) hed.DocumentPath = reader2.GetString(4).Replace("\\", "/");
                    if (!(reader2[5] is DBNull)) hed.DocumentSize = ( int )( reader2.GetInt32(5) / 1024 );
                    if (!(reader2[6] is DBNull)) hed.Description = reader2.GetString(6);
                    hed.Mode = "old";
                    resVal.HealthExaminerDocuments.Add(hed);
                }
                reader2.Close();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.CloseConnection();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        /*
        public ResponeValues SaveUpdate(BE.NewRoutine beData, int UserId, bool isModify)
        {
            if (beData.RoutineId == null) beData.RoutineId = -1;
            ResponeValues resVal = new ResponeValues();
            string days = "";
            if (beData.ForSunday != null && beData.ForSunday == true) days = days + "1,";
            if (beData.ForMonday != null && beData.ForMonday == true) days = days + "2,";
            if (beData.ForTuesday != null && beData.ForTuesday == true) days = days + "3,";
            if (beData.ForWednessday != null && beData.ForWednessday == true) days = days + "4,";
            if (beData.ForThursday != null && beData.ForThursday == true) days = days + "5,";
            if (beData.ForFriday != null && beData.ForFriday == true) days = days + "6,";
            if (beData.ForSaturday != null && beData.ForSaturday == true) days = days + "7,";
            days = days.Length == 0 ? days : days.Substring(0, days.Length - 1);
            for (int i = 0; i < beData.TimetableDetColl.Count; i++)
            {
                dal.OpenConnection();
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DateTime startTime = DateTime.Parse(beData.TimetableDetColl[i].TimeFrom);
                DateTime endTime = DateTime.Parse(beData.TimetableDetColl[i].TimeTo);
                cmd.Parameters.AddWithValue("@UserId", UserId.ToString());
                cmd.Parameters.AddWithValue("@RoutineId", beData.RoutineId);
                cmd.Parameters.AddWithValue("@Days", days);
                cmd.Parameters.AddWithValue("@Description", beData.Description);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.Parameters.AddWithValue("@Remarks", beData.TimetableDetColl[i].Remarks);
                cmd.Parameters.AddWithValue("@LogDateTime", DateTime.Now);
                if (isModify)
                {
                    cmd.CommandText = "usp_UpdateRoutine";
                }
                else
                {
                    cmd.CommandText = "usp_AddRoutine";
                }
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();

                    if (!(cmd.Parameters[8].Value is DBNull))
                        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                    if (!(cmd.Parameters[9].Value is DBNull))
                        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                    if (!(cmd.Parameters[10].Value is DBNull))
                        resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                    if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                }
                catch (System.Data.SqlClient.SqlException ee)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = ee.Message;
                    dal.CloseConnection();
                    break;
                }
                catch (Exception ee)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = ee.Message;
                    dal.CloseConnection();
                    break;
                }
                finally
                {
                    dal.CloseConnection();
                }
            }
            return resVal;
        }

        // new
        public AcademicERP.BE.RoutineCollections getAllRoutines(int UserId, int EntityId)
        {
            AcademicERP.BE.RoutineCollections dataColl = new AcademicERP.BE.RoutineCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllRoutine";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.Routine beData = new AcademicERP.BE.Routine();
                    beData.RoutineId = reader.GetInt32(0);
                    beData.DayName = reader.GetString(1);
                    string days = "";
                    if (beData.DayName.IndexOf('1') >= 0) days = days + "Su, ";
                    if (beData.DayName.IndexOf('2') >= 0) days = days + "Mo, ";
                    if (beData.DayName.IndexOf('3') >= 0) days = days + "Tu, ";
                    if (beData.DayName.IndexOf('4') >= 0) days = days + "We, ";
                    if (beData.DayName.IndexOf('5') >= 0) days = days + "Th, ";
                    if (beData.DayName.IndexOf('6') >= 0) days = days + "Fr, ";
                    if (beData.DayName.IndexOf('7') >= 0) days = days + "Sa, ";
                    beData.DayName = days.Length > 0 ? days.Substring(0, days.Length - 2) : days;
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2).Trim();
                    if (!(reader[3] is DBNull)) beData.CreatedBy = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StartTime = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EndTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6).Trim();
                    if (!(reader[7] is DBNull)) beData.LogDateTime = reader.GetString(7);

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

        public AcademicERP.BE.RoutineSingle getRoutineById(int UserId, int RoutineId)
        {
            AcademicERP.BE.RoutineSingle data = new AcademicERP.BE.RoutineSingle();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@RoutineId", RoutineId);
            cmd.CommandText = "usp_GetRoutineById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.RoutineId = reader.GetInt32(0);
                    data.DayName = reader.GetString(1);
                    string days = "";
                    if (data.DayName.IndexOf('1') >= 0) days = days + "Su, ";
                    if (data.DayName.IndexOf('2') >= 0) days = days + "Mo, ";
                    if (data.DayName.IndexOf('3') >= 0) days = days + "Tu, ";
                    if (data.DayName.IndexOf('4') >= 0) days = days + "We, ";
                    if (data.DayName.IndexOf('5') >= 0) days = days + "Th, ";
                    if (data.DayName.IndexOf('6') >= 0) days = days + "Fr, ";
                    if (data.DayName.IndexOf('7') >= 0) days = days + "Sa, ";
                    data.DayName = days.Length > 0 ? days.Substring(0, days.Length - 2) : days;
                    if (!(reader[2] is DBNull)) data.Description = reader.GetString(2).Trim();
                    if (!(reader[3] is DBNull)) data.CreatedBy = reader.GetString(3);
                    if (!(reader[4] is DBNull)) data.StartTime = reader.GetString(4);
                    if (!(reader[5] is DBNull)) data.EndTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) data.Remarks = reader.GetString(6).Trim();
                    if (!(reader[7] is DBNull)) data.LogDateTime = reader.GetString(7);
                }
                reader.Close();
                data.IsSuccess = true;
                data.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                data.IsSuccess = false;
                data.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return data;
        }

        public ResponeValues DeleteById(int UserId, int EntityId, int RoutineId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", 1);
            cmd.Parameters.AddWithValue("@RoutineId", RoutineId);
            cmd.CommandText = "usp_DelRoutineById";
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



        public AcademicERP.BE.Shift getShiftById(int UserId, int EntityId, int TranId)
        {
            AcademicERP.BE.Shift beData = new AcademicERP.BE.Shift();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetShiftById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicERP.BE.Shift();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.OrderNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);

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
        */


        // ===================================================================================================================// 

        // =================================== Health Checkup (Exam) starts ===================================================// 

        // ===================================================================================================================// 

        public Select2DataCollections GetAllClassListByName(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetAllClassByName";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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

        public Select2DataCollections GetAllSectionListByName(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetAllSectionByName";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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

        public Select2DataCollections GetAllExamTypeListByName(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetAllExamTypeByName";
            try {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
                    dataColl.Add(beData);
                }
                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee) {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally {
                dal.CloseConnection();
            }
            return dataColl;
        }


        public ExamTermTestCollections GetAllInfirmaryTestName()
        {
            ExamTermTestCollections dataColl = new ExamTermTestCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InfirmaryGetAllTestName";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExamTermTest beData = new ExamTermTest();
                    if (!(reader[0] is DBNull)) beData.TestId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);

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

        public Select2DataCollections GetAllTestListByName(string terms)
        {
            Select2DataCollections dataColl = new Select2DataCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            cmd.CommandText = "usp_InfirmaryGetAllTestByName";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Select2Data beData = new Select2Data();
                    beData.id = reader.GetInt32(0);
                    beData.text = reader.GetString(1);
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

        public ResponeValues AddUpdateHealthExam(int UserId, int? ClassId, int? SectionId, int? ExamTypeId, int? TestId, string DefaultValue, string DefaultRemarks)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@TestId", TestId);
            cmd.Parameters.AddWithValue("@DefaultValue", DefaultValue);
            cmd.Parameters.AddWithValue("@DefaultRemarks", DefaultRemarks);
            cmd.Parameters.AddWithValue("@LogDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdateLogDateTime", DateTime.Now);


            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;

            cmd.CommandText = "usp_InfirmaryAddUpdateHealthExam";
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                dal.CommitTransaction();
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



        public ExamTermDefaultInputCollections GetHealthExamByInput(int? ClassId, int? SectionId, int ExamTypeId, int TestId)
        {
            ExamTermDefaultInputCollections dataColl = new ExamTermDefaultInputCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@TestId", TestId);
            cmd.CommandText = "usp_InfirmaryGetHealthExamByInput";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExamTermDefaultInput beData = new ExamTermDefaultInput();
                    if (!(reader[0] is DBNull)) beData.TestId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestName = reader.GetString(1);
                    if (!(reader[1] is DBNull)) beData.DefaultValue = reader.GetString(2);
                    if (!(reader[2] is DBNull)) beData.DefaultRemarks = reader.GetString(3);
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




        public ExamTermDefaultInputCollections DeleteHealthExamByInput(int? ClassId, int? SectionId, int ExamTypeId, int TestId)
        {
            ExamTermDefaultInputCollections dataColl = new ExamTermDefaultInputCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@TestId", TestId);


            cmd.CommandText = "usp_InfirmaryDeleteHealthExamTest";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExamTermDefaultInput beData = new ExamTermDefaultInput();
                    beData.DefaultValue = reader.GetString(0);
                    beData.DefaultRemarks = reader.GetString(1);
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
        } // function ends



        public AddHealthExamParamCollections GetAllHealthExamByExamTypeId(int FromExamTypeId)
        {
            AddHealthExamParamCollections dataColl = new AddHealthExamParamCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamTypeId", FromExamTypeId);
            cmd.CommandText = "GetAllHealthExamByExamTypeId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AddHealthExamParam beData = new AddHealthExamParam();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ExamTypeId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ExamTypeName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.TestId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.TestName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.DefaultValue = reader.GetString(8);
                    else beData.DefaultValue = "";
                    if (!(reader[9] is DBNull)) beData.DefaultRemarks = reader.GetString(9);
                    else beData.DefaultRemarks = "";
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
        } // function ends




    } // class ends
} // namespace ends