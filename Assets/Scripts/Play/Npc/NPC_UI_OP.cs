using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_UI_OP : MonoBehaviour {

    public GameObject NPCTagert;
    public TweenPosition NPCTween;
    protected string targetContext;
    protected GameManage gameManage;

    void Awake()
    {
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
    }

    /// <summary>
    /// 鼠标在NPC上.
    /// </summary>
    void OnMouseOver()
    {
        // 单击NPC
        if (Input.GetMouseButtonDown(0))
        {
            // 显示对应NPC的UI
            ShowUI();
        }
    }

    /// <summary>
    /// 鼠标进入NPC,设置为Normal.
    /// </summary>
    void OnMouseEnter()
    {
        gameManage.setCursorNPCTalk();
    }

    /// <summary>
    /// 鼠标移出NPC.
    /// </summary>
    void OnMouseExit()
    {
        gameManage.setCursorNormal();
    }

    /// <summary>
    /// Show the npc window.
    /// </summary>
    void ShowUI()
    {
        NPCTagert.SetActive(true);
        NPCTween.PlayForward();
    }

    /// <summary>
    /// Hide the npc window.
    /// </summary>
    void HideUI()
    {
        //NPCTagert.SetActive(false);
        NPCTween.PlayReverse();
    }

    /// <summary>
    /// 更新任务显示面板.
    /// </summary>
    protected virtual void UpdateTargetContext() { }

    /// <summary>
    /// 任务对话框上的退出事件.
    /// </summary>
    public void OnCloseButtonClick()
    {
        HideUI();
    }
}
