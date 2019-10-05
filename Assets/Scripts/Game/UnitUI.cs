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

		public UnitManager UnitManager { get; private set; }

		public void Setup(UnitManager um)
		{
			UnitManager = um;
			NameText.text = um.UnitData.Name;
			GetComponent<SpriteRenderer>().sprite = um.UnitData.Artwork;
			RegisterEvents();
		}

		public void RegisterEvents()
		{
			UnitManager.OnCardDraw += OnCardDraw;
		}

		private void OnCardDraw(object sender, Card card)
		{

		}
	}
}