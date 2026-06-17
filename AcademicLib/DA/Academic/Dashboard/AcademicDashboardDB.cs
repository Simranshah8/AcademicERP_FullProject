using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Dashboard
{

	internal class AcademicDashBoardDB
	{
		DataAccessLayer1 dal = null;
		public AcademicDashBoardDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public BE.Academic.Dashboard.AcademicDashBoard GetAcademicDashboard(int UserId, int AcademicYearId, int? ClassShiftId,int? EntityId,int? BranchId)
		{
			BE.Academic.Dashboard.AcademicDashBoard beData = new BE.Academic.Dashboard.AcademicDashBoard();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BranchId", BranchId);
			cmd.CommandText = "usp_GetAcademicDashboard";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.Quotes = reader.GetString(0);
					if (!(reader[1] is DBNull)) beData.QuotesPhotoPath = reader.GetString(1);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalStudent = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TotalFemaleStudent = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.TotalMaleStudent = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.TotalMonthlyAdmissions = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.MonthlyMaleAdmissions = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.MonthlyFemaleAdmissions = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.TotalYearlyAdmissions = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.YearlyMaleAdmissions = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.YearlyFemaleAdmissions = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.TotalLeftStudent = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.LeftMaleStudent = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.LeftFemaleStudent = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.TotalPassoutStudent = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.PassoutMaleStudent = reader.GetInt32(13);
					if (!(reader[14] is DBNull)) beData.PassoutFemaleStudent = reader.GetInt32(14);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalEmployee = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TotalFemale = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.TotalMale = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.TotalNewJoining = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.MaleJoining = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.FemaleJoining = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.TotalLeftEmployee = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.LeftMaleEmp = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.LeftFemaleEmp = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.Teaching = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.MaleTeaching = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.FemaleTeaching = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.NotTeaching = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.MaleNotTeaching = reader.GetInt32(13);
					if (!(reader[14] is DBNull)) beData.FemaleNotTeaching = reader.GetInt32(14);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalSubject = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TotalECA = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.TotalNonECA = reader.GetInt32(2);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalStudentAcc = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Created = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.Remaining = reader.GetInt32(2);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalEmpAccount = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.CreatedEmp = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.RemainingEmp = reader.GetInt32(2);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalCertificate = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TransferCertificate = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.CharacterCertificate = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ExtraCertificate = reader.GetInt32(3);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalYearlyPTM = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TotalMonthlyPTM = reader.GetInt32(1);
					//if (!(reader[2] is DBNull)) beData.MonthlyCreatedPTM = reader.GetInt32(2);
					//if (!(reader[3] is DBNull)) beData.MonthlyRemainingPTM = reader.GetInt32(3);
					//if (!(reader[4] is DBNull)) beData.YearlyCreatedPTM = reader.GetInt32(4);
					//if (!(reader[5] is DBNull)) beData.YearlyRemaining = reader.GetInt32(5);
				}
				if (reader.NextResult())
				{
					beData.ClassScheduleColl = new BE.Academic.Dashboard.ClassScheduleCollections();

					while (reader.Read())
					{
						BE.Academic.Dashboard.ClassSchedule dataColl = new BE.Academic.Dashboard.ClassSchedule();
						if (!(reader[0] is DBNull)) dataColl.ClassId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.ClassName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.ShiftStartTime = reader.GetTimeSpan(2);
                        if (!(reader[3] is DBNull)) dataColl.ShiftEndTime = reader.GetTimeSpan(3);
						if (!(reader[4] is DBNull)) dataColl.StartTime = reader.GetTimeSpan(4);
                        if (!(reader[5] is DBNull)) dataColl.EndTime = reader.GetTimeSpan(5);
						if (!(reader[6] is DBNull)) dataColl.SubjectName = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.TeacherName = reader.GetString(7);
						beData.ClassScheduleColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.HouseColl = new BE.Academic.Dashboard.HouseCollections();

					while (reader.Read())
					{
						BE.Academic.Dashboard.House dataColl = new BE.Academic.Dashboard.House();
						if (!(reader[0] is DBNull)) dataColl.HouseNameId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.HouseName = reader.GetString(1);
						if (!(reader[2] is DBNull)) dataColl.TotalStudents = reader.GetInt32(2);
						if (!(reader[3] is DBNull)) dataColl.TotalBoys = reader.GetInt32(3);
						if (!(reader[4] is DBNull)) dataColl.TotalGirls = reader.GetInt32(4);
						if (!(reader[5] is DBNull)) dataColl.InchargeName = reader.GetString(5);
						if (!(reader[6] is DBNull)) dataColl.PhoneNo = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.ColorCode = reader.GetString(7);
						beData.HouseColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.ClassTeacherColl = new BE.Academic.Dashboard.ClassTeacherCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.ClassTeacher dataColl = new BE.Academic.Dashboard.ClassTeacher();
						if (!(reader[0] is DBNull)) dataColl.ClassTeacherId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.EmployeeId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) dataColl.EmployeeCode = reader.GetString(2);
						if (!(reader[3] is DBNull)) dataColl.TeacherName = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.Designation = reader.GetString(4);
						if (!(reader[5] is DBNull)) dataColl.PhotoPath = reader.GetString(5);
						if (!(reader[6] is DBNull)) dataColl.Gender = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.ClassName = reader.GetString(7);
						if (!(reader[8] is DBNull)) dataColl.SectionName = reader.GetString(8);
						if (!(reader[9] is DBNull)) dataColl.ContactNo = reader.GetString(9);
						if (!(reader[10] is DBNull)) dataColl.Address = reader.GetString(10);
						beData.ClassTeacherColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.StudentBirthdaysColl = new BE.Academic.Dashboard.StudentBirthdaysCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.StudentBirthdays dataColl = new BE.Academic.Dashboard.StudentBirthdays();
						if (!(reader[0] is DBNull)) dataColl.StudentId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.StudentName = reader.GetString(1);
						if (!(reader[2] is DBNull)) dataColl.StudentBdayPhoto = reader.GetString(2);
						if (!(reader[3] is DBNull)) dataColl.ClassName = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.SectionName = reader.GetString(4);
						if (!(reader[5] is DBNull)) dataColl.DOB_AD = Convert.ToDateTime(reader[5]);
						beData.StudentBirthdaysColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.EmployeeBirthdaysColl = new BE.Academic.Dashboard.EmployeeBirthdaysCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.EmployeeBirthdays dataColl = new BE.Academic.Dashboard.EmployeeBirthdays();
						if (!(reader[0] is DBNull)) dataColl.EmployeeId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.EmployeeName = reader.GetString(1);
						if (!(reader[2] is DBNull)) dataColl.EmpBdayPhoto = reader.GetString(2);
						if (!(reader[3] is DBNull)) dataColl.Department = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.DOB_AD = Convert.ToDateTime(reader[4]);
						beData.EmployeeBirthdaysColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.HODColl = new BE.Academic.Dashboard.HODCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.HOD dataColl = new BE.Academic.Dashboard.HOD();
						if (!(reader[0] is DBNull)) dataColl.HODId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.HODEmployeeId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) dataColl.HODCode = reader.GetString(2);
						if (!(reader[3] is DBNull)) dataColl.HODName = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.Designation_HOD = reader.GetString(4);
						if (!(reader[5] is DBNull)) dataColl.PhotoPath_HOD = reader.GetString(5);
						if (!(reader[6] is DBNull)) dataColl.Gender_HOD = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.ClassName_HOD = reader.GetString(7);
						if (!(reader[8] is DBNull)) dataColl.SectionName_HOD = reader.GetString(8);
						if (!(reader[9] is DBNull)) dataColl.ContactNo_HOD = reader.GetString(9);
						if (!(reader[10] is DBNull)) dataColl.Address_HOD = reader.GetString(10);
						if (!(reader[11] is DBNull)) dataColl.DepartmentId = reader.GetInt32(11);
						if (!(reader[12] is DBNull)) dataColl.DepartmentHOD = reader.GetString(12);
						if (!(reader[13] is DBNull)) dataColl.ClassShift = reader.GetString(13);
						beData.HODColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.StudentRemarksColl = new BE.Academic.Dashboard.StudentRemarksCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.StudentRemarks dataColl = new BE.Academic.Dashboard.StudentRemarks();
						if (!(reader[0] is DBNull)) dataColl.TranId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.StudentId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) dataColl.Point = Convert.ToDouble(reader[2]);
						if (!(reader[3] is DBNull)) dataColl.Remarks = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.FilePath = reader.GetString(4);
						if (!(reader[5] is DBNull)) dataColl.ClassName = reader.GetString(5);
						if (!(reader[6] is DBNull)) dataColl.SectionName = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.RegNo = reader.GetString(7);
						if (!(reader[8] is DBNull)) dataColl.RollNo = reader.GetInt32(8);
						if (!(reader[9] is DBNull)) dataColl.RemarksBy = reader.GetString(9);
						if (!(reader[10] is DBNull)) dataColl.StudentName = reader.GetString(10);
						if (!(reader[11] is DBNull)) dataColl.PhotoPath = reader.GetString(11);
						beData.StudentRemarksColl.Add(dataColl);
					}
				}
				if (reader.NextResult())
				{
					beData.EmployeeRemarksColl = new BE.Academic.Dashboard.EmployeeRemarksCollections();
					while (reader.Read())
					{
						BE.Academic.Dashboard.EmployeeRemarks dataColl = new BE.Academic.Dashboard.EmployeeRemarks();
						if (!(reader[0] is DBNull)) dataColl.TranId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) dataColl.EmployeeId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) dataColl.PointEmp = Convert.ToDouble(reader[2]);
						if (!(reader[3] is DBNull)) dataColl.Remarks = reader.GetString(3);
						if (!(reader[4] is DBNull)) dataColl.FilePathEmp = reader.GetString(4);
						if (!(reader[5] is DBNull)) dataColl.Designation = reader.GetString(5);
						if (!(reader[6] is DBNull)) dataColl.RemarksBy = reader.GetString(6);
						if (!(reader[7] is DBNull)) dataColl.EmployeeName = reader.GetString(7);
						if (!(reader[8] is DBNull)) dataColl.PhotoPathEmp = reader.GetString(8);
						beData.EmployeeRemarksColl.Add(dataColl);
					}
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalStudent = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.PhotoUpdated = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.Remaining = reader.GetInt32(2);
				}
				reader.NextResult();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.TotalEmployee = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.PhotoUpdatedEmp = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.EmpRemaining = reader.GetInt32(2);
				}


				reader.Close();
				beData.IsSuccess = true;
				beData.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				beData.IsSuccess = false;
				beData.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return beData;
		}


	}

}

