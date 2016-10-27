var labRecordCategoriesController = angular.module('labRecordCategoriesController', []);


labRecordCategoriesController.controller('labRecordCategoriesCtrl', function ($scope, $http) {
    
        $scope.labRecordCategoryList = [];
        $scope.labRecordCategoryListDisplay = [].concat($scope.labRecordCategoryList);

        $scope.submitType;

        $scope.disableInput = false;

        $http({
            method: 'GET',
            url: '/RecordTypes/getRecordCategories?ClassId=2'

        }).success(function (data) {
            $scope.labRecordCategoryList = data;

        });

        //ADD CATEGORY

        $scope.Category = {
            Id: '',
            Category: '',
            CategoryClassId: ''
        };

        $scope.showAddModal = function (row) {

            $scope.Category = row;

            $('#addModal').modal('toggle');

            $scope.submitType = 'add';

            $('#modalTitle').text('New');

        }

        $scope.updateCategory = function () {

            $scope.Category.CategoryClassId = 2;


            if ($scope.submitType == 'add') {

                $http({
                    method: 'POST',
                    url: '/RecordTypes/addRecordCategory/',
                    data: $.param({ category: $scope.Category }),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

                }).success(function (data) {

                    $scope.Category.Id = data;
                    $scope.labRecordCategoryList.push($scope.Category);
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

                        $scope.labRecordCategoryList.push($scope.Category);
                    }

                });
            }

        }

        //EDIT CATEGORY


        $scope.showEditModal = function (row) {

            $('#addModal').modal('toggle');

            $scope.Category = row;

            $scope.submitType = 'edit';

            $('#modalTitle').text('Edit');
        }


        //DELETE CATEGORY


        $scope.showDeleteModal = function (row) {

            $scope.Category = row;

            $('#deleteModal').modal('toggle');

            $('#CategoryName').text();

            $('#modalTitle').text('Delete');
        }


        $scope.deleteCategory = function () {


            $http({
                method: 'GET',
                url: '/RecordTypes/delete?id=' + $scope.Category.Id + ''
            }).success(function (data) {

                if (data == "ok") {

                    var index = $scope.labRecordCategoryList.indexOf($scope.Category);

                    $scope.labRecordCategoryList.splice(index, 1);
                }

            }, function errorCallback(data) {

            });
        };

});