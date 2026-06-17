using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class ScholarshipDocVerify
	{

		 DA.Scholarship.ScholarshipDocVerifyDB db = null;

		int _UserId = 0;

		public ScholarshipDocVerify(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.ScholarshipDocVerifyDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Scholarship.ScholarshipDocVerify beData)
		{
			bool isModify = beData.VerifyId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Scholarship.ScholarshipDocVerifyCollections GetAllScholarshipDocVerify(int EntityId)
		{
			return db.getAllScholarshipDocVerify(_UserId, EntityId);
		}
		public BE.Scholarship.ScholarshipDocVerify GetScholarshipDocVerifyById(int EntityId, int TranId)
		{
			return db.getScholarshipDocVerifyById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int VerifyId)
		{
			return db.DeleteById(_UserId, EntityId, VerifyId);
		}
		public ResponeValues IsValidData(ref BE.Scholarship.ScholarshipDocVerify beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.VerifyId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.VerifyId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.TranId == 0 || beData.TranId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select VerifyId ";
				}
				else
				{
					 
                    if (beData.V_Status == 2)
                    {
                        if (beData.V_Photo)
                        {
                            resVal.ResponseMSG="unverified फोटो";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Signature)
                        {
                            resVal.ResponseMSG="unverified हस्ताक्षर";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Document)
                        {
                            resVal.ResponseMSG="unverified जन्मदर्ता प्रमाणपत्र वा नागरिकता कागजात";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_CandidateName)
                        {
                            resVal.ResponseMSG="unverified  आवेदकको नाम";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Gender)
                        {
                            resVal.ResponseMSG="unverified  लिङ्ग";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_DOB)
                        {
                            resVal.ResponseMSG="unverified  जन्म मिति";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_FatherName)
                        {
                            resVal.ResponseMSG="unverified  बुवाको नाम";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_MotherName)
                        {
                            resVal.ResponseMSG="unverified  आमाको नाम";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_GrandfatherName)
                        {
                            resVal.ResponseMSG="unverified  हजुरबुबाको नाम";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Email)
                        {
                            resVal.ResponseMSG="unverified  इमेल";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_MobileNo)
                        {
                            resVal.ResponseMSG="unverified  मोबाइल नम्बर";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_PAddress)
                        {
                            resVal.ResponseMSG="unverified  स्थायी ठेगाना";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_TempAddress)
                        {
                            resVal.ResponseMSG="unverified  अस्थायी ठेगाना";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_BCCNo)
                        {
                            resVal.ResponseMSG="unverified  जन्मदर्ता प्रमाणपत्र वा नागरिकता नं";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_BCCIssuedDate)
                        {
                            resVal.ResponseMSG="unverified  जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी मिति";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_BCCIssuedDistrict)
                        {
                            resVal.ResponseMSG="unverified  जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी जिल्ला";
                            resVal.IsSuccess = false;
                            return resVal;
                        }

                        else if (beData.V_BCCIssuedLocalLevel)
                        {
                            resVal.ResponseMSG="unverified  जन्मदर्ता प्रमाणपत्र  जारी स्थानीय तह";
                            resVal.IsSuccess = false;
                            return resVal;
                        }


                        else if (beData.V_SchoolName)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयको नाम";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_SchoolType)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयको प्रकार";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_SchoolDistrict)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयको जिल्ला";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_SchoolLocalLevel)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयको स्थानीय तह";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Character_Transfer_Certi)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_Character_Transfer_CertiDate)
                        {
                            resVal.ResponseMSG="unverified  विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र जारी मिति";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        else if (beData.V_ScholarshipType)
                        {
                            resVal.ResponseMSG="unverified  छात्रवृत्तिको प्रकार";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                        //else if (beData.V_GovSchoolCertiPath)
                        //{
                        //    resVal.ResponseMSG="unverified  शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_GovSchoolCertiMiti)
                        //{
                        //    resVal.ResponseMSG="unverified  शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र जारी मिति ";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_GovSchoolCerti_RefNo)
                        //{
                        //    resVal.ResponseMSG="unverified  शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र चलानी नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_Anusuchi3DocPath)
                        //{
                        //    resVal.ResponseMSG="unverified  संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_Anusuchi3Doc_IssuedMiti)
                        //{
                        //    resVal.ResponseMSG="unverified संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र जारी मिति";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_Anusuchi3Doc_RefNo)
                        //{
                        //    resVal.ResponseMSG="unverified  संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र चलानी नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_MigDocPath)
                        //{
                        //    resVal.ResponseMSG="unverified  काठमाडौं महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट काठमाडौं महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र ";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_Mig_WardId)
                        //{
                        //    resVal.ResponseMSG="unverified  काठमाडौं महानगरपालिकाको सम्बन्धित वडा नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_MigDoc_IssuedMiti)
                        //{
                        //    resVal.ResponseMSG="unverified काठमाडौं महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट काठमाडौं महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र जारी मिति";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_MigDoc_RefNo)
                        //{
                        //    resVal.ResponseMSG="unverified  काठमाडौं महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट काठमाडौं महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र चलानी नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandFillDocPath)
                        //{
                        //    resVal.ResponseMSG="unverified  ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandfilDistrict)
                        //{
                        //    resVal.ResponseMSG="unverified  ल्याण्डफिल्ड साइड प्रभावित जिल्ला";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandfillLocalLevel)
                        //{
                        //    resVal.ResponseMSG="unverified  ल्याण्डफिल्ड साइड प्रभावित स्थानीय तह";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandfillWardNo)
                        //{
                        //    resVal.ResponseMSG="unverified  ल्याण्डफिल्ड साइड वडा नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandfillDoc_IssuedMiti)
                        //{
                        //    resVal.ResponseMSG="unverified  ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र जारी मिति";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}
                        //else if (beData.V_LandFill_RefNo)
                        //{
                        //    resVal.ResponseMSG="unverified ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र चलानी नं";
                        //    resVal.IsSuccess = false;
                        //    return resVal;
                        //}


                        if (beData.ReservationGroupList != null)
                        {
                            foreach (var v in beData.ReservationGroupList)
                            {
                                string gname = "";
                                string aname = "";
                                var findRS = beData.ScholarshipDet.ReservationGroupList.Find(p1 => p1.ReservationGroupId == v.ReservationGroupId);
                                if (findRS != null)
                                {
                                    gname = findRS.ReservationGroupName;
                                    aname = findRS.ConcernedAuthorityName;
                                }

                                if (v.V_ConcernedAuthorityId)
                                {
                                    // resVal.ResponseMSG="unverified  " आरक्षण समूहको सरोकारवाला निकाय";
                                    resVal.ResponseMSG="unverified "+  gname + " " + aname;
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }

                                else if (v.V_GroupWiseCertiMiti)
                                {
                                    resVal.ResponseMSG= "unverified " + gname + " समूह सिफारिसपत्र जारी मिति";
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }

                                else if (v.V_GroupWiseCerti_Path)
                                {
                                    resVal.ResponseMSG= "unverified " + gname + " समूह सिफारिसपत्र";
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }

                                else if (v.V_GroupWiseCerti_RefNo)
                                {
                                    resVal.ResponseMSG= "unverified " + gname + " समूह सिफारिसपत्र चलानी नं";
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }

                                else if (v.V_GrpCerti_IssuedDistrict)
                                {
                                    resVal.ResponseMSG= "unverified " + gname + " समूह सिफारिसपत्र जारी जिल्ला";
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }

                                else if (v.V_ISSUED_LOCALLEVEL)
                                {
                                    resVal.ResponseMSG= "unverified " + gname + " समूह सिफारिसपत्र जारी स्थानीय तह";
                                    resVal.IsSuccess = false;
                                    return resVal;
                                }
                            }
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

