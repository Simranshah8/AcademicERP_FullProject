
app.controller('BookIssueController', function ($scope, $http, $timeout, $filter, GlobalServices, $translate) {
	$scope.Title = 'Book Issue';

	$scope.LoadData = function () {

		$scope.Labels = {
			RegdNo: ''
		};

		//$translate.getTranslationTable()['REGDNO_LNG']
		$translate('REGDNO_LNG').then(function (data) {
			$scope.Labels.RegdNo = data;
		});

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.BookSearchOptions = [{ text: 'AccessionNo', value: 'BD.AccessionNo' }, { text: 'Book Title', value: 'BD.BookTitle' }, { text: 'Subject', value: 'BD.Subject' }, { text: 'BookNo', value: 'BD.BookNo' }, { text: 'CallNo', value: 'BD.CallNo' }];
		$scope.IssueToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Teacher' }];

		$scope.newBookIssue = {
			SelectBook: $scope.BookSearchOptions[0].value,
			BookDetails: null,
			SideBarData: null,
			BookEntryId: 0,
			BookIssueId: null,
			IssueNo: 0,
			IssueDate_TMP: null,
			IssueTo: 1,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			IssueDate_TMP: new Date(),
			PreviousBookDetailsColl: [],
			DetailsColl: [],
			Barcode: '',
			Mode: 'Save'
		};
		$scope.newBookIssue.PreviousBookDetailsColl.push({});



		$scope.GetBookIssueNo();
		//$scope.GetAllBookIssueList();

	};

	$scope.ShowBookDetails = function () {

		$timeout(function () {
			$scope.newBookIssue.BookDetails = $scope.newBookIssue.BookDetails;
		});
	};
	$scope.AddBookForIssue = function () {

		if ($scope.newBookIssue.Barcode && $scope.newBookIssue.Barcode.length > 0) {
			$scope.getBookDetailsByBarcode();
			return;
		}

		$timeout(function () {
			if ($scope.CurCreditRule && $scope.newBookIssue.BookDetails) {
				var totalIssue = $scope.PreviousBookDetailsColl.length + $scope.newBookIssue.DetailsColl.length;

				if ($scope.CurCreditRule.BookLimit <= totalIssue) {
					Swal.fire('Book Issues Limit exceed (' + $scope.CurCreditRule.BookLimit + ')');
				} else {
					$timeout(function () {
						$scope.newBookIssue.DetailsColl.push({
							BookDet: $scope.newBookIssue.BookDetails,
							BookEntryId: $scope.newBookIssue.BookDetails.TranId,
							CreditDays: $scope.CurCreditRule.CreditDays,
							IssueRemarks: ''
						});
						$scope.newBookIssue.BookEntryId = null;
					});
				}

			} else {
				Swal.fire('Please ! 1st setup credit rules.')
			}
		});

	};
	$scope.CurDues = null;
	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
	$scope.getDuesDetails = function () {
		$scope.CurDues = null;
		$scope.CurCreditRule = null;
		$scope.PreviousBookDetailsColl = [];
		if ($scope.newBookIssue.StudentId && $scope.newBookIssue.StudentId > 0) {
			var para = {
				StudentId: $scope.newBookIssue.StudentId,
				PaidUpToMonth: 0,
				PaidUpMonthColl: '',
				SemesterId: ($scope.newBookIssue.StudentDetails ? $scope.newBookIssue.StudentDetails.SemesterId : null),
				ClassYearId: ($scope.newBookIssue.StudentDetails ? $scope.newBookIssue.StudentDetails.ClassYearId : null)
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetDuesForReceipt",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CurDues = res.data.Data;
					bookLimit = $scope.CurDues.BookLimit;

					if (!$scope.CurDues.PhotoPath || $scope.CurDues.PhotoPath.length == 0)
						$scope.CurDues.PhotoPath = $scope.DefaultPhoto;

					$timeout(function () {
						$scope.getPreviousBookList();
						$scope.newBookIssue.DetailsColl = [];
					});
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			var para1 = {
				StudentId: $scope.newBookIssue.StudentId,
				EmployeeId: null,
			};
			$http({
				method: 'POST',
				url: base_url + "Library/Master/GetCreditRules",
				dataType: "json",
				data: JSON.stringify(para1)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CurCreditRule = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}

	}

	$scope.getPreviousBookList = function () {
		$scope.PreviousBookDetailsColl = [];
		var para = {
			StudentId: $scope.newBookIssue.StudentId,
			EmployeeId: $scope.newBookIssue.EmployeeId,
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

	$scope.getBookDetailsByBarcode = function () {
		var para = {
			barCode: $scope.newBookIssue.Barcode
		};
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookDetailsByBarcode",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var bookDetails = res.data.Data;
				$scope.newBookIssue.BookDetails = bookDetails;


				$timeout(function () {
					if ($scope.CurCreditRule && $scope.newBookIssue.BookDetails) {
						var totalIssue = $scope.PreviousBookDetailsColl.length + $scope.newBookIssue.DetailsColl.length;

						if ($scope.CurCreditRule.BookLimit < totalIssue) {
							Swal.fire('Book Issues Limit exceed (' + $scope.CurCreditRule.BookLimit + ')');
						} else {
							$timeout(function () {
								$scope.newBookIssue.DetailsColl.push({
									BookDet: $scope.newBookIssue.BookDetails,
									BookEntryId: $scope.newBookIssue.BookDetails.TranId,
									CreditDays: $scope.CurCreditRule.CreditDays,
									IssueRemarks: ''
								});
								$scope.newBookIssue.BookEntryId = null;
							});
						}

					} else {
						Swal.fire('Please ! 1st setup credit rules.')
					}
				});

			} else
				alert('Book not found');

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.getTeachCreditRule = function () {

        $timeout(function () {
            $scope.getPreviousBookList();
            $scope.newBookIssue.DetailsColl = [];
        });

		$timeout(function () {
			var para1 = {
				StudentId: null,
				EmployeeId: $scope.newBookIssue.EmployeeId,
			};
			$http({
				method: 'POST',
				url: base_url + "Library/Master/GetCreditRules",
				dataType: "json",
				data: JSON.stringify(para1)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CurCreditRule = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
	};
	$scope.ClearBookIssue = function () {

		$timeout(function () {
			$scope.PreviousBookDetailsColl = [];
			$scope.newBookIssue = {
				SelectBook: $scope.BookSearchOptions[0].value,
				BookDetails: null,
				SideBarData: null,
				BookEntryId: 0,
				BookIssueId: null,
				IssueNo: 0,
				IssueDate_TMP: null,
				IssueTo: 1,
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				IssueDate_TMP: new Date(),
				PreviousBookDetailsColl: [],
				DetailsColl: [],
				Mode: 'Save'
			};
			$scope.newBookIssue.PreviousBookDetailsColl.push({});
			$scope.GetBookIssueNo();
			$scope.CurDues = null;
		});

	};
	$scope.ChangeIssueTo = function (beData) {
		$timeout(function () {
			$scope.PreviousBookDetailsColl = [];
			$scope.newBookIssue = {
				SelectBook: $scope.BookSearchOptions[0].value,
				BookDetails: null,
				SideBarData: null,
				BookEntryId: 0,
				BookIssueId: null,
				IssueNo: 0,
				IssueDate_TMP: null,
				IssueTo: beData,
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				IssueDate_TMP: new Date(),
				PreviousBookDetailsColl: [],
				DetailsColl: [],
				Mode: 'Save'
			};
			$scope.newBookIssue.PreviousBookDetailsColl.push({});
			$scope.GetBookIssueNo();
			$scope.CurDues = null;
		});
    }
	$scope.delBookIssueDetails = function (ind) {
		if ($scope.newBookIssue.DetailsColl) {
			if ($scope.newBookIssue.DetailsColl.length > 0) {
				$scope.newBookIssue.DetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.IsValidBookIssue = function () {
		var valid = true;

		if (!$scope.CurCreditRule) {
			Swal.fire('Please ! 1st setup credit rules.')
			return false;
		}

		if (!$scope.newBookIssue.DetailsColl || $scope.newBookIssue.DetailsColl.length == 0) {
			Swal.fire('Please ! Add Book Issue Details')
			return false;
		}

		return valid;
	};

	$scope.SaveUpdateBookIssue = function () {
		if ($scope.IsValidBookIssue() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBookIssue.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBookIssue();
					}
				});
			} else
				$scope.CallSaveUpdateBookIssue();

		}
	};

	$scope.CallSaveUpdateBookIssue = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBookIssue.IssueTo == 1)
			$scope.newBookIssue.EmployeeId = null;
		else
			$scope.newBookIssue.StudentId = null;

		if ($scope.newBookIssue.DateDet) {
			$scope.newBookIssue.IssueDate = $filter('date')(new Date($scope.newBookIssue.DateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newBookIssue.IssueDate = new Date();

		angular.forEach($scope.newBookIssue.DetailsColl, function (dc) {
			dc.IssueDate = $scope.newBookIssue.IssueDate;
		});

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveBookIssue",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBookIssue }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBookIssue();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	};

	$scope.GetBookIssueNo = function () {

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookIssueNo",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.newBookIssue.IssueNo = res.data.Data.RId;
				});
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllBookIssueList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BookIssueList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookIssueList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookIssueList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetBookIssueById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BookIssueId: refData.BookIssueId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookIssueById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBookIssue = res.data.Data;
				$scope.newBookIssue.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBookIssueById = function (refData) {

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
					BookIssueId: refData.BookIssueId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookIssue",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBookIssueList();
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