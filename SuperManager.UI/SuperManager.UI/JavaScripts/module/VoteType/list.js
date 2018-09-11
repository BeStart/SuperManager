
$(function () {

    var data = [
        [{
            "text": "添加投票类别",
            "func": function () {
                location.href = $("#operaterAddUrl").val();
            }
        }, {
            "text": "编辑投票类别",
            "func": function () {
                EditOperater($(this).attr("id"));
            }
        }, {
            "text": "删除投票类别",
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