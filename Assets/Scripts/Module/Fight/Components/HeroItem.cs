using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������ק�Ľű�
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

    //��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close(ViewType.DragHeroView);
        //�����ק���λ���Ƿ���block�ű�
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
