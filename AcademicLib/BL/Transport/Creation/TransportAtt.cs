using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Transport.Creation
{

	public class TransportAtt
	{

		DA.Transport.Creation.TransportAttDB db = null;

		int _UserId = 0;

		public TransportAtt(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db =new DA.Transport.Creation.TransportAttDB(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Transport.Creation.TransportAttCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			foreach (var v in dataColl)
			{
				if (v.Attendance!=true && v.Attendance != false)
				{
					resVal.IsSuccess = false;
					resVal.ResponseMSG = "Please ! Select Pick Up ";
					return resVal;
				}
			}
			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}
		
		public BE.Transport.Creation.TransportAtt GetTransportAttById(int EntityId, DateTime ForDate)
		{
			return db.getTransportAttById(_UserId, EntityId, ForDate);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public BE.Transport.Creation.TransportAttCollections GetAllStudentTransportAtt(int EntityId, int VehicleId, int RouteId, DateTime ForDate, int AttendanceForId, int? AcademicYearId)
		{
			return db.getAllStudentTransportAtt(_UserId, EntityId, VehicleId, RouteId, ForDate, AttendanceForId, AcademicYearId);
		}
		public BE.Transport.Creation.TransportAttCollections GetAllTransportAttendanceList(DateTime? DateFrom, DateTime? DateTo)
		{
			return db.getAllTransportAttendanceList(_UserId, 0, DateFrom, DateTo);
		}
		//Add function by Prshant
		public BE.Transport.Creation.TransportAttCollections GetTransportAttDetail( int? EntityId, DateTime ForDate, int VehicleId, int RouteId)
		{
			return db.GetTransportAttDetail(_UserId, EntityId, ForDate, VehicleId, RouteId);
		}
		public ResponeValues IsValidData(ref BE.Transport.Creation.TransportAtt beData, bool IsModify)
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
                //else if (!beData.VehicleId.HasValue || beData.VehicleId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Vehicle";
                //}
                //else if (beData.AttendanceForId == 0 || !beData.AttendanceForId.HasValue)
                //{
                //	resVal.ResponseMSG = "Please ! Select AttendanceFor ";
                //}
                //else if (beData.VehicleRouteId == 0 || beData.VehicleRouteId.HasValue == false)
                //{
                //	resVal.ResponseMSG = "Please ! Select VehicleRoute ";
                //}
                //else if (beData.StudentID == 0 || beData.StudentID.HasValue == false)
                //{
                //	resVal.ResponseMSG = "Please ! Select StudentID ";
                //}
                //else if (beData.VehiclePointId == 0 || beData.VehiclePointId.HasValue == false)
                //{
                //	resVal.ResponseMSG = "Please ! Select VehiclePoint ";
                //}
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

