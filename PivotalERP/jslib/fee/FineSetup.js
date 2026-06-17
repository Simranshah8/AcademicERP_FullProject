
app.controller('FineSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {

    $scope.Title = 'Fine Setup';

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.SelectConditionList = [{ id: 1, text: 'Minimum Dues' }, { id: 2, text: 'Fine Skip Days' }];
        $scope.FineOnBasisoFAmountList = [{ id: 1, text: 'Amount' }, { id: 2, text: 'Percentage' }];
        $scope.FineOnBasisoFMonthList = [{ id: 1, text: 'Each Month' }, { id: 2, text: 'One Time' }];

        $scope.ClassList = [];
        GlobalServices.getClassList().then(function (res) {
            $scope.ClassList = res.data.Data; 
        }, function (reason) {
            Swal.fire('Failed' + reason);
        }); 

        $scope.beData = {
            TranId: 0,
            classId: null,
            IsActiveWise: false,
            SelectConditionAsPer: 1,
            ConditionAmount: 0,
            FineOnBasisOfAmnt: 1,
            FineOnBasisOfMonth: 1,
            FineAmount: 0,
            DebateOnBasisOfAmnt: 1,
            DebateOnBasisOfMonth: 1,
            ReBateAmount: 0,
            FineSetupColl: []
        };

        $scope.newDet = {
            TranId: 0,
            ClassId: null,
            FineOnBasisOfAmount: 1,
            DebateOnBasisOfAmount: 1,
            FineAmount: 0,
            ReBateAmount: 0,
            CurFineSetupColl: []
        }

        $scope.Fine = {};
        $http({
            method: 'POST',
            url: base_url + "Fee/Creation/GetFineSetup",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.Fine = res.data.Data;

                if ($scope.Fine.DueDataDetColl && $scope.Fine.DueDataDetColl.length > 0)
                    $scope.newDet = $scope.Fine.DueDataDetColl[0];

                if ($scope.Fine.FineSetupDetColl && $scope.Fine.FineSetupDetColl.length > 0)
                    $scope.beData = $scope.Fine.FineSetupDetColl[0];

                $scope.ChangeLabel();
                $scope.Changetext();
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

   
    $scope.AddFineSetupMod = function (ind) {
        if ($scope.CurFineSetup.FineSetupColl) {
            if ($scope.CurFineSetup.FineSetupColl.length > ind + 1) {
                $scope.CurFineSetup.FineSetupColl.splice(ind + 1, 0, {
                    DaysFrom: 0
                })
            } else {
                $scope.CurFineSetup.FineSetupColl.push({
                    DaysFrom: 0
                })
            }
        }
    };
    $scope.delFineSetupMod = function (ind) {
        if ($scope.CurFineSetup.FineSetupColl) {
            if ($scope.CurFineSetup.FineSetupColl.length > 1) {
                $scope.CurFineSetup.FineSetupColl.splice(ind, 1);
            }
        }
    };

    $scope.AddCurFineSetup = function (ind) {
        if ($scope.CurDueFollow.CurFineSetupColl) {
            if ($scope.CurDueFollow.CurFineSetupColl.length > ind + 1) {
                $scope.CurDueFollow.CurFineSetupColl.splice(ind + 1, 0, {
                    DaysFrom: 0
                })
            } else {
                $scope.CurDueFollow.CurFineSetupColl.push({
                    DaysFrom: 0
                })
            }
        }
    };

    $scope.delCurFineSetup = function (ind) {
        if ($scope.CurDueFollow.CurFineSetupColl) {
            if ($scope.CurDueFollow.CurFineSetupColl.length > 1) {
                $scope.CurDueFollow.CurFineSetupColl.splice(ind, 1);
            }
        }
    };



    $scope.SaveUpdateFineSetup = function () {
        if ($scope.confirmMSG.Accept == true) {
            var saveModify = $scope.beData.Mode;
            Swal.fire({
                title: 'Do you want to' + saveModify + 'the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.CallSaveUpdateFineSetup();
                }

            });
        }
        else
            $scope.CallSaveUpdateFineSetup();
    };

    $scope.CallSaveUpdateFineSetup = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
          
        angular.forEach($scope.Fine.FineSetupDetColl, function (fs) {
            fs.SelectConditionAsPer = $scope.beData.SelectConditionAsPer;
            fs.FineOnBasisOfAmnt = $scope.beData.FineOnBasisOfAmnt;
            fs.FineOnBasisOfMonth = $scope.beData.FineOnBasisOfMonth;
            fs.DebateOnBasisOfAmnt = $scope.beData.DebateOnBasisOfAmnt;
            fs.DebateOnBasisOfMonth = $scope.beData.DebateOnBasisOfMonth;
        });

        angular.forEach($scope.Fine.DueDataDetColl, function (fs) {
            fs.FineOnBasisOfAmount = $scope.beData.FineOnBasisOfAmount;
            fs.DebateOnBasisOfAmount = $scope.beData.DebateOnBasisOfAmount; 
        });

        $http({
            method: 'POST',
            url: base_url + "Fee/Creation/SaveFineSetup",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.Fine }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.ChangeLabel = function () {
        if ($scope.newDet.FineOnBasisOfAmount == 0 || $scope.newDet.FineOnBasisOfAmount ==undefined) {
            Swal.fire('Please ! Select Fine Basis');
        }
        else if ($scope.newDet.FineOnBasisOfAmount == 1) {
            $scope.RspMessage = 'Amount';
        }
        else if ($scope.newDet.FineOnBasisOfAmount == 2) {
            $scope.RspMessage = 'Percentage';
        }
    }

    $scope.Changetext = function () {
        if ($scope.beData.FineOnBasisOfAmnt==0 || $scope.beData.FineOnBasisOfAmnt==undefined) {
            Swal.fire('Please ! Select Fine Basis');
        }
        else if ($scope.beData.FineOnBasisOfAmnt == 1) {
            $scope.ResponseMsg = 'Amount'
        }
        else if ($scope.beData.FineOnBasisOfAmnt == 2) {
            $scope.ResponseMsg = 'Percentage' 
        }
    }

    $scope.CurFineSetup = {};
    $scope.ShowRange = function (fs) {
        $scope.CurFineSetup = fs;
        if ($scope.CurFineSetup.FineSetupColl == null)
            $scope.CurFineSetup.FineSetupColl = [];

        if ($scope.CurFineSetup.FineSetupColl.length == 0)
            $scope.CurFineSetup.FineSetupColl.push({});

        $('#modal-xl').modal('show');
    }

    $scope.CurDueFollow = {};
    $scope.ShowRange2 = function (fs) {
        $scope.CurDueFollow = fs;

        if ($scope.CurDueFollow.CurFineSetupColl == null)
            $scope.CurDueFollow.CurFineSetupColl = [];

        if ($scope.CurDueFollow.CurFineSetupColl.length == 0)
            $scope.CurDueFollow.CurFineSetupColl.push({});

        $('#modal-two').modal('show');
    }
});