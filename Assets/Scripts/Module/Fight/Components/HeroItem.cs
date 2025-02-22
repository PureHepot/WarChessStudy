using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 处理拖拽的脚本
/// </summary>
public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Dictionary<string, string> data;
    private void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }


    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close(ViewType.DragHeroView);
        //检测拖拽后的位置是否有block脚本
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, (Collider2D col) =>
        {
            if (col != null)
            {
                Block b = col.GetComponent<Block>();
                if (b != null)
                {
                    Destroy(gameObject);
                    GameApp.FightWorldManager.AddHero(b, data);
                }
            }
        });
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
