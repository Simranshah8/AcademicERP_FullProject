using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{
	internal class CoordinatorDB
	{
		DataAccessLayer1 dal = null;
		public CoordinatorDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(int UserId, BE.Academic.Creation.CoordinatorCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			dal.BeginTransaction();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			try
			{
				//TODO: change in delete
				foreach (var fst in dataColl)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@EmployeeId", fst.EmployeeId);
					cmd.Parameters.AddWithValue("@BatchId", fst.BatchId);
					cmd.Parameters.AddWithValue("@ClassYearId", fst.ClassYearId);
					cmd.Parameters.AddWithValue("@SemesterId", fst.SemesterId);
					cmd.CommandText = "usp_DelCoordinatorDatabyId";
					cmd.ExecuteNonQuery();
				}
				//cmd.Parameters.AddWithValue("@EmployeeId", fst.EmployeeId);				
				//cmd.CommandText = "usp_DelCoordinatorDatabyId";
				//cmd.ExecuteNonQuery();
				foreach (var beData in dataColl)
				{
					if (beData.IsInclude)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
						cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
						cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
						cmd.Parameters.AddWithValue("@IsInclude", beData.IsInclude);
						//TODO: Added Field
						cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
						cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
						cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
						cmd.Parameters.AddWithValue("@UserId", UserId);
						cmd.CommandText = "usp_SaveUpdateCoordinator";
						cmd.ExecuteNonQuery();
					}
				}
				dal.CommitTransaction();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = "Coordinator data Saved";
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

		public AcademicLib.BE.Academic.Creation.CoordinatorCollections getAllCoordinatorClass(int UserId, int EmployeeId)
		{
			AcademicLib.BE.Academic.Creation.CoordinatorCollections dataColl = new AcademicLib.BE.Academic.Creation.CoordinatorCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.CommandText = "usp_GetClassListForCoordinator";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Academic.Creation.Coordinator beData = new BE.Academic.Creation.Coordinator();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.IsInclude = reader.GetBoolean(4);
					if (!(reader[5] is DBNull)) beData.BatchId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.Batch = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.ClassYear = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.Semester = reader.GetString(10);
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


		public BE.Academic.Creation.CoordinatorCollections getAllCoordinator(int UserId)
		{
			BE.Academic.Creation.CoordinatorCollections dataColl = new BE.Academic.Creation.CoordinatorCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.CommandText = "usp_GetAllClassCoordinatorList";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Academic.Creation.Coordinator beData = new BE.Academic.Creation.Coordinator();
					if (!(reader[0] is DBNull)) beData.Teacher = reader.GetString(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.EmployeeId = reader.GetInt32(3);					
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

		public ResponeValues DeleteById(int UserId,int EmployeeId)
		{
			ResponeValues resVal = new ResponeValues();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.CommandText = "usp_DelClassCoordinator";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[2].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

				if (!(cmd.Parameters[3].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

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

