
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};


app.controller('StudentComplaintController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Complaint';
	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			StudentComplaint: 1,
			SelfComplaintComp: 1,
			SelfComplainFeedback: 1

		};

		$scope.searchData = {
			StudentComplaint: '',
			SelfComplaintComp: '',
			SelfComplainFeedback: ''

		};

		$scope.perPage = {
			StudentComplaint: GlobalServices.getPerPageRow(),
			SelfComplaintComp: GlobalServices.getPerPageRow(),
			SelfComplainFeedback: GlobalServices.getPerPageRow(),

		};

		$scope.newStudentComplaint = {
			StudentComplaintId: null,

			Mode: 'Save'
		};

		$scope.newSelfComplaintComp = {
			SelfComplaintCompId: null,
			ComplainTypeId: null,
			ComplaintDetails: '',
			Suggestion: '',
			AttachDocument: null,
			Mode: 'Save'
		};

		$scope.newSelfComplainFeedback = {
			SelfComplainFeedbackId: null,
			Title: '',
			FeedbackDetails: '',
			AttachDocument: null,
			Mode: 'Save'
		};





		//$scope.GetAllStudentComplaintList();
		//$scope.GetAllSelfComplaintCompList();
		//$scope.GetAllSelfComplainFeedbackList();



	}



	$scope.ClearStudentComplaint = function () {
		$scope.newStudentComplaint = {
			StudentComplaintId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearSelfComplaintComp = function () {
		$scope.newSelfComplaintComp = {
			SelfComplaintCompId: null,
			ComplainTypeId: null,
			ComplaintDetails: '',
			Suggestion: '',
			AttachDocument: null,
			Mode: 'Save'
		};
	}

	$scope.ClearSelfComplainFeedback = function () {
		$scope.newSelfComplainFeedback = {
			SelfComplainFeedbackId: null,
			Title: '',
			FeedbackDetails: '',
			AttachDocument: null,
			Mode: 'Save'
		};
	}




	//*************************StudentComplaint *********************************

	//$scope.IsValidStudentComplaint = function () {
	//	if ($scope.newStudentComplaint.FromTitle.isEmpty()) {
	//		Swal.fire('Please ! Enter From Title');
	//		return false;
	//	}




	//	return true;
	//}

	$scope.SaveUpdateStudentComplaint = function () {
		if ($scope.IsValidStudentComplaint() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentComplaint.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentComplaint();
					}
				});
			} else
				$scope.CallSaveUpdateStudentComplaint();

		}
	};

	$scope.CallSaveUpdateStudentComplaint = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction//SaveSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSetup();
				$scope.GetAllStudentComplaintList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentComplaintList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentComplaintList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction//GetAllStudentComplaintList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentComplaintList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentComplaintById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentComplaintId: refData.StudentComplaintId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction//GetSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentComplaint = res.data.Data;
				$scope.newStudentComplaint.Mode = 'Modify';

				document.getElementById('Setup-Employee').style.display = "none";
				document.getElementById('Setup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentComplaintById = function (refData) {
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
					StudentComplaintId: refData.StudentComplaintId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelStudentComplaint",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentComplaintList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************SelfComplaintComp *********************************

	$scope.IsValidSelfComplaintComp = function () {
		if ($scope.newSelfComplaintComp.ComplaintDetails.isEmpty()) {
			Swal.fire('Please ! Enter Complain Details');
			return false;
		}		
		return true;
	}


	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newSelfComplaintComp.AttachmentColl) {
			if ($scope.newSelfComplaintComp.AttachmentColl.length > 0) {
				$scope.newSelfComplaintComp.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newSelfComplaintComp.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};


	$scope.SaveUpdateSelfComplaintComp = function () {
		if ($scope.IsValidSelfComplaintComp() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSelfComplaintComp.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSelfComplaintComp();
					}
				});
			} else
				$scope.CallSaveUpdateSelfComplaintComp();

		}
	};

	$scope.CallSaveUpdateSelfComplaintComp = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction//SaveSelfComplaintComp",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSelfComplaintComp }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSelfComplaintComp();
				$scope.GetAllSelfComplaintCompList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSelfComplaintCompList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SelfComplaintCompList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSelfComplaintCompList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SelfComplaintCompList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSelfComplaintCompById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SelfComplaintCompId: refData.SelfComplaintCompId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetSelfComplaintCompById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSelfComplaintComp = res.data.Data;
				$scope.newSelfComplaintComp.Mode = 'Modify';

				document.getElementById('SelfComplaintComp-content').style.display = "none";
				document.getElementById('SelfComplaintComp-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSelfComplaintCompById = function (refData) {

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
					SelfComplaintCompId: refData.SelfComplaintCompId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelSelfComplaintComp",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSelfComplaintCompList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};




	//*************************SelfComplainFeedback *********************************

	$scope.IsValidSelfComplainFeedback = function () {
		if ($scope.newSelfComplainFeedback.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}

		
		return true;
	}

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newSelfComplainFeedback.AttachmentColl) {
			if ($scope.newSelfComplainFeedback.AttachmentColl.length > 0) {
				$scope.newSelfComplainFeedback.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newSelfComplainFeedback.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};


	$scope.SaveUpdateSelfComplainFeedback = function () {
		if ($scope.IsValidSelfComplainFeedback() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSelfComplainFeedback.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSelfComplainFeedback();
					}
				});
			} else
				$scope.CallSaveUpdateSelfComplainFeedback();

		}
	};

	$scope.CallSaveUpdateSelfComplainFeedback = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveSelfComplainFeedback",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSelfComplainFeedback }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSelfComplainFeedback();
				$scope.GetAllSelfComplainFeedbackList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSelfComplainFeedbackList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SelfComplainFeedbackList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSelfComplainFeedbackList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SelfComplainFeedbackList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSelfComplainFeedbackById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SelfComplainFeedbackId: refData.SelfComplainFeedbackId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetSelfComplainFeedbackById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSelfComplainFeedback = res.data.Data;
				$scope.newSelfComplainFeedback.Mode = 'Modify';

				document.getElementById('SelfComplainFeedback-content').style.display = "none";
				document.getElementById('SelfComplainFeedback-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSelfComplainFeedbackById = function (refData) {

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
					SelfComplainFeedbackId: refData.SelfComplainFeedbackId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelSelfComplainFeedback",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSelfComplainFeedbackList();
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