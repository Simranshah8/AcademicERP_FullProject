app.controller("viewNewEntityCtrl", function ($scope, $http, $filter,GlobalServices, $timeout) {
    $scope.Title = 'New Entity';
    var myDropzone = null;

    LoadData();

    function LoadData() {

        $scope.ColNames = "";
        $scope.NewEntity = {};
        var para = {
            TranId:EntityId
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/ReportWriter/GetNewEntityById",
            dataType: "json",
            data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data) {
                $scope.NewEntity = res.data.Data;

                $scope.SearchOptions = [];
                $scope.NewEntity.FilterFieldColl = [];
                angular.forEach($scope.NewEntity.FieldColl, function (fc) {
                    if ($scope.ColNames.length > 0)
                        $scope.ColNames = $scope.ColNames + ",";

                    $scope.ColNames = $scope.ColNames + fc.Name;

                    $scope.SearchOptions.push({
                        text: fc.Label,
                        value:'UT.'+fc.Name
                    });

                    if (fc.FieldType < 12) {
                        $scope.NewEntity.FilterFieldColl.push(fc);
                    }
                });

                $scope.paginationOptions = {
                    pageNumber: 1,
                    pageSize: GlobalServices.getPerPageRow(),
                    sort: null,
                    SearchType: 'text',
                    SearchCol: '',
                    SearchVal: '',
                    SearchColDet: $scope.SearchOptions[0],
                    SortingCol: $scope.NewEntity.FieldColl[0].Name,
                    SortType:' asc ',
                    pagearray: [],
                    pageOptions: [5, 10, 20, 30, 40, 50] 
                };



                $timeout(function () {

                    // DropzoneJS Demo Code Start
                    Dropzone.autoDiscover = false
                    // Get the template HTML and remove it from the doumenthe template HTML and remove it from the doument
                    var previewNode = document.querySelector("#template")
                    if (previewNode)
                    {
                        previewNode.id = ""
                        var previewTemplate = previewNode.parentNode.innerHTML
                        previewNode.parentNode.removeChild(previewNode)

                        myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
                            url: "/target-url", // Set the url
                            thumbnailWidth: 130,
                            thumbnailHeight: 130,
                            parallelUploads: 20,
                            previewTemplate: previewTemplate,
                            autoQueue: false, // Make sure the files aren't queued until manually added
                            previewsContainer: "#previews", // Define the container to display the previews
                            clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
                        })
                        document.querySelector("#actions .cancel").onclick = function () {
                            myDropzone.removeAllFiles(true)
                        }
                    // DropzoneJS Demo Code End
                    }
                 
                });
                              

                $scope.SearchData();
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

      
        $scope.loadingstatus = "stop";

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList(); 
       
        $scope.beData =
        {
            UTranId: 0,            
            Mode: 'Save',            
        };

      

    };
    $scope.ClearFields = function () {
        $scope.loadingstatus = "stop";
      
        angular.forEach($scope.NewEntity.FieldColl, function (fc) {

            $timeout(function () {
                try {

                    if (fc.FieldType == 12) {
                        var htmName = 'htm' + fc.Name;
                        $scope.beData[fc.Name] = '';
                        let get = document.getElementById(htmName);
                        //  get.summernote('code', '');
                        $('.summernote').summernote('code', '');
                    }
                    else if (fc.FieldType == 13) {
                        var imgName = 'img' + fc.Name;
                        let get = document.getElementById(imgName);
                        get.removeAttribute('src', '');
                    }

                } catch { }
            });        

            
        });

        $('input[type=file]').val('');

        $scope.beData =
        {
            UTranId: 0,
            Mode: 'Save',
        };

        $timeout(function () {
            if (myDropzone) {
                myDropzone.removeAllFiles();
            }
        });
        

        $scope.SearchData();
    }
     

   

    $scope.IsValidData = function ()
    {
         
        var newData = {};
        var isValid = true;
        var msg = "";
        $scope.NewEntity.FieldColl.forEach(function (udf) {

            if (isValid == true) {
                if (udf.FieldType == 2) {
                    var varName = udf.Name + 'Det';
                    newData[udf.Name] = $scope.beData[varName] ? $filter('date')($scope.beData[varName].dateAD, 'yyyy-MM-dd') : null;

                } else if (udf.FieldType == 3 && udf.Source && udf.Source.length > 0) {
                    newData[udf.Name] = $scope.beData[udf.Name];
                }
                else {
                    newData[udf.Name] = $scope.beData[udf.Name];
                }

                if ((newData[udf.Name] == undefined || newData[udf.Name] == null || isEmptyObj(newData[udf.Name])) && udf.IsMandatory == true) {
                    msg = 'Please ! Enter ' + udf.Label;
                    isValid = false;
                }
            }
          

        });

        if (isValid == false) { 
            Swal.fire(msg);
            return false;
        }

        return true;
    }

    $scope.AddNewData = function () {
        if ($scope.IsValidData() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.beData.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateData();
                    }
                });
            }
            else
                $scope.CallSaveUpdateData();
        }
    };

    $scope.CallSaveUpdateData = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        
        var newData = {};
        angular.forEach($scope.NewEntity.FieldColl, function (udf)
        {
            if (udf.FieldType == 2) {
                var varName = udf.Name + 'Det';
                newData[udf.Name] = $scope.beData[varName] ? $filter('date')($scope.beData[varName].dateAD, 'yyyy-MM-dd') : null;
                 
            } else if (udf.FieldType == 3 && udf.Source && udf.Source.length > 0) {
                newData[udf.Name] = $scope.beData[udf.Name];
            }
            else {
                newData[udf.Name] = $scope.beData[udf.Name];
            }

            if (newData[udf.Name] == undefined)
                newData[udf.Name] = null;
        });

        if ($scope.beData.UTranId > 0)
            newData.UTranId = $scope.beData.UTranId;

        
        $http({
            method: 'POST',
            url: base_url + "Setup/ReportWriter/SaveViewNewEntity",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("udfColl", angular.toJson($scope.NewEntity.FieldColl));
                formData.append("jsonData", angular.toJson(data.jsonData));
                formData.append("Name", $scope.NewEntity.Name);

                angular.forEach($scope.NewEntity.FieldColl, function (udf) {
                    if (udf.FieldType == 13) {
                        var varName = udf.Name + '_TMP';
                        var imgAtt = $scope.beData[varName];
                        if (imgAtt && imgAtt.length > 0)
                            formData.append(udf.Name, imgAtt[0]);
                    }
                    else if (udf.FieldType == 14) {

                        if (myDropzone) {
                            var filesColl = myDropzone.files;                            
                            var dInd = 0;
                            if (filesColl && filesColl.length > 0) {
                                angular.forEach(filesColl, function (dc) {
                                    var flName = udf.Name + dInd;
                                    formData.append(flName, dc);
                                    dInd++;
                                });
                            }
                        }
                        
                    }
                    else if (udf.FieldType == 15) {
                        var varName = udf.Name + 'Files';
                        var dInd = 0;
                        var docColl = $scope.beData[varName];
                        if (docColl && docColl.length > 0) {
                            formData.append(udf.Name, docColl[0]);
                        }
                    }
                    else if (udf.FieldType == 16) {
                        var varName = udf.Name + 'Files';
                        var dInd = 0;
                        var docColl = $scope.beData[varName];
                        if (docColl && docColl.length > 0) {
                            angular.forEach(docColl, function (dc) {
                                var flName = udf.Name + dInd;
                                formData.append(flName, dc);
                                dInd++;
                            });
                        }
                    }

                }     );


                return formData;
            },
            data: { jsonData: newData }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            if (res.data.IsSuccess == true) {
                $scope.ClearFields();
                //$scope.GetAllLedgerGroup();
            } else
                Swal.fire(res.data.ResponseMSG);

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetRecordById = function (beData) {


        $('input[type=file]').val('');

        $scope.loadingstatus = "running";
        var para = {
            TranId: beData.UTranId,
            Name: $scope.NewEntity.Name,
            ColName:$scope.ColNames,
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/ReportWriter/GetViewNewEntityById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $timeout(function () {
                    var rData = res.data.Data;

                    $scope.beData.UTranId = rData.UTranId;

                    $timeout(function () {
                        angular.forEach($scope.NewEntity.FieldColl, function (udf) {
                            if (udf.FieldType == 2) {
                                var varCol = udf.Name + "_TMP";
                                if (rData[udf.Name]) {
                                    $scope.beData[varCol] = new Date(rData[udf.Name]);
                                }

                            } else if (udf.FieldType == 3 && udf.Source && udf.Source.length > 0) {
                                $scope.beData[udf.Name] = rData[udf.Name];
                            }
                            else if (udf.FieldType == 14)
                            {
                                $scope.beData[udf.Name] = rData[udf.Name];

                                if ($scope.beData[udf.Name]) {
                                    var gallaryImg = $scope.beData[udf.Name];
                                    var ImageColl = gallaryImg.split("##");
                                    if (ImageColl && ImageColl.length > 0) {
                                        var docInd = 0;
                                        angular.forEach(ImageColl, function (doc) {
                                            var docName = udf.Name + docInd;
                                            var img = new Image();
                                            img.src = doc;
                                            img.height = 300;
                                            img.width = 300;

                                            var mockFile = {
                                                name: docName,
                                                size: 12345,
                                                width: 130,
                                                height: 130,
                                                thumbnailWidth: 130,
                                                thumbnailHeight: 130
                                            };

                                            // Call the default addedfile event handler
                                            myDropzone.emit("addedfile", mockFile);

                                            // And optionally show the thumbnail of the file:
                                            myDropzone.emit("thumbnail", mockFile, doc);

                                            myDropzone.emit("complete", mockFile);

                                            docInd++;
                                        });
                                    }
                                }


                            }
                            else {
                                $scope.beData[udf.Name] = rData[udf.Name];
                            }

                        });


                        $scope.beData.Mode = 'Modify';
                        $('#custom-tabs-four-profile-tab').tab('show');
                    });

                });

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

    }


    $scope.DeleteRecordById = function (beData, ind) {

        var colName = $scope.NewEntity.FieldColl[0].Name;

        Swal.fire({
            //scope: $scope,
            title: 'Are you sure you want to delete ' + beData[colName] + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = { TranId: beData.UTranId,Name:$scope.NewEntity.Name };

                $http({
                    method: 'POST',
                    url: base_url + "Setup/ReportWriter/DeleteViewNewEntityById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess) {
                        $scope.SearchData();
                       }

                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                });
            }

        });
    }


    $scope.SearchDataColl = [];
    $scope.SearchData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $scope.paginationOptions.TotalRows = 0;

        var sCol = $scope.paginationOptions.SearchColDet;

        var para = {
            Name: $scope.NewEntity.Name,
            ColName: $scope.ColNames,
            filter: {
                DateFrom: null,
                DateTo: null,
                PageNumber: $scope.paginationOptions.pageNumber,
                RowsOfPage: $scope.paginationOptions.pageSize,
                SearchCol: (sCol ? sCol.value : ''),
                SearchVal: $scope.paginationOptions.SearchVal,
                SortingCol: $scope.NewEntity.FieldColl[0].Name,
                SortType: ' asc ',
                SearchType: (sCol ? sCol.searchType : 'text')
            }
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/ReportWriter/GetViewNewEntityLst",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.SearchDataColl = res.data.Data.DataColl;
                $scope.paginationOptions.TotalRows = res.data.TotalCount;
                $('#searVoucherRightBtn').modal('show');

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });


    };

    $scope.ReSearchData = function (pageInd) {

        if ($scope.NewEntity && $scope.NewEntity.FieldColl) {
            $timeout(function () {
                if (pageInd && pageInd >= 0)
                    $scope.paginationOptions.pageNumber = pageInd;
                else if (pageInd == -1)
                    $scope.paginationOptions.pageNumber = 1;

                $scope.loadingstatus = 'running';
                showPleaseWait();
                $scope.paginationOptions.TotalRows = 0;
                var sCol = $scope.paginationOptions.SearchColDet;

                var para = {
                    Name: $scope.NewEntity.Name,
                    ColName: $scope.ColNames,
                    filter: {
                        DateFrom: null,
                        DateTo: null,
                        PageNumber: $scope.paginationOptions.pageNumber,
                        RowsOfPage: $scope.paginationOptions.pageSize,
                        SearchCol: (sCol ? sCol.value : ''),
                        SearchVal: $scope.paginationOptions.SearchVal,
                        SortingCol: $scope.NewEntity.FieldColl[0].Name,
                        SortType: ' asc ',
                        SearchType: (sCol ? sCol.searchType : 'text')
                    }
                };

                $http({
                    method: 'POST',
                    url: base_url + "Setup/ReportWriter/GetViewNewEntityLst",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    $scope.loadingstatus = 'stop';
                    hidePleaseWait();

                    if (res.data.IsSuccess && res.data.Data) {
                        $scope.SearchDataColl = res.data.Data.DataColl;
                        $scope.paginationOptions.TotalRows = res.data.TotalCount;

                    } else
                        alert(res.data.ResponseMSG);

                }, function (reason) {
                    alert('Failed' + reason);
                });
            });
        }
     


    }


});