using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ººƒ‹÷∏¡Ó
/// </summary>
public class SkillCommand : BaseCommand
{
    ISkill skill;
    public SkillCommand(ModelBase model) : base(model)
    {
        skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        List<ModelBase> results = skill.GetTarget();
        if(results.Count > 0 )
        {
            GameApp.SkillManager.AddSkill(skill, results);
        }
    }
    public override bool Update(float dt)
    {
        if (GameApp.SkillManager.IsRunningSkill() == false)
        {
            model.isStop = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
