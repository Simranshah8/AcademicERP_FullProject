app.controller('marksheetRptController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Marksheet';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

		$scope.entity = {
			DailyVoucher: DAILYVOUCHERENTITYID
		};
		$('.select2').select2();
		 

		$scope.newDailyVoucher = {
			FromDate_TMP: new Date(),
			ToDate_TMP:new Date(),
			TemplatesColl: [],
			RptTranId: 0,
		};

		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetVoucherList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newDailyVoucher.VoucherColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		 
		$scope.LoadReportTemplates();
	}

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.DailyVoucher + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newDailyVoucher.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		 
	}

	$scope.PrintVoucher = function () {
		var EntityId = $scope.entity.DailyVoucher;
		   
		if ($scope.newDailyVoucher.RptTranId > 0) { 
			var rptPara = {
				rpttranid: $scope.newDailyVoucher.RptTranId,
				istransaction: false,
				entityid: EntityId,
				FromDate: $filter('date')(new Date($scope.newDailyVoucher.FromDateDet.dateAD), 'yyyy-MM-dd'),
				ToDate: $filter('date')(new Date($scope.newDailyVoucher.ToDateDet.dateAD), 'yyyy-MM-dd') ,
				Period: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val(),
				VoucherIdColl: ($scope.newDailyVoucher.VoucherIdColl ? $scope.newDailyVoucher.VoucherIdColl.toString() : "")
			};
			var paraQuery = param(rptPara);
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
			document.body.style.cursor = 'default';
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}

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
		}
	};

});