﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//拖拽出来的图标界面
public class DragHeroView : BaseView
{
    //拖拽中跟随鼠标移动，显示的时候才进行移动
    void Update()
    {
        if (_canvas.enabled == false)
        {
            return;
        }

        //鼠标坐标转换成世界坐标
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
    }

    public override void Open(params object[] args)
    {
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
