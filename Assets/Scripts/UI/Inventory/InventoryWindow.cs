using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour {

    // 对象自身
    public InventoryWindow _instance;
    // 物品栏对象    
    public List<InventoryItemGrid> itemList = new List<InventoryItemGrid>();
    // 金币显示框
    public UILabel coinLable;

    private PlayerInfo playerInfo;
    // 自身动画
    private TweenPosition tweenPosition;
    // 当前窗口是否是显示状态
    private bool isShow;


    void Awake()
    {
        _instance = this;
        tweenPosition = GetComponent<TweenPosition>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        UpdateCoinLabel();
        HideUI();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // Debug 模拟拾取东西
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItemById(Random.Range(2001, 2023));
        }
	}

    /// <summary>
    /// 捡起物品.
    /// </summary>
    /// <param name="id">物品Id</param>
    public bool AddItemById(int id)
    {
        // 1.第一：如果不是装备，判断是否已经存在该物品，
        // 1.1若存在，判断数量是否大于99
        // 1.1若小于 num + 1
        // 1.2否则不捡起
        // 2.第二，否则查找空的格子
        // 2.1若存在空的格子，添加到格子
        // 2.2否则不捡起
        bool isOK = false;
        // 一件装备占一个背包格子
        if (ItemsManage._instance.getItemById(id).type != ItemType.EQUIP)
        {
            foreach (InventoryItemGrid tmp in itemList)
            {
                if (tmp.ivenItem.getItemId() == id && tmp.ivenItem.getItemNum() < 99)
                {
                    tmp.ivenItem.AddItem(1);
                    isOK = true;
                    break;
                }
            }
        }
        if (!isOK)
        {
            foreach (InventoryItemGrid tmp in itemList)
            {
                if (tmp.ivenItem.getItemId() == 0)
                {
                    tmp.ivenItem.SetItemById(id, 1);
                    isOK = true;
                    break;
                }
            }
        }

        return isOK;
    }

    /// <summary>
    /// 更新物品栏信息.
    /// </summary>
    void UpdateInventoryInfo()
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

    //  更新金币信息
    public void UpdateCoinLabel()
    {
        coinLable.text = playerInfo.getPlayerCoins().ToString();
    }
}
