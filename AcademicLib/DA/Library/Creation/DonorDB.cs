using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Creation
{
    internal class DonorDB
    {
        DataAccessLayer1 dal = null;
        public DonorDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Library.Creation.Donor beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@PhoneNo", beData.PhoneNo);
            cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
            cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@DonorId", beData.DonorId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateDonor";
            }
            else
            {
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddDonor";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@LogoPath", beData.LogoPath);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveContactPerson(beData.CUserId, resVal.RId, beData.ContactPersonList);
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
        private void SaveContactPerson(int UserId, int DonorId, List<AcademicLib.BE.Library.Creation.PublicationContactPerson> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || DonorId == 0)
                return;

            foreach (AcademicLib.BE.Library.Creation.PublicationContactPerson beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name))
                {

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@DonorId", DonorId);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
                    cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
                    cmd.Parameters.AddWithValue("@Designation", beData.Designation);
                    cmd.Parameters.AddWithValue("@EmailId", beData.EmailId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddDonorContactPerson";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        private void SaveDocument(int UserId, int DonorId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || DonorId == 0)
                return;

            foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.Name) && !string.IsNullOrEmpty(beData.Extension) && (beData.Data != null || !string.IsNullOrEmpty(beData.DocPath)))
                {
                    if (string.IsNullOrEmpty(beData.DocPath))
                        beData.DocPath = "";

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@DonorId", DonorId);
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
                    cmd.CommandText = "usp_AddDonorAttachDocument";
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public BE.Library.Creation.DonorCollections getAllDonor(int UserId, int EntityId)
        {
            BE.Library.Creation.DonorCollections dataColl = new BE.Library.Creation.DonorCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllDonor";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Creation.Donor beData = new BE.Library.Creation.Donor();
                    beData.DonorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.PhoneNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EmailId = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MobileNo = reader.GetString(6);
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
        public BE.Library.Creation.Donor getDonorById(int UserId, int EntityId, int DonorId)
        {
            BE.Library.Creation.Donor beData = new BE.Library.Creation.Donor();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonorId", DonorId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetDonorById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Library.Creation.Donor();
                    beData.DonorId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.PhoneNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EmailId = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.MobileNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LogoPath = reader.GetString(7);
                }
                beData.ContactPersonList = new List<BE.Library.Creation.PublicationContactPerson>();
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Library.Creation.PublicationContactPerson contactPerson = new BE.Library.Creation.PublicationContactPerson();
                    if (!(reader[0] is DBNull)) contactPerson.Name = reader.GetString(0);
                    if (!(reader[1] is DBNull)) contactPerson.Designation = reader.GetString(1);
                    if (!(reader[2] is DBNull)) contactPerson.MobileNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) contactPerson.ContactNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) contactPerson.EmailId = reader.GetString(4);
                    beData.ContactPersonList.Add(contactPerson);
                }
                reader.NextResult();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
        public ResponeValues DeleteById(int UserId, int EntityId, int DonorId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DonorId", DonorId);
            cmd.CommandText = "usp_DelDonorById";
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
