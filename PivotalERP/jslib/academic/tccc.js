
app.controller('tcccController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'TC/CC';
	$rootScope.ChangeLanguage();
	$scope.LoadData = function () {
		$('.select2').select2(
			{
				allowClear: true,
				openOnEnter: true,
			}
		);
		$scope.ProvinceList = GetStateList();
		$scope.DistrictList = GetDistrictList();

		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.ReportTypeList = [{ id: 1, text: 'Pending' }, { id: 2, text: 'Done' }, { id: 3, text: 'All' },]

		$scope.currentPages = {
			Request: 1,
		};

		$scope.searchData = {
			Request: '',
		};

		$scope.perPage = {
			Request: GlobalServices.getPerPageRow(),
		};
		$scope.NationalityList = GlobalServices.getNationalityList();
		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.newTC = {
			ShowLeftStudent:true,
			TranId: null,
			StudentId:null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newCC = {
			ShowLeftStudent: true,
			TranId: null,
			StudentId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.StudentConfig = {};
		$scope.newReq = {
			ReportType:3,
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetStudentConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.StudentConfig = res.data.Data;

				$timeout(function () {
					$scope.newTC.ShowLeftStudent = $scope.StudentConfig.ShowLeftStudentinTC_CC;
					$scope.newCC.ShowLeftStudent = $scope.StudentConfig.ShowLeftStudentinTC_CC;
				});
			} 
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TCUDFFeildsColl = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetAllUDFFields?entityId="+tcEntityId,
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TCUDFFeildsColl = res.data.Data;

				angular.forEach($scope.TCUDFFeildsColl, function (td) {
					var colName = 'UDF.' + td.Name;
					$scope.gridOptions.columnDefs.push({ name: colName, displayName: td.DisplayName, minWidth: 110, headerCellClass: 'headerAligment' });
				});
			} 
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CCUDFFeildsColl = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetAllUDFFields?entityId=" + ccEntityId,
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CCUDFFeildsColl = res.data.Data;

				angular.forEach($scope.CCUDFFeildsColl, function (td) {
					var colName = 'UDF.' + td.Name;
					$scope.gridOptionscc.columnDefs.push({ name: colName, displayName: td.DisplayName, minWidth: 110, headerCellClass: 'headerAligment' });
				});

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.getStudentForTC = function () {

		if ($scope.newTC.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newTC.StudentId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetTCForEdit",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data)
				{
					var sby = $scope.newTC.SelectStudent;
					$scope.newTC = res.data.Data;
					$scope.newTC.SelectStudent = sby;

					if ($scope.newTC.AdmitDate)
						$scope.newTC.AdmitDate = new Date($scope.newTC.AdmitDate);

					if ($scope.newTC.DOB_AD)
						$scope.newTC.DOB_AD_TMP = new Date($scope.newTC.DOB_AD);

					if ($scope.newTC.IssueDate)
						$scope.newTC.IssueDate_TMP = new Date($scope.newTC.IssueDate);

					if ($scope.newTC.UDF) {						
						var udfFields = JSON.parse($scope.newTC.UDF);
						$scope.newTC.TCUDFFeildsColl = [];

						var tSNo = 1;
						angular.forEach(udfFields, function (f) {
							f.DisplayName = (f.Name ? f.Name : f.DisplayName);
							f.UDFValue = f.Value;
							f.SNo = (!f.SNo || f.SNo =='undefined' ? tSNo : f.SNo);
							$scope.newTC.TCUDFFeildsColl.push(f);
							tSNo++;
						});
						
                    }

					//Added on 2 Magh
					var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.Province);

					if (findProvince)
						$scope.newTC.ProvinceId = findProvince.id;
					else
						$scope.newTC.ProvinceId = null;

					var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.District);
					if (findDistrict)
						$scope.newTC.DistrictId = findDistrict.id;
					else
						$scope.newTC.DistrictId = null;

					var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.LocalLevel);
					if (findArea)
						$scope.newTC.LocalLevelId = findArea.id;
					else
						$scope.newTC.LocalLevelId = null;

							//Ends


				} else {

					$http({
						method: 'POST',
						url: base_url + "Academic/Transaction/GetStudentDetForTCCC",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res.data.IsSuccess && res.data.Data) {
							var sby = $scope.newTC.SelectStudent;
							$scope.newTC = res.data.Data;
							$scope.newTC.SelectStudent = sby;

							if ($scope.newTC.AdmitDate)
								$scope.newTC.AdmitDate = new Date($scope.newTC.AdmitDate);

							if ($scope.newTC.DOB_AD)
								$scope.newTC.DOB_AD_TMP = new Date($scope.newTC.DOB_AD);

							if ($scope.newTC.IssueDate)
								$scope.newTC.IssueDate_TMP = new Date($scope.newTC.IssueDate);

							//Added on 15 baishakjh 2083
							var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.Province);

							if (findProvince)
								$scope.newTC.ProvinceId = findProvince.id;
							else
								$scope.newTC.ProvinceId = null;

							var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.District);
							if (findDistrict)
								$scope.newTC.DistrictId = findDistrict.id;
							else
								$scope.newTC.DistrictId = null;

							var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.LocalLevel);
							if (findArea)
								$scope.newTC.LocalLevelId = findArea.id;
							else
								$scope.newTC.LocalLevelId = null;
							//Ends

							var udfFields = [];
							angular.forEach($scope.TCUDFFeildsColl, function (udf) {
								var ud = {
									SNo: udf.FieldNo,
									Name: udf.Name,
									Value: udf.UDFValue,
									FieldNo: udf.FieldNo,
									DisplayName: udf.DisplayName,
									IsMandatory: udf.IsMandatory
								};
								udfFields.push(ud);
							});
							$scope.newTC.TCUDFFeildsColl = udfFields;
							
						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			
		}
	};
	$scope.getStudentForCC = function () {

		if ($scope.newCC.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newCC.StudentId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetCCForEdit",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var sby = $scope.newCC.SelectStudent;
					$scope.newCC = res.data.Data;
					$scope.newCC.SelectStudent = sby;

					if ($scope.newCC.AdmitDate)
						$scope.newCC.AdmitDate = new Date($scope.newCC.AdmitDate);

					if ($scope.newCC.DOB_AD)
						$scope.newCC.DOB_AD_TMP = new Date($scope.newCC.DOB_AD);

					if ($scope.newCC.IssueDate)
						$scope.newCC.IssueDate_TMP = new Date($scope.newCC.IssueDate);


					if ($scope.newCC.UDF) {
						var udfFields = JSON.parse($scope.newCC.UDF);
						$scope.newCC.CCUDFFeildsColl = [];

						var tSNo = 1;
						angular.forEach(udfFields, function (f) {
							f.DisplayName = (f.Name ? f.Name : f.DisplayName);
							f.UDFValue = f.Value;
							f.SNo = (!f.SNo || f.SNo == 'undefined' ? tSNo : f.SNo);
							f.Name = f.Name;
							$scope.newCC.CCUDFFeildsColl.push(f);
							tSNo++;
						});

						

					}

					//Added on 2 Magh
					var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.Province);

					if (findProvince)
						$scope.newCC.ProvinceId = findProvince.id;
					else
						$scope.newCC.ProvinceId = null;

					var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.District);
					if (findDistrict)
						$scope.newCC.DistrictId = findDistrict.id;
					else
						$scope.newCC.DistrictId = null;

					var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.LocalLevel);
					if (findArea)
						$scope.newCC.LocalLevelId = findArea.id;
					else
						$scope.newCC.LocalLevelId = null;

							//Ends

				} else {
					$http({
						method: 'POST',
						url: base_url + "Academic/Transaction/GetStudentDetForCC",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res.data.IsSuccess && res.data.Data) {
							var sby = $scope.newCC.SelectStudent;
							$scope.newCC = res.data.Data;
							$scope.newCC.SelectStudent = sby;
							//Added
							var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.Province);

							if (findProvince)
								$scope.newCC.ProvinceId = findProvince.id;
							else
								$scope.newCC.ProvinceId = null;

							var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.District);
							if (findDistrict)
								$scope.newCC.DistrictId = findDistrict.id;
							else
								$scope.newCC.DistrictId = null;

							var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.LocalLevel);
							if (findArea)
								$scope.newCC.LocalLevelId = findArea.id;
							else
								$scope.newCC.LocalLevelId = null;
							//Ends

							if ($scope.newCC.AdmitDate)
								$scope.newCC.AdmitDate = new Date($scope.newCC.AdmitDate);

							if ($scope.newCC.DOB_AD)
								$scope.newCC.DOB_AD_TMP = new Date($scope.newCC.DOB_AD);

							if ($scope.newCC.IssueDate)
								$scope.newCC.IssueDate_TMP = new Date($scope.newCC.IssueDate);

							var udfFields = [];
							angular.forEach($scope.CCUDFFeildsColl, function (udf) {
								var ud = {
									SNo: udf.FieldNo,
									Name: udf.Name,
									Value: udf.UDFValue,
									FieldNo: udf.FieldNo,
									DisplayName: udf.DisplayName,
									IsMandatory: udf.IsMandatory
								};
								udfFields.push(ud);
							});
							$scope.newCC.CCUDFFeildsColl = udfFields;

							

						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		
		}
	};

	getterAndSetter();
	getterAndSettercc();
	//tc
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
				{ name: "AutoNumber", displayName: "Issue No.", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd.No", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issue Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "FatherContactNo", displayName: "Contact No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "FullAddress", displayName: "Address", minWidth: 240, headerCellClass: 'headerAligment' },
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 140,
					enableColumnResizing: false,
					cellTemplate: '<a href="" class="p-1" title="Select" ng-click="grid.appScope.PrintTC(row.entity.TranId)">' +
						'<i class="fas fa-print text-info" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Select" ng-click="grid.appScope.DelTCById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>'
				},
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
	};


	//cc

	function getterAndSettercc() {
		$scope.gridOptionscc = [];
		$scope.gridOptionscc = {
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
				{ name: "AutoNumber", displayName: "Issue No.", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd.No", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issue Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 240, headerCellClass: 'headerAligment' },
				{ name: "FatherContactNo", displayName: "Contact No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "FullAddress", displayName: "Address", minWidth: 240, headerCellClass: 'headerAligment' },
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 140,
					enableColumnResizing: false,
					cellTemplate: '<a href="" class="p-1" title="Select" ng-click="grid.appScope.PrintCC(row.entity.TranId)">' +
						'<i class="fas fa-print text-info" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Select" ng-click="grid.appScope.DelCCById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>'
				},

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

		//cc


	};


	$scope.IsValidTC = function () {
		if (!$scope.newTC.StudentId || $scope.newTC.StudentId==0) {
			Swal.fire('Please ! Select Valid Student Name');
			return false;
		}
		//Validation of UDF
		var invalidUDF = null;
		angular.forEach($scope.newTC.TCUDFFeildsColl, function (uf) {
			if (uf.IsMandatory && (!uf.UDFValue || uf.UDFValue.trim() === '')) {
				if (!invalidUDF) {
					invalidUDF = uf;
				}
			}
		});
		if (invalidUDF) {
			Swal.fire('Please ! Enter Valid ' + invalidUDF.DisplayName);			
			return false;
		}
		//Ends
		return true;
	}
	$scope.ClearTC = function ()
	{
		var sby = $scope.newTC.SelectStudent;
		$scope.newTC = {
			ShowLeftStudent: true,
		};
		$scope.newTC.SelectStudent = sby;
		$scope.newTC.Mode = 'Save';

		angular.forEach($scope.TCUDFFeildsColl, function (udf) {
			udf.UDFValue = '';
		});
    }
	$scope.SaveUpdateTC = function () {
		if ($scope.IsValidTC() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTC.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTC();
					}
				});
			} else
				$scope.CallSaveUpdateTC();

		}
	};

	$scope.CallSaveUpdateTC = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newTC.AdmitDateDet) {
			$scope.newTC.AdmitDate = $filter('date')(new Date($scope.newTC.AdmitDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newTC.DOB_ADDet) {
			$scope.newTC.DOB_AD = $filter('date')(new Date($scope.newTC.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newTC.IssueDateDet) {
			$scope.newTC.IssueDate = $filter('date')(new Date($scope.newTC.IssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newTC.IssueDate = new Date();

		var udfValues = '';
		var udfFields = [];
		angular.forEach($scope.newTC.TCUDFFeildsColl, function (udf) {
			var ud = {
				SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
				Name: udf.Name,
				Value: udf.UDFValue,
				IsMandatory: udf.IsMandatory
			};
			udfFields.push(ud);
		});
		if (udfFields.length > 0)
			udfValues = JSON.stringify(udfFields);

		$scope.newTC.UDF = udfValues;

		//Province District Starts
		var selectData1 = $('#cboProvince').select2('data');
		if (selectData1 && selectData1.length > 0)
			province1 = selectData1[0].text.trim();

		selectData1 = $('#cboDistrict').select2('data');
		if (selectData1 && selectData1.length > 0)
			district1 = selectData1[0].text.trim();


		selectData1 = $('#cboArea').select2('data');
		if (selectData1 && selectData1.length > 0)
			area1 = selectData1[0].text.trim();

		$scope.newTC.Province = province1;
		$scope.newTC.District = district1;
		$scope.newTC.LocalLevel = area1;
		//Ends

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveTC",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newTC }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			if (res.data.IsSuccess == true) {
				$scope.ClearTC();
				$scope.PrintTC(res.data.Data.RId);
			} else
				Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelTCById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelTC",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.getAllTCList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.PrintTC = function (tranId) {
		if (tranId && tranId > 0) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + tcEntityId+ "&voucherId=0&isTran=true",
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
												print = true;
												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + tcEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + tcEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
	};

	$scope.getAllTCList = function () {
		
		$scope.loadingstatus = "running";
		showPleaseWait();
		
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllTCList",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = res.data.Data;
				try {

					angular.forEach(dataColl, function (dc) {
						if (dc.UDF && dc.UDF.length > 0) {
							var udfColl = JSON.parse(dc.UDF);
							var strColl = '';
							angular.forEach(udfColl, function (ud) {
								if (strColl.length > 0)
									strColl = strColl + ',';

								strColl = strColl + '"' + ud.Name + '"' + ':"' + ud.Value + '"';
							});

							if (strColl.length > 0) {
								strColl = "{" + strColl + "}";
								dc.UDF = JSON.parse(strColl);
							}
						}
					});
				} catch { }

				
				$scope.gridOptions.data = dataColl;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	// CC


	$scope.IsValidCC = function () {
		if (!$scope.newCC.StudentId || $scope.newCC.StudentId == 0) {
			Swal.fire('Please ! Select Valid Student Name');
			return false;
		}
		//Validation of UDF
		var invalidCUDF = null;
		angular.forEach($scope.newCC.CCUDFFeildsColl, function (uf) {
			if (uf.IsMandatory && (!uf.UDFValue || uf.UDFValue.trim() === '')) {
				if (!invalidCUDF) {
					invalidCUDF = uf;
				}
			}
		});
		if (invalidCUDF) {
			Swal.fire('Please ! Enter Valid ' + invalidCUDF.DisplayName);
			return false;
		}
		//Ends

		return true;
	}
	$scope.ClearCC = function () {
		var sby = $scope.newCC.SelectStudent;
		$scope.newCC = {
			ShowLeftStudent: true,
		};
		$scope.newCC.SelectStudent = sby;
		$scope.newCC.Mode = 'Save';

		angular.forEach($scope.CCUDFFeildsColl, function (udf) {
			udf.UDFValue = '';
		});
	}
	$scope.SaveUpdateCC = function () {
		if ($scope.IsValidCC() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCC.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCC();
					}
				});
			} else
				$scope.CallSaveUpdateCC();

		}
	};

	$scope.CallSaveUpdateCC = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newCC.AdmitDateDet) {
			$scope.newCC.AdmitDate = $filter('date')(new Date($scope.newCC.AdmitDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newCC.DOB_ADDet) {
			$scope.newCC.DOB_AD = $filter('date')(new Date($scope.newCC.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newCC.IssueDateDet) {
			$scope.newCC.IssueDate = $filter('date')(new Date($scope.newCC.IssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newCC.IssueDate = new Date();

		var udfValues = '';
		var udfFields = [];
		angular.forEach($scope.newCC.CCUDFFeildsColl, function (udf) {
			var ud = {
				SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
				Name: udf.Name,
				Value: udf.UDFValue,
				IsMandatory: udf.IsMandatory
			};
			udfFields.push(ud);
		});
		if (udfFields.length > 0)
			udfValues = JSON.stringify(udfFields);

		$scope.newCC.UDF = udfValues;
		//Added on 2 Magh
		var selectData2 = $('#cboProvince1').select2('data');
		if (selectData2 && selectData2.length > 0)
			province2 = selectData2[0].text.trim();

		selectData2 = $('#cboDistrict1').select2('data');
		if (selectData2 && selectData2.length > 0)
			district2 = selectData2[0].text.trim();


		selectData2 = $('#cboArea1').select2('data');
		if (selectData2 && selectData2.length > 0)
			area2 = selectData2[0].text.trim();

		$scope.newCC.Province = province2;
		$scope.newCC.District = district2;
		$scope.newCC.LocalLevel = area2;

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveCC",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCC }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			if (res.data.IsSuccess == true) {
				$scope.ClearCC();
				$scope.PrintCC(res.data.Data.RId);
			} else
				Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelCCById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelCC",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.getAllCCList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.PrintCC = function (tranId) {
		if (tranId && tranId > 0) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + ccEntityId + "&voucherId=0&isTran=true",
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
												print = true;
												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ccEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ccEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
	};

	$scope.getAllCCList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllCCList",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = res.data.Data;

				try {

					angular.forEach(dataColl, function (dc) {
						if (dc.UDF && dc.UDF.length > 0) {
							var udfColl = JSON.parse(dc.UDF);
							var strColl = '';
							angular.forEach(udfColl, function (ud) {
								if (strColl.length > 0)
									strColl = strColl + ',';

								strColl = strColl + '"' + ud.Name + '"' + ':"' + ud.Value + '"';
							});

							if (strColl.length > 0) {
								strColl = "{" + strColl + "}";
								dc.UDF = JSON.parse(strColl);
							}
						}
					});


				} catch { }

				
				$scope.gridOptionscc.data = dataColl;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	$scope.getTCRequestLst = function () {
		$scope.RequestList = [];
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ReportType: $scope.newReq.ReportType
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllTCRequest",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RequestList = res.data.Data;		 
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});