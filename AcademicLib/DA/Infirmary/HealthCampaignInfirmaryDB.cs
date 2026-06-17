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
    public class HCInfirmaryDB
    {
        DataAccessLayer1 dal = null;
        public HCInfirmaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveHCInfirmary(BE.Infirmary.HCInfirmary hcInfirmary, int userId)
        {

            var resVal = new ResponeValues();
            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InfirmarySaveHCInfirmary";

            DataTable examiners = new DataTable();
            examiners.Columns.Add("Id");
            foreach(var examiner in hcInfirmary.ExaminerIds)
            {
                examiners.Rows.Add(examiner);
            }

            DataTable studentReports = new DataTable();
            studentReports.Columns.Add("StudentId");
            studentReports.Columns.Add("Remarks");

            foreach (var studentReport in hcInfirmary.StudentTestDetails)
            {
                studentReports.Rows.Add(studentReport.StudentId,studentReport.Remarks);
            }


            DataTable studentResults = new DataTable();
            studentResults.Columns.Add("StudentId");
            studentResults.Columns.Add("TestId");
            studentResults.Columns.Add("Value");


            foreach (var studentReport in hcInfirmary.StudentTestDetails)
            {
                foreach(var result in studentReport.Results)
                    studentResults.Rows.Add(studentReport.StudentId, result.Id ,result.Value);
            }


            cmd.Parameters.Clear();
            if(hcInfirmary.HCInfirmaryId!=0)
                cmd.Parameters.AddWithValue("@Id", hcInfirmary.HCInfirmaryId);

            cmd.Parameters.AddWithValue("@HealthCampaignId", hcInfirmary.HealthCampaignId);
            cmd.Parameters.AddWithValue("@ClassId", hcInfirmary.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", hcInfirmary.SectionId);
            cmd.Parameters.AddWithValue("@Remarks", hcInfirmary.Remarks);
            cmd.Parameters.AddWithValue("@OnDate", hcInfirmary.OnDate);
            cmd.Parameters.AddWithValue("@CreatedBy", hcInfirmary.CUserId);
            cmd.Parameters.AddWithValue("@LogDateTime", hcInfirmary.LogDateTime);

            cmd.Parameters.AddWithValue("@udt_examiners", examiners);
            cmd.Parameters.AddWithValue("@udt_studentsReports", studentReports);
            cmd.Parameters.AddWithValue("@udt_studentsResults", studentResults);


            int tmpParamIdx = DAHelper.AddOutputParams(cmd);

            /*DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);*/

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

        public List<int> getAllHCInfirmarySuccessfullStudents(int hCInfirmaryId)
        {
            var data = new List<int>(); 

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllHCInfirmarySuccessfullStudents";
                cmd.Parameters.AddWithValue("@Id", hCInfirmaryId);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int val =  reader.GetInt32(0);

                    data.Add(val);

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                
            }
            finally
            {
                dal.CloseConnection();
            }
            return data;
        }

        public BE.Infirmary.HCInfirmary getHCInfirmaryById(int healthCampaignId)
        {

            var hcInfirmary = new BE.Infirmary.HCInfirmary();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetHCInfirmaryById";
                cmd.Parameters.AddWithValue("@Id", healthCampaignId);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hcInfirmary.HCInfirmaryId = reader.GetInt32(0);
                    hcInfirmary.HealthCampaignId = reader.GetInt32(1);
                    hcInfirmary.ClassId = reader.GetInt32(2);
                    hcInfirmary.SectionId = reader.GetInt32(3);
                    hcInfirmary.Remarks = reader.GetString(4).Trim();
                    hcInfirmary.OnDate = reader.GetDateTime(5);
                    hcInfirmary.ExaminerIds = new List<int>();
                    hcInfirmary.StudentTestDetails = new List<BE.Infirmary.StudentTestResults>();

                    reader.NextResult();
                    var entryIdToResults = new Dictionary<int, BE.Infirmary.StudentTestResults>();
                    
                    while (reader.Read())
                    {
                        var stres = new BE.Infirmary.StudentTestResults();
                        int TestEntryId = reader.GetInt32(0);
                        stres.StudentId = reader.GetInt32(1);
                        stres.Remarks = reader.GetString(2).Trim();
                        stres.Results = new List<BE.Infirmary.TestResults>();
                        hcInfirmary.StudentTestDetails.Add(stres);

                        entryIdToResults[TestEntryId] = stres;
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        var stres = new BE.Infirmary.TestResults();
                        int TestEntryId = reader.GetInt32(0);
                        stres.Id = reader.GetInt32(1);
                        stres.Value = reader.GetString(2).Trim();
                        entryIdToResults[TestEntryId].Results.Add(stres);
                    }

                    reader.NextResult();
                    hcInfirmary.ExaminerIds = new List<int>();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        hcInfirmary.ExaminerIds.Add(id);
                    }

                    // additional info like created,modified time,by etc.. if needed
                    hcInfirmary.IsSuccess = true;
                    hcInfirmary.ResponseMSG = "HCInfirmary Fetched Succesfully";

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                hcInfirmary.IsSuccess = false;
                hcInfirmary.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return hcInfirmary;
        }

        public BE.Infirmary.HCInfirmary getHCInfirmaryByIdTmp(int healthCampaignId)
        {

            var healthCampaign = new BE.Infirmary.HCInfirmary();

            return healthCampaign;
        }

        public ResponeValues deleteHCInfirmaryById(int hcInfirmaryById, int userId)
        {
            var resVal = new ResponeValues();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryDeleteHCInfirmaryById";
                cmd.Parameters.AddWithValue("@HCInfirmaryId", hcInfirmaryById);
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

        public BE.Infirmary.HCInfirmaryInfoCollections getAllHCInfirmarys()
        {
            var healthCampaigns = new BE.Infirmary.HCInfirmaryInfoCollections();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllHCInfirmarys";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var healthCampaign = new BE.Infirmary.HCInfirmaryInfo();
                    
                    healthCampaign.HCInfirmaryId = reader.GetInt32(0);
                    healthCampaign.ClassSection = new BE.Infirmary.ClassSection();
                    healthCampaign.ClassSection.ClassId = reader.GetInt32(1);
                    if (!reader.IsDBNull(4)) healthCampaign.ClassSection.SectionId = reader.GetInt32(2);
                    healthCampaign.ClassSection.ClassName = reader.GetString(3).Trim();
                    if (!reader.IsDBNull(6)) healthCampaign.ClassSection.SectionName = reader.GetString(4).Trim();

                    healthCampaign.Remarks = reader.GetString(5).Trim();

                    healthCampaign.OnDate = reader.GetDateTime(6);
                    healthCampaign.HealthCampaignId = reader.GetInt32(7);
                    healthCampaign.DiseaseNames = reader.GetString(8).Trim();
                    healthCampaign.TotalStudents = reader.GetInt32(9);
                    healthCampaign.PresentStudents = reader.GetInt32(10);
                    healthCampaign.AbsentStudents = reader.GetInt32(11);


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
    }
}