using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class FightOptionDesView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("bg/turnBtn").onClick.AddListener(onChangeEnemyTurnBtn);
        Find<Button>("bg/gameOverBtn").onClick.AddListener(onGameOverBtn);
        Find<Button>("bg/cancelBtn").onClick.AddListener(onCancelBtn);
    }

    //结束本局游戏
    private void onGameOverBtn()
    {
    }

    //回合结束 切换到敌人回合
    private void onChangeEnemyTurnBtn()
    {
    }

    //取消
    private void onCancelBtn()
    {
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);
    }
}
