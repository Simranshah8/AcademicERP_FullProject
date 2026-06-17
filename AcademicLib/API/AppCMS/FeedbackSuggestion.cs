using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.AppCMS
{
    public class FeedbackSuggestion
    {
        //new@2021#FeeDBacK@2021
        public string PassKey { get; set; }
        public int? TranId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        public string IPAddress { get; set; }
        public double Lat { get; set; }
        public double Lan { get; set; }
        public string NearestLocation { get; set; }
        public string Response { get; set; }
        public string ResponseBY { get; set; }
        public string ResponseDateTime { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public DateTime PostDateTime { get; set; }

        public string branchCode { get; set; }
        public int UserId { get; set; }
    }
    public class FeedbackSuggestionCollections : System.Collections.Generic.List<FeedbackSuggestion>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
