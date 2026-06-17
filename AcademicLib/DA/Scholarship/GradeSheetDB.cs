using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Scholarship
{
    internal class GradeSheetDB
	{
		DataAccessLayer1 dal = null;
		public GradeSheetDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}


        public BE.Scholarship.GradeSheetCollections getGradeSheet(TableFilter filter,  double? GPA, string Alphabet = "", string SEESymbolNo = "")
        {
            BE.Scholarship.GradeSheetCollections dataColl = new BE.Scholarship.GradeSheetCollections();

            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", filter.UserId);
                cmd.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                cmd.Parameters.AddWithValue("@RowsOfPage", filter.RowsOfPage);
                cmd.Parameters.AddWithValue("@SearchType", filter.SearchType);
                cmd.Parameters.AddWithValue("@SearchCol", filter.SearchCol);
                cmd.Parameters.AddWithValue("@SearchVal", filter.SearchVal);
                cmd.Parameters.AddWithValue("@SortingCol", filter.SortingCol);
                cmd.Parameters.AddWithValue("@SortType", filter.SortType);
                cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
                cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_GetGradeSheet";
                cmd.Parameters.AddWithValue("@DateFrom", filter.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", filter.DateTo);
                cmd.Parameters.AddWithValue("@GPA", GPA);
                cmd.Parameters.AddWithValue("@Alphabet", Alphabet);
                cmd.Parameters.AddWithValue("@SEESymbolNo", SEESymbolNo);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Scholarship.GradeSheet beData = new BE.Scholarship.GradeSheet();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.DOB_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SEESymbolNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Alphabet = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.GPA = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.SchoolName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);

                    dataColl.Add(beData);
                }
                reader.Close();

                if (!(cmd.Parameters[8].Value is DBNull))
                    dataColl.TotalRows = Convert.ToInt32(cmd.Parameters[8].Value);

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


        public ResponeValues DeleteGradeSheet(int UserId, DateTime? DateForm, DateTime? DateTo, double? GPA, string Alphabet, string SEESymbolNo)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@DateForm", DateForm);
			cmd.Parameters.AddWithValue("@DateTo", DateTo);
			cmd.Parameters.AddWithValue("@GPA", GPA);
			cmd.Parameters.AddWithValue("@Alphabet", Alphabet);
			cmd.Parameters.AddWithValue("@SEESymbolNo", SEESymbolNo);
			cmd.CommandText = "usp_DelGradeSheet";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
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
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

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