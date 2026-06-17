using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Report
{
    internal class StudentDetailsDB
    {
        DataAccessLayer1 dal = null;
        public StudentDetailsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicLib.RE.Report.StudentDetails PrintStudentInfo(int UserId, int EntityId, int StudentId)
        {
            AcademicLib.RE.Report.StudentDetails beData = new AcademicLib.RE.Report.StudentDetails();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_PrintStudentInfo";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                     beData = new AcademicLib.RE.Report.StudentDetails();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SystemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AdmitDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.StudentName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StudentNameNP = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.DOB_AD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.DOB_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Religion = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.isEDJ = Convert.ToBoolean(reader[10]);
                    if (!(reader[11] is DBNull)) beData.EDJ = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Caste = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Nationality = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BloodGroup = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ContactNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Email = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.MotherTongue = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Height = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Weigth = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.IsPhysicalDisability = Convert.ToBoolean(reader[20]);
                    if (!(reader[21] is DBNull)) beData.PhysicalDisability = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Aim = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.BirthCertificateNo = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CitizenshipNo = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.NIDNo = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.EnquiryNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.ReferralCode = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.Remarks = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.PhotoPath = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.SignaturePath = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.ClassName = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.FacultyName = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.LevelName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.SemesterName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.BatchName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.SectionName = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.RollNo = reader.GetInt32(37);
                    if (!(reader[38] is DBNull)) beData.AcademicYear = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.IsNewStudent = Convert.ToBoolean(reader[39]);
                    if (!(reader[40] is DBNull)) beData.StudentType = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.Medium = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.HouseName = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.HouseDress = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.TransportPoint = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.BoardersName = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.BoardName = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.BoardRegNo = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.EnrollNo = reader.GetInt32(48);
                    if (!(reader[49] is DBNull)) beData.CardNo = reader.GetInt64(49);
                    if (!(reader[50] is DBNull)) beData.EMSId = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.LedgerPanaNo = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.FirstofClass = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.FatherName = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.F_Profession = reader.GetString(54);
                    if (!(reader[55] is DBNull)) beData.F_ContactNo = reader.GetString(55);
                    if (!(reader[56] is DBNull)) beData.F_Email = reader.GetString(56);
                    if (!(reader[57] is DBNull)) beData.F_AnnualIncome = Convert.ToDouble(reader[57]);
                    if (!(reader[58] is DBNull)) beData.MotherName = reader.GetString(58);
                    if (!(reader[59] is DBNull)) beData.M_Profession = reader.GetString(59);
                    if (!(reader[60] is DBNull)) beData.M_Contact = reader.GetString(60);
                    if (!(reader[61] is DBNull)) beData.M_Email = reader.GetString(61);
                    if (!(reader[62] is DBNull)) beData.M_AnnualIncome = Convert.ToDouble(reader[62]);
                    if (!(reader[63] is DBNull)) beData.FatherPhotoPath = reader.GetString(63);
                    if (!(reader[64] is DBNull)) beData.MotherPhotoPath = reader.GetString(64);
                    if (!(reader[65] is DBNull)) beData.IfGurandianIs = reader.GetInt32(65);
                    if (!(reader[66] is DBNull)) beData.GuardianName = reader.GetString(66);
                    if (!(reader[67] is DBNull)) beData.G_Relation = reader.GetString(67);
                    if (!(reader[68] is DBNull)) beData.G_Profesion = reader.GetString(68);
                    if (!(reader[69] is DBNull)) beData.G_ContactNo = reader.GetString(69);
                    if (!(reader[70] is DBNull)) beData.G_Email = reader.GetString(70);
                    if (!(reader[71] is DBNull)) beData.G_Address = reader.GetString(71);
                    if (!(reader[72] is DBNull)) beData.GuardianPhotoPath = reader.GetString(72);
                    if (!(reader[73] is DBNull)) beData.DonorName = reader.GetString(73);
                    if (!(reader[74] is DBNull)) beData.PA_FullAddress = reader.GetString(74);
                    if (!(reader[75] is DBNull)) beData.PA_Province = reader.GetString(75);
                    if (!(reader[76] is DBNull)) beData.PA_District = reader.GetString(76);
                    if (!(reader[77] is DBNull)) beData.PA_LocalLevel = reader.GetString(77);
                    if (!(reader[78] is DBNull)) beData.PA_WardNo = reader.GetInt32(78);
                    if (!(reader[79] is DBNull)) beData.PA_Village = reader.GetString(79);
                    if (!(reader[80] is DBNull)) beData.PA_HouseNo = reader.GetString(80);
                    if (!(reader[81] is DBNull)) beData.CA_Country = reader.GetString(81);
                    if (!(reader[82] is DBNull)) beData.CA_FullAddress = reader.GetString(82);
                    if (!(reader[83] is DBNull)) beData.CA_Province = reader.GetString(83);
                    if (!(reader[84] is DBNull)) beData.CA_District = reader.GetString(84);
                    if (!(reader[85] is DBNull)) beData.CA_LocalLevel = reader.GetString(85);
                    if (!(reader[86] is DBNull)) beData.CA_WardNo = reader.GetInt32(86);
                    if (!(reader[87] is DBNull)) beData.StreetName = reader.GetString(87);
                    if (!(reader[88] is DBNull)) beData.CA_HouseNo = reader.GetString(88);
                    if (!(reader[89] is DBNull)) beData.PassOutClass = reader.GetString(89);
                    if (!(reader[90] is DBNull)) beData.PassOutSymbolNo = reader.GetString(90);
                    if (!(reader[91] is DBNull)) beData.PassOutGPA = Convert.ToDouble(reader[91]);
                    if (!(reader[92] is DBNull)) beData.PassOutRemarks = reader.GetString(92);
                    if (!(reader[93] is DBNull)) beData.CitizenFrontPhoto = reader.GetString(93);
                    if (!(reader[94] is DBNull)) beData.CitizenBackPhoto = reader.GetString(94);
                    if (!(reader[95] is DBNull)) beData.NIDPhoto = reader.GetString(95);
                    if (!(reader[96] is DBNull)) beData.PaidUpToMonthNP = reader.GetString(96);
                    if (!(reader[97] is DBNull)) beData.PassOutGrade = reader.GetString(97);
                    if (!(reader[98] is DBNull)) beData.AdmitDateMiti = reader.GetString(98);


                }
                reader.NextResult();
                beData.SiblingDetailColl = new AcademicLib.RE.Report.SiblingDetailsCollections();
                while (reader.Read())
                {
                    AcademicLib.RE.Report.SiblingDetails sibling = new AcademicLib.RE.Report.SiblingDetails();
                    if (!(reader[0] is DBNull)) sibling.StudentID = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) sibling.ClassSection = reader.GetString(1);
                    if (!(reader[2] is DBNull)) sibling.StudentName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) sibling.Relation = reader.GetString(3);
                    if (!(reader[4] is DBNull)) sibling.Remarks = reader.GetString(4);
                    beData.SiblingDetailColl.Add(sibling);
                }
                reader.NextResult();
                beData.AcademicDetailsColl = new AcademicLib.RE.Report.StudentAcademicCollections();
                while (reader.Read())
                {
                    AcademicLib.RE.Report.StudentAcademicDetails academic = new AcademicLib.RE.Report.StudentAcademicDetails();

                    if (!(reader[0] is DBNull)) academic.StudentID = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) academic.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) academic.Level = reader.GetString(2);
                    if (!(reader[3] is DBNull)) academic.University = reader.GetString(3);
                    if (!(reader[4] is DBNull)) academic.Exam = reader.GetString(4);
                    if (!(reader[5] is DBNull)) academic.PassoutYear = reader.GetString(5);
                    if (!(reader[6] is DBNull)) academic.SymbolNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) academic.ObtainMarks = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) academic.ObtainPer = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) academic.Division = reader.GetString(9);
                    if (!(reader[10] is DBNull)) academic.GPA = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) academic.SchoolCollege = reader.GetString(11);
                    beData.AcademicDetailsColl.Add(academic);
                }
                reader.NextResult();
                beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.GeneralDocument doc = new Dynamic.BusinessEntity.GeneralDocument();
                    if (!(reader[0] is DBNull)) doc.Id = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) doc.DocumentTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) doc.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) doc.Description = reader.GetString(3);
                    beData.AttachmentColl.Add(doc);
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

