using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescript : MonoBehaviour {

    // 判断是够显示文本
    private bool isShow;
    private UILabel descLable;
    private UISprite bg;


	// Use this for initialization
	void Start () {
        descLable = GetComponent<UILabel>();
        bg = transform.parent.GetComponent<UISprite>();
        HideInfo(0);
        //gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isShow)
        {
            Vector3 pos = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(Input.mousePosition);
            bg.transform.position = pos;
        }
	}

    /// <summary>
    /// 构建药品显示信息.
    /// </summary>
    /// <returns>返回构造好的物品返回信息</returns>
    /// <param name="item">物品组件</param>
    string BuildDragDescInfo(DragItemInfo item)
    {
        string desc = "";

        desc += "名称：" + item.itemName + "\n";
        desc += "+HP：" + item.addHP + "\n";
        desc += "+MP：" + item.addMP + "\n";
        desc += "出售价：" + item.sellPrice + "\n";
        desc += "描述：\n\t" + item.desciption + "\n";

        return desc;
    }
    /// <summary>
    /// 构装备显示信息.
    /// </summary>
    /// <returns>The drag desc info.</returns>
    /// <param name="item">Item.</param>
    string BuildEquipDescInfo(EquipmentItemInfo item)
    {
        string desc = "";

        desc += "名称：" + item.itemName + "\n";
        desc += "装备类型：";
        switch (item.equipType)
        {
            case EquipmentItemType.HEADGEAR :
                desc += "头盔\n"; break;
            case EquipmentItemType.RIGHTHAND :
                desc += "右手装备\n"; break;
            case EquipmentItemType.LEFTHAND :
                desc += "左手装备\n"; break;
            case EquipmentItemType.ARMOR :
                desc += "盔甲\n"; break;
            case EquipmentItemType.ACCESSORY :
                desc += "首饰\n"; break;
            case EquipmentItemType.SHOE :
                desc += "鞋子\n"; break;
        }
        desc += "适用类型：";
        switch (item.jobType)
        {
            case JobType.MAGICIAN:
                desc += "魔法师\n"; break;
            case JobType.SWORDMAN:
                desc += "剑士\n"; break;
            case JobType.COMMON:
                desc += "通用\n"; break;
                
        }
        desc += "+伤害值：" + item.attackPlus + "\n";
        desc += "+防御值：" + item.defensePlus + "\n";
        desc += "+速度值：" + item.speedPlus + "\n";
        desc += "出售价：" + item.sellPrice + "\n";
        desc += "描述：\n\t" + item.desciption + "\n";

        return desc;
    }

    /// <summary>
    /// UI显示信息.
    /// </summary>
    /// <param name="item">信息.</param>
    public void ShowInfo(ItemInfo item, int depth)
    {
        string info="";

        // 选择构建的信息
        switch (item.type)
        {
            case ItemType.DRUG:
                info = BuildDragDescInfo(item as DragItemInfo);
                break;
            case ItemType.EQUIP:
                info = BuildEquipDescInfo(item as EquipmentItemInfo);
                break;
        }
        // 显示文本和背景
        descLable.enabled = true;
        bg.enabled = true;
        descLable.text = info;
        // 增加深度，防止被覆盖
        descLable.depth += depth;
        bg.depth += depth;
        // 已经显示
        isShow = true;
    }
    public void HideInfo(int depth)
    {
        // 恢复深度
        descLable.depth -= depth;
        bg.depth -= depth; 
        //隐藏文本和背景
        descLable.enabled = false;
        bg.enabled = false;
        // 已经隐藏
        isShow = false;
    }
}
