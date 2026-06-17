app.controller('ObjectiveController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Objective';
    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            Objective: 1,
            ObjectiveTransfer: 1,
        };

        $scope.searchData = {
            Objective: '',
            ObjectiveTransfer: '',
        };

        $scope.perPage = {
            Objective: GlobalServices.getPerPageRow(),
            ObjectiveTransfer: GlobalServices.getPerPageRow(),
        };

        $scope.ClassSectionList = [];
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSectionList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ExamTypeList = [];
        GlobalServices.getExamTypeList().then(function (res) {
            $scope.ExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newDet = {
            ClassId: null,
            ExamTypeId: null,
            SubjectId: null,
            FullMark: '',
            ObjectiveDetailsColl: [],
            Mode: 'save'
        };
        $scope.newDet.ObjectiveDetailsColl.push({});
        $scope.GetAllObjectiveList();

        $scope.newTransfer = {
            FromExamTypeId: null,
            ToExamTypeId: null,
            FromClassId: null,
            ToClassId: null,
            FromSectionId: null,
            ToSectionId: null,
            IsClassWise: false
        };
    };


    $scope.ClearDetails = function () {
         $scope.newDet = {
                ClassId: null,
                ExamTypeId: null,
                SubjectId: null,
                FullMark: '',
                ObjectiveDetailsColl: [],
                Mode: 'save'
         };
         $scope.newDet.ObjectiveDetailsColl.push({});
    };

    $scope.ClearTransfer = function () {
        $scope.newTransfer = {
            FromExamTypeId: null,
            ToExamTypeId: null,
            FromClassId: null,
            ToClassId: null,
            FromSectionId: null,
            ToSectionId: null,
            IsClassWise: false
        };
    }

    function OnClickDefault() {
        document.getElementById('Objective-form').style.display = "none";

        document.getElementById('add-Details').onclick = function () {
            document.getElementById('Objective-section').style.display = "none";
            document.getElementById('Objective-form').style.display = "block";
        }

        document.getElementById('back-Details').onclick = function () {
            document.getElementById('Objective-section').style.display = "block";
            document.getElementById('Objective-form').style.display = "none";
        }
    }

    $scope.AddPHDDetails = function (ind) {
        if ($scope.newDet.ObjectiveDetailsColl) {
            if ($scope.newDet.ObjectiveDetailsColl.length > ind + 1) {
                $scope.newDet.ObjectiveDetailsColl.splice(ind + 1, 0, {
                    ClassName: ''
                })
            } else {
                $scope.newDet.ObjectiveDetailsColl.push({
                    ClassName: ''
                })
            }
        }
    };

    $scope.delPHDDetails = function (ind) {
        if ($scope.newDet.ObjectiveDetailsColl) {
            if ($scope.newDet.ObjectiveDetailsColl.length > 1) {
                $scope.newDet.ObjectiveDetailsColl.splice(ind, 1);
            }
        }
    };


    $scope.IsValidObjective = function () {
        return true;
    }

    $scope.SaveUpdateObjective = function () {

        if ($scope.newDet.ObjectiveDetailsColl.length > 0 && $scope.newDet.ObjectiveDetailsColl[0].ObjectiveId) {
            $scope.newDet.ObjectiveId = $scope.newDet.ObjectiveDetailsColl[0].ObjectiveId;
        }

        if ($scope.IsValidObjective() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateObjective();
                    }
                });
            } else
                $scope.CallSaveUpdateObjective();

        }
    };

    $scope.CallSaveUpdateObjective = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.newDet.ClassId = $scope.newDet.SelectedClass.ClassId;
        $scope.newDet.SectionId = $scope.newDet.SelectedClass.SectionId;
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/SaveObjective",
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
                $scope.ClearDetails();
                $scope.GetAllObjectiveList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetObjectiveDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ClassId: $scope.newDet.ClassId,
            ExamTypeId: $scope.newDet.ExamTypeId,
            SubjectId: $scope.newDet.SubjectId
        };
        $http({
           method: 'POST',
           url: base_url + "Exam/Transaction/GetObjective",
           dataType: "json",
           data: JSON.stringify(para)
        }).then(function (res) {
           hidePleaseWait();
           $scope.loadingstatus = "stop";
           if (res.data.IsSuccess && res.data.Data) {
               $scope.newDet.Mode = 'Modify';

               $scope.newDet.ObjectiveDetailsColl = res.data.Data.ObjectiveDetailsColl;
               if (!$scope.newDet.ObjectiveDetailsColl || $scope.newDet.ObjectiveDetailsColl.length == 0) {
                   $scope.newDet.ObjectiveDetailsColl = [];
                   $scope.newDet.ObjectiveDetailsColl.push({});
               }
                 
               if ($scope.newDet.ObjectiveDetailsColl.length > 0) {
                   $scope.newDet.FullMark =  $scope.newDet.ObjectiveDetailsColl[0].FullMark;
               }
           } else {
                    Swal.fire(res.data.ResponseMSG);
           }

        }, function (reason) {
                Swal.fire('Failed' + reason);
        });

    }

    $scope.GetAllObjectiveList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
        $scope.ObjectiveList = [];
		$http({
			method: 'POST',
            url: base_url + "Exam/Transaction/GetAllObjective",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
                $scope.ObjectiveList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

    $scope.DelObjectiveById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.ClassName + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { ObjectiveId: refData.ObjectiveId };
                $http({
                    method: 'POST',
                    url: base_url + "Exam/Transaction/DelObjective",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllObjectiveList();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });



    }

    $scope.GetClassSummaryClassWiseSubject = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ClassId: $scope.newDet.SelectedClass.ClassId
        };
        $scope.SubjectList = [];

        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetClassSummarySubjectClassWise",
            dataType: "json",
            data: JSON.stringify(para)
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

    }

    $scope.excludeSelectedExamType = function (item) {
        return item.id !== $scope.newTransfer.FromExamTypeId;
    };

    $scope.excludeSelectedClass = function (item) {
        if (!$scope.newTransfer || !$scope.newTransfer.SelectedClass) {
            return true;
        }
        return item.ClassId !== $scope.newTransfer.SelectedClass.ClassId;
    };

    $scope.TransferObjective = function (refData) {
            Swal.fire({
                title: 'Do you want to Transfer Objective',
                showCancelButton: true,
                confirmButtonText: 'Transfer',
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    $scope.loadingstatus = "running";
                    showPleaseWait();
                    var para = {
                        FromExamTypeId: $scope.newTransfer.FromExamTypeId,
                        ToExamTypeId: $scope.newTransfer.ToExamTypeId,
                        FromClassId: $scope.newTransfer.SelectedClass.ClassId,
                        ToClassId: $scope.newTransfer.SelectedClass2.ClassId,
                        FromSectionId: $scope.newTransfer.SelectedClass.SectionId,
                        ToSectionId: $scope.newTransfer.SelectedClass2.SectionId,
                    };
                    $http({
                        method: 'POST',
                        url: base_url + "Exam/Transaction/TransferObjective",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";
                        Swal.fire(res.data.ResponseMSG);
                        $scope.ClearTransfer();
                        $scope.GetAllObjectiveList();
                        $scope.GetObjectiveTransferDetails();

                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                }
            });
    };

    $scope.GetObjectiveTransferDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.TransferList = [];
        var para = {
            FromExamTypeId: $scope.newTransfer.FromExamTypeId || null,
            ToExamTypeId: $scope.newTransfer.ToExamTypeId || null,
            FromClassId: ($scope.newTransfer.SelectedClass && $scope.newTransfer.SelectedClass.ClassId) || null,
            ToClassId: ($scope.newTransfer.SelectedClass2 && $scope.newTransfer.SelectedClass2.ClassId) || null
        };
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllObjectiveTansfer",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TransferList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
                
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }
});