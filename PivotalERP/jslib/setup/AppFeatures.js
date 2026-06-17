app.controller('AppFeaturesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'App Features';
    // New Updates
    $scope.LoadData = function () {
        $scope.ClassList = [];
        GlobalServices.getClassList().then(function (res) {
            $scope.ClassList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });

        $scope.searchData = {
            StudentWise: '',
            EmployeeWise: '',
            AdminWise: ''
        };

        $scope.UserList = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Setup/GetEmployeeUserList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.UserList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.AdminUserList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllUserList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AdminUserList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.checkAll = {
            Student: false,
            Teacher: false,
            Admin: false
        };
        $scope.RoleId = null;
        $scope.ClassId = null;
        $scope.SectionId = null;
        $scope.ForUserId = 0;
        $scope.newEmp = { RoleId: 0, ForUserId: 0 };
        $scope.newStudent = { ClassId: null };
        $scope.newAllowEntity = { Full: false };
        $scope.GetAllAppFeatures();
        $scope.GetAllRolesForApp();
        $scope.GetAllAppFeaturesStudent();
        $scope.GetAllAppEmpFeatures();
    };

    //----------------------------For Student--------------------------  
    $scope.GetAllAppFeaturesStudent = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AppFeaturesListS = [];
        var para = {
            ForUserId: null
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAppFeatures",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                // Filter for ForUser === 1
                var allFeatures = res.data.Data.filter(function (x) {
                    return x.ForUser === 1;
                });

                // Group features by EntityId
                var groupedFeatures = {};
                angular.forEach(allFeatures, function (af) {
                    if (!groupedFeatures[af.EntityId]) {
                        groupedFeatures[af.EntityId] = {
                            ForUser: af.ForUser,
                            ModuleName: af.ModuleName,
                            EntityId: af.EntityId,
                            Name: af.Name,
                            Full: af.ClassId === null && af.IsActive, // Set Full if ClassId is null and IsActive
                            ClassPermissions: {}
                        };
                    }
                    // Set IsAllow for the matching ClassId if IsActive is true
                    if (af.IsActive && af.ClassId) {
                        groupedFeatures[af.EntityId].ClassPermissions[af.ClassId] = {
                            ClassId: af.ClassId,
                            IsAllow: true
                        };
                    }
                });
                // Convert grouped features to array and initialize all classes
                $scope.AppFeaturesListS = Object.values(groupedFeatures);
                angular.forEach($scope.AppFeaturesListS, function (af) {
                    // Ensure ClassPermissions includes all classes from ClassList
                    angular.forEach($scope.ClassList, function (cl) {
                        if (!af.ClassPermissions[cl.id]) {
                            af.ClassPermissions[cl.id] = {
                                ClassId: cl.id,
                                IsAllow: false // Default to false if class is not active
                            };
                        }
                    });
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };

    $scope.SaveUpdateStudentAppFeatures = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        // Create a flat array of features for saving
        var featuresToSave = [];
        angular.forEach($scope.AppFeaturesListS, function (af) {
            // If Full is checked, send one record with ClassId: null
            if (af.Full) {
                var feature = {
                    ForUserId: null,
                    ForUser: 1, // Student
                    ModuleName: af.ModuleName,
                    EntityId: af.EntityId,
                    Name: af.Name,
                    IsActive: true, // Full means the feature is active
                    RoleId: null,
                    ClassId: null // Explicitly set ClassId to null
                };
                featuresToSave.push(feature);
            } else {
                // Otherwise, send records for each class based on ClassPermissions
                angular.forEach(af.ClassPermissions, function (perm, classId) {
                    var feature = {
                        ForUserId: null,
                        ForUser: 1, // Student
                        ModuleName: af.ModuleName,
                        EntityId: af.EntityId,
                        Name: af.Name,
                        IsActive: perm.IsAllow, // Use the checkbox state (true or false)
                        RoleId: null,
                        ClassId: parseInt(classId)
                    };
                    featuresToSave.push(feature);
                });
            }
        });

        // Log payload for debugging
        console.log('Saving features:', featuresToSave);
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/SaveAppFeatures",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: featuresToSave }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + errormessage);
        });
    };


    $scope.CheckUnCheckAllClass = function (cl) {
        var chkVal = cl.IsAllow;
        angular.forEach($scope.AppFeaturesListS, function (af) {
            af.ClassPermissions[cl.id].IsAllow = chkVal;
        });
    };

    //----------------------------For Teacher--------------------------
    $scope.GetAllRolesForApp = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.RolesList = [];
        var para = {
            ForUserId: 2
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllRoles",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RolesList = res.data.Data;
                // Initialize IsAllow for header checkboxes
                angular.forEach($scope.RolesList, function (role) {
                    role.IsAllow = false;
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };


    $scope.GetAllAppEmpFeatures = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AppFeaturesListEmp = [];

        var para = {
            ForUserId: $scope.newEmp.ForUserId || null
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAppFeatures",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                // Filter for ForUser === 2
                var allFeatures = res.data.Data.filter(function (x) {
                    return x.ForUser === 2;
                });

                // Group features by EntityId
                var groupedFeatures = {};
                angular.forEach(allFeatures, function (af) {
                    if (!groupedFeatures[af.EntityId]) {
                        groupedFeatures[af.EntityId] = {
                            ForUser: af.ForUser,
                            ModuleName: af.ModuleName,
                            MOrderNo: af.MOrderNo,
                            EOrderNo: af.EOrderNo,
                            EntityId: af.EntityId,
                            Name: af.Name,
                            RolePermissions: {}
                        };
                    }
                    // Set IsAllow for the matching RoleId if IsActive is true
                    if (af.IsActive) {
                        groupedFeatures[af.EntityId].RolePermissions[af.RoleId] = {
                            RoleId: af.RoleId,
                            IsAllow: true
                        };
                    }
                });

                // Convert grouped features to array and sort by MOrderNo, then EOrderNo
                $scope.AppFeaturesListEmp = Object.values(groupedFeatures).sort(function (a, b) {
                    if (a.MOrderNo === b.MOrderNo) {
                        return a.EOrderNo - b.EOrderNo; // Sort by EOrderNo if MOrderNo is equal
                    }
                    return a.MOrderNo - b.MOrderNo; // Primary sort by MOrderNo
                });

                // Initialize all roles
                angular.forEach($scope.AppFeaturesListEmp, function (af) {
                    // Ensure RolePermissions includes all roles from RolesList
                    angular.forEach($scope.RolesList, function (role) {
                        if (!af.RolePermissions[role.RoleId]) {
                            af.RolePermissions[role.RoleId] = {
                                RoleId: role.RoleId,
                                IsAllow: false // Default to false if role is not active
                            };
                        }
                    });
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };

    //$scope.GetAllAppEmpFeatures = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();
    //    $scope.AppFeaturesListEmp = [];

    //    var para = {
    //        ForUserId: $scope.newEmp.ForUserId || null
    //    };
    //    $http({
    //        method: 'POST',
    //        url: base_url + "Setup/Security/GetAppFeatures",
    //        dataType: "json",
    //        data: JSON.stringify(para)
    //    }).then(function (res) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //        if (res.data.IsSuccess && res.data.Data) {
    //            // Filter for ForUser === 2
    //            var allFeatures = res.data.Data.filter(function (x) {
    //                return x.ForUser === 2;
    //            });

    //            // Group features by EntityId
    //            var groupedFeatures = {};
    //            angular.forEach(allFeatures, function (af) {
    //                if (!groupedFeatures[af.EntityId]) {
    //                    groupedFeatures[af.EntityId] = {
    //                        ForUser: af.ForUser,
    //                        ModuleName: af.ModuleName,
    //                        MOrderNo: af.MOrderNo,
    //                        EOrderNo: af.EOrderNo,
    //                        EntityId: af.EntityId,
    //                        Name: af.Name,
    //                        RolePermissions: {}
    //                    };
    //                }
    //                // Set IsAllow for the matching RoleId if IsActive is true
    //                if (af.IsActive) {
    //                    groupedFeatures[af.EntityId].RolePermissions[af.RoleId] = {
    //                        RoleId: af.RoleId,
    //                        IsAllow: true
    //                    };
    //                }
    //            });

    //            // Convert grouped features to array and initialize all roles
    //            $scope.AppFeaturesListEmp = Object.values(groupedFeatures);
    //            angular.forEach($scope.AppFeaturesListEmp, function (af) {
    //                // Ensure RolePermissions includes all roles from RolesList
    //                angular.forEach($scope.RolesList, function (role) {
    //                    if (!af.RolePermissions[role.RoleId]) {
    //                        af.RolePermissions[role.RoleId] = {
    //                            RoleId: role.RoleId,
    //                            IsAllow: false // Default to false if role is not active
    //                        };
    //                    }
    //                });
    //            });
    //        } else {
    //            Swal.fire(res.data.ResponseMSG);
    //        }
    //    }, function (reason) {
    //        Swal.fire('Failed: ' + reason);
    //    });
    //};

    $scope.SaveUpdateEmpAppFeatures = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        // Create a flat array of features, including both checked and unchecked RolePermissions
        var featuresToSave = [];
        angular.forEach($scope.AppFeaturesListEmp, function (af) {
            angular.forEach(af.RolePermissions, function (perm, roleId) {
                // Include all RolePermissions, regardless of IsAllow
                var feature = {
                    ForUserId: $scope.newEmp.ForUserId || null,
                    ForUser: 2, // Teacher
                    ModuleName: af.ModuleName,
                    EntityId: af.EntityId,
                    Name: af.Name,
                    MOrderNo: af.MOrderNo,
                    EOrderNo: af.EOrderNo,
                    IsActive: perm.IsAllow, // Set IsActive based on checkbox state
                    RoleId: parseInt(roleId),
                    ClassId: null
                };
                featuresToSave.push(feature);
            });
        });

        // Log payload for debugging
        console.log('Saving teacher features:', featuresToSave);

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/SaveAppFeatures",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: featuresToSave }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + errormessage);
        });
    };

    $scope.CheckUnCheckAllRole = function (role) {
        var chkVal = role.IsAllow;
        if ($scope.AppFeaturesListEmp.length === 0) {
            console.warn('No features available to update.');
            return;
        }
        angular.forEach($scope.AppFeaturesListEmp, function (af) {
            // Ensure RolePermissions exists for this role
            if (!af.RolePermissions[role.RoleId]) {
                af.RolePermissions[role.RoleId] = {
                    RoleId: role.RoleId,
                    IsAllow: chkVal
                };
            } else {
                af.RolePermissions[role.RoleId].IsAllow = chkVal;
            }
        });
    };

    //----------------------------For Admin--------------------------
    //$scope.GetAllAppFeatures = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();
    //    $scope.AppFeaturesList = [];

    //    var para = {
    //        ForUserId: $scope.ForUserId || null,
    //        RoleId: $scope.RoleId || null,
    //        ClassId: $scope.SelectedClass ? $scope.SelectedClass.ClassId : null,
    //        SectionId: $scope.SelectedClass ? $scope.SelectedClass.SectionId : null
    //    };
    //    $http({
    //        method: 'POST',
    //        url: base_url + "Setup/Security/GetAppFeatures",
    //        dataType: "json",
    //        data: JSON.stringify(para)
    //    }).then(function (res) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //        if (res.data.IsSuccess && res.data.Data) {
    //            $scope.AppFeaturesList = res.data.Data;
    //        } else {
    //            Swal.fire(res.data.ResponseMSG);
    //        }
    //    }, function (reason) {
    //        Swal.fire('Failed: ' + reason);
    //    });
    //};

    //$scope.SaveUpdateAppFeatures = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();
    //    angular.forEach($scope.AppFeaturesList, function (af) {
    //        af.RoleId = $scope.RoleId || null;
    //        af.ClassId = ($scope.SelectedClass && $scope.SelectedClass.ClassId) ? $scope.SelectedClass.ClassId : null;
    //        af.SectionId = ($scope.SelectedClass && $scope.SelectedClass.SectionId) ? $scope.SelectedClass.SectionId : null;
    //    });

    //    $http({
    //        method: 'POST',
    //        url: base_url + "Setup/Security/SaveAppFeatures",
    //        headers: { 'Content-Type': undefined },
    //        transformRequest: function (data) {
    //            var formData = new FormData();
    //            formData.append("jsonData", angular.toJson(data.jsonData));
    //            return formData;
    //        },
    //        data: { jsonData: $scope.AppFeaturesList }
    //    }).then(function (res) {
    //        $scope.loadingstatus = "stop";
    //        hidePleaseWait();
    //        Swal.fire(res.data.ResponseMSG);
    //    }, function (errormessage) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //        Swal.fire('Failed: ' + errormessage);
    //    });
    //};


    $scope.GetAllAppFeatures = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AppFeaturesList = [];

        var para = {
            ForUserId: $scope.ForUserId || null
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAppFeatures",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                // Filter for ForUser === 3 (Admin)
                var allFeatures = res.data.Data.filter(function (x) {
                    return x.ForUser === 3;
                });
                // Group features by EntityId
                var groupedFeatures = {};
                angular.forEach(allFeatures, function (af) {
                    if (!groupedFeatures[af.EntityId]) {
                        groupedFeatures[af.EntityId] = {
                            ForUser: af.ForUser,
                            ForUserId: af.ForUserId,
                            ModuleName: af.ModuleName,
                            EntityId: af.EntityId,
                            Name: af.Name,
                            IsActive: af.IsActive
                        };
                    }
                });
                $scope.AppFeaturesList = Object.values(groupedFeatures);
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };

    $scope.SaveUpdateAppFeatures = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var featuresToSave = [];
        angular.forEach($scope.AppFeaturesList, function (af) {
            var feature = {
                ForUserId: $scope.ForUserId || null,
                ForUser: 3, // Admin
                ModuleName: af.ModuleName,
                EntityId: af.EntityId,
                Name: af.Name,
                IsActive: af.IsActive,
                RoleId: null,
                ClassId: null
            };
            featuresToSave.push(feature);
        });

        console.log('Saving admin features:', featuresToSave);
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/SaveAppFeatures",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: featuresToSave }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + errormessage);
        });
    };

    //$scope.CheckAllStudent = function () {
    //    var chkVal = $scope.checkAll.Student;
    //    angular.forEach($scope.AppFeaturesListS, function (af) {
    //        if (af.ForUser == 1)
    //            af.IsActive = chkVal;
    //    });
    //};

    //$scope.CheckAllTeacher = function () {
    //    var chkVal = $scope.checkAll.Teacher;
    //    angular.forEach($scope.AppFeaturesListEmp, function (af) {
    //        if (af.ForUser == 2)
    //            af.IsActive = chkVal;
    //    });
    //};

    $scope.CheckAllAdmin = function () {
        var chkVal = $scope.checkAll.Admin;
        angular.forEach($scope.AppFeaturesList, function (af) {
            af.IsActive = chkVal;

        });
    };

    $scope.CheckAllFull = function () {
        var tmpData = $filter('filter')($scope.AppFeaturesListS, $scope.searchData.StudentWise);

        angular.forEach(tmpData, function (ent) {
            ent.Full = $scope.newAllowEntity.Full; // Update the Full checkbox state
            // Do not modify ClassPermissions, as Full represents ClassId: null

            angular.forEach($scope.ClassList, function (classItem) {
                if (ent.ClassPermissions && ent.ClassPermissions[classItem.id]) {
                    ent.ClassPermissions[classItem.id].IsAllow = $scope.newAllowEntity.Full;
                }
            });
            //
        });
    };

    //-----------------Added on 1st Poush
    $scope.toggleRowCheckboxes = function (item, isFullChecked) {
        if (isFullChecked !== undefined) {
            item.Full = isFullChecked;
            angular.forEach($scope.ClassList, function (classItem) {
                if (item.ClassPermissions[classItem.id]) {
                    item.ClassPermissions[classItem.id].IsAllow = isFullChecked;
                }
            });
        } else {
            var allChecked = true;
            angular.forEach($scope.ClassList, function (classItem) {
                if (item.ClassPermissions[classItem.id]) {
                    if (!item.ClassPermissions[classItem.id].IsAllow) {
                        allChecked = false;
                    }
                }
            });
            item.Full = allChecked;
        }
    };

});