app.controller('DownloadController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Download';

	$timeout(function () {
		OnClickDefault();
	}, 0);
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			AddDownload: 1,
		};

		$scope.searchData = {
			AddDownload: '',
		};

		$scope.perPage = {
			AddDownload: GlobalServices.getPerPageRow(),
		};
		$scope.GetAllDownloads();
		$scope.DownloadData = {
			TranId: '',
			Title: '',
			AttachmentName: '',
			OrderNo:0,
			IsActive:'',
			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('download-form').style.display = "none";

		document.getElementById('add-download').onclick = function () {
			document.getElementById('download-table').style.display = "none";
			document.getElementById('download-form').style.display = "block";
		}
		document.getElementById('download-back-btn').onclick = function () {
			document.getElementById('download-form').style.display = "none";
			document.getElementById('download-table').style.display = "block";
		}
	}


	$scope.ClearAttachments = function () {
		$scope.DownloadData.Attachment = null;
		$scope.DownloadData.Attachment_TMP = '';
		$scope.DownloadData.AttachmentData = '';
		$scope.DownloadData.AttachmentPath = '';

		// Reset the file input field
		var fileInput = document.getElementById("attachment1");
		if (fileInput) {
			fileInput.value = "";
		}

		// Reset the image preview
		var imgElement = document.getElementById("imgPhotoAttachment");
		if (imgElement) {
			imgElement.src = "";
		}

		// Apply changes to scope
		$scope.$applyAsync(); // Ensures UI updates in the next digest cycle
	};

	$scope.ClearDetails = function () {
		$scope.ClearAttachments();
		$scope.DownloadData = {
			TranId: '',
			Title: '',
			AttachmentName: '',
			OrderNo: 0,
			IsActive: true,
			Mode: 'Save'
		};

		$scope.$applyAsync(); // Ensure UI updates
	};

	


	$scope.IsValidAddDownload = function () {
		return true;
	}

	$scope.SaveUpdateAddDetails = function () {
		if ($scope.IsValidAddDownload() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.DownloadData.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDetails();
					}
				});
			} else
				$scope.CallSaveUpdateDetails();
		}
	};

	$scope.CallSaveUpdateDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var attachment = $scope.DownloadData.Attachment_TMP;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveDownload",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.downloadAttachment && data.downloadAttachment.length > 0)
					formData.append("attachment", data.downloadAttachment[0]);
				return formData;
			},
			data: { jsonData: $scope.DownloadData, downloadAttachment: attachment }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearDetails();
				$scope.GetAllDownloads();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllDownloads = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DownloadDataList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetDownloads",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DownloadDataList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetDownloadById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetDownloadById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DownloadData = res.data.Data;

				$scope.DownloadData.Mode = 'Modify';

				document.getElementById('download-table').style.display = "none";
				document.getElementById('download-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DeleteDownload = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DeleteDownload",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.ClearDetails();
						$scope.GetAllDownloads();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.ShowAttachment = function (attachment) {
		$scope.viewAttachment = {
			ContentPath: '',
			FileType: null
		};

		if (attachment.AttachmentPath && attachment.AttachmentPath.length > 0) {
			$scope.viewAttachment.ContentPath = attachment.AttachmentPath;

			const fileType = determineFileType(attachment.AttachmentPath);
			if (fileType === 'pdf') {
				$scope.viewAttachment.FileType = 'pdf';
				document.getElementById('pdfViewer').src = attachment.AttachmentPath;
			} else {
				$scope.viewAttachment.FileType = fileType;
			}

			$('#attachmentPreviewModal').modal('show');
		}  else {
			Swal.fire('No Attachment Found');
		}
	};

	function determineFileType(filePath) {
		const extension = filePath.split('.').pop().toLowerCase();
		if (extension === 'pdf') return 'pdf';
		if (['jpg', 'jpeg', 'png', 'gif', 'bmp'].includes(extension)) return 'image';
		return 'other';
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});