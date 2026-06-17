using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class ClassWiseDiscountSetupDB
    {
        DataAccessLayer1 dal = null;
        public ClassWiseDiscountSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId, BE.Fee.Creation.ClassWiseDiscountSetupCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;
            int? semesterId = first.SemesterId;
            int? classyearId = first.ClassYearId;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@SemesterId", semesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", classyearId);
                cmd.CommandText = "usp_DelClassWiseDiscountSetup";
                cmd.ExecuteNonQuery();


                foreach (var beData in dataColl)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@DiscountTypeId", beData.DiscountTypeId);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.CommandText = "usp_SaveClassWiseDiscountSetup";
                    cmd.ExecuteNonQuery();                   
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "ClassWise DiscountSetup Saved";
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

        public BE.Fee.Creation.ClassWiseDiscountSetupCollections getClassWiseDiscountSetup(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId,int? SemesterId,int? ClassYearId)
        {
            BE.Fee.Creation.ClassWiseDiscountSetupCollections dataColl = new BE.Fee.Creation.ClassWiseDiscountSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetClassWiseDiscountSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.ClassWiseDiscountSetup beData = new BE.Fee.Creation.ClassWiseDiscountSetup();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.DiscountTypeId = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
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

        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId,int? SemesterId,int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_DelClassWiseDiscountSetup";
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
