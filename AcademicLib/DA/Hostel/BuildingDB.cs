using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Hostel
{
    internal class BuildingDB
    {
        DataAccessLayer1 dal = null;
        public BuildingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Hostel.Building beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Location", beData.Location);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateBuilding";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddBuilding";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@BuildingNo", beData.BuildingNo);
            cmd.Parameters.AddWithValue("@BuildingTypeId", beData.BuildingTypeId);
            cmd.Parameters.AddWithValue("@OtherBuildingType", beData.OtherBuildingType);
            cmd.Parameters.AddWithValue("@NoOfFloor", beData.NoOfFloor);
            cmd.Parameters.AddWithValue("@OverallCondition", beData.OverallCondition);
            cmd.Parameters.AddWithValue("@NoOfClassRooms", beData.NoOfClassRooms);
            cmd.Parameters.AddWithValue("@NoOfOtherRooms", beData.NoOfOtherRooms);
            cmd.Parameters.AddWithValue("@ConstructionDate", beData.ConstructionDate);
            cmd.Parameters.AddWithValue("@StructureType", beData.StructureType);
            cmd.Parameters.AddWithValue("@OtherStructureType", beData.OtherStructureType);
            cmd.Parameters.AddWithValue("@RoofType", beData.RoofType);
            cmd.Parameters.AddWithValue("@OtherRoofType", beData.OtherRoofType);
            cmd.Parameters.AddWithValue("@DamageGrade", beData.DamageGrade);
            cmd.Parameters.AddWithValue("@InfrastructureType", beData.InfrastructureType);
            cmd.Parameters.AddWithValue("@FundingSources", beData.FundingSources);
            cmd.Parameters.AddWithValue("@InterventionType", beData.InterventionType);
            cmd.Parameters.AddWithValue("@IsApprovedDesign", beData.IsApprovedDesign);
            cmd.Parameters.AddWithValue("@IsCompletionCertificate", beData.IsCompletionCertificate);
            cmd.Parameters.AddWithValue("@CompletionStatus", beData.CompletionStatus);
            cmd.Parameters.AddWithValue("@CompletionDate", beData.CompletionDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@Budget", beData.Budget);
            cmd.Parameters.AddWithValue("@BoysToiletNo", beData.BoysToiletNo);
            cmd.Parameters.AddWithValue("@GirlsToiletNo", beData.GirlsToiletNo);
            cmd.Parameters.AddWithValue("@IsToiletFunctional", beData.IsToiletFunctional);
            cmd.Parameters.AddWithValue("@FacilityNotFunctioning", beData.FacilityNotFunctioning);
            cmd.Parameters.AddWithValue("@areaCoveredByBuilding ", beData.areaCoveredByBuilding);
            cmd.Parameters.AddWithValue("@areaCoveredByAllRooms ", beData.areaCoveredByAllRooms);
            cmd.Parameters.AddWithValue("@ownershipOfBuilding", beData.ownershipOfBuilding);
            cmd.Parameters.AddWithValue("@hasInternetConnection", beData.hasInternetConnection);
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveBuildingFacilitiesDetails(beData.CUserId, resVal.RId, beData.BuildingFacilitiesColl);
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
        //Field Add by PRashant
        private void SaveBuildingFacilitiesDetails(int UserId, int BuildingId, AcademicLib.BE.Hostel.BuildingFacilitiesCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || BuildingId == 0)
                return;

            foreach (BE.Hostel.BuildingFacilities beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddBuildingFacilitiesDetails";
                cmd.ExecuteNonQuery();
            }

        }

        public AcademicLib.BE.Hostel.BuildingCollections getAllBuilding(int UserId, int EntityId)
        {
            AcademicLib.BE.Hostel.BuildingCollections dataColl = new AcademicLib.BE.Hostel.BuildingCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllBuilding";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.Building beData = new AcademicLib.BE.Hostel.Building();
                    beData.BuildingId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Location = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.BuildingNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.NoOfFloor = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ConstructionDate_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.StructureType = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.RoofType = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.DamageGrade = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Budget = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.BoysToiletNo = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.GirlsToiletNo = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.BuildingType = reader.GetString(15);
                    //Add Field
                    if (!(reader[16] is DBNull)) beData.OtherBuildingType = reader.GetString(16);

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
        public AcademicLib.BE.Hostel.Building getBuildingById(int UserId, int EntityId, int BuildingId)
        {
            AcademicLib.BE.Hostel.Building beData = new AcademicLib.BE.Hostel.Building();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetBuildingById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Hostel.Building();
                    beData.BuildingId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Location = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ImagePath = reader.GetString(3);
                    //Field Add by PRashant
                    if (!(reader[4] is DBNull)) beData.BuildingNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.BuildingTypeId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.OtherBuildingType = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.NoOfFloor = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.OverallCondition = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.ConstructionDate = Convert.ToDateTime(reader[11]);
                    if (!(reader[12] is DBNull)) beData.StructureType = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.OtherStructureType = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.RoofType = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.OtherRoofType = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.DamageGrade = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.InfrastructureType = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.FundingSources = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.InterventionType = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.IsApprovedDesign = Convert.ToBoolean(reader[20]);
                    if (!(reader[21] is DBNull)) beData.IsCompletionCertificate = Convert.ToBoolean(reader[21]);
                    if (!(reader[22] is DBNull)) beData.CompletionStatus = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.CompletionDate = Convert.ToDateTime(reader[23]);
                    if (!(reader[24] is DBNull)) beData.Remarks = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.Budget = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is DBNull)) beData.BoysToiletNo = reader.GetInt32(26);
                    if (!(reader[27] is DBNull)) beData.GirlsToiletNo = reader.GetInt32(27);
                    if (!(reader[28] is DBNull)) beData.IsToiletFunctional = Convert.ToBoolean(reader[28]);
                    if (!(reader[29] is DBNull)) beData.FacilityNotFunctioning = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.ConstructionDate_BS = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.CompletionData_BS = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.BuildingType = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.areaCoveredByBuilding = Convert.ToDouble(reader[33]);
                    if (!(reader[34] is DBNull)) beData.areaCoveredByAllRooms = Convert.ToDouble(reader[34]);
                    if (!(reader[35] is DBNull)) beData.ownershipOfBuilding = Convert.ToBoolean(reader[35]);
                    if (!(reader[36] is DBNull)) beData.hasInternetConnection = Convert.ToBoolean(reader[36]);
                }
                reader.NextResult();
                beData.BuildingFacilitiesColl = new BE.Hostel.BuildingFacilitiesCollections();
                while (reader.Read())
                {
                    BE.Hostel.BuildingFacilities det1 = new BE.Hostel.BuildingFacilities();
                    if (!(reader[0] is DBNull)) det1.BuildingId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
                    beData.BuildingFacilitiesColl.Add(det1);
                }
                //End
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
        public ResponeValues DeleteById(int UserId, int EntityId, int BuildingId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.CommandText = "usp_DelBuildingById";
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
