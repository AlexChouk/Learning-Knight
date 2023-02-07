using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class ObjectManager : MonoBehaviour
{
    
   private string[] enDeux;
    
   public int NbEnDeux {
        get
        {
            return PlayerPrefs.GetInt(".NbEnDeux");
        }
        set
        {
            PlayerPrefs.SetInt(".NbEnDeux", NbEnDeux);
        }
    }
    
    public string[] EnDeux(string[] Réponses, int vraie)
    {
    	int i = 0;
    	int rand;
    	while (i <= 2)
        {
        	do
        	{
	    		rand = Range(0,3);
        	} while (rand == vraie);
            	Réponses[rand] = "";
            	i=i+1;
      	}
            	
       return Réponses;
    }
    
    public void addObject()
    {
    	NbEnDeux = NbEnDeux+1;
    }
    
    public void removeObject()
    {
    	NbEnDeux = NbEnDeux-1;
    }
    
    public void displayObject()
    {
    	print(NbEnDeux);
    }
}
