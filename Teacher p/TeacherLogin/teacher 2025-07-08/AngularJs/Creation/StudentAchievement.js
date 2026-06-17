app.controller('StudentAchievementController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Achievement';



	$scope.LoadData = function () {
		var glbS = GlobalServices;
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				exDialog.openMessage({
					scope: $scope,
					title: $scope.Title,
					icon: "info",
					message: 'Failed: ' + reason
				});
			});

		$scope.RemarksTypeColl = [];
		$http.get(base_url + "StudentRecord/Creation/GetRemarksTypeList")
			.then(function (data) {
				$scope.RemarksTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		$scope.newDet = {
			RemarksColl: [],
			ForDate: new Date(),
			RemarksFor: 1
		}
		$scope.newDet.RemarksColl.push({});
	}

	$scope.ClearAchievement = function () {
		$scope.newDet = {
			RemarksColl: [],
		};
		$scope.newDet.RemarksColl.push({});
	}

	$scope.AddRemarks = function (ind) {
		if ($scope.newDet.RemarksColl) {
			if ($scope.newDet.RemarksColl.length > ind + 1) {
				$scope.newDet.RemarksColl.splice(ind + 1, 0, {
					Remarks: ''
				})
			} else {
				$scope.newDet.RemarksColl.push({
					Remarks: ''
				})
			}
		}
	};
	$scope.delRemarks = function (ind) {
		if ($scope.newDet.RemarksColl) {
			if ($scope.newDet.RemarksColl.length > 1) {
				$scope.newDet.RemarksColl.splice(ind, 1);
			}
		}
	};

	$scope.newDet = {}; 
	$scope.selectedRowIndex = -1; 

	$scope.AddAchievementDet = function (A, index) {
		if ($scope.newDet.ClassId.ClassId && $scope.newDet.ExamTypeId && $scope.newDet.RemarksTypeId) {
			$scope.newDet.Name = A.Name;
			$scope.newDet.ClassName = A.ClassName; // Assuming A.ClassName exists
			$scope.newDet.SectionName = A.SectionName;
			$scope.newDet.RollNo = A.RollNo;
			$scope.newDet.StudentId = A.StudentId;
			$scope.selectedRowIndex = index;
			$scope.GetPreviousAchievement();
		} else {
			Swal.fire('Please select Exam Type and Remarks Type, and then select the student!');
		}
	};

	
	$scope.GetClassWiseStudent = function () {
		$scope.loadingstatus = "running";
		$scope.StudentForAchievementColl = [];
	/*	var secCount = mx($scope.SectionClassColl).count(p1 => p1.ClassId == $scope.newDet.ClassId && p1.SectionId > 0);*/
		var para = {
			classId: $scope.newDet.ClassId.ClassId,
			SectionId: ($scope.newDet.ClassId.SectionId ? $scope.newDet.ClassId.SectionId : null),
		}

		$http({
			method: 'POST',
			url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
			data: JSON.stringify(para),
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.StudentForAchievementColl = res.data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.IsValidStudentAchievement = function () {
		var isInvalid = false;
		angular.forEach($scope.newDet.RemarksColl, function (S) {
			if (!S.Remarks || S.Remarks.trim() === '') {
				isInvalid = true;
				return;
			}
		});

		if (isInvalid) {
			Swal.fire('Please enter Achievement for empty rows');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateStudentAchievement = function () {
		if ($scope.IsValidStudentAchievement() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentAchievement();
					}
				});
			} else
				$scope.CallSaveUpdateStudentAchievement();
		}
	};

	$scope.CallSaveUpdateStudentAchievement = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var examtype = $scope.newDet.ExamTypeId;
		var remarkstype = $scope.newDet.RemarksTypeId;
		var studentid = $scope.newDet.StudentId;
		var fordate = $scope.newDet.ForDate;
		var dataToSave = [];
		for (var i = 0; i < $scope.newDet.RemarksColl.length; i++) {
			var S = $scope.newDet.RemarksColl[i];
			var remarks = S.Remarks;
			var point = S.Point;
			var remarks = S.Remarks;
			var dataItem = {
				StudentId: studentid,
				Remarks: remarks,
				Point: point,
				ExamTypeId: examtype,
				RemarksTypeId: remarkstype,
				ForDate: fordate,
				RemarksFor: 1
			};
			dataToSave.push(dataItem);
		}

		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveStudentAchievement",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {

				$scope.GetPreviousAchievement();
				/*$scope.ClearAchievement();*/
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.checkAndGetPreviousAchievement = function () {
		if ($scope.newDet.StudentId && $scope.newDet.ExamTypeId && $scope.newDet.RemarksTypeId) {
			$scope.GetPreviousAchievement();
		}
	};

	$scope.GetPreviousAchievement = function () {

		if (!$scope.newDet)
			return;

		if (!$scope.newDet.StudentId)
			return;

		if (!$scope.newDet.ExamTypeId)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PrevAchievementlist = [];
		var para = {
			studentId: $scope.newDet.StudentId,
			examTypeId: $scope.newDet.ExamTypeId
		};
		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/GetPrevAchievement",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.PrevAchievementlist = res.data;

				$scope.newDet.RemarksColl = [];

				// Push an empty row to RemarksColl after clearing
				$scope.newDet.RemarksColl.push({
					Remarks: '',
					Point: ''
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});