using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
namespace AcademicLib.DA.Academic.Creation
{
    internal class ClassDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public ClassDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Creation.Class beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);          
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);

            if (isModify)
            {                               
                cmd.CommandText = "usp_UpdateClass";
            }
            else
            {                                
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddClass";                
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsPassOut", beData.IsPassOut);
            cmd.Parameters.AddWithValue("@StartMonthId", beData.StartMonthId);
            cmd.Parameters.AddWithValue("@EndMonthId", beData.EndMonthId);
            cmd.Parameters.AddWithValue("@FacultyId", beData.FacultyId);
            cmd.Parameters.AddWithValue("@ClassType", beData.ClassType);
            cmd.Parameters.AddWithValue("@LevelId", beData.LevelId);
            cmd.Parameters.AddWithValue("@RunningAcademicYearId", beData.RunningAcademicYearId);
            cmd.Parameters.AddWithValue("@ActiveFeeMapppingMonth", beData.ActiveFeeMapppingMonth);
            cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
            cmd.Parameters.AddWithValue("@Board", beData.Board);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId.Value);

            cmd.Parameters.AddWithValue("@Api_Id", beData.Api_Id);
            cmd.Parameters.AddWithValue("@Api_ResponseId", beData.Api_ResponseId);
            cmd.Parameters.AddWithValue("@LastApiCallAt", beData.LastApiCallAt);
            cmd.Parameters.AddWithValue("@LastResponse", beData.LastResponse);

            cmd.Parameters.AddWithValue("@ugc_universityId", beData.ugc_universityId);
            cmd.Parameters.AddWithValue("@ugc_campusId", beData.ugc_campusId);
            cmd.Parameters.AddWithValue("@ugc_levelId", beData.ugc_levelId);
            cmd.Parameters.AddWithValue("@ugc_levelName", beData.ugc_levelName);
            cmd.Parameters.AddWithValue("@ugc_facultyId", beData.ugc_facultyId);
            cmd.Parameters.AddWithValue("@ugc_facultyName", beData.ugc_facultyName);
            cmd.Parameters.AddWithValue("@ugc_programType", beData.ugc_programType);
            cmd.Parameters.AddWithValue("@ugc_programId", beData.ugc_programId);
            cmd.Parameters.AddWithValue("@ugc_programName", beData.ugc_programName);
            cmd.Parameters.AddWithValue("@ugc_duration", beData.ugc_duration);
            cmd.Parameters.AddWithValue("@Faculty", beData.Faculty);
            //StartDate,EndDate
            cmd.Parameters.AddWithValue("@StartDate", beData.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", beData.EndDate);
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

                if (!resVal.IsSuccess && resVal.ErrorNumber>0)
                    resVal.ResponseMSG = resVal.ResponseMSG+" ("+resVal.ErrorNumber.ToString()+")";

                if (resVal.IsSuccess)
                {
                    SaveClassWiseClassYear(beData.CUserId, resVal.RId, beData.ClassYearIdColl);
                    SaveClassWiseSemester(beData.CUserId, resVal.RId, beData.ClassSemesterIdColl);
                    SaveClassWiseAcademicMonth(beData.CUserId, resVal.RId,beData.AcademicYearId.Value, beData.AcademicMonthColl);
                    UpdateClassWiseMonth(beData.CUserId, resVal.RId, beData.AcademicYearId.Value);
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
        private void SaveClassWiseSemester(int UserId,int ClassId,List<int> dataColl)
        {
            if (dataColl == null)
                return;
           
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SemesterId", beData);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddClassWiseSemester";
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateClassWiseMonth(int UserId, int ClassId, int AcademicYearId)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);            
            cmd.CommandText = "usp_UpdateClassWiseMonth";
            cmd.ExecuteNonQuery();
        }

        private void SaveClassWiseAcademicMonth(int UserId, int ClassId,int AcademicYearId, List<BE.Academic.Creation.ClassWiseAcademicMonth> dataColl)
        {
            if (dataColl == null)
                return;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var beData in dataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && beData.MSNo>0)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassId", ClassId);
                    cmd.Parameters.AddWithValue("@FromMonth", 0);
                    cmd.Parameters.AddWithValue("@ToMonth", 0);
                    cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
                    cmd.Parameters.AddWithValue("@SNo", beData.MonthId);
                    cmd.Parameters.AddWithValue("@MSNo", beData.MSNo);
                    cmd.Parameters.AddWithValue("@MonthN", beData.Name);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                    cmd.Parameters.AddWithValue("@StartDate", IsDBNull(beData.StartDate));
                    cmd.Parameters.AddWithValue("@EndDate", IsDBNull(beData.EndDate));
                    //StartDate,EndDate,
                    cmd.CommandText = "insert into tbl_ClassWiseAcademicMonth(StartDate,EndDate,AcademicYearId,ClassId,FromMonth,ToMonth,MonthId,SNo,MSNo,MonthN,CreateBy) values(@StartDate,@EndDate,@AcademicYearId,@ClassId,@FromMonth,@ToMonth,@MonthId,@SNo,@MSNo,@MonthN,@UserId)";
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
        private void SaveClassWiseClassYear(int UserId, int ClassId, List<int> dataColl)
        {

            if (dataColl == null)
                return;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddClassWiseClassYear";
                cmd.ExecuteNonQuery();
            }
        }
        public ResponeValues UpdateClassForOR(int UserId,BE.Academic.Creation.ClassCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
                    cmd.Parameters.AddWithValue("@ForOnlineRegistration", beData.ForOnlineRegistration);
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "update tbl_Class set DisplayName=@DisplayName,ForOnlineRegistration=@ForOnlineRegistration where ClassId=@ClassId";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Data Updated";
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
        public BE.Academic.Creation.ClassCollections getAllClass(int UserId, int EntityId,bool forOnlineRegistration=false)
        {
            BE.Academic.Creation.ClassCollections dataColl = new BE.Academic.Creation.ClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@forOnlineRegistration", forOnlineRegistration);
            cmd.CommandText = "usp_GetAllClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.Class beData = new BE.Academic.Creation.Class();
                    beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ForOnlineRegistration = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.DisplayName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.IsPassOut = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.StartMonthId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.EndMonthId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.StartMonth = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.EndMonth = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.FacultyId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.ClassType = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.FacultyName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LevelId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.LevelName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.RunningAcademicYearId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.RunningAcademicYear = reader.GetString(17);
                    try
                    {
                        if (!(reader[18] is DBNull)) beData.ActiveFeeMapppingMonth = reader.GetBoolean(18);
                        if (!(reader[19] is DBNull)) beData.IsActive = reader.GetBoolean(19);
                        if (!(reader[20] is DBNull)) beData.Board = reader.GetString(20);
                        if (!(reader[21] is DBNull)) beData.Faculty = reader.GetString(21);
                        //StartDate,EndDate
                        if (!(reader["StartDate"] is DBNull)) beData.StartDate = Convert.ToDateTime(reader["StartDate"]);
                        if (!(reader["EndDate"] is DBNull)) beData.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    catch { }
                    dataColl.Add(beData);
                }
                if(dataColl!=null && dataColl.Count > 0)
                {
                    int classId = 0;
                    reader.NextResult();
                    while (reader.Read())
                    {
                        classId = reader.GetInt32(0);
                        try
                        {
                            dataColl.Find(p1 => p1.ClassId == classId).ClassSemesterIdColl.Add(reader.GetInt32(1));
                        }
                        catch {
                            break;
                        }

                    }
                    reader.NextResult();
                    while (reader.Read())
                    {

                        classId = reader.GetInt32(0);
                        try
                        {
                            dataColl.Find(p1 => p1.ClassId == classId).ClassYearIdColl.Add(reader.GetInt32(1));
                        }
                        catch {
                            break;
                        }

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
        public BE.Academic.Creation.Class getClassById(int UserId, int EntityId,int ClassId,int AcademicYearId)
        {
            BE.Academic.Creation.Class beData = new BE.Academic.Creation.Class();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetClassById";
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.Class();
                    beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.IsPassOut = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.StartMonthId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.EndMonthId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FacultyId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ClassType = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.LevelId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.RunningAcademicYearId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.ActiveFeeMapppingMonth = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.IsActive = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.Board = reader.GetString(13);

                    try
                    {
                        if (!(reader[14] is DBNull)) beData.ugc_universityId = reader.GetInt32(14);
                        if (!(reader[15] is DBNull)) beData.ugc_campusId = reader.GetInt32(15);
                        if (!(reader[16] is DBNull)) beData.ugc_levelId = reader.GetInt32(16);
                        if (!(reader[17] is DBNull)) beData.ugc_levelName = reader.GetString(17);
                        if (!(reader[18] is DBNull)) beData.ugc_facultyId = reader.GetInt32(18);
                        if (!(reader[19] is DBNull)) beData.ugc_facultyName = reader.GetString(19);
                        if (!(reader[20] is DBNull)) beData.ugc_programType = reader.GetString(20);
                        if (!(reader[21] is DBNull)) beData.ugc_programId = reader.GetInt32(21);
                        if (!(reader[22] is DBNull)) beData.ugc_programName = reader.GetString(22);
                        if (!(reader[23] is DBNull)) beData.ugc_duration = reader.GetString(23);
                        if (!(reader[24] is DBNull)) beData.Faculty = reader.GetString(24);
                        //StartDate,EndDate
                        if (!(reader["StartDate"] is DBNull)) beData.StartDate = Convert.ToDateTime(reader["StartDate"]);
                        if (!(reader["EndDate"] is DBNull)) beData.EndDate = Convert.ToDateTime(reader["EndDate"]);

                    }
                    catch { }
                }
                reader.NextResult();
                beData.ClassSemesterIdColl = new List<int>();
                beData.ClassYearIdColl = new List<int>();
                while (reader.Read())
                {
                    beData.ClassSemesterIdColl.Add(reader.GetInt32(0));
                }
                reader.NextResult();
                while (reader.Read())
                {
                    beData.ClassYearIdColl.Add(reader.GetInt32(0));
                }
                reader.NextResult();
                beData.AcademicMonthColl = new List<BE.Academic.Creation.ClassWiseAcademicMonth>();
                while (reader.Read())
                {
                    var ac = new BE.Academic.Creation.ClassWiseAcademicMonth();
                    if (!(reader[0] is DBNull)) ac.MonthId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) ac.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) ac.MSNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) ac.StartDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) ac.EndDate = reader.GetDateTime(4);
                    
                    beData.AcademicMonthColl.Add(ac);
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
        public ResponeValues DeleteById(int UserId,int EntityId,int ClassId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_DelClassById";
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

        public BE.Academic.Creation.ClassSectionList getClassSectionList(int UserId,int AcademicYearId,bool ForRunning,string Role)
        {
            BE.Academic.Creation.ClassSectionList beData = new BE.Academic.Creation.ClassSectionList();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            if(!ForRunning)
                cmd.CommandText = "usp_GetClassSectionList";
            else
            {
                cmd.CommandText = "usp_GetRunningClassSectionList";
                cmd.Parameters.AddWithValue("@RefRoleId", Role);
            }
                

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                beData.ClassList = new BE.Academic.Creation.ClassCollections();
                beData.SectionList = new BE.Academic.Creation.ClassSectionCollections();
                beData.SectionListOnly = new BE.Academic.Creation.ClassSectionCollections();
                while (reader.Read())
                {
                    BE.Academic.Creation.Class clas = new BE.Academic.Creation.Class();
                    clas.ClassId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) clas.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) clas.OrderNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) clas.ClassType = reader.GetInt32(3);
                    try
                    {
                        if (!(reader[4] is DBNull)) clas.BatchId = reader.GetInt32(4);
                        if (!(reader[5] is DBNull)) clas.Batch = reader.GetString(5);
                        if (!(reader[6] is DBNull)) clas.SemesterId = reader.GetInt32(6);
                        if (!(reader[7] is DBNull)) clas.Semester = reader.GetString(7);
                        if (!(reader[8] is DBNull)) clas.ClassYearId = reader.GetInt32(8);
                        if (!(reader[9] is DBNull)) clas.ClassYear = reader.GetString(9);

                        if (!(reader[10] is DBNull)) clas.SubjectTeacher = Convert.ToBoolean(reader[10]);
                        if (!(reader[11] is DBNull)) clas.ClassTeacher = Convert.ToBoolean(reader[11]);
                        if (!(reader[12] is DBNull)) clas.CoOrdinator = Convert.ToBoolean(reader[12]);
                        if (!(reader[13] is DBNull)) clas.HOD = Convert.ToBoolean(reader[13]);
                        if (!(reader[14] is DBNull)) clas.Role = Convert.ToString(reader[14]);
                    }
                    catch { }

                    beData.ClassList.Add(clas);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Creation.ClassSection clas = new BE.Academic.Creation.ClassSection();
                    clas.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) clas.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) clas.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) clas.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) clas.ClassSNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) clas.SectionSNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) clas.ClassType = reader.GetInt32(6);
                    try
                    {
                        if (!(reader[7] is DBNull)) clas.BatchId = reader.GetInt32(7);
                        if (!(reader[8] is DBNull)) clas.Batch = reader.GetString(8);
                        if (!(reader[9] is DBNull)) clas.SemesterId = reader.GetInt32(9);
                        if (!(reader[10] is DBNull)) clas.Semester = reader.GetString(10);
                        if (!(reader[11] is DBNull)) clas.ClassYearId = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) clas.ClassYear = reader.GetString(12);

                        if (!(reader[13] is DBNull)) clas.SubjectTeacher = Convert.ToBoolean(reader[13]);
                        if (!(reader[14] is DBNull)) clas.ClassTeacher = Convert.ToBoolean(reader[14]);
                        if (!(reader[15] is DBNull)) clas.CoOrdinator = Convert.ToBoolean(reader[15]);
                        if (!(reader[16] is DBNull)) clas.HOD = Convert.ToBoolean(reader[16]);
                        if (!(reader[17] is DBNull)) clas.Role = Convert.ToString(reader[17]);
                    }
                    catch { }
                    clas.FilterSection = true;
                    beData.SectionList.Add(clas);

                    if (clas.SectionId > 0)
                        beData.SectionListOnly.Add(clas);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Creation.ClassSection clas = new BE.Academic.Creation.ClassSection();
                    clas.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) clas.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) clas.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) clas.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) clas.ClassSNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) clas.SectionSNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) clas.ClassType = reader.GetInt32(6);
                    try
                    {
                        if (!(reader[7] is DBNull)) clas.BatchId = reader.GetInt32(7);
                        if (!(reader[8] is DBNull)) clas.Batch = reader.GetString(8);
                        if (!(reader[9] is DBNull)) clas.SemesterId = reader.GetInt32(9);
                        if (!(reader[10] is DBNull)) clas.Semester = reader.GetString(10);
                        if (!(reader[11] is DBNull)) clas.ClassYearId = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) clas.ClassYear = reader.GetString(12);

                        if (!(reader[13] is DBNull)) clas.SubjectTeacher = Convert.ToBoolean(reader[13]);
                        if (!(reader[14] is DBNull)) clas.ClassTeacher = Convert.ToBoolean(reader[14]);
                        if (!(reader[15] is DBNull)) clas.CoOrdinator = Convert.ToBoolean(reader[15]);
                        if (!(reader[16] is DBNull)) clas.HOD = Convert.ToBoolean(reader[16]);
                        if (!(reader[17] is DBNull)) clas.Role = Convert.ToString(reader[17]);
                    }
                    catch { }
                    if (clas.SectionId>0)
                        clas.FilterSection = true;
                    else
                        clas.FilterSection = false;

                    beData.SectionListWithClass.Add(clas);                     
                }
                reader.NextResult();
                int cid = 0;
                while (reader.Read())
                {
                    cid = reader.GetInt32(0);
                    var find = beData.ClassList.Find(p1 => p1.ClassId == cid);
                    if (find != null)
                        find.ClassSemesterIdColl.Add(reader.GetInt32(1));
                }
                reader.NextResult();
                cid = 0;
                while (reader.Read())
                {
                    cid = reader.GetInt32(0);
                    var find = beData.ClassList.Find(p1 => p1.ClassId == cid);
                    if (find != null)
                        find.ClassYearIdColl.Add(reader.GetInt32(1));
                }

                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
                beData.ClassList.ResponseMSG = GLOBALMSG.SUCCESS;
                beData.ClassList.IsSuccess = true;

                if (beData.SectionListWithClass != null)
                {
                    var tmpSectionColl = beData.SectionListWithClass;
                    beData.SectionListWithClass = new BE.Academic.Creation.ClassSectionCollections();
                    var querySec = from sc in tmpSectionColl
                                   group sc by new { sc.BatchId, sc.ClassId, sc.SectionId } into g
                                   select new AcademicLib.BE.Academic.Creation.ClassSection
                                   {
                                        ClassId=g.Key.ClassId,
                                        SectionId=g.Key.SectionId,
                                         ClassName=g.First().ClassName,
                                          Batch=g.First().Batch,
                                           BatchId=g.First().BatchId,
                                            ClassSNo=g.First().ClassSNo,
                                             ClassType=g.First().ClassType,
                                              ClassYear=g.First().ClassYear,
                                               ClassYearId=g.First().ClassYearId,
                                                FilterSection=g.First().FilterSection,
                                                 NoOfStudent=g.Sum(p1=>p1.NoOfStudent),
                                                  SectionName=g.First().SectionName,
                                                   SectionSNo=g.First().SectionSNo,
                                                    Semester=g.First().Semester,
                                                     SemesterId=g.First().SemesterId, 
                                                     Role=g.First().Role,
                                                  
                                   };

                    foreach(var qs in querySec)
                    {
                        beData.SectionListWithClass.Add(qs);
                    }
                }
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
        
        public ResponeValues GetClassSetupAutoOrderNo(int UserId, int EntityId,int? For)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.Add("@OrderNo", System.Data.SqlDbType.Int);
            cmd.CommandText = "usp_ClassSetupAutoOrderNo";
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@For", For);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[2].Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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
