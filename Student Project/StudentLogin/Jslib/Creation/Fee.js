app.controller('FeeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Fee';

	OnClickDefault();

	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			StudentLedger: 1,
			

		};

		$scope.searchData = {
			StudentLedger: '',

		};

		$scope.perPage = {
			StudentLedger: GlobalServices.getPerPageRow(),

		};

		$scope.newStudentLedger = {
			StudentLedgerId: null,

		};
		 
	}

	function OnClickDefault() {


		document.getElementById('payment-details').style.display = "none";
		document.getElementById('otp-pin').style.display = "none";

		document.getElementById('basic-addon2').onclick = function () {
			document.getElementById('payment-details').style.display = "block";
			document.getElementById('online-payment').style.display = "none";
			$scope.ClearOnlinePayment();
		}
		document.getElementById('back-to-getway').onclick = function () {
			document.getElementById('payment-details').style.display = "none";
			document.getElementById('online-payment').style.display = "block";
			$scope.ClearOnlinePayment();
		}
		document.getElementById('back-to-details').onclick = function () {
			document.getElementById('payment-details').style.display = "block";
			document.getElementById('online-payment').style.display = "none";
			document.getElementById('otp-pin').style.display = "none";
			$scope.ClearOnlinePayment();
		}
		document.getElementById('next').onclick = function () {
			document.getElementById('payment-details').style.display = "none";
			document.getElementById('online-payment').style.display = "none";
			document.getElementById('otp-pin').style.display = "block";
			$scope.ClearOnlinePayment();
		}


	}



        


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});