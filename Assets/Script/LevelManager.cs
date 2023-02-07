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
    public FocusCamera GameCamera;
         
    private Level _currentLevel;
    
    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;
           
           
    private void Awake()
    {
        _playerParent.gameObject.SetActive(false);
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
    
    public Level NextLevel(int id)
    {
    	int levelCount = _levels.Count;
        if (levelCount == 0) throw new ArgumentOutOfRangeException("There are no levels!");
        if (id < 0) id = -id;
        if(id >= levelCount) id = id % levelCount;
        return(_levels[id+1]);
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

    public void LoadLevel(int levelId) 
    {
        LoadLevel(GetLevelFromId(levelId));
    }

    public void LoadLevel(Level level) 
    {
        _currentLevel = level;
        ClearAllLevels();
        CreateLevel(level);
        _playerParent.gameObject.SetActive(true);
        _playerTransform.position = level.PlayerSpawn;
        GameCamera.StartCamera();
    	_nextResults.onClick.RemoveAllListeners();
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
         GameManager.PT_uiManager.DisplayResults();
    }
    
    private void LoadNextLevel()
    {
	GameManager.PT_uiManager.DisplayInGame();
    	LoadLevel(NextLevel(_levels.IndexOf(_currentLevel)));
    }
    
    void Update() 
    {
    	if (_playerTransform.position.x > 100/*|| hero n'a plus de vie */) 
    	{
    		_currentLevel.IsLevelDone = true;
    		displayResult(" Perdu !", "Recommencer");
            _nextResults.onClick.AddListener(() => ReloadLevel());
    	}
    	
    	if (_playerTransform.position.x >= 100) //GameObject.Find("Level/Start_End/endPoint").gameObject.transform.position.x)
        {
            _currentLevel.IsLevelDone = true;
            displayResult(" Gagne !", "Niveau Suivant");
            if (_levels.IndexOf(_currentLevel) != _levels.Count)
            {
            	_nextResults.onClick.AddListener(() => LoadNextLevel());
            }
            //gagne un bonus EnDeux ?
            //Augmente les statistiques du joueur
        }
    }
}
