
app.controller('StudentListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'StudentList For Exam';


	$scope.LoadData = function () {
		$('.select2').select2();
		var glbS = GlobalServices;
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			StudentList: 1,
		};

		$scope.searchData = {
			StudentList: '',
		};

		$scope.perPage = {
			StudentList: GlobalServices.getPerPageRow(),
		};

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newDet = {
			GenerateId: null,
			StartNo: '',
			PadWidth: '',
			Prefix: '',
			Suffix: '',
			Mode: 'Save'
		};

		/*$scope.GetAllStudentList();*/
	};


	$scope.GetAllStudentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllStudentList = [];
		var para = {			
			SubjectId: $scope.newDet.SubjectId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/GetAllStudentist",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllStudentList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	
});