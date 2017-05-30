using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindow : MonoBehaviour {

    public PlayerInfo player;
    // 道具总数
    private int itemSum;
    // 页数
    private int pageNum;
    // 当前页数
    private int currentPage;
    // 一页的数量
    private static readonly int ITEM_OF_ONE_PAGE = 3;
    // 物品管理组件
    private ItemsManage itemManage;
    // 每一页的道具信息
    private UISprite[] spriteItem = new UISprite[ITEM_OF_ONE_PAGE];
    private UILabel[] labelItem = new UILabel[ITEM_OF_ONE_PAGE];
    private UIButton[] btnItem = new UIButton[ITEM_OF_ONE_PAGE];
    private ItemInfo[] items = new ItemInfo[ITEM_OF_ONE_PAGE];
    // 页数组件
    private UILabel pageInfo;

    // 药品id
    private List<int> dragsId = null;

    // 窗口打开标志
    private bool isShow;
    // 窗口动画
    private UITweener tweenPosition;

    void Start()
    {
        itemManage = GameObject.Find("GameManage").GetComponent<ItemsManage>();
        pageInfo = transform.FindChild("ShopPage").FindChild("BGPage")
                            .FindChild("LAPage").GetComponent<UILabel>();
        // 获取组件
        for (int i = 0; i < ITEM_OF_ONE_PAGE; ++i)
        {
            Transform bgShowItem = transform.FindChild("BGShopItem_" + i);
            spriteItem[i] = bgShowItem.FindChild("IconItem").GetComponent<UISprite>();
            labelItem[i] = bgShowItem.FindChild("ItemDesc").GetComponent<UILabel>();
            btnItem[i] = bgShowItem.FindChild("BtnBuy").GetComponent<UIButton>();
        }
        tweenPosition = GetComponent<TweenPosition>();
        isShow = false;

        itemManage.getAddDragItemId(out dragsId);
        // 获取页数
        pageNum = dragsId.Count % ITEM_OF_ONE_PAGE;
        currentPage = 0;
        // 显示物品
        UpdateShow();
        // 隐藏窗口
        this.HideUI();
    }

    // 设置要显示的物品
    void UpdateShow()
    {
        // 第dragsId[j] 写进 第i个物品
        for (int i = 0, j = currentPage * ITEM_OF_ONE_PAGE; i < ITEM_OF_ONE_PAGE; ++i, ++j)
        {
            ItemInfo item = itemManage.getItemById(dragsId[j]);
            spriteItem[i].spriteName = item.iconName;
            labelItem[i].text = buildDescInfo(item);
            items[i] = item;
        }
    }

    // 构建显示信心
    string buildDescInfo(ItemInfo item)
    {
        string desc = "";

        desc += "名称：" + item.itemName + "\n";
        desc += "作用：" + item.desciption + "\n";
        desc += "售价：" + item.sellPrice + " GOLD\n";

        return desc;
    }

    /// <summary>
    /// 购买.
    /// </summary>
    /// <param name="index">按钮标识.</param>
    bool BuyBtn(int index)
    {
        int sellPrice = items[index].sellPrice;
        bool isBuyOk = false;
        if (sellPrice <= player.getPlayerCoins())
        {
            bool flag = player.AddItemById(items[index].id);
            if (flag)
            {
                player.subPlayerCoin(sellPrice);
                isBuyOk = true;
            }
        }
        return isBuyOk;
    }

    // 购买按钮事件
    public void AddBtn1()
    {   
        BuyBtn(0);
    }
    public void AddBtn2()
    {
        BuyBtn(1);
    }
    public void AddBtn3()
    {
        BuyBtn(2);
    }

    // 下一页事件
    public void NextBtn()
    {

    }
    // 上一页事件
    public void PrevBtn()
    {

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
}
