using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    // 摄像机移动速度
    public float speed;
    public int inZPos;
    public int farZPos;

    private Transform m_Transform;
	
	void Start () {
        m_Transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Transform.position.z < -20.0f)
            m_Transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}  
}
