using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս���е�״̬ö��
public enum GameState
{
    Idel,
    Enter,
    Player
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
}
