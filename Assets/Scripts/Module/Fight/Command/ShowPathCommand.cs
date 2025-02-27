using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʾ·����ָ��
/// </summary>
public class ShowPathCommand : BaseCommand
{
    Collider2D pre;//���֮ǰ��⵽��2d��ײ����
    Collider2D current;//�µ�
    AStar astar;
    AStarPoint start;
    AStarPoint end;
    List<AStarPoint> prePath;//֮ǰ��⵽��·������ ���������

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
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);//ִ��δѡ��

            return true;
        }
        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);//��⵱ǰ���λ���Ƿ���2d��ײ��

        if (current != null)
        {
            //֮ǰ�ļ����ײ�к͵�ǰ�ĺ��Ӳ�һ�� �Ž��� ·�����
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();

                if (b)
                {
                    //��⵽Block�ű������� ����Ѱ·
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, UpdatePath);
                }
                else
                {
                    //û�м�⵽ ��֮ǰ��·�����
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
        //���֮ǰ�Ѿ���·���ˣ� Ҫ�����
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
