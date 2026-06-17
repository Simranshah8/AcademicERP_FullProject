using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA.Health.Transaction
{
    internal class HealthCampaignDB
    {
        DataAccessLayer1 dal = null;
        public HealthCampaignDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicERP.BE.Health.Transaction.HealthCampaign beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CampaignName", beData.CampaignName);
            cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
            cmd.Parameters.AddWithValue("@OrganizedBy", beData.OrganizedBy);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHealthCampaign";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddHealthCampaign";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveHealth(beData.HealthCampaignColl, resVal.RId, beData.CUserId);
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

        private void SaveHealth(List<AcademicERP.BE.Health.Transaction.HealthCampaign_HC> dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@Designation", beData.Designation);
                cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.CommandText = "usp_AddHealth";

                cmd.ExecuteNonQuery();
            }
        }
        public AcademicERP.BE.Health.Transaction.HealthCampaignCollections getAllHealthCampaign(int UserId, int EntityId)
        {
            AcademicERP.BE.Health.Transaction.HealthCampaignCollections dataColl = new AcademicERP.BE.Health.Transaction.HealthCampaignCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
           
            cmd.CommandText = "usp_GetAllHealthCampaign";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.HealthCampaign beData = new AcademicERP.BE.Health.Transaction.HealthCampaign();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.CampaignName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ForDate= reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.OrganizedBy = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ForMiti = reader.GetString(4);
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

        public AcademicERP.BE.Health.Transaction.HealthCampaign getHealthCampaignById(int UserId, int EntityId, int TranId)
        {
            AcademicERP.BE.Health.Transaction.HealthCampaign beData = new AcademicERP.BE.Health.Transaction.HealthCampaign();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetHealthCampaignById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicERP.BE.Health.Transaction.HealthCampaign();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.CampaignName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ForDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.OrganizedBy = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.HealthCampaign_HC det = new AcademicERP.BE.Health.Transaction.HealthCampaign_HC();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Designation = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.ContactNo = reader.GetString(3);
                    beData.HealthCampaignColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelHealthCampaignById";
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


        public AcademicLib.RE.Health.HealthReport getHealthReport(int UserId, int? StudentId,int? EmployeeId)
        {
            AcademicLib.RE.Health.HealthReport retData = new AcademicLib.RE.Health.HealthReport();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetHealthReport";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.MonthlyTest beData = new AcademicERP.BE.Health.Transaction.MonthlyTest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Month = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Teeth = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Nail = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Cleanliness = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ForDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.MonthName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ForMiti = reader.GetString(7);
                    retData.MonthlyTestColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Extension = reader.GetString(4);
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    retData.MonthlyTestColl.Find(p1 => p1.TranId == TranId).AttachMonTesColl.Add(doc);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.HealthGrowth beData = new AcademicERP.BE.Health.Transaction.HealthGrowth();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Height = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Weight = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.TestDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.TestBy = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.TestMiti = reader.GetString(6);
                    retData.HealthGrowthColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Extension = reader.GetString(4);
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    retData.HealthGrowthColl.Find(p1 => p1.TranId == TranId).AttachHelGroColl.Add(doc);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.StoolTest beData = new AcademicERP.BE.Health.Transaction.StoolTest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Color = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Mucus = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Puscell = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RBC = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Cyst = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Ova = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Others = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TestMiti = reader.GetString(9);
                    retData.StoolTestColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Extension = reader.GetString(4);
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    retData.StoolTestColl.Find(p1 => p1.TranId == TranId).AttachStTeColl.Add(doc);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.UrineTest beData = new AcademicERP.BE.Health.Transaction.UrineTest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Color = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Protein = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Sugar = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Transparency = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.WBC = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RBC = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Others = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TestMiti = reader.GetString(9);
                    retData.UrineTestColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Extension = reader.GetString(4);
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    retData.UrineTestColl.Find(p1 => p1.TranId == TranId).AttachUrTeColl.Add(doc);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.GeneralHealth beData = new AcademicERP.BE.Health.Transaction.GeneralHealth();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ForDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.CampaignProgram = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Remarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ForMiti = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.CampaignName = reader.GetString(5);
                    retData.GeneralHealthColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    int TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Extension = reader.GetString(4);
                    if (!(reader[5] is DBNull)) doc.DocPath = reader.GetString(5);
                    retData.GeneralHealthColl.Find(p1 => p1.TranId == TranId).AttachGenHeColl.Add(doc);
                }
                reader.Close();
                retData.IsSuccess = true;
                retData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                retData.IsSuccess = false;
                retData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return retData;
        }
    }
}