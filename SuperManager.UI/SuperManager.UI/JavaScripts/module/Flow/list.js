
$(function () {
    $("#tableList a[class='operater-edit-flow']").click(function (evt) {
        // 跳转到编辑页面
        EditFlowOperater($(this).attr("data-id"));
        // 阻止事件冒泡
        evt.stopPropagation();
    });
    $("#tableList a[class='operater-auth']").click(function (evt) {
        // 跳转到编辑页面
        AuthFlowOperater($(this).attr("data-id"));
        // 阻止事件冒泡
        evt.stopPropagation();
    });
    var data = [
        [{
            "text": "添加流程",
            "func": function () {
                location.href = $("#operaterAddUrl").val();
            }
        }, {
            "text": "编辑流程",
            "func": function () {
                EditOperater($(this).attr("id"));
            }
        }, {
            "text": "删除流程",
            "func": function () {
                DeleteAskConfirmToUrl($(this).attr("id"));
            },
        }, {
            "text": "角色授权",
            "func": function () {
                AuthFlowOperater($(this).attr("id"));
            }
        }, {
            "text": "编辑步骤",
            "func": function () {
                EditFlowOperater($(this).attr("id"));
            }
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

function EditFlowOperater(id) {
    location.href = $("#operaterEditFlowUrl").val().replace("-1", id);
}
function AuthFlowOperater(id) {
    location.href = $("#operaterAuthUrl").val().replace("-1", id);
}