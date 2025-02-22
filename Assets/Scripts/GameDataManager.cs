using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器（存储玩家基本的游戏信息）
/// </summary>
public class GameDataManager
{
    public List<int> heros;//英雄集合

    public int Money;//货币

    public GameDataManager()
    {
        heros = new List<int>();

        heros.Add(10001);
        heros.Add(10002);
        heros.Add(10003);
    }
}
