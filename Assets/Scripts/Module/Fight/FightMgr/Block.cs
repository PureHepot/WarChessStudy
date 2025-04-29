using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
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
        if (GameApp.CommandManager.isRunningCommand == false)
        {
            GameApp.ViewManager.Open(ViewType.FightOptionDesView);
        }
    }
    private void OnUnSelectCallback(object arg)
    {
        dirSp.sprite = null;
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

    //���ü�ͷ�����ͼƬ��Դ �� ��ɫ
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }
}
