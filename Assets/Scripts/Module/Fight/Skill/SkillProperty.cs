using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能属性
public class SkillProperty
{
    public int Id;
    public string Name;
    public int Attack;
    public int AttackCount;
    public int AttackRange;
    public int Target;
    public int TargetType;
    public string Sound;
    public string AniName;
    public float Time;//技能持续时间
    public float AttackTime;//
    public string AttackEffect;
    public SkillProperty(int id)
    {
        Dictionary<string,string> data = GameApp.ConfigManager.GetConfigData("skill").GetDataById(id);
        Id = int.Parse(data["Id"]);
        Name = data["Name"];
        Attack = int.Parse(data["Atk"]);
        AttackCount = int.Parse(data["AtkCount"]);
        Target = int.Parse(data["Target"]);
        TargetType = int.Parse(data["TargetType"]);
        Sound = data["Sound"];
        AniName = data["AniName"];
        Time = float.Parse(data["Time"]) * 0.001f;
        AttackTime = float.Parse(data["AttackTime"]) * 0.001f;
        AttackEffect = data["AttackEffect"];
        AttackRange = int.Parse(data["Range"]);
    }
}
