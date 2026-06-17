

app.controller('MarkSubmitStatusController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Mark Submit Status';

	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();

		
		//$scope.BranchColl = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Setup/Security/GetAllBranchListForEntry",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.BranchColl = res.data.Data;
		//	}
		//}, function (reason) {
		//	alert('Failed' + reason);
		//});
		

		//$scope.ExamTypeList = [];
		//gSrv.getExamTypeList().then(function (res) {
		//	$scope.ExamTypeList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.ExamTypeList = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeList = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.StudentSearchOptions = gSrv.getStudentSearchOptions();

		$scope.currentPages = {
			
			MarkSubmittedStatus: 1,
			MarkSubmittedStatusPending: 1
		};

		$scope.searchData = {			
			MarkSubmittedStatus: '',
			MarkSubmittedStatusPending: ''
		};

		$scope.perPage = {
			MarkSubmittedStatus: gSrv.getPerPageRow(),
			MarkSubmittedStatusPending: gSrv.getPerPageRow()
		};

		$scope.newClassWise = {
			ClassWiseId: null,
			Mode: 'Save'
		};		

		$scope.newMarkSubmittedStatus = {
			MarkSubmittedStatusId: null,
			Mode: 'Save'
		};

		$scope.nt = {
			Title: '',
			Description: ''
		};

		//$scope.GetAllClassList();
		//$scope.GetAllSectionList();
		//$scope.GetAllStudentWiseList();
		//$scope.GetAllMarkSubmittedStatusList();

	}
	$scope.GetBranchId = function (bid) {
		if ($scope.BranchColl.length == 1)
			return $scope.BranchColl[0].BranchId
		else
			return bid;
	}
	
	$scope.sortPending = function (keyname) {
		$scope.sortKeyPending = keyname;   //set the sortKey to the param passed
		$scope.reversePending = !$scope.reversePending; //if true make it false and vice versa
	}
	$scope.sortSubmit = function (keyname) {
		$scope.sortKeySubmit = keyname;   //set the sortKey to the param passed
		$scope.reverseSubmit = !$scope.reverseSubmit; //if true make it false and vice versa
	}

	$scope.ClearMarkSubmittedStatus = function () {
		$scope.newMarkSubmittedStatus = {
			MarkSubmittedStatusId: null,

			Mode: 'Save'
		};
	}


	//************************* Mark Submitted Status  *********************************

	$scope.GetAllMarkSubmittedStatusList = function () {

		if ($scope.newMarkSubmittedStatus && $scope.newMarkSubmittedStatus.ExamTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.PendingMarkList = [];
			$scope.SubmitMarkList = [];

			var para = {
				ExamTypeId: $scope.newMarkSubmittedStatus.ExamTypeId			
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Transaction/GetMarkSubmit",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					angular.forEach(res.data, function (d) {
						if (d.IsPending == true)
							$scope.PendingMarkList.push(d);
						else
							$scope.SubmitMarkList.push(d);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.SendNotificationToTeacher = function () {
		if ($scope.nt.Title && $scope.nt.Description) {
			if ($scope.nt.Title.length > 0 && $scope.nt.Description.length > 0) {
				var tmpDataColl = [];
				angular.forEach($scope.PendingMarkList, function (d) {

					if (d.UserId > 0) {
						var newSMS = {
							EntityId: entityMarkEntry,
							StudentId: 0,
							UserId: d.UserId,
							Title: $scope.nt.Title,
							Message: $scope.nt.Description,
							ContactNo: d.TeacherContactNo,
							StudentName: d.SubjectTeacherName
						};
						tmpDataColl.push(newSMS);
					}
				});

				$http({
					method: 'POST',
					url: base_url + "Global/SendNotificationToStudent",
					dataType: "json",
					data: JSON.stringify(tmpDataColl)
				}).then(function (sRes) {
					Swal.fire(sRes.data.ResponseMSG);
				});

			}
		}

	};

});