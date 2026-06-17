String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};
app.controller('NoticeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Notice';

	OnClickDefault();
	$scope.LoadData = function () {

		$scope.searchData = {
			Send: 'Send',
			Received:'Received'
		};

		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				exDialog.openMessage({
					scope: $scope,
					title: $scope.Title,
					icon: "info",
					message: 'Failed: ' + reason
				});
			});



		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
	

		$scope.newNotice = {
			NoticeId: null,
			Title: '',
			Detail: '',
			ClassId: null,
			SectionId: null,
			Mode: 'Save'
		};

		$scope.GetAllNoticeList();
		$scope.GetAllEventHoliday();

	};

	function OnClickDefault() {
		document.getElementById('notification-details').style.display = "none";

		//document.getElementById('notification').onclick = function () {
		//	document.getElementById('notification-details').style.display = "block";
		//	document.getElementById('notification-list').style.display = "none";
		//}

		//document.getElementById('back-to-list').onclick = function () {
		//	document.getElementById('notification-list').style.display = "block";
		//	document.getElementById('notification-details').style.display = "none";
		//}

	}

	$('#cboClassId').on("change", function (e) {
		//$('#cboSectionId').val(null).trigger('change');
		$scope.SectionColl = [];
		$scope.SectionList = [];
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.ClassId = select_val
		angular.forEach($scope.SectionClassColl, function (SVCollData) {
			if (select_val == SVCollData.ClassId) {

				$scope.Section = SVCollData;
				$scope.SectionColl.push($scope.Section);
			}


		})
		$timeout(function () {
			$scope.SectionList = $scope.SectionColl;
		});



	});
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
			method: 'GET',
			url: base_url + "Notice/Creation/GetAllNoticeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.NoticeList = res.data;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllEventHoliday = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EventHolidayList = [];

		$http({
			method: 'GET',
			url: base_url + "Notice/Creation/GetUpcomingEventHoliday",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.EventHolidayList = res.data;

			} else {
				Swal.fire(res.data);
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

	$scope.downloadFilePath = '';
	$scope.DocumentAtt_Toggle = function (fpath) {
		if (fpath && fpath !== '') {
			$scope.downloadFilePath = WEBURLPATH + fpath;
			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src = WEBURLPATH + fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}

	};

});