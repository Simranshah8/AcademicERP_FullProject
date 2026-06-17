using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class SyllabusPlanDB
	{
		DataAccessLayer1 dal = null;
		public SyllabusPlanDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

        public ResponeValues SaveUpdate(BE.AppCMS.Creation.SyllabusPlan beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@ProgramId", beData.ProgramId);
            cmd.Parameters.AddWithValue("@NoOfSyllabus", beData.NoOfSyllabus);
            cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateSyllabusPlan";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddSyllabusPlan";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
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
                    SaveLessonPlanDet(beData.CUserId, resVal.RId, beData.DetailsColl);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            finally
            {
                this.dal.CloseConnection();
            }
            return resVal;
        }

        private void SaveLessonPlanDet(int UserId, int TranId, BE.AppCMS.Creation.SyllabusPlanDetailsCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;
            int num = 1;
            foreach (BE.AppCMS.Creation.SyllabusPlanDetails syllabusPlanDetails in beDataColl)
            {
                if (!string.IsNullOrEmpty(syllabusPlanDetails.SyllabusName))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@SNo", syllabusPlanDetails.SNo);
                    cmd.Parameters.AddWithValue("@SyllabusName", syllabusPlanDetails.SyllabusName);
                    cmd.Parameters.AddWithValue("@SyllabusId", syllabusPlanDetails.SyllabusId);
                    cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddSyllabusPlanDetails";
                    cmd.ExecuteNonQuery();
                    syllabusPlanDetails.SyllabusId = Convert.ToInt32(cmd.Parameters[4].Value);
                    SaveSyllabusTopic(UserId, syllabusPlanDetails.SyllabusId, syllabusPlanDetails.SNo, syllabusPlanDetails.TopicColl);
                    ++num;
                }
            }
        }

        private void SaveSyllabusTopic(int UserId, int SyllabusId, int SyllabusSNo, BE.AppCMS.Creation.SyllabusTopicCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || SyllabusId == 0)
                return;
            int num = 1;
            foreach (BE.AppCMS.Creation.SyllabusTopic syllabusTopic in beDataColl)
            {
                if (!string.IsNullOrEmpty(syllabusTopic.TopicName))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@SyllabusId", SyllabusId);
                    cmd.Parameters.AddWithValue("@SNo", syllabusTopic.SNo);
                    cmd.Parameters.AddWithValue("@SyllabusSNo", SyllabusSNo);
                    cmd.Parameters.AddWithValue("@TopicName", syllabusTopic.TopicName);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddSyllabusTopic";
                    cmd.ExecuteNonQuery();
                    ++num;
                }
            }
        }

        public BE.AppCMS.Creation.SyllabusPlanCollections getAllSyllabus(int UserId, int EntityId, string BranchCode="")
        {
            BE.AppCMS.Creation.SyllabusPlanCollections dataColl = new BE.AppCMS.Creation.SyllabusPlanCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_getAllSyllabusPlan";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.AppCMS.Creation.SyllabusPlan beData = new BE.AppCMS.Creation.SyllabusPlan();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Program = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.NoOfSyllabus = reader.GetInt32(3);
                    beData.DetailsColl = new BE.AppCMS.Creation.SyllabusPlanDetailsCollections();
                    dataColl.Add(beData);
                };
                reader.NextResult();
                while (reader.Read())
                {
                    BE.AppCMS.Creation.SyllabusPlanDetails det1 = new BE.AppCMS.Creation.SyllabusPlanDetails();
                    if (!(reader[0] is DBNull)) det1.SyllabusId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.TranId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.SyllabusName = reader.GetString(3);
                    det1.TopicColl = new BE.AppCMS.Creation.SyllabusTopicCollections();
                    dataColl.Find(p1 => p1.TranId == det1.TranId).DetailsColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.AppCMS.Creation.SyllabusTopic det1 = new BE.AppCMS.Creation.SyllabusTopic();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.SyllabusId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.SyllabusSNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.SNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.TopicName = reader.GetString(4);
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.SyllabusId == det1.SyllabusId).TopicColl.Add(det1);
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

        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelSyllabusById";
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

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


        public BE.AppCMS.Creation.SyllabusPlan getSyllabusPlanById(int UserId, int EntityId, int TranId)
        {
            BE.AppCMS.Creation.SyllabusPlan beData = new BE.AppCMS.Creation.SyllabusPlan();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetSyllabusPlanById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.AppCMS.Creation.SyllabusPlan();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ProgramId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfSyllabus = reader.GetInt32(3);
                }
                reader.NextResult();
                beData.DetailsColl = new BE.AppCMS.Creation.SyllabusPlanDetailsCollections();
                while (reader.Read())
                {
                    BE.AppCMS.Creation.SyllabusPlanDetails det1 = new BE.AppCMS.Creation.SyllabusPlanDetails();
                    if (!(reader[0] is DBNull)) det1.SyllabusId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.TranId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.SyllabusName = reader.GetString(3);

                    det1.TopicColl = new BE.AppCMS.Creation.SyllabusTopicCollections();
                    beData.DetailsColl.Add(det1);

                }

                reader.NextResult();
                //beData.TopicColl = new BE.AppCMS.Creation.SyllabusTopicCollections();
                while (reader.Read())
                {
                    BE.AppCMS.Creation.SyllabusTopic det1 = new BE.AppCMS.Creation.SyllabusTopic();
                    if (!(reader[0] is DBNull)) det1.SyllabusId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.SyllabusSNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.TopicName = reader.GetString(3);
                    foreach (var details in beData.DetailsColl)
                    {
                        if (details.SyllabusId == det1.SyllabusId)
                        {
                            details.TopicColl.Add(det1);
                            break;
                        }
                    }

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
    }

}

