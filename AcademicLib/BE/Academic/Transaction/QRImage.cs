using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class QRImage:ResponeValues
    {
        public int? QrImageId { get; set; }
        public string OnlineQrPath { get; set; } = "";
        public string PlaystoreQrPath { get; set; } = "";
        public string AppStoreQrPath { get; set; } = "";
    }

    public class QRCollections : List<QRImage>
    {
        public QRCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
