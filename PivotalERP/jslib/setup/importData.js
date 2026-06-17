
app.controller('importDataController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Branch';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.EntityColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetImportEntityList",
			dataType: "json"
		}).then(function (res) {		
			var eColl = res.data.Data;
			angular.forEach(eColl, function (e) {
				if (e.text.toString().includes('UPDATE_')) {
					e.IsUpdate = true;
				} else
					e.IsUpdate = false;

				$scope.EntityColl.push(e);
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.UpdateByColumns = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetUpdateByColumn",
			dataType: "json"
		}).then(function (res) {
			$scope.UpdateByColumns = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ImportData = {
			EntityId: 0,
			Files_TMP:null
		};	

		$scope.SelectedPropertiesColl = [];
		$scope.SelectedPropertiesColl.push({
			PropertyName: '',
			Include: true
		});
	}
	$scope.addrow = function (ind) {

		if (ind + 1 == $scope.SelectedPropertiesColl.length) {
			$scope.SelectedPropertiesColl.push({
				PropertyName: '',
				Include: true
			});
		}

	};
	$scope.delete = function (val) {

		if ($scope.SelectedPropertiesColl.length > 1)
			$scope.SelectedPropertiesColl.splice(val, 1);
	};
	$scope.UploadToSrv = function () {

		if ($scope.ImportData.Files_TMP && $scope.ImportData.Files_TMP.length > 0) {

			$scope.SelectedFile = null;
			$scope.FilePath = null;
			$scope.SheetColl = [];
			$scope.SelectedSheet = null;
			$scope.PropertiesColl = [];
			$scope.ColumnColl = [];

			$http({
				method: 'POST',
				url: base_url + "Setup/Security/SaveImportDataExcelFile",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					//formData.append("jsonData", angular.toJson(data.jsonData));

					formData.append("file0", data.files);

					return formData;
				},
				data: { files: $scope.ImportData.Files_TMP[0] }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				var data = res.data;
				if (data.IsSuccess) {

					$scope.FilePath = data.FilePath;
					$scope.SheetColl = data.SheetColl;

					if($scope.ImportData.SelectedEntity.IsUpdate==true)
						$('#UpdateNewExcel').modal('show');
					else
						$('#AddNewExcel').modal('show');

				} else {
					Swal.fire(data.ResponseMSG);
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});

			
        }
		
	};

	$scope.LoadProperties = function () {

		$scope.loadingstatus = "running";

		$scope.PropertiesColl = [];
		$scope.ColumnColl = [];

		var para = {
			path: $scope.FilePath,
			table: $scope.SelectedSheet,
			EntityId: $scope.ImportData.SelectedEntity.id
		};
		$http.get(base_url + "Setup/Security/LoadAllColumnsFromSheet?path=" + para.path + "&table=" + para.table + "&EntityId=" + para.EntityId).then(
			function (res) {
				if (res.data.IsSuccess) {

					var colQry = mx(res.data.ColumnColl);

					angular.forEach(res.data.PropertiesColl, function (pr) {

						if (pr != "ResponseMSG" && pr != "IsSuccess" && pr != "CUserId") {

							var findCol = colQry.firstOrDefault(p1 => p1.Name == pr);
							var properDet =
							{
								PropertyName: pr,
								Name: '',
								DefaultValue: '',
								Id: (findCol ? findCol.Id : -1)
							};

							$scope.PropertiesColl.push(properDet);
						}
					})

					$scope.ColumnColl = res.data.ColumnColl;

				} else {
					alert(res.data.ResponseMSG);
				}

				$scope.loadingstatus = 'stop';
			}
			, function (reason) {
				$scope.loadingstatus = 'stop';
				alert('Failed: ' + reason);
			}
		);

	};

	$scope.ImportDataExcel = function () {

		if (!$scope.FilePath || !$scope.SelectedSheet || !$scope.ImportData.SelectedEntity)
			return;

		if (!$scope.PropertiesColl || $scope.PropertiesColl.length == 0)
			return;

		var para = {
			path: $scope.FilePath,
			table: $scope.SelectedSheet,
			EntityId: $scope.ImportData.SelectedEntity.id
		};

		$scope.loadingstatus = "running";

		$http({
			method: "post",
			url: base_url + "Setup/Security/FinalImportData?path=" + para.path + "&table=" + para.table + "&EntityId=" + para.EntityId + "&UpdateBy=''",
			data: JSON.stringify($scope.PropertiesColl),
			dataType: "json"
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			alert(res.data.ResponseMSG);

			if (res.data.IsSuccess) {
				$scope.ClearFields();
				$('#AddNewExcel').modal('hide');
			}

		}, function (errormessage) {
			$scope.loadingstatus = 'stop';
			alert('Unable to Store data. pls try again.' + errormessage.responseText);
		});
	};

	$scope.UpdateDataExcel = function () {

		if (!$scope.FilePath || !$scope.SelectedSheet || !$scope.ImportData.SelectedEntity || !$scope.ImportData.UpdateBy)
			return;

		if (!$scope.PropertiesColl || $scope.PropertiesColl.length == 0)
			return;

		if (!$scope.SelectedPropertiesColl || $scope.SelectedPropertiesColl.length == 0)
			return;

		var para = {
			path: $scope.FilePath,
			table: $scope.SelectedSheet,
			EntityId: $scope.ImportData.SelectedEntity.id,
			UpdateBy: $scope.ImportData.UpdateBy
		};

		$scope.loadingstatus = "running";

		$http({
			method: "post",
			url: base_url + "Setup/Security/FinalImportData?path=" + para.path + "&table=" + para.table + "&EntityId=" + para.EntityId+"&UpdateBy="+para.UpdateBy,
			data: JSON.stringify($scope.SelectedPropertiesColl),
			dataType: "json"
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			alert(res.data.ResponseMSG);

			if (res.data.IsSuccess) {
				$scope.ClearFields();
				$('#AddNewExcel').modal('hide');
			}

		}, function (errormessage) {
			$scope.loadingstatus = 'stop';
			alert('Unable to Store data. pls try again.' + errormessage.responseText);
		});
	};

	$scope.ClearFields = function () {
		$scope.loadingstatus = "running";

		$scope.ImportData = {
			EntityId: 0,
			Files_TMP: null
		};
		
		$scope.SelectedFile = null;
		$scope.FilePath = null;
		$scope.SheetColl = [];
		$scope.SelectedSheet = null;
		$scope.SelectedEntityId = null;

		$scope.PropertiesColl = [];
		$scope.ColumnColl = [];
		$scope.loadingstatus = "stop";
	}


	//************************* AllowBranch *********************************

	$scope.IsValidAllowBranch = function () {
		if ($scope.newAllowBranch.UserId.isEmpty()) {
			Swal.fire('Please ! Select User');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAllowBranch = function () {
		if ($scope.IsValidAllowBranch() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowBranch.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowBranch();
					}
				});
			} else
				$scope.CallSaveUpdateAllowBranch();

		}
	};

	$scope.CallSaveUpdateAllowBranch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveAllowBranch",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowBranch }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowBranch();
				$scope.GetAllAllowBranchList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllAllowBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowBranchList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllAllowBranchList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowBranchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllowBranchById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AllowBranchId: refData.AllowBranchId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllowBranchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowBranch = res.data.Data;
				$scope.newAllowBranch.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowBranchById = function (refData) {

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
					AllowBranchId: refData.AllowBranchId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelAllowBranch",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowBranchList();
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

app.controller('clearDataController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Branch';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.EntityColl = [{ id: 1, text: 'Bill Generate' }, { id: 2, text: 'Student Opening' }, { id: 3, text: 'Fee Receipt' }, { id: 4, text: 'Fee Mapping' },
			{ id: 5, text: 'Discount Setup' }, { id: 6, text: 'Manual Billing' }, { id: 7, text: 'Transport Mapping' }, { id: 8, text: 'Bed Mapping' },
			{ id: 9, text: 'Fee Debit' }, { id: 10, text: 'Mark Entry' }, { id: 11, text: 'Mark Setup' }, { id: 12, text: 'ExamSchedule' }, { id: 13, text: 'SubjectMapping' },
			{ id: 14, text: 'Book Details' }, { id: 15, text: 'Unused Class' }, { id: 16, text: 'All Student' }, { id: 17, text: 'All TransportModule' },
			{ id: 18, text: 'LedgerOpningClear' },{ id: 19, text: 'ProductOpeningClear' },
			{ id: 20, text: 'IgnoreNegativeBalance' },{ id: 21, text: 'IgnoreNegativeBalance' },
			{ id: 22, text: 'CostcenterOpeningClear' },			{ id: 23, text: 'Delete All Account Transaction' },
			{ id: 24, text: 'Delete All Inventory Transaction' },			{ id: 25, text: 'Update All ProductCostingMethodFIFO' },
			{ id: 26, text: 'Update All ProductCostingMethodLIFO' },			{ id: 27, text: 'Update All ProductCostingMethodAVG' },
			{ id: 28, text: 'Update All ProductCostingMethodInOUT' },			{ id: 29, text: 'Update All ProductCostingMethodSTD' },
			{ id: 30, text: 'Post All Transaction' },			{ id: 31, text: 'Pending All Transaction' },
			{ id: 32, text: 'UnLocked All Transaction' },			{ id: 33, text: 'Locked All Transaction' }

		];
	
	
		$scope.ClearData = {
			forEntityId: 0,
			Files_TMP: null
		};

	}
	 

	$scope.delData = function () {

		Swal.fire({
			title: 'Do you want to delete the selected data of entity ( deleted data will not recovered ) ?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para = {
					forEntityId: $scope.ClearData.forEntityId
				};

				$scope.loadingstatus = "running";
				$http({
					method: "post",
					url: base_url + "Setup/Security/DelClearData",
					data: JSON.stringify(para),
					dataType: "json"
				}).then(function (res) {
					$scope.loadingstatus = 'stop';
					
					Swal.fire(res.data.ResponseMSG)

				}, function (errormessage) {
					$scope.loadingstatus = 'stop';
					alert('Unable to Store data. pls try again.' + errormessage.responseText);
				});

				 
			}
		});


		
	
	};


});