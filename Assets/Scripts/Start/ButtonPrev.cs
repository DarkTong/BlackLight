using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrev : MonoBehaviour {

    // 获取脚本
    private CharaterCreation charaterCreation;
    private int charaterTotal;  // 角色总数
    private int index;  // 角色下标

	void Start () {
        charaterCreation = GameObject.Find("CharaterCreation").GetComponent<CharaterCreation>();
	}
    /// <summary>
    /// 选择上一个角色.
    /// </summary>
    void OnClick()
    {
        // 获取角色总数
        charaterTotal = charaterCreation.GetCharaterTotal();

        // 获取当前角色下标
        index = charaterCreation.getIndex();
        // 隐藏角色
        charaterCreation.HideCharater(index);
        // 获取前一个角色
        index = (charaterTotal + index - 1) % charaterTotal;
        // 设置显示的角色下标
        charaterCreation.setIndex(index);
        // 显示角色
        charaterCreation.ShowCharater(index);
    }
}
