using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour 
{

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private float _timeRemaining = 60f;
    
    public bool _timerIsRunning = false;
    public bool TimerIsRunning => _timerIsRunning;
    
    private void Start()
    {
        _timerIsRunning = true;
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
                Debug.Log("Time has run out!");
                _timeRemaining = 0f;
                _timerIsRunning = false;
            }
        }
    }
    
    public void reset() 
    {
    	_timeRemaining = 60f;
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
           
}
