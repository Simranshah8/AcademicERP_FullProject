
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

$(document).ready(function () {

	$(document).on('keyup', '.serial', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = $('#chkColumnFocus').prop('checked');
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
					var $input = $td.find('.serial');
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

app.controller('MarksEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarksEntry';
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.LoadData = function () {

		//Get class and Section List
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

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.SubjectME = {
			ColumnWiseFocus: true

		};
		$scope.currentPages = {
			ExamSchedule: 1,
		};

		$scope.searchData = {
			ExamSchedule: '',
			Subjectwise:''
		};

		$scope.perPage = {
			ExamSchedule: GlobalServices.getPerPageRow(),
		};
		$scope.newDet = {
			CRPR: 0,
			CRTH: 0,
			FMPR: 0,
			FMTH: 0
		};

	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.SelectClass = function (V) {
		var Dat = JSON.parse(V)
		$scope.SubjectME.ClassId = Dat.ClassId
		$scope.SubjectME.SectionId = Dat.SectionId
		$scope.GetSubjectList();
	}
	$scope.SelectExamTypeId = function () {
		$scope.SubjectME.ClassId
		$scope.SubjectME.SectionId
		$scope.SubjectME.SubjectId
		$scope.SubjectME.ExamTypeId
		$scope.GetStudentForSubjectME();

	}
	function isNumeric(n) {
		return !isNaN(parseFloat(n)) && isFinite(n);
	}
	$scope.ChangeMarkEntry = function (beData, type) {
		//type  : 1=th  2=pr 3=gth 4=gpr


		if (type == 1) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkTH)) {
					beData.IsAbsentTH = false;
					if (beData.ObtainMarkTH > beData.FMTH) {
						alert("Please Enter Obtain Mark less then equal " + beData.FMTH);
						beData.ObtainMarkTH = 0;
					}
				} else {
					if (beData.ObtainMarkTH == "a" || beData.ObtainMarkTH == "ab" || beData.ObtainMarkTH == "A" || beData.ObtainMarkTH == "AB" || beData.ObtainMarkTH == "Ab") {
						beData.IsAbsentTH = true;

					} else if (len(beData.ObtainMarkTH) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkTH = 0;
					}

				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkTH;
			}
		} else if (type == 2) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkPR)) {
					beData.IsAbsentPR = false;
					if (beData.ObtainMarkPR > beData.FMPR) {
						alert("Please Enter Obtain Mark less then equal " + beData.FMPR);
						beData.ObtainMarkPR = 0;
					}
				} else {
					if (beData.ObtainMarkPR == "a" || beData.ObtainMarkPR == "ab" || beData.ObtainMarkPR == "A" || beData.ObtainMarkPR == "AB" || beData.ObtainMarkPR == "Ab") {
						beData.IsAbsentPR = true;

					} else if (len(beData.ObtainMarkPR) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkPR = 0;
					}
				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkPR;
			}
		}
		else if (type == 3) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.GraceMarkTH)) {
					var val = parseFloat(beData.ObtainMarkTH) + parseFloat(beData.GraceMarkTH);
					if (val > beData.FMTH) {
						alert("Please Enter Grace Mark less then equal " + beData.FMTH);
						beData.GraceMarkTH = 0;
					}
				} else if (len(beData.GraceMarkTH) > 0) {
					alert('Invalid Grace mark');
					beData.GraceMarkTH = 0;
				}

			}
		} else if (type == 4) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.GraceMarkPR)) {
					var val = parseFloat(beData.ObtainMarkPR) + parseFloat(beData.GraceMarkPR);
					if (val > beData.FMTH) {
						alert("Please Enter Grace Mark less then equal " + beData.FPR);
						beData.GraceMarkPR = 0;
					}
				} else if (len(beData.GraceMarkPR) > 0) {
					alert('Invalid Grace mark');
					beData.GraceMarkPR = 0;
				}
			}
		}
	};
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if ($scope.SubjectME.SelectClassSection) {
			var para = {
				classId: $scope.SubjectME.SelectClassSection.ClassId,
				sectionId: $scope.SubjectME.SelectClassSection.SectionId,
				sectionIdColl: ($scope.SubjectME.SelectClassSection.SectionId > 0 ? $scope.SubjectME.SelectClassSection.SectionId.toString() : '')
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

					$timeout(function () {
						$scope.GetStudentForSubjectME();
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}

	$scope.GetStudentForSubjectME = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectME.StudentColl = [];
		$scope.allStudentsExcluded = false;
		if ($scope.SubjectME.SelectClassSection && $scope.SubjectME.SubjectId && $scope.SubjectME.ExamTypeId) {
			var para = {
				classId: $scope.SubjectME.SelectClassSection.ClassId,
				sectionId: $scope.SubjectME.SelectClassSection.SectionId,
				subjectId: $scope.SubjectME.SubjectId,
				examTypeId: $scope.SubjectME.ExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetStudentForSubjectME",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectME.StudentColl = res.data;
					if (Array.isArray($scope.SubjectME.StudentColl) && $scope.SubjectME.StudentColl.length > 0) {

						$scope.allStudentsExcluded = $scope.SubjectME.StudentColl.every(function (st) {
							return !st.IsIncluded;
						});

						$scope.newDet.PMPR = $scope.SubjectME.StudentColl[0].PMPR;
						$scope.newDet.PMTH = $scope.SubjectME.StudentColl[0].PMTH;
						$scope.newDet.FMPR = $scope.SubjectME.StudentColl[0].FMPR;
						$scope.newDet.FMTH = $scope.SubjectME.StudentColl[0].FMTH;
					}


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.SaveSubjectME = function () {
		var meColl = [];
		var examTypeId = $scope.SubjectME.ExamTypeId;
		var subjectId = $scope.SubjectME.SubjectId;
		var classId = $scope.SubjectME.SelectClassSection.ClassId;
		var sectionId = $scope.SubjectME.SelectClassSection.SectionId;

		if (sectionId == 0)
			sectionId = null;

		angular.forEach($scope.SubjectME.StudentColl, function (st) {

			var mar = {
				StudentId: st.StudentId,
				PaperType: st.PaperType,
				ClassId: classId,
				SectionId: sectionId,
				SubjectId: subjectId,
				ExamTypeId: examTypeId,
				ObtainMarkTH: st.ObtainMarkTH,
				ObtainMarkPR: st.ObtainMarkPR,
				GraceMarkTH: (st.GraceMarkTH ? st.GraceMarkTH : 0),
				GraceMarkPR: (st.GraceMarkPR ? st.GraceMarkPR : 0),
				SubjectRemarks: (st.SubjectRemarks ? st.SubjectRemarks : ''),
			};

			meColl.push(mar);
		});

		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveSubjectME",
			dataType: "json",
			data: JSON.stringify(meColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess)
				$scope.ClearSubjectME();

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.ClearSubjectME = function () {
		$scope.SubjectME = null;
	}
});

app.controller('ExamCommentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarksEntry';
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.LoadData = function () {

		//Get class and Section List
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

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.SubjectME = {
			ColumnWiseFocus: true

		};
		$scope.currentPages = {
			ExamSchedule: 1,
		};

		$scope.searchData = {
			ExamSchedule: '',
		};

		$scope.perPage = {
			ExamSchedule: GlobalServices.getPerPageRow(),
		};


	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.SelectClass = function (V) {
		var Dat = JSON.parse(V)
		$scope.SubjectME.ClassId = Dat.ClassId
		$scope.SubjectME.SectionId = Dat.SectionId
		$scope.GetSubjectList();
	}
	$scope.SelectExamTypeId = function () {
		$scope.SubjectME.ClassId
		$scope.SubjectME.SectionId
		$scope.SubjectME.SubjectId
		$scope.SubjectME.ExamTypeId
		$scope.GetStudentForSubjectME();

	}
	function isNumeric(n) {
		return !isNaN(parseFloat(n)) && isFinite(n);
	}
	$scope.ChangeMarkEntry = function (beData, type) {
		//type  : 1=th  2=pr 3=gth 4=gpr


		if (type == 1) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkTH)) {
					beData.IsAbsentTH = false;
					if (beData.ObtainMarkTH > beData.FMTH) {
						alert("Please Enter Obtain Mark less then equal " + beData.FMTH);
						beData.ObtainMarkTH = 0;
					}
				} else {
					if (beData.ObtainMarkTH == "a" || beData.ObtainMarkTH == "ab" || beData.ObtainMarkTH == "A" || beData.ObtainMarkTH == "AB" || beData.ObtainMarkTH == "Ab") {
						beData.IsAbsentTH = true;

					} else if (len(beData.ObtainMarkTH) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkTH = 0;
					}

				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkTH;
			}
		} else if (type == 2) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkPR)) {
					beData.IsAbsentPR = false;
					if (beData.ObtainMarkPR > beData.FMPR) {
						alert("Please Enter Obtain Mark less then equal " + beData.FMPR);
						beData.ObtainMarkPR = 0;
					}
				} else {
					if (beData.ObtainMarkPR == "a" || beData.ObtainMarkPR == "ab" || beData.ObtainMarkPR == "A" || beData.ObtainMarkPR == "AB" || beData.ObtainMarkPR == "Ab") {
						beData.IsAbsentPR = true;

					} else if (len(beData.ObtainMarkPR) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkPR = 0;
					}
				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkPR;
			}
		}
		else if (type == 3) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.GraceMarkTH)) {
					var val = parseFloat(beData.ObtainMarkTH) + parseFloat(beData.GraceMarkTH);
					if (val > beData.FMTH) {
						alert("Please Enter Grace Mark less then equal " + beData.FMTH);
						beData.GraceMarkTH = 0;
					}
				} else if (len(beData.GraceMarkTH) > 0) {
					alert('Invalid Grace mark');
					beData.GraceMarkTH = 0;
				}

			}
		} else if (type == 4) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.GraceMarkPR)) {
					var val = parseFloat(beData.ObtainMarkPR) + parseFloat(beData.GraceMarkPR);
					if (val > beData.FMTH) {
						alert("Please Enter Grace Mark less then equal " + beData.FPR);
						beData.GraceMarkPR = 0;
					}
				} else if (len(beData.GraceMarkPR) > 0) {
					alert('Invalid Grace mark');
					beData.GraceMarkPR = 0;
				}
			}
		}
	};
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if ($scope.SubjectME.SelectClassSection) {
			var para = {
				classId: $scope.SubjectME.SelectClassSection.ClassId,
				sectionId: $scope.SubjectME.SelectClassSection.SectionId,
				sectionIdColl: ($scope.SubjectME.SelectClassSection.SectionId > 0 ? $scope.SubjectME.SelectClassSection.SectionId.toString() : '')
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

					$timeout(function () {
						$scope.GetStudentForSubjectME();
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}

	$scope.GetStudentForSubjectME = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectME.StudentColl = [];
		if ($scope.SubjectME.SelectClassSection && $scope.SubjectME.ExamTypeId) {
			var para = {
				classId: $scope.SubjectME.SelectClassSection.ClassId,
				sectionId: $scope.SubjectME.SelectClassSection.SectionId,
				subjectId: $scope.SubjectME.SubjectId,
				examTypeId: $scope.SubjectME.ExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetStudentForExamComment",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectME.StudentColl = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}
	$scope.SaveSubjectME = function () {
		var meColl = [];

		angular.forEach($scope.SubjectME.StudentColl, function (st) {
			meColl.push(st);
		});

		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveExamComment",
			dataType: "json",
			data: JSON.stringify(meColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess)
				$scope.ClearSubjectME();

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.ClearSubjectME = function () {
		$scope.SubjectME = null;
	}
});