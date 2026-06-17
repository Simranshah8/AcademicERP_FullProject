
app.controller('StudentListforExamController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student List for Exam';
	var glbS = GlobalServices;

	//Ui grid table added by suresh on bhadra 25 starts
	getterAndSetter();

	function getterAndSetter() {
		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "SymbolNo", displayName: "Symbol No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoardName", displayName: "BoardName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalSubject", displayName: "TotalSubject", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetails", displayName: "SubjectDetails", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectCodeDetails", displayName: "SubjectCodeDetails", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetailsWExamDate", displayName: "SubjectDetailsWExamDate", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetailsWExamDateTime", displayName: "SubjectDetailsWExamDateTime", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "Room", displayName: "Room", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RowName", displayName: "RowName", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BenchOrdinalNo", displayName: "Bench No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "SeatCol", displayName: "SeatCol", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ExamShiftName", displayName: "ExamShift", minWidth: 100, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
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
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

		$scope.gridOptions1 = [];

		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			columnDefs: [
				{ name: "SymbolNo", displayName: "Symbol No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoardName", displayName: "BoardName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalSubject", displayName: "TotalSubject", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetails", displayName: "SubjectDetails", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectCodeDetails", displayName: "SubjectCodeDetails", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetailsWExamDate", displayName: "SubjectDetailsWExamDate", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "SubjectDetailsWExamDateTime", displayName: "SubjectDetailsWExamDateTime", minWidth: 240, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
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
			exporterExcelFilename: 'enqSummary1.xlsx',
			exporterExcelSheetName: 'enqSummary1',
			onRegisterApi: function (gridApi) {
				$scope.gridApi1 = gridApi;
			}
		};
	};
	

	$scope.LoadData = function () {
		$scope.entity = {
			AdmitCard: 358
		};

		$('.select2').select2();

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


		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		$scope.StudentSearchOptions = glbS.getStudentSearchOptions();

		$scope.ClassSection = [];

		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SectionList = [];
		glbS.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		$scope.AllSubjectList = [];
		glbS.getSubjectList().then(function (res) {
			$scope.AllSubjectList = res.data.Data;
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.ReExamTypeList = [];
		glbS.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.LoadReportTemplates();		
	}

	$scope.LoadReportTemplates = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.AdmitCard + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newPrintAdmitCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityAllClassExamSchedule + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.printExamSchedule.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	

	$scope.LoadClassWiseSemesterYear1 = function (classId, data) {
		$scope.SelectedClassClassYearList1 = [];
		$scope.SelectedClassSemesterList1 = [];
		$scope.SelectedClass1 = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass1) {
			var semQry = mx($scope.SelectedClass1.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass1.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList1.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList1.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}
	};

	

	$scope.GetStudentListForExam = function () {
		if (($scope.newStudentList.SelectedClass || $scope.newStudentList.SelectedClass == 0) && $scope.newStudentList.ExamTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: ($scope.newStudentList.SelectedClass == 0 ? 0 : $scope.newStudentList.SelectedClass.ClassId),
				SectionId: $scope.newStudentList.SelectedClass == 0 ? 0 : $scope.newStudentList.SelectedClass.SectionId,
				ExamTypeId: $scope.newStudentList.ExamTypeId,
				SubjectId: $scope.newStudentList.SubjectId,
				FilterSection: $scope.newStudentList.SelectedClass == 0 ? false : $scope.newStudentList.SelectedClass.FilterSection
			}
			$http({
				method: 'POST',
				url: base_url + "Exam/Report/GetStudentListForExam",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.gridOptions.data = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.PrintStudentListForExam = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentForExam + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var templatesName = [];
					var sno = 1;
					angular.forEach(templatesColl, function (tc) {
						templatesName.push(sno + '-' + tc.ReportName);
						sno++;
					});

					var print = false;

					var rptTranId = 0;
					if (templatesColl.length == 1)
						rptTranId = templatesColl[0].RptTranId;
					else {
						Swal.fire({
							title: 'Report Templates For Print',
							input: 'select',
							inputOptions: templatesName,
							inputPlaceholder: 'Select a template',
							showCancelButton: true,
							inputValidator: (value) => {
								return new Promise((resolve) => {
									if (value >= 0) {
										resolve()
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {
											var dataColl = [];
											angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
												dataColl.push(fr.entity);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Exam/Report/PrintStudentListForExam",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("jsonData", angular.toJson(data.jsonData));

													return formData;
												},
												data: { jsonData: dataColl }
											}).then(function (res) {

												$scope.loadingstatus = "stop";
												hidePleaseWait();
												if (res.data.IsSuccess && res.data.Data) {


													var rptPara = {
														ClassSectionName: $scope.newStudentList.SelectedClass.text,
														ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newStudentList.ExamTypeId).firstOrDefault().Name
													};
													var paraQuery = param(rptPara);

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
											});

										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var dataColl = [];
						angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
							dataColl.push(fr.entity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Exam/Report/PrintStudentListForExam",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("jsonData", angular.toJson(data.jsonData));

								return formData;
							},
							data: { jsonData: dataColl }
						}).then(function (res) {

							$scope.loadingstatus = "stop";
							hidePleaseWait();
							if (res.data.IsSuccess && res.data.Data) {


								var rptPara = {
									ClassSectionName: $scope.newStudentList.SelectedClass.text,
									ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newStudentList.ExamTypeId).firstOrDefault().Name
								};
								var paraQuery = param(rptPara);

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetStudentListForReExam = function () {
		if (($scope.newReStudentList.SelectedClass || $scope.newReStudentList.SelectedClass == 0) && ($scope.newReStudentList.ExamTypeId || $scope.newReStudentList.ExamTypeId > 0) && ($scope.newReStudentList.ReExamTypeId || $scope.newReStudentList.ReExamTypeId > 0)) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: ($scope.newReStudentList.SelectedClass == 0 ? 0 : $scope.newReStudentList.SelectedClass.ClassId),
				SectionId: $scope.newReStudentList.SelectedClass == 0 ? 0 : $scope.newReStudentList.SelectedClass.SectionId,
				ExamTypeId: $scope.newReStudentList.ExamTypeId,
				ReExamTypeId: $scope.newReStudentList.ReExamTypeId,
				SubjectId: $scope.newReStudentList.SubjectId,
			}
			$http({
				method: 'POST',
				url: base_url + "Exam/Report/GetStudentListForReExam",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.gridOptions1.data = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.PrintStudentListForReExam = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentForExam + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var templatesName = [];
					var sno = 1;
					angular.forEach(templatesColl, function (tc) {
						templatesName.push(sno + '-' + tc.ReportName);
						sno++;
					});

					var print = false;

					var rptTranId = 0;
					if (templatesColl.length == 1)
						rptTranId = templatesColl[0].RptTranId;
					else {
						Swal.fire({
							title: 'Report Templates For Print',
							input: 'select',
							inputOptions: templatesName,
							inputPlaceholder: 'Select a template',
							showCancelButton: true,
							inputValidator: (value) => {
								return new Promise((resolve) => {
									if (value >= 0) {
										resolve()
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {
											var dataColl = [];
											angular.forEach($scope.gridApi1.core.getVisibleRows($scope.gridApi.grid), function (fr) {
												dataColl.push(fr.entity);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Exam/Report/PrintStudentListForExam",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("jsonData", angular.toJson(data.jsonData));

													return formData;
												},
												data: { jsonData: dataColl }
											}).then(function (res) {

												$scope.loadingstatus = "stop";
												hidePleaseWait();
												if (res.data.IsSuccess && res.data.Data) {


													var rptPara = {
														ClassSectionName: $scope.newReStudentList.SelectedClass.text,
														ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ExamTypeId).firstOrDefault().Name,
														ReExamName: mx($scope.ReExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ReExamTypeId).firstOrDefault().Name
													};
													var paraQuery = param(rptPara);

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
											});

										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var dataColl = [];
						angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
							dataColl.push(fr.entity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Exam/Report/PrintStudentListForExam",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("jsonData", angular.toJson(data.jsonData));

								return formData;
							},
							data: { jsonData: dataColl }
						}).then(function (res) {

							$scope.loadingstatus = "stop";
							hidePleaseWait();
							if (res.data.IsSuccess && res.data.Data) {


								var rptPara = {
									ClassSectionName: $scope.newReStudentList.SelectedClass.text,
									ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ExamTypeId).firstOrDefault().Name,
									ReExamName: mx($scope.ReExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ReExamTypeId).firstOrDefault().Name
								};
								var paraQuery = param(rptPara);

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
});