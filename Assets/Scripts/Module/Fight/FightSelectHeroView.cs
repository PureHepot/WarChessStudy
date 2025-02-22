using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 战斗选择英雄视图
/// </summary>
public class FightSelectHeroView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);


        GameObject prefabObj = Find("bottom/grid/item");

        Transform gridTf = Find("bottom/grid").transform;

        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++)
        {
            Dictionary<string,string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(GameApp.GameDataManager.heros[i]);

            GameObject obj = Object.Instantiate(prefabObj, gridTf);

            obj.SetActive(true);

            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }

    private void onFightBtn()
    {
        //如果没选英雄提醒玩家选择英雄
        if (GameApp.FightWorldManager.heroes.Count == 0)
        {
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
            {
                MsgTxt = "没有选择英雄",
                okCallback = () =>
                {
                    GameApp.ViewManager.Close(ViewType.MessageView);
                }

            });
        }
        else
        {
            GameApp.ViewManager.Close(ViewId);//关闭当前英雄界面

            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }
    }


}
