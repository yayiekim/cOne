var labRecordsController = angular.module('labRecordsController', []);


labRecordsController.controller('labRecordsCtrl', function ($scope, $http) {

    $scope.labRecordList = [];
    $scope.labRecordListDisplay = [].concat($scope.labRecordList);

    $scope.recordTypeCategoryList = [];
    $scope.valueTypeList = [];

    //FOR DROPDOWN LIST RECORD CATEGORIES


    $scope.showQuickAdd = function () {

        $('#quickAddModal').modal('toggle');

    };

    $scope.Category = {
        Id: '',
        Category: '',
        CategoryClassId: ''
    };


    $scope.updateCategory = function () {

        $scope.Category.CategoryClassId = 2;

        $http({
            method: 'POST',
            url: '/RecordTypes/addRecordCategory/',
            data: $.param({ category: $scope.Category }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        }).success(function (data) {

            $scope.Category.Id = data;
            $scope.recordTypeCategoryList.push($scope.Category);

            $scope.Category = {
                Id: '',
                Category: '',
                CategoryClassId: ''
            };
        });
    }

    $scope.getRecordTypeCategories = function () {

        $http({
            method: 'GET',
            url: '/RecordTypes/getRecordCategories?ClassId=2'
        }).success(function (data) {

            $scope.recordTypeCategoryList = data;

        }, function errorCallBack(data) {

        });
    }


    //FOR DROPDOWN LIST VALUE TYPES CATEGORIES

    $http({
        method: 'GET',
        url: '/RecordTypes/getValueTypes/'
    }).success(function (data) {

        $scope.valueTypeList = data;
    });


    //GET RECORDTYPES

    $http({
        method: 'GET',
        url: '/RecordTypes/getRecordTypes?classId=2'
    }).success(function (data) {

        $scope.labRecordList = data;
    });


    //ADD RECORDTYPES

    $scope.recordTypes = {

        Id: '',
        RecordTypeName: '',
        RecordTypeCategoryId: '',
        RecordTypeCategoryName: '',
        ValueTypeId: '',
        ValueTypeName: ''
    };

    $scope.showAddModal = function () {

        $scope.recordTypes = {

            Id: '',
            RecordTypeName: '',
            RecordTypeCategoryId: '',
            RecordTypeCategoryName: '',
            ValueTypeId: '',
            ValueTypeName: ''
        };

        $('#addModal').modal('toggle');

        $scope.getRecordTypeCategories();

        $scope.submitType = 'add';

        $('#modalTitle').text('New');
    };


    $scope.updateRecordType = function () {

        if ($scope.submitType == 'add') {

            $http({
                method: 'POST',
                url: '/RecordTypes/addRecordType/',
                data: $.param({ recordType: $scope.recordTypes }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.labRecordList.push(data);
            });
        }
        else {

            $http({
                method: 'POST',
                url: '/RecordTypes/editRecordType/',
                data: $.param({ recordType: $scope.recordTypes }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {

                $scope.recordTypes.RecordTypeCategoryName = data.RecordTypeCategoryName;
                $scope.recordTypes.ValueTypeName = data.ValueTypeName;
            });

        }


    }


    //DELETE RECORD TYPE

    $scope.showDeleteModal = function (row) {

        $scope.recordTypes = row;

        $('#deleteModal').modal('toggle');

    }


    $scope.deleteRecordType = function () {

        $http({
            method: 'GET',
            url: '/RecordTypes/deleteRecordType?id=' + $scope.recordTypes.Id + ''

        }).success(function (data) {

            if (data == 'ok') {

                var index = $scope.labRecordList.indexOf($scope.recordTypes);

                $scope.labRecordList.splice(index, 1)
            }
        });
    }


    //EDIT RECORD TYPE

    $scope.showEditModal = function (row) {

        $('#addModal').modal('toggle');

        $scope.recordTypes = row;

        $scope.submitType = 'edit';

        $scope.getRecordTypeCategories();

        $('#modalTitle').text('Edit');
    }


});