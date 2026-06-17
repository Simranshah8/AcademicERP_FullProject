using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace AcademicLib.BL.Infirmary
{
    public class Disease
    {
        DA.Infirmary.DiseaseDB db = null;
        int _UserId = 0;
        public Disease(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infirmary.DiseaseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Infirmary.Disease disease)
        {
            ResponeValues isValid = new ResponeValues();
            isValid.IsSuccess = true; isValid.ResponseMSG = "";
            int idx = 0;

            /* 
             * get all valid diseases based on studentId
             * and create a mapping from existing DiseaseId => existing Disease Object
             * 
             * Assuming : All studentIds are same in a request
             */
            /*
            var existingDiseases = getAllDiseases();
            var existingDiseaseIdMapping = new Dictionary<int, BE.Infirmary.Disease>();

            foreach (var Disease in existingDiseases)
            {
                existingDiseaseIdMapping[Convert.ToInt32(Disease.DiseaseId)] = Disease;
            }
*/
            /*
             * if operation is update => DiseaseId will be present
             * otherwise its value is NULL
             * 
             * Step 1:
             *  - Check if Disease passed by request are valid
             * 
             *          
             * Step 3:
             *  - Check for validity of data
             */

            int curDiseaseId = Convert.ToInt32(disease.DiseaseId);

            BE.Infirmary.Disease existingDisease = getDiseaseById(curDiseaseId);

            // diseaseId is passed but not present in DB
            if (disease.DiseaseId!=null && !existingDisease.IsSuccess)
            {
                isValid.IsSuccess = false;
                isValid.ResponseMSG = "DiseaseId Passed is Invalid";
                return isValid;
            }

            // if disease already present then it will be used
            // as modifiedby and modifieddatetime
            disease.CUserId = _UserId;
            disease.LogDateTime = DateTime.Now;

            // if OrderNo not passed use previous value
            if(disease.DiseaseId != null && disease.OrderNo == null)
                disease.OrderNo = existingDisease.OrderNo;
            
            IsValidData(disease);
            return db.SaveDisease(disease, _UserId);

        }

        public BE.Infirmary.DiseaseSeverityCollections getHealthIssueSeverities() => db.getHealthIssueSeverities();

        public bool foundSimilarDisease(string name,int severity) => db.foundSimilarDisease(name,severity);

        public BE.Infirmary.DiseaseCollections getAllDiseases() => db.getAllDiseases();

        public ResponeValues IsValidData(BE.Infirmary.Disease data)
        {
            ResponeValues resVal = new ResponeValues();

            if (data.DiseaseName == null || data.DiseaseName == "")
                throw new Exception("For Insertion Pass Disease Name");

            if (data.DiseaseName.Length>254)
                throw new Exception("For Insertion  Max Length Disease Name 254");

            if (data.Description == null || data.Description == "")
                throw new Exception("For Insertion Pass Description");

            if (data.Description.Length > 254)
                throw new Exception("For Insertion Max Length Description 254");

            resVal.IsSuccess = true;
            resVal.ResponseMSG = "Valid";

            return resVal;
        }

        public BE.Infirmary.Disease getDiseaseById(int diseaseId) => db.getDiseaseById(diseaseId);
        public ResponeValues deleteDiseaseById(int diseaseId) => db.deleteDiseaseById(diseaseId, _UserId);

    }
}