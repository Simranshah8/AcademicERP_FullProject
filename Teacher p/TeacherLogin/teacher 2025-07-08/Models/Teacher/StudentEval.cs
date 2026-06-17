using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StudentEval
    {
        public StudentEval()
        {
            AchievementColl = new AchievementDetailCollections();
            EvaluationBarColl = new EvaluationBarCollections();
        }
        public int? StudentId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string DOB_BS { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_Contact { get; set; }
        public string Height { get; set; }
        public string Weigth { get; set; }
        public string Photopath { get; set; }
        public string CompLogoPath { get; set; }
        public string ExamName { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public AchievementDetailCollections AchievementColl { get; set; }
        public EvaluationBarCollections EvaluationBarColl { get; set; }
    }

    public class StudentEvalCollections : System.Collections.Generic.List<StudentEval>
    {
        public StudentEvalCollections()
        {
            ResponseMSG = "";
            AchievementColl = new AchievementDetailCollections();
            EvaluationBarColl = new EvaluationBarCollections();
        }
        public AchievementDetailCollections AchievementColl { get; set; }
        public EvaluationBarCollections EvaluationBarColl { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AchievementDetail
    {
        public int? StudentId { get; set; }
        public string Remarks { get; set; }
        public double Point { get; set; }

    }
    public class AchievementDetailCollections : System.Collections.Generic.List<AchievementDetail>
    {
        public AchievementDetailCollections()
        {
            ResponseMSG = "";

        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class EvaluationBar
    {
        public int? StudentId { get; set; }
        public int? ExamTypeId { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public double Obtain { get; set; }
        public int ClassId { get; set; }
        public string ExamTypeName { get; set; }
    }
    public class EvaluationBarCollections : System.Collections.Generic.List<EvaluationBar>
    {
        public EvaluationBarCollections()
        {
            ResponseMSG = "";

        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}