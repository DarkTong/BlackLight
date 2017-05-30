using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterCreation : MonoBehaviour {

    public GameObject[] charaterPrefabs;    // 所有角色预制体
    public UIInput nameInput;
    private GameObject[] charaterGameObject;    // 所有角色
    private int length; // 所有角色个数
    private int index; // 当前角色的下标

    private string playerName;
    private int playerSelectIndex;

    private Transform m_Transform;

	// Use this for initialization
	void Start () {
        m_Transform = gameObject.transform;

        // 获取角色个数
        length = charaterPrefabs.Length;

        // 生成数组
        charaterGameObject = new GameObject[length];

        // 生成角色
        for (int i = 0; i < length; ++i)
        {
            
            charaterGameObject[i] = Instantiate(charaterPrefabs[i].gameObject,
                                             m_Transform.transform.position,
                                             m_Transform.transform.rotation);
            // 设置父组件
            charaterGameObject[i].transform.SetParent(m_Transform);
            // 显示第0个角色
            if (i == 0)
                charaterGameObject[0].SetActive(true);
            else
                charaterGameObject[i].SetActive(false);
        }

        // 当前角色下标是0
        index = 0;
	}    /// <summary>
    /// Gets the index.
    /// </summary>
    /// <returns>The index.</returns>
    public int getIndex()
    {
        return index;
    }
    /// <summary>
    /// Sets the index.
    /// </summary>
    /// <param name="idx">Index.</param>
    public void setIndex(int idx)
    {
        index = idx;
    }
    /// <summary>
    /// Gets the charater total.
    /// </summary>
    public int GetCharaterTotal()
    {
        return length;
    }
    /// <summary>
    /// Shows the charater.
    /// </summary>
    /// <param name="idx">Index.</param>
    public void ShowCharater(int idx)
    {
        charaterGameObject[idx].SetActive(true);
    }
    /// <summary>
    /// Hides the charater.
    /// </summary>
    /// <param name="idx">Index.</param>
    public void HideCharater(int idx)
    {
        charaterGameObject[idx].SetActive(false);
    }
    /// <summary>
    /// Buttons the OKD own.
    /// </summary>
    public void ButtonOKDown()
    {
        // 玩家名字
        playerName = nameInput.value;
        // 玩家选择的角色
        playerSelectIndex = index;
        //Debug.Log(playerName + "," + playerSelectIndex);

        // 加载下一个场景
    }
}
