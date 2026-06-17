using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AcademicLib.SQLJob
{
  public class API
  {
    // Retrieve the SQLjobs pertaining to a given set of categories
    // CategoriesList is a pipe-delimited list of job categories: if specified (not an empty string),
    // the returned list will be limited to the only jobs belonging to those categories
    public static SQLjob[] LoadSQLjobs(string CategoriesList, string ConnStr)
    {
      string SQLstr = "SELECT * FROM sysjobs_view AS sv";
      SQLstr += " INNER JOIN syscategories AS sc ON sv.category_id=sc.category_id";
      if (CategoriesList != "")
      {
        string CategoriesINclause = "";
        string[] Categories = CategoriesList.Split('|');
        foreach (string Category in Categories)
        {
          if (Category.Trim() != "")
          {
            if (CategoriesINclause != "")
              CategoriesINclause += ",";
            CategoriesINclause += "'" + Category.Trim().Replace("'", "''") + "'";
          }
        }
        SQLstr += " WHERE sc.name IN (" + CategoriesINclause + ")";
      }
      SqlDataAdapter da = new SqlDataAdapter(SQLstr, ConnStr);
      DataTable dt = new DataTable();
      da.Fill(dt);

      if (dt.Rows.Count == 0)
        return null;

      SQLjob[] jobs = new SQLjob[dt.Rows.Count];
      for (int i = 0; i < dt.Rows.Count; i++)
      {
        DataRow dr = dt.Rows[i];
        jobs[i] = new SQLjob();
        jobs[i].job_id = DefNull(dr["job_id"], "");
        jobs[i].originating_server = DefNull(dr["originating_server"], "");
        jobs[i].name = DefNull(dr["name"], "");
        jobs[i].enabled = DefNull(dr["enabled"], (byte)0);
        jobs[i].description = DefNull(dr["description"], "");
        jobs[i].category_id = DefNull(dr["category_id"], 0);
        jobs[i].date_created = DefNull(dr["date_created"], DateTime.MinValue);
        jobs[i].date_modified = DefNull(dr["date_modified"], DateTime.MinValue);
        jobs[i].version_number = DefNull(dr["version_number"], 0);
      }
      return jobs;
    }


    // Retrieve the SQLschedules pertaining to a given job
    // JobID is the GUID of the given job
    public static SQLschedule[] LoadSQLschedules(string JobID, string ConnStr)
    {
      string SQLstr = "sp_help_jobschedule @job_id=N'" + JobID + "'";

      SqlDataAdapter da = new SqlDataAdapter(SQLstr, ConnStr);
      DataTable dt = new DataTable();
      da.Fill(dt);

      if (dt.Rows.Count == 0)
        return null;

      SQLschedule[] schs = new SQLschedule[dt.Rows.Count];
      for (int i = 0; i < dt.Rows.Count; i++)
      {
        DataRow dr = dt.Rows[i];
        schs[i] = new SQLschedule();
        schs[i].schedule_id = DefNull(dr["schedule_id"], 0);
        schs[i].name = DefNull(dr["schedule_name"], "");
        schs[i].enabled = DefNull(dr["enabled"], 0);
        schs[i].freq_type = DefNull(dr["freq_type"], 0);
        schs[i].freq_interval = DefNull(dr["freq_interval"], 0);
        schs[i].freq_subday_type = DefNull(dr["freq_subday_type"], 0);
        schs[i].freq_subday_interval = DefNull(dr["freq_subday_interval"], 0);
        schs[i].freq_relative_interval = DefNull(dr["freq_relative_interval"], 0);
        schs[i].freq_recurrence_factor = DefNull(dr["freq_recurrence_factor"], 0);
        schs[i].active_start_date = DefNullIntegerDate(dr["active_start_date"], DateTime.Today);
        schs[i].active_end_date = DefNullIntegerDate(dr["active_end_date"], new DateTime(9999, 12, 31));
        schs[i].active_start_time = DefNullIntegerTime(dr["active_start_time"], new DateTime(2000, 1, 1, 0, 0, 0));
        schs[i].active_end_time = DefNullIntegerTime(dr["active_end_time"], new DateTime(2000, 1, 1, 23, 59, 59));
        schs[i].date_created = DefNull(dr["date_created"], DateTime.Now);
        schs[i].schedule_uid = DefNull(dr["schedule_uid"], "");
      }
      return schs;
    }


    public static void CreateSQLschedule(string JobID, SQLschedule Schedule, string ConnStr)
    {
      string SQLstr = "DECLARE @schedule_id int";
      SQLstr += " EXEC sp_add_schedule";
      SQLstr += " @schedule_name=N'" + Schedule.name.Replace("'", "''") + "',";
      SQLstr += " @enabled=" + Schedule.enabled.ToString() + ",";
      SQLstr += " @freq_type=" + Schedule.freq_type.ToString() + ",";
      SQLstr += " @freq_interval=" + Schedule.freq_interval.ToString() + ",";
      SQLstr += " @freq_subday_type=" + Schedule.freq_subday_type.ToString() + ",";
      SQLstr += " @freq_subday_interval=" + Schedule.freq_subday_interval.ToString() + ",";
      SQLstr += " @freq_relative_interval=" + Schedule.freq_relative_interval.ToString() + ",";
      SQLstr += " @freq_recurrence_factor=" + Schedule.freq_recurrence_factor.ToString() + ",";
      SQLstr += " @active_start_date=" + Schedule.active_start_date.ToString("yyyyMMdd") + ",";
      SQLstr += " @active_end_date=" + Schedule.active_end_date.ToString("yyyyMMdd") + ",";
      SQLstr += " @active_start_time=" + Schedule.active_start_time.ToString("HHmmss") + ",";
      SQLstr += " @active_end_time=" + Schedule.active_end_time.ToString("HHmmss") + ",";
      SQLstr += " @schedule_id = @schedule_id OUTPUT";
      SQLstr += " SELECT @schedule_id";

      SqlConnection cnn = new SqlConnection(ConnStr);
      SqlCommand cmd = new SqlCommand(SQLstr, cnn);
      cnn.Open();

      int ScheduleID = (int)cmd.ExecuteScalar();

      SQLstr = "EXEC sp_attach_schedule @job_id=N'" + JobID + "', @schedule_id=" + ScheduleID.ToString();
      cmd.CommandText = SQLstr;
      cmd.ExecuteNonQuery();
      cnn.Close();
    }

    public static void UpdateSQLschedule(SQLschedule Schedule, string ConnStr)
    {
      string SQLstr = "DECLARE @schedule_id int";
      SQLstr += " EXEC sp_update_schedule";
      SQLstr += " @schedule_id=" + Schedule.schedule_id.ToString() + ",";
      SQLstr += " @new_name=N'" + Schedule.name.Replace("'", "''") + "',";
      SQLstr += " @enabled=" + Schedule.enabled.ToString() + ",";
      SQLstr += " @freq_type=" + Schedule.freq_type.ToString() + ",";
      SQLstr += " @freq_interval=" + Schedule.freq_interval.ToString() + ",";
      SQLstr += " @freq_subday_type=" + Schedule.freq_subday_type.ToString() + ",";
      SQLstr += " @freq_subday_interval=" + Schedule.freq_subday_interval.ToString() + ",";
      SQLstr += " @freq_relative_interval=" + Schedule.freq_relative_interval.ToString() + ",";
      SQLstr += " @freq_recurrence_factor=" + Schedule.freq_recurrence_factor.ToString() + ",";
      SQLstr += " @active_start_date=" + Schedule.active_start_date.ToString("yyyyMMdd") + ",";
      SQLstr += " @active_end_date=" + Schedule.active_end_date.ToString("yyyyMMdd") + ",";
      SQLstr += " @active_start_time=" + Schedule.active_start_time.ToString("HHmmss") + ",";
      SQLstr += " @active_end_time=" + Schedule.active_end_time.ToString("HHmmss");

      SqlConnection cnn = new SqlConnection(ConnStr);
      SqlCommand cmd = new SqlCommand(SQLstr, cnn);
      cnn.Open();
      cmd.ExecuteNonQuery();
      cnn.Close();
    }

    public static void DeleteSQLschedule(string JobID, int ScheduleID, string ConnStr)
    {
      string SQLstr = "EXEC sp_detach_schedule @job_id=N'" + JobID + "', @schedule_id=" + ScheduleID.ToString() + ", @delete_unused_schedule=1";
      SqlConnection cnn = new SqlConnection(ConnStr);
      SqlCommand cmd = new SqlCommand(SQLstr, cnn);
      cnn.Open();
      cmd.ExecuteNonQuery();
      cnn.Close();
    }



    public static DateTime DefNullIntegerDate(object Value, DateTime DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
      {
        int v = (int)Value;
        int yyyy = v / 10000;
        int mm = (v % 10000) / 100;
        int dd = v % 100;
        return new DateTime(yyyy, mm, dd);
      }
    }

    public static DateTime DefNullIntegerTime(object Value, DateTime DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
      {
        int v = (int)Value;
        int hh = v / 10000;
        int mm = (v % 10000) / 100;
        int ss = v % 100;
        return new DateTime(2000, 1, 1, hh, mm, ss);
      }
    }



    public static object DefNull(object Value, object DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return Value;
    }

    public static string DefNull(object Value, string DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return Value.ToString();
    }

    public static bool DefNull(object Value, bool DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return (bool)Value;
    }

    public static int DefNull(object Value, int DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return (int)Value;
    }

    public static byte DefNull(object Value, byte DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return (byte)Value;
    }

    public static DateTime DefNull(object Value, DateTime DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return (DateTime)Value;
    }

    public static double DefNull(object Value, double DefaultValue)
    {
      if (Value == null)
        return DefaultValue;
      else
        return (double)Value;
    }

  }
}
