using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    private string pathFile;
    private string fileData;
    private string[] lines;
    private string[] lineData;
    
    private string Type;
    private string Niveau;
    
    private string[] Reponses = {"", "", "", ""};
    private string Question = "";
    private int vraie = 1;
    private int rand;
    
    private Button _ans1;
    private Button _ans2;
    private Button _ans3;
    private Button _ans4;
    private TextMeshProUGUI _question;
    
    private bool isFirstQuestionsDisplay; //first question when entering fight mode
    private bool isEnnemyBoss;
    private bool isQuestionDisplay;
    private bool isReponsesDisplay;
    
    public GameObject knight;
    private GameObject Ennemy;    
    
    public Timer _timer;
    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;
    
    public TextMeshProUGUI CurrentHealth;
    public TextMeshProUGUI MaxHealth;
               
    public void setType(string type)
    {
    	Type = type;
    	if (Type == "Maths")
    		pathFile = "/Resources/Learning Knight - Banque questions - Mathématiques.csv";
    	
    	if (Type == "French")
    		pathFile = "/Resources/Learning Knight - Banque questions - Français.csv";
    	 
    }
    
    void Start()
    {	
    	setType("Maths");
        _ans1 = GameObject.Find("Ans1").GetComponent<Button>();
        _ans2 = GameObject.Find("Ans2").GetComponent<Button>();
        _ans3 = GameObject.Find("Ans3").GetComponent<Button>();
        _ans4 = GameObject.Find("Ans4").GetComponent<Button>();          
        _question = GameObject.Find("Question").GetComponent<TextMeshProUGUI>();
                
        isFirstQuestionsDisplay = false;
        isEnnemyBoss = false;
        resetQuestion();
    }
    
    void Update()
    {
      if (! GameManager.GetComponent<PT_UIManager>().isPaused && GameManager.isCurrentlyFighting())
      {
     	if (!isFirstQuestionsDisplay)
     	{
	     	Ennemy = GameManager.getEnemy();
	     	displayEnnemyHealth();
		if (Ennemy.GetComponent<HealthEnnemy>().maxHealthEnnemy >= 30) isEnnemyBoss = true; 
	     	isFirstQuestionsDisplay = true;    
	     	startQuestion();
	}
     	     	
     	if (isFirstQuestionsDisplay)
     	{
     		if (isQuestionDisplay && !isReponsesDisplay && !_timer.TimerIsRunning) startReponses();
     		
     		if (isReponsesDisplay && !isQuestionDisplay && !_timer.TimerIsRunning) {
			_question.text = "Time Run Out";
			print("Time Run Out");
			isReponsesDisplay = false;
			knight.GetComponent<Health>().DamageLifeHero(5);
			LookKnightHealthAfterQuestion();
			startQuestion();
     		}
     	}
    	    	
     }
    }
    
    public void displayEnnemyHealth()
    { 
        CurrentHealth.text = "" + Ennemy.GetComponent<HealthEnnemy>().healthEnnemy_value;
    	MaxHealth.text = "" + Ennemy.GetComponent<HealthEnnemy>().maxHealthEnnemy;
    }
    
    void ButtonClicked(int buttonNo)
    {
        Debug.Log("(" + buttonNo + "," + vraie + ")");
        if (buttonNo == vraie)
        {
           	_question.text = "Bonne réponse !";   
        	_timer.stopTimer();
        	resetQuestion();
        	        	
        	Ennemy.GetComponent<HealthEnnemy>().takeDamage(5);
        	displayEnnemyHealth();
        	
        	if(Ennemy.GetComponent<HealthEnnemy>().ennemyDead())
                {
		    knight.GetComponent<Health>().GainLifeHero(5);
		    Ennemy.SetActive(false);        
		    GameManager.setFight(false);	
	   	    isFirstQuestionsDisplay = false;
	   	    isEnnemyBoss = false;
	   	    resetQuestion();	
		    
		    if (isEnnemyBoss) GameManager.LevelManager.displayResult("Gagne !");
	   	    GameObject.Find("GameCamera").GetComponent<FocusCamera>().endFightMode();	
                } 
                else 
                {
                startQuestion();
              }
        }
        else 
        {    
           _question.text = "Mauvaise réponse !";
           //float toWin = Mathf.Round(Ennemy.GetComponent<HealthEnnemy>().maxHealthEnnemy / 2);
           knight.GetComponent<Health>().DamageLifeHero(5);
           LookKnightHealthAfterQuestion();
        }    
    }
    
    private void LookKnightHealthAfterQuestion()
    {
     	if (knight.GetComponent<Health>().health_value <= 0)
        {
		GameManager.setFight(false);
	   	isFirstQuestionsDisplay = false;
	   	isEnnemyBoss = false;
	   	resetQuestion();
                GameManager.LevelManager.displayResult("Perdu");
        }
        else startQuestion();
    }
        
    private void startQuestion() 
    {     
        _timer.startTimer(3f);
        resetQuestion();
        getQuestion("");
	displayQuestion();	
    }
    
    private void startReponses()
    {
	_timer.startTimer(10f);
     	_ans1.onClick.AddListener(() => ButtonClicked(1));
     	_ans2.onClick.AddListener(() => ButtonClicked(2));
     	_ans3.onClick.AddListener(() => ButtonClicked(3));
     	_ans4.onClick.AddListener(() => ButtonClicked(4));
     	displayRéponses();
    }
    
    private void getQuestion(string niveau) 
    {
    	if (System.IO.File.Exists(Application.dataPath+pathFile)) {
    		fileData = System.IO.File.ReadAllText(Application.dataPath+pathFile);
	    	lines = fileData.Split("\n"[0]);
		rand = Range(0, lines.Length);
	    	lineData = (lines[rand].Trim()).Split(","[0]);
	    	
	    	Type = lineData[0];
	    	Niveau = lineData[1];
	    	Question = lineData[2];
	    	
	    	rand = Range(0,3);
	    	vraie = rand + 1;
	    	Debug.Log(vraie);
	    	Reponses[rand] = lineData[3];
	    	if (rand == 3) rand = 0;
	    	else rand = rand+1;
	    	Reponses[rand] = lineData[4];
	    	if (rand == 3) rand = 0;
	    	else rand = rand+1;
	    	Reponses[rand] = lineData[5];
	    	if (rand == 3) rand = 0;
	    	else rand = rand+1;
	    	Reponses[rand] = lineData[6];
	    	 	
	    } else {
	    	Debug.Log("File not found");
	    }
    }
    
    private void displayQuestion() 
    {
	_question.text = Question;
	isQuestionDisplay = true;
    }
    
    private void displayRéponses()
    {
    	_ans1.GetComponentInChildren<TextMeshProUGUI>().text = Reponses[0];
	_ans2.GetComponentInChildren<TextMeshProUGUI>().text = Reponses[1];
	_ans3.GetComponentInChildren<TextMeshProUGUI>().text = Reponses[2];
	_ans4.GetComponentInChildren<TextMeshProUGUI>().text = Reponses[3];
	isReponsesDisplay = true;
	isQuestionDisplay = false;
    }
    
    private void resetQuestion()
    {
	_timer.reset(3f);
    	_ans1.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans2.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans3.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans4.GetComponentInChildren<TextMeshProUGUI>().text = "";
	isReponsesDisplay = false;
	isQuestionDisplay = false;
    }
    
    public void UseEnDeux()
    {
    	knight.GetComponent<ObjectManager>().EnDeux(Reponses, vraie);
    	displayRéponses();
    }
}
