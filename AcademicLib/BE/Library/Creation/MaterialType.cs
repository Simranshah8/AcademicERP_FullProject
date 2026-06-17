using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class MaterialType : ResponeValues
    {
        public int? MaterialTypeId { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public string Description { get; set; }        
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        
        public int id
        {
            get
            {
                if (MaterialTypeId.HasValue)
                    return MaterialTypeId.Value;
                return 0;
            }
        }

        public string text
        {
            get
            {
                return Name;
            }
        }

    }
    public class MaterialTypeCollections : System.Collections.Generic.List<MaterialType>
    {
        public MaterialTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}