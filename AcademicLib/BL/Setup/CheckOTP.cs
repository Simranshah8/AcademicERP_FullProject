using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class CheckOTP
    {
        DA.Setup.CheckOTPDB db = null;
        int _UserId = 0;
        public CheckOTP(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.CheckOTPDB(hostName, dbName);
        }      
        
        public ResponeValue GetcheckOTPById(string Username)
        {
            return db.CheckOTPEnabled(_UserId, Username);
        }
    }
}