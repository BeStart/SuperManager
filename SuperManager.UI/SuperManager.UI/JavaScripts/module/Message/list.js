
$(function () {
    $("#tableList a[class='operater-info']").click(function (evt) {
        // 跳转到编辑页面
        InfoOperater($(this).attr("data-id"));
        // 阻止事件冒泡
        evt.stopPropagation();
    });
    var data = [
        [{
            "text": "查看留言",
            "func": function () {
                InfoOperater($(this).attr("id"));
            }
        }, {
            "text": "删除留言",
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
function InfoOperater(id) {
    location.href = $("#operaterInfoUrl").val().replace("-1", id);
}