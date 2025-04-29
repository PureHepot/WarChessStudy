using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ܰ�����
/// </summary>
public static class SkillHelper
{
    /// <summary>
    /// Ŀ���Ƿ��ڼ��ܵķ�Χ��
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool IsModelInSkillArea(this ISkill skill, ModelBase target)
    {
        ModelBase current = (ModelBase)skill;
        if (current.GetDis(target) <= skill.skills.AttackRange)
        {
            return true;
        }
        return false;
    }

    public static List<ModelBase> GetTarget(this ISkill skill)
    {
        //0:�������
        //1:������ΧĿ��
        //2:����Ӣ��Ŀ��
        switch (skill.skills.Target)
        {
            case 0:
                return GetTarget_0(skill);
            case 1:
                return GetTarget_1(skill);
            case 2:
                return GetTarget_2(skill);
        }
        return null;
    }

    public static List<ModelBase> GetTarget_0(ISkill skill)
    {
        List<ModelBase> results =new List<ModelBase>();
        Collider2D col = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
        if(col !=null)
        {
            ModelBase target = col.GetComponent<ModelBase>();
            if(target !=null)
            {
                //���ܵ�Ŀ������ �� ����ָ���Ŀ������Ҫ�����ñ�һ��
                if (skill.skills.TargetType == target.Type)
                {
                    results.Add(target);
                }
            }
        }
        return results;
    }

    //1:������Χ������Ŀ��
    public static List<ModelBase> GetTarget_1(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for(int i = 0; i < GameApp.FightWorldManager.heroes.Count; i++)
        {
            //�ҵ��ڼ��ܷ�Χ�ڵ�Ŀ��
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.heroes[i]))
            {
                results.Add(GameApp.FightWorldManager.heroes[i]);
            }

        }
        for (int i = 0; i < GameApp.FightWorldManager.enemys.Count; i++)
        {
            //�ҵ��ڼ��ܷ�Χ�ڵ�Ŀ��
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.enemys[i]))
            {
                results.Add(GameApp.FightWorldManager.enemys[i]);
            }

        }

        return results;
    }

    public static List<ModelBase> GetTarget_2(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightWorldManager.heroes.Count; i++)
        {
            //�ҵ��ڼ��ܷ�Χ�ڵ�Ŀ��
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.heroes[i]))
            {
                results.Add(GameApp.FightWorldManager.heroes[i]);
            }

        }

        return results;
    }

}
