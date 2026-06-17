
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
};

String.prototype.isEmpty = function () {
    return (this.length === 0 || !this.trim());
};


function len(val) {
    if (val == undefined)
        return 0;

    return val.length;
}

function IsNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function isEmptyObj(v) {
    let type = typeof v;
    if (type === 'undefined') {
        return true;
    }
    if (type === 'boolean') {
        return !v;
    }
    if (v === null) {
        return true;
    }
    if (v === undefined) {
        return true;
    }
    if (v instanceof Array) {
        if (v.length < 1) {
            return true;
        }
    } else if (type === 'string') {
        if (v.length < 1) {
            return true;
        }
        if (v === '0') {
            return true;
        }
    } else if (type === 'object') {
        if (Object.keys(v).length < 1) {
            return true;
        }
    } else if (type === 'number') {
        //if (v === 0) {
        //    return true;
        //}
    }
    return false;
}

const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
const months = ['January','February','March','April','May','June','July','August','September','October','November','December'];
const bsMonths = ["बैशाख", "जेठ", "असार", "सावन", "भदौ", "असोज", "कार्तिक", "मंसिर", "पौष", "माघ", "फागुन", "चैत"];
const bsDays = ["आईत", "सोम", "मंगल", "बुध", "बिही", "शुक्र", "शनि"];

const Operators =
{
    EQUAL: '=',
    LIKE:'like'
};


app.directive('fileModelResize', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        require: 'ngModel',
        scope: {
            afterSelected: '&?',
            fileread: "="
        },
        restrict: 'A',
        link: function (scope, element, attrs, ngModel) {

            resizeImage = function (dtFile) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var img = new Image();
                    img.src = this.result;

                    setTimeout(function () {
                        var canvas = document.createElement("canvas");

                        var MAX_WIDTH = 1024;
                        var MAX_HEIGHT = 768;
                        var width = 750;
                        var height = 600;

                        if (width > height) {
                            if (width > MAX_WIDTH) {
                                height *= MAX_WIDTH / width;
                                width = MAX_WIDTH;
                            }
                        } else {
                            if (height > MAX_HEIGHT) {
                                width *= MAX_HEIGHT / height;
                                height = MAX_HEIGHT;
                            }
                        }
                        canvas.width = width;
                        canvas.height = height;
                        var ctx = canvas.getContext("2d");
                        ctx.drawImage(img, 0, 0, width, height);

                        var dataurl = canvas.toDataURL("image/jpeg");

                        $timeout(function () {
                            scope.$apply(function () {

                                if (scope.fileread == null || !scope.fileread)
                                    scope.fileread = [];

                                scope.fileread.push(dataurl);
                            });
                        });
                    }, 100);
                };
                reader.readAsDataURL(dtFile);
            }


            element.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(element[0].files);
                });
                scope.fileread = [];
                scope.$apply();
                if (element[0].files && element[0].files.length > 0) {
                    angular.forEach(element[0].files, function (fl) {
                        resizeImage(fl);
                    });
                }

                if (scope.afterSelected) {
                    setTimeout(function () {
                        $timeout(function () {
                            scope.$apply(function () {
                                scope.afterSelected();
                            });

                        });
                    }, 1000)
                }


            });
        }
    };
}]);

app.directive('fileModel', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        require: 'ngModel',
        scope: {
            afterSelected: '&?',
            fileread: "=?"
        },
        restrict: 'A',
        link: function (scope, element, attrs, ngModel) {

            element.bind('change', function () {
                scope.$apply(function ()
                {
                    ngModel.$setViewValue(element[0].files);
                });

                if (element[0].files && element[0].files.length>0) {
                    var file = element[0].files[0];
                    var reader = new FileReader();
                    reader.onload = function (evt)
                    {
                        return scope.$apply(function () {
                            scope.fileread = evt.target.result;
                        });
                    };
                    reader.readAsDataURL(file);
                } else
                    scope.fileread = null;

                if (scope.afterSelected) {
                    $timeout(function () {
                        scope.$apply(function () {
                            scope.afterSelected();
                        });

                    });
                }
               
               
            });
        }
    };
}]);

app.directive('fileModelResizes', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        require: 'ngModel',
        scope: {
            afterSelected: '&?',
            fileread: "=?"
        },
        restrict: 'A',
        link: function (scope, element, attrs, ngModel) {

            element.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(element[0].files);
                });

                if (element[0].files && element[0].files.length > 0) {
                    var file = element[0].files[0];
                    var reader = new FileReader();
                    reader.onload = function (evt) {
                        return scope.$apply(function () {
                            scope.fileread = evt.target.result;
                        });
                    };
                    reader.readAsDataURL(file);
                } else
                    scope.fileread = null;

                if (scope.afterSelected) {
                    $timeout(function () {
                        scope.$apply(function () {
                            scope.afterSelected();
                        });

                    });
                }


            });
        }
    };
}]);


app.directive('fileModels', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        require: 'ngModel',
        scope: {
            afterSelected: '&?',
            fileread: "=?",
            beData: "=?"
        },
        restrict: 'A',
        link: function (scope, element, attrs, ngModel) {

            element.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(element[0].files);
                });

                if (scope.afterSelected) {
                    $timeout(function () {
                        scope.$apply(function () {

                            if (scope.beData)
                                scope.afterSelected(scope.beData);
                            else
                                scope.afterSelected();
                        });
                    });
                }
            });
        }
    };
}]);

app.directive('fileDropzone', ['$parse', '$timeout', function ($parse, $timeout) {
    return {      
        restrict: 'A',
        //templateUrl: base_url + 'Scripts/fileChooser.tpl.html',
        require: 'ngModel',
        scope: {
            afterSelected: '&?',
            fileread: "=?"
        },
        link: function (scope, element, attrs, ngModel)
        {
           
            var checkSize,
                isTypeValid,
                processDragOverOrEnter,
                validMimeTypes;

            processDragOverOrEnter = function (event) {
                if (event != null) {
                    event.preventDefault();
                }                

                if (!event.dataTransfer)
                    event = event.originalEvent;

                    event.dataTransfer.effectAllowed = 'copy';
                

                return false;
            };

            validMimeTypes = attrs.fileDropzone;

            checkSize = function (size) {
                var _ref;
                if (((_ref = attrs.maxFileSize) === (void 0) || _ref === '') || (size / 1024) / 1024 < attrs.maxFileSize) {
                    return true;
                } else {
                    alert("File must be smaller than " + attrs.maxFileSize + " MB");
                    return false;
                }
            };

            isTypeValid = function (type) {
                if ((validMimeTypes === (void 0) || validMimeTypes === '') || validMimeTypes.indexOf(type) > -1) {
                    return true;
                } else {
                    alert("Invalid file type.  File must be one of following types " + validMimeTypes);
                    return false;
                }
            };

            $(element).bind('dragover', processDragOverOrEnter);
            $(element).bind('dragenter', processDragOverOrEnter);


            return $(element).bind('drop', function (event) {
               // var file, name, reader, size, type;
                if (event != null) {
                    event.preventDefault();
                }
              
                if (!event.dataTransfer)
                    event = event.originalEvent;

                scope.$apply(function ()
                {
                    ngModel.$setViewValue(event.dataTransfer.files);                    
                });

                if (event.dataTransfer) {
                    if (event.dataTransfer.files && event.dataTransfer.files.length>0) {
                        var file = event.dataTransfer.files[0];

                        var reader = new FileReader();
                        reader.onload = function (evt) {
                            return scope.$apply(function () {
                                scope.fileread = evt.target.result;
                            });
                        };
                        reader.readAsDataURL(file);
                    } else
                        scope.fileread = null;
                } else
                    scope.fileread = null;
                
                

                $timeout(function ()
                {
                    scope.$apply(function ()
                    {
                        scope.afterSelected();
                    });
                    
                });
                //file = event.dataTransfer.files[0];
                //name = file.name;
                //type = file.type;
                //size = file.size;
                //reader.readAsDataURL(file);
                return false;
            });
        }
    };
}]);

app.directive('compile', function ($compile, $timeout) {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            $timeout(function () {
                $compile(elem.contents())(scope);
            });
        }
    };
});
app.filter('unsafe', function ($sce) {

    return function (val) {

        return $sce.trustAsHtml(val);

    };

});
app.filter('formatNumber', function ($filter)
{
    return function (num) {
        return $filter('number')(num, 2);
    }
});

app.filter('formatNumberAC', function ($filter) {
    return function (num) {
        return $filter('number')(Math.abs(num), 2) + ' ' + (num > 0 ? 'DR' : 'CR');
    }
});

app.filter('genderFormat', function ($filter)
{
    return function (num)
    {
        switch (num) {
            case 1:
                return "Male";
            case 2:
                return "Femake";
            case 3:
                return "Other";
            default:
                return "";
        }        
    }
});

app.filter('yesnoFormat', function ($filter) {
    return function (bv)
    {
        if (bv == true)
            return "Yes";
        else
           return "No";
    }
});

app.filter('timeFormat', function ($filter) {
    return function (bv)
    {
        if (bv)
            return $filter('date')(new Date(bv), 'h:mma')
        else
            return "";
    }
});

app.filter('dateFormat', function ($filter) {
    return function (bv) {
        if (bv)
            return $filter('date')(new Date(bv), 'yyyy-MM-dd')
        else
            return "";
    }
});
app.filter('dayFormat', function ($filter) {
    return function (num) {
        switch (num) {
            case 1:
                return "Sunday";
            case 2:
                return "Monday";
            case 3:
                return "Tuesday";
            case 4:
                return "Wednesday";
            case 5:
                return "Thursday";
            case 6:
                return "Friday";
            case 7:
                return "Saturday";
            default:
                return "";
        }
    }
});

app.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
});

app.directive('printTbl', [function ($timeout) {
    return {
        scope: {
            tableId: "=?"
        },
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                $(scope.tableId).printThis(
                    {
                        importCSS: true,
                        formValues: false,
                    }
                );
            });
        }
    };
}]);

app.directive('exportExcel', ['$parse', '$timeout', 'Excel', function ($parse, $timeout, Excel) {
    return {
        scope: {
            tableId: "=?",
            sheetName: "=?"
        },
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var exportHref = Excel.tableToExcel(scope.tableId, scope.sheetName);
                $timeout(function () { location.href = exportHref; }, 100); // trigger download
            });
        }
    };
}]);   


app.directive("htmlEditor", ['$timeout', function ($timeout) {
    return {
        require: 'ngModel',
        link: function (scope, el, attr, ngModel) {

            var onFocus = false;

            el.summernote({
                height: 300,        // set editor height
                minHeight: null,    // set minimum height of editor
                maxHeight: null,    // set maximum height of editor
                focus: false        // set focus to editable area after initializing summernote
            });

            scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (newValue) {
                if (onFocus == false) {
                    $timeout(function () {
                        $(el).summernote('code', ngModel.$modelValue)
                    });
                }
            });

            $(el).on('summernote.focus', function () {
                onFocus = true;
            });
            $(el).on('summernote.blur', function () {
                onFocus = false;
            });

            $(el).on('summernote.change', function (e) {
                var strVal = $(el).summernote('code');
                scope.$apply(function () {
                    ngModel.$setViewValue(strVal);
                });
            });
        }
    };
}]);


app.directive('numberInput', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModelCtrl) {

            ngModelCtrl.$formatters.push(function (modelValue) {
                return setDisplayNumber(modelValue, true);
            });

            // it's best to change the displayed text using elem.val() rather than
            // ngModelCtrl.$setViewValue because the latter will re-trigger the parser
            // and not necessarily in the correct order with the changed value last.
            // see http://radify.io/blog/understanding-ngmodelcontroller-by-example-part-1/
            // for an explanation of how ngModelCtrl works.
            ngModelCtrl.$parsers.push(function (viewValue) {
                setDisplayNumber(viewValue);
                return setModelNumber(viewValue);
            });

            // occasionally the parser chain doesn't run (when the user repeatedly 
            // types the same non-numeric character)
            // for these cases, clean up again half a second later using "keyup"
            // (the parser runs much sooner than keyup, so it's better UX to also do it within parser
            // to give the feeling that the comma is added as they type)
            elem.bind('keyup focus', function () {
                setDisplayNumber(elem.val());
            });

            function setDisplayNumber(val, formatter) {
                var valStr, displayValue;

                if (typeof val === 'undefined') {
                    return 0;
                }

                valStr = val.toString();
                displayValue = valStr.replace(/,/g, '').replace(/[A-Za-z]/g, '');
                displayValue = parseFloat(displayValue);
                displayValue = (!isNaN(displayValue)) ? displayValue.toString() : '';

                // handle leading character -/0
                if (valStr.length === 1 && valStr[0] === '-') {
                    displayValue = valStr[0];
                } else if (valStr.length === 1 && valStr[0] === '0') {
                    displayValue = '';
                } else {
                    displayValue = $filter('number')(displayValue);
                }

                // handle decimal
                if (!attrs.integer) {
                    if (displayValue.indexOf('.') === -1) {
                        if (valStr.slice(-1) === '.') {
                            displayValue += '.';
                        } else if (valStr.slice(-2) === '.0') {
                            displayValue += '.0';
                        } else if (valStr.slice(-3) === '.00') {
                            displayValue += '.00';
                        }
                    } // handle last character 0 after decimal and another number
                    else {
                        if (valStr.slice(-1) === '0') {
                            displayValue += '0';
                        }
                    }
                }

                if (attrs.positive && displayValue[0] === '-') {
                    displayValue = displayValue.substring(1);
                }

                if (typeof formatter !== 'undefined') {
                    return (displayValue === '') ? 0 : displayValue;
                } else {
                    elem.val((displayValue === '0') ? '' : displayValue);
                }
            }

            function setModelNumber(val) {
                var modelNum = val.toString().replace(/,/g, '').replace(/[A-Za-z]/g, '');
                modelNum = parseFloat(modelNum);
                modelNum = (!isNaN(modelNum)) ? modelNum : 0;
                if (modelNum.toString().indexOf('.') !== -1) {
                    modelNum = Math.round((modelNum + 0.00001) * 100) / 100;
                }
                if (attrs.positive) {
                    modelNum = Math.abs(modelNum);
                }
                return modelNum;
            }
        }
    };
});
app.directive("allLedger", ['$http', '$timeout','$filter', function ($http,$timeout,$filter) {

    function link(scope, element, attrs, ngModel)
    {

        var placeholder = "** Select Ledger **";
        var showDetails = false;

        if (attrs.placeholder)
            placeholder = attrs.placeholder;

        if (attrs.showdetails == true || attrs.showdetails=="true" )
            showDetails = true;

        $(element).select2({
            placeholder: placeholder,
            allowClear: true,
            width: '100%',
            multiple: false,
            minimumInputLength: 1,
            minimumResultsForSearch: 10,
            //triggerChange: true,
            ajax: {
                quietMillis: 300,
                url: base_url + "Global/GetAllLedger",
                dataType: "json",
                type: "GET",
                data: function (params)
                {
                    var queryParameters =
                    {
                        Top: 10,
                        ColName: "Led.Name",
                        Operator:"like",
                        ForTransaction: true,
                        OrderByCol: "Led.Name",
                        ColValue: params.term
                    }
                    return queryParameters;
                },
                processResults: function (res)
                {
                    if (res.IsSuccess == false) {
                        alert(res.ResponseMSG);
                        return;
                    }
                        
                    //return { results: data };
                    return {
                        results: $.map(res.Data, function (item)
                        {
                            scope.ledgerDetail = item;
                            ngModel.$setViewValue(item.LedgerId);
                            return {
                                text: item.Name + ' - ' + item.Alias + ' - ' + item.Code,
                                id: item.LedgerId,
                                data: item
                            }
                        })
                    };
                }
            },
            initSelection: function (element, callback) {
                //if(!ngModel.$modelValue)
                //    return;

                attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
                    var viewWatch = scope.$watch(value, function (newValue) { // Watch given path for changes

                        if (ngModel.$modelValue) {
                            var id = ngModel.$modelValue;

                            if (id != null && id !== "" && id > 0)
                            {
                                var queryParameters = {
                                    Top: 1,
                                    ColName: "Led.LedgerId",
                                    Operator: "=",
                                    ForTransaction: true,
                                    OrderByCol: "Led.LedgerId",
                                    ColValue: id
                                };

                                return $.ajax(
                                    base_url + "Global/GetAllLedger",
                                    {
                                        dataType: "json",
                                        type: "GET",
                                        data: queryParameters
                                    }).done(function (res)
                                    {
                                        if (res.IsSuccess == false) {
                                            alert(res.ResponseMSG);
                                        }
                                        else if (res.Data.length > 0) {
                                            var item = res.Data[0];
                                            var tData =
                                            {
                                                text: item.Name + ' - ' + item.Alias + ' - ' + item.Code,
                                                id: item.LedgerId,
                                                data: item
                                            }
                                            viewWatch();
                                            
                                            callback(tData);
                                        } else
                                            alert("No Data Found on Load");

                                    });
                            }                           
                        }

                    });
                });

            }
        });


        $(element).on("select2:unselecting", function (e)
        {
            scope.ledgerDetail = {};
            scope.sideBarData = [];
            ngModel.$setViewValue(null);
        });

        
        $(element).on("select2:open", function (e) {

            //$('#sidebarzz').toggleClass('active');
        });
        $(element).on("select2:close", function (e) {            
            //$('#sidebarzz').toggleClass('active');
        });
        $(element).on("change", function (e)
        {
            scope.sideBarData = [];

            var selectedData = $(element).select2('data');

            if (selectedData && selectedData.length > 0) {
                var selectedLedger = selectedData[0].data;
                ngModel.$setViewValue(selectedLedger.LedgerId);
                scope.ledgerDetail = selectedLedger;
                if (showDetails) {
                    $http({
                        method: 'GET',
                        url: base_url + "Global/GetLedgerDetail?LedgerId=" + selectedLedger.LedgerId + "&VoucherType=" + VoucherType,
                        dataType: "json"
                    }).then(function (res) {
                        if (res.data.IsSuccess && res.data.Data)
                        {
                            var led = res.data.Data;
                            scope.ledgerDetail = led;                           
                            scope.sideBarData = [];
                            scope.sideBarData.push({ text: 'Group Name', value: led.GroupName }, { text: 'Ledger Code', value: led.Code }, { text: 'Pan Vat No', value: led.PanVat }, { text: 'Address', value: led.Address }, { text: 'Closing Balance', value: $filter('formatNumberAC')(led.Closing) }, { text: 'Credit Limit', value: $filter('formatNumber')(led.CreditLimitAmt) }, { text: 'PDC Cheque', value: $filter('formatNumber')(led.PDCAmt) }, { text: 'ODC Cheque', value: $filter('formatNumber')(led.ODCAmt) }, { text: 'Total Sales', value: $filter('formatNumber')(led.TranAmt) }, { text: 'Last Sales Date', value: led.LastTranDateBS }, { text: 'B.G. Amount', value: $filter('formatNumber')(led.BGAmt) }, { text: 'Opening', value: $filter('formatNumberAC')(led.Opening) }, { text: 'Total Dr', value: $filter('formatNumber')(led.DrAmt) }, { text: 'Total Cr', value: $filter('formatNumber')(led.CrAmt) }, { text: 'EmailId', value: led.EmailId }, { text: 'Mobile No.', value: led.MobileNo1 })

                            $timeout(function () {
                                scope.confirmAction();
                            });

                        } else
                            alert(res.data.ResponseMSG);

                    }, function (reason) {
                        alert('Failed' + reason);
                    });
                }

            } else {

                scope.ledgerDetail = {};
                scope.sideBarData = [];
                ngModel.$setViewValue(null);
            }
                

        });

    }

    return {
        require: 'ngModel',
        link: link,
        scope: {
            ledgerDetail: '=',
            sideBarData:'=',
            confirmAction: '&'
                }
    };
}]);

app.directive("allProduct", ['$http', '$timeout', '$filter', function ($http, $timeout, $filter) {

    function link(scope, element, attrs, ngModel)
    {

        var placeholder = "** Select Product **";
        var showDetails = false;

        if (attrs.placeholder)
            placeholder = attrs.placeholder;

        if (attrs.showdetails == true || attrs.showdetails == "true")
            showDetails = true;

        $(element).select2({
            placeholder: placeholder,
            allowClear: true,
            width: '100%',
            multiple: false,
            minimumInputLength: 1,
            minimumResultsForSearch: 10,
            //triggerChange: true,
            ajax: {
                quietMillis: 300,
                url: base_url + "Global/GetAllProduct",
                dataType: "json",
                type: "GET",
                data: function (params) {
                    var queryParameters =
                    {
                        Top: 10,
                        ColName: "P.Name",
                        Operator: "like",                        
                        OrderByCol: "P.Name",
                        ColValue: params.term
                    }
                    return queryParameters;
                },
                processResults: function (res) {
                    if (res.IsSuccess == false) {
                        alert(res.ResponseMSG);
                        return;
                    }
                    return {
                        results: $.map(res.Data, function (item) {
                            scope.productDetail = item;
                            ngModel.$setViewValue(item.ProductId);
                            return {
                                text: item.Name + ' - ' + item.Alias + ' - ' + item.Code,
                                id: item.ProductId,
                                data: item
                            }
                        })
                    };
                }
            },
            initSelection: function (element, callback) {
                
                attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
                    var viewWatch = scope.$watch(value, function (newValue) { // Watch given path for changes

                        if (ngModel.$modelValue && value!=newValue) {
                            var id = ngModel.$modelValue;

                            if (id != null && id !== "" && id > 0) {
                                var queryParameters = {
                                    Top: 1,
                                    ColName: "P.ProductId",
                                    Operator: "=",                                    
                                    OrderByCol: "P.ProductId",
                                    ColValue: id
                                };

                                return $.ajax(
                                    base_url + "Global/GetAllProduct",
                                    {
                                        dataType: "json",
                                        type: "GET",
                                        data: queryParameters
                                    }).done(function (res) {
                                        if (res.IsSuccess == false) {
                                            alert(res.ResponseMSG);
                                        }
                                        else if (res.Data.length > 0) {
                                            var item = res.Data[0];
                                            var tData =
                                            {
                                                text: item.Name + ' - ' + item.Alias + ' - ' + item.Code,
                                                id: item.ProductId,
                                                data: item
                                            }
                                            viewWatch();

                                            callback(tData);
                                        } else
                                            alert("No Data Found on Load");

                                    });
                            }
                        }

                    });
                });

            }
        });


        $(element).on("select2:unselecting", function (e) {
            scope.productDetail = {};
            scope.sideBarData = [];
            ngModel.$setViewValue(null);
        });

        $(element).on("select2:open", function (e) {

            //$('#sidebarzz').toggleClass('active');
        });
        $(element).on("select2:close", function (e) {
            //$('#sidebarzz').toggleClass('active');
        });
        $(element).on("change", function (e) {

            scope.sideBarData = [];

            var selectedData = $(element).select2('data');

            if (selectedData && selectedData.length > 0) {
                var selectedProduct = selectedData[0].data;
                ngModel.$setViewValue(selectedProduct.ProductId);
                scope.productDetail = selectedProduct;
                if (showDetails) {
                    $http({
                        method: 'GET',
                        url: base_url + "Global/GetProductDetail?ProductId=" + selectedProduct.ProductId + "&VoucherType=" + VoucherType,
                        dataType: "json"
                    }).then(function (res) {
                        if (res.data.IsSuccess && res.data.Data) {
                            var pro = res.data.Data;
                            scope.productDetail = pro;
                            scope.sideBarData = [];
                            scope.sideBarData.push({ text: 'Group Name', value: pro.ProductGroup }, { text: 'Opening', value: $filter('formatNumber')(pro.OpeningQty) })

                            $timeout(function () {
                                scope.confirmAction();
                            });

                        } else
                            alert(res.data.ResponseMSG);

                    }, function (reason) {
                        alert('Failed' + reason);
                    });
                }

            } else {

                scope.productDetail = {};
                scope.sideBarData = [];
                ngModel.$setViewValue(null);
            }


        });

    }

    return {
        require: 'ngModel',
        link: link,
        scope: {
            productDetail: '=',            
            sideBarData: '=',
            confirmAction: '&'
        }
    };
}]);

app.directive("voucherDate", function () {

    function link(scope, element, attrs, ngModel) {

        var newDate = null;

        attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
            scope.$watch(value, function (newValue) { // Watch given path for changes

                if (ngModel.$modelValue) {
                    if (!ngModel.$modelValue.dateAD) {
                        if (ngModel.$modelValue)
                            newDate = new Date(ngModel.$modelValue);
                        else
                            newDate = new Date();

                        if (dtPicker) {
                            dtPicker.setDate(newDate);
                            var dateDetails = {
                                dateAD: newDate,
                                NY: dtPicker.dateSelectedBS.bsYear,
                                NM: dtPicker.dateSelectedBS.bsMonth + 1,
                                ND: dtPicker.dateSelectedBS.bsDate
                            };
                            ngModel.$setViewValue(dateDetails);
                        }
                    }

                }

            });
        });


        if (ngModel.$modelValue)
            newDate = new Date(ngModel.$modelValue);
        else
            newDate = new Date();

        var dateFormat = "%y-%m-%d";
        var dateFormatMask = "9999-99-99";
        var dateFormatMaskPlaceholder = "yyyy-mm-dd";
        var dtPicker = datepicker(element[0], {
            dateStyle: 2, startDate: newDate, dateSelected: newDate, dateFormat: dateFormat,
            onDateChange: function (el, date, dateBS) {
                if (date && dateBS) {
                    var dateDetails = {
                        dateAD: date,
                        NY: dateBS.bsYear,
                        NM: dateBS.bsMonth + 1,
                        ND: dateBS.bsDate
                    };
                    ngModel.$setViewValue(dateDetails);   // Update the `$viewValue`        
                } else
                    ngModel.$setViewValue(null);

            }
        });

        $(element).on("change", function (e) {
            if (e.currentTarget.value) {
                if (e.currentTarget.value.length == 0) {
                    ngModel.$setViewValue(null);
                }
            } else
                ngModel.$setViewValue(null);

            console.log(ngModel.$modelValue);
        });
        $(element).inputmask(dateFormatMask, {
            "placeholder": dateFormatMaskPlaceholder, "oncomplete": function (e) {
                var event = document.createEvent('HTMLEvents');
                event.initEvent('input', true, true);
                e.currentTarget.dispatchEvent(event);
                $(this).trigger('change');
            }
        });

    }

    return {
        require: 'ngModel',
        link: link
    };
});

app.directive("voucherDateAd", function () {

    function link(scope, element, attrs, ngModel) {

        var newDate = null;

        attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
            scope.$watch(value, function (newValue) {
                // Watch given path for changes
                if (ngModel.$modelValue) {
                    newDate = new Date(ngModel.$modelValue);
                    if (dtPicker && dtPicker.dateSelected!=newValue) {
                        dtPicker.setDate(newDate);
                    }
                }
            });
        });

        if (ngModel.$modelValue)
            newDate = new Date(ngModel.$modelValue);
        else
            newDate = null;

        var dateFormat = "%y-%m-%d";
        var dateFormatMask = "9999-99-99";
        var dateFormatMaskPlaceholder = "yyyy-mm-dd";
        var dtPicker = datepicker(element[0],
            {
                dateStyle: 1, startDate: newDate, dateSelected: newDate, dateFormat: dateFormat,
                //parent: element[0].parentElement.parentElement,
                onDateChange: function (el, date, dateBS) {
                    if (date && dateBS) {
                        ngModel.$setViewValue(date);   // Update the `$viewValue`        
                    } else
                        ngModel.$setViewValue(null);
                }
            });

        $(element).on("change", function (e) {
            if (e.currentTarget.value) {
                if (e.currentTarget.value.length == 0) {
                    ngModel.$setViewValue(null);
                }
            } else
                ngModel.$setViewValue(null);

            console.log(ngModel.$modelValue);
        });
        $(element).inputmask(dateFormatMask, {
            "placeholder": dateFormatMaskPlaceholder, "oncomplete": function (e) {
                var event = document.createEvent('HTMLEvents');
                event.initEvent('input', true, true);
                e.currentTarget.dispatchEvent(event);
                $(this).trigger('onDateChange');
            }
        });



    }

    return {
        require: 'ngModel',
        link: link
    };
});

app.directive("dateControl", function () {

    function link(scope, element, attrs, ngModel)
    {

        var newDate = null;

        attrs.$observe('ngModel', function (value)
        { // Got ng-model bind path here
            scope.$watch(value, function (newValue)
            { // Watch given path for changes
                if (value != newValue) {
                    if (ngModel.$modelValue && dtPicker) {
                        newDate = new Date(dtPicker.dateSelected);
                    } else
                        newDate = new Date();

                    if (dtPicker && newDate) {
                        // dtPicker.setDate(newDate);
                        ngModel.$setViewValue(newDate);
                    }
                }                
            });
        });

        if (ngModel.$modelValue)
            newDate = new Date(ngModel.$modelValue);
        else
            newDate = new Date();

        var dateFormat = "%y-%m-%d";
        var dateFormatMask = "9999-99-99";
        var dateFormatMaskPlaceholder = "yyyy-mm-dd";
        var dtPicker = datepicker(element[0], {
            dateStyle: 2, startDate: newDate, dateSelected: newDate, dateFormat: dateFormat,
            onDateChange: function (el, date, dateBS) {
                if (date && dateBS) {                   
                    ngModel.$setViewValue(date);   // Update the `$viewValue`        
                } else
                    ngModel.$setViewValue(null);

            }
        });

        $(element).on("change", function (e) {
            if (e.currentTarget.value) {
                if (e.currentTarget.value.length == 0) {
                    ngModel.$setViewValue(null);
                }
            } else
                ngModel.$setViewValue(null);

            console.log(ngModel.$modelValue);
        });
        $(element).inputmask(dateFormatMask, {
            "placeholder": dateFormatMaskPlaceholder, "oncomplete": function (e) {
                var event = document.createEvent('HTMLEvents');
                event.initEvent('input', true, true);
                e.currentTarget.dispatchEvent(event);
                $(this).trigger('change');
            }
        });

    }

    return {
        require: 'ngModel',
        link: link
        //scope: {
        //    AD_Date: '=',
        //    BS_Date: '=',
        //    Date_Det: '='
        //}
    };
});

app.directive("allStudent", ['$http', '$timeout', '$filter', function ($http, $timeout, $filter) {

    function link(scope, element, attrs, ngModel) {

        var placeholder = "** Select Student **";
        var showDetails = false;

        if (attrs.placeholder)
            placeholder = attrs.placeholder;

        if (attrs.showdetails == true || attrs.showdetails == "true")
            showDetails = true;

        $(element).select2({
            placeholder: placeholder,
            allowClear: true,
            width: '100%',
            multiple: false,
            minimumInputLength: 1,
            minimumResultsForSearch: 10,
            //triggerChange: true,
            ajax: {
                quietMillis: 300,
                url: base_url + "Global/GetAllStudent",
                dataType: "json",
                type: "GET",
                data: function (params) {
                    var queryParameters =
                    {
                        Top: 10,
                        ColName: scope.searchBy,
                        Operator: "like",
                        ForTransaction: true,
                        OrderByCol: "ST.Name",
                        ColValue: params.term
                    }
                    return queryParameters;
                },
                processResults: function (res) {
                    if (res.IsSuccess == false) {
                        alert(res.ResponseMSG);
                        return;
                    }

                    //return { results: data };
                    return {
                        results: $.map(res.Data, function (item) {
                            scope.studentDetail = item;
                            ngModel.$setViewValue(item.StudentId);
                            return {
                                text: item.Name + ' - ' + item.ClassName + ' - ' + item.RollNo,
                                id: item.StudentId,
                                data: item
                            }
                        })
                    };
                }
            },
            initSelection: function (element, callback) {
                //if(!ngModel.$modelValue)
                //    return;

                attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
                    var viewWatch = scope.$watch(value, function (newValue) { // Watch given path for changes

                        if (ngModel.$modelValue) {
                            var id = ngModel.$modelValue;

                            if (id != null && id !== "" && id > 0) {
                                var queryParameters = {
                                    Top: 1,
                                    ColName: "ST.StudentId",
                                    Operator: "=",
                                    ForTransaction: true,
                                    OrderByCol: "ST.StudentId",
                                    ColValue: id
                                };

                                return $.ajax(
                                    base_url + "Global/GetAllStudent",
                                    {
                                        dataType: "json",
                                        type: "GET",
                                        data: queryParameters
                                    }).done(function (res) {
                                        if (res.IsSuccess == false) {
                                            alert(res.ResponseMSG);
                                        }
                                        else if (res.Data.length > 0) {
                                            var item = res.Data[0];
                                            var tData =
                                            {
                                                text: item.Name + ' - ' + item.ClassName + ' - ' + item.RollNo,
                                                id: item.StudentId,
                                                data: item
                                            }
                                            viewWatch();

                                            callback(tData);
                                        } else
                                            alert("No Data Found on Load");

                                    });
                            }
                        }

                    });
                });

            }
        });


        $(element).on("select2:unselecting", function (e) {
            scope.studentDetail = {};
            scope.sideBarData = [];
            ngModel.$setViewValue(null);
        });

        $(element).on("change", function (e) {
            scope.sideBarData = [];

            var selectedData = $(element).select2('data');

            if (selectedData && selectedData.length > 0) {
                var selectedStudent = selectedData[0].data;
                ngModel.$setViewValue(selectedStudent.StudentId);
                scope.studentDetail = selectedStudent;
                //if (showDetails) {
                //    $http({
                //        method: 'GET',
                //        url: base_url + "Global/GetLedgerDetail?LedgerId=" + selectedLedger.LedgerId + "&VoucherType=" + VoucherType,
                //        dataType: "json"
                //    }).then(function (res) {
                //        if (res.data.IsSuccess && res.data.Data) {
                //            var led = res.data.Data;
                //            scope.ledgerDetail = led;
                //            scope.sideBarData = [];
                //            scope.sideBarData.push({ text: 'Group Name', value: led.GroupName }, { text: 'Ledger Code', value: led.Code }, { text: 'Pan Vat No', value: led.PanVat }, { text: 'Address', value: led.Address }, { text: 'Closing Balance', value: $filter('formatNumberAC')(led.Closing) }, { text: 'Credit Limit', value: $filter('formatNumber')(led.CreditLimitAmt) }, { text: 'PDC Cheque', value: $filter('formatNumber')(led.PDCAmt) }, { text: 'ODC Cheque', value: $filter('formatNumber')(led.ODCAmt) }, { text: 'Total Sales', value: $filter('formatNumber')(led.TranAmt) }, { text: 'Last Sales Date', value: led.LastTranDateBS }, { text: 'B.G. Amount', value: $filter('formatNumber')(led.BGAmt) }, { text: 'Opening', value: $filter('formatNumberAC')(led.Opening) }, { text: 'Total Dr', value: $filter('formatNumber')(led.DrAmt) }, { text: 'Total Cr', value: $filter('formatNumber')(led.CrAmt) }, { text: 'EmailId', value: led.EmailId }, { text: 'Mobile No.', value: led.MobileNo1 })

                //            $timeout(function () {
                //                scope.confirmAction();
                //            });

                //        } else
                //            alert(res.data.ResponseMSG);

                //    }, function (reason) {
                //        alert('Failed' + reason);
                //    });
                //}

            } else {

                scope.studentDetail = {};
                scope.sideBarData = [];
                ngModel.$setViewValue(null);
            }

        });
    }
    return {
        require: 'ngModel',
        link: link,
        scope: {
            studentDetail: '=',
            sideBarData: '=',
            searchBy:'=',
            confirmAction: '&'
        }
    };
}]);

app.directive("allEmployee", ['$http', '$timeout', '$filter', function ($http, $timeout, $filter) {

    function link(scope, element, attrs, ngModel) {

        var placeholder = "** Select Employee **";
        var showDetails = false;

        if (attrs.placeholder)
            placeholder = attrs.placeholder;

        if (attrs.showdetails == true || attrs.showdetails == "true")
            showDetails = true;

        $(element).select2({
            placeholder: placeholder,
            allowClear: true,
            width: '100%',
            multiple: false,
            minimumInputLength: 1,
            minimumResultsForSearch: 10,
            //triggerChange: true,
            ajax: {
                quietMillis: 300,
                url: base_url + "Global/GetAllEmployee",
                dataType: "json",
                type: "GET",
                data: function (params) {
                    var queryParameters =
                    {
                        Top: 10,
                        ColName: scope.searchBy,
                        Operator: "like",
                        ForTransaction: true,
                        OrderByCol: "E.Name",
                        ColValue: params.term
                    }
                    return queryParameters;
                },
                processResults: function (res) {
                    if (res.IsSuccess == false) {
                        alert(res.ResponseMSG);
                        return;
                    }

                    //return { results: data };
                    return {
                        results: $.map(res.Data, function (item) {
                            scope.EmployeeDetail = item;
                            ngModel.$setViewValue(item.EmployeeId);
                            return {
                                text: item.Name + ' - ' + item.Code + ' - ' + item.MobileNo,
                                id: item.EmployeeId,
                                data: item
                            }
                        })
                    };
                }
            },
            initSelection: function (element, callback) {
                //if(!ngModel.$modelValue)
                //    return;

                attrs.$observe('ngModel', function (value) { // Got ng-model bind path here
                    var viewWatch = scope.$watch(value, function (newValue) { // Watch given path for changes

                        if (ngModel.$modelValue) {
                            var id = ngModel.$modelValue;

                            if (id != null && id !== "" && id > 0) {
                                var queryParameters = {
                                    Top: 1,
                                    ColName: "E.EmployeeId",
                                    Operator: "=",
                                    ForTransaction: true,
                                    OrderByCol: "E.EmployeeId",
                                    ColValue: id
                                };

                                return $.ajax(
                                    base_url + "Global/GetAllEmployee",
                                    {
                                        dataType: "json",
                                        type: "GET",
                                        data: queryParameters
                                    }).done(function (res) {
                                        if (res.IsSuccess == false) {
                                            alert(res.ResponseMSG);
                                        }
                                        else if (res.Data.length > 0) {
                                            var item = res.Data[0];
                                            var tData =
                                            {
                                                text: item.Name + ' - ' + item.Code + ' - ' + item.MobileNo,
                                                id: item.EmployeeId,
                                                data: item
                                            }
                                            viewWatch();

                                            callback(tData);
                                        } else
                                            alert("No Data Found on Load");

                                    });
                            }
                        }

                    });
                });

            }
        });


        $(element).on("select2:unselecting", function (e) {
            scope.EmployeeDetail = {};
            scope.sideBarData = [];
            ngModel.$setViewValue(null);
        });

        $(element).on("change", function (e) {
            scope.sideBarData = [];

            var selectedData = $(element).select2('data');

            if (selectedData && selectedData.length > 0) {
                var selectedEmployee = selectedData[0].data;
                ngModel.$setViewValue(selectedEmployee.EmployeeId);
                scope.EmployeeDetail = selectedEmployee;
                //if (showDetails) {
                //    $http({
                //        method: 'GET',
                //        url: base_url + "Global/GetLedgerDetail?LedgerId=" + selectedLedger.LedgerId + "&VoucherType=" + VoucherType,
                //        dataType: "json"
                //    }).then(function (res) {
                //        if (res.data.IsSuccess && res.data.Data) {
                //            var led = res.data.Data;
                //            scope.ledgerDetail = led;
                //            scope.sideBarData = [];
                //            scope.sideBarData.push({ text: 'Group Name', value: led.GroupName }, { text: 'Ledger Code', value: led.Code }, { text: 'Pan Vat No', value: led.PanVat }, { text: 'Address', value: led.Address }, { text: 'Closing Balance', value: $filter('formatNumberAC')(led.Closing) }, { text: 'Credit Limit', value: $filter('formatNumber')(led.CreditLimitAmt) }, { text: 'PDC Cheque', value: $filter('formatNumber')(led.PDCAmt) }, { text: 'ODC Cheque', value: $filter('formatNumber')(led.ODCAmt) }, { text: 'Total Sales', value: $filter('formatNumber')(led.TranAmt) }, { text: 'Last Sales Date', value: led.LastTranDateBS }, { text: 'B.G. Amount', value: $filter('formatNumber')(led.BGAmt) }, { text: 'Opening', value: $filter('formatNumberAC')(led.Opening) }, { text: 'Total Dr', value: $filter('formatNumber')(led.DrAmt) }, { text: 'Total Cr', value: $filter('formatNumber')(led.CrAmt) }, { text: 'EmailId', value: led.EmailId }, { text: 'Mobile No.', value: led.MobileNo1 })

                //            $timeout(function () {
                //                scope.confirmAction();
                //            });

                //        } else
                //            alert(res.data.ResponseMSG);

                //    }, function (reason) {
                //        alert('Failed' + reason);
                //    });
                //}

            } else {

                scope.EmployeeDetail = {};
                scope.sideBarData = [];
                ngModel.$setViewValue(null);
            }

        });
    }
    return {
        require: 'ngModel',
        link: link,
        scope: {
            EmployeeDetail: '=',
            sideBarData: '=',
            searchBy: '=',
            confirmAction: '&'
        }
    };
}]);


app.directive('datePickerAd', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        scope: {
            afterDays: '=?',
            beforeDays: '=?',
            dateDetail: '=?',
            confirmAction: '&'
        },
        link: function (scope, element, attrs, ngModelController) {

            // Private variables
            var datepickerFormat = 'dd-mm-yyyy',
                momentFormat = 'DD-MM-YYYY',
                datepicker,
                elPicker;

            // Init date picker and get objects http://bootstrap-datepicker.readthedocs.org/en/release/index.html
            datepicker = element.datepicker({
                autoclose: true,
                keyboardNavigation: false,
                todayHighlight: true,
                format: datepickerFormat
            });
            elPicker = datepicker.data('datepicker').picker;

            var dateFormatMask = "99-99-9999";
            var dateFormatMaskPlaceholder = "dd-mm-yyyy";
            $(element).inputmask(dateFormatMask, {
                "placeholder": dateFormatMaskPlaceholder, "oncomplete": function (e) {
                    var event = document.createEvent('HTMLEvents');
                    event.initEvent('input', true, true);
                    e.currentTarget.dispatchEvent(event);
                    $(this).trigger('change');
                }
            });

            // Adjust offset on show
            datepicker.on('show', function (evt) {
                elPicker.css('left', parseInt(elPicker.css('left')) + +attrs.offsetX);
                elPicker.css('top', parseInt(elPicker.css('top')) + +attrs.offsetY);
            });

            // Only watch and format if ng-model is present https://docs.angularjs.org/api/ng/type/ngModel.NgModelController
            if (ngModelController) {
                // So we can maintain time
                var lastModelValueMoment;

                ngModelController.$formatters.push(function (modelValue) {
                    //
                    // Date -> String
                    //

                    // Get view value (String) from model value (Date)
                    var viewValue,
                        m = moment(modelValue);
                    if (modelValue && m.isValid()) {
                        // Valid date obj in model
                        lastModelValueMoment = m.clone(); // Save date (so we can restore time later)
                        viewValue = m.format(momentFormat);
                    } else {
                        // Invalid date obj in model
                        lastModelValueMoment = undefined;
                        viewValue = undefined;
                    }

                    // Update picker
                    element.datepicker('update', viewValue);

                    if (!scope.dateDetail) {
                        var dt = modelValue;
                        // var bsDateDet = NepaliFunctions.AD2BS({ year: dt.getFullYear(), month: dt.getMonth() + 1, day: dt.getDate() });
                        //var bsDate = bsDateDet.year + '-' + bsDateDet.month + '-' + bsDateDet.day;
                        var bsDate = DateFormatAD(dt);
                        scope.dateDetail = {
                            dateAD: dt,
                            dateBS: bsDate,
                            NY: dt.getFullYear(),
                            NM: dt.getMonth() + 1,
                            ND: dt.getDate(),
                            age: getDOBAge(dt)
                        };
                    }


                    // Update view
                    return viewValue;
                });

                ngModelController.$parsers.push(function (viewValue) {
                    //
                    // String -> Date
                    //
                    scope.dateDetail = {};

                    // Get model value (Date) from view value (String)
                    var modelValue,
                        m = moment(viewValue, momentFormat, true);
                    if (viewValue && m.isValid()) {
                        // Valid date string in view
                        if (lastModelValueMoment) { // Restore time
                            m.hour(lastModelValueMoment.hour());
                            m.minute(lastModelValueMoment.minute());
                            m.second(lastModelValueMoment.second());
                            m.millisecond(lastModelValueMoment.millisecond());
                        }
                        modelValue = m.toDate();

                        var dt = modelValue;
                        // var bsDateDet = NepaliFunctions.AD2BS({ year: dt.getFullYear(), month: dt.getMonth() + 1, day: dt.getDate() });
                        //var bsDate = bsDateDet.year + '-' + bsDateDet.month + '-' + bsDateDet.day;
                        var bsDate = DateFormatAD(dt);
                        scope.dateDetail = {
                            dateAD: dt,
                            dateBS: bsDate,
                            NY: dt.getFullYear(),
                            NM: dt.getMonth() + 1,
                            ND: dt.getDate(),
                            age: getDOBAge(dt)
                        };

                        scope.confirmAction();

                    } else {
                        // Invalid date string in view
                        modelValue = undefined;
                    }

                    // Update model
                    return modelValue;
                });

                datepicker.on('changeDate', function (evt) {
                    // Only update if it's NOT an <input> (if it's an <input> the datepicker plugin trys to cast the val to a Date)
                    if (evt.target.tagName !== 'INPUT') {
                        ngModelController.$setViewValue(moment(evt.date).format(momentFormat)); // $seViewValue basically calls the $parser above so we need to pass a string date value in
                        ngModelController.$render();
                    }

                    var dt = evt.date;
                    //var bsDateDet = NepaliFunctions.AD2BS({ year: dt.getFullYear(), month: dt.getMonth() + 1, day: dt.getDate() });
                    // var bsDate = bsDateDet.year + '-' + bsDateDet.month + '-' + bsDateDet.day;
                    var bsDate = DateFormatAD(dt);
                    scope.dateDetail = {
                        dateAD: dt,
                        dateBS: bsDate,
                        NY: dt.getFullYear(),
                        NM: dt.getMonth() + 1,
                        ND: dt.getDate(),
                        age: getDOBAge(dt)
                    };

                    scope.confirmAction();

                });
            }

        }
    };
});



app.directive("datePicker", ['$http', '$timeout', '$filter', function ($http, $timeout, $filter) {

    function link(scope, ele, attrs, ngModel) {

        scope.$watch(function () {
            return ngModel.$modelValue;
        }, function (newValue) {
            if (ngModel.$modelValue) {
                var dt = ngModel.$modelValue;

                if (moment.isDate(dt) == true) {
                    var bsDateDet = NepaliFunctions.AD2BS({ year: dt.getFullYear(), month: dt.getMonth() + 1, day: dt.getDate() });

                    var bsDate = bsDateDet.year + '-' + bsDateDet.month.toString().padStart(2, '0') + '-' + bsDateDet.day.toString().padStart(2, '0');
                    element.value = bsDate;

                    $timeout(function () {
                        scope.$apply(function () {

                            var fInit = false;

                            if (!scope.dateDetail) {
                                fInit = true;
                                scope.dateDetail = {};
                            }


                            scope.dateDetail = {
                                dateAD: dt,
                                dateBS: bsDate,
                                NY: bsDateDet.year,
                                NM: bsDateDet.month,
                                ND: bsDateDet.day
                            };

                            if (fInit == true)
                                scope.confirmAction();
                        });
                    });
                }
            }
        });

        var element = ele[0];



        element.nepaliDatePicker({
            disableDaysAfter: (scope.afterDays ? scope.afterDays : null),
            disableDaysBefore: (scope.beforeDays ? scope.beforeDays : null),
            ndpYear: true,
            ndpMonth: true,
            ndpYearCount: 10,
            onChange: function () {
                $timeout(function () {
                    scope.$apply(function () {
                        if (!scope.dateDetail)
                            scope.dateDetail = {};

                        var dates = element.value.split('-');
                        var adDateDet = NepaliFunctions.BS2AD({
                            year: parseInt(dates[0]), month: parseInt(dates[1]), day: parseInt(dates[2])
                        });

                        var adDate = new Date(adDateDet.year, adDateDet.month - 1, adDateDet.day);

                        scope.dateDetail = {
                            dateAD: adDate,
                            dateBS: element.value,
                            NY: parseInt(dates[0]),
                            NM: parseInt(dates[1]),
                            ND: parseInt(dates[2])
                        };

                        //ngModel.$setViewValue(adDate);
                        ngModel.$setViewValue(element.value);

                        $timeout(function () {
                            scope.confirmAction();
                        });

                    });
                });

            }
        });

        var dateFormatMask = "9999-99-99";
        var dateFormatMaskPlaceholder = "yyyy-mm-dd";
        $(element).inputmask(dateFormatMask, {
            "placeholder": dateFormatMaskPlaceholder, "oncomplete": function (e) {
                var event = document.createEvent('HTMLEvents');
                event.initEvent('input', true, true);
                e.currentTarget.dispatchEvent(event);
                $(this).trigger('change');
            }
        });

        element.addEventListener("blur", function () {
            if (!scope.dateDetail)
                scope.dateDetail = {};

            var dates = element.value.split('-');
            var adDateDet = NepaliFunctions.BS2AD({
                year: parseInt(dates[0]), month: parseInt(dates[1]), day: parseInt(dates[2])
            });

            var adDate = new Date(adDateDet.year, adDateDet.month - 1, adDateDet.day);

            scope.dateDetail = {
                dateAD: adDate,
                dateBS: element.value,
                NY: parseInt(dates[0]),
                NM: parseInt(dates[1]),
                ND: parseInt(dates[2])
            };

        }, true);

    }

    return {
        require: 'ngModel',
        link: link,
        scope: {
            afterDays: '=?',
            beforeDays: '=?',
            dateDetail: '=',
            confirmAction: '&'
        }
    };
}]);

function showPleaseWait() {
    $("#pleaseWaitDialog").modal("show");
}

function hidePleaseWait() {
    $("#pleaseWaitDialog").modal("hide");
}

function GetStateList() {
    var dataColl = [
        { id: 1, text: 'Province No.1' },
        { id: 2, text: 'Province No.2' },
        { id: 3, text: 'Province No.3' },
        { id: 4, text: 'Province No.4' },
        { id: 5, text: 'Province No.5' },
        { id: 6, text: 'Province No.6' },
        { id: 7, text: 'Province No.7' }
    ];
    return dataColl;
};

function GetZoneList() {
    var dataColl = [
        { id: 1, text: 'Mechi', textNP: 'मेची' },

        { id: 2, text: 'Koshi', textNP: 'कोशी' },

        { id: 3, text: 'Sagarmatha', textNP: 'सगरमाथा' },

        { id: 4, text: 'Janakpur', textNP: 'जनकपुर' },

        { id: 5, text: 'Bagmati', textNP: ' बागमति' },

        { id: 6, text: 'Narayani', textNP: 'नारायणी' },

        { id: 7, text: 'Gandaki', textNP: 'गण्डकी' },

        { id: 8, text: 'Dhawalagiri', textNP: 'धवलागिरी' },

        { id: 9, text: 'Lumbini', textNP: 'लुम्विनी' },

        { id: 10, text: 'Rapti', textNP: 'राप्ती' },

        { id: 11, text: 'Bheri', textNP: 'भेरी' },

        { id: 12, text: 'Karnali', textNP: 'कर्णाली' },

        { id: 13, text: 'Seti', textNP: 'सेती' },

        { id: 14, text: 'Mahakali', textNP: 'महाकाली' }
    ];
    return dataColl;
};

function GetDistrictList() {
    var dataColl = [
        { id: 1, zoneId: 1, stateId: 1, text: 'Taplejung', textNP: 'ताप्लेजुंग ' },
        { id: 2, zoneId: 1, stateId: 1, text: 'Panchthar', textNP: 'पांचथर ' },
        { id: 3, zoneId: 1, stateId: 1, text: 'Ilam', textNP: 'इलाम ' },
        { id: 4, zoneId: 1, stateId: 1, text: 'Jhapa', textNP: 'झापा ' },
        { id: 5, zoneId: 2, stateId: 1, text: 'Morang', textNP: 'मोरंग ' },
        { id: 6, zoneId: 2, stateId: 1, text: 'Sunsari', textNP: 'सुनसरी' },
        { id: 7, zoneId: 2, stateId: 1, text: 'Dhankutta', textNP: 'धनकुटा ' },
        { id: 8, zoneId: 2, stateId: 1, text: 'Sankhuwasabha', textNP: 'संखुवासभा ' },
        { id: 9, zoneId: 2, stateId: 1, text: 'Bhojpur', textNP: 'भोजपुर ' },
        { id: 10, zoneId: 2, stateId: 1, text: 'Terhathum', textNP: 'तेह्रथुम ' },
        { id: 11, zoneId: 3, stateId: 1, text: 'Okhaldunga', textNP: 'ओखलढुंगा ' },
        { id: 12, zoneId: 3, stateId: 1, text: 'Khotang', textNP: 'खोटाँग ' },
        { id: 13, zoneId: 3, stateId: 1, text: 'Solukhumbu', textNP: 'सोलुखुम्बू ' },
        { id: 14, zoneId: 3, stateId: 1, text: 'Udaypur', textNP: 'उदयपुर ' },
        { id: 15, zoneId: 3, stateId: 2, text: 'Saptari', textNP: 'सप्तरी ' },
        { id: 16, zoneId: 3, stateId: 2, text: 'Siraha', textNP: 'सिराहा ' },
        { id: 17, zoneId: 4, stateId: 2, text: 'Dhanusa', textNP: 'धनुषा ' },
        { id: 18, zoneId: 4, stateId: 2, text: 'Mahottari', textNP: 'महोत्तरी ' },
        { id: 19, zoneId: 4, stateId: 2, text: 'Sarlahi', textNP: 'सर्लाही ' },
        { id: 20, zoneId: 4, stateId: 3, text: 'Sindhuli', textNP: 'सिन्धुली ' },
        { id: 21, zoneId: 4, stateId: 3, text: 'Ramechhap', textNP: 'रामेछाप ' },
        { id: 22, zoneId: 4, stateId: 3, text: 'Dolkha', textNP: 'दोलखा' },
        { id: 23, zoneId: 5, stateId: 3, text: 'Sindhupalchauk', textNP: 'सिन्धुपाल्चोक ' },
        { id: 24, zoneId: 5, stateId: 3, text: 'Kavreplanchauk', textNP: 'काभ्रेपलान्चोक ' },
        { id: 25, zoneId: 5, stateId: 3, text: 'Lalitpur', textNP: 'ललितपुर ' },
        { id: 26, zoneId: 5, stateId: 3, text: 'Bhaktapur', textNP: 'भक्तपुर ' },
        { id: 27, zoneId: 5, stateId: 3, text: 'Kathmandu', textNP: 'काठमाडौँ ' },
        { id: 28, zoneId: 5, stateId: 3, text: 'Nuwakot', textNP: 'नुवाकोट ' },
        { id: 29, zoneId: 5, stateId: 3, text: 'Rasuwa', textNP: 'रसुवा ' },
        { id: 30, zoneId: 5, stateId: 3, text: 'Dhading', textNP: 'धादिङ' },
        { id: 31, zoneId: 6, stateId: 3, text: 'Makwanpur', textNP: 'मकवानपुर ' },
        { id: 32, zoneId: 6, stateId: 2, text: 'Rauthat', textNP: 'रौतहट ' },
        { id: 33, zoneId: 6, stateId: 2, text: 'Bara', textNP: 'बारा ' },
        { id: 34, zoneId: 6, stateId: 2, text: 'Parsa', textNP: 'पर्सा ' },
        { id: 35, zoneId: 6, stateId: 3, text: 'Chitwan', textNP: 'चितवन' },
        { id: 36, zoneId: 7, stateId: 4, text: 'Gorkha', textNP: 'गोरखा ' },
        { id: 37, zoneId: 7, stateId: 4, text: 'Lamjung', textNP: 'लमजुङ ' },
        { id: 38, zoneId: 7, stateId: 4, text: 'Tanahun', textNP: 'तनहुँ ' },
        { id: 39, zoneId: 7, stateId: 4, text: 'Syangja', textNP: 'स्याङग्जा' },
        { id: 40, zoneId: 7, stateId: 4, text: 'Kaski', textNP: 'कास्की ' },
        { id: 41, zoneId: 7, stateId: 4, text: 'Manag', textNP: 'मनाङ ' },
        { id: 42, zoneId: 8, stateId: 4, text: 'Mustang', textNP: 'मुस्ताङ ' },
        { id: 43, zoneId: 8, stateId: 4, text: 'Parwat', textNP: 'पर्वत ' },
        { id: 44, zoneId: 8, stateId: 4, text: 'Myagdi', textNP: 'म्याग्दी ' },
        { id: 45, zoneId: 8, stateId: 4, text: 'Baglung', textNP: 'बागलुङ' },
        { id: 46, zoneId: 9, stateId: 5, text: 'Gulmi', textNP: 'गुल्मी ' },
        { id: 47, zoneId: 9, stateId: 5, text: 'Palpa', textNP: 'पाल्पा ' },
        { id: 48, zoneId: 9, stateId: 4, text: 'Nawalpur', textNP: 'नवलपुर ' },
        { id: 49, zoneId: 9, stateId: 5, text: 'Parasi', textNP: 'परासी ' },
        { id: 50, zoneId: 9, stateId: 5, text: 'Rupandehi', textNP: 'रुपन्देही ' },
        { id: 51, zoneId: 9, stateId: 5, text: 'Arghakhanchi', textNP: 'अर्घाखाँची ' },
        { id: 52, zoneId: 9, stateId: 5, text: 'Kapilvastu', textNP: 'कपिलवस्तु ' },
        { id: 53, zoneId: 10, stateId: 5, text: 'Pyuthan', textNP: 'प्युठान ' },
        { id: 54, zoneId: 10, stateId: 5, text: 'Rolpa', textNP: 'रोल्पा ' },
        { id: 55, zoneId: 10, stateId: 5, text: 'Rukum Purba', textNP: 'पूर्वी रूकुम ' },
        { id: 56, zoneId: 10, stateId: 6, text: 'Rukum Paschim', textNP: 'पश्चिमी रूकुम ' },
        { id: 57, zoneId: 10, stateId: 6, text: 'Salyan', textNP: 'सल्यान ' },
        { id: 58, zoneId: 10, stateId: 5, text: 'Dang', textNP: 'दाङ ' },
        { id: 59, zoneId: 11, stateId: 5, text: 'Bardiya', textNP: 'बर्दिया ' },
        { id: 60, zoneId: 11, stateId: 6, text: 'Surkhet', textNP: 'सुर्खेत ' },
        { id: 61, zoneId: 11, stateId: 6, text: 'Dailekh', textNP: 'दैलेख ' },
        { id: 62, zoneId: 11, stateId: 5, text: 'Banke', textNP: 'बाँके ' },
        { id: 63, zoneId: 11, stateId: 6, text: 'Jajarkot', textNP: 'जाजरकोट ' },
        { id: 64, zoneId: 12, stateId: 6, text: 'Dolpa', textNP: 'डोल्पा ' },
        { id: 65, zoneId: 12, stateId: 6, text: 'Humla', textNP: 'हुम्ला ' },
        { id: 66, zoneId: 12, stateId: 6, text: 'Kalikot', textNP: 'कालिकोट ' },
        { id: 67, zoneId: 12, stateId: 6, text: 'Mugu', textNP: 'मुगु ' },
        { id: 68, zoneId: 12, stateId: 6, text: 'Jumla', textNP: 'जुम्ला' },
        { id: 69, zoneId: 13, stateId: 7, text: 'Bajura', textNP: 'बाजुरा' },
        { id: 70, zoneId: 13, stateId: 7, text: 'Bajhang', textNP: 'बझाङ ' },
        { id: 71, zoneId: 13, stateId: 7, text: 'Achham', textNP: 'अछाम ' },
        { id: 72, zoneId: 13, stateId: 7, text: 'Doti', textNP: 'डोटी ' },
        { id: 73, zoneId: 1, stateId: 7, text: 'Kailali', textNP: 'कैलाली ' },
        { id: 74, zoneId: 14, stateId: 7, text: 'Kanchanpur', textNP: 'कंचनपुर ' },
        { id: 75, zoneId: 14, stateId: 7, text: 'Dadeldhura', textNP: 'डडेलधुरा ' },
        { id: 76, zoneId: 14, stateId: 7, text: 'Baitadi', textNP: 'बैतडी ' },
        { id: 77, zoneId: 14, stateId: 7, text: 'Darchula', textNP: 'दार्चुला ' }
    ];
    return dataColl;
};
