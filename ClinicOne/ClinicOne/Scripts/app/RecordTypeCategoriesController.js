var recordTypeCategoriesController = angular.module('recordTypeCategoriesController', []);


recordTypeCategoriesController.controller('recordTypeCategoriesCtrl', function ($scope, $http) {

    $scope.recordTypeCategoryList = [];
    $scope.recordTypeCategoryListDisplay = [].concat($scope.recordTypeCategoryList);

    $scope.submitType;

    $scope.disableInput = false;

    $http({
        method: 'GET',
        url: '/RecordTypes/geRecordCategories/'
    }).success(function (data) {

        $scope.recordTypeCategoryList = data;

    }, function errorCallBack(data) {

    });

    //ADD CATEGORY

    $scope.Category = {
        Id: '',
        Category: ''
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
                url: '/RecordTypes/addRecordCategory/',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.Category.Id = data;
                $scope.recordTypeCategoryList.push($scope.Category);
            });
        }
        else {

            $http({
                method: 'POST',
                url: '/RecordTypes/editRecordCategory',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                if (data == 'ok') {

                    $scope.recordTypeCategoryList.push($scope.Category);
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
            url: '/RecordTypes/delete?id=' + $scope.Category.Id + ''
        }).success(function (data) {

            if (data == "ok") {

                var index = $scope.recordTypeCategoryList.indexOf($scope.Category);

                $scope.recordTypeCategoryList.splice(index, 1);
            }

        }, function errorCallback(data) {

        });
    };

});