
$(function () {

    var data = [
        [{
            "text": "添加流程类别",
            "func": function () {
                location.href = $("#operaterAddUrl").val();
            }
        }, {
            "text": "编辑流程类别",
            "func": function () {
                EditOperater($(this).attr("id"));
            }
        }, {
            "text": "删除流程类别",
            "func": function () {
                DeleteAskConfirmToUrl($(this).attr("id"));
            },
        }, {
            "text": "刷新页面",
            "func": function () {
                // 刷新页面
                location.reload();
            },
        }]
    ];
    $("#tableList tr[data-menu='contextMenu']").smartMenu(data);
});