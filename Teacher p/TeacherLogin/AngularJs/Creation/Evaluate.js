app.controller('EvaluateController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Question Setup';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Evaluate: 1,
			ExamList:1
		};

		$scope.searchData = {
			Evaluate: '',
			ExamList:''
		};
		$scope.sortData = {
			Evaluate: '',
			ExamList: ''
		};
		$scope.ascDscData = {
			Evaluate: '',
			ExamList: ''
		};

		$scope.perPage = {
			Evaluate: GlobalServices.getPerPageRow(),
			ExamList: GlobalServices.getPerPageRow(),
		};

		$scope.newEvaluate = {
			EvaluateId: null,
			
			Mode: 'Save'
		};

		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				exDialog.openMessage({
					scope: $scope,
					title: $scope.Title,
					icon: "info",
					message: 'Failed: ' + reason
				});
			});

		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/ExamTypeforEntity")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
		//$scope.GetAllEvaluateList();

	};
	$scope.sortEv = function (keyname) {
		$scope.sortData.Evaluate = keyname;   //set the sortKey to the param passed
		$scope.ascDscData.Evaluate = !$scope.ascDscData.Evaluate; //if true make it false and vice versa
	}
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if ($scope.newEvaluate.ClassId) {
			var para = {
				classId: $scope.newEvaluate.ClassId,
				sectionIdColl: ''
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectColl = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.GetAllExamList = function () {

		if ($scope.newEvaluate.ClassId && $scope.newEvaluate.SubjectId && $scope.newEvaluate.ExamTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.ExamList = [];
			var para = {
				DateFrom: $filter('date')(new Date($scope.newEvaluate.DateFromDet.dateAD), 'yyyy-MM-dd'),
				DateTo: $filter('date')(new Date($scope.newEvaluate.DateToDet.dateAD), 'yyyy-MM-dd'),
				ExamTypeId: $scope.newEvaluate.ExamTypeId,
				ClassId: $scope.newEvaluate.ClassId,
				SectionId: null,
				SubjectId: $scope.newEvaluate.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Reporting/GetOnlineExamListForEvaluate",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.ExamList = res.data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		

	}

	$scope.CurExamSetup = null;
	$scope.CurStudent = null;
	$scope.GetStudentList = function (examDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurExamSetup = examDet;
		$scope.CurStudent = null;
		$scope.PresentStudentList = [];
		$scope.AbsentStudentList = [];

		var para = {
			examSetupId: examDet.ExamSetupId,
			classId: examDet.ClassId,
			sectionId:examDet.SectionId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Reporting/GetStudentForEvaluate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				var tmpDataColl = res.data;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.QuestionAttampt && dc.QuestionAttampt > 0)
						$scope.PresentStudentList.push(dc);
					else
						$scope.AbsentStudentList.push(dc);
				});

				$scope.CurExamSetup.TotalStudent = tmpDataColl.length;
				$scope.CurExamSetup.TotalPresent = $scope.PresentStudentList.length;
				$scope.CurExamSetup.TotalAbsent = $scope.AbsentStudentList.length;

				document.getElementById('table-listing').style.display = "none";
			document.getElementById('detail').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentAnswerList = function (studentDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();		
		$scope.CurStudent = studentDet;
		$scope.CurStudent.ObjectiveList = [];
		$scope.CurStudent.SubjectiveList = [];
		$scope.CurStudent.CategoryWiseObjectiveList = [];
		$scope.CurStudent.TotalObjetiveQ = 0;
		$scope.CurStudent.TotalObjetiveA = 0;
		$scope.CurStudent.TotalObjetiveC = 0;
		$scope.CurStudent.TotalObjetiveW = 0;
		$scope.CurStudent.TotalSubjectiveA = 0;
		$scope.CurStudent.TotalSubjectiveL = 0;
		var para = {
			examSetupId: $scope.CurExamSetup.ExamSetupId,
			studentId:studentDet.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Reporting/GetStudentAnswerListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				var tmpDataColl = res.data;

				var objColl = [];
				var totalAswer = 0, totalCorrect = 0, totalWrong = 0;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.QuestionPath && dc.QuestionPath.length > 0)
						dc.QuestionPath = WEBURLPATH.trim() + dc.QuestionPath.trim();

					if (dc.ExamModal == 1) {

						if ((dc.AnswerText && dc.AnswerText.length > 0) || (dc.StudentDocColl && dc.StudentDocColl.length > 0)) {
							$scope.CurStudent.TotalSubjectiveA = $scope.CurStudent.TotalSubjectiveA+1;
                        }else						
							$scope.CurStudent.TotalSubjectiveL = $scope.CurStudent.TotalSubjectiveL+1;

						dc.AnswerFiles = [];

						if (dc.StudentDocColl && dc.StudentDocColl.length > 0) {
							var fInd = 0;
							angular.forEach(dc.StudentDocColl, function (doc) {
								dc.AnswerFiles.push({
									AnswerFiles: (WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/'),
									Index: fInd,
									FileName: 'Answer' + fInd,
									FName: new URL((WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/')).pathname.split('/').pop()
								});
								fInd++;
							});
						}

						$scope.CurStudent.SubjectiveList.push(dc);
					} else {

						dc.ObtainMark = 0;
						if (dc.StudentAnswerNo>0)
							$scope.CurStudent.TotalObjetiveA = $scope.CurStudent.TotalObjetiveA + 1;


						if (dc.IsCorrect == true) {
							$scope.CurStudent.TotalObjetiveC = $scope.CurStudent.TotalObjetiveC + 1;
							dc.ObtainMark = dc.Marks;
						} else if (dc.StudentAnswerNo > 0 && dc.IsCorrect == false)					
							$scope.CurStudent.TotalObjetiveW = $scope.CurStudent.TotalObjetiveW + 1;
						
							

						angular.forEach(dc.DetailsColl, function (dt) {
							dt.ObtainMark = 0;
							dt.CorrectAnswer = 0;
							dt.ShowFile = 0;

							if (dt.FilePath && dt.FilePath.length > 0) {
								var fl = dt.FilePath.toString();
								if (fl.indexOf(".jpg") > 0 || fl.indexOf(".jpeg") > 0 || fl.indexOf(".png") > 0)
									dt.ShowFile = 1;
								else if (fl.indexOf(".pdf") > 0)
									dt.ShowFile = 2;
								else if (fl.indexOf(".mp3") > 0 || fl.indexOf(".mp4") > 0 || fl.indexOf(".wav") > 0)
									dt.ShowFile = 3;
                            }
							
							if (dt.SNo == dc.StudentAnswerNo && dt.IsCorrect == true) {
								dt.CorrectAnswer = 1;
								dt.ObtainMark = dc.Marks;
							} else if (dt.SNo == dc.StudentAnswerNo && dt.IsCorrect == false) {
								dt.CorrectAnswer = 2;								
							}
							
								
						});

						objColl.push(dc);
						//$scope.CurStudent.ObjectiveList.push(dc);
                    }
						

				});

				$scope.CurStudent.TotalObjetiveQ = objColl.length;

				var categoryWise = mx(objColl).groupBy(t => t.CategoryName).toArray();
				var sno = 1;
				angular.forEach(categoryWise, function (q)
				{
					var beData = {
						SNo: sno,
						CategoryName: q.key,						
						ObjectiveList: []
					};
					
					angular.forEach(q.elements, function (el) {
						beData.ObjectiveList.push(el);						
					})
					sno++;

					$scope.CurStudent.CategoryWiseObjectiveList.push(beData);
				})	

				document.getElementById('check').style.display = "block";
			document.getElementById('detail').style.display = "none";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ClearEvaluate = function () {
		$scope.newEvaluate = {
			EvaluateId: null,
			
			Mode: 'Save'
		};
	};



	function OnClickDefault() {
		document.getElementById('detail').style.display = "none";
		document.getElementById('check').style.display = "none";   

		//document.getElementById('detail-btn').onclick = function () {
		//	document.getElementById('table-listing').style.display = "none";
		//	document.getElementById('detail').style.display = "block";
		//}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('detail').style.display = "none";
		}

		//document.getElementById('check-btn').onclick = function () {
		//	document.getElementById('check').style.display = "block";
		//	document.getElementById('detail').style.display = "none";
		//}

		document.getElementById('back-detail-btn').onclick = function () {

			document.getElementById('detail').style.display = "block";
			document.getElementById('check').style.display = "none";
		}

	};


	$(document).ready(function () {
		$('#negativemarkCheckbox').change(function () {
			$('#negativemarkCheckboxdiv').toggle();
		});
	});


	//$scope.IsValidEvaluate = function () {
	//	if ($scope.newEvaluate.Lesson.isEmpty()) {
	//		Swal.fire('Please ! Enter Lesson');
	//		return false;
	//	}
	//	return true;
	//};


	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newEvaluate.AttachmentColl) {
			if ($scope.newEvaluate.AttachmentColl.length > 0) {
				$scope.newEvaluate.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newEvaluate.AttachmentColl.push({
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
			}
		}
	};



	$scope.SaveUpdateEvaluate = function () {
		if ($scope.IsValidEvaluate() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEvaluate.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEvaluate();
					}
				});
			} else
				$scope.CallSaveUpdateEvaluate();

		}
	};

	$scope.CallSaveUpdateEvaluate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newEvaluate.EvaluateDateDet) {
			$scope.newEvaluate.EvaluateDate = $scope.newEvaluate.EvaluateDateDet.dateAD;
		} else
			$scope.newEvaluate.EvaluateDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEvaluate",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEvaluate }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEvaluate();
				$scope.GetAllEvaluateList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllEvaluateList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EvaluateList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllEvaluateList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EvaluateList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetEvaluateById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EvaluateId: refData.EvaluateId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetEvaluateById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEvaluate = res.data.Data;
				$scope.newEvaluate.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEvaluateById = function (refData) {

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
					EvaluateId: refData.EvaluateId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelEvaluate",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEvaluateList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.downloadFilePath = '';
	$scope.DocumentAtt_Toggle = function (fpath) {
		if (fpath && fpath !== '') {
			$scope.downloadFilePath =  fpath;
			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src =  fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}

	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.CurrentQuestionForCheck = {};
	$scope.checkCopy = function (qDet)
	{
		$scope.CurrentQuestionForCheck = qDet;
	
		$scope.CurrentQuestionForCheck.SelectedIndex = 0;

		if (qDet.AnswerFiles && qDet.AnswerFiles.length > 0)
		{			
			$scope.InitImgEditor(qDet.AnswerFiles[0].AnswerFiles);

			document.getElementById('attachment-check-section').style.display = "block";
			document.getElementById('Show-Check-File').style.display = "none";
        }
			
	}
	var imageEditor = null;
	$scope.InitImgEditor = function (imgPath) {

		var imgInd = imgPath.indexOf('?');
		if (imgInd > 0)
			imgPath = imgPath.substring(0, imgInd);

		imageEditor = new tui.ImageEditor('#tui-image-editor-container', {
			includeUI: {
				menu: [
					'draw', 'text', 'rotate', 'flip', 'crop', 'shape'
				],
				loadImage: {
					path: imgPath,
					name: 'SampleImage',
				},
				theme: whiteTheme, // or whiteTheme
				// initMenu: 'draw',
				menuBarPosition: 'right',
				draw: {
					color: '#00a9ff',
					opacity: 1.0,
					range: {
						value: 8
					}
				},
			},
			cssMaxWidth: 700,
			// cssMaxHeight: 500,
			usageStatistics: false,
			selectionStyle: {}
		});
		imageEditor.startDrawingMode('FREE_DRAWING', {
			width: 5,
			color: 'rgba(255,0,0,0.9)',
			arrowType: {
				tail: 'chevron' // triangle
			}
		});


	}
	$scope.ChangeImage = function (sfl) {

		if ($scope.CurrentQuestionForCheck) {
			var dataURL = imageEditor.toDataURL({ format: 'jpeg', quality: 0.8 });
			var flll = dataURItoFile(dataURL);
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].DATAURL = dataURL;
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].File = flll;
			$scope.CurrentQuestionForCheck.SelectedIndex = sfl.Index;

			if (sfl.File)
				imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
			else
				imageEditor.loadImageFromURL(sfl.AnswerFiles, sfl.FileName);
		}
	};
	$scope.NextPreviewImage = function (val) {
		if ($scope.CurrentQuestionForCheck) {
			var dataURL = imageEditor.toDataURL({ format: 'jpeg', quality: 0.8 });
			var flll = dataURItoFile(dataURL);
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].DATAURL = dataURL;
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].File = flll;

			var nInd = $scope.CurrentQuestionForCheck.SelectedIndex + val;

			if (nInd > -1 && nInd < $scope.CurrentQuestionForCheck.AnswerFiles.length) {
				$scope.CurrentQuestionForCheck.SelectedIndex = nInd;
				var sfl = $scope.CurrentQuestionForCheck.AnswerFiles[nInd];
				if (sfl.File)
					imageEditor.loadImageFromFile(sfl.File, sfl.FileName);
				else
					imageEditor.loadImageFromURL(sfl.AnswerFiles, sfl.FileName);
			}

		}
	};

	var BASE64_MARKER = ';base64,';

	// Does the given URL (string) look like a base64-encoded URL?
	function isDataURI(url) {
		return url.split(BASE64_MARKER).length === 2;
	};
	function dataURItoFile(dataURI) {
		if (!isDataURI(dataURI)) { return false; }

		// Format of a base64-encoded URL:
		// data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
		var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
		var filename = 'rc-' + (new Date()).getTime() + '.' + mime.split('/')[1];
		//var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
		var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
		var writer = new Uint8Array(new ArrayBuffer(bytes.length));

		for (var i = 0; i < bytes.length; i++) {
			writer[i] = bytes.charCodeAt(i);
		}

		return new File([writer.buffer], filename, { type: mime });
	}
	$scope.photoBack = function () {
		document.getElementById('Show-Check-File').style.display = "block";
		document.getElementById('attachment-check-section').style.display = "none";

	}
	$scope.ExamCopyCheck = function () {


		if ($scope.CurrentQuestionForCheck )
		{
			//if (!$scope.CurrentQuestionForCheck.Remarks || $scope.CurrentQuestionForCheck.Remarks.length == 0) {
			//	Swal.fire('Please ! Enter Remarks');
			//	return;
			//}

			if (!$scope.CurrentQuestionForCheck.Marks) {
				Swal.fire('No Full Marks');
				return;
			}

			var obMark = $scope.CurrentQuestionForCheck.ObtainMark ;

			if ($scope.CurrentQuestionForCheck.Marks<obMark) {
				Swal.fire('Please ! Enter Obtain Mark Less Then Equal Full Mark');
				return;
			}
			
			var dataURL = imageEditor.toDataURL({ format: 'jpeg', quality: 0.8 });
			var flll = dataURItoFile(dataURL);
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].DATAURL = dataURL;
			$scope.CurrentQuestionForCheck.AnswerFiles[$scope.CurrentQuestionForCheck.SelectedIndex].File = flll;

			var checkHome = {
				OETranId: $scope.CurrentQuestionForCheck.OETranId,
				Remarks: $scope.CurrentQuestionForCheck.Remarks,
				ObtainMark: ($scope.CurrentQuestionForCheck.ObtainMark ? $scope.CurrentQuestionForCheck.ObtainMark : 0 ),
				FilesColl: []
			};

			var attFiles = [];
			angular.forEach($scope.CurrentQuestionForCheck.AnswerFiles, function (fl) {
				if (fl.File) {
					checkHome.FilesColl.push(fl.FName);
					attFiles.push(fl.File);
                }					
			});

			$scope.loadingstatus = "running";


			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Reporting/ExamCopyCheck",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					for (var f = 0; f < data.files.length; f++)
						formData.append("file" + f.toString(), data.files[f]);

					return formData;
				},
				data: { jsonData: checkHome, files: attFiles }
			}).then(function (res) {

				$scope.loadingstatus = "stop";

				if (res.data.ResponseMSG) {

					Swal.fire(res.data.ResponseMSG);

					if (res.data.IsSuccess == true) {
						
					}
				} else {
					Swal.fire(res.data);
				}

			}, function (errormessage) {

				$scope.loadingstatus = "stop";

			});

		} else
			Swal.fire('Please ! Select Status');

	}

	$scope.ExamCopyMark = function (qst) {


		if (qst) {
			
			if (!qst.Marks) {
				Swal.fire('No Full Marks');
				return;
			}

			var obMark = qst.ObtainMark ;

			if (qst.Marks < obMark) {
				Swal.fire('Please ! Enter Obtain Mark Less Then Equal Full Mark');
				return;
			}

		
			var checkHome = {
				OETranId: qst.OETranId,
				Remarks: qst.Remarks,
				ObtainMark: obMark,
				FilesColl: []
			};

			var attFiles = [];
			
			$scope.loadingstatus = "running";


			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Reporting/ExamCopyCheck",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					for (var f = 0; f < data.files.length; f++)
						formData.append("file" + f.toString(), data.files[f]);

					return formData;
				},
				data: { jsonData: checkHome, files: attFiles }
			}).then(function (res) {

				$scope.loadingstatus = "stop";

				if (res.data.ResponseMSG) {

					Swal.fire(res.data.ResponseMSG);

					if (res.data.IsSuccess == true) {

					}
				} else {
					Swal.fire(res.data);
				}

			}, function (errormessage) {

				$scope.loadingstatus = "stop";

			});

		} else
			Swal.fire('Please ! Select Status');

	}

	$scope.SendNotificationToAbsentStudent = function () {

		if ($scope.CurExamSetup.Notification) {

			var para = {
				title: 'Absent In Exam',
				description: $scope.CurExamSetup.Notification,
				studentIdColl:''
			};
			var StudentArray = []
			angular.forEach($scope.AbsentStudentList, function (st) {
				StudentArray.push(st.StudentId);		
			});
			para.studentIdColl = (StudentArray && StudentArray.length > 0 ? StudentArray.toString() : '');

			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Reporting/SendNotificationToAbsent",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));
					
					return formData;
				},
				data: { jsonData: para }
			}).then(function (res) {

				if (res.data.IsSuccess == true) {
					Swal.fire('Sent Successfully');				
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
			
        }
		
	};
});