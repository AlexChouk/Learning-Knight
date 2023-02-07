using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PT_UIManager : MonoBehaviour
{
    [System.Serializable]
    public class UIElement
    {
        public string elementName;
        public GameObject elementReference;
    }
    
    [SerializeField] private List<UIElement> uiElements = new List<UIElement>();
   
    [SerializeField] private Timer _timer;
    private bool isIntro;
    private float timeIntro;
    private GameObject resume;
    private GameObject main;
    
    public bool isPaused;
    
    [SerializeField] public Camera maincamera;
    [SerializeField] public Camera ingameCamera;
    
    private GameManager _instance;
    private GameManager GameManager => _instance ??= GameManager.Instance;

    private void Start()
    {
	resume = GameObject.Find("Txt_Resume");
	main = GameObject.Find("Background_main");
        //DisplayMain();
	//DisplayIntro();
	DisplayLevels();
    }
    
    private void ClearElements()
    {
        foreach (UIElement uiElement in uiElements)
        {
            uiElement.elementReference.SetActive(false);
        }
    }
    
    private void ShowElement(string elementName)
    {
        foreach (UIElement uiElement in uiElements)
        {
            if (uiElement.elementName == elementName)
            {
                uiElement.elementReference.SetActive(true);
            }
        }
    }

    private void DisplayMenu(string menuName)
    {
        ClearElements();
        ShowElement(menuName);
    }

    IEnumerator Fade(float fadeTime, bool fadeIn)
    {
        float elapsedTime = 0.0f;

	  Image img = GameObject.Find("Txt_Resume").GetComponent<Image>();
	  TextMeshProUGUI text = GameObject.Find("Text_Resume").GetComponent<TextMeshProUGUI>();
        
	  Color img_c = img.color;
    	  Color text_c = text.color;

	  while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (fadeIn)
            {
                  img_c.a = Mathf.Clamp01(elapsedTime / fadeTime);
                  text_c.a = Mathf.Clamp01(elapsedTime / fadeTime);  
            }
            else
            {
                 img_c.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);    
                 text_c.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);         
		}
            img.color = img_c;
		text.color = text_c;
        }
    }

	private void DisplayIntro()
	{
	  isIntro = true;
	  timeIntro = 8.0f;
	  _timer.startTimer(timeIntro);

	  resume.SetActive(true);
	  main.SetActive(false);

	  Image img = resume.GetComponent<Image>();
	  TextMeshProUGUI text = GameObject.Find("Text_Resume").GetComponent<TextMeshProUGUI>();
	  
 	  Color img_c = img.color;
    	  Color text_c = text.color;

	  img_c.a = 0.0f;
	  text_c.a = 0.0f;

	  img.color = img_c;
	  text.color = text_c;
	  StartCoroutine(Fade(timeIntro/3.0f, true));
	}

    public void DisplayMain()
    {
        DisplayMenu("Main");
    	Time.timeScale = 0f;
    	isPaused = true;

	resume.SetActive(false);
	main.SetActive(true);
	maincamera.gameObject.SetActive(true);
	ingameCamera.gameObject.SetActive(false);
    }

    void Update()
    {
	if (_timer.TimerIsRunning && isIntro && _timer.TimeRemaining <= timeIntro/3.0f)
	{
		StartCoroutine(Fade(_timer.TimeRemaining, false));
	}

	if (!_timer.TimerIsRunning && isIntro)
	{
		resume.SetActive(false);
		main.SetActive(true);
		isIntro = false;	
	}
    }

    public void DisplayInGame()
    {
    	Time.timeScale = 1f;
    	isPaused = false;
	maincamera.gameObject.SetActive(false);
	ingameCamera.gameObject.SetActive(true);
        DisplayMenu("InGame");
    }
    
    public void DisplayPauseGame()
    {
    	Time.timeScale = 0f;
    	isPaused = true;
        DisplayMenu("Pause");
    }
    
    public void DisplayResults()
    {
    	maincamera.gameObject.SetActive(false);
	ingameCamera.gameObject.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
        DisplayMenu("Results");
    }
        
        
    public void goBackOptions(string name)
    {
    	GameObject.Find(name).SetActive(false);
    }
    
    public void DisplayOptions()
    {
    	maincamera.gameObject.SetActive(true);
	ingameCamera.gameObject.SetActive(false);
    	Time.timeScale = 0f;
    	isPaused = true;
	
        ShowElement("Options");	 	
    }
    
    public void DisplayOptionsInGame()
    {
	maincamera.gameObject.SetActive(false);
	ingameCamera.gameObject.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
	ShowElement("Options_in");
    }
    
    public void DisplayLevels()
    {
	maincamera.gameObject.SetActive(true);
	ingameCamera.gameObject.SetActive(false);
    	Time.timeScale = 0f;
    	isPaused = true;
        DisplayMenu("Levels");
    }
}
