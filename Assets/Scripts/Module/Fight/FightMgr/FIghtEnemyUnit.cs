using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIghtEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetHeroes();
        GameApp.ViewManager.Open(ViewType.TipView, "敌人回合");

        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));

        //敌人移动 使用技能
        for (int i = 0; i < GameApp.FightWorldManager.enemys.Count; i++)
        {
            Enemy enemy = GameApp.FightWorldManager.enemys[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));//等待
            GameApp.CommandManager.AddCommand(new AiMoveCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));
            GameApp.CommandManager.AddCommand(new SkillCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));

        }

        //等待一段时间 切换玩家回合
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, () =>
        {
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }));
        
    }
}
