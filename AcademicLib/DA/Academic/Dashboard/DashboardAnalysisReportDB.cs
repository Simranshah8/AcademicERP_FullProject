using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Dashboard
{

	internal class DashboardAnalysisReportDB
	{
		DataAccessLayer1 dal = null;
		public DashboardAnalysisReportDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public BE.Academic.Dashboard.DashboardAnalysisReport GetAcademicAnalysisReport(int UserId, int AcademicYearId,  int? EntityId, int? BranchId)
		{
            BE.Academic.Dashboard.DashboardAnalysisReport beData = new BE.Academic.Dashboard.DashboardAnalysisReport();

            dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetDashboardAnalysisReport";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                beData.StdRecordColl = new BE.Academic.Dashboard.StudentRecordCollections();
                while (reader.Read())
                {
                    BE.Academic.Dashboard.StudentRecord dataColl = new BE.Academic.Dashboard.StudentRecord();
                    if (!(reader[0] is DBNull)) dataColl.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) dataColl.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) dataColl.TotalNewStudent = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) dataColl.TotalLeftStudent = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) dataColl.TotalPassoutStudent = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) dataColl.TotalOldStudent = reader.GetInt32(5);
                    beData.StdRecordColl.Add(dataColl);
                }
                if (reader.NextResult())
                {
                    beData.StdTpyDistColl = new BE.Academic.Dashboard.StudentTypDistributionCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.StudentTypDistribution dataColl = new BE.Academic.Dashboard.StudentTypDistribution();
                        if (!(reader[0] is DBNull)) dataColl.StudentTypeId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.StudentTypeName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.TotalSTBoys = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.TotalSTGirls = reader.GetInt32(3);
                        beData.StdTpyDistColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.StdGenderDistColl = new BE.Academic.Dashboard.StdGenderDistributionCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.StdGenderDistribution dataColl = new BE.Academic.Dashboard.StdGenderDistribution();
                        if (!(reader[0] is DBNull)) dataColl.StdGenderClassId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.ClassNameStdGender = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.StdGenderBoys = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.StdGenderGirls = reader.GetInt32(3);
                        beData.StdGenderDistColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.CasteStdColl = new BE.Academic.Dashboard.CasteStdCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.CasteStd dataColl = new BE.Academic.Dashboard.CasteStd();
                        if (!(reader[0] is DBNull)) dataColl.StdCasteId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.StdCasteName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.NoOfCasteStd = reader.GetInt32(2);
                        beData.CasteStdColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.AgeWiseStdColl = new BE.Academic.Dashboard.AgeWiseStdCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.AgeWiseStd dataColl = new BE.Academic.Dashboard.AgeWiseStd();
                        if (!(reader[0] is DBNull)) dataColl.StdAgeGp = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.StdTotalBoys = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.StdTotalGirls = reader.GetInt32(2);
                        beData.AgeWiseStdColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.StdDisabiltyColl = new BE.Academic.Dashboard.StdDisabiltyCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.StdDisabilty dataColl = new BE.Academic.Dashboard.StdDisabilty();
                        if (!(reader[0] is DBNull)) dataColl.StdPhysicalDisability = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.StdIsPhysicalDisability = Convert.ToBoolean(reader[1]);
                        if (!(reader[2] is DBNull)) dataColl.BoysWithDisability = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.GirlsWithDisability = reader.GetInt32(3);
                        beData.StdDisabiltyColl.Add(dataColl);
                    }
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.TotalEmployee = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TotalNewJoining = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TotalLeftEmployee = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Teaching = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.NotTeaching = reader.GetInt32(4);
                }
                if (reader.NextResult())
                {
                    beData.DepartmentWiseEmpColl = new BE.Academic.Dashboard.DepartmentWiseEmpCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.DepartmentWiseEmp dataColl = new BE.Academic.Dashboard.DepartmentWiseEmp();
                        if (!(reader[0] is DBNull)) dataColl.DepartmentId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.WiseEmpDepartment = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentMaleEmp = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.DepartmentFemaleEmp = reader.GetInt32(3);
                        beData.DepartmentWiseEmpColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.LevelColl = new BE.Academic.Dashboard.LevelCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.Level dataColl = new BE.Academic.Dashboard.Level();
                        if (!(reader[0] is DBNull)) dataColl.LevelId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.LevelName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.NoOfLevel = reader.GetInt32(2);
                        beData.LevelColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.CasteEmpColl = new BE.Academic.Dashboard.CasteEmpCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.CasteEmp dataColl = new BE.Academic.Dashboard.CasteEmp();
                        if (!(reader[0] is DBNull)) dataColl.EmpCasteId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.EmpCasteName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.NoOfCasteEmp = reader.GetInt32(2);
                        beData.CasteEmpColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.AgeWiseEmpColl = new BE.Academic.Dashboard.AgeWiseEmpCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.AgeWiseEmp dataColl = new BE.Academic.Dashboard.AgeWiseEmp();
                        if (!(reader[0] is DBNull)) dataColl.EmpAgeGp = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.EmpTotalMale = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.EmpTotalFemale = reader.GetInt32(2);
                        beData.AgeWiseEmpColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EmpDisabiltyColl = new BE.Academic.Dashboard.EmpDisabiltyCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.EmpDisabilty dataColl = new BE.Academic.Dashboard.EmpDisabilty();
                        if (!(reader[0] is DBNull)) dataColl.EmpPhysicalDisability = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.EmpIsPhysicalDisability = Convert.ToBoolean(reader[1]);
                        if (!(reader[2] is DBNull)) dataColl.MaleWithDisability = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.FemaleWithDisability = reader.GetInt32(3);
                        beData.EmpDisabiltyColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.ClassWiseStdEmpColl = new BE.Academic.Dashboard.ClassWiseStdEmpCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.ClassWiseStdEmp dataColl = new BE.Academic.Dashboard.ClassWiseStdEmp();
                        if (!(reader[0] is DBNull)) dataColl.StdEmpClassId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.StdEmpClassName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.TotalStdClass = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.TotalEmpClass = reader.GetInt32(3);
                        beData.ClassWiseStdEmpColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.BirthdaySummaryColl = new BE.Academic.Dashboard.BirthdaySummaryCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.BirthdaySummary dataColl = new BE.Academic.Dashboard.BirthdaySummary();
                        if (!(reader[0] is DBNull)) dataColl.BirthMonth = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.BDayMonthName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.TotalStdBDay = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.TotalEmpBDay = reader.GetInt32(3);
                        beData.BirthdaySummaryColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.RemarkStdEmpColl = new BE.Academic.Dashboard.RemarkStdEmpCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.RemarkStdEmp dataColl = new BE.Academic.Dashboard.RemarkStdEmp();
                        if (!(reader[0] is DBNull)) dataColl.StdEmpRemarkMonth = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.RemarkMonthName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.TotalStdRemark = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.TotalEmpRemark = reader.GetInt32(3);
                        beData.RemarkStdEmpColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.CretificateSummaryColl = new BE.Academic.Dashboard.CretificateSummaryCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.CretificateSummary dataColl = new BE.Academic.Dashboard.CretificateSummary();
                        if (!(reader[0] is DBNull)) dataColl.CertiMonthName = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.TrasnferCertificates = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.CharacterCertificates = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.ExtraCertificates = reader.GetInt32(3);
                        beData.CretificateSummaryColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.PTMDetailsColl = new BE.Academic.Dashboard.PTMDetailsCollections();

                    while (reader.Read())
                    {
                        BE.Academic.Dashboard.PTMDetails dataColl = new BE.Academic.Dashboard.PTMDetails();
                        if (!(reader[0] is DBNull)) dataColl.MeetingMonth = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) dataColl.MonthName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.TotalMeetings = reader.GetInt32(2);
                        beData.PTMDetailsColl.Add(dataColl);
                    }
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

