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

app.controller('WorkFlow', function ($scope) {
    var workFlow = "";
    $.ajax({
        url: "http://10.211.52.112:1314/work/getWorkFlow/30568",
        type: "get",
        async: false,
        success: function (data) {
            workFlow = data;
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
            { "field": "WorkId", "display": "none", visible: false },
            { "field": "WorkTitle", "name": "标题" },
            { "field": "WorkContent", "name": "内容", cellTooltip: function (row) { return '内容: ' + row.entity.WorkContent } },
            { "field": "WorkCreateTime", "name": "创建时间" },
            { "field": "DeptLeader", "name": "审批人" },
            { "field": "WorkStatus", "name": "审批状态" }
        ],
        data: workFlow
    };
});