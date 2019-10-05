using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	public TextMeshProUGUI DialogText;

	void Start()
	{

	}

	public void UpdateDialog(string text)
	{
		DialogText.text = text;
	}
}
