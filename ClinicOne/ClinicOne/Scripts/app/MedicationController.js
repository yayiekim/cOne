var medicationController = angular.module('medicationController', []);


medicationController.controller('medicationCtrl', function ($scope, $http) {

    $scope.medicationsList = [];
    $scope.medicationsListDisplay = [].concat($scope.medicationsList);

    $scope.submitType;

    $scope.medicationCategoryList = [];

    $scope.disableInput = false;

    //FOR DROPDOWN MEDICATION CATEGORY

    $scope.getMedicationCategories = function () {

        $http({
            method: 'GET',
            url: '/Medications/getMedicationCategories'
        }).success(function (data) {

            $scope.medicationCategoryList = data;
        });
    }


    $scope.medication = {

        'Id': '',
        'CategoryId': '',
        'CategoryName': '',
        'MedicationName': '',
        'Code': '',
        'Description': '',
        'Amount': '',
        'Dosage': '',
    };

    //GET MEDICATIONS

    $http({
        method: 'GET',
        url: '/Medications/getMedications/'
    }).success(function (data) {

        $scope.medicationsList = data;
    });

    //ADD MEDICATIONS

    $scope.showAddModal = function () {

        $scope.medication = {

            'Id': '',
            'CategoryId': '',
            'CategoryName': '',
            'MedicationName': '',
            'Code': '',
            'Description': '',
            'Amount': '',
            'Dosage': '',
        };


        $('#addModal').modal('toggle');

        $scope.getMedicationCategories();

        $scope.submitType = 'add';

        $scope.disableInput = false;
    }


    $scope.updateMedication = function () {

        if ($scope.submitType == 'add') {

            $http({
                method: 'POST',
                url: '/Medications/addMedication',
                data: $.param({ medication: $scope.medication }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.medicationsList.push(data);
            });
        }

        $http({
            method: 'POST',
            url: '/Medications/editMedication',
            data: $.param({ medication: $scope.medication }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        }).success(function (data) {
        
            
            $scope.medication.CategoryName = data.CategoryName;
        });

    };


    //DETAILS MEDICATION

    $scope.showDetailModal = function (row) {

        $scope.getMedicationCategories();

        $scope.medication = row;

        $('#addModal').modal('toggle');

        $scope.disableInput = true;
    }



    //EDIT MEDICATION

    $scope.showEditModal = function (row) {

        $('#addModal').modal('toggle');

        $scope.medication = row;

        $scope.getMedicationCategories();

        $scope.disableInput = false;
    }



    //DELETE MEDICATION

    $scope.showDeleteModal = function (row) {

        $scope.medication = row;

        $('#deleteModal').modal('toggle');
    }


    $scope.deleteMedication = function () {

        $http({
            method: 'GET',
            url: '/Medications/deleteMedication?id=' + $scope.medication.Id + ''

        }).success(function (data) {

            if (data == 'ok') {

                var index = $scope.medicationsList.indexOf($scope.medication);

                $scope.medicationsList.splice(index, 1)
            }
        });
    }


});