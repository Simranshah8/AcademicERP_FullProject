using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
 
using PivotalERP.Global.Helpers;

namespace AcademicLib.DA.Infirmary
{
    public class DiseaseDB
    {
        DataAccessLayer1 dal = null;
        public DiseaseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveDisease(BE.Infirmary.Disease disease, int userId)
        {

            var resVal = new ResponeValues();
            
            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InfirmarySaveDisease";
            

            cmd.Parameters.Clear();
            if(disease.DiseaseId!=null) 
                cmd.Parameters.AddWithValue("@Id", disease.DiseaseId);
            cmd.Parameters.AddWithValue("@Name", disease.DiseaseName);
            cmd.Parameters.AddWithValue("@Description", disease.Description);
            cmd.Parameters.AddWithValue("@Severity", disease.Severity);
            cmd.Parameters.AddWithValue("@OrderNo", disease.OrderNo);

            cmd.Parameters.AddWithValue("@CreatedBy", disease.CUserId);
            cmd.Parameters.AddWithValue("@LogDateTime", disease.LogDateTime);

            int tmpParamIdx = DAHelper.AddOutputParams(cmd);

            try
            {
                cmd.ExecuteNonQuery();

                DAHelper.GetOutputParams(cmd,tmpParamIdx,resVal);

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

        public BE.Infirmary.Disease getDiseaseById(int diseaseId)
        {

            var disease = new BE.Infirmary.Disease();
            disease.IsSuccess = false;
            disease.ResponseMSG = "Disease Doesnt Exist";
            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetDiseaseById";
                cmd.Parameters.AddWithValue("@DiseaseId", diseaseId);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    disease.DiseaseId = reader.GetInt32(0);
                    disease.DiseaseName = reader.GetString(1).Trim();
                    disease.Description = reader.GetString(2).Trim();
                    disease.Severity = reader.GetInt32(3);
                    if(!reader.IsDBNull(4)) 
                        disease.OrderNo = reader.GetInt32(4);
                    disease.IsSuccess = true;
                    disease.ResponseMSG = "Disease Fetched Succesfully";
                }
                reader.Close();

            }
            catch (Exception ee)
            {
                disease.IsSuccess = false;
                disease.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return disease;
        }

        public bool foundSimilarDisease(string name, int severity)
        {
            bool found = false;
            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllSimilarDiseases";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Severity", severity);
                cmd.Parameters.Add("@FoundSimilar", System.Data.SqlDbType.Int);
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    found = Convert.ToBoolean(cmd.Parameters[2].Value);

            }
            catch (Exception ee)
            {
                found = false;
            }
            finally
            {
                dal.CloseConnection();
            }

            return found;
        }

        public BE.Infirmary.DiseaseSeverityCollections getHealthIssueSeverities()
        {
            var severities = new BE.Infirmary.DiseaseSeverityCollections();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllDiseaseSeverities";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infirmary.DiseaseSeverity severity = new BE.Infirmary.DiseaseSeverity();
                    severity.SeverityId = reader.GetInt32(0);
                    severity.Severity = reader.GetString(1).Trim();
                   
                    severity.IsSuccess = true;
                    severity.ResponseMSG = GLOBALMSG.SUCCESS;
                    severities.Add(severity);

                }
                reader.Close();

            }
            catch (Exception ee)
            {
                severities.IsSuccess = false;
                severities.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            if (severities.Count == 0)
            {
                severities.IsSuccess = false;
                severities.ResponseMSG = "No severities Found";
            }
            else
            {
                severities.IsSuccess = true;
                severities.ResponseMSG = "Fetched Severities Successfully";
            }
            return severities;
        }

        public ResponeValues deleteDiseaseById(int diseaseId, int userId)
        {
            var resVal = new ResponeValues();

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryDeleteDiseaseById";
                cmd.Parameters.AddWithValue("@DiseaseId", diseaseId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                int tmpParamIdx = DAHelper.AddOutputParams(cmd);

                /*cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;*/


                cmd.ExecuteNonQuery();

                DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                {
                    // uncomment for debugging the issue
                    /*resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";*/
                    resVal.ResponseMSG = "Disease Cant be Deleted";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Disease Cant be Deleted";
                /*resVal.ResponseMSG = ee.Message;*/
            }
            finally
            {
                dal.CloseConnection();
            }

            return resVal;
        }

        public BE.Infirmary.DiseaseCollections getAllDiseases()
        {
            var diseases = new BE.Infirmary.DiseaseCollections();

            diseases.IsSuccess = true;
            diseases.ResponseMSG = "Fetched Diseases Successfully";

            try
            {
                dal.OpenConnection();

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_InfirmaryGetAllDiseases";


                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infirmary.Disease disease = new BE.Infirmary.Disease();
                    disease.DiseaseId = reader.GetInt32(0);
                    disease.DiseaseName = reader.GetString(1).Trim();
                    disease.Description = reader.GetString(2).Trim();
                    disease.Severity = reader.GetInt32(3);
                    if (!reader.IsDBNull(4))
                        disease.OrderNo = reader.GetInt32(4);
                    disease.IsSuccess = true;
                    disease.ResponseMSG = GLOBALMSG.SUCCESS;
                    diseases.Add(disease);
                    
                }
                reader.Close();

            }
            catch (Exception ee)
            {
                diseases.IsSuccess = false;
                diseases.ResponseMSG = ee.Message;
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
            return diseases;
        }
    }
}