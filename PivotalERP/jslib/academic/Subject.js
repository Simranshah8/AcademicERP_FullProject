app.controller('SubjectController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Subject';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			Subject: 1
			
		};

		$scope.searchData = {
			Subject: ''
			
		};

		$scope.perPage = {
			Subject: GlobalServices.getPerPageRow(),
			
		};

		$scope.newSubject = {
			SubjectId: null,
			Name: '',
			Code: '',
			CodeTH: '',
			CodePR: '',
			IsECA: false,
			IsMath: false,
			CRTH: 0,
			CRPR: 0,
			CR:0,
			Mode: 'Save'
		};

		$scope.GetAllSubjectList();
	
	}

	function OnClickDefault() {


		document.getElementById('add-subject-form').style.display = "none";


		document.getElementById('add-subject').onclick = function () {
			document.getElementById('add-subject-section').style.display = "none";
			document.getElementById('add-subject-form').style.display = "block";
			$scope.ClearSubject();
		}
		document.getElementById('back-to-subject-list-btn').onclick = function () {
			document.getElementById('add-subject-form').style.display = "none";
			document.getElementById('add-subject-section').style.display = "block";
			$scope.ClearSubject();

		
		}

	}

	$scope.ClearSubject = function () {
		$scope.newSubject = {
			SubjectId: null,
			Name: '',
			Code: '',
			CodeTH: '',
			CodePR: '',
			IsECA: false,
			IsMath: false,
			CRTH: 0,
			CRPR: 0,
			CR: 0,
			Mode: 'Save'
		};
	}
	

	//************************* Subject *********************************

	$scope.IsValidSubject = function () {
		if ($scope.newSubject.Name.isEmpty()) {
			Swal.fire('Please ! Enter Subject Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateSubject = function () {
		if ($scope.IsValidSubject() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSubject.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSubject();
					}
				});
			} else
				$scope.CallSaveUpdateSubject();

		}
	};

	$scope.CallSaveUpdateSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSubject",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSubject }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSubject();
				$scope.GetAllSubjectList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSubjectById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SubjectId: refData.SubjectId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSubject = res.data.Data;
				$scope.newSubject.Mode = 'Modify';

				document.getElementById('add-subject-section').style.display = "none";
				document.getElementById('add-subject-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSubjectById = function (refData) {

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
					SubjectId: refData.SubjectId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelSubject",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSubjectList();
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