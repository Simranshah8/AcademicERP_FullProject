using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Academic.Transaction
{
    public class TeacherWiseQuota
    {
        DA.Academic.Transaction.TeacherWiseQuotaDB db = null;
        int _UserId = 0;
        public TeacherWiseQuota(int UserId, string hostName, string dbName)
        {
            _UserId = UserId;
            db = new DA.Academic.Transaction.TeacherWiseQuotaDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId,int BranchId, BE.Academic.Transaction.TeacherWiseQuotaCollections DataColl)
        {
                return db.SaveUpdate(_UserId, AcademicYearId, BranchId, DataColl);
        }
        public BE.Academic.Transaction.TeacherWiseQuotaCollections GetTeacherWiseQuota(int? DepartmentId, int? EntityId)
        {
            return db.GetTeacherWiseQuota(_UserId, DepartmentId, EntityId);
        }
        public BE.Academic.Transaction.TeacherWiseQuotaCollections GetAllTeacherWiseQuota(int? DepartmentId,int? DesignationId, int? EntityId)
        {
            return db.GetAllTeacherWiseQuota(_UserId, DepartmentId, DesignationId, EntityId);
        }
        public RE.Academic.TeacherQuotaCollections GetAllTeacherQuota()
        {
            return db.GetAllTeacherQuota(_UserId);
        }
    }
}