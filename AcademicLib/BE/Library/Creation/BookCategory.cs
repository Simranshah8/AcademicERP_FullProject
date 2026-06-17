using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.BookCategory.Creation
{
    public class BookCategory : ResponeValues
    {
        public int? BookCategoryId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int OrderNo { get; set; }

    }
    public class BookCategoryColl : List<BookCategory>
    {
        public BookCategoryColl()
        {
            ResponseMSG = " ";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}