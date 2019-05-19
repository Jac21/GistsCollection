(function () {
    "use strict";
    angular
        .module("productManagement")
        .controller("ProductListCtrl",
                     ["productResource", ProductListCtrl]);

    function ProductListCtrl(productResource) {
        var vm = this;

        vm.searchCriteria = "GDN";
        vm.sortProperty = "Price";
        vm.sortDirection = "desc";

        /*
         * e.g.,
         * { $skip: 1, $top: 10, $filter: "contains(ProductCode, 'GDN') and Price ge 5 and Price le 20" }
         * 
         * {
            $filter: "contains(ProductCode, '" +
                vm.searchCriteria + "')" +
                " or contains (ProductName, '" +
                vm.searchCriteria + "')",
            $orderby: vm.sortProperty + " " + vm.sortDirection
        },
         */

        productResource.query(
        function (data) {
            vm.products = data;
        });
    }
}());
