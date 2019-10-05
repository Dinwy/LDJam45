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
		public TextMeshProUGUI HP;
		public TextMeshProUGUI NameText;

		public UnitManager UnitManager { get; private set; }

		// Temp
		public GameObject CardPrefab;
		private GameObject HandArea { get; set; }

		public void Setup(UnitManager um)
		{
			UnitManager = um;
			NameText.text = um.UnitData.Name;
			HP.text = $"{UnitManager.HP} / {UnitManager.UnitData.HP}";
			GetComponent<SpriteRenderer>().sprite = um.UnitData.Artwork;

			RegisterEvents();

			// Temp
			HandArea = GameObject.Find("HandArea");
		}

		public void RegisterEvents()
		{
			UnitManager.OnCardDraw += OnCardDraw;
			UnitManager.OnAttack += OnAttack;
			UnitManager.OnGetDamage += OnGetDamage;
		}

		private void OnCardDraw(object sender, Card card)
		{
			var go = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity);
			go.transform.SetParent(HandArea.transform, false);
			Debug.Log($"[{this.GetType().Name}: Draw a card");
			HandArea.GetComponent<HandAreaManager>()?.Sort();
		}

		private void OnAttack(object sender, EventArgs e)
		{

		}

		private void OnGetDamage(object sender, int damage)
		{
			Debug.Log($"[{this.GetType().Name}] Getting {damage} damage!");
			Slider.DOValue((float)UnitManager.HP / (float)UnitManager.UnitData.HP, 1f);
			HP.text = $"{UnitManager.HP} / {UnitManager.UnitData.HP}";
			this.transform.DOShakePosition(0.5f, 0.5f, damage);
		}
	}
}