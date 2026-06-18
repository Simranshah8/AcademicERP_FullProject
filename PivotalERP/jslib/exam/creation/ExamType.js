app.directive('select', function ($timeout) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			$timeout(function () {
				$(element).select2({
					placeholder: "**Select an Option**",
					allowClear: true
				});

				// Watch ngModel safely
				scope.$watch(attrs.ngModel, function (newValue, oldValue) {
					if (newValue !== oldValue) {
						$timeout(function () {
							$(element).trigger('change');
						});
					}
				});

				// Ensure select2 updates when value changes externally
				$(element).on('change', function () {
					$timeout(function () {
						if (!scope.$$phase) {
							scope.$apply();
						}
					});
				});

			}, 2);
		}
	};
});


app.controller('ExamTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {

	$scope.Title = 'Exam Type';

	OnClickDefault();

	//new js added by suresh on 8 kartik for action button of classwise result publish date
	$scope.isDuplicateClassSelected = function (currentIndex) {
		let selected = $scope.newExamType.ClassWiseColl_TMP[currentIndex].ClassIdColl || [];
		for (let i = 0; i < $scope.newExamType.ClassWiseColl_TMP.length; i++) {
			if (i !== currentIndex) {
				let otherSelected = $scope.newExamType.ClassWiseColl_TMP[i].ClassIdColl || [];
				// check for overlap
				if (selected.some(c => otherSelected.includes(c))) {
					return true;
				}
			}
		}
		return false;
	};

	$scope.AddClassResultDetails = function (ind) {
		if ($scope.isDuplicateClassSelected(ind)) {
			Swal.fire("Duplicate class selection is not allowed.");
			return;
		}

		if ($scope.newExamType.ClassWiseColl_TMP) {
			if ($scope.newExamType.ClassWiseColl_TMP.length > ind + 1) {
				$scope.newExamType.ClassWiseColl_TMP.splice(ind + 1, 0, {
					ClassIdColl: []
				});
			} else {
				$scope.newExamType.ClassWiseColl_TMP.push({
					ClassIdColl: []
				});
			}
		}
	};
	$scope.delClassResultDetails = function (ind) {
		if ($scope.newExamType.ClassWiseColl_TMP) {
			if ($scope.newExamType.ClassWiseColl_TMP.length > 1) {
				$scope.newExamType.ClassWiseColl_TMP.splice(ind, 1);
			}
		}
	};
	//Ends

	//new js added by suresh on 8 kartik for action button of classwise result publish date
	$scope.AddReClassResultDetails = function (ind) {
		if ($scope.newReExamType.ClassWiseColl_TMP) {
			if ($scope.newReExamType.ClassWiseColl_TMP.length > ind + 1) {
				$scope.newReExamType.ClassWiseColl_TMP.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newReExamType.ClassWiseColl_TMP.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delReClassResultDetails = function (ind) {
		if ($scope.newReExamType.ClassWiseColl_TMP) {
			if ($scope.newReExamType.ClassWiseColl_TMP.length > 1) {
				$scope.newReExamType.ClassWiseColl_TMP.splice(ind, 1);
			}
		}
	};
	//Ends

	//add and del
	$scope.AddExamTypeGroupDetails = function (ind) {
		if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl) {
			if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl.length > ind + 1) {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.splice(ind + 1, 0, {
					ExamTypeId: null,
					Percent: 0,
					DisplayName: '',
					IsCalculateAttenDance: false,
					ExamTypeWiseSubjectColl: []
				})
			} else {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({
					ExamTypeId: null,
					Percent: 0,
					DisplayName: '',
					IsCalculateAttenDance: false,
					ExamTypeWiseSubjectColl: []
				})
			}
		}
	};
	$scope.delExamTypeGroupDetails = function (ind) {
		if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl) {
			if ($scope.newExamTypeGroup.ExamTypeGroupDetailsColl.length > 1) {
				$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.splice(ind, 1);
			}
		}
	};

	//ModalTable add and del
	$scope.AddExamTypeGroupDetailsModalTable = function (ind) {
		if ($scope.ExamTypeWiseSubjectColl) {
			if ($scope.ExamTypeWiseSubjectColl.length > ind + 1) {
				$scope.ExamTypeWiseSubjectColl.splice(ind + 1, 0, {
					SubjectId: null
				})
			} else {
				$scope.ExamTypeWiseSubjectColl.splice(ind + 1, 0, {
					SubjectId: null
				})
			}
		}
	};
	$scope.delExamTypeGroupModalTableDetails = function (ind) {
		if ($scope.ExamTypeWiseSubjectColl) {
			if ($scope.ExamTypeWiseSubjectColl.length > 1) {
				$scope.ExamTypeWiseSubjectColl.splice(ind, 1);
			}
		}
	};


	$scope.AddParentExamTypeGroupDetails = function (ind) {
		if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl) {
			if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.length > ind + 1) {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.splice(ind + 1, 0, {
					SNO: 0,
					ExamTypeGroupId: null,
					Percent: 0,
					DisplayName: '',
					IsCalculateAttenDance: false
				})
			} else {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({
					SNO: 0,
					ExamTypeGroupId: null,
					Percent: 0,
					DisplayName: '',
					IsCalculateAttenDance: false
				})
			}
		}
	};
	$scope.delParentExamTypeGroupDetails = function (ind) {
		if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl) {
			if ($scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.length > 1) {
				$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.LoadData = function () {
		$('.select2').select2();

		var glbS = GlobalServices;
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

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


		$scope.newADBSConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetGeneralConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newADBSConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = {};
		glbS.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Company Details
		$scope.CompanyConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CompanyConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			ExamType: 1,
			ReExamType: 1,
			ExamTypeGroup: 1,
			ExamTypeGroupModalTable: 1,
			ParentExamTypeGroup: 1

		};

		$scope.searchData = {
			ExamType: '',
			ReExamType: '',
			ExamTypeGroup: '',
			ExamTypeGroupModalTable: '',
			ParentExamTypeGroup: ''

		};

		$scope.perPage = {
			ExamType: glbS.getPerPageRow(),
			ReExamType: glbS.getPerPageRow(),
			ExamTypeGroup: glbS.getPerPageRow(),
			ExamTypeGroupModalTable: glbS.getPerPageRow(),
			ParentExamTypeGroup: glbS.getPerPageRow(),

		};

		$scope.newExamType = {
			ExamTypeId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			IsOnlineExam: false,
			ExamDate_TMP: null,
			Duration: 0,
			StartTime_TMP: null,
			SectionWiseExam: false,
			MarkSubmitDeadline_Teacher: null,
			MarkSubmitDeadline_Admin: null,
			MarkSubmitDeadline_Teacher_TMP: new Date(),
			MarkSubmitDeadline_Admin_TMP: new Date(),
			ResultDate_TMP: new Date(),
			ForClassWiseResultPublished: false,
			ClassWiseColl_TMP: [],
			ForPreStudent: false,
			IsActive: true,
			DisplayYearAD: '',
			DisplayYearBS:'',
			Mode: 'Save'
		};
		$scope.newExamType.ClassWiseColl_TMP.push({});


		$scope.newReExamType = {
			ReExamTypeId: null,
			ExamTypeId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			IsOnlineExam: false,
			ExamDate_TMP: null,
			Duration: 0,
			StartTime_TMP: null,
			SectionWiseExam: false,
			MarkSubmitDeadline_Teacher: null,
			MarkSubmitDeadline_Admin: null,
			MarkSubmitDeadline_Teacher_TMP: new Date(),
			MarkSubmitDeadline_Admin_TMP: new Date(),
			ResultDate_TMP: new Date(),
			ForClassWiseResultPublished: false,
			ClassWiseColl_TMP: [],
			DisplayYearAD: '',
			DisplayYearBS: '',
			Mode: 'Save',
			IsActive: true
		};
		$scope.newReExamType.ClassWiseColl_TMP.push({});

		$scope.newExamTypeGroup = {
			ExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ExamTypeGroupDetailsColl: [],
			ExamTypeWiseSubjectColl: [],
			IsActive: true,
			DisplayYearAD: '',
			DisplayYearBS: '',
			ResultDate_TMP:new Date(),
			Mode: 'Save'
		};
		$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({});

		//ModalTable
		$scope.newExamTypeGroupModalTable = {
			ExamTypeGroupModalTableId: null,
			SubjectName: '',
			THPer: '',
			PRPer: '',
			ExamTypeGroupModalTableDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.push({});


		$scope.newParentExamTypeGroup = {
			ParentExamTypeGroupId: null,
			Name: '',
			DisplayName: '',
			ResultDate: '',
			ResultTime: '',
			OrderNo: 0,
			ParentExamTypeGroupDetailsColl: [],
			DisplayYearAD: '',
			DisplayYearBS: '',
			ResultDate_TMP: new Date(),
			Mode: 'Save'
		};
		$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({});


		$scope.GetAllExamTypeList();
		$scope.GetAllReExamTypeList();
		$scope.GetAllExamTypeGroupList();
		$scope.GetAllParentExamTypeGroupList();


	}

	$scope.GetAutoOrderNo = function (be) {
		var para = {
			For: be
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassSetupAutoOrderNo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var vDet = res.data.Data;
				if (be == 11) { //Exam Type
					$scope.newExamType.OrderNo = vDet.RId;
				}
				if (be == 12) {  //Exam Type Group
					$scope.newExamTypeGroup.OrderNo = vDet.RId;
				}
				if (be == 13) {	//Re-Exam Type
					$scope.newReExamType.OrderNo = vDet.RId;
				}
				if (be == 14) { //Parent Exam Type Group
					$scope.newParentExamTypeGroup.OrderNo = vDet.RId;
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	function OnClickDefault() {


		document.getElementById('exam-type-form').style.display = "none";
		document.getElementById('reexam-type-form').style.display = "none";
		document.getElementById('exam-type-group-form').style.display = "none";
		document.getElementById('parent-exam-type-form').style.display = "none";


		// exam-type section

		document.getElementById('add-exam-type').onclick = function () {
			document.getElementById('exam-type-section').style.display = "none";
			document.getElementById('exam-type-form').style.display = "block";
			$scope.ClearExamType();
			$scope.GetAutoOrderNo(11);
		}

		document.getElementById('back-exam-type').onclick = function () {
			document.getElementById('exam-type-section').style.display = "block";
			document.getElementById('exam-type-form').style.display = "none";
			$scope.ClearExamType();
		}

		// reexam-type section

		document.getElementById('add-reexam-type').onclick = function () {
			document.getElementById('reexam-type-section').style.display = "none";
			document.getElementById('reexam-type-form').style.display = "block";
			$scope.ClearExamType();
			$scope.GetAutoOrderNo(13);
		}

		document.getElementById('back-reexam-type').onclick = function () {
			document.getElementById('reexam-type-section').style.display = "block";
			document.getElementById('reexam-type-form').style.display = "none";
			$scope.ClearExamType();
		}


		// exam type group section

		document.getElementById('add-exam-type-group').onclick = function () {
			document.getElementById('exam-type-group-section').style.display = "none";
			document.getElementById('exam-type-group-form').style.display = "block";
			$scope.ClearExamTypeGroup();
			$scope.GetAutoOrderNo(12);
		}

		document.getElementById('back-exam-type-group').onclick = function () {
			document.getElementById('exam-type-group-section').style.display = "block";
			document.getElementById('exam-type-group-form').style.display = "none";
			$scope.ClearExamTypeGroup();
		}


		//parent exam type group section


		document.getElementById('add-parent-exam-type').onclick = function () {
			document.getElementById('parent-exam-type-section').style.display = "none";
			document.getElementById('parent-exam-type-form').style.display = "block";
			$scope.ClearParentExamTypeGroup();
			$scope.GetAutoOrderNo(14);
		}

		document.getElementById('back-parent-exam-type').onclick = function () {
			document.getElementById('parent-exam-type-section').style.display = "block";
			document.getElementById('parent-exam-type-form').style.display = "none";
			$scope.ClearParentExamTypeGroup();
		}


	}

	$scope.ClearExamType = function () {
		$timeout(function () {
			$scope.newExamType = {
				ExamTypeId: null,
				Name: '',
				DisplayName: '',
				ResultDate: '',
				ResultTime: '',
				OrderNo: 0,
				IsOnlineExam: false,
				ExamDate_TMP: null,
				Duration: 0,
				StartTime_TMP: null,
				SectionWiseExam: false,
				MarkSubmitDeadline_Teacher: null,
				MarkSubmitDeadline_Admin: null,
				MarkSubmitDeadline_Teacher_TMP: new Date(),
				MarkSubmitDeadline_Admin_TMP: new Date(),
				ResultDate_TMP: new Date(),
				ForClassWiseResultPublished: false,
				ClassWiseColl_TMP: [],
				ForPreStudent: false,
				IsActive: true,
				DisplayYearAD: '',
				DisplayYearBS: '',
				Mode: 'Save'
			};
			$scope.newExamType.ClassWiseColl_TMP.push({});
		});

	}
	$scope.ClearReExamType = function () {
		$timeout(function () {
			$scope.newReExamType = {
				ReExamTypeId: null,
				ExamTypeId: null,
				Name: '',
				DisplayName: '',
				ResultDate: '',
				ResultTime: '',
				OrderNo: 0,
				IsOnlineExam: false,
				ExamDate_TMP: null,
				Duration: 0,
				StartTime_TMP: null,
				SectionWiseExam: false,
				MarkSubmitDeadline_Teacher: null,
				MarkSubmitDeadline_Admin: null,
				MarkSubmitDeadline_Teacher_TMP: new Date(),
				MarkSubmitDeadline_Admin_TMP: new Date(),
				ResultDate_TMP: new Date(),
				ForClassWiseResultPublished: false,
				ClassWiseColl_TMP: [],
				DisplayYearAD: '',
				DisplayYearBS: '',
				Mode: 'Save',
				IsActive: true
			};
			$scope.newReExamType.ClassWiseColl_TMP.push({});
		});

	}
	$scope.ClearExamTypeGroup = function () {
		$timeout(function () {
			$scope.newExamTypeGroup = {
				ExamTypeGroupId: null,
				Name: '',
				DisplayName: '',
				ResultDate: '',
				ResultTime: '',
				OrderNo: 0,
				ExamTypeGroupDetailsColl: [],
				ExamTypeWiseSubjectColl: [],
				IsActive: true,
				DisplayYearAD: '',
				DisplayYearBS: '',
				ResultDate_TMP: new Date(),
				Mode: 'Save'
			};
			$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({});
		});

	}

	$scope.ClearExamTypeGroupModalTable = function () {
		$timeout(function () {
			$scope.newExamTypeGroupModalTable = {
				ExamTypeGroupModalTableId: null,
				SubjectName: '',
				THPer: '',
				PRPer: '',
				ExamTypeGroupModalTableDetailsColl: [],
				Mode: 'Save'
			};
			$scope.newExamTypeGroupModalTable.ExamTypeGroupModalTableDetailsColl.push({});

		});

	}

	$scope.ClearParentExamTypeGroup = function () {
		$timeout(function () {
			$scope.newParentExamTypeGroup = {
				ParentExamTypeGroupId: null,
				Name: '',
				DisplayName: '',
				ResultDate: '',
				ResultTime: '',
				OrderNo: 0,
				ParentExamTypeGroupDetailsColl: [],
				DisplayYearAD: '',
				DisplayYearBS: '',
				ResultDate_TMP:new Date(),
				Mode: 'Save'
			};
			$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({});
		});



	}


	$scope.ResultDateFocusOn = null;
	$scope.OnResultDateFocus = function (dateStyle) {
		$scope.ResultDateFocusOn = dateStyle;
	}

	$scope.updateResultDate = function (dateStyle) {
		if (dateStyle == 1 && $scope.ResultDateFocusOn == 1) {

			if ($scope.newExamType.ResultDateADDet) {
				$scope.newExamType.ResultDateBS_TMP = new Date($scope.newExamType.ResultDateADDet.dateAD);
			}
			else {
				$scope.newStudent.ResultDateBS_TMP = null;
			}
		}
		else if (dateStyle == 2 && $scope.ResultDateFocusOn == 2) {

			if ($scope.newExamType.ResultDateBSDet) {
				$scope.newExamType.ResultDateAD_TMP = new Date($scope.newExamType.ResultDateBSDet.dateAD);
			}
			else {
				$scope.newExamType.ResultDateAD_TMP = null;
			}
		}
		//if ($scope.newStudent.ResultDate_ADDet) {
		//	var englishDate = $filter('date')(new Date($scope.newStudent.ResultDate_ADDet.dateAD), 'yyyy-MM-dd');

		//	$scope.newStudent.ResultDate_AD = englishDate;

		//	if (!$scope.$$phase) {
		//		$scope.$apply();
		//	}
		//}
	};

	//************************* ExamType *********************************

	$scope.IsValidExamType = function () {
		if (!$scope.newExamType.Name || $scope.newExamType.Name.trim() === "") {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		if ($scope.newExamType.ForClassWiseResultPublished === true) {
			let isInvalid = false;

			// 1. MarkSubmit Deadline validation
			angular.forEach($scope.newExamType.ClassWiseColl_TMP, function (gDet, index) {
				if (!gDet.MarkSubmitDeadline_Teacher_TMP || gDet.MarkSubmitDeadline_Teacher_TMP === '') {
					Swal.fire(`Please enter Mark Submit Deadline (Date) at row ${index + 1}`);
					isInvalid = true;
					return false; // break loop
				}
			});

			if (isInvalid) return false;

			// 2. Duplicate class validation
			let selectedClasses = [];
			for (let i = 0; i < $scope.newExamType.ClassWiseColl_TMP.length; i++) {
				let rowClasses = $scope.newExamType.ClassWiseColl_TMP[i].ClassIdColl || [];
				for (let j = 0; j < rowClasses.length; j++) {
					if (selectedClasses.includes(rowClasses[j])) {
						Swal.fire(`Duplicate class selection found at row ${i + 1}`);
						return false;
					}
					selectedClasses.push(rowClasses[j]);
				}
			}
		}

		return true;
	};


	$scope.SaveUpdateExamType = function () {
		if ($scope.IsValidExamType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamType();
					}
				});
			} else
				$scope.CallSaveUpdateExamType();

		}
	};

	$scope.CallSaveUpdateExamType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.updateResultDate($scope.ResultDateFocusOn);
		if ($scope.newExamType.MarkSubmitDeadline_TeacherDet) {
			$scope.newExamType.MarkSubmitDeadline_Teacher = $filter('date')(new Date($scope.newExamType.MarkSubmitDeadline_TeacherDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamType.MarkSubmitDeadline_Teacher = null;

		if ($scope.newExamType.MarkSubmitDeadline_AdminDet) {
			$scope.newExamType.MarkSubmitDeadline_Admin = $filter('date')(new Date($scope.newExamType.MarkSubmitDeadline_AdminDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamType.MarkSubmitDeadline_Admin = null;

		//if ($scope.newExamType.ResultDateADDet) {
		//	$scope.newExamType.ResultDate = $filter('date')(new Date($scope.newExamType.ResultDateADDet.dateAD), 'yyyy-MM-dd');
		//} else
		//	$scope.newExamType.ResultDate = null;

		if ($scope.newExamType.ResultDateDet) {
			$scope.newExamType.ResultDate = $filter('date')(new Date($scope.newExamType.ResultDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamType.ResultDate = null;

		if ($scope.newExamType.ResultTime_TMP)
			$scope.newExamType.ResultTime = $filter('date')(new Date($scope.newExamType.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamType.ResultTime = null;

		if ($scope.newExamType.AdminTime_TMP)
			$scope.newExamType.AdminTime = $filter('date')(new Date($scope.newExamType.AdminTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamType.AdminTime = null;

		if ($scope.newExamType.TeacherTime_TMP)
			$scope.newExamType.TeacherTime = $filter('date')(new Date($scope.newExamType.TeacherTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamType.TeacherTime = null;

		if ($scope.newExamType.ExamDateDet) {
			$scope.newExamType.ExamDate = $filter('date')(new Date($scope.newExamType.ExamDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamType.ExamDate = null;

		if ($scope.newExamType.StartTime_TMP)
			$scope.newExamType.StartTime = $filter('date')(new Date($scope.newExamType.StartTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamType.StartTime = null;

		$scope.newExamType.ClassWiseColl = [];
		angular.forEach($scope.newExamType.ClassWiseColl_TMP, function (cc) {
			if (cc.ResultDateDet) {
				cc.ResultDateTime = $filter('date')(new Date(cc.ResultDateDet.dateAD), 'yyyy-MM-dd');
			}

			if (cc.ResultTime_TMP)
				cc.ResultTime = $filter('date')(new Date(cc.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');


			if (cc.MarkSubmitDeadline_Teacher_TMP) {
				cc.MarkSubmitDeadline_Teacher = $filter('date')(new Date(cc.MarkSubmitDeadline_TeacherDet.dateAD), 'yyyy-MM-dd');
			}

			if (cc.TeacherTime_TMP)
				cc.TeacherTime = $filter('date')(new Date(cc.TeacherTime_TMP), 'yyyy-MM-dd HH:mm:ss');

			if (cc.ResultDateTime && cc.ClassIdColl && cc.ClassIdColl.length > 0)
				$scope.newExamType.ClassWiseColl.push(cc);
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamType();
				$scope.GetAllExamTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeId: refData.ExamTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamType = res.data.Data;

				//if ($scope.newExamType.ResultDate) {
				//	$scope.newExamType.ResultDateBS_TMP = new Date($scope.newExamType.ResultDate);
				//	$scope.newExamType.ResultDateAD_TMP = new Date($scope.newExamType.ResultDate);

				//}
				if ($scope.newExamType.ResultDate)
					$scope.newExamType.ResultDate_TMP = new Date($scope.newExamType.ResultDate);

				if ($scope.newExamType.ResultTime)
					$scope.newExamType.ResultTime_TMP = new Date($scope.newExamType.ResultTime);


				if ($scope.newExamType.ExamDate)
					$scope.newExamType.ExamDate_TMP = new Date($scope.newExamType.ExamDate);

				if ($scope.newExamType.StartTime)
					$scope.newExamType.StartTime_TMP = new Date($scope.newExamType.StartTime);


				if ($scope.newExamType.AdminTime)
					$scope.newExamType.AdminTime_TMP = new Date($scope.newExamType.AdminTime);

				if ($scope.newExamType.TeacherTime)
					$scope.newExamType.TeacherTime_TMP = new Date($scope.newExamType.TeacherTime);

				if ($scope.newExamType.MarkSubmitDeadline_Teacher)
					$scope.newExamType.MarkSubmitDeadline_Teacher_TMP = new Date($scope.newExamType.MarkSubmitDeadline_Teacher);

				if ($scope.newExamType.MarkSubmitDeadline_Admin)
					$scope.newExamType.MarkSubmitDeadline_Admin_TMP = new Date($scope.newExamType.MarkSubmitDeadline_Admin);

				$scope.newExamType.ClassWiseColl_TMP = [];

				if ($scope.newExamType.ClassWiseColl.length == 0)
					$scope.newExamType.ClassWiseColl_TMP.push({});

				angular.forEach($scope.newExamType.ClassWiseColl, function (cc) {

					if (cc.MarkSubmitDeadline_Teacher) {
						cc.MarkSubmitDeadline_Teacher_TMP = new Date(cc.MarkSubmitDeadline_Teacher);
						cc.TeacherTime_TMP = new Date(cc.MarkSubmitDeadline_Teacher);
					}

					if (cc.ResultDateTime) {
						cc.ResultDate_TMP = new Date(cc.ResultDateTime);
						cc.ResultTime_TMP = new Date(cc.ResultDateTime);
						$scope.newExamType.ClassWiseColl_TMP.push(cc);
					}


				});

				$scope.newExamType.Mode = 'Modify';

				document.getElementById('exam-type-section').style.display = "none";
				document.getElementById('exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeById = function (refData) {

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
					ExamTypeId: refData.ExamTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//************************* ExamType *********************************

	$scope.IsValidReExamType = function () {
		if ($scope.newReExamType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateReExamType = function () {
		if ($scope.IsValidReExamType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newReExamType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateReExamType();
					}
				});
			} else
				$scope.CallSaveUpdateReExamType();

		}
	};

	$scope.CallSaveUpdateReExamType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newReExamType.MarkSubmitDeadline_TeacherDet) {
			$scope.newReExamType.MarkSubmitDeadline_Teacher = $filter('date')(new Date($scope.newReExamType.MarkSubmitDeadline_TeacherDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newReExamType.MarkSubmitDeadline_Teacher = null;

		if ($scope.newReExamType.MarkSubmitDeadline_AdminDet) {
			$scope.newReExamType.MarkSubmitDeadline_Admin = $filter('date')(new Date($scope.newReExamType.MarkSubmitDeadline_AdminDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newReExamType.MarkSubmitDeadline_Admin = null;

		if ($scope.newReExamType.ResultDateDet) {
			$scope.newReExamType.ResultDate = $filter('date')(new Date($scope.newReExamType.ResultDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newReExamType.ResultDate = null;

		if ($scope.newReExamType.ResultTime_TMP)
			$scope.newReExamType.ResultTime = $filter('date')(new Date($scope.newReExamType.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newReExamType.ResultTime = null;

		if ($scope.newReExamType.AdminTime_TMP)
			$scope.newReExamType.AdminTime = $filter('date')(new Date($scope.newReExamType.AdminTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newReExamType.AdminTime = null;

		if ($scope.newReExamType.TeacherTime_TMP)
			$scope.newReExamType.TeacherTime = $filter('date')(new Date($scope.newReExamType.TeacherTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newReExamType.TeacherTime = null;

		if ($scope.newReExamType.ExamDateDet) {
			$scope.newReExamType.ExamDate = $filter('date')(new Date($scope.newReExamType.ExamDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newReExamType.ExamDate = null;

		if ($scope.newReExamType.StartTime_TMP)
			$scope.newReExamType.StartTime = $filter('date')(new Date($scope.newReExamType.StartTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newReExamType.StartTime = null;

		$scope.newReExamType.ClassWiseColl = [];
		angular.forEach($scope.newReExamType.ClassWiseColl_TMP, function (cc) {
			if (cc.ResultDateDet) {
				cc.ResultDateTime = $filter('date')(new Date(cc.ResultDateDet.dateAD), 'yyyy-MM-dd');
			}

			if (cc.ResultTime_TMP)
				cc.ResultTime = $filter('date')(new Date(cc.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');

			if (cc.ResultDateTime && cc.ClassIdColl && cc.ClassIdColl.length > 0)
				$scope.newExamType.ClassWiseColl_TMP.push(cc);
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveReExamType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newReExamType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearReExamType();
				$scope.GetAllReExamTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllReExamTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ReExamTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllReExamTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReExamTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetReExamTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ReExamTypeId: refData.ReExamTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetReExamTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newReExamType = res.data.Data;

				if ($scope.newReExamType.ResultDate)
					$scope.newReExamType.ResultDate_TMP = new Date($scope.newReExamType.ResultDate);

				if ($scope.newReExamType.ResultTime)
					$scope.newReExamType.ResultTime_TMP = new Date($scope.newReExamType.ResultTime);


				if ($scope.newReExamType.ExamDate)
					$scope.newReExamType.ExamDate_TMP = new Date($scope.newReExamType.ExamDate);

				if ($scope.newReExamType.StartTime)
					$scope.newReExamType.StartTime_TMP = new Date($scope.newReExamType.StartTime);


				if ($scope.newReExamType.AdminTime)
					$scope.newReExamType.AdminTime_TMP = new Date($scope.newReExamType.AdminTime);

				if ($scope.newReExamType.TeacherTime)
					$scope.newReExamType.TeacherTime_TMP = new Date($scope.newReExamType.TeacherTime);

				if ($scope.newReExamType.MarkSubmitDeadline_Teacher)
					$scope.newReExamType.MarkSubmitDeadline_Teacher_TMP = new Date($scope.newReExamType.MarkSubmitDeadline_Teacher);

				if ($scope.newReExamType.MarkSubmitDeadline_Admin)
					$scope.newReExamType.MarkSubmitDeadline_Admin_TMP = new Date($scope.newReExamType.MarkSubmitDeadline_Admin);

				$scope.newReExamType.ClassWiseColl_TMP = [];

				if ($scope.newReExamType.ClassWiseColl.length == 0)
					$scope.newReExamType.ClassWiseColl_TMP.push({});

				angular.forEach($scope.newReExamType.ClassWiseColl, function (cc) {
					if (cc.ResultDateTime) {
						cc.ResultDate_TMP = new Date(cc.ResultDateTime);
						cc.ResultTime_TMP = new Date(cc.ResultDateTime);
						$scope.newReExamType.ClassWiseColl_TMP.push(cc);
					}
				});

				$scope.newReExamType.Mode = 'Modify';

				document.getElementById('reexam-type-section').style.display = "none";
				document.getElementById('reexam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelReExamTypeById = function (refData) {

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
					ReExamTypeId: refData.ReExamTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelReExamType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllReExamTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Type Group *********************************

	$scope.IsValidExamTypeGroup = function () {
		if ($scope.newExamTypeGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}
	$scope.ShowSubjectWise = function (examGroup) {

		if (!examGroup.ExamTypeWiseSubjectColl || examGroup.ExamTypeWiseSubjectColl.length == 0) {
			examGroup.ExamTypeWiseSubjectColl = [];
			examGroup.ExamTypeWiseSubjectColl.push({
				SubjectId: null
			});
		}
		$scope.ExamTypeWiseSubjectColl = examGroup.ExamTypeWiseSubjectColl;
		$('#modal-exam').modal('show');

	};
	$scope.SaveUpdateExamTypeGroup = function () {
		if ($scope.IsValidExamTypeGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeGroup();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeGroup();

		}
	};


	$scope.CallSaveUpdateExamTypeGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newExamTypeGroup.ResultDateDet) {
			$scope.newExamTypeGroup.ResultDate = $filter('date')(new Date($scope.newExamTypeGroup.ResultDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamTypeGroup.ResultDate = null;

		if ($scope.newExamTypeGroup.ResultTime_TMP)
			$scope.newExamTypeGroup.ResultTime = $filter('date')(new Date($scope.newExamTypeGroup.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamTypeGroup.ResultTime = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamTypeGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamTypeGroup();
				$scope.GetAllExamTypeGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeGroupId: refData.ExamTypeGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamTypeGroup = res.data.Data;
				$scope.newExamTypeGroup.Mode = 'Modify';

				if ($scope.newExamTypeGroup.ResultDate)
					$scope.newExamTypeGroup.ResultDate_TMP = new Date($scope.newExamTypeGroup.ResultDate);

				if ($scope.newExamTypeGroup.ResultTime)
					$scope.newExamTypeGroup.ResultTime_TMP = new Date($scope.newExamTypeGroup.ResultTime);

				if (!$scope.newExamTypeGroup.ExamTypeGroupDetailsColl || $scope.newExamTypeGroup.ExamTypeGroupDetailsColl.length == 0) {
					$scope.newExamTypeGroup.ExamTypeGroupDetailsColl = [];
					$scope.newExamTypeGroup.ExamTypeGroupDetailsColl.push({});
				}

				document.getElementById('exam-type-group-section').style.display = "none";
				document.getElementById('exam-type-group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeGroupById = function (refData) {

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
					ExamTypeGroupId: refData.ExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeGroupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//*************************ParentExamTypeGroup *********************************

	$scope.IsValidParentExamTypeGroup = function () {
		if ($scope.newParentExamTypeGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Parent Exam Type Group Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateParentExamTypeGroup = function () {
		if ($scope.IsValidParentExamTypeGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newParentExamTypeGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateParentExamTypeGroup();
					}
				});
			} else
				$scope.CallSaveUpdateParentExamTypeGroup();

		}
	};

	$scope.CallSaveUpdateParentExamTypeGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newParentExamTypeGroup.ResultDateDet) {
			$scope.newParentExamTypeGroup.ResultDate = $filter('date')(new Date($scope.newParentExamTypeGroup.ResultDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newParentExamTypeGroup.ResultDate = null;

		if ($scope.newParentExamTypeGroup.ResultTime_TMP)
			$scope.newParentExamTypeGroup.ResultTime = $filter('date')(new Date($scope.newParentExamTypeGroup.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newParentExamTypeGroup.ResultTime = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveParentExamTypeGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newParentExamTypeGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearParentExamTypeGroup();
				$scope.GetAllParentExamTypeGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllParentExamTypeGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ParentExamTypeGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllParentExamTypeGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ParentExamTypeGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetParentExamTypeGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetParentExamTypeGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newParentExamTypeGroup = res.data.Data;
				$scope.newParentExamTypeGroup.Mode = 'Modify';

				if ($scope.newParentExamTypeGroup.ResultDate)
					$scope.newParentExamTypeGroup.ResultDate_TMP = new Date($scope.newParentExamTypeGroup.ResultDate);

				if ($scope.newParentExamTypeGroup.ResultTime)
					$scope.newParentExamTypeGroup.ResultTime_TMP = new Date($scope.newParentExamTypeGroup.ResultTime);

				if (!$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl || $scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.length == 0) {
					$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl = [];
					$scope.newParentExamTypeGroup.ParentExamTypeGroupDetailsColl.push({});
				}

				document.getElementById('parent-exam-type-section').style.display = "none";
				document.getElementById('parent-exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelParentExamTypeGroupById = function (refData) {

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
					ParentExamTypeGroupId: refData.ParentExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelParentExamTypeGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllParentExamTypeGroupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.PublishExamResult = function (et) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para =
		{
			ExamTypeId: et.ExamTypeId,
			ClassIdColl: ''
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Report/PublishExamResult",
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

	$scope.PublishGroupExamResult = function (et) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para =
		{
			ExamTypeGroupId: et.ExamTypeGroupId,
			CurExamTypeId: (et.CurrentExamTypeId ? et.CurrentExamTypeId : 0),
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

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.ResultDate_TMP = new Date();
				obj.MarkSubmitDeadline_Teacher_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};


	//$scope.validateDate = function (obj, startField, endFields, startLabel, endLabels) {

	//	if (!Array.isArray(endFields)) {
	//		endFields = [endFields];
	//	}

	//	if (!Array.isArray(endLabels)) {
	//		endLabels = [endLabels];
	//	}

	//	for (var i = 0; i < endFields.length; i++) {

	//		var res = GlobalServices.validateDate(
	//			obj,
	//			startField,
	//			endFields[i],
	//			startLabel,
	//			endLabels[i] || endLabels[0]
	//		);

	//		if (!res.IsSuccess) {

	//			Swal.fire({
	//				icon: 'warning',
	//				text: res.Message,
	//				confirmButtonText: 'OK'
	//			}).then(function () {

	//				obj.ResultDate_TMP = new Date();

	//				//// Reset only the invalid fields if desired
	//				//obj.MarkSubmitDeadline_Teacher_TMP = new Date();
	//				//obj.MarkSubmitDeadline_Admin_TMP = new Date();

	//				$scope.$applyAsync();
	//			});

	//			return false;
	//		}
	//	}

	//	return true;
	//};
});