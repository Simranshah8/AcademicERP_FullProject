app.controller('ContactController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Contact';

    OnClickDefault();
   
    
    $scope.LoadData = function () {
        $('.select2').select2();
     
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            ContactGroup: 1,
            Contact: 1
        };


        $scope.searchData = {
            ContactGroup: '',
            Contact: ''
        };

        $scope.perPage = {
            ContactGroup: GlobalServices.getPerPageRow(),
            Contact: GlobalServices.getPerPageRow()
        };



        $scope.newContactGroup = {
            GroupId: null,
            Name: '',
            OrderNo: 0,
            Description: '',
            Mode: 'save'

        };

        $scope.newContact = {
            ContactId: null,
            Name: '',
            GroupId: null,
            ContactNo: '',
            OrderNo: 0,
            Description: '',
            Mode: 'save'
        };

        $scope.GetallContactList();
        $scope.GetAllContactGroupList();

    };



    function OnClickDefault() {
        document.getElementById('Contact-form').style.display = "none";
        document.getElementById('Contact-Group-form').style.display = "none";


        document.getElementById('add-class-Contact').onclick = function () {
            document.getElementById('Contact-content').style.display = "none";
            document.getElementById('Contact-form').style.display = "block";
        }

        document.getElementById('back-btn-class-Contact').onclick = function () {
            document.getElementById('Contact-form').style.display = "none";
            document.getElementById('Contact-content').style.display = "block";
            $scope.ClearContact();
        }


        document.getElementById('add-class-ContactGroup').onclick = function () {
            document.getElementById('Contact-Group-content').style.display = "none";
            document.getElementById('Contact-Group-form').style.display = "block";
        }

        document.getElementById('back-btn-class-ContactGroup').onclick = function () {
            document.getElementById('Contact-Group-form').style.display = "none";
            document.getElementById('Contact-Group-content').style.display = "block";
            $scope.ClearContactGroup();
        }
    };


    $scope.ClearContactGroup = function () {
        $timeout(function () {
            $scope.newContactGroup = {
                GroupId: null,
                Name: '',
                OrderNo: 0,
                Description: '',
                Mode: 'save'
            };
        });
    }

    $scope.ClearContact = function () {
        $timeout(function () {
            $scope.newContact = {
                ContactId: null,
                Name: '',
                GroupId: null,
                ContactNo: '',
                OrderNo: 0,
                Description: '',
                Mode: 'save'
            };
        });
    }



    /*    ContactGroup starts from here*/

    $scope.IsValidAddContactGroup = function () {
        if ($scope.newContactGroup.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveContactGroup = function () {
        if ($scope.IsValidAddContactGroup() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newContactGroup.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateContactGroup();
                    }
                });
            } else
                $scope.SaveUpdateContactGroup();
        }
    };

    $scope.SaveUpdateContactGroup = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveContactGroup",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newContactGroup }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearContactGroup();
                $scope.GetAllContactGroupList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }



    $scope.GetAllContactGroupList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ContactGroupList = [];
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/GetAllContactGroup",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ContactGroupList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetContactGroupById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            GroupId: refData.GroupId
        };
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/GetContactGroupById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newContactGroup = res.data.Data;

                document.getElementById('Contact-Group-content').style.display = "none";
                document.getElementById('Contact-Group-form').style.display = "block";
                $scope.newEmployeeDet.Mode = 'Modify';

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelContactGroupById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {

            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    GroupId: refData.GroupId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Communication/Creation/DelContactGroupById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.ClearContactGroup();
                        $scope.GetAllContactGroupList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


    /*    cotact group ends here*/

    $scope.IsValidAddContact = function () {
        if ($scope.newContact.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveContact = function () {
        if ($scope.IsValidAddContact() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newContact.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateContact();
                    }
                });
            } else
                $scope.SaveUpdateContact();
        }
    };

    $scope.SaveUpdateContact = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveContact",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newContact }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearContact();
                $scope.GetallContactList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }




    $scope.GetallContactList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ContactList = [];
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/GetAllContact",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ContactList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetContactById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ContactId: refData.ContactId
        };
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/GetContactById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newContact = res.data.Data;

                document.getElementById('Contact-content').style.display = "none";
                document.getElementById('Contact-form').style.display = "block";
                $scope.newContact.Mode = 'Modify';

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.DelContactById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {

            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    ContactId: refData.ContactId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Communication/Creation/DelContactById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.ClearContact();
                        $scope.GetallContactList();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };

});