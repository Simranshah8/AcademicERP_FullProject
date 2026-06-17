app.controller('IncomeExpenseClosingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'IncomeExpenseClosing';


	$scope.LoadData = function () {

		//$scope.PeriodList = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Attendance/Creation/GetAllFinancialPeriodList",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.PeriodList = res.data.Data;
		//	}
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


		$scope.CostClassColl = []; 		
		$scope.loadingstatus = 'running';
		showPleaseWait();
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetAllCostClasss",
			dataType: "json"
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			hidePleaseWait();
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.CostClassColl = res.data.Data;
				});
			} else
				alert(res.data.ResponseMSG);

		}, function (reason) {
			alert('Failed' + reason);
		});

		$scope.newData = {
			PreviousPeriodId: null,
			CurrentssPeriodId: null
		};
	}

	$scope.onFiscalYearChange = function () {
		var prevPeriod = mx($scope.CostClassColl).firstOrDefault(p => p.CostClassId == $scope.newData.PreviousPeriodId);
		var currPeriod = mx($scope.CostClassColl).firstOrDefault(p => p.CostClassId == $scope.newData.CurrentssPeriodId);

		if (prevPeriod) {
			$scope.newData.PreviousPeriodStartDate_TMP = new Date(prevPeriod.StartDate);
			$scope.newData.PreviousPeriodEndDate_TMP = new Date(prevPeriod.EndDate);
		}

		if (currPeriod) {
			$scope.newData.CurrentsStartDate_TMP = new Date(currPeriod.StartDate);
			$scope.newData.NY = currPeriod.StartMiti?.split('-')[0]; // Year: "2082"
			$scope.newData.NM = currPeriod.StartMiti?.split('-')[1]; // Month: "01"
			$scope.newData.ND = currPeriod.StartMiti?.split('-')[2];
		}
	};
	



	$scope.UpdateIncomeExpenseClosing = function () {
		var findP = mx($scope.CostClassColl).firstOrDefault(p1 => p1.CostClassId == $scope.newData.PreviousPeriodId);
		var findT = mx($scope.CostClassColl).firstOrDefault(p2 => p2.CostClassId == $scope.newData.CurrentssPeriodId);
		let fiscalYearText = findP ? findP.Name : 'Selected Fiscal Year';
		let TofiscalYearText = findT ? findT.Name : 'Selected Fiscal Year';
		Swal.fire({
			title: 'Do you want to adjust Income Expense Closing of Fiscal Year ' + fiscalYearText + ' to fiscal Year' + TofiscalYearText + '?',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var findP = mx($scope.CostClassColl).firstOrDefault(p1 => p1.CostClassId == $scope.newData.PreviousPeriodId);
				if (findP) {
					var DateFrom = $filter('date')(new Date(findP.StartDate), 'yyyy-MM-dd');
					var DateTo = $filter('date')(new Date(findP.EndDate), 'yyyy-MM-dd');
				}
				var findC = mx($scope.CostClassColl).firstOrDefault(p1 => p1.CostClassId == $scope.newData.CurrentssPeriodId);
				if (findC) {
					/*var VoucherDateFrom = $filter('date')(new Date(findP.StartDate), 'yyyy-MM-dd');*/
					var VoucherDateTo = $filter('date')(new Date(findT.StartDate), 'yyyy-MM-dd');
				}
				var para = {
					dateFrom: DateFrom,
					dateTo: DateTo,
					/*voucherDate: VoucherDateFrom,*/
					voucherDate: VoucherDateTo,
					ny: $scope.newData.NY,
					nm: $scope.newData.NM,
					nd: $scope.newData.ND
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/SetUp/ModifyIncomeExpensesLedgerAsZero",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					hidePleaseWait();
					Swal.fire('Failed: ' + reason);
				});
			}
		});
	};



});