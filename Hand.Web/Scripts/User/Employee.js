var app = angular.module('app', ['ui.grid', 'ui.grid.selection', 'ui.grid.resizeColumns', 'ui.grid.pagination']);
app.config(function ($provide) {
    $provide.decorator('GridOptions', ['$delegate', 'i18nService', function ($delegate, i18NService) {
        var gridOptions = angular.copy($delegate);
        gridOptions.initialize = function (options) {
            var initOptions = $delegate.initialize(options);
            return initOptions;
        };
        i18NService.setCurrentLang('zh-cn');
        return gridOptions;
    }]);
});

app.controller('MainCtrl', function ($scope) {
    var employee = "";
    $.ajax({
        url: "http://10.211.52.112:1314/user/getEmp",
        type: "get",
        async: false,
        success: function (data) {
            employee = data;
        },
        error: function () {
            alert("数据获取失败");
        }
    });

    $scope.gridOptions = {
        enablePaginationControls: false,
        rowHeight: 60,
        paginationPageSize: 20,
        columnDefs:
        [
            { "field": "EmpId", "display": "none", visible: false },
            { "field": "EmpNo", "name": "员工工号" },
            { "field": "EmpName", "name": "员工姓名" },
            { "field": "EmpEmail", "name": "电子邮件", cellTooltip: function (row) { return 'Email: ' + row.entity.EmpEmail } },
            { "field": "EmpMobile", "name": "手机号码" },
            { "field": "EmpJoinTime", "name": "入职日期" },
            { "field": "EmpWorkAddress", "name": "工作地点", cellTooltip: function (row) { return 'Address: ' + row.entity.EmpWorkAddress } },
            { "field": "EmpRoleName", "name": "岗位职责" },
            { "field": "EmpIsValid", "name": "是否在职" },
            { "field": "EmpDeptName", "name": "所在部门" },
            { "field": "Button", "name": "操作", cellTemplate: '<div><input class="btnEmpOpreation" type="Button" value="编辑" ng-click="grid.appScope.editEmp(row.entity);" /><input id="btnConfig" type="Button" value="设为无效" ng-click="grid.appScope.inValid(row.entity);" /></div>' }
        ],
        data: employee
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
    };

    //编辑弹框
    $scope.editEmp = function (e) {
        layer.open({
            type: 2,
            title: "编辑",
            skin: 'layui-layer-rim', //加上边框onRegisterApi
            area: ['68%', '434px', '16%'], //宽高
            content: "/User/EditEmployee/?empId=" + e.EmpId
        });
    };

    //输入框数值限制
    $scope.maxpage = function () {
        if ($scope.page * $scope.gridOptions.paginationPageSize > $scope.gridOptions.data.count)
            $scope.page = Math.floor($scope.gridOptions.data.length / $scope.gridOptions.paginationPageSize) + 1;
        if ($scope.page <= 0) {
            $scope.page = 1;
            $scope.gridApi.pagination.seek(1);
        }
    };

    //上一页的限制
    $scope.pp = function () {
        if ($scope.page > 1)
            $scope.page = $scope.page - 1;
    }

    //下一页限制
    $scope.np = function () {
        if ($scope.page * $scope.gridOptions.paginationPageSize < $scope.gridOptions.data.length)
            $scope.page = $scope.page + 1;
    }
});