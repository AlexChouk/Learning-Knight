using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
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
    private GameObject resume;
    private GameObject main;

    private void Start()
    {
	  resume = GameObject.Find("Resume");
	  main = GameObject.Find("Background_main");
        DisplayMain();
	  isIntro = true;
	  _timer.startTimer(3.0f);

	  resume.SetActive(true);
	  main.SetActive(false);

	  StartCoroutine(Fade(3.0f, false));
	//DisplayLevels();
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

	  Image img = GameObject.Find("Resume").GetComponent<Image>();
	  TextMeshProUGUI text = GameObject.Find("Text_Resume").GetComponent<TextMeshProUGUI>();
        
	  Color c = img.color;
    
	  while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (fadeIn)
            {
                  c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            }
            else
            {
                 c.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);           
		}
            img.color = c;
		text.color = c;
        }
    }

    public void DisplayMain()
    {
        DisplayMenu("Main");

	  resume.SetActive(false);
	  main.SetActive(true);
    }

    void Update()
    {

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
        DisplayMenu("InGame");
    }
    
    public void DisplayPauseGame()
    {
    	  Time.timeScale = 0f;
        DisplayMenu("Resume");
    }
    
    public void DisplayResults(string text)
    {
    	  Time.timeScale = 0f;
        DisplayMenu("Results");
	  GameObject.Find("Text_Title_res").GetComponent<TextMeshProUGUI>().text = text;
    }
    
    public void DisplayOptions()
    {
    	  Time.timeScale = 0f;
        DisplayMenu("Options");
    }
    
    public void DisplayLevels()
    {
    	  Time.timeScale = 0f;
        DisplayMenu("Levels");
    }
}
