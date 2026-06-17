app.controller('HeightWeightController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Height Weight';

	//OnClickDefault();

	var glbS = GlobalServices;

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();


		$scope.currentPages = {
			HeightAndWeight: 1
		};

		$scope.searchData = {
			HeightAndWeight: ''
		};

		$scope.perPage = {
			HeightAndWeight: glbS.getPerPageRow()
		};


		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.post(base_url + "Examination/Creation/GetClassForClassTeacher")
			.then(function (data) {
				//$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				exDialog.openMessage({
					scope: $scope,
					title: $scope.Title,
					icon: "info",
					message: 'Failed: ' + reason
				});
			});

		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});



		$scope.newHeightAndWeight = {
			HeightAndWeightId: null,
			SelectedClass: null,
			ExamTypeId: null,
			StudentList: [],
			Mode: 'save'
		};


		$scope.SortAsList = [{ text: 'RollNo', value: 'RollNo' }, { text: 'Name', value: 'Name' }, { text: 'RegdNo', value: 'RegdNo' }, { text: 'SectionName', value: 'SectionName' },
		{ text: 'BoardRegdNo', value: 'BoardRegdNo' },
		{ text: 'Section & RollNo', value: 'SectionName,RollNo' },
		{ text: 'Section & Name', value: 'SectionName,Name' },
		{ text: 'Section & RegdNo', value: 'SectionName,RegdNo' },
		{ text: 'Section & BoardRegdNo', value: 'SectionName,BoardRegdNo' },
		];

	}

	function OnClickDefault() {

		document.getElementById('height-weight-form').style.display = "none";

		// Height and Weight Js

		document.getElementById('add-height-weight').onclick = function () {
			document.getElementById('height-weight-section').style.display = "none";
			document.getElementById('height-weight-form').style.display = "block";
		}
		document.getElementById('back-height-weight').onclick = function () {
			document.getElementById('height-weight-form').style.display = "none";
			document.getElementById('height-weight-section').style.display = "block";
		}


	}



	$scope.ClearHeightAndWeight = function () {
		$scope.newHeightAndWeight = {
			HeightAndWeightId: null,
			SelectedClass: null,
			ExamTypeId: null,
			StudentList: [],
			Mode: 'save'
		};

	}




	//*************************Height And Weight *********************************

	$scope.IsValidHeightAndWeight = function () {

		if (!$scope.newHeightAndWeight.ExamTypeId) {
			Swal.fire('Please ! Select ExamType');
			return false;
		}

		if (!$scope.newHeightAndWeight.SelectedClass) {
			Swal.fire('Please ! Select Class Name');
			return false;
		}


		return true;
	}

	$scope.GetClassWiseStudentHW = function () {

		$scope.newHeightAndWeight.StudentList = [];
		if ($scope.newHeightAndWeight.SelectedClass) {
			var para = {
				ClassId: $scope.newHeightAndWeight.SelectedClass.ClassId,
				SectionIdColl: $scope.newHeightAndWeight.SelectedClass.SectionId.toString(),
			};

			$http({
				method: 'POST',
				url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data && res1.data) {
					$scope.newHeightAndWeight.StudentList = res1.data;
					$timeout(function () {
						if ($scope.newHeightAndWeight.ExamTypeId) {
							var para1 = {
								classId: $scope.newHeightAndWeight.SelectedClass.ClassId,
								sectionId: $scope.newHeightAndWeight.SelectedClass.SectionId,
								examTypeId: $scope.newHeightAndWeight.ExamTypeId
							};
							$http({
								method: 'POST',
								url: base_url + "Examination/Creation/GetHeightWeightById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res2) {
								if (res2.data && res2.data) {
									var symDataColl = mx(res2.data);
									if (symDataColl) {
										angular.forEach($scope.newHeightAndWeight.StudentList, function (st) {
											var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
											if (dt) {
												st.Height = dt.Height;
												st.Weight = dt.Weight;
											}
										});
									}
								} else {
									Swal.fire(res2.data.ResponseMSG);
								}
							});
						}

					});


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}
	};

	$scope.SaveUpdateHeightAndWeight = function () {
		if ($scope.IsValidHeightAndWeight() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHeightAndWeight.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHeightAndWeight();
					}
				});
			} else
				$scope.CallSaveUpdateHeightAndWeight();

		}
	};

	$scope.CallSaveUpdateHeightAndWeight = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpData = [];

		angular.forEach($scope.newHeightAndWeight.StudentList, function (st) {

			var newData = {
				StudentId: st.StudentId,
				ExamTypeId: $scope.newHeightAndWeight.ExamTypeId,
				Weight: st.Weight,
				Height: st.Height
			}
			tmpData.push(newData);
		});
		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveHeightAndWeight",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: tmpData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHeightAndWeight();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelHeightAndWeightById = function (refData) {

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
					HeightAndWeightId: refData.HeightAndWeightId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Creation/DelHeightAndWeight",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHeightAndWeightList();
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