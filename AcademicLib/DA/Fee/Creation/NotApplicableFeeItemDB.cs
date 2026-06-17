using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class NotApplicableFeeItemDB
    {
        DataAccessLayer1 dal = null;
        public NotApplicableFeeItemDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId, BE.Fee.Creation.NotApplicableFeeItemCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;
            int feeItemId = first.FeeItemId;
            int? semesterId = first.SemesterId;
            int? classYearId = first.ClassYearId;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@FeeItemId", feeItemId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@SemesterId", semesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", classYearId);
                cmd.CommandText = "usp_DelNotApplicableFeeItem";
                cmd.ExecuteNonQuery();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var beData in dataColl)
                {
                    if (beData.StudentId > 0 && beData.FeeItemId > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.CommandText = "usp_SaveNotApplicableFeeItem";
                        cmd.ExecuteNonQuery();
                    }
                  
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Not Applicable Fee Saved";
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

        public BE.Fee.Creation.NotApplicableFeeItemCollections getAllFeeMapping(int UserId, int EntityId,int AcademicYearId, int ClassId,int? SectionId, int? SemesterId, int? ClassYearId, int FeeItemId)
        {
            BE.Fee.Creation.NotApplicableFeeItemCollections dataColl = new BE.Fee.Creation.NotApplicableFeeItemCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetNotApplicableFeeItem";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.NotApplicableFeeItem beData = new BE.Fee.Creation.NotApplicableFeeItem();
                    beData.StudentId = reader.GetInt32(0);                  
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

        public ResponeValues Delete(int UserId, int EntityId,int AcademicYearId,  int ClassId, int? SectionId,int? SemesterId,int? ClassYearId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_DelNotApplicableFeeItem";
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
