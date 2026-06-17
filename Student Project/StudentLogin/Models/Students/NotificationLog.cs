using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{
    public class NotificationLog
    {


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

    public class NotificationVal
    {
        //public DateTime dateFrom { get; set; }
        //public DateTime dateTo { get; set; }
        public int TranId { get; set; }
        public bool isGeneral { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
   
    }
}