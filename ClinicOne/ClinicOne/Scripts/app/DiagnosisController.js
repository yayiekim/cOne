var diagnosisController = angular.module('diagnosisController', []);



diagnosisController.controller('diagnosisCtrl', function ($scope, $http) {

    $scope.diagnosisList = [];
    $scope.diagnosisListDisplay = [].concat($scope.diagnosisList);

    $scope.submitType;

    $scope.diagnosisCategoryList = [];


    $scope.diagnosis = {

        'Id': '',
        'CategoryId': '',
        'CategoryName': '',
        'DiagnosisName': '',
        'Description': ''
    };

    //GET DIAGNOSIS

    $http({
        method: 'GET',
        url: '/Diagnosis/getDiagnosis'
    }).success(function (data) {

        $scope.diagnosisList = data;
    });

    //ADD DIAGNOSIS

    $scope.showAddModal = function () {

        $scope.diagnosis = {

            'Id': '',
            'CategoryId': '',
            'CategoryName': '',
            'DiagnosisName': '',
            'Description': ''
        };

        $('#addModal').modal('toggle');

        $scope.getDiagnosisCategory();

        $scope.submitType = 'add';

        $('#modalTitle').text('New');
    }

    //FOR DROPDOWNLIST DIAGNOSIS CATEGORY
    

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
                url: '/Diagnosis/addDiagnosisCategory/',
                data: $.param({ category: $scope.Category }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.Category.Id = data;
                $scope.diagnosisCategoryList.push($scope.Category);

                $scope.Category = {
                    Id: '',
                    CategoryName: ''
                };

            });

    }



    $scope.getDiagnosisCategory = function () {

        $http({

            method: 'GET',
            url: '/Diagnosis/getDiagnosisCategories'
        }).success(function (data) {

            $scope.diagnosisCategoryList = data;
        });
    }


    //UPDATE DIAGNOSIS

    $scope.updateDiagnosis = function () {

        if ($scope.submitType == 'add') {

            $http({
                method: 'POST',
                url: '/Diagnosis/addDiagnosis',
                data: $.param({ diagnosis: $scope.diagnosis }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {


                $scope.diagnosisList.push(data);

            });
        }

        else if ($scope.submitType == 'edit') {


            $http({
                method: 'POST',
                url: '/Diagnosis/editDiagsosis',
                data: $.param({ diagnosis: $scope.diagnosis }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).success(function (data) {

                $scope.diagnosis.CategoryName = data.CategoryName;

            });
        }


    }


    //DELETE DIAGNOSIS

    $scope.showDeleteModal = function (row) {

        $scope.diagnosis = row;

        $('#deleteModal').modal('toggle');
    }

    $scope.deleteDiagnosis = function () {

        $http({
            method: 'GET',
            url: '/Diagnosis/deleteDiagnosis?id=' + $scope.diagnosis.Id + ''

        }).success(function (data) {

            if (data == 'ok') {

                var index = $scope.diagnosisList.indexOf($scope.diagnosis);

                $scope.diagnosisList.splice(index, 1)
            }
        });
    }


    //EDIT DIAGNOSIS

    $scope.showEditModal = function (row) {

        $('#addModal').modal('toggle');

        $scope.diagnosis = row;

        $scope.submitType = 'edit';

        $scope.getDiagnosisCategory();

        $('#modalTitle').text('Edit');
    }

});