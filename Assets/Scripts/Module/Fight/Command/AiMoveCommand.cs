using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人移动指令
/// </summary>
public class AiMoveCommand : BaseCommand
{
    Enemy enemy;
    _BFS bfs;
    List<_BFS.Point> paths;
    _BFS.Point current;
    int pathIdex;
    ModelBase target;
    public AiMoveCommand(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy;
        bfs = new _BFS(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
        paths = new List<_BFS.Point>();
    }

    public override void Do()
    {
        base.Do();
        target = GameApp.FightWorldManager.GetMinDisHero(enemy);//获取最近的英雄

        if (target == null)
        {
            isFinish = true;
        }
        else
        {
            paths = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);
            if(paths == null)
            {
                isFinish = true;
            }
            else
            {
                GameApp.MapManager.ChangeBlockType(this.enemy.RowIndex, this.enemy.ColIndex, BlockType.Null);//将原来位置设置为空

            }
        }
    }

    public override bool Update(float dt)
    {
        if (paths.Count == 0)
        {
            return base.Update(dt);
        }
        else
        {
            current = paths[pathIdex];
            if (model.Move(current.RowIndex, current.ColIndex, dt * 5) == true)
            {
                pathIdex++;
                if (pathIdex > paths.Count - 1)
                {
                    enemy.PlayAni("idle");
                    GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
                    return true;
                }
            }
        }
        model.PlayAni("move");

        return false;
    }
}
