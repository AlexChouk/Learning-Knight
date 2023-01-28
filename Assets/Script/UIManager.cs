using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class UIElement
    {
        public string elementName;
        public GameObject elementReference;
    }
    
    [SerializeField] private List<UIElement> uiElements = new List<UIElement>();
    
    private Timer _timer;

    private void Start()
    {
        DisplayMain();
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

    public void DisplayMain()
    {
        DisplayMenu("Main");
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
    
    
    public void DisplayResults()
    {
    	Time.timeScale = 0f;
        DisplayMenu("Results");
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
