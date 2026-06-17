using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Board
    {
        DA.Academic.Creation.BoardDB db = null;
        int _UserId = 0;
        public Board(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.BoardDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Board beData)
        {
            bool isModify = beData.BoardId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.BoardCollections GetAllBoard(int EntityId)
        {
            return db.getAllBoard(_UserId, EntityId);
        }
        public BE.Academic.Creation.Board GetBoardById(int EntityId, int BoardId)
        {
            return db.getBoardById(_UserId, EntityId, BoardId);
        }
        public ResponeValues DeleteById(int EntityId, int BoardId)
        {
            return db.DeleteById(_UserId, EntityId, BoardId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Board beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BoardId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BoardId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Board Name";
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
