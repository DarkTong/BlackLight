using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour {

    // 是否已经按下任何键了
    private bool isAnyKeyDown = false;
    // start button container
    private GameObject startButtonContainer;

    void Start()
    {
        // 获取按键组件
        startButtonContainer = gameObject.transform.parent.Find("StartButtonContainer").gameObject;
        //startButtonContainer.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        // 还没按下任何间 且 正按下任何键
        if (isAnyKeyDown == false && Input.anyKey)
        {
            ShowButton();
        }
	}
    /// <summary>
    /// Shows the button container.
    /// </summary>
    void ShowButton()
    {
        startButtonContainer.SetActive(true);
        gameObject.SetActive(false);
    }
}
