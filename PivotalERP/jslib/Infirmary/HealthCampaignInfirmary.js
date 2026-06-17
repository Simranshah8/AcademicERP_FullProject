
app.controller('HCInfirmaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'HCInfirmary';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
       
        $scope.currentPages = {
            HCInfirmary: 1,
        };

        $scope.searchData = {
            HCInfirmary: '',
        };

        $scope.perPage = {
            HCInfirmary: GlobalServices.getPerPageRow(),
        };


        $scope.TestNameList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllTestName",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TestNameList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.HealthCampaignList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllHealthOperation",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.HealthCampaignList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        


        $scope.newHealthCInfirmary = {
            HealthCamInfirmaryId: null,
            HealthCampaignId: null,
            Date: null,
            ClassTypeId: null,
            ExaminerTypeId: null,
            IsVaccination: 0,
            Remarks: '',
            TestNameId: null,
            StudentId: null,
            Mode: 'Save'
        };
        $scope.GetAllHCInfirmaryList();

        /*     $scope.GetAllHealthCamInfirmaryList();*/
    };


    $scope.ClearHCInfirmary = function () {
        $scope.newHealthCInfirmary = {
            HealthCamInfirmaryId: null,
            HealthCampaignId: null,
            Date: null,
            ClassTypeId: null,
            ExaminerTypeId: null,
            IsVaccination: 0,
            Remarks: '',
            TestNameId: null,
            StudentId: null,
            Mode: 'Save'
        };

    };

    $scope.CheckUnCheckAll = function (payH) {
        if ($scope.StudentForTestValue) {
            $scope.StudentForTestValue.forEach(function (dc) {
                if (dc.IsVaccinationColl) {
                    dc.IsVaccinationColl.forEach(function (ph) {
                        if (ph.TestNameId === payH.TestNameId) {
                            ph.IsAllow = payH.IsAllow;
                        }
                    });
                }

            });
        }
    };



    function OnClickDefault() {
        document.getElementById('formname').style.display = "none";
        document.getElementById('detailform').style.display = "none";

        //HCInfirmary section
        document.getElementById('add-hci-btn').onclick = function () {
            document.getElementById('Nametable').style.display = "none";
            document.getElementById('formname').style.display = "block";
            $scope.ClearHCInfirmary();
        }

        document.getElementById('back-hci-list').onclick = function () {
            document.getElementById('formname').style.display = "none";
            document.getElementById('Nametable').style.display = "block";
            //$scope.ClearHCInfirmary();
        }

        //document.getElementById('detailopen').onclick = function () {
        //    document.getElementById('Nametable').style.display = "none";
        //    document.getElementById('detailform').style.display = "block";
        //    //$scope.ClearHCInfirmary();
        //}

        document.getElementById('backtolisttable').onclick = function () {
            document.getElementById('detailform').style.display = "none";
            document.getElementById('Nametable').style.display = "block";
            //$scope.ClearHCInfirmary();
        }
    };


    //************************* HealthCamInfirmary *********************************   


    //Added By Suresh Starts
   


    $scope.SaveStudentHCInfirmry = function () {
        /*if ($scope.IsValidIncentive() == true) {*/
        if ($scope.confirmMSG.Accept == true) {
            var saveModify = $scope.newHealthCInfirmary.Mode;
            Swal.fire({
                title: 'Do you want to ' + saveModify + ' the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.CallSaveUpdateHCInfirmary();
                }
            });
        } else
            $scope.CallSaveUpdateHCInfirmary();
        /*}*/
    };

    $scope.CallSaveUpdateHCInfirmary = function () {
        if (!$scope.newHealthCInfirmary.HealthCampaignId) {
            Swal.fire('Please select a Health Campaign.');
            return; 
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        if ($scope.newHealthCInfirmary.CampaignDateDet) {
            $scope.newHealthCInfirmary.CampaignDate = $filter('date')(new Date($scope.newHealthCInfirmary.CampaignDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newHealthCInfirmary.CampaignDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        $scope.newHealthCInfirmary.StudentForTestValueColl = [];
        $scope.newHealthCInfirmary.StudentForHCVaccinationColl = [];
        /* var dtColl = [];*/
        $scope.StudentForTestValue.forEach(function (st) {
            st.TestNameColl.forEach(function (ph) {
                if (ph.Value !== null) {
                    $scope.newHealthCInfirmary.StudentForTestValueColl.push({
                        StudentId: st.StudentId,
                        TestNameId: ph.TestNameId,
                        Value: ph.Value
                    });
                }
            })
        });

        $scope.StudentForTestValue.forEach(function (v) {
            v.IsVaccinationColl.forEach(function (ph) {
                if (ph.IsAllow == true) {
                    $scope.newHealthCInfirmary.StudentForHCVaccinationColl.push({
                        StudentId: v.StudentId,
                        TestNameId: ph.TestNameId,
                        Remarks: v.Remarks,
                        IsAllow: ph.IsAllow
                    });
                }
            })
        });

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveHealthCampaignInfirmary",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newHealthCInfirmary }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearHCInfirmary();
                $scope.GetAllHCInfirmaryList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllHCInfirmaryList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.HCInfirmaryList = [];

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetAllHCInfirmary",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
               /* $scope.HCInfirmaryList = res.data.Data;*/
                $scope.HCInfirmaryList = [];

                var query = mx(res.data.Data).groupBy(t => t.HealthCampaignName);
                var sno = 1;
                angular.forEach(query, function (q) {
                    var pare = {
                        SNo: sno,
                        HealthCampaignName: q.key,
                        ChieldColl: []
                    };

                    angular.forEach(q.elements, function (el) {
                        pare.ChieldColl.push(el);
                    })
                    $scope.HCInfirmaryList.push(pare);
                    sno++;
                });


            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }


    $scope.GetHCInfirmaryById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            HealthCamInfirmaryId: refData.HealthCamInfirmaryId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getHCInfirmaryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newHealthCInfirmary = res.data.Data;

                if ($scope.newHealthCInfirmary.CampaignDate)
                    $scope.newHealthCInfirmary.CampaignDate_TMP = new Date($scope.newHealthCInfirmary.CampaignDate);


                $scope.GetHealthCampaignDatabyId();

                $scope.GetStudentForHCInfirmary();
                $scope.newHealthCInfirmary.Mode = 'Modify';

                document.getElementById('Nametable').style.display = "none";
                document.getElementById('formname').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetHealthCampaignDatabyId = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            HealthCampaignId: $scope.newHealthCInfirmary.HealthCampaignId
        };
        $scope.ExaminerList = [];
        $scope.ClassList = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getDataForHealthCampaignById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                //$scope.newGeneralChkup = res.data.Data;

                $scope.ExaminerList = res.data.Data.HealthCampaignExaminerColl;
                $scope.ClassList = res.data.Data.HealthCampaignClassColl;
                $scope.newHealthCInfirmary.IsVaccination = res.data.Data.Vaccination;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetStudentForHCInfirmary = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.StudentForTestValue = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllStudentForHCInfirmary?ClassId=" + $scope.newHealthCInfirmary.ClassId,
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                var dataColl = mx(res.data.Data);
                var query = dataColl.groupBy(t => ({ StudentId: t.StudentId }));

                angular.forEach(query, function (q) {
                    var fst = q.elements[0];
                    var subQry = mx(q.elements);
                    var beData = {
                        StudentId: fst.StudentId,
                        RegNo: fst.RegNo,
                        StudentName: fst.StudentName,
                        RollNo: fst.RollNo,
                        TestNameColl: [],
                        IsVaccinationColl: []
                    };
                    $scope.TestNameList.forEach(function (pa) {
                        var find = subQry.firstOrDefault(p1 => p1.TestNameId == pa.TestNameId);
                        beData.TestNameColl.push({
                            TestNameId: pa.TestNameId,
                            Value: find ? find.Value : 0,
                        });
                    });


                    $scope.TestNameList.forEach(function (pa) {
                        var find = subQry.firstOrDefault(p1 => p1.TestNameId == pa.TestNameId);
                        beData.IsVaccinationColl.push({
                            TestNameId: pa.TestNameId,
                            IsAllow: find ? find.IsAllow : false,
                        });
                    });


                    $scope.StudentForTestValue.push(beData);

                });

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }

    $scope.DelHCInfirmaryById = function (refData) {
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
                    HealthCamInfirmaryId: refData.HealthCamInfirmaryId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteHCInfirmary",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllHCInfirmaryList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };

    //Detail starts
    $scope.GetHCInfirmaryDetailById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            HealthCamInfirmaryId: refData.HealthCamInfirmaryId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getHCInfirmaryForDetailById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;

               

                document.getElementById('Nametable').style.display = "none";
                document.getElementById('detailform').style.display = "block";

                $scope.newHealthCInfirmary.Mode = 'Modify';
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };
    $scope.validateForDate = function (obj, key, keyTMP) {
        if (!obj[key] || !obj[key].dateAD) {
            return true;
        }

        var forDate = $filter('date')(obj[key].dateAD, 'yyyy-MM-dd');
        var today = $filter('date')(new Date(), 'yyyy-MM-dd');

        if (!forDate) return true;

        if (forDate > today) {
            Swal.fire({
                icon: 'warning',
                text: 'Date cannot be a future date.',
                confirmButtonText: 'OK'
            }).then(function () {
                $scope.$apply(function () {
                    obj[keyTMP] = new Date();
                    obj[key] = new Date();
                });
            });
            return false;
        }
        $scope.getVoucherNoOnly();
        return true;
    }
});

