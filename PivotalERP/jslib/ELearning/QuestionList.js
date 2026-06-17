
app.controller('QuestionListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Question List';

	//OnClickDefault();
	var glbS = GlobalServices;

	$scope.LoadData = function () {
		
		$scope.ExamTypeColl = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionClassColl = [];
		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
			$scope.ClassColl = res.data.Data.ClassList;
			$scope.SectionClassColl = res.data.Data.SectionList;
		}, function (reason) {
			Swal.fire('Failed' + reason);
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

	};


	$scope.GetClassWiseSubMap = function () {

		$scope.newQuestionList.SubjectList = [];

		if ($scope.newQuestionList.ClassId && $scope.newQuestionList.ClassId > 0) {
			var para = {
				ClassId: $scope.newQuestionList.ClassId,
				SectionIdColl: ($scope.newQuestionList.SectionId ? $scope.newQuestionList.SectionId.toString() : '')
			};
			 
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = 0;
								subDet.CRPR = 0;
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								subDet.SubjectType = 1;
								subDet.OTH = 1;
								subDet.OPR = 1;
								$scope.newQuestionList.SubjectList.push(subDet);
							}
						});

						$scope.GetAllQuestionList();
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.ClearQuestionList = function () {
		$scope.newQuestionList = {
			QuestionListId: null,

			Mode: 'Save'
		};
	};
	 
	     
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.GetAllQuestionList = function () {
	
		$scope.QuestionList = [];
		if ($scope.newQuestionList.ExamTypeId && $scope.newQuestionList.ClassId && $scope.newQuestionList.SubjectId) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				examTypeId: $scope.newQuestionList.ExamTypeId,
				classId: $scope.newQuestionList.ClassId,
				sectionIdColl: ($scope.newQuestionList.SectionId ? $scope.newQuestionList.SectionId.toString() : ''),
				subjectId: $scope.newQuestionList.SubjectId
			}

			$http({
				method: 'POST',
				url: base_url + "Elearning/Creation/GetQuestionList",
				data: JSON.stringify(para),
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.Data && res.data.IsSuccess==true) {

					var dataColl = res.data.Data;		
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
					url: base_url + "Elearning/Creation/DelQuestionList",
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