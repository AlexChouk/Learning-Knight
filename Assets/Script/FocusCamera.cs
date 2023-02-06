using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    [SerializeField] private GameObject knight;
    public float camSpeed;
    private Move knightMove;

    private GameManager gm;

    private Camera cam;

    private Vector3 camPos;

    private float camSize;

    // Start is called before the first frame update
    void Start()
    {
        knightMove = knight.GetComponent<Move>();
        camSpeed = knightMove.speed;
        transform.position = new Vector3(knight.transform.position.x + camSpeed + 15, transform.position.y, -15);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    if (! gm.GetComponent<PT_UIManager>().isPaused)
    {
        camSpeed = knightMove.speed;
        if(! gm.isCurrentlyFighting()){
            transform.position = new Vector3(transform.position.x + camSpeed, transform.position.y, -15);
        }
        else{
            startFightFocus();
        }
     }
    }

    void startFightFocus(){
        FightMode(gm.getHero(),gm.getEnemy());
    }


    void FightMode(GameObject hero, GameObject enemy){
        cam = gameObject.GetComponent<Camera>();
        camSize = cam.orthographicSize;
        camPos = gameObject.transform.position;
        float x = enemy.transform.position.x - hero.transform.position.x;
        gameObject.transform.position = new Vector3(hero.transform.position.x + x/2,-21,-11);
        cam.orthographicSize = 6;

    }

    void endFightMode(){
        transform.position = camPos;
        cam.orthographicSize = camSize;
    }

}
