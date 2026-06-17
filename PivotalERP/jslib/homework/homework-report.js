app.controller('HomeworkRptController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices) {
	$scope.Title = 'Homework';

	var glbS = GlobalServices;
	OnClickDefault();
	getterAndSetter();

	$rootScope.ConfigFunction = function () {
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

	};


	function OnClickDefault() {
		document.getElementById('datewisedetail-form').style.display = "none";
		document.getElementById('classwisedetail-form').style.display = "none";
		document.getElementById('subjectwisedetail-form').style.display = "none";
		document.getElementById('teacherwisedetail-form').style.display = "none";


		document.getElementById('back-homework-list-btn').onclick = function () {

			document.getElementById('datewiselist').style.display = "block";
			document.getElementById('datewisedetail-form').style.display = "none";
		}

		document.getElementById('back-homework-classwise-btn').onclick = function () {

			document.getElementById('classwiselist').style.display = "block";
			document.getElementById('classwisedetail-form').style.display = "none";
		}

		document.getElementById('back-subjectwiselist').onclick = function () {
			document.getElementById('subjectwiselist').style.display = "block";
			document.getElementById('subjectwisedetail-form').style.display = "none";
		}

		document.getElementById('back-homework-teacherwise-btn').onclick = function () {
			document.getElementById('teacherwisedetail-form').style.display = "none";
			document.getElementById('teacherwiselist').style.display = "block";
		}

	}


	function getterAndSetter() {


		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: true,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{
					name: "S.N",
					displayName: "S.N",
					minWidth: 50,
					enableSorting: false,
					enableFiltering: false,
					headerCellClass: 'headerAligment',
					cellTemplate: `
        <div class="ui-grid-cell-contents text-center">
            {{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}
        </div>
    `
				},
				{ name: "AsignDateTime_BS", displayName: "Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Year", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "SubjectName", displayName: "Subject", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TeacherName", displayName: "Teacher Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Lession", displayName: "Lesson", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Topic", displayName: "Topic", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Description", displayName: "Description", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DeadlineDate_BS", displayName: "Deadline", minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "Submitted",
					displayName: "Submitted",
					minWidth: 140,
					headerCellClass: 'headerAligment',
					cellTemplate: `
						<div class="ui-grid-cell-contents text-center" title="Submitted/Total Student">
							{{row.entity.NoOfSubmit}} / {{row.entity.TotalStudent}}
						</div>  `
				},
				{
					name: "Checked",
					displayName: "Checked",
					minWidth: 140,
					headerCellClass: 'headerAligment',
					cellTemplate: `
						<div class="ui-grid-cell-contents text-center" title="Completed/Pending">
							{{row.entity.TotalChecked}} / {{row.entity.TotalStudent}}
						</div>  `
				},
				{
					name: 'Action',
					enableFiltering: false,
					enableSorting: false,
					minWidth: 80,
					enableColumnResizing: false,
					cellTemplate: `
							<div class="text-center" style="padding-top: 8px; padding-bottom: 8px;">
			<a class="btn btn-success btn-sm"
			   style="height: 20px; line-height: 7px; padding: 5px 10px; color: #fff;"
			   title="Select"
			   ng-click="grid.appScope.GetHomeworkById(row.entity)">
				Details
			</a>
		</div>`
				},
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'Homework.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'Homework.xlsx',
			exporterExcelSheetName: 'Homework',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};
	}
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

		$scope.ReportTypeColl = [{ id: 1, text: 'Grouping' }, { id: 2, text: 'Summary' }];
		$scope.ClassList = [];
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
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
				/*$scope.SelectedClassClassYearList = [];*/
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {

				if (res.data.IsSuccess == false)
					Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newDatewise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			ReportTypeId: 1,
			TotalHomeWork: 0
		};

		$scope.newClasswise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			ClassId: null,
			SectionId: null,
			TotalHomeWork: 0
		};

		$scope.newSubjectwise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			SubjectId: null,
			TotalHomeWork: 0
		};
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.newTeacherwise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			EmployeeId: null,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
		}

		$scope.newAnalysis = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			ClassId: null,
			SectionId: null,
			TotalHomeWork: 0
		};

	}
	$scope.OnDateWiseDateChange = function () {
		$scope.DateWiseHomeWorkList = [];
		if ($scope.newDatewise.FromDateDet && $scope.newDatewise.ToDateDet) {
			$scope.loadingstatus = "running";
			showPleaseWait();


			var para = {
				dateFrom: $filter('date')(new Date(($scope.newDatewise.FromDateDet ? $scope.newDatewise.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date(($scope.newDatewise.ToDateDet ? $scope.newDatewise.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				classId: null,
				sectionId: null,
				subjectId: null,
				employeeId: null

			};

			$http({
				method: 'POST',
				url: base_url + "Homework/Transaction/GetAllHomeWorkList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

					$scope.newDatewise.TotalHomeWork = res.data.Data.length;

					//$scope.DateWiseHomeWorkList = res.data.Data;
					var tmpColl = [];
					var hwColl = mx(res.data.Data);
					var query = hwColl.groupBy(p1 => ({ ClassName: p1.ClassName, SectionName: p1.SectionName }));
					angular.forEach(query, function (qr) {

						var fst = qr.elements[0];
						var newBeData = {
							ClassName: qr.key.ClassName,
							SectionName: qr.key.SectionName,
							TotalCount: qr.elements.length,
							DataColl: []
						};
						angular.forEach(qr.elements, function (el) {
							newBeData.DataColl.push(el);
						});
						tmpColl.push(newBeData);
					});
					$scope.DateWiseHomeWorkList = tmpColl;

				} else {
					if (res.data.IsSuccess == false)
						Swal.fire(res.data.ResponseMSG);

				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetHomeworkById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.HomeWorkDetails = {};
		var para = {
			HomeWorkId: refData.HomeWorkId
		};


		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var query = res.data.Data;
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

						document.getElementById('datewisedetail-form').style.display = "block";
						document.getElementById('datewiselist').style.display = "none";
					});

				}

			} else {

				if (res.data.IsSuccess == false)
					Swal.fire(res.data.ResponseMSG);

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetClassWiseHomeWorkList = function () {

		$scope.ClassWiseHomeWorkList = [];

		if ($scope.newClasswise.FromDateDet && $scope.newClasswise.ToDateDet && $scope.newClasswise.SelectedClass) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				dateFrom: $filter('date')(new Date(($scope.newClasswise.FromDateDet ? $scope.newClasswise.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date(($scope.newClasswise.ToDateDet ? $scope.newClasswise.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				classId: $scope.newClasswise.SelectedClass.ClassId,
				sectionId: $scope.newClasswise.SelectedClass.SectionId,
				subjectId: null,
				employeeId: null,
				BatchId: $scope.newClasswise.BatchId,
				SemesterId: $scope.newClasswise.SemesterId,
				ClassYearId: $scope.newClasswise.ClassYearId
			};

			$http({
				method: 'POST',
				url: base_url + "Homework/Transaction/GetAllHomeWorkList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

					$scope.newClasswise.TotalHomeWork = res.data.Data.length;

					//$scope.DateWiseHomeWorkList = res.data.Data;
					var tmpColl = [];
					var hwColl = mx(res.data.Data);
					var query = hwColl.groupBy(p1 => ({ ClassName: p1.ClassName, SectionName: p1.SectionName }));
					angular.forEach(query, function (qr) {

						var fst = qr.elements[0];
						var newBeData = {
							ClassName: qr.key.ClassName,
							SectionName: qr.key.SectionName,
							TotalCount: qr.elements.length,
							DataColl: []
						};
						angular.forEach(qr.elements, function (el) {
							newBeData.DataColl.push(el);
						});
						tmpColl.push(newBeData);
					});
					$scope.ClassWiseHomeWorkList = tmpColl;

				} else {

					if (res.data.IsSuccess == false)
						Swal.fire(res.data.ResponseMSG);

				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetHMByIdForClassWise = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.classWiseHWDet = {};
		var para = {
			HomeWorkId: refData.HomeWorkId
		};


		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var query = res.data.Data;
				if (query.length > 0) {

					$timeout(function () {
						$scope.classWiseHWDet.FData = query[0];
						$scope.classWiseHWDet.FData.SectionName = refData.SectionName;
						$scope.classWiseHWDet.FData.TeacherName = refData.TeacherName;
						$scope.classWiseHWDet.FData.SubjectName = refData.SubjectName;
						$scope.classWiseHWDet.FData.Lession = refData.Lession;
						$scope.classWiseHWDet.FData.Topic = refData.Topic;
						$scope.classWiseHWDet.FData.AsignDateTime_BS = refData.AsignDateTime_BS;
						$scope.classWiseHWDet.FData.DeadlineDate_BS = refData.DeadlineDate_BS;
						$scope.classWiseHWDet.FData.NoOfSubmit = refData.NoOfSubmit;
						$scope.classWiseHWDet.FData.TotalStudent = refData.TotalStudent;
						$scope.classWiseHWDet.FData.TotalChecked = refData.TotalChecked;

						$scope.classWiseHWDet.SubmitHomeWorksCOll = [];
						$scope.classWiseHWDet.PendingHomeWorksCOll = [];

						angular.forEach(query, function (q) {
							q.HomeWorkId = refData.HomeWorkId;
							if (q.SubmitStatus == "Pending") {

								q.IsCheck = false;

								$scope.classWiseHWDet.PendingHomeWorksCOll.push(q);
							}
							else {
								if (q.CheckStatus == 'Checked')
									q.IsCheck = true;
								else
									q.IsCheck = false;

								$scope.classWiseHWDet.SubmitHomeWorksCOll.push(q);
							}

						});

						document.getElementById('classwiselist').style.display = "none";
						document.getElementById('classwisedetail-form').style.display = "block";

					});

				}

			} else {

				if (res.data.IsSuccess == false)
					Swal.fire(res.data.ResponseMSG);


			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetSubjectWiseHomeWorkList = function () {

		$scope.SubjectWiseHomeWorkList = [];

		if ($scope.newSubjectwise.FromDateDet && $scope.newSubjectwise.ToDateDet && $scope.newSubjectwise.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				dateFrom: $filter('date')(new Date(($scope.newSubjectwise.FromDateDet ? $scope.newSubjectwise.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date(($scope.newSubjectwise.ToDateDet ? $scope.newSubjectwise.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				classId: null,
				sectionId: null,
				subjectId: $scope.newSubjectwise.SubjectId,
				employeeId: null
			};

			$http({
				method: 'POST',
				url: base_url + "Homework/Transaction/GetAllHomeWorkList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

					$scope.newSubjectwise.TotalHomeWork = res.data.Data.length;

					//$scope.DateWiseHomeWorkList = res.data.Data;
					var tmpColl = [];
					var hwColl = mx(res.data.Data);
					var query = hwColl.groupBy(p1 => ({ ClassName: p1.ClassName, SectionName: p1.SectionName }));
					angular.forEach(query, function (qr) {

						var fst = qr.elements[0];
						var newBeData = {
							ClassName: qr.key.ClassName,
							SectionName: qr.key.SectionName,
							TotalCount: qr.elements.length,
							DataColl: []
						};
						angular.forEach(qr.elements, function (el) {
							newBeData.DataColl.push(el);
						});
						tmpColl.push(newBeData);
					});
					$scope.SubjectWiseHomeWorkList = tmpColl;

				} else {

					if (res.data.IsSuccess == false)
						Swal.fire(res.data.ResponseMSG);


				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	$scope.GetHMByIdForSubjectWise = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.subjectWiseHWDet = {};
		var para = {
			HomeWorkId: refData.HomeWorkId
		};
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var query = res.data.Data;
				if (query.length > 0) {

					$timeout(function () {
						$scope.subjectWiseHWDet.FData = query[0];
						$scope.subjectWiseHWDet.FData.SectionName = refData.SectionName;
						$scope.subjectWiseHWDet.FData.TeacherName = refData.TeacherName;
						$scope.subjectWiseHWDet.FData.SubjectName = refData.SubjectName;
						$scope.subjectWiseHWDet.FData.Lession = refData.Lession;
						$scope.subjectWiseHWDet.FData.Topic = refData.Topic;
						$scope.subjectWiseHWDet.FData.AsignDateTime_BS = refData.AsignDateTime_BS;
						$scope.subjectWiseHWDet.FData.DeadlineDate_BS = refData.DeadlineDate_BS;
						$scope.subjectWiseHWDet.FData.NoOfSubmit = refData.NoOfSubmit;
						$scope.subjectWiseHWDet.FData.TotalStudent = refData.TotalStudent;
						$scope.subjectWiseHWDet.FData.TotalChecked = refData.TotalChecked;

						$scope.subjectWiseHWDet.SubmitHomeWorksCOll = [];
						$scope.subjectWiseHWDet.PendingHomeWorksCOll = [];

						angular.forEach(query, function (q) {
							q.HomeWorkId = refData.HomeWorkId;
							if (q.SubmitStatus == "Pending") {

								q.IsCheck = false;

								$scope.subjectWiseHWDet.PendingHomeWorksCOll.push(q);
							}
							else {
								if (q.CheckStatus == 'Checked')
									q.IsCheck = true;
								else
									q.IsCheck = false;

								$scope.subjectWiseHWDet.SubmitHomeWorksCOll.push(q);
							}

						});

						document.getElementById('subjectwiselist').style.display = "none";
						document.getElementById('subjectwisedetail-form').style.display = "block";
					});

				}

			} else {

				if (res.data.IsSuccess == false)
					Swal.fire(res.data.ResponseMSG);

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetTeacherWiseHomeWorkList = function () {

		$scope.TeacherWiseHomeWorkList = [];

		if ($scope.newTeacherwise.FromDateDet && $scope.newTeacherwise.ToDateDet && $scope.newTeacherwise.EmployeeId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				dateFrom: $filter('date')(new Date(($scope.newTeacherwise.FromDateDet ? $scope.newTeacherwise.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date(($scope.newTeacherwise.ToDateDet ? $scope.newTeacherwise.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				classId: null,
				sectionId: null,
				subjectId: null,
				employeeId: $scope.newTeacherwise.EmployeeId
			};

			$http({
				method: 'POST',
				url: base_url + "Homework/Transaction/GetAllHomeWorkList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

					$scope.newTeacherwise.TotalHomeWork = res.data.Data.length;

					//$scope.DateWiseHomeWorkList = res.data.Data;
					var tmpColl = [];
					var hwColl = mx(res.data.Data);
					var query = hwColl.groupBy(p1 => ({ ClassName: p1.ClassName, SectionName: p1.SectionName }));
					angular.forEach(query, function (qr) {

						var fst = qr.elements[0];
						var newBeData = {
							ClassName: qr.key.ClassName,
							SectionName: qr.key.SectionName,
							TotalCount: qr.elements.length,
							DataColl: []
						};
						angular.forEach(qr.elements, function (el) {
							newBeData.DataColl.push(el);
						});
						tmpColl.push(newBeData);
					});
					$scope.TeacherWiseHomeWorkList = tmpColl;

				} else {

					if (res.data.IsSuccess == false)
						Swal.fire(res.data.ResponseMSG);


				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	$scope.GetHMByIdForTeacherWise = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.teacherWiseHWDet = {};
		var para = {
			HomeWorkId: refData.HomeWorkId
		};
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var query = res.data.Data;
				if (query.length > 0) {

					$timeout(function () {
						$scope.teacherWiseHWDet.FData = query[0];
						$scope.teacherWiseHWDet.FData.SectionName = refData.SectionName;
						$scope.teacherWiseHWDet.FData.TeacherName = refData.TeacherName;
						$scope.teacherWiseHWDet.FData.SubjectName = refData.SubjectName;
						$scope.teacherWiseHWDet.FData.Lession = refData.Lession;
						$scope.teacherWiseHWDet.FData.Topic = refData.Topic;
						$scope.teacherWiseHWDet.FData.AsignDateTime_BS = refData.AsignDateTime_BS;
						$scope.teacherWiseHWDet.FData.DeadlineDate_BS = refData.DeadlineDate_BS;
						$scope.teacherWiseHWDet.FData.NoOfSubmit = refData.NoOfSubmit;
						$scope.teacherWiseHWDet.FData.TotalStudent = refData.TotalStudent;
						$scope.teacherWiseHWDet.FData.TotalChecked = refData.TotalChecked;

						$scope.teacherWiseHWDet.SubmitHomeWorksCOll = [];
						$scope.teacherWiseHWDet.PendingHomeWorksCOll = [];

						angular.forEach(query, function (q) {
							q.HomeWorkId = refData.HomeWorkId;
							if (q.SubmitStatus == "Pending") {

								q.IsCheck = false;

								$scope.teacherWiseHWDet.PendingHomeWorksCOll.push(q);
							}
							else {
								if (q.CheckStatus == 'Checked')
									q.IsCheck = true;
								else
									q.IsCheck = false;

								$scope.teacherWiseHWDet.SubmitHomeWorksCOll.push(q);
							}

						});

						document.getElementById('teacherwisedetail-form').style.display = "block";
						document.getElementById('teacherwiselist').style.display = "none";
					});

				}

			} else {

				if (res.data.IsSuccess == false)
					Swal.fire(res.data.ResponseMSG);

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


	var ctx = document.getElementById('canvas').getContext('2d');
	var myLine = null;
	$scope.GetAnalysisHomeWorkList = function () {
		if ($scope.newAnalysis.FromDateDet &&
			$scope.newAnalysis.ToDateDet &&
			$scope.newAnalysis.ToDateDet.dateAD < $scope.newAnalysis.FromDateDet.dateAD) {

			$scope.newAnalysis.ToDate_TMP = null;
			$scope.newAnalysis.ToDateDet = null;

			Swal.fire(
				'Invalid Date',
				'To Date must be greater than or equal to From Date',
				'warning'
			);
			return;
		}
		myLine = null;
		$scope.AnalysisWiseHomeWorkList = [];

		if ($scope.newAnalysis.FromDateDet && $scope.newAnalysis.ToDateDet && $scope.newAnalysis.SelectedClass) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				dateFrom: $filter('date')(new Date(($scope.newAnalysis.FromDateDet ? $scope.newAnalysis.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date(($scope.newAnalysis.ToDateDet ? $scope.newAnalysis.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
				classId: $scope.newAnalysis.SelectedClass.ClassId,
				sectionId: $scope.newAnalysis.SelectedClass.SectionId,
				subjectId: null,
				employeeId: null,
				//Added By Suresh on Falgun 1
				BatchId: $scope.newAnalysis.BatchId,
				SemesterId: $scope.newAnalysis.SemesterId,
				ClassYearId: $scope.newAnalysis.ClassYearId
			};

			$http({
				method: 'POST',
				url: base_url + "Homework/Transaction/GetAllHomeWorkList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

					$scope.newAnalysis.TotalHomeWork = res.data.Data.length;

					var tmpColl = [];
					var hwColl = mx(res.data.Data);
					var query = hwColl.groupBy(p1 => ({ SubjectName: p1.SubjectName, TeacherName: p1.TeacherName }));
					var sno = 1;
					angular.forEach(query, function (qr) {

						var fst = qr.elements[0];
						var newBeData = {
							SNo: sno,
							SubjectName: qr.key.SubjectName,
							TeacherName: qr.key.TeacherName,
							TotalCount: qr.elements.length,
							NoOfSubmit: fst.NoOfSubmit,
							TotalStudent: fst.TotalStudent,
							TotalChecked: fst.TotalChecked,
							NotSubmit: fst.TotalStudent - fst.NoOfSubmit,
							YetToCheck: fst.NoOfSubmit - fst.TotalChecked
						};
						tmpColl.push(newBeData);
						sno++;
					});
					$scope.AnalysisWiseHomeWorkList = tmpColl;
					var labelsColl = [];
					var submitColl = [];
					var totalColl = [];
					var checkedColl = [];
					var notSubmitColl = [];
					var yetToCheckColl = [];
					angular.forEach($scope.AnalysisWiseHomeWorkList, function (sb) {
						labelsColl.push(sb.SubjectName);
						submitColl.push(sb.NoOfSubmit);
						notSubmitColl.push(sb.NotSubmit);
						totalColl.push(sb.TotalStudent);
						checkedColl.push(sb.TotalChecked);
						yetToCheckColl.push(sb.YetToCheck);
					});

					var config = {
						type: 'line',
						data: {
							labels: labelsColl,
							datasets: [{
								label: 'Submit',
								backgroundColor: '#28a745',
								borderColor: '#28a745',
								fill: false,
								data: submitColl,
							}, {
								label: 'Not Submit',
								backgroundColor: '#ea3333',
								borderColor: '#ea3333',
								fill: false,
								data: notSubmitColl,

							},
							{
								label: 'Checked',
								backgroundColor: '#17a2b8',
								borderColor: '#17a2b8',
								fill: false,
								data: checkedColl,

							},
							{
								label: 'Yet to Check',
								backgroundColor: '#ffc107',
								borderColor: '#ffc107',
								fill: false,
								data: yetToCheckColl,
							}
							]
						},
						options: {
							responsive: true,
							// title: {
							// 	display: true,
							// 	text: 'Chart.js Line Chart - Logarithmic'
							// },
							scales: {
								xAxes: [{
									display: true,
									scaleLabel: {
										display: true,
										labelString: 'Subjects'
									},

								}],
								yAxes: [{
									display: true,
									//type: 'logarithmic',
									scaleLabel: {
										display: true,
										labelString: 'No of Students'
									},
									ticks: {
										min: 0,
										max: 100,

										// forces step size to be 5 units
										stepSize: 20
									}
								}]
							}
						}
					};

					if (myLine == null)
						myLine = new Chart(ctx, config);

					//myLine.update();

				} else {
					if (res.data.IsSuccess == false)
						Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.OnDateWiseDateChangeUI = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			dateFrom: $filter('date')(new Date(($scope.newDatewise.FromDateDet ? $scope.newDatewise.FromDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
			dateTo: $filter('date')(new Date(($scope.newDatewise.ToDateDet ? $scope.newDatewise.ToDateDet.dateAD : new Date())), 'yyyy-MM-dd'),
			classId: null,
			sectionId: null,
			subjectId: null,
			employeeId: null

		};
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllHomeWorkList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}




	$scope.sortdatewiseSub = function (keyname) {
		$scope.sortKeydatewiseSub = keyname;
		$scope.reversedateWiseSub = !$scope.reversedateWiseSub;
	}

	$scope.sortdatewisePen = function (keyname) {
		$scope.sortKeydatewisePen = keyname;
		$scope.reversedateWisePen = !$scope.reversedateWisePen;
	}

	$scope.sortclasswiseSub = function (keyname) {
		$scope.sortKeyclasswiseSub = keyname;
		$scope.reverseclassWiseSub = !$scope.reverseclassWiseSub;
	}

	$scope.sortclasswisePen = function (keyname) {
		$scope.sortKeyclasswisePen = keyname;
		$scope.reverseclassWisePen = !$scope.reverseclassWisePen;
	}

	$scope.sortSubjectwiseSub = function (keyname) {
		$scope.sortKeySubjectwiseSub = keyname;
		$scope.reverseSubjectWiseSub = !$scope.reverseSubjectWiseSub;
	}

	$scope.sortSubjectwisePen = function (keyname) {
		$scope.sortKeySubjectwisePen = keyname;
		$scope.reverseSubjectWisePen = !$scope.reverseSubjectWisePen;
	}

	$scope.sortTeacherwiseSub = function (keyname) {
		$scope.sortKeyTeacherwiseSub = keyname;
		$scope.reverseTeacherWiseSub = !$scope.reverseTeacherWiseSub;
	}

	$scope.sortTeacherwisePen = function (keyname) {
		$scope.sortKeyTeacherwisePen = keyname;
		$scope.reverseTeacherWisePen = !$scope.reverseTeacherWisePen;
	}


	$scope.validateDates = function (changedField, obj) {
		if (!obj.FromDateDet || !obj.ToDateDet ||
			!obj.FromDateDet.dateAD || !obj.ToDateDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.FromDateDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.ToDateDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.FromDate_TMP = new Date();
						obj.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.ToDate_TMP = new Date();
						obj.ToDateDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
});