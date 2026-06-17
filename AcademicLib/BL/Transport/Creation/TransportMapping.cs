using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Transport.Creation
{
    public class TransportMapping
    {
        DA.Transport.Creation.TransportMappingDB db = null;
        int _UserId = 0;
        public TransportMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Transport.Creation.TransportMappingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId,BE.Transport.Creation.TransportMappingCollections beData)
        {
            
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
            {
                if(AcademicYearId == 0)
                {
                    isValid.IsSuccess = false;
                    isValid.ResponseMSG = "Please ! Select Valid Academic Year Name";
                    return isValid;
                }
                else
                {
                    return db.SaveUpdate(_UserId, AcademicYearId, beData);
                }
            }
                
            else
                return isValid;
        }
        public BE.Transport.Creation.TransportMappingCollections GetAllFeeMapping(int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            return db.getTransportMapping(_UserId,AcademicYearId, EntityId, ClassId, SectionId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int ClassId, int? SectionId)
        {
            return db.Delete(_UserId,AcademicYearId, EntityId, ClassId, SectionId);
        }

        public ResponeValues DeleteForMonth(int AcaddemicYearId, int ForMonthId)
        {
            return db.DeleteForMonth(_UserId, AcaddemicYearId, ForMonthId);
        }
        public ResponeValues SaveForMonth(int AcaddemicYearId, int ForMonthId)
        {
            return db.SaveUpdateForMonth(_UserId, AcaddemicYearId, ForMonthId);
        }
        public AcademicLib.RE.Transport.StudentSummaryCollections getStudentSummaryList(int AcademicYearId,string ClassIdColl, string SectionIdColl, string RouteIdColl, string PointIdColl, string BatchIdColl, string SemesterIdColl, string ClassYearIdColl) 
        {
            return db.getStudentSummaryList(_UserId,AcademicYearId, ClassIdColl,SectionIdColl,RouteIdColl,PointIdColl, BatchIdColl, SemesterIdColl, ClassYearIdColl);
        }
        public AcademicLib.RE.Transport.StudentSummaryCollections getStudentSummaryForMonth(int AcademicYearId, int ForMonthId)
        {
            return db.getStudentSummaryForMonth(_UserId,AcademicYearId, ForMonthId);
        }
            public ResponeValues IsValidData(ref BE.Transport.Creation.TransportMappingCollections dataColl)
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
