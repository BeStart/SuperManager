
$(function () {

    var data = [
        [{
            "text": "添加用户",
            "func": function () {
                location.href = $("#operaterAddUrl").val();
            }
        }, {
            "text": "编辑用户",
            "func": function () {
                EditOperater($(this).attr("id"));
            }
        }, {
            "text": "删除用户",
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