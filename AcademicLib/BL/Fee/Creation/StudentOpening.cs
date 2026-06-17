using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class StudentOpening
    {
        DA.Fee.Creation.StudentOpeningDB db = null;
        int _UserId = 0;
        public StudentOpening(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.StudentOpeningDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId,BE.Fee.Creation.StudentOpeningCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, beData);
            else
                return isValid;
        }
        public ResponeValues SaveFeeItemWise(int AcademicYearId, BE.Fee.Creation.StudentOpeningCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            foreach(var dc in dataColl)
            {
                if (dc.StudentId == 0 && dc.Amount!=0)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Student";
                    return resVal;
                }

                if(dc.Amount!=0 && !dc.VoucherDate.HasValue)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Opening Date";
                    return resVal;
                }

                if(dc.Amount!=0 && dc.FeeItemId == 0)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Fee Item";
                    return resVal;
                }

            }
            resVal= db.SaveFeeItemWise(_UserId,AcademicYearId, dataColl);

            return resVal;
        }
         public BE.Fee.Creation.StudentOpeningCollections GetAllFeeMapping(int EntityId, int AcademicYearId, int ClassId, int? SectionId,int? FeeItemId, int? SemesterId, int? ClassYearId, int? BatchId)
        {
            return db.getStudentOpening(_UserId,AcademicYearId, EntityId, ClassId, SectionId,FeeItemId,SemesterId,ClassYearId,BatchId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            return db.Delete(_UserId,AcademicYearId, EntityId, ClassId, SectionId,SemesterId,ClassYearId);
        }
        public RE.Fee.ClassWiseOpeningCollections getClassWiseOpening(int AcademicYearId)
        {
            return db.getClassWiseOpening(_UserId,AcademicYearId);
        }
        public ResponeValues ClearStudentOpening(int AcademicYearId)
        {
            return db.ClearStudentOpening(_UserId, AcademicYearId);
        }
            public RE.Fee.StudentOpeningCollections getClassWiseOpening(int AcademicYearId, int ClassId, int? SectionId, int? SemesterId, int? ClassYearId,int? BatchId=null)
        {
            return db.getClassWiseOpening(_UserId,AcademicYearId, ClassId, SectionId,SemesterId,ClassYearId,BatchId);
        }
            public ResponeValues IsValidData(ref BE.Fee.Creation.StudentOpeningCollections dataColl)
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
                    var query = from det in dataColl
                                group det by new { det.StudentId, det.SemesterId, det.ClassYearId } into g
                                select new
                                {
                                    StudentId=g.Key.StudentId,
                                    SemesterId=g.Key.SemesterId,
                                    ClassYearId=g.Key.ClassYearId,
                                    DataColl=g,
                                };

                    foreach(var q in query)
                    {                        
                        List<int> idColl = new List<int>();
                        foreach(var d in q.DataColl)
                        {
                            if (string.IsNullOrEmpty(d.StudentName))
                                d.StudentName = "";

                            if (string.IsNullOrEmpty(d.FeeItemName))
                                d.FeeItemName = "";

                            if (string.IsNullOrEmpty(d.RegNo))
                                d.RegNo = "";

                            if (idColl.Contains(d.FeeItemId))
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Duplicate Fee Item Of Student " + d.StudentName + "(" + d.RegNo + ") :-" + d.FeeItemName;
                                return resVal;
                            }
                            else
                                idColl.Add(d.FeeItemId);
                        }
                    }
                    
                    foreach(var beData in dataColl)
                    {                        
                        if(beData.Amount!=0 && beData.FeeItemId == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Fee Heading Selection was missing";
                            return resVal;
                        }
                        else if(beData.Amount != 0 && !beData.VoucherDate.HasValue)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter Opening Effect Date";
                            return resVal;
                        }
                        else if (beData.Amount != 0 && beData.StudentId==0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Invalid Student " + beData.StudentName + "(" + beData.RegNo + ") :-" + beData.FeeItemName;
                            return resVal;
                        }
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
