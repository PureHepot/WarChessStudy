using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������������㷨
/// </summary>
public class _BFS
{
    public class Point
    {
        public int RowIndex;//������
        public int ColIndex;//������
        public Point Father;//���ڵ� ��������·��

        public Point(int row, int col)
        {
            this.RowIndex = row;
            this.ColIndex = col;
        }
        public Point(int row, int col, Point father)
        {
            this.RowIndex = row;
            this.ColIndex = col;
            this.Father = father;
        }

    }


    public int RowCount;//������
    public int ColCount;//������

    public _BFS(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        finds = new Dictionary<string, Point>();
    }

    public Dictionary<string, Point> finds;//�洢�ҵ��ĵ���ֵ䣨key:�������ƴ���ַ��� value:�����㣩

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="row">��ʼ���������</param>
    /// <param name="col">��ʼ���������</param>
    /// <param name="step">����</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step)
    {
        //������������
        List<Point> searchs = new List<Point>();
        //��ʼ��
        Point startPoint = new Point(row, col);
        //����ʼ��洢����������
        searchs.Add(startPoint);
        //��ʼ��Ĭ�Ͽ�ʼ�Ѿ��ҵ����洢���Ѿ��ҵ��ĵ�ļ���
        finds.Add($"{row}_{col}", startPoint);
        for (int i = 0; i < step; i++)
        {
            List<Point> temps = new List<Point>();
            //���������ϼ�
            for (int j = 0; j < searchs.Count; j++)
            {
                Point current = searchs[j];
                //��ѯ��ǰ�����ܵĵ�
                FindAroundPoints(current, temps);
            }
            if (temps.Count == 0)
            {
                break;
            }
            //���Ҫ�����ļ���
            searchs.Clear();
            //����ʱ���ϵĵ���ӵ�����������
            searchs.AddRange(temps);
        }
        return finds.Values.ToList();
    }


    public void FindAroundPoints(Point current, List<Point> temps)
    {
        //��
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex - 1, current.ColIndex, current, temps);
        }
        //��
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex + 1, current.ColIndex, current, temps);
        }
        //��
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex - 1, current, temps);
        }
        //��
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex + 1, current, temps);
        }
    }

    public void AddFinds(int row, int col, Point father, List<Point> temps)
    {
        //���ڲ��ҵĽڵ� �� ��Ӧ��ͼ���Ӳ����ϰ��� ���ܼ��� �����ֵ�
        if (finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row, col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, father);
            //��ӵ���ѯ�����ֵ�
            finds.Add($"{row}_{col}", p);
            //�����ʱ���������´β���
            temps.Add(p);
        }
    }
}
