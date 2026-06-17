using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal class ThemesDB
    {
        DataAccessLayer1 dal = null;
        public ThemesDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Setup.Themes beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NoNavbarBorder", beData.NoNavbarBorder);
            cmd.Parameters.AddWithValue("@BodySmallText", beData.BodySmallText);
            cmd.Parameters.AddWithValue("@NavbarSmallText", beData.NavbarSmallText);
            cmd.Parameters.AddWithValue("@SidebarNavSmallText", beData.SidebarNavSmallText);
            cmd.Parameters.AddWithValue("@FooterSmallText", beData.FooterSmallText);
            cmd.Parameters.AddWithValue("@SidebarNavFlatStyle", beData.SidebarNavFlatStyle);
            cmd.Parameters.AddWithValue("@SidebarNavLegacyStyle", beData.SidebarNavLegacyStyle);
            cmd.Parameters.AddWithValue("@SidebarNavCompact", beData.SidebarNavCompact);
            cmd.Parameters.AddWithValue("@SidebarNavChildIndent", beData.SidebarNavChildIndent);
            cmd.Parameters.AddWithValue("@SidebarNavChildHideOnCollapse", beData.SidebarNavChildHideOnCollapse);
            cmd.Parameters.AddWithValue("@MainSidebarDisable", beData.MainSidebarDisable);
            cmd.Parameters.AddWithValue("@BrandSmallText", beData.BrandSmallText);
            cmd.Parameters.AddWithValue("@NavbarVariants", beData.NavbarVariants);
            cmd.Parameters.AddWithValue("@AccentColorVariants", beData.AccentColorVariants);
            cmd.Parameters.AddWithValue("@DarkSidebarVariants", beData.DarkSidebarVariants);
            cmd.Parameters.AddWithValue("@LightSidebarVariants", beData.LightSidebarVariants);
            cmd.Parameters.AddWithValue("@BrandLogoVariants", beData.BrandLogoVariants);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.CommandText = "usp_AddThemes";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[20].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[19].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[19].Value);

                if (!(cmd.Parameters[20].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[20].Value);

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[21].Value);

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
        public BE.Setup.Themes getThemes(int UserId)
        {
            BE.Setup.Themes beData = new BE.Setup.Themes();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetThemes";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Setup.Themes();
                    if (!(reader[0] is DBNull)) beData.NoNavbarBorder = reader.GetBoolean(0);
                    if (!(reader[1] is DBNull)) beData.BodySmallText = reader.GetBoolean(1);
                    if (!(reader[2] is DBNull)) beData.NavbarSmallText = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.SidebarNavSmallText = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.FooterSmallText = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.SidebarNavFlatStyle = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.SidebarNavLegacyStyle = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.SidebarNavCompact = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.SidebarNavChildIndent = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.SidebarNavChildHideOnCollapse = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.MainSidebarDisable = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.BrandSmallText = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.NavbarVariants = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.AccentColorVariants = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.DarkSidebarVariants = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.LightSidebarVariants = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BrandLogoVariants = reader.GetString(16);

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
