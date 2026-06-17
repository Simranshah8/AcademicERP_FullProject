app.controller('ReadingMaterialsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Reading Materials';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Content: 1,
			Video: 1,
		};

		$scope.searchData = {
			Content: '',
			Video: '',
		};
		$scope.perPage = {
			Content: GlobalServices.getPerPageRow(),
			Video: GlobalServices.getPerPageRow(),
		};

		$scope.beData = {
			ReadMatContId: null,
			BatchId: null,
			ClassId: null,
			SemesterId: null,
			ClassYearId: null,
			SubjectId: null,
			Title: "",
			ContentsColl: [],
			VideoColl: [],
			Mode: "Save"
		}

		$scope.newFilter = {
			BatchId: null,
			ClassId: null,
			SemesterId: null,
			ClassYearId: null,
			SubjectId: null
		}

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;


			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			}

			if ($scope.AcademicConfig.ActiveSemester == false) {


				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			} else {
				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			} else {
				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

			if ($scope.AcademicConfig.ActiveClassYear == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			}
			else {
				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends


		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.beData.VideoColl.push({});
		$scope.isVideoList = true;
		$scope.isContentList = true;
		$scope.ContentList = [];
		$scope.VideoList = [];
	}

	$scope.ClearContent = function () {
		$scope.beData.ReadMatContId = null;
		$scope.beData.Title = '';
		$scope.beData.ContentsColl = [];
		$scope.beData.Mode = 'Save';
	}
	$scope.ClearVideo = function () {
		$scope.beData.ReadMatVideoId = null;
		$scope.beData.VideoColl = [];
		$scope.beData.VideoColl.push({});
		$scope.beData.Mode = 'Save';
	}

	$scope.ShowVideoList = function () {
		$scope.GetClassSubWiseData();
		$scope.isVideoList = true;
	};

	$scope.ShowAddVideo = function () {
		$scope.isVideoList = false;
	};
	$scope.ShowContentList = function () {
		$scope.isContentList = true;
		$scope.GetClassSubWiseData();
	};

	$scope.ShowAddContent = function () {
		$scope.isContentList = false;
	};

	$scope.GetClassWiseSubjectList = function () {
		$scope.SubjectList = [];
		if ($scope.newFilter.SelectedClass.ClassType == 2 && $scope.AcademicConfig.ActiveClassYear) {
			$scope.newFilter.SemesterId = null;
		}

		if ($scope.newFilter.SelectedClass.ClassType == 3 && $scope.AcademicConfig.ActiveSemester) {
			$scope.newFilter.ClassYearId = null;
		}

		var para = {
			ClassId: $scope.newFilter.SelectedClass.ClassId,
			BatchId: $scope.newFilter.BatchId || null,
			ClassYearId: $scope.newFilter.ClassYearId || null,
			SemesterId: $scope.newFilter.SemesterId || null,
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.SubjectList = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.CallSaveUpdateContent = function () {
		if (!$scope.newFilter.SelectedClass || $scope.newFilter.SubjectList) {
			Swal.fire("Please ! Select Class");
			return;
		}
		if (!$scope.newFilter.SubjectId) {
			Swal.fire("Please ! Select Subject");
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		var filesColl = $scope.beData.ContentsColl;
		var dataToSave = {
			ReadMatContId: $scope.beData.ReadMatContId,
			BatchId: $scope.newFilter.BatchId,
			ClassId: $scope.newFilter.SelectedClass.ClassId,
			SemesterId: $scope.newFilter.SemesterId,
			ClassYearId: $scope.newFilter.ClassYearId,
			SubjectId: $scope.newFilter.SubjectId,
			Title: $scope.beData.Title,
			ContentAttach: $scope.beData.ContentsColl
		}

		$http({
			method: "POST",
			url: base_url + "ReadingMaterials/Creation/SaveReadMatCont",
			headers: { "content-Type": undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				var find = 0;
				angular.forEach(data.files, function (dc) {
					if (dc.File) {
						formData.append("file" + find, dc.File);
					}
					find++;
				});
				return formData;
			},
			data: { jsonData: dataToSave, files: filesColl },
		}).then(
			function (res) {
				$scope.loadingstatus = "stop";
				hidePleaseWait();
				Swal.fire(res.data.ResponseMSG);
				if (res.data.IsSuccess == true) {
					$scope.ClearContent();
				}
			},
			function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			},
		);
	};

	$scope.DeleteContent = function (item) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ReadMatContId: item.ReadMatContId
				}

				$http({
					method: 'POST',
					url: base_url + "ReadingMaterials/Creation/DelReadMatCont",
					dataType: 'json',
					data: JSON.stringify(para),
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					if (res.data.IsSuccess) {
						$scope.GetClassSubWiseData();
					}
					else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
				});
			}
		})
	}

	$scope.GetContentById = function (refData, num) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ReadMatContId: refData.ReadMatContId
		};

		$http({
			method: 'POST',
			url: base_url + "ReadingMaterials/Creation/GetReadMatContById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.beData = res.data.Data;
				$scope.beData.ReadMatContId = res.data.Data.ReadMatContId;
				$scope.beData.ContentsColl = res.data.Data.ContentAttach;

				/* Process attachments*/
				var attachments = $scope.beData.ContentsColl;
				$scope.beData.ContentsColl = [];
				if (attachments && attachments.length > 0) {
					angular.forEach(attachments, function (doc) {
						if (doc.DocPath) {
							var fileExt = getFileExtension(doc.Name);

							// Determine file type
							var fileType = getFileCategory(fileExt);

							$scope.beData.ContentsColl.push({
								Name: doc.Name,
								previewUrl: doc.DocPath,
								DocPath: doc.DocPath,
								fileType: fileType,
								Extension: fileExt,
								name: doc.Name
							});
						}
					});
				}

				if (num == 0) {
					$scope.ShowContentAttach();
				}
				if (num == 1) {
					$scope.beData.Mode = 'Modify';
					$scope.ShowAddContent();
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.CallSaveUpdateVideo = function () {
		if (!$scope.newFilter.SelectedClass || $scope.newFilter.SubjectList) {
			Swal.fire("Please ! Select Class");
			return;
		}
		if (!$scope.newFilter.SubjectId) {
			Swal.fire("Please ! Select Subject");
			return;
		}
		var dataToSave = [];
		angular.forEach($scope.beData.VideoColl, function (item) {
			dataToSave.push({
				BatchId: $scope.newFilter.BatchId,
				ClassId: $scope.newFilter.SelectedClass.ClassId,
				SemesterId: $scope.newFilter.SemesterId,
				ClassYearId: $scope.newFilter.ClassYearId,
				SubjectId: $scope.newFilter.SubjectId,
				ReadMatVideoId: item.ReadMatVideoId,
				VideoTitle: item.VideoTitle,
				VideoLink: item.VideoLink,
				Remarks: item.Remarks
			})
		})

		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "ReadingMaterials/Creation/SaveReadMatVideo",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess) {
				$scope.ClearVideo();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.DeleteVideo = function (item) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ReadMatVideoId: item.ReadMatVideoId
				}

				$http({
					method: 'POST',
					url: base_url + "ReadingMaterials/Creation/DelReadMatVideo",
					dataType: 'json',
					data: JSON.stringify(para),
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					if (res.data.IsSuccess) {
						$scope.GetClassSubWiseData();
					}
					else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
				});
			}
		})
	}

	$scope.GetVideoById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ReadMatVideoId: refData.ReadMatVideoId
		};

		$http({
			method: 'POST',
			url: base_url + "ReadingMaterials/Creation/GetReadMatVideoById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.beData.VideoColl = [res.data.Data];
				$scope.beData.Mode = 'Modify';
				$scope.ShowAddVideo();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.previewFile = function (file) {
		if (file.type === 'pdf') {
			window.open(file.DocPath, '_blank');
		} else if (file.type === 'image') {
			window.open(file.DocPath, '_blank');
		} else {
			window.open(file.DocPath, '_blank');
		}
	};

	$scope.ShowContentAttach = function () {
		$('#ContentAttach').modal('show');
	}

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			FileType: null
		};

		if (item.fileType == 'pdf') {
			$scope.viewImg.ContentPath = item.previewUrl;
			$scope.viewImg.FileType = 'pdf';
			document.getElementById('pdfViewer').src = item.previewUrl;
			$('#PersonalImg').modal('show');
		} else if (item.fileType == 'image') {
			$scope.viewImg.ContentPath = item.previewUrl;
			$scope.viewImg.FileType = 'image';
			$('#PersonalImg').modal('show');
		} else if (item.File || item.fileType) {
			var link = document.createElement('a');
			link.href = item.previewUrl || item.FilePath || item.DocPath;
			link.download = item.name;
			document.body.appendChild(link);
			link.click();
			document.body.removeChild(link);
		} else {
			Swal.fire('No Image Found');
		}
	};

	$scope.onFileSelect = function () {
		var files = $scope.beData.TMPContentsColl;

		if (!files || !files.length) return;

		if (!$scope.beData.ContentsColl) {
			$scope.beData.ContentsColl = [];
		}
		var maxSize = 5 * 1024 * 1024;

		angular.forEach(files, function (file) {
			if (file.size > maxSize) {
				Swal.fire({
					icon: 'warning',
					title: 'File Too Large',
					text: file.name + ' exceeds the 5MB size limit.',
					confirmButtonText: 'OK'
				});
				return;
			}

			var duplicate = $scope.beData.ContentsColl.some(function (f) {
				return f.name === file.name && f.size === file.size;
			});

			if (duplicate) return;

			$scope.beData.ContentsColl.push({
				File: file,
				name: file.name,
				fileNameWithoutExt: getFileNameWithoutExtension(file.name),
				extension: getFileExtension(file.name),
				extensionWithDot: '.' + getFileExtension(file.name),
				type: file.type,
				size: file.size,
				previewUrl: URL.createObjectURL(file),
				fileType: getFileCategory(getFileExtension(file.name)),
				isExisting: false
			});
		});

		$scope.beData.TMPContentsColl = [];
	};

	function getFileExtension(filename) {
		return filename.split('.').pop().toLowerCase();
	}

	function getFileNameWithoutExtension(filename) {
		return filename.substring(0, filename.lastIndexOf('.')) || filename;
	}

	function getFileCategory(extension) {
		var imageExt = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'];
		var pdfExt = ['pdf'];
		var wordExt = ['doc', 'docx'];
		var excelExt = ['xls', 'xlsx'];
		var pptExt = ['ppt', 'pptx'];
		var textExt = ['txt'];

		extension = extension.toLowerCase();

		if (imageExt.includes(extension)) return 'image';
		if (pdfExt.includes(extension)) return 'pdf';
		if (wordExt.includes(extension)) return 'word';
		if (excelExt.includes(extension)) return 'excel';
		if (pptExt.includes(extension)) return 'powerpoint';
		if (textExt.includes(extension)) return 'text';
		return 'other';
	}

	$scope.delContentFile = function (ind) {
		if ($scope.beData.ContentsColl) {
			if ($scope.beData.ContentsColl.length > 0) {
				$scope.beData.ContentsColl.splice(ind, 1);
			}
		}
	};

	$scope.TriggerContentPicker = function () {
		document.getElementById("contentPicker").click();
	};


	$scope.AddRow = function (index) {
		if ($scope.beData.VideoColl) {
			if ($scope.beData.VideoColl.length > index + 1) {
				$scope.beData.VideoColl.splice(index + 1, 0, {});
			}
			else {
				$scope.beData.VideoColl.push({});
			}

		}
	}
	$scope.DeleteRow = function (index) {
		if ($scope.beData.VideoColl) {
			if ($scope.beData.VideoColl.length > 1) {
				$scope.beData.VideoColl.splice(index, 1);
			}
			else {
				$scope.beData.VideoColl[0] = {}
			}
		}
	}


	$scope.GetClassSubWiseData = function () {
		if ($scope.newFilter.SelectedClass || $scope.newFilter.SubjectId) {
			if ($scope.newFilter.SelectedClass.ClassId == null || $scope.newFilter.SubjectId == null) {
				$scope.VideoList = [];
				$scope.ContentList = [];
				return;
			}
			if ($scope.newFilter.SelectedClass.ClassType == 2 && $scope.AcademicConfig.ActiveClassYear) {
				$scope.newFilter.SemesterId = null;
			}

			else if ($scope.newFilter.SelectedClass.ClassType == 3 && $scope.AcademicConfig.ActiveSemester) {
				$scope.newFilter.ClassYearId = null;
			}
			else {
				$scope.newFilter.SemesterId = null;
				$scope.newFilter.ClassYearId = null;
			}
		}
		else {
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BatchId: $scope.newFilter.BatchId || 0,
			ClassId: $scope.newFilter.SelectedClass.ClassId,
			ClassYearId: $scope.newFilter.ClassYearId || 0,
			SemesterId: $scope.newFilter.SemesterId || 0,
			SubjectId: $scope.newFilter.SubjectId
		};

		$http({
			method: 'POST',
			url: base_url + "ReadingMaterials/Creation/GetClassSubWiseData",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VideoList = res.data.Data.ReadMadVideoColl;
				$scope.ContentList = res.data.Data.ReadMatContentColl;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});