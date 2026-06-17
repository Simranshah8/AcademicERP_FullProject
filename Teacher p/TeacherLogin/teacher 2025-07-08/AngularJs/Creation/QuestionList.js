
app.controller('QuestionListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Question List';

	//OnClickDefault();
	$scope.LoadData = function () {
		
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/ExamTypeforEntity")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});


		
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			QuestionList: 1,

		};

		$scope.searchData = {
			QuestionList: '',

		};

		$scope.perPage = {
			QuestionList: GlobalServices.getPerPageRow(),

		};

		$scope.newQuestionList = {
			QuestionListId: null,

			Mode: 'Save'
		};

		//$scope.GetAllQuestionListList();

	};


	$scope.ClearQuestionList = function () {
		$scope.newQuestionList = {
			QuestionListId: null,

			Mode: 'Save'
		};
	};
	$('#cboClassId').on("change", function (e) {
		$scope.SectionColl = [];
		$scope.SectionList = [];
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newQuestionList.ClassId = select_val
		angular.forEach($scope.SectionClassColl, function (SVCollData) {
			if (select_val == SVCollData.ClassId) {

				$scope.Section = SVCollData;
				$scope.SectionColl.push($scope.Section);
			}


		})
		$timeout(function () {
			$scope.SectionList = $scope.SectionColl;
		});



	});

	$('#cboExamTypeId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newQuestionList.ExamTypeId = select_val
		$scope.GetAllQuestionList();
	});
	$('#cboClassId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newQuestionList.ClassId = select_val
		$scope.GetAllQuestionList();
	});
	$('#cboSectionId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.SectionId = select_val;
		$scope.newQuestionList.sectionIdColl = ($scope.SectionId && $scope.SectionId.length > 0 ? $scope.SectionId.toString() : '')
		$scope.GetSubjectList();
		$scope.GetAllQuestionList();
	});
	
	$('#cboSubjectId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newQuestionList.SubjectId = select_val
		$scope.GetAllQuestionList();
	});

	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if ($scope.newQuestionList.sectionIdColl && $scope.newQuestionList.ClassId) {
			var para = {
				classId: $scope.newQuestionList.ClassId,
				sectionIdColl: $scope.newQuestionList.sectionIdColl
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
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


	$scope.SaveUpdateQuestionList = function () {
		if ($scope.IsValidQuestionList() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newQuestionList.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateQuestionList();
					}
				});
			} else
				$scope.CallSaveUpdateQuestionList();

		}
	};

	$scope.CallSaveUpdateQuestionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newQuestionList.QuestionListDateDet) {
			$scope.newQuestionList.QuestionListDate = $scope.newQuestionList.QuestionListDateDet.dateAD;
		} else
			$scope.newQuestionList.QuestionListDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveQuestionList",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newQuestionList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearQuestionList();
				$scope.GetAllQuestionListList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.GetAllQuestionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.QuestionList = [];
		if ($scope.newQuestionList.ExamTypeId && $scope.newQuestionList.ClassId && $scope.newQuestionList.sectionIdColl && $scope.newQuestionList.SubjectId) {
			var Dt = {
				examTypeId: $scope.newQuestionList.ExamTypeId,
				classId: $scope.newQuestionList.ClassId,
				sectionIdColl: $scope.newQuestionList.sectionIdColl,
				subjectId: $scope.newQuestionList.SubjectId

			}

			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Creation/GetQuestionList",
				data: Dt,
				dataType: "json"
			}).then(function (res) {
				if (res.data) {

					var dataColl = res.data;		
					var sno = 1;
					angular.forEach(dataColl, function (dc) {
						dc.SNo = sno;
						$scope.QuestionList.push(dc);
						sno++;
					});
					$scope.QuestionList = dataColl;

					//var query = dataColl.groupBy(t => t.CategoryName).toArray();
					//var sno = 1;
					//angular.forEach(query, function (q) {
					//	var beData = {
					//		SNo: sno,
					//		CategoryName: q.key,
					//		DataColl: q.elements
					//	};					
					//	sno++;
					//	$scope.QuestionList.push(beData);
					//})	

				}
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	
	};

	$scope.GetQuestionListById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			QuestionListId: refData.QuestionListId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetQuestionListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newQuestionList = res.data.Data;
				$scope.newQuestionList.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelQuestionListById = function (refData) {

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
					tranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "OnlineExam/Creation/DelQuestionList",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllQuestionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//Added Js part
	$scope.downFilePath = '';
	$scope.DocumentAtt_Toggle = function (fpath) {		
		if (fpath !== '') {
			$scope.downFilePath = WEBURLPATH+fpath;
			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src = WEBURLPATH +fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}


	};

});