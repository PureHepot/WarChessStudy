using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 英雄脚本
/// </summary>
public class Hero : ModelBase, ISkill
{
    public SkillProperty skills { get; set; }
    private Slider hpSlider;

    protected override void OnStart()
    {
        base.OnStart();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
    }

    public void Init(Dictionary<string,string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Type = int.Parse(data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        skills = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    protected override void OnSelectCallBack(object arg)
    {
        if (GameApp.FightWorldManager.state == GameState.Player)
        {
            if (isStop)
            {
                return;
            }
            if (GameApp.CommandManager.isRunningCommand == true)
            {
                return;
            }
            //执行未选中
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
            if (isStop == false)
            {
                //显示路径
                GameApp.MapManager.ShowStepGrid(this, Step);
                //添加显示路径指令
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                //添加选项事件
                AddOptionEvents();
            }

            base.OnSelectCallBack(arg);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    private void AddOptionEvents()
    {
        GameApp.MessageCenter.AddTempEvent(Defines.OnAttackEvent, onAttackCallback);
        GameApp.MessageCenter.AddTempEvent(Defines.OnIdleEvent, onIdleCallback);
        GameApp.MessageCenter.AddTempEvent(Defines.OnCancelEvent, onCancelCallback);
    }

    private void onAttackCallback(object arg)
    {
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
    }

    private void onIdleCallback(object arg)
    {
        isStop = true;
    }

    private void onCancelCallback(object arg)
    {
        GameApp.CommandManager.Undo();
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close(ViewType.HeroDesView, this);
    }

    public void ShowSkillArea()
    {
        GameApp.MapManager.ShowAttackStep(this, skills.AttackRange, Color.red);
    }

    public void HideSkillArea()
    {
        GameApp.MapManager.HideAttackStep(this, skills.AttackRange);
    }

    public override void GetHit(ISkill skill)
    {
        GameApp.SoundManager.PlayEffect("hit", transform.position);
        CurHp -= skill.skills.Attack;

        GameApp.ViewManager.ShowHitNum($"-{skill.skills.Attack}", Color.red, transform.position);

        PlayEffect(skill.skills.AttackEffect);

        if (CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");
            Destroy(gameObject, 1.2f);
            GameApp.FightWorldManager.RemoveHero(this);
        }

        StopAllCoroutines();

        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateSlider());
    }

    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }
}
