using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameHealth : MonoBehaviour {
    [Header("游戏血量")]
    [Range(1,10)]
    public int gameHealth =10;
	private void Awake () {
		
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
            if(--gameHealth <= 0)
            {
                Debug.Log("GameOver");
            }
        }
    }
}
