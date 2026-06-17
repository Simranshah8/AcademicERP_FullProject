using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Transport.Creation
{
    public class Vehicle :  ResponeValues
    {
        public Vehicle()
        {
            AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public int SeatCapacity { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string FuelEngineType { get; set; }
        public DateTime? NamsariDate { get; set; }
        public DateTime? RenewalDate { get; set; }

        public string NamsariDate_BS { get; set; }
        public string RenewalDate_BS { get; set; }
        public int MFGYear { get; set; }
        public string JachpassNo { get; set; }
        public DateTime? JPValidityFrom { get; set; }
        public DateTime? JPValidityTo { get; set; }

        public string JPValidityFrom_BS { get; set; }
        public string JPValidityTo_BS { get; set; }
        public string JPRemarks { get; set; }
        public string InsuranceNo { get; set; }
        public DateTime? IValidityFrom { get; set; }
        public DateTime? IValidityTo { get; set; }

        public string IValidityFrom_BS { get; set; }
        public string IValidityTo_BS { get; set; }

        public string IRemarks { get; set; }
        public int? InChargeId { get; set; }
        public int? DriverId { get; set; }
        public int? ConductorId { get; set; }
        public string GPSDevice { get; set; }
        public string ImagePath { get; set; }

        public string InCharge { get; set; }
        public string Driver { get; set; }
        public string Conductor { get; set; }

        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        //Added By Suresh on 13 Falgun

        public string NamsariMiti { get; set; }
        public string RenewalMiti { get; set; }
        public string JPValidFromMiti { get; set; }
        public string JPValidToMiti { get; set; }
        public string IValidityFromMiti { get; set; }
        public string IValidityToMiti { get; set; }


        public string Url { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
        public string Authentication { get; set; }
        public string Token { get; set; }


    }
    public class VehicleCollections : List<Vehicle> {
        public VehicleCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
