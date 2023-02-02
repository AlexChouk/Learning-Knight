using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private bool isFighting = false;
    private GameObject Hero;
    private GameObject Enemy;

    void Awake()
    {
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
