using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Academic.Transaction
{
	public class NewBankAccount : ResponeValues
	{

		public int? BankId { get; set; }
		public int? ForUserId { get; set; }
		public int? StudentId { get; set; }
		public int? EmployeeId { get; set; }
		public int Saluation { get; set; } = 4;
		public string FirstName { get; set; } = "";
		public string MiddleName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string Gender { get; set; } = "";
		public int Nationality { get; set; } =1;
		public string OtherNationality { get; set; } = "";
		public DateTime? DOB { get; set; }
		public int? Education { get; set; }
		public int MaritalStatusId { get; set; }
		public string CitizenshipNumber { get; set; } = "";
		public int? TypeofIdSubmittedId { get; set; }
		public string IdNo { get; set; } = "";
		public DateTime? IDIssueDate { get; set; }
		public int IdIssuePlace { get; set; } 
		public DateTime? IDExpiryDate { get; set; }
		public string MobileNo { get; set; } = "";
		public string TelephoneNo { get; set; } = "";
		public string Email { get; set; } = "";
		public string CA_HouseNo { get; set; } = "";
		public string CA_WardNo { get; set; } = "";
		public string CA_StreetName { get; set; } = "";
		public int CA_ProvinceId { get; set; }
		public int CA_DistrictId { get; set; }
		public int CA_LocalLevelId { get; set; }
        public int CA_CountryId { get; set; } = 154;
		public string PA_HouseNo { get; set; } = "";
		public string PA_WardNo { get; set; } = "";
		public string PA_StreetName { get; set; } = "";
		public int PA_ProvinceId { get; set; }
		public int PA_DistrictId { get; set; }
		public int PA_LocalLevelId { get; set; }
		public int PA_CountryId { get; set; } = 154;
        public int Occupation { get; set; } 
		public string OtherOccupation { get; set; } = "";
		public string PanNo { get; set; } = "";
		public string OrganizationName { get; set; } = "";
		public string Designation { get; set; } = "";
		public string EmployeeContactNo { get; set; } = "";
		public double AnnualIncome { get; set; }
		public int SourceOfIncomeId { get; set; }
		public int PurposeofAccount { get; set; }=1 ;
		public string OtherPurposeofAcc { get; set; } = "";
		public double AnnualTransaction { get; set; }
		public string SpouseName { get; set; } = "";
		public string FatherName { get; set; } = "";
		public string MotherName { get; set; } = "";
		public string GrandfatherName { get; set; } = "";
		public string GrandmotherName { get; set; } = "";
		public string SonName { get; set; } = "";
		public string DaughterName { get; set; } = "";
		public string DaughterInLawName { get; set; } = "";
		public string FatherInLawName { get; set; } = "";
		public string NomineeName { get; set; } = "";
		public int? NomineeRelationId { get; set; }
		public int? NomineeIdentificationDocumentId { get; set; }
		public string NomineeIdNo { get; set; } = "";
		public string NomineeFather { get; set; } = "";
		public string NomineeMother { get; set; } = "";
		public bool InstantDebitCard { get; set; }
		public bool NameEmbossedDebitCard { get; set; }
		public bool MobBankingInquiryOnly { get; set; }
		public bool MobBankingInqAndTranBoth { get; set; }
		public bool InternetBankingInquiryOnly { get; set; }
		public bool InternetBankingInqAndTranBoth { get; set; }
		public string PhotoPath { get; set; } = "";
		public string SPhotoPath { get; set; } = "";

		public string attachFile { get; set; }
		public byte[] Photo { get; set; }
		public string UserId { get; set; } = "";

        public string InstanctCard { get; set; } = "E";
        public int MobileBanking { get; set; } = 10;
        public int InternetBanking { get; set; } = 2;

        public int EmployeeType { get; set; }
        public string OtherSourceOfIncome { get; set; } = "";

        public int KYCMethod { get; set; } = 1;
        public int? BankBranchId { get; set; } = null;

        public int BankSNo { get; set; }
        public string BankBranchName { get; set; }
        public NewBankAccount()
		{
			AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }

        public ResponeValues IsValid()
        {
            ResponeValues resVal = new ResponeValues();

            if (string.IsNullOrEmpty(FirstName))
            {
                resVal.ResponseMSG = "First Name Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                resVal.ResponseMSG = "LastName Name Missing";
                return resVal;
            }
 

            if (!DOB.HasValue)
            {
                resVal.ResponseMSG = "DOB Missing";
                return resVal;
            }
 

            //if (OccupationId == 0)
            //{
            //    resVal.ResponseMSG = "Occupation Missing";
            //    return resVal;
            //}

            if (string.IsNullOrEmpty(Gender))
            {
                resVal.ResponseMSG = "Gender Missing";
                return resVal;
            }

            if (MaritalStatusId==0)
            {
                resVal.ResponseMSG = "MartialStatus Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(IdNo))
            {
                resVal.ResponseMSG = "Id No Missing";
                return resVal;
            }

            if (!IDIssueDate.HasValue)
            {
                resVal.ResponseMSG = "Id Issue Date Missing";
                return resVal;
            }

            //if (IdIssuePlace == 0)
            //{
            //    resVal.ResponseMSG = "Id Issue Place Missing";
            //    return resVal;
            //}

            if (string.IsNullOrEmpty(CA_WardNo))
            {
                resVal.ResponseMSG = "C WardNo Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(CA_StreetName))
            {
                resVal.ResponseMSG = "C Street Name Missing";
                return resVal;
            }

            //if (CLocalBody == 0)
            //{
            //    resVal.ResponseMSG = "C Local Body Missing";
            //    return resVal;
            //}

            if (CA_DistrictId == 0)
            {
                resVal.ResponseMSG = "C District Missing";
                return resVal;
            }

            if (CA_ProvinceId == 0)
            {
                resVal.ResponseMSG = "C Province Missing";
                return resVal;
            }

            if (CA_CountryId == 0)
            {
                resVal.ResponseMSG = "C Country Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(PA_WardNo))
            {
                resVal.ResponseMSG = "P WardNo Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(PA_StreetName))
            {
                resVal.ResponseMSG = "P Street Name Missing";
                return resVal;
            }

            //if (PLocalBody == 0)
            //{
            //    resVal.ResponseMSG = "P Local Body Missing";
            //    return resVal;
            //}

            if (PA_DistrictId == 0)
            {
                resVal.ResponseMSG = "P District Missing";
                return resVal;
            }

            if (PA_ProvinceId == 0)
            {
                resVal.ResponseMSG = "P Province Missing";
                return resVal;
            }

            if (PA_CountryId == 0)
            {
                resVal.ResponseMSG = "P Country Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(Email))
            {
                resVal.ResponseMSG = "Email Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(FatherName))
            {
                resVal.ResponseMSG = "Father Name Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(MotherName))
            {
                resVal.ResponseMSG = "Mother Name Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(GrandfatherName))
            {
                resVal.ResponseMSG = "GrandFather Name Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(GrandmotherName))
            {
                resVal.ResponseMSG = "GrandMother Name Missing";
                return resVal;
            }

            if (string.IsNullOrEmpty(MobileNo))
            {
                resVal.ResponseMSG = "Mobile No Missing";
                return resVal;
            }

            if (Occupation==0)
            {
                resVal.ResponseMSG = "Occupation  Missing";
                return resVal;
            }

            if (AnnualIncome <= 0)
            {
                resVal.ResponseMSG = "Annual Income Amount missing";
                return resVal;
            }

            //if (string.IsNullOrEmpty(SourceOfIncomeId))
            //{
            //    resVal.ResponseMSG = "Source Of Income  Missing";
            //    return resVal;
            //}

            if (string.IsNullOrEmpty(CitizenshipNumber))
            {
                resVal.ResponseMSG = "Citizenship Number  Missing";
                return resVal;
            }

            //if (EstAnnTraninAmt <= 0)
            //{
            //    resVal.ResponseMSG = "EstAnnTranin Amount missing";
            //    return resVal;
            //}

            //if (string.IsNullOrEmpty(PurposeOfAccount))
            //{
            //    resVal.ResponseMSG = "Purpose Of Account  Missing";
            //    return resVal;
            //}


            resVal.IsSuccess = true;
            resVal.ResponseMSG = "Valid";
            return resVal;
        }
    }
	public class NewBankAccountAttDoc
	{

		public int BankId { get; set; }
		public int? DocumentTypeId { get; set; }
		public string Name { get; set; } = "";
		public string docDescription { get; set; } = "";
		public string Extension { get; set; } = "";
		public byte[] Document { get; set; }
		public string DocPath { get; set; } = "";
	}

	public class NewBankAccountAttDocCollections : System.Collections.Generic.List<NewBankAccountAttDoc>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class NewBankAccountCollections : System.Collections.Generic.List<NewBankAccount>
	{
		public NewBankAccountCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

