using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Library.Creation
{
    internal class MembershipGenerateDB
    {
        DataAccessLayer1 dal = null;
        public MembershipGenerateDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues GenerateMembership(int AcademicYearId, BE.Library.Creation.MembershipGenerate beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForStudentEmp", beData.ForStudentEmp);
            cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
            cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
            cmd.Parameters.AddWithValue("@MembershipNoAs", beData.MembershipNoAs);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);            
            cmd.Parameters.AddWithValue("@ReGenerate", beData.ReGenerate);          
            cmd.CommandText = "usp_GenerateLibraryMembership";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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

        public BE.Library.Creation.StudentMemberCollections getClassWiseMemberlist(int UserId, int AcademicYearId, int ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            BE.Library.Creation.StudentMemberCollections dataColl = new BE.Library.Creation.StudentMemberCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);            
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);            
            cmd.CommandText = "usp_GetClassWiseLibMembers";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Creation.StudentMember beData = new BE.Library.Creation.StudentMember();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.IsLibMember = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.MembershipNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.IssueDateAD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.ExpiredDateAD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.IssueDateBS = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ExpiredDateBS = reader.GetString(11);
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

        public BE.Library.Creation.EmployeeMemberCollections getEmpMemberlist(int UserId)
        {
            BE.Library.Creation.EmployeeMemberCollections dataColl = new BE.Library.Creation.EmployeeMemberCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);         
            cmd.CommandText = "usp_GetEmpLibMembers";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Library.Creation.EmployeeMember beData = new BE.Library.Creation.EmployeeMember();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);                  
                    if (!(reader[5] is DBNull)) beData.IsLibMember = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.MembershipNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.IssueDateAD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.ExpiredDateAD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.IssueDateBS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ExpiredDateBS = reader.GetString(10);
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
