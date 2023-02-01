using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultatManager : MonoBehaviour
{
	public void displayResultat(string text)
	{
		GameObject.Find("Text_Title_res").GetComponent<TextMeshProUGUI>().text = text;
	}
}
