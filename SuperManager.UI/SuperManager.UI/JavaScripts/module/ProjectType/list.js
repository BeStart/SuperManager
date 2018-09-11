
$(function () {

    var data = [
        [{
            "text": "添加项目分类",
            "func": function () {
                location.href = $("#operaterAddUrl").val();
            }
        }, {
            "text": "编辑项目分类",
            "func": function () {
                EditOperater($(this).attr("id"));
            }
        }, {
            "text": "删除项目分类",
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