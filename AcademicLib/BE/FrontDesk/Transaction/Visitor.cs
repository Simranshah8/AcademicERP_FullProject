using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
   public class Visitor : ResponeValues
    {
        public int? VisitorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public MEETTOS MeeTo { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public string OthersName { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
        public /*TimeSpan*/ DateTime? InTime { get; set; }
        public /*TimeSpan*/ DateTime? ValidityTime { get; set; }
        public /*TimeSpan*/ DateTime? OutTime { get; set; }

        public string Photo { get; set; }
        public string PhotoPath { get; set; }
        public string Remarks { get; set; }

        private Dynamic.BusinessEntity.GeneralDocumentCollections _AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl
        {
            get
            {
                return _AttachmentColl;
            }
            set
            {
                _AttachmentColl = value;
            }
        }

        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogMiti { get; set; }


    }
    public class VisitorCollections : List<Visitor>
    {
        public VisitorCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum MEETTOS
    {
        STUDENT=1,
        EMPLOYEE=2,
        OTHER=3
    }

    //public class VisitorDocumentAttach
    //{
    //    public int TranId { get; set; }
    //    public int VisitorId { get; set; }
    //    public int DocumentTypeId { get; set; }
    //    public string AttachDoc { get; set; }
    //    public string AttachDocPath { get; set; }
    //    public string Description { get; set; }
    //    public string Nam { get; set; }
    //}
    //public class VisitorDocumentAttachCollections : List<VisitorDocumentAttach> { }
}

