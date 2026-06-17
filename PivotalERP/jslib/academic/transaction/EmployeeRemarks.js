app.controller('EmployeeController', function ($scope, $http, $timeout, $rootScope, $filter, $translate, GlobalServices) {
	$scope.Title = 'Employee';
	$rootScope.ChangeLanguage();


	$scope.LoadData = function () {

		//GlobalServices.ChangeLanguage();


		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();

		$scope.RemarksForList = [{ id: 1, text: 'MERITS' }, { id: 2, text: 'DEMERITS' }, { id: 3, text: 'OTHERS' },]


		$scope.currentPages = {
			RemarkList: 1,
		};

		$scope.searchData = {
			RemarkList: ''
		};

		$scope.perPage = {
			RemarkList: gSrv.getPerPageRow(),
		};



		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		gSrv.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LevelList = [];
		gSrv.getLevelList().then(function (res) {
			$scope.LevelList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.CategoryList = [];
		gSrv.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.ServiceTypeList = [];
		gSrv.getServiceTypeList().then(function (res) {
			$scope.ServiceTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.newRemarkList = {
			RemarkListId: null,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			//Mode: 'Save'
		};

		$scope.newEmployeeRemarks = {
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
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

		$scope.ClearEmployeeRemarks();

		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$('.select2').select2();
		 
	}




	$scope.ClearEmployeeRemarks = function () {
		$scope.newEmployeeRemarks = {
			EmployeeRemarksId: null,
			EmployeeRemarksDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
			//Mode: 'Save'
		};
		$scope.newEmployeeRemarks.EmployeeRemarksDetailsColl.push({});
	}
	$scope.ClearRemarkList = function () {
		$scope.newRemarkList = {
			RemarkListId: null,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			//Mode: 'Save'
		};
	}

	$scope.GetEmpRemarks = function () {

		$scope.PEmployeeRemarksList = [];

		$scope.EmployeeRemarksList = [];
		$scope.EmployeeRemarksList.push({
			ForDate_TMP: new Date()
		});

		if ($scope.newEmployeeRemarks.EmployeeId) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				EmployeeId: $scope.newEmployeeRemarks.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmpRemarks",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.PEmployeeRemarksList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.AddRowInRemarks = function (ind) {
		if ($scope.EmployeeRemarksList) {
			if ($scope.EmployeeRemarksList.length > ind + 1) {
				$scope.EmployeeRemarksList.splice(ind + 1, 0, {});
			} else {
				$scope.EmployeeRemarksList.push({
					ForDate_TMP:new Date()
				});
			}
		}

	};
	$scope.delRowInRemarks = function (ind, beData) {
		if ($scope.EmployeeRemarksList && (!beData.TranId || beData.TranId == 0)) {
			if ($scope.EmployeeRemarksList.length > 1) {
				$scope.EmployeeRemarksList.splice(ind, 1);
			}
		} else if (beData.TranId > 0) {
			$scope.DelEmployeeRemarksById(beData);
		}
	};

	$scope.SaveUpdateEmployeeRemarks = function (beData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		beData.EmployeeId = $scope.newEmployeeRemarks.EmployeeId;

		if (beData.ForDateDet)
			beData.ForDate = $filter('date')(new Date(beData.ForDateDet.dateAD), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveEmployeeRemarks",
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
				$scope.GetEmpRemarks();
			}


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelEmployeeRemarksById = function (refData) {

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
					url: base_url + "Academic/Transaction/DelEmployeeRemarks",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetEmpRemarks();
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
			ContentPath: '',
			File: null
		};
		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$('#PersonalImg').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$('#PersonalImg').modal('show');
		} else if (item.File) {
			$scope.viewImg.File = item.File;
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);

			$('#PersonalImg').modal('show');
		}

		else
			Swal.fire('No Image Found');

	};


	$scope.GetEmpRemarksList = function () {
		if ($scope.newRemarkList.FromDateDet &&
			$scope.newRemarkList.ToDateDet &&
			$scope.newRemarkList.ToDateDet.dateAD < $scope.newRemarkList.FromDateDet.dateAD) {

			$scope.newRemarkList.ToDate_TMP = null;
			$scope.newRemarkList.ToDateDet = null;

			Swal.fire(
				'Invalid Date',
				'To Date must be greater than or equal to From Date',
				'warning'
			);
			return;
		}
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
				url: base_url + "Academic/Transaction/GetEmpRemarksList",
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
				StudentId: rl.EmployeeId,
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
					StudentId: rl.EmployeeId || 0,
					UserId: rl.UserId || 0,
					Title: rl.RemarsType,
					Message: msg,
					ContactNo: rl.ContactNo,
					StudentName: rl.Name,
					ContentPath: contentPath
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



});