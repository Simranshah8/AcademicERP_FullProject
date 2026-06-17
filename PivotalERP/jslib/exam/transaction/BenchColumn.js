app.controller('BenchColumnController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Bench Column';
    var gSrv = GlobalServices;
    OnClickDefault();

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = gSrv.getConfirmMSG();
        $scope.perPageColl = gSrv.getPerPageList();

        $scope.searchData = {
            BenchColumn: '',
            BulkRoomDetail: ''
        };

        $scope.ExamTypeList = [];
        gSrv.getExamTypeList().then(function (res) {
            $scope.ExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ExamShiftList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllExamShiftList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExamShiftList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newDet = {
            RoomId: null,
            BenchTypeName: '',
            NoOfBench: 0,
            OrderNo: 0,
            DetailColl: [],
            Mode: 'Save'
        };
        $scope.GetAllBenchColumn();


        $scope.beData = {};

        $scope.newBulk = {
            BulkRoomDetail: []
        }
        $scope.newBulk.BulkRoomDetail.push({});
        $scope.GetAllExamBulkRoom();

    }

    $scope.AddBulkRoom = function (ind) {
        if ($scope.newBulk.BulkRoomDetail) {
            if ($scope.newBulk.BulkRoomDetail.length > ind + 1) {
                $scope.newBulk.BulkRoomDetail.splice(ind + 1, 0, {
                    Rate: ''
                })
            } else {
                $scope.newBulk.BulkRoomDetail.push({
                    Rate: ''
                })
            }
        }
    };
    $scope.delBulkRoom = function (ind) {
        if ($scope.newBulk.BulkRoomDetail) {
            if ($scope.newBulk.BulkRoomDetail.length > 1) {
                $scope.newBulk.BulkRoomDetail.splice(ind, 1);
            }
        }
    };

    function OnClickDefault() {
        document.getElementById('form-part').style.display = "none";
        document.getElementById('roomallocationform').style.display = "none";

        document.getElementById('add-benchcolumn').onclick = function () {
            document.getElementById('listpart').style.display = "none";
            document.getElementById('form-part').style.display = "block";
        }
        document.getElementById('back-btn').onclick = function () {
            document.getElementById('form-part').style.display = "none";
            document.getElementById('listpart').style.display = "block";
        }


        document.getElementById('add-roomallocation').onclick = function () {
            document.getElementById('RoomAllocationlistpart').style.display = "none";
            document.getElementById('roomallocationform').style.display = "block";
        }
        document.getElementById('backtolist2').onclick = function () {
            document.getElementById('roomallocationform').style.display = "none";
            document.getElementById('RoomAllocationlistpart').style.display = "block";
        }
    }

    $scope.ClearBenchColumn = function () {
        $scope.newDet = {
            RoomId: null,
            BenchTypeName: '',
            NoOfBench: 0,
            OrderNo: 0,
            BenchColumnDetailColl: [],
            Mode: 'Save'
        };
    }


    $scope.ClearRoomAllocation = function () {
        $scope.newRoomAllocation = {
            RoomAllocationId: null,
            ExamTypeId: null,
            ExamShiftId: null,
            RoomNamelist: [],
            CheckAll: false,
            Mode: 'Save'
        };

        $timeout(function () {
            angular.forEach($scope.newBulk.BulkRoomDetail, function (fi) {
                fi.IsChecked = false;
                $scope.newRoomAllocation.RoomNamelist.push(fi);
            })
        });

    }
    $scope.CheckAllRoom = function () {
        angular.forEach($scope.newRoomAllocation.RoomNamelist, function (fi) {
            fi.IsChecked = $scope.newRoomAllocation.CheckAll;
        });
    };


    $scope.generateColumnRow = function () {
        // Ensure BenchColumnDetailColl exists
        if (!$scope.newDet.BenchColumnDetailColl) {
            $scope.newDet.BenchColumnDetailColl = [];
        }

        const existingCount = $scope.newDet.BenchColumnDetailColl.length;
        const targetCount = $scope.newDet.NoOfColumn;

        // Add new columns if the target count is greater than the existing count
        if (targetCount > existingCount) {
            for (let i = existingCount + 1; i <= targetCount; i++) {
                $scope.newDet.BenchColumnDetailColl.push({
                    ColSNo: i,
                    Banch_Column_Name: '', // Set default or retain existing values
                });
            }
        }
        // Remove extra columns if the target count is less than the existing count
        else if (targetCount < existingCount) {
            $scope.newDet.BenchColumnDetailColl.splice(targetCount, existingCount - targetCount);
        }
    };

    $scope.IsValidBenchColumn = function () {
        if ($scope.newDet.BenchTypeName.isEmpty()) {
            Swal.fire('Please ! Enter Bench Type Name');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateBenchColumn = function () {
        if ($scope.IsValidBenchColumn() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateBenchColumn();
                    }
                });
            } else
                $scope.CallSaveUpdateBenchColumn();
        }
    };

    $scope.CallSaveUpdateBenchColumn = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/SaveUpdateBenchColumn",
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
                $scope.ClearBenchColumn();
                $scope.GetAllBenchColumn();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllBenchColumn = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.BenchColumnList = [];
        $http({
            method: 'GET',
            url: base_url + "Exam/Transaction/GetAllBenchColumn",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BenchColumnList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetBenchColumnById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            BenchColumnId: refData.BenchColumnId
        };
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/getBenchColumnById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;
                $scope.newDet.Mode = 'Save';
                document.getElementById('listpart').style.display = "none";
                document.getElementById('form-part').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.getBenchColumnName = function (selectedRowDetails, showModal = false) {
        return new Promise((resolve, reject) => {
            $scope.loadingstatus = "running";
            showPleaseWait();

            const para = {
                NoOfColumn: selectedRowDetails.NoOfBanchRow,
            };

            $http.post(base_url + "Exam/Transaction/GetColumnNameById", JSON.stringify(para))
                .then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";

                    if (res.data.IsSuccess && Array.isArray(res.data.Data) && res.data.Data.length > 0) {
                        selectedRowDetails.DetailColl = res.data.Data.map((item, index) => ({
                            Banch_Row_Name: item.ColumnName || `Column ${index + 1}`,
                            Banch_Row_SNo: index + 1,
                            NoOfBanch: 0,
                            NoOfSeatsInRow: 0,
                        }));

                        const selectedColumn = $scope.BenchColumnList.find(
                            column => column.NoOfColumn === selectedRowDetails.NoOfBanchRow
                        );

                        if (selectedColumn) {
                            const noOfColumns = selectedColumn.NoOfColumn || 1;
                            const totalBenches = selectedRowDetails.TotalBench || 0;
                            const totalCapacity = selectedRowDetails.TotalCapacity || 0;

                            const baseBanchPerRow = Math.floor(totalBenches / noOfColumns);
                            const remainder = totalBenches % noOfColumns;

                            selectedRowDetails.DetailColl.forEach((et, index) => {
                                et.NoOfBanch = baseBanchPerRow + (index === selectedRowDetails.DetailColl.length - 1 ? remainder : 0);
                                et.NoOfSeatsInRow = totalBenches > 0 ? Math.floor(totalCapacity / totalBenches) : 0;
                            });
                        }

                        if (showModal) {
                            $scope.selectedRowDetails = selectedRowDetails;
                            $('#modal-detail').modal('show');
                        }

                        resolve(); // Ensure the Promise resolves here
                    } else {
                        Swal.fire('No data available for the selected column.');
                        selectedRowDetails.DetailColl = [];
                        resolve(); // Resolve promise to avoid hanging the flow
                    }
                })
                .catch(function (error) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire('Error', error.statusText || 'An unknown error occurred', 'error');
                    selectedRowDetails.DetailColl = [];
                    reject(error); // Reject promise on error
                });
        });
    };








    $scope.DelBenchColumnById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.BenchTypeName + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { BenchColumnId: refData.BenchColumnId };
                $http({
                    method: 'POST',
                    url: base_url + "Exam/Transaction/DeleteBenchColumn",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllBenchColumn();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }

    //-------------------------Exam bulk Room ---------------------------------
    $scope.IsValidExamBulkRoom = function () {
        var isInvalid = false;
        angular.forEach($scope.newBulk.BulkRoomDetail, function (S) {
            if (!S.RoomName || S.RoomName.trim() === '') {
                isInvalid = true;
                return;
            }
        });

        if (isInvalid) {
            Swal.fire('Please Enter Exam Bulk Room Name');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateExamBulkRoom = function () {
        if ($scope.IsValidExamBulkRoom() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newBulk.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateExamBulkRoom();
                    }
                });
            } else
                $scope.CallSaveUpdateExamBulkRoom();
        }
    };

    //$scope.CallSaveUpdateExamBulkRoom = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();

    //    var dataToSave = [];
    //    for (var i = 0; i < $scope.newBulk.BulkRoomDetail.length; i++) {
    //        var S = $scope.newBulk.BulkRoomDetail[i];
    //        var roomName = S.RoomName;
    //        var totalCapacity = S.TotalCapacity;
    //        var totalBench = S.TotalBench;
    //        var noOfBanchRow = S.NoOfBanchRow;
    //        var dataItem = {
    //            RoomName: roomName,
    //            TotalCapacity: totalCapacity,
    //            TotalBench: totalBench,
    //            NoOfBanchRow: noOfBanchRow,

    //        };
    //        dataToSave.push(dataItem);
    //    }

    //    $http({
    //        method: 'POST',
    //        url: base_url + "Exam/Transaction/SaveExamBulkRoom",
    //        headers: { 'Content-Type': undefined },
    //        transformRequest: function (data) {
    //            var formData = new FormData();
    //            formData.append("jsonData", angular.toJson(data.jsonData));
    //            return formData;
    //        },
    //        data: { jsonData: dataToSave }
    //    }).then(function (res) {
    //        $scope.loadingstatus = "stop";
    //        hidePleaseWait();
    //        Swal.fire(res.data.ResponseMSG);
    //        if (res.data.IsSuccess == true) {

             
    //        }
    //    }, function (errormessage) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //    });
    //}



    $scope.CallSaveUpdateExamBulkRoom = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var dataColl = [];
        var promises = [];

        // Iterate through BulkRoomDetail to collect data
        angular.forEach($scope.newBulk.BulkRoomDetail, function (fm) {
            if (fm.TotalCapacity && fm.TotalBench > 0) {
                // Call getBenchColumnName without showing the modal
                if (!fm.DetailColl || fm.DetailColl.length === 0) {
                    promises.push($scope.getBenchColumnName(fm, false));  // Wait for this to complete
                }
            }
        });

        // Ensure all asynchronous calls are complete before continuing
        Promise.all(promises).then(function () {
            // After all getBenchColumnName calls have completed
            angular.forEach($scope.newBulk.BulkRoomDetail, function (fm) {
                if (fm.TotalCapacity && fm.TotalBench > 0) {
                    var beData = {
                        RoomName: fm.RoomName || '',
                        TotalCapacity: fm.TotalCapacity || 0,
                        TotalBench: fm.TotalBench || 0,
                        NoOfBanchRow: fm.NoOfBanchRow || 0,
                        DetailColl: fm.DetailColl || []
                    };

                    dataColl.push(beData);
                }
            });

            // Make the HTTP POST request after updating the details
            $http({
                method: 'POST',
                url: base_url + "Exam/Transaction/SaveExamBulkRoom",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("jsonData", angular.toJson(data.jsonData));
                    return formData;
                },
                data: { jsonData: dataColl }
            }).then(function (res) {
                $scope.loadingstatus = "stop";
                hidePleaseWait();

                if (res.data && res.data.IsSuccess) {
                    Swal.fire('Success', res.data.ResponseMSG, 'success');
                    $scope.GetAllExamBulkRoom();
                } else {
                    Swal.fire('Error', res.data.ResponseMSG || 'An error occurred', 'error');
                }
            }).catch(function (errormessage) {
                $scope.loadingstatus = "stop";
                hidePleaseWait();
                Swal.fire('Error', errormessage.statusText || 'An unknown error occurred', 'error');
            });
        }).catch(function (error) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire('Error', 'An error occurred while fetching bench column data.', 'error');
        });
    };









    $scope.GetAllExamBulkRoom = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.newBulk.BulkRoomDetail = []; // Ensure BulkRoomDetail is initialized
        $http({
            method: 'GET',
            url: base_url + "Exam/Transaction/GetAllExamBulkRoom",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                // Assign the data from the response
                $scope.newBulk.BulkRoomDetail = res.data.Data;
            } else {
                // Show error message if IsSuccess is false
                Swal.fire(res.data.ResponseMSG);
            }

            // Handle empty or missing data
            if (!$scope.newBulk.BulkRoomDetail || $scope.newBulk.BulkRoomDetail.length === 0) {
                // Add a default row with required structure
                $scope.newBulk.BulkRoomDetail = [{
                    RoomName: "",
                    TotalCapacity: 0,
                    TotalBench: 0,
                    NoOfBanchRow: null
                }];
            }

        }, function (reason) {
            hidePleaseWait();
            Swal.fire('Failed: ' + reason);
        });
    };




    //----------------------------Room Allocation-----------------------------
    $scope.IsValidRoomAllocation = function () {
        //if ($scope.newRoomAllocation.Name.isEmpty()) {
        //    Swal.fire('Please ! Enter Fee Item Group Name');
        //    return false;
        //}
        return true;
    }

    $scope.SaveUpdateRoomAllocation = function () {
        if ($scope.IsValidRoomAllocation() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newRoomAllocation.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateRoomAllocation();
                    }
                });
            } else
                $scope.CallSaveUpdateRoomAllocation();

        }
    };

    $scope.CallSaveUpdateRoomAllocation = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $scope.newRoomAllocation.RoomNameIdColl = [];
        angular.forEach($scope.newRoomAllocation.RoomNamelist, function (fi) {
            if (fi.IsChecked == true)
                $scope.newRoomAllocation.RoomNameIdColl.push(fi.ExamRoomId);
        });



        $http({
            method: 'POST',
            url: base_url + "Exam/Creation/SaveRoomAllocation",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newRoomAllocation }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearRoomAllocation();
                $scope.GetAllRoomAllocationList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    }
});