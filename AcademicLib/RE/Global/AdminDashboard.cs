using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Global
{
    public class AdminDashboard : ResponeValues
    {
        public AdminDashboard()
        {
            HouseWiseStudentColl = new List<HouseWiseStudent>();
            FeeItemWiseDuesColl = new List<FeeItemWiseDues>();
            ClassWiseDuesColl = new List<ClassWiseDues>();
            RouteWiseStudentColl = new List<RouteWiseStudent>();
            VehicleColl = new List<Vehicle>();
            PublicationWiseBookColl = new List<LibraryBook>();
            UpcomingEventHolidayColl = new List<API.AppCMS.EventHoliday>();
        }

        public string Quotes { get; set; }
        public string QuotesPhotoPath { get; set; }
        public double Collection { get; set; }
        public double Discount { get; set; }
        public double Assests { get; set; }
        public double Expenses { get; set; }
        public double Income { get; set; }
        public double Liability { get; set; }
        public int Student_Male { get; set; }
        public int Student_Female { get; set; }
        public int Student_Other { get; set; }

        public int TotalStudent
        {
            get
            {
                return Student_Male + Student_Female + Student_Other;
            }
        }
        public int Student_TM_Male { get; set; }
        public int Student_TM_Female { get; set; } 
        public int Student_TM_Other { get; set; }

        public int TotalStudent_TM
        {
            get
            {
                return Student_TM_Male + Student_TM_Female + Student_TM_Other;
            }
        }

        public int Student_HM_Male { get; set; } 
        public int Student_HM_Female { get; set; }
        public int Student_HM_Other { get; set; }
        public int TotalStudent_HM
        {
            get
            {
                return Student_HM_Male + Student_HM_Female + Student_HM_Other;
            }
        }
        public double TotalFeeAmt { get; set; }
        public double TotalReceivedAmt { get; set; }
        public double TotalDiscountAmt { get; set; }
        public double TotalDuesAmt { get; set; }

        public double TotalFeeAmt_C { get; set; }
        public double TotalReceivedAmt_C { get; set; }
        public double TotalDiscountAmt_C { get; set; }
        public double TotalDuesAmt_C { get; set; }

        public int Emp_T_Male { get; set; }
        public int Emp_T_Female { get; set; }
        public int Emp_NT_Male { get; set; }
        public int Emp_NT_Female { get; set; }


        public string CurrentLoginIP { get; set; }
        public string CurrentLoginLocaion { get; set; }
        public DateTime? CurrentLoginAt_AD { get; set; }
        public string CurrentLoginAt_BS { get; set; }

        public string LastLoginIP { get; set; }
        public string LastLoginLocaion { get; set; }
        public DateTime? LastLoginAt_AD { get; set; }
        public string LastLoginAt_BS { get; set; }

        public int Notification_Student { get; set; }
        public int Notification_Teacher { get; set; }
        public int Notification_Admin { get; set; }

        public List<HouseWiseStudent> HouseWiseStudentColl { get; set; }
        public List<FeeItemWiseDues> FeeItemWiseDuesColl { get; set; }
        public List<ClassWiseDues> ClassWiseDuesColl { get; set; }
        public List<RouteWiseStudent> RouteWiseStudentColl { get; set; }
        public List<Vehicle> VehicleColl { get; set; }

        public List<LibraryBook> PublicationWiseBookColl { get; set; }
        public List<AcademicLib.API.AppCMS.EventHoliday> UpcomingEventHolidayColl { get; set; }

        public int TodayPresentBoys { get; set; }
        public int TodayPresentGirls { get; set; }
        public List<StudentMonthlyAttendanceSumm> StudentMonthlyAttColl { get; set; }
        public SMSBalance SMS { get; set; }
        public int? EmpTotalMale { get; set; }
        public int? EmpTotalFemale { get; set; }
        public int? TodayPresentMale { get; set; }
        public int? TodayPresentFemale { get; set; }
        public int? TodayLateMale { get; set; }
        public int? TodayLateFemale { get; set; }
        public int? TodayLeaveMale { get; set; }
        public int? TodayLeaveFemale { get; set; }
    }
    public class HouseWiseStudent
    {
        public string HouseName { get; set; }
        public int NoOfStudent { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Other { get; set; }
        public string CoOrdinatetor { get; set; }
        public string ContactNo { get; set; }
        public string PhotoPath { get; set; }
    }

    public class FeeItemWiseDues
    {
        public string FeeName { get; set; }
        public double FeeAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double DuesAmt { get; set; }
    }

    public class ClassWiseDues
    {
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FeeName { get; set; }
        public double FeeAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double DuesAmt { get; set; }
        public int NoOfStudent { get; set; }
        public string ClassTeacher { get; set; }
        public string ContactNo { get; set; }
        public string PhotoPath { get; set; }
    } 

    public class RouteWiseStudent
    {
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public int Capacity { get; set; }
        public string RouteName { get; set; }
        public int NoOfStudent { get; set; }
        public string DriverName { get; set; }
        public string ContactNo { get; set; }
        public string PhotoPath { get; set; }
    }

    public class Vehicle
    {
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public DateTime? RenewalDate_AD { get; set; }
        public string RenewalDate_BS { get; set; }
        public string JachPassNo { get; set; }
        public DateTime? JachPassValidUpto_AD { get; set; }
        public string JachPassValidUpto_BS { get; set; }
    }

    public class LibraryBook
    {
        public string PublicationName { get; set; }
        public int NoOfBook { get; set; }
        public int Issues { get; set; }
        public int Balance { get; set; }
        public int NotYetReturn { get; set; }
        public int BookLost { get; set; }
    }

    public class StudentMonthlyAttendanceSumm
    {
        public int NY { get; set; }
        public int NM { get; set; }
        public int TotalPresent { get;  set; }
        public int TotalStudent { get; set; }
        public double AvgStudentPresent { get; set; }
        public int TotalHoliday { get; set; }
        public int TotalWeekEnd { get; set; }
        public int DaysInMonth { get; set; }
    }

    public class SMSBalance
    {
        public int BalanceSMS { get; set; }
        public DateTime? ExpiredDateAD { get; set; }
        public string ExpiredDateBS { get; set; }
        public DateTime? LastDateAD { get; set; }
        public string LastDateBS { get; set; }
        public int LasTimeSend { get; set; }
    }

    public class EmployeeMonthlyAttendanceSum
    {
        public int NY { get; set; }
        public int NM { get; set; }
        public int TotalPresent { get; set; }
        public int TotalLate { get; set; }
        public int TotalLeave { get; set; }
        public int TotalAbsent { get; set; }
        public int TotalEmployee { get; set; }
        public bool? IsWeekend { get; set; }
        public bool? IsHoliday { get; set; }
    }
}
