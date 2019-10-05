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
			UnitManager.OnGetDamage += OnGetDamage;

		}

		private void OnCardDraw(object sender, Card card)
		{
			Debug.Log($"[{this.GetType().Name}: Draw a card");
		}

		private void OnGetDamage(object sender, int damage)
		{
			Debug.Log($"[{this.GetType().Name}] Getting {damage} damage!");
			Slider.DOValue((float)UnitManager.HP / (float)UnitManager.UnitData.HP, 1f);
		}
	}
}