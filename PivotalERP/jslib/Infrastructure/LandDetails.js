app.controller('LandDetailsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'LandDetails';

	OnClickDefault();
	$scope.LoadData = function () {

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			LandDetails: 1,
		};


		$scope.searchData = {
			LandDetails: '',
		};

		$scope.perPage = {
			LandDetails: GlobalServices.getPerPageRow(),
		};

		$scope.newDet =
		{
			LandDetailsId: '',
			TotalArea: '',
			OwnerShipId: null,
			OtherOwnerShip: '',
			UtilizationId: null,
			OtherUtilizationType: '',
			Attachment_TMP: '',
			AttachmentData: '',
			Attachment:'',
			LandRemarks: '',
			Mode: 'Save'
		};

		$scope.UtilizationColl = [
			{ id: 1, text: 'Academic' },
			{ id: 2, text: 'Administrative' },
			{ id: 3, text: 'Playground' },
			{ id: 4, text: 'Hostel' },
			{ id: 5, text: 'Others' }
		];
		$scope.OwnerShipDetcoll = [
			{ id: 1, text: 'Own' },
			{ id: 2, text: 'Rented' },
			{ id: 3, text: 'Leased' },
			{ id: 4, text: 'Grant' },
			{ id: 5, text: 'Donated' },
			{ id: 6, text: 'Shared' },
			{ id: 7, text: 'Others' },
		];

		$scope.GetAllLandDetailsList();

	};

	$scope.ClearLandDetails = function () {
		$scope.ClearAtt();
		$scope.newDet = {
			LandDetailsId: '',
			TotalArea: '',
			OwnerShipId:null,
			OtherOwnerShip: '',
			UtilizationId: null,
			OtherUtilizationType: '',
			Attachment_TMP: '',
			AttachmentData: '',
			Attachment: '',
			LandRemarks: '',
			Mode: 'Save'
		};
		
	}

	$scope.ClearAtt = function () {
		$scope.newDet.Attachment = null;
		$scope.newDet.Attachment_TMP = '';
		$scope.newDet.AttachmentData = '';
		var fileInput = document.getElementById("choose-file");
		if (fileInput) {
			fileInput.value = "";
		}
		var imgElement = document.getElementById("imgPhotoAttachment");
		if (imgElement) {
			imgElement.src = "";
		}
	};

	function OnClickDefault() {
		document.getElementById('add-Utilities-form').style.display = "none";
		

		document.getElementById('add-Utilities').onclick = function () {
			document.getElementById('Utilities-table').style.display = "none";
			document.getElementById('add-Utilities-form').style.display = "block";
		}
		document.getElementById('deviceback-btn').onclick = function () {
			document.getElementById('Utilities-table').style.display = "block";
			document.getElementById('add-Utilities-form').style.display = "none";
		}
		
	}


	$scope.ShowPersonalImg3 = function (item) {
		$scope.viewImg0 = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg0.ContentPath = item.DocPath;
			$scope.viewImg0.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer0').src = item.DocPath;
			$('#PersonalImg3').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg0.ContentPath = item.PhotoPath;
			$scope.viewImg0.FileType = 'image';  // Assuming PhotoPath is for images
			$('#PersonalImg3').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg0.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg0.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg0.FileType === 'pdf') {
				document.getElementById('pdfViewer0').src = $scope.viewImg0.ContentPath;
			}

			$('#PersonalImg3').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};


	//************************* LandDetails*********************************
	$scope.IsValidLandDetails = function () {
		//if ($scope.newDet.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateLandDetails = function () {
		if ($scope.IsValidLandDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLandDetails();
					}
				});
			} else
				$scope.CallSaveUpdateLandDetails();

		}
	};

	$scope.CallSaveUpdateLandDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var photo = $scope.newDet.Attachment_TMP;

		if ($scope.newDet.OwnerShipId !== 7) {
			$scope.newDet.OtherOwnerShip = "";
		}

		if ($scope.newDet.UtilizationId !== 5) {
			$scope.newDet.OtherUtilizationType = "";
		}
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Creation/SaveUpdateLandDetails",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				return formData;
			},
			data: { jsonData: $scope.newDet, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLandDetails();
				$scope.GetAllLandDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllLandDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LandDetailsList = [];
		$http({
			method: 'GET',
			url: base_url + "Infrastructure/Creation/GetAllLandDetails",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LandDetailsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetLandDetailsById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			LandDetailsId: refData.LandDetailsId
		};
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Creation/getLandDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;
				$scope.newDet.Mode = 'Modify';

				document.getElementById('Utilities-table').style.display = "none";
				document.getElementById('add-Utilities-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetAtt = function (item) {
		$scope.viewImg2 = {
			ContentPath: '',
			FileType: null
		};

		if (item.Attachment && item.Attachment.length > 0) {
			$scope.viewImg2.ContentPath = item.Attachment;
			$scope.viewImg2.FileType = 'pdf';
			document.getElementById('pdfViewer2').src = item.Attachment;
			$('#PersonalImg3').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg2.ContentPath = item.PhotoPath;
			$scope.viewImg2.FileType = 'image';
			$('#PersonalImg3').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg2.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg2.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg2.FileType === 'pdf') {
				document.getElementById('pdfViewer2').src = $scope.viewImg2.ContentPath;
			}

			$('#PersonalImg3').modal('show');
		} else {
			Swal.fire('No Any Attachment Found');
		}
	};


	

	$scope.DelLandDetailsById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { LandDetailsId: refData.LandDetailsId };
				$http({
					method: 'POST',
					url: base_url + "Infrastructure/Creation/DeleteLandDetails",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.LandDetailsList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	}

});