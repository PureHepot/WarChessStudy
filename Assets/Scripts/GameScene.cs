using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    float dt;

    public Texture2D mouseTxt;//图标图片

    private void Awake()
    {
        GameApp.Instance.Init();
    }

    private void Start()
    {
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        GameApp.SoundManager.PlayBGM("login");

        RegisterModule();//注册游戏中的控制器

        InitModule();
    }

    //注册控制器
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());

    }

    //执行所有控制器初始化
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    private void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
