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

    }

    private void onSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    private void onQuitGameBtn()
    {

    }
}
