using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class SubjectMappingStudentWise
    {
        DA.Academic.Transaction.SubjectMappingStudentWiseDB db = null;
        int _UserId = 0;
        public SubjectMappingStudentWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.SubjectMappingStudentWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, List<BE.Academic.Transaction.OptionalSubjectStudentWise> dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, dataColl);
            else
                return isValid;
        }
        public BE.Academic.Transaction.SubjectMappingStudentWise getStudentWiseSubjectMapping(int AcademicYearId, int ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,int? BranchId=null)
        {
            return db.getStudentWiseSubjectMapping(_UserId,AcademicYearId, ClassId, SectionId,SemesterId,ClassYearId,BatchId,BranchId);
        }
        public ResponeValues IsValidData(ref List<BE.Academic.Transaction.OptionalSubjectStudentWise> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (dataColl == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else
                {
                    var query = from dc in dataColl
                                group dc by dc.StudentId into g
                                select new
                                {
                                    SId=g.Key,
                                    NoOfSubject=(g.First().TranIdColl!=null ? g.First().TranIdColl.Count : 0),
                                    NoOfOptSubject=g.First().NoOfOptionalSubject,
                                    MatchOptSubject=g.First().MatchOptSubject
                                };

                    foreach(var q in query)
                    {
                        if (q.MatchOptSubject)
                        {
                            if(q.NoOfSubject != q.NoOfOptSubject)
                            {
                                resVal.ResponseMSG = "Subject Mapping Student Wise Does Not Matched";
                                return resVal;
                            }
                        }
                    }

                    foreach (var data in dataColl)
                    {
                        if (data.TranIdColl == null)
                            data.TranIdColl = new List<int>();
                        //data.CUserId = _UserId;
                        //if (data.SectionIdColl == null || data.SectionIdColl.Length == 0)
                        //    data.SectionIdColl = new int[1];
                    }

                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }
    }
}
