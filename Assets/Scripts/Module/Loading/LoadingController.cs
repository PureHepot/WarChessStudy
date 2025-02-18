using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadSomeScene
{


    //public void LoadtheScene(string SceneName, System.Action action)
    //{
    //    LoadingModel loadingModel = new LoadingModel();
    //    loadingModel.SceneName = SceneName;
    //    loadingModel.callback = action;
    //    controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    //}
}



/// <summary>
/// ���س���������
/// </summary>
public class LoadingController : BaseController
{
    AsyncOperation asyncOperation;

    public LoadingController() : base()
    {
        GameApp.ViewManager.Register(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, loadSceneCallback);
    }

    //���س����ص�
    private void loadSceneCallback(object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);

        GameApp.ViewManager.Open(ViewType.LoadingView);

        asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOperation.completed += onLoadEndCallback;
    }

    //���غ�ص�
    private void onLoadEndCallback(AsyncOperation operation)
    {
        asyncOperation.completed -= onLoadEndCallback;

        GetModel<LoadingModel>().callback?.Invoke();

        GameApp.ViewManager.Close(ViewType.LoadingView);
    }
}
