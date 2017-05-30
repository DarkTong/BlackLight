using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 角色属性类
class PlayerAttribute
{
    public int attack, defense, speed;
    public int attackPlus, defensePlus, speedPlus;
    public int remainPoint;
    public int level;
    public int exp;
    public JobType job;
    public static int levelUpPoint = 5;

    public PlayerAttribute(JobType job)
    {
        // 剑士
        if (job == JobType.SWORDMAN)
        {
            attack = 10;
            defense = 5;
            speed = 4;
        }
        // 法师
        else if (job == JobType.MAGICIAN)
        {
            attack = 3;
            defense = 3;
            speed = 3;
        }
        remainPoint = 10;
        level = 1;
        this.job = job;
    }

}
class PlayerEquipment
{
    public int headgear=0;
    public int rightHand=0;
    public int leftHand=0;
    public int shoe=0;
    public int accessory=0;
    public int armor=0;

    public int attackEquip = 0;
    public int defenseEquip = 0;
    public int speedEquip = 0;

    // 更新装备的总加成信息
    public void UpdateInfo()
    {
        this.Reset();
        if(headgear > 0)
            this.Add(ItemsManage._instance.getItemById(headgear) as EquipmentItemInfo);
        if (rightHand > 0)
            this.Add(ItemsManage._instance.getItemById(rightHand) as EquipmentItemInfo);
        if(leftHand > 0)
            this.Add(ItemsManage._instance.getItemById(leftHand) as EquipmentItemInfo);
        if(shoe > 0)
            this.Add(ItemsManage._instance.getItemById(shoe) as EquipmentItemInfo);
        if(accessory > 0)
            this.Add(ItemsManage._instance.getItemById(accessory) as EquipmentItemInfo);
        if(armor > 0)
            this.Add(ItemsManage._instance.getItemById(armor) as EquipmentItemInfo);
    }
    void Reset()
    {
        attackEquip = defenseEquip = speedEquip = 0;
    }

    void Add(EquipmentItemInfo item)
    {
        attackEquip += item.attackPlus;
        defenseEquip += item.defensePlus;
        speedEquip += item.speedPlus;
    }
}


public class PlayerInfo : MonoBehaviour {

    // 组件
    public PlayerStatusWindow playerStatusWin;
    public InventoryWindow inventoryWindow;
    public EquipmentWindow equipmentWin;

    public string playerName;   // 角色名字
    public JobType playerJob = JobType.MAGICIAN;   // 所选角色下标
    public int maxTaskNum;  // 最多同时申请的任务数



    private int playerCoins = 1000;

    private GameObject[] slovingTask;   // 进行中的任务
    private int numSlovingTask; // 进行中的任务数s
    private HashSet<int> finishTask;    //已经完成的任务


    // 角色属性
    private PlayerAttribute playerAttribute;
    // 玩家装备
    private PlayerEquipment playerEquipment;

    void Awake()
    {
        playerAttribute = new PlayerAttribute(playerJob);
        playerEquipment = new PlayerEquipment();
        playerStatusWin = GameObject.Find("PlayerStatusWindow").GetComponent<PlayerStatusWindow>();
        equipmentWin = GameObject.Find("EquipmentWindow").GetComponent<EquipmentWindow>();
    }

    // 增加物品
    public bool AddItemById(int id)
    {
        return inventoryWindow.AddItemById(id);
    }

    /// <summary>
    /// Gets the player coins.
    /// </summary>
    /// <returns>The player coins.</returns>
    public int getPlayerCoins()
    {
        return playerCoins;
    }
    /// <summary>
    /// Adds the player coin.
    /// </summary>
    /// <param name="addCoins">Add coins.</param>
    public void addPlayerCoin(int addCoins)
    {
        playerCoins += addCoins;
        inventoryWindow.UpdateCoinLabel();
    }
    /// <summary>
    /// Subs the player coin.
    /// </summary>
    /// <param name="subCoins">Sub coins.</param>
    public void subPlayerCoin(int subCoins)
    {
        playerCoins -= subCoins;
        inventoryWindow.UpdateCoinLabel();
    }

    // 升级
    public void LevelUp()
    {
        playerAttribute.level++;
        AddRemainPoint(PlayerAttribute.levelUpPoint);
        playerStatusWin.UpdateRemainPointText();
    }

    // 更新装备
    public void UpdateEquipment(EquipmentItemType equip, int equipId)
    {
        switch (equip)
        {
            case EquipmentItemType.HEADGEAR: playerEquipment.headgear = equipId; break;
            case EquipmentItemType.RIGHTHAND: playerEquipment.rightHand = equipId; break;
            case EquipmentItemType.LEFTHAND: playerEquipment.leftHand = equipId; break;
            case EquipmentItemType.ARMOR: playerEquipment.armor = equipId; break;
            case EquipmentItemType.ACCESSORY: playerEquipment.accessory = equipId; break;
            case EquipmentItemType.SHOE: playerEquipment.shoe = equipId; break;
        }
        UpdateEquipmentInfo();
    }
    // 更新装备信息
    public void UpdateEquipmentInfo()
    {
        playerEquipment.UpdateInfo();
        playerStatusWin.UpdateSummaryText();
    }

    /// <summary>
    /// 增加攻击力.
    /// </summary>
    public void AddAttack()
    {
    	if (playerAttribute.remainPoint > 0)
    	{
            playerAttribute.attackPlus++;
            playerAttribute.remainPoint--;
    	}
    }

    /// <summary>
    /// 增加防御力.  
    /// </summary>
    public void AddDefense()
    {
    	if (playerAttribute.remainPoint > 0)
    	{
            playerAttribute.defensePlus++;
            playerAttribute.remainPoint--;
    	}
    }

    /// <summary>
    /// 增加速度.
    /// </summary>
    public void AddSpeed()
    {
    	if (playerAttribute.remainPoint > 0)
    	{
            playerAttribute.speedPlus++;
            playerAttribute.remainPoint--;
    	}
    }

    /// <summary>
    /// 获取攻击了总和
    /// </summary>
    /// <returns>The attack.</returns>
    public int GetAttack()
    {
        return playerAttribute.attack + playerAttribute.attackPlus
                              + playerEquipment.attackEquip;
    }

    /// <summary>
    /// 获取防御总和
    /// </summary>
    /// <returns>The defense.</returns>
    public int GetDefense()
    {
        return playerAttribute.defense + playerAttribute.defensePlus
                              + playerEquipment.defenseEquip;
    }

    /// <summary>
    /// 获取速度总和
    /// </summary>
    /// <returns>The speed.</returns>
    public int GetSpeed()
    {
        return playerAttribute.speed + playerAttribute.speedPlus
                              + playerEquipment.speedEquip;
    }

    /// <summary>
    /// 获取剩余点数
    /// </summary>
    /// <returns>The remain point.</returns>
    public int GetRemainPoint()
    {
    	return playerAttribute.remainPoint;
    }

    /// <summary>
    /// 升级，增加剩余点数
    /// </summary>
    void AddRemainPoint(int num)
    {
        playerAttribute.remainPoint += num;
    }
}


