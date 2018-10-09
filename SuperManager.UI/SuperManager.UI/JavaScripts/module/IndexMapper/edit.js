$(function () {
    $("#indexType").change(function () {
        // 获取当前选中的 value 内容
        var indexType = $(this).val();
        SetMenuListGetJsonData(indexJsonData, indexType, "indexID");
        SetMenuListGetJsonData(mapperJsonData, indexType, "mapperID");
    });
});