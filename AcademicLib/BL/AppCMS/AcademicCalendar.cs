using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class AcademicCalendar
    {
        DA.AppCMS.Creation.AcademicCalendarDB db = null;
        int _UserId = 0;

        public AcademicCalendar(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.AcademicCalendarDB(hostName, dbName);
        }
        public AcademicLib.API.AppCMS.EventHolidayCollections getUpcomingEventHoliday(DateTime? fromDate,DateTime? toDate,int eType, string BranchCode)
        {
            return db.getUpcomingEventHoliday(_UserId,fromDate,toDate,eType,BranchCode);
        }
            public List<AcademicLib.BE.AppCMS.Creation.NepaliCalendar> getNepaliCalendar(int? YearId,string BranchCode)
        {
            List<AcademicLib.BE.AppCMS.Creation.NepaliCalendar> tmpDataColl = new List<BE.AppCMS.Creation.NepaliCalendar>();
            var dataColl= db.getNepaliCalendar(_UserId, YearId,BranchCode);

            var query = from dc in dataColl
                        group dc by dc.NM into g
                        orderby g.Key
                        select new AcademicLib.BE.AppCMS.Creation.NepaliCalendar
                        {
                            MonthId = g.Key,
                            MonthName=g.First().MonthName,
                            DaysInMonth=g.First().DaysInMonth,
                            StartDayId=g.First().StartDayId,
                            DataColl = g.AsEnumerable()                            
                        };

            foreach (var v in query)
            {
                v.EventColl = new List<BE.AppCMS.Creation.EventSummary>();

                for (int i = 1; i < v.StartDayId; i++)
                    v.BlankDaysColl.Add(" ");

                foreach(var d in v.DataColl)
                {
                    foreach (var e in d.EventColl)
                        v.EventColl.Add(e);
                }

                tmpDataColl.Add(v);
            }
                

            return tmpDataColl;
        }
    }
}
