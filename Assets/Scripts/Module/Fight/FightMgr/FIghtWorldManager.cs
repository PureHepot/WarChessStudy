using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//ս���е�״̬ö��
public enum GameState
{
    Idel,
    Enter,
    Player,
    Enemy,
    GameOver
}


/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���� Ӣ�� ��ͼ ���� �ȣ���
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idel;

    private FightUnitBase current;//��ǰ������ս����Ԫ

    public List<Hero> heroes;//ս��Ӣ�۵ļ���

    public List<Enemy> enemys;

    public int RoundCount;//�غ���

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

    //����ս�� ��ʼ����Ϣ 
    public void EnterFight()
    {
        RoundCount = 1;
        heroes = new List<Hero>();
        enemys = new List<Enemy>();
        //�������еĵ��˽ű����д洢
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //���õ�ǰλ�ñ�ռ��
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
        //���λ�ñ�ռ���� ���÷��������Ϊ�ϰ���
        b.Type = BlockType.Obstacle;
        heroes.Add(hero);
    }

    //�Ƴ�
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

    //����Ӣ���ж�
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
    /// ��ȡ���λ�õ�Ӣ��
    /// </summary>
    /// <param name="model">Ŀ��</param>
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
