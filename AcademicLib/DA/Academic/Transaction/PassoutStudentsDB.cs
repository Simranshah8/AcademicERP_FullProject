using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA
{

	internal class PassoutStudentsDB : Dynamic.DataAccess.Common.CommonDB
	{
		DataAccessLayer1 dal = null;
		public PassoutStudentsDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
        //public ResponeValues UpdatePassoutStudents(int UserId, BE.PassoutStudentsCollections dataColl)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    dal.BeginTransaction();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    try
        //    {
        //        foreach (var beData in dataColl)
        //        {
        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
        //            cmd.Parameters.AddWithValue("@PassOutClassId", beData.PassOutClassId);
        //            cmd.Parameters.AddWithValue("@PassOutSymbolNo", beData.PassOutSymbolNo);
        //            cmd.Parameters.AddWithValue("@PassOutGPA", beData.PassOutGPA);
        //            cmd.Parameters.AddWithValue("@PassOutGrade", beData.PassOutGrade);
        //            cmd.Parameters.AddWithValue("@PassOutRemarks", beData.PassOutRemarks);
        //            cmd.Parameters.AddWithValue("@UserId", UserId);
        //            cmd.CommandText = "usp_UpdatePassoutStudent";
        //            cmd.ExecuteNonQuery();
        //        }
        //        dal.CommitTransaction();
        //        resVal.IsSuccess = true;
        //        resVal.ResponseMSG = "Passout Student Updated";
        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        dal.RollbackTransaction();
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        dal.RollbackTransaction();
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }

        //    return resVal;

        //}

        public ResponeValues UpdatePassoutStudents(int UserId, List<AcademicLib.BE.PassoutStudents> DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsValid", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("StudentId", typeof(int));
                tableAllocation.Columns.Add("PassOutClassId", typeof(int));
                tableAllocation.Columns.Add("PassOutSymbolNo", typeof(string));
                tableAllocation.Columns.Add("PassOutGPA", typeof(float));
                tableAllocation.Columns.Add("PassOutGrade", typeof(string));
                tableAllocation.Columns.Add("PassOutRemarks", typeof(string));
                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["StudentId"] = v.StudentId;
                    row["PassOutClassId"] =IsDBNull(v.PassOutClassId);
                    row["PassOutSymbolNo"] = IsDBNull(v.PassOutSymbolNo);
                    row["PassOutGPA"] = IsDBNull(v.PassOutGPA);
                    row["PassOutGrade"] = IsDBNull(v.PassOutGrade);
                    row["PassOutRemarks"] = IsDBNull(v.PassOutRemarks);
                    tableAllocation.Rows.Add(row);
                }                

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@PassoutColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                
                cmd.CommandText = "usp_UpdatePassoutStudent";
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);

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


        public BE.StudentsForPassoutCollections getStudentListForPassout(int UserId, int ClassId, int? SectionId,int? AcademicYearId, bool All = true, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null,int? BranchId=null)
		{
			BE.StudentsForPassoutCollections dataColl = new BE.StudentsForPassoutCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@All", All);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetStudentListForPassout";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.StudentsForPassout beData = new BE.StudentsForPassout();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.IsLeft = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.BoardRegdNo = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.BoardTypeId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.BoardName = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.LeftDate_AD = reader.GetDateTime(12);
					if (!(reader[13] is DBNull)) beData.LeftDate_BS = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.LeftRemarks = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);

					if (!(reader[16] is DBNull)) beData.FatherContact = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.Address = reader.GetString(17);
					if (!(reader[18] is DBNull)) beData.Gender = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.Semester = reader.GetString(19);

					if (!(reader[20] is DBNull)) beData.ClassYear = reader.GetString(20);
					if (!(reader[21] is DBNull)) beData.SemesterId = reader.GetInt32(21);
					if (!(reader[22] is DBNull)) beData.ClassYearId = reader.GetInt32(22);
					if (!(reader[23] is DBNull)) beData.PassOutClassId = reader.GetInt32(23);
					if (!(reader[24] is DBNull)) beData.PassOutSymbolNo = reader.GetString(24);
					if (!(reader[25] is DBNull)) beData.PassOutGPA = reader.GetDouble(25);
					if (!(reader[26] is DBNull)) beData.PassOutGrade = reader.GetString(26);
					if (!(reader[27] is DBNull)) beData.PassOutRemarks = reader.GetString(27);
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

