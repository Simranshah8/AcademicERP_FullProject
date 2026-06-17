using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademicLib.SQLJob
{
  [Serializable]
  public class SQLjob
  {
    public string job_id { get; set; }
    public string originating_server { get; set; }
    public string name { get; set; }
    public int enabled { get; set; }
    public string description { get; set; }
    public int category_id { get; set; }
    public DateTime date_created { get; set; }
    public DateTime date_modified { get; set; }
    public int version_number { get; set; }
  }
}
