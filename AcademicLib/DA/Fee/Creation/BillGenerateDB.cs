using Dynamic.Dashboard.BE;
using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AcademicLib.DA.Fee.Creation
{
    internal class BillGenerateDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public BillGenerateDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues GenerateFine(int UserId,int AcademicYearId,int ForMonthId,bool IsRegenerate)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForMonthId", ForMonthId);
            cmd.Parameters.AddWithValue("@Regenerate", IsRegenerate);
            cmd.CommandText = "usp_GenerateAllClassFine";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Fine Generate Success";
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
        public ResponeValues SaveUpdate(int AcademicYearId,BE.Fee.Creation.BillGenerate beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@ForMonthId", beData.MonthId);
            cmd.Parameters.AddWithValue("@BillDate", beData.BillDate);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentTypeId", beData.StudentTypeId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            
            if (beData.DetailsColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("FeeItemId", typeof(int));
                table.Columns.Add("DisPer", typeof(double));
                table.Columns.Add("DisAmt", typeof(double));
                foreach (var v in beData.DetailsColl)
                {
                    if(v.FeeItemId.HasValue && v.FeeItemId.Value > 0 && (v.DiscountAmt>0 || v.DiscountPer>0))
                    {
                        var row = table.NewRow();
                        row["FeeItemId"] = v.FeeItemId.Value;
                        row["DisPer"] = v.DiscountPer;
                        row["DisAmt"] = v.DiscountAmt;
                        table.Rows.Add(row);
                    }
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@DisDetails", table);
                sqlParam.SqlDbType = SqlDbType.Structured;
                
            }
        
            cmd.CommandText = "usp_BillGenerate";
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Bill Generate Success";
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

        public ResponeValues SaveStudentWise(int AcademicYearId, BE.Fee.Creation.BillGenerate beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@FromMonthId", beData.FromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", beData.ToMonthId);
            cmd.Parameters.AddWithValue("@BillDate", beData.BillDate);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            //Added
            if (beData.DetailsColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("FeeItemId", typeof(int));
                table.Columns.Add("DisPer", typeof(double));
                table.Columns.Add("DisAmt", typeof(double));
                foreach (var v in beData.DetailsColl)
                {
                    if (v.FeeItemId.HasValue && v.FeeItemId.Value > 0 && (v.DiscountAmt > 0 || v.DiscountPer > 0))
                    {
                        var row = table.NewRow();
                        row["FeeItemId"] = v.FeeItemId.Value;
                        row["DisPer"] = v.DiscountPer;
                        row["DisAmt"] = v.DiscountAmt;
                        table.Rows.Add(row);
                    }
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@DisDetails", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }
            //Ends
            cmd.CommandText = "usp_BillGenerateStudentWise";
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);
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

        public RE.Fee.BillGenerateStudentWiseCollections getStudentWiseBillGenerateList(int UserId, int AcademicYearId, int EntityId)
        {
            RE.Fee.BillGenerateStudentWiseCollections dataColl = new RE.Fee.BillGenerateStudentWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentWiseBillGenerateList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.BillGenerateStudentWise beData = new RE.Fee.BillGenerateStudentWise();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.BillDateAD = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.BillDateBS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FromMonth = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.ToMonth = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.LogDateTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.LogDateBS = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.BillNoFrom = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.BillNoTo = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Amount = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.Total = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.FromMonthName = Convert.ToString(reader[19]);
                    if (!(reader[20] is DBNull)) beData.ToMonthName = Convert.ToString(reader[20]);
                    try
                    {
                        if (!(reader[21] is DBNull)) beData.Batch = reader.GetString(21);
                        if (!(reader[22] is DBNull)) beData.ClassYear = reader.GetString(22);
                        if (!(reader[23] is DBNull)) beData.Semester = reader.GetString(23);
                        if (!(reader[24] is DBNull)) beData.InvoiceNoFrom = reader.GetInt32(24);
                        if (!(reader[25] is DBNull)) beData.InvoiceNoTo = reader.GetInt32(25);
                    }
                    catch { }
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

        public RE.Fee.BillGenerateClassWiseCollections getClassWiseBillGenerateList(int UserId, int AcademicYearId, int EntityId)
        {
            RE.Fee.BillGenerateClassWiseCollections dataColl = new RE.Fee.BillGenerateClassWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetBillGenerateList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.BillGenerateClassWise beData = new RE.Fee.BillGenerateClassWise();
                    beData.ClassId = reader.GetInt32(0);
                    beData.ClassName = reader.GetString(1);
                    beData.YearId = reader.GetInt32(2);
                    beData.MonthId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.BillDateAD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.BillDateBS = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.BillNoFrom = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.BillNoTo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.NoOfStudent = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.TotalAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.GenerateBy = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LogDateTime = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.LogDateBS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.InvoiceNoFrom = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.InvoiceNoTo = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.SemesterId = Convert.ToInt32(reader[18]);
                    if (!(reader[19] is DBNull)) beData.ClassYearId = Convert.ToInt32(reader[19]);
                    if (!(reader[20] is DBNull)) beData.Semester = Convert.ToString(reader[20]);
                    if (!(reader[21] is DBNull)) beData.ClassYear = Convert.ToString(reader[21]);
                    if (!(reader[22] is DBNull)) beData.Batch = Convert.ToString(reader[22]);
                    if (!(reader[23] is DBNull)) beData.MonthName = Convert.ToString(reader[23]);
                    try
                    {
                        if (!(reader[24] is DBNull)) beData.BatchId = Convert.ToInt32(reader[24]);
                    }
                    catch { }

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

        public RE.Fee.BillGenerateFeeCollections getClassWiseFeeItem(int UserId, int AcademicYearId, int MonthId,int ClassId, int? SemesterId, int? ClassYearId)
        {
            RE.Fee.BillGenerateFeeCollections dataColl = new RE.Fee.BillGenerateFeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetClassBillGenerateSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.BillGenerateFee beData = new RE.Fee.BillGenerateFee();
                    beData.FeeItemId = reader.GetInt32(0);
                    beData.Name = reader.GetString(1);                                        
                    if (!(reader[2] is DBNull)) beData.Qty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Amount = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[7]);
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

        public RE.Fee.PendingBillGenerateCollections getPendingBillGenerateList(int UserId, int AcademicYearId)
        {
            RE.Fee.PendingBillGenerateCollections dataColl = new RE.Fee.PendingBillGenerateCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetPendingBillGenerateSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.PendingBillGenerate beData = new RE.Fee.PendingBillGenerate();
                    beData.ClassId = reader.GetInt32(0);
                    beData.MonthId = reader.GetInt32(1);
                    beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.MonthName = Convert.ToString(reader[4]);
                    try
                    {
                        if (!(reader[5] is DBNull)) beData.Batch = reader.GetString(5);
                        if (!(reader[6] is DBNull)) beData.ClassYear = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.Semester = reader.GetString(7);
                    }
                    catch { }
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

        public RE.Fee.StudentFeeItemDetailsCollections getStudentFeeDetails(int UserId, int AcademicYearId, int ClassId,int MonthId, int? SemesterId, int? ClassYearId, int? BatchId)
        {
            RE.Fee.StudentFeeItemDetailsCollections dataColl = new RE.Fee.StudentFeeItemDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetBGDForMonthClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.StudentFeeItemDetails beData = new RE.Fee.StudentFeeItemDetails();
                    beData.StudentId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FeeItemName = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Amount = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.BillNo = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.PDues = Convert.ToDouble(reader[12]);
                    try
                    {
                        if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.ClassYear = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.Semester = reader.GetString(15);
                        if (!(reader[16] is DBNull)) beData.InvoiceNo = Convert.ToString(reader[16]);
                    }
                    catch { }
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
        public ResponeValues Delete(int UserId, int AcademicYearId, int MonthId,int ClassId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_DelBillGenerateClassWise";
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
        public ResponeValues DeleteStudentWise(int UserId, int AcademicYearId, int StudentId,int FromMonthId,int ToMonthId,int? SemesterId,int? ClassYearId, int? BatchId = null)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@FromMonthId", FromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", ToMonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_DelBillGenerateStudentWise";
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
        public AcademicLib.BE.Fee.Creation.BillGenerate_SENTCollections getBillForSMS(int UserId, int AcademicYearId, int classId,int? sectionId,int fromMonthId,int toMonthId, int? batchId, int? facultyId, int? semesterId, int? classYearId)
        {
            AcademicLib.BE.Fee.Creation.BillGenerate_SENTCollections dataColl = new AcademicLib.BE.Fee.Creation.BillGenerate_SENTCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMonthId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@BatchId", batchId);
            cmd.Parameters.AddWithValue("@FacultyId", facultyId);
            cmd.Parameters.AddWithValue("@SemesterId", semesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", classYearId);

            cmd.CommandText = "usp_BillDetailsForSMS";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var beData = new AcademicLib.BE.Fee.Creation.BillGenerate_SENT();
                    if (!(reader[0] is DBNull)) beData.UserId = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.RollNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.BillNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.BillDate = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.BillMiti = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.PreviousDues = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.FeeAmount = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DiscountAmt = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.TaxAmt = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.FineAmt = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.CurrentFeeAmt = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.TotalDues = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ForMonth = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.EmailId = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.StudentId = reader.GetInt32(21);
                    dataColl.Add(beData);
                }

                reader.Close();

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public AcademicLib.RE.Fee.BillMissingStudentCollections getBillMissingStudent(int UserId, int AcademicYearId, int classId, int forMonth, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.RE.Fee.BillMissingStudentCollections dataColl = new RE.Fee.BillMissingStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@ForMonthId", forMonth);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetMissingBillGenerateStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var beData = new AcademicLib.RE.Fee.BillMissingStudent();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.StudentType = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.AdmitDate = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.AdmitMiti = reader.GetString(9);
                    try
                    {
                        if (!(reader[10] is DBNull)) beData.Batch = reader.GetString(10);
                        if (!(reader[11] is DBNull)) beData.ClassYear = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.Semester = reader.GetString(12);
                    }
                    catch { }
                     
                    dataColl.Add(beData);
                }

                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG =ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

        public ResponeValues billGenerateMissingStudent(int UserId, int AcademicYearId,int forMonth,string StudentIdColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentIdColl", StudentIdColl);
            cmd.Parameters.AddWithValue("@ForMonthId", forMonth);
            cmd.CommandText = "usp_BillGenerateForMissingStudent";
            try
            {
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Bill Generated Success";

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

        public ResponeValue Upload_TP_StudentStatement(int UserId,List<AcademicLib.Public_API.StudentStatement> dataColl)
        {
            ResponeValue resVal = new ResponeValue();

            try
            {
                dal.OpenConnection();
                dal.BeginTransaction();
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();                
                cmd.CommandText = "truncate table tbl_TP_StudentStatement";
                cmd.ExecuteNonQuery();
 
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.TableName = "tbl_TP_StudentStatement";                
                dt.Columns.Add(new System.Data.DataColumn("RegNo", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("VoucherDate", typeof(DateTime)));
                dt.Columns.Add(new System.Data.DataColumn("ForMonth", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Particular", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Debit", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("Credit", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("DrCr", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Balance", typeof(double)));
                dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("MonthId", typeof(int)));

                foreach (var dc in dataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();                    
                    dr["RegNo"] = dc.RegNo;
                    dr["VoucherDate"] = dc.VoucherDate;
                    dr["ForMonth"] = dc.ForMonth;
                    dr["Particular"] = dc.Particular;
                    dr["Debit"] = dc.Debit;
                    dr["Credit"] = dc.Credit;
                    dr["DrCr"] = dc.DrCr;
                    dr["Balance"] = dc.Balance;
                    dr["Remarks"] = dc.Remarks;
                    dr["MonthId"] = dc.MonthId;
                    dt.Rows.Add(dr);
                }
                System.Data.SqlClient.SqlBulkCopy objbulk = new System.Data.SqlClient.SqlBulkCopy(dal.Connection, System.Data.SqlClient.SqlBulkCopyOptions.Default, dal.Transaction);
                objbulk.DestinationTableName = "tbl_TP_StudentStatement";
                foreach (System.Data.DataColumn c in dt.Columns)
                {
                    objbulk.ColumnMappings.Add(c.ToString(), c.ToString());
                }
                objbulk.WriteToServer(dt);

                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Data Uploaded Success";
            }
            catch (Exception e)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = e.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return resVal;

        }

        public RE.Fee.BillGenerateFeeCollections GetStudentWiseFeeItem(int UserId, int AcademicYearId, int? FromMonthId, int? ToMonthId, int? StudentId)
        {
            RE.Fee.BillGenerateFeeCollections dataColl = new RE.Fee.BillGenerateFeeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FromMonthId", FromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", ToMonthId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_GetStdBillGenerateSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.BillGenerateFee beData = new RE.Fee.BillGenerateFee();
                    beData.FeeItemId = reader.GetInt32(0);
                    beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Qty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Amount = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[7]);
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

        public ResponeValues ImportBillGenerate(int UserId, List<BE.Fee.Creation.ImportBillGenerate> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;           
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

            if (dataColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                
                table.Columns.Add("FiscalYear", typeof(string));
                table.Columns.Add("Branch", typeof(string));
                table.Columns.Add("BillNo", typeof(int));
                table.Columns.Add("AcademicYear", typeof(string));
                table.Columns.Add("MonthId", typeof(int));
                table.Columns.Add("RegdNo", typeof(string));
                table.Columns.Add("BillDate_AD", typeof(DateTime));
                table.Columns.Add("BillDate_BS", typeof(string));
                table.Columns.Add("FeeItem", typeof(string)); 
                table.Columns.Add("Qty", typeof(double)); 
                table.Columns.Add("Rate", typeof(double)); 
                table.Columns.Add("Amount", typeof(double)); 
                table.Columns.Add("DisPer", typeof(double)); 
                table.Columns.Add("DisAmt", typeof(double)); 
                table.Columns.Add("TaxAmt", typeof(double)); 
                table.Columns.Add("FineAmt", typeof(double)); 
                table.Columns.Add("PayableAmt", typeof(double)); 
                table.Columns.Add("Remarks", typeof(string)); 
                foreach (var v in dataColl)
                {
                    var row = table.NewRow();
                    row["FiscalYear"] =IsDBNull(v.FiscalYear);
                    row["Branch"] = IsDBNull(v.Branch);
                    row["BillNo"] = IsDBNull(v.BillNo);
                    row["AcademicYear"] = IsDBNull(v.AcademicYear);
                    row["MonthId"] = IsDBNull(v.MonthId);
                    row["RegdNo"] = IsDBNull(v.RegdNo);
                    row["BillDate_AD"] = IsDBNull(v.BillDate_AD);
                    row["BillDate_BS"] = IsDBNull(v.BillDate_BS);
                    row["FeeItem"] = IsDBNull(v.FeeItem);
                    row["Qty"] =ZeroTo(v.Qty);
                    row["Rate"] = ZeroTo(v.Rate);
                    row["Amount"] = ZeroTo(v.Amount);
                    row["DisPer"] = ZeroTo(v.DisPer);
                    row["DisAmt"] = ZeroTo(v.DisAmt);
                    row["TaxAmt"] = ZeroTo(v.TaxAmt);
                    row["FineAmt"] = ZeroTo(v.FineAmt);
                    row["PayableAmt"] = ZeroTo(v.PayableAmt);
                    row["Remarks"] = IsDBNull(v.Remarks);
                    table.Rows.Add(row);
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@BillColl", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }

            cmd.CommandText = "usp_ImportBillGenerate"; 
            try
            {
                cmd.ExecuteNonQuery();
                
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if(resVal.IsSuccess)
                    dal.CommitTransaction();    
                else
                    dal.RollbackTransaction(); 
                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Bill Generate Success";
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
    }
}
