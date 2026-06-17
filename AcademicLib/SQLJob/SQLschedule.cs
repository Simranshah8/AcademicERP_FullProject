using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademicLib.SQLJob
{
  [Serializable]
  public class SQLschedule
  {
    public int schedule_id { get; set; }
    public string schedule_uid { get; set; }
    public int originating_server_id { get; set; }
    public string name { get; set; }
    public string owner_sid { get; set; }
    public int enabled { get; set; }
    public int freq_type { get; set; }
    public int freq_interval { get; set; }
    public int freq_subday_type { get; set; }
    public int freq_subday_interval { get; set; }
    public int freq_relative_interval { get; set; }
    public int freq_recurrence_factor { get; set; }
    public DateTime active_start_date { get; set; }
    public DateTime active_end_date { get; set; }
    public DateTime active_start_time { get; set; }
    public DateTime active_end_time { get; set; }
    public DateTime date_created { get; set; }
    public DateTime date_modified { get; set; }
    public int version_number { get; set; }
  }
}
