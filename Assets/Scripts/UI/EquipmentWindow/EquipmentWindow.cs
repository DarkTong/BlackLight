using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindow : MonoBehaviour {

    private bool isShow;
    private UITweener tweenPosition;

	// Use this for initialization
	void Start () {
        tweenPosition = GetComponent<UITweener>();
        isShow = false;
        this.HideUI();
	}
	
	// Update is called once per frame
	void Update () {
		
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
