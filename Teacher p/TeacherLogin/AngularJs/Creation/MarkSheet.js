app.controller('marksheetController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Mark Sheet';

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.markSheet = {
			SelectClassSection: null,
			ExamTypeId:null
		};
		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		$scope.ExamTypeGroupColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeGroupList")
			.then(function (data) {
				$scope.ExamTypeGroupColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


	}



	//************************* MarkSheet *********************************

	$scope.GetMarkSheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();		
		if ($scope.markSheet.SelectClassSection && $scope.markSheet.ExamTypeId) {
			var para = {
				ExamTypeId: $scope.markSheet.ExamTypeId,
				ClassId: $scope.markSheet.SelectClassSection.ClassId,
				SectionId: $scope.markSheet.SelectClassSection.SectionId
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Reporting/GetMarkSheet",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess == true)
				{
					document.body.style.cursor = 'wait';
					document.getElementById("frmMarkSheet").src = '';
					document.getElementById("frmMarkSheet").src = res.data.ResponseMSG;
					document.body.style.cursor = 'default';
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	
	$scope.GetGroupMarkSheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.markSheetGroup.SelectClassSection && $scope.markSheetGroup.ExamTypeGroupId) {
			var para = {
				ExamTypeGroupId: $scope.markSheetGroup.ExamTypeGroupId,
				ClassId: $scope.markSheetGroup.SelectClassSection.ClassId,
				SectionId: $scope.markSheetGroup.SelectClassSection.SectionId
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Reporting/GetGroupMarkSheet",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess == true) {
					document.body.style.cursor = 'wait';
					document.getElementById("frmGroupMarkSheet").src = '';
					document.getElementById("frmGroupMarkSheet").src = res.data.ResponseMSG;
					document.body.style.cursor = 'default';
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
});