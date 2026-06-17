app.controller('StudentRemarksController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'StudentRemarks';

	//OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.RemarksForList = [{ id: 1, text: 'MERITS' }, { id: 2, text: 'DEMERITS' }, { id: 3, text: 'OTHERS' },]
		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			StudentRemarks: 1,
			RemarkList: 1

		};

		$scope.searchData = {
			StudentRemarks: '',
			RemarkList: ''

		};

		$scope.perPage = {
			StudentRemarks: GlobalServices.getPerPageRow(),
			RemarkList: GlobalServices.getPerPageRow()
		};

		$scope.newStudentRemarks = {
			StudentRemarksId: null,
			StudentRemarksDetailsColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			//Mode: 'Save'
		};
		$scope.newStudentRemarks.StudentRemarksDetailsColl.push({});

		$scope.newRemarkList = {
			RemarkListId: null,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			//Mode: 'Save'
		};

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


		$timeout(function () {
			$scope.GetRemarksList();
		});

		$scope.StudentRemarksList = [];
		$scope.StudentRemarksList.push({
			ForDate_TMP:new Date()
		});
	}



	$scope.ClearStudentRemarks = function () {
		$scope.newStudentRemarks = {
			StudentRemarksId: null,
			StudentRemarksDetailsColl: [],
			//Mode: 'Save'
		};
		$scope.newStudentRemarks.StudentRemarksDetailsColl.push({});
	}
	$scope.ClearRemarkList = function () {
		$scope.newRemarkList = {
			RemarkListId: null,

			//Mode: 'Save'
		};
	}


	//************************* Student Remarks *********************************

	$scope.GetRemarks = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PStudentRemarksList = [];

		$scope.StudentRemarksList = [];
		$scope.StudentRemarksList.push({});

		var para = {
			StudentId: $scope.newStudentRemarks.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetRemarks",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PStudentRemarksList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.AddRowInRemarks = function (ind) {
		if ($scope.StudentRemarksList) {
			if ($scope.StudentRemarksList.length > ind + 1) {
				$scope.StudentRemarksList.splice(ind + 1, 0, {});
			} else {
				$scope.StudentRemarksList.push({
					ForDate_TMP: new Date()
				});
			}
		}

	};
	$scope.delRowInRemarks = function (ind, beData) {
		if ($scope.StudentRemarksList && (!beData.TranId || beData.TranId == 0)) {
			if ($scope.StudentRemarksList.length > 1) {
				$scope.StudentRemarksList.splice(ind, 1);
			}
		} else if (beData.TranId > 0) {
			$scope.DelStudentRemarksById(beData);
		}
	};

	$scope.ValidateRemarkFile = function (file) {

		if (!file) return true; // no file selected

		var maxSize = 1024 * 1024; // 1 MB

		var allowedTypes = [
			'image/jpeg',
			'image/png',
			'image/gif',
			'application/pdf',
			'application/msword',
			'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
		];

		if (allowedTypes.indexOf(file.type) === -1) {
			Swal.fire(
				'Invalid File Type',
				'Only JPG, PNG, GIF, PDF and DOC files are allowed.',
				'warning'
			);
			return false;
		}

		if (file.size > maxSize) {
			Swal.fire(
				'File Too Large',
				'File size must be less than or equal to 1 MB.',
				'warning'
			);
			return false;
		}

		return true;
	};


	$scope.SaveUpdateStudentRemarks = function (beData) {
		if (beData.AttachFile && beData.AttachFile.length > 0) {
			if (!$scope.ValidateRemarkFile(beData.AttachFile[0])) {
				return;
			}
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		beData.StudentId = $scope.newStudentRemarks.StudentId;

		if (beData.ForDateDet)
			beData.ForDate = $filter('date')(new Date(beData.ForDateDet.dateAD), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentRemarks",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					formData.append("file0", data.files[0]);
				}

				return formData;
			},
			data: { jsonData: beData, files: beData.AttachFile }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.GetRemarks();
			}


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelStudentRemarksById = function (refData) {

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
					url: base_url + "Academic/Creation/DelStudentRemarks",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetRemarks();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.FilePath && item.FilePath.length > 0) {
			$scope.viewImg.ContentPath = item.FilePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};

	$scope.SendNotificationToStudent = function (refData) {

		if (refData.TranId && refData.TranId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var st = $scope.newStudentRemarks.StudentDetails;
			var rType = $scope.RemarksTypeQry.firstOrDefault(p1 => p1.RemarksTypeId == refData.RemarksTypeId);

			var dataColl = [];
			var newSMS = {
				//EntityId: entityStudentSummaryForSMS,
				EntityId: remarksNotificationEntity,
				StudentId: st.StudentId,
				UserId: st.UserId,
				Title: rType.Name,
				Message: refData.Remarks,
				ContactNo: st.ContactNo,
				StudentName: st.Name
			};
			dataColl.push(newSMS);
			$http({
				method: 'POST',
				url: base_url + "Global/SendNotificationToStudent",
				dataType: "json",
				data: JSON.stringify(dataColl)
			}).then(function (sRes) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				Swal.fire(sRes.data.ResponseMSG);
				if (sRes.data.IsSuccess && sRes.data.Data) {

				}
			});
		} else {
			Swal.fire('1st Save Remarks');
		}



	};

	$scope.SendSMSToStudent = function (refData) {
		Swal.fire({
			title: 'Do you want to Send SMS To Student?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var st = $scope.newStudentRemarks.StudentDetails;
				var rType = $scope.RemarksTypeQry.firstOrDefault(p1 => p1.RemarksTypeId == refData.RemarksTypeId);

				var dataColl = [];
				var newSMS = {
					//EntityId: entityStudentSummaryForSMS,
					EntityId: remarksNotificationEntity,
					StudentId: st.StudentId,
					UserId: st.UserId,
					Title: rType.Name,
					Message: refData.Remarks,
					ContactNo: st.ContactNo,
					StudentName: st.Name
				};

				dataColl.push(newSMS);

				$http({
					method: 'POST',
					url: base_url + "Global/SendSMSToStudent",
					dataType: "json",
					data: JSON.stringify(dataColl)
				}).then(function (sRes) {
					Swal.fire(sRes.data.ResponseMSG);
					if (sRes.data.IsSuccess && sRes.data.Data) {

					}
				});
			}
		});


	};

	//************************* RemarkList   *********************************

	$scope.GetRemarksList = function () {
		if ($scope.newRemarkList.FromDateDet && $scope.newRemarkList.ToDateDet) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.AllRemarksList = [];

			var para = {
				dateFrom: $filter('date')(new Date($scope.newRemarkList.FromDateDet.dateAD), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date($scope.newRemarkList.ToDateDet.dateAD), 'yyyy-MM-dd'),
				remarksTypeId: $scope.newRemarkList.RemarksTypeId,
				remarksFor: $scope.newRemarkList.RemarksFor
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetRemarksList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllRemarksList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}

	}

	$scope.SendRemarkSMS = function (rl) {

		if (!rl) {
			Swal.fire('Invalid record');
			return;
		}

		if (!rl.ContactNo) {
			Swal.fire('Contact number not available');
			return;
		}

		if (!rl.Remarks) {
			Swal.fire('Remarks message is empty');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: 'Do you want to send this SMS?',
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send'
		}).then((result) => {

			if (!result.isConfirmed) return;

			var dataColl = [{
				EntityId: remarksSMSEntity,
				StudentId: rl.StudentId,
				UserId: rl.UserId || null,
				Message: rl.Remarks,
				ContactNo: rl.ContactNo,
				StudentName: rl.Name
			}];

			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSToStudent",
				dataType: "json",
				data: JSON.stringify(dataColl)
			}).then(function (res) {

				Swal.fire(res.data.ResponseMSG);

			}, function () {
				Swal.fire('Failed to send SMS');
			});

		});
	};

	$scope.SendRemarkNotification = function (rl) {

		if (!rl) {
			Swal.fire('Invalid record');
			return;
		}

		if (!rl.ContactNo) {
			Swal.fire('Contact number not available');
			return;
		}

		if (!rl.Remarks) {
			Swal.fire('Remarks message is empty');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: 'Do you want to send this notification?',
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {

			if (!result.isConfirmed) return;

			$scope.loadingstatus = "running";
			showPleaseWait();

			var contentPath = rl.FilePath || '';
			var uploadPromise;

			// Check if there's a new file to upload (not the existing FilePath)
			if (rl.AttachFile && rl.AttachFile instanceof File) {
				// This is for new file uploads
				uploadPromise = $http({
					method: 'POST',
					url: base_url + "Global/UploadAttachments",
					headers: { 'Content-Type': undefined },
					transformRequest: function () {
						var formData = new FormData();
						formData.append("file0", rl.AttachFile);
						return formData;
					}
				});

			} else if (rl.FilePath) {
				uploadPromise = Promise.resolve({ data: { IsSuccess: true, Data: [rl.FilePath] } });
			} else {
				// No file to process
				uploadPromise = Promise.resolve({ data: { IsSuccess: true, Data: [] } });
			}

			uploadPromise.then(function (res) {
				// If a new file was uploaded, use its path, otherwise keep the existing FilePath
				if (res.data.IsSuccess && res.data.Data.length > 0) {
					contentPath = res.data.Data[0];
				}

				let msg = rl.Remarks;
				for (let x in rl) {
					let variable = '$$' + x.toLowerCase() + '$$';
					if (msg.indexOf(variable) >= 0) {
						msg = msg.replace(variable, rl[x] || '');
					}
					if (msg.indexOf('$$') === -1) break;
				}

				var dataColl = [{
					EntityId: remarksNotificationEntity,
					StudentId: rl.StudentId || 0,
					UserId: rl.UserId || 0,
					Title: rl.RemarsType || 'Student Remarks',
					Message: msg,
					ContactNo: rl.ContactNo,
					StudentName: rl.Name,
					ContentPath: contentPath // This will contain either existing FilePath or newly uploaded file path
				}];

				$http({
					method: 'POST',
					url: base_url + "Global/SendNotificationToStudent",
					dataType: "json",
					data: JSON.stringify(dataColl)
				}).then(function (sRes) {

					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(sRes.data.ResponseMSG);

				}, function () {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire('Failed to send notification');
				});

			}, function () {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('File upload failed');
			});

		});
	};
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				$scope.$applyAsync();
			});
		} else {
			$scope.GetRemarksList();
        }
	};
	
});