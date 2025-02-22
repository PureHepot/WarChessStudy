using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Null,//�հ�
    Obstacle, //�ϰ���
}

/// <summary>
/// ��ͼ�ϵĸ���
/// </summary>
public class Block : MonoBehaviour
{
    public int RowIndex;//���±�
    public int ColIndex;//���±�
    public BlockType Type;
    private SpriteRenderer selectSp;//ѡ�еĸ��ӵ�ͼƬ
    private SpriteRenderer gridSp;//����ͼƬ
    private SpriteRenderer dirSp;//�ƶ�����ͼƬ

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
    }

    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    public void HideGrid()
    {
        gridSp.enabled = false;
    }

    private void OnSelectCallback(object arg)
    {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }
    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    private void Start()
    {
        
    }
}
