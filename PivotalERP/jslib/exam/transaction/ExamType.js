app.controller('ExamTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Exam Type';

	OnClickDefault();

	//add and del
	$scope.AddExamTypeGroupDetails = function (ind) {
		if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl) {
			if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl.length > ind + 1) {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.splice(ind + 1, 0, {
					Sno: '',
					ExamType: '',
					Percentage: '',
					DisplayName:'',
					CalculateAttendance: ''

				})
			} else {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({
					Sno: '',
					ExamType: '',
					Percentage: '',
					DisplayName: '',
					CalculateAttendance: ''
				})
			}
		}
	};
	$scope.delExamTypeGroupDetails = function (ind) {
		if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl) {
			if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl.length > 1) {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.splice(ind, 1);
			}
		}
	};

	//ModalTable add and del
	$scope.AddExamTypeGroupDetailsModalTable = function (ind) {
		if ($scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl) {
			if ($scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.length > ind + 1) {
				$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.splice(ind + 1, 0, {
					SubjectName: '',
					THPer: '',
					PRPer: ''
					

				})
			} else {
				$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.push({
					SubjectName: '',
					THPer: '',
					PRPer: ''
				})
			}
		}
	};
	$scope.delExamTypeGroupModalTableDetails = function (ind) {
		if ($scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl) {
			if ($scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.length > 1) {
				$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.splice(ind, 1);
			}
		}
	};


	$scope.AddParentExamTypeGroupDetails = function (ind) {
		if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl) {
			if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.length > ind + 1) {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.splice(ind + 1, 0, {
					Sno: '',
					ExamType: '',
					Percentage: '',
					DisplayName: '',
					CalculateAttendance: ''
				})
			} else {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({
					Sno: '',
					ExamType: '',
					Percentage: '',
					DisplayName: '',
					CalculateAttendance: ''
				})
			}
		}
	};
	$scope.delParentExamTypeGroupDetails = function (ind) {
		if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl) {
			if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.length > 1) {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ExamType: 1,
			ExamTypeGroup: 1,
			ExamTypeGroupModalTable: 1,
			ParentExamTypeGroup: 1
			
		};

		$scope.searchData = {
			ExamType: '',
			ExamTypeGroup: '',
			ExamTypeGroupModalTable: '',
			ParentExamTypeGroup: ''
			
		};

		$scope.perPage = {
			ExamType: GlobalServices.getPerPageRow(),
			ExamTypeGroup: GlobalServices.getPerPageRow(),
			ExamTypeGroupModalTable: GlobalServices.getPerPageRow(),
			ParentExamTypeGroup: GlobalServices.getPerPageRow(),
			
		};

		$scope.newExamType = {
			ExamTypeId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime:'',
			OrderNo: 0,
			
			Mode: 'Save'
		};


		$scope.newExamTypeGroup = {
			ExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ExamTypeGroupDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({});

		//ModalTable
		$scope.newExamTypeGroupModalTable = {
			ExamTypeGroupModalTableId: null,
			SubjectName: '',
			THPer: '',
			PRPer: '',
			ExamTypeGroupModalTableDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.push({});


		$scope.newParentExamTypeGroup = {
			ParentExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ParentExamTypeGroupDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({});

		

		//$scope.GetAllExamTypeList();
		//$scope.GetAllExamTypeGroupList();
		//$scope.GetAllParentExamTypeGroupList();
		

	}

	function OnClickDefault() {


		document.getElementById('exam-type-form').style.display = "none";
		document.getElementById('exam-type-group-form').style.display = "none";
		document.getElementById('parent-exam-type-form').style.display = "none";


		// exam-type section

		document.getElementById('add-exam-type').onclick = function () {
			document.getElementById('exam-type-section').style.display = "none";
			document.getElementById('exam-type-form').style.display = "block";
			$scope.ClearExamType();
		}

		document.getElementById('back-exam-type').onclick = function () {
			document.getElementById('exam-type-section').style.display = "block";
			document.getElementById('exam-type-form').style.display = "none";
			$scope.ClearExamType();
		}


		// exam type group section

		document.getElementById('add-exam-type-group').onclick = function () {
			document.getElementById('exam-type-group-section').style.display = "none";
			document.getElementById('exam-type-group-form').style.display = "block";
			$scope.ClearExamTypeGroup();
		}

		document.getElementById('back-exam-type-group').onclick = function () {
			document.getElementById('exam-type-group-section').style.display = "block";
			document.getElementById('exam-type-group-form').style.display = "none";
			$scope.ClearExamTypeGroup();
		}


		//parent exam type group section


		document.getElementById('add-parent-exam-type').onclick = function () {
			document.getElementById('parent-exam-type-section').style.display = "none";
			document.getElementById('parent-exam-type-form').style.display = "block";
			$scope.ClearParentExamTypeGroup();
		}

		document.getElementById('back-parent-exam-type').onclick = function () {
			document.getElementById('parent-exam-type-section').style.display = "block";
			document.getElementById('parent-exam-type-form').style.display = "none";
			$scope.ClearParentExamTypeGroup();
		}

		
	}

	$scope.ClearExamType = function () {
		$scope.newExamType = {
			ExamTypeId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime:'',
			OrderNo: 0,
			Mode: 'Save'
		};
	}
	$scope.ClearExamTypeGroup = function () {
		$scope.newExamTypeGroup = {
			ExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ExamTypeGroupDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({});
	}

	$scope.ClearExamTypeGroupModalTable = function () {
		$scope.newExamTypeGroupModalTable = {
			ExamTypeGroupModalTableId: null,
			SubjectName: '',
			THPer: '',
			PRPer: '',
			ExamTypeGroupModalTableDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.push({});
       
    }

	$scope.ClearParentExamTypeGroup = function () {
		$scope.newParentExamTypeGroup = {
			ParentExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ParentExamTypeGroupDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({});
	}
	

	//************************* ExamType *********************************

	$scope.IsValidExamType = function () {
		if ($scope.newExamType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateExamType = function () {
		if ($scope.IsValidExamType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamType();
					}
				});
			} else
				$scope.CallSaveUpdateExamType();

		}
	};

	$scope.CallSaveUpdateExamType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamType();
				$scope.GetAllExamTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		if ($scope.newExamType.ResultDateDet) {
			$scope.newExamType.ResultDate = $scope.newExamType.ResultDateDet.dateAD;
		} else
			$scope.newExamType.ResultDate = null;
	}

	$scope.GetAllExamTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeId: refData.ExamTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamType = res.data.Data;
				$scope.newExamType.Mode = 'Modify';

				document.getElementById('exam-type-section').style.display = "none";
				document.getElementById('exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeById = function (refData) {

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
					ExamTypeId: refData.ExamTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Type Group *********************************

	$scope.IsValidExamTypeGroup = function () {
		if ($scope.newExamTypeGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamTypeGroup = function () {
		if ($scope.IsValidExamTypeGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeGroup();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeGroup();

		}
	};

	
	$scope.CallSaveUpdateExamTypeGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newExamTypeGroup.ResultDateDet) {
			$scope.newExamTypeGroup.ResultDate = $scope.newExamTypeGroup.ResultDateDet.dateAD;
		} else
			$scope.newExamTypeGroup.ResultDate = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamTypeGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamTypeGroup();
				$scope.GetAllExamTypeGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeGroupId: refData.ExamTypeGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamTypeGroup = res.data.Data;
				$scope.newExamTypeGroup.Mode = 'Modify';

				document.getElementById('exam-type-group-section').style.display = "none";
				document.getElementById('exam-type-group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeGroupById = function (refData) {

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
					ExamTypeGroupId: refData.ExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeGroupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//***************************Modal Table********************
	$scope.SaveUpdateExamTypeGroupModalTable = function () {
		if ($scope.IsValidExamTypeGroupModalTable() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeGroupModalTable.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeGroupModalTable();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeGroupModalTable();

		}
	};

	$scope.CallSaveUpdateExamTypeGroupModalTable = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		


		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeGroupModalTable",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamTypeGroupModalTable }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamTypeGroupModalTable();
				$scope.GetAllExamTypeGroupModalTableList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeGroupModalTableList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeGroupModalTableList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeGroupModalTableList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeGroupModalTableList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.DelExamTypeGroupModalTableById = function (refData) {

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
					ExamTypeGroupModalTableId: refData.ExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeGroupModalTable",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeGroupModalTableList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//***************************Modal Table End********************

	//*************************ParentExamTypeGroup *********************************

	$scope.IsValidParentExamTypeGroup = function () {
		if ($scope.newParentExamTypeGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Parent Exam Type Group Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateParentExamTypeGroup = function () {
		if ($scope.IsValidParentExamTypeGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newParentExamTypeGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateParentExamTypeGroup();
					}
				});
			} else
				$scope.CallSaveUpdateParentExamTypeGroup();

		}
	};

	$scope.CallSaveUpdateParentExamTypeGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newParentExamTypeGroup.ResultDateDet) {
			$scope.newParentExamTypeGroup.ResultDate = $scope.newParentExamTypeGroup.ResultDateDet.dateAD;
		} else
			$scope.newParentExamTypeGroup.ResultDate = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveParentExamTypeGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newParentExamTypeGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearParentExamTypeGroup();
				$scope.GetAllParentExamTypeGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllParentExamTypeGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ParentExamTypeGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllParentExamTypeGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ParentExamTypeGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetParentExamTypeGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ParentExamTypeGroupId: refData.ParentExamTypeGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetParentExamTypeGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newParentExamTypeGroup = res.data.Data;
				$scope.newParentExamTypeGroup.Mode = 'Modify';

				document.getElementById('parent-exam-type-section').style.display = "none";
				document.getElementById('parent-exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelParentExamTypeGroupById = function (refData) {

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
					ParentExamTypeGroupId: refData.ParentExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelParentExamTypeGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllParentExamTypeGroupList();
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