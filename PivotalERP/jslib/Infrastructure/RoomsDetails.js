app.controller('RoomsDetailsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Rooms Details';

    OnClickDefault();

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();


        $scope.currentPages = {
            RoomsDetails: 1

        };

        $scope.searchData = {
            RoomsDetails: ''
        };

        $scope.perPage = {
            RoomsDetails: GlobalServices.getPerPageRow()

        };

        $scope.RoomTypeColl = [
            { id: 1, text: 'Class Room' },
            { id: 2, text: 'Other Room' },
        ];

        $scope.BuildingList = [];
        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetAllBuildingList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BuildingList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.FloorList = [];
        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetAllFloorList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.FloorList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.UtilitiesList = [];
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Setup/GetAllUtilities",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.UtilitiesList = res.data.Data.filter(item => item.IsActive === true);
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newDet = {
            FloorWiseRoomDetailsId:null,
            BuildingId: null,
            FloorId: null,
            subFloorWiseRoomDetailsColl: [],
            Mode: 'Save'
        };

        $scope.GetFloorWiseRoomDetails();

    }

    $scope.ClearRoomDetails = function () {
        $scope.newDet = {
            FloorWiseRoomDetailsId: null,
            BuildingId: null,
            FloorId: null,
            subFloorWiseRoomDetailsColl: [],
            Mode: 'Save'
        };
        $scope.FloorDet= [];
    }

    function OnClickDefault() {
        document.getElementById('room-details-form').style.display = "none";

        document.getElementById('add-room-details').onclick = function () {
            document.getElementById('room-details-tbl').style.display = "none";
            document.getElementById('room-details-form').style.display = "block";
            $scope.ClearRoomDetails();
        }

        document.getElementById('back-room-details-list-btn').onclick = function () {
            document.getElementById('room-details-form').style.display = "none";
            document.getElementById('room-details-tbl').style.display = "block";
            $scope.ClearRoomDetails();
        }

    }

    //$scope.SubUtilitiesList = [];

    //$scope.SubUtilitiesById = function (UtilitiesId, cl) {
    //    if (!UtilitiesId) return;

    //    $scope.loadingstatus = "running";
    //    showPleaseWait();

    //    var para = { UtilitiesId: UtilitiesId };

    //    $http({
    //        method: 'POST',
    //        url: base_url + "Infrastructure/Setup/getUtilitiesById",
    //        dataType: "json",
    //        data: JSON.stringify(para)
    //    }).then(function (res) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";

    //        if (res.data.IsSuccess && res.data.Data) {
    //            cl.SubUtilitiesList = res.data.Data.SubUtilitiesColl || [];


    //            let existingSubUtility = cl.SubUtilitiesList.find(sub => sub.Name === cl.SubUtility);
    //            if (!existingSubUtility) {
    //                cl.SubUtility = null;
    //            }
    //        } else {
    //            Swal.fire(res.data.ResponseMSG);
    //            cl.SubUtilitiesList = [];
    //        }
    //    }, function (reason) {
    //        Swal.fire('Failed' + reason);
    //        cl.SubUtilitiesList = [];
    //    });
    //};



    //Floor Mapping
    $scope.ChangeBuilding = function () {
        if (!$scope.newDet.BuildingId) {
            $scope.FloorMappingList = [];
            return;
        }
        $scope.GetAllFloorMapping();
        $scope.newDet.FloorId = null;
        $scope.newDet.subFloorWiseRoomDetailsColl = [];
    };

    $scope.GetAllFloorMapping = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.FloorMappingList = [];
        var para = {
            BuildingId: $scope.newDet.BuildingId,
        };
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/GetAllFloorMapping",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.FloorMappingList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.DetailsByBuildingFloor = function () {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
                BuildingId: $scope.newDet.BuildingId,
            FloorId: $scope.newDet.FloorId,
        };

        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/DetailsByBuildingFloor",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.FloorDet = res.data.Data;
                $scope.changeList();
                // Iterate over each room detail and fetch sub-utilities if UtilitiesId is present
                $timeout(function () {
                    angular.forEach($scope.newCollection, function (room) {
                        if (room.UtilitiesId) {
                            $scope.SubUtilitiesById(room.UtilitiesId);
                        }
                    });
                });

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.changeList = function () {
        var TotalFloor = parseInt($scope.FloorDet.NoOfClassRooms) + parseInt($scope.FloorDet.NoOfOtherRooms);

        var para = {
            BuildingId: $scope.newDet.BuildingId,
            FloorId: $scope.newDet.FloorId,
        };
        $scope.GetAllFloorWiseRoomDetails(para).then(function () {
            $scope.newDet.subFloorWiseRoomDetailsColl = [];
            if (!(!$scope.newCollection || $scope.newCollection.length === 0)) {
                angular.forEach($scope.newCollection, function (mn) {
                    $scope.SubUtilitiesById(mn.UtilitiesId, mn);


                    $scope.newDet.subFloorWiseRoomDetailsColl.push({
                        Name: mn.Name,
                        UtilitiesId: mn.UtilitiesId,
                        SubUtility: mn.SubUtility,
                        Length: mn.Length,
                        Breadth: mn.Breadth,
                        Capacity: mn.Capacity,
                        Resources: mn.Resources,
                        RoomTypeId: mn.RoomTypeId,
                        FloorWiseRoomDetailsId: mn.FloorWiseRoomDetailsId,
                        SubUtilitiesList: mn.SubUtilitiesList,
                    });
                })
                for (let i = 0; i < (TotalFloor - $scope.newCollection.length); i++) {
                    $scope.newDet.subFloorWiseRoomDetailsColl.push({
                        Name: '',
                        SubUtility: ''
                    });
                }
            } else {
                for (let i = 0; i < (TotalFloor || 0); i++) {
                    $scope.newDet.subFloorWiseRoomDetailsColl.push({
                        Name: '',
                        SubUtility: ''
                    });
                }
            }

            for (let i = 0; i < parseInt($scope.FloorDet.NoOfClassRooms); i++) {
                $scope.newDet.subFloorWiseRoomDetailsColl[i].RoomTypeId = 1;
            }
            for (let i = parseInt($scope.FloorDet.NoOfClassRooms); i < TotalFloor; i++) {
                $scope.newDet.subFloorWiseRoomDetailsColl[i].RoomTypeId = 2;
            }
        })

        
        //$scope.GetAllFloorWiseRoomDetails();

    }


    $scope.IsValidRoomDetails = function () {
       /* if ($scope.ValidateRooomCount() == true)*/
            return true;
    }

    $scope.SaveUpdateRoomDetails = function () {
        if ($scope.IsValidRoomDetails() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateRoomDetails();
                    }
                });
            } else
                $scope.CallSaveUpdateRoomDetails();

        }
    };


    $scope.CallSaveUpdateRoomDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var buildingId = $scope.newDet.BuildingId;
        var floorId = $scope.newDet.FloorId;
        var dataToSave = [];
        for (var i = 0; i < $scope.newDet.subFloorWiseRoomDetailsColl.length; i++) {
            var S = $scope.newDet.subFloorWiseRoomDetailsColl[i];
            var Name = S.Name;
            var UtilitiesId = S.UtilitiesId;
            var SubUtility = S.SubUtility;
            var Length = S.Length;
            var Breadth = S.Breadth;
            var Capacity = S.Capacity;
            var Resources = S.Resources;
            var RoomTypeId = S.RoomTypeId;
            var FloorWiseRoomDetailsId = S.FloorWiseRoomDetailsId || '';

            var dataItem = {
                BuildingId: buildingId,
                FloorId: floorId,
                Name: Name,
                UtilitiesId: UtilitiesId,
                SubUtility: SubUtility,
                Length: Length,
                Breadth: Breadth,
                Capacity: Capacity,
                Resources: Resources,
                RoomTypeId: RoomTypeId,
                FloorWiseRoomDetailsId: FloorWiseRoomDetailsId,

            };
            dataToSave.push(dataItem);
        }


        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/SaveFloorwiseRoomDetails",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: dataToSave }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearRoomDetails();
                $scope.GetFloorWiseRoomDetails();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllFloorWiseRoomDetails = function (refData) {
        if ($scope.newDet.BuildingId && $scope.newDet.FloorId) {
            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                BuildingId: $scope.newDet.BuildingId,
                FloorId: $scope.newDet.FloorId,
            };

            return $http({
                method: 'POST',
                url: base_url + "Infrastructure/Creation/GetAllFloorWiseRoomDetails",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

                if (res.data.IsSuccess && res.data.Data) {
                    $scope.newDet.Mode = "Save";
                    $scope.newCollection = res.data.Data;

                    // Iterate over each room detail and fetch sub-utilities if UtilitiesId is present
                    $timeout(function () {
                        angular.forEach($scope.newCollection, function (room) {
                            if (room.UtilitiesId) {
                                $scope.SubUtilitiesById(room.UtilitiesId, room);
                            }
                        });
                    });

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
                return res.data;

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        }
    };

    // Updated SubUtilitiesById function to accept parameters
    $scope.subUtilitiesCache = {}; // Cache to store fetched SubUtilitiesLists

    $scope.SubUtilitiesById = function (UtilitiesId) {
        if (!UtilitiesId) return;

        // Check if data already exists in the cache
        if ($scope.subUtilitiesCache[UtilitiesId]) {
            updateRowsWithSubUtilities(UtilitiesId, $scope.subUtilitiesCache[UtilitiesId]);
            return;
        }

        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = { UtilitiesId: UtilitiesId };

        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Setup/getUtilitiesById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                let subUtilitiesList = res.data.Data.SubUtilitiesColl || [];

                // Store data in cache
                $scope.subUtilitiesCache[UtilitiesId] = subUtilitiesList;

                updateRowsWithSubUtilities(UtilitiesId, subUtilitiesList);
            } else {
                Swal.fire(res.data.ResponseMSG);
                updateRowsWithSubUtilities(UtilitiesId, []);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
            updateRowsWithSubUtilities(UtilitiesId, []);
        });
    };

    function updateRowsWithSubUtilities(UtilitiesId, subUtilitiesList) {
        // Update all rows with the same UtilitiesId
        $scope.newDet.subFloorWiseRoomDetailsColl.forEach(row => {
            if (row.UtilitiesId === UtilitiesId) {
                row.SubUtilitiesList = subUtilitiesList;

                // Ensure SubUtility is still valid, otherwise reset it
                let existingSubUtility = subUtilitiesList.find(sub => sub.Name === row.SubUtility);
                if (!existingSubUtility) {
                    row.SubUtility = null;
                }
            }
        });
    }


    $scope.GetFloorWiseRoomDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.Filter = $scope.Filter || {};
        var para = {
            BuildingId: $scope.Filter.BuildingId || null,
            FloorId: $scope.Filter.FloorId || null,
        };
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/GetAllFloorWiseRoomDetails",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                $scope.FilterViewList = res.data.Data;

                var groupedData = mx(res.data.Data)
                    .groupBy(fm => ({ BuildingId: fm.BuildingId, FloorId: fm.FloorId }))
                    .toArray();

                var tmpColl = [];

                angular.forEach(groupedData, function (group) {
                    var firstElement = group.elements[0];

                    var newBuildingData = {
                        BuildingId: firstElement.BuildingId,
                        BuildingName: firstElement.BuildingName,
                        FloorName: firstElement.FloorName,
                        Rooms: []
                    };

                    angular.forEach(group.elements, function (room) {
                        newBuildingData.Rooms.push(room);
                    });

                    tmpColl.push(newBuildingData);
                });

                $scope.GroupedRoomMappings = tmpColl;


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    

    $scope.pageChangeHandler = function (num) {
        console.log('page changed to ' + num);
    };

});