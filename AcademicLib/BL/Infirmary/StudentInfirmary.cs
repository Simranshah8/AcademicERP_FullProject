using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicERP.BL
{
    public class StudentInfirmary
    {
        DA.StudentInfirmaryDB db = null;
        int _UserId = 0;
        public StudentInfirmary(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.StudentInfirmaryDB(hostName, dbName);
        }

        public ResponeValues SaveFormDataPMHistory(BE.StudentPastMedicalHistory beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidDataPM(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdatePMHistory(beData, isModify);
            else
                return isValid;
        }

		public ResponeValues SaveFormDataHealthISsue(BE.StudentHealthIssue beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataHealthIssue(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateHealthIssue(beData, isModify);
			else
				return isValid;
		}
		public ResponeValues SaveStudentImmunization(BE.StudentHealthImmunization beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataSI(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateStudentImmunization(beData, isModify);
			else
				return isValid;
		}

		public ResponeValues SaveFormDataStudentCLEvaluation(BE.StudentClinicalLabEvaluation beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataSCLE(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateStudentCLEvaluation(beData, isModify);
			else
				return isValid;
		}

		public BE.StudentDetForInfirmary GetStudentDetForInfirmary(int EntityId, int StudentId)
        {
            return db.getStudentForInfirmaryById(_UserId, EntityId, StudentId);
        }

        public BE.MedicalProductsCollections GetAllMEdicalProducts(int EntityId)
        {
            return db.getAllMedicalProduct(_UserId, EntityId);
        }

		public BE.StudentPastMedicalHistoryCollections GetAllStudentPastMedicalHistory(int EntityId)
		{
			return db.getAllStudentPastMedicalHistory(_UserId, EntityId);
		}

		public BE.StudentPastMedicalHistory GetStudentPastMedicalHistoryById(int EntityId, int TranId)
		{
			return db.getStudentPastMedicalHistoryById(_UserId, EntityId, TranId);
		}

		//PrashantCode
		public BE.StudentPastMedicalHistoryCollections getStudentMedicalHistoryById(int EntityId, int StudentId)
		{
			return db.getStudentMedicalHistoryById(_UserId, EntityId, StudentId);
		}
		//PrashantCode End

		public ResponeValues DeletePastMedicalHistorybyId(int EntityId, int TranId)
		{
			return db.DeleteStudentPastMedicalHistoryById(_UserId, EntityId, TranId);
		}
		

		//Student HEalth ISsue tab code starts
		public ResponeValues IsValidDataPM(ref BE.StudentPastMedicalHistory beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.StudentId == 0 )
				{
					resVal.ResponseMSG = "Please ! Select Student ";
				}
				else if (beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = "Please ! Select HealthIssue ";
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

		public BE.StudentHealthIssueCollections GetAllStudentHealthIssue(int EntityId)
		{
			return db.getAllStudentHealthIssue(_UserId, EntityId);
		}

		public BE.StudentHealthIssue GetStudentHealthIssueById(int EntityId, int TranId)
		{
			return db.getStudentHealthIssueById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteStudentHealthIssueById(int EntityId, int TranId)
		{
			return db.DeleteStudentHealthIssueById(_UserId, EntityId, TranId);
		}

		//PrashantCode
		public BE.StudentHealthIssueCollections getStudentMedicalIssuesById(int EntityId, int StudentId)
		{
			return db.getStudentMedicalIssuesById(_UserId, EntityId, StudentId);
		}

		public BE.StudentGCheckupCollections GetStudentGeneralCheckup(int EntityId, int TranId)
		{
			return db.GetStudentGeneralCheckup(_UserId, EntityId, TranId);
		}
		public BE.StudentHealthImmunizationCollections GetStudentHealthImmunization(int EntityId, int StudentId)
		{
			return db.GetStudentHealthImmunization(_UserId, EntityId, StudentId);
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections GetStudentHealthImmunizationDoc(int EntityId, int TranId)
		{
			return db.GetStudentHealthImmunizationDoc(_UserId, EntityId, TranId);
		}
		public BE.StudentClinicalLabEvaluationCollections GetStudentClinicalLabEvaluation(int EntityId, int StudentId)
		{
			return db.getStudentClinicalLabEvaluation(_UserId, EntityId, StudentId);
		}
		//PrashantCode End

		public ResponeValues IsValidDataHealthIssue(ref BE.StudentHealthIssue beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.StudentId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Student ";
				}
				else if (beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = "Please ! Select HealthIssue ";
				}
				else if (beData.PrescribedBy == 0)
				{
					resVal.ResponseMSG = "Please ! Select PrescribedBy ";
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


		//Student Immunization Tab Starts

		public BE.StudentHealthImmunizationCollections GetAllStudentHealthImmunization(int EntityId)
		{
			return db.getAllStudentHealthImmunization(_UserId, EntityId);
		}
		public BE.StudentHealthImmunization GetStudentHealthImmunizationById(int EntityId, int TranId)
		{
			return db.getStudentHealthImmunizationById(_UserId, EntityId, TranId);
		}

		public ResponeValues DeleteStudentImmunizationById(int EntityId, int TranId)
		{
			return db.DeleteStudentImmunizationById(_UserId, EntityId, TranId);
		}

		public BE.LabValueCollections GetLabValueById(int EntityId, int TestNameId)
		{
			return db.getLabValueById(_UserId, EntityId, TestNameId);
		}
		public ResponeValues IsValidDataSI(ref BE.StudentHealthImmunization beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.StudentId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Student ";
				}
				else if (beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = "Please ! Select HealthIssue ";
				}
				else if (beData.VaccineId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Vaccine ";
				}
				else if (beData.VaccinatorId == 0 )
				{
					resVal.ResponseMSG = "Please ! Select Vaccinator ";
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


		//Student Clinical Lab Evaluation STarts

		public BE.StudentClinicalLabEvaluationCollections GetAllStudentClinicalLabEvaluation(int EntityId)
		{
			return db.getAllStudentClinicalLabEvaluation(_UserId, EntityId);
		}

		public BE.StudentClinicalLabEvaluation GetStudentClinicalLabEvaluationById(int EntityId, int TranId)
		{
			return db.getStudentClinicalLabEvaluationById(_UserId, EntityId, TranId);
		}
		
		public ResponeValues DeleteStudentLabEvaluationById(int EntityId, int TranId)
		{
			return db.DeleteSLEvaluationById(_UserId, EntityId, TranId);
		}
		//Student General Checkup
		public ResponeValues SaveStudentGeneralCheckup(BE.StudentGCheckupCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.UpdateStudentGeneralCheckup(_UserId, dataColl);

			return resVal;
		}
		public ResponeValues IsValidDataSCLE(ref BE.StudentClinicalLabEvaluation beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.StudentId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Student ";
				}
				else if (beData.TestNameId == 0)
				{
					resVal.ResponseMSG = "Please ! Select TestName ";
				}
				else if (beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = "Please ! Select HealthIssue ";
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