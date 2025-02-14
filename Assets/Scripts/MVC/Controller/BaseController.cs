using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;//�¼��ֵ�

    protected BaseModel model;//ģ������

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }
    //ע�����õĳ�ʼ��������Ҫ���п�������ʼ����ִ�У�
    public virtual void Init()
    {

    }

    public virtual void OnLoadView(IBaseView view) { }

    //����ͼ
    public virtual void OpenView(IBaseView view)
    {

    }

    //�ر���ͼ
    public virtual void CloseView(IBaseView view)
    {

    }

    //ע��ģ���¼�
    public void RegisterFunc(string eventName, System.Action<object[]> callback)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] += callback;
        }
        else
        {
            message.Add(eventName, callback);
        }
    }

    //�Ƴ�ģ���¼�
    public void UnregisterFunc(string eventName)
    {
        if (message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }

    //������ģ���¼�
    public void ApplyFunc(string eventName, params object[] args)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);
        }
        else
        {
            Debug.Log("error:" +  eventName);
        }
    }

    //��������ģ���¼�
    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);
    }
    public void ApplyControllerFunc(ControllerType controllerKey, string eventName, params object[] args)
    {
        ApplyControllerFunc((int)controllerKey, eventName, args);
    }

    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    public BaseModel GetModel()
    {
        return model;
    }

    public T GetModel<T>() where T : BaseModel
    {
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        return GameApp.ControllerManager.GetControllerModel(controllerKey);
    }

    //ɾ��������
    public virtual void Destroy()
    {
        RemoveModuelEvent();
        RemoveGlobalEvent();
    }

    //��ʼ��ģ���¼�
    public virtual void InitModuleEvent()
    {

    }

    //�Ƴ�ģ���¼�
    public virtual void RemoveModuelEvent()
    {

    }

    //��ʼ��ȫ��ģ���¼�
    public virtual void InitGlobalEvent()
    {

    }

    //�Ƴ�ȫ���¼�
    public virtual void RemoveGlobalEvent()
    {

    }
}
