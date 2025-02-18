using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡控制器
/// </summary>
public class LevelController : BaseController
{

    public LevelController() : base()
    {
        SetModel(new LevelModel());
        GameApp.ViewManager.Register(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 0,
        });

        InitModuleEvent();
        InitGlobalEvent();
    }


    public override void Init()
    {
        model.Init();//初始化数据

    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, openSelectLevelView);
    }

    public override void InitGlobalEvent()
    {
        GameApp.MessageCenter.AddEvent(Defines.ShowLevelEvent, onShowLevelCallback);
        GameApp.MessageCenter.AddEvent(Defines.HideLevelEvent, onHideLevelCallback);
    }

    public override void RemoveGlobalEvent()
    {
        GameApp.MessageCenter.RemoveEvent(Defines.ShowLevelEvent, onShowLevelCallback);
        GameApp.MessageCenter.RemoveEvent(Defines.HideLevelEvent, onHideLevelCallback);
    }

    private void openSelectLevelView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SelectLevelView, args);
    }
    
    private void onShowLevelCallback(object args)
    {
        //Debug.Log("levelId:" + args.ToString());
        LevelModel levelModel = GetModel<LevelModel>();
        levelModel.current = levelModel.GetLevel(int.Parse(args.ToString()));

        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevel();
    }

    private void onHideLevelCallback(object args)
    {
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevel();
    }
}
