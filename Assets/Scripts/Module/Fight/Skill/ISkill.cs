using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    SkillProperty skills { get; set; }

    void ShowSkillArea();

    void HideSkillArea();
}
