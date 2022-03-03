using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameHealth : MonoBehaviour {
    //单例脚本
    public static GameHealth gameHealthInstance;
    [Header("游戏血量")]
    public int gameHealth =5;
	private void Awake () {
        gameHealthInstance = this;
	}

	private void Start () {
		
	}
	
	private void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            Destroy(other.gameObject);
            gameHealth--;
            if(gameHealth <= 0)
            {
                Debug.Log("GameOver");
            }
        }
    }
}
