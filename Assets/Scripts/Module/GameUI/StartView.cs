using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 开始游戏界面
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("startBtn").onClick.AddListener(onStartGameBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitGameBtn);
    }

    private void onStartGameBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LoadSomeScene.LoadtheScene(this.Controller, "map", () =>
        {
            Controller.ApplyControllerFunc(ControllerType.Level, Defines.OpenSelectLevelView);
        });
        
    }

    private void onSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    private void onQuitGameBtn()
    {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
        {
            MsgTxt = "确定退出游戏吗?",
            okCallback = () =>
            {
                Application.Quit();
            }
        });
    }
}
