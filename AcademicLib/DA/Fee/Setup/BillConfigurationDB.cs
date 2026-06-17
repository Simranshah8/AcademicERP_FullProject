using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Setup
{
    internal class BillConfigurationDB
    {
        DataAccessLayer1 dal = null;
        public BillConfigurationDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Fee.Setup.BillConfiguration beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NumberingMethod", beData.NumberingMethod);
            cmd.Parameters.AddWithValue("@NumericalPartWidth", beData.NumericalPartWidth);
            cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
            cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
            cmd.Parameters.AddWithValue("@StartNumber", beData.StartNumber);
            cmd.Parameters.AddWithValue("@DateStyle", beData.DateStyle);
            cmd.Parameters.AddWithValue("@DateFormat", beData.DateFormat);
            cmd.Parameters.AddWithValue("@NoOfDecimal", beData.NoOfDecimal);
            cmd.Parameters.AddWithValue("@BillingHeadingFont", beData.BillingHeadingFont);
            cmd.Parameters.AddWithValue("@BillingHeading", beData.BillingHeading);
            cmd.Parameters.AddWithValue("@BillingNotesFont", beData.BillingNotesFont);
            cmd.Parameters.AddWithValue("@BillingNotes", beData.BillingNotes);
            cmd.Parameters.AddWithValue("@ReminderFont", beData.ReminderFont);
            cmd.Parameters.AddWithValue("@ReminderNotes", beData.ReminderNotes);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);            
            cmd.CommandText = "usp_AddBillConfiguration";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ShowPreDuesFeeHeading", beData.ShowPreDuesFeeHeading);
            cmd.Parameters.AddWithValue("@BillNoAs", beData.BillNoAs);
            cmd.Parameters.AddWithValue("@ShowLeftStudentInOpening", beData.ShowLeftStudentInOpening);
            cmd.Parameters.AddWithValue("@ShowLeftStudentInFeeMapping", beData.ShowLeftStudentInFeeMapping);
            cmd.Parameters.AddWithValue("@OpeningDuesLabel", beData.OpeningDuesLabel);
            cmd.Parameters.AddWithValue("@CalculateManualBillAs", beData.CalculateManualBillAs);
            cmd.Parameters.AddWithValue("@IgnoreCashSalesReceiptInBillPrint", beData.IgnoreCashSalesReceiptInBillPrint);
            cmd.Parameters.AddWithValue("@DiscountEffectAs", beData.DiscountEffectAs);
            cmd.Parameters.AddWithValue("@ActiveTaxOnManualBilling", beData.ActiveTaxOnManualBilling);
            cmd.Parameters.AddWithValue("@ActiveStudentType", beData.ActiveStudentType);
            cmd.Parameters.AddWithValue("@ActiveRoundOff", beData.ActiveRoundOff);
            cmd.Parameters.AddWithValue("@ActiveForceDiscount", beData.ActiveForceDiscount);
            try
            {
                cmd.ExecuteNonQuery();            

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[18].Value);

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

        public AcademicLib.BE.Fee.Setup.BillConfiguration getBillConfigurationById(int UserId, int EntityId)
        {
            AcademicLib.BE.Fee.Setup.BillConfiguration beData = new AcademicLib.BE.Fee.Setup.BillConfiguration();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetBillConfigurationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Setup.BillConfiguration();
                    if (!(reader[0] is DBNull)) beData.NumberingMethod = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.NumericalPartWidth = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Prefix = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Suffix = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StartNumber = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.DateStyle = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.DateFormat = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.NoOfDecimal = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.BillingHeadingFont = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.BillingHeading = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BillingNotesFont = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.BillingNotes = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ReminderFont = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ReminderNotes = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ShowPreDuesFeeHeading = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.BillNoAs = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.ShowLeftStudentInOpening = reader.GetBoolean(16);
                    if (!(reader[17] is DBNull)) beData.ShowLeftStudentInFeeMapping = reader.GetBoolean(17);
                    if (!(reader[18] is DBNull)) beData.OpeningDuesLabel = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.CalculateManualBillAs = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.IgnoreCashSalesReceiptInBillPrint = reader.GetBoolean(20);
                    if (!(reader[21] is DBNull)) beData.DiscountEffectAs = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.ActiveTaxOnManualBilling = reader.GetBoolean(22);
                    if (!(reader[23] is DBNull)) beData.ActiveStudentType = reader.GetBoolean(23);
                    if (!(reader[24] is DBNull)) beData.ActiveRoundOff = reader.GetBoolean(24);
                    if (!(reader[25] is DBNull)) beData.ActiveForceDiscount = reader.GetBoolean(25);
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
            cmd.CommandText = "usp_DelBillConfigurationById";
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
