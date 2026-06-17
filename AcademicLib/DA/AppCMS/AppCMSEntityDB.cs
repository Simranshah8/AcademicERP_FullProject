using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{
    internal class AppCMSEntityDB
    {
        DataAccessLayer1 dal = null;
        public AppCMSEntityDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 
            try
            {
                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                    cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
                    cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Label", beData.Label);
                    cmd.CommandText = "usp_UpdateAppCMSEntity";
                    cmd.ExecuteNonQuery();
                }

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
 
        public AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections getEntity(int? UserId,string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections dataColl = new AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode); 
            cmd.CommandText = "usp_GetAppCMSEntity";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.AppCMSEntity beData = new BE.AppCMS.Creation.AppCMSEntity();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.OrderNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.IsActive = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.Label = reader.GetString(5);

                    if (string.IsNullOrEmpty(beData.Label))
                        beData.Label = beData.Name;

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
        

    }
}

