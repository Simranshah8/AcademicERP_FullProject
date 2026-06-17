
app.controller('AdmitCardController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Admit Card';
	
	$scope.LoadData = function () {		
		$scope.GetVerifyById();
	};
	
	$scope.GetVerifyById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: 49
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getScholarshipById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newScholarship = res.data.Data;				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.Print = function () {
		$('#admitcard').printThis();
	}

});