using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isFighting = false;
    private GameObject Hero;
    private GameObject Enemy;

    public static GameManager Instance;
 	
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private PT_UIManager _pt_uiManager;
    	
    public LevelManager LevelManager => _levelManager;
    public PT_UIManager PT_uiManager => _pt_uiManager;
    
    private void InitializeSingleton() {
	if(Instance) 
	{
		Debug.LogWarning("Singleton of GameManager already Initialized");
	}
	Instance = this;
    }
	
    void Awake()
    {
    	InitializeSingleton();
        Hero = GameObject.FindGameObjectWithTag("Hero");
    }

    public void setFight(bool fight){
        isFighting = fight;
    }

    public bool isCurrentlyFighting(){
        return isFighting;
    }

    public void setEnemy(GameObject enemy){
        Enemy = enemy;
    }

    public GameObject getHero(){
        return Hero;
    }

    public GameObject getEnemy(){
        return Enemy;
    }
}
