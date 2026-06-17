using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{

    public class Employee
    {
        public Employee()
        {
            EmployeeColl = new List<EmployeeDetail>();
        }
        public List<EmployeeDetail> EmployeeColl { get; set; }

        public int TotalEmployee
        {
            get
            {
                return EmployeeColl.Count;
            }
        }
        public int TotalTeaching
        {
            get
            {
                return EmployeeColl.Count(p1 => p1.IsTeaching == true);
            }
        }
        public int TotalNonTeaching
        {
            get
            {
                return EmployeeColl.Count(p1 => p1.IsTeaching == false);
            }
        }
        
        public dynamic DepartmentWise
        {
            get
            {
                try
                {
                    var qry = from e in EmployeeColl
                              group e by e.Department into g
                              orderby g.First().DepartmentSNo
                              select new
                              {
                                  Department = g.Key,
                                  EmployeeColl = g.OrderBy(p1 => p1.DesignationSNo)
                              };

                    return qry;
                }
                catch { return null; }
              
            }
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeDetail
    {
        public int SNo { get; set; }
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public string EmpCode { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public bool IsTeaching { get; set; }       
        public string PhotoPath { get; set; }
        public int DepartmentSNo { get; set; }
        public int DesignationSNo { get; set; }
    }
}
