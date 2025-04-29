using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIghtEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetHeroes();
        GameApp.ViewManager.Open(ViewType.TipView, "���˻غ�");

        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));

        //�����ƶ� ʹ�ü���
        for (int i = 0; i < GameApp.FightWorldManager.enemys.Count; i++)
        {
            Enemy enemy = GameApp.FightWorldManager.enemys[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));//�ȴ�
            GameApp.CommandManager.AddCommand(new AiMoveCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));
            GameApp.CommandManager.AddCommand(new SkillCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));

        }

        //�ȴ�һ��ʱ�� �л���һغ�
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, () =>
        {
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }));
        
    }
}
