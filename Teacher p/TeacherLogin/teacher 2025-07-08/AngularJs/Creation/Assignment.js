
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('AssignmentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Assignment';

	OnClickDefault();
	var imageEditor = null;
	$scope.InitImgEditor = function (imgPath) {

		var imgInd = imgPath.indexOf('?');
		if (imgInd > 0)
			imgPath = imgPath.substring(0, imgInd);

		imageEditor = new tui.ImageEditor('#tui-image-editor-container', {
			includeUI: {
				menu: [
					'draw', 'text', 'rotate', 'flip', 'crop', 'shape'
				],
				loadImage: {
					path: imgPath,
					name: 'SampleImage',
				},
				theme: whiteTheme, // or whiteTheme
				// initMenu: 'draw',
				menuBarPosition: 'right',
				draw: {
					color: '#00a9ff',
					opacity: 1.0,
					range: {
						value: 8
					}
				},
			},
			cssMaxWidth: 700,
			// cssMaxHeight: 500,
			usageStatistics: false,
			selectionStyle: {}
		});
		imageEditor.startDrawingMode('FREE_DRAWING', {
			width: 5,
			color: 'rgba(255,0,0,0.9)',
			arrowType: {
				tail: 'chevron' // triangle
			}
		});


	}
	$scope.LoadData = function () {

		$scope.Assignment_Files_TMP = null;
		$scope.Assignment_Files_Data = null;
		$scope.Notice_Files_TMP = null;
		$scope.Notice_Files_Data = null;

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();



		//Get class and Section List
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


		//Get AssignmentTypeList
		$scope.AssignmentColl = [];
		$http.get(base_url + "Assignment/Creation/AssignmentTypeList")
			.then(function (data) {
				$scope.AssignmentColl = data.data;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Data not get");
			});


		$scope.currentPages = {
			AddAssignment: 1,
			AssignmentList: 1,

		};

		$scope.searchData = {
			AddAssignment: '',
			AssignmentList: '',

		};

		$scope.perPage = {
			AddAssignment: GlobalServices.getPerPageRow(),
			AssignmentList: GlobalServices.getPerPageRow(),


		};

		$scope.newAddAssignment = {
			AddAssignmentId: null,
			AssignmentTypeId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Title: '',
			Description: '',
			Topic: '',
			File: null,
			DeadlineTime: null,
			DeadlineForReDoTime: null,
			SubmissionsRequired: true,
			MarkScheme:"1",
			Mode: 'Save'
		};

		$scope.newAssignmentList = {
			AssignmentListId: null,

			Mode: 'Save'
		};

		$scope.GetAllAssignmentwithout();
	}

	function OnClickDefault() {
		document.getElementById('add-assignment-form').style.display = "none";
		document.getElementById('detail-form').style.display = "none";
		document.getElementById('check-list').style.display = "none";
		document.getElementById('hw-correction-part').style.display = "none";
		document.getElementById('edit-assignment-form').style.display = "none";
		//document.getElementById('add-file-area').style.display = "none";

		document.getElementById('add-assignment-btn').onclick = function () {
			document.getElementById('add-assignment-section').style.display = "none";
			document.getElementById('add-assignment-form').style.display = "block";
		}

		document.getElementById('back-add-assignment-btn').onclick = function () {
			document.getElementById('add-assignment-form').style.display = "none";
			document.getElementById('add-assignment-section').style.display = "block";
			$scope.GetAllAssignmentwithout();
		}

		$scope.Detailscheck = function () {
			document.getElementById('status-tab').style.display = "none";
			document.getElementById('hw-correction-part').style.display = "block";
		}

		// / Assignment Listadd-hom important

		$scope.Showdetail = function () {
			document.getElementById('Assignment-list-section').style.display = "none";
			document.getElementById('detail-form').style.display = "block";
		}
		document.getElementById('back-assignment-list-btn').onclick = function () {
			document.getElementById('detail-form').style.display = "none";
			document.getElementById('Assignment-list-section').style.display = "block";
		}

		// / Assignment Listadd-hom important


		//document.getElementById('show-check-list').onclick = function () {
		//	document.getElementById('Assignment-list-section').style.display = "none";
		//	document.getElementById('check-list').style.display = "block";
		//}

		document.getElementById('back-to-hwlist').onclick = function () {
			document.getElementById('check-list').style.display = "none";
			document.getElementById('Assignment-list-section').style.display = "block";
		}

		$scope.GetAddAssignmentCheckById = function () {
			document.getElementById('status-tab').style.display = "none";
			document.getElementById('hw-correction-part').style.display = "block";
		}


		document.getElementById('back-to-status-tab').onclick = function () {
			document.getElementById('hw-correction-part').style.display = "none";
			document.getElementById('status-tab').style.display = "block";

			if (imageEditor)
				imageEditor.destroy();
		}

		document.getElementById('save-to-status-tab').onclick = function () {
			$scope.CheckAssignmentd();
		}
		//document.getElementById('add-file-btn').onclick = function () {
		//	document.getElementById('add-file-area').style.display = "block";
		//}


	}


	$scope.ClearAddAssignment = function () {


		$scope.SectionList = [];
		$scope.SubjectColl = [];
		$scope.Assignment_Files_TMP = null;
		$scope.Assignment_Files_Data = null;

		$('input[type=file]').val('');

		$scope.newAddAssignment = {
			AddAssignmentId: null,
			AssignmentTypeId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Lesson: '',
			Description: '',
			Topic: '',
			File: null,
			DeadlineTime: null,
			DeadlineForReDoTime: null,
			SubmissionsRequired: true,
			MarkScheme: "1",
			Mode: 'Save'
		};
	}



	//************************* Add Assignment *********************************
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";

		$scope.SubjectColl = [];
		if ($scope.newAddAssignment.ClassId) {
			var para = {
				classId: $scope.newAddAssignment.ClassId,
				sectionIdColl: ($scope.newAddAssignment.SectionIdColl ? $scope.newAddAssignment.SectionIdColl.toString() : 0),
				sectionId: ($scope.newAddAssignment.SectionIdColl ? $scope.newAddAssignment.SectionIdColl.toString() : 0)
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectColl = res.data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.IsValidAddAssignment = function () {
		if ($scope.newAddAssignment.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAddAssignment = function () {
		if ($scope.IsValidAddAssignment() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddAssignment.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddAssignment();
					}
				});
			} else
				$scope.CallSaveUpdateAddAssignment();

		}
	};
	$scope.CallSaveUpdateAddAssignment = function () {


		$scope.loadingstatus = "running";

		$timeout(function () {

			setTimeout(function () {
				var AssignmentFiles = [];

				if ($scope.Assignment_Files_Data && $scope.Assignment_Files_Data.length > 0) {
					angular.forEach($scope.Assignment_Files_Data, function (fd) {
						AssignmentFiles.push(dataURItoFile(fd));
					});
				}
				else {
					angular.forEach($scope.Assignment_Files_TMP, function (fd) {
						AssignmentFiles.push(fd);
					});
				}






				if ($scope.newAddAssignment.DeadlineDateDet) {
					$scope.newAddAssignment.DeadlineDate = $filter('date')(new Date($scope.newAddAssignment.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
				} else
					$scope.newAddAssignment.DeadlineDate = null;


				if ($scope.newAddAssignment.DeadlineforRedoDet) {
					$scope.newAddAssignment.DeadlineforRedo = $filter('date')(new Date($scope.newAddAssignment.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd');
				} else
					$scope.newAddAssignment.DeadlineforRedo = null;

				if ($scope.newAddAssignment.DeadlineTime_TMP)
					$scope.newAddAssignment.DeadlineTime = $scope.newAddAssignment.DeadlineTime_TMP.toLocaleString();
				else
					$scope.newAddAssignment.DeadlineTime = null;

				if ($scope.newAddAssignment.DeadlineForReDoTime_TMP)
					$scope.newAddAssignment.DeadlineforRedoTime = $scope.newAddAssignment.DeadlineForReDoTime_TMP.toLocaleString();
				else
					$scope.newAddAssignment.DeadlineforRedoTime = null;

				$http({
					method: 'POST',
					url: base_url + "Assignment/Creation/AddAssignment",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						if (data.files && data.files.length > 0) {
							for (var i = 0; i < data.files.length; i++) {
								formData.append("file1" + i, data.files[i]);
							}
						}
						return formData;
					},
					data: { jsonData: $scope.newAddAssignment, files: AssignmentFiles }
				}).then(function (res) {

					$scope.loadingstatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.ClearAddAssignment();
						$scope.GetAllAssignmentwithout();
					}

				}, function (errormessage) {

					$scope.loadingstatus = "stop";

				});

			}, 50);

		});

	}
	$scope.OnChangeDate = function () {
		$scope.V = {};
		if ($scope.newAssignmentList.FromDate_TMP && $scope.newAssignmentList.ToDate_TMP) {


			var res = $scope.newAssignmentList.FromDate_TMP.split("-");
			var resTo = $scope.newAssignmentList.ToDate_TMP.split("-");
			$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.dateTo = NepaliFunctions.BS2AD({ year: resTo[0], month: resTo[1], day: resTo[2] })
			$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.GetAllAddAssignmentList();

		}

	};
	$scope.GetAllAddAssignmentList = function () {
		if ($scope.V) {
			if ($scope.V.dateFrom && $scope.V.dateTo) {
				$scope.loadingstatus = "running";

				$scope.AssignmentList = [];

				$http({
					method: 'POST',
					url: base_url + "Assignment/Creation/GetAssignmentList",
					data: $scope.V,
					dataType: "json"
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					if (res.data) {
						$scope.AssignmentList = res.data;
						$scope.GrandTotal = mx(res.data).sum(p1 => p1.Total);
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		}
	}

	$scope.GetAddAssignmentById = function (refData) {

		$scope.loadingstatus = "running";

		$scope.AssignmentId = refData.AssignmentId;
		$scope.AssignmentDetails = {};
		var para = {
			hwId: refData.AssignmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Assignment/Creation/GetAssignmentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			if (res.data) {

				var query = res.data;
				if (query.length > 0) {

					$timeout(function () {
						$scope.AssignmentDetails.FData = query[0];
						$scope.AssignmentDetails.FData.SectionName = refData.SectionName;
						$scope.AssignmentDetails.FData.TeacherName = refData.TeacherName;
						$scope.AssignmentDetails.FData.SubjectName = refData.SubjectName;
						$scope.AssignmentDetails.FData.Lession = refData.Lession;
						$scope.AssignmentDetails.FData.Topic = refData.Topic;
						$scope.AssignmentDetails.FData.AsignDateTime_BS = refData.AsignDateTime_BS;
						$scope.AssignmentDetails.FData.DeadlineDate_BS = refData.DeadlineDate_BS;
						$scope.AssignmentDetails.FData.NoOfSubmit = refData.NoOfSubmit;
						$scope.AssignmentDetails.FData.TotalStudent = refData.TotalStudent;
						$scope.AssignmentDetails.FData.TotalChecked = refData.TotalChecked;

						$scope.AssignmentDetails.SubmitAssignmentsCOll = [];
						$scope.AssignmentDetails.PendingAssignmentsCOll = [];

						angular.forEach(query, function (q) {
							q.AssignmentId = refData.AssignmentId;
							if (q.SubmitStatus == "Pending") {

								q.IsCheck = false;

								$scope.AssignmentDetails.PendingAssignmentsCOll.push(q);
							}
							else {
								if (q.CheckStatus == 'Checked')
									q.IsCheck = true;
								else
									q.IsCheck = false;

								$scope.AssignmentDetails.SubmitAssignmentsCOll.push(q);
							}

						});
					});

				}





				$scope.newAddAssignment.Mode = 'Save';
				document.getElementById('Assignment-list-section').style.display = "none";
				document.getElementById('detail-form').style.display = "block";

				//document.getElementById('add-Assignment-section').style.display = "none";
				//document.getElementById('add-Assignment-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	//**************************************************CHECK Assignment**********************************************************************************************

	$scope.NextPreviewImage = function (val) {
		if ($scope.AssignmentCorrectDetails) {
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);
			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].File = flll;

			var nInd = $scope.AssignmentCorrectDetails.SelectedIndex + val;

			if (nInd > -1 && nInd < $scope.AssignmentCorrectDetails.FilesColl.length) {
				$scope.AssignmentCorrectDetails.SelectedIndex = nInd;
				var sfl = $scope.AssignmentCorrectDetails.FilesColl[nInd];
				if (sfl.File)
					imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
				else
					imageEditor.loadImageFromURL(sfl.Path, sfl.FileName);
			}

		}
	};

	$scope.ChangeImage = function (sfl) {

		if ($scope.AssignmentCorrectDetails) {
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);
			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].File = flll;
			$scope.AssignmentCorrectDetails.SelectedIndex = sfl.Index;


			if (sfl.File)
				imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
			else
				imageEditor.loadImageFromURL(sfl.Path, sfl.FileName);
		}
	};
	$scope.GetAddAssignmentChecksById = function (hdc) {

		$scope.loadingstatus = "running";
		$scope.AssignmentCorrectDetails = {};

		$scope.AssignmentCorrectDetails = hdc;
		$scope.AssignmentCorrectDetails.CheckRemarks1 = hdc.CheckedRemarks;

		if (hdc.CheckStatus == 'Checked')
			$scope.AssignmentCorrectDetails.CheckStatus1 = 1;
		else
			$scope.AssignmentCorrectDetails.CheckStatus1 = 1;

		$scope.AssignmentCorrectDetails.FilesColl = [];

		$timeout(function () {
			var fileInd = 0;
			angular.forEach($scope.AssignmentCorrectDetails.StudentAttachments.split("##"), function (fl) {
				if (fl && fl.trim().length > 0) {
					var fName = '';
					var ind = fl.lastIndexOf('/');
					var ind1 = fl.lastIndexOf('\\');
					if (ind > ind1) {
						fName = fl.substring(ind + 1);
					} else {
						fName = fl.substring(ind1 + 1);
					}

					var fDet = {
						Path: APIURL.trim() + fl.trim() + '?' + (new Date()).getTime(),
						File: null,
						Index: fileInd,
						FileName: fName
					};
					$scope.AssignmentCorrectDetails.FilesColl.push(fDet);
					fileInd = fileInd + 1;
				}

			});
		});

		$timeout(function () {
			if ($scope.AssignmentCorrectDetails.FilesColl && $scope.AssignmentCorrectDetails.FilesColl.length > 0) {
				$scope.AssignmentCorrectDetails.SelectedIndex = 0;
				$scope.InitImgEditor($scope.AssignmentCorrectDetails.FilesColl[0].Path);

			}

			$scope.newAddAssignment.Mode = 'Save';
			document.getElementById('status-tab').style.display = "none";
			document.getElementById('hw-correction-part').style.display = "block";
		});


	};

	$scope.GetAddAssignmentDoneById = function (refData) {

		$scope.AssignmentDetails.SubmitAssignmentsCOll = [];
		$scope.AssignmentDetails.PendingAssignmentsCOll = [];
		$scope.loadingstatus = "running";

		$scope.AssignmentDetails = {};
		var para = {
			hwId: refData.AssignmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Assignment/Creation/GetAssignmentCorrectById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			if (res.data) {

				var query = res.data;
				if (query.length > 0) {
					$scope.AssignmentDetails.FData = query[0];

					angular.forEach(query, function (q) {

						if (q.SubmitStatus == "Pending")
							$scope.AssignmentDetails.PendingAssignmentsCOll.push(q);
						else
							$scope.AssignmentDetails.SubmitAssignmentsCOll.push(q);
					});
				}


				$scope.newAddAssignment.Mode = 'Save';
				document.getElementById('Assignment-list-section').style.display = "none";
				document.getElementById('detail-form').style.display = "block";

				//document.getElementById('add-Assignment-section').style.display = "none";
				//document.getElementById('add-Assignment-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.dataURLtoBlob = function (dataurl) {
		var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
			bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
		while (n--) {
			u8arr[n] = bstr.charCodeAt(n);
		}
		return new Blob([u8arr], { type: mime });
	};
	$scope.RFiles = [];
	$scope.RImage = function (result) {
		var img = new Image();

		img.src = result;

		setTimeout(function () {

			var canvas = document.createElement("canvas");

			var MAX_WIDTH = 1024;
			var MAX_HEIGHT = 768;
			var width = 800; //img.width;
			var height = 800;// img.height;

			if (width > height) {
				if (width > MAX_WIDTH) {
					height *= MAX_WIDTH / width;
					width = MAX_WIDTH;
				}
			} else {
				if (height > MAX_HEIGHT) {
					width *= MAX_HEIGHT / height;
					height = MAX_HEIGHT;
				}
			}
			canvas.width = width;
			canvas.height = height;
			var ctx = canvas.getContext("2d");
			ctx.drawImage(img, 0, 0, width, height);

			console.log("Hereeee ", width, height);

			var dataurl = canvas.toDataURL("image/jpeg");
			//document.getElementById('output').src = dataurl;

			$scope.RFiles.push(dataurl);

		}, 100);
	}

	var BASE64_MARKER = ';base64,';

	// Does the given URL (string) look like a base64-encoded URL?
	function isDataURI(url) {
		return url.split(BASE64_MARKER).length === 2;
	};
	function dataURItoFile(dataURI) {
		if (!isDataURI(dataURI)) { return false; }

		// Format of a base64-encoded URL:
		// data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
		var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
		var filename = 'rc-' + (new Date()).getTime() + '.' + mime.split('/')[1];
		//var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
		var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
		var writer = new Uint8Array(new ArrayBuffer(bytes.length));

		for (var i = 0; i < bytes.length; i++) {
			writer[i] = bytes.charCodeAt(i);
		}

		return new File([writer.buffer], filename, { type: mime });
	}

	$scope.CheckAssignmentd = function () {

		if ($scope.AssignmentCorrectDetails && $scope.AssignmentCorrectDetails.CheckStatus1 && $scope.AssignmentCorrectDetails.CheckStatus1 > 0) {
			var checkHome = {
				StudentId: $scope.AssignmentCorrectDetails.StudentId,
				AssignmentId: $scope.AssignmentCorrectDetails.AssignmentId,
				Status: $scope.AssignmentCorrectDetails.CheckStatus1,
				Notes: $scope.AssignmentCorrectDetails.CheckRemarks1,
				FilesColl: []
			};

			$scope.RFiles = [];
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);

			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.AssignmentCorrectDetails.FilesColl[$scope.AssignmentCorrectDetails.SelectedIndex].File = flll;

			angular.forEach($scope.AssignmentCorrectDetails.FilesColl, function (fl) {
				if (fl.File) {
					$scope.RImage(fl.DATAURL);
					checkHome.FilesColl.push(fl.FileName);
				}
			});

			$scope.loadingstatus = "running";

			setTimeout(function () {
				$timeout(function () {
					var attFiles = [];
					angular.forEach($scope.RFiles, function (r) {
						var nFile = dataURItoFile(r);
						attFiles.push(nFile);
					});

					$http({
						method: 'POST',
						url: base_url + "Assignment/Creation/CheckAssignment",
						headers: { 'Content-Type': undefined },

						transformRequest: function (data) {

							var formData = new FormData();
							formData.append("jsonData", angular.toJson(data.jsonData));

							for (var f = 0; f < data.files.length; f++)
								formData.append("file" + f.toString(), data.files[f]);

							return formData;
						},
						data: { jsonData: checkHome, files: attFiles }
					}).then(function (res) {

						$scope.loadingstatus = "stop";

						if (res.data.ResponseMSG) {

							Swal.fire(res.data.ResponseMSG);

							if (res.data.IsSuccess == true) {

								document.getElementById('hw-correction-part').style.display = "none";
								document.getElementById('status-tab').style.display = "block";

								var query = mx($scope.AssignmentDetails.SubmitAssignmentsCOll).firstOrDefault(p1 => p1.StudentId == checkHome.StudentId);
								if (query) {
									query.IsCheck = true;
									query.CheckStatus = ($scope.AssignmentCorrectDetails.CheckStatus1 == 1 ? 'DONE' : 'RE-DO');
								}


								if (imageEditor)
									imageEditor.destroy();

								$scope.AssignmentCorrectDetails = {};

							}
						} else {
							Swal.fire(res.data);
						}


					}, function (errormessage) {

						$scope.loadingstatus = "stop";

					});
				});

			}, 600);


		} else
			Swal.fire('Please ! Select Status');

	}

	$scope.GetAllAssignmentwithout = function () {

		$scope.loadingstatus = "running";
		$scope.AssignmentDataColl = [];
		$http({
			method: 'POST',
			url: base_url + "Assignment/Creation/GetAssignmentColl",
			dataType: "json"
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.AssignmentDataColl = [];
				angular.forEach(res.data, function (st) {
					angular.forEach(st.DataColl, function (Det) {
						$scope.AssignmentDataColl.push(Det);
					});
				});

				$scope.AssignmentDataColl = $filter('orderBy')($scope.AssignmentDataColl, 'AsignDateTime_AD', true);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.DelAssignmentById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";


				var para = {
					AssignmentId: refData.AssignmentId
				};
				$http({
					method: 'POST',
					url: base_url + "Assignment/Creation/DelAssignmentById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess) {
						$timeout(function () {
							$scope.GetAllAssignmentwithout();
						});

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
	//*******************************************************Nitification Work***********************************************

	$scope.NotificationToggle = function (item) {
		$scope.newNotice = {};
		$scope.StudentColl = []

		angular.forEach(item, function (DT) {

			$scope.StudentColl.push(DT.StudentId);
		});
		$scope.newNotice.studentIdColl = ($scope.StudentColl && $scope.StudentColl.length > 0 ? $scope.StudentColl.toString() : '');
		$('#modal-notice').modal('show');
	};



	$scope.ClearNotice = function () {
		$scope.newNotice = {
			title: '',
			description: ''
		};
		$('input[type=file]').val('');
	};

	$scope.SendNoticeToStudent = function () {
		$scope.loadingstatus = "running";

		$timeout(function () {
			setTimeout(function () {
				var noticeFiles = [];

				angular.forEach($scope.Notice_Files_Data, function (fd) {
					noticeFiles.push(dataURItoFile(fd));
				});

				var StudentArray = []
				angular.forEach($scope.StudentRecordList, function (ec) {
					if (ec.Full == true) {
						StudentArray.push(ec.StudentId)
					}
				});
				//$scope.newNotice.studentIdColl = (StudentArray && StudentArray.length > 0 ? StudentArray.toString() : '')

				$http({
					method: 'POST',
					url: base_url + "StudentRecord/Creation/SendNoticeToStudent",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						if (data.files && data.files.length > 0)
							formData.append("file1", data.files[0]);

						return formData;
					},
					data: { jsonData: $scope.newNotice, files: noticeFiles }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();
					if (res.data.IsSuccess == true) {
						Swal.fire('Sent Successfully');
						$('#modal-notice').modal('hide');
						$scope.ClearNotice();
					}

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}, 100);
		})
	}

	$scope.EditAssignmentDet = {};
	//Edit button show hide starts
	$scope.editbtn = function (homDet) {
		$scope.EditAssignmentDet = homDet;
		$scope.EditAssignmentDet.AttFilesColl = [];
		angular.forEach(homDet.AttachmentColl, function (doc) {
			var fpath = (WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/');
			$scope.EditAssignmentDet.AttFilesColl.push(fpath);
		});

		$scope.EditAssignmentDet.DeadlineDate_TMP = new Date(homDet.DeadlineDate_AD);
		$scope.EditAssignmentDet.DeadlineTime_TMP = new Date(homDet.DeadlineTime);

		if (homDet.DeadlineforRedo && homDet.DeadlineforRedo != null)
			$scope.EditAssignmentDet.DeadlineforRedo_TMP = new Date(homDet.DeadlineforRedo);

		if (homDet.DeadlineforRedoTime && homDet.DeadlineforRedoTime != null)
			$scope.EditAssignmentDet.DeadlineForReDoTime_TMP = new Date(homDet.DeadlineforRedoTime);

		if (homDet.MarkScheme == 1)
			$scope.EditAssignmentDet.MarkScheme = "1";
		else
			$scope.EditAssignmentDet.MarkScheme = "2";

		document.getElementById('edit-assignment-form').style.display = "block";
		document.getElementById('add-assignment-section').style.display = "none";

	}

	$scope.backTableBtn = function () {
		document.getElementById('add-assignment-section').style.display = "block";
		document.getElementById('edit-assignment-form').style.display = "none";

	}
	$scope.CallUpdateDeadlineAssignment = function () {


		$scope.loadingstatus = "running";

		$timeout(function () {
			if ($scope.EditAssignmentDet.DeadlineDateDet) {
				$scope.EditAssignmentDet.DeadlineDate = $filter('date')(new Date($scope.EditAssignmentDet.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
			} else
				$scope.EditAssignmentDet.DeadlineDate = null;


			if ($scope.EditAssignmentDet.DeadlineforRedoDet) {
				$scope.EditAssignmentDet.DeadlineforRedo = $filter('date')(new Date($scope.EditAssignmentDet.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd');
			} else
				$scope.EditAssignmentDet.DeadlineforRedo = null;

			if ($scope.EditAssignmentDet.DeadlineTime_TMP)
				$scope.EditAssignmentDet.DeadlineTime = $scope.EditAssignmentDet.DeadlineTime_TMP.toLocaleString();
			else
				$scope.EditAssignmentDet.DeadlineTime = null;

			if ($scope.EditAssignmentDet.DeadlineForReDoTime_TMP)
				$scope.EditAssignmentDet.DeadlineforRedoTime = $scope.EditAssignmentDet.DeadlineForReDoTime_TMP.toLocaleString();
			else
				$scope.EditAssignmentDet.DeadlineforRedoTime = null;

			$http({
				method: 'POST',
				url: base_url + "Assignment/Creation/UpdateDeadlineAssignment",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.EditAssignmentDet }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				Swal.fire(res.data.ResponseMSG);


			}, function (errormessage) {

				$scope.loadingstatus = "stop";

			});

		});

	}

});