using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class BedMapping
    {
        DA.Hostel.BedMappingDB db = null;
        int _UserId = 0;
        public BedMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.BedMappingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcaddemicYearId, BE.Hostel.BedMappingCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcaddemicYearId, beData);
            else
                return isValid;
        }
        public BE.Hostel.BedMappingCollections GetAllFeeMapping(int EntityId, int AcaddemicYearId, int ClassId, int? SectionId)
        {
            return db.getBedMapping(_UserId,AcaddemicYearId, EntityId, ClassId, SectionId);
        }
        public ResponeValues Delete(int EntityId, int AcaddemicYearId, int ClassId, int? SectionId)
        {
            return db.Delete(_UserId,AcaddemicYearId, EntityId, ClassId, SectionId);
        }

        public ResponeValues DeleteForMonth( int AcaddemicYearId, int ForMonthId)
        {
            return db.DeleteForMonth(_UserId, AcaddemicYearId, ForMonthId);
        }
        public ResponeValues SaveForMonth(int AcaddemicYearId, int ForMonthId)
        {
            return db.SaveUpdateForMonth(_UserId, AcaddemicYearId, ForMonthId);
        }
        public AcademicLib.RE.Hostel.StudentSummaryCollections getStudentSummaryForMonth( int AcademicYearId, int ForMonthId)
        {
            return db.getStudentSummaryForMonth(_UserId, AcademicYearId, ForMonthId);
        }
        public AcademicLib.RE.Hostel.StudentSummaryCollections getStudentSummaryList( int AcademicYearId, string ClassIdColl, string SectionIdColl,string RoomIdColl, string BatchIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            return db.getStudentSummaryList(_UserId, AcademicYearId, ClassIdColl, SectionIdColl, RoomIdColl, BatchIdColl, SemesterIdColl, ClassYearIdColl);
        }
            public AcademicLib.API.Admin.HostelStudentCollections admin_StudentList(int? ClassId = null, int? SectionId = null,int? AcademicYearId=null)
        {
            return db.admin_StudentList(_UserId, ClassId, SectionId,AcademicYearId);
        }
            public ResponeValues IsValidData(ref BE.Hostel.BedMappingCollections dataColl)
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
                    foreach(var v in dataColl)
                    {
                        if (v.RoomId.HasValue && v.RoomId.Value == 0)
                            v.RoomId = null;

                        if (v.BedId.HasValue && v.BedId.Value == 0)
                            v.BedId = null;
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
