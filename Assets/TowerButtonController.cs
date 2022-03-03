using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TowerButtonController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //单例
    public static TowerButtonController towerButtonController;

    [Header("移动的炮塔位置偏移量")]
    public float movedTowerPosOffset = 2;
    [Header("炮塔对应编号")]
    public int towerIndex;

    private RaycastHit hit;
    private GameObject currentTower;

    private void Awake()
    {
        towerButtonController = this;
    }

    private void Start()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentTower = Instantiate(TowerBuyer.towerBuyerInstance.towerPrefabs[towerIndex]);
        currentTower.GetComponent<Tower>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //让炮塔位置与射线检测位置一致
            currentTower.transform.position = hit.point + Vector3.up * movedTowerPosOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int towerCost = TowerBuyer.towerBuyerInstance.towerPrefabs[towerIndex].GetComponent<Tower>().towerCost;
        if (!hit.transform.name.Contains("Tower_Base") 
            || hit.transform.childCount != 0
            || TowerBuyer.towerBuyerInstance.currentMoney < towerCost)
        {
            //销毁炮塔
            Destroy(currentTower);
            return;
        }
        TowerBuyer.towerBuyerInstance.currentMoney -= towerCost;
        //恢复炮塔行为
        currentTower.GetComponent<Tower>().enabled = true;
        //设置为炮塔基座为父物体
        currentTower.transform.SetParent(hit.transform);
        //设置本地坐标
        currentTower.transform.localPosition = Vector3.up * 2.8f;
    }

}
