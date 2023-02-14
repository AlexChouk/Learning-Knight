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

    private float camSize = 70;
    private int offsetX = 20;
    private int deadZoneY = -50;
    private float offsetY = 40;

    private LevelManager lvlManager;

    private void Awake()
    {
	cam = gameObject.GetComponent<Camera>();
	camPos = gameObject.transform.position;
        knightMove = knight.GetComponent<Move>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
	
		
    public void StartCamera()
    {
        offsetY = 40;
        gameObject.transform.position = new Vector3(knight.transform.position.x + camSpeed + offsetX, knight.transform.position.y + offsetY, -15);
        offsetY = gameObject.transform.position.y;
        cam.orthographicSize = camSize;
    }

    void Update()
    {
      if (! gm.GetComponent<PT_UIManager>().isPaused)
      {
        DeadByBorder();
        if(! gm.isCurrentlyFighting()){
            camSpeed = knightMove.speed;
            if((offsetY - knight.transform.position.y) < 0){
                transform.position = new Vector3(transform.position.x + camSpeed, offsetY - (offsetY - knight.transform.position.y), -15);
            }else if((offsetY- knight.transform.position.y) > 70 && (offsetY - knight.transform.position.y) < 90){
                transform.position = new Vector3(transform.position.x + camSpeed, offsetY + (60 - (offsetY - knight.transform.position.y)), -15);
            }else{
                transform.position = new Vector3(transform.position.x + camSpeed, offsetY, -15);
            }
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
        float x = enemy.transform.position.x - hero.transform.position.x;
        gameObject.transform.position = new Vector3(hero.transform.position.x + x/2,hero.transform.position.y+10,-11);
        cam.orthographicSize = camSize - offsetX;
    }

    public void endFightMode(){
        StartCamera();
    }

    void DeadByBorder(){

        if(knight.transform.position.x < gameObject.transform.position.x - (cam.orthographicSize + offsetX) - 25 || knight.transform.position.y < deadZoneY){
            lvlManager.displayResult("Perdu");
        }
    }


}
