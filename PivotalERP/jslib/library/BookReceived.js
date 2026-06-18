
app.controller('BookReceivedController', function ($scope, $http, $timeout, $filter, GlobalServices, $translate) {
	$scope.Title = 'Book Received';

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
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.newBookReceived = {
			BookReceivedId: null,
			ReceiveNo: 0,
			ReceivedDate_TMP: new Date(),
			ReceivedFrom: 1,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			BookReceivedDetailsColl: [],
			ReceivedBookDetailsColl: [],
			RenewBookDetailsColl: [],
			Barcode: '',
			Mode: 'Save'
		};

		//$scope.GetAllBookReceivedList();

	};

	$scope.ShowBookDetails = function () {

		$timeout(function () {
			$scope.newBookIssue.BookDetails = $scope.newBookIssue.BookDetails;
		});
	};
	$scope.CurDues = null;
	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
	$scope.getDuesDetails = function () {
		$scope.CurDues = null;
		$scope.PreviousBookDetailsColl = [];
		if ($scope.newBookReceived.StudentId && $scope.newBookReceived.StudentId > 0) {
			var para = {
				StudentId: $scope.newBookReceived.StudentId,
				PaidUpToMonth: 0,
				PaidUpMonthColl: '',
				SemesterId: ($scope.newBookReceived.StudentDetails ? $scope.newBookReceived.StudentDetails.SemesterId : null),
				ClassYearId: ($scope.newBookReceived.StudentDetails ? $scope.newBookReceived.StudentDetails.ClassYearId : null)
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
		if (forST == 1)
			$scope.newBookReceived.EmployeeId = null;
		else
			$scope.newBookReceived.StudentId = null;

		$scope.newBookReceived.ReceivedBookDetailsColl = [];
		$scope.newBookReceived.RenewBookDetailsColl = [];
		$scope.PreviousBookDetailsColl = [];
		var para = {
			StudentId: $scope.newBookReceived.StudentId,
			EmployeeId: $scope.newBookReceived.EmployeeId,
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
				$timeout(function () {
					$scope.newBookIssue.BookDetails = bookDetails;
				});

			} else
				alert('Book not found');

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.ClearBookReceived = function () {

		$timeout(function () {

			$scope.PreviousBookDetailsColl = [];

			$scope.newBookReceived = {
				BookReceivedId: null,
				ReceiveNo: 0,
				ReceivedDate_TMP: new Date(),
				ReceivedFrom: 1,
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				BookReceivedDetailsColl: [],
				ReceivedBookDetailsColl: [],
				RenewBookDetailsColl: [],
				Mode: 'Save'
			};
		});
	};

	$scope.ChangeReceiveFrom = function (beData) {
		$timeout(function () {
			$scope.PreviousBookDetailsColl = [];
			$scope.newBookReceived = {
				BookReceivedId: null,
				ReceiveNo: 0,
				ReceivedDate_TMP: new Date(),
				ReceivedFrom: beData,
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				BookReceivedDetailsColl: [],
				ReceivedBookDetailsColl: [],
				RenewBookDetailsColl: [],
				Mode: 'Save'
			};
		});
	}

	//column details Add for BookReceived
	$scope.AddReceivedBookDetails = function (ind, bd) {
		$timeout(function () {
			$scope.newBookReceived.ReceivedBookDetailsColl.push({
				BookEntryId: bd.BookEntryId,
				IssueId: bd.IssueId,
				BookDet: bd,
				ReceivedType: 1,
				CreditDays: bd.CreditDays,
				FineAmount: bd.FineAmt
			});
			$scope.newBookReceived.BookDetails = bd;
			if ($scope.PreviousBookDetailsColl) {
				if ($scope.PreviousBookDetailsColl.length > 0) {
					$scope.PreviousBookDetailsColl.splice(ind, 1);
				}
			}
		});


	};
	$scope.delReceivedBookDetails = function (ind) {
		if (!$scope.newBookReceived.ReceivedBookDetailsColl ||
			$scope.newBookReceived.ReceivedBookDetailsColl.length === 0) {
			return;
		}
		var deletedItem = $scope.newBookReceived.ReceivedBookDetailsColl[ind];
		if (!$scope.PreviousBookDetailsColl) {
			$scope.PreviousBookDetailsColl = [];
		}
		$scope.PreviousBookDetailsColl.push(deletedItem.BookDet);
		$scope.newBookReceived.ReceivedBookDetailsColl.splice(ind, 1);
	};


	//column details Add for RenewBook
	$scope.AddRenewBookDetails = function (ind, bd) {

		$timeout(function () {
			$scope.newBookReceived.RenewBookDetailsColl.push({
				BookEntryId: bd.BookEntryId,
				IssueId: bd.IssueId,
				BookDet: bd,
				ReceivedType: 2,
				CreditDays: bd.CreditDays,
				FineAmount: bd.FineAmt
			});

			$scope.newBookReceived.BookDetails = bd;

			if ($scope.PreviousBookDetailsColl) {
				if ($scope.PreviousBookDetailsColl.length > 0) {
					$scope.PreviousBookDetailsColl.splice(ind, 1);
				}
			}
		});

	};

	$scope.delRenewBookDetails = function (ind) {
		if (!$scope.newBookReceived.RenewBookDetailsColl ||
			$scope.newBookReceived.RenewBookDetailsColl.length === 0) {
			return;
		}
		var deletedItem = $scope.newBookReceived.RenewBookDetailsColl[ind];
		if (!$scope.PreviousBookDetailsColl) {
			$scope.PreviousBookDetailsColl = [];
		}
		$scope.PreviousBookDetailsColl.push(deletedItem.BookDet);
		$scope.newBookReceived.RenewBookDetailsColl.splice(ind, 1);
	};

	//************************* Book Received *********************************

	$scope.IsValidBookReceived = function () {
		var isvalid = true;

		return isvalid;
	}

	$scope.SaveUpdateBookReceived = function () {
		if ($scope.IsValidBookReceived() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBookReceived.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBookReceived();
					}
				});
			} else
				$scope.CallSaveUpdateBookReceived();

		}
	};

	$scope.CallSaveUpdateBookReceived = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBookReceived.ReceivedFrom == 1)
			$scope.newBookReceived.EmployeeId = null;
		else
			$scope.newBookReceived.StudentId = null;

		var recDate = new Date();
		if ($scope.newBookReceived.DateDet) {
			recDate = $filter('date')(new Date($scope.newBookReceived.DateDet.dateAD), 'yyyy-MM-dd');
		}


		var dataColl = [];
		angular.forEach($scope.newBookReceived.ReceivedBookDetailsColl, function (rd) {
			dataColl.push({
				IssueId: rd.IssueId,
				ReturnDate: recDate,
				ReturnRemarks: rd.ReturnRemarks,
				ReceivedType: 1,
				CreditDays: 0,
				FineAmount: rd.FineAmount
			});
		});
		angular.forEach($scope.newBookReceived.RenewBookDetailsColl, function (rd) {
			dataColl.push({
				IssueId: rd.IssueId,
				ReturnDate: recDate,
				RenewalDate: recDate,
				RenewalRemarks: rd.RenewalRemarks,
				ReceivedType: 2,
				CreditDays: rd.CreditDays,
				FineAmount: rd.FineAmount
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveBookReceived",
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
				$scope.ClearBookReceived();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});




	};

	$scope.GetAllBookReceivedList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BookReceivedList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookReceivedList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookReceivedList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetBookReceivedById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BookReceivedId: refData.BookReceivedId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookReceivedById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBookReceived = res.data.Data;
				$scope.newBookReceived.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBookReceivedById = function (refData) {

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
					BookReceivedId: refData.BookReceivedId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookReceived",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBookReceivedList();
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