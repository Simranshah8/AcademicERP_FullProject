app.controller('PTMController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'PTM';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList =res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.PTMAttendByList = [
			{ id: 1, text: "None" },
			{ id: 2, text: "Father" },
			{ id: 3, text: "Mother" },
			{ id: 4, text: "Guardian" }
		];

		$scope.currentPages = {
			PTM: 1,

		};

		$scope.searchData = {
			PTM: '',
			Student: '',

		};

		$scope.perPage = {
			PTM: GlobalServices.getPerPageRow(),

		};

		$scope.newPTM = {
			TranId: null,
			ClassId: null,
			SectionId: null,
			PTMDate_TMP: new Date(),
			PTMBy: null,
			Description: '',
			StudentPTMColl: [],
			SelectedClass: null,
			EmployeeId: null,
			Mode: 'Save'
		};
		//$scope.newPTM.StudentPTMColl.push({
		//	StudentName: '',
		//	PTMAttendBy: '',
		//	TeacherRemarks: '',
		//	ParentRemarks: '',
		//	Recommendation:''
		//});
		$scope.GetAllPTMList();
		//$scope.GetAllStudentPTM();

	}

	function OnClickDefault() {

		document.getElementById('PTM-form').style.display = "none";

		//PTM section
		document.getElementById('add-PTM').onclick = function () {
			document.getElementById('PTM-section').style.display = "none";
			document.getElementById('PTM-form').style.display = "block";
			$scope.ClearPTM();
		}

		document.getElementById('back-to-list-PTM').onclick = function () {
			document.getElementById('PTM-form').style.display = "none";
			document.getElementById('PTM-section').style.display = "block";
			$scope.ClearPTM();
		}

	}

	$scope.ClearPTM = function () {
		$scope.newPTM = {
			TranId: null,
			ClassId: null,
			SectionId: null,
			PTMDate_TMP: new Date(),
			PTMBy: null,
			Description: '',
			StudentPTMColl: [],
			SelectedClass: null,
			EmployeeId: null,
			Mode: 'Save'
		};
		
		//$scope.newPTM.StudentPTMColl.push({
		//	StudentName: '',
		//	PTMAttendBy: '',
		//	TeacherRemarks: '',
		//	ParentRemarks: '',
		//	Recommendation: ''
		//});
	}

	//*************************PTM *********************************

	$scope.IsValidPTM = function () {
		if (!$scope.newPTM.SelectedClass) {
			Swal.fire('Please select Class/Section');
			return false;
		}

		// PTM Date
		if (!$scope.newPTM.PTMDate_TMP) {
			Swal.fire('Please select PTM Date');
			return false;
		}

		// PTM By (Employee)
		if (!$scope.newPTM.EmployeeId || $scope.newPTM.EmployeeId == 0) {
			Swal.fire('Please select PTM By');
			return false;
		}
		if (!$scope.newPTM.StudentPTMColl || $scope.newPTM.StudentPTMColl.length === 0) {
			Swal.fire('No student records found. Cannot save.');
			return false;
		}
		
		return true;
	}

	$scope.SaveUpdatePTM = function () {
		if ($scope.IsValidPTM() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPTM.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePTM();
					}
				});
			} else
				$scope.CallSaveUpdatePTM();

		}
	};

	$scope.CallSaveUpdatePTM = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newPTM.ClassId = $scope.newPTM.SelectedClass.ClassId;
		$scope.newPTM.SectionId = $scope.newPTM.SelectedClass.SectionId;
		$scope.newPTM.SectionId = $scope.newPTM.SelectedClass.SectionId;
		$scope.newPTM.PTMBy = $scope.newPTM.EmployeeId;

		if ($scope.newPTM.PTMDateDet)
			$scope.newPTM.PTMDate = $filter('date')(new Date($scope.newPTM.PTMDateDet.dateAD), 'yyyy-MM-dd');
		else
			$scope.newPTM.PTMDate = null;

		$scope.newPTM.StudentPTMColl.forEach(function (row) {
			row.StudentId = row.StudentId;
			row.PTMAttendBy = row.PTMAttendBy;
			row.TeacherRemarks = row.TeacherRemarks;
			row.ParentRemarks = row.ParentRemarks;
			row.Recommendation = row.Recommendation;
		});
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveParentTeacherMeeting",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPTM }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPTM();
				$scope.GetAllPTMList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
			
	$scope.GetAllPTMList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PTMList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllParentTeacherMeeting",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PTMList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	//$scope.GetPTMById = function (refData) {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	var para = {
	//		TranId: refData.TranId
	//	};
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Creation/GetParentTeacherMeetingById",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.newPTM = res.data.Data;
	//			if ($scope.newPTM.PTMBy)
	//				$scope.newPTM.EmployeeId = $scope.newPTM.PTMBy;
	//			if ($scope.newPTM.PTMDate)
	//				$scope.newPTM.PTMDate_TMP = new Date($scope.newPTM.PTMDate);
	//			angular.forEach($scope.ClassList.SectionList, function (section) {
	//				if (section.ClassId === $scope.newPTM.ClassId && section.SectionId === $scope.newPTM.SectionId) {
	//					$scope.newPTM.SelectedClass = section;
	//				}
	//			});
	//			$scope.newPTM.Mode = 'Modify';
	//			document.getElementById('PTM-section').style.display = "none";
	//			document.getElementById('PTM-form').style.display = "block";
	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};

	$scope.GetPTMById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.PTMDet = null;
        var para = {
			ClassId: refData.ClassId,
			SectionId: refData.SectionId,
			PTMBy: refData.PTMBy || null,
			PTMDate: refData.PTMDate,
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllStudentPTM",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newPTM = res.data.Data;
                $scope.newPTM.StudentPTMColl = res.data.Data.StudentPTMColl;
				$scope.newPTM.Description = res.data.Data.Description;
				$scope.newPTM.EmployeeId = res.data.Data.PTMBy;
				if ($scope.newPTM.PTMDate) {
					$scope.newPTM.PTMDate_TMP = new Date($scope.newPTM.PTMDate);
				}

				if ($scope.ClassList && $scope.ClassList.SectionList) {
					for (var i = 0; i < $scope.ClassList.SectionList.length; i++) {
						var cl = $scope.ClassList.SectionList[i];
						if (cl.ClassId == res.data.Data.ClassId &&
							cl.SectionId == res.data.Data.SectionId) {

							$scope.newPTM.SelectedClass = cl;
							break;
						}
					}
				}

                angular.forEach($scope.newPTM.StudentPTMColl, function (studentptm) {
                    if (studentptm.TranId) {
                        $scope.newPTM.TranId = studentptm.TranId;
                        $scope.newPTM.Mode = 'Modify';
                    }
				});
				document.getElementById('PTM-section').style.display = "none";
				document.getElementById('PTM-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }


    $scope.DelPTMById = function (refData) {

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
					url: base_url + "Academic/Creation/DelParentTeacherMeeting",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetAllPTMList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.IsValidPTMGet = function () {
		//let reason = '';
		//if (!$scope.newPTM.SelectedClass || !$scope.newPTM.SelectedClass.ClassId) {
		//	reason = 'Class';
		//} 
		//if (reason) {
		//	Swal.fire('Please! Select ' + reason);
		//	return false;
		//}
		return true;
	};


    $scope.GetAllStudentPTM = function () {
		if ($scope.newPTM.SelectedClass?.ClassId && $scope.newPTM.EmployeeId && ($scope.newPTM.PTMDateDet?.dateAD || $scope.newPTM.PTMDateDet)) {

			$scope.loadingstatus = "running";
            showPleaseWait();
            $scope.PTMDet = null;

            var para = {
                ClassId: $scope.newPTM.SelectedClass.ClassId,
                SectionId: $scope.newPTM.SelectedClass.SectionId,
                PTMBy: $scope.newPTM.EmployeeId || null,
                PTMDate: $filter('date')(new Date($scope.newPTM.PTMDateDet.dateAD), 'yyyy-MM-dd'),
            };
            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/GetAllStudentPTM",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.newPTM.StudentPTMColl = res.data.Data.StudentPTMColl;
					$scope.newPTM.Description = res.data.Data.Description;

					angular.forEach($scope.newPTM.StudentPTMColl, function (studentptm) {
						if (studentptm.TranId) {
							$scope.newPTM.TranId = studentptm.TranId;
							$scope.newPTM.Mode = 'Modify';
						}
					});

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        }
    }


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});