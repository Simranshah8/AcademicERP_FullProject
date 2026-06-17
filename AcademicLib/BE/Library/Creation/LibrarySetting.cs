using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class Setup : ResponeValues
    {
        public Setup()
        {
            Student = new LibrarySetting();
            Teacher = new LibrarySetting();
            ClassWise = new List<LibraryClassWiseSetting>();
            CategoryWise = new List<LibraryCategoryWiseSetting>();
        }
        public LibrarySetting Student { get; set; }
        public LibrarySetting Teacher { get; set; }
        public List<LibraryClassWiseSetting>  ClassWise { get; set; }
        public List<LibraryCategoryWiseSetting> CategoryWise { get; set; }
    }
    public class LibrarySetting : ResponeValues
    {
        public LibrarySetting()
        {
            FineRuleColl = new List<LibraryFineRule>();
        }
        public int? TranId { get; set; }
        public int ForStudentTeacher { get; set; }
        public int BookLimit { get; set; }
        public int CreditDays { get; set; }
        public double FixFineAmount { get; set; }
        public double LateFineAmountPerDay { get; set; }
        public bool SlabWiseFine { get; set; }
        public List<LibraryFineRule> FineRuleColl { get; set; }
    }
    public class LibraryFineRule
    {
        public int FromDays { get; set; }
        public int ToDays { get; set; }
        public double FineAmount { get; set; }
    }

    public class LibraryClassWiseSetting : ResponeValues
    {
        public LibraryClassWiseSetting()
        {
            FineRuleColl = new List<LibraryFineRule>();
        }
        public int? TranId { get; set; }
        public int? ClassId { get; set; }
        public int BookLimit { get; set; }
        public int CreditDays { get; set; }
        public double FixFineAmount { get; set; }
        public double LateFineAmountPerDay { get; set; }
        public bool SlabWiseFine { get; set; }
        public List<LibraryFineRule> FineRuleColl { get; set; }

        //Added By Suresh on 21 Magh
        public int? BatchId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
    }
    public class LibraryClassWiseSettingCollections : System.Collections.Generic.List<LibraryClassWiseSetting>
    {

    }
    public class LibraryCategoryWiseSetting : ResponeValues
    {
        public LibraryCategoryWiseSetting()
        {
            FineRuleColl = new List<LibraryFineRule>();
        }
        public int? TranId { get; set; }
        public int CategoryId { get; set; }
        public int BookLimit { get; set; }
        public int CreditDays { get; set; }
        public double FixFineAmount { get; set; }
        public double LateFineAmountPerDay { get; set; }
        public bool SlabWiseFine { get; set; }
        public List<LibraryFineRule> FineRuleColl { get; set; }
    }
}
