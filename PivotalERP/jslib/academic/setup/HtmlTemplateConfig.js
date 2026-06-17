app.controller('TemplateController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Template';

    $scope.LoadData = function () {

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.admitCardTemplates = [
            { id: 1, EntityId: 1, name: "AdmitCard2inA4", displayName: "Classic Admit Card", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/AdmitCard2inA4.png" },
            { id: 2, EntityId: 1, name: "AdmitCard4inA4", displayName: "Modern Clean Style", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/AdmitCard4inA4.png" },
            { id: 3, EntityId: 1, name: "AdmitCardA4", displayName: "Compact Layout", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/AdmitCardA4.png" },
            { id: 4, EntityId: 1, name: "AdmitCardwithoutSchedule", displayName: "Photo Left Style", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/AdmitCrad2inA4withoutSchedule.png" },
            { id: 5, EntityId: 1, name: "admitcard8inA4", displayName: "Photo Top Style", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/admitcard8inA4.png" },
            { id: 6, EntityId: 1, name: "admitcard6inA4", displayName: "Photo Top Style", PhotoPath: "/wwwroot/dynamic/images/Report/admitcard/6inA4.png" }
        ];

        $scope.marksheetTemplates = [
            { id: 1, EntityId: 360, name: "DefaultMarksheet", displayName: "Default Marksheet", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/DefaultMarksheet.png" },
            { id: 2, EntityId: 360, name: "gradesheet2inA4", displayName: "Gradesheet 2 in A4", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/gradesheet2inA4.png" },
            { id: 3, EntityId: 360, name: "GradesheetwithSeperateECA", displayName: "Gradesheet with Seperate ECA", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/GradesheetwithSeperateECA.png" },
            { id: 4, EntityId: 360, name: "MarksheetwithSeperateECA", displayName: "Marksheet with Seperate ECA", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/MarksheetwithSeperateECA.png" },
            { id: 5, EntityId: 360, name: "MarkhseetwithSeperateECA2nd", displayName: "Markhseet with Seperate ECA 2nd", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/MarkhseetwithSeperateECA2nd.png" },
            { id: 6, EntityId: 360, name: "Marksheet2inA4", displayName: "Marksheet 2 in A4", PhotoPath: "/wwwroot/dynamic/images/Report/marksheet/Marksheet2inA4.png" }
        ];

        $scope.idCardTemplates = [
            { id: 1, EntityId: 3, name: "Template1", displayName: "Template 1", PhotoPath: "/wwwroot/dynamic/images/Report/Idcards/IDcard.png" },
            { id: 2, EntityId: 3, name: "Template2", displayName: "Template 2" },
            { id: 3, EntityId: 3, name: "Template3", displayName: "Template 3" },
            { id: 4, EntityId: 3, name: "Template4", displayName: "Template 4" },
            { id: 5, EntityId: 3, name: "Template5", displayName: "Template 5" }
        ];

        $scope.CertificateTemplates = [
            { id: 1, EntityId: 4, name: "CCcertificate", displayName: "Template 1", PhotoPath: "/wwwroot/dynamic/images/Report/certificates/CCcertificate.png"},
            { id: 2, EntityId: 4, name: "TCcertificate", displayName: "Template 2 ", PhotoPath: "/wwwroot/dynamic/images/Report/certificates/TCcertificate.png" },
            { id: 3, EntityId: 4, name: "ExtraCertificate", displayName: "Template 3", PhotoPath: "/wwwroot/dynamic/images/Report/certificates/ExtraCertificate.png" },
            { id: 4, EntityId: 4, name: "Template4", displayName: "Template 4" },
            { id: 5, EntityId: 4, name: "Template5", displayName: "Template 5" }
        ];

        $scope.FeeReceiptTemplates = [
            { id: 1, EntityId: 5, name: "FeeReceipt2inA4", displayName: "Template 1", PhotoPath: "/wwwroot/dynamic/images/Report/feereceipt/FeeReceipt2inA4.png"},
            { id: 2, EntityId: 5, name: "FeeReceipt4inA4", displayName: "Template 2 ", PhotoPath: "/wwwroot/dynamic/images/Report/feereceipt/FeeReceipt4inA4.png" },
            { id: 3, EntityId: 5, name: "FeeReceiptA4", displayName: "Template 3", PhotoPath: "/wwwroot/dynamic/images/Report/feereceipt/FeeReceiptA4.png" },
            { id: 4, EntityId: 5, name: "FeeReceiptA5", displayName: "Template 4", PhotoPath: "/wwwroot/dynamic/images/Report/feereceipt/FeeReceiptA5.png"},
            { id: 5, EntityId: 5, name: "Template5", displayName: "Template 5" }
        ];

        $scope.newDet = {
            TemplateId: null,
            EntityId: null,
            SNo: null,
            TemplateName:'',
            IsAllowed:'',
        }
        $scope.GetHTMLTemplateConfig();
    }

    $scope.SaveHTMLTemplate = function () {
        var allTemplates = [];
        var allFiles = [];

        function pushTemplates(templateList) {
            angular.forEach(templateList, function (t) {
                var previewPath = (t.PreviewPath && t.PreviewPath.name) ? "" : (t.PreviewPath || "");
                var newData = {
                    TemplateId: t.TemplateId || 0,   // ✅ backend matches by TemplateId
                    EntityId: t.EntityId,
                    TemplateName: t.TemplateName,
                    PreviewPath: previewPath,
                    SNo: t.id,
                    IsAllowed: t.IsAllowed || false,
                    ForApp: t.ForApp || false
                };
                allTemplates.push(newData);

                // If file uploaded, add to file collection
                if (t.PreviewPath && t.PreviewPath instanceof File) {
                    allFiles.push({ templateId: newData.TemplateId, file: t.PreviewPath });
                }
            });
        }

        // Collect templates from all lists
        pushTemplates($scope.admitCardTemplates);
        pushTemplates($scope.marksheetTemplates);
        pushTemplates($scope.idCardTemplates);
        pushTemplates($scope.CertificateTemplates);
        pushTemplates($scope.FeeReceiptTemplates);

        Swal.fire({
            title: "Are you sure?",
            text: "Do you want to update the template settings?",
            icon: "question",
            showCancelButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                showPleaseWait();
                $scope.loadingstatus = "running";

                $http({
                    method: 'POST',
                    url: base_url + "Academic/SetUp/SaveHTMLTemplateConfig",
                    headers: { 'Content-Type': undefined },
                    transformRequest: function (data) {
                        var formData = new FormData();
                        formData.append("jsonData", angular.toJson(data.jsonData));

                        // Append each uploaded file with key file_<TemplateId>
                        //if (data.jsonData) {
                        //    for (var i = 0; i < data.jsonData.length; i++) {
                        //        for (var j = 0; j < data.jsonData[i].PreviewPath; j++) {
                        //            formData.append("file" + i, data.files[j].File);
                        //        }
                        //    }
                        //}

                        return formData;
                    },
                    data: { jsonData: allTemplates, logofiles: allFiles }
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.Data) {
                        $scope.GetHTMLTemplateConfig();
                    }
                }, function (error) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(error.data?.ResponseMSG || "Something went wrong");
                });
            }
        });
    };



    $scope.GetHTMLTemplateConfig = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Academic/SetUp/GetHTMLTemplatesConfig",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                var backendData = res.data.Data;

                // Reset all values
                angular.forEach($scope.admitCardTemplates, function (item) {
                    item.IsAllowed = false;
                    item.TemplateName = "";
                });
                angular.forEach($scope.marksheetTemplates, function (item) {
                    item.IsAllowed = false;
                    item.TemplateName = "";
                });
                angular.forEach($scope.idCardTemplates, function (item) {
                    item.IsAllowed = false;
                    item.TemplateName = "";
                });
                angular.forEach($scope.CertificateTemplates, function (item) {
                    item.IsAllowed = false;
                    item.TemplateName = "";
                });
                angular.forEach($scope.FeeReceiptTemplates, function (item) {
                    item.IsAllowed = false;
                    item.TemplateName = "";
                });
                backendData.forEach(function (dataItem) {
                    if (dataItem.EntityId === 1) {
                        var match = $scope.admitCardTemplates.find(function (x) {
                            return x.id === dataItem.SNo;
                        });
                        if (match) {
                            match.IsAllowed = dataItem.IsAllowed;
                            match.TemplateName = dataItem.TemplateName;
                        }
                    }
                    else if (dataItem.EntityId === 360) {
                        var match = $scope.marksheetTemplates.find(function (x) {
                            return x.id === dataItem.SNo;
                        });
                        if (match) {
                            match.IsAllowed = dataItem.IsAllowed;
                            match.TemplateName = dataItem.TemplateName;
                        }
                    } else if (dataItem.EntityId === 3) {
                        var match = $scope.idCardTemplates.find(function (x) {
                            return x.id === dataItem.SNo;
                        });
                        if (match) {
                            match.IsAllowed = dataItem.IsAllowed;
                            match.TemplateName = dataItem.TemplateName;
                        }
                    }else if (dataItem.EntityId === 4) {
                        var match = $scope.CertificateTemplates.find(function (x) {
                            return x.id === dataItem.SNo;
                        });
                        if (match) {
                            match.IsAllowed = dataItem.IsAllowed;
                            match.TemplateName = dataItem.TemplateName;
                        }
                    }else if (dataItem.EntityId === 5) {
                        var match = $scope.FeeReceiptTemplates.find(function (x) {
                            return x.id === dataItem.SNo;
                        });
                        if (match) {
                            match.IsAllowed = dataItem.IsAllowed;
                            match.TemplateName = dataItem.TemplateName;
                        }
                    }
                });
            }
            else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire("Failed: " + reason.statusText);
        });
    };


    $scope.previewTemplate = function (item) {
        $scope.viewImg1 = {
            ContentPath: '',
            FileType: null
        };
        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg1.ContentPath = item.DocPath;
            $scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer1').src = item.DocPath;
            $('#templatePreviewModal').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg1.ContentPath = item.PhotoPath;
            $scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
            $('#templatePreviewModal').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg1.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';
            if ($scope.viewImg1.FileType === 'pdf') {
                document.getElementById('pdfViewer1').src = $scope.viewImg1.ContentPath;
            }
            $('#templatePreviewModal').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    //$scope.loadTemplates();

});