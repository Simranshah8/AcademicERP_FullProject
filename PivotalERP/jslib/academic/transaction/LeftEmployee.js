app.controller('EmployeeController', function ($scope, $http, $timeout, $rootScope, $filter, $translate, GlobalServices) {
	$scope.Title = 'Employee';
	$rootScope.ChangeLanguage();
	OnClickDefault();

	function OnClickDefault() {
		document.getElementById('left-employee-form').style.display = "none";

		// sections display and hide
		document.getElementById('add-employee-left').onclick = function () {
			document.getElementById('employee-left-section').style.display = "none";
			document.getElementById('left-employee-form').style.display = "block";
			$scope.ClearLeftEmployee();
		}
		document.getElementById('employee-left-back-btn').onclick = function () {
			document.getElementById('left-employee-form').style.display = "none";
			document.getElementById('employee-left-section').style.display = "block";
			$scope.ClearLeftEmployee();
		}
	}
	$scope.LoadData = function () {
		//GlobalServices.ChangeLanguage();		
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
	
		$scope.currentPages = {
			LeftEmployee: 1
		};

		$scope.searchData = {
			LeftEmployee: ''
		};

		$scope.perPage = {
			LeftEmployee: gSrv.getPerPageRow()
		};
		
		$scope.GetAllLeftEmployeeList();
		$scope.newLeftEmployee = {
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
			LeftRemarks: '',
			LeftDate_TMP: new Date()
		};		
	}

	$scope.ClearLeftEmployee = function () {
		$scope.newLeftEmployee = {
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
			LeftRemarks: ''
		};
	};

	$scope.DelLeftEmployee = function (refData) {

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
					EmployeeId: refData.EmployeeId,
					TranId: 0
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelLeftEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					$scope.GetAllLeftEmployeeList();
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};

	$scope.LeftEmpSelected = function () {

		//alert('ss');
	};
	$scope.GetAllLeftEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LeftEmployeeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetLeftEmployeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeftEmployeeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.CallSaveLeftEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newLeftEmployee.AttachmentFilesColl;


		if ($scope.newLeftEmployee.LeftDateDet) {
			$scope.newLeftEmployee.LeftDate_AD = $scope.newLeftEmployee.LeftDateDet.dateAD;
		} else
			$scope.newLeftEmployee.LeftDate_AD = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveLeftEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newLeftEmployee, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLeftEmployee();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

});