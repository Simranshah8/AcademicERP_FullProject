using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class DefaultSetting : ResponeValues
    {
        public int? TranId { get; set; }
        public int? ModuleId { get; set; }
        public int? EntityNoId { get; set; }
        public int? SubEntityId { get; set; }
        public string SubEntityName { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
    public class DefaultSettingCollections : List<DefaultSetting>
    {
        public DefaultSettingCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
