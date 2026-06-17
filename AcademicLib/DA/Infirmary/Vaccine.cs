using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
 
using PivotalERP.Global.Helpers;

namespace AcademicLib.DA.Infirmary
{
    public class VaccineDB
    {
        DataAccessLayer1 dal = null;
        public VaccineDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        //public ResponeValues SaveVaccine(BE.Infirmary.Vaccine vaccine, int userId)
        //{

        //    var resVal = new ResponeValues();

        //    dal.OpenConnection();

        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = "usp_InfirmarySaveVaccine";


        //    cmd.Parameters.Clear();
        //    if (vaccine.VaccineId != null)
        //        cmd.Parameters.AddWithValue("@Id", vaccine.VaccineId);
        //    cmd.Parameters.AddWithValue("@Name", vaccine.VaccineName);
        //    cmd.Parameters.AddWithValue("@Brand", vaccine.Brand);
        //    cmd.Parameters.AddWithValue("@Description", vaccine.Description);
        //    cmd.Parameters.AddWithValue("@VaccineFor", vaccine.VaccineFor);
        //    cmd.Parameters.AddWithValue("@OrderNo", vaccine.OrderNo);

        //    cmd.Parameters.AddWithValue("@CreatedBy", vaccine.CUserId);
        //    cmd.Parameters.AddWithValue("@LogDateTime", vaccine.LogDateTime);

        //    int tmpParamIdx = DAHelper.AddOutputParams(cmd);

        //    try
        //    {
        //        cmd.ExecuteNonQuery();

        //        DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);

        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }


        //    dal.CloseConnection();

        //    return resVal;
        //}

        //public BE.Infirmary.Vaccine getVaccineById(int vaccineId)
        //{

        //    var vaccine = new BE.Infirmary.Vaccine();

        //    try
        //    {
        //        dal.OpenConnection();

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_InfirmaryGetVaccineById";
        //        cmd.Parameters.AddWithValue("@VaccineId", vaccineId);

        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            vaccine.VaccineId = reader.GetInt32(0);
        //            vaccine.VaccineName = reader.GetString(1).Trim();
        //            vaccine.Description = reader.GetString(2).Trim();
        //            vaccine.Brand = reader.GetString(3).Trim();
        //            vaccine.VaccineFor = reader.GetInt32(4);
        //            if (!reader.IsDBNull(5))
        //                vaccine.OrderNo = reader.GetInt32(5);
        //            vaccine.IsSuccess = true;
        //            vaccine.ResponseMSG = "Vaccine Fetched Succesfully";

        //        }
        //        reader.Close();

        //    }
        //    catch (Exception ee)
        //    {
        //        vaccine.IsSuccess = false;
        //        vaccine.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }

        //    return vaccine;
        //}

        //public ResponeValues deleteVaccineById(int vaccineId, int userId)
        //{
        //    var resVal = new ResponeValues();

        //    try
        //    {
        //        dal.OpenConnection();

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_InfirmaryDeleteVaccineById";
        //        cmd.Parameters.AddWithValue("@VaccineId", vaccineId);
        //        cmd.Parameters.AddWithValue("@UserId", userId);

        //        int tmpParamIdx = DAHelper.AddOutputParams(cmd);

        //        cmd.ExecuteNonQuery();

        //        DAHelper.GetOutputParams(cmd, tmpParamIdx, resVal);

        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = "Vaccine Cant be Deleted";
        //        // uncomment to see real reason
        //        /*resVal.ResponseMSG = ee.Message;*/
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }

        //    return resVal;
        //}

        //public bool foundSimilarVaccine(string name)
        //{
        //    bool found = false;
        //    try
        //    {
        //        dal.OpenConnection();

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_InfirmaryGetAllSimilarVaccines";
        //        cmd.Parameters.AddWithValue("@Name", name);
        //        cmd.Parameters.Add("@FoundSimilar", System.Data.SqlDbType.Int);
        //        cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;

        //        cmd.ExecuteNonQuery();
        //        if (!(cmd.Parameters[1].Value is DBNull))
        //            found = Convert.ToBoolean(cmd.Parameters[1].Value);

        //    }
        //    catch (Exception ee)
        //    {
        //        found = false;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }

        //    return found;
        //}

        //public BE.Infirmary.VaccineCollections getAllVaccines()
        //{
        //    var vaccines = new BE.Infirmary.VaccineCollections();

        //    vaccines.IsSuccess = true;
        //    vaccines.ResponseMSG = "Fetched Vaccines Successfully";
            
        //    try
        //    {
        //        dal.OpenConnection();

        //        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "usp_InfirmaryGetAllVaccines";


        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            BE.Infirmary.Vaccine vaccine = new BE.Infirmary.Vaccine();
        //            vaccine.VaccineId = reader.GetInt32(0);
        //            vaccine.VaccineName = reader.GetString(1).Trim();
        //            vaccine.Description = reader.GetString(2).Trim();
        //            vaccine.Brand = reader.GetString(3).Trim();
        //            vaccine.VaccineFor = reader.GetInt32(4);
        //            if (!reader.IsDBNull(5))
        //                vaccine.OrderNo = reader.GetInt32(5);
        //            vaccine.IsSuccess = true;
        //            vaccine.ResponseMSG = GLOBALMSG.SUCCESS;

        //            vaccines.Add(vaccine);
        //        }
        //        reader.Close();

        //    }
        //    catch (Exception ee)
        //    {
        //        vaccines.IsSuccess = false;
        //        vaccines.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }
        //    /*if (vaccines.Count == 0)
        //    {
        //        vaccines.IsSuccess = false;
        //        vaccines.ResponseMSG = "No vaccines Found";
        //    }
        //    else
        //    {
        //        vaccines.IsSuccess = true;
        //        vaccines.ResponseMSG = "Fetched vaccines Successfully";
        //    }*/
        //    return vaccines;
        //}
    }
}