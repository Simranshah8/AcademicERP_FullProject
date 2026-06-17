using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace PivotalERP.Global.Helpers
{
    public static class DAHelper
    {
        public static int AddOutputParams(System.Data.SqlClient.SqlCommand cmd,bool returnIdPresent = false)
        {
            int tmpParamIdx = cmd.Parameters.Count;
            int curCount = 4;
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ReturnId", System.Data.SqlDbType.Int);

            for (int i = 0; i < curCount; i++)
                cmd.Parameters[tmpParamIdx + i].Direction = System.Data.ParameterDirection.Output;

            return tmpParamIdx;
        }

        public static void GetOutputParams(System.Data.SqlClient.SqlCommand cmd,int tmpParamIdx, ResponeValues resVal)
        {
            
            if (!(cmd.Parameters[tmpParamIdx + 0].Value is DBNull))
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[tmpParamIdx + 0].Value);

            if (!(cmd.Parameters[tmpParamIdx + 1].Value is DBNull))
                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[tmpParamIdx + 1].Value);

            if (!(cmd.Parameters[tmpParamIdx + 2].Value is DBNull))
                resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[tmpParamIdx + 2].Value);

            if (!(cmd.Parameters[tmpParamIdx + 3].Value is DBNull))
                resVal.RId = Convert.ToInt32(cmd.Parameters[tmpParamIdx + 3].Value);

            if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
        }
    }
}