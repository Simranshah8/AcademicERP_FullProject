app.controller('HomeworkController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Homework';

	OnClickDefault();
	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		$scope.MonthList = glbS.getMonthList();

		$scope.ClassList = [];
		glbS.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			HomeworkType: 1,
			AddHomework: 1,
			HomeworkList: 1

		};

		$scope.searchData = {
			HomeworkType: '',
			AddHomework: '',
			HomeworkList: '',
			FromDate_TMP: new Date(),
			ToDate_TMP:new Date()
		};

		$scope.perPage = {
			HomeworkType: glbS.getPerPageRow(),
			AddHomework: glbS.getPerPageRow(),
			HomeworkList: glbS.getPerPageRow(),

		};

		$scope.newHomeworkType = {
			HomeworkTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newAddHomework = {
			AddHomeworkId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Lesson: '',
			Topic: '',
			HomeworkTypeId: null,
			HomeworkDescription: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineForReDo: null,
			DeadlineForReDoTime: null,
			File: null,
			Mode: 'Save'
		};

		$scope.newHomeworkList = {
			HomeworkListId: null,
			HomeworkListDetailColl: [],
			Mode: 'Save'
		};
		$scope.newHomeworkList.HomeworkListDetailColl.push({});
		



		$scope.GetAllHomeworkTypeList();
		//$scope.GetAllAddHomeworkList();
		//$scope.GetAllHomeworkListList();


	}

	function OnClickDefault() {
		document.getElementById('homework-type-form').style.display = "none";
		document.getElementById('add-homework-form').style.display = "none";
		document.getElementById('detail-form').style.display = "none";

		document.getElementById('add-homework-type-btn').onclick = function () {
		document.getElementById('homework-type-section').style.display = "none";
		document.getElementById('homework-type-form').style.display = "block";
		}

		document.getElementById('back-homework-btn').onclick = function () {
		document.getElementById('homework-type-section').style.display = "block";
		document.getElementById('homework-type-form').style.display = "none";
		}

		// Add Homework
		document.getElementById('add-homework-btn').onclick = function () {
		document.getElementById('add-homework-section').style.display = "none";
		document.getElementById('add-homework-form').style.display = "block";
		}

		document.getElementById('back-add-homework-btn').onclick = function () {
		document.getElementById('add-homework-form').style.display = "none";
		document.getElementById('add-homework-section').style.display = "block";
		}	
		
	}

	$scope.SumitDetails = function () {
		document.getElementById('homework-list-section').style.display = "none";
		document.getElementById('detail-form').style.display = "block";
    }

	$scope.BackButton = function () {
		document.getElementById('detail-form').style.display = "none";
		document.getElementById('homework-list-section').style.display = "block";
	}

	$scope.ClearHomeworkType = function () {
		$scope.newHomeworkType = {
			HomeworkTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}
	$scope.ClearAddHomework = function () {
		$scope.newAddHomework = {
			AddHomeworkId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Lesson: '',
			Topic: '',
			HomeworkTypeId: null,
			HomeworkDescription: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineForReDo: null,
			DeadlineForReDoTime: null,
			File: null,
			Mode: 'Save'
		};
	}
	$scope.ClearHomeworkList = function () {
		$scope.newHomeworkList = {
			HomeworkListId: null,
			HomeworkListDetailColl: [],
			Mode: 'Save'
		};
		$scope.newHomeworkList.HomeworkListDetailColl.push({});

		
	}
	

	//************************* HomeworkType *********************************

	$scope.IsValidHomeworkType = function () {
		if ($scope.newHomeworkType.Name.isEmpty()) {
			Swal.fire('Please ! Enter  Name');
			return false;
		}

		if ($scope.newHomeworkType.Name.isEmpty()) {
			Swal.fire('Please ! Enter  Description');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateHomeworkType = function () {
		if ($scope.IsValidHomeworkType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHomeworkType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHomeworkType();
					}
				});
			} else
				$scope.CallSaveUpdateHomeworkType();

		}
	};

	$scope.CallSaveUpdateHomeworkType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveHomeworkType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newHomeworkType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHomeworkType();
				$scope.GetAllHomeworkTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		
	}

	$scope.GetAllHomeworkTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllHomeworkTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HomeworkTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHomeworkTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			HomeworkTypeId: refData.HomeworkTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHomeworkType = res.data.Data;
				$scope.newHomeworkType.Mode = 'Modify';

				document.getElementById('homework-type-section').style.display = "none";
				document.getElementById('homework-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHomeworkTypeById = function (refData) {

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
					HomeworkTypeId: refData.HomeworkTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelHomeworkType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHomeworkTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Add Homework *********************************

	$scope.GetEmpListForClassTeacher = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newAddHomework.EmployeeList = [];

		if ($scope.newAddHomework.ClassId && $scope.newAddHomework.ClassId > 0) {

			var para = {
				ClassId: $scope.newAddHomework.ClassId,
				SectionId: ($scope.newAddHomework.SectionId ? $scope.newAddHomework.SectionId : null)
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmpListForClassTeacher",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newAddHomework.EmployeeList = res.data.Data;

					if ($scope.newAddHomework.EmployeeList.length == 0) {
						Swal.fire('Class Schedule Not Found');
					}
					else if ($scope.newAddHomework.EmployeeList.length > 0) {

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetClassWiseSubjectList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newAddHomework.SubjectList = [];

		if ($scope.newAddHomework.ClassId && $scope.newAddHomework.ClassId > 0 && $scope.newAddHomework.TeacherId && $scope.newAddHomework.TeacherId>0) {

			var para = {
				EmployeeId:$scope.newAddHomework.TeacherId,
				ClassId: $scope.newAddHomework.ClassId,
				SectionId: ($scope.newAddHomework.SectionId ? $scope.newAddHomework.SectionId : null)
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetClassWiseSubjectList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newAddHomework.SubjectList = res.data.Data;

					if ($scope.newAddHomework.SubjectList.length == 0) {
						Swal.fire('No Subject Not Found');
					}
					else if ($scope.newAddHomework.SubjectList.length > 0) {

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.IsValidAddHomework = function () {
		if ($scope.newAddHomework.Lesson.isEmpty()) {
			Swal.fire('Please ! Enter Lesson');
			return false;
		}

		if ($scope.newAddHomework.Topic.isEmpty()) {
			Swal.fire('Please ! Enter Topic');
			return false;
		}

		//if ($scope.newAddHomework.HomeworkDescription.isEmpty()) {
		//	Swal.fire('Please ! Enter Description');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateAddHomework = function () {
		if ($scope.IsValidAddHomework() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddHomework.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddHomework();
					}
				});
			} else
				$scope.CallSaveUpdateAddHomework();

		}
	};

	$scope.CallSaveUpdateAddHomework = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var attachmentColl = $scope.newAddHomework.Files_TMP;

		if ($scope.newAddHomework.DeadlineDateDet) {
			$scope.newAddHomework.DeadlineDate = $filter('date')(new Date($scope.newAddHomework.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAddHomework.DeadlineDate = null;

		if ($scope.newAddHomework.DeadlineTime_TMP)
			$scope.newAddHomework.DeadlineTime = $scope.newAddHomework.DeadlineTime_TMP.toLocaleString();
		else
			$scope.newAddHomework.DeadlineTime = null;

		if ($scope.newAddHomework.DeadlineForReDoDet) {
			$scope.newAddHomework.DeadlineforRedo = $filter('date')(new Date($scope.newAddHomework.DeadlineForReDoDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAddHomework.DeadlineforRedo = null;

		if ($scope.newAddHomework.DeadlineForReDoTime_TMP)
			$scope.newAddHomework.DeadlineforRedoTime = $scope.newAddHomework.DeadlineForReDoTime_TMP.toLocaleString();
		else
			$scope.newAddHomework.DeadlineforRedoTime = null;

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveHomeWork",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File && data.files[i].File!=null)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newAddHomework, files: attachmentColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAddHomework();
				//$scope.GetAllAddHomeworkList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHomeworkList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkList = [];

		var para = {
			dateFrom: ($scope.searchData.FromDateDet ? $scope.searchData.FromDateDet.dateAD : null),
			dateTo: ($scope.searchData.ToDateDet ? $scope.searchData.ToDateDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllHomeWorkList",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HomeworkList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.DelHomeworkById = function (refData) {

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
					HomeWorkId: refData.HomeWorkId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelHomeWork",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHomeworkList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Homework List *********************************

	$scope.IsValidHomeworkList = function () {
		if ($scope.newHomeworkList.Name.isEmpty()) {
			Swal.fire('Please ! Enter HomeworkList Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateHomeworkList = function () {
		if ($scope.IsValidHomeworkList() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHomeworkList.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHomeworkList();
					}
				});
			} else
				$scope.CallSaveUpdateHomeworkList();

		}
	};

	$scope.CallSaveUpdateHomeworkList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveHomeworkList",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newHomeworkList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHomeworkList();
				$scope.GetAllHomeworkListList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHomeworkListList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkListList = [];

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllHomeworkListList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HomeworkListList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHomeworkListById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			HomeworkListId: refData.HomeworkListId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHomeworkList = res.data.Data;
				$scope.newHomeworkList.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHomeworkListById = function (refData) {

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
					HomeworkListId: refData.HomeworkListId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelHomeworkList",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHomeworkListList();
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