using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Transaction
{
    public class BookEntry
    {
        DA.Library.Transaction.BookEntryDB db = null;
        int _UserId = 0;
        public BookEntry(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Transaction.BookEntryDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Transaction.BookEntry beData)
        {
            bool isModify = beData.BookEntryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Transaction.BookEntryCollections GetAllBookEntry(int EntityId)
        {
            return db.getAllBookEntry(_UserId, EntityId);
        }
        public BE.Library.Transaction.BookEntry GetBookEntryById(int EntityId, int BookEntryId)
        {
            return db.getBookEntryById(_UserId, EntityId, BookEntryId);
        }
        public ResponeValues DeleteById(int EntityId, int BookEntryId,int TranId)
        {
            return db.DeleteById(_UserId, EntityId, BookEntryId,TranId);
        }
        public ResponeValues getAccessionNo()
        {
            return db.getAccessionNo(_UserId);
        }
        public RE.Library.BookDetailsCollections getBookDetailsList(int MaterialTypeId, int ForType)
        {
            return db.getBookDetailsList(_UserId,MaterialTypeId,ForType);
        }
        public RE.Library.BookDetailsCollections getBookIssueRegister(DateTime dateFrom, DateTime dateTo,bool PendingForReceived)
        {
            return db.getBookIssueRegister(_UserId, dateFrom, dateTo,PendingForReceived);
        }
        public RE.Library.BookDetailsCollections getBookReceivedRegister( DateTime dateFrom, DateTime dateTo)
        {
            return db.getBookReceivedRegister(_UserId, dateFrom, dateTo);
        }
        public RE.Library.BookDetailsCollections getBookRegister(int BookEntryId)
        {
            return db.getBookRegister(_UserId, BookEntryId);
        }
        public RE.Library.BookDetailsCollections getStudentEmpBookRegister(int? StudentId,int? EmployeeId)
        {
            return db.getStudentEmpBookRegister(_UserId, StudentId,EmployeeId);
        }
        public BE.Library.Transaction.BookAutoCompleteCollections getAllBookAutoComplete( string searchBy, string Operator, string searchValue,bool forReport)
        {
            return db.getAllBookAutoComplete(_UserId, searchBy, Operator, searchValue,forReport);
        }
        public BE.Library.Transaction.BookDetailsForBarCodeCollections getBookForPrintBarcode( int fromAccessionNo, int toAccessionNo)
        {
            return db.getBookForPrintBarcode(_UserId, fromAccessionNo, toAccessionNo);
        }

        public BE.Library.Transaction.BookAutoComplete getBookDetailsByBarcode( string Barcode)
        {
            return db.getBookDetailsByBarcode(_UserId, Barcode);
        }
        public RE.Library.StudentLedgerCollections getStudentLedger(int AcademicYearId, int StudentId)
        {
            return db.getStudentLedger(_UserId,AcademicYearId, StudentId);
        }

        public RE.Library.BookDetailsCollections getAllBookList(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, int ForType)
        {
            return db.getAllBookList(_UserId, SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl, ForType);
        }

        public RE.Library.BookDetailsCollections getAllBooksTaken(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, bool PendingForReceived)
        {
            return db.getAllBooksTaken(_UserId, SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl, PendingForReceived);
        }

        public RE.Library.BookDetailsCollections getAllBookReceived(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            return db.getAllBooksReturned(_UserId, SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl);
        }
        public ResponeValues IsValidData(ref BE.Library.Transaction.BookEntry beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BookEntryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BookEntryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                 else if(!beData.DepartmentId.HasValue || beData.DepartmentId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Department Name";
                }
                else if (!beData.SubjectId.HasValue || beData.SubjectId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Subject Name";
                }                
                else if (!beData.PublicationId.HasValue || beData.PublicationId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Publication Name";
                }
                //else if (!beData.FixFineAmount.HasValue || beData.FixFineAmount.Value == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Fix Fine Amount";
                //}
                //else if (!beData.LateFineAmountPerDay.HasValue || beData.LateFineAmountPerDay.Value == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Late Fix Fine Amount";
                //}
                else if (!beData.NoOfBooks.HasValue || beData.NoOfBooks.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter No Of Books";
                }

                else
                {
                    if (beData.BookTitleId.HasValue && beData.BookTitleId.Value == 0)
                        beData.BookTitleId = null;

                    if (beData.AcademicYearId.HasValue && beData.AcademicYearId.Value == 0)
                        beData.AcademicYearId = null;

                    if (beData.ClassId.HasValue && beData.ClassId.Value == 0)
                        beData.ClassId = null;
                

                    if (beData.PublicationId.HasValue && beData.PublicationId.Value == 0)
                        beData.PublicationId = null;

                    if (beData.DonarId.HasValue && beData.DonarId.Value == 0)
                        beData.DonarId = null;

                    if (beData.SubjectId.HasValue && beData.SubjectId.Value == 0)
                        beData.SubjectId = null;

                    if (beData.MediumId.HasValue && beData.MediumId.Value == 0)
                        beData.MediumId = null;


                    if(beData.DetailsList==null || beData.DetailsList.Count == 0)
                    {
                        resVal.ResponseMSG = "Please ! Enter Book Details";
                        return resVal;
                    }

                    foreach(var v in beData.DetailsList)
                    {
                        if (v.AccessionNo == 0)
                        {
                            resVal.ResponseMSG = "Please ! Enter Accession No.";
                            return resVal;
                        }
                    }


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


        public ResponeValues CheckBookInUse( int BookEntryId)
        {
            return db.CheckBookInUse(_UserId, BookEntryId);
        }

    }
}
