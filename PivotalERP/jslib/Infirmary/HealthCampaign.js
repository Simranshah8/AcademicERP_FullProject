app.controller('HealthCampaignController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'HealthCampaign';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();
		$scope.MonthList = GlobalServices.getMonthList();


		$scope.currentPages = {
			HealthCampaign: 1,
			CheckupSchedule: 1,
		};

		$scope.searchData = {
			HealthCampaign: '',
			CheckupSchedule: '',
			HealthCampaignTest: '',
			HealthCampaignHealthIssue: '',
			GeneralCheckupHealthIssue: '',
			GCCheckupScheduleTest: '',
			GCCheckupScheduleVaccine: ''
		};

		$scope.perPage = {
			HealthCampaign: GlobalServices.getPerPageRow(),
			CheckupSchedule: GlobalServices.getPerPageRow(),
		};

		$scope.HealthIssueList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllHealthIssue",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthIssueList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.TestNameList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllTestName",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TestNameList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.VaccineList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllVaccine",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VaccineList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.getExaminerFromExaminersList = function (id) {
			for (let i = 0; i < $scope.ExaminersList.length; i++) {
				if (id == $scope.ExaminersList[i].ExaminerId) return $scope.ExaminersList[i];
			}
			return null;
		}

		$scope.updateExaminerDetails = function (obj) {
			curExaminer = $scope.getExaminerFromExaminersList(obj.ExaminerId);
			if (!curExaminer) return;
			obj.Name = curExaminer.Name;
			obj.ExaminerRegdNo = curExaminer.ExaminerRegdNo;
			obj.Designation = curExaminer.Designation;
			obj.MobileNo = curExaminer.MobileNo;
		}


		//$scope.ChangeExaminer = function (beData) {
		//	if (!beData.ExaminerId) {
		//		Swal.fire('Please ! Select 1st Employee');
		//	}
		//	var findMT = mx($scope.HealthExaminerList).firstOrDefault(p1 => p1.ExaminerId == beData.ExaminerId);
		//	if (findMT) {
		//		beData.ExaminerRegdNo = findMT.ExaminerRegdNo;
		//		beData.Designation = findMT.Designation;
		//		beData.MobileNo = findMT.MobileNo;
		//	}
		//}

		$scope.HealthExaminerList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllExaminer",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExaminersList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newHealthCampaign = {
			HealthCampaignId: null,
			Name: '',
			DateFrom: new Date(),
			DateTo: new Date(),
			Organizer: '',
			Vaccination: 0,
			Description: '',
			HealthCampaignRepColl: [],
			SelectClassColl: [],
			SelectDiseaseColl: [],
			SelectVaccineColl: [],
			SelectTestColl:[],
			Mode: 'Save'
		};
		$scope.newHealthCampaign.HealthCampaignRepColl.push({});

		$scope.NewGeneralCheckup = {
			GeneralCheckUpId: null,
			Name: '',
			Month: '',
			DateFrom: new Date(),
			DateTo: new Date(),
			Organizer: '',
			Vaccination: 0,
			Description: '',
			GeneralCheckUpRepColl: [],
			Mode: 'Save'
		};
		$scope.NewGeneralCheckup.GeneralCheckUpRepColl.push({});



		$scope.GetAllHealthCampaignList();
		$scope.GetAllGeneralCheckUpList();

	};


	$scope.ClearHealthCampaign = function () {


		$scope.ClassList.forEach(function (tc) {
			tc.IsSelected = false;

		});

		$scope.HealthIssueList.forEach(function (ds) {
			ds.IsSelectedHealth = false;
		});

		$scope.VaccineList.forEach(function (vs) {
			vs.IsSelectedVaccine = false;
		});

		$scope.TestNameList.forEach(function (ts) {
			ts.IsSelectedTest = false;
		});



		$scope.newHealthCampaign = {
			HealthCampaignId: null,
			Name: '',
			DateFrom: new Date(),
			DateTo: new Date(),
			Organizer: '',
			Vaccination: 0,
			Description: '',
			HealthCampaignRepColl: [],
			Mode: 'Save'
		};
		$scope.newHealthCampaign.HealthCampaignRepColl.push({});
	};

	$scope.ClearGeneralCheckUp = function () {

		$scope.ClassList.forEach(function (tc) {
			tc.IsGClassSelected = false;

		});

		$scope.HealthIssueList.forEach(function (tc) {
			tc.IsGDiseaseSelected = false;
		});

		$scope.TestNameList.forEach(function (tc) {
			tc.IsGTestSelected = false;
		});

		$scope.VaccineList.forEach(function (tc) {
			tc.IsGVaccineSelected = false;
		});



		$scope.NewGeneralCheckup = {
			GeneralCheckUpId: null,
			Name: '',
			Month: '',
			DateFrom: new Date(),
			DateTo: new Date(),
			Organizer: '',
			Vaccination:0,
			Description: '',
			GeneralCheckUpRepColl: [],
			Mode: 'Save'
		};
		$scope.NewGeneralCheckup.GeneralCheckUpRepColl.push({});
	};

	$scope.AddHealthCampaign = function (ind) {
		if ($scope.newHealthCampaign.HealthCampaignRepColl) {
			if ($scope.newHealthCampaign.HealthCampaignRepColl.length > ind + 1) {
				$scope.newHealthCampaign.HealthCampaignRepColl.splice(ind + 1, 0, {
					Organization: ''
				})
			} else {
				$scope.newHealthCampaign.HealthCampaignRepColl.push({
					Organization: ''
				})
			}
		}
	};

	$scope.delHealthCampaign = function (ind) {
		if ($scope.newHealthCampaign.HealthCampaignRepColl) {
			if ($scope.newHealthCampaign.HealthCampaignRepColl.length > 1) {
				$scope.newHealthCampaign.HealthCampaignRepColl.splice(ind, 1);
			}
		}
	};


	$scope.AddGeneralCheckupExaminer = function (ind) {
		if ($scope.NewGeneralCheckup.GeneralCheckUpRepColl) {
			if ($scope.NewGeneralCheckup.GeneralCheckUpRepColl.length > ind + 1) {
				$scope.NewGeneralCheckup.GeneralCheckUpRepColl.splice(ind + 1, 0, {
					Organization: ''
				})
			} else {
				$scope.NewGeneralCheckup.GeneralCheckUpRepColl.push({
					Organization: ''
				})
			}
		}
	};

	$scope.DelGeneralCheckupExaminer = function (ind) {
		if ($scope.NewGeneralCheckup.GeneralCheckUpRepColl) {
			if ($scope.NewGeneralCheckup.GeneralCheckUpRepColl.length > 1) {
				$scope.NewGeneralCheckup.GeneralCheckUpRepColl.splice(ind, 1);
			}
		}
	};
	

	function OnClickDefault() {
		document.getElementById('healthcampaignform').style.display = "none";
		document.getElementById('CheckupScheduleform').style.display = "none";

		document.getElementById('add-healthcampaign').onclick = function () {
			document.getElementById('campaigntable').style.display = "none";
			document.getElementById('healthcampaignform').style.display = "block";
			$scope.ClearHealthCampaign();
		}
		document.getElementById('backtohealthcampaignlist').onclick = function () {
			document.getElementById('campaigntable').style.display = "block";
			document.getElementById('healthcampaignform').style.display = "none";
			$scope.ClearHealthCampaign();
		}
		//Schedule js
		document.getElementById('add-CheckupSchedule').onclick = function () {
			document.getElementById('scheduletable').style.display = "none";
			document.getElementById('CheckupScheduleform').style.display = "block";
			$scope.ClearHealthCampaign();
		}
		document.getElementById('backtogeneralcheckuplist').onclick = function () {
			document.getElementById('scheduletable').style.display = "block";
			document.getElementById('CheckupScheduleform').style.display = "none";
			$scope.ClearHealthCampaign();
		}
	};

	//************************* Health Campaigh *********************************
	$scope.CheckUnCheckAllClass = function (payH)
	{
		if ($scope.ClassList) {
			$scope.ClassList.forEach(function (ph) {
				ph.IsSelected = payH.CheckedAll;
			});
		}
	};

	$scope.CheckUnCheckAllDisease = function (payH) {
		if ($scope.HealthIssueList) {
			$scope.HealthIssueList.forEach(function (ph) {
				ph.IsSelectedHealth = payH.CheckedAll;
			});
		}
	};

	$scope.CheckUnCheckAllTest = function (payH) {
		if ($scope.TestNameList) {
			$scope.TestNameList.forEach(function (ph) {
				ph.IsSelectedTest = payH.CheckAllTest;
			});
		}
	};

	$scope.CheckUnCheckAllVaccine = function (payH) {
		if ($scope.VaccineList) {
			$scope.VaccineList.forEach(function (ph) {
				ph.IsSelectedVaccine = payH.CheckedAll;
			});
		}
	};
	


	$scope.IsValidHealthCampaign = function () {
		if ($scope.newHealthCampaign.Name.isEmpty()) {
			Swal.fire('Please ! Enter Campaign Name');
			return false;
		}
		return true;
	};


	$scope.SaveUpdateHealthOperation = function () {
		if ($scope.IsValidHealthCampaign() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHealthCampaign.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHealthCampaign();
					}
				});
			} else
				$scope.CallSaveUpdateHealthCampaign();

		}
	};

	$scope.CallSaveUpdateHealthCampaign = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();



		var selectedClassIdColl = [];
		$scope.ClassList.forEach(function (tc) {
			if (tc.IsSelected == true) {
				selectedClassIdColl.push(tc.ClassId);
			}
		});

		$scope.newHealthCampaign.SelectClassColl = selectedClassIdColl;

		var selectedDiseaseIdColl = [];
		$scope.HealthIssueList.forEach(function (ds) {
			if (ds.IsSelectedHealth == true) {
				selectedDiseaseIdColl.push(ds.HealthIssueId);
			}
		});

		$scope.newHealthCampaign.SelectDiseaseColl = selectedDiseaseIdColl;


		var selectedVaccineIdColl = [];
		$scope.VaccineList.forEach(function (vs) {
			if (vs.IsSelectedVaccine == true) {
				selectedVaccineIdColl.push(vs.VaccineId);
			}
		});

		$scope.newHealthCampaign.SelectVaccineColl = selectedVaccineIdColl;


		var selectedTestIdColl = [];
		$scope.TestNameList.forEach(function (ts) {
			if (ts.IsSelectedTest == true) {
				selectedTestIdColl.push(ts.TestNameId);
            }
		});
		$scope.newHealthCampaign.SelectTestColl = selectedTestIdColl;


		if ($scope.newHealthCampaign.DateFromDet) {
			$scope.newHealthCampaign.DateFrom = $filter('date')(new Date($scope.newHealthCampaign.DateFromDet.dateAD), 'yyyy-MM-dd');
		}
		if ($scope.newHealthCampaign.DateToDet) {
			$scope.newHealthCampaign.DateTo = $filter('date')(new Date($scope.newHealthCampaign.DateToDet.dateAD), 'yyyy-MM-dd');
		}
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateHealthOperation",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newHealthCampaign }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHealthCampaign();
				$scope.GetAllHealthCampaignList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllHealthCampaignList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HealthCampaignList = [];

		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllHealthOperation",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthCampaignList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetHealthCampaignId = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			HealthCampaignId: refData.HealthCampaignId
		};

		$scope.ClassList.forEach(function (cl) {
			cl.IsSelected = false;
		});

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetHealthOperationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHealthCampaign = res.data.Data;				

				if (!$scope.newHealthCampaign.HealthCampaignRepColl || $scope.newHealthCampaign.HealthCampaignRepColl.length == 0) {
					$scope.newHealthCampaign.HealthCampaignRepColl = [];
					$scope.newHealthCampaign.HealthCampaignRepColl.push({});
				}
				if ($scope.newHealthCampaign.DateFrom) {
					$scope.newHealthCampaign.DateFrom_TMP = new Date($scope.newHealthCampaign.DateFrom);
				}
                
				if ($scope.newHealthCampaign.DateTo) {
					$scope.newHealthCampaign.DateTo_TMP = new Date($scope.newHealthCampaign.DateTo);
                }
				//For Class
				if ($scope.newHealthCampaign.SelectClassColl) {
					var clIdCOll = mx($scope.newHealthCampaign.SelectClassColl);

					$scope.ClassList.forEach(function (cl) {
						if(clIdCOll.contains(cl.ClassId)==true)
							cl.IsSelected = true;
						else
							cl.IsSelected = false;
					});
				}


				//For Health Issue
				if ($scope.newHealthCampaign.SelectDiseaseColl) {

					var clIdCOll1 = mx($scope.newHealthCampaign.SelectDiseaseColl);

					$scope.HealthIssueList.forEach(function (cl) {
						if (clIdCOll1.contains(cl.HealthIssueId) == true)
							cl.IsSelectedHealth = true;
						else
							cl.IsSelectedHealth = false;
					});
				}

				//For Test
				if ($scope.newHealthCampaign.SelectTestColl) {

					var clIdCOll2 = mx($scope.newHealthCampaign.SelectTestColl);

					$scope.TestNameList.forEach(function (cl) {
						if (clIdCOll2.contains(cl.TestNameId) == true)
							cl.IsSelectedTest = true;
						else
							cl.IsSelectedTest = false;
					});
				}

				//For Vaccine
				if ($scope.newHealthCampaign.SelectVaccineColl) {

					var clIdCOll3 = mx($scope.newHealthCampaign.SelectVaccineColl);

					$scope.VaccineList.forEach(function (cl) {
						if (clIdCOll3.contains(cl.VaccineId) == true)
							cl.IsSelectedVaccine = true;
						else
							cl.IsSelectedVaccine = false;
					});
				}


				$scope.newHealthCampaign.Mode = 'Modify';
				document.getElementById('campaigntable').style.display = "none";
				document.getElementById('healthcampaignform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHealthCampaignById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					HealthCampaignId: refData.HealthCampaignId
				};

				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteHealthOperation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess == true) {
						$scope.GetAllHealthCampaignList();

					}
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//----------------------------------General Checkup Schedule-------------------------------------------
	 $scope.CheckUnCheckAllGCClass = function (payH) {
		if ($scope.ClassList) {
			$scope.ClassList.forEach(function (ph) {
				ph.IsGClassSelected = payH.CheckedAll;
			});
		}
	};

	$scope.CheckUnCheckAllGCDisease = function (payH) {
		if ($scope.HealthIssueList) {
			$scope.HealthIssueList.forEach(function (ph) {
				ph.IsGDiseaseSelected = payH.CheckedAll;
			});
		}
	};


	$scope.CheckUnCheckAllGCTest = function (payH) {
		if ($scope.TestNameList) {
			$scope.TestNameList.forEach(function (ph) {
				ph.IsGTestSelected = payH.CheckedAll;
			});
		}
	};


	$scope.CheckUnCheckAllGCVaccine = function (payH) {
		if ($scope.VaccineList) {
			$scope.VaccineList.forEach(function (ph) {
				ph.IsGVaccineSelected = payH.CheckedAll;
			});
		}
	};

	

	$scope.IsValidGeneralCheckUpe = function () {
		if ($scope.NewGeneralCheckup.Name.isEmpty()) {
			Swal.fire('Please ! Enter CheckUp Name');
			return false;
		}
		return true;
	};


	$scope.SaveUpdateGeneralCheckUp = function () {
		if ($scope.IsValidGeneralCheckUpe() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.NewGeneralCheckup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGeneralCheckUp();
					}
				});
			} else
				$scope.CallSaveUpdateGeneralCheckUp();

		}
	};

	$scope.CallSaveUpdateGeneralCheckUp = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var selectedGClassIdColl = [];
		$scope.ClassList.forEach(function (tc) {
			if (tc.IsGClassSelected == true) {
				selectedGClassIdColl.push(tc.ClassId);
			}
		});

		$scope.NewGeneralCheckup.SelectGClassColl = selectedGClassIdColl;



		var selectedGDiseaseIdColl = [];
		$scope.HealthIssueList.forEach(function (tc) {
			if (tc.IsGDiseaseSelected == true) {
				selectedGDiseaseIdColl.push(tc.HealthIssueId);
			}
		});

		$scope.NewGeneralCheckup.SelectGDiseaseColl = selectedGDiseaseIdColl;


		var selectedGTestIdColl = [];
		$scope.TestNameList.forEach(function (tc) {
			if (tc.IsGTestSelected == true) {
				selectedGTestIdColl.push(tc.TestNameId);
			}
		});

		$scope.NewGeneralCheckup.SelectGTestColl = selectedGTestIdColl;


		var selectedGVaccineIdColl = [];
		$scope.VaccineList.forEach(function (tc) {
			if (tc.IsGVaccineSelected == true) {
				selectedGVaccineIdColl.push(tc.VaccineId);
			}
		});

		$scope.NewGeneralCheckup.SelectGVaccineColl = selectedGVaccineIdColl;



		if ($scope.NewGeneralCheckup.DateFromDet) {
			$scope.NewGeneralCheckup.DateFrom = $filter('date')(new Date($scope.NewGeneralCheckup.DateFromDet.dateAD), 'yyyy-MM-dd');
		}
		if ($scope.NewGeneralCheckup.DateToDet) {
			$scope.NewGeneralCheckup.DateTo = $filter('date')(new Date($scope.NewGeneralCheckup.DateToDet.dateAD), 'yyyy-MM-dd');
		}

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateGeneralCheckUp",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.NewGeneralCheckup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearGeneralCheckUp();
				$scope.GetAllGeneralCheckUpList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllGeneralCheckUpList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GeneralCheckUpList = [];

		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllGeneralCheckUp",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {


				$scope.GeneralCheckupList = [];

				var query = mx(res.data.Data).groupBy(t => t.MonthName);
				var sno = 1;
				angular.forEach(query, function (q) {
					var pare = {
						SNo: sno,
						MonthName: q.key,
						ChieldColl: []
					};

					angular.forEach(q.elements, function (el) {
						pare.ChieldColl.push(el);
					})
					$scope.GeneralCheckupList.push(pare);
					sno++;
				});


				//let mappingById = {}
			
				//let curData = res.data.Data;
				//curData.forEach((x) => {
				//	if (!(x.Month in mappingById))
				//		mappingById[x.Month] = []
				//	mappingById[x.Month].push(x);
				//})
				//$scope.GeneralCheckupListByMonth = mappingById;
				//$scope.GeneralCheckupListByMonthKeys = Object.keys($scope.GeneralCheckupListByMonth);
				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}


			if (res.data.IsSuccess && res.data.Data) {
				$scope.GeneralCheckUpList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetGeneralCheckUpId = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			GeneralCheckUpId: refData.GeneralCheckUpId
		};

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getGeneralCheckUpById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.NewGeneralCheckup = res.data.Data;

				if (!$scope.NewGeneralCheckup.GeneralCheckUpRepColl || $scope.NewGeneralCheckup.GeneralCheckUpRepColl.length == 0) {
					$scope.NewGeneralCheckup.GeneralCheckUpRepColl = [];
					$scope.NewGeneralCheckup.GeneralCheckUpRepColl.push({});
				}

				if ($scope.NewGeneralCheckup.DateFrom) {
					$scope.NewGeneralCheckup.DateFrom_TMP = new Date($scope.NewGeneralCheckup.DateFrom);
                }

				if ($scope.NewGeneralCheckup.DateTo) {
					$scope.NewGeneralCheckup.DateTo_TMP = new Date($scope.NewGeneralCheckup.DateTo);
                }

				//For Class
				if ($scope.NewGeneralCheckup.SelectGClassColl) {
					var clIdCOll = mx($scope.NewGeneralCheckup.SelectGClassColl);

					$scope.ClassList.forEach(function (cl) {
						if (clIdCOll.contains(cl.ClassId) == true)
							cl.IsGClassSelected = true;
						else
							cl.IsGClassSelected = false;
					});
				}

				//For Class
				if ($scope.NewGeneralCheckup.SelectGDiseaseColl) {
					var clIdCOll1 = mx($scope.NewGeneralCheckup.SelectGDiseaseColl);

					$scope.HealthIssueList.forEach(function (cl) {
						if (clIdCOll1.contains(cl.HealthIssueId) == true)
							cl.IsGDiseaseSelected = true;
						else
							cl.IsGDiseaseSelected = false;
					});
				}

				//For Test
				if ($scope.NewGeneralCheckup.SelectGTestColl) {
					var clIdCOll2 = mx($scope.NewGeneralCheckup.SelectGTestColl);

					$scope.TestNameList.forEach(function (cl) {
						if (clIdCOll2.contains(cl.TestNameId) == true)
							cl.IsGTestSelected = true;
						else
							cl.IsGTestSelected = false;
					});
				}

				//For Vaccine
				if ($scope.NewGeneralCheckup.SelectGVaccineColl) {
					var clIdCOll3 = mx($scope.NewGeneralCheckup.SelectGVaccineColl);

					$scope.VaccineList.forEach(function (cl) {
						if (clIdCOll3.contains(cl.VaccineId) == true)
							cl.IsGVaccineSelected = true;
						else
							cl.IsGVaccineSelected = false;
					});
				}


				$scope.NewGeneralCheckup.Mode = 'Modify';
				document.getElementById('scheduletable').style.display = "none";
				document.getElementById('CheckupScheduleform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelGeneralCheckUpById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					GeneralCheckUpId: refData.GeneralCheckUpId
				};

				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteGeneralCheckUp",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess == true) {
						$scope.GetAllGeneralCheckUpList();

					}
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.validateDates = function (changedField, obj) {
		if (!obj.DateFromDet || !obj.DateToDet ||
			!obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateFrom_TMP = new Date();
						obj.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateTo_TMP = new Date();
						obj.DateToDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
});
