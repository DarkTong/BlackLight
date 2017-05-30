using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNPC : NPC_UI_OP {

    public int taskRewardCoins = 1000;
    private PlayerInfo player;

    private UILabel taskContestUI;  //文本UI
    private GameObject Accept, Apply, Cancel; //三个按键

    private bool isAccept = false;  //任务是否被接受
    private int isKill = 100; //狼被杀死的个数
    private const int finishKill = 10;    //任务完成数

    void Start()
    {
        taskContestUI = NPCTagert.transform.Find("TaskContest").GetComponent<UILabel>();
        Accept = NPCTagert.transform.Find("Accept").gameObject;
        Apply = NPCTagert.transform.Find("Apply").gameObject;
        Cancel = NPCTagert.transform.Find("Cancel").gameObject;
        // 获取玩家信息脚本
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();
        // 获取GameManage脚本
        //gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
    }

    void Update()
    {
        // 若完成则显示提交按钮
        if (isAccept && isKill >= finishKill)
        {
            Apply.SetActive(true);
        }
        else
        {
            Apply.SetActive(false);
        }
    }

    /// <summary>
    /// Ons the accept button click.
    /// </summary>
    public void OnAcceptButtonClick()
    {
        isAccept = true;
        UpdateTargetContext();
        Accept.SetActive(false);
        Apply.SetActive(false);
        Cancel.SetActive(false);
    }
    /// <summary>
    /// Ons the cancel button click.
    /// </summary>
    public void OnCancelButtonClick()
    {
        OnCloseButtonClick();
    }
    /// <summary>
    /// Ons the apply button click.
    /// </summary>
    public void OnApplyButtonClick()
    {
        if (isKill > finishKill)
        {
            // 获得奖励
            player.addPlayerCoin(taskRewardCoins);
            Debug.Log(player.getPlayerCoins());
            isKill = 0;
            isAccept = false;
            //          UpdateTargetContext();
            taskContestUI.text = "";
        }
        // 关闭窗口
        OnCloseButtonClick();
    }

    /// <summary>
    /// 更新任务显示信息.
    /// </summary>
    protected override void UpdateTargetContext() 
    {
        if (isAccept)
        {
            // 更新最新的任务信息
            targetContext = "【任务进度】\n" +
                "已经杀死" + isKill + "只小狼\n" +
                "【任务目标】\n" +
                "至少杀死10只小狼\n";
            // 更新显示面板
            taskContestUI.text = targetContext;
        }
        else
        {
            // 设置任务信息
            targetContext = "【任务描述】\n" +
                "由于这段时间狼群一直滋扰我们村子，现在想" +
                "找一个勇者帮忙抑制狼群的生态\n" +
                "【任务内容】\n" +
                "杀死10只小狼\n" +
                "【任务奖励】\n" +
                "1000 GOLD\n";
            // 设置UILable显示信息
            taskContestUI.text = targetContext;
        }
    }

}
