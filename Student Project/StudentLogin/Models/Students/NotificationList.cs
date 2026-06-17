using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{
    public class NotificationList
    {
        public int NoticeId { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
        public DateTime NoticeDate { get; set; }
        public DateTime PublishOn { get; set; }
        public DateTime PublishTime { get; set; }
        public int OrderNo { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public string NoticeDate_BS { get; set; }
        public string PublishOn_BS { get; set; }
        public object AttachmentColl { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int EntityId { get; set; }
        public int ErrorNumber { get; set; }
    }
}