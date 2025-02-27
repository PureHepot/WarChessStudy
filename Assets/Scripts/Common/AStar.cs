using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ·����
/// </summary>
public class AStarPoint
{
    public int RowIndex;
    public int ColIndex;
    public int G;//��ǰ�ڵ㵽��ʼ��ľ���
    public int H;//��ǰ�ڵ㵽�յ�ľ���
    public int F;//F = G + H
    public AStarPoint Parent;//���ڵ�

    public AStarPoint(int row, int col)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = null;
    }
    public AStarPoint(int row, int col, AStarPoint parent)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = parent;
    }

    public int GetG()
    {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null)
        {
            _g += 1;
            parent = parent.Parent;
        }
        return _g;
    }

    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
}

public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open;//open��
    private Dictionary<string, AStarPoint> close;//close�� �Ѿ����ҹ���·���������������
    private AStarPoint start;//���
    private AStarPoint end;//�յ�

    public AStar(int rowCount, int colCount)
    {
        this.rowCount = rowCount;
        this.colCount = colCount;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();
    }

    public AStarPoint IsInOpen(int rowIndex, int colIndex)
    {
        for (int i = 0; i < open.Count; i++)
        {
            if (open[i].RowIndex == rowIndex && open[i].ColIndex == colIndex)
            {
                return open[i];
            }
        }
        return null;
    }

    public bool IsInClose(int rowIndex, int colIndex)
    {
        if (close.ContainsKey($"{rowIndex}_{colIndex}"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //1.���ߵ���뵽open��
    //2.����open���� fֵ��С��·��
    //3.���ҵ�����Сfֵ�ĵ��open���Ƴ�������ӵ�close��
    //4.����ǰ��·������Χ�ĵ���ӵ�open���������ҵĵ㣩
    //5.�ж��ص��Ƿ���open���У�������ھʹӲ���2������ʼִ��
    public bool FindPath(AStarPoint start, AStarPoint end, System.Action<List<AStarPoint>> findCallback)
    {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();

        //�������
        open.Add(start);
        while (true)
        {
            //ִ�е�2��
            AStarPoint current = GetMinFFromInOpen();
            if (current == null)
                return false;
            else
            {
                //3.1 ��open���Ƴ�
                open.Remove(current);
                //3.2 ��ӵ�close����
                close.Add($"{current.RowIndex}_{current.ColIndex}", current);
                //��4��
                AddAroundToOpen(current);
                //�ж��յ��Ƿ���open����
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if (endPoint != null)
                {
                    findCallback(GetPath(endPoint));
                    return true;
                }
                //����open�� ����F�Ĵ�С˳��
                open.Sort(OpenSort);
            }
        }
    }

    public List<AStarPoint> GetPath(AStarPoint point)
    {
        List<AStarPoint> path = new List<AStarPoint>();
        path.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null)
        {
            path.Add(parent);
            parent = parent.Parent;
        }

        path.Reverse();
        return path;
    }

    public int OpenSort(AStarPoint a, AStarPoint b)
    {
        return a.F - b.F;
    }

    public void AddAroundToOpen(AStarPoint current)
    {
        if (current.RowIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex - 1, current.ColIndex);
        }
        if (current.RowIndex + 1 < rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColIndex);
        }
        if (current.ColIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex, current.ColIndex - 1);
        }
        if (current.ColIndex + 1 < colCount)
        {
            AddOpen(current, current.RowIndex, current.ColIndex + 1);
        }
    }

    public void AddOpen(AStarPoint current, int row, int col)
    {
        //����open��close ���Ҳ������ϰ�����ܼ���open
        if (IsInClose(row, col) == false && IsInOpen(row, col) == null && GameApp.MapManager.GetBlockType(row, col) == BlockType.Null)
        {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }

    public AStarPoint GetMinFFromInOpen()
    {
        if(open.Count == 0)
            return null;
        return open[0];//���ȶ���
    }
}
