using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//选择关卡信息界面
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);
    }

    //返回开始场景
    private void onCloseBtn()
    {
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = delegate ()
        {
            //打开选择关卡界面
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    //显示关卡详情面板
    public void ShowLevelDes()
    {
        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    //隐藏关卡详情面板
    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }

    //切换到战斗场景
    public void onFightBtn()
    {
        //关闭当前界面
        GameApp.ViewManager.Close(ViewId);
        //摄像机重置位置
        GameApp.CameraManager.ResetPos();

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName;//跳转战斗场景
        loadingModel.callback = delegate ()
        {
            //加载成功后显示战斗界面等
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
            
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
}
