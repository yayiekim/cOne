var patientController = angular.module('patientController', []);


patientController.controller('patientCtrl', function ($scope, $http) {


    function ToJavaScriptDate(value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt;
    }

    $scope.submitType;

    $scope.disableInput = false;

    $scope.showAge = false;


    //GET PATIENTSLIST


    $scope.patientsList = [];
    $scope.patientsListDisplay = [].concat($scope.patientsList);

    $http({
        method: 'GET',
        url: '/Patients/getPatients'
    }).success(function (data) {


        angular.forEach(data, function (resdata) {


            var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
            resdata.BirthDate = BirthDate;


        });


        $scope.patientsList = data;

    }, function errorCallBack(data) {

    });


    //ADD PATIENTS

    $scope.showAddModal = function () {

        $scope.Patient = {

            Id: '',
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: '',
            BirthDate: '',
            Age: '',
            BloodType: '',
            ContactNumber1: '',
            ContactNumber2: '',
            Address1: '',
            Address2: ''

        };

        $('#addModal').modal('toggle');

        $scope.submitType = 'add';

        $('#modalTitle').text('Add New Patient');

        $scope.disableInput = false;

        $scope.showAge = false;
    }

    $scope.Patient = {

        Id: '',
        FirstName: '',
        MiddleName: '',
        LastName: '',
        Gender: '',
        BirthDate: '',
        Age: '',
        BloodType: '',
        ContactNumber1: '',
        ContactNumber2: '',
        Address1: '',
        Address2: ''

    };

    $scope.updatePatient = function () {

        if ($scope.submitType == 'add') {

            $http({
                method: 'POST',
                url: '/Patients/addPatient',
                data: $.param({ patient: $scope.Patient }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                var BirthDate = new Date(ToJavaScriptDate(data.BirthDate))
                data.BirthDate = BirthDate;


                $scope.patientsList.push(data);

            });

        }
        else {

            $http({
                method: 'POST',
                url: '/Patients/editPatient',
                data: $.param({ patient: $scope.Patient }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                $scope.patientsList.push($scope.Patient);

            });
        }


    }


    //EDIT PATIENTS


    $scope.showEditModal = function (row) {

        $('#addModal').modal('toggle');

        $('#modalTitle').text('Edit Patient');

        $scope.Patient = row;

        $scope.submitType = 'edit';

        $scope.disableInput = false;

        $scope.showAge = false;
    }


    //DETAILS PATIENTS

    $scope.showDetailModal = function (row) {

        $('#addModal').modal('toggle');

        $('#modalTitle').text('Patient Details');

        $scope.Patient = row;

        $scope.disableInput = true;

        $scope.showAge = true;

    }


    //DELETE PATIENTS



    $scope.showConfirmDeleteModal = function (row) {

        $scope.Patient = row;

        $('#deleteModal').modal('toggle');

        $('#deletePatientFullName').text(row.FirstName + ' ' + row.MiddleName + ' ' + row.LastName);
    }


    $scope.deletePatient = function () {


        $http({
            method: 'GET',
            url: '/Patients/delete?id=' + $scope.Patient.Id + ''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.patientsList.indexOf($scope.Patient);

                $scope.patientsList.splice(index, 1);
            }

        }, function errorCallback(data) {

        });
    };





});