using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
using System.Data;
using PivotalERP.Global.Helpers;

namespace AcademicLib.DA.Infirmary
{
    public class GeneralCheckupDB
    {
        DataAccessLayer1 dal = null;
        public GeneralCheckupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveGeneralCheckup(BE.Infirmary.GeneralCheckup GeneralCheckup, int userId)
        {

            var resVal = new ResponeValues();
            
            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InfirmarySaveGeneralCheckup";

            DataTable examiners = new DataTable();
            examiners.Columns.Add("Id");
            foreach(var examiner in GeneralCheckup.ExaminerIds)
            {
                examiners.Rows.Add(examiner);
            }

            DataTable vaccines = new DataTable();
            vaccines.Columns.Add("Id");
            foreach (var vaccine in GeneralCheckup.VaccineIds)
            {
                vaccines.Rows.Add(vaccine);
            }

            DataTable tests = new DataTable();
            tests.Columns.Add("Id");
            foreach (var test in GeneralCheckup.TestIds)
            {
                tests.Rows.Add(test);
            }


            DataTable diseases = new DataTable();
            diseases.Columns.Add("Id");
            foreach (var disease in GeneralCheckup.DiseaseIds)
            {
                diseases.Rows.Add(disease);
            }

            DataTable cs = new DataTable();
            cs.Columns.Add("ClassId");
            cs.Columns.Add("SectionId");

            foreach (var classSection in GeneralCheckup.ClassSectionIds)
            {
                cs.Rows.Add(classSection.ClassId,classSection.SectionId);
            }

            cmd.Parameters.Clear();
            if(GeneralCheckup.GeneralCheckupId!=null)
                cmd.Parameters.AddWithValue("@Id", GeneralCheckup.GeneralCheckupId);
            cmd.Parameters.AddWithValue("@Name", GeneralCheckup.GeneralCheckupName);
            cmd.Parameters.AddWithValue("@Description", GeneralCheckup.Description);
            cmd.Parameters.AddWithValue("@FromDate", GeneralCheckup.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", GeneralCheckup.ToDate);
            cmd.Parameters.AddWithValue("@Month", GeneralCheckup.Month);
            cmd.Parameters.AddWithValue("@IsVaccination", GeneralCheckup.IsVaccination);

            cmd.Parameters.AddWithValue("@udt_diseases", diseases);
            cmd.Parameters.AddWithValue("@udt_vaccines", vaccines);
            cmd.Parameters.AddWithValue("@udt_examiners", examiners);
            cmd.Parameters.AddWithValue("@udt_classSections", cs);
            cmd.Parameters.AddWithValue("@udt_tests", tests);




            // Parameters Common for Every Table

            cmd.Parameters.AddWithValue("@CreatedBy", GeneralCheckup.CUserId);
            cmd.Parameters.AddWithValue("@LogDateTime", GeneralCheckup.LogDateTime);
            int tmpParamIdx = DAHelper.AddOutputParams(cmd);

            try
            {
                cmd.ExecuteNonQuery();
                DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);
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
             
            
            dal.CloseConnection();

            return resVal;
        }

        public BE.Infirmary.GeneralCheckup getGeneralCheckupById(int GeneralCheckupId)
        {

            var GeneralCheckup = new BE.Infirmary.GeneralCheckup();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetGeneralCheckupById";
                cmd.Parameters.AddWithValue("@GeneralCheckupId", GeneralCheckupId);
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    GeneralCheckup.GeneralCheckupId = reader.GetInt32(0);
                    GeneralCheckup.GeneralCheckupName = reader.GetString(1).Trim();
                    GeneralCheckup.Description = reader.GetString(2).Trim();
                    GeneralCheckup.FromDate = reader.GetDateTime(3);
                    GeneralCheckup.ToDate = reader.GetDateTime(4);
                    GeneralCheckup.Month = reader.GetInt32(5);
                    GeneralCheckup.IsVaccination = reader.GetBoolean(6);
                    GeneralCheckup.ExaminerIds = new List<int>();
                    GeneralCheckup.DiseaseIds = new List<int>();
                    GeneralCheckup.VaccineIds = new List<int>();
                    GeneralCheckup.ClassSectionIds = new BE.Infirmary.ClassSectionCollections();
                    GeneralCheckup.TestIds = new List<int>();

                    reader.NextResult();
                    while (reader.Read())
                    {
                        GeneralCheckup.ExaminerIds.Add(reader.GetInt32(1));
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        GeneralCheckup.DiseaseIds.Add(reader.GetInt32(1));
                    }
                    
                    reader.NextResult();
                    while (reader.Read())
                    {
                        GeneralCheckup.VaccineIds.Add(reader.GetInt32(1));
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        GeneralCheckup.TestIds.Add(reader.GetInt32(1));
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        var classSection = new BE.Infirmary.ClassSection();
                        classSection.ClassId = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            classSection.SectionId = reader.GetInt32(2);

                        GeneralCheckup.ClassSectionIds.Add(classSection);
                    }
                    // additional info like created,modified time,by etc.. if needed
                    GeneralCheckup.IsSuccess = true;
                    GeneralCheckup.ResponseMSG = "GeneralCheckup Fetched Succesfully";

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                GeneralCheckup.IsSuccess = false;
                GeneralCheckup.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return GeneralCheckup;
        }

        public ResponeValues deleteGeneralCheckupById(int GeneralCheckupId, int userId)
        {
            var resVal = new ResponeValues();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryDeleteGeneralCheckupById";
                cmd.Parameters.AddWithValue("@GeneralCheckupId", GeneralCheckupId);
                cmd.Parameters.AddWithValue("@UserId", userId);


                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

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

        public BE.Infirmary.GeneralCheckupCollections getAllGeneralCheckups()
        {
            var GeneralCheckups = new BE.Infirmary.GeneralCheckupCollections();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllGeneralCheckups";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infirmary.GeneralCheckup GeneralCheckup = new BE.Infirmary.GeneralCheckup();
                    GeneralCheckup.GeneralCheckupId = reader.GetInt32(0);
                    GeneralCheckup.GeneralCheckupName = reader.GetString(1).Trim();
                    GeneralCheckup.Description = reader.GetString(2).Trim();
                    GeneralCheckup.FromDate = reader.GetDateTime(3);
                    GeneralCheckup.ToDate = reader.GetDateTime(4);
                    GeneralCheckup.Month = reader.GetInt32(5);
                    GeneralCheckup.IsVaccination = reader.GetBoolean(6);
                    /*GeneralCheckup.CUserId = reader.GetInt32(7);
                    GeneralCheckup.LogDateTime = reader.GetDateTime(8);*/
                    /*GeneralCheckup.ModifiedBy = reader.GetInt32(9);*/
                    GeneralCheckup.IsSuccess = true;
                    GeneralCheckup.ResponseMSG = GLOBALMSG.SUCCESS;

                    GeneralCheckups.Add(GeneralCheckup);
                    GeneralCheckups.IsSuccess = true;
                    GeneralCheckups.ResponseMSG = GLOBALMSG.SUCCESS;
                }
                reader.Close();

            }
            catch (Exception ee)
            {
                GeneralCheckups.IsSuccess = false;
                GeneralCheckups.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            if (GeneralCheckups.Count == 0)
            {
                GeneralCheckups.IsSuccess = false;
                GeneralCheckups.ResponseMSG = "No GeneralCheckups Found";
            }
            return GeneralCheckups;
        }
    }
}