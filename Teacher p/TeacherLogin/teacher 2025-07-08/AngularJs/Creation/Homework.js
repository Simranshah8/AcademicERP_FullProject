
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('HomeworkController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Homework';

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

		$scope.HomeWork_Files_TMP = null;
		$scope.HomeWork_Files_Data = null;
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


		//Get HomeWorkTypeList
		$scope.HomeworkColl = [];
		$http.get(base_url + "Homework/Creation/HomeWorkTypeList")
			.then(function (data) {
				$scope.HomeworkColl = data.data;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Data not get");
			});


		$scope.currentPages = {
			AddHomework: 1,
			HomeworkList: 1,

		};

		$scope.searchData = {
			AddHomework: '',
			HomeworkList: '',

		};

		$scope.perPage = {
			AddHomework: GlobalServices.getPerPageRow(),
			HomeworkList: GlobalServices.getPerPageRow(),


		};

		$scope.newAddHomework = {
			AddHomeworkId: null,
			HomeworkTypeId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Lesson: '',
			Description: '',
			Topic: '',
			File: null,
			DeadlineTime: null,
			DeadlineForReDoTime: null,
			SubmissionsRequired:true,
			Mode: 'Save'
		};

		$scope.newHomeworkList = {
			HomeworkListId: null,

			Mode: 'Save'
		};

		$scope.GetAllHomeworkwithout();	
	}

	function OnClickDefault() {
		document.getElementById('add-homework-form').style.display = "none";
		document.getElementById('detail-form').style.display = "none";
		document.getElementById('check-list').style.display = "none";
		document.getElementById('hw-correction-part').style.display = "none";
		document.getElementById('edit-homework-form').style.display = "none";
		//document.getElementById('add-file-area').style.display = "none";

		document.getElementById('add-homework-btn').onclick = function () {
			document.getElementById('add-homework-section').style.display = "none";
			document.getElementById('add-homework-form').style.display = "block";
		}

		document.getElementById('back-add-homework-btn').onclick = function () {
			document.getElementById('add-homework-form').style.display = "none";
			document.getElementById('add-homework-section').style.display = "block";
			$scope.GetAllHomeworkwithout();
		}

		$scope.Detailscheck = function () {
			document.getElementById('status-tab').style.display = "none";
			document.getElementById('hw-correction-part').style.display = "block";
		}

		// / Homework Listadd-hom important

		$scope.Showdetail = function () {
			document.getElementById('homework-list-section').style.display = "none";
			document.getElementById('detail-form').style.display = "block";
		}
		document.getElementById('back-homework-list-btn').onclick = function () {
			document.getElementById('detail-form').style.display = "none";
			document.getElementById('homework-list-section').style.display = "block";
		}

		// / Homework Listadd-hom important

		
		//document.getElementById('show-check-list').onclick = function () {
		//	document.getElementById('homework-list-section').style.display = "none";
		//	document.getElementById('check-list').style.display = "block";
		//}

		document.getElementById('back-to-hwlist').onclick = function () {
			document.getElementById('check-list').style.display = "none";
			document.getElementById('homework-list-section').style.display = "block";
		}

		$scope.GetAddHomeworkCheckById = function () {
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
			$scope.CheckHomeWorkd();
		}
		//document.getElementById('add-file-btn').onclick = function () {
		//	document.getElementById('add-file-area').style.display = "block";
		//}


	}


	$scope.ClearAddHomework = function () {

		
		$scope.SectionList = [];
		$scope.SubjectColl = [];
		$scope.HomeWork_Files_TMP = null;
		$scope.HomeWork_Files_Data = null;

		$('input[type=file]').val('');

		$scope.newAddHomework = {
			AddHomeworkId: null,
			HomeworkTypeId: null,
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
			Mode: 'Save'
		};
	}
	
	
	
	//************************* Add Homework *********************************
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		
		$scope.SubjectColl = [];
		if ($scope.newAddHomework.ClassId) {
			var para = {
				classId: $scope.newAddHomework.ClassId,
				sectionIdColl: ($scope.newAddHomework.SectionIdColl ? $scope.newAddHomework.SectionIdColl.toString() : 0),
				sectionId: ($scope.newAddHomework.SectionIdColl ? $scope.newAddHomework.SectionIdColl.toString() : 0)
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				
				$scope.loadingstatus = "stop";
				if (res.data)
				{
					$scope.SubjectColl = res.data;
					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.IsValidAddHomework = function () {
		if ($scope.newAddHomework.Lesson.isEmpty()) {
			Swal.fire('Please ! Enter Lesson');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAddHomework = function () {
		if ($scope.IsValidAddHomework() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddHomework.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddHomework();
					}
				});
			} else
				$scope.CallSaveUpdateAddHomework();

		}
	};
	$scope.CallSaveUpdateAddHomework = function () {


		$scope.loadingstatus = "running";

		$timeout(function () {

			setTimeout(function () {
				var homeworkFiles = [];

				if ($scope.HomeWork_Files_Data && $scope.HomeWork_Files_Data.length > 0) {
					angular.forEach($scope.HomeWork_Files_Data, function (fd) {
						homeworkFiles.push(dataURItoFile(fd));
					});
				}
				else
				{
					angular.forEach($scope.HomeWork_Files_TMP, function (fd) {
						homeworkFiles.push(fd);
					});
                } 

				if ($scope.newAddHomework.DeadlineDateDet) {
					$scope.newAddHomework.DeadlineDate = $filter('date')(new Date($scope.newAddHomework.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
				} else
					$scope.newAddHomework.DeadlineDate = null;


				if ($scope.newAddHomework.DeadlineforRedoDet) {
					$scope.newAddHomework.DeadlineforRedo = $filter('date')(new Date($scope.newAddHomework.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd');
				} else
					$scope.newAddHomework.DeadlineforRedo = null;

				if ($scope.newAddHomework.DeadlineTime_TMP)
					$scope.newAddHomework.DeadlineTime = $scope.newAddHomework.DeadlineTime_TMP.toLocaleString();
				else
					$scope.newAddHomework.DeadlineTime = null;

				if ($scope.newAddHomework.DeadlineForReDoTime_TMP)
					$scope.newAddHomework.DeadlineforRedoTime = $scope.newAddHomework.DeadlineForReDoTime_TMP.toLocaleString();
				else
					$scope.newAddHomework.DeadlineforRedoTime = null;

				$http({
					method: 'POST',
					url: base_url + "Homework/Creation/AddHomeWork",
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
					data: { jsonData: $scope.newAddHomework, files: homeworkFiles }
				}).then(function (res) {

					$scope.loadingstatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.ClearAddHomework();
						$scope.GetAllHomeworkwithout();
					}

				}, function (errormessage) {

					$scope.loadingstatus = "stop";

				});

			}, 50);

		});
		
	}
	$scope.OnChangeDate = function () {
		$scope.V = {};
		if ($scope.newHomeworkList.FromDate_TMP && $scope.newHomeworkList.ToDate_TMP) {


			
			//var res = $scope.newHomeworkList.FromDate_TMP.split("-");
			//var resTo = $scope.newHomeworkList.ToDate_TMP.split("-");
			//$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			//$scope.dateTo = NepaliFunctions.BS2AD({ year: resTo[0], month: resTo[1], day: resTo[2] })
			//$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			//$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.dateFrom = $filter('date')(new Date(($scope.newHomeworkList.FromDateDet ? $scope.newHomeworkList.FromDateDet.dateAD : $scope.newHomeworkList.FromDate_TMP)), 'yyyy-MM-dd');
			$scope.dateTo = $filter('date')(new Date(($scope.newHomeworkList.ToDateDet ? $scope.newHomeworkList.ToDateDet.dateAD : $scope.newHomeworkList.ToDate_TMP)), 'yyyy-MM-dd');

			$scope.V.dateFrom = $scope.dateFrom;
			$scope.V.dateTo = $scope.dateTo;
			$scope.GetAllAddHomeworkList();

		}

	};
	$scope.GetAllAddHomeworkList = function () {
		if ($scope.V)
		{
			if ($scope.V.dateFrom && $scope.V.dateTo) {
				$scope.loadingstatus = "running";
				
				$scope.HomeworkList = [];

				$http({
					method: 'POST',
					url: base_url + "Homework/Creation/GetHomeworkList",
					data: $scope.V,
					dataType: "json"
				}).then(function (res) {
					
					$scope.loadingstatus = "stop";
					if (res.data)
					{
						$scope.HomeworkList = res.data;			
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

	$scope.GetAddHomeworkById = function (refData) {

		$scope.loadingstatus = "running";
		
		$scope.HomeWorkId = refData.HomeWorkId;
		$scope.HomeWorkDetails = {};
		var para = {
			hwId: refData.HomeWorkId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Creation/GetHomeWorkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			
			$scope.loadingstatus = "stop";
			if (res.data) {

				var query = res.data;
				if (query.length > 0) {

					$timeout(function () {
						$scope.HomeWorkDetails.FData = query[0];
						$scope.HomeWorkDetails.FData.SectionName = refData.SectionName;
						$scope.HomeWorkDetails.FData.TeacherName = refData.TeacherName;
						$scope.HomeWorkDetails.FData.SubjectName = refData.SubjectName;
						$scope.HomeWorkDetails.FData.Lession = refData.Lession;
						$scope.HomeWorkDetails.FData.Topic = refData.Topic;
						$scope.HomeWorkDetails.FData.AsignDateTime_BS = refData.AsignDateTime_BS;
						$scope.HomeWorkDetails.FData.DeadlineDate_BS = refData.DeadlineDate_BS;
						$scope.HomeWorkDetails.FData.NoOfSubmit = refData.NoOfSubmit;
						$scope.HomeWorkDetails.FData.TotalStudent = refData.TotalStudent;
						$scope.HomeWorkDetails.FData.TotalChecked = refData.TotalChecked;						

						$scope.HomeWorkDetails.SubmitHomeWorksCOll = [];
						$scope.HomeWorkDetails.PendingHomeWorksCOll = [];

						angular.forEach(query, function (q) {
							q.HomeWorkId = refData.HomeWorkId;
							if (q.SubmitStatus == "Pending") {

   							    q.IsCheck = false;

								$scope.HomeWorkDetails.PendingHomeWorksCOll.push(q);
							}
							else {
								if (q.CheckStatus == 'Checked')
									q.IsCheck = true;
								else
									q.IsCheck = false;

								$scope.HomeWorkDetails.SubmitHomeWorksCOll.push(q);
                            }
								
						});
					});
					
				}





				$scope.newAddHomework.Mode = 'Save';
				document.getElementById('homework-list-section').style.display = "none";
				document.getElementById('detail-form').style.display = "block";

				//document.getElementById('add-homework-section').style.display = "none";
				//document.getElementById('add-homework-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	
//**************************************************CHECK HOMEWORK**********************************************************************************************

	$scope.NextPreviewImage = function (val) {
		if ($scope.HomeWorkCorrectDetails)
		{
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].File = flll;

			var nInd = $scope.HomeWorkCorrectDetails.SelectedIndex + val;

			if (nInd > -1 && nInd < $scope.HomeWorkCorrectDetails.FilesColl.length)
			{
				$scope.HomeWorkCorrectDetails.SelectedIndex = nInd;
				var sfl=$scope.HomeWorkCorrectDetails.FilesColl[nInd];				
				if (sfl.File)
					imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
				else
					imageEditor.loadImageFromURL(sfl.Path, sfl.FileName);
            }
			
		}
	};

	$scope.ChangeImage = function (sfl) {

		if ($scope.HomeWorkCorrectDetails )
		{
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].File = flll;
			$scope.HomeWorkCorrectDetails.SelectedIndex = sfl.Index;


			if(sfl.File)
				imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
			else
				imageEditor.loadImageFromURL(sfl.Path, sfl.FileName);
        }
	};
	$scope.GetAddHomeworkChecksById = function (hdc) {

		$scope.loadingstatus = "running";
		$scope.HomeWorkCorrectDetails = {};
				
		$scope.HomeWorkCorrectDetails = hdc;		
		$scope.HomeWorkCorrectDetails.CheckRemarks1 = hdc.CheckedRemarks;

		if (hdc.CheckStatus == 'Checked')
			$scope.HomeWorkCorrectDetails.CheckStatus1 = 1;
		else
			$scope.HomeWorkCorrectDetails.CheckStatus1 = 1;

		$scope.HomeWorkCorrectDetails.FilesColl = [];

		$timeout(function () {
			var fileInd = 0;
			angular.forEach($scope.HomeWorkCorrectDetails.StudentAttachments.split("##"), function (fl) {
				if (fl && fl.trim().length > 0)
				{
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
					$scope.HomeWorkCorrectDetails.FilesColl.push(fDet);
					fileInd = fileInd + 1;
				}

			});
		});

		$timeout(function () {
			if ($scope.HomeWorkCorrectDetails.FilesColl && $scope.HomeWorkCorrectDetails.FilesColl.length > 0) {
				$scope.HomeWorkCorrectDetails.SelectedIndex = 0;
				$scope.InitImgEditor($scope.HomeWorkCorrectDetails.FilesColl[0].Path);
				
			}

			$scope.newAddHomework.Mode = 'Save';
			document.getElementById('status-tab').style.display = "none";
			document.getElementById('hw-correction-part').style.display = "block";
		});

		
	};

	$scope.GetAddHomeworkDoneById = function (refData) {

		$scope.HomeWorkDetails.SubmitHomeWorksCOll = [];
		$scope.HomeWorkDetails.PendingHomeWorksCOll = [];
		$scope.loadingstatus = "running";
		
		$scope.HomeWorkDetails = {};
		var para = {
			hwId: refData.homeWorkId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Creation/GetHomeWorkCorrectById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			
			$scope.loadingstatus = "stop";
			if (res.data) {

				var query = res.data;
				if (query.length > 0) {
					$scope.HomeWorkDetails.FData = query[0];

					angular.forEach(query, function (q) {

						if (q.SubmitStatus == "Pending")
							$scope.HomeWorkDetails.PendingHomeWorksCOll.push(q);
						else
							$scope.HomeWorkDetails.SubmitHomeWorksCOll.push(q);
					});
				}


				$scope.newAddHomework.Mode = 'Save';
				document.getElementById('homework-list-section').style.display = "none";
				document.getElementById('detail-form').style.display = "block";

				//document.getElementById('add-homework-section').style.display = "none";
				//document.getElementById('add-homework-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	
	$scope.dataURLtoBlob =function(dataurl) {
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
	
	$scope.CheckHomeWorkd = function () {

		if ($scope.HomeWorkCorrectDetails && $scope.HomeWorkCorrectDetails.CheckStatus1 &&  $scope.HomeWorkCorrectDetails.CheckStatus1 > 0)
		{
			var checkHome = {
				StudentId: $scope.HomeWorkCorrectDetails.StudentId,
				HomeWorkId: $scope.HomeWorkCorrectDetails.HomeWorkId,
				Status: $scope.HomeWorkCorrectDetails.CheckStatus1,
				Notes: $scope.HomeWorkCorrectDetails.CheckRemarks1,				
				FilesColl: []
			};

			$scope.RFiles = [];
			var dataURL = imageEditor.toDataURL();
			var flll = dataURItoFile(dataURL);
			
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].DATAURL = dataURL;
			$scope.HomeWorkCorrectDetails.FilesColl[$scope.HomeWorkCorrectDetails.SelectedIndex].File = flll;
			
			angular.forEach($scope.HomeWorkCorrectDetails.FilesColl, function (fl) {
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
						url: base_url + "Homework/Creation/CheckHomeWork",
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

								var query = mx($scope.HomeWorkDetails.SubmitHomeWorksCOll).firstOrDefault(p1 => p1.StudentId == checkHome.StudentId);
								if (query) {
									query.IsCheck = true;
									query.CheckStatus = ($scope.HomeWorkCorrectDetails.CheckStatus1 == 1 ? 'DONE' : 'RE-DO');
								}
									

								if (imageEditor)
									imageEditor.destroy();

								$scope.HomeWorkCorrectDetails = {};

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

	$scope.GetAllHomeworkwithout = function () {

		$scope.loadingstatus = "running";		
		$scope.HomeworkDataColl = [];
		$http({
			method: 'POST',
			url: base_url + "Homework/Creation/GetHomeworkColl",
			dataType: "json"
		}).then(function (res) {
			
			$scope.loadingstatus = "stop";
			if (res.data) {			
				$scope.HomeworkDataColl = [];
				angular.forEach(res.data, function (st) {
					angular.forEach(st.DataColl, function (Det) {
						$scope.HomeworkDataColl.push(Det);
					});
				});

				$scope.HomeworkDataColl = $filter('orderBy')($scope.HomeworkDataColl, 'AsignDateTime_AD', true);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.DelHomeworkById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				

				var para = {
					homeWorkId: refData.HomeWorkId
				};
				$http({
					method: 'POST',
					url: base_url + "Homework/Creation/DelHomeworkById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess) {
						$timeout(function () {
							$scope.GetAllHomeworkwithout();						
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

	$scope.EditHomeworkDet = {};
	//Edit button show hide starts
	$scope.editbtn = function (homDet)
	{
		$scope.EditHomeworkDet = homDet;
		$scope.EditHomeworkDet.AttFilesColl = [];
		angular.forEach(homDet.AttachmentColl, function (doc) {
			var fpath = (WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/');
			$scope.EditHomeworkDet.AttFilesColl.push(fpath);
		});

		$scope.EditHomeworkDet.DeadlineDate_TMP= new Date(homDet.DeadlineDate_AD);
		$scope.EditHomeworkDet.DeadlineTime_TMP = new Date(homDet.DeadlineTime);

		if (homDet.DeadlineforRedo && homDet.DeadlineforRedo!=null)
			$scope.EditHomeworkDet.DeadlineforRedo_TMP = new Date(homDet.DeadlineforRedo);

		if (homDet.DeadlineforRedoTime && homDet.DeadlineforRedoTime != null)
			$scope.EditHomeworkDet.DeadlineForReDoTime_TMP = new Date(homDet.DeadlineforRedoTime);

		document.getElementById('edit-homework-form').style.display = "block";
		document.getElementById('add-homework-section').style.display = "none";

	}

	$scope.backTableBtn = function () {
		document.getElementById('add-homework-section').style.display = "block";
		document.getElementById('edit-homework-form').style.display = "none";

	}
	$scope.CallUpdateDeadlineHomeWork = function () {


		$scope.loadingstatus = "running";

		$timeout(function () {
			if ($scope.EditHomeworkDet.DeadlineDateDet) {
				$scope.EditHomeworkDet.DeadlineDate = $filter('date')(new Date($scope.EditHomeworkDet.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
			} else
				$scope.EditHomeworkDet.DeadlineDate = null;


			if ($scope.EditHomeworkDet.DeadlineforRedoDet) {
				$scope.EditHomeworkDet.DeadlineforRedo = $filter('date')(new Date($scope.EditHomeworkDet.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd');
			} else
				$scope.EditHomeworkDet.DeadlineforRedo = null;

			if ($scope.EditHomeworkDet.DeadlineTime_TMP)
				$scope.EditHomeworkDet.DeadlineTime = $scope.EditHomeworkDet.DeadlineTime_TMP.toLocaleString();
			else
				$scope.EditHomeworkDet.DeadlineTime = null;

			if ($scope.EditHomeworkDet.DeadlineForReDoTime_TMP)
				$scope.EditHomeworkDet.DeadlineforRedoTime = $scope.EditHomeworkDet.DeadlineForReDoTime_TMP.toLocaleString();
			else
				$scope.EditHomeworkDet.DeadlineforRedoTime = null;

			$http({
				method: 'POST',
				url: base_url + "Homework/Creation/UpdateDeadlineHomeWork",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.EditHomeworkDet }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				Swal.fire(res.data.ResponseMSG);


			}, function (errormessage) {

				$scope.loadingstatus = "stop";

			});

		});

	}

});