using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    // 鼠标管理
    public Texture2D cursorAttack;
    public Texture2D cursorLockTarget;
    public Texture2D cursorNormal;
    public Texture2D cursorNPCTalk;
    public Texture2D cursorPick;
    private Vector2 hotspot = Vector2.zero;    //   鼠标点击的点
    private CursorMode mode = CursorMode.Auto;  //  设置光标设置方式，软件/硬件

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 把光标设置成Attack.
    /// </summary>
    public void setCursorAttack()
    {
        Cursor.SetCursor(cursorAttack, hotspot, mode); 
    }
    /// <summary>
    /// 把光标设置成LockTarget.
    /// </summary>
    public void setCursorLockTarget()
    {
        Cursor.SetCursor(cursorLockTarget, hotspot, mode);
    }
    /// <summary>
    /// 把光标设置成NPCTalk.
    /// </summary>
    public void setCursorNPCTalk()
    {
        Cursor.SetCursor(cursorNPCTalk, hotspot, mode);
    }
    /// <summary>
    /// 把光标设置成Normal.
    /// </summary>
    public void setCursorNormal()
    {
        Cursor.SetCursor(cursorNormal, hotspot, mode);
    }
    /// <summary>
    /// 把光标设置成Pick.
    /// </summary>
    public void setCursorPick()
    {
        Cursor.SetCursor(cursorPick, hotspot, mode);
    }
}
