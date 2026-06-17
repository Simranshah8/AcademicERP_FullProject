using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class SeatDetails
    {
        public int ExamShiftId { get; set; }
        public int ExamTypeId { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public int Banch_Row_SNo { get; set; }
        public int Seat_SNo { get; set; }
        public int Seat_Col { get; set; }
        public int BanchNo { get; set; }
    }
    public class SeatDetailsCollections : System.Collections.Generic.List<SeatDetails>
    {

    }

    public class ExamRoom : ResponeValues
    {
        public ExamRoom()
        {
            DetailColl = new List<ExamRoomDetails>();
        }
        public int? RoomId { get; set; }

        public int ExamShiftId { get; set; }
        public int ExamTypeId { get; set; }
        public string Name { get; set; }
        public int RoomNo { get; set; }
        public int TotalBanch { get; set; }
        public int NoOfBanchRow { get; set; }
        public int AvailableSeats { get; set; }
        public List<ExamRoomDetails> DetailColl { get; set; }

        public string ExamTypeName { get; set; }
        public string ExamShiftName { get; set; }
    }
    public class ExamRoomCollections : System.Collections.Generic.List<ExamRoom>
    {
        public ExamRoomCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamRoomDetails
    {
        public int Banch_Row_SNo { get; set; }
        public string Banch_Row_Name { get; set; }
        public int NoOfBanch { get; set; }
        public int NoOfSeatsInRow { get; set; }
        public string Col_1 { get; set; }
        public string Col_2 { get; set; }
        public string Col_3 { get; set; }
        public string Col_4 { get; set; }
        public string Col_5 { get; set; }
    }
    public class ExamSeatPlan
    {
    }

    public class GenerateSeatPlan : ResponeValues
    {
        public int ExamTypeId { get; set; }
        public int ExamShiftId { get; set; }
        public string ClassIdColl { get; set; }
        public string RoomIdColl { get; set; }

        // --1=Class and RollNo wise, 2= Class and SymbolNo wise, 3= SymbolNo wise,4=Random,5=ClassWise Random
        public int SeatPlanAs { get; set; }
    }

    public class RoomSeatDetails
    {
        public RoomSeatDetails()
        {
            StudentDet = "";
        }
        public int RId { get; set; }
        public int RoomId { get; set; }
        public int Banch_Row_SNo { get; set; }
        public int SNo { get; set; }
        public int Seat_Col { get; set; }
        public int BanchNo { get; set; }
        public int TotalBanch { get; set; }
        public int NoOfBanchRow { get; set; }
        public string StudentDet { get; set; }
    }
    public class RoomSeatDetailsCollections : System.Collections.Generic.List<RoomSeatDetails>
    {
        public RoomSeatDetailsCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentForSeatPlan
    {
        public StudentForSeatPlan()
        {
            RegNo = "";
            Name = "";
            ClassName = "";
            SectionName = "";
            SymbolNo = "";
        }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SymbolNo { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
    }
    public class StudentForSeatPlanCollections : System.Collections.Generic.List<StudentForSeatPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
