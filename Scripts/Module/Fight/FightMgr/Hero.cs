using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//英雄脚本
public class Hero : ModelBase,ISkill
{   
    public SkillProperty skillPro { get; set; }
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
        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    //选中
    protected override void OnSelectCallBack(object arg)
    {
        //玩家回合才能选中角色
        if (GameApp.FightManager.state == GameState.Player)
        {
            //不能操作
            if(GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }
            //执行未选中
            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);

            if(IsStop == false)
            {
                //显示路径
                GameApp.MapManager.ShowStepGrid(this, Step);
                //添加显示路径指令
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                //添加选项事件
                addOptionEvent();
            }
            
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
        
    }

    private void addOptionEvent()
    {
        GameApp.MsgCenter.AddTempEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnCancelEvent, onCancelCallBack);
    }

    //攻击
    private void onAttackCallBack(System.Object arg)
    {
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
    }

    //待机
    private void onIdleCallBack(System.Object arg)
    {
        IsStop = true;
    }

    //取消移动
    private void onCancelCallBack(System.Object arg)
    {
        GameApp.CommandManager.Undo();
    }

    //未选中
    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);
    }

    //显示技能区域
    public void ShowSkillArea()
    {
        GameApp.MapManager.ShowAttackStep(this, skillPro.AttackRange, Color.red);
    }

    //隐藏技能区域
    public void HideSkillArea()
    {
        GameApp.MapManager.HideAttackStep(this, skillPro.AttackRange);
    }
}
