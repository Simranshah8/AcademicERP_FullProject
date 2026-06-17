using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{ 
  public class CommentSetup :  ResponeValues
{
    public int? TranId { get; set; }   
    public int Wise { get; set; }
    public int ForStudent { get; set; }  
    public double MinVal { get; set; }
    public double MaxVal { get; set; }
    
    public string Comment { get; set; }
        public string Grade { get; set; }
   
}
public class CommentSetupCollections : List<CommentSetup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
