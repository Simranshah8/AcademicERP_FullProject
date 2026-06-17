app.controller('SettingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Library Setting';
	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			Setting: 1,
		};

		$scope.searchData = {
			Setting: '',
		};

		$scope.perPage = {
			Setting: GlobalServices.getPerPageRow(),
		};


		$scope.ClassSection = {};
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BookCategoryList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookCategory",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookCategoryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
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

					//$timeout(function () {
					//	$scope.LoadClassList();
					//});

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

			$scope.ClassYearList = [];
			$scope.SelectedClassClassYearList = [];
			GlobalServices.getClassYearList().then(function (res) {
				$scope.ClassYearList = res.data.Data;

				//$timeout(function () {
				//	$scope.LoadClassList();
				//});

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newSetting_ST = {
			ForStudentTeacher:1,
			BookLimit: 0,
			CreditDays: 0,
			FixFineAmount: 0,
			LateFineAmountPerDay: 0,
			SlabWiseFine: false,
			FineRuleColl:[],
			Mode: 'Save'
		};
		$scope.newSetting_ST.FineRuleColl.push({});

		$scope.newSetting_EMP = {
			ForStudentTeacher: 2,
			BookLimit: 0,
			CreditDays: 0,
			FixFineAmount: 0,
			LateFineAmountPerDay: 0,
			SlabWiseFine: false,
			FineRuleColl: [],
			Mode: 'Save'
		};
		$scope.newSetting_EMP.FineRuleColl.push({});

		$scope.newLibraryClassWiseSetting = [];
		$scope.newLibraryClassWiseSetting.push({
			ClassId: null,
			BookLimit:0,
			FineRuleColl: [],
		});


		$scope.CategoryWiseSetting = [];
		$scope.CategoryWiseSetting.push({
			CategoryId: null,
			BookLimit: 0,
			FineRuleColl: []
        })
		$scope.GetAllSettingList();
	};

	$scope.CurFineRuleColl = [];

	$scope.ClearSetting = function () {
		$scope.newSetting = {
			St_BookLimit: '',
			St_CreditDays: '',
			St_LateFine: '',
			St_LateFinePerDay: '',
			Emp_BookLimit: '',
			Emp_CreditDays: '',
			Emp_LateFine: '',
			Emp_LateFinePerDay: '',
			BookIssueStudentColl: [],
			BookIssueTeacherColl: [],
			BookIssueClasswiseColl: [],
			FineAmountSlabwiseColl: [],
			Mode: 'Save'
		};
		$scope.newSetting.BookIssueStudentColl.push({});
		$scope.newSetting.BookIssueTeacherColl.push({});
		$scope.newSetting.BookIssueClasswiseColl.push({});
		$scope.newSetting.FineAmountSlabwiseColl.push({});
	};

	//Add and delete BookIssue Studentwise
	$scope.AddBookIssueStudent = function (ind) {
		if ($scope.newSetting_ST.FineRuleColl) {
			if ($scope.newSetting_ST.FineRuleColl.length > ind + 1) {
				$scope.newSetting_ST.FineRuleColl.splice(ind + 1, 0, {
				})
			} else {
				$scope.newSetting_ST.FineRuleColl.push({					
				})
			}
		}
	};

	$scope.delBookIssueStudent = function (ind) {
		if ($scope.newSetting_ST.FineRuleColl) {
			if ($scope.newSetting_ST.FineRuleColl.length > 1) {
				$scope.newSetting_ST.FineRuleColl.splice(ind, 1);
			}
		}
	};

	//Add and delete BookIssue Teacherwise
	$scope.AddBookIssueTeacher = function (ind) {
		if ($scope.newSetting_EMP.FineRuleColl) {
			if ($scope.newSetting_EMP.FineRuleColl.length > ind + 1) {
				$scope.newSetting_EMP.FineRuleColl.splice(ind + 1, 0, {
				})
			} else {
				$scope.newSetting_EMP.FineRuleColl.push({
				})
			}
		}
	};
	$scope.delBookIssueTeacher = function (ind) {
		if ($scope.newSetting_EMP.FineRuleColl) {
			if ($scope.newSetting_EMP.FineRuleColl.length > 1) {
				$scope.newSetting_EMP.FineRuleColl.splice(ind, 1);
			}
		}
	};

	//Add and delete BookIssue Classwise
	$scope.AddBookIssueClasswise = function (ind) {
		if ($scope.newLibraryClassWiseSetting) {
			if ($scope.newLibraryClassWiseSetting.length > ind + 1) {
				$scope.newLibraryClassWiseSetting.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newLibraryClassWiseSetting.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delBookIssueClasswise = function (ind, item) {
		if ($scope.newLibraryClassWiseSetting) {
			$scope.newLibraryClassWiseSetting.splice(ind, 1);
			if (item.TranId) {
				$scope.DeleteBookIssueClassWiseById(item);
			}
			if ($scope.newLibraryClassWiseSetting.length === 0) {
				$scope.newLibraryClassWiseSetting.push({
					TranId: null,
					ClassId: null
				});
			}
		}
	};

	//Add and delete FineAmountSlabwise of modal
	$scope.AddFineAmountSlabwise = function (ind) {
		if ($scope.CurFineRuleColl) {
			if ($scope.CurFineRuleColl.length > ind + 1) {
				$scope.CurFineRuleColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.CurFineRuleColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delFineAmountSlabwise = function (ind) {
		if ($scope.CurFineRuleColl) {
			if ($scope.CurFineRuleColl.length > 1) {
				$scope.CurFineRuleColl.splice(ind, 1);
			}
		}
	};



	$scope.AddFineAmountSlabwiseC = function (ind) {
		if ($scope.CurFineRuleCollC) {
			if ($scope.CurFineRuleCollC.length > ind + 1) {
				$scope.CurFineRuleCollC.splice(ind + 1, 0, {
					CategoryName: ''
				})
			} else {
				$scope.CurFineRuleCollC.push({
					CategoryName: ''
				})
			}
		}
	};
	$scope.delFineAmountSlabwiseC = function (ind) {
		if ($scope.CurFineRuleCollC) {
			if ($scope.CurFineRuleCollC.length > 1) {
				$scope.CurFineRuleCollC.splice(ind, 1);
			}
		}
	};

	//Add and delete CategoryWise
	$scope.AddBookIssueCategorywise = function (ind) {
		if ($scope.CategoryWiseSetting) {
			if ($scope.CategoryWiseSetting.length > ind + 1) {
				$scope.CategoryWiseSetting.splice(ind + 1, 0, {
				})
			} else {
				$scope.CategoryWiseSetting.push({
				})
			}
		}
	};
	$scope.delBookIssueCategorywise = function (ind) {
		if ($scope.CategoryWiseSetting) {
			if ($scope.CategoryWiseSetting.length > 1) {
				$scope.CategoryWiseSetting.splice(ind, 1);
			}
		}
	};


	$scope.ShowClassWiseFineRule = function (beData)
	{
		$scope.CurFineRuleColl = beData.FineRuleColl;

		if ($scope.CurFineRuleColl == null)
			$scope.CurFineRuleColl = [];

		if ($scope.CurFineRuleColl.length == 0)
			$scope.CurFineRuleColl.push({});

		$('#modal-xl').modal('show');
	};

	$scope.ShowCategoryWiseFineRule = function (beData) {
		$scope.CurFineRuleCollC = beData.FineRuleColl;

		if ($scope.CurFineRuleCollC == null)
			$scope.CurFineRuleCollC = [];

		if ($scope.CurFineRuleCollC.length == 0)
			$scope.CurFineRuleCollC.push({});

		$('#modal-xlC').modal('show');
	};

	$scope.IsValidSetting = function () {
		var valid = true;
		return valid;
    }
	$scope.SaveUpdateSetting = function () {
		if ($scope.IsValidSetting() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSetting.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSetting();
					}
				});
			} else
				$scope.CallSaveUpdateSetting();

		}
	};

	$scope.CallSaveUpdateSetting = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();		

		angular.forEach($scope.newLibraryClassWiseSetting, function (item) {
			if (item.SelectedClass) {
				item.ClassId = item.SelectedClass.ClassId;  // Add ClassId to the object
			}
		});

		var beData = {
			Student: $scope.newSetting_ST,
			Teacher: $scope.newSetting_EMP,
			ClassWise: $scope.newLibraryClassWiseSetting,
			CategoryWise: $scope.CategoryWiseSetting
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveSetting",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				
				return formData;
			},

			data: { jsonData: beData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllSettingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SettingList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetSetting",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				$timeout(function () {
					var dt = res.data.Data;
					$scope.newSetting_ST = dt.Student;
					$scope.newSetting_EMP = dt.Teacher;
					$scope.newLibraryClassWiseSetting = dt.ClassWise;
					$scope.CategoryWiseSetting = dt.CategoryWise;

					//Added By suresh(eha samma data aako xa... Esma classId, SemesterId, ClassYearId lai view ma assign garne part pending xa. Namelara coment gareko)
					
					//var classQry = mx($scope.ClassSection);
					//angular.forEach($scope.newLibraryClassWiseSetting, function (cc) {
					//	cc.ClassId = cc.id;
					//	cc.ClassType = cc.ClassType;
					//	var SelectedClass = classQry.firstOrDefault(p1 => p1.ClassId == cc.id);

					//	if (SelectedClass) {
					//		var semQry = mx(SelectedClass.ClassSemesterIdColl);
					//		var cyQry = mx(SelectedClass.ClassYearIdColl);

					//		cc.SelectedClassClassYearList = [];
					//		cc.SelectedClassSemesterList = [];

					//		angular.forEach($scope.SemesterList, function (sem) {
					//			if (semQry.contains(sem.id)) {
					//				cc.SelectedClassSemesterList.push({
					//					id: sem.id,
					//					text: sem.text,
					//					SemesterId: sem.id,
					//					Name: sem.Name
					//				});
					//			}
					//		});

					//		angular.forEach($scope.ClassYearList, function (sem) {
					//			if (cyQry.contains(sem.id)) {
					//				cc.SelectedClassClassYearList.push({
					//					id: sem.id,
					//					text: sem.text,
					//					ClassYearId: sem.id,
					//					Name: sem.Name
					//				});
					//			}
					//		});

					//	}

					//});

					$scope.$watch('ClassSection.ClassList', function (newVal) {
						if (newVal && newVal.length > 0 && $scope.newLibraryClassWiseSetting) {
							angular.forEach($scope.newLibraryClassWiseSetting, function (setStu) {
								var matchedClass = newVal.find(function (cl) {
									return cl.ClassId == setStu.ClassId;
								});

								if (matchedClass) {
									setStu.SelectedClass = matchedClass;
								}
							});
						}
					});
					//ends

					if ($scope.newSetting_ST.FineRuleColl.length == 0) {
						$scope.newSetting_ST.FineRuleColl.push({
							FromDays: 0,
							ToDays: 0,
							FineAmount:0
						});
                    }
						

					if ($scope.newSetting_EMP.FineRuleColl.length == 0) {
						$scope.newSetting_EMP.FineRuleColl.push({
							FromDays: 0,
							ToDays: 0,
							FineAmount: 0
						});
                    }
						

					if ($scope.newLibraryClassWiseSetting.length == 0) {
						$scope.newLibraryClassWiseSetting.push({
							ClassId: 0,
							BookLimit: 0,
							CreditDays:0
						});
					}

					if ($scope.CategoryWiseSetting.length == 0) {
						$scope.CategoryWiseSetting.push({
							CategoryId: 0,
							BookLimit: 0,
							CreditDays: 0
						});
					}
						

					$scope.newSetting_ST.Mode = 'Save';
					$scope.newSetting_EMP.Mode = 'Save';
				});
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

    $scope.DeleteBookIssueClassWiseById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
			TranId: refData.TranId,
			ClassId: refData.ClassId,
        };
        $http({
            method: 'POST',
            url: base_url + "Library/Master/DeleteBookIssueClassWiseById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
			//if (!res.data.IsSuccess) {
			//	Swal.fire("Error", res.data.ResponseMSG || "Failed to delete record.", "error");
			//} else {
			//	Swal.fire("Deleted!", "Record deleted successfully.", "success");
			//}
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
            //Swal.fire('Failed' + reason);
        });
	};


});