using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս������
/// </summary>
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();

        GameApp.CommandManager.Clear();

        if (GameApp.FightWorldManager.heroes.Count == 0)
        {
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, () =>
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if(GameApp.FightWorldManager.enemys.Count == 0)
        {
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, () =>
            {
                GameApp.ViewManager.Open(ViewType.WinView);
            }));
        }
        else
        {

        }
        
    }

    public override bool Update(float dt)
    {
        return true;
    }
}
