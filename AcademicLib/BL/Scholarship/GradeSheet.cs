using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Scholarship
{
    public class GradeSheet
    {
        DA.Scholarship.GradeSheetDB db = null;
        int _UserId = 0;
        public GradeSheet(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Scholarship.GradeSheetDB(hostName, dbName);
        }

        public BE.Scholarship.GradeSheetCollections getGradeSheet(TableFilter filter, double? GPA, string Alphabet = "", string SEESymbolNo = "")
        {
            return db.getGradeSheet(filter, GPA, Alphabet, SEESymbolNo);
        }
        public ResponeValues DeleteGradeSheet(DateTime? DateForm, DateTime? DateTo, double? GPA, string Alphabet, string SEESymbolNo)
        {
            return db.DeleteGradeSheet(_UserId,DateForm, DateTo, GPA, Alphabet, SEESymbolNo);
        }


    }
}