using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusWindow : MonoBehaviour {

    // 角色信息
    public PlayerInfo playerInfo;

    private bool isShow = false;
    private UITweener tweenPosition;
    private UILabel attackUI, defenseUI, speedUI, remainPointUI, summaryInfo;
    private UIButton attackBtn, defenseBtn, speedBtn;

	// Use this for initialization
	void Awake () {
        tweenPosition = GetComponent<UITweener>();
        attackUI = transform.FindChild("AttackText").FindChild("Value").GetComponent<UILabel>();
        defenseUI = transform.FindChild("DefenseText").FindChild("Value").GetComponent<UILabel>();
        speedUI = transform.FindChild("SpeedText").FindChild("Value").GetComponent<UILabel>();
        remainPointUI = transform.FindChild("RemainPoint").GetComponent<UILabel>();
        summaryInfo = transform.FindChild("SummaryInfo").GetComponent<UILabel>();
        attackBtn = transform.FindChild("AttackText").FindChild("AddBtn").GetComponent<UIButton>();
        defenseBtn = transform.FindChild("DefenseText").FindChild("AddBtn").GetComponent<UIButton>();
        speedBtn = transform.FindChild("SpeedText").FindChild("AddBtn").GetComponent<UIButton>();

        // 显示信息
        UpdateSummaryText();

        HideUI();
	}

    // 更新攻击力显示
    public void UpdateAttackText()
    {
        attackUI.text = playerInfo.GetAttack().ToString();
        UpdateRemainPointText();
    }
    // 更新防御力显示
    public void UpdateDefenseText()
    {
        defenseUI.text = playerInfo.GetDefense().ToString();
        UpdateRemainPointText();
    }
    // 更新速度显示
    public void UpdateSpeedText()
    {
        speedUI.text = playerInfo.GetSpeed().ToString();
        UpdateRemainPointText();
    }
    // 更新剩余点数
    public void UpdateRemainPointText()
    {
        int remainP = playerInfo.GetRemainPoint();
        remainPointUI.text = "剩余点数：" + remainP;
        if (remainP <= 0)
            hideAllAddButton();
        else
            showAllAddButton();
    }
    // 更新总结信息
    public void UpdateSummaryText()
    {
        this.UpdateAttackText();
        this.UpdateDefenseText();
        this.UpdateSpeedText();
        this.UpdateRemainPointText();

        string str = "";
        str += "伤害：" + playerInfo.GetAttack() + " ";
        str += "防御：" + playerInfo.GetDefense() + " ";
        str += "速度：" + playerInfo.GetSpeed();
        summaryInfo.text = str;
    }

    // 显示所有+按钮
    public void showAllAddButton()
    {
        attackBtn.gameObject.SetActive(true);
        defenseBtn.gameObject.SetActive(true);
        speedBtn.gameObject.SetActive(true);
    }
    // 隐藏所有+按钮
    public void hideAllAddButton()
    {
        Debug.Log("tmp");
        attackBtn.gameObject.SetActive(false);
        defenseBtn.gameObject.SetActive(false);
        speedBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示UI.
    /// </summary>
    void ShowUI()
    {
    	isShow = true;
    	this.gameObject.SetActive(true);
    	tweenPosition.PlayForward();
    }

    /// <summary>
    /// 隐藏UI.
    /// </summary>
    void HideUI()
    {
    	isShow = false;
    	tweenPosition.PlayReverse();
    }

    /// <summary>
    /// 外部接口，每次改变UI显示状态.
    /// </summary>
    public void TranslateUIState()
    {
    	if (isShow)
    		HideUI();
    	else
    		ShowUI();
    }
}
