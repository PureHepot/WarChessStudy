using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ק������ͼ�����
/// </summary>
public class DragHeroView : BaseView
{
    private void Update()
    {
        //��ק�и�������ƶ� ��ʾ��ʱ������ƶ�
        if (_canvas.enabled == false)
        {
            return;
        }

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
    }

    public override void Open(params object[] args)
    {
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
