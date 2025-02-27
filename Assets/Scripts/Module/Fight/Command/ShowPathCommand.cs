using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示路径的指令
/// </summary>
public class ShowPathCommand : BaseCommand
{
    Collider2D pre;//鼠标之前检测到的2d碰撞盒子
    Collider2D current;//新的
    AStar astar;
    AStarPoint start;
    AStarPoint end;
    List<AStarPoint> prePath;//之前检测到的路径集合 用来清空用

    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePath = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);//执行未选中

            return true;
        }
        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);//检测当前鼠标位置是否有2d碰撞盒

        if (current != null)
        {
            //之前的检测碰撞盒和当前的盒子不一致 才进行 路径检测
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();

                if (b)
                {
                    //检测到Block脚本的物体 进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, UpdatePath);
                }
                else
                {
                    //没有检测到 将之前的路径清空
                    for (int i = 0; i < prePath.Count; i++)
                    {
                        GameApp.MapManager.mapArr[prePath[i].RowIndex, prePath[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePath.Clear();
                }
            }
        }

        return false;
    }

    private void UpdatePath(List<AStarPoint> path)
    {
        //如果之前已经有路径了， 要先清除
        if (prePath.Count != 0)
        {
            for (int i = 0; i < prePath.Count; i++)
            {
                  GameApp.MapManager.mapArr[prePath[i].RowIndex, prePath[i].ColIndex].SetDirSp(null, Color.white);
            }
        }

        for (int i = 0; i < path.Count; i++)
        {
            BlockDirection dir = BlockDirection.down;
            GameApp.MapManager.SetBlockDir(path[i].RowIndex, path[i].ColIndex, dir, Color.yellow);
        }

        prePath = path;
    }
}
