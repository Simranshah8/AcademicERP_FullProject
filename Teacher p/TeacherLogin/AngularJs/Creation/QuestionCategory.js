

app.controller('QuestionCategoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Question Category';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();

		$scope.currentPages = {
			QuestionCategory: 1,

		};

		$scope.searchData = {
			QuestionCategory: '',

		};

		$scope.perPage = {
			QuestionCategory: GlobalServices.getPerPageRow(),

		};

		$scope.newQuestionCategory = {
			QuestionCategoryId: null,			
			ExamModalType: null,
			OrderNo: '',
			CategoryName: '',
			Description: '',
			Mode: 'Save'
		};

		//$scope.GetAllQuestionCategoryList();

	};


	$scope.ClearQuestionCategory = function () {
		$timeout(function () {
			$scope.newQuestionCategory = {
				QuestionCategoryId: null,
				ExamModalType: null,
				OrderNo: '',
				CategoryName: '',
				Description: '',
				Mode: 'Save'
			};
			$('.select2').val(null).trigger('change');

		})
		
	};



	function OnClickDefault() {
		document.getElementById('exam-setup-form').style.display = "none";  

		document.getElementById('add-exam-setup').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('exam-setup-form').style.display = "block";
		}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('exam-setup-form').style.display = "none";
		}
	};

	//************************* Class *********************************



	$scope.IsValidQuestionCategory = function () {
		if ($scope.newQuestionCategory.CategoryName.isEmpty()) {
			Swal.fire('Please ! Enter Category Name');
			return false;
		}

		return true;
	};


	//$('#ExamModalType').on("change", function (e) {
		
	//	$timeout(function () {
	//		var selected_element = $(e.currentTarget);
	//		var select_val = selected_element.val();
	//		$scope.newQuestionCategory.ExamModalType = select_val;
	//	})
		
	//});
	//$('#NumberingMethod').on("change", function (e) {

	//	$timeout(function () {
	//		var selected_element = $(e.currentTarget);
	//		var select_val = selected_element.val();
	//		$scope.newQuestionCategory.NumberingMethod = select_val;
	//	})

	//});

	$scope.SaveUpdateQuestionCategory = function () {
		if ($scope.IsValidQuestionCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newQuestionCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateQuestionCategory();
					}
				});
			} else
				$scope.CallSaveUpdateQuestionCategory();

		}
	};

	$scope.CallSaveUpdateQuestionCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		//if ($scope.newQuestionCategory.QuestionCategoryDateDet) {
		//	$scope.newQuestionCategory.QuestionCategoryDate = $scope.newQuestionCategory.QuestionCategoryDateDet.dateAD;
		//} else
		//	$scope.newQuestionCategory.QuestionCategoryDate = null;


		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Creation/AddExamModal",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newQuestionCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearQuestionCategory();
				$scope.GetAllQuestionCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllQuestionCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.QuestionCategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Creation/GetAllExamModalList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.QuestionCategoryList = res.data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetQuestionCategoryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CategoryId: refData.CategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Creation/GetQuestionCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data) {
				$scope.newQuestionCategory = res.data;
				$scope.newQuestionCategory.ExamModalType = res.data.ExamModalType;
				$scope.newQuestionCategory.NumberingMethod = res.data.NumberingMethod;


				$scope.newQuestionCategory.Mode = 'Modify';

				document.getElementById('table-listing').style.display = "none";
				document.getElementById('exam-setup-form').style.display = "block";
				//$('#ExamModalType').val($scope.newQuestionCategory.ExamModalType).trigger('change');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelQuestionCategoryById = function (refData) {

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
					CategoryId: refData.CategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "OnlineExam/Creation/DelQuestionCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllQuestionCategoryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

});