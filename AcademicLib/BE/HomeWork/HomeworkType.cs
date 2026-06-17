using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.HomeWork
{
    public class HomeworkType: ResponeValues
    {
        public int? HomeworkTypeId { get; set; }
        public int OrderNo { get; set; }
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";
    }
public class HomeworkTypeCollections: List<HomeworkType> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
