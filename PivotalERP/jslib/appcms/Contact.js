app.controller('ContactController', function ($scope, $http, $sce, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Contact';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.newContact = {
			ContactId: null,
			Address: '',
			ContactNo: '',
			EmailId: '',
			OpeningHours: '',
			MapUrl: '',
			Mode: 'Save'
		};

		$scope.GetContact();
	}

	function OnClickDefault() {
		document.getElementById('contact-form').style.display = "none";

		// Contact Detail
		document.getElementById('edit-contact-detail').onclick = function () {
			document.getElementById('contact-detail').style.display = "none";
			document.getElementById('contact-form').style.display = "block";

		}
		document.getElementById('back-to-contact-detail').onclick = function () {
			document.getElementById('contact-detail').style.display = "block";
			document.getElementById('contact-form').style.display = "none";
		}
	};




	$scope.ClearContact = function () {
		$scope.newContact = {
			ContactId: null,
			Address: '',
			ContactNo: '',
			EmailId: '',
			OpeningHours: '',
			MapUrl: '',
			Mode: 'Save'
		};
	}





	/*Contact Tab Js*/
	$scope.IsValidContact = function () {
		if ($scope.newContact.Address.isEmpty()) {
			Swal.fire('Please ! Enter Address');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateContact = function () {
		if ($scope.IsValidContact() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newContact.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateContact();
					}
				});
			} else
				$scope.CallSaveUpdateContact();

		}
	};

	$scope.CallSaveUpdateContact = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveContact",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},

			data: { jsonData: $scope.newContact }
		}).then(function (res) {

			//Added by Shishant on Dec 19, 2024
			if (res.data.IsSuccess) {
				$scope.newContact.SecureMapUrl = $scope.newContact.SecureMapUrlPreview;
			}
			//

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetContact = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetContact",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			//Modified by Shishant on Dec 19, 2024.
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newContact = res.data.Data;
				$scope.newContact.Mode = 'Save';
				$scope.onMapUrlFetch();
				//

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	//Added by Shishant on Dec 19, 2024.
	$scope.onMapUrlChange = function () {
		if ($scope.newContact.MapUrl) {
			var match = $scope.newContact.MapUrl.match(/<iframe[^>]*src="([^"]+)"/);

			if (match && match[1]) {
				$scope.newContact.SecureMapUrlPreview = $sce.trustAsResourceUrl(match[1]);
			} else {
				$scope.newContact.SecureMapUrlPreview = null;
			}
		}
	};
	$scope.onMapUrlFetch = function () {
		if ($scope.newContact.MapUrl) {
			var match = $scope.newContact.MapUrl.match(/<iframe[^>]*src="([^"]+)"/);
			if (match && match[1]) {
				$scope.newContact.SecureMapUrl = $sce.trustAsResourceUrl(match[1]);
			} else {
				$scope.newContact.SecureMapUrl = null;
			}
		}
	};
	//


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
});