app.controller('ExamReportController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'TimeTable';

	//OnClickDefault();

	$scope.LoadData = function () {

		$scope.PrintMark = {}
		$scope.ExamTypeColl = [];
		$http.get(base_url + "Student/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		
		$('.select2').select2();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Marking: 1,
			Grading:1

		};

		$scope.searchData = {
			Marking: '',
			Grading:''

		};

		$scope.perPage = {
			Marking: GlobalServices.getPerPageRow(),
			Grading: GlobalServices.getPerPageRow()

		};

		$scope.newMarking = {
			MarkingId: null,

		};

		$scope.newGrading = {
			GradingId: null,

		};
	}



	$scope.ClearMarking = function () {
		$scope.newMarking = {
			MarkingId: null,

		};
	}

	$scope.ClearGrading = function () {
		$scope.newGrading = {
			GradingId: null,

		};
	}

	$scope.ngChangeExamGrading = function () {
		$scope.Data = {};
		$scope.ExamType;
		$scope.GetObtainMarkGrading();

		$scope.Data.examTypeId = $scope.ExamType.ExamTypeId;

	}
	$scope.ngChangeExamMarking = function () {
		$scope.Data = {};
		$scope.ExamType;
		$scope.GetObtainMarkMarking();

		$scope.Data.examTypeId = $scope.ExamType.examTypeId;
	}

	$scope.GetObtainMarkGrading = function () {
		$scope.StatusLoading1 = false;
		$scope.ObtainMarkGradingLoading = 'Pls wait Loading Data'
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetObtainMark",
			data: $scope.Data,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.ObtainMarkGrading = res.data;
				$scope.StatusLoading1 = res.data.IsSuccess;
				if (res.data.IsSuccess == false) {
					$scope.ObtainMarkGradingLoading = res.data.ResponseMSG;
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetObtainMarkMarking = function () {

		$scope.ObtainMarkMarkingLoading = 'Pls wait Loading Data'
		$scope.StatusLoading = false;
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetObtainMark",
			data: $scope.Data,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.ObtainMarkMarking = res.data;
				$scope.StatusLoading = res.data.IsSuccess;
				if (res.data.IsSuccess == false) {
					$scope.ObtainMarkMarkingLoading = res.data.ResponseMSG;
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.PrintMarkSheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/PrintMarkSheet",
			data: $scope.Data,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.PrintMark = res.data;
				var Pdf = document.location.origin + '/' + $scope.PrintMark.ResponseMSG;
			}
			if ($scope.PrintMark.IsSuccess) {
				//window.open($scope.PrintMark.ResponseMSG, '_blank');
				document.body.style.cursor = 'wait';
				document.getElementById("frmMarkSheet").src = '';
				document.getElementById("frmMarkSheet").src = Pdf;
				document.body.style.cursor = 'default';
			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});