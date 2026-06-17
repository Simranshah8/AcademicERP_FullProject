using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Infrastructure
{

	internal class GeneralInformationDB
	{
		DataAccessLayer1 dal = null;
		public GeneralInformationDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.Infrastructure.GeneralInformation beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@BranchId",beData.BranchId);
	cmd.Parameters.AddWithValue("@CompanyId",beData.CompanyId);
	cmd.Parameters.AddWithValue("@ProvinceId",beData.ProvinceId);
	cmd.Parameters.AddWithValue("@Province",beData.Province);
	cmd.Parameters.AddWithValue("@DistrictId",beData.DistrictId);
	cmd.Parameters.AddWithValue("@District",beData.District);
	cmd.Parameters.AddWithValue("@LocalLevelId",beData.LocalLevelId);
	cmd.Parameters.AddWithValue("@LocalLevel",beData.LocalLevel);
	cmd.Parameters.AddWithValue("@WardNo",beData.WardNo);
	cmd.Parameters.AddWithValue("@Tole",beData.Tole);
	cmd.Parameters.AddWithValue("@MajorLandmarks",beData.MajorLandmarks);
	cmd.Parameters.AddWithValue("@Latitude",beData.Latitude);
	cmd.Parameters.AddWithValue("@Longitude",beData.Longitude);
	cmd.Parameters.AddWithValue("@CampusChiefId",beData.CampusChiefId);
	cmd.Parameters.AddWithValue("@CCAppointmentDate",beData.CCAppointmentDate);
	cmd.Parameters.AddWithValue("@CampusITId",beData.CampusITId);
	cmd.Parameters.AddWithValue("@CampusITAppointmentDate",beData.CampusITAppointmentDate);
	cmd.Parameters.AddWithValue("@IsLandOwnershipCertificate",beData.IsLandOwnershipCertificate);
	cmd.Parameters.AddWithValue("@LandOwnershipTypeId",beData.LandOwnershipTypeId);
	cmd.Parameters.AddWithValue("@LandAreaSqm",beData.LandAreaSqm);
	cmd.Parameters.AddWithValue("@LandAreaRopani",beData.LandAreaRopani);
	cmd.Parameters.AddWithValue("@LandAreaBigha",beData.LandAreaBigha);
	cmd.Parameters.AddWithValue("@SiteOrientationId",beData.SiteOrientationId);
	cmd.Parameters.AddWithValue("@IsAllWeatherRoad",beData.IsAllWeatherRoad);
	cmd.Parameters.AddWithValue("@IsVehicleAccessibility",beData.IsVehicleAccessibility);
	cmd.Parameters.AddWithValue("@RoadTypeId",beData.RoadTypeId);
	cmd.Parameters.AddWithValue("@RoadWidth",beData.RoadWidth);
	cmd.Parameters.AddWithValue("@WalkingDistanceMeter",beData.WalkingDistanceMeter);
	cmd.Parameters.AddWithValue("@WalkingDistanceMin",beData.WalkingDistanceMin);
	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@TranId",beData.TranId);
	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateGeneralInformation";
	}
	else
	{
		cmd.Parameters[31].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddGeneralInformation";
	}
	cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
	cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
	cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
	cmd.Parameters[32].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[33].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[34].Direction = System.Data.ParameterDirection.Output;
	try
	{
		cmd.ExecuteNonQuery();
		if (!(cmd.Parameters[31].Value is DBNull))
			resVal.RId = Convert.ToInt32(cmd.Parameters[31].Value);

		if (!(cmd.Parameters[32].Value is DBNull))
			resVal.ResponseMSG = Convert.ToString(cmd.Parameters[32].Value);

		if (!(cmd.Parameters[33].Value is DBNull))
			resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[33].Value);

		if (!(cmd.Parameters[34].Value is DBNull))
			resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[34].Value);

		if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
			resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

	if (resVal.RId > 0 && resVal.IsSuccess)
			{
				SaveGeneralInfoFacilitiesDetails(beData.CUserId, resVal.RId, beData.GeneralInfoFacilitiesColl);
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

	


	public BE.Infrastructure.GeneralInformation getAllGeneralInformation(int UserId, int EntityId)
		{
			BE.Infrastructure.GeneralInformation beData = new BE.Infrastructure.GeneralInformation();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetGeneralInformation";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Infrastructure.GeneralInformation();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.CompanyId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ProvinceId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Province = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.DistrictId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.District = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.LocalLevelId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.LocalLevel = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.WardNo = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.Tole = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.MajorLandmarks = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.Latitude = Convert.ToDouble(reader[12]);
					if (!(reader[13] is DBNull)) beData.Longitude = Convert.ToDouble(reader[13]);
					if (!(reader[14] is DBNull)) beData.CampusChiefId = reader.GetInt32(14);
					if (!(reader[15] is DBNull)) beData.CCAppointmentDate = Convert.ToDateTime(reader[15]);
					if (!(reader[16] is DBNull)) beData.CampusITId = reader.GetInt32(16);
					if (!(reader[17] is DBNull)) beData.CampusITAppointmentDate = Convert.ToDateTime(reader[17]);
					if (!(reader[18] is DBNull)) beData.IsLandOwnershipCertificate = Convert.ToBoolean(reader[18]);
					if (!(reader[19] is DBNull)) beData.LandOwnershipTypeId = reader.GetInt32(19);
					if (!(reader[20] is DBNull)) beData.LandAreaSqm = Convert.ToDouble(reader[20]);
					if (!(reader[21] is DBNull)) beData.LandAreaRopani = Convert.ToDouble(reader[21]);
					if (!(reader[22] is DBNull)) beData.LandAreaBigha = Convert.ToDouble(reader[22]);
					if (!(reader[23] is DBNull)) beData.SiteOrientationId = reader.GetInt32(23);
					if (!(reader[24] is DBNull)) beData.IsAllWeatherRoad = Convert.ToBoolean(reader[24]);
					if (!(reader[25] is DBNull)) beData.IsVehicleAccessibility = Convert.ToBoolean(reader[25]);
					if (!(reader[26] is DBNull)) beData.RoadTypeId = reader.GetInt32(26);
					if (!(reader[27] is DBNull)) beData.RoadWidth = Convert.ToDouble(reader[27]);
					if (!(reader[28] is DBNull)) beData.WalkingDistanceMeter = Convert.ToDouble(reader[28]);
					if (!(reader[29] is DBNull)) beData.WalkingDistanceMin = Convert.ToDouble(reader[29]);
				}
				reader.NextResult();
				beData.GeneralInfoFacilitiesColl = new BE.Infrastructure.GeneralInfoFacilitiesCollections();
				while (reader.Read())
				{
					BE.Infrastructure.GeneralInfoFacilities det1 = new BE.Infrastructure.GeneralInfoFacilities();
					if (!(reader[0] is DBNull)) det1.FacilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);				
					if (!(reader[3] is DBNull)) det1.IsAvailable = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) det1.SourceName = reader.GetString(4);
					beData.GeneralInfoFacilitiesColl.Add(det1);
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

	private void SaveGeneralInfoFacilitiesDetails(int UserId, int TranId,BE.Infrastructure.GeneralInfoFacilitiesCollections beDataColl)
  {
	if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
		return;

	foreach (BE.Infrastructure.GeneralInfoFacilities beData in beDataColl)
	{
		System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
		cmd.Parameters.AddWithValue("@TranId",TranId);
	    cmd.Parameters.AddWithValue("@UserId", UserId);
		cmd.Parameters.AddWithValue("@SNo",beData.SNo);
		cmd.Parameters.AddWithValue("@Name",beData.Name);
		cmd.Parameters.AddWithValue("@IsAvailable",beData.IsAvailable);
		cmd.Parameters.AddWithValue("@SourceName",beData.SourceName);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.CommandText = "usp_AddGeneralInfoFacilitiesDetails";
		cmd.ExecuteNonQuery();
	}

}

public BE.Infrastructure.GeneralInformation getGeneralInformationById(int UserId, int EntityId, int TranId)
{
	BE.Infrastructure.GeneralInformation beData = new BE.Infrastructure.GeneralInformation();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@TranId", TranId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetGeneralInformationById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.Infrastructure.GeneralInformation();
			if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) beData.CompanyId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.ProvinceId = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.Province = reader.GetString(4);
			if (!(reader[5] is DBNull)) beData.DistrictId = reader.GetInt32(5);
			if (!(reader[6] is DBNull)) beData.District = reader.GetString(6);
			if (!(reader[7] is DBNull)) beData.LocalLevelId = reader.GetInt32(7);
			if (!(reader[8] is DBNull)) beData.LocalLevel = reader.GetString(8);
			if (!(reader[9] is DBNull)) beData.WardNo = reader.GetInt32(9);
			if (!(reader[10] is DBNull)) beData.Tole = reader.GetString(10);
			if (!(reader[11] is DBNull)) beData.MajorLandmarks = reader.GetString(11);
			if (!(reader[12] is DBNull)) beData.Latitude = Convert.ToDouble(reader[12]);
			if (!(reader[13] is DBNull)) beData.Longitude = Convert.ToDouble(reader[13]);
			if (!(reader[14] is DBNull)) beData.CampusChiefId = reader.GetInt32(14);
			if (!(reader[15] is DBNull)) beData.CCAppointmentDate = Convert.ToDateTime(reader[15]);
			if (!(reader[16] is DBNull)) beData.CampusITId = reader.GetInt32(16);
			if (!(reader[17] is DBNull)) beData.CampusITAppointmentDate = Convert.ToDateTime(reader[17]);
			if (!(reader[18] is DBNull)) beData.IsLandOwnershipCertificate = Convert.ToBoolean(reader[18]);
			if (!(reader[19] is DBNull)) beData.LandOwnershipTypeId = reader.GetInt32(19);
			if (!(reader[20] is DBNull)) beData.LandAreaSqm = Convert.ToDouble(reader[20]);
			if (!(reader[21] is DBNull)) beData.LandAreaRopani = Convert.ToDouble(reader[21]);
			if (!(reader[22] is DBNull)) beData.LandAreaBigha = Convert.ToDouble(reader[22]);
			if (!(reader[23] is DBNull)) beData.SiteOrientationId = reader.GetInt32(23);
			if (!(reader[24] is DBNull)) beData.IsAllWeatherRoad = Convert.ToBoolean(reader[24]);
			if (!(reader[25] is DBNull)) beData.IsVehicleAccessibility = Convert.ToBoolean(reader[25]);
			if (!(reader[26] is DBNull)) beData.RoadTypeId = reader.GetInt32(26);
			if (!(reader[27] is DBNull)) beData.RoadWidth = Convert.ToDouble(reader[27]);
			if (!(reader[28] is DBNull)) beData.WalkingDistanceMeter = Convert.ToDouble(reader[28]);
			if (!(reader[29] is DBNull)) beData.WalkingDistanceMin = Convert.ToDouble(reader[29]);
			}
		reader.NextResult();
		beData.GeneralInfoFacilitiesColl = new BE.Infrastructure.GeneralInfoFacilitiesCollections();
		while (reader.Read())
		{
			BE.Infrastructure.GeneralInfoFacilities det1 = new BE.Infrastructure.GeneralInfoFacilities();
			if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) det1.SNo = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
			if (!(reader[3] is DBNull)) det1.IsAvailable = Convert.ToBoolean(reader[3]);
			if (!(reader[4] is DBNull)) det1.SourceName = reader.GetString(4);
					beData.GeneralInfoFacilitiesColl.Add(det1);
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


		public AcademicLib.BE.Infrastructure.EmpShortDet getEmpShortDetbyId (int UserId, int EntityId, int EmployeeId)
		{
			AcademicLib.BE.Infrastructure.EmpShortDet beData = new AcademicLib.BE.Infrastructure.EmpShortDet();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetEmpShortDetById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.Infrastructure.EmpShortDet();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.OfficeContactNo = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.OfficeEmailId = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Qualification = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Gender = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.Caste = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.IsTeaching = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.Name = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.MotherName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.Department = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.Designation = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.DOB_AD = reader.GetDateTime(13);
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

