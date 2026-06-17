using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{

	public class NewBankAccount
	{

		AcademicLib.DA.Academic.Transaction.NewBankAccountDB db = null;

		int _UserId = 0;

		public NewBankAccount(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Academic.Transaction.NewBankAccountDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Academic.Transaction.NewBankAccount beData)
		{
			bool isModify = beData.BankId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Academic.Transaction.NewBankAccountCollections GetAllNewBankAccount(int EntityId)
		{
			return db.getAllNewBankAccount(_UserId, EntityId);
		}
		public AcademicLib.BE.Academic.Transaction.NewBankAccount GetNewBankAccountById(int EntityId, int BankId,int? ForUserId)
		{
			return db.getNewBankAccountById(_UserId, EntityId, BankId,ForUserId);
		}
		public ResponeValues DeleteById(int EntityId, int BankId)
		{
			return db.DeleteById(_UserId, EntityId, BankId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Academic.Transaction.NewBankAccount beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.BankId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.BankId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.Saluation==0)
				{
					resVal.ResponseMSG = "Please select Salutation";
				}
				else if (string.IsNullOrEmpty(beData.Gender))
				{
					resVal.ResponseMSG = "Please select Gender";
				}
				else if (string.IsNullOrEmpty(beData.FirstName))
                {
					resVal.ResponseMSG = "Please ! Enter First Name";
                }
				else if (!beData.DOB.HasValue || beData.DOB == null)
				{
					resVal.ResponseMSG = "Please provide a Date of Birth";
				}

				else if (!beData.Education.HasValue || beData.Education == 0)
				{
					resVal.ResponseMSG = "Please ! Select Education Label";
				}
				else if (beData.AnnualIncome < 0)
                {
					resVal.ResponseMSG = "Please ! Enter Positive Value for Annual Income";
                }
				else if (beData.PurposeofAccount == 0)
                {
					resVal.ResponseMSG = "Please ! Select Purpose of account";
                }
				else if (beData.Occupation == 0)
				{
					resVal.ResponseMSG = "Please ! Select Occupation";
				}
				else if (beData.MaritalStatusId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Marital Status";
				}
				else if(beData.MaritalStatusId==1  && string.IsNullOrEmpty(beData.SpouseName))
                {
					resVal.ResponseMSG = "Please ! Enter Spouse Name";
                }
				else if (beData.MaritalStatusId == 1 & beData.Gender=="F" && string.IsNullOrEmpty(beData.FatherInLawName))
				{
					resVal.ResponseMSG = "Please ! Enter Father In Law Name";
				}
				else if (beData.CA_ProvinceId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter Province of Current Address";
				}

				else if (  beData.CA_DistrictId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter District of Current Address";
				}

				else if (  beData.CA_LocalLevelId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter Local Level of Current Address ";
				}

				else if (  beData.PA_ProvinceId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter Province of Permanent Address";
				}

				else if (  beData.PA_DistrictId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter District of Permanent Address";
				}
				else if (  beData.PA_LocalLevelId == 0)
				{
					resVal.ResponseMSG = "Please ! Enter Local Level of Permanent Address ";
				}

				else if (!beData.TypeofIdSubmittedId.HasValue || beData.TypeofIdSubmittedId == 0)
				{
					resVal.ResponseMSG = "Please ! Select the type of Submitted Id ";
				}
				else if (!beData.TypeofIdSubmittedId.HasValue || beData.TypeofIdSubmittedId == 0)
				{
					resVal.ResponseMSG = "Please ! Select the type of Submitted Id ";
				}
				else if (beData.EmployeeType == 0)
				{
					resVal.ResponseMSG = "Please select Employee Type";
				}
				else if(beData.SourceOfIncomeId==7 && string.IsNullOrEmpty(beData.OtherSourceOfIncome))
                {
					resVal.ResponseMSG = "Please ! Enter Other Source of Income";
                }
				else if (beData.PurposeofAccount==0)
				{
					resVal.ResponseMSG = "Please select Purpose of Account";
				}

				else if ( beData.SourceOfIncomeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Source Of Income";
				}
					
				else if ( beData.AnnualTransaction == 0)
				{
					resVal.ResponseMSG = "Please ! Enter Estimated Annual Transaction ";
				}
				else if (string.IsNullOrEmpty(beData.FatherName))
                {
					resVal.ResponseMSG = "Please ! Enter Father Name";
                }
				else if (string.IsNullOrEmpty(beData.MotherName))
				{
					resVal.ResponseMSG = "Please ! Enter Mother Name";
				}
				else if (string.IsNullOrEmpty(beData.GrandfatherName))
				{
					resVal.ResponseMSG = "Please ! Enter Grand Father Name";
				}
				//else if (string.IsNullOrEmpty(beData.GrandmotherName))
				//{
				//	resVal.ResponseMSG = "Please ! Enter Grand Mother Name";
				//}
				else if (beData.IdIssuePlace==0)
				{
					resVal.ResponseMSG = "Please ! Select Valid Id Issue Place Name";
				}
				else if(!string.IsNullOrEmpty(beData.MobileNo) && beData.MobileNo.Length != 10)
                {
					resVal.ResponseMSG = "Invalid Mobile No. Please Enter 10 digit  Mobile No";
                }
				else if (!string.IsNullOrEmpty(beData.TelephoneNo) && beData.TelephoneNo.Length != 10)
				{
					resVal.ResponseMSG = "Invalid Telephone No. Please Enter 10 digit  Telephone No";
				}
				else if (!string.IsNullOrEmpty(beData.EmployeeContactNo) && beData.EmployeeContactNo.Length != 10)
				{
					resVal.ResponseMSG = "Invalid EmployeeContactNo Please Enter 10 digit.";
				}
				else if (!string.IsNullOrEmpty(beData.PanNo) && beData.PanNo.Length != 9)
				{
					resVal.ResponseMSG = "Invalid PAN. Please Enter 9 digit  PAN";
				}	
				else if(!beData.TypeofIdSubmittedId.HasValue || beData.TypeofIdSubmittedId.Value == 0)
                {
					resVal.ResponseMSG = "Please ! Select Valid Id Type";
                }
				else if (beData.Occupation == 0)
                {
					resVal.ResponseMSG = "Please ! Select Valid Occupation";
                }				
				else if(beData.KYCMethod==1 && string.IsNullOrEmpty(beData.Email))
                {
					resVal.ResponseMSG = "Please ! Enter Email Id";
                }
				else if (beData.KYCMethod == 1 && string.IsNullOrEmpty(beData.MobileNo))
				{
					resVal.ResponseMSG = "Please ! Enter Mobile No";
				}
				else if (beData.KYCMethod == 2 && (!beData.BankBranchId.HasValue || beData.BankBranchId==0))
				{
					resVal.ResponseMSG = "Please ! Select Bank Branch Name";
				}
				else
				{
                    if (beData.TypeofIdSubmittedId.HasValue)
                    {
                        if (!beData.IDIssueDate.HasValue)
                        {
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Enter Issue Date of ID";
							return resVal;
                        }else if (beData.DOB.Value > beData.IDIssueDate.Value)
                        {
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Enter Valid Issue Date";
							return resVal;
                        }
						else if (beData.IDIssueDate.Value > DateTime.Today)
                        {
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Enter Valid Issue Date";
							return resVal;
						}


						if(beData.TypeofIdSubmittedId.Value==3 || beData.TypeofIdSubmittedId.Value == 5)
                        {
                            if (!beData.IDExpiryDate.HasValue)
                            {
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Please ! Enter Expiry Date Of ID";
								return resVal;
                            }else if (beData.IDExpiryDate.Value < beData.IDIssueDate.Value)
                            {
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Please ! Enter Valid Expire Date Of ID";
								return resVal;
                            } 
                        }
                    }
					if (string.IsNullOrEmpty(beData.FirstName))
						beData.FirstName = "";

					if (string.IsNullOrEmpty(beData.MiddleName))
						beData.MiddleName = "";

					if (string.IsNullOrEmpty(beData.LastName))
						beData.LastName = "";

					beData.FirstName = beData.FirstName.Trim();
					beData.MiddleName = beData.MiddleName.Trim();
					beData.LastName = beData.LastName.Trim();

					if(beData.FirstName.Contains(" "))
                    {
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Remove Blank Space From First Name";
						return resVal;
                    }
					var charArr = beData.MiddleName.ToArray();
					int c = 0;
					foreach(var ch in charArr)
                    {
						if (ch == ' ')
							c++;
                    }					
					if (c>1)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Remove Blank Space From Middle Name";
						return resVal;
					}

					charArr = beData.LastName.ToArray();
					c = 0;
					foreach (var ch in charArr)
					{
						if (ch == ' ')
							c++;
					}				 

					if (c > 1)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Remove Blank Space From Last Name";
						return resVal;
					}

					if (beData.CA_WardNo == "0")
						beData.CA_WardNo = "";

					if (beData.PA_WardNo == "0")
						beData.PA_WardNo = "";

					if (beData.AnnualTransaction>=1000000000){
						resVal.ResponseMSG = "Please ! Visit Nerested Branch";
						return resVal;
					}

					var valid = beData.IsValid();
					if (valid.IsSuccess == false)
						return valid;

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

