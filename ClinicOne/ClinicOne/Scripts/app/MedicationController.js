var medicationController = angular.module('medicationController', []);


medicationController.controller('medicationCtrl', function ($scope, $http) {

    $scope.medicationsList = [];
    $scope.medicationsListDisplay = [].concat($scope.medicationsList);

    $scope.submitType;

    $scope.medicationCategoryList = [];

    $scope.disableInput = false;

    $scope.showSave = false;

    //FOR DROPDOWN MEDICATION CATEGORY

    $scope.showQuickAdd = function () {

        $('#quickAddModal').modal('toggle');

    };


    $scope.Category = {
        Id: '',
        CategoryName: ''
    };

    $scope.updateCategory = function () {

            $http({
                method: 'POST',
                url: '/Medications/addMedicationCategory/',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.Category.Id = data;
                $scope.medicationCategoryList.push($scope.Category);

                $scope.Category = {
                    Id: '',
                    CategoryName: ''
                };

            });
      
    }

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

        $scope.showSave = true;

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

        $('#modalTitle').text('New');
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

        $scope.showSave = false;

        $scope.getMedicationCategories();

        $scope.medication = row;

        $('#addModal').modal('toggle');

        $scope.disableInput = true;

        $('#modalTitle').text('Details');

        $scope.disableSave = false;

    }



    //EDIT MEDICATION

    $scope.showEditModal = function (row) {

        $scope.showSave = true;

        $('#addModal').modal('toggle');

        $scope.medication = row;

        $scope.getMedicationCategories();

        $scope.disableInput = false;

        $('#modalTitle').text('Edit');
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