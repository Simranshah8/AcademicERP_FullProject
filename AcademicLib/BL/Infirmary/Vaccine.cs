using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Dynamic.BusinessEntity.Security;

namespace AcademicLib.BL.Infirmary
{
    public class Vaccine
    {
        //DA.Infirmary.VaccineDB db = null;
        //int _UserId = 0;
        //string _hostName;
        //string _dbName;
        //public Vaccine(int UserId, string hostName, string dbName)
        //{
        //    this._UserId = UserId;
        //    this._hostName = hostName;
        //    this._dbName = dbName;
        //    db = new DA.Infirmary.VaccineDB(hostName, dbName);
        //}
        //public ResponeValues SaveFormData( BE.Infirmary.Vaccine vaccine)
        //{
        //    ResponeValues isValid = new ResponeValues();
        //    isValid.IsSuccess = true; isValid.ResponseMSG = "";
        //    int idx = 0;

        //    /* 
        //     * get all valid vaccines based on studentId
        //     * and create a mapping from existing VaccineId => existing Vaccine Object
        //     * 
        //     * Assuming : All studentIds are same in a request
        //     */
            
        //    var existingVaccines = getAllVaccines();
        //    var existingVaccineIdMapping = new Dictionary<int, BE.Infirmary.Vaccine>();

        //    foreach (var Vaccine in existingVaccines)
        //    {
        //        existingVaccineIdMapping[Convert.ToInt32(Vaccine.VaccineId)] = Vaccine;
        //    }

        //    /*
        //     * if operation is update => VaccineId will be present
        //     * otherwise its value is NULL
        //     * 
        //     * Step 1:
        //     *  - Check if all Vaccines passed by request are valid
        //     *  
        //     * Step 2:
        //     *  - if VaccineId is present :
        //     *      => Update exisiting Fields with newOnes,
        //     *      => if no info is present in new one use existing, 
        //     *          
        //     * Step 3:
        //     *  - Check for validity of data
        //     */

        //    int curVaccineId = Convert.ToInt32(vaccine.VaccineId);

        //    BE.Infirmary.Vaccine existingVaccine = getVaccineById(curVaccineId);

        //    // vaccineId is passed but not present in DB
        //    if (vaccine.VaccineId != null && !existingVaccine.IsSuccess)
        //    {
        //        isValid.IsSuccess = false;
        //        isValid.ResponseMSG = "VaccineId Passed is Invalid";
        //        return isValid;
        //    }

        //    // if vaccine already present then it will be used
        //    // as modifiedby and modifieddatetime
        //    vaccine.CUserId = _UserId;
        //    vaccine.LogDateTime = DateTime.Now;

        //    // if OrderNo not passed use previous value
        //    if (vaccine.VaccineId != null && vaccine.OrderNo == null)
        //        vaccine.OrderNo = existingVaccine.OrderNo;

        //    int diseaseId = vaccine.VaccineFor;

        //    int curDiseaseId = Convert.ToInt32(diseaseId);

        //    BE.Infirmary.Disease existingDisease = new DA.Infirmary.DiseaseDB(_hostName, _dbName).getDiseaseById(diseaseId); ;

        //    if(!existingDisease.IsSuccess)
        //        throw new Exception("VaccineFor Field is Invalid");

        //    IsValidData(vaccine);
        //    return db.SaveVaccine(vaccine, _UserId);

        //}
        //public bool foundSimilarVaccine(string name) => db.foundSimilarVaccine(name);

        //public BE.Infirmary.VaccineCollections getAllVaccines() => db.getAllVaccines();
        //public ResponeValues IsValidData(BE.Infirmary.Vaccine data)
        //{
        //    ResponeValues resVal = new ResponeValues();

        //    if (data.VaccineName == null || data.VaccineName == "")
        //        throw new Exception("For Insertion: Pass Vaccine Name");

        //    if (data.VaccineName.Length > 254)
        //        throw new Exception("For Insertion: Max Length for Vaccine Name is 254");


        //    if (data.Brand == null || data.Brand == "")
        //        throw new Exception("For Insertion: Pass Brand");

        //    if (data.Brand.Length > 254)
        //        throw new Exception("For Insertion: Max Length for Brand is 254");

        //    if (data.Description == null || data.Description == "")
        //        throw new Exception("For Insertion: Pass Description");

        //    if (data.Description.Length > 254)
        //        throw new Exception("For Insertion: Max Length for Description is 254");

        //    resVal.IsSuccess = true;
        //    resVal.ResponseMSG = "Valid";

        //    return resVal;
        //}

        //public BE.Infirmary.Vaccine getVaccineById(int vaccineId) => db.getVaccineById(vaccineId);
        //public ResponeValues deleteVaccineById(int vaccineId) => db.deleteVaccineById(vaccineId, _UserId);

    }
}