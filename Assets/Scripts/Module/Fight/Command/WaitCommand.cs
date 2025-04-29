using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : BaseCommand
{
    private float time;
    System.Action callback;

    public WaitCommand(float t, System.Action callback = null)
    {
        time = t;
        this.callback = callback;
    }

    public override bool Update(float dt)
    {
        time-=dt;
        if (time <= 0)
        {
            callback?.Invoke();
            return true;
        }
        return false;
    }
}
