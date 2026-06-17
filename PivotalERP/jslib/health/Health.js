app.controller('HealthController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Health';

	OnClickDefault();
	$scope.LoadData = function ()
	{
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.CountryList = GlobalServices.getCountryList();
		$scope.DisablityList = GlobalServices.getDisablityList();

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.IssueToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Teacher' }];		
		$scope.EmployeeColl = [{ Id: 1, text: 'Suresh Poudel' }, { Id: 2, text: 'Rukesh Gawacchi' }, { Id: 3, text: 'Aakash Ghimire' }, { Id: 4, text: 'Shushant Pandey' }];
		$scope.StudentsColl = [{ Id: 1, text: 'Bikash Jaiswal' }, { Id: 2, text: 'Sagar Gosawami' }, { Id: 3, text: 'Raj Gupta' }, { Id: 4, text: 'Aayush Singh' }];

 

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CampaignColl = [];
		$http({
			method: 'GET',
			url: base_url + "Academic/Transaction/GetAllHealthCampaign",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CampaignColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			MonthlyTest: 1,
			HealthGrowth: 1,
			stoolTest: 1,
			urineTest: 1,
			genHealth: 1,

		};

		$scope.searchData = {
			MonthlyTest: '',
			HealthGrowth: '',
			stoolTest: '',
			urineTest: '',
			genHealth: '',
		};

		$scope.perPage = {
			MonthlyTest: GlobalServices.getPerPageRow(),
			HealthGrowth: GlobalServices.getPerPageRow(),
			stoolTest: GlobalServices.getPerPageRow(),
			urineTest: GlobalServices.getPerPageRow(),
			genHealth: GlobalServices.getPerPageRow(),
		};

		$scope.MonthlyTest = {
			TranId: 0,
			Month: null,
			Teeth: '',
			Nail: '',
			//IssueTo: 1,
			//SelectStudent: 1,
			//SelectEmployee: 1,
			//SelectStudent: $scope.StudentSearchOptions[0].value,
			//SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Cleanliness: '',
			AttachMonTesColl: [],
			Mode: 'Save'
		};

		$scope.HealthGrowth = {
			TranId: 0,
			Height: '',
			Weight: '',
			TestDate_TMP: new Date(),
			TestBy: '',
			Remarks: '',
			AttachHelGroColl: [],
			Mode: 'Save'
		};

		$scope.stoolTest = {
			TranId: 0,
			TestDate_TMP: new Date(),
			Color: '',
			Mucus: '',
			Puscell: '',
			RBC: '',
			Cyst: '',
			Ova: '',
			Others: '',
			AttachStTeColl: [],
			Mode: 'Save'
		};

		$scope.urineTest = {
			TranId: 0,
			TestDate_TMP: new Date(),
			Color: '',
			Protein: '',
			Sugar: '',
			Transparency: '',
			WBC: '',
			RBC: '',
			Others: '',
			AttachUrTeColl: [],
			Mode: 'Save'
		};

		$scope.genHealth = {
			TranId: 0,
			ForDate_TMP: new Date(),
			CampaignProgram: '',
			Remarks: '',
			AttachGenHeColl: [],
			Mode: 'Save'
		};

		$scope.HealthRpt = {
			ReportFor: 1,
			StudentId: null,
			EmployeeId: null,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			SelectStudent: $scope.StudentSearchOptions[0].value
		};

		//$scope.GetAllMonthlyTest();
		//$scope.GetAllHealthGrowth();
		//$scope.GetAllStoolTest();
		//$scope.GetAllUrineTest();
		//$scope.GetAllGeneralHealth();
	}


	$scope.HealthRptFor = function () {

		$scope.MonthlyColl = [];
		$scope.HealthGrowthColl = [];
		$scope.stoolTestColl = [];
		$scope.UrineTestColl = [];
		$scope.GeneralHealthColl = [];

		if ($scope.HealthRpt.ReportFor == 1)
			$scope.HealthRpt.EmployeeId = null;
		else
			$scope.HealthRpt.StudentId = null;


		showPleaseWait();
		$scope.MonthlyColl = [];
		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetHealthRpt",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				var dt = res.data.Data;
				$scope.MonthlyColl = dt.MonthlyTestColl;
				$scope.HealthGrowthColl = dt.HealthGrowthColl;
				$scope.stoolTestColl = dt.StoolTestColl;
				$scope.UrineTestColl = dt.UrineTestColl;
				$scope.GeneralHealthColl = dt.GeneralHealthColl;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

    }

	/*-----------/Load Function Ends-------------- */

	function OnClickDefault() {
		document.getElementById('monthform').style.display = "none";
		document.getElementById('growthform').style.display = "none";
		document.getElementById('stoolform').style.display = "none";
		document.getElementById('urineform').style.display = "none";
		document.getElementById('generalform').style.display = "none";

		document.getElementById('add-growth').onclick = function () {
			document.getElementById('growthtable').style.display = "none";
			document.getElementById('growthform').style.display = "block";
			$scope.ClearHealthGrowth();
		}
		document.getElementById('backgrowth').onclick = function () {
			document.getElementById('growthtable').style.display = "block";
			document.getElementById('growthform').style.display = "none";
			$scope.ClearHealthGrowth();
		}


		document.getElementById('add-stool').onclick = function () {
			document.getElementById('stooltable').style.display = "none";
			document.getElementById('stoolform').style.display = "block";
			$scope.ClearStoolTest();
		}
		document.getElementById('backstool').onclick = function () {
			document.getElementById('stooltable').style.display = "block";
			document.getElementById('stoolform').style.display = "none";
			$scope.ClearStoolTest();
		}

		document.getElementById('add-urine').onclick = function () {
			document.getElementById('urinetable').style.display = "none";
			document.getElementById('urineform').style.display = "block";
			$scope.ClearUrineTest();
		}
		document.getElementById('backurine').onclick = function () {
			document.getElementById('urinetable').style.display = "block";
			document.getElementById('urineform').style.display = "none";
			$scope.ClearUrineTest();
		}

		document.getElementById('add-general').onclick = function () {
			document.getElementById('generaltable').style.display = "none";
			document.getElementById('generalform').style.display = "block";
			$scope.ClearGeneralHealth();
		}
		document.getElementById('backgeneral').onclick = function () {
			document.getElementById('generaltable').style.display = "block";
			document.getElementById('generalform').style.display = "none";
			$scope.ClearGeneralHealth();
		}

		document.getElementById('add-month').onclick = function () {
			document.getElementById('monthtable').style.display = "none";
			document.getElementById('monthform').style.display = "block";
			$scope.ClearMonthlyTest();
		}
		document.getElementById('backmonth').onclick = function () {
			document.getElementById('monthtable').style.display = "block";
			document.getElementById('monthform').style.display = "none";
			$scope.ClearMonthlyTest();
		}
	}

	$scope.ClearMonthlyTest = function () {
		$scope.MonthlyTest = {
			TranId: 0,
			Month: null,
			Teeth: '',
			Nail: '',
			Cleanliness: '',
			//IssueTo: 1,
			//SelectStudent: 1,
			//SelectEmployee: 1,
			//SelectStudent: $scope.StudentSearchOptions[0].value,
			//SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			AttachMonTesColl: [],
			Mode: 'Save'
		};
	}

	$scope.ClearHealthGrowth = function () {
		$scope.HealthGrowth = {
			TranId: 0,
			Height: '',
			Weight: '',
			TestDate_TMP: new Date(),
			TestBy: '',
			Remarks: '',
			AttachHelGroColl: [],
			Mode: 'Save'
		};
	}

	$scope.ClearStoolTest = function () {
		$scope.stoolTest = {
			TranId: 0,
			TestDate_TMP: new Date(),
			Color: '',
			Mucus: '',
			Puscell: '',
			RBC: '',
			Cyst: '',
			Ova: '',
			Others: '',
			AttachStTeColl: [],
			Mode: 'Save'
		};
	}

	$scope.ClearUrineTest = function () {
		$scope.urineTest = {
			TranId: 0,
			TestDate_TMP: new Date(),
			Color: '',
			Protein: '',
			Sugar: '',
			Transparency: '',
			WBC: '',
			RBC: '',
			Others: '',
			AttachUrTeColl: [],
			Mode: 'Save'
		};
	}

	$scope.ClearGeneralHealth = function () {
		$scope.genHealth = {
			TranId: 0,
			ForDate_TMP: new Date(),
			CampaignProgram: '',
			Remarks: '',
			AttachGenHeColl: [],
			Mode: 'Save'
		};
	}


	/*-----------------Monthly Test Tab Starts--------------------------*/
	$scope.delAttachMonthlyTest = function (ind) {
		if ($scope.MonthlyTest.AttachMonTesColl) {
			if ($scope.MonthlyTest.AttachMonTesColl.length > 0) {
				$scope.MonthlyTest.AttachMonTesColl.splice(ind, 1);
			}
		}
	}

	$scope.AddFilesMonthlyTest = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.MonthlyTest.AttachMonTesColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.IsValidMonthlyTest = function () {
		if ($scope.MonthlyTest.Nail.isEmpty()) {
			Swal.fire('Please ! Enter Monthly Nail Condition');
			return false;
		}

		if (!$scope.HealthRpt.StudentId && !$scope.HealthRpt.EmployeeId) {
			Swal.fire('Please ! Select Student/Employee Name');
			return false;
		}

		return true;
	}

	$scope.AddMonthlyTest = function () {
		if ($scope.IsValidMonthlyTest() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.MonthlyTest.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMonthlyTest();
					}
				});
			} else
				$scope.CallSaveUpdateMonthlyTest();

		}
	};

	$scope.CallSaveUpdateMonthlyTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.MonthlyTest.AttachMonTesColl;

		$scope.MonthlyTest.StudentId = $scope.HealthRpt.StudentId;
		$scope.MonthlyTest.EmployeeId = $scope.HealthRpt.EmployeeId;

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveMonthlyTest",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.MonthlyTest, files: filesColl}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearMonthlyTest();
				$scope.GetAllMonthlyTest();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllMonthlyTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MonthlyColl = [];
		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllMonthlyTest",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MonthlyColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteMonthlyTest = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Nail + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteMonthlyTest",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.MonthlyColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getMonthlyTestById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getMonthlyTestById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.MonthlyTest = res.data.Data;
					$scope.MonthlyTest.Mode = 'Modify';
					document.getElementById('monthtable').style.display = "none";
					document.getElementById('monthform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

	$scope.ShowPersonalImg = function (docDet) {
		$scope.viewImg = {
			ContentPath: '',
			File: null,
			FileData: null
		};
		if (docDet.attachFile || docDet.File) {
			$scope.viewImg.ContentPath = docDet.attachFile;
			$scope.viewImg.File = docDet.File;
			$scope.viewImg.FileData = docDet.DocumentData;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');
	};

	/*-----------------------Health Growth Tab Starts------------------------*/
	$scope.delAttachHealthGrowth = function (ind) {
		if ($scope.HealthGrowth.AttachHelGroColl) {
			if ($scope.HealthGrowth.AttachHelGroColl.length > 0) {
				$scope.HealthGrowth.AttachHelGroColl.splice(ind, 1);
			}
		}
	}

	$scope.AddFilesHealthGrowth = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.HealthGrowth.AttachHelGroColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.IsValidHealthGrowth = function () {
		if ($scope.HealthGrowth.Height.isEmpty()) {
			Swal.fire('Please ! Enter Height of the Students');
			return false;
		}

		if (!$scope.HealthRpt.StudentId && !$scope.HealthRpt.EmployeeId) {
			Swal.fire('Please ! Select Student/Employee Name');
			return false;
        }

		return true;
	}

	$scope.AddHealthGrowth = function () {
		if ($scope.IsValidHealthGrowth() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.HealthGrowth.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHealthGrowth();
					}
				});
			} else
				$scope.CallSaveUpdateHealthGrowth();

		}
	};

	$scope.CallSaveUpdateHealthGrowth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.HealthGrowth.AttachHelGroColl;

		$scope.HealthGrowth.StudentId = $scope.HealthRpt.StudentId;
		$scope.HealthGrowth.EmployeeId = $scope.HealthRpt.EmployeeId;

		if ($scope.HealthGrowth.TestDateDet) {
			$scope.HealthGrowth.TestDate = $filter('date')(new Date($scope.HealthGrowth.TestDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.HealthGrowth.TestDate = $filter('date')(new Date(), 'yyyy-MM-dd'); 

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveHealthGrowth",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.HealthGrowth, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHealthGrowth();
				$scope.GetAllHealthGrowth();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHealthGrowth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HealthGrowthColl = [];
		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllHealthGrowth",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthGrowthColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteHealthGrowth = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Height + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteHealthGrowth",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.HealthGrowthColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getHealthGrowthById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getHealthGrowthById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.HealthGrowth = res.data.Data;
					$scope.HealthGrowth.Mode = 'Modify';
					document.getElementById('growthtable').style.display = "none";
					document.getElementById('growthform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

	/*-----------------------/Health Growth Tab End------------------------*/

	/*-----------------------Stool Test Tab Starts------------------------*/

	$scope.delAttachStoolTest = function (ind) {
		if ($scope.stoolTest.AttachStTeColl) {
			if ($scope.stoolTest.AttachStTeColl.length > 0) {
				$scope.stoolTest.AttachStTeColl.splice(ind, 1);
			}
		}
	}

	$scope.AddFilesStoolTest = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.stoolTest.AttachStTeColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.IsValidStoolTest = function () {
		if ($scope.stoolTest.Color.isEmpty()) {
			Swal.fire('Please ! Enter Color of the Student Stool');
			return false;
		}

		if (!$scope.HealthRpt.StudentId && !$scope.HealthRpt.EmployeeId) {
			Swal.fire('Please ! Select Student/Employee Name');
			return false;
		}

		return true;
	}

	$scope.AddStoolTest = function () {
		if ($scope.IsValidStoolTest() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.stoolTest.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStoolTest();
					}
				});
			} else
				$scope.CallSaveUpdateStoolTest();

		}
	};

	$scope.CallSaveUpdateStoolTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.stoolTest.AttachStTeColl;

		if ($scope.stoolTest.TestDateDet) {
			$scope.stoolTest.TestDate = $filter('date')(new Date($scope.stoolTest.TestDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.stoolTest.TestDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$scope.stoolTest.StudentId = $scope.HealthRpt.StudentId;
		$scope.stoolTest.EmployeeId = $scope.HealthRpt.EmployeeId;

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveStoolTest",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.stoolTest, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStoolTest();
				$scope.GetAllStoolTest();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStoolTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.stoolTestColl = [];
		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllStoolTest",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.stoolTestColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteStoolTest = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Color + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteStoolTest",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.stoolTestColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getStoolTestById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getStoolTestById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.stoolTest = res.data.Data;
					$scope.stoolTest.Mode = 'Modify';
					document.getElementById('stooltable').style.display = "none";
					document.getElementById('stoolform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

	/*-----------------------/Stool Test Tab Ends------------------------*/

	/*-----------------------Urine Test Tab Starts ------------------------*/
	$scope.delAttachUrineTest = function (ind) {
		if ($scope.urineTest.AttachStTeColl) {
			if ($scope.urineTest.AttachUrTeColl.length > 0) {
				$scope.urineTest.AttachUrTeColl.splice(ind, 1);
			}
		}
	}

	$scope.AddFilesUrineTest = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.urineTest.AttachUrTeColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.IsValidurineTest = function () {
		if ($scope.urineTest.Protein.isEmpty()) {
			Swal.fire('Please ! Enter Protien Level of Student');
			return false;
		}

		if (!$scope.HealthRpt.StudentId && !$scope.HealthRpt.EmployeeId) {
			Swal.fire('Please ! Select Student/Employee Name');
			return false;
		}

		return true;
	}

	$scope.AddUrineTest = function () {
		if ($scope.IsValidurineTest() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.urineTest.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUrineTest();
					}
				});
			} else
				$scope.CallSaveUpdateUrineTest();

		}
	};

	$scope.CallSaveUpdateUrineTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.urineTest.AttachUrTeColl;


		$scope.urineTest.StudentId = $scope.HealthRpt.StudentId;
		$scope.urineTest.EmployeeId = $scope.HealthRpt.EmployeeId;

		if ($scope.urineTest.TestDateDet) {
			$scope.urineTest.TestDate = $filter('date')(new Date($scope.urineTest.TestDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.urineTest.TestDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveUrineTest",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.urineTest, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUrineTest();
				$scope.GetAllUrineTest();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUrineTest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UrineTestColl = [];

		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllUrineTest",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UrineTestColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteUrineTest = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Color + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteUrineTest",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.UrineTestColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getUrineTestById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getUrineTestById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.urineTest = res.data.Data;
					$scope.urineTest.Mode = 'Modify';
					document.getElementById('urinetable').style.display = "none";
					document.getElementById('urineform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

	/*-----------------------/Urine Test Tab Ends ------------------------*/

	/*----------------------------General Health Tab Starts---------------------*/

	$scope.delAttachGeneralHealth = function (ind) {
		if ($scope.genHealth.AttachGenHeColl) {
			if ($scope.genHealth.AttachGenHeColl.length > 0) {
				$scope.genHealth.AttachGenHeColl.splice(ind, 1);
			}
		}
	}

	$scope.AddFilesGeneralHealth = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.genHealth.AttachGenHeColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.IsValidGeneralHealth = function () {
		if ($scope.genHealth.Remarks.isEmpty()) {
			Swal.fire('Please ! Enter Remarks for the Health of Students');
			return false;
		}

		if (!$scope.HealthRpt.StudentId && !$scope.HealthRpt.EmployeeId) {
			Swal.fire('Please ! Select Student/Employee Name');
			return false;
		}

		return true;
	}

	$scope.AddGeneralHealth = function () {
		if ($scope.IsValidGeneralHealth() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.genHealth.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGeneralHealth();
					}
				});
			} else
				$scope.CallSaveUpdateGeneralHealth();

		}
	};

	$scope.CallSaveUpdateGeneralHealth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.genHealth.AttachGenHeColl;

		if ($scope.genHealth.ForDateDet) {
			$scope.genHealth.ForDate = $filter('date')(new Date($scope.genHealth.ForDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.genHealth.ForDate = $filter('date')(new Date(), 'yyyy-MM-dd');


		$scope.genHealth.StudentId = $scope.HealthRpt.StudentId;
		$scope.genHealth.EmployeeId = $scope.HealthRpt.EmployeeId;

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveGeneralHealth",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.genHealth, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearGeneralHealth();
				$scope.GetAllGeneralHealth();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllGeneralHealth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GeneralHealthColl = [];

		var para = {
			StudentId: $scope.HealthRpt.StudentId,
			EmployeeId: $scope.HealthRpt.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllGeneralHealth",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GeneralHealthColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteGeneralHealth = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Remarks + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteGeneralHealth",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GeneralHealthColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getGeneralHealthById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getGeneralHealthById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.genHealth = res.data.Data;
					$scope.genHealth.Mode = 'Modify';
					document.getElementById('generaltable').style.display = "none";
					document.getElementById('generalform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}


	/*----------------------------/General Health Tab End---------------------*/

});