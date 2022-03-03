using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    //虚拟轴
    private float ver, hor;
    [Header("起始结界")]
    public Vector2 startLimit = new Vector2(20, 0);
    [Header("终点结界")]
    public Vector2 endLimit = new Vector2(40, 35);
    [Header("摄像机移动速度")]
    public float moveSpeed = 20f;

	private void Awake () {
		
	}

	private void Start () {
		
	}
	
	private void Update () {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        //获取摄像机的位置
        Vector3 currentPos = transform.position + new Vector3(hor, 0, ver) * Time.deltaTime * moveSpeed;

        //限制范围
        float x = Mathf.Clamp(currentPos.x, startLimit.x, endLimit.x);
        float z = Mathf.Clamp(currentPos.z, startLimit.y, endLimit.y);

        transform.position = new Vector3(x, currentPos.y, z);
	}
}
