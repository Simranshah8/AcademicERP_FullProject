using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ExamBulkRoom : ResponeValues
    {
        public int? ExamRoomId { get; set; }
        public string RoomName { get; set; }
        public int TotalCapacity { get; set; }
        public int TotalBench { get; set; }
        public int NoOfBanchRow { get; set; }
        public ExamBulkRoom()
        {
            DetailColl = new ExamBulkRoomDetailCollections();
        }
        public ExamBulkRoomDetailCollections DetailColl { get; set; }

    }

    public class ExamBulkRoomDetail
    {

        public int ExamRoomId { get; set; }
        public string Banch_Row_Name { get; set; } = "";
        public int NoOfBanch { get; set; }
        public int NoOfSeatsInRow { get; set; }
     
    }

    public class ExamBulkRoomDetailCollections : System.Collections.Generic.List<ExamBulkRoomDetail>
    {

        public string ResponseMSG { get; set; } = "";

        public bool IsSuccess { get; set; }

    }
    public class ExamBulkRoomCollections : System.Collections.Generic.List<ExamBulkRoom>
    {
        public ExamBulkRoomCollections()
        {
            ResponseMSG = "";
        }
        //public int? ExamRoomId { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}