app.controller('FinancialReportsController',
    function ($scope, $http, $timeout, $filter, GlobalServices) {

        $scope.Title = 'Financial Reports';
       
        $scope.LoadData = function () {

            $('.select2').select2();
            $scope.starStatus = {
                Payment: false,
                PaymentSummary: false,
                FeeWisePayment: false,
                TransactionSummary: false,
                PaymentMethodDaybook: false,
                AllBills: false,
                VoidPayment: false,
                TenderPayment: false,
                PaymentRefundReport: false,
                PaymentChangeLogs: false,
                InvoiceStatement: false,
                MonthlyIS: false,
                YearlyIS: false
            };

            $scope.toggleStar = function (key) {
                $scope.starStatus[key] = !$scope.starStatus[key];
            };


           
        };      

    });
