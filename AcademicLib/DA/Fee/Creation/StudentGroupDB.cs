using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class StudentGroupDB
    {
        DataAccessLayer1 dal = null;
        public StudentGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
    }
}
