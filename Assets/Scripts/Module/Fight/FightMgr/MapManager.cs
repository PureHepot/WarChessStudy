using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//������ʾ�����ö�� ö���ַ�������ԴͼƬ·��һ��
public enum BlockDirection
{
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max
}

/// <summary>
/// ��ͼ�������� �洢�����ͼ������Ϣ
/// </summary>
public class MapManager
{
    private Tilemap tileMap;

    public Block[,] mapArr;

    public int RowCount;//��ͼ��
    public int ColCount;//��ͼ��

    public List<Sprite> dirSpArr;//�洢��ͷ����ͼƬ�ļ���

    //��ʼ����ͼ��Ϣ
    public void Init()
    {
        dirSpArr = new List<Sprite>();

        for (int i = 0; i < (int)BlockDirection.max; i++)
        {
            dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
        }


        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
        
        //��ͼ��С ���Խ������Ϣд�����ñ��н�������
        RowCount = 12;
        ColCount = 20;

        mapArr = new Block[RowCount, ColCount];

        List<Vector3Int> tempPosArr = new List<Vector3Int>();//��ʱ��¼��Ƭ��ͼÿ�����ӵ�λ��

        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }

        //��һά�����λ��ת���ɶ�ά�����Block ���д洢
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i < tempPosArr.Count; i++)
        {
            int row = i/ ColCount;
            int col = i % ColCount;
            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            b.RowIndex = row;
            b.ColIndex = col;
            b.transform.position = tileMap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f,0.5f,0);
            mapArr[row, col] = b;
        }
    }

    public BlockType GetBlockType(int row, int col)
    {
        return mapArr[row, col].Type;
    }
    

    public void ChangeBlockType(int row, int col, BlockType type)
    {
        mapArr[row,col].Type = type;
    }

    //��ʾ�ƶ�����
    public void ShowStepGrid(ModelBase model, int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);

        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.green);
        }
    }

    //�����ƶ�������
    public void HideStepGrid(ModelBase model, int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);

        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
        }
    }

    public void SetBlockDir(int rowIndex, int colIndex, BlockDirection dir, Color color)
    {
        mapArr[rowIndex, colIndex].SetDirSp(dirSpArr[(int)dir], color);
    }
}
