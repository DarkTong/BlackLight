using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private PlayerMove playerMove;
    // animation 组件
    private Animation playerAnima;
	// Use this for initialization
	void Start () {
        playerMove = GetComponent<PlayerMove>();
        playerAnima = GetComponent<Animation>();
	}
	
	void LateUpdate () {
        if (playerMove.state == PlayerState.MOVING)
        {
            PlayAnimation("Run");
        }
        else if (playerMove.state == PlayerState.IDLE)
        {
            PlayAnimation("Idle");
        }
	}

    /// <summary>
    /// 按名字播放角色动画.
    /// </summary>
    /// <param name="animName">Animation name.</param>
    void PlayAnimation(string animName)
    {
        playerAnima.CrossFade(animName);
    }
}
