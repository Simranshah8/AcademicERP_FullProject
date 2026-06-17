using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeDebitDB
    {
        DataAccessLayer1 dal = null;
        public FeeDebitDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId, BE.Fee.Creation.FeeDebitCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;
            int monthId = first.MonthId;
            int? batchId = first.BatchId;
            int? semesterId = first.SemesterId;
            int? classYearId = first.ClassYearId;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@MonthId", monthId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@BatchId", batchId);
                cmd.Parameters.AddWithValue("@SemesterId", semesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", classYearId);
                cmd.CommandText = "usp_DelFeeDebit";
                cmd.ExecuteNonQuery();


                foreach (var beData in dataColl)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
                    cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                    cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                    cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                    cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                    cmd.Parameters.AddWithValue("@TaxAmt", beData.TaxAmt);
                    cmd.Parameters.AddWithValue("@FineAmt", beData.FineAmt);
                    cmd.Parameters.AddWithValue("@PayableAmt", beData.PayableAmt);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@BatchId", batchId);
                    cmd.Parameters.AddWithValue("@SemesterId", semesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", classYearId);
                    cmd.CommandText = "usp_SaveFeeDebit";
                    cmd.ExecuteNonQuery();                   
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "FeeDebit ClassWise Saved";
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
        public BE.Fee.Creation.FeeDebitCollections getFeeDebit(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId,int MonthId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            BE.Fee.Creation.FeeDebitCollections dataColl = new BE.Fee.Creation.FeeDebitCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            //BatchId,SemesterId,ClassYearId
            cmd.CommandText = "usp_GetFeeDebit";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeDebit beData = new BE.Fee.Creation.FeeDebit();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.MonthId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Remarks = reader.GetString(9);

                    if (!(reader[10] is DBNull)) beData.ClassId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.SectionId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.BatchId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.SemesterId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.ClassYearId = reader.GetInt32(14);
                    
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
        public ResponeValues Delete(int UserId, int EntityId, int AcademicYearId, int ClassId, int? SectionId,int MonthId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelFeeDebit";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.DELETE_SUCCESS;
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

        public ResponeValues SaveUpdateStudentWise(int UserId, int AcademicYearId, BE.Fee.Creation.FeeDebitCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int studentId = first.StudentId;
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@BatchId", first.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", first.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", first.ClassYearId);
                cmd.CommandText = "usp_DelFeeDebitStudentWise";
                cmd.ExecuteNonQuery();


                foreach (var beData in dataColl)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
                    cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                    cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                    cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                    cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                    cmd.Parameters.AddWithValue("@TaxAmt", beData.TaxAmt);
                    cmd.Parameters.AddWithValue("@FineAmt", beData.FineAmt);
                    cmd.Parameters.AddWithValue("@PayableAmt", beData.PayableAmt);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.CommandText = "usp_SaveFeeDebit";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "FeeDebit StudentWise Saved";
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
        public BE.Fee.Creation.FeeDebitCollections getFeeDebitStudentWise(int UserId, int AcademicYearId, int EntityId, int StudentId)
        {
            BE.Fee.Creation.FeeDebitCollections dataColl = new BE.Fee.Creation.FeeDebitCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeDebitStudentWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeDebit beData = new BE.Fee.Creation.FeeDebit();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.MonthId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Remarks = reader.GetString(9);
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
        public ResponeValues DeleteStudentWise(int UserId, int AcademicYearId, int EntityId, int StudentId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelFeeDebitStudentWise";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.DELETE_SUCCESS;
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


        public ResponeValues SaveFeeDebitFeeItemWise(int UserId, int AcademicYearId, BE.Fee.Creation.FeeDebitFeeItemWise beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();                
                cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
                cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                cmd.Parameters.AddWithValue("@ClassIdColl", beData.ClassIdColl);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                cmd.Parameters.AddWithValue("@SectionIdColl", beData.SectionIdColl);
                cmd.CommandText = "usp_SaveFeeDebitFeeItemWise";
                cmd.ExecuteNonQuery();
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "FeeDebit FeeItemWise Saved";
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
