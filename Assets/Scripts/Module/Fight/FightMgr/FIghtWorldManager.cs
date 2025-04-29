using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//战斗中的状态枚举
public enum GameState
{
    Idel,
    Enter,
    Player,
    Enemy,
    GameOver
}


/// <summary>
/// 战斗管理器（用于管理战斗相关的实体（敌人 英雄 地图 格子 等））
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idel;

    private FightUnitBase current;//当前所处的战斗单元

    public List<Hero> heroes;//战斗英雄的集合

    public List<Enemy> enemys;

    public int RoundCount;//回合数

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    public FightWorldManager()
    {
        heroes = new List<Hero>();
        enemys = new List<Enemy>();
        ChangeState(GameState.Idel);
    }

    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == true)
        {
            //todo
        }
        else
        {
            current = null;
        }
    }

    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
        switch (this.state)
        {
            case GameState.Idel:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;
            case GameState.Player:
                _current = new FightPlayerUnit();
                break;
            case GameState.Enemy:
                _current = new FIghtEnemyUnit();
                break;
            case GameState.GameOver:
                _current = new FightGameOverUnit();
                break;
        }
        _current.Init();
    }

    //进入战斗 初始化信息 
    public void EnterFight()
    {
        RoundCount = 1;
        heroes = new List<Hero>();
        enemys = new List<Enemy>();
        //将场景中的敌人脚本进行存储
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //设置当前位置被占用
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemys.Add(enemy);
        }
    }

    public void AddHero(Block b, Dictionary<string, string> data)
    {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        //这个位置被占领了 设置方块的类型为障碍物
        b.Type = BlockType.Obstacle;
        heroes.Add(hero);
    }

    //移除
    public void RemoveEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);

        GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Null);

        if (enemys.Count == 0)
        {
            ChangeState(GameState.GameOver);
        }
    }

    public void RemoveHero(Hero hero)
    {
        heroes.Remove(hero);

        GameApp.MapManager.ChangeBlockType(hero.RowIndex, hero.ColIndex, BlockType.Null);

        if (heroes.Count == 0)
        {
            ChangeState(GameState.GameOver);
        }
    }

    //重置英雄行动
    public void ResetHeroes()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            heroes[i].isStop = false;
        }
    }

    public void ResetEnemys()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].isStop = false;

        }
    }

    /// <summary>
    /// 获取最近位置的英雄
    /// </summary>
    /// <param name="model">目标</param>
    /// <returns></returns>
    public ModelBase GetMinDisHero(ModelBase model)
    {
        if (heroes.Count == 0)
        {
            return null;
        }
        Hero hero = heroes[0];
        float min_dis = hero.GetDis(model);
        for(int i = 1;i<heroes.Count;i++)
        {
            float dis = heroes[i].GetDis(model);
            if (dis < min_dis)
            {
                min_dis = dis;
                hero = heroes[i];
            }
        }
        return hero;
    }

    public void ReLoadRes()
    {
        heroes.Clear();
        enemys.Clear();
        GameApp.MapManager.Clear();
    }
}
