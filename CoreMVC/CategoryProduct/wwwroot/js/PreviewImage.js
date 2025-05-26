function preview(inputFile) {
    var file = inputFile.files[0];
    var allowTypes = "image.*";
    if (file.type.match(allowTypes)) { //選中的檔案是否合乎圖片副檔名
        //預覽
        var reader = new FileReader();
        reader.onload = function (e) {  //設定處理load事件 通常用e表示事件之參數
            // $("img").attr("src",e.target.result); 因為在_PicturePartial生成img但在文件中並無img可能造成維護困難
            $("#Picture").prev().attr("src", e.target.result);//抓id picture的前一個
            $("#Picture").prev().attr("title", file.name);
        };
        reader.readAsDataURL(file);
        $("button").prop("disabled", false);
    }
    else {
        alert("不允許的檔案上傳類型");
        $("button").prop("disabled", true);
        inputFile.value = "";
    }
}
