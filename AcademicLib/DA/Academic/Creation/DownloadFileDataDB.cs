using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{
    internal class DownloadFileDataDB
    {
        DataAccessLayer1 dal = null;
        public DownloadFileDataDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        
        public BE.Academic.Creation.StudentDocFile getStudentDocFileById(int UserId, int EntityId, int StudentId)
        {
            BE.Academic.Creation.StudentDocFile beData = new BE.Academic.Creation.StudentDocFile();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStudentFilesDetailById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.StudentDocFile();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Email = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.PhotoPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SignaturePath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SectionName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RollNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.PA_FullAddress = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Batch = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Semester = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClassYear = reader.GetString(13);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    beData.AttachmentColl.Add(det);
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


        public BE.Academic.Creation.EmployeeDocFile getEmployeeDocFileById(int UserId, int EntityId, int EmployeeId)
        {
            BE.Academic.Creation.EmployeeDocFile beData = new BE.Academic.Creation.EmployeeDocFile();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeFilesDetailById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.EmployeeDocFile();
                    beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EmployeeCode = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.OfficeEmailId = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.PhotoPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SignaturePath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.PA_FullAddress = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Department = reader.GetString(8);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
                    beData.AttachmentColl.Add(det);
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
    }
}
