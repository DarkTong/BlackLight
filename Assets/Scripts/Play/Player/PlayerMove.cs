using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    MOVING,
    IDLE
}

public class PlayerMove : MonoBehaviour {

    // 移动速度
    public int moveSpeed = 4;
    // 角色状态
    public PlayerState state = PlayerState.MOVING;

    // 角色控制器
    private CharacterController controller;
    private playerDir dir;

	void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        dir = gameObject.GetComponent<playerDir>();
	}

    void Update()
    {
        float dis = Vector3.Distance(dir.target, transform.position);
        //Debug.Log(dir.target);
        if (dis > 0.2)
        {
            state = PlayerState.MOVING;
            controller.SimpleMove(transform.forward * moveSpeed);
        }
        else
        {
            state = PlayerState.IDLE;
        }
    }
}
