app.controller('UtilitiesController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, Excel, $translate, FileSaver) {
    $scope.Title = 'Utilities';

    getterAndSetter();
    OnClickDefault();
	var gSrv = GlobalServices;
    function getterAndSetter() {

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
            rowHeight: 31,
            columnDefs: [     
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 90,
					enableColumnResizing: false,
					cellClass: "overflow-visible",
					cellTemplate: '<a href="" class="p-1" title="Click For Edit" ng-click="grid.appScope.GetContinuousConfirmationById(row.entity)">' +
						'<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
						'</a>' +
						
						'<a href="" class="p-1" title="Click For Delete" ng-click="grid.appScope.DelContinuousConfirmationById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>',
					pinned: 'left',

				},
				/*{ name: "SNo", displayName: "S.No.", minWidth: 75, headerCellClass: 'headerAligment', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>' },*/
                { name: "RegNo", displayName: "Adm.No.", minWidth: 130, headerCellClass: 'headerAligment' },
                { name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
                { name: "Gender", displayName: "Gender", minWidth: 110, headerCellClass: 'headerAligment' },
                { name: "RollNo", displayName: "RollNo", minWidth: 110, headerCellClass: 'headerAligment' },
                { name: "ClassName", displayName: "Class", minWidth: 130, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
                { name: "SectionName", displayName: "Section", minWidth: 110, headerCellClass: 'headerAligment' },
                { name: "Continuous", displayName: "Continuous?", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "NotContinueReason", displayName: "Reason", minWidth: 180, headerCellClass: 'headerAligment' },
                { name: "Feedback", displayName: "Feedback", minWidth: 190, headerCellClass: 'headerAligment' },
                { name: "ContactNo", displayName: "Contac tNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 190, headerCellClass: 'headerAligment' },
                { name: "FContactNo", displayName: "Father Contact No", minWidth: 150, headerCellClass: 'headerAligment' },
                { name: "MotherName", displayName: "Mother Name", minWidth: 190, headerCellClass: 'headerAligment' },
                { name: "MContactNo", displayName: "Mother ContactNo", minWidth: 150, headerCellClass: 'headerAligment' },
                { name: "GuardianName", displayName: "Guardian Name", minWidth: 190, headerCellClass: 'headerAligment' },
                { name: "GContactNo", displayName: "Guardian Contact No", minWidth: 190, headerCellClass: 'headerAligment' },
            ],
            exporterCsvFilename: 'registrationSummary.csv',
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
            exporterExcelFilename: 'regSummary.xlsx',
            exporterExcelSheetName: 'regSummary',
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            }
        };
    };

	$scope.LoadData = function () {
		$('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
        $scope.NotContinueReasonList = [
            { id: 1, text: 'Transferring to another school' },
            { id: 2, text: 'Relocating to another city/country' },
            { id: 3, text: 'Financial reasons' },
            { id: 4, text: 'Health issues' },
            { id: 5, text: 'Personal/family reasons' },
            { id: 6, text: 'Dissatisfaction with academic performance' },
            { id: 7, text: 'Other' },        ]

        $scope.searchData = {
            Subject: ''

		};


		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data.ClassList);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newFilter = {
			SelectedClassId: 0,
			SectionId: 0
		};
       
        $scope.newDet = {
            StudentId: null,
            ContinueYes: false,
            ContinueNo: false,
            NotContinueReasonId: null,
            OtherReason: '',
            Feedback: '',
            SelectStudent: $scope.StudentSearchOptions[0].value,
            Mode: 'Save'
        };

       /* $scope.GetAllSubjectList();*/

    }
    function OnClickDefault() {
        document.getElementById('form-section').style.display = "none";
        document.getElementById('add-form').onclick = function () {
            document.getElementById('tablesection').style.display = "none";
            document.getElementById('form-section').style.display = "block";
            $scope.ClearSubject();
        }
        document.getElementById('back-to-list').onclick = function () {
            document.getElementById('form-section').style.display = "none";
            document.getElementById('tablesection').style.display = "block";
            $scope.ClearSubject();


        }

    }


	$scope.ClearContinuousConfirmation = function () {
        $scope.newDet = {
            StudentId: null,
            ContinueYes: false,
            ContinueNo: false,
            NotContinueReasonId: null,
            OtherReason: '',
            Feedback: '',
            SelectStudent: $scope.StudentSearchOptions[0].value,
            Mode: 'Save'
        };
    }

	$scope.selectContinueOption = function (option) {
		if (option === 'yes') {
			$scope.newDet.ContinueNo = false;
			$scope.newDet.NotContinueReasonId = null;
			$scope.newDet.OtherReason = '';
		} else if (option === 'no') {
			$scope.newDet.ContinueYes = false;
		}
	};



	$scope.ChangeStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentDetForInfirmarybyId",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				let studentData = res.data.Data;

				// Preserve SelectStudent value
				studentData.SelectStudent = $scope.newDet.SelectStudent;

				// Assign student data without losing existing properties
				$scope.bedata = Object.assign({}, $scope.newDet, studentData);

				if (!$scope.bedata.PhotoPath) {
					$scope.bedata.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';
				}
				if ($scope.bedata.DOB_AD) {
					let dob = new Date($scope.bedata.DOB_AD);
					let today = new Date();
					let ageYears = today.getFullYear() - dob.getFullYear();
					let ageMonths = today.getMonth() - dob.getMonth();
					let ageDays = today.getDate() - dob.getDate();

					if (ageDays < 0) {
						ageMonths--;
						let previousMonth = new Date(today.getFullYear(), today.getMonth(), 0);
						ageDays += previousMonth.getDate();
					}
					if (ageMonths < 0) {
						ageYears--;
						ageMonths += 12;
					}
					$scope.bedata.Age = `${ageYears} Years, ${ageMonths} Months, ${ageDays} Days`;
				}


			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.IsValidContinuousConfirmation = function () {
		if (!$scope.newDet.StudentId || $scope.newDet.StudentId === '') {
			Swal.fire('Please! Select a Student');
			return false;
		}

		if (!$scope.newDet.ContinueYes && !$scope.newDet.ContinueNo) {
			Swal.fire('Please select whether you will continue or not.');
			return false;
		}

		if ($scope.newDet.ContinueNo) {
			if (!$scope.newDet.NotContinueReasonId || $scope.newDet.NotContinueReasonId === '') {
				Swal.fire('Please select a reason for not continuing.');
				return false;
			}

			if ($scope.newDet.NotContinueReasonId == 7 && (!$scope.newDet.OtherReason || $scope.newDet.OtherReason.trim() === '')) {
				Swal.fire('Please specify the other reason for not continuing.');
				return false;
			}
		}

		return true;
	};


	$scope.SaveUpdateContinuousConfirmation = function () {
		if ($scope.IsValidContinuousConfirmation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateContinuousConfirmation();
					}
				});
			} else
				$scope.CallSaveUpdateContinuousConfirmation();
		}
	};

	$scope.CallSaveUpdateContinuousConfirmation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveUpdateContinuousConfirmation",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearContinuousConfirmation();
				$scope.GetAllContinuousConfirmationList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllContinuousConfirmationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ContinuousConfirmationList = [];
		var para = {
			ClassId: $scope.newFilter.SelectedClassId,   // Updated line
			SectionId: $scope.newFilter.SectionId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllContinuousConfirmation",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	

	$scope.GetContinuousConfirmationById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getContinuousConfirmationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;
				$scope.newDet.Mode = 'Modify';

				$scope.newDet.SelectStudent= $scope.StudentSearchOptions[0].value;
				$scope.ChangeStudent();
				document.getElementById('tablesection').style.display = "none";
				document.getElementById('form-section').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelContinuousConfirmationById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteContinuousConfirmation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllContinuousConfirmationList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

});
