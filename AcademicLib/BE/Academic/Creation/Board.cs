using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Board : Common
    {
        public int? BoardId { get; set; }
        public int id
        {
            get
            {
                if (BoardId.HasValue)
                    return BoardId.Value;
                return 0;
            }
        }

    }

    public class BoardCollections : System.Collections.Generic.List<Board>
    {
        public BoardCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
