using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadSomeScene
{

    public static void LoadtheScene(BaseController controller ,string sceneName, System.Action action)
    {
        LoadingModel loadingmodel = new LoadingModel();
        loadingmodel.SceneName = sceneName;
        loadingmodel.callback = action;
        controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingmodel);
    }
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

        GameApp.TimerManager.Register(0.25f, () =>
        {
            GetModel<LoadingModel>().callback?.Invoke();

            GameApp.ViewManager.Close(ViewType.LoadingView);
        });

        
    }
}
