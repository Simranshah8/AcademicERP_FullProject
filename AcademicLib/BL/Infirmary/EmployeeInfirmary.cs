using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL
{

	public class EmployeeInfirmary
	{

		DA.EmployeeInfirmaryDB db = null;

		int _UserId = 0;

		public EmployeeInfirmary(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.EmployeeInfirmaryDB(hostName, dbName);
		}
		public ResponeValues SaveFormDataPMHistory(BE.EmployeePastMedicalHistory beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataPM(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdatePMHistory(beData, isModify);
			else
				return isValid;
		}
		public BE.EmployeeDetForInfirmary getEmployeeForInfirmaryById(int? EntityId, int EmployeeId)
		{
			return db.getEmployeeForInfirmaryById(_UserId, EntityId, EmployeeId);
		}
		public BE.EmployeePastMedicalHistoryCollections getEmployeeMedicalHistoryById(int? EntityId, int EmployeeId)
		{
			return db.getEmployeeMedicalHistoryById(_UserId, EntityId, EmployeeId);
		}
		public ResponeValues IsValidDataPM(ref BE.EmployeePastMedicalHistory beData, bool IsModify)
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
				else if (beData.EmployeeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Employee ";
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


		public BE.EmployeePastMedicalHistory GetEmployeePastMedicalHistoryById(int? EntityId, int TranId)
		{
			return db.getEmployeePastMedicalHistoryById(_UserId, EntityId, TranId);
		}

		public ResponeValues DelEmpPastMedHty(int? EntityId, int TranId)
		{
			return db.DeleteEmployeePastMedicalHistoryById(_UserId, EntityId, TranId);
		}

        //For Health Issue Tab code starts
		public BE.EmployeeHealthIssueCollections getEmployeeMedicalIssuesById(int? EntityId, int EmployeeId)
		{
			return db.getEmployeeMedicalIssuesById(_UserId, EntityId, EmployeeId);
		}

		public ResponeValues SaveFormDataHealthISsue(BE.EmployeeHealthIssue beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataHealthIssue(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateHealthIssue(beData, isModify);
			else
				return isValid;
		}
		public ResponeValues IsValidDataHealthIssue(ref BE.EmployeeHealthIssue beData, bool IsModify)
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
				else if (beData.EmployeeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Employee ";
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


		public BE.EmployeeHealthIssue getEmployeeHealthIssueById(int? EntityId, int TranId)
		{
			return db.getEmployeeHealthIssueById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteEmployeeHealthIssue(int? EntityId, int TranId)
		{
			return db.DeleteEmployeeHealthIssue(_UserId, EntityId, TranId);
		}

		//Employee General Checkup
		public ResponeValues SaveEmployeeGeneralCheckup(BE.EmployeeGCheckupCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.UpdateEmployeeGeneralCheckup(_UserId, dataColl);

			return resVal;
		}
		public BE.EmployeeGCheckupCollections GetEmployeeGeneralCheckup(int EntityId, int TranId)
		{
			return db.GetEmployeeGeneralCheckup(_UserId, EntityId, TranId);
		}


        //Employee Health Immunization Tab Code Starts
		public ResponeValues SaveEmployeeImmunization(BE.EmployeeHealthImmunization beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataSI(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateEmployeeImmunization(beData, isModify);
			else
				return isValid;
		}
		public ResponeValues IsValidDataSI(ref BE.EmployeeHealthImmunization beData, bool IsModify)
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
				else if (beData.EmployeeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Employee ";
				}
				else if (beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = "Please ! Select HealthIssue ";
				}
				else if (beData.VaccineId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Vaccine ";
				}
				else if (beData.VaccinatorId == 0)
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

		public BE.EmployeeHealthImmunizationCollections GetEmployeeHealthImmunization(int EntityId, int EmployeeId)
		{
			return db.GetEmployeeHealthImmunization(_UserId, EntityId, EmployeeId);
		}
		public BE.EmployeeHealthImmunization GetEmployeeHealthImmunizationById(int EntityId, int TranId)
		{
			return db.getEmployeeHealthImmunizationById(_UserId, EntityId, TranId);
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections GetEmployeeHealthImmunizationDoc(int EntityId, int TranId)
		{
			return db.GetEmployeeHealthImmunizationDoc(_UserId, EntityId, TranId);
		}
		public ResponeValues DelEmpHealthImmunization(int? EntityId, int TranId)
        {
			return db.DeleteEmployeeImmunizationById(_UserId, EntityId, TranId);
        }

		//for Clinical Lab Evaluation
		public ResponeValues SaveFormDataEmployeeCLEvaluation(BE.EmployeeClinicalLabEvaluation beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidDataSCLE(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateEmployeeCLEvaluation(beData, isModify);
			else
				return isValid;
		}

		public ResponeValues IsValidDataSCLE(ref BE.EmployeeClinicalLabEvaluation beData, bool IsModify)
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
				else if (beData.EmployeeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Employee ";
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

		public BE.EmployeeClinicalLabEvaluationCollections GetEmpLabEval(int? EntityId, int EmployeeId)
        {
			return db.getEmployeeClinicalLabEvaluation(_UserId, EntityId, EmployeeId);

		}
		public BE.EmployeeClinicalLabEvaluation getEmployeeClinicalLabEvaluationById(int? EntityId, int TranId)
		{
			return db.getEmployeeClinicalLabEvaluationById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteSLEvaluationById(int? EntityId, int TranId)
		{
			return db.DeleteSLEvaluationById(_UserId, EntityId, TranId);
		}
	}

}

