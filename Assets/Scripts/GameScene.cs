using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    float dt;

    public Texture2D mouseTxt;//ͼ��ͼƬ

    private void Awake()
    {
        GameApp.Instance.Init();
    }

    private void Start()
    {
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        GameApp.SoundManager.PlayBGM("login");

        RegisterModule();//ע����Ϸ�еĿ�����

        InitModule();
    }

    //ע�������
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());

    }

    //ִ�����п�������ʼ��
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
