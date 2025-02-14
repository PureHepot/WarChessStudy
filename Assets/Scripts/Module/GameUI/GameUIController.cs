using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����һЩ��Ϸͨ��ui�Ŀ�������������壬��ʾ��壬��ʼ��Ϸ���������������ע�ᣩ
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //ע����ͼ

        //��ʼ��Ϸ��ͼע��
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

        InitModuleEvent();//��ʼ��ģ���¼�
        InitGlobalEvent();//��ʼ��ȫ���¼�
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

    //����Ϸ��ʼ��ͼ
    private void openStartView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.StartView, args);
    }

    //���������
    private void openSetView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SetView, args);   
    }

    private void openMessageView(object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MessageView, args);
    }
}
