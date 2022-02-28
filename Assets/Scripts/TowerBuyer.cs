using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TowerBuyer : MonoBehaviour {
    [Header("炮塔预设体数组")]
    public GameObject[] towerPrefabs;
    [Header("游戏初始金钱")]
    public int currentMoney =300;
    //当前选中的炮塔编号
    private int currentTowerIndex;
    //显示金钱文本
    private Text moneyText;
    //射线碰撞检测器
    private RaycastHit hit;
    //射线
    private Ray ray;

	private void Awake () {
        moneyText = transform.Find("CoinIcon/CoinText").GetComponent<Text>();
	}

	private void Start () {
        UpdateTextMoney();
    }

    public void UpdateTextMoney()
    {
        moneyText.text = currentMoney.ToString();
    }

    private void Update () {
        //将鼠标坐标转化为一条射线
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && 
            Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.name.Contains("Tower_Base"))
            {
                return;
            }
            //Debug.Log(currentTowerIndex);
            //获取炮塔的预设体
            Tower currentTowerPrefab = towerPrefabs[currentTowerIndex].GetComponent<Tower>();

            if (currentMoney < currentTowerPrefab.towerCost)
            {
                Debug.Log("金钱不够");
                return;
            }
            currentMoney -= currentTowerPrefab.towerCost;
            UpdateTextMoney();

            GameObject currentTower = Instantiate(towerPrefabs[currentTowerIndex]);
            //设置父对象
            currentTower.transform.SetParent(hit.collider.transform);
            currentTower.transform.localPosition = Vector3.up * 2.8f;
        }
	}

    /// <summary>
    /// 炮塔按钮点击事件
    /// </summary>
    /// <param name="towerIndex"></param>
    public void OnTowerButtonClick(int towerIndex)
    {
        //设置当前点击的炮塔编号
        currentTowerIndex = towerIndex;
    }
}
