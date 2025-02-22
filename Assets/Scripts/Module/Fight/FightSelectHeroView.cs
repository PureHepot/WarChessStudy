using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ս��ѡ��Ӣ����ͼ
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
        //���ûѡӢ���������ѡ��Ӣ��
        if (GameApp.FightWorldManager.heroes.Count == 0)
        {
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
            {
                MsgTxt = "û��ѡ��Ӣ��",
                okCallback = () =>
                {
                    GameApp.ViewManager.Close(ViewType.MessageView);
                }

            });
        }
        else
        {
            GameApp.ViewManager.Close(ViewId);//�رյ�ǰӢ�۽���

            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }
    }


}
