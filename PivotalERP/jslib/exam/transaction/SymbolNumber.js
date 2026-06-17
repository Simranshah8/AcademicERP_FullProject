app.controller('SymbolNumberController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Symbol Number';

	//OnClickDefault();

	var glbS = GlobalServices;

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();


		$scope.currentPages = {
			SymbolNumber: 1,
			ReSymbolNumber: 1,
			SymbolNumberTransfer: 1,
			/*HeightAndWeight: 1,*/
			ResultDispatch: 1,
			/*	HeightAndWeightTransfer: 1,*/


		};

		$scope.searchData = {
			SymbolNumber: '',
			ReSymbolNumber: '',
			SymbolNumberTransfer: '',
			/*	HeightAndWeight: '',*/
			ResultDispatch: '',
			/*HeightAndWeightTransfer: ''*/
		};

		$scope.perPage = {
			SymbolNumber: 50,
			ReSymbolNumber: 50,
			SymbolNumberTransfer: 50,
			/*HeightAndWeight: glbS.getPerPageRow(),*/
			ResultDispatch: 50,
			/*	HeightAndWeightTransfer: glbS.getPerPageRow(),*/
		};



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


		$scope.ClassSection = {};
		$scope.ClassListsection = [];
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
			$scope.ClassListsection = res.data.Data;
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


		$scope.newSymbolNumber = {
			SymbolNumberId: null,
			StartNo: 1,
			StartAlpha: '',
			PadWidth: 0,
			Prefix: '',
			Suffix: '',
			ExamTypeId: null,
			Mode: 'Save'
		};

		$scope.newReSymbolNumber = {
			SymbolNumberId: null,
			StartNo: 1,
			StartAlpha: '',
			PadWidth: 0,
			Prefix: '',
			Suffix: '',
			ExamTypeId: null,
			Mode: 'Save'
		};


		$scope.newSymbolNumberTransfer = {
			FromExamTypeId: null,
			ToExamTypeId: null

		};
		//$scope.newHeightAndWeightTransfer = {
		//	FromExamTypeId: null,
		//	ToExamTypeId: null

		//};

		//$scope.newHeightAndWeight = {
		//	HeightAndWeightId: null,
		//	SelectedClass: null,
		//	ExamTypeId: null,
		//	StudentList: [],
		//	Mode: 'save'
		//};

		$scope.newResultDispatch = {
			SelectedClass: null,
			ExamTypeId: null,
			StudentList: [],
			Mode: 'save'
		};

		$scope.SortAsList = [{ text: 'RollNo', value: 'RollNo' }, { text: 'Name', value: 'Name' }, { text: 'RegdNo', value: 'RegdNo' }, { text: 'SectionName', value: 'SectionName' },
		{ text: 'BoardRegdNo', value: 'BoardRegdNo' },
		{ text: 'Section & RollNo', value: 'SectionName,RollNo' },
		{ text: 'Section & Name', value: 'SectionName,Name' },
		{ text: 'Section & RegdNo', value: 'SectionName,RegdNo' },
		{ text: 'Section & BoardRegdNo', value: 'SectionName,BoardRegdNo' },
		];

		///$scope.GetTranforHeightWeight();

	}

	function OnClickDefault() {

		document.getElementById('height-weight-form').style.display = "none";


		/// Examwise Symbol No Js

		document.getElementById('add-examwise-btn').onclick = function () {
			document.getElementById('examwise-symbol-section').style.display = "none";
			document.getElementById('examwise-symbol-section-form').style.display = "block";
		}
		document.getElementById('back-examwise-btn').onclick = function () {
			document.getElementById('examwise-symbol-section-form').style.display = "none";
			document.getElementById('examwise-symbol-section').style.display = "block";
		}

		// Height and Weight Js

		document.getElementById('add-height-weight').onclick = function () {
			document.getElementById('height-weight-section').style.display = "none";
			document.getElementById('height-weight-form').style.display = "block";
		}
		document.getElementById('back-height-weight').onclick = function () {
			document.getElementById('height-weight-form').style.display = "none";
			document.getElementById('height-weight-section').style.display = "block";
		}


	}

	$scope.ClearSymbolNumber = function () {
		$scope.newSymbolNumber = {
			SymbolNumberId: null,
			StartNo: 1,
			StartAlpha: '',
			PadWidth: 0,
			Prefix: '',
			Suffix: '',
			ExamTypeId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearResultDispatch = function () {
		$scope.newResultDispatch = {
			ExamTypeId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearReSymbolNumber = function () {
		$scope.newReSymbolNumber = {
			SymbolNumberId: null,
			StartNo: 1,
			StartAlpha: '',
			PadWidth: 0,
			Prefix: '',
			Suffix: '',
			ExamTypeId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearSymbolNumberTransfer = function () {
		$scope.newSymbolNumberTransfer = {
			FromExamTypeId: null,
			ToExamTypeId: null

		};
	}



	//$scope.ClearHeightAndWeight = function () {
	//	$scope.newHeightAndWeight = {
	//		HeightAndWeightId: null,
	//		SelectedClass: null,
	//		ExamTypeId: null,
	//		StudentList: [],
	//		Mode: 'save'
	//	};

	//}

	$scope.excludeSelectedExamType = function (item) {
		return item.id !== $scope.newSymbolNumberTransfer.FromExamTypeId;
	};

	//************************* Symbol Number *********************************

	$scope.IsValidSymbolNumber = function () {
		if (!$scope.newSymbolNumber.ExamTypeId) {
			Swal.fire('Please ! Select ExamType');
			return false;
		}

		if (!$scope.newSymbolNumber.SelectedClass) {
			Swal.fire('Please ! Select Class Name');
			return false;
		}
		var hasSymbolNo = false;

		angular.forEach($scope.newSymbolNumber.StudentList, function (sn) {
			if (sn.SymbolNo && sn.SymbolNo.toString().trim() !== '') {
				hasSymbolNo = true;
			}
		});

		if (!hasSymbolNo) {
			Swal.fire('At least one student must have a Symbol Number.');
			return false;
		}

		return true;
	}
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}
	$scope.DataSorting = function () {

		var sortOptions = $scope.newSymbolNumber.SortAs.split(",");

		//var dataColl = $filter('orderBy')($filter('filter')($scope.UpdateStudentList, $scope.searchData.UpdateStudent), $scope.newUpdateStudent.SortAs, $scope.reverse);
		var dataColl = $filter('orderBy')($scope.newSymbolNumber.StudentList, sortOptions);
		$scope.newSymbolNumber.StudentList = [];
		$timeout(function () {
			$scope.$apply(function () {
				angular.forEach(dataColl, function (dc) {
					$scope.newSymbolNumber.StudentList.push(dc);
				})
			});
		});

	};

	$scope.AutoGenerateSymbolNo = function () {

		if ($scope.newSymbolNumber) {
			var startNo = parseInt($scope.newSymbolNumber.StartNo);
			var pad = $scope.newSymbolNumber.PadWidth;
			if (isNaN(startNo))
				startNo = 0;

			var startAlpha = $scope.newSymbolNumber.StartAlpha;

			//var tmpDataColl = $scope.newSymbolNumber.StudentList;// $filter('orderBy')($scope.newSymbolNumber.StudentList, $scope.sortKey, $scope.reverse);
			var tmpDataColl = $filter('orderBy')($scope.newSymbolNumber.StudentList, $scope.sortKeySN, $scope.reverse1);

			angular.forEach(tmpDataColl, function (st) {
				st.SymbolNo = $scope.newSymbolNumber.Prefix + startNo.toString().padStart(pad, '0') + $scope.newSymbolNumber.Suffix + (startAlpha ? startAlpha : '');
				startNo++;

				if (startAlpha)
					startAlpha = nextChar(startAlpha);
			});
		}

	};
	function nextChar(c) {
		var u = c.toUpperCase();
		if (same(u, 'Z')) {
			var txt = '';
			var i = u.length;
			while (i--) {
				txt += 'A';
			}
			return 'A';
			//return (txt + 'A');
		} else {
			var p = "";
			var q = "";
			if (u.length > 1) {
				p = u.substring(0, u.length - 1);
				q = String.fromCharCode(p.slice(-1).charCodeAt(0));
			}
			var l = u.slice(-1).charCodeAt(0);
			var z = nextLetter(l);

			return z;
			//if (z === 'A') {
			//	return p.slice(0, -1) + nextLetter(q.slice(-1).charCodeAt(0)) + z;
			//} else {
			//	return p + z;
			//}
		}
	}

	function nextLetter(l) {
		if (l < 90) {
			return String.fromCharCode(l + 1);
		}
		else {
			return 'A';
		}
	}

	function same(str, char) {
		var i = str.length;
		while (i--) {
			if (str[i] !== char) {
				return false;
			}
		}
		return true;
	}

	$scope.GetClassWiseAllStudent = function () {

		$scope.newSymbolNumber.StudentList = [];
		if ($scope.newSymbolNumber.SelectedClassOnly) {


			var sectionIdColl = '';
			var query = mx($scope.ClassSection.SectionList).where(p1 => p1.ClassId == $scope.newSymbolNumber.SelectedClassOnly.ClassId);
			angular.forEach(query, function (s) {
				if (sectionIdColl.length > 0)
					sectionIdColl = sectionIdColl + ',';

				sectionIdColl = sectionIdColl + s.SectionId;
			});

			var para = {
				ClassId: $scope.newSymbolNumber.SelectedClassOnly.ClassId,
				SectionIdColl: sectionIdColl,
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newSymbolNumber.StudentList = res1.data.Data;
					$timeout(function () {

						if ($scope.newSymbolNumber.ExamTypeId) {
							var para1 = {
								ClassId: $scope.newSymbolNumber.SelectedClassOnly.ClassId,
								SectionId: 0,
								ExamTypeId: $scope.newSymbolNumber.ExamTypeId,
							};

							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetSymbolNumberById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res2) {
								if (res2.data.IsSuccess && res2.data.Data) {
									var symDataColl = mx(res2.data.Data);

									if (symDataColl) {
										var fData = symDataColl.firstOrDefault();
										if (fData) {
											$scope.newSymbolNumber.StartAlpha = fData.StartAlpha;
											$scope.newSymbolNumber.StartNo = fData.StartNumber;
											$scope.newSymbolNumber.PadWidth = fData.PadWith;
											$scope.newSymbolNumber.Prefix = fData.Prefix;
											$scope.newSymbolNumber.Suffix = fData.Suffix;
										}
										angular.forEach($scope.newSymbolNumber.StudentList, function (st) {

											var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
											if (dt) {
												st.SymbolNo = dt.SymbolNo;
											}
										});

									}


								} else {
									Swal.fire(res2.data.ResponseMSG);
								}
							});
						}

					});


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};
	$scope.$watch('newSymbolNumber.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newSymbolNumber.BatchId = null;
			$scope.newSymbolNumber.ClassYearId = null;
			$scope.newSymbolNumber.SemesterId = null;
		}
	});

	$scope.GetClassWiseStudent = function () {

		$scope.newSymbolNumber.StudentList = [];
		if ($scope.newSymbolNumber.SelectedClass) {
			var para = {
				ClassId: $scope.newSymbolNumber.SelectedClass.ClassId,
				SectionIdColl: $scope.newSymbolNumber.SelectedClass.SectionId.toString(),
				FacultyId: $scope.newSymbolNumber.FacultyId,
				BatchId: $scope.newSymbolNumber.BatchId,
				ClassYearId: $scope.newSymbolNumber.ClassYearId,
				SemesterId: $scope.newSymbolNumber.SemesterId,
				FilterSection: $scope.newSymbolNumber.SelectedClass.FilterSection
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newSymbolNumber.StudentList = res1.data.Data;
					$timeout(function () {

						if ($scope.newSymbolNumber.ExamTypeId) {
							var para1 = {
								ClassId: $scope.newSymbolNumber.SelectedClass.ClassId,
								SectionId: $scope.newSymbolNumber.SelectedClass.SectionId,
								ExamTypeId: $scope.newSymbolNumber.ExamTypeId,
								FilterSection: $scope.newSymbolNumber.SelectedClass.FilterSection
							};

							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetSymbolNumberById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res2) {
								if (res2.data.IsSuccess && res2.data.Data) {
									var symDataColl = mx(res2.data.Data);

									if (symDataColl) {
										var fData = symDataColl.firstOrDefault();
										if (fData) {
											$scope.newSymbolNumber.StartAlpha = fData.StartAlpha;
											$scope.newSymbolNumber.StartNo = fData.StartNumber;
											$scope.newSymbolNumber.PadWidth = fData.PadWith;
											$scope.newSymbolNumber.Prefix = fData.Prefix;
											$scope.newSymbolNumber.Suffix = fData.Suffix;
										}
										angular.forEach($scope.newSymbolNumber.StudentList, function (st) {

											var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
											if (dt) {
												st.SymbolNo = dt.SymbolNo;
											}
										});

									}


								} else {
									Swal.fire(res2.data.ResponseMSG);
								}
							});
						}

					});


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};

	$scope.SaveUpdateSymbolNumber = function () {
		if ($scope.IsValidSymbolNumber() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSymbolNumber.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSymbolNumber();
					}
				});
			} else
				$scope.CallSaveUpdateSymbolNumber();

		}
	};

	$scope.CallSaveUpdateSymbolNumber = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpData = [];

		angular.forEach($scope.newSymbolNumber.StudentList, function (st) {

			var newData = {
				StudentId: st.StudentId,
				ExamTypeId: $scope.newSymbolNumber.ExamTypeId,
				Prefix: $scope.newSymbolNumber.Prefix,
				Suffix: $scope.newSymbolNumber.Suffix,
				StartNumber: $scope.newSymbolNumber.StartNo,
				StartAlpha: $scope.newSymbolNumber.StartAlpha,
				PadWith: $scope.newSymbolNumber.PadWidth,
				ClassYearId: $scope.newSymbolNumber.ClassYearId,
				SemesterId: $scope.newSymbolNumber.SemesterId,
				SymbolNo: st.SymbolNo
			}
			tmpData.push(newData);
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveSymbolNumber",
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
				$scope.ClearSymbolNumber();

			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.DelSymbolNumberById = function (refData) {

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
					SymbolNumberId: refData.SymbolNumberId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelSymbolNumber",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSymbolNumberList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.TransforSymbolNumber = function (refData) {
		if (!$scope.newSymbolNumberTransfer.FromExamTypeId || $scope.newSymbolNumberTransfer.FromExamTypeId == 0) {
			Swal.fire({
				title: 'Please select a From Exam Type!'
			});
			return;
		}

		if (!$scope.newSymbolNumberTransfer.ToExamTypeId || $scope.newSymbolNumberTransfer.ToExamTypeId == 0) {
			Swal.fire({
				title: 'Please select a To Exam Type!'
			});
			return;
		}
		if ($scope.newSymbolNumberTransfer.FromExamTypeId && $scope.newSymbolNumberTransfer.ToExamTypeId) {
			Swal.fire({
				title: 'Do you want to transfer symbol no of selected examtype ?',
				showCancelButton: true,
				confirmButtonText: 'Transfer',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					var para = {
						FromExamTypeId: $scope.newSymbolNumberTransfer.FromExamTypeId,
						ToExamTypeId: $scope.newSymbolNumberTransfer.ToExamTypeId
					};

					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/TransforSymbolNumber",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire(res.data.ResponseMSG);

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
		}
	};

	//************************* Re Symbol Number*********************************


	$scope.IsValidReSymbolNumber = function () {
		if (!$scope.newReSymbolNumber.ExamTypeId) {
			Swal.fire('Please ! Select ExamType');
			return false;
		}

		if (!$scope.newReSymbolNumber.ReExamTypeId) {
			Swal.fire('Please ! Select ReExamType');
			return false;
		}

		if (!$scope.newReSymbolNumber.SelectedClass) {
			Swal.fire('Please ! Select Class Name');
			return false;
		}

		return true;
	}

	$scope.ReDataSorting = function () {

		var sortOptions = $scope.newReSymbolNumber.SortAs.split(",");

		//var dataColl = $filter('orderBy')($filter('filter')($scope.UpdateStudentList, $scope.searchData.UpdateStudent), $scope.newUpdateStudent.SortAs, $scope.reverse);
		var dataColl = $filter('orderBy')($scope.newReSymbolNumber.StudentList, sortOptions);
		$scope.newReSymbolNumber.StudentList = [];
		$timeout(function () {
			$scope.$apply(function () {
				angular.forEach(dataColl, function (dc) {
					$scope.newReSymbolNumber.StudentList.push(dc);
				})
			});
		});

	};

	$scope.ReAutoGenerateSymbolNo = function () {

		if ($scope.newReSymbolNumber) {
			var startNo = parseInt($scope.newReSymbolNumber.StartNo);
			var pad = $scope.newReSymbolNumber.PadWidth;
			if (isNaN(startNo))
				startNo = 0;

			var startAlpha = $scope.newReSymbolNumber.StartAlpha;

			var tmpDataColl = $scope.newReSymbolNumber.StudentList;// $filter('orderBy')($scope.newSymbolNumber.StudentList, $scope.sortKey, $scope.reverse);

			angular.forEach(tmpDataColl, function (st) {
				st.SymbolNo = $scope.newReSymbolNumber.Prefix + startNo.toString().padStart(pad, '0') + $scope.newReSymbolNumber.Suffix + (startAlpha ? startAlpha : '');
				startNo++;

				if (startAlpha)
					startAlpha = nextChar(startAlpha);
			});
		}

	};


	$scope.GetReClassWiseAllStudent = function () {

		$scope.newReSymbolNumber.SubjectList = [];
		$scope.newReSymbolNumber.StudentList = [];
		if (($scope.newReSymbolNumber.SelectedClassOnly || $scope.newReSymbolNumber.SelectedClass) && $scope.newReSymbolNumber.ExamTypeId && $scope.newReSymbolNumber.ReExamTypeId) {

			var classId = null;
			var sectionId = null;
			var forClass = false;

			if ($scope.newReSymbolNumber.SelectedClassOnly) {
				classId = $scope.newReSymbolNumber.SelectedClassOnly.ClassId;
				forClass = true;
				sectionId = null;
			} else {
				classId = $scope.newReSymbolNumber.SelectedClass.ClassId;
				sectionId = $scope.newReSymbolNumber.SelectedClass.SectionId;
				forClass = false;
			}


			var para = {
				ClassId: classId,
				SectionId: sectionId,
				ExamTypeId: $scope.newReSymbolNumber.ExamTypeId,
				ReExamTypeId: $scope.newReSymbolNumber.ReExamTypeId,
				ForClassWise: forClass
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetReSymbolNumber",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newReSymbolNumber.StudentList = res1.data.Data.StudentList;
					$scope.newReSymbolNumber.SubjectList = res1.data.Data.SubjectList;

					if ($scope.newReSymbolNumber.StudentList && $scope.newReSymbolNumber.StudentList.length > 0) {
						var fData = $scope.newReSymbolNumber.StudentList[0];
						if (fData) {
							$scope.newReSymbolNumber.StartAlpha = fData.StartAlpha;
							$scope.newReSymbolNumber.StartNo = fData.StartNumber;
							$scope.newReSymbolNumber.PadWidth = fData.PadWith;
							$scope.newReSymbolNumber.Prefix = fData.Prefix;
							$scope.newReSymbolNumber.Suffix = fData.Suffix;
						}
					}

					angular.forEach($scope.newReSymbolNumber.StudentList, function (st) {
						st.SubjectList = [];
						var subColl = mx(st.ExamSubjectList);
						angular.forEach($scope.newReSymbolNumber.SubjectList, function (sb) {
							var findSub = subColl.firstOrDefault(p1 => p1.SubjectId == sb.SubjectId);
							if (findSub) {
								var newBeData = {
									SubjectId: sb.SubjectId,
									ConductReExam: findSub.ConductReExam,
									ObtainMark: findSub.ObtainMark,
									IsFail: findSub.IsFail,
									Enabled: true
								};
								st.SubjectList.push(newBeData);
							} else {
								var newBeData = {
									SubjectId: sb.SubjectId,
									ConductReExam: false,
									ObtainMark: '',
									IsFail: false,
									Enabled: false
								};
								st.SubjectList.push(newBeData);
							}
						});

					});


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};



	$scope.SaveUpdateReSymbolNumber = function () {
		if ($scope.IsValidReSymbolNumber() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newReSymbolNumber.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateReSymbolNumber();
					}
				});
			} else
				$scope.CallSaveUpdateReSymbolNumber();

		}
	};

	$scope.CallSaveUpdateReSymbolNumber = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpData = [];

		angular.forEach($scope.newReSymbolNumber.StudentList, function (st) {

			var newData = {
				StudentId: st.StudentId,
				ExamTypeId: $scope.newReSymbolNumber.ExamTypeId,
				ReExamTypeId: $scope.newReSymbolNumber.ReExamTypeId,
				Prefix: $scope.newReSymbolNumber.Prefix,
				Suffix: $scope.newReSymbolNumber.Suffix,
				StartNumber: $scope.newReSymbolNumber.StartNo,
				StartAlpha: $scope.newReSymbolNumber.StartAlpha,
				PadWith: $scope.newReSymbolNumber.PadWidth,
				SymbolNo: st.SymbolNo,
				ReExamSubjectList: [],
			}

			angular.forEach(st.SubjectList, function (sb) {
				if (sb.Enabled == true && sb.ConductReExam == true)
					newData.ReExamSubjectList.push(sb.SubjectId);
			});

			tmpData.push(newData);
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveReSymbolNumber",
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
				$scope.ClearReSymbolNumber();

			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.DelReSymbolNumberById = function (refData) {

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
					SymbolNumberId: refData.SymbolNumberId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelReSymbolNumber",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllReSymbolNumberList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	$scope.DelReExamSymbollNo = function (refData) {
		if ($scope.newReSymbolNumber &&
			($scope.newReSymbolNumber.SelectedClassOnly || $scope.newReSymbolNumber.SelectedClass) &&
			$scope.newReSymbolNumber.ExamTypeId &&
			$scope.newReSymbolNumber.ReExamTypeId) {

			Swal.fire({
				title: 'Do you want to delete the selected data?',
				showCancelButton: true,
				confirmButtonText: 'Delete',
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					let classId = null;
					let sectionId = null;
					let forClass = false;

					if ($scope.newReSymbolNumber.SelectedClassOnly) {
						classId = $scope.newReSymbolNumber.SelectedClassOnly.ClassId;
						forClass = true;
					} else {
						classId = $scope.newReSymbolNumber.SelectedClass.ClassId;
						sectionId = $scope.newReSymbolNumber.SelectedClass.SectionId || null;
					}

					const para = {
						ClassId: classId,
						SectionId: sectionId,
						ExamTypeId: $scope.newReSymbolNumber.ExamTypeId,
						ReExamTypeId: $scope.newReSymbolNumber.ReExamTypeId,
						ForClassWise: forClass
					};

					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/DeleteReSymbolNumberColl",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res.data.IsSuccess) {
							$scope.GetReClassWiseAllStudent(refData);
							Swal.fire(res.data.ResponseMSG);
						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
		} else {
			Swal.fire('Validation Error', 'Please ensure all required fields are selected.', 'warning');
		}
	};







	//*************************Height And Weight *********************************

	//$scope.IsValidHeightAndWeight = function () {

	//	if (!$scope.newHeightAndWeight.ExamTypeId) {
	//		Swal.fire('Please ! Select ExamType');
	//		return false;
	//	}

	//	if (!$scope.newHeightAndWeight.SelectedClass) {
	//		Swal.fire('Please ! Select Class Name');
	//		return false;
	//	}


	//	return true;
	//}
	//$scope.GetClassWiseStudentHW = function () {

	//	$scope.newHeightAndWeight.StudentList = [];
	//	if ($scope.newHeightAndWeight.SelectedClass) {
	//		var para = {
	//			ClassId: $scope.newHeightAndWeight.SelectedClass.ClassId,
	//			SectionIdColl: $scope.newHeightAndWeight.SelectedClass.SectionId.toString(),
	//		};

	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Transaction/GetClassWiseStudentList",
	//			dataSchedule: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res1) {
	//			if (res1.data.IsSuccess && res1.data.Data) {
	//				$scope.newHeightAndWeight.StudentList = res1.data.Data;
	//				$timeout(function () {

	//					if ($scope.newHeightAndWeight.ExamTypeId) {
	//						var para1 = {
	//							ClassId: $scope.newHeightAndWeight.SelectedClass.ClassId,
	//							SectionId: $scope.newHeightAndWeight.SelectedClass.SectionId,
	//							ExamTypeId: $scope.newHeightAndWeight.ExamTypeId
	//						};

	//						$http({
	//							method: 'POST',
	//							url: base_url + "Exam/Transaction/GetHeightWeightById",
	//							dataSchedule: "json",
	//							data: JSON.stringify(para1)
	//						}).then(function (res2) {
	//							if (res2.data.IsSuccess && res2.data.Data) {
	//								var symDataColl = mx(res2.data.Data);

	//								if (symDataColl) {

	//									angular.forEach($scope.newHeightAndWeight.StudentList, function (st) {

	//										var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
	//										if (dt) {
	//											st.Height = dt.Height;
	//											st.Weight = dt.Weight;
	//										}
	//									});

	//								}


	//							} else {
	//								Swal.fire(res2.data.ResponseMSG);
	//							}
	//						});
	//					}

	//				});


	//			} else {
	//				Swal.fire(res1.data.ResponseMSG);
	//			}
	//		});
	//	}

	//};
	//$scope.SaveUpdateHeightAndWeight = function () {
	//	if ($scope.IsValidHeightAndWeight() == true) {
	//		if ($scope.confirmMSG.Accept == true) {
	//			var saveModify = $scope.newHeightAndWeight.Mode;
	//			Swal.fire({
	//				title: 'Do you want to ' + saveModify + ' the current data?',
	//				showCancelButton: true,
	//				confirmButtonText: saveModify,
	//			}).then((result) => {
	//				/* Read more about isConfirmed, isDenied below */
	//				if (result.isConfirmed) {
	//					$scope.CallSaveUpdateHeightAndWeight();
	//				}
	//			});
	//		} else
	//			$scope.CallSaveUpdateHeightAndWeight();

	//	}
	//};

	//$scope.CallSaveUpdateHeightAndWeight = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var tmpData = [];

	//	angular.forEach($scope.newHeightAndWeight.StudentList, function (st) {

	//		var newData = {
	//			StudentId: st.StudentId,
	//			ExamTypeId: $scope.newHeightAndWeight.ExamTypeId,
	//			Weight: st.Weight,
	//			Height: st.Height
	//		}
	//		tmpData.push(newData);
	//	});

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Exam/Transaction/SaveHeightAndWeight",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			return formData;
	//		},
	//		data: { jsonData: tmpData }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);

	//		if (res.data.IsSuccess == true) {
	//			$scope.ClearHeightAndWeight();
	//		}

	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});
	//}

	//$scope.DelHeightAndWeightById = function (refData) {

	//	Swal.fire({
	//		title: 'Do you want to delete the selected data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Delete',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			$scope.loadingstatus = "running";
	//			showPleaseWait();

	//			var para = {
	//				HeightAndWeightId: refData.HeightAndWeightId
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Exam/Transaction/DelHeightAndWeight",
	//				dataType: "json",
	//				data: JSON.stringify(para)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				if (res.data.IsSuccess) {
	//					$scope.GetAllHeightAndWeightList();
	//				} else {
	//					Swal.fire(res.data.ResponseMSG);
	//				}

	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};


	$scope.GetClassWiseStudentForResult = function () {

		$scope.newResultDispatch.StudentList = [];
		if ($scope.newResultDispatch.SelectedClass && $scope.newResultDispatch.ExamTypeId) {
			var para = {
				ClassId: $scope.newResultDispatch.SelectedClass.ClassId,
				SectionId: $scope.newResultDispatch.SelectedClass.SectionId,
				ExamTypeId: $scope.newResultDispatch.ExamTypeId
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

	//*************************Height And Weight Transfor *********************************

	//$scope.SelectedExamType = function (item) {
	//	return item.id !== $scope.newHeightAndWeightTransfer.FromExamTypeId;
	//};

	//$scope.TransforHeightWeight = function () {

	//	if ($scope.newHeightAndWeightTransfer.FromExamTypeId && $scope.newHeightAndWeightTransfer.ToExamTypeId) {
	//		Swal.fire({
	//			title: 'Do you want to transfer height and weight of selected examtype ?',
	//			showCancelButton: true,
	//			confirmButtonText: 'Transfer',
	//		}).then((result) => {
	//			/* Read more about isConfirmed, isDenied below */
	//			if (result.isConfirmed) {
	//				$scope.loadingstatus = "running";
	//				showPleaseWait();

	//				var para = {
	//					FromExamTypeId: $scope.newHeightAndWeightTransfer.FromExamTypeId,
	//					ToExamTypeId: $scope.newHeightAndWeightTransfer.ToExamTypeId
	//				};

	//				$http({
	//					method: 'POST',
	//					url: base_url + "Exam/Transaction/TransforHeightWeight",
	//					dataType: "json",
	//					data: JSON.stringify(para)
	//				}).then(function (res) {
	//					hidePleaseWait();
	//					$scope.loadingstatus = "stop";
	//					Swal.fire(res.data.ResponseMSG);

	//				}, function (reason) {
	//					Swal.fire('Failed' + reason);
	//				});
	//			}
	//		});
	//	}
	//};


	//$scope.GetTranforHeightWeight = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.HeightAndWeightTransferList = [];
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Exam/Transaction/GetTranforHeightWeight",
	//		dataType: "json"
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.HeightAndWeightTransferList = res.data.Data;
	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};


	//$scope.DelTransforHW = function (refData) {
	//	Swal.fire({
	//		title: 'Do you want to delete the selected data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Delete',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			$scope.loadingstatus = "running";
	//			showPleaseWait();
	//			var para = {
	//				TranId: refData.TranId
	//			};
	//			$http({
	//				method: 'POST',
	//				url: base_url + "Exam/Transaction/DeleteTransforHeigthWeigthById",
	//				dataType: "json",
	//				data: JSON.stringify(para)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				if (res.data.IsSuccess) {
	//					$scope.GetTranforHeightWeight();
	//				} else {
	//					Swal.fire(res.data.ResponseMSG);
	//				}

	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});
	//};

	$scope.sortSN = function (keyname) {
		$scope.sortKeySN = keyname;   //set the sortKey to the param passed
		$scope.reverse1 = !$scope.reverse1; //if true make it false and vice versa
	}

	$scope.sort = function (keyname) {
		$scope.sortKeyRS = keyname;   //set the sortKey to the param passed
		$scope.reverse2 = !$scope.reverse2; //if true make it false and vice versa
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});