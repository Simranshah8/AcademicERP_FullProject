using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal  class YearClosingDB
    {
        DataAccessLayer1 dal = null;
        public YearClosingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamAs", beData.ExamAs);
                cmd.Parameters.AddWithValue("@PromoteTo", beData.PromoteTo);
                cmd.Parameters.AddWithValue("@RollNoAs", beData.RollNoAs);
                cmd.Parameters.AddWithValue("@SectionAs", beData.SectionAs);
                cmd.Parameters.AddWithValue("@FromAcademicYearId", beData.FromAcademicYearId);
                cmd.Parameters.AddWithValue("@ToAcademicYearId", beData.ToAcademicYearId);
                cmd.Parameters.AddWithValue("@CostClassId", beData.CostClassId);

                cmd.Parameters.AddWithValue("@ForwardBedMapping", beData.ForwardBedMapping);
                cmd.Parameters.AddWithValue("@ForwardFeeMapping", beData.ForwardFeeMapping);
                cmd.Parameters.AddWithValue("@ForwardTransportMapping", beData.ForwardTransportMapping);
                cmd.Parameters.AddWithValue("@OpeningFeeHeadingId", beData.OpeningFeeHeadingId);
                cmd.Parameters.AddWithValue("@ForwardDiscountSetup", beData.ForwardDiscountSetup);

                cmd.Parameters.AddWithValue("@ForwardOpening", beData.ForwardOpening);
                cmd.Parameters.AddWithValue("@ForwardClassSchedule", beData.ForwardClassSchedule);
                cmd.Parameters.AddWithValue("@StudentIdColl", beData.StudentIdColl);

                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("FromClassId", typeof(int));
                tableAllocation.Columns.Add("ToClassId", typeof(int));
                tableAllocation.Columns.Add("ExamTypeId", typeof(int));
                tableAllocation.Columns.Add("ExamTypeGroupId", typeof(int));
                tableAllocation.Columns.Add("FromSemesterId", typeof(int));
                tableAllocation.Columns.Add("ToSemesterId", typeof(int));
                tableAllocation.Columns.Add("FromClassYearId", typeof(int));
                tableAllocation.Columns.Add("ToClassYearId", typeof(int));
                tableAllocation.Columns.Add("BatchId", typeof(int));
                if (beData.ClassColl != null)
                {
                    foreach (var v in beData.ClassColl)
                    {
                        if(v.FromClassId>0 && v.ToClassId > 0)
                        {
                            var row = tableAllocation.NewRow();

                            row["FromClassId"] = v.FromClassId;
                            row["ToClassId"] = v.ToClassId;
                             
                            if (v.ExamTypeId.HasValue)
                                row["ExamTypeId"] = v.ExamTypeId;
                            else
                                row["ExamTypeId"] = 0;

                            if (v.ExamTypeGroupId.HasValue)
                                row["ExamTypeGroupId"] = v.ExamTypeGroupId;
                            else
                                row["ExamTypeGroupId"] = 0;


                            if (v.FromSemesterId.HasValue)
                                row["FromSemesterId"] = v.FromSemesterId;
                            else
                                row["FromSemesterId"] = 0;

                            if (v.ToSemesterId.HasValue)
                                row["ToSemesterId"] = v.ToSemesterId;
                            else
                                row["ToSemesterId"] = 0;

                            if (v.FromClassYearId.HasValue)
                                row["FromClassYearId"] = v.FromClassYearId;
                            else
                                row["FromClassYearId"] = 0;

                            if (v.ToClassYearId.HasValue)
                                row["ToClassYearId"] = v.ToClassYearId;
                            else
                                row["ToClassYearId"] = 0;
                             

                            if (v.BatchId.HasValue)
                                row["BatchId"] = v.BatchId;
                            else
                                row["BatchId"] = 0;

                            tableAllocation.Rows.Add(row);
                        }                       
                    }
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@ClassColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                 
                cmd.CommandText = "usp_ClassUpgrade";
                cmd.ExecuteNonQuery();
                
                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Class Upgrade Done";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues ReSaveUpdate(int UserId, AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamAs", beData.ExamAs);
                cmd.Parameters.AddWithValue("@PromoteTo", beData.PromoteTo);
                cmd.Parameters.AddWithValue("@RollNoAs", beData.RollNoAs);
                cmd.Parameters.AddWithValue("@SectionAs", beData.SectionAs);
                cmd.Parameters.AddWithValue("@FromAcademicYearId", beData.FromAcademicYearId);
                cmd.Parameters.AddWithValue("@ToAcademicYearId", beData.ToAcademicYearId);
                cmd.Parameters.AddWithValue("@CostClassId", beData.CostClassId);

                cmd.Parameters.AddWithValue("@ForwardBedMapping", beData.ForwardBedMapping);
                cmd.Parameters.AddWithValue("@ForwardFeeMapping", beData.ForwardFeeMapping);
                cmd.Parameters.AddWithValue("@ForwardTransportMapping", beData.ForwardTransportMapping);
                cmd.Parameters.AddWithValue("@OpeningFeeHeadingId", beData.OpeningFeeHeadingId);
                cmd.Parameters.AddWithValue("@ForwardDiscountSetup", beData.ForwardDiscountSetup);
                cmd.Parameters.AddWithValue("@ForwardOpening", beData.ForwardOpening);

                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("FromClassId", typeof(int));
                tableAllocation.Columns.Add("ToClassId", typeof(int));
                tableAllocation.Columns.Add("ExamTypeId", typeof(int));
                tableAllocation.Columns.Add("ExamTypeGroupId", typeof(int));
                tableAllocation.Columns.Add("FromSemesterId", typeof(int));
                tableAllocation.Columns.Add("ToSemesterId", typeof(int));
                tableAllocation.Columns.Add("FromClassYearId", typeof(int));
                tableAllocation.Columns.Add("ToClassYearId", typeof(int));
                tableAllocation.Columns.Add("BatchId", typeof(int));
                if (beData.ClassColl != null)
                {
                    foreach (var v in beData.ClassColl)
                    {
                        if (v.FromClassId > 0 && v.ToClassId > 0)
                        {
                            var row = tableAllocation.NewRow();

                            row["FromClassId"] = v.FromClassId;
                            row["ToClassId"] = v.ToClassId;

                            if (v.ExamTypeId.HasValue)
                                row["ExamTypeId"] = v.ExamTypeId;
                            else
                                row["ExamTypeId"] = 0;

                            if (v.ExamTypeGroupId.HasValue)
                                row["ExamTypeGroupId"] = v.ExamTypeGroupId;
                            else
                                row["ExamTypeGroupId"] = 0;


                            if (v.FromSemesterId.HasValue)
                                row["FromSemesterId"] = v.FromSemesterId;
                            else
                                row["FromSemesterId"] = 0;

                            if (v.ToSemesterId.HasValue)
                                row["ToSemesterId"] = v.ToSemesterId;
                            else
                                row["ToSemesterId"] = 0;

                            if (v.FromClassYearId.HasValue)
                                row["FromClassYearId"] = v.FromClassYearId;
                            else
                                row["FromClassYearId"] = 0;

                            if (v.ToClassYearId.HasValue)
                                row["ToClassYearId"] = v.ToClassYearId;
                            else
                                row["ToClassYearId"] = 0;
                             
                            if (v.BatchId.HasValue)
                                row["BatchId"] = v.BatchId;
                            else
                                row["BatchId"] = 0;

                            tableAllocation.Rows.Add(row);
                        }
                    }
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@ClassColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;

                cmd.CommandText = "usp_ReClassUpgrade";
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Class Re-Closing Done";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues ReClosingStudentFee(int UserId, AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@FromAcademicYearId", beData.FromAcademicYearId);
                cmd.Parameters.AddWithValue("@ToAcademicYearId", beData.ToAcademicYearId);
                cmd.Parameters.AddWithValue("@OpeningFeeHeadingId", beData.OpeningFeeHeadingId);
   
                cmd.CommandText = "usp_ReStudentOpeningTransfer";
                cmd.ExecuteNonQuery();

              
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Re-Closing And Opening Transfer Success";

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

        public ResponeValues ReClosingStudentTransport(int UserId, AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@FromAcademicYearId", beData.FromAcademicYearId);
                cmd.Parameters.AddWithValue("@ToAcademicYearId", beData.ToAcademicYearId); 

                cmd.CommandText = "usp_ReTransportMappingClosing";
                cmd.ExecuteNonQuery();


                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Re-Closing And Transport Mapping Success";

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

        public ResponeValues UpdateAcademicYear(int UserId, AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ToAcademicYearId", beData.ToAcademicYearId);
                cmd.CommandText = "usp_UpdateAcademicYearToAll";
                cmd.ExecuteNonQuery();

                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Academic Year Update Success";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
                dal.RollbackTransaction();
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues generateOTP(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection(); 
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId); 
                cmd.CommandText = "usp_GenerateYearClosingOTP";
                cmd.ExecuteNonQuery();
                                  
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "OTP Sent To Account Department. please contact at 9801050665,9801591142.";

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
