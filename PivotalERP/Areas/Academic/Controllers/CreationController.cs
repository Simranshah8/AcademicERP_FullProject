using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Academic.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Academic/Creation

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ClassSetup, false)]
        public ActionResult ClassSetup()
        {
            
            return View();
        }

        #region "Class"
      
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveClass()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Class>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ClassId.HasValue)
                        beData.ClassId = 0;

                    beData.AcademicYearId = this.AcademicYearId;
                    resVal = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult UpdateClassForOR()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.ClassCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).UpdateClassForOR(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        
        [HttpPost]
        [AllowAnonymous]
        public JsonNetResult GetAllClassList()
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.Class(1, hostName, dbName).GetAllClass(0,true);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).GetAllClass(0);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonNetResult GetAllClassListForOR()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Class(1, hostName, dbName).GetAllClass(0, true);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult GetClassSectionList(bool? ForRunning=null)
        {
            if (!ForRunning.HasValue)
                ForRunning = false;

            var dataColl = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).getClassSectionList(this.AcademicYearId, ForRunning.Value,"");

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassById(int ClassId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).GetClassById(0, ClassId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelClass(int ClassId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Section"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveSection()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Section>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.SectionId.HasValue)
                        beData.SectionId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Section(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllSectionList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Section(User.UserId, User.HostName, User.DBName).GetAllSection(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllSectionForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Section(User.UserId, User.HostName, User.DBName).getAllSectionForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSectionById(int SectionId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Section(User.UserId, User.HostName, User.DBName).GetSectionById(0, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelSection(int SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Section(User.UserId, User.HostName, User.DBName).DeleteById(0, SectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ClassYear"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveClassYear()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.ClassYear>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ClassYearId.HasValue)
                        beData.ClassYearId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.ClassYear(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassYearList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassYear(User.UserId, User.HostName, User.DBName).GetAllClassYear(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassYearForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassYear(User.UserId, User.HostName, User.DBName).getAllClassYearForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassYearById(int ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassYear(User.UserId, User.HostName, User.DBName).GetClassYearById(0, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelClassYear(int ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.ClassYear(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ClassLevel"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveClassLevel()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.ClassLevel>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.LevelId.HasValue)
                        beData.LevelId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.ClassLevel(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassLevelList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassLevel(User.UserId, User.HostName, User.DBName).GetAllClassLevel(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassLevelForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassLevel(User.UserId, User.HostName, User.DBName).getAllClassLevelForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassLevelById(int LevelId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassLevel(User.UserId, User.HostName, User.DBName).GetClassLevelById(0, LevelId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelClassLevel(int LevelId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.ClassLevel(User.UserId, User.HostName, User.DBName).DeleteById(0, LevelId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Faculty"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveFaculty()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Faculty>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.FacultyId.HasValue)
                        beData.FacultyId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Faculty(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllFacultyList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Faculty(User.UserId, User.HostName, User.DBName).GetAllFaculty(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllFacultyForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Faculty(User.UserId, User.HostName, User.DBName).getAllFacultyForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFacultyById(int FacultyId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Faculty(User.UserId, User.HostName, User.DBName).GetFacultyById(0, FacultyId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelFaculty(int FacultyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Faculty(User.UserId, User.HostName, User.DBName).DeleteById(0, FacultyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Semester"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveSemester()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Semester>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.SemesterId.HasValue)
                        beData.SemesterId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Semester(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllSemesterList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Semester(User.UserId, User.HostName, User.DBName).GetAllSemester(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllSemesterForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Semester(User.UserId, User.HostName, User.DBName).getAllSemesterForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSemesterById(int SemesterId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Semester(User.UserId, User.HostName, User.DBName).GetSemesterById(0, SemesterId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelSemester(int SemesterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Semester(User.UserId, User.HostName, User.DBName).DeleteById(0, SemesterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Batch"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveBatch()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Batch>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.BatchId.HasValue)
                        beData.BatchId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Batch(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllBatchList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Batch(User.UserId, User.HostName, User.DBName).GetAllBatch(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllBatchForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Batch(User.UserId, User.HostName, User.DBName).getAllBatchForTran(0, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBatchById(int BatchId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Batch(User.UserId, User.HostName, User.DBName).GetBatchById(0, BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelBatch(int BatchId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Batch(User.UserId, User.HostName, User.DBName).DeleteById(0, BatchId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "AcademicYear"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveAcademicYear()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.AcademicYear>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AcademicYearId.HasValue)
                        beData.AcademicYearId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.AcademicYear(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllAcademicYearList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.AcademicYear(User.UserId, User.HostName, User.DBName).GetAllAcademicYear(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAcademicYearById(int AcademicYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.AcademicYear(User.UserId, User.HostName, User.DBName).GetAcademicYearById(0, AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ChangeAcademicYearId(int AcademicYearId)
        {
            var dataColl = ChangeAcademicYear(AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelAcademicYear(int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.AcademicYear(User.UserId, User.HostName, User.DBName).DeleteById(0, AcademicYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Board"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveBoard()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Board>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.BoardId.HasValue)
                        beData.BoardId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Board(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllBoardList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Board(User.UserId, User.HostName, User.DBName).GetAllBoard(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBoardById(int BoardId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Board(User.UserId, User.HostName, User.DBName).GetBoardById(0, BoardId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelBoard(int BoardId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Board(User.UserId, User.HostName, User.DBName).DeleteById(0, BoardId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "HouseName"

        [HttpPost]
        public JsonNetResult SaveHouseName()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.HouseName>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HouseNameId.HasValue)
                        beData.HouseNameId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.HouseName(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllHouseNameList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.HouseName(User.UserId, User.HostName, User.DBName).GetAllHouseName(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetHouseNameById(int HouseNameId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.HouseName(User.UserId, User.HostName, User.DBName).GetHouseNameById(0, HouseNameId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHouseName(int HouseNameId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.HouseName(User.UserId, User.HostName, User.DBName).DeleteById(0, HouseNameId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "HouseDress"

        [HttpPost]
        public JsonNetResult SaveHouseDress()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.HouseDress>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HouseDressId.HasValue)
                        beData.HouseDressId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.HouseDress(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllHouseDressList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.HouseDress(User.UserId, User.HostName, User.DBName).GetAllHouseDress(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetHouseDressById(int HouseDressId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.HouseDress(User.UserId, User.HostName, User.DBName).GetHouseDressById(0, HouseDressId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHouseDress(int HouseDressId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.HouseDress(User.UserId, User.HostName, User.DBName).DeleteById(0, HouseDressId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "StudentType"

        [HttpPost]
        public JsonNetResult SaveStudentType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.StudentType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.StudentTypeId.HasValue)
                        beData.StudentTypeId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.StudentType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllStudentTypeList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.StudentType(User.UserId, User.HostName, User.DBName).GetAllStudentType(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentTypeById(int StudentTypeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.StudentType(User.UserId, User.HostName, User.DBName).GetStudentTypeById(0, StudentTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelStudentType(int StudentTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.StudentType(User.UserId, User.HostName, User.DBName).DeleteById(0, StudentTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Medium"

        [HttpPost]
        public JsonNetResult SaveMedium()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Medium>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.MediumId.HasValue)
                        beData.MediumId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Medium(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllMediumList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Medium(User.UserId, User.HostName, User.DBName).GetAllMedium(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetMediumById(int MediumId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Medium(User.UserId, User.HostName, User.DBName).GetMediumById(0, MediumId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelMedium(int MediumId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Medium(User.UserId, User.HostName, User.DBName).DeleteById(0, MediumId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Department"

        [HttpPost]
        public JsonNetResult SaveDepartment()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Department>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DepartmentId.HasValue)
                        beData.DepartmentId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Department(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllDepartmentList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Department(User.UserId, User.HostName, User.DBName).GetAllDepartment(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDepartmentById(int DepartmentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Department(User.UserId, User.HostName, User.DBName).GetDepartmentById(0, DepartmentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDepartment(int DepartmentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Department(User.UserId, User.HostName, User.DBName).DeleteById(0, DepartmentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Designation"

        [HttpPost]
        public JsonNetResult SaveDesignation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Designation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DesignationId.HasValue)
                        beData.DesignationId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Designation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllDesignationList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Designation(User.UserId, User.HostName, User.DBName).GetAllDesignation(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDesignationById(int DesignationId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Designation(User.UserId, User.HostName, User.DBName).GetDesignationById(0, DesignationId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDesignation(int DesignationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Designation(User.UserId, User.HostName, User.DBName).DeleteById(0, DesignationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Level"

        [HttpPost]
        public JsonNetResult SaveLevel()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Level>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.LevelId.HasValue)
                        beData.LevelId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Level(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllLevelList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Level(User.UserId, User.HostName, User.DBName).GetAllLevel(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLevelById(int LevelId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Level(User.UserId, User.HostName, User.DBName).GetLevelById(0, LevelId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelLevel(int LevelId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Level(User.UserId, User.HostName, User.DBName).DeleteById(0, LevelId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ServiceType"

        [HttpPost]
        public JsonNetResult SaveServiceType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.ServiceType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ServiceTypeId.HasValue)
                        beData.ServiceTypeId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.ServiceType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllServiceTypeList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ServiceType(User.UserId, User.HostName, User.DBName).GetAllServiceType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetServiceTypeById(int ServiceTypeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ServiceType(User.UserId, User.HostName, User.DBName).GetServiceTypeById(0, ServiceTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelServiceType(int ServiceTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.ServiceType(User.UserId, User.HostName, User.DBName).DeleteById(0, ServiceTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Category"

        [HttpPost]
        public JsonNetResult SaveCategory()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Category>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CategoryId.HasValue)
                        beData.CategoryId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Category(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllCategoryList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Category(User.UserId, User.HostName, User.DBName).GetAllCategory(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCategoryById(int CategoryId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Category(User.UserId, User.HostName, User.DBName).GetCategoryById(0, CategoryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelCategory(int CategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Category(User.UserId, User.HostName, User.DBName).DeleteById(0, CategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Caste"

        [HttpPost]
        public JsonNetResult SaveCaste()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Caste>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CasteId.HasValue)
                        beData.CasteId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Caste(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonNetResult GetAllCasteList()
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.Caste(1, hostName, dbName).GetAllCaste(0);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.Caste(User.UserId, User.HostName, User.DBName).GetAllCaste(0);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            
        }

        [HttpPost]
        public JsonNetResult GetCasteById(int CasteId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Caste(User.UserId, User.HostName, User.DBName).GetCasteById(0, CasteId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelCaste(int CasteId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Caste(User.UserId, User.HostName, User.DBName).DeleteById(0, CasteId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "RemarksType"

        [HttpPost]
        public JsonNetResult SaveRemarksType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.RemarksType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.RemarksTypeId.HasValue)
                        beData.RemarksTypeId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.RemarksType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllRemarksTypeList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.RemarksType(User.UserId, User.HostName, User.DBName).GetAllRemarksType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRemarksTypeById(int RemarksTypeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.RemarksType(User.UserId, User.HostName, User.DBName).GetRemarksTypeById(0, RemarksTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelRemarksType(int RemarksTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.RemarksType(User.UserId, User.HostName, User.DBName).DeleteById(0, RemarksTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "DocumentType"

        [HttpPost]
        public JsonNetResult SaveDocumentType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.DocumentType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DocumentTypeId.HasValue)
                        beData.DocumentTypeId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.DocumentType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonNetResult GetAllDocumentTypeList()
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.DocumentType(1, hostName, dbName).GetAllDocumentType(0);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.DocumentType(User.UserId, User.HostName, User.DBName).GetAllDocumentType(0);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            
        }

        [HttpPost]
        public JsonNetResult GetDocumentTypeById(int DocumentTypeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.DocumentType(User.UserId, User.HostName, User.DBName).GetDocumentTypeById(0, DocumentTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDocumentType(int DocumentTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.DocumentType(User.UserId, User.HostName, User.DBName).DeleteById(0, DocumentTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Subject"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SubjectSetup, false)]
        public ActionResult Subject()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveSubject()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Subject>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.SubjectId.HasValue)
                        beData.SubjectId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [AllowAnonymous]
        public JsonNetResult GetAllSubjectList()
        {
            if (User!=null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var dataColl = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).GetAllSubject(0, this.AcademicYearId, null, null, null, true);

                    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
                }
                else
                {
                    var dataColl = new AcademicLib.BL.Academic.Creation.Subject(1, hostName, dbName).GetAllSubject(0, 1, null, null, null, true);

                    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
                }
                
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.Subject(1,hostName,dbName).GetAllSubject(0,1, null, null, null, true);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            
        }

        [HttpPost]
        public JsonNetResult GetSubjectListForLessonPlan(int ClassId, int? BatchId, int? ClassYearId, int? SemesterId/*, int? FacultyId*/)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).getSubjectListForLessonPlan(ClassId, BatchId, ClassYearId, SemesterId/*, FacultyId*/, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetClassWiseSubjectList(int? EmployeeId,int? ClassId,int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).GetAllSubject(0,this.AcademicYearId, EmployeeId, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSubjectById(int SubjectId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).GetSubjectById(0, SubjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelSubject(int SubjectId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Subject(User.UserId, User.HostName, User.DBName).DeleteById(0, SubjectId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Categories, false)]
        public ActionResult Student()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Categories, false)]
        public ActionResult Employee()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Categories, false)]
        public ActionResult Others()
        {
            return View();
        }
        
        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentRemarks, false)]
        public ActionResult StudentRemarks()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.StudentRemarks, false)]
        public JsonNetResult SaveStudentRemarks()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.StudentRemarks>(Request["jsonData"]);
                if (beData != null)
                { 
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files["file0"];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.FilePath = att.DocPath;
                        }
                    }
                      
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.StudentRemarks(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetRemarks(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(User.UserId, User.HostName, User.DBName).getRemarksList(this.AcademicYearId, DateTime.Today, DateTime.Today, null, StudentId, false,null);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRemarksList(DateTime dateFrom,DateTime dateTo,int? remarksTypeId,int? remarksFor)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.StudentRemarks(User.UserId, User.HostName, User.DBName).getRemarksList(this.AcademicYearId, dateFrom,dateTo,remarksTypeId,null,false,remarksFor);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Subject Mapping"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SubjectSetup, false)]
        public ActionResult SubjectMapping()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveSubjectMappingClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {             
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.SubjectMappingClassWiseCollections>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.SubjectMappingClassWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSubjectMappingClassWise(int ClassId,string SectionIdColl, int? SemesterId=null, int? ClassYearId = null, int? BatchId = null,int? BranchId=null)
        {
            
            var dataColl = new AcademicLib.BL.Academic.Transaction.SubjectMappingClassWise(User.UserId, User.HostName, User.DBName).getClassWiseSubjectMapping(this.AcademicYearId, ClassId, SectionIdColl,SemesterId,ClassYearId,BatchId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveSubjectMappingStudentWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.OptionalSubjectStudentWise>>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.SubjectMappingStudentWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSubjectMappingStudentWise(int ClassId, int? SectionId,int? SemesterId=null,int? ClassYearId=null,int? BatchId=null,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.SubjectMappingStudentWise(User.UserId, User.HostName, User.DBName).getStudentWiseSubjectMapping(this.AcademicYearId, ClassId, SectionId, SemesterId, ClassYearId, BatchId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult CopySubjectMappingClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.CopySubjectMapping>(Request["jsonData"]);
                if (dataColl != null)
                {
                    dataColl.AcademicYearId = this.AcademicYearId;
                    dataColl.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.SubjectMappingClassWise(User.UserId, User.HostName, User.DBName).copySubjectMappingClassWise(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ClassTimeTable, false)]
        public ActionResult ClassShedule()
       {
            return View();
       }

        #region "Save ClassShift"
        [HttpPost]
        public JsonNetResult SaveClassShift()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassShift>(Request["jsonData"]);
                if (dataColl != null)
                {
                    dataColl.CUserId = User.UserId;

                    if (!dataColl.ClassShiftId.HasValue)
                        dataColl.ClassShiftId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ClassShift(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId,dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonNetResult GetAllClassShift()
        {
            
            if (User!=null)
            {
                var dataColl = new AcademicLib.BL.Academic.Transaction.ClassShift(User.UserId, User.HostName, User.DBName).GetAllClassShift(0, this.AcademicYearId, false);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Transaction.ClassShift(1, hostName,dbName).GetAllClassShift(0, this.AcademicYearId, false);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            
        }
        //Added by Simran
        [HttpPost]
        public JsonNetResult GetClassWiseShift(int? AcademicYearId,int ClassId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassShift(User.UserId, User.HostName, User.DBName).GetClassWiseShift(0, AcademicYearId, ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        //End
        [HttpPost]
        public JsonNetResult GetClassShiftById(int ClassShiftId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassShift(User.UserId, User.HostName, User.DBName).GetClassShiftById(0, ClassShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelClassShift(int ClassShiftId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ClassShift(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassShiftId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Period Management"
        [HttpPost]
        public JsonNetResult SavePeriodManagement()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.PeriodManagement>(Request["jsonData"]);
                if (dataColl != null)
                {
                    dataColl.CUserId = User.UserId;

                    if (!dataColl.TranId.HasValue)
                        dataColl.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.PeriodManagement(User.UserId, User.HostName, User.DBName).SaveFormData(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllPeriodManagement()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.PeriodManagement(User.UserId, User.HostName, User.DBName).GetAllPeriodManagement(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPeriodManagementById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.PeriodManagement(User.UserId, User.HostName, User.DBName).GetPeriodManagementById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetPeriodManagementByClassShiftId(int ClassShiftId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.PeriodManagement(User.UserId, User.HostName, User.DBName).getPeriodManagementByClassShiftId(0, ClassShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelPeriodManagement(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.PeriodManagement(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Class Schedule"
        [HttpPost]
        public JsonNetResult SaveClassSchedule()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassScheduleCollections>(Request["jsonData"]);
                if (dataColl != null)
                {
                
                    resVal = new AcademicLib.BL.Academic.Transaction.ClassSchedule(User.UserId, User.HostName, User.DBName).SaveFormData(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
      

        [HttpPost]
        public JsonNetResult GetClassScheduleByClassId(int ClassId,int? SectionId,int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassSchedule(User.UserId, User.HostName, User.DBName).GetClassScheduleByClassId(ClassId, SectionId, ClassShiftId, SemesterId, ClassYearId, BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelClassSchedule(int ClassId,int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ClassSchedule(User.UserId, User.HostName, User.DBName).DeleteByShiftId(0,ClassId, ClassShiftId, SemesterId, ClassYearId, BatchId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ClassGroup"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassTimeTable, false)]
        public JsonNetResult SaveClassGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ClassGroupId.HasValue)
                        beData.ClassGroupId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ClassGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassGroupList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassGroup(User.UserId, User.HostName, User.DBName).GetAllClassGroup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassGroupById(int ClassGroupId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassGroup(User.UserId, User.HostName, User.DBName).GetClassGroupById(0, ClassGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassTimeTable, false)]
        public JsonNetResult DelClassGroup(int ClassGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ClassGroup(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Physical Class Schedule"
        [HttpPost]
        public JsonNetResult SavePhysicalClassSchedule()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassScheduleCollections>(Request["jsonData"]);
                if (dataColl != null)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.PhysicalClassSchedule(User.UserId, User.HostName, User.DBName).SaveFormData(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetPhysicalClassScheduleByClassId(int ClassId, int? SectionId, int ClassShiftId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.PhysicalClassSchedule(User.UserId, User.HostName, User.DBName).GetClassScheduleByClassId(ClassId, SectionId, ClassShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        #endregion

        #region "Class Teacher"
        [HttpPost]
        public JsonNetResult SaveClassTeacher()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassTeacher>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ClassTeacherId.HasValue)
                        beData.ClassTeacherId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ClassTeacher(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassTeacherList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassTeacher(User.UserId, User.HostName, User.DBName).GetAllClassTeacher(0,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassTeacherById(int ClassTeacherId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ClassTeacher(User.UserId, User.HostName, User.DBName).GetClassTeacherById(0, ClassTeacherId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelClassTeacher(int ClassTeacherId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ClassTeacher(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassTeacherId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Class HOD"
        [HttpPost]
        public JsonNetResult SaveClassHOD()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.ClassHODCollections>(Request["jsonData"]);
                if (dataColl != null)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.HOD(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassHOD()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.HOD(User.UserId, User.HostName, User.DBName).GetAllHOD();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetClassHODByd(int DepartmentId, int EmployeeId, int ClassShiftId, int? SubjectId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.HOD(User.UserId, User.HostName, User.DBName).GetAllHOD(DepartmentId, EmployeeId, ClassShiftId, SubjectId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelClassHOD(int DepartmentId,int EmployeeId,int ClassShiftId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.HOD(User.UserId, User.HostName, User.DBName).DeleteById(DepartmentId, EmployeeId, ClassShiftId,this.AcademicYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.LessonPlan, false)]
        public ActionResult LessonPlan()
       {
            return View();
       }

        [HttpPost]
        public JsonNetResult GetLessonPlanByClassSubject(int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonPlanByClassSubjectWise(ClassId, SubjectId, SectionIdColl, BatchId, ClassYearId, SemesterId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonPlanByClass(int? ClassId,int? SectionId,int? EmployeeId,int? SubjectId,int ReportType, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonPlanByClass(ClassId,SectionId,EmployeeId,SubjectId,ReportType, BatchId, ClassYearId, SemesterId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveLessonPlan()
        {
            string photoLocation = "/Attachments/lms";
            ResponeValues resVal = new ResponeValues();
           
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlan>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.AcademicYearId = this.AcademicYearId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;                        
                        HttpPostedFileBase file = filesColl["file0"];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.CoverFilePath = att.DocPath;                            
                        }
                    }


                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult UpdateLessonPlanDate()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlan>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).UpdatePlanDate(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult UpdateLessonPlan()
        {
            return View();
        }
        public ActionResult TodaysClass()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetTodayLessonPlan(DateTime? forDate,int? classId,int? sectionId,int? subjectId,int? employeeId,int reportType, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getTodayLessonPlan(this.AcademicYearId, forDate, classId, sectionId, subjectId, employeeId, reportType, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult SyllabusStatus()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.UploadPhotosSignature, false)]
        public ActionResult UploadPhotoSignature()
       {
            return View();
       }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.UploadPhotosSignature, false)]
        public JsonNetResult UpdateStudentPhoto()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto>(Request["jsonData"]);
                if (Request.Files.Count > 0 && beData!=null)
                {
                    string query = "";
                    //switch (beData.PhotoUploadedBy)
                    //{
                    //    case 1:
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where RegNo=@RegNo";
                    //        break;
                    //    case 2:                            
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where BoardRegNo=@BoardRegNo";
                    //        break;
                    //    case 3:
                    //        {
                    //            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where EnrollNo=@EnrollNo";                                
                    //        }
                    //        break;
                    //    case 4:
                    //        {
                    //            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where CardNo=@CardNo";                                
                    //        }
                    //        break;
                    //    case 5:
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where EMSId=@EMSId";                            
                    //        break;
                    //    case 6:
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where StudentId=@StudentId";
                    //        break;
                    //    case 7:
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where AutoNumber=@AutoNumber";
                    //        break;
                    //    case 8:
                    //        query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where RollNo=@RollNo and ClassId=@ClassId and SectionId=@SectionId ";
                    //        break;
                    //}
                    //Commented above switch case and added following
                    switch (beData.PhotoUploadedBy)
                    {
                        case 1:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where RegNo=@RegNo";
                            break;
                        case 2:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where BoardRegNo=@BoardRegNo";
                            break;
                        case 3:
                            {
                                query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where EnrollNo=@EnrollNo";
                            }
                            break;
                        case 4:
                            {
                                query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where CardNo=@CardNo";
                            }
                            break;
                        case 5:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where EMSId=@EMSId";
                            break;
                        case 6:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where AutoNumber=@AutoNumber";
                            break;
                        case 7:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where StudentId=@StudentId";
                            break;
                        case 8:
                            query = "update top(1) tbl_Student set PhotoPath=@PhotoPath where RollNo=@RollNo and ClassId=@ClassId and SectionId=@SectionId ";
                            break;
                    }
                    //Ends
                    var filesColl = Request.Files;                    
                    int fInd = 0;
                    List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto> studentColl = new List<AcademicLib.BE.Academic.Transaction.ImportStudentPhoto>();
                    for (int f=0;f<filesColl.Count;f++)
                    {
                        AcademicLib.BE.Academic.Transaction.ImportStudentPhoto student = new AcademicLib.BE.Academic.Transaction.ImportStudentPhoto();
                        HttpPostedFileBase file = filesColl["file" + f];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            if (att != null)
                            {
                                student.PhotoPath = att.DocPath;
                                //switch (beData.PhotoUploadedBy)
                                //{
                                //    case 1:
                                //        student.RegNo = att.Name.Replace(att.Extension, "");
                                //        break;
                                //    case 2:
                                //        student.BoardRegdNo = att.Name.Replace(att.Extension, "");
                                //        break;
                                //    case 3:
                                //        {
                                //            int val = 0;
                                //            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                //            student.EnrollNo = val;
                                //        }                                        
                                //        break;
                                //    case 4:
                                //        {
                                //            int val = 0;
                                //            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                //            student.CardNo = val;
                                //        }                                        
                                //        break;
                                //    case 5:
                                //        student.EMSId = att.Name.Replace(att.Extension, "");
                                //        break;
                                //    case 6:
                                //        {
                                //            int val = 0;
                                //            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                //            student.StudentId = val;
                                //        }
                                //        break;
                                //    case 7:
                                //        {
                                //            int val = 0;
                                //            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                //            student.AutoNumber = val;
                                //        }
                                //        break;
                                //    case 8:
                                //        {
                                //            student.ClassId = beData.ClassId;
                                //            student.SectionId = beData.SectionId;
                                //            int val = 0;
                                //            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                //            student.RollNo = val;
                                //        }
                                //        break;
                                //}

                                //Commented above and added following code
                                switch (beData.PhotoUploadedBy)
                                {
                                    case 1:
                                        student.RegNo = att.Name.Replace(att.Extension, "");
                                        break;
                                    case 2:
                                        student.BoardRegdNo = att.Name.Replace(att.Extension, "");
                                        break;
                                    case 3:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.EnrollNo = val;
                                        }
                                        break;
                                    case 4:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.CardNo = val;
                                        }
                                        break;
                                    case 5:
                                        student.EMSId = att.Name.Replace(att.Extension, "");
                                        break;
                                    case 6:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.AutoNumber = val;
                                        }
                                        break;
                                    case 7:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.StudentId = val;
                                        }
                                        break;
                                    case 8:
                                        {
                                            student.ClassId = beData.ClassId;
                                            student.SectionId = beData.SectionId;
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.RollNo = val;
                                        }
                                        break;
                                }
                                studentColl.Add(student);
                            }
                        }
                        fInd++;
                    }

                    resVal= new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateStudentPhoto_Query(studentColl, query);
                }              
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Download, false)]
        public ActionResult Download()
       {
            return View();
       }

        public ActionResult LMS()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult SaveLessonTeacherContent()
        {
            string photoLocation = "/Attachments/lms";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>>(Request["jsonData"]);
                if (dataColl != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        int fInd = 0;
                        foreach (var beData in dataColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.FilePath = att.DocPath;
                                beData.FileName = att.Name;
                            }
                            fInd++;
                        }
                    }
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTeacherContent(dataColl);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTeacherContent(int LessonId, int LessonSNo)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonTeacherContent(LessonId, LessonSNo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult SaveLessonTopicTeacherContent()
        {
            string photoLocation = "/Attachments/lms";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>>(Request["jsonData"]);
                if (dataColl != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        int fInd = 0;
                        foreach (var beData in dataColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.FilePath = att.DocPath;
                                beData.FileName = att.Name;
                            }
                            fInd++;
                        }
                    }
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTopicTeacherContent(dataColl);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTopicTeacherContent(int LessonId, int LessonSNo, int TopicSNo)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonTopicTeacherContent(LessonId, LessonSNo, TopicSNo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [ValidateInput(false)]
        [HttpPost] 
        public JsonNetResult SaveLessonTopicContent()
        {
            string photoLocation = "/Attachments/lms";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicContent>>(Request["jsonData"]);
                if (dataColl != null)
                { 
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        int fInd = 0;
                        foreach(var beData in dataColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.FilePath = att.DocPath;
                                beData.FileName = att.Name;
                            }
                            fInd++;
                        }
                    }
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTopicContent(dataColl);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTopicContent(/*int LessonId,*/int LessonSNo,int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonTopicContent(/*LessonId, */LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveLessonTopicVideo()
        { 
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<List<AcademicLib.BE.Academic.Transaction.LessonTopicVideo>>(Request["jsonData"]);
                if (dataColl != null)
                {                    
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTopicVideo(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTopicVideo(/*int LessonId,*/int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonTopicVideo(/*LessonId, */LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult SaveLessonTopicQuiz()
        {
            string photoLocation = "/Attachments/lms";
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopicQuiz>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;                        
                        foreach (var que in beData.QuestionColl)
                        {
                            HttpPostedFileBase file = filesColl["file-q" + que.SNo];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                que.ContentPath = att.DocPath;                                
                            }

                            foreach(var ans in que.AnswerColl)
                            {
                                HttpPostedFileBase file1 = filesColl["file-q" + que.SNo+"-a"+ans.SNo];
                                if (file1 != null)
                                {
                                    var att = GetAttachmentDocuments(photoLocation, file1);
                                    ans.ContentPath = att.DocPath;
                                }
                            }
                        }
                    }

                    if (!beData.QuizId.HasValue)
                        beData.QuizId = 0;
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTopicQuiz(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTopicQuiz(int QuizId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).getLessonTopicQuiz(QuizId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult StartLesson()
        { 
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlanDetails>(Request["jsonData"]);
                if (dataColl != null)
                {                     
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).StartLesson(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult EndLesson()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonPlanDetails>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).EndLesson(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult StartTopic()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopic>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).StartTopic(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult EndTopic()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopic>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).EndTopic(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult StartTopicContent()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).StartTopicContent(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonNetResult EndTopicContent()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var dataColl = DeserializeObject<AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).EndTopicContent(dataColl);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.UploadPhotosSignature, false)]
        public JsonNetResult UpdateEmployeePhoto()
        {
            string photoLocation = "/Attachments/academic/employee";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto>(Request["jsonData"]);
                if (Request.Files.Count > 0 && beData != null)
                {
                    string query = "";
                    switch (beData.PhotoUploadedBy)
                    {
                        case 1:
                            query = "update tbl_Employee set PhotoPath=@PhotoPath where EmployeeCode=@EmployeeCode";
                            break;
                 
                        case 2:
                            {
                                query = "update tbl_Employee set PhotoPath=@PhotoPath where EnrollNumber=@EnrollNumber";
                            }
                            break;
                        case 3:
                            {
                                query = "update tbl_Employee set PhotoPath=@PhotoPath where CardNo=@CardNo";
                            }
                            break;                      
                        case 4:
                            query = "update tbl_Employee set PhotoPath=@PhotoPath where EmployeeId=@EmployeeId";
                            break;
                        case 5:
                            query = "update tbl_Employee set PhotoPath=@PhotoPath where AutoNumber=@AutoNumber";
                            break;
                    }

                    var filesColl = Request.Files;
                    int fInd = 0;
                    List<AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto> studentColl = new List<AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto>();
                    for (int f = 0; f < filesColl.Count; f++)
                    {
                        AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto student = new AcademicLib.BE.Academic.Transaction.ImportEmployeePhoto();
                        HttpPostedFileBase file = filesColl["file" + f];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            if (att != null)
                            {
                                student.PhotoPath = att.DocPath;
                                switch (beData.PhotoUploadedBy)
                                {
                                    case 1:
                                        student.EmployeeCode = att.Name.Replace(att.Extension, "");
                                        break;
                                    case 2:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.EnrollNo = val;
                                        }
                                        break;
                                    case 3:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.CardNo = val;
                                        }
                                        break;
                                   
                                    case 4:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.EmployeeId = val;
                                        }
                                        break;
                                    case 5:
                                        {
                                            int val = 0;
                                            int.TryParse(att.Name.Replace(att.Extension, ""), out val);
                                            student.AutoNumber = val;
                                        }
                                        break;
                                }

                                studentColl.Add(student);
                            }
                        }
                        fInd++;
                    }

                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).UpdateEmployeePhoto_Query(studentColl, query);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult CEHRD()
        {
            return View();
        }

        public ActionResult MidasLMS()
        {
            ResponeValues resVal = new ResponeValues();
            var usr = User;
            resVal.ResponseMSG = new Global.GlobalFunction(usr.UserId, usr.HostName, usr.DBName, "").GetMidasLMS(usr.BranchId);

            return View(resVal);
        }

        public ActionResult EPustakalaya()
        {
            return View();
        }

        

        #region "SubjectGroup"


        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SubjectSetup, false)]
        public JsonNetResult SaveSubjectGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.SubjectGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.SubjectGroupId.HasValue)
                        beData.SubjectGroupId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.SubjectGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllSubjectGroup()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.SubjectGroup(User.UserId, User.HostName, User.DBName).GetAllSubjectGroup(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        } 
        [HttpPost]
        public JsonNetResult GetSubjectGroupById(int SubjectGroupId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.SubjectGroup(User.UserId, User.HostName, User.DBName).GetSubjectGroupById(0, SubjectGroupId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelSubjectGroup(int SubjectGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.SubjectGroup(User.UserId, User.HostName, User.DBName).DeleteById(0, SubjectGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "InsuranceType"


        [HttpPost]
        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.InsuranceType, false)]
        public JsonNetResult SaveInsuranceType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.InsuranceType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.InsuranceId.HasValue)
                        beData.InsuranceId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.InsuranceType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllInsuranceType()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.InsuranceType(User.UserId, User.HostName, User.DBName).GetAllInsuranceType(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetInsuranceTypeById(int InsuranceId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.InsuranceType(User.UserId, User.HostName, User.DBName).GetInsuranceTypeById(0, InsuranceId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        //[PermissionsAttribute(Actions.Delete, (int)ENTITIES.InsuranceType, false)]
        public JsonNetResult DelInsuranceType(int InsuranceId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.InsuranceType(User.UserId, User.HostName, User.DBName).DeleteById(0, InsuranceId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        public ActionResult AssignRole()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetStudentDocFileById(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.DownoadFileData(User.UserId, User.HostName, User.DBName).GetStudentDocFile(0, StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmployeeDocFileById(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.DownoadFileData(User.UserId, User.HostName, User.DBName).GetEmployeeDocFile(0, EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "ParentTeacherMeeting"

        public ActionResult ParentTeacherMeeting()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveParentTeacherMeeting()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ParentTeacherMeeting(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllParentTeacherMeeting()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ParentTeacherMeeting(User.UserId, User.HostName, User.DBName).GetAllParentTeacherMeeting(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetParentTeacherMeetingById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ParentTeacherMeeting(User.UserId, User.HostName, User.DBName).GetParentTeacherMeetingById(0, TranId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelParentTeacherMeeting(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ParentTeacherMeeting(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllStudentPTM(int ClassId, int? SectionId, DateTime? PTMDate, int? PTMBy)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.ParentTeacherMeeting(User.UserId, User.HostName, User.DBName).GetAllStudentPTM(0, ClassId, SectionId, PTMDate, PTMBy);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        [HttpPost]
        public JsonNetResult GetAllPhotoStatus()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.PhotoStatus(User.UserId, User.HostName, User.DBName).GetAllPhotoStatus(0, this.AcademicYearId, 1);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #region"teacherWiseQuota"


        [HttpPost]
        public JsonNetResult GetTeacherWiseQuota(int? DepartmentId)
        {
            AcademicLib.BE.Academic.Transaction.TeacherWiseQuotaCollections dataColl = new AcademicLib.BE.Academic.Transaction.TeacherWiseQuotaCollections();
            try
            {

                dataColl = new AcademicLib.BL.Academic.Transaction.TeacherWiseQuota(User.UserId, User.HostName, User.DBName).GetTeacherWiseQuota(DepartmentId, 0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveTeacherWiseQuota()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.TeacherWiseQuotaCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Academic.Transaction.TeacherWiseQuota(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, User.BranchId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllTeacherWiseQuota(int? DepartmentId, int? DesignationId)
        {
            AcademicLib.BE.Academic.Transaction.TeacherWiseQuotaCollections dataColl = new AcademicLib.BE.Academic.Transaction.TeacherWiseQuotaCollections();
            try
            {

                dataColl = new AcademicLib.BL.Academic.Transaction.TeacherWiseQuota(User.UserId, User.HostName, User.DBName).GetAllTeacherWiseQuota(DepartmentId, DesignationId, 0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        public ActionResult TermSyllabus()
        {
            return View();
        }
        #region "Syllabus"
        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveSyllabus()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Academic.Creation.TermSyllabus>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Academic.Creation.TermSyllabus(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllSyllabus()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.TermSyllabus(User.UserId, User.HostName, User.DBName).GetAllSyllabus(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassSubjectWiseSyllabus(int? BatchId, int? ClassId, int? SemesterId, int? ClassYearId, int? ExamTypeId, int? SubjectId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.TermSyllabus(User.UserId, User.HostName, User.DBName).GetClassSubjectwiseTermSyllabus(0, BatchId, ClassId, SemesterId, ClassYearId, ExamTypeId, SubjectId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        [HttpPost]

        public JsonNetResult SaveUpdateCoordinator()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.CoordinatorCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Academic.Creation.Coordinator(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCoordinatorClass(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Coordinator(User.UserId, User.HostName, User.DBName).GetCoordinatorClasswise(EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllClassCoordinator()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.Coordinator(User.UserId, User.HostName, User.DBName).GetAllCoordinator();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelClassCoordinator(int EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Coordinator(User.UserId, User.HostName, User.DBName).DeleteById(EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "ClassGroups"
        [HttpPost]
        public JsonNetResult SaveClassGroups()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.ClassGroups>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.ClassGroupId.HasValue)
                        beData.ClassGroupId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.ClassGroups(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllClassGroups()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassGroups(User.UserId, User.HostName, User.DBName).GetAllClassGroups(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetClassGroupsById(int ClassGroupId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.ClassGroups(User.UserId, User.HostName, User.DBName).GetClassGroupsById(0, ClassGroupId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelClassGroups(int ClassGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.ClassGroups(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        //Left Type
        #region "LeftType"

        [HttpPost]

        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveLeftType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.LeftType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.LeftTypeId.HasValue)
                        beData.LeftTypeId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.LeftType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllLeftTypeList()
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.LeftType(User.UserId, User.HostName, User.DBName).GetAllLeftType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        
        [HttpPost]
        public JsonNetResult GetLeftTypeById(int LeftTypeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Creation.LeftType(User.UserId, User.HostName, User.DBName).GetLeftTypeById(0, LeftTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelLeftType(int LeftTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.LeftType(User.UserId, User.HostName, User.DBName).DeleteLeftTypeById(0, LeftTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        //For Techer Quota
        [HttpPost]
        public JsonNetResult GetAllTeacherQuota()
        {
            AcademicLib.RE.Academic.TeacherQuotaCollections dataColl = new AcademicLib.RE.Academic.TeacherQuotaCollections();
            try
            {

                dataColl = new AcademicLib.BL.Academic.Transaction.TeacherWiseQuota(User.UserId, User.HostName, User.DBName).GetAllTeacherQuota();
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllEmployeePhotoDownloadLog()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).GetAllEmployeePhotoDownloadLog(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllStudentPhotoDownloadLog()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).GetAllStudentPhotoDownloadLog(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetLessonTopicQuizList(/*int LessonId,*/int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).GetLessonTopicQuizList(/*LessonId, */LessonSNo, TopicSNo, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelLessonTopicQuizById(int QuizId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).DelLessonTopicQuizById(0, QuizId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        
        [HttpPost]
        public JsonNetResult GetClassSetupAutoOrderNo(int? For)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Creation.Class(User.UserId, User.HostName, User.DBName).GetClassSetupAutoOrderNo(0,For);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

    }
}