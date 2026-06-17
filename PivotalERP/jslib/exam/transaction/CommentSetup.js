
app.filter('commentWise', function ($filter) {
	return function (num)
	{		
		switch (num) {
			case 1:
				return "Percentage Wise";
			case 2:
				return "GPA Wise";
			case 3:
				return "Rank Wise";
			case 4:
				return "Result Wise"
			case 5:
				return "Grade Wise"
			default:
				return "";
		}
	}
});

app.filter('commentFor', function ($filter) {
	return function (num) {
		switch (num) {
			case 1:
				return "Pass";
			case 2:
				return "Fail";			
			default:
				return "";
		}
	}
});

app.controller('CommentSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Comment Setup';

	OnClickDefault();

	var glbS = GlobalServices;

	$scope.LoadData = function () {

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

		$scope.currentPages = {
			CommentSetup: 1			
		};

		$scope.searchData = {
			CommentSetup: ''		
		};

		$scope.perPage = {
			CommentSetup: glbS.getPerPageRow(),			
		};

		$scope.WiseColl = [
			{ id: 1, text: 'Percentage Wise' },
			{ id: 2, text: 'GPA Wise' },
			{ id: 3, text: 'Rank Wise' },
			{ id: 4, text: 'Result Wise' },
			{ id: 5, text: 'Grade Wise' },
		];

		$scope.newCommentSetup = {
			CommentSetupId: null,
			Wise: 1,
			ForStudent: 1,			
			MinVal: 0,
			MaxVal: 0,
			Comment: '',
			Mode: 'Save'
		};

		$scope.GetAllCommentSetupList();
		
	}

	function OnClickDefault() {


		document.getElementById('comment-setup-form').style.display = "none";

		//document.getElementById('gpa-fields').style.display = "none";
		//document.getElementById('rank-fields').style.display = "none";
		//document.getElementById('subject-fields').style.display = "none";
		//document.getElementById('result-fields').style.display = "none";
		//document.getElementById('student-fields').style.display = "none";
		//document.getElementById('gpwise').style.display = "none";
		//document.getElementById('orderno').style.display = "none";


		// comment setup section

		document.getElementById('add-comment-setup').onclick = function () {
			document.getElementById('comment-setup-section').style.display = "none";
			document.getElementById('comment-setup-form').style.display = "block";
			$scope.ClearCommentSetup();
		}

		document.getElementById('back-comment-setup').onclick = function () {
			document.getElementById('comment-setup-section').style.display = "block";
			document.getElementById('comment-setup-form').style.display = "none";
			$scope.ClearCommentSetup();
		}





	}

	$scope.ClearCommentSetup = function () {

		$timeout(function () {
			$scope.newCommentSetup = {
				CommentSetupId: null,
				Wise: 1,
				ForStudent: 1,
				MinVal: 0,
				MaxVal: 0,
				Comment: '',
				Mode: 'Save'
			};
		});		
	}
	


	//************************* Comment Setup*********************************

	$scope.IsValidCommentSetup = function () {
		if ($scope.newCommentSetup.Comment.isEmpty()) {
			Swal.fire('Please ! Enter Comment Details');
			return false;
		}
		if ($scope.newCommentSetup.Wise != 4 && $scope.newCommentSetup.Wise != 5) {
			// Minimum Value
			if ($scope.newCommentSetup.MinVal === null ||
				$scope.newCommentSetup.MinVal === undefined ||
				$scope.newCommentSetup.MinVal === "") {
				Swal.fire('Please enter Minimum Value');
				return false;
			}

			// Maximum Value
			if ($scope.newCommentSetup.MaxVal === null ||
				$scope.newCommentSetup.MaxVal === undefined ||
				$scope.newCommentSetup.MaxVal === "") {
				Swal.fire('Please enter Maximum Value');
				return false;
			}

			// Min <= Max check
			if (parseFloat($scope.newCommentSetup.MinVal) > parseFloat($scope.newCommentSetup.MaxVal)) {
				Swal.fire('Minimum Value cannot be greater than Maximum Value');
				return false;
			}
		}
		
		return true;
	}

	$scope.SaveUpdateCommentSetup = function () {
		if ($scope.IsValidCommentSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCommentSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCommentSetup();
					}
				});
			} else
				$scope.CallSaveUpdateCommentSetup();

		}
	};

	$scope.CallSaveUpdateCommentSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveCommentSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCommentSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCommentSetup();
				$scope.GetAllCommentSetupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCommentSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CommentSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllCommentSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CommentSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCommentSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetCommentSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCommentSetup = res.data.Data;
				$scope.newCommentSetup.Mode = 'Modify';

				document.getElementById('comment-setup-section').style.display = "none";
				document.getElementById('comment-setup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCommentSetupById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelCommentSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCommentSetupList();
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