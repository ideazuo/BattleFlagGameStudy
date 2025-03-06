using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//英雄脚本
public class Hero : ModelBase
{
    public void Init(Dictionary<string,string>data,int row,int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    //选中
    protected override void OnSelectCallBack(object arg)
    {
        //玩家回合才能选中角色
        if (GameApp.FightManager.state == GameState.Player)
        {
            //不能操作
            if (IsStop == true)
            {
                return;
            }
            if(GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }

            //添加显示路径指令
            GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

            base.OnSelectCallBack(arg);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
        
    }

    //未选中
    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);
    }
}
