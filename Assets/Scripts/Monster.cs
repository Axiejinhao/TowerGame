using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {
    [Header("怪物血量")]
    public float monsterHP;
    [Header("怪物身高")]
    public float monsterHeight = 1f;
    [HideInInspector]
    //击杀可获取的金钱
    private int monsterValue;
    //怪物的总血量
    private float monsterMaxHP;

    private Animator ani;
    private NavMeshAgent nav;
    private CapsuleCollider cap;
    private Slider slider;

    //死亡事件
    public Action<Monster> deathEvent;

	private void Awake () {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        slider = transform.Find("Canvas/Slider").GetComponent<Slider>();
	}

	private void Start () {
        monsterMaxHP = monsterHP;
    }
	
	private void Update () {
        slider.value = monsterHP / monsterMaxHP;
	}

    public void MonsterInit(float moveSpeed, float hp, int value, Vector3 target)
    {
        nav.speed = moveSpeed;
        monsterHP = hp;
        monsterValue = value;
        nav.SetDestination(target);
    }

    //怪物扣血
    public void TakeDamage(float damage)
    {
        monsterHP -= damage;

        if(monsterHP>0)
        {
            //播放受伤动画
            ani.SetTrigger("Damage");
            nav.isStopped = true;

            //延时恢复导航
            Invoke("ResumeNavigation", 0.2f);
        }
        else
        {
            //增加金币
            TowerBuyer.towerBuyerInstance.currentMoney += monsterValue;

            //播放死亡动画
            ani.SetTrigger("Dead");
            //停止导航
            nav.isStopped = true;
            if(deathEvent != null)
            {
                //执行死亡事件
                deathEvent(this);
            }
            
            //关闭碰撞体
            cap.enabled = false;

            //延时销毁
            Destroy(gameObject, 0.6f);
        }
    }

    /// <summary>
    /// 恢复导航
    /// </summary>
    public void ResumeNavigation()
    {
        nav.isStopped = false ;
    }
}
