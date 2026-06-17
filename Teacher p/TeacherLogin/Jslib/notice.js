
app.controller('NoticeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Notice';

	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();

		//$scope.currentPages = {
		//	Notice: 1,

		//};

		//$scope.searchData = {
		//	Notice: '',

		//};

		//$scope.perPage = {
		//	Notice: GlobalServices.getPerPageRow(),

		//};

		$scope.newNotice = {
			NoticeId: null,
			Title: '',
			Detail: '',
			ClassId: null,
			SectionId: null,
			Mode: 'Save'
		};

		//$scope.GetAllNoticeList();

	};


	$scope.ClearNotice = function () {
		$scope.newNotice = {
			NoticeId: null,
			Title: '',
			Detail: '',
			ClassId: null,
			SectionId: null,
			Mode: 'Save'
		};
	};



	
	//************************* Class *********************************

	$scope.IsValidNotice = function () {
		if ($scope.newNotice.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}

		

		return true;
	};


	
	



	$scope.SaveUpdateNotice = function () {
		if ($scope.IsValidNotice() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newNotice.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateNotice();
					}
				});
			} else
				$scope.CallSaveUpdateNotice();

		}
	};

	$scope.CallSaveUpdateNotice = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newNotice.NoticeDateDet) {
			$scope.newNotice.NoticeDate = $scope.newNotice.NoticeDateDet.dateAD;
		} else
			$scope.newNotice.NoticeDate = null;


		$http({
			method: 'POST',
			url: base_url + "Notice/Creation/SaveNotice",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newNotice }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearNotice();
				$scope.GetAllNoticeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllNoticeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.NoticeList = [];

		$http({
			method: 'POST',
			url: base_url + "Notice/Creation/GetAllNoticeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.NoticeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetNoticeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			NoticeId: refData.NoticeId
		};

		$http({
			method: 'POST',
			url: base_url + "Notice/Creation/GetNoticeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newNotice = res.data.Data;
				$scope.newNotice.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelNoticeById = function (refData) {

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
					NoticeId: refData.NoticeId
				};

				$http({
					method: 'POST',
					url: base_url + "Notice/Creation/DelNotice",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllNoticeList();
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