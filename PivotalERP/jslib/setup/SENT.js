
app.controller('sentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'SMS/Email/Notification';

	

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Entity: 1,
			Schedule:1
		};

		$scope.searchData = {
			Entity: '',
			Schedule:''
		};

		$scope.perPage = {
			Entity: GlobalServices.getPerPageRow(),
			Schedule: GlobalServices.getPerPageRow()
		};


		$scope.ModuleList = [{ id: 24, text: 'Admission Management' },{ id: 2, text: 'Front Desk' }, { id: 3, text: 'Academic Calendar' }, { id: 4, text: 'Academic' }, { id: 5, text: 'Exam' }, { id: 6, text: 'Fee' }, { id: 7, text: 'Homework' }, { id: 8, text: 'Attendance' }, { id: 9, text: 'App CMS' },
			{ id: 10, text: 'Transport' }, { id: 11, text: 'Hostel' }, { id: 12, text: 'Canteen' }, { id: 13, text: 'Library' }, { id: 14, text: 'OnlineClass' }, { id: 15, text: 'Accounting' }, { id: 16, text: 'Inventory' }];

		$scope.ActionTypeList = [{ id: 1, text: 'Manual' }, { id: 2, text: 'Schedule' }, { id: 3, text: 'After Save' }, { id: 4, text: 'After Update' }, { id: 5, text: 'After Delete' }];		

		$scope.StatusList = [{ id: 1, text: 'Enabled' }, { id: 0, text: 'Disabled' }];

		$scope.newSENT = {
			ModuleId: null,
			Heading: '',
			ActionType: 1,
			Status: 1,
			Name: '',
			Title: '',
			Description:''

		};


		//$scope.GetAllUserGroupList();

	}

	$scope.ClearSENT = function () {

		$timeout(function () {
			var mid = $scope.newSENT.ModuleId;
			$scope.newSENT = {
				ModuleId: mid,
				Heading: '',
				ActionType: 1,
				Status: 1,
				Name: '',
				Title: '',
				Description: ''

			};
		});
	};

	$scope.GetEntityList = function () {

		$scope.EntityList = [];
		if ($scope.newSENT.ModuleId && $scope.newSENT.ModuleId > 0) {

			var para = {
				modul:$scope.newSENT.ModuleId
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetEntityListByModule",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.EntityList = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	};

	$scope.AddProperties = function (p) {
		$scope.newSENT.Description = $scope.newSENT.Description + '$$'+p.datatype.toLowerCase()+'$$';
	};

	//TemplateType			int not null default 1,  --1=SMS,2=Email,3=Notification
	$scope.ShowNotification = function (ent,forATS)
	{
		$scope.ClearSENT();
		$timeout(function ()
		{
			$scope.newSENT.EntityId = ent.id;
			$scope.newSENT.ForATS = forATS;
			$scope.newSENT.TemplateType = 3;

			if (forATS == 1) {
				$scope.newSENT.Heading = 'Admin Notification :'+ent.text;
			}
			else if (forATS == 2) {
				$scope.newSENT.Heading = 'Teacher Notification :' + ent.text;
			}
			else if (forATS == 3) {
				$scope.newSENT.Heading = 'Student Notification :' + ent.text;
			}

			var para = {
				entityId: ent.id
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetEntityPropertiesList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess)
				{
					$scope.newSENT.PropertiesList = res.data.Data;

					var para1 = {
						EntityId: para.entityId,
						ForATS: $scope.newSENT.ForATS,
						TemplateType: $scope.newSENT.TemplateType
					};

					$http({
						method: 'POST',
						url: base_url + "Setup/Security/GetSENT",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newSENT.TemplateList = res.data.Data;
							$('#modal-admin-notification').modal('show');
						}
						else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
					
				}
				else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



			
		});
		
    }

	$scope.ShowEmail = function (ent, forATS) {

		$scope.ClearSENT();

		$timeout(function () {

			$scope.newSENT.EntityId = ent.id;
			$scope.newSENT.ForATS = forATS;
			$scope.newSENT.TemplateType = 2;

			if (forATS == 1) {
				$scope.newSENT.Heading = 'Admin Email :' + ent.text;
			}
			else if (forATS == 2) {
				$scope.newSENT.Heading = 'Teacher Email :' + ent.text;
			}
			else if (forATS == 3) {
				$scope.newSENT.Heading = 'Student Email :' + ent.text;
			}


			var para = {
				entityId: ent.id
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetEntityPropertiesList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess) {
					$scope.newSENT.PropertiesList = res.data.Data;

					var para1 = {
						EntityId: para.entityId,
						ForATS: $scope.newSENT.ForATS,
						TemplateType: $scope.newSENT.TemplateType
					};

					$http({
						method: 'POST',
						url: base_url + "Setup/Security/GetSENT",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newSENT.TemplateList = res.data.Data;
							$('#modal-admin-email').modal('show');
						}
						else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

					
				}
				else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			
		});

	}

	$scope.ShowSMS = function (ent, forATS) {

		$scope.ClearSENT();

		$timeout(function () {

			$scope.newSENT.EntityId = ent.id;
			$scope.newSENT.ForATS = forATS;
			$scope.newSENT.TemplateType = 1;

			if (forATS == 1) {
				$scope.newSENT.Heading = 'Admin SMS :' + ent.text;
			}
			else if (forATS == 2) {
				$scope.newSENT.Heading = 'Teacher SMS :' + ent.text;
			}
			else if (forATS == 3) {
				$scope.newSENT.Heading = 'Student SMS :' + ent.text;
			}

			var para = {
				entityId: ent.id
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetEntityPropertiesList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess) {
					$scope.newSENT.PropertiesList = res.data.Data;

					var para1 = {
						EntityId: para.entityId,
						ForATS: $scope.newSENT.ForATS,
						TemplateType: $scope.newSENT.TemplateType
					};

					$http({
						method: 'POST',
						url: base_url + "Setup/Security/GetSENT",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newSENT.TemplateList = res.data.Data;
							$('#modal-admin-sms').modal('show');
						}
						else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

					
				}
				else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


			
		});

	}

	$scope.ClearUserGroup = function () {
		$scope.newUserGroup = {
			GroupId: 0,
			GroupName: '',
			Alias: '',
			Description: '',
			Inbuilt: false,
			Mode: 'Save'
		};

	}

	$scope.ClearData = function () {
		$scope.newSENT.Title = '';
		$scope.newSENT.Name = '';
		$scope.newSENT.Description = '';
		$scope.newSENT.Recipients = '';
		$scope.newSENT.EmailCC = '';
		$scope.newSENT.EmailBCC = '';
	};

	$scope.IsValidSENT = function () {
		if ($scope.newSENT.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		if ($scope.newSENT.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}

		if ($scope.newSENT.Description.isEmpty()) {
			Swal.fire('Please ! Enter Description');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateSENT = function () {
		if ($scope.IsValidSENT() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUserGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSENT();
					}
				});
			} else
				$scope.CallSaveUpdateSENT();

		}
	};

	$scope.CallSaveUpdateSENT = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveUpdateSENT",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSENT }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			alert(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {

				//if (!$scope.newSENT.TemplateList)
				//	$scope.newSENT.TemplateList = [];

				//$scope.newSENT.TemplateList.push({
				//	TranId: res.data.RId,
				//	EntityId: $scope.newSENT.EntityId,
				//	Name: $scope.newSENT.Name,
				//	Title: $scope.newSENT.Title,
				//	Description: $scope.newSENT.Description,
				//	ActionType:$scope.newSENT.ActionType
				//});

				$timeout(function () {
					var para1 = {
						EntityId: $scope.newSENT.EntityId,
						ForATS: $scope.newSENT.ForATS,
						TemplateType: $scope.newSENT.TemplateType
					};

					$http({
						method: 'POST',
						url: base_url + "Setup/Security/GetSENT",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newSENT.TemplateList = res.data.Data;
						}
						else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				});

				$scope.ClearData();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUserGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UserGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSENTById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENTById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var rs = res.data.Data;				
				$scope.newSENT.Name = rs.Name;
				$scope.newSENT.TranId = rs.TranId;
				$scope.newSENT.Title = rs.Title;
				$scope.newSENT.Description = rs.Description;
				$scope.newSENT.Recipients = rs.Recipients;
				$scope.newSENT.EmailCC = rs.EmailCC;
				$scope.newSENT.EmailBCC = rs.EmailBCC;
				$scope.newSENT.ActionType = rs.ActionType;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSENTById = function (refData) {

		var cMSG=confirm("Do you want to delete the selected data?");

		if (cMSG == true) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				TranId: refData.TranId
			};

			$http({
				method: 'POST',
				url: base_url + "Setup/Security/DelSENT",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$timeout(function () {
						var para1 = {
							EntityId: $scope.newSENT.EntityId,
							ForATS: $scope.newSENT.ForATS,
							TemplateType: $scope.newSENT.TemplateType
						};

						$http({
							method: 'POST',
							url: base_url + "Setup/Security/GetSENT",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res) {
							if (res.data.IsSuccess) {
								$scope.newSENT.TemplateList = res.data.Data;
							}
							else {
								Swal.fire(res.data.ResponseMSG);
							}
						}, function (reason) {
							Swal.fire('Failed' + reason);
						});
					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        } 
	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});
