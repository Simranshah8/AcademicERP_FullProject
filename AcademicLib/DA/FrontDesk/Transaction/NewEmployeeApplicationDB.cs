using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class NewEmployeeApplicationDB
    {
        DataAccessLayer1 dal = null;
        public NewEmployeeApplicationDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.NewEmployeeApplication beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationId", beData.ApplicationId);
            cmd.Parameters.AddWithValue("@ApplictionDate", beData.ApplictionDate);
            cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", beData.LastName);
            cmd.Parameters.AddWithValue("@Gender", beData.Gender);
            cmd.Parameters.AddWithValue("@DOB", beData.DOB);
            cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@MaritalStatus", beData.MaritalStatus);
            cmd.Parameters.AddWithValue("@FullAddress", beData.FullAddress);
            cmd.Parameters.AddWithValue("@ISTeacher", beData.ISTeacher);
            cmd.Parameters.AddWithValue("@Source", beData.Source);
            cmd.Parameters.AddWithValue("@SalaryExpectation", beData.SalaryExpectation);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@LevelId", beData.LevelId);
            //cmd.Parameters.AddWithValue("@Photo", beData.Photo);
            //cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
            ////
            //cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            //cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            //cmd.Parameters.AddWithValue("@NewEmployeeId", beData.NewEmployeeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateNewEmployeeApplication";
            }
            else
            {
                cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddNewEmployeeApplication";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[22].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[22].Value);

                if (!(cmd.Parameters[23].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[23].Value);

                if (!(cmd.Parameters[24].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[24].Value);

                if (!(cmd.Parameters[25].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[25].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {

                    SaveNewEmployeeQualification(beData.CUserId, resVal.RId, beData.NewEmployeeQualificationColl);
                    SaveNewEmployeeWorkExperience(beData.CUserId, resVal.RId, beData.NewEmployeeWorkExperienceColl);
                    SaveNewEmployeeReference(beData.CUserId, resVal.RId, beData.NewEmployeeReferenceColl);
                    SaveNewEmployeeAttachDocument(beData.CUserId, resVal.RId, beData.NewEmployeeAttachDocumentColl);
                }
                // if (beData.NewEmployeeQualificationColl != null && beData.NewEmployeeQualificationColl.Count > 0)
                //   SaveNewEmployeeQualification(beData.NewEmployeeId, beData.NewEmployeeQualificationColl);
                //  if (beData.NewEmployeeWorkExperienceColl != null && beData.NewEmployeeWorkExperienceColl.Count > 0)
                // SaveNewEmployeeWorkExperience(beData.NewEmployeeId, beData.NewEmployeeWorkExperienceColl);
                //if (beData.NewEmployeeReferenceColl != null && beData.NewEmployeeReferenceColl.Count > 0)
                //  SaveNewEmployeeReference(beData.NewEmployeeId, beData.NewEmployeeReferenceColl);
                // if (beData.NewEmployeeAttachDocumentColl != null && beData.NewEmployeeAttachDocumentColl.Count > 0)
                //  SaveNewEmployeeAttachDocument(beData.NewEmployeeId, beData.NewEmployeeAttachDocumentColl);


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
        private void SaveNewEmployeeQualification(int UserId, int NewEmployeeId, List<BE.FrontDesk.Transaction.NewEmployeeQualification> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || NewEmployeeId == 0)
                return;

            foreach (BE.FrontDesk.Transaction.NewEmployeeQualification beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@BoardUniversity", beData.BoardUniversity);
                cmd.Parameters.AddWithValue("@PassedYear", beData.PassedYear);
                cmd.Parameters.AddWithValue("@GradePercentage", beData.GradePercentage);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddNewEmployeeQualification";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveNewEmployeeWorkExperience(int UserId, int NewEmployeeId, List<BE.FrontDesk.Transaction.NewEmployeeWorkExperience> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || NewEmployeeId == 0)
                return;

            foreach (BE.FrontDesk.Transaction.NewEmployeeWorkExperience beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
                cmd.Parameters.AddWithValue("@Organization", beData.Organization);
                cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
                cmd.Parameters.AddWithValue("@JobTitle", beData.JobTitle);
                cmd.Parameters.AddWithValue("@WorkDuration", beData.WorkDuration);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddNewEmployeeWorkExperience";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveNewEmployeeReference(int UserId, int NewEmployeeId, List<BE.FrontDesk.Transaction.NewEmployeeReference> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || NewEmployeeId == 0)
                return;

            foreach (BE.FrontDesk.Transaction.NewEmployeeReference beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
                cmd.Parameters.AddWithValue("@ReferencePerson", beData.ReferencePerson);
                cmd.Parameters.AddWithValue("@DesignationId", beData.DesignationId);
                cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
                cmd.Parameters.AddWithValue("@Email", beData.Email);
                cmd.Parameters.AddWithValue("@Organiozation", beData.Organiozation);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddNewEmployeeReference";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveNewEmployeeAttachDocument(int UserId, int NewEmployeeId, List<BE.FrontDesk.Transaction.NewEmployeeAttachDocument> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || NewEmployeeId == 0)
                return;

            foreach (BE.FrontDesk.Transaction.NewEmployeeAttachDocument beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
                cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                cmd.Parameters.AddWithValue("@AttachDocumet", beData.AttachDocumet);
                cmd.Parameters.AddWithValue("@AttachDocumentPath", beData.AttachDocumentPath);
                cmd.Parameters.AddWithValue("@Description", beData.Description);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddNewEmployeeAttachDocument";
                cmd.ExecuteNonQuery();
            }

        }

        //
        public BE.FrontDesk.Transaction.NewEmployeeApplicationCollections getAllNewEmployeeApplication(int UserId, int EntityId)
        {
            BE.FrontDesk.Transaction.NewEmployeeApplicationCollections dataColl = new BE.FrontDesk.Transaction.NewEmployeeApplicationCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllNewEmployeeApplication";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.NewEmployeeApplication beData = new BE.FrontDesk.Transaction.NewEmployeeApplication();
                   // beData.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ApplicationId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ApplictionDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.FirstName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MiddleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.LastName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.DOB = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Religion = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Nationality = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.Email = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.MaritalStatus = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.FullAddress = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ISTeacher = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.Source = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SalaryExpectation = reader.GetDecimal(16);
                    if (!(reader[17] is DBNull)) beData.SubjectId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.LevelId = reader.GetInt32(18);
                    //if (!(reader[19] is DBNull)) beData.Photo = reader.GetString(19);
                    //if (!(reader[20] is DBNull)) beData.PhotoPath = reader.GetString(20);
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
        public BE.FrontDesk.Transaction.NewEmployeeApplication getNewEmployeeApplicationById(int UserId, int EntityId, int NewEmployeeId)
        {
            BE.FrontDesk.Transaction.NewEmployeeApplication beData = new BE.FrontDesk.Transaction.NewEmployeeApplication();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetNewEmployeeApplicationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.NewEmployeeApplication();
                    //beData.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ApplicationId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ApplictionDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.FirstName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MiddleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.LastName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.DOB = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Religion = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Nationality = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.Email = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.MaritalStatus = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.FullAddress = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ISTeacher = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.Source = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.SalaryExpectation = reader.GetDecimal(16);
                    if (!(reader[17] is DBNull)) beData.SubjectId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.LevelId = reader.GetInt32(18);
                    //if (!(reader[19] is DBNull)) beData.Photo = reader.GetString(19);
                    //if (!(reader[20] is DBNull)) beData.PhotoPath = reader.GetString(20);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.NewEmployeeQualification Qualification = new BE.FrontDesk.Transaction.NewEmployeeQualification();

                    if (!(reader[0] is System.DBNull)) Qualification.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Qualification.Name = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Qualification.BoardUniversity = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Qualification.PassedYear = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Qualification.GradePercentage = reader.GetString(4);
                    beData.NewEmployeeQualificationColl.Add(Qualification);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.NewEmployeeWorkExperience Experience = new BE.FrontDesk.Transaction.NewEmployeeWorkExperience();

                    if (!(reader[0] is System.DBNull)) Experience.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Experience.Organization = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Experience.DepartmentId = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) Experience.JobTitle = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Experience.WorkDuration = reader.GetString(4);
                    if (!(reader[4] is System.DBNull)) Experience.Remarks = reader.GetString(4);
                    beData.NewEmployeeWorkExperienceColl.Add(Experience);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.NewEmployeeReference Reference = new BE.FrontDesk.Transaction.NewEmployeeReference();

                    if (!(reader[0] is System.DBNull)) Reference.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Reference.ReferencePerson = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Reference.DesignationId = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) Reference.ContactNo = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Reference.Email = reader.GetString(4);
                    if (!(reader[4] is System.DBNull)) Reference.Organiozation = reader.GetString(4);
                    beData.NewEmployeeReferenceColl.Add(Reference);
                }
                 reader.NextResult();

                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.NewEmployeeAttachDocument Document = new BE.FrontDesk.Transaction.NewEmployeeAttachDocument();

                    if (!(reader[0] is System.DBNull)) Document.NewEmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Document.DocumentTypeId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Document.AttachDocumet = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Document.AttachDocumentPath = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Document.Description = reader.GetString(4);
                    beData.NewEmployeeAttachDocumentColl.Add(Document);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int NewEmployeeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@NewEmployeeId", NewEmployeeId);
            cmd.CommandText = "usp_DelNewEmployeeApplicationById";
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
