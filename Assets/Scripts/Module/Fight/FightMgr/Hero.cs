using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 英雄脚本
/// </summary>
public class Hero : ModelBase
{
    public void Init(Dictionary<string,string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Type = int.Parse(data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    protected override void OnSelectCallBack(object arg)
    {
        if (GameApp.FightWorldManager.state == GameState.Player)
        {
            if (isStop)
            {
                return;
            }
            if (GameApp.CommandManager.isRunningCommand == true)
            {
                return;
            }

            //添加显示路径指令
            GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

            base.OnSelectCallBack(arg);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close(ViewType.HeroDesView, this);
    }
}
