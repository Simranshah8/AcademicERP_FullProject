
app.controller('StudentRecordController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Record';

	OnClickDefault();
	$scope.LoadData = function () {

		$scope.Remarks_Files_TMP = null;
		$scope.Remarks_Files_Data = null;
		$scope.Notice_Files_TMP = null;
		$scope.Notice_Files_Data = null;

		$scope.StudentRecord = {
			ClassId: null,
			SectionIdColl:''
		};

		$scope.V = {};

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

		$scope.RemarkTypeColl = [];
		$http.get(base_url + "StudentRecord/Creation/GetRemarksTypeList")
			.then(function (data) {
				$scope.RemarkTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Record: 1,
			StudentRemarks: 1,
					};

		$scope.searchData = {
			Record: '',
			StudentRemarks: '',
					};

		$scope.perPage = {
			Record: GlobalServices.getPerPageRow(),
			StudentRemarks: GlobalServices.getPerPageRow(),
					};

		$scope.newRemarks = {
			RecordId: null,
			RecordDetailsColl: [],
		};
		$scope.newNotice = {
			RecordId: null,
			RecordDetailsColl: [],
		};
	
		$scope.newStudentRemarks = {
			StudentRemarksId: null,
			StudentRemarksDetailsColl: [],
		};

	}
	

	$scope.ClearnewRemarks = function () {
		$timeout(function () {
			$('input[type=file]').val('');
			$scope.newRemarks = {
				StudentId: null,
				forDate: null,
				forDateDet: null,
				RemarksTypeId: null,
				description: null,
				studentIdColl: null				
			}
			$('#CboRemarksTypeId').val(null).trigger('change');
			
		})
		
    }

	$scope.ClearRecord = function () {
		$scope.newStudentRemarks = {
			StudentRemarksId: null,
			StudentRemarksDetailsColl: [],
		};

		$scope.newStudentRemarks.StudentRemarksDetailsColl.push({});
	}

	//*************************Record *********************************


	function OnClickDefault() {
		document.getElementById('add-remarks-form').style.display = "none";


		// Add Assignment Type
		document.getElementById('add-remarks').onclick = function () {
			document.getElementById('add-remarks-form').style.display = "block";
			document.getElementById('remarks-list').style.display = "none";

		}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('add-remarks-form').style.display = "none";

			document.getElementById('remarks-list').style.display = "block";
		}


	}

	$scope.checkFull = function () {

		var val = $scope.chkFull;

		if ($scope.StudentRecordList) {
			angular.forEach($scope.StudentRecordList, function (ec) {
				ec.Full = val;
			});
		}
	};
	
	$scope.GetAllRecord = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.Record = [];
		$http({
			method: 'POST',
			url: base_url + "StudentRecord/Creation/GetAllRecord",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data) {
				$scope.Record = res.data;
				//$scope.v=res.data;



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//*************************Student Remarks *********************************


	$('#ClassSectionId').on("change", function (e) {
		$scope.CSID = {}
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.CSID=JSON.parse(select_val);
		$scope.para = {
			ClassId: $scope.CSID.ClassId,
			SectionIdColl: $scope.CSID.SectionId

		};
		$scope.GetStudentColl();

	});

	$('#StudentId').on("change", function (e) {
		$scope.StudentId = {}
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newRemarks.studentIdColl = select_val;

	});

	$scope.GetStudentColl = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentColl = [];

		$http({
			method: 'POST',
			url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
			data: $scope.para,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.StudentColl = res.data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.GetStudentRecord = function () {

		$scope.StudentRecordList = [];
		$scope.loadingstatus = "running";
		
		var secCount = mx($scope.SectionClassColl).count(p1 => p1.ClassId == $scope.StudentRecord.ClassId && p1.SectionId>0);

		var para = {
			classId: $scope.StudentRecord.ClassId,
			sectionIdColl:($scope.StudentRecord.SectionIdColl ? $scope.StudentRecord.SectionIdColl.toString() : '')
		}

		$http({
			method: 'POST',
			url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
			data: JSON.stringify(para),
			dataType: "json"
		}).then(function (res)
		{
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.StudentRecordList = res.data;
				angular.forEach($scope.StudentRecordList, function (ec) {
					ec.Full =null;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveRemarksToStudent = function ()
	{
		$scope.loadingstatus = "running";

		var remarksFiles = [];
		//angular.forEach($scope.Remarks_Files_Data, function (fd) {
		//	remarksFiles.push(dataURItoFile(fd));
		//});

		angular.forEach($scope.Remarks_Files_TMP, function (fd) {
			remarksFiles.push(fd);
		});

		
		$scope.newRemarks.forDate = $filter('date')(new Date($scope.newRemarks.forDateDet.dateAD), 'yyyy-MM-dd'); 

		var StudentArray = []
		angular.forEach($scope.StudentRecordList, function (ec) {
			if (ec.Full == true) {
				StudentArray.push(ec.StudentId)
			}
		});
		$scope.newRemarks.studentIdColl = (StudentArray && StudentArray.length > 0 ? StudentArray.toString() : '')

		$http({
			method: 'POST',
			url: base_url + "StudentRecord/Creation/AddRemarksToStudent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.files && data.files.length > 0)
					formData.append("file1", data.files[0]);

				return formData;
			},
			data: { jsonData: $scope.newRemarks, files: remarksFiles }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();



			if (res.data.IsSuccess == true) {
				Swal.fire('Sent Successfully');
				$scope.ClearnewRemarks();
				//	$scope.GetAllAddAssignmentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
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

	$scope.ClearNotice = function () {
		$scope.newNotice = {
			title: '',
			description:''
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
				$scope.newNotice.studentIdColl = (StudentArray && StudentArray.length > 0 ? StudentArray.toString() : '')

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
						$scope.ClearNotice();
					}

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}, 100);
        })		
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             