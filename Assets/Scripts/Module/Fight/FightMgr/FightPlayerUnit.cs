using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҵĻغ�
/// </summary>
public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetEnemys();
        GameApp.ViewManager.Open(ViewType.TipView, "��һغ�");
    }
}
