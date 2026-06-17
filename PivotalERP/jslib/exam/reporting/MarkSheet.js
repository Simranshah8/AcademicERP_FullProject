app.controller('marksheetRptController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Marksheet';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

		$scope.TemplateList = [
			{ id: 1, text: "GradesheetHTML", Folder:"Marksheet" },
			{ id: 2, text: "MarkesheetHTML", Folder: "Marksheet" }
		];

		$scope.entity = {
			MarkSheet: 360,
			GroupMarkSheet: 362,
			ParentMarkSheet: 0,
			ReMarkSheet: entityReExamMarkSheet
		};
		$('.select2').select2({
			AllowClear:true,
		});

		$scope.BranchColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchListForEntry",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchColl = res.data.Data;
			}
		}, function (reason) {
			alert('Failed' + reason);
		});

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

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


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ReExamTypeList = [];
		gSrv.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ExamTypeGroupList = [];
		gSrv.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newNormalTabulation = {
			SelectedClass: null,
			ExamTypeId: null,
			TemplatesColl: [],
			ReportTemplateId:0,
			RptTranId: 0,
			HTMLTemplate: ''
		};

		$scope.newReNormalTabulation = {
			SelectedClass: null,
			ExamTypeId: null,
			TemplatesColl: [],
			ReportTemplateId:0,
			RptTranId: 0
		};

		$scope.newGroupTabulation = {
			SelectedClass: null,
			ExamTypeId: null,
			TemplatesColl: [],
			RptTranId: 0,
			CurExamTypeId:0
		};

		$scope.newResultDispatch = {
			SelectedClass: null,
			ExamTypeId: null,
			StudentList: [],
			Mode: 'save'
		};

		$scope.LoadReportTemplates();

		//$scope.AllStudentColl = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Academic/Transaction/GetAllStudentForTran",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data)
		//		$scope.AllStudentColl = res.data.Data;

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

	}

	$scope.GetBranchId = function (bid) {
		if ($scope.BranchColl.length == 1)
			return $scope.BranchColl[0].BranchId
		else
			return bid;
	}

	$scope.LoadReportTemplates=function(){

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.MarkSheet + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res)
		{
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newNormalTabulation.TemplatesColl = res.data.Data;
			 
            }
				
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.ReMarkSheet + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
			 
				$scope.newReNormalTabulation.TemplatesColl = res.data.Data;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.GroupMarkSheet + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newGroupTabulation.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
    }

	$scope.LoadClassWiseSemesterYear = function (classId) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassList.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass) {
			var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};
	$scope.ChangeNormalClass = function () {
		$scope.SelectedClass = null;
		if ($scope.newNormalTabulation.SelectedClass && $scope.newNormalTabulation.SelectedClass.length == 1) {
			$scope.SelectedClass = $scope.newNormalTabulation.SelectedClass[0];
			$scope.LoadClassWiseSemesterYear($scope.SelectedClass.ClassId);
        }
		$scope.PrintNormalTabulation();
    }
	$scope.PrintNormalTabulation = function (fromRef)
	{
		if (fromRef == 1 || fromRef == 2)
			$scope.GetExamWiseTemplate();

		if ($scope.newNormalTabulation.SelectedClass && $scope.newNormalTabulation.ExamTypeId && $scope.newNormalTabulation.RptTranId && $scope.newNormalTabulation.SelectedClass.length>0) {

			var selectRpt = mx($scope.newNormalTabulation.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newNormalTabulation.RptTranId);

			var EntityId = $scope.entity.MarkSheet;

			var tmpCS = $scope.newNormalTabulation.SelectedClass[0];

			var tmpIdColl = [];
			var cIdColl = '';
			angular.forEach($scope.newNormalTabulation.SelectedClass, function (cl) {
				
				if (mx(tmpIdColl).contains(cl.ClassId)==false)
					tmpIdColl.push(cl.ClassId);
			});

			cIdColl = tmpIdColl.toString();

			if ($scope.newNormalTabulation.SelectedClass.length == 1)
				cIdColl = '';

			var stIdColl = [];
			angular.forEach($scope.newNormalTabulation.StudentIdColl, function (s) {
				if (s > 0) {
					stIdColl.push(s);
                }
			});

			if ($scope.newNormalTabulation.RptTranId > 0) {
				var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newNormalTabulation.ExamTypeId);
				var rptPara = {
					istransaction: false,
					entityid: EntityId,
					ClassId: tmpCS.ClassId,
					SectionId: tmpCS.SectionId,
					ExamTypeId: $scope.newNormalTabulation.ExamTypeId,
					ClassName: tmpCS.text,
					FilterSection: ($scope.newNormalTabulation.FilterSection ? $scope.newNormalTabulation.FilterSection : true),
					ExamName: (examN ? examN.DisplayName : ''),
					rptTranId: $scope.newNormalTabulation.RptTranId,
					classIdColl: (cIdColl == '0' ? '' : cIdColl),
					BatchId: ($scope.newNormalTabulation.BatchId ? $scope.newNormalTabulation.BatchId : 0),
					SemesterId: ($scope.newNormalTabulation.SemesterId ? $scope.newNormalTabulation.SemesterId : 0),
					ClassYearId: ($scope.newNormalTabulation.ClassYearId ? $scope.newNormalTabulation.ClassYearId : 0),
					FromPublished: ($scope.newNormalTabulation.FromPublished ? $scope.newNormalTabulation.FromPublished : false),
					BranchId: ($scope.newNormalTabulation.BranchId ? $scope.newNormalTabulation.BranchId : 0),
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				if (selectRpt.IsRDLC == true) {
					document.getElementById("frmRptTabulation").src = base_url + "Exam/Report/RdlcMarkSheet?" + paraQuery;
				} else {
					document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				}
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
            }			
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}	

	};

	$scope.PrintGroupTabulation = function (fromRef) {

		if (fromRef == 1 || fromRef == 2)
			$scope.GetExamGroupWiseTemplate();

		if ($scope.newGroupTabulation.SelectedClass && $scope.newGroupTabulation.ExamTypeGroupId && $scope.newGroupTabulation.RptTranId) {
			var EntityId = $scope.entity.GroupMarkSheet;

			var stIdColl = [];
			angular.forEach($scope.newGroupTabulation.StudentIdColl, function (s) {
				if (s > 0) {
					stIdColl.push(s);
				}
			});

			if ($scope.newGroupTabulation.RptTranId > 0) {				
				var examN = mx($scope.ExamTypeGroupList).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.newGroupTabulation.ExamTypeGroupId);
				var rptPara = {
					entityid: EntityId,
					ClassId: $scope.newGroupTabulation.SelectedClass.ClassId,
					SectionId: $scope.newGroupTabulation.SelectedClass.SectionId,
					ExamTypeId: $scope.newGroupTabulation.CurExamTypeId,
					//ReExamTypeId: $scope.newGroupTabulation.ReExamTypeId,
					ClassName: $scope.newGroupTabulation.SelectedClass.text,
					FilterSection: ($scope.newGroupTabulation.FilterSection ? $scope.newGroupTabulation.FilterSection :true),
					ExamName: (examN ? examN.DisplayName : ''),
					ReExamName:'',
					rptTranId: $scope.newGroupTabulation.RptTranId,
					classIdColl: '',
					BatchId: ($scope.newGroupTabulation.BatchId ? $scope.newGroupTabulation.BatchId : 0),
					SemesterId: ($scope.newGroupTabulation.SemesterId ? $scope.newGroupTabulation.SemesterId : 0),
					ClassYearId: ($scope.newGroupTabulation.ClassYearId ? $scope.newGroupTabulation.ClassYearId : 0),
					FromPublished: ($scope.newGroupTabulation.FromPublished ? $scope.newGroupTabulation.FromPublished : false),
					BranchId: ($scope.newGroupTabulation.BranchId ? $scope.newGroupTabulation.BranchId : 0),
					istransaction: false,										
					ExamTypeGroupId: $scope.newGroupTabulation.ExamTypeGroupId,
					CurExamTypeId: ($scope.newGroupTabulation.CurExamTypeId ? $scope.newGroupTabulation.CurExamTypeId : 0),
					ExamTypeGroupId2: ($scope.newGroupTabulation.ExamTypeGroupId2 ? $scope.newGroupTabulation.ExamTypeGroupId2 : 0),
					StudentIdColl: stIdColl.toString()
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptGrpTabulation").src = '';
				document.getElementById("frmRptGrpTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptGrpTabulation").src = '';				
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptGrpTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.PublishExamResult = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para =
		{
			ExamTypeId: $scope.newNormalTabulation.ExamTypeId,
			ClassIdColl:''
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Report/PublishExamResult",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PublishGroupExamResult = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para =
		{
			ExamTypeGroupId: $scope.newGroupTabulation.ExamTypeGroupId,
			CurExamTypeId: ($scope.newGroupTabulation.CurExamTypeId ? $scope.newGroupTabulation.CurExamTypeId : 0),
			ClassIdColl: ''
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Report/PublishGroupExamResult",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire('Failed' + reason);
		});
	}


	$scope.PrintReNormalTabulation = function () {
		if ($scope.newReNormalTabulation.SelectedClass && $scope.newReNormalTabulation.ExamTypeId && $scope.newReNormalTabulation.RptTranId && $scope.newReNormalTabulation.SelectedClass.length > 0) {

			var EntityId = $scope.entity.ReMarkSheet;

			var tmpCS = $scope.newReNormalTabulation.SelectedClass[0];

			var tmpIdColl = [];
			var cIdColl = '';
			angular.forEach($scope.newReNormalTabulation.SelectedClass, function (cl) {

				if (mx(tmpIdColl).contains(cl.ClassId) == false)
					tmpIdColl.push(cl.ClassId);
			});

			cIdColl = tmpIdColl.toString();

			if ($scope.newReNormalTabulation.SelectedClass.length == 1)
				cIdColl = '';

			if ($scope.newReNormalTabulation.RptTranId > 0) {
				var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newReNormalTabulation.ExamTypeId);
				var rptPara = {
					rpttranid: $scope.newReNormalTabulation.RptTranId,
					istransaction: false,
					entityid: EntityId,
					//ClassId: $scope.newNormalTabulation.SelectedClass.ClassId,
					//SectionId: $scope.newNormalTabulation.SelectedClass.SectionId,
					ClassId: tmpCS.ClassId,
					SectionId: tmpCS.SectionId,
					ExamTypeId: $scope.newReNormalTabulation.ExamTypeId,
					ReExamTypeId: $scope.newReNormalTabulation.ReExamTypeId,
					classIdColl: (cIdColl == '0' ? '' : cIdColl),
					BranchId: ($scope.newReNormalTabulation.BranchId ? $scope.newReNormalTabulation.BranchId : 0),
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptReTabulation").src = '';
				document.getElementById("frmRptReTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptReTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptReTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.ClearResultDispatch = function () {
		$scope.newResultDispatch = {
			ExamTypeId: null,
			Mode: 'Save'
		};
	}

	$scope.GetClassWiseStudentForResult = function () {

		$scope.newResultDispatch.StudentList = [];
		if ($scope.newResultDispatch.SelectedClass && $scope.newResultDispatch.ExamTypeId) {
			var para = {
				BatchId: $scope.newResultDispatch.BatchId,
				ClassId: $scope.newResultDispatch.SelectedClass.ClassId,
				SectionId: $scope.newResultDispatch.SelectedClass.SectionId,
				SemesterId: $scope.newResultDispatch.SemesterId,
				ClassYearId: $scope.newResultDispatch.ClassYearId,
				ExamTypeId: $scope.newResultDispatch.ExamTypeId,
				BranchId: ($scope.newResultDispatch.BranchId ? $scope.newResultDispatch.BranchId : 0),
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetResultDispatch",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newResultDispatch.StudentList = res1.data.Data;

					angular.forEach($scope.newResultDispatch.StudentList, function (st) {
						if (st.DispatchDate)
							st.DispatchDate_TMP = new Date(st.DispatchDate);
					});

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};
	$scope.CheckUnCheckAllDispatch = function (chkVal) {
		angular.forEach($scope.newResultDispatch.StudentList, function (st) {
			st.IsDispatch = chkVal;
		});
	}
	$scope.ChangeAllDispatchDate = function () {

		if ($scope.newResultDispatch.DispatchDateDet) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			angular.forEach($scope.newResultDispatch.StudentList, function (st) {
				$timeout(function () {
					if (!st.DispatchDate_TMP)
						st.DispatchDate_TMP = new Date($scope.newResultDispatch.DispatchDateDet.dateAD);
				});
			});

			$scope.loadingstatus = "stop";
			hidePleaseWait();

		}

	}
	$scope.ChangeDispatchRemarks = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newResultDispatch.StudentList, function (st) {
			st.Remarks = $scope.newResultDispatch.Remarks;
		});

		$scope.loadingstatus = "stop";
		hidePleaseWait();


	}
	$scope.SaveUpdateResultDispatch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpData = [];

		angular.forEach($scope.newResultDispatch.StudentList, function (st) {

			var newData = {
				IsDispatch: st.IsDispatch,
				StudentId: st.StudentId,
				ExamTypeId: st.ExamTypeId,
				DispatchDate: st.DispatchDateDet ? $filter('date')(new Date(st.DispatchDateDet.dateAD), 'yyyy-MM-dd') : null,
				Remarks: st.Remarks,
				AcademicYearId: st.AcademicYearId
			}
			tmpData.push(newData);
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveResultDispatch",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearResultDispatch();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetExamWiseTemplate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newNormalTabulation.ReportTemplateId = 0;

		var para = {
			ExamTypeId: $scope.newNormalTabulation.ExamTypeId,
			ExamTypeGroupId: null
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeWiseTemplate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var tempColl = mx(res.data.Data);

				if ($scope.newNormalTabulation.SelectedClass && $scope.newNormalTabulation.SelectedClass.length > 0) {
					var findTemp = tempColl.firstOrDefault(p1 => p1.ClassId == $scope.newNormalTabulation.SelectedClass[0].ClassId);
					if (findTemp && findTemp.ReportTemplateId > 0) {
						$scope.newNormalTabulation.ReportTemplateId = findTemp.ReportTemplateId;											 
                    }
                }

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamGroupWiseTemplate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newGroupTabulation.ReportTemplateId = 0;

		var para = {
			ExamTypeId: null,
			ExamTypeGroupId: $scope.newGroupTabulation.ExamTypeGroupId
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeWiseTemplate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var tempColl = mx(res.data.Data);

				if ($scope.newGroupTabulation.SelectedClass) {
					var findTemp = tempColl.firstOrDefault(p1 => p1.ClassId == $scope.newGroupTabulation.SelectedClass.ClassId);
					if (findTemp && findTemp.ReportTemplateId>0) {
						$scope.newGroupTabulation.ReportTemplateId = findTemp.ReportTemplateId;
					}
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GMarkSheet_Coll = [];
	$scope.iframeSrc = "";
	$scope.ViewGMarkSheet = function (templateModel, callback) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GMarkSheet_Coll = [];
		if (!$scope.newNormalTabulation.ExamTypeId) {
			Swal.fire("Please ! Select ExamType");
			hidePleaseWait();
			return;
		}
		if (!$scope.newNormalTabulation.SelectedClass || $scope.newNormalTabulation.SelectedClass.length === 0) {
			Swal.fire("Please ! Select at least one Class");
			hidePleaseWait();
			return;
		}

		var pc = {};
		pc['ClassIdColl'] = $scope.newNormalTabulation.SelectedClass.map(function (cl) { return cl.ClassId; }).toString();
		pc['ExamTypeId'] = $scope.newNormalTabulation.ExamTypeId;

		var para = {
			procName: "PrintMarkSheet",
			qry: '',
			asParentChild: true,
			tblNames: 'StudentColl,MarkColl',
			colRelations: 'StudentId,StudentId',
			paraColl: pc,
		};

		$http({
			method: 'POST',
			url: base_url + "Global/GetCustomData",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess == true) {
				$scope.GMarkSheet_Coll = res.data.Data.StudentColl;
				var templateUrl = base_url + "TemplateGlobal/LoadTemplateWithFolder?folder=" + $scope.newNormalTabulation.HTMLTemplate.Folder +"&name=" + $scope.newNormalTabulation.HTMLTemplate.text;

				$('#modalToPrint').modal('show');
				$scope.iframeSrc = templateUrl;

				if (callback) {
					callback($scope.GMarkSheet_Coll, templateUrl);
				}
			} else {
				var exam = $scope.ExamTypeList.find(function (x) {
					return x.ExamTypeId == $scope.newNormalTabulation.ExamTypeId;
				});
				var examName = exam ? exam.DisplayName : 'Selected Exam';
				Swal.fire("Please! Publish Marksheet For " + examName + ".");
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
			hidePleaseWait();
		});
	};
	$scope.printIframe = function () {
		var iframe = document.getElementById("marksheetIframe");
		if (iframe && iframe.contentWindow) {
			iframe.contentWindow.focus();
			iframe.contentWindow.print();
		} else {
			Swal.fire("Error!", "No marksheet loaded to print", "error");
		}
	};



});