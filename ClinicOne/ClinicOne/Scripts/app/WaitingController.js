var waitingController = angular.module('waitingController', []);


waitingController.controller('waitingCtrl', function ($scope, $http, $filter) {


    function ToJavaScriptDate(value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt;
    }
    
    //GET WAITINGLIST

    $scope.alertMessage;

    $scope.disable = true;
   
    $scope.waitingList = [];
    $scope.waitingListDisplay = [].concat($scope.waitingList);

    $http({
        method: 'GET',
        url: '/Waiting/getWaitingPatients'
    }).success(function (data) {

        angular.forEach(data, function (resdata) {


            var Schedule = new Date(ToJavaScriptDate(resdata.Schedule))
            resdata.Schedule = Schedule;
                 

        });


        $scope.waitingList = data;

    }, function errorCallback(data) {

    });


    //ADD WAITING PATIENT

    $scope.patientsList = [];
    $scope.patientsListDisplay = [].concat($scope.patientsList);

    $scope.showAddWaitingModal = function () {

        $('#addModal').modal('toggle');


        if ($scope.patientsList.length == 0) {

            $http({
                method: 'GET',
                url: '/Patients/getPatients'
            }).success(function (data) {


                angular.forEach(data, function (resdata) {


                    var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
                    resdata.BirthDate = BirthDate;


                });

                $scope.patientsList = data;

            }, function errorCallback(data) {

            });
        }

       

    }

    $scope.selectedId;


    $scope.showConfirm = function (Id) {

        $scope.selectedId = Id;

        var myRedObjects = $filter('filter')($scope.waitingList, { PatientId: Id });

        if (myRedObjects.length > 0) {
            
            $('#warningModal').modal('toggle');
        }
        else {

            $('#quickAddModal').modal('toggle');
        }
    };


    $scope.addWaiting = function () {


        $scope.waitingModel = {
            'Id': '',
            'PatientId': $scope.selectedId,
            'Schedule': '',
            'Remarks': $('#remarksTb').val()

        };

        $http({
            method: 'POST',
            url: '/Waiting/addWaitingPatient',
            data: $.param({ patient: $scope.waitingModel }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {



            var Schedule = new Date(ToJavaScriptDate(data.Schedule))
            data.Schedule = Schedule;

            $scope.waitingList.push(data);

        });


    }


    //DELETE WAITING PATIENTS


    $scope.selectedWaitingRow;

    $scope.showConfirmDeleteModal = function (row) {

        $scope.selectedWaitingRow = row;

        $('#deleteModal').modal('toggle');

        $('#dName').text(row.PatientFullName);


    }

    $scope.DeleteWaiting = function () {

                
        $http({
            method: 'GET',
            url: '/Waiting/deleteWaitingPatient?id='+ $scope.selectedWaitingRow.Id +''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.waitingList.indexOf($scope.selectedWaitingRow);

                $scope.waitingList.splice(index, 1);
            }

        }, function errorCallback(data) {

        });


    };



    $scope.showAdmitModal = function (row) {

        $scope.selectedWaitingRow = row;

        $('#admitModal').modal('toggle');

        $('#confirmAdmitPatientName').text(row.PatientFullName);

    }

    

    $scope.admitPatient = function (row) {

        $http({
            method: 'GET',
            url: '/Waiting/admitPatient?id=' + row.Id + ''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.waitingList.indexOf(row);

                $scope.waitingList.splice(index, 1);
            }
            else {


                $scope.alertMessage = data;
                $('#AlertModal').modal('toggle');

            }

        }, function errorCallback(data) {

        });

    }


});