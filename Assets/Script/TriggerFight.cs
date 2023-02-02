using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFight : MonoBehaviour
{
    private gameManager gm;
    public GameObject Enemy;
    private Camera cam;

    void Awake()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Hero"){
            gm.setEnemy(Enemy);
            gm.setFight(true);
        } 
    }
}
