app.controller('FeedbackSuggestionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Feedback/Suggestion';


	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Feedback: 1
		};

		$scope.searchData = {
			Feedback: ''
		};

		$scope.perPage = {
			Feedback: GlobalServices.getPerPageRow(),
		};



		$scope.newFeedback = {
			FeedbackId: null,
			Mode: 'Save'
		};


		$scope.GetAllFeedbackList();
	}
	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.ImagePath && item.ImagePath.length > 0) {
			$scope.viewImg.ContentPath = item.ImagePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};
	$scope.GetAllFeedbackList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeedbackList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetFeedbackList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeedbackList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}


	$scope.currentFeedback = null;
	$scope.ShowResponeDialog = function (feedback) {
		$scope.currentFeedback = feedback;
		$('#replyFeedback').modal('show');
	}

	$scope.UpdateFeedback = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/UpdateFeedback",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.currentFeedback }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
});