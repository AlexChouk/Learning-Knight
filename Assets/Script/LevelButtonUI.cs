using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour 
{

    [SerializeField] private TextMeshProUGUI _levelName;
    
    private Level _currentLevel;
    private bool _isLevelDone;

    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;

    public void SetLevelButton(Level level)
    {
        _currentLevel = level;
        _levelName.text = level.name;
        if(_isLevelDone)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0.6f,0.6f,0.6f,1.0f);
        }
    }

    public void OnButtonClick()
    {
        GameManager.PT_uiManager.DisplayInGame();
        GameManager.LevelManager.LoadLevel(_currentLevel);
    }
}
