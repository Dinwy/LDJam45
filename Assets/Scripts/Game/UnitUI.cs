using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitUI : MonoBehaviour
	{
		public Slider Slider;
		public TextMeshProUGUI NameText;

		public void Setup(Unit unit)
		{
			NameText.text = unit.Name;
			GetComponent<SpriteRenderer>().sprite = unit.Artwork;
		}
	}
}