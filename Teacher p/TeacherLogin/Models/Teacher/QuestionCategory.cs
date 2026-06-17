using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class QuestionCategory
    {
        public int CategoryId { get; set; }
        public int ExamModalType { get; set; }
        public int OrderNo { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int NumberingMethod { get; set; }
     
    }

    public class CategoryId
    {
        public int categoryId { get; set; }
    }
}