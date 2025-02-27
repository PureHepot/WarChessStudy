using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ModelBase
{
    protected override void OnStart()
    {
        base.OnStart();

        data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);

        Type = int.Parse(data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    protected override void OnSelectCallBack(object arg)
    {
        base.OnSelectCallBack(arg);

        GameApp.ViewManager.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);

        GameApp.ViewManager.Close(ViewType.EnemyDesView, this); 
    }
}
