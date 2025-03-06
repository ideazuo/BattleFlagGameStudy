﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Null,//空
    Obstacle//障碍物
}

//地图的单元格子
public class Block : MonoBehaviour
{
    public int RowIndex;//行下标
    public int ColIndex;//列下标
    public BlockType Type;//格子类型
    private SpriteRenderer selectSp;//选中的格子图片
    private SpriteRenderer gridSp;//网格图片
    private SpriteRenderer dirSp;//移动方向图片

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    //显示格子
    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    //隐藏格子
    public void HideGrid()
    {
        gridSp.enabled = false;
    }

    void OnSelectCallBack(System.Object arg)
    {
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //设置箭头方向的图片资源和颜色
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }
}
