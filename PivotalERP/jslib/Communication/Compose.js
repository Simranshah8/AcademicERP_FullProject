
app.controller('ComposeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Email Compose';
    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.EmployeeList = [{ id: 1, text: 'Ramesh Sharma' }, { id: 2, text: 'Ganesh Sharma' }];
        $scope.dropdownValues = ["Option 1", "Option 2", "Option 3"];
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.FilterStudentList = [{ id: 1, text: 'Class-Section' }, { id: 2, text: 'Student Type' }, { id: 3, text: 'Student Group' }, { id: 4, text: 'Student House' }, { id: 5, text: 'Transport' }, { id: 6, text: 'Gender' },{ id: 7, text: 'Individual' }];
        $scope.FilterEmployeeList = [{ id: 1, text: 'Employee' }, { id: 2, text: 'EmployeeGroup' }];
        $scope.currentPages = {
            Compose: 1,
            BulkCompose: 1
        };

        $scope.searchData = {
            Compose: '',
            BulkCompose: '',
        };

        $scope.perPage = {
            Compose: GlobalServices.getPerPageRow(),
            BulkCompose: GlobalServices.getPerPageRow(),
        };
        $scope.newStudent = {
            FilterID: 1
        };
        $scope.newEmployee = {
            EmployeeFilterID: 1
        };

        $scope.newEmailCompose = {
            EmailComposeId: null,
            
            Mode: 'Send'
        };
        $scope.newNotificationCompose = {
            NotificationComposeId: null,

            Mode: 'Send'
        };
        $scope.newWhatsappCompose = {
            WhatsappComposeId: null,

            Mode: 'Send'
        };
        $scope.newSmsCompose = {
            SmsComposeId: null,
            Name: '',
            Brand: '',
            OrderNo: 1,
            BulkComposeForId: null,
            Mode: 'Send'
        };

        //$scope.CommunicationTypeList = [];
        //$http({
        //    method: 'GET',
        //    url: base_url + "Communication/Creation/GetAllCommunicationType",
        //    dataType: "json"
        //}).then(function (res) {
        //    hidePleaseWait();
        //    $scope.loadingstatus = "stop";
        //    if (res.data.IsSuccess && res.data.Data) {
        //        $scope.CommunicationTypeList = res.data.Data;

        //    } else {
        //        Swal.fire(res.data.ResponseMSG);
        //    }

        //}, function (reason) {
        //    Swal.fire('Failed' + reason);
        //});
        //$scope.GetAllComposeList();
        //$scope.GetAllBulkComposeList();
    };



    $scope.myButton1 = 'btn-success';
    $scope.myButton2 = 'btn-success';
    $scope.myButton3 = 'btn-success';
    $scope.myButton4 = 'btn-success';
    $scope.myButton5 = 'btn-success';
    $scope.changeBgColor1 = function () {
        $scope.myButton1 = "btn-lightgreen";
    };
    $scope.changeBgColor2 = function () {
        $scope.myButton2 = "btn-lightgreen ";
    };
    $scope.changeBgColor3 = function () {
        $scope.myButton3 = "btn-lightgreen ";
    };
    $scope.changeBgColor4 = function () {
        $scope.myButton4 = "btn-lightgreen ";
    };
    $scope.changeBgColor5 = function () {
        $scope.myButton5 = "btn-lightgreen ";
    };
    $scope.newEmailCompose = function () {
        $scope.newEmailCompose = {
            EmailComposeId: null,
           
            Mode: 'Send'
        };
    };
    $scope.ClearSmsCompose = function () {
        $scope.newSmsCompose = {
            SmsComposeId: null,
            BulkComposeForId: null,
            Mode: 'Send'
        };
    };


    function OnClickDefault() {
        document.getElementById('schedulediv').style.display = "none";
        document.getElementById('schedulesmsdiv').style.display = "none";
        document.getElementById('schedulevoicediv').style.display = "none";
        document.getElementById('schedulenotificationdiv').style.display = "none";
        document.getElementById('scheduleWhatsappdiv').style.display = "none";
        document.getElementById('audioprogress').style.display = "none";
        
        document.getElementById('schedulebth').onclick = function () {
            document.getElementById('schedulediv').style.display = "block";
        }

        document.getElementById('schedulesmsbth').onclick = function () {
            document.getElementById('schedulesmsdiv').style.display = "block";
        }
        document.getElementById('schedulevoicebth').onclick = function () {
            document.getElementById('schedulevoicediv').style.display = "block";
        }
        document.getElementById('schedulenotificationbth').onclick = function () {
            document.getElementById('schedulenotificationdiv').style.display = "block";
        }
        document.getElementById('scheduleWhatsappbth').onclick = function () {
            document.getElementById('scheduleWhatsappdiv').style.display = "block";
        }

        document.getElementById('translateaudiobtn').onclick = function () {
            document.getElementById('audioprogress').style.display = "block";
        }

    };

   
    var BASE64_MARKER = ';base64,';
    // Does the given URL (string) look like a base64-encoded URL?
    function isDataURI(url) {
        return url.split(BASE64_MARKER).length === 2;
    };
    function dataURItoFile(dataURI) {
        if (!isDataURI(dataURI)) { return false; }
        // Format of a base64-encoded URL:
        // data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
        var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
        var filename = 'rc-' + (new Date()).getTime() + '.' + mime.split('/')[1];
        //var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
        var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
        var writer = new Uint8Array(new ArrayBuffer(bytes.length));
        for (var i = 0; i < bytes.length; i++) {
            writer[i] = bytes.charCodeAt(i);
        }
        return new File([writer.buffer], filename, { type: mime });
    }


    
   
});

