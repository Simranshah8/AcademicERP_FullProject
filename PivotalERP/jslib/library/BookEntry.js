

app.controller('BookEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Book Entry';
	$scope.LoadData = function () {
		$('.select2').select2({
			//allowClear: true
		});
		//$("#cboBookTitle").select2({
		//	placeholder: "Select a State",
		//	allowClear: true
		//});
		var gSrv = GlobalServices;
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();

		$scope.currentPages = {
			BookEntry: 1,

		};

		$scope.searchData = {
			BookEntry: '',

		};

		$scope.perPage = {
			BookEntry: GlobalServices.getPerPageRow(),

		};


		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAllBookEntryList();
		 
		$scope.StatusList = gSrv.getStatusList();
		
		$scope.BookTitleList = [];
		$scope.BookTitleQuery = [];
		gSrv.getBookTitleList().then(function (res) {
			$scope.BookTitleList = res.data.Data;
			$scope.BookTitleQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AuthorList = [];
		$scope.AuthorQuery = [];
		gSrv.getAuthorList().then(function (res) {
			$scope.AuthorList = res.data.Data;
			$scope.AuthorQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.EditionList = [];
		$scope.EditionQuery = [];
		gSrv.getEditionList().then(function (res) {
			$scope.EditionList = res.data.Data;
			$scope.EditionQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MaterialTypeList = [];
		gSrv.getMaterialTypeList().then(function (res) {
			$scope.MaterialTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		


		$scope.PublicationList = [];
		$scope.PublicationQuery = [];
		gSrv.getPublicationList().then(function (res) {
			$scope.PublicationList = res.data.Data;
			$scope.PublicationQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SubjectList = [];
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.DonorList = [];
		gSrv.getDonorList().then(function (res) {
			$scope.DonorList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.RackList = [];
		gSrv.getRackList().then(function (res) {
			$scope.RackList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DepartmentList = [];
		$scope.DepartmentQuery= [];
		gSrv.getLibDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
			$scope.DepartmentQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		$scope.ClassQuery = [];
		gSrv.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
			$scope.ClassQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.MediumList = [];
		$scope.MediumQuery = [];
		gSrv.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
			$scope.MediumQuery = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicYearList = [];
		gSrv.getAcademicYearList().then(function (res) {
			$scope.AcademicYearList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BookCategoryList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookCategory",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookCategoryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {

			$scope.$apply(function () {
				$scope.newBookEntry = {
					BookTitleId: null,
					FixFineAmount: 0,
					LateFineAmountPerDay: 0,
					NoOfBooks: 0,
					BookEntryId: null,
					AccessionNo: 0,
					BarCode: '',
					BookTitleId: 1,
					AuthorId: null,
					SubjectId: null,
					PublicationId: null,
					EditionId: null,
					YearId: null,
					Volume: '',
					ISBNNo: '',
					BookNo: '',
					CallNo:'',
					Pages: 0,
					Language: '',
					DonarId: null,
					DepartmentId: null,
					ClassId: null,
					MediumId: null,
					AcademicYearId: null,
					RackId: null,
					Location: '',
					PurchaseDate: new Date(),
					PurchaseDate_TMP: new Date(),
					Vendor: '',
					BillNo: '',
					BookPrice: 0,
					CreditDays: 0,
					Description: '',
					NoOfBooks: 0,
					StartedBookCode: '',
					EndedBookCode: '',
					Status: 1,
					BookEntryDetailsColl: [],
					Mode: 'Save'
				};
				$scope.newBookEntry.BookEntryDetailsColl.push({});
			});
			
			//$('#cboBookTitle').val('1');
			//$('#cboBookTitle').select2().trigger('change');
			
		});
		$timeout(function () {
			$scope.ClearBookEntry();
		});
		$timeout(function () {

			if (TranId && TranId > 0) {
				$scope.GetBookEntryById();
			} else
				$scope.GetAccessionNo();

		});



	};

	$scope.ChangeClass = function () {
		$scope.SelectedClass = null;

		if ($scope.newBookEntry.ClassId > 0) {
			$scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newBookEntry.ClassId);

			if ($scope.SelectedClass) {
				var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
				var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

				$scope.SelectedClassClassYearList = [];
				$scope.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});

			}
        }
    }

	$scope.ClearBookEntry = function () {

		$timeout(function () {
			$scope.ClearPhoto();
			$scope.ClearBookEntryPhoto();
			$scope.newBookEntry = {
				BookTitleId: null,
				FixFineAmount: 0,
				LateFineAmountPerDay: 0,
				NoOfBooks: 0,
				BookEntryId: null,
				AccessionNo: 0,
				BarCode: '',
				BookTitle: '',
				AuthorId: null,
				SubjectId: null,
				PublicationId: null,
				EditionId: null,
				YearId: null,
				Volume: '',
				ISBNNo: '',
				BookNo: '',
				CallNo: '',
				Pages: 0,
				Language: '',
				DonarId: null,
				DepartmentId: null,
				ClassId: null,
				MediumId: null,
				AcademicYearId: null,
				RackId: null,
				Location: '',
				PurchaseDate: new Date(),
				PurchaseDate_TMP: new Date(),
				Vendor: '',
				BillNo: '',
				BookPrice: 0,
				CreditDays: 0,
				Description: '',
				NoOfBooks: 0,
				StartedBookCode: '',
				EndedBookCode: '',
				Status: 1,
				BookEntryDetailsColl: [],
				Mode: 'Save'
			};
			$scope.newBookEntry.BookEntryDetailsColl.push({});

			$scope.GetAccessionNo();
		});
		
	};

	// Clear photo
	$scope.ClearPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newBookEntry.PhotoData = null;
				$scope.newBookEntry.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};
	$scope.ClearBookEntryPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newBookEntry.PhotoData2 = null;
				$scope.newBookEntry.Photo_TMP = [];
			});

		});

		$('#imgPhoto-back').attr('src', '');
		$('#imgPhoto1-back').attr('src', '');

	};
	//************************* Class *********************************

	$scope.CheckedAllForBarCode = function () {
		angular.forEach($scope.newBookEntry.BookEntryDetailsColl, function (det) {
			det.PrintBarCode = $scope.newBookEntry.CheckAll;
			
		});

	};
	$scope.ChangeBookTitle = function () {

		var bookTitle = '', authors = '', publication = '', edition = '',className='',department='',medium='';
		var rackId = null;

		if ($scope.newBookEntry.BookTitleId && $scope.newBookEntry.BookTitleId > 0) {
			var findD = $scope.BookTitleQuery.firstOrDefault(p1 => p1.BookTitleId == $scope.newBookEntry.BookTitleId);
			if (findD)
				bookTitle = findD.Name;
		}
		if ($scope.newBookEntry.PublicationId && $scope.newBookEntry.PublicationId > 0) {
			var findD = $scope.PublicationQuery.firstOrDefault(p1 => p1.PublicationId == $scope.newBookEntry.PublicationId);
			if (findD)
				publication = findD.Name;
		}
		if ($scope.newBookEntry.EditionId && $scope.newBookEntry.EditionId > 0) {
			var findD = $scope.EditionQuery.firstOrDefault(p1 => p1.EditionId == $scope.newBookEntry.EditionId);
			if (findD)
				edition = findD.Name;
		}

		if ($scope.newBookEntry.ClassId && $scope.newBookEntry.ClassId > 0) {
			var findD = $scope.ClassQuery.firstOrDefault(p1 => p1.ClassId == $scope.newBookEntry.ClassId);
			if (findD)
				className = findD.Name;
		}

		if ($scope.newBookEntry.MediumId && $scope.newBookEntry.MediumId > 0) {
			var findD = $scope.MediumQuery.firstOrDefault(p1 => p1.MediumId == $scope.newBookEntry.MediumId);
			if (findD)
				medium = findD.Name;
		}

		if ($scope.newBookEntry.DepartmentId && $scope.newBookEntry.DepartmentId > 0) {
			var findD = $scope.DepartmentQuery.firstOrDefault(p1 => p1.DepartmentId == $scope.newBookEntry.DepartmentId);
			if (findD)
				department = findD.Name;
		}

		if ($scope.newBookEntry.RackId && $scope.newBookEntry.RackId > 0) {
			rackId = $scope.newBookEntry.RackId;
			var rackDet = $scope.RackList.find(r => r.RackId == rackId);
			$scope.newBookEntry.Location = rackDet.Location
		}

		if ($scope.newBookEntry.AuthorIdColl) {
			angular.forEach($scope.newBookEntry.AuthorIdColl, function (aid) {

				var findD = $scope.AuthorQuery.firstOrDefault(p1 => p1.AuthorId == aid);
				if (findD) {

					if (authors.length > 0)
						authors = authors + ',';

					authors = authors + findD.Name;
                }
			});
        }

		angular.forEach($scope.newBookEntry.BookEntryDetailsColl, function (det) {
			det.BookTitle = bookTitle;
			det.Publication = publication;
			det.Edition = edition;			
			det.ClassName = className;
			det.Department = department;
			det.Medium = medium;
			det.ISBSNo = $scope.newBookEntry.ISBNNo;

			if(rackId)
				det.RackId = rackId;

			det.Authors = authors;

		});

	};

	$scope.PrintBarCodeOfBook = function () {

		var dataColl = [];
		angular.forEach($scope.newBookEntry.BookEntryDetailsColl, function (det) {

			if (det.PrintBarCode == true)
				dataColl.push(det);
		});

		$http({
			method: 'POST',
			url: base_url + "Library/Master/PrintLibraryBarCode",
			dataType: "json",
			data:JSON.stringify(dataColl)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {

				document.body.style.cursor = 'wait';
				document.getElementById("frmRpt").src = '';
				document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=0&istransaction=true&entityid=432&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
				document.body.style.cursor = 'default';
				$('#FrmPrintReport').modal('show');

			} else
				Swal.fire('No Templates found for print');
			
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	};

	$scope.ChangeStartEnd = function (start, end) {

		if ($scope.newBookEntry.BookEntryDetailsColl)
		{
			var s = start;
			var ss = 1;
			angular.forEach($scope.newBookEntry.BookEntryDetailsColl, function (det) {
				det.AccessionNo = s;
				det.BarCode = start+'-'+ss;
				s++;
				ss++;
			});
			$scope.newBookEntry.EndedAccessionNo = (s - 1);
        }
		
	};
	$scope.ChangeNoOfBooks = function (noofbook) {
		if (noofbook < 0) {
			$scope.newBookEntry.NoOfBooks = 0;
			$scope.newBookEntry.BookEntryDetailsColl = [];
			GlobalServices.validateAndFocus('BENoOfBooks', 'Number of books cannot be less than zero', false);
			return false;
		} else {
			if (!$scope.newBookEntry.BookEntryDetailsColl)
				$scope.newBookEntry.BookEntryDetailsColl = [];

			var sno = 1;
			var totalRow = $scope.newBookEntry.BookEntryDetailsColl.length;
			for (var ind = totalRow; ind < noofbook; ind++) {
				$scope.newBookEntry.BookEntryDetailsColl.push(
					{
						SNo: sno
					}
				);
				sno++;
			}

			while (totalRow > noofbook) {
				totalRow = $scope.newBookEntry.BookEntryDetailsColl.length;
				if (totalRow != noofbook)
					$scope.newBookEntry.BookEntryDetailsColl.splice(totalRow - 1, 1);
			};

			$scope.ChangeBookTitle();
		}
	};

	$scope.IsValidBookEntry = function () {
		//let accNo = Number($scope.newBookEntry.AccessionNo);
		//if (accNo < 453453524 || accNo > 45345352412) {
		//	GlobalServices.validateAndFocus('BEAccessionNo', 'Please enter a valid Accession No between 453453524 and 45345352412', false);
		//	return false;
		//}
		if ($scope.newBookEntry.BookTitleId <= 0) {
			GlobalServices.validateAndFocus('cboBookTitle', 'Please! Select Book Title', true);
			return false;
		}
		if ($scope.newBookEntry.SubjectId <= 0) {
			GlobalServices.validateAndFocus('BESubject', 'Please! Select Subject', true);
			return false;
		}
		if ($scope.newBookEntry.PublicationId <= 0) {
			GlobalServices.validateAndFocus('BEPublication', 'Please! Select Publication', true);
			return false;
		}
		if ($scope.newBookEntry.DepartmentId <= 0) {
			GlobalServices.validateAndFocus('BEDepartment', 'Please! Select Department', true);
			return false;
		}
		if ($scope.newBookEntry.FixFineAmount <= 0) {
			GlobalServices.validateAndFocus('BEFixFineAmount', 'Please! Enter Fix Fine Amount', false);
			return false;
		}
		if ($scope.newBookEntry.LateFineAmountPerDay <= 0) {
			GlobalServices.validateAndFocus('BELateFineAmountPerDay', 'Please! Enter Fine Amount Per Day', false);
			return false;
		}
		if ($scope.newBookEntry.NoOfBooks <= 0) {
			GlobalServices.validateAndFocus('BENoOfBooks', 'Please! Enter No of Books', false);
			return false;
		}		
		return true;
	};
	$scope.GetAccessionNo = function () {
	
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAccessionNo",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBookEntry.AccessionNo = res.data.Data.RId;
				$scope.newBookEntry.BarCode = res.data.Data.RId;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.SaveUpdateBookEntry = function () {
		if ($scope.IsValidBookEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBookEntry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBookEntry();
					}
				});
			} else
				$scope.CallSaveUpdateBookEntry();

		}
	};

	$scope.CallSaveUpdateBookEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var frontBookPhoto = $scope.newBookEntry.Photo_TMP;
		var backBookPhoto = $scope.newBookEntry.Photo_TMP2;

		$scope.newBookEntry.DetailsList = [];

		angular.forEach($scope.newBookEntry.BookEntryDetailsColl, function (det) {
			$scope.newBookEntry.DetailsList.push(det);
		});

		if ($scope.newBookEntry.DetailsList.length == 0) {
			$scope.newBookEntry.DetailsList.push({
				AccessionNo: $scope.newBookEntry.AccessionNo,
				BarCode: $scope.newBookEntry.BarCode,
				RackId: $scope.newBookEntry.RackId
			});
        }

		if ($scope.newBookEntry.PurchaseDateDet) {
			$scope.newBookEntry.PurchaseDate = $scope.newBookEntry.PurchaseDateDet.dateAD;
		} else
			$scope.newBookEntry.PurchaseDate = null;


		if ($scope.SelectedClass) {
			$scope.newBookEntry.FacultyId = $scope.SelectedClass.FacultyId;
			$scope.newBookEntry.ClassLevelId = $scope.SelectedClass.LevelId;
        }

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveBookEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.photo && data.photo.length > 0)
					formData.append("photo", data.photo[0]);

				if (data.photo2 && data.photo2.length > 0)
					formData.append("photo2", data.photo2[0]);

				return formData;
			},
			data: { jsonData: $scope.newBookEntry, photo: frontBookPhoto, photo2: backBookPhoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBookEntry();
				//$scope.GetAllBookEntryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	
	$scope.GetBookEntryById = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BookEntryId: TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookEntryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				

				$timeout(function () {
					$scope.newBookEntry = res.data.Data;

					$scope.ChangeClass();

					if ($scope.newBookEntry.PurchaseDate)
						$scope.newBookEntry.PurchaseDate_TMP = new Date($scope.newBookEntry.PurchaseDate);

					$scope.newBookEntry.Mode = 'Modify';
				});

				$timeout(function () {
					$scope.newBookEntry.BookEntryDetailsColl = [];

					if ($scope.newBookEntry.DetailsList && $scope.newBookEntry.DetailsList.length > 0) {
						$scope.newBookEntry.AccessionNo = $scope.newBookEntry.DetailsList[0].AccessionNo;
						$scope.newBookEntry.BarCode = $scope.newBookEntry.DetailsList[0].BarCode;
                    }

					angular.forEach($scope.newBookEntry.DetailsList, function (det) {
						$scope.newBookEntry.BookEntryDetailsColl.push(det);
					});

					$scope.ChangeBookTitle();
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBookEntryById = function (refData) {

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
					BookEntryId: refData.BookEntryId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBookEntryList();
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