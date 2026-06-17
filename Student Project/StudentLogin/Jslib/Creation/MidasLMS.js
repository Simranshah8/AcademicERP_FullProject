app.controller('midasLMSController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'LMS';
	var glbS = GlobalServices;
	LoadData();

    function LoadData() {

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetMidasURL",
			dataType: "json",
			//data: para
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			if (res.data) {
				document.getElementById("frmLMS").src = res.data.ResponseMSG;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);

		});
    }

});