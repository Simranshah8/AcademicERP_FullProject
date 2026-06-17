app.controller('DailySummaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'DailySummary';

    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
     

        $scope.newLedgerwise = {
            DateFromDet: new Date(),
            DateToDet: new Date(),
            branchId: 0,
            branchIdColl: ''
        };

        $scope.beData = {
            //Overall Student Strength
            RunningBoys: 0,
            RunningGirls: 0,

            LeftBoys: 0,
            LeftGirls: 0,

            TransportBoys: 0,
            TransportGirls: 0,

            HostelBoys: 0,
            HostelGirls: 0,


            //Students Attendance Overview
            TotalStudent: 0,
            TotalPresentStudent: 0,
            TotalAbsentStudent: 0,

            TotalBoys: 0,
            TotalGirls: 0,

            TodayPresentBoys: 0,
            TodayPresentGirls: 0,

            TodayAbsentBoys: 0,
            TodayAbsentGirls: 0,


            //Overall Staff Strength
            TeachingStaffMen: 0,
            TeachingStaffWomen: 0,

            NonTeachingStaffMen: 0,
            NonTeachingStaffWomen: 0,


            //Staff Attendance Overview
            TotalStaff: 0,
            TotalPresentStaff: 0,
            TotalAbsentStaff: 0,

            TotalStaffMen: 0,
            TotalStaffWomen: 0,

            TodayPresentMen: 0,
            TodayPresentWomen: 0,

            TodayAbsentMen: 0,
            TodayAbsentWomen: 0,


            //Today's Accounts Status 
            AccountStatusColl: [
                {
                    FeeCollected: 125000,
                    ExpenseSent: 82000,
                    DiscountGiven: 5000,
                    TotalDues: 45000,
                    RunningDues: 25000
                },
                {
                    FeeCollected: 98000,
                    ExpenseSent: 76000,
                    DiscountGiven: 2500,
                    TotalDues: 32000,
                    RunningDues: 18000
                },
                {
                    FeeCollected: 143000,
                    ExpenseSent: 96500,
                    DiscountGiven: 4000,
                    TotalDues: 52000,
                    RunningDues: 27000
                }
            ],
            //Class Wise Fee Summary
            FeeSummaryColl: [
                {
                    ClassName: "Nursery",
                    StudentNumber: 25,
                    TotalFee: 125000,
                    FeeCollected: 98000,
                    DiscountGiven: 5000,
                    BalanceAmount: 22000
                },
                {
                    ClassName: "LKG",
                    StudentNumber: 28,
                    TotalFee: 140000,
                    FeeCollected: 115000,
                    DiscountGiven: 4000,
                    BalanceAmount: 21000
                },
                {
                    ClassName: "UKG",
                    StudentNumber: 30,
                    TotalFee: 150000,
                    FeeCollected: 122000,
                    DiscountGiven: 7000,
                    BalanceAmount: 21000
                },
                {
                    ClassName: "Grade 1",
                    StudentNumber: 32,
                    TotalFee: 160000,
                    FeeCollected: 135000,
                    DiscountGiven: 3000,
                    BalanceAmount: 22000
                },
                {
                    ClassName: "Grade 2",
                    StudentNumber: 29,
                    TotalFee: 145000,
                    FeeCollected: 120000,
                    DiscountGiven: 5000,
                    BalanceAmount: 20000
                },
                {
                    ClassName: "Grade 3",
                    StudentNumber: 27,
                    TotalFee: 135000,
                    FeeCollected: 100000,
                    DiscountGiven: 8000,
                    BalanceAmount: 27000
                }
            ],
            //Cash and Bank Summary
            CashAndBankColl: [
                {
                    Particular: "Cash in Hand",
                    Opening: 25000,
                    Transaction: 15000,
                    Closing: 40000
                },
                {
                    Particular: "SBI Bank",
                    Opening: 125000,
                    Transaction: -20000,
                    Closing: 105000
                },
                {
                    Particular: "Nepal Bank",
                    Opening: 98000,
                    Transaction: 5000,
                    Closing: 103000
                },
                {
                    Particular: "HBL Bank",
                    Opening: 76000,
                    Transaction: -10000,
                    Closing: 66000
                },
                {
                    Particular: "Petty Cash",
                    Opening: 5000,
                    Transaction: 3000,
                    Closing: 8000
                }
            ],




            //SMS Balance
            AvailableDate: 0,
            LastTransactionDate: 0,

            RamainingSMS: 0,
            SMSSent: 0,

            LastIP: 0,
            CurentIP: 0,


        };
        $scope.CompanyConfig = {};
        $scope.Logo = {};
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetCompanyDet",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CompanyConfig = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //logo

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



    };

  



    $scope.PrintData = function () {
        $('#printcard').printThis();
    }
});