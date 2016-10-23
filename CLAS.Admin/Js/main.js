var IsToggle = true;
var BeginTask = "";
var BeginBreadcrumb = "";
var $breadcrumb = $("#breadcrumb");
var OpenHistory = new Array();
var CloseHistory = new Array();
var OnCloseFlag = false;


Global.Main = {};
Global.Main.AutoBodyWidth = function () {
    //$(".body").css("width", ($("body").width() - 20) + "px");
    $(".TagsCover").css("width", ($(".iframeTags").width() - 66) + "px");
}
Global.Main.LogOff = function () { if (confirm("确认注销?")) location.href = "http://" + location.host + "/Account/LogOff"; }
Global.Main.ChangePassword = function () { Global.Iframe.OpenIframe("修改密码", "ChangePassword", "SystemUser/ChangePassword", false, false) }


Global.Iframe = {};
Global.Iframe.iframeId = "_iframe";
Global.Iframe.tabId = "_tab";
Global.Iframe.LoadingInterval;
Global.Iframe.LoadingPercent = 0;
Global.Iframe.Loadingdir = 2;
Global.Iframe.Loadinglen = 0;
Global.Iframe.HomeHtml = '<li class="title h40 fL34 pl10 pr10 active  dI " id="{id}" >首页</li>'
Global.Iframe.NewTagHtml = '<li class="title h40 fL34 pl20 pr5 active  dI " id="{id}" >{name}<span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span></li>'
Global.Iframe.NewIframeHtml = '<li id="{id}" ><iframe src="{url}" class="wp100 hp100"></iframe></li>';
Global.Iframe.OpenIframe = function (name, id, url, IsFromClose, IsRresh) {
    Global.Iframe.AutoIframeHeight();
    var ifrId = id + Global.Iframe.iframeId;
    var tabId = id + Global.Iframe.tabId;
    var ifr = $("#" + ifrId)[0];

    var IsOpening = $("#" + tabId).hasClass("active");
    //无论新开窗还是显示，都先要把.active和iframe的display:block去掉
    $(" .iframeTags   ul > .active").removeClass("active");
    $(" .iframeContents>ul>li").addClass("hidden");

    if(ifr==undefined)//新开窗
    {
        //首页的样式不一样
        if (id == 'home')
            $(" .iframeTags   ul").append(Global.Iframe.HomeHtml.format({ id: tabId, name: name }));
        else
           $(" .iframeTags   ul").append(Global.Iframe.NewTagHtml.format({ id: tabId, name: name }));
        $(" .iframeContents>ul").append(Global.Iframe.NewIframeHtml.format({ id: ifrId, url:"http://"+ location.host+"/"+ url }));
        //绑定事件
        $("#" + tabId).click(function () { Global.Iframe.ShowIframe(id) })
        $("#" + tabId).find("span").click(function () { Global.Iframe.CloseIframe(id) })
        Global.Iframe.Load($("#" + ifrId));


    }
    else//把已经开窗的显示出来
    {
        if (IsRresh)//默认刷新页面，通常是子页面要求刷新页面
        {
            Global.Iframe.Load($("#" + ifrId));
            var ifr = $("#" + ifrId + " iframe ")[0]
            ifr.src = url;
        }
        $("#" + ifrId).removeClass("hidden")
        $("#" + tabId).addClass("active")
    }
    if (!IsOpening && !IsFromClose)
    OpenHistory.push( {
        id: id,
        name: name,
        url: url
    })
    Global.Iframe.ShowTabSlice();
}
Global.Iframe.CloseIframe = function (id) {
    if ($(" .iframeTags   ul > li").length == 1)
    {
        Global.Utils.ShowMessage("请至少保持一项！");
        return;
    }

    id = id.replace(Global.Iframe.tabId, "");
    var ifr = $("#" + id + Global.Iframe.iframeId);
    var tab = $("#" + id + Global.Iframe.tabId);
    var url = ifr.attr("src");
    var name = tab.html();
    if (tab.hasClass("active"))
    {
        if (OpenHistory.length == 2)
        {
            i = 0;
            Global.Iframe.OpenIframe("", OpenHistory[i].id, "", true);
            OpenHistory = OpenHistory.slice(0, i + 1);
        }
        for (var i =OpenHistory.length-2; i >0 ; i--) {
            if ($("#"+OpenHistory[i].id + Global.Iframe.tabId).length)
            {

                Global.Iframe.OpenIframe("", OpenHistory[i].id, "", true);
                OpenHistory = OpenHistory.slice(0, i+1);
                break;
            }
        }
    }

    ifr.remove();
    tab.remove();

    CloseHistory.push({
        id: id,
        name: name,
        url: url
    });

    Global.Iframe.ShowTabSlice();
}
Global.Iframe.CloseIframeNow = function () {
    Global.Iframe.CloseIframe(Global.Iframe.GetNowId())
}
Global.Iframe.ShowIframe = function (id) {
        Global.Iframe.OpenIframe("", id, "");
}
Global.Iframe.RefreshIframe = function () {
    var nowIframe = Global.Iframe.GetNowIframe()
    if (nowIframe.length)
    {
        Global.Iframe.Load(nowIframe.parent())
        nowIframe[0].src = nowIframe.attr("src");
    }
    else
        Global.Utils.ShowMessage("未选择任何作业");
}
Global.Iframe.GetNowIframe = function () {
    return $("iframe", " .iframeContents>ul>li:not(.hidden)");
}

Global.Iframe.GetNowId = function () {
    return $(".iframeContents>ul>li:not(.hidden)")[0].id;
}

Global.Iframe.AutoIframe = function (id) {
    var ifm = $("#" + id)[0];
    if (ifm != undefined) {
        var subWeb = document.frames ? document.frames[id].document : ifm.contentDocument;
        if (ifm != null && subWeb != null) ifm.height = subWeb.body.scrollHeight;
    }
}
//调节iframe的高度
Global.Iframe.AutoIframeHeight = function () {
    //部分高度计算不到，仍然要减去,越小下面空隙越小，目前到30可以不出现滚动条
    var OtherHeight = 30;
    $(".iframeContents").css("height", ($(window).outerHeight() - $(".header").outerHeight() - $(".message").outerHeight() - $(".iframeTags").outerHeight() - OtherHeight) + "px");
}
//负责滑动的函数
Global.Iframe.TabSlice=function(direction)
{
    var MaxLeft=0;
    var MinLeft=$(".iframeTags").width() - 66 -$(" .iframeTags ul").width()
    var left = parseInt($(" .iframeTags ul").css("left").replace("px"), 10)
    var slicePx=150;
    if (direction == "left")//往左边话
    {
        if (left == MaxLeft)
            return;
        if (left + slicePx > MaxLeft)
            left = MaxLeft;
        else
            left += slicePx;
    }
    else {
        if (left == MinLeft)
            return;
        if (left - slicePx < MinLeft)
            left = MinLeft;
        else
            left -= slicePx;
    }
    $(" .iframeTags ul").css("left",left+"px")
}
//控制是否显示滑动按钮的函数,并且刷新ul的长度
Global.Iframe.ShowTabSlice = function () {

    var leftS = $(".iframeTags .leftSlice");
    var rightS = $(".iframeTags .rightSlice");
    var maxLength = $(".iframeTags").width() - 66;
    if ($(" .iframeTags ul").width() > maxLength)
    {
        leftS.removeClass("hidden");
        rightS.removeClass("hidden");
    }
    else
    {

        leftS.addClass("hidden");
        rightS.addClass("hidden");
    }
}
Global.Iframe.AutoTagUlWidth = function () {
    //刷新ul的长度
    var ulWidth = 0;
    var tagLis = $(".iframeTags .TagsCover ul li");
    for (var i = 0; i < tagLis.length; i++) {
        ulWidth += $(tagLis[i]).outerWidth(true);
        console.log(tagLis[i].offsetWidth)
        console.log($(tagLis[i]).outerWidth(true))
        console.log($(tagLis[i]).outerWidth(false))
    };
    $(".iframeTags .TagsCover ul").css("width", ulWidth + "px");
}

Global.Iframe.Load = function (iframeLi) {
    if($(".loadingCover", iframeLi).length==0)
    {
        var iframe= $("iframe", iframeLi)[0]
        iframe.onload = iframe.onreadystatechange = function () {
            $(".loadingCover", iframeLi).fadeOut(10);
            //if (this.readyState && this.readyState != 'complete') return;
            //else {
              
            //}
        }
        iframeLi.append('<div class="loadingCover" class="hp100 wp100 pAbs"><div class="loading" >…页面加载中…</div></div>')
    }
    else
    {
        $(".loadingCover", iframeLi).css("display", "block");
    }

}


Global.Menu = {};
Global.Menu.MenuHover = function () {
    $(".menu>ul>li>a").each(function () {
        $(this).click(function () {
            $("ul", this.parentNode).toggleClass("hidden");
        });
    })
}
Global.Menu.ClickMenu = function (ob) {
    if (ob != undefined) {
        //更新面包屑，目前仅支持2级菜单
        var MainTitle = $('a', ob.parentNode.parentNode.parentNode).html();
        var SecondTitle = ob.innerHTML;
        $breadcrumb.html(MainTitle + ' » ' + SecondTitle);


        $(".menu>ul>li>ul>li>.active").removeClass("active")
        //选中2级菜单加上背景色
        $(ob).addClass("active")
        //选中1级菜单加上背景色
        $(">a", ob.parentNode.parentNode.parentNode).addClass("active")
    }
};

Global.Menu.ShowUcTip = function () {
    layer.open({
        type: 1,
        title: false,
        closeBtn: 0,
        area: '955px',
        skin: 'layui-layer-nobg', //没有背景色
        shadeClose: true,
        content: $('#UcTip')
    });
}






