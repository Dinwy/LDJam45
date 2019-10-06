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
		public Canvas Canvas;
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
			UnitManager.OnUnitDied += OnUnitDied;
		}

		private void OnCardDraw(object sender, Card card)
		{
			var go = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<Renderer>().material.mainTexture = card.Artwork.texture;

			go.transform.SetParent(HandArea.transform, false);
			Debug.Log($"[{this.GetType().Name}: Draw a card");

			// Temp. Sort.
			HandArea.GetComponent<HandAreaManager>().Sort();
		}

		private void OnAttack(object sender, EventArgs e)
		{

		}

		private void OnUnitDied(object sender, EventArgs e)
		{
			Debug.Log("UnitDied");

			var sequence = DOTween.Sequence();

			// 
			DOTween.To(() => Canvas.GetComponent<CanvasGroup>().alpha, x => Canvas.GetComponent<CanvasGroup>().alpha = x, 0, 1f);
			GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 1f);
			sequence.AppendInterval(1f);

			sequence.AppendCallback(() =>
			{
				gameObject.SetActive(false);
				Debug.LogWarning($"[{this.GetType().Name}] Unit died");
			});
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