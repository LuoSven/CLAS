var IsToggle = true;
var BeginTask = "";
var BeginBreadcrumb = "";
$(function () {
    Global.Init();
})

var Global = {};
Global.OpenNewIframe = function (ob, url, id) {
    if (ob != undefined) {
        //更新面包屑，目前仅支持2级菜单
        var MainTitle = $('a', ob.parentNode.parentNode.parentNode).html();
        var SecondTitle = ob.innerHTML;
        $("#breadcrumb").html(MainTitle + ' » ' + SecondTitle);

       
        $(" .active").removeClass("active")
        //选中2级菜单加上背景色
        $(ob).addClass("active")
        //选中1级菜单加上背景色
        $(">a",ob.parentNode.parentNode.parentNode).addClass("active")
    }
    //更新iframe内容
    $("#mainIframe").attr("src", url);
}
Global.AutoIframe = function (id) {
    var ifm = $("#" + id)[0];
    if (ifm != undefined) {
        var subWeb = document.frames ? document.frames[id].document : ifm.contentDocument;
        if (ifm != null && subWeb != null) ifm.height = subWeb.body.scrollHeight;
    }
}
Global.FileUploader = function () {
    var b_version = navigator.appVersion
    var version = b_version.split(";")
    var browser = navigator.appName;
    var trim_Version = version[1].replace(/[ ]/g, "");
    //批次把上传按钮的样式改掉，并且绑定事件
    $(".file").each(function () {
        //生成控件和样式
        //IE8前面不能套其他样式
        if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") {
            $(this).before('<input type="text"  style=" float: left;" readonly="readOnly" />')
            $(this).attr("style", "width: 70px; float: left;")
            //获取载入时默认的文件名并且显示到前面的输入框
            var displayValue = $(this).attr("displayValue");
            if (displayValue != undefined) {
                displayValues = displayValue.split("/");
                displayValue = displayValues[displayValues.length - 1];
                $(this).prev().val(displayValue);
            }
            $(this).live('change', function () {
                var ImgPath = this.value.split("/");
                var ImgName = ImgPath[ImgPath.length - 1];
                $(this).attr("displayValue", ImgName);
                $(this).prev().val(ImgName);
            });
        }
        else {
            var id = this.id + "FileCover";
            $(this.parentNode).append('<button type="button" class="fileCover btn btn-default btn-xs" id="' + id + '">本地上传</button>')
            $(this).appendTo($('#' + id));
            $('#' + id).before('<input type="text"  readonly="readOnly" />')
            //获取载入时默认的文件名并且显示到前面的输入框
            var displayValue = $(this).attr("displayValue");
            if (displayValue != undefined) {
                displayValues = displayValue.split("/");
                displayValue = displayValues[displayValues.length - 1];
                $(this.parentNode).prev().val(displayValue);
            }


            $(this).live('change', function () {
                var ImgPath = this.value.split("/");
                var ImgName = ImgPath[ImgPath.length - 1];
                $(this).attr("displayValue", ImgName);
                $(this.parentNode).prev().val(ImgName);
            });
        }

    })
}
Global.DatePicker = function () {
    //时间插件目前用my97来做
    $(".datepicker").focus(function () {
        WdatePicker({ el: this.id })
    });
}
Global.MenuHover = function () {
    //绑定菜单title点击
    //为了防止菜单内容的ul也被点到，同时因为不想把事件放在a上，这样又要把img也绑上事件，目前是用背景的，点不到的说
    $(".body .menu>ul>li>ul").mouseover(function () { IsToggle = false }).mouseout(function () { IsToggle = true });
    $(".body .menu>ul>li").click(function () {
        if (IsToggle) {
            var a = $('a', this), ul = $('ul', this);
            
            ul.toggleClass('openul');
            if (a.hasClass('open')) a.removeClass('open'); else a.addClass('open');
        }
    })
}

Global.LogOff = function (){if(confirm("确认注销?"))location.href = "Account/LogOff";}
Global.DeleteConfirm = function (url) {
    if (confirm("确认删除？"))
        location.href = url;
}
Global.GetRandomCode = function () {
    var chars = ['_', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];

    var n = 16;
    var res = "";
    res += chars[Math.ceil(Math.random() * (chars.length - 13)) + 12];
    for (var i = 0; i < n - 1 ; i++) {
        var id = Math.ceil(Math.random() * chars.length - 1);
        res += chars[id];
    }
    return res;
}
Global.GetKeyWords = function (content) {
    //抓取关键词算法
    content.split("。")
}
Global.Init = function () {
    Global.MenuHover();
    $("#logOff").click(function () { Global.LogOff() });
    //暂时用这个办法解决iframe内部刷新高度会变化的问题
    if (window.location.href == top.location.href)
        setInterval(function () { Global.AutoIframe("mainIframe"); }, 500);
    Global.DatePicker();
    Global.FileUploader();
   // Global.OpenNewIframe(undefined, "/" + BeginTask.split(",")[0] + "/" + BeginTask.split(",")[1])
    $("#breadcrumb").html(BeginBreadcrumb);

}