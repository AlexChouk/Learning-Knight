using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public GameObject hero; 
    public float sprint_cooldown_duration;
    public bool isSprintOnCooldown;

    public int timeRemaining = 0;
    public Image timer_color;

    private TextMeshProUGUI timer_ui;

    private float timer = 0.0f;

    private GameObject canvas;
    private GameObject canvasSprint;
    private GameObject canvasAth;

    private GameManager gm;
    
    void Awake()
    {
        canvas = GameObject.Find("TimerSprint");
        canvasSprint = GameObject.Find("SPRINT");
        canvasAth = GameObject.Find("ATH");
        timer_ui = canvas.GetComponent<TextMeshProUGUI>();
        sprint_cooldown_duration = hero.GetComponent<Move>().cooldownDuration();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start() {
        canvas.SetActive(false);
    }

    void Update()
    {
        isSprintOnCooldown = hero.GetComponent<Move>().isSprintOnCooldown();

        if(isSprintOnCooldown){
            timeRemaining = Mathf.FloorToInt(sprint_cooldown_duration);
        }

        if(! gm.isCurrentlyFighting()){
            canvasAth.SetActive(true);
            canvasSprint.SetActive(true);
        }else{
            canvasAth.SetActive(false);
            canvasSprint.SetActive(false);
        }

        if(timeRemaining > 0){
            canvas.SetActive(true);
            timer_color.GetComponent<Image>().color = new Color32(255,0,0,100);
            Debug.Log(timeRemaining);
            timer += Time.deltaTime;
            timeRemaining -= (int) timer%60;
            timer_ui.text = "" +timeRemaining;
        }else{
            canvas.SetActive(false);
            timer_color.GetComponent<Image>().color = new Color32(128,255,165,255);
            timer = 0;
        }
    }

}
