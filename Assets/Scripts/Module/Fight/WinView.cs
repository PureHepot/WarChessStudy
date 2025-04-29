using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();

        Find<Button>("bg/okBtn").onClick.AddListener(onOkBtn);
    }

    private void onOkBtn()
    {
        GameApp.FightWorldManager.ReLoadRes();

        GameApp.ViewManager.CloseAll();
        //ÇÐ»»³¡¾°
        LoadingModel load = new LoadingModel();
        load.SceneName = "map";
        load.callback = () =>
        {
            GameApp.SoundManager.PlayBGM("mapbgm");
            GameApp.ViewManager.Open(ViewType.SelectLevelView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading,Defines.LoadingScene,load);
    }
}
