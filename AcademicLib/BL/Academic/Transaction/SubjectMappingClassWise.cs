using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class SubjectMappingClassWise
    {
        DA.Academic.Transaction.SubjectMappingClassWiseDB db = null;
        int _UserId = 0;
        public SubjectMappingClassWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.SubjectMappingClassWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Academic.Transaction.SubjectMappingClassWiseCollections beData)
        {          
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData);
            else
                return isValid;
        }

        public ResponeValues copySubjectMappingClassWise(AcademicLib.BE.Academic.Transaction.CopySubjectMapping beData)
        {
            return db.copySubjectMappingClassWise(beData);
        }
            public BE.Academic.Transaction.SubjectMappingClassWiseCollections getClassWiseSubjectMapping(int AcademicYearId, int ClassId, string SectionIdColl,int? SemesterId,int? ClassYearId,int? BatchId,int? BranchId=null)
        {
            var dataColl= db.getClassWiseSubjectMapping(_UserId,AcademicYearId, ClassId, SectionIdColl,SemesterId,ClassYearId,BatchId,BranchId);

            BE.Academic.Transaction.SubjectMappingClassWiseCollections mappingColl = new BE.Academic.Transaction.SubjectMappingClassWiseCollections();
            mappingColl.IsSuccess = dataColl.IsSuccess;
            mappingColl.ResponseMSG = dataColl.ResponseMSG;

            var query = from dc in dataColl
                        group dc by new { dc.ClassId, dc.SubjectId,dc.SemesterId,dc.ClassYearId,dc.BatchId } into g
                        select new BE.Academic.Transaction.SubjectMappingClassWise
                        {
                             ClassId=g.Key.ClassId,
                             SemesterId=g.Key.SemesterId,
                             ClassYearId=g.Key.ClassYearId,
                             BatchId=g.Key.BatchId,
                             SectionIdColl=dataColl.First().SectionIdColl,
                             SubjectName=g.First().SubjectName,
                              CodeTH=g.First().CodeTH,
                              CodePR=g.First().CodePR,
                              CRTH=g.First().CRTH,
                               CRPR=g.First().CRPR,
                                CR=g.First().CR,
                               IsExtra=g.First().IsExtra,
                               IsOptional=g.First().IsOptional,
                                ResponseMSG=dataColl.ResponseMSG,
                                IsSuccess=dataColl.IsSuccess,
                                 PaperType=g.First().PaperType,
                                  SubjectId=g.Key.SubjectId,
                                   SNo=g.First().SNo,
                                     NoOfOptionalSub=g.First().NoOfOptionalSub
                        };

            foreach (var q in query.OrderBy(p1=>p1.SNo))
                mappingColl.Add(q);

            return mappingColl;

        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.SubjectMappingClassWiseCollections beData)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }               
                else
                {

                   foreach(var data in beData)
                    {
                        
                        data.CUserId = _UserId;
                        if (data.SectionIdColl == null || data.SectionIdColl.Length == 0)
                            data.SectionIdColl = new int[1];

                        data.CR = data.CRTH + data.CRPR;
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

        public ResponeValues ImportSubjectMapping( List<BE.Academic.Transaction.ImportSubjectMappingClassWise> dataColl)
        {
            return db.ImportSubjectMapping(_UserId, dataColl);   
        }
        }
}
