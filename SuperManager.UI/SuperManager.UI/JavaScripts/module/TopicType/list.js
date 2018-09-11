$(function () {
    $("#tableList tr").click(function () {
        var id = $(this).attr("id");
        if (id == null) return;
        ShowAndHideRow(this, id);
    });
    var menuAddRoot = {
        "text": "添加根级菜单",
        "func": function () {
            location.href = $("#operaterAddUrl").val().replace("-1", 0);
        }
    };
    var menuAddChild = {
        "text": "添加子类菜单",
        "func": function () {
            location.href = $("#operaterAddUrl").val().replace("-1", $(this).attr("id"));
        }
    };
    var menuEdit = {
        "text": "编辑菜单",
        "func": function () {
            EditOperater($(this).attr("id"));
        }
    };
    var menuDelete = {
        "text": "删除菜单",
        "func": function () {
            DeleteAskConfirmToUrl($(this).attr("id"));
        },
    };
    var menuRefresh = {
        "text": "刷新页面",
        "func": function () {
            location.reload();
        },
    };

    var menuData = [];
    $("#tableList tr[data-menu='contextMenu']").smartMenu(menuData, {
        beforeShow: function () {
            $.smartMenu.remove();
            if ($(this).attr("data-pid") == "0") {
                menuData[0] = [menuAddRoot, menuAddChild, menuEdit, menuDelete, menuRefresh];
            } else {
                menuData[0] = [menuAddRoot, menuEdit, menuDelete, menuRefresh];
            }
        }
    });
});