using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFight : MonoBehaviour
{
    private GameManager gm;
    public GameObject Enemy;
    private Camera cam;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Hero"){
            gm.setEnemy(Enemy);
            gm.setFight(true);
        } 
    }
}
