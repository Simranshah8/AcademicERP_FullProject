
app.controller('BookLostController', function ($scope, $http, $timeout, $filter, GlobalServices, $translate) {
    $scope.Title = 'Book Lost';

    getterAndSetter();

    function getterAndSetter() {


        $scope.gridOptions = [];

        $scope.gridOptions = {
            showGridFooter: true,
            showColumnFooter: false,
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: true,
            enableSorting: true,
            enableRowSelection: true,
            enableSelectAll: true,
            enableGridMenu: true,

            columnDefs: [
                { name: "SNo", displayName: "S.No", minWidth: 70, headerCellClass: 'headerAligment' },
                { name: "LostDamage", displayName: "Lost/Damage", minWidth: 100, headerCellClass: 'headerAligment' },
                { name: "LostDateBS", displayName: "Lost/Damage Date", minWidth: 140, headerCellClass: 'headerAligment' },
                { name: "IssueTo", displayName: "Lost/Damage By", minWidth: 90, headerCellClass: 'headerAligment' },
                { name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
                { name: 'RegdNo', displayName: 'Regd. No.', width: 120, headerCellClass: 'headerAligment'  },
                { name: 'ClassName', displayName: 'Class', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'SectionName', displayName: 'Section', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'AccessionNo', displayName: 'Accession No', width: 130, headerCellClass: 'headerAligment'  },
                { name: 'BookTitle', displayName: 'Book Title', width: 200, headerCellClass: 'headerAligment' },
                { name: 'Subject', displayName: 'Subject', width: 150, headerCellClass: 'headerAligment'  },
                { name: 'Publication', displayName: 'Publication', width: 150, headerCellClass: 'headerAligment' },
                { name: 'IssueDate_BS', displayName: 'Issued Date', width: 120, headerCellClass: 'headerAligment'  },
                { name: 'CreditDays', displayName: 'Credit Days', width: 100, headerCellClass: 'headerAligment' },
                { name: 'TotalDays', displayName: 'Total Days', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'OutStandingDays', displayName: 'Outstanding Days', width: 130, headerCellClass: 'headerAligment' },
                { name: 'ReturnRemarks', displayName: 'Remarks', width: 150, headerCellClass: 'headerAligment'  },
                { name: 'BookNo', displayName: 'Book No', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'CallNo', displayName: 'Call No', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'BarCode', displayName: 'Bar Code', width: 120, headerCellClass: 'headerAligment'  },
                { name: 'Batch', displayName: 'Batch', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'Faculty', displayName: 'Faculty', width: 120, headerCellClass: 'headerAligment' },
                { name: 'Level', displayName: 'Level', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'Semester', displayName: 'Semester', width: 100, headerCellClass: 'headerAligment' },
                { name: 'ClassYear', displayName: 'Class Year', width: 120, headerCellClass: 'headerAligment'  },
                { name: 'BookPrice', displayName: 'Price', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'Vendor', displayName: 'Vendor', width: 150, headerCellClass: 'headerAligment' },
                { name: 'PurchaseDate_BS', displayName: 'Purchase Date', width: 130, headerCellClass: 'headerAligment'  },
                { name: 'BillNo', displayName: 'Bill No.', width: 100, headerCellClass: 'headerAligment' },
                { name: 'B_Faculty', displayName: 'B_Faculty', width: 120, headerCellClass: 'headerAligment' },
                { name: 'B_Level', displayName: 'B_Level', width: 100, headerCellClass: 'headerAligment' },
                { name: 'B_Semester', displayName: 'B_Semester', width: 100, headerCellClass: 'headerAligment'  },
                { name: 'B_ClassYear', displayName: 'B_ClassYear', width: 120, headerCellClass: 'headerAligment' },
                { name: 'BookCategory', displayName: 'Book Category', width: 150, headerCellClass: 'headerAligment'  },
                {
                    name: 'Action',
                    enableHiding: false,
                    enableFiltering: false,
                    enableSorting: false,
                    minWidth: 90,
                    enableColumnResizing: false,
                    pinnedRight: true,
                    cellClass: 'text-center',
                    cellTemplate:
                        '</a>' + '<a href="" class="p-1" title="Select" ng-click="grid.appScope.ShowModal(row.entity)">' +
                        '<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
                        '</a>'
                }
            ],
            //   rowTemplate: rowTemplate(),
            exporterCsvFilename: 'empSummary.csv',
            exporterPdfDefaultStyle: { fontSize: 9 },
            exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
            exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
            exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
            exporterPdfFooter: function (currentPage, pageCount) {
                return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
            },
            exporterPdfCustomFormatter: function (docDefinition) {
                docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
                docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
                return docDefinition;
            },
            exporterPdfOrientation: 'portrait',
            exporterPdfPageSize: 'LETTER',
            exporterPdfMaxGridWidth: 500,
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
            exporterExcelFilename: 'empSummary.xlsx',
            exporterExcelSheetName: 'DayBook',
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            }
        };

    };
    $scope.LoadData = function () {

        $('.select2').select2();

        $scope.Labels = {
            RegdNo: ''
        };

        //$translate.getTranslationTable()['REGDNO_LNG']
        $translate('REGDNO_LNG').then(function (data) {
            $scope.Labels.RegdNo = data;
        });

        $scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
        $scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
        $scope.BookSearchOptions = [{ text: 'AccessionNo', value: 'BD.AccessionNo' }, { text: 'Book Title', value: 'BD.BookTitle' }, { text: 'Subject', value: 'BD.Subject' }, { text: 'BookNo', value: 'BD.BookNo' }, { text: 'CallNo', value: 'BD.CallNo' }];
        $scope.IssueToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Teacher' }];
        $scope.ActionTypeColl = [{ id: 1, text: 'Recovery Amount' }, { id: 2, text: 'Buy & Submit' }, { id: 3, text: 'Additional Fine' }, { id: 4, text: 'None' }];
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.AcademicConfig = {};
        GlobalServices.getAcademicConfig().then(function (res1) {
            $scope.AcademicConfig = res1.data.Data;

            if ($scope.AcademicConfig.ActiveFaculty == false) {

                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);

                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'B_Faculty' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);

            }

            if ($scope.AcademicConfig.ActiveLevel == false) {

                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);



                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'B_Level' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);

            }

            if ($scope.AcademicConfig.ActiveSemester == false) {

                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);


                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'B_Semester' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);

            }

            if ($scope.AcademicConfig.ActiveBatch == false) {
                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);


            }

            if ($scope.AcademicConfig.ActiveClassYear == false) {

                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);


                findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'B_ClassYear' });
                if (findInd != -1)
                    $scope.gridOptions.columnDefs.splice(findInd, 1);

            }


        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //$scope.gridApi.grid.refresh();

        $scope.newBookLost = {
            BookLostId: null,
            ReceiveNo: 0,
            LostDamageFrom: 1,
            SelectStudent: $scope.StudentSearchOptions[0].value,
            SelectEmployee: $scope.EmployeeSearchOptions[0].value,
            BookLostDetailsColl: [],
            LostBookDetailsColl: [],
            DamageBookDetailsColl: [],
            Barcode: '',
            Mode: 'Save'
        };
        $scope.newFilter = {
            DateFrom_TMP: new Date(new Date().setDate(new Date().getDate() - 7)),
            DateTo_TMP: new Date()
        }


    };

    // default header
    $scope.recoveryHeaderForLost = 'Recovery Amount';
    $scope.recoveryHeader_Damage = 'Recovery Amount';
    $scope.onActionTypeChange = function (actionTypeId, ld) {
        if (ld == 1) {
            if (actionTypeId == 1) {
                $scope.recoveryHeaderForLost = 'Recovery Amount';
            }
            else if (actionTypeId == 2) {
                $scope.recoveryHeaderForLost = 'Is Book Submitted?';
            }
            else if (actionTypeId == 3) {
                $scope.recoveryHeaderForLost = 'Additional Fine';
            } else {
                $scope.recoveryHeaderForLost = 'None';
            }
        }
        else {
            if (actionTypeId == 1) {
                $scope.recoveryHeader_Damage = 'Recovery Amount';
            }
            else if (actionTypeId == 2) {
                $scope.recoveryHeader_Damage = 'Is Book Submitted?';
            }
            else if (actionTypeId == 3) {
                $scope.recoveryHeader_Damage = 'Additional Fine';
            } else {
                $scope.recoveryHeader_Damage = 'None';
            }
        }
    };

    $scope.GetBookDetails = function (BookDetails) {
        $scope.newBookLost.BookDetails = BookDetails;

    };

    $scope.CurDues = null;
    $scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
    $scope.getDuesDetails = function () {
        $scope.CurDues = null;
        $scope.PreviousBookDetailsColl = [];
        if ($scope.newBookLost.StudentId && $scope.newBookLost.StudentId > 0) {
            var para = {
                StudentId: $scope.newBookLost.StudentId,
                PaidUpToMonth: 0,
                PaidUpMonthColl: '',
                SemesterId: ($scope.newBookLost.StudentDetails ? $scope.newBookLost.StudentDetails.SemesterId : null),
                ClassYearId: ($scope.newBookLost.StudentDetails ? $scope.newBookLost.StudentDetails.ClassYearId : null)
            };
            $http({
                method: 'POST',
                url: base_url + "Fee/Transaction/GetDuesForReceipt",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.CurDues = res.data.Data;

                    if (!$scope.CurDues.PhotoPath || $scope.CurDues.PhotoPath.length == 0)
                        $scope.CurDues.PhotoPath = $scope.DefaultPhoto;

                    $timeout(function () {
                        $scope.getPreviousBookList(1);
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }

    }

    $scope.getPreviousBookList = function (forST) {
        $scope.newBookLost.LostBookDetailsColl = [];
        $scope.newBookLost.DamageBookDetailsColl = [];
        if (forST == 1) {
            $scope.newBookLost.EmployeeId = null;
        }
        else
            $scope.newBookLost.StudentId = null;

        $scope.PreviousBookDetailsColl = [];
        var para = {
            StudentId: $scope.newBookLost.StudentId,
            EmployeeId: $scope.newBookLost.EmployeeId,
        };
        $http({
            method: 'POST',
            url: base_url + "Library/Master/GetPreviousBookDetailsList",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.PreviousBookDetailsColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }

   
    $scope.ClearBookLost = function () {

        $timeout(function () {

            $scope.PreviousBookDetailsColl = [];

            $scope.newBookLost = {
                BookLostId: null,
                ReceiveNo: 0,
                LostDate_TMP: new Date(),
                LostDamageFrom: 1,
                SelectStudent: $scope.StudentSearchOptions[0].value,
                SelectEmployee: $scope.EmployeeSearchOptions[0].value,
                BookLostDetailsColl: [],
                LostBookDetailsColl: [],
                DamageBookDetailsColl: [],
                Mode: 'Save'
            };
        });


    };


    //column details Add for BookLost
    $scope.AddLostBookDetails = function (ind, bd) {

        $timeout(function () {
            $scope.newBookLost.LostBookDetailsColl.push({
                BookEntryId: bd.BookEntryId,
                IssueId: bd.IssueId,
                BookDet: bd,
                LostDamage: 1,
                ActionTypeId: 1,
                FineAmount: bd.FineAmt,
                LostDate_TMP: new Date()
            });

            $scope.newBookLost.BookDetails = bd;
            if ($scope.PreviousBookDetailsColl) {
                if ($scope.PreviousBookDetailsColl.length > 0) {
                    $scope.PreviousBookDetailsColl.splice(ind, 1);
                }
            }
        });

    };
    $scope.delLostBookDetails = function (ind) {
        if (!$scope.newBookLost.LostBookDetailsColl ||
            $scope.newBookLost.LostBookDetailsColl.length === 0) {
            return;
        }
        var deletedItem = $scope.newBookLost.LostBookDetailsColl[ind];
        if (!$scope.PreviousBookDetailsColl) {
            $scope.PreviousBookDetailsColl = [];
        }
        $scope.PreviousBookDetailsColl.push(deletedItem.BookDet);
        $scope.newBookLost.LostBookDetailsColl.splice(ind, 1);
    };


    //column details Add for DamageBook
    $scope.AddDamageBookDetails = function (ind, bd) {

        $timeout(function () {
            $scope.newBookLost.DamageBookDetailsColl.push({
                BookEntryId: bd.BookEntryId,
                IssueId: bd.IssueId,
                BookDet: bd,
                LostDamage: 2,
                ActionTypeId: 1,
                FineAmount: bd.FineAmt,
                LostDate_TMP: new Date()
            });

            $scope.newBookLost.BookDetails = bd;

            if ($scope.PreviousBookDetailsColl) {
                if ($scope.PreviousBookDetailsColl.length > 0) {
                    $scope.PreviousBookDetailsColl.splice(ind, 1);
                }
            }
        });

    };
    $scope.delDamageBookDetails = function (ind) {
        if (!$scope.newBookLost.DamageBookDetailsColl ||
            $scope.newBookLost.DamageBookDetailsColl.length === 0) {
            return;
        }
        var deletedItem = $scope.newBookLost.DamageBookDetailsColl[ind];
        if (!$scope.PreviousBookDetailsColl) {
            $scope.PreviousBookDetailsColl = [];
        }
        $scope.PreviousBookDetailsColl.push(deletedItem.BookDet);
        $scope.newBookLost.DamageBookDetailsColl.splice(ind, 1);
    };

    //************************* Book Lost *********************************

    $scope.IsValidBookLost = function () {
        var isvalid = true;

        return isvalid;
    }

    $scope.SaveUpdateBookLost = function () {
        if ($scope.IsValidBookLost() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newBookLost.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateBookLost();
                    }
                });
            } else
                $scope.CallSaveUpdateBookLost();

        }
    };

    $scope.CallSaveUpdateBookLost = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        if ($scope.newBookLost.LostDamageFrom == 1)
            $scope.newBookLost.EmployeeId = null;
        else
            $scope.newBookLost.StudentId = null;

        var dataColl = [];
        angular.forEach($scope.newBookLost.LostBookDetailsColl, function (rd) {
            if (rd.ActionTypeId == 2)
                rd.RecoveryAmount = null;
            else if (rd.ActionTypeId == 4) {
                rd.RecoveryAmount = null;
                rd.IsBookSubmitted = null;
            }
            else
                rd.IsBookSubmitted = null;
            dataColl.push({
                IssueId: rd.IssueId,
                LostDate: $filter('date')(new Date(rd.LostDateDet.dateAD), 'yyyy-MM-dd'),
                LostRemarks: rd.LostRemarks,
                LostTypeId: 1,
                ActionTypeId: rd.ActionTypeId,
                RecoveryAmount: rd.RecoveryAmount,
                IsBookSubmitted: rd.IsBookSubmitted
            });
        });
        angular.forEach($scope.newBookLost.DamageBookDetailsColl, function (rd) {
            if (rd.ActionTypeId == 2)
                rd.RecoveryAmount = null;
            else if (rd.ActionTypeId == 4) {
                rd.RecoveryAmount = null;
                rd.IsBookSubmitted = null;
            }
            else
                rd.IsBookSubmitted = null;
            dataColl.push({
                IssueId: rd.IssueId,
                LostDate: $filter('date')(new Date(rd.LostDateDet.dateAD), 'yyyy-MM-dd'),
                LostRemarks: rd.DamageRemarks,
                LostTypeId: 2,
                ActionTypeId: rd.ActionTypeId,
                RecoveryAmount: rd.RecoveryAmount,
                IsBookSubmitted: rd.IsBookSubmitted
            });
        });

        $http({
            method: 'POST',
            url: base_url + "Library/Master/SaveBookLostDamage",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearBookLost();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    //************************** Book Lost List *************************************
    $scope.onDateFromChange = function () {
        if ($scope.newFilter.DateToDet &&
            $scope.newFilter.DateFromDet &&
            $scope.newFilter.DateToDet.dateAD < $scope.newFilter.DateFromDet.dateAD) {
            $scope.newFilter.DateTo_TMP = null;
            Swal.fire('Invalid Date', 'Date To cannot be before Date From', 'warning');
        }
    };

    $scope.onDateToChange = function () {
        if ($scope.newFilter.DateFromDet &&
            $scope.newFilter.DateToDet &&
            $scope.newFilter.DateToDet.dateAD < $scope.newFilter.DateFromDet.dateAD) {
            $scope.newFilter.DateTo_TMP = null;
            Swal.fire('Invalid Date', 'Date To cannot be before Date From', 'warning');
        }
    };

    $scope.GetBookLostList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            DateFrom: $filter('date')(new Date($scope.newFilter.DateFromDet.dateAD), 'yyyy-MM-dd'),
            DateTo: $filter('date')(new Date($scope.newFilter.DateToDet.dateAD), 'yyyy-MM-dd'),
        }
        $http({
            method: 'POST',
            url: base_url + "Library/Master/GetBookLostList",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.gridOptions.data = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.ShowModal = function (data) {
        $scope.beData = {};
        $scope.clearData();
        $scope.beData = data;
        $scope.onActionTypeChange(data.ActionTypeId, 1);
        $('#actionTaken').modal('show');
    }
    $scope.clearData = function() {
        $scope.beData.RecoveryAmount = 0;
        $scope.beData.IsBookSubmitted = false;
    }
    $scope.UpdateBookLostAction = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            IssueId: refData.IssueId,
            RecoveryAmount: refData.RecoveryAmount || null,
            IsBookSubmitted: refData.IsBookSubmitted || false,
        };
        $http({
            method: 'POST',
            url: base_url + "Library/Master/UpdateBookLostAction",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GetBookLostList();
                $scope.clearData();
                Swal.fire(res.data.ResponseMSG);
                $('#actionTaken').modal('hide');
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.validateDates = function (changedField, obj) {
        if (!obj.DateFromDet || !obj.DateToDet ||
            !obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
            return true;
        }

        var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
        var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

        if (!fromDate || !toDate) return true;

        if (fromDate > toDate) {
            if (changedField === 'fromDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'From Date cannot be After To Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateFrom_TMP = new Date();
                        obj.DateFromDet = new Date();
                    });
                });
            } else if (changedField === 'toDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'To Date cannot be Before From Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateTo_TMP = new Date();
                        obj.DateToDet = new Date();
                    });
                });
            }
            return false;
        }
        return true;
    };

    $scope.validateForDate = function (obj) {
        if (!obj.LostDateDet || !obj.LostDateDet.dateAD) {
            return true;
        }

        var forDate = $filter('date')(obj.LostDateDet.dateAD, 'yyyy-MM-dd');
        var today = $filter('date')(new Date(), 'yyyy-MM-dd');

        if (!forDate) return true;

        if (forDate > today) {
            Swal.fire({
                icon: 'warning',
                text: 'Date cannot be a future date.',
                confirmButtonText: 'OK'
            }).then(function () {
                $scope.$apply(function () {
                    obj.LostDate_TMP = new Date();
                    obj.LostDateDet = new Date();
                });
            });
            return false;
        }
        return true;
    }

    $scope.validateDates = function (changedField, obj) {
        if (!obj.DateFromDet || !obj.DateToDet ||
            !obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
            return true;
        }

        var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
        var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

        if (!fromDate || !toDate) return true;

        if (fromDate > toDate) {
            if (changedField === 'fromDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'From Date cannot be After To Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateFrom_TMP = new Date();
                        obj.DateFromDet = new Date();
                    });
                });
            } else if (changedField === 'toDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'To Date cannot be Before From Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateTo_TMP = new Date();
                        obj.DateToDet = new Date();
                    });
                });
            }
            return false;
        }
        return true;
    };
});