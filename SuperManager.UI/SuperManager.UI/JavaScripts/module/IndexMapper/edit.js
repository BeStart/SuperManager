$(function () {
    $("#indexType").change(function () {
        var html = "";
        // 获取当前选中的 value 内容
        var indexType = $(this).val();
        var itemList = indexJsonData[indexType];
        if (itemList != null) {
            for(var index = 0; index < itemList.length; index ++) {
                html += "<option value={value}>{name}</option>".replace(/[{]value[}]/gi, itemList[index].key).replace(/[{]name[}]/gi, itemList[index].value);
            }
        }
        $("#indexID").html(html);
    });
});