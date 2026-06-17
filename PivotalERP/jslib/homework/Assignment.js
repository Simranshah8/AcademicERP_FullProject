app.controller('AssignmentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Assignment';

	OnClickDefault();
	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

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
			AssignmentType: 1,
			AddAssignment: 1,
			AssignmentList: 1,
			StudentEvaluation: 1,
			TeacherEvaluation:1

		};

		$scope.searchData = {
			AssignmentType: '',
			AddAssignment: '',
			AssignmentList: '',
			StudentEvaluation: '',
			TeacherEvaluation:''
		};

		$scope.perPage = {
			AssignmentType: glbS.getPerPageRow(),
			AddAssignment: glbS.getPerPageRow(),
			AssignmentList: glbS.getPerPageRow(),
			StudentEvaluation: glbS.getPerPageRow(),
			TeacherEvaluation: glbS.getPerPageRow()

		};

		$scope.newAssignmentType = {
			AssignmentTypeId: null,
			Name: '',
			AssignmentDescription: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newAddAssignment = {
			AddAssignmentId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			AssignmentType: '',
			Title: '',			
			AssignmentDescription: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineForReDo: null,
			DeadlineForReDoTime: null,
			IsAllowLateSibmission:false,
			Marks: 0,			
			Mode: 'Save'
		};

		$scope.newAssignmentList = {
			AcademicYearId: null,
			AssignmentListDetailColl: [],
			Mode: 'Save'
		};
		$scope.newAssignmentList.AssignmentListDetailColl.push({});

		$scope.newStudentEvaluation = {
			AcademicYearId: null,
			Mode: 'Save'
		};


		$scope.newTeacherEvaluation = {
			AcademicYearId: null,
			Mode: 'Save'
		};



		$scope.GetAllAssignmentTypeList();
		//$scope.GetAllAddHomeworkList();
		//$scope.GetAllHomeworkListList();


	}

	function OnClickDefault() {
		document.getElementById('assignment-type-form').style.display = "none";
		document.getElementById('add-assignment-form').style.display = "none";
		document.getElementById('detail-form').style.display = "none";

		document.getElementById('add-assignment-type-btn').onclick = function () {
			document.getElementById('assignment-type-section').style.display = "none";
			document.getElementById('assignment-type-form').style.display = "block";

		}

		document.getElementById('back-assignment-btn').onclick = function () {
			document.getElementById('assignment-type-section').style.display = "block";

			document.getElementById('assignment-type-form').style.display = "none";
		}
		// Add Assignment Close

		// Add Assignment Type
		document.getElementById('add-assignment-btn').onclick = function () {

			document.getElementById('add-assignment-form').style.display = "block";
			document.getElementById('assignment-add-section').style.display = "none";

		}

		document.getElementById('back-add-assignment-btn').onclick = function () {
			document.getElementById('add-assignment-form').style.display = "none";

			document.getElementById('assignment-add-section').style.display = "block";
		}	
	}


	$scope.SumitDetails = function () {
		document.getElementById('assignment-list-section').style.display = "none";
		document.getElementById('detail-form').style.display = "block";
	}

	$scope.BackButton = function () {
		document.getElementById('detail-form').style.display = "none";
		document.getElementById('assignment-list-section').style.display = "block";
	}


	$scope.ClearAssignmentType = function () {
		$scope.newAssignmentType = {
			AssignmentTypeId: null,
			Name: '',
			AssignmentDescription: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}

	$scope.ClearAddAssignment = function () {
		$scope.newAddAssignment = {
			AddAssignmentId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			AssignmentType: '',
			Title: '',
			AssignmentDescription: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineForReDo: null,
			DeadlineForReDoTime: null,
			Marks: null,
			File: null,
			Mode: 'Save'
		};
	}
	$scope.ClearAssignmentList = function () {
		$scope.newAssignmentList = {
			AssignmentListId: null,
			AssignmentListDetailColl: [],
			Mode: 'Save'
		};
		$scope.newAssignmentList.AssignmentListDetailColl.push({});
	}

	$scope.ClearStudentEvaluation = function () {
		$scope.newStudentEvaluation = {
			StudentEvaluationId: null,

			Mode: 'Save'
		};
	}

	$scope.ClearTeacherEvaluation = function () {
		$scope.newTeacherEvaluation = {
			TeacherEvaluationId: null,

			Mode: 'Save'
		};
	}


	//************************* Assignment Type *********************************

	$scope.IsValidAssignmentType = function () {
		if ($scope.newAssignmentType.Name.isEmpty()) {
			Swal.fire('Please ! Enter  Name');
			return false;
		}

	

		return true;
	};

	$scope.SaveUpdateAssignmentType = function () {
		if ($scope.IsValidAssignmentType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAssignmentType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAssignmentType();
					}
				});
			} else
				$scope.CallSaveUpdateAssignmentType();

		}
	};

	$scope.CallSaveUpdateAssignmentType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveAssignmentType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				
				return formData;
			},
			data: { jsonData: $scope.newAssignmentType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAssignmentType();
				$scope.GetAllAssignmentTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	}

	$scope.GetAllAssignmentTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignmentTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetAllAssignmentTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AssignmentTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAssignmentTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AssignmentTypeId: refData.AssignmentTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetAssignmentTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAssignmentType = res.data.Data;
				$scope.newAssignmentType.Mode = 'Modify';

				document.getElementById('assignment-type-section').style.display = "none";
				document.getElementById('assignment-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAssignmentTypeById = function (refData) {

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
					AssignmentTypeId: refData.AssignmentTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "HomeWork/Transaction/DelAssignmentType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAssignmentTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Add Assignment *********************************

	$scope.GetEmpListForClassTeacher = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newAddAssignment.EmployeeList = [];

		if ($scope.newAddAssignment.ClassId && $scope.newAddAssignment.ClassId > 0) {

			var para = {
				ClassId: $scope.newAddAssignment.ClassId,
				SectionId: ($scope.newAddAssignment.SectionId ? $scope.newAddAssignment.SectionId : null)
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
					$scope.newAddAssignment.EmployeeList = res.data.Data;

					if ($scope.newAddAssignment.EmployeeList.length == 0) {
						Swal.fire('Class Schedule Not Found');
					}
					else if ($scope.newAddAssignment.EmployeeList.length > 0) {

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

		$scope.newAddAssignment.SubjectList = [];

		if ($scope.newAddAssignment.ClassId && $scope.newAddAssignment.ClassId > 0 && $scope.newAddAssignment.TeacherId && $scope.newAddAssignment.TeacherId > 0) {

			var para = {
				EmployeeId: $scope.newAddAssignment.TeacherId,
				ClassId: $scope.newAddAssignment.ClassId,
				SectionId: ($scope.newAddAssignment.SectionId ? $scope.newAddAssignment.SectionId : null)
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
					$scope.newAddAssignment.SubjectList = res.data.Data;

					if ($scope.newAddAssignment.SubjectList.length == 0) {
						Swal.fire('No Subject Not Found');
					}
					else if ($scope.newAddAssignment.SubjectList.length > 0) {

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.IsValidAddAssignment = function () {
		if ($scope.newAddAssignment.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAddAssignment = function () {
		if ($scope.IsValidAddAssignment() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddAssignment.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddAssignment();
					}
				});
			} else
				$scope.CallSaveUpdateAddAssignment();

		}
	};

	$scope.CallSaveUpdateAddAssignment = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var attachmentColl = $scope.newAddAssignment.Files_TMP;

		if ($scope.newAddAssignment.DeadlineDateDet) {
			$scope.newAddAssignment.DeadlineDate = $filter('date')(new Date($scope.newAddAssignment.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAddAssignment.DeadlineDate = null;

		if ($scope.newAddAssignment.DeadlineTime_TMP)
			$scope.newAddAssignment.DeadlineTime = $scope.newAddAssignment.DeadlineTime_TMP.toLocaleString();
		else
			$scope.newAddAssignment.DeadlineTime = null;

		if ($scope.newAddAssignment.DeadlineForReDoDet) {
			$scope.newAddAssignment.DeadlineforRedo = $filter('date')(new Date($scope.newAddAssignment.DeadlineForReDoDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAddAssignment.DeadlineforRedo = null;

		if ($scope.newAddAssignment.DeadlineForReDoTime_TMP)
			$scope.newAddAssignment.DeadlineforRedoTime = $scope.newAddAssignment.DeadlineForReDoTime_TMP.toLocaleString();
		else
			$scope.newAddAssignment.DeadlineforRedoTime = null;

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveAssignment",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File && data.files[i].File != null)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newAddAssignment, files: attachmentColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAddAssignment();
				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	

	$scope.GetAllAddAssignmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddAssignmentList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAddAssignmentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AddAssignmentList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAddAssignmentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AddAssignmentId: refData.AddAssignmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAddAssignmentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAddAssignment = res.data.Data;
				$scope.newAddAssignment.Mode = 'Modify';

				document.getElementById('AddAssignment-content').style.display = "none";
				document.getElementById('AddAssignment-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAddAssignmentById = function (refData) {

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
					AddAssignmentId: refData.AddAssignmentId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelAddAssignment",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAddAssignmentList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Assignment List *********************************

	$scope.IsValidAssignmentList = function () {
		if ($scope.newAssignmentList.Name.isEmpty()) {
			Swal.fire('Please ! Enter AssignmentList Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateAssignmentList = function () {
		if ($scope.IsValidAssignmentList() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAssignmentList.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAssignmentList();
					}
				});
			} else
				$scope.CallSaveUpdateAssignmentList();

		}
	};

	$scope.CallSaveUpdateAssignmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveAssignmentList",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAssignmentList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAssignmentList();
				$scope.GetAllAssignmentListList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAssignmentListList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignmentListList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAssignmentListList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AssignmentListList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAssignmentListById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AssignmentListId: refData.AssignmentListId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAssignmentListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAssignmentList = res.data.Data;
				$scope.newAssignmentList.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAssignmentListById = function (refData) {

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
					AssignmentListId: refData.AssignmentListId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelAssignmentList",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAssignmentListList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//*************************Student Evaluation *********************************

	$scope.IsValidStudentEvaluation = function () {
		if ($scope.newStudentEvaluation.Name.isEmpty()) {
			Swal.fire('Please ! Enter StudentEvaluation Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateStudentEvaluation = function () {
		if ($scope.IsValidStudentEvaluation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentEvaluation.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentEvaluation();
					}
				});
			} else
				$scope.CallSaveUpdateStudentEvaluation();

		}
	};

	$scope.CallSaveUpdateStudentEvaluation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentEvaluation",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentEvaluation }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentEvaluation();
				$scope.GetAllStudentEvaluationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentEvaluationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentEvaluationList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentEvaluationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentEvaluationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentEvaluationById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentEvaluationId: refData.StudentEvaluationId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetStudentEvaluationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentEvaluation = res.data.Data;
				$scope.newStudentEvaluation.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentEvaluationById = function (refData) {

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
					StudentEvaluationId: refData.StudentEvaluationId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudentEvaluation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentEvaluationList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//************************* Teacher Evaluation *********************************

	$scope.IsValidTeacherEvaluation = function () {
		if ($scope.newTeacherEvaluation.Name.isEmpty()) {
			Swal.fire('Please ! Enter TeacherEvaluation Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateTeacherEvaluation = function () {
		if ($scope.IsValidTeacherEvaluation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTeacherEvaluation.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTeacherEvaluation();
					}
				});
			} else
				$scope.CallSaveUpdateTeacherEvaluation();

		}
	};

	$scope.CallSaveUpdateTeacherEvaluation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveTeacherEvaluation",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newTeacherEvaluation }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearTeacherEvaluation();
				$scope.GetAllTeacherEvaluationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTeacherEvaluationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherEvaluationList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllTeacherEvaluationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TeacherEvaluationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTeacherEvaluationById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TeacherEvaluationId: refData.TeacherEvaluationId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetTeacherEvaluationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTeacherEvaluation = res.data.Data;
				$scope.newTeacherEvaluation.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTeacherEvaluationById = function (refData) {

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
					TeacherEvaluationId: refData.TeacherEvaluationId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelTeacherEvaluation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTeacherEvaluationList();
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