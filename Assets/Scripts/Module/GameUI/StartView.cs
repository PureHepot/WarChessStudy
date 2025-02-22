using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��ʼ��Ϸ����
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
            MsgTxt = "ȷ���˳���Ϸ��?",
            okCallback = () =>
            {
                Application.Quit();
            }
        });
    }
}
