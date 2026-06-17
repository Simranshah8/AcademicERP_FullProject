using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class Roles
    {
        DA.Setup.RolesDB db = null;
        int _UserId = 0;
        public Roles(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.RolesDB(hostName, dbName);
        }
      
        public RE.Setup.RolesCollections GetAllRoles(int? ForUserId)
        {
            return db.getAllRoles(_UserId, ForUserId);
        }

    }
}
