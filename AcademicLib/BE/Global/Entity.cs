using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public enum NOTIFICATION_ENTITY
    {
        LOGIN=1,
        LOGOUT=2,
        USER_DETAILS=3,
        NOTICE=4,
        REMARKS=5,
        DAILY_ATTENDANCE=6,
        SUBJECTWISE_ATTENDANCE=7,
        ONLINE_CLASS_ATTENDANCE=8,
        COMPLAINT=9,
        EXAM_SCHEDULE=10,
        EXAM_RESULT=11,
        EXAM_ATTENDANCE=12,
        HOMEWORK=13,
        ASSIGNMENT=14,
        EVENTS=15,
        HOLIDAYS=16,
        RESET_PASSWORD=17,
        ONLINE_CLASS_START=18,
        ONLINE_CLASS_END=19,
        HOMEWORK_CHECK = 20,
        Assignment_CHECK=21,
        SEND_SMS=22,
        BANNER=23,
        BIO_DAILY_ATTENDANCE = 24,
        TRANSPORT_START_FOR_PICKUP=25,
        TRANSPORT_PICKUP=26,
        TRANSPORT_DROP=27,
        TRANSPORT_START_FOR_DROP = 28,
        FEE_RECEIPT=29,
        BILL_GENERATE=30,
        STUDENT_REMARKS=31,
        EXAM_SEAT_PLAN=32,
        VISITOR=33,
        BIRTHDAY=34,
        LEAVE_REQUEST=35,
        LEAVE_APPROVED=36,
        ENABLE_NOTIFICATION=37,
        DISABLE_NOTIFICATION=38,
        EMPLOYEE_REMARKS = 39,
    }

    public enum ONLINE_PLATFORMS
    { 
        ZOOM=1,
        GOOGLE_MEET=2,
        MICROSOFT_TEAM=3
    }

    public enum IMPORTS_ENTITY
    {
        CLASS=1,
        SECTION=2,
        DEPARTMENT=3,
        CATEGORY=4,
        STUDENT=5,
        EMPLOYEE=6,
        MARKENTRY=7,
        UPDATE_STUDENT=8,
        STUDENT_OPENING=9,
        BOOK_DETAILS=10,
        IMPORT_STUDENT_DOB=11,
        IMPORT_EMPLOYEE_DOB=12,
        IMPORT_STUDENT_ADMIT_DATE=13,
        EXAM_WISE_SYMBOLNO=14,
        UPDATE_EMPLOYEE=15,
        LEDGER=16,
        PRODUCT=17,
        IMPORT_STUDENT_PHOTO=18,
        ALLSUBJECTMARKENTRY=19,
        LEDGER_OPENING=20,
        RECEIPT=21,
        PAYMENT=22,
        STOCK_JOURNAL=23,
        COSTCENTER=24,
        PURCHASE_INVOICE=25,
        PURCHASE_RETURN=26,
        SALES_INVOICE=27,
        SALES_RETURN=28,
        UPDATE_PRODUCT=29,
        UPDATE_LEDGER=30,
        IMPORT_EMPLOYEE_BANKACCOUNT=31,
        JOURNAL=32,
        RECEIPTNOTE=33,
        DELIVERYNOTE=34,
        Admission_Enquiry=35,
        SALARY_DETAILS=36,
        COSTCENTER_OPENING=37,
        BillGenerate=38,
        FeeDebit=39,
        FeeCredit=40,
        FeeReceipt=41,
        SubjectMappingClassWise=42,
        MarkSetup=43,   
        ExamAttendance=44,
        Subject=45,

    }
}
