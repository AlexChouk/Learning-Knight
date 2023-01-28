using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{

    public Timer _timer;
    private GameObject resume;
    private GameObject background;
    
    // Start is called before the first frame update
    void Start()
    {
        _timer.startTimer(3f);
        resume = GameObject.Find("PT_Main/Resume");
        background = GameObject.Find("PT_Main/Background");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timer.TimerIsRunning)
        {
        	resume.SetActive(false);
        	background.SetActive(true);
        }
    }
}
