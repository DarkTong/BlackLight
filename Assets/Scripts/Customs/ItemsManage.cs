using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 道具类型
public enum ItemType
{
    DRUG,
    EQUIP,
    MATERIAL,
    EMPTY
}
// 道具类型 - 装备类型
public enum EquipmentItemType
{
    ARMOR,      // 盔甲
    HEADGEAR,   // 头盔
    RIGHTHAND,  // 右护手
    LEFTHAND,   // 左护手
    SHOE,       // 鞋子
    ACCESSORY   // 首饰
}
// 道具类型 - 装备类型 - 适用类型
public enum JobType
{
    SWORDMAN,   // 剑士
    MAGICIAN,   // 法师
    COMMON      // 通用
}


public class ItemsManage : MonoBehaviour {

    static public ItemsManage _instance;
    public TextAsset dragItemText;  
    public TextAsset equipItemText;

    // 物品字典
    private Dictionary<int, ItemInfo> dictItems = new Dictionary<int, ItemInfo>();

    // 获取自身对象，用于外部调用
    void Awake()
    {
        _instance = this;
        // 添加一个空元素
        dictItems.Add(0, ItemInfo.itemNull);
        // 读取物品信息
        ReadInfo(dragItemText);
        ReadInfo(equipItemText);
        Debug.Log(dictItems.Count);
    }

    /// <summary>
    /// 读取物品信息并且加载到物品字典中.
    /// </summary>
    void ReadInfo(TextAsset textAsset)
    {
        // 获取文本内容
        string text = textAsset.text;
        // 将内容分行
        string[] strArray = text.Split('\n');
        // 由于第一行是描述
        for (int i = 1; i < strArray.Length; ++i)
        {
            string[] porpertys = strArray[i].Split(',');
            // 信息不完整
            if (porpertys.Length < 9)
                continue;

            ItemInfo item = null;
            // 3.类型
            switch (porpertys[3])
            {
                case "DRUG":
                    DragItemInfo itemD = new DragItemInfo();
                    // 类型
                    itemD.type = ItemType.DRUG;
                    // 4.加血量值 
                    itemD.addHP = int.Parse(porpertys[4]);
                    // 5.加魔法   
                    itemD.addMP = int.Parse(porpertys[5]);
                    item = itemD as ItemInfo;
                    break;
                case "EQUIP":
                    EquipmentItemInfo itemE = new EquipmentItemInfo();
                    itemE.type = ItemType.EQUIP;
                    itemE.attackPlus = int.Parse(porpertys[7]);
                    itemE.defensePlus = int.Parse(porpertys[8]);
                    itemE.speedPlus = int.Parse(porpertys[9]);
                    itemE.equipType = (EquipmentItemType)System.Enum.Parse(typeof(EquipmentItemType), porpertys[10]);
                    itemE.jobType = (JobType)System.Enum.Parse(typeof(JobType), porpertys[11]);
                    item = itemE as ItemInfo;
                    break;
                case "MATERIAL":
                    //type = ItemType.MATERIAL;
                    break;
                default:
                    item = new ItemInfo();
                    break;
            }

            // 0.id
            item.id = int.Parse(porpertys[0]);
            // 1.名称
            item.itemName = porpertys[1];
            // 2.图标名字
            item.iconName = porpertys[2];
            // 4.出售价 
            item.sellPrice = int.Parse(porpertys[4]);
            // 5.购买
            item.sellPrice = int.Parse(porpertys[5]);
            // 6.描述
            item.desciption = porpertys[6];

            //Debug.Log(item.desciption);

            dictItems.Add(item.id, item);
        }
    }

    // 根据Id获取物品信息
    public ItemInfo getItemById(int id)
    {
        return dictItems[id];
    }
    /// <summary>
    /// 获取所有药品的ID.
    /// </summary>
    /// <returns>具有所有物品Id的链表</returns>
    public void getAddDragItemId(out List<int> dragItems)
    {
        // 遍历所有物品
        dragItems = new List<int>();
        foreach (int item in dictItems.Keys)
        {
            if (dictItems[item].type == ItemType.DRUG)
            {
                dragItems.Add(item);
            }
        }
    }
}

/* 
 * 0.物品id 
 * 1.名称  
 * 2.类型 
 * 3.图标名字    
 * 4.加血量值 
 * 5.加魔法值    
 * 6.出售价 
 * 7.购买价 
 * 8.描述
 */
// 物品信息
public class ItemInfo : Object
{
    public int id;
    public string itemName;
    public ItemType type = new ItemType();
    public string iconName;
    public int sellPrice;
    public int buyPrice;
    public string desciption;

    // 静态类对象
    public static readonly ItemInfo itemNull = new ItemInfo();

    // 重载构造函数
    public ItemInfo(int id = 0, string name = "icon-null")
    {
        this.id = id;
        this.iconName = name;
        this.type = ItemType.EMPTY;
    }
    public ItemInfo(ItemInfo item)
    {
        this.id = item.id;
        this.itemName = item.itemName;
        this.type = item.type;
        this.iconName = item.iconName;
        this.sellPrice = item.sellPrice;
        this.buyPrice = item.buyPrice;
        this.desciption = item.desciption;
    }
}
// 药品信息
public class DragItemInfo : ItemInfo
{
    public int addHP;
    public int addMP;
    public DragItemInfo(int id = 0, string name = "icon-null")
        :base(id, name){}
}
// 装备信息
public class EquipmentItemInfo : ItemInfo
{
    public int attackPlus;
    public int defensePlus;
    public int speedPlus;
    public EquipmentItemType equipType = new EquipmentItemType();
    public JobType jobType = new JobType();

    public EquipmentItemInfo() { }
    public EquipmentItemInfo(ItemInfo item) : base(item) { }

}
