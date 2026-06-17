using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Hostel
{
    internal class BedMappingDB
    {
        DataAccessLayer1 dal = null;
        public BedMappingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId,int AcademicYearId, BE.Hostel.BedMappingCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues(); 
            dal.OpenConnection();
            dal.BeginTransaction();
            var first = dataColl.First();
            int classId = first.ClassId;
            int? sectionId = first.SectionId;
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@StudentId", first.StudentId);
                cmd.CommandText = "usp_DelBedMapping";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (beData.StudentId > 0)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@RoomId", beData.RoomId);
                        cmd.Parameters.AddWithValue("@AllotDate", beData.AllotDate);
                        cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                        cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                        cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                        cmd.Parameters.AddWithValue("@PayableAmt", beData.PayableAmt);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@BedNo", beData.BedNo);
                        cmd.Parameters.AddWithValue("@BedId", beData.BedId);
                        cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmd.CommandText = "usp_SaveBedMapping";
                        cmd.ExecuteNonQuery();
                        int tranId = Convert.ToInt32(cmd.Parameters[8].Value);
                        cmd.CommandType = System.Data.CommandType.Text;
                        foreach (var m in beData.MonthIdColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TranId", tranId);
                            cmd.Parameters.AddWithValue("@MonthId", m.MonthId);
                            cmd.Parameters.AddWithValue("@Rate", m.Rate);
                            cmd.Parameters.AddWithValue("@DiscountPer", m.DiscountPer);
                            cmd.Parameters.AddWithValue("@DiscountAmt", m.DiscountAmt);
                            cmd.Parameters.AddWithValue("@PayableAmt", m.PayableAmt);
                            cmd.Parameters.AddWithValue("@Remarks", m.Remarks);
                            cmd.CommandText = "insert into tbl_BedMappingMonth(TranId,MonthId,Rate,DiscountPer,DiscountAmt,PayableAmt,Remarks) values(@TranId,@MonthId,@Rate,@DiscountPer,@DiscountAmt,@PayableAmt,@Remarks)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                  
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Bed Mapping Saved";
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

        public ResponeValues SaveUpdateForMonth(int UserId, int AcademicYearId, int ForMonth)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
           
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ForMonth", ForMonth); 
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.CommandText = "usp_AddBedMappingForMonth";
                cmd.ExecuteNonQuery();

                 
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Bed Mapping Saved For Month";
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

        public BE.Hostel.BedMappingCollections getBedMapping(int UserId,int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            BE.Hostel.BedMappingCollections dataColl = new BE.Hostel.BedMappingCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetBedMapping";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Hostel.BedMapping beData = new BE.Hostel.BedMapping();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RoomId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AllotDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.BedNo = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.BedId = Convert.ToInt32(reader[8]);
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int sid = reader.GetInt32(0);
                    BE.Transport.Creation.TransportMappingMonth beData = new BE.Transport.Creation.TransportMappingMonth();
                    if (!(reader[1] is DBNull)) beData.MonthId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Rate = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Remarks = Convert.ToString(reader[6]);
                    dataColl.Find(p1 => p1.StudentId == sid).MonthIdColl.Add(beData);
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

        public ResponeValues Delete(int UserId, int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelBedMapping";
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

        public ResponeValues DeleteForMonth(int UserId, int AcademicYearId, int ForMonth)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForMonth", ForMonth); 
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelBedMappingForMonth";
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
        public AcademicLib.API.Admin.HostelStudentCollections admin_StudentList(int UserId, int? ClassId = null, int? SectionId = null,int? AcademicYearId=null)
        {
            AcademicLib.API.Admin.HostelStudentCollections dataColl = new API.Admin.HostelStudentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "ups_admin_HostelStudentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.HostelStudent beData = new API.Admin.HostelStudent();
                    beData.SNo = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.UserId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RollNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.RegNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.RoomName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.BedName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.BedNo = Convert.ToString(reader[12]);
                    if (!(reader[13] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.AllotDate_AD = Convert.ToDateTime(reader[14]);
                    if (!(reader[15] is DBNull)) beData.AllotDate_BS = Convert.ToString(reader[15]);
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

        public AcademicLib.RE.Hostel.StudentSummaryCollections getStudentSummaryList(int UserId, int AcademicYearId, string ClassIdColl, string SectionIdColl, string RoomIdColl, string BatchIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            AcademicLib.RE.Hostel.StudentSummaryCollections dataColl = new RE.Hostel.StudentSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassIdColl", ClassIdColl);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@RoomIdColl", RoomIdColl);
            //Added By Suresh on Magh 17
            cmd.Parameters.AddWithValue("@BatchIdColl", BatchIdColl);
            cmd.Parameters.AddWithValue("@SemesterIdColl", SemesterIdColl);
            cmd.Parameters.AddWithValue("@ClassYearIdColl", ClassYearIdColl);
            //Ends
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetHostelStudentSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Hostel.StudentSummary beData = new RE.Hostel.StudentSummary();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RegNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Gender = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.MotherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.M_ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Address = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PhotoPath = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BloodGroup = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.DOB_AD = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.DOB_BS = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.HouseName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Medium = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BoardName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.BoardRegNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.EnrollNo = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.CardNo = reader.GetInt64(24);
                    if (!(reader[25] is DBNull)) beData.BusStop = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.BusPoint = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.GuardianName = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Relation = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Address = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_ContacNo = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.UserName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.AdmitDate_AD = reader.GetDateTime(32);
                    if (!(reader[33] is DBNull)) beData.AdmitDate_BS = reader.GetString(33);

                    if (!(reader[34] is DBNull)) beData.RoomName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BedName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.BedNo = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.Rate = Convert.ToDouble(reader[37]);
                    if (!(reader[38] is DBNull)) beData.AllotDate = reader.GetDateTime(38);
                    if (!(reader[39] is DBNull)) beData.AllotMiti = reader.GetString(39);


                    if (!(reader[40] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.DebitAmt = Convert.ToDouble(reader[41]);
                    if (!(reader[42] is DBNull)) beData.CreditAmt = Convert.ToDouble(reader[42]);

                    //Added By Suresh
                    if (!(reader[43] is DBNull)) beData.BatchId = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.SemesterId = reader.GetInt32(44);
                    if (!(reader[45] is DBNull)) beData.ClassYearId = reader.GetInt32(45);
                    if (!(reader[46] is DBNull)) beData.Batch = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.Semester = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.ClassYear = reader.GetString(48);
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

        public AcademicLib.RE.Hostel.StudentSummaryCollections getStudentSummaryForMonth(int UserId, int AcademicYearId, int ForMonthId)
        {
            AcademicLib.RE.Hostel.StudentSummaryCollections dataColl = new RE.Hostel.StudentSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForMonthId", ForMonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetHostelStudentForMonth";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Hostel.StudentSummary beData = new RE.Hostel.StudentSummary();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoNumber = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RegNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.Gender = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.FatherName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.F_ContactNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.MotherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.M_ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ContactNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Address = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.PhotoPath = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BloodGroup = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.DOB_AD = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.DOB_BS = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.HouseName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Medium = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.BoardName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.BoardRegNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.EnrollNo = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.CardNo = reader.GetInt64(24);
                    if (!(reader[25] is DBNull)) beData.BusStop = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.BusPoint = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.GuardianName = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.G_Relation = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.G_Address = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.G_ContacNo = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.UserName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.AdmitDate_AD = reader.GetDateTime(32);
                    if (!(reader[33] is DBNull)) beData.AdmitDate_BS = reader.GetString(33);

                    if (!(reader[34] is DBNull)) beData.RoomName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BedName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.BedNo = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.Rate = Convert.ToDouble(reader[37]);
                    if (!(reader[38] is DBNull)) beData.AllotDate = reader.GetDateTime(38);
                    if (!(reader[39] is DBNull)) beData.AllotMiti = reader.GetString(39);

                
                    if (!(reader[40] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.DebitAmt = Convert.ToDouble(reader[41]);
                    if (!(reader[42] is DBNull)) beData.CreditAmt = Convert.ToDouble(reader[42]);
                    //Added By Suresh on Magh 14 starts
                    if (!(reader[43] is DBNull)) beData.Batch = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.ClassYear = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.Semester = reader.GetString(45);
                    //Ends
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
