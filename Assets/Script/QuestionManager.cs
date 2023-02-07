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
    
    private string[] Réponses = {"", "", "", ""};
    private string Question = "";
    private int vraie = 1;
    private int rand;
    
    private Button _ans1;
    private Button _ans2;
    private Button _ans3;
    private Button _ans4;
    private TextMeshProUGUI _question;
    
    private bool isQuestionDisplay;
    private bool isRéponsesDisplay;
    
    public GameObject knight;
    private GameObject Ennemy;
    public GameObject Questions;
    
    private bool isQuestionsDisplay;
    
    private bool isEnnemyBoss;
    
    public Timer _timer;
    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;
    
    public TextMeshProUGUI CurrentHealth;
    public TextMeshProUGUI MaxHealth;
           
    public void setType(string type)
    {
    	Type = type;
    	if (type == "Maths")
    	{
    		pathFile = "/Resources/Learning Knight - Banque questions - Mathématiques.csv";
    	}
    	
    	if (type == "French")
    	{
    		pathFile = "/Resources/Learning Knight - Banque questions - Français.csv";
    	}
    	 
    }
    
    // Start is called before the first frame update
    void Start()
    {	
    	setType("Maths");
        _ans1 = GameObject.Find("Ans1").GetComponent<Button>();
        _ans2 = GameObject.Find("Ans2").GetComponent<Button>();
        _ans3 = GameObject.Find("Ans3").GetComponent<Button>();
        _ans4 = GameObject.Find("Ans4").GetComponent<Button>();          
        _question = GameObject.Find("Question").GetComponent<TextMeshProUGUI>();	
        
        Questions = GameObject.Find("Questions");
        isQuestionsDisplay = false;
        isEnnemyBoss = false;
     	Questions.SetActive(false);
    }
    
    void Update()
    {
      if (! GameManager.GetComponent<PT_UIManager>().isPaused)
      {
     	if (GameManager.isCurrentlyFighting())
     	{
     		if (!isQuestionsDisplay)
     		{
     			Questions.SetActive(true);
	     		startQuestion(Type);
	     		isQuestionsDisplay = true;
	     		Ennemy = GameManager.getEnemy();
	     		if (Ennemy.GetComponent<HealthEnnemy>().maxHealthEnnemy >= 30) isEnnemyBoss = true;
	       	        displayEnnemyHealth();
	     	}
     	}
     	     	
    	if (isQuestionDisplay) {	
		if (!_timer.TimerIsRunning) {
			isRéponsesDisplay = true;
			isQuestionDisplay = false;
			startRéponses();
		}
    	}
    	    	
     	if (isRéponsesDisplay && !isQuestionDisplay) {
     		if (!_timer.TimerIsRunning) {
			print("Time Run Out");
			isRéponsesDisplay = false;
			knight.GetComponent<Health>().DamageLifeHero(5);
			    if (knight.GetComponent<Health>().health_value <= 0)
			    {
			      GameManager.LevelManager.displayResult("Perdu");
			    }
			    else
			    {
			    	startQuestion(Type);
			    }
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
        Debug.Log("Button clicked = " + buttonNo);
        if (buttonNo == (vraie + 1))
        {
        	Debug.Log("YES!");
        	_timer.stopTimer();
        	resetQuestion();
        	//porte le coup si monstre plus de pv repasse en phase exploration sinon continue
        	 Ennemy.GetComponent<HealthEnnemy>().takeDamage(5);
        	 displayEnnemyHealth();
        	 if(Ennemy.GetComponent<HealthEnnemy>().ennemyDead())
                 {
                 	if (isEnnemyBoss) 
                 	{
		        	knight.GetComponent<Health>().GainLifeHero(5);
              			GameManager.LevelManager.displayResult("Gagne !");	
                 	}
                 	else
                 	{
		        // Héro gagne 5 points de vie
		        knight.GetComponent<Health>().GainLifeHero(5);
		        // Ennemy détruit
		        Ennemy.SetActive(false);
		        // Retour exploration
		        _timer.reset(5);
		        Questions.SetActive(false);
		        
		        }
		        GameObject.Find("GameCamera").GetComponent<FocusCamera>().endFightMode();
		        
                } 
                else 
                {
                // Question suivante ? 
                //ou héro attaqué par ennemi puis question suivante ?
                float toWin = Mathf.Round(Ennemy.GetComponent<HealthEnnemy>().maxHealthEnnemy / 2);
                knight.GetComponent<Health>().DamageLifeHero(toWin);
		startQuestion(Type);
              }
        }
        else 
        {
        	Debug.Log("No!");
        	//mauvaise réponse -> perdu des points de vie te question suivante
        	
            knight.GetComponent<Health>().DamageLifeHero(5);
            if (knight.GetComponent<Health>().health_value <= 0)
            {
		GameManager.setFight(false);
		Questions.SetActive(false);
                GameManager.LevelManager.displayResult("Perdu");
            }
            else
	    {
		startQuestion(Type);
	    }
        }    
    }
    
    
    private void startQuestion(string type) 
    {
    	//get Type and Niveau from Level choice menu
    	isQuestionDisplay = false;
    	isRéponsesDisplay = false;
        
	int rand = Range(1, 30);
        getQuestion(rand, type);
        
        _timer.startTimer(5f);
	    	
	displayQuestion();	
    }
    
    private void startRéponses()
    {
	_timer.startTimer(10f);
     	_ans1.onClick.AddListener(() => ButtonClicked(1));
     	_ans2.onClick.AddListener(() => ButtonClicked(2));
     	_ans3.onClick.AddListener(() => ButtonClicked(3));
     	_ans4.onClick.AddListener(() => ButtonClicked(4));
     	
     	displayRéponses();
    }
    
    private void getQuestion(int index, string type) 
    {
    	if (System.IO.File.Exists(Application.dataPath+pathFile)) {
    		fileData = System.IO.File.ReadAllText(Application.dataPath+pathFile);
	    	lines = fileData.Split("\n"[0]);
	    	lineData = (lines[index].Trim()).Split(","[0]);
	    	
	    	Type = lineData[0];
	    	Niveau = lineData[1];
	    	Question = lineData[2];
	    	
	    	Réponses[0] = lineData[3];
	    	Réponses[1] = lineData[4];
	    	Réponses[2] = lineData[5];
	    	Réponses[3] = lineData[6];
	    
            	int i = 0;
	    	while (Array.IndexOf(Réponses, "") != -1)
	    	{
	    		do 
	    		{
	    			rand = Range(0,3);
	    		} while (Réponses[rand] != "");
	    		
	    		Réponses[rand] = lineData[i];
	    		i=i+1;
	    	}
            	
            	while (Réponses[i] != lineData[3])
            	{
            		i=i+1;
            	}
            	vraie = i;    	
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
    	_ans1.GetComponentInChildren<TextMeshProUGUI>().text = Réponses[0];
	_ans2.GetComponentInChildren<TextMeshProUGUI>().text = Réponses[1];
	_ans3.GetComponentInChildren<TextMeshProUGUI>().text = Réponses[2];
	_ans4.GetComponentInChildren<TextMeshProUGUI>().text = Réponses[3];
	isRéponsesDisplay = true;
    }
    
    private void resetQuestion()
    {
	_question.text = "";
	_ans1.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans2.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans3.GetComponentInChildren<TextMeshProUGUI>().text = "";
	_ans4.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
    
    public void UseEnDeux()
    {
    	knight.GetComponent<ObjectManager>().EnDeux(Réponses, vraie);
    	displayRéponses();
    }
}
