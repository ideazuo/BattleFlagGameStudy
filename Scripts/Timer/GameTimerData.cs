using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerData
{
    private float timer;//计时时长
    private System.Action callback;//回调

    public GameTimerData(float timer,System.Action callback)
    {
        this.timer = timer;
        this.callback = callback;
    }

    public bool OnUpdate(float dt)
    {
        timer -= dt;
        if(timer < 0)
        {
            this.callback.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
