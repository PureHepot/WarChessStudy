using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ģ�ͻ���
public class BaseModel
{
    public BaseController controller;

    public BaseModel(BaseController controller)
    {
        this.controller = controller;
    }

    public BaseModel()
    {

    }

    public virtual void Init()
    {

    }
}
