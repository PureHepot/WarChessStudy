using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ó¢ÐÛ½Å±¾
/// </summary>
public class Hero : ModelBase
{
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
    }
}
