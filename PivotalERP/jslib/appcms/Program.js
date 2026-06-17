app.controller('ProgramController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Program';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Program: 1,
			Syllabus: 1,

		};

		$scope.searchData = {
			Program: '',
			Syllabus: '',

		};

		$scope.perPage = {
			Program: GlobalServices.getPerPageRow(),
			Syllabus: GlobalServices.getPerPageRow(),

		};
		$scope.newProgram = {
			ProgramId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			Mode: 'Save'
		};

		$scope.newSyllabus = {
			ProgramId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			DetailsColl:[],
			Mode: 'Save'
		};
		$scope.GetAllProgramList();
		$scope.GetAllSyllabusList();

	}

	function OnClickDefault() {

		document.getElementById('Program-form').style.display = "none";
		document.getElementById('Syllabus-form').style.display = "none";

		//Program section
		document.getElementById('add-Program').onclick = function () {
			document.getElementById('Program-section').style.display = "none";
			document.getElementById('Program-form').style.display = "block";
			$timeout(function () {
				$scope.ClearProgram();
			});

		}

		document.getElementById('back-to-list-Program').onclick = function () {
			document.getElementById('Program-form').style.display = "none";
			document.getElementById('Program-section').style.display = "block";
			$timeout(function () {
				$scope.ClearProgram();
			});
		}
		//Syllabus section
		document.getElementById('add-Syllabus').onclick = function () {
			document.getElementById('Syllabus-section').style.display = "none";
			document.getElementById('Syllabus-form').style.display = "block";
			$timeout(function () {
				$scope.ClearSyllabus();
			});

		}

		document.getElementById('back-to-list-Syllabus').onclick = function () {
			document.getElementById('Syllabus-form').style.display = "none";
			document.getElementById('Syllabus-section').style.display = "block";
			$timeout(function () {
				$scope.ClearSyllabus();
			});
		}

	}

	$scope.ClearProgram = function () {
		$scope.newProgram = {
			ProgramId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			Mode: 'Save'
		};

	}

	$scope.ClearSyllabus = function () {
		$scope.newSyllabus = {
			ProgramId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			DetailsColl: [],
			TopicColl: [],
			Mode: 'Save'
		};
		$scope.CurrentSyllabus = {
			TopicColl: []
		};

	}

	//*************************Program *********************************

	$scope.IsValidProgram = function () {
		if ($scope.newProgram.Name.isEmpty()) {
			Swal.fire('Please ! Enter Program Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateProgram = function () {
		if ($scope.IsValidProgram() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newProgram.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateProgram();
					}
				});
			} else
				$scope.CallSaveUpdateProgram();

		}
	};

	$scope.CallSaveUpdateProgram = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveProgram",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newProgram }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearProgram();
				$scope.GetAllProgramList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllProgramList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ProgramList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllProgram",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ProgramList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetProgramById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ProgramId: refData.ProgramId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetProgramById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newProgram = res.data.Data;
				$scope.newProgram.Mode = 'Modify';

				document.getElementById('Program-section').style.display = "none";
				document.getElementById('Program-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelProgramById = function (refData) {

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
					ProgramId: refData.ProgramId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelProgram",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllProgramList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Syllabus *********************************
	$scope.CurrentSyllabus = {};
	$scope.ChangeCurrentSyllabus = function (Syllabus) {
		$scope.CurrentSyllabus = Syllabus || {};
	}
	$scope.AddTopic = function (ind) {
		if ($scope.CurrentSyllabus.TopicColl) {
			if ($scope.CurrentSyllabus.TopicColl.length > ind + 1) {
				$scope.CurrentSyllabus.TopicColl.splice(ind + 1, 0, {
					SNo: 0,
					TopicName: ''
				})
			} else {
				$scope.CurrentSyllabus.TopicColl.push({
					SNo: 0,
					TopicName: ''
				})
			}
		}
		var sno = 1;
		angular.forEach($scope.CurrentSyllabus.TopicColl, function (tc) {
			tc.SNo = sno;
			sno++;
		});

	};
	$scope.DelTopic = function (ind) {
		if ($scope.CurrentSyllabus.TopicColl) {
			if ($scope.CurrentSyllabus.TopicColl.length > 1) {
				$scope.CurrentSyllabus.TopicColl.splice(ind, 1);
			}
		}

		var sno = 1;
		angular.forEach($scope.CurrentSyllabus.TopicColl, function (tc) {
			tc.SNo = sno;
			sno++;
		});
	};

	$scope.ChangeNoOfSyllabus = function () {
		var nl = $scope.newSyllabus.NoOfSyllabus;
		if (nl > $scope.newSyllabus.DetailsColl.length) {
			var needToAdd = nl - $scope.newSyllabus.DetailsColl.length;
			for (var i = 0; i < needToAdd; i++) {
				$scope.newSyllabus.DetailsColl.push({
					SNo: 0,
					TopicColl: [{ SNo: 1, TopicName: '' }],
				});
			}
		} else {
			var needToAdd = $scope.newSyllabus.DetailsColl.length - nl;
			for (var i = 0; i < needToAdd; i++) {
				var ind = $scope.newSyllabus.DetailsColl.length - 1;
				$scope.newSyllabus.DetailsColl.splice(ind, 1);
			}
		}

        var sno = 1;
        angular.forEach($scope.newSyllabus.DetailsColl, function (dc) {
            dc.SNo = sno;
            sno++;
        });
	};

    $scope.addNL = function (val) {
        if (val < 0) {
			if ($scope.newSyllabus.NoOfSyllabus > 0)
				$scope.newSyllabus.NoOfSyllabus = $scope.newSyllabus.NoOfSyllabus + val;

        } else {
			$scope.newSyllabus.NoOfSyllabus = $scope.newSyllabus.NoOfSyllabus + val;
        }
        $scope.ChangeNoOfSyllabus();
    }

	$scope.SaveUpdateSyllabus = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: 'POST',
			url: base_url + "AppCMS/Creation/SaveSyllabus",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
			data: { jsonData: $scope.newSyllabus}
        }).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearSyllabus();
				$scope.GetAllSyllabusList();
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

    }

	$scope.GetAllSyllabusList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SyllabusList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllSyllabus",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SyllabusList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

    $scope.DelSyllabusById = function (refData) {
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
                    url: base_url + "AppCMS/Creation/DelSyllabus",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllSyllabusList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


	$scope.GetSyllabusPlanById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetSyllabusPlanById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSyllabus = res.data.Data;
				$scope.newSyllabus.Mode = 'Modify';
				document.getElementById('Syllabus-section').style.display = "none";
				document.getElementById('Syllabus-form').style.display = "block";
				//$scope.GetSyllabusTopicById($scope.newSyllabus.SyllabusId);



				if ($scope.newSyllabus.DetailsColl && $scope.newSyllabus.DetailsColl.length > 0) {
					// Set CurrentSyllabus to the first entry in DetailsColl
					$scope.CurrentSyllabus = $scope.newSyllabus.DetailsColl[0];

					// Optionally, you can also load the TopicColl for the first entry in CurrentSyllabus
					if ($scope.CurrentSyllabus.TopicColl && $scope.CurrentSyllabus.TopicColl.length > 0) {
						$scope.selectedTopic = $scope.CurrentSyllabus.TopicColl[0]; // You can bind this to your view as needed
					}
				}


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	//$scope.GetSyllabusTopicById = function (refData) {
	//	if (refData.SyllabusId > 1) {
	//		$scope.loadingstatus = "running";
	//		showPleaseWait();

	//		var para = {
	//			SyllabusId: refData.SyllabusId
	//		};

	//		$http({
	//			method: 'POST',
	//			url: base_url + "AppCMS/Creation/GetSyllabusTopicById",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				//$scope.CurrentSyllabus = {
	//				//	SyllabusId: refData.SyllabusId,
	//				//	SyllabusName: refData.SyllabusName,
	//				//	SNo: refData.SNo,
	//				//	TopicColl: res.data.Data
	//				//};
	//				$scope.CurrentSyllabus = res.data.Data;
	//				$scope.ChangeCurrentSyllabus($scope.CurrentSyllabus);

	//				if (!$scope.CurrentSyllabus.TopicColl || $scope.CurrentSyllabus.TopicColl.length === 0) {
	//					$scope.CurrentSyllabus.TopicColl = [{ SNo: 1, TopicName: '' }];
	//				}
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
 //       }
	//};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});