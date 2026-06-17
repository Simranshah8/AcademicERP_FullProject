app.controller('NotificationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Notification';

	OnClickDefault();

	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {

		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.currentPages = {
			Personal: 1,
			General: 1, 
		};

		$scope.currentPages = {
			Personal: 1,
			General: 1
		};

		$scope.perPage = {
			Personal: GlobalServices.getPerPageRow(),
			General: GlobalServices.getPerPageRow() 
		};

		$scope.GetGeneralNotification();		
	}

	function OnClickDefault() {
		document.getElementById('notification-details').style.display = "none";
		document.getElementById('general heading').style.display = "none";


		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('personal-section').style.display = "block";
			document.getElementById('notification-details').style.display = "none";
		}

		//New added js starts

		//document.getElementById('general').onclick = function () {
		//	document.getElementById('general heading').style.display = "block";
		//	document.getElementById('personal-heading').style.display = "none";
		//}

		//document.getElementById('back-to-list').onclick = function () {
		//	document.getElementById('personal-section').style.display = "block";
		//	document.getElementById('notification-details').style.display = "none";
		//}

		//document.getElementById('notification').onclick = function () {
		//	document.getElementById('notification-details').style.display = "block";
		//	document.getElementById('notification-list').style.display = "none";

		//}

		//document.getElementById('back-to-list').onclick = function () {
		//	document.getElementById('notification-list').style.display = "block";
		//	document.getElementById('notification-details').style.display = "none";
		//}
		//New added js ends
	}

	$scope.PersonalImg = function (item) {
		$scope.ContentPath = item;
		$('#PersonalImg').modal('show');
	};

	$scope.genaralImg = function (item) {
		$scope.ContentPath = item;
		$('#GenaralImg').modal('show');
	};



	$scope.GetById = function (W) {
		$scope.CardDet = {};
		$scope.CardDet.Subject = W.Subject;
		$scope.CardDet.Content = W.Content;
		$scope.CardDet.ContentPath = W.ContentPath;
		$scope.CardDet.Heading = W.Heading;
		$scope.CardDet.SendBy = W.SendBy;
		$scope.CardDet.LogDate_AD = W.LogDate_AD;
		$scope.CardDet.TranId = W.TranId;
		$scope.CardDet.AtTime = W.AtTime;
		$scope.CardDet.SendByPhotoPath = W.SendByPhotoPath;

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ResPonceData = {};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/ReadNotificationLog",
			data: $scope.CardDet,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.ResponseMSG == 'Read Done') {
				document.getElementById('notification-details').style.display = "block";
				document.getElementById('personal-section').style.display = "none";
				$scope.GetPrivateNotification();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	 
	$scope.GetGeneralNotification = function ()
	{
		$scope.loadingstatus = "running";
		showPleaseWait();
		
		$scope.PersonalColl = [];
		$scope.GeneralColl = [];
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetNotification",			 
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				var tmpDataColl = res.data;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.NotificationType == "Private")
						$scope.PersonalColl.push(dc);
					else
						$scope.GeneralColl.push(dc);
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});