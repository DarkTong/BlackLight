using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem {

    // 玩家信息
    private PlayerInfo playerInfo;

    // 信息显示脚本
    private ItemDescript itemDesc;

    private int itemNum;

    private ItemInfo itemInfo = ItemInfo.itemNull;
    private UISprite sprite;
    private UILabel itemNumUI;
    // 获取ItemsManage脚本
    private ItemsManage itemsManage;
    private BoxCollider boxCollider;
    // 描述框标记和计时
    private bool isShowInfo = false;
    private bool hasOnShow = false;
    private float ShowInfoTime = 0.0f;
    private const float deltaTimeShowInfo = 0.5f;

    void Start()
    {
        // 执行父类的Start函数
        base.Start();
        // 获取管理组件
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        // 获取UISprite组件
        sprite = GetComponent<UISprite>();
        sprite.spriteName = itemInfo.iconName;
        // 获取UILable组件
        itemNumUI = transform.FindChild("ItemNumber").GetComponent<UILabel>();
        // 获取ItemsManage组件
        itemsManage = GameObject.Find("GameManage").GetComponent<ItemsManage>();
        // 获取信息显示框
        itemDesc = transform.parent.parent.parent.FindChild("bg_itemDescript")
                            .FindChild("ItemDescript").GetComponent<ItemDescript>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // 父类Uodate
        base.Update();
        // 控制描述框显示
        if (isShowInfo && !hasOnShow)
        {
            ShowInfoTime += Time.deltaTime;
            // 延迟显示
            if (ShowInfoTime > deltaTimeShowInfo)
            {
                itemDesc.ShowInfo(itemInfo, sprite.depth);
                hasOnShow = true;
            }
        }
    }

    /// <summary>
    /// 更新Lable面板.
    /// </summary>
    void UpdateItemNumUI()
    {
    	// 设置Lable
    	itemNumUI.text = itemNum.ToString();
        if (itemNum > 1 && itemInfo.type == ItemType.EQUIP
           || itemNum > 0 && itemInfo.type != ItemType.EQUIP)
        {
            itemNumUI.gameObject.SetActive(true);
        }
        else
            itemNumUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 物品位置归0.
    /// </summary>
    void ResetItemPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 设置字体颜色为红色，所有组件深度+2
    /// </summary>
    void SetItemDepthAndColor()
    {
        sprite.depth += 100;
        itemNumUI.depth += 100;
        itemNumUI.color = Color.red;
    }

    /// <summary>
    /// 拖拽前要改变字体颜色为红色，所有组件深度2
    /// </summary>
    void ResetItemDepthAndColor()
    {
        sprite.depth -= 100;
        itemNumUI.depth -= 100;
        itemNumUI.color = Color.white;
    }

    /// <summary>
    /// 恢复到空状态.
    /// </summary>
    void Reset()
    {
        // 重置物品信息
        itemInfo = ItemInfo.itemNull;
        itemNum = 0;
        // 更新UI显示
        itemNumUI.text = "0";
        itemNumUI.gameObject.SetActive(false);
        sprite.spriteName = itemInfo.iconName;
        // 关闭BoxCollider
        boxCollider.enabled = false;
        // 重置位置
        ResetItemPosition();
    }



    /// <summary>
    /// 物品拖拽前，所有组件增加深度.
    /// </summary>
    protected override void OnDragDropStart()
    {
        // 若是空物体则不实现拖拽
        if (itemInfo.id == 0)
        {
            //Debug.Log("empty");
            return;
        }
        // 非空物体实现拖拽
        base.OnDragDropStart();
        // 取消显示信息框
        itemDesc.HideInfo(sprite.depth);
        // 改变组件属性
        SetItemDepthAndColor();
    }

    /// <summary>
    /// 物品拖拽期间.
    /// </summary>
    /// <param name="delta">Delta.</param>
    protected override void OnDragDropMove(Vector2 delta)
    {
        // 若是空物体则不实现拖拽
        if (itemInfo.id == 0)
        {
            //Debug.Log("empty");
            return;
        }
        base.OnDragDropMove(delta);
    }

    /// <summary>
    /// 拖拽物体释放时的事件处理.
    /// </summary>
    /// <param name="surface">Surface.</param>
    protected override void OnDragDropRelease(GameObject surface)
    {
        // 若是空物体则不实现拖拽
        if (itemInfo.id == 0)
        {
            //Debug.Log("empty");
            return;
        }
        base.OnDragDropRelease(surface);
        // 恢复组件属性
        ResetItemDepthAndColor();

        // Debug
        if (surface != null)
            Debug.Log(surface.tag.ToLower() + " " + itemInfo.type.ToString().ToLower());

        // 若移动到别的位置
        if (surface == null)
        {
            ResetItemPosition();
        }
        // 若着落点是物品,
        else if (surface.tag == Tags.inventoryItem)
        {
            // 如果从装备栏移动
            if (itemInfo.type == ItemType.EQUIP)
            {
                playerInfo.UpdateEquipment((itemInfo as EquipmentItemInfo).equipType, itemInfo.id);
                //playerInfo.UpdateEquipmentInfo();
            }
            // 交换两个位置
            swapItem(surface.GetComponentInChildren<InventoryItem>());

        }
        // 或者是换装备,那么判断格子的tag是否和物品的type匹配
        else if (itemInfo.type == ItemType.EQUIP
                 && ((itemInfo as EquipmentItemInfo).jobType == JobType.COMMON
                     || playerInfo.playerJob == (itemInfo as EquipmentItemInfo).jobType)
                 && surface.tag.ToLower() == (itemInfo as EquipmentItemInfo).equipType.ToString().ToLower())
        {
            playerInfo.UpdateEquipment((itemInfo as EquipmentItemInfo).equipType, itemInfo.id);
            swapItem(surface.GetComponentInChildren<InventoryItem>());
            //playerInfo.UpdateEquipmentInfo();
        }
        else
        {
            ResetItemPosition();
        }
    }

    /// <summary>
    /// 交换两个物品的位置.
    /// </summary>
    void swapItem(InventoryItem newPlace)
    {
        //InventoryItem newPlace = surface.GetComponentInChildren<InventoryItem>();
        // 交换
        // newPlace -> tmpPlace
        int tmpItemInfoId = newPlace.itemInfo.id;
        int tmpItemNum = newPlace.itemNum;
        // nowPlace -> newPlace
        newPlace.SetItemById(itemInfo.id, itemNum);
        newPlace.ResetItemPosition();
		// tmpPlace -> nowPlace
		SetItemById(tmpItemInfoId, tmpItemNum);
		ResetItemPosition();
    }

    /// <summary>
    /// 设置物品.
    /// </summary>
    public void SetItemById(int id, int num = 1)
    {
        itemInfo = itemsManage.getItemById(id);
        // 为了控制物体拖拽 
        //if (itemInfo.id != 0)
            boxCollider.enabled = true;
        //else
        //    boxCollider.enabled = false;
        // 设置sprite显示的图片的名字
        sprite.spriteName = itemInfo.iconName;
        // 设置数量
        itemNum = num;
        // 更新Lable面板
        UpdateItemNumUI();
    }
    /// <summary>
    /// 增加物品数量.
    /// </summary>
    /// <param name="num">默认数量是1</param>
    public void AddItem(int num=1)
    {
        itemNum += num;
        UpdateItemNumUI();
    }

    /// <summary>
    /// 获取物品Id.
    /// </summary>
    /// <returns>The item identifier.</returns>
    public int getItemId()
    {
        return itemInfo.id;
    }

    /// <summary>
    /// 获取物品数量.
    /// </summary>
    /// <returns>The item number.</returns>
    public int getItemNum()
    {
        return itemNum;
    }

    /// <summary>
    /// 鼠标进入事件.
    /// </summary>
    public void OnHoverOver()
    {
        // 空物体不执行
        if (itemInfo.id == 0)
            return;
        isShowInfo = true;
        ShowInfoTime = 0.0f;
    }

    /// <summary>
    /// 鼠标移出时间.
    /// </summary>
    public void OnHoverExit()
    {   
        // 空物体不执行
        if (itemInfo.id == 0)
            return;
        if (isShowInfo == true)
        {
            isShowInfo = false;
            hasOnShow = false;
            itemDesc.HideInfo(sprite.depth);
        }
    }
}
