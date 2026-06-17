using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Health
{
    public class HealthReport : ResponeValues
    {

        public HealthReport()
        {
            MonthlyTestColl = new AcademicERP.BE.Health.Transaction.MonthlyTestCollections();
            StoolTestColl = new AcademicERP.BE.Health.Transaction.StoolTestCollections();
            HealthGrowthColl = new AcademicERP.BE.Health.Transaction.HealthGrowthCollections();
            UrineTestColl = new AcademicERP.BE.Health.Transaction.UrineTestCollections();
            GeneralHealthColl = new AcademicERP.BE.Health.Transaction.GeneralHealthCollections();
        }

        public AcademicERP.BE.Health.Transaction.MonthlyTestCollections MonthlyTestColl { get; set; }
        public AcademicERP.BE.Health.Transaction.StoolTestCollections StoolTestColl { get; set; }
        public AcademicERP.BE.Health.Transaction.HealthGrowthCollections HealthGrowthColl { get; set; }

        public AcademicERP.BE.Health.Transaction.UrineTestCollections UrineTestColl { get; set; }
        public AcademicERP.BE.Health.Transaction.GeneralHealthCollections GeneralHealthColl { get; set; }

    }
}
