using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BlockType
{
    Null,//空白
    Obstacle, //障碍物
}

/// <summary>
/// 地图上的格子
/// </summary>
public class Block : MonoBehaviour
{
    public int RowIndex;//行下标
    public int ColIndex;//纵下标
    public BlockType Type;
    private SpriteRenderer selectSp;//选中的格子的图片
    private SpriteRenderer gridSp;//网格图片
    private SpriteRenderer dirSp;//移动方向图片

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

    //设置箭头方向的图片资源 和 颜色
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }
}
