using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AcademicLib.DA.Academic.Creation
{
    internal class SubjectDB : Dynamic.DataAccess.Common.CommonDB
    {        
        DataAccessLayer1 dal = null;
        public SubjectDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Creation.Subject beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Code", beData.Code);
            cmd.Parameters.AddWithValue("@CodeTH", beData.CodeTH);
            cmd.Parameters.AddWithValue("@CodePR", beData.CodePR);
            cmd.Parameters.AddWithValue("@IsECA", beData.IsECA);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);


            if (isModify)
            {
                cmd.CommandText = "usp_UpdateSubject";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddSubject";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsMath", beData.IsMath);

            cmd.Parameters.AddWithValue("@CRTH", beData.CRTH);
            cmd.Parameters.AddWithValue("@CRPR", beData.CRPR);
            cmd.Parameters.AddWithValue("@CR", beData.CR);

            try
            {
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
        public BE.Academic.Creation.SubjectCollections getAllSubject(int UserId, int EntityId,int AcademicYearId,int? EmployeeId=null, int? ClassId=null,int? SectionId=null,bool forAllSubject=false,int? BatchId=null,int? ClassYearId=null,int? SemesterId=null, string Role="")
        {
            BE.Academic.Creation.SubjectCollections dataColl = new BE.Academic.Creation.SubjectCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);

            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);

            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@ForAllSubject", forAllSubject);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@RefRoleId", Role);

            cmd.CommandText = "usp_GetAllSubject";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.Subject beData = new BE.Academic.Creation.Subject();
                    beData.SubjectId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CodeTH = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CodePR = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.IsECA = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsMath = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.CRTH = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.CRPR = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.CR = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.ClassId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.SectionId = reader.GetInt32(11);
                    try
                    {

                        if (!(reader[12] is DBNull)) beData.BatchId = reader.GetInt32(12);
                        if (!(reader[13] is DBNull)) beData.SemesterId = reader.GetInt32(13);
                        if (!(reader[14] is DBNull)) beData.ClassYearId = reader.GetInt32(14);
                        if (!(reader[15] is DBNull)) beData.Batch = reader.GetString(15);
                        if (!(reader[16] is DBNull)) beData.Semester = reader.GetString(16);
                        if (!(reader[17] is DBNull)) beData.ClassYear = reader.GetString(17);


                        if (!(reader[18] is DBNull)) beData.SubjectTeacher = Convert.ToBoolean(reader[18]);
                        if (!(reader[19] is DBNull)) beData.ClassTeacher = Convert.ToBoolean(reader[19]);
                        if (!(reader[20] is DBNull)) beData.CoOrdinator = Convert.ToBoolean(reader[20]);
                        if (!(reader[21] is DBNull)) beData.HOD = Convert.ToBoolean(reader[21]);
                        if (!(reader[22] is DBNull)) beData.Role = Convert.ToString(reader[22]);

                    }
                    catch { }
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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
        public BE.Academic.Creation.SubjectCollections getSubjectListForLessonPlan(int UserId, int ClassId, int? BatchId, int? ClassYearId, int? SemesterId, /*int? FacultyId,*/ int AcademicYearId)
        {
            BE.Academic.Creation.SubjectCollections dataColl = new BE.Academic.Creation.SubjectCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            //cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.CommandText = "usp_GetSubjectListForLessonPlan";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.Subject beData = new BE.Academic.Creation.Subject();
                    beData.SubjectId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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

        public BE.Academic.Creation.Subject getSubjectById(int UserId, int EntityId, int SubjectId)
        {
            BE.Academic.Creation.Subject beData = new BE.Academic.Creation.Subject();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetSubjectById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.Subject();
                    beData.SubjectId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CodeTH = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.CodePR = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.IsECA = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.IsMath = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.CRTH = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.CRPR = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.CR = Convert.ToDouble(reader[9]);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int SubjectId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_DelSubjectById";
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


        public ResponeValues ImportSubject(int UserId, List<BE.Academic.Creation.ImportSubject> dataColl)
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

                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Code", typeof(string));
                table.Columns.Add("CodeTH", typeof(string));
                table.Columns.Add("CodePR", typeof(string));
                table.Columns.Add("IsECA", typeof(string));
                table.Columns.Add("IsMath", typeof(string));
                table.Columns.Add("CRTH", typeof(string));
                table.Columns.Add("CRPR", typeof(string));
                table.Columns.Add("CR", typeof(string));
                foreach (var v in dataColl)
                {
                    var row = table.NewRow();
                    row["Name"] = IsDBNull(v.Name);
                    row["Code"] = IsDBNull(v.Code);
                    row["CodeTH"] = IsDBNull(v.CodeTH);
                    row["CodePR"] = IsDBNull(v.CodePR);
                    row["IsECA"] = IsDBNull(v.IsECA);
                    row["IsMath"] = IsDBNull(v.IsMath);
                    row["CRTH"] = IsDBNull(v.CRTH);
                    row["CRPR"] = IsDBNull(v.CRPR);
                    row["CR"] = IsDBNull(v.CR);
                    table.Rows.Add(row);
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@DataColl", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }

            cmd.CommandText = "usp_ImportSubject";
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
