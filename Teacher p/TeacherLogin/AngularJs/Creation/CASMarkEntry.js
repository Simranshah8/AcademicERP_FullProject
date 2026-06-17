
$(document).ready(function () {

	$(document).on('keyup', '.serialCAS', function (e) {

		if (e.which == 13) {
			var checkBoxChecked = true;
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialCAS');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});


});


app.controller('CASMarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'CAS MarkEntry';
	var gSrv = GlobalServices;

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CASTypeList = [];
		gSrv.getCASTypeList().then(function (res) {
			$scope.CASTypeList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			MarksEntry: 1,
			ClasswiseSummary: 1,
			MarkLedger: 1,
			ClassWise: 1,
		};

		$scope.searchData = {
			MarksEntry: '',
			ClasswiseSummary: '',
			MarkLedger: '',
			ClassWise: '',
		};

		$scope.perPage = {
			MarksEntry: GlobalServices.getPerPageRow(),
			ClasswiseSummary: GlobalServices.getPerPageRow(),
			MarkLedger: GlobalServices.getPerPageRow(),
			ClassWise: GlobalServices.getPerPageRow()
		};

		$scope.newMarksEntry = {
			MarksEntryId: null,
			ClassSecId: null,
			SubjectId: null,
			TeacherId: null,
			TestName: '',
			TestDate_TMP: '',
			FulMarks: '',
			PassMarks: '',
			Mode: 'Save'
		};
		$scope.newClasswiseSummary = {
			ClasswiseSummaryId: null,
			ClassSecId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
		$scope.newMarkLedger = {
			MarkLedgerId: null,
			ClassSecId: null,
			SubjectId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};

		$scope.newClassWise = {
			ClassWiseId: null,

			Mode: 'Save'
		};

		
		//$scope.GetAllMarksEntryList();
		//$scope.GetAllClasswiseSummaryList();
	}

	function OnClickDefault() {
		document.getElementById('detailsection').style.display = "none";

		//document.getElementById('detailbtn').onclick = function () {
		//	document.getElementById('listsection').style.display = "none";
		//	document.getElementById('detailsection').style.display = "block";
		//}
		document.getElementById('backlist').onclick = function () {
			document.getElementById('listsection').style.display = "block";
			document.getElementById('detailsection').style.display = "none";
		}
	}

	$scope.sortClassWise = function (keyname) {
		$scope.sortKeyClassWise = keyname;   //set the sortKey to the param passed
		$scope.reverseClassWise = !$scope.reverseClassWise; //if true make it false and vice versa
	}
	$scope.sortSubjectWise = function (keyname) {
		$scope.sortKeySubjectWise = keyname;   //set the sortKey to the param passed
		$scope.reverseSubjectWise = !$scope.reverseSubjectWise; //if true make it false and vice versa
	}

	$scope.ClearMarksEntry = function () {
		$scope.newMarksEntry = {
			ClassSecId: null,
			SubjectId: null,
			TeacherId: null,
			TestName: '',
			TestDate: '',
			FulMarks: '',
			PassMarks: '',
			Mode: 'Save'
		};

		$scope.newClassWise = {
			Mode: 'Save'
		};
	}

	$scope.ClearClasswiseSummary = function () {
		$scope.newClasswiseSummary = {
			ClassSecId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
	}

	$scope.ClearMarkLedger = function () {
		$scope.newMarkLedger = {
			ClassSecId: null,
			SubjectId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
	}
	//************************* MarksEntry *********************************
	$scope.IsValidMarksEntry = function () {

		var isValid = true;
		angular.forEach($scope.newClassWise.StudentColl, function (st) {
			st.Mark = $scope.newClassWise.Mark;
			st.EmployeeId = $scope.newClassWise.EmployeeId;

			if (IsNumber(st.ObtainMark)) {
				if (st.ObtainMark > st.Mark) {
					Swal.fire("Please ! Enter Obtainmark less then equal to " + st.Mark);
					isValid = false;
				}
			}

		});

		return isValid;
	}

	$scope.ChangeMarkEntry = function (dc) {

		if (IsNumber(dc.ObtainMark)) {
			if (dc.ObtainMark > $scope.newClassWise.Mark) {
				alert("Please ! Enter Mark less than equal " + $scope.newClassWise.Mark);
				dc.ObtainMark = 0;
			}
		} else {
			if (dc.ObtainMark == "a" || dc.ObtainMark == "ab" || dc.ObtainMark == "A" || dc.ObtainMark == "AB" || dc.ObtainMark == "Ab") {
				dc.Remarks = "AB";

			} else if (len(dc.ObtainMark) > 0) {
				alert('Invalid obtain mark');
				dc.ObtainMark = 0;
			}
		}
	};

	$scope.GetClassWiseSubMap = function () {

		$scope.newClassWise.SubjectList = [];
		$scope.newClassWise.MarkSetupDetailsColl = [];
		$scope.newClassWise.StudentColl = [];
		if ($scope.newClassWise.SelectedClass) {
			var para = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: $scope.newClassWise.SelectedClass.SectionId  
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Global/GetAllSubjectList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.newClassWise.SubjectList = res.data; 
					 
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetEmpListForClassTeacher = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClassWise.EmployeeList = [];

		if ($scope.newClassWise.SelectedClass) {

			var para = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: ($scope.newClassWise.SelectedClass.SectionId ? $scope.newClassWise.SelectedClass.SectionId : null),			 
				SubjectId: $scope.newClassWise.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "Global/GetSubjectTeacher",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newClassWise.EmployeeList = res.data.Data;

					if ($scope.newClassWise.EmployeeList && $scope.newClassWise.EmployeeList.length > 0) {
						$scope.newClassWise.EmployeeId = $scope.newClassWise.EmployeeList[0].EmployeeId;
					}

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetStudentListForME = function () {
		if ($scope.newClassWise.SelectedClass && $scope.newClassWise.SubjectId > 0 && $scope.newClassWise.CASTypeId > 0 && $scope.newClassWise.ExamDateDet && $scope.newClassWise.ExamDateDet.dateAD) {

			var para1 = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: $scope.newClassWise.SelectedClass.SectionId,
				SubjectId: $scope.newClassWise.SubjectId,
				CASTypeId: $scope.newClassWise.CASTypeId,
				FilterSection: $scope.newClassWise.SelectedClass.FilterSection,
				ExamDate: $filter('date')(new Date($scope.newClassWise.ExamDateDet.dateAD), 'yyyy-MM-dd')
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetStudentForCASE",
				dataSchedule: "json",
				data: JSON.stringify(para1)
			}).then(function (res1) {
				if (res1.data) {
					var exMarkEntry = res1.data;
					$scope.newClassWise.StudentColl = exMarkEntry;

					if ($scope.newClassWise.StudentColl.length > 0) {
						$timeout(function () {

							if ($scope.newClassWise.StudentColl[0].EmployeeId && $scope.newClassWise.StudentColl[0].EmployeeId > 0)
								$scope.newClassWise.EmployeeId = $scope.newClassWise.StudentColl[0].EmployeeId;
						});

						$scope.newClassWise.Mark = $scope.newClassWise.StudentColl[0].Mark;

						angular.forEach($scope.newClassWise.StudentColl, function (st) {
							if (st.IsAbsent == true)
								st.ObtainMark = 'AB';
						});
					}

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	$scope.SaveUpdateMarksEntry = function () {
		if ($scope.IsValidMarksEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarksEntry();
					}
				});
			} else
				$scope.CallSaveUpdateMarksEntry();
		}
	};

	$scope.CallSaveUpdateMarksEntry = function () {

		var examDate = $filter('date')(new Date($scope.newClassWise.ExamDateDet.dateAD), 'yyyy-MM-dd');
		angular.forEach($scope.newClassWise.StudentColl, function (st) {
			st.Mark = $scope.newClassWise.Mark;
			st.EmployeeId = $scope.newClassWise.EmployeeId;

			st.ExamDate = examDate;

			if (st.ObtainMark != undefined) {

				var isAbsent = st.ObtainMark.toString().toLowerCase();
				if (isAbsent == 'ab') {
					st.IsAbsent = true;
					st.ObtainMark = 0;
				} else {

					st.IsAbsent = false;

					if (st.ObtainMark > st.Mark) {
						Swal.fire("Please ! Enter Obtainmark less then equal to " + st.Mark);
						return false;
					}
				}

			}

		});

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveCASExamWiseME",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newClassWise.StudentColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearMarksEntry();

			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	//************************* ClasswiseSummary *********************************


	$scope.GetAllClasswiseSummaryList = function () {

		$scope.ClasswiseSummaryList = [];

		if ($scope.newClasswiseSummary.SelectedClass && $scope.newClasswiseSummary.DateFromDet && $scope.newClasswiseSummary.DateToDet) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newClasswiseSummary.SelectedClass.ClassId,
				SectionId: $scope.newClasswiseSummary.SelectedClass.SectionId,
				FilterSection: $scope.newClasswiseSummary.SelectedClass.FilterSection,
				DateFrom: $filter('date')(new Date($scope.newClasswiseSummary.DateFromDet.dateAD), 'yyyy-MM-dd'),
				DateTo: $filter('date')(new Date($scope.newClasswiseSummary.DateToDet.dateAD), 'yyyy-MM-dd')
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetCASMarkEntrySubjectSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					var dataColl = mx(res.data);

					var query = dataColl.groupBy(t => t.SubjectName);
					angular.forEach(query, function (q) {
						var beData = {
							SubjectName: q.key,
							Mark: mx(q.elements).sum(p1 => p1.Mark),
							TotalCount: q.elements.length,
							DataColl: q.elements,
							ClassId: para.ClassId,
							SectionId: para.SectionId,
							FilterSection: para.FilterSection
						};
						$scope.ClasswiseSummaryList.push(beData);
					})
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}


	}

	$scope.CurMarkDetails = {};
	$scope.ShowMarkDetails = function (cm, ct) {

		$scope.CurMarkDetails = {};
		var para1 = {
			ClassId: cm.ClassId,
			SectionId: cm.SectionId,
			SubjectId: ct.SubjectId,
			CASTypeId: ct.CASTypeId,
			FilterSection: cm.FilterSection,
			ExamDate: $filter('date')(new Date(ct.ExamDate), 'yyyy-MM-dd')
		};
		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/GetStudentForCASE",
			dataSchedule: "json",
			data: JSON.stringify(para1)
		}).then(function (res1) {
			if (res1.data && res1.data) {
				var meDetColl = res1.data;

				if (meDetColl.length > 0) {

					$scope.CurMarkDetails.ClassName = meDetColl[0].ClassName;
					$scope.CurMarkDetails.SectionName = meDetColl[0].SectionName;
					$scope.CurMarkDetails.EmployeeName = meDetColl[0].EmployeeName;
					$scope.CurMarkDetails.ExamDate = ct.ExamDate;
					$scope.CurMarkDetails.ExamMiti = ct.ExamMiti;
					$scope.CurMarkDetails.SubjectName = cm.SubjectName;
					$scope.CurMarkDetails.Mark = ct.Mark;
					$scope.CurMarkDetails.CASTypeName = ct.CASTypeName;
					$scope.CurMarkDetails.DataColl = meDetColl;

					document.getElementById('listsection').style.display = "none";
					document.getElementById('detailsection').style.display = "block";
				}


			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	//************************* MarksLedger *********************************

	$scope.GetClassWiseSubMapML = function () {

		$scope.newMarkLedger.SubjectList = [];
		$scope.newMarkLedger.MarkSetupDetailsColl = [];
		$scope.newMarkLedger.StudentColl = [];
		if ($scope.newMarkLedger.SelectedClass) {
			var para = {
				ClassId: $scope.newMarkLedger.SelectedClass.ClassId,
				SectionId: $scope.newMarkLedger.SelectedClass.SectionId
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Global/GetAllSubjectList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.newMarkLedger.SubjectList = res.data;				 
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};



	$scope.GetMarkLedger = function () {

		$scope.MarkLedgerCASTypeColl = [];
		$scope.MarkLedgerColl = [];
		if ($scope.newMarkLedger.SelectedClass && $scope.newMarkLedger.DateFromDet && $scope.newMarkLedger.DateToDet && $scope.newMarkLedger.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newMarkLedger.SelectedClass.ClassId,
				SectionId: $scope.newMarkLedger.SelectedClass.SectionId,
				FilterSection: $scope.newMarkLedger.SelectedClass.FilterSection,
				SubjectId: $scope.newMarkLedger.SubjectId,
				DateFrom: $filter('date')(new Date($scope.newMarkLedger.DateFromDet.dateAD), 'yyyy-MM-dd'),
				DateTo: $filter('date')(new Date($scope.newMarkLedger.DateToDet.dateAD), 'yyyy-MM-dd')
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetCASMarkEntrySummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					var dataColl = mx(res.data);

					var query = dataColl.groupBy(t => t.StudentId);

					var casTypeQuery = dataColl.groupBy(t => t.CASType);

					var tmpCASTypeColl = [];
					angular.forEach(casTypeQuery, function (ct) {
						tmpCASTypeColl.push(ct.key);
						$scope.MarkLedgerCASTypeColl.push(ct.key);
					});

					var sno = 1;
					angular.forEach(query, function (q) {

						var fst = q.elements[0];
						var eQuery = mx(q.elements);
						var beData = {
							SNo: sno,
							UserId: fst.UserId,
							StudentId: fst.StudentId,
							RegdNo: fst.RegdNo,
							Name: fst.Name,
							RollNo: fst.RollNo,
							ObtainMark: eQuery.sum(p1 => p1.ObtainMark),
							Mark: eQuery.sum(p1 => p1.Mark),
							DataColl: []
						};

						angular.forEach(tmpCASTypeColl, function (ct) {

							var find = eQuery.firstOrDefault(p1 => p1.CASType == ct);

							beData.DataColl.push({
								CASTypeName: ct,
								ObtainMark: find ? find.ObtainMark : ""
							});
						});
						$scope.MarkLedgerColl.push(beData);
						sno++;
					})

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}


	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});