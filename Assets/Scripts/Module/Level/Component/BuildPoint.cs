using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId;//…Ë÷√πÿø®Id

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.ShowLevelEvent, LevelId);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.HideLevelEvent, LevelId);
    }
}
