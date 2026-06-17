app.controller('ExtraEntity', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Extra Entity';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();


		$scope.ForEntityColl = [{ id: 1, text: 'All' }, { id: 2, text: 'Student' }, { id: 3, text: 'Employee' }];
		//$scope.DataTypeColl = [{ id: 1, text: 'String' }, { id: 2, text: 'WholeNumberOnly' }, { id: 3, text: 'DecimalNumber' }, { id: 4, text: 'Date' }, { id: 5, text: 'DateTime' }, { id: 6, text: 'YesNo' }, { id: 7, text: 'Memo' }, { id: 8, text: 'List' }];
		$scope.IssueToColl = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }, { id: 3, text: 'Other' }];

		$scope.DataTypeColl = GlobalServices.getUDFTypes();

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		
		$scope.currentPages = {
			ExtraEntity: 1,
			CertificateIssue: 1,

		};

		$scope.searchData = {
			ExtraEntity: '',
			CertificateIssue: '',
		};

		$scope.perPage = {
			ExtraEntity: GlobalServices.getPerPageRow(),
			CertificateIssue: GlobalServices.getPerPageRow(),
		};

		$scope.beData = {
			TranId: 0,
			BranchId: null,
			Name: '',
			Description: '',
			For: null,
			Notes: '',
			ExtraEntityAttributeColl: [],
			Mode: 'Save'
		};

		$scope.beData.ExtraEntityAttributeColl.push({
			IsMandatory: false,
			DefaultValue: '',
			SelectOptions: '',
			DataType:1
		});


		$scope.newDet = {
			TranId: 0,
			AcademicYearId: null,
			AutoNumber: '',
			IssueTo: 1,
			EmployeeId: null,
			StudentId: null,
			Name: null,
			Address: '',
			DOB: '',
			IssueDate_TMP: new Date(),
			FatherName: '',
			MotherName: '',
			EmailId: '',
			Department: '',
			Designation: '',
			ClassName: '',
			SectionName: '',
			RollNo: '',
			RegdNo: '',
			Attributes: '',
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value, 
			Mode: 'Save'
		};

		$scope.GetAllExtraEntity();
		$scope.GetAllExtraCertificateIssue();
		//if ($scope.newDet.IssueTo) {
		//	$scope.ChangeForType();
		//}
	}


	$scope.ClearExtraEntity = function () {
		$scope.beData = {
			TranId: 0,
			BranchId: null,
			Name: '',
			Description: '',
			For: null,
			Notes: '',
			ExtraEntityAttributeColl: [],
			Mode: 'Save'
		};
		$scope.beData.ExtraEntityAttributeColl.push({
			IsMandatory: false,
			DefaultValue: '',
			SelectOptions: '',
			DataType: 1
		});
	};

	$scope.ClearExtraCertificate = function () {
		$scope.newDet = {
			TranId: 0,
			AcademicYearId: null,
			AutoNumber: '',
			IssueTo: 1,
			EmployeeId: null,
			StudentId: null,
			Name: null,
			Address: '',
			DOB: '',
			IssueDate_TMP: new Date(),
			FatherName: '',
			MotherName: '',
			EmailId: '',
			Department: '',
			Designation: '',
			ClassName: '',
			SectionName: '',
			RollNo: '',
			RegdNo: '',
			Attributes: '',
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};
		$scope.ChangeForType();
	};

	$scope.AddExtraEnt = function (ind) {
		if ($scope.beData.ExtraEntityAttributeColl) {
			if ($scope.beData.ExtraEntityAttributeColl.length > ind + 1) {
				$scope.beData.ExtraEntityAttributeColl.splice(ind + 1, 0, {
					IsMandatory: false,
					DefaultValue: '',
					SelectOptions: '',
					DataType: 1
				})
			} else {
				$scope.beData.ExtraEntityAttributeColl.push({
					IsMandatory: false,
					DefaultValue: '',
					SelectOptions: '',
					DataType: 1
				})
			}
		}
	};

	$scope.delExtraEnt = function (ind) {
		if ($scope.beData.ExtraEntityAttributeColl) {
			if ($scope.beData.ExtraEntityAttributeColl.length > 1) {
				$scope.beData.ExtraEntityAttributeColl.splice(ind, 1);
			}
		}
	};

	function OnClickDefault() {
		document.getElementById('ExtraEntity-form').style.display = "none";
		document.getElementById('Certificate-form').style.display = "none";

		document.getElementById('add-ExtratEntity').onclick = function () {
			document.getElementById('ExtraEntity-section').style.display = "none";
			document.getElementById('ExtraEntity-form').style.display = "block";
			$scope.ClearExtraEntity();
		}
		document.getElementById('back-Entity-btn').onclick = function () {
			document.getElementById('ExtraEntity-form').style.display = "none";
			document.getElementById('ExtraEntity-section').style.display = "block";
			$scope.ClearExtraEntity();
		}


		document.getElementById('add-CertificateIssue').onclick = function () {
			document.getElementById('Certificate-section').style.display = "none";
			document.getElementById('Certificate-form').style.display = "block";
		}
		document.getElementById('Certificate-Issue-btn').onclick = function () {
			document.getElementById('Certificate-form').style.display = "none";
			document.getElementById('Certificate-section').style.display = "block";
		}
	}


	//************************* ExtraEntity *********************************

	$scope.IsValidExtraEntity = function () {
		if ($scope.beData.Name.isEmpty()) {
			Swal.fire('Please ! Enter ExtraEntity Name');
			return false;
		}
		return true;
	};

	$scope.SaveExtraEntity = function () {
		if ($scope.IsValidExtraEntity() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.HealthCampaign.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExtraEntity();
					}
				});
			} else
				$scope.CallSaveUpdateExtraEntity();

		}
	};

	$scope.CallSaveUpdateExtraEntity = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();	

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveExtraEntity",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.beData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExtraEntity();
				$scope.GetAllExtraEntity();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllExtraEntity = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExtraEntityList = [];
		$scope.ExtraEntityList_Qry = [];
		$scope.ExtraEntityFilterList = [];
		$http({
			method: 'GET',
			url: base_url + "Academic/Transaction/GetAllExtraEntity",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExtraEntityList = res.data.Data;
				$scope.ExtraEntityList_Qry = mx(res.data.Data);

				$timeout(function () {
					$scope.ChangeForType();
				}, 0);
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ChangeForType = function () {
	
		$scope.ExtraEntityFilterList = [];
		if ($scope.newDet.IssueTo == 1) {
			var qry = $scope.ExtraEntityList_Qry.where(p1 => p1.For == 1 || p1.For == 2);
			angular.forEach(qry, function (q) {
				$scope.ExtraEntityFilterList.push(q);
			});
		} else if ($scope.newDet.IssueTo == 2) {
			var qry = $scope.ExtraEntityList_Qry.where(p1 => p1.For == 1 || p1.For == 3);
			angular.forEach(qry, function (q) {
				$scope.ExtraEntityFilterList.push(q);
			});
		} else if ($scope.newDet.IssueTo == 3) {
			var qry = $scope.ExtraEntityList_Qry.where(p1 => p1.For == 1 || p1.For == 2 || p1.For == 3);
			angular.forEach(qry, function (q) {
				$scope.ExtraEntityFilterList.push(q);
			});
		}
		
    }

	$scope.deleteExtraEntity = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteExtraEntity",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.ExtraEntityList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.ExtraEntityById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getExtraEntityById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.beData = res.data.Data;

					if (!$scope.beData.ExtraEntityAttributeColl || $scope.beData.ExtraEntityAttributeColl.length == 0) {
						$scope.beData.ExtraEntityAttributeColl = [];
						$scope.beData.ExtraEntityAttributeColl.push({
							IsMandatory: false,
							DefaultValue: '',
							SelectOptions: '',
							DataType: 1
						});
					}

					$scope.beData.Mode = 'Modify';
					document.getElementById('ExtraEntity-section').style.display = "none";
					document.getElementById('ExtraEntity-form').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

	//************************* ExtraCertificate Issue *********************************
	//$scope.IsValidExtraCertificate = function () {
	//	if ($scope.newDet.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter ExtraCertificateIssue Name');
	//		return false;
	//	}
	//	return true;
	//};

	$scope.GetExtraCertificateNo = function () {

		if ($scope.newDet.ExtraEntityId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'GET',
				url: base_url + "Academic/Transaction/getExtraCertificateNo?ExtraEntityId=" + $scope.newDet.ExtraEntityId,
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newDet.AutoNumber = res.data.Data.RId;

					//Added By Suresh on chaitra 8
					var udfFields = [];

					var findEntity = mx($scope.ExtraEntityList).firstOrDefault(p1 => p1.TranId == $scope.newDet.ExtraEntityId);
					if (findEntity) {
						angular.forEach(findEntity.ExtraEntityAttributeColl, function (udf) {
							var ud = {
								SNo: udf.SNo,
								Name: udf.Name,
								Value: udf.DefaultValue,
								FieldNo: udf.SNo,
								DisplayName: udf.Name,
								FieldType: udf.DataType,
								IsMandatory: udf.IsMandatory,
								Length: udf.MaxLen,
								SelectOptions: udf.SelectOptions
							};
							udfFields.push(ud);
						});
					}

					$scope.newDet.UDFFeildsColl = udfFields;
					//Ends
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		} else {
			$scope.newDet.AutoNumber = '';
        }
		
	}

	$scope.CurDues = null;
	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
	$scope.getDuesDetails = function () {
		$scope.CurDues = null; 
		if ($scope.newDet.StudentId && $scope.newDet.StudentId > 0) {
			var para = {
				StudentId: $scope.newDet.StudentId 
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentDetForTCCC",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var dt = res.data.Data;
					var sby = $scope.newDet.SelectStudent;

					$scope.newDet.Name = dt.Name;
					$scope.newDet.StudentId = dt.StudentId;
					$scope.newDet.EmployeeId = null;
					$scope.newDet.Address = dt.FullAddress;
					$scope.newDet.FatherName = dt.FatherName;
					$scope.newDet.MotherName = dt.MotherName;
					$scope.newDet.ContactNo = dt.FatherContactNo;
					$scope.newDet.EmailId = '';
					$scope.newDet.Department = '';
					$scope.newDet.Designation = '';
					$scope.newDet.ClassName = dt.ClassName;

					$scope.newDet.SectionName = dt.SectionName;
					$scope.newDet.RollNo = dt.RollNo;
					$scope.newDet.RegdNo = dt.RegdNo;

					if (dt.AdmitDate)
						$scope.newDet.AdmitDate = new Date(dt.AdmitDate);

					if (dt.DOB_AD)
						$scope.newDet.DOB = new Date(dt.DOB_AD);
					else
						$scope.newDet.DOB = null;

					var udfFields = [];

					var findEntity = mx($scope.ExtraEntityList).firstOrDefault(p1 => p1.TranId == $scope.newDet.ExtraEntityId);
					if (findEntity) {
						angular.forEach(findEntity.ExtraEntityAttributeColl, function (udf) {
							var ud = {
								SNo: udf.SNo,
								Name: udf.Name,
								Value: udf.DefaultValue,
								FieldNo: udf.SNo,
								DisplayName: udf.Name,
								FieldType: udf.DataType,
								IsMandatory: udf.IsMandatory,
								Length: udf.MaxLen,
								SelectOptions: udf.SelectOptions
							};
							udfFields.push(ud);
						});
                    }
					
					$scope.newDet.UDFFeildsColl = udfFields;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}


	$scope.AddExtraCertificate = function () {
	/*	if ($scope.IsValidExtraCertificate() == true) {*/
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExtraCertificate();
					}
				});
			} else
				$scope.CallSaveUpdateExtraCertificate();

		}
	/*};*/

	$scope.CallSaveUpdateExtraCertificate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		//Added By Suresh in case of Employee selection to save the name on chiatra 8
		if ($scope.newDet.EmployeeDetails) {
			$scope.newDet.Name = $scope.newDet.EmployeeDetails.Name;
		}

		if ($scope.newDet.IssueDateDet) {
			$scope.newDet.IssueDate = $filter('date')(new Date($scope.newDet.IssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newDet.IssueDate = new Date();

		//Ends

		var udfValues = '';
		var udfFields = [];
		angular.forEach($scope.newDet.UDFFeildsColl, function (udf) {
			var ud = {
				SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
				Name: udf.Name,
				Value: udf.UDFValue
			};
			udfFields.push(ud);
		});
		if (udfFields.length > 0)
			udfValues = JSON.stringify(udfFields);

		$scope.newDet.Attributes = udfValues;


		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveExtraCertificateIssue",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExtraCertificate();				
				$scope.Print(res.data.Data.RId);
				$scope.GetAllExtraCertificateIssue();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllExtraCertificateIssue = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExtraEntityList = [];
		$http({
			method: 'GET',
			url: base_url + "Academic/Transaction/GetAllExtraCertificateIssue",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CertificateIssueList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteCertificateIssue = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteExtraCertificateIssue",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.CertificateIssueList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.GetCertificateIssueById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getExtraCertificateIssueById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.newDet = res.data.Data;
					$scope.newDet.Mode = 'Modify';
					document.getElementById('Certificate-section').style.display = "none";
					document.getElementById('Certificate-form').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}


	$scope.Print = function (tranId) {
		if (tranId && tranId > 0) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=true",
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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&TranId=" + TranId + "&vouchertype=0";
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&TranId=" + TranId + "&vouchertype=0";
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



	//Added By Suresh on Chaitra 8 Starts
	$scope.GetEmpDetail = function () {

		if ($scope.newDet.EmployeeId && $scope.newDet.EmployeeId > 0) {
			var para = {
				EmployeeId: $scope.newDet.EmployeeId
			};

			$http({
				method: 'POST',
				url: base_url + "Infrastructure/Creation/getEmpShortDetById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					var dt = res.data.Data;

					$scope.newDet.Name = dt.Name;
					$scope.newDet.StudentId = null;
					$scope.newDet.EmployeeId = dt.EmployeeId; // Corrected this line
					$scope.newDet.Address = dt.Address;
					$scope.newDet.FatherName = dt.FatherName;
					$scope.newDet.MotherName = dt.MotherName;
					$scope.newDet.ContactNo = dt.OfficeContactNo;
					$scope.newDet.EmailId = dt.OfficeEmailId;
					$scope.newDet.Department = dt.Department;
					$scope.newDet.Designation = dt.Designation;
					$scope.newDet.ClassName = '';
					$scope.newDet.SectionName = '';
					$scope.newDet.RollNo = '';
					$scope.newDet.RegdNo = '';

					if (dt.DOB_AD) {
						$scope.newDet.DOB = new Date(dt.DOB_AD);
					} else {
						$scope.newDet.DOB = null;
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed: ' + reason);
			});
		}
	};
});

