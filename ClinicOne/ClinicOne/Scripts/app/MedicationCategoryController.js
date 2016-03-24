var medicationCategoryController = angular.module('medicationCategoryController', []);



medicationCategoryController.controller('medicationCategoryCtrl', function ($scope, $http) {

    $scope.MedicationCategoryList = [];
    $scope.MedicationCategoryListDisplay = [].concat($scope.MedicationCategoryList);

    $scope.submitType;

    $scope.disableInput = false;

    $http({
        method: 'GET',
        url: '/Medications/getMedicationCategories/'
    }).success(function (data) {

        $scope.MedicationCategoryList = data;

    }, function errorCallBack(data) {

    });

    //ADD CATEGORY

    $scope.Category = {
        Id: '',
        CategoryName: ''
    };

    $scope.showAddModal = function (row) {

        $scope.Category = row;

        $('#addModal').modal('toggle');

        $scope.submitType = 'add';

    }


    $scope.updateCategory = function () {

        if ($scope.submitType == 'add') {

            $http({
                method: 'POST',
                url: '/Medications/addMedicationCategory/',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.Category.Id = data;
                $scope.MedicationCategoryList.push($scope.Category);
            });
        }
        else {

            $http({
                method: 'POST',
                url: '/Medications/editMedicationCategory',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                if (data == 'ok') {

                    $scope.MedicationCategoryList.push($scope.Category);
                }

            });
        }

    }


    //EDIT CATEGORY


    $scope.showEditModal = function (row) {

        $('#addModal').modal('toggle');

        $scope.Category = row;

        $scope.submitType = 'edit';
    }



    //DELETE CATEGORY


    $scope.showDeleteModal = function (row) {

        $scope.Category = row;

        $('#deleteModal').modal('toggle');

        $('#CategoryName').text();
    }


    $scope.deleteCategory = function () {


        $http({
            method: 'GET',
            url: '/Medications/deleteMedicationCategory?id=' + $scope.Category.Id + ''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.MedicationCategoryList.indexOf($scope.Category);

                $scope.MedicationCategoryList.splice(index, 1);
            }

        }, function errorCallback(data) {

        });
    };


});