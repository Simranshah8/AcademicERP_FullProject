using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    public class RolesDB
    {
        DataAccessLayer1 dal = null;
        public RolesDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
    
        public AcademicLib.RE.Setup.RolesCollections getAllRoles(int UserId, int? ForUserId)
        {
            AcademicLib.RE.Setup.RolesCollections dataColl = new AcademicLib.RE.Setup.RolesCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
            cmd.CommandText = "usp_GetAllAppUserRoles";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Setup.Roles beData = new AcademicLib.RE.Setup.Roles();
                    if (!(reader[0] is DBNull)) beData.ForUser = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RoleId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                   
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