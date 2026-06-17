using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{

	internal class NewBankAccountDB
	{
		DataAccessLayer1 dal = null;
		public NewBankAccountDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(AcademicLib.BE.Academic.Transaction.NewBankAccount beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Saluation", beData.Saluation);
			cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
			cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
			cmd.Parameters.AddWithValue("@LastName", beData.LastName);
			cmd.Parameters.AddWithValue("@Gender", beData.Gender);
			cmd.Parameters.AddWithValue("@Nationality", beData.Nationality);
			cmd.Parameters.AddWithValue("@OtherNationality", beData.OtherNationality);
			cmd.Parameters.AddWithValue("@DOB", beData.DOB);
			cmd.Parameters.AddWithValue("@Education", beData.Education);
			cmd.Parameters.AddWithValue("@MaritalStatusId", beData.MaritalStatusId);
			cmd.Parameters.AddWithValue("@CitizenshipNumber", beData.CitizenshipNumber);
			cmd.Parameters.AddWithValue("@TypeofIdSubmittedId", beData.TypeofIdSubmittedId);
			cmd.Parameters.AddWithValue("@IdNo", beData.IdNo);
			cmd.Parameters.AddWithValue("@IDIssueDate", beData.IDIssueDate);
			cmd.Parameters.AddWithValue("@IdIssuePlace", beData.IdIssuePlace);
			cmd.Parameters.AddWithValue("@IDExpiryDate", beData.IDExpiryDate);
			cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
			cmd.Parameters.AddWithValue("@TelephoneNo", beData.TelephoneNo);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@CA_HouseNo", beData.CA_HouseNo);
			cmd.Parameters.AddWithValue("@CA_WardNo", beData.CA_WardNo);
			cmd.Parameters.AddWithValue("@CA_StreetName", beData.CA_StreetName);
			cmd.Parameters.AddWithValue("@CA_ProvinceId", beData.CA_ProvinceId);
			cmd.Parameters.AddWithValue("@CA_DistrictId", beData.CA_DistrictId);
			cmd.Parameters.AddWithValue("@CA_LocalLevelId", beData.CA_LocalLevelId);
			cmd.Parameters.AddWithValue("@CA_CountryId", beData.CA_CountryId);
			cmd.Parameters.AddWithValue("@PA_HouseNo", beData.PA_HouseNo);
			cmd.Parameters.AddWithValue("@PA_WardNo", beData.PA_WardNo);
			cmd.Parameters.AddWithValue("@PA_StreetName", beData.PA_StreetName);
			cmd.Parameters.AddWithValue("@PA_ProvinceId", beData.PA_ProvinceId);
			cmd.Parameters.AddWithValue("@PA_DistrictId", beData.PA_DistrictId);
			cmd.Parameters.AddWithValue("@PA_LocalLevelId", beData.PA_LocalLevelId);
			cmd.Parameters.AddWithValue("@PA_CountryId", beData.PA_CountryId);
			cmd.Parameters.AddWithValue("@Occupation", beData.Occupation);
			cmd.Parameters.AddWithValue("@OtherOccupation", beData.OtherOccupation);
			cmd.Parameters.AddWithValue("@PanNo", beData.PanNo);
			cmd.Parameters.AddWithValue("@OrganizationName", beData.OrganizationName);
			cmd.Parameters.AddWithValue("@Designation", beData.Designation);
			cmd.Parameters.AddWithValue("@EmployeeContactNo", beData.EmployeeContactNo);
			cmd.Parameters.AddWithValue("@AnnualIncome", beData.AnnualIncome);
			cmd.Parameters.AddWithValue("@SourceOfIncomeId", beData.SourceOfIncomeId);
			cmd.Parameters.AddWithValue("@PurposeofAccount", beData.PurposeofAccount);
			cmd.Parameters.AddWithValue("@OtherPurposeofAcc", beData.OtherPurposeofAcc);
			cmd.Parameters.AddWithValue("@AnnualTransaction", beData.AnnualTransaction);
			cmd.Parameters.AddWithValue("@SpouseName", beData.SpouseName);
			cmd.Parameters.AddWithValue("@FatherName", beData.FatherName);
			cmd.Parameters.AddWithValue("@MotherName", beData.MotherName);
			cmd.Parameters.AddWithValue("@GrandfatherName", beData.GrandfatherName);
			cmd.Parameters.AddWithValue("@GrandmotherName", beData.GrandmotherName);
			cmd.Parameters.AddWithValue("@SonName", beData.SonName);
			cmd.Parameters.AddWithValue("@DaughterName", beData.DaughterName);
			cmd.Parameters.AddWithValue("@DaughterInLawName", beData.DaughterInLawName);
			cmd.Parameters.AddWithValue("@FatherInLawName", beData.FatherInLawName);
			cmd.Parameters.AddWithValue("@NomineeName", beData.NomineeName);
			cmd.Parameters.AddWithValue("@NomineeRelationId", beData.NomineeRelationId);
			cmd.Parameters.AddWithValue("@NomineeIdentificationDocumentId", beData.NomineeIdentificationDocumentId);
			cmd.Parameters.AddWithValue("@NomineeIdNo", beData.NomineeIdNo);
			cmd.Parameters.AddWithValue("@NomineeFather", beData.NomineeFather);
			cmd.Parameters.AddWithValue("@NomineeMother", beData.NomineeMother);
			cmd.Parameters.AddWithValue("@InstantDebitCard", beData.InstantDebitCard);
			cmd.Parameters.AddWithValue("@NameEmbossedDebitCard", beData.NameEmbossedDebitCard);
			cmd.Parameters.AddWithValue("@MobBankingInquiryOnly", beData.MobBankingInquiryOnly);
			cmd.Parameters.AddWithValue("@MobBankingInqAndTranBoth", beData.MobBankingInqAndTranBoth);
			cmd.Parameters.AddWithValue("@InternetBankingInquiryOnly", beData.InternetBankingInquiryOnly);
			cmd.Parameters.AddWithValue("@InternetBankingInqAndTranBoth", beData.InternetBankingInqAndTranBoth);
			cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
			cmd.Parameters.AddWithValue("@SPhotoPath", beData.SPhotoPath);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@BankId", beData.BankId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateNewBankAccount";
			}
			else
			{
				cmd.Parameters[69].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddNewBankAccount";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[70].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[71].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[72].Direction = System.Data.ParameterDirection.Output;

			cmd.Parameters.AddWithValue("@ForUserId", beData.ForUserId);
			cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
			cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);

			cmd.Parameters.AddWithValue("@InstanctCard", beData.InstanctCard);
			cmd.Parameters.AddWithValue("@MobileBanking", beData.MobileBanking);
			cmd.Parameters.AddWithValue("@InternetBanking", beData.InternetBanking);
			cmd.Parameters.AddWithValue("@EmployeeType", beData.EmployeeType);
			cmd.Parameters.AddWithValue("@OtherSourceOfIncome", beData.OtherSourceOfIncome);

			cmd.Parameters.AddWithValue("@KYCMethod", beData.KYCMethod);
			cmd.Parameters.AddWithValue("@BankBranchId", beData.BankBranchId);
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[69].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[69].Value);

				if (!(cmd.Parameters[70].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[70].Value);

				if (!(cmd.Parameters[71].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[71].Value);

				if (!(cmd.Parameters[72].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[72].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveNewBankAccountAttDocDetails(beData.AttachmentColl, resVal.RId, beData.CUserId);
				}
			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return resVal;

		}

		public ResponeValues DeleteById(int UserId, int EntityId, int BankId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BankId", BankId);
			cmd.CommandText = "usp_DelNewBankAccountById";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[3].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return resVal;
		}
		public AcademicLib.BE.Academic.Transaction.NewBankAccountCollections getAllNewBankAccount(int UserId, int EntityId)
		{
			AcademicLib.BE.Academic.Transaction.NewBankAccountCollections dataColl = new AcademicLib.BE.Academic.Transaction.NewBankAccountCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllNewBankAccount";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Academic.Transaction.NewBankAccount beData = new AcademicLib.BE.Academic.Transaction.NewBankAccount();
					if (!(reader[0] is DBNull)) beData.BankId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.MiddleName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.LastName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Gender = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Nationality = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.DOB = Convert.ToDateTime(reader[6]);
					if (!(reader[7] is DBNull)) beData.CitizenshipNumber = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.MobileNo = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.Email = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.PurposeofAccount = reader.GetInt32(10);
					dataColl.Add(beData);
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return dataColl;

		}




		private void SaveNewBankAccountAttDocDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int BankId, int UserId)
		{
			foreach (var beData in dataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.Parameters.AddWithValue("@docDescription", beData.Description);
				cmd.Parameters.AddWithValue("@Extension", beData.Extension);
				cmd.Parameters.AddWithValue("@Document", beData.Data);
				cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
				cmd.Parameters.AddWithValue("@BankId", BankId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddNewBankAccountAttDocDetails";
				cmd.ExecuteNonQuery();
			}
		}


		public AcademicLib.BE.Academic.Transaction.NewBankAccount getNewBankAccountById(int UserId, int EntityId, int BankId,int? ForUserId)
		{
			AcademicLib.BE.Academic.Transaction.NewBankAccount beData = new AcademicLib.BE.Academic.Transaction.NewBankAccount();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BankId", BankId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
			cmd.CommandText = "usp_GetNewBankAccountById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.Academic.Transaction.NewBankAccount();
					if (!(reader[0] is DBNull)) beData.BankId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Saluation = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.FirstName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.MiddleName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.LastName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Gender = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Nationality = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.OtherNationality = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.DOB = Convert.ToDateTime(reader[8]);
					if (!(reader[9] is DBNull)) beData.Education = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.MaritalStatusId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.CitizenshipNumber = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.TypeofIdSubmittedId = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.IdNo = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.IDIssueDate = Convert.ToDateTime(reader[14]);
					if (!(reader[15] is DBNull)) beData.IdIssuePlace = reader.GetInt32(15);
					if (!(reader[16] is DBNull)) beData.IDExpiryDate = Convert.ToDateTime(reader[16]);
					if (!(reader[17] is DBNull)) beData.MobileNo = reader.GetString(17);
					if (!(reader[18] is DBNull)) beData.TelephoneNo = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.Email = reader.GetString(19);
					if (!(reader[20] is DBNull)) beData.CA_HouseNo = reader.GetString(20);
					if (!(reader[21] is DBNull)) beData.CA_WardNo = reader.GetString(21);
					if (!(reader[22] is DBNull)) beData.CA_StreetName = reader.GetString(22);
					if (!(reader[23] is DBNull)) beData.CA_ProvinceId = reader.GetInt32(23);
					if (!(reader[24] is DBNull)) beData.CA_DistrictId = reader.GetInt32(24);
					if (!(reader[25] is DBNull)) beData.CA_LocalLevelId = reader.GetInt32(25);
					if (!(reader[26] is DBNull)) beData.CA_CountryId = reader.GetInt32(26);
					if (!(reader[27] is DBNull)) beData.PA_HouseNo = reader.GetString(27);
					if (!(reader[28] is DBNull)) beData.PA_WardNo = reader.GetString(28);
					if (!(reader[29] is DBNull)) beData.PA_StreetName = reader.GetString(29);
					if (!(reader[30] is DBNull)) beData.PA_ProvinceId = reader.GetInt32(30);
					if (!(reader[31] is DBNull)) beData.PA_DistrictId = reader.GetInt32(31);
					if (!(reader[32] is DBNull)) beData.PA_LocalLevelId = reader.GetInt32(32);
					if (!(reader[33] is DBNull)) beData.PA_CountryId = reader.GetInt32(33);
					if (!(reader[34] is DBNull)) beData.Occupation = reader.GetInt32(34);
					if (!(reader[35] is DBNull)) beData.OtherOccupation = reader.GetString(35);
					if (!(reader[36] is DBNull)) beData.PanNo = reader.GetString(36);
					if (!(reader[37] is DBNull)) beData.OrganizationName = reader.GetString(37);
					if (!(reader[38] is DBNull)) beData.Designation = reader.GetString(38);
					if (!(reader[39] is DBNull)) beData.EmployeeContactNo = reader.GetString(39);
					if (!(reader[40] is DBNull)) beData.AnnualIncome = Convert.ToDouble(reader[40]);
					if (!(reader[41] is DBNull)) beData.SourceOfIncomeId = reader.GetInt32(41);
					if (!(reader[42] is DBNull)) beData.PurposeofAccount = reader.GetInt32(42);
					if (!(reader[43] is DBNull)) beData.OtherPurposeofAcc = reader.GetString(43);
					if (!(reader[44] is DBNull)) beData.AnnualTransaction = Convert.ToDouble(reader[44]);
					if (!(reader[45] is DBNull)) beData.SpouseName = reader.GetString(45);
					if (!(reader[46] is DBNull)) beData.FatherName = reader.GetString(46);
					if (!(reader[47] is DBNull)) beData.MotherName = reader.GetString(47);
					if (!(reader[48] is DBNull)) beData.GrandfatherName = reader.GetString(48);
					if (!(reader[49] is DBNull)) beData.GrandmotherName = reader.GetString(49);
					if (!(reader[50] is DBNull)) beData.SonName = reader.GetString(50);
					if (!(reader[51] is DBNull)) beData.DaughterName = reader.GetString(51);
					if (!(reader[52] is DBNull)) beData.DaughterInLawName = reader.GetString(52);
					if (!(reader[53] is DBNull)) beData.FatherInLawName = reader.GetString(53);
					if (!(reader[54] is DBNull)) beData.NomineeName = reader.GetString(54);
					if (!(reader[55] is DBNull)) beData.NomineeRelationId = reader.GetInt32(55);
					if (!(reader[56] is DBNull)) beData.NomineeIdentificationDocumentId = reader.GetInt32(56);
					if (!(reader[57] is DBNull)) beData.NomineeIdNo = reader.GetString(57);
					if (!(reader[58] is DBNull)) beData.NomineeFather = reader.GetString(58);
					if (!(reader[59] is DBNull)) beData.NomineeMother = reader.GetString(59);
					if (!(reader[60] is DBNull)) beData.InstantDebitCard = Convert.ToBoolean(reader[60]);
					if (!(reader[61] is DBNull)) beData.NameEmbossedDebitCard = Convert.ToBoolean(reader[61]);
					if (!(reader[62] is DBNull)) beData.MobBankingInquiryOnly = Convert.ToBoolean(reader[62]);
					if (!(reader[63] is DBNull)) beData.MobBankingInqAndTranBoth = Convert.ToBoolean(reader[63]);
					if (!(reader[64] is DBNull)) beData.InternetBankingInquiryOnly = Convert.ToBoolean(reader[64]);
					if (!(reader[65] is DBNull)) beData.InternetBankingInqAndTranBoth = Convert.ToBoolean(reader[65]);
					if (!(reader[66] is DBNull)) beData.PhotoPath = reader.GetString(66);
					if (!(reader[67] is DBNull)) beData.SPhotoPath = reader.GetString(67);

					if (!(reader[68] is DBNull)) beData.ForUserId = reader.GetInt32(68);
					if (!(reader[69] is DBNull)) beData.StudentId = reader.GetInt32(69);
					if (!(reader[70] is DBNull)) beData.EmployeeId = reader.GetInt32(70);

					if (!(reader[71] is DBNull)) beData.InstanctCard = reader.GetString(71);
					if (!(reader[72] is DBNull)) beData.MobileBanking = reader.GetInt32(72);
					if (!(reader[73] is DBNull)) beData.InternetBanking = reader.GetInt32(73);
					if (!(reader[74] is DBNull)) beData.EmployeeType = reader.GetInt32(74);
					if (!(reader[75] is DBNull)) beData.OtherSourceOfIncome = reader.GetString(75);

					if (!(reader[76] is DBNull)) beData.BankBranchId = reader.GetInt32(76);
					if (!(reader[77] is DBNull)) beData.KYCMethod = reader.GetInt32(77);
					if (!(reader[78] is DBNull)) beData.BankSNo = reader.GetInt32(78);
					if (!(reader[79] is DBNull)) beData.BankBranchName = reader.GetString(79);

					beData.IsSuccess = true;
					beData.ResponseMSG = GLOBALMSG.SUCCESS;
				}else
                {
					beData.IsSuccess = false;
					beData.ResponseMSG = "Please ! Update Profile 1st";
                }
				reader.NextResult();
				while (reader.Read())
				{
					Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
					if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) det.Extension = reader.GetString(3);
					if (!(reader[4] is DBNull)) det.DocPath = reader.GetString(4);
					beData.AttachmentColl.Add(det);
				}
				reader.Close();
				
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

