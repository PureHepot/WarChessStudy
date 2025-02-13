using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一定义游戏中的管理器，在此类中进行初始化
/// </summary>
public class GameApp : Singleton<GameApp>
{
    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;
    public static ViewManager ViewManager;
    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
    }
}
