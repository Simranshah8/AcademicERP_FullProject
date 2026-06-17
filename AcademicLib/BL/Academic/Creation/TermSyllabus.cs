using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

    public class TermSyllabus
    {

        DA.Academic.Creation.SyllabusDB db = null;

        int _UserId = 0;

        public TermSyllabus(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.SyllabusDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(List<BE.Academic.Creation.TermSyllabus> dataColl)
        {

            ResponeValues resVal = new ResponeValues();
            resVal = db.SaveUpdate(_UserId, dataColl);

            return resVal;
        }
        public BE.Academic.Creation.TermSyllabusColl GetAllSyllabus(int EntityId)
        {
            return db.getAllSyllabus(_UserId, EntityId);
        }

        public BE.Academic.Creation.TermSyllabusColl GetClassSubjectwiseTermSyllabus(int EntityId, int? BatchId, int? ClassId, int? SemesterId, int? ClassYearId, int? ExamTypeId, int? SubjectId)
        {
            return db.getAllClassSubjectWiseTermSyllabus(_UserId, EntityId, BatchId, ClassId, SemesterId, ClassYearId, ExamTypeId, SubjectId);
        }
    }

}

