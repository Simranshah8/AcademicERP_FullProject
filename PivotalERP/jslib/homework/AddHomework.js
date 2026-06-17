app.controller('AddHomeworkController', function ($scope, $http, $timeout, $filter, GlobalServices, $sce) {
	$scope.Title = 'AddHomework';
	OnClickDefault();
	var glbS = GlobalServices;

	$scope.LoadData = function () {

		$scope.HomeWork_Files_TMP = null;
		$scope.HomeWork_Files_Data = null;

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			AddHomework: 1

		};

		$scope.searchData = {
			AddHomework: '',
		};

		$scope.perPage = {
			AddHomework: GlobalServices.getPerPageRow(),
		};


		$scope.Configuration = {};
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetHAConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";


			if (res.data.IsSuccess && res.data.Data) {
				$scope.Configuration = res.data.Data;
			}
			else {
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
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//student list
		$scope.ClassListsection = [];
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassListsection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.SubjectList = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Academic/Creation/GetAllSubjectList",
		//	dataType: "json"
		//}).then(function (res) {		 
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.SubjectList = res.data.Data;

		//	} else {

		//		if (res.data.IsSuccess == false)
		//			Swal.fire(res.data.ResponseMSG);
		//	}

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		//$scope.SubjectList = {};
		//glbS.getSubjectList().then(function (res) {
		//	$scope.SubjectList = mx(res.data.Data);
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		//class List
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



		//section
		$scope.SectionList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSectionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SectionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//subject
		$scope.SubjectList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//homeworkType
		$scope.HomeworkTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllHomeworkTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HomeworkTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newFilter = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			StudentId: null,
			ClassId: null,
			SubjectId: null,
			EmployeeId: null
		};

		$scope.newAddHomework = {
			DeadlineDate_TMP: new Date(),
			HomeworkId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Subject: [],
			Lesson: '',
			Topic: '',
			HomeworkTypeId: null,
			Description: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineforRedo: null,
			DeadlineforRedoTime: null,
			HomeworkFile: '',
			SubmissionsRequired: false,
			IsAllowLateSibmission: false,
			HomeWork_Files_TMP: '',
			HomeWork_Files_Data: '',
			AttachmentColl: [],
			SectionListName: '',
			SubjectName: '',
			Mode: 'Save'
		};
		$scope.GetTeacherNameList();
		//$scope.GetAllAddHomework();
	}

	function OnClickDefault() {
		document.getElementById('homework-add-form').style.display = "none";

		document.getElementById('add-homework-add-btn').onclick = function () {
			document.getElementById('homework-add-section').style.display = "none";
			document.getElementById('homework-add-form').style.display = "block";
		}

		document.getElementById('back-homework-btn').onclick = function () {
			document.getElementById('homework-add-section').style.display = "block";
			document.getElementById('homework-add-form').style.display = "none";
		}

	}

	//$scope.ClearAddHomework = function () {
	//	$scope.newAddHomework = {
	//		HomeworkId: null,
	//		TeacherId: null,
	//		ClassId: null,
	//		SectionId: null,
	//		Section: [],
	//		SubjectId: null,
	//		Lesson: '',
	//		Topic: '',
	//		HomeworkTypeId: null,
	//		Description: '',
	//		DeadlineDate: null,
	//		DeadlineTime: null,
	//		DeadlineforRedo: null,
	//		DeadlineforRedoTime: null,
	//		HomeworkFile: '',
	//		SubmissionsRequired: false,
	//		IsAllowLateSibmission: false,
	//		AttachmentColl:[],
	//		Mode: 'Save'


	//	};
	//	$scope.SelectedFilesInfo = [];
	//	$scope.FileExtensionsInfo = [];
	//	$scope.FileNames = [];
	//	$scope.HomeWork_Files_TMP = [];
	//	$scope.HomeWork_Files_Data = [];

	//	// Reset attachment-related variables
	//	$scope.existingAttachments = [];
	//	$scope.attachmentCount = 0;

	//	// Reset file input
	//	angular.element('#flHomeWork').val(null);
	//}


	//Teacher name

	$scope.GetTeacherNameList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherNameList = [];
		$http({
			method: 'GET',
			url: base_url + "HomeWork/Transaction/GetTeacherName",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TeacherNameList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//*********************class wise section name********************************

	$scope.GetTeacherWiseClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			EmployeeId: $scope.newAddHomework.TeacherId
		};
		$scope.TeacherWiseClassList = [];
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetTeacherWiseClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TeacherWiseClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//**********************************************************
	$scope.GetClassWiseSubjectListForALP = function () {
		$scope.SubjectListALP = [];
		var para = {
			ClassId: $scope.newAddHomework.SelectedClass.ClassId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.SubjectListALP = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetSubjectLessonWise = function () {
		if ($scope.newAddHomework.SubjectId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newAddHomework.SelectedClass.ClassId,
				SubjectId: $scope.newAddHomework.SubjectId
			};
			$scope.SubjectLessonWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetLessonTopicDetailsWise = function (lessonName) {
		if (lessonName) {
			// Find the LessonId based on the selected LessonName
			var selectedLesson = $scope.SubjectLessonWiseList.find(function (lesson) {
				return lesson.LessonName === lessonName;
			});

			// If found, proceed with the LessonId
			if (selectedLesson) {
				var lessonId = selectedLesson.LessonId;

				// Now you can send the LessonId
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					LessonId: lessonId
				};
				$scope.LessonTopicDetailsWiseList = [];
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/GetLessonTopicDetailsWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess && res.data.Data) {
						$scope.LessonTopicDetailsWiseList = res.data.Data;
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		}
	};


	$scope.ClearAddHomework = function () {
		// Clear preview attachments and revoke object URLs for new files
		if ($scope.previewAttachments) {
			angular.forEach($scope.previewAttachments, function (file) {
				if (!file.isExisting && file.previewUrl && file.previewUrl.startsWith('blob:')) {
					URL.revokeObjectURL(file.previewUrl);
				}
			});
		}

		$scope.newAddHomework = {
			HomeworkId: null,
			TeacherId: null,
			ClassId: null,
			SectionId: null,
			Section: [],
			SubjectId: null,
			Lesson: '',
			Topic: '',
			HomeworkTypeId: null,
			Description: '',
			DeadlineDate: null,
			DeadlineTime: null,
			DeadlineforRedo: null,
			DeadlineforRedoTime: null,
			HomeworkFile: '',
			SubmissionsRequired: false,
			IsAllowLateSibmission: false,
			AttachmentColl: [],
			SectionListName: '',
			SubjectName: '',
			Mode: 'Save'
		};

		$scope.SelectedFilesInfo = [];
		$scope.HomeWork_Files_TMP = [];
		$scope.HomeWork_Files_Data = [];
		$scope.previewAttachments = [];
		$scope.deletedAttachments = [];
		$scope.attachmentCount = 0;

		// Reset file input
		var fileInput = document.getElementById('flHomeWork');
		if (fileInput) {
			fileInput.value = '';
		}
		var label = document.querySelector('.custom-file-label');
		if (label) {
			label.textContent = 'Choose files';
		}
	};


	$scope.GetClassWiseSubMap = function () {
		$scope.newAddHomework.SubjectList = [];

		if ($scope.newAddHomework?.SelectedClass?.ClassId > 0) {

			var para = {
				ClassId: $scope.newAddHomework.SelectedClass.ClassId,
				SectionIdColl: ($scope.newAddHomework.SectionId ? $scope.newAddHomework.SectionId.toString() : ''),
				SemesterId: $scope.newAddHomework.SemesterId || null,
				ClassYearId: $scope.newAddHomework.ClassYearId || null,
				BatchId: $scope.newAddHomework.BatchId || null,
				BranchId: null
			};

			showPleaseWait();
			$scope.loadingstatus = "running";

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {

				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {

					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length > 0) {

						angular.forEach(SubjectMappingColl, function (sm) {

							// 🔹 Replace firstOrDefault with find()
							var subDet = $scope.SubjectList.find(function (p1) {
								return p1.SubjectId == sm.SubjectId;
							});

							if (subDet) {
								var newObj = angular.copy(subDet);
								$scope.newAddHomework.SubjectList.push(newObj);
							}

						});
					}
				}
				else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				hidePleaseWait();
				Swal.fire('Failed ' + reason);
			});
		}
	};

	//************************ Add Homework ******************************  
	$scope.getExistingAttachmentsCount = function () {
		if (!$scope.previewAttachments) return 0;
		return $scope.previewAttachments.filter(function (file) {
			return file.isExisting === true;
		}).length;
	};

	$scope.getNewAttachmentsCount = function () {
		if (!$scope.previewAttachments) return 0;
		return $scope.previewAttachments.filter(function (file) {
			return file.isExisting === false;
		}).length;
	};

	$scope.hasExistingAttachments = function () {
		return $scope.getExistingAttachmentsCount() > 0;
	};

	$scope.hasNewAttachments = function () {
		return $scope.getNewAttachmentsCount() > 0;
	};

	$scope.IsValidHomeworkType = function () {
		// Validate Teacher Name (required field)
		if (!$scope.newAddHomework.TeacherId || $scope.newAddHomework.TeacherId == 0) {
			Swal.fire('Please select Teacher Name');
			return false;
		}

		// Validate Class selection
		if (!$scope.newAddHomework.SelectedClass && $scope.newAddHomework.SelectedClass !== 0) {
			Swal.fire('Please select Class');
			return false;
		}

		// Validate Subject
		if (!$scope.newAddHomework.SubjectId || $scope.newAddHomework.SubjectId == 0) {
			Swal.fire('Please select Subject');
			return false;
		}

		// Validate Homework Type
		if (!$scope.newAddHomework.HomeworkTypeId || $scope.newAddHomework.HomeworkTypeId == 0) {
			Swal.fire('Please select Homework Type');
			return false;
		}

		return true;
	}
	$scope.SaveUpdateAddHomework = function () {
		if ($scope.IsValidHomeworkType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddHomework.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHomework();
					}
				});
			} else
				$scope.CallSaveUpdateHomework();
		}
	};

	function getFileExtension(filename) {
		return filename.split('.').pop().toLowerCase();
	}

	// Helper function to get filename without extension
	function getFileNameWithoutExtension(filename) {
		return filename.substring(0, filename.lastIndexOf('.')) || filename;
	}

	//function dataURItoFile(dataURI, fileInfo) {
	//	var byteString = atob(dataURI.split(',')[1]);
	//	var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
	//	var ab = new ArrayBuffer(byteString.length);
	//	var ia = new Uint8Array(ab);
	//	for (var i = 0; i < byteString.length; i++) {
	//		ia[i] = byteString.charCodeAt(i);
	//	}
	//	var blob = new Blob([ab], { type: mimeString });
	//	var fileName = fileInfo ? fileInfo.name : ('file.' + mimeString.split('/')[1]);
	//	return new File([blob], fileName, { type: mimeString });
	//}


	$scope.GetAllAddHomework1 = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddedHomeworkList = [];
		var para = {
			DateFrom: ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			DateTo: ($scope.newFilter.ToDateDet ? $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
			StudentId: $scope.newFilter.StudentId,
			ClassId: ($scope.newFilter.SelectedClass && $scope.newFilter.SelectedClass.ClassId) ? $scope.newFilter.SelectedClass.ClassId : null,
			SubjectId: $scope.newFilter.SubjectId,
			EmployeeId: $scope.newFilter.TeacherId,
			BatchId: $scope.newFilter.BatchId,
			SemesterId: $scope.newFilter.SemesterId,
			ClassYearId: $scope.newFilter.ClassYearId
		};
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetAllAddHomework",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AddedHomeworkList = res.data.Data;
			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetAddHomeworkById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			HomeWorkId: refData.HomeWorkId
		};
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetAddedHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAddHomework = res.data.Data;
				if ($scope.previewAttachments) {
					angular.forEach($scope.previewAttachments, function (file) {
						if (!file.isExisting && file.previewUrl && file.previewUrl.startsWith('blob:')) {
							URL.revokeObjectURL(file.previewUrl);
						}
					});
				}
				$scope.previewAttachments = [];
				$scope.SelectedFilesInfo = [];
				$scope.HomeWork_Files_TMP = [];
				$scope.HomeWork_Files_Data = [];
				$scope.deletedAttachments = [];
				if ($scope.newAddHomework.AttachmentColl && $scope.newAddHomework.AttachmentColl.length > 0) {
					angular.forEach($scope.newAddHomework.AttachmentColl, function (doc) {
						if (doc.DocPath) {
							var cleanPath = doc.DocPath.replace(/\\/g, '/');
							var fpath = cleanPath.startsWith('http') ? cleanPath : (WEBURLPATH.trim() + cleanPath).replace(/\\/g, '/');

							var fileExt = doc.Extension ?
								doc.Extension.toLowerCase().replace('.', '') :
								fpath.split('.').pop().toLowerCase();


							var fileInfo = {
								DocumentId: doc.DocumentId,
								name: doc.Name || 'Attachment',
								fileNameWithoutExt: getFileNameWithoutExtension(doc.Name || 'Attachment'),
								extension: fileExt,
								extensionWithDot: doc.Extension || '.' + fileExt,
								type: getMimeType(fileExt),
								size: doc.FileSize || 0,
								previewUrl: fpath,
								fileType: getFileCategory(fileExt),
								isExisting: true,
								DocPath: doc.DocPath,
								FullPath: fpath
							};

							$scope.previewAttachments.push(fileInfo);
						}
					});
				}

				// Calculate attachment count
				$scope.attachmentCount = $scope.previewAttachments.length;
				// Handle dates
				if ($scope.newAddHomework.DeadlineDate)
					$scope.newAddHomework.DeadlineDate_TMP = new Date($scope.newAddHomework.DeadlineDate);

				if ($scope.newAddHomework.DeadlineTime)
					$scope.newAddHomework.DeadlineTime_TMP = new Date($scope.newAddHomework.DeadlineTime);

				if ($scope.newAddHomework.DeadlineforRedo)
					$scope.newAddHomework.DeadlineforRedo_TMP = new Date($scope.newAddHomework.DeadlineforRedo);

				if ($scope.newAddHomework.DeadlineforRedoTime)
					$scope.newAddHomework.DeadlineForReDoTime_TMP = new Date($scope.newAddHomework.DeadlineforRedoTime);

				// Set selected class
				if ($scope.ClassSection && $scope.ClassSection.ClassList) {
					$scope.newAddHomework.SelectedClass = $scope.ClassSection.ClassList.find(
						cl => cl.ClassId === res.data.Data.ClassId
					);
				}

				$timeout(function () {
					$('.select2').each(function () {
						if ($(this).hasClass('select2-hidden-accessible')) {
							$(this).select2('destroy');
						}
						$(this).select2({ width: '100%' });
					});
				}, 0);

				$scope.newAddHomework.Mode = 'Modify';
				document.getElementById('homework-add-section').style.display = "none";
				document.getElementById('homework-add-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	function getMimeType(extension) {
		var mimeTypes = {
			'jpg': 'image/jpeg',
			'jpeg': 'image/jpeg',
			'png': 'image/png',
			'gif': 'image/gif',
			'bmp': 'image/bmp',
			'webp': 'image/webp',
			'pdf': 'application/pdf',
			'doc': 'application/msword',
			'docx': 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
			'xls': 'application/vnd.ms-excel',
			'xlsx': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
			'ppt': 'application/vnd.ms-powerpoint',
			'pptx': 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
			'txt': 'text/plain'
		};
		return mimeTypes[extension.toLowerCase()] || 'application/octet-stream';
	}

	$scope.previewAttachments = [];
	//$scope.removeFile = function (index) {
	//	var file = $scope.previewAttachments[index];
	//	var isExisting = file.isExisting;

	//	Swal.fire({
	//		title: 'Remove File',
	//		text: 'Are you sure you want to remove this ' + (isExisting ? 'existing' : '') + ' file?',
	//		icon: 'warning',
	//		showCancelButton: true,
	//		confirmButtonText: 'Yes, remove it',
	//		cancelButtonText: 'Cancel'
	//	}).then((result) => {
	//		if (result.isConfirmed) {
	//			if (!isExisting && file.previewUrl && file.previewUrl.startsWith('blob:')) {
	//				URL.revokeObjectURL(file.previewUrl);
	//			}

	//			if (isExisting && file.DocumentId) {
	//				if (!$scope.deletedAttachments) {
	//					$scope.deletedAttachments = [];
	//				}
	//				$scope.deletedAttachments.push(file.DocumentId);
	//				console.log("Marked for deletion - DocumentId:", file.DocumentId);
	//			}

	//			$scope.previewAttachments.splice(index, 1);
	//			if (!isExisting) {
	//				for (var i = 0; i < $scope.SelectedFilesInfo.length; i++) {
	//					if ($scope.SelectedFilesInfo[i].name === file.name &&
	//						$scope.SelectedFilesInfo[i].size === file.size) {
	//						$scope.SelectedFilesInfo.splice(i, 1);
	//						break;
	//					}
	//				}

	//				if ($scope.HomeWork_Files_TMP && $scope.HomeWork_Files_TMP.length > 0) {
	//					for (var j = 0; j < $scope.HomeWork_Files_TMP.length; j++) {
	//						if ($scope.HomeWork_Files_TMP[j].name === file.name &&
	//							$scope.HomeWork_Files_TMP[j].size === file.size) {
	//							$scope.HomeWork_Files_TMP.splice(j, 1);
	//							$scope.HomeWork_Files_Data.splice(j, 1);
	//							break;
	//						}
	//					}
	//				}
	//			}

	//			$scope.attachmentCount = $scope.previewAttachments.length;
	//			if (!isExisting && $scope.previewAttachments.filter(f => !f.isExisting).length === 0) {
	//				var fileInput = document.getElementById('flHomeWork');
	//				if (fileInput) {
	//					fileInput.value = '';
	//				}
	//				var label = document.querySelector('.custom-file-label');
	//				if (label) {
	//					label.textContent = 'Choose files';
	//				}
	//			}		
	//			Swal.fire('Removed!', 'File has been ' + (isExisting ? 'marked for deletion' : 'removed') + '.', 'success');
	//		}
	//	});
	//};

	$scope.removeFile = function (index, $event) {
		if ($event) {
			$event.preventDefault();
			$event.stopPropagation();
		}

		if ($scope._removeInProgress) return;
		$scope._removeInProgress = true;

		const file = $scope.previewAttachments[index];
		if (!file) {
			console.warn("No file found at index:", index);
			$scope._removeInProgress = false;
			return;
		}

		const isExisting = !!file.isExisting;

		Swal.fire({
			title: 'Remove File',
			text: `Are you sure you want to remove this ${isExisting ? 'existing' : ''} file?`,
			icon: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#d33',
			cancelButtonColor: '#3085d6',
			confirmButtonText: 'Yes, remove it',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			$timeout(() => {
				if (!result.isConfirmed) {
					$scope._removeInProgress = false;
					return;
				}
				if (!isExisting && file.previewUrl && file.previewUrl.startsWith('blob:')) {
					try {
						URL.revokeObjectURL(file.previewUrl);
					} catch (err) {
						console.warn("Failed to revoke blob URL:", err);
					}
				}
				if (isExisting && file.DocumentId) {
					if (!$scope.deletedAttachments) $scope.deletedAttachments = [];
					$scope.deletedAttachments.push(file.DocumentId);
					console.log("Marked for deletion → DocumentId:", file.DocumentId);
				}

				$scope.previewAttachments.splice(index, 1);
				if (!isExisting) {
					if ($scope.SelectedFilesInfo?.length) {
						for (let i = 0; i < $scope.SelectedFilesInfo.length; i++) {
							if ($scope.SelectedFilesInfo[i].name === file.name &&
								$scope.SelectedFilesInfo[i].size === file.size) {
								$scope.SelectedFilesInfo.splice(i, 1);
								break;
							}
						}
					}
					if ($scope.HomeWork_Files_TMP?.length) {
						for (let j = 0; j < $scope.HomeWork_Files_TMP.length; j++) {
							if ($scope.HomeWork_Files_TMP[j].name === file.name &&
								$scope.HomeWork_Files_TMP[j].size === file.size) {
								$scope.HomeWork_Files_TMP.splice(j, 1);
								if ($scope.HomeWork_Files_Data?.length > j) {
									$scope.HomeWork_Files_Data.splice(j, 1);
								}
								break;
							}
						}
					}
				}
				$scope.attachmentCount = $scope.previewAttachments.length;
				if (!isExisting && $scope.previewAttachments.every(f => f.isExisting)) {
					const fileInput = document.getElementById('flHomeWork');
					if (fileInput) fileInput.value = '';

					const label = document.querySelector('.custom-file-label');
					if (label) label.textContent = 'Choose files';
				}
				Swal.fire({
					title: 'Removed!',
					text: `File has been ${isExisting ? 'marked for deletion' : 'removed'}.`,
					icon: 'success',
					timer: 2000,
					showConfirmButton: false
				});

				$scope._removeInProgress = false;

			}, 0);   // ← $timeout with 0 delay = next digest tick

		}).catch(err => {
			console.error("Swal error:", err);
			$scope._removeInProgress = false;
		});
	};

	// Function to view existing file
	$scope.viewExistingFile = function (file) {
		if (file.fileType === 'pdf' || file.fileType === 'image') {
			window.open(file.FullPath, '_blank');
		} else {
			window.open(file.FullPath, '_blank');
		}
	};



	$scope.DelAddedHomeWork = function (refData) {
		Swal.fire({
			title: 'Do you want to delete Homework Id ' + refData.HomeWorkId,
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			//Read more about isConfirmed, isDenied below 
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					HomeWorkId: refData.HomeWorkId
				};
				$http({
					method: 'POST',
					url: base_url + "HomeWork/Transaction/DeleteAddedHomework",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAddHomework1();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.viewAttachmentsOnly = function (homDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AttFilesViewOnly = [];
		$scope.homeworkDetails = {};
		var para = {
			HomeWorkId: homDet.HomeWorkId
		};
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAddedHomeworkById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var homeworkData = res.data.Data;
				// Store homework details
				$scope.homeworkDetails = {
					HomeworkId: homeworkData.HomeWorkId,
					HomeworkDate: homeworkData.HomeworkDate || new Date(),
					HomeworkTypeName: homeworkData.HomeworkTypeName,
					ClassName: homeworkData.ClassName,
					SectionName: homeworkData.SectionName,
					SubjectName: homeworkData.SubjectName,
					SectionListName: homeworkData.SectionListName,
					BatchName: homeworkData.BatchName || '',
					SemesterName: homeworkData.SemesterName || '',
					Lesson: homeworkData.Lesson || 'N/A',
					Topic: homeworkData.Topic || 'N/A',
					Description: homeworkData.Description || '',
					DeadlineDate: homeworkData.DeadlineDate ? new Date(homeworkData.DeadlineDate) : null,
					DeadlineMiti: homDet.DeadlineDate_BS,
					AsignDateTime_BS: homDet.AsignDateTime_BS,
					DeadlineTime: homeworkData.DeadlineTime ? new Date(homeworkData.DeadlineTime) : null,
					DeadlineforRedo: homeworkData.DeadlineforRedo ? new Date(homeworkData.DeadlineforRedo) : null,
					DeadlineforRedoTime: homeworkData.DeadlineforRedoTime ? new Date(homeworkData.DeadlineforRedoTime) : null,
					IsAllowLateSibmission: homeworkData.IsAllowLateSibmission || false,
					SubmissionsRequired: homeworkData.SubmissionsRequired || false
				};

				// Process attachments
				var attachments = homeworkData.AttachmentColl;


				if (attachments && attachments.length > 0) {
					angular.forEach(attachments, function (doc) {
						if (doc.DocPath) {
							// Clean the path
							var cleanPath = doc.DocPath.replace(/\\/g, '/');
							var fpath = cleanPath.startsWith('http') ? cleanPath : (WEBURLPATH.trim() + cleanPath).replace(/\\/g, '/');

							// Get file extension
							var fileExt = doc.Extension ?
								doc.Extension.toLowerCase().replace('.', '') :
								fpath.split('.').pop().toLowerCase();

							// Determine file type
							var fileType = 'image';
							if (fileExt === 'pdf') {
								fileType = 'pdf';
							} else if (['doc', 'docx'].includes(fileExt)) {
								fileType = 'word';
							} else if (['xls', 'xlsx'].includes(fileExt)) {
								fileType = 'excel';
							} else if (['ppt', 'pptx'].includes(fileExt)) {
								fileType = 'powerpoint';
							} else if (['txt'].includes(fileExt)) {
								fileType = 'text';
							} else if (['mp4', 'avi', 'mov', 'wmv'].includes(fileExt)) {
								fileType = 'video';
							} else if (['mp3', 'wav'].includes(fileExt)) {
								fileType = 'audio';
							}

							$scope.AttFilesViewOnly.push({
								url: fpath,
								type: fileType,
								extension: fileExt,
								name: doc.Name || doc.Description || 'Attachment',
								description: doc.Description || doc.Name || '',
								fullName: doc.Name + (doc.Extension ? '.' + doc.Extension.replace('.', '') : '')
							});
						}
					});
				}

				// Show the modal
				$('#attachmentModalOnly').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG || "Failed to load homework details.");
			}

		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire('Failed to load details: ' + (reason.statusText || reason));
		});
	};



	$scope.previewFile = function (file) {
		if (file.type === 'pdf') {
			window.open(file.url, '_blank');
		} else if (file.type === 'image') {
			window.open(file.url, '_blank');
		} else {
			window.open(file.url, '_blank');
		}
	};

	$('#previewModal').on('hidden.bs.modal', function () {
		$scope.SelectedFilePreview = null;
		$scope.$apply();
	});

	function getFileCategory(extension) {
		var imageExt = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'];
		var pdfExt = ['pdf'];
		var wordExt = ['doc', 'docx'];
		var excelExt = ['xls', 'xlsx'];
		var pptExt = ['ppt', 'pptx'];
		var textExt = ['txt'];

		extension = extension.toLowerCase();

		if (imageExt.includes(extension)) return 'image';
		if (pdfExt.includes(extension)) return 'pdf';
		if (wordExt.includes(extension)) return 'word';
		if (excelExt.includes(extension)) return 'excel';
		if (pptExt.includes(extension)) return 'powerpoint';
		if (textExt.includes(extension)) return 'text';
		return 'other';
	}
	function getFileExtension(filename) {
		if (!filename) return '';
		return filename.split('.').pop().toLowerCase();
	}

	function getFileNameWithoutExtension(filename) {
		if (!filename) return '';
		return filename.substring(0, filename.lastIndexOf('.')) || filename;
	}

	$scope.onFileSelect = function () {
		var fileInput = document.getElementById('flHomeWork');
		if (!fileInput || !fileInput.files || fileInput.files.length === 0) return;

		var files = fileInput.files;

		angular.forEach(files, function (file) {
			var duplicate = $scope.previewAttachments.some(function (f) {
				return f.name === file.name && f.size === file.size;
			});
			if (duplicate) return;
			var previewUrl = URL.createObjectURL(file);
			var fileInfo = {
				originalFile: file,
				name: file.name,
				fileNameWithoutExt: getFileNameWithoutExtension(file.name),
				extension: getFileExtension(file.name),
				extensionWithDot: '.' + getFileExtension(file.name),
				type: file.type,
				size: file.size,
				previewUrl: previewUrl,
				fileType: getFileCategory(getFileExtension(file.name)),
				isExisting: false
			};
			$scope.previewAttachments.push(fileInfo);
		});
		// Reset file input
		fileInput.value = '';
		// Update label
		var label = document.querySelector('.custom-file-label');
		if (label) {
			label.textContent = $scope.previewAttachments.length + ' file(s) selected';
		}
		$scope.$apply();
	};


	// Unified view function
	$scope.viewFile = function (file) {
		if (file.fileType === 'pdf' || file.fileType === 'image') {
			window.open(file.previewUrl, '_blank');
		} else {
			window.open(file.previewUrl, '_blank');
		}
	};


	$scope.CallSaveUpdateHomework = function () {
		if (!$scope.IsValidHomeworkType()) {
			$scope.loadingstatus = "stop";
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.newAddHomework.DeadlineDateDet) {
			$scope.newAddHomework.DeadlineDate = $filter('date')(new Date($scope.newAddHomework.DeadlineDateDet.dateAD), 'yyyy-MM-dd');
		} else {
			$scope.newAddHomework.DeadlineDate = null;
		}

		if ($scope.newAddHomework.DeadlineforRedoDet) {
			$scope.newAddHomework.DeadlineforRedo = $filter('date')(new Date($scope.newAddHomework.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd');
		} else {
			$scope.newAddHomework.DeadlineforRedo = null;
		}

		if ($scope.newAddHomework.DeadlineTime_TMP) {
			$scope.newAddHomework.DeadlineTime = $filter('date')(new Date($scope.newAddHomework.DeadlineTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		}
		else
			$scope.newAddHomework.DeadlineTime = null;

		if ($scope.newAddHomework.DeadlineForReDoTime_TMP) {
			$scope.newAddHomework.DeadlineForReDoTime = $filter('date')(new Date($scope.newAddHomework.DeadlineForReDoTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		}
		else
			$scope.newAddHomework.DeadlineForReDoTime = null;


		$scope.newAddHomework.ClassId = $scope.newAddHomework.SelectedClass ?
			$scope.newAddHomework.SelectedClass.ClassId : null;

		$scope.newAddHomework.SectionIdColl =
			($scope.newAddHomework.SectionIdColl || []).toString();
		// Collections for different types
		var existingAttachments = [];  // Existing files (already on server)
		var newAttachmentsMetadata = []; // New files metadata
		var newFilesToUpload = []; // Actual new file objects for upload

		// Process all attachments from previewAttachments
		if ($scope.previewAttachments && $scope.previewAttachments.length > 0) {
			angular.forEach($scope.previewAttachments, function (file) {
				if (file.isExisting) {
					// EXISTING FILE - Send only metadata with DocumentId
					var existingFile = {
						DocumentId: file.DocumentId,
						DocumentTypeId: file.DocumentTypeId || 1,
						Name: file.name,
						Description: file.name,
						Extension: file.extensionWithDot,
						DocPath: file.DocPath,
						Data: null,
						IsExisting: true
					};
					existingAttachments.push(existingFile);

				} else {
					// NEW FILE - Send metadata and prepare for upload
					var newFileMeta = {
						DocumentTypeId: 1,
						Name: file.name,
						Description: file.name,
						Extension: file.extensionWithDot,
						Data: null,
						DocPath: null,
						IsNew: true,
						FileName: file.name,
						FileSize: file.size
					};
					newAttachmentsMetadata.push(newFileMeta);

					// Add original file to upload queue
					if (file.originalFile) {
						newFilesToUpload.push(file.originalFile);
					}
				}
			});
		}

		// COMBINE both existing and new attachments into one collection
		var completeAttachmentColl = [];
		// First add all existing attachments
		if (existingAttachments.length > 0) {
			completeAttachmentColl = completeAttachmentColl.concat(existingAttachments);
		}
		// Then add all new attachments metadata
		if (newAttachmentsMetadata.length > 0) {
			completeAttachmentColl = completeAttachmentColl.concat(newAttachmentsMetadata);
		}
		$scope.newAddHomework.AttachmentColl = completeAttachmentColl;
		$scope.newAddHomework.DeletedAttachmentIds = $scope.deletedAttachments || [];
		var homeworkData = angular.copy($scope.newAddHomework);
		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveAddHomeWork",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();

				var jsonString = angular.toJson(data.homeworkData);
				formData.append("jsonData", jsonString);
				if (data.newFiles && data.newFiles.length > 0) {
					for (var i = 0; i < data.newFiles.length; i++) {
						formData.append("file" + i, data.newFiles[i]);
					}
				}
				return formData;
			},
			data: {
				homeworkData: homeworkData,  // Complete data with ALL attachments
				newFiles: newFilesToUpload    // Only new files for upload
			}
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire({
				icon: res.data.IsSuccess ? 'success' : 'error',
				title: res.data.IsSuccess ? 'Success!' : 'Error!',
				text: res.data.ResponseMSG,
				timer: res.data.IsSuccess ? 1000 : undefined
			});
			if (res.data.IsSuccess == true) {
				$scope.GetAllAddHomework1();
				$scope.ClearAddHomework();

				document.getElementById('homework-add-section').style.display = "block";
				document.getElementById('homework-add-form').style.display = "none";
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire({
				icon: 'error',
				title: 'Error!',
				text: "Error occurred while saving data: " + (errormessage.statusText || errormessage)
			});
		});
	};


	$scope.validateDates1 = function (changedField) {
		if (!$scope.newFilter.FromDateDet || !$scope.newFilter.ToDateDet ||
			!$scope.newFilter.FromDateDet.dateAD || !$scope.newFilter.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newFilter.FromDate_TMP = new Date();
						$scope.newFilter.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newFilter.ToDate_TMP = new Date();
						$scope.newFilter.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

	$scope.validateDates2 = function (changedField) {
		if (!$scope.newAddHomework.DeadlineDateDet || !$scope.newAddHomework.DeadlineforRedoDet ||
			!$scope.newAddHomework.DeadlineDateDet.dateAD || !$scope.newAddHomework.DeadlineforRedoDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newAddHomework.DeadlineDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newAddHomework.DeadlineforRedoDet.dateAD), 'yyyy-MM-dd')

		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'DeadlineDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Deadline Date cannot be After Deadline for Redo Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newAddHomework.DeadlineDate_TMP = new Date();
						$scope.newAddHomework.DeadlineDateDet = new Date();
					});
				});
			} else if (changedField === 'DeadlineforRedo') {
				Swal.fire({
					icon: 'warning',
					text: 'Deadline for Redo Date cannot be Before Deadline Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newAddHomework.DeadlineforRedo_TMP = new Date();
						$scope.newAddHomework.DeadlineforRedoDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});