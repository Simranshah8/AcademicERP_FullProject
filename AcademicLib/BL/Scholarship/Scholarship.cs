using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class Scholarship : AcademicLib.BL.CommonBL
	{
		AcademicLib.DA.Scholarship.ScholarshipDB db = null;

		int _UserId = 0;

		public Scholarship(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Scholarship.ScholarshipDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Scholarship.Scholarship beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Scholarship.ScholarshipCollections GetAllScholarship(int EntityId)
		{
			return db.getAllScholarship(_UserId, EntityId);
		}
		public BE.Scholarship.ScholarshipCollections getAllScholarship(TableFilter filter, int? StatusId, int? SubjectId,int? ClassId)
		{
			return db.getAllScholarship(filter,StatusId,SubjectId,ClassId);
		}
			public AcademicLib.BE.Scholarship.Scholarship GetScholarshipById(int EntityId, int TranId)
		{
			return db.getScholarshipById(_UserId, EntityId, TranId);
		}

		public AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections GetSchoolSubjectWise(int EntityId, int? SubjectId,int? ClassId)
		{
			return db.getSchoolSubjectwise(_UserId, EntityId, SubjectId,ClassId);
		}

		public AcademicLib.BE.Scholarship.GradeSheetCollections GetAllGradeSheet(int EntityId, string SEESymbolNo, string Alphabet, DateTime DOB_AD, string DOB_BS,double? GPA, string Pwd,int? ClassId)
		{
			return db.getAllGradeSheet(_UserId, EntityId, SEESymbolNo, Alphabet, DOB_AD,DOB_BS,GPA,Pwd,ClassId);
		}
		public ResponeValue CheckScholarshipApply(string SEESymbolNo, string Alphabet,int? ClassId)
		{
			return db.CheckScholarshipApply(_UserId, SEESymbolNo, Alphabet,ClassId);
		}
		public ResponeValue ResetPwd(string TranIdColl)
		{
			return db.ResetPwd(_UserId, TranIdColl);
		}
			public ResponeValues IsValidData(ref AcademicLib.BE.Scholarship.Scholarship beData, bool IsModify)
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
				else if (string.IsNullOrEmpty(beData.FirstName))
                {
					resVal.ResponseMSG = "Please ! Enter First Name";
                }
				else if(!beData.ClassId.HasValue || beData.ClassId == 0)
                {
					resVal.ResponseMSG = "Please ! Select Class For Scholarship";
                }
				else if (string.IsNullOrEmpty(beData.FirstName))
				{
					resVal.ResponseMSG = "Please ! Enter First Name ( पहिलो नाम )";
				}
				else if (string.IsNullOrEmpty(beData.LastName))
				{
					resVal.ResponseMSG = "Please ! Enter Last Name ( थर )";
				}
				else if (beData.Gender==0)
				{
					resVal.ResponseMSG = "Please ! Select Gender ( लिङ्ग )";
				}
				else if (!beData.DOB.HasValue || beData.DOB.Value.Year>2015)
				{
					resVal.ResponseMSG = "Invalid D.O.B. (जन्म मिति)";
				}
				else if (string.IsNullOrEmpty(beData.SEESymbolNo))
                {
					resVal.ResponseMSG = "Please ! Enter SEE 2080 Symbol No.";
                }
                else if (string.IsNullOrEmpty(beData.Alphabet) && beData.ClassId!=2)
                {
					resVal.ResponseMSG = "Please ! Enter Symbol No. Alphabet";
                }
				else if (string.IsNullOrEmpty(beData.MobileNo))
                {
					resVal.ResponseMSG = "Please ! Enter Mobile No.";
                }
				else if (string.IsNullOrEmpty(beData.F_FirstName))
				{
					resVal.ResponseMSG = "Please ! Enter Father First Name";
				}
				else if (string.IsNullOrEmpty(beData.F_LastName))
				{
					resVal.ResponseMSG = "Please ! Enter Father Last Name";
				}
				else if (string.IsNullOrEmpty(beData.M_FirstName))
				{
					resVal.ResponseMSG = "Please ! Enter Mother First Name";
				}
				else if (string.IsNullOrEmpty(beData.M_LastName))
				{
					resVal.ResponseMSG = "Please ! Enter Mother Last Name";
				}
				else if (string.IsNullOrEmpty(beData.GF_FirstName))
				{
					resVal.ResponseMSG = "Please ! Enter Grand Father First Name";
				}
				else if (string.IsNullOrEmpty(beData.GF_LastName))
				{
					resVal.ResponseMSG = "Please ! Enter  Grand Father Last Name";
				}
				 
				else if (string.IsNullOrEmpty(beData.P_Province))
				{
					resVal.ResponseMSG = "Please ! Select Permanent Province";
				}
				else if (string.IsNullOrEmpty(beData.P_District))
				{
					resVal.ResponseMSG = "Please ! Select Permanent District";
				}
				else if (string.IsNullOrEmpty(beData.P_LocalLevel))
				{
					resVal.ResponseMSG = "Please ! Select Permanent Local Level";
				}
				else if (!beData.GPA.HasValue)
                {
					resVal.ResponseMSG = "Please ! Enter GPA";
                }
				else if (beData.GPA.Value > 4)
                {
					resVal.ResponseMSG = "Please ! Enter GPA Less Than Or Equal 4";
                }
				else if (beData.GPA.Value <1.6)
				{
					resVal.ResponseMSG = "You Are Not Eligiable For Scholarship";
				}
				else if (string.IsNullOrEmpty(beData.PhotoPath))
                {
					resVal.ResponseMSG = "Please ! Upload Photo";
                }
				else if (string.IsNullOrEmpty(beData.SignaturePath))
				{
					resVal.ResponseMSG = "Please ! Upload Signature";
				}
				else if((!beData.AppliedSubjectId.HasValue || beData.AppliedSubjectId == 0) && beData.ClassId!=2)
                {
					resVal.ResponseMSG = "Please ! Select Applied Subject Name";
                }
				else if(beData.SchoolPriorityListColl==null || beData.SchoolPriorityListColl.Count == 0)
                {
					resVal.ResponseMSG = "Please ! Select School Priority From List";
                }
				else if(beData.BC_IssuedDate.HasValue && beData.BC_IssuedDate.Value > DateTime.Today)
                {
					resVal.ResponseMSG = "Please ! Birth/Citizenship Issue Date less than today ";
                }
				else if (beData.GovSchoolCerti_IssuedDate.HasValue && beData.GovSchoolCerti_IssuedDate.Value > DateTime.Today)
				{
					resVal.ResponseMSG = "Please ! School Certificate Issue Date less than today ";
				}
				//else if(!beData.Lat.HasValue || beData.Lat.Value == 0)
    //            {
				//	resVal.ResponseMSG = "Please ! Enable Geo Location 1st";
    //            }
				//else if (!beData.Lng.HasValue || beData.Lng.Value == 0)
				//{
				//	resVal.ResponseMSG = "Please ! Enable Geo Location 1st";
				//}
				else if (string.IsNullOrEmpty(beData.SchoolName))
                {
					resVal.ResponseMSG = "Please ! Enter School Name";
                }else if(!beData.SchoolTypeId.HasValue || beData.SchoolTypeId == 0)
                {
					resVal.ResponseMSG = "Please ! Select School Type";
                }
				else if(!beData.SchoolDistrictId.HasValue || beData.SchoolDistrictId == 0)
                {
					resVal.ResponseMSG = "Please ! Select School District";
                }
				else if (!beData.SchoolLocalLevelId.HasValue || beData.SchoolLocalLevelId == 0)
				{
					resVal.ResponseMSG = "Please ! Select School Local Level";
				}				
				else if (!beData.ScholarshipTypeId.HasValue || beData.ScholarshipTypeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Scholarship Type";
				}
				else
				{

					if (beData.ReservationGroupList == null)
						beData.ReservationGroupList = new BE.Scholarship.ReservationListCollections();

					if(beData.BC_IssuedDate.HasValue && beData.BC_IssuedDate > DateTime.Today)
                    {
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Birth Certificate/Citizenship Issue Date";
						return resVal;
                    }

					if (beData.PovCerti_IssuedDate.HasValue && beData.PovCerti_IssuedDate > DateTime.Today)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Poverty Certificate Issued Date";
						return resVal;
					}
					 
					if (beData.GovSchoolCerti_IssuedDate.HasValue && beData.GovSchoolCerti_IssuedDate > DateTime.Today)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Birth Certificate/Citizenship Issue Date";
						return resVal;
					}
					 
					if (beData.Certi_IssuedDate.HasValue && beData.Certi_IssuedDate > DateTime.Today)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Certificate Issued Date";
						return resVal;
					}

					if (beData.MigDoc_IssuedDate.HasValue && beData.MigDoc_IssuedDate > DateTime.Today)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Mig. Document Issued Date";
						return resVal;
					}

					if (beData.LandFill_IssuedDate.HasValue && beData.LandFill_IssuedDate > DateTime.Today)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Invalid Birth Land Fill Issued Date";
						return resVal;
					}
					  

					var vEmail = base.IsValidEmail (beData.Email);
					if(vEmail.IsSuccess==false)
                    {
						return vEmail;
                    }

					var vMobile = base.IsValidContactNo(beData.MobileNo);
					if (vMobile.IsSuccess == false)
					{
						return vMobile;
					}

                    if (beData.BC_DocumentNameId == 2)
                    {
                        if (string.IsNullOrEmpty(beData.BC_FilePath))
                        {
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Upoad Birth Certificate";
							return resVal;
                        }
                    }
					else if (beData.BC_DocumentNameId == 1)
					{
						if (string.IsNullOrEmpty(beData.CtznshipFront_FilePath))
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Upoad Citizenship Front Side ";
							return resVal;
						}

						if (string.IsNullOrEmpty(beData.CtznshipBack_FilePath))
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Upoad Citizenship Back Side ";
							return resVal;
						}
					}



					beData.GPA = Math.Round(beData.GPA.Value, 2);
					int row = 1;
					List<int> duplicateSIdColl = new List<int>();
					foreach(var sp in beData.SchoolPriorityListColl)
                    {
						if(sp.SchoolId.HasValue && sp.SchoolId.Value > 0)
                        {
                            if (duplicateSIdColl.Contains(sp.SchoolId.Value))
                            {
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Duplicate School Name at row "+row.ToString();
								return resVal;
                            }
                            else
                            {
								duplicateSIdColl.Add(sp.SchoolId.Value);
                            }
                        }
						row++;
                    }
					row = 1;

                    if (duplicateSIdColl.Count == 0)
                    {
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Please ! Select School Priority From List";
						return resVal;
					}

     //               if (beData.Gender == 2)
     //               {
					//	var findRG = beData.ReservationGroupList.Find(p1 => p1.ReservationGroupId == 1);
     //                   if (findRG == null)
     //                   {
					//		beData.ReservationGroupList.Add(new BE.Scholarship.ReservationList()
					//		{
					//			 ReservationGroupId=1,								  
					//		});
     //                   }
     //               }
     //               else
     //               {
					//	var findRG = beData.ReservationGroupList.Find(p1 => p1.ReservationGroupId == 1);
					//	if (findRG != null)
					//	{
					//		resVal.IsSuccess = false;
					//		resVal.ResponseMSG = "Female Reservation Group Not Allow For Male or Others";
					//		return resVal;
					//	}
					//}

					if(beData.ReservationGroupList!=null && beData.ReservationGroupList.Count > 0)
                    {
						foreach(var rs in beData.ReservationGroupList)
                        {
							if (!rs.ReservationGroupId.HasValue || rs.ReservationGroupId == 0)
							{
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Please ! Select Reservation Group Name";
								return resVal;
							}

							if (!rs.ConcernedAuthorityId.HasValue || rs.ConcernedAuthorityId == 0)
							{
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Please ! Select Reservation Concerned Authority";
								return resVal;
							}

							if (string.IsNullOrEmpty(rs.GroupWiseCerti_Path))
							{
								resVal.IsSuccess = false;
								resVal.ResponseMSG = "Please ! Upload Related Document of Reservation ";
								return resVal;
							}
							 
								

								//if(rs.ConcernedAuthorityId==1 || rs.ConcernedAuthorityId == 6)
        //                        {
								//	if(!rs.GrpCerti_IssuedDistrictId.HasValue || rs.GrpCerti_IssuedDistrictId == 0)
        //                            {
								//		resVal.IsSuccess = false;
								//		resVal.ResponseMSG = "Please ! Select District";
								//		return resVal;
        //                            }
        //                        }

								//if ( rs.ConcernedAuthorityId == 6)
								//{
								//	if (!rs.GrpCerti_IssuedLocalLevelId.HasValue || rs.GrpCerti_IssuedLocalLevelId == 0)
								//	{
								//		resVal.IsSuccess = false;
								//		resVal.ResponseMSG = "Please ! Select Local Level";
								//		return resVal;
								//	}
								//}

								//if (rs.ConcernedAuthorityId != 5)
								//{
								//	if (!rs.GroupWiseCerti_IssuedDate.HasValue || rs.GroupWiseCerti_IssuedDate>DateTime.Today)
								//	{
								//		resVal.IsSuccess = false;
								//		resVal.ResponseMSG = "Invalid Date";
								//		return resVal;
								//	}

									 
								//}

							 

							}
							 
                    }

     //               if (beData.SchoolTypeId == 1)
     //               {
					//	if(!beData.GovSchoolCerti_IssuedDate.HasValue || beData.GovSchoolCerti_IssuedDate > DateTime.Today)
     //                   {
					//		resVal.IsSuccess = false;
					//		resVal.ResponseMSG = "Please ! Select Government Certificate Issue Date.";
					//		return resVal;
     //                   }

					//	//if (string.IsNullOrEmpty(beData.GovSchoolCerti_RefNo))
					//	//{
					//	//	resVal.IsSuccess = false;
					//	//	resVal.ResponseMSG = "Please ! Enter Goverment Certificate Ref. No";
					//	//	return resVal;
					//	//}

					//	if (string.IsNullOrEmpty(beData.GovSchoolCertiPath))
					//	{
					//		resVal.IsSuccess = false;
					//		resVal.ResponseMSG = "Please ! Upload Goverment Certificate.";
					//		return resVal;
					//	}

					//	if (beData.ScholarshipTypeId == 6 || beData.ScholarshipTypeId == 7) 
					//	{
					//		resVal.IsSuccess = false;
					//		if (!beData.LandfilDistrictId.HasValue || beData.LandfilDistrictId==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site District";
					//			return resVal;
					//		}
					//		if (!beData.LandfillLocalLevelId.HasValue || beData.LandfillLocalLevelId==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site LocalLevel";
					//			return resVal;
					//		}
					//		if (!beData.LandfillWardNo.HasValue || beData.LandfillWardNo==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site WardNo";
					//			return resVal;
					//		}
					//		if (!beData.LandFill_IssuedDate.HasValue || beData.LandFill_IssuedDate>DateTime.Today) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site Certificate Issued Date";
					//			return resVal;
					//		}

					//		//if (string.IsNullOrEmpty(beData.LandFill_RefNo)) 
					//		//{
					//		//	resVal.ResponseMSG = "Please ! Select Landfill Site Certificate Reference No";
					//		//	return resVal;
					//		//}
					//		if (string.IsNullOrEmpty(beData.LandFillDocPath)) {
					//			resVal.ResponseMSG = "Please ! Upload Landfill Site Certificate";
					//			return resVal;
					//		}
					//	}
					//}



					//if (beData.SchoolTypeId == 2) {
					//	if (beData.ScholarshipTypeId == 3) {
					//		if (!beData.LandfilDistrictId.HasValue ||  beData.LandfilDistrictId==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site District";
					//			return resVal;
					//		}
					//		if (!beData.LandfillLocalLevelId.HasValue || beData.LandfillLocalLevelId==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site LocalLevel";
					//			return resVal;
					//		}
					//		if (!beData.LandfillWardNo.HasValue || beData.LandfillWardNo==0) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site WardNo";
					//			return resVal;
					//		}
					//		if (!beData.LandFill_IssuedDate.HasValue || beData.LandFill_IssuedDate>DateTime.Today) {
					//			resVal.ResponseMSG = "Please ! Select Landfill Site Certificate Issued Date";
					//			return resVal;
					//		}
					//		//if (string.IsNullOrEmpty(beData.LandFill_RefNo)) {
					//		//	resVal.ResponseMSG = "Please ! Select Landfill Site Certificate Reference No";
					//		//	return resVal;
					//		//}
					//		if (string.IsNullOrEmpty(beData.LandFillDocPath)) {
					//			resVal.ResponseMSG = "Please ! Upload Landfill Site Certificate";
					//			return resVal;
					//		}
					//	}
					//}



					if(beData.ScholarshipTypeId==2 || beData.ScholarshipTypeId == 3)
                    {
						if (!beData.MigDoc_IssuedDate.HasValue || beData.MigDoc_IssuedDate > DateTime.Today)
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Invalid Mig. Document Issued Date";
							return resVal;
						}
						if (string.IsNullOrEmpty(beData.MigDocPath))
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Upload Mig. Document ";
							return resVal;
						}
						if (!beData.Mig_WardId.HasValue || beData.Mig_WardId==0)
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Select Mig. Ward No. ";
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

