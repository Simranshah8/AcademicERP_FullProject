using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal class AppFeaturesDB
    {
        DataAccessLayer1 dal = null;
        public AppFeaturesDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Setup.AppFeaturesCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            try
            {
                int? ForUserId = dataColl.FirstOrDefault()?.ForUserId;
                int ForUser = dataColl.FirstOrDefault()?.ForUser ?? 0;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ForUserId", ForUserId.HasValue ? (object)ForUserId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ForUser", ForUser);

                // Build dynamic delete query based on ForUser
                string deleteQuery = @"EXEC sp_set_session_context @key=N'UserId', @value=@UserId; ";

                // Collect unique EntityId values from the incoming data
                var entityIds = dataColl.Select(x => x.EntityId).Distinct().ToList();

                if (ForUser == 1) // Students: Delete based on ForUser and EntityId
                {
                    if (entityIds.Any())
                    {
                        // Delete records for ForUser = 1 and specific EntityIds
                        deleteQuery += "DELETE FROM tbl_AppFeatures WHERE ForUser = @ForUser AND EntityId IN (";
                        deleteQuery += string.Join(",", entityIds);
                        deleteQuery += ")";
                    }
                    else
                    {
                        // If no EntityIds provided, avoid deleting any records
                        deleteQuery += "SELECT 1;"; // No-op to avoid empty command
                    }
                }
                else if (ForUser == 2 || ForUser == 3) // Teachers and Admins: Delete based on ForUser, ForUserId (or UserId IS NULL), and EntityId
                {
                    if (entityIds.Any())
                    {
                        // Delete records for ForUser = 2 or 3, specific ForUserId (or UserId IS NULL), and specific EntityIds
                        deleteQuery += "DELETE FROM tbl_AppFeatures WHERE ForUser = @ForUser AND ISNULL(UserId, 0) = ISNULL(@ForUserId, 0) AND EntityId IN (";
                        deleteQuery += string.Join(",", entityIds);
                        deleteQuery += ")";
                    }
                    else
                    {
                        // If no EntityIds provided, avoid deleting any records
                        deleteQuery += "SELECT 1;"; // No-op to avoid empty command
                    }
                }
                else
                {
                    throw new Exception("Invalid ForUser value");
                }

                cmd.CommandText = deleteQuery;
                cmd.ExecuteNonQuery();

                // Prepare DataTable for bulk insert
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.TableName = "tbl_AppFeatures";
                dt.Columns.Add(new System.Data.DataColumn("UserId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("ForUser", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("ModuleName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("EntityId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("Name", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("IsActive", typeof(bool)));
                dt.Columns.Add(new System.Data.DataColumn("PActive", typeof(bool)));
                dt.Columns.Add(new System.Data.DataColumn("CreateBy", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("RoleId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("ClassId", typeof(int)));

                dt.Columns.Add(new System.Data.DataColumn("MOrderNo", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("EOrderNo", typeof(int)));

                foreach (var dc in dataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();
                    dr["UserId"] = dc.ForUserId.HasValue ? (object)dc.ForUserId.Value : DBNull.Value;
                    dr["ForUser"] = dc.ForUser;
                    dr["ModuleName"] = dc.ModuleName;
                    dr["EntityId"] = dc.EntityId;
                    dr["Name"] = dc.Name;
                    dr["IsActive"] = dc.IsActive;
                    dr["PActive"] = true;
                    dr["CreateBy"] = UserId;
                    dr["RoleId"] = dc.RoleId.HasValue ? (object)dc.RoleId.Value : DBNull.Value;
                    dr["ClassId"] = dc.ClassId.HasValue ? (object)dc.ClassId.Value : DBNull.Value;

                    dr["MOrderNo"] = dc.MOrderNo.HasValue ? (object)dc.MOrderNo.Value : DBNull.Value;
                    dr["EOrderNo"] = dc.EOrderNo.HasValue ? (object)dc.EOrderNo.Value : DBNull.Value;
                    dt.Rows.Add(dr);
                }

                System.Data.SqlClient.SqlBulkCopy objbulk = new System.Data.SqlClient.SqlBulkCopy(dal.Connection, System.Data.SqlClient.SqlBulkCopyOptions.Default, dal.Transaction);
                objbulk.DestinationTableName = "tbl_AppFeatures";
                foreach (System.Data.DataColumn c in dt.Columns)
                {
                    objbulk.ColumnMappings.Add(c.ToString(), c.ToString());
                }
                objbulk.WriteToServer(dt);

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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

        public BE.Setup.AppFeaturesCollections getAllAppFeatures(int UserId, int? ForUserId)
        {
            BE.Setup.AppFeaturesCollections dataColl = new BE.Setup.AppFeaturesCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);

            cmd.CommandText = "usp_GetAllAppFeatures";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.AppFeatures beData = new BE.Setup.AppFeatures();
                    beData.ForUser = reader.GetInt32(0);
                    beData.ModuleName = reader.GetString(1);
                    beData.EntityId = reader.GetInt32(2);
                    beData.Name = reader.GetString(3); ;
                    beData.IsActive = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull))
                    {
                        if (beData.ForUser == 1)
                            beData.ClassId = reader.GetInt32(5); // ClassOrRoleId maps to ClassId for students
                        else
                            beData.RoleId = reader.GetInt32(5); // ClassOrRoleId maps to RoleId for teachers/admins
                    }
                    if (reader[6] is DBNull) beData.MOrderNo = null;
                    else
                        beData.MOrderNo = reader.GetInt32(6);

                    if (reader[7] is DBNull) beData.EOrderNo = null;
                    else
                        beData.EOrderNo = reader.GetInt32(7);
                    beData.ForUserId = ForUserId;
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

        public BE.Setup.AppFeaturesCollections getAppFeatures(int UserId)
        {
            BE.Setup.AppFeaturesCollections dataColl = new BE.Setup.AppFeaturesCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAppFeatures";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.AppFeatures beData = new BE.Setup.AppFeatures();
                    beData.ForUser = reader.GetInt32(0);
                    beData.ModuleName = reader.GetString(1);
                    beData.EntityId = reader.GetInt32(2);
                    beData.Name = reader.GetString(3); ;
                    beData.IsActive = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.MOrderNo = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) beData.EOrderNo = Convert.ToInt32(reader[6]);

                    if (!(reader[7] is DBNull)) beData.SubjectTeacher = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.ClassTeacher = Convert.ToBoolean(reader[8]);
                    if (!(reader[9] is DBNull)) beData.CoOrdinator = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.HOD = Convert.ToBoolean(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Role = Convert.ToString(reader[11]);

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
