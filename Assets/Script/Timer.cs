using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour 
{

    private TextMeshProUGUI _timeText;
    private float _timeRemaining;
    public float TimeRemaining => _timeRemaining;
    
    public bool _timerIsRunning;
    public bool TimerIsRunning => _timerIsRunning;
    
    private void Awake()
    {
        _timerIsRunning = false;
        _timeText = GameObject.Find("TimerQuestion").GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0f)
            {
                _timeRemaining -= Time.deltaTime;
                DisplayTime(_timeRemaining);
            }
            else
            {
                _timeRemaining = 0f;
                _timerIsRunning = false;
            }
        }
    }
    
    public void startTimer(float number) 
    {
    	_timeRemaining = number;
    	_timerIsRunning = true;
    }
    
    public void stopTimer() 
    {
    	_timerIsRunning = false;
    }
    
    public void reset(float number) 
    {
    	_timeRemaining = number;
    }
    
    public void clear(float number)
    {
    	stopTimer();
    	reset(number);
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //_timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timeText.text = string.Format("{0}", seconds);
    }
           
}
