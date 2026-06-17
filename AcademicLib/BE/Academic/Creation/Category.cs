using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Category : Common
    {
        public int? CategoryId { get; set; }
        public int id
        {
            get
            {
                if (CategoryId.HasValue)
                    return CategoryId.Value;
                return 0;
            }
        }

    }

    public class CategoryCollections : System.Collections.Generic.List<Category>
    {
        public CategoryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
