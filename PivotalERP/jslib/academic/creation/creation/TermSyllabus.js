app.directive('select', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                $(element).select2({
                    placeholder: "**Select an Option**",
                    allowClear: true
                });

                // Watch ngModel safely
                scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        $timeout(function () {
                            $(element).trigger('change');
                        });
                    }
                });

                // Ensure select2 updates when value changes externally
                $(element).on('change', function () {
                    $timeout(function () {
                        if (!scope.$$phase) {
                            scope.$apply();
                        }
                    });
                });

            }, 2);
        }
    };
});
app.controller('TermSyllabusController', function ($scope, $http, $timeout, GlobalServices) {
    $scope.Title = 'TermSyllabus';

    OnClickDefault();
    var glbS = GlobalServices;
    $scope.LoadData = function () {
        setTimeout(() => {
            $('.select2').select2();
        }, 100);

        $scope.GetAllSyllabusList();

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            TermSyllabus: 1,
        };
        $scope.searchData = {
            ExamTypeList: '',
            ClassTypeList: ''

        };
        $scope.perPage = {
            TermSyllabus: GlobalServices.getPerPageRow(),
        };


        //Company Details
        $scope.newCompanyDet = {};
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetCompanyDet",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {

                $scope.newCompanyDet = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.Logo = [];
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllAboutUsList",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.Logo = res.data.Data[0];
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ExamTypeList = [];
        glbS.getExamTypeList().then(function (res) {
            $scope.ExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.Configuration = {};
        $http({
            method: 'POST',
            url: base_url + "HomeWork/Transaction/GetHAConfiguration",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";


            if (res.data.IsSuccess && res.data.Data) {
                $scope.Configuration = res.data.Data;
            }
            else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.AcademicConfig = {};
        GlobalServices.getAcademicConfig().then(function (res1) {
            $scope.AcademicConfig = res1.data.Data;

            if ($scope.AcademicConfig.ActiveFaculty == true) {

                $scope.FacultyList = [];
                GlobalServices.getFacultyList().then(function (res) {
                    $scope.FacultyList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }


            if ($scope.AcademicConfig.ActiveLevel == true) {

                $scope.LevelList = [];
                GlobalServices.getClassLevelList().then(function (res) {
                    $scope.LevelList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveSemester == true) {

                $scope.SelectedClassSemesterList = [];
                $scope.SemesterList = [];
                GlobalServices.getSemesterList().then(function (res) {
                    $scope.SemesterList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveBatch == true) {

                $scope.BatchList = [];
                GlobalServices.getBatchList().then(function (res) {
                    $scope.BatchList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveClassYear == true) {

                $scope.ClassYearList = [];
                /*$scope.SelectedClassClassYearList = [];*/
                GlobalServices.getClassYearList().then(function (res) {
                    $scope.ClassYearList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ClassSection = {};
        glbS.getClassSectionList().then(function (res) {
            $scope.ClassSection = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //student list
        $scope.ClassListsection = [];
        glbS.getClassSectionList().then(function (res) {
            $scope.ClassListsection = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.SubjectList = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSubjectList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SubjectList = res.data.Data;

            } else {

                if (res.data.IsSuccess == false)
                    Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //class List
        $scope.ClassSectionList = [];
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSectionList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.newSyllabus =
        {
            SyllabusId: null,
            ClassId: 1,
            ExamTypeId: 2,
            SubjectId: null,
            BatchId: 1,
            SemesterId: 1,
            ClassYearId: 1,
            AddSyllabusColl: [],
            Mode: 'save'
        };
        $scope.newSyllabus.AddSyllabusColl.push({});
    };

    $scope.getLessonAndTopicNames = function (S) {
        var lessonName = '';
        var topicNames = [];
        if (S.LessonId && $scope.SubjectLessonWiseList) {
            var lesson = $scope.SubjectLessonWiseList.find(function (l) {
                return l.LessonId == S.LessonId;
            });
            if (lesson) {
                lessonName = lesson.LessonName;
            }
        }
        if (S.TopicId && S.LessonTopicDetailsWiseList) {
            var selectedIds = Array.isArray(S.TopicId) ? S.TopicId : [S.TopicId];
            topicNames = S.LessonTopicDetailsWiseList
                .filter(function (t) {
                    return selectedIds.indexOf(t.TopicId) !== -1;
                })
                .map(function (t) {
                    return t.TopicName;
                });
        }
        if (!lessonName && topicNames.length === 0) {
            return 'Select Lesson and Topic';
        } else if (lessonName && topicNames.length === 0) {
            return lessonName;
        } else if (!lessonName && topicNames.length > 0) {
            return topicNames.join(', ');
        } else {
            return lessonName + ' / ' + topicNames.join(', ');
        }
    };


    $scope.CurrentTopic = {};
    $scope.ChangeCurrentTopic = function (Topic) {
        $scope.CurrentTopic = Topic;
    }


   

   

    function OnClickDefault() {
        document.getElementById('add-Syllabus-form').style.display = "none";
        document.getElementById('preview-form').style.display = "none";

        document.getElementById('add-Syllabus').onclick = function () {
            document.getElementById('table-section').style.display = "none";
            document.getElementById('add-Syllabus-form').style.display = "block";
        }
        document.getElementById('Syllabusback-btn').onclick = function () {
            document.getElementById('add-Syllabus-form').style.display = "none";
            document.getElementById('table-section').style.display = "block";
        }
    };

    $scope.GetClassWiseSubjectList = function (classId) {
        $scope.SubjectList = [];
        var para = {
            ClassId: $scope.newSyllabus.SelectedClass.ClassId,
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {

                $timeout(function () {
                    $scope.SubjectList = res.data.Data;
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

 

   


    $scope.ClearDetails = function () {
        $timeout(function () {
            $scope.newSyllabus =
            {
                SyllabusId: null,
                ClassId: null,
                ExamTypeId: null,
                SubjectId: null,
                BatchId: null,
                SemesterId: null,
                ClassYearId: null,
                Content: '',
                TeachingPeriod: '',
                TeachingMethods: '',
                TeachingMaterials: '',
                Evaluation: '',
                Evaluation: '',
                Remarks: '',
                AddSyllabusColl: [],
                Mode: 'save'
            };
            $scope.newSyllabus.AddSyllabusColl.push({});
        });
    };


    $scope.AddSyllabus = function (index) {
        $scope.newSyllabus.AddSyllabusColl.splice(index + 1, 0, {
            LessonId: null,
            TopicId: [],
            Content: '',
            TeachingPeriod: '',
            TeachingMethods: '',
            TeachingMaterials: '',
            Evaluation: '',
            Remarks: '',
            LessonTopicDetailsWiseList: []
        });
    };

    $scope.DeleteSyllabus = function (index) {
        if ($scope.newSyllabus.AddSyllabusColl) {
            if ($scope.newSyllabus.AddSyllabusColl.length > 1) {
                $scope.newSyllabus.AddSyllabusColl.splice(index, 1);
            }
        }
    }

    $scope.IsValidSyllabus = function () {
        if (!$scope.newSyllabus.ExamTypeId) {
            Swal.fire('Please select an Exam Type.');
            return false;
        }

        if (!$scope.newSyllabus.ClassId) {
            Swal.fire('Please select a Class.');
            return false;
        }

        if (!$scope.newSyllabus.SubjectId) {
            Swal.fire('Please select a Subject.');
            return false;
        }

        return true;
    }

    $scope.SaveSyllabus = function () {
        if ($scope.IsValidSyllabus() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newSyllabus.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateSyllabus();
                    }
                });
            }
        } else
            $scope.SaveUpdateSyllabus();
    };



    $scope.SaveUpdateSyllabus = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var tmpDataColl = [];

        angular.forEach($scope.newSyllabus.AddSyllabusColl, function (det) {
            // Convert Topics array to comma-separated string
            if (Array.isArray(det.TopicId)) {
                det.TopicId = det.TopicId.join(',');
            }

            // Assign properties from parent object
            det.ExamTypeId = $scope.newSyllabus.ExamTypeId;
            det.ClassId = $scope.newSyllabus.SelectedClass.ClassId;
            det.SubjectId = $scope.newSyllabus.SubjectId;
            det.BatchId = $scope.newSyllabus.BatchId || null;
            det.SemesterId = $scope.newSyllabus.SemesterId || null;
            det.ClassYearId = $scope.newSyllabus.ClassYearId || null;

            // Push to collection
            tmpDataColl.push(det);
        });

        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/SaveSyllabus",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: {
                jsonData: tmpDataColl
            }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess === true) {
                $scope.ClearDetails();
                $scope.GetAllSyllabusList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    };


    //$scope.SaveUpdateSyllabus = function () {
    //    $scope.loadingstatus = "running";
    //    showPleaseWait();
    //    var tmpDataColl = [];

    //    angular.forEach($scope.newSyllabus.AddSyllabusColl, function (det) {
    //        tmpDataColl.push(det);
    //        det.ExamTypeId = $scope.newSyllabus.ExamTypeId;
    //        det.ClassId = $scope.newSyllabus.SelectedClass.ClassId;
    //        det.SubjectId = $scope.newSyllabus.SubjectId;
    //        det.BatchId = $scope.newSyllabus.BatchId;
    //        det.SemesterId = $scope.newSyllabus.SemesterId;
    //        det.ClassYearId = $scope.newSyllabus.ClassYearId;
    //    });

    //    $http({
    //        method: 'POST',
    //        url: base_url + "Academic/Creation/SaveSyllabus",
    //        headers: { 'Content-Type': undefined },
    //        transformRequest: function (data) {
    //             var formData = new FormData();
    //            formData.append("jsonData", angular.toJson(data.jsonData));

    //            return formData;
    //        },
    //        data: {
    //            jsonData: tmpDataColl
    //        }
    //    }).then(function (res) {
    //        $scope.loadingstatus = "stop";
    //        hidePleaseWait();
    //        Swal.fire(res.data.ResponseMSG);
    //        if (res.data.IsSuccess == true) {
    //            $scope.ClearDetails();
    //            $scope.GetAllSyllabusList();
    //        }
    //    }, function (errormessage) {
    //        hidePleaseWait();
    //        $scope.loadingstatus = "stop";
    //    });
    //}

    $scope.GetAllSyllabusList = function () {
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSyllabus",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var rawData = res.data.Data;
                var grouped = {};

                angular.forEach(rawData, function (item) {
                    var key = item.ExamTypeId;

                    if (!grouped[key]) {
                        grouped[key] = {
                            ExamTypeId: item.ExamTypeId,
                            ExamName: item.ExamName,
                            classId: item.ClassId,
                            ClassName: item.ClassName,
                            Records: []
                        };
                    }
                    grouped[key].Records.push(item);
                });

                $scope.GroupedSyllabusList = Object.values(grouped);
            } else {
                $scope.GroupedSyllabusList = [];
                Swal.fire(res.data.ResponseMSG);
            }
        });
    };

   
    $scope.ShowClasswiseSyllabus = function (S) {
        $scope.ContentSyllabusList = [];
        $scope.SelectedExamType = S.ExamName;
        $scope.SelectedClass = S.ClassName;
        $scope.SelectedSubjectName = S.SubjectName;
        if (S.SubjectId != null) {
            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                BatchId: S.BatchId,
                ClassId: S.ClassId,
                SemesterId: S.SemesterId,
                ClassYearId: S.ClassYearId,
                ExamTypeId: S.ExamTypeId,
                SubjectId: S.SubjectId
            };

            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/GetAllClassSubjectWiseSyllabus",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

                if (res.data.IsSuccess && res.data.Data) {
                    $scope.ContentSyllabusList = res.data.Data;

                    $('#ModalList').modal('show');
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed: ' + reason);
            });
        }
    };

    $scope.PrintSyllabus = function (S) {
        $scope.ContentSyllabusList = [];
        $scope.SelectedExamType = S.SubjectName;
        $scope.SelectedClass = S.ClassName;
        $scope.SelectedSubjectName = S.SubjectName;
        if (S.SubjectId != null) {
            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                BatchId: S.BatchId,
                ClassId: S.ClassId,
                SemesterId: S.SemesterId,
                ClassYearId: S.ClassYearId,
                ExamTypeId: S.ExamTypeId,
                SubjectId: S.SubjectId
            };

            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/GetAllClassSubjectWiseSyllabus",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

                if (res.data.IsSuccess && res.data.Data) {
                    $scope.ContentSyllabusList = res.data.Data;

                    $('#printcard').printThis();
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed: ' + reason);
            });
        }
    };

    $scope.GetClassSubjectWiseTermSyllabus = function () {
        $scope.newSyllabus.AssetsDetailColl = [];

        if ($scope.newSyllabus.SubjectId != null) {
            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                BatchId: $scope.newSyllabus.BatchId,
                ClassId: $scope.newSyllabus.ClassId,
                SemesterId: $scope.newSyllabus.SemesterId,
                ClassYearId: $scope.newSyllabus.ClassYearId,
                ExamTypeId: $scope.newSyllabus.ExamTypeId,
                SubjectId: $scope.newSyllabus.SubjectId
            };

            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/GetAllClassSubjectWiseSyllabus",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

                if (res.data.IsSuccess && res.data.Data) {
                    $scope.newSyllabus.AddSyllabusColl = res.data.Data;

                    // Convert TopicId from comma-separated string to array of numbers
                    angular.forEach($scope.newSyllabus.AddSyllabusColl, function (item) {
                        if (item.TopicId && typeof item.TopicId === 'string') {
                            item.TopicId = item.TopicId.split(',').map(function (id) {
                                return parseInt(id.trim());
                            });
                        }
                    });

                    // Also ensure TopicId inside LessonTopicDetailsWiseList are numbers for all lessons
                    angular.forEach($scope.SubjectLessonWiseList, function (lesson) {
                        if (lesson.LessonTopicDetailsWiseList) {
                            angular.forEach(lesson.LessonTopicDetailsWiseList, function (topic) {
                                topic.TopicId = parseInt(topic.TopicId);
                            });
                        }
                    });

                    if (!$scope.newSyllabus.AddSyllabusColl || $scope.newSyllabus.AddSyllabusColl.length == 0) {
                        $scope.newSyllabus.AddSyllabusColl = [{}];
                    }

                    $timeout(function () {
                        $scope.GetSubjectLessonWise();
                    });
                    $timeout(function () {
                        $scope.GetLessonTopicDetailsWise();
                    });

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed: ' + reason);
            });
        }
    };

    $scope.GetSubjectLessonWise = function () {
        if ($scope.newSyllabus.SelectedClass.ClassId && $scope.newSyllabus.SubjectId > 0) {
            $scope.loadingstatus = "running";
            showPleaseWait();
            var para = {
                ClassId: $scope.newSyllabus.SelectedClass.ClassId,
                SubjectId: $scope.newSyllabus.SubjectId
            };
            $scope.SubjectLessonWiseList = [];

            $http({
                method: 'POST',
                url: base_url + "Exam/Transaction/GetSubjectLessonWise",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.SubjectLessonWiseList = res.data.Data;

                    // Now, for each syllabus item, if LessonId exists, call GetLessonTopicDetailsWise(S)
                    angular.forEach($scope.newSyllabus.AddSyllabusColl, function (S) {
                        if (S.LessonId) {
                            $scope.GetLessonTopicDetailsWise(S);
                        }
                    });

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }
    };



    $scope.GetLessonTopicDetailsWise = function (S) {
        if (S.LessonId) {
            $scope.loadingstatus = "running";
            showPleaseWait();
            var para = {
                LessonId: S.LessonId
            };
            S.LessonTopicDetailsWiseList = []; // Attach topic list to the specific row object

            $http({
                method: 'POST',
                url: base_url + "Exam/Transaction/GetLessonTopicDetailsWise",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    S.LessonTopicDetailsWiseList = res.data.Data;
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed: ' + reason);
            });
        }
    };
});