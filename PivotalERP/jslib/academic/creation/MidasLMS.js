app.controller('midasLMSController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'LMS';
    LoadData();

    function LoadData() {
        $scope.LMSURL = '';
        $http({
            method: 'POST',
            url: base_url + "Global/GetMidasURL",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess) {
                $scope.LMSURL = res.data.Data.ResponseMSG;                
                document.getElementById("frmLMS").src = $scope.LMSURL;
                jQuery("#frmLMS").bind('load', function () {

                    var frameID = 'frmLMS';
                    var frameObj = document.getElementById(frameID);
                    var frameContent = frameObj.contentWindow.document.body.innerHTML;

                    var f1 = $("#frmLMS");                
                    var target = $(".main-body__title", f1.contents());
                    target.removeClass("test2");

                    $("#frmLMS").contents().find(".main-body__title").removeClass("hidden");
                    //main-body__title
                    console.log('loaded')
                    
                });

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }
});