using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour {
    [Header("炮塔的价格")]
    public int towerCost = 200;
    [Header("炮塔主体")]
    public Transform turret;
    [Header("炮塔转身速度")]
    public float turnSpeed = 100f;
    [Header("炮弹预设体")]
    public GameObject bulletPrefab;
    [Header("开火的时间间隔")]
    public float fireInterval =0.5f;

    [Header("炮弹生成点")]
    public Transform firePoint;
    [Header("炮塔的炮弹伤害")]
    public float damage = 100f;

    //计时器
    private float timer = 0f;

    //攻击对象
    private List<Monster> monsterList;

	private void Awake () {
        monsterList = new List<Monster>();
	}

	private void Start () {
        //第一发子弹不需要蓄力
        timer = fireInterval-Time.deltaTime;
    }
	
	private void Update () {
        //Debug.Log(monsterList.Count);
		if(monsterList.Count == 0)
        {
            return;
        }

        //炮塔指向怪物的方向向量
        Vector3 dir = monsterList[0].transform.position + Vector3.up * monsterList[0].monsterHeight - turret.position;
        //转换成四元数
        Quaternion targetQua = Quaternion.LookRotation(dir);

        turret.rotation = Quaternion.Lerp(turret.rotation, targetQua, Time.deltaTime * turnSpeed);

        timer += Time.deltaTime;

        if(timer>=fireInterval)
        {
            //生成炮弹
            GameObject blt = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            //初始化炮弹
            blt.GetComponent<Bullet>().BulletInit(monsterList[0].transform, this.damage);
            timer = 0;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if(other.tag == "Monster")
        {
            Monster currentMonster = other.GetComponent<Monster>();
            //加入列表
            monsterList.Add(currentMonster);

            //给怪物绑定死亡事件
            currentMonster.deathEvent += RemoveMonsterFromList;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            Monster currentMonster = other.GetComponent<Monster>();
            //移除列表
            monsterList.Remove(other.GetComponent<Monster>());
            //给怪物解绑死亡事件
            currentMonster.deathEvent -= RemoveMonsterFromList;
        }
    }

    public void RemoveMonsterFromList(Monster monster)
    {
        if(monsterList.Contains(monster))
        {
            //从列表中移除
            monsterList.Remove(monster);
        }
    }
}
