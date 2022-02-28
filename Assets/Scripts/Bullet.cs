using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {
    //追踪的目标
    private Transform target;
    [Header("追踪速度")]
    public float speed = 5f;
    //炮弹伤害
    private float damage = 100f;

	private void Awake () {
		
	}

	private void Start () {
		
	}
	
	private void Update () {
		if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// 炮弹信息初始化
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void BulletInit(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            //扣血
            other.GetComponent<Monster>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
