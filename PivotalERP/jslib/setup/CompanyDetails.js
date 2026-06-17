
app.controller('CompanyDetailsController', function ($scope, $http, $timeout, $filter, $rootScope, $translate, GlobalServices) {
	$scope.Title = 'Company Details';
	$rootScope.ChangeLanguage();
	

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.newCompanyDetails = {
			CompanyDetailsId: null,
			Name: '',
			DisplayName: '',
			Address: '',
			District: '',
			State: '',
			Zone: '',
			Country: '',
			ZipCode: null,
			RegdNo: null,
			PanVatNo: null,

			Photo: null,
			PhotoPath: null,
			
			PhoneNo1: '',
			PhoneNo2: '',
			FaxNo: '',
			EmailId: '',
			Website: '',

			Mode: 'Save'
		};

		$scope.GetCompanyDetails();
		
	}

	$scope.IsValidCompanyDetails = function () {
		if ($scope.newCompanyDetails.Name.isEmpty()) {
			Swal.fire('Please ! Enter Company Name');
			return false;
		}
	
		return true;
	}

	$scope.SaveUpdateCompanyDetails = function () {
		if ($scope.IsValidCompanyDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCompanyDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCompanyDetails();
					}
				});
			} else
				$scope.CallSaveUpdateCompanyDetails();

		}
	};

	$scope.CallSaveUpdateCompanyDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		 

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveUpdateCompanyDet",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCompanyDetails }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			if (res.data.IsSuccess == true) {

				var keyLang = 'np';
				if ($scope.newCompanyDetails.Country == 'Nepal') {
					keyLang = 'np';
				}
				else if ($scope.newCompanyDetails.Country == 'India') {
					keyLang = 'in';
				}
				$rootScope.LANG = keyLang;
				$translate.use(keyLang); 
				
            }

			Swal.fire(res.data.ResponseMSG);
		
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	
	}

	$scope.GetCompanyDetails= function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newCompanyDetails = {};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCompanyDetails = res.data.Data;				 

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


});