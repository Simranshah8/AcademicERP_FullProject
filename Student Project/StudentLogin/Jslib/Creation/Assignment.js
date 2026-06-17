app.controller('AssignmentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Assignment';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.count = 0;
		$('.select2').select2();
		$scope.GetAllAssignmentList();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.currentPages = {
			Assignment: 1,
			SubmittedAssignment: 1,
			NotSubmittedAssignment: 1,
		};

		$scope.searchData = {
			Assignment: '',
			SubmittedAssignment: ''
		};

		$scope.perPage = {
			Assignment: GlobalServices.getPerPageRow(),
			SubmittedAssignment: GlobalServices.getPerPageRow(),
			NotSubmittedAssignment: GlobalServices.getPerPageRow(),
		};

		$scope.newAssignment = {
			AssignmentId: null,
			AssignmentListColl: []
		};
		$scope.newAssignment.AssignmentListColl.push({});

		$scope.newSubmittedAssignment = {
			SubmittedAssignmentId: null,

		};


		$scope.newToSubmitAssignment = {
			SubmittedAssignmentId: null,

		};

	}

	function OnClickDefault() {
		document.getElementById('submitted').style.display = "none";
		document.getElementById('resubmit').style.display = "none";
		document.getElementById('NotDone').style.display = "none";

		document.getElementById('back-to-Assignmentlist').onclick = function () {
			document.getElementById('Assignment-list').style.display = "block";
			document.getElementById('submitted').style.display = "none";
			$scope.ClearAssignment();
		}
		document.getElementById('back-to-list-resubmit').onclick = function () {
			document.getElementById('submitted-Assignment').style.display = "block";
			document.getElementById('resubmit').style.display = "none";
			$scope.ClearAssignment();
		}

		document.getElementById('back-to-list-NotDone').onclick = function () {
			document.getElementById('AssignmentNotDone').style.display = "block";
			document.getElementById('NotDone').style.display = "none";
			$scope.ClearAssignment();
		}


		$scope.NotDoneStatus = function (V) {
			$scope.Assignment3rd = {};
			$scope.Assignment3rd.TeacherName = V.TeacherName;
			$scope.Assignment3rd.SubjectName = V.SubjectName;
			$scope.Assignment3rd.Title = V.Title;			
			$scope.Assignment3rd.Description = V.Description;
			$scope.Assignment3rd.AsignDateTime_AD = V.AsignDateTime_AD;
			$scope.Assignment3rd.AssignmentId = V.AssignmentId;
			$scope.Assignment3rd.AssignmentStatus = V.AssignmentStatus;
			$scope.Assignment3rd.TotalChecked = V.TotalChecked;
			$scope.Assignment3rd.DeadlineTime = V.DeadlineTime;
			$scope.Assignment3rd.CheckedRemarks = V.CheckedRemarks;
			$scope.Assignment3rd.Notes = V.StudentNotes;
			$scope.Assignment3rd.AttachmentColl = V.AttachmentColl;
			$scope.Assignment3rd.StudentAttachmentsColl = V.StudentAttachmentsColl;
			$scope.Assignment3rd.IsAllowLateSibmission = V.IsAllowLateSibmission;
			$scope.Assignment3rd.AssignmentId = V.AssignmentId;
			document.getElementById('AssignmentNotDone').style.display = "none";

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
		$scope.Assignment = {};
		$scope.Assignment.TeacherName = V.TeacherName;
		$scope.Assignment.SubjectName = V.SubjectName;
		$scope.Assignment.Title = V.Title;		
		$scope.Assignment.Description = V.Description;
		$scope.Assignment.AsignDateTime_AD = V.AsignDateTime_AD;
		$scope.Assignment.AssignmentId = V.AssignmentId;
		$scope.Assignment.AssignmentStatus = V.AssignmentStatus;
		$scope.Assignment.TotalChecked = V.TotalChecked;
		$scope.Assignment.DeadlineTime = V.DeadlineTime;
		$scope.Assignment.CheckedRemarks = V.CheckedRemarks;
		$scope.Assignment.Notes = V.StudentNotes;
		$scope.Assignment.AttachmentColl = V.AttachmentColl;
		$scope.Assignment.StudentAttachmentsColl = V.StudentAttachmentsColl;
		$scope.Assignment.AssignmentId = V.AssignmentId;

		document.getElementById('Assignment-list').style.display = "none";
		document.getElementById('submitted').style.display = "block";
		$scope.ClearAssignment();
	}
	$scope.OnClickStatus = function (V) {

		$scope.Assignment2nd = {};
		$scope.Assignment2nd.TeacherName = V.TeacherName;
		$scope.Assignment2nd.SubjectName = V.SubjectName;
		$scope.Assignment2nd.Title = V.Title;		
		$scope.Assignment2nd.Description = V.Description;
		$scope.Assignment2nd.AsignDateTime_AD = V.AsignDateTime_AD;
		$scope.Assignment2nd.AssignmentId = V.AssignmentId;
		$scope.Assignment2nd.AssignmentStatus = V.AssignmentStatus;
		$scope.Assignment2nd.TotalChecked = V.TotalChecked;
		$scope.Assignment2nd.DeadlineTime = V.DeadlineTime;
		$scope.Assignment2nd.CheckedRemarks = V.CheckedRemarks;
		$scope.Assignment2nd.Notes = V.StudentNotes;
		$scope.Assignment2nd.AttachmentColl = V.AttachmentColl;
		$scope.Assignment2nd.StudentAttachmentsColl = V.StudentAttachmentsColl;

		document.getElementById('submitted-Assignment').style.display = "none";
		document.getElementById('resubmit').style.display = "block";
		$scope.ClearAssignment();


	}
	$scope.SelectDate = function () {
		$scope.V = {};
		if ($scope.newSubmittedAssignment.FromDate && $scope.newSubmittedAssignment.ToDate) {
			var res = $scope.newSubmittedAssignment.FromDate.split("-");
			$scope.FromDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.V.dateFrom = $scope.FromDate.year + '-' + $scope.FromDate.month + '-' + $scope.FromDate.day;
			var res1 = $scope.newSubmittedAssignment.ToDate.split("-");
			$scope.ToDate = NepaliFunctions.BS2AD({ year: res1[0], month: res1[1], day: res1[2] })
			$scope.V.dateTo = $scope.ToDate.year + '-' + $scope.ToDate.month + '-' + $scope.ToDate.day;
			$scope.GetSubmitedAllAssignmentList();
		}

	}


	$scope.GetSubmitedAllAssignmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignmentList = [];
		$scope.SubmitedAssignment = [];


		$http({
			method: 'POST',
			url: base_url + "Student/Creation/DateGetAllAssignmentList",
			data: $scope.V,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.AssignmentList = res.data;
				angular.forEach($scope.AssignmentList, function (st) {
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
					if (st.AssignmentStatus == 'PENDING' || st.AssignmentStatus == 'DONE') {
						$scope.SubmitedAssignment.push(st);
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
		if ($scope.newToSubmitAssignment.FromDate && $scope.newToSubmitAssignment.ToDate) {
			var res = $scope.newToSubmitAssignment.FromDate.split("-");
			$scope.FromDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.V.dateFrom = $scope.FromDate.year + '-' + $scope.FromDate.month + '-' + $scope.FromDate.day;
			var res1 = $scope.newToSubmitAssignment.ToDate.split("-");
			$scope.ToDate = NepaliFunctions.BS2AD({ year: res1[0], month: res1[1], day: res1[2] })
			$scope.V.dateTo = $scope.ToDate.year + '-' + $scope.ToDate.month + '-' + $scope.ToDate.day;
			$scope.ToSubmittAllAssignmentList();
		}

	}
	$scope.ToSubmittAllAssignmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignmentList = [];
		$scope.ToSubmitAssignment = [];
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/DateGetAllAssignmentList",
			data: $scope.V,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.AssignmentList = res.data;
				angular.forEach($scope.AssignmentList, function (st) {


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
					if (st.AssignmentStatus == 'PENDING' || st.AssignmentStatus == 'DONE') {
					}
					else if (st.AssignmentStatus == 'RE-DO' || st.AssignmentStatus == 'NEW' || st.IsAllowLateSibmission == true) {
						$scope.ToSubmitAssignment.push(st)
					}

					else if (st.AssignmentStatus == 'NOT DONE' && st.IsAllowLateSibmission == false) {
					}


				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	$scope.ClearAssignment = function () {
		$scope.newAssignment = {
			AssignmentId: null,
			AssignmentListColl: []
		};
		$scope.newAssignment.AssignmentListColl.push({});
	}
	$scope.ClearSubmittedAssignment = function () {
		$scope.newSubmittedAssignment = {
			SubmittedAssignmentId: null,

		};
	}

	//Get AssignmentTypeList
	$scope.AssignmentColl = [];
	$http.get(base_url + "Student/Creation/GetAssignmentTypeList")
		.then(function (data) {
			$scope.AssignmentColl = data.data;
		}, function (reason) {
			alert("Data not get");
		});

	$scope.GetAllAssignmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignmentList = [];
		$scope.ToSubmitAssignment = [];
		$scope.SubmitedAssignment = [];
		$scope.NotDoneAssignment = [];
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetAllAssignmentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.AssignmentList = res.data;
				angular.forEach($scope.AssignmentList, function (st) {



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

					if (st.AssignmentStatus == 'PENDING' || st.AssignmentStatus == 'DONE') {
						$scope.SubmitedAssignment.push(st);
					}
					else if (st.AssignmentStatus == 'RE-DO' || st.AssignmentStatus == 'NEW' /*|| st.IsAllowLateSibmission == true*/) {
						$scope.ToSubmitAssignment.push(st)
					}

					else if (st.AssignmentStatus == 'NOT DONE' /*&& st.IsAllowLateSibmission == false*/) {

						$scope.NotDoneAssignment.push(st);
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

		if (!$scope.newAssignment.uploadFiles)
			$scope.newAssignment.uploadFiles = [];

		if ($scope.Files_TMP && $scope.Files_Data && $scope.Files_TMP.length == $scope.Files_Data.length) {


			var ind = 0;
			angular.forEach($scope.Files_TMP, function (fl) {
				$scope.newAssignment.uploadFiles.push({
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
					$scope.newAssignment.uploadFiles.splice(id, 1);
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
	$scope.SubmitAssignment = function (Val) {
		$scope.Assignment.AssignmentId = Val;

		$scope.loadingstatus = "running";
		showPleaseWait();
		var uploadFiles = [];


		angular.forEach($scope.newAssignment.uploadFiles, function (Doc) {
			var rFile = dataURItoFile(Doc.uploadFiles);
			uploadFiles.push(rFile);


		});


		$http({
			method: 'POST',
			url: base_url + "Student/Creation/AddAssignment",
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
			data: { jsonData: $scope.Assignment, files: uploadFiles }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.GetAllAssignmentList();
				document.getElementById('Assignment-list').style.display = "block";
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
