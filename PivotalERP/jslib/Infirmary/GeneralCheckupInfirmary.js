app.controller('GCInfirmaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'GCInfirmary';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.MonthList = GlobalServices.getMonthList();

        $scope.currentPages = {
            GHealthCInfirmary: 1,
        };

        $scope.searchData = {
            GHealthCInfirmary: '',
        };

        $scope.perPage = {
            GHealthCInfirmary: GlobalServices.getPerPageRow(),
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
            url: base_url + "Infirmary/Creation/GetAllGeneralCheckUp",
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

        

 
        $scope.newGeneralChkup = {
            HealthIssueId: null,
            HealthCampaignId:null,
            Name: '',
            Severity: '',
            OrderNo: '',
            Description: '',
            HealthCampaignIsColl: [],
            Mode: 'Save'
        };
        $scope.GetAllGeneralChkupInfirmaryList();
    };

    $scope.ClearGCInfirmary = function () {
        $scope.newGeneralChkup = {
            HealthIssueId: null,
            HealthCampaignId: null,
            Name: '',
            Severity: '',
            OrderNo: '',
            Description: '',
            HealthCampaignIsColl: [],
            Mode: 'Save'
        };
    };



    function OnClickDefault() {
        document.getElementById('formname').style.display = "none";
        document.getElementById('detailform').style.display = "none";
        //GCInfirmary section
        document.getElementById('add-hci-btn').onclick = function () {
        document.getElementById('Nametable').style.display = "none";
        document.getElementById('formname').style.display = "block";
        }

        document.getElementById('back-hci-list').onclick = function () {
        document.getElementById('formname').style.display = "none";
        document.getElementById('Nametable').style.display = "block";
        }

       //document.getElementById('detailopen').onclick = function () {
       //document.getElementById('Nametable').style.display = "none";
       //document.getElementById('detailform').style.display = "block";
       //}

        document.getElementById('backtolisttable').onclick = function () {
        document.getElementById('detailform').style.display = "none";
        document.getElementById('Nametable').style.display = "block";
        }
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

    $scope.GetGeneralCheckUpId = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        if ($scope.newGeneralChkup.HealthCampaignId) {
            var para = {
                GeneralCheckUpId: $scope.newGeneralChkup.HealthCampaignId
            };
            $scope.ExaminerList = [];
            $scope.ClassList = [];
            $http({
                method: 'POST',
                url: base_url + "Infirmary/Creation/getDataForGeneralCheckupById",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    //$scope.newGeneralChkup = res.data.Data;

                    $scope.ExaminerList = res.data.Data.GeneralHealthExaminerColl;
                    $scope.ClassList = res.data.Data.GeneralCheckupClassColl;
                    $scope.newGeneralChkup.IsVaccination = res.data.Data.Vaccination;
                    $scope.newGeneralChkup.MonthId = res.data.Data.Month;
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }
    };


    $scope.GetStudentForGChkupInfirmary = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.StudentForTestValue = [];
        if ($scope.newGeneralChkup.ClassId) {
            var para =
            {
                ClassId: $scope.newGeneralChkup.ClassId
            };
            $http({
                method: 'POST',
                url: base_url + "Infirmary/Creation/GetAllStudentForGChkupInfirmary",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    var dataColl = mx(res.data.Data);
                    var query = dataColl.groupBy(t => ({ StudentId: t.StudentId }))
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
    }


    $scope.SaveGeneralChkupInfirmry = function () {
        /*if ($scope.IsValidIncentive() == true) {*/
        if ($scope.confirmMSG.Accept == true) {
            var saveModify = $scope.newGeneralChkup.Mode;
            Swal.fire({
                title: 'Do you want to ' + saveModify + ' the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.CallSaveUpdateGChkupInfirmary();
                }
            });
        } else
            $scope.CallSaveUpdateGChkupInfirmary();
        /*}*/
    };

    $scope.CallSaveUpdateGChkupInfirmary = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        if ($scope.newGeneralChkup.CheckupDateDet) {
            $scope.newGeneralChkup.CheckupDate = $filter('date')(new Date($scope.newGeneralChkup.CheckupDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newGeneralChkup.CheckupDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        $scope.newGeneralChkup.GeneralCheckupTestValueColl = [];
        $scope.newGeneralChkup.GeneralCheckupVaccinationColl = [];
        /* var dtColl = [];*/
        $scope.StudentForTestValue.forEach(function (st) {
            st.TestNameColl.forEach(function (ph) {
                if (ph.Value !== null) {
                    $scope.newGeneralChkup.GeneralCheckupTestValueColl.push({
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
                    $scope.newGeneralChkup.GeneralCheckupVaccinationColl.push({
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
            url: base_url + "Infirmary/Creation/SaveGeneralChkupInfirmary",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.newGeneralChkup }
            /* data: { jsonData: $scope.newGeneralChkup }*/
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearGCInfirmary();
                $scope.GetAllGeneralChkupInfirmaryList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllGeneralChkupInfirmaryList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.GeneralChkupInfirmaryList = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetAllGeneralChkupInfirmry",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GeneralChkupInfirmaryList = res.data.Data;
                $scope.GeneralChkupInfirmaryList = [];

                var query = mx(res.data.Data).groupBy(t => t.Month);
                var sno = 1;
                angular.forEach(query, function (q) {
                    var pare = {
                        SNo: sno,
                        Month: q.key,
                        ChieldColl: []
                    };

                    angular.forEach(q.elements, function (el) {
                        pare.ChieldColl.push(el);
                    })
                    $scope.GeneralChkupInfirmaryList.push(pare);
                    sno++;
                });

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }


    $scope.GetGChkupInfirmaryById = function (refData) {
        $scope.loadingstatus = "running";
       
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getGeneralChkupInfirmaryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newGeneralChkup = res.data.Data;
                if ($scope.newGeneralChkup.CheckupDate)
                    $scope.newGeneralChkup.CheckupDate_TMP = new Date($scope.newGeneralChkup.CheckupDate);
                
                        
               
                if ($scope.newGeneralChkup.GeneralCheckupVaccinationColl) {
                    var clIdCOll1 = mx($scope.newGeneralChkup.GeneralCheckupVaccinationColl);               

                    $scope.GetGeneralCheckUpId();
                    $scope.GetStudentForGChkupInfirmary();

                    $scope.StudentForTestValue.forEach(function (cl) {
                        cl.IsVaccinationColl.forEach(function (Ph) {
                            Ph.Remarks = clIdCOll1.includes(Ph.Remarks);
                            Ph.IsAllow = clIdCOll1.includes(Ph.TestNameId);
                        });
                    });
                }                                         

                $scope.newGeneralChkup.Mode = 'Modify';
                document.getElementById('Nametable').style.display = "none";
                document.getElementById('formname').style.display = "block";

                
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelGeneralChkupInfirmaryById = function (refData) {
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
                    url: base_url + "Infirmary/Creation/DeleteGeneralChkupInfirmary",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllGeneralChkupInfirmaryList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


  


    //Detail part starts
    $scope.GetGChkupInfirmaryDetailById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getGeneralChkupForDetailById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;
                //if ($scope.newGeneralChkup.CheckupDate)
                //    $scope.newGeneralChkup.CheckupDate_TMP = new Date($scope.newGeneralChkup.CheckupDate);             

               

                $scope.newGeneralChkup.Mode = 'Modify';
               document.getElementById('Nametable').style.display = "none";
               document.getElementById('detailform').style.display = "block";


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

