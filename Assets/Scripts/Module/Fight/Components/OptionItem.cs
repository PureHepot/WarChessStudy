using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//СЎПо
public class OptionItem : MonoBehaviour
{
    OptionData opData;

    public void Init(OptionData option)
    {
        opData = option;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameApp.MessageCenter.PostTempEvent(opData.EventName);
            GameApp.ViewManager.Close(ViewType.SelectOptionView);
        });
        transform.Find("txt").GetComponent<Text>().text = opData.Name;
    }
}
