using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;//ģ��Id
    public Dictionary<string, string> data;//���ݱ�
    public int Step;//�ж���
    public int Attack;//������
    public int Type;//����
    public int MaxHp;//���Ѫ��
    public int CurHp;//��ǰѪ��

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;//����ͼƬ��Ⱦ���
    public GameObject stopObj;//ֹͣ�����ı������
    public Animator animator;//�������

    private bool _isStop;//�Ƿ��ƶ�����

    public bool isStop
    {
        get
        {
            return _isStop;
        }
        set
        {
            stopObj.SetActive(value);

            if (value == true)
            {
                bodySp.color = Color.gray;
            }
            else
            {
                bodySp.color= Color.white;
            }
            _isStop = value;
        }
    }

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        animator = transform.Find("body").GetComponent<Animator>();
    }
    private void Start()
    {
        OnStart();
    }
    protected virtual void OnStart()
    {
        AddEvents();
    }
    protected virtual void OnDestroy()
    {
        RemoveEvents();
    }

    protected virtual void AddEvents()
    {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }
    protected virtual void RemoveEvents()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //ѡ�лص�
    protected virtual void OnSelectCallBack(object arg)
    {
        //ִ��δѡ��
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

        GameApp.MapManager.ShowStepGrid(this, Step);
    }
    //δѡ�еĻص�
    protected virtual void OnUnSelectCallBack(object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);
    }

    //��ɫ��ת
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //�ƶ���ָ���±�ĸ���
    public virtual bool Move(int rowIndex, int colIndex, float dt)
    {
        Vector3 pos = GameApp.MapManager.GetBlockPos(rowIndex, colIndex);

        pos.z = transform.position.z;

        if (transform.position.x > pos.x && transform.localScale.x > 0)
        {
            Flip();
        }
        if (transform.position.x < pos.x && transform.localScale.x < 0)
        {
            Flip();
        }
        
        if (Vector3.Distance(transform.position, pos) <= 0.02f)
        {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);

        return false;
    }

    public void PlayAni(string aniName)
    {
        animator.Play(aniName);
    }

    public virtual void GetHit(ISkill skill)
    {

    }

    //������Ч
    public virtual void PlayEffect(string name)
    {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject;
        obj.transform.position = transform.position;
    }

    //��������model�ľ��루���������±���㣩
    public float GetDis(ModelBase model)
    {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    //������Ч
    public void PlaySound(string name)
    {
        GameApp.SoundManager.PlayEffect(name, transform.position);
    }

    //����ĳ��ģ��
    public void LookAtModel(ModelBase model)
    {
        if ((model.transform.position.x > transform.position.x)&&(transform.localScale.x < 0))
        {
            Flip();
        }
        else if ((model.transform.position.x < transform.position.x) && transform.localScale.x > 0)
        {
            Flip();
        }
    }
}
