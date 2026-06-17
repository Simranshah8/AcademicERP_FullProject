using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Library
{
    public class BarCode : Dynamic.Accounting.IReportLoadObjectData
    {

        public BarCode(List<AcademicLib.BE.Library.Transaction.PrintBarCode>  dataColl)
        {
            this.dataColl = dataColl;            
        }

        private List<AcademicLib.BE.Library.Transaction.PrintBarCode> dataColl = null;
        public System.Collections.IEnumerable DataColl
        {
            get
            {
                return dataColl;
            }
        }

        public string ReportPath
        {
            get
            {
                return "";
            }
        }
        public void GetDataOfCurrentRow(object obj, Dynamic.ReportEngine.RDL.Row row)
        {
            //SNo, AccessionNo, BarCode, BookTitle, ISBSNo, ClassName, Medium, Department 
            AcademicLib.BE.Library.Transaction.PrintBarCode csl = (AcademicLib.BE.Library.Transaction.PrintBarCode)obj;
            row.Data[0] = csl.SNo;
            row.Data[1] = csl.AccessionNo;
            row.Data[2] = csl.BarCode;
            row.Data[3] = csl.BookTitle;
            row.Data[4] = csl.ISBSNo;
            row.Data[5] = csl.ClassName;
            row.Data[6] = csl.Medium;
            row.Data[7] = csl.Department;
            
        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
