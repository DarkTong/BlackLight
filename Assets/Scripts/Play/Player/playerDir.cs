using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDir : MonoBehaviour {

    // 鼠标点击地面的特效
    public GameObject effectClickGround;
    // 脚本
    public PlayerMove playerMove; 
    // 移动的目标位置
    public Vector3 target;

    // 射线
    private Ray ray;
    private RaycastHit hitInfo;

    // 判断鼠标是否按下
    bool isMouseDown;


	void Start () {
        playerMove = GetComponent<PlayerMove>();
        isMouseDown = false;
        target = transform.position;
	}
	
	void Update () {
        // 按下鼠标左键
        if ((UICamera.hoveredObject == null || UICamera.hoveredObject.name == "UI Root") 
            && Input.GetMouseButtonDown(0))
        {
            // 设置射线
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 是否有接触到碰撞器
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            // 接触到的碰撞器是地面
            if (isCollider && hitInfo.collider.tag == Tags.ground)
            {
                isMouseDown = true;
                ShowClickGroundEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }
        }

        // 鼠标弹起
        if (Input.GetMouseButtonUp(0))
            isMouseDown = false;
        // 移动的时候要不断更新朝向
        if (isMouseDown)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }
        }
        else if (playerMove.state == PlayerState.MOVING)
        {
            LookAtTarget(target);
        }
	}
    /// <summary>
    /// Shows the click ground effect.
    /// </summary>
    /// <param name="hitPoint">Hit point.</param>
    void ShowClickGroundEffect(Vector3 hitPoint)
    {
        // 触碰点上移，为了避免和地面的深度冲突
        hitPoint.y += 0.1f;
        // 生成特效对象
        Instantiate(effectClickGround, hitPoint, Quaternion.identity);
    }
    /// <summary>
    /// player looks at target.
    /// </summary>
    /// <param name="hitPoint">Hit point.</param>
    void LookAtTarget(Vector3 hitPoint)
    {
        target = hitPoint;
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }
}
