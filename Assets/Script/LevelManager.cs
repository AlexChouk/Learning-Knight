using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _levelParent;
    [SerializeField] private GameObject _levelButton;
    [SerializeField] private Transform _levelButtonParent;
    
    [SerializeField] private TextMeshProUGUI _resultsText;
    [SerializeField] private TextMeshProUGUI _resultsButtonText;
    
    [SerializeField] private Button _nextResults;
    [SerializeField] private Button _reloadResults;
    public FocusCamera GameCamera;
         
    private Level _currentLevel;
    private bool isResultDisplay;
    
    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;
           
           
    private void Awake()
    {
        _playerParent.gameObject.SetActive(false);
        isResultDisplay = false;
    }
    
    public void StartUI() 
    {
        GenerateButtons();
    }

    private void GenerateButtons() 
    {
     	if(_levelButtonParent.childCount == 0)
     	{
     	   foreach (Level l in _levels) 
           {
	    GameObject button = Instantiate(_levelButton, _levelButtonParent);
	    LevelButtonUI levelButtonUI = button.GetComponent<LevelButtonUI>();
	    levelButtonUI.SetLevelButton(l);
	   }
        }
    }
    
    private Level GetLevelFromName(string levelName) 
    {
        foreach (Level level in _levels) 
        {
            if (level.name.Equals(levelName)) 
            {
                return level;
            }
        }

        throw new ArgumentException("Level not found with name : " + levelName);
    }

    private Level GetLevelFromId(int id) 
    {
        int levelCount = _levels.Count;
        if (levelCount == 0) throw new ArgumentOutOfRangeException("There are no levels!");
        if (id < 0) id = -id;
        if(id >= levelCount) id = id % levelCount;
        return _levels[id];
    }
       
    private void CreateLevel(Level level) 
    {
        Instantiate(level.LevelPrefab, _levelParent);
    }

    private void ClearAllLevels() 
    {
        foreach (Transform child in _levelParent) 
        {
            Destroy(child.gameObject);
        }
    }
    
    public Level GetNextLevelFromCurrent()
    {
    	int levelCount = _levels.Count;
    	if (levelCount == 0) throw new ArgumentOutOfRangeException("There are no levels!");
        int i = 0;
        int index = 0;
     	
     	foreach (Level l in _levels) 
        {
           	if (GetLevelFromName(_currentLevel.name) == l)
           	{
           		index = i;
           	}
           	i = i+1;
        }
        index = index+1;
        if (index < 0) index = -index;
        if(index >= levelCount) index = index % levelCount;
        return _levels[index];
    }
    

    public void LoadLevel(int levelId) 
    {
        LoadLevel(GetLevelFromId(levelId));
    }

    public void LoadLevel(Level level) 
    {
        _currentLevel = level;
    	 isResultDisplay = false;
        ClearAllLevels();
        CreateLevel(level);
        _playerParent.gameObject.SetActive(true);
        _playerTransform.position = level.PlayerSpawn;
        GameManager.UiManager.timeRemaining = 0;
        _playerParent.transform.GetChild(0).GetComponent<Move>().ResetSprint();
        GameCamera.StartCamera();
    }
    
    public void ReloadLevel() 
    {   	
	GameManager.PT_uiManager.DisplayInGame();
        LoadLevel(_currentLevel);
    }
    
    private void displayResult(string res, string button) 
    {
         _resultsText.text = res;
         _resultsButtonText.text = button;
    	 isResultDisplay = true;
         
         if (_resultsText.text == "Perdu")
         {
    		_nextResults.gameObject.SetActive(false);
    		_reloadResults.gameObject.SetActive(true);
    	 }
    	 else
    	 {
    	 	if (_levels.IndexOf(_currentLevel) < _levels.Count-1)
            	{
    	 	  _nextResults.gameObject.SetActive(true);
    		  _reloadResults.gameObject.SetActive(false);
            	}
	    	else
	    	{
    		  _nextResults.gameObject.SetActive(false);
    		  _reloadResults.gameObject.SetActive(false); 

    	 	}
    	 }
         GameManager.PT_uiManager.DisplayResults();
    }
    
    public void LoadNextLevel()
    {
	GameManager.PT_uiManager.DisplayInGame();
    	LoadLevel(GetNextLevelFromCurrent());
    }
    
    void Update() 
    {
    	if (_playerTransform.position.x > 500/*|| hero n'a plus de vie */) 
    	{
    		if (! isResultDisplay)
    		{
    			displayResult(" Perdu !", "Recommencer");
    		}
    	}
    	
    	if (_playerTransform.position.x >= 600) //GameObject.Find("Level/Start_End/endPoint").gameObject.transform.position.x)
        {
        	if (! isResultDisplay)
    		{
		    _currentLevel.IsLevelDone = true;
		    displayResult(" Gagne !", "Niveau Suivant");
		    //gagne un bonus EnDeux ?
		    //Augmente les statistiques du joueur
		}
        }
    }
}
