using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PivotalERP.ScheduleJobs
{
    public class JobScheduler
    {
        public static Quartz.IScheduler scheduler = null;
        public static async void Start()
        {
            var schedulerFactory = new StdSchedulerFactory();
            scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            try
            {
                

                //new Models.GlobalFunction().SetDBName();
                //HRM.BusinessEntity.Employee.ReminderCollections scheduleColl = new HRM.DataAccess.Employee.ReminderDB().getAllReminderForSchedule(1);

                //if (scheduleColl != null)
                //{
                //    foreach (var beData in scheduleColl)
                //    {
                //        IJobDetail job = JobBuilder.Create<ReminderJob>()
                //                       .WithIdentity(beData.TranId.ToString(), Convert.ToInt32(HRM.BusinessEntity.Global.FormsEntity.Reminder).ToString())
                //                       .StoreDurably()
                //                        //.UsingJobData("jobSays", "Hello World!")
                //                        //.UsingJobData("myFloatValue", 3.141f)
                //                       .Build();

                //        DateTime startDate = new DateTime(beData.StartDate.Value.Year, beData.StartDate.Value.Month, beData.StartDate.Value.Day, 0, 1, 0);
                //        DateTimeOffset startDateUTC = new DateTimeOffset(startDate);

                //        DateTime stopDate = new DateTime(beData.EndDate.Value.Year, beData.EndDate.Value.Month, beData.EndDate.Value.Day, 23, 59, 0);
                //        DateTimeOffset endDateUTC = new DateTimeOffset(stopDate);

                //        ITrigger trigger = TriggerBuilder.Create()
                //            .WithIdentity("Trigger" + beData.TranId.ToString(), HRM.BusinessEntity.Global.FormsEntity.Reminder.ToString())
                //            .StartAt(startDateUTC)
                //            .WithCronSchedule(beData.Cron)
                //            .EndAt(stopDate)
                //            .Build();

                //        await scheduler.ScheduleJob(job, trigger);

                //    }
                //}


            }
            catch { }
        }

    }

    public class ReminderJob : IJob
    {
        async Task IJob.Execute(IJobExecutionContext context)
        {

            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                int TranId = Convert.ToInt32(context.JobDetail.Key.Name);
                //HRM.BusinessEntity.Notification.ReminderCollections dataColl = new HRM.DataAccess.Notification.NotificationDB().getReminderDetails(TranId);

                //if (dataColl != null && dataColl.Count > 0)
                //{
                //    // Send Auto Email
                //    #region "Send Email And Notification"

                //    try
                //    {
                //        string emailId = "";
                //        string empCode = "";
                //        foreach (var dc in dataColl)
                //        {
                //            if (!string.IsNullOrEmpty(emailId))
                //                emailId = emailId + ",";

                //            if (!string.IsNullOrEmpty(empCode))
                //                empCode = empCode + ",";

                //            if (!string.IsNullOrEmpty(dc.EmailId))
                //                emailId = emailId + dc.EmailId;

                //            empCode = empCode + dc.EmployeeCode;
                //        }


                //        #region "Send Email"

                //        HRM.BusinessEntity.Notification.Reminder reminder = dataColl[0];

                //        HRM.BusinessEntity.Global.Email email = new HRM.BusinessEntity.Global.Email();
                //        email.Message = reminder.Content;
                //        email.Subject = reminder.Title;
                //        email.EmployeeId = null;
                //        email.To = emailId;
                //        email.CC = "";

                //        try
                //        {
                //            new Models.GlobalFunction().SendEmail(email);
                //        }
                //        catch { }

                //        #endregion                       

                //    }
                //    catch { }

                //    #endregion

                //    // End Sending Email
                //}

            }
            catch { }
        }
    }
}