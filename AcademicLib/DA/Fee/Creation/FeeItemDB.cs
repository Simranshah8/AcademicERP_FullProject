using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeItemDB
    {
        DataAccessLayer1 dal = null;
        public FeeItemDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Fee.Creation.FeeItem beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Code", beData.Code);
            cmd.Parameters.AddWithValue("@PrintName", beData.PrintName);
            cmd.Parameters.AddWithValue("@HeadFor", beData.HeadFor);
            cmd.Parameters.AddWithValue("@LedgerId", beData.LedgerId);
            cmd.Parameters.AddWithValue("@ProductId", beData.ProductId);
            cmd.Parameters.AddWithValue("@ApplyTax", beData.ApplyTax);
            cmd.Parameters.AddWithValue("@TaxRate", beData.TaxRate);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@OneTimeApplicable", beData.OneTimeApplicable);
            cmd.Parameters.AddWithValue("@OnlyForNewStudent", beData.OnlyForNewStudent);
            cmd.Parameters.AddWithValue("@OnlyForOldStudent", beData.OnlyForOldStudent);
            cmd.Parameters.AddWithValue("@OnlyForHostel", beData.OnlyForHostel);
            cmd.Parameters.AddWithValue("@OnlyForTransport", beData.OnlyForTransport);
            cmd.Parameters.AddWithValue("@OnlyForFixedStudent", beData.OnlyForFixedStudent);
            cmd.Parameters.AddWithValue("@IsExtraFee", beData.IsExtraFee);
            cmd.Parameters.AddWithValue("@RefundableFee", beData.RefundableFee);
            cmd.Parameters.AddWithValue("@ScholarshipApplicable", beData.ScholarshipApplicable);
            cmd.Parameters.AddWithValue("@ApplyFine", beData.ApplyFine);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateFeeItem";
            }
            else
            {
                cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddFeeItem";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsSecurityDeposit", beData.IsSecurityDeposit);

            cmd.Parameters.AddWithValue("@NotApplicableForHostel", beData.NotApplicableForHostel);
            cmd.Parameters.AddWithValue("@NotApplicableForTransport", beData.NotApplicableForTransport);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[21].Value);

                if (!(cmd.Parameters[22].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[22].Value);

                if (!(cmd.Parameters[23].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[23].Value);

                if (!(cmd.Parameters[24].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[24].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if(resVal.IsSuccess && resVal.RId > 0 && beData.MonthIdColl!=null && beData.MonthIdColl.Count>0)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    foreach(var m in beData.MonthIdColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@FeeItemId", resVal.RId);
                        cmd.Parameters.AddWithValue("@MonthId", m);
                        cmd.CommandText = "insert into tbl_FeeItemMonth(FeeItemId,MonthId) values(@FeeItemId,@MonthId)";
                        cmd.ExecuteNonQuery();
                    }
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
        public BE.Fee.Creation.FeeItemCollections getAllFeeItem(int UserId, int EntityId)
        {
            BE.Fee.Creation.FeeItemCollections dataColl = new BE.Fee.Creation.FeeItemCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllFeeItem";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeItem beData = new BE.Fee.Creation.FeeItem();
                    beData.FeeItemId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Code = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.PrintName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.HeadFor = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsExtraFee = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.ScholarshipApplicable = reader.GetBoolean(7);
                    try
                    {
                        if (!(reader[8] is DBNull)) beData.Ledger = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.LedgerGroup = reader.GetString(9);
                        if (!(reader[10] is DBNull)) beData.Product = reader.GetString(10);
                        if (!(reader[11] is DBNull)) beData.ProductType = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.TaxRate = Convert.ToDouble(reader[12]);
                        if (!(reader[13] is DBNull)) beData.OneTimeApplicable = reader.GetBoolean(13);
                    }
                    catch { }
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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
        public BE.Fee.Creation.FeeItem getFeeItemById(int UserId, int EntityId, int FeeItemId)
        {
            BE.Fee.Creation.FeeItem beData = new BE.Fee.Creation.FeeItem();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetFeeItemById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Fee.Creation.FeeItem();
                    beData.FeeItemId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.PrintName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.HeadFor = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.LedgerId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ProductId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ApplyTax = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.TaxRate =Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.OrderNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.OneTimeApplicable = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.OnlyForNewStudent = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.OnlyForOldStudent = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.OnlyForHostel = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.OnlyForTransport = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.OnlyForFixedStudent = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.IsExtraFee = reader.GetBoolean(16);
                    if (!(reader[17] is DBNull)) beData.RefundableFee = reader.GetBoolean(17);
                    if (!(reader[18] is DBNull)) beData.ScholarshipApplicable = reader.GetBoolean(18);
                    if (!(reader[19] is DBNull)) beData.ApplyFine = reader.GetBoolean(19);
                    if (!(reader[20] is DBNull)) beData.IsSecurityDeposit = reader.GetBoolean(20);
                    if (!(reader[21] is DBNull)) beData.NotApplicableForHostel = reader.GetBoolean(21);
                    if (!(reader[22] is DBNull)) beData.NotApplicableForTransport = reader.GetBoolean(22);
                }
                reader.NextResult();
                beData.MonthIdColl = new List<int>();
                while (reader.Read())
                {
                    beData.MonthIdColl.Add(reader.GetInt32(0));
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
        public ResponeValues DeleteById(int UserId, int EntityId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.CommandText = "usp_DelFeeItemById";
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
            catch(System.Data.ConstraintException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Can not delete . Already in used.";
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
