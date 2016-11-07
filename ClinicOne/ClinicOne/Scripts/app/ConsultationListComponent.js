﻿var consultationListComponent = angular.module('consultationListComponent', ['smart-table']);

consultationListComponent.component('consultationList', {
    templateUrl: '/Static/ConsultationList.html',
     bindings: {
         myConsultations: '=',
         selectedRow: '='
    },

    controller: function MyController($http) {

        var $ctrl = this;

        $ctrl.consultationListDisplay = [].concat($ctrl.myConsultations);
        
        function ToJavaScriptDate(value) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return dt;
        }
        
        $ctrl.showDeleteModal = function (row) {

            $ctrl.selectedRow = row;

            $('#deleteModal').modal('toggle');
        }

        $ctrl.deleteConsultation = function () {

            var index = $ctrl.myConsultations.indexOf($ctrl.selectedRow);
            $ctrl.myConsultations.splice(index, 1);

        }

        $ctrl.setSelectedRow = function (row) {

            $ctrl.selectedRow = row;

        }



    }
});
