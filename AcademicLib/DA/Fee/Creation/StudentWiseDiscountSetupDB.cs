using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Creation
{
    internal class StudentWiseDiscountSetupDB
    {
        DataAccessLayer1 dal = null;
        public StudentWiseDiscountSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId, BE.Fee.Creation.FeeItemWiseDiscountSetupCollections dataColl)
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
                cmd.Parameters.AddWithValue("@SemesterId", first.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", first.ClassYearId);
                cmd.CommandText = "usp_DelStudentWiseDiscountSetup";
                cmd.ExecuteNonQuery(); 

                foreach (var beData in dataColl)
                {
                    if (beData.StudentId > 0 && beData.FeeItemId > 0)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                        cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                        cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                        cmd.CommandText = "usp_SaveFeeItemDiscountSetup";
                        cmd.ExecuteNonQuery();
                        int tranId = Convert.ToInt32(cmd.Parameters[6].Value);
                        cmd.CommandType = System.Data.CommandType.Text;
                        foreach (var m in beData.MonthIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TranId", tranId);
                            cmd.Parameters.AddWithValue("@MonthId", m.MonthId);
                            cmd.Parameters.AddWithValue("@DiscountAmt", m.DiscountAmt);
                            cmd.Parameters.AddWithValue("@DiscountPer", m.DiscountPer);

                            cmd.CommandText = "insert into tbl_FeeItemWiseDiscountSetupMonth(TranId,MonthId,DiscountAmt,DiscountPer) values(@TranId,@MonthId,@DiscountAmt,@DiscountPer)";
                            cmd.ExecuteNonQuery();
                        }

                    }

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Wise Discount Setup Saved";
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

        public BE.Fee.Creation.FeeItemWiseDiscountSetupCollections getStudentWiseDiscountSetup(int UserId, int AcademicYearId, int EntityId, int StudentId, int? SemesterId, int? ClassYearId, int? BatchId = null)
        {
            BE.Fee.Creation.FeeItemWiseDiscountSetupCollections dataColl = new BE.Fee.Creation.FeeItemWiseDiscountSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId); 
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetStudentWiseDiscountSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeItemWiseDiscountSetup beData = new BE.Fee.Creation.FeeItemWiseDiscountSetup();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Remarks = reader.GetString(4);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeItemWiseDiscountSetupMonth beData = new BE.Fee.Creation.FeeItemWiseDiscountSetupMonth();
                    int sid = 0,fid=0;
                    if (!(reader[0] is DBNull)) sid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) fid = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.MonthId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    dataColl.Find(p1 => p1.StudentId == sid && p1.FeeItemId==fid).MonthIdColl.Add(beData);
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
        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId, int StudentId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId); 
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_DelStudentWiseDiscountSetup";
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
