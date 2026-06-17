using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeMappingClassWiseDB
    {
        DataAccessLayer1 dal = null;
        public FeeMappingClassWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, int AcademicYearId, BE.Fee.Creation.FeeMappingClassWiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DelFeeMappingClassWise";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.ExecuteNonQuery();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var beData in dataColl)
                {
                    if (beData.Rate != 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                        cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                        cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_SaveClassWiseFeeMapping";
                        cmd.ExecuteNonQuery();

                        int tranId = Convert.ToInt32(cmd.Parameters[5].Value);

                        if (beData.MonthColl != null && beData.MonthColl.Count>0)
                        {
                            foreach (var mn in beData.MonthColl)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Parameters.AddWithValue("@TranId", tranId);
                                cmd.Parameters.AddWithValue("@MonthId", mn.MonthId);
                                if (mn.DueDate.HasValue)
                                    cmd.Parameters.AddWithValue("@DueDate", mn.DueDate);
                                else
                                    cmd.Parameters.AddWithValue("@DueDate", DBNull.Value);

                                cmd.CommandText = "insert into tbl_FeeMappingClassWiseMonth(TranId,MonthId,DueDate) values(@TranId,@MonthId,@DueDate)";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }                   
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Feemapping ClassWise Done.";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public BE.Fee.Creation.FeeMappingClassWiseCollections getAllFeeMapping(int UserId,int AcademicYearId, int EntityId)
        {
            BE.Fee.Creation.FeeMappingClassWiseCollections dataColl = new BE.Fee.Creation.FeeMappingClassWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeemappingClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeMappingClassWise beData = new BE.Fee.Creation.FeeMappingClassWise();
                    beData.ClassId = reader.GetInt32(0);
                    beData.ClassName = reader.GetString(1);
                    beData.FeeItemId = reader.GetInt32(2);
                    beData.FeeItemName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Rate = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TranId = Convert.ToInt32(reader[5]);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeMappingMonth mm = new BE.Fee.Creation.FeeMappingMonth();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) mm.MonthId = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) mm.DueDate = Convert.ToDateTime(reader[2]);
                    try
                    {
                        dataColl.Find(p1 => p1.TranId == tranId).MonthColl.Add(mm);
                    }
                    catch { }
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

        public ResponeValues Delete(int UserId,int AcademicYearId, int EntityId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.CommandText = "usp_DelFeeMappingClassWise";
          
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
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


        public RE.Fee.FeeMappingStudentCollections getFeeMappingStudentList(int UserId, int AcademicYearId, string ClassIdColl,string FeeItemIdColl,int For)
        {
            RE.Fee.FeeMappingStudentCollections dataColl = new RE.Fee.FeeMappingStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", FeeItemIdColl);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@For", For);
            cmd.CommandText = "usp_FeeMappingSetupStudentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.FeeMappingStudent beData = new RE.Fee.FeeMappingStudent();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FeeItem = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Nature = reader.GetString(11); 
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
