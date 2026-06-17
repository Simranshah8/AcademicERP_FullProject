using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class EQuickAccess
    {
        DA.Academic.Transaction.EQuickAccessDB db = null;
        int _UserId = 0;
        public EQuickAccess(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.EQuickAccessDB(hostName, dbName);
        }
        
        //public BE.EQuickAccess GetEQuickAccessById(int EntityId, int EmployeeId)
        //{
        //    return db.getEQuickAccessById(_UserId, EntityId, EmployeeId);
        //}

        public BE.Academic.Transaction.EmpAttachmentCollections GetEmpAttForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEAttForQuickAccessByyId(_UserId, EntityId,EmployeeId);
        }
        public BE.Academic.Transaction.EmpComplainCollections GetEmpComplainForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEmpComplainForQuickAccessByyId(_UserId, EntityId, EmployeeId);
        }

        //public BE.EmpRemarksCollections GetEmpRemarksForQuickAccess(int EntityId, int EmployeeId)
        //{
        //    return db.getEmpRemarksForQuickAccessByyId(_UserId, EntityId, EmployeeId);
        //}

        public BE.Academic.Transaction.EmpLeaveTakenCollections GetEmpLeaveTakenForQuickAccess(int EntityId, int EmployeeId)
        {
            return db.getEmpLeaveTakenForQuickAccessByyId(_UserId, EntityId, EmployeeId);
        }

        //Student Quick Access Starts
        public BE.Academic.Transaction.StudentComplainCollections GetStudentComplainForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentComplainForQuickAccessById(_UserId, EntityId, StudentId);
        }
        public BE.Academic.Transaction.StudentLeaveTakenCollections GetStudentLeaveTakenForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentLeaveTakenForQuickAccessById(_UserId, EntityId, StudentId);
        }

        public BE.Academic.Transaction.StudentAttachmentForQACollections GetStudentAttForQuickAccess(int EntityId, int StudentId)
        {
            return db.getStudentAttForQuickAccessById(_UserId, EntityId, StudentId);
        }
    }
}