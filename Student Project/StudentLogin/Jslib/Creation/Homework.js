app.controller('HomeworkController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Homework';

	OnClickDefault();

	$scope.LoadData = function () {
		
		$scope.count = 0;
		$('.select2').select2();
		$scope.GetAllHomeworkList(); 
		$scope.perPageColl = GlobalServices.getPerPageList();  
		$scope.currentPages = {
			Homework: 1,
			SubmittedHomework: 1,
			NotSubmittedHomework: 1,
		};

		$scope.searchData = {
			Homework: '',
			SubmittedHomework: ''
		};

		$scope.perPage = {
			Homework: GlobalServices.getPerPageRow(),
			SubmittedHomework: GlobalServices.getPerPageRow(),
			NotSubmittedHomework: GlobalServices.getPerPageRow(),
		};

		$scope.newHomework = {
			HomeworkId: null,
			HomeworkListColl: []
		};
		$scope.newHomework.HomeworkListColl.push({});

		$scope.newSubmittedHomework = {
			SubmittedHomeworkId: null,

		};


		$scope.newToSubmitHomework = {
			SubmittedHomeworkId: null,

		}; 

	}

	function OnClickDefault() { 
		document.getElementById('submitted').style.display = "none"; 
		document.getElementById('resubmit').style.display = "none";
		document.getElementById('NotDone').style.display = "none"; 

		document.getElementById('back-to-homeworklist').onclick = function () {
			document.getElementById('homework-list').style.display = "block";
			document.getElementById('submitted').style.display = "none";
			$scope.ClearHomework();
		} 
		document.getElementById('back-to-list-resubmit').onclick = function () {
			document.getElementById('submitted-homework').style.display = "block";
			document.getElementById('resubmit').style.display = "none";
			$scope.ClearHomework();
		}

		document.getElementById('back-to-list-NotDone').onclick = function () {
			document.getElementById('homeworkNotDone').style.display = "block";
			document.getElementById('NotDone').style.display = "none";
			$scope.ClearHomework();
		}


		$scope.NotDoneStatus = function (V) {
			$scope.HomeWork3rd = {};
			$scope.HomeWork3rd.TeacherName = V.TeacherName;
			$scope.HomeWork3rd.SubjectName = V.SubjectName;
			$scope.HomeWork3rd.Lession = V.Lession;
			$scope.HomeWork3rd.Topic = V.Topic;
			$scope.HomeWork3rd.Description = V.Description;
			$scope.HomeWork3rd.AsignDateTime_AD = V.AsignDateTime_AD;
			$scope.HomeWork3rd.HomeWorkId = V.HomeWorkId;
			$scope.HomeWork3rd.HomeWorkStatus = V.HomeWorkStatus;
			$scope.HomeWork3rd.TotalChecked = V.TotalChecked;
			$scope.HomeWork3rd.DeadlineTime = V.DeadlineTime;
			$scope.HomeWork3rd.CheckedRemarks = V.CheckedRemarks;
			$scope.HomeWork3rd.Notes = V.StudentNotes;
			$scope.HomeWork3rd.AttachmentColl = V.AttachmentColl;
			$scope.HomeWork3rd.StudentAttachmentsColl = V.StudentAttachmentsColl;
			$scope.HomeWork3rd.IsAllowLateSibmission = V.IsAllowLateSibmission;
			$scope.HomeWork3rd.HomeWorkId = V.HomeWorkId;
			document.getElementById('homeworkNotDone').style.display = "none";

			document.getElementById('NotDone').style.display = "block";
		} 
		$('a a').remove();

		document.documentElement.setAttribute("lang", "en");
		document.documentElement.removeAttribute("class");
	}
	$scope.modalIMG = function (item) {
		if (item !== '') {
			$scope.TeacherDoc = item
			document.body.style.cursor = 'wait';
			document.getElementById("frmTeacherDoc").src = '';                        
			document.getElementById("frmTeacherDoc").src = item;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
        }
		
	};

	$scope.selectStatus = function () {
		$scope.Status;
	}
	$scope.onSelectHomewoskType = function (cl) {
		var Val = JSON.parse(cl);
		$scope.HomewoskTypeName = Val.Name;
    }
	$scope.modalIMG_resubmit = function (item) {
		if (item !== '') {
			$scope.TeacherDoc = item
			document.body.style.cursor = 'wait';
			document.getElementById("frmTeacherDoc_R").src = '';
			document.getElementById("frmTeacherDoc_R").src = item;
			document.body.style.cursor = 'default';
			$('#modalIMG-resubmit').modal('show');
        }
		
	};


	$scope.NotSubmitted_resubmit = function (item) {
		if (item !== '') {
			$scope.TeacherDoc = item
			document.body.style.cursor = 'wait';
			document.getElementById("NotSubmitted_Iframe").src = '';
			document.getElementById("NotSubmitted_Iframe").src = item;
			document.body.style.cursor = 'default';
			$('#modalIMG-NotSubmitted').modal('show');
		}

	};


	$scope.CheckedAttach = function (item) {
		if (item !== '') {
			$scope.CheckedDoc = item
			 
			$('#CheckedAttach').modal('show');
		}

	};
	$scope.ResubmitToggel = function (item) {
		if (item !== '') {
			$scope.CheckedDoc = item 
			$('#ResubmitToggel').modal('show');
		}

	};


	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.Submitted = function (V) {
		$scope.HomeWork = {};
		$scope.HomeWork.TeacherName = V.TeacherName;
		$scope.HomeWork.SubjectName = V.SubjectName;
		$scope.HomeWork.Lession = V.Lession;
		$scope.HomeWork.Topic = V.Topic;
		$scope.HomeWork.Description = V.Description;
		$scope.HomeWork.AsignDateTime_AD = V.AsignDateTime_AD;
		$scope.HomeWork.HomeWorkId = V.HomeWorkId;
		$scope.HomeWork.HomeWorkStatus = V.HomeWorkStatus;
		$scope.HomeWork.TotalChecked = V.TotalChecked;
		$scope.HomeWork.DeadlineTime = V.DeadlineTime;
		$scope.HomeWork.CheckedRemarks = V.CheckedRemarks;
		$scope.HomeWork.Notes = V.StudentNotes;
		$scope.HomeWork.AttachmentColl = V.AttachmentColl;
		$scope.HomeWork.StudentAttachmentsColl = V.StudentAttachmentsColl;
		$scope.HomeWork.HomeWorkId = V.HomeWorkId;
		 
		document.getElementById('homework-list').style.display = "none";
		document.getElementById('submitted').style.display = "block";
		$scope.ClearHomework();
	}
	$scope.OnClickStatus = function (V) {
		
		$scope.HomeWork2nd = {};
		$scope.HomeWork2nd.TeacherName = V.TeacherName;
		$scope.HomeWork2nd.SubjectName = V.SubjectName;
		$scope.HomeWork2nd.Lession = V.Lession;
		$scope.HomeWork2nd.Topic = V.Topic;
		$scope.HomeWork2nd.Description = V.Description;
		$scope.HomeWork2nd.AsignDateTime_AD = V.AsignDateTime_AD;
		$scope.HomeWork2nd.HomeWorkId = V.HomeWorkId;
		$scope.HomeWork2nd.HomeWorkStatus = V.HomeWorkStatus;
		$scope.HomeWork2nd.TotalChecked = V.TotalChecked;
		$scope.HomeWork2nd.DeadlineTime = V.DeadlineTime;
		$scope.HomeWork2nd.CheckedRemarks = V.CheckedRemarks;
		$scope.HomeWork2nd.Notes = V.StudentNotes;
		$scope.HomeWork2nd.AttachmentColl = V.AttachmentColl;
		$scope.HomeWork2nd.StudentAttachmentsColl = V.StudentAttachmentsColl;
		 
		document.getElementById('submitted-homework').style.display = "none";
		document.getElementById('resubmit').style.display = "block";
		$scope.ClearHomework();
		 

	}
	$scope.SelectDate = function () {
		$scope.V = {};
		if ($scope.newSubmittedHomework.FromDate && $scope.newSubmittedHomework.ToDate) {
			var res = $scope.newSubmittedHomework.FromDate.split("-");
			$scope.FromDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.V.dateFrom = $scope.FromDate.year + '-' + $scope.FromDate.month + '-' + $scope.FromDate.day;
			var res1 = $scope.newSubmittedHomework.ToDate.split("-");
			$scope.ToDate = NepaliFunctions.BS2AD({ year: res1[0], month: res1[1], day: res1[2] })
			$scope.V.dateTo = $scope.ToDate.year + '-' + $scope.ToDate.month + '-' + $scope.ToDate.day;
			$scope.GetSubmitedAllHomeworkList();
		}
		
	}

	
	$scope.GetSubmitedAllHomeworkList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkList = [];
		$scope.SubmitedHomework = [];
		

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/DateGetAllHomeworkList",
			data: $scope.V,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.HomeworkList = res.data; 
				angular.forEach($scope.HomeworkList, function (st) { 
					if (st.Attachments == '') {
						st.AttachmentColl = [];
					}
					else {
						st.AttachmentColl = st.Attachments.split("## ");
					}
					if (st.StudentAttachments == '') {
						st.StudentAttachmentsColl = [];
					}
					else {
						st.StudentAttachmentsColl = st.StudentAttachments.split("## ");
					}
					if (st.HomeWorkStatus == 'PENDING' || st.HomeWorkStatus == 'DONE') {
						$scope.SubmitedHomework.push(st);
					}  
				}); 
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	$scope.ToSubmittSelectDate = function () {
		$scope.V = {};
		if ($scope.newToSubmitHomework.FromDate && $scope.newToSubmitHomework.ToDate) {
			var res = $scope.newToSubmitHomework.FromDate.split("-");
			$scope.FromDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.V.dateFrom = $scope.FromDate.year + '-' + $scope.FromDate.month + '-' + $scope.FromDate.day;
			var res1 = $scope.newToSubmitHomework.ToDate.split("-");
			$scope.ToDate = NepaliFunctions.BS2AD({ year: res1[0], month: res1[1], day: res1[2] })
			$scope.V.dateTo = $scope.ToDate.year + '-' + $scope.ToDate.month + '-' + $scope.ToDate.day;
			$scope.ToSubmittAllHomeworkList();
		}

	}
	$scope.ToSubmittAllHomeworkList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkList = [];
		$scope.ToSubmitHomework = []; 
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/DateGetAllHomeworkList",
			data: $scope.V,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.HomeworkList = res.data; 
				angular.forEach($scope.HomeworkList, function (st) {


					if (st.Attachments == '') {
						st.AttachmentColl = [];
					}
					else {
						st.AttachmentColl = st.Attachments.split("## ");
					}
					if (st.StudentAttachments == '') {
						st.StudentAttachmentsColl = [];
					}
					else {
						st.StudentAttachmentsColl = st.StudentAttachments.split("## ");
					}
					if (st.HomeWorkStatus == 'PENDING' || st.HomeWorkStatus == 'DONE') { 
					}
					else if (st.HomeWorkStatus == 'RE-DO' || st.HomeWorkStatus == 'NEW' || st.IsAllowLateSibmission == true) {
						$scope.ToSubmitHomework.push(st)
					}

					else if (st.HomeWorkStatus == 'NOT DONE' && st.IsAllowLateSibmission == false) { 
					}


				}); 
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	$scope.ClearHomework = function () {
		$scope.newHomework = {
			HomeworkId: null,
			HomeworkListColl: []
		};
		$scope.newHomework.HomeworkListColl.push({});
	}
	$scope.ClearSubmittedHomework = function () {
		$scope.newSubmittedHomework = {
			SubmittedHomeworkId: null,

		};
	}

	//Get HomeWorkTypeList
	$scope.HomeworkColl = [];
	$http.get(base_url + "Student/Creation/GetHomeworkTypeList")
		.then(function (data) {
			$scope.HomeworkColl = data.data; 
		}, function (reason) {
			alert("Data not get");
		});
	
	$scope.GetAllHomeworkList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HomeworkList = [];
		$scope.ToSubmitHomework = [];
		$scope.SubmitedHomework = [];
		$scope.NotDoneHomework = []; 
			$http({
				method: 'POST',
				url: base_url + "Student/Creation/GetAllHomeworkList", 
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data && res.data) {
					$scope.HomeworkList = res.data;
					angular.forEach($scope.HomeworkList, function (st) {

						

						if (st.Attachments == '') {
							st.AttachmentColl = [];
						}
						else {
							st.AttachmentColl = st.Attachments.split("## ");
						}
						if (st.StudentAttachments == '') {
							st.StudentAttachmentsColl = [];
						}
						else {
							st.StudentAttachmentsColl = st.StudentAttachments.split("## ");
						}
						
						 if (st.HomeWorkStatus == 'PENDING' || st.HomeWorkStatus == 'DONE') {
							$scope.SubmitedHomework.push(st);
						}
						else if (st.HomeWorkStatus == 'RE-DO' || st.HomeWorkStatus == 'NEW' /*|| st.IsAllowLateSibmission == true*/) {
							$scope.ToSubmitHomework.push(st)
						}
						
						 else if (st.HomeWorkStatus == 'NOT DONE' /*&& st.IsAllowLateSibmission == false*/) {
							 
							$scope.NotDoneHomework.push(st);
						} 

					}); 
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        
		

	}

	
	$scope.AfterChooseFile = function () {
		
		if (!$scope.newHomework.uploadFiles)
			$scope.newHomework.uploadFiles = [];

		if ($scope.Files_TMP && $scope.Files_Data && $scope.Files_TMP.length==$scope.Files_Data.length) {
		

			var ind = 0;
			angular.forEach($scope.Files_TMP, function (fl)
			{
				$scope.newHomework.uploadFiles.push({
					file: fl,
					FileName: fl.name,
					uploadFiles: $scope.Files_Data[ind]
				});
				ind++;
			});
			
		}
	
	};
	
	
	$scope.DeleteAlbumPhoto = function (id) {
		$scope.loadingstatus = "running";
		$scope.loadingstatus = "stop";
		Swal.fire({
			icon: "info",
			text: 'Are you sure to delete selected Photo ',
			buttons: true,
			dangerMode: true,
		}).then((e) => {
			if (e) {
				$timeout(function () {
					$scope.newHomework.uploadFiles.splice(id, 1);
				});

			}
			else {
				Swal.fire("Nothing Deleted", '', 'info');
			}
		})
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
		var filename = 'dataURI-file-' + (new Date()).getTime() + '.' + mime.split('/')[1];
		//var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
		var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
		var writer = new Uint8Array(new ArrayBuffer(bytes.length));

		for (var i = 0; i < bytes.length; i++) {
			writer[i] = bytes.charCodeAt(i);
		}

		return new File([writer.buffer], filename, { type: mime });
	}
	$scope.SubmitHomeWork = function (Val) {
		$scope.HomeWork.HomeWorkId = Val;
		
		$scope.loadingstatus = "running";
		showPleaseWait();
		var uploadFiles = [];

		
		angular.forEach($scope.newHomework.uploadFiles, function (Doc) {
			var rFile = dataURItoFile(Doc.uploadFiles);
			uploadFiles.push(rFile);
			

		});


		$http({
			method: 'POST',
			url: base_url + "Student/Creation/AddHomeWork",
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
			data: { jsonData: $scope.HomeWork, files: uploadFiles }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.GetAllHomeworkList();
				document.getElementById('homework-list').style.display = "block";
				document.getElementById('submitted').style.display = "none";
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.AllDownload = function (DocColl) {

		angular.forEach(DocColl, function (Doc) {
			const a = document.createElement('a')
			a.href = Doc
			a.download = Doc.split('/').pop()
			document.body.appendChild(a)
			a.click()
			document.body.removeChild(a)
		});
		

    } 
	$scope.downloadFile = function (filename) {

		linkElement.setAttribute('href', url);
		linkElement.setAttribute("download", filename);


	};
	
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});

app.directive('ngFiles', ['$parse', function ($parse) {

	function file_links(scope, element, attrs) {
		var onChange = $parse(attrs.ngFiles);
		element.on('change', function (event) {
			onChange(scope, { $files: event.target.files });
		});
	}

	return {
		link: file_links
	}
}]);

app.filter('getType', function () {
	return function (obj) {
		return _typeof(obj);
	};
});
