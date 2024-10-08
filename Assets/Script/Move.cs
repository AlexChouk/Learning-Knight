using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private static float DEFAULT_SPEED = 2.0f;

    private static float SPRINT_SPEED = 6.0f;
    public float speed = 0;
    public bool isSprinting = false;
    public bool isOnCooldown = false;
    private Jump jump;
    private Slide slide;
    public float sprint_cooldown;

    public GameManager gm;

    void Awake()
    {
        jump = gameObject.GetComponent<Jump>();
        slide = gameObject.GetComponent<Slide>();
        setSpeed(DEFAULT_SPEED);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if(! gm.isCurrentlyFighting())
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        
        if(! isSprinting){
            setSpeed(DEFAULT_SPEED);
        }
        
        if (Input.GetButtonDown("Fire1") && !isOnCooldown && ! gm.isCurrentlyFighting())
        {
            if(! jump.isJumping && ! slide.isSliding){
                isSprinting = true;
                StartCoroutine(StartSprint());
            }
        }
    }

    public IEnumerator StartSprint() {
            setSpeed(SPRINT_SPEED);
            yield return new WaitForSeconds(1f);
            isSprinting = false;
            isOnCooldown = true;
            StartCoroutine(SprintCooldown());
    }

    public IEnumerator SprintCooldown(){
        yield return new WaitForSeconds(sprint_cooldown);
        isOnCooldown = false;
    }


    public void setSpeed(float sp){
        speed = sp/50;
    }

    public bool isSprintOnCooldown(){
        return isOnCooldown;
    }

    public float cooldownDuration(){
        return sprint_cooldown;
    }
}
