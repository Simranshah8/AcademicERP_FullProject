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
    public class HealthCampaignDB
    {
        DataAccessLayer1 dal = null;
        public HealthCampaignDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveHealthCampaign(BE.Infirmary.HealthCampaign healthCampaign, int userId)
        {

            var resVal = new ResponeValues();
            
            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InfirmarySaveHealthCampaign";

            DataTable examiners = new DataTable();
            examiners.Columns.Add("Id");
            foreach(var examiner in healthCampaign.ExaminerIds)
            {
                examiners.Rows.Add(examiner);
            }

            DataTable vaccines = new DataTable();
            vaccines.Columns.Add("Id");
            foreach (var vaccine in healthCampaign.VaccineIds)
            {
                vaccines.Rows.Add(vaccine);
            }

            DataTable tests = new DataTable();
            tests.Columns.Add("Id");
            foreach (var test in healthCampaign.TestIds)
            {
                tests.Rows.Add(test);
            }


            DataTable diseases = new DataTable();
            diseases.Columns.Add("Id");
            foreach (var disease in healthCampaign.DiseaseIds)
            {
                diseases.Rows.Add(disease);
            }

            DataTable cs = new DataTable();
            cs.Columns.Add("ClassId");
            cs.Columns.Add("SectionId");

            foreach (var classSection in healthCampaign.ClassSectionIds)
            {
                cs.Rows.Add(classSection.ClassId,classSection.SectionId);
            }

            cmd.Parameters.Clear();
            if(healthCampaign.HealthCampaignId!=null|| healthCampaign.HealthCampaignId!=0)
                cmd.Parameters.AddWithValue("@Id", healthCampaign.HealthCampaignId);
            cmd.Parameters.AddWithValue("@Name", healthCampaign.HealthCampaignName);
            cmd.Parameters.AddWithValue("@Description", healthCampaign.Description);
            cmd.Parameters.AddWithValue("@FromDate", healthCampaign.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", healthCampaign.ToDate);
            cmd.Parameters.AddWithValue("@Organizer", healthCampaign.Organizer);
            cmd.Parameters.AddWithValue("@IsVaccination", healthCampaign.IsVaccination);

            cmd.Parameters.AddWithValue("@udt_diseases", diseases);
            cmd.Parameters.AddWithValue("@udt_vaccines", vaccines);
            cmd.Parameters.AddWithValue("@udt_examiners", examiners);
            cmd.Parameters.AddWithValue("@udt_classSections", cs);
            cmd.Parameters.AddWithValue("@udt_tests", tests);




            // Parameters Common for Every Table

            cmd.Parameters.AddWithValue("@CreatedBy", healthCampaign.CUserId);
            cmd.Parameters.AddWithValue("@LogDateTime", healthCampaign.LogDateTime);
            int tmpParamIdx = DAHelper.AddOutputParams(cmd);

            

            DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);

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

        public BE.Infirmary.HealthCampaign getHealthCampaignById(int healthCampaignId)
        {

            var healthCampaign = new BE.Infirmary.HealthCampaign();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetHealthCampaignById";
                cmd.Parameters.AddWithValue("@HealthCampaignId", healthCampaignId);
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    healthCampaign.HealthCampaignId = reader.GetInt32(0);
                    healthCampaign.HealthCampaignName = reader.GetString(1).Trim();
                    healthCampaign.Description = reader.GetString(2).Trim();
                    healthCampaign.FromDate = reader.GetDateTime(3);
                    healthCampaign.ToDate = reader.GetDateTime(4);
                    healthCampaign.Organizer = reader.GetString(5).Trim();
                    healthCampaign.IsVaccination = reader.GetBoolean(6);

                    
                    // additional info like created,modified time,by etc.. if needed
                    healthCampaign.IsSuccess = true;
                    healthCampaign.ResponseMSG = "HealthCampaign Fetched Succesfully";

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                healthCampaign.IsSuccess = false;
                healthCampaign.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            if(healthCampaign.IsSuccess) fetchDetails(healthCampaign);
            return healthCampaign;
        }

        private void fetchDetails(BE.Infirmary.HealthCampaign healthCampaign)
        {
            try
            {
                healthCampaign.ExaminerIds = new List<int>();
                healthCampaign.DiseaseIds = new List<int>();
                healthCampaign.VaccineIds = new List<int>();
                healthCampaign.ClassSectionIds = new BE.Infirmary.ClassSectionCollections();
                healthCampaign.TestIds = new List<int>();
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HealthCampaignId", healthCampaign.HealthCampaignId);


                cmd.CommandText = "usp_InfirmaryGetHealthCampaignExaminers";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    healthCampaign.ExaminerIds.Add(reader.GetInt32(1));
                }
                reader.Close();

                cmd.CommandText = "usp_InfirmaryGetHealthCampaignDiseases";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    healthCampaign.DiseaseIds.Add(reader.GetInt32(1));
                }
                reader.Close();

                cmd.CommandText = "usp_InfirmaryGetHealthCampaignVaccines";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    healthCampaign.VaccineIds.Add(reader.GetInt32(1));
                }
                reader.Close();

                cmd.CommandText = "usp_InfirmaryGetHealthCampaignTests";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    healthCampaign.TestIds.Add(reader.GetInt32(1));
                }
                reader.Close();

                cmd.CommandText = "usp_InfirmaryGetHealthCampaignClassSections";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var classSection = new BE.Infirmary.ClassSection();
                    classSection.ClassId = reader.GetInt32(1);
                    if(!reader.IsDBNull(2))
                        classSection.SectionId = reader.GetInt32(2);

                    healthCampaign.ClassSectionIds.Add(classSection);
                }
                reader.Close();
            }
            catch (Exception ee)
            {
                healthCampaign.IsSuccess = false;
                healthCampaign.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        public BE.Infirmary.HealthCampaign getHealthCampaignByIdTmp(int healthCampaignId)
        {

            var healthCampaign = new BE.Infirmary.HealthCampaign();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetHealthCampaignById";
                cmd.Parameters.AddWithValue("@HealthCampaignId", healthCampaignId);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    healthCampaign.HealthCampaignId = reader.GetInt32(0);
                    healthCampaign.HealthCampaignName = reader.GetString(1).Trim();
                    healthCampaign.Description = reader.GetString(2).Trim();
                    healthCampaign.FromDate = reader.GetDateTime(3);
                    healthCampaign.ToDate = reader.GetDateTime(4);
                    healthCampaign.Organizer = reader.GetString(5).Trim();
                    healthCampaign.IsVaccination = reader.GetBoolean(6);

                    // additional info like created,modified time,by etc.. if needed
                    healthCampaign.IsSuccess = true;
                    healthCampaign.ResponseMSG = "HealthCampaign Fetched Succesfully";

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                healthCampaign.IsSuccess = false;
                healthCampaign.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return healthCampaign;
        }

        public ResponeValues deleteHealthCampaignById(int healthCampaignId, int userId)
        {
            var resVal = new ResponeValues();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryDeleteHealthCampaignById";
                cmd.Parameters.AddWithValue("@HealthCampaignId", healthCampaignId);
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

        public BE.Infirmary.HealthCampaignCollections getAllHealthCampaigns()
        {
            var healthCampaigns = new BE.Infirmary.HealthCampaignCollections();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllHealthCampaigns";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infirmary.HealthCampaign healthCampaign = new BE.Infirmary.HealthCampaign();
                    healthCampaign.HealthCampaignId = reader.GetInt32(0);
                    healthCampaign.HealthCampaignName = reader.GetString(1).Trim();
                    healthCampaign.Description = reader.GetString(2).Trim();
                    healthCampaign.FromDate = reader.GetDateTime(3);
                    healthCampaign.ToDate = reader.GetDateTime(4);
                    healthCampaign.Organizer = reader.GetString(5).Trim();
                    healthCampaign.IsVaccination = reader.GetBoolean(6);
                    /*healthCampaign.CUserId = reader.GetInt32(7);
                    healthCampaign.LogDateTime = reader.GetDateTime(8);*/
                    /*healthCampaign.ModifiedBy = reader.GetInt32(9);*/
                    healthCampaign.IsSuccess = true;
                    healthCampaign.ResponseMSG = GLOBALMSG.SUCCESS;

                    healthCampaigns.Add(healthCampaign);
                    healthCampaigns.IsSuccess = true;
                    healthCampaigns.ResponseMSG = GLOBALMSG.SUCCESS;
                }
                reader.Close();

            }
            catch (Exception ee)
            {
                healthCampaigns.IsSuccess = false;
                healthCampaigns.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            if (healthCampaigns.Count == 0)
            {
                healthCampaigns.IsSuccess = false;
                healthCampaigns.ResponseMSG = "No healthCampaigns Found";
            }
            return healthCampaigns;
        }


        public BE.Infirmary.ClassSectionCollections getAllClassSection()
        {
            var classSections = new BE.Infirmary.ClassSectionCollections();

            classSections.IsSuccess = true;
            classSections.ResponseMSG = "Fetched All class-section Successfully";

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryHealthCampaignGetAllClassSection";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var classSection = new BE.Infirmary.ClassSection();
                    classSection.ClassId = reader.GetInt32(0);
                    classSection.ClassName = reader.GetString(1).Trim();
                    if(!reader.IsDBNull(2))
                        classSection.SectionId = reader.GetInt32(2);
                    if (!reader.IsDBNull(3))
                        classSection.SectionName = reader.GetString(3).Trim();

                    classSection.IsSuccess = true;
                    classSection.ResponseMSG = GLOBALMSG.SUCCESS;
                    classSections.Add(classSection);

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                classSections.IsSuccess = false;
                classSections.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            /*if (diseases.Count == 0)
            {
                diseases.IsSuccess = false;
                diseases.ResponseMSG = "No diseases Found";
            }
            else
            {
                diseases.IsSuccess = true;
                diseases.ResponseMSG = "Fetched Diseases Successfully";
            }*/
            return classSections;
        }
    }
}