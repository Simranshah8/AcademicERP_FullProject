

app.controller('SeatAllotmentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Seat Allotment';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			SeatAllotment: 1,

		};

		$scope.searchData = {
			SeatAllotment: '',

		};

		$scope.perPage = {
			SeatAllotment: GlobalServices.getPerPageRow(),

		};

		$scope.MediumList = [];
		GlobalServices.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newSeatAllotment = {
			SeatAllotmentId: null,
			NewQuota: '',
			SeatAllotmentDetailColl: [],
			sectionwise: false,
			Mode: 'Save'
		};


		$scope.newSeatAllotment.SeatAllotmentDetailColl.push({});
		//$scope.GetAllSeatAllotmentList();
	
	};




	function OnClickDefault() {
		document.getElementById('seat-allotment-form').style.display = "none";
		

		document.getElementById('add-seat-allotment-btn').onclick = function () {
			document.getElementById('seat-allotment-listing').style.display = "none";
			document.getElementById('seat-allotment-form').style.display = "block";

		}
		document.getElementById('back-allotment-list').onclick = function () {
			document.getElementById('seat-allotment-listing').style.display = "block";

			document.getElementById('seat-allotment-form').style.display = "none";
		}
	}


	$scope.ClearSeatAllotment = function () {
		$scope.newSeatAllotment = {
			SeatAllotmentId: null,
			NewQuota: '',
			SeatAllotmentDetailColl: [],
			Mode: 'Save'
		};
		$scope.newSeatAllotment.SeatAllotmentDetailColl.push({});
	};


/*Add and Delete Button*/
	$scope.AddSeatAllotmentDetail = function (ind) {
		if ($scope.newSeatAllotment.SeatAllotmentDetailColl) {
			if ($scope.newSeatAllotment.SeatAllotmentDetailColl.length > ind + 1) {
				$scope.newSeatAllotment.SeatAllotmentDetailColl.splice(ind + 1, 0, {
					NewQuota: ''
				})
			} else {
				$scope.newSeatAllotment.SeatAllotmentDetailColl.push({
					NewQuota: ''
				})
			}
		}
	};
	$scope.delSeatAllotmentDetail = function (ind) {
		if ($scope.newSeatAllotment.SeatAllotmentDetailColl) {
			if ($scope.newSeatAllotment.SeatAllotmentDetailColl.length > 1) {
				$scope.newSeatAllotment.SeatAllotmentDetailColl.splice(ind, 1);
			}
		}
	};

	

	
	$scope.SaveUpdateSeatAllotment = function () {
		if ($scope.IsValidSeatAllotment() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSeatAllotment.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSeatAllotment();
					}
				});
			} else
				$scope.CallSaveUpdateSeatAllotment();

		}
	};

	$scope.CallSaveUpdateSeatAllotment = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveSeatAllotment",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSeatAllotment }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSeatAllotment();
				$scope.GetAllSeatAllotmentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllSeatAllotmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SeatAllotmentList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllSeatAllotmentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SeatAllotmentList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetSeatAllotmentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SeatAllotmentId: refData.SeatAllotmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetSeatAllotmentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSeatAllotment = res.data.Data;
				$scope.newSeatAllotment.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.DelSeatAllotmentById = function (refData) {

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
					SeatAllotmentId: refData.SeatAllotmentId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelSeatAllotment",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSeatAllotmentList();
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