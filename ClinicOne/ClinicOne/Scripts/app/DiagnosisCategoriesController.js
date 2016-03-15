var diagnosisCategoriesController = angular.module('diagnosisCategoriesController', []);



diagnosisCategoriesController.controller('diagnosisCategoriesCtrl', function ($scope, $http) {
    
    $scope.diagnosisTypeCategoryList = [];
    $scope.diagnosisCategoryListDisplay = [].concat($scope.diagnosisTypeCategoryList);

    $scope.submitType;

    $scope.disableInput = false;

    $http({
        method: 'GET',
        url: '/Diagnosis/getDiagnosisCategories/'
    }).success(function (data) {

        $scope.diagnosisTypeCategoryList = data;

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
                url: '/Diagnosis/addDiagnosisCategory/',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.Category.Id = data;
                $scope.diagnosisTypeCategoryList.push($scope.Category);
            });
        }
        else {

            $http({
                method: 'POST',
                url: '/Diagnosis/editDiagnosisCategory',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                if (data == 'ok') {

                    $scope.diagnosisTypeCategoryList.push($scope.Category);
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
            url: '/Diagnosis/deleteDiagnosisCategory?id=' + $scope.Category.Id + ''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.diagnosisTypeCategoryList.indexOf($scope.Category);

                $scope.diagnosisTypeCategoryList.splice(index, 1);
            }

        }, function errorCallback(data) {

        });
    };

});