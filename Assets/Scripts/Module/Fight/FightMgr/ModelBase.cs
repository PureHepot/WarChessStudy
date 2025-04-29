using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;//模型Id
    public Dictionary<string, string> data;//数据表
    public int Step;//行动力
    public int Attack;//攻击力
    public int Type;//类型
    public int MaxHp;//最大血量
    public int CurHp;//当前血量

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;//身体图片渲染组件
    public GameObject stopObj;//停止动作的标记物体
    public Animator animator;//动画组件

    private bool _isStop;//是否移动完标记

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

    //选中回调
    protected virtual void OnSelectCallBack(object arg)
    {
        //执行未选中
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

        GameApp.MapManager.ShowStepGrid(this, Step);
    }
    //未选中的回调
    protected virtual void OnUnSelectCallBack(object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);
    }

    //角色反转
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //移动到指定下标的格子
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

    //播放特效
    public virtual void PlayEffect(string name)
    {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject;
        obj.transform.position = transform.position;
    }

    //计算两个model的距离（根据行列下标计算）
    public float GetDis(ModelBase model)
    {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    //播放音效
    public void PlaySound(string name)
    {
        GameApp.SoundManager.PlayEffect(name, transform.position);
    }

    //看向某个模型
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
