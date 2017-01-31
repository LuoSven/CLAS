using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CLAS.Common
{
    /// <summary>
    /// 脚本命令类型
    /// </summary>
    public enum InstructionCommandType
    {
        /// <summary>
        /// 找到并且移动到某点
        /// 0:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
        /// </summary>
        FindAndMoveTo=0,
        /// <summary>   
        /// 相对移动
        /// </summary>
        MoveToRelative=1,
        /// <summary>
        /// 鼠标点击
        /// </summary>
        MouseClick=2,
        /// <summary>
        /// 打字
        /// </summary>
        KeyPress=3,
        /// <summary>
        /// 鼠标双击
        /// </summary>
        MouseDoubleClick=4,
        /// <summary>
        /// 延时
        /// </summary>
        Delay=5,
        /// <summary>
        /// 获取字符串
        /// </summary>
        Ocr = 6,
        /// <summary>
        /// 获取字符串
        /// </summary>
        OcrInFile ,
        /// <summary>
        /// 添加字库
        /// </summary>
        AddDict,
        /// <summary>
        /// 批量添加字库
        /// </summary>
        UseDict,
        /// <summary>
        /// 批量添加字库
        /// </summary>
        GetDict,
        /// <summary>
        /// 批量添加字库
        /// </summary>
        GetDictCount ,
        /// <summary>
        /// 从文件载入字库
        /// </summary>
        SetDict,

        SetPath,
        Capture,
        CaptureAndUpload, 
        GetAveRGB,

        #region 结果型命令，返回true，false
        /// <summary>
        /// 找到某点
        /// Find:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
        /// </summary>
        Find = 200,
        #endregion




        #region 表现层函数
        /// <summary>
        /// 找到某点
        /// 300:数据失败，请输入700，然后点击价格
        /// </summary>
        ShowMessage = 300,
        /// <summary>
        /// 倒数 
        /// 301:10
        /// </summary>
        CountDown = 301,
        /// <summary>
        /// 计入日志
        /// </summary>
        Log=302,
        /// <summary>
        /// 设置发送时的价格
        /// </summary>
        Set48Price=303,
        /// <summary>
        /// 设置发送时的价格
        /// </summary>
        SetSendPrice = 304,
        /// <summary>
        /// 找到某点
        /// 305:删除按键记录
        /// </summary>
        ClearAllKey = 305,
        #endregion
       
    }

    /// <summary>
    /// 指令执行类型，分为执行指令和校验指令
    /// </summary>
    public enum InstructionExecuteType
    {
        /// <summary>
        /// 用来执行的命令
        /// </summary>
        [Description("执行")]
        Execute,
        /// <summary>
        /// 用来校验的命令
        /// </summary>
        [Description("校验")]
        Check
    }

    public enum ServerCommandType
    {
        /// <summary>
        /// 不进行任何更新
        /// </summary>
        [Description("不进行任何更新")]
        None,
        /// <summary>
        /// 同步策略
        /// </summary>
        [Description("同步策略")]
        SynTactics,
        /// <summary>
        /// 同步脚本
        /// </summary>
        [Description("同步脚本")]
        SynScript, 
    }
    /// <summary>
    /// 权限类型枚举
    /// </summary>
    public enum RightType
    {
        [Description("查询")]
        View = 1,
        [Description("增删改")]
        Form,
    }


    public enum PublicVariby
    {
        /// <summary>
        /// 51当前时间
        /// </summary>
        time51,
        /// <summary>
        /// 当前脚本键入数量
        /// </summary>
        keyDownCount,

        /// <summary>
        /// 所有键入数量
        /// </summary>
        allDownCount,
        /// <summary>
        /// 当前拍牌价格
        /// </summary>
        price,
        /// <summary>
        /// 加价价格
        /// </summary>
        addPrice,
        /// <summary>
        /// 差多少提交
        /// </summary>
        downReducePrice,
        /// <summary>
        /// 延时时间（毫秒）
        /// </summary>
        delayTime,
        /// <summary>
        /// 48秒价格
        /// </summary>
        price48,
    }

    /// <summary>
    /// 拍手表现记录表
    /// </summary>
    public enum BidderPerformanceRecordType
    {
        /// <summary>
        /// 拍中
        /// </summary>
        In=1,
        /// <summary>
        /// 价格已经送出
        /// </summary>
         Sended,
        /// <summary>
        /// 系统错误
        /// </summary>
        SystemError,
        
        
    }
}
