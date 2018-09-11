
$(function () {

    CKEDITOR.replace("replyContent", {
        allowedContent: true,
        filebrowserUploadUrl: uploadFilePath,
        filebrowserImageUploadUrl: uploadImagePath,
    });
    
    $("#tableList a[class='operater-info']").click(function (evt) {
        // 跳转到编辑页面
        InfoOperater($(this).attr("data-id"));
        // 阻止事件冒泡
        evt.stopPropagation();
    });

    var menuMessageRead = {
        "text": "标记已读",
        "func": function () {
            ReadOperater($(this).attr("id"));
        },
    };
    var menuMessageReply = {
        "text": "回复留言",
        "func": function () {
            ReplyOperater($(this).attr("id"));
        },
    };
    var menuMessageDelete = {
        "text": "删除留言",
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

    var data = [
        [{
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
    var menuData = [];
    $("#tableList tr[data-menu='contextMenu']").smartMenu(menuData, {
        beforeShow: function () {
            $.smartMenu.remove();
            // 如果是默认
            if ($(this).attr("data-message-status") == "0") {
                menuData[0] = [menuMessageRead, menuMessageReply, menuMessageDelete, menuRefresh];
            }
                // 如果是已读状态
            else if ($(this).attr("data-message-status") == "1")
            {
                menuData[0] = [menuMessageReply, menuMessageDelete, menuRefresh];
            }
                // 如果是已回复状态
            else if ($(this).attr("data-message-status") == "2") {
                menuData[0] = [menuMessageDelete, menuRefresh];
            }
        }
    });
});