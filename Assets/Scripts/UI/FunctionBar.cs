using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour {

    // 背包窗口
    public InventoryWindow inventoryWin;
    public PlayerStatusWindow playerStatusWin;
    public EquipmentWindow equipmentWin;

    private ItemsManage itemManage;

    // 是否已经点击了背包窗口
    private bool isClickBagButton = false;

    void Start()
    {
        // 获取背包字典
        itemManage = GameObject.Find("GameManage").GetComponent<ItemsManage>();
    }

    /// <summary>
    /// Status按钮响应.
    /// </summary>
    public void OnStatusButtonClick()
    {
        playerStatusWin.TranslateUIState();
    }

    /// <summary>
    /// Equip按钮响应.
    /// </summary>
    public void OnEquipButtonClick()
    {
        equipmentWin.TranslateUIState();
    }

    /// <summary>
    /// Skill按钮响应.
    /// </summary>
    public void OnSkillButtonClick()
    {
        
    }

    /// <summary>
    /// Bag按钮响应.
    /// </summary>
    public void OnBagButtonClick()
    {
        // 若没按过，则显示窗口，若按过隐藏窗口
        inventoryWin.TranslateUIState();
    }

    /// <summary>
    /// Setting按钮响应.
    /// </summary>
    public void OnSettingButtonClick()
    {

    }
}
