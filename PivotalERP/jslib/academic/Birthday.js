app.controller('BirthdayController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Birthday';

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			StudentBirthday: 1,
			EmployeeBirthday: 1
			

		};

		$scope.searchData = {
			StudentBirthday: '',
			EmployeeBirthday: ''
		};

		$scope.perPage = {
			StudentBirthday: GlobalServices.getPerPageRow(),
			EmployeeBirthday: GlobalServices.getPerPageRow()
			

		};

		$scope.newStudentBirthday = {
			StudentBirthdayId: null,

		};

		$scope.newEmployeeBirthday = {
			EmployeeBirthdayId: null,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,

		};

	}

	$scope.ClearStudentBirthday = function () {
		$scope.newStudentBirthday = {
			StudentBirthdayId: null,

		};
	}
	$scope.ClearEmployeeBirthday = function () {
		$scope.newEmployeeBirthday = {
			EmployeeBirthdayId: null,

		};
	}
	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});