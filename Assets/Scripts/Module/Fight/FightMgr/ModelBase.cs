using System.Collections;
using System.Collections.Generic;
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
}
