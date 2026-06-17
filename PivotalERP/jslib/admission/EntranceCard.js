

app.controller('EntranceCardController', function ($scope, $http, $timeout, $filter, $rootScope, $translate, GlobalServices, FileSaver) {
	$scope.Title = 'Entrance Card';

	var gSrv = GlobalServices;
	getterAndSetter();

	$scope.PrintHtmlForm = function () {

		$('#admission-enquiry-form').printThis({
			importCSS: true,
			importStyle: true,
			formValues: true,
			//header: "<h1>Look at all of my kitties!</h1>"
		});

	}

	

	
	//$scope.GetEnqSummaryList = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	if ($scope.newEnquiry.ToDateDet && $scope.newEnquiry.FromDateDet) {
	//		var para = {
	//			dateFrom: $filter('date')(new Date($scope.newEnquiry.FromDateDet.dateAD), 'yyyy-MM-dd'),
	//			dateTo: $filter('date')(new Date($scope.newEnquiry.ToDateDet.dateAD), 'yyyy-MM-dd')
	//		}
	//		$http({
	//			method: 'POST',
	//			url: base_url + "FrontDesk/Transaction/GetEnqSummary",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess) {
	//				$scope.gridOptions.data = res.data.Data;
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}
	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}
	//}


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
				{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>' },
				{
					name: "Enquiry/Reg No.",
					width: 150, field: "EnquiryId",
					cellTemplate: '<div class="ui-grid-cell-contents">' +
						'{{ row.entity.EnquiryId || "" }} {{ row.entity.RegId || "" }}' +
						'</div>'
				},
				{ name: "Status", displayName: "Status", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Sourse", displayName: "Sourse", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EntryDate", displayName: "Enquiry Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "For Class", minWidth: 140, headerCellClass: 'headerAligment' },			
				{
					name: "DOB_AD", displayName: "DOB(AD)", minWidth: 140, headerCellClass: 'headerAligment',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
				},
				{ name: "DOB_BS", displayName: "DOB(BS)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },		
				{ name: "ExamName", displayName: "Exam Name", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "ExamDate", displayName: "Exam Date", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "ExamTime", displayName: "Exam Time", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "SymbolNo", displayName: "SymbolNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Venue", displayName: "Venue", minWidth: 140, headerCellClass: 'headerAligment' },				
				{ name: "PaymentStatus", displayName: "Payment Status", minWidth: 140, headerCellClass: 'headerAligment' },
				
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

	$scope.GetEnqSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.newEnquiry.ToDateDet && $scope.newEnquiry.FromDateDet) {
			var para = {
				DateFrom: $filter('date')(new Date($scope.newEnquiry.FromDateDet.dateAD), 'yyyy-MM-dd'),
				DateTo: $filter('date')(new Date($scope.newEnquiry.ToDateDet.dateAD), 'yyyy-MM-dd')
			}
			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetDataForEntranceCard",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.gridOptions.data = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.ComDet = {};


		GlobalServices.getCompanyDet().then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ComDet = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$timeout(function () {
			//Add by prashant Chaitra 26
			$scope.SubjectList = [];
			GlobalServices.getSubjectList().then(function (res) {
				$scope.SubjectList = res.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
			//Add by prashant End

			gSrv.getClassList().then(function (res) {
				$scope.ClassList = res.data.Data;			

				$scope.GetEntranceConfig();

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$scope.newCompanyDetails = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCompanyDet = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.Logo = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Logo = res.data.Data[0];
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			Enquiry: 1,			
		};

		$scope.searchData = {
			Enquiry: ''		
		};

		$scope.newEnquiry = {			
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()
		};

        $scope.newDet = {
            ExamName: '',
            ExamDate_TMP: new Date(),
            StartTime: '',
            EndTime: '',
            Venue: '',
            ForClassWise: false,
            ClassWiseEntranceSetupList: [],
            ExamRules: '',
            ResultDate_TMP: new Date(),
            Subject: '',
            FullMarks: 0,
            PassMarks: 0,
            Mode: 'Save'
		};

		//For Symboll No.

		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
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

	};

	//For Symbol No Start
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

	//Added the reset for entrance exam setuo
	$scope.ResetEntranceExamSetup = function () {
		$scope.newDet.ExamName = '';
		$scope.newDet.ExamDate_TMP = '';
		$scope.newDet.ExamDateDet = null;
		$scope.newDet.StartTime_TMP = '';
		$scope.newDet.EndTime_TMP = '';
		$scope.newDet.ResultDate_TMP = '';
		$scope.newDet.ResultDateDet = null;
		$scope.newDet.ResultTime_TMP = '';
		$scope.newDet.SubjectColl = [];
		$scope.newDet.FullMarks = null;
		$scope.newDet.PassMarks = null;
		$scope.newDet.Venue = '';
		$scope.newDet.ForClassWise = false;
		$scope.newDet.ExamRules = '';
		if ($scope.newDet.ClassWiseEntranceSetupList && $scope.newDet.ClassWiseEntranceSetupList.length > 0) {
			$scope.newDet.ClassWiseEntranceSetupList.forEach(function (cl) {
				cl.ExamName = '';
				cl.ExamDate_TMP = '';
				cl.ExamDateDet = null;
				cl.StartTime_TMP = '';
				cl.EndTime_TMP = '';
				cl.ResultDate_TMP = '';
				cl.ResultDateDet = null;
				cl.ResultTime_TMP = '';
				cl.SubjectColl = [];
				cl.FullMarks = null;
				cl.PassMarks = null;
				cl.Venue = '';
				setTimeout(function () {
					if (typeof $ === 'function' && $(document).find('select.select2').length > 0) {
						$(document).find('select.select2').trigger('change.select2');
					}
				}, 0);
			});
		}
		setTimeout(function () {
			if (typeof $ === 'function' && $(document).find('select.select2').length > 0) {
				$(document).find('select.select2').trigger('change.select2');
			}
		}, 0);
	};
	//Ends

	$scope.GetEntranceSymbolNo = function () {		
		$scope.newSymbolNumber.StudentList = [];
		if ($scope.newSymbolNumber.ClassId) {

			var para = {
				ClassId: $scope.newSymbolNumber.ClassId === 0 ? null : $scope.newSymbolNumber.ClassId ?? null,
			};
			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetEntranceSymbolNo",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newSymbolNumber.StudentList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};



	$scope.AutoGenerateSymbolNo = function () {

		if (!$scope.newSymbolNumber || !$scope.newSymbolNumber.ClassId || $scope.newSymbolNumber.ClassId == 0) {
			Swal.fire("Please! Select Class");
			return;
		}


		if ($scope.newSymbolNumber) {
			var startNo = parseInt($scope.newSymbolNumber.StartNo);
			var pad = $scope.newSymbolNumber.PadWidth;
			if (isNaN(startNo))
				startNo = 0;

			var startAlpha = $scope.newSymbolNumber.StartAlpha;

			var tmpDataColl = $scope.newSymbolNumber.StudentList;// $filter('orderBy')($scope.newSymbolNumber.StudentList, $scope.sortKey, $scope.reverse);

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

	$scope.SaveUpdateSymbolNumber = function () {
		if (!$scope.newSymbolNumber.ClassId || $scope.newSymbolNumber.ClassId === 0) {
			Swal.fire('Please Select Class');
			return;
		}

		if ($scope.newSymbolNumber.StartNo === null ||
			$scope.newSymbolNumber.StartNo === undefined ||
			$scope.newSymbolNumber.StartNo === '') {
			Swal.fire('Please Enter StartNo');
			return;
		}

		let symbolToNames = {};
		angular.forEach($scope.newSymbolNumber.StudentList, function (student) {
			let sym = (student.SymbolNo || '').toString().trim();

			if (sym !== '') {
				if (!symbolToNames[sym]) {
					symbolToNames[sym] = [];
				}
				symbolToNames[sym].push(student.Name || 'Unknown');
			}
		});
		let duplicateMessages = [];
		angular.forEach(symbolToNames, function (names, symbol) {
			if (names.length > 1) {
				duplicateMessages.push(
					`Symbol <strong>${symbol}</strong> is used by: ${names.join(', ')}`
				);
			}
		});
		if (duplicateMessages.length > 0) {
			Swal.fire({
				icon: 'error',
				title: 'Duplicate Symbol Numbers Detected',
				html: duplicateMessages.join('<br>') +
					'<br><br>Please correct the duplicates before saving.',
				confirmButtonText: 'OK'
			});
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpData = [];

		angular.forEach($scope.newSymbolNumber.StudentList, function (st) {
			var newData = {
				EnquiryNo: (st.EnquiryId !== null && st.EnquiryId !== undefined) ? st.EnquiryId : cs.RegId,
				Prefix: $scope.newSymbolNumber.Prefix,
				Suffix: $scope.newSymbolNumber.Suffix,
				StartNumber: $scope.newSymbolNumber.StartNo,
				StartAlpha: $scope.newSymbolNumber.StartAlpha,
				PadWith: $scope.newSymbolNumber.PadWidth,
				SymbolNo: st.SymbolNo
			}
			tmpData.push(newData);
		});
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveEntranceSymbolNo",
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
				$scope.GetEntranceSymbolNo();

			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



    //For Symbol No End

	$scope.validateFullAndPassMarks = function (newDet) {
		if (newDet.FullMarks != null && newDet.PassMarks != null && newDet.FullMarks <= newDet.PassMarks) {
			Swal.fire({
				icon: 'error',
				title: 'Invalid Marks',
				text: 'Full Marks must be greater than Pass Marks.'
			});
		}
	};

	$scope.validateExamAndResultDate = function (newDet) {
		if (newDet.ExamDateDet && newDet.ResultDateDet) {
			let examDate = new Date(newDet.ExamDateDet.year, newDet.ExamDateDet.month - 1, newDet.ExamDateDet.day);
			let resultDate = new Date(newDet.ResultDateDet.year, newDet.ResultDateDet.month - 1, newDet.ResultDateDet.day);

			if (resultDate < examDate) {
				Swal.fire({
					icon: 'error',
					title: 'Invalid Date',
					text: 'Result Date cannot be before Exam Date.'
				});
			}
		}
	};

	$scope.validateStartAndEndTime = function (newDet) {
		if (newDet.StartTime_TMP && newDet.EndTime_TMP) {
			let startTime = new Date("1970-01-01T" + newDet.StartTime_TMP + ":00");
			let endTime = new Date("1970-01-01T" + newDet.EndTime_TMP + ":00");

			if (startTime >= endTime) {
				Swal.fire({
					icon: 'error',
					title: 'Invalid Time',
					text: 'Start Time must be before End Time.'
				});
			}
		}
	};


	$scope.validateFullAndPassMarks = function (data) {
		if (data.FullMarks !== undefined && data.FullMarks < 0) {
			Swal.fire('Full Mark cannot be negative');
			data.FullMarks = '';   
		}

		if (data.PassMarks !== undefined && data.PassMarks < 0) {
			Swal.fire('Pass Mark cannot be negative');
			data.PassMarks = '';
		}
	};

	$scope.IsValidConfig = function () {
		if (!$scope.newDet.ExamName || $scope.newDet.ExamName.trim() === '') {
			Swal.fire('Please! Enter Exam Name');
			return false;
		}

		if (!$scope.newDet.ExamDate_TMP) {
			Swal.fire('Please! Select Exam Date');
			return false;
		}

		if (!$scope.newDet.StartTime_TMP) {
			Swal.fire('Please! Enter Start Time');
			return false;
		}

		if (!$scope.newDet.EndTime_TMP) {
			Swal.fire('Please! Enter End Time');
			return false;
		}

		if ($scope.newDet.StartTime_TMP && $scope.newDet.EndTime_TMP) {
			if ($scope.newDet.EndTime_TMP <= $scope.newDet.StartTime_TMP) {
				Swal.fire('End Time must be after Start Time');
				return false;
			}
		}

		if (!$scope.newDet.ResultDate_TMP) {
			Swal.fire('Please! Select Result Date');
			return false;
		}

		if (!$scope.newDet.ResultTime_TMP) {
			Swal.fire('Please! Enter Result Time');
			return false;
		}

		// Result date should be after exam date
		const examDate = new Date($scope.newDet.ExamDate_TMP);
		const resultDate = new Date($scope.newDet.ResultDate_TMP);
		examDate.setHours(0, 0, 0, 0);
		resultDate.setHours(0, 0, 0, 0);

		if (resultDate <= examDate) {
			Swal.fire('Result Date must be after the Exam Date');
			return false;
		}

		if (!$scope.newDet.SubjectColl || $scope.newDet.SubjectColl.length === 0) {
			Swal.fire('Please! Select at least one Subject');
			return false;
		}

		if (!$scope.newDet.FullMarks && $scope.newDet.FullMarks !== 0) {
			Swal.fire('Please! Enter valid Full Marks');
			return false;
		}

		if (!$scope.newDet.PassMarks && $scope.newDet.PassMarks !== 0) {
			Swal.fire('Please! Enter valid Pass Marks');
			return false;
		}

		if (parseFloat($scope.newDet.PassMarks) > parseFloat($scope.newDet.FullMarks)) {
			Swal.fire('Pass Marks cannot exceed Full Marks');
			return false;
		}

		if (!$scope.newDet.Venue || $scope.newDet.Venue.trim() === '') {
			Swal.fire('Please! Enter Venue');
			return false;
		}

		if ($scope.newDet.ForClassWise === true) {
			if (!$scope.newDet.ClassWiseEntranceSetupList ||
				$scope.newDet.ClassWiseEntranceSetupList.length === 0) {
				Swal.fire('Please add at least one class configuration');
				return false;
			}

			for (let i = 0; i < $scope.newDet.ClassWiseEntranceSetupList.length; i++) {
				const cl = $scope.newDet.ClassWiseEntranceSetupList[i];
			/*	const rowNumber = i + 1;*/
				const classIdentifier = cl.text ? `"${cl.text}"` : `Row ${i + 1}`;
				
				if (cl.StartTime_TMP && cl.EndTime_TMP && cl.EndTime_TMP < cl.StartTime_TMP) {
					Swal.fire(`Row ${classIdentifier}: End Time must be after Start Time`);
					return false;
				}
						

				// Result date > Exam date
				const clExamDate = new Date(cl.ExamDate_TMP);
				const clResultDate = new Date(cl.ResultDate_TMP);
				clExamDate.setHours(0, 0, 0, 0);
				clResultDate.setHours(0, 0, 0, 0);

				if (clResultDate < clExamDate) {
					Swal.fire(`Row ${classIdentifier}: Result Date must be after the Exam Date`);
					return false;
				}



				if (parseFloat(cl.PassMarks) > parseFloat(cl.FullMarks)) {
					Swal.fire(`Row ${classIdentifier}: Pass Marks cannot exceed Full Marks`);
					return false;
				}

				
			}
		}

		return true;
	};

	//$scope.IsValidConfig = function () {
	//	// Check Exam Name
	//	if (!$scope.newDet.ExamName || $scope.newDet.ExamName.trim() === '') {
	//		Swal.fire('Please! Enter Exam Name');
	//		return false;
	//	}

	//	// Check Exam Date
	//	if (!$scope.newDet.ExamDate_TMP) {
	//		Swal.fire('Please! Select Exam Date');
	//		return false;
	//	}

	//	// Check Start Time
	//	if (!$scope.newDet.StartTime_TMP) {
	//		Swal.fire('Please! Enter Start Time');
	//		return false;
	//	}

	//	// Check End Time
	//	if (!$scope.newDet.EndTime_TMP) {
	//		Swal.fire('Please! Enter End Time');
	//		return false;
	//	}

	//	if ($scope.newDet.StartTime_TMP && $scope.newDet.EndTime_TMP) {
	//		// Direct string comparison for HH:MM format
	//		if ($scope.newDet.EndTime_TMP <= $scope.newDet.StartTime_TMP) {
	//			Swal.fire('End Time must be after Start Time');
	//			return false;
	//		}
	//	}

	//	// Check Result Date
	//	if (!$scope.newDet.ResultDate_TMP) {
	//		Swal.fire('Please! Select Result Date');
	//		return false;
	//	}

	//	// Check Result Time
	//	if (!$scope.newDet.ResultTime_TMP) {
	//		Swal.fire('Please! Enter Result Time');
	//		return false;
	//	}

	//	// VALIDATION ADDED: Check if Result Date exceeds Exam Date
	//	// Assuming ExamDate_TMP and ResultDate_TMP are Date objects or strings that can be parsed
	//	const examDate = new Date($scope.newDet.ExamDate_TMP);
	//	const resultDate = new Date($scope.newDet.ResultDate_TMP);

	//	// Clear time parts to compare only dates
	//	examDate.setHours(0, 0, 0, 0);
	//	resultDate.setHours(0, 0, 0, 0);

	//	// Check if Result Date is on or before Exam Date
	//	if (resultDate <= examDate) {
	//		Swal.fire('Result Date must be after the Exam Date');
	//		return false;
	//	}

	//	// Check Subject Included
	//	if (!$scope.newDet.SubjectColl || $scope.newDet.SubjectColl.length === 0) {
	//		Swal.fire('Please! Select at least one Subject');
	//		return false;
	//	}

	//	// Check Full Marks
	//	if ($scope.newDet.FullMarks === null || $scope.newDet.FullMarks === undefined || $scope.newDet.FullMarks === '') {
	//		Swal.fire('Please! Enter Full Marks');
	//		return false;
	//	}

	//	// Check Pass Marks
	//	if ($scope.newDet.PassMarks === null || $scope.newDet.PassMarks === undefined || $scope.newDet.PassMarks === '') {
	//		Swal.fire('Please! Enter Pass Marks');
	//		return false;
	//	}

	//	// Additional validation: Pass Marks should not exceed Full Marks
	//	if (parseFloat($scope.newDet.PassMarks) > parseFloat($scope.newDet.FullMarks)) {
	//		Swal.fire('Pass Marks cannot exceed Full Marks');
	//		return false;
	//	}

	//	// Check Venue
	//	if (!$scope.newDet.Venue || $scope.newDet.Venue.trim() === '') {
	//		Swal.fire('Please! Enter Venue');
	//		return false;
	//	}

	//	return true; // All validations passed
	//};


	$scope.copyToClassWiseList = function () {
		if (!$scope.newDet.ForClassWise || !$scope.newDet.ClassWiseEntranceSetupList) return;

		$scope.newDet.ClassWiseEntranceSetupList.forEach(function (cl) {
			cl.ExamName = $scope.newDet.ExamName;
			cl.ExamDate_TMP = $scope.newDet.ExamDate_TMP;
			cl.ExamDateDet = angular.copy($scope.newDet.ExamDateDet);
			cl.StartTime_TMP = $scope.newDet.StartTime_TMP;
			cl.EndTime_TMP = $scope.newDet.EndTime_TMP;
			cl.ResultDate_TMP = $scope.newDet.ResultDate_TMP;
			cl.ResultDateDet = angular.copy($scope.newDet.ResultDateDet);
			cl.ResultTime_TMP = $scope.newDet.ResultTime_TMP;
			cl.SubjectColl = angular.copy($scope.newDet.SubjectColl);
			cl.FullMarks = $scope.newDet.FullMarks;
			cl.PassMarks = $scope.newDet.PassMarks;
			cl.Venue = $scope.newDet.Venue;
		});
	};


	$scope.SaveUpdateSetup = function () {
		if ($scope.IsValidConfig() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEntranceSetup();
					}
				});
			} else
				$scope.CallSaveUpdateEntranceSetup();

		}
	};

	
	$scope.CallSaveUpdateEntranceSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newDet.ExamDateDet)
			$scope.newDet.ExamDate = $filter('date')(new Date($scope.newDet.ExamDateDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newDet.StartTime_TMP)
			$scope.newDet.StartTime = $scope.newDet.StartTime_TMP.toLocaleString();


		if ($scope.newDet.EndTime_TMP)
			$scope.newDet.EndTime = $scope.newDet.EndTime_TMP.toLocaleString();

		angular.forEach($scope.newDet.ClassWiseEntranceSetupList, function (cl) {
			if (cl.ExamDateDet) {
				cl.ExamDate = $filter('date')(new Date(cl.ExamDateDet.dateAD), 'yyyy-MM-dd');
			}

			if (cl.StartTime_TMP)
				cl.StartTime = cl.StartTime_TMP.toLocaleString();

			if (cl.EndTime_TMP)
				cl.EndTime = cl.EndTime_TMP.toLocaleString();

			//Add by prashnt Chaitra 26
			if (cl.ResultDateDet && cl.ResultTime_TMP) {
				// Extract date from ResultDateDet
				const datePart = new Date(cl.ResultDateDet.dateAD);
				// Extract time from ResultTime_TMP
				const timePart = new Date(cl.ResultTime_TMP);
				// Combine date and time into a single Date object
				const combinedDateTime = new Date(
					datePart.getFullYear(),
					datePart.getMonth(),
					datePart.getDate(),
					timePart.getHours(),
					timePart.getMinutes(),
					timePart.getSeconds()
				);

				// Format the combined date and time
				cl.ResultDate = $filter('date')(combinedDateTime, 'yyyy-MM-dd HH:mm:ss');
			} else {
				cl.ResultDate = null;
			}

			if (cl.SubjectColl)
				cl.Subject = cl.SubjectColl.toString();
			else
				cl.Subject = '';

		});

		//Add by prashnt Chaitra 26
		if ($scope.newDet.ResultDateDet && $scope.newDet.ResultTime_TMP) {
			// Extract date from ResultDateDet
			const datePart = new Date($scope.newDet.ResultDateDet.dateAD);
			// Extract time from ResultTime_TMP
			const timePart = new Date($scope.newDet.ResultTime_TMP);
			// Combine date and time into a single Date object
			const combinedDateTime = new Date(
				datePart.getFullYear(),
				datePart.getMonth(),
				datePart.getDate(),
				timePart.getHours(),
				timePart.getMinutes(),
				timePart.getSeconds()
			);

			// Format the combined date and time
			$scope.newDet.ResultDate = $filter('date')(combinedDateTime, 'yyyy-MM-dd HH:mm:ss');
		} else {
			$scope.newDet.ResultDate = null;
		}

		if ($scope.newDet.SubjectColl)
			$scope.newDet.Subject = $scope.newDet.SubjectColl.toString();
		else
			$scope.newDet.Subject = '';

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveEntranceSetup",
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
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetEntranceConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetEntranceSetup",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;

				if ($scope.newDet.ExamDate)
					$scope.newDet.ExamDate_TMP = new Date($scope.newDet.ExamDate);

				if ($scope.newDet.StartTime)
					$scope.newDet.StartTime_TMP = new Date($scope.newDet.StartTime);

				if ($scope.newDet.EndTime)
					$scope.newDet.EndTime_TMP = new Date($scope.newDet.EndTime);


				var cRankList = mx($scope.newDet.ClassWiseEntranceSetupList);
				$scope.newDet.ClassWiseEntranceSetupList = [];
				angular.forEach($scope.ClassList, function (cl) {
					var find = cRankList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);
					var subjectArray = [];
					if (find && find.Subject) {
						subjectArray = find.Subject.split(',').map(Number);
					}
					$scope.newDet.ClassWiseEntranceSetupList.push({
						ClassId: cl.ClassId,
						text: cl.Name,
						ExamName: find ? find.ExamName : '',
						ExamDate_TMP: find && find.ExamDate ? new Date(find.ExamDate) : '',
						StartTime_TMP: find && find.StartTime ? new Date(find.StartTime) : '',
						EndTime_TMP: find && find.EndTime ? new Date(find.EndTime) : '',
						Venue: find ? find.Venue : '',
						ResultDate_TMP: find && find.ResultDate ? new Date(find.ResultDate) : '',
						ResultTime_TMP: find && find.ResultDate ? new Date(find.ResultDate) : '',
						FullMarks: find ? find.FullMarks : '',
						PassMarks: find ? find.PassMarks : '',
						SubjectColl: subjectArray
					});
				});
				//Add by Prashant Chaitra 26
				if ($scope.newDet.ResultDate) {
					$scope.newDet.ResultDate_TMP = new Date($scope.newDet.ResultDate);
					$scope.newDet.ResultTime_TMP = new Date($scope.newDet.ResultDate);
				}


				if ($scope.newDet.Subject) {
					$scope.newDet.SubjectColl = $scope.newDet.Subject.split(',').map(Number);

					setTimeout(function () {
						$('.select2').trigger('change');
					}, 100);
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.ShowPrintDialog = function () {
		$scope.newPrint = {
			DataColl: []
		};

		// Get all the selected rows from the grid
		var tmpCheckedData = $scope.gridApi.selection.getSelectedRows();
		for (let i = 0; i < tmpCheckedData.length; i++) {
			$scope.newPrint.DataColl.push(tmpCheckedData[i]);
		}

		if ($scope.newPrint.DataColl.length == 0) {
			Swal.fire('Please select data from the list to print admit card');
			return;
		} else {
			$('#admitcardmodal').modal('show');
		}
	};

	$scope.PrintData = function () {
		$('.CardSection').printThis();
	}

	$scope.validateDates = function (changedField) {
		if (!$scope.newEnquiry.FromDateDet || !$scope.newEnquiry.ToDateDet ||
			!$scope.newEnquiry.FromDateDet.dateAD || !$scope.newEnquiry.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newEnquiry.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newEnquiry.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be greater than To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newEnquiry.FromDate_TMP = new Date();
						$scope.newEnquiry.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be less than From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newEnquiry.ToDate_TMP = new Date();
						$scope.newEnquiry.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});