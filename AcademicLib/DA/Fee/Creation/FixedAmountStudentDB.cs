using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class FixedAmountStudentDB
    {
        DataAccessLayer1 dal = null;
        public FixedAmountStudentDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId, BE.Fee.Creation.FixedAmountStudentCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.CommandText = "usp_DelFixedAmountStudent";
                cmd.ExecuteNonQuery();

                
                foreach (var beData in dataColl)
                {
                    if (beData.StudentId > 0)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_SaveFixedAmountStudent";
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.ExecuteNonQuery();
                        int tranId = Convert.ToInt32(cmd.Parameters[4].Value);
                        cmd.CommandType = System.Data.CommandType.Text;
                        foreach (var m in beData.MonthIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TranId", tranId);
                            cmd.Parameters.AddWithValue("@MonthId", m);
                            cmd.CommandText = "insert into tbl_FixedAmountStudentMonth(TranId,MonthId) values(@TranId,@MonthId)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Fixed Amount Student Saved";
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

        public BE.Fee.Creation.FixedAmountStudentCollections getFixedAmountStudent(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            BE.Fee.Creation.FixedAmountStudentCollections dataColl = new BE.Fee.Creation.FixedAmountStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFixedAmountStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FixedAmountStudent beData = new BE.Fee.Creation.FixedAmountStudent();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Amount = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);                    
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int sid = reader.GetInt32(0);
                    int mid = reader.GetInt32(1);
                    dataColl.Find(p1 => p1.StudentId == sid).MonthIdColl.Add(mid);
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

        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelFixedAmountStudent";
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
