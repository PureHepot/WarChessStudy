using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilManager
{
    private GameTimer timer;
    private Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)> skills;//技能队列

    public SkilManager()
    {
        timer = new GameTimer();
        skills = new Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)>();
    }

    public void AddSkill(ISkill skill, List<ModelBase> targets = null, System.Action callback = null)
    {
        skills.Enqueue((skill, targets, callback));
    }

    public void UseSkill(ISkill skill, List<ModelBase> targets, System.Action callback)
    {
        ModelBase current = (ModelBase)skill;

        if(targets.Count > 0)
        {
            current.LookAtModel(targets[0]);
        }

        current.PlaySound(skill.skills.Sound);
        current.PlayAni(skill.skills.AniName);
        timer.Register(skill.skills.AttackTime, () =>
        {
            int atkCount = Mathf.Min(skill.skills.AttackCount, targets.Count);

            for (int i = 0;i<atkCount; i++)
            {
                targets[i].GetHit(skill);
            }
        });

        timer.Register(skill.skills.Time, () =>
        {
            current.PlayAni("idel");
            callback?.Invoke();
        });
    }

    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if (timer.Count() == 0 && skills.Count > 0)
        {
            var next = skills.Dequeue();

            if (next.targets != null)
            {
                UseSkill(next.skill, next.targets, next.callback);
            }
        }
    }


    public bool IsRunningSkill()
    {
        if (timer.Count() == 0 && skills.Count == 0)
        {
            return false;
        }
        else
        {
            return true; 
        }
    }

    public void Clear()
    {
        timer.Break();
        skills.Clear();
    }
}
