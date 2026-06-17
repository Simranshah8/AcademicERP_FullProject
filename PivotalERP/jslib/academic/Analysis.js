app.controller('AnalysisController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Analysis';



    $scope.LoadData = function ()
    {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            StudentSummary: 1,
        }
        $scope.searchData = {
            StudentSummary: '',
        }
        $scope.perPage = {
            StudentSummary: GlobalServices.getPerPageRow(),
        }

        $scope.loadStudentDynamicSummary();

        $scope.ColumnColl = [{ id: 1, text: 'NoOfStudent', datatype: 'int32' }, { id: 2, text: 'Class', datatype: 'string' }, { id: 3, text: 'Section', datatype: 'string' }, { id: 4, text: 'Gender', datatype: 'string' }, { id: 5, text: 'Caste', datatype: 'string' },
            { id: 6, text: 'House', datatype: 'string' }, { id: 7, text: 'Medium', datatype: 'string' }, { id: 8, text: 'Board', datatype: 'string' }, { id: 9, text: 'IsNew', datatype: 'string' }, { id: 10, text: 'Route', datatype: 'string' },
            { id: 11, text: 'Point', datatype: 'string' }, { id: 12, text: 'Room', datatype: 'string' }, { id: 13, text: 'TravelType', datatype: 'string' }, { id: 14, text: 'Age', datatype: 'string' },
            , { id: 15, text: 'Batch', datatype: 'string' }, { id: 16, text: 'Semester', datatype: 'string' }, { id: 17, text: 'ClassYear', datatype: 'string' }, { id: 18, text: 'Faculty', datatype: 'string' }, { id: 19, text: 'Level', datatype: 'string' }

        ];
        $scope.AcademicConfig = {};
        GlobalServices.getAcademicConfig().then(function (res1) {
            $scope.AcademicConfig = res1.data.Data;
        });

		
		
		
        $scope.columnDefs = [];
        var sno = 1;
        angular.forEach($scope.ColumnColl, function (cln) {
            if (cln.datatype == "string")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
            else if (cln.datatype == "int32" || cln.datatype == "int64")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text, aggregateFunc: 'sum', formatFunc: function (val) { return $filter('formatNumber')(val); } });
            else if (cln.datatype == "double" || cln.datatype == "float")
                $scope.columnDefs.push({
                    name: cln.text, caption: cln.text,
                    dataSettings: {
                        aggregateFunc: 'sum',
                        formatFunc: function (value) {
                            //return $filter('formatNumber')(value);
                            return Number(value).toFixed(0);
                        }
                    }
                });
            else if (cln.datatype == "datetime" || cln.datatype == "date")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
            else
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
        });
		
        $scope.AgeList = [];
        $scope.AgeList.push({
            Age:0
        });

		google.load("visualization", "1", { packages: ["corechart", "charteditor"] });

	}

    $scope.AddAgeList = function (ind) {
        if ($scope.AgeList) {
            if ($scope.AgeList.length > ind + 1) {
                $scope.AgeList.splice(ind + 1, 0, {
                    Age: 0
                })
            } else {
                $scope.AgeList.push({
                    Age: 0
                })
            }
        }

    };
    $scope.DelAgeList = function (ind) {
        if ($scope.AgeList) {
            if ($scope.AgeList.length > 1) {
                $scope.AgeList.splice(ind, 1);
            }
        }
    };

    $scope.GetDataForMatrix = function () {

        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DataColl = [];

        var agePara = [];
        angular.forEach($scope.AgeList, function (al) {
            agePara.push(al.Age);
        });

        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetAnalysis",
            dataType: "json",
            data: JSON.stringify(agePara)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.DataColl = res.data.Data;

                var config = {
                    dataSource: $scope.DataColl,
                    canMoveFields: true,
                    dataHeadersLocation: 'columns',
                    width: 1099,
                    height: 611,
                    theme: 'green',
                    toolbar: {
                        visible: true
                    },
                    grandTotal: {
                        rowsvisible: true,
                        columnsvisible: true
                    },
                    subTotal: {
                        visible: true,
                        collapsed: true,
                        collapsible: true
                    },
                    rowSettings: {
                        subTotal: {
                            visible: true,
                            collapsed: true,
                            collapsible: true
                        }
                    },
                    columnSettings: {
                        subTotal: {
                            visible: true,
                            collapsed: true,
                            collapsible: true
                        }
                    },
                    fields: $scope.columnDefs,
                    //rows: ['Manufacturer'],//, 'Category' ],
                    //columns: ['Class', 'Category'],
                    //data: ['Quantity', 'Amount']                       
                };

                var elem = document.getElementById('datatable')

                $scope.pgridwidget = new orb.pgridwidget(config);
                $scope.pgridwidget.render(elem);

            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

       
    }

    $scope.GetDataForPivot= function () {

        $scope.loadingstatus = "running";
        showPleaseWait();
      

        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetAnalysis",
            dataType: "json",
            //data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                var DataColl = res.data.Data;

                var derivers = $.pivotUtilities.derivers;
                var renderers = $.extend($.pivotUtilities.renderers,
                    $.pivotUtilities.plotly_renderers,
                    $.pivotUtilities.gchart_renderers,
                    $.pivotUtilities.export_renderers);

                 $("#datatable1").pivotUI(DataColl, { renderers: renderers });

            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }


    $scope.getClassDetailsColspan = function () {
        var count = 3;

        if ($scope.AcademicConfig.ActiveBatch) count += 1;
        if ($scope.AcademicConfig.ActiveFaculty) count += 1;
        if ($scope.AcademicConfig.ActiveLevel) count += 1;
        if ($scope.AcademicConfig.ActiveClassYear) count += 1;
        if ($scope.AcademicConfig.ActiveSemester) count += 1;

        return count;
    };

    $scope.loadStudentDynamicSummary = function () {
        $http({
            method: 'GET',
            url: base_url + "Academic/Report/GetStudentDynamicSummary",
            dataType: "json",
           /* params: { UserId: 1 }*/
        }).then(function (res) {
            if (res.data.IsSuccess) {
                $scope.StudentSummaryList = res.data.Data;
                var dynamicCols = {};

                angular.forEach($scope.StudentSummaryList, function (item) {
                    angular.forEach(item.DynamicCounts, function (val, key) {
                        dynamicCols[key] = true;
                    });
                });

                var allCols = Object.keys(dynamicCols);
                $scope.GenderColumns = allCols.filter(function (x) {
                    return ["Male", "Female", "Other"].includes(x);
                });
                $scope.IsNewColumns = allCols.filter(function (x) {
                    return x.includes("Student");
                });
                $scope.DisabilityColumns = allCols.filter(function (x) {
                    return x.includes("Disability");
                });
                $scope.CasteColumns = allCols.filter(function (x) {
                    return !$scope.GenderColumns.includes(x)
                        && !$scope.IsNewColumns.includes(x)
                        && !$scope.DisabilityColumns.includes(x);
                });

                $scope.DynamicColumns = [].concat(
                    $scope.GenderColumns,
                    $scope.CasteColumns,
                    $scope.IsNewColumns,
                    $scope.DisabilityColumns
                );

            } else {
                Swal.fire("Error", res.data.ResponseMSG, "error");
            }
        }, function (err) {
            Swal.fire("Error", err.statusText, "error");
        });
    };


});