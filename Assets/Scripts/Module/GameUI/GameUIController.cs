using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理一些游戏通用ui的控制器（设置面板，提示面板，开始游戏面板等在这个控制器注册）
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //注册视图

        //开始游戏视图注册
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf

        });
        GameApp.ViewManager.Register(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            Sorting_Order = 1,
            parentTf = GameApp.ViewManager.canvasTf
        });
        GameApp.ViewManager.Register(ViewType.MessageView, new ViewInfo()
        {
            PrefabName = "MessageView",
            controller = this,
            Sorting_Order = 999,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent();//初始化模板事件
        InitGlobalEvent();//初始化全局事件
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView);
        RegisterFunc(Defines.OpenSetView, openSetView);
        RegisterFunc(Defines.OpenMessageView, openMessageView);
    }

    public override void InitGlobalEvent()
    {
        base.InitGlobalEvent();
    }

    //打开游戏开始视图
    private void openStartView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.StartView, args);
    }

    //打开设置面板
    private void openSetView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SetView, args);   
    }

    private void openMessageView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MessageView, args);
    }
}
