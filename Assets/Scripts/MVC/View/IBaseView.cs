using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseView
{
    bool IsInit();//��ʼ����

    bool IsShow();//�Ƿ���ʾ

    void InitUI();//��ʼ�����

    void InitData();//��ʼ������

    void Open(params object[] args);//�����

    void Close(params object[] args);//�ر����

    void DestroyView();//ɾ�����

    void ApplyFunc(string eventName, params object[] args);//������ģ���¼�

    void ApplyControllerFunc(int controllerKey, string eventName, params object[] args);//��������������ģ���¼�

    void SetVisible(bool value);//������ʾ����

    int ViewId { get; set; }//���id

    BaseController Controller { get; set; }//�������������



}
