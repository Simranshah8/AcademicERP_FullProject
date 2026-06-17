app.controller('QuickAccessController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'Quick Access';
	$rootScope.ChangeLanguage();

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.GenderColl = GlobalServices.getGenderList();
		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.RemarksForList = [{ id: 1, text: 'MERITS' }, { id: 2, text: 'DEMERITS' }, { id: 3, text: 'OTHERS' },]
		$scope.DisablityList = GlobalServices.getDisablityList();
		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.MaritalStatusList = GlobalServices.getMaritaStatusList();

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.RoomList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllRoomListForMapping",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RoomList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TransportPointList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			LeaveTaken:1,
			ELeaveTaken: 1,
			SMeritRemarks: 1,
			SDemeritRemarks: 1,
			SOtherRemarks: 1,
			EMeritRemarks: 1,
			EDemeritRemarks: 1,
			EOtherRemarks: 1,
			SComplainlist: 1,
			EComplainlist: 1,
			SNotification: 1,
			
		};		

		$scope.perPage = {
			ELeaveTaken: GlobalServices.getPerPageRow(),
			LeaveTaken: GlobalServices.getPerPageRow(),
			SMeritRemarks: GlobalServices.getPerPageRow(),
			SDemeritRemarks: GlobalServices.getPerPageRow(),
			SOtherRemarks: GlobalServices.getPerPageRow(),
			EMeritRemarks: GlobalServices.getPerPageRow(),
			EDemeritRemarks: GlobalServices.getPerPageRow(),
			EOtherRemarks: GlobalServices.getPerPageRow(),
			SComplainlist: GlobalServices.getPerPageRow(),
			EComplainlist: GlobalServices.getPerPageRow(),
			SNotification: GlobalServices.getPerPageRow(),
		};

		$scope.searchData = {
			EmpRemarks: '',
			ELeaveTaken: '',
			LeaveTaken:'',
			SMeritRemarks:'',
			SDemeritRemarks:'',
			SOtherRemarks:'',
			EMeritRemarks:'',
			EDemeritRemarks:'',
			EOtherRemarks:'',
			SComplainlist:'',
			EComplainlist:'',
			SNotification:''
		};

        $scope.newQuickAccess =
        {
            ShowLeftStudent:true,
            SelectStudent: $scope.StudentSearchOptions[0].value,
			StudentId: null,
			PhotoPath: '~/wwwroot/dynamic/images/avatar-img.jpeg'
        };

		//Employee Quick Access
		$scope.newEQuickAccess =
		{
			ShowLeftEmployee: true,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			EmployeeId: null,
			PhotoPath: '~/wwwroot/dynamic/images/avatar-img.jpeg'
		};
		//Ends

		$scope.RemarksTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllRemarksTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RemarksTypeList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



        $scope.newStudentLedger = {
            TemplatesColl:[]
        };

        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentLedger + "&voucherId=0&isTran=false",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data)
                $scope.newStudentLedger.TemplatesColl = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

		//$scope.GetAllCreateCanteenList();
		//$scope.GetAllFoodCategoryList();
		//$scope.GetAllCreateFoodList();
		//$scope.GetAllCreateFoodList();
	}

	function OnClickDefault() {
		document.getElementById('form-edit').style.display = "none";
		document.getElementById('Eform-edit').style.display = "none";
		
		document.getElementById('card-p').classList.add("current-page");
		$('.profile').show();
		$('.attendance, .remarks , .complain , .notification ,.homework, .assignment , .exam-area, .examgroup-area , .fee , .e-library , .document').hide();
		$('.card-p').addClass('current-page');
		$('.card-1, .card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-21 , .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');


		//document.getElementById('editbtn').onclick = function () {
		//	document.getElementById('form-data').style.display = "none";
		//	document.getElementById('form-edit').style.display = "block";
		//}
		document.getElementById('Back').onclick = function () {
			$scope.getQuickAccess();
			document.getElementById('form-edit').style.display = "none";
			document.getElementById('form-data').style.display = "block";
		}
		//document.getElementById('Eeditbtn').onclick = function () {
		//	document.getElementById('Eform-data').style.display = "none";
		//	document.getElementById('Eform-edit').style.display = "block";
		//}
		document.getElementById('EBack').onclick = function () {
			$scope.getEQuickAccess();
			document.getElementById('Eform-edit').style.display = "none";
			document.getElementById('Eform-data').style.display = "block";
		}


		

		$('.card-p').click(function () {
			$('.profile').show();
			$('.attendance, .remarks , .complain , .notification , .assignment , .exam-area, .examgroup-area , .fee , .e-library , .document,.homework').hide();
			$('.card-p').addClass('current-page');
			$('.card-1, .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-21 , .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-1').click(function () {
			$('.attendance').show();
			$('.profile, .remarks , .complain , .notification , .assignment , .exam-area , .examgroup-area, .fee , .e-library , .document,.homework').hide();
			//  $('.period-wise , .exam-wise , .leave-wise').hide();
			$('.card-1').addClass('current-page');
			$('.card-p, .card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-21, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-2').click(function () {
			$('.remarks').show();
			$('.profile, .attendance , .complain , .notification , .assignment , .exam-area, .examgroup-area , .fee , .e-library , .document,.homework').hide();
			$('.card-2').addClass('current-page');
			$('.card-p, .card-1 , .card-3 , .card-4 , .card-5 , .card-6 , .card-21, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-3').click(function () {
			$('.complain').show();
			$('.profile, .attendance , .remarks , .notification , .assignment , .exam-area , .examgroup-area, .fee , .e-library , .document,.homework').hide();
			$('.card-3').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-4 , .card-5 , .card-6 , .card-21, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-4').click(function () {
			$('.notification').show();
			$('.profile, .attendance , .remarks , .complain , .assignment , .exam-area , .examgroup-area, .fee , .e-library , .document,.homework').hide();
			$('.card-4').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-5 , .card-6, .card-21 , .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-5').click(function () {
			$('.assignment').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .exam-area , .examgroup-area, .fee , .e-library , .document,.homework').hide();
			$('.card-5').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-6 , .card-21, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-6').click(function () {
			$('.exam-area').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .examgroup-area, .fee , .e-library , .document,.homework').hide();
			$('.card-6').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-21, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-21').click(function () {
			$('.examgroup-area').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .exam-area, .fee , .e-library , .document,.homework').hide();
			$('.card-21').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-7 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-7').click(function () {
			$('.fee').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .exam-area , .examgroup-area, .e-library , .document,.homework').hide();
			$('.card-7').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-21 , .card-8 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-8').click(function () {
			$('.e-library').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .exam-area, .examgroup-area , .fee , .document,.homework').hide();
			$('.card-8').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-21 , .card-7 , .card-9,.card-10').removeClass('current-page');
		});

		$('.card-9').click(function () {
			$('.document').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .exam-area, .examgroup-area , .fee , .e-library,.homework').hide();
			$('.card-9').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-21 , .card-7 , .card-8,.card-10').removeClass('current-page');
		});
		$('.card-10').click(function () {
			$('.homework').show();
			$('.profile, .attendance , .remarks , .complain , .notification , .assignment , .exam-area, .examgroup-area , .fee , .e-library').hide();
			$('.card-10').addClass('current-page');
			$('.card-p, .card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6, .card-21 , .card-7 , .card-8,.card-9').removeClass('current-page');
		});

		//New code added by suresh on 6th jestha

		$('.profilee').show();
		$('.attendancee, .remarkse , .complaine , .notificatione ,.homeworke, .assignmente , .exam-areae, .examgroup-areae , .feee , .e-librarye , .documente').hide();
		$('.card-pe').addClass('current-page');
		$('.card-1e, .card-2e , .card-3e , .card-4e , .card-5e , .card-6e , .card-21e , .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');


		$('.card-pe').click(function () {
			$('.profilee').show();
			$('.attendancee, .remarkse , .complaine , .notificatione , .assignmente , .exam-areae, .examgroup-areae , .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-pe').addClass('current-page');
			$('.card-1e, .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-21e , .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-1e').click(function () {
			$('.attendancee').show();
			$('.profilee, .remarkse , .complaine , .notificatione , .assignmente , .exam-areae , .examgroup-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			//  $('.period-wise , .exam-wise , .leave-wise').hide();
			$('.card-1e').addClass('current-page');
			$('.card-pe, .card-2e , .card-3e , .card-4e , .card-5e , .card-6e , .card-21e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-2e').click(function () {
			$('.remarkse').show();
			$('.profilee, .attendancee , .complaine , .notificatione , .assignmente , .exam-areae, .examgroup-areae , .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-2e').addClass('current-page');
			$('.card-pe, .card-1e , .card-3e , .card-4e , .card-5e , .card-6e , .card-21e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-3e').click(function () {
			$('.complaine').show();
			$('.profilee, .attendancee , .remarkse , .notificatione , .assignmente , .exam-areae , .examgroup-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-3e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-4e , .card-5e , .card-6e , .card-21e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-4e').click(function () {
			$('.notificatione').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .assignmente , .exam-areae , .examgroup-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-4e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-5e , .card-6e, .card-21e , .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-5e').click(function () {
			$('.assignmente').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .exam-areae , .examgroup-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-5e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-6e , .card-21e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-6e').click(function () {
			$('.exam-areae').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .examgroup-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-6e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-21e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-21e').click(function () {
			$('.examgroup-areae').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .exam-areae, .feee , .e-librarye , .documente,.homeworke').hide();
			$('.card-21e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-7e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-7e').click(function () {
			$('.feee').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .exam-areae , .examgroup-areae, .e-librarye , .documente,.homeworke').hide();
			$('.card-7e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-21e , .card-8e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-8e').click(function () {
			$('.e-librarye').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .exam-areae, .examgroup-areae , .feee , .documente,.homeworke').hide();
			$('.card-8e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-21e , .card-7e , .card-9e,.card-10e').removeClass('current-page');
		});

		$('.card-9e').click(function () {
			$('.documente').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .exam-areae, .examgroup-areae , .feee , .e-librarye,.homeworke').hide();
			$('.card-9e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-21e , .card-7e , .card-8e,.card-10e').removeClass('current-page');
		});
		$('.card-10e').click(function () {
			$('.homeworke').show();
			$('.profilee, .attendancee , .remarkse , .complaine , .notificatione , .assignmente , .exam-areae, .examgroup-areae , .feee , .e-librarye').hide();
			$('.card-10e').addClass('current-page');
			$('.card-pe, .card-1e , .card-2e , .card-3e , .card-4e , .card-5e , .card-6e, .card-21e , .card-7e , .card-8e,.card-9e').removeClass('current-page');
		});

	};

	$scope.IsValidStudentId = function () {
		if (!$scope.StudentPF.StudentId) {
			Swal.fire("Please! Select Student");
			return false;
		}
		else
			return true;
    }


	$scope.GetStudentById = function () {
		if ($scope.IsValidStudentId() == true) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.StudentPF.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.updateStudentPF = res.data.Data;

					if ($scope.updateStudentPF.DOB_AD)
						$scope.updateStudentPF.DOB_TMP = new Date($scope.updateStudentPF.DOB_AD);

					$scope.updateStudentPF.Mode = 'Update';
					document.getElementById('form-data').style.display = "none";
					document.getElementById('form-edit').style.display = "block";

					

					if ($scope.updateStudentPF.StudentId)
						$scope.updateStudentPF.StudentId = $scope.updateStudentPF.StudentId;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	};



	$scope.UpdateStudentProfile = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.updateStudentPF.DOB_ADDet) {
			$scope.updateStudentPF.DOB_AD = $filter('date')(new Date($scope.updateStudentPF.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		}
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/UpdateStudent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.updateStudentPF }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();			
			if (res.data.IsSuccess == true) {
				Swal.fire(res.data.ResponseMSG);
				document.getElementById('form-edit').style.display = "none";
				document.getElementById('form-data').style.display = "block";
				$scope.getQuickAccess();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}




    $scope.getQuickAccess = function () {
        // Display Profile
        var para =
        {
            StudentId: $scope.newQuickAccess.StudentId
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetStudentProfile",
            dataType: "json",
            data:JSON.stringify(para)
        }).then(function (res1) {
            if (res1.data.IsSuccess && res1.data.Data) {
				$scope.StudentPF = res1.data.Data;

				$timeout(function () {
					const qrcodeContainer = document.getElementById("qrcode");
					qrcodeContainer.innerHTML = ""; // Clear previous QR code if any

					if ($scope.StudentPF.QrCode) {
						new QRCode(qrcodeContainer, {
							text: $scope.StudentPF.QrCode,
							width: 140,
							height: 140,
							colorDark: "#000000",
							colorLight: "#ffffff",
							correctLevel: QRCode.CorrectLevel.L
						});
					}

				});
		

				if ($scope.StudentPF.PhotoPath && $scope.StudentPF.PhotoPath.length == 0)
					$scope.StudentPF.PhotoPath = '/wwwroot/dynamic/images/avatar-img.png';
				else if (!$scope.StudentPF.PhotoPath)
					$scope.StudentPF.PhotoPath = '/wwwroot/dynamic/images/avatar-img.png';

				$timeout(function () {
					$scope.getStudentLedger();
				});

				$timeout(function () {
					$scope.getStudentVoucher();
				});

				$timeout(function () {
					$scope.getStudentResult();
				});

				$timeout(function () {
					$scope.getStudentGroupResult();
				});

				$timeout(function () {
					$scope.getNotificationLog();
				});

				$timeout(function () {
					$scope.getStudentRemarks();
				});

				$timeout(function () {
					$scope.getStudentHomeWorks();
				});
				$timeout(function () {
					$scope.getStudentAssignments();
				});

				$timeout(function () {
					$scope.getStudentBookLedger();
				});

				$timeout(function () {
					$scope.getStudentComplain();
				});

				$timeout(function () {
					$scope.getStudentLeaveTaken();
				});

				$timeout(function () {
					$scope.getStudentAttachment();
				});

				$timeout(function () {
					$scope.getStudentAttendance();
				});
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

	$scope.getStudentLedger = function () {

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Report/GetStudentLedger",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentLedger = res.data.Data;

					$scope.StudentLedger.LedgerColl = [];

					var dataColl = mx($scope.StudentLedger.LedgerDetailsColl);
					var query = dataColl.groupBy(t => t.Particular).toArray();
					var queryFeeHeading = dataColl.groupBy(t => t.FeeHeading).toArray();

					$timeout(function () {
						$scope.$apply(function () {
							$scope.StudentLedger.FeeHeadingColl = [];
							angular.forEach(queryFeeHeading, function (f) {
								if (f.key && f.key.length > 0 && f.key != 'Opening') {
									$scope.StudentLedger.FeeHeadingColl.push({
										id: f.key,
										text: f.key
									});
								}
							});
						});
					});

					$timeout(function () {
						var sno = 1;
						var totalDues = 0;
						var opening = $scope.StudentLedger.OpeningAmt;

						angular.forEach(query, function (q) {
							var elColl = mx(q.elements);
							totalDues = totalDues + elColl.sum(p1 => p1.PDues + p1.Debit - p1.Credit);
							var beData = {
								SNo: sno,
								MonthName: q.key,
								Opening: opening,
								//Amount: opening+ elColl.sum(p1 => p1.PDues + p1.Debit),
								Amount: opening + elColl.sum(p1 => p1.Debit),
								DisAmt: elColl.sum(p1 => p1.Discount),
								PaidAmt: elColl.sum(p1 => p1.Paid),
								Balance: totalDues,
								RefNo: elColl.max(p1 => p1.RefNo),
								VoucherNo: elColl.max(p1 => p1.VoucherNo),
								VoucherDate: elColl.max(p1 => p1.VoucherDate),
								DataColl: []
							};


							var sno1 = 1;
							angular.forEach($scope.StudentLedger.FeeHeadingColl, function (fi) {
								beData.DataColl.push({
									SNo: sno1,
									FeeHeading: fi.text,
									DebitAmt: elColl.where(p1 => p1.FeeHeading == fi.text).sum(p1 => p1.Debit)
								});
								sno1++;
							})
							sno++;

							$scope.StudentLedger.LedgerColl.push(beData);

							if (sno > 1)
								opening = totalDues;
						});

					});


					if (!$scope.StudentLedger.PhotoPath || $scope.StudentLedger.PhotoPath.length == 0)
						$scope.StudentLedger.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.getStudentRemarks = function () {

		$scope.StudentMeritRemarksColl = [];
		$scope.StudentDemeritRemarksColl = [];
		$scope.StudentOthersRemarksColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentRemarks",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					/*	$scope.StudentRemarksColl = res.data.Data;		*/
					$scope.StudentMeritRemarksColl = [];
					$scope.StudentDemeritRemarksColl = [];
					$scope.StudentOthersRemarksColl = [];

					res.data.Data.forEach(function (dt) {
						if (dt.RemarksFor == 'MERITS')
							$scope.StudentMeritRemarksColl.push(dt)
						else if (dt.RemarksFor == 'DEMERITS')
							$scope.StudentDemeritRemarksColl.push(dt)
						else
							$scope.StudentOthersRemarksColl.push(dt)

					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getStudentBookLedger = function () {

		$scope.StudentBookColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentBookLedger",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentBookColl = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getStudentHomeWorks = function () {

		$scope.StudentHomeWork = {
			NEW: 0,
			NEWDataColl: [],
			NOTDONE: 0,
			NOTDONEDataColl: [],
			PENDING: 0,
			PENDINGDataColl: [],
			REDO: 0,
			REDODataColl: [],
			DONE: 0,
			DONEDataColl: [],
			CurHomeWorkColl:[],
		};
		 
		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentHomeWorks",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var homeWorks = res.data.Data;
					var query = mx(homeWorks).groupBy(t => t.HomeWorkStatus).toArray();

					var totalCount = homeWorks.length;
					angular.forEach(query, function (q) {
					 
						if (q.key == "NEW") {
							$scope.StudentHomeWork.NEW = q.elements.length;
							$scope.StudentHomeWork.NEWDataColl = q.elements;
							$scope.StudentHomeWork.NEWPER = ((q.elements.length / totalCount) * 100).toFixed(2);
							$scope.StudentHomeWork.CurHomeWorkColl = q.elements;
						} else if (q.key == "NOT DONE") {
							$scope.StudentHomeWork.NOTDONE = q.elements.length;
							$scope.StudentHomeWork.NOTDONEDataColl = q.elements;
							$scope.StudentHomeWork.NOTDONEPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "PENDING") {
							$scope.StudentHomeWork.PENDING = q.elements.length;
							$scope.StudentHomeWork.PENDINGDataColl = q.elements;
							$scope.StudentHomeWork.PENDINGPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "RE-DO") {
							$scope.StudentHomeWork.REDO = q.elements.length;
							$scope.StudentHomeWork.REDODataColl = q.elements;
							$scope.StudentHomeWork.REDOPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "DONE") {
							$scope.StudentHomeWork.DONE = q.elements.length;
							$scope.StudentHomeWork.DONEDataColl = q.elements;
							$scope.StudentHomeWork.DONEPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
					})

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getStudentAssignments = function () {

		$scope.StudentAssignment = {
			NEW: 0,
			NEWDataColl: [],
			NOTDONE: 0,
			NOTDONEDataColl: [],
			PENDING: 0,
			PENDINGDataColl: [],
			REDO: 0,
			REDODataColl: [],
			DONE: 0,
			DONEDataColl: [],
			CurHomeWorkColl: [],
		};

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentAssignmentWorks",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var homeWorks = res.data.Data;
					var query = mx(homeWorks).groupBy(t => t.AssignmentStatus).toArray();

					var totalCount = homeWorks.length;
					angular.forEach(query, function (q) {

						if (q.key == "NEW") {
							$scope.StudentAssignment.NEW = q.elements.length;
							$scope.StudentAssignment.NEWDataColl = q.elements;
							$scope.StudentAssignment.NEWPER = ((q.elements.length / totalCount) * 100).toFixed(2);
							$scope.StudentAssignment.CurHomeWorkColl = q.elements;
						} else if (q.key == "NOT DONE") {
							$scope.StudentAssignment.NOTDONE = q.elements.length;
							$scope.StudentAssignment.NOTDONEDataColl = q.elements;
							$scope.StudentAssignment.NOTDONEPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "PENDING") {
							$scope.StudentAssignment.PENDING = q.elements.length;
							$scope.StudentAssignment.PENDINGDataColl = q.elements;
							$scope.StudentAssignment.PENDINGPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "RE-DO") {
							$scope.StudentAssignment.REDO = q.elements.length;
							$scope.StudentAssignment.REDODataColl = q.elements;
							$scope.StudentAssignment.REDOPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
						else if (q.key == "DONE") {
							$scope.StudentAssignment.DONE = q.elements.length;
							$scope.StudentAssignment.DONEDataColl = q.elements;
							$scope.StudentAssignment.DONEPER = ((q.elements.length / totalCount) * 100).toFixed(2);
						}
					})

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.PrintPDFStudentLedger = function () {
		$("#divLedger").printThis(
			{
				importCSS: true,
				importStyle: true,
				loadCSS: "/Content/printrules.css"
			}
		);
	};
	$scope.PrintStudentLedger = function () {
		if ($scope.newQuickAccess.StudentId && $scope.newStudentLedger.RptTranId > 0) {

			var EntityId = entityStudentLedger;

			var rptPara = {
				studentId: $scope.newQuickAccess.StudentId,
				rptTranId: $scope.newStudentLedger.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRpt").src = '';
			document.getElementById("frmRpt").style.width = '100%';
			document.getElementById("frmRpt").style.height = '1300px';
			document.getElementById("frmRpt").style.visibility = 'visible';
			document.getElementById("frmRpt").src = base_url + "Fee/Report/RptStudentLedger?" + paraQuery;
			$('#FrmPrintReport').modal('show');
		} else if ($scope.newQuickAccess.StudentId > 0 && $scope.newStudentLedger.RptTranId == -1) {
			$scope.PrintPDFStudentLedger();
		}
	};

	$scope.getStudentVoucher = function () {

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Report/GetStudentVoucher",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentVoucher = res.data.Data;

					if (!$scope.StudentVoucher.PhotoPath || $scope.StudentVoucher.PhotoPath.length == 0)
						$scope.StudentVoucher.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';

					var frColl = [];
					angular.forEach($scope.StudentVoucher.VoucherColl, function (fr) {
						fr.$$treeLevel = 0;
						frColl.push(fr);
						angular.forEach(fr.DetailsColl, function (det) {
							det.Particulars = ' - ' + det.FeeItem;
							frColl.push(det);
						});
					});
					$scope.gridOptions1.data = frColl;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.PrintPDFStudentVoucher = function () {
		$("#profile").printThis(
			{
				importCSS: true,
				importStyle: true,
				loadCSS: "/Content/printrules.css"
			}
		);
	};
	getterAndSetter();
	function getterAndSetter() {
		$scope.discounts = [];
		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			//enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "VoucherDate_BS", displayName: "Date(B.S.)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherNo", displayName: "Voucher No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Particulars", displayName: "Particulars", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Amount", displayName: "Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DisAmt", displayName: "Discount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Debit", displayName: "Debit Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Credit", displayName: "Credit Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment' },

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

	$scope.getStudentResult = function () {

		$scope.ResultSubjectList = [];
		$scope.ResultColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentResult",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.ResultColl = res.data.Data;					  

					if ($scope.ResultColl && $scope.ResultColl.length > 0)
						$scope.ResultSubjectList = $scope.ResultColl[0].SubjectList;

					$timeout(function () {
						angular.forEach($scope.ResultColl, function (rc) {
							rc.tmpSubjectMarkColl = [];
							var markColl = mx(rc.SubjectMarkColl);
							angular.forEach($scope.ResultSubjectList, function (sb) {
								var findSub = markColl.firstOrDefault(p1 => p1.SubjectId == sb.SubjectId);
								if (findSub)
									rc.tmpSubjectMarkColl.push(findSub);
								else
									rc.tmpSubjectMarkColl.push({});
							});
						});

						angular.forEach($scope.ResultSubjectList, function (rs) {
							rs.ExamSubjectColl=[]
							angular.forEach($scope.ResultColl, function (rc) {
								var findSub = mx(rc.SubjectMarkColl).firstOrDefault(p1 => p1.SubjectId == rs.SubjectId);
								if (findSub) {
									rs.ExamSubjectColl.push({
										ExamTypeName: rc.ExamTypeName,
										OM: findSub.OM,
										Per: findSub.Per
									});
								} else {
									rs.ExamSubjectColl.push({});
                                }
							});
							
							var avg1 = 0;

						});
					});

					$timeout(function () {
						$scope.ExamResultEvlChart();
					});

					$timeout(function () {
						$scope.ExamSubjectChart();
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getStudentGroupResult = function () {

		$scope.GroupResultSubjectList = [];
		$scope.GroupResultColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentGroupResult",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.GroupResultColl = res.data.Data;

					if ($scope.GroupResultColl && $scope.GroupResultColl.length > 0)
						$scope.GroupResultSubjectList = $scope.GroupResultColl[0].SubjectList;

					$timeout(function () {
						angular.forEach($scope.GroupResultColl, function (rc) {
							rc.tmpSubjectMarkColl = [];
							var markColl = mx(rc.SubjectMarkColl);
							angular.forEach($scope.GroupResultSubjectList, function (sb) {
								var findSub = markColl.firstOrDefault(p1 => p1.SubjectId == sb.SubjectId);
								if (findSub)
									rc.tmpSubjectMarkColl.push(findSub);
								else
									rc.tmpSubjectMarkColl.push({});
							});
						});

						angular.forEach($scope.GroupResultSubjectList, function (rs) {
							rs.ExamSubjectColl = []
							angular.forEach($scope.GroupResultColl, function (rc) {
								var findSub = mx(rc.SubjectMarkColl).firstOrDefault(p1 => p1.SubjectId == rs.SubjectId);
								if (findSub) {
									rs.ExamSubjectColl.push({
										ExamTypeName: rc.ExamTypeName,
										OM: findSub.OM,
										Per: findSub.Per
									});
								} else {
									rs.ExamSubjectColl.push({});
								}
							});

							var avg1 = 0;

						});
					});

					$timeout(function () {
						$scope.GroupExamResultEvlChart();
					});

					$timeout(function () {
						$scope.GroupExamSubjectChart();
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.ResultEvaluation = function (rc) {
		$timeout(function () {
			$scope.CurResult = rc;
		});
	}

	var myChart = null;
	$scope.ExamResultEvlChart = function () {

		if (myChart)
			myChart.destroy();

		var ctx = document.getElementById('myChart');

		var labelColl = [];
		var dataColl = [];
		angular.forEach($scope.ResultColl, function (rc) {
			labelColl.push(rc.ExamTypeName);
			dataColl.push(rc.Per);
		});

		myChart = new Chart(ctx, {
			type: 'line',
			data: {
				labels: labelColl,
				datasets: [{
					label: ' overall progress',
					display: false,
					data: dataColl,
					callback: function (data) { return data },
					backgroundColor: [
						'rgba(255,255, 255, 0.9)'

					],
					borderColor: [
						'rgba(118, 190, 78, 1)'

					],
					fontColor: [
						'rgba(0,0,0,1)'
					],
					fontWeight: [
						'bold'
					],
					borderWidth: 2
				}]
			},
			options: {
				responsive: false,
				scales: {
					yAxes: [{
						ticks: {
							min: 20,
							max: 100,
							stepSize: 20,
							fontSize: 14,
							callback: function (value) { return value },
							fontWeight: [
								'bold'
							]
						}
					}],
					xAxes: [{
						ticks: {
							fontSize: 14,
							fontWeight: [
								'bold'
							]
						}
					}],
				},
				legend: {
					display: false,
					position: "right",
					margin: {
						left: 50,
						right: 0
					}
				},

				layout: {
					padding: {
						top: 50,
						left: 20,
						right: 0,
						bottom: 10
					}
				}
			}
		});
	}

	var myChart1 = null;
	$scope.ExamSubjectChart = function () {

		if (myChart1)
			myChart1.destroy();

		var labelColl = [];
		var dataColl = [];
		angular.forEach($scope.ResultSubjectList, function (sb) {
			labelColl.push(sb.SubjectName);
			dataColl.push(sb.APer);
		});

		var ctx2 = document.getElementById('subjectChart');
		myChart1 = new Chart(ctx2, {
			type: 'line',
			data: {
				labels: labelColl,
				datasets: [{
					label: ' overall progress',
					display: false,
					data: dataColl,
					callback: function (data) { return data + "%" },
					backgroundColor: [
						'rgba(255,255, 255, 0.9)'
					],
					borderColor: [
						'rgba(118, 190, 78, 1)'
					],
					fontColor: [
						'rgba(0,0,0,1)'
					],
					borderWidth: 2
				}]
			},
			options: {
				responsive: false,
				scales: {
					yAxes: [{
						ticks: {
							min: 0,
							max: 100,
							stepSize: 20,
							fontSize: 14,
							callback: function (value) { return value }
						}
					}],
					xAxes: [{
						ticks: {
							fontSize: 14,
							fontWeight: [
								'bold'
							]
						}
					}],
				},
				legend: {
					display: false,
					position: "right",
					margin: {
						left: 50,
						right: 0
					}
				},
				layout: {
					padding: {
						top: 50,
						left: 20,
						right: 0,
						bottom: 10
					}
				}
			}
		});
	}

	$scope.GroupResultEvaluation = function (rc) {
		$timeout(function () {
			$scope.CurResult = rc;
		});
	}

	var myChartGroup = null;
	$scope.GroupExamResultEvlChart = function () {

		if (myChartGroup)
			myChartGroup.destroy();

		var ctx = document.getElementById('myChartGroup');

		var labelColl = [];
		var dataColl = [];
		angular.forEach($scope.GroupResultColl, function (rc) {
			labelColl.push(rc.ExamTypeName);
			dataColl.push(rc.Per);
		});

		myChartGroup = new Chart(ctx, {
			type: 'line',
			data: {
				labels: labelColl,
				datasets: [{
					label: ' overall progress',
					display: false,
					data: dataColl,
					callback: function (data) { return data },
					backgroundColor: [
						'rgba(255,255, 255, 0.9)'

					],
					borderColor: [
						'rgba(118, 190, 78, 1)'

					],
					fontColor: [
						'rgba(0,0,0,1)'
					],
					fontWeight: [
						'bold'
					],
					borderWidth: 2
				}]
			},
			options: {
				responsive: false,
				scales: {
					yAxes: [{
						ticks: {
							min: 20,
							max: 100,
							stepSize: 20,
							fontSize: 14,
							callback: function (value) { return value },
							fontWeight: [
								'bold'
							]
						}
					}],
					xAxes: [{
						ticks: {
							fontSize: 14,
							fontWeight: [
								'bold'
							]
						}
					}],
				},
				legend: {
					display: false,
					position: "right",
					margin: {
						left: 50,
						right: 0
					}
				},

				layout: {
					padding: {
						top: 50,
						left: 20,
						right: 0,
						bottom: 10
					}
				}
			}
		});
	}

	var myChartGroup1 = null;
	$scope.GroupExamSubjectChart = function () {

		if (myChartGroup1)
			myChartGroup1.destroy();

		var labelColl = [];
		var dataColl = [];
		angular.forEach($scope.GroupResultSubjectList, function (sb) {
			labelColl.push(sb.SubjectName);
			dataColl.push(sb.APer);
		});

		var ctx2 = document.getElementById('subjectChartGroup');
		myChartGroup1 = new Chart(ctx2, {
			type: 'line',
			data: {
				labels: labelColl,
				datasets: [{
					label: ' overall progress',
					display: false,
					data: dataColl,
					callback: function (data) { return data + "%" },
					backgroundColor: [
						'rgba(255,255, 255, 0.9)'
					],
					borderColor: [
						'rgba(118, 190, 78, 1)'
					],
					fontColor: [
						'rgba(0,0,0,1)'
					],
					borderWidth: 2
				}]
			},
			options: {
				responsive: false,
				scales: {
					yAxes: [{
						ticks: {
							min: 0,
							max: 100,
							stepSize: 20,
							fontSize: 14,
							callback: function (value) { return value }
						}
					}],
					xAxes: [{
						ticks: {
							fontSize: 14,
							fontWeight: [
								'bold'
							]
						}
					}],
				},
				legend: {
					display: false,
					position: "right",
					margin: {
						left: 50,
						right: 0
					}
				},
				layout: {
					padding: {
						top: 50,
						left: 20,
						right: 0,
						bottom: 10
					}
				}
			}
		});
	}

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.FilePath && item.FilePath.length > 0) {
			$scope.viewImg.ContentPath = item.FilePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};

	$scope.getNotificationLog = function () {
		$scope.NotificationLogColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentNotificationLog",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.NotificationLogColl = res.data.Data; 

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}






	//Code added by suresh for student Quick Access
	$scope.getStudentComplain = function () {

		$scope.StudentComplainColl = [];

		if ($scope.newQuickAccess.StudentId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentComplainForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.StudentComplainColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}



	$scope.getStudentLeaveTaken = function () {
		$scope.StudentLeaveTakenColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentLeaveTakenForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.StudentLeaveTakenColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}



	$scope.getStudentAttendance = function () {
		$scope.StudentAttendanceColl = [];
		$scope.totalDays = 0;
		$scope.totalWeekEnd = 0;
		$scope.totalHolidays = 0;
		$scope.TPresent = 0;
		$scope.TAbsent = 0;
		$scope.TLeave = 0;
		$scope.TotalSchoolDays = 0;

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentAttendance",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.StudentAttendanceColl = res.data.Data;
					$scope.totalDays = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalDays || 0), 0);
					$scope.totalWeekEnd = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalWeekEnd || 0), 0);
					$scope.totalHolidays = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalHoliday || 0), 0);
					$scope.TPresent = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalPresent || 0), 0);
					$scope.TAbsent = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalAbsent || 0), 0);
					$scope.TLeave = $scope.StudentAttendanceColl.reduce((acc, curr) => acc + (curr.TotalLeave || 0), 0);
					$scope.TotalSchoolDays = $scope.totalDays - $scope.totalWeekEnd - $scope.totalHolidays;


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getStudentAttachment = function () {

		$scope.StudentAttachmentColl = [];

		if ($scope.newQuickAccess.StudentId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newQuickAccess.StudentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetStudentAttForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.StudentAttachmentColl = res.data.Data;
					for (var i = 0; i < $scope.StudentAttachmentColl.length; i++) {
						// Check the extension and set the imgSrc property
						if ($scope.StudentAttachmentColl[i].Extension === '.pdf') {
							$scope.StudentAttachmentColl[i].DocPathimg = '~/dynamic/images/flaticons/pdf.png';
						} else {
							$scope.StudentAttachmentColl[i].DocPathimg = $scope.StudentAttachmentColl[i].DocPath;
						}
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}



	$scope.ShowDocPdf = function (item) {
		$scope.viewImg1 = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg1.ContentPath = item.DocPath;
			$scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer1').src = item.DocPath;
			$('#DocView').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg1.ContentPath = item.PhotoPath;
			$scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
			$('#DocView').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg1.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg1.FileType === 'pdf') {
				document.getElementById('pdfViewer1').src = $scope.viewImg1.ContentPath;
			}

			$('#DocView').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};

	//Code Addded by suresh for Employee Quick Access
	$scope.GetEmployeeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			EmployeeId: refData.EmployeeId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetEmployeeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.updateEmployeePF = res.data.Data;

				if ($scope.updateEmployeePF.DOB_AD) {
					$scope.updateEmployeePF.DOB_AD_TMP = new Date($scope.updateEmployeePF.DOB_AD);
				}
				$scope.updateEmployeePF.Mode = 'Modify';
				document.getElementById('Eform-data').style.display = "none";
				document.getElementById('Eform-edit').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



	$scope.UpdateEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.updateEmployeePF.DOB_ADDet) {
			$scope.updateEmployeePF.DOB_AD = $filter('date')(new Date($scope.updateEmployeePF.DOB_ADDet.dateAD), 'yyyy-MM-dd');;
		} else
			$scope.updateEmployeePF.DOB_AD = null;


		$http({
			method: 'POST',
			url: base_url + "Academic/Report/UpdateEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));


				return formData;
			},
			data: { jsonData: $scope.updateEmployeePF }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();


			if (res.data.IsSuccess == true) {
				Swal.fire(res.data.ResponseMSG);
				$scope.getEQuickAccess();
				document.getElementById('Eform-data').style.display = "block";
				document.getElementById('Eform-edit').style.display = "none";
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



	$scope.getEQuickAccess = function () {
		// Display Profile
		if ($scope.newEQuickAccess.EmployeeId) {
			var para =
			{
				EmployeeId: $scope.newEQuickAccess.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmployeeProfile",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.EmployeePF = res1.data.Data;

					if ($scope.EmployeePF.PhotoPath && $scope.EmployeePF.PhotoPath.length == 0)
						$scope.EmployeePF.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpeg';
					else if (!$scope.EmployeePF.PhotoPath)
						$scope.EmployeePF.PhotoPath = '/wwwroot/dynamic/images/avatar-img.png';

				}

				$timeout(function () {
					$scope.getEmployeeAttachment();
				});

				$timeout(function () {
					$scope.getEmployeeComplain();
				});

				$timeout(function () {
					$scope.getEmployeeRemarks();
				});
				$timeout(function () {
					$scope.getEmployeeLeaveTaken();
				});

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		};
	}


	$scope.getEmployeeAttachment = function () {

		$scope.EmpAttachmentColl = [];

		if ($scope.newEQuickAccess.EmployeeId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				EmployeeId: $scope.newEQuickAccess.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEAttForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.EmpAttachmentColl = res.data.Data;
				for (var i = 0; i < $scope.EmpAttachmentColl.length; i++) {
					// Check the extension and set the imgSrc property
					if ($scope.EmpAttachmentColl[i].Extension === '.pdf') {
						$scope.EmpAttachmentColl[i].DocPathimg = '~/dynamic/images/flaticons/pdf.png';
					} else {
						$scope.EmpAttachmentColl[i].DocPathimg = $scope.EmpAttachmentColl[i].DocPath;
					}
				}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}


	$scope.ShowEmpDocPdf = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer').src = item.DocPath;
			$('#EmpDocView').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
			$('#EmpDocView').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg.FileType === 'pdf') {
				document.getElementById('pdfViewer').src = $scope.viewImg.ContentPath;
			}

			$('#EmpDocView').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};



	$scope.getEmployeeComplain = function () {

		$scope.EmpComlainColl = [];

		if ($scope.newEQuickAccess.EmployeeId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				EmployeeId: $scope.newEQuickAccess.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmpComplainForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.EmpComplainColl = res.data.Data;
					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getEmployeeRemarks = function () {

	/*	$scope.EmpRemarksColl = [];*/
		$scope.EmpMeritRemarksColl = [];
		$scope.EmpDemeritRemarksColl = [];
		$scope.EmpOthersRemarksColl = [];

		if ($scope.newEQuickAccess.EmployeeId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				EmployeeId: $scope.newEQuickAccess.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmpRemarksForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					/*$scope.EmpRemarksColl = res.data.Data;*/

					$scope.EmpMeritRemarksColl = [];
					$scope.EmpDemeritRemarksColl = [];
					$scope.EmpOthersRemarksColl = [];

					res.data.Data.forEach(function (dt) {
						if (dt.RemarksFor == 'MERITS')
							$scope.EmpMeritRemarksColl.push(dt)
						else if (dt.RemarksFor == 'DEMERITS')
							$scope.EmpDemeritRemarksColl.push(dt)
						else
							$scope.EmpOthersRemarksColl.push(dt)

					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getEmployeeLeaveTaken = function () {

		$scope.EmpLeaveTakenColl = [];

		if ($scope.newEQuickAccess.EmployeeId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				EmployeeId: $scope.newEQuickAccess.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmpLeaveTakenForQuickAccess",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					// Process the received data
					$scope.EmpLeaveTakenColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
});