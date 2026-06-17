using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Transport.Creation
{
    internal class VehicleDB
    {
        DataAccessLayer1 dal = null;
        public VehicleDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Transport.Creation.Vehicle beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehicleNumber", beData.VehicleNumber);
            cmd.Parameters.AddWithValue("@SeatCapacity", beData.SeatCapacity);
            cmd.Parameters.AddWithValue("@EngineNo", beData.EngineNo);
            cmd.Parameters.AddWithValue("@ChassisNo", beData.ChassisNo);
            cmd.Parameters.AddWithValue("@FuelEngineType", beData.FuelEngineType);
            cmd.Parameters.AddWithValue("@NamsariDate", beData.NamsariDate);
            cmd.Parameters.AddWithValue("@RenewalDate", beData.RenewalDate);
            cmd.Parameters.AddWithValue("@MFGYear", beData.MFGYear);
            cmd.Parameters.AddWithValue("@JachpassNo", beData.JachpassNo);
            cmd.Parameters.AddWithValue("@JPValidityFrom", beData.JPValidityFrom);
            cmd.Parameters.AddWithValue("@JPValidityTo", beData.JPValidityTo);
            cmd.Parameters.AddWithValue("@JPRemarks", beData.JPRemarks);
            cmd.Parameters.AddWithValue("@InsuranceNo", beData.InsuranceNo);
            cmd.Parameters.AddWithValue("@IValidityFrom", beData.IValidityFrom);
            cmd.Parameters.AddWithValue("@IValidityTo", beData.IValidityTo);
            cmd.Parameters.AddWithValue("@IRemarks", beData.IRemarks);
            cmd.Parameters.AddWithValue("@InChargeId", beData.InChargeId);
            cmd.Parameters.AddWithValue("@DriverId", beData.DriverId);
            cmd.Parameters.AddWithValue("@ConductorId", beData.ConductorId);
            cmd.Parameters.AddWithValue("@GPSDevice", beData.GPSDevice);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);          
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@VehicleId", beData.VehicleId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateVehicle";
            }
            else
            {
                cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddVehicle";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@VehicleName", beData.VehicleName);
            cmd.Parameters.AddWithValue("@Url", beData.Url);
            cmd.Parameters.AddWithValue("@User", beData.User);
            cmd.Parameters.AddWithValue("@Pwd", beData.Pwd);
            cmd.Parameters.AddWithValue("@Authentication", beData.Authentication);
            cmd.Parameters.AddWithValue("@Token", beData.Token);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[23].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[23].Value);

                if (!(cmd.Parameters[24].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[24].Value);

                if (!(cmd.Parameters[25].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[25].Value);

                if (!(cmd.Parameters[26].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[26].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                if (resVal.RId > 0 && resVal.IsSuccess)
                {                    
                    SaveDocument(beData.CUserId, resVal.RId, beData.AttachmentColl);
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

        private void SaveDocument(int UserId, int VehicleId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || VehicleId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                    cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);

                    if (beData.Data != null)
                        cmd.Parameters.AddWithValue("@Document", beData.Data);
                    else
                        cmd.Parameters.AddWithValue("@Document", System.Data.SqlTypes.SqlBinary.Null);

                    cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                    cmd.Parameters.AddWithValue("@Description", beData.Description);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddVehicleAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public AcademicLib.BE.Transport.Creation.VehicleCollections getAllVehicle(int UserId, int EntityId)
        {
            AcademicLib.BE.Transport.Creation.VehicleCollections dataColl = new AcademicLib.BE.Transport.Creation.VehicleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllVehicle";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.Vehicle beData = new AcademicLib.BE.Transport.Creation.Vehicle();
                    beData.VehicleId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.VehicleName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleNumber = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SeatCapacity = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Driver = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RenewalDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.RenewalDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.JPValidityTo = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.JPValidityTo_BS = reader.GetString(8);

                    //Added By Suresh on 12 Falgun
                    if (!(reader[9] is DBNull)) beData.ImagePath = reader.GetString(9);

                    //Added By Suresh on 15 Falgun
                    if (!(reader[10] is DBNull)) beData.FuelEngineType = reader.GetString(10);

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
        public AcademicLib.BE.Transport.Creation.Vehicle getVehicleById(int UserId, int EntityId, int VehicleId)
        {
            AcademicLib.BE.Transport.Creation.Vehicle beData = new AcademicLib.BE.Transport.Creation.Vehicle();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetVehicleById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Transport.Creation.Vehicle();
                    beData.VehicleId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.VehicleNumber = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SeatCapacity = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EngineNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ChassisNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FuelEngineType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.NamsariDate = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.RenewalDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.MFGYear = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.JachpassNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.JPValidityFrom = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.JPValidityTo = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.JPRemarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.InsuranceNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IValidityFrom = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.IValidityTo = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.IRemarks = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.InChargeId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.DriverId = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.ConductorId = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.GPSDevice = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.ImagePath = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.VehicleName = reader.GetString(22);

                    //Added By Suresh on 13 Falgun for print
                    if (!(reader[23] is DBNull)) beData.NamsariMiti = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.RenewalMiti = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.JPValidFromMiti = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.JPValidToMiti = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.IValidityFromMiti = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.IValidityToMiti = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.InCharge = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.Driver = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.Conductor = reader.GetString(31);

                    if (!(reader[32] is DBNull)) beData.Url = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.User = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.Pwd = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.Authentication = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.Token = reader.GetString(36);
                    //C.Url,C.[User],C.Pwd,C.[Authentication],C.Token
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) doc.Description = reader.GetString(4);

                    beData.AttachmentColl.Add(doc);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int VehicleId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
            cmd.CommandText = "usp_DelVehicleById";
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

        public AcademicLib.API.Admin.VehicleDetailCollections admin_VehicleList(int UserId)
        {
            AcademicLib.API.Admin.VehicleDetailCollections dataColl = new API.Admin.VehicleDetailCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);          
            cmd.CommandText = "usp_admin_VehicleList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.VehicleDetail beData = new API.Admin.VehicleDetail();
                    beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.VehicleName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RenewalDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.RenewalDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.JachpassNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.JPValidityTo_AD = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.JPValidityTo_BS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.DriverName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DriverContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ConductorName = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.ConductorContactNo = Convert.ToString(reader[11]);
                    beData.VehicleId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.DriverId = Convert.ToInt32(reader[13]);
                    if (!(reader[14] is DBNull)) beData.ConductorId = Convert.ToInt32(reader[14]);
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
