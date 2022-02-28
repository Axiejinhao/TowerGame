using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MonsterCreater : MonoBehaviour {
    [Header("波次信息")]
    public MonsterWave[] monsterWaves;

    //怪物计时器
    private float monsterTimer =0f;
    //波次计时器
    private float waveTimer =0f;
    //怪物计数器
    private int counter =0;
    //当前波次
    private int waveIndex =0;
    //出生点
    private Transform startTran;
    //终点
    private Transform endTran;
    //怪物的旋转修正
    //private Quaternion monsterQua;

	private void Awake () {
        startTran = GameObject.FindWithTag("Start").transform;
        endTran = GameObject.FindWithTag("End").transform;
	}

	private void Start () {
        //monsterQua = Quaternion.Euler(Vector3.up * 90);
	}
	
	private void Update () {
        //计时器计时
        waveTimer += Time.deltaTime;

        //所有怪物波次生成完毕
        if(waveIndex >= monsterWaves.Length)
        {
            return;
        }

        if (waveTimer > monsterWaves[waveIndex].waveInterval)
        {
            monsterTimer += Time.deltaTime;

            if(counter < monsterWaves[waveIndex].monsterCount)
            {

                //根据计时器生成怪物
                if (monsterTimer >= monsterWaves[waveIndex].monsterInterval)
                {
                    //生成怪物
                    GameObject currentMonster = Instantiate(
                        monsterWaves[waveIndex].monsterPrefab, 
                        startTran.position, 
                        Quaternion.identity);
                    //初始化怪物信息
                    currentMonster.GetComponent<Monster>().MonsterInit(
                        monsterWaves[waveIndex].monsterNavSpeed,
                        monsterWaves[waveIndex].monsterHP,
                        monsterWaves[waveIndex].monsterValue,
                        endTran.position,);
                    //计时器归零
                    monsterTimer = 0;
                    //计数器递增
                    counter++;
                }
            }
            //怪物生成完毕
            else
            {
                //计时器归零
                waveTimer = 0;
                counter = 0;

                waveIndex++;
            }
        }
	}
}

/// <summary>
/// 怪物波次信息
/// </summary>
[System.Serializable]
public class MonsterWave
{
    [Header("下波怪到达时间")]
    public float waveInterval =3;
    [Header("怪物生成间隔")]
    public float monsterInterval =1;
    [Header("怪物个数")]
    public int monsterCount =3;
    [Header("怪物预设体")]
    public GameObject monsterPrefab;
    [Header("怪物血量")]
    public float monsterHP =100;
    [Header("怪物移动速度")]
    public float monsterNavSpeed =5;
    [Header("击杀可获取的金钱")]
    public int monsterValue = 20;
}