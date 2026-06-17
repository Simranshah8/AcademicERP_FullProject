app.controller('StudentProtofolioController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'Student Protofolio';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();


		$scope.currentPages = {
			StudentPortfolio: 1

		};

		$scope.perPage = {
			StudentPortfolio: GlobalServices.getPerPageRow()
		};

		$scope.searchData = {
			StudentPortfolio: ''
		};

		$scope.MonthList = GlobalServices.getMonthList();


		//Company Details
		$scope.CompanyConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CompanyConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//logo
		$scope.Logo = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Logo = res.data.Data[0];
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentPortfolio =
		{
			ShowLeftStudent: false,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			StudentId: null,
			MonthId: null
		};


	}

	$scope.ClearStdPortfolio = function () {
		$scope.newStudentPortfolio =
		{
			ShowLeftStudent: false,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			StudentId: null,
			MonthId: null
		};
	}
	$scope.updateMonthName = function () {
		var selectedMonth = $scope.MonthList.find(function (item) {
			return item.id === $scope.newStudentPortfolio.MonthId;
		});
		if (selectedMonth) {
			$scope.newStudentPortfolio.MonthName = selectedMonth.text;
		} else {
			$scope.newStudentPortfolio.MonthName = '';
		}
	};

	$scope.getStudentPortfolio = function () {
		// Display Profile
		var para =
		{
			StudentId: $scope.newStudentPortfolio.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentProfile",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.newStudentPortfolio = res1.data.Data;

				$timeout(function () {
					const qrcodeContainer = document.getElementById("qrcode");
					qrcodeContainer.innerHTML = ""; // Clear previous QR code if any

					if ($scope.newStudentPortfolio.QrCode) {
						new QRCode(qrcodeContainer, {
							text: $scope.newStudentPortfolio.QrCode,
							width: 140,
							height: 140,
							colorDark: "#000000",
							colorLight: "#ffffff",
							correctLevel: QRCode.CorrectLevel.L
						});
					}
				});
				if ($scope.newStudentPortfolio.PhotoPath && $scope.newStudentPortfolio.PhotoPath.length == 0)
					$scope.newStudentPortfolio.PhotoPath = '/wwwroot/dynamic/images/avatar-img.png';
				else if (!$scope.newStudentPortfolio.PhotoPath)
					$scope.newStudentPortfolio.PhotoPath = '/wwwroot/dynamic/images/avatar-img.png';
				$scope.newStudentPortfolio.SelectStudent=$scope.StudentSearchOptions[0].value;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.PrintData = function () {
		$('#printcard').printThis();

	};



});