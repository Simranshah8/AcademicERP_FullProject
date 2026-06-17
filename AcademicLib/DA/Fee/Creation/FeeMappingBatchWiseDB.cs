using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeMappingBatchWiseDB
    {
        DataAccessLayer1 dal = null;
        public FeeMappingBatchWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId,int BatchId,int FacultyId, BE.Fee.Creation.FeeMappingClassWiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DelFeeMappingBatchWise";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@BatchId", BatchId);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.ExecuteNonQuery();

                
                foreach (var beData in dataColl)
                {
                    if (beData.MonthColl == null)
                        beData.MonthColl = new List<BE.Fee.Creation.FeeMappingMonth>();

                    cmd.Parameters.Clear();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                    cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@BatchId", BatchId);
                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@Qty", beData.MonthColl.Count);
                    cmd.CommandText = "usp_SaveBatchWiseFeeMapping";
                    cmd.ExecuteNonQuery();

                    int tranId = Convert.ToInt32(cmd.Parameters[8].Value);

                    if (beData.MonthColl != null)
                    {
                        foreach (var mn in beData.MonthColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@TranId", tranId);
                            cmd.Parameters.AddWithValue("@MonthId", mn.MonthId);
                            if(mn.DueDate.HasValue)
                                cmd.Parameters.AddWithValue("@DueDate", mn.DueDate);
                            else
                                cmd.Parameters.AddWithValue("@DueDate", DBNull.Value);
                             
                            cmd.CommandText = "insert into tbl_FeeMappingClassWiseMonth(TranId,MonthId,DueDate) values(@TranId,@MonthId,@DueDate)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Feemapping BatchWise Done.";
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

        public BE.Fee.Creation.FeeMappingClassWiseCollections getAllFeeMapping(int UserId, int AcademicYearId, int EntityId,int BatchId,int FacultyId)
        {
            BE.Fee.Creation.FeeMappingClassWiseCollections dataColl = new BE.Fee.Creation.FeeMappingClassWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
            cmd.CommandText = "usp_GetBatchWiseFeemapping";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeMappingClassWise beData = new BE.Fee.Creation.FeeMappingClassWise();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Semester = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassYear = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FeeItemName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.SemesterId    = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ClassYearId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.BatchId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.FacultyId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.FeeItemId = reader.GetInt32(11);                    
                    if (!(reader[12] is DBNull)) beData.Rate = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.TranId = Convert.ToInt32(reader[13]);
                    if (!(reader[14] is DBNull)) beData.OneTimeApplicable = Convert.ToBoolean(reader[14]);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int tranId = reader.GetInt32(0);
                    var findFM = dataColl.Find(p1 => p1.TranId == tranId);
                    if (findFM != null)
                    {
                        BE.Fee.Creation.FeeMappingMonth mnt = new BE.Fee.Creation.FeeMappingMonth();
                        if (!(reader[1] is DBNull)) mnt.MonthId = Convert.ToInt32(reader[1]);
                        if (!(reader[2] is DBNull)) mnt.DueDate = Convert.ToDateTime(reader[2]);
                        findFM.MonthColl.Add(mnt);
                    }
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

        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId,int BatchId,int FacultyId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_DelFeeMappingBatchWise";
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
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
     
    }
}
