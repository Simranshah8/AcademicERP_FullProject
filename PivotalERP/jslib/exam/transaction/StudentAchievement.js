app.controller('StudentAchievementController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Achievement';

	

	$scope.LoadData = function () {
		var glbS = GlobalServices;
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.RemarksTypeList = [];
		$scope.RemarksTypeQry = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllRemarksTypeList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RemarksTypeList = res.data.Data;
				$scope.RemarksTypeQry = mx(res.data.Data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		

		$scope.newDet={
			RemarksColl: [],
			ForDate: new Date(),
			RemarksFor:1
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
					Remarks: '',
					Point:0
				})
			} else {
				$scope.newDet.RemarksColl.push({
					Remarks: '',
					Point: 0
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

	$scope.newDet = {}; // Initialize the newDet object
	$scope.selectedRowIndex = -1; // Initialize selectedRowIndex to track the selected row

	$scope.AddAchievementDet = function (A, index) {
		if ($scope.newDet.SelectedClass && $scope.newDet.ExamTypeId && $scope.newDet.RemarksTypeId) {
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
		$scope.StudentForAchievementColl = [];
		if ($scope.newDet.SelectedClass) {
			var para = {
				ClassId: $scope.newDet.SelectedClass.ClassId,
				SectionId: ($scope.newDet.SelectedClass.SectionId ? $scope.newDet.SelectedClass.SectionId : null),
			};
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentForAchievement",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentForAchievementColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			
		}
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
				RemarksFor:1
			};
			dataToSave.push(dataItem);
		}

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveStudentAchievement",
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
			/*	$scope.ClearAchievement();*/
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
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PrevAchievementlist = [];
		var para = {
			StudentId: $scope.newDet.StudentId,
			ExamTypeId: $scope.newDet.ExamTypeId
			//RemarksTypeId: $scope.newDet.RemarksTypeId,
		};
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetPrevAchievement",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PrevAchievementlist = res.data.Data;

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

	$scope.DelStudentAchievementById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Remarks + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteAchievement",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetPreviousAchievement();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});