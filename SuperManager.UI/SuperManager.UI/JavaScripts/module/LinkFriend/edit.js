$(function () {
    AsyncUploadFile("imagePhoto", uploadCoverPath, /(\.|\/)(gif|jpe?g|png)$/i, 10 * 1024 * 1024, "只能上传 JP(E)G/GIF/PNG 格式的图片！", "上传图片大小不能超过 10 M！", function (data) {
        if (data.Error != "") {
            ErrorAlert(data.Error);
            return;
        }
        UploadFileSuccessCallback("cover_url", "cover_link", "cover_item", data);
    }, null, null);
    $("#imagePhoto_delete").click(function () {
        UploadFileClearCallback("cover_item", "cover_url");
    });
});
function UploadFileSuccessCallback(url, link, container, data) {
    $("#" + url).attr("value", data.Data);
    $("#" + link).attr("href", data.Data);
    $("#" + container).show();
}
function UploadFileClearCallback(container, url) {
    $("#" + url).attr("value", "");
    $("#" + container).hide();
}