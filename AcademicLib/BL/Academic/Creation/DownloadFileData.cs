using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class DownoadFileData
    {
        DA.Academic.Creation.DownloadFileDataDB db = null;
        int _UserId = 0;
        public DownoadFileData(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.DownloadFileDataDB(hostName, dbName);
        }
     
        public BE.Academic.Creation.StudentDocFile GetStudentDocFile(int EntityId, int StudentId)
        {
            return db.getStudentDocFileById(_UserId, EntityId, StudentId);
        }
        public BE.Academic.Creation.EmployeeDocFile GetEmployeeDocFile(int EntityId, int EmployeeId)
        {
            return db.getEmployeeDocFileById(_UserId, EntityId, EmployeeId);
        }

    }
}
