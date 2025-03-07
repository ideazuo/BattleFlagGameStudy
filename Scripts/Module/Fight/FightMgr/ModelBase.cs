using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;//物体id
    public Dictionary<string, string> data;//数据表
    public int Step;//行动力
    public int Attack;//攻击力
    public int Type;//类型
    public int MaxHp;//最大血量
    public int CurHp;//当前血量

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;//身体图片渲染组件
    public GameObject stopObj;//停止行动的标记物体
    public Animator ani;//动画组件

    private bool _isStop;//是否移动完标记

    public bool IsStop
    {
        get
        {
            return _isStop;
        }
        set
        {
            stopObj.SetActive(value);

            if(value == true)
            {
                bodySp.color = Color.gray;
            }
            else
            {
                bodySp.color = Color.white;
            }

            _isStop = value;
        }
    }

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        AddEvent();
        
    }

    protected virtual void OnDestroy()
    {
        RemoveEvent();
    }

    //注册事件
    protected virtual void AddEvent()
    {
        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //移除事件
    protected virtual void RemoveEvent()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //选中回调
    protected virtual void OnSelectCallBack(System.Object arg)
    {
        //执行未选中
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);

        GameApp.MapManager.ShowStepGrid(this, Step);
    }

    //未选中回调
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);
    }

    //转向
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
            //转向
            Flip();
        }
        if (transform.position.x < pos.x && transform.localScale.x < 0)
        {
            Flip();
        }

        //如果离目的地很近 返回 true
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

    //播放动画
    public void PlayAni(string aniName)
    {
        ani.Play(aniName);
    }
}
