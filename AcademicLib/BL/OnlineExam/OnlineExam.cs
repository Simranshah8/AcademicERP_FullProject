using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.OnlineExam
{
    public class OnlineExam
    {
        DA.OnlineExam.OnlineExamDB db = null;
        int _UserId = 0;

        public OnlineExam(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.OnlineExam.OnlineExamDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(API.OnlineExam.StartOnlineExam beData)
        {
            return db.SaveUpdate(beData);
        }
        public ResponeValues EndExam(API.OnlineExam.StartOnlineExam beData)
        {
            return db.EndExam(beData);
        }
        public ResponeValues SubmitAnswer(API.OnlineExam.StudentAnswer beData)
        {
            return db.SubmitAnswer(beData);
        }
     }
}
