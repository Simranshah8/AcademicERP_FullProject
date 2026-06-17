app.controller('UserCredentialController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'User Credential';

	/*OnClickDefault();*/

	$scope.LoadData = function () {

		$('.select2').select2();

		var glbS = GlobalServices;
		$scope.perPageColl = glbS.getPerPageList();
	
		$scope.currentPages = {
			StudentUsers: 1,
			ParentUsers: 1,
			EmployeeUsers: 1,
			AdminUsers: 1
		};

		$scope.searchData = {
			StudentUsers: '',
			ParentUsers: '',
			EmployeeUsers: '',
			AdminUsers: ''
		};

		$scope.perPage = {
			StudentUsers: glbS.getPerPageRow(),
			ParentUsers: glbS.getPerPageRow(),
			EmployeeUsers: glbS.getPerPageRow(),
			AdminUsers: glbS.getPerPageRow()
		};
		//Added new code for config
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
		//Ends

		//$scope.AcademicConfig = {};
		//GlobalServices.getAcademicConfig().then(function (res1) {
		//	$scope.AcademicConfig = res1.data.Data; 
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentUsers = {
			ClassId: null,
			SectionId:null,
			StudentUsersId: null,	
			AsPer: 1,
			canUpdate: false,
			Prefix: '',
			Suffix:'',
			Mode: 'Save'
		};

		$scope.newParentUsers = {
			ParentUsersId: null,			
			Mode: 'Save'
		};

		$scope.newEmployeeUsers = {
			EmployeeUsersId: null,	
			AsPer: 1,
			canUpdate:false,
			Mode: 'Save'
		};

		$scope.newAdminUsers = {
			AdminUsersId: null,			
			Mode: 'Save'
		};

		$scope.EmpUserAsPerList = [
			{ id: 1, text: 'AutoNumber' },
			{ id: 2, text: 'Code' },
			{ id: 3, text: 'FirstName' },
			{ id: 4, text: 'Code+FirstName' },
			{ id: 5, text: 'FirstName+Code' },
			{ id: 6, text: 'Code+FirstName' },
			{ id: 7, text: 'FirstName+Code' },
			{ id: 8, text: 'ContactNo' } 
		];

		$scope.StudentUserAsPerList = [
			{ id: 1, text: 'AutoNumber' },
			{ id: 2, text: 'RegdNo' },
			{ id: 3, text: 'FirstName' },
			{ id: 4, text: 'AutoNumber+FirstName' },
			{ id: 5, text: 'FirstName+AutoNumber' },
			{ id: 6, text: 'RegdNo+FirstName' },
			{ id: 7, text: 'FirstName+RegdNo' },
			{ id: 8, text: 'ContactNo' },
			{ id: 9, text: 'FatherContactNo' }
		];

		$scope.GetAllEmployeeUsersList();
		
	
	}

	
	
	$scope.ClearStudentUsers = function () {
		$scope.newStudentUsers = {
			StudentUsersId: null,			
			Mode: 'Save'
		};
	}
	$scope.ClearParentUsers = function () {
		$scope.newParentUsers = {
			ParentUsersId: null,			
			Mode: 'Save'
		};
	}
	$scope.ClearEmployeeUsers = function () {
		$scope.newEmployeeUsers = {
			EmployeeUsersId: null,			
			Mode: 'Save'
		};
	}
	$scope.ClearAdminUsers = function () {
		$scope.newAdminUsers = {
			AdminUsersId: null,			
			Mode: 'Save'
		};
	}

	//************************* StudentUsers *********************************

	
	$scope.SaveUpdateStudentUsers = function () {
		if ($scope.IsValidStudentUsers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentUsers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentUsers();
					}
				});
			} else
				$scope.CallSaveUpdateStudentUsers();

		}
	};

	$scope.CallSaveUpdateStudentUsers = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentUsers",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentUsers }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentUsers();				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentUsersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentUsersList = [];

		var para = {
			/*ClassId: $scope.newStudentUsers.ClassId,*/
			ClassId: $scope.newStudentUsers.SelectedClass.ClassId,
			SectionIdColl: ($scope.newStudentUsers.SectionId && $scope.newStudentUsers.SectionId.length > 0 ? $scope.newStudentUsers.SectionId.toString() : ''),
			BatchId: $scope.newStudentUsers.BatchId || null,
			SemesterId: $scope.newStudentUsers.SemesterId || null,
			ClassYearId: $scope.newStudentUsers.ClassYearId || null			
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetStudentUserList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				$scope.StudentUsersList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	
	$scope.GenerateStudentUser = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AsPer: $scope.newStudentUsers.AsPer,
			canUpdate: $scope.newStudentUsers.canUpdate,
			Prefix: $scope.newStudentUsers.Prefix,
			Suffix: $scope.newStudentUsers.Suffix
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GenerateStudentUser",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	//$scope.SendNotificationToStudentUser = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Setup/SendNotificationToStudentUser",
	//		dataType: "json",
	//		data: JSON.stringify($scope.StudentUsersList)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		Swal.fire(res.data.ResponseMSG);
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});

	//};

	$scope.UpdateQR = function () {

		Swal.fire({
			title: 'Are you Sure To Update Student / Employee QR Code?',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
			 
				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/ReGenerateQR",
					dataType: "json",
					//data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		}); 

	};

	$scope.ResetPwdClass = function ()
	{
		
		Swal.fire({
			title: 'Are you Sure To Reset Pwd Of Selected Class . After Reset Can not Recoverd ?',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.newStudentUsers.SelectedClass.ClassId
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/ResetClassWisePwd",
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



	};
	$scope.ResetPwdEmployeee = function () {

		Swal.fire({
			title: 'Are you Sure To Reset Pwd Of All Employee . After Reset Can not Recoverd ?',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
			 
				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/ResetPwdEmployeee",
					dataType: "json",
					//data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});



	};
	$scope.SendNotificationToForceLogOut = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SendNotificationToForceLogOut",
			dataType: "json",
			data: JSON.stringify($scope.StudentUsersList)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.StudentForlogout = function (st) {
		var stColl = [];
		stColl.push(st);

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SendNotificationToForceLogOut",
			dataType: "json",
			data: JSON.stringify(stColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.AllNotificationEnabled = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/AllNotificationEnabled",
			dataType: "json",			
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.AllNotificationDisabled = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/AllNotificationDisabled",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	//$scope.SendSMSToStudentUser = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Setup/SendSMSToStudentUser",
	//		dataType: "json",
	//		data: JSON.stringify($scope.StudentUsersList)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		Swal.fire(res.data.ResponseMSG);
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});

	//};
	$scope.GetStudentUsersById = function (refData) {

		refData.NewPwd = refData.Pwd;
		refData.CanEdit = true;
	};

	$scope.UpdateStudentPwd = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: refData.UserId,
			OldPwd: refData.Pwd,
			NewPwd: refData.NewPwd,
			UserName:refData.UserName
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/UpdatePwd",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess)
			{
				$timeout(function () {
					refData.Pwd = refData.NewPwd;
					refData.CanEdit = false;
				});				
			} 

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.DelStudentUsersById = function (refData) {

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
					StudentUsersId: refData.StudentUsersId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelStudentUsers",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentUsersList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	$scope.SendSMSToStudentUser = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To Student Users?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentUserSMS,
					ForATS: 3,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];

													var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);													 
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];


								var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.SendSMSFromMobileToStudentUser = function () {

		Swal.fire({
			title: 'Do you want to Send SMS From Mobile To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentUserSMS,
					ForATS: 3,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSFromMobileToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];

								var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSFromMobileToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.SendNotificationToStudentUser = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityStudentUserSMS,
					ForATS: 3,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendNotificationToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];

								var tmpFilterData = $filter('filter')($scope.StudentUsersList, $scope.searchData.StudentUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendNotificationToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});


	};

	//************************* ParentUsers *********************************

	

	$scope.SaveUpdateParentUsers = function () {
		if ($scope.IsValidParentUsers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newParentUsers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateParentUsers();
					}
				});
			} else
				$scope.CallSaveUpdateParentUsers();

		}
	};

	$scope.CallSaveUpdateParentUsers = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SaveParentUsers",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newParentUsers }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearParentUsers();
				$scope.GetAllParentUsersList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllParentUsersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ParentUsersList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAllParentUsersList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ParentUsersList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetParentUsersById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ParentUsersId: refData.ParentUsersId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetParentUsersById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newParentUsers = res.data.Data;
				$scope.newParentUsers.Mode = 'Modify';

				document.getElementById('ParentUsers-content').style.display = "none";
				document.getElementById('ParentUsers-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelParentUsersById = function (refData) {

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
					ParentUsersId: refData.ParentUsersId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelParentUsers",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllParentUsersList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Academic Year *********************************

	

	$scope.SaveUpdateEmployeeUsers = function () {
		if ($scope.IsValidEmployeeUsers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployeeUsers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployeeUsers();
					}
				});
			} else
				$scope.CallSaveUpdateEmployeeUsers();

		}
	};

	$scope.CallSaveUpdateEmployeeUsers = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SaveEmployeeUsers",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmployeeUsers }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployeeUsers();
				$scope.GetAllEmployeeUsersList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllEmployeeUsersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeUsersList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetEmployeeUserList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeUsersList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GenerateEmployeeUser = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AsPer: $scope.newEmployeeUsers.AsPer,
			canUpdate: $scope.newEmployeeUsers.canUpdate,
			Prefix: $scope.newEmployeeUsers.Prefix,
			Suffix:$scope.newEmployeeUsers.Suffix
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GenerateEmployeeUser",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	//$scope.SendNotificationToEmployeeUser = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Setup/SendNotificationToEmployeeUser",
	//		dataType: "json",
	//		data: JSON.stringify($scope.EmployeeUsersList)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		Swal.fire(res.data.ResponseMSG);
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});

	//};
	//$scope.SendSMSToEmployeeUser = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Setup/SendSMSToEmployeeUser",
	//		dataType: "json",
	//		data: JSON.stringify($scope.EmployeeUsersList)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		Swal.fire(res.data.ResponseMSG);
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});

	//};
	$scope.GetEmployeeUsersById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EmployeeUsersId: refData.EmployeeUsersId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetEmployeeUsersById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployeeUsers = res.data.Data;
				$scope.newEmployeeUsers.Mode = 'Modify';

				document.getElementById('batch-ParentUsers').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEmployeeUsersById = function (refData) {

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
					EmployeeUsersId: refData.EmployeeUsersId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelEmployeeUsers",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEmployeeUsersList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.SendSMSToEmployeeUser = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To Employee Users?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityTeacherUserSMS,
					ForATS: 1,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];

													var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);												
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															EntityId: entityTeacherUserSMS,
															StudentId: objEntity.EmployeeId,
															EmployeeId: objEntity.EmployeeId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															EmployeeName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];

								var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										EntityId: entityTeacherUserSMS,
										StudentId: objEntity.EmployeeId,
										EmployeeId: objEntity.EmployeeId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										EmployeeName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.SendSMSFromMobileToEmployeeUser = function () {

		Swal.fire({
			title: 'Do you want to Send SMS From Mobile To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityTeacherUserSMS,
					ForATS: 1,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															EntityId: entityTeacherUserSMS,
															StudentId: objEntity.EmployeeId,
															EmployeeId: objEntity.EmployeeId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															EmployeeName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSFromMobileToEmployee",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];

								var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										EntityId: entityTeacherUserSMS,
										StudentId: objEntity.EmployeeId,
										EmployeeId: objEntity.EmployeeId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										EmployeeName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSFromMobileToEmployee",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.SendNotificationToEmployeeUser = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityTeacherUserSMS,
					ForATS: 1,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);
													angular.forEach(tmpFilterData, function (fr) {

														var objEntity = fr;
														var msg = selectedTemplate.Description;
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
															EntityId: entityTeacherUserSMS,
															StudentId: objEntity.EmployeeId,
															EmployeeId: objEntity.EmployeeId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.ContactNo,
															EmployeeName: objEntity.Name
														};

														dataColl.push(newSMS);

													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendNotificationToEmployee",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];

								var tmpFilterData = $filter('filter')($scope.EmployeeUsersList, $scope.searchData.EmployeeUsers);
								angular.forEach(tmpFilterData, function (fr) {

									var objEntity = fr;
									var msg = selectedTemplate.Description;
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
										EntityId: entityTeacherUserSMS,
										StudentId: objEntity.EmployeeId,
										EmployeeId: objEntity.EmployeeId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										EmployeeName: objEntity.Name
									};

									dataColl.push(newSMS);

								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendNotificationToEmployee",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});
							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});


	};

	//************************* AdminUsers *********************************

	
	$scope.SaveUpdateAdminUsers = function () {
		if ($scope.IsValidAdminUsers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAdminUsers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAdminUsers();
					}
				});
			} else
				$scope.CallSaveUpdateAdminUsers();

		}
	};

	$scope.CallSaveUpdateAdminUsers = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SaveAdminUsers",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAdminUsers }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAdminUsers();
				$scope.GetAllAdminUsersList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAdminUsersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AdminUsersList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAllAdminUsersList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AdminUsersList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAdminUsersById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AdminUsersId: refData.AdminUsersId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAdminUsersById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAdminUsers = res.data.Data;
				$scope.newAdminUsers.Mode = 'Modify';

				document.getElementById('AdminUsers-ParentUsers').style.display = "none";
				document.getElementById('AdminUsers-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAdminUsersById = function (refData) {

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
					AdminUsersId: refData.AdminUsersId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelAdminUsers",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAdminUsersList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.ClearLogById = function (refData) {

		Swal.fire({
			title: 'Do you want to clear login lof of user ' + refData.UserName + ' ?',
			showCancelButton: true,
			confirmButtonText: 'Clear Log',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					UserName: refData.UserName
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/ClearLoginLog",
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


	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});