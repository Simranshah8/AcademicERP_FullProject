using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class NotificationCount
    {
        public int Total { get; set; }
        public int Read { get; set; }
        public int UnRead { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class NotificationLog
    {
        public NotificationLog()
        {
            Heading = "";
            Subject = "";
            Content = "";
            ContentPath = "";
            EntityName = "";
            LogDate_BS = "";
            AtTime = "";
            SendBy = "";
            SendByPhotoPath = "";
            SendUserBy = "";
        }
        public int TranId { get; set; }
        public bool IsRead { get; set; }
        public string Heading { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ContentPath { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public DateTime LogDate_AD { get; set; }
        public string LogDate_BS { get; set; }
        public string AtTime { get; set; }
        public string SendUserBy { get; set; }
        public string SendByPhotoPath { get; set; }
        public string SendBy { get; set; }

        public string NotificationType { get; set; }
        public string SendReceived { get; set; }
        public string ClassSection { get; set; }
    }
}