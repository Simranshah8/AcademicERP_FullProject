app.controller('GradeDivisionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Grade Division';

	OnClickDefault();

	
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Division: 1,
			Grade: 1
		
		};

		$scope.searchData = {
			Division: '',
			Grade: ''
			

		};

		$scope.perPage = {
			Division: GlobalServices.getPerPageRow(),
			Grade: GlobalServices.getPerPageRow()
		
		};

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newDivision = {
			DivisionId: null,
			Name: '',
			Description: '',
			MinPer: 0,
			MaxPer: 0,
			ClassId:null,
			Mode: 'Save'
		};


		$scope.newGrade = {
			GradeId: null,
			Name: '',
			Description: '',
			MinPer: 0,
			MaxPer: 0,
			GradePoint: 0,
			SubjectType: 1,
			ClassId: null,
			Mode: 'Save'
		};
		

		$scope.GetAllDivisionList();
		$scope.GetAllGradeList();
		


	}

	function OnClickDefault() {

		document.getElementById('division-form').style.display = "none";
		document.getElementById('grade-form').style.display = "none";

		// division section

		document.getElementById('add-division').onclick = function () {
			document.getElementById('division-section').style.display = "none";
			document.getElementById('division-form').style.display = "block";
		}

		document.getElementById('back-division').onclick = function () {
			document.getElementById('division-section').style.display = "block";
			document.getElementById('division-form').style.display = "none";
		}

		// grade section

		document.getElementById('add-grade').onclick = function () {
			document.getElementById('grade-section').style.display = "none";
			document.getElementById('grade-form').style.display = "block";
		}

		document.getElementById('back-grade').onclick = function () {
			document.getElementById('grade-section').style.display = "block";
			document.getElementById('grade-form').style.display = "none";
		}

	};

	$scope.ClearDivision = function () {
		$scope.newDivision = {
			DivisionId: null,
			Name: '',
			Description: '',
			MinPer: 0,
			MaxPer: 0,
			ClassId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearGrade = function () {
		$scope.newGrade = {
			DivisionId: null,
			Name: '',
			Description: '',
			MinPer: 0,
			MaxPer: 0,
			GradePoint: 0,
			SubjectType: 1,
			ClassId: null,
			Mode: 'Save'
		};
		
	}


	//************************* Division *********************************

	$scope.IsValidDivision = function () {
		//if (!$scope.newDivision.ClassId || $scope.newDivision.ClassId === 0) {
		//	Swal.fire('Please select Class');
		//	return false;
		//}

		if ($scope.newDivision.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		if ($scope.newDivision.MinPer === null || $scope.newDivision.MinPer === undefined || $scope.newDivision.MinPer === "") {
			Swal.fire('Please enter Minimum Percentage');
			return false;
		}

		if ($scope.newDivision.MaxPer === null || $scope.newDivision.MaxPer === undefined || $scope.newDivision.MaxPer === "") {
			Swal.fire('Please enter Maximum Percentage');
			return false;
		}
		if (parseFloat($scope.newDivision.MinPer) > parseFloat($scope.newDivision.MaxPer)) {
			Swal.fire('Minimum Percentage cannot be greater than Maximum Percentage');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateDivision = function () {
		if ($scope.IsValidDivision() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDivision.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDivision();
					}
				});
			} else
				$scope.CallSaveUpdateDivision();

		}
	};

	$scope.CallSaveUpdateDivision = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveDivision",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDivision }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDivision();
				$scope.GetAllDivisionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

$scope.GetAllDivisionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DivisionList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllDivisionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DivisionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

$scope.GetDivisionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DivisionId: refData.DivisionId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetDivisionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDivision = res.data.Data;
				$scope.newDivision.Mode = 'Modify';

				document.getElementById('division-section').style.display = "none";
				document.getElementById('division-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDivisionById = function (refData) {

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
					DivisionId: refData.DivisionId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelDivision",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDivisionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Grade *********************************

	$scope.IsValidGrade = function () {
		// Class validation
		//if (!$scope.newGrade.ClassId || $scope.newGrade.ClassId === 0) {
		//	Swal.fire('Please select Class');
		//	return false;
		//}
				// Name validation
		if (!$scope.newGrade.Name || $scope.newGrade.Name.trim() === "") {
			Swal.fire('Please enter Name');
			return false;
		}
				
		if ($scope.newGrade.MinPer === null || $scope.newGrade.MinPer === undefined || $scope.newGrade.MinPer === "") {
			Swal.fire('Please enter Minimum Percentage');
			return false;
		}
		if ($scope.newGrade.MaxPer === null || $scope.newGrade.MaxPer === undefined || $scope.newGrade.MaxPer === "") {
			Swal.fire('Please enter Maximum Percentage');
			return false;
		}

		if (parseFloat($scope.newGrade.MinPer) > parseFloat($scope.newGrade.MaxPer)) {
			Swal.fire('Minimum Percentage cannot be greater than Maximum Percentage');
			return false;
		}		

		return true; 
	};


	$scope.SaveUpdateGrade = function () {
		if ($scope.IsValidGrade() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newGrade.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGrade();
					}
				});
			} else
				$scope.CallSaveUpdateGrade();

		}
	};

$scope.CallSaveUpdateGrade = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveGrade",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newGrade }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearGrade();
				$scope.GetAllGradeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllGradeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GradeList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllGradeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GradeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetGradeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			GradeId: refData.GradeId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetGradeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newGrade = res.data.Data;
				$scope.newGrade.Mode = 'Modify';

				document.getElementById('grade-section').style.display = "none";
				document.getElementById('grade-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelGradeById = function (refData) {

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
					GradeId: refData.GradeId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelGrade",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllGradeList();
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