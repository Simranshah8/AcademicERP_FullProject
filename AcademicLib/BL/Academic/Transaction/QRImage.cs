using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class QRImage
    {
        DA.Academic.Transaction.QRImageDB db = null;
        int _UserId = 0;
        public QRImage(int UserId,string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.QRImageDB(dbName, hostName);
        }

        public ResponeValues SaveUpdate(BE.Academic.Transaction.QRImage beData)
        {
            bool isModify = beData.QrImageId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);

            if (isValid.IsSuccess) {
                return db.SaveUpdate(beData, isModify,0);
            }
            else
            {
                return isValid;
            }
        }

        public ResponeValues DeletQrById(int EntityId,int QrImageId)
        {
            return db.DeleteById(_UserId, EntityId, QrImageId);
        }

        public BE.Academic.Transaction.QRCollections getAllQrImages(int EntityId)
        {
            return db.GetAllQrImage(_UserId, EntityId);
        }

        public BE.Academic.Transaction.QRImage getQRById(int EntityId,int QRId)
        {
            return db.GetQrImageById(_UserId, EntityId, QRId);
        }
        private ResponeValues IsValidData(ref BE.Academic.Transaction.QRImage beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.QrImageId==0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.QrImageId>0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
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
