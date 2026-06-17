app.controller('AddDeviceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Add Device';

    OnClickDefault();
    //connectSocketServer();

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
        $scope.DeviceCompanyColl = GlobalServices.getDeviceCompany();
        $scope.UseSSL = false;
        $scope.DevicePort = 7789;
        $scope.ForList = [{ id: 1, text: 'Class' }, { id: 2, text: 'Hostel' }];
		$scope.currentPages = {
			AddDevice: 1,			
		};

		$scope.searchData = {
			AddDevice: '',			
		};

		$scope.perPage = {
			AddDevice: GlobalServices.getPerPageRow(),			
		};

		$scope.newAddDevice = {
			DeviceId: null,
			Name: '',			
			MachineSerialNo: '',			
            Location: '',
            ForId:1,
			Mode: 'Save'
		};		
		$scope.GetAllAddDeviceList();
	}

	function OnClickDefault() {
		document.getElementById('add-device-form').style.display = "none";

		document.getElementById('add-device').onclick = function () {
			document.getElementById('device-section').style.display = "none";
			document.getElementById('add-device-form').style.display = "block";
			$scope.ClearAddDevice();
		}
		document.getElementById('deviceback-btn').onclick = function () {
			document.getElementById('add-device-form').style.display = "none";
			document.getElementById('device-section').style.display = "block";
			$scope.ClearAddDevice();
		}
	}

	$scope.ClearAddDevice = function () {

		$timeout(function () {
			$scope.newAddDevice = {
				DeviceId: null,
				Name: '',
				MachineSerialNo: '',
                Location: '',
                ForId: 1,
				Mode: 'Save'
			};
		});
		
	}
	//************************* AddDevice *********************************
	$scope.IsValidAddDevice = function () {
		if ($scope.newAddDevice.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateAddDevice = function () {
		if ($scope.IsValidAddDevice() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAddDevice.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddDevice();
					}
				});
			} else
				$scope.CallSaveUpdateAddDevice();
		}
	};

	$scope.CallSaveUpdateAddDevice = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/SaveDevice",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newAddDevice }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearAddDevice();
				$scope.GetAllAddDeviceList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllAddDeviceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddDeviceList = [];
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllDeviceList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AddDeviceList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetAddDeviceById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			DeviceId: refData.DeviceId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetDeviceById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAddDevice = res.data.Data;
				$scope.newAddDevice.Mode = 'Modify';

				document.getElementById('device-section').style.display = "none";
				document.getElementById('add-device-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAddDeviceById = function (refData) {
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
					DeviceId: refData.DeviceId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DelDevice",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAddDeviceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};
	
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

    var ws;

    $scope.connectSocketServer=function() {
        // create a new websocket and connect     

        var url = 'WS';
        if ($scope.UseSSL && $scope.UseSSL == true)
            url = 'WSS';

        var websocket = new WebSocketEx(url+'://202.51.3.113:'+$scope.DevicePort, '', function ()
        {
            //messageBoard.append('* Connection open<br/>');
            ws = websocket;
            // ws.send('{ "MSG":"Test Server" }');
        }, function (evt) {
            //messageBoard.append('* Connection closed<br/>');
            Swal.fire('Unable To Connect With Server. Connection closed.');
        }, function (evt) {
            // On MSG Received
            $scope.$apply(
                function () {
                    $scope.loadingstatus = 'stop';
                });

            var res = JSON.parse(evt.data);

            $scope.$apply(
                function () {
                    if (res.Status && res.SerialNo) {
                        angular.forEach($scope.AddDeviceList, function (machine) {

                            if (machine.MachineSerialNo == res.SerialNo) {
                                machine.Status = res.Status;
                            }
                        });

                    } else
                        alert(res.ResponseMSG);

                });



        }, function (evt) {
            //On Error

            $scope.$apply(
                function () {
                    $scope.loadingstatus = 'stop';
                });
            var res = JSON.parse(evt.data);


            if (res.Status && res.SerialNo) {
                angular.forEach($scope.AddDeviceList, function (machine) {

                    if (machine.MachineSerialNo == res.SerialNo) {
                        machine.Status = res.Status;
                    }
                });

            } else
                alert(res.ResponseMSG);

        });
    }

    $scope.checkDeviceStatus = function () {

        $scope.loadingstatus = 'running';

        if (!ws || ws==undefined)
            $scope.connectSocketServer();

        angular.forEach($scope.AddDeviceList, function (machine) {
            machine.Status = "";
        });

        $timeout(function () {

            if (ws && ws != undefined) {
                angular.forEach($scope.AddDeviceList, function (machine) {

                    var beData = {
                        cmd: "getdevstatus",
                        serialNo: machine.MachineSerialNo,
                        reg: true,
                        deviceCompany: machine.DeviceCompanyId
                    };
                    ws.send(JSON.stringify(beData));
                     
                });
            }
           
        });
     

    };

    $scope.getUserInfo = function (machine) {

        $scope.loadingstatus = 'running';

        var beData = {
            cmd: "getuserlist",
            serialNo: machine.MachineSerialNo,
            reg: true,
            deviceCompany: machine.DeviceCompanyId
        };
        ws.send(JSON.stringify(beData));
        setTimeout(function () {
            $scope.$apply(function () {

                var beData = {
                    cmd: "getuserinfo",
                    serialNo: machine.MachineSerialNo,
                    reg: true,
                    deviceCompany: machine.DeviceCompanyId
                };

                ws.send(JSON.stringify(beData));
            });
        }, 9000);

    };

    $scope.CheckUnCheckEmp = function () {

        angular.forEach($scope.EmpColl, function (emp) {
            emp.IsSelected = $scope.beData.CheckedAll;
        });
    }

    $scope.upLoadUserInfo = function (machine) {

        $scope.loadingstatus = 'running';
        $scope.EmpColl = [];
        $scope.selectedMachine = machine;
        $http.post(base_url + "Attendance/Transaction/GetEmpFingerList").then(function (res) {

            $scope.EmpColl = res.data;
            $('#AddEmployeeList').modal('show');

            $scope.loadingstatus = "stop";
        }, function (errormessage) {
            $scope.loadingstatus = "stop";
            alert('Unable to get data for update.' + errormessage.responseText);
        });

    };

    $scope.upLoadUserInfoToMachine = function (machine) {

        $scope.loadingstatus = 'running';
        
        if (machine) {
            
            var beData = {
                cmd: "uploaduserinfo",
                serialNo: machine.MachineSerialNo,
                reg: true,
                empidcoll: "",
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    }

    $scope.upLoadUserCardToMachine = function (machine) {

        $scope.loadingstatus = 'running';

        if (machine) {

            var beData = {
                cmd: "uploadusercard",
                serialNo: machine.MachineSerialNo,
                reg: true,
                empidcoll: "",
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    }

    $scope.downloadAllLog = function (machine) {

        $scope.loadingstatus = 'running';

        var beData = {
            cmd: "getalllog",
            serialNo: machine.MachineSerialNo,
            reg: true,
            deviceCompany: machine.DeviceCompanyId
        };
        ws.send(JSON.stringify(beData));

    };
    $scope.downloadNewLog = function (machine) {

        $scope.loadingstatus = 'running';

        var beData = {
            cmd: "getnewlog",
            serialNo: machine.MachineSerialNo,
            reg: true,
            deviceCompany: machine.DeviceCompanyId
        };
        ws.send(JSON.stringify(beData));

    };
 
    $scope.cleanuserlock = function (machine) {

        if (confirm("Are you sure to clear admin thum")) {
            $scope.loadingstatus = 'running';

            var beData = {
                cmd: "cleanadmin",
                serialNo: machine.MachineSerialNo,
                reg: true,
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    };

    $scope.cleanlog = function (machine) {

        if (confirm("Are you sure to clean attendance log")) {
            $scope.loadingstatus = 'running';

            var beData = {
                cmd: "cleanlog",
                serialNo: machine.MachineSerialNo,
                reg: true,
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    };
    $scope.cleanuser = function (machine) {

        if (confirm("Are you sure to delete all user")) {
            $scope.loadingstatus = 'running';

            var beData = {
                cmd: "cleanuser",
                serialNo: machine.MachineSerialNo,
                reg: true,
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    };
    $scope.opendoor = function (machine) {

        if (confirm("Are you sure to open door")) {
            $scope.loadingstatus = 'running';

            var beData = {
                cmd: "opendoor",
                serialNo: machine.MachineSerialNo,
                reg: true,
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    };
    $scope.reboot = function (machine) {

        if (confirm("Are you sure to re-start selected device")) {
            $scope.loadingstatus = 'running';

            var beData = {
                cmd: "reboot",
                serialNo: machine.MachineSerialNo,
                reg: true,
                deviceCompany: machine.DeviceCompanyId
            };
            ws.send(JSON.stringify(beData));
        }

    };

    $scope.showSelectDeviceMessage = function () {
        // Check if at least one device is selected
        const isAnyDeviceSelected = $scope.AddDeviceList.some(device => device.IsSelected);

        if (isAnyDeviceSelected) {
            $('#UploadCard').modal('show');
        } else {
            Swal.fire('To proceed, please select the devices first, then upload the card numbers.');
        }
    };

    $scope.showDeleteDeviceMessage = function () {
        // Check if at least one device is selected
        const isAnyDeviceSelected = $scope.AddDeviceList.some(device => device.IsSelected);

        if (isAnyDeviceSelected) {
            $('#DeleteCard').modal('show');
        } else {
            Swal.fire('To proceed, please select the devices first, then upload the card numbers.');
        }
    };

	$scope.UploadCardno = function () {

		if (!ws) {
			Swal.fire('Device Not Connected');
			return;
		}



		var cardNos = $scope.newUpload.CardNo;
		if (cardNos && cardNos.length > 0) {

			var cardNoColl = [];
			var qry = mx(cardNos);
			if (qry.contains('-')) {

				var strColl = cardNos.split('-');
				var startNo = parseInt(strColl[0]);
				var endNo = parseInt(strColl[1]);

				for (var i = startNo; i <= endNo; i++) {
					cardNoColl.push(i);
				}

			}
			else if (qry.contains(',')) {
				cardNos.split(',').forEach(function (cd) {
					cardNoColl.push(cd);
				});
			}
			else {
				cardNoColl.push(cardNos);
			}


			$scope.DeviceList.forEach(function (dv) {
				var companyId = dv.DeviceCompanyId;
				if (dv.IsSelected == true) {

					if (dv.Status == "Connected") {

						var beData = {
							cmd: "setcard",
							serialNo: dv.MachineSerialNo,
							reg: true,
							deviceCompany: companyId,
							cardno: cardNoColl.toString(),
						};
						ws.send(JSON.stringify(beData));

					}

				}
			});
			$('#UploadCard').modal('hide');

		}
	}

	$scope.DeleteCardno = function () {

		if (!ws) {
			Swal.fire('Device Not Connected');
			return;
		}



		var cardNos = $scope.newDelete.CardNo;
		if (cardNos && cardNos.length > 0) {

			var cardNoColl = [];
			var qry = mx(cardNos);
			if (qry.contains('-')) {

				var strColl = cardNos.split('-');
				var startNo = parseInt(strColl[0]);
				var endNo = parseInt(strColl[1]);

				for (var i = startNo; i <= endNo; i++) {
					cardNoColl.push(i);
				}

			}
			else if (qry.contains(',')) {
				cardNos.split(',').forEach(function (cd) {
					cardNoColl.push(cd);
				});
			}
			else {
				cardNoColl.push(cardNos);
			}


			$scope.DeviceList.forEach(function (dv) {
				var companyId = dv.DeviceCompanyId;
				if (dv.IsSelected == true) {

					if (dv.Status == "Connected") {

						var beData = {
							cmd: "deletecard",
							serialNo: dv.MachineSerialNo,
							reg: true,
							deviceCompany: companyId,
							empidcoll: cardNoColl.toString(),
						};
						ws.send(JSON.stringify(beData));

					}

				}
			});
			$('#DeleteCard').modal('hide');

		}
	}


});