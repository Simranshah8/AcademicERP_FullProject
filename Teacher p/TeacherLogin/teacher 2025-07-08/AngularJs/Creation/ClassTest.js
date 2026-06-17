String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('ClassTestController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Class Test';

	//OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		//$scope.ClassList = [];
		//GlobalServices.getClassList().then(function (res) {
		//	$scope.ClassList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		//$scope.SectionList = [];
		//GlobalServices.getSectionList().then(function (res) {
		//	$scope.SectionList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


		$scope.currentPages = {
			MarksEntry: 1,
			Summary: 1,

		};

		$scope.searchData = {
			MarksEntry: '',
			Summary: '',

		};

		$scope.perPage = {
			MarksEntry: GlobalServices.getPerPageRow(),
			Summary: GlobalServices.getPerPageRow(),


		};

		$scope.newMarksEntry = {
			MarksEntryId: null,
			MarksEntryDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarksEntry.MarksEntryDetailsColl.push({});

		$scope.newSummary = {
			SummaryId: null,
			SummaryDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newSummary.SummaryDetailsColl.push({});





		//$scope.GetAllMarksEntryList();
		//$scope.GetAllSummaryList();



	}



	$scope.ClearMarksEntry = function () {
		$scope.newMarksEntry = {
			MarksEntryId: null,
			MarksEntryDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarksEntry.MarksEntryDetailsColl.push({});
	}

	$scope.ClearSummary = function () {
		$scope.newSummary = {
			SummaryId: null,
			SummaryDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newSummary.SummaryDetailsColl.push({});
	}



	//*************************Marks Entry *********************************

	//$scope.IsValidMarksEntry = function () {
	//	if ($scope.newMarksEntry.Lesson.isEmpty()) {
	//		Swal.fire('Please ! Enter Lesson');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateMarksEntry = function () {
		if ($scope.IsValidMarksEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMarksEntry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarksEntry();
					}
				});
			} else
				$scope.CallSaveUpdateMarksEntry();

		}
	};

	$scope.CallSaveUpdateMarksEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveMarksEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newMarksEntry }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearMarksEntry();
				$scope.GetAllMarksEntryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllMarksEntryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MarksEntryList = [];

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllMarksEntryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MarksEntryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetMarksEntryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			MarksEntryId: refData.MarksEntryId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetMarksEntryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newMarksEntry = res.data.Data;
				$scope.newMarksEntry.Mode = 'Modify';

				document.getElementById('MarksEntry-content').style.display = "none";
				document.getElementById('MarksEntry-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelMarksEntryById = function (refData) {

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
					MarksEntryId: refData.MarksEntryId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelMarksEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllMarksEntryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Bulk Attendance *********************************

	//$scope.IsValidSummary = function () {
	//	if ($scope.newSummary.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter  Name');
	//		return false;
	//	}

	//	if ($scope.newSummary.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter  Description');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdateSummary = function () {
		if ($scope.IsValidSummary() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSummary.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSummary();
					}
				});
			} else
				$scope.CallSaveUpdateSummary();

		}
	};

	$scope.CallSaveUpdateSummary = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveSummary",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSummary }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSummary();
				$scope.GetAllSummaryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SummaryList = [];

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllSummaryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SummaryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSummaryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SummaryId: refData.SummaryId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetSummaryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSummary = res.data.Data;
				$scope.newSummary.Mode = 'Modify';

				document.getElementById('Summary-section').style.display = "none";
				document.getElementById('Summary-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSummaryById = function (refData) {

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
					SummaryId: refData.SummaryId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelSummary",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSummaryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});