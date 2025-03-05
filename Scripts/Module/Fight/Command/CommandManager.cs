﻿using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// 命令管理器
/// </summary>
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue;//将要执行的命令队列
    private Stack<BaseCommand> unDoStack;//撤销的命令栈
    private BaseCommand current;//当前所执行的命令

    public CommandManager()
    {
        willDoCommandQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    //是否在执行命令中
    public bool IsRunningCommand
    {
        get
        {
            return current != null;
        }
    }

    //添加命令
    public void AddCommand(BaseCommand cmd)
    {
        willDoCommandQueue.Enqueue(cmd);//添加到命令队列
        unDoStack.Push(cmd);//添加到撤销栈
    }

    //每帧执行
    public void Update(float dt)
    {
        if(current == null)
        {
            if(willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do();//执行
            }
        }
        else
        {
            if(current.Update(dt) == true)
            {
                current = null;
            }
        }
    }

    public void Clear()
    {
        willDoCommandQueue.Clear();
        unDoStack.Clear();
        current = null;
    }

    //撤销上一个命令
    public void Undo()
    {
        if(unDoStack.Count > 0)
        {
            unDoStack.Pop().UnDo();
        }
    }
}
