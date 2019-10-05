using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace LDJam45.Game
{
	public class UnitManager : MonoBehaviour, IUnitAction
	{
		public UnitData UnitData;
		public UnitUI UnitUI;

		public List<Card> Hands = new List<Card>();
		public Stack<Card> Deck = new Stack<Card>();

		public event EventHandler<Card> OnCardDraw;
		public event EventHandler OnAttack;// Need to change arg to Card

		public event EventHandler<int> OnGetDamage; // Need to change arg to Card

		public Guid ID { get; private set; }
		public int HP { get; private set; }

		public void Setup()
		{
			if (UnitData == null)
			{
				Debug.Log($"[{this.GetType().Name}] Unit data is null!");
				return;
			}

			Debug.Log("Setup Unit Manager");
			ID = Guid.NewGuid();
			HP = UnitData.HP;
			UnitUI.Setup(this);
		}

		public void Draw()
		{
			if (Deck.Count == 0)
			{
				Debug.LogWarning("No cards in the deck!");
				return;
			}

			var card = Deck.Pop();
			Hands.Add(card);
			Debug.Log($"Card added: {card.Name}, {Hands.Count}");
			OnCardDraw?.Invoke(this, card);
		}

		public void Use()
		{

		}

		public void Attack(UnitManager unit)
		{
			OnAttack?.Invoke(this, EventArgs.Empty);
		}

		public void GetDamage(int damage)
		{
			this.HP -= damage;
			OnGetDamage?.Invoke(this, damage);
		}
	}
}