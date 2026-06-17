app.directive('select', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                $(element).select2({
                    placeholder: "**Select an Option**",
                    allowClear: true
                });

                // Watch ngModel safely
                scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        $timeout(function () {
                            $(element).trigger('change');
                        });
                    }
                });

                // Ensure select2 updates when value changes externally
                $(element).on('change', function () {
                    $timeout(function () {
                        if (!scope.$$phase) {
                            scope.$apply();
                        }
                    });
                });

            }, 2);
        }
    };
});


app.controller('BuildingDetailsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Building Details';

    OnClickDefault();

    $scope.LoadData = function () {
        setTimeout(() => {
            $('.select2').select2();
        }, 100);

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.OverallConditionColl = [
            { id: 1, text: 'Very Good/No Damage' },
            { id: 2, text: 'Good/No Damage' },
            { id: 3, text: 'Normal Rash/Damage Grade 1' },
            { id: 4, text: 'Cracked/ Damage Grade 2 & 3' },
            { id: 5, text: 'Very Dilapidated/Damage Grade 4 & 5' },
        ];

        $scope.StructureTypeColl = [
            { id: 1, text: 'RCC Frame Structure' },
            { id: 2, text: 'Brick Masonry in cement mortar with RCC Slab (Load Bearing Structure)' },
            { id: 3, text: 'Stone Masonry in cement mortar with RCC Slab (Load Bearing Structure)' },
            { id: 4, text: 'Brick Masonry in mud mortar with RCC Slab (Load Bearing Structure)' },
            { id: 5, text: 'Stone Masonry in mud mortar with RCC Slab (Load Bearing Structure)' },
            { id: 6, text: 'Brick Masonry with Steel Truss' },
            { id: 7, text: 'Brick Masonry with c/c mortar' },
            { id: 8, text: 'Stone Masonry with c/c mortar' },
            { id: 9, text: 'Brick Masonry with mud mortar' },
            { id: 10, text: 'Stone Masonry with mud mortar' },
            { id: 11, text: 'Stone Frame with Steel Truss' },
            { id: 12, text: 'Stone Masonry with Steel Truss' },
            { id: 13, text: 'Wooden Structure' },
            { id: 14, text: 'Others' },
        ];

        $scope.RoofTypeColl = [
            { id: 1, text: 'RCC Slab' },
            { id: 2, text: 'CGI Sheet/UPVC Sheet' },
            { id: 3, text: 'Timber (Wood)' },
            { id: 4, text: 'Tile/Jhingati' },
            { id: 5, text: 'Slate Stone' },
            { id: 6, text: 'Khar/Bush' },
            { id: 7, text: 'Steel Truss with CGI Sheet' },
            { id: 8, text: 'Timber/Bamboo Structure with CGI Sheet' },
            { id: 9, text: 'Others' },
        ];

        $scope.DamageGradeColl = [
            { id: 1, text: 'Completely damaged' },
            { id: 2, text: 'Major damage' },
            { id: 3, text: 'Minor damage' },
            { id: 4, text: 'No damage' },
            { id: 5, text: 'Damage Grade 1' },
            { id: 6, text: 'Damage Grade 2' },
            { id: 7, text: 'Damage Grade 3' },
            { id: 8, text: 'Damage Grade 4' },
            { id: 9, text: 'Damage Grade 5' },
        ];

        $scope.InfrastructureTypeColl = [
            { id: 1, text: 'Academic Classes' },
            { id: 2, text: 'Toilet' },
            { id: 3, text: 'Toilet for Disable' },
            { id: 4, text: 'Hostel' },
            { id: 5, text: 'Lab' },
            { id: 6, text: 'Library' },
            { id: 7, text: 'Multipurpose Hall' },
            { id: 8, text: 'Canteen' },
            { id: 9, text: 'Administrative Room' },
        ];

        $scope.FundingSourceColl = [
            { "id": 1, "text": "Government of Nepal (GON) Grants" },
            { "id": 2, "text": "Ministry of Education, Science & Technology" },
            { "id": 3, "text": "Provincial Government Grants" },
            { "id": 4, "text": "Local Government Budget (Municipality/Rural Municipality)" },
            { "id": 5, "text": "Presidential Educational Reform Program" },
            { "id": 6, "text": "Parliamentary Development Funds" },
            { "id": 7, "text": "Asian Development Bank (ADB)" },
            { "id": 8, "text": "World Bank" },
            { "id": 9, "text": "UNICEF/UNESCO Support" },
            { "id": 10, "text": "Indian Government Grants" },
            { "id": 11, "text": "Other International NGOs & Development Agencies" },
            { "id": 12, "text": "Donor & Community Contributions" },
            { "id": 13, "text": "Institution Own Funds" },
            { "id": 14, "text": "Corporate Social Responsibility (CSR) Funds" },
            { "id": 15, "text": "Reconstruction Grants (Post-Earthquake & Disaster Recovery)" },
            { "id": 16, "text": "Public-Private Partnerships (PPP)" }
        ];

        $scope.InterventionTypeColl = [
            { id: 1, text: 'New Construction' },
            { id: 2, text: 'Retrofitting' },
            { id: 3, text: 'Rehabilitation ' },
            { id: 4, text: 'Reconstruction' },
            { id: 5, text: 'Retro' },
            { id: 6, text: 'Existing' }
        ];


        $scope.CompletionStatusColl = [
            { id: 0, text: 'select Status' },
        ];

        //Company Details
        $scope.newCompanyDet = {};
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetCompanyDet",
            dataType: "json",
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

        $scope.BuildingTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Infrastructure/Creation/GetAllBuildingType",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BuildingTypeList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.currentPages = {
            BuildingDetails: 1,
            FloorMapping: 1

        };

        $scope.searchData = {
            BuildingDetails: '',
            FloorMapping: ''

        };

        $scope.perPage = {
            BuildingDetails: GlobalServices.getPerPageRow(),
            FloorMapping: GlobalServices.getPerPageRow()

        };

        //BuildingDetails
        $scope.newBuildingDet = {
            BuildingId: null,
            BuildingNo: '',
            Name: '',
            Location: '',
            BuildingTypeId: null,
            OtherBuildingType: '',
            NoOfFloor: null,
            OverallCondition: '',
            NoOfClassRooms: null,
            NoOfOtherRooms: null,
            ConstructionDate_TMP: '',
            StructureType: '',
            OtherStructureType: '',
            RoofType: '',
            OtherRoofType: '',
            DamageGrade: '',
            InfrastructureType: '',
            FundingSources: '',
            InterventionType: '',
            IsApprovedDesign: 0,
            IsCompletionCertificate: 0,
            CompletionStatus: '',
            CompletionDate_TMP: '',
            Remarks: '',
            Budget: 0,
            BoysToiletNo: null,
            GirlsToiletNo: null,
            IsToiletFunctional: 0,
            FacilityNotFunctioning: '',
            BuildingFacilitiesColl: [],
            Mode: 'Save'
        };
        $scope.newBuildingDet.BuildingFacilitiesColl.push({});
      
        //

        //Floor Mapping

        $scope.newFloorMappingDet = {
            TranId: null,
            BuildingId: null,
            TotalFloor: null,
            TotalClassRooms: null,
            TotalOtherRooms: null,
            Mode: 'Save'
        }
        $scope.newFloorMappingDet.BuildingWiseFloorList = [];
        $scope.GetAllFloorList();
        $scope.GetAllFloorMapping();
        $scope.GetAllBuildingDetailsList();
    }

    function OnClickDefault() {



        //Building Details
        document.getElementById('building-form').style.display = "none";
        document.getElementById('preview-form').style.display = "none";

        document.getElementById('add-Bulding').onclick = function () {
            document.getElementById('Buildingtbl').style.display = "none";
            document.getElementById('building-form').style.display = "block";
            $scope.ClearBuildingDetails();
        }

        document.getElementById('back-list-btn').onclick = function () {
            document.getElementById('building-form').style.display = "none";
            document.getElementById('Buildingtbl').style.display = "block";
            $scope.ClearBuildingDetails();
        }

        //document.getElementById('add-Bulding-print').onclick = function () {
        //    document.getElementById('Buildingtbl').style.display = "none";
        //    document.getElementById('preview-form').style.display = "block";
        //    $scope.ClearBuildingDetails();
        //}

        //


        //Floor Mapping
        document.getElementById('floor-mapping-form').style.display = "none";

        document.getElementById('add-Floor').onclick = function () {
            document.getElementById('floor-mapping-tbl').style.display = "none";
            document.getElementById('floor-mapping-form').style.display = "block";
            $scope.ClearFloorMapping();
        }

        document.getElementById('back-floor-list-btn').onclick = function () {
            document.getElementById('floor-mapping-form').style.display = "none";
            document.getElementById('floor-mapping-tbl').style.display = "block";
            $scope.ClearFloorMapping();
        }
        //


    }



    //Building Details
    $scope.ClearBuildingDetails = function () {
        $scope.newBuildingDet = {
            BuildingId: null,
            BuildingNo: '',
            Name: '',
            Location: '',
            BuildingTypeId: null,
            OtherBuildingType: '',
            NoOfFloor: null,
            OverallCondition: '',
            NoOfClassRooms: null,
            NoOfOtherRooms: null,
            ConstructionDate_TMP: '',
            StructureType: '',
            OtherStructureType: '',
            RoofType: '',
            OtherRoofType: '',
            DamageGrade: '',
            InfrastructureType: '',
            FundingSources: '',
            InterventionType: '',
            IsApprovedDesign: 0,
            IsCompletionCertificate: 0,
            CompletionStatus: '',
            CompletionDate_TMP: '',
            Remarks: '',
            Budget: 0,
            BoysToiletNo: null,
            GirlsToiletNo: null,
            IsToiletFunctional: 0,
            FacilityNotFunctioning: '',
            BuildingFacilitiesColl: [],
            Mode: 'Save'
        };
        $scope.newBuildingDet.BuildingFacilitiesColl.push({});

        $scope.ClearBuildingPhoto();
        setTimeout(function () {
            $('.select2').val(null).trigger('change');
        }, 100);
    }


    $scope.AddRoomDetails = function (ind) {
        if ($scope.newRoom.RoomDetailsColl) {
            if ($scope.newRoom.RoomDetailsColl.length > ind + 1) {
                $scope.newRoom.RoomDetailsColl.splice(ind + 1, 0, {
                    RoomName: ''
                })
            } else {
                $scope.newRoom.RoomDetailsColl.push({
                    RoomName: ''
                })
            }
        }
    };
    $scope.delRoomDetails = function (ind) {
        if ($scope.newRoom.RoomDetailsColl) {
            if ($scope.newRoom.RoomDetailsColl.length > 1) {
                $scope.newRoom.RoomDetailsColl.splice(ind, 1);
            }
        }
    };

    $scope.ChangeDate = function (field, dateStyle) {
        $timeout(function () {

            if (field == 'ConstructionDate') {
                if (dateStyle == 1) //AD
                {
                    if ($scope.newBuildingDet.ConstructionDateADDet) {
                        $scope.newBuildingDet.ConstructionDate_TMP = new Date($scope.newBuildingDet.ConstructionDateADDet.dateAD);
                    }
                }
                else if (dateStyle == 2) //BS
                {
                    if ($scope.newBuildingDet.ConstructedDateDet) {
                        $scope.newBuildingDet.ConstructionDateAD_TMP = new Date($scope.newBuildingDet.ConstructedDateDet.dateAD);
                    }
                }
            }


        });
    }

    $scope.updateDate = function () {
        if ($scope.newBuildingDet.ConstructedDateDet) {
            var englishDate = $filter('date')(new Date($scope.newBuildingDet.ConstructedDateDet.dateAD), 'yyyy-MM-dd');

            $scope.newBuildingDet.ConstructionDate = englishDate;

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }
    };
    $scope.updateDate_BS = function () {
        if ($scope.newBuildingDet.ConstructionDate) {
            $scope.$applyAsync(function () {
                $scope.newBuildingDet.ConstructionDate_TMP = new Date($scope.newBuildingDet.ConstructionDate);
            });
        }
    };

    //************************* BuildingDetails *********************************

    $scope.IsValidBuildingDetails = function () {
        return true;
    }

    $scope.SaveUpdateBuildingDetails = function () {
        if ($scope.IsValidBuildingDetails() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newBuildingDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateBuildingDetails();
                    }
                });
            } else
                $scope.CallSaveUpdateBuildingDetails();

        }
    };

    $scope.CallSaveUpdateBuildingDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();


        if ($scope.newBuildingDet.ConstructedDateDet)
            $scope.newBuildingDet.ConstructionDate = $filter('date')(new Date($scope.newBuildingDet.ConstructedDateDet.dateAD), 'yyyy-MM-dd');

        if ($scope.newBuildingDet.IsCompletionCertificate == true) {
            if ($scope.newBuildingDet.CompletionDateDet) {
                $scope.newBuildingDet.CompletionDate = $filter('date')(new Date($scope.newBuildingDet.CompletionDateDet.dateAD), 'yyyy-MM-dd');
            }
            $scope.newBuildingDet.CompletionStatus = '';
        } else {
            $scope.newBuildingDet.CompletionDate = '';
        }

        if ($scope.newBuildingDet.IsCompletionCertificate == true)
            $scope.newBuildingDet.FacilityNotFunctioning = '';

        var BPhoto = $scope.newBuildingDet.BuildingImage_TMP;

        if ($scope.newBuildingDet.BuildingTypeColl)
            $scope.newBuildingDet.OtherBuildingType = $scope.newBuildingDet.BuildingTypeColl.toString();
        else
            $scope.newBuildingDet.OtherBuildingType = '';


        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/SaveBuilding",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.buildingPhoto && data.buildingPhoto.length > 0)
                    formData.append("photo", data.buildingPhoto[0]);

                return formData;
            },
            data: { jsonData: $scope.newBuildingDet, buildingPhoto: BPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearBuildingDetails();
                $scope.GetAllBuildingDetailsList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    }


    $scope.GetAllBuildingDetailsList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.BuildingDetailsList = [];

        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetAllBuildingList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BuildingDetailsList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }

    //$scope.GetAllBuildingDetailsList = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();
    //    $scope.BuildingDetailsList = [];

    //    $http({
    //        method: 'POST',
    //        url: base_url + "Hostel/Creation/GetAllBuildingList",
    //        dataType: "json"
    //    }).then(function (res) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";

    //        if (res.data.IsSuccess && res.data.Data) {
    //            $scope.BuildingDetailsList = res.data.Data;
    //            setTimeout(function () {
    //            angular.forEach($scope.BuildingDetailsList, function (cl) {
    //                if (cl.OtherBuildingType) {
    //                    var buildingTypeIds = [...new Set(cl.OtherBuildingType.split(',').map(Number))]; // Remove duplicates
    //                    cl.BuildingTypeColl = [...new Set(buildingTypeIds.map(function (id) {
    //                        var buildingType = $scope.BuildingTypeList.find(type => type.BuildingTypeId === id);
    //                        return buildingType ? buildingType.Name : '';
    //                    }))]; // Remove duplicates from names too
    //                }
    //            });

    //            }, 100);


    //        } else {
    //            Swal.fire(res.data.ResponseMSG);
    //        }
    //    }, function (reason) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //        Swal.fire('Failed: ' + reason);
    //    });
    //};



    $scope.GetBuildingDetailsById = function (refData) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            BuildingId: refData.BuildingId
        };

        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetBuildingById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newBuildingDet = res.data.Data;
                $scope.newBuildingDet.Mode = 'Modify';

                document.getElementById('Buildingtbl').style.display = "none";
                document.getElementById('building-form').style.display = "block";

                if ($scope.newBuildingDet.IsApprovedDesign == true)
                    $scope.newBuildingDet.IsApprovedDesign = 1;
                else
                    $scope.newBuildingDet.IsApprovedDesign = 0;

                if ($scope.newBuildingDet.IsCompletionCertificate == true)
                    $scope.newBuildingDet.IsCompletionCertificate = 1;
                else
                    $scope.newBuildingDet.IsCompletionCertificate = 0;

                if ($scope.newBuildingDet.IsToiletFunctional == true)
                    $scope.newBuildingDet.IsToiletFunctional = 1;
                else
                    $scope.newBuildingDet.IsToiletFunctional = 0;

                if ($scope.newBuildingDet.ownershipOfBuilding == true)
                    $scope.newBuildingDet.ownershipOfBuilding = 1;
                else
                    $scope.newBuildingDet.ownershipOfBuilding = 0;

                if ($scope.newBuildingDet.hasInternetConnection == true)
                    $scope.newBuildingDet.hasInternetConnection = 1;
                else
                    $scope.newBuildingDet.hasInternetConnection = 0;


                if ($scope.newBuildingDet.CompletionDate)
                    $scope.newBuildingDet.CompletionDate_TMP = new Date($scope.newBuildingDet.CompletionDate);

                if ($scope.newBuildingDet.ConstructionDate)
                    $scope.newBuildingDet.ConstructionDate_TMP = new Date($scope.newBuildingDet.ConstructionDate);


                if (!$scope.newBuildingDet.BuildingFacilitiesColl || $scope.newBuildingDet.BuildingFacilitiesColl.length == 0) {
                    $scope.newBuildingDet.BuildingFacilitiesColl = [];
                    $scope.newBuildingDet.BuildingFacilitiesColl.push({});

                }

                if ($scope.newBuildingDet.OtherBuildingType) {
                    $scope.newBuildingDet.BuildingTypeColl = $scope.newBuildingDet.OtherBuildingType.split(',').map(Number);

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
    };

    $scope.DelBuildingDetailsById = function (refData) {

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
                    BuildingId: refData.BuildingId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Hostel/Creation/DelBuilding",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllBuildingDetailsList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


    $scope.PrintData = function (refData) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            BuildingId: refData.BuildingId
        };

        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetBuildingById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.Print = res.data.Data;

                if ($scope.Print.OtherBuildingType) {
                    var buildingTypeIds = [...new Set($scope.Print.OtherBuildingType.split(',').map(Number))]; // Convert to numbers & remove duplicates

                    $scope.Print.BuildingTypeColl = buildingTypeIds.map(function (id) {
                        var buildingType = $scope.BuildingTypeList.find(type => type.BuildingTypeId === id);
                        return buildingType ? buildingType.Name : '';
                    }).filter(name => name); // Remove empty values
                } else {
                    $scope.Print.BuildingTypeColl = []; // Ensure it exists
                }


                $scope.timestamp = new Date().getTime();

                $('#printcard').printThis();

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.AddFacilitiesDetails = function (ind) {
        if ($scope.newBuildingDet.BuildingFacilitiesColl) {
            if ($scope.newBuildingDet.BuildingFacilitiesColl.length > ind + 1) {
                $scope.newBuildingDet.BuildingFacilitiesColl.splice(ind + 1, 0, {
                    Facilities: ''
                })
            } else {
                $scope.newBuildingDet.BuildingFacilitiesColl.push({
                    Facilities: ''
                })
            }
        }
    };
    $scope.delFacilitiesDetails = function (ind) {
        if ($scope.newBuildingDet.BuildingFacilitiesColl) {
            if ($scope.newBuildingDet.BuildingFacilitiesColl.length > 1) {
                $scope.newBuildingDet.BuildingFacilitiesColl.splice(ind, 1);
            }
        }
    };

    $scope.ClearBuildingPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newBuildingDet.PhotoData = null;
                $scope.newBuildingDet.Photo_TMP = [];
            });
        });
        $('#imgAcadImage1').attr('src', '');
    };
    //




    //Floor Mapping
    $scope.ChangeBuilding = function () {
        angular.forEach($scope.BuildingDetailsList, function (building) {
            if (building.BuildingId == $scope.newFloorMappingDet.BuildingId) {
                $scope.newFloorMappingDet.TotalFloor = building.NoOfFloor;
                $scope.newFloorMappingDet.TotalClassRooms = building.NoOfClassRooms;
                $scope.newFloorMappingDet.TotalOtherRooms = building.NoOfOtherRooms;
            }
        });

        $scope.GetFloorMappingById($scope.newFloorMappingDet.BuildingId).then(function () {
            $scope.newFloorMappingDet.BuildingWiseFloorList = [];

            if ($scope.newFloorMappingDetColl && $scope.newFloorMappingDetColl.length > 0) {
                angular.forEach($scope.newFloorMappingDetColl, function (mn) {
                    $scope.newFloorMappingDet.BuildingWiseFloorList.push({
                        FloorId: mn.FloorId,
                        NoOfClassRooms: mn.NoOfClassRooms,
                        NoOfOtherRooms: mn.NoOfOtherRooms,
                        SafetyMeasures: mn.SafetyMeasures,
                        FloorType: mn.FloorType || [], // Ensure FloorType is always an array
                        TranId: mn.TranId
                    });
                });

                for (let i = 0; i < ($scope.newFloorMappingDet.TotalFloor - $scope.newFloorMappingDetColl.length); i++) {
                    $scope.newFloorMappingDet.BuildingWiseFloorList.push({
                        FloorId: null,
                        NoOfClassRooms: '',
                        NoOfOtherRooms: '',
                        SafetyMeasures: '',
                        FloorType: [], // Ensuring FloorType is an array
                        TranId: null
                    });
                }
            } else {
                for (let i = 0; i < ($scope.newFloorMappingDet.TotalFloor || 0); i++) {
                    $scope.newFloorMappingDet.BuildingWiseFloorList.push({
                        FloorId: null,
                        NoOfClassRooms: '',
                        NoOfOtherRooms: '',
                        SafetyMeasures: '',
                        FloorType: [], // Ensuring FloorType is an array
                        TranId: null
                    });
                }
                $scope.DistributeEvenly();
            }
        });
    };



    $scope.GetAllFloorList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
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
    }

    $scope.ClearFloorMapping = function () {
        $scope.newFloorMappingDet = {
            TranId: null,
            BuildingId: null,
            TotalFloor: null,
            TotalClassRooms: null,
            TotalOtherRooms: null,
            Mode: 'Save'
        }
        $scope.newFloorMappingDet.BuildingWiseFloorList = [];
    }

    $scope.IsValidFloorMapping = function () {
        if ($scope.ValidateRooomCount() == true)
            return true;
    }

    $scope.SaveUpdateFloorMapping = function () {
        if ($scope.IsValidFloorMapping() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newFloorMappingDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateFloorMapping();
                    }
                });
            } else
                $scope.CallSaveUpdateFloorMapping();

        }
    };


    $scope.CallSaveUpdateFloorMapping = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var buildingId = $scope.newFloorMappingDet.BuildingId;
        var dataToSave = [];
        for (var i = 0; i < $scope.newFloorMappingDet.BuildingWiseFloorList.length; i++) {
            var S = $scope.newFloorMappingDet.BuildingWiseFloorList[i];
            var FloorId = S.FloorId;
            var NoOfClassRooms = S.NoOfClassRooms;
            var NoOfOtherRooms = S.NoOfOtherRooms;
            var SafetyMeasures = S.SafetyMeasures;
            var TranId = S.TranId || '';

            var dataItem = {
                BuildingId: buildingId,
                FloorId: FloorId,
                NoOfClassRooms: NoOfClassRooms,
                NoOfOtherRooms: NoOfOtherRooms,
                SafetyMeasures: SafetyMeasures,
                FloorType: S.FloorType && Array.isArray(S.FloorType) ? S.FloorType.join(",") : "",
                TranId: TranId,
            };
            dataToSave.push(dataItem);
        }

        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/SaveFloorMappingColl",
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
                $scope.ClearFloorMapping();
                $scope.GetAllFloorMapping();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }


    $scope.GetAllFloorMapping = function (BuildingId) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.FloorMappingList = [];

        var para = {
            BuildingId: BuildingId
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

                //angular.forEach($scope.FloorMappingList, function (cl) {
                //    if (cl.FloorType) {
                //        var floorTypeIds = [...new Set(cl.FloorType.split(',').map(Number))]; // Remove duplicates
                //        cl.FloorTypeNames = [...new Set(floorTypeIds.map(function (id) {
                //            var floorType = $scope.BuildingTypeList.find(type => type.BuildingTypeId === id);
                //            return floorType ? floorType.Name : '';
                //        }))]; // Remove duplicate names
                //    }
                //});


                var groupedData = mx(res.data.Data)
                    .groupBy(fm => ({ BuildingId: fm.BuildingId, BuildingName: fm.BuildingName }))
                    .toArray();

                var tmpColl = [];

                angular.forEach(groupedData, function (group) {
                    var firstElement = group.elements[0];

                    var newBuildingData = {
                        BuildingId: firstElement.BuildingId,
                        BuildingName: firstElement.BuildingName,
                        Floors: []
                    };

                    angular.forEach(group.elements, function (floor) {
                        newBuildingData.Floors.push(floor);
                    });

                    tmpColl.push(newBuildingData);
                });

                $scope.GroupedFloorMappings = tmpColl;


            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }
    $scope.GetFloorMappingById = function (BuildingId) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            BuildingId: BuildingId
        };

        return $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/GetFloorMappingById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {

                $scope.newFloorMappingDetColl = res.data.Data;

                angular.forEach($scope.newFloorMappingDetColl, function (item) {
                    if (item.FloorType) {
                        item.FloorType = item.FloorType.split(',').map(Number);
                    }
                });
                $scope.newFloorMappingDet.Mode = 'Modify';

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

            return res.data;

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelFloorMappingById = async function (BuildingId) {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            BuildingId: BuildingId
        };

        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/DelFloorMapping",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.GetAllFloorMapping();
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };


    $scope.ValidateRooomCount = function () {
        var totalClassRooms = 0;
        var totalOtherRooms = 0;

        for (var i = 0; i < $scope.newFloorMappingDet.BuildingWiseFloorList.length; i++) {
            var building = $scope.newFloorMappingDet.BuildingWiseFloorList[i];
            var classRoomsInput = building.NoOfClassRooms?.toString().trim();
            var otherRoomsInput = building.NoOfOtherRooms?.toString().trim();

            if (!/^\d+$/.test(classRoomsInput) && classRoomsInput !== "") {
                Swal.fire("Please enter a valid number for classrooms.");
                return false;
            }

            if (otherRoomsInput !== "" && !/^\d+$/.test(otherRoomsInput)) {
                Swal.fire("Please enter a valid number for other rooms.");
                return false;
            }

            building.NoOfClassRooms = parseInt(classRoomsInput) || 0;
            building.NoOfOtherRooms = parseInt(otherRoomsInput) || 0;

            totalClassRooms += building.NoOfClassRooms;
            totalOtherRooms += building.NoOfOtherRooms;
        }

        if ($scope.newFloorMappingDet.TotalClassRooms < totalClassRooms) {
            Swal.fire("Total no. of classrooms in each floor exceeds the total no. of classrooms.");
            return false;
        }

        if ($scope.newFloorMappingDet.TotalOtherRooms < totalOtherRooms) {
            Swal.fire("Total no. of other rooms in each floor exceeds the total no. of other rooms.");
            return false;
        }

        //if ($scope.newFloorMappingDet.NoOfClassRooms != totalClassRooms) {
        //    Swal.fire("Total no. of classrooms isn't equal to total no. of classrooms in each floor.");
        //    return false;
        //}

        //if ($scope.newFloorMappingDet.NoOfOtherRooms != totalOtherRooms) {
        //    Swal.fire("Total no. of other rooms isn't equal to total no. of other rooms in each floor.");
        //    return false;
        //}


        return true;
    };

    $scope.DistributeEvenly = function () {

        var classRoomsBaseValue = Math.floor($scope.newFloorMappingDet.TotalClassRooms / $scope.newFloorMappingDet.TotalFloor);
        var classRoomsRemValue = $scope.newFloorMappingDet.TotalClassRooms % $scope.newFloorMappingDet.TotalFloor;

        var otherRoomsBaseValue = Math.floor($scope.newFloorMappingDet.TotalOtherRooms / $scope.newFloorMappingDet.TotalFloor);
        var otherRoomsRemValue = $scope.newFloorMappingDet.TotalOtherRooms % $scope.newFloorMappingDet.TotalFloor;


        angular.forEach($scope.newFloorMappingDet.BuildingWiseFloorList, function (building) {
            building.NoOfClassRooms = classRoomsBaseValue;
            building.NoOfOtherRooms = otherRoomsBaseValue;

            if (classRoomsRemValue > 0) {
                building.NoOfClassRooms += 1;
                classRoomsRemValue--;
            }
            if (otherRoomsRemValue > 0) {
                building.NoOfOtherRooms += 1;
                otherRoomsRemValue--;
            }

        })
    }


    $scope.isFloorDisabled = function (floorId, currentFloorId) {
        return floorId !== currentFloorId &&
            $scope.newFloorMappingDet.BuildingWiseFloorList.some(cl => cl.FloorId === floorId);
    };



    $scope.pageChangeHandler = function (num) {
        console.log('page changed to ' + num);
    };

});