using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//选择英雄面板
public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }

    //选完英雄开始进入到玩家回合
    private void onFightBtn()
    {
        //如果一个英雄都没选要提示玩家选择，这里我没有对应的提示界面，自己可以制作扩展
        //上面是原作者的注释，我可以考虑自己增加一个提示界面来验证学习成果
        if(GameApp.FightManager.heros.Count == 0)
        {
            Debug.Log("没有选择英雄");
        }
        else
        {
            GameApp.ViewManager.Close(ViewId);//关闭当前选英雄界面

            //切换到玩家回合
            GameApp.FightManager.ChangeState(GameState.Player);
        }
    }

    public override void Open(params object[] args)
    {
        base.Open(args);

        GameObject prefabObj = Find("bottom/grid/item");

        Transform gridTf = Find("bottom/grid").transform;

        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++)
        {
            Dictionary<string,string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(GameApp.GameDataManager.heros[i]);

            GameObject obj = Instantiate(prefabObj, gridTf);

            obj.SetActive(true);

            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
