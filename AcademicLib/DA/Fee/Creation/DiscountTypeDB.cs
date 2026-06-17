using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class DiscountTypeDB
    {
        DataAccessLayer1 dal = null;
        public DiscountTypeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Fee.Creation.DiscountType beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Code", beData.Code);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@DiscountTypeId", beData.DiscountTypeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateDiscountType";
            }
            else
            {
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddDiscountType";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if(resVal.IsSuccess && resVal.RId > 0)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    int sno = 1;
                    foreach (var m in beData.DetailsColl)
                    {
                        if(m.FeeItemId.HasValue && m.FeeItemId.Value > 0)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@DiscountTypeId", resVal.RId);
                            cmd.Parameters.AddWithValue("@SNo", sno);
                            cmd.Parameters.AddWithValue("@FeeItemId", m.FeeItemId);
                            cmd.Parameters.AddWithValue("@DiscountPer", m.DiscountPer);
                            cmd.Parameters.AddWithValue("@DiscountAmt", m.DiscountAmt);
                            cmd.CommandText = "insert into tbl_DiscountTypeDetails(DiscountTypeId,SNo,FeeItemId,DiscountPer,DiscountAmt) values(@DiscountTypeId,@SNo,@FeeItemId,@DiscountPer,@DiscountAmt)";
                            cmd.ExecuteNonQuery();
                            int dtranId = dal.GetInsertId(cmd, "tbl_DiscountTypeDetails");
                            if (dtranId > 0)
                            {
                                foreach (var v in m.MonthIdColl)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@DTranId", dtranId);
                                    cmd.Parameters.AddWithValue("@MonthId", v);
                                    cmd.CommandText = "insert into tbl_DiscountTypeMonth(DTranId,MonthId) values(@DTranId,@MonthId)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            sno++;
                        }
                        
                    }
                }
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
        public BE.Fee.Creation.DiscountTypeCollections getAllDiscountType(int UserId, int EntityId)
        {
            BE.Fee.Creation.DiscountTypeCollections dataColl = new BE.Fee.Creation.DiscountTypeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllDiscountType";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.DiscountType beData = new BE.Fee.Creation.DiscountType();
                    beData.DiscountTypeId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.IsActive = reader.GetBoolean(3);
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
        public BE.Fee.Creation.DiscountType getDiscountTypeById(int UserId, int EntityId, int DiscountTypeId)
        {
            BE.Fee.Creation.DiscountType beData = new BE.Fee.Creation.DiscountType();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DiscountTypeId", DiscountTypeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetDiscountTypeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Fee.Creation.DiscountType();
                    beData.DiscountTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.IsActive = reader.GetBoolean(3);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Fee.Creation.DiscountTypeDetails det = new BE.Fee.Creation.DiscountTypeDetails();
                    det.DTranId = reader.GetInt32(0);
                    det.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.DiscountPer = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) det.DiscountAmt = Convert.ToDouble(reader[3]);
                    beData.DetailsColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int dtranId = reader.GetInt32(0);
                    int mid = reader.GetInt32(1);
                    beData.DetailsColl.Find(p1 => p1.DTranId == dtranId).MonthIdColl.Add(mid);
                }

                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }
        public ResponeValues DeleteById(int UserId, int EntityId, int DiscountTypeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DiscountTypeId", DiscountTypeId);
            cmd.CommandText = "usp_DelDiscountTypeById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

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

        public RE.Fee.DiscountStudentCollections getDiscountStudentList(int UserId,int AcademicYearId,string ClassIdColl,string FeeItemIdColl)
        {
            RE.Fee.DiscountStudentCollections dataColl = new RE.Fee.DiscountStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", FeeItemIdColl);
            cmd.CommandText = "usp_GetDiscountStudentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.DiscountStudent beData = new RE.Fee.DiscountStudent();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.F_ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Caste = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TranType = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DiscountType = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Details = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.TransportPoint = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TransportRoute = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.RoomName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.IsLeft = reader.GetBoolean(18);
                    if (!(reader[19] is DBNull)) beData.UserId = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.Semester = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.ClassYear = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Batch = reader.GetString(22);
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
