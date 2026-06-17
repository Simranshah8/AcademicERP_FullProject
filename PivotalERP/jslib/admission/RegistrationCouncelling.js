app.controller('RegistrationCouncellingController', function ($scope, $http, $timeout, $filter, GlobalServices, FileSaver) {
	$scope.Title = 'Registration Councelling';
	//OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2(
			{
				allowClear: true,
				openOnEnter: true,
			}
		);
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			AdmissionReceipt: 1,
			AdmissionConfirmed: 1,
			Followup: 1,
			TodaysFollowup: 1,
			PendingFollowup: 1,
			UpcomingFollowup: 1,
			FollowupNotRequired: 1
		};

		$scope.searchData = {
			AdmissionReceipt: '',
			AdmissionConfirmed: '',
			Followup: '',
			TodaysFollowup: '',
			PendingFollowup: '',
			UpcomingFollowup: '',
			FollowupNotRequired: ''
		};

		$scope.newAllowEntity = {
			AdmissionReceipt: null,
			AdmissionConfirmed: null,
		};
		$scope.perPage = {
			AdmissionReceipt: GlobalServices.getPerPageRow(),
			AdmissionConfirmed: GlobalServices.getPerPageRow(),
			Followup: GlobalServices.getPerPageRow(),
			TodaysFollowup: GlobalServices.getPerPageRow(),
			PendingFollowup: GlobalServices.getPerPageRow(),
			UpcomingFollowup: GlobalServices.getPerPageRow(),
			FollowupNotRequired: GlobalServices.getPerPageRow(),
		};

		$scope.DiscountTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllDiscountTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DiscountTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newFeeConfiguration = {};
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetFeeConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeConfiguration = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SourceList = [];
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSourceList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SourceList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CommunicationTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/creation/GetAllCommunicationTypeList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CommunicationTypeList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAdmitStudentList();
		//$scope.GetAllFollowupList();
	}
	$scope.chkCheckAll = false;
	$scope.CheckUnCheckAll = function () {
		angular.forEach($scope.StudentForReceiptColl, function (es) {
			es.IsSelected = $scope.chkCheckAll;
		});
	}
	$scope.GetAdmitStudentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AdmitStudentList = [];

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetRegAdmitStudent",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AdmitStudentList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FollowupList = [];
		$scope.TodaysFollowupList = [];
		$scope.PendingFollowupList = [];
		$scope.UpcomingFollowupList = [];
		$scope.FollowupNotRequiredList = [];

		var para = {
			FollowupType: 0
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetAdmissionFollowup",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FollowupList = res.data.Data;

				angular.forEach(res.data.Data, function (d) {

					if (d.Status == 5) {
						$scope.FollowupNotRequiredList.push(d);
					} else {
						if (d.FollowupType == 1)
							$scope.TodaysFollowupList.push(d);
						else if (d.FollowupType == 2)
							$scope.PendingFollowupList.push(d);
						else if (d.FollowupType == 3)
							$scope.UpcomingFollowupList.push(d);
					}

				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	function OnClickDefault() {
		document.getElementById('admissionfeereceipt').style.display = 'none';

		//document.getElementById('admissionrecipte').onclick = function () {
		//	document.getElementById('admissionfeereceipt').style.display = 'block';
		//	document.getElementById('admissionconfirmationtable').style.display = 'none';
		//}
		document.getElementById('backbtntwo').onclick = function () {
			document.getElementById('admissionfeereceipt').style.display = 'none';
			document.getElementById('admissionconfirmationtable').style.display = 'block';
		}


	}
	$scope.GetStudentForReceipt = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentForReceiptColl = [];
		$http({
			method: 'POST',
			url: base_url + "admissionmanagement/creation/GetRegSummaryForAdmitConfirm",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				$scope.StudentForReceiptColl = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.admissionReceiptGen = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newFeeReceipt = {};
		$scope.newFeeReceipt.TranId = refData.EligibleTranId;
		$scope.newFeeReceipt.ReceiptAsLedgerId = 1;
		$scope.newFeeReceipt.Name = refData.Name;
		$scope.newFeeReceipt.EnquiryNo = refData.EnquiryNo;
		$scope.newFeeReceipt.ClassName = refData.ClassName;
		$scope.newFeeReceipt.SectionName = refData.SectionName;
		$scope.newFeeReceipt.FatherName = refData.FatherName;
		$scope.newFeeReceipt.F_ContactNo = refData.F_ContactNo;
		$scope.newFeeReceipt.Address = refData.Address;
		$scope.newFeeReceipt.Name = refData.Name;
		$scope.newFeeReceipt.StudentType = refData.StudentType;

		var para = {
			StudentId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetRegistrationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var newDet = res.data.Data;
				$scope.newFeeReceipt.StudentId = newDet.StudentId;
				$scope.newFeeReceipt.PhotoPath = newDet.PhotoPath;
				$scope.newFeeReceipt.ClassPreferredForId = newDet.Eligibility.ClassPreferredForId;

				var para1 = {
					ForId: 1,
					ClassId: newDet.Eligibility.ClassPreferredForId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/GetFeeItemFor",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res1) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res1.data.IsSuccess == true) {
						$scope.newFeeReceipt.FeeItemColl = res1.data.Data;

						var findData = mx($scope.newFeeReceipt.FeeItemColl);
						$scope.newFeeReceipt.Qty = findData.sum(p1 => p1.Qty);
						$scope.newFeeReceipt.Rate = 0;
						$scope.newFeeReceipt.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
						$scope.newFeeReceipt.PayableAmt = findData.sum(p1 => p1.PayableAmt);

						document.getElementById('admissionfeereceipt').style.display = 'block';
						document.getElementById('admissionconfirmationtable').style.display = 'none';

					} else {
						Swal.fire(res1.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ChangeDiscountType = function (discountTypeId) {

		if (!discountTypeId || discountTypeId < 1)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DiscountTypeId: discountTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetDiscountTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var detColl = mx(res.data.Data.DetailsColl);
				angular.forEach($scope.newFeeReceipt.FeeItemColl, function (fi) {
					fi.DiscountPer = 0;
					fi.DiscountAmt = 0;
					var findFee = detColl.firstOrDefault(p1 => p1.FeeItemId == fi.FeeItemId);
					if (findFee) {
						if (findFee.DiscountAmt > 0) {
							fi.DiscountAmt = findFee.DiscountAmt;
							$scope.ChangeRate(fi, 4);
						}
						else if (findFee.DiscountPer > 0) {
							fi.DiscountPer = findFee.DiscountPer;
							$scope.ChangeRate(fi, 3);
						}
						else {
							$scope.ChangeRate(fi, 3);
						}
					}

				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ChangeRate = function (det, col) {
		if (col == 1 || col == 2) {
			det.PayableAmt = (det.Qty * det.Rate) - det.DiscountAmt;
		} else if (col == 3) {
			var disAmt = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountPer > 0) {
				disAmt = amt * det.DiscountPer / 100;
			}

			det.DiscountAmt = disAmt;
			det.PayableAmt = amt - disAmt;
		} else if (col == 4) {
			var disPer = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountAmt > 0) {
				disPer = (det.DiscountAmt / amt) * 100;
			}
			det.DiscountPer = disPer;
			det.PayableAmt = amt - det.DiscountAmt;
		}
		var findData = mx($scope.newFeeReceipt.FeeItemColl);
		$scope.newFeeReceipt.Qty = findData.sum(p1 => p1.Qty);
		$scope.newFeeReceipt.Rate = 0;
		$scope.newFeeReceipt.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
		$scope.newFeeReceipt.PayableAmt = findData.sum(p1 => p1.PayableAmt);

	};

	$scope.SaveReceipt = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveEligibleFeeReceipt",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFeeReceipt }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$('#followup').modal('hide');

				document.getElementById('admissionfeereceipt').style.display = 'none';
				document.getElementById('admissionconfirmationtable').style.display = 'block';

				$scope.GetStudentForReceipt();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DownloadReceipt = function (tranId) {
		if ((tranId || tranId > 0)) {

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + ReceiptEntityId + "&voucherId=0&isTran=true",
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
												var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ReceiptEntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
												var req = {
													url: printURL,
													method: 'GET',
													responseType: 'arraybuffer'
												};
												$http(req).then(function (resp) {
													var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
													hidePleaseWait();
													$scope.loadingstatus = "stop";
													FileSaver.saveAs(serverData, 'receipt-form.pdf');
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
							var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ReceiptEntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
							var req = {
								url: printURL,
								method: 'GET',
								responseType: 'arraybuffer'
							};
							$http(req).then(function (resp) {
								var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								FileSaver.saveAs(serverData, 'receipt-form.pdf');
							});
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	};

	$scope.newFollowup = {};
	$scope.openFollowup = function (beData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newFollowup = {
			StudentId: beData.EligibleTranId,
			AutoNumber: beData.EnquiryNo,
			Name: beData.Name,
			RegdNo: beData.AutoManualNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
		};

		var para = {
			TranId: beData.EligibleTranId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetAdmitFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followup').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.curFollowup = {};
	$scope.closeFollowup = function (beData, statusId) {
		$scope.curFollowup = beData;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var statusStr = '';
		if (statusId == 3) {
			statusStr = 'Hold'
		} else if (statusId == 4)
			statusStr = 'Resumed';
		else if (statusId == 5)
			statusStr = 'Rejected';
		else if (statusId == 8)
			statusStr = 'Approved';

		$scope.newFollowup = {
			TranId: beData.EligibleTranId,
			AutoNumber: beData.AutoNumber,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
			Status: statusId,
			StatusStr: statusStr,
			StatusRemarks: '',
		};

		var para = {
			TranId: beData.EligibleTranId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetRegFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followupClosed').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.SaveUpdateFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newFollowup.PaymentDueDateDet && $scope.newFollowup.PaymentDueDateDet.dateAD) {
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date($scope.newFollowup.PaymentDueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newFollowup.NextFollowupDateDet && $scope.newFollowup.NextFollowupDateDet.dateAD) {
			$scope.newFollowup.NextFollowupDate = $filter('date')(new Date($scope.newFollowup.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newFollowup.NextFollowupTime_TMP)
			$scope.newFollowup.NextFollowupTime = $scope.newFollowup.NextFollowupTime_TMP.toLocaleString();
		else
			$scope.newFollowup.NextFollowupTime_TMP = null;

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveAdmitFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$('#followup').modal('hide');
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.SaveFollowupClosed = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: $scope.newFollowup.TranId,
			Status: $scope.newFollowup.Status,
			Remarks: $scope.newFollowup.StatusRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveAdmitStatus",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.curFollowup.Status = $scope.newFollowup.Status;
				$scope.curFollowup.StatusRemarks = $scope.newFollowup.StatusRemarks;
				$('#followupClosed').modal('hide');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.SMSTemplateChanged = function (st) {
		$scope.CurSMSSend.Temlate = st;
		$scope.CurSMSSend.Description = st.Description;
	}
	$scope.EmailTemplateChanged = function (st) {
		$scope.CurEmailSend.Temlate = st;
		$scope.CurEmailSend.Title = st.Title;
		$scope.CurEmailSend.Subject = st.Title;
		$scope.CurEmailSend.CC = st.CC;
		$scope.CurEmailSend.Description = st.Description;
	}
	$scope.SendSMSIndivisual = function (beData) {
		$scope.CurSMSSend = {
			Temlate: {},
			Description: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl: []
		};
		$scope.CurSMSSend.DataColl.push(beData);

		$('#sendsms').modal('show');
	};
	$scope.SendEmailIndivisual = function (beData) {


		myDropzone.removeAllFiles();

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: [],
		};
		$scope.CurEmailSend.DataColl.push(beData);

		if ($scope.CurEmailSend.DataColl.length == 0) {
			Swal.fire('Please ! Select Data From List To Send Email')
			return;
		} else {
			$('#sendemail').modal('show');
		}

	}
	$scope.ShowSMSDialog = function () {
		$scope.CurSMSSend = {
			Temlate: {},
			Description: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl: []
		};

		angular.forEach($scope.StudentForReceiptColl, function (es) {
			if (es.IsSelected == true)
				$scope.CurSMSSend.DataColl.push(es);
		});


		if ($scope.CurSMSSend.DataColl.length == 0) {
			Swal.fire('Please ! Select Data From List To Send SMS')
			return;
		} else {
			$('#sendsms').modal('show');
		}

	};

	$scope.SendSMS = function () {
		if ($scope.CurSMSSend && $scope.CurSMSSend.DataColl && $scope.CurSMSSend.DataColl.length > 0) {

			var smsColl = [];
			angular.forEach($scope.CurSMSSend.DataColl, function (objEntity) {
				var contactNoColl = [];
				if ($scope.CurSMSSend.Primary == true) {

					if (objEntity.ContactNo && objEntity.ContactNo.length > 0)
						contactNoColl.push(objEntity.ContactNo);
				}
				if ($scope.CurSMSSend.Father == true) {
					if (objEntity.F_ContactNo && objEntity.F_ContactNo.length > 0)
						contactNoColl.push(objEntity.F_ContactNo);
				}

				if ($scope.CurSMSSend.Mother == true) {
					if (objEntity.M_ContactNo && objEntity.M_ContactNo.length > 0)
						contactNoColl.push(objEntity.M_ContactNo);
				}

				if ($scope.CurSMSSend.Guardian == true) {
					if (objEntity.G_Contact && objEntity.G_Contact.length > 0)
						contactNoColl.push(objEntity.G_Contact);
				}

				if (contactNoColl.length > 0) {
					var msg = $scope.CurSMSSend.Description;
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(variable, val);
						}

						if (msg.indexOf('$$') == -1)
							break;
					}

					var newSMS = {
						EntityId: entityStudentUserSMS,
						StudentId: objEntity.TranId,
						UserId: 0,
						Title: $scope.CurSMSSend.Temlate.Title,
						Message: msg,
						ContactNo: contactNoColl.toString(),
						StudentName: objEntity.Name
					};
					smsColl.push(newSMS);
				}

			});

			if (smsColl.length > 0) {

				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Global/SendSMSToStudent",
					dataType: "json",
					data: JSON.stringify(smsColl)
				}).then(function (sRes) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(sRes.data.ResponseMSG);
					$('#sendsms').modal('hide');
				});
			}

		} else {
			Swal.fire('No Data found for sms');
		}
	};

	$scope.ShowEmailDialog = function () {

		myDropzone.removeAllFiles();

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: [],
		};
		angular.forEach($scope.StudentForReceiptColl, function (es) {
			if (es.IsSelected == true)
				$scope.CurEmailSend.DataColl.push(es);
		});

		$('#sendemail').modal('show');
	};

	$scope.SendEmail = function () {
		if ($scope.CurEmailSend && $scope.CurEmailSend.DataColl && $scope.CurEmailSend.DataColl.length > 0) {

			var filesColl = myDropzone.files;

			var ccColl = [];
			if ($scope.CurEmailSend.EmployeeColl && $scope.CurEmailSend.EmployeeColl.length > 0) {
				angular.forEach($scope.CurEmailSend.EmployeeColl, function (emp) {
					if (emp.EmailId && emp.EmailId.length > 0)
						ccColl.push(emp.EmailId);
				});
			}

			var emailDataColl = [];
			angular.forEach($scope.CurEmailSend.DataColl, function (objEntity) {
				var emailColl = [];
				if ($scope.CurEmailSend.Primary == true) {

					if (objEntity.Email && objEntity.Email.length > 0)
						emailColl.push(objEntity.Email);
				}
				if ($scope.CurEmailSend.Father == true) {
					if (objEntity.F_Email && objEntity.F_Email.length > 0)
						emailColl.push(objEntity.F_Email);
				}

				if ($scope.CurEmailSend.Mother == true) {
					if (objEntity.M_Email && objEntity.M_Email.length > 0)
						emailColl.push(objEntity.M_Email);
				}

				if ($scope.CurEmailSend.Guardian == true) {
					if (objEntity.G_Email && objEntity.G_Email.length > 0)
						emailColl.push(objEntity.G_Email);
				}

				if (emailColl.length > 0) {
					var msg = $scope.CurEmailSend.Description;
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(variable, val);
						}

						if (msg.indexOf('$$') == -1)
							break;
					}

					var paraColl = [];
					paraColl.push({ Key: 'StudentId', Value: objEntity.StudentId });

					var newEmail = {
						EntityId: entityStudentUserSMS,
						StudentId: objEntity.TranId,
						UserId: 0,
						Title: $scope.CurEmailSend.Temlate.Title,
						Subject: $scope.CurEmailSend.Subject,
						Message: msg,
						CC: ccColl.toString(),
						To: emailColl.toString(),
						StudentName: objEntity.Name,
						ParaColl: paraColl,
						FileName: 'enquiry-form'
					};
					emailDataColl.push(newEmail);
				}

			});

			if (emailDataColl.length > 0) {


				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Global/SendEmailToStudent",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						if (data.files) {
							for (var i = 0; i < data.files.length; i++) {

								if (data.files[i].File)
									formData.append("file" + i, data.files[i].File);
								else
									formData.append("file" + i, data.files[i]);
							}
						}

						return formData;
					},
					data: { jsonData: emailDataColl, files: filesColl }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					Swal.fire(res.data.ResponseMSG);

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					$('#sendemail').modal('hide');
				});

			}

		} else {
			Swal.fire('No Data found for sms');
		}
	};
});