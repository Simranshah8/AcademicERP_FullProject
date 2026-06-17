using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class StudentOpeningDB
    {
        DataAccessLayer1 dal = null;
        public StudentOpeningDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId, BE.Fee.Creation.StudentOpeningCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;
            int? semesterId = first.SemesterId;
            int? classYearId = first.ClassYearId;
            int? batchId = first.BatchId;
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                var query = from dc in dataColl
                            group dc by new { dc.BatchId,  dc.SemesterId, dc.ClassYearId } into g
                            select new
                            {
                                BatchId=g.Key.BatchId,
                                SemesterId=g.Key.SemesterId,
                                ClassYearId=g.Key.ClassYearId
                            };
                
                foreach(var q in query)
                {
                    semesterId = q.SemesterId;
                    classYearId = q.ClassYearId;
                    cmd.Parameters.Clear();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ClassId", classId);
                    cmd.Parameters.AddWithValue("@SectionId", sectionId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@SemesterId", semesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", classYearId);
                    cmd.Parameters.AddWithValue("@BatchId", batchId);
                    cmd.CommandText = "usp_DelStudentOpening";
                    cmd.ExecuteNonQuery();
                }
             
                //foreach (var beData in dataColl)
                //{
                //    if (beData.Amount != 0 && beData.StudentId!=0 && beData.FeeItemId!=0)
                //    {
                //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //        cmd.Parameters.Clear();
                //        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                //        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                //        cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                //        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                //        cmd.Parameters.AddWithValue("@UserId", UserId);
                //        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                //        cmd.CommandText = "usp_SaveStudentOpening";
                //        cmd.ExecuteNonQuery();
                //    }

                //}

                //select TranId,StudentId,FeeItemId,Amount,Remarks,CreateBy,AcademicYearId from tbl_StudentOpening
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.TableName = "tbl_StudentOpening";                
                dt.Columns.Add(new System.Data.DataColumn("StudentId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("FeeItemId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("Amount", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("CreateBy", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("AcademicYearId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SemesterId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("ClassYearId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("VoucherDate", typeof(DateTime)));
                foreach (var dc in dataColl)
                {
                    if (dc.StudentId > 0 && dc.Amount!=0 && dc.FeeItemId>0)
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        dr["StudentId"] = dc.StudentId;
                        dr["FeeItemId"] = dc.FeeItemId;
                        dr["Amount"] = dc.Amount;

                        if (string.IsNullOrEmpty(dc.Remarks))
                            dr["Remarks"] = "";
                        else
                            dr["Remarks"] = dc.Remarks;

                        dr["CreateBy"] = UserId;
                        dr["AcademicYearId"] = AcademicYearId;

                        if (dc.SemesterId.HasValue && dc.SemesterId > 0)
                            dr["SemesterId"] = dc.SemesterId;
                        else
                            dr["SemesterId"] = DBNull.Value;


                        if (dc.ClassYearId.HasValue && dc.ClassYearId > 0)
                            dr["ClassYearId"] = dc.ClassYearId;
                        else
                            dr["ClassYearId"] = DBNull.Value;

                        if (dc.VoucherDate.HasValue)
                            dr["VoucherDate"] = dc.VoucherDate.Value;
                        else
                            dr["VoucherDate"] = DBNull.Value;

                        dt.Rows.Add(dr);
                    }
                    
                }
                System.Data.SqlClient.SqlBulkCopy objbulk = new System.Data.SqlClient.SqlBulkCopy(dal.Connection, System.Data.SqlClient.SqlBulkCopyOptions.Default, dal.Transaction);
                objbulk.DestinationTableName = "tbl_StudentOpening";
                foreach (System.Data.DataColumn column in dt.Columns)
                {
                    objbulk.ColumnMappings.Add(column.ToString(), column.ToString());
                }
                objbulk.WriteToServer(dt, System.Data.DataRowState.Added);

                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateStudentOpeningToAc";
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Opening Saved";
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

        public ResponeValues SaveFeeItemWise(int UserId, int AcademicYearId, BE.Fee.Creation.StudentOpeningCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
         
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {              
                foreach (var beData in dataColl)
                {
                    if (beData.Amount != 0 && beData.StudentId>0)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                        cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                        cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                        cmd.Parameters.AddWithValue("@VoucherDate", beData.VoucherDate);
                        cmd.CommandText = "usp_SaveFeeWiseStudentOpening";
                        cmd.ExecuteNonQuery();
                    }
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "FeeWise Student Opening Saved";
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

        public BE.Fee.Creation.StudentOpeningCollections getStudentOpening(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId,int? FeeItemId,int? SemesterId,int? ClassYearId, int? BatchId)
        {
            BE.Fee.Creation.StudentOpeningCollections dataColl = new BE.Fee.Creation.StudentOpeningCollections();

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
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetStudentOpening";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {                    
                    BE.Fee.Creation.StudentOpening beData = new BE.Fee.Creation.StudentOpening();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Amount = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Remarks = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SemesterId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassYearId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.VoucherDate = reader.GetDateTime(6);
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

        public RE.Fee.ClassWiseOpeningCollections getClassWiseOpening(int UserId, int AcademicYearId)
        {
            RE.Fee.ClassWiseOpeningCollections dataColl = new RE.Fee.ClassWiseOpeningCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetRptStudentOpening";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.ClassWiseOpening beData = new RE.Fee.ClassWiseOpening();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FeeItemName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Amount = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.SemesterId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.ClassYearId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Semester = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ClassYear = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Batch = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.BatchId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.VoucherDate = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.VoucherMiti = reader.GetString(14);
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

        public RE.Fee.StudentOpeningCollections getClassWiseOpening(int UserId, int AcademicYearId, int ClassId,int? SectionId,int? SemesterId,int? ClassYearId,int? BatchId=null)
        {
            RE.Fee.StudentOpeningCollections dataColl = new RE.Fee.StudentOpeningCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetRptStudentOpeningDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.StudentOpening beData = new RE.Fee.StudentOpening();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.FeeItemName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Amount = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.VoucherDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.VoucherMiti = reader.GetString(10);
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
            cmd.CommandText = "usp_DelStudentOpening";
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

        public ResponeValues ClearStudentOpening(int UserId, int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_ClearStudentOpening";
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
