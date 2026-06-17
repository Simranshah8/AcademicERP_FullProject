using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.CRM
{
    public class Kyc 
    {
        public Kyc()
        {
            AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            ContactDetColl = new List<kycContactDetail>();
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int? CustomerId { get; set; }
        public int? AddressBookId { get; set; }
        public string CompanyCode { get; set; } = "";
        public int CompanyTypeId { get; set; }
        public int CompanyCategoryId { get; set; }
        public string CompanyName { get; set; } = "";
        public string BillingName { get; set; } = "";
        public string CompanyRegdNo { get; set; } = "";
        public int? CompanyPanNo { get; set; }
        public string CompanyContactNo { get; set; } = "";
        public string CompanyEmailId { get; set; } = "";
        public string CompanyStatus { get; set; } = "";
        public int? CountryId { get; set; }
        public int? BranchId { get; set; }
        public int? AgreementId { get; set; }
        public string ProvinceState { get; set; } = "";
        public string District { get; set; } = "";
        public string LocalLevel { get; set; } = "";
        public int? WardNo { get; set; }
        public string StreetName { get; set; } = "";
        public string FullAddress { get; set; } = "";
        public string Logopath { get; set; } = "";
        public byte[] Photo { get; set; }
        public string attachFile { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        public List<kycContactDetail> ContactDetColl { get; set; }        
        public string Panpath { get; set; }
        public string Taxpath { get; set; }
        public string Registrationpath { get; set; }
        public bool NeedToUpdate { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public DateTime? DomainExpiryDate { get; set; }
        public string OneSignalId { get; set; }
        public string OneSignalKey { get; set; }
        public string SMSApiPrimary { get; set; }
        public string SMSApiSeconday { get; set; }
        public string UrlName { get; set; }
        public string KYCUpdateBy { get; set; }
    }

 
    public class kycContactDetail
    {
        public int? CustomerId { get; set; }
        public string Name { get; set; }
        public int? DesignationId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string Remarks { get; set; }
        public string Designation { get; set; }
        public int? ContactPurposeId { get; set; }
        public int? ContactUserId { get; set; }
        public bool ContactStatus { get; set; }
        public string UserName { get; set; } = "";
    }

    public class KycCustomerAttachDoc
    {

        public int CustomerId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Extension { get; set; } = "";
        public byte[] Document { get; set; }
        public string DocPath { get; set; } = "";
    }
}
