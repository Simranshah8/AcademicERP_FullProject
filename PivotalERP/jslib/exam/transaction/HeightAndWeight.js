app.controller('HeightAndWeightController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Height and Weight';

	//OnClickDefault();

	var glbS = GlobalServices;

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();


		$scope.currentPages = {
			HeightAndWeight: 1,
			HeightAndWeightTransfer: 1,
		};

		$scope.searchData = {		
			HeightAndWeight: '',
			HeightAndWeightTransfer: ''
		};

		$scope.perPage = {
			HeightAndWeight: glbS.getPerPageRow(),
			HeightAndWeightTransfer: glbS.getPerPageRow(),
		};

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

		$scope.ReExamTypeList = [];
		glbS.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newHeightAndWeightTransfer = {
			FromExamTypeId: null,
			ToExamTypeId: null
		};

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
		$scope.GetTranforHeightWeight();
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

	$scope.excludeSelectedExamType = function (item) {
		return item.id !== $scope.newSymbolNumberTransfer.FromExamTypeId;
	};
	
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
				url: base_url + "Academic/Transaction/GetClassWiseStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newHeightAndWeight.StudentList = res1.data.Data;
					$timeout(function () {

						if ($scope.newHeightAndWeight.ExamTypeId) {
							var para1 = {
								ClassId: $scope.newHeightAndWeight.SelectedClass.ClassId,
								SectionId: $scope.newHeightAndWeight.SelectedClass.SectionId,
								ExamTypeId: $scope.newHeightAndWeight.ExamTypeId
							};

							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetHeightWeightById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res2) {
								if (res2.data.IsSuccess && res2.data.Data) {
									var symDataColl = mx(res2.data.Data);

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
			url: base_url + "Exam/Transaction/SaveHeightAndWeight",
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
					url: base_url + "Exam/Transaction/DelHeightAndWeight",
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



	//*************************Height And Weight Transfor *********************************

	$scope.SelectedExamType = function (item) {
		return item.id !== $scope.newHeightAndWeightTransfer.FromExamTypeId;
	};

	$scope.TransforHeightWeight = function () {

		if ($scope.newHeightAndWeightTransfer.FromExamTypeId && $scope.newHeightAndWeightTransfer.ToExamTypeId) {
			Swal.fire({
				title: 'Do you want to transfer height and weight of selected examtype ?',
				showCancelButton: true,
				confirmButtonText: 'Transfer',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					var para = {
						FromExamTypeId: $scope.newHeightAndWeightTransfer.FromExamTypeId,
						ToExamTypeId: $scope.newHeightAndWeightTransfer.ToExamTypeId
					};

					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/TransforHeightWeight",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire(res.data.ResponseMSG);

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
		}
	};

	$scope.GetTranforHeightWeight = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HeightAndWeightTransferList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetTranforHeightWeight",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HeightAndWeightTransferList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTransforHW = function (refData) {
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
					url: base_url + "Exam/Transaction/DeleteTransforHeigthWeigthById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetTranforHeightWeight();
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