using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : ModelBase, ISkill
{
    public SkillProperty skills { get; set; }

    private Slider hpSlider;

    protected override void OnStart()
    {
        base.OnStart();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();

        data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);

        Type = int.Parse(data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        skills = new SkillProperty(int.Parse(data["Skill"]));
    }

    protected override void OnSelectCallBack(object arg)
    {
        if (GameApp.CommandManager.isRunningCommand == true)
        {
            return;
        }

        base.OnSelectCallBack(arg);

        GameApp.ViewManager.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);

        GameApp.ViewManager.Close(ViewType.EnemyDesView, this); 
    }

    public void ShowSkillArea()
    {
        
    }

    public void HideSkillArea()
    {
        
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
            GameApp.FightWorldManager.RemoveEnemy(this);
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
