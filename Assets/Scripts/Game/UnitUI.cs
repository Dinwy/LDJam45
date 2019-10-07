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
		private GameManager gameManager;

		private SoundManager soundManager;

		void Start()
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			soundManager = gameManager.gameObject.GetComponent<SoundManager>();
		}

		public void Setup(UnitManager um)
		{
			UnitManager = um;
			NameText.text = um.UnitData.Name;
			HP.text = $"{UnitManager.HP} / {UnitManager.UnitData.HP}";
			GetComponent<SpriteRenderer>().sprite = um.UnitData.Artwork;

			// Temp
			if (um.UnitData.Name != "Boy" && um.UnitData.name != "MonsterA")
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
				Canvas.transform.position = new Vector3(Canvas.transform.position.x, Canvas.transform.position.y + 0.5f, Canvas.transform.position.z);
			}

			// Resize Collider
			var S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
			gameObject.GetComponent<BoxCollider>().size = S;

			RegisterEvents();

			// Temp
			HandArea = GameObject.Find("HandArea");
		}

		public void RegisterEvents()
		{
			UnitManager.OnCardDraw += OnCardDraw;
			UnitManager.OnAttack += OnAttack;
			UnitManager.OnGetDamage += OnGetDamage;
			UnitManager.OnGetHeal += OnGetHeal;
			UnitManager.OnUnitDied += OnUnitDied;
			UnitManager.OnCardAddedToDeck += OnCardAddedToDeck;
			UnitManager.OnExhausted += OnExhausted;
		}

		private void OnExhausted(object sender, Action callback)
		{
			var unit = sender as UnitManager;
			gameManager.DialogManager.UpdateDialog($"{unit.UnitData.Name} is exhausted!");

			var seq = DOTween.Sequence();
			seq.AppendInterval(1f);
			seq.AppendCallback(() => callback());
		}

		private void OnCardAddedToDeck(object sender, CardData card)
		{
			Debug.LogWarning($"{card.Name} Added to the deck");
		}

		private void OnCardDraw(object sender, CardData card)
		{
			var unit = sender as UnitManager;

			// Set owner ID
			card.OwnerID = unit.ID;

			var deckIcon = gameManager.GameUIManager.DeckIcon;
			var go = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity);
			go.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			go.transform.position = deckIcon.transform.position;
			go.transform.DOScale(1, 1f);
			// go.transform.

			go.transform.SetParent(HandArea.transform, false);

			go.GetComponent<Renderer>().material.mainTexture = card.Artwork.texture;
			go.GetComponent<CardDragger>().Card = card;

			Debug.Log($"[{this.GetType().Name}: Draw a card, {card.Name}, {card.Amount} {card.CardClass}");

			// Temp. Sort.
			HandArea.GetComponent<HandAreaManager>().Sort();
		}

		private void OnAttack(object sender, CardArgs attackArgs)
		{
			Debug.LogWarning($"[{this.GetType().Name}] OnAttack!");

			var unit = sender as UnitManager;

			var dir = (attackArgs.Receiver.transform.position - attackArgs.Attacker.transform.position).normalized;

			var duration = 0.5f;
			var sequence = DOTween.Sequence();

			switch (attackArgs.CardData.CardAnimation)
			{
				case CardAnimation.NormalAttack:
				default:
					soundManager.AudioSource.PlayOneShot(soundManager.HitTrack);

					sequence.Append(transform.DOLocalMoveX(transform.localPosition.x + 2 * dir.x, duration)
					.OnComplete(() => transform.DOLocalMoveX(transform.localPosition.x - 2 * dir.x, duration)));
					break;

			}

			attackArgs.Receiver.GetDamage(attackArgs.CardData.Amount);
			sequence.OnComplete(() => attackArgs.Callback());
		}

		private void OnUnitDied(object sender, EventArgs e)
		{
			Debug.Log("UnitDied");

			var sequence = DOTween.Sequence();

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
			var seq = DOTween.Sequence();
			Debug.LogWarning("OnGetDamage!");
			Debug.LogWarning($"[{this.GetType().Name}] Getting {damage} damage!");
			seq.Append(Slider.DOValue((float)UnitManager.HP / (float)UnitManager.UnitData.HP, 1f));
			HP.text = $"{UnitManager.HP} / {UnitManager.UnitData.HP}";
			seq.Append(transform.DOShakePosition(0.5f, 1, damage));
		}

		private void OnGetHeal(object sender, CardArgs cardArgs)
		{
			var seq = DOTween.Sequence();

			Debug.Log($"[{this.GetType().Name}] Getting Heal: {cardArgs.CardData.Amount}!");
			seq.Append(Slider.DOValue((float)UnitManager.HP / (float)UnitManager.UnitData.HP, 1f));
			HP.text = $"{UnitManager.HP} / {UnitManager.UnitData.HP}";

			Debug.LogWarning($"[{this.GetType().Name}] OnAttack!");

			var unit = sender as UnitManager;

			seq.OnComplete(() => cardArgs.Callback());
		}
	}
}