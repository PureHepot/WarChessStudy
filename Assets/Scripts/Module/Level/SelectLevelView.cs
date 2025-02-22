using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择关卡信息视图
/// </summary>
public class SelectLevelView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
    }
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);
    }

    private void onCloseBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LoadSomeScene.LoadtheScene(this.Controller, "game", () =>
        {
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        });

    }

    public void ShowLevel()
    {
        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Destination;
    }

    public void HideLevel()
    {
        Find("level").SetActive(false);
    }

    //进入战斗
    private void onFightBtn()
    {
        //关闭当前界面
        GameApp.ViewManager.Close(this.ViewId);
        //充值摄像机
        GameApp.CameraManager.ResetPos();
        LoadSomeScene.LoadtheScene(this.Controller, Controller.GetModel<LevelModel>().current.SceneName, () =>
        {
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
        });
    }
}
