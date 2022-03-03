using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TowerBuyer : MonoBehaviour{
    //单例脚本
    public static TowerBuyer towerBuyerInstance;

    [Header("炮塔预设体数组")]
    public GameObject[] towerPrefabs;
    [Header("游戏初始金钱")]
    public int currentMoney =300;
    //当前选中的炮塔编号
    private int currentTowerIndex;
    //显示金钱文本
    private Text moneyText;
    //显示生命文本
    private Text healthText;
    //射线碰撞检测器
    private RaycastHit hit;
    //射线
    private Ray ray;
    //游戏速度开关
    private Toggle gameSpeedToggle;

    //是否正在拖拽中
    private bool flagOnDrag =false;

	private void Awake () {
        towerBuyerInstance = this;
        gameSpeedToggle = transform.Find("Toggle").GetComponent<Toggle>();
        moneyText = transform.Find("CoinIcon/CoinText").GetComponent<Text>();
        healthText = transform.Find("Health/HealthText").GetComponent<Text>();
	}

	private void Start () {
        gameSpeedToggle.onValueChanged.AddListener(OnGameSpeedToggleValueChange);
        UpdateTextMoney();
    }

    public void UpdateTextMoney()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void UpdateTextHealth()
    {
        healthText.text = GameHealth.gameHealthInstance.gameHealth.ToString();
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
            if(hit.collider.transform.childCount != 0)
            {
                return;
            }
            //Debug.Log(hit.collider.name);
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

        UpdateTextMoney();
        UpdateTextHealth();
    }

    /// <summary>
    /// 开关触发事件
    /// </summary>
    public void OnGameSpeedToggleValueChange(bool val)
    {
        if(val)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
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
