using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // scroll Speed
    public float scrollSpeed = 1f;
    public float minDistance = 5.0f;
    public float maxDistance = 18.0f;
    public float minYAngle = 10.0f;
    public float maxYAngle = 70.0f;
    // rotateSpeed;
    public float rotateSpeed = 10f;


    // playerComponent
    private Transform player;
    // 相机相对角色的方向
    private Vector3 dirCamera;
    // 相机相对角色的距离
    private float disCameraToPlayer;


	
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
        // 相机看向玩家
        transform.LookAt(player.position);
        // 相机和玩家的偏移方向
        UpdateDirCamera();
        // 偏移距离
        disCameraToPlayer = Vector3.Distance(transform.position, player.position);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));

        ScrollCamera();
        RotateCamera();

        // 确定相机位置
        transform.position = player.position + dirCamera * disCameraToPlayer;
        // 确定摄像机角度
        transform.LookAt(player.position);
	}
    /// <summary>
    /// Rotates the camera.
    /// </summary>
    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            // 先更新摄像机的位置，由于角色移动了，摄像机位置是旧值
            transform.position = player.position + dirCamera* disCameraToPlayer;

            // 计算水平偏移量并旋转
            float xOffSet = Input.GetAxis("Mouse X");
            transform.RotateAround(player.position, Vector3.up, xOffSet * rotateSpeed);

            // 计算垂直偏移量并旋转
            // 由于影响摄像机dir的值有posotion和rotation所以先把未转换的数据保留
            Vector3 prePos = transform.position, preAngle = transform.eulerAngles;
            float yOffset = Input.GetAxis("Mouse Y");
            transform.RotateAround(player.position, transform.right, -1 * yOffset * rotateSpeed);
            // 若超出返回，回滚数据
            if (transform.eulerAngles.x < minYAngle || transform.eulerAngles.x > maxYAngle)
            {
                transform.position = prePos;
                transform.eulerAngles = preAngle;
            }

            // 更新摄像机方向
            UpdateDirCamera();
        }
    }
    /// <summary>
    /// 移动相机与玩家的距离.
    /// </summary>
    void ScrollCamera()
    {
		// 鼠标滑轮改变相机与角色的距离 
		UpdateDisCameraToPlayer();
    }
    /// <summary>
    /// Updates the dis camera to player.
    /// </summary>
    void UpdateDisCameraToPlayer()
    {
        disCameraToPlayer -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        disCameraToPlayer = Mathf.Clamp(disCameraToPlayer, minDistance, maxDistance);
    }
    /// <summary>
    /// Updates the dir camera.
    /// </summary>
    void UpdateDirCamera()
    {
        dirCamera = (transform.position - player.position).normalized;
    }
}
